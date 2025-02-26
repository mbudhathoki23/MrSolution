using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Inventory_Report.Analysis
{
    partial class FrmLandedCostRegister
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
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSysDateType = new System.Windows.Forms.ComboBox();
            this.msk_ToDate = new MrMaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.msk_FromDate = new MrMaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Find = new MrTextBox();
            this.lbl_Find = new System.Windows.Forms.Label();
            this.lbl_Branch = new System.Windows.Forms.Label();
            this.cmb_Branch = new System.Windows.Forms.ComboBox();
            this.chk_Date = new System.Windows.Forms.CheckBox();
            this.chk_Remarks = new System.Windows.Forms.CheckBox();
            this.cmb_GroupBy = new System.Windows.Forms.ComboBox();
            this.lbl_GroupBy = new System.Windows.Forms.Label();
            this.cmb_Currency = new System.Windows.Forms.ComboBox();
            this.lbl_Currency = new System.Windows.Forms.Label();
            this.chk_Summary = new System.Windows.Forms.CheckBox();
            this.chk_SelectAll = new System.Windows.Forms.CheckBox();
            this.btn_Show = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.PanelHeader = new MrPanel();
            this.panel5 = new MrPanel();
            this.panel4 = new MrPanel();
            this.PnlBorderHeaderTop = new MrPanel();
            this.PnlBorderHeaderBottom = new MrPanel();
            this.clsSeparator1 = new ClsSeparator();
            this.clsSeparatorH1 = new ClsSeparatorH();
            this.PanelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 20);
            this.label4.TabIndex = 196;
            this.label4.Text = "Date Type";
            // 
            // cmbSysDateType
            // 
            this.cmbSysDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSysDateType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSysDateType.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSysDateType.FormattingEnabled = true;
            this.cmbSysDateType.Items.AddRange(new object[] {
            "Custom Date",
            "Today",
            "Yesterday",
            "Current Week",
            "Last Week",
            "Current Month",
            "Last Month",
            "Upto Date",
            "Accounting Period"});
            this.cmbSysDateType.Location = new System.Drawing.Point(107, 13);
            this.cmbSysDateType.Name = "cmbSysDateType";
            this.cmbSysDateType.Size = new System.Drawing.Size(201, 28);
            this.cmbSysDateType.TabIndex = 0;
            this.cmbSysDateType.SelectedIndexChanged += new System.EventHandler(this.cmbSysDateType_SelectedIndexChanged);
            // 
            // msk_ToDate
            // 
            this.msk_ToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_ToDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msk_ToDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.msk_ToDate.Location = new System.Drawing.Point(107, 83);
            this.msk_ToDate.Mask = "00/00/0000";
            this.msk_ToDate.Name = "msk_ToDate";
            this.msk_ToDate.Size = new System.Drawing.Size(122, 26);
            this.msk_ToDate.TabIndex = 2;
            this.msk_ToDate.Enter += new System.EventHandler(this.msk_ToDate_Enter);
            this.msk_ToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.msk_ToDate_KeyDown);
            this.msk_ToDate.Leave += new System.EventHandler(this.msk_ToDate_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 195;
            this.label2.Text = "To Date";
            // 
            // msk_FromDate
            // 
            this.msk_FromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_FromDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msk_FromDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.msk_FromDate.Location = new System.Drawing.Point(107, 49);
            this.msk_FromDate.Mask = "00/00/0000";
            this.msk_FromDate.Name = "msk_FromDate";
            this.msk_FromDate.Size = new System.Drawing.Size(122, 26);
            this.msk_FromDate.TabIndex = 1;
            this.msk_FromDate.Enter += new System.EventHandler(this.msk_FromDate_Enter);
            this.msk_FromDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.msk_FromDate_KeyDown);
            this.msk_FromDate.Leave += new System.EventHandler(this.msk_FromDate_Leave);
            this.msk_FromDate.Validated += new System.EventHandler(this.msk_FromDate_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 20);
            this.label1.TabIndex = 194;
            this.label1.Text = "From Date";
            // 
            // txt_Find
            // 
            this.txt_Find.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Find.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Find.Location = new System.Drawing.Point(414, 147);
            this.txt_Find.Name = "txt_Find";
            this.txt_Find.Size = new System.Drawing.Size(161, 26);
            this.txt_Find.TabIndex = 10;
            this.txt_Find.Enter += new System.EventHandler(this.txt_Find_Enter);
            this.txt_Find.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Find_KeyDown);
            this.txt_Find.Leave += new System.EventHandler(this.txt_Find_Leave);
            // 
            // lbl_Find
            // 
            this.lbl_Find.AutoSize = true;
            this.lbl_Find.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Find.Location = new System.Drawing.Point(334, 150);
            this.lbl_Find.Name = "lbl_Find";
            this.lbl_Find.Size = new System.Drawing.Size(64, 20);
            this.lbl_Find.TabIndex = 77;
            this.lbl_Find.Text = "Search";
            // 
            // lbl_Branch
            // 
            this.lbl_Branch.AutoSize = true;
            this.lbl_Branch.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Branch.Location = new System.Drawing.Point(9, 157);
            this.lbl_Branch.Name = "lbl_Branch";
            this.lbl_Branch.Size = new System.Drawing.Size(72, 20);
            this.lbl_Branch.TabIndex = 75;
            this.lbl_Branch.Text = "Branch ";
            // 
            // cmb_Branch
            // 
            this.cmb_Branch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Branch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_Branch.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_Branch.FormattingEnabled = true;
            this.cmb_Branch.Location = new System.Drawing.Point(107, 153);
            this.cmb_Branch.Name = "cmb_Branch";
            this.cmb_Branch.Size = new System.Drawing.Size(201, 28);
            this.cmb_Branch.TabIndex = 4;
            // 
            // chk_Date
            // 
            this.chk_Date.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_Date.Location = new System.Drawing.Point(334, 77);
            this.chk_Date.Name = "chk_Date";
            this.chk_Date.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Date.Size = new System.Drawing.Size(241, 32);
            this.chk_Date.TabIndex = 8;
            this.chk_Date.Text = "Date";
            this.chk_Date.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Date.UseVisualStyleBackColor = true;
            // 
            // chk_Remarks
            // 
            this.chk_Remarks.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_Remarks.Location = new System.Drawing.Point(334, 45);
            this.chk_Remarks.Name = "chk_Remarks";
            this.chk_Remarks.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Remarks.Size = new System.Drawing.Size(241, 32);
            this.chk_Remarks.TabIndex = 7;
            this.chk_Remarks.Text = "Default Unit";
            this.chk_Remarks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Remarks.UseVisualStyleBackColor = true;
            // 
            // cmb_GroupBy
            // 
            this.cmb_GroupBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_GroupBy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_GroupBy.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_GroupBy.FormattingEnabled = true;
            this.cmb_GroupBy.Items.AddRange(new object[] {
            "Date ",
            "Product",
            "Party"});
            this.cmb_GroupBy.Location = new System.Drawing.Point(107, 117);
            this.cmb_GroupBy.Name = "cmb_GroupBy";
            this.cmb_GroupBy.Size = new System.Drawing.Size(201, 28);
            this.cmb_GroupBy.TabIndex = 3;
            this.cmb_GroupBy.SelectedIndexChanged += new System.EventHandler(this.cmb_GroupBy_SelectedIndexChanged);
            // 
            // lbl_GroupBy
            // 
            this.lbl_GroupBy.AutoSize = true;
            this.lbl_GroupBy.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_GroupBy.Location = new System.Drawing.Point(9, 121);
            this.lbl_GroupBy.Name = "lbl_GroupBy";
            this.lbl_GroupBy.Size = new System.Drawing.Size(85, 20);
            this.lbl_GroupBy.TabIndex = 64;
            this.lbl_GroupBy.Text = "Group By";
            // 
            // cmb_Currency
            // 
            this.cmb_Currency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Currency.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmb_Currency.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_Currency.FormattingEnabled = true;
            this.cmb_Currency.Items.AddRange(new object[] {
            "Local",
            "Foreign",
            "Both"});
            this.cmb_Currency.Location = new System.Drawing.Point(107, 189);
            this.cmb_Currency.Name = "cmb_Currency";
            this.cmb_Currency.Size = new System.Drawing.Size(200, 28);
            this.cmb_Currency.TabIndex = 5;
            // 
            // lbl_Currency
            // 
            this.lbl_Currency.AutoSize = true;
            this.lbl_Currency.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Currency.Location = new System.Drawing.Point(9, 193);
            this.lbl_Currency.Name = "lbl_Currency";
            this.lbl_Currency.Size = new System.Drawing.Size(83, 20);
            this.lbl_Currency.TabIndex = 62;
            this.lbl_Currency.Text = "Currency";
            // 
            // chk_Summary
            // 
            this.chk_Summary.Checked = true;
            this.chk_Summary.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Summary.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_Summary.Location = new System.Drawing.Point(334, 13);
            this.chk_Summary.Name = "chk_Summary";
            this.chk_Summary.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Summary.Size = new System.Drawing.Size(241, 32);
            this.chk_Summary.TabIndex = 6;
            this.chk_Summary.Text = "Summary";
            this.chk_Summary.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Summary.UseVisualStyleBackColor = true;
            // 
            // chk_SelectAll
            // 
            this.chk_SelectAll.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_SelectAll.Location = new System.Drawing.Point(334, 109);
            this.chk_SelectAll.Name = "chk_SelectAll";
            this.chk_SelectAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_SelectAll.Size = new System.Drawing.Size(241, 32);
            this.chk_SelectAll.TabIndex = 9;
            this.chk_SelectAll.Text = "Select All";
            this.chk_SelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_SelectAll.UseVisualStyleBackColor = true;
            // 
            // btn_Show
            // 
            this.btn_Show.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.btn_Show.Appearance.Options.UseFont = true;
            this.btn_Show.Location = new System.Drawing.Point(312, 231);
            this.btn_Show.Name = "btn_Show";
            this.btn_Show.Size = new System.Drawing.Size(114, 38);
            this.btn_Show.TabIndex = 11;
            this.btn_Show.Text = "&SHOW";
            this.btn_Show.Click += new System.EventHandler(this.btn_Show_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Location = new System.Drawing.Point(426, 231);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(125, 38);
            this.simpleButton2.TabIndex = 12;
            this.simpleButton2.Text = "&CANCEL";
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelHeader.Controls.Add(this.clsSeparatorH1);
            this.PanelHeader.Controls.Add(this.clsSeparator1);
            this.PanelHeader.Controls.Add(this.btn_Show);
            this.PanelHeader.Controls.Add(this.simpleButton2);
            this.PanelHeader.Controls.Add(this.label4);
            this.PanelHeader.Controls.Add(this.cmbSysDateType);
            this.PanelHeader.Controls.Add(this.panel5);
            this.PanelHeader.Controls.Add(this.msk_ToDate);
            this.PanelHeader.Controls.Add(this.panel4);
            this.PanelHeader.Controls.Add(this.label2);
            this.PanelHeader.Controls.Add(this.PnlBorderHeaderTop);
            this.PanelHeader.Controls.Add(this.msk_FromDate);
            this.PanelHeader.Controls.Add(this.PnlBorderHeaderBottom);
            this.PanelHeader.Controls.Add(this.label1);
            this.PanelHeader.Controls.Add(this.chk_SelectAll);
            this.PanelHeader.Controls.Add(this.txt_Find);
            this.PanelHeader.Controls.Add(this.chk_Summary);
            this.PanelHeader.Controls.Add(this.lbl_Find);
            this.PanelHeader.Controls.Add(this.lbl_Currency);
            this.PanelHeader.Controls.Add(this.lbl_Branch);
            this.PanelHeader.Controls.Add(this.cmb_Currency);
            this.PanelHeader.Controls.Add(this.cmb_Branch);
            this.PanelHeader.Controls.Add(this.lbl_GroupBy);
            this.PanelHeader.Controls.Add(this.chk_Date);
            this.PanelHeader.Controls.Add(this.cmb_GroupBy);
            this.PanelHeader.Controls.Add(this.chk_Remarks);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(594, 279);
            this.PanelHeader.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(3, 271);
            this.panel5.TabIndex = 10;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(589, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(3, 271);
            this.panel4.TabIndex = 9;
            // 
            // PnlBorderHeaderTop
            // 
            this.PnlBorderHeaderTop.BackColor = System.Drawing.Color.White;
            this.PnlBorderHeaderTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlBorderHeaderTop.Location = new System.Drawing.Point(0, 0);
            this.PnlBorderHeaderTop.Name = "PnlBorderHeaderTop";
            this.PnlBorderHeaderTop.Size = new System.Drawing.Size(592, 3);
            this.PnlBorderHeaderTop.TabIndex = 8;
            // 
            // PnlBorderHeaderBottom
            // 
            this.PnlBorderHeaderBottom.BackColor = System.Drawing.Color.White;
            this.PnlBorderHeaderBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PnlBorderHeaderBottom.Location = new System.Drawing.Point(0, 274);
            this.PnlBorderHeaderBottom.Name = "PnlBorderHeaderBottom";
            this.PnlBorderHeaderBottom.Size = new System.Drawing.Size(592, 3);
            this.PnlBorderHeaderBottom.TabIndex = 0;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.Lavender;
            this.clsSeparator1.Location = new System.Drawing.Point(13, 224);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(694, 3);
            this.clsSeparator1.TabIndex = 197;
            this.clsSeparator1.TabStop = false;
            // 
            // clsSeparatorH1
            // 
            this.clsSeparatorH1.Location = new System.Drawing.Point(328, 9);
            this.clsSeparatorH1.Name = "clsSeparatorH1";
            this.clsSeparatorH1.Size = new System.Drawing.Size(3, 215);
            this.clsSeparatorH1.TabIndex = 198;
            this.clsSeparatorH1.TabStop = false;
            // 
            // FrmLandedCostRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(594, 279);
            this.Controls.Add(this.PanelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmLandedCostRegister";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Landed Cost Register";
            this.Load += new System.EventHandler(this.LandedCostRegister_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LandedCostRegister_KeyPress);
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl_Branch;
        private System.Windows.Forms.ComboBox cmb_Branch;
        private System.Windows.Forms.CheckBox chk_Date;
        private System.Windows.Forms.CheckBox chk_Remarks;
        private System.Windows.Forms.ComboBox cmb_GroupBy;
        private System.Windows.Forms.Label lbl_GroupBy;
        private System.Windows.Forms.ComboBox cmb_Currency;
        private System.Windows.Forms.Label lbl_Currency;
        private System.Windows.Forms.CheckBox chk_Summary;
        private System.Windows.Forms.CheckBox chk_SelectAll;
        private System.Windows.Forms.TextBox txt_Find;
        private System.Windows.Forms.Label lbl_Find;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSysDateType;
        private System.Windows.Forms.MaskedTextBox msk_ToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox msk_FromDate;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btn_Show;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private System.Windows.Forms.Panel PanelHeader;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel PnlBorderHeaderTop;
        private System.Windows.Forms.Panel PnlBorderHeaderBottom;
        private ClsSeparator clsSeparator1;
        private ClsSeparatorH clsSeparatorH1;
    }
}