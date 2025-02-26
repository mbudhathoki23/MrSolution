
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Register_Report.Analysis_Report
{
    partial class FrmMemberShipReports
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
            this.BtnShow = new DevExpress.XtraEditors.SimpleButton();
            this.ChkDate = new System.Windows.Forms.CheckBox();
            this.mrGroup1 = new MrGroup();
            this.CmbDateType = new System.Windows.Forms.ComboBox();
            this.MskFrom = new MrMaskedTextBox();
            this.MskToDate = new MrMaskedTextBox();
            this.clsSeparator2 = new ClsSeparator();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.mrGroup3 = new MrGroup();
            this.PanelHeader = new MrPanel();
            this.rChkSummary = new System.Windows.Forms.RadioButton();
            this.rChkDetails = new System.Windows.Forms.RadioButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.mrGroup1.SuspendLayout();
            this.mrGroup3.SuspendLayout();
            this.PanelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnShow
            // 
            this.BtnShow.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnShow.Appearance.Options.UseFont = true;
            this.BtnShow.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnShow.Location = new System.Drawing.Point(119, 67);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(99, 36);
            this.BtnShow.TabIndex = 8;
            this.BtnShow.Text = "&SHOW";
            // 
            // IsDate
            // 
            this.ChkDate.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.ChkDate.Location = new System.Drawing.Point(11, 33);
            this.ChkDate.Margin = new System.Windows.Forms.Padding(4);
            this.ChkDate.Name = "ChkDate";
            this.ChkDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDate.Size = new System.Drawing.Size(146, 27);
            this.ChkDate.TabIndex = 6;
            this.ChkDate.Text = "Date";
            this.ChkDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDate.UseVisualStyleBackColor = true;
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.CmbDateType);
            this.mrGroup1.Controls.Add(this.MskFrom);
            this.mrGroup1.Controls.Add(this.rChkSummary);
            this.mrGroup1.Controls.Add(this.MskToDate);
            this.mrGroup1.Controls.Add(this.rChkDetails);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "Date Filter";
            this.mrGroup1.Location = new System.Drawing.Point(3, 3);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 10;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = false;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(350, 116);
            this.mrGroup1.TabIndex = 0;
            // 
            // CmbDateType
            // 
            this.CmbDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbDateType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbDateType.Font = new System.Drawing.Font("Bookman Old Style", 12F);
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
            this.CmbDateType.Location = new System.Drawing.Point(10, 52);
            this.CmbDateType.Margin = new System.Windows.Forms.Padding(4);
            this.CmbDateType.Name = "CmbDateType";
            this.CmbDateType.Size = new System.Drawing.Size(325, 28);
            this.CmbDateType.TabIndex = 2;
            // 
            // MskFrom
            // 
            this.MskFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskFrom.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.MskFrom.Location = new System.Drawing.Point(11, 84);
            this.MskFrom.Margin = new System.Windows.Forms.Padding(4);
            this.MskFrom.Mask = "00/00/0000";
            this.MskFrom.Name = "MskFrom";
            this.MskFrom.Size = new System.Drawing.Size(126, 26);
            this.MskFrom.TabIndex = 3;
            // 
            // MskToDate
            // 
            this.MskToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskToDate.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.MskToDate.Location = new System.Drawing.Point(139, 84);
            this.MskToDate.Margin = new System.Windows.Forms.Padding(4);
            this.MskToDate.Mask = "00/00/0000";
            this.MskToDate.Name = "MskToDate";
            this.MskToDate.Size = new System.Drawing.Size(126, 26);
            this.MskToDate.TabIndex = 4;
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.Wheat;
            this.clsSeparator2.Location = new System.Drawing.Point(3, 63);
            this.clsSeparator2.Margin = new System.Windows.Forms.Padding(4);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Padding = new System.Windows.Forms.Padding(4);
            this.clsSeparator2.Size = new System.Drawing.Size(332, 2);
            this.clsSeparator2.TabIndex = 12;
            this.clsSeparator2.TabStop = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(219, 67);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(116, 36);
            this.BtnCancel.TabIndex = 9;
            this.BtnCancel.Text = "&CANCEL";
            // 
            // mrGroup3
            // 
            this.mrGroup3.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup3.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup3.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.Vertical;
            this.mrGroup3.BorderColor = System.Drawing.Color.White;
            this.mrGroup3.BorderThickness = 1F;
            this.mrGroup3.Controls.Add(this.checkBox1);
            this.mrGroup3.Controls.Add(this.BtnShow);
            this.mrGroup3.Controls.Add(this.ChkDate);
            this.mrGroup3.Controls.Add(this.clsSeparator2);
            this.mrGroup3.Controls.Add(this.BtnCancel);
            this.mrGroup3.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup3.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.mrGroup3.GroupImage = null;
            this.mrGroup3.GroupTitle = "Filter Value";
            this.mrGroup3.Location = new System.Drawing.Point(3, 120);
            this.mrGroup3.Name = "mrGroup3";
            this.mrGroup3.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup3.PaintGroupBox = false;
            this.mrGroup3.RoundCorners = 10;
            this.mrGroup3.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup3.ShadowControl = false;
            this.mrGroup3.ShadowThickness = 3;
            this.mrGroup3.Size = new System.Drawing.Size(350, 116);
            this.mrGroup3.TabIndex = 2;
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.mrGroup3);
            this.PanelHeader.Controls.Add(this.mrGroup1);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Margin = new System.Windows.Forms.Padding(4);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(359, 240);
            this.PanelHeader.TabIndex = 1;
            // 
            // rChkSummary
            // 
            this.rChkSummary.Checked = true;
            this.rChkSummary.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rChkSummary.Location = new System.Drawing.Point(11, 26);
            this.rChkSummary.Name = "rChkSummary";
            this.rChkSummary.Size = new System.Drawing.Size(108, 24);
            this.rChkSummary.TabIndex = 0;
            this.rChkSummary.TabStop = true;
            this.rChkSummary.Text = "Summary";
            this.rChkSummary.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rChkDetails
            // 
            this.rChkDetails.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rChkDetails.Location = new System.Drawing.Point(136, 26);
            this.rChkDetails.Name = "rChkDetails";
            this.rChkDetails.Size = new System.Drawing.Size(99, 24);
            this.rChkDetails.TabIndex = 1;
            this.rChkDetails.Text = "Details";
            this.rChkDetails.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBox1
            // 
            this.checkBox1.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.checkBox1.Location = new System.Drawing.Point(165, 33);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox1.Size = new System.Drawing.Size(146, 27);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "Date";
            this.checkBox1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // FrmMemberShipReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 240);
            this.Controls.Add(this.PanelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Name = "FrmMemberShipReports";
            this.Text = "Member Ship Reports";
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            this.mrGroup3.ResumeLayout(false);
            this.PanelHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton BtnShow;
        private System.Windows.Forms.CheckBox ChkDate;
        private MrGroup mrGroup1;
        private System.Windows.Forms.ComboBox CmbDateType;
        private System.Windows.Forms.MaskedTextBox MskFrom;
        private System.Windows.Forms.MaskedTextBox MskToDate;
        private ClsSeparator clsSeparator2;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private MrGroup mrGroup3;
        private System.Windows.Forms.Panel PanelHeader;
        private System.Windows.Forms.RadioButton rChkSummary;
        private System.Windows.Forms.RadioButton rChkDetails;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}