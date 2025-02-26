using DevExpress.XtraEditors;
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.Restro.Entry
{
    partial class FrmRSalesInvoice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRSalesInvoice));
            this.PanelHeader = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.DataGridGroup = new System.Windows.Forms.GroupBox();
            this.PnlInvoiceDetails = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.label36 = new System.Windows.Forms.Label();
            this.LblDisplayReturnAmount = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.LblDisplayReceivedAmount = new System.Windows.Forms.Label();
            this.PDetails = new MrDAL.Control.ControlsEx.Control.MrPanel();
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
            this.ChkPrint = new System.Windows.Forms.CheckBox();
            this.InvoiceGroup = new System.Windows.Forms.GroupBox();
            this.TxtTaxableAmount = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.TxtVatRate = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtVatAmount = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.TxtServiceChargeRate = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtServiceCharge = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.TxtVoucherAmount = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.LblNumberInWords = new System.Windows.Forms.Label();
            this.label111 = new System.Windows.Forms.Label();
            this.TxtChangeAmount = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.TxtTenderAmount = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtNetAmount = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtBillDiscountAmount = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtBillDiscountPercentage = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtBasicAmount = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CustomerGroup = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TxtMember = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.btnMember = new System.Windows.Forms.Button();
            this.CmbPaymentType = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.lbl_CBLedger = new System.Windows.Forms.Label();
            this.btnCustomer = new System.Windows.Forms.Button();
            this.TxtCustomer = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.CustomerDetails = new System.Windows.Forms.GroupBox();
            this.lblCreditDays = new System.Windows.Forms.Label();
            this.lblPan = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.lbl_Currentbal = new System.Windows.Forms.Label();
            this.lblCrLimit = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lbl_CurrentBalance = new System.Windows.Forms.Label();
            this.lbl_Remarks = new System.Windows.Forms.Label();
            this.ChkTaxInvoice = new System.Windows.Forms.CheckBox();
            this.TxtRemarks = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.RGrid = new System.Windows.Forms.DataGridView();
            this.PnlProductDetails = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.LblProduct = new System.Windows.Forms.Label();
            this.LblBarcode = new System.Windows.Forms.Label();
            this.LblSalesRate = new System.Windows.Forms.Label();
            this.LblUnit = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BillTotalGroup = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.LblItemsDiscountSum = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.LblItemsTotal = new System.Windows.Forms.Label();
            this.lblTermAmount = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.lblTax = new System.Windows.Forms.Label();
            this.LblItemsTotalQty = new System.Windows.Forms.Label();
            this.lblNonTaxable = new System.Windows.Forms.Label();
            this.LblItemsNetAmount = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lblTaxable = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.BillInformatonGroup = new System.Windows.Forms.GroupBox();
            this.sidePanel1 = new DevExpress.XtraEditors.SidePanel();
            this.MnuMenuStrip = new System.Windows.Forms.MenuStrip();
            this.MnuRestaurantMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuAddOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuEstimateInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuReturnInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuReverseInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuOrderCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuOrderPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuRePrintOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuConfirmationPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuPrintInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuDayClosing = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuLockBilling = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuNightAudit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuTableTransfer = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuTableSplit = new System.Windows.Forms.ToolStripMenuItem();
            this.TxtAltUnit = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.TxtAltQty = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnProduct = new System.Windows.Forms.Button();
            this.TxtProduct = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtRefVno = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnRefVno = new System.Windows.Forms.Button();
            this.LblInvoiceNo = new System.Windows.Forms.Label();
            this.TxtCounter = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.MskDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.MskMiti = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.TxtVNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtUnit = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.BtnVno = new System.Windows.Forms.Button();
            this.TxtQty = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.LblVoucherCaption = new System.Windows.Forms.Label();
            this.PanelHeader.SuspendLayout();
            this.DataGridGroup.SuspendLayout();
            this.PnlInvoiceDetails.SuspendLayout();
            this.PDetails.SuspendLayout();
            this.MemberGroup.SuspendLayout();
            this.InvoiceGroup.SuspendLayout();
            this.CustomerGroup.SuspendLayout();
            this.CustomerDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).BeginInit();
            this.PnlProductDetails.SuspendLayout();
            this.BillTotalGroup.SuspendLayout();
            this.BillInformatonGroup.SuspendLayout();
            this.sidePanel1.SuspendLayout();
            this.MnuMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.DataGridGroup);
            this.PanelHeader.Controls.Add(this.BillTotalGroup);
            this.PanelHeader.Controls.Add(this.BillInformatonGroup);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(1347, 748);
            this.PanelHeader.TabIndex = 0;
            // 
            // DataGridGroup
            // 
            this.DataGridGroup.Controls.Add(this.PnlInvoiceDetails);
            this.DataGridGroup.Controls.Add(this.PDetails);
            this.DataGridGroup.Controls.Add(this.RGrid);
            this.DataGridGroup.Controls.Add(this.PnlProductDetails);
            this.DataGridGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridGroup.Location = new System.Drawing.Point(0, 85);
            this.DataGridGroup.Name = "DataGridGroup";
            this.DataGridGroup.Size = new System.Drawing.Size(1347, 606);
            this.DataGridGroup.TabIndex = 1;
            this.DataGridGroup.TabStop = false;
            // 
            // PnlInvoiceDetails
            // 
            this.PnlInvoiceDetails.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PnlInvoiceDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlInvoiceDetails.Controls.Add(this.label36);
            this.PnlInvoiceDetails.Controls.Add(this.LblDisplayReturnAmount);
            this.PnlInvoiceDetails.Controls.Add(this.label34);
            this.PnlInvoiceDetails.Controls.Add(this.LblDisplayReceivedAmount);
            this.PnlInvoiceDetails.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PnlInvoiceDetails.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlInvoiceDetails.Location = new System.Drawing.Point(3, 515);
            this.PnlInvoiceDetails.Name = "PnlInvoiceDetails";
            this.PnlInvoiceDetails.Size = new System.Drawing.Size(1341, 54);
            this.PnlInvoiceDetails.TabIndex = 305;
            // 
            // label36
            // 
            this.label36.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.ForeColor = System.Drawing.Color.Black;
            this.label36.Location = new System.Drawing.Point(405, 14);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(156, 20);
            this.label36.TabIndex = 152;
            this.label36.Text = "REFUND AMOUNT";
            // 
            // LblDisplayReturnAmount
            // 
            this.LblDisplayReturnAmount.BackColor = System.Drawing.SystemColors.HighlightText;
            this.LblDisplayReturnAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblDisplayReturnAmount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblDisplayReturnAmount.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDisplayReturnAmount.ForeColor = System.Drawing.Color.DarkGreen;
            this.LblDisplayReturnAmount.Location = new System.Drawing.Point(569, 2);
            this.LblDisplayReturnAmount.Name = "LblDisplayReturnAmount";
            this.LblDisplayReturnAmount.Size = new System.Drawing.Size(202, 45);
            this.LblDisplayReturnAmount.TabIndex = 151;
            this.LblDisplayReturnAmount.Text = "0.00";
            this.LblDisplayReturnAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label34
            // 
            this.label34.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.ForeColor = System.Drawing.Color.Black;
            this.label34.Location = new System.Drawing.Point(15, 14);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(172, 20);
            this.label34.TabIndex = 150;
            this.label34.Text = "RECEIVED AMOUNT";
            // 
            // LblDisplayReceivedAmount
            // 
            this.LblDisplayReceivedAmount.BackColor = System.Drawing.SystemColors.HighlightText;
            this.LblDisplayReceivedAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblDisplayReceivedAmount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblDisplayReceivedAmount.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDisplayReceivedAmount.ForeColor = System.Drawing.Color.DarkGreen;
            this.LblDisplayReceivedAmount.Location = new System.Drawing.Point(195, 2);
            this.LblDisplayReceivedAmount.Name = "LblDisplayReceivedAmount";
            this.LblDisplayReceivedAmount.Size = new System.Drawing.Size(202, 45);
            this.LblDisplayReceivedAmount.TabIndex = 149;
            this.LblDisplayReceivedAmount.Text = "0.00";
            this.LblDisplayReceivedAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PDetails
            // 
            this.PDetails.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PDetails.Controls.Add(this.MemberGroup);
            this.PDetails.Controls.Add(this.ChkPrint);
            this.PDetails.Controls.Add(this.InvoiceGroup);
            this.PDetails.Controls.Add(this.CustomerGroup);
            this.PDetails.Controls.Add(this.CustomerDetails);
            this.PDetails.Controls.Add(this.lbl_Remarks);
            this.PDetails.Controls.Add(this.ChkTaxInvoice);
            this.PDetails.Controls.Add(this.TxtRemarks);
            this.PDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PDetails.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PDetails.Location = new System.Drawing.Point(3, 22);
            this.PDetails.Name = "PDetails";
            this.PDetails.Size = new System.Drawing.Size(1341, 547);
            this.PDetails.TabIndex = 303;
            this.PDetails.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PDetails_PreviewKeyDown);
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
            this.MemberGroup.Location = new System.Drawing.Point(645, 202);
            this.MemberGroup.Name = "MemberGroup";
            this.MemberGroup.Size = new System.Drawing.Size(542, 134);
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
            this.LblTag.Location = new System.Drawing.Point(343, 52);
            this.LblTag.Name = "LblTag";
            this.LblTag.Size = new System.Drawing.Size(129, 23);
            this.LblTag.TabIndex = 160;
            // 
            // label26
            // 
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.Black;
            this.label26.Location = new System.Drawing.Point(284, 53);
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
            this.LblMemberAmount.Location = new System.Drawing.Point(152, 104);
            this.LblMemberAmount.Name = "LblMemberAmount";
            this.LblMemberAmount.Size = new System.Drawing.Size(130, 24);
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
            this.LblMemberType.Location = new System.Drawing.Point(152, 78);
            this.LblMemberType.Name = "LblMemberType";
            this.LblMemberType.Size = new System.Drawing.Size(130, 23);
            this.LblMemberType.TabIndex = 156;
            // 
            // LblMemberShortName
            // 
            this.LblMemberShortName.BackColor = System.Drawing.SystemColors.Window;
            this.LblMemberShortName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblMemberShortName.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMemberShortName.ForeColor = System.Drawing.Color.Black;
            this.LblMemberShortName.Location = new System.Drawing.Point(152, 52);
            this.LblMemberShortName.Name = "LblMemberShortName";
            this.LblMemberShortName.Size = new System.Drawing.Size(130, 23);
            this.LblMemberShortName.TabIndex = 155;
            // 
            // LblMemberName
            // 
            this.LblMemberName.BackColor = System.Drawing.SystemColors.Window;
            this.LblMemberName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblMemberName.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMemberName.ForeColor = System.Drawing.Color.Black;
            this.LblMemberName.Location = new System.Drawing.Point(152, 26);
            this.LblMemberName.Name = "LblMemberName";
            this.LblMemberName.Size = new System.Drawing.Size(320, 23);
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
            this.label17.Location = new System.Drawing.Point(5, 79);
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
            // ChkPrint
            // 
            this.ChkPrint.AutoSize = true;
            this.ChkPrint.Checked = true;
            this.ChkPrint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkPrint.Location = new System.Drawing.Point(1028, 7);
            this.ChkPrint.Name = "ChkPrint";
            this.ChkPrint.Size = new System.Drawing.Size(122, 23);
            this.ChkPrint.TabIndex = 3;
            this.ChkPrint.Text = "Print Invoice";
            this.ChkPrint.UseVisualStyleBackColor = true;
            // 
            // InvoiceGroup
            // 
            this.InvoiceGroup.Controls.Add(this.TxtTaxableAmount);
            this.InvoiceGroup.Controls.Add(this.label42);
            this.InvoiceGroup.Controls.Add(this.label40);
            this.InvoiceGroup.Controls.Add(this.TxtVatRate);
            this.InvoiceGroup.Controls.Add(this.TxtVatAmount);
            this.InvoiceGroup.Controls.Add(this.label41);
            this.InvoiceGroup.Controls.Add(this.label38);
            this.InvoiceGroup.Controls.Add(this.TxtServiceChargeRate);
            this.InvoiceGroup.Controls.Add(this.TxtServiceCharge);
            this.InvoiceGroup.Controls.Add(this.label37);
            this.InvoiceGroup.Controls.Add(this.TxtVoucherAmount);
            this.InvoiceGroup.Controls.Add(this.label35);
            this.InvoiceGroup.Controls.Add(this.LblNumberInWords);
            this.InvoiceGroup.Controls.Add(this.label111);
            this.InvoiceGroup.Controls.Add(this.TxtChangeAmount);
            this.InvoiceGroup.Controls.Add(this.label9);
            this.InvoiceGroup.Controls.Add(this.TxtTenderAmount);
            this.InvoiceGroup.Controls.Add(this.label7);
            this.InvoiceGroup.Controls.Add(this.TxtNetAmount);
            this.InvoiceGroup.Controls.Add(this.label6);
            this.InvoiceGroup.Controls.Add(this.label5);
            this.InvoiceGroup.Controls.Add(this.TxtBillDiscountAmount);
            this.InvoiceGroup.Controls.Add(this.TxtBillDiscountPercentage);
            this.InvoiceGroup.Controls.Add(this.label4);
            this.InvoiceGroup.Controls.Add(this.TxtBasicAmount);
            this.InvoiceGroup.Controls.Add(this.label2);
            this.InvoiceGroup.Location = new System.Drawing.Point(3, 33);
            this.InvoiceGroup.Name = "InvoiceGroup";
            this.InvoiceGroup.Size = new System.Drawing.Size(637, 455);
            this.InvoiceGroup.TabIndex = 0;
            this.InvoiceGroup.TabStop = false;
            // 
            // TxtTaxableAmount
            // 
            this.TxtTaxableAmount.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtTaxableAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTaxableAmount.Enabled = false;
            this.TxtTaxableAmount.Font = new System.Drawing.Font("Bookman Old Style", 16F);
            this.TxtTaxableAmount.Location = new System.Drawing.Point(238, 153);
            this.TxtTaxableAmount.MaxLength = 255;
            this.TxtTaxableAmount.Name = "TxtTaxableAmount";
            this.TxtTaxableAmount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtTaxableAmount.Size = new System.Drawing.Size(387, 33);
            this.TxtTaxableAmount.TabIndex = 9;
            this.TxtTaxableAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Bookman Old Style", 16F);
            this.label42.ForeColor = System.Drawing.Color.Navy;
            this.label42.Location = new System.Drawing.Point(12, 157);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(190, 25);
            this.label42.TabIndex = 311;
            this.label42.Text = "Taxable Amount";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(378, 194);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(22, 18);
            this.label40.TabIndex = 309;
            this.label40.Text = "%";
            // 
            // TxtVatRate
            // 
            this.TxtVatRate.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtVatRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtVatRate.Font = new System.Drawing.Font("Bookman Old Style", 16F);
            this.TxtVatRate.Location = new System.Drawing.Point(238, 187);
            this.TxtVatRate.MaxLength = 5;
            this.TxtVatRate.Name = "TxtVatRate";
            this.TxtVatRate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtVatRate.Size = new System.Drawing.Size(135, 33);
            this.TxtVatRate.TabIndex = 4;
            this.TxtVatRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtVatRate.TextChanged += new System.EventHandler(this.TxtVatRate_TextChanged);
            this.TxtVatRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtVatRate_KeyPress);
            this.TxtVatRate.Validating += new System.ComponentModel.CancelEventHandler(this.TxtVatRate_Validating);
            // 
            // TxtVatAmount
            // 
            this.TxtVatAmount.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtVatAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtVatAmount.Font = new System.Drawing.Font("Bookman Old Style", 16F);
            this.TxtVatAmount.Location = new System.Drawing.Point(404, 187);
            this.TxtVatAmount.MaxLength = 255;
            this.TxtVatAmount.Name = "TxtVatAmount";
            this.TxtVatAmount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtVatAmount.Size = new System.Drawing.Size(220, 33);
            this.TxtVatAmount.TabIndex = 5;
            this.TxtVatAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtVatAmount.TextChanged += new System.EventHandler(this.TxtVatAmount_TextChanged);
            this.TxtVatAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtVatAmount_KeyPress);
            this.TxtVatAmount.Validating += new System.ComponentModel.CancelEventHandler(this.TxtVatAmount_Validating);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Bookman Old Style", 16F);
            this.label41.Location = new System.Drawing.Point(14, 191);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(141, 25);
            this.label41.TabIndex = 308;
            this.label41.Text = "Vat Amount";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(377, 126);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(22, 18);
            this.label38.TabIndex = 305;
            this.label38.Text = "%";
            // 
            // TxtServiceChargeRate
            // 
            this.TxtServiceChargeRate.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtServiceChargeRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtServiceChargeRate.Font = new System.Drawing.Font("Bookman Old Style", 16F);
            this.TxtServiceChargeRate.Location = new System.Drawing.Point(238, 119);
            this.TxtServiceChargeRate.MaxLength = 5;
            this.TxtServiceChargeRate.Name = "TxtServiceChargeRate";
            this.TxtServiceChargeRate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtServiceChargeRate.Size = new System.Drawing.Size(135, 33);
            this.TxtServiceChargeRate.TabIndex = 2;
            this.TxtServiceChargeRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtServiceChargeRate.TextChanged += new System.EventHandler(this.TxtServiceChangeRate_TextChanged);
            this.TxtServiceChargeRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtServiceChargeRate_KeyPress);
            this.TxtServiceChargeRate.Validating += new System.ComponentModel.CancelEventHandler(this.TxtServiceChargeRate_Validating);
            // 
            // TxtServiceCharge
            // 
            this.TxtServiceCharge.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtServiceCharge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtServiceCharge.Font = new System.Drawing.Font("Bookman Old Style", 16F);
            this.TxtServiceCharge.Location = new System.Drawing.Point(404, 119);
            this.TxtServiceCharge.MaxLength = 255;
            this.TxtServiceCharge.Name = "TxtServiceCharge";
            this.TxtServiceCharge.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtServiceCharge.Size = new System.Drawing.Size(222, 33);
            this.TxtServiceCharge.TabIndex = 3;
            this.TxtServiceCharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtServiceCharge.TextChanged += new System.EventHandler(this.TxtServiceChange_TextChanged);
            this.TxtServiceCharge.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtServiceCharge_KeyPress);
            this.TxtServiceCharge.Validating += new System.ComponentModel.CancelEventHandler(this.TxtServiceCharge_Validating);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Bookman Old Style", 16F);
            this.label37.Location = new System.Drawing.Point(13, 123);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(176, 25);
            this.label37.TabIndex = 303;
            this.label37.Text = "Service Charge";
            // 
            // TxtVoucherAmount
            // 
            this.TxtVoucherAmount.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtVoucherAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtVoucherAmount.Enabled = false;
            this.TxtVoucherAmount.Font = new System.Drawing.Font("Bookman Old Style", 16F);
            this.TxtVoucherAmount.Location = new System.Drawing.Point(238, 85);
            this.TxtVoucherAmount.MaxLength = 255;
            this.TxtVoucherAmount.Name = "TxtVoucherAmount";
            this.TxtVoucherAmount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtVoucherAmount.Size = new System.Drawing.Size(387, 33);
            this.TxtVoucherAmount.TabIndex = 10;
            this.TxtVoucherAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Bookman Old Style", 16F);
            this.label35.ForeColor = System.Drawing.Color.Navy;
            this.label35.Location = new System.Drawing.Point(12, 89);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(163, 25);
            this.label35.TabIndex = 301;
            this.label35.Text = "Basic Amount";
            // 
            // LblNumberInWords
            // 
            this.LblNumberInWords.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNumberInWords.Location = new System.Drawing.Point(87, 369);
            this.LblNumberInWords.Name = "LblNumberInWords";
            this.LblNumberInWords.Size = new System.Drawing.Size(537, 59);
            this.LblNumberInWords.TabIndex = 225;
            this.LblNumberInWords.Text = "Only.";
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label111.Location = new System.Drawing.Point(8, 369);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(79, 20);
            this.label111.TabIndex = 224;
            this.label111.Text = "In Words";
            // 
            // TxtChangeAmount
            // 
            this.TxtChangeAmount.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtChangeAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtChangeAmount.Font = new System.Drawing.Font("Arial", 32F, System.Drawing.FontStyle.Bold);
            this.TxtChangeAmount.ForeColor = System.Drawing.Color.DarkSalmon;
            this.TxtChangeAmount.Location = new System.Drawing.Point(237, 309);
            this.TxtChangeAmount.MaxLength = 255;
            this.TxtChangeAmount.Name = "TxtChangeAmount";
            this.TxtChangeAmount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtChangeAmount.Size = new System.Drawing.Size(387, 57);
            this.TxtChangeAmount.TabIndex = 7;
            this.TxtChangeAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Navy;
            this.label9.Location = new System.Drawing.Point(11, 321);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(228, 32);
            this.label9.TabIndex = 229;
            this.label9.Text = "Change Amount";
            // 
            // TxtTenderAmount
            // 
            this.TxtTenderAmount.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtTenderAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTenderAmount.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold);
            this.TxtTenderAmount.Location = new System.Drawing.Point(237, 263);
            this.TxtTenderAmount.MaxLength = 255;
            this.TxtTenderAmount.Name = "TxtTenderAmount";
            this.TxtTenderAmount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtTenderAmount.Size = new System.Drawing.Size(387, 44);
            this.TxtTenderAmount.TabIndex = 6;
            this.TxtTenderAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtTenderAmount.TextChanged += new System.EventHandler(this.TxtTenderAmount_TextChanged);
            this.TxtTenderAmount.Enter += new System.EventHandler(this.TxtTenderAmt_Enter);
            this.TxtTenderAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtTenderAmt_KeyDown);
            this.TxtTenderAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtTenderAmt_KeyPress);
            this.TxtTenderAmount.Leave += new System.EventHandler(this.TxtTenderAmt_Leave);
            this.TxtTenderAmount.Validating += new System.ComponentModel.CancelEventHandler(this.TxtTenderAmt_Validating);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Bookman Old Style", 20F);
            this.label7.Location = new System.Drawing.Point(11, 269);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(218, 32);
            this.label7.TabIndex = 227;
            this.label7.Text = "Tender Amount";
            // 
            // TxtNetAmount
            // 
            this.TxtNetAmount.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNetAmount.Enabled = false;
            this.TxtNetAmount.Font = new System.Drawing.Font("Bookman Old Style", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtNetAmount.Location = new System.Drawing.Point(237, 222);
            this.TxtNetAmount.MaxLength = 255;
            this.TxtNetAmount.Name = "TxtNetAmount";
            this.TxtNetAmount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtNetAmount.Size = new System.Drawing.Size(387, 39);
            this.TxtNetAmount.TabIndex = 8;
            this.TxtNetAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Navy;
            this.label6.Location = new System.Drawing.Point(11, 224);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(171, 32);
            this.label6.TabIndex = 225;
            this.label6.Text = "Net Amount";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(379, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 18);
            this.label5.TabIndex = 223;
            this.label5.Text = "%";
            // 
            // TxtBillDiscountAmount
            // 
            this.TxtBillDiscountAmount.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtBillDiscountAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBillDiscountAmount.Font = new System.Drawing.Font("Bookman Old Style", 16F);
            this.TxtBillDiscountAmount.Location = new System.Drawing.Point(404, 51);
            this.TxtBillDiscountAmount.MaxLength = 255;
            this.TxtBillDiscountAmount.Name = "TxtBillDiscountAmount";
            this.TxtBillDiscountAmount.Size = new System.Drawing.Size(221, 33);
            this.TxtBillDiscountAmount.TabIndex = 1;
            this.TxtBillDiscountAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtBillDiscountAmount.TextChanged += new System.EventHandler(this.TxtBillDiscountAmount_TextChanged);
            this.TxtBillDiscountAmount.Enter += new System.EventHandler(this.TxtBillDiscountAmount_Enter);
            this.TxtBillDiscountAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBillDiscountAmount_KeyPress);
            this.TxtBillDiscountAmount.Leave += new System.EventHandler(this.TxtBillDiscountAmount_Leave);
            this.TxtBillDiscountAmount.Validating += new System.ComponentModel.CancelEventHandler(this.TxtBillDiscountAmount_Validating);
            // 
            // TxtBillDiscountPercentage
            // 
            this.TxtBillDiscountPercentage.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtBillDiscountPercentage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBillDiscountPercentage.Font = new System.Drawing.Font("Bookman Old Style", 16F);
            this.TxtBillDiscountPercentage.Location = new System.Drawing.Point(238, 51);
            this.TxtBillDiscountPercentage.MaxLength = 5;
            this.TxtBillDiscountPercentage.Name = "TxtBillDiscountPercentage";
            this.TxtBillDiscountPercentage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtBillDiscountPercentage.Size = new System.Drawing.Size(135, 33);
            this.TxtBillDiscountPercentage.TabIndex = 0;
            this.TxtBillDiscountPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtBillDiscountPercentage.TextChanged += new System.EventHandler(this.TxtBillDiscountPercentage_TextChanged);
            this.TxtBillDiscountPercentage.Enter += new System.EventHandler(this.TxtBillDiscountPercentage_Enter);
            this.TxtBillDiscountPercentage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBillDiscountPercentage_KeyPress);
            this.TxtBillDiscountPercentage.Leave += new System.EventHandler(this.TxtBillDiscountPercentage_Leave);
            this.TxtBillDiscountPercentage.Validated += new System.EventHandler(this.TxtBillDiscountPercentage_Validated);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 16F);
            this.label4.Location = new System.Drawing.Point(12, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 25);
            this.label4.TabIndex = 58;
            this.label4.Text = "Discount";
            // 
            // TxtBasicAmount
            // 
            this.TxtBasicAmount.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtBasicAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBasicAmount.Enabled = false;
            this.TxtBasicAmount.Font = new System.Drawing.Font("Bookman Old Style", 16F);
            this.TxtBasicAmount.Location = new System.Drawing.Point(238, 15);
            this.TxtBasicAmount.MaxLength = 255;
            this.TxtBasicAmount.Name = "TxtBasicAmount";
            this.TxtBasicAmount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtBasicAmount.Size = new System.Drawing.Size(387, 33);
            this.TxtBasicAmount.TabIndex = 11;
            this.TxtBasicAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 16F);
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(12, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 25);
            this.label2.TabIndex = 56;
            this.label2.Text = "Bill Amount";
            // 
            // CustomerGroup
            // 
            this.CustomerGroup.Controls.Add(this.label8);
            this.CustomerGroup.Controls.Add(this.TxtMember);
            this.CustomerGroup.Controls.Add(this.btnMember);
            this.CustomerGroup.Controls.Add(this.CmbPaymentType);
            this.CustomerGroup.Controls.Add(this.label30);
            this.CustomerGroup.Controls.Add(this.lbl_CBLedger);
            this.CustomerGroup.Controls.Add(this.btnCustomer);
            this.CustomerGroup.Controls.Add(this.TxtCustomer);
            this.CustomerGroup.Controls.Add(this.BtnSave);
            this.CustomerGroup.Location = new System.Drawing.Point(645, 33);
            this.CustomerGroup.Name = "CustomerGroup";
            this.CustomerGroup.Size = new System.Drawing.Size(545, 167);
            this.CustomerGroup.TabIndex = 0;
            this.CustomerGroup.TabStop = false;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(5, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 20);
            this.label8.TabIndex = 333;
            this.label8.Text = "Member";
            // 
            // TxtMember
            // 
            this.TxtMember.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtMember.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMember.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtMember.Location = new System.Drawing.Point(128, 14);
            this.TxtMember.MaxLength = 255;
            this.TxtMember.Name = "TxtMember";
            this.TxtMember.Size = new System.Drawing.Size(379, 26);
            this.TxtMember.TabIndex = 0;
            this.TxtMember.Tag = "0";
            this.TxtMember.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtMember_KeyDown);
            this.TxtMember.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtMember_KeyPress);
            this.TxtMember.Validated += new System.EventHandler(this.TxtMember_Validated);
            // 
            // btnMember
            // 
            this.btnMember.CausesValidation = false;
            this.btnMember.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMember.ForeColor = System.Drawing.SystemColors.Window;
            this.btnMember.Image = global::MrBLL.Properties.Resources.search16;
            this.btnMember.Location = new System.Drawing.Point(507, 14);
            this.btnMember.Name = "btnMember";
            this.btnMember.Size = new System.Drawing.Size(29, 27);
            this.btnMember.TabIndex = 332;
            this.btnMember.TabStop = false;
            this.btnMember.UseVisualStyleBackColor = true;
            this.btnMember.Click += new System.EventHandler(this.BtnMember_Click);
            // 
            // CmbPaymentType
            // 
            this.CmbPaymentType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CmbPaymentType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CmbPaymentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbPaymentType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbPaymentType.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.CmbPaymentType.FormattingEnabled = true;
            this.CmbPaymentType.Items.AddRange(new object[] {
            "Cash",
            "Credit",
            "Card",
            "Other"});
            this.CmbPaymentType.Location = new System.Drawing.Point(128, 42);
            this.CmbPaymentType.Name = "CmbPaymentType";
            this.CmbPaymentType.Size = new System.Drawing.Size(318, 28);
            this.CmbPaymentType.TabIndex = 1;
            this.CmbPaymentType.SelectedIndexChanged += new System.EventHandler(this.CmbPaymentType_SelectedIndexChanged);
            this.CmbPaymentType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbPaymentType_KeyDown);
            this.CmbPaymentType.Leave += new System.EventHandler(this.CmbPaymentType_Leave);
            // 
            // label30
            // 
            this.label30.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label30.Location = new System.Drawing.Point(4, 46);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(120, 20);
            this.label30.TabIndex = 200;
            this.label30.Text = "Payment Type";
            // 
            // lbl_CBLedger
            // 
            this.lbl_CBLedger.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_CBLedger.AutoSize = true;
            this.lbl_CBLedger.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_CBLedger.Location = new System.Drawing.Point(5, 75);
            this.lbl_CBLedger.Name = "lbl_CBLedger";
            this.lbl_CBLedger.Size = new System.Drawing.Size(86, 20);
            this.lbl_CBLedger.TabIndex = 296;
            this.lbl_CBLedger.Text = "Customer";
            // 
            // btnCustomer
            // 
            this.btnCustomer.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.btnCustomer.Image = global::MrBLL.Properties.Resources.search16;
            this.btnCustomer.Location = new System.Drawing.Point(507, 72);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Size = new System.Drawing.Size(29, 27);
            this.btnCustomer.TabIndex = 297;
            this.btnCustomer.TabStop = false;
            this.btnCustomer.UseVisualStyleBackColor = true;
            this.btnCustomer.Click += new System.EventHandler(this.BtnCustomer_Click);
            // 
            // TxtCustomer
            // 
            this.TxtCustomer.BackColor = System.Drawing.Color.White;
            this.TxtCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCustomer.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.TxtCustomer.Location = new System.Drawing.Point(128, 72);
            this.TxtCustomer.MaxLength = 255;
            this.TxtCustomer.Name = "TxtCustomer";
            this.TxtCustomer.ReadOnly = true;
            this.TxtCustomer.Size = new System.Drawing.Size(377, 26);
            this.TxtCustomer.TabIndex = 2;
            this.TxtCustomer.TextChanged += new System.EventHandler(this.TxtCustomer_TextChanged);
            this.TxtCustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCustomer_KeyDown);
            this.TxtCustomer.Validating += new System.ComponentModel.CancelEventHandler(this.TxtCustomer_Validating);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Appearance.Options.UseForeColor = true;
            this.BtnSave.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(152, 101);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(231, 61);
            this.BtnSave.TabIndex = 9;
            this.BtnSave.Text = "&Save";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // CustomerDetails
            // 
            this.CustomerDetails.Controls.Add(this.lblCreditDays);
            this.CustomerDetails.Controls.Add(this.lblPan);
            this.CustomerDetails.Controls.Add(this.label10);
            this.CustomerDetails.Controls.Add(this.label25);
            this.CustomerDetails.Controls.Add(this.lbl_Currentbal);
            this.CustomerDetails.Controls.Add(this.lblCrLimit);
            this.CustomerDetails.Controls.Add(this.label14);
            this.CustomerDetails.Controls.Add(this.lbl_CurrentBalance);
            this.CustomerDetails.Location = new System.Drawing.Point(645, 328);
            this.CustomerDetails.Name = "CustomerDetails";
            this.CustomerDetails.Size = new System.Drawing.Size(544, 159);
            this.CustomerDetails.TabIndex = 306;
            this.CustomerDetails.TabStop = false;
            // 
            // lblCreditDays
            // 
            this.lblCreditDays.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblCreditDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCreditDays.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCreditDays.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditDays.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblCreditDays.Location = new System.Drawing.Point(128, 121);
            this.lblCreditDays.Name = "lblCreditDays";
            this.lblCreditDays.Size = new System.Drawing.Size(341, 33);
            this.lblCreditDays.TabIndex = 175;
            this.lblCreditDays.Text = "0";
            this.lblCreditDays.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPan
            // 
            this.lblPan.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblPan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPan.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPan.ForeColor = System.Drawing.Color.Black;
            this.lblPan.Location = new System.Drawing.Point(128, 14);
            this.lblPan.Name = "lblPan";
            this.lblPan.Size = new System.Drawing.Size(341, 33);
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
            this.label10.Location = new System.Drawing.Point(5, 20);
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
            this.label25.Location = new System.Drawing.Point(5, 127);
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
            this.lbl_Currentbal.Location = new System.Drawing.Point(5, 56);
            this.lbl_Currentbal.Name = "lbl_Currentbal";
            this.lbl_Currentbal.Size = new System.Drawing.Size(72, 20);
            this.lbl_Currentbal.TabIndex = 148;
            this.lbl_Currentbal.Text = "Balance";
            // 
            // lblCrLimit
            // 
            this.lblCrLimit.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lblCrLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCrLimit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCrLimit.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCrLimit.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblCrLimit.Location = new System.Drawing.Point(128, 85);
            this.lblCrLimit.Name = "lblCrLimit";
            this.lblCrLimit.Size = new System.Drawing.Size(341, 33);
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
            this.label14.Location = new System.Drawing.Point(5, 91);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(104, 20);
            this.label14.TabIndex = 152;
            this.label14.Text = "Credit Limit";
            // 
            // lbl_CurrentBalance
            // 
            this.lbl_CurrentBalance.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lbl_CurrentBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_CurrentBalance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_CurrentBalance.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CurrentBalance.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_CurrentBalance.Location = new System.Drawing.Point(128, 50);
            this.lbl_CurrentBalance.Name = "lbl_CurrentBalance";
            this.lbl_CurrentBalance.Size = new System.Drawing.Size(341, 33);
            this.lbl_CurrentBalance.TabIndex = 9;
            this.lbl_CurrentBalance.Text = "0.00";
            this.lbl_CurrentBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_Remarks
            // 
            this.lbl_Remarks.AutoSize = true;
            this.lbl_Remarks.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Remarks.Location = new System.Drawing.Point(11, 9);
            this.lbl_Remarks.Name = "lbl_Remarks";
            this.lbl_Remarks.Size = new System.Drawing.Size(79, 20);
            this.lbl_Remarks.TabIndex = 223;
            this.lbl_Remarks.Text = "Remarks";
            // 
            // ChkTaxInvoice
            // 
            this.ChkTaxInvoice.AutoSize = true;
            this.ChkTaxInvoice.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkTaxInvoice.Location = new System.Drawing.Point(1160, 6);
            this.ChkTaxInvoice.Name = "ChkTaxInvoice";
            this.ChkTaxInvoice.Size = new System.Drawing.Size(109, 24);
            this.ChkTaxInvoice.TabIndex = 299;
            this.ChkTaxInvoice.Text = "TaxInvoice";
            this.ChkTaxInvoice.UseVisualStyleBackColor = true;
            // 
            // TxtRemarks
            // 
            this.TxtRemarks.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtRemarks.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtRemarks.Location = new System.Drawing.Point(93, 6);
            this.TxtRemarks.MaxLength = 255;
            this.TxtRemarks.Name = "TxtRemarks";
            this.TxtRemarks.Size = new System.Drawing.Size(929, 26);
            this.TxtRemarks.TabIndex = 10;
            this.TxtRemarks.Enter += new System.EventHandler(this.TxtRemarks_Enter);
            this.TxtRemarks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtRemarks_KeyDown);
            this.TxtRemarks.Leave += new System.EventHandler(this.TxtRemarks_Leave);
            this.TxtRemarks.Validated += new System.EventHandler(this.TxtRemarks_Validated);
            // 
            // RGrid
            // 
            this.RGrid.AllowUserToAddRows = false;
            this.RGrid.AllowUserToDeleteRows = false;
            this.RGrid.AllowUserToResizeColumns = false;
            this.RGrid.AllowUserToResizeRows = false;
            this.RGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.RGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.RGrid.ColumnHeadersHeight = 27;
            this.RGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.RGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.RGrid.Location = new System.Drawing.Point(3, 22);
            this.RGrid.Name = "RGrid";
            this.RGrid.ReadOnly = true;
            this.RGrid.RowHeadersVisible = false;
            this.RGrid.RowHeadersWidth = 51;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RGrid.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.RGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RGrid.Size = new System.Drawing.Size(1341, 547);
            this.RGrid.TabIndex = 21;
            this.RGrid.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_CellContentDoubleClick);
            this.RGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_CellEnter);
            this.RGrid.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.RGrid_RowHeaderMouseDoubleClick);
            this.RGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_RowEnter);
            this.RGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RGrid_KeyDown);
            // 
            // PnlProductDetails
            // 
            this.PnlProductDetails.BackColor = System.Drawing.SystemColors.InactiveCaption;
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
            this.PnlProductDetails.Location = new System.Drawing.Point(3, 569);
            this.PnlProductDetails.Name = "PnlProductDetails";
            this.PnlProductDetails.Size = new System.Drawing.Size(1341, 34);
            this.PnlProductDetails.TabIndex = 304;
            // 
            // LblProduct
            // 
            this.LblProduct.BackColor = System.Drawing.Color.White;
            this.LblProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblProduct.Location = new System.Drawing.Point(86, 6);
            this.LblProduct.Name = "LblProduct";
            this.LblProduct.Size = new System.Drawing.Size(409, 22);
            this.LblProduct.TabIndex = 7;
            // 
            // LblBarcode
            // 
            this.LblBarcode.BackColor = System.Drawing.Color.White;
            this.LblBarcode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblBarcode.Location = new System.Drawing.Point(573, 6);
            this.LblBarcode.Name = "LblBarcode";
            this.LblBarcode.Size = new System.Drawing.Size(166, 22);
            this.LblBarcode.TabIndex = 6;
            // 
            // LblSalesRate
            // 
            this.LblSalesRate.BackColor = System.Drawing.Color.White;
            this.LblSalesRate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblSalesRate.Location = new System.Drawing.Point(967, 6);
            this.LblSalesRate.Name = "LblSalesRate";
            this.LblSalesRate.Size = new System.Drawing.Size(119, 22);
            this.LblSalesRate.TabIndex = 5;
            this.LblSalesRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblUnit
            // 
            this.LblUnit.BackColor = System.Drawing.Color.White;
            this.LblUnit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblUnit.Location = new System.Drawing.Point(793, 6);
            this.LblUnit.Name = "LblUnit";
            this.LblUnit.Size = new System.Drawing.Size(119, 22);
            this.LblUnit.TabIndex = 4;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(912, 7);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(54, 19);
            this.label33.TabIndex = 3;
            this.label33.Text = "Rate :";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(739, 7);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(51, 19);
            this.label23.TabIndex = 2;
            this.label23.Text = "UOM:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(495, 7);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 19);
            this.label11.TabIndex = 1;
            this.label11.Text = "Barcode:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "Product :";
            // 
            // BillTotalGroup
            // 
            this.BillTotalGroup.Controls.Add(this.label21);
            this.BillTotalGroup.Controls.Add(this.LblItemsDiscountSum);
            this.BillTotalGroup.Controls.Add(this.label32);
            this.BillTotalGroup.Controls.Add(this.LblItemsTotal);
            this.BillTotalGroup.Controls.Add(this.lblTermAmount);
            this.BillTotalGroup.Controls.Add(this.label31);
            this.BillTotalGroup.Controls.Add(this.lblTax);
            this.BillTotalGroup.Controls.Add(this.LblItemsTotalQty);
            this.BillTotalGroup.Controls.Add(this.lblNonTaxable);
            this.BillTotalGroup.Controls.Add(this.LblItemsNetAmount);
            this.BillTotalGroup.Controls.Add(this.label29);
            this.BillTotalGroup.Controls.Add(this.label22);
            this.BillTotalGroup.Controls.Add(this.lblTaxable);
            this.BillTotalGroup.Controls.Add(this.label24);
            this.BillTotalGroup.Controls.Add(this.label28);
            this.BillTotalGroup.Controls.Add(this.label27);
            this.BillTotalGroup.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BillTotalGroup.Location = new System.Drawing.Point(0, 691);
            this.BillTotalGroup.Name = "BillTotalGroup";
            this.BillTotalGroup.Size = new System.Drawing.Size(1347, 57);
            this.BillTotalGroup.TabIndex = 0;
            this.BillTotalGroup.TabStop = false;
            this.BillTotalGroup.Text = "Invoice Total";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(470, 26);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(80, 20);
            this.label21.TabIndex = 253;
            this.label21.Text = "Discount";
            // 
            // LblItemsDiscountSum
            // 
            this.LblItemsDiscountSum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblItemsDiscountSum.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblItemsDiscountSum.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblItemsDiscountSum.Location = new System.Drawing.Point(550, 22);
            this.LblItemsDiscountSum.Name = "LblItemsDiscountSum";
            this.LblItemsDiscountSum.Size = new System.Drawing.Size(126, 28);
            this.LblItemsDiscountSum.TabIndex = 252;
            this.LblItemsDiscountSum.Text = "0.00";
            this.LblItemsDiscountSum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(225, 26);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(119, 20);
            this.label32.TabIndex = 247;
            this.label32.Text = "Basic Amount";
            // 
            // LblItemsTotal
            // 
            this.LblItemsTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblItemsTotal.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblItemsTotal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblItemsTotal.Location = new System.Drawing.Point(344, 22);
            this.LblItemsTotal.Name = "LblItemsTotal";
            this.LblItemsTotal.Size = new System.Drawing.Size(126, 28);
            this.LblItemsTotal.TabIndex = 246;
            this.LblItemsTotal.Text = "0.00";
            this.LblItemsTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTermAmount
            // 
            this.lblTermAmount.AutoSize = true;
            this.lblTermAmount.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblTermAmount.Location = new System.Drawing.Point(1206, 16);
            this.lblTermAmount.Name = "lblTermAmount";
            this.lblTermAmount.Size = new System.Drawing.Size(29, 13);
            this.lblTermAmount.TabIndex = 251;
            this.lblTermAmount.Text = "0.00";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(14, 26);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(85, 20);
            this.label31.TabIndex = 248;
            this.label31.Text = "Total  Qty";
            // 
            // lblTax
            // 
            this.lblTax.AutoSize = true;
            this.lblTax.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblTax.Location = new System.Drawing.Point(1140, 16);
            this.lblTax.Name = "lblTax";
            this.lblTax.Size = new System.Drawing.Size(29, 13);
            this.lblTax.TabIndex = 251;
            this.lblTax.Text = "0.00";
            // 
            // LblItemsTotalQty
            // 
            this.LblItemsTotalQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblItemsTotalQty.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblItemsTotalQty.Location = new System.Drawing.Point(99, 22);
            this.LblItemsTotalQty.Name = "LblItemsTotalQty";
            this.LblItemsTotalQty.Size = new System.Drawing.Size(126, 28);
            this.LblItemsTotalQty.TabIndex = 249;
            this.LblItemsTotalQty.Text = "0.00";
            this.LblItemsTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNonTaxable
            // 
            this.lblNonTaxable.AutoSize = true;
            this.lblNonTaxable.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblNonTaxable.Location = new System.Drawing.Point(1039, 16);
            this.lblNonTaxable.Name = "lblNonTaxable";
            this.lblNonTaxable.Size = new System.Drawing.Size(29, 13);
            this.lblNonTaxable.TabIndex = 251;
            this.lblNonTaxable.Text = "0.00";
            // 
            // LblItemsNetAmount
            // 
            this.LblItemsNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblItemsNetAmount.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblItemsNetAmount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblItemsNetAmount.Location = new System.Drawing.Point(798, 22);
            this.LblItemsNetAmount.Name = "LblItemsNetAmount";
            this.LblItemsNetAmount.Size = new System.Drawing.Size(135, 28);
            this.LblItemsNetAmount.TabIndex = 250;
            this.LblItemsNetAmount.Text = "0.00";
            this.LblItemsNetAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(679, 26);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(116, 20);
            this.label29.TabIndex = 251;
            this.label29.Text = "Total Net Amt";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label22.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label22.Location = new System.Drawing.Point(1206, 33);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(70, 13);
            this.label22.TabIndex = 251;
            this.label22.Text = "Term amount";
            // 
            // lblTaxable
            // 
            this.lblTaxable.AutoSize = true;
            this.lblTaxable.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblTaxable.Location = new System.Drawing.Point(943, 16);
            this.lblTaxable.Name = "lblTaxable";
            this.lblTaxable.Size = new System.Drawing.Size(29, 13);
            this.lblTaxable.TabIndex = 251;
            this.lblTaxable.Text = "0.00";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label24.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label24.Location = new System.Drawing.Point(1140, 33);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(25, 13);
            this.label24.TabIndex = 251;
            this.label24.Text = "Tax";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label28.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label28.Location = new System.Drawing.Point(943, 33);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(45, 13);
            this.label28.TabIndex = 251;
            this.label28.Text = "Taxable";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label27.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label27.Location = new System.Drawing.Point(1023, 33);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(64, 13);
            this.label27.TabIndex = 251;
            this.label27.Text = "NonTaxable";
            // 
            // BillInformatonGroup
            // 
            this.BillInformatonGroup.Controls.Add(this.sidePanel1);
            this.BillInformatonGroup.Controls.Add(this.TxtAltUnit);
            this.BillInformatonGroup.Controls.Add(this.label39);
            this.BillInformatonGroup.Controls.Add(this.TxtAltQty);
            this.BillInformatonGroup.Controls.Add(this.BtnProduct);
            this.BillInformatonGroup.Controls.Add(this.TxtProduct);
            this.BillInformatonGroup.Controls.Add(this.TxtRefVno);
            this.BillInformatonGroup.Controls.Add(this.BtnRefVno);
            this.BillInformatonGroup.Controls.Add(this.LblInvoiceNo);
            this.BillInformatonGroup.Controls.Add(this.TxtCounter);
            this.BillInformatonGroup.Controls.Add(this.clsSeparator1);
            this.BillInformatonGroup.Controls.Add(this.MskDate);
            this.BillInformatonGroup.Controls.Add(this.MskMiti);
            this.BillInformatonGroup.Controls.Add(this.label13);
            this.BillInformatonGroup.Controls.Add(this.label12);
            this.BillInformatonGroup.Controls.Add(this.TxtVNo);
            this.BillInformatonGroup.Controls.Add(this.label1);
            this.BillInformatonGroup.Controls.Add(this.TxtUnit);
            this.BillInformatonGroup.Controls.Add(this.label19);
            this.BillInformatonGroup.Controls.Add(this.BtnVno);
            this.BillInformatonGroup.Controls.Add(this.TxtQty);
            this.BillInformatonGroup.Controls.Add(this.label20);
            this.BillInformatonGroup.Controls.Add(this.LblVoucherCaption);
            this.BillInformatonGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.BillInformatonGroup.Location = new System.Drawing.Point(0, 0);
            this.BillInformatonGroup.Name = "BillInformatonGroup";
            this.BillInformatonGroup.Size = new System.Drawing.Size(1347, 85);
            this.BillInformatonGroup.TabIndex = 0;
            this.BillInformatonGroup.TabStop = false;
            // 
            // sidePanel1
            // 
            this.sidePanel1.Controls.Add(this.MnuMenuStrip);
            this.sidePanel1.Location = new System.Drawing.Point(1136, 12);
            this.sidePanel1.Name = "sidePanel1";
            this.sidePanel1.Size = new System.Drawing.Size(202, 31);
            this.sidePanel1.TabIndex = 312;
            this.sidePanel1.Text = "sidePanel1";
            // 
            // MnuMenuStrip
            // 
            this.MnuMenuStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MnuMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MnuMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuRestaurantMenu});
            this.MnuMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MnuMenuStrip.Name = "MnuMenuStrip";
            this.MnuMenuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.MnuMenuStrip.Size = new System.Drawing.Size(202, 31);
            this.MnuMenuStrip.TabIndex = 350;
            this.MnuMenuStrip.Text = "RESTAURANT MENU";
            // 
            // MnuRestaurantMenu
            // 
            this.MnuRestaurantMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuAddOrder,
            this.MnuEstimateInvoice,
            this.MnuReturnInvoice,
            this.MnuReverseInvoice,
            this.toolStripSeparator5,
            this.MnuOrderCancel,
            this.toolStripSeparator4,
            this.MnuOrderPrint,
            this.MnuRePrintOrder,
            this.MnuConfirmationPrint,
            this.MnuPrintInvoice,
            this.toolStripSeparator3,
            this.MnuDayClosing,
            this.MnuLockBilling,
            this.toolStripSeparator2,
            this.MnuNightAudit,
            this.toolStripSeparator1,
            this.MnuTableTransfer,
            this.MnuTableSplit});
            this.MnuRestaurantMenu.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MnuRestaurantMenu.Name = "MnuRestaurantMenu";
            this.MnuRestaurantMenu.Size = new System.Drawing.Size(186, 27);
            this.MnuRestaurantMenu.Text = "RESTAURANT MENU";
            // 
            // MnuAddOrder
            // 
            this.MnuAddOrder.Name = "MnuAddOrder";
            this.MnuAddOrder.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.MnuAddOrder.Size = new System.Drawing.Size(299, 24);
            this.MnuAddOrder.Text = "ADD ORDER";
            this.MnuAddOrder.Click += new System.EventHandler(this.MnuAddOrder_Click);
            // 
            // MnuEstimateInvoice
            // 
            this.MnuEstimateInvoice.Name = "MnuEstimateInvoice";
            this.MnuEstimateInvoice.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
            this.MnuEstimateInvoice.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.MnuEstimateInvoice.Size = new System.Drawing.Size(299, 24);
            this.MnuEstimateInvoice.Text = "ESTIMATE BILL";
            // 
            // MnuReturnInvoice
            // 
            this.MnuReturnInvoice.Name = "MnuReturnInvoice";
            this.MnuReturnInvoice.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.R)));
            this.MnuReturnInvoice.Size = new System.Drawing.Size(299, 24);
            this.MnuReturnInvoice.Text = "RETURN INVOICE";
            this.MnuReturnInvoice.Click += new System.EventHandler(this.MnuReturnInvoice_Click);
            // 
            // MnuReverseInvoice
            // 
            this.MnuReverseInvoice.Name = "MnuReverseInvoice";
            this.MnuReverseInvoice.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.MnuReverseInvoice.Size = new System.Drawing.Size(299, 24);
            this.MnuReverseInvoice.Text = "REVERSE INVOICE";
            this.MnuReverseInvoice.Click += new System.EventHandler(this.MnuReverseInvoice_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(296, 6);
            // 
            // MnuOrderCancel
            // 
            this.MnuOrderCancel.Name = "MnuOrderCancel";
            this.MnuOrderCancel.Size = new System.Drawing.Size(299, 24);
            this.MnuOrderCancel.Text = "ORDER CANCEL";
            this.MnuOrderCancel.Click += new System.EventHandler(this.MnuOrderCancel_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(296, 6);
            // 
            // MnuOrderPrint
            // 
            this.MnuOrderPrint.Name = "MnuOrderPrint";
            this.MnuOrderPrint.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.P)));
            this.MnuOrderPrint.Size = new System.Drawing.Size(299, 24);
            this.MnuOrderPrint.Text = "ORDER PRINT";
            this.MnuOrderPrint.Click += new System.EventHandler(this.MnuOrderPrint_Click);
            // 
            // MnuRePrintOrder
            // 
            this.MnuRePrintOrder.Name = "MnuRePrintOrder";
            this.MnuRePrintOrder.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.P)));
            this.MnuRePrintOrder.Size = new System.Drawing.Size(299, 24);
            this.MnuRePrintOrder.Text = "RE-PRINT ORDER";
            this.MnuRePrintOrder.Click += new System.EventHandler(this.MnuRePrintOrder_Click);
            // 
            // MnuConfirmationPrint
            // 
            this.MnuConfirmationPrint.Name = "MnuConfirmationPrint";
            this.MnuConfirmationPrint.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.MnuConfirmationPrint.Size = new System.Drawing.Size(299, 24);
            this.MnuConfirmationPrint.Text = "CONFIRMATION PRINT";
            this.MnuConfirmationPrint.Click += new System.EventHandler(this.MnuConfirmationPrint_Click);
            // 
            // MnuPrintInvoice
            // 
            this.MnuPrintInvoice.Name = "MnuPrintInvoice";
            this.MnuPrintInvoice.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.MnuPrintInvoice.Size = new System.Drawing.Size(299, 24);
            this.MnuPrintInvoice.Text = "PRINT INVOICE";
            this.MnuPrintInvoice.Click += new System.EventHandler(this.MnuPrintInvoice_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(296, 6);
            // 
            // MnuDayClosing
            // 
            this.MnuDayClosing.Name = "MnuDayClosing";
            this.MnuDayClosing.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.MnuDayClosing.Size = new System.Drawing.Size(299, 24);
            this.MnuDayClosing.Text = "SHIFT CLOSED";
            this.MnuDayClosing.Click += new System.EventHandler(this.MnuDayClosing_Click);
            // 
            // MnuLockBilling
            // 
            this.MnuLockBilling.Name = "MnuLockBilling";
            this.MnuLockBilling.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.MnuLockBilling.Size = new System.Drawing.Size(299, 24);
            this.MnuLockBilling.Text = "LOCK BILLING";
            this.MnuLockBilling.Click += new System.EventHandler(this.MnuLockBilling_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(296, 6);
            // 
            // MnuNightAudit
            // 
            this.MnuNightAudit.Name = "MnuNightAudit";
            this.MnuNightAudit.Size = new System.Drawing.Size(299, 24);
            this.MnuNightAudit.Text = "NIGHT AUDIT";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(296, 6);
            // 
            // MnuTableTransfer
            // 
            this.MnuTableTransfer.Name = "MnuTableTransfer";
            this.MnuTableTransfer.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.MnuTableTransfer.Size = new System.Drawing.Size(299, 24);
            this.MnuTableTransfer.Text = "TABLE TRANSFER";
            this.MnuTableTransfer.Click += new System.EventHandler(this.MnuTableTransfer_Click);
            // 
            // MnuTableSplit
            // 
            this.MnuTableSplit.Name = "MnuTableSplit";
            this.MnuTableSplit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.MnuTableSplit.Size = new System.Drawing.Size(299, 24);
            this.MnuTableSplit.Text = "TABLE SPLIT";
            this.MnuTableSplit.Click += new System.EventHandler(this.MnuTableSplit_Click);
            // 
            // TxtAltUnit
            // 
            this.TxtAltUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAltUnit.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAltUnit.Location = new System.Drawing.Point(912, 53);
            this.TxtAltUnit.MaxLength = 255;
            this.TxtAltUnit.Name = "TxtAltUnit";
            this.TxtAltUnit.ReadOnly = true;
            this.TxtAltUnit.Size = new System.Drawing.Size(113, 26);
            this.TxtAltUnit.TabIndex = 353;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(735, 56);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(64, 20);
            this.label39.TabIndex = 352;
            this.label39.Text = "Alt Qty";
            // 
            // TxtAltQty
            // 
            this.TxtAltQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAltQty.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAltQty.Location = new System.Drawing.Point(799, 53);
            this.TxtAltQty.MaxLength = 255;
            this.TxtAltQty.Name = "TxtAltQty";
            this.TxtAltQty.Size = new System.Drawing.Size(113, 26);
            this.TxtAltQty.TabIndex = 351;
            this.TxtAltQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // BtnProduct
            // 
            this.BtnProduct.CausesValidation = false;
            this.BtnProduct.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnProduct.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnProduct.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnProduct.Location = new System.Drawing.Point(699, 53);
            this.BtnProduct.Name = "BtnProduct";
            this.BtnProduct.Size = new System.Drawing.Size(32, 26);
            this.BtnProduct.TabIndex = 348;
            this.BtnProduct.TabStop = false;
            this.BtnProduct.UseVisualStyleBackColor = true;
            this.BtnProduct.Click += new System.EventHandler(this.BtnProduct_Click);
            // 
            // TxtProduct
            // 
            this.TxtProduct.BackColor = System.Drawing.Color.White;
            this.TxtProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtProduct.ForeColor = System.Drawing.Color.Black;
            this.TxtProduct.Location = new System.Drawing.Point(93, 54);
            this.TxtProduct.Name = "TxtProduct";
            this.TxtProduct.ReadOnly = true;
            this.TxtProduct.Size = new System.Drawing.Size(600, 26);
            this.TxtProduct.TabIndex = 14;
            this.TxtProduct.Enter += new System.EventHandler(this.ListOfProduct_Enter);
            this.TxtProduct.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListOfProduct_KeyDown);
            // 
            // TxtRefVno
            // 
            this.TxtRefVno.BackColor = System.Drawing.Color.White;
            this.TxtRefVno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtRefVno.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtRefVno.ForeColor = System.Drawing.Color.Black;
            this.TxtRefVno.Location = new System.Drawing.Point(922, 15);
            this.TxtRefVno.MaxLength = 255;
            this.TxtRefVno.Name = "TxtRefVno";
            this.TxtRefVno.Size = new System.Drawing.Size(171, 26);
            this.TxtRefVno.TabIndex = 13;
            this.TxtRefVno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtRefVno_KeyDown);
            this.TxtRefVno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtRefVno_KeyPress);
            this.TxtRefVno.Leave += new System.EventHandler(this.TxtRefVno_Leave);
            // 
            // BtnRefVno
            // 
            this.BtnRefVno.CausesValidation = false;
            this.BtnRefVno.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRefVno.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnRefVno.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnRefVno.Location = new System.Drawing.Point(1095, 14);
            this.BtnRefVno.Name = "BtnRefVno";
            this.BtnRefVno.Size = new System.Drawing.Size(32, 28);
            this.BtnRefVno.TabIndex = 344;
            this.BtnRefVno.TabStop = false;
            this.BtnRefVno.UseVisualStyleBackColor = true;
            this.BtnRefVno.Click += new System.EventHandler(this.BtnRefVno_Click);
            // 
            // LblInvoiceNo
            // 
            this.LblInvoiceNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblInvoiceNo.AutoSize = true;
            this.LblInvoiceNo.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblInvoiceNo.Location = new System.Drawing.Point(832, 17);
            this.LblInvoiceNo.Name = "LblInvoiceNo";
            this.LblInvoiceNo.Size = new System.Drawing.Size(89, 20);
            this.LblInvoiceNo.TabIndex = 345;
            this.LblInvoiceNo.Text = "Invoice No";
            // 
            // TxtCounter
            // 
            this.TxtCounter.BackColor = System.Drawing.Color.White;
            this.TxtCounter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCounter.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCounter.ForeColor = System.Drawing.Color.Black;
            this.TxtCounter.Location = new System.Drawing.Point(683, 15);
            this.TxtCounter.MaxLength = 255;
            this.TxtCounter.Name = "TxtCounter";
            this.TxtCounter.Size = new System.Drawing.Size(149, 26);
            this.TxtCounter.TabIndex = 12;
            this.TxtCounter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalControl_KeyPress);
            this.TxtCounter.Leave += new System.EventHandler(this.TxtCounter_Leave);
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(3, 47);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(1341, 2);
            this.clsSeparator1.TabIndex = 305;
            this.clsSeparator1.TabStop = false;
            // 
            // MskDate
            // 
            this.MskDate.BackColor = System.Drawing.Color.White;
            this.MskDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskDate.ForeColor = System.Drawing.Color.Black;
            this.MskDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskDate.Location = new System.Drawing.Point(509, 15);
            this.MskDate.Mask = "00/00/0000";
            this.MskDate.Name = "MskDate";
            this.MskDate.Size = new System.Drawing.Size(123, 26);
            this.MskDate.TabIndex = 11;
            this.MskDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalControl_KeyPress);
            // 
            // MskMiti
            // 
            this.MskMiti.BackColor = System.Drawing.Color.White;
            this.MskMiti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskMiti.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskMiti.ForeColor = System.Drawing.Color.Black;
            this.MskMiti.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskMiti.Location = new System.Drawing.Point(340, 15);
            this.MskMiti.Mask = "00/00/0000";
            this.MskMiti.Name = "MskMiti";
            this.MskMiti.Size = new System.Drawing.Size(123, 26);
            this.MskMiti.TabIndex = 10;
            this.MskMiti.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalControl_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(300, 18);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(40, 20);
            this.label13.TabIndex = 203;
            this.label13.Text = "Miti";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(632, 18);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(51, 20);
            this.label12.TabIndex = 202;
            this.label12.Text = "Table";
            // 
            // TxtVNo
            // 
            this.TxtVNo.BackColor = System.Drawing.Color.White;
            this.TxtVNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtVNo.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtVNo.ForeColor = System.Drawing.Color.Black;
            this.TxtVNo.Location = new System.Drawing.Point(93, 15);
            this.TxtVNo.MaxLength = 255;
            this.TxtVNo.Name = "TxtVNo";
            this.TxtVNo.Size = new System.Drawing.Size(175, 26);
            this.TxtVNo.TabIndex = 9;
            this.TxtVNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtVNo_KeyDown);
            this.TxtVNo.Validating += new System.ComponentModel.CancelEventHandler(this.TxtVno_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(463, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 20);
            this.label1.TabIndex = 56;
            this.label1.Text = "Date";
            // 
            // TxtUnit
            // 
            this.TxtUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtUnit.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUnit.Location = new System.Drawing.Point(1182, 53);
            this.TxtUnit.MaxLength = 255;
            this.TxtUnit.Name = "TxtUnit";
            this.TxtUnit.ReadOnly = true;
            this.TxtUnit.Size = new System.Drawing.Size(106, 26);
            this.TxtUnit.TabIndex = 221;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(1025, 55);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(37, 20);
            this.label19.TabIndex = 208;
            this.label19.Text = "Qty";
            // 
            // BtnVno
            // 
            this.BtnVno.CausesValidation = false;
            this.BtnVno.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnVno.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnVno.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnVno.Location = new System.Drawing.Point(268, 14);
            this.BtnVno.Name = "BtnVno";
            this.BtnVno.Size = new System.Drawing.Size(32, 28);
            this.BtnVno.TabIndex = 51;
            this.BtnVno.TabStop = false;
            this.BtnVno.UseVisualStyleBackColor = true;
            this.BtnVno.Click += new System.EventHandler(this.BtnVno_Click);
            // 
            // TxtQty
            // 
            this.TxtQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtQty.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtQty.Location = new System.Drawing.Point(1067, 53);
            this.TxtQty.MaxLength = 255;
            this.TxtQty.Name = "TxtQty";
            this.TxtQty.Size = new System.Drawing.Size(113, 26);
            this.TxtQty.TabIndex = 15;
            this.TxtQty.Text = "1.00";
            this.TxtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtQty_KeyDown);
            this.TxtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtQty_KeyPress);
            this.TxtQty.Validating += new System.ComponentModel.CancelEventHandler(this.TxtQty_Validating);
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(4, 57);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(78, 20);
            this.label20.TabIndex = 209;
            this.label20.Text = "Item List";
            // 
            // LblVoucherCaption
            // 
            this.LblVoucherCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblVoucherCaption.AutoSize = true;
            this.LblVoucherCaption.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblVoucherCaption.Location = new System.Drawing.Point(4, 18);
            this.LblVoucherCaption.Name = "LblVoucherCaption";
            this.LblVoucherCaption.Size = new System.Drawing.Size(81, 20);
            this.LblVoucherCaption.TabIndex = 54;
            this.LblVoucherCaption.Text = "Order No";
            // 
            // FrmRSalesInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1347, 748);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MnuMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmRSalesInvoice";
            this.ShowIcon = false;
            this.Text = "RESTAURANT POS INVOICING";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmRSalesInvoice_Load);
            this.Shown += new System.EventHandler(this.FrmRSalesInvoice_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmRSalesInvoice_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmRSalesInvoice_KeyPress);
            this.PanelHeader.ResumeLayout(false);
            this.DataGridGroup.ResumeLayout(false);
            this.PnlInvoiceDetails.ResumeLayout(false);
            this.PnlInvoiceDetails.PerformLayout();
            this.PDetails.ResumeLayout(false);
            this.PDetails.PerformLayout();
            this.MemberGroup.ResumeLayout(false);
            this.MemberGroup.PerformLayout();
            this.InvoiceGroup.ResumeLayout(false);
            this.InvoiceGroup.PerformLayout();
            this.CustomerGroup.ResumeLayout(false);
            this.CustomerGroup.PerformLayout();
            this.CustomerDetails.ResumeLayout(false);
            this.CustomerDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).EndInit();
            this.PnlProductDetails.ResumeLayout(false);
            this.PnlProductDetails.PerformLayout();
            this.BillTotalGroup.ResumeLayout(false);
            this.BillTotalGroup.PerformLayout();
            this.BillInformatonGroup.ResumeLayout(false);
            this.BillInformatonGroup.PerformLayout();
            this.sidePanel1.ResumeLayout(false);
            this.sidePanel1.PerformLayout();
            this.MnuMenuStrip.ResumeLayout(false);
            this.MnuMenuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.DataGridView RGrid;
        private System.Windows.Forms.Button btnCustomer;
        private System.Windows.Forms.Label lbl_CBLedger;
        private System.Windows.Forms.Button BtnVno;
        private System.Windows.Forms.Label LblVoucherCaption;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox CustomerGroup;
        private System.Windows.Forms.ComboBox CmbPaymentType;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.GroupBox BillTotalGroup;
        private System.Windows.Forms.Label LblNumberInWords;
        private System.Windows.Forms.Label label111;
        private System.Windows.Forms.Label lbl_Remarks;
        private System.Windows.Forms.GroupBox InvoiceGroup;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label12;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox CustomerDetails;
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
        private System.Windows.Forms.Label LblMemberName;
        private System.Windows.Forms.Label LblMemberShortName;
        private System.Windows.Forms.Label LblMemberType;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label LblMemberAmount;
        private System.Windows.Forms.Label LblTag;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.GroupBox BillInformatonGroup;
        private System.Windows.Forms.GroupBox DataGridGroup;
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
        private MrTextBox TxtQty;
        private MrTextBox TxtCustomer;
        private MrTextBox TxtVNo;
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
        private MrMaskedTextBox MskMiti;
        private MrMaskedTextBox MskDate;
        private MrTextBox TxtProduct;
        private System.Windows.Forms.Button BtnProduct;
        private MrTextBox TxtVoucherAmount;
        private System.Windows.Forms.Label label35;
        private MrTextBox TxtServiceCharge;
        private System.Windows.Forms.Label label37;
        private MrTextBox TxtServiceChargeRate;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.MenuStrip MnuMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem MnuRestaurantMenu;
        private MrTextBox TxtAltUnit;
        private System.Windows.Forms.Label label39;
        private MrTextBox TxtAltQty;
        private System.Windows.Forms.ToolStripMenuItem MnuAddOrder;
        private System.Windows.Forms.ToolStripMenuItem MnuOrderCancel;
        private System.Windows.Forms.ToolStripMenuItem MnuOrderPrint;
        private System.Windows.Forms.ToolStripMenuItem MnuRePrintOrder;
        private System.Windows.Forms.ToolStripMenuItem MnuReturnInvoice;
        private System.Windows.Forms.ToolStripMenuItem MnuReverseInvoice;
        private System.Windows.Forms.ToolStripMenuItem MnuPrintInvoice;
        private System.Windows.Forms.ToolStripMenuItem MnuDayClosing;
        private System.Windows.Forms.ToolStripMenuItem MnuLockBilling;
        private System.Windows.Forms.Label label40;
        private MrTextBox TxtVatRate;
        private MrTextBox TxtVatAmount;
        private System.Windows.Forms.Label label41;
        private MrTextBox TxtTaxableAmount;
        private System.Windows.Forms.Label label42;
        private SidePanel sidePanel1;
        private System.Windows.Forms.ToolStripMenuItem MnuConfirmationPrint;
        private System.Windows.Forms.ToolStripMenuItem MnuEstimateInvoice;
        private MrPanel PDetails;
        private MrPanel PanelHeader;
        private MrPanel PnlProductDetails;
        private MrPanel PnlInvoiceDetails;
        private System.Windows.Forms.ToolStripMenuItem MnuNightAudit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MnuTableTransfer;
        private System.Windows.Forms.ToolStripMenuItem MnuTableSplit;
    }
}