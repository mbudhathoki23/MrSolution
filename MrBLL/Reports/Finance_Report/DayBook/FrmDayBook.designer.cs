using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MrDAL.Control.ControlsEx.Control;
using ComboBox = System.Windows.Forms.ComboBox;

namespace MrBLL.Reports.Finance_Report.DayBook
{
    partial class FrmDayBook
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
            this.CmbDateType = new System.Windows.Forms.ComboBox();
            this.ChkSummary = new System.Windows.Forms.CheckBox();
            this.ChkIsTFormat = new System.Windows.Forms.CheckBox();
            this.TxtFilterValue = new MrTextBox();
            this.lbl_Find = new System.Windows.Forms.Label();
            this.ChkIsDate = new System.Windows.Forms.CheckBox();
            this.ChkListDaybook = new System.Windows.Forms.CheckedListBox();
            this.ChkIncludeSubledger = new System.Windows.Forms.CheckBox();
            this.ChkIncludeRemarks = new System.Windows.Forms.CheckBox();
            this.MskToDate = new MrMaskedTextBox();
            this.MskFrom = new MrMaskedTextBox();
            this.ckbSelectAll = new System.Windows.Forms.CheckBox();
            this.chk_ViewDxReport = new System.Windows.Forms.CheckBox();
            this.ChkCombineSales = new System.Windows.Forms.CheckBox();
            this.RbtnCurrencyBoth = new System.Windows.Forms.RadioButton();
            this.RbtnForeign = new System.Windows.Forms.RadioButton();
            this.RbtnLocal = new System.Windows.Forms.RadioButton();
            this.roundPanel5 = new RoundPanel();
            this.BtnShow = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.clsSeparator1 = new ClsSeparator();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.roundPanel5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // CmbDateType
            // 
            this.CmbDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbDateType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbDateType.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbDateType.FormattingEnabled = true;
            this.CmbDateType.Location = new System.Drawing.Point(4, 11);
            this.CmbDateType.Name = "CmbDateType";
            this.CmbDateType.Size = new System.Drawing.Size(246, 28);
            this.CmbDateType.TabIndex = 0;
            this.CmbDateType.SelectedIndexChanged += new System.EventHandler(this.cmbSysDateType_SelectedIndexChanged);
            // 
            // ChkSummary
            // 
            this.ChkSummary.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSummary.Location = new System.Drawing.Point(128, 12);
            this.ChkSummary.Name = "ChkSummary";
            this.ChkSummary.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSummary.Size = new System.Drawing.Size(124, 23);
            this.ChkSummary.TabIndex = 1;
            this.ChkSummary.Text = "Summary";
            this.ChkSummary.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSummary.UseVisualStyleBackColor = true;
            // 
            // ChkIsTFormat
            // 
            this.ChkIsTFormat.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIsTFormat.Location = new System.Drawing.Point(6, 13);
            this.ChkIsTFormat.Name = "ChkIsTFormat";
            this.ChkIsTFormat.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIsTFormat.Size = new System.Drawing.Size(116, 23);
            this.ChkIsTFormat.TabIndex = 0;
            this.ChkIsTFormat.Text = "T - Format";
            this.ChkIsTFormat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIsTFormat.UseVisualStyleBackColor = true;
            // 
            // TxtFilterValue
            // 
            this.TxtFilterValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtFilterValue.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtFilterValue.Location = new System.Drawing.Point(296, 14);
            this.TxtFilterValue.Name = "TxtFilterValue";
            this.TxtFilterValue.Size = new System.Drawing.Size(204, 26);
            this.TxtFilterValue.TabIndex = 3;
            // 
            // lbl_Find
            // 
            this.lbl_Find.AutoSize = true;
            this.lbl_Find.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Find.Location = new System.Drawing.Point(251, 17);
            this.lbl_Find.Name = "lbl_Find";
            this.lbl_Find.Size = new System.Drawing.Size(45, 20);
            this.lbl_Find.TabIndex = 71;
            this.lbl_Find.Text = "Find";
            // 
            // ChkIsDate
            // 
            this.ChkIsDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIsDate.Location = new System.Drawing.Point(415, 12);
            this.ChkIsDate.Name = "ChkIsDate";
            this.ChkIsDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIsDate.Size = new System.Drawing.Size(91, 23);
            this.ChkIsDate.TabIndex = 3;
            this.ChkIsDate.Text = "Miti";
            this.ChkIsDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIsDate.UseVisualStyleBackColor = true;
            // 
            // ChkListDaybook
            // 
            this.ChkListDaybook.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ChkListDaybook.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkListDaybook.FormattingEnabled = true;
            this.ChkListDaybook.Location = new System.Drawing.Point(3, 9);
            this.ChkListDaybook.Name = "ChkListDaybook";
            this.ChkListDaybook.Size = new System.Drawing.Size(510, 214);
            this.ChkListDaybook.TabIndex = 0;
            this.ChkListDaybook.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ChkListDayBook_ItemCheck);
            // 
            // ChkIncludeSubledger
            // 
            this.ChkIncludeSubledger.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeSubledger.Location = new System.Drawing.Point(260, 37);
            this.ChkIncludeSubledger.Name = "ChkIncludeSubledger";
            this.ChkIncludeSubledger.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeSubledger.Size = new System.Drawing.Size(147, 23);
            this.ChkIncludeSubledger.TabIndex = 6;
            this.ChkIncludeSubledger.Text = "Sub Ledger";
            this.ChkIncludeSubledger.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeSubledger.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeRemarks
            // 
            this.ChkIncludeRemarks.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeRemarks.Location = new System.Drawing.Point(6, 37);
            this.ChkIncludeRemarks.Name = "ChkIncludeRemarks";
            this.ChkIncludeRemarks.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeRemarks.Size = new System.Drawing.Size(116, 23);
            this.ChkIncludeRemarks.TabIndex = 4;
            this.ChkIncludeRemarks.Text = "Remarks";
            this.ChkIncludeRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeRemarks.UseVisualStyleBackColor = true;
            // 
            // MskToDate
            // 
            this.MskToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskToDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskToDate.Location = new System.Drawing.Point(380, 12);
            this.MskToDate.Mask = "00/00/0000";
            this.MskToDate.Name = "MskToDate";
            this.MskToDate.Size = new System.Drawing.Size(122, 26);
            this.MskToDate.TabIndex = 2;
            // 
            // MskFrom
            // 
            this.MskFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskFrom.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskFrom.Location = new System.Drawing.Point(250, 12);
            this.MskFrom.Mask = "00/00/0000";
            this.MskFrom.Name = "MskFrom";
            this.MskFrom.Size = new System.Drawing.Size(129, 26);
            this.MskFrom.TabIndex = 1;
            // 
            // ckbSelectAll
            // 
            this.ckbSelectAll.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbSelectAll.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.ckbSelectAll.ForeColor = System.Drawing.Color.Black;
            this.ckbSelectAll.Location = new System.Drawing.Point(128, 36);
            this.ckbSelectAll.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ckbSelectAll.Name = "ckbSelectAll";
            this.ckbSelectAll.Size = new System.Drawing.Size(124, 24);
            this.ckbSelectAll.TabIndex = 5;
            this.ckbSelectAll.Text = "Select All";
            this.ckbSelectAll.UseVisualStyleBackColor = true;
            this.ckbSelectAll.CheckedChanged += new System.EventHandler(this.ckbSelectAll_CheckedChanged);
            // 
            // chk_ViewDxReport
            // 
            this.chk_ViewDxReport.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_ViewDxReport.Location = new System.Drawing.Point(8, 69);
            this.chk_ViewDxReport.Name = "chk_ViewDxReport";
            this.chk_ViewDxReport.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_ViewDxReport.Size = new System.Drawing.Size(225, 27);
            this.chk_ViewDxReport.TabIndex = 10;
            this.chk_ViewDxReport.Text = "View Dynamic Report";
            this.chk_ViewDxReport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_ViewDxReport.UseVisualStyleBackColor = true;
            // 
            // ChkCombineSales
            // 
            this.ChkCombineSales.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkCombineSales.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.ChkCombineSales.ForeColor = System.Drawing.Color.Black;
            this.ChkCombineSales.Location = new System.Drawing.Point(260, 12);
            this.ChkCombineSales.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ChkCombineSales.Name = "ChkCombineSales";
            this.ChkCombineSales.Size = new System.Drawing.Size(147, 24);
            this.ChkCombineSales.TabIndex = 2;
            this.ChkCombineSales.Text = "Combine Sales";
            this.ChkCombineSales.UseVisualStyleBackColor = true;
            this.ChkCombineSales.CheckedChanged += new System.EventHandler(this.ChkCombineSales_CheckedChanged);
            // 
            // RbtnCurrencyBoth
            // 
            this.RbtnCurrencyBoth.AutoSize = true;
            this.RbtnCurrencyBoth.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.RbtnCurrencyBoth.Location = new System.Drawing.Point(82, 15);
            this.RbtnCurrencyBoth.Name = "RbtnCurrencyBoth";
            this.RbtnCurrencyBoth.Size = new System.Drawing.Size(65, 24);
            this.RbtnCurrencyBoth.TabIndex = 1;
            this.RbtnCurrencyBoth.Text = "Both";
            this.RbtnCurrencyBoth.UseVisualStyleBackColor = true;
            // 
            // RbtnForeign
            // 
            this.RbtnForeign.AutoSize = true;
            this.RbtnForeign.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.RbtnForeign.Location = new System.Drawing.Point(155, 15);
            this.RbtnForeign.Name = "RbtnForeign";
            this.RbtnForeign.Size = new System.Drawing.Size(87, 24);
            this.RbtnForeign.TabIndex = 2;
            this.RbtnForeign.Text = "Foreign";
            this.RbtnForeign.UseVisualStyleBackColor = true;
            // 
            // RbtnLocal
            // 
            this.RbtnLocal.AutoSize = true;
            this.RbtnLocal.Checked = true;
            this.RbtnLocal.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.RbtnLocal.Location = new System.Drawing.Point(6, 15);
            this.RbtnLocal.Name = "RbtnLocal";
            this.RbtnLocal.Size = new System.Drawing.Size(68, 24);
            this.RbtnLocal.TabIndex = 0;
            this.RbtnLocal.TabStop = true;
            this.RbtnLocal.Text = "Local";
            this.RbtnLocal.UseVisualStyleBackColor = true;
            // 
            // roundPanel5
            // 
            this.roundPanel5.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.roundPanel5.Controls.Add(this.ChkListDaybook);
            this.roundPanel5.Controls.Add(this.groupBox2);
            this.roundPanel5.Controls.Add(this.groupBox3);
            this.roundPanel5.Controls.Add(this.groupBox1);
            this.roundPanel5.Location = new System.Drawing.Point(2, 2);
            this.roundPanel5.Name = "roundPanel5";
            this.roundPanel5.Radious = 25;
            this.roundPanel5.Size = new System.Drawing.Size(517, 395);
            this.roundPanel5.TabIndex = 0;
            this.roundPanel5.TabStop = false;
            this.roundPanel5.TitleBackColor = System.Drawing.Color.DarkSlateBlue;
            this.roundPanel5.TitleFont = new System.Drawing.Font("Bookman Old Style", 0.01F);
            this.roundPanel5.TitleForeColor = System.Drawing.Color.White;
            this.roundPanel5.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
            // 
            // BtnShow
            // 
            this.BtnShow.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnShow.Appearance.Options.UseFont = true;
            this.BtnShow.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnShow.Location = new System.Drawing.Point(284, 65);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(109, 35);
            this.BtnShow.TabIndex = 8;
            this.BtnShow.Text = "&SHOW";
            this.BtnShow.Click += new System.EventHandler(this.btn_Show_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(395, 65);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(109, 35);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "&CANCEL";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.clsSeparator1);
            this.groupBox1.Controls.Add(this.ChkCombineSales);
            this.groupBox1.Controls.Add(this.BtnShow);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.ChkIsTFormat);
            this.groupBox1.Controls.Add(this.chk_ViewDxReport);
            this.groupBox1.Controls.Add(this.ckbSelectAll);
            this.groupBox1.Controls.Add(this.ChkSummary);
            this.groupBox1.Controls.Add(this.ChkIncludeRemarks);
            this.groupBox1.Controls.Add(this.ChkIncludeSubledger);
            this.groupBox1.Controls.Add(this.ChkIsDate);
            this.groupBox1.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.groupBox1.Location = new System.Drawing.Point(3, 288);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(510, 103);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.Lavender;
            this.clsSeparator1.Location = new System.Drawing.Point(14, 60);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(490, 3);
            this.clsSeparator1.TabIndex = 7;
            this.clsSeparator1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CmbDateType);
            this.groupBox2.Controls.Add(this.MskFrom);
            this.groupBox2.Controls.Add(this.MskToDate);
            this.groupBox2.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.groupBox2.Location = new System.Drawing.Point(3, 216);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(510, 43);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.TxtFilterValue);
            this.groupBox3.Controls.Add(this.RbtnLocal);
            this.groupBox3.Controls.Add(this.lbl_Find);
            this.groupBox3.Controls.Add(this.RbtnForeign);
            this.groupBox3.Controls.Add(this.RbtnCurrencyBoth);
            this.groupBox3.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.groupBox3.Location = new System.Drawing.Point(3, 250);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(510, 47);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // FrmDayBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(521, 397);
            this.Controls.Add(this.roundPanel5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmDayBook";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DAY BOOK REPORTS";
            this.Load += new System.EventHandler(this.FrmDayBook_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmDayBook_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmDayBook_KeyPress);
            this.roundPanel5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private CheckBox ChkIncludeSubledger;
        private CheckBox ChkIncludeRemarks;
        private MaskedTextBox MskToDate;
        private MaskedTextBox MskFrom;
        private CheckedListBox ChkListDaybook;
        private CheckBox ChkIsDate;
        private TextBox TxtFilterValue;
        private Label lbl_Find;
        private CheckBox ChkIsTFormat;
        private CheckBox ChkSummary;
        private CheckBox chk_ViewDxReport;
        private ComboBox CmbDateType;
        private CheckBox ckbSelectAll;
		private RadioButton RbtnCurrencyBoth;
		private RadioButton RbtnForeign;
		private RadioButton RbtnLocal;
		private RoundPanel roundPanel5;
		private SimpleButton BtnShow;
		private SimpleButton btnCancel;
        private CheckBox ChkCombineSales;
        private GroupBox groupBox1;
        private ClsSeparator clsSeparator1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
    }
}