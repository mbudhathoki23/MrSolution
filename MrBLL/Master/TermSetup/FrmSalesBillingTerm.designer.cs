using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Master.TermSetup
{
    partial class FrmSalesBillingTerm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSalesBillingTerm));
            this.ChkActive = new System.Windows.Forms.CheckBox();
            this.ChkProfitability = new System.Windows.Forms.CheckBox();
            this.ChkSupress = new System.Windows.Forms.CheckBox();
            this.lblBTInvoiceRate = new System.Windows.Forms.Label();
            this.CmbCondition = new System.Windows.Forms.ComboBox();
            this.lblBTInvoiceCondition = new System.Windows.Forms.Label();
            this.CmbSign = new System.Windows.Forms.ComboBox();
            this.lblBTInvoiceSign = new System.Windows.Forms.Label();
            this.CmbBasis = new System.Windows.Forms.ComboBox();
            this.lblBTInvoiceBasis = new System.Windows.Forms.Label();
            this.CmbType = new System.Windows.Forms.ComboBox();
            this.lblBTInvoiceType = new System.Windows.Forms.Label();
            this.lblBTInvoiceGl = new System.Windows.Forms.Label();
            this.CmbModule = new System.Windows.Forms.ComboBox();
            this.lblBTInvoiceDesc = new System.Windows.Forms.Label();
            this.lblBTInvoiceModule = new System.Windows.Forms.Label();
            this.lblBTInvoiceTermSno = new System.Windows.Forms.Label();
            this.StorePanel = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.BtnView = new DevExpress.XtraEditors.SimpleButton();
            this.BtnLedger = new System.Windows.Forms.Button();
            this.BtnCode = new System.Windows.Forms.Button();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.TxtRate = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.TxtCode = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtDescription = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtLedger = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.StorePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChkActive
            // 
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkActive.Location = new System.Drawing.Point(3, 261);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkActive.Size = new System.Drawing.Size(73, 25);
            this.ChkActive.TabIndex = 14;
            this.ChkActive.Text = "Active";
            this.ChkActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkActive.UseVisualStyleBackColor = true;
            // 
            // ChkProfitability
            // 
            this.ChkProfitability.AutoEllipsis = true;
            this.ChkProfitability.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkProfitability.Checked = true;
            this.ChkProfitability.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkProfitability.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkProfitability.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.ChkProfitability.Location = new System.Drawing.Point(358, 166);
            this.ChkProfitability.Name = "ChkProfitability";
            this.ChkProfitability.Size = new System.Drawing.Size(142, 25);
            this.ChkProfitability.TabIndex = 14;
            this.ChkProfitability.Text = "Profitability";
            this.ChkProfitability.UseVisualStyleBackColor = true;
            // 
            // ChkSupress
            // 
            this.ChkSupress.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSupress.Checked = true;
            this.ChkSupress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkSupress.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSupress.Location = new System.Drawing.Point(358, 196);
            this.ChkSupress.Name = "ChkSupress";
            this.ChkSupress.Size = new System.Drawing.Size(142, 25);
            this.ChkSupress.TabIndex = 15;
            this.ChkSupress.Text = "Supress if Zero";
            this.ChkSupress.UseVisualStyleBackColor = true;
            // 
            // lblBTInvoiceRate
            // 
            this.lblBTInvoiceRate.AutoSize = true;
            this.lblBTInvoiceRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBTInvoiceRate.Location = new System.Drawing.Point(12, 226);
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
            this.CmbCondition.Location = new System.Drawing.Point(116, 191);
            this.CmbCondition.Name = "CmbCondition";
            this.CmbCondition.Size = new System.Drawing.Size(234, 27);
            this.CmbCondition.TabIndex = 12;
            this.CmbCondition.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbCondition_KeyPress);
            // 
            // lblBTInvoiceCondition
            // 
            this.lblBTInvoiceCondition.AutoSize = true;
            this.lblBTInvoiceCondition.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBTInvoiceCondition.Location = new System.Drawing.Point(12, 195);
            this.lblBTInvoiceCondition.Name = "lblBTInvoiceCondition";
            this.lblBTInvoiceCondition.Size = new System.Drawing.Size(81, 19);
            this.lblBTInvoiceCondition.TabIndex = 8;
            this.lblBTInvoiceCondition.Text = "Condition";
            // 
            // CmbSign
            // 
            this.CmbSign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbSign.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbSign.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbSign.FormattingEnabled = true;
            this.CmbSign.Items.AddRange(new object[] {
            "+",
            "-"});
            this.CmbSign.Location = new System.Drawing.Point(411, 103);
            this.CmbSign.Name = "CmbSign";
            this.CmbSign.Size = new System.Drawing.Size(164, 27);
            this.CmbSign.TabIndex = 9;
            this.CmbSign.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbSign_KeyPress);
            // 
            // lblBTInvoiceSign
            // 
            this.lblBTInvoiceSign.AutoSize = true;
            this.lblBTInvoiceSign.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBTInvoiceSign.Location = new System.Drawing.Point(363, 107);
            this.lblBTInvoiceSign.Name = "lblBTInvoiceSign";
            this.lblBTInvoiceSign.Size = new System.Drawing.Size(47, 19);
            this.lblBTInvoiceSign.TabIndex = 7;
            this.lblBTInvoiceSign.Text = "Sign ";
            // 
            // CmbBasis
            // 
            this.CmbBasis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbBasis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbBasis.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbBasis.FormattingEnabled = true;
            this.CmbBasis.Items.AddRange(new object[] {
            "Value",
            "Quantity"});
            this.CmbBasis.Location = new System.Drawing.Point(116, 161);
            this.CmbBasis.Name = "CmbBasis";
            this.CmbBasis.Size = new System.Drawing.Size(234, 27);
            this.CmbBasis.TabIndex = 11;
            this.CmbBasis.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbBasis_KeyPress);
            // 
            // lblBTInvoiceBasis
            // 
            this.lblBTInvoiceBasis.AutoSize = true;
            this.lblBTInvoiceBasis.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBTInvoiceBasis.Location = new System.Drawing.Point(12, 166);
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
            this.CmbType.Location = new System.Drawing.Point(411, 45);
            this.CmbType.Name = "CmbType";
            this.CmbType.Size = new System.Drawing.Size(167, 27);
            this.CmbType.TabIndex = 6;
            this.CmbType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbType_KeyDown);
            this.CmbType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Cmb_TypeKeyPress);
            // 
            // lblBTInvoiceType
            // 
            this.lblBTInvoiceType.AutoSize = true;
            this.lblBTInvoiceType.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBTInvoiceType.Location = new System.Drawing.Point(363, 48);
            this.lblBTInvoiceType.Name = "lblBTInvoiceType";
            this.lblBTInvoiceType.Size = new System.Drawing.Size(43, 19);
            this.lblBTInvoiceType.TabIndex = 5;
            this.lblBTInvoiceType.Text = "Type";
            // 
            // lblBTInvoiceGl
            // 
            this.lblBTInvoiceGl.AutoSize = true;
            this.lblBTInvoiceGl.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBTInvoiceGl.Location = new System.Drawing.Point(12, 137);
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
            this.CmbModule.Location = new System.Drawing.Point(116, 103);
            this.CmbModule.Name = "CmbModule";
            this.CmbModule.Size = new System.Drawing.Size(234, 27);
            this.CmbModule.TabIndex = 8;
            this.CmbModule.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbModule_KeyDown);
            // 
            // lblBTInvoiceDesc
            // 
            this.lblBTInvoiceDesc.AutoSize = true;
            this.lblBTInvoiceDesc.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBTInvoiceDesc.Location = new System.Drawing.Point(12, 78);
            this.lblBTInvoiceDesc.Name = "lblBTInvoiceDesc";
            this.lblBTInvoiceDesc.Size = new System.Drawing.Size(95, 19);
            this.lblBTInvoiceDesc.TabIndex = 0;
            this.lblBTInvoiceDesc.Text = "Description";
            // 
            // lblBTInvoiceModule
            // 
            this.lblBTInvoiceModule.AutoSize = true;
            this.lblBTInvoiceModule.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBTInvoiceModule.Location = new System.Drawing.Point(12, 106);
            this.lblBTInvoiceModule.Name = "lblBTInvoiceModule";
            this.lblBTInvoiceModule.Size = new System.Drawing.Size(63, 19);
            this.lblBTInvoiceModule.TabIndex = 0;
            this.lblBTInvoiceModule.Text = "Module";
            // 
            // lblBTInvoiceTermSno
            // 
            this.lblBTInvoiceTermSno.AutoSize = true;
            this.lblBTInvoiceTermSno.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBTInvoiceTermSno.Location = new System.Drawing.Point(12, 50);
            this.lblBTInvoiceTermSno.Name = "lblBTInvoiceTermSno";
            this.lblBTInvoiceTermSno.Size = new System.Drawing.Size(89, 19);
            this.lblBTInvoiceTermSno.TabIndex = 0;
            this.lblBTInvoiceTermSno.Text = "Sno (Code)";
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.BtnView);
            this.StorePanel.Controls.Add(this.BtnLedger);
            this.StorePanel.Controls.Add(this.BtnCode);
            this.StorePanel.Controls.Add(this.clsSeparator2);
            this.StorePanel.Controls.Add(this.clsSeparator1);
            this.StorePanel.Controls.Add(this.BtnCancel);
            this.StorePanel.Controls.Add(this.ChkActive);
            this.StorePanel.Controls.Add(this.BtnSave);
            this.StorePanel.Controls.Add(this.BtnNew);
            this.StorePanel.Controls.Add(this.ChkProfitability);
            this.StorePanel.Controls.Add(this.ChkSupress);
            this.StorePanel.Controls.Add(this.BtnDelete);
            this.StorePanel.Controls.Add(this.TxtRate);
            this.StorePanel.Controls.Add(this.lblBTInvoiceRate);
            this.StorePanel.Controls.Add(this.BtnEdit);
            this.StorePanel.Controls.Add(this.CmbCondition);
            this.StorePanel.Controls.Add(this.BtnExit);
            this.StorePanel.Controls.Add(this.lblBTInvoiceCondition);
            this.StorePanel.Controls.Add(this.CmbSign);
            this.StorePanel.Controls.Add(this.lblBTInvoiceSign);
            this.StorePanel.Controls.Add(this.TxtCode);
            this.StorePanel.Controls.Add(this.CmbBasis);
            this.StorePanel.Controls.Add(this.lblBTInvoiceTermSno);
            this.StorePanel.Controls.Add(this.lblBTInvoiceBasis);
            this.StorePanel.Controls.Add(this.lblBTInvoiceModule);
            this.StorePanel.Controls.Add(this.CmbType);
            this.StorePanel.Controls.Add(this.lblBTInvoiceDesc);
            this.StorePanel.Controls.Add(this.lblBTInvoiceType);
            this.StorePanel.Controls.Add(this.TxtDescription);
            this.StorePanel.Controls.Add(this.TxtLedger);
            this.StorePanel.Controls.Add(this.CmbModule);
            this.StorePanel.Controls.Add(this.lblBTInvoiceGl);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(584, 298);
            this.StorePanel.TabIndex = 0;
            // 
            // BtnView
            // 
            this.BtnView.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnView.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnView.Appearance.Options.UseFont = true;
            this.BtnView.Appearance.Options.UseForeColor = true;
            this.BtnView.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnView.Location = new System.Drawing.Point(270, 4);
            this.BtnView.Margin = new System.Windows.Forms.Padding(4);
            this.BtnView.Name = "BtnView";
            this.BtnView.Size = new System.Drawing.Size(78, 33);
            this.BtnView.TabIndex = 3;
            this.BtnView.Tag = "ViewButtonCheck";
            this.BtnView.Text = "&VIEW";
            this.BtnView.Click += new System.EventHandler(this.BtnView_Click);
            // 
            // BtnLedger
            // 
            this.BtnLedger.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnLedger.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnLedger.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnLedger.Location = new System.Drawing.Point(554, 134);
            this.BtnLedger.Name = "BtnLedger";
            this.BtnLedger.Size = new System.Drawing.Size(25, 25);
            this.BtnLedger.TabIndex = 31;
            this.BtnLedger.TabStop = false;
            this.BtnLedger.UseVisualStyleBackColor = false;
            this.BtnLedger.Click += new System.EventHandler(this.BtnLedger_Click);
            // 
            // BtnCode
            // 
            this.BtnCode.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnCode.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnCode.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnCode.Location = new System.Drawing.Point(245, 46);
            this.BtnCode.Name = "BtnCode";
            this.BtnCode.Size = new System.Drawing.Size(25, 25);
            this.BtnCode.TabIndex = 30;
            this.BtnCode.TabStop = false;
            this.BtnCode.UseVisualStyleBackColor = false;
            this.BtnCode.Click += new System.EventHandler(this.BtnCode_Click);
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(3, 250);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(581, 2);
            this.clsSeparator2.TabIndex = 29;
            this.clsSeparator2.TabStop = false;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(3, 40);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(581, 2);
            this.clsSeparator1.TabIndex = 28;
            this.clsSeparator1.TabStop = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.Appearance.Options.UseForeColor = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.BtnCancel.Location = new System.Drawing.Point(476, 255);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(101, 37);
            this.BtnCancel.TabIndex = 17;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Appearance.Options.UseForeColor = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(378, 255);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(95, 37);
            this.BtnSave.TabIndex = 16;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnNew
            // 
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNew.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.Appearance.Options.UseForeColor = true;
            this.BtnNew.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnNew.Location = new System.Drawing.Point(10, 4);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(75, 33);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Tag = "NewButtonCheck";
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.Appearance.Options.UseForeColor = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(167, 4);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(103, 33);
            this.BtnDelete.TabIndex = 2;
            this.BtnDelete.Tag = "DeleteButtonCheck";
            this.BtnDelete.Text = "&DELETE";
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // TxtRate
            // 
            this.TxtRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtRate.Location = new System.Drawing.Point(115, 223);
            this.TxtRate.Name = "TxtRate";
            this.TxtRate.Size = new System.Drawing.Size(235, 25);
            this.TxtRate.TabIndex = 13;
            this.TxtRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtRate_KeyPress);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEdit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnEdit.Appearance.Options.UseFont = true;
            this.BtnEdit.Appearance.Options.UseForeColor = true;
            this.BtnEdit.ImageOptions.Image = global::MrBLL.Properties.Resources.Edit;
            this.BtnEdit.Location = new System.Drawing.Point(85, 4);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(82, 33);
            this.BtnEdit.TabIndex = 1;
            this.BtnEdit.Tag = "EditButtonCheck";
            this.BtnEdit.Text = "&EDIT";
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseForeColor = true;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.BtnExit.Location = new System.Drawing.Point(502, 4);
            this.BtnExit.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(76, 33);
            this.BtnExit.TabIndex = 4;
            this.BtnExit.Text = "E&XIT";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // TxtCode
            // 
            this.TxtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCode.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCode.Location = new System.Drawing.Point(115, 47);
            this.TxtCode.MaxLength = 50;
            this.TxtCode.Name = "TxtCode";
            this.TxtCode.Size = new System.Drawing.Size(124, 25);
            this.TxtCode.TabIndex = 5;
            this.TxtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCode_KeyDown);
            this.TxtCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCode_KeyPress);
            this.TxtCode.Leave += new System.EventHandler(this.TxtCode_Leave);
            this.TxtCode.Validating += new System.ComponentModel.CancelEventHandler(this.TxtCode_Validating);
            // 
            // TxtDescription
            // 
            this.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDescription.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDescription.Location = new System.Drawing.Point(115, 75);
            this.TxtDescription.MaxLength = 255;
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(463, 25);
            this.TxtDescription.TabIndex = 7;
            this.TxtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDescription_KeyDown);
            this.TxtDescription.Leave += new System.EventHandler(this.TxtDescription_Leave);
            // 
            // TxtLedger
            // 
            this.TxtLedger.BackColor = System.Drawing.Color.White;
            this.TxtLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtLedger.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtLedger.Location = new System.Drawing.Point(116, 134);
            this.TxtLedger.Name = "TxtLedger";
            this.TxtLedger.ReadOnly = true;
            this.TxtLedger.Size = new System.Drawing.Size(439, 25);
            this.TxtLedger.TabIndex = 10;
            this.TxtLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtLedger_KeyDown);
            this.TxtLedger.Leave += new System.EventHandler(this.TxtLedger_Leave);
            this.TxtLedger.Validating += new System.ComponentModel.CancelEventHandler(this.TxtLedger_Validating);
            // 
            // FrmSalesBillingTerm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(584, 298);
            this.Controls.Add(this.StorePanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmSalesBillingTerm";
            this.ShowIcon = false;
            this.Tag = "Sales Term";
            this.Text = "SALES BILLING TERM SETUP";
            this.Load += new System.EventHandler(this.FrmSalesTerm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmSalesTerm_KeyPress);
            this.StorePanel.ResumeLayout(false);
            this.StorePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
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
        private System.Windows.Forms.Label lblBTInvoiceRate;
        private System.Windows.Forms.ComboBox CmbCondition;
        private System.Windows.Forms.Label lblBTInvoiceCondition;
        private System.Windows.Forms.CheckBox ChkSupress;
        private System.Windows.Forms.CheckBox ChkProfitability;
        private System.Windows.Forms.CheckBox ChkActive;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private ClsSeparator clsSeparator1;
        private ClsSeparator clsSeparator2;
        private System.Windows.Forms.Button BtnCode;
        private System.Windows.Forms.Button BtnLedger;
		private DevExpress.XtraEditors.SimpleButton BtnView;
        private MrTextBox TxtLedger;
        private MrTextBox TxtDescription;
        private MrTextBox TxtCode;
        private MrTextBox TxtRate;
        private MrPanel StorePanel;
    }
}