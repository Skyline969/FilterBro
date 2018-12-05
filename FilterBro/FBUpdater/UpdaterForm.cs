using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FBUpdater
{
    public partial class frmMainForm : Form
    {
        // Where the PoE folder is: My Documents\My Games\Path of Exile, wherever My Documents happens to be on the system.
        public static string strPathOfExilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString(), "My Games", "Path of Exile");
        // Where FilterBro stores its stuff. Just a subfolder in the Path of Exile folder.
        public static string strFilterBroPath = Path.Combine(strPathOfExilePath, "FilterBro");

        public frmMainForm()
        {
            InitializeComponent();
        }

        private void frmMainForm_Load(object sender, EventArgs e)
        {
            InstallUpdateFiles();
        }

        /*
         * Installs the downloaded update files, then cleans up the directory and zip file.
         */
        private void InstallUpdateFiles()
        {
            if (Directory.Exists(Path.Combine(strFilterBroPath, "FilterBroUpdate")))
            {
                try
                {
                    DirectoryInfo dirUpdateDirectory = new DirectoryInfo(Path.Combine(Directory.GetDirectories(Path.Combine(strFilterBroPath,
                    "FilterBroUpdate"))[0].ToString()));
                    foreach (var file in dirUpdateDirectory.GetFiles())
                    {
                        lblStatus.Text = "Copying " + file.Name + "....";
                        lblStatus.Refresh();
                        //File.Move(file.FullName, file.Name);
                        File.Copy(file.FullName, file.Name, true);
                        File.Delete(file.FullName);
                    }

                    // Finally, delete the update directory
                    lblStatus.Text = "Cleaning up....";
                    lblStatus.Refresh();
                    Directory.Delete(Path.Combine(strFilterBroPath, "FilterBroUpdate"), true);
                    File.Delete(Path.Combine(strFilterBroPath, "FilterBroUpdate.zip"));

                    // Let the user know we're closing up
                    lblStatus.Text = "Returning to FilterBro....";
                    lblStatus.Refresh();
                    Close();
                }
                catch (DirectoryNotFoundException)
                {
                    // Let the user know there was a problem
                    lblStatus.Text = "Could not find update subdirectory!";
                    lblStatus.Refresh();
                }
            }
            else
            {
                // Let the user know there was a problem
                lblStatus.Text = "Could not find update directory!";
                lblStatus.Refresh();
            }
        }

        private void frmMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process.Start("FilterBro.exe");
        }
    }
}
