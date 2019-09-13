﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using WMPLib;

namespace FilterBro
{
    public partial class frmCustomSounds : Form
    {
        // Reference to the parent form
        private frmFilterBro frmParent;
        // Flagged from the parent if the apply button needs to be clicked automatically
        public bool bAutoApply;
        // Stores a mapping of built in sound : sound to replace it with
        private Dictionary<string, string> dictReplaceActions;
        // Stores a mapping of combo box value : sound file
        private Dictionary<string, string> dictDefaultSounds;
        // Stores a mapping of combo box value : text used in filter
        private Dictionary<string, string> dictDefaultText;
        // A list of the default sounds
        private List<string> lstDefaultSounds;
        // A list of the custom sounds available
        private List<string> lstCustomSounds;
        // A list of all default sounds
        private List<string> lstPreviewSounds;
        // Supported file extensions for sounds
        public static string strSupportedExtensions = "*.mp3,*.wav";
        // URL to stream built-in sounds from
        private static string strStreamURL = "";

        public frmCustomSounds(frmFilterBro owner)
        {
            InitializeComponent();

            this.frmParent = owner;
            dictReplaceActions = new Dictionary<string, string>();
            dictDefaultSounds = new Dictionary<string, string>();
            dictDefaultText = new Dictionary<string, string>();
            lstDefaultSounds = new List<string>();
            lstCustomSounds = new List<string>();
            lstPreviewSounds = new List<string>();
            bAutoApply = false;

            lblEditingFor.Text = "Editing custom sounds for " + frmParent.GetSelectedFilter();
        }

        private void frmCustomSounds2_Load(object sender, EventArgs e)
        {
            for (int i = 1; i <= 16; i++)
            {
                string strDisplay = "Alert Sound " + i;
                dictDefaultSounds.Add(strDisplay, "AlertSound" + i + ".mp3");
                dictDefaultText.Add(strDisplay, "PlayAlertSound " + i);
                lstPreviewSounds.Add(strDisplay);
            }
            // Add the default sounds that appear in the selected filter
            List<string> filters = frmParent.GetSelectedFilterFiles();
            foreach (string filter in filters)
            {
                // Open the filter files relevant to the currently selected filter
                string contents = "";
                StreamReader sr = new StreamReader(filter);
                contents = sr.ReadToEnd();
                sr.Close();
                // Scan for any PlayAlertSound # occurrences
                Regex ItemRegex = new Regex(@"PlayAlertSound\s{1,}\d{1,}", RegexOptions.Compiled);
                foreach (Match match in ItemRegex.Matches(contents))
                {
                    // Get the number
                    string strAlertSound = Regex.Match(match.Value, @"\d{1,}").Value.ToString().Trim();
                    // If the number doesn't exist in the list, add it
                    if (!lstDefaultSounds.Contains<string>("Alert Sound " + strAlertSound))
                    {
                        string strDisplay = "Alert Sound " + strAlertSound;
                        lstDefaultSounds.Add(strDisplay);
                    }
                }
            }

            // Sort the found default sounds
            lstDefaultSounds = lstDefaultSounds.OrderBy(s => int.Parse(s.Substring(12))).ToList<string>();
            lstPreviewSounds = lstPreviewSounds.OrderBy(s => int.Parse(s.Substring(12))).ToList<string>();

            // For every audio file in the PoE directory, add it to the list. Add any other popular
            // file extensions here.
            foreach (string sound in Directory.GetFiles(frmFilterBro.strPathOfExilePath, "*.*")
                .Where(s => strSupportedExtensions.Contains(Path.GetExtension(s).ToLower())))
                    lstCustomSounds.Add(Path.GetFileName(sound));

            // Populate our combo boxes
            cboReplace.DataSource = lstDefaultSounds;
            cboWith.DataSource = lstCustomSounds;
            cboPreviewList.DataSource = lstPreviewSounds;

            // Get the DataGridView ready to accept data.
            dgvActions.Columns.Add("Replace", "Replace");
            dgvActions.Columns.Add("With", "With");

            // Update the data grid with existing actions
            UpdateDataGrid();

            // Set the Replace preview button visibility, allowing users to provide the files
            CheckReplaceSoundExists();

            // Set the Preview preview button visibility, allowing users to provide the files
            CheckPreviewSoundExists();

            if (bAutoApply)
                ApplyButtonClicked();
        }

        /*
         * Updates the DataGridView with the replace action dictionary's contents.
         * This happens whenever the dictionary is updated.
         */
        private void UpdateDataGrid()
        {
            dgvActions.Rows.Clear();
            //foreach(KeyValuePair<string, string> action in dictReplaceActions)
            foreach (KeyValuePair<string, string> action in frmParent.dictFilterSounds[frmParent.GetSelectedFilter()])
                dgvActions.Rows.Add(action.Key, action.Value);
            dgvActions.Sort(dgvActions.Columns["Replace"], ListSortDirection.Ascending);
        }

        /*
         * Takes in a list of filter files to open up and replace sound files in.
         */
        public void ReplaceFilterSounds(List<string> filters)
        {
            // Go through each passed in filter
            foreach (string filter in filters)
            {
                // Load the entire filter
                string strFilterContents = File.ReadAllText(filter);
                // Go through the replace action dictionary and find and replace all instances of each default
                // sound with the corresponding custom sound
                //foreach(KeyValuePair<string, string> action in dictReplaceActions)
                foreach (KeyValuePair<string, string> action in frmParent.dictFilterSounds[frmParent.GetSelectedFilter()])
                {
                    try
                    {
                        // We need to find all occurrences of the alert sound as well as the volume. We need to strip the volume
                        // as PoE does not use volume for custom alert sounds.
                        Regex ItemRegex = new Regex(dictDefaultText[action.Key] + @"\s{1,}\d{0,}", RegexOptions.Compiled);
                        var ReplaceMatches = ItemRegex.Matches(strFilterContents);
                        
                        // Replace the PlayAlertSound occurrences with CustomAlertSound values the user has defined.
                        foreach (var match in ReplaceMatches.Cast<Match>().Reverse())
                        {
                            StringBuilder sbBuilder = new StringBuilder();
                            sbBuilder.Append(strFilterContents.Substring(0, match.Index));
                            sbBuilder.Append("CustomAlertSound \"" + action.Value + "\" ");
                            sbBuilder.Append(strFilterContents.Substring(match.Index + match.Length));
                            strFilterContents = sbBuilder.ToString();
                        }
                        // Old replacement code, now defunct as it does not replace volume which breaks custom sounds.
                        // Will be removed in a future update.
                        //strFilterContents = strFilterContents.Replace(dictDefaultText[action.Key], "CustomAlertSound \"" + action.Value + "\" ");
                    } // The key might not exist if we are using a saved config. This is fine.
                    catch (KeyNotFoundException) { }
                }
                // Save the updated file
                File.WriteAllText(filter, strFilterContents);
            }
            //dictReplaceActions.Clear();
            //dgvActions.Rows.Clear();
            MessageBox.Show("Filters updated successully!");
        }

        /*
         * Connects to a web server to stream the built-in sound file. This is to work
         * in accordance to GGG's wishes that the built-in sound files do not get distributed
         * with a third party application.
         */
        private void btnPreviewReplace_Click(object sender, EventArgs e)
        {
            // Play the custom sound file located in the same folder as FilterBro
            WindowsMediaPlayer snd = new WindowsMediaPlayer();
            snd.URL = dictDefaultSounds[cboReplace.SelectedItem.ToString()];
            snd.controls.play();
            // Code for streaming from a URL
            /*if (strStreamURL != "")
            {
                // Play the built-in sound file relative to the location of FilterBro.exe
                WindowsMediaPlayer snd = new WindowsMediaPlayer();
                snd.PlayStateChange += new WMPLib._WMPOCXEvents_PlayStateChangeEventHandler(WebSoundStatusChangeHandler);
                snd.MediaError += new WMPLib._WMPOCXEvents_MediaErrorEventHandler(WebSoundErrorHandler);
                snd.URL = strStreamURL + dictDefaultSounds[cboReplace.SelectedItem.ToString()];
                snd.controls.play();
            }*/
        }

        /*
         * Handles the preview button when streaming a sound file. Prevents the user from
         * mashing the preview button if the sound doesn't play instantly to avoid tons
         * of requests to the web server.
         */
        private void WebSoundStatusChangeHandler(int state)
        {
            if ((WMPLib.WMPPlayState)state == WMPLib.WMPPlayState.wmppsStopped)
            {
                btnPreviewReplace.Enabled = true;
                cboReplace.Enabled = true;
            }
            else
            {
                btnPreviewReplace.Enabled = false;
                cboReplace.Enabled = false;
            }
        }

        /*
         * Handles any errors encountered while streaming an audio file.
         */
        private void WebSoundErrorHandler(object wmpObj)
        {
            MessageBox.Show("Could not play " + dictDefaultSounds[cboReplace.SelectedItem.ToString()]);
            btnPreviewReplace.Enabled = true;
            cboReplace.Enabled = true;
        }

        private void btnWithPreview_Click(object sender, EventArgs e)
        {
            // Play the custom sound file located in the Path of Exile install folder
            MediaPlayer snd = new MediaPlayer();
            snd.Open(new System.Uri(Path.Combine(frmFilterBro.strPathOfExilePath, cboWith.SelectedValue.ToString())));
            snd.Play();
        }

        /*
         * Add a new replace action given the values of the two combo boxes.
         * If there is already an action to replace the selected default sound,
         * it will be updated with the newly selected sound.
         */
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (frmParent.dictFilterSounds[frmParent.GetSelectedFilter()].ContainsKey(cboReplace.SelectedItem.ToString()))
                frmParent.dictFilterSounds[frmParent.GetSelectedFilter()][cboReplace.SelectedItem.ToString()] = cboWith.SelectedItem.ToString();
            else
                frmParent.dictFilterSounds[frmParent.GetSelectedFilter()].Add(cboReplace.SelectedItem.ToString(), cboWith.SelectedItem.ToString());
            /*if (dictReplaceActions.ContainsKey(cboReplace.SelectedItem.ToString()))
                dictReplaceActions[cboReplace.SelectedItem.ToString()] = cboWith.SelectedItem.ToString();
            else
                dictReplaceActions.Add(cboReplace.SelectedItem.ToString(), cboWith.SelectedItem.ToString());*/
            UpdateDataGrid();
        }

        /*
         * Remove the selected action from the action dictionary based on the selected row
         * in the data grid.
         */
        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                //dictReplaceActions.Remove(dgvActions.SelectedRows[0].Cells[0].Value.ToString());
                frmParent.dictFilterSounds[frmParent.GetSelectedFilter()].Remove(dgvActions.SelectedRows[0].Cells[0].Value.ToString());
                UpdateDataGrid();
            }
            catch (ArgumentOutOfRangeException) { }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            ApplyButtonClicked();
        }

        /*
         * Get a list of the installed filter files pertaining to the currently selected filter
         * and begin replacing sounds based on the action dictionary. Notify the user if the filter
         * is not currently installed.
         */
        private void ApplyButtonClicked()
        {
            SaveCustomSounds();
            List<string> lstSelectedFilterFiles = frmParent.GetSelectedFilterFiles();
            if (!lstSelectedFilterFiles.Any())
                MessageBox.Show("The currently selected loot filter is not installed.");
            else
                ReplaceFilterSounds(lstSelectedFilterFiles);
            this.Close();
        }

        private void SaveCustomSounds()
        {
            // Serialize the custom sounds dictionary
            string strCustomSounds = JsonConvert.SerializeObject(frmParent.dictFilterSounds, Formatting.Indented);
            // Write the custom sounds
            File.WriteAllText("sound_config.json", strCustomSounds);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Close the editor without applying the selected custom sounds?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                this.Close();
        }

        /*
         * Before the form closes, if there are pending actions make sure the user wants to close
         * the window without applying the changes.
         */
        private void frmCustomSounds_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (dictReplaceActions.Count > 0)
            /*if (frmParent.dictFilterSounds[frmParent.GetSelectedFilter()].Count > 0)
            {
                if (MessageBox.Show("Close the editor without applying the selected custom sounds?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No)
                    e.Cancel = true;
            }*/
        }

        /*
         * When the Replace combo box changes, check to see if we have the sound file.
         */
        private void cboReplace_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckReplaceSoundExists();
        }

        /*
         * Enables/disables the Replace Preview button based on whether the preview file exists.
         */
        private void CheckReplaceSoundExists()
        {
            bool blComboItemExists = false;
            try
            {
                blComboItemExists = File.Exists(dictDefaultSounds[cboReplace.SelectedItem.ToString()]);
            } catch (NullReferenceException) { }
            btnPreviewReplace.Visible = blComboItemExists;
            btnPreviewReplace.Enabled = blComboItemExists;
        }

        private void cboPreviewList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckPreviewSoundExists();
        }

        /*
         * Enables/disables the Preview Preview button based on whether the preview file exists.
         */
        private void CheckPreviewSoundExists()
        {
            bool blPreviewItemExists = false;
            try
            {
                blPreviewItemExists = File.Exists(dictDefaultSounds[cboPreviewList.SelectedItem.ToString()]);
            }
            catch (NullReferenceException) { }
            btnPreviewPreview.Visible = blPreviewItemExists;
            btnPreviewPreview.Enabled = blPreviewItemExists;
        }

        private void btnPreviewPreview_Click(object sender, EventArgs e)
        {
            WindowsMediaPlayer snd = new WindowsMediaPlayer();
            snd.URL = dictDefaultSounds[cboPreviewList.SelectedItem.ToString()];
            snd.controls.play();
        }
    }
}
