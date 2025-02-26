using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Setup.BusinessUnit
{
    partial class FrmCompanyUnitSetup
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
            this.txt_ContPerPhone = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.MskRegistrationDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.btn_Branch = new System.Windows.Forms.Button();
            this.txt_BranchName = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_ContactPerPhone = new System.Windows.Forms.Label();
            this.txt_ContPerAdd = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_ContPerAdd = new System.Windows.Forms.Label();
            this.TxtEmail = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_Email = new System.Windows.Forms.Label();
            this.txt_ContactPerson = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_ContactPersion = new System.Windows.Forms.Label();
            this.lbl_Regdate = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_PhoneNo = new System.Windows.Forms.Label();
            this.lbl_Country = new System.Windows.Forms.Label();
            this.txt_Fax = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtPhone = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtCountry = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtAddress = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtDescription = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtShortName = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_CmpAddress = new System.Windows.Forms.Label();
            this.lbl_CmpName = new System.Windows.Forms.Label();
            this.lbl_BranchCode = new System.Windows.Forms.Label();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Clear = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Save = new DevExpress.XtraEditors.SimpleButton();
            this.StorePanel = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.btn_Sync = new DevExpress.XtraEditors.SimpleButton();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.BtnDescription = new System.Windows.Forms.Button();
            this.StorePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_ContPerPhone
            // 
            this.txt_ContPerPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ContPerPhone.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.txt_ContPerPhone.Location = new System.Drawing.Point(361, 183);
            this.txt_ContPerPhone.MaxLength = 50;
            this.txt_ContPerPhone.Name = "txt_ContPerPhone";
            this.txt_ContPerPhone.Size = new System.Drawing.Size(172, 26);
            this.txt_ContPerPhone.TabIndex = 14;
            this.txt_ContPerPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_ContPerPhone_KeyPress);
            // 
            // MskRegistrationDate
            // 
            this.MskRegistrationDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskRegistrationDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskRegistrationDate.Location = new System.Drawing.Point(404, 72);
            this.MskRegistrationDate.Mask = "00/00/0000";
            this.MskRegistrationDate.Name = "MskRegistrationDate";
            this.MskRegistrationDate.Size = new System.Drawing.Size(129, 25);
            this.MskRegistrationDate.TabIndex = 7;
            this.MskRegistrationDate.ValidatingType = typeof(System.DateTime);
            this.MskRegistrationDate.Leave += new System.EventHandler(this.MskRegistrationDate_Leave);
            // 
            // btn_Branch
            // 
            this.btn_Branch.CausesValidation = false;
            this.btn_Branch.Image = global::MrBLL.Properties.Resources.search16;
            this.btn_Branch.Location = new System.Drawing.Point(503, 238);
            this.btn_Branch.Name = "btn_Branch";
            this.btn_Branch.Size = new System.Drawing.Size(30, 26);
            this.btn_Branch.TabIndex = 37;
            this.btn_Branch.TabStop = false;
            this.btn_Branch.UseVisualStyleBackColor = true;
            this.btn_Branch.Click += new System.EventHandler(this.btn_Branch_Click);
            // 
            // txt_BranchName
            // 
            this.txt_BranchName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_BranchName.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.txt_BranchName.Location = new System.Drawing.Point(121, 239);
            this.txt_BranchName.MaxLength = 100;
            this.txt_BranchName.Name = "txt_BranchName";
            this.txt_BranchName.ReadOnly = true;
            this.txt_BranchName.Size = new System.Drawing.Size(379, 26);
            this.txt_BranchName.TabIndex = 16;
            this.txt_BranchName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_BranchName_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label1.Location = new System.Drawing.Point(5, 242);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.TabIndex = 35;
            this.label1.Text = "Branch";
            // 
            // lbl_ContactPerPhone
            // 
            this.lbl_ContactPerPhone.AutoSize = true;
            this.lbl_ContactPerPhone.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_ContactPerPhone.Location = new System.Drawing.Point(5, 213);
            this.lbl_ContactPerPhone.Name = "lbl_ContactPerPhone";
            this.lbl_ContactPerPhone.Size = new System.Drawing.Size(72, 20);
            this.lbl_ContactPerPhone.TabIndex = 34;
            this.lbl_ContactPerPhone.Text = "Address";
            // 
            // txt_ContPerAdd
            // 
            this.txt_ContPerAdd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ContPerAdd.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.txt_ContPerAdd.Location = new System.Drawing.Point(121, 211);
            this.txt_ContPerAdd.MaxLength = 50;
            this.txt_ContPerAdd.Name = "txt_ContPerAdd";
            this.txt_ContPerAdd.Size = new System.Drawing.Size(412, 26);
            this.txt_ContPerAdd.TabIndex = 15;
            // 
            // lbl_ContPerAdd
            // 
            this.lbl_ContPerAdd.AutoSize = true;
            this.lbl_ContPerAdd.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_ContPerAdd.Location = new System.Drawing.Point(282, 186);
            this.lbl_ContPerAdd.Name = "lbl_ContPerAdd";
            this.lbl_ContPerAdd.Size = new System.Drawing.Size(73, 20);
            this.lbl_ContPerAdd.TabIndex = 32;
            this.lbl_ContPerAdd.Text = "Number";
            // 
            // TxtEmail
            // 
            this.TxtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtEmail.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.TxtEmail.Location = new System.Drawing.Point(315, 155);
            this.TxtEmail.MaxLength = 50;
            this.TxtEmail.Name = "TxtEmail";
            this.TxtEmail.Size = new System.Drawing.Size(219, 26);
            this.TxtEmail.TabIndex = 12;
            this.TxtEmail.Leave += new System.EventHandler(this.txt_Email_Leave);
            // 
            // lbl_Email
            // 
            this.lbl_Email.AutoSize = true;
            this.lbl_Email.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_Email.Location = new System.Drawing.Point(261, 158);
            this.lbl_Email.Name = "lbl_Email";
            this.lbl_Email.Size = new System.Drawing.Size(54, 20);
            this.lbl_Email.TabIndex = 27;
            this.lbl_Email.Text = "Email";
            // 
            // txt_ContactPerson
            // 
            this.txt_ContactPerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ContactPerson.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.txt_ContactPerson.Location = new System.Drawing.Point(121, 183);
            this.txt_ContactPerson.MaxLength = 50;
            this.txt_ContactPerson.Name = "txt_ContactPerson";
            this.txt_ContactPerson.Size = new System.Drawing.Size(138, 26);
            this.txt_ContactPerson.TabIndex = 13;
            // 
            // lbl_ContactPersion
            // 
            this.lbl_ContactPersion.AutoSize = true;
            this.lbl_ContactPersion.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_ContactPersion.Location = new System.Drawing.Point(5, 186);
            this.lbl_ContactPersion.Name = "lbl_ContactPersion";
            this.lbl_ContactPersion.Size = new System.Drawing.Size(68, 20);
            this.lbl_ContactPersion.TabIndex = 25;
            this.lbl_ContactPersion.Text = "Person ";
            // 
            // lbl_Regdate
            // 
            this.lbl_Regdate.AutoSize = true;
            this.lbl_Regdate.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_Regdate.Location = new System.Drawing.Point(263, 74);
            this.lbl_Regdate.Name = "lbl_Regdate";
            this.lbl_Regdate.Size = new System.Drawing.Size(135, 20);
            this.lbl_Regdate.TabIndex = 17;
            this.lbl_Regdate.Text = "Registered Date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label5.Location = new System.Drawing.Point(5, 159);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 20);
            this.label5.TabIndex = 16;
            this.label5.Text = "Fax No.";
            // 
            // lbl_PhoneNo
            // 
            this.lbl_PhoneNo.AutoSize = true;
            this.lbl_PhoneNo.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_PhoneNo.Location = new System.Drawing.Point(292, 130);
            this.lbl_PhoneNo.Name = "lbl_PhoneNo";
            this.lbl_PhoneNo.Size = new System.Drawing.Size(63, 20);
            this.lbl_PhoneNo.TabIndex = 15;
            this.lbl_PhoneNo.Text = "Tel No.";
            // 
            // lbl_Country
            // 
            this.lbl_Country.AutoSize = true;
            this.lbl_Country.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_Country.Location = new System.Drawing.Point(5, 130);
            this.lbl_Country.Name = "lbl_Country";
            this.lbl_Country.Size = new System.Drawing.Size(74, 20);
            this.lbl_Country.TabIndex = 12;
            this.lbl_Country.Text = "Country";
            // 
            // txt_Fax
            // 
            this.txt_Fax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Fax.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.txt_Fax.Location = new System.Drawing.Point(121, 155);
            this.txt_Fax.MaxLength = 50;
            this.txt_Fax.Name = "txt_Fax";
            this.txt_Fax.Size = new System.Drawing.Size(138, 26);
            this.txt_Fax.TabIndex = 11;
            // 
            // TxtPhone
            // 
            this.TxtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPhone.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.TxtPhone.Location = new System.Drawing.Point(395, 127);
            this.TxtPhone.MaxLength = 50;
            this.TxtPhone.Name = "TxtPhone";
            this.TxtPhone.Size = new System.Drawing.Size(138, 26);
            this.TxtPhone.TabIndex = 10;
            this.TxtPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPhone_KeyPress);
            // 
            // TxtCountry
            // 
            this.TxtCountry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCountry.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.TxtCountry.Location = new System.Drawing.Point(121, 127);
            this.TxtCountry.MaxLength = 50;
            this.TxtCountry.Name = "TxtCountry";
            this.TxtCountry.Size = new System.Drawing.Size(138, 26);
            this.TxtCountry.TabIndex = 9;
            // 
            // TxtAddress
            // 
            this.TxtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAddress.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.TxtAddress.Location = new System.Drawing.Point(121, 99);
            this.TxtAddress.MaxLength = 255;
            this.TxtAddress.Name = "TxtAddress";
            this.TxtAddress.Size = new System.Drawing.Size(412, 26);
            this.TxtAddress.TabIndex = 8;
            // 
            // TxtDescription
            // 
            this.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDescription.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.TxtDescription.Location = new System.Drawing.Point(121, 43);
            this.TxtDescription.MaxLength = 100;
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(380, 26);
            this.TxtDescription.TabIndex = 5;
            this.TxtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDescription_KeyDown);
            this.TxtDescription.Leave += new System.EventHandler(this.TxtDescription_Leave);
            this.TxtDescription.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDescription_Validating);
            // 
            // TxtShortName
            // 
            this.TxtShortName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtShortName.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.TxtShortName.Location = new System.Drawing.Point(121, 71);
            this.TxtShortName.MaxLength = 10;
            this.TxtShortName.Name = "TxtShortName";
            this.TxtShortName.Size = new System.Drawing.Size(138, 26);
            this.TxtShortName.TabIndex = 6;
            this.TxtShortName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtShortName_KeyDown);
            this.TxtShortName.Leave += new System.EventHandler(this.TxtShortName_Leave);
            this.TxtShortName.Validated += new System.EventHandler(this.TxtShortName_Validated);
            // 
            // lbl_CmpAddress
            // 
            this.lbl_CmpAddress.AutoSize = true;
            this.lbl_CmpAddress.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_CmpAddress.Location = new System.Drawing.Point(5, 100);
            this.lbl_CmpAddress.Name = "lbl_CmpAddress";
            this.lbl_CmpAddress.Size = new System.Drawing.Size(72, 20);
            this.lbl_CmpAddress.TabIndex = 3;
            this.lbl_CmpAddress.Text = "Address";
            // 
            // lbl_CmpName
            // 
            this.lbl_CmpName.AutoSize = true;
            this.lbl_CmpName.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_CmpName.Location = new System.Drawing.Point(5, 46);
            this.lbl_CmpName.Name = "lbl_CmpName";
            this.lbl_CmpName.Size = new System.Drawing.Size(100, 20);
            this.lbl_CmpName.TabIndex = 2;
            this.lbl_CmpName.Text = "Description";
            // 
            // lbl_BranchCode
            // 
            this.lbl_BranchCode.AutoSize = true;
            this.lbl_BranchCode.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_BranchCode.Location = new System.Drawing.Point(5, 74);
            this.lbl_BranchCode.Name = "lbl_BranchCode";
            this.lbl_BranchCode.Size = new System.Drawing.Size(98, 20);
            this.lbl_BranchCode.TabIndex = 1;
            this.lbl_BranchCode.Text = "ShortName";
            // 
            // BtnNew
            // 
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNew.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.Appearance.Options.UseForeColor = true;
            this.BtnNew.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnNew.Location = new System.Drawing.Point(9, 5);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(73, 33);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Tag = "NewButtonCheck";
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.btn_New_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.Appearance.Options.UseForeColor = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(160, 5);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(104, 33);
            this.BtnDelete.TabIndex = 2;
            this.BtnDelete.Tag = "DeleteButtonCheck";
            this.BtnDelete.Text = "&DELETE";
            this.BtnDelete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEdit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnEdit.Appearance.Options.UseFont = true;
            this.BtnEdit.Appearance.Options.UseForeColor = true;
            this.BtnEdit.ImageOptions.Image = global::MrBLL.Properties.Resources.Edit;
            this.BtnEdit.Location = new System.Drawing.Point(83, 5);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(77, 33);
            this.BtnEdit.TabIndex = 1;
            this.BtnEdit.Tag = "EditButtonCheck";
            this.BtnEdit.Text = "&EDIT";
            this.BtnEdit.Click += new System.EventHandler(this.btn_Edit_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseForeColor = true;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.BtnExit.Location = new System.Drawing.Point(457, 5);
            this.BtnExit.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(78, 33);
            this.BtnExit.TabIndex = 3;
            this.BtnExit.Text = "E&XIT";
            this.BtnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btn_Clear
            // 
            this.btn_Clear.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Clear.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn_Clear.Appearance.Options.UseFont = true;
            this.btn_Clear.Appearance.Options.UseForeColor = true;
            this.btn_Clear.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.btn_Clear.Location = new System.Drawing.Point(428, 269);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(107, 35);
            this.btn_Clear.TabIndex = 265;
            this.btn_Clear.Text = "&CANCEL";
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn_Save.Appearance.Options.UseFont = true;
            this.btn_Save.Appearance.Options.UseForeColor = true;
            this.btn_Save.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.btn_Save.Location = new System.Drawing.Point(323, 269);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(104, 35);
            this.btn_Save.TabIndex = 264;
            this.btn_Save.Text = "&SAVE";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.btn_Sync);
            this.StorePanel.Controls.Add(this.clsSeparator2);
            this.StorePanel.Controls.Add(this.btn_Clear);
            this.StorePanel.Controls.Add(this.clsSeparator1);
            this.StorePanel.Controls.Add(this.btn_Save);
            this.StorePanel.Controls.Add(this.BtnDescription);
            this.StorePanel.Controls.Add(this.MskRegistrationDate);
            this.StorePanel.Controls.Add(this.BtnNew);
            this.StorePanel.Controls.Add(this.btn_Branch);
            this.StorePanel.Controls.Add(this.BtnDelete);
            this.StorePanel.Controls.Add(this.txt_BranchName);
            this.StorePanel.Controls.Add(this.label1);
            this.StorePanel.Controls.Add(this.BtnEdit);
            this.StorePanel.Controls.Add(this.txt_ContPerPhone);
            this.StorePanel.Controls.Add(this.lbl_ContactPerPhone);
            this.StorePanel.Controls.Add(this.BtnExit);
            this.StorePanel.Controls.Add(this.txt_ContPerAdd);
            this.StorePanel.Controls.Add(this.lbl_ContPerAdd);
            this.StorePanel.Controls.Add(this.TxtEmail);
            this.StorePanel.Controls.Add(this.TxtDescription);
            this.StorePanel.Controls.Add(this.lbl_Email);
            this.StorePanel.Controls.Add(this.lbl_BranchCode);
            this.StorePanel.Controls.Add(this.txt_ContactPerson);
            this.StorePanel.Controls.Add(this.lbl_CmpName);
            this.StorePanel.Controls.Add(this.lbl_ContactPersion);
            this.StorePanel.Controls.Add(this.lbl_CmpAddress);
            this.StorePanel.Controls.Add(this.lbl_Regdate);
            this.StorePanel.Controls.Add(this.TxtShortName);
            this.StorePanel.Controls.Add(this.label5);
            this.StorePanel.Controls.Add(this.TxtAddress);
            this.StorePanel.Controls.Add(this.lbl_PhoneNo);
            this.StorePanel.Controls.Add(this.TxtCountry);
            this.StorePanel.Controls.Add(this.lbl_Country);
            this.StorePanel.Controls.Add(this.TxtPhone);
            this.StorePanel.Controls.Add(this.txt_Fax);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(540, 308);
            this.StorePanel.TabIndex = 266;
            // 
            // btn_Sync
            // 
            this.btn_Sync.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Sync.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn_Sync.Appearance.Options.UseFont = true;
            this.btn_Sync.Appearance.Options.UseForeColor = true;
            this.btn_Sync.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.btn_Sync.Location = new System.Drawing.Point(213, 269);
            this.btn_Sync.Name = "btn_Sync";
            this.btn_Sync.Size = new System.Drawing.Size(104, 35);
            this.btn_Sync.TabIndex = 267;
            this.btn_Sync.Text = "&SYNC";
            this.btn_Sync.Click += new System.EventHandler(this.btn_Sync_Click);
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(4, 266);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(531, 2);
            this.clsSeparator2.TabIndex = 41;
            this.clsSeparator2.TabStop = false;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(9, 39);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(526, 2);
            this.clsSeparator1.TabIndex = 40;
            this.clsSeparator1.TabStop = false;
            // 
            // BtnDescription
            // 
            this.BtnDescription.CausesValidation = false;
            this.BtnDescription.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDescription.Location = new System.Drawing.Point(503, 43);
            this.BtnDescription.Name = "BtnDescription";
            this.BtnDescription.Size = new System.Drawing.Size(31, 26);
            this.BtnDescription.TabIndex = 39;
            this.BtnDescription.TabStop = false;
            this.BtnDescription.UseVisualStyleBackColor = true;
            this.BtnDescription.Click += new System.EventHandler(this.BtnDescription_Click);
            // 
            // FrmCompanyUnitSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(540, 308);
            this.Controls.Add(this.StorePanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmCompanyUnitSetup";
            this.Tag = "Unit";
            this.Text = "Company Unit Setup";
            this.Load += new System.EventHandler(this.FrmCompanyUnitSetup_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmCompanyUnitSetup_KeyPress);
            this.StorePanel.ResumeLayout(false);
            this.StorePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl_ContactPerPhone;
        private System.Windows.Forms.Label lbl_ContPerAdd;
        private System.Windows.Forms.Label lbl_Email;
        private System.Windows.Forms.Label lbl_ContactPersion;
        private System.Windows.Forms.Label lbl_Regdate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_PhoneNo;
        private System.Windows.Forms.Label lbl_Country;
        private System.Windows.Forms.Label lbl_CmpAddress;
        private System.Windows.Forms.Label lbl_CmpName;
        private System.Windows.Forms.Label lbl_BranchCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Branch;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton btn_Clear;
        private DevExpress.XtraEditors.SimpleButton btn_Save;
        private System.Windows.Forms.Button BtnDescription;
        private ClsSeparator clsSeparator1;
        private ClsSeparator clsSeparator2;
        private MrTextBox txt_ContPerPhone;
        private MrTextBox txt_ContPerAdd;
        private MrTextBox TxtEmail;
        private MrTextBox txt_ContactPerson;
        private MrTextBox txt_Fax;
        private MrTextBox TxtPhone;
        private MrTextBox TxtCountry;
        private MrTextBox TxtAddress;
        private MrTextBox TxtDescription;
        private MrTextBox TxtShortName;
        private MrTextBox txt_BranchName;
        private MrMaskedTextBox MskRegistrationDate;
        private MrPanel StorePanel;
        private DevExpress.XtraEditors.SimpleButton btn_Sync;
    }
}