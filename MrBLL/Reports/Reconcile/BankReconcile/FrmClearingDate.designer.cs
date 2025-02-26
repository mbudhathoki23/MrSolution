using System.ComponentModel;
using System.Windows.Forms;
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Reconcile.BankReconcile
{
    partial class FrmClearingDate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmClearingDate));
            this.txt_Station = new MrTextBox();
            this.lbl_Station = new System.Windows.Forms.Label();
            this.msk_ClearingDate = new MrMaskedTextBox();
            this.lbl_ClearingDate = new System.Windows.Forms.Label();
            this.txt_ChequeNo = new MrTextBox();
            this.lbl_ChequeNo = new System.Windows.Forms.Label();
            this.msk_ChequeDate = new MrMaskedTextBox();
            this.lbl_ChequeDate = new System.Windows.Forms.Label();
            this.txt_VoucherNo = new MrTextBox();
            this.lbl_VoucherNo = new System.Windows.Forms.Label();
            this.txt_BankName = new MrTextBox();
            this.lbl_BankName = new System.Windows.Forms.Label();
            this.msk_VoucherDate = new MrMaskedTextBox();
            this.lbl_VoucherDate = new System.Windows.Forms.Label();
            this.mrPanel1 = new MrPanel();
            this.clsSeparator1 = new ClsSeparator();
            this.btn_Show = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnLedger = new System.Windows.Forms.Button();
            this.mrPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_Station
            // 
            this.txt_Station.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Station.Location = new System.Drawing.Point(441, 86);
            this.txt_Station.MaxLength = 255;
            this.txt_Station.Name = "txt_Station";
            this.txt_Station.Size = new System.Drawing.Size(146, 25);
            this.txt_Station.TabIndex = 88;
            this.txt_Station.Visible = false;
            // 
            // lbl_Station
            // 
            this.lbl_Station.AutoSize = true;
            this.lbl_Station.Location = new System.Drawing.Point(272, 87);
            this.lbl_Station.Name = "lbl_Station";
            this.lbl_Station.Size = new System.Drawing.Size(63, 19);
            this.lbl_Station.TabIndex = 89;
            this.lbl_Station.Text = "Module";
            this.lbl_Station.Visible = false;
            // 
            // msk_ClearingDate
            // 
            this.msk_ClearingDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_ClearingDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.msk_ClearingDate.Location = new System.Drawing.Point(122, 84);
            this.msk_ClearingDate.Mask = "00/00/0000";
            this.msk_ClearingDate.Name = "msk_ClearingDate";
            this.msk_ClearingDate.Size = new System.Drawing.Size(146, 25);
            this.msk_ClearingDate.TabIndex = 85;
            // 
            // lbl_ClearingDate
            // 
            this.lbl_ClearingDate.AutoSize = true;
            this.lbl_ClearingDate.Location = new System.Drawing.Point(6, 84);
            this.lbl_ClearingDate.Name = "lbl_ClearingDate";
            this.lbl_ClearingDate.Size = new System.Drawing.Size(114, 19);
            this.lbl_ClearingDate.TabIndex = 86;
            this.lbl_ClearingDate.Text = "Clearing Date";
            // 
            // txt_ChequeNo
            // 
            this.txt_ChequeNo.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txt_ChequeNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ChequeNo.Enabled = false;
            this.txt_ChequeNo.Location = new System.Drawing.Point(122, 58);
            this.txt_ChequeNo.MaxLength = 255;
            this.txt_ChequeNo.Name = "txt_ChequeNo";
            this.txt_ChequeNo.Size = new System.Drawing.Size(146, 25);
            this.txt_ChequeNo.TabIndex = 83;
            // 
            // lbl_ChequeNo
            // 
            this.lbl_ChequeNo.AutoSize = true;
            this.lbl_ChequeNo.Location = new System.Drawing.Point(6, 58);
            this.lbl_ChequeNo.Name = "lbl_ChequeNo";
            this.lbl_ChequeNo.Size = new System.Drawing.Size(90, 19);
            this.lbl_ChequeNo.TabIndex = 84;
            this.lbl_ChequeNo.Text = "Cheque No";
            // 
            // msk_ChequeDate
            // 
            this.msk_ChequeDate.BackColor = System.Drawing.SystemColors.HighlightText;
            this.msk_ChequeDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_ChequeDate.Enabled = false;
            this.msk_ChequeDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.msk_ChequeDate.Location = new System.Drawing.Point(441, 59);
            this.msk_ChequeDate.Mask = "00/00/0000";
            this.msk_ChequeDate.Name = "msk_ChequeDate";
            this.msk_ChequeDate.Size = new System.Drawing.Size(146, 25);
            this.msk_ChequeDate.TabIndex = 81;
            // 
            // lbl_ChequeDate
            // 
            this.lbl_ChequeDate.AutoSize = true;
            this.lbl_ChequeDate.Location = new System.Drawing.Point(274, 61);
            this.lbl_ChequeDate.Name = "lbl_ChequeDate";
            this.lbl_ChequeDate.Size = new System.Drawing.Size(107, 19);
            this.lbl_ChequeDate.TabIndex = 82;
            this.lbl_ChequeDate.Text = "Cheque Date";
            // 
            // txt_VoucherNo
            // 
            this.txt_VoucherNo.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txt_VoucherNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_VoucherNo.Enabled = false;
            this.txt_VoucherNo.Location = new System.Drawing.Point(122, 32);
            this.txt_VoucherNo.MaxLength = 255;
            this.txt_VoucherNo.Name = "txt_VoucherNo";
            this.txt_VoucherNo.Size = new System.Drawing.Size(146, 25);
            this.txt_VoucherNo.TabIndex = 79;
            // 
            // lbl_VoucherNo
            // 
            this.lbl_VoucherNo.AutoSize = true;
            this.lbl_VoucherNo.Location = new System.Drawing.Point(6, 32);
            this.lbl_VoucherNo.Name = "lbl_VoucherNo";
            this.lbl_VoucherNo.Size = new System.Drawing.Size(96, 19);
            this.lbl_VoucherNo.TabIndex = 80;
            this.lbl_VoucherNo.Text = "Voucher No";
            // 
            // txt_BankName
            // 
            this.txt_BankName.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txt_BankName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_BankName.Enabled = false;
            this.txt_BankName.Location = new System.Drawing.Point(122, 6);
            this.txt_BankName.MaxLength = 255;
            this.txt_BankName.Name = "txt_BankName";
            this.txt_BankName.Size = new System.Drawing.Size(434, 25);
            this.txt_BankName.TabIndex = 77;
            // 
            // lbl_BankName
            // 
            this.lbl_BankName.AutoSize = true;
            this.lbl_BankName.Location = new System.Drawing.Point(6, 6);
            this.lbl_BankName.Name = "lbl_BankName";
            this.lbl_BankName.Size = new System.Drawing.Size(96, 19);
            this.lbl_BankName.TabIndex = 78;
            this.lbl_BankName.Text = "Bank Name";
            // 
            // msk_VoucherDate
            // 
            this.msk_VoucherDate.BackColor = System.Drawing.SystemColors.HighlightText;
            this.msk_VoucherDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_VoucherDate.Enabled = false;
            this.msk_VoucherDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.msk_VoucherDate.Location = new System.Drawing.Point(441, 32);
            this.msk_VoucherDate.Mask = "00/00/0000";
            this.msk_VoucherDate.Name = "msk_VoucherDate";
            this.msk_VoucherDate.Size = new System.Drawing.Size(146, 25);
            this.msk_VoucherDate.TabIndex = 75;
            // 
            // lbl_VoucherDate
            // 
            this.lbl_VoucherDate.AutoSize = true;
            this.lbl_VoucherDate.Location = new System.Drawing.Point(274, 34);
            this.lbl_VoucherDate.Name = "lbl_VoucherDate";
            this.lbl_VoucherDate.Size = new System.Drawing.Size(113, 19);
            this.lbl_VoucherDate.TabIndex = 76;
            this.lbl_VoucherDate.Text = "Voucher Date";
            // 
            // mrPanel1
            // 
            this.mrPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel1.Controls.Add(this.BtnLedger);
            this.mrPanel1.Controls.Add(this.btn_Show);
            this.mrPanel1.Controls.Add(this.btnCancel);
            this.mrPanel1.Controls.Add(this.clsSeparator1);
            this.mrPanel1.Controls.Add(this.txt_Station);
            this.mrPanel1.Controls.Add(this.txt_BankName);
            this.mrPanel1.Controls.Add(this.lbl_Station);
            this.mrPanel1.Controls.Add(this.lbl_VoucherDate);
            this.mrPanel1.Controls.Add(this.msk_VoucherDate);
            this.mrPanel1.Controls.Add(this.msk_ClearingDate);
            this.mrPanel1.Controls.Add(this.lbl_BankName);
            this.mrPanel1.Controls.Add(this.lbl_ClearingDate);
            this.mrPanel1.Controls.Add(this.lbl_VoucherNo);
            this.mrPanel1.Controls.Add(this.txt_ChequeNo);
            this.mrPanel1.Controls.Add(this.txt_VoucherNo);
            this.mrPanel1.Controls.Add(this.lbl_ChequeNo);
            this.mrPanel1.Controls.Add(this.lbl_ChequeDate);
            this.mrPanel1.Controls.Add(this.msk_ChequeDate);
            this.mrPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrPanel1.Location = new System.Drawing.Point(0, 0);
            this.mrPanel1.Name = "mrPanel1";
            this.mrPanel1.Size = new System.Drawing.Size(592, 163);
            this.mrPanel1.TabIndex = 1;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator1.Location = new System.Drawing.Point(10, 117);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(577, 2);
            this.clsSeparator1.TabIndex = 90;
            this.clsSeparator1.TabStop = false;
            // 
            // btn_Show
            // 
            this.btn_Show.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Show.Appearance.Options.UseFont = true;
            this.btn_Show.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.btn_Show.Location = new System.Drawing.Point(378, 125);
            this.btn_Show.Name = "btn_Show";
            this.btn_Show.Size = new System.Drawing.Size(104, 35);
            this.btn_Show.TabIndex = 91;
            this.btn_Show.Text = "&SAVE";
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(483, 125);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(104, 35);
            this.btnCancel.TabIndex = 92;
            this.btnCancel.Text = "&CANCEL";
            // 
            // BtnLedger
            // 
            this.BtnLedger.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnLedger.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnLedger.Location = new System.Drawing.Point(558, 4);
            this.BtnLedger.Name = "BtnLedger";
            this.BtnLedger.Size = new System.Drawing.Size(27, 27);
            this.BtnLedger.TabIndex = 15;
            this.BtnLedger.TabStop = false;
            this.BtnLedger.UseVisualStyleBackColor = true;
            // 
            // FrmClearingDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 163);
            this.Controls.Add(this.mrPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmClearingDate";
            this.ShowIcon = false;
            this.Text = "Clearing Date";
            this.Load += new System.EventHandler(this.FrmClearingDate_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmClearingDate_KeyPress);
            this.mrPanel1.ResumeLayout(false);
            this.mrPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Label lbl_VoucherDate;
        private Label lbl_VoucherNo;
        private Label lbl_BankName;
        private Label lbl_ChequeNo;
        private Label lbl_ChequeDate;
        private Label lbl_ClearingDate;
        private Label lbl_Station;
        private MrPanel mrPanel1;
        private MrMaskedTextBox msk_VoucherDate;
        private MrTextBox txt_VoucherNo;
        private MrTextBox txt_BankName;
        private MrTextBox txt_ChequeNo;
        private MrMaskedTextBox msk_ChequeDate;
        private MrMaskedTextBox msk_ClearingDate;
        private MrTextBox txt_Station;
        private ClsSeparator clsSeparator1;
        private DevExpress.XtraEditors.SimpleButton btn_Show;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private Button BtnLedger;
    }
}