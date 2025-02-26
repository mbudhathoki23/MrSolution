namespace MrBLL.Domains.MoneyExchange
{
    partial class FrmForeignCurrencySales
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label26 = new System.Windows.Forms.Label();
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
            this.LblTotalAmount = new System.Windows.Forms.Label();
            this.mrPanel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.BtnVendor = new System.Windows.Forms.Button();
            this.TxtVendorLedger = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_CBLedger = new System.Windows.Forms.Label();
            this.clsSeparator3 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.btnRemarks = new System.Windows.Forms.Button();
            this.lbl_Remarks = new System.Windows.Forms.Label();
            this.TxtRemarks = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.TabReceiptControl = new System.Windows.Forms.TabControl();
            this.TabReceipt = new System.Windows.Forms.TabPage();
            this.RGrid = new MrDAL.Control.ControlsEx.Control.EntryGridViewEx();
            this.mrPanel3 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.LblBalanceType = new System.Windows.Forms.Label();
            this.lbl_Currentbal = new System.Windows.Forms.Label();
            this.LblBalance = new System.Windows.Forms.Label();
            this.mrPanel1.SuspendLayout();
            this.TabReceiptControl.SuspendLayout();
            this.TabReceipt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).BeginInit();
            this.mrPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label26.Location = new System.Drawing.Point(452, 6);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(67, 19);
            this.label26.TabIndex = 245;
            this.label26.Text = "Amount";
            // 
            // TxtRefVno
            // 
            this.TxtRefVno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtRefVno.Font = new System.Drawing.Font("Book Antiqua", 11F);
            this.TxtRefVno.Location = new System.Drawing.Point(674, 48);
            this.TxtRefVno.Name = "TxtRefVno";
            this.TxtRefVno.Size = new System.Drawing.Size(117, 26);
            this.TxtRefVno.TabIndex = 11;
            // 
            // MskRefDate
            // 
            this.MskRefDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskRefDate.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.MskRefDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskRefDate.Location = new System.Drawing.Point(877, 49);
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
            this.MskDate.Location = new System.Drawing.Point(468, 49);
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
            this.MskMiti.Location = new System.Drawing.Point(306, 49);
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
            this.BtnVno.Location = new System.Drawing.Point(239, 50);
            this.BtnVno.Name = "BtnVno";
            this.BtnVno.Size = new System.Drawing.Size(26, 22);
            this.BtnVno.TabIndex = 330;
            this.BtnVno.UseVisualStyleBackColor = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(796, 52);
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
            this.label1.Location = new System.Drawing.Point(423, 52);
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
            this.TxtVno.Location = new System.Drawing.Point(109, 49);
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
            this.lbl_VoucherNo.Location = new System.Drawing.Point(8, 52);
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
            this.label6.Location = new System.Drawing.Point(611, 52);
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
            this.label2.Location = new System.Drawing.Point(268, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 19);
            this.label2.TabIndex = 332;
            this.label2.Text = "Miti";
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(12, 41);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(987, 2);
            this.clsSeparator1.TabIndex = 320;
            this.clsSeparator1.TabStop = false;
            // 
            // BtnReverse
            // 
            this.BtnReverse.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnReverse.Appearance.Options.UseFont = true;
            this.BtnReverse.ImageOptions.Image = global::MrBLL.Properties.Resources.Reverse;
            this.BtnReverse.Location = new System.Drawing.Point(611, 5);
            this.BtnReverse.Name = "BtnReverse";
            this.BtnReverse.Size = new System.Drawing.Size(116, 36);
            this.BtnReverse.TabIndex = 4;
            this.BtnReverse.Text = "&REVERSE";
            // 
            // BtnCopy
            // 
            this.BtnCopy.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnCopy.Appearance.Options.UseFont = true;
            this.BtnCopy.ImageOptions.Image = global::MrBLL.Properties.Resources.Copy;
            this.BtnCopy.Location = new System.Drawing.Point(728, 5);
            this.BtnCopy.Name = "BtnCopy";
            this.BtnCopy.Size = new System.Drawing.Size(92, 36);
            this.BtnCopy.TabIndex = 5;
            this.BtnCopy.Text = "&COPY";
            // 
            // BtnPrint
            // 
            this.BtnPrint.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnPrint.Appearance.Options.UseFont = true;
            this.BtnPrint.ImageOptions.Image = global::MrBLL.Properties.Resources.Printerview;
            this.BtnPrint.Location = new System.Drawing.Point(821, 5);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(92, 36);
            this.BtnPrint.TabIndex = 6;
            this.BtnPrint.Text = "&PRINT";
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.BtnExit.Location = new System.Drawing.Point(914, 5);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(82, 36);
            this.BtnExit.TabIndex = 7;
            this.BtnExit.Text = "E&XIT";
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(177, 5);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(108, 36);
            this.BtnDelete.TabIndex = 3;
            this.BtnDelete.Tag = "DeleteButtonCheck";
            this.BtnDelete.Text = "&DELETE";
            // 
            // BtnNew
            // 
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnNew.Location = new System.Drawing.Point(11, 5);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(82, 36);
            this.BtnNew.TabIndex = 1;
            this.BtnNew.Tag = "NewButtonCheck";
            this.BtnNew.Text = "&NEW";
            // 
            // LblTotalAmount
            // 
            this.LblTotalAmount.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LblTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTotalAmount.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.LblTotalAmount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblTotalAmount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LblTotalAmount.Location = new System.Drawing.Point(517, 3);
            this.LblTotalAmount.Name = "LblTotalAmount";
            this.LblTotalAmount.Size = new System.Drawing.Size(138, 24);
            this.LblTotalAmount.TabIndex = 244;
            this.LblTotalAmount.Text = "0.00";
            this.LblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mrPanel1
            // 
            this.mrPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel1.Controls.Add(this.LblBalanceType);
            this.mrPanel1.Controls.Add(this.lbl_Currentbal);
            this.mrPanel1.Controls.Add(this.LblBalance);
            this.mrPanel1.Controls.Add(this.BtnVendor);
            this.mrPanel1.Controls.Add(this.TxtVendorLedger);
            this.mrPanel1.Controls.Add(this.lbl_CBLedger);
            this.mrPanel1.Controls.Add(this.clsSeparator3);
            this.mrPanel1.Controls.Add(this.btnRemarks);
            this.mrPanel1.Controls.Add(this.lbl_Remarks);
            this.mrPanel1.Controls.Add(this.TxtRemarks);
            this.mrPanel1.Controls.Add(this.BtnCancel);
            this.mrPanel1.Controls.Add(this.BtnSave);
            this.mrPanel1.Controls.Add(this.clsSeparator2);
            this.mrPanel1.Controls.Add(this.TabReceiptControl);
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
            this.mrPanel1.Size = new System.Drawing.Size(1002, 538);
            this.mrPanel1.TabIndex = 1;
            // 
            // BtnVendor
            // 
            this.BtnVendor.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.BtnVendor.CausesValidation = false;
            this.BtnVendor.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.BtnVendor.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnVendor.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnVendor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BtnVendor.Location = new System.Drawing.Point(651, 83);
            this.BtnVendor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnVendor.Name = "BtnVendor";
            this.BtnVendor.Size = new System.Drawing.Size(29, 27);
            this.BtnVendor.TabIndex = 360;
            this.BtnVendor.TabStop = false;
            this.BtnVendor.UseVisualStyleBackColor = false;
            // 
            // TxtVendorLedger
            // 
            this.TxtVendorLedger.BackColor = System.Drawing.Color.White;
            this.TxtVendorLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtVendorLedger.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.TxtVendorLedger.Location = new System.Drawing.Point(109, 84);
            this.TxtVendorLedger.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtVendorLedger.MaxLength = 255;
            this.TxtVendorLedger.Name = "TxtVendorLedger";
            this.TxtVendorLedger.ReadOnly = true;
            this.TxtVendorLedger.Size = new System.Drawing.Size(541, 23);
            this.TxtVendorLedger.TabIndex = 13;
            // 
            // lbl_CBLedger
            // 
            this.lbl_CBLedger.AutoSize = true;
            this.lbl_CBLedger.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.lbl_CBLedger.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_CBLedger.Location = new System.Drawing.Point(12, 86);
            this.lbl_CBLedger.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_CBLedger.Name = "lbl_CBLedger";
            this.lbl_CBLedger.Size = new System.Drawing.Size(75, 19);
            this.lbl_CBLedger.TabIndex = 359;
            this.lbl_CBLedger.Text = "Suppliers";
            // 
            // clsSeparator3
            // 
            this.clsSeparator3.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator3.Location = new System.Drawing.Point(14, 112);
            this.clsSeparator3.Name = "clsSeparator3";
            this.clsSeparator3.Size = new System.Drawing.Size(985, 2);
            this.clsSeparator3.TabIndex = 322;
            this.clsSeparator3.TabStop = false;
            // 
            // btnRemarks
            // 
            this.btnRemarks.Font = new System.Drawing.Font("Arial", 12F);
            this.btnRemarks.Image = ((System.Drawing.Image)(resources.GetObject("btnRemarks.Image")));
            this.btnRemarks.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemarks.Location = new System.Drawing.Point(969, 467);
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
            this.lbl_Remarks.Location = new System.Drawing.Point(19, 471);
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
            this.TxtRemarks.Location = new System.Drawing.Point(106, 468);
            this.TxtRemarks.MaxLength = 255;
            this.TxtRemarks.Name = "TxtRemarks";
            this.TxtRemarks.Size = new System.Drawing.Size(861, 25);
            this.TxtRemarks.TabIndex = 14;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.BtnCancel.Location = new System.Drawing.Point(894, 495);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(103, 38);
            this.BtnCancel.TabIndex = 16;
            this.BtnCancel.Text = "&CANCEL";
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(809, 495);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(82, 38);
            this.BtnSave.TabIndex = 15;
            this.BtnSave.Text = "&SAVE";
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(11, 79);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(985, 2);
            this.clsSeparator2.TabIndex = 321;
            this.clsSeparator2.TabStop = false;
            // 
            // TabReceiptControl
            // 
            this.TabReceiptControl.Controls.Add(this.TabReceipt);
            this.TabReceiptControl.HotTrack = true;
            this.TabReceiptControl.Location = new System.Drawing.Point(8, 116);
            this.TabReceiptControl.Name = "TabReceiptControl";
            this.TabReceiptControl.SelectedIndex = 0;
            this.TabReceiptControl.Size = new System.Drawing.Size(988, 345);
            this.TabReceiptControl.TabIndex = 12;
            // 
            // TabReceipt
            // 
            this.TabReceipt.Controls.Add(this.RGrid);
            this.TabReceipt.Controls.Add(this.mrPanel3);
            this.TabReceipt.Location = new System.Drawing.Point(4, 28);
            this.TabReceipt.Name = "TabReceipt";
            this.TabReceipt.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.TabReceipt.Size = new System.Drawing.Size(980, 313);
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
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.RGrid.DefaultCellStyle = dataGridViewCellStyle10;
            this.RGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RGrid.DoubleBufferEnabled = true;
            this.RGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.RGrid.Location = new System.Drawing.Point(3, 3);
            this.RGrid.MultiSelect = false;
            this.RGrid.Name = "RGrid";
            this.RGrid.ReadOnly = true;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Bookman Old Style", 9F);
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.RGrid.RowHeadersVisible = false;
            this.RGrid.RowHeadersWidth = 51;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            this.RGrid.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.RGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RGrid.Size = new System.Drawing.Size(974, 276);
            this.RGrid.StandardTab = true;
            this.RGrid.TabIndex = 0;
            this.RGrid.TabStop = false;
            // 
            // mrPanel3
            // 
            this.mrPanel3.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel3.Controls.Add(this.label26);
            this.mrPanel3.Controls.Add(this.LblTotalAmount);
            this.mrPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mrPanel3.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrPanel3.Location = new System.Drawing.Point(3, 279);
            this.mrPanel3.Name = "mrPanel3";
            this.mrPanel3.Size = new System.Drawing.Size(974, 31);
            this.mrPanel3.TabIndex = 324;
            // 
            // BtnEdit
            // 
            this.BtnEdit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnEdit.Appearance.Options.UseFont = true;
            this.BtnEdit.ImageOptions.Image = global::MrBLL.Properties.Resources.Edit;
            this.BtnEdit.Location = new System.Drawing.Point(94, 5);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(82, 36);
            this.BtnEdit.TabIndex = 2;
            this.BtnEdit.Tag = "EditButtonCheck";
            this.BtnEdit.Text = "&EDIT";
            // 
            // LblBalanceType
            // 
            this.LblBalanceType.AutoSize = true;
            this.LblBalanceType.ForeColor = System.Drawing.Color.Black;
            this.LblBalanceType.Location = new System.Drawing.Point(962, 87);
            this.LblBalanceType.Name = "LblBalanceType";
            this.LblBalanceType.Size = new System.Drawing.Size(27, 19);
            this.LblBalanceType.TabIndex = 363;
            this.LblBalanceType.Text = "Cr";
            // 
            // lbl_Currentbal
            // 
            this.lbl_Currentbal.AutoSize = true;
            this.lbl_Currentbal.ForeColor = System.Drawing.Color.Black;
            this.lbl_Currentbal.Location = new System.Drawing.Point(685, 88);
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
            this.LblBalance.Location = new System.Drawing.Point(761, 85);
            this.LblBalance.Name = "LblBalance";
            this.LblBalance.Size = new System.Drawing.Size(197, 24);
            this.LblBalance.TabIndex = 361;
            this.LblBalance.Text = "0.00";
            this.LblBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmForeignCurrencyPurchase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 538);
            this.Controls.Add(this.mrPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Name = "FrmForeignCurrencyPurchase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Foreign Currency Purchase";
            this.Load += new System.EventHandler(this.FrmForeignCurrencySales_Load);
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

        private System.Windows.Forms.Label label26;
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
        private System.Windows.Forms.Label LblTotalAmount;
        private MrDAL.Control.ControlsEx.Control.MrPanel mrPanel1;
        private System.Windows.Forms.Button BtnVendor;
        private MrDAL.Control.ControlsEx.Control.MrTextBox TxtVendorLedger;
        private System.Windows.Forms.Label lbl_CBLedger;
        private MrDAL.Control.ControlsEx.Control.ClsSeparator clsSeparator3;
        private System.Windows.Forms.Button btnRemarks;
        private System.Windows.Forms.Label lbl_Remarks;
        private MrDAL.Control.ControlsEx.Control.MrTextBox TxtRemarks;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private MrDAL.Control.ControlsEx.Control.ClsSeparator clsSeparator2;
        private System.Windows.Forms.TabControl TabReceiptControl;
        private System.Windows.Forms.TabPage TabReceipt;
        private MrDAL.Control.ControlsEx.Control.EntryGridViewEx RGrid;
        private MrDAL.Control.ControlsEx.Control.MrPanel mrPanel3;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        public System.Windows.Forms.Label LblBalanceType;
        public System.Windows.Forms.Label lbl_Currentbal;
        public System.Windows.Forms.Label LblBalance;
    }
}