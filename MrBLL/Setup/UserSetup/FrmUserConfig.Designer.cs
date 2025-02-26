using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Setup.UserSetup
{
    partial class FrmUserConfig
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
            this.roundPanel1 = new MrDAL.Control.ControlsEx.Control.RoundPanel();
            this.ChkQtyChange = new System.Windows.Forms.CheckBox();
            this.ChkChangeRate = new System.Windows.Forms.CheckBox();
            this.ChkAuditLog = new System.Windows.Forms.CheckBox();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.ChkPDCDashBoard = new System.Windows.Forms.CheckBox();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.TxtUser = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnUser = new System.Windows.Forms.Button();
            this.TxtLedger = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.ChkDelete = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ChkModify = new System.Windows.Forms.CheckBox();
            this.BtnLedger = new System.Windows.Forms.Button();
            this.ChkAuthorized = new System.Windows.Forms.CheckBox();
            this.MskPostStartDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.lbl_PostingStartDate = new System.Windows.Forms.Label();
            this.MskPostEndDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.TxtValidDays = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_ValidDays = new System.Windows.Forms.Label();
            this.ChkPost = new System.Windows.Forms.CheckBox();
            this.MskModifyStartDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.MskModifyEndDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.CmbCategory = new System.Windows.Forms.ComboBox();
            this.lbl_ModifyEndDate = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.roundPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // roundPanel1
            // 
            this.roundPanel1.BackColor = System.Drawing.Color.Transparent;
            this.roundPanel1.Controls.Add(this.label2);
            this.roundPanel1.Controls.Add(this.ChkQtyChange);
            this.roundPanel1.Controls.Add(this.ChkChangeRate);
            this.roundPanel1.Controls.Add(this.ChkAuditLog);
            this.roundPanel1.Controls.Add(this.clsSeparator1);
            this.roundPanel1.Controls.Add(this.ChkPDCDashBoard);
            this.roundPanel1.Controls.Add(this.BtnCancel);
            this.roundPanel1.Controls.Add(this.clsSeparator2);
            this.roundPanel1.Controls.Add(this.BtnSave);
            this.roundPanel1.Controls.Add(this.TxtUser);
            this.roundPanel1.Controls.Add(this.label3);
            this.roundPanel1.Controls.Add(this.BtnUser);
            this.roundPanel1.Controls.Add(this.TxtLedger);
            this.roundPanel1.Controls.Add(this.ChkDelete);
            this.roundPanel1.Controls.Add(this.label1);
            this.roundPanel1.Controls.Add(this.ChkModify);
            this.roundPanel1.Controls.Add(this.BtnLedger);
            this.roundPanel1.Controls.Add(this.ChkAuthorized);
            this.roundPanel1.Controls.Add(this.MskPostStartDate);
            this.roundPanel1.Controls.Add(this.lbl_PostingStartDate);
            this.roundPanel1.Controls.Add(this.MskPostEndDate);
            this.roundPanel1.Controls.Add(this.TxtValidDays);
            this.roundPanel1.Controls.Add(this.lbl_ValidDays);
            this.roundPanel1.Controls.Add(this.ChkPost);
            this.roundPanel1.Controls.Add(this.MskModifyStartDate);
            this.roundPanel1.Controls.Add(this.MskModifyEndDate);
            this.roundPanel1.Controls.Add(this.CmbCategory);
            this.roundPanel1.Controls.Add(this.lbl_ModifyEndDate);
            this.roundPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roundPanel1.Location = new System.Drawing.Point(0, 0);
            this.roundPanel1.Name = "roundPanel1";
            this.roundPanel1.Radious = 25;
            this.roundPanel1.Size = new System.Drawing.Size(472, 326);
            this.roundPanel1.TabIndex = 0;
            this.roundPanel1.TabStop = false;
            this.roundPanel1.Text = "USER CONFIGRATION";
            this.roundPanel1.TitleBackColor = System.Drawing.Color.DarkSlateBlue;
            this.roundPanel1.TitleFont = new System.Drawing.Font("Bookman Old Style", 12F);
            this.roundPanel1.TitleForeColor = System.Drawing.Color.White;
            this.roundPanel1.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
            // 
            // ChkQtyChange
            // 
            this.ChkQtyChange.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkQtyChange.Location = new System.Drawing.Point(4, 258);
            this.ChkQtyChange.Name = "ChkQtyChange";
            this.ChkQtyChange.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkQtyChange.Size = new System.Drawing.Size(138, 24);
            this.ChkQtyChange.TabIndex = 317;
            this.ChkQtyChange.Text = "Is QtyChange";
            this.ChkQtyChange.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.ChkQtyChange.UseVisualStyleBackColor = true;
            // 
            // ChkChangeRate
            // 
            this.ChkChangeRate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkChangeRate.Location = new System.Drawing.Point(4, 234);
            this.ChkChangeRate.Name = "ChkChangeRate";
            this.ChkChangeRate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkChangeRate.Size = new System.Drawing.Size(138, 24);
            this.ChkChangeRate.TabIndex = 9;
            this.ChkChangeRate.Text = "Rate Change";
            this.ChkChangeRate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.ChkChangeRate.UseVisualStyleBackColor = true;
            // 
            // ChkAuditLog
            // 
            this.ChkAuditLog.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAuditLog.Location = new System.Drawing.Point(248, 233);
            this.ChkAuditLog.Name = "ChkAuditLog";
            this.ChkAuditLog.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkAuditLog.Size = new System.Drawing.Size(112, 24);
            this.ChkAuditLog.TabIndex = 13;
            this.ChkAuditLog.Text = "IsAuditLog";
            this.ChkAuditLog.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.ChkAuditLog.UseVisualStyleBackColor = true;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(6, 282);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(458, 2);
            this.clsSeparator1.TabIndex = 316;
            this.clsSeparator1.TabStop = false;
            // 
            // ChkPDCDashBoard
            // 
            this.ChkPDCDashBoard.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkPDCDashBoard.Location = new System.Drawing.Point(248, 211);
            this.ChkPDCDashBoard.Name = "ChkPDCDashBoard";
            this.ChkPDCDashBoard.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkPDCDashBoard.Size = new System.Drawing.Size(112, 24);
            this.ChkPDCDashBoard.TabIndex = 12;
            this.ChkPDCDashBoard.Text = "IsPDC";
            this.ChkPDCDashBoard.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.ChkPDCDashBoard.UseVisualStyleBackColor = true;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(256, 287);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(105, 36);
            this.BtnCancel.TabIndex = 16;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(7, 202);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(458, 2);
            this.clsSeparator2.TabIndex = 315;
            this.clsSeparator2.TabStop = false;
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(164, 287);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(91, 36);
            this.BtnSave.TabIndex = 15;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // TxtUser
            // 
            this.TxtUser.BackColor = System.Drawing.Color.White;
            this.TxtUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtUser.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUser.Location = new System.Drawing.Point(112, 29);
            this.TxtUser.Name = "TxtUser";
            this.TxtUser.ReadOnly = true;
            this.TxtUser.Size = new System.Drawing.Size(319, 26);
            this.TxtUser.TabIndex = 0;
            this.TxtUser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtUser_KeyDown);
            this.TxtUser.Leave += new System.EventHandler(this.TxtUser_Leave);
            this.TxtUser.Validating += new System.ComponentModel.CancelEventHandler(this.TxtUser_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 20);
            this.label3.TabIndex = 313;
            this.label3.Text = "User Info";
            // 
            // BtnUser
            // 
            this.BtnUser.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnUser.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnUser.Image = global::MrBLL.Properties.Resources.User;
            this.BtnUser.Location = new System.Drawing.Point(434, 30);
            this.BtnUser.Name = "BtnUser";
            this.BtnUser.Size = new System.Drawing.Size(27, 24);
            this.BtnUser.TabIndex = 314;
            this.BtnUser.TabStop = false;
            this.BtnUser.UseVisualStyleBackColor = false;
            this.BtnUser.Click += new System.EventHandler(this.BtnUser_Click);
            // 
            // TxtLedger
            // 
            this.TxtLedger.BackColor = System.Drawing.Color.White;
            this.TxtLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtLedger.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtLedger.Location = new System.Drawing.Point(112, 57);
            this.TxtLedger.Name = "TxtLedger";
            this.TxtLedger.ReadOnly = true;
            this.TxtLedger.Size = new System.Drawing.Size(319, 26);
            this.TxtLedger.TabIndex = 1;
            this.TxtLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtLedger_KeyDown);
            this.TxtLedger.Leave += new System.EventHandler(this.TxtLedger_Leave);
            this.TxtLedger.Validating += new System.ComponentModel.CancelEventHandler(this.TxtLedger_Validating);
            // 
            // ChkDelete
            // 
            this.ChkDelete.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDelete.Location = new System.Drawing.Point(148, 235);
            this.ChkDelete.Name = "ChkDelete";
            this.ChkDelete.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDelete.Size = new System.Drawing.Size(94, 24);
            this.ChkDelete.TabIndex = 11;
            this.ChkDelete.Text = "IsDelete";
            this.ChkDelete.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.ChkDelete.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.TabIndex = 57;
            this.label1.Text = "Ledger";
            // 
            // ChkModify
            // 
            this.ChkModify.AutoSize = true;
            this.ChkModify.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkModify.Location = new System.Drawing.Point(148, 210);
            this.ChkModify.Name = "ChkModify";
            this.ChkModify.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkModify.Size = new System.Drawing.Size(94, 24);
            this.ChkModify.TabIndex = 10;
            this.ChkModify.Text = "IsModify";
            this.ChkModify.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.ChkModify.UseVisualStyleBackColor = true;
            // 
            // BtnLedger
            // 
            this.BtnLedger.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnLedger.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnLedger.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnLedger.Location = new System.Drawing.Point(434, 58);
            this.BtnLedger.Name = "BtnLedger";
            this.BtnLedger.Size = new System.Drawing.Size(27, 24);
            this.BtnLedger.TabIndex = 309;
            this.BtnLedger.TabStop = false;
            this.BtnLedger.UseVisualStyleBackColor = false;
            this.BtnLedger.Click += new System.EventHandler(this.BtnLedger_Click);
            // 
            // ChkAuthorized
            // 
            this.ChkAuthorized.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAuthorized.Location = new System.Drawing.Point(4, 210);
            this.ChkAuthorized.Name = "ChkAuthorized";
            this.ChkAuthorized.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkAuthorized.Size = new System.Drawing.Size(138, 24);
            this.ChkAuthorized.TabIndex = 8;
            this.ChkAuthorized.Text = "IsAuthorized";
            this.ChkAuthorized.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.ChkAuthorized.UseVisualStyleBackColor = true;
            // 
            // MskPostStartDate
            // 
            this.MskPostStartDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskPostStartDate.Enabled = false;
            this.MskPostStartDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskPostStartDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskPostStartDate.Location = new System.Drawing.Point(112, 85);
            this.MskPostStartDate.Mask = "00/00/0000";
            this.MskPostStartDate.Name = "MskPostStartDate";
            this.MskPostStartDate.Size = new System.Drawing.Size(146, 26);
            this.MskPostStartDate.TabIndex = 2;
            // 
            // lbl_PostingStartDate
            // 
            this.lbl_PostingStartDate.AutoSize = true;
            this.lbl_PostingStartDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_PostingStartDate.Location = new System.Drawing.Point(10, 89);
            this.lbl_PostingStartDate.Name = "lbl_PostingStartDate";
            this.lbl_PostingStartDate.Size = new System.Drawing.Size(84, 20);
            this.lbl_PostingStartDate.TabIndex = 44;
            this.lbl_PostingStartDate.Text = "Post Date";
            // 
            // MskPostEndDate
            // 
            this.MskPostEndDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskPostEndDate.Enabled = false;
            this.MskPostEndDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskPostEndDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskPostEndDate.Location = new System.Drawing.Point(285, 85);
            this.MskPostEndDate.Mask = "00/00/0000";
            this.MskPostEndDate.Name = "MskPostEndDate";
            this.MskPostEndDate.Size = new System.Drawing.Size(146, 26);
            this.MskPostEndDate.TabIndex = 3;
            // 
            // TxtValidDays
            // 
            this.TxtValidDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtValidDays.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtValidDays.Location = new System.Drawing.Point(112, 141);
            this.TxtValidDays.Margin = new System.Windows.Forms.Padding(2);
            this.TxtValidDays.MaxLength = 3;
            this.TxtValidDays.Name = "TxtValidDays";
            this.TxtValidDays.Size = new System.Drawing.Size(146, 26);
            this.TxtValidDays.TabIndex = 6;
            this.TxtValidDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtValidDays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtValidDays_KeyPress);
            // 
            // lbl_ValidDays
            // 
            this.lbl_ValidDays.AutoSize = true;
            this.lbl_ValidDays.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ValidDays.Location = new System.Drawing.Point(10, 144);
            this.lbl_ValidDays.Name = "lbl_ValidDays";
            this.lbl_ValidDays.Size = new System.Drawing.Size(93, 20);
            this.lbl_ValidDays.TabIndex = 54;
            this.lbl_ValidDays.Text = "Valid Days";
            // 
            // ChkPost
            // 
            this.ChkPost.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkPost.Location = new System.Drawing.Point(366, 211);
            this.ChkPost.Name = "ChkPost";
            this.ChkPost.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkPost.Size = new System.Drawing.Size(100, 24);
            this.ChkPost.TabIndex = 14;
            this.ChkPost.Text = "IsPosted";
            this.ChkPost.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.ChkPost.UseVisualStyleBackColor = true;
            // 
            // MskModifyStartDate
            // 
            this.MskModifyStartDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskModifyStartDate.Enabled = false;
            this.MskModifyStartDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskModifyStartDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskModifyStartDate.Location = new System.Drawing.Point(112, 113);
            this.MskModifyStartDate.Mask = "00/00/0000";
            this.MskModifyStartDate.Name = "MskModifyStartDate";
            this.MskModifyStartDate.Size = new System.Drawing.Size(146, 26);
            this.MskModifyStartDate.TabIndex = 4;
            // 
            // MskModifyEndDate
            // 
            this.MskModifyEndDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskModifyEndDate.Enabled = false;
            this.MskModifyEndDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskModifyEndDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskModifyEndDate.Location = new System.Drawing.Point(285, 113);
            this.MskModifyEndDate.Mask = "00/00/0000";
            this.MskModifyEndDate.Name = "MskModifyEndDate";
            this.MskModifyEndDate.Size = new System.Drawing.Size(146, 26);
            this.MskModifyEndDate.TabIndex = 5;
            // 
            // CmbCategory
            // 
            this.CmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbCategory.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbCategory.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbCategory.FormattingEnabled = true;
            this.CmbCategory.Items.AddRange(new object[] {
            "NORMAL",
            "TERMINAL",
            "ORDER",
            "REPORT",
            "ACCOUNT",
            "SALES",
            "SUPPORT",
            "ADMINISTRATOR"});
            this.CmbCategory.Location = new System.Drawing.Point(112, 170);
            this.CmbCategory.Name = "CmbCategory";
            this.CmbCategory.Size = new System.Drawing.Size(319, 28);
            this.CmbCategory.TabIndex = 7;
            // 
            // lbl_ModifyEndDate
            // 
            this.lbl_ModifyEndDate.AutoSize = true;
            this.lbl_ModifyEndDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ModifyEndDate.Location = new System.Drawing.Point(10, 116);
            this.lbl_ModifyEndDate.Name = "lbl_ModifyEndDate";
            this.lbl_ModifyEndDate.Size = new System.Drawing.Size(104, 20);
            this.lbl_ModifyEndDate.TabIndex = 47;
            this.lbl_ModifyEndDate.Text = "Modify Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 318;
            this.label2.Text = "Category";
            // 
            // FrmUserConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(472, 326);
            this.Controls.Add(this.roundPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmUserConfig";
            this.ShowIcon = false;
            this.Text = "USER CONFIGRATION";
            this.Load += new System.EventHandler(this.FrmUserConfig_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmUserConfig_KeyPress);
            this.roundPanel1.ResumeLayout(false);
            this.roundPanel1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private RoundPanel roundPanel1;
		private System.Windows.Forms.CheckBox ChkDelete;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox ChkModify;
		private System.Windows.Forms.Button BtnLedger;
		private System.Windows.Forms.CheckBox ChkAuthorized;
		private System.Windows.Forms.Label lbl_PostingStartDate;
		private System.Windows.Forms.Label lbl_ValidDays;
		private System.Windows.Forms.CheckBox ChkPost;
		private System.Windows.Forms.Label lbl_ModifyEndDate;
		private System.Windows.Forms.ComboBox CmbCategory;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button BtnUser;
		private ClsSeparator clsSeparator2;
		private DevExpress.XtraEditors.SimpleButton BtnCancel;
		private DevExpress.XtraEditors.SimpleButton BtnSave;
		private System.Windows.Forms.CheckBox ChkPDCDashBoard;
		private ClsSeparator clsSeparator1;
		private System.Windows.Forms.CheckBox ChkAuditLog;
		private System.Windows.Forms.CheckBox ChkChangeRate;
        private System.Windows.Forms.CheckBox ChkQtyChange;
        private MrTextBox TxtLedger;
        private MrMaskedTextBox MskPostStartDate;
        private MrMaskedTextBox MskPostEndDate;
        private MrTextBox TxtValidDays;
        private MrMaskedTextBox MskModifyStartDate;
        private MrMaskedTextBox MskModifyEndDate;
        private MrTextBox TxtUser;
        private System.Windows.Forms.Label label2;
    }
}