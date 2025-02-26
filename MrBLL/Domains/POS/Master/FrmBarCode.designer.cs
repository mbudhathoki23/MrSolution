
using System.Windows.Forms;
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.POS.Master
{
    partial class FrmBarCode
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBarCode));
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkAllPrintName = new System.Windows.Forms.CheckBox();
            this.btnSelectNone = new System.Windows.Forms.Button();
            this.btnPageDialog = new System.Windows.Forms.Button();
            this.btnAddSelected = new System.Windows.Forms.Button();
            this.chkAllPrintRate = new System.Windows.Forms.CheckBox();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.chkAllPrintCompany = new System.Windows.Forms.CheckBox();
            this.chkAllPrintCode = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnClearList = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gridProducts = new MrDAL.Control.ControlsEx.Control.EntryGridViewEx();
            this.colCheckBox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAdd = new System.Windows.Forms.DataGridViewButtonColumn();
            this.bsProducts = new System.Windows.Forms.BindingSource(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtSearchProduct = new System.Windows.Forms.TextBox();
            this.cbxProductSource = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.gridBarcodes = new MrDAL.Control.ControlsEx.Control.EntryGridViewEx();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPrintedBarCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrintCode = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colPrintCompany = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colPrintProductName = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colPrintSalesRate = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colTextType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colTextAlignment = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.bsBarcodes = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nudMarginBottom = new System.Windows.Forms.NumericUpDown();
            this.nudMarginRight = new System.Windows.Forms.NumericUpDown();
            this.nudMarginTop = new System.Windows.Forms.NumericUpDown();
            this.nudPaperWidth = new System.Windows.Forms.NumericUpDown();
            this.nudPaperHeight = new System.Windows.Forms.NumericUpDown();
            this.nudMarginLeft = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cbxPrinters = new System.Windows.Forms.ComboBox();
            this.cbxPaperKind = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudAutoFitPageWidth = new System.Windows.Forms.NumericUpDown();
            this.chkFillBarSpace = new System.Windows.Forms.CheckBox();
            this.chkDrawBorder = new System.Windows.Forms.CheckBox();
            this.chkAutoFitPageWidth = new System.Windows.Forms.CheckBox();
            this.chkAutoBarModule = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.nudColumnSpacing = new System.Windows.Forms.NumericUpDown();
            this.nudBarModule = new System.Windows.Forms.NumericUpDown();
            this.nudRowHeight = new System.Windows.Forms.NumericUpDown();
            this.nudColumnWidth = new System.Windows.Forms.NumericUpDown();
            this.nudColumnCount = new System.Windows.Forms.NumericUpDown();
            this.tlpFontSettings = new System.Windows.Forms.TableLayoutPanel();
            this.chkBoxPrintSalesRate = new System.Windows.Forms.CheckBox();
            this.chkBoxPrintProductName = new System.Windows.Forms.CheckBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.lblCompanyFont = new System.Windows.Forms.Label();
            this.llSalesRate = new System.Windows.Forms.LinkLabel();
            this.lblProductFont = new System.Windows.Forms.Label();
            this.llProductNameFont = new System.Windows.Forms.LinkLabel();
            this.lblSalesRateFont = new System.Windows.Forms.Label();
            this.llCompanyFont = new System.Windows.Forms.LinkLabel();
            this.chkBoxPrintCompany = new System.Windows.Forms.CheckBox();
            this.label25 = new System.Windows.Forms.Label();
            this.chkBoxPrintBarcode = new System.Windows.Forms.CheckBox();
            this.txtBoxCompany = new System.Windows.Forms.TextBox();
            this.cmbSelectedBarCode = new System.Windows.Forms.ComboBox();
            this.cmbCompAlign = new System.Windows.Forms.ComboBox();
            this.cmbProductAlign = new System.Windows.Forms.ComboBox();
            this.cmbRateAlign = new System.Windows.Forms.ComboBox();
            this.cmbBarcodeAlign = new System.Windows.Forms.ComboBox();
            this.cmbRateOptions = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cbxSymbology = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProducts)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBarcodes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsBarcodes)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMarginBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMarginRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMarginTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPaperWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPaperHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMarginLeft)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAutoFitPageWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudColumnSpacing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBarModule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRowHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudColumnWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudColumnCount)).BeginInit();
            this.tlpFontSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkAllPrintName);
            this.panel1.Controls.Add(this.btnSelectNone);
            this.panel1.Controls.Add(this.btnPageDialog);
            this.panel1.Controls.Add(this.btnAddSelected);
            this.panel1.Controls.Add(this.chkAllPrintRate);
            this.panel1.Controls.Add(this.btnSelectAll);
            this.panel1.Controls.Add(this.chkAllPrintCompany);
            this.panel1.Controls.Add(this.chkAllPrintCode);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnClearList);
            this.panel1.Controls.Add(this.btnPreview);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 707);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1708, 47);
            this.panel1.TabIndex = 0;
            // 
            // chkAllPrintName
            // 
            this.chkAllPrintName.AutoSize = true;
            this.chkAllPrintName.Location = new System.Drawing.Point(723, 15);
            this.chkAllPrintName.Name = "chkAllPrintName";
            this.chkAllPrintName.Size = new System.Drawing.Size(15, 14);
            this.chkAllPrintName.TabIndex = 3;
            this.chkAllPrintName.UseVisualStyleBackColor = true;
            this.chkAllPrintName.Visible = false;
            // 
            // btnSelectNone
            // 
            this.btnSelectNone.Location = new System.Drawing.Point(94, 8);
            this.btnSelectNone.Name = "btnSelectNone";
            this.btnSelectNone.Size = new System.Drawing.Size(75, 30);
            this.btnSelectNone.TabIndex = 1;
            this.btnSelectNone.Text = "Select None";
            this.btnSelectNone.UseVisualStyleBackColor = true;
            this.btnSelectNone.Click += new System.EventHandler(this.BtnSelectNone_Click);
            // 
            // btnPageDialog
            // 
            this.btnPageDialog.Location = new System.Drawing.Point(540, 10);
            this.btnPageDialog.Name = "btnPageDialog";
            this.btnPageDialog.Size = new System.Drawing.Size(58, 23);
            this.btnPageDialog.TabIndex = 1;
            this.btnPageDialog.Text = "Page Dialog";
            this.btnPageDialog.UseVisualStyleBackColor = true;
            this.btnPageDialog.Visible = false;
            this.btnPageDialog.Click += new System.EventHandler(this.btnPageDialog_Click);
            // 
            // btnAddSelected
            // 
            this.btnAddSelected.Location = new System.Drawing.Point(175, 8);
            this.btnAddSelected.Name = "btnAddSelected";
            this.btnAddSelected.Size = new System.Drawing.Size(106, 30);
            this.btnAddSelected.TabIndex = 1;
            this.btnAddSelected.Text = "Add Selected";
            this.btnAddSelected.UseVisualStyleBackColor = true;
            this.btnAddSelected.Click += new System.EventHandler(this.BtnAddSelected_Click);
            // 
            // chkAllPrintRate
            // 
            this.chkAllPrintRate.AutoSize = true;
            this.chkAllPrintRate.Location = new System.Drawing.Point(765, 15);
            this.chkAllPrintRate.Name = "chkAllPrintRate";
            this.chkAllPrintRate.Size = new System.Drawing.Size(15, 14);
            this.chkAllPrintRate.TabIndex = 3;
            this.chkAllPrintRate.UseVisualStyleBackColor = true;
            this.chkAllPrintRate.Visible = false;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(13, 8);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 30);
            this.btnSelectAll.TabIndex = 1;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.BtnSelectAll_Click);
            // 
            // chkAllPrintCompany
            // 
            this.chkAllPrintCompany.AutoSize = true;
            this.chkAllPrintCompany.Location = new System.Drawing.Point(696, 17);
            this.chkAllPrintCompany.Name = "chkAllPrintCompany";
            this.chkAllPrintCompany.Size = new System.Drawing.Size(15, 14);
            this.chkAllPrintCompany.TabIndex = 3;
            this.chkAllPrintCompany.UseVisualStyleBackColor = true;
            this.chkAllPrintCompany.Visible = false;
            // 
            // chkAllPrintCode
            // 
            this.chkAllPrintCode.AutoSize = true;
            this.chkAllPrintCode.Location = new System.Drawing.Point(744, 15);
            this.chkAllPrintCode.Name = "chkAllPrintCode";
            this.chkAllPrintCode.Size = new System.Drawing.Size(15, 14);
            this.chkAllPrintCode.TabIndex = 3;
            this.chkAllPrintCode.UseVisualStyleBackColor = true;
            this.chkAllPrintCode.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(1616, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 30);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnClearList
            // 
            this.btnClearList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearList.Location = new System.Drawing.Point(1530, 8);
            this.btnClearList.Name = "btnClearList";
            this.btnClearList.Size = new System.Drawing.Size(80, 30);
            this.btnClearList.TabIndex = 0;
            this.btnClearList.Text = "ClearControl List";
            this.btnClearList.UseVisualStyleBackColor = true;
            this.btnClearList.Click += new System.EventHandler(this.BtnClearList_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreview.Location = new System.Drawing.Point(1403, 8);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(121, 30);
            this.btnPreview.TabIndex = 0;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.BtnPreview_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gridProducts);
            this.splitContainer1.Panel1.Controls.Add(this.panel3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gridBarcodes);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(1708, 707);
            this.splitContainer1.SplitterDistance = 452;
            this.splitContainer1.TabIndex = 1;
            // 
            // gridProducts
            // 
            this.gridProducts.AllowUserToAddRows = false;
            this.gridProducts.AllowUserToDeleteRows = false;
            this.gridProducts.AllowUserToResizeRows = false;
            this.gridProducts.AutoGenerateColumns = false;
            this.gridProducts.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.gridProducts.BlockNavigationOnNextRowOnEnter = true;
            this.gridProducts.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProducts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheckBox,
            this.Column2,
            this.Column3,
            this.Column4,
            this.colAdd});
            this.gridProducts.DataSource = this.bsProducts;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Bookman Old Style", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridProducts.DefaultCellStyle = dataGridViewCellStyle1;
            this.gridProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridProducts.DoubleBufferEnabled = true;
            this.gridProducts.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.gridProducts.Location = new System.Drawing.Point(0, 30);
            this.gridProducts.MultiSelect = false;
            this.gridProducts.Name = "gridProducts";
            this.gridProducts.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Bookman Old Style", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProducts.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridProducts.RowHeadersVisible = false;
            this.gridProducts.RowHeadersWidth = 51;
            this.gridProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridProducts.Size = new System.Drawing.Size(452, 677);
            this.gridProducts.TabIndex = 2;
            this.gridProducts.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridProducts_CellContentClick);
            this.gridProducts.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GridProducts_CellFormatting);
            // 
            // colCheckBox
            // 
            this.colCheckBox.FalseValue = "false";
            this.colCheckBox.HeaderText = "";
            this.colCheckBox.MinimumWidth = 6;
            this.colCheckBox.Name = "colCheckBox";
            this.colCheckBox.ReadOnly = true;
            this.colCheckBox.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colCheckBox.TrueValue = "true";
            this.colCheckBox.Width = 30;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.DataPropertyName = "ProductName";
            this.Column2.HeaderText = "Product";
            this.Column2.MinimumWidth = 120;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "ProductCategory";
            this.Column3.HeaderText = "Category";
            this.Column3.MinimumWidth = 120;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 120;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "ProductBarCode";
            this.Column4.HeaderText = "BarCode";
            this.Column4.MinimumWidth = 100;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 125;
            // 
            // colAdd
            // 
            this.colAdd.HeaderText = "";
            this.colAdd.MinimumWidth = 6;
            this.colAdd.Name = "colAdd";
            this.colAdd.ReadOnly = true;
            this.colAdd.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colAdd.Text = "";
            this.colAdd.Width = 40;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtSearchProduct);
            this.panel3.Controls.Add(this.cbxProductSource);
            this.panel3.Controls.Add(this.label21);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(452, 30);
            this.panel3.TabIndex = 3;
            // 
            // txtSearchProduct
            // 
            this.txtSearchProduct.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchProduct.Location = new System.Drawing.Point(175, 5);
            this.txtSearchProduct.MaxLength = 50;
            this.txtSearchProduct.Name = "txtSearchProduct";
            this.txtSearchProduct.Size = new System.Drawing.Size(274, 21);
            this.txtSearchProduct.TabIndex = 0;
            this.txtSearchProduct.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchProduct_KeyPress);
            // 
            // cbxProductSource
            // 
            this.cbxProductSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxProductSource.FormattingEnabled = true;
            this.cbxProductSource.Location = new System.Drawing.Point(44, 5);
            this.cbxProductSource.Name = "cbxProductSource";
            this.cbxProductSource.Size = new System.Drawing.Size(125, 21);
            this.cbxProductSource.TabIndex = 0;
            this.cbxProductSource.SelectedIndexChanged += new System.EventHandler(this.cbxProductSource_SelectedIndexChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Location = new System.Drawing.Point(3, 9);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(40, 13);
            this.label21.TabIndex = 1;
            this.label21.Text = "Source";
            // 
            // gridBarcodes
            // 
            this.gridBarcodes.AllowUserToAddRows = false;
            this.gridBarcodes.AllowUserToDeleteRows = false;
            this.gridBarcodes.AllowUserToResizeRows = false;
            this.gridBarcodes.AutoGenerateColumns = false;
            this.gridBarcodes.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.gridBarcodes.BlockNavigationOnNextRowOnEnter = true;
            this.gridBarcodes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridBarcodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridBarcodes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column5,
            this.Column8,
            this.clmPrintedBarCode,
            this.Column6,
            this.colPrintCode,
            this.colPrintCompany,
            this.colPrintProductName,
            this.colPrintSalesRate,
            this.colTextType,
            this.colTextAlignment,
            this.colDelete});
            this.gridBarcodes.DataSource = this.bsBarcodes;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Bookman Old Style", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridBarcodes.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridBarcodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridBarcodes.DoubleBufferEnabled = true;
            this.gridBarcodes.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.gridBarcodes.Location = new System.Drawing.Point(0, 30);
            this.gridBarcodes.MultiSelect = false;
            this.gridBarcodes.Name = "gridBarcodes";
            this.gridBarcodes.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Bookman Old Style", 9F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridBarcodes.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gridBarcodes.RowHeadersVisible = false;
            this.gridBarcodes.RowHeadersWidth = 51;
            this.gridBarcodes.Size = new System.Drawing.Size(1252, 457);
            this.gridBarcodes.TabIndex = 1;
            this.gridBarcodes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridBarcode_CellContentClick);
            this.gridBarcodes.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GridBarcode_CellFormatting);
            this.gridBarcodes.LocationChanged += new System.EventHandler(this.gridBarcodes_SizeChanged);
            this.gridBarcodes.SizeChanged += new System.EventHandler(this.gridBarcodes_SizeChanged);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.DataPropertyName = "ProductName";
            this.Column1.HeaderText = "Product";
            this.Column1.MinimumWidth = 120;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "ProductCategory";
            this.Column5.HeaderText = "Category";
            this.Column5.MinimumWidth = 120;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 120;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "ProductBarCode";
            this.Column8.HeaderText = "Bar Code";
            this.Column8.MinimumWidth = 120;
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 120;
            // 
            // clmPrintedBarCode
            // 
            this.clmPrintedBarCode.DataPropertyName = "PrintedBarCode";
            this.clmPrintedBarCode.HeaderText = "PrintedBarCode";
            this.clmPrintedBarCode.MinimumWidth = 6;
            this.clmPrintedBarCode.Name = "clmPrintedBarCode";
            this.clmPrintedBarCode.ReadOnly = true;
            this.clmPrintedBarCode.Width = 125;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "PrintCount";
            this.Column6.HeaderText = "Copies";
            this.Column6.MinimumWidth = 80;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 80;
            // 
            // colPrintCode
            // 
            this.colPrintCode.DataPropertyName = "PrintText";
            this.colPrintCode.HeaderText = "Print Code";
            this.colPrintCode.MinimumWidth = 70;
            this.colPrintCode.Name = "colPrintCode";
            this.colPrintCode.ReadOnly = true;
            this.colPrintCode.Width = 70;
            // 
            // colPrintCompany
            // 
            this.colPrintCompany.DataPropertyName = "PrintCompanyName";
            this.colPrintCompany.HeaderText = "Print Company";
            this.colPrintCompany.MinimumWidth = 6;
            this.colPrintCompany.Name = "colPrintCompany";
            this.colPrintCompany.ReadOnly = true;
            this.colPrintCompany.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colPrintCompany.Width = 85;
            // 
            // colPrintProductName
            // 
            this.colPrintProductName.DataPropertyName = "PrintProductName";
            this.colPrintProductName.HeaderText = "Print Name";
            this.colPrintProductName.MinimumWidth = 6;
            this.colPrintProductName.Name = "colPrintProductName";
            this.colPrintProductName.ReadOnly = true;
            this.colPrintProductName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colPrintProductName.Width = 70;
            // 
            // colPrintSalesRate
            // 
            this.colPrintSalesRate.DataPropertyName = "PrintSalesRate";
            this.colPrintSalesRate.HeaderText = "Print Rate";
            this.colPrintSalesRate.MinimumWidth = 6;
            this.colPrintSalesRate.Name = "colPrintSalesRate";
            this.colPrintSalesRate.ReadOnly = true;
            this.colPrintSalesRate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colPrintSalesRate.Width = 70;
            // 
            // colTextType
            // 
            this.colTextType.DataPropertyName = "PrintTextType";
            this.colTextType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colTextType.HeaderText = "Text Type";
            this.colTextType.MinimumWidth = 100;
            this.colTextType.Name = "colTextType";
            this.colTextType.ReadOnly = true;
            this.colTextType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colTextType.Visible = false;
            this.colTextType.Width = 125;
            // 
            // colTextAlignment
            // 
            this.colTextAlignment.DataPropertyName = "TextAlignment";
            this.colTextAlignment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colTextAlignment.HeaderText = "Text Alignment";
            this.colTextAlignment.MinimumWidth = 110;
            this.colTextAlignment.Name = "colTextAlignment";
            this.colTextAlignment.ReadOnly = true;
            this.colTextAlignment.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colTextAlignment.Width = 110;
            // 
            // colDelete
            // 
            this.colDelete.HeaderText = "";
            this.colDelete.MinimumWidth = 50;
            this.colDelete.Name = "colDelete";
            this.colDelete.ReadOnly = true;
            this.colDelete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colDelete.Width = 50;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Gainsboro;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1252, 30);
            this.label1.TabIndex = 2;
            this.label1.Text = "Barcode To Print";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 487);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1252, 220);
            this.panel2.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.nudMarginBottom);
            this.groupBox2.Controls.Add(this.nudMarginRight);
            this.groupBox2.Controls.Add(this.nudMarginTop);
            this.groupBox2.Controls.Add(this.nudPaperWidth);
            this.groupBox2.Controls.Add(this.nudPaperHeight);
            this.groupBox2.Controls.Add(this.nudMarginLeft);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.cbxPrinters);
            this.groupBox2.Controls.Add(this.cbxPaperKind);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(299, 220);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Printer Settings";
            // 
            // label20
            // 
            this.label20.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label20.Location = new System.Drawing.Point(69, 146);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(220, 2);
            this.label20.TabIndex = 4;
            this.label20.Text = "label20";
            // 
            // label19
            // 
            this.label19.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label19.Location = new System.Drawing.Point(65, 61);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(220, 2);
            this.label19.TabIndex = 3;
            this.label19.Text = "label19";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(179, 80);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(35, 13);
            this.label18.TabIndex = 1;
            this.label18.Text = "Width";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(76, 80);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(38, 13);
            this.label17.TabIndex = 1;
            this.label17.Text = "Height";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Sticker Size(mm)";
            // 
            // nudMarginBottom
            // 
            this.nudMarginBottom.Location = new System.Drawing.Point(220, 181);
            this.nudMarginBottom.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudMarginBottom.Name = "nudMarginBottom";
            this.nudMarginBottom.Size = new System.Drawing.Size(60, 21);
            this.nudMarginBottom.TabIndex = 2;
            this.nudMarginBottom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // nudMarginRight
            // 
            this.nudMarginRight.Location = new System.Drawing.Point(220, 154);
            this.nudMarginRight.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudMarginRight.Name = "nudMarginRight";
            this.nudMarginRight.Size = new System.Drawing.Size(60, 21);
            this.nudMarginRight.TabIndex = 2;
            this.nudMarginRight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // nudMarginTop
            // 
            this.nudMarginTop.Location = new System.Drawing.Point(115, 181);
            this.nudMarginTop.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudMarginTop.Name = "nudMarginTop";
            this.nudMarginTop.Size = new System.Drawing.Size(60, 21);
            this.nudMarginTop.TabIndex = 2;
            this.nudMarginTop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // nudPaperWidth
            // 
            this.nudPaperWidth.Location = new System.Drawing.Point(220, 78);
            this.nudPaperWidth.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudPaperWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPaperWidth.Name = "nudPaperWidth";
            this.nudPaperWidth.Size = new System.Drawing.Size(60, 21);
            this.nudPaperWidth.TabIndex = 2;
            this.nudPaperWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudPaperWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudPaperHeight
            // 
            this.nudPaperHeight.Location = new System.Drawing.Point(115, 77);
            this.nudPaperHeight.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudPaperHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPaperHeight.Name = "nudPaperHeight";
            this.nudPaperHeight.Size = new System.Drawing.Size(60, 21);
            this.nudPaperHeight.TabIndex = 2;
            this.nudPaperHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudPaperHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudMarginLeft
            // 
            this.nudMarginLeft.Location = new System.Drawing.Point(115, 154);
            this.nudMarginLeft.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudMarginLeft.Name = "nudMarginLeft";
            this.nudMarginLeft.Size = new System.Drawing.Size(60, 21);
            this.nudMarginLeft.TabIndex = 2;
            this.nudMarginLeft.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(181, 185);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(41, 13);
            this.label16.TabIndex = 1;
            this.label16.Text = "Bottom";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Paper Kind";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(88, 185);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(25, 13);
            this.label15.TabIndex = 1;
            this.label15.Text = "Top";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(181, 158);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(32, 13);
            this.label14.TabIndex = 1;
            this.label14.Text = "Right";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(88, 158);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(26, 13);
            this.label13.TabIndex = 1;
            this.label13.Text = "Left";
            // 
            // cbxPrinters
            // 
            this.cbxPrinters.FormattingEnabled = true;
            this.cbxPrinters.Location = new System.Drawing.Point(79, 106);
            this.cbxPrinters.Name = "cbxPrinters";
            this.cbxPrinters.Size = new System.Drawing.Size(210, 21);
            this.cbxPrinters.TabIndex = 0;
            // 
            // cbxPaperKind
            // 
            this.cbxPaperKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPaperKind.FormattingEnabled = true;
            this.cbxPaperKind.Location = new System.Drawing.Point(79, 20);
            this.cbxPaperKind.Name = "cbxPaperKind";
            this.cbxPaperKind.Size = new System.Drawing.Size(210, 21);
            this.cbxPaperKind.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Margins";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Printer";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudAutoFitPageWidth);
            this.groupBox1.Controls.Add(this.chkFillBarSpace);
            this.groupBox1.Controls.Add(this.chkDrawBorder);
            this.groupBox1.Controls.Add(this.chkAutoFitPageWidth);
            this.groupBox1.Controls.Add(this.chkAutoBarModule);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.nudColumnSpacing);
            this.groupBox1.Controls.Add(this.nudBarModule);
            this.groupBox1.Controls.Add(this.nudRowHeight);
            this.groupBox1.Controls.Add(this.nudColumnWidth);
            this.groupBox1.Controls.Add(this.nudColumnCount);
            this.groupBox1.Controls.Add(this.tlpFontSettings);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.btnSaveConfig);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.cbxSymbology);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(308, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(935, 220);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Barcode Settings";
            // 
            // nudAutoFitPageWidth
            // 
            this.nudAutoFitPageWidth.Location = new System.Drawing.Point(426, 171);
            this.nudAutoFitPageWidth.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudAutoFitPageWidth.Name = "nudAutoFitPageWidth";
            this.nudAutoFitPageWidth.Size = new System.Drawing.Size(43, 21);
            this.nudAutoFitPageWidth.TabIndex = 2;
            this.nudAutoFitPageWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkFillBarSpace
            // 
            this.chkFillBarSpace.AutoSize = true;
            this.chkFillBarSpace.Checked = true;
            this.chkFillBarSpace.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFillBarSpace.Location = new System.Drawing.Point(293, 192);
            this.chkFillBarSpace.Name = "chkFillBarSpace";
            this.chkFillBarSpace.Size = new System.Drawing.Size(88, 17);
            this.chkFillBarSpace.TabIndex = 3;
            this.chkFillBarSpace.Text = "Fill bar space";
            this.chkFillBarSpace.UseVisualStyleBackColor = true;
            // 
            // chkDrawBorder
            // 
            this.chkDrawBorder.AutoSize = true;
            this.chkDrawBorder.Checked = true;
            this.chkDrawBorder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDrawBorder.Location = new System.Drawing.Point(100, 192);
            this.chkDrawBorder.Name = "chkDrawBorder";
            this.chkDrawBorder.Size = new System.Drawing.Size(86, 17);
            this.chkDrawBorder.TabIndex = 3;
            this.chkDrawBorder.Text = "Draw border";
            this.chkDrawBorder.UseVisualStyleBackColor = true;
            // 
            // chkAutoFitPageWidth
            // 
            this.chkAutoFitPageWidth.BackColor = System.Drawing.Color.Transparent;
            this.chkAutoFitPageWidth.Checked = true;
            this.chkAutoFitPageWidth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoFitPageWidth.Location = new System.Drawing.Point(293, 173);
            this.chkAutoFitPageWidth.Name = "chkAutoFitPageWidth";
            this.chkAutoFitPageWidth.Size = new System.Drawing.Size(139, 17);
            this.chkAutoFitPageWidth.TabIndex = 3;
            this.chkAutoFitPageWidth.Text = "Auto fit to pages width";
            this.chkAutoFitPageWidth.UseVisualStyleBackColor = false;
            // 
            // chkAutoBarModule
            // 
            this.chkAutoBarModule.AutoSize = true;
            this.chkAutoBarModule.Checked = true;
            this.chkAutoBarModule.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoBarModule.Location = new System.Drawing.Point(100, 173);
            this.chkAutoBarModule.Name = "chkAutoBarModule";
            this.chkAutoBarModule.Size = new System.Drawing.Size(138, 17);
            this.chkAutoBarModule.TabIndex = 3;
            this.chkAutoBarModule.Text = "Auto adjust bar module";
            this.chkAutoBarModule.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Row Height";
            // 
            // nudColumnSpacing
            // 
            this.nudColumnSpacing.DecimalPlaces = 2;
            this.nudColumnSpacing.Location = new System.Drawing.Point(100, 124);
            this.nudColumnSpacing.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudColumnSpacing.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudColumnSpacing.Name = "nudColumnSpacing";
            this.nudColumnSpacing.Size = new System.Drawing.Size(171, 21);
            this.nudColumnSpacing.TabIndex = 2;
            this.nudColumnSpacing.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudColumnSpacing.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // nudBarModule
            // 
            this.nudBarModule.DecimalPlaces = 2;
            this.nudBarModule.Location = new System.Drawing.Point(100, 150);
            this.nudBarModule.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudBarModule.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBarModule.Name = "nudBarModule";
            this.nudBarModule.Size = new System.Drawing.Size(171, 21);
            this.nudBarModule.TabIndex = 2;
            this.nudBarModule.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudBarModule.Value = new decimal(new int[] {
            508,
            0,
            0,
            131072});
            // 
            // nudRowHeight
            // 
            this.nudRowHeight.DecimalPlaces = 4;
            this.nudRowHeight.Location = new System.Drawing.Point(100, 72);
            this.nudRowHeight.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudRowHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRowHeight.Name = "nudRowHeight";
            this.nudRowHeight.Size = new System.Drawing.Size(171, 21);
            this.nudRowHeight.TabIndex = 2;
            this.nudRowHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudRowHeight.Value = new decimal(new int[] {
            2475418,
            0,
            0,
            262144});
            // 
            // nudColumnWidth
            // 
            this.nudColumnWidth.Location = new System.Drawing.Point(100, 98);
            this.nudColumnWidth.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudColumnWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudColumnWidth.Name = "nudColumnWidth";
            this.nudColumnWidth.Size = new System.Drawing.Size(171, 21);
            this.nudColumnWidth.TabIndex = 2;
            this.nudColumnWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudColumnWidth.Value = new decimal(new int[] {
            460,
            0,
            0,
            0});
            // 
            // nudColumnCount
            // 
            this.nudColumnCount.Location = new System.Drawing.Point(100, 46);
            this.nudColumnCount.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudColumnCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudColumnCount.Name = "nudColumnCount";
            this.nudColumnCount.Size = new System.Drawing.Size(171, 21);
            this.nudColumnCount.TabIndex = 2;
            this.nudColumnCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudColumnCount.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // tlpFontSettings
            // 
            this.tlpFontSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpFontSettings.AutoSize = true;
            this.tlpFontSettings.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tlpFontSettings.ColumnCount = 6;
            this.tlpFontSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.tlpFontSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFontSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.tlpFontSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tlpFontSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 205F));
            this.tlpFontSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 168F));
            this.tlpFontSettings.Controls.Add(this.chkBoxPrintSalesRate, 3, 2);
            this.tlpFontSettings.Controls.Add(this.chkBoxPrintProductName, 3, 1);
            this.tlpFontSettings.Controls.Add(this.label22, 0, 0);
            this.tlpFontSettings.Controls.Add(this.label23, 0, 1);
            this.tlpFontSettings.Controls.Add(this.label24, 0, 2);
            this.tlpFontSettings.Controls.Add(this.lblCompanyFont, 1, 0);
            this.tlpFontSettings.Controls.Add(this.llSalesRate, 2, 2);
            this.tlpFontSettings.Controls.Add(this.lblProductFont, 1, 1);
            this.tlpFontSettings.Controls.Add(this.llProductNameFont, 2, 1);
            this.tlpFontSettings.Controls.Add(this.lblSalesRateFont, 1, 2);
            this.tlpFontSettings.Controls.Add(this.llCompanyFont, 2, 0);
            this.tlpFontSettings.Controls.Add(this.chkBoxPrintCompany, 3, 0);
            this.tlpFontSettings.Controls.Add(this.label25, 0, 3);
            this.tlpFontSettings.Controls.Add(this.chkBoxPrintBarcode, 3, 3);
            this.tlpFontSettings.Controls.Add(this.txtBoxCompany, 4, 0);
            this.tlpFontSettings.Controls.Add(this.cmbSelectedBarCode, 4, 3);
            this.tlpFontSettings.Controls.Add(this.cmbCompAlign, 5, 0);
            this.tlpFontSettings.Controls.Add(this.cmbProductAlign, 5, 1);
            this.tlpFontSettings.Controls.Add(this.cmbRateAlign, 5, 2);
            this.tlpFontSettings.Controls.Add(this.cmbBarcodeAlign, 5, 3);
            this.tlpFontSettings.Controls.Add(this.cmbRateOptions, 4, 2);
            this.tlpFontSettings.Location = new System.Drawing.Point(293, 33);
            this.tlpFontSettings.Name = "tlpFontSettings";
            this.tlpFontSettings.RowCount = 4;
            this.tlpFontSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpFontSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tlpFontSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpFontSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tlpFontSettings.Size = new System.Drawing.Size(636, 132);
            this.tlpFontSettings.TabIndex = 1;
            this.tlpFontSettings.Text = "Sales Rate";
            // 
            // chkBoxPrintSalesRate
            // 
            this.chkBoxPrintSalesRate.AutoSize = true;
            this.chkBoxPrintSalesRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkBoxPrintSalesRate.Location = new System.Drawing.Point(195, 74);
            this.chkBoxPrintSalesRate.Name = "chkBoxPrintSalesRate";
            this.chkBoxPrintSalesRate.Size = new System.Drawing.Size(59, 22);
            this.chkBoxPrintSalesRate.TabIndex = 4;
            this.chkBoxPrintSalesRate.Text = "Print";
            this.chkBoxPrintSalesRate.UseVisualStyleBackColor = true;
            this.chkBoxPrintSalesRate.CheckedChanged += new System.EventHandler(this.chkBoxPrintSalesRate_CheckedChanged);
            // 
            // chkBoxPrintProductName
            // 
            this.chkBoxPrintProductName.AutoSize = true;
            this.chkBoxPrintProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkBoxPrintProductName.Location = new System.Drawing.Point(195, 43);
            this.chkBoxPrintProductName.Name = "chkBoxPrintProductName";
            this.chkBoxPrintProductName.Size = new System.Drawing.Size(59, 23);
            this.chkBoxPrintProductName.TabIndex = 3;
            this.chkBoxPrintProductName.Text = "Print";
            this.chkBoxPrintProductName.UseVisualStyleBackColor = true;
            this.chkBoxPrintProductName.CheckedChanged += new System.EventHandler(this.chkBoxPrintProductName_CheckedChanged);
            // 
            // label22
            // 
            this.label22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label22.Location = new System.Drawing.Point(5, 2);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(110, 36);
            this.label22.TabIndex = 1;
            this.label22.Text = "Company Name";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label23
            // 
            this.label23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label23.Location = new System.Drawing.Point(5, 40);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(110, 29);
            this.label23.TabIndex = 1;
            this.label23.Text = "Product Name";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label24
            // 
            this.label24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label24.Location = new System.Drawing.Point(5, 71);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(110, 28);
            this.label24.TabIndex = 1;
            this.label24.Text = "Sales Rate";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCompanyFont
            // 
            this.lblCompanyFont.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCompanyFont.Location = new System.Drawing.Point(123, 2);
            this.lblCompanyFont.Name = "lblCompanyFont";
            this.lblCompanyFont.Size = new System.Drawing.Size(1, 36);
            this.lblCompanyFont.TabIndex = 1;
            this.lblCompanyFont.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // llSalesRate
            // 
            this.llSalesRate.AutoSize = true;
            this.llSalesRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.llSalesRate.Location = new System.Drawing.Point(126, 71);
            this.llSalesRate.Name = "llSalesRate";
            this.llSalesRate.Size = new System.Drawing.Size(61, 28);
            this.llSalesRate.TabIndex = 1;
            this.llSalesRate.TabStop = true;
            this.llSalesRate.Text = "Change";
            this.llSalesRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.llSalesRate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llSalesRate_LinkClicked);
            // 
            // lblProductFont
            // 
            this.lblProductFont.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductFont.Location = new System.Drawing.Point(123, 40);
            this.lblProductFont.Name = "lblProductFont";
            this.lblProductFont.Size = new System.Drawing.Size(1, 29);
            this.lblProductFont.TabIndex = 1;
            this.lblProductFont.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // llProductNameFont
            // 
            this.llProductNameFont.AutoSize = true;
            this.llProductNameFont.Dock = System.Windows.Forms.DockStyle.Fill;
            this.llProductNameFont.Location = new System.Drawing.Point(126, 40);
            this.llProductNameFont.Name = "llProductNameFont";
            this.llProductNameFont.Size = new System.Drawing.Size(61, 29);
            this.llProductNameFont.TabIndex = 1;
            this.llProductNameFont.TabStop = true;
            this.llProductNameFont.Text = "Change";
            this.llProductNameFont.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.llProductNameFont.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llProductNameFont_LinkClicked);
            // 
            // lblSalesRateFont
            // 
            this.lblSalesRateFont.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSalesRateFont.Location = new System.Drawing.Point(123, 71);
            this.lblSalesRateFont.Name = "lblSalesRateFont";
            this.lblSalesRateFont.Size = new System.Drawing.Size(1, 28);
            this.lblSalesRateFont.TabIndex = 1;
            this.lblSalesRateFont.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // llCompanyFont
            // 
            this.llCompanyFont.AutoSize = true;
            this.llCompanyFont.Dock = System.Windows.Forms.DockStyle.Fill;
            this.llCompanyFont.Location = new System.Drawing.Point(126, 2);
            this.llCompanyFont.Name = "llCompanyFont";
            this.llCompanyFont.Size = new System.Drawing.Size(61, 36);
            this.llCompanyFont.TabIndex = 1;
            this.llCompanyFont.TabStop = true;
            this.llCompanyFont.Text = "Change";
            this.llCompanyFont.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.llCompanyFont.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llCompanyFont_LinkClicked);
            // 
            // chkBoxPrintCompany
            // 
            this.chkBoxPrintCompany.AutoSize = true;
            this.chkBoxPrintCompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkBoxPrintCompany.Location = new System.Drawing.Point(195, 5);
            this.chkBoxPrintCompany.Name = "chkBoxPrintCompany";
            this.chkBoxPrintCompany.Size = new System.Drawing.Size(59, 30);
            this.chkBoxPrintCompany.TabIndex = 2;
            this.chkBoxPrintCompany.Text = "Print";
            this.chkBoxPrintCompany.UseVisualStyleBackColor = true;
            this.chkBoxPrintCompany.CheckedChanged += new System.EventHandler(this.chkBoxPrintCompany_CheckedChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label25.Location = new System.Drawing.Point(5, 101);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(110, 29);
            this.label25.TabIndex = 5;
            this.label25.Text = "Barcode";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkBoxPrintBarcode
            // 
            this.chkBoxPrintBarcode.AutoSize = true;
            this.chkBoxPrintBarcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkBoxPrintBarcode.Location = new System.Drawing.Point(195, 104);
            this.chkBoxPrintBarcode.Name = "chkBoxPrintBarcode";
            this.chkBoxPrintBarcode.Size = new System.Drawing.Size(59, 23);
            this.chkBoxPrintBarcode.TabIndex = 6;
            this.chkBoxPrintBarcode.Text = "Print";
            this.chkBoxPrintBarcode.UseVisualStyleBackColor = true;
            this.chkBoxPrintBarcode.CheckedChanged += new System.EventHandler(this.chkBoxPrintBarcode_CheckedChanged);
            // 
            // txtBoxCompany
            // 
            this.txtBoxCompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBoxCompany.Location = new System.Drawing.Point(261, 4);
            this.txtBoxCompany.Margin = new System.Windows.Forms.Padding(2);
            this.txtBoxCompany.Multiline = true;
            this.txtBoxCompany.Name = "txtBoxCompany";
            this.txtBoxCompany.Size = new System.Drawing.Size(201, 32);
            this.txtBoxCompany.TabIndex = 7;
            this.txtBoxCompany.WordWrap = false;
            this.txtBoxCompany.TextChanged += new System.EventHandler(this.txtBoxCompany_TextChanged);
            // 
            // cmbSelectedBarCode
            // 
            this.cmbSelectedBarCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSelectedBarCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectedBarCode.FormattingEnabled = true;
            this.cmbSelectedBarCode.Location = new System.Drawing.Point(262, 104);
            this.cmbSelectedBarCode.Name = "cmbSelectedBarCode";
            this.cmbSelectedBarCode.Size = new System.Drawing.Size(199, 21);
            this.cmbSelectedBarCode.TabIndex = 8;
            this.cmbSelectedBarCode.SelectedIndexChanged += new System.EventHandler(this.cmbSelectedBarCode_SelectedIndexChanged);
            // 
            // cmbCompAlign
            // 
            this.cmbCompAlign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCompAlign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompAlign.FormattingEnabled = true;
            this.cmbCompAlign.Items.AddRange(new object[] {
            "Left",
            "Center",
            "Right"});
            this.cmbCompAlign.Location = new System.Drawing.Point(469, 5);
            this.cmbCompAlign.Name = "cmbCompAlign";
            this.cmbCompAlign.Size = new System.Drawing.Size(162, 21);
            this.cmbCompAlign.TabIndex = 9;
            this.cmbCompAlign.SelectedIndexChanged += new System.EventHandler(this.cmbCompAlign_SelectedIndexChanged);
            // 
            // cmbProductAlign
            // 
            this.cmbProductAlign.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbProductAlign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProductAlign.FormattingEnabled = true;
            this.cmbProductAlign.Items.AddRange(new object[] {
            "Left",
            "Center",
            "Right"});
            this.cmbProductAlign.Location = new System.Drawing.Point(469, 43);
            this.cmbProductAlign.Name = "cmbProductAlign";
            this.cmbProductAlign.Size = new System.Drawing.Size(162, 21);
            this.cmbProductAlign.TabIndex = 10;
            this.cmbProductAlign.SelectedIndexChanged += new System.EventHandler(this.cmbProductAlign_SelectedIndexChanged);
            // 
            // cmbRateAlign
            // 
            this.cmbRateAlign.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbRateAlign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRateAlign.FormattingEnabled = true;
            this.cmbRateAlign.Items.AddRange(new object[] {
            "Left",
            "Center",
            "Right"});
            this.cmbRateAlign.Location = new System.Drawing.Point(469, 74);
            this.cmbRateAlign.Name = "cmbRateAlign";
            this.cmbRateAlign.Size = new System.Drawing.Size(162, 21);
            this.cmbRateAlign.TabIndex = 11;
            this.cmbRateAlign.SelectedIndexChanged += new System.EventHandler(this.cmbRateAlign_SelectedIndexChanged);
            // 
            // cmbBarcodeAlign
            // 
            this.cmbBarcodeAlign.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbBarcodeAlign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBarcodeAlign.FormattingEnabled = true;
            this.cmbBarcodeAlign.Items.AddRange(new object[] {
            "Left",
            "Center",
            "Right"});
            this.cmbBarcodeAlign.Location = new System.Drawing.Point(469, 104);
            this.cmbBarcodeAlign.Name = "cmbBarcodeAlign";
            this.cmbBarcodeAlign.Size = new System.Drawing.Size(162, 21);
            this.cmbBarcodeAlign.TabIndex = 12;
            this.cmbBarcodeAlign.SelectedIndexChanged += new System.EventHandler(this.cmbBarcodeAlign_SelectedIndexChanged);
            // 
            // cmbRateOptions
            // 
            this.cmbRateOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbRateOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRateOptions.FormattingEnabled = true;
            this.cmbRateOptions.Items.AddRange(new object[] {
            "Rate",
            "RateWithCode"});
            this.cmbRateOptions.Location = new System.Drawing.Point(262, 74);
            this.cmbRateOptions.Name = "cmbRateOptions";
            this.cmbRateOptions.Size = new System.Drawing.Size(199, 21);
            this.cmbRateOptions.TabIndex = 13;
            this.cmbRateOptions.SelectedIndexChanged += new System.EventHandler(this.cmbRateOptions_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 154);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 13);
            this.label12.TabIndex = 1;
            this.label12.Text = "Bar Module";
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveConfig.Location = new System.Drawing.Point(771, 168);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(158, 41);
            this.btnSaveConfig.TabIndex = 0;
            this.btnSaveConfig.Text = "Save Configuration";
            this.btnSaveConfig.UseVisualStyleBackColor = true;
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Symbology";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 126);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Column Spacing";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 100);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Column Width";
            // 
            // cbxSymbology
            // 
            this.cbxSymbology.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSymbology.FormattingEnabled = true;
            this.cbxSymbology.Location = new System.Drawing.Point(100, 20);
            this.cbxSymbology.Name = "cbxSymbology";
            this.cbxSymbology.Size = new System.Drawing.Size(171, 21);
            this.cbxSymbology.TabIndex = 0;
            this.cbxSymbology.SelectedIndexChanged += new System.EventHandler(this.cbxSymbology_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Column Count";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(290, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Font Settings";
            // 
            // FrmBarCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1708, 754);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmBarCode";
            this.ShowIcon = false;
            this.Text = "Barcode Print";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmBarCode_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProducts)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBarcodes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsBarcodes)).EndInit();
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMarginBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMarginRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMarginTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPaperWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPaperHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMarginLeft)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAutoFitPageWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudColumnSpacing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBarModule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRowHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudColumnWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudColumnCount)).EndInit();
            this.tlpFontSettings.ResumeLayout(false);
            this.tlpFontSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.BindingSource bsProducts;
        private System.Windows.Forms.TextBox txtSearchProduct;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.BindingSource bsBarcodes;
        private System.Windows.Forms.Button btnSelectNone;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnAddSelected;
        private System.Windows.Forms.Button btnClearList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxPaperKind;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbxPrinters;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown nudMarginLeft;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown nudMarginBottom;
        private System.Windows.Forms.NumericUpDown nudMarginRight;
        private System.Windows.Forms.NumericUpDown nudMarginTop;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown nudBarModule;
        private System.Windows.Forms.NumericUpDown nudColumnCount;
        private System.Windows.Forms.NumericUpDown nudColumnSpacing;
        private System.Windows.Forms.NumericUpDown nudRowHeight;
        private System.Windows.Forms.NumericUpDown nudColumnWidth;
        private System.Windows.Forms.ComboBox cbxSymbology;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudPaperWidth;
        private System.Windows.Forms.NumericUpDown nudPaperHeight;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.CheckBox chkAutoBarModule;
        private System.Windows.Forms.CheckBox chkDrawBorder;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewButtonColumn colAdd;
        private System.Windows.Forms.CheckBox chkAllPrintName;
        private System.Windows.Forms.CheckBox chkAllPrintRate;
        private System.Windows.Forms.CheckBox chkAllPrintCompany;
        private System.Windows.Forms.CheckBox chkAllPrintCode;
        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.Button btnPageDialog;
        private System.Windows.Forms.LinkLabel llCompanyFont;
        private System.Windows.Forms.TableLayoutPanel tlpFontSettings;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label lblCompanyFont;
        private System.Windows.Forms.LinkLabel llSalesRate;
        private System.Windows.Forms.Label lblProductFont;
        private System.Windows.Forms.LinkLabel llProductNameFont;
        private System.Windows.Forms.Label lblSalesRateFont;
        private System.Windows.Forms.CheckBox chkAutoFitPageWidth;
        private System.Windows.Forms.NumericUpDown nudAutoFitPageWidth;
        private System.Windows.Forms.CheckBox chkFillBarSpace;
        private System.Windows.Forms.ComboBox cbxProductSource;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label21;
        private CheckBox chkBoxPrintSalesRate;
        private CheckBox chkBoxPrintProductName;
        private CheckBox chkBoxPrintCompany;
        private EntryGridViewEx gridProducts;
        private EntryGridViewEx gridBarcodes;
        private Label label25;
        private CheckBox chkBoxPrintBarcode;
        private TextBox txtBoxCompany;
        private ComboBox cmbSelectedBarCode;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn clmPrintedBarCode;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewCheckBoxColumn colPrintCode;
        private DataGridViewCheckBoxColumn colPrintCompany;
        private DataGridViewCheckBoxColumn colPrintProductName;
        private DataGridViewCheckBoxColumn colPrintSalesRate;
        private DataGridViewComboBoxColumn colTextType;
        private DataGridViewComboBoxColumn colTextAlignment;
        private DataGridViewButtonColumn colDelete;
        private ComboBox cmbCompAlign;
        private ComboBox cmbProductAlign;
        private ComboBox cmbRateAlign;
        private ComboBox cmbBarcodeAlign;
        private ComboBox cmbRateOptions;
    }
}