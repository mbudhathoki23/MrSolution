using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Register_Report.Vat_Report
{
    partial class FrmEntryLogRegister
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
            this.gb_TBOptions = new System.Windows.Forms.GroupBox();
            this.CmbDateType = new System.Windows.Forms.ComboBox();
            this.MskToDate = new MrMaskedTextBox();
            this.MskFrom = new MrMaskedTextBox();
            this.ChkDetails = new System.Windows.Forms.CheckBox();
            this.ChkDate = new System.Windows.Forms.CheckBox();
            this.ChkEntryLog = new System.Windows.Forms.CheckedListBox();
            this.roundPanel1 = new RoundPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnShow = new DevExpress.XtraEditors.SimpleButton();
            this.gb_TBOptions.SuspendLayout();
            this.roundPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_TBOptions
            // 
            this.gb_TBOptions.Controls.Add(this.CmbDateType);
            this.gb_TBOptions.Controls.Add(this.MskToDate);
            this.gb_TBOptions.Controls.Add(this.MskFrom);
            this.gb_TBOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gb_TBOptions.Location = new System.Drawing.Point(4, 135);
            this.gb_TBOptions.Name = "gb_TBOptions";
            this.gb_TBOptions.Size = new System.Drawing.Size(426, 42);
            this.gb_TBOptions.TabIndex = 1;
            this.gb_TBOptions.TabStop = false;
            // 
            // CmbDateType
            // 
            this.CmbDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbDateType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbDateType.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbDateType.FormattingEnabled = true;
            this.CmbDateType.Location = new System.Drawing.Point(6, 11);
            this.CmbDateType.Name = "CmbDateType";
            this.CmbDateType.Size = new System.Drawing.Size(191, 27);
            this.CmbDateType.TabIndex = 0;
            // 
            // MskToDate
            // 
            this.MskToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskToDate.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.MskToDate.Location = new System.Drawing.Point(314, 11);
            this.MskToDate.Mask = "00/00/0000";
            this.MskToDate.Name = "MskToDate";
            this.MskToDate.Size = new System.Drawing.Size(111, 26);
            this.MskToDate.TabIndex = 2;
            this.MskToDate.Enter += new System.EventHandler(this.msk_ToDate_Enter);
            this.MskToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.msk_ToDate_KeyDown);
            this.MskToDate.Leave += new System.EventHandler(this.msk_ToDate_Leave);
            this.MskToDate.Validated += new System.EventHandler(this.msk_ToDate_Validated);
            // 
            // MskFrom
            // 
            this.MskFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskFrom.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.MskFrom.Location = new System.Drawing.Point(202, 11);
            this.MskFrom.Mask = "00/00/0000";
            this.MskFrom.Name = "MskFrom";
            this.MskFrom.Size = new System.Drawing.Size(111, 26);
            this.MskFrom.TabIndex = 1;
            this.MskFrom.Enter += new System.EventHandler(this.msk_FromDate_Enter);
            this.MskFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.msk_FromDate_KeyDown);
            this.MskFrom.Leave += new System.EventHandler(this.msk_FromDate_Leave);
            this.MskFrom.Validated += new System.EventHandler(this.msk_FromDate_Validated);
            // 
            // ChkDetails
            // 
            this.ChkDetails.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.ChkDetails.Location = new System.Drawing.Point(6, 11);
            this.ChkDetails.Name = "ChkDetails";
            this.ChkDetails.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDetails.Size = new System.Drawing.Size(115, 22);
            this.ChkDetails.TabIndex = 0;
            this.ChkDetails.Text = "Details";
            this.ChkDetails.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDetails.UseVisualStyleBackColor = true;
            // 
            // IsDate
            // 
            this.ChkDate.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.ChkDate.Location = new System.Drawing.Point(127, 11);
            this.ChkDate.Name = "ChkDate";
            this.ChkDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDate.Size = new System.Drawing.Size(115, 22);
            this.ChkDate.TabIndex = 1;
            this.ChkDate.Text = "Miti";
            this.ChkDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDate.UseVisualStyleBackColor = true;
            // 
            // ChkEntryLog
            // 
            this.ChkEntryLog.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.ChkEntryLog.FormattingEnabled = true;
            this.ChkEntryLog.Location = new System.Drawing.Point(4, 10);
            this.ChkEntryLog.Name = "ChkEntryLog";
            this.ChkEntryLog.Size = new System.Drawing.Size(427, 130);
            this.ChkEntryLog.TabIndex = 0;
            // 
            // roundPanel1
            // 
            this.roundPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.roundPanel1.Controls.Add(this.ChkEntryLog);
            this.roundPanel1.Controls.Add(this.gb_TBOptions);
            this.roundPanel1.Controls.Add(this.groupBox1);
            this.roundPanel1.Controls.Add(this.groupBox2);
            this.roundPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roundPanel1.Location = new System.Drawing.Point(0, 0);
            this.roundPanel1.Name = "roundPanel1";
            this.roundPanel1.Radious = 25;
            this.roundPanel1.Size = new System.Drawing.Size(435, 250);
            this.roundPanel1.TabIndex = 0;
            this.roundPanel1.TabStop = false;
            this.roundPanel1.TitleBackColor = System.Drawing.Color.DarkSlateBlue;
            this.roundPanel1.TitleFont = new System.Drawing.Font("Bookman Old Style", 1F);
            this.roundPanel1.TitleForeColor = System.Drawing.Color.White;
            this.roundPanel1.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ChkDetails);
            this.groupBox1.Controls.Add(this.ChkDate);
            this.groupBox1.Location = new System.Drawing.Point(4, 171);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(426, 37);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.BtnCancel);
            this.groupBox2.Controls.Add(this.BtnShow);
            this.groupBox2.Location = new System.Drawing.Point(4, 202);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(425, 46);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(301, 8);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(106, 36);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnShow
            // 
            this.BtnShow.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnShow.Appearance.Options.UseFont = true;
            this.BtnShow.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnShow.Location = new System.Drawing.Point(202, 8);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(97, 36);
            this.BtnShow.TabIndex = 0;
            this.BtnShow.Text = "&SHOW";
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // FrmEntryLogRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 250);
            this.Controls.Add(this.roundPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmEntryLogRegister";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Entry Log Register";
            this.Load += new System.EventHandler(this.FrmEntryLogRegister_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmEntryLogRegister_KeyPress);
            this.gb_TBOptions.ResumeLayout(false);
            this.gb_TBOptions.PerformLayout();
            this.roundPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_TBOptions;
        private System.Windows.Forms.CheckBox ChkDate;
        private System.Windows.Forms.CheckedListBox ChkEntryLog;
        private System.Windows.Forms.MaskedTextBox MskToDate;
        private System.Windows.Forms.MaskedTextBox MskFrom;
        private System.Windows.Forms.CheckBox ChkDetails;
        private RoundPanel roundPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox CmbDateType;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnShow;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}