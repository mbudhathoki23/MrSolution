using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.DataEntry.OpeningMaster
{
    partial class FrmLedgerOpeningBillWiseEntry
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLedgerOpeningBillWiseEntry));
            this.DGrid = new System.Windows.Forms.DataGridView();
            this.GTxtSno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtLedgerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TxtVno = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_VoucherNo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnAgent = new System.Windows.Forms.Button();
            this.BtnDepartment = new System.Windows.Forms.Button();
            this.BtnSubledger = new System.Windows.Forms.Button();
            this.TxtCurrencyRate = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_CurrencyRate = new System.Windows.Forms.Label();
            this.lbl_Currency = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.TxtNarration = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtCreditAmount = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtDebitAmount = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtDepartment = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lvlSubledger = new System.Windows.Forms.Label();
            this.TxtSubledger = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.TxtAgent = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.LblDifferencAmount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LblLocalCreditAmount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LblCreditAmount = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.LblLocalDebitAmount = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.LblDebitAmount = new System.Windows.Forms.Label();
            this.LblInWords = new System.Windows.Forms.Label();
            this.lbl_NoInWords = new System.Windows.Forms.Label();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.LblLedgerDesc = new System.Windows.Forms.Label();
            this.lblHints = new System.Windows.Forms.Label();
            this.MskDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.MskMiti = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.BtnVNo = new System.Windows.Forms.Button();
            this.CmbModule = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PanelHeader = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.GTxtSearch = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.btnNarration = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.BtnCurrency = new System.Windows.Forms.Button();
            this.TxtCurrency = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.Btn_Narration = new System.Windows.Forms.Button();
            this.clsSeparator3 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.panel3 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).BeginInit();
            this.PanelHeader.SuspendLayout();
            this.btnNarration.SuspendLayout();
            this.SuspendLayout();
            // 
            // DGrid
            // 
            this.DGrid.AllowUserToAddRows = false;
            this.DGrid.AllowUserToDeleteRows = false;
            this.DGrid.AllowUserToResizeColumns = false;
            this.DGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.DGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.DGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.DGrid.CausesValidation = false;
            this.DGrid.ColumnHeadersHeight = 25;
            this.DGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GTxtSno,
            this.GTxtLedgerId,
            this.GTxtDescription,
            this.GTxtAmount,
            this.GTxtType});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGrid.DefaultCellStyle = dataGridViewCellStyle9;
            this.DGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.DGrid.Location = new System.Drawing.Point(0, 0);
            this.DGrid.Margin = new System.Windows.Forms.Padding(2);
            this.DGrid.MultiSelect = false;
            this.DGrid.Name = "DGrid";
            this.DGrid.ReadOnly = true;
            this.DGrid.RowHeadersVisible = false;
            this.DGrid.RowHeadersWidth = 51;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.Snow;
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            this.DGrid.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.DGrid.RowTemplate.Height = 24;
            this.DGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGrid.Size = new System.Drawing.Size(680, 438);
            this.DGrid.StandardTab = true;
            this.DGrid.TabIndex = 0;
            this.DGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGrid_CellContentClick);
            this.DGrid.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGrid_CellContentDoubleClick);
            this.DGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGrid_CellDoubleClick);
            this.DGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellEnter);
            this.DGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_RowEnter);
            this.DGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Grid_KeyDown);
            // 
            // GTxtSno
            // 
            this.GTxtSno.Frozen = true;
            this.GTxtSno.HeaderText = "SNo";
            this.GTxtSno.MinimumWidth = 6;
            this.GTxtSno.Name = "GTxtSno";
            this.GTxtSno.ReadOnly = true;
            this.GTxtSno.Width = 40;
            // 
            // GTxtLedgerId
            // 
            this.GTxtLedgerId.HeaderText = "GlId";
            this.GTxtLedgerId.MinimumWidth = 6;
            this.GTxtLedgerId.Name = "GTxtLedgerId";
            this.GTxtLedgerId.ReadOnly = true;
            this.GTxtLedgerId.Visible = false;
            this.GTxtLedgerId.Width = 125;
            // 
            // GTxtDescription
            // 
            this.GTxtDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GTxtDescription.DefaultCellStyle = dataGridViewCellStyle7;
            this.GTxtDescription.HeaderText = "Description";
            this.GTxtDescription.MinimumWidth = 6;
            this.GTxtDescription.Name = "GTxtDescription";
            this.GTxtDescription.ReadOnly = true;
            // 
            // GTxtAmount
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GTxtAmount.DefaultCellStyle = dataGridViewCellStyle8;
            this.GTxtAmount.HeaderText = "Amount";
            this.GTxtAmount.MinimumWidth = 6;
            this.GTxtAmount.Name = "GTxtAmount";
            this.GTxtAmount.ReadOnly = true;
            this.GTxtAmount.Width = 120;
            // 
            // GTxtType
            // 
            this.GTxtType.HeaderText = "";
            this.GTxtType.MinimumWidth = 6;
            this.GTxtType.Name = "GTxtType";
            this.GTxtType.ReadOnly = true;
            this.GTxtType.Width = 40;
            // 
            // TxtVno
            // 
            this.TxtVno.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtVno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtVno.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtVno.Location = new System.Drawing.Point(110, 76);
            this.TxtVno.MaxLength = 50;
            this.TxtVno.Name = "TxtVno";
            this.TxtVno.Size = new System.Drawing.Size(135, 25);
            this.TxtVno.TabIndex = 5;
            this.TxtVno.Enter += new System.EventHandler(this.txtVno_Enter);
            this.TxtVno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVno_KeyDown);
            this.TxtVno.Leave += new System.EventHandler(this.txtVno_Leave);
            this.TxtVno.Validating += new System.ComponentModel.CancelEventHandler(this.txtVno_Validating);
            // 
            // lbl_VoucherNo
            // 
            this.lbl_VoucherNo.AutoSize = true;
            this.lbl_VoucherNo.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_VoucherNo.Location = new System.Drawing.Point(10, 79);
            this.lbl_VoucherNo.Name = "lbl_VoucherNo";
            this.lbl_VoucherNo.Size = new System.Drawing.Size(96, 19);
            this.lbl_VoucherNo.TabIndex = 51;
            this.lbl_VoucherNo.Text = "Voucher No";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(276, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 19);
            this.label1.TabIndex = 56;
            this.label1.Text = "Date";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // BtnAgent
            // 
            this.BtnAgent.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAgent.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnAgent.Image = ((System.Drawing.Image)(resources.GetObject("BtnAgent.Image")));
            this.BtnAgent.Location = new System.Drawing.Point(506, 166);
            this.BtnAgent.Name = "BtnAgent";
            this.BtnAgent.Size = new System.Drawing.Size(32, 25);
            this.BtnAgent.TabIndex = 323;
            this.BtnAgent.UseVisualStyleBackColor = true;
            this.BtnAgent.Click += new System.EventHandler(this.btnAgent_Click);
            // 
            // BtnDepartment
            // 
            this.BtnDepartment.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDepartment.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnDepartment.Image = ((System.Drawing.Image)(resources.GetObject("BtnDepartment.Image")));
            this.BtnDepartment.Location = new System.Drawing.Point(506, 136);
            this.BtnDepartment.Name = "BtnDepartment";
            this.BtnDepartment.Size = new System.Drawing.Size(32, 25);
            this.BtnDepartment.TabIndex = 322;
            this.BtnDepartment.UseVisualStyleBackColor = true;
            this.BtnDepartment.Click += new System.EventHandler(this.BtnDepartment_Click);
            // 
            // BtnSubledger
            // 
            this.BtnSubledger.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSubledger.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnSubledger.Image = ((System.Drawing.Image)(resources.GetObject("BtnSubledger.Image")));
            this.BtnSubledger.Location = new System.Drawing.Point(506, 106);
            this.BtnSubledger.Name = "BtnSubledger";
            this.BtnSubledger.Size = new System.Drawing.Size(32, 25);
            this.BtnSubledger.TabIndex = 321;
            this.BtnSubledger.UseVisualStyleBackColor = true;
            this.BtnSubledger.Click += new System.EventHandler(this.btnSubledger_Click);
            // 
            // TxtCurrencyRate
            // 
            this.TxtCurrencyRate.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtCurrencyRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCurrencyRate.CausesValidation = false;
            this.TxtCurrencyRate.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCurrencyRate.Location = new System.Drawing.Point(381, 196);
            this.TxtCurrencyRate.MaxLength = 50;
            this.TxtCurrencyRate.Name = "TxtCurrencyRate";
            this.TxtCurrencyRate.Size = new System.Drawing.Size(157, 25);
            this.TxtCurrencyRate.TabIndex = 12;
            this.TxtCurrencyRate.Text = "1.00";
            this.TxtCurrencyRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtCurrencyRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCurrencyRate_KeyDown);
            this.TxtCurrencyRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCurrencyRate_KeyPress);
            this.TxtCurrencyRate.Leave += new System.EventHandler(this.txtCurrencyRate_Leave);
            // 
            // lbl_CurrencyRate
            // 
            this.lbl_CurrencyRate.AutoSize = true;
            this.lbl_CurrencyRate.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CurrencyRate.Location = new System.Drawing.Point(308, 199);
            this.lbl_CurrencyRate.Name = "lbl_CurrencyRate";
            this.lbl_CurrencyRate.Size = new System.Drawing.Size(44, 19);
            this.lbl_CurrencyRate.TabIndex = 213;
            this.lbl_CurrencyRate.Text = "Rate";
            // 
            // lbl_Currency
            // 
            this.lbl_Currency.AutoSize = true;
            this.lbl_Currency.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Currency.Location = new System.Drawing.Point(10, 199);
            this.lbl_Currency.Name = "lbl_Currency";
            this.lbl_Currency.Size = new System.Drawing.Size(79, 19);
            this.lbl_Currency.TabIndex = 212;
            this.lbl_Currency.Text = "Currency";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(10, 259);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(81, 19);
            this.label12.TabIndex = 209;
            this.label12.Text = "Narration";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(308, 229);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 19);
            this.label11.TabIndex = 208;
            this.label11.Text = "Credit";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(10, 229);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 19);
            this.label8.TabIndex = 207;
            this.label8.Text = "Debit";
            // 
            // TxtNarration
            // 
            this.TxtNarration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNarration.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtNarration.Location = new System.Drawing.Point(110, 256);
            this.TxtNarration.MaxLength = 255;
            this.TxtNarration.Name = "TxtNarration";
            this.TxtNarration.Size = new System.Drawing.Size(399, 25);
            this.TxtNarration.TabIndex = 15;
            this.TxtNarration.Enter += new System.EventHandler(this.txtNarration_Enter);
            this.TxtNarration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtNarration_KeyDown);
            this.TxtNarration.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNarration_KeyPress);
            this.TxtNarration.Leave += new System.EventHandler(this.txtNarration_Leave);
            // 
            // TxtCreditAmount
            // 
            this.TxtCreditAmount.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtCreditAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCreditAmount.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCreditAmount.Location = new System.Drawing.Point(381, 226);
            this.TxtCreditAmount.MaxLength = 50;
            this.TxtCreditAmount.Name = "TxtCreditAmount";
            this.TxtCreditAmount.Size = new System.Drawing.Size(157, 25);
            this.TxtCreditAmount.TabIndex = 14;
            this.TxtCreditAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtCreditAmount.TextChanged += new System.EventHandler(this.txt_Credit_TextChanged);
            this.TxtCreditAmount.Enter += new System.EventHandler(this.txt_Credit_Enter);
            this.TxtCreditAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Credit_KeyDown);
            this.TxtCreditAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Credit_KeyPress);
            this.TxtCreditAmount.Leave += new System.EventHandler(this.txt_Credit_Leave);
            this.TxtCreditAmount.Validating += new System.ComponentModel.CancelEventHandler(this.txt_Credit_Validating);
            // 
            // TxtDebitAmount
            // 
            this.TxtDebitAmount.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtDebitAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDebitAmount.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDebitAmount.Location = new System.Drawing.Point(110, 226);
            this.TxtDebitAmount.MaxLength = 50;
            this.TxtDebitAmount.Name = "TxtDebitAmount";
            this.TxtDebitAmount.Size = new System.Drawing.Size(157, 25);
            this.TxtDebitAmount.TabIndex = 13;
            this.TxtDebitAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtDebitAmount.TextChanged += new System.EventHandler(this.txt_Debit_TextChanged);
            this.TxtDebitAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Debit_KeyDown);
            this.TxtDebitAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Debit_KeyPress);
            this.TxtDebitAmount.Validated += new System.EventHandler(this.txt_Debit_Validated);
            // 
            // TxtDepartment
            // 
            this.TxtDepartment.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtDepartment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDepartment.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDepartment.Location = new System.Drawing.Point(110, 136);
            this.TxtDepartment.MaxLength = 255;
            this.TxtDepartment.Name = "TxtDepartment";
            this.TxtDepartment.Size = new System.Drawing.Size(395, 25);
            this.TxtDepartment.TabIndex = 9;
            this.TxtDepartment.Enter += new System.EventHandler(this.txtClass_Enter);
            this.TxtDepartment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtClass_KeyDown);
            this.TxtDepartment.Leave += new System.EventHandler(this.txtClass_Leave);
            this.TxtDepartment.Validating += new System.ComponentModel.CancelEventHandler(this.txtClass_Validating);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(10, 139);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 19);
            this.label7.TabIndex = 202;
            this.label7.Text = "Department";
            // 
            // lvlSubledger
            // 
            this.lvlSubledger.AutoSize = true;
            this.lvlSubledger.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlSubledger.Location = new System.Drawing.Point(10, 109);
            this.lvlSubledger.Name = "lvlSubledger";
            this.lvlSubledger.Size = new System.Drawing.Size(88, 19);
            this.lvlSubledger.TabIndex = 191;
            this.lvlSubledger.Text = "SubLedger";
            // 
            // TxtSubledger
            // 
            this.TxtSubledger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSubledger.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSubledger.Location = new System.Drawing.Point(110, 106);
            this.TxtSubledger.MaxLength = 255;
            this.TxtSubledger.Name = "TxtSubledger";
            this.TxtSubledger.Size = new System.Drawing.Size(395, 25);
            this.TxtSubledger.TabIndex = 8;
            this.TxtSubledger.Text = "\r\n\r\n";
            this.TxtSubledger.Enter += new System.EventHandler(this.txtSubledger_Enter);
            this.TxtSubledger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubledger_KeyDown);
            this.TxtSubledger.Leave += new System.EventHandler(this.txtSubledger_Leave);
            this.TxtSubledger.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubledger_Validating);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(10, 169);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 19);
            this.label9.TabIndex = 188;
            this.label9.Text = "Agent";
            // 
            // TxtAgent
            // 
            this.TxtAgent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAgent.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAgent.Location = new System.Drawing.Point(110, 166);
            this.TxtAgent.MaxLength = 255;
            this.TxtAgent.Name = "TxtAgent";
            this.TxtAgent.Size = new System.Drawing.Size(395, 25);
            this.TxtAgent.TabIndex = 10;
            this.TxtAgent.Enter += new System.EventHandler(this.txtAgent_Enter);
            this.TxtAgent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAgent_KeyDown);
            this.TxtAgent.Leave += new System.EventHandler(this.txtAgent_Leave);
            this.TxtAgent.Validating += new System.ComponentModel.CancelEventHandler(this.txtAgent_Validating);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(406, 319);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 19);
            this.label13.TabIndex = 247;
            this.label13.Text = "Diff.";
            // 
            // LblDifferencAmount
            // 
            this.LblDifferencAmount.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LblDifferencAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblDifferencAmount.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDifferencAmount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblDifferencAmount.Location = new System.Drawing.Point(452, 316);
            this.LblDifferencAmount.Name = "LblDifferencAmount";
            this.LblDifferencAmount.Size = new System.Drawing.Size(83, 24);
            this.LblDifferencAmount.TabIndex = 246;
            this.LblDifferencAmount.Text = "0.00";
            this.LblDifferencAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(187, 319);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 19);
            this.label4.TabIndex = 245;
            this.label4.Text = "Local Credit";
            // 
            // LblLocalCreditAmount
            // 
            this.LblLocalCreditAmount.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LblLocalCreditAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblLocalCreditAmount.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblLocalCreditAmount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblLocalCreditAmount.Location = new System.Drawing.Point(290, 316);
            this.LblLocalCreditAmount.Name = "LblLocalCreditAmount";
            this.LblLocalCreditAmount.Size = new System.Drawing.Size(113, 24);
            this.LblLocalCreditAmount.TabIndex = 244;
            this.LblLocalCreditAmount.Text = "0.00";
            this.LblLocalCreditAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(187, 289);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 19);
            this.label3.TabIndex = 243;
            this.label3.Text = "Local Debit";
            // 
            // LblCreditAmount
            // 
            this.LblCreditAmount.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LblCreditAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblCreditAmount.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCreditAmount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblCreditAmount.Location = new System.Drawing.Point(290, 286);
            this.LblCreditAmount.Name = "LblCreditAmount";
            this.LblCreditAmount.Size = new System.Drawing.Size(113, 24);
            this.LblCreditAmount.TabIndex = 242;
            this.LblCreditAmount.Text = "0.00";
            this.LblCreditAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(10, 319);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(56, 19);
            this.label14.TabIndex = 241;
            this.label14.Text = "Credit";
            // 
            // LblLocalDebitAmount
            // 
            this.LblLocalDebitAmount.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LblLocalDebitAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblLocalDebitAmount.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblLocalDebitAmount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblLocalDebitAmount.Location = new System.Drawing.Point(73, 316);
            this.LblLocalDebitAmount.Name = "LblLocalDebitAmount";
            this.LblLocalDebitAmount.Size = new System.Drawing.Size(111, 24);
            this.LblLocalDebitAmount.TabIndex = 240;
            this.LblLocalDebitAmount.Text = "0.00";
            this.LblLocalDebitAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(10, 289);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(49, 19);
            this.label26.TabIndex = 239;
            this.label26.Text = "Debit";
            // 
            // LblDebitAmount
            // 
            this.LblDebitAmount.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LblDebitAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblDebitAmount.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDebitAmount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblDebitAmount.Location = new System.Drawing.Point(73, 286);
            this.LblDebitAmount.Name = "LblDebitAmount";
            this.LblDebitAmount.Size = new System.Drawing.Size(111, 24);
            this.LblDebitAmount.TabIndex = 238;
            this.LblDebitAmount.Text = "0.00";
            this.LblDebitAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblInWords
            // 
            this.LblInWords.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.LblInWords.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblInWords.ForeColor = System.Drawing.SystemColors.Desktop;
            this.LblInWords.Location = new System.Drawing.Point(92, 351);
            this.LblInWords.Name = "LblInWords";
            this.LblInWords.Size = new System.Drawing.Size(441, 22);
            this.LblInWords.TabIndex = 225;
            this.LblInWords.Text = "Only.";
            // 
            // lbl_NoInWords
            // 
            this.lbl_NoInWords.AutoSize = true;
            this.lbl_NoInWords.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NoInWords.Location = new System.Drawing.Point(10, 349);
            this.lbl_NoInWords.Name = "lbl_NoInWords";
            this.lbl_NoInWords.Size = new System.Drawing.Size(75, 19);
            this.lbl_NoInWords.TabIndex = 224;
            this.lbl_NoInWords.Text = "In Words";
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.BtnCancel.Location = new System.Drawing.Point(422, 401);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(114, 35);
            this.BtnCancel.TabIndex = 17;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(327, 401);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(93, 35);
            this.BtnSave.TabIndex = 16;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // LblLedgerDesc
            // 
            this.LblLedgerDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblLedgerDesc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblLedgerDesc.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblLedgerDesc.Location = new System.Drawing.Point(7, 375);
            this.LblLedgerDesc.Name = "LblLedgerDesc";
            this.LblLedgerDesc.Size = new System.Drawing.Size(528, 23);
            this.LblLedgerDesc.TabIndex = 295;
            // 
            // lblHints
            // 
            this.lblHints.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHints.ForeColor = System.Drawing.Color.Red;
            this.lblHints.Location = new System.Drawing.Point(2, 405);
            this.lblHints.Name = "lblHints";
            this.lblHints.Size = new System.Drawing.Size(52, 22);
            this.lblHints.TabIndex = 286;
            this.lblHints.Text = "Hints.";
            this.lblHints.Visible = false;
            // 
            // MskDate
            // 
            this.MskDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskDate.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskDate.Location = new System.Drawing.Point(430, 76);
            this.MskDate.Mask = "00/00/0000";
            this.MskDate.Name = "MskDate";
            this.MskDate.Size = new System.Drawing.Size(104, 25);
            this.MskDate.TabIndex = 7;
            this.MskDate.ValidatingType = typeof(System.DateTime);
            this.MskDate.Enter += new System.EventHandler(this.dpDate_Enter);
            this.MskDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dpDate_KeyDown);
            this.MskDate.Leave += new System.EventHandler(this.dpDate_Leave);
            this.MskDate.Validating += new System.ComponentModel.CancelEventHandler(this.MskDate_Validating);
            // 
            // MskMiti
            // 
            this.MskMiti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskMiti.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskMiti.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskMiti.Location = new System.Drawing.Point(321, 76);
            this.MskMiti.Mask = "00/00/0000";
            this.MskMiti.Name = "MskMiti";
            this.MskMiti.Size = new System.Drawing.Size(109, 25);
            this.MskMiti.TabIndex = 6;
            this.MskMiti.ValidatingType = typeof(System.DateTime);
            this.MskMiti.Enter += new System.EventHandler(this.mskMiti_Enter);
            this.MskMiti.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mskMiti_KeyDown);
            this.MskMiti.Leave += new System.EventHandler(this.mskMiti_Leave);
            this.MskMiti.Validating += new System.ComponentModel.CancelEventHandler(this.MskMiti_Validating);
            // 
            // btnExit
            // 
            this.btnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.btnExit.Location = new System.Drawing.Point(456, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(82, 33);
            this.btnExit.TabIndex = 3;
            this.btnExit.Tag = "EXIT";
            this.btnExit.Text = "E&XIT";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(168, 4);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(104, 33);
            this.BtnDelete.TabIndex = 2;
            this.BtnDelete.Tag = "DELETE";
            this.BtnDelete.Text = "&DELETE";
            this.BtnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnEdit.Appearance.Options.UseFont = true;
            this.BtnEdit.ImageOptions.Image = global::MrBLL.Properties.Resources.Edit;
            this.BtnEdit.Location = new System.Drawing.Point(87, 4);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(82, 33);
            this.BtnEdit.TabIndex = 1;
            this.BtnEdit.Text = "&EDIT";
            this.BtnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // BtnNew
            // 
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnNew.Location = new System.Drawing.Point(4, 4);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(83, 33);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Tag = "SAVE";
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // BtnVNo
            // 
            this.BtnVNo.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnVNo.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnVNo.Location = new System.Drawing.Point(246, 75);
            this.BtnVNo.Name = "BtnVNo";
            this.BtnVNo.Size = new System.Drawing.Size(28, 26);
            this.BtnVNo.TabIndex = 296;
            this.BtnVNo.UseVisualStyleBackColor = false;
            this.BtnVNo.Click += new System.EventHandler(this.btnVno_Click);
            // 
            // CmbModule
            // 
            this.CmbModule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbModule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbModule.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbModule.FormattingEnabled = true;
            this.CmbModule.Location = new System.Drawing.Point(110, 49);
            this.CmbModule.Name = "CmbModule";
            this.CmbModule.Size = new System.Drawing.Size(423, 27);
            this.CmbModule.TabIndex = 4;
            this.CmbModule.SelectedIndexChanged += new System.EventHandler(this.CbModule_SelectedIndexChanged);
            this.CmbModule.Enter += new System.EventHandler(this.CbModule_Enter);
            this.CmbModule.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CbModule_KeyDown);
            this.CmbModule.Leave += new System.EventHandler(this.CbModule_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 19);
            this.label2.TabIndex = 215;
            this.label2.Text = "Module";
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.GTxtSearch);
            this.PanelHeader.Controls.Add(this.DGrid);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(680, 438);
            this.PanelHeader.TabIndex = 320;
            // 
            // GTxtSearch
            // 
            this.GTxtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GTxtSearch.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.GTxtSearch.Location = new System.Drawing.Point(0, 413);
            this.GTxtSearch.Name = "GTxtSearch";
            this.GTxtSearch.Size = new System.Drawing.Size(680, 25);
            this.GTxtSearch.TabIndex = 11;
            this.GTxtSearch.Visible = false;
            this.GTxtSearch.TextChanged += new System.EventHandler(this.GTxtSearch_TextChanged);
            // 
            // btnNarration
            // 
            this.btnNarration.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnNarration.Controls.Add(this.BtnCurrency);
            this.btnNarration.Controls.Add(this.TxtCurrency);
            this.btnNarration.Controls.Add(this.Btn_Narration);
            this.btnNarration.Controls.Add(this.clsSeparator3);
            this.btnNarration.Controls.Add(this.clsSeparator2);
            this.btnNarration.Controls.Add(this.clsSeparator1);
            this.btnNarration.Controls.Add(this.BtnCancel);
            this.btnNarration.Controls.Add(this.label13);
            this.btnNarration.Controls.Add(this.BtnSave);
            this.btnNarration.Controls.Add(this.BtnAgent);
            this.btnNarration.Controls.Add(this.LblLedgerDesc);
            this.btnNarration.Controls.Add(this.LblDifferencAmount);
            this.btnNarration.Controls.Add(this.lblHints);
            this.btnNarration.Controls.Add(this.MskDate);
            this.btnNarration.Controls.Add(this.label4);
            this.btnNarration.Controls.Add(this.BtnDepartment);
            this.btnNarration.Controls.Add(this.LblLocalCreditAmount);
            this.btnNarration.Controls.Add(this.label3);
            this.btnNarration.Controls.Add(this.BtnSubledger);
            this.btnNarration.Controls.Add(this.LblCreditAmount);
            this.btnNarration.Controls.Add(this.MskMiti);
            this.btnNarration.Controls.Add(this.label14);
            this.btnNarration.Controls.Add(this.LblLocalDebitAmount);
            this.btnNarration.Controls.Add(this.panel3);
            this.btnNarration.Controls.Add(this.label26);
            this.btnNarration.Controls.Add(this.LblDebitAmount);
            this.btnNarration.Controls.Add(this.TxtCurrencyRate);
            this.btnNarration.Controls.Add(this.LblInWords);
            this.btnNarration.Controls.Add(this.BtnVNo);
            this.btnNarration.Controls.Add(this.lbl_NoInWords);
            this.btnNarration.Controls.Add(this.lbl_CurrencyRate);
            this.btnNarration.Controls.Add(this.btnExit);
            this.btnNarration.Controls.Add(this.lbl_Currency);
            this.btnNarration.Controls.Add(this.CmbModule);
            this.btnNarration.Controls.Add(this.label12);
            this.btnNarration.Controls.Add(this.label2);
            this.btnNarration.Controls.Add(this.label11);
            this.btnNarration.Controls.Add(this.label8);
            this.btnNarration.Controls.Add(this.label1);
            this.btnNarration.Controls.Add(this.TxtNarration);
            this.btnNarration.Controls.Add(this.BtnDelete);
            this.btnNarration.Controls.Add(this.TxtCreditAmount);
            this.btnNarration.Controls.Add(this.lbl_VoucherNo);
            this.btnNarration.Controls.Add(this.TxtDebitAmount);
            this.btnNarration.Controls.Add(this.BtnEdit);
            this.btnNarration.Controls.Add(this.TxtDepartment);
            this.btnNarration.Controls.Add(this.TxtVno);
            this.btnNarration.Controls.Add(this.label7);
            this.btnNarration.Controls.Add(this.lvlSubledger);
            this.btnNarration.Controls.Add(this.TxtSubledger);
            this.btnNarration.Controls.Add(this.BtnNew);
            this.btnNarration.Controls.Add(this.label9);
            this.btnNarration.Controls.Add(this.TxtAgent);
            this.btnNarration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNarration.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNarration.Location = new System.Drawing.Point(680, 0);
            this.btnNarration.Name = "btnNarration";
            this.btnNarration.Size = new System.Drawing.Size(551, 438);
            this.btnNarration.TabIndex = 0;
            // 
            // BtnCurrency
            // 
            this.BtnCurrency.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnCurrency.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnCurrency.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnCurrency.Location = new System.Drawing.Point(274, 196);
            this.BtnCurrency.Name = "BtnCurrency";
            this.BtnCurrency.Size = new System.Drawing.Size(28, 24);
            this.BtnCurrency.TabIndex = 340;
            this.BtnCurrency.TabStop = false;
            this.BtnCurrency.UseVisualStyleBackColor = false;
            this.BtnCurrency.Click += new System.EventHandler(this.BtnCurrency_Click);
            // 
            // TxtCurrency
            // 
            this.TxtCurrency.BackColor = System.Drawing.Color.White;
            this.TxtCurrency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCurrency.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtCurrency.Location = new System.Drawing.Point(110, 196);
            this.TxtCurrency.MaxLength = 50;
            this.TxtCurrency.Name = "TxtCurrency";
            this.TxtCurrency.ReadOnly = true;
            this.TxtCurrency.Size = new System.Drawing.Size(162, 25);
            this.TxtCurrency.TabIndex = 11;
            this.TxtCurrency.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCurrency_KeyDown);
            this.TxtCurrency.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalEnter_KeyPress);
            this.TxtCurrency.Leave += new System.EventHandler(this.TxtCurrency_Leave);
            this.TxtCurrency.Validating += new System.ComponentModel.CancelEventHandler(this.TxtCurrency_Validating);
            // 
            // Btn_Narration
            // 
            this.Btn_Narration.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Narration.ForeColor = System.Drawing.SystemColors.Window;
            this.Btn_Narration.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Narration.Image")));
            this.Btn_Narration.Location = new System.Drawing.Point(510, 255);
            this.Btn_Narration.Name = "Btn_Narration";
            this.Btn_Narration.Size = new System.Drawing.Size(32, 25);
            this.Btn_Narration.TabIndex = 327;
            this.Btn_Narration.UseVisualStyleBackColor = true;
            this.Btn_Narration.Click += new System.EventHandler(this.Btn_Narration_Click);
            // 
            // clsSeparator3
            // 
            this.clsSeparator3.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator3.Location = new System.Drawing.Point(6, 282);
            this.clsSeparator3.Margin = new System.Windows.Forms.Padding(2);
            this.clsSeparator3.Name = "clsSeparator3";
            this.clsSeparator3.Padding = new System.Windows.Forms.Padding(2);
            this.clsSeparator3.Size = new System.Drawing.Size(534, 2);
            this.clsSeparator3.TabIndex = 326;
            this.clsSeparator3.TabStop = false;
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(2, 102);
            this.clsSeparator2.Margin = new System.Windows.Forms.Padding(2);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Padding = new System.Windows.Forms.Padding(2);
            this.clsSeparator2.Size = new System.Drawing.Size(534, 2);
            this.clsSeparator2.TabIndex = 325;
            this.clsSeparator2.TabStop = false;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(5, 39);
            this.clsSeparator1.Margin = new System.Windows.Forms.Padding(2);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Padding = new System.Windows.Forms.Padding(2);
            this.clsSeparator1.Size = new System.Drawing.Size(534, 2);
            this.clsSeparator1.TabIndex = 324;
            this.clsSeparator1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(548, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(3, 438);
            this.panel3.TabIndex = 9;
            // 
            // FrmLedgerOpeningBillWiseEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1231, 438);
            this.Controls.Add(this.btnNarration);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLedgerOpeningBillWiseEntry";
            this.ShowIcon = false;
            this.Tag = "Edit";
            this.Text = "LEDGER OPENING DETAILS";
            this.Load += new System.EventHandler(this.BillWiseLedgerOpening_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BillWiseLedgerOpening_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).EndInit();
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            this.btnNarration.ResumeLayout(false);
            this.btnNarration.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGrid;
        private System.Windows.Forms.Label lbl_VoucherNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lvlSubledger;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbl_CurrencyRate;
        private System.Windows.Forms.Label lbl_Currency;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label LblDifferencAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label LblLocalCreditAmount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LblCreditAmount;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label LblLocalDebitAmount;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label LblDebitAmount;
        private System.Windows.Forms.Label LblInWords;
        private System.Windows.Forms.Label lbl_NoInWords;
        private System.Windows.Forms.Label lblHints;
        private System.Windows.Forms.Label LblLedgerDesc;
        private System.Windows.Forms.ComboBox CmbModule;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnSubledger;
        private System.Windows.Forms.Button BtnAgent;
        private System.Windows.Forms.Button BtnDepartment;
        private System.Windows.Forms.Button BtnVNo;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private ClsSeparator clsSeparator1;
        private ClsSeparator clsSeparator2;
        private ClsSeparator clsSeparator3;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtSno;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtLedgerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtType;
        private System.Windows.Forms.Button Btn_Narration;
        private System.Windows.Forms.Button BtnCurrency;
        private MrTextBox TxtVno;
        private MrTextBox TxtNarration;
        private MrTextBox TxtCreditAmount;
        private MrTextBox TxtDebitAmount;
        private MrTextBox TxtDepartment;
        private MrTextBox TxtSubledger;
        private MrTextBox TxtAgent;
        private MrTextBox TxtCurrencyRate;
        private MrMaskedTextBox MskDate;
        private MrMaskedTextBox MskMiti;
        private MrPanel PanelHeader;
        private MrPanel btnNarration;
        private MrPanel panel3;
        private MrTextBox GTxtSearch;
        private MrTextBox TxtCurrency;
    }
}