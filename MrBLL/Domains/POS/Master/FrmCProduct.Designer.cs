using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.POS.Master
{
    partial class FrmCProduct
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.PnOpeningDetails = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.txtOpeningStock = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.txtOpeningAmount = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtOpeningRate = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_SalesRate = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.txt_Margin = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.txt_PurchaseRate = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.txt_Vat = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.txt_Unit = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.txt_ProductCode = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.txt_ProductGroup = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.txt_Product = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.txt_ShortName = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.Grid = new System.Windows.Forms.DataGridView();
            this.dgv_Sno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_PId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_BarCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_GrpId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Group = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_ShortName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Vat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_BuyRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Margin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SaleRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dgv_Delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.PanelHeader = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.panel5 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.panel4 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.PnlBorderHeaderTop = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.PnlBorderHeaderBottom = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.groupBox1.SuspendLayout();
            this.PnOpeningDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            this.PanelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.PnOpeningDetails);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_SalesRate);
            this.groupBox1.Controls.Add(this.txt_Margin);
            this.groupBox1.Controls.Add(this.txt_PurchaseRate);
            this.groupBox1.Controls.Add(this.txt_Vat);
            this.groupBox1.Controls.Add(this.txt_Unit);
            this.groupBox1.Controls.Add(this.txt_ProductCode);
            this.groupBox1.Controls.Add(this.txt_ProductGroup);
            this.groupBox1.Controls.Add(this.txt_Product);
            this.groupBox1.Controls.Add(this.txt_ShortName);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1005, 114);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.btnSave.Location = new System.Drawing.Point(328, 60);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 36);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "&SAVE";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // PnOpeningDetails
            // 
            this.PnOpeningDetails.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PnOpeningDetails.Controls.Add(this.label10);
            this.PnOpeningDetails.Controls.Add(this.txtOpeningStock);
            this.PnOpeningDetails.Controls.Add(this.txtOpeningAmount);
            this.PnOpeningDetails.Controls.Add(this.label12);
            this.PnOpeningDetails.Controls.Add(this.txtOpeningRate);
            this.PnOpeningDetails.Controls.Add(this.label11);
            this.PnOpeningDetails.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnOpeningDetails.Location = new System.Drawing.Point(709, 55);
            this.PnOpeningDetails.Name = "PnOpeningDetails";
            this.PnOpeningDetails.Size = new System.Drawing.Size(274, 59);
            this.PnOpeningDetails.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 68);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(145, 20);
            this.label10.TabIndex = 65;
            this.label10.Text = "Opening Amount";
            // 
            // txtOpeningStock
            // 
            this.txtOpeningStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOpeningStock.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOpeningStock.Location = new System.Drawing.Point(157, 5);
            this.txtOpeningStock.MaxLength = 50;
            this.txtOpeningStock.Name = "txtOpeningStock";
            this.txtOpeningStock.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtOpeningStock.Size = new System.Drawing.Size(110, 26);
            this.txtOpeningStock.TabIndex = 9;
            this.txtOpeningStock.Text = "0.00";
            // 
            // txtOpeningAmount
            // 
            this.txtOpeningAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOpeningAmount.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOpeningAmount.Location = new System.Drawing.Point(157, 65);
            this.txtOpeningAmount.Name = "txtOpeningAmount";
            this.txtOpeningAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtOpeningAmount.Size = new System.Drawing.Size(110, 26);
            this.txtOpeningAmount.TabIndex = 11;
            this.txtOpeningAmount.Text = "0.00";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(11, 8);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(110, 20);
            this.label12.TabIndex = 63;
            this.label12.Text = "Opening Qty";
            // 
            // txtOpeningRate
            // 
            this.txtOpeningRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOpeningRate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOpeningRate.Location = new System.Drawing.Point(157, 35);
            this.txtOpeningRate.MaxLength = 50;
            this.txtOpeningRate.Name = "txtOpeningRate";
            this.txtOpeningRate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtOpeningRate.Size = new System.Drawing.Size(110, 26);
            this.txtOpeningRate.TabIndex = 10;
            this.txtOpeningRate.Text = "0.00";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(11, 36);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(118, 20);
            this.label11.TabIndex = 64;
            this.label11.Text = "Opening Rate";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(187, 86);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 19);
            this.label9.TabIndex = 18;
            this.label9.Text = "Sales Rate";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(111, 86);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 19);
            this.label8.TabIndex = 17;
            this.label8.Text = "Margin";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(11, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 19);
            this.label7.TabIndex = 16;
            this.label7.Text = "Buy Rate";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(938, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 19);
            this.label6.TabIndex = 15;
            this.label6.Text = "VAT";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(878, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 19);
            this.label5.TabIndex = 14;
            this.label5.Text = "Unit";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(742, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 19);
            this.label4.TabIndex = 13;
            this.label4.Text = "Product Code";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(593, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 19);
            this.label3.TabIndex = 12;
            this.label3.Text = "Category";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(215, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 19);
            this.label2.TabIndex = 11;
            this.label2.Text = "Product Description";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(39, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 19);
            this.label1.TabIndex = 10;
            this.label1.Text = "BarCode";
            // 
            // txt_SalesRate
            // 
            this.txt_SalesRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_SalesRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_SalesRate.Location = new System.Drawing.Point(186, 60);
            this.txt_SalesRate.MaxLength = 25;
            this.txt_SalesRate.Name = "txt_SalesRate";
            this.txt_SalesRate.Size = new System.Drawing.Size(93, 25);
            this.txt_SalesRate.TabIndex = 8;
            this.txt_SalesRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_SalesRate.Enter += new System.EventHandler(this.txt_SalesRate_Enter);
            this.txt_SalesRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_SalesRate_KeyPress);
            this.txt_SalesRate.Leave += new System.EventHandler(this.txt_SalesRate_Leave);
            this.txt_SalesRate.Validating += new System.ComponentModel.CancelEventHandler(this.txt_SalesRate_Validating);
            // 
            // txt_Margin
            // 
            this.txt_Margin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Margin.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Margin.Location = new System.Drawing.Point(99, 60);
            this.txt_Margin.MaxLength = 25;
            this.txt_Margin.Name = "txt_Margin";
            this.txt_Margin.Size = new System.Drawing.Size(84, 25);
            this.txt_Margin.TabIndex = 7;
            this.txt_Margin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Margin.Enter += new System.EventHandler(this.TxtMargin_Enter);
            this.txt_Margin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtMargin_KeyPress);
            this.txt_Margin.Leave += new System.EventHandler(this.txt_Margin_Leave);
            this.txt_Margin.Validating += new System.ComponentModel.CancelEventHandler(this.TxtMargin_Validating);
            // 
            // txt_PurchaseRate
            // 
            this.txt_PurchaseRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_PurchaseRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_PurchaseRate.Location = new System.Drawing.Point(5, 60);
            this.txt_PurchaseRate.MaxLength = 25;
            this.txt_PurchaseRate.Name = "txt_PurchaseRate";
            this.txt_PurchaseRate.Size = new System.Drawing.Size(91, 25);
            this.txt_PurchaseRate.TabIndex = 6;
            this.txt_PurchaseRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_PurchaseRate.Enter += new System.EventHandler(this.txt_PurchaseRate_Enter);
            this.txt_PurchaseRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPurchaseRate_KeyPress);
            this.txt_PurchaseRate.Leave += new System.EventHandler(this.txt_PurchaseRate_Leave);
            this.txt_PurchaseRate.Validating += new System.ComponentModel.CancelEventHandler(this.Txt_PurchaseRate_Validating);
            // 
            // txt_Vat
            // 
            this.txt_Vat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Vat.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Vat.Location = new System.Drawing.Point(932, 11);
            this.txt_Vat.MaxLength = 50;
            this.txt_Vat.Name = "txt_Vat";
            this.txt_Vat.Size = new System.Drawing.Size(51, 25);
            this.txt_Vat.TabIndex = 5;
            this.txt_Vat.Text = "13.00";
            this.txt_Vat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Vat.Enter += new System.EventHandler(this.Txt_Vat_Enter);
            this.txt_Vat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Vat_KeyDown);
            this.txt_Vat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Vat_KeyPress);
            this.txt_Vat.Leave += new System.EventHandler(this.Txt_Vat_Leave);
            // 
            // txt_Unit
            // 
            this.txt_Unit.BackColor = System.Drawing.SystemColors.Window;
            this.txt_Unit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Unit.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Unit.Location = new System.Drawing.Point(869, 11);
            this.txt_Unit.MaxLength = 50;
            this.txt_Unit.Name = "txt_Unit";
            this.txt_Unit.ReadOnly = true;
            this.txt_Unit.Size = new System.Drawing.Size(61, 25);
            this.txt_Unit.TabIndex = 4;
            this.txt_Unit.Enter += new System.EventHandler(this.txt_Unit_Enter);
            this.txt_Unit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Unit_KeyDown);
            this.txt_Unit.Leave += new System.EventHandler(this.txt_Unit_Leave);
            this.txt_Unit.Validating += new System.ComponentModel.CancelEventHandler(this.txt_Unit_Validating);
            // 
            // txt_ProductCode
            // 
            this.txt_ProductCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ProductCode.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ProductCode.Location = new System.Drawing.Point(726, 11);
            this.txt_ProductCode.MaxLength = 50;
            this.txt_ProductCode.Name = "txt_ProductCode";
            this.txt_ProductCode.Size = new System.Drawing.Size(141, 25);
            this.txt_ProductCode.TabIndex = 3;
            this.txt_ProductCode.Enter += new System.EventHandler(this.txt_ProductCode_Enter);
            this.txt_ProductCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_ProductCode_KeyDown);
            this.txt_ProductCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_ProductCode_KeyPress);
            this.txt_ProductCode.Leave += new System.EventHandler(this.txt_ProductCode_Leave);
            this.txt_ProductCode.Validating += new System.ComponentModel.CancelEventHandler(this.txt_ProductCode_Validating);
            // 
            // txt_ProductGroup
            // 
            this.txt_ProductGroup.BackColor = System.Drawing.SystemColors.Window;
            this.txt_ProductGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ProductGroup.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ProductGroup.Location = new System.Drawing.Point(539, 11);
            this.txt_ProductGroup.MaxLength = 50;
            this.txt_ProductGroup.Name = "txt_ProductGroup";
            this.txt_ProductGroup.ReadOnly = true;
            this.txt_ProductGroup.Size = new System.Drawing.Size(183, 25);
            this.txt_ProductGroup.TabIndex = 2;
            this.txt_ProductGroup.Enter += new System.EventHandler(this.txt_ProductGroup_Enter);
            this.txt_ProductGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_ProductGroup_KeyDown);
            this.txt_ProductGroup.Leave += new System.EventHandler(this.txt_ProductGroup_Leave);
            this.txt_ProductGroup.Validating += new System.ComponentModel.CancelEventHandler(this.txt_ProductGroup_Validating);
            // 
            // txt_Product
            // 
            this.txt_Product.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Product.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Product.Location = new System.Drawing.Point(151, 11);
            this.txt_Product.MaxLength = 255;
            this.txt_Product.Name = "txt_Product";
            this.txt_Product.Size = new System.Drawing.Size(382, 25);
            this.txt_Product.TabIndex = 1;
            this.txt_Product.Enter += new System.EventHandler(this.txt_Product_Enter);
            this.txt_Product.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Product_KeyDown);
            this.txt_Product.Leave += new System.EventHandler(this.txt_Product_Leave);
            this.txt_Product.Validating += new System.ComponentModel.CancelEventHandler(this.txt_Product_Validating);
            // 
            // txt_ShortName
            // 
            this.txt_ShortName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ShortName.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ShortName.Location = new System.Drawing.Point(5, 11);
            this.txt_ShortName.MaxLength = 50;
            this.txt_ShortName.Name = "txt_ShortName";
            this.txt_ShortName.Size = new System.Drawing.Size(142, 25);
            this.txt_ShortName.TabIndex = 0;
            this.txt_ShortName.TextChanged += new System.EventHandler(this.txt_ShortName_TextChanged);
            this.txt_ShortName.Enter += new System.EventHandler(this.txt_ShortName_Enter);
            this.txt_ShortName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_ShortName_KeyDown);
            this.txt_ShortName.Leave += new System.EventHandler(this.txt_ShortName_Leave);
            this.txt_ShortName.Validating += new System.ComponentModel.CancelEventHandler(this.txt_ShortName_Validating);
            // 
            // Grid
            // 
            this.Grid.AllowUserToAddRows = false;
            this.Grid.AllowUserToResizeColumns = false;
            this.Grid.AllowUserToResizeRows = false;
            this.Grid.CausesValidation = false;
            this.Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_Sno,
            this.dgv_PId,
            this.dgv_BarCode,
            this.dgv_Description,
            this.dgv_GrpId,
            this.dgv_Group,
            this.dgv_ShortName,
            this.UnitId,
            this.dgv_Unit,
            this.dgv_Vat,
            this.dgv_BuyRate,
            this.dgv_Margin,
            this.dgv_SaleRate,
            this.dgv_Edit,
            this.dgv_Delete});
            this.Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grid.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.Grid.Location = new System.Drawing.Point(3, 117);
            this.Grid.Name = "Grid";
            this.Grid.ReadOnly = true;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grid.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.Grid.RowHeadersVisible = false;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Grid.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Grid.Size = new System.Drawing.Size(1002, 412);
            this.Grid.TabIndex = 40;
            this.Grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellContentClick);
            this.Grid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellEnter);
            this.Grid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_RowEnter);
            this.Grid.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Grid_RowHeaderMouseDoubleClick);
            this.Grid.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.Grid_UserDeletedRow);
            this.Grid.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.Grid_UserDeletingRow);
            this.Grid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Grid_KeyDown);
            // 
            // dgv_Sno
            // 
            this.dgv_Sno.HeaderText = "SNo";
            this.dgv_Sno.Name = "dgv_Sno";
            this.dgv_Sno.ReadOnly = true;
            this.dgv_Sno.Width = 40;
            // 
            // dgv_PId
            // 
            this.dgv_PId.HeaderText = "ProductId";
            this.dgv_PId.Name = "dgv_PId";
            this.dgv_PId.ReadOnly = true;
            this.dgv_PId.Visible = false;
            // 
            // dgv_BarCode
            // 
            this.dgv_BarCode.HeaderText = "BarCode";
            this.dgv_BarCode.Name = "dgv_BarCode";
            this.dgv_BarCode.ReadOnly = true;
            this.dgv_BarCode.Width = 120;
            // 
            // dgv_Description
            // 
            this.dgv_Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgv_Description.HeaderText = "Description";
            this.dgv_Description.Name = "dgv_Description";
            this.dgv_Description.ReadOnly = true;
            // 
            // dgv_GrpId
            // 
            this.dgv_GrpId.HeaderText = "GrpId";
            this.dgv_GrpId.Name = "dgv_GrpId";
            this.dgv_GrpId.ReadOnly = true;
            this.dgv_GrpId.Visible = false;
            this.dgv_GrpId.Width = 5;
            // 
            // dgv_Group
            // 
            this.dgv_Group.HeaderText = "Category";
            this.dgv_Group.Name = "dgv_Group";
            this.dgv_Group.ReadOnly = true;
            this.dgv_Group.Width = 150;
            // 
            // dgv_ShortName
            // 
            this.dgv_ShortName.HeaderText = "ShortName";
            this.dgv_ShortName.Name = "dgv_ShortName";
            this.dgv_ShortName.ReadOnly = true;
            this.dgv_ShortName.Width = 120;
            // 
            // UnitId
            // 
            this.UnitId.HeaderText = "UnitId";
            this.UnitId.Name = "UnitId";
            this.UnitId.ReadOnly = true;
            this.UnitId.Visible = false;
            // 
            // dgv_Unit
            // 
            this.dgv_Unit.HeaderText = "Unit";
            this.dgv_Unit.Name = "dgv_Unit";
            this.dgv_Unit.ReadOnly = true;
            this.dgv_Unit.Width = 75;
            // 
            // dgv_Vat
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgv_Vat.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgv_Vat.HeaderText = "VAT";
            this.dgv_Vat.Name = "dgv_Vat";
            this.dgv_Vat.ReadOnly = true;
            this.dgv_Vat.Width = 60;
            // 
            // dgv_BuyRate
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgv_BuyRate.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgv_BuyRate.HeaderText = "Buy Rate";
            this.dgv_BuyRate.Name = "dgv_BuyRate";
            this.dgv_BuyRate.ReadOnly = true;
            // 
            // dgv_Margin
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgv_Margin.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgv_Margin.HeaderText = "Margin";
            this.dgv_Margin.Name = "dgv_Margin";
            this.dgv_Margin.ReadOnly = true;
            this.dgv_Margin.Width = 80;
            // 
            // dgv_SaleRate
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgv_SaleRate.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgv_SaleRate.HeaderText = "Sales Rate";
            this.dgv_SaleRate.Name = "dgv_SaleRate";
            this.dgv_SaleRate.ReadOnly = true;
            this.dgv_SaleRate.Width = 120;
            // 
            // dgv_Edit
            // 
            this.dgv_Edit.HeaderText = "Edit";
            this.dgv_Edit.Name = "dgv_Edit";
            this.dgv_Edit.ReadOnly = true;
            this.dgv_Edit.Visible = false;
            this.dgv_Edit.Width = 60;
            // 
            // dgv_Delete
            // 
            this.dgv_Delete.HeaderText = "Delete";
            this.dgv_Delete.Name = "dgv_Delete";
            this.dgv_Delete.ReadOnly = true;
            this.dgv_Delete.Visible = false;
            this.dgv_Delete.Width = 70;
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.Grid);
            this.PanelHeader.Controls.Add(this.panel5);
            this.PanelHeader.Controls.Add(this.groupBox1);
            this.PanelHeader.Controls.Add(this.panel4);
            this.PanelHeader.Controls.Add(this.PnlBorderHeaderTop);
            this.PanelHeader.Controls.Add(this.PnlBorderHeaderBottom);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(1008, 532);
            this.PanelHeader.TabIndex = 41;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(0, 117);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(3, 412);
            this.panel5.TabIndex = 10;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(1005, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(3, 526);
            this.panel4.TabIndex = 9;
            // 
            // PnlBorderHeaderTop
            // 
            this.PnlBorderHeaderTop.BackColor = System.Drawing.Color.White;
            this.PnlBorderHeaderTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlBorderHeaderTop.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlBorderHeaderTop.Location = new System.Drawing.Point(0, 0);
            this.PnlBorderHeaderTop.Name = "PnlBorderHeaderTop";
            this.PnlBorderHeaderTop.Size = new System.Drawing.Size(1008, 3);
            this.PnlBorderHeaderTop.TabIndex = 8;
            // 
            // PnlBorderHeaderBottom
            // 
            this.PnlBorderHeaderBottom.BackColor = System.Drawing.Color.White;
            this.PnlBorderHeaderBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PnlBorderHeaderBottom.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlBorderHeaderBottom.Location = new System.Drawing.Point(0, 529);
            this.PnlBorderHeaderBottom.Name = "PnlBorderHeaderBottom";
            this.PnlBorderHeaderBottom.Size = new System.Drawing.Size(1008, 3);
            this.PnlBorderHeaderBottom.TabIndex = 0;
            // 
            // FrmCProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1008, 532);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCProduct";
            this.ShowIcon = false;
            this.Text = "Counter Product Details";
            this.Load += new System.EventHandler(this.FrmCounterProduct_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCounterProduct_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmCounterProduct_KeyPress);
            this.Resize += new System.EventHandler(this.FrmCounterProduct_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.PnOpeningDetails.ResumeLayout(false);
            this.PnOpeningDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            this.PanelHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView Grid;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Sno;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_PId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_BarCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_GrpId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Group;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_ShortName;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Vat;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_BuyRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Margin;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SaleRate;
        private System.Windows.Forms.DataGridViewButtonColumn dgv_Edit;
        private System.Windows.Forms.DataGridViewButtonColumn dgv_Delete;
        private MrTextBox txt_ShortName;
        private MrTextBox txt_Product;
        private MrTextBox txt_ProductCode;
        private MrTextBox txt_ProductGroup;
        private MrTextBox txt_Unit;
        private MrTextBox txt_Vat;
        private MrTextBox txt_Margin;
        private MrTextBox txt_PurchaseRate;
        private MrTextBox txt_SalesRate;
        private MrPanel PnOpeningDetails;
        private MrTextBox txtOpeningAmount;
        private MrTextBox txtOpeningRate;
        private MrTextBox txtOpeningStock;
        private MrPanel PanelHeader;
        private MrPanel panel5;
        private MrPanel panel4;
        private MrPanel PnlBorderHeaderTop;
        private MrPanel PnlBorderHeaderBottom;
    }
}