using System.ComponentModel;
using System.Drawing.Printing;
using System.Windows.Forms;
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Finance_Report
{
    partial class DisplayFinanceReports
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayFinanceReports));
            this.RGrid = new DataGridViewEx();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsBtnPrintReport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnExportReport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnSearchReport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnRefreshReport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnAutoColumnSizeReport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.PanelHeader = new MrPanel();
            this.panel2 = new MrPanel();
            this.lbl_AccountingPeriod = new System.Windows.Forms.Label();
            this.LblAccPeriodDate = new System.Windows.Forms.Label();
            this.lbl_DateTime = new System.Windows.Forms.Label();
            this.lbl_PageNo = new System.Windows.Forms.Label();
            this.lbl_ComanyName = new System.Windows.Forms.Label();
            this.LblReportName = new System.Windows.Forms.Label();
            this.panel4 = new MrPanel();
            this.LblCompanyPANVATNo = new System.Windows.Forms.Label();
            this.LblCompanyAddress = new System.Windows.Forms.Label();
            this.LblReportDate = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.PanelHeader.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // RGrid
            // 
            this.RGrid.AllowUserToAddRows = false;
            this.RGrid.AllowUserToDeleteRows = false;
            this.RGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.RGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.RGrid.BlockNavigationOnNextRowOnEnter = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.RGrid.ColumnHeadersHeight = 30;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.RGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.RGrid.DisplayColumnChooser = true;
            this.RGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RGrid.DoubleBufferEnabled = true;
            this.RGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.RGrid.Location = new System.Drawing.Point(0, 118);
            this.RGrid.Margin = new System.Windows.Forms.Padding(2);
            this.RGrid.MultiSelect = false;
            this.RGrid.Name = "RGrid";
            this.RGrid.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.RGrid.RowHeadersWidth = 30;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            this.RGrid.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.RGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RGrid.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            this.RGrid.RowTemplate.Height = 24;
            this.RGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RGrid.Size = new System.Drawing.Size(984, 543);
            this.RGrid.StandardTab = true;
            this.RGrid.TabIndex = 0;
            this.RGrid.EnterKeyPressed += new System.EventHandler<System.EventArgs>(this.RGrid_EnterKeyPressed);
            this.RGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_CellContentClick);
            this.RGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_CellEnter);
            this.RGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.RGrid_CellFormatting);
            this.RGrid.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.RGrid_DataBindingComplete);
            this.RGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_RowEnter);
            this.RGrid.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.RGrid_RowHeaderMouseDoubleClick);
            this.RGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RGrid_KeyDown);
            this.RGrid.Leave += new System.EventHandler(this.RGrid_Leave);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnPrintReport,
            this.toolStripSeparator5,
            this.tsBtnExportReport,
            this.toolStripSeparator3,
            this.tsBtnSearchReport,
            this.toolStripSeparator4,
            this.tsBtnRefreshReport,
            this.toolStripSeparator1,
            this.tsBtnAutoColumnSizeReport,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(155, 25);
            this.toolStrip1.TabIndex = 72;
            // 
            // tsBtnPrintReport
            // 
            this.tsBtnPrintReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnPrintReport.Image = global::MrBLL.Properties.Resources.Print_16;
            this.tsBtnPrintReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnPrintReport.Name = "tsBtnPrintReport";
            this.tsBtnPrintReport.Size = new System.Drawing.Size(23, 22);
            this.tsBtnPrintReport.Text = "PRINT REPORT";
            this.tsBtnPrintReport.Click += new System.EventHandler(this.TsBtnPrint_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tsBtnExportReport
            // 
            this.tsBtnExportReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnExportReport.Image = global::MrBLL.Properties.Resources.Excel16;
            this.tsBtnExportReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnExportReport.Name = "tsBtnExportReport";
            this.tsBtnExportReport.Size = new System.Drawing.Size(23, 22);
            this.tsBtnExportReport.Text = "EXPORT REPORT TO EXCEL";
            this.tsBtnExportReport.Click += new System.EventHandler(this.TsBtnExport_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsBtnSearchReport
            // 
            this.tsBtnSearchReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnSearchReport.Image = global::MrBLL.Properties.Resources.search16;
            this.tsBtnSearchReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnSearchReport.Name = "tsBtnSearchReport";
            this.tsBtnSearchReport.Size = new System.Drawing.Size(23, 22);
            this.tsBtnSearchReport.Text = "SEARCH DATA ON REPORT";
            this.tsBtnSearchReport.Click += new System.EventHandler(this.TsBtnSearch_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsBtnRefreshReport
            // 
            this.tsBtnRefreshReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnRefreshReport.Image = global::MrBLL.Properties.Resources.refresh;
            this.tsBtnRefreshReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnRefreshReport.Name = "tsBtnRefreshReport";
            this.tsBtnRefreshReport.Size = new System.Drawing.Size(23, 22);
            this.tsBtnRefreshReport.Text = "REFRESH DATA";
            this.tsBtnRefreshReport.Click += new System.EventHandler(this.TsBtnRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsBtnAutoColumnSizeReport
            // 
            this.tsBtnAutoColumnSizeReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnAutoColumnSizeReport.Image = global::MrBLL.Properties.Resources.FitColumn;
            this.tsBtnAutoColumnSizeReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnAutoColumnSizeReport.Name = "tsBtnAutoColumnSizeReport";
            this.tsBtnAutoColumnSizeReport.Size = new System.Drawing.Size(23, 22);
            this.tsBtnAutoColumnSizeReport.Text = "FIT REPORT COLUMN";
            this.tsBtnAutoColumnSizeReport.Click += new System.EventHandler(this.TsBtnFitColumnGrid_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // printDocument1
            // 
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.PrintDocument1_BeginPrint);
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocument1_PrintPage);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.panel2);
            this.PanelHeader.Controls.Add(this.lbl_ComanyName);
            this.PanelHeader.Controls.Add(this.LblReportName);
            this.PanelHeader.Controls.Add(this.panel4);
            this.PanelHeader.Controls.Add(this.LblCompanyPANVATNo);
            this.PanelHeader.Controls.Add(this.LblCompanyAddress);
            this.PanelHeader.Controls.Add(this.LblReportDate);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(984, 118);
            this.PanelHeader.TabIndex = 82;
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
            this.panel2.Location = new System.Drawing.Point(769, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(215, 118);
            this.panel2.TabIndex = 80;
            // 
            // lbl_AccountingPeriod
            // 
            this.lbl_AccountingPeriod.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AccountingPeriod.Location = new System.Drawing.Point(7, 4);
            this.lbl_AccountingPeriod.Name = "lbl_AccountingPeriod";
            this.lbl_AccountingPeriod.Size = new System.Drawing.Size(205, 21);
            this.lbl_AccountingPeriod.TabIndex = 38;
            this.lbl_AccountingPeriod.Text = "Accounting Period";
            this.lbl_AccountingPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblAccPeriodDate
            // 
            this.LblAccPeriodDate.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAccPeriodDate.Location = new System.Drawing.Point(7, 24);
            this.LblAccPeriodDate.Name = "LblAccPeriodDate";
            this.LblAccPeriodDate.Size = new System.Drawing.Size(205, 21);
            this.LblAccPeriodDate.TabIndex = 39;
            this.LblAccPeriodDate.Text = "Acc Period Date";
            this.LblAccPeriodDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DateTime
            // 
            this.lbl_DateTime.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_DateTime.Location = new System.Drawing.Point(7, 43);
            this.lbl_DateTime.Name = "lbl_DateTime";
            this.lbl_DateTime.Size = new System.Drawing.Size(205, 21);
            this.lbl_DateTime.TabIndex = 41;
            this.lbl_DateTime.Text = "Date Time";
            this.lbl_DateTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_PageNo
            // 
            this.lbl_PageNo.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_PageNo.Location = new System.Drawing.Point(7, 62);
            this.lbl_PageNo.Name = "lbl_PageNo";
            this.lbl_PageNo.Size = new System.Drawing.Size(205, 21);
            this.lbl_PageNo.TabIndex = 42;
            this.lbl_PageNo.Text = "Page No";
            this.lbl_PageNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ComanyName
            // 
            this.lbl_ComanyName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_ComanyName.Font = new System.Drawing.Font("Bookman Old Style", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ComanyName.Location = new System.Drawing.Point(164, 2);
            this.lbl_ComanyName.Name = "lbl_ComanyName";
            this.lbl_ComanyName.Size = new System.Drawing.Size(599, 27);
            this.lbl_ComanyName.TabIndex = 75;
            this.lbl_ComanyName.Text = "Company Name";
            this.lbl_ComanyName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblReportName
            // 
            this.LblReportName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblReportName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblReportName.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblReportName.ForeColor = System.Drawing.Color.Crimson;
            this.LblReportName.Location = new System.Drawing.Point(164, 73);
            this.LblReportName.Name = "LblReportName";
            this.LblReportName.Size = new System.Drawing.Size(599, 20);
            this.LblReportName.TabIndex = 77;
            this.LblReportName.Text = "Report Name";
            this.LblReportName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel4.Controls.Add(this.toolStrip1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(155, 118);
            this.panel4.TabIndex = 73;
            // 
            // LblCompanyPANVATNo
            // 
            this.LblCompanyPANVATNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblCompanyPANVATNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblCompanyPANVATNo.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCompanyPANVATNo.Location = new System.Drawing.Point(164, 52);
            this.LblCompanyPANVATNo.Name = "LblCompanyPANVATNo";
            this.LblCompanyPANVATNo.Size = new System.Drawing.Size(599, 20);
            this.LblCompanyPANVATNo.TabIndex = 79;
            this.LblCompanyPANVATNo.Text = "PAN/VAT No";
            this.LblCompanyPANVATNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblCompanyAddress
            // 
            this.LblCompanyAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblCompanyAddress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblCompanyAddress.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCompanyAddress.Location = new System.Drawing.Point(164, 30);
            this.LblCompanyAddress.Name = "LblCompanyAddress";
            this.LblCompanyAddress.Size = new System.Drawing.Size(599, 20);
            this.LblCompanyAddress.TabIndex = 76;
            this.LblCompanyAddress.Text = "Company Address";
            this.LblCompanyAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblReportDate
            // 
            this.LblReportDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblReportDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblReportDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblReportDate.Location = new System.Drawing.Point(164, 95);
            this.LblReportDate.Name = "LblReportDate";
            this.LblReportDate.Size = new System.Drawing.Size(599, 20);
            this.LblReportDate.TabIndex = 78;
            this.LblReportDate.Text = "ReportDate";
            this.LblReportDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DisplayFinanceReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.RGrid);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DisplayFinanceReports";
            this.ShowIcon = false;
            this.Text = "FINANCE REPORT";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.RptReportDisplay_Load);
            this.Shown += new System.EventHandler(this.DisplayFinanceReports_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RptReportDisplay_KeyPress);
            this.Resize += new System.EventHandler(this.RptReportDisplay_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.PanelHeader.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private ToolStrip toolStrip1;
        private ToolStripButton tsBtnPrintReport;
        private ToolStripButton tsBtnExportReport;
        private ToolStripButton tsBtnSearchReport;
        private ToolStripButton tsBtnRefreshReport;
        private SaveFileDialog saveFileDialog1;
        private PrintDocument printDocument1;
		private PrintPreviewDialog printPreviewDialog1;
        private ToolStripButton tsBtnAutoColumnSizeReport;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
		private Label lbl_ComanyName;
		private Label LblCompanyAddress;
		private Label LblReportDate;
		private Label LblCompanyPANVATNo;
		private Label LblReportName;
        private Label lbl_AccountingPeriod;
        private Label LblAccPeriodDate;
        private Label lbl_DateTime;
        private Label lbl_PageNo;
        private DataGridViewEx RGrid;
        private MrPanel PanelHeader;
        private MrPanel panel4;
        private MrPanel panel2;
    }
}