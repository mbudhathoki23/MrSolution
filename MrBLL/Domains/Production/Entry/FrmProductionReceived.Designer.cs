using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.Production.Entry
{
    partial class FrmProductionReceived
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblStockQty = new System.Windows.Forms.Label();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtMaster = new DevExpress.XtraEditors.TextEdit();
            this.dgvPurchase = new System.Windows.Forms.DataGridView();
            this.dgv_Sno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_ProductId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_UnitId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.label11 = new System.Windows.Forms.Label();
            this.lblDetTotNetAmt = new System.Windows.Forms.Label();
            this.lbl_TotQty = new System.Windows.Forms.Label();
            this.lbl_TotQty1 = new System.Windows.Forms.Label();
            this.lbl_NoInWordsDetl = new System.Windows.Forms.Label();
            this.lbl_NoInWords = new System.Windows.Forms.Label();
            this.txtRemarks = new MrTextBox();
            this.lbl_Remarks = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btnSalesOrder = new DevExpress.XtraEditors.SimpleButton();
            this.txtSalesOrder = new DevExpress.XtraEditors.TextEdit();
            this.txtCustomer = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnReverse = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnNew = new DevExpress.XtraEditors.SimpleButton();
            this.label2 = new System.Windows.Forms.Label();
            this.mskMiti = new System.Windows.Forms.DateTimePicker();
            this.dpDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblPurchaseRate = new System.Windows.Forms.Label();
            this.lblSalesRate = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtVno = new DevExpress.XtraEditors.TextEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.btnVno = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtRate = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtQty = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtProduct = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaster.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchase)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSalesOrder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtVno.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProduct.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStockQty
            // 
            this.lblStockQty.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblStockQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStockQty.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblStockQty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblStockQty.Location = new System.Drawing.Point(79, 69);
            this.lblStockQty.Name = "lblStockQty";
            this.lblStockQty.Size = new System.Drawing.Size(110, 24);
            this.lblStockQty.TabIndex = 232;
            this.lblStockQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.Location = new System.Drawing.Point(348, 68);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(86, 18);
            this.labelControl9.TabIndex = 209;
            this.labelControl9.Text = "Cutter Name";
            // 
            // textEdit1
            // 
            this.textEdit1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textEdit1.Location = new System.Drawing.Point(439, 65);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.textEdit1.Properties.Appearance.Options.UseFont = true;
            this.textEdit1.Size = new System.Drawing.Size(190, 24);
            this.textEdit1.TabIndex = 208;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Location = new System.Drawing.Point(9, 68);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(89, 18);
            this.labelControl8.TabIndex = 207;
            this.labelControl8.Text = "Master Name";
            // 
            // txtMaster
            // 
            this.txtMaster.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtMaster.Location = new System.Drawing.Point(106, 65);
            this.txtMaster.Name = "txtMaster";
            this.txtMaster.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.txtMaster.Properties.Appearance.Options.UseFont = true;
            this.txtMaster.Size = new System.Drawing.Size(234, 24);
            this.txtMaster.TabIndex = 206;
            // 
            // dgvPurchase
            // 
            this.dgvPurchase.AllowUserToAddRows = false;
            this.dgvPurchase.AllowUserToResizeColumns = false;
            this.dgvPurchase.AllowUserToResizeRows = false;
            this.dgvPurchase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPurchase.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvPurchase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPurchase.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_Sno,
            this.dgv_ProductId,
            this.dgv_Code,
            this.dgv_Description,
            this.dgv_Qty,
            this.dgv_UnitId,
            this.dgv_Unit,
            this.dgv_Rate,
            this.dgv_Amount});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPurchase.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvPurchase.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvPurchase.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvPurchase.Location = new System.Drawing.Point(3, 260);
            this.dgvPurchase.Name = "dgvPurchase";
            this.dgvPurchase.ReadOnly = true;
            this.dgvPurchase.RowHeadersVisible = false;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvPurchase.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvPurchase.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPurchase.Size = new System.Drawing.Size(633, 128);
            this.dgvPurchase.TabIndex = 322;
            // 
            // dgv_Sno
            // 
            this.dgv_Sno.HeaderText = "Sno";
            this.dgv_Sno.Name = "dgv_Sno";
            this.dgv_Sno.ReadOnly = true;
            this.dgv_Sno.Width = 45;
            // 
            // dgv_ProductId
            // 
            this.dgv_ProductId.HeaderText = "ProductId";
            this.dgv_ProductId.Name = "dgv_ProductId";
            this.dgv_ProductId.ReadOnly = true;
            this.dgv_ProductId.Visible = false;
            this.dgv_ProductId.Width = 150;
            // 
            // dgv_Code
            // 
            this.dgv_Code.HeaderText = "Code";
            this.dgv_Code.Name = "dgv_Code";
            this.dgv_Code.ReadOnly = true;
            this.dgv_Code.Visible = false;
            this.dgv_Code.Width = 120;
            // 
            // dgv_Description
            // 
            this.dgv_Description.HeaderText = "Description";
            this.dgv_Description.Name = "dgv_Description";
            this.dgv_Description.ReadOnly = true;
            this.dgv_Description.Width = 250;
            // 
            // dgv_Qty
            // 
            this.dgv_Qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv_Qty.HeaderText = "Qty";
            this.dgv_Qty.Name = "dgv_Qty";
            this.dgv_Qty.ReadOnly = true;
            this.dgv_Qty.Width = 62;
            // 
            // dgv_UnitId
            // 
            this.dgv_UnitId.HeaderText = "UnitId";
            this.dgv_UnitId.Name = "dgv_UnitId";
            this.dgv_UnitId.ReadOnly = true;
            this.dgv_UnitId.Visible = false;
            // 
            // dgv_Unit
            // 
            this.dgv_Unit.HeaderText = "Unit";
            this.dgv_Unit.Name = "dgv_Unit";
            this.dgv_Unit.ReadOnly = true;
            this.dgv_Unit.Width = 80;
            // 
            // dgv_Rate
            // 
            this.dgv_Rate.HeaderText = "Rate";
            this.dgv_Rate.Name = "dgv_Rate";
            this.dgv_Rate.ReadOnly = true;
            this.dgv_Rate.Width = 80;
            // 
            // dgv_Amount
            // 
            this.dgv_Amount.HeaderText = "Amount";
            this.dgv_Amount.Name = "dgv_Amount";
            this.dgv_Amount.ReadOnly = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnCancel);
            this.groupBox4.Controls.Add(this.btnSave);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.lblDetTotNetAmt);
            this.groupBox4.Controls.Add(this.lbl_TotQty);
            this.groupBox4.Controls.Add(this.lbl_TotQty1);
            this.groupBox4.Controls.Add(this.lbl_NoInWordsDetl);
            this.groupBox4.Controls.Add(this.lbl_NoInWords);
            this.groupBox4.Controls.Add(this.txtRemarks);
            this.groupBox4.Controls.Add(this.lbl_Remarks);
            this.groupBox4.Location = new System.Drawing.Point(0, 385);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(636, 120);
            this.groupBox4.TabIndex = 323;
            this.groupBox4.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(565, 90);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 27);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "&Cancel";
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Location = new System.Drawing.Point(490, 90);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(72, 27);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "&Save";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(393, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 17);
            this.label11.TabIndex = 245;
            this.label11.Text = "Total Net Amt";
            // 
            // lblDetTotNetAmt
            // 
            this.lblDetTotNetAmt.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblDetTotNetAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDetTotNetAmt.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetTotNetAmt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDetTotNetAmt.Location = new System.Drawing.Point(498, 12);
            this.lblDetTotNetAmt.Name = "lblDetTotNetAmt";
            this.lblDetTotNetAmt.Size = new System.Drawing.Size(108, 24);
            this.lblDetTotNetAmt.TabIndex = 244;
            this.lblDetTotNetAmt.Text = "0.00";
            this.lblDetTotNetAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_TotQty
            // 
            this.lbl_TotQty.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lbl_TotQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_TotQty.Font = new System.Drawing.Font("Arial", 11F);
            this.lbl_TotQty.Location = new System.Drawing.Point(298, 14);
            this.lbl_TotQty.Name = "lbl_TotQty";
            this.lbl_TotQty.Size = new System.Drawing.Size(89, 24);
            this.lbl_TotQty.TabIndex = 243;
            this.lbl_TotQty.Text = "0.00";
            this.lbl_TotQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_TotQty1
            // 
            this.lbl_TotQty1.AutoSize = true;
            this.lbl_TotQty1.Font = new System.Drawing.Font("Arial", 11F);
            this.lbl_TotQty1.Location = new System.Drawing.Point(222, 18);
            this.lbl_TotQty1.Name = "lbl_TotQty1";
            this.lbl_TotQty1.Size = new System.Drawing.Size(69, 17);
            this.lbl_TotQty1.TabIndex = 242;
            this.lbl_TotQty1.Text = "Total  Qty";
            // 
            // lbl_NoInWordsDetl
            // 
            this.lbl_NoInWordsDetl.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NoInWordsDetl.Location = new System.Drawing.Point(107, 38);
            this.lbl_NoInWordsDetl.Name = "lbl_NoInWordsDetl";
            this.lbl_NoInWordsDetl.Size = new System.Drawing.Size(526, 22);
            this.lbl_NoInWordsDetl.TabIndex = 225;
            this.lbl_NoInWordsDetl.Text = "Only.";
            // 
            // lbl_NoInWords
            // 
            this.lbl_NoInWords.AutoSize = true;
            this.lbl_NoInWords.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NoInWords.Location = new System.Drawing.Point(7, 38);
            this.lbl_NoInWords.Name = "lbl_NoInWords";
            this.lbl_NoInWords.Size = new System.Drawing.Size(67, 17);
            this.lbl_NoInWords.TabIndex = 224;
            this.lbl_NoInWords.Text = "In Words";
            // 
            // txtRemarks
            // 
            this.txtRemarks.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(108, 63);
            this.txtRemarks.MaxLength = 255;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(527, 24);
            this.txtRemarks.TabIndex = 13;
            // 
            // lbl_Remarks
            // 
            this.lbl_Remarks.AutoSize = true;
            this.lbl_Remarks.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Remarks.Location = new System.Drawing.Point(6, 67);
            this.lbl_Remarks.Name = "lbl_Remarks";
            this.lbl_Remarks.Size = new System.Drawing.Size(68, 17);
            this.lbl_Remarks.TabIndex = 223;
            this.lbl_Remarks.Text = "Remarks";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial", 10F);
            this.label13.ForeColor = System.Drawing.SystemColors.Window;
            this.label13.Location = new System.Drawing.Point(5, 75);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(30, 16);
            this.label13.TabIndex = 218;
            this.label13.Text = "Qty";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(253, 13);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(61, 18);
            this.labelControl5.TabIndex = 205;
            this.labelControl5.Text = "Issue No.";
            // 
            // btnSalesOrder
            // 
            this.btnSalesOrder.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalesOrder.Appearance.Options.UseFont = true;
            this.btnSalesOrder.Location = new System.Drawing.Point(423, 9);
            this.btnSalesOrder.Name = "btnSalesOrder";
            this.btnSalesOrder.Size = new System.Drawing.Size(33, 26);
            this.btnSalesOrder.TabIndex = 204;
            this.btnSalesOrder.Text = "**";
            // 
            // txtSalesOrder
            // 
            this.txtSalesOrder.Location = new System.Drawing.Point(326, 11);
            this.txtSalesOrder.Name = "txtSalesOrder";
            this.txtSalesOrder.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.txtSalesOrder.Properties.Appearance.Options.UseFont = true;
            this.txtSalesOrder.Size = new System.Drawing.Size(129, 24);
            this.txtSalesOrder.TabIndex = 7;
            // 
            // txtCustomer
            // 
            this.txtCustomer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtCustomer.Location = new System.Drawing.Point(106, 38);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.txtCustomer.Properties.Appearance.Options.UseFont = true;
            this.txtCustomer.Size = new System.Drawing.Size(350, 24);
            this.txtCustomer.TabIndex = 10;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(7, 40);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(65, 18);
            this.labelControl2.TabIndex = 202;
            this.labelControl2.Text = "Customer";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnReverse);
            this.groupBox1.Controls.Add(this.btnPrint);
            this.groupBox1.Controls.Add(this.btnExit);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnEdit);
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Location = new System.Drawing.Point(3, -3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(632, 46);
            this.groupBox1.TabIndex = 318;
            this.groupBox1.TabStop = false;
            // 
            // btnReverse
            // 
            this.btnReverse.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReverse.Appearance.Options.UseFont = true;
            this.btnReverse.Location = new System.Drawing.Point(412, 9);
            this.btnReverse.Name = "btnReverse";
            this.btnReverse.Size = new System.Drawing.Size(74, 30);
            this.btnReverse.TabIndex = 3;
            this.btnReverse.Text = "&Reverse";
            // 
            // btnPrint
            // 
            this.btnPrint.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Appearance.Options.UseFont = true;
            this.btnPrint.Location = new System.Drawing.Point(492, 9);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(64, 30);
            this.btnPrint.TabIndex = 4;
            this.btnPrint.Text = "&Print";
            // 
            // btnExit
            // 
            this.btnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.Location = new System.Drawing.Point(562, 9);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(64, 30);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "E&xit";
            // 
            // btnDelete
            // 
            this.btnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Appearance.Options.UseFont = true;
            this.btnDelete.Location = new System.Drawing.Point(147, 9);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(64, 30);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "&Delete";
            // 
            // btnEdit
            // 
            this.btnEdit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Appearance.Options.UseFont = true;
            this.btnEdit.Location = new System.Drawing.Point(77, 9);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(64, 30);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "&Edit";
            // 
            // btnNew
            // 
            this.btnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Appearance.Options.UseFont = true;
            this.btnNew.Location = new System.Drawing.Point(7, 9);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(64, 30);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "&New";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 11F);
            this.label2.Location = new System.Drawing.Point(457, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 17);
            this.label2.TabIndex = 201;
            this.label2.Text = "Date";
            // 
            // mskMiti
            // 
            this.mskMiti.CalendarTitleBackColor = System.Drawing.SystemColors.Highlight;
            this.mskMiti.CustomFormat = "dd/MM/yyyy";
            this.mskMiti.Font = new System.Drawing.Font("Arial", 11F);
            this.mskMiti.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.mskMiti.Location = new System.Drawing.Point(502, 10);
            this.mskMiti.Name = "mskMiti";
            this.mskMiti.ShowUpDown = true;
            this.mskMiti.Size = new System.Drawing.Size(127, 24);
            this.mskMiti.TabIndex = 8;
            this.mskMiti.Value = new System.DateTime(2017, 12, 13, 21, 58, 56, 0);
            // 
            // dpDate
            // 
            this.dpDate.CalendarTitleBackColor = System.Drawing.SystemColors.Highlight;
            this.dpDate.CustomFormat = "dd/MM/yyyy";
            this.dpDate.Font = new System.Drawing.Font("Arial", 11F);
            this.dpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpDate.Location = new System.Drawing.Point(502, 38);
            this.dpDate.Name = "dpDate";
            this.dpDate.ShowUpDown = true;
            this.dpDate.Size = new System.Drawing.Size(127, 24);
            this.dpDate.TabIndex = 9;
            this.dpDate.Value = new System.DateTime(2017, 12, 13, 21, 58, 56, 0);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblStockQty);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.lblPurchaseRate);
            this.groupBox5.Controls.Add(this.lblSalesRate);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Font = new System.Drawing.Font("Arial", 10F);
            this.groupBox5.Location = new System.Drawing.Point(441, 156);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(195, 98);
            this.groupBox5.TabIndex = 321;
            this.groupBox5.TabStop = false;
            // 
            // lblPurchaseRate
            // 
            this.lblPurchaseRate.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblPurchaseRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPurchaseRate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblPurchaseRate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPurchaseRate.Location = new System.Drawing.Point(79, 12);
            this.lblPurchaseRate.Name = "lblPurchaseRate";
            this.lblPurchaseRate.Size = new System.Drawing.Size(112, 25);
            this.lblPurchaseRate.TabIndex = 233;
            this.lblPurchaseRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSalesRate
            // 
            this.lblSalesRate.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblSalesRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSalesRate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblSalesRate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSalesRate.Location = new System.Drawing.Point(79, 41);
            this.lblSalesRate.Name = "lblSalesRate";
            this.lblSalesRate.Size = new System.Drawing.Size(112, 24);
            this.lblSalesRate.TabIndex = 234;
            this.lblSalesRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Arial", 10F);
            this.label15.ForeColor = System.Drawing.SystemColors.Window;
            this.label15.Location = new System.Drawing.Point(2, 45);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 16);
            this.label15.TabIndex = 220;
            this.label15.Text = "Sales Rate";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Arial", 10F);
            this.label14.ForeColor = System.Drawing.SystemColors.Window;
            this.label14.Location = new System.Drawing.Point(6, 15);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(66, 16);
            this.label14.TabIndex = 219;
            this.label14.Text = "Buy Rate";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 11F);
            this.label1.Location = new System.Drawing.Point(458, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 17);
            this.label1.TabIndex = 200;
            this.label1.Text = "Miti";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(7, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(75, 18);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Costing No.";
            // 
            // txtVno
            // 
            this.txtVno.Location = new System.Drawing.Point(106, 11);
            this.txtVno.Name = "txtVno";
            this.txtVno.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.txtVno.Properties.Appearance.Options.UseFont = true;
            this.txtVno.Size = new System.Drawing.Size(132, 24);
            this.txtVno.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelControl10);
            this.groupBox2.Controls.Add(this.textEdit2);
            this.groupBox2.Controls.Add(this.labelControl9);
            this.groupBox2.Controls.Add(this.textEdit1);
            this.groupBox2.Controls.Add(this.labelControl8);
            this.groupBox2.Controls.Add(this.txtMaster);
            this.groupBox2.Controls.Add(this.labelControl5);
            this.groupBox2.Controls.Add(this.btnSalesOrder);
            this.groupBox2.Controls.Add(this.txtSalesOrder);
            this.groupBox2.Controls.Add(this.txtCustomer);
            this.groupBox2.Controls.Add(this.labelControl2);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.mskMiti);
            this.groupBox2.Controls.Add(this.dpDate);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnVno);
            this.groupBox2.Controls.Add(this.labelControl1);
            this.groupBox2.Controls.Add(this.txtVno);
            this.groupBox2.Location = new System.Drawing.Point(3, 38);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(632, 124);
            this.groupBox2.TabIndex = 319;
            this.groupBox2.TabStop = false;
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.labelControl10.Appearance.Options.UseFont = true;
            this.labelControl10.Location = new System.Drawing.Point(11, 95);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(79, 18);
            this.labelControl10.TabIndex = 211;
            this.labelControl10.Text = "Department";
            // 
            // textEdit2
            // 
            this.textEdit2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textEdit2.Location = new System.Drawing.Point(106, 92);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.textEdit2.Properties.Appearance.Options.UseFont = true;
            this.textEdit2.Size = new System.Drawing.Size(234, 24);
            this.textEdit2.TabIndex = 210;
            // 
            // btnVno
            // 
            this.btnVno.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVno.Appearance.Options.UseFont = true;
            this.btnVno.Location = new System.Drawing.Point(214, 10);
            this.btnVno.Name = "btnVno";
            this.btnVno.Size = new System.Drawing.Size(33, 26);
            this.btnVno.TabIndex = 3;
            this.btnVno.Text = "**";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.labelControl6);
            this.groupBox3.Controls.Add(this.txtRate);
            this.groupBox3.Controls.Add(this.labelControl3);
            this.groupBox3.Controls.Add(this.labelControl4);
            this.groupBox3.Controls.Add(this.txtQty);
            this.groupBox3.Controls.Add(this.labelControl7);
            this.groupBox3.Controls.Add(this.txtProduct);
            this.groupBox3.Location = new System.Drawing.Point(4, 158);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(436, 97);
            this.groupBox3.TabIndex = 320;
            this.groupBox3.TabStop = false;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(11, 73);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(30, 18);
            this.labelControl6.TabIndex = 209;
            this.labelControl6.Text = "Rate";
            // 
            // txtRate
            // 
            this.txtRate.Location = new System.Drawing.Point(62, 67);
            this.txtRate.Name = "txtRate";
            this.txtRate.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.txtRate.Properties.Appearance.Options.UseFont = true;
            this.txtRate.Size = new System.Drawing.Size(107, 24);
            this.txtRate.TabIndex = 12;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(175, 42);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(57, 18);
            this.labelControl3.TabIndex = 207;
            this.labelControl3.Text = "Qty Desc";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(11, 43);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(21, 18);
            this.labelControl4.TabIndex = 206;
            this.labelControl4.Text = "Qty";
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(62, 39);
            this.txtQty.Name = "txtQty";
            this.txtQty.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.txtQty.Properties.Appearance.Options.UseFont = true;
            this.txtQty.Size = new System.Drawing.Size(107, 24);
            this.txtQty.TabIndex = 11;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(7, 15);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(49, 18);
            this.labelControl7.TabIndex = 204;
            this.labelControl7.Text = "Product";
            // 
            // txtProduct
            // 
            this.txtProduct.Location = new System.Drawing.Point(62, 12);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.txtProduct.Properties.Appearance.Options.UseFont = true;
            this.txtProduct.Size = new System.Drawing.Size(367, 24);
            this.txtProduct.TabIndex = 10;
            // 
            // FrmProductionReceived
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(637, 507);
            this.Controls.Add(this.dgvPurchase);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmProductionReceived";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Production Received";
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaster.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchase)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSalesOrder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtVno.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProduct.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblStockQty;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit txtMaster;
        private System.Windows.Forms.DataGridView dgvPurchase;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Sno;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_ProductId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_UnitId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Amount;
        private System.Windows.Forms.GroupBox groupBox4;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblDetTotNetAmt;
        private System.Windows.Forms.Label lbl_TotQty;
        private System.Windows.Forms.Label lbl_TotQty1;
        private System.Windows.Forms.Label lbl_NoInWordsDetl;
        private System.Windows.Forms.Label lbl_NoInWords;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label lbl_Remarks;
        private System.Windows.Forms.Label label13;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SimpleButton btnSalesOrder;
        private DevExpress.XtraEditors.TextEdit txtSalesOrder;
        private DevExpress.XtraEditors.TextEdit txtCustomer;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton btnReverse;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.SimpleButton btnNew;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker mskMiti;
        private System.Windows.Forms.DateTimePicker dpDate;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblPurchaseRate;
        private System.Windows.Forms.Label lblSalesRate;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtVno;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.SimpleButton btnVno;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtRate;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtQty;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtProduct;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.TextEdit textEdit2;
    }
}