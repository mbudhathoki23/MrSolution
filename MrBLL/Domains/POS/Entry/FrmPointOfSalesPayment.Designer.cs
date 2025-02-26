
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.POS.Entry
{
    partial class FrmPointOfSalesPayment
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
            this.pnlBottom = new MrPanel();
            this.chkPrintInvoice = new System.Windows.Forms.CheckBox();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnAccept = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
            this.tabCash = new DevExpress.XtraTab.XtraTabPage();
            this.TxtTenderAmount = new MrTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtChangeAmount = new MrTextBox();
            this.tabCard = new DevExpress.XtraTab.XtraTabPage();
            this.glkupCardLedgers = new DevExpress.XtraEditors.GridLookUpEdit();
            this.bsCardLedgers = new System.Windows.Forms.BindingSource(this.components);
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tabCredit = new DevExpress.XtraTab.XtraTabPage();
            this.glkupCreditLedgers = new DevExpress.XtraEditors.GridLookUpEdit();
            this.bsCreditLedgers = new System.Windows.Forms.BindingSource(this.components);
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtLedgerBalance = new MrTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtGrandTotal = new MrTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.roundPanel1 = new RoundPanel();
            this.chkCustomerManual = new System.Windows.Forms.CheckBox();
            this.glkupPartyLedgers = new DevExpress.XtraEditors.GridLookUpEdit();
            this.bsPartyLedgers = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtRemarks = new MrTextBox();
            this.TxtPhoneNo = new MrTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtPanNo = new MrTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtAddress = new MrTextBox();
            this.TxtInvoiceTo = new MrTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
            this.xtraTabControl.SuspendLayout();
            this.tabCash.SuspendLayout();
            this.tabCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkupCardLedgers.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCardLedgers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.tabCredit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkupCreditLedgers.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCreditLedgers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.roundPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkupPartyLedgers.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPartyLedgers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.pnlBottom.Controls.Add(this.chkPrintInvoice);
            this.pnlBottom.Controls.Add(this.BtnCancel);
            this.pnlBottom.Controls.Add(this.BtnAccept);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBottom.Location = new System.Drawing.Point(0, 472);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(665, 58);
            this.pnlBottom.TabIndex = 0;
            // 
            // chkPrintInvoice
            // 
            this.chkPrintInvoice.AutoSize = true;
            this.chkPrintInvoice.Checked = true;
            this.chkPrintInvoice.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPrintInvoice.Font = new System.Drawing.Font("Bookman Old Style", 14F);
            this.chkPrintInvoice.Location = new System.Drawing.Point(6, 17);
            this.chkPrintInvoice.Name = "chkPrintInvoice";
            this.chkPrintInvoice.Size = new System.Drawing.Size(149, 25);
            this.chkPrintInvoice.TabIndex = 2;
            this.chkPrintInvoice.Text = "Print Invoice";
            this.chkPrintInvoice.UseVisualStyleBackColor = true;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 14F);
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.BtnCancel.Location = new System.Drawing.Point(527, 10);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(131, 39);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnAccept
            // 
            this.BtnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAccept.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 14F);
            this.BtnAccept.Appearance.Options.UseFont = true;
            this.BtnAccept.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.BtnAccept.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.BtnAccept.Location = new System.Drawing.Point(414, 10);
            this.BtnAccept.Name = "BtnAccept";
            this.BtnAccept.Size = new System.Drawing.Size(107, 39);
            this.BtnAccept.TabIndex = 0;
            this.BtnAccept.Text = "&ACCEPT";
            this.BtnAccept.Click += new System.EventHandler(this.BtnAccept_Click);
            // 
            // xtraTabControl
            // 
            this.xtraTabControl.AppearancePage.Header.Font = new System.Drawing.Font("Tahoma", 14F);
            this.xtraTabControl.AppearancePage.Header.Options.UseFont = true;
            this.xtraTabControl.Location = new System.Drawing.Point(115, 260);
            this.xtraTabControl.Name = "xtraTabControl";
            this.xtraTabControl.SelectedTabPage = this.tabCash;
            this.xtraTabControl.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True;
            this.xtraTabControl.Size = new System.Drawing.Size(540, 121);
            this.xtraTabControl.TabIndex = 7;
            this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabCash,
            this.tabCard,
            this.tabCredit});
            this.xtraTabControl.TabPageWidth = 65;
            this.xtraTabControl.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl_SelectedPageChanged);
            // 
            // tabCash
            // 
            this.tabCash.Controls.Add(this.TxtTenderAmount);
            this.tabCash.Controls.Add(this.label6);
            this.tabCash.Controls.Add(this.label7);
            this.tabCash.Controls.Add(this.TxtChangeAmount);
            this.tabCash.Name = "tabCash";
            this.tabCash.Size = new System.Drawing.Size(538, 86);
            this.tabCash.Text = "CASH";
            // 
            // TxtTenderAmount
            // 
            this.TxtTenderAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTenderAmount.Font = new System.Drawing.Font("Bookman Old Style", 18F);
            this.TxtTenderAmount.Location = new System.Drawing.Point(120, 6);
            this.TxtTenderAmount.Margin = new System.Windows.Forms.Padding(2);
            this.TxtTenderAmount.Name = "TxtTenderAmount";
            this.TxtTenderAmount.Size = new System.Drawing.Size(365, 36);
            this.TxtTenderAmount.TabIndex = 0;
            this.TxtTenderAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtTenderAmount.TextChanged += new System.EventHandler(this.TxtTenderAmount_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label6.Location = new System.Drawing.Point(12, 17);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 20);
            this.label6.TabIndex = 57;
            this.label6.Text = "Tender ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label7.Location = new System.Drawing.Point(14, 57);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 20);
            this.label7.TabIndex = 58;
            this.label7.Text = "Change";
            // 
            // TxtChangeAmount
            // 
            this.TxtChangeAmount.BackColor = System.Drawing.Color.White;
            this.TxtChangeAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtChangeAmount.Font = new System.Drawing.Font("Bookman Old Style", 18F);
            this.TxtChangeAmount.ForeColor = System.Drawing.Color.Navy;
            this.TxtChangeAmount.Location = new System.Drawing.Point(120, 46);
            this.TxtChangeAmount.Margin = new System.Windows.Forms.Padding(2);
            this.TxtChangeAmount.Name = "TxtChangeAmount";
            this.TxtChangeAmount.ReadOnly = true;
            this.TxtChangeAmount.Size = new System.Drawing.Size(365, 36);
            this.TxtChangeAmount.TabIndex = 1;
            this.TxtChangeAmount.Text = "0.00";
            this.TxtChangeAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tabCard
            // 
            this.tabCard.Controls.Add(this.glkupCardLedgers);
            this.tabCard.Name = "tabCard";
            this.tabCard.Size = new System.Drawing.Size(538, 86);
            this.tabCard.Text = "CARD";
            // 
            // glkupCardLedgers
            // 
            this.glkupCardLedgers.EditValue = "";
            this.glkupCardLedgers.Location = new System.Drawing.Point(58, 20);
            this.glkupCardLedgers.Name = "glkupCardLedgers";
            this.glkupCardLedgers.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 20F);
            this.glkupCardLedgers.Properties.Appearance.Options.UseFont = true;
            this.glkupCardLedgers.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkupCardLedgers.Properties.DataSource = this.bsCardLedgers;
            this.glkupCardLedgers.Properties.DisplayMember = "Particular";
            this.glkupCardLedgers.Properties.NullText = "";
            this.glkupCardLedgers.Properties.PopupView = this.gridView2;
            this.glkupCardLedgers.Properties.ValueMember = "LedgerId";
            this.glkupCardLedgers.Size = new System.Drawing.Size(414, 38);
            this.glkupCardLedgers.TabIndex = 337;
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn17,
            this.gridColumn18,
            this.gridColumn19});
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "PARTICULAR";
            this.gridColumn13.FieldName = "Particular";
            this.gridColumn13.MinWidth = 200;
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 0;
            this.gridColumn13.Width = 200;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "LEDGER TYPE";
            this.gridColumn14.FieldName = "GLType";
            this.gridColumn14.MinWidth = 120;
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 4;
            this.gridColumn14.Width = 120;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "ADDRESS";
            this.gridColumn15.FieldName = "GLAddress";
            this.gridColumn15.MinWidth = 150;
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 5;
            this.gridColumn15.Width = 150;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "PHONE NO.";
            this.gridColumn16.FieldName = "PhoneNo";
            this.gridColumn16.MinWidth = 100;
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 6;
            this.gridColumn16.Width = 100;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "PAN NO.";
            this.gridColumn17.FieldName = "PanNo";
            this.gridColumn17.MinWidth = 100;
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 3;
            this.gridColumn17.Width = 100;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "LEDGER CODE";
            this.gridColumn18.FieldName = "LedgerCode";
            this.gridColumn18.MinWidth = 80;
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 1;
            this.gridColumn18.Width = 80;
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "BALANCE";
            this.gridColumn19.DisplayFormat.FormatString = "N2";
            this.gridColumn19.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn19.FieldName = "Balance";
            this.gridColumn19.MinWidth = 80;
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 2;
            this.gridColumn19.Width = 80;
            // 
            // tabCredit
            // 
            this.tabCredit.Controls.Add(this.glkupCreditLedgers);
            this.tabCredit.Controls.Add(this.label1);
            this.tabCredit.Controls.Add(this.TxtLedgerBalance);
            this.tabCredit.Controls.Add(this.label9);
            this.tabCredit.Name = "tabCredit";
            this.tabCredit.Size = new System.Drawing.Size(538, 86);
            this.tabCredit.Text = "CREDIT";
            // 
            // glkupCreditLedgers
            // 
            this.glkupCreditLedgers.EditValue = "";
            this.glkupCreditLedgers.Location = new System.Drawing.Point(85, 7);
            this.glkupCreditLedgers.Name = "glkupCreditLedgers";
            this.glkupCreditLedgers.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 20F);
            this.glkupCreditLedgers.Properties.Appearance.Options.UseFont = true;
            this.glkupCreditLedgers.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkupCreditLedgers.Properties.DataSource = this.bsCreditLedgers;
            this.glkupCreditLedgers.Properties.DisplayMember = "Particular";
            this.glkupCreditLedgers.Properties.NullText = "";
            this.glkupCreditLedgers.Properties.PopupView = this.gridLookUpEdit1View;
            this.glkupCreditLedgers.Properties.ValueMember = "LedgerId";
            this.glkupCreditLedgers.Size = new System.Drawing.Size(389, 38);
            this.glkupCreditLedgers.TabIndex = 336;
            this.glkupCreditLedgers.EditValueChanged += new System.EventHandler(this.glkupGeneralLedgers_EditValueChanged);
            this.glkupCreditLedgers.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.glkupGeneralLedgers_KeyPress);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn6,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn9,
            this.gridColumn7,
            this.gridColumn8});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "PARTICULAR";
            this.gridColumn6.FieldName = "Particular";
            this.gridColumn6.MinWidth = 200;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 0;
            this.gridColumn6.Width = 200;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "LEDGER TYPE";
            this.gridColumn10.FieldName = "GLType";
            this.gridColumn10.MinWidth = 120;
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 4;
            this.gridColumn10.Width = 120;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "ADDRESS";
            this.gridColumn11.FieldName = "GLAddress";
            this.gridColumn11.MinWidth = 150;
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 5;
            this.gridColumn11.Width = 150;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "PHONE NO.";
            this.gridColumn12.FieldName = "PhoneNo";
            this.gridColumn12.MinWidth = 100;
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 6;
            this.gridColumn12.Width = 100;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "PAN NO.";
            this.gridColumn9.FieldName = "PanNo";
            this.gridColumn9.MinWidth = 100;
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 3;
            this.gridColumn9.Width = 100;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "LEDGER CODE";
            this.gridColumn7.FieldName = "LedgerCode";
            this.gridColumn7.MinWidth = 80;
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 1;
            this.gridColumn7.Width = 80;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "BALANCE";
            this.gridColumn8.DisplayFormat.FormatString = "N2";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn8.FieldName = "Balance";
            this.gridColumn8.MinWidth = 80;
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 2;
            this.gridColumn8.Width = 80;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label1.Location = new System.Drawing.Point(18, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ledger";
            // 
            // TxtLedgerBalance
            // 
            this.TxtLedgerBalance.BackColor = System.Drawing.Color.White;
            this.TxtLedgerBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtLedgerBalance.Enabled = false;
            this.TxtLedgerBalance.Font = new System.Drawing.Font("Bookman Old Style", 18F);
            this.TxtLedgerBalance.Location = new System.Drawing.Point(262, 50);
            this.TxtLedgerBalance.Margin = new System.Windows.Forms.Padding(2);
            this.TxtLedgerBalance.Name = "TxtLedgerBalance";
            this.TxtLedgerBalance.Size = new System.Drawing.Size(212, 36);
            this.TxtLedgerBalance.TabIndex = 300;
            this.TxtLedgerBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Bookman Old Style", 20F);
            this.label9.Location = new System.Drawing.Point(137, 52);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(121, 32);
            this.label9.TabIndex = 301;
            this.label9.Text = "Balance";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label5.Location = new System.Drawing.Point(6, 232);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 20);
            this.label5.TabIndex = 56;
            this.label5.Text = "Bill Amount";
            // 
            // TxtGrandTotal
            // 
            this.TxtGrandTotal.BackColor = System.Drawing.Color.White;
            this.TxtGrandTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtGrandTotal.Enabled = false;
            this.TxtGrandTotal.Font = new System.Drawing.Font("Bookman Old Style", 18F);
            this.TxtGrandTotal.ForeColor = System.Drawing.Color.Navy;
            this.TxtGrandTotal.Location = new System.Drawing.Point(114, 223);
            this.TxtGrandTotal.Margin = new System.Windows.Forms.Padding(2);
            this.TxtGrandTotal.Name = "TxtGrandTotal";
            this.TxtGrandTotal.ReadOnly = true;
            this.TxtGrandTotal.Size = new System.Drawing.Size(208, 36);
            this.TxtGrandTotal.TabIndex = 6;
            this.TxtGrandTotal.Text = "0.00";
            this.TxtGrandTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.splitContainer1.Panel1.Controls.Add(this.roundPanel1);
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(665, 472);
            this.splitContainer1.SplitterDistance = 572;
            this.splitContainer1.TabIndex = 0;
            // 
            // roundPanel1
            // 
            this.roundPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.roundPanel1.Controls.Add(this.label5);
            this.roundPanel1.Controls.Add(this.chkCustomerManual);
            this.roundPanel1.Controls.Add(this.TxtGrandTotal);
            this.roundPanel1.Controls.Add(this.glkupPartyLedgers);
            this.roundPanel1.Controls.Add(this.xtraTabControl);
            this.roundPanel1.Controls.Add(this.label12);
            this.roundPanel1.Controls.Add(this.label10);
            this.roundPanel1.Controls.Add(this.txtRemarks);
            this.roundPanel1.Controls.Add(this.TxtPhoneNo);
            this.roundPanel1.Controls.Add(this.label2);
            this.roundPanel1.Controls.Add(this.TxtPanNo);
            this.roundPanel1.Controls.Add(this.label11);
            this.roundPanel1.Controls.Add(this.label3);
            this.roundPanel1.Controls.Add(this.TxtAddress);
            this.roundPanel1.Controls.Add(this.TxtInvoiceTo);
            this.roundPanel1.Controls.Add(this.label4);
            this.roundPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roundPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.roundPanel1.Location = new System.Drawing.Point(0, 0);
            this.roundPanel1.Name = "roundPanel1";
            this.roundPanel1.Radious = 25;
            this.roundPanel1.Size = new System.Drawing.Size(665, 472);
            this.roundPanel1.TabIndex = 1;
            this.roundPanel1.TabStop = false;
            this.roundPanel1.Text = "CUSTOMER DETAILS";
            this.roundPanel1.TitleBackColor = System.Drawing.Color.DarkSlateBlue;
            this.roundPanel1.TitleFont = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.roundPanel1.TitleForeColor = System.Drawing.Color.White;
            this.roundPanel1.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
            // 
            // chkCustomerManual
            // 
            this.chkCustomerManual.AutoSize = true;
            this.chkCustomerManual.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCustomerManual.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.chkCustomerManual.Location = new System.Drawing.Point(6, 34);
            this.chkCustomerManual.Name = "chkCustomerManual";
            this.chkCustomerManual.Size = new System.Drawing.Size(151, 24);
            this.chkCustomerManual.TabIndex = 1;
            this.chkCustomerManual.Text = "Enter Manually";
            this.chkCustomerManual.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCustomerManual.UseVisualStyleBackColor = true;
            this.chkCustomerManual.CheckedChanged += new System.EventHandler(this.chkCustomerManual_CheckedChanged);
            // 
            // glkupPartyLedgers
            // 
            this.glkupPartyLedgers.EditValue = "";
            this.glkupPartyLedgers.Location = new System.Drawing.Point(114, 62);
            this.glkupPartyLedgers.Name = "glkupPartyLedgers";
            this.glkupPartyLedgers.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 20F);
            this.glkupPartyLedgers.Properties.Appearance.Options.UseFont = true;
            this.glkupPartyLedgers.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkupPartyLedgers.Properties.DataSource = this.bsPartyLedgers;
            this.glkupPartyLedgers.Properties.NullText = "";
            this.glkupPartyLedgers.Properties.PopupView = this.gridView1;
            this.glkupPartyLedgers.Size = new System.Drawing.Size(539, 38);
            this.glkupPartyLedgers.TabIndex = 0;
            this.glkupPartyLedgers.EditValueChanged += new System.EventHandler(this.glkupPartyLedgers_EditValueChanged);
            this.glkupPartyLedgers.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.glkupPartyLedgers_KeyPress);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn5,
            this.gridColumn4});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "PARTY NAME";
            this.gridColumn1.FieldName = "PartyName";
            this.gridColumn1.MinWidth = 200;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 200;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "ADDRESS";
            this.gridColumn2.FieldName = "Address";
            this.gridColumn2.MinWidth = 150;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 150;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "MOBILE NO.";
            this.gridColumn3.FieldName = "MobileNo";
            this.gridColumn3.MinWidth = 120;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 120;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "VAT NO.";
            this.gridColumn5.FieldName = "VatNo";
            this.gridColumn5.MinWidth = 100;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 100;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "CONTACT PERSON";
            this.gridColumn4.FieldName = "ContactPerson";
            this.gridColumn4.MinWidth = 150;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 150;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label12.Location = new System.Drawing.Point(6, 391);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(79, 20);
            this.label12.TabIndex = 305;
            this.label12.Text = "Remakrs";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label10.Location = new System.Drawing.Point(340, 190);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 20);
            this.label10.TabIndex = 305;
            this.label10.Text = "Phone No";
            // 
            // txtRemarks
            // 
            this.txtRemarks.BackColor = System.Drawing.Color.White;
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.Location = new System.Drawing.Point(114, 384);
            this.txtRemarks.Margin = new System.Windows.Forms.Padding(2);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemarks.Size = new System.Drawing.Size(538, 85);
            this.txtRemarks.TabIndex = 8;
            this.txtRemarks.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtPhoneNo
            // 
            this.TxtPhoneNo.BackColor = System.Drawing.Color.White;
            this.TxtPhoneNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPhoneNo.Font = new System.Drawing.Font("Bookman Old Style", 18F);
            this.TxtPhoneNo.Location = new System.Drawing.Point(448, 183);
            this.TxtPhoneNo.Margin = new System.Windows.Forms.Padding(2);
            this.TxtPhoneNo.Name = "TxtPhoneNo";
            this.TxtPhoneNo.Size = new System.Drawing.Size(207, 36);
            this.TxtPhoneNo.TabIndex = 5;
            this.TxtPhoneNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label2.Location = new System.Drawing.Point(6, 71);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 20);
            this.label2.TabIndex = 55;
            this.label2.Text = "Invoice To";
            // 
            // TxtPanNo
            // 
            this.TxtPanNo.BackColor = System.Drawing.Color.White;
            this.TxtPanNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPanNo.Font = new System.Drawing.Font("Bookman Old Style", 18F);
            this.TxtPanNo.Location = new System.Drawing.Point(115, 183);
            this.TxtPanNo.Margin = new System.Windows.Forms.Padding(2);
            this.TxtPanNo.Name = "TxtPanNo";
            this.TxtPanNo.Size = new System.Drawing.Size(207, 36);
            this.TxtPanNo.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label11.Location = new System.Drawing.Point(7, 110);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 20);
            this.label11.TabIndex = 53;
            this.label11.Text = "Customer";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label3.Location = new System.Drawing.Point(7, 150);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 20);
            this.label3.TabIndex = 53;
            this.label3.Text = "Address";
            // 
            // TxtAddress
            // 
            this.TxtAddress.BackColor = System.Drawing.Color.White;
            this.TxtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAddress.Font = new System.Drawing.Font("Bookman Old Style", 18F);
            this.TxtAddress.Location = new System.Drawing.Point(115, 143);
            this.TxtAddress.Margin = new System.Windows.Forms.Padding(2);
            this.TxtAddress.Name = "TxtAddress";
            this.TxtAddress.Size = new System.Drawing.Size(540, 36);
            this.TxtAddress.TabIndex = 3;
            // 
            // TxtInvoiceTo
            // 
            this.TxtInvoiceTo.BackColor = System.Drawing.Color.White;
            this.TxtInvoiceTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtInvoiceTo.Font = new System.Drawing.Font("Bookman Old Style", 18F);
            this.TxtInvoiceTo.Location = new System.Drawing.Point(115, 103);
            this.TxtInvoiceTo.Margin = new System.Windows.Forms.Padding(2);
            this.TxtInvoiceTo.Name = "TxtInvoiceTo";
            this.TxtInvoiceTo.Size = new System.Drawing.Size(540, 36);
            this.TxtInvoiceTo.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label4.Location = new System.Drawing.Point(7, 190);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 20);
            this.label4.TabIndex = 54;
            this.label4.Text = "Pan No";
            // 
            // FrmPointOfSalesPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(665, 530);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.pnlBottom);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPointOfSalesPayment";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Invoice Payment";
            this.Load += new System.EventHandler(this.FrmPointOfSalesPayment_Load);
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
            this.xtraTabControl.ResumeLayout(false);
            this.tabCash.ResumeLayout(false);
            this.tabCash.PerformLayout();
            this.tabCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.glkupCardLedgers.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCardLedgers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.tabCredit.ResumeLayout(false);
            this.tabCredit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkupCreditLedgers.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCreditLedgers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.roundPanel1.ResumeLayout(false);
            this.roundPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkupPartyLedgers.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPartyLedgers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraTab.XtraTabControl xtraTabControl;
        private DevExpress.XtraTab.XtraTabPage tabCash;
        private DevExpress.XtraTab.XtraTabPage tabCard;
        private DevExpress.XtraTab.XtraTabPage tabCredit;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private RoundPanel roundPanel1;
        private MrTextBox TxtGrandTotal;
        private System.Windows.Forms.Label label10;
        private MrTextBox TxtPhoneNo;
        private System.Windows.Forms.Label label9;
        private MrTextBox TxtLedgerBalance;
        public DevExpress.XtraEditors.SimpleButton BtnAccept;
        public MrTextBox TxtChangeAmount;
        public DevExpress.XtraEditors.SimpleButton BtnCancel;
        private MrTextBox TxtTenderAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private MrTextBox TxtPanNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private MrTextBox TxtAddress;
        private MrTextBox TxtInvoiceTo;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.GridLookUpEdit glkupCreditLedgers;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private System.Windows.Forms.BindingSource bsPartyLedgers;
        private System.Windows.Forms.BindingSource bsCreditLedgers;
        private DevExpress.XtraEditors.GridLookUpEdit glkupPartyLedgers;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.CheckBox chkPrintInvoice;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private System.Windows.Forms.CheckBox chkCustomerManual;
        private System.Windows.Forms.Label label11;
        private DevExpress.XtraEditors.GridLookUpEdit glkupCardLedgers;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private System.Windows.Forms.BindingSource bsCardLedgers;
        private System.Windows.Forms.Label label12;
        private MrTextBox txtRemarks;
        private MrPanel pnlBottom;
    }
}