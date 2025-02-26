using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MrDAL.Control.ControlsEx.Control;
using ComboBox = System.Windows.Forms.ComboBox;

namespace MrBLL.Reports.Finance_Report.DayBook
{
    partial class FrmCashBook
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
            this.ChkTFormat = new System.Windows.Forms.CheckBox();
            this.ChkSummary = new System.Windows.Forms.CheckBox();
            this.ChkCombineSales = new System.Windows.Forms.CheckBox();
            this.BtnLedger = new System.Windows.Forms.Button();
            this.TxtCashLedger = new MrTextBox();
            this.lbl_BankName = new System.Windows.Forms.Label();
            this.ChkIsDate = new System.Windows.Forms.CheckBox();
            this.ChkIncludeSubledger = new System.Windows.Forms.CheckBox();
            this.ChkRemarks = new System.Windows.Forms.CheckBox();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Show = new DevExpress.XtraEditors.SimpleButton();
            this.ChkNarration = new System.Windows.Forms.CheckBox();
            this.roundPanel1 = new RoundPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CmbDateType = new System.Windows.Forms.ComboBox();
            this.MskFrom = new MrMaskedTextBox();
            this.MskToDate = new MrMaskedTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ChkVoucherTotal = new System.Windows.Forms.CheckBox();
            this.roundPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChkTFormat
            // 
            this.ChkTFormat.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkTFormat.Location = new System.Drawing.Point(191, 36);
            this.ChkTFormat.Name = "ChkTFormat";
            this.ChkTFormat.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkTFormat.Size = new System.Drawing.Size(160, 22);
            this.ChkTFormat.TabIndex = 5;
            this.ChkTFormat.Text = "T - Format";
            this.ChkTFormat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkTFormat.UseVisualStyleBackColor = true;
            // 
            // ChkSummary
            // 
            this.ChkSummary.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSummary.Location = new System.Drawing.Point(5, 12);
            this.ChkSummary.Name = "ChkSummary";
            this.ChkSummary.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSummary.Size = new System.Drawing.Size(180, 22);
            this.ChkSummary.TabIndex = 0;
            this.ChkSummary.Text = "Summary";
            this.ChkSummary.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSummary.UseVisualStyleBackColor = true;
            this.ChkSummary.CheckedChanged += new System.EventHandler(this.ChkSummary_CheckedChanged);
            // 
            // ChkCombineSales
            // 
            this.ChkCombineSales.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkCombineSales.Location = new System.Drawing.Point(5, 60);
            this.ChkCombineSales.Name = "ChkCombineSales";
            this.ChkCombineSales.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkCombineSales.Size = new System.Drawing.Size(180, 22);
            this.ChkCombineSales.TabIndex = 2;
            this.ChkCombineSales.Text = "Combine Sales";
            this.ChkCombineSales.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkCombineSales.UseVisualStyleBackColor = true;
            // 
            // BtnLedger
            // 
            this.BtnLedger.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnLedger.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnLedger.Location = new System.Drawing.Point(468, 8);
            this.BtnLedger.Name = "BtnLedger";
            this.BtnLedger.Size = new System.Drawing.Size(27, 27);
            this.BtnLedger.TabIndex = 14;
            this.BtnLedger.TabStop = false;
            this.BtnLedger.UseVisualStyleBackColor = true;
            this.BtnLedger.Click += new System.EventHandler(this.BtnLedger_Click);
            // 
            // TxtCashLedger
            // 
            this.TxtCashLedger.BackColor = System.Drawing.Color.White;
            this.TxtCashLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCashLedger.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCashLedger.Location = new System.Drawing.Point(118, 9);
            this.TxtCashLedger.MaxLength = 255;
            this.TxtCashLedger.Name = "TxtCashLedger";
            this.TxtCashLedger.ReadOnly = true;
            this.TxtCashLedger.Size = new System.Drawing.Size(347, 25);
            this.TxtCashLedger.TabIndex = 1;
            this.TxtCashLedger.Enter += new System.EventHandler(this.TxtCashLedger_Enter);
            this.TxtCashLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCashLedger_KeyDown);
            this.TxtCashLedger.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCashLedger_KeyPress);
            this.TxtCashLedger.Leave += new System.EventHandler(this.TxtCashLedger_Leave);
            // 
            // lbl_BankName
            // 
            this.lbl_BankName.AutoSize = true;
            this.lbl_BankName.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_BankName.Location = new System.Drawing.Point(4, 12);
            this.lbl_BankName.Name = "lbl_BankName";
            this.lbl_BankName.Size = new System.Drawing.Size(108, 19);
            this.lbl_BankName.TabIndex = 0;
            this.lbl_BankName.Text = "Cash && Bank";
            // 
            // ChkIsDate
            // 
            this.ChkIsDate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIsDate.Location = new System.Drawing.Point(191, 60);
            this.ChkIsDate.Name = "ChkIsDate";
            this.ChkIsDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIsDate.Size = new System.Drawing.Size(160, 22);
            this.ChkIsDate.TabIndex = 6;
            this.ChkIsDate.Text = "Date";
            this.ChkIsDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIsDate.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeSubledger
            // 
            this.ChkIncludeSubledger.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeSubledger.Location = new System.Drawing.Point(5, 36);
            this.ChkIncludeSubledger.Name = "ChkIncludeSubledger";
            this.ChkIncludeSubledger.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeSubledger.Size = new System.Drawing.Size(180, 22);
            this.ChkIncludeSubledger.TabIndex = 1;
            this.ChkIncludeSubledger.Text = "Sub Ledger";
            this.ChkIncludeSubledger.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeSubledger.UseVisualStyleBackColor = true;
            // 
            // ChkRemarks
            // 
            this.ChkRemarks.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkRemarks.Location = new System.Drawing.Point(191, 12);
            this.ChkRemarks.Name = "ChkRemarks";
            this.ChkRemarks.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkRemarks.Size = new System.Drawing.Size(160, 22);
            this.ChkRemarks.TabIndex = 4;
            this.ChkRemarks.Text = "Remarks";
            this.ChkRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkRemarks.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(340, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(125, 35);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&CANCEL";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btn_Show
            // 
            this.btn_Show.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Show.Appearance.Options.UseFont = true;
            this.btn_Show.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.btn_Show.Location = new System.Drawing.Point(241, 9);
            this.btn_Show.Name = "btn_Show";
            this.btn_Show.Size = new System.Drawing.Size(97, 35);
            this.btn_Show.TabIndex = 0;
            this.btn_Show.Text = "&SHOW";
            this.btn_Show.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // ChkNarration
            // 
            this.ChkNarration.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkNarration.Location = new System.Drawing.Point(5, 84);
            this.ChkNarration.Name = "ChkNarration";
            this.ChkNarration.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkNarration.Size = new System.Drawing.Size(180, 20);
            this.ChkNarration.TabIndex = 3;
            this.ChkNarration.Text = "Narration";
            this.ChkNarration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkNarration.UseVisualStyleBackColor = true;
            // 
            // roundPanel1
            // 
            this.roundPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.roundPanel1.Controls.Add(this.groupBox1);
            this.roundPanel1.Controls.Add(this.groupBox2);
            this.roundPanel1.Controls.Add(this.groupBox4);
            this.roundPanel1.Controls.Add(this.groupBox3);
            this.roundPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roundPanel1.Location = new System.Drawing.Point(0, 0);
            this.roundPanel1.Name = "roundPanel1";
            this.roundPanel1.Radious = 25;
            this.roundPanel1.Size = new System.Drawing.Size(510, 232);
            this.roundPanel1.TabIndex = 0;
            this.roundPanel1.TabStop = false;
            this.roundPanel1.TitleBackColor = System.Drawing.Color.DarkSlateBlue;
            this.roundPanel1.TitleFont = new System.Drawing.Font("Bookman Old Style", 1F);
            this.roundPanel1.TitleForeColor = System.Drawing.Color.White;
            this.roundPanel1.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CmbDateType);
            this.groupBox1.Controls.Add(this.MskFrom);
            this.groupBox1.Controls.Add(this.MskToDate);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(499, 40);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // CmbDateType
            // 
            this.CmbDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbDateType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbDateType.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbDateType.FormattingEnabled = true;
            this.CmbDateType.Items.AddRange(new object[] {
            "Custom Date",
            "Today",
            "Yesterday",
            "Current Week",
            "Last Week",
            "Current Month",
            "Last Month",
            "Upto Date",
            "Accounting Period"});
            this.CmbDateType.Location = new System.Drawing.Point(6, 9);
            this.CmbDateType.Name = "CmbDateType";
            this.CmbDateType.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CmbDateType.Size = new System.Drawing.Size(251, 28);
            this.CmbDateType.TabIndex = 0;
            this.CmbDateType.SelectedIndexChanged += new System.EventHandler(this.CmbDateType_SelectedIndexChanged);
            this.CmbDateType.Enter += new System.EventHandler(this.CmbDateType_Enter);
            this.CmbDateType.Leave += new System.EventHandler(this.CmbDateType_Leave);
            // 
            // MskFrom
            // 
            this.MskFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskFrom.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskFrom.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskFrom.Location = new System.Drawing.Point(263, 9);
            this.MskFrom.Mask = "00/00/0000";
            this.MskFrom.Name = "MskFrom";
            this.MskFrom.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.MskFrom.Size = new System.Drawing.Size(115, 26);
            this.MskFrom.TabIndex = 1;
            this.MskFrom.Enter += new System.EventHandler(this.MskFrom_Enter);
            this.MskFrom.Leave += new System.EventHandler(this.MskFrom_Leave);
            // 
            // MskToDate
            // 
            this.MskToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskToDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskToDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskToDate.Location = new System.Drawing.Point(380, 9);
            this.MskToDate.Mask = "00/00/0000";
            this.MskToDate.Name = "MskToDate";
            this.MskToDate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.MskToDate.Size = new System.Drawing.Size(115, 26);
            this.MskToDate.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TxtCashLedger);
            this.groupBox2.Controls.Add(this.lbl_BankName);
            this.groupBox2.Controls.Add(this.BtnLedger);
            this.groupBox2.Location = new System.Drawing.Point(6, 40);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(499, 40);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ChkVoucherTotal);
            this.groupBox4.Controls.Add(this.ChkSummary);
            this.groupBox4.Controls.Add(this.ChkIncludeSubledger);
            this.groupBox4.Controls.Add(this.ChkNarration);
            this.groupBox4.Controls.Add(this.ChkCombineSales);
            this.groupBox4.Controls.Add(this.ChkIsDate);
            this.groupBox4.Controls.Add(this.ChkRemarks);
            this.groupBox4.Controls.Add(this.ChkTFormat);
            this.groupBox4.Location = new System.Drawing.Point(7, 74);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(498, 111);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_Show);
            this.groupBox3.Controls.Add(this.btnCancel);
            this.groupBox3.Location = new System.Drawing.Point(6, 179);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(499, 46);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            // 
            // ChkVoucherTotal
            // 
            this.ChkVoucherTotal.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkVoucherTotal.Location = new System.Drawing.Point(191, 82);
            this.ChkVoucherTotal.Name = "ChkVoucherTotal";
            this.ChkVoucherTotal.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkVoucherTotal.Size = new System.Drawing.Size(160, 22);
            this.ChkVoucherTotal.TabIndex = 7;
            this.ChkVoucherTotal.Text = "Voucher Total";
            this.ChkVoucherTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkVoucherTotal.UseVisualStyleBackColor = true;
            // 
            // FrmCashBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(510, 232);
            this.Controls.Add(this.roundPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmCashBook";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cash/Bank Book";
            this.Load += new System.EventHandler(this.FrmCashBook_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmCashBook_KeyPress);
            this.roundPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private CheckBox ChkIsDate;
        private CheckBox ChkIncludeSubledger;
        private CheckBox ChkRemarks;
        private Button BtnLedger;
        private TextBox TxtCashLedger;
        private Label lbl_BankName;
        private CheckBox ChkCombineSales;
        private CheckBox ChkTFormat;
        private CheckBox ChkSummary;
        private SimpleButton btn_Show;
        private SimpleButton btnCancel;
        private CheckBox ChkNarration;
        private RoundPanel roundPanel1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private ComboBox CmbDateType;
        private MaskedTextBox MskFrom;
        private MaskedTextBox MskToDate;
        private GroupBox groupBox4;
        private CheckBox ChkVoucherTotal;
    }
}