using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MrDAL.Control.ControlsEx.Control;
using ComboBox = System.Windows.Forms.ComboBox;

namespace MrBLL.Reports.Finance_Report.ListOfMaster
{
    partial class FrmLedgerList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLedgerList));
            this.BtnShow = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.mrGroup1 = new MrDAL.Control.ControlsEx.Control.MrGroup();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ChkSelectAll = new System.Windows.Forms.CheckBox();
            this.ChkIncludeMainArea = new System.Windows.Forms.CheckBox();
            this.ChkIncludeMainAgent = new System.Windows.Forms.CheckBox();
            this.ChkLedgerScheme = new System.Windows.Forms.CheckBox();
            this.ChkLedgerTerm = new System.Windows.Forms.CheckBox();
            this.ChkLedgerContactDetails = new System.Windows.Forms.CheckBox();
            this.ChkIncludeLedger = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ChkSubLedger = new System.Windows.Forms.RadioButton();
            this.ChkDepartment = new System.Windows.Forms.RadioButton();
            this.ChkAgent = new System.Windows.Forms.RadioButton();
            this.ChkArea = new System.Windows.Forms.RadioButton();
            this.ChkAccountSubGroup = new System.Windows.Forms.RadioButton();
            this.ChkAccountGroup = new System.Windows.Forms.RadioButton();
            this.ChkLedger = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.mrPanel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.mrGroup1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.mrPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnShow
            // 
            this.BtnShow.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnShow.Appearance.Options.UseFont = true;
            this.BtnShow.ImageOptions.Image = global::MrBLL.Properties.Resources.ShowReport;
            this.BtnShow.Location = new System.Drawing.Point(115, 14);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(103, 39);
            this.BtnShow.TabIndex = 0;
            this.BtnShow.Text = "&SHOW";
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(220, 14);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(116, 40);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrDAL.Control.ControlsEx.Control.MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.groupBox2);
            this.mrGroup1.Controls.Add(this.groupBox1);
            this.mrGroup1.Controls.Add(this.groupBox3);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "Ledger Reports";
            this.mrGroup1.Location = new System.Drawing.Point(0, 0);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 10;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = false;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(550, 248);
            this.mrGroup1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ChkSelectAll);
            this.groupBox2.Controls.Add(this.ChkIncludeMainArea);
            this.groupBox2.Controls.Add(this.ChkIncludeMainAgent);
            this.groupBox2.Controls.Add(this.ChkLedgerScheme);
            this.groupBox2.Controls.Add(this.ChkLedgerTerm);
            this.groupBox2.Controls.Add(this.ChkLedgerContactDetails);
            this.groupBox2.Controls.Add(this.ChkIncludeLedger);
            this.groupBox2.Location = new System.Drawing.Point(205, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(339, 176);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // ChkSelectAll
            // 
            this.ChkSelectAll.Checked = true;
            this.ChkSelectAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkSelectAll.Location = new System.Drawing.Point(7, 132);
            this.ChkSelectAll.Name = "ChkSelectAll";
            this.ChkSelectAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSelectAll.Size = new System.Drawing.Size(173, 23);
            this.ChkSelectAll.TabIndex = 6;
            this.ChkSelectAll.Text = "Select All";
            this.ChkSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSelectAll.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeMainArea
            // 
            this.ChkIncludeMainArea.Location = new System.Drawing.Point(186, 45);
            this.ChkIncludeMainArea.Name = "ChkIncludeMainArea";
            this.ChkIncludeMainArea.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeMainArea.Size = new System.Drawing.Size(147, 23);
            this.ChkIncludeMainArea.TabIndex = 3;
            this.ChkIncludeMainArea.Text = "Main Area";
            this.ChkIncludeMainArea.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeMainArea.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeMainAgent
            // 
            this.ChkIncludeMainAgent.Location = new System.Drawing.Point(186, 17);
            this.ChkIncludeMainAgent.Name = "ChkIncludeMainAgent";
            this.ChkIncludeMainAgent.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeMainAgent.Size = new System.Drawing.Size(147, 23);
            this.ChkIncludeMainAgent.TabIndex = 1;
            this.ChkIncludeMainAgent.Text = "Main Agent";
            this.ChkIncludeMainAgent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeMainAgent.UseVisualStyleBackColor = true;
            // 
            // ChkLedgerScheme
            // 
            this.ChkLedgerScheme.Location = new System.Drawing.Point(7, 103);
            this.ChkLedgerScheme.Name = "ChkLedgerScheme";
            this.ChkLedgerScheme.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkLedgerScheme.Size = new System.Drawing.Size(173, 23);
            this.ChkLedgerScheme.TabIndex = 5;
            this.ChkLedgerScheme.Text = "Ledger Scheme";
            this.ChkLedgerScheme.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkLedgerScheme.UseVisualStyleBackColor = true;
            // 
            // ChkLedgerTerm
            // 
            this.ChkLedgerTerm.Location = new System.Drawing.Point(7, 74);
            this.ChkLedgerTerm.Name = "ChkLedgerTerm";
            this.ChkLedgerTerm.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkLedgerTerm.Size = new System.Drawing.Size(173, 23);
            this.ChkLedgerTerm.TabIndex = 4;
            this.ChkLedgerTerm.Text = "Ledger Term";
            this.ChkLedgerTerm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkLedgerTerm.UseVisualStyleBackColor = true;
            // 
            // ChkLedgerContactDetails
            // 
            this.ChkLedgerContactDetails.Location = new System.Drawing.Point(7, 45);
            this.ChkLedgerContactDetails.Name = "ChkLedgerContactDetails";
            this.ChkLedgerContactDetails.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkLedgerContactDetails.Size = new System.Drawing.Size(173, 23);
            this.ChkLedgerContactDetails.TabIndex = 2;
            this.ChkLedgerContactDetails.Text = "Contact Details";
            this.ChkLedgerContactDetails.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkLedgerContactDetails.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeLedger
            // 
            this.ChkIncludeLedger.Location = new System.Drawing.Point(7, 16);
            this.ChkIncludeLedger.Name = "ChkIncludeLedger";
            this.ChkIncludeLedger.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeLedger.Size = new System.Drawing.Size(173, 23);
            this.ChkIncludeLedger.TabIndex = 0;
            this.ChkIncludeLedger.Text = "Include Ledger";
            this.ChkIncludeLedger.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeLedger.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ChkSubLedger);
            this.groupBox1.Controls.Add(this.ChkDepartment);
            this.groupBox1.Controls.Add(this.ChkAgent);
            this.groupBox1.Controls.Add(this.ChkArea);
            this.groupBox1.Controls.Add(this.ChkAccountSubGroup);
            this.groupBox1.Controls.Add(this.ChkAccountGroup);
            this.groupBox1.Controls.Add(this.ChkLedger);
            this.groupBox1.Location = new System.Drawing.Point(12, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(190, 228);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // ChkSubLedger
            // 
            this.ChkSubLedger.AutoSize = true;
            this.ChkSubLedger.Location = new System.Drawing.Point(6, 195);
            this.ChkSubLedger.Name = "ChkSubLedger";
            this.ChkSubLedger.Size = new System.Drawing.Size(111, 23);
            this.ChkSubLedger.TabIndex = 6;
            this.ChkSubLedger.Text = "Sub Ledger";
            this.ChkSubLedger.UseVisualStyleBackColor = true;
            this.ChkSubLedger.CheckedChanged += new System.EventHandler(this.ChkLedger_CheckedChanged);
            // 
            // ChkDepartment
            // 
            this.ChkDepartment.AutoSize = true;
            this.ChkDepartment.Location = new System.Drawing.Point(6, 166);
            this.ChkDepartment.Name = "ChkDepartment";
            this.ChkDepartment.Size = new System.Drawing.Size(117, 23);
            this.ChkDepartment.TabIndex = 5;
            this.ChkDepartment.Text = "Department";
            this.ChkDepartment.UseVisualStyleBackColor = true;
            this.ChkDepartment.CheckedChanged += new System.EventHandler(this.ChkDepartment_CheckedChanged);
            // 
            // ChkAgent
            // 
            this.ChkAgent.AutoSize = true;
            this.ChkAgent.Location = new System.Drawing.Point(6, 134);
            this.ChkAgent.Name = "ChkAgent";
            this.ChkAgent.Size = new System.Drawing.Size(70, 23);
            this.ChkAgent.TabIndex = 4;
            this.ChkAgent.Text = "Agent";
            this.ChkAgent.UseVisualStyleBackColor = true;
            this.ChkAgent.CheckedChanged += new System.EventHandler(this.ChkLedger_CheckedChanged);
            // 
            // ChkArea
            // 
            this.ChkArea.AutoSize = true;
            this.ChkArea.Location = new System.Drawing.Point(6, 103);
            this.ChkArea.Name = "ChkArea";
            this.ChkArea.Size = new System.Drawing.Size(62, 23);
            this.ChkArea.TabIndex = 3;
            this.ChkArea.Text = "Area";
            this.ChkArea.UseVisualStyleBackColor = true;
            this.ChkArea.CheckedChanged += new System.EventHandler(this.ChkLedger_CheckedChanged);
            // 
            // ChkAccountSubGroup
            // 
            this.ChkAccountSubGroup.AutoSize = true;
            this.ChkAccountSubGroup.Location = new System.Drawing.Point(6, 74);
            this.ChkAccountSubGroup.Name = "ChkAccountSubGroup";
            this.ChkAccountSubGroup.Size = new System.Drawing.Size(170, 23);
            this.ChkAccountSubGroup.TabIndex = 2;
            this.ChkAccountSubGroup.Text = "Account Sub Group";
            this.ChkAccountSubGroup.UseVisualStyleBackColor = true;
            this.ChkAccountSubGroup.CheckedChanged += new System.EventHandler(this.ChkLedger_CheckedChanged);
            // 
            // ChkAccountGroup
            // 
            this.ChkAccountGroup.AutoSize = true;
            this.ChkAccountGroup.Location = new System.Drawing.Point(6, 45);
            this.ChkAccountGroup.Name = "ChkAccountGroup";
            this.ChkAccountGroup.Size = new System.Drawing.Size(137, 23);
            this.ChkAccountGroup.TabIndex = 1;
            this.ChkAccountGroup.Text = "Account Group";
            this.ChkAccountGroup.UseVisualStyleBackColor = true;
            this.ChkAccountGroup.CheckedChanged += new System.EventHandler(this.ChkLedger_CheckedChanged);
            // 
            // ChkLedger
            // 
            this.ChkLedger.AutoSize = true;
            this.ChkLedger.Checked = true;
            this.ChkLedger.Location = new System.Drawing.Point(6, 16);
            this.ChkLedger.Name = "ChkLedger";
            this.ChkLedger.Size = new System.Drawing.Size(78, 23);
            this.ChkLedger.TabIndex = 0;
            this.ChkLedger.TabStop = true;
            this.ChkLedger.Text = "Ledger";
            this.ChkLedger.UseVisualStyleBackColor = true;
            this.ChkLedger.CheckedChanged += new System.EventHandler(this.ChkLedger_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.BtnShow);
            this.groupBox3.Controls.Add(this.BtnCancel);
            this.groupBox3.Location = new System.Drawing.Point(205, 186);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(339, 59);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // mrPanel1
            // 
            this.mrPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel1.Controls.Add(this.mrGroup1);
            this.mrPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrPanel1.Location = new System.Drawing.Point(0, 0);
            this.mrPanel1.Name = "mrPanel1";
            this.mrPanel1.Size = new System.Drawing.Size(550, 248);
            this.mrPanel1.TabIndex = 33;
            // 
            // FrmLedgerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(550, 248);
            this.Controls.Add(this.mrPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmLedgerList";
            this.ShowIcon = false;
            this.Text = "Ledger List";
            this.Load += new System.EventHandler(this.FrmLedgerList_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmLedgerList_KeyPress);
            this.mrGroup1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.mrPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private SimpleButton BtnShow;
        private SimpleButton BtnCancel;
        private MrGroup mrGroup1;
        private MrPanel mrPanel1;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private GroupBox groupBox3;
        private RadioButton ChkSubLedger;
        private RadioButton ChkDepartment;
        private RadioButton ChkAgent;
        private RadioButton ChkArea;
        private RadioButton ChkAccountSubGroup;
        private RadioButton ChkAccountGroup;
        private RadioButton ChkLedger;
        private CheckBox ChkIncludeLedger;
        private CheckBox ChkLedgerTerm;
        private CheckBox ChkLedgerContactDetails;
        private CheckBox ChkLedgerScheme;
        private CheckBox ChkIncludeMainAgent;
        private CheckBox ChkIncludeMainArea;
        private CheckBox ChkSelectAll;
    }
}