
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.POS.Entry
{
    partial class FrmPointOfSales
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPointOfSales));
            this.GrpMemberType = new System.Windows.Forms.GroupBox();
            this.LblPriceTag = new System.Windows.Forms.Label();
            this.lbl00001 = new System.Windows.Forms.Label();
            this.LblMemberAmount = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.LblMemberType = new System.Windows.Forms.Label();
            this.LblMemberPhone = new System.Windows.Forms.Label();
            this.LblMemberShortName = new System.Windows.Forms.Label();
            this.LblMemberName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtBarcode = new MrTextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.TxtUOM = new MrTextBox();
            this.GrpInvoiceTotalDetails = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtBillAmount = new MrTextBox();
            this.clsSeparator2 = new ClsSeparator();
            this.TxtDiscount = new MrTextBox();
            this.TxtGrandTotal = new MrTextBox();
            this.TxtDiscountPercentage = new MrTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.GrpButtonDetails = new System.Windows.Forms.GroupBox();
            this.BtnReset = new DevExpress.XtraEditors.SimpleButton();
            this.BtnNext = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCash = new DevExpress.XtraEditors.SimpleButton();
            this.BtnHold = new DevExpress.XtraEditors.SimpleButton();
            this.TxtQty = new MrTextBox();
            this.DGrid = new System.Windows.Forms.DataGridView();
            this.colSn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colShortName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAltQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAltUom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUOM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNetAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsInvoiceItems = new System.Windows.Forms.BindingSource(this.components);
            this.pnlBottom = new MrPanel();
            this.GrpSelectedProduct = new System.Windows.Forms.GroupBox();
            this.BtnCancelItemEdit = new DevExpress.XtraEditors.SimpleButton();
            this.glkupProducts = new DevExpress.XtraEditors.GridLookUpEdit();
            this.bsProducts = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lblItemDisP = new System.Windows.Forms.Label();
            this.lblItemRate = new System.Windows.Forms.Label();
            this.lblItemDis = new System.Windows.Forms.Label();
            this.TxtAltUOM = new MrTextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.TxtAltQty = new MrTextBox();
            this.GrpProductInfo = new System.Windows.Forms.GroupBox();
            this.LblRate = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.LblStockQty = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.LblProduct = new System.Windows.Forms.Label();
            this.GrpInvoiceTotal = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.LblItemsDiscountSum = new System.Windows.Forms.Label();
            this.lblRefInvoiceId = new System.Windows.Forms.Label();
            this.lblTermAmount = new System.Windows.Forms.Label();
            this.lblTax = new System.Windows.Forms.Label();
            this.lblNonTaxable = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblTaxable = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.LblItemsNetAmount = new System.Windows.Forms.Label();
            this.LblItemsTotalQty = new System.Windows.Forms.Label();
            this.lbl_TotQty1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.LblItemsTotal = new System.Windows.Forms.Label();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.glkupMembers = new DevExpress.XtraEditors.GridLookUpEdit();
            this.bsMembersList = new System.Windows.Forms.BindingSource(this.components);
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcShortName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMemberType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStartDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcExpireDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPhone = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEmail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDiscount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBalance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnReverse = new DevExpress.XtraEditors.SimpleButton();
            this.btnReprintLast = new DevExpress.XtraEditors.SimpleButton();
            this.BtnHoldList = new DevExpress.XtraEditors.SimpleButton();
            this.clsSeparator5 = new ClsSeparator();
            this.clsSeparator1 = new ClsSeparator();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.slBranch = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.slUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel10 = new System.Windows.Forms.ToolStripStatusLabel();
            this.slToday = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel12 = new System.Windows.Forms.ToolStripStatusLabel();
            this.slMiti = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel9 = new System.Windows.Forms.ToolStripStatusLabel();
            this.slNextInvoice = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.slLastInvoice = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.slLastGrandTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.slLastTender = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel11 = new System.Windows.Forms.ToolStripStatusLabel();
            this.slLastReturn = new System.Windows.Forms.ToolStripStatusLabel();
            this.GrpMemberType.SuspendLayout();
            this.GrpInvoiceTotalDetails.SuspendLayout();
            this.GrpButtonDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsInvoiceItems)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.GrpSelectedProduct.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkupProducts.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.GrpProductInfo.SuspendLayout();
            this.GrpInvoiceTotal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkupMembers.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMembersList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            this.statusStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // GrpMemberType
            // 
            this.GrpMemberType.Controls.Add(this.LblPriceTag);
            this.GrpMemberType.Controls.Add(this.lbl00001);
            this.GrpMemberType.Controls.Add(this.LblMemberAmount);
            this.GrpMemberType.Controls.Add(this.label18);
            this.GrpMemberType.Controls.Add(this.LblMemberType);
            this.GrpMemberType.Controls.Add(this.LblMemberPhone);
            this.GrpMemberType.Controls.Add(this.LblMemberShortName);
            this.GrpMemberType.Controls.Add(this.LblMemberName);
            this.GrpMemberType.Controls.Add(this.label1);
            this.GrpMemberType.Controls.Add(this.label17);
            this.GrpMemberType.Controls.Add(this.label16);
            this.GrpMemberType.Controls.Add(this.label15);
            this.GrpMemberType.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.GrpMemberType.Location = new System.Drawing.Point(7, 79);
            this.GrpMemberType.Name = "GrpMemberType";
            this.GrpMemberType.Size = new System.Drawing.Size(329, 180);
            this.GrpMemberType.TabIndex = 308;
            this.GrpMemberType.TabStop = false;
            this.GrpMemberType.Text = "Member Information";
            // 
            // LblPriceTag
            // 
            this.LblPriceTag.BackColor = System.Drawing.SystemColors.Window;
            this.LblPriceTag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPriceTag.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPriceTag.ForeColor = System.Drawing.Color.Black;
            this.LblPriceTag.Location = new System.Drawing.Point(88, 149);
            this.LblPriceTag.Name = "LblPriceTag";
            this.LblPriceTag.Size = new System.Drawing.Size(231, 25);
            this.LblPriceTag.TabIndex = 160;
            this.LblPriceTag.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl00001
            // 
            this.lbl00001.AutoSize = true;
            this.lbl00001.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl00001.ForeColor = System.Drawing.Color.Black;
            this.lbl00001.Location = new System.Drawing.Point(7, 151);
            this.lbl00001.Name = "lbl00001";
            this.lbl00001.Size = new System.Drawing.Size(85, 20);
            this.lbl00001.TabIndex = 159;
            this.lbl00001.Text = "Price Tag:";
            // 
            // LblMemberAmount
            // 
            this.LblMemberAmount.BackColor = System.Drawing.SystemColors.Window;
            this.LblMemberAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblMemberAmount.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMemberAmount.ForeColor = System.Drawing.Color.Black;
            this.LblMemberAmount.Location = new System.Drawing.Point(88, 121);
            this.LblMemberAmount.Name = "LblMemberAmount";
            this.LblMemberAmount.Size = new System.Drawing.Size(231, 25);
            this.LblMemberAmount.TabIndex = 158;
            this.LblMemberAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Black;
            this.label18.Location = new System.Drawing.Point(7, 123);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(82, 20);
            this.label18.TabIndex = 157;
            this.label18.Text = "Balance :";
            // 
            // LblMemberType
            // 
            this.LblMemberType.BackColor = System.Drawing.SystemColors.Window;
            this.LblMemberType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblMemberType.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMemberType.ForeColor = System.Drawing.Color.Black;
            this.LblMemberType.Location = new System.Drawing.Point(88, 95);
            this.LblMemberType.Name = "LblMemberType";
            this.LblMemberType.Size = new System.Drawing.Size(231, 24);
            this.LblMemberType.TabIndex = 156;
            // 
            // LblMemberPhone
            // 
            this.LblMemberPhone.BackColor = System.Drawing.SystemColors.Window;
            this.LblMemberPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblMemberPhone.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMemberPhone.ForeColor = System.Drawing.Color.Black;
            this.LblMemberPhone.Location = new System.Drawing.Point(88, 71);
            this.LblMemberPhone.Name = "LblMemberPhone";
            this.LblMemberPhone.Size = new System.Drawing.Size(231, 24);
            this.LblMemberPhone.TabIndex = 155;
            // 
            // LblMemberShortName
            // 
            this.LblMemberShortName.BackColor = System.Drawing.SystemColors.Window;
            this.LblMemberShortName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblMemberShortName.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMemberShortName.ForeColor = System.Drawing.Color.Black;
            this.LblMemberShortName.Location = new System.Drawing.Point(88, 47);
            this.LblMemberShortName.Name = "LblMemberShortName";
            this.LblMemberShortName.Size = new System.Drawing.Size(231, 24);
            this.LblMemberShortName.TabIndex = 155;
            // 
            // LblMemberName
            // 
            this.LblMemberName.BackColor = System.Drawing.SystemColors.Window;
            this.LblMemberName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblMemberName.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMemberName.ForeColor = System.Drawing.Color.Black;
            this.LblMemberName.Location = new System.Drawing.Point(88, 21);
            this.LblMemberName.Name = "LblMemberName";
            this.LblMemberName.Size = new System.Drawing.Size(231, 24);
            this.LblMemberName.TabIndex = 154;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(7, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 152;
            this.label1.Text = "Phone No";
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(7, 97);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(56, 20);
            this.label17.TabIndex = 153;
            this.label17.Text = "Type :";
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(7, 49);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(103, 20);
            this.label16.TabIndex = 152;
            this.label16.Text = "Short Name";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(7, 23);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(64, 20);
            this.label15.TabIndex = 151;
            this.label15.Text = "Name :";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 20);
            this.label3.TabIndex = 333;
            this.label3.Text = "Member";
            // 
            // TxtBarcode
            // 
            this.TxtBarcode.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBarcode.Location = new System.Drawing.Point(80, 10);
            this.TxtBarcode.MaxLength = 255;
            this.TxtBarcode.Name = "TxtBarcode";
            this.TxtBarcode.Size = new System.Drawing.Size(254, 26);
            this.TxtBarcode.TabIndex = 0;
            this.TxtBarcode.Enter += new System.EventHandler(this.GlobalEnter_Event);
            this.TxtBarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBarcode_KeyPress);
            this.TxtBarcode.Leave += new System.EventHandler(this.GlobalLeave_Event);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(926, 13);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(37, 20);
            this.label19.TabIndex = 336;
            this.label19.Text = "Qty";
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(3, 13);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(77, 20);
            this.label20.TabIndex = 211;
            this.label20.Text = "BarCode";
            // 
            // TxtUOM
            // 
            this.TxtUOM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtUOM.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUOM.Location = new System.Drawing.Point(1043, 10);
            this.TxtUOM.MaxLength = 255;
            this.TxtUOM.Name = "TxtUOM";
            this.TxtUOM.ReadOnly = true;
            this.TxtUOM.Size = new System.Drawing.Size(80, 26);
            this.TxtUOM.TabIndex = 4;
            // 
            // GrpInvoiceTotalDetails
            // 
            this.GrpInvoiceTotalDetails.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.GrpInvoiceTotalDetails.Controls.Add(this.label5);
            this.GrpInvoiceTotalDetails.Controls.Add(this.label4);
            this.GrpInvoiceTotalDetails.Controls.Add(this.TxtBillAmount);
            this.GrpInvoiceTotalDetails.Controls.Add(this.clsSeparator2);
            this.GrpInvoiceTotalDetails.Controls.Add(this.TxtDiscount);
            this.GrpInvoiceTotalDetails.Controls.Add(this.TxtGrandTotal);
            this.GrpInvoiceTotalDetails.Controls.Add(this.TxtDiscountPercentage);
            this.GrpInvoiceTotalDetails.Controls.Add(this.label2);
            this.GrpInvoiceTotalDetails.Controls.Add(this.label6);
            this.GrpInvoiceTotalDetails.Location = new System.Drawing.Point(7, 279);
            this.GrpInvoiceTotalDetails.Name = "GrpInvoiceTotalDetails";
            this.GrpInvoiceTotalDetails.Size = new System.Drawing.Size(329, 236);
            this.GrpInvoiceTotalDetails.TabIndex = 1;
            this.GrpInvoiceTotalDetails.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(113, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 18);
            this.label5.TabIndex = 227;
            this.label5.Text = "%";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(6, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 32);
            this.label4.TabIndex = 226;
            this.label4.Text = "Discount";
            // 
            // TxtBillAmount
            // 
            this.TxtBillAmount.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtBillAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBillAmount.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.TxtBillAmount.Location = new System.Drawing.Point(6, 45);
            this.TxtBillAmount.MaxLength = 255;
            this.TxtBillAmount.Name = "TxtBillAmount";
            this.TxtBillAmount.ReadOnly = true;
            this.TxtBillAmount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtBillAmount.Size = new System.Drawing.Size(318, 32);
            this.TxtBillAmount.TabIndex = 0;
            this.TxtBillAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.Lavender;
            this.clsSeparator2.Location = new System.Drawing.Point(6, 81);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(318, 3);
            this.clsSeparator2.TabIndex = 312;
            this.clsSeparator2.TabStop = false;
            // 
            // TxtDiscount
            // 
            this.TxtDiscount.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtDiscount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDiscount.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.TxtDiscount.Location = new System.Drawing.Point(135, 124);
            this.TxtDiscount.Name = "TxtDiscount";
            this.TxtDiscount.Size = new System.Drawing.Size(189, 32);
            this.TxtDiscount.TabIndex = 2;
            this.TxtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtDiscount.TextChanged += new System.EventHandler(this.TxtDiscount_TextChanged);
            this.TxtDiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDiscount_KeyPress);
            // 
            // TxtGrandTotal
            // 
            this.TxtGrandTotal.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtGrandTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtGrandTotal.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.TxtGrandTotal.Location = new System.Drawing.Point(6, 193);
            this.TxtGrandTotal.MaxLength = 255;
            this.TxtGrandTotal.Name = "TxtGrandTotal";
            this.TxtGrandTotal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtGrandTotal.Size = new System.Drawing.Size(318, 38);
            this.TxtGrandTotal.TabIndex = 3;
            this.TxtGrandTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtDiscountPercentage
            // 
            this.TxtDiscountPercentage.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtDiscountPercentage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDiscountPercentage.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.TxtDiscountPercentage.Location = new System.Drawing.Point(6, 124);
            this.TxtDiscountPercentage.MaxLength = 2;
            this.TxtDiscountPercentage.Name = "TxtDiscountPercentage";
            this.TxtDiscountPercentage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtDiscountPercentage.Size = new System.Drawing.Size(103, 32);
            this.TxtDiscountPercentage.TabIndex = 1;
            this.TxtDiscountPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtDiscountPercentage.TextChanged += new System.EventHandler(this.TxtDiscountPercentage_TextChanged);
            this.TxtDiscountPercentage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDiscountPercentage_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(6, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(172, 32);
            this.label2.TabIndex = 314;
            this.label2.Text = "Bill Amount";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Navy;
            this.label6.Location = new System.Drawing.Point(6, 158);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(172, 32);
            this.label6.TabIndex = 227;
            this.label6.Text = "Net Amount";
            // 
            // GrpButtonDetails
            // 
            this.GrpButtonDetails.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.GrpButtonDetails.Controls.Add(this.BtnReset);
            this.GrpButtonDetails.Controls.Add(this.BtnNext);
            this.GrpButtonDetails.Controls.Add(this.BtnCash);
            this.GrpButtonDetails.Controls.Add(this.BtnHold);
            this.GrpButtonDetails.Location = new System.Drawing.Point(7, 516);
            this.GrpButtonDetails.Name = "GrpButtonDetails";
            this.GrpButtonDetails.Size = new System.Drawing.Size(329, 130);
            this.GrpButtonDetails.TabIndex = 2;
            this.GrpButtonDetails.TabStop = false;
            // 
            // BtnReset
            // 
            this.BtnReset.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnReset.Appearance.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.BtnReset.Appearance.Options.UseFont = true;
            this.BtnReset.Appearance.Options.UseForeColor = true;
            this.BtnReset.Location = new System.Drawing.Point(6, 69);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(149, 56);
            this.BtnReset.TabIndex = 5;
            this.BtnReset.Text = "&RESET";
            this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // BtnNext
            // 
            this.BtnNext.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNext.Appearance.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.BtnNext.Appearance.Options.UseFont = true;
            this.BtnNext.Appearance.Options.UseForeColor = true;
            this.BtnNext.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnNext.Location = new System.Drawing.Point(161, 10);
            this.BtnNext.Name = "BtnNext";
            this.BtnNext.Size = new System.Drawing.Size(162, 53);
            this.BtnNext.TabIndex = 0;
            this.BtnNext.Text = "NEXT";
            this.BtnNext.Click += new System.EventHandler(this.BtnNext_Click);
            // 
            // BtnCash
            // 
            this.BtnCash.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCash.Appearance.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.BtnCash.Appearance.Options.UseFont = true;
            this.BtnCash.Appearance.Options.UseForeColor = true;
            this.BtnCash.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnCash.Location = new System.Drawing.Point(6, 10);
            this.BtnCash.Name = "BtnCash";
            this.BtnCash.Size = new System.Drawing.Size(149, 53);
            this.BtnCash.TabIndex = 0;
            this.BtnCash.Text = "C&ASH";
            this.BtnCash.Click += new System.EventHandler(this.BtnCash_Click);
            // 
            // BtnHold
            // 
            this.BtnHold.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnHold.Appearance.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.BtnHold.Appearance.Options.UseFont = true;
            this.BtnHold.Appearance.Options.UseForeColor = true;
            this.BtnHold.Location = new System.Drawing.Point(161, 69);
            this.BtnHold.Name = "BtnHold";
            this.BtnHold.Size = new System.Drawing.Size(161, 56);
            this.BtnHold.TabIndex = 4;
            this.BtnHold.Text = "HOLD";
            this.BtnHold.Click += new System.EventHandler(this.BtnHold_Click);
            // 
            // TxtQty
            // 
            this.TxtQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtQty.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtQty.Location = new System.Drawing.Point(963, 10);
            this.TxtQty.MaxLength = 255;
            this.TxtQty.Name = "TxtQty";
            this.TxtQty.Size = new System.Drawing.Size(80, 26);
            this.TxtQty.TabIndex = 3;
            this.TxtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtQty.TextChanged += new System.EventHandler(this.TxtQty_TextChanged);
            this.TxtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtQty_KeyDown);
            // 
            // DGrid
            // 
            this.DGrid.AllowUserToAddRows = false;
            this.DGrid.AllowUserToResizeColumns = false;
            this.DGrid.AllowUserToResizeRows = false;
            this.DGrid.AutoGenerateColumns = false;
            this.DGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.DGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGrid.ColumnHeadersHeight = 27;
            this.DGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSn,
            this.colShortName,
            this.colProductName,
            this.colAltQty,
            this.colAltUom,
            this.colQty,
            this.colUOM,
            this.colRate,
            this.colAmount,
            this.colDiscount,
            this.colNetAmount});
            this.DGrid.DataSource = this.bsInvoiceItems;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGrid.DefaultCellStyle = dataGridViewCellStyle8;
            this.DGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.DGrid.Location = new System.Drawing.Point(0, 0);
            this.DGrid.MultiSelect = false;
            this.DGrid.Name = "DGrid";
            this.DGrid.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.DGrid.RowHeadersWidth = 20;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DGrid.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.DGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGrid.Size = new System.Drawing.Size(1010, 724);
            this.DGrid.TabIndex = 0;
            this.DGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGrid_CellDoubleClick);
            this.DGrid.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DGrid_RowHeaderMouseDoubleClick);
            this.DGrid.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.DGrid_UserDeletedRow);
            this.DGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DGrid_KeyDown);
            // 
            // colSn
            // 
            this.colSn.DataPropertyName = "SNo";
            this.colSn.HeaderText = "#";
            this.colSn.Name = "colSn";
            this.colSn.ReadOnly = true;
            this.colSn.Width = 40;
            // 
            // colShortName
            // 
            this.colShortName.DataPropertyName = "ProductShortName";
            this.colShortName.HeaderText = "SHORT NAME";
            this.colShortName.MinimumWidth = 150;
            this.colShortName.Name = "colShortName";
            this.colShortName.ReadOnly = true;
            this.colShortName.Width = 150;
            // 
            // colProductName
            // 
            this.colProductName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colProductName.DataPropertyName = "ProductName";
            this.colProductName.HeaderText = "PRODUCT";
            this.colProductName.MinimumWidth = 150;
            this.colProductName.Name = "colProductName";
            this.colProductName.ReadOnly = true;
            // 
            // colAltQty
            // 
            this.colAltQty.DataPropertyName = "AltQty";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colAltQty.DefaultCellStyle = dataGridViewCellStyle2;
            this.colAltQty.HeaderText = "ALT QTY";
            this.colAltQty.Name = "colAltQty";
            this.colAltQty.ReadOnly = true;
            this.colAltQty.Width = 80;
            // 
            // colAltUom
            // 
            this.colAltUom.DataPropertyName = "AltUnitName";
            this.colAltUom.HeaderText = "ALT UOM";
            this.colAltUom.Name = "colAltUom";
            this.colAltUom.ReadOnly = true;
            this.colAltUom.Width = 70;
            // 
            // colQty
            // 
            this.colQty.DataPropertyName = "Qty";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            this.colQty.DefaultCellStyle = dataGridViewCellStyle3;
            this.colQty.HeaderText = "QTY";
            this.colQty.Name = "colQty";
            this.colQty.ReadOnly = true;
            this.colQty.Width = 80;
            // 
            // colUOM
            // 
            this.colUOM.DataPropertyName = "UnitName";
            this.colUOM.HeaderText = "UOM";
            this.colUOM.Name = "colUOM";
            this.colUOM.ReadOnly = true;
            this.colUOM.Width = 70;
            // 
            // colRate
            // 
            this.colRate.DataPropertyName = "Rate";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "0.00";
            this.colRate.DefaultCellStyle = dataGridViewCellStyle4;
            this.colRate.HeaderText = "RATE";
            this.colRate.Name = "colRate";
            this.colRate.ReadOnly = true;
            // 
            // colAmount
            // 
            this.colAmount.DataPropertyName = "NAmount";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "0.00";
            this.colAmount.DefaultCellStyle = dataGridViewCellStyle5;
            this.colAmount.HeaderText = "TOTAL";
            this.colAmount.Name = "colAmount";
            this.colAmount.ReadOnly = true;
            this.colAmount.Width = 80;
            // 
            // colDiscount
            // 
            this.colDiscount.DataPropertyName = "ItemDis";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "0.00";
            this.colDiscount.DefaultCellStyle = dataGridViewCellStyle6;
            this.colDiscount.HeaderText = "DISCOUNT";
            this.colDiscount.Name = "colDiscount";
            this.colDiscount.ReadOnly = true;
            this.colDiscount.Width = 80;
            // 
            // colNetAmount
            // 
            this.colNetAmount.DataPropertyName = "ActualAmountAfterDiscount";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "0.00";
            this.colNetAmount.DefaultCellStyle = dataGridViewCellStyle7;
            this.colNetAmount.HeaderText = "NET";
            this.colNetAmount.Name = "colNetAmount";
            this.colNetAmount.ReadOnly = true;
            // 
            // bsInvoiceItems
            // 
            this.bsInvoiceItems.CurrentItemChanged += new System.EventHandler(this.bsInvoiceItems_CurrentItemChanged);
            this.bsInvoiceItems.ListChanged += new System.ComponentModel.ListChangedEventHandler(this.bsInvoiceItems_ListChanged);
            // 
            // pnlBottom
            // 
            this.pnlBottom.AutoSize = true;
            this.pnlBottom.Controls.Add(this.GrpSelectedProduct);
            this.pnlBottom.Controls.Add(this.GrpProductInfo);
            this.pnlBottom.Controls.Add(this.GrpInvoiceTotal);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 582);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1370, 142);
            this.pnlBottom.TabIndex = 2;
            // 
            // GrpSelectedProduct
            // 
            this.GrpSelectedProduct.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.GrpSelectedProduct.Controls.Add(this.BtnCancelItemEdit);
            this.GrpSelectedProduct.Controls.Add(this.glkupProducts);
            this.GrpSelectedProduct.Controls.Add(this.lblItemDisP);
            this.GrpSelectedProduct.Controls.Add(this.lblItemRate);
            this.GrpSelectedProduct.Controls.Add(this.lblItemDis);
            this.GrpSelectedProduct.Controls.Add(this.TxtAltUOM);
            this.GrpSelectedProduct.Controls.Add(this.label21);
            this.GrpSelectedProduct.Controls.Add(this.TxtAltQty);
            this.GrpSelectedProduct.Controls.Add(this.TxtBarcode);
            this.GrpSelectedProduct.Controls.Add(this.TxtUOM);
            this.GrpSelectedProduct.Controls.Add(this.label19);
            this.GrpSelectedProduct.Controls.Add(this.label20);
            this.GrpSelectedProduct.Controls.Add(this.TxtQty);
            this.GrpSelectedProduct.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.GrpSelectedProduct.Location = new System.Drawing.Point(0, 0);
            this.GrpSelectedProduct.Name = "GrpSelectedProduct";
            this.GrpSelectedProduct.Size = new System.Drawing.Size(1370, 39);
            this.GrpSelectedProduct.TabIndex = 0;
            this.GrpSelectedProduct.TabStop = false;
            // 
            // BtnCancelItemEdit
            // 
            this.BtnCancelItemEdit.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.BtnCancelItemEdit.Location = new System.Drawing.Point(1123, 10);
            this.BtnCancelItemEdit.Name = "BtnCancelItemEdit";
            this.BtnCancelItemEdit.Size = new System.Drawing.Size(31, 26);
            this.BtnCancelItemEdit.TabIndex = 9;
            this.BtnCancelItemEdit.Click += new System.EventHandler(this.BtnCancelItemEdit_Click);
            // 
            // glkupProducts
            // 
            this.glkupProducts.EditValue = "";
            this.glkupProducts.Location = new System.Drawing.Point(334, 8);
            this.glkupProducts.Name = "glkupProducts";
            this.glkupProducts.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.glkupProducts.Properties.Appearance.Options.UseFont = true;
            this.glkupProducts.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.glkupProducts.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkupProducts.Properties.DataSource = this.bsProducts;
            this.glkupProducts.Properties.NullText = "";
            this.glkupProducts.Properties.PopupView = this.gridView1;
            this.glkupProducts.Size = new System.Drawing.Size(360, 28);
            this.glkupProducts.TabIndex = 340;
            this.glkupProducts.EditValueChanged += new System.EventHandler(this.glkupProducts_EditValueChanged);
            this.glkupProducts.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.glkupProducts_KeyPress);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "PRODUCT NAME";
            this.gridColumn1.FieldName = "ProductName";
            this.gridColumn1.MinWidth = 250;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 250;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "SHORT NAME";
            this.gridColumn2.FieldName = "ProductShortName";
            this.gridColumn2.MinWidth = 150;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 150;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "STOCK QTY";
            this.gridColumn3.DisplayFormat.FormatString = "N2";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn3.FieldName = "BalanceQty";
            this.gridColumn3.MinWidth = 100;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 100;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "UOM";
            this.gridColumn4.FieldName = "UnitCode";
            this.gridColumn4.MinWidth = 70;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 80;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "BUY RATE";
            this.gridColumn5.DisplayFormat.FormatString = "N2";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "BuyRate";
            this.gridColumn5.MinWidth = 80;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 80;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "SALES RATE";
            this.gridColumn6.DisplayFormat.FormatString = "N2";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn6.FieldName = "SalesRate";
            this.gridColumn6.MinWidth = 80;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 80;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "GROUP";
            this.gridColumn7.FieldName = "GroupName";
            this.gridColumn7.MinWidth = 120;
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            this.gridColumn7.Width = 120;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "SUB -GROUP";
            this.gridColumn8.FieldName = "SubGroupName";
            this.gridColumn8.MinWidth = 120;
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 7;
            this.gridColumn8.Width = 120;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "ALT UNIT";
            this.gridColumn9.FieldName = "HasAltUnit";
            this.gridColumn9.MinWidth = 70;
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 8;
            this.gridColumn9.Width = 70;
            // 
            // lblItemDisP
            // 
            this.lblItemDisP.AutoSize = true;
            this.lblItemDisP.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblItemDisP.Location = new System.Drawing.Point(1321, 16);
            this.lblItemDisP.Name = "lblItemDisP";
            this.lblItemDisP.Size = new System.Drawing.Size(29, 13);
            this.lblItemDisP.TabIndex = 251;
            this.lblItemDisP.Text = "0.00";
            // 
            // lblItemRate
            // 
            this.lblItemRate.AutoSize = true;
            this.lblItemRate.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblItemRate.Location = new System.Drawing.Point(1160, 16);
            this.lblItemRate.Name = "lblItemRate";
            this.lblItemRate.Size = new System.Drawing.Size(29, 13);
            this.lblItemRate.TabIndex = 251;
            this.lblItemRate.Text = "0.00";
            // 
            // lblItemDis
            // 
            this.lblItemDis.AutoSize = true;
            this.lblItemDis.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblItemDis.Location = new System.Drawing.Point(1240, 16);
            this.lblItemDis.Name = "lblItemDis";
            this.lblItemDis.Size = new System.Drawing.Size(29, 13);
            this.lblItemDis.TabIndex = 251;
            this.lblItemDis.Text = "0.00";
            // 
            // TxtAltUOM
            // 
            this.TxtAltUOM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAltUOM.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAltUOM.Location = new System.Drawing.Point(843, 10);
            this.TxtAltUOM.MaxLength = 255;
            this.TxtAltUOM.Name = "TxtAltUOM";
            this.TxtAltUOM.ReadOnly = true;
            this.TxtAltUOM.Size = new System.Drawing.Size(80, 26);
            this.TxtAltUOM.TabIndex = 2;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(696, 13);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(64, 20);
            this.label21.TabIndex = 339;
            this.label21.Text = "Alt Qty";
            // 
            // TxtAltQty
            // 
            this.TxtAltQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAltQty.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAltQty.Location = new System.Drawing.Point(763, 10);
            this.TxtAltQty.MaxLength = 255;
            this.TxtAltQty.Name = "TxtAltQty";
            this.TxtAltQty.Size = new System.Drawing.Size(80, 26);
            this.TxtAltQty.TabIndex = 1;
            this.TxtAltQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtAltQty.TextChanged += new System.EventHandler(this.TxtAltQty_TextChanged);
            this.TxtAltQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtAltQty_KeyDown);
            // 
            // GrpProductInfo
            // 
            this.GrpProductInfo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.GrpProductInfo.Controls.Add(this.LblRate);
            this.GrpProductInfo.Controls.Add(this.label9);
            this.GrpProductInfo.Controls.Add(this.LblStockQty);
            this.GrpProductInfo.Controls.Add(this.label8);
            this.GrpProductInfo.Controls.Add(this.LblProduct);
            this.GrpProductInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.GrpProductInfo.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.GrpProductInfo.Location = new System.Drawing.Point(0, 39);
            this.GrpProductInfo.Name = "GrpProductInfo";
            this.GrpProductInfo.Size = new System.Drawing.Size(1370, 51);
            this.GrpProductInfo.TabIndex = 0;
            this.GrpProductInfo.TabStop = false;
            this.GrpProductInfo.Text = "Product Info";
            this.GrpProductInfo.Visible = false;
            // 
            // LblRate
            // 
            this.LblRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblRate.Location = new System.Drawing.Point(772, 20);
            this.LblRate.Name = "LblRate";
            this.LblRate.Size = new System.Drawing.Size(91, 25);
            this.LblRate.TabIndex = 4;
            this.LblRate.Text = "0.00";
            this.LblRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(713, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 20);
            this.label9.TabIndex = 3;
            this.label9.Text = "Rate :";
            // 
            // LblStockQty
            // 
            this.LblStockQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblStockQty.Location = new System.Drawing.Point(610, 20);
            this.LblStockQty.Name = "LblStockQty";
            this.LblStockQty.Size = new System.Drawing.Size(99, 25);
            this.LblStockQty.TabIndex = 2;
            this.LblStockQty.Text = "0.00";
            this.LblStockQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(512, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 20);
            this.label8.TabIndex = 1;
            this.label8.Text = "Stock Qty :";
            // 
            // LblProduct
            // 
            this.LblProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblProduct.Location = new System.Drawing.Point(7, 20);
            this.LblProduct.Name = "LblProduct";
            this.LblProduct.Size = new System.Drawing.Size(485, 25);
            this.LblProduct.TabIndex = 0;
            // 
            // GrpInvoiceTotal
            // 
            this.GrpInvoiceTotal.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.GrpInvoiceTotal.Controls.Add(this.label10);
            this.GrpInvoiceTotal.Controls.Add(this.LblItemsDiscountSum);
            this.GrpInvoiceTotal.Controls.Add(this.lblRefInvoiceId);
            this.GrpInvoiceTotal.Controls.Add(this.lblTermAmount);
            this.GrpInvoiceTotal.Controls.Add(this.lblTax);
            this.GrpInvoiceTotal.Controls.Add(this.lblNonTaxable);
            this.GrpInvoiceTotal.Controls.Add(this.label23);
            this.GrpInvoiceTotal.Controls.Add(this.label22);
            this.GrpInvoiceTotal.Controls.Add(this.label14);
            this.GrpInvoiceTotal.Controls.Add(this.label13);
            this.GrpInvoiceTotal.Controls.Add(this.label12);
            this.GrpInvoiceTotal.Controls.Add(this.lblTaxable);
            this.GrpInvoiceTotal.Controls.Add(this.label11);
            this.GrpInvoiceTotal.Controls.Add(this.LblItemsNetAmount);
            this.GrpInvoiceTotal.Controls.Add(this.LblItemsTotalQty);
            this.GrpInvoiceTotal.Controls.Add(this.lbl_TotQty1);
            this.GrpInvoiceTotal.Controls.Add(this.label7);
            this.GrpInvoiceTotal.Controls.Add(this.LblItemsTotal);
            this.GrpInvoiceTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.GrpInvoiceTotal.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.GrpInvoiceTotal.Location = new System.Drawing.Point(0, 90);
            this.GrpInvoiceTotal.Name = "GrpInvoiceTotal";
            this.GrpInvoiceTotal.Size = new System.Drawing.Size(1370, 52);
            this.GrpInvoiceTotal.TabIndex = 0;
            this.GrpInvoiceTotal.TabStop = false;
            this.GrpInvoiceTotal.Text = "Invoice Total";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(460, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 20);
            this.label10.TabIndex = 253;
            this.label10.Text = "Discount";
            // 
            // LblItemsDiscountSum
            // 
            this.LblItemsDiscountSum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblItemsDiscountSum.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblItemsDiscountSum.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblItemsDiscountSum.Location = new System.Drawing.Point(540, 20);
            this.LblItemsDiscountSum.Name = "LblItemsDiscountSum";
            this.LblItemsDiscountSum.Size = new System.Drawing.Size(126, 28);
            this.LblItemsDiscountSum.TabIndex = 252;
            this.LblItemsDiscountSum.Text = "0.00";
            this.LblItemsDiscountSum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRefInvoiceId
            // 
            this.lblRefInvoiceId.AutoSize = true;
            this.lblRefInvoiceId.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblRefInvoiceId.Location = new System.Drawing.Point(1438, 14);
            this.lblRefInvoiceId.Name = "lblRefInvoiceId";
            this.lblRefInvoiceId.Size = new System.Drawing.Size(29, 13);
            this.lblRefInvoiceId.TabIndex = 251;
            this.lblRefInvoiceId.Text = "0.00";
            // 
            // lblTermAmount
            // 
            this.lblTermAmount.AutoSize = true;
            this.lblTermAmount.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblTermAmount.Location = new System.Drawing.Point(1256, 14);
            this.lblTermAmount.Name = "lblTermAmount";
            this.lblTermAmount.Size = new System.Drawing.Size(29, 13);
            this.lblTermAmount.TabIndex = 251;
            this.lblTermAmount.Text = "0.00";
            // 
            // lblTax
            // 
            this.lblTax.AutoSize = true;
            this.lblTax.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblTax.Location = new System.Drawing.Point(1130, 14);
            this.lblTax.Name = "lblTax";
            this.lblTax.Size = new System.Drawing.Size(29, 13);
            this.lblTax.TabIndex = 251;
            this.lblTax.Text = "0.00";
            // 
            // lblNonTaxable
            // 
            this.lblNonTaxable.AutoSize = true;
            this.lblNonTaxable.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblNonTaxable.Location = new System.Drawing.Point(1029, 14);
            this.lblNonTaxable.Name = "lblNonTaxable";
            this.lblNonTaxable.Size = new System.Drawing.Size(29, 13);
            this.lblNonTaxable.TabIndex = 251;
            this.lblNonTaxable.Text = "0.00";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label23.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label23.Location = new System.Drawing.Point(1438, 31);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(39, 13);
            this.label23.TabIndex = 251;
            this.label23.Text = "Ref Bill";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label22.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label22.Location = new System.Drawing.Point(1256, 31);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(70, 13);
            this.label22.TabIndex = 251;
            this.label22.Text = "Term amount";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label14.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label14.Location = new System.Drawing.Point(1130, 31);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(25, 13);
            this.label14.TabIndex = 251;
            this.label14.Text = "Tax";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label13.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label13.Location = new System.Drawing.Point(1013, 31);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 13);
            this.label13.TabIndex = 251;
            this.label13.Text = "NonTaxable";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label12.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label12.Location = new System.Drawing.Point(933, 31);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 13);
            this.label12.TabIndex = 251;
            this.label12.Text = "Taxable";
            // 
            // lblTaxable
            // 
            this.lblTaxable.AutoSize = true;
            this.lblTaxable.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblTaxable.Location = new System.Drawing.Point(933, 14);
            this.lblTaxable.Name = "lblTaxable";
            this.lblTaxable.Size = new System.Drawing.Size(29, 13);
            this.lblTaxable.TabIndex = 251;
            this.lblTaxable.Text = "0.00";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(669, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(116, 20);
            this.label11.TabIndex = 251;
            this.label11.Text = "Total Net Amt";
            // 
            // LblItemsNetAmount
            // 
            this.LblItemsNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblItemsNetAmount.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblItemsNetAmount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblItemsNetAmount.Location = new System.Drawing.Point(788, 20);
            this.LblItemsNetAmount.Name = "LblItemsNetAmount";
            this.LblItemsNetAmount.Size = new System.Drawing.Size(135, 28);
            this.LblItemsNetAmount.TabIndex = 250;
            this.LblItemsNetAmount.Text = "0.00";
            this.LblItemsNetAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblItemsTotalQty
            // 
            this.LblItemsTotalQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblItemsTotalQty.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblItemsTotalQty.Location = new System.Drawing.Point(89, 20);
            this.LblItemsTotalQty.Name = "LblItemsTotalQty";
            this.LblItemsTotalQty.Size = new System.Drawing.Size(126, 28);
            this.LblItemsTotalQty.TabIndex = 249;
            this.LblItemsTotalQty.Text = "0.00";
            this.LblItemsTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_TotQty1
            // 
            this.lbl_TotQty1.AutoSize = true;
            this.lbl_TotQty1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TotQty1.Location = new System.Drawing.Point(4, 24);
            this.lbl_TotQty1.Name = "lbl_TotQty1";
            this.lbl_TotQty1.Size = new System.Drawing.Size(85, 20);
            this.lbl_TotQty1.TabIndex = 248;
            this.lbl_TotQty1.Text = "Total  Qty";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(215, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 20);
            this.label7.TabIndex = 247;
            this.label7.Text = "Basic Amount";
            // 
            // LblItemsTotal
            // 
            this.LblItemsTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblItemsTotal.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblItemsTotal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblItemsTotal.Location = new System.Drawing.Point(334, 20);
            this.LblItemsTotal.Name = "LblItemsTotal";
            this.LblItemsTotal.Size = new System.Drawing.Size(126, 28);
            this.LblItemsTotal.TabIndex = 246;
            this.LblItemsTotal.Text = "0.00";
            this.LblItemsTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.DGrid);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.glkupMembers);
            this.splitContainerMain.Panel2.Controls.Add(this.btnReverse);
            this.splitContainerMain.Panel2.Controls.Add(this.btnReprintLast);
            this.splitContainerMain.Panel2.Controls.Add(this.BtnHoldList);
            this.splitContainerMain.Panel2.Controls.Add(this.GrpButtonDetails);
            this.splitContainerMain.Panel2.Controls.Add(this.GrpInvoiceTotalDetails);
            this.splitContainerMain.Panel2.Controls.Add(this.clsSeparator5);
            this.splitContainerMain.Panel2.Controls.Add(this.GrpMemberType);
            this.splitContainerMain.Panel2.Controls.Add(this.label3);
            this.splitContainerMain.Panel2.Controls.Add(this.clsSeparator1);
            this.splitContainerMain.Size = new System.Drawing.Size(1370, 724);
            this.splitContainerMain.SplitterDistance = 1010;
            this.splitContainerMain.TabIndex = 1;
            // 
            // glkupMembers
            // 
            this.glkupMembers.EditValue = "";
            this.glkupMembers.Location = new System.Drawing.Point(89, 41);
            this.glkupMembers.Name = "glkupMembers";
            this.glkupMembers.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.glkupMembers.Properties.Appearance.Options.UseFont = true;
            this.glkupMembers.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.glkupMembers.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkupMembers.Properties.DataSource = this.bsMembersList;
            this.glkupMembers.Properties.NullText = "";
            this.glkupMembers.Properties.PopupView = this.gridLookUpEdit1View;
            this.glkupMembers.Size = new System.Drawing.Size(260, 28);
            this.glkupMembers.TabIndex = 335;
            this.glkupMembers.EditValueChanged += new System.EventHandler(this.glkupMembers_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcDescription,
            this.gcShortName,
            this.gcMemberType,
            this.gcStartDate,
            this.gcExpireDate,
            this.gcPhone,
            this.gcEmail,
            this.gcDiscount,
            this.gcBalance});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gcDescription
            // 
            this.gcDescription.Caption = "DESCRIPTION";
            this.gcDescription.FieldName = "MShipDesc";
            this.gcDescription.Name = "gcDescription";
            this.gcDescription.Visible = true;
            this.gcDescription.VisibleIndex = 0;
            // 
            // gcShortName
            // 
            this.gcShortName.Caption = "SHORT NAME";
            this.gcShortName.FieldName = "MShipShortName";
            this.gcShortName.Name = "gcShortName";
            this.gcShortName.Visible = true;
            this.gcShortName.VisibleIndex = 1;
            // 
            // gcMemberType
            // 
            this.gcMemberType.Caption = "MEMBER TYPE";
            this.gcMemberType.FieldName = "MemberType";
            this.gcMemberType.Name = "gcMemberType";
            this.gcMemberType.Visible = true;
            this.gcMemberType.VisibleIndex = 2;
            // 
            // gcStartDate
            // 
            this.gcStartDate.Caption = "START DATE";
            this.gcStartDate.FieldName = "MValidDate";
            this.gcStartDate.Name = "gcStartDate";
            this.gcStartDate.Visible = true;
            this.gcStartDate.VisibleIndex = 3;
            // 
            // gcExpireDate
            // 
            this.gcExpireDate.Caption = "EXPIRE DATE";
            this.gcExpireDate.FieldName = "MExpireDate";
            this.gcExpireDate.Name = "gcExpireDate";
            this.gcExpireDate.Visible = true;
            this.gcExpireDate.VisibleIndex = 4;
            // 
            // gcPhone
            // 
            this.gcPhone.Caption = "PHONE";
            this.gcPhone.FieldName = "PhoneNo";
            this.gcPhone.Name = "gcPhone";
            this.gcPhone.Visible = true;
            this.gcPhone.VisibleIndex = 5;
            // 
            // gcEmail
            // 
            this.gcEmail.Caption = "EMAIL";
            this.gcEmail.FieldName = "EmailAdd";
            this.gcEmail.Name = "gcEmail";
            this.gcEmail.Visible = true;
            this.gcEmail.VisibleIndex = 6;
            // 
            // gcDiscount
            // 
            this.gcDiscount.Caption = "Discount %";
            this.gcDiscount.FieldName = "DiscountPercent";
            this.gcDiscount.Name = "gcDiscount";
            this.gcDiscount.Visible = true;
            this.gcDiscount.VisibleIndex = 8;
            // 
            // gcBalance
            // 
            this.gcBalance.Caption = "Balance";
            this.gcBalance.FieldName = "Balance";
            this.gcBalance.Name = "gcBalance";
            this.gcBalance.Visible = true;
            this.gcBalance.VisibleIndex = 7;
            // 
            // btnReverse
            // 
            this.btnReverse.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.btnReverse.Appearance.Options.UseFont = true;
            this.btnReverse.Location = new System.Drawing.Point(226, 3);
            this.btnReverse.Name = "btnReverse";
            this.btnReverse.Size = new System.Drawing.Size(115, 32);
            this.btnReverse.TabIndex = 334;
            this.btnReverse.Text = "REVERSE";
            this.btnReverse.Click += new System.EventHandler(this.btnReverse_Click);
            // 
            // btnReprintLast
            // 
            this.btnReprintLast.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.btnReprintLast.Appearance.Options.UseFont = true;
            this.btnReprintLast.Location = new System.Drawing.Point(105, 3);
            this.btnReprintLast.Name = "btnReprintLast";
            this.btnReprintLast.Size = new System.Drawing.Size(115, 32);
            this.btnReprintLast.TabIndex = 334;
            this.btnReprintLast.Text = "REPRINT LAST";
            this.btnReprintLast.Click += new System.EventHandler(this.btnReprintLast_Click);
            // 
            // BtnHoldList
            // 
            this.BtnHoldList.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.BtnHoldList.Appearance.Options.UseFont = true;
            this.BtnHoldList.Location = new System.Drawing.Point(3, 3);
            this.BtnHoldList.Name = "BtnHoldList";
            this.BtnHoldList.Size = new System.Drawing.Size(96, 32);
            this.BtnHoldList.TabIndex = 334;
            this.BtnHoldList.Text = "HOLD LIST";
            this.BtnHoldList.Click += new System.EventHandler(this.BtnHoldList_Click);
            // 
            // clsSeparator5
            // 
            this.clsSeparator5.BackColor = System.Drawing.Color.Lavender;
            this.clsSeparator5.Location = new System.Drawing.Point(3, 70);
            this.clsSeparator5.Name = "clsSeparator5";
            this.clsSeparator5.Size = new System.Drawing.Size(346, 3);
            this.clsSeparator5.TabIndex = 312;
            this.clsSeparator5.TabStop = false;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.Lavender;
            this.clsSeparator1.Location = new System.Drawing.Point(3, 38);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(346, 3);
            this.clsSeparator1.TabIndex = 311;
            this.clsSeparator1.TabStop = false;
            // 
            // statusStripMain
            // 
            this.statusStripMain.Font = new System.Drawing.Font("Bookman Old Style", 9F);
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slBranch,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel6,
            this.slUser,
            this.toolStripStatusLabel10,
            this.slToday,
            this.toolStripStatusLabel12,
            this.slMiti,
            this.toolStripStatusLabel9,
            this.slNextInvoice,
            this.toolStripStatusLabel4,
            this.slLastInvoice,
            this.toolStripStatusLabel2,
            this.slLastGrandTotal,
            this.toolStripStatusLabel7,
            this.slLastTender,
            this.toolStripStatusLabel11,
            this.slLastReturn});
            this.statusStripMain.Location = new System.Drawing.Point(0, 724);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(1370, 25);
            this.statusStripMain.SizingGrip = false;
            this.statusStripMain.TabIndex = 4;
            // 
            // slBranch
            // 
            this.slBranch.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.slBranch.Name = "slBranch";
            this.slBranch.Size = new System.Drawing.Size(20, 20);
            this.slBranch.Text = "..";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(57, 20);
            this.toolStripStatusLabel3.Text = "Counter";
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(36, 20);
            this.toolStripStatusLabel6.Text = "User";
            // 
            // slUser
            // 
            this.slUser.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.slUser.Name = "slUser";
            this.slUser.Size = new System.Drawing.Size(20, 20);
            this.slUser.Text = "..";
            // 
            // toolStripStatusLabel10
            // 
            this.toolStripStatusLabel10.Name = "toolStripStatusLabel10";
            this.toolStripStatusLabel10.Size = new System.Drawing.Size(37, 20);
            this.toolStripStatusLabel10.Text = "Date";
            // 
            // slToday
            // 
            this.slToday.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.slToday.Name = "slToday";
            this.slToday.Size = new System.Drawing.Size(20, 20);
            this.slToday.Text = "..";
            // 
            // toolStripStatusLabel12
            // 
            this.toolStripStatusLabel12.Name = "toolStripStatusLabel12";
            this.toolStripStatusLabel12.Size = new System.Drawing.Size(32, 20);
            this.toolStripStatusLabel12.Text = "Miti";
            // 
            // slMiti
            // 
            this.slMiti.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.slMiti.Name = "slMiti";
            this.slMiti.Size = new System.Drawing.Size(20, 20);
            this.slMiti.Text = "..";
            // 
            // toolStripStatusLabel9
            // 
            this.toolStripStatusLabel9.Name = "toolStripStatusLabel9";
            this.toolStripStatusLabel9.Size = new System.Drawing.Size(84, 20);
            this.toolStripStatusLabel9.Text = "Next Invoice";
            // 
            // slNextInvoice
            // 
            this.slNextInvoice.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.slNextInvoice.Name = "slNextInvoice";
            this.slNextInvoice.Size = new System.Drawing.Size(20, 20);
            this.slNextInvoice.Text = "..";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(81, 20);
            this.toolStripStatusLabel4.Text = "Last Invoice";
            // 
            // slLastInvoice
            // 
            this.slLastInvoice.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.slLastInvoice.Name = "slLastInvoice";
            this.slLastInvoice.Size = new System.Drawing.Size(20, 20);
            this.slLastInvoice.Text = "..";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(109, 20);
            this.toolStripStatusLabel2.Text = "Last Grand Total";
            // 
            // slLastGrandTotal
            // 
            this.slLastGrandTotal.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.slLastGrandTotal.Name = "slLastGrandTotal";
            this.slLastGrandTotal.Size = new System.Drawing.Size(20, 20);
            this.slLastGrandTotal.Text = "..";
            // 
            // toolStripStatusLabel7
            // 
            this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
            this.toolStripStatusLabel7.Size = new System.Drawing.Size(79, 20);
            this.toolStripStatusLabel7.Text = "Last Tender";
            // 
            // slLastTender
            // 
            this.slLastTender.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.slLastTender.Name = "slLastTender";
            this.slLastTender.Size = new System.Drawing.Size(20, 20);
            this.slLastTender.Text = "..";
            // 
            // toolStripStatusLabel11
            // 
            this.toolStripStatusLabel11.Name = "toolStripStatusLabel11";
            this.toolStripStatusLabel11.Size = new System.Drawing.Size(80, 20);
            this.toolStripStatusLabel11.Text = "Last Return";
            // 
            // slLastReturn
            // 
            this.slLastReturn.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.slLastReturn.Name = "slLastReturn";
            this.slLastReturn.Size = new System.Drawing.Size(20, 20);
            this.slLastReturn.Text = "..";
            // 
            // FrmPointOfSales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.statusStripMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Name = "FrmPointOfSales";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POINT OF SALES";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmPointOfSales_Load);
            this.Shown += new System.EventHandler(this.FrmPointOfSales_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPointOfSales_KeyDown);
            this.GrpMemberType.ResumeLayout(false);
            this.GrpMemberType.PerformLayout();
            this.GrpInvoiceTotalDetails.ResumeLayout(false);
            this.GrpInvoiceTotalDetails.PerformLayout();
            this.GrpButtonDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsInvoiceItems)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.GrpSelectedProduct.ResumeLayout(false);
            this.GrpSelectedProduct.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkupProducts.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.GrpProductInfo.ResumeLayout(false);
            this.GrpProductInfo.PerformLayout();
            this.GrpInvoiceTotal.ResumeLayout(false);
            this.GrpInvoiceTotal.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            this.splitContainerMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.glkupMembers.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMembersList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ClsSeparator clsSeparator5;
        private System.Windows.Forms.Label label3;
        private ClsSeparator clsSeparator1;
        private System.Windows.Forms.TextBox TxtBarcode;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox TxtUOM;
        private System.Windows.Forms.GroupBox GrpInvoiceTotalDetails;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtBillAmount;
        private ClsSeparator clsSeparator2;
        private System.Windows.Forms.TextBox TxtDiscount;
        private System.Windows.Forms.TextBox TxtGrandTotal;
        private System.Windows.Forms.TextBox TxtDiscountPercentage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox GrpButtonDetails;
        private DevExpress.XtraEditors.SimpleButton BtnReset;
        private DevExpress.XtraEditors.SimpleButton BtnCash;
        private DevExpress.XtraEditors.SimpleButton BtnHold;
        private System.Windows.Forms.TextBox TxtQty;
        private System.Windows.Forms.DataGridView DGrid;
        private System.Windows.Forms.GroupBox GrpMemberType;
        private System.Windows.Forms.Label LblMemberAmount;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label LblMemberType;
        private System.Windows.Forms.Label LblMemberShortName;
        private System.Windows.Forms.Label LblMemberName;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox GrpInvoiceTotal;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label LblItemsDiscountSum;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label LblItemsNetAmount;
        private System.Windows.Forms.Label LblItemsTotalQty;
        private System.Windows.Forms.Label lbl_TotQty1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label LblItemsTotal;
        private System.Windows.Forms.GroupBox GrpProductInfo;
        private System.Windows.Forms.Label LblRate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label LblStockQty;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label LblProduct;
        private System.Windows.Forms.GroupBox GrpSelectedProduct;
        private System.Windows.Forms.Label LblPriceTag;
        private System.Windows.Forms.Label lbl00001;
        private System.Windows.Forms.TextBox TxtAltUOM;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox TxtAltQty;
        private DevExpress.XtraEditors.SimpleButton BtnNext;
        private System.Windows.Forms.BindingSource bsInvoiceItems;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.ToolStripStatusLabel slBranch;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel slUser;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel slLastInvoice;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel9;
        private System.Windows.Forms.ToolStripStatusLabel slNextInvoice;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel10;
        private System.Windows.Forms.ToolStripStatusLabel slToday;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel12;
        private System.Windows.Forms.ToolStripStatusLabel slMiti;
        private System.Windows.Forms.Label lblTax;
        private System.Windows.Forms.Label lblNonTaxable;
        private System.Windows.Forms.Label lblTaxable;
        private DevExpress.XtraEditors.SimpleButton BtnHoldList;
        private DevExpress.XtraEditors.SimpleButton btnReverse;
        private DevExpress.XtraEditors.SimpleButton btnReprintLast;
        private DevExpress.XtraEditors.GridLookUpEdit glkupMembers;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gcDescription;
        private DevExpress.XtraGrid.Columns.GridColumn gcShortName;
        private DevExpress.XtraGrid.Columns.GridColumn gcMemberType;
        private DevExpress.XtraGrid.Columns.GridColumn gcStartDate;
        private DevExpress.XtraGrid.Columns.GridColumn gcExpireDate;
        private DevExpress.XtraGrid.Columns.GridColumn gcPhone;
        private DevExpress.XtraGrid.Columns.GridColumn gcEmail;
        private System.Windows.Forms.BindingSource bsMembersList;
        private DevExpress.XtraGrid.Columns.GridColumn gcBalance;
        private DevExpress.XtraGrid.Columns.GridColumn gcDiscount;
        private DevExpress.XtraEditors.GridLookUpEdit glkupProducts;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource bsProducts;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraEditors.SimpleButton BtnCancelItemEdit;
        private System.Windows.Forms.Label LblMemberPhone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTermAmount;
        private System.Windows.Forms.Label lblRefInvoiceId;
        private System.Windows.Forms.Label lblItemDis;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShortName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAltQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAltUom;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUOM;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNetAmount;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lblItemDisP;
        private System.Windows.Forms.Label lblItemRate;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel slLastGrandTotal;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
        private System.Windows.Forms.ToolStripStatusLabel slLastTender;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel11;
        private System.Windows.Forms.ToolStripStatusLabel slLastReturn;
    }
}