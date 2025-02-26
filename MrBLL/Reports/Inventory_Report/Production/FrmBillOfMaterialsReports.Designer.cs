
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Inventory_Report.Production
{
    partial class FrmBillOfMaterialsReports
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
            this.rChkGroupWise = new System.Windows.Forms.RadioButton();
            this.rChkProductWise = new System.Windows.Forms.RadioButton();
            this.rChkSubGroupWise = new System.Windows.Forms.RadioButton();
            this.MskFrom = new MrMaskedTextBox();
            this.mrGroup5 = new MrGroup();
            this.ChkDynamicReports = new System.Windows.Forms.CheckBox();
            this.ChkDate = new System.Windows.Forms.CheckBox();
            this.ChkIncludeCostCenter = new System.Windows.Forms.CheckBox();
            this.ChkIncludeDepartment = new System.Windows.Forms.CheckBox();
            this.clsSeparator1 = new ClsSeparator();
            this.ChkSummary = new System.Windows.Forms.CheckBox();
            this.BtnShow = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.ChkSelectAll = new System.Windows.Forms.CheckBox();
            this.CmbDateType = new System.Windows.Forms.ComboBox();
            this.MskToDate = new MrMaskedTextBox();
            this.mrGroup1 = new MrGroup();
            this.panel1 = new MrPanel();
            this.mrGroup3 = new MrGroup();
            this.ChkCostRatio = new System.Windows.Forms.CheckBox();
            this.mrGroup5.SuspendLayout();
            this.mrGroup1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.mrGroup3.SuspendLayout();
            this.SuspendLayout();
            // 
            // rChkGroupWise
            // 
            this.rChkGroupWise.AutoSize = true;
            this.rChkGroupWise.Location = new System.Drawing.Point(5, 51);
            this.rChkGroupWise.Name = "rChkGroupWise";
            this.rChkGroupWise.Size = new System.Drawing.Size(113, 23);
            this.rChkGroupWise.TabIndex = 2;
            this.rChkGroupWise.Text = "Group Wise";
            this.rChkGroupWise.UseVisualStyleBackColor = true;
            // 
            // rChkProductWise
            // 
            this.rChkProductWise.AutoSize = true;
            this.rChkProductWise.Checked = true;
            this.rChkProductWise.Location = new System.Drawing.Point(5, 28);
            this.rChkProductWise.Name = "rChkProductWise";
            this.rChkProductWise.Size = new System.Drawing.Size(125, 23);
            this.rChkProductWise.TabIndex = 1;
            this.rChkProductWise.TabStop = true;
            this.rChkProductWise.Text = "Product Wise";
            this.rChkProductWise.UseVisualStyleBackColor = true;
            // 
            // rChkSubGroupWise
            // 
            this.rChkSubGroupWise.AutoSize = true;
            this.rChkSubGroupWise.Location = new System.Drawing.Point(5, 74);
            this.rChkSubGroupWise.Name = "rChkSubGroupWise";
            this.rChkSubGroupWise.Size = new System.Drawing.Size(141, 23);
            this.rChkSubGroupWise.TabIndex = 3;
            this.rChkSubGroupWise.Text = "SubGroup Wise";
            this.rChkSubGroupWise.UseVisualStyleBackColor = true;
            // 
            // MskFrom
            // 
            this.MskFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskFrom.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskFrom.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskFrom.Location = new System.Drawing.Point(231, 29);
            this.MskFrom.Mask = "00/00/0000";
            this.MskFrom.Name = "MskFrom";
            this.MskFrom.Size = new System.Drawing.Size(129, 25);
            this.MskFrom.TabIndex = 1;
            this.MskFrom.ValidatingType = typeof(System.DateTime);
            // 
            // mrGroup5
            // 
            this.mrGroup5.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup5.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup5.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup5.BorderColor = System.Drawing.Color.White;
            this.mrGroup5.BorderThickness = 1F;
            this.mrGroup5.Controls.Add(this.ChkCostRatio);
            this.mrGroup5.Controls.Add(this.ChkDynamicReports);
            this.mrGroup5.Controls.Add(this.ChkDate);
            this.mrGroup5.Controls.Add(this.ChkIncludeCostCenter);
            this.mrGroup5.Controls.Add(this.ChkIncludeDepartment);
            this.mrGroup5.Controls.Add(this.ChkSummary);
            this.mrGroup5.Controls.Add(this.ChkSelectAll);
            this.mrGroup5.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup5.GroupImage = null;
            this.mrGroup5.GroupTitle = "Filter Value";
            this.mrGroup5.Location = new System.Drawing.Point(178, 70);
            this.mrGroup5.Name = "mrGroup5";
            this.mrGroup5.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup5.PaintGroupBox = false;
            this.mrGroup5.RoundCorners = 10;
            this.mrGroup5.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup5.ShadowControl = false;
            this.mrGroup5.ShadowThickness = 3;
            this.mrGroup5.Size = new System.Drawing.Size(321, 131);
            this.mrGroup5.TabIndex = 1;
            // 
            // ChkDynamicReports
            // 
            this.ChkDynamicReports.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDynamicReports.Location = new System.Drawing.Point(146, 73);
            this.ChkDynamicReports.Name = "ChkDynamicReports";
            this.ChkDynamicReports.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDynamicReports.Size = new System.Drawing.Size(135, 24);
            this.ChkDynamicReports.TabIndex = 12;
            this.ChkDynamicReports.Text = "Advance";
            this.ChkDynamicReports.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDynamicReports.UseVisualStyleBackColor = true;
            // 
            // IsDate
            // 
            this.ChkDate.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDate.Location = new System.Drawing.Point(12, 77);
            this.ChkDate.Name = "ChkDate";
            this.ChkDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDate.Size = new System.Drawing.Size(118, 24);
            this.ChkDate.TabIndex = 3;
            this.ChkDate.Text = "Date";
            this.ChkDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDate.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeCostCenter
            // 
            this.ChkIncludeCostCenter.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeCostCenter.Location = new System.Drawing.Point(12, 52);
            this.ChkIncludeCostCenter.Name = "ChkIncludeCostCenter";
            this.ChkIncludeCostCenter.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeCostCenter.Size = new System.Drawing.Size(118, 24);
            this.ChkIncludeCostCenter.TabIndex = 2;
            this.ChkIncludeCostCenter.Text = "CostCenter";
            this.ChkIncludeCostCenter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeCostCenter.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeDepartment
            // 
            this.ChkIncludeDepartment.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeDepartment.Location = new System.Drawing.Point(146, 27);
            this.ChkIncludeDepartment.Name = "ChkIncludeDepartment";
            this.ChkIncludeDepartment.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeDepartment.Size = new System.Drawing.Size(135, 23);
            this.ChkIncludeDepartment.TabIndex = 1;
            this.ChkIncludeDepartment.Text = "Department";
            this.ChkIncludeDepartment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeDepartment.UseVisualStyleBackColor = true;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator1.Location = new System.Drawing.Point(6, 207);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(499, 2);
            this.clsSeparator1.TabIndex = 12;
            this.clsSeparator1.TabStop = false;
            // 
            // ChkSummary
            // 
            this.ChkSummary.Checked = true;
            this.ChkSummary.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkSummary.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSummary.Location = new System.Drawing.Point(12, 27);
            this.ChkSummary.Name = "ChkSummary";
            this.ChkSummary.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSummary.Size = new System.Drawing.Size(118, 24);
            this.ChkSummary.TabIndex = 0;
            this.ChkSummary.Text = "Summary";
            this.ChkSummary.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSummary.UseVisualStyleBackColor = true;
            this.ChkSummary.CheckedChanged += new System.EventHandler(this.ChkSummary_CheckedChanged);
            // 
            // BtnShow
            // 
            this.BtnShow.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnShow.Appearance.Options.UseFont = true;
            this.BtnShow.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.BtnShow.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnShow.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.BtnShow.Location = new System.Drawing.Point(268, 212);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(95, 34);
            this.BtnShow.TabIndex = 4;
            this.BtnShow.Text = "&SHOW";
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.BtnCancel.Location = new System.Drawing.Point(365, 212);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(112, 34);
            this.BtnCancel.TabIndex = 5;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // ChkSelectAll
            // 
            this.ChkSelectAll.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSelectAll.Location = new System.Drawing.Point(146, 50);
            this.ChkSelectAll.Name = "ChkSelectAll";
            this.ChkSelectAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSelectAll.Size = new System.Drawing.Size(135, 23);
            this.ChkSelectAll.TabIndex = 3;
            this.ChkSelectAll.Text = "Select All";
            this.ChkSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSelectAll.UseVisualStyleBackColor = true;
            // 
            // CmbDateType
            // 
            this.CmbDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbDateType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbDateType.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.CmbDateType.Location = new System.Drawing.Point(5, 27);
            this.CmbDateType.Name = "CmbDateType";
            this.CmbDateType.Size = new System.Drawing.Size(224, 27);
            this.CmbDateType.TabIndex = 0;
            this.CmbDateType.SelectedIndexChanged += new System.EventHandler(this.CmbDateType_SelectedIndexChanged);
            // 
            // MskToDate
            // 
            this.MskToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskToDate.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskToDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskToDate.Location = new System.Drawing.Point(361, 29);
            this.MskToDate.Mask = "00/00/0000";
            this.MskToDate.Name = "MskToDate";
            this.MskToDate.Size = new System.Drawing.Size(129, 25);
            this.MskToDate.TabIndex = 2;
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
            this.mrGroup1.Controls.Add(this.MskToDate);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
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
            this.mrGroup1.Size = new System.Drawing.Size(497, 58);
            this.mrGroup1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.mrGroup5);
            this.panel1.Controls.Add(this.mrGroup3);
            this.panel1.Controls.Add(this.mrGroup1);
            this.panel1.Controls.Add(this.BtnShow);
            this.panel1.Controls.Add(this.BtnCancel);
            this.panel1.Controls.Add(this.clsSeparator1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(508, 250);
            this.panel1.TabIndex = 0;
            // 
            // mrGroup3
            // 
            this.mrGroup3.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup3.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup3.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup3.BorderColor = System.Drawing.Color.White;
            this.mrGroup3.BorderThickness = 1F;
            this.mrGroup3.Controls.Add(this.rChkGroupWise);
            this.mrGroup3.Controls.Add(this.rChkProductWise);
            this.mrGroup3.Controls.Add(this.rChkSubGroupWise);
            this.mrGroup3.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup3.GroupImage = null;
            this.mrGroup3.GroupTitle = "Report Type";
            this.mrGroup3.Location = new System.Drawing.Point(3, 70);
            this.mrGroup3.Name = "mrGroup3";
            this.mrGroup3.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup3.PaintGroupBox = false;
            this.mrGroup3.RoundCorners = 10;
            this.mrGroup3.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup3.ShadowControl = false;
            this.mrGroup3.ShadowThickness = 3;
            this.mrGroup3.Size = new System.Drawing.Size(169, 131);
            this.mrGroup3.TabIndex = 0;
            // 
            // ChkCostRatio
            // 
            this.ChkCostRatio.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkCostRatio.Location = new System.Drawing.Point(12, 102);
            this.ChkCostRatio.Name = "ChkCostRatio";
            this.ChkCostRatio.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkCostRatio.Size = new System.Drawing.Size(118, 24);
            this.ChkCostRatio.TabIndex = 13;
            this.ChkCostRatio.Text = "Cost Ratio";
            this.ChkCostRatio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkCostRatio.UseVisualStyleBackColor = true;
            // 
            // FrmBillOfMaterialsReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 250);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmBillOfMaterialsReports";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bill Of Materials Reports";
            this.Load += new System.EventHandler(this.FrmBillOfMaterialsReports_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmBillOfMaterialsReports_KeyPress);
            this.mrGroup5.ResumeLayout(false);
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.mrGroup3.ResumeLayout(false);
            this.mrGroup3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RadioButton rChkGroupWise;
        private System.Windows.Forms.RadioButton rChkProductWise;
        private System.Windows.Forms.RadioButton rChkSubGroupWise;
        private MrMaskedTextBox MskFrom;
        private MrGroup mrGroup5;
        private ClsSeparator clsSeparator1;
        private System.Windows.Forms.CheckBox ChkSummary;
        private DevExpress.XtraEditors.SimpleButton BtnShow;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private System.Windows.Forms.CheckBox ChkSelectAll;
        private System.Windows.Forms.ComboBox CmbDateType;
        private MrMaskedTextBox MskToDate;
        private MrGroup mrGroup1;
        private System.Windows.Forms.Panel panel1;
        private MrGroup mrGroup3;
        private System.Windows.Forms.CheckBox ChkIncludeDepartment;
        private System.Windows.Forms.CheckBox ChkIncludeCostCenter;
        private System.Windows.Forms.CheckBox ChkDate;
        private System.Windows.Forms.CheckBox ChkDynamicReports;
        private System.Windows.Forms.CheckBox ChkCostRatio;
    }
}