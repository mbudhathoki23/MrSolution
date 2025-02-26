using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.DataEntry.ProductionMaster
{
    partial class FrmFinishedGoodsReturn
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnReverse = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnNew = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.label2 = new System.Windows.Forms.Label();
            this.mskMiti = new System.Windows.Forms.DateTimePicker();
            this.dpDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRate = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtQty = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtRawMaterial = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.txtVno = new DevExpress.XtraEditors.TextEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtRemarks = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.label11 = new System.Windows.Forms.Label();
            this.lblDetTotNetAmt = new System.Windows.Forms.Label();
            this.lbl_TotQty = new System.Windows.Forms.Label();
            this.lbl_TotQty1 = new System.Windows.Forms.Label();
            this.lbl_NoInWordsDetl = new System.Windows.Forms.Label();
            this.lbl_NoInWords = new System.Windows.Forms.Label();
            this.lbl_Remarks = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblStockQty = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblPurchaseRate = new System.Windows.Forms.Label();
            this.lblSalesRate = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRawMaterial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVno.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvPurchase
            // 
            this.dgvPurchase.AllowUserToAddRows = false;
            this.dgvPurchase.AllowUserToResizeColumns = false;
            this.dgvPurchase.AllowUserToResizeRows = false;
            this.dgvPurchase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPurchase.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPurchase.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPurchase.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvPurchase.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvPurchase.Location = new System.Drawing.Point(3, 246);
            this.dgvPurchase.Margin = new System.Windows.Forms.Padding(4);
            this.dgvPurchase.Name = "dgvPurchase";
            this.dgvPurchase.ReadOnly = true;
            this.dgvPurchase.RowHeadersVisible = false;
            this.dgvPurchase.RowHeadersWidth = 51;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvPurchase.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPurchase.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPurchase.Size = new System.Drawing.Size(844, 197);
            this.dgvPurchase.TabIndex = 312;
            // 
            // dgv_Sno
            // 
            this.dgv_Sno.HeaderText = "Sno";
            this.dgv_Sno.MinimumWidth = 6;
            this.dgv_Sno.Name = "dgv_Sno";
            this.dgv_Sno.ReadOnly = true;
            this.dgv_Sno.Width = 45;
            // 
            // dgv_ProductId
            // 
            this.dgv_ProductId.HeaderText = "ProductId";
            this.dgv_ProductId.MinimumWidth = 6;
            this.dgv_ProductId.Name = "dgv_ProductId";
            this.dgv_ProductId.ReadOnly = true;
            this.dgv_ProductId.Visible = false;
            this.dgv_ProductId.Width = 150;
            // 
            // dgv_Code
            // 
            this.dgv_Code.HeaderText = "Code";
            this.dgv_Code.MinimumWidth = 6;
            this.dgv_Code.Name = "dgv_Code";
            this.dgv_Code.ReadOnly = true;
            this.dgv_Code.Visible = false;
            this.dgv_Code.Width = 120;
            // 
            // dgv_Description
            // 
            this.dgv_Description.HeaderText = "Description";
            this.dgv_Description.MinimumWidth = 6;
            this.dgv_Description.Name = "dgv_Description";
            this.dgv_Description.ReadOnly = true;
            this.dgv_Description.Width = 250;
            // 
            // dgv_Qty
            // 
            this.dgv_Qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv_Qty.HeaderText = "Qty";
            this.dgv_Qty.MinimumWidth = 6;
            this.dgv_Qty.Name = "dgv_Qty";
            this.dgv_Qty.ReadOnly = true;
            this.dgv_Qty.Width = 74;
            // 
            // dgv_UnitId
            // 
            this.dgv_UnitId.HeaderText = "UnitId";
            this.dgv_UnitId.MinimumWidth = 6;
            this.dgv_UnitId.Name = "dgv_UnitId";
            this.dgv_UnitId.ReadOnly = true;
            this.dgv_UnitId.Visible = false;
            this.dgv_UnitId.Width = 125;
            // 
            // dgv_Unit
            // 
            this.dgv_Unit.HeaderText = "Unit";
            this.dgv_Unit.MinimumWidth = 6;
            this.dgv_Unit.Name = "dgv_Unit";
            this.dgv_Unit.ReadOnly = true;
            this.dgv_Unit.Width = 80;
            // 
            // dgv_Rate
            // 
            this.dgv_Rate.HeaderText = "Rate";
            this.dgv_Rate.MinimumWidth = 6;
            this.dgv_Rate.Name = "dgv_Rate";
            this.dgv_Rate.ReadOnly = true;
            this.dgv_Rate.Width = 80;
            // 
            // dgv_Amount
            // 
            this.dgv_Amount.HeaderText = "Amount";
            this.dgv_Amount.MinimumWidth = 6;
            this.dgv_Amount.Name = "dgv_Amount";
            this.dgv_Amount.ReadOnly = true;
            this.dgv_Amount.Width = 125;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(340, 44);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(4);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(44, 32);
            this.simpleButton1.TabIndex = 203;
            this.simpleButton1.Text = "**";
            // 
            // btnReverse
            // 
            this.btnReverse.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReverse.Appearance.Options.UseFont = true;
            this.btnReverse.Location = new System.Drawing.Point(549, 11);
            this.btnReverse.Margin = new System.Windows.Forms.Padding(4);
            this.btnReverse.Name = "btnReverse";
            this.btnReverse.Size = new System.Drawing.Size(99, 37);
            this.btnReverse.TabIndex = 3;
            this.btnReverse.Text = "&Reverse";
            // 
            // btnPrint
            // 
            this.btnPrint.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Appearance.Options.UseFont = true;
            this.btnPrint.Location = new System.Drawing.Point(656, 11);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(85, 37);
            this.btnPrint.TabIndex = 4;
            this.btnPrint.Text = "&Print";
            // 
            // btnExit
            // 
            this.btnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.Location = new System.Drawing.Point(749, 11);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(85, 37);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "E&xit";
            // 
            // btnDelete
            // 
            this.btnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Appearance.Options.UseFont = true;
            this.btnDelete.Location = new System.Drawing.Point(196, 11);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(85, 37);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Tag = "DeleteButtonCheck";
            this.btnDelete.Text = "&Delete";
            // 
            // btnEdit
            // 
            this.btnEdit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Appearance.Options.UseFont = true;
            this.btnEdit.Location = new System.Drawing.Point(103, 11);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(85, 37);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Tag = "EditButtonCheck";
            this.btnEdit.Text = "&Edit";
            // 
            // btnNew
            // 
            this.btnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Appearance.Options.UseFont = true;
            this.btnNew.Location = new System.Drawing.Point(9, 11);
            this.btnNew.Margin = new System.Windows.Forms.Padding(4);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(85, 37);
            this.btnNew.TabIndex = 0;
            this.btnNew.Tag = "NewButtonCheck";
            this.btnNew.Text = "&New";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(11, 48);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(109, 21);
            this.labelControl2.TabIndex = 202;
            this.labelControl2.Text = "Received No.";
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(143, 47);
            this.textEdit1.Margin = new System.Windows.Forms.Padding(4);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.textEdit1.Properties.Appearance.Options.UseFont = true;
            this.textEdit1.Size = new System.Drawing.Size(241, 28);
            this.textEdit1.TabIndex = 7;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(9, 15);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(96, 21);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Costing No.";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(15, 90);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(38, 21);
            this.labelControl6.TabIndex = 209;
            this.labelControl6.Text = "Rate";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 11F);
            this.label2.Location = new System.Drawing.Point(612, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 22);
            this.label2.TabIndex = 201;
            this.label2.Text = "Date";
            // 
            // mskMiti
            // 
            this.mskMiti.CalendarTitleBackColor = System.Drawing.SystemColors.Highlight;
            this.mskMiti.CustomFormat = "dd/MM/yyyy";
            this.mskMiti.Font = new System.Drawing.Font("Arial", 11F);
            this.mskMiti.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.mskMiti.Location = new System.Drawing.Point(669, 14);
            this.mskMiti.Margin = new System.Windows.Forms.Padding(4);
            this.mskMiti.Name = "mskMiti";
            this.mskMiti.ShowUpDown = true;
            this.mskMiti.Size = new System.Drawing.Size(163, 29);
            this.mskMiti.TabIndex = 8;
            this.mskMiti.Value = new System.DateTime(2017, 12, 13, 21, 58, 56, 0);
            // 
            // dpDate
            // 
            this.dpDate.CalendarTitleBackColor = System.Drawing.SystemColors.Highlight;
            this.dpDate.CustomFormat = "dd/MM/yyyy";
            this.dpDate.Font = new System.Drawing.Font("Arial", 11F);
            this.dpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpDate.Location = new System.Drawing.Point(669, 47);
            this.dpDate.Margin = new System.Windows.Forms.Padding(4);
            this.dpDate.Name = "dpDate";
            this.dpDate.ShowUpDown = true;
            this.dpDate.Size = new System.Drawing.Size(163, 29);
            this.dpDate.TabIndex = 9;
            this.dpDate.Value = new System.DateTime(2017, 12, 13, 21, 58, 56, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 11F);
            this.label1.Location = new System.Drawing.Point(608, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 22);
            this.label1.TabIndex = 200;
            this.label1.Text = "Miti";
            // 
            // txtRate
            // 
            this.txtRate.Location = new System.Drawing.Point(83, 82);
            this.txtRate.Margin = new System.Windows.Forms.Padding(4);
            this.txtRate.Name = "txtRate";
            this.txtRate.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.txtRate.Properties.Appearance.Options.UseFont = true;
            this.txtRate.Size = new System.Drawing.Size(143, 28);
            this.txtRate.TabIndex = 12;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(233, 52);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(76, 21);
            this.labelControl5.TabIndex = 207;
            this.labelControl5.Text = "Qty Desc";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(15, 53);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(30, 21);
            this.labelControl4.TabIndex = 206;
            this.labelControl4.Text = "Qty";
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(83, 48);
            this.txtQty.Margin = new System.Windows.Forms.Padding(4);
            this.txtQty.Name = "txtQty";
            this.txtQty.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.txtQty.Properties.Appearance.Options.UseFont = true;
            this.txtQty.Size = new System.Drawing.Size(143, 28);
            this.txtQty.TabIndex = 11;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(9, 18);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(66, 21);
            this.labelControl3.TabIndex = 204;
            this.labelControl3.Text = "Product";
            // 
            // txtRawMaterial
            // 
            this.txtRawMaterial.Location = new System.Drawing.Point(83, 15);
            this.txtRawMaterial.Margin = new System.Windows.Forms.Padding(4);
            this.txtRawMaterial.Name = "txtRawMaterial";
            this.txtRawMaterial.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.txtRawMaterial.Properties.Appearance.Options.UseFont = true;
            this.txtRawMaterial.Size = new System.Drawing.Size(489, 28);
            this.txtRawMaterial.TabIndex = 10;
            // 
            // simpleButton4
            // 
            this.simpleButton4.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton4.Appearance.Options.UseFont = true;
            this.simpleButton4.Location = new System.Drawing.Point(339, 11);
            this.simpleButton4.Margin = new System.Windows.Forms.Padding(4);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(44, 32);
            this.simpleButton4.TabIndex = 3;
            this.simpleButton4.Text = "**";
            // 
            // txtVno
            // 
            this.txtVno.Location = new System.Drawing.Point(141, 14);
            this.txtVno.Margin = new System.Windows.Forms.Padding(4);
            this.txtVno.Name = "txtVno";
            this.txtVno.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.txtVno.Properties.Appearance.Options.UseFont = true;
            this.txtVno.Size = new System.Drawing.Size(241, 28);
            this.txtVno.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnReverse);
            this.groupBox1.Controls.Add(this.btnPrint);
            this.groupBox1.Controls.Add(this.btnExit);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnEdit);
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Location = new System.Drawing.Point(3, -5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(843, 57);
            this.groupBox1.TabIndex = 309;
            this.groupBox1.TabStop = false;
            // 
            // txtRemarks
            // 
            this.txtRemarks.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(144, 78);
            this.txtRemarks.Margin = new System.Windows.Forms.Padding(4);
            this.txtRemarks.MaxLength = 255;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(702, 29);
            this.txtRemarks.TabIndex = 13;
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
            this.groupBox4.Location = new System.Drawing.Point(1, 438);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(848, 148);
            this.groupBox4.TabIndex = 314;
            this.groupBox4.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(753, 111);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 33);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "&Cancel";
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Location = new System.Drawing.Point(653, 111);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(96, 33);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(524, 18);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(122, 22);
            this.label11.TabIndex = 245;
            this.label11.Text = "Total Net Amt";
            // 
            // lblDetTotNetAmt
            // 
            this.lblDetTotNetAmt.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblDetTotNetAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDetTotNetAmt.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetTotNetAmt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDetTotNetAmt.Location = new System.Drawing.Point(664, 15);
            this.lblDetTotNetAmt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDetTotNetAmt.Name = "lblDetTotNetAmt";
            this.lblDetTotNetAmt.Size = new System.Drawing.Size(143, 29);
            this.lblDetTotNetAmt.TabIndex = 244;
            this.lblDetTotNetAmt.Text = "0.00";
            this.lblDetTotNetAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_TotQty
            // 
            this.lbl_TotQty.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lbl_TotQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_TotQty.Font = new System.Drawing.Font("Arial", 11F);
            this.lbl_TotQty.Location = new System.Drawing.Point(397, 17);
            this.lbl_TotQty.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_TotQty.Name = "lbl_TotQty";
            this.lbl_TotQty.Size = new System.Drawing.Size(118, 29);
            this.lbl_TotQty.TabIndex = 243;
            this.lbl_TotQty.Text = "0.00";
            this.lbl_TotQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_TotQty1
            // 
            this.lbl_TotQty1.AutoSize = true;
            this.lbl_TotQty1.Font = new System.Drawing.Font("Arial", 11F);
            this.lbl_TotQty1.Location = new System.Drawing.Point(296, 22);
            this.lbl_TotQty1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_TotQty1.Name = "lbl_TotQty1";
            this.lbl_TotQty1.Size = new System.Drawing.Size(89, 22);
            this.lbl_TotQty1.TabIndex = 242;
            this.lbl_TotQty1.Text = "Total  Qty";
            // 
            // lbl_NoInWordsDetl
            // 
            this.lbl_NoInWordsDetl.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NoInWordsDetl.Location = new System.Drawing.Point(143, 47);
            this.lbl_NoInWordsDetl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_NoInWordsDetl.Name = "lbl_NoInWordsDetl";
            this.lbl_NoInWordsDetl.Size = new System.Drawing.Size(701, 27);
            this.lbl_NoInWordsDetl.TabIndex = 225;
            this.lbl_NoInWordsDetl.Text = "Only.";
            // 
            // lbl_NoInWords
            // 
            this.lbl_NoInWords.AutoSize = true;
            this.lbl_NoInWords.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NoInWords.Location = new System.Drawing.Point(9, 47);
            this.lbl_NoInWords.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_NoInWords.Name = "lbl_NoInWords";
            this.lbl_NoInWords.Size = new System.Drawing.Size(88, 22);
            this.lbl_NoInWords.TabIndex = 224;
            this.lbl_NoInWords.Text = "In Words";
            // 
            // lbl_Remarks
            // 
            this.lbl_Remarks.AutoSize = true;
            this.lbl_Remarks.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Remarks.Location = new System.Drawing.Point(8, 82);
            this.lbl_Remarks.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Remarks.Name = "lbl_Remarks";
            this.lbl_Remarks.Size = new System.Drawing.Size(86, 22);
            this.lbl_Remarks.TabIndex = 223;
            this.lbl_Remarks.Text = "Remarks";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblStockQty);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.lblPurchaseRate);
            this.groupBox5.Controls.Add(this.lblSalesRate);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Font = new System.Drawing.Font("Arial", 10F);
            this.groupBox5.Location = new System.Drawing.Point(585, 121);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(260, 121);
            this.groupBox5.TabIndex = 313;
            this.groupBox5.TabStop = false;
            // 
            // lblStockQty
            // 
            this.lblStockQty.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblStockQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStockQty.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblStockQty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblStockQty.Location = new System.Drawing.Point(105, 84);
            this.lblStockQty.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStockQty.Name = "lblStockQty";
            this.lblStockQty.Size = new System.Drawing.Size(149, 32);
            this.lblStockQty.TabIndex = 232;
            this.lblStockQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial", 10F);
            this.label13.ForeColor = System.Drawing.SystemColors.Window;
            this.label13.Location = new System.Drawing.Point(7, 94);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 19);
            this.label13.TabIndex = 218;
            this.label13.Text = "Qty";
            // 
            // lblPurchaseRate
            // 
            this.lblPurchaseRate.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblPurchaseRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPurchaseRate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblPurchaseRate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPurchaseRate.Location = new System.Drawing.Point(105, 15);
            this.lblPurchaseRate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPurchaseRate.Name = "lblPurchaseRate";
            this.lblPurchaseRate.Size = new System.Drawing.Size(149, 33);
            this.lblPurchaseRate.TabIndex = 233;
            this.lblPurchaseRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSalesRate
            // 
            this.lblSalesRate.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblSalesRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSalesRate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblSalesRate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSalesRate.Location = new System.Drawing.Point(105, 49);
            this.lblSalesRate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSalesRate.Name = "lblSalesRate";
            this.lblSalesRate.Size = new System.Drawing.Size(149, 32);
            this.lblSalesRate.TabIndex = 234;
            this.lblSalesRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Arial", 10F);
            this.label14.ForeColor = System.Drawing.SystemColors.Window;
            this.label14.Location = new System.Drawing.Point(5, 18);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(76, 19);
            this.label14.TabIndex = 219;
            this.label14.Text = "Buy Rate";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Arial", 10F);
            this.label15.ForeColor = System.Drawing.SystemColors.Window;
            this.label15.Location = new System.Drawing.Point(3, 55);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(87, 19);
            this.label15.TabIndex = 220;
            this.label15.Text = "Sales Rate";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.labelControl6);
            this.groupBox3.Controls.Add(this.txtRate);
            this.groupBox3.Controls.Add(this.labelControl5);
            this.groupBox3.Controls.Add(this.labelControl4);
            this.groupBox3.Controls.Add(this.txtQty);
            this.groupBox3.Controls.Add(this.labelControl3);
            this.groupBox3.Controls.Add(this.txtRawMaterial);
            this.groupBox3.Location = new System.Drawing.Point(3, 123);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(581, 119);
            this.groupBox3.TabIndex = 311;
            this.groupBox3.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.simpleButton1);
            this.groupBox2.Controls.Add(this.labelControl2);
            this.groupBox2.Controls.Add(this.textEdit1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.mskMiti);
            this.groupBox2.Controls.Add(this.dpDate);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.simpleButton4);
            this.groupBox2.Controls.Add(this.labelControl1);
            this.groupBox2.Controls.Add(this.txtVno);
            this.groupBox2.Location = new System.Drawing.Point(3, 46);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(843, 84);
            this.groupBox2.TabIndex = 310;
            this.groupBox2.TabStop = false;
            // 
            // FrmFinishedGoodsReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(852, 585);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dgvPurchase);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFinishedGoodsReturn";
            this.ShowIcon = false;
            this.Tag = "FinishedGoods Return";
            this.Text = "FinishedGoods Return";
            this.Load += new System.EventHandler(this.FrmFinishedGoodsReturn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRawMaterial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVno.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

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
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton btnReverse;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.SimpleButton btnNew;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker mskMiti;
        private System.Windows.Forms.DateTimePicker dpDate;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtRate;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtQty;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtRawMaterial;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.TextEdit txtVno;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblDetTotNetAmt;
        private System.Windows.Forms.Label lbl_TotQty;
        private System.Windows.Forms.Label lbl_TotQty1;
        private System.Windows.Forms.Label lbl_NoInWordsDetl;
        private System.Windows.Forms.Label lbl_NoInWords;
        private System.Windows.Forms.Label lbl_Remarks;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblStockQty;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblPurchaseRate;
        private System.Windows.Forms.Label lblSalesRate;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private MrTextBox txtRemarks;
    }
}