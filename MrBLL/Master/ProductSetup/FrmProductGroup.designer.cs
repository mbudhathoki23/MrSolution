using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Master.ProductSetup
{
    partial class FrmProductGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProductGroup));
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.ChkActive = new System.Windows.Forms.CheckBox();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.CmbPrinter = new System.Windows.Forms.ComboBox();
            this.lblGrpMarginPer = new System.Windows.Forms.Label();
            this.lblGrpDefaultPrinter = new System.Windows.Forms.Label();
            this.TxtMargin = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblGrpMargin = new System.Windows.Forms.Label();
            this.TxtShortName = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblGrpCode = new System.Windows.Forms.Label();
            this.TxtDescription = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblGrpName = new System.Windows.Forms.Label();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSync = new DevExpress.XtraEditors.SimpleButton();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.StorePanel = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.TabProductGroup = new DevExpress.XtraBars.Navigation.TabPane();
            this.TabLedgerGroup = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.TxtNepaliDesc = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnDescription = new System.Windows.Forms.Button();
            this.TabGroupDetails = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.BtnBSClosingLedger = new System.Windows.Forms.Button();
            this.BtnClosingLedger = new System.Windows.Forms.Button();
            this.BtnOpeningLedger = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.TxtOpeningLedger = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.TxtClosingLedger = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtBSClosingLedger = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.BtnSalesReturn = new System.Windows.Forms.Button();
            this.BtnSalesLedger = new System.Windows.Forms.Button();
            this.BtnPurchaseReturn = new System.Windows.Forms.Button();
            this.TxtPurchaseLedger = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnPurchaseLedger = new System.Windows.Forms.Button();
            this.lvlProductPurchase = new System.Windows.Forms.Label();
            this.lvlProductPurchaseReturn = new System.Windows.Forms.Label();
            this.TxtPurchaseReturn = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lvlProductSales = new System.Windows.Forms.Label();
            this.TxtSalesLedger = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtSalesReturn = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lvlProductSalesReturn = new System.Windows.Forms.Label();
            this.mrPanel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
           
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.mrPanel2 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.BtnView = new DevExpress.XtraEditors.SimpleButton();
            this.StorePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TabProductGroup)).BeginInit();
            this.TabProductGroup.SuspendLayout();
            this.TabLedgerGroup.SuspendLayout();
            this.TabGroupDetails.SuspendLayout();
            this.mrPanel1.SuspendLayout();
            this.mrPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.BtnCancel.Location = new System.Drawing.Point(380, 5);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(104, 37);
            this.BtnCancel.TabIndex = 13;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // ChkActive
            // 
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkActive.Location = new System.Drawing.Point(18, 9);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkActive.Size = new System.Drawing.Size(84, 29);
            this.ChkActive.TabIndex = 10;
            this.ChkActive.Text = "Active";
            this.ChkActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkActive.UseVisualStyleBackColor = true;
            this.ChkActive.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ChkActive_KeyPress);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(291, 5);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(89, 37);
            this.BtnSave.TabIndex = 12;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // CmbPrinter
            // 
            this.CmbPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbPrinter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbPrinter.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbPrinter.FormattingEnabled = true;
            this.CmbPrinter.Location = new System.Drawing.Point(127, 90);
            this.CmbPrinter.MaxLength = 50;
            this.CmbPrinter.Name = "CmbPrinter";
            this.CmbPrinter.Size = new System.Drawing.Size(353, 27);
            this.CmbPrinter.TabIndex = 8;
            this.CmbPrinter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbPrinter_KeyPress);
            // 
            // lblGrpMarginPer
            // 
            this.lblGrpMarginPer.AutoSize = true;
            this.lblGrpMarginPer.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrpMarginPer.Location = new System.Drawing.Point(304, 65);
            this.lblGrpMarginPer.Name = "lblGrpMarginPer";
            this.lblGrpMarginPer.Size = new System.Drawing.Size(23, 19);
            this.lblGrpMarginPer.TabIndex = 7;
            this.lblGrpMarginPer.Text = "%";
            // 
            // lblGrpDefaultPrinter
            // 
            this.lblGrpDefaultPrinter.AutoSize = true;
            this.lblGrpDefaultPrinter.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrpDefaultPrinter.Location = new System.Drawing.Point(12, 94);
            this.lblGrpDefaultPrinter.Name = "lblGrpDefaultPrinter";
            this.lblGrpDefaultPrinter.Size = new System.Drawing.Size(104, 19);
            this.lblGrpDefaultPrinter.TabIndex = 6;
            this.lblGrpDefaultPrinter.Text = "Printer Desc";
            // 
            // TxtMargin
            // 
            this.TxtMargin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMargin.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtMargin.Location = new System.Drawing.Point(127, 62);
            this.TxtMargin.MaxLength = 25;
            this.TxtMargin.Name = "TxtMargin";
            this.TxtMargin.Size = new System.Drawing.Size(171, 25);
            this.TxtMargin.TabIndex = 7;
            this.TxtMargin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtMargin.TextBoxType = MrDAL.Control.ControlsEx.Control.TextBoxType.Decimal;
            this.TxtMargin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtMargin_KeyPress);
            // 
            // lblGrpMargin
            // 
            this.lblGrpMargin.AutoSize = true;
            this.lblGrpMargin.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrpMargin.Location = new System.Drawing.Point(12, 65);
            this.lblGrpMargin.Name = "lblGrpMargin";
            this.lblGrpMargin.Size = new System.Drawing.Size(61, 19);
            this.lblGrpMargin.TabIndex = 4;
            this.lblGrpMargin.Text = "Margin";
            // 
            // TxtShortName
            // 
            this.TxtShortName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtShortName.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtShortName.Location = new System.Drawing.Point(127, 35);
            this.TxtShortName.MaxLength = 200;
            this.TxtShortName.Name = "TxtShortName";
            this.TxtShortName.Size = new System.Drawing.Size(171, 25);
            this.TxtShortName.TabIndex = 6;
            this.TxtShortName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtShortName_KeyDown);
            this.TxtShortName.Leave += new System.EventHandler(this.TxtShortName_Leave);
            this.TxtShortName.Validating += new System.ComponentModel.CancelEventHandler(this.TxtShortName_Validating);
            // 
            // lblGrpCode
            // 
            this.lblGrpCode.AutoSize = true;
            this.lblGrpCode.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrpCode.Location = new System.Drawing.Point(12, 38);
            this.lblGrpCode.Name = "lblGrpCode";
            this.lblGrpCode.Size = new System.Drawing.Size(98, 19);
            this.lblGrpCode.TabIndex = 2;
            this.lblGrpCode.Text = "Short Name";
            // 
            // TxtDescription
            // 
            this.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDescription.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDescription.Location = new System.Drawing.Point(127, 7);
            this.TxtDescription.MaxLength = 200;
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(322, 25);
            this.TxtDescription.TabIndex = 5;
            this.TxtDescription.TextChanged += new System.EventHandler(this.TxtDescription_TextChanged);
            this.TxtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDescription_KeyDown);
            this.TxtDescription.Leave += new System.EventHandler(this.TxtDescription_Leave);
            this.TxtDescription.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDescription_Validating);
            // 
            // lblGrpName
            // 
            this.lblGrpName.AutoSize = true;
            this.lblGrpName.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrpName.Location = new System.Drawing.Point(12, 10);
            this.lblGrpName.Name = "lblGrpName";
            this.lblGrpName.Size = new System.Drawing.Size(95, 19);
            this.lblGrpName.TabIndex = 0;
            this.lblGrpName.Text = "Description";
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.BtnExit.Location = new System.Drawing.Point(415, 6);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(77, 33);
            this.BtnExit.TabIndex = 4;
            this.BtnExit.Text = "E&XIT";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(156, 6);
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
            this.BtnEdit.Appearance.Options.UseFont = true;
            this.BtnEdit.ImageOptions.Image = global::MrBLL.Properties.Resources.Edit;
            this.BtnEdit.Location = new System.Drawing.Point(82, 6);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(73, 33);
            this.BtnEdit.TabIndex = 1;
            this.BtnEdit.Tag = "EditButtonCheck";
            this.BtnEdit.Text = "&EDIT";
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnNew
            // 
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnNew.Location = new System.Drawing.Point(6, 6);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(75, 33);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Tag = "NewButtonCheck";
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.TabProductGroup);
            this.StorePanel.Controls.Add(this.mrPanel1);
            this.StorePanel.Controls.Add(this.mrPanel2);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(494, 326);
            this.StorePanel.TabIndex = 0;
            // 
            // TabProductGroup
            // 
            this.TabProductGroup.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TabProductGroup.Appearance.Options.UseFont = true;
            this.TabProductGroup.AppearanceButton.Hovered.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TabProductGroup.AppearanceButton.Hovered.Options.UseFont = true;
            this.TabProductGroup.AppearanceButton.Normal.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TabProductGroup.AppearanceButton.Normal.Options.UseFont = true;
            this.TabProductGroup.AppearanceButton.Pressed.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TabProductGroup.AppearanceButton.Pressed.Options.UseFont = true;
            this.TabProductGroup.Controls.Add(this.TabLedgerGroup);
            this.TabProductGroup.Controls.Add(this.TabGroupDetails);
            this.TabProductGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabProductGroup.Location = new System.Drawing.Point(0, 44);
            this.TabProductGroup.Name = "TabProductGroup";
            this.TabProductGroup.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.TabLedgerGroup,
            this.TabGroupDetails});
            this.TabProductGroup.RegularSize = new System.Drawing.Size(494, 234);
            this.TabProductGroup.SelectedPage = this.TabLedgerGroup;
            this.TabProductGroup.Size = new System.Drawing.Size(494, 234);
            this.TabProductGroup.TabIndex = 1;
            this.TabProductGroup.Text = "Group Details";
            // 
            // TabLedgerGroup
            // 
            this.TabLedgerGroup.Caption = "Group Details";
            this.TabLedgerGroup.Controls.Add(this.TxtNepaliDesc);
            this.TabLedgerGroup.Controls.Add(this.label1);
            this.TabLedgerGroup.Controls.Add(this.lblGrpCode);
            this.TabLedgerGroup.Controls.Add(this.BtnDescription);
            this.TabLedgerGroup.Controls.Add(this.lblGrpMargin);
            this.TabLedgerGroup.Controls.Add(this.TxtShortName);
            this.TabLedgerGroup.Controls.Add(this.TxtDescription);
            this.TabLedgerGroup.Controls.Add(this.TxtMargin);
            this.TabLedgerGroup.Controls.Add(this.lblGrpName);
            this.TabLedgerGroup.Controls.Add(this.lblGrpDefaultPrinter);
            this.TabLedgerGroup.Controls.Add(this.CmbPrinter);
            this.TabLedgerGroup.Controls.Add(this.lblGrpMarginPer);
            this.TabLedgerGroup.Name = "TabLedgerGroup";
            this.TabLedgerGroup.Size = new System.Drawing.Size(494, 201);
            // 
            // TxtNepaliDesc
            // 
            this.TxtNepaliDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNepaliDesc.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtNepaliDesc.Location = new System.Drawing.Point(127, 123);
            this.TxtNepaliDesc.MaxLength = 200;
            this.TxtNepaliDesc.Name = "TxtNepaliDesc";
            this.TxtNepaliDesc.Size = new System.Drawing.Size(353, 25);
            this.TxtNepaliDesc.TabIndex = 9;
            this.TxtNepaliDesc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 19);
            this.label1.TabIndex = 310;
            this.label1.Text = "Nepali";
            // 
            // BtnDescription
            // 
            this.BtnDescription.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnDescription.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnDescription.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDescription.Location = new System.Drawing.Point(450, 7);
            this.BtnDescription.Name = "BtnDescription";
            this.BtnDescription.Size = new System.Drawing.Size(25, 25);
            this.BtnDescription.TabIndex = 309;
            this.BtnDescription.TabStop = false;
            this.BtnDescription.UseVisualStyleBackColor = false;
            this.BtnDescription.Click += new System.EventHandler(this.BtnDescription_Click);
            // 
            // TabGroupDetails
            // 
            this.TabGroupDetails.Caption = "Ledger Group";
            this.TabGroupDetails.Controls.Add(this.BtnBSClosingLedger);
            this.TabGroupDetails.Controls.Add(this.BtnClosingLedger);
            this.TabGroupDetails.Controls.Add(this.BtnOpeningLedger);
            this.TabGroupDetails.Controls.Add(this.label13);
            this.TabGroupDetails.Controls.Add(this.TxtOpeningLedger);
            this.TabGroupDetails.Controls.Add(this.label14);
            this.TabGroupDetails.Controls.Add(this.TxtClosingLedger);
            this.TabGroupDetails.Controls.Add(this.TxtBSClosingLedger);
            this.TabGroupDetails.Controls.Add(this.label15);
            this.TabGroupDetails.Controls.Add(this.BtnSalesReturn);
            this.TabGroupDetails.Controls.Add(this.BtnSalesLedger);
            this.TabGroupDetails.Controls.Add(this.BtnPurchaseReturn);
            this.TabGroupDetails.Controls.Add(this.TxtPurchaseLedger);
            this.TabGroupDetails.Controls.Add(this.BtnPurchaseLedger);
            this.TabGroupDetails.Controls.Add(this.lvlProductPurchase);
            this.TabGroupDetails.Controls.Add(this.lvlProductPurchaseReturn);
            this.TabGroupDetails.Controls.Add(this.TxtPurchaseReturn);
            this.TabGroupDetails.Controls.Add(this.lvlProductSales);
            this.TabGroupDetails.Controls.Add(this.TxtSalesLedger);
            this.TabGroupDetails.Controls.Add(this.TxtSalesReturn);
            this.TabGroupDetails.Controls.Add(this.lvlProductSalesReturn);
            this.TabGroupDetails.Name = "TabGroupDetails";
            this.TabGroupDetails.Size = new System.Drawing.Size(494, 201);
            // 
            // BtnBSClosingLedger
            // 
            this.BtnBSClosingLedger.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.BtnBSClosingLedger.CausesValidation = false;
            this.BtnBSClosingLedger.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBSClosingLedger.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnBSClosingLedger.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnBSClosingLedger.Location = new System.Drawing.Point(463, 172);
            this.BtnBSClosingLedger.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnBSClosingLedger.Name = "BtnBSClosingLedger";
            this.BtnBSClosingLedger.Size = new System.Drawing.Size(29, 25);
            this.BtnBSClosingLedger.TabIndex = 88;
            this.BtnBSClosingLedger.TabStop = false;
            this.BtnBSClosingLedger.UseVisualStyleBackColor = false;
            this.BtnBSClosingLedger.Click += new System.EventHandler(this.BtnBSClosingLedger_Click);
            // 
            // BtnClosingLedger
            // 
            this.BtnClosingLedger.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.BtnClosingLedger.CausesValidation = false;
            this.BtnClosingLedger.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClosingLedger.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnClosingLedger.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnClosingLedger.Location = new System.Drawing.Point(463, 145);
            this.BtnClosingLedger.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnClosingLedger.Name = "BtnClosingLedger";
            this.BtnClosingLedger.Size = new System.Drawing.Size(29, 25);
            this.BtnClosingLedger.TabIndex = 87;
            this.BtnClosingLedger.TabStop = false;
            this.BtnClosingLedger.UseVisualStyleBackColor = false;
            this.BtnClosingLedger.Click += new System.EventHandler(this.BtnClosingLedger_Click);
            // 
            // BtnOpeningLedger
            // 
            this.BtnOpeningLedger.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.BtnOpeningLedger.CausesValidation = false;
            this.BtnOpeningLedger.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnOpeningLedger.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnOpeningLedger.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnOpeningLedger.Location = new System.Drawing.Point(463, 117);
            this.BtnOpeningLedger.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnOpeningLedger.Name = "BtnOpeningLedger";
            this.BtnOpeningLedger.Size = new System.Drawing.Size(29, 25);
            this.BtnOpeningLedger.TabIndex = 86;
            this.BtnOpeningLedger.TabStop = false;
            this.BtnOpeningLedger.UseVisualStyleBackColor = false;
            this.BtnOpeningLedger.Click += new System.EventHandler(this.BtnOpeningLedger_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(6, 120);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(127, 19);
            this.label13.TabIndex = 83;
            this.label13.Text = "Opening Ledger";
            // 
            // TxtOpeningLedger
            // 
            this.TxtOpeningLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtOpeningLedger.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtOpeningLedger.Location = new System.Drawing.Point(160, 117);
            this.TxtOpeningLedger.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtOpeningLedger.Name = "TxtOpeningLedger";
            this.TxtOpeningLedger.Size = new System.Drawing.Size(303, 25);
            this.TxtOpeningLedger.TabIndex = 5;
            this.TxtOpeningLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtOpeningLedger_KeyDown);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(6, 148);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(120, 19);
            this.label14.TabIndex = 84;
            this.label14.Text = "Closing Ledger";
            // 
            // TxtClosingLedger
            // 
            this.TxtClosingLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtClosingLedger.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtClosingLedger.Location = new System.Drawing.Point(160, 145);
            this.TxtClosingLedger.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtClosingLedger.Name = "TxtClosingLedger";
            this.TxtClosingLedger.Size = new System.Drawing.Size(303, 25);
            this.TxtClosingLedger.TabIndex = 6;
            this.TxtClosingLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtClosingLedger_KeyDown);
            // 
            // TxtBSClosingLedger
            // 
            this.TxtBSClosingLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBSClosingLedger.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBSClosingLedger.Location = new System.Drawing.Point(160, 172);
            this.TxtBSClosingLedger.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtBSClosingLedger.Name = "TxtBSClosingLedger";
            this.TxtBSClosingLedger.Size = new System.Drawing.Size(303, 25);
            this.TxtBSClosingLedger.TabIndex = 7;
            this.TxtBSClosingLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBSClosingLedger_KeyDown);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(6, 175);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(146, 19);
            this.label15.TabIndex = 85;
            this.label15.Text = "BS Closing Ledger";
            // 
            // BtnSalesReturn
            // 
            this.BtnSalesReturn.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.BtnSalesReturn.CausesValidation = false;
            this.BtnSalesReturn.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSalesReturn.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnSalesReturn.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnSalesReturn.Location = new System.Drawing.Point(463, 90);
            this.BtnSalesReturn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnSalesReturn.Name = "BtnSalesReturn";
            this.BtnSalesReturn.Size = new System.Drawing.Size(29, 25);
            this.BtnSalesReturn.TabIndex = 79;
            this.BtnSalesReturn.TabStop = false;
            this.BtnSalesReturn.UseVisualStyleBackColor = false;
            this.BtnSalesReturn.Click += new System.EventHandler(this.BtnSalesReturn_Click);
            // 
            // BtnSalesLedger
            // 
            this.BtnSalesLedger.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.BtnSalesLedger.CausesValidation = false;
            this.BtnSalesLedger.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSalesLedger.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnSalesLedger.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnSalesLedger.Location = new System.Drawing.Point(463, 63);
            this.BtnSalesLedger.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnSalesLedger.Name = "BtnSalesLedger";
            this.BtnSalesLedger.Size = new System.Drawing.Size(29, 25);
            this.BtnSalesLedger.TabIndex = 78;
            this.BtnSalesLedger.TabStop = false;
            this.BtnSalesLedger.UseVisualStyleBackColor = false;
            this.BtnSalesLedger.Click += new System.EventHandler(this.BtnSalesLedger_Click);
            // 
            // BtnPurchaseReturn
            // 
            this.BtnPurchaseReturn.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.BtnPurchaseReturn.CausesValidation = false;
            this.BtnPurchaseReturn.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPurchaseReturn.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnPurchaseReturn.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnPurchaseReturn.Location = new System.Drawing.Point(463, 35);
            this.BtnPurchaseReturn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnPurchaseReturn.Name = "BtnPurchaseReturn";
            this.BtnPurchaseReturn.Size = new System.Drawing.Size(29, 25);
            this.BtnPurchaseReturn.TabIndex = 77;
            this.BtnPurchaseReturn.TabStop = false;
            this.BtnPurchaseReturn.UseVisualStyleBackColor = false;
            this.BtnPurchaseReturn.Click += new System.EventHandler(this.BtnPurchaseReturn_Click);
            // 
            // TxtPurchaseLedger
            // 
            this.TxtPurchaseLedger.BackColor = System.Drawing.Color.White;
            this.TxtPurchaseLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPurchaseLedger.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPurchaseLedger.ForeColor = System.Drawing.Color.Black;
            this.TxtPurchaseLedger.Location = new System.Drawing.Point(160, 8);
            this.TxtPurchaseLedger.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtPurchaseLedger.Name = "TxtPurchaseLedger";
            this.TxtPurchaseLedger.Size = new System.Drawing.Size(303, 25);
            this.TxtPurchaseLedger.TabIndex = 1;
            this.TxtPurchaseLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPurchaseLedger_KeyDown);
            // 
            // BtnPurchaseLedger
            // 
            this.BtnPurchaseLedger.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.BtnPurchaseLedger.CausesValidation = false;
            this.BtnPurchaseLedger.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPurchaseLedger.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnPurchaseLedger.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnPurchaseLedger.Location = new System.Drawing.Point(463, 8);
            this.BtnPurchaseLedger.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnPurchaseLedger.Name = "BtnPurchaseLedger";
            this.BtnPurchaseLedger.Size = new System.Drawing.Size(29, 25);
            this.BtnPurchaseLedger.TabIndex = 76;
            this.BtnPurchaseLedger.TabStop = false;
            this.BtnPurchaseLedger.UseVisualStyleBackColor = false;
            this.BtnPurchaseLedger.Click += new System.EventHandler(this.BtnPurchaseLedger_Click);
            // 
            // lvlProductPurchase
            // 
            this.lvlProductPurchase.AutoSize = true;
            this.lvlProductPurchase.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductPurchase.Location = new System.Drawing.Point(6, 11);
            this.lvlProductPurchase.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lvlProductPurchase.Name = "lvlProductPurchase";
            this.lvlProductPurchase.Size = new System.Drawing.Size(135, 19);
            this.lvlProductPurchase.TabIndex = 75;
            this.lvlProductPurchase.Text = "Purchase Ledger";
            // 
            // lvlProductPurchaseReturn
            // 
            this.lvlProductPurchaseReturn.AutoSize = true;
            this.lvlProductPurchaseReturn.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductPurchaseReturn.Location = new System.Drawing.Point(6, 38);
            this.lvlProductPurchaseReturn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lvlProductPurchaseReturn.Name = "lvlProductPurchaseReturn";
            this.lvlProductPurchaseReturn.Size = new System.Drawing.Size(137, 19);
            this.lvlProductPurchaseReturn.TabIndex = 74;
            this.lvlProductPurchaseReturn.Text = "Purchase Return";
            // 
            // TxtPurchaseReturn
            // 
            this.TxtPurchaseReturn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPurchaseReturn.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPurchaseReturn.Location = new System.Drawing.Point(160, 35);
            this.TxtPurchaseReturn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtPurchaseReturn.Name = "TxtPurchaseReturn";
            this.TxtPurchaseReturn.Size = new System.Drawing.Size(303, 25);
            this.TxtPurchaseReturn.TabIndex = 2;
            this.TxtPurchaseReturn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPurchaseReturn_KeyDown);
            // 
            // lvlProductSales
            // 
            this.lvlProductSales.AutoSize = true;
            this.lvlProductSales.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductSales.Location = new System.Drawing.Point(6, 66);
            this.lvlProductSales.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lvlProductSales.Name = "lvlProductSales";
            this.lvlProductSales.Size = new System.Drawing.Size(106, 19);
            this.lvlProductSales.TabIndex = 73;
            this.lvlProductSales.Text = "Sales Ledger";
            // 
            // TxtSalesLedger
            // 
            this.TxtSalesLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSalesLedger.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSalesLedger.Location = new System.Drawing.Point(160, 63);
            this.TxtSalesLedger.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtSalesLedger.Name = "TxtSalesLedger";
            this.TxtSalesLedger.Size = new System.Drawing.Size(303, 25);
            this.TxtSalesLedger.TabIndex = 3;
            this.TxtSalesLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSalesLedger_KeyDown);
            // 
            // TxtSalesReturn
            // 
            this.TxtSalesReturn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSalesReturn.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSalesReturn.Location = new System.Drawing.Point(160, 90);
            this.TxtSalesReturn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtSalesReturn.Name = "TxtSalesReturn";
            this.TxtSalesReturn.Size = new System.Drawing.Size(303, 25);
            this.TxtSalesReturn.TabIndex = 4;
            this.TxtSalesReturn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSalesReturn_KeyDown);
            // 
            // lvlProductSalesReturn
            // 
            this.lvlProductSalesReturn.AutoSize = true;
            this.lvlProductSalesReturn.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductSalesReturn.Location = new System.Drawing.Point(6, 93);
            this.lvlProductSalesReturn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lvlProductSalesReturn.Name = "lvlProductSalesReturn";
            this.lvlProductSalesReturn.Size = new System.Drawing.Size(108, 19);
            this.lvlProductSalesReturn.TabIndex = 72;
            this.lvlProductSalesReturn.Text = "Sales Return";
            // 
            // mrPanel1
            // 
            this.mrPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel1.Controls.Add(this.BtnSync);
            this.mrPanel1.Controls.Add(this.BtnSave);
            this.mrPanel1.Controls.Add(this.ChkActive);
            this.mrPanel1.Controls.Add(this.BtnCancel);
            this.mrPanel1.Controls.Add(this.clsSeparator2);
            this.mrPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mrPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrPanel1.Location = new System.Drawing.Point(0, 278);
            this.mrPanel1.Name = "mrPanel1";
            this.mrPanel1.Size = new System.Drawing.Size(494, 48);
            this.mrPanel1.TabIndex = 0;
            // 
            // BtnSync
            // 
            this.BtnSync.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSync.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSync.Appearance.Options.UseFont = true;
            this.BtnSync.Appearance.Options.UseForeColor = true;
            this.BtnSync.ImageOptions.Image = global::MrBLL.Properties.Resources.ReportRefresh;
            this.BtnSync.Location = new System.Drawing.Point(204, 8);
            this.BtnSync.Margin = new System.Windows.Forms.Padding(4);
            this.BtnSync.Name = "BtnSync";
            this.BtnSync.Size = new System.Drawing.Size(86, 33);
            this.BtnSync.TabIndex = 11;
            this.BtnSync.Text = "&SYNC";
            this.BtnSync.Click += new System.EventHandler(this.BtnSync_Click);
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(5, 3);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(482, 2);
            this.clsSeparator2.TabIndex = 46;
            this.clsSeparator2.TabStop = false;
            // 
            // mrPanel2
            // 
            this.mrPanel2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel2.Controls.Add(this.BtnNew);
            this.mrPanel2.Controls.Add(this.BtnEdit);
            this.mrPanel2.Controls.Add(this.BtnView);
            this.mrPanel2.Controls.Add(this.BtnDelete);
            this.mrPanel2.Controls.Add(this.BtnExit);
            this.mrPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.mrPanel2.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrPanel2.Location = new System.Drawing.Point(0, 0);
            this.mrPanel2.Name = "mrPanel2";
            this.mrPanel2.Size = new System.Drawing.Size(494, 44);
            this.mrPanel2.TabIndex = 0;
            // 
            // BtnView
            // 
            this.BtnView.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnView.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnView.Appearance.Options.UseFont = true;
            this.BtnView.Appearance.Options.UseForeColor = true;
            this.BtnView.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnView.Location = new System.Drawing.Point(257, 6);
            this.BtnView.Margin = new System.Windows.Forms.Padding(4);
            this.BtnView.Name = "BtnView";
            this.BtnView.Size = new System.Drawing.Size(78, 33);
            this.BtnView.TabIndex = 3;
            this.BtnView.Tag = "ViewButtonCheck";
            this.BtnView.Text = "&VIEW";
            this.BtnView.Click += new System.EventHandler(this.BtnView_Click);
            // 
            // FrmProductGroup
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(494, 326);
            this.Controls.Add(this.StorePanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmProductGroup";
            this.ShowIcon = false;
            this.Tag = "Product Group";
            this.Text = "Product Group Details";
            this.Load += new System.EventHandler(this.FrmProductGroup_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmProductGroup_KeyPress);
            this.StorePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TabProductGroup)).EndInit();
            this.TabProductGroup.ResumeLayout(false);
            this.TabLedgerGroup.ResumeLayout(false);
            this.TabLedgerGroup.PerformLayout();
            this.TabGroupDetails.ResumeLayout(false);
            this.TabGroupDetails.PerformLayout();
            this.mrPanel1.ResumeLayout(false);
            this.mrPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblGrpMarginPer;
        private System.Windows.Forms.Label lblGrpDefaultPrinter;
        private System.Windows.Forms.Label lblGrpMargin;
        private System.Windows.Forms.Label lblGrpCode;
        private System.Windows.Forms.Label lblGrpName;
        private System.Windows.Forms.ComboBox CmbPrinter;
        private System.Windows.Forms.CheckBox ChkActive;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private ClsSeparator clsSeparator2;
        private System.Windows.Forms.Button BtnDescription;
		private DevExpress.XtraEditors.SimpleButton BtnView;
        private MrTextBox TxtMargin;
        private MrTextBox TxtShortName;
        private MrTextBox TxtDescription;
        private MrPanel StorePanel;
        private DevExpress.XtraBars.Navigation.TabPane TabProductGroup;
        private DevExpress.XtraBars.Navigation.TabNavigationPage TabLedgerGroup;
        private DevExpress.XtraBars.Navigation.TabNavigationPage TabGroupDetails;
        private System.Windows.Forms.Button BtnBSClosingLedger;
        private System.Windows.Forms.Button BtnClosingLedger;
        private System.Windows.Forms.Button BtnOpeningLedger;
        private System.Windows.Forms.Label label13;
        private MrTextBox TxtOpeningLedger;
        private System.Windows.Forms.Label label14;
        private MrTextBox TxtClosingLedger;
        private MrTextBox TxtBSClosingLedger;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button BtnSalesReturn;
        private System.Windows.Forms.Button BtnSalesLedger;
        private System.Windows.Forms.Button BtnPurchaseReturn;
        private MrTextBox TxtPurchaseLedger;
        private System.Windows.Forms.Button BtnPurchaseLedger;
        private System.Windows.Forms.Label lvlProductPurchase;
        private System.Windows.Forms.Label lvlProductPurchaseReturn;
        private MrTextBox TxtPurchaseReturn;
        private System.Windows.Forms.Label lvlProductSales;
        private MrTextBox TxtSalesLedger;
        private MrTextBox TxtSalesReturn;
        private System.Windows.Forms.Label lvlProductSalesReturn;
        private MrPanel mrPanel1;
        private MrPanel mrPanel2;
        private MrTextBox TxtNepaliDesc;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton BtnSync;
    }
}