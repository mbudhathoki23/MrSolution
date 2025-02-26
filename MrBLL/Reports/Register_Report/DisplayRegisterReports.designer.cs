using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Register_Report
{
    partial class DisplayRegisterReports
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayRegisterReports));
            this.LblAccPeriodDate = new System.Windows.Forms.Label();
            this.lbl_AccountingPeriod = new System.Windows.Forms.Label();
            this.LblReportDate = new System.Windows.Forms.Label();
            this.LblReportName = new System.Windows.Forms.Label();
            this.LblCompanyAddress = new System.Windows.Forms.Label();
            this.LblComanyName = new System.Windows.Forms.Label();
            this.lbl_DateTime = new System.Windows.Forms.Label();
            this.lbl_PageNo = new System.Windows.Forms.Label();
            this.panel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.RGrid = new MrDAL.Control.ControlsEx.Control.DataGridViewEx();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.lbl_ClassCode = new System.Windows.Forms.Label();
            this.grp_RoomStatus = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.txt_ChecIn = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.txt_Booked = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_CheckIn = new System.Windows.Forms.Label();
            this.lbl_Booked = new System.Windows.Forms.Label();
            this.lbl_Currency = new System.Windows.Forms.Label();
            this.LblCompanyPANVATNo = new System.Windows.Forms.Label();
            this.panel2 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.panel4 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtn_Print = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtn_Export = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtn_Search = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnRefreshReport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnFitColumn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.panel3 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).BeginInit();
            this.grp_RoomStatus.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblAccPeriodDate
            // 
            this.LblAccPeriodDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblAccPeriodDate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.LblAccPeriodDate.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAccPeriodDate.Location = new System.Drawing.Point(3, 27);
            this.LblAccPeriodDate.Name = "LblAccPeriodDate";
            this.LblAccPeriodDate.Size = new System.Drawing.Size(207, 19);
            this.LblAccPeriodDate.TabIndex = 29;
            this.LblAccPeriodDate.Text = "2075-04-01 to 2076-03-32";
            this.LblAccPeriodDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_AccountingPeriod
            // 
            this.lbl_AccountingPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_AccountingPeriod.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbl_AccountingPeriod.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AccountingPeriod.Location = new System.Drawing.Point(3, 5);
            this.lbl_AccountingPeriod.Name = "lbl_AccountingPeriod";
            this.lbl_AccountingPeriod.Size = new System.Drawing.Size(207, 19);
            this.lbl_AccountingPeriod.TabIndex = 28;
            this.lbl_AccountingPeriod.Text = "Accounting Peroid";
            this.lbl_AccountingPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblReportDate
            // 
            this.LblReportDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblReportDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblReportDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblReportDate.Location = new System.Drawing.Point(170, 104);
            this.LblReportDate.Name = "LblReportDate";
            this.LblReportDate.Size = new System.Drawing.Size(577, 25);
            this.LblReportDate.TabIndex = 27;
            this.LblReportDate.Text = "ReportDate";
            this.LblReportDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblReportName
            // 
            this.LblReportName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblReportName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblReportName.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblReportName.ForeColor = System.Drawing.Color.Crimson;
            this.LblReportName.Location = new System.Drawing.Point(170, 79);
            this.LblReportName.Name = "LblReportName";
            this.LblReportName.Size = new System.Drawing.Size(577, 25);
            this.LblReportName.TabIndex = 26;
            this.LblReportName.Text = "Report Name";
            this.LblReportName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblCompanyAddress
            // 
            this.LblCompanyAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblCompanyAddress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblCompanyAddress.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCompanyAddress.Location = new System.Drawing.Point(170, 29);
            this.LblCompanyAddress.Name = "LblCompanyAddress";
            this.LblCompanyAddress.Size = new System.Drawing.Size(577, 25);
            this.LblCompanyAddress.TabIndex = 25;
            this.LblCompanyAddress.Text = "Company Address";
            this.LblCompanyAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblComanyName
            // 
            this.LblComanyName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblComanyName.Font = new System.Drawing.Font("Bookman Old Style", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblComanyName.Location = new System.Drawing.Point(170, 4);
            this.LblComanyName.Name = "LblComanyName";
            this.LblComanyName.Size = new System.Drawing.Size(577, 25);
            this.LblComanyName.TabIndex = 24;
            this.LblComanyName.Text = "Company Name";
            this.LblComanyName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DateTime
            // 
            this.lbl_DateTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_DateTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbl_DateTime.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_DateTime.Location = new System.Drawing.Point(3, 49);
            this.lbl_DateTime.Name = "lbl_DateTime";
            this.lbl_DateTime.Size = new System.Drawing.Size(207, 19);
            this.lbl_DateTime.TabIndex = 31;
            this.lbl_DateTime.Text = "Date Time";
            this.lbl_DateTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_PageNo
            // 
            this.lbl_PageNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_PageNo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbl_PageNo.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_PageNo.Location = new System.Drawing.Point(3, 93);
            this.lbl_PageNo.Name = "lbl_PageNo";
            this.lbl_PageNo.Size = new System.Drawing.Size(207, 28);
            this.lbl_PageNo.TabIndex = 32;
            this.lbl_PageNo.Text = "Page No";
            this.lbl_PageNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.RGrid);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 133);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(963, 528);
            this.panel1.TabIndex = 33;
            // 
            // RGrid
            // 
            this.RGrid.AllowUserToAddRows = false;
            this.RGrid.AllowUserToDeleteRows = false;
            this.RGrid.AllowUserToResizeRows = false;
            this.RGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.RGrid.BlockNavigationOnNextRowOnEnter = true;
            this.RGrid.CausesValidation = false;
            this.RGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.RGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
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
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            this.RGrid.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.RGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RGrid.RowTemplate.Height = 24;
            this.RGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RGrid.Size = new System.Drawing.Size(963, 528);
            this.RGrid.TabIndex = 0;
            this.RGrid.EnterKeyPressed += new System.EventHandler<System.EventArgs>(this.RGrid_EnterKeyPressed);
            this.RGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_CellContentClick);
            this.RGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_CellEnter);
            this.RGrid.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.RGrid_CellPainting);
            this.RGrid.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.RGrid_ColumnWidthChanged);
            this.RGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.RGrid_DataError);
            this.RGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_RowEnter);
            this.RGrid.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.RGrid_RowHeaderMouseDoubleClick);
            this.RGrid.Scroll += new System.Windows.Forms.ScrollEventHandler(this.RGrid_Scroll);
            this.RGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.RGrid_Paint);
            this.RGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RGrid_KeyDown);
            // 
            // lbl_ClassCode
            // 
            this.lbl_ClassCode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbl_ClassCode.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_ClassCode.Location = new System.Drawing.Point(91, 80);
            this.lbl_ClassCode.Name = "lbl_ClassCode";
            this.lbl_ClassCode.Size = new System.Drawing.Size(153, 15);
            this.lbl_ClassCode.TabIndex = 34;
            this.lbl_ClassCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grp_RoomStatus
            // 
            this.grp_RoomStatus.Controls.Add(this.label3);
            this.grp_RoomStatus.Controls.Add(this.textBox1);
            this.grp_RoomStatus.Controls.Add(this.txt_ChecIn);
            this.grp_RoomStatus.Controls.Add(this.txt_Booked);
            this.grp_RoomStatus.Controls.Add(this.lbl_CheckIn);
            this.grp_RoomStatus.Controls.Add(this.lbl_Booked);
            this.grp_RoomStatus.Location = new System.Drawing.Point(3, 42);
            this.grp_RoomStatus.Name = "grp_RoomStatus";
            this.grp_RoomStatus.Size = new System.Drawing.Size(141, 68);
            this.grp_RoomStatus.TabIndex = 35;
            this.grp_RoomStatus.TabStop = false;
            this.grp_RoomStatus.Text = "Room Status";
            this.grp_RoomStatus.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 19);
            this.label3.TabIndex = 91;
            this.label3.Text = "CheckIn";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Yellow;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(59, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(30, 25);
            this.textBox1.TabIndex = 1;
            // 
            // txt_ChecIn
            // 
            this.txt_ChecIn.BackColor = System.Drawing.Color.Green;
            this.txt_ChecIn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ChecIn.Location = new System.Drawing.Point(106, 32);
            this.txt_ChecIn.Name = "txt_ChecIn";
            this.txt_ChecIn.ReadOnly = true;
            this.txt_ChecIn.Size = new System.Drawing.Size(30, 25);
            this.txt_ChecIn.TabIndex = 1;
            // 
            // txt_Booked
            // 
            this.txt_Booked.BackColor = System.Drawing.Color.Red;
            this.txt_Booked.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Booked.Location = new System.Drawing.Point(9, 32);
            this.txt_Booked.Name = "txt_Booked";
            this.txt_Booked.ReadOnly = true;
            this.txt_Booked.Size = new System.Drawing.Size(30, 25);
            this.txt_Booked.TabIndex = 1;
            // 
            // lbl_CheckIn
            // 
            this.lbl_CheckIn.AutoSize = true;
            this.lbl_CheckIn.Location = new System.Drawing.Point(103, 16);
            this.lbl_CheckIn.Name = "lbl_CheckIn";
            this.lbl_CheckIn.Size = new System.Drawing.Size(48, 19);
            this.lbl_CheckIn.TabIndex = 0;
            this.lbl_CheckIn.Text = "Open";
            // 
            // lbl_Booked
            // 
            this.lbl_Booked.AutoSize = true;
            this.lbl_Booked.Location = new System.Drawing.Point(6, 16);
            this.lbl_Booked.Name = "lbl_Booked";
            this.lbl_Booked.Size = new System.Drawing.Size(63, 19);
            this.lbl_Booked.TabIndex = 0;
            this.lbl_Booked.Text = "Booked";
            // 
            // lbl_Currency
            // 
            this.lbl_Currency.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Currency.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbl_Currency.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Currency.Location = new System.Drawing.Point(3, 71);
            this.lbl_Currency.Name = "lbl_Currency";
            this.lbl_Currency.Size = new System.Drawing.Size(207, 19);
            this.lbl_Currency.TabIndex = 36;
            this.lbl_Currency.Text = "Currency :";
            this.lbl_Currency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_Currency.Visible = false;
            // 
            // LblCompanyPANVATNo
            // 
            this.LblCompanyPANVATNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblCompanyPANVATNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblCompanyPANVATNo.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCompanyPANVATNo.Location = new System.Drawing.Point(170, 54);
            this.LblCompanyPANVATNo.Name = "LblCompanyPANVATNo";
            this.LblCompanyPANVATNo.Size = new System.Drawing.Size(577, 25);
            this.LblCompanyPANVATNo.TabIndex = 37;
            this.LblCompanyPANVATNo.Text = "PAN/VAT No";
            this.LblCompanyPANVATNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel2.Controls.Add(this.LblReportName);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.lbl_ClassCode);
            this.panel2.Controls.Add(this.LblComanyName);
            this.panel2.Controls.Add(this.LblCompanyAddress);
            this.panel2.Controls.Add(this.LblReportDate);
            this.panel2.Controls.Add(this.LblCompanyPANVATNo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(963, 133);
            this.panel2.TabIndex = 38;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel4.Controls.Add(this.pbLogo);
            this.panel4.Controls.Add(this.toolStrip1);
            this.panel4.Controls.Add(this.grp_RoomStatus);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(165, 133);
            this.panel4.TabIndex = 39;
            // 
            // pbLogo
            // 
            this.pbLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbLogo.Location = new System.Drawing.Point(0, 35);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(165, 98);
            this.pbLogo.TabIndex = 36;
            this.pbLogo.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtn_Print,
            this.toolStripSeparator5,
            this.tsbtn_Export,
            this.toolStripSeparator3,
            this.tsbtn_Search,
            this.toolStripSeparator4,
            this.tsBtnRefreshReport,
            this.toolStripSeparator1,
            this.BtnFitColumn,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(165, 35);
            this.toolStrip1.TabIndex = 73;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtn_Print
            // 
            this.tsbtn_Print.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsbtn_Print.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtn_Print.Image = global::MrBLL.Properties.Resources.Print_16;
            this.tsbtn_Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_Print.Name = "tsbtn_Print";
            this.tsbtn_Print.Size = new System.Drawing.Size(23, 32);
            this.tsbtn_Print.Text = "Print";
            this.tsbtn_Print.ToolTipText = "Print Report";
            this.tsbtn_Print.Click += new System.EventHandler(this.TsBtnPrint_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 35);
            // 
            // tsbtn_Export
            // 
            this.tsbtn_Export.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtn_Export.Image = global::MrBLL.Properties.Resources.Excel16;
            this.tsbtn_Export.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_Export.Name = "tsbtn_Export";
            this.tsbtn_Export.Size = new System.Drawing.Size(23, 32);
            this.tsbtn_Export.Text = "Export";
            this.tsbtn_Export.ToolTipText = "Export Report";
            this.tsbtn_Export.Click += new System.EventHandler(this.TsBtnExport_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 35);
            // 
            // tsbtn_Search
            // 
            this.tsbtn_Search.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtn_Search.Image = global::MrBLL.Properties.Resources.search16;
            this.tsbtn_Search.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_Search.Name = "tsbtn_Search";
            this.tsbtn_Search.Size = new System.Drawing.Size(23, 32);
            this.tsbtn_Search.Text = "Search";
            this.tsbtn_Search.ToolTipText = "Search Value";
            this.tsbtn_Search.Click += new System.EventHandler(this.TsBtnSearch_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 35);
            // 
            // tsBtnRefreshReport
            // 
            this.tsBtnRefreshReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnRefreshReport.Image = global::MrBLL.Properties.Resources.refresh;
            this.tsBtnRefreshReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnRefreshReport.Name = "tsBtnRefreshReport";
            this.tsBtnRefreshReport.Size = new System.Drawing.Size(23, 32);
            this.tsBtnRefreshReport.Text = "Refresh _Reports";
            this.tsBtnRefreshReport.ToolTipText = "Refresh _Reports";
            this.tsBtnRefreshReport.Click += new System.EventHandler(this.TsBtnRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 35);
            // 
            // BtnFitColumn
            // 
            this.BtnFitColumn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnFitColumn.Image = global::MrBLL.Properties.Resources.FitColumn;
            this.BtnFitColumn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnFitColumn.Name = "BtnFitColumn";
            this.BtnFitColumn.Size = new System.Drawing.Size(23, 32);
            this.BtnFitColumn.Text = "Fit Report Column";
            this.BtnFitColumn.Click += new System.EventHandler(this.TsBtnFitColumn_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 35);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel3.Controls.Add(this.lbl_AccountingPeriod);
            this.panel3.Controls.Add(this.LblAccPeriodDate);
            this.panel3.Controls.Add(this.lbl_DateTime);
            this.panel3.Controls.Add(this.lbl_Currency);
            this.panel3.Controls.Add(this.lbl_PageNo);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(750, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(213, 133);
            this.panel3.TabIndex = 38;
            // 
            // printDocument1
            // 
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.PrintDocument1_BeginPrint);
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocument1_PrintPage);
            // 
            // DisplayRegisterReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(963, 661);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DisplayRegisterReports";
            this.ShowIcon = false;
            this.Text = "Display Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DisplayRegisterReports_Load);
            this.Shown += new System.EventHandler(this.DisplayRegisterReports_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DisplayRegisterReports_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DisplayRegisterReports_KeyPress);
            this.Resize += new System.EventHandler(this.DisplayRegisterReports_Resize);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).EndInit();
            this.grp_RoomStatus.ResumeLayout(false);
            this.grp_RoomStatus.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LblAccPeriodDate;
        private System.Windows.Forms.Label lbl_AccountingPeriod;
        private System.Windows.Forms.Label LblReportDate;
        private System.Windows.Forms.Label LblReportName;
        private System.Windows.Forms.Label LblCompanyAddress;
        private System.Windows.Forms.Label LblComanyName;
        private System.Windows.Forms.Label lbl_DateTime;
        private System.Windows.Forms.Label lbl_PageNo;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label lbl_ClassCode;
        private System.Windows.Forms.GroupBox grp_RoomStatus;
        private System.Windows.Forms.Label lbl_Booked;
        private System.Windows.Forms.Label lbl_CheckIn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_Currency;
        private System.Windows.Forms.Label LblCompanyPANVATNo;
        private DataGridViewEx RGrid;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtn_Print;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbtn_Export;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbtn_Search;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsBtnRefreshReport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton BtnFitColumn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private MrTextBox txt_ChecIn;
        private MrTextBox txt_Booked;
        private MrTextBox textBox1;
        private MrPanel panel1;
        private MrPanel panel2;
        private MrPanel panel4;
        private MrPanel panel3;
    }
}