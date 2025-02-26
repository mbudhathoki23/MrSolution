using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Setup.BranchSetup
{
    partial class FrmBranchRights
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBranchRights));
            this.gb_Search = new System.Windows.Forms.GroupBox();
            this.cmb_User = new System.Windows.Forms.ComboBox();
            this.lbl_User = new System.Windows.Forms.Label();
            this.SGrid = new System.Windows.Forms.DataGridView();
            this.IsCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.GTxtBranchId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtBranch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.ChkSelectAll = new System.Windows.Forms.CheckBox();
            this.StorePanel = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gb_Search.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SGrid)).BeginInit();
            this.StorePanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_Search
            // 
            this.gb_Search.Controls.Add(this.cmb_User);
            this.gb_Search.Controls.Add(this.lbl_User);
            this.gb_Search.Location = new System.Drawing.Point(3, -4);
            this.gb_Search.Name = "gb_Search";
            this.gb_Search.Size = new System.Drawing.Size(513, 42);
            this.gb_Search.TabIndex = 12;
            this.gb_Search.TabStop = false;
            // 
            // cmb_User
            // 
            this.cmb_User.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_User.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmb_User.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.cmb_User.FormattingEnabled = true;
            this.cmb_User.Location = new System.Drawing.Point(109, 12);
            this.cmb_User.Name = "cmb_User";
            this.cmb_User.Size = new System.Drawing.Size(385, 27);
            this.cmb_User.TabIndex = 10;
            this.cmb_User.SelectedValueChanged += new System.EventHandler(this.CmbUser_SelectedValueChanged);
            // 
            // lbl_User
            // 
            this.lbl_User.AutoSize = true;
            this.lbl_User.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_User.Location = new System.Drawing.Point(10, 15);
            this.lbl_User.Name = "lbl_User";
            this.lbl_User.Size = new System.Drawing.Size(93, 19);
            this.lbl_User.TabIndex = 9;
            this.lbl_User.Text = "User Name";
            // 
            // SGrid
            // 
            this.SGrid.AllowUserToAddRows = false;
            this.SGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.SGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IsCheck,
            this.GTxtBranchId,
            this.GTxtBranch});
            this.SGrid.Location = new System.Drawing.Point(2, 41);
            this.SGrid.Margin = new System.Windows.Forms.Padding(2);
            this.SGrid.Name = "SGrid";
            this.SGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.SGrid.RowHeadersVisible = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.SGrid.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.SGrid.RowTemplate.Height = 24;
            this.SGrid.Size = new System.Drawing.Size(514, 205);
            this.SGrid.TabIndex = 11;
            // 
            // IsCheck
            // 
            this.IsCheck.Frozen = true;
            this.IsCheck.HeaderText = "CHECK";
            this.IsCheck.Name = "IsCheck";
            this.IsCheck.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsCheck.Width = 75;
            // 
            // GTxtBranchId
            // 
            this.GTxtBranchId.HeaderText = "BranchId";
            this.GTxtBranchId.Name = "GTxtBranchId";
            this.GTxtBranchId.ReadOnly = true;
            this.GTxtBranchId.Visible = false;
            this.GTxtBranchId.Width = 5;
            // 
            // GTxtBranch
            // 
            this.GTxtBranch.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.GTxtBranch.HeaderText = "BRANCH";
            this.GTxtBranch.Name = "GTxtBranch";
            this.GTxtBranch.ReadOnly = true;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(267, 13);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(114, 34);
            this.BtnCancel.TabIndex = 14;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(172, 13);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(93, 34);
            this.BtnSave.TabIndex = 13;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // ChkSelectAll
            // 
            this.ChkSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ChkSelectAll.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkSelectAll.Location = new System.Drawing.Point(10, 19);
            this.ChkSelectAll.Name = "ChkSelectAll";
            this.ChkSelectAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSelectAll.Size = new System.Drawing.Size(125, 22);
            this.ChkSelectAll.TabIndex = 64;
            this.ChkSelectAll.Text = "Select All";
            this.ChkSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSelectAll.UseVisualStyleBackColor = true;
            this.ChkSelectAll.CheckedChanged += new System.EventHandler(this.ChkSelectAll_CheckedChanged);
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.gb_Search);
            this.StorePanel.Controls.Add(this.SGrid);
            this.StorePanel.Controls.Add(this.groupBox1);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(528, 299);
            this.StorePanel.TabIndex = 65;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnSave);
            this.groupBox1.Controls.Add(this.BtnCancel);
            this.groupBox1.Controls.Add(this.ChkSelectAll);
            this.groupBox1.Location = new System.Drawing.Point(3, 243);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(425, 50);
            this.groupBox1.TabIndex = 65;
            this.groupBox1.TabStop = false;
            // 
            // FrmBranchRights
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 299);
            this.Controls.Add(this.StorePanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmBranchRights";
            this.ShowIcon = false;
            this.Text = "Branch Rights";
            this.Load += new System.EventHandler(this.FrmBranchRights_Load);
            this.gb_Search.ResumeLayout(false);
            this.gb_Search.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SGrid)).EndInit();
            this.StorePanel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_Search;
        private System.Windows.Forms.ComboBox cmb_User;
        private System.Windows.Forms.Label lbl_User;
        private System.Windows.Forms.DataGridView SGrid;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private System.Windows.Forms.CheckBox ChkSelectAll;
        private System.Windows.Forms.GroupBox groupBox1;
        private MrPanel StorePanel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtBranchId;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtBranch;
    }
}