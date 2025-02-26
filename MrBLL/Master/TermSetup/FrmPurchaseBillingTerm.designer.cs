using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Master.TermSetup
{
    partial class FrmPurchaseBillingTerm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPurchaseBillingTerm));
            this.ChkActive = new System.Windows.Forms.CheckBox();
            this.TxtRate = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblBTInvoiceRate = new System.Windows.Forms.Label();
            this.CmbCondition = new System.Windows.Forms.ComboBox();
            this.lblBTInvoiceCondition = new System.Windows.Forms.Label();
            this.CmbSign = new System.Windows.Forms.ComboBox();
            this.lblBTInvoiceSign = new System.Windows.Forms.Label();
            this.CmbBasis = new System.Windows.Forms.ComboBox();
            this.lblBTInvoiceBasis = new System.Windows.Forms.Label();
            this.CmbType = new System.Windows.Forms.ComboBox();
            this.lblBTInvoiceType = new System.Windows.Forms.Label();
            this.TxtLedger = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblBTInvoiceGl = new System.Windows.Forms.Label();
            this.CmbModule = new System.Windows.Forms.ComboBox();
            this.TxtDescription = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtCode = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblBTInvoiceDesc = new System.Windows.Forms.Label();
            this.lblBTInvoiceModule = new System.Windows.Forms.Label();
            this.lblBTInvoiceTermSno = new System.Windows.Forms.Label();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.StorePanel = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.ChkSupress = new System.Windows.Forms.CheckBox();
            this.ChkStockValuation = new System.Windows.Forms.CheckBox();
            this.BtnView = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCode = new System.Windows.Forms.Button();
            this.BtnLedger = new System.Windows.Forms.Button();
            this.clsSeparator3 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.StorePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChkActive
            // 
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ChkActive.Location = new System.Drawing.Point(11, 263);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkActive.Size = new System.Drawing.Size(103, 24);
            this.ChkActive.TabIndex = 18;
            this.ChkActive.Text = "Active";
            this.ChkActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkActive.UseVisualStyleBackColor = true;
            // 
            // TxtRate
            // 
            this.TxtRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtRate.Location = new System.Drawing.Point(114, 221);
            this.TxtRate.Name = "TxtRate";
            this.TxtRate.Size = new System.Drawing.Size(235, 25);
            this.TxtRate.TabIndex = 13;
            this.TxtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtRate_KeyPress);
            this.TxtRate.Validating += new System.ComponentModel.CancelEventHandler(this.TxtRate_Validating);
            // 
            // lblBTInvoiceRate
            // 
            this.lblBTInvoiceRate.AutoSize = true;
            this.lblBTInvoiceRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBTInvoiceRate.Location = new System.Drawing.Point(6, 228);
            this.lblBTInvoiceRate.Name = "lblBTInvoiceRate";
            this.lblBTInvoiceRate.Size = new System.Drawing.Size(88, 19);
            this.lblBTInvoiceRate.TabIndex = 10;
            this.lblBTInvoiceRate.Text = "Term Rate";
            // 
            // CmbCondition
            // 
            this.CmbCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbCondition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbCondition.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbCondition.FormattingEnabled = true;
            this.CmbCondition.Items.AddRange(new object[] {
            "BillWise",
            "ProductWise"});
            this.CmbCondition.Location = new System.Drawing.Point(114, 162);
            this.CmbCondition.Name = "CmbCondition";
            this.CmbCondition.Size = new System.Drawing.Size(237, 27);
            this.CmbCondition.TabIndex = 11;
            this.CmbCondition.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbCondition_KeyDown);
            // 
            // lblBTInvoiceCondition
            // 
            this.lblBTInvoiceCondition.AutoSize = true;
            this.lblBTInvoiceCondition.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBTInvoiceCondition.Location = new System.Drawing.Point(6, 168);
            this.lblBTInvoiceCondition.Name = "lblBTInvoiceCondition";
            this.lblBTInvoiceCondition.Size = new System.Drawing.Size(81, 19);
            this.lblBTInvoiceCondition.TabIndex = 8;
            this.lblBTInvoiceCondition.Text = "Condition";
            // 
            // CmbSign
            // 
            this.CmbSign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbSign.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbSign.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbSign.FormattingEnabled = true;
            this.CmbSign.Items.AddRange(new object[] {
            "+",
            "-"});
            this.CmbSign.Location = new System.Drawing.Point(409, 103);
            this.CmbSign.Name = "CmbSign";
            this.CmbSign.Size = new System.Drawing.Size(170, 27);
            this.CmbSign.TabIndex = 9;
            this.CmbSign.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbSign_KeyDown);
            // 
            // lblBTInvoiceSign
            // 
            this.lblBTInvoiceSign.AutoSize = true;
            this.lblBTInvoiceSign.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBTInvoiceSign.Location = new System.Drawing.Point(360, 107);
            this.lblBTInvoiceSign.Name = "lblBTInvoiceSign";
            this.lblBTInvoiceSign.Size = new System.Drawing.Size(47, 19);
            this.lblBTInvoiceSign.TabIndex = 7;
            this.lblBTInvoiceSign.Text = "Sign ";
            // 
            // CmbBasis
            // 
            this.CmbBasis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbBasis.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbBasis.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbBasis.FormattingEnabled = true;
            this.CmbBasis.Items.AddRange(new object[] {
            "Value",
            "Quantity"});
            this.CmbBasis.Location = new System.Drawing.Point(114, 192);
            this.CmbBasis.Name = "CmbBasis";
            this.CmbBasis.Size = new System.Drawing.Size(236, 27);
            this.CmbBasis.TabIndex = 12;
            this.CmbBasis.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbBasis_KeyDown);
            // 
            // lblBTInvoiceBasis
            // 
            this.lblBTInvoiceBasis.AutoSize = true;
            this.lblBTInvoiceBasis.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBTInvoiceBasis.Location = new System.Drawing.Point(6, 198);
            this.lblBTInvoiceBasis.Name = "lblBTInvoiceBasis";
            this.lblBTInvoiceBasis.Size = new System.Drawing.Size(50, 19);
            this.lblBTInvoiceBasis.TabIndex = 5;
            this.lblBTInvoiceBasis.Text = "Basis";
            // 
            // CmbType
            // 
            this.CmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbType.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbType.FormattingEnabled = true;
            this.CmbType.Items.AddRange(new object[] {
            "General",
            "Additional",
            "Round Off"});
            this.CmbType.Location = new System.Drawing.Point(409, 45);
            this.CmbType.Name = "CmbType";
            this.CmbType.Size = new System.Drawing.Size(170, 27);
            this.CmbType.TabIndex = 6;
            this.CmbType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbType_KeyDown);
            // 
            // lblBTInvoiceType
            // 
            this.lblBTInvoiceType.AutoSize = true;
            this.lblBTInvoiceType.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBTInvoiceType.Location = new System.Drawing.Point(362, 48);
            this.lblBTInvoiceType.Name = "lblBTInvoiceType";
            this.lblBTInvoiceType.Size = new System.Drawing.Size(43, 19);
            this.lblBTInvoiceType.TabIndex = 5;
            this.lblBTInvoiceType.Text = "Type";
            // 
            // TxtLedger
            // 
            this.TxtLedger.BackColor = System.Drawing.Color.White;
            this.TxtLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtLedger.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtLedger.Location = new System.Drawing.Point(114, 133);
            this.TxtLedger.Name = "TxtLedger";
            this.TxtLedger.ReadOnly = true;
            this.TxtLedger.Size = new System.Drawing.Size(431, 25);
            this.TxtLedger.TabIndex = 10;
            this.TxtLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtLedger_KeyDown);
            this.TxtLedger.Leave += new System.EventHandler(this.TxtLedger_Leave);
            this.TxtLedger.Validating += new System.ComponentModel.CancelEventHandler(this.TxtLedger_Validating);
            // 
            // lblBTInvoiceGl
            // 
            this.lblBTInvoiceGl.AutoSize = true;
            this.lblBTInvoiceGl.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBTInvoiceGl.Location = new System.Drawing.Point(6, 136);
            this.lblBTInvoiceGl.Name = "lblBTInvoiceGl";
            this.lblBTInvoiceGl.Size = new System.Drawing.Size(60, 19);
            this.lblBTInvoiceGl.TabIndex = 3;
            this.lblBTInvoiceGl.Text = "Ledger";
            // 
            // CmbModule
            // 
            this.CmbModule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbModule.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbModule.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbModule.FormattingEnabled = true;
            this.CmbModule.Items.AddRange(new object[] {
            "Sales",
            "Sales Return"});
            this.CmbModule.Location = new System.Drawing.Point(116, 103);
            this.CmbModule.Name = "CmbModule";
            this.CmbModule.Size = new System.Drawing.Size(235, 27);
            this.CmbModule.TabIndex = 8;
            this.CmbModule.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbModule_KeyDown);
            // 
            // TxtDescription
            // 
            this.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDescription.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDescription.Location = new System.Drawing.Point(114, 75);
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(466, 25);
            this.TxtDescription.TabIndex = 7;
            this.TxtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDescription_KeyDown);
            this.TxtDescription.Leave += new System.EventHandler(this.TxtDescription_Leave);
            this.TxtDescription.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDescription_Validating);
            // 
            // TxtCode
            // 
            this.TxtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCode.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCode.Location = new System.Drawing.Point(114, 47);
            this.TxtCode.MaxLength = 2;
            this.TxtCode.Name = "TxtCode";
            this.TxtCode.Size = new System.Drawing.Size(147, 25);
            this.TxtCode.TabIndex = 5;
            this.TxtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCode_KeyDown);
            this.TxtCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCode_KeyPress);
            this.TxtCode.Leave += new System.EventHandler(this.TxtCode_Leave);
            this.TxtCode.Validating += new System.ComponentModel.CancelEventHandler(this.TxtCode_Validating);
            // 
            // lblBTInvoiceDesc
            // 
            this.lblBTInvoiceDesc.AutoSize = true;
            this.lblBTInvoiceDesc.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBTInvoiceDesc.Location = new System.Drawing.Point(6, 78);
            this.lblBTInvoiceDesc.Name = "lblBTInvoiceDesc";
            this.lblBTInvoiceDesc.Size = new System.Drawing.Size(95, 19);
            this.lblBTInvoiceDesc.TabIndex = 0;
            this.lblBTInvoiceDesc.Text = "Description";
            // 
            // lblBTInvoiceModule
            // 
            this.lblBTInvoiceModule.AutoSize = true;
            this.lblBTInvoiceModule.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBTInvoiceModule.Location = new System.Drawing.Point(6, 107);
            this.lblBTInvoiceModule.Name = "lblBTInvoiceModule";
            this.lblBTInvoiceModule.Size = new System.Drawing.Size(63, 19);
            this.lblBTInvoiceModule.TabIndex = 0;
            this.lblBTInvoiceModule.Text = "Module";
            // 
            // lblBTInvoiceTermSno
            // 
            this.lblBTInvoiceTermSno.AutoSize = true;
            this.lblBTInvoiceTermSno.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBTInvoiceTermSno.Location = new System.Drawing.Point(6, 50);
            this.lblBTInvoiceTermSno.Name = "lblBTInvoiceTermSno";
            this.lblBTInvoiceTermSno.Size = new System.Drawing.Size(89, 19);
            this.lblBTInvoiceTermSno.TabIndex = 0;
            this.lblBTInvoiceTermSno.Text = "Sno (Code)";
            // 
            // BtnNew
            // 
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNew.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.Appearance.Options.UseForeColor = true;
            this.BtnNew.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnNew.Location = new System.Drawing.Point(4, 3);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(73, 33);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Tag = "NewButtonCheck";
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.Appearance.Options.UseForeColor = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(156, 3);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(100, 33);
            this.BtnDelete.TabIndex = 2;
            this.BtnDelete.Tag = "DeleteButtonCheck";
            this.BtnDelete.Text = "&DELETE";
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEdit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnEdit.Appearance.Options.UseFont = true;
            this.BtnEdit.Appearance.Options.UseForeColor = true;
            this.BtnEdit.ImageOptions.Image = global::MrBLL.Properties.Resources.Edit;
            this.BtnEdit.Location = new System.Drawing.Point(78, 3);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(76, 33);
            this.BtnEdit.TabIndex = 1;
            this.BtnEdit.Tag = "EditButtonCheck";
            this.BtnEdit.Text = "&EDIT";
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseForeColor = true;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.BtnExit.Location = new System.Drawing.Point(503, 3);
            this.BtnExit.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(77, 33);
            this.BtnExit.TabIndex = 4;
            this.BtnExit.Text = "E&XIT";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.Appearance.Options.UseForeColor = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.BtnCancel.Location = new System.Drawing.Point(475, 259);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(104, 33);
            this.BtnCancel.TabIndex = 17;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Appearance.Options.UseForeColor = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(386, 259);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(89, 33);
            this.BtnSave.TabIndex = 16;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.ChkSupress);
            this.StorePanel.Controls.Add(this.ChkStockValuation);
            this.StorePanel.Controls.Add(this.BtnView);
            this.StorePanel.Controls.Add(this.BtnCode);
            this.StorePanel.Controls.Add(this.BtnLedger);
            this.StorePanel.Controls.Add(this.ChkActive);
            this.StorePanel.Controls.Add(this.clsSeparator3);
            this.StorePanel.Controls.Add(this.BtnCancel);
            this.StorePanel.Controls.Add(this.clsSeparator1);
            this.StorePanel.Controls.Add(this.TxtRate);
            this.StorePanel.Controls.Add(this.BtnSave);
            this.StorePanel.Controls.Add(this.lblBTInvoiceRate);
            this.StorePanel.Controls.Add(this.BtnDelete);
            this.StorePanel.Controls.Add(this.CmbCondition);
            this.StorePanel.Controls.Add(this.BtnEdit);
            this.StorePanel.Controls.Add(this.lblBTInvoiceCondition);
            this.StorePanel.Controls.Add(this.BtnNew);
            this.StorePanel.Controls.Add(this.CmbSign);
            this.StorePanel.Controls.Add(this.BtnExit);
            this.StorePanel.Controls.Add(this.lblBTInvoiceSign);
            this.StorePanel.Controls.Add(this.CmbBasis);
            this.StorePanel.Controls.Add(this.lblBTInvoiceBasis);
            this.StorePanel.Controls.Add(this.CmbType);
            this.StorePanel.Controls.Add(this.lblBTInvoiceType);
            this.StorePanel.Controls.Add(this.lblBTInvoiceTermSno);
            this.StorePanel.Controls.Add(this.TxtLedger);
            this.StorePanel.Controls.Add(this.lblBTInvoiceModule);
            this.StorePanel.Controls.Add(this.lblBTInvoiceGl);
            this.StorePanel.Controls.Add(this.lblBTInvoiceDesc);
            this.StorePanel.Controls.Add(this.CmbModule);
            this.StorePanel.Controls.Add(this.TxtCode);
            this.StorePanel.Controls.Add(this.TxtDescription);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(584, 298);
            this.StorePanel.TabIndex = 0;
            // 
            // ChkSupress
            // 
            this.ChkSupress.AutoSize = true;
            this.ChkSupress.Location = new System.Drawing.Point(375, 194);
            this.ChkSupress.Name = "ChkSupress";
            this.ChkSupress.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSupress.Size = new System.Drawing.Size(142, 23);
            this.ChkSupress.TabIndex = 15;
            this.ChkSupress.Text = "Supress if Zero";
            this.ChkSupress.UseVisualStyleBackColor = true;
            // 
            // ChkStockValuation
            // 
            this.ChkStockValuation.AutoSize = true;
            this.ChkStockValuation.Location = new System.Drawing.Point(375, 168);
            this.ChkStockValuation.Name = "ChkStockValuation";
            this.ChkStockValuation.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkStockValuation.Size = new System.Drawing.Size(147, 23);
            this.ChkStockValuation.TabIndex = 14;
            this.ChkStockValuation.Text = "Stock Valuation";
            this.ChkStockValuation.UseVisualStyleBackColor = true;
            // 
            // BtnView
            // 
            this.BtnView.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnView.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnView.Appearance.Options.UseFont = true;
            this.BtnView.Appearance.Options.UseForeColor = true;
            this.BtnView.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnView.Location = new System.Drawing.Point(257, 3);
            this.BtnView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnView.Name = "BtnView";
            this.BtnView.Size = new System.Drawing.Size(78, 33);
            this.BtnView.TabIndex = 3;
            this.BtnView.Tag = "ViewButtonCheck";
            this.BtnView.Text = "&VIEW";
            this.BtnView.Click += new System.EventHandler(this.BtnView_Click);
            // 
            // BtnCode
            // 
            this.BtnCode.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnCode.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnCode.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnCode.Location = new System.Drawing.Point(262, 47);
            this.BtnCode.Name = "BtnCode";
            this.BtnCode.Size = new System.Drawing.Size(25, 25);
            this.BtnCode.TabIndex = 18;
            this.BtnCode.TabStop = false;
            this.BtnCode.UseVisualStyleBackColor = false;
            this.BtnCode.Click += new System.EventHandler(this.BtnCode_Click);
            // 
            // BtnLedger
            // 
            this.BtnLedger.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnLedger.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnLedger.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnLedger.Location = new System.Drawing.Point(554, 133);
            this.BtnLedger.Name = "BtnLedger";
            this.BtnLedger.Size = new System.Drawing.Size(25, 25);
            this.BtnLedger.TabIndex = 18;
            this.BtnLedger.TabStop = false;
            this.BtnLedger.UseVisualStyleBackColor = false;
            this.BtnLedger.Click += new System.EventHandler(this.BtnLedger_Click);
            // 
            // clsSeparator3
            // 
            this.clsSeparator3.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator3.Location = new System.Drawing.Point(4, 249);
            this.clsSeparator3.Name = "clsSeparator3";
            this.clsSeparator3.Size = new System.Drawing.Size(576, 2);
            this.clsSeparator3.TabIndex = 12;
            this.clsSeparator3.TabStop = false;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(7, 40);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(577, 2);
            this.clsSeparator1.TabIndex = 11;
            this.clsSeparator1.TabStop = false;
            // 
            // FrmPurchaseBillingTerm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(584, 298);
            this.Controls.Add(this.StorePanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "FrmPurchaseBillingTerm";
            this.ShowIcon = false;
            this.Tag = "Purchase Term";
            this.Text = "Purchase Term Details";
            this.Load += new System.EventHandler(this.FrmPurchaseTerm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmPurchaseTerm_KeyPress);
            this.StorePanel.ResumeLayout(false);
            this.StorePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox ChkActive;
        private System.Windows.Forms.Label lblBTInvoiceRate;
        private System.Windows.Forms.ComboBox CmbCondition;
        private System.Windows.Forms.Label lblBTInvoiceCondition;
        private System.Windows.Forms.ComboBox CmbSign;
        private System.Windows.Forms.Label lblBTInvoiceSign;
        private System.Windows.Forms.ComboBox CmbBasis;
        private System.Windows.Forms.Label lblBTInvoiceBasis;
        private System.Windows.Forms.ComboBox CmbType;
        private System.Windows.Forms.Label lblBTInvoiceType;
        private System.Windows.Forms.Label lblBTInvoiceGl;
        private System.Windows.Forms.ComboBox CmbModule;
        private System.Windows.Forms.Label lblBTInvoiceDesc;
        private System.Windows.Forms.Label lblBTInvoiceModule;
        private System.Windows.Forms.Label lblBTInvoiceTermSno;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
		private ClsSeparator clsSeparator1;
		private ClsSeparator clsSeparator3;
        private System.Windows.Forms.Button BtnLedger;
        private System.Windows.Forms.Button BtnCode;
		private DevExpress.XtraEditors.SimpleButton BtnView;
        private MrTextBox TxtRate;
        private MrTextBox TxtLedger;
        private MrTextBox TxtDescription;
        private MrTextBox TxtCode;
        private MrPanel StorePanel;
        private System.Windows.Forms.CheckBox ChkSupress;
        private System.Windows.Forms.CheckBox ChkStockValuation;
    }
}