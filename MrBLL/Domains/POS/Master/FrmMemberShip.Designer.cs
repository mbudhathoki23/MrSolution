using MrDAL.Control.ControlsEx.Control;

 namespace MrBLL.Domains.POS.Master
{
    partial class FrmMemberShip
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
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.ChkActive = new System.Windows.Forms.CheckBox();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.TxtDescription = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.StorePanel = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.CmbPriceTag = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.BtnView = new DevExpress.XtraEditors.SimpleButton();
            this.TxtShortName = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtMemberId = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.MskStartDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.MskExpireDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.BtnMemberType = new System.Windows.Forms.Button();
            this.TxtMemberType = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.BtnLedger = new System.Windows.Forms.Button();
            this.BtnDescription = new System.Windows.Forms.Button();
            this.TxtEmail = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtLedger = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtPhoneNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.StorePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(12, 234);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(528, 2);
            this.clsSeparator2.TabIndex = 12;
            this.clsSeparator2.TabStop = false;
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(346, 237);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(89, 33);
            this.BtnSave.TabIndex = 16;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.BtnCancel.Location = new System.Drawing.Point(436, 237);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(104, 33);
            this.BtnCancel.TabIndex = 17;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // ChkActive
            // 
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Location = new System.Drawing.Point(11, 241);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkActive.Size = new System.Drawing.Size(109, 24);
            this.ChkActive.TabIndex = 15;
            this.ChkActive.Text = "Active";
            this.ChkActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkActive.ThreeState = true;
            this.ChkActive.UseVisualStyleBackColor = true;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(3, 40);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(536, 2);
            this.clsSeparator1.TabIndex = 11;
            this.clsSeparator1.TabStop = false;
            // 
            // TxtDescription
            // 
            this.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDescription.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtDescription.Location = new System.Drawing.Point(118, 74);
            this.TxtDescription.MaxLength = 200;
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(397, 25);
            this.TxtDescription.TabIndex = 7;
            this.TxtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDescription_KeyDown);
            this.TxtDescription.Leave += new System.EventHandler(this.TxtDescription_Leave);
            this.TxtDescription.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDescription_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Description";
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.button1);
            this.StorePanel.Controls.Add(this.CmbPriceTag);
            this.StorePanel.Controls.Add(this.label9);
            this.StorePanel.Controls.Add(this.BtnView);
            this.StorePanel.Controls.Add(this.TxtShortName);
            this.StorePanel.Controls.Add(this.label2);
            this.StorePanel.Controls.Add(this.TxtMemberId);
            this.StorePanel.Controls.Add(this.label7);
            this.StorePanel.Controls.Add(this.MskStartDate);
            this.StorePanel.Controls.Add(this.MskExpireDate);
            this.StorePanel.Controls.Add(this.label11);
            this.StorePanel.Controls.Add(this.label8);
            this.StorePanel.Controls.Add(this.BtnMemberType);
            this.StorePanel.Controls.Add(this.TxtMemberType);
            this.StorePanel.Controls.Add(this.label6);
            this.StorePanel.Controls.Add(this.BtnLedger);
            this.StorePanel.Controls.Add(this.BtnDescription);
            this.StorePanel.Controls.Add(this.TxtEmail);
            this.StorePanel.Controls.Add(this.label5);
            this.StorePanel.Controls.Add(this.TxtLedger);
            this.StorePanel.Controls.Add(this.label4);
            this.StorePanel.Controls.Add(this.TxtPhoneNo);
            this.StorePanel.Controls.Add(this.label3);
            this.StorePanel.Controls.Add(this.clsSeparator2);
            this.StorePanel.Controls.Add(this.BtnSave);
            this.StorePanel.Controls.Add(this.BtnCancel);
            this.StorePanel.Controls.Add(this.ChkActive);
            this.StorePanel.Controls.Add(this.clsSeparator1);
            this.StorePanel.Controls.Add(this.BtnExit);
            this.StorePanel.Controls.Add(this.TxtDescription);
            this.StorePanel.Controls.Add(this.BtnDelete);
            this.StorePanel.Controls.Add(this.label1);
            this.StorePanel.Controls.Add(this.BtnEdit);
            this.StorePanel.Controls.Add(this.BtnNew);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(544, 275);
            this.StorePanel.TabIndex = 0;
            this.StorePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.StorePanel_Paint);
            // 
            // button1
            // 
            this.button1.Image = global::MrBLL.Properties.Resources.ReportRefresh;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(256, 237);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 35);
            this.button1.TabIndex = 18;
            this.button1.Text = "&SYNC";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.BtnSync_Click);
            // 
            // CmbPriceTag
            // 
            this.CmbPriceTag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbPriceTag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbPriceTag.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.CmbPriceTag.FormattingEnabled = true;
            this.CmbPriceTag.Items.AddRange(new object[] {
            "MRP",
            "Trade",
            "Wholesale",
            "Retail",
            "Dealer",
            "Resellar",
            "SalesRate"});
            this.CmbPriceTag.Location = new System.Drawing.Point(377, 100);
            this.CmbPriceTag.MaxLength = 50;
            this.CmbPriceTag.Name = "CmbPriceTag";
            this.CmbPriceTag.Size = new System.Drawing.Size(138, 27);
            this.CmbPriceTag.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(289, 104);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 19);
            this.label9.TabIndex = 325;
            this.label9.Text = "Price Tag";
            // 
            // BtnView
            // 
            this.BtnView.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnView.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnView.Appearance.Options.UseFont = true;
            this.BtnView.Appearance.Options.UseForeColor = true;
            this.BtnView.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnView.Location = new System.Drawing.Point(256, 5);
            this.BtnView.Margin = new System.Windows.Forms.Padding(4);
            this.BtnView.Name = "BtnView";
            this.BtnView.Size = new System.Drawing.Size(86, 33);
            this.BtnView.TabIndex = 3;
            this.BtnView.Tag = "ViewButtonCheck";
            this.BtnView.Text = "&VIEW";
            this.BtnView.Click += new System.EventHandler(this.BtnView_Click);
            // 
            // TxtShortName
            // 
            this.TxtShortName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtShortName.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtShortName.Location = new System.Drawing.Point(118, 101);
            this.TxtShortName.MaxLength = 50;
            this.TxtShortName.Name = "TxtShortName";
            this.TxtShortName.Size = new System.Drawing.Size(170, 25);
            this.TxtShortName.TabIndex = 8;
            this.TxtShortName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtShortName_KeyDown);
            this.TxtShortName.Leave += new System.EventHandler(this.TxtShortName_Leave);
            this.TxtShortName.Validating += new System.ComponentModel.CancelEventHandler(this.TxtShortName_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 19);
            this.label2.TabIndex = 322;
            this.label2.Text = "Short Name";
            // 
            // TxtMemberId
            // 
            this.TxtMemberId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMemberId.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtMemberId.Location = new System.Drawing.Point(118, 48);
            this.TxtMemberId.MaxLength = 50;
            this.TxtMemberId.Name = "TxtMemberId";
            this.TxtMemberId.Size = new System.Drawing.Size(170, 25);
            this.TxtMemberId.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 19);
            this.label7.TabIndex = 321;
            this.label7.Text = "Member Id";
            // 
            // MskStartDate
            // 
            this.MskStartDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskStartDate.Enabled = false;
            this.MskStartDate.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.MskStartDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskStartDate.Location = new System.Drawing.Point(118, 207);
            this.MskStartDate.Mask = "00/00/0000";
            this.MskStartDate.Name = "MskStartDate";
            this.MskStartDate.Size = new System.Drawing.Size(118, 25);
            this.MskStartDate.TabIndex = 13;
            // 
            // MskExpireDate
            // 
            this.MskExpireDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskExpireDate.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.MskExpireDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskExpireDate.Location = new System.Drawing.Point(397, 207);
            this.MskExpireDate.Mask = "00/00/0000";
            this.MskExpireDate.Name = "MskExpireDate";
            this.MskExpireDate.Size = new System.Drawing.Size(118, 25);
            this.MskExpireDate.TabIndex = 14;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label11.Location = new System.Drawing.Point(11, 210);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(88, 19);
            this.label11.TabIndex = 319;
            this.label11.Text = "Start Date";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label8.Location = new System.Drawing.Point(275, 210);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 19);
            this.label8.TabIndex = 318;
            this.label8.Text = "Expire Date";
            // 
            // BtnMemberType
            // 
            this.BtnMemberType.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnMemberType.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnMemberType.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnMemberType.Location = new System.Drawing.Point(515, 180);
            this.BtnMemberType.Name = "BtnMemberType";
            this.BtnMemberType.Size = new System.Drawing.Size(25, 25);
            this.BtnMemberType.TabIndex = 313;
            this.BtnMemberType.TabStop = false;
            this.BtnMemberType.UseVisualStyleBackColor = false;
            this.BtnMemberType.Click += new System.EventHandler(this.BtnMemberType_Click);
            // 
            // TxtMemberType
            // 
            this.TxtMemberType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMemberType.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtMemberType.Location = new System.Drawing.Point(118, 180);
            this.TxtMemberType.MaxLength = 200;
            this.TxtMemberType.Name = "TxtMemberType";
            this.TxtMemberType.Size = new System.Drawing.Size(397, 25);
            this.TxtMemberType.TabIndex = 12;
            this.TxtMemberType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtMemberType_KeyDown);
            this.TxtMemberType.Leave += new System.EventHandler(this.TxtMemberType_Leave);
            this.TxtMemberType.Validating += new System.ComponentModel.CancelEventHandler(this.TxtMemberType_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 183);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 19);
            this.label6.TabIndex = 311;
            this.label6.Text = "Type";
            // 
            // BtnLedger
            // 
            this.BtnLedger.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnLedger.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnLedger.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnLedger.Location = new System.Drawing.Point(515, 127);
            this.BtnLedger.Name = "BtnLedger";
            this.BtnLedger.Size = new System.Drawing.Size(25, 25);
            this.BtnLedger.TabIndex = 310;
            this.BtnLedger.TabStop = false;
            this.BtnLedger.UseVisualStyleBackColor = false;
            this.BtnLedger.Click += new System.EventHandler(this.BtnLedger_Click);
            // 
            // BtnDescription
            // 
            this.BtnDescription.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnDescription.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnDescription.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDescription.Location = new System.Drawing.Point(515, 74);
            this.BtnDescription.Name = "BtnDescription";
            this.BtnDescription.Size = new System.Drawing.Size(25, 25);
            this.BtnDescription.TabIndex = 309;
            this.BtnDescription.TabStop = false;
            this.BtnDescription.UseVisualStyleBackColor = false;
            this.BtnDescription.Click += new System.EventHandler(this.BtnDescription_Click);
            // 
            // TxtEmail
            // 
            this.TxtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtEmail.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtEmail.Location = new System.Drawing.Point(118, 154);
            this.TxtEmail.MaxLength = 200;
            this.TxtEmail.Name = "TxtEmail";
            this.TxtEmail.Size = new System.Drawing.Size(397, 25);
            this.TxtEmail.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 19);
            this.label5.TabIndex = 17;
            this.label5.Text = "Email";
            // 
            // TxtLedger
            // 
            this.TxtLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtLedger.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtLedger.Location = new System.Drawing.Point(118, 127);
            this.TxtLedger.MaxLength = 200;
            this.TxtLedger.Name = "TxtLedger";
            this.TxtLedger.Size = new System.Drawing.Size(397, 25);
            this.TxtLedger.TabIndex = 10;
            this.TxtLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtLedger_KeyDown);
            this.TxtLedger.Leave += new System.EventHandler(this.TxtLedger_Leave);
            this.TxtLedger.Validating += new System.ComponentModel.CancelEventHandler(this.TxtLedger_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 19);
            this.label4.TabIndex = 15;
            this.label4.Text = "Ledger";
            // 
            // TxtPhoneNo
            // 
            this.TxtPhoneNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPhoneNo.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtPhoneNo.Location = new System.Drawing.Point(377, 48);
            this.TxtPhoneNo.MaxLength = 50;
            this.TxtPhoneNo.Name = "TxtPhoneNo";
            this.TxtPhoneNo.Size = new System.Drawing.Size(138, 25);
            this.TxtPhoneNo.TabIndex = 6;
            this.TxtPhoneNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPhoneNo_KeyDown);
            this.TxtPhoneNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPhoneNo_KeyPress);
            this.TxtPhoneNo.Validating += new System.ComponentModel.CancelEventHandler(this.TxtPhoneNo_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(293, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 19);
            this.label3.TabIndex = 13;
            this.label3.Text = "Phone No";
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.BtnExit.Location = new System.Drawing.Point(462, 5);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(77, 33);
            this.BtnExit.TabIndex = 4;
            this.BtnExit.Text = "E&XIT";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(155, 5);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(100, 33);
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
            this.BtnEdit.Location = new System.Drawing.Point(80, 5);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(73, 33);
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
            this.BtnNew.Location = new System.Drawing.Point(4, 5);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(75, 33);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Tag = "NewButtonCheck";
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // FrmMemberShip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 275);
            this.Controls.Add(this.StorePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmMemberShip";
            this.ShowIcon = false;
            this.Tag = "MemberShip Setup";
            this.Text = "Member Ship Setup";
            this.Load += new System.EventHandler(this.FrmMemberShip_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmMemberShip_KeyPress);
            this.StorePanel.ResumeLayout(false);
            this.StorePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private ClsSeparator clsSeparator2;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private System.Windows.Forms.CheckBox ChkActive;
        private ClsSeparator clsSeparator1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BtnLedger;
        private System.Windows.Forms.Button BtnDescription;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button BtnMemberType;
        public System.Windows.Forms.Label label11;
        public System.Windows.Forms.Label label8;
		private DevExpress.XtraEditors.SimpleButton BtnExit;
		private DevExpress.XtraEditors.SimpleButton BtnDelete;
		private DevExpress.XtraEditors.SimpleButton BtnEdit;
		private DevExpress.XtraEditors.SimpleButton BtnNew;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton BtnView;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox CmbPriceTag;
        private MrTextBox TxtDescription;
        private MrPanel StorePanel;
        private MrTextBox TxtPhoneNo;
        private MrTextBox TxtLedger;
        private MrTextBox TxtEmail;
        private MrTextBox TxtMemberType;
        public MrMaskedTextBox MskStartDate;
        private MrMaskedTextBox MskExpireDate;
        private MrTextBox TxtMemberId;
        private MrTextBox TxtShortName;
        private System.Windows.Forms.Button button1;
    }
}