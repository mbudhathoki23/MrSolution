using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.SystemSetting
{
    partial class FrmSalesSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSalesSetting));
            this.BtnSalesServiceTerm = new System.Windows.Forms.Button();
            this.TxtSalesServiceTerm = new MrTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.BtnSalesDiscountTerm = new System.Windows.Forms.Button();
            this.TxtSalesDiscountTerm = new MrTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CmbCreditBalanceWarning = new System.Windows.Forms.ComboBox();
            this.lbl_CreditDaysWarmning = new System.Windows.Forms.Label();
            this.lbl_CreditBalanceWar = new System.Windows.Forms.Label();
            this.CmbCreditDaysWarning = new System.Windows.Forms.ComboBox();
            this.BtnSalesVAT = new System.Windows.Forms.Button();
            this.BtnSalesReturn = new System.Windows.Forms.Button();
            this.BtnSalesInvoice = new System.Windows.Forms.Button();
            this.TxtSalesLedger = new MrTextBox();
            this.lbl_SalesAc = new System.Windows.Forms.Label();
            this.lbl_SalesReturnAc = new System.Windows.Forms.Label();
            this.TxtReturnLedger = new MrTextBox();
            this.lbl_SalesVat = new System.Windows.Forms.Label();
            this.TxtSalesVat = new MrTextBox();
            this.ChkAvailabeStock = new System.Windows.Forms.CheckBox();
            this.ChkStockValueinSalesReturn = new System.Windows.Forms.CheckBox();
            this.ChkDescriptionEnable = new System.Windows.Forms.CheckBox();
            this.ChkDispatchOrder = new System.Windows.Forms.CheckBox();
            this.ChkTenderAmt = new System.Windows.Forms.CheckBox();
            this.ChkProductGroupwisebilling = new System.Windows.Forms.CheckBox();
            this.ChkAdvanceReceipt = new System.Windows.Forms.CheckBox();
            this.ChkPartyInfo = new System.Windows.Forms.CheckBox();
            this.ChkUpdateRate = new System.Windows.Forms.CheckBox();
            this.ChkChangeRate = new System.Windows.Forms.CheckBox();
            this.ChkLastRate = new System.Windows.Forms.CheckBox();
            this.ChkCarryRate = new System.Windows.Forms.CheckBox();
            this.ChkBasicAmtEnable = new System.Windows.Forms.CheckBox();
            this.ChkAltUnitEnable = new System.Windows.Forms.CheckBox();
            this.ChkUnitEnable = new System.Windows.Forms.CheckBox();
            this.ChkRemarksEnable = new System.Windows.Forms.CheckBox();
            this.ChkSubLedgerEnable = new System.Windows.Forms.CheckBox();
            this.ChkInvoiceDateEnable = new System.Windows.Forms.CheckBox();
            this.ChkQuotationEnable = new System.Windows.Forms.CheckBox();
            this.ChkDepartmentEnable = new System.Windows.Forms.CheckBox();
            this.ChkChallanEnable = new System.Windows.Forms.CheckBox();
            this.ChkOrderEnable = new System.Windows.Forms.CheckBox();
            this.ChkGodownEnable = new System.Windows.Forms.CheckBox();
            this.ChkAgentEnable = new System.Windows.Forms.CheckBox();
            this.ChkCurrencyEnable = new System.Windows.Forms.CheckBox();
            this.ChkAgentMandatory = new System.Windows.Forms.CheckBox();
            this.ChkGodownMandatory = new System.Windows.Forms.CheckBox();
            this.ChkSubLedgerMandatory = new System.Windows.Forms.CheckBox();
            this.ChkChallanMandatory = new System.Windows.Forms.CheckBox();
            this.ChkCurrencyMandatory = new System.Windows.Forms.CheckBox();
            this.ChkDeparmentMandatory = new System.Windows.Forms.CheckBox();
            this.ChkOrderMandatory = new System.Windows.Forms.CheckBox();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSaveClosed = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnProcuctDiscountTerm = new System.Windows.Forms.Button();
            this.TxtProductDiscountTerm = new MrTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.ChkRemarksMandatory = new System.Windows.Forms.CheckBox();
            this.ChkDispatchOrderMandatory = new System.Windows.Forms.CheckBox();
            this.ChkQuotationMandatory = new System.Windows.Forms.CheckBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.ChkDateChange = new System.Windows.Forms.CheckBox();
            this.ChkIndentEnable = new System.Windows.Forms.CheckBox();
            this.clsSeparator2 = new ClsSeparator();
            this.panel1 = new MrPanel();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnSalesServiceTerm
            // 
            this.BtnSalesServiceTerm.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSalesServiceTerm.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnSalesServiceTerm.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnSalesServiceTerm.Location = new System.Drawing.Point(661, 42);
            this.BtnSalesServiceTerm.Name = "BtnSalesServiceTerm";
            this.BtnSalesServiceTerm.Size = new System.Drawing.Size(26, 26);
            this.BtnSalesServiceTerm.TabIndex = 198;
            this.BtnSalesServiceTerm.TabStop = false;
            this.BtnSalesServiceTerm.UseVisualStyleBackColor = true;
            this.BtnSalesServiceTerm.Click += new System.EventHandler(this.BtnSalesServiceTerm_Click);
            // 
            // TxtSalesServiceTerm
            // 
            this.TxtSalesServiceTerm.BackColor = System.Drawing.Color.White;
            this.TxtSalesServiceTerm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSalesServiceTerm.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSalesServiceTerm.Location = new System.Drawing.Point(466, 42);
            this.TxtSalesServiceTerm.MaxLength = 255;
            this.TxtSalesServiceTerm.Name = "TxtSalesServiceTerm";
            this.TxtSalesServiceTerm.ReadOnly = true;
            this.TxtSalesServiceTerm.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtSalesServiceTerm.Size = new System.Drawing.Size(192, 23);
            this.TxtSalesServiceTerm.TabIndex = 2;
            this.TxtSalesServiceTerm.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtSalesServiceTerm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSalesServiceTerm_KeyDown);
            this.TxtSalesServiceTerm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtSalesServiceTerm.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label4.Location = new System.Drawing.Point(334, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 19);
            this.label4.TabIndex = 196;
            this.label4.Text = "Service Chrg (B)";
            // 
            // BtnSalesDiscountTerm
            // 
            this.BtnSalesDiscountTerm.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSalesDiscountTerm.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnSalesDiscountTerm.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnSalesDiscountTerm.Location = new System.Drawing.Point(301, 40);
            this.BtnSalesDiscountTerm.Name = "BtnSalesDiscountTerm";
            this.BtnSalesDiscountTerm.Size = new System.Drawing.Size(26, 26);
            this.BtnSalesDiscountTerm.TabIndex = 194;
            this.BtnSalesDiscountTerm.TabStop = false;
            this.BtnSalesDiscountTerm.UseVisualStyleBackColor = true;
            this.BtnSalesDiscountTerm.Click += new System.EventHandler(this.BtnSalesDiscountTerm_Click);
            // 
            // TxtSalesDiscountTerm
            // 
            this.TxtSalesDiscountTerm.BackColor = System.Drawing.Color.White;
            this.TxtSalesDiscountTerm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSalesDiscountTerm.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSalesDiscountTerm.Location = new System.Drawing.Point(106, 40);
            this.TxtSalesDiscountTerm.MaxLength = 255;
            this.TxtSalesDiscountTerm.Name = "TxtSalesDiscountTerm";
            this.TxtSalesDiscountTerm.ReadOnly = true;
            this.TxtSalesDiscountTerm.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtSalesDiscountTerm.Size = new System.Drawing.Size(192, 23);
            this.TxtSalesDiscountTerm.TabIndex = 1;
            this.TxtSalesDiscountTerm.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtSalesDiscountTerm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSalesDiscountTerm_KeyDown);
            this.TxtSalesDiscountTerm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtSalesDiscountTerm.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label2.Location = new System.Drawing.Point(347, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 19);
            this.label2.TabIndex = 190;
            this.label2.Text = "Discount (P)";
            // 
            // CmbCreditBalanceWarning
            // 
            this.CmbCreditBalanceWarning.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbCreditBalanceWarning.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbCreditBalanceWarning.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbCreditBalanceWarning.FormattingEnabled = true;
            this.CmbCreditBalanceWarning.Items.AddRange(new object[] {
            "Ignore",
            "Warning",
            "Block"});
            this.CmbCreditBalanceWarning.Location = new System.Drawing.Point(116, 45);
            this.CmbCreditBalanceWarning.Name = "CmbCreditBalanceWarning";
            this.CmbCreditBalanceWarning.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CmbCreditBalanceWarning.Size = new System.Drawing.Size(158, 26);
            this.CmbCreditBalanceWarning.TabIndex = 1;
            this.CmbCreditBalanceWarning.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            // 
            // lbl_CreditDaysWarmning
            // 
            this.lbl_CreditDaysWarmning.AutoSize = true;
            this.lbl_CreditDaysWarmning.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CreditDaysWarmning.Location = new System.Drawing.Point(3, 22);
            this.lbl_CreditDaysWarmning.Name = "lbl_CreditDaysWarmning";
            this.lbl_CreditDaysWarmning.Size = new System.Drawing.Size(90, 19);
            this.lbl_CreditDaysWarmning.TabIndex = 187;
            this.lbl_CreditDaysWarmning.Text = "Credit Days";
            // 
            // lbl_CreditBalanceWar
            // 
            this.lbl_CreditBalanceWar.AutoSize = true;
            this.lbl_CreditBalanceWar.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CreditBalanceWar.Location = new System.Drawing.Point(4, 49);
            this.lbl_CreditBalanceWar.Name = "lbl_CreditBalanceWar";
            this.lbl_CreditBalanceWar.Size = new System.Drawing.Size(113, 19);
            this.lbl_CreditBalanceWar.TabIndex = 179;
            this.lbl_CreditBalanceWar.Text = "Credit Balance";
            // 
            // CmbCreditDaysWarning
            // 
            this.CmbCreditDaysWarning.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbCreditDaysWarning.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbCreditDaysWarning.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbCreditDaysWarning.FormattingEnabled = true;
            this.CmbCreditDaysWarning.Items.AddRange(new object[] {
            "Ignore",
            "Warning",
            "Block"});
            this.CmbCreditDaysWarning.Location = new System.Drawing.Point(116, 18);
            this.CmbCreditDaysWarning.Name = "CmbCreditDaysWarning";
            this.CmbCreditDaysWarning.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CmbCreditDaysWarning.Size = new System.Drawing.Size(158, 26);
            this.CmbCreditDaysWarning.TabIndex = 0;
            this.CmbCreditDaysWarning.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            // 
            // BtnSalesVAT
            // 
            this.BtnSalesVAT.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSalesVAT.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnSalesVAT.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnSalesVAT.Location = new System.Drawing.Point(301, 14);
            this.BtnSalesVAT.Name = "BtnSalesVAT";
            this.BtnSalesVAT.Size = new System.Drawing.Size(26, 26);
            this.BtnSalesVAT.TabIndex = 175;
            this.BtnSalesVAT.TabStop = false;
            this.BtnSalesVAT.UseVisualStyleBackColor = true;
            this.BtnSalesVAT.Click += new System.EventHandler(this.BtnSalesVAT_Click);
            // 
            // BtnSalesReturn
            // 
            this.BtnSalesReturn.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSalesReturn.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnSalesReturn.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnSalesReturn.Location = new System.Drawing.Point(377, 44);
            this.BtnSalesReturn.Name = "BtnSalesReturn";
            this.BtnSalesReturn.Size = new System.Drawing.Size(26, 23);
            this.BtnSalesReturn.TabIndex = 174;
            this.BtnSalesReturn.TabStop = false;
            this.BtnSalesReturn.UseVisualStyleBackColor = true;
            this.BtnSalesReturn.Click += new System.EventHandler(this.BtnSalesReturn_Click);
            // 
            // BtnSalesInvoice
            // 
            this.BtnSalesInvoice.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSalesInvoice.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnSalesInvoice.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnSalesInvoice.Location = new System.Drawing.Point(377, 19);
            this.BtnSalesInvoice.Name = "BtnSalesInvoice";
            this.BtnSalesInvoice.Size = new System.Drawing.Size(26, 23);
            this.BtnSalesInvoice.TabIndex = 173;
            this.BtnSalesInvoice.TabStop = false;
            this.BtnSalesInvoice.UseVisualStyleBackColor = true;
            this.BtnSalesInvoice.Click += new System.EventHandler(this.BtnSalesInvoice_Click);
            // 
            // TxtSalesLedger
            // 
            this.TxtSalesLedger.BackColor = System.Drawing.Color.White;
            this.TxtSalesLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSalesLedger.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSalesLedger.Location = new System.Drawing.Point(77, 19);
            this.TxtSalesLedger.MaxLength = 255;
            this.TxtSalesLedger.Name = "TxtSalesLedger";
            this.TxtSalesLedger.ReadOnly = true;
            this.TxtSalesLedger.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtSalesLedger.Size = new System.Drawing.Size(294, 23);
            this.TxtSalesLedger.TabIndex = 0;
            this.TxtSalesLedger.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtSalesLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSalesAc_KeyDown);
            this.TxtSalesLedger.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtSalesLedger.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // lbl_SalesAc
            // 
            this.lbl_SalesAc.AutoSize = true;
            this.lbl_SalesAc.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SalesAc.Location = new System.Drawing.Point(13, 21);
            this.lbl_SalesAc.Name = "lbl_SalesAc";
            this.lbl_SalesAc.Size = new System.Drawing.Size(60, 19);
            this.lbl_SalesAc.TabIndex = 168;
            this.lbl_SalesAc.Text = "Invoice";
            // 
            // lbl_SalesReturnAc
            // 
            this.lbl_SalesReturnAc.AutoSize = true;
            this.lbl_SalesReturnAc.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SalesReturnAc.Location = new System.Drawing.Point(13, 46);
            this.lbl_SalesReturnAc.Name = "lbl_SalesReturnAc";
            this.lbl_SalesReturnAc.Size = new System.Drawing.Size(58, 19);
            this.lbl_SalesReturnAc.TabIndex = 169;
            this.lbl_SalesReturnAc.Text = "Return";
            // 
            // TxtReturnLedger
            // 
            this.TxtReturnLedger.BackColor = System.Drawing.Color.White;
            this.TxtReturnLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtReturnLedger.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtReturnLedger.Location = new System.Drawing.Point(77, 44);
            this.TxtReturnLedger.MaxLength = 255;
            this.TxtReturnLedger.Name = "TxtReturnLedger";
            this.TxtReturnLedger.ReadOnly = true;
            this.TxtReturnLedger.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtReturnLedger.Size = new System.Drawing.Size(294, 23);
            this.TxtReturnLedger.TabIndex = 1;
            this.TxtReturnLedger.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtReturnLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSalesReturnAc_KeyDown);
            this.TxtReturnLedger.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtReturnLedger.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // lbl_SalesVat
            // 
            this.lbl_SalesVat.AutoSize = true;
            this.lbl_SalesVat.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SalesVat.Location = new System.Drawing.Point(5, 19);
            this.lbl_SalesVat.Name = "lbl_SalesVat";
            this.lbl_SalesVat.Size = new System.Drawing.Size(38, 19);
            this.lbl_SalesVat.TabIndex = 170;
            this.lbl_SalesVat.Text = "VAT";
            // 
            // TxtSalesVat
            // 
            this.TxtSalesVat.BackColor = System.Drawing.Color.White;
            this.TxtSalesVat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSalesVat.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSalesVat.Location = new System.Drawing.Point(106, 14);
            this.TxtSalesVat.MaxLength = 255;
            this.TxtSalesVat.Name = "TxtSalesVat";
            this.TxtSalesVat.ReadOnly = true;
            this.TxtSalesVat.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtSalesVat.Size = new System.Drawing.Size(192, 23);
            this.TxtSalesVat.TabIndex = 0;
            this.TxtSalesVat.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtSalesVat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSalesVat_KeyDown);
            this.TxtSalesVat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtSalesVat.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // ChkAvailabeStock
            // 
            this.ChkAvailabeStock.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAvailabeStock.Location = new System.Drawing.Point(6, 181);
            this.ChkAvailabeStock.Name = "ChkAvailabeStock";
            this.ChkAvailabeStock.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkAvailabeStock.Size = new System.Drawing.Size(138, 23);
            this.ChkAvailabeStock.TabIndex = 7;
            this.ChkAvailabeStock.Text = "Available Stock";
            this.ChkAvailabeStock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkAvailabeStock.UseVisualStyleBackColor = true;
            // 
            // ChkStockValueinSalesReturn
            // 
            this.ChkStockValueinSalesReturn.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkStockValueinSalesReturn.Location = new System.Drawing.Point(150, 19);
            this.ChkStockValueinSalesReturn.Name = "ChkStockValueinSalesReturn";
            this.ChkStockValueinSalesReturn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkStockValueinSalesReturn.Size = new System.Drawing.Size(163, 23);
            this.ChkStockValueinSalesReturn.TabIndex = 9;
            this.ChkStockValueinSalesReturn.Text = "Stock Val in SR";
            this.ChkStockValueinSalesReturn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkStockValueinSalesReturn.UseVisualStyleBackColor = true;
            // 
            // ChkDescriptionEnable
            // 
            this.ChkDescriptionEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDescriptionEnable.Location = new System.Drawing.Point(150, 180);
            this.ChkDescriptionEnable.Name = "ChkDescriptionEnable";
            this.ChkDescriptionEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDescriptionEnable.Size = new System.Drawing.Size(163, 23);
            this.ChkDescriptionEnable.TabIndex = 16;
            this.ChkDescriptionEnable.Text = "Descriptions";
            this.ChkDescriptionEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDescriptionEnable.UseVisualStyleBackColor = true;
            // 
            // ChkDispatchOrder
            // 
            this.ChkDispatchOrder.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDispatchOrder.Location = new System.Drawing.Point(326, 42);
            this.ChkDispatchOrder.Name = "ChkDispatchOrder";
            this.ChkDispatchOrder.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDispatchOrder.Size = new System.Drawing.Size(141, 23);
            this.ChkDispatchOrder.TabIndex = 19;
            this.ChkDispatchOrder.Text = "Dispatch Order";
            this.ChkDispatchOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDispatchOrder.UseVisualStyleBackColor = true;
            this.ChkDispatchOrder.CheckStateChanged += new System.EventHandler(this.ChkSDispatchOrder_CheckStateChanged);
            // 
            // ChkTenderAmt
            // 
            this.ChkTenderAmt.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkTenderAmt.Location = new System.Drawing.Point(150, 111);
            this.ChkTenderAmt.Name = "ChkTenderAmt";
            this.ChkTenderAmt.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkTenderAmt.Size = new System.Drawing.Size(163, 23);
            this.ChkTenderAmt.TabIndex = 13;
            this.ChkTenderAmt.Text = "Tender Amount";
            this.ChkTenderAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkTenderAmt.UseVisualStyleBackColor = true;
            // 
            // ChkProductGroupwisebilling
            // 
            this.ChkProductGroupwisebilling.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkProductGroupwisebilling.Location = new System.Drawing.Point(150, 134);
            this.ChkProductGroupwisebilling.Name = "ChkProductGroupwisebilling";
            this.ChkProductGroupwisebilling.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkProductGroupwisebilling.Size = new System.Drawing.Size(163, 23);
            this.ChkProductGroupwisebilling.TabIndex = 14;
            this.ChkProductGroupwisebilling.Text = "Product GroupWise";
            this.ChkProductGroupwisebilling.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkProductGroupwisebilling.UseVisualStyleBackColor = true;
            // 
            // ChkAdvanceReceipt
            // 
            this.ChkAdvanceReceipt.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAdvanceReceipt.Location = new System.Drawing.Point(150, 157);
            this.ChkAdvanceReceipt.Name = "ChkAdvanceReceipt";
            this.ChkAdvanceReceipt.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkAdvanceReceipt.Size = new System.Drawing.Size(163, 23);
            this.ChkAdvanceReceipt.TabIndex = 15;
            this.ChkAdvanceReceipt.Text = "Advance Receipt";
            this.ChkAdvanceReceipt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkAdvanceReceipt.UseVisualStyleBackColor = true;
            // 
            // ChkPartyInfo
            // 
            this.ChkPartyInfo.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkPartyInfo.Location = new System.Drawing.Point(150, 42);
            this.ChkPartyInfo.Name = "ChkPartyInfo";
            this.ChkPartyInfo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkPartyInfo.Size = new System.Drawing.Size(163, 23);
            this.ChkPartyInfo.TabIndex = 10;
            this.ChkPartyInfo.Text = "Party Info";
            this.ChkPartyInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkPartyInfo.UseVisualStyleBackColor = true;
            // 
            // ChkUpdateRate
            // 
            this.ChkUpdateRate.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkUpdateRate.Location = new System.Drawing.Point(150, 88);
            this.ChkUpdateRate.Name = "ChkUpdateRate";
            this.ChkUpdateRate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkUpdateRate.Size = new System.Drawing.Size(163, 23);
            this.ChkUpdateRate.TabIndex = 12;
            this.ChkUpdateRate.Text = "Update Rate";
            this.ChkUpdateRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkUpdateRate.UseVisualStyleBackColor = true;
            // 
            // ChkChangeRate
            // 
            this.ChkChangeRate.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkChangeRate.Location = new System.Drawing.Point(6, 66);
            this.ChkChangeRate.Name = "ChkChangeRate";
            this.ChkChangeRate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkChangeRate.Size = new System.Drawing.Size(138, 23);
            this.ChkChangeRate.TabIndex = 2;
            this.ChkChangeRate.Text = "Change Rate";
            this.ChkChangeRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkChangeRate.UseVisualStyleBackColor = true;
            // 
            // ChkLastRate
            // 
            this.ChkLastRate.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkLastRate.Location = new System.Drawing.Point(6, 43);
            this.ChkLastRate.Name = "ChkLastRate";
            this.ChkLastRate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkLastRate.Size = new System.Drawing.Size(138, 23);
            this.ChkLastRate.TabIndex = 1;
            this.ChkLastRate.Text = "Last Rate";
            this.ChkLastRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkLastRate.UseVisualStyleBackColor = true;
            // 
            // ChkCarryRate
            // 
            this.ChkCarryRate.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkCarryRate.Location = new System.Drawing.Point(6, 20);
            this.ChkCarryRate.Name = "ChkCarryRate";
            this.ChkCarryRate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkCarryRate.Size = new System.Drawing.Size(138, 23);
            this.ChkCarryRate.TabIndex = 0;
            this.ChkCarryRate.Text = "Carry Rate";
            this.ChkCarryRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkCarryRate.UseVisualStyleBackColor = true;
            // 
            // ChkBasicAmtEnable
            // 
            this.ChkBasicAmtEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkBasicAmtEnable.Location = new System.Drawing.Point(6, 158);
            this.ChkBasicAmtEnable.Name = "ChkBasicAmtEnable";
            this.ChkBasicAmtEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkBasicAmtEnable.Size = new System.Drawing.Size(138, 23);
            this.ChkBasicAmtEnable.TabIndex = 6;
            this.ChkBasicAmtEnable.Text = "Basic Amount";
            this.ChkBasicAmtEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkBasicAmtEnable.UseVisualStyleBackColor = true;
            // 
            // ChkAltUnitEnable
            // 
            this.ChkAltUnitEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAltUnitEnable.Location = new System.Drawing.Point(6, 112);
            this.ChkAltUnitEnable.Name = "ChkAltUnitEnable";
            this.ChkAltUnitEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkAltUnitEnable.Size = new System.Drawing.Size(138, 23);
            this.ChkAltUnitEnable.TabIndex = 4;
            this.ChkAltUnitEnable.Text = "Alt Unit";
            this.ChkAltUnitEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkAltUnitEnable.UseVisualStyleBackColor = true;
            // 
            // ChkUnitEnable
            // 
            this.ChkUnitEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkUnitEnable.Location = new System.Drawing.Point(6, 204);
            this.ChkUnitEnable.Name = "ChkUnitEnable";
            this.ChkUnitEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkUnitEnable.Size = new System.Drawing.Size(138, 23);
            this.ChkUnitEnable.TabIndex = 8;
            this.ChkUnitEnable.Text = "Unit";
            this.ChkUnitEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkUnitEnable.UseVisualStyleBackColor = true;
            this.ChkUnitEnable.Visible = false;
            // 
            // ChkRemarksEnable
            // 
            this.ChkRemarksEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkRemarksEnable.Location = new System.Drawing.Point(150, 65);
            this.ChkRemarksEnable.Name = "ChkRemarksEnable";
            this.ChkRemarksEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkRemarksEnable.Size = new System.Drawing.Size(163, 23);
            this.ChkRemarksEnable.TabIndex = 11;
            this.ChkRemarksEnable.Text = "Remarks";
            this.ChkRemarksEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkRemarksEnable.UseVisualStyleBackColor = true;
            this.ChkRemarksEnable.CheckStateChanged += new System.EventHandler(this.ChkSEnableRemarks_CheckStateChanged);
            // 
            // ChkSubLedgerEnable
            // 
            this.ChkSubLedgerEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSubLedgerEnable.Location = new System.Drawing.Point(326, 109);
            this.ChkSubLedgerEnable.Name = "ChkSubLedgerEnable";
            this.ChkSubLedgerEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSubLedgerEnable.Size = new System.Drawing.Size(141, 22);
            this.ChkSubLedgerEnable.TabIndex = 22;
            this.ChkSubLedgerEnable.Text = "Sub Ledger";
            this.ChkSubLedgerEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSubLedgerEnable.UseVisualStyleBackColor = true;
            this.ChkSubLedgerEnable.CheckStateChanged += new System.EventHandler(this.ChkSEnableSubLedger_CheckStateChanged);
            // 
            // ChkInvoiceDateEnable
            // 
            this.ChkInvoiceDateEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkInvoiceDateEnable.Location = new System.Drawing.Point(150, 201);
            this.ChkInvoiceDateEnable.Name = "ChkInvoiceDateEnable";
            this.ChkInvoiceDateEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkInvoiceDateEnable.Size = new System.Drawing.Size(163, 23);
            this.ChkInvoiceDateEnable.TabIndex = 17;
            this.ChkInvoiceDateEnable.Text = "Invoice Date";
            this.ChkInvoiceDateEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkInvoiceDateEnable.UseVisualStyleBackColor = true;
            // 
            // ChkQuotationEnable
            // 
            this.ChkQuotationEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkQuotationEnable.Location = new System.Drawing.Point(326, 20);
            this.ChkQuotationEnable.Name = "ChkQuotationEnable";
            this.ChkQuotationEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkQuotationEnable.Size = new System.Drawing.Size(141, 22);
            this.ChkQuotationEnable.TabIndex = 18;
            this.ChkQuotationEnable.Text = "Quotation";
            this.ChkQuotationEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkQuotationEnable.UseVisualStyleBackColor = true;
            this.ChkQuotationEnable.CheckedChanged += new System.EventHandler(this.ChkSEnableQuotation_CheckedChanged);
            // 
            // ChkDepartmentEnable
            // 
            this.ChkDepartmentEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDepartmentEnable.Location = new System.Drawing.Point(326, 153);
            this.ChkDepartmentEnable.Name = "ChkDepartmentEnable";
            this.ChkDepartmentEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDepartmentEnable.Size = new System.Drawing.Size(141, 22);
            this.ChkDepartmentEnable.TabIndex = 24;
            this.ChkDepartmentEnable.Text = "Department";
            this.ChkDepartmentEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDepartmentEnable.UseVisualStyleBackColor = true;
            this.ChkDepartmentEnable.CheckStateChanged += new System.EventHandler(this.ChkDepartmentEnable_CheckStateChanged);
            // 
            // ChkChallanEnable
            // 
            this.ChkChallanEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkChallanEnable.Location = new System.Drawing.Point(326, 87);
            this.ChkChallanEnable.Name = "ChkChallanEnable";
            this.ChkChallanEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkChallanEnable.Size = new System.Drawing.Size(141, 22);
            this.ChkChallanEnable.TabIndex = 21;
            this.ChkChallanEnable.Text = "Challan";
            this.ChkChallanEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkChallanEnable.UseVisualStyleBackColor = true;
            this.ChkChallanEnable.CheckStateChanged += new System.EventHandler(this.ChkSEnableChallan_CheckStateChanged);
            // 
            // ChkOrderEnable
            // 
            this.ChkOrderEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkOrderEnable.Location = new System.Drawing.Point(326, 65);
            this.ChkOrderEnable.Name = "ChkOrderEnable";
            this.ChkOrderEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkOrderEnable.Size = new System.Drawing.Size(141, 22);
            this.ChkOrderEnable.TabIndex = 20;
            this.ChkOrderEnable.Text = "Order";
            this.ChkOrderEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkOrderEnable.UseVisualStyleBackColor = true;
            this.ChkOrderEnable.CheckStateChanged += new System.EventHandler(this.ChkSEnableOrder_CheckStateChanged);
            // 
            // ChkGodownEnable
            // 
            this.ChkGodownEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkGodownEnable.Location = new System.Drawing.Point(326, 200);
            this.ChkGodownEnable.Name = "ChkGodownEnable";
            this.ChkGodownEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkGodownEnable.Size = new System.Drawing.Size(141, 25);
            this.ChkGodownEnable.TabIndex = 26;
            this.ChkGodownEnable.Text = "Godown";
            this.ChkGodownEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkGodownEnable.UseVisualStyleBackColor = true;
            this.ChkGodownEnable.CheckStateChanged += new System.EventHandler(this.ChkGodownEnable_CheckStateChanged);
            // 
            // ChkAgentEnable
            // 
            this.ChkAgentEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAgentEnable.Location = new System.Drawing.Point(326, 131);
            this.ChkAgentEnable.Name = "ChkAgentEnable";
            this.ChkAgentEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkAgentEnable.Size = new System.Drawing.Size(141, 22);
            this.ChkAgentEnable.TabIndex = 23;
            this.ChkAgentEnable.Text = "Agent";
            this.ChkAgentEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkAgentEnable.UseVisualStyleBackColor = true;
            this.ChkAgentEnable.CheckStateChanged += new System.EventHandler(this.ChkSEnableAgent_CheckStateChanged);
            // 
            // ChkCurrencyEnable
            // 
            this.ChkCurrencyEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkCurrencyEnable.Location = new System.Drawing.Point(326, 175);
            this.ChkCurrencyEnable.Name = "ChkCurrencyEnable";
            this.ChkCurrencyEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkCurrencyEnable.Size = new System.Drawing.Size(141, 25);
            this.ChkCurrencyEnable.TabIndex = 25;
            this.ChkCurrencyEnable.Text = "Currency";
            this.ChkCurrencyEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkCurrencyEnable.UseVisualStyleBackColor = true;
            this.ChkCurrencyEnable.CheckStateChanged += new System.EventHandler(this.ChkSEnableCurrency_CheckStateChanged);
            // 
            // ChkAgentMandatory
            // 
            this.ChkAgentMandatory.Enabled = false;
            this.ChkAgentMandatory.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAgentMandatory.Location = new System.Drawing.Point(7, 124);
            this.ChkAgentMandatory.Name = "ChkAgentMandatory";
            this.ChkAgentMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkAgentMandatory.Size = new System.Drawing.Size(135, 22);
            this.ChkAgentMandatory.TabIndex = 5;
            this.ChkAgentMandatory.Text = "Agent";
            this.ChkAgentMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkAgentMandatory.UseVisualStyleBackColor = true;
            // 
            // ChkGodownMandatory
            // 
            this.ChkGodownMandatory.Enabled = false;
            this.ChkGodownMandatory.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkGodownMandatory.Location = new System.Drawing.Point(7, 190);
            this.ChkGodownMandatory.Name = "ChkGodownMandatory";
            this.ChkGodownMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkGodownMandatory.Size = new System.Drawing.Size(135, 20);
            this.ChkGodownMandatory.TabIndex = 8;
            this.ChkGodownMandatory.Text = "Godown";
            this.ChkGodownMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkGodownMandatory.UseVisualStyleBackColor = true;
            // 
            // ChkSubLedgerMandatory
            // 
            this.ChkSubLedgerMandatory.Enabled = false;
            this.ChkSubLedgerMandatory.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSubLedgerMandatory.Location = new System.Drawing.Point(7, 102);
            this.ChkSubLedgerMandatory.Name = "ChkSubLedgerMandatory";
            this.ChkSubLedgerMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSubLedgerMandatory.Size = new System.Drawing.Size(135, 22);
            this.ChkSubLedgerMandatory.TabIndex = 4;
            this.ChkSubLedgerMandatory.Text = "Sub Ledger";
            this.ChkSubLedgerMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSubLedgerMandatory.UseVisualStyleBackColor = true;
            // 
            // ChkChallanMandatory
            // 
            this.ChkChallanMandatory.Enabled = false;
            this.ChkChallanMandatory.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkChallanMandatory.Location = new System.Drawing.Point(7, 82);
            this.ChkChallanMandatory.Name = "ChkChallanMandatory";
            this.ChkChallanMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkChallanMandatory.Size = new System.Drawing.Size(135, 20);
            this.ChkChallanMandatory.TabIndex = 3;
            this.ChkChallanMandatory.Text = "Challan";
            this.ChkChallanMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkChallanMandatory.UseVisualStyleBackColor = true;
            // 
            // ChkCurrencyMandatory
            // 
            this.ChkCurrencyMandatory.Enabled = false;
            this.ChkCurrencyMandatory.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkCurrencyMandatory.Location = new System.Drawing.Point(7, 168);
            this.ChkCurrencyMandatory.Name = "ChkCurrencyMandatory";
            this.ChkCurrencyMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkCurrencyMandatory.Size = new System.Drawing.Size(135, 22);
            this.ChkCurrencyMandatory.TabIndex = 7;
            this.ChkCurrencyMandatory.Text = "Currency";
            this.ChkCurrencyMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkCurrencyMandatory.UseVisualStyleBackColor = true;
            // 
            // ChkDeparmentMandatory
            // 
            this.ChkDeparmentMandatory.Enabled = false;
            this.ChkDeparmentMandatory.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDeparmentMandatory.Location = new System.Drawing.Point(7, 146);
            this.ChkDeparmentMandatory.Name = "ChkDeparmentMandatory";
            this.ChkDeparmentMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDeparmentMandatory.Size = new System.Drawing.Size(135, 22);
            this.ChkDeparmentMandatory.TabIndex = 6;
            this.ChkDeparmentMandatory.Text = "Department";
            this.ChkDeparmentMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDeparmentMandatory.UseVisualStyleBackColor = true;
            // 
            // ChkOrderMandatory
            // 
            this.ChkOrderMandatory.Enabled = false;
            this.ChkOrderMandatory.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkOrderMandatory.Location = new System.Drawing.Point(7, 62);
            this.ChkOrderMandatory.Name = "ChkOrderMandatory";
            this.ChkOrderMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkOrderMandatory.Size = new System.Drawing.Size(135, 20);
            this.ChkOrderMandatory.TabIndex = 2;
            this.ChkOrderMandatory.Text = "Order";
            this.ChkOrderMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkOrderMandatory.UseVisualStyleBackColor = true;
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(594, 394);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(94, 36);
            this.BtnSave.TabIndex = 6;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnSaveClosed
            // 
            this.BtnSaveClosed.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSaveClosed.Appearance.Options.UseFont = true;
            this.BtnSaveClosed.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSaveClosed.Location = new System.Drawing.Point(423, 394);
            this.BtnSaveClosed.Name = "BtnSaveClosed";
            this.BtnSaveClosed.Size = new System.Drawing.Size(170, 36);
            this.BtnSaveClosed.TabIndex = 5;
            this.BtnSaveClosed.Text = "SAVE && C&LOSED";
            this.BtnSaveClosed.Click += new System.EventHandler(this.BtnSaveClosed_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox4.Controls.Add(this.CmbCreditBalanceWarning);
            this.groupBox4.Controls.Add(this.lbl_CreditBalanceWar);
            this.groupBox4.Controls.Add(this.CmbCreditDaysWarning);
            this.groupBox4.Controls.Add(this.lbl_CreditDaysWarmning);
            this.groupBox4.Location = new System.Drawing.Point(416, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(277, 77);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Credit Tag";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox3.Controls.Add(this.BtnSalesServiceTerm);
            this.groupBox3.Controls.Add(this.lbl_SalesVat);
            this.groupBox3.Controls.Add(this.TxtSalesServiceTerm);
            this.groupBox3.Controls.Add(this.TxtSalesVat);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.BtnSalesVAT);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.BtnProcuctDiscountTerm);
            this.groupBox3.Controls.Add(this.BtnSalesDiscountTerm);
            this.groupBox3.Controls.Add(this.TxtProductDiscountTerm);
            this.groupBox3.Controls.Add(this.TxtSalesDiscountTerm);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox3.Location = new System.Drawing.Point(3, 80);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(690, 68);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Term Tag";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label1.Location = new System.Drawing.Point(2, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 19);
            this.label1.TabIndex = 190;
            this.label1.Text = "Discount (B)";
            // 
            // BtnProcuctDiscountTerm
            // 
            this.BtnProcuctDiscountTerm.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnProcuctDiscountTerm.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnProcuctDiscountTerm.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnProcuctDiscountTerm.Location = new System.Drawing.Point(661, 15);
            this.BtnProcuctDiscountTerm.Name = "BtnProcuctDiscountTerm";
            this.BtnProcuctDiscountTerm.Size = new System.Drawing.Size(26, 26);
            this.BtnProcuctDiscountTerm.TabIndex = 194;
            this.BtnProcuctDiscountTerm.TabStop = false;
            this.BtnProcuctDiscountTerm.UseVisualStyleBackColor = true;
            this.BtnProcuctDiscountTerm.Click += new System.EventHandler(this.BtnProductDiscountTerm_Click);
            // 
            // TxtProductDiscountTerm
            // 
            this.TxtProductDiscountTerm.BackColor = System.Drawing.Color.White;
            this.TxtProductDiscountTerm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtProductDiscountTerm.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtProductDiscountTerm.Location = new System.Drawing.Point(466, 17);
            this.TxtProductDiscountTerm.MaxLength = 255;
            this.TxtProductDiscountTerm.Name = "TxtProductDiscountTerm";
            this.TxtProductDiscountTerm.ReadOnly = true;
            this.TxtProductDiscountTerm.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtProductDiscountTerm.Size = new System.Drawing.Size(192, 23);
            this.TxtProductDiscountTerm.TabIndex = 3;
            this.TxtProductDiscountTerm.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtProductDiscountTerm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtProductDiscountTerm_KeyDown);
            this.TxtProductDiscountTerm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtProductDiscountTerm.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox2.Controls.Add(this.lbl_SalesAc);
            this.groupBox2.Controls.Add(this.TxtReturnLedger);
            this.groupBox2.Controls.Add(this.lbl_SalesReturnAc);
            this.groupBox2.Controls.Add(this.TxtSalesLedger);
            this.groupBox2.Controls.Add(this.BtnSalesInvoice);
            this.groupBox2.Controls.Add(this.BtnSalesReturn);
            this.groupBox2.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(413, 77);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ledger Tag";
            // 
            // groupBox9
            // 
            this.groupBox9.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox9.Controls.Add(this.ChkRemarksMandatory);
            this.groupBox9.Controls.Add(this.ChkDispatchOrderMandatory);
            this.groupBox9.Controls.Add(this.ChkQuotationMandatory);
            this.groupBox9.Controls.Add(this.ChkOrderMandatory);
            this.groupBox9.Controls.Add(this.ChkChallanMandatory);
            this.groupBox9.Controls.Add(this.ChkSubLedgerMandatory);
            this.groupBox9.Controls.Add(this.ChkCurrencyMandatory);
            this.groupBox9.Controls.Add(this.ChkGodownMandatory);
            this.groupBox9.Controls.Add(this.ChkAgentMandatory);
            this.groupBox9.Controls.Add(this.ChkDeparmentMandatory);
            this.groupBox9.Location = new System.Drawing.Point(520, 149);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(173, 239);
            this.groupBox9.TabIndex = 4;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Mandatory Tag";
            // 
            // ChkRemarksMandatory
            // 
            this.ChkRemarksMandatory.Enabled = false;
            this.ChkRemarksMandatory.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkRemarksMandatory.Location = new System.Drawing.Point(7, 210);
            this.ChkRemarksMandatory.Name = "ChkRemarksMandatory";
            this.ChkRemarksMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkRemarksMandatory.Size = new System.Drawing.Size(135, 20);
            this.ChkRemarksMandatory.TabIndex = 9;
            this.ChkRemarksMandatory.Text = "Remarks";
            this.ChkRemarksMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkRemarksMandatory.UseVisualStyleBackColor = true;
            // 
            // ChkDispatchOrderMandatory
            // 
            this.ChkDispatchOrderMandatory.Enabled = false;
            this.ChkDispatchOrderMandatory.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDispatchOrderMandatory.Location = new System.Drawing.Point(7, 42);
            this.ChkDispatchOrderMandatory.Name = "ChkDispatchOrderMandatory";
            this.ChkDispatchOrderMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDispatchOrderMandatory.Size = new System.Drawing.Size(135, 20);
            this.ChkDispatchOrderMandatory.TabIndex = 1;
            this.ChkDispatchOrderMandatory.Text = "Dispatch Order";
            this.ChkDispatchOrderMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDispatchOrderMandatory.UseVisualStyleBackColor = true;
            // 
            // ChkQuotationMandatory
            // 
            this.ChkQuotationMandatory.Enabled = false;
            this.ChkQuotationMandatory.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkQuotationMandatory.Location = new System.Drawing.Point(7, 22);
            this.ChkQuotationMandatory.Name = "ChkQuotationMandatory";
            this.ChkQuotationMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkQuotationMandatory.Size = new System.Drawing.Size(135, 20);
            this.ChkQuotationMandatory.TabIndex = 0;
            this.ChkQuotationMandatory.Text = "Quotation";
            this.ChkQuotationMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkQuotationMandatory.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox7.Controls.Add(this.ChkOrderEnable);
            this.groupBox7.Controls.Add(this.ChkAvailabeStock);
            this.groupBox7.Controls.Add(this.ChkQuotationEnable);
            this.groupBox7.Controls.Add(this.ChkChallanEnable);
            this.groupBox7.Controls.Add(this.ChkPartyInfo);
            this.groupBox7.Controls.Add(this.ChkDispatchOrder);
            this.groupBox7.Controls.Add(this.ChkStockValueinSalesReturn);
            this.groupBox7.Controls.Add(this.ChkCurrencyEnable);
            this.groupBox7.Controls.Add(this.ChkDescriptionEnable);
            this.groupBox7.Controls.Add(this.ChkSubLedgerEnable);
            this.groupBox7.Controls.Add(this.ChkDateChange);
            this.groupBox7.Controls.Add(this.ChkGodownEnable);
            this.groupBox7.Controls.Add(this.ChkRemarksEnable);
            this.groupBox7.Controls.Add(this.ChkDepartmentEnable);
            this.groupBox7.Controls.Add(this.ChkTenderAmt);
            this.groupBox7.Controls.Add(this.ChkAgentEnable);
            this.groupBox7.Controls.Add(this.ChkChangeRate);
            this.groupBox7.Controls.Add(this.ChkLastRate);
            this.groupBox7.Controls.Add(this.ChkProductGroupwisebilling);
            this.groupBox7.Controls.Add(this.ChkIndentEnable);
            this.groupBox7.Controls.Add(this.ChkCarryRate);
            this.groupBox7.Controls.Add(this.ChkAdvanceReceipt);
            this.groupBox7.Controls.Add(this.ChkAltUnitEnable);
            this.groupBox7.Controls.Add(this.ChkBasicAmtEnable);
            this.groupBox7.Controls.Add(this.ChkInvoiceDateEnable);
            this.groupBox7.Controls.Add(this.ChkUpdateRate);
            this.groupBox7.Controls.Add(this.ChkUnitEnable);
            this.groupBox7.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.Location = new System.Drawing.Point(3, 149);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(517, 239);
            this.groupBox7.TabIndex = 3;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Sales Tag";
            // 
            // ChkDateChange
            // 
            this.ChkDateChange.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDateChange.Location = new System.Drawing.Point(6, 89);
            this.ChkDateChange.Name = "ChkDateChange";
            this.ChkDateChange.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDateChange.Size = new System.Drawing.Size(138, 23);
            this.ChkDateChange.TabIndex = 3;
            this.ChkDateChange.Text = "Date Change";
            this.ChkDateChange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDateChange.UseVisualStyleBackColor = true;
            // 
            // ChkIndentEnable
            // 
            this.ChkIndentEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIndentEnable.Location = new System.Drawing.Point(6, 135);
            this.ChkIndentEnable.Name = "ChkIndentEnable";
            this.ChkIndentEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIndentEnable.Size = new System.Drawing.Size(138, 23);
            this.ChkIndentEnable.TabIndex = 5;
            this.ChkIndentEnable.Text = "Indent";
            this.ChkIndentEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIndentEnable.UseVisualStyleBackColor = true;
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator2.Location = new System.Drawing.Point(8, 390);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(684, 2);
            this.clsSeparator2.TabIndex = 41;
            this.clsSeparator2.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.groupBox9);
            this.panel1.Controls.Add(this.BtnSave);
            this.panel1.Controls.Add(this.clsSeparator2);
            this.panel1.Controls.Add(this.BtnSaveClosed);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(695, 433);
            this.panel1.TabIndex = 0;
            // 
            // FrmSalesSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(695, 433);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSalesSetting";
            this.ShowIcon = false;
            this.Text = "SALES SETTING";
            this.Load += new System.EventHandler(this.FrmSalesSetting_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button BtnSalesVAT;
        private System.Windows.Forms.Button BtnSalesReturn;
        private System.Windows.Forms.Button BtnSalesInvoice;
        private System.Windows.Forms.Label lbl_SalesAc;
        private System.Windows.Forms.Label lbl_SalesReturnAc;
        private System.Windows.Forms.Label lbl_SalesVat;
        private System.Windows.Forms.Label lbl_CreditBalanceWar;
        private System.Windows.Forms.ComboBox CmbCreditDaysWarning;
        private System.Windows.Forms.ComboBox CmbCreditBalanceWarning;
        private System.Windows.Forms.Label lbl_CreditDaysWarmning;
        private System.Windows.Forms.CheckBox ChkInvoiceDateEnable;
        private System.Windows.Forms.CheckBox ChkQuotationEnable;
        private System.Windows.Forms.CheckBox ChkDepartmentEnable;
        private System.Windows.Forms.CheckBox ChkChallanEnable;
        private System.Windows.Forms.CheckBox ChkOrderEnable;
        private System.Windows.Forms.CheckBox ChkGodownEnable;
        private System.Windows.Forms.CheckBox ChkAgentEnable;
        private System.Windows.Forms.CheckBox ChkCurrencyEnable;
        private System.Windows.Forms.CheckBox ChkUpdateRate;
        private System.Windows.Forms.CheckBox ChkChangeRate;
        private System.Windows.Forms.CheckBox ChkLastRate;
        private System.Windows.Forms.CheckBox ChkCarryRate;
        private System.Windows.Forms.CheckBox ChkBasicAmtEnable;
        private System.Windows.Forms.CheckBox ChkAltUnitEnable;
        private System.Windows.Forms.CheckBox ChkUnitEnable;
        private System.Windows.Forms.CheckBox ChkRemarksEnable;
        private System.Windows.Forms.CheckBox ChkSubLedgerEnable;
        private System.Windows.Forms.CheckBox ChkAvailabeStock;
        private System.Windows.Forms.CheckBox ChkStockValueinSalesReturn;
        private System.Windows.Forms.CheckBox ChkDescriptionEnable;
        private System.Windows.Forms.CheckBox ChkDispatchOrder;
        private System.Windows.Forms.CheckBox ChkTenderAmt;
        private System.Windows.Forms.CheckBox ChkProductGroupwisebilling;
        private System.Windows.Forms.CheckBox ChkAdvanceReceipt;
        private System.Windows.Forms.CheckBox ChkPartyInfo;
        private System.Windows.Forms.CheckBox ChkDeparmentMandatory;
        private System.Windows.Forms.CheckBox ChkOrderMandatory;
        private System.Windows.Forms.CheckBox ChkChallanMandatory;
        private System.Windows.Forms.CheckBox ChkCurrencyMandatory;
        private System.Windows.Forms.CheckBox ChkGodownMandatory;
        private System.Windows.Forms.CheckBox ChkSubLedgerMandatory;
        private System.Windows.Forms.CheckBox ChkAgentMandatory;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SimpleButton BtnSaveClosed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnSalesDiscountTerm;
        private System.Windows.Forms.Button BtnSalesServiceTerm;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckBox ChkDispatchOrderMandatory;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnProcuctDiscountTerm;
        private System.Windows.Forms.CheckBox ChkQuotationMandatory;
        private System.Windows.Forms.CheckBox ChkIndentEnable;
        private System.Windows.Forms.CheckBox ChkDateChange;
        private ClsSeparator clsSeparator2;
        private System.Windows.Forms.CheckBox ChkRemarksMandatory;
        private MrTextBox TxtSalesLedger;
        private MrTextBox TxtReturnLedger;
        private MrTextBox TxtSalesVat;
        private MrTextBox TxtSalesDiscountTerm;
        private MrTextBox TxtSalesServiceTerm;
        private MrTextBox TxtProductDiscountTerm;
        private MrPanel panel1;
    }
}