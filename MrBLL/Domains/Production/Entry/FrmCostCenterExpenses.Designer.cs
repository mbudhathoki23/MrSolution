using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.Production.Entry
{
    partial class FrmCostCenterExpenses
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnRemarks = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.lblDetTotNetAmt = new System.Windows.Forms.Label();
            this.lbl_NoInWordsDetl = new System.Windows.Forms.Label();
            this.lbl_NoInWords = new System.Windows.Forms.Label();
            this.txtRemarks = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_Remarks = new System.Windows.Forms.Label();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnReverse = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnNew = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btnSalesOrder = new DevExpress.XtraEditors.SimpleButton();
            this.txtSalesOrder = new DevExpress.XtraEditors.TextEdit();
            this.txtProduct = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.label2 = new System.Windows.Forms.Label();
            this.mskMiti = new System.Windows.Forms.DateTimePicker();
            this.dpDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnVno = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtVno = new DevExpress.XtraEditors.TextEdit();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtRate = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtCostCenter = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtLedger = new DevExpress.XtraEditors.TextEdit();
            this.dgvPurchase = new System.Windows.Forms.DataGridView();
            this.dgv_Sno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_LedgerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_CCId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_CostCenter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Narration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_OrderNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_OrderSNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSalesOrder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProduct.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVno.Properties)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCostCenter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLedger.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchase)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnRemarks);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.lblDetTotNetAmt);
            this.groupBox4.Controls.Add(this.lbl_NoInWordsDetl);
            this.groupBox4.Controls.Add(this.lbl_NoInWords);
            this.groupBox4.Controls.Add(this.txtRemarks);
            this.groupBox4.Controls.Add(this.lbl_Remarks);
            this.groupBox4.Location = new System.Drawing.Point(1, 405);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Size = new System.Drawing.Size(848, 119);
            this.groupBox4.TabIndex = 308;
            this.groupBox4.TabStop = false;
            // 
            // btnRemarks
            // 
            this.btnRemarks.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnRemarks.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.btnRemarks.Image = global::MrBLL.Properties.Resources.search16;
            this.btnRemarks.Location = new System.Drawing.Point(799, 78);
            this.btnRemarks.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRemarks.Name = "btnRemarks";
            this.btnRemarks.Size = new System.Drawing.Size(37, 30);
            this.btnRemarks.TabIndex = 246;
            this.btnRemarks.UseVisualStyleBackColor = false;
            this.btnRemarks.Click += new System.EventHandler(this.btnRemarks_Click);
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
            // lbl_NoInWordsDetl
            // 
            this.lbl_NoInWordsDetl.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
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
            // txtRemarks
            // 
            this.txtRemarks.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(144, 78);
            this.txtRemarks.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRemarks.MaxLength = 255;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(653, 29);
            this.txtRemarks.TabIndex = 14;
            this.txtRemarks.Enter += new System.EventHandler(this.txtRemarks_Enter);
            this.txtRemarks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemarks_KeyDown);
            this.txtRemarks.Leave += new System.EventHandler(this.txtRemarks_Leave);
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
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.btnCancel.Location = new System.Drawing.Point(705, 532);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(144, 44);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "&CANCEL";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.btnSave.Location = new System.Drawing.Point(553, 532);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(144, 44);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "&SAVE";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(843, 57);
            this.groupBox1.TabIndex = 303;
            this.groupBox1.TabStop = false;
            // 
            // btnReverse
            // 
            this.btnReverse.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReverse.Appearance.Options.UseFont = true;
            this.btnReverse.ImageOptions.Image = global::MrBLL.Properties.Resources.Reverse;
            this.btnReverse.Location = new System.Drawing.Point(464, 12);
            this.btnReverse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReverse.Name = "btnReverse";
            this.btnReverse.Size = new System.Drawing.Size(149, 37);
            this.btnReverse.TabIndex = 3;
            this.btnReverse.Text = "&REVERSE";
            this.btnReverse.Click += new System.EventHandler(this.btnReverse_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Appearance.Options.UseFont = true;
            this.btnPrint.ImageOptions.Image = global::MrBLL.Properties.Resources.Printerview;
            this.btnPrint.Location = new System.Drawing.Point(620, 12);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(109, 37);
            this.btnPrint.TabIndex = 4;
            this.btnPrint.Text = "&PRINT";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExit
            // 
            this.btnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.btnExit.Location = new System.Drawing.Point(737, 12);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(97, 37);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "E&XIT";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Appearance.Options.UseFont = true;
            this.btnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.btnDelete.Location = new System.Drawing.Point(229, 11);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(141, 37);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Tag = "DeleteButtonCheck";
            this.btnDelete.Text = "&DELETE";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Appearance.Options.UseFont = true;
            this.btnEdit.ImageOptions.Image = global::MrBLL.Properties.Resources.Edit;
            this.btnEdit.Location = new System.Drawing.Point(119, 12);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(103, 37);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Tag = "EditButtonCheck";
            this.btnEdit.Text = "&EDIT";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnNew
            // 
            this.btnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Appearance.Options.UseFont = true;
            this.btnNew.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.btnNew.Location = new System.Drawing.Point(9, 11);
            this.btnNew.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(100, 37);
            this.btnNew.TabIndex = 0;
            this.btnNew.Tag = "NewButtonCheck";
            this.btnNew.Text = "&NEW";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelControl5);
            this.groupBox2.Controls.Add(this.btnSalesOrder);
            this.groupBox2.Controls.Add(this.txtSalesOrder);
            this.groupBox2.Controls.Add(this.txtProduct);
            this.groupBox2.Controls.Add(this.labelControl2);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.mskMiti);
            this.groupBox2.Controls.Add(this.dpDate);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnVno);
            this.groupBox2.Controls.Add(this.labelControl1);
            this.groupBox2.Controls.Add(this.txtVno);
            this.groupBox2.Location = new System.Drawing.Point(3, 47);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(843, 84);
            this.groupBox2.TabIndex = 304;
            this.groupBox2.TabStop = false;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(337, 16);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(82, 21);
            this.labelControl5.TabIndex = 205;
            this.labelControl5.Text = "Order No.";
            // 
            // btnSalesOrder
            // 
            this.btnSalesOrder.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalesOrder.Appearance.Options.UseFont = true;
            this.btnSalesOrder.Location = new System.Drawing.Point(564, 11);
            this.btnSalesOrder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSalesOrder.Name = "btnSalesOrder";
            this.btnSalesOrder.Size = new System.Drawing.Size(44, 32);
            this.btnSalesOrder.TabIndex = 204;
            this.btnSalesOrder.Text = "**";
            this.btnSalesOrder.Click += new System.EventHandler(this.btnSalesOrder_Click);
            // 
            // txtSalesOrder
            // 
            this.txtSalesOrder.Location = new System.Drawing.Point(435, 14);
            this.txtSalesOrder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSalesOrder.Name = "txtSalesOrder";
            this.txtSalesOrder.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.txtSalesOrder.Properties.Appearance.Options.UseFont = true;
            this.txtSalesOrder.Size = new System.Drawing.Size(172, 28);
            this.txtSalesOrder.TabIndex = 7;
            this.txtSalesOrder.Enter += new System.EventHandler(this.txtSalesOrder_Enter);
            this.txtSalesOrder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSalesOrder_KeyDown);
            this.txtSalesOrder.Leave += new System.EventHandler(this.txtSalesOrder_Leave);
            this.txtSalesOrder.Validating += new System.ComponentModel.CancelEventHandler(this.txtSalesOrder_Validating);
            // 
            // txtProduct
            // 
            this.txtProduct.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtProduct.Location = new System.Drawing.Point(141, 47);
            this.txtProduct.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.txtProduct.Properties.Appearance.Options.UseFont = true;
            this.txtProduct.Size = new System.Drawing.Size(467, 28);
            this.txtProduct.TabIndex = 10;
            this.txtProduct.Enter += new System.EventHandler(this.txtProduct_Enter);
            this.txtProduct.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProduct_KeyDown);
            this.txtProduct.Leave += new System.EventHandler(this.txtProduct_Leave);
            this.txtProduct.Validating += new System.ComponentModel.CancelEventHandler(this.txtProduct_Validating);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(9, 44);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(123, 21);
            this.labelControl2.TabIndex = 202;
            this.labelControl2.Text = "Finished Good";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 11F);
            this.label2.Location = new System.Drawing.Point(609, 47);
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
            this.mskMiti.Location = new System.Drawing.Point(669, 12);
            this.mskMiti.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mskMiti.Name = "mskMiti";
            this.mskMiti.ShowUpDown = true;
            this.mskMiti.Size = new System.Drawing.Size(168, 29);
            this.mskMiti.TabIndex = 8;
            this.mskMiti.Value = new System.DateTime(2017, 12, 13, 21, 58, 56, 0);
            this.mskMiti.Enter += new System.EventHandler(this.mskMiti_Enter);
            this.mskMiti.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mskMiti_KeyDown);
            this.mskMiti.Leave += new System.EventHandler(this.mskMiti_Leave);
            // 
            // dpDate
            // 
            this.dpDate.CalendarTitleBackColor = System.Drawing.SystemColors.Highlight;
            this.dpDate.CustomFormat = "dd/MM/yyyy";
            this.dpDate.Font = new System.Drawing.Font("Arial", 11F);
            this.dpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpDate.Location = new System.Drawing.Point(669, 47);
            this.dpDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dpDate.Name = "dpDate";
            this.dpDate.ShowUpDown = true;
            this.dpDate.Size = new System.Drawing.Size(168, 29);
            this.dpDate.TabIndex = 9;
            this.dpDate.Value = new System.DateTime(2017, 12, 13, 21, 58, 56, 0);
            this.dpDate.Enter += new System.EventHandler(this.dpDate_Enter);
            this.dpDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dpDate_KeyDown);
            this.dpDate.Leave += new System.EventHandler(this.dpDate_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 11F);
            this.label1.Location = new System.Drawing.Point(611, 17);
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
            this.btnVno.Location = new System.Drawing.Point(285, 12);
            this.btnVno.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnVno.Name = "btnVno";
            this.btnVno.Size = new System.Drawing.Size(44, 32);
            this.btnVno.TabIndex = 3;
            this.btnVno.Text = "**";
            this.btnVno.Click += new System.EventHandler(this.btnVno_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(9, 15);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(96, 21);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Costing No.";
            // 
            // txtVno
            // 
            this.txtVno.Location = new System.Drawing.Point(141, 14);
            this.txtVno.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtVno.Name = "txtVno";
            this.txtVno.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.txtVno.Properties.Appearance.Options.UseFont = true;
            this.txtVno.Size = new System.Drawing.Size(176, 28);
            this.txtVno.TabIndex = 6;
            this.txtVno.Enter += new System.EventHandler(this.txtVno_Enter);
            this.txtVno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVno_KeyDown);
            this.txtVno.Leave += new System.EventHandler(this.txtVno_Leave);
            this.txtVno.Validating += new System.ComponentModel.CancelEventHandler(this.txtVno_Validating);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.labelControl6);
            this.groupBox3.Controls.Add(this.txtRate);
            this.groupBox3.Controls.Add(this.labelControl4);
            this.groupBox3.Controls.Add(this.txtCostCenter);
            this.groupBox3.Controls.Add(this.labelControl3);
            this.groupBox3.Controls.Add(this.txtLedger);
            this.groupBox3.Location = new System.Drawing.Point(3, 124);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(843, 85);
            this.groupBox3.TabIndex = 305;
            this.groupBox3.TabStop = false;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(436, 52);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(38, 21);
            this.labelControl6.TabIndex = 209;
            this.labelControl6.Text = "Rate";
            // 
            // txtRate
            // 
            this.txtRate.Location = new System.Drawing.Point(484, 49);
            this.txtRate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRate.Name = "txtRate";
            this.txtRate.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.txtRate.Properties.Appearance.Options.UseFont = true;
            this.txtRate.Size = new System.Drawing.Size(177, 28);
            this.txtRate.TabIndex = 13;
            this.txtRate.Enter += new System.EventHandler(this.txtRate_Enter);
            this.txtRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRate_KeyPress);
            this.txtRate.Leave += new System.EventHandler(this.txtRate_Leave);
            this.txtRate.Validating += new System.ComponentModel.CancelEventHandler(this.txtRate_Validating);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(15, 53);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(95, 21);
            this.labelControl4.TabIndex = 206;
            this.labelControl4.Text = "CostCenter";
            // 
            // txtCostCenter
            // 
            this.txtCostCenter.Location = new System.Drawing.Point(119, 48);
            this.txtCostCenter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCostCenter.Name = "txtCostCenter";
            this.txtCostCenter.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.txtCostCenter.Properties.Appearance.Options.UseFont = true;
            this.txtCostCenter.Size = new System.Drawing.Size(309, 28);
            this.txtCostCenter.TabIndex = 12;
            this.txtCostCenter.Enter += new System.EventHandler(this.txtCostCenter_Enter);
            this.txtCostCenter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCostCenter_KeyDown);
            this.txtCostCenter.Leave += new System.EventHandler(this.txtCostCenter_Leave);
            this.txtCostCenter.Validating += new System.ComponentModel.CancelEventHandler(this.txtCostCenter_Validating);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(15, 22);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(57, 21);
            this.labelControl3.TabIndex = 204;
            this.labelControl3.Text = "Ledger";
            // 
            // txtLedger
            // 
            this.txtLedger.Location = new System.Drawing.Point(119, 15);
            this.txtLedger.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtLedger.Name = "txtLedger";
            this.txtLedger.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.txtLedger.Properties.Appearance.Options.UseFont = true;
            this.txtLedger.Size = new System.Drawing.Size(543, 28);
            this.txtLedger.TabIndex = 11;
            this.txtLedger.Enter += new System.EventHandler(this.txtLedger_Enter);
            this.txtLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLedger_KeyDown);
            this.txtLedger.Leave += new System.EventHandler(this.txtLedger_Leave);
            this.txtLedger.Validating += new System.ComponentModel.CancelEventHandler(this.txtLedger_Validating);
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
            this.dgv_LedgerId,
            this.dgv_Description,
            this.dgv_CCId,
            this.dgv_CostCenter,
            this.dgv_Rate,
            this.dgv_Amount,
            this.dgv_Narration,
            this.dgv_OrderNo,
            this.dgv_OrderSNo});
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
            this.dgvPurchase.Location = new System.Drawing.Point(3, 213);
            this.dgvPurchase.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvPurchase.Name = "dgvPurchase";
            this.dgvPurchase.ReadOnly = true;
            this.dgvPurchase.RowHeadersVisible = false;
            this.dgvPurchase.RowHeadersWidth = 51;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvPurchase.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPurchase.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPurchase.Size = new System.Drawing.Size(844, 197);
            this.dgvPurchase.TabIndex = 306;
            // 
            // dgv_Sno
            // 
            this.dgv_Sno.HeaderText = "Sno";
            this.dgv_Sno.MinimumWidth = 6;
            this.dgv_Sno.Name = "dgv_Sno";
            this.dgv_Sno.ReadOnly = true;
            this.dgv_Sno.Width = 45;
            // 
            // dgv_LedgerId
            // 
            this.dgv_LedgerId.HeaderText = "ProductId";
            this.dgv_LedgerId.MinimumWidth = 6;
            this.dgv_LedgerId.Name = "dgv_LedgerId";
            this.dgv_LedgerId.ReadOnly = true;
            this.dgv_LedgerId.Visible = false;
            this.dgv_LedgerId.Width = 150;
            // 
            // dgv_Description
            // 
            this.dgv_Description.HeaderText = "Description";
            this.dgv_Description.MinimumWidth = 6;
            this.dgv_Description.Name = "dgv_Description";
            this.dgv_Description.ReadOnly = true;
            this.dgv_Description.Width = 250;
            // 
            // dgv_CCId
            // 
            this.dgv_CCId.HeaderText = "CID";
            this.dgv_CCId.MinimumWidth = 6;
            this.dgv_CCId.Name = "dgv_CCId";
            this.dgv_CCId.ReadOnly = true;
            this.dgv_CCId.Visible = false;
            this.dgv_CCId.Width = 125;
            // 
            // dgv_CostCenter
            // 
            this.dgv_CostCenter.HeaderText = "CostCenter";
            this.dgv_CostCenter.MinimumWidth = 6;
            this.dgv_CostCenter.Name = "dgv_CostCenter";
            this.dgv_CostCenter.ReadOnly = true;
            this.dgv_CostCenter.Width = 150;
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
            this.dgv_Amount.Width = 80;
            // 
            // dgv_Narration
            // 
            this.dgv_Narration.HeaderText = "Narration";
            this.dgv_Narration.MinimumWidth = 6;
            this.dgv_Narration.Name = "dgv_Narration";
            this.dgv_Narration.ReadOnly = true;
            this.dgv_Narration.Visible = false;
            this.dgv_Narration.Width = 125;
            // 
            // dgv_OrderNo
            // 
            this.dgv_OrderNo.HeaderText = "OrderNo";
            this.dgv_OrderNo.MinimumWidth = 6;
            this.dgv_OrderNo.Name = "dgv_OrderNo";
            this.dgv_OrderNo.ReadOnly = true;
            this.dgv_OrderNo.Visible = false;
            this.dgv_OrderNo.Width = 125;
            // 
            // dgv_OrderSNo
            // 
            this.dgv_OrderSNo.HeaderText = "OrderSNo";
            this.dgv_OrderSNo.MinimumWidth = 6;
            this.dgv_OrderSNo.Name = "dgv_OrderSNo";
            this.dgv_OrderSNo.ReadOnly = true;
            this.dgv_OrderSNo.Visible = false;
            this.dgv_OrderSNo.Width = 125;
            // 
            // FrmCostCenterExpenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(852, 582);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.dgvPurchase);
            this.Controls.Add(this.groupBox4);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCostCenterExpenses";
            this.ShowIcon = false;
            this.Tag = "CostCenter Expenses";
            this.Text = "CostCenter Expenses";
            this.Load += new System.EventHandler(this.FrmCostCenterExpenses_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmCostCenterExpenses_KeyPress);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSalesOrder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProduct.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVno.Properties)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCostCenter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLedger.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchase)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox4;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblDetTotNetAmt;
        private System.Windows.Forms.Label lbl_NoInWordsDetl;
        private System.Windows.Forms.Label lbl_NoInWords;
        private System.Windows.Forms.Label lbl_Remarks;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton btnReverse;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.SimpleButton btnNew;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.TextEdit txtProduct;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker mskMiti;
        private System.Windows.Forms.DateTimePicker dpDate;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnVno;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtVno;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtRate;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtCostCenter;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtLedger;
        private System.Windows.Forms.DataGridView dgvPurchase;
        private DevExpress.XtraEditors.TextEdit txtSalesOrder;
        private DevExpress.XtraEditors.SimpleButton btnSalesOrder;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Sno;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_LedgerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_CCId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_CostCenter;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Narration;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_OrderNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_OrderSNo;
        private System.Windows.Forms.Button btnRemarks;
        private MrTextBox txtRemarks;
    }
}