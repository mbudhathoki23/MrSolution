using DevExpress.XtraEditors;
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.POS.Master
{
	partial class FrmPosProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPosProduct));
            this.TabPOS = new DevExpress.XtraTab.XtraTabControl();
            this.tbProduct = new DevExpress.XtraTab.XtraTabPage();
            this.label16 = new System.Windows.Forms.Label();
            this.TxtHsCode = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtAltSalesRate = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtSalesRate = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.TxtBarcode = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtDescription = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtCategory = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnAltUnit = new System.Windows.Forms.Button();
            this.TxtBarcode2 = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtBarcode1 = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.TxtUnit = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnUnit = new System.Windows.Forms.Button();
            this.BtnGroup = new System.Windows.Forms.Button();
            this.TxtMRP = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnBarcode1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.BtnSubGroup = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.BtnBarcode = new System.Windows.Forms.Button();
            this.TxtAltUnit = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtSubCategory = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtMargin = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnDescription = new System.Windows.Forms.Button();
            this.TxtAltQty = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.ChkIsTaxable = new DevExpress.XtraEditors.CheckEdit();
            this.label9 = new System.Windows.Forms.Label();
            this.TxtBuyRate = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtQty = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tbImage = new DevExpress.XtraTab.XtraTabPage();
            this.ChkBatchWise = new System.Windows.Forms.CheckBox();
            this.LinkAttachment1 = new System.Windows.Forms.LinkLabel();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.ChkBarcodeList = new DevExpress.XtraEditors.CheckEdit();
            this.ChkPriceUpdate = new DevExpress.XtraEditors.CheckEdit();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.ChkActive = new System.Windows.Forms.CheckBox();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.TxtTaxRate = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label17 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.TabPOS)).BeginInit();
            this.TabPOS.SuspendLayout();
            this.tbProduct.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChkIsTaxable.Properties)).BeginInit();
            this.tbImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkBarcodeList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkPriceUpdate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // TabPOS
            // 
            this.TabPOS.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.TabPOS.Appearance.Options.UseFont = true;
            this.TabPOS.AppearancePage.Header.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.TabPOS.AppearancePage.Header.Options.UseFont = true;
            this.TabPOS.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.TabPOS.AppearancePage.HeaderActive.Options.UseFont = true;
            this.TabPOS.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.TabPOS.AppearancePage.HeaderDisabled.Options.UseFont = true;
            this.TabPOS.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.TabPOS.AppearancePage.HeaderHotTracked.Options.UseFont = true;
            this.TabPOS.AppearancePage.PageClient.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.TabPOS.AppearancePage.PageClient.Options.UseFont = true;
            this.TabPOS.Location = new System.Drawing.Point(0, 0);
            this.TabPOS.Name = "TabPOS";
            this.TabPOS.SelectedTabPage = this.tbProduct;
            this.TabPOS.Size = new System.Drawing.Size(598, 324);
            this.TabPOS.TabIndex = 0;
            this.TabPOS.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tbProduct,
            this.tbImage});
            // 
            // tbProduct
            // 
            this.tbProduct.Appearance.Header.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tbProduct.Appearance.Header.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.tbProduct.Appearance.Header.Options.UseBackColor = true;
            this.tbProduct.Appearance.Header.Options.UseFont = true;
            this.tbProduct.Appearance.HeaderActive.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tbProduct.Appearance.HeaderActive.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.tbProduct.Appearance.HeaderActive.Options.UseBackColor = true;
            this.tbProduct.Appearance.HeaderActive.Options.UseFont = true;
            this.tbProduct.Appearance.HeaderDisabled.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tbProduct.Appearance.HeaderDisabled.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.tbProduct.Appearance.HeaderDisabled.Options.UseBackColor = true;
            this.tbProduct.Appearance.HeaderDisabled.Options.UseFont = true;
            this.tbProduct.Appearance.HeaderHotTracked.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tbProduct.Appearance.HeaderHotTracked.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.tbProduct.Appearance.HeaderHotTracked.Options.UseBackColor = true;
            this.tbProduct.Appearance.HeaderHotTracked.Options.UseFont = true;
            this.tbProduct.Appearance.PageClient.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tbProduct.Appearance.PageClient.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.tbProduct.Appearance.PageClient.Options.UseBackColor = true;
            this.tbProduct.Appearance.PageClient.Options.UseFont = true;
            this.tbProduct.Controls.Add(this.label17);
            this.tbProduct.Controls.Add(this.TxtTaxRate);
            this.tbProduct.Controls.Add(this.label16);
            this.tbProduct.Controls.Add(this.TxtHsCode);
            this.tbProduct.Controls.Add(this.TxtAltSalesRate);
            this.tbProduct.Controls.Add(this.label15);
            this.tbProduct.Controls.Add(this.label6);
            this.tbProduct.Controls.Add(this.label1);
            this.tbProduct.Controls.Add(this.label2);
            this.tbProduct.Controls.Add(this.label3);
            this.tbProduct.Controls.Add(this.label4);
            this.tbProduct.Controls.Add(this.TxtSalesRate);
            this.tbProduct.Controls.Add(this.label10);
            this.tbProduct.Controls.Add(this.TxtBarcode);
            this.tbProduct.Controls.Add(this.label5);
            this.tbProduct.Controls.Add(this.TxtDescription);
            this.tbProduct.Controls.Add(this.TxtCategory);
            this.tbProduct.Controls.Add(this.BtnAltUnit);
            this.tbProduct.Controls.Add(this.TxtBarcode2);
            this.tbProduct.Controls.Add(this.TxtBarcode1);
            this.tbProduct.Controls.Add(this.label13);
            this.tbProduct.Controls.Add(this.TxtUnit);
            this.tbProduct.Controls.Add(this.BtnUnit);
            this.tbProduct.Controls.Add(this.BtnGroup);
            this.tbProduct.Controls.Add(this.TxtMRP);
            this.tbProduct.Controls.Add(this.BtnBarcode1);
            this.tbProduct.Controls.Add(this.label7);
            this.tbProduct.Controls.Add(this.BtnSubGroup);
            this.tbProduct.Controls.Add(this.label11);
            this.tbProduct.Controls.Add(this.BtnBarcode);
            this.tbProduct.Controls.Add(this.TxtAltUnit);
            this.tbProduct.Controls.Add(this.TxtSubCategory);
            this.tbProduct.Controls.Add(this.TxtMargin);
            this.tbProduct.Controls.Add(this.BtnDescription);
            this.tbProduct.Controls.Add(this.TxtAltQty);
            this.tbProduct.Controls.Add(this.label12);
            this.tbProduct.Controls.Add(this.ChkIsTaxable);
            this.tbProduct.Controls.Add(this.label9);
            this.tbProduct.Controls.Add(this.TxtBuyRate);
            this.tbProduct.Controls.Add(this.TxtQty);
            this.tbProduct.Controls.Add(this.label8);
            this.tbProduct.Controls.Add(this.label14);
            this.tbProduct.Name = "tbProduct";
            this.tbProduct.Size = new System.Drawing.Size(596, 293);
            this.tbProduct.Text = "Product Details";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(331, 45);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(71, 19);
            this.label16.TabIndex = 67;
            this.label16.Text = "Hs Code";
            // 
            // TxtHsCode
            // 
            this.TxtHsCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtHsCode.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtHsCode.Location = new System.Drawing.Point(415, 42);
            this.TxtHsCode.Name = "TxtHsCode";
            this.TxtHsCode.Size = new System.Drawing.Size(140, 25);
            this.TxtHsCode.TabIndex = 3;
            this.TxtHsCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtProductId_KeyPress);
            // 
            // TxtAltSalesRate
            // 
            this.TxtAltSalesRate.AcceptsReturn = true;
            this.TxtAltSalesRate.AcceptsTab = true;
            this.TxtAltSalesRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAltSalesRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAltSalesRate.Location = new System.Drawing.Point(393, 263);
            this.TxtAltSalesRate.Name = "TxtAltSalesRate";
            this.TxtAltSalesRate.Size = new System.Drawing.Size(161, 25);
            this.TxtAltSalesRate.TabIndex = 17;
            this.TxtAltSalesRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtAltSalesRate.TextBoxType = MrDAL.Control.ControlsEx.Control.TextBoxType.Decimal;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(273, 266);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(116, 19);
            this.label15.TabIndex = 65;
            this.label15.Text = "Alt Sales Rate";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(309, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 19);
            this.label6.TabIndex = 62;
            this.label6.Text = "Alt Unit";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 19);
            this.label1.TabIndex = 10;
            this.label1.Text = "BarCode";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 19);
            this.label2.TabIndex = 11;
            this.label2.Text = "Description";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 19);
            this.label3.TabIndex = 12;
            this.label3.Text = "Category";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 19);
            this.label4.TabIndex = 13;
            this.label4.Text = "ShortName";
            // 
            // TxtSalesRate
            // 
            this.TxtSalesRate.AcceptsReturn = true;
            this.TxtSalesRate.AcceptsTab = true;
            this.TxtSalesRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSalesRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSalesRate.Location = new System.Drawing.Point(110, 262);
            this.TxtSalesRate.Name = "TxtSalesRate";
            this.TxtSalesRate.Size = new System.Drawing.Size(161, 25);
            this.TxtSalesRate.TabIndex = 16;
            this.TxtSalesRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtSalesRate.TextBoxType = MrDAL.Control.ControlsEx.Control.TextBoxType.Decimal;
            this.TxtSalesRate.TextChanged += new System.EventHandler(this.TxtSalesRate_TextChanged);
            this.TxtSalesRate.Enter += new System.EventHandler(this.Ctrl_Enter);
            this.TxtSalesRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSalesRate_KeyDown);
            this.TxtSalesRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtSalesRate_KeyPress);
            this.TxtSalesRate.Leave += new System.EventHandler(this.Ctrl_Leave);
            this.TxtSalesRate.Validating += new System.ComponentModel.CancelEventHandler(this.TxtSalesRate_Validating);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(331, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 19);
            this.label10.TabIndex = 22;
            this.label10.Text = "BarCode1";
            // 
            // TxtBarcode
            // 
            this.TxtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBarcode.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBarcode.Location = new System.Drawing.Point(109, 14);
            this.TxtBarcode.Name = "TxtBarcode";
            this.TxtBarcode.Size = new System.Drawing.Size(175, 25);
            this.TxtBarcode.TabIndex = 0;
            this.TxtBarcode.Enter += new System.EventHandler(this.Ctrl_Enter);
            this.TxtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBarcode_KeyDown);
            this.TxtBarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBarcode_KeyPress);
            this.TxtBarcode.Validating += new System.ComponentModel.CancelEventHandler(this.TxtBarcode_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(8, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 19);
            this.label5.TabIndex = 60;
            this.label5.Text = "Unit";
            // 
            // TxtDescription
            // 
            this.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDescription.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDescription.Location = new System.Drawing.Point(109, 70);
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(446, 25);
            this.TxtDescription.TabIndex = 4;
            this.TxtDescription.Enter += new System.EventHandler(this.Ctrl_Enter);
            this.TxtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDescription_KeyDown);
            this.TxtDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_KeyPress);
            this.TxtDescription.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDescription_Validating);
            // 
            // TxtCategory
            // 
            this.TxtCategory.BackColor = System.Drawing.SystemColors.Window;
            this.TxtCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCategory.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCategory.Location = new System.Drawing.Point(109, 98);
            this.TxtCategory.Name = "TxtCategory";
            this.TxtCategory.ReadOnly = true;
            this.TxtCategory.Size = new System.Drawing.Size(446, 25);
            this.TxtCategory.TabIndex = 5;
            this.TxtCategory.Enter += new System.EventHandler(this.Ctrl_Enter);
            this.TxtCategory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtGroup_KeyDown);
            this.TxtCategory.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_KeyPress);
            this.TxtCategory.Leave += new System.EventHandler(this.TxtCategory_Leave);
            this.TxtCategory.Validating += new System.ComponentModel.CancelEventHandler(this.TxtGroup_Validating);
            // 
            // BtnAltUnit
            // 
            this.BtnAltUnit.CausesValidation = false;
            this.BtnAltUnit.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAltUnit.ForeColor = System.Drawing.Color.Transparent;
            this.BtnAltUnit.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnAltUnit.Location = new System.Drawing.Point(558, 153);
            this.BtnAltUnit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnAltUnit.Name = "BtnAltUnit";
            this.BtnAltUnit.Size = new System.Drawing.Size(29, 26);
            this.BtnAltUnit.TabIndex = 63;
            this.BtnAltUnit.TabStop = false;
            this.BtnAltUnit.UseVisualStyleBackColor = false;
            this.BtnAltUnit.Click += new System.EventHandler(this.BtnAltUnit_Click);
            // 
            // TxtBarcode2
            // 
            this.TxtBarcode2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBarcode2.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBarcode2.Location = new System.Drawing.Point(109, 42);
            this.TxtBarcode2.Name = "TxtBarcode2";
            this.TxtBarcode2.Size = new System.Drawing.Size(175, 25);
            this.TxtBarcode2.TabIndex = 2;
            this.TxtBarcode2.Enter += new System.EventHandler(this.Ctrl_Enter);
            this.TxtBarcode2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtProductId_KeyPress);
            this.TxtBarcode2.Leave += new System.EventHandler(this.Ctrl_Leave);
            this.TxtBarcode2.Validating += new System.ComponentModel.CancelEventHandler(this.TxtProductCode_Validating);
            // 
            // TxtBarcode1
            // 
            this.TxtBarcode1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBarcode1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBarcode1.Location = new System.Drawing.Point(415, 14);
            this.TxtBarcode1.Name = "TxtBarcode1";
            this.TxtBarcode1.Size = new System.Drawing.Size(140, 25);
            this.TxtBarcode1.TabIndex = 1;
            this.TxtBarcode1.Enter += new System.EventHandler(this.Ctrl_Enter);
            this.TxtBarcode1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBarcode1_KeyDown);
            this.TxtBarcode1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBarcode1_KeyPress);
            this.TxtBarcode1.Leave += new System.EventHandler(this.Ctrl_Leave);
            this.TxtBarcode1.Validating += new System.ComponentModel.CancelEventHandler(this.TxtBarcode1_Validating);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(7, 183);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(61, 19);
            this.label13.TabIndex = 61;
            this.label13.Text = "Alt Qty";
            // 
            // TxtUnit
            // 
            this.TxtUnit.BackColor = System.Drawing.SystemColors.Window;
            this.TxtUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtUnit.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUnit.Location = new System.Drawing.Point(109, 153);
            this.TxtUnit.Name = "TxtUnit";
            this.TxtUnit.ReadOnly = true;
            this.TxtUnit.Size = new System.Drawing.Size(162, 25);
            this.TxtUnit.TabIndex = 7;
            this.TxtUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtUnit_KeyDown);
            this.TxtUnit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_KeyPress);
            this.TxtUnit.Validating += new System.ComponentModel.CancelEventHandler(this.TxtUnit_Validating);
            // 
            // BtnUnit
            // 
            this.BtnUnit.CausesValidation = false;
            this.BtnUnit.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnUnit.ForeColor = System.Drawing.Color.Transparent;
            this.BtnUnit.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnUnit.Location = new System.Drawing.Point(273, 153);
            this.BtnUnit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnUnit.Name = "BtnUnit";
            this.BtnUnit.Size = new System.Drawing.Size(29, 26);
            this.BtnUnit.TabIndex = 61;
            this.BtnUnit.TabStop = false;
            this.BtnUnit.UseVisualStyleBackColor = false;
            this.BtnUnit.Click += new System.EventHandler(this.BtnProductUnit_Click);
            // 
            // BtnGroup
            // 
            this.BtnGroup.CausesValidation = false;
            this.BtnGroup.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGroup.ForeColor = System.Drawing.Color.Transparent;
            this.BtnGroup.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnGroup.Location = new System.Drawing.Point(558, 97);
            this.BtnGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnGroup.Name = "BtnGroup";
            this.BtnGroup.Size = new System.Drawing.Size(29, 26);
            this.BtnGroup.TabIndex = 41;
            this.BtnGroup.TabStop = false;
            this.BtnGroup.UseVisualStyleBackColor = false;
            this.BtnGroup.Click += new System.EventHandler(this.BtnProductGroup_Click);
            // 
            // TxtMRP
            // 
            this.TxtMRP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMRP.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtMRP.Location = new System.Drawing.Point(110, 208);
            this.TxtMRP.Name = "TxtMRP";
            this.TxtMRP.Size = new System.Drawing.Size(161, 25);
            this.TxtMRP.TabIndex = 11;
            this.TxtMRP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtMRP.TextBoxType = MrDAL.Control.ControlsEx.Control.TextBoxType.Decimal;
            this.TxtMRP.Enter += new System.EventHandler(this.Ctrl_Enter);
            this.TxtMRP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtMRP_KeyPress);
            this.TxtMRP.Leave += new System.EventHandler(this.TxtMRP_Leave);
            // 
            // BtnBarcode1
            // 
            this.BtnBarcode1.CausesValidation = false;
            this.BtnBarcode1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBarcode1.ForeColor = System.Drawing.Color.Transparent;
            this.BtnBarcode1.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnBarcode1.Location = new System.Drawing.Point(557, 13);
            this.BtnBarcode1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnBarcode1.Name = "BtnBarcode1";
            this.BtnBarcode1.Size = new System.Drawing.Size(29, 26);
            this.BtnBarcode1.TabIndex = 42;
            this.BtnBarcode1.TabStop = false;
            this.BtnBarcode1.UseVisualStyleBackColor = false;
            this.BtnBarcode1.Click += new System.EventHandler(this.BtnBarcode1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(309, 211);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 19);
            this.label7.TabIndex = 28;
            this.label7.Text = "Buy Rate";
            // 
            // BtnSubGroup
            // 
            this.BtnSubGroup.CausesValidation = false;
            this.BtnSubGroup.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSubGroup.ForeColor = System.Drawing.Color.Transparent;
            this.BtnSubGroup.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnSubGroup.Location = new System.Drawing.Point(558, 125);
            this.BtnSubGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnSubGroup.Name = "BtnSubGroup";
            this.BtnSubGroup.Size = new System.Drawing.Size(29, 26);
            this.BtnSubGroup.TabIndex = 52;
            this.BtnSubGroup.TabStop = false;
            this.BtnSubGroup.UseVisualStyleBackColor = false;
            this.BtnSubGroup.Click += new System.EventHandler(this.BtnSubGroup_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(8, 211);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 19);
            this.label11.TabIndex = 49;
            this.label11.Text = "MRP Rate";
            // 
            // BtnBarcode
            // 
            this.BtnBarcode.CausesValidation = false;
            this.BtnBarcode.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBarcode.ForeColor = System.Drawing.Color.Transparent;
            this.BtnBarcode.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnBarcode.Location = new System.Drawing.Point(285, 14);
            this.BtnBarcode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnBarcode.Name = "BtnBarcode";
            this.BtnBarcode.Size = new System.Drawing.Size(29, 26);
            this.BtnBarcode.TabIndex = 43;
            this.BtnBarcode.TabStop = false;
            this.BtnBarcode.UseVisualStyleBackColor = false;
            this.BtnBarcode.Click += new System.EventHandler(this.BtnBarcode_Click);
            // 
            // TxtAltUnit
            // 
            this.TxtAltUnit.BackColor = System.Drawing.SystemColors.Window;
            this.TxtAltUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAltUnit.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAltUnit.Location = new System.Drawing.Point(393, 153);
            this.TxtAltUnit.Name = "TxtAltUnit";
            this.TxtAltUnit.ReadOnly = true;
            this.TxtAltUnit.Size = new System.Drawing.Size(161, 25);
            this.TxtAltUnit.TabIndex = 8;
            this.TxtAltUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtAltUnit_KeyDown);
            this.TxtAltUnit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_KeyPress);
            // 
            // TxtSubCategory
            // 
            this.TxtSubCategory.BackColor = System.Drawing.SystemColors.Window;
            this.TxtSubCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSubCategory.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSubCategory.Location = new System.Drawing.Point(109, 125);
            this.TxtSubCategory.Name = "TxtSubCategory";
            this.TxtSubCategory.ReadOnly = true;
            this.TxtSubCategory.Size = new System.Drawing.Size(446, 25);
            this.TxtSubCategory.TabIndex = 6;
            this.TxtSubCategory.Enter += new System.EventHandler(this.Ctrl_Enter);
            this.TxtSubCategory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSubCategory_KeyDown);
            this.TxtSubCategory.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_KeyPress);
            this.TxtSubCategory.Leave += new System.EventHandler(this.Ctrl_Leave);
            this.TxtSubCategory.Validating += new System.ComponentModel.CancelEventHandler(this.TxtSubCategory_Validating);
            // 
            // TxtMargin
            // 
            this.TxtMargin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMargin.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtMargin.Location = new System.Drawing.Point(110, 235);
            this.TxtMargin.Name = "TxtMargin";
            this.TxtMargin.Size = new System.Drawing.Size(161, 25);
            this.TxtMargin.TabIndex = 13;
            this.TxtMargin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtMargin.TextBoxType = MrDAL.Control.ControlsEx.Control.TextBoxType.Decimal;
            this.TxtMargin.TextChanged += new System.EventHandler(this.TxtMargin_TextChanged);
            this.TxtMargin.Enter += new System.EventHandler(this.Ctrl_Enter);
            this.TxtMargin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtMargin_KeyPress);
            this.TxtMargin.Leave += new System.EventHandler(this.Ctrl_Leave);
            this.TxtMargin.Validating += new System.ComponentModel.CancelEventHandler(this.TxtMargin_Validating);
            this.TxtMargin.Validated += new System.EventHandler(this.TxtMargin_Validated);
            // 
            // BtnDescription
            // 
            this.BtnDescription.CausesValidation = false;
            this.BtnDescription.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDescription.ForeColor = System.Drawing.Color.Transparent;
            this.BtnDescription.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDescription.Location = new System.Drawing.Point(558, 70);
            this.BtnDescription.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnDescription.Name = "BtnDescription";
            this.BtnDescription.Size = new System.Drawing.Size(29, 26);
            this.BtnDescription.TabIndex = 44;
            this.BtnDescription.TabStop = false;
            this.BtnDescription.UseVisualStyleBackColor = false;
            this.BtnDescription.Click += new System.EventHandler(this.BtnDescription_Click);
            // 
            // TxtAltQty
            // 
            this.TxtAltQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAltQty.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAltQty.Location = new System.Drawing.Point(110, 181);
            this.TxtAltQty.Name = "TxtAltQty";
            this.TxtAltQty.Size = new System.Drawing.Size(161, 25);
            this.TxtAltQty.TabIndex = 9;
            this.TxtAltQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtAltQty.TextBoxType = MrDAL.Control.ControlsEx.Control.TextBoxType.Decimal;
            this.TxtAltQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtAltQty_KeyPress);
            this.TxtAltQty.Validating += new System.ComponentModel.CancelEventHandler(this.TxtAltQty_Validating);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(8, 129);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(103, 19);
            this.label12.TabIndex = 51;
            this.label12.Text = "SubCategory";
            // 
            // ChkIsTaxable
            // 
            this.ChkIsTaxable.CausesValidation = false;
            this.ChkIsTaxable.EditValue = true;
            this.ChkIsTaxable.EnterMoveNextControl = true;
            this.ChkIsTaxable.Location = new System.Drawing.Point(277, 234);
            this.ChkIsTaxable.Name = "ChkIsTaxable";
            this.ChkIsTaxable.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ChkIsTaxable.Properties.Appearance.Options.UseFont = true;
            this.ChkIsTaxable.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ChkIsTaxable.Properties.AppearanceDisabled.Options.UseFont = true;
            this.ChkIsTaxable.Properties.AppearanceFocused.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ChkIsTaxable.Properties.AppearanceFocused.Options.UseFont = true;
            this.ChkIsTaxable.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ChkIsTaxable.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.ChkIsTaxable.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.ChkIsTaxable.Properties.Caption = "Is Taxable";
            this.ChkIsTaxable.Properties.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.SvgRadio2;
            this.ChkIsTaxable.Size = new System.Drawing.Size(112, 27);
            this.ChkIsTaxable.TabIndex = 14;
            this.ChkIsTaxable.ToolTip = "Is Taxable";
            this.ChkIsTaxable.CheckedChanged += new System.EventHandler(this.ChkIsTaxable_CheckedChanged);
            this.ChkIsTaxable.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ChkIsTaxable_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(8, 265);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 19);
            this.label9.TabIndex = 18;
            this.label9.Text = "Sales Rate";
            // 
            // TxtBuyRate
            // 
            this.TxtBuyRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBuyRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBuyRate.Location = new System.Drawing.Point(393, 207);
            this.TxtBuyRate.Name = "TxtBuyRate";
            this.TxtBuyRate.Size = new System.Drawing.Size(161, 25);
            this.TxtBuyRate.TabIndex = 12;
            this.TxtBuyRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtBuyRate.TextBoxType = MrDAL.Control.ControlsEx.Control.TextBoxType.Decimal;
            this.TxtBuyRate.TextChanged += new System.EventHandler(this.TxtBuyRate_TextChanged);
            this.TxtBuyRate.Enter += new System.EventHandler(this.Ctrl_Enter);
            this.TxtBuyRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPurchaseRate_KeyPress);
            this.TxtBuyRate.Leave += new System.EventHandler(this.Ctrl_Leave);
            this.TxtBuyRate.Validating += new System.ComponentModel.CancelEventHandler(this.TxtPurchaseRate_Validating);
            // 
            // TxtQty
            // 
            this.TxtQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtQty.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtQty.Location = new System.Drawing.Point(393, 180);
            this.TxtQty.Name = "TxtQty";
            this.TxtQty.Size = new System.Drawing.Size(161, 25);
            this.TxtQty.TabIndex = 10;
            this.TxtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtQty.TextBoxType = MrDAL.Control.ControlsEx.Control.TextBoxType.Decimal;
            this.TxtQty.Enter += new System.EventHandler(this.Ctrl_Enter);
            this.TxtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtQty_KeyPress);
            this.TxtQty.Leave += new System.EventHandler(this.Ctrl_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(8, 238);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 19);
            this.label8.TabIndex = 17;
            this.label8.Text = "Margin";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(309, 183);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(35, 19);
            this.label14.TabIndex = 2;
            this.label14.Text = "Qty";
            // 
            // tbImage
            // 
            this.tbImage.Controls.Add(this.ChkBatchWise);
            this.tbImage.Controls.Add(this.LinkAttachment1);
            this.tbImage.Controls.Add(this.pictureEdit1);
            this.tbImage.Controls.Add(this.ChkBarcodeList);
            this.tbImage.Controls.Add(this.ChkPriceUpdate);
            this.tbImage.Name = "tbImage";
            this.tbImage.Size = new System.Drawing.Size(596, 293);
            this.tbImage.Text = "Product Image";
            // 
            // ChkBatchWise
            // 
            this.ChkBatchWise.AutoSize = true;
            this.ChkBatchWise.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ChkBatchWise.Location = new System.Drawing.Point(10, 223);
            this.ChkBatchWise.Name = "ChkBatchWise";
            this.ChkBatchWise.Size = new System.Drawing.Size(113, 23);
            this.ChkBatchWise.TabIndex = 3;
            this.ChkBatchWise.Text = "Batch Wise";
            this.ChkBatchWise.UseVisualStyleBackColor = true;
            // 
            // LinkAttachment1
            // 
            this.LinkAttachment1.AutoSize = true;
            this.LinkAttachment1.Location = new System.Drawing.Point(182, 140);
            this.LinkAttachment1.Name = "LinkAttachment1";
            this.LinkAttachment1.Size = new System.Drawing.Size(45, 13);
            this.LinkAttachment1.TabIndex = 360;
            this.LinkAttachment1.TabStop = true;
            this.LinkAttachment1.Text = "Preview";
            this.LinkAttachment1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkAttachment1_LinkClicked);
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Location = new System.Drawing.Point(11, 3);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.pictureEdit1.Properties.Appearance.Options.UseFont = true;
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit1.Size = new System.Drawing.Size(171, 150);
            this.pictureEdit1.TabIndex = 0;
            this.pictureEdit1.DoubleClick += new System.EventHandler(this.PictureEdit1_DoubleClick);
            this.pictureEdit1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PictureEdit1_KeyDown);
            // 
            // ChkBarcodeList
            // 
            this.ChkBarcodeList.CausesValidation = false;
            this.ChkBarcodeList.EnterMoveNextControl = true;
            this.ChkBarcodeList.Location = new System.Drawing.Point(9, 159);
            this.ChkBarcodeList.Name = "ChkBarcodeList";
            this.ChkBarcodeList.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ChkBarcodeList.Properties.Appearance.Options.UseFont = true;
            this.ChkBarcodeList.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ChkBarcodeList.Properties.AppearanceDisabled.Options.UseFont = true;
            this.ChkBarcodeList.Properties.AppearanceFocused.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ChkBarcodeList.Properties.AppearanceFocused.Options.UseFont = true;
            this.ChkBarcodeList.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ChkBarcodeList.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.ChkBarcodeList.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.ChkBarcodeList.Properties.Caption = "Show Barcode List";
            this.ChkBarcodeList.Properties.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.SvgRadio2;
            this.ChkBarcodeList.Size = new System.Drawing.Size(173, 27);
            this.ChkBarcodeList.TabIndex = 1;
            this.ChkBarcodeList.ToolTip = "Show Barcode List";
            this.ChkBarcodeList.CheckedChanged += new System.EventHandler(this.ChkBarcodeList_CheckedChanged);
            // 
            // ChkPriceUpdate
            // 
            this.ChkPriceUpdate.CausesValidation = false;
            this.ChkPriceUpdate.Enabled = false;
            this.ChkPriceUpdate.EnterMoveNextControl = true;
            this.ChkPriceUpdate.Location = new System.Drawing.Point(9, 190);
            this.ChkPriceUpdate.Name = "ChkPriceUpdate";
            this.ChkPriceUpdate.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ChkPriceUpdate.Properties.Appearance.Options.UseFont = true;
            this.ChkPriceUpdate.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ChkPriceUpdate.Properties.AppearanceDisabled.Options.UseFont = true;
            this.ChkPriceUpdate.Properties.AppearanceFocused.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ChkPriceUpdate.Properties.AppearanceFocused.Options.UseFont = true;
            this.ChkPriceUpdate.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ChkPriceUpdate.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.ChkPriceUpdate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.ChkPriceUpdate.Properties.Caption = "Show Price";
            this.ChkPriceUpdate.Properties.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.SvgRadio2;
            this.ChkPriceUpdate.Size = new System.Drawing.Size(130, 27);
            this.ChkPriceUpdate.TabIndex = 2;
            this.ChkPriceUpdate.ToolTip = "Show Barcode List";
            this.ChkPriceUpdate.Visible = false;
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(388, 328);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(103, 34);
            this.BtnSave.TabIndex = 1;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(493, 328);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(103, 34);
            this.BtnCancel.TabIndex = 2;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(-1, 190);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(593, 2);
            this.clsSeparator1.TabIndex = 20;
            this.clsSeparator1.TabStop = false;
            // 
            // ChkActive
            // 
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkActive.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ChkActive.Location = new System.Drawing.Point(7, 326);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkActive.Size = new System.Drawing.Size(105, 24);
            this.ChkActive.TabIndex = 21;
            this.ChkActive.Text = "Active";
            this.ChkActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkActive.UseVisualStyleBackColor = true;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.ImageOptions.Image = global::MrBLL.Properties.Resources.ReportRefresh;
            this.simpleButton1.Location = new System.Drawing.Point(302, 328);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(85, 34);
            this.simpleButton1.TabIndex = 22;
            this.simpleButton1.Text = "&SYNC";
            this.simpleButton1.Click += new System.EventHandler(this.BtnSync_Click);
            // 
            // TxtTaxRate
            // 
            this.TxtTaxRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTaxRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTaxRate.Location = new System.Drawing.Point(473, 235);
            this.TxtTaxRate.Name = "TxtTaxRate";
            this.TxtTaxRate.Size = new System.Drawing.Size(81, 25);
            this.TxtTaxRate.TabIndex = 15;
            this.TxtTaxRate.Text = "13.00";
            this.TxtTaxRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtTaxRate.TextBoxType = MrDAL.Control.ControlsEx.Control.TextBoxType.Decimal;
            this.TxtTaxRate.Validating += new System.ComponentModel.CancelEventHandler(this.TxtTaxRate_Validating);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(395, 238);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(75, 19);
            this.label17.TabIndex = 69;
            this.label17.Text = "Tax Rate";
            // 
            // FrmPosProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(597, 364);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.ChkActive);
            this.Controls.Add(this.TabPOS);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.clsSeparator1);
            this.Controls.Add(this.BtnCancel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPosProduct";
            this.ShowIcon = false;
            this.Text = "Counter Product";
            this.Load += new System.EventHandler(this.FrmCounterProduct_Load);
            this.Shown += new System.EventHandler(this.FrmPOSProduct_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCounterProduct_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmPOSProduct_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.TabPOS)).EndInit();
            this.TabPOS.ResumeLayout(false);
            this.tbProduct.ResumeLayout(false);
            this.tbProduct.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChkIsTaxable.Properties)).EndInit();
            this.tbImage.ResumeLayout(false);
            this.tbImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkBarcodeList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkPriceUpdate.Properties)).EndInit();
            this.ResumeLayout(false);

		}

        #endregion

        private DevExpress.XtraTab.XtraTabControl TabPOS;
        private DevExpress.XtraTab.XtraTabPage tbProduct;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BtnAltUnit;
        private System.Windows.Forms.Button BtnUnit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private CheckEdit ChkBarcodeList;
        private System.Windows.Forms.Button BtnGroup;
        private System.Windows.Forms.Button BtnBarcode1;
        private System.Windows.Forms.Button BtnSubGroup;
        private System.Windows.Forms.Button BtnBarcode;
        private System.Windows.Forms.Button BtnDescription;
        private System.Windows.Forms.Label label12;
        private CheckEdit ChkPriceUpdate;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label14;
        private SimpleButton BtnSave;
        private CheckEdit ChkIsTaxable;
        private System.Windows.Forms.Label label11;
        private SimpleButton BtnCancel;
        private DevExpress.XtraTab.XtraTabPage tbImage;
        private PictureEdit pictureEdit1;
        private ClsSeparator clsSeparator1;
        private System.Windows.Forms.LinkLabel LinkAttachment1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.CheckBox ChkActive;
        private MrTextBox TxtAltUnit;
        private MrTextBox TxtUnit;
        private MrTextBox TxtBarcode;
        private MrTextBox TxtDescription;
        private MrTextBox TxtCategory;
        private MrTextBox TxtBarcode2;
        private MrTextBox TxtBarcode1;
        private MrTextBox TxtSubCategory;
        private MrTextBox TxtSalesRate;
        private MrTextBox TxtAltQty;
        private MrTextBox TxtQty;
        private MrTextBox TxtBuyRate;
        private MrTextBox TxtMargin;
        private MrTextBox TxtMRP;
        private System.Windows.Forms.CheckBox ChkBatchWise;
        private MrTextBox TxtAltSalesRate;
        private System.Windows.Forms.Label label15;
        private SimpleButton simpleButton1;
        private System.Windows.Forms.Label label16;
        private MrTextBox TxtHsCode;
        private MrTextBox TxtTaxRate;
        private System.Windows.Forms.Label label17;
    }
}