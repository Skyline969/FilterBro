﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Octokit;

namespace FilterBro
{
    public partial class frmFilterBro : Form
    {
        // Version string shows in the title bar
        private static string VERSION = "1.0";
        // Stores a mapping of filter name : installed version
        private Dictionary<string, string> dictLocalVersions;
        // Stores a mapping of GitHub project : GitHub username
        private Dictionary<string, string> dictGHProjects;
        // Stores a mapping of filter name : GitHub project
        private Dictionary<string, string> dictGHMap;
        // Stores a mapping of filter name : list of installed filters pertaining to that filter
        private Dictionary<string, List<string>> dictLocalFiles;
        // Where the PoE folder is: My Documents\My Games\Path of Exile, wherever My Documents happens to be on the system.
        public static string strPathOfExilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString(), "My Games", "Path of Exile");
        // Where FilterBro stores its stuff. Just a subfolder in the Path of Exile folder.
        public static string strFilterBroPath = Path.Combine(strPathOfExilePath, "FilterBro");

        // Regex for NeverSink's filter
        Regex rg_NeverSink_title = new Regex(@"\#\s{0,}NeverSink's Indepth Loot Filter.*");
        Regex rg_NeverSink_version = new Regex(@"\#\s{0,}VERSION\:.*");
        Regex rg_NeverSink_style = new Regex(@"\#\s{0,}STYLE\:.*");

        public frmFilterBro()
        {
            InitializeComponent();
            this.dictLocalVersions = new Dictionary<string, string>();
            this.dictGHProjects = new Dictionary<string, string>();
            this.dictGHMap = new Dictionary<string, string>();
            this.dictLocalFiles = new Dictionary<string, List<string>>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblStatus.Text = "Initializing....";
            lblStatus.Refresh();
            this.Text = "FilterBro " + VERSION;

            // Add filter items
            cboFilterSelector.Items.Add("NeverSink's Item Filter - Regular");
            cboFilterSelector.Items.Add("NeverSink's Item Filter - Blue");
            cboFilterSelector.Items.Add("NeverSink's Item Filter - Gaia");
            cboFilterSelector.Items.Add("NeverSink's Item Filter - Purple");
            cboFilterSelector.Items.Add("NeverSink's Item Filter - Slick");
            cboFilterSelector.Items.Add("NeverSink's Item Filter - Vaal");
            cboFilterSelector.Items.Add("NeverSink's Item Filter - Velvet");

            // Add GitHub projects
            dictGHProjects.Add("NeverSink-Filter", "NeverSinkDev");

            // Map filter items to projects
            dictGHMap.Add("NeverSink's Item Filter - Regular", "NeverSink-Filter");
            dictGHMap.Add("NeverSink's Item Filter - Blue", "NeverSink-Filter");
            dictGHMap.Add("NeverSink's Item Filter - Gaia", "NeverSink-Filter");
            dictGHMap.Add("NeverSink's Item Filter - Purple", "NeverSink-Filter");
            dictGHMap.Add("NeverSink's Item Filter - Slick", "NeverSink-Filter");
            dictGHMap.Add("NeverSink's Item Filter - Vaal", "NeverSink-Filter");
            dictGHMap.Add("NeverSink's Item Filter - Velvet", "NeverSink-Filter");

            // Select the first filter
            cboFilterSelector.SelectedIndex = 0;

            // Check installed filters
            CheckFilterVersionLocal();

            // Update the text box
            UpdateVersionTextBoxes();

            lblStatus.Text = "";
            lblStatus.Refresh();

            // Shift focus away from the combo box as it highlights it
            this.ActiveControl = btnCheckUpdate;
        }

        /*
         * Loads all local filters and checks their version based on specific regular expressions.
         * For example, NeverSink puts his version numbers in a specific format, so we can utilize
         * a regular expression to look for that specific pattern.
         */
        private void CheckFilterVersionLocal()
        {
            lblStatus.Text = "Refreshing....";
            lblStatus.Refresh();

            // Mark all custom filters for removal
            List<object> lstComboItemsToRemove = new List<object>();
            foreach(var item in cboFilterSelector.Items)
            {
                if (IsCustomFilter(item.ToString()))
                    lstComboItemsToRemove.Add(item.ToString());
            }
            // Remove all marked custom filters from the combobox
            foreach (var item in lstComboItemsToRemove)
                cboFilterSelector.Items.Remove(item);
            // If we removed the selected item from the combobox, default to the first item
            try
            {
                string tmp = cboFilterSelector.SelectedItem.GetType().ToString();
            } catch (NullReferenceException) { cboFilterSelector.SelectedIndex = 0; }

            // Reinstantiate the list of installed filters
            this.dictLocalVersions = new Dictionary<string, string>();
            this.dictLocalFiles = new Dictionary<string, List<string>>();

            // Get a list of filters from My Documents\My Games\Path of Exile
            string[] installedFilters = Directory.GetFiles(strPathOfExilePath, "*.filter");

            // Go through each filter and attempt to find out what kind of filter it is
            foreach(string filter in installedFilters)
            {
                // Read the filter
                string contents = "";
                StreamReader sr = new StreamReader(filter);
                contents = sr.ReadToEnd();
                sr.Close();

                Match nsFilterMatch = rg_NeverSink_title.Match(contents);
                if (nsFilterMatch.Success)
                {
                    // We found a NeverSink filter
                    Match nsFilterVersionMatch = rg_NeverSink_version.Match(contents);
                    Match nsFilterStyleMatch = rg_NeverSink_style.Match(contents);
                    if (nsFilterVersionMatch.Success && nsFilterStyleMatch.Success)
                    {
                        string foundVersion = Regex.Match(nsFilterVersionMatch.Value, @"([-+]?[0-9]*\.?[0-9]+)").Groups[1].Value;
                        string tmpKey = "";
                        if (nsFilterStyleMatch.Value.Contains("NORMAL"))
                            tmpKey = "NeverSink's Item Filter - Regular";
                        else if (nsFilterStyleMatch.Value.Contains("BLUE"))
                            tmpKey = "NeverSink's Item Filter - Blue";
                        else if (nsFilterStyleMatch.Value.Contains("GAIA"))
                            tmpKey = "NeverSink's Item Filter - Gaia";
                        else if (nsFilterStyleMatch.Value.Contains("PURPLE"))
                            tmpKey = "NeverSink's Item Filter - Purple";
                        else if (nsFilterStyleMatch.Value.Contains("SLICK"))
                            tmpKey = "NeverSink's Item Filter - Slick";
                        else if (nsFilterStyleMatch.Value.Contains("VAAL"))
                            tmpKey = "NeverSink's Item Filter - Vaal";
                        else if (nsFilterStyleMatch.Value.Contains("VELVET"))
                            tmpKey = "NeverSink's Item Filter - Velvet";

                        if (tmpKey != "")
                        {
                            try {
                                dictLocalVersions.Add(tmpKey, foundVersion);
                            }
                            catch (ArgumentException) { } // Just means that filter was already added, likely searching the different filter types

                            try {
                                if (!dictLocalFiles.ContainsKey(tmpKey))
                                    dictLocalFiles.Add(tmpKey, new List<string>());
                                dictLocalFiles[tmpKey].Add(filter);
                            }
                            catch (ArgumentException) { } // Just means that filter was already added, likely searching the different filter types
                        }
                    }
                }
                else // If we don't match any regex, add this filter on its own
                {
                    try
                    {
                        string customFilter = Path.GetFileNameWithoutExtension(filter);
                        dictLocalFiles.Add(customFilter, new List<string>());
                        dictLocalFiles[customFilter].Add(filter);
                        cboFilterSelector.Items.Add(customFilter);
                    }
                    catch (ArgumentException) { }
                }
            }

            lblStatus.Text = "";
            lblStatus.Refresh();
        }

        /*
         * Checks to see if the passed-in filter is a custom filter or a built-in (supported) filter.
         */
        public bool IsCustomFilter()
        {
            try
            {
                return !dictGHMap.Keys.Contains(cboFilterSelector.SelectedItem.ToString());
            } catch (NullReferenceException) { return true; }
        }

        public bool IsCustomFilter(string filter)
        {
            try
            { 
                return !dictGHMap.Keys.Contains(filter);
            }
            catch (NullReferenceException) { return true; }
        }

        /*
         * Updates the version text boxes by clearing the latest version (as that has not been checked yet)
         * and displays the currently installed version if that filter has been installed.
         */
        private void UpdateVersionTextBoxes()
        {
            txtCurrentVersion.Text = "";
            txtLatestVersion.Text = "";

            if (IsCustomFilter())
            {
                txtCurrentVersion.Text = "N/A";
                btnCheckUpdate.Text = "Install";

                // Disable update/reinstall on a custom filter
                btnCheckUpdate.Enabled = false;
                btnReinstall.Enabled = false;
            }
            else
            {
                // Enable update/reinstall if it is a supported filter
                btnCheckUpdate.Enabled = true;
                btnReinstall.Enabled = true;

                if (dictLocalVersions.ContainsKey(cboFilterSelector.SelectedItem.ToString()))
                {
                    txtCurrentVersion.Text = dictLocalVersions[cboFilterSelector.SelectedItem.ToString()];
                    btnCheckUpdate.Text = "Check for Update";
                }
                else
                {
                    txtCurrentVersion.Text = "Not Installed";
                    btnCheckUpdate.Text = "Install";
                }
            }

            lblStatus.Text = "";
            lblStatus.Refresh();
        }

        /*
         * Downloads the selected filter, extracts it, and processes it depending on which filter is selected.
         */
        private async void UpdateSelectedFilter(string downloadURL, string project)
        {
            lblStatus.Text = "Downloading " + project + ".zip....";
            lblStatus.Refresh();
            string strExtractDirectory = "";
            // Download the file
            WebClient wbDownloader = new WebClient();
            wbDownloader.Headers.Add("user-agent", "FilterBro");
            System.IO.Directory.CreateDirectory(strFilterBroPath);
            wbDownloader.DownloadFile(downloadURL, Path.Combine(strFilterBroPath, project + ".zip"));
            // Extract the file
            lblStatus.Text = "Extracting" + project + ".zip....";
            lblStatus.Refresh();
            using (ZipArchive zip = ZipFile.OpenRead(Path.Combine(strFilterBroPath, project + ".zip")))
            {
                strExtractDirectory = Path.Combine(strFilterBroPath, zip.Entries.First().FullName.TrimEnd('/'));
                // Remove the extracted directory if it exists
                try
                {
                    Directory.Delete(strExtractDirectory, true);
                }
                catch (DirectoryNotFoundException) { }
                zip.ExtractToDirectory(strFilterBroPath);
            }

            lblStatus.Text = "Copying files....";
            lblStatus.Refresh();
            // Get the files to copy based on the filter we are installing
            string[] filtersToCopy;
            switch (cboFilterSelector.SelectedItem.ToString())
            {
                case "NeverSink's Item Filter - Regular":
                    filtersToCopy = Directory.GetFiles(strExtractDirectory, "*.filter");
                    break;
                case "NeverSink's Item Filter - Blue":
                    filtersToCopy = Directory.GetFiles(Path.Combine(strExtractDirectory, "(STYLE) BLUE"), "*.filter");
                    break;
                case "NeverSink's Item Filter - Gaia":
                    filtersToCopy = Directory.GetFiles(Path.Combine(strExtractDirectory, "(STYLE) GAIA"), "*.filter");
                    break;
                case "NeverSink's Item Filter - Purple":
                    filtersToCopy = Directory.GetFiles(Path.Combine(strExtractDirectory, "(STYLE) PURPLE"), "*.filter");
                    break;
                case "NeverSink's Item Filter - Slick":
                    filtersToCopy = Directory.GetFiles(Path.Combine(strExtractDirectory, "(STYLE) SLICK"), "*.filter");
                    break;
                case "NeverSink's Item Filter - Vaal":
                    filtersToCopy = Directory.GetFiles(Path.Combine(strExtractDirectory, "(STYLE) VAAL"), "*.filter");
                    break;
                case "NeverSink's Item Filter - Velvet":
                    filtersToCopy = Directory.GetFiles(Path.Combine(strExtractDirectory, "(STYLE) VELVET"), "*.filter");
                    break;
                default:
                    filtersToCopy = new string[0];
                    break;
            }
            if (filtersToCopy.Count() == 0)
                lblStatus.Text = "No filters found.";
            else
            {
                // Delete old filters pertaining to the currently selected filter
                try {
                    foreach (string filter in dictLocalFiles[cboFilterSelector.SelectedItem.ToString()])
                        File.Delete(filter);
                } catch (KeyNotFoundException) { } // Ignore KeyNotFoundExceptions since that will occur if this is a new filter install, not an update

                // Copy filters to Path of Exile directory
                foreach (string filter in filtersToCopy)
                    File.Copy(filter, Path.Combine(strPathOfExilePath, Path.GetFileName(filter)), true);
            }

            lblStatus.Text = "Refreshing filters....";
            lblStatus.Refresh();
            // Refresh installed filters
            CheckFilterVersionLocal();

            // Update the text box
            UpdateVersionTextBoxes();

            lblStatus.Text = "";
            lblStatus.Refresh();
            txtLatestVersion.Text = txtCurrentVersion.Text;

            cboFilterSelector.Enabled = true;
        }

        /*
         * Deletes all files pertaining to the selected filter.
         */
        private async void DeleteSelectedFilter()
        {
            lblStatus.Text = "Deleting " + cboFilterSelector.SelectedItem.ToString() + "....";
            lblStatus.Refresh();

            try {
                foreach (string filter in dictLocalFiles[cboFilterSelector.SelectedItem.ToString()])
                    File.Delete(filter);
            }
            catch (KeyNotFoundException) { } // Ignore KeyNotFoundExceptions since that will occur if this is a new filter install, not an update

            // Refresh installed filters
            CheckFilterVersionLocal();

            // Update the text box
            UpdateVersionTextBoxes();

            cboFilterSelector.Enabled = true;
            lblStatus.Text = "";
            lblStatus.Refresh();
        }

        /*
         * Checks the selected filter for updates by connecting to GitHub and getting the latest version.
         * The variable ignoreSameVersion will force an update prompt even if the versions match. This is
         * used if the user wants to reinstall their filter, for example if they want to remove any
         * customizations they may have made.
         */
        private async void CheckUpdateSelectedFilter(bool ignoreSameVersion)
        {
            if (cboFilterSelector.Enabled)
            {
                if (IsCustomFilter())
                {
                    lblStatus.Text = "Cannot update custom filter.";
                    lblStatus.Refresh();
                }
                else
                {
                    lblStatus.Text = "Checking for update....";
                    cboFilterSelector.Enabled = false;
                    // Get latest release from GitHub based on combobox currently selected item
                    if (dictGHMap.ContainsKey(cboFilterSelector.SelectedItem.ToString()))
                    {
                        // We need to make sure that there is a GitHub project for this filter, which there should be.
                        if (dictGHProjects.ContainsKey(dictGHMap[cboFilterSelector.SelectedItem.ToString()]))
                        {
                            // Connect to GitHub and get the latest version of the filter.
                            GitHubClient gh = new GitHubClient(new ProductHeaderValue("FilterBro"));
                            Release tmp = await gh.Repository.Release.GetLatest(dictGHProjects[dictGHMap[cboFilterSelector.SelectedItem.ToString()]],
                                dictGHMap[cboFilterSelector.SelectedItem.ToString()]);
                            txtLatestVersion.Text = tmp.TagName;

                            if (!ignoreSameVersion)
                            {
                                // If there is a newer version, ask the user if they want to update.
                                if (txtCurrentVersion.Text != txtLatestVersion.Text)
                                {
                                    DialogResult dialogResult = MessageBox.Show("There is a new update available for "
                                        + cboFilterSelector.SelectedItem.ToString() + "\nDo you want to update to the latest version?",
                                        "Update Available", MessageBoxButtons.YesNo);
                                    if (dialogResult == DialogResult.Yes)
                                    {
                                        lblStatus.Text = "Updating....";
                                        lblStatus.Refresh();
                                        UpdateSelectedFilter(tmp.ZipballUrl, dictGHMap[cboFilterSelector.SelectedItem.ToString()]);
                                    }
                                    else
                                    {
                                        lblStatus.Text = "";
                                        lblStatus.Refresh();
                                        cboFilterSelector.Enabled = true;
                                    }
                                }
                                else
                                {
                                    lblStatus.Text = "Filter is up to date.";
                                    lblStatus.Refresh();
                                    cboFilterSelector.Enabled = true;
                                }
                            }
                            else
                            {
                                // If we ignore the fact that the versions match, we prompt the user if they want to reinstall the filter.
                                DialogResult dialogResult = MessageBox.Show("Do you want to reinstall " + cboFilterSelector.SelectedItem.ToString()
                                    + "?", "Confirm Reinstall", MessageBoxButtons.YesNo);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    lblStatus.Text = "Reinstalling....";
                                    lblStatus.Refresh();
                                    UpdateSelectedFilter(tmp.ZipballUrl, dictGHMap[cboFilterSelector.SelectedItem.ToString()]);
                                }
                                else
                                {
                                    lblStatus.Text = "";
                                    lblStatus.Refresh();
                                    cboFilterSelector.Enabled = true;
                                }
                            }
                        }
                        else
                        {
                            lblStatus.Text = "Update URL Not Found!";
                            lblStatus.Refresh();
                            cboFilterSelector.Enabled = true;
                        }
                    }
                    else
                    {
                        lblStatus.Text = "Update Map Not Found!";
                        lblStatus.Refresh();
                        cboFilterSelector.Enabled = true;
                    }
                }
            }
        }

        /*
         * Gets the currently selected filter. This is used by the sound editor window.
         */
        public string GetSelectedFilter()
        {
            return cboFilterSelector.SelectedItem.ToString();
        }

        /*
         * Gets a list of filter files pertaining to the currently selected filter.
         * This is used by the sound editor window.
         */
        public List<string> GetSelectedFilterFiles()
        {
            List<string> tmpFiles = new List<string>();
            try
            {
                foreach (string filter in dictLocalFiles[cboFilterSelector.SelectedItem.ToString()])
                    tmpFiles.Add(filter);
            }
            catch (KeyNotFoundException) { } // Ignore KeyNotFoundExceptions since that will occur if this is a new filter install, not an update

            return tmpFiles;
        }

        /*
         * Reload the filter files once the sound editor is closed.
         * This is likely not needed anymore and was used when the
         * editor saved to a different file instead of overwriting.
         */
        private void CustomSoundsClosed(object sender, FormClosedEventArgs e)
        {
            /*lblStatus.Text = "Reloading....";
            lblStatus.Refresh();

            CheckFilterVersionLocal();
            UpdateVersionTextBoxes();

            lblStatus.Text = "";*/
        }

        private void cboFilterSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateVersionTextBoxes();
            lblStatus.Text = "";
        }

        private async void btnCheckUpdate_Click(object sender, EventArgs e)
        {
            CheckUpdateSelectedFilter(false);
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "Reloading....";
            lblStatus.Refresh();

            CheckFilterVersionLocal();
            UpdateVersionTextBoxes();

            lblStatus.Text = "";
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            // If we are not currently carrying out another operation, prompt the user if they want
            // to delete the currently selected filter, and do so if they confirm.
            if (cboFilterSelector.Enabled)
            {
                cboFilterSelector.Enabled = false;
                DialogResult dialogResult = MessageBox.Show(((IsCustomFilter()) ? "Are you sure you want to delete "
                    + cboFilterSelector.SelectedItem.ToString() + "?" : "Are you sure you want to delete all filters for " 
                    + cboFilterSelector.SelectedItem.ToString() + "?"), "Confirm Delete", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                    DeleteSelectedFilter();
                else
                    cboFilterSelector.Enabled = true;
            }
        }

        /*
         * Opens the custom sound editor window. Make sure the currently selected filter is installed
         * before opening the window, and if it isn't ask the user if they want to install it.
         */
        private void btnCustomSoundsOpen_Click(object sender, EventArgs e)
        {
            if (txtCurrentVersion.Text != "Not Installed")
            {
                if (Directory.GetFiles(frmFilterBro.strPathOfExilePath, "*.*")
                    .Where(s => frmCustomSounds.strSupportedExtensions.Contains(Path.GetExtension(s).ToLower())).Count() > 0)
                {
                    frmCustomSounds csForm = new frmCustomSounds(this);
                    csForm.FormClosed += new FormClosedEventHandler(CustomSoundsClosed);
                    csForm.ShowDialog();
                }
                else
                    MessageBox.Show("You don't have any supported sound files in your Path of Exile directory. Supported extensions: " + frmCustomSounds.strSupportedExtensions);
            }
            else if (MessageBox.Show("The currently selected filter is not installed. Do you want to install it?",
                "Filter Not Installed", MessageBoxButtons.YesNo) == DialogResult.Yes)
                CheckUpdateSelectedFilter(false);
        }

        private void btnReinstall_Click(object sender, EventArgs e)
        {
            CheckUpdateSelectedFilter(true);
        }
    }
}
