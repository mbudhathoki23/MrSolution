using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.SystemSetting
{
    partial class FrmSystemSettings
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
            this.TxtBackupLocation = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_Path = new System.Windows.Forms.Label();
            this.TxtBackupSchIntervaldays = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_AutoBackUp = new System.Windows.Forms.Label();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSaveClose = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSavingPath = new System.Windows.Forms.Button();
            this.ChkShowPassword = new System.Windows.Forms.CheckBox();
            this.TxtEmailPassword = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtEmailId = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SaveLocation = new System.Windows.Forms.FolderBrowserDialog();
            this.BtnCreditors = new System.Windows.Forms.Button();
            this.BtnDebtors = new System.Windows.Forms.Button();
            this.TxtCreditors = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.TxtDebtors = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnPF = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.BtnTDS = new System.Windows.Forms.Button();
            this.BtnSalary = new System.Windows.Forms.Button();
            this.TxtSalaryLedger = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtPfLedger = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtTdsLedger = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.CmbTextColor = new System.Windows.Forms.ComboBox();
            this.label48 = new System.Windows.Forms.Label();
            this.CmbFormColor = new System.Windows.Forms.ComboBox();
            this.label47 = new System.Windows.Forms.Label();
            this.CmbReportFontStyle = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.CmbPaperSize = new System.Windows.Forms.ComboBox();
            this.CmbFontSize = new System.Windows.Forms.ComboBox();
            this.CmbFontName = new System.Windows.Forms.ComboBox();
            this.lbl_PaperSize = new System.Windows.Forms.Label();
            this.lbl_FontSize = new System.Windows.Forms.Label();
            this.lbl_FontName = new System.Windows.Forms.Label();
            this.BtnAbtInvoiceDesign = new System.Windows.Forms.Button();
            this.BtnInvoiceDesign = new System.Windows.Forms.Button();
            this.BtnOrderNumbering = new System.Windows.Forms.Button();
            this.BtnInvoicePrinter = new System.Windows.Forms.Button();
            this.BtnAbtInvNumbering = new System.Windows.Forms.Button();
            this.BtnOrderDesign = new System.Windows.Forms.Button();
            this.BtnInvoiceNumbering = new System.Windows.Forms.Button();
            this.BtnOrderPrinter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.lbl_OrderDesignName = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.CmbFiscalYear = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CmbRateFormat = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_Qty = new System.Windows.Forms.Label();
            this.CmbCurrencyFormat = new System.Windows.Forms.ComboBox();
            this.CmbQtyFormat = new System.Windows.Forms.ComboBox();
            this.lbl_CurrencyNume = new System.Windows.Forms.Label();
            this.lbl_Amount = new System.Windows.Forms.Label();
            this.CmbAmountFormat = new System.Windows.Forms.ComboBox();
            this.CmbDefaultPrinter = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.CmbCurrency = new System.Windows.Forms.ComboBox();
            this.lbl_Currency = new System.Windows.Forms.Label();
            this.ChkConfirmCancel = new System.Windows.Forms.CheckBox();
            this.ChkEnableUdf = new System.Windows.Forms.CheckBox();
            this.ChkConfirmSave = new System.Windows.Forms.CheckBox();
            this.ChkCurrentDate = new System.Windows.Forms.CheckBox();
            this.ChkAuditTrial = new System.Windows.Forms.CheckBox();
            this.ChkAutoPopList = new System.Windows.Forms.CheckBox();
            this.rChkMiti = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.MskApplicableTime = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ChkNightAuditApplicable = new System.Windows.Forms.CheckBox();
            this.ChkBarcodeSearch = new System.Windows.Forms.CheckBox();
            this.ChkPrintBranch = new System.Windows.Forms.CheckBox();
            this.ChkPrintDateTime = new System.Windows.Forms.CheckBox();
            this.ChkOrderPrint = new System.Windows.Forms.CheckBox();
            this.ChkInvPrint = new System.Windows.Forms.CheckBox();
            this.ChkConfirmExits = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rChkDate = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TxtOrderPrinter = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtInvoicePrinter = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtAbtInvoiceDesign = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.TxtInvoiceDesign = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtOrderDesign = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtInvoiceNumbering = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtAbtInvoiceNumbering = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtOrderNumbering = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.tabSetting = new System.Windows.Forms.TabControl();
            this.tbSetting = new System.Windows.Forms.TabPage();
            this.tbPrint = new System.Windows.Forms.TabPage();
            this.panel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.mrGroup1 = new MrDAL.Control.ControlsEx.Control.MrGroup();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.ChkSearchAlpha = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.tabSetting.SuspendLayout();
            this.tbSetting.SuspendLayout();
            this.tbPrint.SuspendLayout();
            this.panel1.SuspendLayout();
            this.mrGroup1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxtBackupLocation
            // 
            this.TxtBackupLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBackupLocation.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBackupLocation.Location = new System.Drawing.Point(267, 23);
            this.TxtBackupLocation.Name = "TxtBackupLocation";
            this.TxtBackupLocation.Size = new System.Drawing.Size(422, 23);
            this.TxtBackupLocation.TabIndex = 1;
            this.TxtBackupLocation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            // 
            // lbl_Path
            // 
            this.lbl_Path.AutoSize = true;
            this.lbl_Path.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Path.Location = new System.Drawing.Point(193, 25);
            this.lbl_Path.Name = "lbl_Path";
            this.lbl_Path.Size = new System.Drawing.Size(73, 19);
            this.lbl_Path.TabIndex = 212;
            this.lbl_Path.Text = "Location:";
            // 
            // TxtBackupSchIntervaldays
            // 
            this.TxtBackupSchIntervaldays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBackupSchIntervaldays.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBackupSchIntervaldays.Location = new System.Drawing.Point(123, 23);
            this.TxtBackupSchIntervaldays.Name = "TxtBackupSchIntervaldays";
            this.TxtBackupSchIntervaldays.Size = new System.Drawing.Size(66, 23);
            this.TxtBackupSchIntervaldays.TabIndex = 0;
            this.TxtBackupSchIntervaldays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            // 
            // lbl_AutoBackUp
            // 
            this.lbl_AutoBackUp.AutoSize = true;
            this.lbl_AutoBackUp.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AutoBackUp.Location = new System.Drawing.Point(15, 25);
            this.lbl_AutoBackUp.Name = "lbl_AutoBackUp";
            this.lbl_AutoBackUp.Size = new System.Drawing.Size(105, 19);
            this.lbl_AutoBackUp.TabIndex = 211;
            this.lbl_AutoBackUp.Text = "Interval Days:";
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(544, 16);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(94, 40);
            this.BtnSave.TabIndex = 1;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnSaveClose
            // 
            this.BtnSaveClose.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSaveClose.Appearance.Options.UseFont = true;
            this.BtnSaveClose.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSaveClose.Location = new System.Drawing.Point(374, 16);
            this.BtnSaveClose.Name = "BtnSaveClose";
            this.BtnSaveClose.Size = new System.Drawing.Size(169, 40);
            this.BtnSaveClose.TabIndex = 0;
            this.BtnSaveClose.Text = "SAVE && &CLOSE";
            this.BtnSaveClose.Click += new System.EventHandler(this.BtnSaveClose_Click);
            // 
            // BtnSavingPath
            // 
            this.BtnSavingPath.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSavingPath.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnSavingPath.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnSavingPath.Location = new System.Drawing.Point(695, 21);
            this.BtnSavingPath.Name = "BtnSavingPath";
            this.BtnSavingPath.Size = new System.Drawing.Size(27, 23);
            this.BtnSavingPath.TabIndex = 220;
            this.BtnSavingPath.TabStop = false;
            this.BtnSavingPath.UseVisualStyleBackColor = true;
            this.BtnSavingPath.Click += new System.EventHandler(this.BtnSavingPath_Click);
            // 
            // ChkShowPassword
            // 
            this.ChkShowPassword.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkShowPassword.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkShowPassword.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkShowPassword.Location = new System.Drawing.Point(249, 45);
            this.ChkShowPassword.Name = "ChkShowPassword";
            this.ChkShowPassword.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ChkShowPassword.Size = new System.Drawing.Size(134, 23);
            this.ChkShowPassword.TabIndex = 2;
            this.ChkShowPassword.Text = "Show Password";
            this.ChkShowPassword.UseVisualStyleBackColor = true;
            this.ChkShowPassword.CheckedChanged += new System.EventHandler(this.ChkShowPassword_CheckedChanged);
            // 
            // TxtEmailPassword
            // 
            this.TxtEmailPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtEmailPassword.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtEmailPassword.Location = new System.Drawing.Point(125, 45);
            this.TxtEmailPassword.Name = "TxtEmailPassword";
            this.TxtEmailPassword.Size = new System.Drawing.Size(122, 23);
            this.TxtEmailPassword.TabIndex = 1;
            this.TxtEmailPassword.UseSystemPasswordChar = true;
            this.TxtEmailPassword.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtEmailPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtEmailPassword.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // TxtEmailId
            // 
            this.TxtEmailId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtEmailId.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtEmailId.Location = new System.Drawing.Point(125, 21);
            this.TxtEmailId.Name = "TxtEmailId";
            this.TxtEmailId.Size = new System.Drawing.Size(380, 23);
            this.TxtEmailId.TabIndex = 0;
            this.TxtEmailId.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtEmailId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtEmailId.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 19);
            this.label3.TabIndex = 225;
            this.label3.Text = "Password :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 19);
            this.label4.TabIndex = 224;
            this.label4.Text = "Email Address :";
            // 
            // BtnCreditors
            // 
            this.BtnCreditors.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCreditors.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnCreditors.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnCreditors.Location = new System.Drawing.Point(511, 48);
            this.BtnCreditors.Name = "BtnCreditors";
            this.BtnCreditors.Size = new System.Drawing.Size(22, 23);
            this.BtnCreditors.TabIndex = 219;
            this.BtnCreditors.TabStop = false;
            this.BtnCreditors.UseVisualStyleBackColor = true;
            this.BtnCreditors.Click += new System.EventHandler(this.BtnCreditors_Click);
            // 
            // BtnDebtors
            // 
            this.BtnDebtors.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDebtors.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnDebtors.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDebtors.Location = new System.Drawing.Point(512, 22);
            this.BtnDebtors.Name = "BtnDebtors";
            this.BtnDebtors.Size = new System.Drawing.Size(22, 23);
            this.BtnDebtors.TabIndex = 218;
            this.BtnDebtors.TabStop = false;
            this.BtnDebtors.UseVisualStyleBackColor = true;
            this.BtnDebtors.Click += new System.EventHandler(this.BtnDebtors_Click);
            // 
            // TxtCreditors
            // 
            this.TxtCreditors.BackColor = System.Drawing.Color.White;
            this.TxtCreditors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCreditors.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCreditors.Location = new System.Drawing.Point(83, 48);
            this.TxtCreditors.MaxLength = 255;
            this.TxtCreditors.Name = "TxtCreditors";
            this.TxtCreditors.ReadOnly = true;
            this.TxtCreditors.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtCreditors.Size = new System.Drawing.Size(422, 23);
            this.TxtCreditors.TabIndex = 1;
            this.TxtCreditors.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtCreditors.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCreditors_KeyDown);
            this.TxtCreditors.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtCreditors.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(6, 26);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(66, 19);
            this.label17.TabIndex = 216;
            this.label17.Text = "Debtors:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(3, 50);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 19);
            this.label16.TabIndex = 217;
            this.label16.Text = "Creditors:";
            // 
            // TxtDebtors
            // 
            this.TxtDebtors.BackColor = System.Drawing.Color.White;
            this.TxtDebtors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDebtors.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDebtors.Location = new System.Drawing.Point(83, 23);
            this.TxtDebtors.MaxLength = 255;
            this.TxtDebtors.Name = "TxtDebtors";
            this.TxtDebtors.ReadOnly = true;
            this.TxtDebtors.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtDebtors.Size = new System.Drawing.Size(422, 23);
            this.TxtDebtors.TabIndex = 0;
            this.TxtDebtors.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtDebtors.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDebtors_KeyDown);
            this.TxtDebtors.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtDebtors.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // BtnPF
            // 
            this.BtnPF.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPF.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnPF.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnPF.Location = new System.Drawing.Point(394, 70);
            this.BtnPF.Name = "BtnPF";
            this.BtnPF.Size = new System.Drawing.Size(22, 23);
            this.BtnPF.TabIndex = 213;
            this.BtnPF.TabStop = false;
            this.BtnPF.UseVisualStyleBackColor = true;
            this.BtnPF.Click += new System.EventHandler(this.BtnPF_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(6, 22);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(57, 19);
            this.label15.TabIndex = 208;
            this.label15.Text = "Salary:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(6, 72);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(31, 19);
            this.label18.TabIndex = 212;
            this.label18.Text = "PF:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(6, 49);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(42, 19);
            this.label14.TabIndex = 209;
            this.label14.Text = "TDS:";
            // 
            // BtnTDS
            // 
            this.BtnTDS.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnTDS.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnTDS.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnTDS.Location = new System.Drawing.Point(394, 45);
            this.BtnTDS.Name = "BtnTDS";
            this.BtnTDS.Size = new System.Drawing.Size(22, 23);
            this.BtnTDS.TabIndex = 211;
            this.BtnTDS.TabStop = false;
            this.BtnTDS.UseVisualStyleBackColor = true;
            this.BtnTDS.Click += new System.EventHandler(this.BtnTDS_Click);
            // 
            // BtnSalary
            // 
            this.BtnSalary.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSalary.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnSalary.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnSalary.Location = new System.Drawing.Point(394, 20);
            this.BtnSalary.Name = "BtnSalary";
            this.BtnSalary.Size = new System.Drawing.Size(22, 23);
            this.BtnSalary.TabIndex = 210;
            this.BtnSalary.TabStop = false;
            this.BtnSalary.UseVisualStyleBackColor = true;
            this.BtnSalary.Click += new System.EventHandler(this.BtnSalary_Click);
            // 
            // TxtSalaryLedger
            // 
            this.TxtSalaryLedger.BackColor = System.Drawing.Color.White;
            this.TxtSalaryLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSalaryLedger.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSalaryLedger.Location = new System.Drawing.Point(83, 20);
            this.TxtSalaryLedger.MaxLength = 255;
            this.TxtSalaryLedger.Name = "TxtSalaryLedger";
            this.TxtSalaryLedger.ReadOnly = true;
            this.TxtSalaryLedger.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtSalaryLedger.Size = new System.Drawing.Size(305, 23);
            this.TxtSalaryLedger.TabIndex = 0;
            this.TxtSalaryLedger.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtSalaryLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSalary_KeyDown);
            this.TxtSalaryLedger.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtSalaryLedger.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // TxtPfLedger
            // 
            this.TxtPfLedger.BackColor = System.Drawing.Color.White;
            this.TxtPfLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPfLedger.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPfLedger.Location = new System.Drawing.Point(83, 70);
            this.TxtPfLedger.MaxLength = 255;
            this.TxtPfLedger.Name = "TxtPfLedger";
            this.TxtPfLedger.ReadOnly = true;
            this.TxtPfLedger.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtPfLedger.Size = new System.Drawing.Size(305, 23);
            this.TxtPfLedger.TabIndex = 2;
            this.TxtPfLedger.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtPfLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPF_KeyDown);
            this.TxtPfLedger.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtPfLedger.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // TxtTdsLedger
            // 
            this.TxtTdsLedger.BackColor = System.Drawing.Color.White;
            this.TxtTdsLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTdsLedger.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTdsLedger.Location = new System.Drawing.Point(83, 45);
            this.TxtTdsLedger.MaxLength = 255;
            this.TxtTdsLedger.Name = "TxtTdsLedger";
            this.TxtTdsLedger.ReadOnly = true;
            this.TxtTdsLedger.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtTdsLedger.Size = new System.Drawing.Size(305, 23);
            this.TxtTdsLedger.TabIndex = 1;
            this.TxtTdsLedger.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtTdsLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtTDS_KeyDown);
            this.TxtTdsLedger.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtTdsLedger.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // CmbTextColor
            // 
            this.CmbTextColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbTextColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbTextColor.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbTextColor.FormattingEnabled = true;
            this.CmbTextColor.Location = new System.Drawing.Point(432, 170);
            this.CmbTextColor.Name = "CmbTextColor";
            this.CmbTextColor.Size = new System.Drawing.Size(280, 26);
            this.CmbTextColor.TabIndex = 6;
            this.CmbTextColor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.Location = new System.Drawing.Point(333, 170);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(84, 19);
            this.label48.TabIndex = 185;
            this.label48.Text = "Text Color:";
            // 
            // CmbFormColor
            // 
            this.CmbFormColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbFormColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbFormColor.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbFormColor.FormattingEnabled = true;
            this.CmbFormColor.Location = new System.Drawing.Point(432, 141);
            this.CmbFormColor.Name = "CmbFormColor";
            this.CmbFormColor.Size = new System.Drawing.Size(280, 26);
            this.CmbFormColor.TabIndex = 5;
            this.CmbFormColor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.Location = new System.Drawing.Point(333, 145);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(90, 19);
            this.label47.TabIndex = 184;
            this.label47.Text = "Form Color:";
            // 
            // CmbReportFontStyle
            // 
            this.CmbReportFontStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbReportFontStyle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbReportFontStyle.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbReportFontStyle.FormattingEnabled = true;
            this.CmbReportFontStyle.Location = new System.Drawing.Point(432, 112);
            this.CmbReportFontStyle.Name = "CmbReportFontStyle";
            this.CmbReportFontStyle.Size = new System.Drawing.Size(280, 26);
            this.CmbReportFontStyle.TabIndex = 3;
            this.CmbReportFontStyle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(333, 115);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(97, 19);
            this.label12.TabIndex = 181;
            this.label12.Text = "Report Style:";
            // 
            // CmbPaperSize
            // 
            this.CmbPaperSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbPaperSize.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbPaperSize.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbPaperSize.FormattingEnabled = true;
            this.CmbPaperSize.Location = new System.Drawing.Point(432, 83);
            this.CmbPaperSize.Name = "CmbPaperSize";
            this.CmbPaperSize.Size = new System.Drawing.Size(280, 26);
            this.CmbPaperSize.TabIndex = 2;
            this.CmbPaperSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            // 
            // CmbFontSize
            // 
            this.CmbFontSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbFontSize.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbFontSize.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbFontSize.FormattingEnabled = true;
            this.CmbFontSize.Location = new System.Drawing.Point(432, 54);
            this.CmbFontSize.Name = "CmbFontSize";
            this.CmbFontSize.Size = new System.Drawing.Size(280, 26);
            this.CmbFontSize.TabIndex = 1;
            this.CmbFontSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            // 
            // CmbFontName
            // 
            this.CmbFontName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbFontName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbFontName.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbFontName.FormattingEnabled = true;
            this.CmbFontName.Location = new System.Drawing.Point(432, 25);
            this.CmbFontName.Name = "CmbFontName";
            this.CmbFontName.Size = new System.Drawing.Size(280, 26);
            this.CmbFontName.TabIndex = 0;
            this.CmbFontName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            // 
            // lbl_PaperSize
            // 
            this.lbl_PaperSize.AutoSize = true;
            this.lbl_PaperSize.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_PaperSize.Location = new System.Drawing.Point(333, 86);
            this.lbl_PaperSize.Name = "lbl_PaperSize";
            this.lbl_PaperSize.Size = new System.Drawing.Size(89, 19);
            this.lbl_PaperSize.TabIndex = 180;
            this.lbl_PaperSize.Text = "Paper Size :";
            // 
            // lbl_FontSize
            // 
            this.lbl_FontSize.AutoSize = true;
            this.lbl_FontSize.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_FontSize.Location = new System.Drawing.Point(333, 57);
            this.lbl_FontSize.Name = "lbl_FontSize";
            this.lbl_FontSize.Size = new System.Drawing.Size(42, 19);
            this.lbl_FontSize.TabIndex = 179;
            this.lbl_FontSize.Text = "Size:";
            // 
            // lbl_FontName
            // 
            this.lbl_FontName.AutoSize = true;
            this.lbl_FontName.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_FontName.Location = new System.Drawing.Point(333, 28);
            this.lbl_FontName.Name = "lbl_FontName";
            this.lbl_FontName.Size = new System.Drawing.Size(92, 19);
            this.lbl_FontName.TabIndex = 178;
            this.lbl_FontName.Text = "Font Name :";
            // 
            // BtnAbtInvoiceDesign
            // 
            this.BtnAbtInvoiceDesign.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.BtnAbtInvoiceDesign.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnAbtInvoiceDesign.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnAbtInvoiceDesign.Location = new System.Drawing.Point(510, 194);
            this.BtnAbtInvoiceDesign.Name = "BtnAbtInvoiceDesign";
            this.BtnAbtInvoiceDesign.Size = new System.Drawing.Size(25, 23);
            this.BtnAbtInvoiceDesign.TabIndex = 240;
            this.BtnAbtInvoiceDesign.TabStop = false;
            this.BtnAbtInvoiceDesign.UseVisualStyleBackColor = true;
            this.BtnAbtInvoiceDesign.Click += new System.EventHandler(this.BtnAbtInvoiceDesign_Click);
            // 
            // BtnInvoiceDesign
            // 
            this.BtnInvoiceDesign.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.BtnInvoiceDesign.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnInvoiceDesign.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnInvoiceDesign.Location = new System.Drawing.Point(510, 169);
            this.BtnInvoiceDesign.Name = "BtnInvoiceDesign";
            this.BtnInvoiceDesign.Size = new System.Drawing.Size(25, 23);
            this.BtnInvoiceDesign.TabIndex = 239;
            this.BtnInvoiceDesign.TabStop = false;
            this.BtnInvoiceDesign.UseVisualStyleBackColor = true;
            this.BtnInvoiceDesign.Click += new System.EventHandler(this.BtnInvoiceDesign_Click);
            // 
            // BtnOrderNumbering
            // 
            this.BtnOrderNumbering.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.BtnOrderNumbering.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnOrderNumbering.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnOrderNumbering.Location = new System.Drawing.Point(510, 73);
            this.BtnOrderNumbering.Name = "BtnOrderNumbering";
            this.BtnOrderNumbering.Size = new System.Drawing.Size(25, 23);
            this.BtnOrderNumbering.TabIndex = 238;
            this.BtnOrderNumbering.TabStop = false;
            this.BtnOrderNumbering.UseVisualStyleBackColor = true;
            this.BtnOrderNumbering.Click += new System.EventHandler(this.BtnOrderNumbering_Click);
            // 
            // BtnInvoicePrinter
            // 
            this.BtnInvoicePrinter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.BtnInvoicePrinter.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnInvoicePrinter.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnInvoicePrinter.Location = new System.Drawing.Point(510, 49);
            this.BtnInvoicePrinter.Name = "BtnInvoicePrinter";
            this.BtnInvoicePrinter.Size = new System.Drawing.Size(25, 23);
            this.BtnInvoicePrinter.TabIndex = 237;
            this.BtnInvoicePrinter.TabStop = false;
            this.BtnInvoicePrinter.UseVisualStyleBackColor = true;
            this.BtnInvoicePrinter.Click += new System.EventHandler(this.BtnInvoicePrinter_Click);
            // 
            // BtnAbtInvNumbering
            // 
            this.BtnAbtInvNumbering.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.BtnAbtInvNumbering.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnAbtInvNumbering.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnAbtInvNumbering.Location = new System.Drawing.Point(510, 122);
            this.BtnAbtInvNumbering.Name = "BtnAbtInvNumbering";
            this.BtnAbtInvNumbering.Size = new System.Drawing.Size(25, 23);
            this.BtnAbtInvNumbering.TabIndex = 236;
            this.BtnAbtInvNumbering.TabStop = false;
            this.BtnAbtInvNumbering.UseVisualStyleBackColor = true;
            this.BtnAbtInvNumbering.Click += new System.EventHandler(this.BtnAbtInvNumbering_Click);
            // 
            // BtnOrderDesign
            // 
            this.BtnOrderDesign.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.BtnOrderDesign.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnOrderDesign.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnOrderDesign.Location = new System.Drawing.Point(510, 146);
            this.BtnOrderDesign.Name = "BtnOrderDesign";
            this.BtnOrderDesign.Size = new System.Drawing.Size(25, 23);
            this.BtnOrderDesign.TabIndex = 235;
            this.BtnOrderDesign.TabStop = false;
            this.BtnOrderDesign.UseVisualStyleBackColor = true;
            this.BtnOrderDesign.Click += new System.EventHandler(this.BtnOrderDesign_Click);
            // 
            // BtnInvoiceNumbering
            // 
            this.BtnInvoiceNumbering.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.BtnInvoiceNumbering.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnInvoiceNumbering.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnInvoiceNumbering.Location = new System.Drawing.Point(510, 98);
            this.BtnInvoiceNumbering.Name = "BtnInvoiceNumbering";
            this.BtnInvoiceNumbering.Size = new System.Drawing.Size(25, 23);
            this.BtnInvoiceNumbering.TabIndex = 234;
            this.BtnInvoiceNumbering.TabStop = false;
            this.BtnInvoiceNumbering.UseVisualStyleBackColor = true;
            this.BtnInvoiceNumbering.Click += new System.EventHandler(this.BtnInvoiceNumbering_Click);
            // 
            // BtnOrderPrinter
            // 
            this.BtnOrderPrinter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.BtnOrderPrinter.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnOrderPrinter.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnOrderPrinter.Location = new System.Drawing.Point(510, 24);
            this.BtnOrderPrinter.Name = "BtnOrderPrinter";
            this.BtnOrderPrinter.Size = new System.Drawing.Size(25, 23);
            this.BtnOrderPrinter.TabIndex = 233;
            this.BtnOrderPrinter.TabStop = false;
            this.BtnOrderPrinter.UseVisualStyleBackColor = true;
            this.BtnOrderPrinter.Click += new System.EventHandler(this.BtnOrderPrinter_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 196);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(150, 19);
            this.label1.TabIndex = 228;
            this.label1.Text = "Default AVT Design:";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(12, 171);
            this.label42.Name = "label42";
            this.label42.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label42.Size = new System.Drawing.Size(143, 19);
            this.label42.TabIndex = 227;
            this.label42.Text = "Default Inv Design:";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(10, 75);
            this.label38.Name = "label38";
            this.label38.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label38.Size = new System.Drawing.Size(190, 19);
            this.label38.TabIndex = 226;
            this.label38.Text = "Default Order Numbering:";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(10, 50);
            this.label36.Name = "label36";
            this.label36.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label36.Size = new System.Drawing.Size(147, 19);
            this.label36.TabIndex = 225;
            this.label36.Text = "Default Inv Printer :";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(11, 26);
            this.label35.Name = "label35";
            this.label35.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label35.Size = new System.Drawing.Size(165, 19);
            this.label35.TabIndex = 217;
            this.label35.Text = "Default Order Printer :";
            // 
            // lbl_OrderDesignName
            // 
            this.lbl_OrderDesignName.AutoSize = true;
            this.lbl_OrderDesignName.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_OrderDesignName.Location = new System.Drawing.Point(12, 123);
            this.lbl_OrderDesignName.Name = "lbl_OrderDesignName";
            this.lbl_OrderDesignName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_OrderDesignName.Size = new System.Drawing.Size(205, 19);
            this.lbl_OrderDesignName.TabIndex = 218;
            this.lbl_OrderDesignName.Text = "Default AVT Inv Numbering:";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(11, 100);
            this.label37.Name = "label37";
            this.label37.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label37.Size = new System.Drawing.Size(172, 19);
            this.label37.TabIndex = 219;
            this.label37.Text = "Default Inv Numbering:";
            // 
            // CmbFiscalYear
            // 
            this.CmbFiscalYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbFiscalYear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbFiscalYear.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbFiscalYear.FormattingEnabled = true;
            this.CmbFiscalYear.Location = new System.Drawing.Point(142, 54);
            this.CmbFiscalYear.Name = "CmbFiscalYear";
            this.CmbFiscalYear.Size = new System.Drawing.Size(177, 26);
            this.CmbFiscalYear.TabIndex = 1;
            this.CmbFiscalYear.SelectedIndexChanged += new System.EventHandler(this.CmbFiscalYear_SelectedIndexChanged);
            this.CmbFiscalYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 19);
            this.label5.TabIndex = 71;
            this.label5.Text = "Fiscal Year :";
            // 
            // CmbRateFormat
            // 
            this.CmbRateFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbRateFormat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbRateFormat.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbRateFormat.FormattingEnabled = true;
            this.CmbRateFormat.Location = new System.Drawing.Point(142, 113);
            this.CmbRateFormat.Name = "CmbRateFormat";
            this.CmbRateFormat.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CmbRateFormat.Size = new System.Drawing.Size(177, 26);
            this.CmbRateFormat.TabIndex = 3;
            this.CmbRateFormat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 19);
            this.label7.TabIndex = 69;
            this.label7.Text = "Rate Format:";
            // 
            // lbl_Qty
            // 
            this.lbl_Qty.AutoSize = true;
            this.lbl_Qty.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Qty.Location = new System.Drawing.Point(6, 146);
            this.lbl_Qty.Name = "lbl_Qty";
            this.lbl_Qty.Size = new System.Drawing.Size(90, 19);
            this.lbl_Qty.TabIndex = 66;
            this.lbl_Qty.Text = "Qty Format:";
            // 
            // CmbCurrencyFormat
            // 
            this.CmbCurrencyFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbCurrencyFormat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbCurrencyFormat.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbCurrencyFormat.FormattingEnabled = true;
            this.CmbCurrencyFormat.Location = new System.Drawing.Point(142, 171);
            this.CmbCurrencyFormat.Name = "CmbCurrencyFormat";
            this.CmbCurrencyFormat.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CmbCurrencyFormat.Size = new System.Drawing.Size(177, 26);
            this.CmbCurrencyFormat.TabIndex = 5;
            this.CmbCurrencyFormat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            // 
            // CmbQtyFormat
            // 
            this.CmbQtyFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbQtyFormat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbQtyFormat.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbQtyFormat.FormattingEnabled = true;
            this.CmbQtyFormat.Location = new System.Drawing.Point(142, 142);
            this.CmbQtyFormat.Name = "CmbQtyFormat";
            this.CmbQtyFormat.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CmbQtyFormat.Size = new System.Drawing.Size(177, 26);
            this.CmbQtyFormat.TabIndex = 4;
            this.CmbQtyFormat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            // 
            // lbl_CurrencyNume
            // 
            this.lbl_CurrencyNume.AutoSize = true;
            this.lbl_CurrencyNume.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CurrencyNume.Location = new System.Drawing.Point(6, 175);
            this.lbl_CurrencyNume.Name = "lbl_CurrencyNume";
            this.lbl_CurrencyNume.Size = new System.Drawing.Size(132, 19);
            this.lbl_CurrencyNume.TabIndex = 67;
            this.lbl_CurrencyNume.Text = "Currency Fromat:";
            // 
            // lbl_Amount
            // 
            this.lbl_Amount.AutoSize = true;
            this.lbl_Amount.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Amount.Location = new System.Drawing.Point(6, 87);
            this.lbl_Amount.Name = "lbl_Amount";
            this.lbl_Amount.Size = new System.Drawing.Size(122, 19);
            this.lbl_Amount.TabIndex = 62;
            this.lbl_Amount.Text = "Amount Format:";
            // 
            // CmbAmountFormat
            // 
            this.CmbAmountFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbAmountFormat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbAmountFormat.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbAmountFormat.FormattingEnabled = true;
            this.CmbAmountFormat.Location = new System.Drawing.Point(142, 83);
            this.CmbAmountFormat.Name = "CmbAmountFormat";
            this.CmbAmountFormat.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CmbAmountFormat.Size = new System.Drawing.Size(177, 26);
            this.CmbAmountFormat.TabIndex = 2;
            this.CmbAmountFormat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            // 
            // CmbDefaultPrinter
            // 
            this.CmbDefaultPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbDefaultPrinter.DropDownWidth = 250;
            this.CmbDefaultPrinter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbDefaultPrinter.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbDefaultPrinter.FormattingEnabled = true;
            this.CmbDefaultPrinter.Location = new System.Drawing.Point(142, 200);
            this.CmbDefaultPrinter.Name = "CmbDefaultPrinter";
            this.CmbDefaultPrinter.Size = new System.Drawing.Size(570, 26);
            this.CmbDefaultPrinter.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(11, 203);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 19);
            this.label6.TabIndex = 54;
            this.label6.Text = "Default Printer:";
            // 
            // CmbCurrency
            // 
            this.CmbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbCurrency.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbCurrency.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbCurrency.FormattingEnabled = true;
            this.CmbCurrency.Location = new System.Drawing.Point(142, 25);
            this.CmbCurrency.Name = "CmbCurrency";
            this.CmbCurrency.Size = new System.Drawing.Size(177, 26);
            this.CmbCurrency.TabIndex = 0;
            this.CmbCurrency.SelectedValueChanged += new System.EventHandler(this.CmbCurrency_SelectedValueChanged);
            this.CmbCurrency.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            // 
            // lbl_Currency
            // 
            this.lbl_Currency.AutoSize = true;
            this.lbl_Currency.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Currency.Location = new System.Drawing.Point(6, 29);
            this.lbl_Currency.Name = "lbl_Currency";
            this.lbl_Currency.Size = new System.Drawing.Size(83, 19);
            this.lbl_Currency.TabIndex = 53;
            this.lbl_Currency.Text = "Currency :";
            // 
            // ChkConfirmCancel
            // 
            this.ChkConfirmCancel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ChkConfirmCancel.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkConfirmCancel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ChkConfirmCancel.Location = new System.Drawing.Point(215, 88);
            this.ChkConfirmCancel.Name = "ChkConfirmCancel";
            this.ChkConfirmCancel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkConfirmCancel.Size = new System.Drawing.Size(139, 25);
            this.ChkConfirmCancel.TabIndex = 5;
            this.ChkConfirmCancel.Text = "Confirm Cancel";
            this.ChkConfirmCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkConfirmCancel.UseVisualStyleBackColor = false;
            // 
            // ChkEnableUdf
            // 
            this.ChkEnableUdf.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ChkEnableUdf.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkEnableUdf.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ChkEnableUdf.Location = new System.Drawing.Point(215, 21);
            this.ChkEnableUdf.Name = "ChkEnableUdf";
            this.ChkEnableUdf.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkEnableUdf.Size = new System.Drawing.Size(139, 25);
            this.ChkEnableUdf.TabIndex = 4;
            this.ChkEnableUdf.Text = "UDF";
            this.ChkEnableUdf.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkEnableUdf.UseVisualStyleBackColor = false;
            // 
            // ChkConfirmSave
            // 
            this.ChkConfirmSave.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ChkConfirmSave.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkConfirmSave.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ChkConfirmSave.Location = new System.Drawing.Point(215, 66);
            this.ChkConfirmSave.Name = "ChkConfirmSave";
            this.ChkConfirmSave.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkConfirmSave.Size = new System.Drawing.Size(139, 25);
            this.ChkConfirmSave.TabIndex = 3;
            this.ChkConfirmSave.Text = "Confirm Save";
            this.ChkConfirmSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkConfirmSave.UseVisualStyleBackColor = false;
            // 
            // ChkCurrentDate
            // 
            this.ChkCurrentDate.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ChkCurrentDate.Checked = true;
            this.ChkCurrentDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkCurrentDate.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkCurrentDate.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ChkCurrentDate.Location = new System.Drawing.Point(6, 136);
            this.ChkCurrentDate.Name = "ChkCurrentDate";
            this.ChkCurrentDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkCurrentDate.Size = new System.Drawing.Size(139, 25);
            this.ChkCurrentDate.TabIndex = 1;
            this.ChkCurrentDate.Text = "Current Date";
            this.ChkCurrentDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkCurrentDate.UseVisualStyleBackColor = false;
            // 
            // ChkAuditTrial
            // 
            this.ChkAuditTrial.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ChkAuditTrial.Checked = true;
            this.ChkAuditTrial.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkAuditTrial.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAuditTrial.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ChkAuditTrial.Location = new System.Drawing.Point(6, 90);
            this.ChkAuditTrial.Name = "ChkAuditTrial";
            this.ChkAuditTrial.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkAuditTrial.Size = new System.Drawing.Size(139, 25);
            this.ChkAuditTrial.TabIndex = 2;
            this.ChkAuditTrial.Text = "Audit Trial";
            this.ChkAuditTrial.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkAuditTrial.UseVisualStyleBackColor = false;
            // 
            // ChkAutoPopList
            // 
            this.ChkAutoPopList.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ChkAutoPopList.Checked = true;
            this.ChkAutoPopList.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkAutoPopList.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAutoPopList.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ChkAutoPopList.Location = new System.Drawing.Point(6, 113);
            this.ChkAutoPopList.Name = "ChkAutoPopList";
            this.ChkAutoPopList.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkAutoPopList.Size = new System.Drawing.Size(139, 25);
            this.ChkAutoPopList.TabIndex = 6;
            this.ChkAutoPopList.Text = "Auto Popup";
            this.ChkAutoPopList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkAutoPopList.UseVisualStyleBackColor = false;
            // 
            // rChkMiti
            // 
            this.rChkMiti.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.rChkMiti.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rChkMiti.Checked = true;
            this.rChkMiti.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rChkMiti.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rChkMiti.Location = new System.Drawing.Point(5, 14);
            this.rChkMiti.Name = "rChkMiti";
            this.rChkMiti.Size = new System.Drawing.Size(139, 20);
            this.rChkMiti.TabIndex = 0;
            this.rChkMiti.TabStop = true;
            this.rChkMiti.Text = "Miti Based";
            this.rChkMiti.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Controls.Add(this.ChkSearchAlpha);
            this.groupBox1.Controls.Add(this.groupBox7);
            this.groupBox1.Controls.Add(this.ChkBarcodeSearch);
            this.groupBox1.Controls.Add(this.ChkPrintBranch);
            this.groupBox1.Controls.Add(this.ChkPrintDateTime);
            this.groupBox1.Controls.Add(this.ChkOrderPrint);
            this.groupBox1.Controls.Add(this.ChkInvPrint);
            this.groupBox1.Controls.Add(this.ChkConfirmExits);
            this.groupBox1.Controls.Add(this.ChkConfirmCancel);
            this.groupBox1.Controls.Add(this.ChkAuditTrial);
            this.groupBox1.Controls.Add(this.ChkEnableUdf);
            this.groupBox1.Controls.Add(this.ChkAutoPopList);
            this.groupBox1.Controls.Add(this.ChkCurrentDate);
            this.groupBox1.Controls.Add(this.ChkConfirmSave);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(751, 187);
            this.groupBox1.TabIndex = 227;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Option Tag";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.MskApplicableTime);
            this.groupBox7.Controls.Add(this.label2);
            this.groupBox7.Controls.Add(this.ChkNightAuditApplicable);
            this.groupBox7.Location = new System.Drawing.Point(160, 118);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(357, 64);
            this.groupBox7.TabIndex = 15;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Night Audit";
            // 
            // MskApplicableTime
            // 
            this.MskApplicableTime.Location = new System.Drawing.Point(249, 27);
            this.MskApplicableTime.Mask = "00:00";
            this.MskApplicableTime.Name = "MskApplicableTime";
            this.MskApplicableTime.Size = new System.Drawing.Size(100, 26);
            this.MskApplicableTime.TabIndex = 222;
            this.MskApplicableTime.ValidatingType = typeof(System.DateTime);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(198, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 19);
            this.label2.TabIndex = 221;
            this.label2.Text = "Time";
            // 
            // ChkNightAuditApplicable
            // 
            this.ChkNightAuditApplicable.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ChkNightAuditApplicable.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkNightAuditApplicable.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ChkNightAuditApplicable.Location = new System.Drawing.Point(6, 25);
            this.ChkNightAuditApplicable.Name = "ChkNightAuditApplicable";
            this.ChkNightAuditApplicable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkNightAuditApplicable.Size = new System.Drawing.Size(188, 25);
            this.ChkNightAuditApplicable.TabIndex = 16;
            this.ChkNightAuditApplicable.Text = "Night Audit Appicable";
            this.ChkNightAuditApplicable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkNightAuditApplicable.UseVisualStyleBackColor = false;
            this.ChkNightAuditApplicable.CheckedChanged += new System.EventHandler(this.ChkNightAuditApplicable_CheckedChanged);
            // 
            // ChkBarcodeSearch
            // 
            this.ChkBarcodeSearch.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ChkBarcodeSearch.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkBarcodeSearch.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ChkBarcodeSearch.Location = new System.Drawing.Point(364, 88);
            this.ChkBarcodeSearch.Name = "ChkBarcodeSearch";
            this.ChkBarcodeSearch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkBarcodeSearch.Size = new System.Drawing.Size(139, 25);
            this.ChkBarcodeSearch.TabIndex = 13;
            this.ChkBarcodeSearch.Text = "Barcode Search";
            this.ChkBarcodeSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkBarcodeSearch.UseVisualStyleBackColor = false;
            // 
            // ChkPrintBranch
            // 
            this.ChkPrintBranch.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ChkPrintBranch.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkPrintBranch.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ChkPrintBranch.Location = new System.Drawing.Point(215, 42);
            this.ChkPrintBranch.Name = "ChkPrintBranch";
            this.ChkPrintBranch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkPrintBranch.Size = new System.Drawing.Size(139, 25);
            this.ChkPrintBranch.TabIndex = 12;
            this.ChkPrintBranch.Text = "Print Branch";
            this.ChkPrintBranch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkPrintBranch.UseVisualStyleBackColor = false;
            // 
            // ChkPrintDateTime
            // 
            this.ChkPrintDateTime.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ChkPrintDateTime.Checked = true;
            this.ChkPrintDateTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkPrintDateTime.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkPrintDateTime.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ChkPrintDateTime.Location = new System.Drawing.Point(364, 21);
            this.ChkPrintDateTime.Name = "ChkPrintDateTime";
            this.ChkPrintDateTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkPrintDateTime.Size = new System.Drawing.Size(139, 25);
            this.ChkPrintDateTime.TabIndex = 10;
            this.ChkPrintDateTime.Text = "Date Time";
            this.ChkPrintDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkPrintDateTime.UseVisualStyleBackColor = false;
            // 
            // ChkOrderPrint
            // 
            this.ChkOrderPrint.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ChkOrderPrint.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkOrderPrint.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ChkOrderPrint.Location = new System.Drawing.Point(6, 159);
            this.ChkOrderPrint.Name = "ChkOrderPrint";
            this.ChkOrderPrint.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkOrderPrint.Size = new System.Drawing.Size(139, 25);
            this.ChkOrderPrint.TabIndex = 8;
            this.ChkOrderPrint.Text = "Order Print";
            this.ChkOrderPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkOrderPrint.UseVisualStyleBackColor = false;
            // 
            // ChkInvPrint
            // 
            this.ChkInvPrint.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ChkInvPrint.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkInvPrint.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ChkInvPrint.Location = new System.Drawing.Point(364, 65);
            this.ChkInvPrint.Name = "ChkInvPrint";
            this.ChkInvPrint.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkInvPrint.Size = new System.Drawing.Size(139, 25);
            this.ChkInvPrint.TabIndex = 9;
            this.ChkInvPrint.Text = "Invoice Print";
            this.ChkInvPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkInvPrint.UseVisualStyleBackColor = false;
            // 
            // ChkConfirmExits
            // 
            this.ChkConfirmExits.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ChkConfirmExits.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkConfirmExits.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ChkConfirmExits.Location = new System.Drawing.Point(364, 42);
            this.ChkConfirmExits.Name = "ChkConfirmExits";
            this.ChkConfirmExits.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkConfirmExits.Size = new System.Drawing.Size(139, 25);
            this.ChkConfirmExits.TabIndex = 7;
            this.ChkConfirmExits.Text = "Confirm Exit";
            this.ChkConfirmExits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkConfirmExits.UseVisualStyleBackColor = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rChkMiti);
            this.groupBox4.Controls.Add(this.rChkDate);
            this.groupBox4.Location = new System.Drawing.Point(9, 21);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 63);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            // 
            // rChkDate
            // 
            this.rChkDate.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.rChkDate.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rChkDate.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rChkDate.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rChkDate.Location = new System.Drawing.Point(6, 37);
            this.rChkDate.Name = "rChkDate";
            this.rChkDate.Size = new System.Drawing.Size(139, 20);
            this.rChkDate.TabIndex = 11;
            this.rChkDate.Text = "Date Based";
            this.rChkDate.UseVisualStyleBackColor = false;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox2.Controls.Add(this.CmbTextColor);
            this.groupBox2.Controls.Add(this.CmbFiscalYear);
            this.groupBox2.Controls.Add(this.lbl_FontName);
            this.groupBox2.Controls.Add(this.lbl_Currency);
            this.groupBox2.Controls.Add(this.label48);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.lbl_FontSize);
            this.groupBox2.Controls.Add(this.CmbCurrency);
            this.groupBox2.Controls.Add(this.CmbFormColor);
            this.groupBox2.Controls.Add(this.CmbRateFormat);
            this.groupBox2.Controls.Add(this.lbl_PaperSize);
            this.groupBox2.Controls.Add(this.CmbAmountFormat);
            this.groupBox2.Controls.Add(this.label47);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.CmbFontName);
            this.groupBox2.Controls.Add(this.CmbReportFontStyle);
            this.groupBox2.Controls.Add(this.lbl_Amount);
            this.groupBox2.Controls.Add(this.CmbFontSize);
            this.groupBox2.Controls.Add(this.lbl_Qty);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.CmbDefaultPrinter);
            this.groupBox2.Controls.Add(this.CmbPaperSize);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lbl_CurrencyNume);
            this.groupBox2.Controls.Add(this.CmbCurrencyFormat);
            this.groupBox2.Controls.Add(this.CmbQtyFormat);
            this.groupBox2.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(6, 196);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(751, 247);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Format Tag";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox3.Controls.Add(this.BtnAbtInvoiceDesign);
            this.groupBox3.Controls.Add(this.TxtOrderPrinter);
            this.groupBox3.Controls.Add(this.label36);
            this.groupBox3.Controls.Add(this.BtnInvoiceDesign);
            this.groupBox3.Controls.Add(this.BtnOrderPrinter);
            this.groupBox3.Controls.Add(this.lbl_OrderDesignName);
            this.groupBox3.Controls.Add(this.label42);
            this.groupBox3.Controls.Add(this.label38);
            this.groupBox3.Controls.Add(this.TxtInvoicePrinter);
            this.groupBox3.Controls.Add(this.TxtAbtInvoiceDesign);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label39);
            this.groupBox3.Controls.Add(this.label35);
            this.groupBox3.Controls.Add(this.BtnOrderNumbering);
            this.groupBox3.Controls.Add(this.BtnInvoicePrinter);
            this.groupBox3.Controls.Add(this.TxtInvoiceDesign);
            this.groupBox3.Controls.Add(this.TxtOrderDesign);
            this.groupBox3.Controls.Add(this.TxtInvoiceNumbering);
            this.groupBox3.Controls.Add(this.BtnOrderDesign);
            this.groupBox3.Controls.Add(this.label37);
            this.groupBox3.Controls.Add(this.BtnInvoiceNumbering);
            this.groupBox3.Controls.Add(this.TxtAbtInvoiceNumbering);
            this.groupBox3.Controls.Add(this.BtnAbtInvNumbering);
            this.groupBox3.Controls.Add(this.TxtOrderNumbering);
            this.groupBox3.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(5, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(718, 228);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Print && Document Numbering Setting";
            // 
            // TxtOrderPrinter
            // 
            this.TxtOrderPrinter.BackColor = System.Drawing.Color.White;
            this.TxtOrderPrinter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtOrderPrinter.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtOrderPrinter.Location = new System.Drawing.Point(217, 25);
            this.TxtOrderPrinter.MaxLength = 255;
            this.TxtOrderPrinter.Name = "TxtOrderPrinter";
            this.TxtOrderPrinter.ReadOnly = true;
            this.TxtOrderPrinter.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtOrderPrinter.Size = new System.Drawing.Size(289, 23);
            this.TxtOrderPrinter.TabIndex = 0;
            this.TxtOrderPrinter.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtOrderPrinter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDOPrinter_KeyDown);
            this.TxtOrderPrinter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtOrderPrinter.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // TxtInvoicePrinter
            // 
            this.TxtInvoicePrinter.BackColor = System.Drawing.Color.White;
            this.TxtInvoicePrinter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtInvoicePrinter.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtInvoicePrinter.Location = new System.Drawing.Point(217, 49);
            this.TxtInvoicePrinter.MaxLength = 255;
            this.TxtInvoicePrinter.Name = "TxtInvoicePrinter";
            this.TxtInvoicePrinter.ReadOnly = true;
            this.TxtInvoicePrinter.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtInvoicePrinter.Size = new System.Drawing.Size(289, 23);
            this.TxtInvoicePrinter.TabIndex = 1;
            this.TxtInvoicePrinter.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtInvoicePrinter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDIPrinter_KeyDown);
            this.TxtInvoicePrinter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtInvoicePrinter.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // TxtAbtInvoiceDesign
            // 
            this.TxtAbtInvoiceDesign.BackColor = System.Drawing.Color.White;
            this.TxtAbtInvoiceDesign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAbtInvoiceDesign.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAbtInvoiceDesign.Location = new System.Drawing.Point(217, 194);
            this.TxtAbtInvoiceDesign.MaxLength = 255;
            this.TxtAbtInvoiceDesign.Name = "TxtAbtInvoiceDesign";
            this.TxtAbtInvoiceDesign.ReadOnly = true;
            this.TxtAbtInvoiceDesign.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtAbtInvoiceDesign.Size = new System.Drawing.Size(289, 23);
            this.TxtAbtInvoiceDesign.TabIndex = 7;
            this.TxtAbtInvoiceDesign.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtAbtInvoiceDesign.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtRIDesign_KeyDown);
            this.TxtAbtInvoiceDesign.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtAbtInvoiceDesign.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(12, 148);
            this.label39.Name = "label39";
            this.label39.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label39.Size = new System.Drawing.Size(161, 19);
            this.label39.TabIndex = 220;
            this.label39.Text = "Default Order Design:";
            // 
            // TxtInvoiceDesign
            // 
            this.TxtInvoiceDesign.BackColor = System.Drawing.Color.White;
            this.TxtInvoiceDesign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtInvoiceDesign.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtInvoiceDesign.Location = new System.Drawing.Point(217, 169);
            this.TxtInvoiceDesign.MaxLength = 255;
            this.TxtInvoiceDesign.Name = "TxtInvoiceDesign";
            this.TxtInvoiceDesign.ReadOnly = true;
            this.TxtInvoiceDesign.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtInvoiceDesign.Size = new System.Drawing.Size(289, 23);
            this.TxtInvoiceDesign.TabIndex = 6;
            this.TxtInvoiceDesign.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtInvoiceDesign.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtAPDesign_KeyDown);
            this.TxtInvoiceDesign.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtInvoiceDesign.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // TxtOrderDesign
            // 
            this.TxtOrderDesign.BackColor = System.Drawing.Color.White;
            this.TxtOrderDesign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtOrderDesign.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtOrderDesign.Location = new System.Drawing.Point(217, 145);
            this.TxtOrderDesign.MaxLength = 255;
            this.TxtOrderDesign.Name = "TxtOrderDesign";
            this.TxtOrderDesign.ReadOnly = true;
            this.TxtOrderDesign.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtOrderDesign.Size = new System.Drawing.Size(289, 23);
            this.TxtOrderDesign.TabIndex = 5;
            this.TxtOrderDesign.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtOrderDesign.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDIDesign_KeyDown);
            this.TxtOrderDesign.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtOrderDesign.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // TxtInvoiceNumbering
            // 
            this.TxtInvoiceNumbering.BackColor = System.Drawing.Color.White;
            this.TxtInvoiceNumbering.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtInvoiceNumbering.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtInvoiceNumbering.Location = new System.Drawing.Point(217, 97);
            this.TxtInvoiceNumbering.MaxLength = 255;
            this.TxtInvoiceNumbering.Name = "TxtInvoiceNumbering";
            this.TxtInvoiceNumbering.ReadOnly = true;
            this.TxtInvoiceNumbering.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtInvoiceNumbering.Size = new System.Drawing.Size(289, 23);
            this.TxtInvoiceNumbering.TabIndex = 3;
            this.TxtInvoiceNumbering.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtInvoiceNumbering.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtODNumbering_KeyDown);
            this.TxtInvoiceNumbering.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtInvoiceNumbering.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // TxtAbtInvoiceNumbering
            // 
            this.TxtAbtInvoiceNumbering.BackColor = System.Drawing.Color.White;
            this.TxtAbtInvoiceNumbering.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAbtInvoiceNumbering.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAbtInvoiceNumbering.Location = new System.Drawing.Point(217, 121);
            this.TxtAbtInvoiceNumbering.MaxLength = 255;
            this.TxtAbtInvoiceNumbering.Name = "TxtAbtInvoiceNumbering";
            this.TxtAbtInvoiceNumbering.ReadOnly = true;
            this.TxtAbtInvoiceNumbering.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtAbtInvoiceNumbering.Size = new System.Drawing.Size(289, 23);
            this.TxtAbtInvoiceNumbering.TabIndex = 4;
            this.TxtAbtInvoiceNumbering.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtAbtInvoiceNumbering.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDODesign_KeyDown);
            this.TxtAbtInvoiceNumbering.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtAbtInvoiceNumbering.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // TxtOrderNumbering
            // 
            this.TxtOrderNumbering.BackColor = System.Drawing.Color.White;
            this.TxtOrderNumbering.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtOrderNumbering.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtOrderNumbering.Location = new System.Drawing.Point(217, 73);
            this.TxtOrderNumbering.MaxLength = 255;
            this.TxtOrderNumbering.Name = "TxtOrderNumbering";
            this.TxtOrderNumbering.ReadOnly = true;
            this.TxtOrderNumbering.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtOrderNumbering.Size = new System.Drawing.Size(289, 23);
            this.TxtOrderNumbering.TabIndex = 2;
            this.TxtOrderNumbering.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtOrderNumbering.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtIDNumbering_KeyDown);
            this.TxtOrderNumbering.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtOrderNumbering.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.TxtTdsLedger);
            this.groupBox5.Controls.Add(this.TxtPfLedger);
            this.groupBox5.Controls.Add(this.TxtSalaryLedger);
            this.groupBox5.Controls.Add(this.BtnSalary);
            this.groupBox5.Controls.Add(this.BtnTDS);
            this.groupBox5.Controls.Add(this.BtnPF);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(5, 401);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(717, 99);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Payroll Tag";
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox6.Controls.Add(this.label4);
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Controls.Add(this.TxtEmailId);
            this.groupBox6.Controls.Add(this.TxtEmailPassword);
            this.groupBox6.Controls.Add(this.ChkShowPassword);
            this.groupBox6.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(6, 230);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(717, 72);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Email";
            // 
            // groupBox9
            // 
            this.groupBox9.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox9.Controls.Add(this.TxtBackupLocation);
            this.groupBox9.Controls.Add(this.lbl_AutoBackUp);
            this.groupBox9.Controls.Add(this.lbl_Path);
            this.groupBox9.Controls.Add(this.BtnSavingPath);
            this.groupBox9.Controls.Add(this.TxtBackupSchIntervaldays);
            this.groupBox9.Location = new System.Drawing.Point(6, 449);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(751, 73);
            this.groupBox9.TabIndex = 1;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Backup Details";
            // 
            // groupBox12
            // 
            this.groupBox12.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox12.Controls.Add(this.BtnCreditors);
            this.groupBox12.Controls.Add(this.label17);
            this.groupBox12.Controls.Add(this.TxtDebtors);
            this.groupBox12.Controls.Add(this.BtnDebtors);
            this.groupBox12.Controls.Add(this.label16);
            this.groupBox12.Controls.Add(this.TxtCreditors);
            this.groupBox12.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox12.Location = new System.Drawing.Point(6, 302);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(717, 97);
            this.groupBox12.TabIndex = 2;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Tours && Travel Account Group";
            // 
            // tabSetting
            // 
            this.tabSetting.Controls.Add(this.tbSetting);
            this.tabSetting.Controls.Add(this.tbPrint);
            this.tabSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSetting.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabSetting.Location = new System.Drawing.Point(0, 0);
            this.tabSetting.Name = "tabSetting";
            this.tabSetting.SelectedIndex = 0;
            this.tabSetting.Size = new System.Drawing.Size(769, 562);
            this.tabSetting.TabIndex = 240;
            // 
            // tbSetting
            // 
            this.tbSetting.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tbSetting.Controls.Add(this.groupBox1);
            this.tbSetting.Controls.Add(this.groupBox2);
            this.tbSetting.Controls.Add(this.groupBox9);
            this.tbSetting.Location = new System.Drawing.Point(4, 29);
            this.tbSetting.Name = "tbSetting";
            this.tbSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tbSetting.Size = new System.Drawing.Size(761, 529);
            this.tbSetting.TabIndex = 0;
            this.tbSetting.Text = "SETTING";
            // 
            // tbPrint
            // 
            this.tbPrint.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tbPrint.Controls.Add(this.groupBox5);
            this.tbPrint.Controls.Add(this.groupBox3);
            this.tbPrint.Controls.Add(this.groupBox12);
            this.tbPrint.Controls.Add(this.groupBox6);
            this.tbPrint.Location = new System.Drawing.Point(4, 29);
            this.tbPrint.Name = "tbPrint";
            this.tbPrint.Padding = new System.Windows.Forms.Padding(3);
            this.tbPrint.Size = new System.Drawing.Size(761, 529);
            this.tbPrint.TabIndex = 1;
            this.tbPrint.Text = "PRINT & EMAIL ";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.tabSetting);
            this.panel1.Controls.Add(this.mrGroup1);
            this.panel1.Controls.Add(this.clsSeparator1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(769, 623);
            this.panel1.TabIndex = 241;
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrDAL.Control.ControlsEx.Control.MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.BtnSaveClose);
            this.mrGroup1.Controls.Add(this.BtnSave);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "";
            this.mrGroup1.Location = new System.Drawing.Point(0, 562);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 10;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = false;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(769, 61);
            this.mrGroup1.TabIndex = 242;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(4, 493);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(631, 2);
            this.clsSeparator1.TabIndex = 241;
            this.clsSeparator1.TabStop = false;
            // 
            // ChkSearchAlpha
            // 
            this.ChkSearchAlpha.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ChkSearchAlpha.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSearchAlpha.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ChkSearchAlpha.Location = new System.Drawing.Point(509, 24);
            this.ChkSearchAlpha.Name = "ChkSearchAlpha";
            this.ChkSearchAlpha.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSearchAlpha.Size = new System.Drawing.Size(139, 25);
            this.ChkSearchAlpha.TabIndex = 16;
            this.ChkSearchAlpha.Text = "Search Alpha";
            this.ChkSearchAlpha.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSearchAlpha.UseVisualStyleBackColor = false;
            // 
            // FrmSystemSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(769, 623);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSystemSettings";
            this.ShowIcon = false;
            this.Text = "SYSTEM SETTING";
            this.Load += new System.EventHandler(this.FrmSystemSettings_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmSystemSettings_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.tabSetting.ResumeLayout(false);
            this.tbSetting.ResumeLayout(false);
            this.tbPrint.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.mrGroup1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox CmbDefaultPrinter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox CmbCurrency;
        private System.Windows.Forms.Label lbl_Currency;
        private System.Windows.Forms.Label lbl_Amount;
        private System.Windows.Forms.ComboBox CmbAmountFormat;
        private System.Windows.Forms.Label lbl_Qty;
        private System.Windows.Forms.ComboBox CmbCurrencyFormat;
        private System.Windows.Forms.ComboBox CmbQtyFormat;
        private System.Windows.Forms.Label lbl_CurrencyNume;
        private System.Windows.Forms.ComboBox CmbRateFormat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox CmbFiscalYear;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label lbl_OrderDesignName;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Button BtnAbtInvoiceDesign;
        private System.Windows.Forms.Button BtnInvoiceDesign;
        private System.Windows.Forms.Button BtnOrderNumbering;
        private System.Windows.Forms.Button BtnInvoicePrinter;
        private System.Windows.Forms.Button BtnAbtInvNumbering;
        private System.Windows.Forms.Button BtnOrderDesign;
        private System.Windows.Forms.Button BtnInvoiceNumbering;
        private System.Windows.Forms.Button BtnOrderPrinter;
        private System.Windows.Forms.ComboBox CmbReportFontStyle;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox CmbPaperSize;
        private System.Windows.Forms.ComboBox CmbFontSize;
        private System.Windows.Forms.ComboBox CmbFontName;
        private System.Windows.Forms.Label lbl_PaperSize;
        private System.Windows.Forms.Label lbl_FontSize;
        private System.Windows.Forms.Label lbl_FontName;
        private System.Windows.Forms.ComboBox CmbTextColor;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.ComboBox CmbFormColor;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Button BtnPF;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button BtnTDS;
        private System.Windows.Forms.Button BtnSalary;
        private System.Windows.Forms.Button BtnCreditors;
        private System.Windows.Forms.Button BtnDebtors;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lbl_Path;
        private System.Windows.Forms.Label lbl_AutoBackUp;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SimpleButton BtnSaveClose;
        private System.Windows.Forms.Button BtnSavingPath;
        private System.Windows.Forms.CheckBox ChkShowPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rChkMiti;
        private System.Windows.Forms.CheckBox ChkAutoPopList;
        private System.Windows.Forms.CheckBox ChkAuditTrial;
        private System.Windows.Forms.CheckBox ChkCurrentDate;
        private System.Windows.Forms.CheckBox ChkConfirmSave;
        private System.Windows.Forms.CheckBox ChkEnableUdf;
        private System.Windows.Forms.CheckBox ChkConfirmCancel;
        private System.Windows.Forms.FolderBrowserDialog SaveLocation;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.CheckBox ChkConfirmExits;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.CheckBox ChkOrderPrint;
        private System.Windows.Forms.CheckBox ChkInvPrint;
        private System.Windows.Forms.CheckBox ChkPrintDateTime;
        private System.Windows.Forms.RadioButton rChkDate;
        private System.Windows.Forms.TabControl tabSetting;
        private System.Windows.Forms.TabPage tbSetting;
        private System.Windows.Forms.TabPage tbPrint;
        private ClsSeparator clsSeparator1;
        private System.Windows.Forms.CheckBox ChkPrintBranch;
        private MrTextBox TxtSalaryLedger;
        private MrTextBox TxtPfLedger;
        private MrTextBox TxtTdsLedger;
        private MrTextBox TxtCreditors;
        private MrTextBox TxtDebtors;
        private MrTextBox TxtBackupLocation;
        private MrTextBox TxtBackupSchIntervaldays;
        private MrTextBox TxtEmailPassword;
        private MrTextBox TxtEmailId;
        private MrTextBox TxtOrderPrinter;
        private MrTextBox TxtInvoiceNumbering;
        private MrTextBox TxtOrderDesign;
        private MrTextBox TxtAbtInvoiceNumbering;
        private MrTextBox TxtAbtInvoiceDesign;
        private MrTextBox TxtInvoiceDesign;
        private MrTextBox TxtOrderNumbering;
        private MrTextBox TxtInvoicePrinter;
        private MrPanel panel1;
        private System.Windows.Forms.CheckBox ChkBarcodeSearch;
        private MrGroup mrGroup1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckBox ChkNightAuditApplicable;
        private System.Windows.Forms.MaskedTextBox MskApplicableTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox ChkSearchAlpha;
    }
}