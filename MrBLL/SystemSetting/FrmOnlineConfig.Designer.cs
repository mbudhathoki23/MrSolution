using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.SystemSetting
{
    partial class FrmOnlineConfig
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
            this.roundPanel1 = new MrDAL.Control.ControlsEx.Control.RoundPanel();
            this.TxtApiUrl = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtOrginId = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.roundPanel2 = new MrDAL.Control.ControlsEx.Control.RoundPanel();
            this.ChkTableId = new System.Windows.Forms.CheckBox();
            this.ChkBranch = new System.Windows.Forms.CheckBox();
            this.ChkProduct = new System.Windows.Forms.CheckBox();
            this.ChkBillingTerm = new System.Windows.Forms.CheckBox();
            this.ChkAgent = new System.Windows.Forms.CheckBox();
            this.ChkGeneralLedger = new System.Windows.Forms.CheckBox();
            this.ChkCostCenter = new System.Windows.Forms.CheckBox();
            this.ChkMember = new System.Windows.Forms.CheckBox();
            this.ChkArea = new System.Windows.Forms.CheckBox();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.StorePanel = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.roundPanel4 = new MrDAL.Control.ControlsEx.Control.RoundPanel();
            this.ChkStockAdjustment = new System.Windows.Forms.CheckBox();
            this.roundPanel3 = new MrDAL.Control.ControlsEx.Control.RoundPanel();
            this.ChkPurchaseAdditional = new System.Windows.Forms.CheckBox();
            this.ChkSalesAdditional = new System.Windows.Forms.CheckBox();
            this.ChkSalesReturn = new System.Windows.Forms.CheckBox();
            this.ChkCashBank = new System.Windows.Forms.CheckBox();
            this.ChkPurchaseReturn = new System.Windows.Forms.CheckBox();
            this.ChkPurchaseInvoice = new System.Windows.Forms.CheckBox();
            this.ChkPurchaseChallan = new System.Windows.Forms.CheckBox();
            this.ChkPurchaseOrder = new System.Windows.Forms.CheckBox();
            this.ChkSalesOrder = new System.Windows.Forms.CheckBox();
            this.ChkSalesInvoice = new System.Windows.Forms.CheckBox();
            this.ChkProductOpening = new System.Windows.Forms.CheckBox();
            this.ChkLedgerOpening = new System.Windows.Forms.CheckBox();
            this.ChkPDCVoucher = new System.Windows.Forms.CheckBox();
            this.ChkPurchaseIndent = new System.Windows.Forms.CheckBox();
            this.ChkSalesQuotation = new System.Windows.Forms.CheckBox();
            this.ChkSalesChallan = new System.Windows.Forms.CheckBox();
            this.ChkNotesRegister = new System.Windows.Forms.CheckBox();
            this.ChkJournalVoucher = new System.Windows.Forms.CheckBox();
            this.roundPanel1.SuspendLayout();
            this.roundPanel2.SuspendLayout();
            this.StorePanel.SuspendLayout();
            this.roundPanel4.SuspendLayout();
            this.roundPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // roundPanel1
            // 
            this.roundPanel1.BackColor = System.Drawing.Color.Transparent;
            this.roundPanel1.Controls.Add(this.TxtApiUrl);
            this.roundPanel1.Controls.Add(this.TxtOrginId);
            this.roundPanel1.Controls.Add(this.label1);
            this.roundPanel1.Controls.Add(this.label3);
            this.roundPanel1.Location = new System.Drawing.Point(8, 336);
            this.roundPanel1.Name = "roundPanel1";
            this.roundPanel1.Radious = 25;
            this.roundPanel1.Size = new System.Drawing.Size(497, 83);
            this.roundPanel1.TabIndex = 25;
            this.roundPanel1.TabStop = false;
            this.roundPanel1.Text = "ONLINE SERVER CONNECTION";
            this.roundPanel1.TitleBackColor = System.Drawing.Color.DarkSlateBlue;
            this.roundPanel1.TitleFont = new System.Drawing.Font("Bookman Old Style", 11F);
            this.roundPanel1.TitleForeColor = System.Drawing.Color.White;
            this.roundPanel1.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
            // 
            // TxtApiUrl
            // 
            this.TxtApiUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtApiUrl.Location = new System.Drawing.Point(92, 27);
            this.TxtApiUrl.Name = "TxtApiUrl";
            this.TxtApiUrl.Size = new System.Drawing.Size(401, 25);
            this.TxtApiUrl.TabIndex = 10;
            // 
            // TxtOrginId
            // 
            this.TxtOrginId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtOrginId.Location = new System.Drawing.Point(92, 55);
            this.TxtOrginId.Name = "TxtOrginId";
            this.TxtOrginId.Size = new System.Drawing.Size(401, 25);
            this.TxtOrginId.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 19);
            this.label1.TabIndex = 16;
            this.label1.Text = "API URL";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 19);
            this.label3.TabIndex = 18;
            this.label3.Text = "API Key";
            // 
            // roundPanel2
            // 
            this.roundPanel2.BackColor = System.Drawing.Color.Transparent;
            this.roundPanel2.Controls.Add(this.ChkTableId);
            this.roundPanel2.Controls.Add(this.ChkBranch);
            this.roundPanel2.Controls.Add(this.ChkProduct);
            this.roundPanel2.Controls.Add(this.ChkBillingTerm);
            this.roundPanel2.Controls.Add(this.ChkAgent);
            this.roundPanel2.Controls.Add(this.ChkGeneralLedger);
            this.roundPanel2.Controls.Add(this.ChkCostCenter);
            this.roundPanel2.Controls.Add(this.ChkMember);
            this.roundPanel2.Controls.Add(this.ChkArea);
            this.roundPanel2.Location = new System.Drawing.Point(8, 5);
            this.roundPanel2.Name = "roundPanel2";
            this.roundPanel2.Radious = 25;
            this.roundPanel2.Size = new System.Drawing.Size(493, 99);
            this.roundPanel2.TabIndex = 26;
            this.roundPanel2.TabStop = false;
            this.roundPanel2.Text = "Sync Module";
            this.roundPanel2.TitleBackColor = System.Drawing.Color.DarkSlateBlue;
            this.roundPanel2.TitleFont = new System.Drawing.Font("Bookman Old Style", 11F);
            this.roundPanel2.TitleForeColor = System.Drawing.Color.White;
            this.roundPanel2.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
            // 
            // ChkTableId
            // 
            this.ChkTableId.AutoSize = true;
            this.ChkTableId.Location = new System.Drawing.Point(12, 73);
            this.ChkTableId.Name = "ChkTableId";
            this.ChkTableId.Size = new System.Drawing.Size(87, 23);
            this.ChkTableId.TabIndex = 3;
            this.ChkTableId.Text = "Table Id";
            this.ChkTableId.UseVisualStyleBackColor = true;
            // 
            // ChkBranch
            // 
            this.ChkBranch.AutoSize = true;
            this.ChkBranch.Location = new System.Drawing.Point(12, 27);
            this.ChkBranch.Name = "ChkBranch";
            this.ChkBranch.Size = new System.Drawing.Size(83, 23);
            this.ChkBranch.TabIndex = 1;
            this.ChkBranch.Text = "Branch";
            this.ChkBranch.UseVisualStyleBackColor = true;
            // 
            // ChkProduct
            // 
            this.ChkProduct.AutoSize = true;
            this.ChkProduct.Location = new System.Drawing.Point(316, 27);
            this.ChkProduct.Name = "ChkProduct";
            this.ChkProduct.Size = new System.Drawing.Size(85, 23);
            this.ChkProduct.TabIndex = 0;
            this.ChkProduct.Text = "Product";
            this.ChkProduct.UseVisualStyleBackColor = true;
            // 
            // ChkBillingTerm
            // 
            this.ChkBillingTerm.AutoSize = true;
            this.ChkBillingTerm.Location = new System.Drawing.Point(163, 50);
            this.ChkBillingTerm.Name = "ChkBillingTerm";
            this.ChkBillingTerm.Size = new System.Drawing.Size(129, 23);
            this.ChkBillingTerm.TabIndex = 0;
            this.ChkBillingTerm.Text = "Billing Terms";
            this.ChkBillingTerm.UseVisualStyleBackColor = true;
            // 
            // ChkAgent
            // 
            this.ChkAgent.AutoSize = true;
            this.ChkAgent.Location = new System.Drawing.Point(163, 73);
            this.ChkAgent.Name = "ChkAgent";
            this.ChkAgent.Size = new System.Drawing.Size(71, 23);
            this.ChkAgent.TabIndex = 0;
            this.ChkAgent.Text = "Agent";
            this.ChkAgent.UseVisualStyleBackColor = true;
            // 
            // ChkGeneralLedger
            // 
            this.ChkGeneralLedger.AutoSize = true;
            this.ChkGeneralLedger.Location = new System.Drawing.Point(12, 50);
            this.ChkGeneralLedger.Name = "ChkGeneralLedger";
            this.ChkGeneralLedger.Size = new System.Drawing.Size(145, 23);
            this.ChkGeneralLedger.TabIndex = 0;
            this.ChkGeneralLedger.Text = "General Ledger";
            this.ChkGeneralLedger.UseVisualStyleBackColor = true;
            // 
            // ChkCostCenter
            // 
            this.ChkCostCenter.AutoSize = true;
            this.ChkCostCenter.Location = new System.Drawing.Point(316, 50);
            this.ChkCostCenter.Name = "ChkCostCenter";
            this.ChkCostCenter.Size = new System.Drawing.Size(113, 23);
            this.ChkCostCenter.TabIndex = 0;
            this.ChkCostCenter.Text = "CostCenter";
            this.ChkCostCenter.UseVisualStyleBackColor = true;
            // 
            // ChkMember
            // 
            this.ChkMember.AutoSize = true;
            this.ChkMember.Location = new System.Drawing.Point(316, 73);
            this.ChkMember.Name = "ChkMember";
            this.ChkMember.Size = new System.Drawing.Size(88, 23);
            this.ChkMember.TabIndex = 0;
            this.ChkMember.Text = "Member";
            this.ChkMember.UseVisualStyleBackColor = true;
            // 
            // ChkArea
            // 
            this.ChkArea.AutoSize = true;
            this.ChkArea.Location = new System.Drawing.Point(163, 27);
            this.ChkArea.Name = "ChkArea";
            this.ChkArea.Size = new System.Drawing.Size(63, 23);
            this.ChkArea.TabIndex = 0;
            this.ChkArea.Text = "Area";
            this.ChkArea.UseVisualStyleBackColor = true;
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Location = new System.Drawing.Point(252, 421);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(106, 35);
            this.BtnSave.TabIndex = 27;
            this.BtnSave.Text = "SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.Location = new System.Drawing.Point(359, 421);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(106, 35);
            this.BtnCancel.TabIndex = 28;
            this.BtnCancel.Text = "CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.roundPanel4);
            this.StorePanel.Controls.Add(this.roundPanel3);
            this.StorePanel.Controls.Add(this.BtnCancel);
            this.StorePanel.Controls.Add(this.roundPanel2);
            this.StorePanel.Controls.Add(this.BtnSave);
            this.StorePanel.Controls.Add(this.roundPanel1);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(511, 461);
            this.StorePanel.TabIndex = 2;
            // 
            // roundPanel4
            // 
            this.roundPanel4.BackColor = System.Drawing.Color.Transparent;
            this.roundPanel4.Controls.Add(this.ChkStockAdjustment);
            this.roundPanel4.Location = new System.Drawing.Point(8, 281);
            this.roundPanel4.Name = "roundPanel4";
            this.roundPanel4.Radious = 25;
            this.roundPanel4.Size = new System.Drawing.Size(499, 54);
            this.roundPanel4.TabIndex = 29;
            this.roundPanel4.TabStop = false;
            this.roundPanel4.Text = "Sync Stock";
            this.roundPanel4.TitleBackColor = System.Drawing.Color.DarkSlateBlue;
            this.roundPanel4.TitleFont = new System.Drawing.Font("Bookman Old Style", 12F);
            this.roundPanel4.TitleForeColor = System.Drawing.Color.White;
            this.roundPanel4.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
            // 
            // ChkStockAdjustment
            // 
            this.ChkStockAdjustment.AutoSize = true;
            this.ChkStockAdjustment.Location = new System.Drawing.Point(15, 27);
            this.ChkStockAdjustment.Name = "ChkStockAdjustment";
            this.ChkStockAdjustment.Size = new System.Drawing.Size(161, 23);
            this.ChkStockAdjustment.TabIndex = 5;
            this.ChkStockAdjustment.Text = "Stock Adjustment";
            this.ChkStockAdjustment.UseVisualStyleBackColor = true;
            // 
            // roundPanel3
            // 
            this.roundPanel3.BackColor = System.Drawing.Color.Transparent;
            this.roundPanel3.Controls.Add(this.ChkPurchaseAdditional);
            this.roundPanel3.Controls.Add(this.ChkSalesAdditional);
            this.roundPanel3.Controls.Add(this.ChkSalesReturn);
            this.roundPanel3.Controls.Add(this.ChkCashBank);
            this.roundPanel3.Controls.Add(this.ChkPurchaseReturn);
            this.roundPanel3.Controls.Add(this.ChkPurchaseInvoice);
            this.roundPanel3.Controls.Add(this.ChkPurchaseChallan);
            this.roundPanel3.Controls.Add(this.ChkPurchaseOrder);
            this.roundPanel3.Controls.Add(this.ChkSalesOrder);
            this.roundPanel3.Controls.Add(this.ChkSalesInvoice);
            this.roundPanel3.Controls.Add(this.ChkProductOpening);
            this.roundPanel3.Controls.Add(this.ChkLedgerOpening);
            this.roundPanel3.Controls.Add(this.ChkPDCVoucher);
            this.roundPanel3.Controls.Add(this.ChkPurchaseIndent);
            this.roundPanel3.Controls.Add(this.ChkSalesQuotation);
            this.roundPanel3.Controls.Add(this.ChkSalesChallan);
            this.roundPanel3.Controls.Add(this.ChkNotesRegister);
            this.roundPanel3.Controls.Add(this.ChkJournalVoucher);
            this.roundPanel3.Location = new System.Drawing.Point(8, 105);
            this.roundPanel3.Name = "roundPanel3";
            this.roundPanel3.Radious = 25;
            this.roundPanel3.Size = new System.Drawing.Size(494, 175);
            this.roundPanel3.TabIndex = 27;
            this.roundPanel3.TabStop = false;
            this.roundPanel3.Text = "Sync Transaction";
            this.roundPanel3.TitleBackColor = System.Drawing.Color.DarkSlateBlue;
            this.roundPanel3.TitleFont = new System.Drawing.Font("Bookman Old Style", 11F);
            this.roundPanel3.TitleForeColor = System.Drawing.Color.White;
            this.roundPanel3.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
            // 
            // ChkPurchaseAdditional
            // 
            this.ChkPurchaseAdditional.AutoSize = true;
            this.ChkPurchaseAdditional.Location = new System.Drawing.Point(317, 151);
            this.ChkPurchaseAdditional.Name = "ChkPurchaseAdditional";
            this.ChkPurchaseAdditional.Size = new System.Drawing.Size(179, 23);
            this.ChkPurchaseAdditional.TabIndex = 4;
            this.ChkPurchaseAdditional.Text = "Purchase Additional";
            this.ChkPurchaseAdditional.UseVisualStyleBackColor = true;
            // 
            // ChkSalesAdditional
            // 
            this.ChkSalesAdditional.AutoSize = true;
            this.ChkSalesAdditional.Location = new System.Drawing.Point(164, 151);
            this.ChkSalesAdditional.Name = "ChkSalesAdditional";
            this.ChkSalesAdditional.Size = new System.Drawing.Size(150, 23);
            this.ChkSalesAdditional.TabIndex = 3;
            this.ChkSalesAdditional.Text = "Sales Additional";
            this.ChkSalesAdditional.UseVisualStyleBackColor = true;
            // 
            // ChkSalesReturn
            // 
            this.ChkSalesReturn.AutoSize = true;
            this.ChkSalesReturn.Location = new System.Drawing.Point(164, 126);
            this.ChkSalesReturn.Name = "ChkSalesReturn";
            this.ChkSalesReturn.Size = new System.Drawing.Size(127, 23);
            this.ChkSalesReturn.TabIndex = 2;
            this.ChkSalesReturn.Text = "Sales Return";
            this.ChkSalesReturn.UseVisualStyleBackColor = true;
            // 
            // ChkCashBank
            // 
            this.ChkCashBank.AutoSize = true;
            this.ChkCashBank.Location = new System.Drawing.Point(12, 26);
            this.ChkCashBank.Name = "ChkCashBank";
            this.ChkCashBank.Size = new System.Drawing.Size(110, 23);
            this.ChkCashBank.TabIndex = 1;
            this.ChkCashBank.Text = "Cash Bank";
            this.ChkCashBank.UseVisualStyleBackColor = true;
            // 
            // ChkPurchaseReturn
            // 
            this.ChkPurchaseReturn.AutoSize = true;
            this.ChkPurchaseReturn.Location = new System.Drawing.Point(317, 126);
            this.ChkPurchaseReturn.Name = "ChkPurchaseReturn";
            this.ChkPurchaseReturn.Size = new System.Drawing.Size(156, 23);
            this.ChkPurchaseReturn.TabIndex = 0;
            this.ChkPurchaseReturn.Text = "Purchase Return";
            this.ChkPurchaseReturn.UseVisualStyleBackColor = true;
            // 
            // ChkPurchaseInvoice
            // 
            this.ChkPurchaseInvoice.AutoSize = true;
            this.ChkPurchaseInvoice.Location = new System.Drawing.Point(317, 101);
            this.ChkPurchaseInvoice.Name = "ChkPurchaseInvoice";
            this.ChkPurchaseInvoice.Size = new System.Drawing.Size(155, 23);
            this.ChkPurchaseInvoice.TabIndex = 0;
            this.ChkPurchaseInvoice.Text = "Purchase Invoice";
            this.ChkPurchaseInvoice.UseVisualStyleBackColor = true;
            // 
            // ChkPurchaseChallan
            // 
            this.ChkPurchaseChallan.AutoSize = true;
            this.ChkPurchaseChallan.Location = new System.Drawing.Point(317, 76);
            this.ChkPurchaseChallan.Name = "ChkPurchaseChallan";
            this.ChkPurchaseChallan.Size = new System.Drawing.Size(162, 23);
            this.ChkPurchaseChallan.TabIndex = 0;
            this.ChkPurchaseChallan.Text = "Purchase Challan";
            this.ChkPurchaseChallan.UseVisualStyleBackColor = true;
            // 
            // ChkPurchaseOrder
            // 
            this.ChkPurchaseOrder.AutoSize = true;
            this.ChkPurchaseOrder.Location = new System.Drawing.Point(317, 51);
            this.ChkPurchaseOrder.Name = "ChkPurchaseOrder";
            this.ChkPurchaseOrder.Size = new System.Drawing.Size(147, 23);
            this.ChkPurchaseOrder.TabIndex = 0;
            this.ChkPurchaseOrder.Text = "Purchase Order";
            this.ChkPurchaseOrder.UseVisualStyleBackColor = true;
            // 
            // ChkSalesOrder
            // 
            this.ChkSalesOrder.AutoSize = true;
            this.ChkSalesOrder.Location = new System.Drawing.Point(164, 51);
            this.ChkSalesOrder.Name = "ChkSalesOrder";
            this.ChkSalesOrder.Size = new System.Drawing.Size(118, 23);
            this.ChkSalesOrder.TabIndex = 0;
            this.ChkSalesOrder.Text = "Sales Order";
            this.ChkSalesOrder.UseVisualStyleBackColor = true;
            // 
            // ChkSalesInvoice
            // 
            this.ChkSalesInvoice.AutoSize = true;
            this.ChkSalesInvoice.Location = new System.Drawing.Point(164, 101);
            this.ChkSalesInvoice.Name = "ChkSalesInvoice";
            this.ChkSalesInvoice.Size = new System.Drawing.Size(126, 23);
            this.ChkSalesInvoice.TabIndex = 0;
            this.ChkSalesInvoice.Text = "Sales Invoice";
            this.ChkSalesInvoice.UseVisualStyleBackColor = true;
            // 
            // ChkProductOpening
            // 
            this.ChkProductOpening.AutoSize = true;
            this.ChkProductOpening.Location = new System.Drawing.Point(12, 151);
            this.ChkProductOpening.Name = "ChkProductOpening";
            this.ChkProductOpening.Size = new System.Drawing.Size(152, 23);
            this.ChkProductOpening.TabIndex = 0;
            this.ChkProductOpening.Text = "Product Opening";
            this.ChkProductOpening.UseVisualStyleBackColor = true;
            // 
            // ChkLedgerOpening
            // 
            this.ChkLedgerOpening.AutoSize = true;
            this.ChkLedgerOpening.Location = new System.Drawing.Point(12, 126);
            this.ChkLedgerOpening.Name = "ChkLedgerOpening";
            this.ChkLedgerOpening.Size = new System.Drawing.Size(146, 23);
            this.ChkLedgerOpening.TabIndex = 0;
            this.ChkLedgerOpening.Text = "Ledger Opening";
            this.ChkLedgerOpening.UseVisualStyleBackColor = true;
            // 
            // ChkPDCVoucher
            // 
            this.ChkPDCVoucher.AutoSize = true;
            this.ChkPDCVoucher.Location = new System.Drawing.Point(12, 101);
            this.ChkPDCVoucher.Name = "ChkPDCVoucher";
            this.ChkPDCVoucher.Size = new System.Drawing.Size(128, 23);
            this.ChkPDCVoucher.TabIndex = 0;
            this.ChkPDCVoucher.Text = "PDC Voucher";
            this.ChkPDCVoucher.UseVisualStyleBackColor = true;
            // 
            // ChkPurchaseIndent
            // 
            this.ChkPurchaseIndent.AutoSize = true;
            this.ChkPurchaseIndent.Location = new System.Drawing.Point(317, 26);
            this.ChkPurchaseIndent.Name = "ChkPurchaseIndent";
            this.ChkPurchaseIndent.Size = new System.Drawing.Size(152, 23);
            this.ChkPurchaseIndent.TabIndex = 0;
            this.ChkPurchaseIndent.Text = "Purchase Indent";
            this.ChkPurchaseIndent.UseVisualStyleBackColor = true;
            // 
            // ChkSalesQuotation
            // 
            this.ChkSalesQuotation.AutoSize = true;
            this.ChkSalesQuotation.Location = new System.Drawing.Point(164, 26);
            this.ChkSalesQuotation.Name = "ChkSalesQuotation";
            this.ChkSalesQuotation.Size = new System.Drawing.Size(148, 23);
            this.ChkSalesQuotation.TabIndex = 0;
            this.ChkSalesQuotation.Text = "Sales Quotation";
            this.ChkSalesQuotation.UseVisualStyleBackColor = true;
            // 
            // ChkSalesChallan
            // 
            this.ChkSalesChallan.AutoSize = true;
            this.ChkSalesChallan.Location = new System.Drawing.Point(164, 76);
            this.ChkSalesChallan.Name = "ChkSalesChallan";
            this.ChkSalesChallan.Size = new System.Drawing.Size(133, 23);
            this.ChkSalesChallan.TabIndex = 0;
            this.ChkSalesChallan.Text = "Sales Challan";
            this.ChkSalesChallan.UseVisualStyleBackColor = true;
            // 
            // ChkNotesRegister
            // 
            this.ChkNotesRegister.AutoSize = true;
            this.ChkNotesRegister.Location = new System.Drawing.Point(12, 76);
            this.ChkNotesRegister.Name = "ChkNotesRegister";
            this.ChkNotesRegister.Size = new System.Drawing.Size(138, 23);
            this.ChkNotesRegister.TabIndex = 0;
            this.ChkNotesRegister.Text = "Notes Register";
            this.ChkNotesRegister.UseVisualStyleBackColor = true;
            // 
            // ChkJournalVoucher
            // 
            this.ChkJournalVoucher.AutoSize = true;
            this.ChkJournalVoucher.Location = new System.Drawing.Point(12, 51);
            this.ChkJournalVoucher.Name = "ChkJournalVoucher";
            this.ChkJournalVoucher.Size = new System.Drawing.Size(154, 23);
            this.ChkJournalVoucher.TabIndex = 0;
            this.ChkJournalVoucher.Text = "Journal Voucher";
            this.ChkJournalVoucher.UseVisualStyleBackColor = true;
            // 
            // FrmOnlineConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 461);
            this.Controls.Add(this.StorePanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "FrmOnlineConfig";
            this.ShowIcon = false;
            this.Text = "Online Config";
            this.Load += new System.EventHandler(this.FrmOnlineConfig_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmOnlineConfig_KeyPress);
            this.roundPanel1.ResumeLayout(false);
            this.roundPanel1.PerformLayout();
            this.roundPanel2.ResumeLayout(false);
            this.roundPanel2.PerformLayout();
            this.StorePanel.ResumeLayout(false);
            this.roundPanel4.ResumeLayout(false);
            this.roundPanel4.PerformLayout();
            this.roundPanel3.ResumeLayout(false);
            this.roundPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

		#endregion
		private RoundPanel roundPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private RoundPanel roundPanel2;
		private DevExpress.XtraEditors.SimpleButton BtnCancel;
		private DevExpress.XtraEditors.SimpleButton BtnSave;
		private System.Windows.Forms.CheckBox ChkBillingTerm;
		private System.Windows.Forms.CheckBox ChkAgent;
		private System.Windows.Forms.CheckBox ChkGeneralLedger;
		private System.Windows.Forms.CheckBox ChkCostCenter;
		private System.Windows.Forms.CheckBox ChkMember;
		private System.Windows.Forms.CheckBox ChkArea;
		private System.Windows.Forms.CheckBox ChkBranch;
		private System.Windows.Forms.CheckBox ChkTableId;
		private System.Windows.Forms.CheckBox ChkProduct;
		private RoundPanel roundPanel3;
		private System.Windows.Forms.CheckBox ChkPurchaseAdditional;
		private System.Windows.Forms.CheckBox ChkSalesAdditional;
		private System.Windows.Forms.CheckBox ChkSalesReturn;
		private System.Windows.Forms.CheckBox ChkCashBank;
		private System.Windows.Forms.CheckBox ChkPurchaseReturn;
		private System.Windows.Forms.CheckBox ChkPurchaseInvoice;
		private System.Windows.Forms.CheckBox ChkPurchaseChallan;
		private System.Windows.Forms.CheckBox ChkPurchaseOrder;
		private System.Windows.Forms.CheckBox ChkSalesOrder;
		private System.Windows.Forms.CheckBox ChkSalesInvoice;
		private System.Windows.Forms.CheckBox ChkProductOpening;
		private System.Windows.Forms.CheckBox ChkLedgerOpening;
		private System.Windows.Forms.CheckBox ChkPDCVoucher;
		private System.Windows.Forms.CheckBox ChkPurchaseIndent;
		private System.Windows.Forms.CheckBox ChkSalesQuotation;
		private System.Windows.Forms.CheckBox ChkSalesChallan;
		private System.Windows.Forms.CheckBox ChkNotesRegister;
		private System.Windows.Forms.CheckBox ChkJournalVoucher;
		private RoundPanel roundPanel4;
		private System.Windows.Forms.CheckBox ChkStockAdjustment;
        private MrPanel StorePanel;
        private MrTextBox TxtApiUrl;
        private MrTextBox TxtOrginId;
    }
}