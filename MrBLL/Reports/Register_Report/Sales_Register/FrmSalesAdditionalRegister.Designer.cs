using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Register_Report.Sales_Register
{
    partial class FrmSalesAdditionalRegister
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
            this.panel15 = new MrPanel();
            this.panel13 = new MrPanel();
            this.panel14 = new MrPanel();
            this.panel16 = new MrPanel();
            this.StorePanel = new MrPanel();
            this.roundPanel1 = new RoundPanel();
            this.CmbDateType = new System.Windows.Forms.ComboBox();
            this.MskFrom = new MrMaskedTextBox();
            this.MskToDate = new MrMaskedTextBox();
            this.roundPanel5 = new RoundPanel();
            this.TxtVoucherNo = new MrTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.roundPanel4 = new RoundPanel();
            this.rChkOtherSales = new System.Windows.Forms.RadioButton();
            this.rChkCardSales = new System.Windows.Forms.RadioButton();
            this.rChkCashSales = new System.Windows.Forms.RadioButton();
            this.rChkCreditSales = new System.Windows.Forms.RadioButton();
            this.rChkAllType = new System.Windows.Forms.RadioButton();
            this.roundPanel3 = new RoundPanel();
            this.ChkDocAgent = new System.Windows.Forms.CheckBox();
            this.ChkSummary = new System.Windows.Forms.CheckBox();
            this.ChkAdditionalTerm = new System.Windows.Forms.CheckBox();
            this.ChkFreeQty = new System.Windows.Forms.CheckBox();
            this.ChkHorizontal = new System.Windows.Forms.CheckBox();
            this.ChkIncludeAltQty = new System.Windows.Forms.CheckBox();
            this.ChkIncludeOrderNo = new System.Windows.Forms.CheckBox();
            this.ChkIncludeChallanNo = new System.Windows.Forms.CheckBox();
            this.ChkIncludeBatch = new System.Windows.Forms.CheckBox();
            this.ChkIncludeGodown = new System.Windows.Forms.CheckBox();
            this.ChkDateWise = new System.Windows.Forms.CheckBox();
            this.ChkIncludeRemarks = new System.Windows.Forms.CheckBox();
            this.roundPanel2 = new RoundPanel();
            this.rChkDepartment = new System.Windows.Forms.RadioButton();
            this.rChkUserWise = new System.Windows.Forms.RadioButton();
            this.rChkCounter = new System.Windows.Forms.RadioButton();
            this.rChkProductSubGroup = new System.Windows.Forms.RadioButton();
            this.rChkProductGroup = new System.Windows.Forms.RadioButton();
            this.rChkProduct = new System.Windows.Forms.RadioButton();
            this.rChkArea = new System.Windows.Forms.RadioButton();
            this.rChkAgent = new System.Windows.Forms.RadioButton();
            this.rChkCustomer = new System.Windows.Forms.RadioButton();
            this.rChkInvoice = new System.Windows.Forms.RadioButton();
            this.rChkDate = new System.Windows.Forms.RadioButton();
            this.ChkDynamicReport = new System.Windows.Forms.CheckBox();
            this.roundPanel6 = new RoundPanel();
            this.BtnShow = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.ChkSelectAll = new System.Windows.Forms.CheckBox();
            this.StorePanel.SuspendLayout();
            this.roundPanel1.SuspendLayout();
            this.roundPanel5.SuspendLayout();
            this.roundPanel4.SuspendLayout();
            this.roundPanel3.SuspendLayout();
            this.roundPanel2.SuspendLayout();
            this.roundPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.Color.White;
            this.panel15.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel15.Location = new System.Drawing.Point(0, 0);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(593, 3);
            this.panel15.TabIndex = 2;
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.Color.White;
            this.panel13.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel13.Location = new System.Drawing.Point(0, 3);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(3, 425);
            this.panel13.TabIndex = 4;
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.White;
            this.panel14.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel14.Location = new System.Drawing.Point(590, 3);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(3, 425);
            this.panel14.TabIndex = 9;
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.White;
            this.panel16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel16.Location = new System.Drawing.Point(0, 428);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(593, 3);
            this.panel16.TabIndex = 0;
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.roundPanel5);
            this.StorePanel.Controls.Add(this.roundPanel4);
            this.StorePanel.Controls.Add(this.roundPanel3);
            this.StorePanel.Controls.Add(this.roundPanel2);
            this.StorePanel.Controls.Add(this.roundPanel6);
            this.StorePanel.Controls.Add(this.roundPanel1);
            this.StorePanel.Controls.Add(this.panel13);
            this.StorePanel.Controls.Add(this.panel14);
            this.StorePanel.Controls.Add(this.panel15);
            this.StorePanel.Controls.Add(this.panel16);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(593, 431);
            this.StorePanel.TabIndex = 12;
            // 
            // roundPanel1
            // 
            this.roundPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.roundPanel1.Controls.Add(this.CmbDateType);
            this.roundPanel1.Controls.Add(this.MskFrom);
            this.roundPanel1.Controls.Add(this.MskToDate);
            this.roundPanel1.Location = new System.Drawing.Point(7, 7);
            this.roundPanel1.Name = "roundPanel1";
            this.roundPanel1.Radious = 25;
            this.roundPanel1.Size = new System.Drawing.Size(572, 64);
            this.roundPanel1.TabIndex = 10;
            this.roundPanel1.TabStop = false;
            this.roundPanel1.Text = "DATE TYPE";
            this.roundPanel1.TitleBackColor = System.Drawing.Color.DarkSlateBlue;
            this.roundPanel1.TitleFont = new System.Drawing.Font("Bookman Old Style", 11F);
            this.roundPanel1.TitleForeColor = System.Drawing.Color.White;
            this.roundPanel1.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
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
            this.CmbDateType.Location = new System.Drawing.Point(5, 29);
            this.CmbDateType.Name = "CmbDateType";
            this.CmbDateType.Size = new System.Drawing.Size(258, 27);
            this.CmbDateType.TabIndex = 0;
            // 
            // MskFrom
            // 
            this.MskFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskFrom.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskFrom.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskFrom.Location = new System.Drawing.Point(269, 31);
            this.MskFrom.Mask = "00/00/0000";
            this.MskFrom.Name = "MskFrom";
            this.MskFrom.Size = new System.Drawing.Size(129, 25);
            this.MskFrom.TabIndex = 1;
            this.MskFrom.ValidatingType = typeof(System.DateTime);
            // 
            // MskToDate
            // 
            this.MskToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskToDate.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskToDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskToDate.Location = new System.Drawing.Point(404, 31);
            this.MskToDate.Mask = "00/00/0000";
            this.MskToDate.Name = "MskToDate";
            this.MskToDate.Size = new System.Drawing.Size(129, 25);
            this.MskToDate.TabIndex = 2;
            // 
            // roundPanel5
            // 
            this.roundPanel5.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.roundPanel5.Controls.Add(this.TxtVoucherNo);
            this.roundPanel5.Controls.Add(this.label1);
            this.roundPanel5.Location = new System.Drawing.Point(228, 306);
            this.roundPanel5.Name = "roundPanel5";
            this.roundPanel5.Radious = 25;
            this.roundPanel5.Size = new System.Drawing.Size(353, 63);
            this.roundPanel5.TabIndex = 14;
            this.roundPanel5.TabStop = false;
            this.roundPanel5.Text = "Filter Invoice";
            this.roundPanel5.TitleBackColor = System.Drawing.Color.DarkSlateBlue;
            this.roundPanel5.TitleFont = new System.Drawing.Font("Bookman Old Style", 12F);
            this.roundPanel5.TitleForeColor = System.Drawing.Color.White;
            this.roundPanel5.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
            // 
            // TxtVoucherNo
            // 
            this.TxtVoucherNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtVoucherNo.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtVoucherNo.Location = new System.Drawing.Point(126, 28);
            this.TxtVoucherNo.Name = "TxtVoucherNo";
            this.TxtVoucherNo.Size = new System.Drawing.Size(185, 25);
            this.TxtVoucherNo.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 19);
            this.label1.TabIndex = 23;
            this.label1.Text = "Find Invoice";
            // 
            // roundPanel4
            // 
            this.roundPanel4.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.roundPanel4.Controls.Add(this.rChkOtherSales);
            this.roundPanel4.Controls.Add(this.rChkCardSales);
            this.roundPanel4.Controls.Add(this.rChkCashSales);
            this.roundPanel4.Controls.Add(this.rChkCreditSales);
            this.roundPanel4.Controls.Add(this.rChkAllType);
            this.roundPanel4.Location = new System.Drawing.Point(228, 249);
            this.roundPanel4.Name = "roundPanel4";
            this.roundPanel4.Radious = 25;
            this.roundPanel4.Size = new System.Drawing.Size(353, 54);
            this.roundPanel4.TabIndex = 13;
            this.roundPanel4.TabStop = false;
            this.roundPanel4.Text = "Invoice Type";
            this.roundPanel4.TitleBackColor = System.Drawing.Color.DarkSlateBlue;
            this.roundPanel4.TitleFont = new System.Drawing.Font("Bookman Old Style", 12F);
            this.roundPanel4.TitleForeColor = System.Drawing.Color.White;
            this.roundPanel4.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
            // 
            // rChkOtherSales
            // 
            this.rChkOtherSales.AutoSize = true;
            this.rChkOtherSales.Location = new System.Drawing.Point(255, 30);
            this.rChkOtherSales.Name = "rChkOtherSales";
            this.rChkOtherSales.Size = new System.Drawing.Size(71, 23);
            this.rChkOtherSales.TabIndex = 4;
            this.rChkOtherSales.Text = "Other";
            this.rChkOtherSales.UseVisualStyleBackColor = true;
            // 
            // rChkCardSales
            // 
            this.rChkCardSales.AutoSize = true;
            this.rChkCardSales.Location = new System.Drawing.Point(192, 30);
            this.rChkCardSales.Name = "rChkCardSales";
            this.rChkCardSales.Size = new System.Drawing.Size(63, 23);
            this.rChkCardSales.TabIndex = 3;
            this.rChkCardSales.Text = "Card";
            this.rChkCardSales.UseVisualStyleBackColor = true;
            // 
            // rChkCashSales
            // 
            this.rChkCashSales.AutoSize = true;
            this.rChkCashSales.Location = new System.Drawing.Point(127, 30);
            this.rChkCashSales.Name = "rChkCashSales";
            this.rChkCashSales.Size = new System.Drawing.Size(65, 23);
            this.rChkCashSales.TabIndex = 2;
            this.rChkCashSales.Text = "Cash";
            this.rChkCashSales.UseVisualStyleBackColor = true;
            // 
            // rChkCreditSales
            // 
            this.rChkCreditSales.AutoSize = true;
            this.rChkCreditSales.Location = new System.Drawing.Point(53, 30);
            this.rChkCreditSales.Name = "rChkCreditSales";
            this.rChkCreditSales.Size = new System.Drawing.Size(74, 23);
            this.rChkCreditSales.TabIndex = 1;
            this.rChkCreditSales.Text = "Credit";
            this.rChkCreditSales.UseVisualStyleBackColor = true;
            // 
            // rChkAllType
            // 
            this.rChkAllType.AutoSize = true;
            this.rChkAllType.Checked = true;
            this.rChkAllType.Location = new System.Drawing.Point(6, 30);
            this.rChkAllType.Name = "rChkAllType";
            this.rChkAllType.Size = new System.Drawing.Size(47, 23);
            this.rChkAllType.TabIndex = 0;
            this.rChkAllType.TabStop = true;
            this.rChkAllType.Text = "All";
            this.rChkAllType.UseVisualStyleBackColor = true;
            // 
            // roundPanel3
            // 
            this.roundPanel3.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.roundPanel3.Controls.Add(this.ChkDocAgent);
            this.roundPanel3.Controls.Add(this.ChkSummary);
            this.roundPanel3.Controls.Add(this.ChkAdditionalTerm);
            this.roundPanel3.Controls.Add(this.ChkFreeQty);
            this.roundPanel3.Controls.Add(this.ChkHorizontal);
            this.roundPanel3.Controls.Add(this.ChkIncludeAltQty);
            this.roundPanel3.Controls.Add(this.ChkIncludeOrderNo);
            this.roundPanel3.Controls.Add(this.ChkIncludeChallanNo);
            this.roundPanel3.Controls.Add(this.ChkIncludeBatch);
            this.roundPanel3.Controls.Add(this.ChkIncludeGodown);
            this.roundPanel3.Controls.Add(this.ChkDateWise);
            this.roundPanel3.Controls.Add(this.ChkIncludeRemarks);
            this.roundPanel3.Location = new System.Drawing.Point(228, 77);
            this.roundPanel3.Name = "roundPanel3";
            this.roundPanel3.Radious = 25;
            this.roundPanel3.Size = new System.Drawing.Size(353, 169);
            this.roundPanel3.TabIndex = 12;
            this.roundPanel3.TabStop = false;
            this.roundPanel3.Text = "Invoice Details";
            this.roundPanel3.TitleBackColor = System.Drawing.Color.DarkSlateBlue;
            this.roundPanel3.TitleFont = new System.Drawing.Font("Bookman Old Style", 12F);
            this.roundPanel3.TitleForeColor = System.Drawing.Color.White;
            this.roundPanel3.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
            // 
            // ChkDocAgent
            // 
            this.ChkDocAgent.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDocAgent.Location = new System.Drawing.Point(178, 143);
            this.ChkDocAgent.Name = "ChkDocAgent";
            this.ChkDocAgent.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDocAgent.Size = new System.Drawing.Size(162, 23);
            this.ChkDocAgent.TabIndex = 11;
            this.ChkDocAgent.Text = "DocAgent";
            this.ChkDocAgent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDocAgent.UseVisualStyleBackColor = true;
            // 
            // ChkSummary
            // 
            this.ChkSummary.Checked = true;
            this.ChkSummary.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkSummary.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSummary.Location = new System.Drawing.Point(6, 28);
            this.ChkSummary.Name = "ChkSummary";
            this.ChkSummary.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSummary.Size = new System.Drawing.Size(165, 23);
            this.ChkSummary.TabIndex = 0;
            this.ChkSummary.Text = "Summary";
            this.ChkSummary.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSummary.UseVisualStyleBackColor = true;
            // 
            // ChkAdditionalTerm
            // 
            this.ChkAdditionalTerm.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAdditionalTerm.Location = new System.Drawing.Point(178, 28);
            this.ChkAdditionalTerm.Name = "ChkAdditionalTerm";
            this.ChkAdditionalTerm.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkAdditionalTerm.Size = new System.Drawing.Size(162, 23);
            this.ChkAdditionalTerm.TabIndex = 6;
            this.ChkAdditionalTerm.Text = "Additional Term";
            this.ChkAdditionalTerm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkAdditionalTerm.UseVisualStyleBackColor = true;
            // 
            // ChkFreeQty
            // 
            this.ChkFreeQty.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkFreeQty.Location = new System.Drawing.Point(6, 142);
            this.ChkFreeQty.Name = "ChkFreeQty";
            this.ChkFreeQty.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkFreeQty.Size = new System.Drawing.Size(165, 23);
            this.ChkFreeQty.TabIndex = 5;
            this.ChkFreeQty.Text = "Free Qty";
            this.ChkFreeQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkFreeQty.UseVisualStyleBackColor = true;
            // 
            // ChkHorizontal
            // 
            this.ChkHorizontal.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkHorizontal.Location = new System.Drawing.Point(178, 51);
            this.ChkHorizontal.Name = "ChkHorizontal";
            this.ChkHorizontal.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkHorizontal.Size = new System.Drawing.Size(162, 23);
            this.ChkHorizontal.TabIndex = 7;
            this.ChkHorizontal.Text = "Horizontal";
            this.ChkHorizontal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkHorizontal.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeAltQty
            // 
            this.ChkIncludeAltQty.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeAltQty.Location = new System.Drawing.Point(6, 120);
            this.ChkIncludeAltQty.Name = "ChkIncludeAltQty";
            this.ChkIncludeAltQty.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeAltQty.Size = new System.Drawing.Size(165, 23);
            this.ChkIncludeAltQty.TabIndex = 4;
            this.ChkIncludeAltQty.Text = "Alt Qty";
            this.ChkIncludeAltQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeAltQty.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeOrderNo
            // 
            this.ChkIncludeOrderNo.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeOrderNo.Location = new System.Drawing.Point(178, 74);
            this.ChkIncludeOrderNo.Name = "ChkIncludeOrderNo";
            this.ChkIncludeOrderNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeOrderNo.Size = new System.Drawing.Size(162, 23);
            this.ChkIncludeOrderNo.TabIndex = 8;
            this.ChkIncludeOrderNo.Text = "Order No";
            this.ChkIncludeOrderNo.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.ChkIncludeOrderNo.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeChallanNo
            // 
            this.ChkIncludeChallanNo.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeChallanNo.Location = new System.Drawing.Point(178, 97);
            this.ChkIncludeChallanNo.Name = "ChkIncludeChallanNo";
            this.ChkIncludeChallanNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeChallanNo.Size = new System.Drawing.Size(162, 23);
            this.ChkIncludeChallanNo.TabIndex = 9;
            this.ChkIncludeChallanNo.Text = "Challan No";
            this.ChkIncludeChallanNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeChallanNo.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeBatch
            // 
            this.ChkIncludeBatch.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeBatch.Location = new System.Drawing.Point(178, 120);
            this.ChkIncludeBatch.Name = "ChkIncludeBatch";
            this.ChkIncludeBatch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeBatch.Size = new System.Drawing.Size(162, 23);
            this.ChkIncludeBatch.TabIndex = 10;
            this.ChkIncludeBatch.Text = "Batch";
            this.ChkIncludeBatch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeBatch.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeGodown
            // 
            this.ChkIncludeGodown.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeGodown.Location = new System.Drawing.Point(6, 51);
            this.ChkIncludeGodown.Name = "ChkIncludeGodown";
            this.ChkIncludeGodown.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeGodown.Size = new System.Drawing.Size(165, 23);
            this.ChkIncludeGodown.TabIndex = 1;
            this.ChkIncludeGodown.Text = "Godown";
            this.ChkIncludeGodown.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeGodown.UseVisualStyleBackColor = true;
            // 
            // ChkDateWise
            // 
            this.ChkDateWise.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDateWise.Location = new System.Drawing.Point(6, 74);
            this.ChkDateWise.Name = "ChkDateWise";
            this.ChkDateWise.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDateWise.Size = new System.Drawing.Size(165, 23);
            this.ChkDateWise.TabIndex = 2;
            this.ChkDateWise.Text = "Date";
            this.ChkDateWise.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDateWise.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeRemarks
            // 
            this.ChkIncludeRemarks.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeRemarks.Location = new System.Drawing.Point(6, 97);
            this.ChkIncludeRemarks.Name = "ChkIncludeRemarks";
            this.ChkIncludeRemarks.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeRemarks.Size = new System.Drawing.Size(165, 23);
            this.ChkIncludeRemarks.TabIndex = 3;
            this.ChkIncludeRemarks.Text = "Remarks";
            this.ChkIncludeRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeRemarks.UseVisualStyleBackColor = true;
            // 
            // roundPanel2
            // 
            this.roundPanel2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.roundPanel2.Controls.Add(this.rChkDepartment);
            this.roundPanel2.Controls.Add(this.rChkUserWise);
            this.roundPanel2.Controls.Add(this.rChkCounter);
            this.roundPanel2.Controls.Add(this.rChkProductSubGroup);
            this.roundPanel2.Controls.Add(this.rChkProductGroup);
            this.roundPanel2.Controls.Add(this.rChkProduct);
            this.roundPanel2.Controls.Add(this.rChkArea);
            this.roundPanel2.Controls.Add(this.rChkAgent);
            this.roundPanel2.Controls.Add(this.rChkCustomer);
            this.roundPanel2.Controls.Add(this.rChkInvoice);
            this.roundPanel2.Controls.Add(this.rChkDate);
            this.roundPanel2.Location = new System.Drawing.Point(9, 77);
            this.roundPanel2.Name = "roundPanel2";
            this.roundPanel2.Radious = 25;
            this.roundPanel2.Size = new System.Drawing.Size(213, 292);
            this.roundPanel2.TabIndex = 11;
            this.roundPanel2.TabStop = false;
            this.roundPanel2.Text = "REPORT TYPES";
            this.roundPanel2.TitleBackColor = System.Drawing.Color.DarkSlateBlue;
            this.roundPanel2.TitleFont = new System.Drawing.Font("Bookman Old Style", 11F);
            this.roundPanel2.TitleForeColor = System.Drawing.Color.White;
            this.roundPanel2.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
            // 
            // rChkDepartment
            // 
            this.rChkDepartment.AutoSize = true;
            this.rChkDepartment.Location = new System.Drawing.Point(9, 260);
            this.rChkDepartment.Name = "rChkDepartment";
            this.rChkDepartment.Size = new System.Drawing.Size(158, 23);
            this.rChkDepartment.TabIndex = 10;
            this.rChkDepartment.Text = "Department Wise";
            this.rChkDepartment.UseVisualStyleBackColor = true;
            // 
            // rChkUserWise
            // 
            this.rChkUserWise.AutoSize = true;
            this.rChkUserWise.Location = new System.Drawing.Point(9, 237);
            this.rChkUserWise.Name = "rChkUserWise";
            this.rChkUserWise.Size = new System.Drawing.Size(104, 23);
            this.rChkUserWise.TabIndex = 9;
            this.rChkUserWise.Text = "User Wise";
            this.rChkUserWise.UseVisualStyleBackColor = true;
            // 
            // rChkCounter
            // 
            this.rChkCounter.AutoSize = true;
            this.rChkCounter.Location = new System.Drawing.Point(9, 214);
            this.rChkCounter.Name = "rChkCounter";
            this.rChkCounter.Size = new System.Drawing.Size(129, 23);
            this.rChkCounter.TabIndex = 8;
            this.rChkCounter.Text = "Counter Wise";
            this.rChkCounter.UseVisualStyleBackColor = true;
            // 
            // rChkProductSubGroup
            // 
            this.rChkProductSubGroup.AutoSize = true;
            this.rChkProductSubGroup.Location = new System.Drawing.Point(9, 191);
            this.rChkProductSubGroup.Name = "rChkProductSubGroup";
            this.rChkProductSubGroup.Size = new System.Drawing.Size(203, 23);
            this.rChkProductSubGroup.TabIndex = 7;
            this.rChkProductSubGroup.Text = "Product SubGroup Wise";
            this.rChkProductSubGroup.UseVisualStyleBackColor = true;
            // 
            // rChkProductGroup
            // 
            this.rChkProductGroup.AutoSize = true;
            this.rChkProductGroup.Location = new System.Drawing.Point(9, 168);
            this.rChkProductGroup.Name = "rChkProductGroup";
            this.rChkProductGroup.Size = new System.Drawing.Size(175, 23);
            this.rChkProductGroup.TabIndex = 6;
            this.rChkProductGroup.Text = "Product Group Wise";
            this.rChkProductGroup.UseVisualStyleBackColor = true;
            // 
            // rChkProduct
            // 
            this.rChkProduct.AutoSize = true;
            this.rChkProduct.Location = new System.Drawing.Point(9, 145);
            this.rChkProduct.Name = "rChkProduct";
            this.rChkProduct.Size = new System.Drawing.Size(125, 23);
            this.rChkProduct.TabIndex = 5;
            this.rChkProduct.Text = "Product Wise";
            this.rChkProduct.UseVisualStyleBackColor = true;
            // 
            // rChkArea
            // 
            this.rChkArea.AutoSize = true;
            this.rChkArea.Location = new System.Drawing.Point(9, 122);
            this.rChkArea.Name = "rChkArea";
            this.rChkArea.Size = new System.Drawing.Size(103, 23);
            this.rChkArea.TabIndex = 4;
            this.rChkArea.Text = "Area Wise";
            this.rChkArea.UseVisualStyleBackColor = true;
            // 
            // rChkAgent
            // 
            this.rChkAgent.AutoSize = true;
            this.rChkAgent.Location = new System.Drawing.Point(9, 99);
            this.rChkAgent.Name = "rChkAgent";
            this.rChkAgent.Size = new System.Drawing.Size(111, 23);
            this.rChkAgent.TabIndex = 3;
            this.rChkAgent.Text = "Agent Wise";
            this.rChkAgent.UseVisualStyleBackColor = true;
            // 
            // rChkCustomer
            // 
            this.rChkCustomer.AutoSize = true;
            this.rChkCustomer.Location = new System.Drawing.Point(9, 76);
            this.rChkCustomer.Name = "rChkCustomer";
            this.rChkCustomer.Size = new System.Drawing.Size(141, 23);
            this.rChkCustomer.TabIndex = 2;
            this.rChkCustomer.Text = "Customer Wise";
            this.rChkCustomer.UseVisualStyleBackColor = true;
            // 
            // rChkInvoice
            // 
            this.rChkInvoice.AutoSize = true;
            this.rChkInvoice.Location = new System.Drawing.Point(9, 53);
            this.rChkInvoice.Name = "rChkInvoice";
            this.rChkInvoice.Size = new System.Drawing.Size(120, 23);
            this.rChkInvoice.TabIndex = 1;
            this.rChkInvoice.Text = "Invoice Wise";
            this.rChkInvoice.UseVisualStyleBackColor = true;
            // 
            // rChkDate
            // 
            this.rChkDate.AutoSize = true;
            this.rChkDate.Checked = true;
            this.rChkDate.Location = new System.Drawing.Point(9, 30);
            this.rChkDate.Name = "rChkDate";
            this.rChkDate.Size = new System.Drawing.Size(104, 23);
            this.rChkDate.TabIndex = 0;
            this.rChkDate.TabStop = true;
            this.rChkDate.Text = "Date Wise";
            this.rChkDate.UseVisualStyleBackColor = true;
            // 
            // ChkDynamicReport
            // 
            this.ChkDynamicReport.AllowDrop = true;
            this.ChkDynamicReport.Cursor = System.Windows.Forms.Cursors.Default;
            this.ChkDynamicReport.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDynamicReport.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.ChkDynamicReport.Location = new System.Drawing.Point(5, 13);
            this.ChkDynamicReport.Name = "ChkDynamicReport";
            this.ChkDynamicReport.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDynamicReport.Size = new System.Drawing.Size(162, 23);
            this.ChkDynamicReport.TabIndex = 0;
            this.ChkDynamicReport.Text = "Dynamic Report";
            this.ChkDynamicReport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDynamicReport.UseMnemonic = false;
            this.ChkDynamicReport.UseVisualStyleBackColor = true;
            // 
            // roundPanel6
            // 
            this.roundPanel6.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.roundPanel6.Controls.Add(this.BtnShow);
            this.roundPanel6.Controls.Add(this.BtnCancel);
            this.roundPanel6.Controls.Add(this.ChkDynamicReport);
            this.roundPanel6.Controls.Add(this.ChkSelectAll);
            this.roundPanel6.Location = new System.Drawing.Point(9, 370);
            this.roundPanel6.Name = "roundPanel6";
            this.roundPanel6.Radious = 25;
            this.roundPanel6.Size = new System.Drawing.Size(572, 52);
            this.roundPanel6.TabIndex = 15;
            this.roundPanel6.TabStop = false;
            this.roundPanel6.TitleBackColor = System.Drawing.Color.DarkSlateBlue;
            this.roundPanel6.TitleFont = new System.Drawing.Font("Bookman Old Style", 1F);
            this.roundPanel6.TitleForeColor = System.Drawing.Color.White;
            this.roundPanel6.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
            // 
            // BtnShow
            // 
            this.BtnShow.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnShow.Appearance.Options.UseFont = true;
            this.BtnShow.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.BtnShow.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnShow.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.BtnShow.Location = new System.Drawing.Point(355, 11);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(95, 37);
            this.BtnShow.TabIndex = 2;
            this.BtnShow.Text = "&SHOW";
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.BtnCancel.Location = new System.Drawing.Point(452, 11);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(112, 37);
            this.BtnCancel.TabIndex = 3;
            this.BtnCancel.Text = "&CANCEL";
            // 
            // ChkSelectAll
            // 
            this.ChkSelectAll.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSelectAll.Location = new System.Drawing.Point(173, 13);
            this.ChkSelectAll.Name = "ChkSelectAll";
            this.ChkSelectAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSelectAll.Size = new System.Drawing.Size(152, 23);
            this.ChkSelectAll.TabIndex = 1;
            this.ChkSelectAll.Text = "Select All";
            this.ChkSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSelectAll.UseVisualStyleBackColor = true;
            // 
            // FrmSalesAdditionalRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 431);
            this.Controls.Add(this.StorePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmSalesAdditionalRegister";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sales Additional Register";
            this.Load += new System.EventHandler(this.FrmSalesAdditionalRegister_Load);
            this.StorePanel.ResumeLayout(false);
            this.roundPanel1.ResumeLayout(false);
            this.roundPanel1.PerformLayout();
            this.roundPanel5.ResumeLayout(false);
            this.roundPanel5.PerformLayout();
            this.roundPanel4.ResumeLayout(false);
            this.roundPanel4.PerformLayout();
            this.roundPanel3.ResumeLayout(false);
            this.roundPanel2.ResumeLayout(false);
            this.roundPanel2.PerformLayout();
            this.roundPanel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Panel StorePanel;
        private RoundPanel roundPanel1;
        private System.Windows.Forms.ComboBox CmbDateType;
        private System.Windows.Forms.MaskedTextBox MskFrom;
        private System.Windows.Forms.MaskedTextBox MskToDate;
        private RoundPanel roundPanel5;
        private System.Windows.Forms.TextBox TxtVoucherNo;
        private System.Windows.Forms.Label label1;
        private RoundPanel roundPanel4;
        private System.Windows.Forms.RadioButton rChkOtherSales;
        private System.Windows.Forms.RadioButton rChkCardSales;
        private System.Windows.Forms.RadioButton rChkCashSales;
        private System.Windows.Forms.RadioButton rChkCreditSales;
        private System.Windows.Forms.RadioButton rChkAllType;
        private RoundPanel roundPanel3;
        private System.Windows.Forms.CheckBox ChkDocAgent;
        private System.Windows.Forms.CheckBox ChkSummary;
        private System.Windows.Forms.CheckBox ChkAdditionalTerm;
        private System.Windows.Forms.CheckBox ChkFreeQty;
        private System.Windows.Forms.CheckBox ChkHorizontal;
        private System.Windows.Forms.CheckBox ChkIncludeAltQty;
        private System.Windows.Forms.CheckBox ChkIncludeOrderNo;
        private System.Windows.Forms.CheckBox ChkIncludeChallanNo;
        private System.Windows.Forms.CheckBox ChkIncludeBatch;
        private System.Windows.Forms.CheckBox ChkIncludeGodown;
        private System.Windows.Forms.CheckBox ChkDateWise;
        private System.Windows.Forms.CheckBox ChkIncludeRemarks;
        private RoundPanel roundPanel2;
        private System.Windows.Forms.RadioButton rChkDepartment;
        private System.Windows.Forms.RadioButton rChkUserWise;
        private System.Windows.Forms.RadioButton rChkCounter;
        private System.Windows.Forms.RadioButton rChkProductSubGroup;
        private System.Windows.Forms.RadioButton rChkProductGroup;
        private System.Windows.Forms.RadioButton rChkProduct;
        private System.Windows.Forms.RadioButton rChkArea;
        private System.Windows.Forms.RadioButton rChkAgent;
        private System.Windows.Forms.RadioButton rChkCustomer;
        private System.Windows.Forms.RadioButton rChkInvoice;
        private System.Windows.Forms.RadioButton rChkDate;
        private RoundPanel roundPanel6;
        private DevExpress.XtraEditors.SimpleButton BtnShow;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private System.Windows.Forms.CheckBox ChkDynamicReport;
        private System.Windows.Forms.CheckBox ChkSelectAll;
    }
}