using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Utility.MrLicense
{
    partial class FrmLicenseGenerator
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
            this.label1 = new System.Windows.Forms.Label();
            this.TxtCompany = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.CmbSoftwareModule = new System.Windows.Forms.ComboBox();
            this.TxtNoOfUsers = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.TxtRegDays = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.MskExpireDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.MskStartDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtRegistrationBy = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtRequestBy = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtSerialNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtAddress = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtClientId = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.btnRegistration = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnMail = new DevExpress.XtraEditors.SimpleButton();
            this.PanelHeader = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.mrGroup1 = new MrDAL.Control.ControlsEx.Control.MrGroup();
            this.TxtMacAddress = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.ChkIRDRegister = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ChkOnline = new System.Windows.Forms.CheckBox();
            this.PanelHeader.SuspendLayout();
            this.mrGroup1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(5, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Company";
            // 
            // TxtCompany
            // 
            this.TxtCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCompany.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCompany.Location = new System.Drawing.Point(140, 15);
            this.TxtCompany.Name = "TxtCompany";
            this.TxtCompany.Size = new System.Drawing.Size(526, 26);
            this.TxtCompany.TabIndex = 0;
            this.TxtCompany.Leave += new System.EventHandler(this.TxtClientName_Leave);
            // 
            // CmbSoftwareModule
            // 
            this.CmbSoftwareModule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbSoftwareModule.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbSoftwareModule.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbSoftwareModule.FormattingEnabled = true;
            this.CmbSoftwareModule.Items.AddRange(new object[] {
            "AIMS",
            "DEVELOPER",
            "ERP",
            "HOSPITAL",
            "POS",
            "RESTRO"});
            this.CmbSoftwareModule.Location = new System.Drawing.Point(140, 191);
            this.CmbSoftwareModule.Name = "CmbSoftwareModule";
            this.CmbSoftwareModule.Size = new System.Drawing.Size(526, 28);
            this.CmbSoftwareModule.Sorted = true;
            this.CmbSoftwareModule.TabIndex = 6;
            // 
            // TxtNoOfUsers
            // 
            this.TxtNoOfUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNoOfUsers.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtNoOfUsers.Location = new System.Drawing.Point(140, 253);
            this.TxtNoOfUsers.Name = "TxtNoOfUsers";
            this.TxtNoOfUsers.PasswordChar = '*';
            this.TxtNoOfUsers.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TxtNoOfUsers.Size = new System.Drawing.Size(152, 26);
            this.TxtNoOfUsers.TabIndex = 10;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label10.Location = new System.Drawing.Point(5, 196);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 20);
            this.label10.TabIndex = 18;
            this.label10.Text = "Module";
            // 
            // TxtRegDays
            // 
            this.TxtRegDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtRegDays.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtRegDays.Location = new System.Drawing.Point(140, 224);
            this.TxtRegDays.Name = "TxtRegDays";
            this.TxtRegDays.PasswordChar = '*';
            this.TxtRegDays.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TxtRegDays.Size = new System.Drawing.Size(152, 26);
            this.TxtRegDays.TabIndex = 7;
            this.TxtRegDays.TextChanged += new System.EventHandler(this.TxtRegDays_TextChanged);
            this.TxtRegDays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtRegDays_KeyPress);
            this.TxtRegDays.Leave += new System.EventHandler(this.TxtRegDays_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label9.Location = new System.Drawing.Point(5, 229);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 20);
            this.label9.TabIndex = 16;
            this.label9.Text = "No. of Days";
            // 
            // MskExpireDate
            // 
            this.MskExpireDate.BackColor = System.Drawing.SystemColors.Window;
            this.MskExpireDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskExpireDate.Enabled = false;
            this.MskExpireDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskExpireDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskExpireDate.Location = new System.Drawing.Point(420, 225);
            this.MskExpireDate.Mask = "00/00/0000";
            this.MskExpireDate.Name = "MskExpireDate";
            this.MskExpireDate.ReadOnly = true;
            this.MskExpireDate.Size = new System.Drawing.Size(121, 26);
            this.MskExpireDate.TabIndex = 9;
            this.MskExpireDate.ValidatingType = typeof(System.DateTime);
            // 
            // MskStartDate
            // 
            this.MskStartDate.BackColor = System.Drawing.SystemColors.Window;
            this.MskStartDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskStartDate.Enabled = false;
            this.MskStartDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskStartDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskStartDate.Location = new System.Drawing.Point(295, 225);
            this.MskStartDate.Mask = "00/00/0000";
            this.MskStartDate.Name = "MskStartDate";
            this.MskStartDate.ReadOnly = true;
            this.MskStartDate.Size = new System.Drawing.Size(122, 26);
            this.MskStartDate.TabIndex = 8;
            this.MskStartDate.ValidatingType = typeof(System.DateTime);
            this.MskStartDate.Enter += new System.EventHandler(this.MskRegDate_Enter);
            this.MskStartDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MskRegDate_KeyDown);
            this.MskStartDate.Leave += new System.EventHandler(this.MskRegDate_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(5, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "Register By";
            // 
            // TxtRegistrationBy
            // 
            this.TxtRegistrationBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtRegistrationBy.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtRegistrationBy.Location = new System.Drawing.Point(140, 159);
            this.TxtRegistrationBy.Name = "TxtRegistrationBy";
            this.TxtRegistrationBy.PasswordChar = '*';
            this.TxtRegistrationBy.Size = new System.Drawing.Size(526, 26);
            this.TxtRegistrationBy.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(5, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Request By";
            // 
            // TxtRequestBy
            // 
            this.TxtRequestBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtRequestBy.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtRequestBy.Location = new System.Drawing.Point(140, 72);
            this.TxtRequestBy.Name = "TxtRequestBy";
            this.TxtRequestBy.PasswordChar = '*';
            this.TxtRequestBy.Size = new System.Drawing.Size(526, 26);
            this.TxtRequestBy.TabIndex = 2;
            this.TxtRequestBy.Leave += new System.EventHandler(this.TxtRequestBy_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(5, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Serial No.";
            // 
            // TxtSerialNo
            // 
            this.TxtSerialNo.BackColor = System.Drawing.SystemColors.Window;
            this.TxtSerialNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSerialNo.Enabled = false;
            this.TxtSerialNo.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSerialNo.Location = new System.Drawing.Point(140, 101);
            this.TxtSerialNo.Name = "TxtSerialNo";
            this.TxtSerialNo.ReadOnly = true;
            this.TxtSerialNo.Size = new System.Drawing.Size(286, 26);
            this.TxtSerialNo.TabIndex = 3;
            this.TxtSerialNo.WordWrap = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(5, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Address";
            // 
            // TxtAddress
            // 
            this.TxtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAddress.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAddress.Location = new System.Drawing.Point(140, 43);
            this.TxtAddress.Name = "TxtAddress";
            this.TxtAddress.Size = new System.Drawing.Size(526, 26);
            this.TxtAddress.TabIndex = 1;
            this.TxtAddress.Leave += new System.EventHandler(this.TxtClientAddress_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(5, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Registration No";
            // 
            // TxtClientId
            // 
            this.TxtClientId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtClientId.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtClientId.Location = new System.Drawing.Point(140, 130);
            this.TxtClientId.Name = "TxtClientId";
            this.TxtClientId.PasswordChar = '*';
            this.TxtClientId.Size = new System.Drawing.Size(286, 26);
            this.TxtClientId.TabIndex = 4;
            this.TxtClientId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtClientId_KeyPress);
            // 
            // btnRegistration
            // 
            this.btnRegistration.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistration.Appearance.Options.UseFont = true;
            this.btnRegistration.ImageOptions.Image = global::MrBLL.Properties.Resources.OK;
            this.btnRegistration.Location = new System.Drawing.Point(431, 288);
            this.btnRegistration.Name = "btnRegistration";
            this.btnRegistration.Size = new System.Drawing.Size(123, 33);
            this.btnRegistration.TabIndex = 12;
            this.btnRegistration.Text = "&REGISTER";
            this.btnRegistration.Click += new System.EventHandler(this.BtnRegistration_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.btnCancel.Location = new System.Drawing.Point(556, 288);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(106, 33);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "&CANCEL";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnMail
            // 
            this.btnMail.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMail.Appearance.Options.UseFont = true;
            this.btnMail.Location = new System.Drawing.Point(359, 288);
            this.btnMail.Name = "btnMail";
            this.btnMail.Size = new System.Drawing.Size(71, 33);
            this.btnMail.TabIndex = 14;
            this.btnMail.Text = "&MAIL";
            this.btnMail.Visible = false;
            this.btnMail.Click += new System.EventHandler(this.BtnMail_Click);
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.mrGroup1);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(672, 327);
            this.PanelHeader.TabIndex = 0;
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrDAL.Control.ControlsEx.Control.MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.TxtMacAddress);
            this.mrGroup1.Controls.Add(this.label8);
            this.mrGroup1.Controls.Add(this.clsSeparator1);
            this.mrGroup1.Controls.Add(this.TxtCompany);
            this.mrGroup1.Controls.Add(this.ChkIRDRegister);
            this.mrGroup1.Controls.Add(this.MskStartDate);
            this.mrGroup1.Controls.Add(this.label7);
            this.mrGroup1.Controls.Add(this.label9);
            this.mrGroup1.Controls.Add(this.label10);
            this.mrGroup1.Controls.Add(this.btnMail);
            this.mrGroup1.Controls.Add(this.MskExpireDate);
            this.mrGroup1.Controls.Add(this.btnCancel);
            this.mrGroup1.Controls.Add(this.TxtRegDays);
            this.mrGroup1.Controls.Add(this.ChkOnline);
            this.mrGroup1.Controls.Add(this.label6);
            this.mrGroup1.Controls.Add(this.btnRegistration);
            this.mrGroup1.Controls.Add(this.label5);
            this.mrGroup1.Controls.Add(this.TxtAddress);
            this.mrGroup1.Controls.Add(this.TxtNoOfUsers);
            this.mrGroup1.Controls.Add(this.TxtRequestBy);
            this.mrGroup1.Controls.Add(this.label4);
            this.mrGroup1.Controls.Add(this.TxtSerialNo);
            this.mrGroup1.Controls.Add(this.label3);
            this.mrGroup1.Controls.Add(this.TxtClientId);
            this.mrGroup1.Controls.Add(this.label1);
            this.mrGroup1.Controls.Add(this.TxtRegistrationBy);
            this.mrGroup1.Controls.Add(this.label2);
            this.mrGroup1.Controls.Add(this.CmbSoftwareModule);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrGroup1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "";
            this.mrGroup1.Location = new System.Drawing.Point(0, 0);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = true;
            this.mrGroup1.RoundCorners = 10;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = false;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(672, 327);
            this.mrGroup1.TabIndex = 1;
            // 
            // TxtMacAddress
            // 
            this.TxtMacAddress.BackColor = System.Drawing.SystemColors.Window;
            this.TxtMacAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMacAddress.Enabled = false;
            this.TxtMacAddress.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtMacAddress.Location = new System.Drawing.Point(477, 101);
            this.TxtMacAddress.Name = "TxtMacAddress";
            this.TxtMacAddress.ReadOnly = true;
            this.TxtMacAddress.Size = new System.Drawing.Size(189, 26);
            this.TxtMacAddress.TabIndex = 29;
            this.TxtMacAddress.WordWrap = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label8.Location = new System.Drawing.Point(426, 104);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 20);
            this.label8.TabIndex = 28;
            this.label8.Text = "MAC";
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(3, 283);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(674, 2);
            this.clsSeparator1.TabIndex = 27;
            this.clsSeparator1.TabStop = false;
            // 
            // ChkIRDRegister
            // 
            this.ChkIRDRegister.AutoSize = true;
            this.ChkIRDRegister.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIRDRegister.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ChkIRDRegister.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIRDRegister.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIRDRegister.Location = new System.Drawing.Point(127, 295);
            this.ChkIRDRegister.Name = "ChkIRDRegister";
            this.ChkIRDRegister.Size = new System.Drawing.Size(152, 25);
            this.ChkIRDRegister.TabIndex = 11;
            this.ChkIRDRegister.Text = "Is IRD Register";
            this.ChkIRDRegister.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label7.Location = new System.Drawing.Point(5, 256);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 20);
            this.label7.TabIndex = 25;
            this.label7.Text = "Nodes";
            // 
            // ChkOnline
            // 
            this.ChkOnline.AutoSize = true;
            this.ChkOnline.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkOnline.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ChkOnline.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkOnline.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkOnline.Location = new System.Drawing.Point(9, 295);
            this.ChkOnline.Name = "ChkOnline";
            this.ChkOnline.Size = new System.Drawing.Size(101, 25);
            this.ChkOnline.TabIndex = 15;
            this.ChkOnline.Text = "IsOnline";
            this.ChkOnline.UseVisualStyleBackColor = true;
            // 
            // FrmLicenseGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(672, 327);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLicenseGenerator";
            this.ShowIcon = false;
            this.Text = "Online Software Registration";
            this.Load += new System.EventHandler(this.FrmOnlineRegistration_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmOnlineRegistration_KeyPress);
            this.PanelHeader.ResumeLayout(false);
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private DevExpress.XtraEditors.SimpleButton btnRegistration;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox CmbSoftwareModule;
        private DevExpress.XtraEditors.SimpleButton btnMail;
        private System.Windows.Forms.CheckBox ChkOnline;
        private System.Windows.Forms.CheckBox ChkIRDRegister;
        private System.Windows.Forms.Label label7;
        private ClsSeparator clsSeparator1;
        private MrGroup mrGroup1;
        private MrTextBox TxtCompany;
        private MrTextBox TxtAddress;
        private MrTextBox TxtClientId;
        private MrTextBox TxtRegistrationBy;
        private MrTextBox TxtRequestBy;
        private MrTextBox TxtSerialNo;
        private MrMaskedTextBox MskStartDate;
        private MrTextBox TxtRegDays;
        private MrMaskedTextBox MskExpireDate;
        private MrTextBox TxtNoOfUsers;
        private MrPanel PanelHeader;
        private MrTextBox TxtMacAddress;
        private System.Windows.Forms.Label label8;
    }
}