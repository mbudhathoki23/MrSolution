using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Utility.Database
{
    partial class FrmAttachDeAttach
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
        [System.Obsolete]
        private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAttachDeAttach));
            this.roundPanel1 = new RoundPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dListDrive = new Microsoft.VisualBasic.Compatibility.VB6.DirListBox();
            this.FbFolderLocation = new Microsoft.VisualBasic.Compatibility.VB6.FileListBox();
            this.DriveFolder = new Microsoft.VisualBasic.Compatibility.VB6.DriveListBox();
            this.LstViewCompany = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.RBtnDeAttach = new System.Windows.Forms.RadioButton();
            this.RBtnAttach = new System.Windows.Forms.RadioButton();
            this.roundPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // roundPanel1
            // 
            this.roundPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.roundPanel1.Controls.Add(this.splitContainer1);
            this.roundPanel1.Controls.Add(this.groupBox1);
            this.roundPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roundPanel1.Location = new System.Drawing.Point(400, 0);
            this.roundPanel1.Name = "roundPanel1";
            this.roundPanel1.Radious = 25;
            this.roundPanel1.Size = new System.Drawing.Size(513, 418);
            this.roundPanel1.TabIndex = 0;
            this.roundPanel1.TabStop = false;
            this.roundPanel1.Text = "ATTACH AND DE-ATTACH";
            this.roundPanel1.TitleBackColor = System.Drawing.Color.DarkSlateBlue;
            this.roundPanel1.TitleFont = new System.Drawing.Font("Bookman Old Style", 12F);
            this.roundPanel1.TitleForeColor = System.Drawing.Color.White;
            this.roundPanel1.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(6, 35);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dListDrive);
            this.splitContainer1.Panel1.Controls.Add(this.FbFolderLocation);
            this.splitContainer1.Panel1.Controls.Add(this.DriveFolder);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.LstViewCompany);
            this.splitContainer1.Size = new System.Drawing.Size(904, 329);
            this.splitContainer1.SplitterDistance = 326;
            this.splitContainer1.TabIndex = 2;
            // 
            // dListDrive
            // 
            this.dListDrive.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.dListDrive.FormattingEnabled = true;
            this.dListDrive.IntegralHeight = false;
            this.dListDrive.Location = new System.Drawing.Point(9, 33);
            this.dListDrive.Name = "dListDrive";
            this.dListDrive.Size = new System.Drawing.Size(308, 156);
            this.dListDrive.TabIndex = 2;
            this.dListDrive.SelectedIndexChanged += new System.EventHandler(this.dListDrive_SelectedIndexChanged);
            // 
            // FbFolderLocation
            // 
            this.FbFolderLocation.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.FbFolderLocation.FormattingEnabled = true;
            this.FbFolderLocation.Location = new System.Drawing.Point(9, 195);
            this.FbFolderLocation.Name = "FbFolderLocation";
            this.FbFolderLocation.Pattern = "*.*";
            this.FbFolderLocation.Size = new System.Drawing.Size(308, 121);
            this.FbFolderLocation.TabIndex = 1;
            this.FbFolderLocation.SelectedIndexChanged += new System.EventHandler(this.FbFolderLocation_SelectedIndexChanged);
            // 
            // DriveFolder
            // 
            this.DriveFolder.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DriveFolder.FormattingEnabled = true;
            this.DriveFolder.Location = new System.Drawing.Point(9, 6);
            this.DriveFolder.Name = "DriveFolder";
            this.DriveFolder.Size = new System.Drawing.Size(308, 26);
            this.DriveFolder.TabIndex = 0;
            this.DriveFolder.SelectedIndexChanged += new System.EventHandler(this.DriveFolder_SelectedIndexChanged);
            // 
            // LstViewCompany
            // 
            this.LstViewCompany.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.LstViewCompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LstViewCompany.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstViewCompany.HideSelection = false;
            this.LstViewCompany.Location = new System.Drawing.Point(0, 0);
            this.LstViewCompany.Name = "LstViewCompany";
            this.LstViewCompany.Size = new System.Drawing.Size(574, 329);
            this.LstViewCompany.TabIndex = 0;
            this.LstViewCompany.UseCompatibleStateImageBehavior = false;
            this.LstViewCompany.View = System.Windows.Forms.View.List;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnCancel);
            this.groupBox1.Controls.Add(this.BtnSave);
            this.groupBox1.Controls.Add(this.RBtnDeAttach);
            this.groupBox1.Controls.Add(this.RBtnAttach);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(3, 370);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(507, 45);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(599, 9);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(111, 35);
            this.BtnCancel.TabIndex = 10;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnSave.Location = new System.Drawing.Point(466, 9);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(131, 35);
            this.BtnSave.TabIndex = 9;
            this.BtnSave.Text = "&ATTACH";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // RBtnDeAttach
            // 
            this.RBtnDeAttach.AutoSize = true;
            this.RBtnDeAttach.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.RBtnDeAttach.Location = new System.Drawing.Point(115, 16);
            this.RBtnDeAttach.Name = "RBtnDeAttach";
            this.RBtnDeAttach.Size = new System.Drawing.Size(126, 23);
            this.RBtnDeAttach.TabIndex = 1;
            this.RBtnDeAttach.TabStop = true;
            this.RBtnDeAttach.Text = "DE - ATTACH";
            this.RBtnDeAttach.UseVisualStyleBackColor = true;
            // 
            // RBtnAttach
            // 
            this.RBtnAttach.AutoSize = true;
            this.RBtnAttach.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.RBtnAttach.Location = new System.Drawing.Point(12, 16);
            this.RBtnAttach.Name = "RBtnAttach";
            this.RBtnAttach.Size = new System.Drawing.Size(88, 23);
            this.RBtnAttach.TabIndex = 0;
            this.RBtnAttach.TabStop = true;
            this.RBtnAttach.Text = "ATTACH";
            this.RBtnAttach.UseVisualStyleBackColor = true;
            // 
            // FrmAttachDeAttach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 418);
            this.Controls.Add(this.roundPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmAttachDeAttach";
            this.Text = "ATTACH AND DE-ATTACH DATABASE ";
            this.Load += new System.EventHandler(this.FrmAttachDeAttach_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmAttachDeAttach_KeyPress);
            this.roundPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private RoundPanel roundPanel1;
        [System.Obsolete]
        private Microsoft.VisualBasic.Compatibility.VB6.DriveListBox DriveFolder;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.SplitContainer splitContainer1;
        [System.Obsolete]
        private Microsoft.VisualBasic.Compatibility.VB6.FileListBox FbFolderLocation;
		private System.Windows.Forms.ListView LstViewCompany;
		private System.Windows.Forms.RadioButton RBtnAttach;
		private System.Windows.Forms.RadioButton RBtnDeAttach;
		private DevExpress.XtraEditors.SimpleButton BtnCancel;
		private DevExpress.XtraEditors.SimpleButton BtnSave;
        [System.Obsolete]
        private Microsoft.VisualBasic.Compatibility.VB6.DirListBox dListDrive;
    }
}