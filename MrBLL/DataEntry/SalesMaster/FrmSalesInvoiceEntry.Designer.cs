using System.Windows.Forms;
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.DataEntry.SalesMaster
{
    partial class FrmSalesInvoiceEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSalesInvoiceEntry));
            this.TabLedgerOpening = new System.Windows.Forms.TabControl();
            this.TabProductDetails = new System.Windows.Forms.TabPage();
            this.DGrid = new MrDAL.Control.ControlsEx.Control.EntryGridViewEx();
            this.mrPanel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.LblAltStockQty = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.LblAltSalesRate = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.LblSalesRate = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.LblStockQty = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.LblShortName = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.TabAttachment = new System.Windows.Forms.TabPage();
            this.LinkAttachment5 = new System.Windows.Forms.LinkLabel();
            this.LinkAttachment4 = new System.Windows.Forms.LinkLabel();
            this.LinkAttachment3 = new System.Windows.Forms.LinkLabel();
            this.LinkAttachment2 = new System.Windows.Forms.LinkLabel();
            this.LinkAttachment1 = new System.Windows.Forms.LinkLabel();
            this.BtnAttachment5 = new System.Windows.Forms.Button();
            this.LblAttachment5 = new System.Windows.Forms.Label();
            this.PAttachment5 = new System.Windows.Forms.PictureBox();
            this.BtnAttachment4 = new System.Windows.Forms.Button();
            this.LblAttachment4 = new System.Windows.Forms.Label();
            this.PAttachment4 = new System.Windows.Forms.PictureBox();
            this.BtnAttachment3 = new System.Windows.Forms.Button();
            this.LblAttachment3 = new System.Windows.Forms.Label();
            this.PAttachment3 = new System.Windows.Forms.PictureBox();
            this.BtnAttachment2 = new System.Windows.Forms.Button();
            this.LblAttachment2 = new System.Windows.Forms.Label();
            this.PAttachment2 = new System.Windows.Forms.PictureBox();
            this.BtnAttachment1 = new System.Windows.Forms.Button();
            this.LblAttachment1 = new System.Windows.Forms.Label();
            this.PAttachment1 = new System.Windows.Forms.PictureBox();
            this.TabAdditionalInformation = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TxtCustomName = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label49 = new System.Windows.Forms.Label();
            this.TxtLCNumber = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.MskVendorOrderDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.MskExportInvoiceDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.MskContractNoDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.TxtContractNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtBankDetails = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.TxtVendorOrderNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtExportInvoiceNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TxtDriver = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.TxtLicenseNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtPhoneNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CmbBiltyType = new System.Windows.Forms.ComboBox();
            this.label43 = new System.Windows.Forms.Label();
            this.MskBiltyDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.TxtTransport = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtPackage = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.TxtBiltyNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtVechileNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.TabMailingAddress = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.TxtSEmail = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.TxtShippingAddress = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtSCountry = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.TxtSState = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtSCity = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.TxtMEmail = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.TxtMailingAddress = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtMCountry = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.TxtMState = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtMCity = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.panel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.label17 = new System.Windows.Forms.Label();
            this.MskOrderMiti = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.MskOrderDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.MskChallanMiti = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.MskChallanDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.BtnReturnInvoice = new DevExpress.XtraEditors.SimpleButton();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_Remarks = new System.Windows.Forms.Label();
            this.TxtRemarks = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_NoInWords = new System.Windows.Forms.Label();
            this.LblNumberInWords = new System.Windows.Forms.Label();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.LblTotalAltQty = new System.Windows.Forms.Label();
            this.TxtBillTermAmount = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.LblTotalBasicAmount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.BtnBillingTerm = new System.Windows.Forms.Button();
            this.LblTotalNetAmount = new System.Windows.Forms.Label();
            this.LblTotalLocalNetAmount = new System.Windows.Forms.Label();
            this.LblTotalQty = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.lbl_TotQty1 = new System.Windows.Forms.Label();
            this.lbl_TotAltQty1 = new System.Windows.Forms.Label();
            this.TxtCurrency = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnCurrency = new System.Windows.Forms.Button();
            this.label31 = new System.Windows.Forms.Label();
            this.BtnChallanNo = new System.Windows.Forms.Button();
            this.TxtChallan = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnVendor = new System.Windows.Forms.Button();
            this.TxtOrder = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnOrderNo = new System.Windows.Forms.Button();
            this.TxtDepartment = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnDepartment = new System.Windows.Forms.Button();
            this.TxtCustomer = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtDueDays = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CmbInvoiceType = new System.Windows.Forms.ComboBox();
            this.TxtAgent = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnVno = new System.Windows.Forms.Button();
            this.CmbPaymentMode = new System.Windows.Forms.ComboBox();
            this.BtnAgent = new System.Windows.Forms.Button();
            this.label30 = new System.Windows.Forms.Label();
            this.TxtSubledger = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtVno = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnSubledger = new System.Windows.Forms.Button();
            this.label29 = new System.Windows.Forms.Label();
            this.TxtRefVno = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.MskMiti = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.MskDueDays = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.MskDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.TxtCurrencyRate = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.lbl_Currency = new System.Windows.Forms.Label();
            this.MskRefDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.lbl_CurrencyRate = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.LblBalanceType = new System.Windows.Forms.Label();
            this.LblCreditDays = new System.Windows.Forms.Label();
            this.LblPanNo = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.lbl_Currentbal = new System.Windows.Forms.Label();
            this.LblCreditLimit = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.LblBalance = new System.Windows.Forms.Label();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnPrintInvoice = new DevExpress.XtraEditors.SimpleButton();
            this.BtnReverse = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCopy = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnPurchaseInvoice = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_VoucherNo = new System.Windows.Forms.Label();
            this.lbl_Class = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_RefVoucherNo = new System.Windows.Forms.Label();
            this.lbl_CBLedger = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.TabLedgerOpening.SuspendLayout();
            this.TabProductDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).BeginInit();
            this.mrPanel1.SuspendLayout();
            this.TabAttachment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment1)).BeginInit();
            this.TabAdditionalInformation.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.TabMailingAddress.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabLedgerOpening
            // 
            this.TabLedgerOpening.Controls.Add(this.TabProductDetails);
            this.TabLedgerOpening.Controls.Add(this.TabAttachment);
            this.TabLedgerOpening.Controls.Add(this.TabAdditionalInformation);
            this.TabLedgerOpening.Controls.Add(this.TabMailingAddress);
            this.TabLedgerOpening.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabLedgerOpening.Location = new System.Drawing.Point(3, 221);
            this.TabLedgerOpening.Name = "TabLedgerOpening";
            this.TabLedgerOpening.SelectedIndex = 0;
            this.TabLedgerOpening.Size = new System.Drawing.Size(1043, 243);
            this.TabLedgerOpening.TabIndex = 16;
            // 
            // TabProductDetails
            // 
            this.TabProductDetails.Controls.Add(this.DGrid);
            this.TabProductDetails.Controls.Add(this.mrPanel1);
            this.TabProductDetails.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabProductDetails.Location = new System.Drawing.Point(4, 27);
            this.TabProductDetails.Name = "TabProductDetails";
            this.TabProductDetails.Padding = new System.Windows.Forms.Padding(3);
            this.TabProductDetails.Size = new System.Drawing.Size(1035, 212);
            this.TabProductDetails.TabIndex = 0;
            this.TabProductDetails.Text = "PRODUCT DETAILS";
            this.TabProductDetails.UseVisualStyleBackColor = true;
            // 
            // DGrid
            // 
            this.DGrid.AllowUserToAddRows = false;
            this.DGrid.AllowUserToDeleteRows = false;
            this.DGrid.AllowUserToResizeColumns = false;
            this.DGrid.AllowUserToResizeRows = false;
            this.DGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.DGrid.BlockNavigationOnNextRowOnEnter = true;
            this.DGrid.CausesValidation = false;
            this.DGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.DGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.DGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.DGrid.ColumnHeadersHeight = 29;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGrid.DefaultCellStyle = dataGridViewCellStyle1;
            this.DGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGrid.DoubleBufferEnabled = true;
            this.DGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.DGrid.Location = new System.Drawing.Point(3, 3);
            this.DGrid.MultiSelect = false;
            this.DGrid.Name = "DGrid";
            this.DGrid.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Bookman Old Style", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DGrid.RowHeadersVisible = false;
            this.DGrid.RowHeadersWidth = 51;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            this.DGrid.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.DGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGrid.Size = new System.Drawing.Size(1029, 177);
            this.DGrid.StandardTab = true;
            this.DGrid.TabIndex = 0;
            this.DGrid.TabStop = false;
            this.DGrid.EnterKeyPressed += new System.EventHandler<System.EventArgs>(this.OnDGridOnEnterKeyPressed);
            // 
            // mrPanel1
            // 
            this.mrPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mrPanel1.Controls.Add(this.LblAltStockQty);
            this.mrPanel1.Controls.Add(this.label56);
            this.mrPanel1.Controls.Add(this.LblAltSalesRate);
            this.mrPanel1.Controls.Add(this.label54);
            this.mrPanel1.Controls.Add(this.LblSalesRate);
            this.mrPanel1.Controls.Add(this.label52);
            this.mrPanel1.Controls.Add(this.LblStockQty);
            this.mrPanel1.Controls.Add(this.label50);
            this.mrPanel1.Controls.Add(this.LblShortName);
            this.mrPanel1.Controls.Add(this.label21);
            this.mrPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mrPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrPanel1.Location = new System.Drawing.Point(3, 180);
            this.mrPanel1.Name = "mrPanel1";
            this.mrPanel1.Size = new System.Drawing.Size(1029, 29);
            this.mrPanel1.TabIndex = 585;
            // 
            // LblAltStockQty
            // 
            this.LblAltStockQty.BackColor = System.Drawing.SystemColors.HighlightText;
            this.LblAltStockQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblAltStockQty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblAltStockQty.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAltStockQty.Location = new System.Drawing.Point(379, 2);
            this.LblAltStockQty.Name = "LblAltStockQty";
            this.LblAltStockQty.Size = new System.Drawing.Size(96, 24);
            this.LblAltStockQty.TabIndex = 253;
            this.LblAltStockQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.Location = new System.Drawing.Point(271, 4);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(102, 19);
            this.label56.TabIndex = 252;
            this.label56.Text = "Stock AltQty";
            // 
            // LblAltSalesRate
            // 
            this.LblAltSalesRate.BackColor = System.Drawing.SystemColors.HighlightText;
            this.LblAltSalesRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblAltSalesRate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblAltSalesRate.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAltSalesRate.Location = new System.Drawing.Point(926, 2);
            this.LblAltSalesRate.Name = "LblAltSalesRate";
            this.LblAltSalesRate.Size = new System.Drawing.Size(101, 24);
            this.LblAltSalesRate.TabIndex = 251;
            this.LblAltSalesRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(855, 4);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(70, 19);
            this.label54.TabIndex = 250;
            this.label54.Text = "Alt Rate";
            // 
            // LblSalesRate
            // 
            this.LblSalesRate.BackColor = System.Drawing.SystemColors.HighlightText;
            this.LblSalesRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblSalesRate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblSalesRate.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSalesRate.Location = new System.Drawing.Point(762, 2);
            this.LblSalesRate.Name = "LblSalesRate";
            this.LblSalesRate.Size = new System.Drawing.Size(92, 24);
            this.LblSalesRate.TabIndex = 249;
            this.LblSalesRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.Location = new System.Drawing.Point(672, 4);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(90, 19);
            this.label52.TabIndex = 248;
            this.label52.Text = "Sales Rate";
            // 
            // LblStockQty
            // 
            this.LblStockQty.BackColor = System.Drawing.SystemColors.HighlightText;
            this.LblStockQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblStockQty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblStockQty.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStockQty.Location = new System.Drawing.Point(555, 2);
            this.LblStockQty.Name = "LblStockQty";
            this.LblStockQty.Size = new System.Drawing.Size(115, 24);
            this.LblStockQty.TabIndex = 247;
            this.LblStockQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.Location = new System.Drawing.Point(473, 4);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(81, 19);
            this.label50.TabIndex = 246;
            this.label50.Text = "Stock Qty";
            // 
            // LblShortName
            // 
            this.LblShortName.BackColor = System.Drawing.SystemColors.HighlightText;
            this.LblShortName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblShortName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblShortName.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblShortName.Location = new System.Drawing.Point(107, 2);
            this.LblShortName.Name = "LblShortName";
            this.LblShortName.Size = new System.Drawing.Size(162, 24);
            this.LblShortName.TabIndex = 245;
            this.LblShortName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(8, 5);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(93, 19);
            this.label21.TabIndex = 244;
            this.label21.Text = "ShortName";
            // 
            // TabAttachment
            // 
            this.TabAttachment.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.TabAttachment.Controls.Add(this.LinkAttachment5);
            this.TabAttachment.Controls.Add(this.LinkAttachment4);
            this.TabAttachment.Controls.Add(this.LinkAttachment3);
            this.TabAttachment.Controls.Add(this.LinkAttachment2);
            this.TabAttachment.Controls.Add(this.LinkAttachment1);
            this.TabAttachment.Controls.Add(this.BtnAttachment5);
            this.TabAttachment.Controls.Add(this.LblAttachment5);
            this.TabAttachment.Controls.Add(this.PAttachment5);
            this.TabAttachment.Controls.Add(this.BtnAttachment4);
            this.TabAttachment.Controls.Add(this.LblAttachment4);
            this.TabAttachment.Controls.Add(this.PAttachment4);
            this.TabAttachment.Controls.Add(this.BtnAttachment3);
            this.TabAttachment.Controls.Add(this.LblAttachment3);
            this.TabAttachment.Controls.Add(this.PAttachment3);
            this.TabAttachment.Controls.Add(this.BtnAttachment2);
            this.TabAttachment.Controls.Add(this.LblAttachment2);
            this.TabAttachment.Controls.Add(this.PAttachment2);
            this.TabAttachment.Controls.Add(this.BtnAttachment1);
            this.TabAttachment.Controls.Add(this.LblAttachment1);
            this.TabAttachment.Controls.Add(this.PAttachment1);
            this.TabAttachment.Location = new System.Drawing.Point(4, 27);
            this.TabAttachment.Name = "TabAttachment";
            this.TabAttachment.Padding = new System.Windows.Forms.Padding(3);
            this.TabAttachment.Size = new System.Drawing.Size(1035, 212);
            this.TabAttachment.TabIndex = 1;
            this.TabAttachment.Text = "ATTACHMENT";
            // 
            // LinkAttachment5
            // 
            this.LinkAttachment5.AutoSize = true;
            this.LinkAttachment5.Location = new System.Drawing.Point(723, 100);
            this.LinkAttachment5.Name = "LinkAttachment5";
            this.LinkAttachment5.Size = new System.Drawing.Size(63, 19);
            this.LinkAttachment5.TabIndex = 379;
            this.LinkAttachment5.TabStop = true;
            this.LinkAttachment5.Text = "Preview";
            this.LinkAttachment5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkAttachment5_LinkClicked);
            // 
            // LinkAttachment4
            // 
            this.LinkAttachment4.AutoSize = true;
            this.LinkAttachment4.Location = new System.Drawing.Point(542, 101);
            this.LinkAttachment4.Name = "LinkAttachment4";
            this.LinkAttachment4.Size = new System.Drawing.Size(63, 19);
            this.LinkAttachment4.TabIndex = 378;
            this.LinkAttachment4.TabStop = true;
            this.LinkAttachment4.Text = "Preview";
            this.LinkAttachment4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkAttachment4_LinkClicked);
            // 
            // LinkAttachment3
            // 
            this.LinkAttachment3.AutoSize = true;
            this.LinkAttachment3.Location = new System.Drawing.Point(368, 102);
            this.LinkAttachment3.Name = "LinkAttachment3";
            this.LinkAttachment3.Size = new System.Drawing.Size(63, 19);
            this.LinkAttachment3.TabIndex = 377;
            this.LinkAttachment3.TabStop = true;
            this.LinkAttachment3.Text = "Preview";
            this.LinkAttachment3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkAttachment3_LinkClicked);
            // 
            // LinkAttachment2
            // 
            this.LinkAttachment2.AutoSize = true;
            this.LinkAttachment2.Location = new System.Drawing.Point(194, 103);
            this.LinkAttachment2.Name = "LinkAttachment2";
            this.LinkAttachment2.Size = new System.Drawing.Size(63, 19);
            this.LinkAttachment2.TabIndex = 376;
            this.LinkAttachment2.TabStop = true;
            this.LinkAttachment2.Text = "Preview";
            this.LinkAttachment2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkAttachment2_LinkClicked);
            // 
            // LinkAttachment1
            // 
            this.LinkAttachment1.AutoSize = true;
            this.LinkAttachment1.Location = new System.Drawing.Point(21, 101);
            this.LinkAttachment1.Name = "LinkAttachment1";
            this.LinkAttachment1.Size = new System.Drawing.Size(63, 19);
            this.LinkAttachment1.TabIndex = 375;
            this.LinkAttachment1.TabStop = true;
            this.LinkAttachment1.Text = "Preview";
            this.LinkAttachment1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkAttachment1_LinkClicked);
            // 
            // BtnAttachment5
            // 
            this.BtnAttachment5.Location = new System.Drawing.Point(746, 127);
            this.BtnAttachment5.Name = "BtnAttachment5";
            this.BtnAttachment5.Size = new System.Drawing.Size(116, 32);
            this.BtnAttachment5.TabIndex = 374;
            this.BtnAttachment5.Text = "Attachment ";
            this.BtnAttachment5.UseVisualStyleBackColor = true;
            this.BtnAttachment5.Click += new System.EventHandler(this.BtnAttachment5_Click);
            // 
            // LblAttachment5
            // 
            this.LblAttachment5.AutoSize = true;
            this.LblAttachment5.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAttachment5.Location = new System.Drawing.Point(759, 5);
            this.LblAttachment5.Name = "LblAttachment5";
            this.LblAttachment5.Size = new System.Drawing.Size(90, 19);
            this.LblAttachment5.TabIndex = 373;
            this.LblAttachment5.Text = "Attachment";
            // 
            // PAttachment5
            // 
            this.PAttachment5.Location = new System.Drawing.Point(718, 27);
            this.PAttachment5.Name = "PAttachment5";
            this.PAttachment5.Size = new System.Drawing.Size(173, 96);
            this.PAttachment5.TabIndex = 372;
            this.PAttachment5.TabStop = false;
            // 
            // BtnAttachment4
            // 
            this.BtnAttachment4.Location = new System.Drawing.Point(565, 128);
            this.BtnAttachment4.Name = "BtnAttachment4";
            this.BtnAttachment4.Size = new System.Drawing.Size(116, 32);
            this.BtnAttachment4.TabIndex = 371;
            this.BtnAttachment4.Text = "Attachment ";
            this.BtnAttachment4.UseVisualStyleBackColor = true;
            this.BtnAttachment4.Click += new System.EventHandler(this.BtnAttachment4_Click);
            // 
            // LblAttachment4
            // 
            this.LblAttachment4.AutoSize = true;
            this.LblAttachment4.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAttachment4.Location = new System.Drawing.Point(578, 5);
            this.LblAttachment4.Name = "LblAttachment4";
            this.LblAttachment4.Size = new System.Drawing.Size(90, 19);
            this.LblAttachment4.TabIndex = 370;
            this.LblAttachment4.Text = "Attachment";
            // 
            // PAttachment4
            // 
            this.PAttachment4.Location = new System.Drawing.Point(537, 27);
            this.PAttachment4.Name = "PAttachment4";
            this.PAttachment4.Size = new System.Drawing.Size(173, 96);
            this.PAttachment4.TabIndex = 369;
            this.PAttachment4.TabStop = false;
            // 
            // BtnAttachment3
            // 
            this.BtnAttachment3.Location = new System.Drawing.Point(392, 128);
            this.BtnAttachment3.Name = "BtnAttachment3";
            this.BtnAttachment3.Size = new System.Drawing.Size(116, 32);
            this.BtnAttachment3.TabIndex = 368;
            this.BtnAttachment3.Text = "Attachment ";
            this.BtnAttachment3.UseVisualStyleBackColor = true;
            this.BtnAttachment3.Click += new System.EventHandler(this.BtnAttachment3_Click);
            // 
            // LblAttachment3
            // 
            this.LblAttachment3.AutoSize = true;
            this.LblAttachment3.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAttachment3.Location = new System.Drawing.Point(405, 6);
            this.LblAttachment3.Name = "LblAttachment3";
            this.LblAttachment3.Size = new System.Drawing.Size(90, 19);
            this.LblAttachment3.TabIndex = 367;
            this.LblAttachment3.Text = "Attachment";
            // 
            // PAttachment3
            // 
            this.PAttachment3.Location = new System.Drawing.Point(363, 28);
            this.PAttachment3.Name = "PAttachment3";
            this.PAttachment3.Size = new System.Drawing.Size(173, 96);
            this.PAttachment3.TabIndex = 366;
            this.PAttachment3.TabStop = false;
            // 
            // BtnAttachment2
            // 
            this.BtnAttachment2.Location = new System.Drawing.Point(219, 128);
            this.BtnAttachment2.Name = "BtnAttachment2";
            this.BtnAttachment2.Size = new System.Drawing.Size(116, 32);
            this.BtnAttachment2.TabIndex = 365;
            this.BtnAttachment2.Text = "Attachment ";
            this.BtnAttachment2.UseVisualStyleBackColor = true;
            this.BtnAttachment2.Click += new System.EventHandler(this.BtnAttachment2_Click);
            // 
            // LblAttachment2
            // 
            this.LblAttachment2.AutoSize = true;
            this.LblAttachment2.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAttachment2.Location = new System.Drawing.Point(232, 6);
            this.LblAttachment2.Name = "LblAttachment2";
            this.LblAttachment2.Size = new System.Drawing.Size(90, 19);
            this.LblAttachment2.TabIndex = 364;
            this.LblAttachment2.Text = "Attachment";
            // 
            // PAttachment2
            // 
            this.PAttachment2.Location = new System.Drawing.Point(190, 28);
            this.PAttachment2.Name = "PAttachment2";
            this.PAttachment2.Size = new System.Drawing.Size(173, 96);
            this.PAttachment2.TabIndex = 363;
            this.PAttachment2.TabStop = false;
            // 
            // BtnAttachment1
            // 
            this.BtnAttachment1.Location = new System.Drawing.Point(46, 128);
            this.BtnAttachment1.Name = "BtnAttachment1";
            this.BtnAttachment1.Size = new System.Drawing.Size(124, 32);
            this.BtnAttachment1.TabIndex = 361;
            this.BtnAttachment1.Text = "Attachment ";
            this.BtnAttachment1.UseVisualStyleBackColor = true;
            this.BtnAttachment1.Click += new System.EventHandler(this.BtnAttachment1_Click);
            // 
            // LblAttachment1
            // 
            this.LblAttachment1.AutoSize = true;
            this.LblAttachment1.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAttachment1.Location = new System.Drawing.Point(59, 6);
            this.LblAttachment1.Name = "LblAttachment1";
            this.LblAttachment1.Size = new System.Drawing.Size(90, 19);
            this.LblAttachment1.TabIndex = 362;
            this.LblAttachment1.Text = "Attachment";
            // 
            // PAttachment1
            // 
            this.PAttachment1.Location = new System.Drawing.Point(18, 28);
            this.PAttachment1.Name = "PAttachment1";
            this.PAttachment1.Size = new System.Drawing.Size(170, 96);
            this.PAttachment1.TabIndex = 360;
            this.PAttachment1.TabStop = false;
            // 
            // TabAdditionalInformation
            // 
            this.TabAdditionalInformation.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.TabAdditionalInformation.Controls.Add(this.groupBox3);
            this.TabAdditionalInformation.Controls.Add(this.groupBox2);
            this.TabAdditionalInformation.Controls.Add(this.groupBox1);
            this.TabAdditionalInformation.Location = new System.Drawing.Point(4, 27);
            this.TabAdditionalInformation.Name = "TabAdditionalInformation";
            this.TabAdditionalInformation.Padding = new System.Windows.Forms.Padding(3);
            this.TabAdditionalInformation.Size = new System.Drawing.Size(1035, 212);
            this.TabAdditionalInformation.TabIndex = 2;
            this.TabAdditionalInformation.Text = "ADDITIONAL INFORMATION";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.TxtCustomName);
            this.groupBox3.Controls.Add(this.label49);
            this.groupBox3.Controls.Add(this.TxtLCNumber);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.MskVendorOrderDate);
            this.groupBox3.Controls.Add(this.MskExportInvoiceDate);
            this.groupBox3.Controls.Add(this.MskContractNoDate);
            this.groupBox3.Controls.Add(this.TxtContractNo);
            this.groupBox3.Controls.Add(this.TxtBankDetails);
            this.groupBox3.Controls.Add(this.label32);
            this.groupBox3.Controls.Add(this.label34);
            this.groupBox3.Controls.Add(this.label36);
            this.groupBox3.Controls.Add(this.TxtVendorOrderNo);
            this.groupBox3.Controls.Add(this.TxtExportInvoiceNo);
            this.groupBox3.Controls.Add(this.label37);
            this.groupBox3.Location = new System.Drawing.Point(637, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(399, 187);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Export Details";
            // 
            // TxtCustomName
            // 
            this.TxtCustomName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCustomName.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtCustomName.Location = new System.Drawing.Point(124, 151);
            this.TxtCustomName.MaxLength = 255;
            this.TxtCustomName.Name = "TxtCustomName";
            this.TxtCustomName.Size = new System.Drawing.Size(266, 25);
            this.TxtCustomName.TabIndex = 8;
            this.TxtCustomName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label49.Location = new System.Drawing.Point(3, 154);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(66, 19);
            this.label49.TabIndex = 332;
            this.label49.Text = "Custom";
            // 
            // TxtLCNumber
            // 
            this.TxtLCNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtLCNumber.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtLCNumber.Location = new System.Drawing.Point(124, 124);
            this.TxtLCNumber.MaxLength = 255;
            this.TxtLCNumber.Name = "TxtLCNumber";
            this.TxtLCNumber.Size = new System.Drawing.Size(266, 25);
            this.TxtLCNumber.TabIndex = 7;
            this.TxtLCNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label12.Location = new System.Drawing.Point(3, 127);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(102, 19);
            this.label12.TabIndex = 330;
            this.label12.Text = "L/C Number";
            // 
            // MskVendorOrderDate
            // 
            this.MskVendorOrderDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskVendorOrderDate.Enabled = false;
            this.MskVendorOrderDate.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskVendorOrderDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskVendorOrderDate.Location = new System.Drawing.Point(275, 72);
            this.MskVendorOrderDate.Mask = "00/00/0000";
            this.MskVendorOrderDate.Name = "MskVendorOrderDate";
            this.MskVendorOrderDate.Size = new System.Drawing.Size(115, 24);
            this.MskVendorOrderDate.TabIndex = 5;
            // 
            // MskExportInvoiceDate
            // 
            this.MskExportInvoiceDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskExportInvoiceDate.Enabled = false;
            this.MskExportInvoiceDate.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskExportInvoiceDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskExportInvoiceDate.Location = new System.Drawing.Point(275, 45);
            this.MskExportInvoiceDate.Mask = "00/00/0000";
            this.MskExportInvoiceDate.Name = "MskExportInvoiceDate";
            this.MskExportInvoiceDate.Size = new System.Drawing.Size(115, 24);
            this.MskExportInvoiceDate.TabIndex = 3;
            // 
            // MskContractNoDate
            // 
            this.MskContractNoDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskContractNoDate.Enabled = false;
            this.MskContractNoDate.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskContractNoDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskContractNoDate.Location = new System.Drawing.Point(275, 19);
            this.MskContractNoDate.Mask = "00/00/0000";
            this.MskContractNoDate.Name = "MskContractNoDate";
            this.MskContractNoDate.Size = new System.Drawing.Size(115, 24);
            this.MskContractNoDate.TabIndex = 1;
            // 
            // TxtContractNo
            // 
            this.TxtContractNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtContractNo.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtContractNo.Location = new System.Drawing.Point(124, 19);
            this.TxtContractNo.MaxLength = 255;
            this.TxtContractNo.Name = "TxtContractNo";
            this.TxtContractNo.Size = new System.Drawing.Size(146, 25);
            this.TxtContractNo.TabIndex = 0;
            this.TxtContractNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtBankDetails
            // 
            this.TxtBankDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBankDetails.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtBankDetails.Location = new System.Drawing.Point(124, 98);
            this.TxtBankDetails.MaxLength = 255;
            this.TxtBankDetails.Name = "TxtBankDetails";
            this.TxtBankDetails.Size = new System.Drawing.Size(266, 25);
            this.TxtBankDetails.TabIndex = 6;
            this.TxtBankDetails.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label32.Location = new System.Drawing.Point(3, 21);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(98, 19);
            this.label32.TabIndex = 312;
            this.label32.Text = "Contract No";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label34.Location = new System.Drawing.Point(3, 101);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(107, 19);
            this.label34.TabIndex = 318;
            this.label34.Text = "Bank Details";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label36.Location = new System.Drawing.Point(3, 48);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(113, 19);
            this.label36.TabIndex = 314;
            this.label36.Text = "Export Invoice";
            // 
            // TxtVendorOrderNo
            // 
            this.TxtVendorOrderNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtVendorOrderNo.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtVendorOrderNo.Location = new System.Drawing.Point(124, 72);
            this.TxtVendorOrderNo.MaxLength = 255;
            this.TxtVendorOrderNo.Name = "TxtVendorOrderNo";
            this.TxtVendorOrderNo.Size = new System.Drawing.Size(146, 25);
            this.TxtVendorOrderNo.TabIndex = 4;
            this.TxtVendorOrderNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtExportInvoiceNo
            // 
            this.TxtExportInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtExportInvoiceNo.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtExportInvoiceNo.Location = new System.Drawing.Point(124, 45);
            this.TxtExportInvoiceNo.MaxLength = 255;
            this.TxtExportInvoiceNo.Name = "TxtExportInvoiceNo";
            this.TxtExportInvoiceNo.Size = new System.Drawing.Size(146, 25);
            this.TxtExportInvoiceNo.TabIndex = 2;
            this.TxtExportInvoiceNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label37.Location = new System.Drawing.Point(3, 75);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(77, 19);
            this.label37.TabIndex = 316;
            this.label37.Text = "Order No";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TxtDriver);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.TxtLicenseNo);
            this.groupBox2.Controls.Add(this.TxtPhoneNo);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Location = new System.Drawing.Point(333, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(299, 104);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Driver Info";
            // 
            // TxtDriver
            // 
            this.TxtDriver.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDriver.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtDriver.Location = new System.Drawing.Point(96, 21);
            this.TxtDriver.MaxLength = 255;
            this.TxtDriver.Name = "TxtDriver";
            this.TxtDriver.Size = new System.Drawing.Size(200, 25);
            this.TxtDriver.TabIndex = 0;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label13.Location = new System.Drawing.Point(3, 21);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 19);
            this.label13.TabIndex = 312;
            this.label13.Text = "Drivers";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label15.Location = new System.Drawing.Point(3, 48);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(79, 19);
            this.label15.TabIndex = 314;
            this.label15.Text = "Phone No";
            // 
            // TxtLicenseNo
            // 
            this.TxtLicenseNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtLicenseNo.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtLicenseNo.Location = new System.Drawing.Point(96, 74);
            this.TxtLicenseNo.MaxLength = 255;
            this.TxtLicenseNo.Name = "TxtLicenseNo";
            this.TxtLicenseNo.Size = new System.Drawing.Size(200, 25);
            this.TxtLicenseNo.TabIndex = 2;
            // 
            // TxtPhoneNo
            // 
            this.TxtPhoneNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPhoneNo.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtPhoneNo.Location = new System.Drawing.Point(96, 47);
            this.TxtPhoneNo.MaxLength = 255;
            this.TxtPhoneNo.Name = "TxtPhoneNo";
            this.TxtPhoneNo.Size = new System.Drawing.Size(200, 25);
            this.TxtPhoneNo.TabIndex = 1;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label16.Location = new System.Drawing.Point(3, 75);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(91, 19);
            this.label16.TabIndex = 316;
            this.label16.Text = "License No";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CmbBiltyType);
            this.groupBox1.Controls.Add(this.label43);
            this.groupBox1.Controls.Add(this.MskBiltyDate);
            this.groupBox1.Controls.Add(this.label42);
            this.groupBox1.Controls.Add(this.TxtTransport);
            this.groupBox1.Controls.Add(this.TxtPackage);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.label35);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.TxtBiltyNo);
            this.groupBox1.Controls.Add(this.TxtVechileNo);
            this.groupBox1.Controls.Add(this.label33);
            this.groupBox1.Location = new System.Drawing.Point(6, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(324, 188);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Transport Details";
            // 
            // CmbBiltyType
            // 
            this.CmbBiltyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbBiltyType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbBiltyType.Font = new System.Drawing.Font("Bookman Old Style", 9F);
            this.CmbBiltyType.FormattingEnabled = true;
            this.CmbBiltyType.Items.AddRange(new object[] {
            "DUE",
            "TO PAY",
            "PAID",
            "FREE"});
            this.CmbBiltyType.Location = new System.Drawing.Point(96, 156);
            this.CmbBiltyType.Name = "CmbBiltyType";
            this.CmbBiltyType.Size = new System.Drawing.Size(220, 24);
            this.CmbBiltyType.TabIndex = 5;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label43.Location = new System.Drawing.Point(3, 158);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(83, 19);
            this.label43.TabIndex = 325;
            this.label43.Text = "Bilty Type";
            // 
            // MskBiltyDate
            // 
            this.MskBiltyDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskBiltyDate.Enabled = false;
            this.MskBiltyDate.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskBiltyDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskBiltyDate.Location = new System.Drawing.Point(96, 128);
            this.MskBiltyDate.Mask = "00/00/0000";
            this.MskBiltyDate.Name = "MskBiltyDate";
            this.MskBiltyDate.Size = new System.Drawing.Size(115, 24);
            this.MskBiltyDate.TabIndex = 4;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label42.Location = new System.Drawing.Point(3, 131);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(85, 19);
            this.label42.TabIndex = 319;
            this.label42.Text = "Bilty Date";
            // 
            // TxtTransport
            // 
            this.TxtTransport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTransport.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtTransport.Location = new System.Drawing.Point(96, 21);
            this.TxtTransport.MaxLength = 255;
            this.TxtTransport.Name = "TxtTransport";
            this.TxtTransport.Size = new System.Drawing.Size(220, 25);
            this.TxtTransport.TabIndex = 0;
            // 
            // TxtPackage
            // 
            this.TxtPackage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPackage.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtPackage.Location = new System.Drawing.Point(96, 100);
            this.TxtPackage.MaxLength = 255;
            this.TxtPackage.Name = "TxtPackage";
            this.TxtPackage.Size = new System.Drawing.Size(220, 25);
            this.TxtPackage.TabIndex = 3;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label23.Location = new System.Drawing.Point(3, 21);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(81, 19);
            this.label23.TabIndex = 312;
            this.label23.Text = "Transport";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label35.Location = new System.Drawing.Point(3, 101);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(70, 19);
            this.label35.TabIndex = 318;
            this.label35.Text = "Package";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label27.Location = new System.Drawing.Point(3, 48);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(85, 19);
            this.label27.TabIndex = 314;
            this.label27.Text = "VechileNo";
            // 
            // TxtBiltyNo
            // 
            this.TxtBiltyNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBiltyNo.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtBiltyNo.Location = new System.Drawing.Point(96, 74);
            this.TxtBiltyNo.MaxLength = 255;
            this.TxtBiltyNo.Name = "TxtBiltyNo";
            this.TxtBiltyNo.Size = new System.Drawing.Size(220, 25);
            this.TxtBiltyNo.TabIndex = 2;
            // 
            // TxtVechileNo
            // 
            this.TxtVechileNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtVechileNo.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtVechileNo.Location = new System.Drawing.Point(96, 47);
            this.TxtVechileNo.MaxLength = 255;
            this.TxtVechileNo.Name = "TxtVechileNo";
            this.TxtVechileNo.Size = new System.Drawing.Size(220, 25);
            this.TxtVechileNo.TabIndex = 1;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label33.Location = new System.Drawing.Point(3, 75);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(68, 19);
            this.label33.TabIndex = 316;
            this.label33.Text = "Bilty No";
            // 
            // TabMailingAddress
            // 
            this.TabMailingAddress.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.TabMailingAddress.Controls.Add(this.groupBox5);
            this.TabMailingAddress.Controls.Add(this.groupBox4);
            this.TabMailingAddress.Location = new System.Drawing.Point(4, 27);
            this.TabMailingAddress.Name = "TabMailingAddress";
            this.TabMailingAddress.Padding = new System.Windows.Forms.Padding(3);
            this.TabMailingAddress.Size = new System.Drawing.Size(1035, 212);
            this.TabMailingAddress.TabIndex = 3;
            this.TabMailingAddress.Text = "INVOICE ADDRESS";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.TxtSEmail);
            this.groupBox5.Controls.Add(this.label44);
            this.groupBox5.Controls.Add(this.TxtShippingAddress);
            this.groupBox5.Controls.Add(this.TxtSCountry);
            this.groupBox5.Controls.Add(this.label45);
            this.groupBox5.Controls.Add(this.label46);
            this.groupBox5.Controls.Add(this.label47);
            this.groupBox5.Controls.Add(this.TxtSState);
            this.groupBox5.Controls.Add(this.TxtSCity);
            this.groupBox5.Controls.Add(this.label48);
            this.groupBox5.Location = new System.Drawing.Point(487, 1);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(474, 196);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Shipping Address";
            // 
            // TxtSEmail
            // 
            this.TxtSEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSEmail.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtSEmail.Location = new System.Drawing.Point(96, 164);
            this.TxtSEmail.MaxLength = 255;
            this.TxtSEmail.Name = "TxtSEmail";
            this.TxtSEmail.Size = new System.Drawing.Size(220, 25);
            this.TxtSEmail.TabIndex = 4;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label44.Location = new System.Drawing.Point(3, 166);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(52, 19);
            this.label44.TabIndex = 320;
            this.label44.Text = "Email";
            // 
            // TxtShippingAddress
            // 
            this.TxtShippingAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtShippingAddress.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtShippingAddress.Location = new System.Drawing.Point(96, 21);
            this.TxtShippingAddress.MaxLength = 255;
            this.TxtShippingAddress.Multiline = true;
            this.TxtShippingAddress.Name = "TxtShippingAddress";
            this.TxtShippingAddress.Size = new System.Drawing.Size(372, 59);
            this.TxtShippingAddress.TabIndex = 0;
            // 
            // TxtSCountry
            // 
            this.TxtSCountry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSCountry.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtSCountry.Location = new System.Drawing.Point(96, 137);
            this.TxtSCountry.MaxLength = 255;
            this.TxtSCountry.Name = "TxtSCountry";
            this.TxtSCountry.Size = new System.Drawing.Size(220, 25);
            this.TxtSCountry.TabIndex = 3;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label45.Location = new System.Drawing.Point(3, 35);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(69, 19);
            this.label45.TabIndex = 312;
            this.label45.Text = "Address";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label46.Location = new System.Drawing.Point(3, 138);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(69, 19);
            this.label46.TabIndex = 318;
            this.label46.Text = "Country";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label47.Location = new System.Drawing.Point(3, 87);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(39, 19);
            this.label47.TabIndex = 314;
            this.label47.Text = "City";
            // 
            // TxtSState
            // 
            this.TxtSState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSState.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtSState.Location = new System.Drawing.Point(96, 111);
            this.TxtSState.MaxLength = 255;
            this.TxtSState.Name = "TxtSState";
            this.TxtSState.Size = new System.Drawing.Size(220, 25);
            this.TxtSState.TabIndex = 2;
            // 
            // TxtSCity
            // 
            this.TxtSCity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSCity.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtSCity.Location = new System.Drawing.Point(96, 84);
            this.TxtSCity.MaxLength = 255;
            this.TxtSCity.Name = "TxtSCity";
            this.TxtSCity.Size = new System.Drawing.Size(220, 25);
            this.TxtSCity.TabIndex = 1;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label48.Location = new System.Drawing.Point(3, 112);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(49, 19);
            this.label48.TabIndex = 316;
            this.label48.Text = "State";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.TxtMEmail);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.TxtMailingAddress);
            this.groupBox4.Controls.Add(this.TxtMCountry);
            this.groupBox4.Controls.Add(this.label38);
            this.groupBox4.Controls.Add(this.label39);
            this.groupBox4.Controls.Add(this.label40);
            this.groupBox4.Controls.Add(this.TxtMState);
            this.groupBox4.Controls.Add(this.TxtMCity);
            this.groupBox4.Controls.Add(this.label41);
            this.groupBox4.Location = new System.Drawing.Point(6, 1);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(474, 196);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Mailing Address";
            // 
            // TxtMEmail
            // 
            this.TxtMEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMEmail.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtMEmail.Location = new System.Drawing.Point(96, 164);
            this.TxtMEmail.MaxLength = 255;
            this.TxtMEmail.Name = "TxtMEmail";
            this.TxtMEmail.Size = new System.Drawing.Size(220, 25);
            this.TxtMEmail.TabIndex = 4;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label14.Location = new System.Drawing.Point(3, 166);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(52, 19);
            this.label14.TabIndex = 320;
            this.label14.Text = "Email";
            // 
            // TxtMailingAddress
            // 
            this.TxtMailingAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMailingAddress.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtMailingAddress.Location = new System.Drawing.Point(96, 21);
            this.TxtMailingAddress.MaxLength = 255;
            this.TxtMailingAddress.Multiline = true;
            this.TxtMailingAddress.Name = "TxtMailingAddress";
            this.TxtMailingAddress.Size = new System.Drawing.Size(372, 59);
            this.TxtMailingAddress.TabIndex = 0;
            // 
            // TxtMCountry
            // 
            this.TxtMCountry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMCountry.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtMCountry.Location = new System.Drawing.Point(96, 137);
            this.TxtMCountry.MaxLength = 255;
            this.TxtMCountry.Name = "TxtMCountry";
            this.TxtMCountry.Size = new System.Drawing.Size(220, 25);
            this.TxtMCountry.TabIndex = 3;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label38.Location = new System.Drawing.Point(3, 35);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(69, 19);
            this.label38.TabIndex = 312;
            this.label38.Text = "Address";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label39.Location = new System.Drawing.Point(3, 138);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(69, 19);
            this.label39.TabIndex = 318;
            this.label39.Text = "Country";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label40.Location = new System.Drawing.Point(3, 87);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(39, 19);
            this.label40.TabIndex = 314;
            this.label40.Text = "City";
            // 
            // TxtMState
            // 
            this.TxtMState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMState.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtMState.Location = new System.Drawing.Point(96, 111);
            this.TxtMState.MaxLength = 255;
            this.TxtMState.Name = "TxtMState";
            this.TxtMState.Size = new System.Drawing.Size(220, 25);
            this.TxtMState.TabIndex = 2;
            // 
            // TxtMCity
            // 
            this.TxtMCity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMCity.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtMCity.Location = new System.Drawing.Point(96, 84);
            this.TxtMCity.MaxLength = 255;
            this.TxtMCity.Name = "TxtMCity";
            this.TxtMCity.Size = new System.Drawing.Size(220, 25);
            this.TxtMCity.TabIndex = 1;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label41.Location = new System.Drawing.Point(3, 112);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(49, 19);
            this.label41.TabIndex = 316;
            this.label41.Text = "State";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.MskOrderMiti);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.MskOrderDate);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.MskChallanMiti);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.MskChallanDate);
            this.panel1.Controls.Add(this.BtnReturnInvoice);
            this.panel1.Controls.Add(this.clsSeparator2);
            this.panel1.Controls.Add(this.clsSeparator1);
            this.panel1.Controls.Add(this.BtnCancel);
            this.panel1.Controls.Add(this.lbl_Remarks);
            this.panel1.Controls.Add(this.TxtRemarks);
            this.panel1.Controls.Add(this.lbl_NoInWords);
            this.panel1.Controls.Add(this.LblNumberInWords);
            this.panel1.Controls.Add(this.BtnSave);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.BtnCurrency);
            this.panel1.Controls.Add(this.label31);
            this.panel1.Controls.Add(this.BtnChallanNo);
            this.panel1.Controls.Add(this.TxtChallan);
            this.panel1.Controls.Add(this.BtnVendor);
            this.panel1.Controls.Add(this.TxtOrder);
            this.panel1.Controls.Add(this.BtnOrderNo);
            this.panel1.Controls.Add(this.TxtDepartment);
            this.panel1.Controls.Add(this.BtnDepartment);
            this.panel1.Controls.Add(this.TxtCustomer);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.TxtDueDays);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.CmbInvoiceType);
            this.panel1.Controls.Add(this.TxtAgent);
            this.panel1.Controls.Add(this.BtnVno);
            this.panel1.Controls.Add(this.CmbPaymentMode);
            this.panel1.Controls.Add(this.BtnAgent);
            this.panel1.Controls.Add(this.label30);
            this.panel1.Controls.Add(this.TxtSubledger);
            this.panel1.Controls.Add(this.TxtVno);
            this.panel1.Controls.Add(this.BtnSubledger);
            this.panel1.Controls.Add(this.label29);
            this.panel1.Controls.Add(this.TxtRefVno);
            this.panel1.Controls.Add(this.MskMiti);
            this.panel1.Controls.Add(this.MskDueDays);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.MskDate);
            this.panel1.Controls.Add(this.TxtCurrencyRate);
            this.panel1.Controls.Add(this.label28);
            this.panel1.Controls.Add(this.lbl_Currency);
            this.panel1.Controls.Add(this.MskRefDate);
            this.panel1.Controls.Add(this.lbl_CurrencyRate);
            this.panel1.Controls.Add(this.groupBox6);
            this.panel1.Controls.Add(this.BtnNew);
            this.panel1.Controls.Add(this.BtnEdit);
            this.panel1.Controls.Add(this.BtnDelete);
            this.panel1.Controls.Add(this.BtnPrintInvoice);
            this.panel1.Controls.Add(this.BtnReverse);
            this.panel1.Controls.Add(this.BtnCopy);
            this.panel1.Controls.Add(this.BtnExit);
            this.panel1.Controls.Add(this.BtnPurchaseInvoice);
            this.panel1.Controls.Add(this.TabLedgerOpening);
            this.panel1.Controls.Add(this.lbl_VoucherNo);
            this.panel1.Controls.Add(this.lbl_Class);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lbl_RefVoucherNo);
            this.panel1.Controls.Add(this.lbl_CBLedger);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.TxtCurrency);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1054, 567);
            this.panel1.TabIndex = 0;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label17.Location = new System.Drawing.Point(401, 92);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(45, 19);
            this.label17.TabIndex = 583;
            this.label17.Text = "Date";
            // 
            // MskOrderMiti
            // 
            this.MskOrderMiti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskOrderMiti.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.MskOrderMiti.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskOrderMiti.Location = new System.Drawing.Point(289, 88);
            this.MskOrderMiti.Mask = "00/00/0000";
            this.MskOrderMiti.Name = "MskOrderMiti";
            this.MskOrderMiti.Size = new System.Drawing.Size(95, 24);
            this.MskOrderMiti.TabIndex = 18;
            this.MskOrderMiti.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label18.Location = new System.Drawing.Point(251, 92);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(38, 19);
            this.label18.TabIndex = 584;
            this.label18.Text = "Miti";
            // 
            // MskOrderDate
            // 
            this.MskOrderDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskOrderDate.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.MskOrderDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskOrderDate.Location = new System.Drawing.Point(453, 88);
            this.MskOrderDate.Mask = "00/00/0000";
            this.MskOrderDate.Name = "MskOrderDate";
            this.MskOrderDate.Size = new System.Drawing.Size(100, 24);
            this.MskOrderDate.TabIndex = 19;
            this.MskOrderDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label4.Location = new System.Drawing.Point(401, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 19);
            this.label4.TabIndex = 579;
            this.label4.Text = "Date";
            // 
            // MskChallanMiti
            // 
            this.MskChallanMiti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskChallanMiti.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.MskChallanMiti.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskChallanMiti.Location = new System.Drawing.Point(289, 62);
            this.MskChallanMiti.Mask = "00/00/0000";
            this.MskChallanMiti.Name = "MskChallanMiti";
            this.MskChallanMiti.Size = new System.Drawing.Size(95, 24);
            this.MskChallanMiti.TabIndex = 15;
            this.MskChallanMiti.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label8.Location = new System.Drawing.Point(251, 66);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 19);
            this.label8.TabIndex = 580;
            this.label8.Text = "Miti";
            // 
            // MskChallanDate
            // 
            this.MskChallanDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskChallanDate.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.MskChallanDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskChallanDate.Location = new System.Drawing.Point(453, 62);
            this.MskChallanDate.Mask = "00/00/0000";
            this.MskChallanDate.Name = "MskChallanDate";
            this.MskChallanDate.Size = new System.Drawing.Size(100, 24);
            this.MskChallanDate.TabIndex = 16;
            this.MskChallanDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // BtnReturnInvoice
            // 
            this.BtnReturnInvoice.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnReturnInvoice.Appearance.Options.UseFont = true;
            this.BtnReturnInvoice.ImageOptions.Image = global::MrBLL.Properties.Resources.Return;
            this.BtnReturnInvoice.Location = new System.Drawing.Point(452, 2);
            this.BtnReturnInvoice.Name = "BtnReturnInvoice";
            this.BtnReturnInvoice.Size = new System.Drawing.Size(108, 30);
            this.BtnReturnInvoice.TabIndex = 3;
            this.BtnReturnInvoice.Text = "&RETURN";
            this.BtnReturnInvoice.Click += new System.EventHandler(this.BtnReturnInvoice_Click);
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(7, 33);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(1041, 2);
            this.clsSeparator2.TabIndex = 575;
            this.clsSeparator2.TabStop = false;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(8, 219);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(1041, 2);
            this.clsSeparator1.TabIndex = 31;
            this.clsSeparator1.TabStop = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(945, 531);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(104, 32);
            this.BtnCancel.TabIndex = 34;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // lbl_Remarks
            // 
            this.lbl_Remarks.AutoSize = true;
            this.lbl_Remarks.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Remarks.Location = new System.Drawing.Point(5, 507);
            this.lbl_Remarks.Name = "lbl_Remarks";
            this.lbl_Remarks.Size = new System.Drawing.Size(76, 19);
            this.lbl_Remarks.TabIndex = 571;
            this.lbl_Remarks.Text = "Remarks";
            // 
            // TxtRemarks
            // 
            this.TxtRemarks.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtRemarks.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtRemarks.Location = new System.Drawing.Point(97, 504);
            this.TxtRemarks.MaxLength = 255;
            this.TxtRemarks.Name = "TxtRemarks";
            this.TxtRemarks.Size = new System.Drawing.Size(949, 24);
            this.TxtRemarks.TabIndex = 32;
            this.TxtRemarks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtRemarks_KeyDown);
            // 
            // lbl_NoInWords
            // 
            this.lbl_NoInWords.AutoSize = true;
            this.lbl_NoInWords.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NoInWords.Location = new System.Drawing.Point(5, 538);
            this.lbl_NoInWords.Name = "lbl_NoInWords";
            this.lbl_NoInWords.Size = new System.Drawing.Size(91, 19);
            this.lbl_NoInWords.TabIndex = 572;
            this.lbl_NoInWords.Text = "In Words :-";
            // 
            // LblNumberInWords
            // 
            this.LblNumberInWords.BackColor = System.Drawing.Color.White;
            this.LblNumberInWords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblNumberInWords.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNumberInWords.ForeColor = System.Drawing.SystemColors.Desktop;
            this.LblNumberInWords.Location = new System.Drawing.Point(97, 532);
            this.LblNumberInWords.Name = "LblNumberInWords";
            this.LblNumberInWords.Size = new System.Drawing.Size(756, 29);
            this.LblNumberInWords.TabIndex = 573;
            this.LblNumberInWords.Text = "Only.";
            this.LblNumberInWords.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(857, 531);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(87, 32);
            this.BtnSave.TabIndex = 33;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.LblTotalAltQty);
            this.panel2.Controls.Add(this.TxtBillTermAmount);
            this.panel2.Controls.Add(this.label24);
            this.panel2.Controls.Add(this.LblTotalBasicAmount);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.BtnBillingTerm);
            this.panel2.Controls.Add(this.LblTotalNetAmount);
            this.panel2.Controls.Add(this.LblTotalLocalNetAmount);
            this.panel2.Controls.Add(this.LblTotalQty);
            this.panel2.Controls.Add(this.label26);
            this.panel2.Controls.Add(this.lbl_TotQty1);
            this.panel2.Controls.Add(this.lbl_TotAltQty1);
            this.panel2.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(4, 466);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1043, 35);
            this.panel2.TabIndex = 32;
            // 
            // LblTotalAltQty
            // 
            this.LblTotalAltQty.BackColor = System.Drawing.SystemColors.HighlightText;
            this.LblTotalAltQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTotalAltQty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblTotalAltQty.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotalAltQty.Location = new System.Drawing.Point(69, 6);
            this.LblTotalAltQty.Name = "LblTotalAltQty";
            this.LblTotalAltQty.Size = new System.Drawing.Size(70, 24);
            this.LblTotalAltQty.TabIndex = 241;
            this.LblTotalAltQty.Text = "0.00";
            this.LblTotalAltQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtBillTermAmount
            // 
            this.TxtBillTermAmount.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.TxtBillTermAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBillTermAmount.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBillTermAmount.Location = new System.Drawing.Point(535, 6);
            this.TxtBillTermAmount.MaxLength = 18;
            this.TxtBillTermAmount.Name = "TxtBillTermAmount";
            this.TxtBillTermAmount.ReadOnly = true;
            this.TxtBillTermAmount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtBillTermAmount.Size = new System.Drawing.Size(73, 23);
            this.TxtBillTermAmount.TabIndex = 0;
            this.TxtBillTermAmount.Text = "0.00";
            this.TxtBillTermAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtBillTermAmount.TextChanged += new System.EventHandler(this.TxtBillTermAmount_TextChanged);
            this.TxtBillTermAmount.Enter += new System.EventHandler(this.TxtBillTermAmount_Enter);
            this.TxtBillTermAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBillTermAmount_KeyDown);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(636, 8);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(83, 19);
            this.label24.TabIndex = 1;
            this.label24.Text = "Total AMT";
            // 
            // LblTotalBasicAmount
            // 
            this.LblTotalBasicAmount.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LblTotalBasicAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTotalBasicAmount.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotalBasicAmount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblTotalBasicAmount.Location = new System.Drawing.Point(351, 6);
            this.LblTotalBasicAmount.Name = "LblTotalBasicAmount";
            this.LblTotalBasicAmount.Size = new System.Drawing.Size(101, 24);
            this.LblTotalBasicAmount.TabIndex = 226;
            this.LblTotalBasicAmount.Text = "0.00";
            this.LblTotalBasicAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(266, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 19);
            this.label3.TabIndex = 227;
            this.label3.Text = "Basic Amt";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(452, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 19);
            this.label5.TabIndex = 229;
            this.label5.Text = "Term Amt";
            // 
            // BtnBillingTerm
            // 
            this.BtnBillingTerm.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBillingTerm.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnBillingTerm.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnBillingTerm.Location = new System.Drawing.Point(608, 6);
            this.BtnBillingTerm.Name = "BtnBillingTerm";
            this.BtnBillingTerm.Size = new System.Drawing.Size(28, 24);
            this.BtnBillingTerm.TabIndex = 33;
            this.BtnBillingTerm.TabStop = false;
            this.BtnBillingTerm.UseVisualStyleBackColor = false;
            this.BtnBillingTerm.Click += new System.EventHandler(this.BtnBillingTerm_Click);
            // 
            // LblTotalNetAmount
            // 
            this.LblTotalNetAmount.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LblTotalNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTotalNetAmount.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotalNetAmount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblTotalNetAmount.Location = new System.Drawing.Point(719, 6);
            this.LblTotalNetAmount.Name = "LblTotalNetAmount";
            this.LblTotalNetAmount.Size = new System.Drawing.Size(114, 24);
            this.LblTotalNetAmount.TabIndex = 236;
            this.LblTotalNetAmount.Text = "0.00";
            this.LblTotalNetAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblTotalLocalNetAmount
            // 
            this.LblTotalLocalNetAmount.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LblTotalLocalNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTotalLocalNetAmount.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotalLocalNetAmount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblTotalLocalNetAmount.Location = new System.Drawing.Point(899, 6);
            this.LblTotalLocalNetAmount.Name = "LblTotalLocalNetAmount";
            this.LblTotalLocalNetAmount.Size = new System.Drawing.Size(135, 24);
            this.LblTotalLocalNetAmount.TabIndex = 238;
            this.LblTotalLocalNetAmount.Text = "0.00";
            this.LblTotalLocalNetAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblTotalQty
            // 
            this.LblTotalQty.BackColor = System.Drawing.SystemColors.HighlightText;
            this.LblTotalQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTotalQty.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotalQty.Location = new System.Drawing.Point(179, 6);
            this.LblTotalQty.Name = "LblTotalQty";
            this.LblTotalQty.Size = new System.Drawing.Size(87, 24);
            this.LblTotalQty.TabIndex = 243;
            this.LblTotalQty.Text = "0.00";
            this.LblTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(833, 8);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(66, 19);
            this.label26.TabIndex = 239;
            this.label26.Text = "LN AMT";
            // 
            // lbl_TotQty1
            // 
            this.lbl_TotQty1.AutoSize = true;
            this.lbl_TotQty1.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TotQty1.Location = new System.Drawing.Point(139, 8);
            this.lbl_TotQty1.Name = "lbl_TotQty1";
            this.lbl_TotQty1.Size = new System.Drawing.Size(40, 19);
            this.lbl_TotQty1.TabIndex = 242;
            this.lbl_TotQty1.Text = " Qty";
            // 
            // lbl_TotAltQty1
            // 
            this.lbl_TotAltQty1.AutoSize = true;
            this.lbl_TotAltQty1.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TotAltQty1.Location = new System.Drawing.Point(8, 8);
            this.lbl_TotAltQty1.Name = "lbl_TotAltQty1";
            this.lbl_TotAltQty1.Size = new System.Drawing.Size(61, 19);
            this.lbl_TotAltQty1.TabIndex = 0;
            this.lbl_TotAltQty1.Text = "Alt Qty";
            // 
            // TxtCurrency
            // 
            this.TxtCurrency.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtCurrency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCurrency.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCurrency.Location = new System.Drawing.Point(566, 139);
            this.TxtCurrency.MaxLength = 255;
            this.TxtCurrency.Name = "TxtCurrency";
            this.TxtCurrency.ReadOnly = true;
            this.TxtCurrency.Size = new System.Drawing.Size(152, 25);
            this.TxtCurrency.TabIndex = 29;
            this.TxtCurrency.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCurrency_KeyDown);
            // 
            // BtnCurrency
            // 
            this.BtnCurrency.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCurrency.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnCurrency.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnCurrency.Location = new System.Drawing.Point(692, 138);
            this.BtnCurrency.Name = "BtnCurrency";
            this.BtnCurrency.Size = new System.Drawing.Size(26, 26);
            this.BtnCurrency.TabIndex = 566;
            this.BtnCurrency.TabStop = false;
            this.BtnCurrency.UseVisualStyleBackColor = false;
            this.BtnCurrency.Visible = false;
            this.BtnCurrency.Click += new System.EventHandler(this.BtnCurrency_Click);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(784, 66);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(55, 19);
            this.label31.TabIndex = 563;
            this.label31.Text = "Bill In";
            // 
            // BtnChallanNo
            // 
            this.BtnChallanNo.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.BtnChallanNo.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnChallanNo.Location = new System.Drawing.Point(225, 62);
            this.BtnChallanNo.Name = "BtnChallanNo";
            this.BtnChallanNo.Size = new System.Drawing.Size(26, 24);
            this.BtnChallanNo.TabIndex = 526;
            this.BtnChallanNo.TabStop = false;
            this.BtnChallanNo.UseVisualStyleBackColor = false;
            this.BtnChallanNo.Click += new System.EventHandler(this.BtnChallan_Click);
            // 
            // TxtChallan
            // 
            this.TxtChallan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtChallan.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.TxtChallan.Location = new System.Drawing.Point(100, 62);
            this.TxtChallan.MaxLength = 50;
            this.TxtChallan.Name = "TxtChallan";
            this.TxtChallan.Size = new System.Drawing.Size(124, 24);
            this.TxtChallan.TabIndex = 14;
            this.TxtChallan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtChallan_KeyDown);
            this.TxtChallan.Validating += new System.ComponentModel.CancelEventHandler(this.TxtChallan_Validating);
            // 
            // BtnVendor
            // 
            this.BtnVendor.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.BtnVendor.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnVendor.Location = new System.Drawing.Point(456, 115);
            this.BtnVendor.Name = "BtnVendor";
            this.BtnVendor.Size = new System.Drawing.Size(26, 24);
            this.BtnVendor.TabIndex = 552;
            this.BtnVendor.TabStop = false;
            this.BtnVendor.UseVisualStyleBackColor = false;
            this.BtnVendor.Click += new System.EventHandler(this.BtnVendor_Click);
            // 
            // TxtOrder
            // 
            this.TxtOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtOrder.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.TxtOrder.Location = new System.Drawing.Point(100, 88);
            this.TxtOrder.MaxLength = 50;
            this.TxtOrder.Name = "TxtOrder";
            this.TxtOrder.Size = new System.Drawing.Size(124, 24);
            this.TxtOrder.TabIndex = 17;
            this.TxtOrder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtOrder_KeyDown);
            this.TxtOrder.Validating += new System.ComponentModel.CancelEventHandler(this.TxtOrder_Validating);
            // 
            // BtnOrderNo
            // 
            this.BtnOrderNo.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.BtnOrderNo.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnOrderNo.Location = new System.Drawing.Point(225, 88);
            this.BtnOrderNo.Name = "BtnOrderNo";
            this.BtnOrderNo.Size = new System.Drawing.Size(26, 24);
            this.BtnOrderNo.TabIndex = 550;
            this.BtnOrderNo.TabStop = false;
            this.BtnOrderNo.UseVisualStyleBackColor = false;
            this.BtnOrderNo.Click += new System.EventHandler(this.BtnOrder_Click);
            // 
            // TxtDepartment
            // 
            this.TxtDepartment.BackColor = System.Drawing.Color.White;
            this.TxtDepartment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDepartment.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.TxtDepartment.Location = new System.Drawing.Point(100, 167);
            this.TxtDepartment.MaxLength = 255;
            this.TxtDepartment.Name = "TxtDepartment";
            this.TxtDepartment.ReadOnly = true;
            this.TxtDepartment.Size = new System.Drawing.Size(355, 24);
            this.TxtDepartment.TabIndex = 24;
            this.TxtDepartment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDepartment_KeyDown);
            // 
            // BtnDepartment
            // 
            this.BtnDepartment.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.BtnDepartment.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDepartment.Location = new System.Drawing.Point(456, 167);
            this.BtnDepartment.Name = "BtnDepartment";
            this.BtnDepartment.Size = new System.Drawing.Size(26, 24);
            this.BtnDepartment.TabIndex = 553;
            this.BtnDepartment.TabStop = false;
            this.BtnDepartment.UseVisualStyleBackColor = false;
            this.BtnDepartment.Click += new System.EventHandler(this.BtnDepartment_Click);
            // 
            // TxtCustomer
            // 
            this.TxtCustomer.BackColor = System.Drawing.Color.White;
            this.TxtCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCustomer.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.TxtCustomer.Location = new System.Drawing.Point(100, 115);
            this.TxtCustomer.MaxLength = 255;
            this.TxtCustomer.Name = "TxtCustomer";
            this.TxtCustomer.ReadOnly = true;
            this.TxtCustomer.Size = new System.Drawing.Size(355, 24);
            this.TxtCustomer.TabIndex = 22;
            this.TxtCustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCustomer_KeyDown);
            this.TxtCustomer.Validating += new System.ComponentModel.CancelEventHandler(this.TxtCustomer_Validating);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label7.Location = new System.Drawing.Point(484, 116);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 19);
            this.label7.TabIndex = 554;
            this.label7.Text = "Due Days";
            // 
            // TxtDueDays
            // 
            this.TxtDueDays.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtDueDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDueDays.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.TxtDueDays.Location = new System.Drawing.Point(566, 113);
            this.TxtDueDays.MaxLength = 255;
            this.TxtDueDays.Name = "TxtDueDays";
            this.TxtDueDays.Size = new System.Drawing.Size(62, 24);
            this.TxtDueDays.TabIndex = 26;
            this.TxtDueDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtDueDays.TextChanged += new System.EventHandler(this.TxtDueDays_TextChanged);
            this.TxtDueDays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDueDays_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label1.Location = new System.Drawing.Point(401, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 19);
            this.label1.TabIndex = 544;
            this.label1.Text = "Date";
            // 
            // CmbInvoiceType
            // 
            this.CmbInvoiceType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.CmbInvoiceType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CmbInvoiceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbInvoiceType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbInvoiceType.Font = new System.Drawing.Font("Bookman Old Style", 9F);
            this.CmbInvoiceType.FormattingEnabled = true;
            this.CmbInvoiceType.Location = new System.Drawing.Point(652, 63);
            this.CmbInvoiceType.Name = "CmbInvoiceType";
            this.CmbInvoiceType.Size = new System.Drawing.Size(133, 24);
            this.CmbInvoiceType.TabIndex = 17;
            this.CmbInvoiceType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbInvType_KeyPress);
            // 
            // TxtAgent
            // 
            this.TxtAgent.BackColor = System.Drawing.Color.White;
            this.TxtAgent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAgent.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.TxtAgent.Location = new System.Drawing.Point(100, 193);
            this.TxtAgent.MaxLength = 255;
            this.TxtAgent.Name = "TxtAgent";
            this.TxtAgent.ReadOnly = true;
            this.TxtAgent.Size = new System.Drawing.Size(355, 24);
            this.TxtAgent.TabIndex = 25;
            this.TxtAgent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtAgent_KeyDown);
            // 
            // BtnVno
            // 
            this.BtnVno.CausesValidation = false;
            this.BtnVno.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.BtnVno.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnVno.Location = new System.Drawing.Point(225, 37);
            this.BtnVno.Name = "BtnVno";
            this.BtnVno.Size = new System.Drawing.Size(26, 24);
            this.BtnVno.TabIndex = 525;
            this.BtnVno.TabStop = false;
            this.BtnVno.UseVisualStyleBackColor = false;
            this.BtnVno.Click += new System.EventHandler(this.BtnVno_Click);
            // 
            // CmbPaymentMode
            // 
            this.CmbPaymentMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbPaymentMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbPaymentMode.Font = new System.Drawing.Font("Bookman Old Style", 9F);
            this.CmbPaymentMode.FormattingEnabled = true;
            this.CmbPaymentMode.Location = new System.Drawing.Point(857, 63);
            this.CmbPaymentMode.Name = "CmbPaymentMode";
            this.CmbPaymentMode.Size = new System.Drawing.Size(189, 24);
            this.CmbPaymentMode.TabIndex = 25;
            this.CmbPaymentMode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbBillIn_KeyPress);
            // 
            // BtnAgent
            // 
            this.BtnAgent.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.BtnAgent.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnAgent.Location = new System.Drawing.Point(456, 193);
            this.BtnAgent.Name = "BtnAgent";
            this.BtnAgent.Size = new System.Drawing.Size(26, 24);
            this.BtnAgent.TabIndex = 556;
            this.BtnAgent.TabStop = false;
            this.BtnAgent.UseVisualStyleBackColor = false;
            this.BtnAgent.Click += new System.EventHandler(this.BtnAgent_Click);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label30.Location = new System.Drawing.Point(581, 66);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(70, 19);
            this.label30.TabIndex = 561;
            this.label30.Text = "Inv Type";
            // 
            // TxtSubledger
            // 
            this.TxtSubledger.BackColor = System.Drawing.Color.White;
            this.TxtSubledger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSubledger.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.TxtSubledger.Location = new System.Drawing.Point(100, 141);
            this.TxtSubledger.MaxLength = 255;
            this.TxtSubledger.Name = "TxtSubledger";
            this.TxtSubledger.ReadOnly = true;
            this.TxtSubledger.Size = new System.Drawing.Size(355, 24);
            this.TxtSubledger.TabIndex = 23;
            this.TxtSubledger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSubledger_KeyDown);
            // 
            // TxtVno
            // 
            this.TxtVno.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtVno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtVno.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.TxtVno.Location = new System.Drawing.Point(100, 37);
            this.TxtVno.MaxLength = 50;
            this.TxtVno.Name = "TxtVno";
            this.TxtVno.Size = new System.Drawing.Size(124, 24);
            this.TxtVno.TabIndex = 9;
            this.TxtVno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtVno_KeyDown);
            this.TxtVno.Validating += new System.ComponentModel.CancelEventHandler(this.TxtVno_Validating);
            // 
            // BtnSubledger
            // 
            this.BtnSubledger.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSubledger.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.BtnSubledger.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnSubledger.Location = new System.Drawing.Point(456, 141);
            this.BtnSubledger.Name = "BtnSubledger";
            this.BtnSubledger.Size = new System.Drawing.Size(26, 24);
            this.BtnSubledger.TabIndex = 558;
            this.BtnSubledger.TabStop = false;
            this.BtnSubledger.UseVisualStyleBackColor = false;
            this.BtnSubledger.Click += new System.EventHandler(this.BtnSubLedger_Click);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.label29.Location = new System.Drawing.Point(581, 40);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(53, 19);
            this.label29.TabIndex = 560;
            this.label29.Text = "Ref No";
            // 
            // TxtRefVno
            // 
            this.TxtRefVno.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtRefVno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtRefVno.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.TxtRefVno.Location = new System.Drawing.Point(652, 38);
            this.TxtRefVno.MaxLength = 50;
            this.TxtRefVno.Name = "TxtRefVno";
            this.TxtRefVno.Size = new System.Drawing.Size(133, 23);
            this.TxtRefVno.TabIndex = 12;
            this.TxtRefVno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtRefVno_KeyDown);
            // 
            // MskMiti
            // 
            this.MskMiti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskMiti.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.MskMiti.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskMiti.Location = new System.Drawing.Point(289, 37);
            this.MskMiti.Mask = "00/00/0000";
            this.MskMiti.Name = "MskMiti";
            this.MskMiti.Size = new System.Drawing.Size(95, 24);
            this.MskMiti.TabIndex = 10;
            this.MskMiti.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.MskMiti.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalEnter_KeyPress);
            this.MskMiti.Validating += new System.ComponentModel.CancelEventHandler(this.MskMiti_Validating);
            // 
            // MskDueDays
            // 
            this.MskDueDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskDueDays.Enabled = false;
            this.MskDueDays.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.MskDueDays.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskDueDays.Location = new System.Drawing.Point(629, 113);
            this.MskDueDays.Mask = "00/00/0000";
            this.MskDueDays.Name = "MskDueDays";
            this.MskDueDays.Size = new System.Drawing.Size(88, 24);
            this.MskDueDays.TabIndex = 28;
            this.MskDueDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label11.Location = new System.Drawing.Point(251, 40);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 19);
            this.label11.TabIndex = 562;
            this.label11.Text = "Miti";
            // 
            // MskDate
            // 
            this.MskDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskDate.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.MskDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskDate.Location = new System.Drawing.Point(453, 37);
            this.MskDate.Mask = "00/00/0000";
            this.MskDate.Name = "MskDate";
            this.MskDate.Size = new System.Drawing.Size(100, 24);
            this.MskDate.TabIndex = 11;
            this.MskDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.MskDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalEnter_KeyPress);
            this.MskDate.Validating += new System.ComponentModel.CancelEventHandler(this.MskDate_Validating);
            // 
            // TxtCurrencyRate
            // 
            this.TxtCurrencyRate.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtCurrencyRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCurrencyRate.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.TxtCurrencyRate.Location = new System.Drawing.Point(566, 166);
            this.TxtCurrencyRate.MaxLength = 255;
            this.TxtCurrencyRate.Name = "TxtCurrencyRate";
            this.TxtCurrencyRate.Size = new System.Drawing.Size(152, 24);
            this.TxtCurrencyRate.TabIndex = 30;
            this.TxtCurrencyRate.Text = "1.00";
            this.TxtCurrencyRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtCurrencyRate.TextChanged += new System.EventHandler(this.TxtCurrencyRate_TextChanged);
            this.TxtCurrencyRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCurrencyRate_KeyPress);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.label28.Location = new System.Drawing.Point(784, 39);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(67, 19);
            this.label28.TabIndex = 559;
            this.label28.Text = "Ref Date";
            // 
            // lbl_Currency
            // 
            this.lbl_Currency.AutoSize = true;
            this.lbl_Currency.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.lbl_Currency.Location = new System.Drawing.Point(484, 142);
            this.lbl_Currency.Name = "lbl_Currency";
            this.lbl_Currency.Size = new System.Drawing.Size(79, 19);
            this.lbl_Currency.TabIndex = 545;
            this.lbl_Currency.Text = "Currency";
            // 
            // MskRefDate
            // 
            this.MskRefDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskRefDate.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.MskRefDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskRefDate.Location = new System.Drawing.Point(857, 37);
            this.MskRefDate.Mask = "00/00/0000";
            this.MskRefDate.Name = "MskRefDate";
            this.MskRefDate.Size = new System.Drawing.Size(87, 23);
            this.MskRefDate.TabIndex = 13;
            this.MskRefDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.MskRefDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MskRefDate_KeyDown);
            // 
            // lbl_CurrencyRate
            // 
            this.lbl_CurrencyRate.AutoSize = true;
            this.lbl_CurrencyRate.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.lbl_CurrencyRate.Location = new System.Drawing.Point(484, 169);
            this.lbl_CurrencyRate.Name = "lbl_CurrencyRate";
            this.lbl_CurrencyRate.Size = new System.Drawing.Size(44, 19);
            this.lbl_CurrencyRate.TabIndex = 546;
            this.lbl_CurrencyRate.Text = "Rate";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.LblBalanceType);
            this.groupBox6.Controls.Add(this.LblCreditDays);
            this.groupBox6.Controls.Add(this.LblPanNo);
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Controls.Add(this.label25);
            this.groupBox6.Controls.Add(this.lbl_Currentbal);
            this.groupBox6.Controls.Add(this.LblCreditLimit);
            this.groupBox6.Controls.Add(this.label20);
            this.groupBox6.Controls.Add(this.LblBalance);
            this.groupBox6.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.groupBox6.Location = new System.Drawing.Point(724, 82);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(327, 119);
            this.groupBox6.TabIndex = 564;
            this.groupBox6.TabStop = false;
            // 
            // LblBalanceType
            // 
            this.LblBalanceType.AutoSize = true;
            this.LblBalanceType.ForeColor = System.Drawing.Color.Black;
            this.LblBalanceType.Location = new System.Drawing.Point(294, 38);
            this.LblBalanceType.Name = "LblBalanceType";
            this.LblBalanceType.Size = new System.Drawing.Size(27, 19);
            this.LblBalanceType.TabIndex = 176;
            this.LblBalanceType.Text = "Cr";
            // 
            // LblCreditDays
            // 
            this.LblCreditDays.BackColor = System.Drawing.Color.LightBlue;
            this.LblCreditDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblCreditDays.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblCreditDays.ForeColor = System.Drawing.Color.Crimson;
            this.LblCreditDays.Location = new System.Drawing.Point(5, 84);
            this.LblCreditDays.Name = "LblCreditDays";
            this.LblCreditDays.Size = new System.Drawing.Size(135, 28);
            this.LblCreditDays.TabIndex = 175;
            this.LblCreditDays.Text = "0";
            this.LblCreditDays.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblPanNo
            // 
            this.LblPanNo.BackColor = System.Drawing.Color.LightBlue;
            this.LblPanNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPanNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblPanNo.ForeColor = System.Drawing.Color.Crimson;
            this.LblPanNo.Location = new System.Drawing.Point(5, 34);
            this.LblPanNo.Name = "LblPanNo";
            this.LblPanNo.Size = new System.Drawing.Size(135, 28);
            this.LblPanNo.TabIndex = 149;
            this.LblPanNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(8, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 19);
            this.label6.TabIndex = 150;
            this.label6.Text = "Pan No";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(8, 64);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(98, 19);
            this.label25.TabIndex = 174;
            this.label25.Text = "Credit Days";
            // 
            // lbl_Currentbal
            // 
            this.lbl_Currentbal.AutoSize = true;
            this.lbl_Currentbal.ForeColor = System.Drawing.Color.Black;
            this.lbl_Currentbal.Location = new System.Drawing.Point(145, 12);
            this.lbl_Currentbal.Name = "lbl_Currentbal";
            this.lbl_Currentbal.Size = new System.Drawing.Size(70, 19);
            this.lbl_Currentbal.TabIndex = 148;
            this.lbl_Currentbal.Text = "Balance";
            // 
            // LblCreditLimit
            // 
            this.LblCreditLimit.BackColor = System.Drawing.Color.LightBlue;
            this.LblCreditLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblCreditLimit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblCreditLimit.ForeColor = System.Drawing.Color.Crimson;
            this.LblCreditLimit.Location = new System.Drawing.Point(144, 85);
            this.LblCreditLimit.Name = "LblCreditLimit";
            this.LblCreditLimit.Size = new System.Drawing.Size(144, 28);
            this.LblCreditLimit.TabIndex = 151;
            this.LblCreditLimit.Text = "0.00";
            this.LblCreditLimit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(145, 64);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(100, 19);
            this.label20.TabIndex = 152;
            this.label20.Text = "Credit Limit";
            // 
            // LblBalance
            // 
            this.LblBalance.BackColor = System.Drawing.Color.LightBlue;
            this.LblBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblBalance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblBalance.ForeColor = System.Drawing.Color.Crimson;
            this.LblBalance.Location = new System.Drawing.Point(144, 34);
            this.LblBalance.Name = "LblBalance";
            this.LblBalance.Size = new System.Drawing.Size(144, 28);
            this.LblBalance.TabIndex = 9;
            this.LblBalance.Text = "0.00";
            this.LblBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BtnNew
            // 
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnNew.Location = new System.Drawing.Point(5, 2);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(80, 30);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Tag = "NewButtonCheck";
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEdit.Appearance.Options.UseFont = true;
            this.BtnEdit.ImageOptions.Image = global::MrBLL.Properties.Resources.Edit;
            this.BtnEdit.Location = new System.Drawing.Point(86, 2);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(76, 30);
            this.BtnEdit.TabIndex = 1;
            this.BtnEdit.Tag = "EditButtonCheck";
            this.BtnEdit.Text = "&EDIT";
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(163, 2);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(102, 30);
            this.BtnDelete.TabIndex = 2;
            this.BtnDelete.Tag = "DeleteButtonCheck";
            this.BtnDelete.Text = "&DELETE";
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnPrintInvoice
            // 
            this.BtnPrintInvoice.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPrintInvoice.Appearance.Options.UseFont = true;
            this.BtnPrintInvoice.ImageOptions.Image = global::MrBLL.Properties.Resources.Printer24;
            this.BtnPrintInvoice.Location = new System.Drawing.Point(692, 2);
            this.BtnPrintInvoice.Name = "BtnPrintInvoice";
            this.BtnPrintInvoice.Size = new System.Drawing.Size(84, 30);
            this.BtnPrintInvoice.TabIndex = 5;
            this.BtnPrintInvoice.Text = "&PRINT";
            this.BtnPrintInvoice.Click += new System.EventHandler(this.BtnPrintInvoice_Click);
            // 
            // BtnReverse
            // 
            this.BtnReverse.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnReverse.Appearance.Options.UseFont = true;
            this.BtnReverse.ImageOptions.Image = global::MrBLL.Properties.Resources.Reverse;
            this.BtnReverse.Location = new System.Drawing.Point(777, 2);
            this.BtnReverse.Name = "BtnReverse";
            this.BtnReverse.Size = new System.Drawing.Size(116, 30);
            this.BtnReverse.TabIndex = 6;
            this.BtnReverse.Text = "&REVERSE";
            this.BtnReverse.Click += new System.EventHandler(this.BtnReverse_Click);
            // 
            // BtnCopy
            // 
            this.BtnCopy.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCopy.Appearance.Options.UseFont = true;
            this.BtnCopy.ImageOptions.Image = global::MrBLL.Properties.Resources.Copy;
            this.BtnCopy.Location = new System.Drawing.Point(894, 2);
            this.BtnCopy.Name = "BtnCopy";
            this.BtnCopy.Size = new System.Drawing.Size(82, 30);
            this.BtnCopy.TabIndex = 7;
            this.BtnCopy.Text = "&COPY";
            this.BtnCopy.Click += new System.EventHandler(this.BtnCopy_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.AllowDrop = true;
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.BtnExit.Location = new System.Drawing.Point(977, 2);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(74, 30);
            this.BtnExit.TabIndex = 8;
            this.BtnExit.Text = "E&XIT";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnPurchaseInvoice
            // 
            this.BtnPurchaseInvoice.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPurchaseInvoice.Appearance.Options.UseFont = true;
            this.BtnPurchaseInvoice.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnPurchaseInvoice.Location = new System.Drawing.Point(561, 2);
            this.BtnPurchaseInvoice.Name = "BtnPurchaseInvoice";
            this.BtnPurchaseInvoice.Size = new System.Drawing.Size(130, 30);
            this.BtnPurchaseInvoice.TabIndex = 4;
            this.BtnPurchaseInvoice.Text = "&PURCHASE";
            this.BtnPurchaseInvoice.Click += new System.EventHandler(this.BtnPurchaseInvoice_Click);
            // 
            // lbl_VoucherNo
            // 
            this.lbl_VoucherNo.AutoSize = true;
            this.lbl_VoucherNo.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.lbl_VoucherNo.Location = new System.Drawing.Point(4, 40);
            this.lbl_VoucherNo.Name = "lbl_VoucherNo";
            this.lbl_VoucherNo.Size = new System.Drawing.Size(85, 19);
            this.lbl_VoucherNo.TabIndex = 543;
            this.lbl_VoucherNo.Text = "Invoice No";
            // 
            // lbl_Class
            // 
            this.lbl_Class.AutoSize = true;
            this.lbl_Class.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.lbl_Class.Location = new System.Drawing.Point(4, 170);
            this.lbl_Class.Name = "lbl_Class";
            this.lbl_Class.Size = new System.Drawing.Size(99, 19);
            this.lbl_Class.TabIndex = 548;
            this.lbl_Class.Text = "Department";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label2.Location = new System.Drawing.Point(4, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 19);
            this.label2.TabIndex = 551;
            this.label2.Text = "Order No";
            // 
            // lbl_RefVoucherNo
            // 
            this.lbl_RefVoucherNo.AutoSize = true;
            this.lbl_RefVoucherNo.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.lbl_RefVoucherNo.Location = new System.Drawing.Point(4, 65);
            this.lbl_RefVoucherNo.Name = "lbl_RefVoucherNo";
            this.lbl_RefVoucherNo.Size = new System.Drawing.Size(67, 19);
            this.lbl_RefVoucherNo.TabIndex = 549;
            this.lbl_RefVoucherNo.Text = "GRN No";
            // 
            // lbl_CBLedger
            // 
            this.lbl_CBLedger.AutoSize = true;
            this.lbl_CBLedger.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.lbl_CBLedger.Location = new System.Drawing.Point(4, 118);
            this.lbl_CBLedger.Name = "lbl_CBLedger";
            this.lbl_CBLedger.Size = new System.Drawing.Size(82, 19);
            this.lbl_CBLedger.TabIndex = 547;
            this.lbl_CBLedger.Text = "Customer";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label9.Location = new System.Drawing.Point(4, 196);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 19);
            this.label9.TabIndex = 555;
            this.label9.Text = "Agent";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label10.Location = new System.Drawing.Point(4, 144);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 19);
            this.label10.TabIndex = 557;
            this.label10.Text = "SubLedger";
            // 
            // FrmSalesInvoiceEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 567);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmSalesInvoiceEntry";
            this.ShowIcon = false;
            this.Tag = "Sales Master->Invoice";
            this.Text = "SALES INVOICE ENTRY";
            this.Load += new System.EventHandler(this.FrmSalesInvoiceEntry_Load);
            this.Shown += new System.EventHandler(this.FrmSalesInvoiceEntry_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmSalesInvoiceEntry_KeyPress);
            this.TabLedgerOpening.ResumeLayout(false);
            this.TabProductDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).EndInit();
            this.mrPanel1.ResumeLayout(false);
            this.mrPanel1.PerformLayout();
            this.TabAttachment.ResumeLayout(false);
            this.TabAttachment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment1)).EndInit();
            this.TabAdditionalInformation.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.TabMailingAddress.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabLedgerOpening;
        private System.Windows.Forms.TabPage TabProductDetails;
        private System.Windows.Forms.TabPage TabAttachment;
        private System.Windows.Forms.LinkLabel LinkAttachment5;
        private System.Windows.Forms.LinkLabel LinkAttachment4;
        private System.Windows.Forms.LinkLabel LinkAttachment3;
        private System.Windows.Forms.LinkLabel LinkAttachment2;
        private System.Windows.Forms.LinkLabel LinkAttachment1;
        private System.Windows.Forms.Button BtnAttachment5;
        public System.Windows.Forms.Label LblAttachment5;
        private System.Windows.Forms.PictureBox PAttachment5;
        private System.Windows.Forms.Button BtnAttachment4;
        public System.Windows.Forms.Label LblAttachment4;
        private System.Windows.Forms.PictureBox PAttachment4;
        private System.Windows.Forms.Button BtnAttachment3;
        public System.Windows.Forms.Label LblAttachment3;
        private System.Windows.Forms.PictureBox PAttachment3;
        private System.Windows.Forms.Button BtnAttachment2;
        public System.Windows.Forms.Label LblAttachment2;
        private System.Windows.Forms.PictureBox PAttachment2;
        private System.Windows.Forms.Button BtnAttachment1;
        public System.Windows.Forms.Label LblAttachment1;
        private System.Windows.Forms.PictureBox PAttachment1;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnPrintInvoice;
        private DevExpress.XtraEditors.SimpleButton BtnReverse;
        private DevExpress.XtraEditors.SimpleButton BtnCopy;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnPurchaseInvoice;
        private System.Windows.Forms.Button BtnCurrency;
        private System.Windows.Forms.Label lbl_VoucherNo;
        private System.Windows.Forms.Label lbl_Class;
        public System.Windows.Forms.Label label31;
        public System.Windows.Forms.Button BtnChallanNo;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button BtnVendor;
        private System.Windows.Forms.Label lbl_RefVoucherNo;
        public System.Windows.Forms.Button BtnOrderNo;
        private System.Windows.Forms.Button BtnDepartment;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbl_CBLedger;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CmbInvoiceType;
        public System.Windows.Forms.Button BtnVno;
        private System.Windows.Forms.ComboBox CmbPaymentMode;
        private System.Windows.Forms.Button BtnAgent;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label30;
        public System.Windows.Forms.Button BtnSubledger;
        private System.Windows.Forms.Label label29;
        private MrMaskedTextBox MskMiti;
        private MrMaskedTextBox MskDueDays;
        private System.Windows.Forms.Label label11;
        private MrMaskedTextBox MskDate;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label lbl_Currency;
        private MrMaskedTextBox MskRefDate;
        private System.Windows.Forms.Label lbl_CurrencyRate;
        public System.Windows.Forms.GroupBox groupBox6;
        public System.Windows.Forms.Label LblCreditDays;
        public System.Windows.Forms.Label LblPanNo;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label label25;
        public System.Windows.Forms.Label lbl_Currentbal;
        public System.Windows.Forms.Label LblCreditLimit;
        public System.Windows.Forms.Label label20;
        public System.Windows.Forms.Label LblBalance;
        private System.Windows.Forms.Label LblTotalAltQty;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label LblTotalBasicAmount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BtnBillingTerm;
        private System.Windows.Forms.Label LblTotalNetAmount;
        private System.Windows.Forms.Label LblTotalLocalNetAmount;
        private System.Windows.Forms.Label LblTotalQty;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label lbl_TotQty1;
        private System.Windows.Forms.Label lbl_TotAltQty1;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private System.Windows.Forms.Label lbl_Remarks;
        private System.Windows.Forms.Label lbl_NoInWords;
        private System.Windows.Forms.Label LblNumberInWords;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private ClsSeparator clsSeparator1;
        private ClsSeparator clsSeparator2;
        public System.Windows.Forms.Label LblBalanceType;
        private MrTextBox TxtCurrency;
        private MrTextBox TxtChallan;
        private MrTextBox TxtOrder;
        private MrTextBox TxtDepartment;
        private MrTextBox TxtCustomer;
        private MrTextBox TxtDueDays;
        private MrTextBox TxtAgent;
        private MrTextBox TxtSubledger;
        private MrTextBox TxtVno;
        private MrTextBox TxtRefVno;
        private MrTextBox TxtCurrencyRate;
        private MrTextBox TxtBillTermAmount;
        private MrTextBox TxtRemarks;
        private DevExpress.XtraEditors.SimpleButton BtnReturnInvoice;
        private System.Windows.Forms.TabPage TabAdditionalInformation;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.ComboBox CmbBiltyType;
        public System.Windows.Forms.Label label43;
        public System.Windows.Forms.Label label42;
        public System.Windows.Forms.Label label23;
        public System.Windows.Forms.Label label35;
        public System.Windows.Forms.Label label27;
        public System.Windows.Forms.Label label33;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Label label13;
        public System.Windows.Forms.Label label15;
        public System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.Label label49;
        public System.Windows.Forms.Label label12;
        public System.Windows.Forms.Label label32;
        public System.Windows.Forms.Label label34;
        public System.Windows.Forms.Label label36;
        public System.Windows.Forms.Label label37;
        private System.Windows.Forms.TabPage TabMailingAddress;
        private System.Windows.Forms.GroupBox groupBox4;
        public System.Windows.Forms.Label label14;
        public System.Windows.Forms.Label label38;
        public System.Windows.Forms.Label label39;
        public System.Windows.Forms.Label label40;
        public System.Windows.Forms.Label label41;
        private System.Windows.Forms.GroupBox groupBox5;
        public System.Windows.Forms.Label label44;
        public System.Windows.Forms.Label label45;
        public System.Windows.Forms.Label label46;
        public System.Windows.Forms.Label label47;
        public System.Windows.Forms.Label label48;
        private MrPanel panel1;
        private MrPanel panel2;
        public MrMaskedTextBox MskBiltyDate;
        public MrTextBox TxtTransport;
        public MrTextBox TxtPackage;
        public MrTextBox TxtBiltyNo;
        public MrTextBox TxtVechileNo;
        public MrTextBox TxtDriver;
        public MrTextBox TxtLicenseNo;
        public MrTextBox TxtPhoneNo;
        public MrTextBox TxtCustomName;
        public MrTextBox TxtLCNumber;
        public MrMaskedTextBox MskVendorOrderDate;
        public MrMaskedTextBox MskExportInvoiceDate;
        public MrMaskedTextBox MskContractNoDate;
        public MrTextBox TxtContractNo;
        public MrTextBox TxtBankDetails;
        public MrTextBox TxtVendorOrderNo;
        public MrTextBox TxtExportInvoiceNo;
        public MrTextBox TxtMEmail;
        public MrTextBox TxtMailingAddress;
        public MrTextBox TxtMCountry;
        public MrTextBox TxtMState;
        public MrTextBox TxtMCity;
        public MrTextBox TxtSEmail;
        public MrTextBox TxtShippingAddress;
        public MrTextBox TxtSCountry;
        public MrTextBox TxtSState;
        public MrTextBox TxtSCity;
        private System.Windows.Forms.Label label4;
        private MrMaskedTextBox MskChallanMiti;
        private System.Windows.Forms.Label label8;
        private MrMaskedTextBox MskChallanDate;
        private System.Windows.Forms.Label label17;
        private MrMaskedTextBox MskOrderMiti;
        private System.Windows.Forms.Label label18;
        private MrMaskedTextBox MskOrderDate;
        private EntryGridViewEx DGrid;
        private MrPanel mrPanel1;
        private Label LblShortName;
        private Label label21;
        private Label LblSalesRate;
        private Label label52;
        private Label LblStockQty;
        private Label label50;
        private Label LblAltSalesRate;
        private Label label54;
        private Label LblAltStockQty;
        private Label label56;
    }
}