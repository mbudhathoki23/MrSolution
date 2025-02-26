using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.DataEntry.StockMaster
{
    partial class FrmPhysicalStockEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPhysicalStockEntry));
            this.lbl_TotQty1 = new System.Windows.Forms.Label();
            this.LblTotalQty = new System.Windows.Forms.Label();
            this.LblTotalAmount = new System.Windows.Forms.Label();
            this.lbl_TotNetAmt1 = new System.Windows.Forms.Label();
            this.DGrid = new MrDAL.Control.ControlsEx.Control.EntryGridViewEx();
            this.LblNumInWords = new System.Windows.Forms.Label();
            this.lbl_InWords = new System.Windows.Forms.Label();
            this.BtnVno = new System.Windows.Forms.Button();
            this.BtnDepartment = new System.Windows.Forms.Button();
            this.MskMiti = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.MskDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.lbl_AdjustmentDate = new System.Windows.Forms.Label();
            this.TxtVNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_AdjustmentNo = new System.Windows.Forms.Label();
            this.TxtDepartment = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_Class = new System.Windows.Forms.Label();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.TxtRemarks = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_Remarks = new System.Windows.Forms.Label();
            this.btnCopy = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnReverse = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.mrMaskedTextBox1 = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PanelProduct = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.label23 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblSalesRate = new System.Windows.Forms.Label();
            this.lbl_MinQty = new System.Windows.Forms.Label();
            this.lblPurchaseRate = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.lblMrpRate = new System.Windows.Forms.Label();
            this.lbl_MaxQty = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.lblStockQty = new System.Windows.Forms.Label();
            this.lbStockAltQty = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.LblTotalAltQty = new System.Windows.Forms.Label();
            this.BtnRefVno = new System.Windows.Forms.Button();
            this.TxtRefVno = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.clsSeparator6 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparator5 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.TabProduct = new System.Windows.Forms.TabControl();
            this.TbInfomartion = new System.Windows.Forms.TabPage();
            this.TbDocument = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.PanelProduct.SuspendLayout();
            this.TabProduct.SuspendLayout();
            this.TbInfomartion.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_TotQty1
            // 
            this.lbl_TotQty1.AutoSize = true;
            this.lbl_TotQty1.Location = new System.Drawing.Point(477, 382);
            this.lbl_TotQty1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_TotQty1.Name = "lbl_TotQty1";
            this.lbl_TotQty1.Size = new System.Drawing.Size(82, 19);
            this.lbl_TotQty1.TabIndex = 122;
            this.lbl_TotQty1.Text = "Total  Qty";
            // 
            // LblTotalQty
            // 
            this.LblTotalQty.BackColor = System.Drawing.SystemColors.HighlightText;
            this.LblTotalQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTotalQty.Location = new System.Drawing.Point(563, 379);
            this.LblTotalQty.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblTotalQty.Name = "LblTotalQty";
            this.LblTotalQty.Size = new System.Drawing.Size(153, 25);
            this.LblTotalQty.TabIndex = 123;
            this.LblTotalQty.Text = "0.00";
            this.LblTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblTotalAmount
            // 
            this.LblTotalAmount.BackColor = System.Drawing.SystemColors.HighlightText;
            this.LblTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTotalAmount.Location = new System.Drawing.Point(833, 379);
            this.LblTotalAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblTotalAmount.Name = "LblTotalAmount";
            this.LblTotalAmount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LblTotalAmount.Size = new System.Drawing.Size(153, 25);
            this.LblTotalAmount.TabIndex = 129;
            this.LblTotalAmount.Text = "0.00";
            this.LblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_TotNetAmt1
            // 
            this.lbl_TotNetAmt1.AutoSize = true;
            this.lbl_TotNetAmt1.Location = new System.Drawing.Point(735, 382);
            this.lbl_TotNetAmt1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_TotNetAmt1.Name = "lbl_TotNetAmt1";
            this.lbl_TotNetAmt1.Size = new System.Drawing.Size(98, 19);
            this.lbl_TotNetAmt1.TabIndex = 128;
            this.lbl_TotNetAmt1.Text = "Net Amount";
            // 
            // DGrid
            // 
            this.DGrid.AllowUserToAddRows = false;
            this.DGrid.AllowUserToDeleteRows = false;
            this.DGrid.AllowUserToResizeColumns = false;
            this.DGrid.AllowUserToResizeRows = false;
            this.DGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.DGrid.BlockNavigationOnNextRowOnEnter = true;
            this.DGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Bookman Old Style", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGrid.DefaultCellStyle = dataGridViewCellStyle1;
            this.DGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGrid.DoubleBufferEnabled = true;
            this.DGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.DGrid.Location = new System.Drawing.Point(3, 3);
            this.DGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DGrid.MultiSelect = false;
            this.DGrid.Name = "DGrid";
            this.DGrid.ReadOnly = true;
            this.DGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Bookman Old Style", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DGrid.RowHeadersWidth = 10;
            this.DGrid.RowTemplate.Height = 24;
            this.DGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGrid.Size = new System.Drawing.Size(965, 163);
            this.DGrid.StandardTab = true;
            this.DGrid.TabIndex = 22;
            this.DGrid.EnterKeyPressed += new System.EventHandler<System.EventArgs>(this.DGrid_EnterKeyPressed);
            this.DGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DGrid_KeyDown);
            // 
            // LblNumInWords
            // 
            this.LblNumInWords.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.LblNumInWords.Location = new System.Drawing.Point(98, 451);
            this.LblNumInWords.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblNumInWords.Name = "LblNumInWords";
            this.LblNumInWords.Size = new System.Drawing.Size(525, 22);
            this.LblNumInWords.TabIndex = 131;
            this.LblNumInWords.Text = "Only.";
            // 
            // lbl_InWords
            // 
            this.lbl_InWords.AutoSize = true;
            this.lbl_InWords.Location = new System.Drawing.Point(9, 453);
            this.lbl_InWords.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_InWords.Name = "lbl_InWords";
            this.lbl_InWords.Size = new System.Drawing.Size(91, 19);
            this.lbl_InWords.TabIndex = 130;
            this.lbl_InWords.Text = "In Words :-";
            // 
            // BtnVno
            // 
            this.BtnVno.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.BtnVno.CausesValidation = false;
            this.BtnVno.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnVno.Location = new System.Drawing.Point(240, 45);
            this.BtnVno.Margin = new System.Windows.Forms.Padding(4);
            this.BtnVno.Name = "BtnVno";
            this.BtnVno.Size = new System.Drawing.Size(32, 26);
            this.BtnVno.TabIndex = 187;
            this.BtnVno.TabStop = false;
            this.BtnVno.UseVisualStyleBackColor = false;
            this.BtnVno.Click += new System.EventHandler(this.BtnVno_Click);
            // 
            // BtnDepartment
            // 
            this.BtnDepartment.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.BtnDepartment.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnDepartment.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnDepartment.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDepartment.Location = new System.Drawing.Point(426, 72);
            this.BtnDepartment.Margin = new System.Windows.Forms.Padding(4);
            this.BtnDepartment.Name = "BtnDepartment";
            this.BtnDepartment.Size = new System.Drawing.Size(31, 27);
            this.BtnDepartment.TabIndex = 186;
            this.BtnDepartment.TabStop = false;
            this.BtnDepartment.UseVisualStyleBackColor = false;
            this.BtnDepartment.Click += new System.EventHandler(this.BtnDepartment_Click);
            // 
            // MskMiti
            // 
            this.MskMiti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskMiti.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskMiti.Location = new System.Drawing.Point(312, 46);
            this.MskMiti.Margin = new System.Windows.Forms.Padding(4);
            this.MskMiti.Mask = "00/00/0000";
            this.MskMiti.Name = "MskMiti";
            this.MskMiti.Size = new System.Drawing.Size(110, 25);
            this.MskMiti.TabIndex = 8;
            this.MskMiti.Enter += new System.EventHandler(this.MskMiti_Enter);
            this.MskMiti.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyPress);
            this.MskMiti.Validating += new System.ComponentModel.CancelEventHandler(this.MskMiti_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(274, 49);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 19);
            this.label1.TabIndex = 185;
            this.label1.Text = "Miti";
            // 
            // MskDate
            // 
            this.MskDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskDate.Location = new System.Drawing.Point(467, 46);
            this.MskDate.Margin = new System.Windows.Forms.Padding(4);
            this.MskDate.Mask = "00/00/0000";
            this.MskDate.Name = "MskDate";
            this.MskDate.Size = new System.Drawing.Size(105, 25);
            this.MskDate.TabIndex = 9;
            this.MskDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyPress);
            this.MskDate.Validated += new System.EventHandler(this.MskDate_Validated);
            // 
            // lbl_AdjustmentDate
            // 
            this.lbl_AdjustmentDate.AutoSize = true;
            this.lbl_AdjustmentDate.Location = new System.Drawing.Point(422, 49);
            this.lbl_AdjustmentDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_AdjustmentDate.Name = "lbl_AdjustmentDate";
            this.lbl_AdjustmentDate.Size = new System.Drawing.Size(45, 19);
            this.lbl_AdjustmentDate.TabIndex = 183;
            this.lbl_AdjustmentDate.Text = "Date";
            // 
            // TxtVNo
            // 
            this.TxtVNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtVNo.Location = new System.Drawing.Point(105, 46);
            this.TxtVNo.Margin = new System.Windows.Forms.Padding(4);
            this.TxtVNo.MaxLength = 255;
            this.TxtVNo.Name = "TxtVNo";
            this.TxtVNo.Size = new System.Drawing.Size(133, 25);
            this.TxtVNo.TabIndex = 7;
            this.TxtVNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtVNo_KeyDown);
            this.TxtVNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyPress);
            this.TxtVNo.Leave += new System.EventHandler(this.TxtVNo_Leave);
            this.TxtVNo.Validated += new System.EventHandler(this.TxtVNo_Validated);
            // 
            // lbl_AdjustmentNo
            // 
            this.lbl_AdjustmentNo.AutoSize = true;
            this.lbl_AdjustmentNo.Location = new System.Drawing.Point(6, 49);
            this.lbl_AdjustmentNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_AdjustmentNo.Name = "lbl_AdjustmentNo";
            this.lbl_AdjustmentNo.Size = new System.Drawing.Size(96, 19);
            this.lbl_AdjustmentNo.TabIndex = 181;
            this.lbl_AdjustmentNo.Text = "Voucher No";
            // 
            // TxtDepartment
            // 
            this.TxtDepartment.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtDepartment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDepartment.Location = new System.Drawing.Point(105, 73);
            this.TxtDepartment.Margin = new System.Windows.Forms.Padding(4);
            this.TxtDepartment.MaxLength = 255;
            this.TxtDepartment.Name = "TxtDepartment";
            this.TxtDepartment.Size = new System.Drawing.Size(317, 25);
            this.TxtDepartment.TabIndex = 11;
            this.TxtDepartment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDepartment_KeyDown);
            this.TxtDepartment.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyPress);
            // 
            // lbl_Class
            // 
            this.lbl_Class.AutoSize = true;
            this.lbl_Class.Location = new System.Drawing.Point(6, 76);
            this.lbl_Class.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Class.Name = "lbl_Class";
            this.lbl_Class.Size = new System.Drawing.Size(99, 19);
            this.lbl_Class.TabIndex = 168;
            this.lbl_Class.Text = "Department";
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.Cursor = System.Windows.Forms.Cursors.Default;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(881, 445);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(104, 34);
            this.BtnCancel.TabIndex = 23;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Cursor = System.Windows.Forms.Cursors.Default;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(794, 445);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(86, 34);
            this.BtnSave.TabIndex = 22;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // TxtRemarks
            // 
            this.TxtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtRemarks.Location = new System.Drawing.Point(102, 413);
            this.TxtRemarks.Margin = new System.Windows.Forms.Padding(4);
            this.TxtRemarks.MaxLength = 255;
            this.TxtRemarks.Name = "TxtRemarks";
            this.TxtRemarks.Size = new System.Drawing.Size(883, 25);
            this.TxtRemarks.TabIndex = 21;
            this.TxtRemarks.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyPress);
            // 
            // lbl_Remarks
            // 
            this.lbl_Remarks.AutoSize = true;
            this.lbl_Remarks.Location = new System.Drawing.Point(9, 419);
            this.lbl_Remarks.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Remarks.Name = "lbl_Remarks";
            this.lbl_Remarks.Size = new System.Drawing.Size(76, 19);
            this.lbl_Remarks.TabIndex = 80;
            this.lbl_Remarks.Text = "Remarks\r\n";
            // 
            // btnCopy
            // 
            this.btnCopy.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopy.Appearance.Options.UseFont = true;
            this.btnCopy.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCopy.ImageOptions.Image = global::MrBLL.Properties.Resources.Copy;
            this.btnCopy.Location = new System.Drawing.Point(829, 4);
            this.btnCopy.Margin = new System.Windows.Forms.Padding(4);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(82, 35);
            this.btnCopy.TabIndex = 5;
            this.btnCopy.Text = "&COPY";
            this.btnCopy.Click += new System.EventHandler(this.BtnCopy_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Cursor = System.Windows.Forms.Cursors.Default;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.BtnExit.Location = new System.Drawing.Point(911, 4);
            this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(73, 35);
            this.BtnExit.TabIndex = 6;
            this.BtnExit.Text = "E&XIT";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // btnReverse
            // 
            this.btnReverse.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReverse.Appearance.Options.UseFont = true;
            this.btnReverse.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnReverse.ImageOptions.Image = global::MrBLL.Properties.Resources.Reverse;
            this.btnReverse.Location = new System.Drawing.Point(714, 4);
            this.btnReverse.Margin = new System.Windows.Forms.Padding(4);
            this.btnReverse.Name = "btnReverse";
            this.btnReverse.Size = new System.Drawing.Size(115, 35);
            this.btnReverse.TabIndex = 4;
            this.btnReverse.Text = "&REVERSE";
            this.btnReverse.Click += new System.EventHandler(this.BtnReverse_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Appearance.Options.UseFont = true;
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnPrint.ImageOptions.Image = global::MrBLL.Properties.Resources.Printerview;
            this.btnPrint.Location = new System.Drawing.Point(632, 4);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(82, 35);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "&PRINT";
            this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.Cursor = System.Windows.Forms.Cursors.Default;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(159, 5);
            this.BtnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(102, 35);
            this.BtnDelete.TabIndex = 2;
            this.BtnDelete.Tag = "DeleteButtonCheck";
            this.BtnDelete.Text = "&DELETE";
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEdit.Appearance.Options.UseFont = true;
            this.BtnEdit.Cursor = System.Windows.Forms.Cursors.Default;
            this.BtnEdit.ImageOptions.Image = global::MrBLL.Properties.Resources.Edit;
            this.BtnEdit.Location = new System.Drawing.Point(84, 5);
            this.BtnEdit.Margin = new System.Windows.Forms.Padding(4);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(75, 35);
            this.BtnEdit.TabIndex = 1;
            this.BtnEdit.Tag = "EditButtonCheck";
            this.BtnEdit.Text = "&EDIT";
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnNew
            // 
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.Cursor = System.Windows.Forms.Cursors.Default;
            this.BtnNew.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnNew.Location = new System.Drawing.Point(10, 5);
            this.BtnNew.Margin = new System.Windows.Forms.Padding(4);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(74, 35);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Tag = "NewButtonCheck";
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.mrMaskedTextBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.PanelProduct);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.LblTotalAltQty);
            this.panel1.Controls.Add(this.BtnRefVno);
            this.panel1.Controls.Add(this.TxtRefVno);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.BtnCancel);
            this.panel1.Controls.Add(this.clsSeparator6);
            this.panel1.Controls.Add(this.BtnSave);
            this.panel1.Controls.Add(this.lbl_InWords);
            this.panel1.Controls.Add(this.clsSeparator5);
            this.panel1.Controls.Add(this.LblNumInWords);
            this.panel1.Controls.Add(this.TxtRemarks);
            this.panel1.Controls.Add(this.lbl_Remarks);
            this.panel1.Controls.Add(this.lbl_TotQty1);
            this.panel1.Controls.Add(this.LblTotalQty);
            this.panel1.Controls.Add(this.clsSeparator2);
            this.panel1.Controls.Add(this.clsSeparator1);
            this.panel1.Controls.Add(this.LblTotalAmount);
            this.panel1.Controls.Add(this.lbl_TotNetAmt1);
            this.panel1.Controls.Add(this.BtnVno);
            this.panel1.Controls.Add(this.btnCopy);
            this.panel1.Controls.Add(this.BtnDepartment);
            this.panel1.Controls.Add(this.MskMiti);
            this.panel1.Controls.Add(this.BtnExit);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.MskDate);
            this.panel1.Controls.Add(this.lbl_AdjustmentDate);
            this.panel1.Controls.Add(this.btnReverse);
            this.panel1.Controls.Add(this.TxtVNo);
            this.panel1.Controls.Add(this.lbl_AdjustmentNo);
            this.panel1.Controls.Add(this.TxtDepartment);
            this.panel1.Controls.Add(this.BtnNew);
            this.panel1.Controls.Add(this.lbl_Class);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.BtnEdit);
            this.panel1.Controls.Add(this.BtnDelete);
            this.panel1.Controls.Add(this.TabProduct);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(988, 482);
            this.panel1.TabIndex = 0;
            // 
            // mrMaskedTextBox1
            // 
            this.mrMaskedTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mrMaskedTextBox1.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.mrMaskedTextBox1.Location = new System.Drawing.Point(854, 46);
            this.mrMaskedTextBox1.Margin = new System.Windows.Forms.Padding(4);
            this.mrMaskedTextBox1.Mask = "00/00/0000";
            this.mrMaskedTextBox1.Name = "mrMaskedTextBox1";
            this.mrMaskedTextBox1.Size = new System.Drawing.Size(128, 25);
            this.mrMaskedTextBox1.TabIndex = 204;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(811, 49);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 19);
            this.label2.TabIndex = 205;
            this.label2.Text = "Miti";
            // 
            // PanelProduct
            // 
            this.PanelProduct.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelProduct.Controls.Add(this.label23);
            this.PanelProduct.Controls.Add(this.label16);
            this.PanelProduct.Controls.Add(this.label27);
            this.PanelProduct.Controls.Add(this.label15);
            this.PanelProduct.Controls.Add(this.label14);
            this.PanelProduct.Controls.Add(this.lblSalesRate);
            this.PanelProduct.Controls.Add(this.lbl_MinQty);
            this.PanelProduct.Controls.Add(this.lblPurchaseRate);
            this.PanelProduct.Controls.Add(this.label34);
            this.PanelProduct.Controls.Add(this.lblMrpRate);
            this.PanelProduct.Controls.Add(this.lbl_MaxQty);
            this.PanelProduct.Controls.Add(this.label13);
            this.PanelProduct.Controls.Add(this.label32);
            this.PanelProduct.Controls.Add(this.lblStockQty);
            this.PanelProduct.Controls.Add(this.lbStockAltQty);
            this.PanelProduct.Controls.Add(this.label12);
            this.PanelProduct.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelProduct.Location = new System.Drawing.Point(13, 313);
            this.PanelProduct.Name = "PanelProduct";
            this.PanelProduct.Size = new System.Drawing.Size(972, 62);
            this.PanelProduct.TabIndex = 203;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label23.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label23.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label23.Location = new System.Drawing.Point(314, 32);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(124, 24);
            this.label23.TabIndex = 241;
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label16.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label16.Location = new System.Drawing.Point(217, 9);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(82, 19);
            this.label16.TabIndex = 221;
            this.label16.Text = "MRP Rate";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label27.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label27.Location = new System.Drawing.Point(217, 34);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(92, 19);
            this.label27.TabIndex = 240;
            this.label27.Text = "Trade Rate";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label15.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label15.Location = new System.Drawing.Point(5, 35);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(90, 19);
            this.label15.TabIndex = 220;
            this.label15.Text = "Sales Rate";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label14.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label14.Location = new System.Drawing.Point(4, 10);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(78, 19);
            this.label14.TabIndex = 219;
            this.label14.Text = "Buy Rate";
            // 
            // lblSalesRate
            // 
            this.lblSalesRate.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblSalesRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSalesRate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSalesRate.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lblSalesRate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSalesRate.Location = new System.Drawing.Point(98, 32);
            this.lblSalesRate.Name = "lblSalesRate";
            this.lblSalesRate.Size = new System.Drawing.Size(114, 24);
            this.lblSalesRate.TabIndex = 234;
            this.lblSalesRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_MinQty
            // 
            this.lbl_MinQty.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_MinQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_MinQty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_MinQty.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_MinQty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_MinQty.Location = new System.Drawing.Point(709, 33);
            this.lbl_MinQty.Name = "lbl_MinQty";
            this.lbl_MinQty.Size = new System.Drawing.Size(109, 24);
            this.lbl_MinQty.TabIndex = 239;
            this.lbl_MinQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_MinQty.Visible = false;
            // 
            // lblPurchaseRate
            // 
            this.lblPurchaseRate.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblPurchaseRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPurchaseRate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPurchaseRate.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lblPurchaseRate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPurchaseRate.Location = new System.Drawing.Point(98, 6);
            this.lblPurchaseRate.Name = "lblPurchaseRate";
            this.lblPurchaseRate.Size = new System.Drawing.Size(114, 24);
            this.lblPurchaseRate.TabIndex = 233;
            this.lblPurchaseRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label34.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label34.Location = new System.Drawing.Point(633, 35);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(68, 19);
            this.label34.TabIndex = 238;
            this.label34.Text = "Min Qty";
            this.label34.Visible = false;
            // 
            // lblMrpRate
            // 
            this.lblMrpRate.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblMrpRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMrpRate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblMrpRate.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lblMrpRate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMrpRate.Location = new System.Drawing.Point(314, 7);
            this.lblMrpRate.Name = "lblMrpRate";
            this.lblMrpRate.Size = new System.Drawing.Size(124, 24);
            this.lblMrpRate.TabIndex = 235;
            this.lblMrpRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_MaxQty
            // 
            this.lbl_MaxQty.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_MaxQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_MaxQty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_MaxQty.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_MaxQty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_MaxQty.Location = new System.Drawing.Point(709, 7);
            this.lbl_MaxQty.Name = "lbl_MaxQty";
            this.lbl_MaxQty.Size = new System.Drawing.Size(109, 24);
            this.lbl_MaxQty.TabIndex = 237;
            this.lbl_MaxQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_MaxQty.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label13.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label13.Location = new System.Drawing.Point(446, 35);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 19);
            this.label13.TabIndex = 218;
            this.label13.Text = "Qty";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label32.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label32.Location = new System.Drawing.Point(631, 9);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(70, 19);
            this.label32.TabIndex = 236;
            this.label32.Text = "Max Qty";
            this.label32.Visible = false;
            // 
            // lblStockQty
            // 
            this.lblStockQty.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblStockQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStockQty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblStockQty.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lblStockQty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblStockQty.Location = new System.Drawing.Point(510, 32);
            this.lblStockQty.Name = "lblStockQty";
            this.lblStockQty.Size = new System.Drawing.Size(109, 24);
            this.lblStockQty.TabIndex = 232;
            this.lblStockQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbStockAltQty
            // 
            this.lbStockAltQty.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbStockAltQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbStockAltQty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbStockAltQty.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbStockAltQty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbStockAltQty.Location = new System.Drawing.Point(510, 7);
            this.lbStockAltQty.Name = "lbStockAltQty";
            this.lbStockAltQty.Size = new System.Drawing.Size(109, 24);
            this.lbStockAltQty.TabIndex = 231;
            this.lbStockAltQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label12.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label12.Location = new System.Drawing.Point(446, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 19);
            this.label12.TabIndex = 217;
            this.label12.Text = "Alt Qty";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(205, 381);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(103, 19);
            this.label10.TabIndex = 201;
            this.label10.Text = "Total  AltQty";
            // 
            // LblTotalAltQty
            // 
            this.LblTotalAltQty.BackColor = System.Drawing.SystemColors.HighlightText;
            this.LblTotalAltQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTotalAltQty.Location = new System.Drawing.Point(316, 379);
            this.LblTotalAltQty.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblTotalAltQty.Name = "LblTotalAltQty";
            this.LblTotalAltQty.Size = new System.Drawing.Size(153, 25);
            this.LblTotalAltQty.TabIndex = 202;
            this.LblTotalAltQty.Text = "0.00";
            this.LblTotalAltQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BtnRefVno
            // 
            this.BtnRefVno.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.BtnRefVno.CausesValidation = false;
            this.BtnRefVno.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnRefVno.Location = new System.Drawing.Point(767, 45);
            this.BtnRefVno.Margin = new System.Windows.Forms.Padding(4);
            this.BtnRefVno.Name = "BtnRefVno";
            this.BtnRefVno.Size = new System.Drawing.Size(32, 26);
            this.BtnRefVno.TabIndex = 197;
            this.BtnRefVno.TabStop = false;
            this.BtnRefVno.UseVisualStyleBackColor = false;
            // 
            // TxtRefVno
            // 
            this.TxtRefVno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtRefVno.Location = new System.Drawing.Point(641, 46);
            this.TxtRefVno.Margin = new System.Windows.Forms.Padding(4);
            this.TxtRefVno.MaxLength = 255;
            this.TxtRefVno.Name = "TxtRefVno";
            this.TxtRefVno.Size = new System.Drawing.Size(123, 25);
            this.TxtRefVno.TabIndex = 10;
            this.TxtRefVno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtRefVno_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(572, 49);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 19);
            this.label3.TabIndex = 196;
            this.label3.Text = "Ref Vno";
            // 
            // clsSeparator6
            // 
            this.clsSeparator6.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator6.Location = new System.Drawing.Point(12, 441);
            this.clsSeparator6.Name = "clsSeparator6";
            this.clsSeparator6.Size = new System.Drawing.Size(977, 2);
            this.clsSeparator6.TabIndex = 193;
            this.clsSeparator6.TabStop = false;
            // 
            // clsSeparator5
            // 
            this.clsSeparator5.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator5.Location = new System.Drawing.Point(8, 407);
            this.clsSeparator5.Name = "clsSeparator5";
            this.clsSeparator5.Size = new System.Drawing.Size(977, 2);
            this.clsSeparator5.TabIndex = 192;
            this.clsSeparator5.TabStop = false;
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(10, 101);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(976, 2);
            this.clsSeparator2.TabIndex = 189;
            this.clsSeparator2.TabStop = false;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(10, 41);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(976, 2);
            this.clsSeparator1.TabIndex = 188;
            this.clsSeparator1.TabStop = false;
            // 
            // TabProduct
            // 
            this.TabProduct.Controls.Add(this.TbInfomartion);
            this.TabProduct.Controls.Add(this.TbDocument);
            this.TabProduct.Location = new System.Drawing.Point(10, 109);
            this.TabProduct.Name = "TabProduct";
            this.TabProduct.SelectedIndex = 0;
            this.TabProduct.Size = new System.Drawing.Size(979, 201);
            this.TabProduct.TabIndex = 20;
            // 
            // TbInfomartion
            // 
            this.TbInfomartion.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.TbInfomartion.Controls.Add(this.DGrid);
            this.TbInfomartion.Location = new System.Drawing.Point(4, 28);
            this.TbInfomartion.Name = "TbInfomartion";
            this.TbInfomartion.Padding = new System.Windows.Forms.Padding(3);
            this.TbInfomartion.Size = new System.Drawing.Size(971, 169);
            this.TbInfomartion.TabIndex = 0;
            this.TbInfomartion.Text = "Information";
            // 
            // TbDocument
            // 
            this.TbDocument.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.TbDocument.Location = new System.Drawing.Point(4, 28);
            this.TbDocument.Name = "TbDocument";
            this.TbDocument.Padding = new System.Windows.Forms.Padding(3);
            this.TbDocument.Size = new System.Drawing.Size(971, 169);
            this.TbDocument.TabIndex = 1;
            this.TbDocument.Text = "Document";
            // 
            // FrmPhysicalStockEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(988, 482);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmPhysicalStockEntry";
            this.ShowIcon = false;
            this.Tag = "Physical Stock";
            this.Text = "PHYSICAL STOCK ENTRY";
            this.Load += new System.EventHandler(this.FrmPhysicalStockEntry_Load);
            this.Shown += new System.EventHandler(this.FrmPhysicalStockEntry_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmPhysicalStockEntry_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.PanelProduct.ResumeLayout(false);
            this.PanelProduct.PerformLayout();
            this.TabProduct.ResumeLayout(false);
            this.TbInfomartion.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl_TotQty1;
        private System.Windows.Forms.Label LblTotalQty;
        private System.Windows.Forms.Label LblTotalAmount;
        private System.Windows.Forms.Label lbl_TotNetAmt1;
        private System.Windows.Forms.Label lbl_AdjustmentDate;
        private System.Windows.Forms.Label lbl_AdjustmentNo;
        private System.Windows.Forms.Label lbl_Class;
        private System.Windows.Forms.Label lbl_Remarks;
        private System.Windows.Forms.Label LblNumInWords;
        private System.Windows.Forms.Label lbl_InWords;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton btnReverse;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private DevExpress.XtraEditors.SimpleButton btnCopy;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button BtnDepartment;
        public System.Windows.Forms.Button BtnVno;
        private ClsSeparator clsSeparator1;
        private ClsSeparator clsSeparator2;
        private ClsSeparator clsSeparator5;
        private ClsSeparator clsSeparator6;
        public System.Windows.Forms.Button BtnRefVno;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label LblTotalAltQty;
        public System.Windows.Forms.Label label23;
        public System.Windows.Forms.Label label16;
        public System.Windows.Forms.Label label27;
        public System.Windows.Forms.Label label15;
        public System.Windows.Forms.Label label14;
        public System.Windows.Forms.Label lblSalesRate;
        public System.Windows.Forms.Label lbl_MinQty;
        public System.Windows.Forms.Label lblPurchaseRate;
        public System.Windows.Forms.Label label34;
        public System.Windows.Forms.Label lblMrpRate;
        public System.Windows.Forms.Label lbl_MaxQty;
        public System.Windows.Forms.Label label13;
        public System.Windows.Forms.Label label32;
        public System.Windows.Forms.Label lblStockQty;
        public System.Windows.Forms.Label lbStockAltQty;
        public System.Windows.Forms.Label label12;
        private System.Windows.Forms.TabControl TabProduct;
        private System.Windows.Forms.TabPage TbInfomartion;
        private System.Windows.Forms.TabPage TbDocument;
        private MrMaskedTextBox MskDate;
        private MrTextBox TxtVNo;
        private MrTextBox TxtDepartment;
        private MrTextBox TxtRemarks;
        private MrMaskedTextBox MskMiti;
        private MrTextBox TxtRefVno;
        private MrPanel panel1;
        private MrPanel PanelProduct;
        private MrMaskedTextBox mrMaskedTextBox1;
        private System.Windows.Forms.Label label2;
        private EntryGridViewEx DGrid;
    }
}