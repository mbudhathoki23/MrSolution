using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Setup.CompanySetup
{
    partial class FrmCompanySetup
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
            this.components = new System.ComponentModel.Container();
            this.lbl_CompanyLogo = new System.Windows.Forms.Label();
            this.pb_CompanyLogo = new System.Windows.Forms.PictureBox();
            this.cb_DataBasePath = new System.Windows.Forms.ComboBox();
            this.lbl_DatabasePath = new System.Windows.Forms.Label();
            this.TxtEmailAddress = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_Email = new System.Windows.Forms.Label();
            this.TxtWebSites = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_WebSite = new System.Windows.Forms.Label();
            this.TxtTPanNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.MskRegDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.CmbFiscalYear = new System.Windows.Forms.ComboBox();
            this.lbl_PanNo = new System.Windows.Forms.Label();
            this.lbl_Regdate = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_PhoneNo = new System.Windows.Forms.Label();
            this.lbl_Country = new System.Windows.Forms.Label();
            this.TxtFaxNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtPhoneNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtCountry = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtAddress = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtPrintingName = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_CmpAddress = new System.Windows.Forms.Label();
            this.lbl_CmpName = new System.Windows.Forms.Label();
            this.CompanyregToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.PanelHeader = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtIntial = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtCompanyDesc = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.MskEndDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.MskStartDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.pb_CompanyLogo)).BeginInit();
            this.PanelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_CompanyLogo
            // 
            this.lbl_CompanyLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_CompanyLogo.Enabled = false;
            this.lbl_CompanyLogo.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_CompanyLogo.ForeColor = System.Drawing.Color.Maroon;
            this.lbl_CompanyLogo.Location = new System.Drawing.Point(20, 58);
            this.lbl_CompanyLogo.Name = "lbl_CompanyLogo";
            this.lbl_CompanyLogo.Size = new System.Drawing.Size(84, 58);
            this.lbl_CompanyLogo.TabIndex = 21;
            this.lbl_CompanyLogo.Text = "Double click to add Logo";
            this.lbl_CompanyLogo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbl_CompanyLogo.DoubleClick += new System.EventHandler(this.LblCompanyLogo_DoubleClick);
            // 
            // pb_CompanyLogo
            // 
            this.pb_CompanyLogo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pb_CompanyLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pb_CompanyLogo.Enabled = false;
            this.pb_CompanyLogo.Location = new System.Drawing.Point(7, 43);
            this.pb_CompanyLogo.Name = "pb_CompanyLogo";
            this.pb_CompanyLogo.Size = new System.Drawing.Size(116, 88);
            this.pb_CompanyLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_CompanyLogo.TabIndex = 33;
            this.pb_CompanyLogo.TabStop = false;
            this.pb_CompanyLogo.DoubleClick += new System.EventHandler(this.PbCompanyLogo_DoubleClick);
            // 
            // cb_DataBasePath
            // 
            this.cb_DataBasePath.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_DataBasePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_DataBasePath.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.cb_DataBasePath.FormattingEnabled = true;
            this.cb_DataBasePath.Location = new System.Drawing.Point(115, 309);
            this.cb_DataBasePath.Name = "cb_DataBasePath";
            this.cb_DataBasePath.Size = new System.Drawing.Size(248, 27);
            this.cb_DataBasePath.TabIndex = 18;
            // 
            // lbl_DatabasePath
            // 
            this.lbl_DatabasePath.AutoSize = true;
            this.lbl_DatabasePath.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_DatabasePath.Location = new System.Drawing.Point(5, 313);
            this.lbl_DatabasePath.Name = "lbl_DatabasePath";
            this.lbl_DatabasePath.Size = new System.Drawing.Size(72, 19);
            this.lbl_DatabasePath.TabIndex = 32;
            this.lbl_DatabasePath.Text = "Location";
            // 
            // TxtEmailAddress
            // 
            this.TxtEmailAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtEmailAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.TxtEmailAddress.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtEmailAddress.Location = new System.Drawing.Point(115, 280);
            this.TxtEmailAddress.Name = "TxtEmailAddress";
            this.TxtEmailAddress.Size = new System.Drawing.Size(248, 25);
            this.TxtEmailAddress.TabIndex = 16;
            this.TxtEmailAddress.Leave += new System.EventHandler(this.TxtEmailId_Leave);
            // 
            // lbl_Email
            // 
            this.lbl_Email.AutoSize = true;
            this.lbl_Email.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_Email.Location = new System.Drawing.Point(6, 283);
            this.lbl_Email.Name = "lbl_Email";
            this.lbl_Email.Size = new System.Drawing.Size(52, 19);
            this.lbl_Email.TabIndex = 27;
            this.lbl_Email.Text = "Email";
            // 
            // TxtWebSites
            // 
            this.TxtWebSites.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtWebSites.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.TxtWebSites.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtWebSites.Location = new System.Drawing.Point(466, 280);
            this.TxtWebSites.Name = "TxtWebSites";
            this.TxtWebSites.Size = new System.Drawing.Size(255, 25);
            this.TxtWebSites.TabIndex = 17;
            this.TxtWebSites.Leave += new System.EventHandler(this.TxtWebSite_Leave);
            // 
            // lbl_WebSite
            // 
            this.lbl_WebSite.AutoSize = true;
            this.lbl_WebSite.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_WebSite.Location = new System.Drawing.Point(386, 283);
            this.lbl_WebSite.Name = "lbl_WebSite";
            this.lbl_WebSite.Size = new System.Drawing.Size(68, 19);
            this.lbl_WebSite.TabIndex = 25;
            this.lbl_WebSite.Text = "Website";
            // 
            // TxtTPanNo
            // 
            this.TxtTPanNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTPanNo.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtTPanNo.Location = new System.Drawing.Point(115, 252);
            this.TxtTPanNo.Name = "TxtTPanNo";
            this.TxtTPanNo.Size = new System.Drawing.Size(248, 25);
            this.TxtTPanNo.TabIndex = 14;
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.BtnExit.Location = new System.Drawing.Point(639, 4);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(80, 32);
            this.BtnExit.TabIndex = 3;
            this.BtnExit.Text = "E&XIT";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(159, 4);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(96, 32);
            this.BtnDelete.TabIndex = 2;
            this.BtnDelete.Tag = "DeleteButtonCheck";
            this.BtnDelete.Text = "&DELETE";
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnEdit.Appearance.Options.UseFont = true;
            this.BtnEdit.ImageOptions.Image = global::MrBLL.Properties.Resources.Edit;
            this.BtnEdit.Location = new System.Drawing.Point(83, 4);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(75, 32);
            this.BtnEdit.TabIndex = 1;
            this.BtnEdit.Tag = "EditButtonCheck";
            this.BtnEdit.Text = "&EDIT";
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnNew
            // 
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnNew.Location = new System.Drawing.Point(6, 4);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(75, 32);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Tag = "NewButtonCheck";
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(613, 344);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(106, 36);
            this.BtnCancel.TabIndex = 20;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // MskRegDate
            // 
            this.MskRegDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskRegDate.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.MskRegDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskRegDate.Location = new System.Drawing.Point(232, 48);
            this.MskRegDate.Mask = "00/00/0000";
            this.MskRegDate.Name = "MskRegDate";
            this.MskRegDate.Size = new System.Drawing.Size(162, 25);
            this.MskRegDate.TabIndex = 4;
            this.MskRegDate.ValidatingType = typeof(System.DateTime);
            this.MskRegDate.Validated += new System.EventHandler(this.MskRegister_DateValidated);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(509, 344);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(103, 35);
            this.BtnSave.TabIndex = 19;
            this.BtnSave.Text = "C&REATE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // CmbFiscalYear
            // 
            this.CmbFiscalYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbFiscalYear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbFiscalYear.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.CmbFiscalYear.FormattingEnabled = true;
            this.CmbFiscalYear.Location = new System.Drawing.Point(232, 108);
            this.CmbFiscalYear.Name = "CmbFiscalYear";
            this.CmbFiscalYear.Size = new System.Drawing.Size(213, 27);
            this.CmbFiscalYear.TabIndex = 6;
            this.CmbFiscalYear.SelectedIndexChanged += new System.EventHandler(this.CmbFiscalYear_SelectedIndexChanged);
            // 
            // lbl_PanNo
            // 
            this.lbl_PanNo.AutoSize = true;
            this.lbl_PanNo.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_PanNo.Location = new System.Drawing.Point(6, 255);
            this.lbl_PanNo.Name = "lbl_PanNo";
            this.lbl_PanNo.Size = new System.Drawing.Size(107, 19);
            this.lbl_PanNo.TabIndex = 23;
            this.lbl_PanNo.Text = "PAN/VAT No.";
            // 
            // lbl_Regdate
            // 
            this.lbl_Regdate.AutoSize = true;
            this.lbl_Regdate.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_Regdate.Location = new System.Drawing.Point(133, 51);
            this.lbl_Regdate.Name = "lbl_Regdate";
            this.lbl_Regdate.Size = new System.Drawing.Size(78, 19);
            this.lbl_Regdate.TabIndex = 17;
            this.lbl_Regdate.Text = "Reg Date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label5.Location = new System.Drawing.Point(386, 255);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 19);
            this.label5.TabIndex = 16;
            this.label5.Text = "Fax";
            // 
            // lbl_PhoneNo
            // 
            this.lbl_PhoneNo.AutoSize = true;
            this.lbl_PhoneNo.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_PhoneNo.Location = new System.Drawing.Point(386, 228);
            this.lbl_PhoneNo.Name = "lbl_PhoneNo";
            this.lbl_PhoneNo.Size = new System.Drawing.Size(61, 19);
            this.lbl_PhoneNo.TabIndex = 15;
            this.lbl_PhoneNo.Text = "Tel No.";
            // 
            // lbl_Country
            // 
            this.lbl_Country.AutoSize = true;
            this.lbl_Country.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_Country.Location = new System.Drawing.Point(6, 228);
            this.lbl_Country.Name = "lbl_Country";
            this.lbl_Country.Size = new System.Drawing.Size(69, 19);
            this.lbl_Country.TabIndex = 12;
            this.lbl_Country.Text = "Country";
            // 
            // TxtFaxNo
            // 
            this.TxtFaxNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtFaxNo.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtFaxNo.Location = new System.Drawing.Point(466, 252);
            this.TxtFaxNo.Name = "TxtFaxNo";
            this.TxtFaxNo.Size = new System.Drawing.Size(255, 25);
            this.TxtFaxNo.TabIndex = 15;
            this.TxtFaxNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtFax_KeyPress);
            // 
            // TxtPhoneNo
            // 
            this.TxtPhoneNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPhoneNo.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtPhoneNo.Location = new System.Drawing.Point(466, 225);
            this.TxtPhoneNo.Name = "TxtPhoneNo";
            this.TxtPhoneNo.Size = new System.Drawing.Size(255, 25);
            this.TxtPhoneNo.TabIndex = 13;
            this.TxtPhoneNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPhone_KeyPress);
            // 
            // TxtCountry
            // 
            this.TxtCountry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCountry.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtCountry.Location = new System.Drawing.Point(115, 225);
            this.TxtCountry.Name = "TxtCountry";
            this.TxtCountry.Size = new System.Drawing.Size(248, 25);
            this.TxtCountry.TabIndex = 12;
            // 
            // TxtAddress
            // 
            this.TxtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAddress.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtAddress.Location = new System.Drawing.Point(115, 196);
            this.TxtAddress.Name = "TxtAddress";
            this.TxtAddress.Size = new System.Drawing.Size(606, 25);
            this.TxtAddress.TabIndex = 11;
            // 
            // TxtPrintingName
            // 
            this.TxtPrintingName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPrintingName.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtPrintingName.Location = new System.Drawing.Point(115, 140);
            this.TxtPrintingName.Name = "TxtPrintingName";
            this.TxtPrintingName.Size = new System.Drawing.Size(606, 25);
            this.TxtPrintingName.TabIndex = 9;
            this.TxtPrintingName.TextChanged += new System.EventHandler(this.TxtPrintingName_TextChanged);
            this.TxtPrintingName.Leave += new System.EventHandler(this.TxtPrintingName_Leave);
            this.TxtPrintingName.Validated += new System.EventHandler(this.TxtCompanyName_Validated);
            // 
            // lbl_CmpAddress
            // 
            this.lbl_CmpAddress.AutoSize = true;
            this.lbl_CmpAddress.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_CmpAddress.Location = new System.Drawing.Point(6, 199);
            this.lbl_CmpAddress.Name = "lbl_CmpAddress";
            this.lbl_CmpAddress.Size = new System.Drawing.Size(69, 19);
            this.lbl_CmpAddress.TabIndex = 3;
            this.lbl_CmpAddress.Text = "Address";
            // 
            // lbl_CmpName
            // 
            this.lbl_CmpName.AutoSize = true;
            this.lbl_CmpName.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_CmpName.Location = new System.Drawing.Point(6, 143);
            this.lbl_CmpName.Name = "lbl_CmpName";
            this.lbl_CmpName.Size = new System.Drawing.Size(69, 19);
            this.lbl_CmpName.TabIndex = 2;
            this.lbl_CmpName.Text = "Printing";
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.label4);
            this.PanelHeader.Controls.Add(this.label3);
            this.PanelHeader.Controls.Add(this.label2);
            this.PanelHeader.Controls.Add(this.TxtIntial);
            this.PanelHeader.Controls.Add(this.label1);
            this.PanelHeader.Controls.Add(this.TxtCompanyDesc);
            this.PanelHeader.Controls.Add(this.MskEndDate);
            this.PanelHeader.Controls.Add(this.MskStartDate);
            this.PanelHeader.Controls.Add(this.clsSeparator2);
            this.PanelHeader.Controls.Add(this.clsSeparator1);
            this.PanelHeader.Controls.Add(this.BtnExit);
            this.PanelHeader.Controls.Add(this.BtnNew);
            this.PanelHeader.Controls.Add(this.BtnEdit);
            this.PanelHeader.Controls.Add(this.BtnDelete);
            this.PanelHeader.Controls.Add(this.BtnCancel);
            this.PanelHeader.Controls.Add(this.lbl_CmpName);
            this.PanelHeader.Controls.Add(this.MskRegDate);
            this.PanelHeader.Controls.Add(this.lbl_CmpAddress);
            this.PanelHeader.Controls.Add(this.BtnSave);
            this.PanelHeader.Controls.Add(this.TxtPrintingName);
            this.PanelHeader.Controls.Add(this.CmbFiscalYear);
            this.PanelHeader.Controls.Add(this.TxtAddress);
            this.PanelHeader.Controls.Add(this.TxtCountry);
            this.PanelHeader.Controls.Add(this.TxtPhoneNo);
            this.PanelHeader.Controls.Add(this.lbl_CompanyLogo);
            this.PanelHeader.Controls.Add(this.TxtFaxNo);
            this.PanelHeader.Controls.Add(this.pb_CompanyLogo);
            this.PanelHeader.Controls.Add(this.lbl_Country);
            this.PanelHeader.Controls.Add(this.cb_DataBasePath);
            this.PanelHeader.Controls.Add(this.lbl_PhoneNo);
            this.PanelHeader.Controls.Add(this.lbl_DatabasePath);
            this.PanelHeader.Controls.Add(this.label5);
            this.PanelHeader.Controls.Add(this.TxtEmailAddress);
            this.PanelHeader.Controls.Add(this.lbl_Regdate);
            this.PanelHeader.Controls.Add(this.lbl_Email);
            this.PanelHeader.Controls.Add(this.lbl_PanNo);
            this.PanelHeader.Controls.Add(this.TxtWebSites);
            this.PanelHeader.Controls.Add(this.TxtTPanNo);
            this.PanelHeader.Controls.Add(this.lbl_WebSite);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(724, 383);
            this.PanelHeader.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label4.Location = new System.Drawing.Point(395, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 19);
            this.label4.TabIndex = 48;
            this.label4.Text = "A.D.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label3.Location = new System.Drawing.Point(133, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 19);
            this.label3.TabIndex = 47;
            this.label3.Text = "Fiscal Year";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label2.Location = new System.Drawing.Point(133, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 19);
            this.label2.TabIndex = 46;
            this.label2.Text = "Intial";
            // 
            // TxtIntial
            // 
            this.TxtIntial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtIntial.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtIntial.Location = new System.Drawing.Point(232, 77);
            this.TxtIntial.MaxLength = 10;
            this.TxtIntial.Name = "TxtIntial";
            this.TxtIntial.Size = new System.Drawing.Size(162, 25);
            this.TxtIntial.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label1.Location = new System.Drawing.Point(6, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 19);
            this.label1.TabIndex = 44;
            this.label1.Text = "Description";
            // 
            // TxtCompanyDesc
            // 
            this.TxtCompanyDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCompanyDesc.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtCompanyDesc.Location = new System.Drawing.Point(115, 168);
            this.TxtCompanyDesc.Name = "TxtCompanyDesc";
            this.TxtCompanyDesc.Size = new System.Drawing.Size(606, 25);
            this.TxtCompanyDesc.TabIndex = 10;
            // 
            // MskEndDate
            // 
            this.MskEndDate.BackColor = System.Drawing.Color.White;
            this.MskEndDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskEndDate.Enabled = false;
            this.MskEndDate.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.MskEndDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskEndDate.Location = new System.Drawing.Point(586, 109);
            this.MskEndDate.Mask = "00/00/0000";
            this.MskEndDate.Name = "MskEndDate";
            this.MskEndDate.Size = new System.Drawing.Size(132, 25);
            this.MskEndDate.TabIndex = 8;
            this.MskEndDate.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.MskEndDate.ValidatingType = typeof(System.DateTime);
            // 
            // MskStartDate
            // 
            this.MskStartDate.BackColor = System.Drawing.Color.White;
            this.MskStartDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskStartDate.Enabled = false;
            this.MskStartDate.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.MskStartDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskStartDate.Location = new System.Drawing.Point(451, 109);
            this.MskStartDate.Mask = "00/00/0000";
            this.MskStartDate.Name = "MskStartDate";
            this.MskStartDate.Size = new System.Drawing.Size(132, 25);
            this.MskStartDate.TabIndex = 7;
            this.MskStartDate.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.MskStartDate.ValidatingType = typeof(System.DateTime);
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(9, 341);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(705, 2);
            this.clsSeparator2.TabIndex = 41;
            this.clsSeparator2.TabStop = false;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clsSeparator1.Location = new System.Drawing.Point(3, 39);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(718, 2);
            this.clsSeparator1.TabIndex = 40;
            this.clsSeparator1.TabStop = false;
            // 
            // FrmCompanySetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(724, 383);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCompanySetup";
            this.ShowIcon = false;
            this.Tag = "Company";
            this.Text = "Company Setup";
            this.Load += new System.EventHandler(this.FrmCompanySetup_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmCompanySetup_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.pb_CompanyLogo)).EndInit();
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl_CompanyLogo;
        private System.Windows.Forms.PictureBox pb_CompanyLogo;
        private System.Windows.Forms.ComboBox cb_DataBasePath;
        private System.Windows.Forms.Label lbl_DatabasePath;
        private System.Windows.Forms.Label lbl_Email;
        private System.Windows.Forms.Label lbl_WebSite;
        private System.Windows.Forms.Label lbl_PanNo;
        private System.Windows.Forms.Label lbl_Regdate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_PhoneNo;
        private System.Windows.Forms.Label lbl_Country;
        private System.Windows.Forms.Label lbl_CmpAddress;
        private System.Windows.Forms.Label lbl_CmpName;
        private System.Windows.Forms.ToolTip CompanyregToolTip;
        private System.Windows.Forms.ComboBox CmbFiscalYear;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private ClsSeparator clsSeparator1;
        private ClsSeparator clsSeparator2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private MrTextBox TxtEmailAddress;
        private MrTextBox TxtWebSites;
        private MrTextBox TxtTPanNo;
        private MrTextBox TxtFaxNo;
        private MrTextBox TxtPhoneNo;
        private MrTextBox TxtCountry;
        private MrTextBox TxtAddress;
        private MrTextBox TxtPrintingName;
        private MrMaskedTextBox MskRegDate;
        private MrMaskedTextBox MskEndDate;
        private MrMaskedTextBox MskStartDate;
        private MrTextBox TxtCompanyDesc;
        private MrTextBox TxtIntial;
        private MrPanel PanelHeader;
        private System.Windows.Forms.Label label4;
    }
}