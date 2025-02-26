using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Utility.ImportUtility
{
	partial class FrmCloudDataSync
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
            this.clsSeparator3 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparatorH1 = new MrDAL.Control.ControlsEx.Control.ClsSeparatorH();
            this.ChkTransaction = new System.Windows.Forms.CheckBox();
            this.ChkCopyMaster = new System.Windows.Forms.CheckBox();
            this.ChkListEntry = new System.Windows.Forms.CheckedListBox();
            this.ChkListMaster = new System.Windows.Forms.CheckedListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.BtnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnImport = new DevExpress.XtraEditors.SimpleButton();
            this.ckbSelectAll = new System.Windows.Forms.CheckBox();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.StorePanel.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.clsSeparator3);
            this.StorePanel.Controls.Add(this.clsSeparatorH1);
            this.StorePanel.Controls.Add(this.ChkTransaction);
            this.StorePanel.Controls.Add(this.ChkCopyMaster);
            this.StorePanel.Controls.Add(this.ChkListEntry);
            this.StorePanel.Controls.Add(this.ChkListMaster);
            this.StorePanel.Controls.Add(this.groupBox4);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(701, 590);
            this.StorePanel.TabIndex = 0;
            // 
            // clsSeparator3
            // 
            this.clsSeparator3.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator3.Location = new System.Drawing.Point(9, 35);
            this.clsSeparator3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clsSeparator3.Name = "clsSeparator3";
            this.clsSeparator3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clsSeparator3.Size = new System.Drawing.Size(688, 2);
            this.clsSeparator3.TabIndex = 30;
            this.clsSeparator3.TabStop = false;
            // 
            // clsSeparatorH1
            // 
            this.clsSeparatorH1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparatorH1.Location = new System.Drawing.Point(350, 45);
            this.clsSeparatorH1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clsSeparatorH1.Name = "clsSeparatorH1";
            this.clsSeparatorH1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clsSeparatorH1.Size = new System.Drawing.Size(3, 485);
            this.clsSeparatorH1.TabIndex = 30;
            this.clsSeparatorH1.TabStop = false;
            // 
            // ChkTransaction
            // 
            this.ChkTransaction.AutoSize = true;
            this.ChkTransaction.ForeColor = System.Drawing.Color.Black;
            this.ChkTransaction.Location = new System.Drawing.Point(373, 8);
            this.ChkTransaction.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.ChkTransaction.Name = "ChkTransaction";
            this.ChkTransaction.Size = new System.Drawing.Size(136, 25);
            this.ChkTransaction.TabIndex = 1;
            this.ChkTransaction.Text = "Copy Entry";
            this.ChkTransaction.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkTransaction.UseVisualStyleBackColor = true;
            this.ChkTransaction.CheckedChanged += new System.EventHandler(this.ChkTransaction_CheckedChanged);
            // 
            // ChkCopyMaster
            // 
            this.ChkCopyMaster.AutoSize = true;
            this.ChkCopyMaster.ForeColor = System.Drawing.Color.Black;
            this.ChkCopyMaster.Location = new System.Drawing.Point(17, 9);
            this.ChkCopyMaster.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.ChkCopyMaster.Name = "ChkCopyMaster";
            this.ChkCopyMaster.Size = new System.Drawing.Size(148, 25);
            this.ChkCopyMaster.TabIndex = 0;
            this.ChkCopyMaster.Text = "Copy Master";
            this.ChkCopyMaster.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkCopyMaster.UseVisualStyleBackColor = true;
            this.ChkCopyMaster.CheckedChanged += new System.EventHandler(this.ChkCopyMaster_CheckedChanged);
            // 
            // ChkListEntry
            // 
            this.ChkListEntry.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkListEntry.FormattingEnabled = true;
            this.ChkListEntry.Items.AddRange(new object[] {
            "Opening"});
            this.ChkListEntry.Location = new System.Drawing.Point(373, 40);
            this.ChkListEntry.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.ChkListEntry.Name = "ChkListEntry";
            this.ChkListEntry.Size = new System.Drawing.Size(321, 490);
            this.ChkListEntry.TabIndex = 7;
            // 
            // ChkListMaster
            // 
            this.ChkListMaster.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkListMaster.FormattingEnabled = true;
            this.ChkListMaster.Items.AddRange(new object[] {
            "Account Group"});
            this.ChkListMaster.Location = new System.Drawing.Point(16, 40);
            this.ChkListMaster.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.ChkListMaster.Name = "ChkListMaster";
            this.ChkListMaster.Size = new System.Drawing.Size(323, 490);
            this.ChkListMaster.TabIndex = 6;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.simpleButton1);
            this.groupBox4.Controls.Add(this.BtnUpdate);
            this.groupBox4.Controls.Add(this.btn_Cancel);
            this.groupBox4.Controls.Add(this.BtnImport);
            this.groupBox4.Controls.Add(this.ckbSelectAll);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox4.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.groupBox4.Location = new System.Drawing.Point(0, 521);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.groupBox4.Size = new System.Drawing.Size(701, 69);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            // 
            // BtnUpdate
            // 
            this.BtnUpdate.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnUpdate.Appearance.ForeColor = System.Drawing.SystemColors.MenuText;
            this.BtnUpdate.Appearance.Options.UseBackColor = true;
            this.BtnUpdate.Appearance.Options.UseFont = true;
            this.BtnUpdate.Appearance.Options.UseForeColor = true;
            this.BtnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BtnUpdate.Location = new System.Drawing.Point(230, 18);
            this.BtnUpdate.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.BtnUpdate.Name = "BtnUpdate";
            this.BtnUpdate.Size = new System.Drawing.Size(112, 39);
            this.BtnUpdate.TabIndex = 0;
            this.BtnUpdate.Text = "&UPDATE";
            this.BtnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Appearance.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btn_Cancel.Appearance.Options.UseFont = true;
            this.btn_Cancel.Appearance.Options.UseForeColor = true;
            this.btn_Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_Cancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(572, 18);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(122, 39);
            this.btn_Cancel.TabIndex = 2;
            this.btn_Cancel.Text = "CANCEL";
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // BtnImport
            // 
            this.BtnImport.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnImport.Appearance.ForeColor = System.Drawing.SystemColors.MenuText;
            this.BtnImport.Appearance.Options.UseBackColor = true;
            this.BtnImport.Appearance.Options.UseFont = true;
            this.BtnImport.Appearance.Options.UseForeColor = true;
            this.BtnImport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BtnImport.Location = new System.Drawing.Point(456, 18);
            this.BtnImport.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.BtnImport.Name = "BtnImport";
            this.BtnImport.Size = new System.Drawing.Size(114, 39);
            this.BtnImport.TabIndex = 1;
            this.BtnImport.Text = "&IMPORT";
            this.BtnImport.Click += new System.EventHandler(this.BtnImport_Click);
            // 
            // ckbSelectAll
            // 
            this.ckbSelectAll.AutoSize = true;
            this.ckbSelectAll.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbSelectAll.ForeColor = System.Drawing.Color.Black;
            this.ckbSelectAll.Location = new System.Drawing.Point(5, 23);
            this.ckbSelectAll.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.ckbSelectAll.Name = "ckbSelectAll";
            this.ckbSelectAll.Size = new System.Drawing.Size(123, 27);
            this.ckbSelectAll.TabIndex = 0;
            this.ckbSelectAll.Text = "Select All";
            this.ckbSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbSelectAll.UseVisualStyleBackColor = true;
            this.ckbSelectAll.CheckedChanged += new System.EventHandler(this.ckbSelectAll_CheckedChanged);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.ForeColor = System.Drawing.SystemColors.MenuText;
            this.simpleButton1.Appearance.Options.UseBackColor = true;
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Appearance.Options.UseForeColor = true;
            this.simpleButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.simpleButton1.Location = new System.Drawing.Point(346, 18);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(7);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(107, 39);
            this.simpleButton1.TabIndex = 3;
            this.simpleButton1.Text = "&EXPORT";
            // 
            // FrmCloudDataSync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 590);
            this.Controls.Add(this.StorePanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.MaximizeBox = false;
            this.Name = "FrmCloudDataSync";
            this.ShowIcon = false;
            this.Text = "LOCAL SERVER DATA IMPORT";
            this.Load += new System.EventHandler(this.FrmCloudDataSync_Load);
            this.StorePanel.ResumeLayout(false);
            this.StorePanel.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.CheckedListBox ChkListEntry;
		private System.Windows.Forms.GroupBox groupBox4;
		private DevExpress.XtraEditors.SimpleButton btn_Cancel;
		private DevExpress.XtraEditors.SimpleButton BtnImport;
		private System.Windows.Forms.CheckBox ckbSelectAll;
		private System.Windows.Forms.CheckedListBox ChkListMaster;
		private System.Windows.Forms.CheckBox ChkTransaction;
		private System.Windows.Forms.CheckBox ChkCopyMaster;
		private ClsSeparatorH clsSeparatorH1;
		private ClsSeparator clsSeparator3;
		private DevExpress.XtraEditors.SimpleButton BtnUpdate;
        private MrPanel StorePanel;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}