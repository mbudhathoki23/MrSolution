namespace MrBLL.Domains.MoneyExchange
{
    partial class FrmForeignCurrencyPurchase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmForeignCurrencyPurchase));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TxtRefVno = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.MskRefDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.MskDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.MskMiti = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.BtnVno = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtVno = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_VoucherNo = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.BtnReverse = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCopy = new DevExpress.XtraEditors.SimpleButton();
            this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.mrPanel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.TabReceiptControl = new System.Windows.Forms.TabControl();
            this.TabReceipt = new System.Windows.Forms.TabPage();
            this.RGrid = new MrDAL.Control.ControlsEx.Control.EntryGridViewEx();
            this.mrPanel3 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.label26 = new System.Windows.Forms.Label();
            this.LblTotalAmount = new System.Windows.Forms.Label();
            this.LblBalanceType = new System.Windows.Forms.Label();
            this.lbl_Currentbal = new System.Windows.Forms.Label();
            this.LblBalance = new System.Windows.Forms.Label();
            this.BtnVendor = new System.Windows.Forms.Button();
            this.TxtLedger = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_CBLedger = new System.Windows.Forms.Label();
            this.clsSeparator3 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.btnRemarks = new System.Windows.Forms.Button();
            this.lbl_Remarks = new System.Windows.Forms.Label();
            this.TxtRemarks = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_NoInWords = new System.Windows.Forms.Label();
            this.lbl_NoInWordsDetl = new System.Windows.Forms.Label();
            this.GTxtSNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtCurrencyId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtCurrency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtExchangeRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtLocalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.LblLocalNetAmount = new System.Windows.Forms.Label();
            this.mrPanel1.SuspendLayout();
            this.TabReceiptControl.SuspendLayout();
            this.TabReceipt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).BeginInit();
            this.mrPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxtRefVno
            // 
            this.TxtRefVno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtRefVno.Font = new System.Drawing.Font("Book Antiqua", 11F);
            this.TxtRefVno.Location = new System.Drawing.Point(674, 41);
            this.TxtRefVno.Name = "TxtRefVno";
            this.TxtRefVno.Size = new System.Drawing.Size(117, 26);
            this.TxtRefVno.TabIndex = 11;
            // 
            // MskRefDate
            // 
            this.MskRefDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskRefDate.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.MskRefDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskRefDate.Location = new System.Drawing.Point(877, 42);
            this.MskRefDate.Mask = "00/00/0000";
            this.MskRefDate.Name = "MskRefDate";
            this.MskRefDate.Size = new System.Drawing.Size(112, 25);
            this.MskRefDate.TabIndex = 12;
            this.MskRefDate.ValidatingType = typeof(System.DateTime);
            // 
            // MskDate
            // 
            this.MskDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskDate.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.MskDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskDate.Location = new System.Drawing.Point(468, 42);
            this.MskDate.Mask = "00/00/0000";
            this.MskDate.Name = "MskDate";
            this.MskDate.Size = new System.Drawing.Size(117, 25);
            this.MskDate.TabIndex = 10;
            this.MskDate.ValidatingType = typeof(System.DateTime);
            // 
            // MskMiti
            // 
            this.MskMiti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskMiti.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.MskMiti.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskMiti.Location = new System.Drawing.Point(306, 42);
            this.MskMiti.Mask = "00/00/0000";
            this.MskMiti.Name = "MskMiti";
            this.MskMiti.Size = new System.Drawing.Size(117, 25);
            this.MskMiti.TabIndex = 9;
            this.MskMiti.ValidatingType = typeof(System.DateTime);
            // 
            // BtnVno
            // 
            this.BtnVno.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnVno.Image = ((System.Drawing.Image)(resources.GetObject("BtnVno.Image")));
            this.BtnVno.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BtnVno.Location = new System.Drawing.Point(240, 41);
            this.BtnVno.Name = "BtnVno";
            this.BtnVno.Size = new System.Drawing.Size(28, 26);
            this.BtnVno.TabIndex = 330;
            this.BtnVno.UseVisualStyleBackColor = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(796, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 19);
            this.label10.TabIndex = 328;
            this.label10.Text = "Ref Date";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(423, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 19);
            this.label1.TabIndex = 329;
            this.label1.Text = "Date";
            // 
            // TxtVno
            // 
            this.TxtVno.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtVno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtVno.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtVno.Location = new System.Drawing.Point(109, 42);
            this.TxtVno.MaxLength = 255;
            this.TxtVno.Name = "TxtVno";
            this.TxtVno.Size = new System.Drawing.Size(129, 25);
            this.TxtVno.TabIndex = 8;
            // 
            // lbl_VoucherNo
            // 
            this.lbl_VoucherNo.AutoSize = true;
            this.lbl_VoucherNo.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_VoucherNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_VoucherNo.Location = new System.Drawing.Point(8, 45);
            this.lbl_VoucherNo.Name = "lbl_VoucherNo";
            this.lbl_VoucherNo.Size = new System.Drawing.Size(96, 19);
            this.lbl_VoucherNo.TabIndex = 327;
            this.lbl_VoucherNo.Text = "Voucher No";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(611, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 19);
            this.label6.TabIndex = 331;
            this.label6.Text = "Ref No";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(268, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 19);
            this.label2.TabIndex = 332;
            this.label2.Text = "Miti";
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(4, 37);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(995, 2);
            this.clsSeparator1.TabIndex = 320;
            this.clsSeparator1.TabStop = false;
            // 
            // BtnReverse
            // 
            this.BtnReverse.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnReverse.Appearance.Options.UseFont = true;
            this.BtnReverse.ImageOptions.Image = global::MrBLL.Properties.Resources.Reverse;
            this.BtnReverse.Location = new System.Drawing.Point(641, 5);
            this.BtnReverse.Name = "BtnReverse";
            this.BtnReverse.Size = new System.Drawing.Size(115, 29);
            this.BtnReverse.TabIndex = 4;
            this.BtnReverse.Text = "&REVERSE";
            this.BtnReverse.Click += new System.EventHandler(this.BtnReverse_Click);
            // 
            // BtnCopy
            // 
            this.BtnCopy.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnCopy.Appearance.Options.UseFont = true;
            this.BtnCopy.ImageOptions.Image = global::MrBLL.Properties.Resources.Copy;
            this.BtnCopy.Location = new System.Drawing.Point(757, 5);
            this.BtnCopy.Name = "BtnCopy";
            this.BtnCopy.Size = new System.Drawing.Size(87, 29);
            this.BtnCopy.TabIndex = 5;
            this.BtnCopy.Text = "&COPY";
            this.BtnCopy.Click += new System.EventHandler(this.BtnCopy_Click);
            // 
            // BtnPrint
            // 
            this.BtnPrint.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnPrint.Appearance.Options.UseFont = true;
            this.BtnPrint.ImageOptions.Image = global::MrBLL.Properties.Resources.Printerview;
            this.BtnPrint.Location = new System.Drawing.Point(845, 5);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(78, 29);
            this.BtnPrint.TabIndex = 6;
            this.BtnPrint.Text = "&PRINT";
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.BtnExit.Location = new System.Drawing.Point(924, 5);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(73, 29);
            this.BtnExit.TabIndex = 7;
            this.BtnExit.Text = "E&XIT";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(164, 5);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(104, 31);
            this.BtnDelete.TabIndex = 3;
            this.BtnDelete.Tag = "DeleteButtonCheck";
            this.BtnDelete.Text = "&DELETE";
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnNew
            // 
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnNew.Location = new System.Drawing.Point(6, 5);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(77, 31);
            this.BtnNew.TabIndex = 1;
            this.BtnNew.Tag = "NewButtonCheck";
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // mrPanel1
            // 
            this.mrPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel1.Controls.Add(this.lbl_NoInWords);
            this.mrPanel1.Controls.Add(this.lbl_NoInWordsDetl);
            this.mrPanel1.Controls.Add(this.TabReceiptControl);
            this.mrPanel1.Controls.Add(this.LblBalanceType);
            this.mrPanel1.Controls.Add(this.lbl_Currentbal);
            this.mrPanel1.Controls.Add(this.LblBalance);
            this.mrPanel1.Controls.Add(this.BtnVendor);
            this.mrPanel1.Controls.Add(this.TxtLedger);
            this.mrPanel1.Controls.Add(this.lbl_CBLedger);
            this.mrPanel1.Controls.Add(this.clsSeparator3);
            this.mrPanel1.Controls.Add(this.btnRemarks);
            this.mrPanel1.Controls.Add(this.lbl_Remarks);
            this.mrPanel1.Controls.Add(this.TxtRemarks);
            this.mrPanel1.Controls.Add(this.BtnCancel);
            this.mrPanel1.Controls.Add(this.BtnSave);
            this.mrPanel1.Controls.Add(this.clsSeparator2);
            this.mrPanel1.Controls.Add(this.TxtRefVno);
            this.mrPanel1.Controls.Add(this.MskRefDate);
            this.mrPanel1.Controls.Add(this.MskDate);
            this.mrPanel1.Controls.Add(this.MskMiti);
            this.mrPanel1.Controls.Add(this.BtnVno);
            this.mrPanel1.Controls.Add(this.label10);
            this.mrPanel1.Controls.Add(this.label1);
            this.mrPanel1.Controls.Add(this.TxtVno);
            this.mrPanel1.Controls.Add(this.lbl_VoucherNo);
            this.mrPanel1.Controls.Add(this.label6);
            this.mrPanel1.Controls.Add(this.label2);
            this.mrPanel1.Controls.Add(this.clsSeparator1);
            this.mrPanel1.Controls.Add(this.BtnReverse);
            this.mrPanel1.Controls.Add(this.BtnCopy);
            this.mrPanel1.Controls.Add(this.BtnPrint);
            this.mrPanel1.Controls.Add(this.BtnExit);
            this.mrPanel1.Controls.Add(this.BtnDelete);
            this.mrPanel1.Controls.Add(this.BtnEdit);
            this.mrPanel1.Controls.Add(this.BtnNew);
            this.mrPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrPanel1.Location = new System.Drawing.Point(0, 0);
            this.mrPanel1.Name = "mrPanel1";
            this.mrPanel1.Size = new System.Drawing.Size(1002, 490);
            this.mrPanel1.TabIndex = 1;
            // 
            // TabReceiptControl
            // 
            this.TabReceiptControl.Controls.Add(this.TabReceipt);
            this.TabReceiptControl.HotTrack = true;
            this.TabReceiptControl.Location = new System.Drawing.Point(4, 111);
            this.TabReceiptControl.Name = "TabReceiptControl";
            this.TabReceiptControl.SelectedIndex = 0;
            this.TabReceiptControl.Size = new System.Drawing.Size(992, 304);
            this.TabReceiptControl.TabIndex = 364;
            // 
            // TabReceipt
            // 
            this.TabReceipt.Controls.Add(this.RGrid);
            this.TabReceipt.Controls.Add(this.mrPanel3);
            this.TabReceipt.Location = new System.Drawing.Point(4, 28);
            this.TabReceipt.Name = "TabReceipt";
            this.TabReceipt.Padding = new System.Windows.Forms.Padding(3);
            this.TabReceipt.Size = new System.Drawing.Size(984, 272);
            this.TabReceipt.TabIndex = 0;
            this.TabReceipt.Text = "CURRENCY DETAILS";
            this.TabReceipt.UseVisualStyleBackColor = true;
            // 
            // RGrid
            // 
            this.RGrid.AllowUserToAddRows = false;
            this.RGrid.AllowUserToDeleteRows = false;
            this.RGrid.AllowUserToResizeColumns = false;
            this.RGrid.AllowUserToResizeRows = false;
            this.RGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.RGrid.BlockNavigationOnNextRowOnEnter = true;
            this.RGrid.CausesValidation = false;
            this.RGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.RGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.RGrid.ColumnHeadersHeight = 29;
            this.RGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GTxtSNo,
            this.GTxtCurrencyId,
            this.GTxtCurrency,
            this.GTxtRate,
            this.GTxtExchangeRate,
            this.GTxtAmount,
            this.GTxtLocalAmount});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.RGrid.DefaultCellStyle = dataGridViewCellStyle1;
            this.RGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RGrid.DoubleBufferEnabled = true;
            this.RGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.RGrid.Location = new System.Drawing.Point(3, 3);
            this.RGrid.MultiSelect = false;
            this.RGrid.Name = "RGrid";
            this.RGrid.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Bookman Old Style", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.RGrid.RowHeadersVisible = false;
            this.RGrid.RowHeadersWidth = 51;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            this.RGrid.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.RGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RGrid.Size = new System.Drawing.Size(978, 235);
            this.RGrid.StandardTab = true;
            this.RGrid.TabIndex = 0;
            this.RGrid.TabStop = false;
            // 
            // mrPanel3
            // 
            this.mrPanel3.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel3.Controls.Add(this.label3);
            this.mrPanel3.Controls.Add(this.LblLocalNetAmount);
            this.mrPanel3.Controls.Add(this.label26);
            this.mrPanel3.Controls.Add(this.LblTotalAmount);
            this.mrPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mrPanel3.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrPanel3.Location = new System.Drawing.Point(3, 238);
            this.mrPanel3.Name = "mrPanel3";
            this.mrPanel3.Size = new System.Drawing.Size(978, 31);
            this.mrPanel3.TabIndex = 324;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label26.Location = new System.Drawing.Point(524, 5);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(67, 19);
            this.label26.TabIndex = 245;
            this.label26.Text = "Amount";
            // 
            // LblTotalAmount
            // 
            this.LblTotalAmount.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LblTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTotalAmount.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.LblTotalAmount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblTotalAmount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LblTotalAmount.Location = new System.Drawing.Point(592, 3);
            this.LblTotalAmount.Name = "LblTotalAmount";
            this.LblTotalAmount.Size = new System.Drawing.Size(138, 24);
            this.LblTotalAmount.TabIndex = 244;
            this.LblTotalAmount.Text = "0.00";
            this.LblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblBalanceType
            // 
            this.LblBalanceType.AutoSize = true;
            this.LblBalanceType.ForeColor = System.Drawing.Color.Black;
            this.LblBalanceType.Location = new System.Drawing.Point(962, 80);
            this.LblBalanceType.Name = "LblBalanceType";
            this.LblBalanceType.Size = new System.Drawing.Size(27, 19);
            this.LblBalanceType.TabIndex = 363;
            this.LblBalanceType.Text = "Cr";
            // 
            // lbl_Currentbal
            // 
            this.lbl_Currentbal.AutoSize = true;
            this.lbl_Currentbal.ForeColor = System.Drawing.Color.Black;
            this.lbl_Currentbal.Location = new System.Drawing.Point(685, 81);
            this.lbl_Currentbal.Name = "lbl_Currentbal";
            this.lbl_Currentbal.Size = new System.Drawing.Size(70, 19);
            this.lbl_Currentbal.TabIndex = 362;
            this.lbl_Currentbal.Text = "Balance";
            // 
            // LblBalance
            // 
            this.LblBalance.BackColor = System.Drawing.Color.LightBlue;
            this.LblBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblBalance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblBalance.ForeColor = System.Drawing.Color.Crimson;
            this.LblBalance.Location = new System.Drawing.Point(761, 78);
            this.LblBalance.Name = "LblBalance";
            this.LblBalance.Size = new System.Drawing.Size(197, 24);
            this.LblBalance.TabIndex = 361;
            this.LblBalance.Text = "0.00";
            this.LblBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BtnVendor
            // 
            this.BtnVendor.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.BtnVendor.CausesValidation = false;
            this.BtnVendor.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.BtnVendor.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnVendor.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnVendor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BtnVendor.Location = new System.Drawing.Point(650, 76);
            this.BtnVendor.Margin = new System.Windows.Forms.Padding(4);
            this.BtnVendor.Name = "BtnVendor";
            this.BtnVendor.Size = new System.Drawing.Size(29, 24);
            this.BtnVendor.TabIndex = 360;
            this.BtnVendor.TabStop = false;
            this.BtnVendor.UseVisualStyleBackColor = false;
            this.BtnVendor.Click += new System.EventHandler(this.BtnVendor_Click);
            // 
            // TxtLedger
            // 
            this.TxtLedger.BackColor = System.Drawing.Color.White;
            this.TxtLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtLedger.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.TxtLedger.Location = new System.Drawing.Point(109, 77);
            this.TxtLedger.Margin = new System.Windows.Forms.Padding(4);
            this.TxtLedger.MaxLength = 255;
            this.TxtLedger.Name = "TxtLedger";
            this.TxtLedger.ReadOnly = true;
            this.TxtLedger.Size = new System.Drawing.Size(541, 23);
            this.TxtLedger.TabIndex = 13;
            // 
            // lbl_CBLedger
            // 
            this.lbl_CBLedger.AutoSize = true;
            this.lbl_CBLedger.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.lbl_CBLedger.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_CBLedger.Location = new System.Drawing.Point(12, 79);
            this.lbl_CBLedger.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_CBLedger.Name = "lbl_CBLedger";
            this.lbl_CBLedger.Size = new System.Drawing.Size(75, 19);
            this.lbl_CBLedger.TabIndex = 359;
            this.lbl_CBLedger.Text = "Suppliers";
            // 
            // clsSeparator3
            // 
            this.clsSeparator3.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator3.Location = new System.Drawing.Point(3, 105);
            this.clsSeparator3.Name = "clsSeparator3";
            this.clsSeparator3.Size = new System.Drawing.Size(996, 2);
            this.clsSeparator3.TabIndex = 322;
            this.clsSeparator3.TabStop = false;
            // 
            // btnRemarks
            // 
            this.btnRemarks.Font = new System.Drawing.Font("Arial", 12F);
            this.btnRemarks.Image = ((System.Drawing.Image)(resources.GetObject("btnRemarks.Image")));
            this.btnRemarks.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemarks.Location = new System.Drawing.Point(967, 420);
            this.btnRemarks.Name = "btnRemarks";
            this.btnRemarks.Size = new System.Drawing.Size(28, 26);
            this.btnRemarks.TabIndex = 345;
            this.btnRemarks.UseVisualStyleBackColor = false;
            // 
            // lbl_Remarks
            // 
            this.lbl_Remarks.AutoSize = true;
            this.lbl_Remarks.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_Remarks.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_Remarks.Location = new System.Drawing.Point(22, 424);
            this.lbl_Remarks.Name = "lbl_Remarks";
            this.lbl_Remarks.Size = new System.Drawing.Size(76, 19);
            this.lbl_Remarks.TabIndex = 344;
            this.lbl_Remarks.Text = "Remarks";
            // 
            // TxtRemarks
            // 
            this.TxtRemarks.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtRemarks.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtRemarks.Location = new System.Drawing.Point(109, 421);
            this.TxtRemarks.MaxLength = 255;
            this.TxtRemarks.Name = "TxtRemarks";
            this.TxtRemarks.Size = new System.Drawing.Size(857, 25);
            this.TxtRemarks.TabIndex = 14;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.BtnCancel.Location = new System.Drawing.Point(897, 448);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(103, 38);
            this.BtnCancel.TabIndex = 16;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(812, 448);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(82, 38);
            this.BtnSave.TabIndex = 15;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(4, 71);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(997, 2);
            this.clsSeparator2.TabIndex = 321;
            this.clsSeparator2.TabStop = false;
            // 
            // BtnEdit
            // 
            this.BtnEdit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnEdit.Appearance.Options.UseFont = true;
            this.BtnEdit.ImageOptions.Image = global::MrBLL.Properties.Resources.Edit;
            this.BtnEdit.Location = new System.Drawing.Point(84, 5);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(79, 31);
            this.BtnEdit.TabIndex = 2;
            this.BtnEdit.Tag = "EditButtonCheck";
            this.BtnEdit.Text = "&EDIT";
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // lbl_NoInWords
            // 
            this.lbl_NoInWords.AutoSize = true;
            this.lbl_NoInWords.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_NoInWords.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_NoInWords.Location = new System.Drawing.Point(22, 453);
            this.lbl_NoInWords.Name = "lbl_NoInWords";
            this.lbl_NoInWords.Size = new System.Drawing.Size(75, 19);
            this.lbl_NoInWords.TabIndex = 365;
            this.lbl_NoInWords.Text = "In Words";
            // 
            // lbl_NoInWordsDetl
            // 
            this.lbl_NoInWordsDetl.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lbl_NoInWordsDetl.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_NoInWordsDetl.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lbl_NoInWordsDetl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_NoInWordsDetl.Location = new System.Drawing.Point(105, 449);
            this.lbl_NoInWordsDetl.Name = "lbl_NoInWordsDetl";
            this.lbl_NoInWordsDetl.Size = new System.Drawing.Size(701, 34);
            this.lbl_NoInWordsDetl.TabIndex = 366;
            this.lbl_NoInWordsDetl.Text = "Only.";
            this.lbl_NoInWordsDetl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GTxtSNo
            // 
            this.GTxtSNo.HeaderText = "SNo";
            this.GTxtSNo.Name = "GTxtSNo";
            this.GTxtSNo.ReadOnly = true;
            this.GTxtSNo.Width = 65;
            // 
            // GTxtCurrencyId
            // 
            this.GTxtCurrencyId.HeaderText = "CurrencyId";
            this.GTxtCurrencyId.Name = "GTxtCurrencyId";
            this.GTxtCurrencyId.ReadOnly = true;
            this.GTxtCurrencyId.Visible = false;
            // 
            // GTxtCurrency
            // 
            this.GTxtCurrency.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.GTxtCurrency.HeaderText = "Currency";
            this.GTxtCurrency.Name = "GTxtCurrency";
            this.GTxtCurrency.ReadOnly = true;
            // 
            // GTxtRate
            // 
            this.GTxtRate.HeaderText = "Rate";
            this.GTxtRate.Name = "GTxtRate";
            this.GTxtRate.ReadOnly = true;
            // 
            // GTxtExchangeRate
            // 
            this.GTxtExchangeRate.HeaderText = "LocalRate";
            this.GTxtExchangeRate.Name = "GTxtExchangeRate";
            this.GTxtExchangeRate.ReadOnly = true;
            // 
            // GTxtAmount
            // 
            this.GTxtAmount.HeaderText = "Amount";
            this.GTxtAmount.Name = "GTxtAmount";
            this.GTxtAmount.ReadOnly = true;
            // 
            // GTxtLocalAmount
            // 
            this.GTxtLocalAmount.HeaderText = "LocalAmount";
            this.GTxtLocalAmount.Name = "GTxtLocalAmount";
            this.GTxtLocalAmount.ReadOnly = true;
            this.GTxtLocalAmount.Width = 150;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(740, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 19);
            this.label3.TabIndex = 247;
            this.label3.Text = "L Amount";
            // 
            // LblLocalNetAmount
            // 
            this.LblLocalNetAmount.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LblLocalNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblLocalNetAmount.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.LblLocalNetAmount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblLocalNetAmount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LblLocalNetAmount.Location = new System.Drawing.Point(834, 3);
            this.LblLocalNetAmount.Name = "LblLocalNetAmount";
            this.LblLocalNetAmount.Size = new System.Drawing.Size(138, 24);
            this.LblLocalNetAmount.TabIndex = 246;
            this.LblLocalNetAmount.Text = "0.00";
            this.LblLocalNetAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmForeignCurrencyPurchase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 490);
            this.Controls.Add(this.mrPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmForeignCurrencyPurchase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Foreign Currency Purchase";
            this.Load += new System.EventHandler(this.FrmForeignCurrencyPurchase_Load);
            this.Shown += new System.EventHandler(this.FrmForeignCurrencyPurchase_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmForeignCurrencyPurchase_KeyPress);
            this.mrPanel1.ResumeLayout(false);
            this.mrPanel1.PerformLayout();
            this.TabReceiptControl.ResumeLayout(false);
            this.TabReceipt.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).EndInit();
            this.mrPanel3.ResumeLayout(false);
            this.mrPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private MrDAL.Control.ControlsEx.Control.MrTextBox TxtRefVno;
        private MrDAL.Control.ControlsEx.Control.MrMaskedTextBox MskRefDate;
        private MrDAL.Control.ControlsEx.Control.MrMaskedTextBox MskDate;
        private MrDAL.Control.ControlsEx.Control.MrMaskedTextBox MskMiti;
        private System.Windows.Forms.Button BtnVno;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private MrDAL.Control.ControlsEx.Control.MrTextBox TxtVno;
        private System.Windows.Forms.Label lbl_VoucherNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private MrDAL.Control.ControlsEx.Control.ClsSeparator clsSeparator1;
        private DevExpress.XtraEditors.SimpleButton BtnReverse;
        private DevExpress.XtraEditors.SimpleButton BtnCopy;
        private DevExpress.XtraEditors.SimpleButton BtnPrint;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private MrDAL.Control.ControlsEx.Control.MrPanel mrPanel1;
        private System.Windows.Forms.Button BtnVendor;
        private MrDAL.Control.ControlsEx.Control.MrTextBox TxtLedger;
        private System.Windows.Forms.Label lbl_CBLedger;
        private MrDAL.Control.ControlsEx.Control.ClsSeparator clsSeparator3;
        private System.Windows.Forms.Button btnRemarks;
        private System.Windows.Forms.Label lbl_Remarks;
        private MrDAL.Control.ControlsEx.Control.MrTextBox TxtRemarks;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private MrDAL.Control.ControlsEx.Control.ClsSeparator clsSeparator2;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        public System.Windows.Forms.Label LblBalanceType;
        public System.Windows.Forms.Label lbl_Currentbal;
        public System.Windows.Forms.Label LblBalance;
        private System.Windows.Forms.TabControl TabReceiptControl;
        private System.Windows.Forms.TabPage TabReceipt;
        private MrDAL.Control.ControlsEx.Control.EntryGridViewEx RGrid;
        private MrDAL.Control.ControlsEx.Control.MrPanel mrPanel3;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label LblTotalAmount;
        private System.Windows.Forms.Label lbl_NoInWords;
        private System.Windows.Forms.Label lbl_NoInWordsDetl;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtSNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtCurrencyId;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtCurrency;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtExchangeRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtLocalAmount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LblLocalNetAmount;
    }
}