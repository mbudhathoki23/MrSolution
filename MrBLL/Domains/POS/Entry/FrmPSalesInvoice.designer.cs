using System.Windows.Forms;
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.POS.Entry
{
    partial class FrmPSalesInvoice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPSalesInvoice));
            this.label21 = new System.Windows.Forms.Label();
            this.LblItemsDiscountSum = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.LblItemsTotal = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.LblItemsTotalQty = new System.Windows.Forms.Label();
            this.LblItemsNetAmount = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.bsProduct = new System.Windows.Forms.BindingSource(this.components);
            this.tlpBillSummary = new System.Windows.Forms.TableLayoutPanel();
            this.PanelHeader = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.DataGridGroup = new System.Windows.Forms.Panel();
            this.PDetails = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.GrpCustomer = new System.Windows.Forms.Panel();
            this.TxtBasicAmount = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.MemberGroup = new System.Windows.Forms.GroupBox();
            this.LblTag = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.LblMemberAmount = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.LblMemberType = new System.Windows.Forms.Label();
            this.LblMemberShortName = new System.Windows.Forms.Label();
            this.LblMemberName = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.TxtMember = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtNetAmount = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.ChkTaxInvoice = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtTenderAmount = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.btnMember = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.ChkPrint = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.TxtBillDiscountAmount = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtChangeAmount = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtBillDiscountPercentage = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CustomerDetails = new System.Windows.Forms.Panel();
            this.lblCreditDays = new System.Windows.Forms.Label();
            this.lblPan = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.lbl_Currentbal = new System.Windows.Forms.Label();
            this.lblCrLimit = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lbl_CurrentBalance = new System.Windows.Forms.Label();
            this.lbl_Remarks = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.TxtRemarks = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.LblNumberInWords = new System.Windows.Forms.Label();
            this.CmbPaymentType = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.BtnHold = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_CBLedger = new System.Windows.Forms.Label();
            this.btnCustomer = new System.Windows.Forms.Button();
            this.TxtCustomer = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label111 = new System.Windows.Forms.Label();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.PnlInvoiceDetails = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.label36 = new System.Windows.Forms.Label();
            this.lblTermAmount = new System.Windows.Forms.Label();
            this.LblDisplayReturnAmount = new System.Windows.Forms.Label();
            this.lblTax = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.lblNonTaxable = new System.Windows.Forms.Label();
            this.LblDisplayReceivedAmount = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.lblTaxable = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.RGrid = new System.Windows.Forms.DataGridView();
            this.PnlProductDetails = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.LblStockQty = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.LblProduct = new System.Windows.Forms.Label();
            this.LblBarcode = new System.Windows.Forms.Label();
            this.LblSalesRate = new System.Windows.Forms.Label();
            this.LblUnit = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlTop = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.TxtQty = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtRefVno = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mENUSETUPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuReturnInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuReverseInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuExchangeInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuRePrintInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuCashVoucher = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuLockScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuDayClosing = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuTodaysSalesReports = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuHoldInvoiceList = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.TxtAltQty = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.TxtAltUnit = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.ListOfProduct = new System.Windows.Forms.Label();
            this.lbl_VoucherNo = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.BtnRefVno = new System.Windows.Forms.Button();
            this.BtnVno = new System.Windows.Forms.Button();
            this.LblInvoiceNo = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.BtnCounter = new System.Windows.Forms.Button();
            this.TxtUnit = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtCounter = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtBarcode = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtVno = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.label12 = new System.Windows.Forms.Label();
            this.MskDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.MskMiti = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.bsProduct)).BeginInit();
            this.tlpBillSummary.SuspendLayout();
            this.PanelHeader.SuspendLayout();
            this.DataGridGroup.SuspendLayout();
            this.PDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.GrpCustomer.SuspendLayout();
            this.MemberGroup.SuspendLayout();
            this.CustomerDetails.SuspendLayout();
            this.PnlInvoiceDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).BeginInit();
            this.PnlProductDetails.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label21.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(483, 2);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(94, 28);
            this.label21.TabIndex = 253;
            this.label21.Text = "Discount";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblItemsDiscountSum
            // 
            this.LblItemsDiscountSum.BackColor = System.Drawing.Color.White;
            this.LblItemsDiscountSum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblItemsDiscountSum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblItemsDiscountSum.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblItemsDiscountSum.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblItemsDiscountSum.Location = new System.Drawing.Point(585, 2);
            this.LblItemsDiscountSum.Name = "LblItemsDiscountSum";
            this.LblItemsDiscountSum.Size = new System.Drawing.Size(114, 28);
            this.LblItemsDiscountSum.TabIndex = 252;
            this.LblItemsDiscountSum.Text = "0.00";
            this.LblItemsDiscountSum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label32
            // 
            this.label32.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label32.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(229, 2);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(124, 28);
            this.label32.TabIndex = 247;
            this.label32.Text = "Basic Amount";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblItemsTotal
            // 
            this.LblItemsTotal.BackColor = System.Drawing.Color.White;
            this.LblItemsTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblItemsTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblItemsTotal.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblItemsTotal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblItemsTotal.Location = new System.Drawing.Point(361, 2);
            this.LblItemsTotal.Name = "LblItemsTotal";
            this.LblItemsTotal.Size = new System.Drawing.Size(114, 28);
            this.LblItemsTotal.TabIndex = 246;
            this.LblItemsTotal.Text = "0.00";
            this.LblItemsTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label31
            // 
            this.label31.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label31.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label31.Location = new System.Drawing.Point(5, 2);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(94, 28);
            this.label31.TabIndex = 248;
            this.label31.Text = "Total  Qty";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblItemsTotalQty
            // 
            this.LblItemsTotalQty.BackColor = System.Drawing.Color.White;
            this.LblItemsTotalQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblItemsTotalQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblItemsTotalQty.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblItemsTotalQty.Location = new System.Drawing.Point(107, 2);
            this.LblItemsTotalQty.Name = "LblItemsTotalQty";
            this.LblItemsTotalQty.Size = new System.Drawing.Size(114, 28);
            this.LblItemsTotalQty.TabIndex = 249;
            this.LblItemsTotalQty.Text = "0.00";
            this.LblItemsTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblItemsNetAmount
            // 
            this.LblItemsNetAmount.BackColor = System.Drawing.Color.White;
            this.LblItemsNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblItemsNetAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblItemsNetAmount.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblItemsNetAmount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblItemsNetAmount.Location = new System.Drawing.Point(839, 2);
            this.LblItemsNetAmount.Name = "LblItemsNetAmount";
            this.LblItemsNetAmount.Size = new System.Drawing.Size(114, 28);
            this.LblItemsNetAmount.TabIndex = 250;
            this.LblItemsNetAmount.Text = "0.00";
            this.LblItemsNetAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label29.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(707, 2);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(124, 28);
            this.label29.TabIndex = 251;
            this.label29.Text = "Total Net Amt";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlpBillSummary
            // 
            this.tlpBillSummary.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tlpBillSummary.ColumnCount = 9;
            this.tlpBillSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpBillSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpBillSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tlpBillSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpBillSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpBillSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpBillSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tlpBillSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpBillSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpBillSummary.Controls.Add(this.LblItemsDiscountSum, 5, 0);
            this.tlpBillSummary.Controls.Add(this.label21, 4, 0);
            this.tlpBillSummary.Controls.Add(this.LblItemsNetAmount, 7, 0);
            this.tlpBillSummary.Controls.Add(this.label29, 6, 0);
            this.tlpBillSummary.Controls.Add(this.LblItemsTotal, 3, 0);
            this.tlpBillSummary.Controls.Add(this.label32, 2, 0);
            this.tlpBillSummary.Controls.Add(this.LblItemsTotalQty, 1, 0);
            this.tlpBillSummary.Controls.Add(this.label31, 0, 0);
            this.tlpBillSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tlpBillSummary.Location = new System.Drawing.Point(0, 700);
            this.tlpBillSummary.Name = "tlpBillSummary";
            this.tlpBillSummary.RowCount = 1;
            this.tlpBillSummary.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBillSummary.Size = new System.Drawing.Size(1250, 32);
            this.tlpBillSummary.TabIndex = 308;
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.DataGridGroup);
            this.PanelHeader.Controls.Add(this.pnlTop);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(1250, 700);
            this.PanelHeader.TabIndex = 0;
            // 
            // DataGridGroup
            // 
            this.DataGridGroup.Controls.Add(this.PDetails);
            this.DataGridGroup.Controls.Add(this.PnlInvoiceDetails);
            this.DataGridGroup.Controls.Add(this.RGrid);
            this.DataGridGroup.Controls.Add(this.PnlProductDetails);
            this.DataGridGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridGroup.Location = new System.Drawing.Point(0, 101);
            this.DataGridGroup.Name = "DataGridGroup";
            this.DataGridGroup.Size = new System.Drawing.Size(1250, 599);
            this.DataGridGroup.TabIndex = 2;
            // 
            // PDetails
            // 
            this.PDetails.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PDetails.Controls.Add(this.splitContainer);
            this.PDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PDetails.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PDetails.Location = new System.Drawing.Point(0, 0);
            this.PDetails.Name = "PDetails";
            this.PDetails.Size = new System.Drawing.Size(1250, 516);
            this.PDetails.TabIndex = 303;
            this.PDetails.Paint += new System.Windows.Forms.PaintEventHandler(this.PDetails_Paint);
            this.PDetails.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PDetails_PreviewKeyDown);
            // 
            // splitContainer
            // 
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.GrpCustomer);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.CustomerDetails);
            this.splitContainer.Panel2.Controls.Add(this.lbl_Remarks);
            this.splitContainer.Panel2.Controls.Add(this.label37);
            this.splitContainer.Panel2.Controls.Add(this.TxtRemarks);
            this.splitContainer.Panel2.Controls.Add(this.LblNumberInWords);
            this.splitContainer.Panel2.Controls.Add(this.CmbPaymentType);
            this.splitContainer.Panel2.Controls.Add(this.label30);
            this.splitContainer.Panel2.Controls.Add(this.BtnHold);
            this.splitContainer.Panel2.Controls.Add(this.lbl_CBLedger);
            this.splitContainer.Panel2.Controls.Add(this.btnCustomer);
            this.splitContainer.Panel2.Controls.Add(this.TxtCustomer);
            this.splitContainer.Panel2.Controls.Add(this.label111);
            this.splitContainer.Panel2.Controls.Add(this.BtnSave);
            this.splitContainer.Panel2MinSize = 430;
            this.splitContainer.Size = new System.Drawing.Size(1250, 516);
            this.splitContainer.SplitterDistance = 755;
            this.splitContainer.TabIndex = 355;
            // 
            // GrpCustomer
            // 
            this.GrpCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GrpCustomer.Controls.Add(this.TxtBasicAmount);
            this.GrpCustomer.Controls.Add(this.label8);
            this.GrpCustomer.Controls.Add(this.MemberGroup);
            this.GrpCustomer.Controls.Add(this.TxtMember);
            this.GrpCustomer.Controls.Add(this.TxtNetAmount);
            this.GrpCustomer.Controls.Add(this.ChkTaxInvoice);
            this.GrpCustomer.Controls.Add(this.label7);
            this.GrpCustomer.Controls.Add(this.label6);
            this.GrpCustomer.Controls.Add(this.TxtTenderAmount);
            this.GrpCustomer.Controls.Add(this.btnMember);
            this.GrpCustomer.Controls.Add(this.label5);
            this.GrpCustomer.Controls.Add(this.ChkPrint);
            this.GrpCustomer.Controls.Add(this.label9);
            this.GrpCustomer.Controls.Add(this.TxtBillDiscountAmount);
            this.GrpCustomer.Controls.Add(this.TxtChangeAmount);
            this.GrpCustomer.Controls.Add(this.TxtBillDiscountPercentage);
            this.GrpCustomer.Controls.Add(this.label4);
            this.GrpCustomer.Controls.Add(this.label2);
            this.GrpCustomer.Location = new System.Drawing.Point(0, 0);
            this.GrpCustomer.Name = "GrpCustomer";
            this.GrpCustomer.Size = new System.Drawing.Size(565, 405);
            this.GrpCustomer.TabIndex = 0;
            // 
            // TxtBasicAmount
            // 
            this.TxtBasicAmount.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtBasicAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBasicAmount.Font = new System.Drawing.Font("Bookman Old Style", 16F, System.Drawing.FontStyle.Bold);
            this.TxtBasicAmount.Location = new System.Drawing.Point(161, 61);
            this.TxtBasicAmount.MaxLength = 255;
            this.TxtBasicAmount.Name = "TxtBasicAmount";
            this.TxtBasicAmount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtBasicAmount.Size = new System.Drawing.Size(399, 33);
            this.TxtBasicAmount.TabIndex = 0;
            this.TxtBasicAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Bookman Old Style", 16F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(6, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 25);
            this.label8.TabIndex = 333;
            this.label8.Text = "Member";
            // 
            // MemberGroup
            // 
            this.MemberGroup.Controls.Add(this.LblTag);
            this.MemberGroup.Controls.Add(this.label26);
            this.MemberGroup.Controls.Add(this.LblMemberAmount);
            this.MemberGroup.Controls.Add(this.label18);
            this.MemberGroup.Controls.Add(this.LblMemberType);
            this.MemberGroup.Controls.Add(this.LblMemberShortName);
            this.MemberGroup.Controls.Add(this.LblMemberName);
            this.MemberGroup.Controls.Add(this.label17);
            this.MemberGroup.Controls.Add(this.label16);
            this.MemberGroup.Controls.Add(this.label15);
            this.MemberGroup.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.MemberGroup.Location = new System.Drawing.Point(0, 271);
            this.MemberGroup.Name = "MemberGroup";
            this.MemberGroup.Size = new System.Drawing.Size(563, 132);
            this.MemberGroup.TabIndex = 307;
            this.MemberGroup.TabStop = false;
            this.MemberGroup.Text = "Member Information";
            // 
            // LblTag
            // 
            this.LblTag.BackColor = System.Drawing.SystemColors.Window;
            this.LblTag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTag.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTag.ForeColor = System.Drawing.Color.Black;
            this.LblTag.Location = new System.Drawing.Point(421, 52);
            this.LblTag.Name = "LblTag";
            this.LblTag.Size = new System.Drawing.Size(139, 23);
            this.LblTag.TabIndex = 160;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.Black;
            this.label26.Location = new System.Drawing.Point(358, 54);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(56, 20);
            this.label26.TabIndex = 159;
            this.label26.Text = "Type :";
            // 
            // LblMemberAmount
            // 
            this.LblMemberAmount.BackColor = System.Drawing.SystemColors.Window;
            this.LblMemberAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblMemberAmount.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMemberAmount.ForeColor = System.Drawing.Color.Black;
            this.LblMemberAmount.Location = new System.Drawing.Point(128, 104);
            this.LblMemberAmount.Name = "LblMemberAmount";
            this.LblMemberAmount.Size = new System.Drawing.Size(154, 24);
            this.LblMemberAmount.TabIndex = 158;
            this.LblMemberAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Black;
            this.label18.Location = new System.Drawing.Point(6, 103);
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
            this.LblMemberType.Location = new System.Drawing.Point(128, 78);
            this.LblMemberType.Name = "LblMemberType";
            this.LblMemberType.Size = new System.Drawing.Size(154, 23);
            this.LblMemberType.TabIndex = 156;
            // 
            // LblMemberShortName
            // 
            this.LblMemberShortName.BackColor = System.Drawing.SystemColors.Window;
            this.LblMemberShortName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblMemberShortName.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMemberShortName.ForeColor = System.Drawing.Color.Black;
            this.LblMemberShortName.Location = new System.Drawing.Point(128, 52);
            this.LblMemberShortName.Name = "LblMemberShortName";
            this.LblMemberShortName.Size = new System.Drawing.Size(154, 23);
            this.LblMemberShortName.TabIndex = 155;
            // 
            // LblMemberName
            // 
            this.LblMemberName.BackColor = System.Drawing.SystemColors.Window;
            this.LblMemberName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblMemberName.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMemberName.ForeColor = System.Drawing.Color.Black;
            this.LblMemberName.Location = new System.Drawing.Point(128, 26);
            this.LblMemberName.Name = "LblMemberName";
            this.LblMemberName.Size = new System.Drawing.Size(432, 23);
            this.LblMemberName.TabIndex = 154;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(6, 79);
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
            this.label16.Location = new System.Drawing.Point(6, 53);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(108, 20);
            this.label16.TabIndex = 152;
            this.label16.Text = "ShortName :";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(6, 27);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(64, 20);
            this.label15.TabIndex = 151;
            this.label15.Text = "Name :";
            // 
            // TxtMember
            // 
            this.TxtMember.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtMember.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMember.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtMember.Location = new System.Drawing.Point(161, 33);
            this.TxtMember.MaxLength = 255;
            this.TxtMember.Name = "TxtMember";
            this.TxtMember.Size = new System.Drawing.Size(364, 26);
            this.TxtMember.TabIndex = 0;
            this.TxtMember.Tag = "0";
            this.TxtMember.Enter += new System.EventHandler(this.TxtVno_Enter);
            this.TxtMember.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtMember_KeyDown);
            this.TxtMember.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtMember_KeyPress);
            this.TxtMember.Leave += new System.EventHandler(this.TxtVno_Leave);
            this.TxtMember.Validated += new System.EventHandler(this.TxtMember_Validated);
            // 
            // TxtNetAmount
            // 
            this.TxtNetAmount.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNetAmount.Font = new System.Drawing.Font("Bookman Old Style", 16F, System.Drawing.FontStyle.Bold);
            this.TxtNetAmount.Location = new System.Drawing.Point(161, 129);
            this.TxtNetAmount.MaxLength = 255;
            this.TxtNetAmount.Name = "TxtNetAmount";
            this.TxtNetAmount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtNetAmount.Size = new System.Drawing.Size(399, 33);
            this.TxtNetAmount.TabIndex = 3;
            this.TxtNetAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ChkTaxInvoice
            // 
            this.ChkTaxInvoice.AutoSize = true;
            this.ChkTaxInvoice.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.ChkTaxInvoice.Location = new System.Drawing.Point(265, 5);
            this.ChkTaxInvoice.Name = "ChkTaxInvoice";
            this.ChkTaxInvoice.Size = new System.Drawing.Size(109, 24);
            this.ChkTaxInvoice.TabIndex = 299;
            this.ChkTaxInvoice.Text = "TaxInvoice";
            this.ChkTaxInvoice.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Bookman Old Style", 16F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(6, 166);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 25);
            this.label7.TabIndex = 227;
            this.label7.Text = "Tender";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Bookman Old Style", 16F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(6, 133);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(148, 25);
            this.label6.TabIndex = 225;
            this.label6.Text = "Grand Total";
            // 
            // TxtTenderAmount
            // 
            this.TxtTenderAmount.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtTenderAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTenderAmount.Font = new System.Drawing.Font("Bookman Old Style", 16F, System.Drawing.FontStyle.Bold);
            this.TxtTenderAmount.Location = new System.Drawing.Point(161, 163);
            this.TxtTenderAmount.MaxLength = 255;
            this.TxtTenderAmount.Name = "TxtTenderAmount";
            this.TxtTenderAmount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtTenderAmount.Size = new System.Drawing.Size(399, 33);
            this.TxtTenderAmount.TabIndex = 3;
            this.TxtTenderAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtTenderAmount.TextChanged += new System.EventHandler(this.TxtTenderAmount_TextChanged);
            this.TxtTenderAmount.Enter += new System.EventHandler(this.TxtTenderAmt_Enter);
            this.TxtTenderAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtTenderAmt_KeyDown);
            this.TxtTenderAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtTenderAmt_KeyPress);
            this.TxtTenderAmount.Leave += new System.EventHandler(this.TxtTenderAmt_Leave);
            this.TxtTenderAmount.Validating += new System.ComponentModel.CancelEventHandler(this.TxtTenderAmt_Validating);
            // 
            // btnMember
            // 
            this.btnMember.CausesValidation = false;
            this.btnMember.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMember.ForeColor = System.Drawing.SystemColors.Window;
            this.btnMember.Image = global::MrBLL.Properties.Resources.search16;
            this.btnMember.Location = new System.Drawing.Point(527, 33);
            this.btnMember.Name = "btnMember";
            this.btnMember.Size = new System.Drawing.Size(32, 26);
            this.btnMember.TabIndex = 332;
            this.btnMember.TabStop = false;
            this.btnMember.UseVisualStyleBackColor = true;
            this.btnMember.Click += new System.EventHandler(this.BtnMember_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Bookman Old Style", 16F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(276, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 25);
            this.label5.TabIndex = 223;
            this.label5.Text = "%";
            // 
            // ChkPrint
            // 
            this.ChkPrint.AutoSize = true;
            this.ChkPrint.Checked = true;
            this.ChkPrint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkPrint.Location = new System.Drawing.Point(137, 6);
            this.ChkPrint.Name = "ChkPrint";
            this.ChkPrint.Size = new System.Drawing.Size(122, 23);
            this.ChkPrint.TabIndex = 3;
            this.ChkPrint.Text = "Print Invoice";
            this.ChkPrint.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Bookman Old Style", 16F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(6, 210);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 25);
            this.label9.TabIndex = 229;
            this.label9.Text = "Change";
            // 
            // TxtBillDiscountAmount
            // 
            this.TxtBillDiscountAmount.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtBillDiscountAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBillDiscountAmount.Font = new System.Drawing.Font("Bookman Old Style", 16F, System.Drawing.FontStyle.Bold);
            this.TxtBillDiscountAmount.Location = new System.Drawing.Point(324, 95);
            this.TxtBillDiscountAmount.MaxLength = 255;
            this.TxtBillDiscountAmount.Name = "TxtBillDiscountAmount";
            this.TxtBillDiscountAmount.Size = new System.Drawing.Size(236, 33);
            this.TxtBillDiscountAmount.TabIndex = 2;
            this.TxtBillDiscountAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtBillDiscountAmount.TextChanged += new System.EventHandler(this.TxtBillDiscountAmount_TextChanged);
            this.TxtBillDiscountAmount.Enter += new System.EventHandler(this.TxtBillDiscountAmount_Enter);
            this.TxtBillDiscountAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBillDiscountAmount_KeyPress);
            this.TxtBillDiscountAmount.Leave += new System.EventHandler(this.TxtBillDiscountAmount_Leave);
            this.TxtBillDiscountAmount.Validating += new System.ComponentModel.CancelEventHandler(this.TxtBillDiscountAmount_Validating);
            // 
            // TxtChangeAmount
            // 
            this.TxtChangeAmount.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtChangeAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtChangeAmount.Font = new System.Drawing.Font("Bookman Old Style", 40F, System.Drawing.FontStyle.Bold);
            this.TxtChangeAmount.ForeColor = System.Drawing.Color.Red;
            this.TxtChangeAmount.Location = new System.Drawing.Point(161, 198);
            this.TxtChangeAmount.MaxLength = 255;
            this.TxtChangeAmount.Name = "TxtChangeAmount";
            this.TxtChangeAmount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtChangeAmount.Size = new System.Drawing.Size(399, 70);
            this.TxtChangeAmount.TabIndex = 10;
            this.TxtChangeAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtBillDiscountPercentage
            // 
            this.TxtBillDiscountPercentage.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtBillDiscountPercentage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBillDiscountPercentage.Font = new System.Drawing.Font("Bookman Old Style", 16F, System.Drawing.FontStyle.Bold);
            this.TxtBillDiscountPercentage.Location = new System.Drawing.Point(161, 95);
            this.TxtBillDiscountPercentage.MaxLength = 6;
            this.TxtBillDiscountPercentage.Name = "TxtBillDiscountPercentage";
            this.TxtBillDiscountPercentage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtBillDiscountPercentage.Size = new System.Drawing.Size(108, 33);
            this.TxtBillDiscountPercentage.TabIndex = 1;
            this.TxtBillDiscountPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtBillDiscountPercentage.TextBoxType = MrDAL.Control.ControlsEx.Control.TextBoxType.Decimal;
            this.TxtBillDiscountPercentage.TextChanged += new System.EventHandler(this.TxtBillDiscountPercentage_TextChanged);
            this.TxtBillDiscountPercentage.Enter += new System.EventHandler(this.TxtBillDiscountPercentage_Enter);
            this.TxtBillDiscountPercentage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBillDiscountPercentage_KeyPress);
            this.TxtBillDiscountPercentage.Leave += new System.EventHandler(this.TxtBillDiscountPercentage_Leave);
            this.TxtBillDiscountPercentage.Validated += new System.EventHandler(this.TxtBillDiscountPercentage_Validated);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 16F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(6, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 25);
            this.label4.TabIndex = 58;
            this.label4.Text = "Discount";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 16F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(6, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 25);
            this.label2.TabIndex = 56;
            this.label2.Text = "SubTotal";
            // 
            // CustomerDetails
            // 
            this.CustomerDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CustomerDetails.Controls.Add(this.lblCreditDays);
            this.CustomerDetails.Controls.Add(this.lblPan);
            this.CustomerDetails.Controls.Add(this.label10);
            this.CustomerDetails.Controls.Add(this.label25);
            this.CustomerDetails.Controls.Add(this.lbl_Currentbal);
            this.CustomerDetails.Controls.Add(this.lblCrLimit);
            this.CustomerDetails.Controls.Add(this.label14);
            this.CustomerDetails.Controls.Add(this.lbl_CurrentBalance);
            this.CustomerDetails.Location = new System.Drawing.Point(8, 233);
            this.CustomerDetails.Name = "CustomerDetails";
            this.CustomerDetails.Size = new System.Drawing.Size(392, 150);
            this.CustomerDetails.TabIndex = 306;
            // 
            // lblCreditDays
            // 
            this.lblCreditDays.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCreditDays.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblCreditDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCreditDays.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCreditDays.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditDays.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblCreditDays.Location = new System.Drawing.Point(128, 109);
            this.lblCreditDays.Name = "lblCreditDays";
            this.lblCreditDays.Size = new System.Drawing.Size(253, 33);
            this.lblCreditDays.TabIndex = 175;
            this.lblCreditDays.Text = "0";
            this.lblCreditDays.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPan
            // 
            this.lblPan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPan.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblPan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPan.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPan.ForeColor = System.Drawing.Color.Black;
            this.lblPan.Location = new System.Drawing.Point(128, 2);
            this.lblPan.Name = "lblPan";
            this.lblPan.Size = new System.Drawing.Size(253, 33);
            this.lblPan.TabIndex = 149;
            this.lblPan.Text = "000000000";
            this.lblPan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(5, 8);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 20);
            this.label10.TabIndex = 150;
            this.label10.Text = "Pan No";
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label25.Location = new System.Drawing.Point(5, 115);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(102, 20);
            this.label25.TabIndex = 174;
            this.label25.Text = "Credit Days";
            // 
            // lbl_Currentbal
            // 
            this.lbl_Currentbal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Currentbal.AutoSize = true;
            this.lbl_Currentbal.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Currentbal.ForeColor = System.Drawing.Color.Black;
            this.lbl_Currentbal.Location = new System.Drawing.Point(5, 44);
            this.lbl_Currentbal.Name = "lbl_Currentbal";
            this.lbl_Currentbal.Size = new System.Drawing.Size(72, 20);
            this.lbl_Currentbal.TabIndex = 148;
            this.lbl_Currentbal.Text = "Balance";
            // 
            // lblCrLimit
            // 
            this.lblCrLimit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCrLimit.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblCrLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCrLimit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCrLimit.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCrLimit.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblCrLimit.Location = new System.Drawing.Point(128, 73);
            this.lblCrLimit.Name = "lblCrLimit";
            this.lblCrLimit.Size = new System.Drawing.Size(253, 33);
            this.lblCrLimit.TabIndex = 151;
            this.lblCrLimit.Text = "0.00";
            this.lblCrLimit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(5, 79);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(104, 20);
            this.label14.TabIndex = 152;
            this.label14.Text = "Credit Limit";
            // 
            // lbl_CurrentBalance
            // 
            this.lbl_CurrentBalance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_CurrentBalance.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lbl_CurrentBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_CurrentBalance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_CurrentBalance.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CurrentBalance.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_CurrentBalance.Location = new System.Drawing.Point(128, 38);
            this.lbl_CurrentBalance.Name = "lbl_CurrentBalance";
            this.lbl_CurrentBalance.Size = new System.Drawing.Size(253, 33);
            this.lbl_CurrentBalance.TabIndex = 9;
            this.lbl_CurrentBalance.Text = "0.00";
            this.lbl_CurrentBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_Remarks
            // 
            this.lbl_Remarks.AutoSize = true;
            this.lbl_Remarks.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_Remarks.Location = new System.Drawing.Point(8, 63);
            this.lbl_Remarks.Name = "lbl_Remarks";
            this.lbl_Remarks.Size = new System.Drawing.Size(79, 20);
            this.lbl_Remarks.TabIndex = 223;
            this.lbl_Remarks.Text = "Remarks";
            // 
            // label37
            // 
            this.label37.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label37.Dock = System.Windows.Forms.DockStyle.Left;
            this.label37.ForeColor = System.Drawing.Color.Black;
            this.label37.Location = new System.Drawing.Point(0, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(2, 514);
            this.label37.TabIndex = 159;
            // 
            // TxtRemarks
            // 
            this.TxtRemarks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtRemarks.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtRemarks.Font = new System.Drawing.Font("Bookman Old Style", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtRemarks.Location = new System.Drawing.Point(129, 62);
            this.TxtRemarks.MaxLength = 255;
            this.TxtRemarks.Multiline = true;
            this.TxtRemarks.Name = "TxtRemarks";
            this.TxtRemarks.Size = new System.Drawing.Size(235, 43);
            this.TxtRemarks.TabIndex = 0;
            this.TxtRemarks.Enter += new System.EventHandler(this.TxtRemarks_Enter);
            this.TxtRemarks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtRemarks_KeyDown);
            this.TxtRemarks.Leave += new System.EventHandler(this.TxtRemarks_Leave);
            this.TxtRemarks.Validated += new System.EventHandler(this.TxtRemarks_Validated);
            // 
            // LblNumberInWords
            // 
            this.LblNumberInWords.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNumberInWords.Location = new System.Drawing.Point(85, 162);
            this.LblNumberInWords.Name = "LblNumberInWords";
            this.LblNumberInWords.Size = new System.Drawing.Size(450, 73);
            this.LblNumberInWords.TabIndex = 225;
            this.LblNumberInWords.Text = "Only.";
            // 
            // CmbPaymentType
            // 
            this.CmbPaymentType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CmbPaymentType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CmbPaymentType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CmbPaymentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbPaymentType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbPaymentType.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.CmbPaymentType.FormattingEnabled = true;
            this.CmbPaymentType.Location = new System.Drawing.Point(129, 5);
            this.CmbPaymentType.Name = "CmbPaymentType";
            this.CmbPaymentType.Size = new System.Drawing.Size(135, 28);
            this.CmbPaymentType.TabIndex = 1;
            this.CmbPaymentType.SelectedIndexChanged += new System.EventHandler(this.CmbPaymentType_SelectedIndexChanged);
            this.CmbPaymentType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbPaymentType_KeyDown);
            this.CmbPaymentType.Validating += new System.ComponentModel.CancelEventHandler(this.CmbPaymentType_Validating);
            // 
            // label30
            // 
            this.label30.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label30.Location = new System.Drawing.Point(8, 9);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(120, 20);
            this.label30.TabIndex = 200;
            this.label30.Text = "Payment Type";
            // 
            // BtnHold
            // 
            this.BtnHold.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.True;
            this.BtnHold.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnHold.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnHold.Appearance.Options.UseFont = true;
            this.BtnHold.Appearance.Options.UseForeColor = true;
            this.BtnHold.AutoWidthInLayoutControl = true;
            this.BtnHold.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.BtnHold.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.BtnHold.Location = new System.Drawing.Point(11, 108);
            this.BtnHold.Name = "BtnHold";
            this.BtnHold.Size = new System.Drawing.Size(160, 50);
            this.BtnHold.TabIndex = 6;
            this.BtnHold.Text = "&HOLD (F9)";
            this.BtnHold.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Application;
            this.BtnHold.Click += new System.EventHandler(this.BtnHold_Click);
            // 
            // lbl_CBLedger
            // 
            this.lbl_CBLedger.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_CBLedger.AutoSize = true;
            this.lbl_CBLedger.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_CBLedger.Location = new System.Drawing.Point(8, 38);
            this.lbl_CBLedger.Name = "lbl_CBLedger";
            this.lbl_CBLedger.Size = new System.Drawing.Size(86, 20);
            this.lbl_CBLedger.TabIndex = 296;
            this.lbl_CBLedger.Text = "Customer";
            // 
            // btnCustomer
            // 
            this.btnCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCustomer.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.btnCustomer.Image = global::MrBLL.Properties.Resources.search16;
            this.btnCustomer.Location = new System.Drawing.Point(332, 35);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Size = new System.Drawing.Size(32, 26);
            this.btnCustomer.TabIndex = 297;
            this.btnCustomer.TabStop = false;
            this.btnCustomer.UseVisualStyleBackColor = true;
            this.btnCustomer.Click += new System.EventHandler(this.BtnCustomer_Click);
            // 
            // TxtCustomer
            // 
            this.TxtCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCustomer.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.TxtCustomer.Location = new System.Drawing.Point(129, 35);
            this.TxtCustomer.MaxLength = 255;
            this.TxtCustomer.Name = "TxtCustomer";
            this.TxtCustomer.Size = new System.Drawing.Size(204, 26);
            this.TxtCustomer.TabIndex = 2;
            this.TxtCustomer.TextChanged += new System.EventHandler(this.TxtCustomer_TextChanged);
            this.TxtCustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCustomer_KeyDown);
            this.TxtCustomer.Validating += new System.ComponentModel.CancelEventHandler(this.TxtCustomer_Validating);
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label111.Location = new System.Drawing.Point(8, 162);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(79, 20);
            this.label111.TabIndex = 224;
            this.label111.Text = "In Words";
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Appearance.Options.UseForeColor = true;
            this.BtnSave.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(248, 108);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(153, 50);
            this.BtnSave.TabIndex = 5;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // PnlInvoiceDetails
            // 
            this.PnlInvoiceDetails.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PnlInvoiceDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlInvoiceDetails.Controls.Add(this.label36);
            this.PnlInvoiceDetails.Controls.Add(this.lblTermAmount);
            this.PnlInvoiceDetails.Controls.Add(this.LblDisplayReturnAmount);
            this.PnlInvoiceDetails.Controls.Add(this.lblTax);
            this.PnlInvoiceDetails.Controls.Add(this.label34);
            this.PnlInvoiceDetails.Controls.Add(this.lblNonTaxable);
            this.PnlInvoiceDetails.Controls.Add(this.LblDisplayReceivedAmount);
            this.PnlInvoiceDetails.Controls.Add(this.label22);
            this.PnlInvoiceDetails.Controls.Add(this.label28);
            this.PnlInvoiceDetails.Controls.Add(this.lblTaxable);
            this.PnlInvoiceDetails.Controls.Add(this.label27);
            this.PnlInvoiceDetails.Controls.Add(this.label24);
            this.PnlInvoiceDetails.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PnlInvoiceDetails.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlInvoiceDetails.Location = new System.Drawing.Point(0, 516);
            this.PnlInvoiceDetails.Name = "PnlInvoiceDetails";
            this.PnlInvoiceDetails.Size = new System.Drawing.Size(1250, 45);
            this.PnlInvoiceDetails.TabIndex = 305;
            // 
            // label36
            // 
            this.label36.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.ForeColor = System.Drawing.Color.Black;
            this.label36.Location = new System.Drawing.Point(765, 12);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(156, 20);
            this.label36.TabIndex = 152;
            this.label36.Text = "REFUND AMOUNT";
            // 
            // lblTermAmount
            // 
            this.lblTermAmount.AutoSize = true;
            this.lblTermAmount.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblTermAmount.Location = new System.Drawing.Point(207, 6);
            this.lblTermAmount.Name = "lblTermAmount";
            this.lblTermAmount.Size = new System.Drawing.Size(29, 13);
            this.lblTermAmount.TabIndex = 251;
            this.lblTermAmount.Text = "0.00";
            this.lblTermAmount.Visible = false;
            // 
            // LblDisplayReturnAmount
            // 
            this.LblDisplayReturnAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblDisplayReturnAmount.BackColor = System.Drawing.SystemColors.HighlightText;
            this.LblDisplayReturnAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblDisplayReturnAmount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblDisplayReturnAmount.Font = new System.Drawing.Font("Bookman Old Style", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDisplayReturnAmount.Location = new System.Drawing.Point(927, 3);
            this.LblDisplayReturnAmount.Name = "LblDisplayReturnAmount";
            this.LblDisplayReturnAmount.Size = new System.Drawing.Size(202, 38);
            this.LblDisplayReturnAmount.TabIndex = 151;
            this.LblDisplayReturnAmount.Text = "0.00";
            this.LblDisplayReturnAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTax
            // 
            this.lblTax.AutoSize = true;
            this.lblTax.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblTax.Location = new System.Drawing.Point(141, 6);
            this.lblTax.Name = "lblTax";
            this.lblTax.Size = new System.Drawing.Size(29, 13);
            this.lblTax.TabIndex = 251;
            this.lblTax.Text = "0.00";
            this.lblTax.Visible = false;
            // 
            // label34
            // 
            this.label34.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.ForeColor = System.Drawing.Color.Black;
            this.label34.Location = new System.Drawing.Point(374, 12);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(172, 20);
            this.label34.TabIndex = 150;
            this.label34.Text = "RECEIVED AMOUNT";
            // 
            // lblNonTaxable
            // 
            this.lblNonTaxable.AutoSize = true;
            this.lblNonTaxable.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblNonTaxable.Location = new System.Drawing.Point(88, 6);
            this.lblNonTaxable.Name = "lblNonTaxable";
            this.lblNonTaxable.Size = new System.Drawing.Size(29, 13);
            this.lblNonTaxable.TabIndex = 251;
            this.lblNonTaxable.Text = "0.00";
            this.lblNonTaxable.Visible = false;
            // 
            // LblDisplayReceivedAmount
            // 
            this.LblDisplayReceivedAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblDisplayReceivedAmount.BackColor = System.Drawing.SystemColors.HighlightText;
            this.LblDisplayReceivedAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblDisplayReceivedAmount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblDisplayReceivedAmount.Font = new System.Drawing.Font("Bookman Old Style", 16F);
            this.LblDisplayReceivedAmount.Location = new System.Drawing.Point(552, 3);
            this.LblDisplayReceivedAmount.Name = "LblDisplayReceivedAmount";
            this.LblDisplayReceivedAmount.Size = new System.Drawing.Size(202, 38);
            this.LblDisplayReceivedAmount.TabIndex = 149;
            this.LblDisplayReceivedAmount.Text = "0.00";
            this.LblDisplayReceivedAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label22.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label22.Location = new System.Drawing.Point(207, 23);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(70, 13);
            this.label22.TabIndex = 251;
            this.label22.Text = "Term amount";
            this.label22.Visible = false;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label28.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label28.Location = new System.Drawing.Point(16, 23);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(45, 13);
            this.label28.TabIndex = 251;
            this.label28.Text = "Taxable";
            this.label28.Visible = false;
            // 
            // lblTaxable
            // 
            this.lblTaxable.AutoSize = true;
            this.lblTaxable.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblTaxable.Location = new System.Drawing.Point(16, 6);
            this.lblTaxable.Name = "lblTaxable";
            this.lblTaxable.Size = new System.Drawing.Size(29, 13);
            this.lblTaxable.TabIndex = 251;
            this.lblTaxable.Text = "0.00";
            this.lblTaxable.Visible = false;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label27.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label27.Location = new System.Drawing.Point(72, 23);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(64, 13);
            this.label27.TabIndex = 251;
            this.label27.Text = "NonTaxable";
            this.label27.Visible = false;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label24.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label24.Location = new System.Drawing.Point(141, 23);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(25, 13);
            this.label24.TabIndex = 251;
            this.label24.Text = "Tax";
            this.label24.Visible = false;
            // 
            // RGrid
            // 
            this.RGrid.AllowUserToAddRows = false;
            this.RGrid.AllowUserToResizeColumns = false;
            this.RGrid.AllowUserToResizeRows = false;
            this.RGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.RGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.RGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.RGrid.ColumnHeadersHeight = 27;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RGrid.DefaultCellStyle = dataGridViewCellStyle1;
            this.RGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.RGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.RGrid.Location = new System.Drawing.Point(0, 0);
            this.RGrid.Name = "RGrid";
            this.RGrid.ReadOnly = true;
            this.RGrid.RowHeadersVisible = false;
            this.RGrid.RowHeadersWidth = 51;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RGrid.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.RGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RGrid.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RGrid.Size = new System.Drawing.Size(1250, 561);
            this.RGrid.TabIndex = 21;
            this.RGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_CellEnter);
            this.RGrid.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.RGrid_RowHeaderMouseDoubleClick);
            this.RGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_RowEnter);
            this.RGrid.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.RGrid_UserDeletedRow);
            this.RGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RGrid_KeyDown);
            // 
            // PnlProductDetails
            // 
            this.PnlProductDetails.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PnlProductDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlProductDetails.Controls.Add(this.LblStockQty);
            this.PnlProductDetails.Controls.Add(this.label39);
            this.PnlProductDetails.Controls.Add(this.LblProduct);
            this.PnlProductDetails.Controls.Add(this.LblBarcode);
            this.PnlProductDetails.Controls.Add(this.LblSalesRate);
            this.PnlProductDetails.Controls.Add(this.LblUnit);
            this.PnlProductDetails.Controls.Add(this.label33);
            this.PnlProductDetails.Controls.Add(this.label23);
            this.PnlProductDetails.Controls.Add(this.label11);
            this.PnlProductDetails.Controls.Add(this.label3);
            this.PnlProductDetails.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PnlProductDetails.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlProductDetails.Location = new System.Drawing.Point(0, 561);
            this.PnlProductDetails.Name = "PnlProductDetails";
            this.PnlProductDetails.Size = new System.Drawing.Size(1250, 38);
            this.PnlProductDetails.TabIndex = 304;
            // 
            // LblStockQty
            // 
            this.LblStockQty.BackColor = System.Drawing.Color.White;
            this.LblStockQty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblStockQty.Location = new System.Drawing.Point(1053, 6);
            this.LblStockQty.Name = "LblStockQty";
            this.LblStockQty.Size = new System.Drawing.Size(99, 25);
            this.LblStockQty.TabIndex = 9;
            this.LblStockQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(1011, 9);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(40, 19);
            this.label39.TabIndex = 8;
            this.label39.Text = "Qty:";
            // 
            // LblProduct
            // 
            this.LblProduct.BackColor = System.Drawing.Color.White;
            this.LblProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblProduct.Location = new System.Drawing.Point(86, 6);
            this.LblProduct.Name = "LblProduct";
            this.LblProduct.Size = new System.Drawing.Size(315, 25);
            this.LblProduct.TabIndex = 7;
            // 
            // LblBarcode
            // 
            this.LblBarcode.BackColor = System.Drawing.Color.White;
            this.LblBarcode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblBarcode.Location = new System.Drawing.Point(485, 6);
            this.LblBarcode.Name = "LblBarcode";
            this.LblBarcode.Size = new System.Drawing.Size(166, 25);
            this.LblBarcode.TabIndex = 6;
            // 
            // LblSalesRate
            // 
            this.LblSalesRate.BackColor = System.Drawing.Color.White;
            this.LblSalesRate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblSalesRate.Location = new System.Drawing.Point(891, 6);
            this.LblSalesRate.Name = "LblSalesRate";
            this.LblSalesRate.Size = new System.Drawing.Size(116, 25);
            this.LblSalesRate.TabIndex = 5;
            this.LblSalesRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblUnit
            // 
            this.LblUnit.BackColor = System.Drawing.Color.White;
            this.LblUnit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblUnit.Location = new System.Drawing.Point(710, 6);
            this.LblUnit.Name = "LblUnit";
            this.LblUnit.Size = new System.Drawing.Size(119, 25);
            this.LblUnit.TabIndex = 4;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(833, 9);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(54, 19);
            this.label33.TabIndex = 3;
            this.label33.Text = "Rate :";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(655, 9);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(51, 19);
            this.label23.TabIndex = 2;
            this.label23.Text = "UOM:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(406, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 19);
            this.label11.TabIndex = 1;
            this.label11.Text = "Barcode:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "Product :";
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.pnlTop.Controls.Add(this.TxtQty);
            this.pnlTop.Controls.Add(this.TxtRefVno);
            this.pnlTop.Controls.Add(this.menuStrip1);
            this.pnlTop.Controls.Add(this.TxtAltQty);
            this.pnlTop.Controls.Add(this.label35);
            this.pnlTop.Controls.Add(this.TxtAltUnit);
            this.pnlTop.Controls.Add(this.BtnNew);
            this.pnlTop.Controls.Add(this.ListOfProduct);
            this.pnlTop.Controls.Add(this.lbl_VoucherNo);
            this.pnlTop.Controls.Add(this.label20);
            this.pnlTop.Controls.Add(this.BtnRefVno);
            this.pnlTop.Controls.Add(this.BtnVno);
            this.pnlTop.Controls.Add(this.LblInvoiceNo);
            this.pnlTop.Controls.Add(this.label19);
            this.pnlTop.Controls.Add(this.BtnCounter);
            this.pnlTop.Controls.Add(this.TxtUnit);
            this.pnlTop.Controls.Add(this.TxtCounter);
            this.pnlTop.Controls.Add(this.TxtBarcode);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Controls.Add(this.TxtVno);
            this.pnlTop.Controls.Add(this.clsSeparator1);
            this.pnlTop.Controls.Add(this.label12);
            this.pnlTop.Controls.Add(this.MskDate);
            this.pnlTop.Controls.Add(this.label13);
            this.pnlTop.Controls.Add(this.MskMiti);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1250, 101);
            this.pnlTop.TabIndex = 0;
            // 
            // TxtQty
            // 
            this.TxtQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtQty.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtQty.Location = new System.Drawing.Point(1075, 71);
            this.TxtQty.MaxLength = 255;
            this.TxtQty.Name = "TxtQty";
            this.TxtQty.Size = new System.Drawing.Size(86, 26);
            this.TxtQty.TabIndex = 7;
            this.TxtQty.Text = "1.00";
            this.TxtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtQty.TextChanged += new System.EventHandler(this.TxtQty_TextChanged);
            this.TxtQty.Enter += new System.EventHandler(this.TxtQty_Enter);
            this.TxtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtQty_KeyDown);
            this.TxtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtQty_KeyPress);
            this.TxtQty.Leave += new System.EventHandler(this.TxtQty_Leave);
            this.TxtQty.Validating += new System.ComponentModel.CancelEventHandler(this.TxtQty_Validating);
            // 
            // TxtRefVno
            // 
            this.TxtRefVno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtRefVno.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtRefVno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtRefVno.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtRefVno.Location = new System.Drawing.Point(1025, 42);
            this.TxtRefVno.MaxLength = 255;
            this.TxtRefVno.Name = "TxtRefVno";
            this.TxtRefVno.Size = new System.Drawing.Size(184, 26);
            this.TxtRefVno.TabIndex = 4;
            this.TxtRefVno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtRefVno_KeyDown);
            this.TxtRefVno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtRefVno_KeyPress);
            this.TxtRefVno.Leave += new System.EventHandler(this.TxtRefVno_Leave);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mENUSETUPToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(1111, 6);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(135, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mENUSETUPToolStripMenuItem
            // 
            this.mENUSETUPToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuReturnInvoice,
            this.MnuReverseInvoice,
            this.MnuExchangeInvoice,
            this.MnuRePrintInvoice,
            this.MnuCashVoucher,
            this.MnuLockScreen,
            this.MnuDayClosing,
            this.MnuTodaysSalesReports,
            this.MnuHoldInvoiceList,
            this.MnuExit});
            this.mENUSETUPToolStripMenuItem.Name = "mENUSETUPToolStripMenuItem";
            this.mENUSETUPToolStripMenuItem.Size = new System.Drawing.Size(129, 24);
            this.mENUSETUPToolStripMenuItem.Text = "MENU SETUP";
            // 
            // MnuReturnInvoice
            // 
            this.MnuReturnInvoice.Name = "MnuReturnInvoice";
            this.MnuReturnInvoice.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.MnuReturnInvoice.Size = new System.Drawing.Size(324, 24);
            this.MnuReturnInvoice.Text = "RETURN INVOICE";
            this.MnuReturnInvoice.Click += new System.EventHandler(this.BtnReturn_Click);
            // 
            // MnuReverseInvoice
            // 
            this.MnuReverseInvoice.Name = "MnuReverseInvoice";
            this.MnuReverseInvoice.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.MnuReverseInvoice.Size = new System.Drawing.Size(324, 24);
            this.MnuReverseInvoice.Text = "REVERSE INVOICE";
            this.MnuReverseInvoice.Click += new System.EventHandler(this.BtnReverse_Click);
            // 
            // MnuExchangeInvoice
            // 
            this.MnuExchangeInvoice.Name = "MnuExchangeInvoice";
            this.MnuExchangeInvoice.Size = new System.Drawing.Size(324, 24);
            this.MnuExchangeInvoice.Text = "EXCHANGE INVOICE";
            // 
            // MnuRePrintInvoice
            // 
            this.MnuRePrintInvoice.Name = "MnuRePrintInvoice";
            this.MnuRePrintInvoice.Size = new System.Drawing.Size(324, 24);
            this.MnuRePrintInvoice.Text = "RE-PRINT INVOICE";
            this.MnuRePrintInvoice.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // MnuCashVoucher
            // 
            this.MnuCashVoucher.Name = "MnuCashVoucher";
            this.MnuCashVoucher.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.MnuCashVoucher.Size = new System.Drawing.Size(324, 24);
            this.MnuCashVoucher.Text = "CASH VOUCHER";
            // 
            // MnuLockScreen
            // 
            this.MnuLockScreen.Name = "MnuLockScreen";
            this.MnuLockScreen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.MnuLockScreen.Size = new System.Drawing.Size(324, 24);
            this.MnuLockScreen.Text = "LOCK SCREEN";
            this.MnuLockScreen.Click += new System.EventHandler(this.BtnLock_Click);
            // 
            // MnuDayClosing
            // 
            this.MnuDayClosing.Name = "MnuDayClosing";
            this.MnuDayClosing.Size = new System.Drawing.Size(324, 24);
            this.MnuDayClosing.Text = "DAY CLOSING";
            this.MnuDayClosing.Click += new System.EventHandler(this.BtnDayClosing_Click);
            // 
            // MnuTodaysSalesReports
            // 
            this.MnuTodaysSalesReports.Name = "MnuTodaysSalesReports";
            this.MnuTodaysSalesReports.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.MnuTodaysSalesReports.Size = new System.Drawing.Size(324, 24);
            this.MnuTodaysSalesReports.Text = "TODAY SALES REPORT";
            this.MnuTodaysSalesReports.Click += new System.EventHandler(this.BtnTodaySales_Click);
            // 
            // MnuHoldInvoiceList
            // 
            this.MnuHoldInvoiceList.Name = "MnuHoldInvoiceList";
            this.MnuHoldInvoiceList.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.MnuHoldInvoiceList.Size = new System.Drawing.Size(324, 24);
            this.MnuHoldInvoiceList.Text = "PENDING LIST";
            this.MnuHoldInvoiceList.Click += new System.EventHandler(this.MnuHoldInvoiceList_Click);
            // 
            // MnuExit
            // 
            this.MnuExit.Name = "MnuExit";
            this.MnuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.MnuExit.Size = new System.Drawing.Size(324, 24);
            this.MnuExit.Text = "EXIT FORM";
            this.MnuExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // TxtAltQty
            // 
            this.TxtAltQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtAltQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAltQty.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAltQty.Location = new System.Drawing.Point(871, 71);
            this.TxtAltQty.MaxLength = 255;
            this.TxtAltQty.Name = "TxtAltQty";
            this.TxtAltQty.Size = new System.Drawing.Size(86, 26);
            this.TxtAltQty.TabIndex = 6;
            this.TxtAltQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtAltQty.TextChanged += new System.EventHandler(this.TxtAltQty_TextChanged);
            // 
            // label35
            // 
            this.label35.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(806, 74);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(64, 20);
            this.label35.TabIndex = 353;
            this.label35.Text = "Alt Qty";
            // 
            // TxtAltUnit
            // 
            this.TxtAltUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtAltUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAltUnit.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAltUnit.Location = new System.Drawing.Point(958, 71);
            this.TxtAltUnit.MaxLength = 255;
            this.TxtAltUnit.Name = "TxtAltUnit";
            this.TxtAltUnit.ReadOnly = true;
            this.TxtAltUnit.Size = new System.Drawing.Size(80, 26);
            this.TxtAltUnit.TabIndex = 354;
            // 
            // BtnNew
            // 
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNew.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.Appearance.Options.UseForeColor = true;
            this.BtnNew.Cursor = System.Windows.Forms.Cursors.Default;
            this.BtnNew.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnNew.Location = new System.Drawing.Point(7, 3);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(86, 34);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // ListOfProduct
            // 
            this.ListOfProduct.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListOfProduct.BackColor = System.Drawing.Color.White;
            this.ListOfProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ListOfProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ListOfProduct.Location = new System.Drawing.Point(278, 71);
            this.ListOfProduct.MinimumSize = new System.Drawing.Size(300, 26);
            this.ListOfProduct.Name = "ListOfProduct";
            this.ListOfProduct.Size = new System.Drawing.Size(522, 26);
            this.ListOfProduct.TabIndex = 347;
            // 
            // lbl_VoucherNo
            // 
            this.lbl_VoucherNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_VoucherNo.AutoSize = true;
            this.lbl_VoucherNo.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_VoucherNo.Location = new System.Drawing.Point(8, 45);
            this.lbl_VoucherNo.Name = "lbl_VoucherNo";
            this.lbl_VoucherNo.Size = new System.Drawing.Size(89, 20);
            this.lbl_VoucherNo.TabIndex = 54;
            this.lbl_VoucherNo.Text = "Invoice No";
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(8, 74);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(77, 20);
            this.label20.TabIndex = 209;
            this.label20.Text = "BarCode";
            // 
            // BtnRefVno
            // 
            this.BtnRefVno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnRefVno.CausesValidation = false;
            this.BtnRefVno.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRefVno.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnRefVno.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnRefVno.Location = new System.Drawing.Point(1209, 41);
            this.BtnRefVno.Name = "BtnRefVno";
            this.BtnRefVno.Size = new System.Drawing.Size(32, 28);
            this.BtnRefVno.TabIndex = 344;
            this.BtnRefVno.TabStop = false;
            this.BtnRefVno.UseVisualStyleBackColor = true;
            this.BtnRefVno.Click += new System.EventHandler(this.BtnVno_Click);
            // 
            // BtnVno
            // 
            this.BtnVno.CausesValidation = false;
            this.BtnVno.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnVno.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnVno.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnVno.Location = new System.Drawing.Point(277, 41);
            this.BtnVno.Name = "BtnVno";
            this.BtnVno.Size = new System.Drawing.Size(32, 28);
            this.BtnVno.TabIndex = 51;
            this.BtnVno.TabStop = false;
            this.BtnVno.UseVisualStyleBackColor = true;
            this.BtnVno.Click += new System.EventHandler(this.BtnVno_Click);
            // 
            // LblInvoiceNo
            // 
            this.LblInvoiceNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblInvoiceNo.AutoSize = true;
            this.LblInvoiceNo.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblInvoiceNo.Location = new System.Drawing.Point(933, 45);
            this.LblInvoiceNo.Name = "LblInvoiceNo";
            this.LblInvoiceNo.Size = new System.Drawing.Size(89, 20);
            this.LblInvoiceNo.TabIndex = 345;
            this.LblInvoiceNo.Text = "Invoice No";
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(1040, 74);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(37, 20);
            this.label19.TabIndex = 208;
            this.label19.Text = "Qty";
            // 
            // BtnCounter
            // 
            this.BtnCounter.CausesValidation = false;
            this.BtnCounter.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCounter.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnCounter.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnCounter.Location = new System.Drawing.Point(888, 41);
            this.BtnCounter.Name = "BtnCounter";
            this.BtnCounter.Size = new System.Drawing.Size(32, 28);
            this.BtnCounter.TabIndex = 342;
            this.BtnCounter.TabStop = false;
            this.BtnCounter.UseVisualStyleBackColor = true;
            this.BtnCounter.Click += new System.EventHandler(this.BtnCounter_Click);
            // 
            // TxtUnit
            // 
            this.TxtUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtUnit.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUnit.Location = new System.Drawing.Point(1162, 71);
            this.TxtUnit.MaxLength = 255;
            this.TxtUnit.Name = "TxtUnit";
            this.TxtUnit.ReadOnly = true;
            this.TxtUnit.Size = new System.Drawing.Size(80, 26);
            this.TxtUnit.TabIndex = 221;
            // 
            // TxtCounter
            // 
            this.TxtCounter.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtCounter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCounter.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCounter.Location = new System.Drawing.Point(725, 42);
            this.TxtCounter.MaxLength = 255;
            this.TxtCounter.Name = "TxtCounter";
            this.TxtCounter.Size = new System.Drawing.Size(163, 26);
            this.TxtCounter.TabIndex = 3;
            this.TxtCounter.Enter += new System.EventHandler(this.GlobalControl_Enter);
            this.TxtCounter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalControl_KeyPress);
            this.TxtCounter.Leave += new System.EventHandler(this.TxtCounter_Leave);
            // 
            // TxtBarcode
            // 
            this.TxtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBarcode.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBarcode.Location = new System.Drawing.Point(97, 71);
            this.TxtBarcode.MaxLength = 255;
            this.TxtBarcode.Name = "TxtBarcode";
            this.TxtBarcode.Size = new System.Drawing.Size(180, 26);
            this.TxtBarcode.TabIndex = 5;
            this.TxtBarcode.Enter += new System.EventHandler(this.TxtBarcode_Enter);
            this.TxtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBarcode_KeyDown);
            this.TxtBarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBarcode_KeyPress);
            this.TxtBarcode.Validating += new System.ComponentModel.CancelEventHandler(this.TxtBarcode_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(483, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 20);
            this.label1.TabIndex = 56;
            this.label1.Text = "Date";
            // 
            // TxtVno
            // 
            this.TxtVno.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtVno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtVno.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtVno.Location = new System.Drawing.Point(97, 42);
            this.TxtVno.MaxLength = 255;
            this.TxtVno.Name = "TxtVno";
            this.TxtVno.Size = new System.Drawing.Size(180, 26);
            this.TxtVno.TabIndex = 0;
            this.TxtVno.Enter += new System.EventHandler(this.GlobalControl_Enter);
            this.TxtVno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtVNo_KeyDown);
            this.TxtVno.Leave += new System.EventHandler(this.GlobalControl_Leave);
            this.TxtVno.Validating += new System.ComponentModel.CancelEventHandler(this.TxtVno_Validating);
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(3, 39);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(1247, 2);
            this.clsSeparator1.TabIndex = 305;
            this.clsSeparator1.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(653, 45);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 20);
            this.label12.TabIndex = 202;
            this.label12.Text = "Counter";
            // 
            // MskDate
            // 
            this.MskDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskDate.Location = new System.Drawing.Point(529, 42);
            this.MskDate.Mask = "00/00/0000";
            this.MskDate.Name = "MskDate";
            this.MskDate.Size = new System.Drawing.Size(123, 26);
            this.MskDate.TabIndex = 2;
            this.MskDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalControl_KeyPress);
            this.MskDate.Validating += new System.ComponentModel.CancelEventHandler(this.MskDate_Validating);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(316, 45);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(40, 20);
            this.label13.TabIndex = 203;
            this.label13.Text = "Miti";
            // 
            // MskMiti
            // 
            this.MskMiti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskMiti.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskMiti.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskMiti.Location = new System.Drawing.Point(356, 42);
            this.MskMiti.Mask = "00/00/0000";
            this.MskMiti.Name = "MskMiti";
            this.MskMiti.Size = new System.Drawing.Size(123, 26);
            this.MskMiti.TabIndex = 1;
            this.MskMiti.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalControl_KeyPress);
            this.MskMiti.Validating += new System.ComponentModel.CancelEventHandler(this.MskMiti_Validating);
            // 
            // FrmPSalesInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1250, 732);
            this.Controls.Add(this.PanelHeader);
            this.Controls.Add(this.tlpBillSummary);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmPSalesInvoice";
            this.ShowIcon = false;
            this.Text = "POINT OF SALES";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmPSalesInvoice_Load);
            this.Shown += new System.EventHandler(this.FrmPSalesInvoice_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPSalesInvoice_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmPSalesInvoice_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.bsProduct)).EndInit();
            this.tlpBillSummary.ResumeLayout(false);
            this.tlpBillSummary.PerformLayout();
            this.PanelHeader.ResumeLayout(false);
            this.DataGridGroup.ResumeLayout(false);
            this.PDetails.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.GrpCustomer.ResumeLayout(false);
            this.GrpCustomer.PerformLayout();
            this.MemberGroup.ResumeLayout(false);
            this.MemberGroup.PerformLayout();
            this.CustomerDetails.ResumeLayout(false);
            this.CustomerDetails.PerformLayout();
            this.PnlInvoiceDetails.ResumeLayout(false);
            this.PnlInvoiceDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).EndInit();
            this.PnlProductDetails.ResumeLayout(false);
            this.PnlProductDetails.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.DataGridView RGrid;
        private System.Windows.Forms.Button btnCustomer;
        private System.Windows.Forms.Label lbl_CBLedger;
        private System.Windows.Forms.Button BtnVno;
        private System.Windows.Forms.Label lbl_VoucherNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel GrpCustomer;
        private System.Windows.Forms.ComboBox CmbPaymentType;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label LblNumberInWords;
        private System.Windows.Forms.Label label111;
        private System.Windows.Forms.Label lbl_Remarks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label12;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private DevExpress.XtraEditors.SimpleButton BtnHold;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel CustomerDetails;
        private System.Windows.Forms.Label lblCreditDays;
        private System.Windows.Forms.Label lblPan;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label lbl_Currentbal;
        private System.Windows.Forms.Label lblCrLimit;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lbl_CurrentBalance;
        private ClsSeparator clsSeparator1;
        private System.Windows.Forms.CheckBox ChkPrint;
        private System.Windows.Forms.CheckBox ChkTaxInvoice;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnMember;
        private System.Windows.Forms.GroupBox MemberGroup;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Panel DataGridGroup;
        private System.Windows.Forms.BindingSource bsProduct;
        private System.Windows.Forms.Button BtnCounter;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label LblItemsDiscountSum;
        private System.Windows.Forms.Label lblTermAmount;
        private System.Windows.Forms.Label lblTax;
        private System.Windows.Forms.Label lblNonTaxable;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label lblTaxable;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label LblItemsNetAmount;
        private System.Windows.Forms.Label LblItemsTotalQty;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label LblItemsTotal;
        private System.Windows.Forms.Button BtnRefVno;
        private System.Windows.Forms.Label LblInvoiceNo;
        private MrTextBox TxtUnit;
        private MrTextBox TxtBarcode;
        private MrTextBox TxtQty;
        private MrTextBox TxtCustomer;
        private MrTextBox TxtVno;
        private MrTextBox TxtRemarks;
        private MrTextBox TxtBasicAmount;
        private MrTextBox TxtChangeAmount;
        private MrTextBox TxtTenderAmount;
        private MrTextBox TxtNetAmount;
        private MrTextBox TxtBillDiscountAmount;
        private MrTextBox TxtBillDiscountPercentage;
        private MrTextBox TxtMember;
        private MrTextBox TxtCounter;
        private MrTextBox TxtRefVno;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label LblUnit;
        private System.Windows.Forms.Label LblSalesRate;
        private System.Windows.Forms.Label LblProduct;
        private System.Windows.Forms.Label LblBarcode;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label LblDisplayReceivedAmount;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label LblDisplayReturnAmount;
        private System.Windows.Forms.Label ListOfProduct;
        private MrMaskedTextBox MskMiti;
        private MrMaskedTextBox MskDate;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mENUSETUPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MnuReturnInvoice;
        private System.Windows.Forms.ToolStripMenuItem MnuReverseInvoice;
        private System.Windows.Forms.ToolStripMenuItem MnuExchangeInvoice;
        private System.Windows.Forms.ToolStripMenuItem MnuCashVoucher;
        private System.Windows.Forms.ToolStripMenuItem MnuLockScreen;
        private System.Windows.Forms.ToolStripMenuItem MnuDayClosing;
        private System.Windows.Forms.ToolStripMenuItem MnuTodaysSalesReports;
        private System.Windows.Forms.ToolStripMenuItem MnuHoldInvoiceList;
        private System.Windows.Forms.ToolStripMenuItem MnuExit;
        private System.Windows.Forms.ToolStripMenuItem MnuRePrintInvoice;
        private MrPanel PDetails;
        private MrPanel PanelHeader;
        private MrPanel PnlProductDetails;
        private MrPanel PnlInvoiceDetails;
        private MrPanel pnlTop;
        private MrTextBox TxtAltQty;
        private System.Windows.Forms.Label label35;
        private MrTextBox TxtAltUnit;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TableLayoutPanel tlpBillSummary;
        private System.Windows.Forms.Label label37;
        private Label LblStockQty;
        private Label label39;
        private Label LblTag;
        private Label LblMemberAmount;
        private Label LblMemberType;
        private Label LblMemberShortName;
        private Label LblMemberName;
    }
}