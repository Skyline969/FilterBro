namespace FilterBro
{
    partial class frmCustomSounds
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cboReplace = new System.Windows.Forms.ComboBox();
            this.lblReplace = new System.Windows.Forms.Label();
            this.lblWith = new System.Windows.Forms.Label();
            this.cboWith = new System.Windows.Forms.ComboBox();
            this.dgvActions = new System.Windows.Forms.DataGridView();
            this.btnPreviewReplace = new System.Windows.Forms.Button();
            this.btnWithPreview = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lblEditingFor = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActions)).BeginInit();
            this.SuspendLayout();
            // 
            // cboReplace
            // 
            this.cboReplace.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.cboReplace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReplace.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboReplace.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cboReplace.FormattingEnabled = true;
            this.cboReplace.Location = new System.Drawing.Point(65, 52);
            this.cboReplace.Name = "cboReplace";
            this.cboReplace.Size = new System.Drawing.Size(182, 21);
            this.cboReplace.TabIndex = 0;
            // 
            // lblReplace
            // 
            this.lblReplace.AutoSize = true;
            this.lblReplace.Location = new System.Drawing.Point(12, 55);
            this.lblReplace.Name = "lblReplace";
            this.lblReplace.Size = new System.Drawing.Size(47, 13);
            this.lblReplace.TabIndex = 1;
            this.lblReplace.Text = "Replace";
            // 
            // lblWith
            // 
            this.lblWith.AutoSize = true;
            this.lblWith.Location = new System.Drawing.Point(12, 86);
            this.lblWith.Name = "lblWith";
            this.lblWith.Size = new System.Drawing.Size(29, 13);
            this.lblWith.TabIndex = 2;
            this.lblWith.Text = "With";
            // 
            // cboWith
            // 
            this.cboWith.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.cboWith.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWith.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboWith.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cboWith.FormattingEnabled = true;
            this.cboWith.Location = new System.Drawing.Point(65, 83);
            this.cboWith.Name = "cboWith";
            this.cboWith.Size = new System.Drawing.Size(182, 21);
            this.cboWith.TabIndex = 3;
            // 
            // dgvActions
            // 
            this.dgvActions.AllowUserToAddRows = false;
            this.dgvActions.AllowUserToDeleteRows = false;
            this.dgvActions.AllowUserToOrderColumns = true;
            this.dgvActions.AllowUserToResizeColumns = false;
            this.dgvActions.AllowUserToResizeRows = false;
            this.dgvActions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvActions.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvActions.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvActions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvActions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvActions.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvActions.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvActions.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvActions.Location = new System.Drawing.Point(12, 114);
            this.dgvActions.MultiSelect = false;
            this.dgvActions.Name = "dgvActions";
            this.dgvActions.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvActions.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvActions.RowHeadersVisible = false;
            this.dgvActions.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvActions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvActions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvActions.ShowCellErrors = false;
            this.dgvActions.ShowCellToolTips = false;
            this.dgvActions.ShowEditingIcon = false;
            this.dgvActions.ShowRowErrors = false;
            this.dgvActions.Size = new System.Drawing.Size(360, 98);
            this.dgvActions.TabIndex = 4;
            // 
            // btnPreviewReplace
            // 
            this.btnPreviewReplace.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnPreviewReplace.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPreviewReplace.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnPreviewReplace.Location = new System.Drawing.Point(254, 52);
            this.btnPreviewReplace.Name = "btnPreviewReplace";
            this.btnPreviewReplace.Size = new System.Drawing.Size(56, 23);
            this.btnPreviewReplace.TabIndex = 5;
            this.btnPreviewReplace.Text = "Preview";
            this.btnPreviewReplace.UseVisualStyleBackColor = false;
            this.btnPreviewReplace.Click += new System.EventHandler(this.btnPreviewReplace_Click);
            // 
            // btnWithPreview
            // 
            this.btnWithPreview.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnWithPreview.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnWithPreview.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnWithPreview.Location = new System.Drawing.Point(254, 81);
            this.btnWithPreview.Name = "btnWithPreview";
            this.btnWithPreview.Size = new System.Drawing.Size(56, 23);
            this.btnWithPreview.TabIndex = 6;
            this.btnWithPreview.Text = "Preview";
            this.btnWithPreview.UseVisualStyleBackColor = false;
            this.btnWithPreview.Click += new System.EventHandler(this.btnWithPreview_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Location = new System.Drawing.Point(316, 52);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(56, 23);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnApply.Location = new System.Drawing.Point(12, 226);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 8;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(297, 226);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemove.Location = new System.Drawing.Point(316, 81);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(56, 23);
            this.btnRemove.TabIndex = 10;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // lblEditingFor
            // 
            this.lblEditingFor.Location = new System.Drawing.Point(12, 9);
            this.lblEditingFor.Name = "lblEditingFor";
            this.lblEditingFor.Size = new System.Drawing.Size(360, 23);
            this.lblEditingFor.TabIndex = 11;
            this.lblEditingFor.Text = "Editing custom sounds for ";
            this.lblEditingFor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmCustomSounds
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.lblEditingFor);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnWithPreview);
            this.Controls.Add(this.btnPreviewReplace);
            this.Controls.Add(this.dgvActions);
            this.Controls.Add(this.cboWith);
            this.Controls.Add(this.lblWith);
            this.Controls.Add(this.lblReplace);
            this.Controls.Add(this.cboReplace);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCustomSounds";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Custom Sounds";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCustomSounds_FormClosing);
            this.Load += new System.EventHandler(this.frmCustomSounds2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvActions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboReplace;
        private System.Windows.Forms.Label lblReplace;
        private System.Windows.Forms.Label lblWith;
        private System.Windows.Forms.ComboBox cboWith;
        private System.Windows.Forms.DataGridView dgvActions;
        private System.Windows.Forms.Button btnPreviewReplace;
        private System.Windows.Forms.Button btnWithPreview;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label lblEditingFor;
    }
}