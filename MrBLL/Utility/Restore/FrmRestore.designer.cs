using System.Windows.Forms;
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Utility.Restore
{
    partial class FrmRestore
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRestore));
            this.txtBakFile = new MrTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblServer = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbDatabase = new System.Windows.Forms.ComboBox();
            this.openBakFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.lnkConnect = new System.Windows.Forms.LinkLabel();
            this.lblRestoring = new System.Windows.Forms.Label();
            this.gbSource = new System.Windows.Forms.GroupBox();
            this.btnBrowseFile = new System.Windows.Forms.Button();
            this.gbTarget = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panel1 = new MrPanel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.lnkhistory = new System.Windows.Forms.LinkLabel();
            this.BtnRestore = new DevExpress.XtraEditors.SimpleButton();
            this.mrGroup1 = new MrGroup();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.gbSource.SuspendLayout();
            this.gbTarget.SuspendLayout();
            this.panel1.SuspendLayout();
            this.mrGroup1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtBakFile
            // 
            this.txtBakFile.AcceptsReturn = true;
            this.txtBakFile.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtBakFile.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.txtBakFile.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.txtBakFile.Location = new System.Drawing.Point(123, 22);
            this.txtBakFile.Name = "txtBakFile";
            this.txtBakFile.Size = new System.Drawing.Size(407, 25);
            this.txtBakFile.TabIndex = 1;
            this.txtBakFile.TextChanged += new System.EventHandler(this.TxtBakFileTextChanged);
            this.txtBakFile.Leave += new System.EventHandler(this.TxtBakFileLeave);
            this.txtBakFile.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.TxtBakFilePreviewKeyDown);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(6, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 22);
            this.label2.TabIndex = 0;
            this.label2.Text = "Backup file (bak, zip):";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(9, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "SQL GetServer:";
            // 
            // lblServer
            // 
            this.lblServer.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblServer.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.lblServer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblServer.Location = new System.Drawing.Point(184, 16);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(311, 32);
            this.lblServer.TabIndex = 1;
            this.lblServer.Text = ".\\SQLEXPRESS";
            this.lblServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(9, 59);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 19);
            this.label4.TabIndex = 3;
            this.label4.Text = "Target Database:";
            // 
            // cbDatabase
            // 
            this.cbDatabase.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbDatabase.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDatabase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbDatabase.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.cbDatabase.Location = new System.Drawing.Point(184, 55);
            this.cbDatabase.Name = "cbDatabase";
            this.cbDatabase.Size = new System.Drawing.Size(311, 27);
            this.cbDatabase.TabIndex = 0;
            this.cbDatabase.SelectedIndexChanged += new System.EventHandler(this.CbDatabaseSelectedIndexChanged);
            this.cbDatabase.TextUpdate += new System.EventHandler(this.CbDatabaseTextUpdate);
            this.cbDatabase.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.CbDatabasePreviewKeyDown);
            // 
            // openBakFileDialog
            // 
            this.openBakFileDialog.Filter = "BAK or ZIP files|*.bak;*.zip|All files|*.*";
            this.openBakFileDialog.Title = "Source Backup File";
            // 
            // lnkConnect
            // 
            this.lnkConnect.AutoSize = true;
            this.lnkConnect.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.lnkConnect.Location = new System.Drawing.Point(501, 22);
            this.lnkConnect.Name = "lnkConnect";
            this.lnkConnect.Size = new System.Drawing.Size(66, 19);
            this.lnkConnect.TabIndex = 2;
            this.lnkConnect.TabStop = true;
            this.lnkConnect.Text = "Change";
            this.lnkConnect.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkConnectLinkClicked);
            // 
            // lblRestoring
            // 
            this.lblRestoring.AutoSize = true;
            this.lblRestoring.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.lblRestoring.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRestoring.Location = new System.Drawing.Point(4, 271);
            this.lblRestoring.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblRestoring.Name = "lblRestoring";
            this.lblRestoring.Size = new System.Drawing.Size(96, 19);
            this.lblRestoring.TabIndex = 4;
            this.lblRestoring.Text = "Restoring...";
            this.lblRestoring.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblRestoring.Visible = false;
            // 
            // gbSource
            // 
            this.gbSource.Controls.Add(this.lnkhistory);
            this.gbSource.Controls.Add(this.label2);
            this.gbSource.Controls.Add(this.txtBakFile);
            this.gbSource.Controls.Add(this.btnBrowseFile);
            this.gbSource.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbSource.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gbSource.Location = new System.Drawing.Point(4, 118);
            this.gbSource.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.gbSource.Name = "gbSource";
            this.gbSource.Size = new System.Drawing.Size(573, 57);
            this.gbSource.TabIndex = 0;
            this.gbSource.TabStop = false;
            this.gbSource.Text = "Source (From)";
            // 
            // btnBrowseFile
            // 
            this.btnBrowseFile.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnBrowseFile.Image = global::MrBLL.Properties.Resources.search16;
            this.btnBrowseFile.Location = new System.Drawing.Point(536, 22);
            this.btnBrowseFile.Name = "btnBrowseFile";
            this.btnBrowseFile.Size = new System.Drawing.Size(31, 25);
            this.btnBrowseFile.TabIndex = 2;
            this.btnBrowseFile.UseVisualStyleBackColor = true;
            this.btnBrowseFile.Click += new System.EventHandler(this.BtnBrowseFileClick);
            // 
            // gbTarget
            // 
            this.gbTarget.Controls.Add(this.label1);
            this.gbTarget.Controls.Add(this.lblServer);
            this.gbTarget.Controls.Add(this.lnkConnect);
            this.gbTarget.Controls.Add(this.label4);
            this.gbTarget.Controls.Add(this.cbDatabase);
            this.gbTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbTarget.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gbTarget.Location = new System.Drawing.Point(4, 176);
            this.gbTarget.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.gbTarget.Name = "gbTarget";
            this.gbTarget.Size = new System.Drawing.Size(573, 89);
            this.gbTarget.TabIndex = 1;
            this.gbTarget.TabStop = false;
            this.gbTarget.Text = "Target (To)";
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.lblStatus.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblStatus.Location = new System.Drawing.Point(10, 270);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(421, 35);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Done";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatus.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(4, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(573, 87);
            this.panel1.TabIndex = 0;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.linkLabel1.Location = new System.Drawing.Point(344, 11);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(189, 19);
            this.linkLabel1.TabIndex = 1;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "www.mrsolution.com.np";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1LinkClicked);
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(573, 87);
            this.label3.TabIndex = 0;
            this.label3.Text = "If you want to get computer billing approval, please consult with  your vendor \r\n" +
    "once before using this system Or user party will have to pay  penalty according " +
    "to government law.";
            // 
            // lnkhistory
            // 
            this.lnkhistory.AutoSize = true;
            this.lnkhistory.BackColor = System.Drawing.Color.Transparent;
            this.lnkhistory.Font = new System.Drawing.Font("Bookman Old Style", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkhistory.Location = new System.Drawing.Point(445, 3);
            this.lnkhistory.Name = "lnkhistory";
            this.lnkhistory.Size = new System.Drawing.Size(128, 16);
            this.lnkhistory.TabIndex = 3;
            this.lnkhistory.TabStop = true;
            this.lnkhistory.Text = "RESTORE HISTORY";
            this.lnkhistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkHistory_LinkClicked);
            // 
            // BtnRestore
            // 
            this.BtnRestore.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRestore.Appearance.Options.UseFont = true;
            this.BtnRestore.Location = new System.Drawing.Point(437, 267);
            this.BtnRestore.Name = "BtnRestore";
            this.BtnRestore.Size = new System.Drawing.Size(136, 38);
            this.BtnRestore.TabIndex = 2;
            this.BtnRestore.Text = "&RESTORE";
            this.BtnRestore.Click += new System.EventHandler(this.BtnRestoreClick);
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.BtnRestore);
            this.mrGroup1.Controls.Add(this.progressBar);
            this.mrGroup1.Controls.Add(this.linkLabel1);
            this.mrGroup1.Controls.Add(this.panel1);
            this.mrGroup1.Controls.Add(this.gbSource);
            this.mrGroup1.Controls.Add(this.lblRestoring);
            this.mrGroup1.Controls.Add(this.gbTarget);
            this.mrGroup1.Controls.Add(this.lblStatus);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrGroup1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "RESTORE";
            this.mrGroup1.Location = new System.Drawing.Point(0, 0);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 10;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = false;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(579, 333);
            this.mrGroup1.TabIndex = 1;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(7, 307);
            this.progressBar.MarqueeAnimationSpeed = 50;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(562, 20);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 6;
            this.progressBar.Visible = false;
            // 
            // FrmRestore
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(579, 333);
            this.Controls.Add(this.mrGroup1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmRestore";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DATABASE RESTORE TOOLS";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmRestore_Closed);
            this.Load += new System.EventHandler(this.FrmRestore_Load);
            this.Shown += new System.EventHandler(this.Form1Shown);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FrmRestoreDragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FrmRestoreDragEnter);
            this.gbSource.ResumeLayout(false);
            this.gbSource.PerformLayout();
            this.gbTarget.ResumeLayout(false);
            this.gbTarget.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		

	

		//private IContainer components;

		private TextBox txtBakFile;

		private Label label2;

		private Label label1;

		private Label lblServer;

		private Label label4;

		private ComboBox cbDatabase;

		private Button btnBrowseFile;

		private OpenFileDialog openBakFileDialog;

		private LinkLabel lnkConnect;

		private Label lblRestoring;

		private GroupBox gbSource;

		private GroupBox gbTarget;

		private Label lblStatus;

		private Panel panel1;
		private DevExpress.XtraEditors.SimpleButton BtnRestore;
		private LinkLabel linkLabel1;
		private Label label3;
        private LinkLabel lnkhistory;
        private MrGroup mrGroup1;
        private ProgressBar progressBar;
    }
}