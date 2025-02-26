using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.SystemSetting
{
    partial class FrmFinanceSetting
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
            this.Btn_Save = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSaveAndClose = new DevExpress.XtraEditors.SimpleButton();
            this.ChkDepartmentMandatory = new System.Windows.Forms.CheckBox();
            this.ChkRemarksMandatory = new System.Windows.Forms.CheckBox();
            this.ChkAgentMandatory = new System.Windows.Forms.CheckBox();
            this.ChkSubledgerMandatory = new System.Windows.Forms.CheckBox();
            this.ChkCurrencyMandatory = new System.Windows.Forms.CheckBox();
            this.ChkEnableVoucherDate = new System.Windows.Forms.CheckBox();
            this.ChkNarrationMandatory = new System.Windows.Forms.CheckBox();
            this.ChkDepartmentItemEnable = new System.Windows.Forms.CheckBox();
            this.ChkDepartmentEnable = new System.Windows.Forms.CheckBox();
            this.ChkRemarksEnable = new System.Windows.Forms.CheckBox();
            this.ChkAgentEnable = new System.Windows.Forms.CheckBox();
            this.ChkSubledgerEnable = new System.Windows.Forms.CheckBox();
            this.ChkCurrencyEnable = new System.Windows.Forms.CheckBox();
            this.BtnProvisionBank = new System.Windows.Forms.Button();
            this.lbl_ProfitandLoss = new System.Windows.Forms.Label();
            this.ChkWarnNegativeTransaction = new System.Windows.Forms.CheckBox();
            this.BtnVatLedger = new System.Windows.Forms.Button();
            this.lbl_CashBook = new System.Windows.Forms.Label();
            this.BtnCashLedger = new System.Windows.Forms.Button();
            this.TxtCashBook = new MrTextBox();
            this.BtnProfitLoss = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.TxtPDCBankAccount = new MrTextBox();
            this.TxtVATAccount = new MrTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.TxtProfitLoss = new MrTextBox();
            this.NegativeBalance = new System.Windows.Forms.GroupBox();
            this.CmbNegativeTransaction = new System.Windows.Forms.ComboBox();
            this.LedgerTag = new System.Windows.Forms.GroupBox();
            this.EnableControl = new System.Windows.Forms.GroupBox();
            this.FinanceTag = new System.Windows.Forms.GroupBox();
            this.ChkEnableShortNameWise = new System.Windows.Forms.CheckBox();
            this.MandatoryControl = new System.Windows.Forms.GroupBox();
            this.ChkDepartmentItemMandatory = new System.Windows.Forms.CheckBox();
            this.panel1 = new MrPanel();
            this.clsSeparator1 = new ClsSeparator();
            this.NegativeBalance.SuspendLayout();
            this.LedgerTag.SuspendLayout();
            this.EnableControl.SuspendLayout();
            this.FinanceTag.SuspendLayout();
            this.MandatoryControl.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_Save
            // 
            this.Btn_Save.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Save.Appearance.Options.UseFont = true;
            this.Btn_Save.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.Btn_Save.Location = new System.Drawing.Point(432, 333);
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new System.Drawing.Size(107, 36);
            this.Btn_Save.TabIndex = 6;
            this.Btn_Save.Text = "&SAVE";
            this.Btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // BtnSaveAndClose
            // 
            this.BtnSaveAndClose.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSaveAndClose.Appearance.Options.UseFont = true;
            this.BtnSaveAndClose.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSaveAndClose.Location = new System.Drawing.Point(254, 333);
            this.BtnSaveAndClose.Name = "BtnSaveAndClose";
            this.BtnSaveAndClose.Size = new System.Drawing.Size(175, 36);
            this.BtnSaveAndClose.TabIndex = 5;
            this.BtnSaveAndClose.Text = "SAVE && C&LOSE";
            this.BtnSaveAndClose.Click += new System.EventHandler(this.BtnSaveAndClose_Click);
            // 
            // ChkDepartmentMandatory
            // 
            this.ChkDepartmentMandatory.Enabled = false;
            this.ChkDepartmentMandatory.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDepartmentMandatory.Location = new System.Drawing.Point(6, 43);
            this.ChkDepartmentMandatory.Name = "ChkDepartmentMandatory";
            this.ChkDepartmentMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDepartmentMandatory.Size = new System.Drawing.Size(181, 22);
            this.ChkDepartmentMandatory.TabIndex = 1;
            this.ChkDepartmentMandatory.Text = "Department";
            this.ChkDepartmentMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDepartmentMandatory.UseVisualStyleBackColor = true;
            // 
            // ChkRemarksMandatory
            // 
            this.ChkRemarksMandatory.Enabled = false;
            this.ChkRemarksMandatory.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkRemarksMandatory.Location = new System.Drawing.Point(6, 65);
            this.ChkRemarksMandatory.Name = "ChkRemarksMandatory";
            this.ChkRemarksMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkRemarksMandatory.Size = new System.Drawing.Size(181, 22);
            this.ChkRemarksMandatory.TabIndex = 2;
            this.ChkRemarksMandatory.Text = "Remarks";
            this.ChkRemarksMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkRemarksMandatory.UseVisualStyleBackColor = true;
            // 
            // ChkAgentMandatory
            // 
            this.ChkAgentMandatory.Enabled = false;
            this.ChkAgentMandatory.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAgentMandatory.Location = new System.Drawing.Point(6, 21);
            this.ChkAgentMandatory.Name = "ChkAgentMandatory";
            this.ChkAgentMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkAgentMandatory.Size = new System.Drawing.Size(181, 22);
            this.ChkAgentMandatory.TabIndex = 0;
            this.ChkAgentMandatory.Text = "Agent";
            this.ChkAgentMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkAgentMandatory.UseVisualStyleBackColor = true;
            // 
            // ChkSubledgerMandatory
            // 
            this.ChkSubledgerMandatory.Enabled = false;
            this.ChkSubledgerMandatory.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSubledgerMandatory.Location = new System.Drawing.Point(6, 109);
            this.ChkSubledgerMandatory.Name = "ChkSubledgerMandatory";
            this.ChkSubledgerMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSubledgerMandatory.Size = new System.Drawing.Size(181, 22);
            this.ChkSubledgerMandatory.TabIndex = 4;
            this.ChkSubledgerMandatory.Text = "Sub Ledger";
            this.ChkSubledgerMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSubledgerMandatory.UseVisualStyleBackColor = true;
            // 
            // ChkCurrencyMandatory
            // 
            this.ChkCurrencyMandatory.Enabled = false;
            this.ChkCurrencyMandatory.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkCurrencyMandatory.Location = new System.Drawing.Point(6, 87);
            this.ChkCurrencyMandatory.Name = "ChkCurrencyMandatory";
            this.ChkCurrencyMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkCurrencyMandatory.Size = new System.Drawing.Size(181, 22);
            this.ChkCurrencyMandatory.TabIndex = 3;
            this.ChkCurrencyMandatory.Text = "Currency";
            this.ChkCurrencyMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkCurrencyMandatory.UseVisualStyleBackColor = true;
            // 
            // ChkEnableVoucherDate
            // 
            this.ChkEnableVoucherDate.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkEnableVoucherDate.Location = new System.Drawing.Point(6, 41);
            this.ChkEnableVoucherDate.Name = "ChkEnableVoucherDate";
            this.ChkEnableVoucherDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkEnableVoucherDate.Size = new System.Drawing.Size(209, 22);
            this.ChkEnableVoucherDate.TabIndex = 1;
            this.ChkEnableVoucherDate.Text = "Voucher Date";
            this.ChkEnableVoucherDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkEnableVoucherDate.UseVisualStyleBackColor = true;
            // 
            // ChkNarrationMandatory
            // 
            this.ChkNarrationMandatory.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkNarrationMandatory.Location = new System.Drawing.Point(6, 153);
            this.ChkNarrationMandatory.Name = "ChkNarrationMandatory";
            this.ChkNarrationMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkNarrationMandatory.Size = new System.Drawing.Size(181, 22);
            this.ChkNarrationMandatory.TabIndex = 6;
            this.ChkNarrationMandatory.Text = "Narration";
            this.ChkNarrationMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkNarrationMandatory.UseVisualStyleBackColor = true;
            // 
            // ChkDepartmentItemEnable
            // 
            this.ChkDepartmentItemEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDepartmentItemEnable.Location = new System.Drawing.Point(6, 136);
            this.ChkDepartmentItemEnable.Name = "ChkDepartmentItemEnable";
            this.ChkDepartmentItemEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDepartmentItemEnable.Size = new System.Drawing.Size(148, 23);
            this.ChkDepartmentItemEnable.TabIndex = 5;
            this.ChkDepartmentItemEnable.Text = "Department Item";
            this.ChkDepartmentItemEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDepartmentItemEnable.UseVisualStyleBackColor = true;
            this.ChkDepartmentItemEnable.CheckedChanged += new System.EventHandler(this.ChkEnableDepartmentItem_CheckedChanged);
            // 
            // ChkDepartmentEnable
            // 
            this.ChkDepartmentEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDepartmentEnable.Location = new System.Drawing.Point(6, 44);
            this.ChkDepartmentEnable.Name = "ChkDepartmentEnable";
            this.ChkDepartmentEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDepartmentEnable.Size = new System.Drawing.Size(148, 23);
            this.ChkDepartmentEnable.TabIndex = 1;
            this.ChkDepartmentEnable.Text = "Department";
            this.ChkDepartmentEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDepartmentEnable.UseVisualStyleBackColor = true;
            this.ChkDepartmentEnable.CheckedChanged += new System.EventHandler(this.ChkEnableDepartment_CheckedChanged);
            // 
            // ChkRemarksEnable
            // 
            this.ChkRemarksEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkRemarksEnable.Location = new System.Drawing.Point(6, 67);
            this.ChkRemarksEnable.Name = "ChkRemarksEnable";
            this.ChkRemarksEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkRemarksEnable.Size = new System.Drawing.Size(148, 23);
            this.ChkRemarksEnable.TabIndex = 2;
            this.ChkRemarksEnable.Text = "Remarks";
            this.ChkRemarksEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkRemarksEnable.UseVisualStyleBackColor = true;
            this.ChkRemarksEnable.CheckedChanged += new System.EventHandler(this.ChkEnableRemarks_CheckedChanged);
            // 
            // ChkAgentEnable
            // 
            this.ChkAgentEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAgentEnable.Location = new System.Drawing.Point(6, 21);
            this.ChkAgentEnable.Name = "ChkAgentEnable";
            this.ChkAgentEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkAgentEnable.Size = new System.Drawing.Size(148, 23);
            this.ChkAgentEnable.TabIndex = 0;
            this.ChkAgentEnable.Text = "Agent";
            this.ChkAgentEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkAgentEnable.UseVisualStyleBackColor = true;
            this.ChkAgentEnable.CheckedChanged += new System.EventHandler(this.ChkEnableAgent_CheckedChanged);
            // 
            // ChkSubledgerEnable
            // 
            this.ChkSubledgerEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSubledgerEnable.Location = new System.Drawing.Point(6, 113);
            this.ChkSubledgerEnable.Name = "ChkSubledgerEnable";
            this.ChkSubledgerEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSubledgerEnable.Size = new System.Drawing.Size(148, 23);
            this.ChkSubledgerEnable.TabIndex = 4;
            this.ChkSubledgerEnable.Text = "Sub Ledger";
            this.ChkSubledgerEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSubledgerEnable.UseVisualStyleBackColor = true;
            this.ChkSubledgerEnable.CheckedChanged += new System.EventHandler(this.ChkEnableSubledger_CheckedChanged);
            // 
            // ChkCurrencyEnable
            // 
            this.ChkCurrencyEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkCurrencyEnable.Location = new System.Drawing.Point(6, 90);
            this.ChkCurrencyEnable.Name = "ChkCurrencyEnable";
            this.ChkCurrencyEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkCurrencyEnable.Size = new System.Drawing.Size(148, 23);
            this.ChkCurrencyEnable.TabIndex = 3;
            this.ChkCurrencyEnable.Text = "Currency";
            this.ChkCurrencyEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkCurrencyEnable.UseVisualStyleBackColor = true;
            this.ChkCurrencyEnable.CheckedChanged += new System.EventHandler(this.ChkEnableCurrency_CheckedChanged);
            // 
            // BtnProvisionBank
            // 
            this.BtnProvisionBank.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnProvisionBank.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnProvisionBank.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnProvisionBank.Location = new System.Drawing.Point(338, 98);
            this.BtnProvisionBank.Name = "BtnProvisionBank";
            this.BtnProvisionBank.Size = new System.Drawing.Size(28, 23);
            this.BtnProvisionBank.TabIndex = 172;
            this.BtnProvisionBank.TabStop = false;
            this.BtnProvisionBank.UseVisualStyleBackColor = true;
            this.BtnProvisionBank.Click += new System.EventHandler(this.BtnProvisionBank_Click);
            // 
            // lbl_ProfitandLoss
            // 
            this.lbl_ProfitandLoss.AutoSize = true;
            this.lbl_ProfitandLoss.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_ProfitandLoss.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_ProfitandLoss.Location = new System.Drawing.Point(4, 20);
            this.lbl_ProfitandLoss.Name = "lbl_ProfitandLoss";
            this.lbl_ProfitandLoss.Size = new System.Drawing.Size(114, 19);
            this.lbl_ProfitandLoss.TabIndex = 165;
            this.lbl_ProfitandLoss.Text = "Profit  && Loss ";
            // 
            // ChkWarnNegativeTransaction
            // 
            this.ChkWarnNegativeTransaction.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkWarnNegativeTransaction.Location = new System.Drawing.Point(6, 63);
            this.ChkWarnNegativeTransaction.Name = "ChkWarnNegativeTransaction";
            this.ChkWarnNegativeTransaction.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkWarnNegativeTransaction.Size = new System.Drawing.Size(209, 22);
            this.ChkWarnNegativeTransaction.TabIndex = 2;
            this.ChkWarnNegativeTransaction.Text = "Warn Negative Transaction";
            this.ChkWarnNegativeTransaction.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkWarnNegativeTransaction.UseVisualStyleBackColor = true;
            // 
            // BtnVatLedger
            // 
            this.BtnVatLedger.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnVatLedger.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnVatLedger.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnVatLedger.Location = new System.Drawing.Point(338, 71);
            this.BtnVatLedger.Name = "BtnVatLedger";
            this.BtnVatLedger.Size = new System.Drawing.Size(28, 23);
            this.BtnVatLedger.TabIndex = 171;
            this.BtnVatLedger.TabStop = false;
            this.BtnVatLedger.UseVisualStyleBackColor = true;
            this.BtnVatLedger.Click += new System.EventHandler(this.BtnVatLedger_Click);
            // 
            // lbl_CashBook
            // 
            this.lbl_CashBook.AutoSize = true;
            this.lbl_CashBook.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_CashBook.Location = new System.Drawing.Point(4, 47);
            this.lbl_CashBook.Name = "lbl_CashBook";
            this.lbl_CashBook.Size = new System.Drawing.Size(112, 19);
            this.lbl_CashBook.TabIndex = 166;
            this.lbl_CashBook.Text = "Cash Account";
            // 
            // BtnCashLedger
            // 
            this.BtnCashLedger.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnCashLedger.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnCashLedger.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnCashLedger.Location = new System.Drawing.Point(338, 45);
            this.BtnCashLedger.Name = "BtnCashLedger";
            this.BtnCashLedger.Size = new System.Drawing.Size(28, 23);
            this.BtnCashLedger.TabIndex = 170;
            this.BtnCashLedger.TabStop = false;
            this.BtnCashLedger.UseVisualStyleBackColor = true;
            this.BtnCashLedger.Click += new System.EventHandler(this.BtnCashLedger_Click);
            // 
            // TxtCashBook
            // 
            this.TxtCashBook.BackColor = System.Drawing.Color.White;
            this.TxtCashBook.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCashBook.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtCashBook.Location = new System.Drawing.Point(119, 44);
            this.TxtCashBook.MaxLength = 255;
            this.TxtCashBook.Name = "TxtCashBook";
            this.TxtCashBook.ReadOnly = true;
            this.TxtCashBook.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtCashBook.Size = new System.Drawing.Size(215, 25);
            this.TxtCashBook.TabIndex = 1;
            this.TxtCashBook.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtCashBook.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCAshBookLedger_KeyDown);
            this.TxtCashBook.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtCashBook.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // BtnProfitLoss
            // 
            this.BtnProfitLoss.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnProfitLoss.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnProfitLoss.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnProfitLoss.Location = new System.Drawing.Point(338, 16);
            this.BtnProfitLoss.Name = "BtnProfitLoss";
            this.BtnProfitLoss.Size = new System.Drawing.Size(28, 23);
            this.BtnProfitLoss.TabIndex = 169;
            this.BtnProfitLoss.TabStop = false;
            this.BtnProfitLoss.UseVisualStyleBackColor = true;
            this.BtnProfitLoss.Click += new System.EventHandler(this.BtnProfitLoss_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label9.Location = new System.Drawing.Point(4, 73);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 19);
            this.label9.TabIndex = 167;
            this.label9.Text = "Vat Account";
            // 
            // TxtPDCBankAccount
            // 
            this.TxtPDCBankAccount.BackColor = System.Drawing.Color.White;
            this.TxtPDCBankAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPDCBankAccount.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtPDCBankAccount.Location = new System.Drawing.Point(119, 97);
            this.TxtPDCBankAccount.MaxLength = 255;
            this.TxtPDCBankAccount.Name = "TxtPDCBankAccount";
            this.TxtPDCBankAccount.ReadOnly = true;
            this.TxtPDCBankAccount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtPDCBankAccount.Size = new System.Drawing.Size(215, 25);
            this.TxtPDCBankAccount.TabIndex = 3;
            this.TxtPDCBankAccount.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtPDCBankAccount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPDCBankAccount_KeyDown);
            this.TxtPDCBankAccount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtPDCBankAccount.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // TxtVATAccount
            // 
            this.TxtVATAccount.BackColor = System.Drawing.Color.White;
            this.TxtVATAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtVATAccount.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtVATAccount.Location = new System.Drawing.Point(119, 70);
            this.TxtVATAccount.MaxLength = 255;
            this.TxtVATAccount.Name = "TxtVATAccount";
            this.TxtVATAccount.ReadOnly = true;
            this.TxtVATAccount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtVATAccount.Size = new System.Drawing.Size(215, 25);
            this.TxtVATAccount.TabIndex = 2;
            this.TxtVATAccount.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtVATAccount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtVatLedger_KeyDown);
            this.TxtVATAccount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtVATAccount.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label10.Location = new System.Drawing.Point(4, 100);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 19);
            this.label10.TabIndex = 168;
            this.label10.Text = "PDC Bank";
            // 
            // TxtProfitLoss
            // 
            this.TxtProfitLoss.BackColor = System.Drawing.Color.White;
            this.TxtProfitLoss.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtProfitLoss.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtProfitLoss.Location = new System.Drawing.Point(119, 17);
            this.TxtProfitLoss.MaxLength = 255;
            this.TxtProfitLoss.Name = "TxtProfitLoss";
            this.TxtProfitLoss.ReadOnly = true;
            this.TxtProfitLoss.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtProfitLoss.Size = new System.Drawing.Size(215, 25);
            this.TxtProfitLoss.TabIndex = 0;
            this.TxtProfitLoss.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtProfitLoss.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtProfitLoss_KeyDown);
            this.TxtProfitLoss.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtProfitLoss.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // NegativeBalance
            // 
            this.NegativeBalance.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.NegativeBalance.Controls.Add(this.CmbNegativeTransaction);
            this.NegativeBalance.Location = new System.Drawing.Point(379, 83);
            this.NegativeBalance.Name = "NegativeBalance";
            this.NegativeBalance.Size = new System.Drawing.Size(222, 56);
            this.NegativeBalance.TabIndex = 2;
            this.NegativeBalance.TabStop = false;
            this.NegativeBalance.Text = "Negative Transasction";
            // 
            // CmbNegativeTransaction
            // 
            this.CmbNegativeTransaction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbNegativeTransaction.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbNegativeTransaction.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbNegativeTransaction.FormattingEnabled = true;
            this.CmbNegativeTransaction.Location = new System.Drawing.Point(6, 23);
            this.CmbNegativeTransaction.Name = "CmbNegativeTransaction";
            this.CmbNegativeTransaction.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CmbNegativeTransaction.Size = new System.Drawing.Size(209, 26);
            this.CmbNegativeTransaction.TabIndex = 0;
            // 
            // LedgerTag
            // 
            this.LedgerTag.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.LedgerTag.Controls.Add(this.BtnProvisionBank);
            this.LedgerTag.Controls.Add(this.TxtProfitLoss);
            this.LedgerTag.Controls.Add(this.label9);
            this.LedgerTag.Controls.Add(this.lbl_ProfitandLoss);
            this.LedgerTag.Controls.Add(this.TxtPDCBankAccount);
            this.LedgerTag.Controls.Add(this.lbl_CashBook);
            this.LedgerTag.Controls.Add(this.TxtVATAccount);
            this.LedgerTag.Controls.Add(this.label10);
            this.LedgerTag.Controls.Add(this.BtnCashLedger);
            this.LedgerTag.Controls.Add(this.BtnVatLedger);
            this.LedgerTag.Controls.Add(this.TxtCashBook);
            this.LedgerTag.Controls.Add(this.BtnProfitLoss);
            this.LedgerTag.Location = new System.Drawing.Point(2, -2);
            this.LedgerTag.Name = "LedgerTag";
            this.LedgerTag.Size = new System.Drawing.Size(377, 141);
            this.LedgerTag.TabIndex = 0;
            this.LedgerTag.TabStop = false;
            this.LedgerTag.Text = "Ledger Tag";
            // 
            // EnableControl
            // 
            this.EnableControl.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.EnableControl.Controls.Add(this.ChkAgentEnable);
            this.EnableControl.Controls.Add(this.ChkDepartmentItemEnable);
            this.EnableControl.Controls.Add(this.ChkDepartmentEnable);
            this.EnableControl.Controls.Add(this.ChkSubledgerEnable);
            this.EnableControl.Controls.Add(this.ChkRemarksEnable);
            this.EnableControl.Controls.Add(this.ChkCurrencyEnable);
            this.EnableControl.Location = new System.Drawing.Point(2, 139);
            this.EnableControl.Name = "EnableControl";
            this.EnableControl.Size = new System.Drawing.Size(199, 178);
            this.EnableControl.TabIndex = 3;
            this.EnableControl.TabStop = false;
            this.EnableControl.Text = "Mandatory Enable";
            // 
            // FinanceTag
            // 
            this.FinanceTag.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.FinanceTag.Controls.Add(this.ChkEnableVoucherDate);
            this.FinanceTag.Controls.Add(this.ChkWarnNegativeTransaction);
            this.FinanceTag.Controls.Add(this.ChkEnableShortNameWise);
            this.FinanceTag.Location = new System.Drawing.Point(379, -2);
            this.FinanceTag.Name = "FinanceTag";
            this.FinanceTag.Size = new System.Drawing.Size(222, 86);
            this.FinanceTag.TabIndex = 1;
            this.FinanceTag.TabStop = false;
            this.FinanceTag.Text = "Finance Tag";
            // 
            // ChkEnableShortNameWise
            // 
            this.ChkEnableShortNameWise.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkEnableShortNameWise.Location = new System.Drawing.Point(6, 19);
            this.ChkEnableShortNameWise.Name = "ChkEnableShortNameWise";
            this.ChkEnableShortNameWise.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkEnableShortNameWise.Size = new System.Drawing.Size(209, 22);
            this.ChkEnableShortNameWise.TabIndex = 0;
            this.ChkEnableShortNameWise.Text = "Short Name Wise";
            this.ChkEnableShortNameWise.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkEnableShortNameWise.UseVisualStyleBackColor = true;
            // 
            // MandatoryControl
            // 
            this.MandatoryControl.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.MandatoryControl.Controls.Add(this.ChkDepartmentItemMandatory);
            this.MandatoryControl.Controls.Add(this.ChkSubledgerMandatory);
            this.MandatoryControl.Controls.Add(this.ChkNarrationMandatory);
            this.MandatoryControl.Controls.Add(this.ChkCurrencyMandatory);
            this.MandatoryControl.Controls.Add(this.ChkRemarksMandatory);
            this.MandatoryControl.Controls.Add(this.ChkDepartmentMandatory);
            this.MandatoryControl.Controls.Add(this.ChkAgentMandatory);
            this.MandatoryControl.Location = new System.Drawing.Point(201, 139);
            this.MandatoryControl.Name = "MandatoryControl";
            this.MandatoryControl.Size = new System.Drawing.Size(405, 178);
            this.MandatoryControl.TabIndex = 4;
            this.MandatoryControl.TabStop = false;
            this.MandatoryControl.Text = "Mandatory Tag";
            // 
            // ChkDepartmentItemMandatory
            // 
            this.ChkDepartmentItemMandatory.Enabled = false;
            this.ChkDepartmentItemMandatory.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDepartmentItemMandatory.Location = new System.Drawing.Point(6, 131);
            this.ChkDepartmentItemMandatory.Name = "ChkDepartmentItemMandatory";
            this.ChkDepartmentItemMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDepartmentItemMandatory.Size = new System.Drawing.Size(181, 22);
            this.ChkDepartmentItemMandatory.TabIndex = 5;
            this.ChkDepartmentItemMandatory.Text = "Department Item";
            this.ChkDepartmentItemMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDepartmentItemMandatory.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.BtnSaveAndClose);
            this.panel1.Controls.Add(this.Btn_Save);
            this.panel1.Controls.Add(this.clsSeparator1);
            this.panel1.Controls.Add(this.LedgerTag);
            this.panel1.Controls.Add(this.MandatoryControl);
            this.panel1.Controls.Add(this.EnableControl);
            this.panel1.Controls.Add(this.NegativeBalance);
            this.panel1.Controls.Add(this.FinanceTag);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(606, 372);
            this.panel1.TabIndex = 0;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator1.Location = new System.Drawing.Point(14, 329);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(590, 2);
            this.clsSeparator1.TabIndex = 177;
            this.clsSeparator1.TabStop = false;
            // 
            // FrmFinanceSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(606, 372);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFinanceSetting";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FINANCE SETTING";
            this.Load += new System.EventHandler(this.FrmFinanceSetting_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmFinanceSetting_KeyPress);
            this.NegativeBalance.ResumeLayout(false);
            this.LedgerTag.ResumeLayout(false);
            this.LedgerTag.PerformLayout();
            this.EnableControl.ResumeLayout(false);
            this.FinanceTag.ResumeLayout(false);
            this.MandatoryControl.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button BtnProvisionBank;
        private System.Windows.Forms.Label lbl_ProfitandLoss;
        private System.Windows.Forms.CheckBox ChkWarnNegativeTransaction;
        private System.Windows.Forms.Button BtnVatLedger;
        private System.Windows.Forms.Label lbl_CashBook;
        private System.Windows.Forms.Button BtnCashLedger;
        private System.Windows.Forms.Button BtnProfitLoss;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox ChkEnableVoucherDate;
        private System.Windows.Forms.CheckBox ChkNarrationMandatory;
        private System.Windows.Forms.CheckBox ChkDepartmentItemEnable;
        private System.Windows.Forms.CheckBox ChkDepartmentEnable;
        private System.Windows.Forms.CheckBox ChkRemarksEnable;
        private System.Windows.Forms.CheckBox ChkAgentEnable;
        private System.Windows.Forms.CheckBox ChkSubledgerEnable;
        private System.Windows.Forms.CheckBox ChkCurrencyEnable;
        private System.Windows.Forms.CheckBox ChkDepartmentMandatory;
        private System.Windows.Forms.CheckBox ChkRemarksMandatory;
        private System.Windows.Forms.CheckBox ChkAgentMandatory;
        private System.Windows.Forms.CheckBox ChkSubledgerMandatory;
        private System.Windows.Forms.CheckBox ChkCurrencyMandatory;
        private DevExpress.XtraEditors.SimpleButton Btn_Save;
        private DevExpress.XtraEditors.SimpleButton BtnSaveAndClose;
        private System.Windows.Forms.GroupBox NegativeBalance;
        private System.Windows.Forms.GroupBox LedgerTag;
        private System.Windows.Forms.GroupBox EnableControl;
        private System.Windows.Forms.GroupBox FinanceTag;
        private System.Windows.Forms.GroupBox MandatoryControl;
        private System.Windows.Forms.CheckBox ChkEnableShortNameWise;
        private System.Windows.Forms.CheckBox ChkDepartmentItemMandatory;
        private System.Windows.Forms.ComboBox CmbNegativeTransaction;
        private ClsSeparator clsSeparator1;
        private MrTextBox TxtCashBook;
        private MrTextBox TxtPDCBankAccount;
        private MrTextBox TxtVATAccount;
        private MrTextBox TxtProfitLoss;
        private MrPanel panel1;
    }
}