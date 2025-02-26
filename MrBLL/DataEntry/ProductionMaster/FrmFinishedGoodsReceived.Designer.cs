using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.DataEntry.ProductionMaster
{
    partial class FrmFinishedGoodsReceived
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
            this.txtRemarks = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblStockQty = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblPurchaseRate = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.label11 = new System.Windows.Forms.Label();
            this.lblDetTotNetAmt = new System.Windows.Forms.Label();
            this.lbl_TotQty = new System.Windows.Forms.Label();
            this.lbl_TotQty1 = new System.Windows.Forms.Label();
            this.lblSalesRate = new System.Windows.Forms.Label();
            this.lbl_Remarks = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnReverse = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnNew = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtMasterCostCenter = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.mskMiti = new System.Windows.Forms.DateTimePicker();
            this.TxtMasterGodown = new DevExpress.XtraEditors.TextEdit();
            this.dpDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnVno = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtVno = new DevExpress.XtraEditors.TextEdit();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnRemarks = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgvPurchase = new System.Windows.Forms.DataGridView();
            this.dgv_Sno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_ProductId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gvn_GdnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gvn_CCId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Godown = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CostCenter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_UnitId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BtnCostCenter = new System.Windows.Forms.Button();
            this.BtnGodown = new System.Windows.Forms.Button();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.TxtCostcenter = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.TxtGodown = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtRate = new DevExpress.XtraEditors.TextEdit();
            this.lblRawQtyUnit = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtQty = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtRawMaterial = new DevExpress.XtraEditors.TextEdit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtMasterCostCenter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtMasterGodown.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVno.Properties)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchase)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtCostcenter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtGodown.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRawMaterial.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtRemarks
            // 
            this.txtRemarks.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(144, 48);
            this.txtRemarks.Margin = new System.Windows.Forms.Padding(4);
            this.txtRemarks.MaxLength = 255;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(879, 29);
            this.txtRemarks.TabIndex = 0;
            this.txtRemarks.Enter += new System.EventHandler(this.txtRemarks_Enter);
            this.txtRemarks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemarks_KeyDown);
            this.txtRemarks.Leave += new System.EventHandler(this.txtRemarks_Leave);
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
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(969, 85);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 33);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Location = new System.Drawing.Point(869, 85);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(96, 33);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(768, 17);
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
            this.lblDetTotNetAmt.Location = new System.Drawing.Point(909, 12);
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
            this.lbl_TotQty.Location = new System.Drawing.Point(641, 12);
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
            this.lbl_TotQty1.Location = new System.Drawing.Point(540, 17);
            this.lbl_TotQty1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_TotQty1.Name = "lbl_TotQty1";
            this.lbl_TotQty1.Size = new System.Drawing.Size(89, 22);
            this.lbl_TotQty1.TabIndex = 242;
            this.lbl_TotQty1.Text = "Total  Qty";
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
            // lbl_Remarks
            // 
            this.lbl_Remarks.AutoSize = true;
            this.lbl_Remarks.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Remarks.Location = new System.Drawing.Point(8, 53);
            this.lbl_Remarks.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Remarks.Name = "lbl_Remarks";
            this.lbl_Remarks.Size = new System.Drawing.Size(86, 22);
            this.lbl_Remarks.TabIndex = 223;
            this.lbl_Remarks.Text = "Remarks";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnReverse);
            this.groupBox1.Controls.Add(this.btnPrint);
            this.groupBox1.Controls.Add(this.btnExit);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnEdit);
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Location = new System.Drawing.Point(3, -4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1067, 57);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnReverse
            // 
            this.btnReverse.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReverse.Appearance.Options.UseFont = true;
            this.btnReverse.Location = new System.Drawing.Point(773, 11);
            this.btnReverse.Margin = new System.Windows.Forms.Padding(4);
            this.btnReverse.Name = "btnReverse";
            this.btnReverse.Size = new System.Drawing.Size(99, 37);
            this.btnReverse.TabIndex = 3;
            this.btnReverse.Text = "&Reverse";
            this.btnReverse.Click += new System.EventHandler(this.btnReverse_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Appearance.Options.UseFont = true;
            this.btnPrint.Location = new System.Drawing.Point(880, 11);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(85, 37);
            this.btnPrint.TabIndex = 4;
            this.btnPrint.Text = "&Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExit
            // 
            this.btnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.Location = new System.Drawing.Point(973, 11);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(85, 37);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "E&xit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
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
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
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
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
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
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelControl2);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.TxtMasterCostCenter);
            this.groupBox2.Controls.Add(this.labelControl5);
            this.groupBox2.Controls.Add(this.mskMiti);
            this.groupBox2.Controls.Add(this.TxtMasterGodown);
            this.groupBox2.Controls.Add(this.dpDate);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnVno);
            this.groupBox2.Controls.Add(this.labelControl1);
            this.groupBox2.Controls.Add(this.txtVno);
            this.groupBox2.Location = new System.Drawing.Point(3, 47);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1067, 97);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(449, 50);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(91, 21);
            this.labelControl2.TabIndex = 217;
            this.labelControl2.Text = "Costcenter";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 11F);
            this.label2.Location = new System.Drawing.Point(837, 15);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 22);
            this.label2.TabIndex = 201;
            this.label2.Text = "Date";
            // 
            // TxtMasterCostCenter
            // 
            this.TxtMasterCostCenter.Location = new System.Drawing.Point(559, 47);
            this.TxtMasterCostCenter.Margin = new System.Windows.Forms.Padding(4);
            this.TxtMasterCostCenter.Name = "TxtMasterCostCenter";
            this.TxtMasterCostCenter.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.TxtMasterCostCenter.Properties.Appearance.Options.UseFont = true;
            this.TxtMasterCostCenter.Size = new System.Drawing.Size(261, 28);
            this.TxtMasterCostCenter.TabIndex = 4;
            this.TxtMasterCostCenter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtMasterCostCenter_KeyDown);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(13, 48);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(69, 21);
            this.labelControl5.TabIndex = 216;
            this.labelControl5.Text = "Godown";
            // 
            // mskMiti
            // 
            this.mskMiti.CalendarTitleBackColor = System.Drawing.SystemColors.Highlight;
            this.mskMiti.CustomFormat = "dd/MM/yyyy";
            this.mskMiti.Font = new System.Drawing.Font("Arial", 11F);
            this.mskMiti.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.mskMiti.Location = new System.Drawing.Point(560, 14);
            this.mskMiti.Margin = new System.Windows.Forms.Padding(4);
            this.mskMiti.Name = "mskMiti";
            this.mskMiti.ShowUpDown = true;
            this.mskMiti.Size = new System.Drawing.Size(163, 29);
            this.mskMiti.TabIndex = 1;
            this.mskMiti.Value = new System.DateTime(2017, 12, 13, 21, 58, 56, 0);
            this.mskMiti.Enter += new System.EventHandler(this.mskMiti_Enter);
            this.mskMiti.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mskMiti_KeyDown);
            this.mskMiti.Leave += new System.EventHandler(this.mskMiti_Leave);
            // 
            // TxtMasterGodown
            // 
            this.TxtMasterGodown.Location = new System.Drawing.Point(141, 48);
            this.TxtMasterGodown.Margin = new System.Windows.Forms.Padding(4);
            this.TxtMasterGodown.Name = "TxtMasterGodown";
            this.TxtMasterGodown.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.TxtMasterGodown.Properties.Appearance.Options.UseFont = true;
            this.TxtMasterGodown.Size = new System.Drawing.Size(241, 28);
            this.TxtMasterGodown.TabIndex = 3;
            this.TxtMasterGodown.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtMasterGodown_KeyDown);
            // 
            // dpDate
            // 
            this.dpDate.CalendarTitleBackColor = System.Drawing.SystemColors.Highlight;
            this.dpDate.CustomFormat = "dd/MM/yyyy";
            this.dpDate.Font = new System.Drawing.Font("Arial", 11F);
            this.dpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpDate.Location = new System.Drawing.Point(895, 11);
            this.dpDate.Margin = new System.Windows.Forms.Padding(4);
            this.dpDate.Name = "dpDate";
            this.dpDate.ShowUpDown = true;
            this.dpDate.Size = new System.Drawing.Size(163, 29);
            this.dpDate.TabIndex = 2;
            this.dpDate.Value = new System.DateTime(2017, 12, 13, 21, 58, 56, 0);
            this.dpDate.Enter += new System.EventHandler(this.dpDate_Enter);
            this.dpDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dpDate_KeyDown);
            this.dpDate.Leave += new System.EventHandler(this.dpDate_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 11F);
            this.label1.Location = new System.Drawing.Point(505, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 22);
            this.label1.TabIndex = 200;
            this.label1.Text = "Miti";
            // 
            // btnVno
            // 
            this.btnVno.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVno.Appearance.Options.UseFont = true;
            this.btnVno.Location = new System.Drawing.Point(385, 12);
            this.btnVno.Margin = new System.Windows.Forms.Padding(4);
            this.btnVno.Name = "btnVno";
            this.btnVno.Size = new System.Drawing.Size(44, 32);
            this.btnVno.TabIndex = 3;
            this.btnVno.TabStop = false;
            this.btnVno.Text = "**";
            this.btnVno.Click += new System.EventHandler(this.btnVno_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(9, 15);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(65, 21);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "FGR No";
            // 
            // txtVno
            // 
            this.txtVno.Location = new System.Drawing.Point(141, 14);
            this.txtVno.Margin = new System.Windows.Forms.Padding(4);
            this.txtVno.Name = "txtVno";
            this.txtVno.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.txtVno.Properties.Appearance.Options.UseFont = true;
            this.txtVno.Size = new System.Drawing.Size(241, 28);
            this.txtVno.TabIndex = 0;
            this.txtVno.Enter += new System.EventHandler(this.txtVno_Enter);
            this.txtVno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVno_KeyDown);
            this.txtVno.Leave += new System.EventHandler(this.txtVno_Leave);
            this.txtVno.Validating += new System.ComponentModel.CancelEventHandler(this.txtVno_Validating);
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox4.Controls.Add(this.btnRemarks);
            this.groupBox4.Controls.Add(this.btnCancel);
            this.groupBox4.Controls.Add(this.btnSave);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.lblDetTotNetAmt);
            this.groupBox4.Controls.Add(this.lbl_TotQty);
            this.groupBox4.Controls.Add(this.lbl_TotQty1);
            this.groupBox4.Controls.Add(this.txtRemarks);
            this.groupBox4.Controls.Add(this.lbl_Remarks);
            this.groupBox4.Location = new System.Drawing.Point(1, 464);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(1068, 123);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            // 
            // btnRemarks
            // 
            this.btnRemarks.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnRemarks.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.btnRemarks.Image = global::MrBLL.Properties.Resources.search16;
            this.btnRemarks.Location = new System.Drawing.Point(1024, 46);
            this.btnRemarks.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemarks.Name = "btnRemarks";
            this.btnRemarks.Size = new System.Drawing.Size(39, 34);
            this.btnRemarks.TabIndex = 249;
            this.btnRemarks.UseVisualStyleBackColor = false;
            this.btnRemarks.Click += new System.EventHandler(this.btnRemarks_Click);
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
            this.groupBox5.Location = new System.Drawing.Point(804, 140);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(264, 121);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
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
            this.gvn_GdnId,
            this.gvn_CCId,
            this.Godown,
            this.CostCenter,
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
            this.dgvPurchase.Location = new System.Drawing.Point(0, 268);
            this.dgvPurchase.Margin = new System.Windows.Forms.Padding(4);
            this.dgvPurchase.Name = "dgvPurchase";
            this.dgvPurchase.ReadOnly = true;
            this.dgvPurchase.RowHeadersVisible = false;
            this.dgvPurchase.RowHeadersWidth = 51;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvPurchase.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPurchase.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPurchase.Size = new System.Drawing.Size(1069, 197);
            this.dgvPurchase.TabIndex = 4;
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
            // gvn_GdnId
            // 
            this.gvn_GdnId.HeaderText = "GodownId";
            this.gvn_GdnId.MinimumWidth = 6;
            this.gvn_GdnId.Name = "gvn_GdnId";
            this.gvn_GdnId.ReadOnly = true;
            this.gvn_GdnId.Visible = false;
            this.gvn_GdnId.Width = 125;
            // 
            // gvn_CCId
            // 
            this.gvn_CCId.HeaderText = "CCId";
            this.gvn_CCId.MinimumWidth = 6;
            this.gvn_CCId.Name = "gvn_CCId";
            this.gvn_CCId.ReadOnly = true;
            this.gvn_CCId.Visible = false;
            this.gvn_CCId.Width = 125;
            // 
            // Godown
            // 
            this.Godown.HeaderText = "Godown";
            this.Godown.MinimumWidth = 6;
            this.Godown.Name = "Godown";
            this.Godown.ReadOnly = true;
            this.Godown.Width = 125;
            // 
            // CostCenter
            // 
            this.CostCenter.HeaderText = "CostCenter";
            this.CostCenter.MinimumWidth = 6;
            this.CostCenter.Name = "CostCenter";
            this.CostCenter.ReadOnly = true;
            this.CostCenter.Width = 125;
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.BtnCostCenter);
            this.groupBox3.Controls.Add(this.BtnGodown);
            this.groupBox3.Controls.Add(this.labelControl8);
            this.groupBox3.Controls.Add(this.TxtCostcenter);
            this.groupBox3.Controls.Add(this.labelControl7);
            this.groupBox3.Controls.Add(this.TxtGodown);
            this.groupBox3.Controls.Add(this.labelControl6);
            this.groupBox3.Controls.Add(this.txtRate);
            this.groupBox3.Controls.Add(this.lblRawQtyUnit);
            this.groupBox3.Controls.Add(this.labelControl4);
            this.groupBox3.Controls.Add(this.txtQty);
            this.groupBox3.Controls.Add(this.labelControl3);
            this.groupBox3.Controls.Add(this.txtRawMaterial);
            this.groupBox3.Location = new System.Drawing.Point(3, 143);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(800, 119);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // BtnCostCenter
            // 
            this.BtnCostCenter.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnCostCenter.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnCostCenter.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnCostCenter.Location = new System.Drawing.Point(729, 50);
            this.BtnCostCenter.Margin = new System.Windows.Forms.Padding(4);
            this.BtnCostCenter.Name = "BtnCostCenter";
            this.BtnCostCenter.Size = new System.Drawing.Size(36, 31);
            this.BtnCostCenter.TabIndex = 310;
            this.BtnCostCenter.TabStop = false;
            this.BtnCostCenter.UseVisualStyleBackColor = false;
            this.BtnCostCenter.Click += new System.EventHandler(this.BtnCostCenter_Click);
            // 
            // BtnGodown
            // 
            this.BtnGodown.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnGodown.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnGodown.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnGodown.Location = new System.Drawing.Point(321, 50);
            this.BtnGodown.Margin = new System.Windows.Forms.Padding(4);
            this.BtnGodown.Name = "BtnGodown";
            this.BtnGodown.Size = new System.Drawing.Size(36, 31);
            this.BtnGodown.TabIndex = 309;
            this.BtnGodown.TabStop = false;
            this.BtnGodown.UseVisualStyleBackColor = false;
            this.BtnGodown.Click += new System.EventHandler(this.BtnGodown_Click);
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Location = new System.Drawing.Point(361, 54);
            this.labelControl8.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(91, 21);
            this.labelControl8.TabIndex = 213;
            this.labelControl8.Text = "Costcenter";
            // 
            // TxtCostcenter
            // 
            this.TxtCostcenter.Location = new System.Drawing.Point(468, 52);
            this.TxtCostcenter.Margin = new System.Windows.Forms.Padding(4);
            this.TxtCostcenter.Name = "TxtCostcenter";
            this.TxtCostcenter.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.TxtCostcenter.Properties.Appearance.Options.UseFont = true;
            this.TxtCostcenter.Size = new System.Drawing.Size(261, 28);
            this.TxtCostcenter.TabIndex = 2;
            this.TxtCostcenter.Enter += new System.EventHandler(this.TxtCostCenter_Enter);
            this.TxtCostcenter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCostCenter_KeyDown);
            this.TxtCostcenter.Leave += new System.EventHandler(this.TxtCostCenter_Leave);
            this.TxtCostcenter.Validating += new System.ComponentModel.CancelEventHandler(this.TxtCostCenter_Validating);
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(8, 53);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(69, 21);
            this.labelControl7.TabIndex = 211;
            this.labelControl7.Text = "Godown";
            // 
            // TxtGodown
            // 
            this.TxtGodown.Location = new System.Drawing.Point(81, 50);
            this.TxtGodown.Margin = new System.Windows.Forms.Padding(4);
            this.TxtGodown.Name = "TxtGodown";
            this.TxtGodown.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.TxtGodown.Properties.Appearance.Options.UseFont = true;
            this.TxtGodown.Size = new System.Drawing.Size(240, 28);
            this.TxtGodown.TabIndex = 1;
            this.TxtGodown.Enter += new System.EventHandler(this.txtGdn_Enter);
            this.TxtGodown.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGdn_KeyDown);
            this.TxtGodown.Leave += new System.EventHandler(this.txtGdn_Leave);
            this.TxtGodown.Validating += new System.ComponentModel.CancelEventHandler(this.txtGdn_Validating);
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(505, 87);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(38, 21);
            this.labelControl6.TabIndex = 209;
            this.labelControl6.Text = "Rate";
            // 
            // txtRate
            // 
            this.txtRate.Location = new System.Drawing.Point(563, 84);
            this.txtRate.Margin = new System.Windows.Forms.Padding(4);
            this.txtRate.Name = "txtRate";
            this.txtRate.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.txtRate.Properties.Appearance.Options.UseFont = true;
            this.txtRate.Size = new System.Drawing.Size(167, 28);
            this.txtRate.TabIndex = 4;
            this.txtRate.Enter += new System.EventHandler(this.txtRate_Enter);
            this.txtRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRate_KeyPress);
            this.txtRate.Leave += new System.EventHandler(this.txtRate_Leave);
            this.txtRate.Validating += new System.ComponentModel.CancelEventHandler(this.txtRate_Validating);
            // 
            // lblRawQtyUnit
            // 
            this.lblRawQtyUnit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.lblRawQtyUnit.Appearance.Options.UseFont = true;
            this.lblRawQtyUnit.Location = new System.Drawing.Point(273, 86);
            this.lblRawQtyUnit.Margin = new System.Windows.Forms.Padding(4);
            this.lblRawQtyUnit.Name = "lblRawQtyUnit";
            this.lblRawQtyUnit.Size = new System.Drawing.Size(76, 21);
            this.lblRawQtyUnit.TabIndex = 207;
            this.lblRawQtyUnit.Text = "Qty Desc";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(15, 87);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(30, 21);
            this.labelControl4.TabIndex = 206;
            this.labelControl4.Text = "Qty";
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(83, 82);
            this.txtQty.Margin = new System.Windows.Forms.Padding(4);
            this.txtQty.Name = "txtQty";
            this.txtQty.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.txtQty.Properties.Appearance.Options.UseFont = true;
            this.txtQty.Size = new System.Drawing.Size(167, 28);
            this.txtQty.TabIndex = 3;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            this.txtQty.Validating += new System.ComponentModel.CancelEventHandler(this.txtQty_Validating);
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
            this.txtRawMaterial.Location = new System.Drawing.Point(81, 15);
            this.txtRawMaterial.Margin = new System.Windows.Forms.Padding(4);
            this.txtRawMaterial.Name = "txtRawMaterial";
            this.txtRawMaterial.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.txtRawMaterial.Properties.Appearance.Options.UseFont = true;
            this.txtRawMaterial.Size = new System.Drawing.Size(648, 28);
            this.txtRawMaterial.TabIndex = 0;
            this.txtRawMaterial.Enter += new System.EventHandler(this.txtRawMaterial_Enter);
            this.txtRawMaterial.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRawMaterial_KeyDown);
            this.txtRawMaterial.Leave += new System.EventHandler(this.txtRawMaterial_Leave);
            this.txtRawMaterial.Validating += new System.ComponentModel.CancelEventHandler(this.txtRawMaterial_Validating);
            // 
            // FrmFinishedGoodsReceived
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1073, 602);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.dgvPurchase);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox5);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFinishedGoodsReceived";
            this.ShowIcon = false;
            this.Tag = "Finished Goods Received";
            this.Text = "FinishedGoods Received";
            this.Load += new System.EventHandler(this.FrmFinishedGoodsReceived_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmFinishedGoodsReceived_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtMasterCostCenter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtMasterGodown.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVno.Properties)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchase)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtCostcenter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtGodown.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRawMaterial.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblStockQty;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblPurchaseRate;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblDetTotNetAmt;
        private System.Windows.Forms.Label lbl_TotQty;
        private System.Windows.Forms.Label lbl_TotQty1;
        private System.Windows.Forms.Label lblSalesRate;
        private System.Windows.Forms.Label lbl_Remarks;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton btnReverse;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.SimpleButton btnNew;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker mskMiti;
        private System.Windows.Forms.DateTimePicker dpDate;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnVno;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtVno;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView dgvPurchase;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Sno;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_ProductId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn gvn_GdnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn gvn_CCId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Godown;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostCenter;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_UnitId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Amount;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit TxtCostcenter;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtRate;
        private DevExpress.XtraEditors.LabelControl lblRawQtyUnit;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtQty;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtRawMaterial;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit TxtMasterCostCenter;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit TxtMasterGodown;
        private System.Windows.Forms.Button BtnCostCenter;
        private System.Windows.Forms.Button BtnGodown;
        private DevExpress.XtraEditors.TextEdit TxtGodown;
        private System.Windows.Forms.Button btnRemarks;
        private MrTextBox txtRemarks;
    }
}