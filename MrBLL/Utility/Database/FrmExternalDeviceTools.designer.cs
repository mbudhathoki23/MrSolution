
using System.Windows.Forms;
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Utility.Database
{
    partial class FrmExternalDeviceTools
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
            this.openBakFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.mrPanel1 = new MrPanel();
            this.mrGroup1 = new MrGroup();
            this.BtnBrowser = new DevExpress.XtraEditors.SimpleButton();
            this.BtnAttach = new DevExpress.XtraEditors.SimpleButton();
            this.TxtLocation = new MrTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.mrGroup2 = new MrGroup();
            this.RGrid = new EntryGridViewEx();
            this.BtnDeAttach = new DevExpress.XtraEditors.SimpleButton();
            this.Inital = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CompanyInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mrPanel1.SuspendLayout();
            this.mrGroup1.SuspendLayout();
            this.mrGroup2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // openBakFileDialog
            // 
            this.openBakFileDialog.Filter = "MDF files|*.MDF;*.mdf*.*";
            this.openBakFileDialog.Title = "Source Backup File";
            // 
            // mrPanel1
            // 
            this.mrPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel1.Controls.Add(this.mrGroup1);
            this.mrPanel1.Controls.Add(this.mrGroup2);
            this.mrPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrPanel1.Location = new System.Drawing.Point(0, 0);
            this.mrPanel1.Name = "mrPanel1";
            this.mrPanel1.Size = new System.Drawing.Size(739, 381);
            this.mrPanel1.TabIndex = 0;
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.BtnBrowser);
            this.mrGroup1.Controls.Add(this.BtnAttach);
            this.mrGroup1.Controls.Add(this.TxtLocation);
            this.mrGroup1.Controls.Add(this.label4);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "";
            this.mrGroup1.Location = new System.Drawing.Point(3, -7);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 5;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = false;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(733, 108);
            this.mrGroup1.TabIndex = 0;
            // 
            // BtnBrowser
            // 
            this.BtnBrowser.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBrowser.Appearance.Options.UseFont = true;
            this.BtnBrowser.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.BtnBrowser.ImageOptions.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnBrowser.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.BtnBrowser.Location = new System.Drawing.Point(695, 38);
            this.BtnBrowser.Name = "BtnBrowser";
            this.BtnBrowser.Size = new System.Drawing.Size(28, 25);
            this.BtnBrowser.TabIndex = 24;
            this.BtnBrowser.Click += new System.EventHandler(this.BtnBrowser_Click);
            // 
            // BtnAttach
            // 
            this.BtnAttach.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAttach.Appearance.Options.UseFont = true;
            this.BtnAttach.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnAttach.Location = new System.Drawing.Point(7, 65);
            this.BtnAttach.Name = "BtnAttach";
            this.BtnAttach.Size = new System.Drawing.Size(133, 36);
            this.BtnAttach.TabIndex = 1;
            this.BtnAttach.Text = "&ATTACH";
            this.BtnAttach.Click += new System.EventHandler(this.BtnAttach_Click);
            // 
            // TxtLocation
            // 
            this.TxtLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtLocation.Location = new System.Drawing.Point(6, 39);
            this.TxtLocation.Name = "TxtLocation";
            this.TxtLocation.Size = new System.Drawing.Size(686, 23);
            this.TxtLocation.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 20);
            this.label4.TabIndex = 23;
            this.label4.Text = "Destination";
            // 
            // mrGroup2
            // 
            this.mrGroup2.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup2.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup2.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup2.BorderColor = System.Drawing.Color.White;
            this.mrGroup2.BorderThickness = 1F;
            this.mrGroup2.Controls.Add(this.RGrid);
            this.mrGroup2.Controls.Add(this.BtnDeAttach);
            this.mrGroup2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup2.GroupImage = null;
            this.mrGroup2.GroupTitle = "";
            this.mrGroup2.Location = new System.Drawing.Point(3, 94);
            this.mrGroup2.Name = "mrGroup2";
            this.mrGroup2.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup2.PaintGroupBox = false;
            this.mrGroup2.RoundCorners = 5;
            this.mrGroup2.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup2.ShadowControl = false;
            this.mrGroup2.ShadowThickness = 3;
            this.mrGroup2.Size = new System.Drawing.Size(733, 282);
            this.mrGroup2.TabIndex = 1;
            // 
            // RGrid
            // 
            this.RGrid.AllowUserToAddRows = false;
            this.RGrid.AllowUserToDeleteRows = false;
            this.RGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.RGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Inital,
            this.CompanyInfo});
            this.RGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.RGrid.Location = new System.Drawing.Point(8, 19);
            this.RGrid.Name = "RGrid";
            this.RGrid.ReadOnly = true;
            this.RGrid.Size = new System.Drawing.Size(717, 219);
            this.RGrid.TabIndex = 0;
            // 
            // BtnDeAttach
            // 
            this.BtnDeAttach.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDeAttach.Appearance.Options.UseFont = true;
            this.BtnDeAttach.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDeAttach.Location = new System.Drawing.Point(7, 241);
            this.BtnDeAttach.Name = "BtnDeAttach";
            this.BtnDeAttach.Size = new System.Drawing.Size(133, 35);
            this.BtnDeAttach.TabIndex = 27;
            this.BtnDeAttach.Text = "&DE-ATTACH";
            this.BtnDeAttach.Click += new System.EventHandler(this.BtnDeAttach_Click);
            // 
            // Inital
            // 
            this.Inital.DataPropertyName = "Initial";
            this.Inital.HeaderText = "Initial";
            this.Inital.Name = "Inital";
            this.Inital.ReadOnly = true;
            this.Inital.Visible = false;
            // 
            // CompanyInfo
            // 
            this.CompanyInfo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CompanyInfo.DataPropertyName = "CompanyInfo";
            this.CompanyInfo.HeaderText = "Company Info";
            this.CompanyInfo.Name = "CompanyInfo";
            this.CompanyInfo.ReadOnly = true;
            // 
            // FrmExternalDeviceTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 381);
            this.Controls.Add(this.mrPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmExternalDeviceTools";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "External Device Tools";
            this.Load += new System.EventHandler(this.FrmExternalDeviceTools_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmExternalDeviceTools_KeyPress);
            this.mrPanel1.ResumeLayout(false);
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            this.mrGroup2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MrPanel mrPanel1;
        private MrGroup mrGroup1;
        private MrGroup mrGroup2;
        private DevExpress.XtraEditors.SimpleButton BtnBrowser;
        private DevExpress.XtraEditors.SimpleButton BtnAttach;
        private MrTextBox TxtLocation;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.SimpleButton BtnDeAttach;
        private DataGridView RGrid;
        private System.Windows.Forms.OpenFileDialog openBakFileDialog;
        private System.Windows.Forms.DataGridViewTextBoxColumn Inital;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompanyInfo;
    }
}