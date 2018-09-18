﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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

            lblEditingFor.Text = "Editing custom sounds for " + frmParent.GetSelectedFilter();
        }

        /*
         * Takes in a list of filter files to open up and replace sound files in.
         */
        private void ReplaceFilterSounds(List<string> filters)
        {
            // Go through each passed in filter
            foreach (string filter in filters)
            {
                // Load the entire filter
                string strFilterContents = File.ReadAllText(filter);
                // Go through the replace action dictionary and find and replace all instances of each default
                // sound with the corresponding custom sound
                foreach(KeyValuePair<string, string> action in dictReplaceActions)
                    strFilterContents = strFilterContents.Replace(dictDefaultText[action.Key], "CustomAlertSound \"" + action.Value + "\" ");

                // Save the updated file
                //File.WriteAllText(Path.Combine(frmFilterBro.strPathOfExilePath, Path.GetFileNameWithoutExtension(filter) + "-custom.filter"), strFilterContents);
                File.WriteAllText(filter, strFilterContents);
            }

            MessageBox.Show("Filters updated successully!");
        }

        private void frmCustomSounds2_Load(object sender, EventArgs e)
        {
            // Add the default sounds. In future updates, if GGG adds more sounds
            // all we need to do is get the files, rename them like the others,
            // and increment this number.
            for (int i=1; i <= 16; i++)
            {
                string strDisplay = "Alert Sound " + i;
                lstDefaultSounds.Add(strDisplay);
                dictDefaultSounds.Add(strDisplay, "AlertSound" + i + ".mp3");
                dictDefaultText.Add(strDisplay, "PlayAlertSound " + i + " ");
            }

            // For every audio file in the PoE directory, add it to the list. Add any other popular
            // file extensions here.
            foreach (string sound in Directory.GetFiles(frmFilterBro.strPathOfExilePath, "*.*")
                .Where(s => strSupportedExtensions.Contains(Path.GetExtension(s).ToLower())))
            {
                lstCustomSounds.Add(Path.GetFileName(sound));
            }

            // Populate our combo boxes
            cboReplace.DataSource = lstDefaultSounds;
            cboWith.DataSource = lstCustomSounds;

            // Get the DataGridView ready to accept data.
            dgvActions.Columns.Add("Replace", "Replace");
            dgvActions.Columns.Add("With", "With");
        }

        /*
         * Updates the DataGridView with the replace action dictionary's contents.
         * This happens whenever the dictionary is updated.
         */
        private void UpdateDataGrid()
        {
            dgvActions.Rows.Clear();
            foreach(KeyValuePair<string, string> action in dictReplaceActions)
                dgvActions.Rows.Add(action.Key, action.Value);
        }

        /*
         * Connects to a web server to stream the built-in sound file. This is to work
         * in accordance to GGG's wishes that the built-in sound files do not get distributed
         * with a third party application.
         */
        private void btnPreviewReplace_Click(object sender, EventArgs e)
        {
            if (strStreamURL != "")
            {
                // Play the built-in sound file relative to the location of FilterBro.exe
                WindowsMediaPlayer snd = new WindowsMediaPlayer();
                snd.PlayStateChange += new WMPLib._WMPOCXEvents_PlayStateChangeEventHandler(WebSoundStatusChangeHandler);
                snd.MediaError += new WMPLib._WMPOCXEvents_MediaErrorEventHandler(WebSoundErrorHandler);
                snd.URL = strStreamURL + dictDefaultSounds[cboReplace.SelectedItem.ToString()];
                snd.controls.play();
            }
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
            if (dictReplaceActions.ContainsKey(cboReplace.SelectedItem.ToString()))
                dictReplaceActions[cboReplace.SelectedItem.ToString()] = cboWith.SelectedItem.ToString();
            else
                dictReplaceActions.Add(cboReplace.SelectedItem.ToString(), cboWith.SelectedItem.ToString());
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
                dictReplaceActions.Remove(dgvActions.SelectedRows[0].Cells[0].Value.ToString());
                UpdateDataGrid();
            }
            catch (ArgumentOutOfRangeException) { }
        }

        /*
         * Get a list of the installed filter files pertaining to the currently selected filter
         * and begin replacing sounds based on the action dictionary. Notify the user if the filter
         * is not currently installed.
         */
        private void btnApply_Click(object sender, EventArgs e)
        {
            List<string> lstSelectedFilterFiles = frmParent.GetSelectedFilterFiles();
            if (!lstSelectedFilterFiles.Any())
                MessageBox.Show("The currently selected loot filter is not installed.");
            else
                ReplaceFilterSounds(lstSelectedFilterFiles);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /*
         * Before the form closes, if there are pending actions make sure the user wants to close
         * the window without applying the changes.
         */
        private void frmCustomSounds_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dictReplaceActions.Count > 0)
            {
                if (MessageBox.Show("Close the editor without applying the selected custom sounds?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No)
                    e.Cancel = true;
            }
        }
    }
}
