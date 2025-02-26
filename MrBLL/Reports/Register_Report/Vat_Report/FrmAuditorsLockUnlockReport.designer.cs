using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Register_Report.Vat_Report
{
    partial class FrmAuditorsLockUnlockReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAuditorsLockUnlockReport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gb_BankReconcile = new System.Windows.Forms.GroupBox();
            this.lbl_OpenFor = new System.Windows.Forms.Label();
            this.cmb_OpenFor = new System.Windows.Forms.ComboBox();
            this.msk_Date = new MrMaskedTextBox();
            this.lbl_AsOnDate = new System.Windows.Forms.Label();
            this.lbl_PageNo = new System.Windows.Forms.Label();
            this.lbl_DateTime = new System.Windows.Forms.Label();
            this.lbl_AccPeriodDate = new System.Windows.Forms.Label();
            this.lbl_AccountingPeriod = new System.Windows.Forms.Label();
            this.btn_UnTagAll = new System.Windows.Forms.Button();
            this.btn_TagAll = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtn_Print = new System.Windows.Forms.ToolStripButton();
            this.tsbtn_Export = new System.Windows.Forms.ToolStripButton();
            this.tsbtn_Search = new System.Windows.Forms.ToolStripButton();
            this.tsbtn_Refresh = new System.Windows.Forms.ToolStripButton();
            this.dgv_DisplayReport = new System.Windows.Forms.DataGridView();
            this.lbl_ComanyName = new System.Windows.Forms.Label();
            this.lbl_ReportDate = new System.Windows.Forms.Label();
            this.lbl_ReportName = new System.Windows.Forms.Label();
            this.lbl_CompanyAddress = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.gb_BankReconcile.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DisplayReport)).BeginInit();
            this.SuspendLayout();
            // 
            // gb_BankReconcile
            // 
            this.gb_BankReconcile.Controls.Add(this.lbl_OpenFor);
            this.gb_BankReconcile.Controls.Add(this.cmb_OpenFor);
            this.gb_BankReconcile.Controls.Add(this.msk_Date);
            this.gb_BankReconcile.Controls.Add(this.lbl_AsOnDate);
            this.gb_BankReconcile.Controls.Add(this.lbl_PageNo);
            this.gb_BankReconcile.Controls.Add(this.lbl_DateTime);
            this.gb_BankReconcile.Controls.Add(this.lbl_AccPeriodDate);
            this.gb_BankReconcile.Controls.Add(this.lbl_AccountingPeriod);
            this.gb_BankReconcile.Controls.Add(this.btn_UnTagAll);
            this.gb_BankReconcile.Controls.Add(this.btn_TagAll);
            this.gb_BankReconcile.Controls.Add(this.toolStrip1);
            this.gb_BankReconcile.Controls.Add(this.dgv_DisplayReport);
            this.gb_BankReconcile.Controls.Add(this.lbl_ComanyName);
            this.gb_BankReconcile.Controls.Add(this.lbl_ReportDate);
            this.gb_BankReconcile.Controls.Add(this.lbl_ReportName);
            this.gb_BankReconcile.Controls.Add(this.lbl_CompanyAddress);
            this.gb_BankReconcile.Location = new System.Drawing.Point(0, -4);
            this.gb_BankReconcile.Name = "gb_BankReconcile";
            this.gb_BankReconcile.Size = new System.Drawing.Size(932, 616);
            this.gb_BankReconcile.TabIndex = 10;
            this.gb_BankReconcile.TabStop = false;
            // 
            // lbl_OpenFor
            // 
            this.lbl_OpenFor.AutoSize = true;
            this.lbl_OpenFor.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_OpenFor.Location = new System.Drawing.Point(5, 50);
            this.lbl_OpenFor.Name = "lbl_OpenFor";
            this.lbl_OpenFor.Size = new System.Drawing.Size(58, 15);
            this.lbl_OpenFor.TabIndex = 92;
            this.lbl_OpenFor.Text = "Open For";
            // 
            // cmb_OpenFor
            // 
            this.cmb_OpenFor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_OpenFor.FormattingEnabled = true;
            this.cmb_OpenFor.Items.AddRange(new object[] {
            "Both",
            "Lock",
            "UnLock"});
            this.cmb_OpenFor.Location = new System.Drawing.Point(80, 48);
            this.cmb_OpenFor.Name = "cmb_OpenFor";
            this.cmb_OpenFor.Size = new System.Drawing.Size(124, 21);
            this.cmb_OpenFor.TabIndex = 1;
            this.cmb_OpenFor.SelectedIndexChanged += new System.EventHandler(this.cmb_OpenFor_SelectedIndexChanged);
            // 
            // msk_Date
            // 
            this.msk_Date.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_Date.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msk_Date.Location = new System.Drawing.Point(110, 79);
            this.msk_Date.Mask = "00/00/0000";
            this.msk_Date.Name = "msk_Date";
            this.msk_Date.Size = new System.Drawing.Size(94, 20);
            this.msk_Date.TabIndex = 2;
            this.msk_Date.Enter += new System.EventHandler(this.msk_Date_Enter);
            this.msk_Date.Leave += new System.EventHandler(this.msk_Date_Leave);
            this.msk_Date.Validated += new System.EventHandler(this.msk_Date_Validated);
            // 
            // lbl_AsOnDate
            // 
            this.lbl_AsOnDate.AutoSize = true;
            this.lbl_AsOnDate.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_AsOnDate.Location = new System.Drawing.Point(5, 81);
            this.lbl_AsOnDate.Name = "lbl_AsOnDate";
            this.lbl_AsOnDate.Size = new System.Drawing.Size(103, 15);
            this.lbl_AsOnDate.TabIndex = 90;
            this.lbl_AsOnDate.Text = "Lock/Unlock Date";
            // 
            // lbl_PageNo
            // 
            this.lbl_PageNo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbl_PageNo.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_PageNo.Location = new System.Drawing.Point(760, 81);
            this.lbl_PageNo.Name = "lbl_PageNo";
            this.lbl_PageNo.Size = new System.Drawing.Size(153, 15);
            this.lbl_PageNo.TabIndex = 88;
            this.lbl_PageNo.Text = "Page No";
            this.lbl_PageNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DateTime
            // 
            this.lbl_DateTime.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbl_DateTime.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_DateTime.Location = new System.Drawing.Point(756, 50);
            this.lbl_DateTime.Name = "lbl_DateTime";
            this.lbl_DateTime.Size = new System.Drawing.Size(157, 11);
            this.lbl_DateTime.TabIndex = 87;
            this.lbl_DateTime.Text = "Date Time";
            this.lbl_DateTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_AccPeriodDate
            // 
            this.lbl_AccPeriodDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbl_AccPeriodDate.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_AccPeriodDate.Location = new System.Drawing.Point(760, 35);
            this.lbl_AccPeriodDate.Name = "lbl_AccPeriodDate";
            this.lbl_AccPeriodDate.Size = new System.Drawing.Size(153, 15);
            this.lbl_AccPeriodDate.TabIndex = 86;
            this.lbl_AccPeriodDate.Text = "Acc Period Date";
            this.lbl_AccPeriodDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_AccountingPeriod
            // 
            this.lbl_AccountingPeriod.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbl_AccountingPeriod.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_AccountingPeriod.Location = new System.Drawing.Point(762, 16);
            this.lbl_AccountingPeriod.Name = "lbl_AccountingPeriod";
            this.lbl_AccountingPeriod.Size = new System.Drawing.Size(148, 15);
            this.lbl_AccountingPeriod.TabIndex = 85;
            this.lbl_AccountingPeriod.Text = "Accounting Period";
            this.lbl_AccountingPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_UnTagAll
            // 
            this.btn_UnTagAll.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_UnTagAll.Location = new System.Drawing.Point(213, 78);
            this.btn_UnTagAll.Name = "btn_UnTagAll";
            this.btn_UnTagAll.Size = new System.Drawing.Size(70, 26);
            this.btn_UnTagAll.TabIndex = 4;
            this.btn_UnTagAll.Text = "&UnLock All";
            this.btn_UnTagAll.UseVisualStyleBackColor = true;
            this.btn_UnTagAll.Click += new System.EventHandler(this.btn_UnTagAll_Click);
            // 
            // btn_TagAll
            // 
            this.btn_TagAll.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_TagAll.Location = new System.Drawing.Point(213, 45);
            this.btn_TagAll.Name = "btn_TagAll";
            this.btn_TagAll.Size = new System.Drawing.Size(70, 26);
            this.btn_TagAll.TabIndex = 3;
            this.btn_TagAll.Text = "&Lock All";
            this.btn_TagAll.UseVisualStyleBackColor = true;
            this.btn_TagAll.Click += new System.EventHandler(this.btn_TagAll_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtn_Print,
            this.tsbtn_Export,
            this.tsbtn_Search,
            this.tsbtn_Refresh});
            this.toolStrip1.Location = new System.Drawing.Point(4, 4);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(119, 25);
            this.toolStrip1.TabIndex = 78;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtn_Print
            // 
            this.tsbtn_Print.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtn_Print.Image = global::MrBLL.Properties.Resources.Print24;
            this.tsbtn_Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_Print.Name = "tsbtn_Print";
            this.tsbtn_Print.Size = new System.Drawing.Size(23, 22);
            this.tsbtn_Print.Text = "Print";
            this.tsbtn_Print.Click += new System.EventHandler(this.tsbtn_Print_Click);
            // 
            // tsbtn_Export
            // 
            this.tsbtn_Export.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtn_Export.Image = global::MrBLL.Properties.Resources.Export;
            this.tsbtn_Export.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_Export.Name = "tsbtn_Export";
            this.tsbtn_Export.Size = new System.Drawing.Size(23, 22);
            this.tsbtn_Export.Text = "Export";
            this.tsbtn_Export.Click += new System.EventHandler(this.tsbtn_Export_Click);
            // 
            // tsbtn_Search
            // 
            this.tsbtn_Search.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtn_Search.Image = global::MrBLL.Properties.Resources.search16;
            this.tsbtn_Search.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_Search.Name = "tsbtn_Search";
            this.tsbtn_Search.Size = new System.Drawing.Size(23, 22);
            this.tsbtn_Search.Text = "Search";
            this.tsbtn_Search.Click += new System.EventHandler(this.tsbtn_Search_Click);
            // 
            // tsbtn_Refresh
            // 
            this.tsbtn_Refresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtn_Refresh.Image = global::MrBLL.Properties.Resources.referesh;
            this.tsbtn_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_Refresh.Name = "tsbtn_Refresh";
            this.tsbtn_Refresh.Size = new System.Drawing.Size(23, 22);
            this.tsbtn_Refresh.Text = "Refresh";
            this.tsbtn_Refresh.Click += new System.EventHandler(this.tsbtn_Refresh_Click);
            // 
            // dgv_DisplayReport
            // 
            this.dgv_DisplayReport.AllowUserToAddRows = false;
            this.dgv_DisplayReport.AllowUserToResizeColumns = false;
            this.dgv_DisplayReport.AllowUserToResizeRows = false;
            this.dgv_DisplayReport.BackgroundColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_DisplayReport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_DisplayReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_DisplayReport.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_DisplayReport.Location = new System.Drawing.Point(0, 111);
            this.dgv_DisplayReport.Margin = new System.Windows.Forms.Padding(2);
            this.dgv_DisplayReport.Name = "dgv_DisplayReport";
            this.dgv_DisplayReport.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_DisplayReport.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dgv_DisplayReport.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_DisplayReport.RowTemplate.Height = 24;
            this.dgv_DisplayReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_DisplayReport.Size = new System.Drawing.Size(931, 505);
            this.dgv_DisplayReport.TabIndex = 10;
            this.dgv_DisplayReport.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_DisplayReport_CellClick);
            this.dgv_DisplayReport.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_DisplayReport_CellEnter);
            this.dgv_DisplayReport.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_DisplayReport_RowEnter);
            this.dgv_DisplayReport.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_DisplayReport_KeyDown);
            this.dgv_DisplayReport.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgv_DisplayReport_KeyPress);
            // 
            // lbl_ComanyName
            // 
            this.lbl_ComanyName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbl_ComanyName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ComanyName.Location = new System.Drawing.Point(76, 11);
            this.lbl_ComanyName.Name = "lbl_ComanyName";
            this.lbl_ComanyName.Size = new System.Drawing.Size(840, 23);
            this.lbl_ComanyName.TabIndex = 81;
            this.lbl_ComanyName.Text = "Company Name";
            this.lbl_ComanyName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ReportDate
            // 
            this.lbl_ReportDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbl_ReportDate.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_ReportDate.Location = new System.Drawing.Point(76, 79);
            this.lbl_ReportDate.Name = "lbl_ReportDate";
            this.lbl_ReportDate.Size = new System.Drawing.Size(840, 19);
            this.lbl_ReportDate.TabIndex = 84;
            this.lbl_ReportDate.Text = "ReportDate";
            this.lbl_ReportDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ReportName
            // 
            this.lbl_ReportName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbl_ReportName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbl_ReportName.Location = new System.Drawing.Point(76, 63);
            this.lbl_ReportName.Name = "lbl_ReportName";
            this.lbl_ReportName.Size = new System.Drawing.Size(840, 17);
            this.lbl_ReportName.TabIndex = 83;
            this.lbl_ReportName.Text = "Report Name";
            this.lbl_ReportName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_CompanyAddress
            // 
            this.lbl_CompanyAddress.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbl_CompanyAddress.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CompanyAddress.Location = new System.Drawing.Point(76, 35);
            this.lbl_CompanyAddress.Name = "lbl_CompanyAddress";
            this.lbl_CompanyAddress.Size = new System.Drawing.Size(840, 16);
            this.lbl_CompanyAddress.TabIndex = 82;
            this.lbl_CompanyAddress.Text = "Company Address";
            this.lbl_CompanyAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // printDocument1
            // 
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // FrmAuditorsLockUnlockReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 612);
            this.Controls.Add(this.gb_BankReconcile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Name = "FrmAuditorsLockUnlockReport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auditors Lock Unlock Report";
            this.Activated += new System.EventHandler(this.FrmAuditorsLockUnlockReport_Activated);
            this.Load += new System.EventHandler(this.FrmAuditorsLockUnlockReport_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmAuditorsLockUnlockReport_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmAuditorsLockUnlockReport_KeyPress);
            this.Resize += new System.EventHandler(this.FrmAuditorsLockUnlockReport_Resize);
            this.gb_BankReconcile.ResumeLayout(false);
            this.gb_BankReconcile.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DisplayReport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_BankReconcile;
        private System.Windows.Forms.DataGridView dgv_DisplayReport;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtn_Print;
        private System.Windows.Forms.ToolStripButton tsbtn_Export;
        private System.Windows.Forms.ToolStripButton tsbtn_Search;
        private System.Windows.Forms.ToolStripButton tsbtn_Refresh;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Button btn_UnTagAll;
        private System.Windows.Forms.Button btn_TagAll;
        private System.Windows.Forms.Label lbl_PageNo;
        private System.Windows.Forms.Label lbl_DateTime;
        private System.Windows.Forms.Label lbl_AccPeriodDate;
        private System.Windows.Forms.Label lbl_AccountingPeriod;
        private System.Windows.Forms.Label lbl_ReportDate;
        private System.Windows.Forms.Label lbl_ReportName;
        private System.Windows.Forms.Label lbl_CompanyAddress;
        private System.Windows.Forms.Label lbl_ComanyName;
        private System.Windows.Forms.MaskedTextBox msk_Date;
        private System.Windows.Forms.Label lbl_AsOnDate;
        private System.Windows.Forms.Label lbl_OpenFor;
        private System.Windows.Forms.ComboBox cmb_OpenFor;

    }
}