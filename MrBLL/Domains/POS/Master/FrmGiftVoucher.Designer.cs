
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.POS.Master
{
    partial class FrmGiftVoucher
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
            this.StorePanel = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.CmbGiftVoucherType = new MrDAL.Control.ControlsEx.Control.MrComboBox();
            this.TxtIIssueAmount = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnGiftVoucher = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtDescription = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnView = new DevExpress.XtraEditors.SimpleButton();
            this.TxtGiftVoucherNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.MskExpireDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.ChkActive = new System.Windows.Forms.CheckBox();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.button1 = new System.Windows.Forms.Button();
            this.StorePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.button1);
            this.StorePanel.Controls.Add(this.label3);
            this.StorePanel.Controls.Add(this.CmbGiftVoucherType);
            this.StorePanel.Controls.Add(this.TxtIIssueAmount);
            this.StorePanel.Controls.Add(this.label2);
            this.StorePanel.Controls.Add(this.BtnGiftVoucher);
            this.StorePanel.Controls.Add(this.label1);
            this.StorePanel.Controls.Add(this.TxtDescription);
            this.StorePanel.Controls.Add(this.BtnView);
            this.StorePanel.Controls.Add(this.TxtGiftVoucherNo);
            this.StorePanel.Controls.Add(this.label7);
            this.StorePanel.Controls.Add(this.MskExpireDate);
            this.StorePanel.Controls.Add(this.label8);
            this.StorePanel.Controls.Add(this.clsSeparator2);
            this.StorePanel.Controls.Add(this.BtnSave);
            this.StorePanel.Controls.Add(this.BtnCancel);
            this.StorePanel.Controls.Add(this.ChkActive);
            this.StorePanel.Controls.Add(this.clsSeparator1);
            this.StorePanel.Controls.Add(this.BtnExit);
            this.StorePanel.Controls.Add(this.BtnDelete);
            this.StorePanel.Controls.Add(this.BtnEdit);
            this.StorePanel.Controls.Add(this.BtnNew);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(541, 178);
            this.StorePanel.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(271, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 19);
            this.label3.TabIndex = 328;
            this.label3.Text = "Cliam";
            // 
            // CmbGiftVoucherType
            // 
            this.CmbGiftVoucherType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbGiftVoucherType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbGiftVoucherType.FormattingEnabled = true;
            this.CmbGiftVoucherType.Location = new System.Drawing.Point(330, 107);
            this.CmbGiftVoucherType.Name = "CmbGiftVoucherType";
            this.CmbGiftVoucherType.Size = new System.Drawing.Size(205, 27);
            this.CmbGiftVoucherType.TabIndex = 9;
            // 
            // TxtIIssueAmount
            // 
            this.TxtIIssueAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtIIssueAmount.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtIIssueAmount.Location = new System.Drawing.Point(124, 107);
            this.TxtIIssueAmount.MaxLength = 50;
            this.TxtIIssueAmount.Name = "TxtIIssueAmount";
            this.TxtIIssueAmount.Size = new System.Drawing.Size(142, 25);
            this.TxtIIssueAmount.TabIndex = 8;
            this.TxtIIssueAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtIIssueAmount.TextBoxType = MrDAL.Control.ControlsEx.Control.TextBoxType.Decimal;
            this.TxtIIssueAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtIIssueAmount_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 19);
            this.label2.TabIndex = 326;
            this.label2.Text = "Issue Amount";
            // 
            // BtnGiftVoucher
            // 
            this.BtnGiftVoucher.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnGiftVoucher.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnGiftVoucher.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnGiftVoucher.Location = new System.Drawing.Point(268, 46);
            this.BtnGiftVoucher.Name = "BtnGiftVoucher";
            this.BtnGiftVoucher.Size = new System.Drawing.Size(30, 28);
            this.BtnGiftVoucher.TabIndex = 324;
            this.BtnGiftVoucher.TabStop = false;
            this.BtnGiftVoucher.UseVisualStyleBackColor = false;
            this.BtnGiftVoucher.Click += new System.EventHandler(this.BtnGiftVoucher_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 19);
            this.label1.TabIndex = 323;
            this.label1.Text = "Description";
            // 
            // TxtDescription
            // 
            this.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDescription.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtDescription.Location = new System.Drawing.Point(124, 78);
            this.TxtDescription.MaxLength = 255;
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(414, 25);
            this.TxtDescription.TabIndex = 7;
            this.TxtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDescription_KeyDown);
            this.TxtDescription.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDescription_Validating);
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
            // TxtGiftVoucherNo
            // 
            this.TxtGiftVoucherNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtGiftVoucherNo.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtGiftVoucherNo.Location = new System.Drawing.Point(124, 48);
            this.TxtGiftVoucherNo.MaxLength = 50;
            this.TxtGiftVoucherNo.Name = "TxtGiftVoucherNo";
            this.TxtGiftVoucherNo.Size = new System.Drawing.Size(142, 25);
            this.TxtGiftVoucherNo.TabIndex = 5;
            this.TxtGiftVoucherNo.TextBoxType = MrDAL.Control.ControlsEx.Control.TextBoxType.Numeric;
            this.TxtGiftVoucherNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtGiftVoucherNo_KeyDown);
            this.TxtGiftVoucherNo.Validating += new System.ComponentModel.CancelEventHandler(this.TxtGiftVoucherNo_Validating);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 19);
            this.label7.TabIndex = 321;
            this.label7.Text = "Card No:";
            // 
            // MskExpireDate
            // 
            this.MskExpireDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskExpireDate.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.MskExpireDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskExpireDate.Location = new System.Drawing.Point(418, 47);
            this.MskExpireDate.Mask = "00/00/0000";
            this.MskExpireDate.Name = "MskExpireDate";
            this.MskExpireDate.Size = new System.Drawing.Size(118, 25);
            this.MskExpireDate.TabIndex = 6;
            this.MskExpireDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MskExpireDate_KeyDown);
            this.MskExpireDate.Validating += new System.ComponentModel.CancelEventHandler(this.MskExpireDate_Validating);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.label8.Location = new System.Drawing.Point(313, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 19);
            this.label8.TabIndex = 318;
            this.label8.Text = "Expire Date";
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(7, 137);
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
            this.BtnSave.Location = new System.Drawing.Point(341, 140);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(89, 33);
            this.BtnSave.TabIndex = 10;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.BtnCancel.Location = new System.Drawing.Point(431, 140);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(104, 33);
            this.BtnCancel.TabIndex = 11;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // ChkActive
            // 
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Location = new System.Drawing.Point(6, 144);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkActive.Size = new System.Drawing.Size(109, 24);
            this.ChkActive.TabIndex = 11;
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
            this.BtnDelete.Location = new System.Drawing.Point(156, 5);
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
            this.BtnEdit.Location = new System.Drawing.Point(83, 5);
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
            this.BtnNew.Location = new System.Drawing.Point(8, 5);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(75, 33);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Tag = "NewButtonCheck";
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // button1
            // 
            this.button1.Image = global::MrBLL.Properties.Resources.ReportRefresh;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(249, 141);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 32);
            this.button1.TabIndex = 8;
            this.button1.Text = "&SYNC";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.BtnSync_Click);
            // 
            // FrmGiftVoucher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(541, 178);
            this.Controls.Add(this.StorePanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmGiftVoucher";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "Gift Voucher Generate";
            this.Text = "GIFT VOUCHER";
            this.Load += new System.EventHandler(this.FrmGiftVoucher_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmGiftVoucher_KeyPress);
            this.StorePanel.ResumeLayout(false);
            this.StorePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton BtnView;
        private MrTextBox TxtGiftVoucherNo;
        private System.Windows.Forms.Label label7;
        private MrMaskedTextBox MskExpireDate;
        public System.Windows.Forms.Label label8;
        private ClsSeparator clsSeparator2;
        private MrPanel StorePanel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private System.Windows.Forms.CheckBox ChkActive;
        private ClsSeparator clsSeparator1;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private MrTextBox TxtDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnGiftVoucher;
        private MrTextBox TxtIIssueAmount;
        private System.Windows.Forms.Label label2;
        private MrComboBox CmbGiftVoucherType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
    }
}