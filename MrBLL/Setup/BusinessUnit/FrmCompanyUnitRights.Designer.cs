using MrDAL.Control.ControlsEx.Control;
using System;

namespace MrBLL.Setup.BusinessUnit
{
    partial class FrmCompanyUnitRights
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
            this.gb_Search = new System.Windows.Forms.GroupBox();
            this.cmb_User = new System.Windows.Forms.ComboBox();
            this.lbl_User = new System.Windows.Forms.Label();
            this.dgv_CompanyUnitRights = new System.Windows.Forms.DataGridView();
            this.IsCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.GtxtCompanyUnitId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GtxtCompanyUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.cb_SelectAll = new System.Windows.Forms.CheckBox();
            this.StorePanel = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.gb_Search.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CompanyUnitRights)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.StorePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_Search
            // 
            this.gb_Search.Controls.Add(this.cmb_User);
            this.gb_Search.Controls.Add(this.lbl_User);
            this.gb_Search.Location = new System.Drawing.Point(9, 9);
            this.gb_Search.Name = "gb_Search";
            this.gb_Search.Size = new System.Drawing.Size(441, 42);
            this.gb_Search.TabIndex = 13;
            this.gb_Search.TabStop = false;
            // 
            // cmb_User
            // 
            this.cmb_User.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_User.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmb_User.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_User.FormattingEnabled = true;
            this.cmb_User.Location = new System.Drawing.Point(99, 12);
            this.cmb_User.Name = "cmb_User";
            this.cmb_User.Size = new System.Drawing.Size(271, 25);
            this.cmb_User.TabIndex = 10;
            this.cmb_User.SelectedValueChanged += new System.EventHandler(this.cmb_User_SelectedValueChanged);
            // 
            // lbl_User
            // 
            this.lbl_User.AutoSize = true;
            this.lbl_User.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_User.Location = new System.Drawing.Point(10, 15);
            this.lbl_User.Name = "lbl_User";
            this.lbl_User.Size = new System.Drawing.Size(82, 17);
            this.lbl_User.TabIndex = 9;
            this.lbl_User.Text = "User Name";
            // 
            // dgv_CompanyUnitRights
            // 
            this.dgv_CompanyUnitRights.AllowUserToAddRows = false;
            this.dgv_CompanyUnitRights.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.dgv_CompanyUnitRights.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_CompanyUnitRights.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IsCheck,
            this.GtxtCompanyUnitId,
            this.GtxtCompanyUnit});
            this.dgv_CompanyUnitRights.Location = new System.Drawing.Point(9, 53);
            this.dgv_CompanyUnitRights.Margin = new System.Windows.Forms.Padding(2);
            this.dgv_CompanyUnitRights.Name = "dgv_CompanyUnitRights";
            this.dgv_CompanyUnitRights.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_CompanyUnitRights.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_CompanyUnitRights.RowHeadersVisible = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dgv_CompanyUnitRights.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_CompanyUnitRights.RowTemplate.Height = 24;
            this.dgv_CompanyUnitRights.Size = new System.Drawing.Size(441, 236);
            this.dgv_CompanyUnitRights.TabIndex = 12;
            // 
            // IsCheck
            // 
            this.IsCheck.FalseValue = "0";
            this.IsCheck.HeaderText = "Check";
            this.IsCheck.Name = "IsCheck";
            this.IsCheck.TrueValue = "1";
            this.IsCheck.Width = 65;
            // 
            // GtxtCompanyUnitId
            // 
            this.GtxtCompanyUnitId.HeaderText = "CompanyUnitId";
            this.GtxtCompanyUnitId.Name = "GtxtCompanyUnitId";
            this.GtxtCompanyUnitId.ReadOnly = true;
            this.GtxtCompanyUnitId.Visible = false;
            this.GtxtCompanyUnitId.Width = 5;
            // 
            // GtxtCompanyUnit
            // 
            this.GtxtCompanyUnit.HeaderText = "CompanyUnitSetup";
            this.GtxtCompanyUnit.Name = "GtxtCompanyUnit";
            this.GtxtCompanyUnit.Width = 350;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnSave);
            this.groupBox1.Controls.Add(this.BtnCancel);
            this.groupBox1.Controls.Add(this.cb_SelectAll);
            this.groupBox1.Location = new System.Drawing.Point(9, 287);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(441, 55);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(231, 15);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(93, 34);
            this.BtnSave.TabIndex = 64;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(324, 15);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(114, 34);
            this.BtnCancel.TabIndex = 65;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // cb_SelectAll
            // 
            this.cb_SelectAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cb_SelectAll.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_SelectAll.Location = new System.Drawing.Point(13, 19);
            this.cb_SelectAll.Name = "cb_SelectAll";
            this.cb_SelectAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cb_SelectAll.Size = new System.Drawing.Size(125, 22);
            this.cb_SelectAll.TabIndex = 63;
            this.cb_SelectAll.Text = "Select All";
            this.cb_SelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cb_SelectAll.UseVisualStyleBackColor = true;
            this.cb_SelectAll.CheckedChanged += new System.EventHandler(this.cb_SelectAll_CheckedChanged);
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.CausesValidation = false;
            this.StorePanel.Controls.Add(this.gb_Search);
            this.StorePanel.Controls.Add(this.dgv_CompanyUnitRights);
            this.StorePanel.Controls.Add(this.groupBox1);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(457, 347);
            this.StorePanel.TabIndex = 15;
            // 
            // FrmCompanyUnitRights
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 347);
            this.Controls.Add(this.StorePanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmCompanyUnitRights";
            this.ShowIcon = false;
            this.Text = "Company Unit Rights";
            this.Load += new System.EventHandler(this.FrmCompanyUnitRights_Load);
            this.gb_Search.ResumeLayout(false);
            this.gb_Search.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CompanyUnitRights)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.StorePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        //private void cb_SelectAll_CheckedChanged(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //private void cmb_User_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion
        private System.Windows.Forms.GroupBox gb_Search;
        private System.Windows.Forms.ComboBox cmb_User;
        private System.Windows.Forms.Label lbl_User;
        private System.Windows.Forms.DataGridView dgv_CompanyUnitRights;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cb_SelectAll;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private MrPanel StorePanel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn GtxtCompanyUnitId;
        private System.Windows.Forms.DataGridViewTextBoxColumn GtxtCompanyUnit;
    }
}