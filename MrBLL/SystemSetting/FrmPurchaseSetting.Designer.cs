using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.SystemSetting
{
    partial class FrmPurchaseSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPurchaseSetting));
            this.CmbCreditBalance = new System.Windows.Forms.ComboBox();
            this.label44 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.CmbCreditDays = new System.Windows.Forms.ComboBox();
            this.BtnPurchaseDiscount = new System.Windows.Forms.Button();
            this.BtnPurchaseProDiscount = new System.Windows.Forms.Button();
            this.BtnPurchaseAdditionalVAT = new System.Windows.Forms.Button();
            this.TxtDiscount = new MrTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.TxtPurchaseAddVat = new MrTextBox();
            this.TxtProDiscount = new MrTextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.lbl_PurchaseAc = new System.Windows.Forms.Label();
            this.lbl_PurchaseReturnAc = new System.Windows.Forms.Label();
            this.BtnPurchaseVAT = new System.Windows.Forms.Button();
            this.BtnPurchaseReturn = new System.Windows.Forms.Button();
            this.label29 = new System.Windows.Forms.Label();
            this.BtnPurchaseInvoice = new System.Windows.Forms.Button();
            this.TxtPurchaseVat = new MrTextBox();
            this.TxtPurchaseAc = new MrTextBox();
            this.TxtPurchaseReturnAc = new MrTextBox();
            this.ChkLastRate = new System.Windows.Forms.CheckBox();
            this.ChkCarryRate = new System.Windows.Forms.CheckBox();
            this.ChkAgentEnable = new System.Windows.Forms.CheckBox();
            this.ChkIndentEnable = new System.Windows.Forms.CheckBox();
            this.ChkAltUnitEnable = new System.Windows.Forms.CheckBox();
            this.ChkSubLedgerEnable = new System.Windows.Forms.CheckBox();
            this.ChkBasicAmountEnable = new System.Windows.Forms.CheckBox();
            this.ChkCurrencyEnable = new System.Windows.Forms.CheckBox();
            this.ChkChallanEnable = new System.Windows.Forms.CheckBox();
            this.ChkGodownEnable = new System.Windows.Forms.CheckBox();
            this.ChkDepartmentEnable = new System.Windows.Forms.CheckBox();
            this.ChkOrderEnable = new System.Windows.Forms.CheckBox();
            this.ChkDepartmentMandatory = new System.Windows.Forms.CheckBox();
            this.ChkGodownMandatory = new System.Windows.Forms.CheckBox();
            this.ChkCurrencyMandatory = new System.Windows.Forms.CheckBox();
            this.ChkSubLedgerMandatory = new System.Windows.Forms.CheckBox();
            this.ChkChallanMandatory = new System.Windows.Forms.CheckBox();
            this.ChkOrderMandatory = new System.Windows.Forms.CheckBox();
            this.ChkAgentMandatory = new System.Windows.Forms.CheckBox();
            this.Btn_Save = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.ChkRemarksEnable = new System.Windows.Forms.CheckBox();
            this.ChkChangeCurrencyRate = new System.Windows.Forms.CheckBox();
            this.ChkNarrationEnable = new System.Windows.Forms.CheckBox();
            this.ChkChangeRate = new System.Windows.Forms.CheckBox();
            this.ChkDateChangeEnable = new System.Windows.Forms.CheckBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.ChkRemarksMandatory = new System.Windows.Forms.CheckBox();
            this.panel1 = new MrPanel();
            this.clsSeparator1 = new ClsSeparator();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CmbCreditBalance
            // 
            this.CmbCreditBalance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbCreditBalance.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbCreditBalance.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbCreditBalance.FormattingEnabled = true;
            this.CmbCreditBalance.Location = new System.Drawing.Point(6, 85);
            this.CmbCreditBalance.Name = "CmbCreditBalance";
            this.CmbCreditBalance.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CmbCreditBalance.Size = new System.Drawing.Size(189, 26);
            this.CmbCreditBalance.TabIndex = 189;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(6, 66);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(113, 19);
            this.label44.TabIndex = 190;
            this.label44.Text = "Credit Balance";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(6, 21);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(94, 19);
            this.label43.TabIndex = 191;
            this.label43.Text = "Credit Days ";
            // 
            // CmbCreditDays
            // 
            this.CmbCreditDays.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbCreditDays.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbCreditDays.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbCreditDays.FormattingEnabled = true;
            this.CmbCreditDays.Location = new System.Drawing.Point(6, 40);
            this.CmbCreditDays.Name = "CmbCreditDays";
            this.CmbCreditDays.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CmbCreditDays.Size = new System.Drawing.Size(189, 26);
            this.CmbCreditDays.TabIndex = 188;
            // 
            // BtnPurchaseDiscount
            // 
            this.BtnPurchaseDiscount.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPurchaseDiscount.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnPurchaseDiscount.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnPurchaseDiscount.Location = new System.Drawing.Point(312, 101);
            this.BtnPurchaseDiscount.Name = "BtnPurchaseDiscount";
            this.BtnPurchaseDiscount.Size = new System.Drawing.Size(32, 25);
            this.BtnPurchaseDiscount.TabIndex = 187;
            this.BtnPurchaseDiscount.TabStop = false;
            this.BtnPurchaseDiscount.UseVisualStyleBackColor = true;
            this.BtnPurchaseDiscount.Click += new System.EventHandler(this.BtnPurchaseDiscount_Click);
            // 
            // BtnPurchaseProDiscount
            // 
            this.BtnPurchaseProDiscount.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPurchaseProDiscount.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnPurchaseProDiscount.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnPurchaseProDiscount.Location = new System.Drawing.Point(312, 74);
            this.BtnPurchaseProDiscount.Name = "BtnPurchaseProDiscount";
            this.BtnPurchaseProDiscount.Size = new System.Drawing.Size(32, 25);
            this.BtnPurchaseProDiscount.TabIndex = 186;
            this.BtnPurchaseProDiscount.TabStop = false;
            this.BtnPurchaseProDiscount.UseVisualStyleBackColor = true;
            this.BtnPurchaseProDiscount.Click += new System.EventHandler(this.BtnPurchaseProDiscount_Click);
            // 
            // BtnPurchaseAdditionalVAT
            // 
            this.BtnPurchaseAdditionalVAT.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPurchaseAdditionalVAT.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnPurchaseAdditionalVAT.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnPurchaseAdditionalVAT.Location = new System.Drawing.Point(312, 47);
            this.BtnPurchaseAdditionalVAT.Name = "BtnPurchaseAdditionalVAT";
            this.BtnPurchaseAdditionalVAT.Size = new System.Drawing.Size(32, 25);
            this.BtnPurchaseAdditionalVAT.TabIndex = 185;
            this.BtnPurchaseAdditionalVAT.TabStop = false;
            this.BtnPurchaseAdditionalVAT.UseVisualStyleBackColor = true;
            this.BtnPurchaseAdditionalVAT.Click += new System.EventHandler(this.BtnPurchaseAdditionalVAT_Click);
            // 
            // TxtDiscount
            // 
            this.TxtDiscount.BackColor = System.Drawing.Color.White;
            this.TxtDiscount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDiscount.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDiscount.Location = new System.Drawing.Point(109, 101);
            this.TxtDiscount.MaxLength = 255;
            this.TxtDiscount.Name = "TxtDiscount";
            this.TxtDiscount.ReadOnly = true;
            this.TxtDiscount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtDiscount.Size = new System.Drawing.Size(202, 23);
            this.TxtDiscount.TabIndex = 181;
            this.TxtDiscount.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtDiscount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDiscount_KeyDown);
            this.TxtDiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtDiscount.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(7, 49);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 19);
            this.label11.TabIndex = 182;
            this.label11.Text = "Add VAT";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(7, 103);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(95, 19);
            this.label31.TabIndex = 184;
            this.label31.Text = "Discount (B)";
            // 
            // TxtPurchaseAddVat
            // 
            this.TxtPurchaseAddVat.BackColor = System.Drawing.Color.White;
            this.TxtPurchaseAddVat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPurchaseAddVat.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPurchaseAddVat.Location = new System.Drawing.Point(109, 47);
            this.TxtPurchaseAddVat.MaxLength = 255;
            this.TxtPurchaseAddVat.Name = "TxtPurchaseAddVat";
            this.TxtPurchaseAddVat.ReadOnly = true;
            this.TxtPurchaseAddVat.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtPurchaseAddVat.Size = new System.Drawing.Size(202, 23);
            this.TxtPurchaseAddVat.TabIndex = 179;
            this.TxtPurchaseAddVat.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtPurchaseAddVat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPurchaseAddVat_KeyDown);
            this.TxtPurchaseAddVat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtPurchaseAddVat.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // TxtProDiscount
            // 
            this.TxtProDiscount.BackColor = System.Drawing.Color.White;
            this.TxtProDiscount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtProDiscount.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtProDiscount.Location = new System.Drawing.Point(109, 74);
            this.TxtProDiscount.MaxLength = 255;
            this.TxtProDiscount.Name = "TxtProDiscount";
            this.TxtProDiscount.ReadOnly = true;
            this.TxtProDiscount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtProDiscount.Size = new System.Drawing.Size(202, 23);
            this.TxtProDiscount.TabIndex = 180;
            this.TxtProDiscount.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtProDiscount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtProDiscount_KeyDown);
            this.TxtProDiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtProDiscount.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(7, 76);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(94, 19);
            this.label30.TabIndex = 183;
            this.label30.Text = "Discount (P)";
            // 
            // lbl_PurchaseAc
            // 
            this.lbl_PurchaseAc.AutoSize = true;
            this.lbl_PurchaseAc.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_PurchaseAc.Location = new System.Drawing.Point(7, 19);
            this.lbl_PurchaseAc.Name = "lbl_PurchaseAc";
            this.lbl_PurchaseAc.Size = new System.Drawing.Size(90, 19);
            this.lbl_PurchaseAc.TabIndex = 167;
            this.lbl_PurchaseAc.Text = "Invoice A/c";
            // 
            // lbl_PurchaseReturnAc
            // 
            this.lbl_PurchaseReturnAc.AutoSize = true;
            this.lbl_PurchaseReturnAc.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_PurchaseReturnAc.Location = new System.Drawing.Point(7, 46);
            this.lbl_PurchaseReturnAc.Name = "lbl_PurchaseReturnAc";
            this.lbl_PurchaseReturnAc.Size = new System.Drawing.Size(88, 19);
            this.lbl_PurchaseReturnAc.TabIndex = 168;
            this.lbl_PurchaseReturnAc.Text = "Return A/c";
            // 
            // BtnPurchaseVAT
            // 
            this.BtnPurchaseVAT.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPurchaseVAT.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnPurchaseVAT.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnPurchaseVAT.Location = new System.Drawing.Point(312, 19);
            this.BtnPurchaseVAT.Name = "BtnPurchaseVAT";
            this.BtnPurchaseVAT.Size = new System.Drawing.Size(32, 25);
            this.BtnPurchaseVAT.TabIndex = 175;
            this.BtnPurchaseVAT.TabStop = false;
            this.BtnPurchaseVAT.UseVisualStyleBackColor = true;
            this.BtnPurchaseVAT.Click += new System.EventHandler(this.BtnPurchaseVAT_Click);
            // 
            // BtnPurchaseReturn
            // 
            this.BtnPurchaseReturn.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPurchaseReturn.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnPurchaseReturn.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnPurchaseReturn.Location = new System.Drawing.Point(491, 43);
            this.BtnPurchaseReturn.Name = "BtnPurchaseReturn";
            this.BtnPurchaseReturn.Size = new System.Drawing.Size(29, 25);
            this.BtnPurchaseReturn.TabIndex = 174;
            this.BtnPurchaseReturn.TabStop = false;
            this.BtnPurchaseReturn.UseVisualStyleBackColor = true;
            this.BtnPurchaseReturn.Click += new System.EventHandler(this.BtnPurchaseReturn_Click);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(7, 22);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(38, 19);
            this.label29.TabIndex = 169;
            this.label29.Text = "VAT";
            // 
            // BtnPurchaseInvoice
            // 
            this.BtnPurchaseInvoice.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPurchaseInvoice.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnPurchaseInvoice.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnPurchaseInvoice.Location = new System.Drawing.Point(491, 16);
            this.BtnPurchaseInvoice.Name = "BtnPurchaseInvoice";
            this.BtnPurchaseInvoice.Size = new System.Drawing.Size(29, 25);
            this.BtnPurchaseInvoice.TabIndex = 173;
            this.BtnPurchaseInvoice.TabStop = false;
            this.BtnPurchaseInvoice.UseVisualStyleBackColor = true;
            this.BtnPurchaseInvoice.Click += new System.EventHandler(this.BtnPurchaseInvoice_Click);
            // 
            // TxtPurchaseVat
            // 
            this.TxtPurchaseVat.BackColor = System.Drawing.Color.White;
            this.TxtPurchaseVat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPurchaseVat.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPurchaseVat.Location = new System.Drawing.Point(109, 20);
            this.TxtPurchaseVat.MaxLength = 255;
            this.TxtPurchaseVat.Name = "TxtPurchaseVat";
            this.TxtPurchaseVat.ReadOnly = true;
            this.TxtPurchaseVat.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtPurchaseVat.Size = new System.Drawing.Size(202, 23);
            this.TxtPurchaseVat.TabIndex = 163;
            this.TxtPurchaseVat.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtPurchaseVat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPurchaseVat_KeyDown);
            this.TxtPurchaseVat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtPurchaseVat.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // TxtPurchaseAc
            // 
            this.TxtPurchaseAc.BackColor = System.Drawing.Color.White;
            this.TxtPurchaseAc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPurchaseAc.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPurchaseAc.Location = new System.Drawing.Point(109, 17);
            this.TxtPurchaseAc.MaxLength = 255;
            this.TxtPurchaseAc.Name = "TxtPurchaseAc";
            this.TxtPurchaseAc.ReadOnly = true;
            this.TxtPurchaseAc.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtPurchaseAc.Size = new System.Drawing.Size(377, 23);
            this.TxtPurchaseAc.TabIndex = 161;
            this.TxtPurchaseAc.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtPurchaseAc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPurchaseAc_KeyDown);
            this.TxtPurchaseAc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtPurchaseAc.Leave += new System.EventHandler(this.TxtPurchaseAc_Leave);
            // 
            // TxtPurchaseReturnAc
            // 
            this.TxtPurchaseReturnAc.BackColor = System.Drawing.Color.White;
            this.TxtPurchaseReturnAc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPurchaseReturnAc.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPurchaseReturnAc.Location = new System.Drawing.Point(109, 44);
            this.TxtPurchaseReturnAc.MaxLength = 255;
            this.TxtPurchaseReturnAc.Name = "TxtPurchaseReturnAc";
            this.TxtPurchaseReturnAc.ReadOnly = true;
            this.TxtPurchaseReturnAc.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtPurchaseReturnAc.Size = new System.Drawing.Size(377, 23);
            this.TxtPurchaseReturnAc.TabIndex = 162;
            this.TxtPurchaseReturnAc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPurchaseReturnAc_KeyDown);
            this.TxtPurchaseReturnAc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtPurchaseReturnAc.Leave += new System.EventHandler(this.TxtPurchaseReturnAc_Leave);
            // 
            // ChkLastRate
            // 
            this.ChkLastRate.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkLastRate.Location = new System.Drawing.Point(6, 95);
            this.ChkLastRate.Name = "ChkLastRate";
            this.ChkLastRate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkLastRate.Size = new System.Drawing.Size(132, 24);
            this.ChkLastRate.TabIndex = 17;
            this.ChkLastRate.Text = "Last Rate";
            this.ChkLastRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkLastRate.UseVisualStyleBackColor = true;
            // 
            // ChkCarryRate
            // 
            this.ChkCarryRate.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkCarryRate.Location = new System.Drawing.Point(6, 47);
            this.ChkCarryRate.Name = "ChkCarryRate";
            this.ChkCarryRate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkCarryRate.Size = new System.Drawing.Size(132, 24);
            this.ChkCarryRate.TabIndex = 16;
            this.ChkCarryRate.Text = "Carry Rate";
            this.ChkCarryRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkCarryRate.UseVisualStyleBackColor = true;
            // 
            // ChkAgentEnable
            // 
            this.ChkAgentEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAgentEnable.Location = new System.Drawing.Point(145, 95);
            this.ChkAgentEnable.Name = "ChkAgentEnable";
            this.ChkAgentEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkAgentEnable.Size = new System.Drawing.Size(132, 24);
            this.ChkAgentEnable.TabIndex = 23;
            this.ChkAgentEnable.Text = "Agent";
            this.ChkAgentEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkAgentEnable.UseVisualStyleBackColor = true;
            this.ChkAgentEnable.CheckStateChanged += new System.EventHandler(this.Chk_PAgent_CheckStateChanged);
            // 
            // ChkIndentEnable
            // 
            this.ChkIndentEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIndentEnable.Location = new System.Drawing.Point(6, 143);
            this.ChkIndentEnable.Name = "ChkIndentEnable";
            this.ChkIndentEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIndentEnable.Size = new System.Drawing.Size(132, 24);
            this.ChkIndentEnable.TabIndex = 22;
            this.ChkIndentEnable.Text = "Indent";
            this.ChkIndentEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIndentEnable.UseVisualStyleBackColor = true;
            // 
            // ChkAltUnitEnable
            // 
            this.ChkAltUnitEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAltUnitEnable.Location = new System.Drawing.Point(6, 119);
            this.ChkAltUnitEnable.Name = "ChkAltUnitEnable";
            this.ChkAltUnitEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkAltUnitEnable.Size = new System.Drawing.Size(132, 24);
            this.ChkAltUnitEnable.TabIndex = 20;
            this.ChkAltUnitEnable.Text = "Alt Unit";
            this.ChkAltUnitEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkAltUnitEnable.UseVisualStyleBackColor = true;
            // 
            // ChkSubLedgerEnable
            // 
            this.ChkSubLedgerEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSubLedgerEnable.Location = new System.Drawing.Point(145, 71);
            this.ChkSubLedgerEnable.Name = "ChkSubLedgerEnable";
            this.ChkSubLedgerEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSubLedgerEnable.Size = new System.Drawing.Size(132, 24);
            this.ChkSubLedgerEnable.TabIndex = 19;
            this.ChkSubLedgerEnable.Text = "Sub Ledger";
            this.ChkSubLedgerEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSubLedgerEnable.UseVisualStyleBackColor = true;
            this.ChkSubLedgerEnable.CheckStateChanged += new System.EventHandler(this.Chk_PSubLedger_CheckStateChanged);
            // 
            // ChkBasicAmountEnable
            // 
            this.ChkBasicAmountEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkBasicAmountEnable.Location = new System.Drawing.Point(6, 190);
            this.ChkBasicAmountEnable.Name = "ChkBasicAmountEnable";
            this.ChkBasicAmountEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkBasicAmountEnable.Size = new System.Drawing.Size(132, 24);
            this.ChkBasicAmountEnable.TabIndex = 14;
            this.ChkBasicAmountEnable.Text = "Basic Amount";
            this.ChkBasicAmountEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkBasicAmountEnable.UseVisualStyleBackColor = true;
            // 
            // ChkCurrencyEnable
            // 
            this.ChkCurrencyEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkCurrencyEnable.Location = new System.Drawing.Point(145, 143);
            this.ChkCurrencyEnable.Name = "ChkCurrencyEnable";
            this.ChkCurrencyEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkCurrencyEnable.Size = new System.Drawing.Size(132, 24);
            this.ChkCurrencyEnable.TabIndex = 13;
            this.ChkCurrencyEnable.Text = "Currency";
            this.ChkCurrencyEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkCurrencyEnable.UseVisualStyleBackColor = true;
            this.ChkCurrencyEnable.CheckStateChanged += new System.EventHandler(this.ChkCurrencyEnable_CheckStateChanged);
            // 
            // ChkChallanEnable
            // 
            this.ChkChallanEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkChallanEnable.Location = new System.Drawing.Point(145, 47);
            this.ChkChallanEnable.Name = "ChkChallanEnable";
            this.ChkChallanEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkChallanEnable.Size = new System.Drawing.Size(132, 24);
            this.ChkChallanEnable.TabIndex = 12;
            this.ChkChallanEnable.Text = "Challan";
            this.ChkChallanEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkChallanEnable.UseVisualStyleBackColor = true;
            this.ChkChallanEnable.CheckStateChanged += new System.EventHandler(this.ChkChallanEnable_CheckStateChanged);
            // 
            // ChkGodownEnable
            // 
            this.ChkGodownEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkGodownEnable.Location = new System.Drawing.Point(145, 167);
            this.ChkGodownEnable.Name = "ChkGodownEnable";
            this.ChkGodownEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkGodownEnable.Size = new System.Drawing.Size(132, 24);
            this.ChkGodownEnable.TabIndex = 10;
            this.ChkGodownEnable.Text = "Godown";
            this.ChkGodownEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkGodownEnable.UseVisualStyleBackColor = true;
            this.ChkGodownEnable.CheckStateChanged += new System.EventHandler(this.ChkGodownEnable_CheckStateChanged);
            // 
            // ChkDepartmentEnable
            // 
            this.ChkDepartmentEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDepartmentEnable.Location = new System.Drawing.Point(145, 119);
            this.ChkDepartmentEnable.Name = "ChkDepartmentEnable";
            this.ChkDepartmentEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDepartmentEnable.Size = new System.Drawing.Size(132, 24);
            this.ChkDepartmentEnable.TabIndex = 9;
            this.ChkDepartmentEnable.Text = "Department";
            this.ChkDepartmentEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDepartmentEnable.UseVisualStyleBackColor = true;
            this.ChkDepartmentEnable.CheckStateChanged += new System.EventHandler(this.ChkDepartmentEnable_CheckStateChanged);
            // 
            // ChkOrderEnable
            // 
            this.ChkOrderEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkOrderEnable.Location = new System.Drawing.Point(145, 23);
            this.ChkOrderEnable.Name = "ChkOrderEnable";
            this.ChkOrderEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkOrderEnable.Size = new System.Drawing.Size(132, 24);
            this.ChkOrderEnable.TabIndex = 8;
            this.ChkOrderEnable.Text = "Order";
            this.ChkOrderEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkOrderEnable.UseVisualStyleBackColor = true;
            this.ChkOrderEnable.CheckStateChanged += new System.EventHandler(this.Chk_POrder_CheckStateChanged);
            // 
            // ChkDepartmentMandatory
            // 
            this.ChkDepartmentMandatory.Enabled = false;
            this.ChkDepartmentMandatory.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDepartmentMandatory.Location = new System.Drawing.Point(8, 121);
            this.ChkDepartmentMandatory.Name = "ChkDepartmentMandatory";
            this.ChkDepartmentMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDepartmentMandatory.Size = new System.Drawing.Size(132, 24);
            this.ChkDepartmentMandatory.TabIndex = 8;
            this.ChkDepartmentMandatory.Text = "Department";
            this.ChkDepartmentMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDepartmentMandatory.UseVisualStyleBackColor = true;
            // 
            // ChkGodownMandatory
            // 
            this.ChkGodownMandatory.Enabled = false;
            this.ChkGodownMandatory.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkGodownMandatory.Location = new System.Drawing.Point(8, 169);
            this.ChkGodownMandatory.Name = "ChkGodownMandatory";
            this.ChkGodownMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkGodownMandatory.Size = new System.Drawing.Size(132, 24);
            this.ChkGodownMandatory.TabIndex = 9;
            this.ChkGodownMandatory.Text = "Godown";
            this.ChkGodownMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkGodownMandatory.UseVisualStyleBackColor = true;
            // 
            // ChkCurrencyMandatory
            // 
            this.ChkCurrencyMandatory.Enabled = false;
            this.ChkCurrencyMandatory.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkCurrencyMandatory.Location = new System.Drawing.Point(8, 145);
            this.ChkCurrencyMandatory.Name = "ChkCurrencyMandatory";
            this.ChkCurrencyMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkCurrencyMandatory.Size = new System.Drawing.Size(132, 24);
            this.ChkCurrencyMandatory.TabIndex = 10;
            this.ChkCurrencyMandatory.Text = "Currency";
            this.ChkCurrencyMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkCurrencyMandatory.UseVisualStyleBackColor = true;
            // 
            // ChkSubLedgerMandatory
            // 
            this.ChkSubLedgerMandatory.Enabled = false;
            this.ChkSubLedgerMandatory.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSubLedgerMandatory.Location = new System.Drawing.Point(8, 73);
            this.ChkSubLedgerMandatory.Name = "ChkSubLedgerMandatory";
            this.ChkSubLedgerMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSubLedgerMandatory.Size = new System.Drawing.Size(132, 24);
            this.ChkSubLedgerMandatory.TabIndex = 5;
            this.ChkSubLedgerMandatory.Text = "Sub Ledger";
            this.ChkSubLedgerMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSubLedgerMandatory.UseVisualStyleBackColor = true;
            // 
            // ChkChallanMandatory
            // 
            this.ChkChallanMandatory.Enabled = false;
            this.ChkChallanMandatory.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkChallanMandatory.Location = new System.Drawing.Point(8, 49);
            this.ChkChallanMandatory.Name = "ChkChallanMandatory";
            this.ChkChallanMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkChallanMandatory.Size = new System.Drawing.Size(132, 24);
            this.ChkChallanMandatory.TabIndex = 7;
            this.ChkChallanMandatory.Text = "Challan";
            this.ChkChallanMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkChallanMandatory.UseVisualStyleBackColor = true;
            // 
            // ChkOrderMandatory
            // 
            this.ChkOrderMandatory.Enabled = false;
            this.ChkOrderMandatory.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkOrderMandatory.Location = new System.Drawing.Point(8, 25);
            this.ChkOrderMandatory.Name = "ChkOrderMandatory";
            this.ChkOrderMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkOrderMandatory.Size = new System.Drawing.Size(132, 24);
            this.ChkOrderMandatory.TabIndex = 4;
            this.ChkOrderMandatory.Text = "Order";
            this.ChkOrderMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkOrderMandatory.UseVisualStyleBackColor = true;
            // 
            // ChkAgentMandatory
            // 
            this.ChkAgentMandatory.Enabled = false;
            this.ChkAgentMandatory.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAgentMandatory.Location = new System.Drawing.Point(8, 97);
            this.ChkAgentMandatory.Name = "ChkAgentMandatory";
            this.ChkAgentMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkAgentMandatory.Size = new System.Drawing.Size(132, 24);
            this.ChkAgentMandatory.TabIndex = 6;
            this.ChkAgentMandatory.Text = "Agent";
            this.ChkAgentMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkAgentMandatory.UseVisualStyleBackColor = true;
            // 
            // Btn_Save
            // 
            this.Btn_Save.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Save.Appearance.Options.UseFont = true;
            this.Btn_Save.ImageOptions.Image = global::MrBLL.Properties.Resources.search16;
            this.Btn_Save.Location = new System.Drawing.Point(457, 452);
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new System.Drawing.Size(98, 36);
            this.Btn_Save.TabIndex = 178;
            this.Btn_Save.Text = "&SAVE";
            this.Btn_Save.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnSave.Location = new System.Drawing.Point(272, 452);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(182, 36);
            this.BtnSave.TabIndex = 177;
            this.BtnSave.Text = "SAVE && C&LOSE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSaveClosed_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox5.Controls.Add(this.label43);
            this.groupBox5.Controls.Add(this.CmbCreditBalance);
            this.groupBox5.Controls.Add(this.CmbCreditDays);
            this.groupBox5.Controls.Add(this.label44);
            this.groupBox5.Location = new System.Drawing.Point(354, 74);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(201, 126);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Credit Tag";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox3.Controls.Add(this.TxtPurchaseVat);
            this.groupBox3.Controls.Add(this.label29);
            this.groupBox3.Controls.Add(this.BtnPurchaseVAT);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label30);
            this.groupBox3.Controls.Add(this.BtnPurchaseDiscount);
            this.groupBox3.Controls.Add(this.TxtProDiscount);
            this.groupBox3.Controls.Add(this.BtnPurchaseProDiscount);
            this.groupBox3.Controls.Add(this.TxtPurchaseAddVat);
            this.groupBox3.Controls.Add(this.BtnPurchaseAdditionalVAT);
            this.groupBox3.Controls.Add(this.label31);
            this.groupBox3.Controls.Add(this.TxtDiscount);
            this.groupBox3.Location = new System.Drawing.Point(4, 74);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(350, 129);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Term Tag";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox2.Controls.Add(this.TxtPurchaseAc);
            this.groupBox2.Controls.Add(this.BtnPurchaseInvoice);
            this.groupBox2.Controls.Add(this.TxtPurchaseReturnAc);
            this.groupBox2.Controls.Add(this.BtnPurchaseReturn);
            this.groupBox2.Controls.Add(this.lbl_PurchaseReturnAc);
            this.groupBox2.Controls.Add(this.lbl_PurchaseAc);
            this.groupBox2.Location = new System.Drawing.Point(4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(551, 71);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ledger Tag";
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox6.Controls.Add(this.ChkRemarksEnable);
            this.groupBox6.Controls.Add(this.ChkOrderEnable);
            this.groupBox6.Controls.Add(this.ChkChangeCurrencyRate);
            this.groupBox6.Controls.Add(this.ChkChallanEnable);
            this.groupBox6.Controls.Add(this.ChkNarrationEnable);
            this.groupBox6.Controls.Add(this.ChkSubLedgerEnable);
            this.groupBox6.Controls.Add(this.ChkChangeRate);
            this.groupBox6.Controls.Add(this.ChkAgentEnable);
            this.groupBox6.Controls.Add(this.ChkDateChangeEnable);
            this.groupBox6.Controls.Add(this.ChkDepartmentEnable);
            this.groupBox6.Controls.Add(this.ChkAltUnitEnable);
            this.groupBox6.Controls.Add(this.ChkCurrencyEnable);
            this.groupBox6.Controls.Add(this.ChkCarryRate);
            this.groupBox6.Controls.Add(this.ChkGodownEnable);
            this.groupBox6.Controls.Add(this.ChkLastRate);
            this.groupBox6.Controls.Add(this.ChkIndentEnable);
            this.groupBox6.Controls.Add(this.ChkBasicAmountEnable);
            this.groupBox6.Location = new System.Drawing.Point(5, 203);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(399, 243);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Purchase Tag";
            // 
            // ChkRemarksEnable
            // 
            this.ChkRemarksEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkRemarksEnable.Location = new System.Drawing.Point(6, 213);
            this.ChkRemarksEnable.Name = "ChkRemarksEnable";
            this.ChkRemarksEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkRemarksEnable.Size = new System.Drawing.Size(132, 24);
            this.ChkRemarksEnable.TabIndex = 24;
            this.ChkRemarksEnable.Text = "Remarks";
            this.ChkRemarksEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkRemarksEnable.UseVisualStyleBackColor = true;
            this.ChkRemarksEnable.CheckStateChanged += new System.EventHandler(this.ChkRemarksEnable_CheckStateChanged);
            // 
            // ChkChangeCurrencyRate
            // 
            this.ChkChangeCurrencyRate.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkChangeCurrencyRate.Location = new System.Drawing.Point(145, 190);
            this.ChkChangeCurrencyRate.Name = "ChkChangeCurrencyRate";
            this.ChkChangeCurrencyRate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkChangeCurrencyRate.Size = new System.Drawing.Size(132, 24);
            this.ChkChangeCurrencyRate.TabIndex = 18;
            this.ChkChangeCurrencyRate.Text = "Currency Rate";
            this.ChkChangeCurrencyRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkChangeCurrencyRate.UseVisualStyleBackColor = true;
            // 
            // ChkNarrationEnable
            // 
            this.ChkNarrationEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkNarrationEnable.Location = new System.Drawing.Point(6, 164);
            this.ChkNarrationEnable.Name = "ChkNarrationEnable";
            this.ChkNarrationEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkNarrationEnable.Size = new System.Drawing.Size(132, 24);
            this.ChkNarrationEnable.TabIndex = 23;
            this.ChkNarrationEnable.Text = "Narration";
            this.ChkNarrationEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkNarrationEnable.UseVisualStyleBackColor = true;
            // 
            // ChkChangeRate
            // 
            this.ChkChangeRate.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkChangeRate.Location = new System.Drawing.Point(6, 71);
            this.ChkChangeRate.Name = "ChkChangeRate";
            this.ChkChangeRate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkChangeRate.Size = new System.Drawing.Size(132, 24);
            this.ChkChangeRate.TabIndex = 17;
            this.ChkChangeRate.Text = "Change Rate";
            this.ChkChangeRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkChangeRate.UseVisualStyleBackColor = true;
            // 
            // ChkDateChangeEnable
            // 
            this.ChkDateChangeEnable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDateChangeEnable.Location = new System.Drawing.Point(6, 23);
            this.ChkDateChangeEnable.Name = "ChkDateChangeEnable";
            this.ChkDateChangeEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDateChangeEnable.Size = new System.Drawing.Size(132, 24);
            this.ChkDateChangeEnable.TabIndex = 12;
            this.ChkDateChangeEnable.Text = "Date Change";
            this.ChkDateChangeEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDateChangeEnable.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            this.groupBox9.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox9.Controls.Add(this.ChkRemarksMandatory);
            this.groupBox9.Controls.Add(this.ChkDepartmentMandatory);
            this.groupBox9.Controls.Add(this.ChkGodownMandatory);
            this.groupBox9.Controls.Add(this.ChkOrderMandatory);
            this.groupBox9.Controls.Add(this.ChkChallanMandatory);
            this.groupBox9.Controls.Add(this.ChkSubLedgerMandatory);
            this.groupBox9.Controls.Add(this.ChkAgentMandatory);
            this.groupBox9.Controls.Add(this.ChkCurrencyMandatory);
            this.groupBox9.Location = new System.Drawing.Point(404, 203);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(153, 243);
            this.groupBox9.TabIndex = 0;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Mandatory Tag";
            // 
            // ChkRemarksMandatory
            // 
            this.ChkRemarksMandatory.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkRemarksMandatory.Location = new System.Drawing.Point(8, 193);
            this.ChkRemarksMandatory.Name = "ChkRemarksMandatory";
            this.ChkRemarksMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkRemarksMandatory.Size = new System.Drawing.Size(132, 24);
            this.ChkRemarksMandatory.TabIndex = 25;
            this.ChkRemarksMandatory.Text = "Remarks";
            this.ChkRemarksMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkRemarksMandatory.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.clsSeparator1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.Btn_Save);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.BtnSave);
            this.panel1.Controls.Add(this.groupBox6);
            this.panel1.Controls.Add(this.groupBox9);
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(561, 491);
            this.panel1.TabIndex = 179;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.Wheat;
            this.clsSeparator1.Location = new System.Drawing.Point(10, 447);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(545, 2);
            this.clsSeparator1.TabIndex = 179;
            this.clsSeparator1.TabStop = false;
            // 
            // FrmPurchaseSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(561, 491);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPurchaseSetting";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PURCHASE SETTING";
            this.Load += new System.EventHandler(this.FrmPurchaseSetting_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmPurchaseSetting_KeyPress);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl_PurchaseAc;
        private System.Windows.Forms.Label lbl_PurchaseReturnAc;
        private System.Windows.Forms.Button BtnPurchaseVAT;
        private System.Windows.Forms.Button BtnPurchaseReturn;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Button BtnPurchaseInvoice;
        private System.Windows.Forms.TextBox TxtPurchaseVat;
        private System.Windows.Forms.TextBox TxtPurchaseAc;
        private System.Windows.Forms.TextBox TxtPurchaseReturnAc;
        private System.Windows.Forms.Button BtnPurchaseDiscount;
        private System.Windows.Forms.Button BtnPurchaseProDiscount;
        private System.Windows.Forms.Button BtnPurchaseAdditionalVAT;
        private System.Windows.Forms.TextBox TxtDiscount;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox TxtPurchaseAddVat;
        private System.Windows.Forms.TextBox TxtProDiscount;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.ComboBox CmbCreditBalance;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.ComboBox CmbCreditDays;
        private System.Windows.Forms.CheckBox ChkBasicAmountEnable;
        private System.Windows.Forms.CheckBox ChkCurrencyEnable;
        private System.Windows.Forms.CheckBox ChkChallanEnable;
        private System.Windows.Forms.CheckBox ChkGodownEnable;
        private System.Windows.Forms.CheckBox ChkDepartmentEnable;
        private System.Windows.Forms.CheckBox ChkOrderEnable;
        private System.Windows.Forms.CheckBox ChkLastRate;
        private System.Windows.Forms.CheckBox ChkCarryRate;
        private System.Windows.Forms.CheckBox ChkAgentEnable;
        private System.Windows.Forms.CheckBox ChkIndentEnable;
        private System.Windows.Forms.CheckBox ChkAltUnitEnable;
        private System.Windows.Forms.CheckBox ChkSubLedgerEnable;
        private System.Windows.Forms.CheckBox ChkSubLedgerMandatory;
        private System.Windows.Forms.CheckBox ChkChallanMandatory;
        private System.Windows.Forms.CheckBox ChkOrderMandatory;
        private System.Windows.Forms.CheckBox ChkAgentMandatory;
        private System.Windows.Forms.CheckBox ChkDepartmentMandatory;
        private System.Windows.Forms.CheckBox ChkGodownMandatory;
        private System.Windows.Forms.CheckBox ChkCurrencyMandatory;
        private DevExpress.XtraEditors.SimpleButton Btn_Save;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.CheckBox ChkDateChangeEnable;
        private System.Windows.Forms.CheckBox ChkChangeRate;
        private System.Windows.Forms.CheckBox ChkChangeCurrencyRate;
        private System.Windows.Forms.CheckBox ChkNarrationEnable;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox ChkRemarksEnable;
        private System.Windows.Forms.CheckBox ChkRemarksMandatory;
        private ClsSeparator clsSeparator1;
    }
}