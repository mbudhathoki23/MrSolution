using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.DataEntry.PurchaseMaster
{
    partial class FrmPurchaseAdditional
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPurchaseAdditional));
            this.mrPanel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_Remarks = new System.Windows.Forms.Label();
            this.TxtRemarks = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_NoInWords = new System.Windows.Forms.Label();
            this.LblNumberInWords = new System.Windows.Forms.Label();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.label8 = new System.Windows.Forms.Label();
            this.TxtLocalInvoiceAmount = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtCurrencyRate = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtCurrency = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_Class = new System.Windows.Forms.Label();
            this.TxtDepartment = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnDepartment = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnPurchaseInvoice = new System.Windows.Forms.Button();
            this.TxtPurchaseInvoiceNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtInvoiceAmount = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.MskInvoiceMiti = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.MskInvoiceDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.clsSeparator3 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.lbl_VoucherNo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnVno = new System.Windows.Forms.Button();
            this.TxtVoucherNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.MskMiti = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.MskDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnPrintInvoice = new DevExpress.XtraEditors.SimpleButton();
            this.BtnReverse = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCopy = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.TabAdditionalTerm = new System.Windows.Forms.TabControl();
            this.TabProductWise = new System.Windows.Forms.TabPage();
            this.ProductGrid = new MrDAL.Control.ControlsEx.Control.EntryGridViewEx();
            this.GTxtSNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtProductId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtBasicQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtBasicAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtTermAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtNetAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mrGroup1 = new MrDAL.Control.ControlsEx.Control.MrGroup();
            this.LblTotalQty = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.LblTotalProductTerm = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.LblTotalBasicAmount = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.LblTotalNetAmount = new System.Windows.Forms.Label();
            this.TabBillWise = new System.Windows.Forms.TabPage();
            this.BillGrid = new MrDAL.Control.ControlsEx.Control.EntryGridViewEx();
            this.GTxtTermSno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtTermId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtTerm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtLedgerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtCbLedgerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtLedger = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtSubledgerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtSubLedger = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtTermType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtSign = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtInvoiceNetAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mrGroup2 = new MrDAL.Control.ControlsEx.Control.MrGroup();
            this.LblBillTermAmount = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.LblLedgerBalance = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.LblNetBillAmount = new System.Windows.Forms.Label();
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
            this.mrPanel1.SuspendLayout();
            this.TabAdditionalTerm.SuspendLayout();
            this.TabProductWise.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProductGrid)).BeginInit();
            this.mrGroup1.SuspendLayout();
            this.TabBillWise.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BillGrid)).BeginInit();
            this.mrGroup2.SuspendLayout();
            this.TabAttachment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment1)).BeginInit();
            this.SuspendLayout();
            // 
            // mrPanel1
            // 
            this.mrPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel1.Controls.Add(this.BtnCancel);
            this.mrPanel1.Controls.Add(this.lbl_Remarks);
            this.mrPanel1.Controls.Add(this.TxtRemarks);
            this.mrPanel1.Controls.Add(this.lbl_NoInWords);
            this.mrPanel1.Controls.Add(this.LblNumberInWords);
            this.mrPanel1.Controls.Add(this.BtnSave);
            this.mrPanel1.Controls.Add(this.label8);
            this.mrPanel1.Controls.Add(this.TxtLocalInvoiceAmount);
            this.mrPanel1.Controls.Add(this.label7);
            this.mrPanel1.Controls.Add(this.TxtCurrencyRate);
            this.mrPanel1.Controls.Add(this.label6);
            this.mrPanel1.Controls.Add(this.TxtCurrency);
            this.mrPanel1.Controls.Add(this.lbl_Class);
            this.mrPanel1.Controls.Add(this.TxtDepartment);
            this.mrPanel1.Controls.Add(this.BtnDepartment);
            this.mrPanel1.Controls.Add(this.label2);
            this.mrPanel1.Controls.Add(this.label3);
            this.mrPanel1.Controls.Add(this.BtnPurchaseInvoice);
            this.mrPanel1.Controls.Add(this.TxtPurchaseInvoiceNo);
            this.mrPanel1.Controls.Add(this.label4);
            this.mrPanel1.Controls.Add(this.TxtInvoiceAmount);
            this.mrPanel1.Controls.Add(this.MskInvoiceMiti);
            this.mrPanel1.Controls.Add(this.label5);
            this.mrPanel1.Controls.Add(this.MskInvoiceDate);
            this.mrPanel1.Controls.Add(this.clsSeparator3);
            this.mrPanel1.Controls.Add(this.lbl_VoucherNo);
            this.mrPanel1.Controls.Add(this.label1);
            this.mrPanel1.Controls.Add(this.BtnVno);
            this.mrPanel1.Controls.Add(this.TxtVoucherNo);
            this.mrPanel1.Controls.Add(this.MskMiti);
            this.mrPanel1.Controls.Add(this.label11);
            this.mrPanel1.Controls.Add(this.MskDate);
            this.mrPanel1.Controls.Add(this.clsSeparator1);
            this.mrPanel1.Controls.Add(this.clsSeparator2);
            this.mrPanel1.Controls.Add(this.BtnNew);
            this.mrPanel1.Controls.Add(this.BtnEdit);
            this.mrPanel1.Controls.Add(this.BtnDelete);
            this.mrPanel1.Controls.Add(this.BtnPrintInvoice);
            this.mrPanel1.Controls.Add(this.BtnReverse);
            this.mrPanel1.Controls.Add(this.BtnCopy);
            this.mrPanel1.Controls.Add(this.BtnExit);
            this.mrPanel1.Controls.Add(this.TabAdditionalTerm);
            this.mrPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrPanel1.Location = new System.Drawing.Point(0, 0);
            this.mrPanel1.Name = "mrPanel1";
            this.mrPanel1.Size = new System.Drawing.Size(944, 469);
            this.mrPanel1.TabIndex = 0;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(836, 430);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(104, 35);
            this.BtnCancel.TabIndex = 619;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // lbl_Remarks
            // 
            this.lbl_Remarks.AutoSize = true;
            this.lbl_Remarks.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Remarks.Location = new System.Drawing.Point(2, 404);
            this.lbl_Remarks.Name = "lbl_Remarks";
            this.lbl_Remarks.Size = new System.Drawing.Size(76, 19);
            this.lbl_Remarks.TabIndex = 620;
            this.lbl_Remarks.Text = "Remarks";
            // 
            // TxtRemarks
            // 
            this.TxtRemarks.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtRemarks.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtRemarks.Location = new System.Drawing.Point(94, 403);
            this.TxtRemarks.MaxLength = 255;
            this.TxtRemarks.Name = "TxtRemarks";
            this.TxtRemarks.Size = new System.Drawing.Size(846, 24);
            this.TxtRemarks.TabIndex = 617;
            // 
            // lbl_NoInWords
            // 
            this.lbl_NoInWords.AutoSize = true;
            this.lbl_NoInWords.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NoInWords.Location = new System.Drawing.Point(2, 438);
            this.lbl_NoInWords.Name = "lbl_NoInWords";
            this.lbl_NoInWords.Size = new System.Drawing.Size(91, 19);
            this.lbl_NoInWords.TabIndex = 621;
            this.lbl_NoInWords.Text = "In Words :-";
            // 
            // LblNumberInWords
            // 
            this.LblNumberInWords.BackColor = System.Drawing.Color.White;
            this.LblNumberInWords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblNumberInWords.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNumberInWords.ForeColor = System.Drawing.SystemColors.Desktop;
            this.LblNumberInWords.Location = new System.Drawing.Point(100, 431);
            this.LblNumberInWords.Name = "LblNumberInWords";
            this.LblNumberInWords.Size = new System.Drawing.Size(642, 28);
            this.LblNumberInWords.TabIndex = 622;
            this.LblNumberInWords.Text = "Only.";
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(748, 430);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(87, 35);
            this.BtnSave.TabIndex = 618;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.label8.Location = new System.Drawing.Point(620, 99);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 19);
            this.label8.TabIndex = 616;
            this.label8.Text = "Local Amount";
            // 
            // TxtLocalInvoiceAmount
            // 
            this.TxtLocalInvoiceAmount.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtLocalInvoiceAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtLocalInvoiceAmount.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.TxtLocalInvoiceAmount.Location = new System.Drawing.Point(753, 97);
            this.TxtLocalInvoiceAmount.MaxLength = 255;
            this.TxtLocalInvoiceAmount.Name = "TxtLocalInvoiceAmount";
            this.TxtLocalInvoiceAmount.Size = new System.Drawing.Size(163, 23);
            this.TxtLocalInvoiceAmount.TabIndex = 17;
            this.TxtLocalInvoiceAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.label7.Location = new System.Drawing.Point(228, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 19);
            this.label7.TabIndex = 614;
            this.label7.Text = "Exchange Rate";
            // 
            // TxtCurrencyRate
            // 
            this.TxtCurrencyRate.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtCurrencyRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCurrencyRate.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.TxtCurrencyRate.Location = new System.Drawing.Point(345, 97);
            this.TxtCurrencyRate.MaxLength = 255;
            this.TxtCurrencyRate.Name = "TxtCurrencyRate";
            this.TxtCurrencyRate.Size = new System.Drawing.Size(127, 23);
            this.TxtCurrencyRate.TabIndex = 16;
            this.TxtCurrencyRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.label6.Location = new System.Drawing.Point(7, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 19);
            this.label6.TabIndex = 612;
            this.label6.Text = "Currency";
            // 
            // TxtCurrency
            // 
            this.TxtCurrency.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtCurrency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCurrency.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.TxtCurrency.Location = new System.Drawing.Point(94, 97);
            this.TxtCurrency.MaxLength = 255;
            this.TxtCurrency.Name = "TxtCurrency";
            this.TxtCurrency.Size = new System.Drawing.Size(127, 23);
            this.TxtCurrency.TabIndex = 15;
            // 
            // lbl_Class
            // 
            this.lbl_Class.AutoSize = true;
            this.lbl_Class.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.lbl_Class.Location = new System.Drawing.Point(580, 41);
            this.lbl_Class.Name = "lbl_Class";
            this.lbl_Class.Size = new System.Drawing.Size(99, 19);
            this.lbl_Class.TabIndex = 609;
            this.lbl_Class.Text = "Department";
            // 
            // TxtDepartment
            // 
            this.TxtDepartment.BackColor = System.Drawing.Color.White;
            this.TxtDepartment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDepartment.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.TxtDepartment.Location = new System.Drawing.Point(679, 39);
            this.TxtDepartment.MaxLength = 255;
            this.TxtDepartment.Name = "TxtDepartment";
            this.TxtDepartment.ReadOnly = true;
            this.TxtDepartment.Size = new System.Drawing.Size(237, 24);
            this.TxtDepartment.TabIndex = 10;
            this.TxtDepartment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDepartment_KeyDown);
            this.TxtDepartment.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDepartment_Validating);
            // 
            // BtnDepartment
            // 
            this.BtnDepartment.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.BtnDepartment.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDepartment.Location = new System.Drawing.Point(915, 39);
            this.BtnDepartment.Name = "BtnDepartment";
            this.BtnDepartment.Size = new System.Drawing.Size(26, 24);
            this.BtnDepartment.TabIndex = 610;
            this.BtnDepartment.TabStop = false;
            this.BtnDepartment.UseVisualStyleBackColor = false;
            this.BtnDepartment.Click += new System.EventHandler(this.BtnDepartment_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label2.Location = new System.Drawing.Point(7, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 19);
            this.label2.TabIndex = 603;
            this.label2.Text = "Invoice No";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label3.Location = new System.Drawing.Point(410, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 19);
            this.label3.TabIndex = 604;
            this.label3.Text = "Date";
            // 
            // BtnPurchaseInvoice
            // 
            this.BtnPurchaseInvoice.CausesValidation = false;
            this.BtnPurchaseInvoice.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.BtnPurchaseInvoice.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnPurchaseInvoice.Location = new System.Drawing.Point(221, 70);
            this.BtnPurchaseInvoice.Name = "BtnPurchaseInvoice";
            this.BtnPurchaseInvoice.Size = new System.Drawing.Size(27, 26);
            this.BtnPurchaseInvoice.TabIndex = 598;
            this.BtnPurchaseInvoice.TabStop = false;
            this.BtnPurchaseInvoice.UseVisualStyleBackColor = false;
            this.BtnPurchaseInvoice.Click += new System.EventHandler(this.BtnPurchaseInvoice_Click);
            // 
            // TxtPurchaseInvoiceNo
            // 
            this.TxtPurchaseInvoiceNo.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtPurchaseInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPurchaseInvoiceNo.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.TxtPurchaseInvoiceNo.Location = new System.Drawing.Point(94, 71);
            this.TxtPurchaseInvoiceNo.MaxLength = 50;
            this.TxtPurchaseInvoiceNo.Name = "TxtPurchaseInvoiceNo";
            this.TxtPurchaseInvoiceNo.Size = new System.Drawing.Size(127, 24);
            this.TxtPurchaseInvoiceNo.TabIndex = 11;
            this.TxtPurchaseInvoiceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPurchaseInvoiceNo_KeyDown);
            this.TxtPurchaseInvoiceNo.Validating += new System.ComponentModel.CancelEventHandler(this.TxtPurchaseInvoiceNo_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.label4.Location = new System.Drawing.Point(620, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 19);
            this.label4.TabIndex = 606;
            this.label4.Text = "Invoice Amount";
            // 
            // TxtInvoiceAmount
            // 
            this.TxtInvoiceAmount.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtInvoiceAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtInvoiceAmount.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.TxtInvoiceAmount.Location = new System.Drawing.Point(753, 70);
            this.TxtInvoiceAmount.MaxLength = 255;
            this.TxtInvoiceAmount.Name = "TxtInvoiceAmount";
            this.TxtInvoiceAmount.Size = new System.Drawing.Size(163, 23);
            this.TxtInvoiceAmount.TabIndex = 14;
            this.TxtInvoiceAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // MskInvoiceMiti
            // 
            this.MskInvoiceMiti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskInvoiceMiti.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.MskInvoiceMiti.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskInvoiceMiti.Location = new System.Drawing.Point(288, 71);
            this.MskInvoiceMiti.Mask = "00/00/0000";
            this.MskInvoiceMiti.Name = "MskInvoiceMiti";
            this.MskInvoiceMiti.Size = new System.Drawing.Size(118, 24);
            this.MskInvoiceMiti.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label5.Location = new System.Drawing.Point(250, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 19);
            this.label5.TabIndex = 607;
            this.label5.Text = "Miti";
            // 
            // MskInvoiceDate
            // 
            this.MskInvoiceDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskInvoiceDate.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.MskInvoiceDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskInvoiceDate.Location = new System.Drawing.Point(458, 71);
            this.MskInvoiceDate.Mask = "00/00/0000";
            this.MskInvoiceDate.Name = "MskInvoiceDate";
            this.MskInvoiceDate.Size = new System.Drawing.Size(118, 24);
            this.MskInvoiceDate.TabIndex = 13;
            // 
            // clsSeparator3
            // 
            this.clsSeparator3.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator3.Location = new System.Drawing.Point(6, 65);
            this.clsSeparator3.Name = "clsSeparator3";
            this.clsSeparator3.Size = new System.Drawing.Size(935, 2);
            this.clsSeparator3.TabIndex = 585;
            this.clsSeparator3.TabStop = false;
            // 
            // lbl_VoucherNo
            // 
            this.lbl_VoucherNo.AutoSize = true;
            this.lbl_VoucherNo.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.lbl_VoucherNo.Location = new System.Drawing.Point(7, 42);
            this.lbl_VoucherNo.Name = "lbl_VoucherNo";
            this.lbl_VoucherNo.Size = new System.Drawing.Size(85, 19);
            this.lbl_VoucherNo.TabIndex = 592;
            this.lbl_VoucherNo.Text = "Invoice No";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label1.Location = new System.Drawing.Point(410, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 19);
            this.label1.TabIndex = 593;
            this.label1.Text = "Date";
            // 
            // BtnVno
            // 
            this.BtnVno.CausesValidation = false;
            this.BtnVno.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.BtnVno.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnVno.Location = new System.Drawing.Point(221, 38);
            this.BtnVno.Name = "BtnVno";
            this.BtnVno.Size = new System.Drawing.Size(27, 26);
            this.BtnVno.TabIndex = 587;
            this.BtnVno.TabStop = false;
            this.BtnVno.UseVisualStyleBackColor = false;
            this.BtnVno.Click += new System.EventHandler(this.BtnVno_Click);
            // 
            // TxtVoucherNo
            // 
            this.TxtVoucherNo.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtVoucherNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtVoucherNo.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.TxtVoucherNo.Location = new System.Drawing.Point(94, 39);
            this.TxtVoucherNo.MaxLength = 50;
            this.TxtVoucherNo.Name = "TxtVoucherNo";
            this.TxtVoucherNo.Size = new System.Drawing.Size(127, 24);
            this.TxtVoucherNo.TabIndex = 7;
            this.TxtVoucherNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtAdditionalVoucherNo_KeyDown);
            this.TxtVoucherNo.Validating += new System.ComponentModel.CancelEventHandler(this.TxtAdditionalVoucherNo_Validating);
            // 
            // MskMiti
            // 
            this.MskMiti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskMiti.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.MskMiti.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskMiti.Location = new System.Drawing.Point(288, 39);
            this.MskMiti.Mask = "00/00/0000";
            this.MskMiti.Name = "MskMiti";
            this.MskMiti.Size = new System.Drawing.Size(118, 24);
            this.MskMiti.TabIndex = 8;
            this.MskMiti.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyPress);
            this.MskMiti.Validating += new System.ComponentModel.CancelEventHandler(this.MskMiti_Validating);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label11.Location = new System.Drawing.Point(250, 42);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 19);
            this.label11.TabIndex = 596;
            this.label11.Text = "Miti";
            // 
            // MskDate
            // 
            this.MskDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskDate.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.MskDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskDate.Location = new System.Drawing.Point(458, 39);
            this.MskDate.Mask = "00/00/0000";
            this.MskDate.Name = "MskDate";
            this.MskDate.Size = new System.Drawing.Size(118, 24);
            this.MskDate.TabIndex = 9;
            this.MskDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyPress);
            this.MskDate.Validating += new System.ComponentModel.CancelEventHandler(this.MskDate_Validating);
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(3, 123);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(938, 2);
            this.clsSeparator1.TabIndex = 585;
            this.clsSeparator1.TabStop = false;
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(3, 35);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(938, 2);
            this.clsSeparator2.TabIndex = 584;
            this.clsSeparator2.TabStop = false;
            // 
            // BtnNew
            // 
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnNew.Location = new System.Drawing.Point(3, 3);
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
            this.BtnEdit.Location = new System.Drawing.Point(84, 3);
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
            this.BtnDelete.Location = new System.Drawing.Point(160, 3);
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
            this.BtnPrintInvoice.ImageOptions.Image = global::MrBLL.Properties.Resources.Print_16;
            this.BtnPrintInvoice.Location = new System.Drawing.Point(584, 3);
            this.BtnPrintInvoice.Name = "BtnPrintInvoice";
            this.BtnPrintInvoice.Size = new System.Drawing.Size(84, 30);
            this.BtnPrintInvoice.TabIndex = 3;
            this.BtnPrintInvoice.Text = "&PRINT";
            this.BtnPrintInvoice.Click += new System.EventHandler(this.BtnPrintInvoice_Click);
            // 
            // BtnReverse
            // 
            this.BtnReverse.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnReverse.Appearance.Options.UseFont = true;
            this.BtnReverse.ImageOptions.Image = global::MrBLL.Properties.Resources.Return;
            this.BtnReverse.Location = new System.Drawing.Point(668, 3);
            this.BtnReverse.Name = "BtnReverse";
            this.BtnReverse.Size = new System.Drawing.Size(116, 30);
            this.BtnReverse.TabIndex = 4;
            this.BtnReverse.Text = "&REVERSE";
            this.BtnReverse.Click += new System.EventHandler(this.BtnReverse_Click);
            // 
            // BtnCopy
            // 
            this.BtnCopy.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCopy.Appearance.Options.UseFont = true;
            this.BtnCopy.ImageOptions.Image = global::MrBLL.Properties.Resources.Copy;
            this.BtnCopy.Location = new System.Drawing.Point(784, 3);
            this.BtnCopy.Name = "BtnCopy";
            this.BtnCopy.Size = new System.Drawing.Size(82, 30);
            this.BtnCopy.TabIndex = 5;
            this.BtnCopy.Text = "&COPY";
            this.BtnCopy.Click += new System.EventHandler(this.BtnCopy_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.AllowDrop = true;
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.BtnExit.Location = new System.Drawing.Point(867, 3);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(74, 30);
            this.BtnExit.TabIndex = 6;
            this.BtnExit.Text = "E&XIT";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // TabAdditionalTerm
            // 
            this.TabAdditionalTerm.Controls.Add(this.TabProductWise);
            this.TabAdditionalTerm.Controls.Add(this.TabBillWise);
            this.TabAdditionalTerm.Controls.Add(this.TabAttachment);
            this.TabAdditionalTerm.Location = new System.Drawing.Point(3, 129);
            this.TabAdditionalTerm.Name = "TabAdditionalTerm";
            this.TabAdditionalTerm.SelectedIndex = 0;
            this.TabAdditionalTerm.Size = new System.Drawing.Size(937, 270);
            this.TabAdditionalTerm.TabIndex = 18;
            // 
            // TabProductWise
            // 
            this.TabProductWise.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.TabProductWise.Controls.Add(this.ProductGrid);
            this.TabProductWise.Controls.Add(this.mrGroup1);
            this.TabProductWise.Location = new System.Drawing.Point(4, 28);
            this.TabProductWise.Name = "TabProductWise";
            this.TabProductWise.Padding = new System.Windows.Forms.Padding(3);
            this.TabProductWise.Size = new System.Drawing.Size(929, 238);
            this.TabProductWise.TabIndex = 0;
            this.TabProductWise.Text = "Product Wise Term";
            // 
            // ProductGrid
            // 
            this.ProductGrid.AllowUserToAddRows = false;
            this.ProductGrid.AllowUserToDeleteRows = false;
            this.ProductGrid.AllowUserToResizeColumns = false;
            this.ProductGrid.AllowUserToResizeRows = false;
            this.ProductGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.ProductGrid.BlockNavigationOnNextRowOnEnter = true;
            this.ProductGrid.CausesValidation = false;
            this.ProductGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.ProductGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.ProductGrid.ColumnHeadersHeight = 25;
            this.ProductGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GTxtSNo,
            this.GTxtProductId,
            this.GTxtProduct,
            this.GTxtBasicQty,
            this.GTxtBasicAmount,
            this.GTxtTermAmount,
            this.GTxtNetAmount});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ProductGrid.DefaultCellStyle = dataGridViewCellStyle6;
            this.ProductGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProductGrid.DoubleBufferEnabled = true;
            this.ProductGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.ProductGrid.Location = new System.Drawing.Point(3, 3);
            this.ProductGrid.MultiSelect = false;
            this.ProductGrid.Name = "ProductGrid";
            this.ProductGrid.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Bookman Old Style", 9F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ProductGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.ProductGrid.RowHeadersVisible = false;
            this.ProductGrid.RowHeadersWidth = 51;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            this.ProductGrid.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.ProductGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProductGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ProductGrid.Size = new System.Drawing.Size(923, 190);
            this.ProductGrid.StandardTab = true;
            this.ProductGrid.TabIndex = 0;
            this.ProductGrid.TabStop = false;
            this.ProductGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProductGrid_KeyDown);
            // 
            // GTxtSNo
            // 
            this.GTxtSNo.HeaderText = "SNO";
            this.GTxtSNo.MinimumWidth = 6;
            this.GTxtSNo.Name = "GTxtSNo";
            this.GTxtSNo.ReadOnly = true;
            this.GTxtSNo.Width = 65;
            // 
            // GTxtProductId
            // 
            this.GTxtProductId.HeaderText = "ProductId";
            this.GTxtProductId.MinimumWidth = 6;
            this.GTxtProductId.Name = "GTxtProductId";
            this.GTxtProductId.ReadOnly = true;
            this.GTxtProductId.Visible = false;
            this.GTxtProductId.Width = 6;
            // 
            // GTxtProduct
            // 
            this.GTxtProduct.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GTxtProduct.DefaultCellStyle = dataGridViewCellStyle1;
            this.GTxtProduct.HeaderText = "PRODUCT";
            this.GTxtProduct.MinimumWidth = 6;
            this.GTxtProduct.Name = "GTxtProduct";
            this.GTxtProduct.ReadOnly = true;
            // 
            // GTxtBasicQty
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GTxtBasicQty.DefaultCellStyle = dataGridViewCellStyle2;
            this.GTxtBasicQty.HeaderText = "QTY";
            this.GTxtBasicQty.MinimumWidth = 6;
            this.GTxtBasicQty.Name = "GTxtBasicQty";
            this.GTxtBasicQty.ReadOnly = true;
            this.GTxtBasicQty.Visible = false;
            this.GTxtBasicQty.Width = 6;
            // 
            // GTxtBasicAmount
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GTxtBasicAmount.DefaultCellStyle = dataGridViewCellStyle3;
            this.GTxtBasicAmount.HeaderText = "AMOUNT";
            this.GTxtBasicAmount.MinimumWidth = 6;
            this.GTxtBasicAmount.Name = "GTxtBasicAmount";
            this.GTxtBasicAmount.ReadOnly = true;
            this.GTxtBasicAmount.Width = 150;
            // 
            // GTxtTermAmount
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GTxtTermAmount.DefaultCellStyle = dataGridViewCellStyle4;
            this.GTxtTermAmount.HeaderText = "TERM";
            this.GTxtTermAmount.MinimumWidth = 6;
            this.GTxtTermAmount.Name = "GTxtTermAmount";
            this.GTxtTermAmount.ReadOnly = true;
            this.GTxtTermAmount.Width = 150;
            // 
            // GTxtNetAmount
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GTxtNetAmount.DefaultCellStyle = dataGridViewCellStyle5;
            this.GTxtNetAmount.HeaderText = "NET_AMOUNT";
            this.GTxtNetAmount.MinimumWidth = 6;
            this.GTxtNetAmount.Name = "GTxtNetAmount";
            this.GTxtNetAmount.ReadOnly = true;
            this.GTxtNetAmount.Width = 150;
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrDAL.Control.ControlsEx.Control.MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.LblTotalQty);
            this.mrGroup1.Controls.Add(this.label18);
            this.mrGroup1.Controls.Add(this.LblTotalProductTerm);
            this.mrGroup1.Controls.Add(this.label24);
            this.mrGroup1.Controls.Add(this.LblTotalBasicAmount);
            this.mrGroup1.Controls.Add(this.label9);
            this.mrGroup1.Controls.Add(this.label10);
            this.mrGroup1.Controls.Add(this.LblTotalNetAmount);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "";
            this.mrGroup1.Location = new System.Drawing.Point(3, 193);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 5;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = false;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(923, 42);
            this.mrGroup1.TabIndex = 0;
            // 
            // LblTotalQty
            // 
            this.LblTotalQty.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LblTotalQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTotalQty.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotalQty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblTotalQty.Location = new System.Drawing.Point(46, 16);
            this.LblTotalQty.Name = "LblTotalQty";
            this.LblTotalQty.Size = new System.Drawing.Size(89, 23);
            this.LblTotalQty.TabIndex = 247;
            this.LblTotalQty.Text = "0.00";
            this.LblTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(0, 18);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(35, 19);
            this.label18.TabIndex = 248;
            this.label18.Text = "Qty";
            // 
            // LblTotalProductTerm
            // 
            this.LblTotalProductTerm.BackColor = System.Drawing.Color.White;
            this.LblTotalProductTerm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTotalProductTerm.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotalProductTerm.ForeColor = System.Drawing.Color.Black;
            this.LblTotalProductTerm.Location = new System.Drawing.Point(498, 16);
            this.LblTotalProductTerm.MaxLength = 18;
            this.LblTotalProductTerm.Name = "LblTotalProductTerm";
            this.LblTotalProductTerm.ReadOnly = true;
            this.LblTotalProductTerm.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LblTotalProductTerm.Size = new System.Drawing.Size(139, 23);
            this.LblTotalProductTerm.TabIndex = 240;
            this.LblTotalProductTerm.Text = "0.00";
            this.LblTotalProductTerm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(637, 18);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(140, 19);
            this.label24.TabIndex = 241;
            this.label24.Text = "Total Net Amount";
            // 
            // LblTotalBasicAmount
            // 
            this.LblTotalBasicAmount.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LblTotalBasicAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTotalBasicAmount.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotalBasicAmount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblTotalBasicAmount.Location = new System.Drawing.Point(248, 16);
            this.LblTotalBasicAmount.Name = "LblTotalBasicAmount";
            this.LblTotalBasicAmount.Size = new System.Drawing.Size(139, 23);
            this.LblTotalBasicAmount.TabIndex = 243;
            this.LblTotalBasicAmount.Text = "0.00";
            this.LblTotalBasicAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(135, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(113, 19);
            this.label9.TabIndex = 244;
            this.label9.Text = "Basic Amount";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(387, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(111, 19);
            this.label10.TabIndex = 245;
            this.label10.Text = "Term Amount";
            // 
            // LblTotalNetAmount
            // 
            this.LblTotalNetAmount.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LblTotalNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTotalNetAmount.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotalNetAmount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblTotalNetAmount.Location = new System.Drawing.Point(777, 16);
            this.LblTotalNetAmount.Name = "LblTotalNetAmount";
            this.LblTotalNetAmount.Size = new System.Drawing.Size(139, 23);
            this.LblTotalNetAmount.TabIndex = 246;
            this.LblTotalNetAmount.Text = "0.00";
            this.LblTotalNetAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TabBillWise
            // 
            this.TabBillWise.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.TabBillWise.Controls.Add(this.BillGrid);
            this.TabBillWise.Controls.Add(this.mrGroup2);
            this.TabBillWise.Location = new System.Drawing.Point(4, 28);
            this.TabBillWise.Name = "TabBillWise";
            this.TabBillWise.Padding = new System.Windows.Forms.Padding(3);
            this.TabBillWise.Size = new System.Drawing.Size(929, 238);
            this.TabBillWise.TabIndex = 1;
            this.TabBillWise.Text = "Billing Terms";
            // 
            // BillGrid
            // 
            this.BillGrid.AllowUserToAddRows = false;
            this.BillGrid.AllowUserToDeleteRows = false;
            this.BillGrid.AllowUserToResizeColumns = false;
            this.BillGrid.AllowUserToResizeRows = false;
            this.BillGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.BillGrid.BlockNavigationOnNextRowOnEnter = true;
            this.BillGrid.CausesValidation = false;
            this.BillGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.BillGrid.ColumnHeadersHeight = 25;
            this.BillGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GTxtTermSno,
            this.GTxtTermId,
            this.GTxtTerm,
            this.GTxtLedgerId,
            this.GTxtCbLedgerId,
            this.GTxtLedger,
            this.GTxtSubledgerId,
            this.GTxtSubLedger,
            this.GTxtTermType,
            this.GTxtSign,
            this.GTxtRate,
            this.GTxtAmount,
            this.GTxtInvoiceNetAmount});
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.BillGrid.DefaultCellStyle = dataGridViewCellStyle14;
            this.BillGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BillGrid.DoubleBufferEnabled = true;
            this.BillGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.BillGrid.Location = new System.Drawing.Point(3, 3);
            this.BillGrid.MultiSelect = false;
            this.BillGrid.Name = "BillGrid";
            this.BillGrid.ReadOnly = true;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Bookman Old Style", 9F);
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.BillGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.BillGrid.RowHeadersVisible = false;
            this.BillGrid.RowHeadersWidth = 51;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            this.BillGrid.RowsDefaultCellStyle = dataGridViewCellStyle16;
            this.BillGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BillGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.BillGrid.Size = new System.Drawing.Size(923, 190);
            this.BillGrid.StandardTab = true;
            this.BillGrid.TabIndex = 2;
            this.BillGrid.TabStop = false;
            this.BillGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BillGrid_KeyDown);
            // 
            // GTxtTermSno
            // 
            this.GTxtTermSno.HeaderText = "SNO";
            this.GTxtTermSno.MinimumWidth = 6;
            this.GTxtTermSno.Name = "GTxtTermSno";
            this.GTxtTermSno.ReadOnly = true;
            this.GTxtTermSno.Width = 65;
            // 
            // GTxtTermId
            // 
            this.GTxtTermId.HeaderText = "TermId";
            this.GTxtTermId.MinimumWidth = 6;
            this.GTxtTermId.Name = "GTxtTermId";
            this.GTxtTermId.ReadOnly = true;
            this.GTxtTermId.Visible = false;
            this.GTxtTermId.Width = 6;
            // 
            // GTxtTerm
            // 
            this.GTxtTerm.HeaderText = "TERM";
            this.GTxtTerm.MinimumWidth = 6;
            this.GTxtTerm.Name = "GTxtTerm";
            this.GTxtTerm.ReadOnly = true;
            this.GTxtTerm.Width = 150;
            // 
            // GTxtLedgerId
            // 
            this.GTxtLedgerId.HeaderText = "LedgerId";
            this.GTxtLedgerId.MinimumWidth = 6;
            this.GTxtLedgerId.Name = "GTxtLedgerId";
            this.GTxtLedgerId.ReadOnly = true;
            this.GTxtLedgerId.Visible = false;
            this.GTxtLedgerId.Width = 6;
            // 
            // GTxtCbLedgerId
            // 
            this.GTxtCbLedgerId.HeaderText = "CbLedgerId";
            this.GTxtCbLedgerId.MinimumWidth = 6;
            this.GTxtCbLedgerId.Name = "GTxtCbLedgerId";
            this.GTxtCbLedgerId.ReadOnly = true;
            this.GTxtCbLedgerId.Visible = false;
            this.GTxtCbLedgerId.Width = 125;
            // 
            // GTxtLedger
            // 
            this.GTxtLedger.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.GTxtLedger.HeaderText = "LEDGER";
            this.GTxtLedger.MinimumWidth = 6;
            this.GTxtLedger.Name = "GTxtLedger";
            this.GTxtLedger.ReadOnly = true;
            // 
            // GTxtSubledgerId
            // 
            this.GTxtSubledgerId.HeaderText = "SubledgerId";
            this.GTxtSubledgerId.MinimumWidth = 6;
            this.GTxtSubledgerId.Name = "GTxtSubledgerId";
            this.GTxtSubledgerId.ReadOnly = true;
            this.GTxtSubledgerId.Visible = false;
            this.GTxtSubledgerId.Width = 6;
            // 
            // GTxtSubLedger
            // 
            this.GTxtSubLedger.HeaderText = "SUB_LEDGER";
            this.GTxtSubLedger.MinimumWidth = 6;
            this.GTxtSubLedger.Name = "GTxtSubLedger";
            this.GTxtSubLedger.ReadOnly = true;
            this.GTxtSubLedger.Visible = false;
            this.GTxtSubLedger.Width = 125;
            // 
            // GTxtTermType
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.GTxtTermType.DefaultCellStyle = dataGridViewCellStyle9;
            this.GTxtTermType.HeaderText = "TT";
            this.GTxtTermType.MinimumWidth = 6;
            this.GTxtTermType.Name = "GTxtTermType";
            this.GTxtTermType.ReadOnly = true;
            this.GTxtTermType.Width = 45;
            // 
            // GTxtSign
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.GTxtSign.DefaultCellStyle = dataGridViewCellStyle10;
            this.GTxtSign.HeaderText = "SIGN";
            this.GTxtSign.MinimumWidth = 6;
            this.GTxtSign.Name = "GTxtSign";
            this.GTxtSign.ReadOnly = true;
            this.GTxtSign.Width = 65;
            // 
            // GTxtRate
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GTxtRate.DefaultCellStyle = dataGridViewCellStyle11;
            this.GTxtRate.HeaderText = "RATE";
            this.GTxtRate.MinimumWidth = 6;
            this.GTxtRate.Name = "GTxtRate";
            this.GTxtRate.ReadOnly = true;
            this.GTxtRate.Width = 120;
            // 
            // GTxtAmount
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GTxtAmount.DefaultCellStyle = dataGridViewCellStyle12;
            this.GTxtAmount.HeaderText = "AMOUNT";
            this.GTxtAmount.MinimumWidth = 6;
            this.GTxtAmount.Name = "GTxtAmount";
            this.GTxtAmount.ReadOnly = true;
            this.GTxtAmount.Width = 120;
            // 
            // GTxtInvoiceNetAmount
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GTxtInvoiceNetAmount.DefaultCellStyle = dataGridViewCellStyle13;
            this.GTxtInvoiceNetAmount.HeaderText = "NET_AMOUNT";
            this.GTxtInvoiceNetAmount.MinimumWidth = 6;
            this.GTxtInvoiceNetAmount.Name = "GTxtInvoiceNetAmount";
            this.GTxtInvoiceNetAmount.ReadOnly = true;
            this.GTxtInvoiceNetAmount.Width = 150;
            // 
            // mrGroup2
            // 
            this.mrGroup2.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup2.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup2.BackgroundGradientMode = MrDAL.Control.ControlsEx.Control.MrGroup.GroupBoxGradientMode.None;
            this.mrGroup2.BorderColor = System.Drawing.Color.White;
            this.mrGroup2.BorderThickness = 1F;
            this.mrGroup2.Controls.Add(this.LblBillTermAmount);
            this.mrGroup2.Controls.Add(this.label12);
            this.mrGroup2.Controls.Add(this.LblLedgerBalance);
            this.mrGroup2.Controls.Add(this.label14);
            this.mrGroup2.Controls.Add(this.label15);
            this.mrGroup2.Controls.Add(this.LblNetBillAmount);
            this.mrGroup2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mrGroup2.GroupImage = null;
            this.mrGroup2.GroupTitle = "";
            this.mrGroup2.Location = new System.Drawing.Point(3, 193);
            this.mrGroup2.Name = "mrGroup2";
            this.mrGroup2.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup2.PaintGroupBox = false;
            this.mrGroup2.RoundCorners = 5;
            this.mrGroup2.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup2.ShadowControl = false;
            this.mrGroup2.ShadowThickness = 3;
            this.mrGroup2.Size = new System.Drawing.Size(923, 42);
            this.mrGroup2.TabIndex = 1;
            // 
            // LblBillTermAmount
            // 
            this.LblBillTermAmount.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LblBillTermAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblBillTermAmount.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblBillTermAmount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblBillTermAmount.Location = new System.Drawing.Point(500, 14);
            this.LblBillTermAmount.Name = "LblBillTermAmount";
            this.LblBillTermAmount.Size = new System.Drawing.Size(139, 23);
            this.LblBillTermAmount.TabIndex = 253;
            this.LblBillTermAmount.Text = "0.00";
            this.LblBillTermAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(639, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(140, 19);
            this.label12.TabIndex = 248;
            this.label12.Text = "Total Net Amount";
            // 
            // LblLedgerBalance
            // 
            this.LblLedgerBalance.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LblLedgerBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblLedgerBalance.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblLedgerBalance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblLedgerBalance.Location = new System.Drawing.Point(250, 14);
            this.LblLedgerBalance.Name = "LblLedgerBalance";
            this.LblLedgerBalance.Size = new System.Drawing.Size(139, 23);
            this.LblLedgerBalance.TabIndex = 249;
            this.LblLedgerBalance.Text = "0.00";
            this.LblLedgerBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(124, 16);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(126, 19);
            this.label14.TabIndex = 250;
            this.label14.Text = "Ledger Balance";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(389, 16);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(111, 19);
            this.label15.TabIndex = 251;
            this.label15.Text = "Term Amount";
            // 
            // LblNetBillAmount
            // 
            this.LblNetBillAmount.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LblNetBillAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblNetBillAmount.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNetBillAmount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblNetBillAmount.Location = new System.Drawing.Point(779, 14);
            this.LblNetBillAmount.Name = "LblNetBillAmount";
            this.LblNetBillAmount.Size = new System.Drawing.Size(139, 23);
            this.LblNetBillAmount.TabIndex = 252;
            this.LblNetBillAmount.Text = "0.00";
            this.LblNetBillAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.TabAttachment.Location = new System.Drawing.Point(4, 28);
            this.TabAttachment.Name = "TabAttachment";
            this.TabAttachment.Padding = new System.Windows.Forms.Padding(3);
            this.TabAttachment.Size = new System.Drawing.Size(929, 238);
            this.TabAttachment.TabIndex = 2;
            this.TabAttachment.Text = "Attachment";
            // 
            // LinkAttachment5
            // 
            this.LinkAttachment5.AutoSize = true;
            this.LinkAttachment5.Location = new System.Drawing.Point(726, 125);
            this.LinkAttachment5.Name = "LinkAttachment5";
            this.LinkAttachment5.Size = new System.Drawing.Size(67, 19);
            this.LinkAttachment5.TabIndex = 399;
            this.LinkAttachment5.TabStop = true;
            this.LinkAttachment5.Text = "Preview";
            // 
            // LinkAttachment4
            // 
            this.LinkAttachment4.AutoSize = true;
            this.LinkAttachment4.Location = new System.Drawing.Point(545, 126);
            this.LinkAttachment4.Name = "LinkAttachment4";
            this.LinkAttachment4.Size = new System.Drawing.Size(67, 19);
            this.LinkAttachment4.TabIndex = 398;
            this.LinkAttachment4.TabStop = true;
            this.LinkAttachment4.Text = "Preview";
            // 
            // LinkAttachment3
            // 
            this.LinkAttachment3.AutoSize = true;
            this.LinkAttachment3.Location = new System.Drawing.Point(371, 127);
            this.LinkAttachment3.Name = "LinkAttachment3";
            this.LinkAttachment3.Size = new System.Drawing.Size(67, 19);
            this.LinkAttachment3.TabIndex = 397;
            this.LinkAttachment3.TabStop = true;
            this.LinkAttachment3.Text = "Preview";
            // 
            // LinkAttachment2
            // 
            this.LinkAttachment2.AutoSize = true;
            this.LinkAttachment2.Location = new System.Drawing.Point(197, 128);
            this.LinkAttachment2.Name = "LinkAttachment2";
            this.LinkAttachment2.Size = new System.Drawing.Size(67, 19);
            this.LinkAttachment2.TabIndex = 396;
            this.LinkAttachment2.TabStop = true;
            this.LinkAttachment2.Text = "Preview";
            // 
            // LinkAttachment1
            // 
            this.LinkAttachment1.AutoSize = true;
            this.LinkAttachment1.Location = new System.Drawing.Point(24, 126);
            this.LinkAttachment1.Name = "LinkAttachment1";
            this.LinkAttachment1.Size = new System.Drawing.Size(67, 19);
            this.LinkAttachment1.TabIndex = 395;
            this.LinkAttachment1.TabStop = true;
            this.LinkAttachment1.Text = "Preview";
            // 
            // BtnAttachment5
            // 
            this.BtnAttachment5.Location = new System.Drawing.Point(749, 152);
            this.BtnAttachment5.Name = "BtnAttachment5";
            this.BtnAttachment5.Size = new System.Drawing.Size(116, 32);
            this.BtnAttachment5.TabIndex = 394;
            this.BtnAttachment5.Text = "Attachment ";
            this.BtnAttachment5.UseVisualStyleBackColor = true;
            // 
            // LblAttachment5
            // 
            this.LblAttachment5.AutoSize = true;
            this.LblAttachment5.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAttachment5.Location = new System.Drawing.Point(762, 30);
            this.LblAttachment5.Name = "LblAttachment5";
            this.LblAttachment5.Size = new System.Drawing.Size(90, 19);
            this.LblAttachment5.TabIndex = 393;
            this.LblAttachment5.Text = "Attachment";
            // 
            // PAttachment5
            // 
            this.PAttachment5.Location = new System.Drawing.Point(721, 52);
            this.PAttachment5.Name = "PAttachment5";
            this.PAttachment5.Size = new System.Drawing.Size(173, 96);
            this.PAttachment5.TabIndex = 392;
            this.PAttachment5.TabStop = false;
            // 
            // BtnAttachment4
            // 
            this.BtnAttachment4.Location = new System.Drawing.Point(568, 153);
            this.BtnAttachment4.Name = "BtnAttachment4";
            this.BtnAttachment4.Size = new System.Drawing.Size(116, 32);
            this.BtnAttachment4.TabIndex = 391;
            this.BtnAttachment4.Text = "Attachment ";
            this.BtnAttachment4.UseVisualStyleBackColor = true;
            // 
            // LblAttachment4
            // 
            this.LblAttachment4.AutoSize = true;
            this.LblAttachment4.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAttachment4.Location = new System.Drawing.Point(581, 30);
            this.LblAttachment4.Name = "LblAttachment4";
            this.LblAttachment4.Size = new System.Drawing.Size(90, 19);
            this.LblAttachment4.TabIndex = 390;
            this.LblAttachment4.Text = "Attachment";
            // 
            // PAttachment4
            // 
            this.PAttachment4.Location = new System.Drawing.Point(540, 52);
            this.PAttachment4.Name = "PAttachment4";
            this.PAttachment4.Size = new System.Drawing.Size(173, 96);
            this.PAttachment4.TabIndex = 389;
            this.PAttachment4.TabStop = false;
            // 
            // BtnAttachment3
            // 
            this.BtnAttachment3.Location = new System.Drawing.Point(395, 153);
            this.BtnAttachment3.Name = "BtnAttachment3";
            this.BtnAttachment3.Size = new System.Drawing.Size(116, 32);
            this.BtnAttachment3.TabIndex = 388;
            this.BtnAttachment3.Text = "Attachment ";
            this.BtnAttachment3.UseVisualStyleBackColor = true;
            // 
            // LblAttachment3
            // 
            this.LblAttachment3.AutoSize = true;
            this.LblAttachment3.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAttachment3.Location = new System.Drawing.Point(408, 31);
            this.LblAttachment3.Name = "LblAttachment3";
            this.LblAttachment3.Size = new System.Drawing.Size(90, 19);
            this.LblAttachment3.TabIndex = 387;
            this.LblAttachment3.Text = "Attachment";
            // 
            // PAttachment3
            // 
            this.PAttachment3.Location = new System.Drawing.Point(366, 53);
            this.PAttachment3.Name = "PAttachment3";
            this.PAttachment3.Size = new System.Drawing.Size(173, 96);
            this.PAttachment3.TabIndex = 386;
            this.PAttachment3.TabStop = false;
            // 
            // BtnAttachment2
            // 
            this.BtnAttachment2.Location = new System.Drawing.Point(222, 153);
            this.BtnAttachment2.Name = "BtnAttachment2";
            this.BtnAttachment2.Size = new System.Drawing.Size(116, 32);
            this.BtnAttachment2.TabIndex = 385;
            this.BtnAttachment2.Text = "Attachment ";
            this.BtnAttachment2.UseVisualStyleBackColor = true;
            // 
            // LblAttachment2
            // 
            this.LblAttachment2.AutoSize = true;
            this.LblAttachment2.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAttachment2.Location = new System.Drawing.Point(235, 31);
            this.LblAttachment2.Name = "LblAttachment2";
            this.LblAttachment2.Size = new System.Drawing.Size(90, 19);
            this.LblAttachment2.TabIndex = 384;
            this.LblAttachment2.Text = "Attachment";
            // 
            // PAttachment2
            // 
            this.PAttachment2.Location = new System.Drawing.Point(193, 53);
            this.PAttachment2.Name = "PAttachment2";
            this.PAttachment2.Size = new System.Drawing.Size(173, 96);
            this.PAttachment2.TabIndex = 383;
            this.PAttachment2.TabStop = false;
            // 
            // BtnAttachment1
            // 
            this.BtnAttachment1.Location = new System.Drawing.Point(49, 153);
            this.BtnAttachment1.Name = "BtnAttachment1";
            this.BtnAttachment1.Size = new System.Drawing.Size(124, 32);
            this.BtnAttachment1.TabIndex = 381;
            this.BtnAttachment1.Text = "Attachment ";
            this.BtnAttachment1.UseVisualStyleBackColor = true;
            // 
            // LblAttachment1
            // 
            this.LblAttachment1.AutoSize = true;
            this.LblAttachment1.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAttachment1.Location = new System.Drawing.Point(62, 31);
            this.LblAttachment1.Name = "LblAttachment1";
            this.LblAttachment1.Size = new System.Drawing.Size(90, 19);
            this.LblAttachment1.TabIndex = 382;
            this.LblAttachment1.Text = "Attachment";
            // 
            // PAttachment1
            // 
            this.PAttachment1.Location = new System.Drawing.Point(21, 53);
            this.PAttachment1.Name = "PAttachment1";
            this.PAttachment1.Size = new System.Drawing.Size(170, 96);
            this.PAttachment1.TabIndex = 380;
            this.PAttachment1.TabStop = false;
            // 
            // FrmPurchaseAdditional
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 469);
            this.Controls.Add(this.mrPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmPurchaseAdditional";
            this.ShowIcon = false;
            this.Tag = "Additional Invoice";
            this.Text = "PURCHASE ADDITIONAL TERMS";
            this.Load += new System.EventHandler(this.FrmPurchaseAdditional_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmPurchaseAdditional_KeyPress);
            this.mrPanel1.ResumeLayout(false);
            this.mrPanel1.PerformLayout();
            this.TabAdditionalTerm.ResumeLayout(false);
            this.TabProductWise.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProductGrid)).EndInit();
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            this.TabBillWise.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BillGrid)).EndInit();
            this.mrGroup2.ResumeLayout(false);
            this.mrGroup2.PerformLayout();
            this.TabAttachment.ResumeLayout(false);
            this.TabAttachment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PAttachment1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MrPanel mrPanel1;
        private System.Windows.Forms.TabControl TabAdditionalTerm;
        private System.Windows.Forms.TabPage TabProductWise;
        private System.Windows.Forms.TabPage TabBillWise;
        private ClsSeparator clsSeparator2;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnPrintInvoice;
        private DevExpress.XtraEditors.SimpleButton BtnReverse;
        private DevExpress.XtraEditors.SimpleButton BtnCopy;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private ClsSeparator clsSeparator1;
        private System.Windows.Forms.Label lbl_VoucherNo;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button BtnVno;
        private MrTextBox TxtVoucherNo;
        private MrMaskedTextBox MskMiti;
        private System.Windows.Forms.Label label11;
        private MrMaskedTextBox MskDate;
        private ClsSeparator clsSeparator3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Button BtnPurchaseInvoice;
        private MrTextBox TxtPurchaseInvoiceNo;
        private System.Windows.Forms.Label label4;
        private MrTextBox TxtInvoiceAmount;
        private MrMaskedTextBox MskInvoiceMiti;
        private System.Windows.Forms.Label label5;
        private MrMaskedTextBox MskInvoiceDate;
        private System.Windows.Forms.Label lbl_Class;
        private MrTextBox TxtDepartment;
        private System.Windows.Forms.Button BtnDepartment;
        private System.Windows.Forms.Label label6;
        private MrTextBox TxtCurrency;
        private System.Windows.Forms.Label label7;
        private MrTextBox TxtCurrencyRate;
        private System.Windows.Forms.Label label8;
        private MrTextBox TxtLocalInvoiceAmount;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private System.Windows.Forms.Label lbl_Remarks;
        private MrTextBox TxtRemarks;
        private System.Windows.Forms.Label lbl_NoInWords;
        private System.Windows.Forms.Label LblNumberInWords;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private MrGroup mrGroup1;
        private MrGroup mrGroup2;
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
        private MrTextBox LblTotalProductTerm;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label LblTotalBasicAmount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label LblTotalNetAmount;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label LblLedgerBalance;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label LblNetBillAmount;
        private System.Windows.Forms.Label LblBillTermAmount;
        private System.Windows.Forms.Label LblTotalQty;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtSNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtProductId;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtBasicQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtBasicAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtTermAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtNetAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtTermSno;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtTermId;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtTerm;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtLedgerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtCbLedgerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtLedger;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtSubledgerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtSubLedger;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtTermType;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtSign;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtInvoiceNetAmount;
        private EntryGridViewEx ProductGrid;
        private EntryGridViewEx BillGrid;
    }
}