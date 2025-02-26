using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Setup.UserSetup
{
    partial class FrmUserSetup
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
            this.roundPanel2 = new MrDAL.Control.ControlsEx.Control.RoundPanel();
            this.BtnUser = new System.Windows.Forms.Button();
            this.TxtUser = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_UserAlias = new System.Windows.Forms.Label();
            this.TxtUserFullName = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_UserName = new System.Windows.Forms.Label();
            this.TxtPassword = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_Password = new System.Windows.Forms.Label();
            this.TxtConfirm = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtMobileNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_MobileNo = new System.Windows.Forms.Label();
            this.TxtTelPhoneNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtAddress = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_Address = new System.Windows.Forms.Label();
            this.CmbUserRole = new System.Windows.Forms.ComboBox();
            this.lbl_UserType = new System.Windows.Forms.Label();
            this.TxtEmail = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_Email = new System.Windows.Forms.Label();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.ChkActive = new System.Windows.Forms.CheckBox();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.PanelHeader = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.roundPanel2.SuspendLayout();
            this.PanelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // roundPanel2
            // 
            this.roundPanel2.BackColor = System.Drawing.Color.Transparent;
            this.roundPanel2.Controls.Add(this.BtnUser);
            this.roundPanel2.Controls.Add(this.TxtUser);
            this.roundPanel2.Controls.Add(this.lbl_UserAlias);
            this.roundPanel2.Controls.Add(this.TxtUserFullName);
            this.roundPanel2.Controls.Add(this.lbl_UserName);
            this.roundPanel2.Controls.Add(this.TxtPassword);
            this.roundPanel2.Controls.Add(this.lbl_Password);
            this.roundPanel2.Controls.Add(this.TxtConfirm);
            this.roundPanel2.Controls.Add(this.TxtMobileNo);
            this.roundPanel2.Controls.Add(this.lbl_MobileNo);
            this.roundPanel2.Controls.Add(this.TxtTelPhoneNo);
            this.roundPanel2.Controls.Add(this.TxtAddress);
            this.roundPanel2.Controls.Add(this.lbl_Address);
            this.roundPanel2.Controls.Add(this.CmbUserRole);
            this.roundPanel2.Controls.Add(this.lbl_UserType);
            this.roundPanel2.Controls.Add(this.TxtEmail);
            this.roundPanel2.Controls.Add(this.lbl_Email);
            this.roundPanel2.Location = new System.Drawing.Point(5, 55);
            this.roundPanel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.roundPanel2.Name = "roundPanel2";
            this.roundPanel2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.roundPanel2.Radious = 25;
            this.roundPanel2.Size = new System.Drawing.Size(592, 279);
            this.roundPanel2.TabIndex = 4;
            this.roundPanel2.TabStop = false;
            this.roundPanel2.Text = "USER SETUP";
            this.roundPanel2.TitleBackColor = System.Drawing.Color.DarkSlateBlue;
            this.roundPanel2.TitleFont = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roundPanel2.TitleForeColor = System.Drawing.Color.White;
            this.roundPanel2.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
            // 
            // BtnUser
            // 
            this.BtnUser.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnUser.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnUser.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnUser.Location = new System.Drawing.Point(431, 37);
            this.BtnUser.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnUser.Name = "BtnUser";
            this.BtnUser.Size = new System.Drawing.Size(36, 30);
            this.BtnUser.TabIndex = 312;
            this.BtnUser.TabStop = false;
            this.BtnUser.UseVisualStyleBackColor = false;
            this.BtnUser.Click += new System.EventHandler(this.BtnUser_Click);
            // 
            // TxtUser
            // 
            this.TxtUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtUser.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.TxtUser.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUser.Location = new System.Drawing.Point(161, 34);
            this.TxtUser.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtUser.Name = "TxtUser";
            this.TxtUser.Size = new System.Drawing.Size(265, 31);
            this.TxtUser.TabIndex = 0;
            this.TxtUser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtUser_KeyDown);
            this.TxtUser.Leave += new System.EventHandler(this.TxtUser_Leave);
            this.TxtUser.Validating += new System.ComponentModel.CancelEventHandler(this.TxtUser_Validating);
            // 
            // lbl_UserAlias
            // 
            this.lbl_UserAlias.AutoSize = true;
            this.lbl_UserAlias.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_UserAlias.Location = new System.Drawing.Point(17, 38);
            this.lbl_UserAlias.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_UserAlias.Name = "lbl_UserAlias";
            this.lbl_UserAlias.Size = new System.Drawing.Size(118, 23);
            this.lbl_UserAlias.TabIndex = 0;
            this.lbl_UserAlias.Text = "User Name";
            // 
            // TxtUserFullName
            // 
            this.TxtUserFullName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtUserFullName.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUserFullName.Location = new System.Drawing.Point(161, 69);
            this.TxtUserFullName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtUserFullName.Name = "TxtUserFullName";
            this.TxtUserFullName.Size = new System.Drawing.Size(422, 31);
            this.TxtUserFullName.TabIndex = 1;
            // 
            // lbl_UserName
            // 
            this.lbl_UserName.AutoSize = true;
            this.lbl_UserName.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_UserName.Location = new System.Drawing.Point(17, 74);
            this.lbl_UserName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_UserName.Name = "lbl_UserName";
            this.lbl_UserName.Size = new System.Drawing.Size(111, 23);
            this.lbl_UserName.TabIndex = 1;
            this.lbl_UserName.Text = "Full Name";
            // 
            // TxtPassword
            // 
            this.TxtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPassword.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPassword.Location = new System.Drawing.Point(161, 102);
            this.TxtPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.PasswordChar = '*';
            this.TxtPassword.Size = new System.Drawing.Size(202, 31);
            this.TxtPassword.TabIndex = 2;
            this.TxtPassword.Validating += new System.ComponentModel.CancelEventHandler(this.TxtPassword_Validating);
            // 
            // lbl_Password
            // 
            this.lbl_Password.AutoSize = true;
            this.lbl_Password.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Password.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_Password.Location = new System.Drawing.Point(17, 103);
            this.lbl_Password.Name = "lbl_Password";
            this.lbl_Password.Size = new System.Drawing.Size(102, 23);
            this.lbl_Password.TabIndex = 7;
            this.lbl_Password.Text = "Password";
            // 
            // TxtConfirm
            // 
            this.TxtConfirm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtConfirm.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtConfirm.Location = new System.Drawing.Point(380, 102);
            this.TxtConfirm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TxtConfirm.Name = "TxtConfirm";
            this.TxtConfirm.PasswordChar = '*';
            this.TxtConfirm.Size = new System.Drawing.Size(202, 31);
            this.TxtConfirm.TabIndex = 3;
            this.TxtConfirm.Validating += new System.ComponentModel.CancelEventHandler(this.TxtConfirm_Validating);
            // 
            // TxtMobileNo
            // 
            this.TxtMobileNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMobileNo.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtMobileNo.Location = new System.Drawing.Point(161, 135);
            this.TxtMobileNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtMobileNo.Name = "TxtMobileNo";
            this.TxtMobileNo.Size = new System.Drawing.Size(202, 31);
            this.TxtMobileNo.TabIndex = 4;
            // 
            // lbl_MobileNo
            // 
            this.lbl_MobileNo.AutoSize = true;
            this.lbl_MobileNo.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MobileNo.Location = new System.Drawing.Point(17, 139);
            this.lbl_MobileNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_MobileNo.Name = "lbl_MobileNo";
            this.lbl_MobileNo.Size = new System.Drawing.Size(112, 23);
            this.lbl_MobileNo.TabIndex = 42;
            this.lbl_MobileNo.Text = "Mobile No.";
            // 
            // TxtTelPhoneNo
            // 
            this.TxtTelPhoneNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTelPhoneNo.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTelPhoneNo.Location = new System.Drawing.Point(380, 137);
            this.TxtTelPhoneNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtTelPhoneNo.Name = "TxtTelPhoneNo";
            this.TxtTelPhoneNo.Size = new System.Drawing.Size(202, 31);
            this.TxtTelPhoneNo.TabIndex = 5;
            // 
            // TxtAddress
            // 
            this.TxtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAddress.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAddress.Location = new System.Drawing.Point(161, 170);
            this.TxtAddress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtAddress.Name = "TxtAddress";
            this.TxtAddress.Size = new System.Drawing.Size(421, 31);
            this.TxtAddress.TabIndex = 6;
            // 
            // lbl_Address
            // 
            this.lbl_Address.AutoSize = true;
            this.lbl_Address.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Address.Location = new System.Drawing.Point(17, 171);
            this.lbl_Address.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Address.Name = "lbl_Address";
            this.lbl_Address.Size = new System.Drawing.Size(88, 23);
            this.lbl_Address.TabIndex = 34;
            this.lbl_Address.Text = "Address";
            // 
            // CmbUserRole
            // 
            this.CmbUserRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbUserRole.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbUserRole.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbUserRole.FormattingEnabled = true;
            this.CmbUserRole.Location = new System.Drawing.Point(163, 204);
            this.CmbUserRole.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CmbUserRole.Name = "CmbUserRole";
            this.CmbUserRole.Size = new System.Drawing.Size(201, 31);
            this.CmbUserRole.TabIndex = 7;
            // 
            // lbl_UserType
            // 
            this.lbl_UserType.AutoSize = true;
            this.lbl_UserType.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_UserType.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_UserType.Location = new System.Drawing.Point(17, 208);
            this.lbl_UserType.Name = "lbl_UserType";
            this.lbl_UserType.Size = new System.Drawing.Size(104, 23);
            this.lbl_UserType.TabIndex = 36;
            this.lbl_UserType.Text = "User Role";
            // 
            // TxtEmail
            // 
            this.TxtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtEmail.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtEmail.Location = new System.Drawing.Point(161, 241);
            this.TxtEmail.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtEmail.Name = "TxtEmail";
            this.TxtEmail.Size = new System.Drawing.Size(421, 31);
            this.TxtEmail.TabIndex = 8;
            // 
            // lbl_Email
            // 
            this.lbl_Email.AutoSize = true;
            this.lbl_Email.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Email.Location = new System.Drawing.Point(17, 244);
            this.lbl_Email.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Email.Name = "lbl_Email";
            this.lbl_Email.Size = new System.Drawing.Size(65, 23);
            this.lbl_Email.TabIndex = 40;
            this.lbl_Email.Text = "Email";
            // 
            // BtnNew
            // 
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnNew.Location = new System.Drawing.Point(7, 6);
            this.BtnNew.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(99, 42);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Tag = "NewButtonCheck";
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(212, 6);
            this.BtnDelete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(137, 42);
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
            this.BtnEdit.Location = new System.Drawing.Point(107, 6);
            this.BtnEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(104, 42);
            this.BtnEdit.TabIndex = 1;
            this.BtnEdit.Tag = "EditButtonCheck";
            this.BtnEdit.Text = "&EDIT";
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(319, 342);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(121, 42);
            this.BtnSave.TabIndex = 5;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnExit.Location = new System.Drawing.Point(497, 4);
            this.BtnExit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(100, 42);
            this.BtnExit.TabIndex = 3;
            this.BtnExit.Text = "E&XIT";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(440, 342);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(140, 42);
            this.BtnCancel.TabIndex = 6;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // ChkActive
            // 
            this.ChkActive.AutoSize = true;
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkActive.Location = new System.Drawing.Point(9, 348);
            this.ChkActive.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkActive.Size = new System.Drawing.Size(97, 27);
            this.ChkActive.TabIndex = 7;
            this.ChkActive.Text = "Status";
            this.ChkActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkActive.UseVisualStyleBackColor = true;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(12, 50);
            this.clsSeparator1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clsSeparator1.Size = new System.Drawing.Size(588, 2);
            this.clsSeparator1.TabIndex = 59;
            this.clsSeparator1.TabStop = false;
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(12, 337);
            this.clsSeparator2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clsSeparator2.Size = new System.Drawing.Size(581, 2);
            this.clsSeparator2.TabIndex = 60;
            this.clsSeparator2.TabStop = false;
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.clsSeparator2);
            this.PanelHeader.Controls.Add(this.clsSeparator1);
            this.PanelHeader.Controls.Add(this.ChkActive);
            this.PanelHeader.Controls.Add(this.BtnCancel);
            this.PanelHeader.Controls.Add(this.BtnExit);
            this.PanelHeader.Controls.Add(this.BtnSave);
            this.PanelHeader.Controls.Add(this.BtnEdit);
            this.PanelHeader.Controls.Add(this.BtnDelete);
            this.PanelHeader.Controls.Add(this.BtnNew);
            this.PanelHeader.Controls.Add(this.roundPanel2);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(605, 390);
            this.PanelHeader.TabIndex = 0;
            // 
            // FrmUserSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(605, 390);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.MaximizeBox = false;
            this.Name = "FrmUserSetup";
            this.ShowIcon = false;
            this.Tag = "User Master";
            this.Text = "USER DETAILS SETUP";
            this.Load += new System.EventHandler(this.FrmUserSetup_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmUserSetup_KeyPress);
            this.roundPanel2.ResumeLayout(false);
            this.roundPanel2.PerformLayout();
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private RoundPanel roundPanel2;
        private System.Windows.Forms.Button BtnUser;
        private MrTextBox TxtUser;
        private System.Windows.Forms.Label lbl_UserAlias;
        private MrTextBox TxtUserFullName;
        private System.Windows.Forms.Label lbl_UserName;
        private MrTextBox TxtPassword;
        private System.Windows.Forms.Label lbl_Password;
        private MrTextBox TxtConfirm;
        private MrTextBox TxtMobileNo;
        private System.Windows.Forms.Label lbl_MobileNo;
        private MrTextBox TxtTelPhoneNo;
        private MrTextBox TxtAddress;
        private System.Windows.Forms.Label lbl_Address;
        private System.Windows.Forms.ComboBox CmbUserRole;
        private System.Windows.Forms.Label lbl_UserType;
        private MrTextBox TxtEmail;
        private System.Windows.Forms.Label lbl_Email;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private System.Windows.Forms.CheckBox ChkActive;
        private ClsSeparator clsSeparator1;
        private ClsSeparator clsSeparator2;
        private MrPanel PanelHeader;
    }
}