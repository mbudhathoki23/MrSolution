using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.DataEntry.Common
{
    partial class FrmBillingTerm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbl_TotProTermAmt = new System.Windows.Forms.Label();
            this.lbl_BasicTotProAmt = new System.Windows.Forms.Label();
            this.PDGrid = new System.Windows.Forms.DataGridView();
            this.dgv_SNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_TermId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Basis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Sign = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Formula = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_TaxationType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_PGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lblBillAmount = new System.Windows.Forms.Label();
            this.lbl_Procap = new System.Windows.Forms.Label();
            this.chk_ProTerm = new System.Windows.Forms.CheckBox();
            this.lbl_BasicAmt = new System.Windows.Forms.Label();
            this.lbl_Total = new System.Windows.Forms.Label();
            this.lbl_BasicAmt1 = new System.Windows.Forms.Label();
            this.lbl_Total1 = new System.Windows.Forms.Label();
            this.BDGrid = new System.Windows.Forms.DataGridView();
            this.g_SNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.g_TermId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.g_Desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.g_Basis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.g_Sign = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.g_Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.g_Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.g_Formula = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.g_TaxationType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gv_PGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gv_OrderNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PanelHeader = new MrPanel();
            this.PnlBillingTerm2 = new MrPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.clsSeparator5 = new ClsSeparator();
            this.clsSeparator2 = new ClsSeparator();
            this.PnlBillingTerm1 = new MrPanel();
            this.clsSeparator4 = new ClsSeparator();
            this.clsSeparator1 = new ClsSeparator();
            this.TxtTermRate = new MrTextBox();
            this.TxtTermAmount = new MrTextBox();
            this.TxtTermSign = new MrTextBox();
            this.TxtTermDescription = new MrTextBox();
            this.TxtBasicAmount = new MrTextBox();
            this.btn_Ok = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.clsSeparator3 = new ClsSeparator();
            this.panel1 = new MrPanel();
            ((System.ComponentModel.ISupportInitialize)(this.PDGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BDGrid)).BeginInit();
            this.PanelHeader.SuspendLayout();
            this.PnlBillingTerm2.SuspendLayout();
            this.PnlBillingTerm1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_TotProTermAmt
            // 
            this.lbl_TotProTermAmt.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lbl_TotProTermAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_TotProTermAmt.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TotProTermAmt.Location = new System.Drawing.Point(443, 187);
            this.lbl_TotProTermAmt.Name = "lbl_TotProTermAmt";
            this.lbl_TotProTermAmt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_TotProTermAmt.Size = new System.Drawing.Size(136, 25);
            this.lbl_TotProTermAmt.TabIndex = 97;
            this.lbl_TotProTermAmt.Text = "0.00";
            this.lbl_TotProTermAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_BasicTotProAmt
            // 
            this.lbl_BasicTotProAmt.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lbl_BasicTotProAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_BasicTotProAmt.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_BasicTotProAmt.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lbl_BasicTotProAmt.Location = new System.Drawing.Point(443, 4);
            this.lbl_BasicTotProAmt.Name = "lbl_BasicTotProAmt";
            this.lbl_BasicTotProAmt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_BasicTotProAmt.Size = new System.Drawing.Size(136, 25);
            this.lbl_BasicTotProAmt.TabIndex = 96;
            this.lbl_BasicTotProAmt.Text = "0.00";
            this.lbl_BasicTotProAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PDGrid
            // 
            this.PDGrid.AllowUserToAddRows = false;
            this.PDGrid.AllowUserToDeleteRows = false;
            this.PDGrid.AllowUserToResizeColumns = false;
            this.PDGrid.AllowUserToResizeRows = false;
            this.PDGrid.ColumnHeadersHeight = 30;
            this.PDGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.PDGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_SNo,
            this.dgv_TermId,
            this.dgv_Desc,
            this.dgv_Basis,
            this.dgv_Sign,
            this.dgv_Rate,
            this.dgv_Amount,
            this.dgv_Formula,
            this.dgv_TaxationType,
            this.dgv_PGroup});
            this.PDGrid.Location = new System.Drawing.Point(1, 33);
            this.PDGrid.Margin = new System.Windows.Forms.Padding(2);
            this.PDGrid.MultiSelect = false;
            this.PDGrid.Name = "PDGrid";
            this.PDGrid.ReadOnly = true;
            this.PDGrid.RowHeadersWidth = 15;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.PDGrid.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.PDGrid.RowTemplate.Height = 24;
            this.PDGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PDGrid.Size = new System.Drawing.Size(575, 152);
            this.PDGrid.TabIndex = 0;
            // 
            // dgv_SNo
            // 
            this.dgv_SNo.HeaderText = "SNo";
            this.dgv_SNo.Name = "dgv_SNo";
            this.dgv_SNo.ReadOnly = true;
            this.dgv_SNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgv_SNo.Width = 50;
            // 
            // dgv_TermId
            // 
            this.dgv_TermId.HeaderText = "Term Id";
            this.dgv_TermId.Name = "dgv_TermId";
            this.dgv_TermId.ReadOnly = true;
            this.dgv_TermId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgv_TermId.Visible = false;
            // 
            // dgv_Desc
            // 
            this.dgv_Desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgv_Desc.FillWeight = 130F;
            this.dgv_Desc.HeaderText = "Description";
            this.dgv_Desc.MaxInputLength = 256;
            this.dgv_Desc.Name = "dgv_Desc";
            this.dgv_Desc.ReadOnly = true;
            this.dgv_Desc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgv_Basis
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_Basis.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_Basis.HeaderText = "Basis";
            this.dgv_Basis.MaxInputLength = 255;
            this.dgv_Basis.Name = "dgv_Basis";
            this.dgv_Basis.ReadOnly = true;
            this.dgv_Basis.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgv_Basis.Width = 55;
            // 
            // dgv_Sign
            // 
            this.dgv_Sign.HeaderText = "Sign";
            this.dgv_Sign.Name = "dgv_Sign";
            this.dgv_Sign.ReadOnly = true;
            this.dgv_Sign.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgv_Sign.Width = 50;
            // 
            // dgv_Rate
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.dgv_Rate.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_Rate.HeaderText = "Rate";
            this.dgv_Rate.Name = "dgv_Rate";
            this.dgv_Rate.ReadOnly = true;
            this.dgv_Rate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgv_Rate.Width = 50;
            // 
            // dgv_Amount
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "0.00";
            this.dgv_Amount.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_Amount.HeaderText = "Amount";
            this.dgv_Amount.MaxInputLength = 18;
            this.dgv_Amount.Name = "dgv_Amount";
            this.dgv_Amount.ReadOnly = true;
            this.dgv_Amount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgv_Formula
            // 
            this.dgv_Formula.HeaderText = "Formula";
            this.dgv_Formula.Name = "dgv_Formula";
            this.dgv_Formula.ReadOnly = true;
            this.dgv_Formula.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgv_Formula.Visible = false;
            // 
            // dgv_TaxationType
            // 
            this.dgv_TaxationType.HeaderText = "TaxationType";
            this.dgv_TaxationType.Name = "dgv_TaxationType";
            this.dgv_TaxationType.ReadOnly = true;
            this.dgv_TaxationType.Visible = false;
            // 
            // dgv_PGroup
            // 
            this.dgv_PGroup.HeaderText = "PGroup";
            this.dgv_PGroup.Name = "dgv_PGroup";
            this.dgv_PGroup.ReadOnly = true;
            this.dgv_PGroup.Visible = false;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.Location = new System.Drawing.Point(324, 190);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(111, 19);
            this.lblTotalAmount.TabIndex = 56;
            this.lblTotalAmount.Text = "Term Amount";
            // 
            // lblBillAmount
            // 
            this.lblBillAmount.AutoSize = true;
            this.lblBillAmount.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillAmount.Location = new System.Drawing.Point(324, 7);
            this.lblBillAmount.Name = "lblBillAmount";
            this.lblBillAmount.Size = new System.Drawing.Size(98, 19);
            this.lblBillAmount.TabIndex = 52;
            this.lblBillAmount.Text = "Bill Amount";
            // 
            // lbl_Procap
            // 
            this.lbl_Procap.AutoSize = true;
            this.lbl_Procap.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Procap.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lbl_Procap.Location = new System.Drawing.Point(4, 6);
            this.lbl_Procap.Name = "lbl_Procap";
            this.lbl_Procap.Size = new System.Drawing.Size(157, 20);
            this.lbl_Procap.TabIndex = 62;
            this.lbl_Procap.Text = "Product Wise Term";
            this.lbl_Procap.Visible = false;
            // 
            // chk_ProTerm
            // 
            this.chk_ProTerm.AutoSize = true;
            this.chk_ProTerm.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_ProTerm.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.chk_ProTerm.Location = new System.Drawing.Point(100, 190);
            this.chk_ProTerm.Name = "chk_ProTerm";
            this.chk_ProTerm.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_ProTerm.Size = new System.Drawing.Size(215, 23);
            this.chk_ProTerm.TabIndex = 113;
            this.chk_ProTerm.Text = "Show Product Wise Term";
            this.chk_ProTerm.UseVisualStyleBackColor = true;
            this.chk_ProTerm.CheckedChanged += new System.EventHandler(this.chk_ProTerm_Click);
            // 
            // lbl_BasicAmt
            // 
            this.lbl_BasicAmt.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lbl_BasicAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_BasicAmt.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_BasicAmt.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lbl_BasicAmt.Location = new System.Drawing.Point(443, 4);
            this.lbl_BasicAmt.Name = "lbl_BasicAmt";
            this.lbl_BasicAmt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_BasicAmt.Size = new System.Drawing.Size(136, 25);
            this.lbl_BasicAmt.TabIndex = 110;
            this.lbl_BasicAmt.Text = "0.00";
            this.lbl_BasicAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_Total
            // 
            this.lbl_Total.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lbl_Total.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Total.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Total.Location = new System.Drawing.Point(443, 189);
            this.lbl_Total.Name = "lbl_Total";
            this.lbl_Total.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_Total.Size = new System.Drawing.Size(136, 25);
            this.lbl_Total.TabIndex = 109;
            this.lbl_Total.Text = "0.00";
            this.lbl_Total.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_BasicAmt1
            // 
            this.lbl_BasicAmt1.AutoSize = true;
            this.lbl_BasicAmt1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_BasicAmt1.Location = new System.Drawing.Point(324, 7);
            this.lbl_BasicAmt1.Name = "lbl_BasicAmt1";
            this.lbl_BasicAmt1.Size = new System.Drawing.Size(98, 19);
            this.lbl_BasicAmt1.TabIndex = 102;
            this.lbl_BasicAmt1.Text = "Bill Amount";
            // 
            // lbl_Total1
            // 
            this.lbl_Total1.AutoSize = true;
            this.lbl_Total1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Total1.Location = new System.Drawing.Point(324, 192);
            this.lbl_Total1.Name = "lbl_Total1";
            this.lbl_Total1.Size = new System.Drawing.Size(111, 19);
            this.lbl_Total1.TabIndex = 101;
            this.lbl_Total1.Text = "Term Amount";
            this.lbl_Total1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BDGrid
            // 
            this.BDGrid.AllowUserToAddRows = false;
            this.BDGrid.AllowUserToDeleteRows = false;
            this.BDGrid.AllowUserToResizeColumns = false;
            this.BDGrid.AllowUserToResizeRows = false;
            this.BDGrid.ColumnHeadersHeight = 30;
            this.BDGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.BDGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.g_SNo,
            this.g_TermId,
            this.g_Desc,
            this.g_Basis,
            this.g_Sign,
            this.g_Rate,
            this.g_Amount,
            this.g_Formula,
            this.g_TaxationType,
            this.gv_PGroup,
            this.gv_OrderNo});
            this.BDGrid.Location = new System.Drawing.Point(1, 35);
            this.BDGrid.Margin = new System.Windows.Forms.Padding(2);
            this.BDGrid.MultiSelect = false;
            this.BDGrid.Name = "BDGrid";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.BDGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.BDGrid.RowHeadersWidth = 15;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.BDGrid.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.BDGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BDGrid.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.BDGrid.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.BDGrid.RowTemplate.Height = 24;
            this.BDGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.BDGrid.Size = new System.Drawing.Size(575, 152);
            this.BDGrid.TabIndex = 1;
            this.BDGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellEnter);
            this.BDGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_RowEnter);
            this.BDGrid.Enter += new System.EventHandler(this.Grid_Enter);
            this.BDGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Grid_KeyDown);
            // 
            // g_SNo
            // 
            this.g_SNo.HeaderText = "SNo";
            this.g_SNo.Name = "g_SNo";
            this.g_SNo.ReadOnly = true;
            this.g_SNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.g_SNo.Width = 50;
            // 
            // g_TermId
            // 
            this.g_TermId.HeaderText = "Term Id";
            this.g_TermId.Name = "g_TermId";
            this.g_TermId.ReadOnly = true;
            this.g_TermId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.g_TermId.Visible = false;
            // 
            // g_Desc
            // 
            this.g_Desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.g_Desc.FillWeight = 130F;
            this.g_Desc.HeaderText = "Description";
            this.g_Desc.MaxInputLength = 256;
            this.g_Desc.Name = "g_Desc";
            this.g_Desc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // g_Basis
            // 
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.g_Basis.DefaultCellStyle = dataGridViewCellStyle5;
            this.g_Basis.HeaderText = "Basis";
            this.g_Basis.MaxInputLength = 255;
            this.g_Basis.Name = "g_Basis";
            this.g_Basis.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.g_Basis.Width = 55;
            // 
            // g_Sign
            // 
            this.g_Sign.HeaderText = "Sign";
            this.g_Sign.Name = "g_Sign";
            this.g_Sign.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.g_Sign.Width = 50;
            // 
            // g_Rate
            // 
            this.g_Rate.HeaderText = "Rate";
            this.g_Rate.Name = "g_Rate";
            this.g_Rate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.g_Rate.Width = 50;
            // 
            // g_Amount
            // 
            dataGridViewCellStyle6.Format = "0.00";
            this.g_Amount.DefaultCellStyle = dataGridViewCellStyle6;
            this.g_Amount.HeaderText = "Amount";
            this.g_Amount.MaxInputLength = 18;
            this.g_Amount.Name = "g_Amount";
            this.g_Amount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // g_Formula
            // 
            this.g_Formula.HeaderText = "Formula";
            this.g_Formula.Name = "g_Formula";
            this.g_Formula.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.g_Formula.Visible = false;
            // 
            // g_TaxationType
            // 
            this.g_TaxationType.HeaderText = "Taxation Type";
            this.g_TaxationType.Name = "g_TaxationType";
            this.g_TaxationType.Visible = false;
            // 
            // gv_PGroup
            // 
            this.gv_PGroup.HeaderText = "PGroup";
            this.gv_PGroup.Name = "gv_PGroup";
            this.gv_PGroup.Visible = false;
            // 
            // gv_OrderNo
            // 
            this.gv_OrderNo.HeaderText = "Order No";
            this.gv_OrderNo.Name = "gv_OrderNo";
            this.gv_OrderNo.Visible = false;
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.panel1);
            this.PanelHeader.Controls.Add(this.PnlBillingTerm2);
            this.PanelHeader.Controls.Add(this.PnlBillingTerm1);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(588, 512);
            this.PanelHeader.TabIndex = 102;
            // 
            // PnlBillingTerm2
            // 
            this.PnlBillingTerm2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PnlBillingTerm2.Controls.Add(this.label1);
            this.PnlBillingTerm2.Controls.Add(this.clsSeparator5);
            this.PnlBillingTerm2.Controls.Add(this.clsSeparator2);
            this.PnlBillingTerm2.Controls.Add(this.BDGrid);
            this.PnlBillingTerm2.Controls.Add(this.lbl_Total1);
            this.PnlBillingTerm2.Controls.Add(this.chk_ProTerm);
            this.PnlBillingTerm2.Controls.Add(this.lbl_BasicAmt1);
            this.PnlBillingTerm2.Controls.Add(this.lbl_BasicAmt);
            this.PnlBillingTerm2.Controls.Add(this.lbl_Total);
            this.PnlBillingTerm2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlBillingTerm2.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlBillingTerm2.Location = new System.Drawing.Point(0, 220);
            this.PnlBillingTerm2.Name = "PnlBillingTerm2";
            this.PnlBillingTerm2.Size = new System.Drawing.Size(588, 292);
            this.PnlBillingTerm2.TabIndex = 103;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label1.Location = new System.Drawing.Point(6, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 20);
            this.label1.TabIndex = 100;
            this.label1.Text = "Billwise Term";
            this.label1.Visible = false;
            // 
            // clsSeparator5
            // 
            this.clsSeparator5.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator5.Location = new System.Drawing.Point(7, 30);
            this.clsSeparator5.Name = "clsSeparator5";
            this.clsSeparator5.Size = new System.Drawing.Size(576, 2);
            this.clsSeparator5.TabIndex = 99;
            this.clsSeparator5.TabStop = false;
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator2.Location = new System.Drawing.Point(6, 216);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(576, 2);
            this.clsSeparator2.TabIndex = 99;
            this.clsSeparator2.TabStop = false;
            // 
            // PnlBillingTerm1
            // 
            this.PnlBillingTerm1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PnlBillingTerm1.Controls.Add(this.clsSeparator4);
            this.PnlBillingTerm1.Controls.Add(this.clsSeparator1);
            this.PnlBillingTerm1.Controls.Add(this.lbl_TotProTermAmt);
            this.PnlBillingTerm1.Controls.Add(this.lbl_Procap);
            this.PnlBillingTerm1.Controls.Add(this.lbl_BasicTotProAmt);
            this.PnlBillingTerm1.Controls.Add(this.PDGrid);
            this.PnlBillingTerm1.Controls.Add(this.lblBillAmount);
            this.PnlBillingTerm1.Controls.Add(this.lblTotalAmount);
            this.PnlBillingTerm1.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlBillingTerm1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlBillingTerm1.Location = new System.Drawing.Point(0, 0);
            this.PnlBillingTerm1.Name = "PnlBillingTerm1";
            this.PnlBillingTerm1.Size = new System.Drawing.Size(588, 220);
            this.PnlBillingTerm1.TabIndex = 102;
            // 
            // clsSeparator4
            // 
            this.clsSeparator4.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator4.Location = new System.Drawing.Point(0, 30);
            this.clsSeparator4.Name = "clsSeparator4";
            this.clsSeparator4.Size = new System.Drawing.Size(576, 2);
            this.clsSeparator4.TabIndex = 99;
            this.clsSeparator4.TabStop = false;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator1.Location = new System.Drawing.Point(6, 214);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(576, 2);
            this.clsSeparator1.TabIndex = 98;
            this.clsSeparator1.TabStop = false;
            // 
            // TxtTermRate
            // 
            this.TxtTermRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTermRate.Font = new System.Drawing.Font("Arial", 9F);
            this.TxtTermRate.Location = new System.Drawing.Point(428, 2);
            this.TxtTermRate.MaxLength = 255;
            this.TxtTermRate.Name = "TxtTermRate";
            this.TxtTermRate.Size = new System.Drawing.Size(43, 21);
            this.TxtTermRate.TabIndex = 2;
            this.TxtTermRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtTermRate.TextChanged += new System.EventHandler(this.txt_Rate_TextChanged);
            this.TxtTermRate.Enter += new System.EventHandler(this.txt_Rate_Enter);
            this.TxtTermRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Rate_KeyPress);
            this.TxtTermRate.Validated += new System.EventHandler(this.txt_Rate_Validated);
            // 
            // TxtTermAmount
            // 
            this.TxtTermAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTermAmount.Font = new System.Drawing.Font("Arial", 9F);
            this.TxtTermAmount.Location = new System.Drawing.Point(473, 2);
            this.TxtTermAmount.MaxLength = 255;
            this.TxtTermAmount.Name = "TxtTermAmount";
            this.TxtTermAmount.Size = new System.Drawing.Size(102, 21);
            this.TxtTermAmount.TabIndex = 3;
            this.TxtTermAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtTermAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Amount_KeyPress);
            this.TxtTermAmount.Validated += new System.EventHandler(this.txt_Amount_Validated);
            // 
            // TxtTermSign
            // 
            this.TxtTermSign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTermSign.Enabled = false;
            this.TxtTermSign.Font = new System.Drawing.Font("Arial", 9F);
            this.TxtTermSign.Location = new System.Drawing.Point(379, 2);
            this.TxtTermSign.MaxLength = 255;
            this.TxtTermSign.Name = "TxtTermSign";
            this.TxtTermSign.Size = new System.Drawing.Size(49, 21);
            this.TxtTermSign.TabIndex = 107;
            // 
            // TxtTermDescription
            // 
            this.TxtTermDescription.BackColor = System.Drawing.SystemColors.Window;
            this.TxtTermDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTermDescription.Enabled = false;
            this.TxtTermDescription.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTermDescription.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtTermDescription.Location = new System.Drawing.Point(93, 2);
            this.TxtTermDescription.MaxLength = 255;
            this.TxtTermDescription.Name = "TxtTermDescription";
            this.TxtTermDescription.Size = new System.Drawing.Size(222, 21);
            this.TxtTermDescription.TabIndex = 105;
            // 
            // TxtBasicAmount
            // 
            this.TxtBasicAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBasicAmount.Enabled = false;
            this.TxtBasicAmount.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBasicAmount.Location = new System.Drawing.Point(316, 2);
            this.TxtBasicAmount.MaxLength = 255;
            this.TxtBasicAmount.Name = "TxtBasicAmount";
            this.TxtBasicAmount.Size = new System.Drawing.Size(63, 21);
            this.TxtBasicAmount.TabIndex = 106;
            // 
            // btn_Ok
            // 
            this.btn_Ok.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Ok.Appearance.Options.UseFont = true;
            this.btn_Ok.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.btn_Ok.Location = new System.Drawing.Point(374, 29);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(92, 32);
            this.btn_Ok.TabIndex = 108;
            this.btn_Ok.Text = "&SAVE";
            this.btn_Ok.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.btnCancel.Location = new System.Drawing.Point(468, 29);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(108, 32);
            this.btnCancel.TabIndex = 109;
            this.btnCancel.Text = "&CANCEL";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // clsSeparator3
            // 
            this.clsSeparator3.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator3.Location = new System.Drawing.Point(6, 24);
            this.clsSeparator3.Name = "clsSeparator3";
            this.clsSeparator3.Size = new System.Drawing.Size(576, 2);
            this.clsSeparator3.TabIndex = 100;
            this.clsSeparator3.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.clsSeparator3);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btn_Ok);
            this.panel1.Controls.Add(this.TxtBasicAmount);
            this.panel1.Controls.Add(this.TxtTermDescription);
            this.panel1.Controls.Add(this.TxtTermSign);
            this.panel1.Controls.Add(this.TxtTermAmount);
            this.panel1.Controls.Add(this.TxtTermRate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 446);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(588, 66);
            this.panel1.TabIndex = 110;
            // 
            // FrmBillingTerm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(588, 512);
            this.ControlBox = false;
            this.Controls.Add(this.PanelHeader);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBillingTerm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Billing Term";
            this.Load += new System.EventHandler(this.Billing_Term_Load);
            this.Shown += new System.EventHandler(this.Billing_Term_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Billing_Term_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.PDGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BDGrid)).EndInit();
            this.PanelHeader.ResumeLayout(false);
            this.PnlBillingTerm2.ResumeLayout(false);
            this.PnlBillingTerm2.PerformLayout();
            this.PnlBillingTerm1.ResumeLayout(false);
            this.PnlBillingTerm1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl_TotProTermAmt;
        private System.Windows.Forms.Label lbl_BasicTotProAmt;
        private System.Windows.Forms.DataGridView PDGrid;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label lblBillAmount;
        private System.Windows.Forms.Label lbl_Procap;
        private System.Windows.Forms.Label lbl_BasicAmt;
        private System.Windows.Forms.Label lbl_Total;
        private System.Windows.Forms.DataGridView BDGrid;
        private System.Windows.Forms.Label lbl_BasicAmt1;
        private System.Windows.Forms.Label lbl_Total1;
        private System.Windows.Forms.CheckBox chk_ProTerm;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_TermId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Basis;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Sign;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Formula;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_TaxationType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_PGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn g_SNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn g_TermId;
        private System.Windows.Forms.DataGridViewTextBoxColumn g_Desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn g_Basis;
        private System.Windows.Forms.DataGridViewTextBoxColumn g_Sign;
        private System.Windows.Forms.DataGridViewTextBoxColumn g_Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn g_Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn g_Formula;
        private System.Windows.Forms.DataGridViewTextBoxColumn g_TaxationType;
        private System.Windows.Forms.DataGridViewTextBoxColumn gv_PGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn gv_OrderNo;
        private ClsSeparator clsSeparator1;
        private ClsSeparator clsSeparator2;
        private ClsSeparator clsSeparator4;
        private ClsSeparator clsSeparator5;
        private System.Windows.Forms.Label label1;
        private MrPanel panel1;
        private ClsSeparator clsSeparator3;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btn_Ok;
        private MrTextBox TxtBasicAmount;
        private MrTextBox TxtTermDescription;
        private MrTextBox TxtTermSign;
        private MrTextBox TxtTermAmount;
        private MrTextBox TxtTermRate;
        private MrPanel PanelHeader;
        private MrPanel PnlBillingTerm1;
        private MrPanel PnlBillingTerm2;
    }
}