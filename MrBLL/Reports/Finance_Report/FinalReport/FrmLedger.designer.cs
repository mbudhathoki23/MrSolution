using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MrDAL.Control.ControlsEx.Control;
using ComboBox = System.Windows.Forms.ComboBox;

namespace MrBLL.Reports.Finance_Report.FinalReport
{
    partial class FrmLedger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLedger));
            this.CmbDateType = new System.Windows.Forms.ComboBox();
            this.MskToDate = new MrMaskedTextBox();
            this.MskFrom = new MrMaskedTextBox();
            this.ChkLedgerPanVat = new System.Windows.Forms.CheckBox();
            this.ChkIncludePDC = new System.Windows.Forms.CheckBox();
            this.ChkDNCNDetails = new System.Windows.Forms.CheckBox();
            this.ChkPostingDetails = new System.Windows.Forms.CheckBox();
            this.ChkIncludeUDF = new System.Windows.Forms.CheckBox();
            this.ChkIncludeAdjustment = new System.Windows.Forms.CheckBox();
            this.ChkProductDetails = new System.Windows.Forms.CheckBox();
            this.ChkAllSubledger = new System.Windows.Forms.CheckBox();
            this.ChkDate = new System.Windows.Forms.CheckBox();
            this.ChkDocAgents = new System.Windows.Forms.CheckBox();
            this.ChkIncludeSubledger = new System.Windows.Forms.CheckBox();
            this.ChkIncludeRemarks = new System.Windows.Forms.CheckBox();
            this.ChkSelectAll = new System.Windows.Forms.CheckBox();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Show = new DevExpress.XtraEditors.SimpleButton();
            this.PanelHeader = new MrPanel();
            this.mrGroup3 = new MrGroup();
            this.clsSeparator1 = new ClsSeparator();
            this.ChkIncludeLedger = new System.Windows.Forms.CheckBox();
            this.ChkIncludeNarration = new System.Windows.Forms.CheckBox();
            this.ChkRefVno = new System.Windows.Forms.CheckBox();
            this.mrGroup2 = new MrGroup();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.BtnDescription = new System.Windows.Forms.RadioButton();
            this.RbtnBalance = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.RbtnCurrencyBoth = new System.Windows.Forms.RadioButton();
            this.RbtnLocal = new System.Windows.Forms.RadioButton();
            this.RbtnForeign = new System.Windows.Forms.RadioButton();
            this.GrpLedgerType = new System.Windows.Forms.GroupBox();
            this.RbtnBank = new System.Windows.Forms.RadioButton();
            this.RbtnCash = new System.Windows.Forms.RadioButton();
            this.RbtnAll = new System.Windows.Forms.RadioButton();
            this.RbtnCustomer = new System.Windows.Forms.RadioButton();
            this.RbtnVendor = new System.Windows.Forms.RadioButton();
            this.RbtnBoth = new System.Windows.Forms.RadioButton();
            this.rChkSubledgerWise = new System.Windows.Forms.RadioButton();
            this.rChkLedgerWise = new System.Windows.Forms.RadioButton();
            this.rChkAccountSubGroupWise = new System.Windows.Forms.RadioButton();
            this.rChkAgentWise = new System.Windows.Forms.RadioButton();
            this.rChkDepartmentWise = new System.Windows.Forms.RadioButton();
            this.rChkAccountGroupWise = new System.Windows.Forms.RadioButton();
            this.mrGroup1 = new MrGroup();
            this.rChkDetails = new System.Windows.Forms.RadioButton();
            this.rChkSummary = new System.Windows.Forms.RadioButton();
            this.PanelHeader.SuspendLayout();
            this.mrGroup3.SuspendLayout();
            this.mrGroup2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.GrpLedgerType.SuspendLayout();
            this.mrGroup1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CmbDateType
            // 
            this.CmbDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbDateType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbDateType.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbDateType.FormattingEnabled = true;
            this.CmbDateType.Items.AddRange(new object[] {
            "Custom Date",
            "Today",
            "Yesterday",
            "Current Week",
            "Last Week",
            "Current Month",
            "Last Month",
            "Upto Date",
            "Accounting Period"});
            this.CmbDateType.Location = new System.Drawing.Point(6, 17);
            this.CmbDateType.Name = "CmbDateType";
            this.CmbDateType.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CmbDateType.Size = new System.Drawing.Size(363, 28);
            this.CmbDateType.TabIndex = 2;
            this.CmbDateType.SelectedIndexChanged += new System.EventHandler(this.CmbDateType_SelectedIndexChanged);
            // 
            // MskToDate
            // 
            this.MskToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskToDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskToDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskToDate.Location = new System.Drawing.Point(158, 48);
            this.MskToDate.Mask = "00/00/0000";
            this.MskToDate.Name = "MskToDate";
            this.MskToDate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.MskToDate.Size = new System.Drawing.Size(145, 26);
            this.MskToDate.TabIndex = 4;
            this.MskToDate.Enter += new System.EventHandler(this.MskToDate_Enter);
            this.MskToDate.Leave += new System.EventHandler(this.MskToDate_Leave);
            this.MskToDate.Validating += new System.ComponentModel.CancelEventHandler(this.MskToDate_Validating);
            // 
            // MskFrom
            // 
            this.MskFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskFrom.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskFrom.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskFrom.Location = new System.Drawing.Point(7, 48);
            this.MskFrom.Mask = "00/00/0000";
            this.MskFrom.Name = "MskFrom";
            this.MskFrom.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.MskFrom.Size = new System.Drawing.Size(145, 26);
            this.MskFrom.TabIndex = 3;
            this.MskFrom.Enter += new System.EventHandler(this.MskFrom_Enter);
            this.MskFrom.Leave += new System.EventHandler(this.MskFrom_Leave);
            this.MskFrom.Validating += new System.ComponentModel.CancelEventHandler(this.MskFrom_Validating);
            // 
            // ChkLedgerPanVat
            // 
            this.ChkLedgerPanVat.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkLedgerPanVat.Location = new System.Drawing.Point(365, 26);
            this.ChkLedgerPanVat.Name = "ChkLedgerPanVat";
            this.ChkLedgerPanVat.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkLedgerPanVat.Size = new System.Drawing.Size(190, 24);
            this.ChkLedgerPanVat.TabIndex = 10;
            this.ChkLedgerPanVat.Text = "Show TPAN/VAT";
            this.ChkLedgerPanVat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkLedgerPanVat.UseVisualStyleBackColor = true;
            // 
            // ChkIncludePDC
            // 
            this.ChkIncludePDC.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludePDC.Location = new System.Drawing.Point(6, 122);
            this.ChkIncludePDC.Name = "ChkIncludePDC";
            this.ChkIncludePDC.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludePDC.Size = new System.Drawing.Size(167, 24);
            this.ChkIncludePDC.TabIndex = 4;
            this.ChkIncludePDC.Text = "Include PDC";
            this.ChkIncludePDC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludePDC.UseVisualStyleBackColor = true;
            // 
            // ChkDNCNDetails
            // 
            this.ChkDNCNDetails.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDNCNDetails.Location = new System.Drawing.Point(365, 74);
            this.ChkDNCNDetails.Name = "ChkDNCNDetails";
            this.ChkDNCNDetails.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDNCNDetails.Size = new System.Drawing.Size(190, 24);
            this.ChkDNCNDetails.TabIndex = 12;
            this.ChkDNCNDetails.Text = "DN/CN Details";
            this.ChkDNCNDetails.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDNCNDetails.UseVisualStyleBackColor = true;
            // 
            // ChkPostingDetails
            // 
            this.ChkPostingDetails.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkPostingDetails.Location = new System.Drawing.Point(365, 98);
            this.ChkPostingDetails.Name = "ChkPostingDetails";
            this.ChkPostingDetails.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkPostingDetails.Size = new System.Drawing.Size(190, 24);
            this.ChkPostingDetails.TabIndex = 13;
            this.ChkPostingDetails.Text = "Posting Details";
            this.ChkPostingDetails.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkPostingDetails.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeUDF
            // 
            this.ChkIncludeUDF.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeUDF.Location = new System.Drawing.Point(188, 122);
            this.ChkIncludeUDF.Name = "ChkIncludeUDF";
            this.ChkIncludeUDF.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeUDF.Size = new System.Drawing.Size(167, 24);
            this.ChkIncludeUDF.TabIndex = 9;
            this.ChkIncludeUDF.Text = "Include UDF";
            this.ChkIncludeUDF.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeUDF.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeAdjustment
            // 
            this.ChkIncludeAdjustment.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeAdjustment.Location = new System.Drawing.Point(365, 50);
            this.ChkIncludeAdjustment.Name = "ChkIncludeAdjustment";
            this.ChkIncludeAdjustment.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeAdjustment.Size = new System.Drawing.Size(190, 24);
            this.ChkIncludeAdjustment.TabIndex = 11;
            this.ChkIncludeAdjustment.Text = "With Adjustment";
            this.ChkIncludeAdjustment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeAdjustment.UseVisualStyleBackColor = true;
            // 
            // ChkProductDetails
            // 
            this.ChkProductDetails.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkProductDetails.Location = new System.Drawing.Point(365, 122);
            this.ChkProductDetails.Name = "ChkProductDetails";
            this.ChkProductDetails.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkProductDetails.Size = new System.Drawing.Size(190, 24);
            this.ChkProductDetails.TabIndex = 14;
            this.ChkProductDetails.Text = "Product Details";
            this.ChkProductDetails.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkProductDetails.UseVisualStyleBackColor = true;
            // 
            // ChkAllSubledger
            // 
            this.ChkAllSubledger.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAllSubledger.Location = new System.Drawing.Point(6, 98);
            this.ChkAllSubledger.Name = "ChkAllSubledger";
            this.ChkAllSubledger.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkAllSubledger.Size = new System.Drawing.Size(167, 24);
            this.ChkAllSubledger.TabIndex = 3;
            this.ChkAllSubledger.Text = "All Sub Ledger";
            this.ChkAllSubledger.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkAllSubledger.UseVisualStyleBackColor = true;
            // 
            // ChkDate
            // 
            this.ChkDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDate.Location = new System.Drawing.Point(188, 26);
            this.ChkDate.Name = "ChkDate";
            this.ChkDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDate.Size = new System.Drawing.Size(167, 24);
            this.ChkDate.TabIndex = 5;
            this.ChkDate.Text = "Date";
            this.ChkDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDate.UseVisualStyleBackColor = true;
            // 
            // ChkDocAgents
            // 
            this.ChkDocAgents.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDocAgents.Location = new System.Drawing.Point(6, 50);
            this.ChkDocAgents.Name = "ChkDocAgents";
            this.ChkDocAgents.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDocAgents.Size = new System.Drawing.Size(167, 24);
            this.ChkDocAgents.TabIndex = 1;
            this.ChkDocAgents.Text = "Doc Agent";
            this.ChkDocAgents.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDocAgents.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeSubledger
            // 
            this.ChkIncludeSubledger.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeSubledger.Location = new System.Drawing.Point(6, 74);
            this.ChkIncludeSubledger.Name = "ChkIncludeSubledger";
            this.ChkIncludeSubledger.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeSubledger.Size = new System.Drawing.Size(167, 24);
            this.ChkIncludeSubledger.TabIndex = 2;
            this.ChkIncludeSubledger.Text = "Sub Ledger";
            this.ChkIncludeSubledger.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeSubledger.UseVisualStyleBackColor = true;
            this.ChkIncludeSubledger.CheckedChanged += new System.EventHandler(this.ChkIncludeSubledger_CheckedChanged);
            // 
            // ChkIncludeRemarks
            // 
            this.ChkIncludeRemarks.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeRemarks.Location = new System.Drawing.Point(188, 74);
            this.ChkIncludeRemarks.Name = "ChkIncludeRemarks";
            this.ChkIncludeRemarks.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeRemarks.Size = new System.Drawing.Size(167, 24);
            this.ChkIncludeRemarks.TabIndex = 7;
            this.ChkIncludeRemarks.Text = "Remarks";
            this.ChkIncludeRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeRemarks.UseVisualStyleBackColor = true;
            // 
            // ChkSelectAll
            // 
            this.ChkSelectAll.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSelectAll.Location = new System.Drawing.Point(6, 158);
            this.ChkSelectAll.Name = "ChkSelectAll";
            this.ChkSelectAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSelectAll.Size = new System.Drawing.Size(146, 24);
            this.ChkSelectAll.TabIndex = 15;
            this.ChkSelectAll.Text = "All Ledger";
            this.ChkSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSelectAll.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(348, 152);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(109, 36);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "&CANCEL";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btn_Show
            // 
            this.btn_Show.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Show.Appearance.Options.UseFont = true;
            this.btn_Show.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.btn_Show.Location = new System.Drawing.Point(237, 152);
            this.btn_Show.Name = "btn_Show";
            this.btn_Show.Size = new System.Drawing.Size(109, 36);
            this.btn_Show.TabIndex = 16;
            this.btn_Show.Text = "&SHOW";
            this.btn_Show.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.mrGroup3);
            this.PanelHeader.Controls.Add(this.mrGroup2);
            this.PanelHeader.Controls.Add(this.mrGroup1);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(567, 451);
            this.PanelHeader.TabIndex = 0;
            // 
            // mrGroup3
            // 
            this.mrGroup3.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup3.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup3.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup3.BorderColor = System.Drawing.Color.White;
            this.mrGroup3.BorderThickness = 1F;
            this.mrGroup3.Controls.Add(this.clsSeparator1);
            this.mrGroup3.Controls.Add(this.btn_Show);
            this.mrGroup3.Controls.Add(this.ChkSelectAll);
            this.mrGroup3.Controls.Add(this.ChkPostingDetails);
            this.mrGroup3.Controls.Add(this.btnCancel);
            this.mrGroup3.Controls.Add(this.ChkIncludeRemarks);
            this.mrGroup3.Controls.Add(this.ChkDNCNDetails);
            this.mrGroup3.Controls.Add(this.ChkIncludeLedger);
            this.mrGroup3.Controls.Add(this.ChkProductDetails);
            this.mrGroup3.Controls.Add(this.ChkDate);
            this.mrGroup3.Controls.Add(this.ChkLedgerPanVat);
            this.mrGroup3.Controls.Add(this.ChkIncludeAdjustment);
            this.mrGroup3.Controls.Add(this.ChkIncludePDC);
            this.mrGroup3.Controls.Add(this.ChkAllSubledger);
            this.mrGroup3.Controls.Add(this.ChkIncludeSubledger);
            this.mrGroup3.Controls.Add(this.ChkIncludeNarration);
            this.mrGroup3.Controls.Add(this.ChkIncludeUDF);
            this.mrGroup3.Controls.Add(this.ChkRefVno);
            this.mrGroup3.Controls.Add(this.ChkDocAgents);
            this.mrGroup3.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup3.GroupImage = null;
            this.mrGroup3.GroupTitle = "Filter Value";
            this.mrGroup3.Location = new System.Drawing.Point(5, 255);
            this.mrGroup3.Name = "mrGroup3";
            this.mrGroup3.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup3.PaintGroupBox = false;
            this.mrGroup3.RoundCorners = 10;
            this.mrGroup3.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup3.ShadowControl = true;
            this.mrGroup3.ShadowThickness = 3;
            this.mrGroup3.Size = new System.Drawing.Size(564, 195);
            this.mrGroup3.TabIndex = 2;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator1.Location = new System.Drawing.Point(6, 147);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(538, 2);
            this.clsSeparator1.TabIndex = 18;
            this.clsSeparator1.TabStop = false;
            // 
            // ChkIncludeLedger
            // 
            this.ChkIncludeLedger.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeLedger.Location = new System.Drawing.Point(6, 26);
            this.ChkIncludeLedger.Name = "ChkIncludeLedger";
            this.ChkIncludeLedger.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeLedger.Size = new System.Drawing.Size(167, 24);
            this.ChkIncludeLedger.TabIndex = 0;
            this.ChkIncludeLedger.Text = "Ledger";
            this.ChkIncludeLedger.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeLedger.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeNarration
            // 
            this.ChkIncludeNarration.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeNarration.Location = new System.Drawing.Point(188, 50);
            this.ChkIncludeNarration.Name = "ChkIncludeNarration";
            this.ChkIncludeNarration.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeNarration.Size = new System.Drawing.Size(167, 24);
            this.ChkIncludeNarration.TabIndex = 6;
            this.ChkIncludeNarration.Text = "Narration";
            this.ChkIncludeNarration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeNarration.UseVisualStyleBackColor = true;
            // 
            // ChkRefVno
            // 
            this.ChkRefVno.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkRefVno.Location = new System.Drawing.Point(188, 98);
            this.ChkRefVno.Name = "ChkRefVno";
            this.ChkRefVno.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkRefVno.Size = new System.Drawing.Size(167, 24);
            this.ChkRefVno.TabIndex = 8;
            this.ChkRefVno.Text = "Ref Vno";
            this.ChkRefVno.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkRefVno.UseVisualStyleBackColor = true;
            this.ChkRefVno.CheckStateChanged += new System.EventHandler(this.ChkRefVno_CheckStateChanged);
            // 
            // mrGroup2
            // 
            this.mrGroup2.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup2.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup2.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup2.BorderColor = System.Drawing.Color.White;
            this.mrGroup2.BorderThickness = 1F;
            this.mrGroup2.Controls.Add(this.groupBox7);
            this.mrGroup2.Controls.Add(this.groupBox3);
            this.mrGroup2.Controls.Add(this.GrpLedgerType);
            this.mrGroup2.Controls.Add(this.rChkSubledgerWise);
            this.mrGroup2.Controls.Add(this.rChkLedgerWise);
            this.mrGroup2.Controls.Add(this.rChkAccountSubGroupWise);
            this.mrGroup2.Controls.Add(this.rChkAgentWise);
            this.mrGroup2.Controls.Add(this.rChkDepartmentWise);
            this.mrGroup2.Controls.Add(this.rChkAccountGroupWise);
            this.mrGroup2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup2.GroupImage = null;
            this.mrGroup2.GroupTitle = "Report Type";
            this.mrGroup2.Location = new System.Drawing.Point(5, 75);
            this.mrGroup2.Name = "mrGroup2";
            this.mrGroup2.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup2.PaintGroupBox = false;
            this.mrGroup2.RoundCorners = 10;
            this.mrGroup2.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup2.ShadowControl = true;
            this.mrGroup2.ShadowThickness = 3;
            this.mrGroup2.Size = new System.Drawing.Size(564, 181);
            this.mrGroup2.TabIndex = 1;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.BtnDescription);
            this.groupBox7.Controls.Add(this.RbtnBalance);
            this.groupBox7.Location = new System.Drawing.Point(397, 104);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(139, 72);
            this.groupBox7.TabIndex = 8;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Sort On";
            // 
            // BtnDescription
            // 
            this.BtnDescription.AutoCheck = false;
            this.BtnDescription.AutoSize = true;
            this.BtnDescription.Checked = true;
            this.BtnDescription.Location = new System.Drawing.Point(6, 19);
            this.BtnDescription.Name = "BtnDescription";
            this.BtnDescription.Size = new System.Drawing.Size(118, 24);
            this.BtnDescription.TabIndex = 0;
            this.BtnDescription.Text = "Description";
            this.BtnDescription.UseVisualStyleBackColor = true;
            // 
            // RbtnBalance
            // 
            this.RbtnBalance.AutoSize = true;
            this.RbtnBalance.Location = new System.Drawing.Point(6, 41);
            this.RbtnBalance.Name = "RbtnBalance";
            this.RbtnBalance.Size = new System.Drawing.Size(90, 24);
            this.RbtnBalance.TabIndex = 1;
            this.RbtnBalance.Text = "Balance";
            this.RbtnBalance.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.RbtnCurrencyBoth);
            this.groupBox3.Controls.Add(this.RbtnLocal);
            this.groupBox3.Controls.Add(this.RbtnForeign);
            this.groupBox3.Location = new System.Drawing.Point(397, 11);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(139, 93);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Currency Type";
            // 
            // RbtnCurrencyBoth
            // 
            this.RbtnCurrencyBoth.AutoSize = true;
            this.RbtnCurrencyBoth.Location = new System.Drawing.Point(9, 43);
            this.RbtnCurrencyBoth.Name = "RbtnCurrencyBoth";
            this.RbtnCurrencyBoth.Size = new System.Drawing.Size(65, 24);
            this.RbtnCurrencyBoth.TabIndex = 1;
            this.RbtnCurrencyBoth.Text = "Both";
            this.RbtnCurrencyBoth.UseVisualStyleBackColor = true;
            // 
            // RbtnLocal
            // 
            this.RbtnLocal.AutoSize = true;
            this.RbtnLocal.Checked = true;
            this.RbtnLocal.Location = new System.Drawing.Point(9, 19);
            this.RbtnLocal.Name = "RbtnLocal";
            this.RbtnLocal.Size = new System.Drawing.Size(68, 24);
            this.RbtnLocal.TabIndex = 0;
            this.RbtnLocal.TabStop = true;
            this.RbtnLocal.Text = "Local";
            this.RbtnLocal.UseVisualStyleBackColor = true;
            // 
            // RbtnForeign
            // 
            this.RbtnForeign.AutoSize = true;
            this.RbtnForeign.Location = new System.Drawing.Point(9, 67);
            this.RbtnForeign.Name = "RbtnForeign";
            this.RbtnForeign.Size = new System.Drawing.Size(87, 24);
            this.RbtnForeign.TabIndex = 2;
            this.RbtnForeign.Text = "Foreign";
            this.RbtnForeign.UseVisualStyleBackColor = true;
            // 
            // GrpLedgerType
            // 
            this.GrpLedgerType.Controls.Add(this.RbtnBank);
            this.GrpLedgerType.Controls.Add(this.RbtnCash);
            this.GrpLedgerType.Controls.Add(this.RbtnAll);
            this.GrpLedgerType.Controls.Add(this.RbtnCustomer);
            this.GrpLedgerType.Controls.Add(this.RbtnVendor);
            this.GrpLedgerType.Controls.Add(this.RbtnBoth);
            this.GrpLedgerType.Location = new System.Drawing.Point(259, 11);
            this.GrpLedgerType.Name = "GrpLedgerType";
            this.GrpLedgerType.Size = new System.Drawing.Size(132, 167);
            this.GrpLedgerType.TabIndex = 6;
            this.GrpLedgerType.TabStop = false;
            this.GrpLedgerType.Text = "Ledger Type";
            // 
            // RbtnBank
            // 
            this.RbtnBank.AutoSize = true;
            this.RbtnBank.Location = new System.Drawing.Point(6, 45);
            this.RbtnBank.Name = "RbtnBank";
            this.RbtnBank.Size = new System.Drawing.Size(69, 24);
            this.RbtnBank.TabIndex = 1;
            this.RbtnBank.Text = "Bank";
            this.RbtnBank.UseVisualStyleBackColor = true;
            // 
            // RbtnCash
            // 
            this.RbtnCash.AutoSize = true;
            this.RbtnCash.Location = new System.Drawing.Point(6, 69);
            this.RbtnCash.Name = "RbtnCash";
            this.RbtnCash.Size = new System.Drawing.Size(67, 24);
            this.RbtnCash.TabIndex = 2;
            this.RbtnCash.Text = "Cash";
            this.RbtnCash.UseVisualStyleBackColor = true;
            // 
            // RbtnAll
            // 
            this.RbtnAll.AutoSize = true;
            this.RbtnAll.Checked = true;
            this.RbtnAll.Location = new System.Drawing.Point(6, 21);
            this.RbtnAll.Name = "RbtnAll";
            this.RbtnAll.Size = new System.Drawing.Size(48, 24);
            this.RbtnAll.TabIndex = 0;
            this.RbtnAll.TabStop = true;
            this.RbtnAll.Text = "All";
            this.RbtnAll.UseVisualStyleBackColor = true;
            // 
            // RbtnCustomer
            // 
            this.RbtnCustomer.AutoSize = true;
            this.RbtnCustomer.Location = new System.Drawing.Point(6, 93);
            this.RbtnCustomer.Name = "RbtnCustomer";
            this.RbtnCustomer.Size = new System.Drawing.Size(104, 24);
            this.RbtnCustomer.TabIndex = 3;
            this.RbtnCustomer.Text = "Customer";
            this.RbtnCustomer.UseVisualStyleBackColor = true;
            // 
            // RbtnVendor
            // 
            this.RbtnVendor.AutoSize = true;
            this.RbtnVendor.Location = new System.Drawing.Point(6, 117);
            this.RbtnVendor.Name = "RbtnVendor";
            this.RbtnVendor.Size = new System.Drawing.Size(84, 24);
            this.RbtnVendor.TabIndex = 4;
            this.RbtnVendor.Text = "Vendor";
            this.RbtnVendor.UseVisualStyleBackColor = true;
            // 
            // RbtnBoth
            // 
            this.RbtnBoth.AutoSize = true;
            this.RbtnBoth.Location = new System.Drawing.Point(6, 141);
            this.RbtnBoth.Name = "RbtnBoth";
            this.RbtnBoth.Size = new System.Drawing.Size(65, 24);
            this.RbtnBoth.TabIndex = 5;
            this.RbtnBoth.Text = "Both";
            this.RbtnBoth.UseVisualStyleBackColor = true;
            // 
            // rChkSubledgerWise
            // 
            this.rChkSubledgerWise.AutoSize = true;
            this.rChkSubledgerWise.Location = new System.Drawing.Point(9, 150);
            this.rChkSubledgerWise.Name = "rChkSubledgerWise";
            this.rChkSubledgerWise.Size = new System.Drawing.Size(108, 24);
            this.rChkSubledgerWise.TabIndex = 5;
            this.rChkSubledgerWise.Text = "SubLedger";
            this.rChkSubledgerWise.UseVisualStyleBackColor = true;
            this.rChkSubledgerWise.CheckedChanged += new System.EventHandler(this.rChkSubledgerWise_CheckedChanged);
            // 
            // rChkLedgerWise
            // 
            this.rChkLedgerWise.AutoSize = true;
            this.rChkLedgerWise.Checked = true;
            this.rChkLedgerWise.Location = new System.Drawing.Point(9, 30);
            this.rChkLedgerWise.Name = "rChkLedgerWise";
            this.rChkLedgerWise.Size = new System.Drawing.Size(81, 24);
            this.rChkLedgerWise.TabIndex = 0;
            this.rChkLedgerWise.TabStop = true;
            this.rChkLedgerWise.Text = "Ledger";
            this.rChkLedgerWise.UseVisualStyleBackColor = true;
            this.rChkLedgerWise.CheckedChanged += new System.EventHandler(this.rChkLedgerWise_CheckedChanged);
            // 
            // rChkAccountSubGroupWise
            // 
            this.rChkAccountSubGroupWise.AutoSize = true;
            this.rChkAccountSubGroupWise.Location = new System.Drawing.Point(9, 78);
            this.rChkAccountSubGroupWise.Name = "rChkAccountSubGroupWise";
            this.rChkAccountSubGroupWise.Size = new System.Drawing.Size(114, 24);
            this.rChkAccountSubGroupWise.TabIndex = 2;
            this.rChkAccountSubGroupWise.Text = "Sub Group";
            this.rChkAccountSubGroupWise.UseVisualStyleBackColor = true;
            // 
            // rChkAgentWise
            // 
            this.rChkAgentWise.AutoSize = true;
            this.rChkAgentWise.Location = new System.Drawing.Point(9, 126);
            this.rChkAgentWise.Name = "rChkAgentWise";
            this.rChkAgentWise.Size = new System.Drawing.Size(73, 24);
            this.rChkAgentWise.TabIndex = 4;
            this.rChkAgentWise.Text = "Agent";
            this.rChkAgentWise.UseVisualStyleBackColor = true;
            // 
            // rChkDepartmentWise
            // 
            this.rChkDepartmentWise.AutoSize = true;
            this.rChkDepartmentWise.Location = new System.Drawing.Point(9, 102);
            this.rChkDepartmentWise.Name = "rChkDepartmentWise";
            this.rChkDepartmentWise.Size = new System.Drawing.Size(122, 24);
            this.rChkDepartmentWise.TabIndex = 3;
            this.rChkDepartmentWise.Text = "Department";
            this.rChkDepartmentWise.UseVisualStyleBackColor = true;
            // 
            // rChkAccountGroupWise
            // 
            this.rChkAccountGroupWise.AutoSize = true;
            this.rChkAccountGroupWise.Location = new System.Drawing.Point(9, 54);
            this.rChkAccountGroupWise.Name = "rChkAccountGroupWise";
            this.rChkAccountGroupWise.Size = new System.Drawing.Size(146, 24);
            this.rChkAccountGroupWise.TabIndex = 1;
            this.rChkAccountGroupWise.Text = "Account Group";
            this.rChkAccountGroupWise.UseVisualStyleBackColor = true;
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.rChkDetails);
            this.mrGroup1.Controls.Add(this.rChkSummary);
            this.mrGroup1.Controls.Add(this.CmbDateType);
            this.mrGroup1.Controls.Add(this.MskFrom);
            this.mrGroup1.Controls.Add(this.MskToDate);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "";
            this.mrGroup1.Location = new System.Drawing.Point(5, -7);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 10;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = true;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(564, 83);
            this.mrGroup1.TabIndex = 0;
            // 
            // rChkDetails
            // 
            this.rChkDetails.AutoSize = true;
            this.rChkDetails.Location = new System.Drawing.Point(446, 47);
            this.rChkDetails.Name = "rChkDetails";
            this.rChkDetails.Size = new System.Drawing.Size(82, 24);
            this.rChkDetails.TabIndex = 1;
            this.rChkDetails.Text = "Details";
            this.rChkDetails.UseVisualStyleBackColor = true;
            this.rChkDetails.CheckedChanged += new System.EventHandler(this.rChkSummary_CheckedChanged);
            // 
            // rChkSummary
            // 
            this.rChkSummary.AutoSize = true;
            this.rChkSummary.Checked = true;
            this.rChkSummary.Location = new System.Drawing.Point(446, 17);
            this.rChkSummary.Name = "rChkSummary";
            this.rChkSummary.Size = new System.Drawing.Size(104, 24);
            this.rChkSummary.TabIndex = 0;
            this.rChkSummary.TabStop = true;
            this.rChkSummary.Text = "Summary";
            this.rChkSummary.UseVisualStyleBackColor = true;
            this.rChkSummary.CheckedChanged += new System.EventHandler(this.rChkSummary_CheckedChanged);
            // 
            // FrmLedger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(567, 451);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmLedger";
            this.ShowIcon = false;
            this.Text = "GENERAL LEDGER REPORT";
            this.Load += new System.EventHandler(this.FrmLedger_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmLedger_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmLedger_KeyPress);
            this.PanelHeader.ResumeLayout(false);
            this.mrGroup3.ResumeLayout(false);
            this.mrGroup2.ResumeLayout(false);
            this.mrGroup2.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.GrpLedgerType.ResumeLayout(false);
            this.GrpLedgerType.PerformLayout();
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private CheckBox ChkSelectAll;
        private CheckBox ChkDate;
        private CheckBox ChkIncludeSubledger;
        private CheckBox ChkIncludeRemarks;
        private CheckBox ChkAllSubledger;
        private CheckBox ChkIncludeUDF;
        private CheckBox ChkProductDetails;
        private CheckBox ChkDNCNDetails;
        private CheckBox ChkPostingDetails;
        private ComboBox CmbDateType;
        private SimpleButton btnCancel;
        private SimpleButton btn_Show;
        private CheckBox ChkIncludePDC;
        private CheckBox ChkIncludeAdjustment;
        private CheckBox ChkDocAgents;
        private CheckBox ChkLedgerPanVat;
        private CheckBox ChkRefVno;
        private CheckBox ChkIncludeNarration;
        private RadioButton rChkLedgerWise;
        private RadioButton rChkAccountSubGroupWise;
        private RadioButton rChkAccountGroupWise;
        private RadioButton RbtnLocal;
        private RadioButton RbtnForeign;
        private RadioButton RbtnCurrencyBoth;
        private RadioButton rChkSubledgerWise;
        private RadioButton rChkAgentWise;
        private RadioButton rChkDepartmentWise;
        private RadioButton RbtnAll;
        private RadioButton RbtnCustomer;
        private RadioButton RbtnVendor;
        private RadioButton RbtnBoth;
        private RadioButton RbtnCash;
        private RadioButton RbtnBank;
        private RadioButton BtnDescription;
        private RadioButton RbtnBalance;
        private RadioButton rChkDetails;
        private RadioButton rChkSummary;
        private GroupBox GrpLedgerType;
        private GroupBox groupBox3;
        private GroupBox groupBox7;
        private MrGroup mrGroup1;
        private MrGroup mrGroup2;
        private MrGroup mrGroup3;
        private CheckBox ChkIncludeLedger;
        private ClsSeparator clsSeparator1;
        private MrMaskedTextBox MskToDate;
        private MrMaskedTextBox MskFrom;
        private MrPanel PanelHeader;
    }
}