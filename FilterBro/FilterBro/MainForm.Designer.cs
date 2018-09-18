namespace FilterBro
{
    partial class frmFilterBro
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFilterBro));
            this.lblFilterDropdown = new System.Windows.Forms.Label();
            this.cboFilterSelector = new System.Windows.Forms.ComboBox();
            this.lblCurrentVer = new System.Windows.Forms.Label();
            this.txtCurrentVersion = new System.Windows.Forms.TextBox();
            this.lblLatestVer = new System.Windows.Forms.Label();
            this.txtLatestVersion = new System.Windows.Forms.TextBox();
            this.btnCheckUpdate = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnCustomSoundsOpen = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnReinstall = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblFilterDropdown
            // 
            this.lblFilterDropdown.AutoSize = true;
            this.lblFilterDropdown.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblFilterDropdown.Location = new System.Drawing.Point(13, 13);
            this.lblFilterDropdown.Name = "lblFilterDropdown";
            this.lblFilterDropdown.Size = new System.Drawing.Size(29, 13);
            this.lblFilterDropdown.TabIndex = 0;
            this.lblFilterDropdown.Text = "Filter";
            // 
            // cboFilterSelector
            // 
            this.cboFilterSelector.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.cboFilterSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilterSelector.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboFilterSelector.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cboFilterSelector.FormattingEnabled = true;
            this.cboFilterSelector.Location = new System.Drawing.Point(49, 10);
            this.cboFilterSelector.Name = "cboFilterSelector";
            this.cboFilterSelector.Size = new System.Drawing.Size(223, 21);
            this.cboFilterSelector.TabIndex = 1;
            this.cboFilterSelector.SelectedIndexChanged += new System.EventHandler(this.cboFilterSelector_SelectedIndexChanged);
            // 
            // lblCurrentVer
            // 
            this.lblCurrentVer.AutoSize = true;
            this.lblCurrentVer.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblCurrentVer.Location = new System.Drawing.Point(13, 55);
            this.lblCurrentVer.Name = "lblCurrentVer";
            this.lblCurrentVer.Size = new System.Drawing.Size(82, 13);
            this.lblCurrentVer.TabIndex = 2;
            this.lblCurrentVer.Text = "Current Version:";
            // 
            // txtCurrentVersion
            // 
            this.txtCurrentVersion.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txtCurrentVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCurrentVersion.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtCurrentVersion.Location = new System.Drawing.Point(104, 52);
            this.txtCurrentVersion.Name = "txtCurrentVersion";
            this.txtCurrentVersion.ReadOnly = true;
            this.txtCurrentVersion.Size = new System.Drawing.Size(168, 20);
            this.txtCurrentVersion.TabIndex = 3;
            // 
            // lblLatestVer
            // 
            this.lblLatestVer.AutoSize = true;
            this.lblLatestVer.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblLatestVer.Location = new System.Drawing.Point(13, 90);
            this.lblLatestVer.Name = "lblLatestVer";
            this.lblLatestVer.Size = new System.Drawing.Size(77, 13);
            this.lblLatestVer.TabIndex = 4;
            this.lblLatestVer.Text = "Latest Version:";
            // 
            // txtLatestVersion
            // 
            this.txtLatestVersion.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txtLatestVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLatestVersion.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtLatestVersion.Location = new System.Drawing.Point(104, 87);
            this.txtLatestVersion.Name = "txtLatestVersion";
            this.txtLatestVersion.ReadOnly = true;
            this.txtLatestVersion.Size = new System.Drawing.Size(168, 20);
            this.txtLatestVersion.TabIndex = 5;
            // 
            // btnCheckUpdate
            // 
            this.btnCheckUpdate.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnCheckUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCheckUpdate.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCheckUpdate.Location = new System.Drawing.Point(16, 226);
            this.btnCheckUpdate.Name = "btnCheckUpdate";
            this.btnCheckUpdate.Size = new System.Drawing.Size(105, 23);
            this.btnCheckUpdate.TabIndex = 6;
            this.btnCheckUpdate.Text = "Check for Update";
            this.btnCheckUpdate.UseVisualStyleBackColor = false;
            this.btnCheckUpdate.Click += new System.EventHandler(this.btnCheckUpdate_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblStatus.Location = new System.Drawing.Point(16, 124);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(256, 41);
            this.lblStatus.TabIndex = 7;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRefresh.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnRefresh.Location = new System.Drawing.Point(197, 226);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnCustomSoundsOpen
            // 
            this.btnCustomSoundsOpen.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnCustomSoundsOpen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCustomSoundsOpen.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCustomSoundsOpen.Location = new System.Drawing.Point(16, 197);
            this.btnCustomSoundsOpen.Name = "btnCustomSoundsOpen";
            this.btnCustomSoundsOpen.Size = new System.Drawing.Size(105, 23);
            this.btnCustomSoundsOpen.TabIndex = 9;
            this.btnCustomSoundsOpen.Text = "Custom Sounds....";
            this.btnCustomSoundsOpen.UseVisualStyleBackColor = false;
            this.btnCustomSoundsOpen.Click += new System.EventHandler(this.btnCustomSoundsOpen_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnDelete.Location = new System.Drawing.Point(197, 197);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnReinstall
            // 
            this.btnReinstall.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnReinstall.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReinstall.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnReinstall.Location = new System.Drawing.Point(197, 168);
            this.btnReinstall.Name = "btnReinstall";
            this.btnReinstall.Size = new System.Drawing.Size(75, 23);
            this.btnReinstall.TabIndex = 11;
            this.btnReinstall.Text = "Reinstall";
            this.btnReinstall.UseVisualStyleBackColor = false;
            this.btnReinstall.Click += new System.EventHandler(this.btnReinstall_Click);
            // 
            // frmFilterBro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnReinstall);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnCustomSoundsOpen);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnCheckUpdate);
            this.Controls.Add(this.txtLatestVersion);
            this.Controls.Add(this.lblLatestVer);
            this.Controls.Add(this.txtCurrentVersion);
            this.Controls.Add(this.lblCurrentVer);
            this.Controls.Add(this.cboFilterSelector);
            this.Controls.Add(this.lblFilterDropdown);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmFilterBro";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FilterBro";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFilterDropdown;
        private System.Windows.Forms.ComboBox cboFilterSelector;
        private System.Windows.Forms.Label lblCurrentVer;
        private System.Windows.Forms.TextBox txtCurrentVersion;
        private System.Windows.Forms.Label lblLatestVer;
        private System.Windows.Forms.TextBox txtLatestVersion;
        private System.Windows.Forms.Button btnCheckUpdate;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnCustomSoundsOpen;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnReinstall;
    }
}

