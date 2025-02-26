using System;
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.SystemSetting
{
    partial class FrmSMSConfig
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
            this.roundPanel1 = new RoundPanel();
            this.TxtAlterNumber = new MrTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ChkPurchaseReturn = new System.Windows.Forms.CheckBox();
            this.ChkPurchaseInvoice = new System.Windows.Forms.CheckBox();
            this.ChkJournalVoucher = new System.Windows.Forms.CheckBox();
            this.ChkSalesReturn = new System.Windows.Forms.CheckBox();
            this.ChkSalesInvoice = new System.Windows.Forms.CheckBox();
            this.ChkCashBank = new System.Windows.Forms.CheckBox();
            this.Btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_Save = new DevExpress.XtraEditors.SimpleButton();
            this.clsSeparator1 = new ClsSeparator();
            this.TxtToken = new MrTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.roundPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // roundPanel1
            // 
            this.roundPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.roundPanel1.Controls.Add(this.TxtAlterNumber);
            this.roundPanel1.Controls.Add(this.label2);
            this.roundPanel1.Controls.Add(this.groupBox1);
            this.roundPanel1.Controls.Add(this.Btn_Cancel);
            this.roundPanel1.Controls.Add(this.Btn_Save);
            this.roundPanel1.Controls.Add(this.clsSeparator1);
            this.roundPanel1.Controls.Add(this.TxtToken);
            this.roundPanel1.Controls.Add(this.label1);
            this.roundPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roundPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.roundPanel1.Location = new System.Drawing.Point(0, 0);
            this.roundPanel1.Name = "roundPanel1";
            this.roundPanel1.Radious = 25;
            this.roundPanel1.Size = new System.Drawing.Size(412, 270);
            this.roundPanel1.TabIndex = 0;
            this.roundPanel1.TabStop = false;
            this.roundPanel1.Text = "SMS Config";
            this.roundPanel1.TitleBackColor = System.Drawing.Color.DarkSlateBlue;
            this.roundPanel1.TitleFont = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roundPanel1.TitleForeColor = System.Drawing.Color.White;
            this.roundPanel1.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
            // 
            // TxtAlterNumber
            // 
            this.TxtAlterNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAlterNumber.Location = new System.Drawing.Point(73, 199);
            this.TxtAlterNumber.Name = "TxtAlterNumber";
            this.TxtAlterNumber.Size = new System.Drawing.Size(332, 25);
            this.TxtAlterNumber.TabIndex = 2;
            this.TxtAlterNumber.TextBoxType = TextBoxType.Date;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 202);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 19);
            this.label2.TabIndex = 39;
            this.label2.Text = "Alter";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ChkPurchaseReturn);
            this.groupBox1.Controls.Add(this.ChkPurchaseInvoice);
            this.groupBox1.Controls.Add(this.ChkJournalVoucher);
            this.groupBox1.Controls.Add(this.ChkSalesReturn);
            this.groupBox1.Controls.Add(this.ChkSalesInvoice);
            this.groupBox1.Controls.Add(this.ChkCashBank);
            this.groupBox1.Location = new System.Drawing.Point(7, 78);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(398, 119);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SMS ON MODULE";
            // 
            // ChkPurchaseReturn
            // 
            this.ChkPurchaseReturn.AutoSize = true;
            this.ChkPurchaseReturn.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.ChkPurchaseReturn.Location = new System.Drawing.Point(222, 39);
            this.ChkPurchaseReturn.Name = "ChkPurchaseReturn";
            this.ChkPurchaseReturn.Size = new System.Drawing.Size(168, 23);
            this.ChkPurchaseReturn.TabIndex = 5;
            this.ChkPurchaseReturn.Text = "PURCHASE RETURN";
            this.ChkPurchaseReturn.UseVisualStyleBackColor = true;
            // 
            // ChkPurchaseInvoice
            // 
            this.ChkPurchaseInvoice.AutoSize = true;
            this.ChkPurchaseInvoice.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.ChkPurchaseInvoice.Location = new System.Drawing.Point(222, 15);
            this.ChkPurchaseInvoice.Name = "ChkPurchaseInvoice";
            this.ChkPurchaseInvoice.Size = new System.Drawing.Size(170, 23);
            this.ChkPurchaseInvoice.TabIndex = 4;
            this.ChkPurchaseInvoice.Text = "PURCHASE INVOICE";
            this.ChkPurchaseInvoice.UseVisualStyleBackColor = true;
            // 
            // ChkJournalVoucher
            // 
            this.ChkJournalVoucher.AutoSize = true;
            this.ChkJournalVoucher.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.ChkJournalVoucher.Location = new System.Drawing.Point(9, 47);
            this.ChkJournalVoucher.Name = "ChkJournalVoucher";
            this.ChkJournalVoucher.Size = new System.Drawing.Size(170, 23);
            this.ChkJournalVoucher.TabIndex = 1;
            this.ChkJournalVoucher.Text = "JOURNAL VOUCHER";
            this.ChkJournalVoucher.UseVisualStyleBackColor = true;
            // 
            // ChkSalesReturn
            // 
            this.ChkSalesReturn.AutoSize = true;
            this.ChkSalesReturn.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.ChkSalesReturn.Location = new System.Drawing.Point(9, 70);
            this.ChkSalesReturn.Name = "ChkSalesReturn";
            this.ChkSalesReturn.Size = new System.Drawing.Size(135, 23);
            this.ChkSalesReturn.TabIndex = 2;
            this.ChkSalesReturn.Text = "SALES RETURN";
            this.ChkSalesReturn.UseVisualStyleBackColor = true;
            // 
            // ChkSalesInvoice
            // 
            this.ChkSalesInvoice.AutoSize = true;
            this.ChkSalesInvoice.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.ChkSalesInvoice.Location = new System.Drawing.Point(9, 93);
            this.ChkSalesInvoice.Name = "ChkSalesInvoice";
            this.ChkSalesInvoice.Size = new System.Drawing.Size(137, 23);
            this.ChkSalesInvoice.TabIndex = 3;
            this.ChkSalesInvoice.Text = "SALES INVOICE";
            this.ChkSalesInvoice.UseVisualStyleBackColor = true;
            // 
            // ChkCashBank
            // 
            this.ChkCashBank.AutoSize = true;
            this.ChkCashBank.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.ChkCashBank.Location = new System.Drawing.Point(9, 24);
            this.ChkCashBank.Name = "ChkCashBank";
            this.ChkCashBank.Size = new System.Drawing.Size(127, 23);
            this.ChkCashBank.TabIndex = 0;
            this.ChkCashBank.Text = "CASH && BANK";
            this.ChkCashBank.UseVisualStyleBackColor = true;
            this.ChkCashBank.CheckedChanged += new System.EventHandler(this.ChkCashBank_CheckedChanged);
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Cancel.Appearance.Options.UseFont = true;
            this.Btn_Cancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.Btn_Cancel.Location = new System.Drawing.Point(197, 231);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(105, 35);
            this.Btn_Cancel.TabIndex = 4;
            this.Btn_Cancel.Text = "&CANCEL";
            this.Btn_Cancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // Btn_Save
            // 
            this.Btn_Save.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Save.Appearance.Options.UseFont = true;
            this.Btn_Save.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.Btn_Save.Location = new System.Drawing.Point(90, 231);
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new System.Drawing.Size(104, 35);
            this.Btn_Save.TabIndex = 3;
            this.Btn_Save.Text = "&SAVE";
            this.Btn_Save.Click += new System.EventHandler(this.BtnSave_ClickAsync);
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator1.Location = new System.Drawing.Point(7, 226);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(398, 2);
            this.clsSeparator1.TabIndex = 6;
            this.clsSeparator1.TabStop = false;
            // 
            // TxtToken
            // 
            this.TxtToken.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtToken.Location = new System.Drawing.Point(73, 26);
            this.TxtToken.Multiline = true;
            this.TxtToken.Name = "TxtToken";
            this.TxtToken.Size = new System.Drawing.Size(332, 50);
            this.TxtToken.TabIndex = 0;
            this.TxtToken.Leave += new System.EventHandler(this.TxtToken_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Token";
            // 
            // FrmSMSConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 270);
            this.Controls.Add(this.roundPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmSMSConfig";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "SMS CONFRIGRATION";
            this.Load += new System.EventHandler(this.FrmSMSConfig_Load);
            this.roundPanel1.ResumeLayout(false);
            this.roundPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        private void ChkCashBank_CheckedChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        #endregion

        private RoundPanel roundPanel1;
        private System.Windows.Forms.Label label1;
        private ClsSeparator clsSeparator1;
        public DevExpress.XtraEditors.SimpleButton Btn_Cancel;
        public DevExpress.XtraEditors.SimpleButton Btn_Save;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox ChkCashBank;
		private System.Windows.Forms.CheckBox ChkSalesReturn;
		private System.Windows.Forms.CheckBox ChkSalesInvoice;
		private System.Windows.Forms.CheckBox ChkPurchaseInvoice;
		private System.Windows.Forms.CheckBox ChkJournalVoucher;
		private System.Windows.Forms.CheckBox ChkPurchaseReturn;
		private System.Windows.Forms.Label label2;
        private MrTextBox TxtToken;
        private MrTextBox TxtAlterNumber;
    }
}