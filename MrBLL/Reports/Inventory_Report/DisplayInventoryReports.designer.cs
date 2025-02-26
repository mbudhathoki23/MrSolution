using System.Windows.Forms;
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Inventory_Report
{
    partial class DisplayInventoryReports
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayInventoryReports));
            this.lbl_PageNo = new System.Windows.Forms.Label();
            this.lbl_DateTime = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtn_Print = new System.Windows.Forms.ToolStripButton();
            this.tsbtn_Export = new System.Windows.Forms.ToolStripButton();
            this.tsbtn_Search = new System.Windows.Forms.ToolStripButton();
            this.tsBtnRefreshReport = new System.Windows.Forms.ToolStripButton();
            this.BtnFitColumn = new System.Windows.Forms.ToolStripButton();
            this.LblAccPeriodDate = new System.Windows.Forms.Label();
            this.lbl_AccountingPeriod = new System.Windows.Forms.Label();
            this.LblReportDate = new System.Windows.Forms.Label();
            this.LblReportName = new System.Windows.Forms.Label();
            this.LblCompanyAddress = new System.Windows.Forms.Label();
            this.lbl_ComanyName = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.panel1 = new MrPanel();
            this.panel2 = new MrPanel();
            this.LblCompanyPANVATNo = new System.Windows.Forms.Label();
            this.panel3 = new MrPanel();
            this.panel4 = new MrPanel();
            this.RGrid = new DataGridViewEx();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_PageNo
            // 
            this.lbl_PageNo.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_PageNo.Location = new System.Drawing.Point(7, 93);
            this.lbl_PageNo.Name = "lbl_PageNo";
            this.lbl_PageNo.Size = new System.Drawing.Size(205, 21);
            this.lbl_PageNo.TabIndex = 42;
            this.lbl_PageNo.Text = "Page No";
            this.lbl_PageNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DateTime
            // 
            this.lbl_DateTime.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_DateTime.Location = new System.Drawing.Point(7, 64);
            this.lbl_DateTime.Name = "lbl_DateTime";
            this.lbl_DateTime.Size = new System.Drawing.Size(205, 21);
            this.lbl_DateTime.TabIndex = 41;
            this.lbl_DateTime.Text = "Date Time";
            this.lbl_DateTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtn_Print,
            this.tsbtn_Export,
            this.tsbtn_Search,
            this.tsBtnRefreshReport,
            this.BtnFitColumn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(140, 27);
            this.toolStrip1.TabIndex = 40;
            // 
            // tsbtn_Print
            // 
            this.tsbtn_Print.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtn_Print.Image = global::MrBLL.Properties.Resources.Print24;
            this.tsbtn_Print.Name = "tsbtn_Print";
            this.tsbtn_Print.Size = new System.Drawing.Size(24, 24);
            this.tsbtn_Print.Text = "Print";
            this.tsbtn_Print.Click += new System.EventHandler(this.TsBtnPrint_Click);
            // 
            // tsbtn_Export
            // 
            this.tsbtn_Export.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtn_Export.Image = global::MrBLL.Properties.Resources.Excel16;
            this.tsbtn_Export.Name = "tsbtn_Export";
            this.tsbtn_Export.Size = new System.Drawing.Size(24, 24);
            this.tsbtn_Export.Text = "Export";
            this.tsbtn_Export.Click += new System.EventHandler(this.TsBtnExport_Click);
            // 
            // tsbtn_Search
            // 
            this.tsbtn_Search.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtn_Search.Image = global::MrBLL.Properties.Resources.search16;
            this.tsbtn_Search.Name = "tsbtn_Search";
            this.tsbtn_Search.Size = new System.Drawing.Size(24, 24);
            this.tsbtn_Search.Text = "Search";
            this.tsbtn_Search.Click += new System.EventHandler(this.TsBtnSearch_Click);
            // 
            // tsBtnRefreshReport
            // 
            this.tsBtnRefreshReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnRefreshReport.Image = global::MrBLL.Properties.Resources.refresh;
            this.tsBtnRefreshReport.Name = "tsBtnRefreshReport";
            this.tsBtnRefreshReport.Size = new System.Drawing.Size(24, 24);
            this.tsBtnRefreshReport.Text = "Refresh";
            this.tsBtnRefreshReport.Click += new System.EventHandler(this.TsBtnRefresh_Click);
            // 
            // BtnFitColumn
            // 
            this.BtnFitColumn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnFitColumn.Image = global::MrBLL.Properties.Resources.FitColumn;
            this.BtnFitColumn.Name = "BtnFitColumn";
            this.BtnFitColumn.Size = new System.Drawing.Size(24, 24);
            this.BtnFitColumn.Text = "Fit Column";
            this.BtnFitColumn.Click += new System.EventHandler(this.BtnFitColumn_Click);
            // 
            // LblAccPeriodDate
            // 
            this.LblAccPeriodDate.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAccPeriodDate.Location = new System.Drawing.Point(7, 35);
            this.LblAccPeriodDate.Name = "LblAccPeriodDate";
            this.LblAccPeriodDate.Size = new System.Drawing.Size(205, 21);
            this.LblAccPeriodDate.TabIndex = 39;
            this.LblAccPeriodDate.Text = "Acc Period Date";
            this.LblAccPeriodDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_AccountingPeriod
            // 
            this.lbl_AccountingPeriod.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AccountingPeriod.Location = new System.Drawing.Point(7, 6);
            this.lbl_AccountingPeriod.Name = "lbl_AccountingPeriod";
            this.lbl_AccountingPeriod.Size = new System.Drawing.Size(205, 21);
            this.lbl_AccountingPeriod.TabIndex = 38;
            this.lbl_AccountingPeriod.Text = "Accounting Period";
            this.lbl_AccountingPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblReportDate
            // 
            this.LblReportDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblReportDate.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.LblReportDate.Location = new System.Drawing.Point(146, 98);
            this.LblReportDate.Name = "LblReportDate";
            this.LblReportDate.Size = new System.Drawing.Size(624, 22);
            this.LblReportDate.TabIndex = 37;
            this.LblReportDate.Text = "ReportDate";
            this.LblReportDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblReportName
            // 
            this.LblReportName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblReportName.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.LblReportName.ForeColor = System.Drawing.Color.Crimson;
            this.LblReportName.Location = new System.Drawing.Point(146, 75);
            this.LblReportName.Name = "LblReportName";
            this.LblReportName.Size = new System.Drawing.Size(624, 23);
            this.LblReportName.TabIndex = 36;
            this.LblReportName.Text = "Report Name";
            this.LblReportName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblCompanyAddress
            // 
            this.LblCompanyAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblCompanyAddress.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCompanyAddress.Location = new System.Drawing.Point(146, 28);
            this.LblCompanyAddress.Name = "LblCompanyAddress";
            this.LblCompanyAddress.Size = new System.Drawing.Size(624, 24);
            this.LblCompanyAddress.TabIndex = 35;
            this.LblCompanyAddress.Text = "Company Address";
            this.LblCompanyAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ComanyName
            // 
            this.lbl_ComanyName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_ComanyName.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ComanyName.Location = new System.Drawing.Point(146, 5);
            this.lbl_ComanyName.Name = "lbl_ComanyName";
            this.lbl_ComanyName.Size = new System.Drawing.Size(624, 23);
            this.lbl_ComanyName.TabIndex = 34;
            this.lbl_ComanyName.Text = "Company Name";
            this.lbl_ComanyName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // printDocument1
            // 
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.PrintDocument1_BeginPrint);
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocument1_PrintPage);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.lbl_ComanyName);
            this.panel1.Controls.Add(this.LblCompanyAddress);
            this.panel1.Controls.Add(this.LblCompanyPANVATNo);
            this.panel1.Controls.Add(this.LblReportName);
            this.panel1.Controls.Add(this.LblReportDate);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 125);
            this.panel1.TabIndex = 43;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel2.Controls.Add(this.lbl_AccountingPeriod);
            this.panel2.Controls.Add(this.LblAccPeriodDate);
            this.panel2.Controls.Add(this.lbl_DateTime);
            this.panel2.Controls.Add(this.lbl_PageNo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(766, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(218, 125);
            this.panel2.TabIndex = 44;
            // 
            // LblCompanyPANVATNo
            // 
            this.LblCompanyPANVATNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblCompanyPANVATNo.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.LblCompanyPANVATNo.Location = new System.Drawing.Point(146, 52);
            this.LblCompanyPANVATNo.Name = "LblCompanyPANVATNo";
            this.LblCompanyPANVATNo.Size = new System.Drawing.Size(624, 23);
            this.LblCompanyPANVATNo.TabIndex = 80;
            this.LblCompanyPANVATNo.Text = "PAN/VAT No";
            this.LblCompanyPANVATNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel3.Controls.Add(this.toolStrip1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(140, 125);
            this.panel3.TabIndex = 81;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel4.Controls.Add(this.RGrid);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(0, 125);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(984, 536);
            this.panel4.TabIndex = 44;
            // 
            // RGrid
            // 
            this.RGrid.AllowUserToAddRows = false;
            this.RGrid.AllowUserToDeleteRows = false;
            this.RGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.RGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.RGrid.CausesValidation = false;
            this.RGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.RGrid.ColumnHeadersHeight = 30;
            this.RGrid.DisplayColumnChooser = true;
            this.RGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RGrid.DoubleBufferEnabled = true;
            this.RGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.RGrid.Location = new System.Drawing.Point(0, 0);
            this.RGrid.Margin = new System.Windows.Forms.Padding(2);
            this.RGrid.MultiSelect = false;
            this.RGrid.Name = "RGrid";
            this.RGrid.ReadOnly = true;
            this.RGrid.RowHeadersWidth = 30;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RGrid.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.RGrid.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.GhostWhite;
            this.RGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RGrid.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            this.RGrid.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FloralWhite;
            this.RGrid.RowTemplate.Height = 24;
            this.RGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RGrid.Size = new System.Drawing.Size(984, 536);
            this.RGrid.TabIndex = 1;
            this.RGrid.EnterKeyPressed += new System.EventHandler<System.EventArgs>(this.RGrid_EnterKeyPressed);
            this.RGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_CellEnter);
            this.RGrid.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.RGrid_DataBindingComplete);
            this.RGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_RowEnter);
            this.RGrid.SizeChanged += new System.EventHandler(this.RGrid_SizeChanged);
            this.RGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RGrid_KeyDown);
            // 
            // DisplayInventoryReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DisplayInventoryReports";
            this.ShowIcon = false;
            this.Text = "Inventory Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmInventoryReport_FormClosed);
            this.Load += new System.EventHandler(this.FrmInventoryReport_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmInventoryReport_KeyPress);
            this.Resize += new System.EventHandler(this.FrmInventoryReport_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_PageNo;
        private System.Windows.Forms.Label lbl_DateTime;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtn_Print;
        private System.Windows.Forms.ToolStripButton tsbtn_Export;
        private System.Windows.Forms.ToolStripButton tsbtn_Search;
        private System.Windows.Forms.Label LblAccPeriodDate;
        private System.Windows.Forms.Label lbl_AccountingPeriod;
        private System.Windows.Forms.Label LblReportDate;
        private System.Windows.Forms.Label LblReportName;
        private System.Windows.Forms.Label LblCompanyAddress;
        private System.Windows.Forms.Label lbl_ComanyName;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.ToolStripButton tsBtnRefreshReport;
        private System.Windows.Forms.Label LblCompanyPANVATNo;
        private DataGridViewEx RGrid;
        private System.Windows.Forms.ToolStripButton BtnFitColumn;
        private MrPanel panel1;
        private MrPanel panel2;
        private MrPanel panel4;
        private MrPanel panel3;
    }
}