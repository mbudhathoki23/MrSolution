using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Setup.UserSetup
{
    partial class FrmUserRoleSetup
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
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.BtnDescription = new System.Windows.Forms.Button();
            this.ChkActive = new System.Windows.Forms.CheckBox();
            this.btn_Clear = new DevExpress.XtraEditors.SimpleButton();
            this.TxtDescription = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtShortName = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_CmpName = new System.Windows.Forms.Label();
            this.btn_Save = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_RoleCode = new System.Windows.Forms.Label();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.roundPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // roundPanel1
            // 
            this.roundPanel1.BackColor = System.Drawing.Color.Transparent;
            this.roundPanel1.Controls.Add(this.clsSeparator2);
            this.roundPanel1.Controls.Add(this.clsSeparator1);
            this.roundPanel1.Controls.Add(this.BtnDescription);
            this.roundPanel1.Controls.Add(this.ChkActive);
            this.roundPanel1.Controls.Add(this.btn_Clear);
            this.roundPanel1.Controls.Add(this.TxtDescription);
            this.roundPanel1.Controls.Add(this.TxtShortName);
            this.roundPanel1.Controls.Add(this.BtnExit);
            this.roundPanel1.Controls.Add(this.lbl_CmpName);
            this.roundPanel1.Controls.Add(this.btn_Save);
            this.roundPanel1.Controls.Add(this.lbl_RoleCode);
            this.roundPanel1.Controls.Add(this.BtnDelete);
            this.roundPanel1.Controls.Add(this.BtnEdit);
            this.roundPanel1.Controls.Add(this.BtnNew);
            this.roundPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roundPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.roundPanel1.Location = new System.Drawing.Point(0, 0);
            this.roundPanel1.Name = "roundPanel1";
            this.roundPanel1.Radious = 25;
            this.roundPanel1.Size = new System.Drawing.Size(459, 168);
            this.roundPanel1.TabIndex = 0;
            this.roundPanel1.TabStop = false;
            this.roundPanel1.Text = "USER ROLES SETUP";
            this.roundPanel1.TitleBackColor = System.Drawing.Color.DarkSlateBlue;
            this.roundPanel1.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.roundPanel1.TitleForeColor = System.Drawing.Color.White;
            this.roundPanel1.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(3, 126);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(449, 2);
            this.clsSeparator2.TabIndex = 21;
            this.clsSeparator2.TabStop = false;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(2, 64);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(449, 2);
            this.clsSeparator1.TabIndex = 20;
            this.clsSeparator1.TabStop = false;
            // 
            // BtnDescription
            // 
            this.BtnDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.BtnDescription.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnDescription.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDescription.Location = new System.Drawing.Point(423, 70);
            this.BtnDescription.Name = "BtnDescription";
            this.BtnDescription.Size = new System.Drawing.Size(28, 25);
            this.BtnDescription.TabIndex = 19;
            this.BtnDescription.TabStop = false;
            this.BtnDescription.UseVisualStyleBackColor = false;
            this.BtnDescription.Click += new System.EventHandler(this.BtnDescription_Click);
            // 
            // ChkActive
            // 
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkActive.Location = new System.Drawing.Point(11, 135);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkActive.Size = new System.Drawing.Size(106, 22);
            this.ChkActive.TabIndex = 8;
            this.ChkActive.Text = "Status";
            this.ChkActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkActive.UseVisualStyleBackColor = true;
            this.ChkActive.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cb_Status_KeyDown);
            // 
            // btn_Clear
            // 
            this.btn_Clear.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.btn_Clear.Appearance.Options.UseFont = true;
            this.btn_Clear.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.btn_Clear.Location = new System.Drawing.Point(350, 130);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(101, 34);
            this.btn_Clear.TabIndex = 5;
            this.btn_Clear.Text = "&CANCEL";
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // TxtDescription
            // 
            this.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDescription.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtDescription.Location = new System.Drawing.Point(98, 70);
            this.TxtDescription.MaxLength = 100;
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.ReadOnly = true;
            this.TxtDescription.Size = new System.Drawing.Size(322, 25);
            this.TxtDescription.TabIndex = 3;
            this.TxtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_RoleName_KeyDown);
            this.TxtDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyEvents_KeyPress);
            this.TxtDescription.Leave += new System.EventHandler(this.txt_UnitName_Leave);
            this.TxtDescription.Validated += new System.EventHandler(this.txt_RoleName_Validated);
            // 
            // TxtShortName
            // 
            this.TxtShortName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtShortName.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtShortName.Location = new System.Drawing.Point(98, 98);
            this.TxtShortName.MaxLength = 10;
            this.TxtShortName.Name = "TxtShortName";
            this.TxtShortName.Size = new System.Drawing.Size(139, 25);
            this.TxtShortName.TabIndex = 4;
            this.TxtShortName.Visible = false;
            this.TxtShortName.Enter += new System.EventHandler(this.txt_RoleCode_Enter);
            this.TxtShortName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_RoleCode_KeyDown);
            this.TxtShortName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyEvents_KeyPress);
            this.TxtShortName.Leave += new System.EventHandler(this.txt_RoleCode_Leave);
            this.TxtShortName.Validated += new System.EventHandler(this.txt_RoleCode_Validated);
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.BtnExit.Location = new System.Drawing.Point(378, 26);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(77, 32);
            this.BtnExit.TabIndex = 6;
            this.BtnExit.Text = "E&XIT";
            this.BtnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lbl_CmpName
            // 
            this.lbl_CmpName.AutoSize = true;
            this.lbl_CmpName.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_CmpName.Location = new System.Drawing.Point(7, 73);
            this.lbl_CmpName.Name = "lbl_CmpName";
            this.lbl_CmpName.Size = new System.Drawing.Size(90, 19);
            this.lbl_CmpName.TabIndex = 2;
            this.lbl_CmpName.Text = "Role Name";
            // 
            // btn_Save
            // 
            this.btn_Save.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.btn_Save.Appearance.Options.UseFont = true;
            this.btn_Save.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.btn_Save.Location = new System.Drawing.Point(251, 130);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(96, 34);
            this.btn_Save.TabIndex = 4;
            this.btn_Save.Text = "&SAVE";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // lbl_RoleCode
            // 
            this.lbl_RoleCode.AutoSize = true;
            this.lbl_RoleCode.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_RoleCode.Location = new System.Drawing.Point(7, 101);
            this.lbl_RoleCode.Name = "lbl_RoleCode";
            this.lbl_RoleCode.Size = new System.Drawing.Size(93, 19);
            this.lbl_RoleCode.TabIndex = 1;
            this.lbl_RoleCode.Text = "ShortName";
            this.lbl_RoleCode.Visible = false;
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(162, 26);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(105, 32);
            this.BtnDelete.TabIndex = 2;
            this.BtnDelete.Tag = "DeleteButtonCheck";
            this.BtnDelete.Text = "&DELETE";
            this.BtnDelete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnEdit.Appearance.Options.UseFont = true;
            this.BtnEdit.ImageOptions.Image = global::MrBLL.Properties.Resources.Edit;
            this.BtnEdit.Location = new System.Drawing.Point(82, 26);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(79, 32);
            this.BtnEdit.TabIndex = 1;
            this.BtnEdit.Tag = "EditButtonCheck";
            this.BtnEdit.Text = "&EDIT";
            this.BtnEdit.Click += new System.EventHandler(this.btn_Edit_Click);
            // 
            // BtnNew
            // 
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnNew.Location = new System.Drawing.Point(2, 26);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(79, 32);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Tag = "NewButtonCheck";
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.btn_New_Click);
            // 
            // FrmUserRoleSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(459, 168);
            this.Controls.Add(this.roundPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "FrmUserRoleSetup";
            this.ShowIcon = false;
            this.Tag = "Role Master";
            this.Text = "User Role Setup";
            this.Load += new System.EventHandler(this.FrmUserRoleSetup_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmUserRoleSetup_KeyPress);
            this.roundPanel1.ResumeLayout(false);
            this.roundPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl_CmpName;
        private System.Windows.Forms.Label lbl_RoleCode;
        private System.Windows.Forms.CheckBox ChkActive;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton btn_Save;
        private DevExpress.XtraEditors.SimpleButton btn_Clear;
		private RoundPanel roundPanel1;
		private System.Windows.Forms.Button BtnDescription;
		private ClsSeparator clsSeparator1;
		private ClsSeparator clsSeparator2;
        private MrTextBox TxtDescription;
        private MrTextBox TxtShortName;
    }
}