using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.DataEntry.PurchaseMaster
{
    partial class FrmPurchaseOrderEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPurchaseOrderEntry));
            this.TabLedgerOpening = new System.Windows.Forms.TabControl();
            this.TabProductDetails = new System.Windows.Forms.TabPage();
            this.DGrid = new MrDAL.Control.ControlsEx.Control.EntryGridViewEx();
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
            this.panel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.MskIndentMiti = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
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
            this.lbl_VoucherNo = new System.Windows.Forms.Label();
            this.lbl_Class = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnVendor = new System.Windows.Forms.Button();
            this.TxtIndentNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnIndentNo = new System.Windows.Forms.Button();
            this.TxtDepartment = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnDepartment = new System.Windows.Forms.Button();
            this.TxtVendor = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_CBLedger = new System.Windows.Forms.Label();
            this.TxtDueDays = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CmbInvoiceType = new System.Windows.Forms.ComboBox();
            this.TxtAgent = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnVno = new System.Windows.Forms.Button();
            this.CmbPaymentMode = new System.Windows.Forms.ComboBox();
            this.BtnAgent = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
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
            this.BtnSalesInvoice = new DevExpress.XtraEditors.SimpleButton();
            this.TabLedgerOpening.SuspendLayout();
            this.TabProductDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).BeginInit();
            this.TabAttachment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabLedgerOpening
            // 
            this.TabLedgerOpening.Controls.Add(this.TabProductDetails);
            this.TabLedgerOpening.Controls.Add(this.TabAttachment);
            this.TabLedgerOpening.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabLedgerOpening.Location = new System.Drawing.Point(11, 177);
            this.TabLedgerOpening.Name = "TabLedgerOpening";
            this.TabLedgerOpening.SelectedIndex = 0;
            this.TabLedgerOpening.Size = new System.Drawing.Size(1043, 240);
            this.TabLedgerOpening.TabIndex = 16;
            // 
            // TabProductDetails
            // 
            this.TabProductDetails.Controls.Add(this.DGrid);
            this.TabProductDetails.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabProductDetails.Location = new System.Drawing.Point(4, 27);
            this.TabProductDetails.Name = "TabProductDetails";
            this.TabProductDetails.Padding = new System.Windows.Forms.Padding(3);
            this.TabProductDetails.Size = new System.Drawing.Size(1035, 209);
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
            this.DGrid.Size = new System.Drawing.Size(1029, 203);
            this.DGrid.StandardTab = true;
            this.DGrid.TabIndex = 0;
            this.DGrid.TabStop = false;
            this.DGrid.EnterKeyPressed += new System.EventHandler<System.EventArgs>(this.DGrid_EnterKeyPressed);
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
            this.TabAttachment.Size = new System.Drawing.Size(1035, 209);
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.MskIndentMiti);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.clsSeparator2);
            this.panel1.Controls.Add(this.clsSeparator1);
            this.panel1.Controls.Add(this.BtnCancel);
            this.panel1.Controls.Add(this.lbl_Remarks);
            this.panel1.Controls.Add(this.TxtRemarks);
            this.panel1.Controls.Add(this.lbl_NoInWords);
            this.panel1.Controls.Add(this.LblNumberInWords);
            this.panel1.Controls.Add(this.BtnSave);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.TxtCurrency);
            this.panel1.Controls.Add(this.BtnCurrency);
            this.panel1.Controls.Add(this.lbl_VoucherNo);
            this.panel1.Controls.Add(this.lbl_Class);
            this.panel1.Controls.Add(this.label31);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.BtnVendor);
            this.panel1.Controls.Add(this.TxtIndentNo);
            this.panel1.Controls.Add(this.BtnIndentNo);
            this.panel1.Controls.Add(this.TxtDepartment);
            this.panel1.Controls.Add(this.BtnDepartment);
            this.panel1.Controls.Add(this.TxtVendor);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.lbl_CBLedger);
            this.panel1.Controls.Add(this.TxtDueDays);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.CmbInvoiceType);
            this.panel1.Controls.Add(this.TxtAgent);
            this.panel1.Controls.Add(this.BtnVno);
            this.panel1.Controls.Add(this.CmbPaymentMode);
            this.panel1.Controls.Add(this.BtnAgent);
            this.panel1.Controls.Add(this.label10);
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
            this.panel1.Controls.Add(this.BtnSalesInvoice);
            this.panel1.Controls.Add(this.TabLedgerOpening);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1055, 521);
            this.panel1.TabIndex = 17;
            // 
            // MskIndentMiti
            // 
            this.MskIndentMiti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskIndentMiti.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.MskIndentMiti.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskIndentMiti.Location = new System.Drawing.Point(289, 63);
            this.MskIndentMiti.Mask = "00/00/0000";
            this.MskIndentMiti.Name = "MskIndentMiti";
            this.MskIndentMiti.Size = new System.Drawing.Size(118, 24);
            this.MskIndentMiti.TabIndex = 576;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label8.Location = new System.Drawing.Point(251, 66);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 19);
            this.label8.TabIndex = 577;
            this.label8.Text = "Miti";
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
            this.clsSeparator1.Location = new System.Drawing.Point(12, 172);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(1041, 2);
            this.clsSeparator1.TabIndex = 574;
            this.clsSeparator1.TabStop = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(949, 483);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(104, 35);
            this.BtnCancel.TabIndex = 570;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // lbl_Remarks
            // 
            this.lbl_Remarks.AutoSize = true;
            this.lbl_Remarks.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Remarks.Location = new System.Drawing.Point(9, 457);
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
            this.TxtRemarks.Location = new System.Drawing.Point(107, 457);
            this.TxtRemarks.MaxLength = 255;
            this.TxtRemarks.Name = "TxtRemarks";
            this.TxtRemarks.Size = new System.Drawing.Size(943, 24);
            this.TxtRemarks.TabIndex = 568;
            this.TxtRemarks.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalEnter_KeyPress);
            // 
            // lbl_NoInWords
            // 
            this.lbl_NoInWords.AutoSize = true;
            this.lbl_NoInWords.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NoInWords.Location = new System.Drawing.Point(9, 491);
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
            this.LblNumberInWords.Location = new System.Drawing.Point(107, 483);
            this.LblNumberInWords.Name = "LblNumberInWords";
            this.LblNumberInWords.Size = new System.Drawing.Size(748, 32);
            this.LblNumberInWords.TabIndex = 573;
            this.LblNumberInWords.Text = "Only.";
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(861, 483);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(87, 35);
            this.BtnSave.TabIndex = 569;
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
            this.panel2.Location = new System.Drawing.Point(8, 419);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1043, 35);
            this.panel2.TabIndex = 567;
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
            this.TxtCurrency.Location = new System.Drawing.Point(827, 89);
            this.TxtCurrency.MaxLength = 255;
            this.TxtCurrency.Name = "TxtCurrency";
            this.TxtCurrency.ReadOnly = true;
            this.TxtCurrency.Size = new System.Drawing.Size(90, 25);
            this.TxtCurrency.TabIndex = 538;
            this.TxtCurrency.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCurrency_KeyDown);
            // 
            // BtnCurrency
            // 
            this.BtnCurrency.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCurrency.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnCurrency.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnCurrency.Location = new System.Drawing.Point(920, 88);
            this.BtnCurrency.Name = "BtnCurrency";
            this.BtnCurrency.Size = new System.Drawing.Size(26, 26);
            this.BtnCurrency.TabIndex = 566;
            this.BtnCurrency.TabStop = false;
            this.BtnCurrency.UseVisualStyleBackColor = false;
            this.BtnCurrency.Click += new System.EventHandler(this.BtnCurrency_Click);
            // 
            // lbl_VoucherNo
            // 
            this.lbl_VoucherNo.AutoSize = true;
            this.lbl_VoucherNo.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.lbl_VoucherNo.Location = new System.Drawing.Point(8, 40);
            this.lbl_VoucherNo.Name = "lbl_VoucherNo";
            this.lbl_VoucherNo.Size = new System.Drawing.Size(77, 19);
            this.lbl_VoucherNo.TabIndex = 543;
            this.lbl_VoucherNo.Text = "Order No";
            // 
            // lbl_Class
            // 
            this.lbl_Class.AutoSize = true;
            this.lbl_Class.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.lbl_Class.Location = new System.Drawing.Point(366, 148);
            this.lbl_Class.Name = "lbl_Class";
            this.lbl_Class.Size = new System.Drawing.Size(99, 19);
            this.lbl_Class.TabIndex = 548;
            this.lbl_Class.Text = "Department";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(748, 65);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(55, 19);
            this.label31.TabIndex = 563;
            this.label31.Text = "Bill In";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label4.Location = new System.Drawing.Point(8, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 19);
            this.label4.TabIndex = 565;
            this.label4.Text = "Details :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label2.Location = new System.Drawing.Point(8, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 19);
            this.label2.TabIndex = 551;
            this.label2.Text = "Indent No";
            // 
            // BtnVendor
            // 
            this.BtnVendor.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.BtnVendor.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnVendor.Location = new System.Drawing.Point(456, 89);
            this.BtnVendor.Name = "BtnVendor";
            this.BtnVendor.Size = new System.Drawing.Size(26, 24);
            this.BtnVendor.TabIndex = 552;
            this.BtnVendor.TabStop = false;
            this.BtnVendor.UseVisualStyleBackColor = false;
            this.BtnVendor.Click += new System.EventHandler(this.BtnVendor_Click);
            // 
            // TxtIndentNo
            // 
            this.TxtIndentNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtIndentNo.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.TxtIndentNo.Location = new System.Drawing.Point(100, 62);
            this.TxtIndentNo.MaxLength = 50;
            this.TxtIndentNo.Name = "TxtIndentNo";
            this.TxtIndentNo.Size = new System.Drawing.Size(124, 24);
            this.TxtIndentNo.TabIndex = 532;
            this.TxtIndentNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtOrder_KeyDown);
            this.TxtIndentNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalEnter_KeyPress);
            // 
            // BtnIndentNo
            // 
            this.BtnIndentNo.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.BtnIndentNo.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnIndentNo.Location = new System.Drawing.Point(226, 62);
            this.BtnIndentNo.Name = "BtnIndentNo";
            this.BtnIndentNo.Size = new System.Drawing.Size(26, 24);
            this.BtnIndentNo.TabIndex = 550;
            this.BtnIndentNo.TabStop = false;
            this.BtnIndentNo.UseVisualStyleBackColor = false;
            this.BtnIndentNo.Click += new System.EventHandler(this.BtnOrder_Click);
            // 
            // TxtDepartment
            // 
            this.TxtDepartment.BackColor = System.Drawing.Color.White;
            this.TxtDepartment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDepartment.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.TxtDepartment.Location = new System.Drawing.Point(465, 146);
            this.TxtDepartment.MaxLength = 50;
            this.TxtDepartment.Name = "TxtDepartment";
            this.TxtDepartment.ReadOnly = true;
            this.TxtDepartment.Size = new System.Drawing.Size(240, 24);
            this.TxtDepartment.TabIndex = 541;
            this.TxtDepartment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDepartment_KeyDown);
            // 
            // BtnDepartment
            // 
            this.BtnDepartment.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.BtnDepartment.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDepartment.Location = new System.Drawing.Point(705, 146);
            this.BtnDepartment.Name = "BtnDepartment";
            this.BtnDepartment.Size = new System.Drawing.Size(26, 24);
            this.BtnDepartment.TabIndex = 553;
            this.BtnDepartment.TabStop = false;
            this.BtnDepartment.UseVisualStyleBackColor = false;
            this.BtnDepartment.Click += new System.EventHandler(this.BtnDepartment_Click);
            // 
            // TxtVendor
            // 
            this.TxtVendor.BackColor = System.Drawing.Color.White;
            this.TxtVendor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtVendor.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.TxtVendor.Location = new System.Drawing.Point(100, 89);
            this.TxtVendor.MaxLength = 255;
            this.TxtVendor.Name = "TxtVendor";
            this.TxtVendor.ReadOnly = true;
            this.TxtVendor.Size = new System.Drawing.Size(355, 24);
            this.TxtVendor.TabIndex = 535;
            this.TxtVendor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtVendor_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label7.Location = new System.Drawing.Point(482, 92);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 19);
            this.label7.TabIndex = 554;
            this.label7.Text = "Due Days";
            // 
            // lbl_CBLedger
            // 
            this.lbl_CBLedger.AutoSize = true;
            this.lbl_CBLedger.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.lbl_CBLedger.Location = new System.Drawing.Point(8, 92);
            this.lbl_CBLedger.Name = "lbl_CBLedger";
            this.lbl_CBLedger.Size = new System.Drawing.Size(63, 19);
            this.lbl_CBLedger.TabIndex = 547;
            this.lbl_CBLedger.Text = "Vendor";
            // 
            // TxtDueDays
            // 
            this.TxtDueDays.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtDueDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDueDays.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.TxtDueDays.Location = new System.Drawing.Point(564, 89);
            this.TxtDueDays.MaxLength = 255;
            this.TxtDueDays.Name = "TxtDueDays";
            this.TxtDueDays.Size = new System.Drawing.Size(62, 24);
            this.TxtDueDays.TabIndex = 536;
            this.TxtDueDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtDueDays.TextChanged += new System.EventHandler(this.TxtDueDays_TextChanged);
            this.TxtDueDays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDueDays_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label9.Location = new System.Drawing.Point(731, 149);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 19);
            this.label9.TabIndex = 555;
            this.label9.Text = "Agent";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label1.Location = new System.Drawing.Point(411, 40);
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
            this.CmbInvoiceType.Items.AddRange(new object[] {
            "Assets",
            "Import",
            "Local"});
            this.CmbInvoiceType.Location = new System.Drawing.Point(564, 62);
            this.CmbInvoiceType.Name = "CmbInvoiceType";
            this.CmbInvoiceType.Size = new System.Drawing.Size(180, 24);
            this.CmbInvoiceType.Sorted = true;
            this.CmbInvoiceType.TabIndex = 533;
            this.CmbInvoiceType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbInvType_KeyPress);
            // 
            // TxtAgent
            // 
            this.TxtAgent.BackColor = System.Drawing.Color.White;
            this.TxtAgent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAgent.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.TxtAgent.Location = new System.Drawing.Point(783, 146);
            this.TxtAgent.MaxLength = 50;
            this.TxtAgent.Name = "TxtAgent";
            this.TxtAgent.ReadOnly = true;
            this.TxtAgent.Size = new System.Drawing.Size(240, 24);
            this.TxtAgent.TabIndex = 542;
            this.TxtAgent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtAgent_KeyDown);
            // 
            // BtnVno
            // 
            this.BtnVno.CausesValidation = false;
            this.BtnVno.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.BtnVno.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnVno.Location = new System.Drawing.Point(226, 38);
            this.BtnVno.Name = "BtnVno";
            this.BtnVno.Size = new System.Drawing.Size(26, 22);
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
            this.CmbPaymentMode.Items.AddRange(new object[] {
            "Credit",
            "Cash",
            "Bank",
            "Other"});
            this.CmbPaymentMode.Location = new System.Drawing.Point(827, 62);
            this.CmbPaymentMode.Name = "CmbPaymentMode";
            this.CmbPaymentMode.Size = new System.Drawing.Size(227, 24);
            this.CmbPaymentMode.TabIndex = 534;
            this.CmbPaymentMode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbBillIn_KeyPress);
            // 
            // BtnAgent
            // 
            this.BtnAgent.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.BtnAgent.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnAgent.Location = new System.Drawing.Point(1023, 146);
            this.BtnAgent.Name = "BtnAgent";
            this.BtnAgent.Size = new System.Drawing.Size(26, 24);
            this.BtnAgent.TabIndex = 556;
            this.BtnAgent.TabStop = false;
            this.BtnAgent.UseVisualStyleBackColor = false;
            this.BtnAgent.Click += new System.EventHandler(this.BtnAgent_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label10.Location = new System.Drawing.Point(8, 144);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 19);
            this.label10.TabIndex = 557;
            this.label10.Text = "SubLedger";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label30.Location = new System.Drawing.Point(485, 65);
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
            this.TxtSubledger.Location = new System.Drawing.Point(100, 144);
            this.TxtSubledger.MaxLength = 50;
            this.TxtSubledger.Name = "TxtSubledger";
            this.TxtSubledger.ReadOnly = true;
            this.TxtSubledger.Size = new System.Drawing.Size(240, 24);
            this.TxtSubledger.TabIndex = 540;
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
            this.TxtVno.TabIndex = 524;
            this.TxtVno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtVno_KeyDown);
            this.TxtVno.Validating += new System.ComponentModel.CancelEventHandler(this.TxtVno_Validating);
            // 
            // BtnSubledger
            // 
            this.BtnSubledger.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSubledger.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.BtnSubledger.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnSubledger.Location = new System.Drawing.Point(340, 144);
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
            this.TxtRefVno.Location = new System.Drawing.Point(634, 38);
            this.TxtRefVno.MaxLength = 255;
            this.TxtRefVno.Name = "TxtRefVno";
            this.TxtRefVno.Size = new System.Drawing.Size(110, 23);
            this.TxtRefVno.TabIndex = 529;
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
            this.MskMiti.Size = new System.Drawing.Size(118, 24);
            this.MskMiti.TabIndex = 527;
            this.MskMiti.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalEnter_KeyPress);
            this.MskMiti.Validating += new System.ComponentModel.CancelEventHandler(this.MskMiti_Validating);
            // 
            // MskDueDays
            // 
            this.MskDueDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskDueDays.Enabled = false;
            this.MskDueDays.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.MskDueDays.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskDueDays.Location = new System.Drawing.Point(627, 89);
            this.MskDueDays.Mask = "00/00/0000";
            this.MskDueDays.Name = "MskDueDays";
            this.MskDueDays.Size = new System.Drawing.Size(117, 24);
            this.MskDueDays.TabIndex = 537;
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
            this.MskDate.Location = new System.Drawing.Point(459, 37);
            this.MskDate.Mask = "00/00/0000";
            this.MskDate.Name = "MskDate";
            this.MskDate.Size = new System.Drawing.Size(118, 24);
            this.MskDate.TabIndex = 528;
            this.MskDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalEnter_KeyPress);
            this.MskDate.Validating += new System.ComponentModel.CancelEventHandler(this.MskDate_Validating);
            // 
            // TxtCurrencyRate
            // 
            this.TxtCurrencyRate.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtCurrencyRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCurrencyRate.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.TxtCurrencyRate.Location = new System.Drawing.Point(996, 89);
            this.TxtCurrencyRate.MaxLength = 255;
            this.TxtCurrencyRate.Name = "TxtCurrencyRate";
            this.TxtCurrencyRate.Size = new System.Drawing.Size(57, 24);
            this.TxtCurrencyRate.TabIndex = 539;
            this.TxtCurrencyRate.Text = "1.00";
            this.TxtCurrencyRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtCurrencyRate.TextChanged += new System.EventHandler(this.TxtCurrencyRate_TextChanged);
            this.TxtCurrencyRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCurrencyRate_KeyPress);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.label28.Location = new System.Drawing.Point(748, 40);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(67, 19);
            this.label28.TabIndex = 559;
            this.label28.Text = "Ref Date";
            // 
            // lbl_Currency
            // 
            this.lbl_Currency.AutoSize = true;
            this.lbl_Currency.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.lbl_Currency.Location = new System.Drawing.Point(748, 92);
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
            this.MskRefDate.Location = new System.Drawing.Point(827, 38);
            this.MskRefDate.Mask = "00/00/0000";
            this.MskRefDate.Name = "MskRefDate";
            this.MskRefDate.Size = new System.Drawing.Size(108, 23);
            this.MskRefDate.TabIndex = 530;
            this.MskRefDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalEnter_KeyPress);
            // 
            // lbl_CurrencyRate
            // 
            this.lbl_CurrencyRate.AutoSize = true;
            this.lbl_CurrencyRate.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.lbl_CurrencyRate.Location = new System.Drawing.Point(952, 92);
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
            this.groupBox6.Location = new System.Drawing.Point(101, 106);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(953, 38);
            this.groupBox6.TabIndex = 564;
            this.groupBox6.TabStop = false;
            // 
            // LblBalanceType
            // 
            this.LblBalanceType.AutoSize = true;
            this.LblBalanceType.ForeColor = System.Drawing.Color.Black;
            this.LblBalanceType.Location = new System.Drawing.Point(426, 12);
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
            this.LblCreditDays.Location = new System.Drawing.Point(855, 12);
            this.LblCreditDays.Name = "LblCreditDays";
            this.LblCreditDays.Size = new System.Drawing.Size(84, 22);
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
            this.LblPanNo.Location = new System.Drawing.Point(73, 11);
            this.LblPanNo.Name = "LblPanNo";
            this.LblPanNo.Size = new System.Drawing.Size(127, 24);
            this.LblPanNo.TabIndex = 149;
            this.LblPanNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(6, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 19);
            this.label6.TabIndex = 150;
            this.label6.Text = "Pan No";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(756, 14);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(98, 19);
            this.label25.TabIndex = 174;
            this.label25.Text = "Credit Days";
            // 
            // lbl_Currentbal
            // 
            this.lbl_Currentbal.AutoSize = true;
            this.lbl_Currentbal.ForeColor = System.Drawing.Color.Black;
            this.lbl_Currentbal.Location = new System.Drawing.Point(209, 13);
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
            this.LblCreditLimit.Location = new System.Drawing.Point(607, 12);
            this.LblCreditLimit.Name = "LblCreditLimit";
            this.LblCreditLimit.Size = new System.Drawing.Size(137, 22);
            this.LblCreditLimit.TabIndex = 151;
            this.LblCreditLimit.Text = "0.00";
            this.LblCreditLimit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(495, 14);
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
            this.LblBalance.Location = new System.Drawing.Point(280, 10);
            this.LblBalance.Name = "LblBalance";
            this.LblBalance.Size = new System.Drawing.Size(144, 24);
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
            this.BtnNew.TabIndex = 17;
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
            this.BtnEdit.TabIndex = 18;
            this.BtnEdit.Tag = "EditButtonCheck";
            this.BtnEdit.Text = "&EDIT";
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(162, 2);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(102, 30);
            this.BtnDelete.TabIndex = 19;
            this.BtnDelete.Tag = "DeleteButtonCheck";
            this.BtnDelete.Text = "&DELETE";
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnPrintInvoice
            // 
            this.BtnPrintInvoice.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPrintInvoice.Appearance.Options.UseFont = true;
            this.BtnPrintInvoice.ImageOptions.Image = global::MrBLL.Properties.Resources.Printer24;
            this.BtnPrintInvoice.Location = new System.Drawing.Point(694, 2);
            this.BtnPrintInvoice.Name = "BtnPrintInvoice";
            this.BtnPrintInvoice.Size = new System.Drawing.Size(84, 30);
            this.BtnPrintInvoice.TabIndex = 21;
            this.BtnPrintInvoice.Text = "&PRINT";
            this.BtnPrintInvoice.Click += new System.EventHandler(this.BtnPrintInvoice_Click);
            // 
            // BtnReverse
            // 
            this.BtnReverse.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnReverse.Appearance.Options.UseFont = true;
            this.BtnReverse.ImageOptions.Image = global::MrBLL.Properties.Resources.reverse24;
            this.BtnReverse.Location = new System.Drawing.Point(778, 2);
            this.BtnReverse.Name = "BtnReverse";
            this.BtnReverse.Size = new System.Drawing.Size(116, 30);
            this.BtnReverse.TabIndex = 22;
            this.BtnReverse.Text = "&REVERSE";
            this.BtnReverse.Click += new System.EventHandler(this.BtnReverse_Click);
            // 
            // BtnCopy
            // 
            this.BtnCopy.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCopy.Appearance.Options.UseFont = true;
            this.BtnCopy.ImageOptions.Image = global::MrBLL.Properties.Resources.Copy24;
            this.BtnCopy.Location = new System.Drawing.Point(894, 2);
            this.BtnCopy.Name = "BtnCopy";
            this.BtnCopy.Size = new System.Drawing.Size(82, 30);
            this.BtnCopy.TabIndex = 23;
            this.BtnCopy.Text = "&COPY";
            this.BtnCopy.Click += new System.EventHandler(this.BtnCopy_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.AllowDrop = true;
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit24;
            this.BtnExit.Location = new System.Drawing.Point(977, 2);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(74, 30);
            this.BtnExit.TabIndex = 24;
            this.BtnExit.Text = "E&XIT";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnSalesInvoice
            // 
            this.BtnSalesInvoice.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSalesInvoice.Appearance.Options.UseFont = true;
            this.BtnSalesInvoice.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnSalesInvoice.Location = new System.Drawing.Point(604, 2);
            this.BtnSalesInvoice.Name = "BtnSalesInvoice";
            this.BtnSalesInvoice.Size = new System.Drawing.Size(90, 30);
            this.BtnSalesInvoice.TabIndex = 20;
            this.BtnSalesInvoice.Text = "&SALES";
            // 
            // FrmPurchaseOrderEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 521);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmPurchaseOrderEntry";
            this.ShowIcon = false;
            this.Tag = "Order";
            this.Text = "PURCHASE ORDER ENTRY";
            this.Load += new System.EventHandler(this.FrmPurchaseOrderEntry_Load);
            this.Shown += new System.EventHandler(this.FrmPurchaseOrderEntry_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmPurchaseOrderEntry_KeyPress);
            this.TabLedgerOpening.ResumeLayout(false);
            this.TabProductDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).EndInit();
            this.TabAttachment.ResumeLayout(false);
            this.TabAttachment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment1)).EndInit();
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
        private DevExpress.XtraEditors.SimpleButton BtnSalesInvoice;
        private System.Windows.Forms.Button BtnCurrency;
        private System.Windows.Forms.Label lbl_VoucherNo;
        private System.Windows.Forms.Label lbl_Class;
        public System.Windows.Forms.Label label31;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button BtnVendor;
        public System.Windows.Forms.Button BtnIndentNo;
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
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label lbl_Currency;
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
        private System.Windows.Forms.Label label8;
        private EntryGridViewEx DGrid;
        private MrPanel panel1;
        private MrTextBox TxtCurrency;
        private MrTextBox TxtIndentNo;
        private MrTextBox TxtDepartment;
        private MrTextBox TxtVendor;
        private MrTextBox TxtDueDays;
        private MrTextBox TxtAgent;
        private MrTextBox TxtSubledger;
        private MrTextBox TxtVno;
        private MrTextBox TxtRefVno;
        private MrMaskedTextBox MskMiti;
        private MrMaskedTextBox MskDueDays;
        private MrMaskedTextBox MskDate;
        private MrTextBox TxtCurrencyRate;
        private MrMaskedTextBox MskRefDate;
        private MrPanel panel2;
        private MrTextBox TxtBillTermAmount;
        private MrTextBox TxtRemarks;
        private MrMaskedTextBox MskIndentMiti;
    }
}