using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Master.Import
{
	partial class FrmCopyMaster
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
            this.StorePanel = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.BtnCompany = new System.Windows.Forms.Button();
            this.TxtCompany = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtDatabase = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.ChkListMaster = new System.Windows.Forms.CheckedListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnImport = new DevExpress.XtraEditors.SimpleButton();
            this.ckbSelectAll = new System.Windows.Forms.CheckBox();
            this.StorePanel.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.BtnCompany);
            this.StorePanel.Controls.Add(this.TxtCompany);
            this.StorePanel.Controls.Add(this.label1);
            this.StorePanel.Controls.Add(this.label2);
            this.StorePanel.Controls.Add(this.TxtDatabase);
            this.StorePanel.Controls.Add(this.ChkListMaster);
            this.StorePanel.Controls.Add(this.groupBox4);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(645, 623);
            this.StorePanel.TabIndex = 0;
            // 
            // BtnCompany
            // 
            this.BtnCompany.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnCompany.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnCompany.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnCompany.Location = new System.Drawing.Point(614, 42);
            this.BtnCompany.Name = "BtnCompany";
            this.BtnCompany.Size = new System.Drawing.Size(29, 27);
            this.BtnCompany.TabIndex = 314;
            this.BtnCompany.TabStop = false;
            this.BtnCompany.UseVisualStyleBackColor = false;
            this.BtnCompany.Click += new System.EventHandler(this.BtnCompany_Click);
            // 
            // TxtCompany
            // 
            this.TxtCompany.BackColor = System.Drawing.SystemColors.Window;
            this.TxtCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCompany.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.TxtCompany.Location = new System.Drawing.Point(142, 43);
            this.TxtCompany.Name = "TxtCompany";
            this.TxtCompany.ReadOnly = true;
            this.TxtCompany.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtCompany.Size = new System.Drawing.Size(472, 26);
            this.TxtCompany.TabIndex = 31;
            this.TxtCompany.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCompany_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.TabIndex = 23;
            this.label1.Text = "Database";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label2.Location = new System.Drawing.Point(9, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 20);
            this.label2.TabIndex = 25;
            this.label2.Text = "Company Desc";
            // 
            // TxtDatabase
            // 
            this.TxtDatabase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDatabase.Enabled = false;
            this.TxtDatabase.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.TxtDatabase.Location = new System.Drawing.Point(142, 12);
            this.TxtDatabase.Name = "TxtDatabase";
            this.TxtDatabase.ReadOnly = true;
            this.TxtDatabase.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtDatabase.Size = new System.Drawing.Size(173, 26);
            this.TxtDatabase.TabIndex = 4;
            this.TxtDatabase.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDatabase_KeyDown);
            // 
            // ChkListMaster
            // 
            this.ChkListMaster.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ChkListMaster.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkListMaster.FormattingEnabled = true;
            this.ChkListMaster.Items.AddRange(new object[] {
            "Account Group"});
            this.ChkListMaster.Location = new System.Drawing.Point(0, 79);
            this.ChkListMaster.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ChkListMaster.Name = "ChkListMaster";
            this.ChkListMaster.Size = new System.Drawing.Size(645, 488);
            this.ChkListMaster.TabIndex = 6;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btn_Cancel);
            this.groupBox4.Controls.Add(this.BtnImport);
            this.groupBox4.Controls.Add(this.ckbSelectAll);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox4.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.groupBox4.Location = new System.Drawing.Point(0, 567);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox4.Size = new System.Drawing.Size(645, 56);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Appearance.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btn_Cancel.Appearance.Options.UseFont = true;
            this.btn_Cancel.Appearance.Options.UseForeColor = true;
            this.btn_Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_Cancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(395, 15);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(115, 34);
            this.btn_Cancel.TabIndex = 2;
            this.btn_Cancel.Text = "CANCEL";
            this.btn_Cancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnImport
            // 
            this.BtnImport.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnImport.Appearance.ForeColor = System.Drawing.SystemColors.MenuText;
            this.BtnImport.Appearance.Options.UseBackColor = true;
            this.BtnImport.Appearance.Options.UseFont = true;
            this.BtnImport.Appearance.Options.UseForeColor = true;
            this.BtnImport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BtnImport.ImageOptions.Image = global::MrBLL.Properties.Resources.Export;
            this.BtnImport.Location = new System.Drawing.Point(224, 15);
            this.BtnImport.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.BtnImport.Name = "BtnImport";
            this.BtnImport.Size = new System.Drawing.Size(170, 35);
            this.BtnImport.TabIndex = 1;
            this.BtnImport.Text = "&COPY MASTER";
            this.BtnImport.Click += new System.EventHandler(this.BtnImport_Click);
            // 
            // ckbSelectAll
            // 
            this.ckbSelectAll.AutoSize = true;
            this.ckbSelectAll.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbSelectAll.ForeColor = System.Drawing.Color.Black;
            this.ckbSelectAll.Location = new System.Drawing.Point(4, 19);
            this.ckbSelectAll.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ckbSelectAll.Name = "ckbSelectAll";
            this.ckbSelectAll.Size = new System.Drawing.Size(102, 24);
            this.ckbSelectAll.TabIndex = 0;
            this.ckbSelectAll.Text = "Select All";
            this.ckbSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbSelectAll.UseVisualStyleBackColor = true;
            this.ckbSelectAll.CheckedChanged += new System.EventHandler(this.CkbSelectAll_CheckedChanged);
            // 
            // FrmCopyMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 623);
            this.Controls.Add(this.StorePanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmCopyMaster";
            this.ShowIcon = false;
            this.Text = "COPY MASTER LIST";
            this.Load += new System.EventHandler(this.FrmImportData_Load);
            this.StorePanel.ResumeLayout(false);
            this.StorePanel.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.GroupBox groupBox4;
		private DevExpress.XtraEditors.SimpleButton btn_Cancel;
		private DevExpress.XtraEditors.SimpleButton BtnImport;
		private System.Windows.Forms.CheckBox ckbSelectAll;
		private System.Windows.Forms.CheckedListBox ChkListMaster;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button BtnCompany;
        private MrPanel StorePanel;
        private MrTextBox TxtDatabase;
        private MrTextBox TxtCompany;
    }
}