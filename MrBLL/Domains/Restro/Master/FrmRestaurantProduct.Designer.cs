using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.Restro.Master
{
    partial class FrmRestaurantProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRestaurantProduct));
            this.label1 = new System.Windows.Forms.Label();
            this.qtyDesc = new System.Windows.Forms.Label();
            this.AltUniDesc = new System.Windows.Forms.Label();
            this.BtnSubGroup = new System.Windows.Forms.Button();
            this.lvlProductVatPer = new System.Windows.Forms.Label();
            this.lvlProductTaxable = new System.Windows.Forms.Label();
            this.lvlProductSubGroup = new System.Windows.Forms.Label();
            this.lvlProductSalesRate = new System.Windows.Forms.Label();
            this.lvlProductConversion = new System.Windows.Forms.Label();
            this.lvlProductAltUnit = new System.Windows.Forms.Label();
            this.lvlProductUnit = new System.Windows.Forms.Label();
            this.CmbItemType = new System.Windows.Forms.ComboBox();
            this.lvlProductType = new System.Windows.Forms.Label();
            this.lvlProductCode = new System.Windows.Forms.Label();
            this.lvlProductName = new System.Windows.Forms.Label();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.ChkActive = new System.Windows.Forms.CheckBox();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.StorePanel = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtItemCode = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnCategory = new System.Windows.Forms.Button();
            this.TxtCategory = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnUOM = new System.Windows.Forms.Button();
            this.BtnAltUOM = new System.Windows.Forms.Button();
            this.BtnDescription = new System.Windows.Forms.Button();
            this.BtnView = new DevExpress.XtraEditors.SimpleButton();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.TxtVat = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtSubGroup = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtDescription = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtSalesRate = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtShortName = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtRate = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtConvQty = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtConvAltQty = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtUnit = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtAltUnit = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.StorePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(453, 206);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 19);
            this.label1.TabIndex = 54;
            this.label1.Text = "Rate";
            // 
            // qtyDesc
            // 
            this.qtyDesc.BackColor = System.Drawing.Color.White;
            this.qtyDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.qtyDesc.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.qtyDesc.Location = new System.Drawing.Point(518, 121);
            this.qtyDesc.Name = "qtyDesc";
            this.qtyDesc.Size = new System.Drawing.Size(75, 25);
            this.qtyDesc.TabIndex = 13;
            // 
            // AltUniDesc
            // 
            this.AltUniDesc.BackColor = System.Drawing.Color.White;
            this.AltUniDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AltUniDesc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AltUniDesc.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AltUniDesc.Location = new System.Drawing.Point(216, 121);
            this.AltUniDesc.Name = "AltUniDesc";
            this.AltUniDesc.Size = new System.Drawing.Size(75, 25);
            this.AltUniDesc.TabIndex = 11;
            // 
            // BtnSubGroup
            // 
            this.BtnSubGroup.CausesValidation = false;
            this.BtnSubGroup.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSubGroup.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnSubGroup.Location = new System.Drawing.Point(594, 174);
            this.BtnSubGroup.Name = "BtnSubGroup";
            this.BtnSubGroup.Size = new System.Drawing.Size(29, 27);
            this.BtnSubGroup.TabIndex = 12;
            this.BtnSubGroup.TabStop = false;
            this.BtnSubGroup.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.BtnSubGroup.UseVisualStyleBackColor = true;
            this.BtnSubGroup.Click += new System.EventHandler(this.BtnSubGroup_Click);
            // 
            // lvlProductVatPer
            // 
            this.lvlProductVatPer.AutoSize = true;
            this.lvlProductVatPer.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductVatPer.Location = new System.Drawing.Point(430, 206);
            this.lvlProductVatPer.Name = "lvlProductVatPer";
            this.lvlProductVatPer.Size = new System.Drawing.Size(23, 19);
            this.lvlProductVatPer.TabIndex = 22;
            this.lvlProductVatPer.Text = "%";
            // 
            // lvlProductTaxable
            // 
            this.lvlProductTaxable.AutoSize = true;
            this.lvlProductTaxable.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductTaxable.Location = new System.Drawing.Point(277, 206);
            this.lvlProductTaxable.Name = "lvlProductTaxable";
            this.lvlProductTaxable.Size = new System.Drawing.Size(106, 19);
            this.lvlProductTaxable.TabIndex = 15;
            this.lvlProductTaxable.Text = "Taxable Rate";
            // 
            // lvlProductSubGroup
            // 
            this.lvlProductSubGroup.AutoSize = true;
            this.lvlProductSubGroup.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductSubGroup.Location = new System.Drawing.Point(12, 178);
            this.lvlProductSubGroup.Name = "lvlProductSubGroup";
            this.lvlProductSubGroup.Size = new System.Drawing.Size(56, 19);
            this.lvlProductSubGroup.TabIndex = 18;
            this.lvlProductSubGroup.Text = "Menu ";
            // 
            // lvlProductSalesRate
            // 
            this.lvlProductSalesRate.AutoSize = true;
            this.lvlProductSalesRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductSalesRate.Location = new System.Drawing.Point(12, 205);
            this.lvlProductSalesRate.Name = "lvlProductSalesRate";
            this.lvlProductSalesRate.Size = new System.Drawing.Size(90, 19);
            this.lvlProductSalesRate.TabIndex = 15;
            this.lvlProductSalesRate.Text = "Sales Rate";
            // 
            // lvlProductConversion
            // 
            this.lvlProductConversion.AutoSize = true;
            this.lvlProductConversion.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductConversion.Location = new System.Drawing.Point(12, 124);
            this.lvlProductConversion.Name = "lvlProductConversion";
            this.lvlProductConversion.Size = new System.Drawing.Size(92, 19);
            this.lvlProductConversion.TabIndex = 9;
            this.lvlProductConversion.Text = "Conversion";
            // 
            // lvlProductAltUnit
            // 
            this.lvlProductAltUnit.AutoSize = true;
            this.lvlProductAltUnit.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductAltUnit.Location = new System.Drawing.Point(352, 98);
            this.lvlProductAltUnit.Name = "lvlProductAltUnit";
            this.lvlProductAltUnit.Size = new System.Drawing.Size(68, 19);
            this.lvlProductAltUnit.TabIndex = 7;
            this.lvlProductAltUnit.Text = "Alt Unit";
            // 
            // lvlProductUnit
            // 
            this.lvlProductUnit.AutoSize = true;
            this.lvlProductUnit.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductUnit.Location = new System.Drawing.Point(12, 98);
            this.lvlProductUnit.Name = "lvlProductUnit";
            this.lvlProductUnit.Size = new System.Drawing.Size(42, 19);
            this.lvlProductUnit.TabIndex = 5;
            this.lvlProductUnit.Text = "Unit";
            // 
            // CmbItemType
            // 
            this.CmbItemType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.CmbItemType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CmbItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbItemType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbItemType.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbItemType.FormattingEnabled = true;
            this.CmbItemType.Items.AddRange(new object[] {
            "Inventory",
            "Service",
            "Assets"});
            this.CmbItemType.Location = new System.Drawing.Point(425, 67);
            this.CmbItemType.Name = "CmbItemType";
            this.CmbItemType.Size = new System.Drawing.Size(168, 27);
            this.CmbItemType.TabIndex = 7;
            this.CmbItemType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_KeyPress);
            // 
            // lvlProductType
            // 
            this.lvlProductType.AutoSize = true;
            this.lvlProductType.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductType.Location = new System.Drawing.Point(351, 71);
            this.lvlProductType.Name = "lvlProductType";
            this.lvlProductType.Size = new System.Drawing.Size(43, 19);
            this.lvlProductType.TabIndex = 2;
            this.lvlProductType.Text = "Type";
            // 
            // lvlProductCode
            // 
            this.lvlProductCode.AutoSize = true;
            this.lvlProductCode.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductCode.Location = new System.Drawing.Point(12, 72);
            this.lvlProductCode.Name = "lvlProductCode";
            this.lvlProductCode.Size = new System.Drawing.Size(98, 19);
            this.lvlProductCode.TabIndex = 2;
            this.lvlProductCode.Text = "Short Name";
            // 
            // lvlProductName
            // 
            this.lvlProductName.AutoSize = true;
            this.lvlProductName.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductName.Location = new System.Drawing.Point(12, 45);
            this.lvlProductName.Name = "lvlProductName";
            this.lvlProductName.Size = new System.Drawing.Size(95, 19);
            this.lvlProductName.TabIndex = 0;
            this.lvlProductName.Text = "Description";
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.BtnCancel.Location = new System.Drawing.Point(480, 259);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(109, 37);
            this.BtnCancel.TabIndex = 19;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(369, 259);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(109, 37);
            this.BtnSave.TabIndex = 18;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // ChkActive
            // 
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkActive.Location = new System.Drawing.Point(16, 264);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkActive.Size = new System.Drawing.Size(102, 26);
            this.ChkActive.TabIndex = 20;
            this.ChkActive.Text = "Active";
            this.ChkActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkActive.UseVisualStyleBackColor = true;
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.BtnExit.Location = new System.Drawing.Point(545, 4);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(76, 32);
            this.BtnExit.TabIndex = 4;
            this.BtnExit.Text = "E&XIT";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(167, 4);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(103, 32);
            this.BtnDelete.TabIndex = 2;
            this.BtnDelete.Tag = "DeleteButtonCheck";
            this.BtnDelete.Text = "&DELETE";
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEdit.Appearance.Options.UseFont = true;
            this.BtnEdit.ImageOptions.Image = global::MrBLL.Properties.Resources.Edit;
            this.BtnEdit.Location = new System.Drawing.Point(85, 4);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(82, 32);
            this.BtnEdit.TabIndex = 1;
            this.BtnEdit.Tag = "EditButtonCheck";
            this.BtnEdit.Text = "&EDIT";
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnNew
            // 
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnNew.Location = new System.Drawing.Point(10, 4);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(75, 32);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Tag = "NewButtonCheck";
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.simpleButton1);
            this.StorePanel.Controls.Add(this.label3);
            this.StorePanel.Controls.Add(this.TxtItemCode);
            this.StorePanel.Controls.Add(this.BtnCategory);
            this.StorePanel.Controls.Add(this.TxtCategory);
            this.StorePanel.Controls.Add(this.label2);
            this.StorePanel.Controls.Add(this.BtnUOM);
            this.StorePanel.Controls.Add(this.BtnAltUOM);
            this.StorePanel.Controls.Add(this.BtnDescription);
            this.StorePanel.Controls.Add(this.BtnView);
            this.StorePanel.Controls.Add(this.clsSeparator2);
            this.StorePanel.Controls.Add(this.clsSeparator1);
            this.StorePanel.Controls.Add(this.BtnCancel);
            this.StorePanel.Controls.Add(this.qtyDesc);
            this.StorePanel.Controls.Add(this.BtnSave);
            this.StorePanel.Controls.Add(this.label1);
            this.StorePanel.Controls.Add(this.ChkActive);
            this.StorePanel.Controls.Add(this.AltUniDesc);
            this.StorePanel.Controls.Add(this.BtnExit);
            this.StorePanel.Controls.Add(this.BtnDelete);
            this.StorePanel.Controls.Add(this.BtnEdit);
            this.StorePanel.Controls.Add(this.BtnSubGroup);
            this.StorePanel.Controls.Add(this.BtnNew);
            this.StorePanel.Controls.Add(this.lvlProductVatPer);
            this.StorePanel.Controls.Add(this.lvlProductTaxable);
            this.StorePanel.Controls.Add(this.TxtVat);
            this.StorePanel.Controls.Add(this.lvlProductName);
            this.StorePanel.Controls.Add(this.TxtSubGroup);
            this.StorePanel.Controls.Add(this.lvlProductSubGroup);
            this.StorePanel.Controls.Add(this.TxtDescription);
            this.StorePanel.Controls.Add(this.lvlProductCode);
            this.StorePanel.Controls.Add(this.lvlProductSalesRate);
            this.StorePanel.Controls.Add(this.lvlProductType);
            this.StorePanel.Controls.Add(this.TxtSalesRate);
            this.StorePanel.Controls.Add(this.TxtShortName);
            this.StorePanel.Controls.Add(this.TxtRate);
            this.StorePanel.Controls.Add(this.CmbItemType);
            this.StorePanel.Controls.Add(this.TxtConvQty);
            this.StorePanel.Controls.Add(this.lvlProductUnit);
            this.StorePanel.Controls.Add(this.TxtConvAltQty);
            this.StorePanel.Controls.Add(this.TxtUnit);
            this.StorePanel.Controls.Add(this.lvlProductConversion);
            this.StorePanel.Controls.Add(this.lvlProductAltUnit);
            this.StorePanel.Controls.Add(this.TxtAltUnit);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(624, 298);
            this.StorePanel.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 232);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 19);
            this.label3.TabIndex = 318;
            this.label3.Text = "Item Code";
            // 
            // TxtItemCode
            // 
            this.TxtItemCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtItemCode.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtItemCode.Location = new System.Drawing.Point(122, 229);
            this.TxtItemCode.Name = "TxtItemCode";
            this.TxtItemCode.Size = new System.Drawing.Size(149, 25);
            this.TxtItemCode.TabIndex = 17;
            this.TxtItemCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtItemCode_KeyDown);
            // 
            // BtnCategory
            // 
            this.BtnCategory.CausesValidation = false;
            this.BtnCategory.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCategory.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnCategory.Location = new System.Drawing.Point(594, 147);
            this.BtnCategory.Name = "BtnCategory";
            this.BtnCategory.Size = new System.Drawing.Size(29, 27);
            this.BtnCategory.TabIndex = 316;
            this.BtnCategory.TabStop = false;
            this.BtnCategory.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.BtnCategory.UseVisualStyleBackColor = true;
            this.BtnCategory.Click += new System.EventHandler(this.BtnCategory_Click);
            // 
            // TxtCategory
            // 
            this.TxtCategory.BackColor = System.Drawing.Color.White;
            this.TxtCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCategory.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCategory.Location = new System.Drawing.Point(122, 148);
            this.TxtCategory.Name = "TxtCategory";
            this.TxtCategory.ReadOnly = true;
            this.TxtCategory.Size = new System.Drawing.Size(470, 25);
            this.TxtCategory.TabIndex = 12;
            this.TxtCategory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCategory_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 19);
            this.label2.TabIndex = 317;
            this.label2.Text = "Category";
            // 
            // BtnUOM
            // 
            this.BtnUOM.BackColor = System.Drawing.Color.Transparent;
            this.BtnUOM.CausesValidation = false;
            this.BtnUOM.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnUOM.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnUOM.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnUOM.Location = new System.Drawing.Point(291, 94);
            this.BtnUOM.Name = "BtnUOM";
            this.BtnUOM.Size = new System.Drawing.Size(28, 26);
            this.BtnUOM.TabIndex = 314;
            this.BtnUOM.TabStop = false;
            this.BtnUOM.UseVisualStyleBackColor = false;
            this.BtnUOM.Click += new System.EventHandler(this.BtnUOM_Click);
            // 
            // BtnAltUOM
            // 
            this.BtnAltUOM.BackColor = System.Drawing.Color.Transparent;
            this.BtnAltUOM.CausesValidation = false;
            this.BtnAltUOM.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnAltUOM.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnAltUOM.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnAltUOM.Location = new System.Drawing.Point(595, 94);
            this.BtnAltUOM.Name = "BtnAltUOM";
            this.BtnAltUOM.Size = new System.Drawing.Size(28, 26);
            this.BtnAltUOM.TabIndex = 8;
            this.BtnAltUOM.TabStop = false;
            this.BtnAltUOM.UseVisualStyleBackColor = false;
            this.BtnAltUOM.Click += new System.EventHandler(this.BtnAltUOM_Click);
            // 
            // BtnDescription
            // 
            this.BtnDescription.BackColor = System.Drawing.Color.Transparent;
            this.BtnDescription.CausesValidation = false;
            this.BtnDescription.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnDescription.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnDescription.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDescription.Location = new System.Drawing.Point(594, 41);
            this.BtnDescription.Name = "BtnDescription";
            this.BtnDescription.Size = new System.Drawing.Size(28, 26);
            this.BtnDescription.TabIndex = 313;
            this.BtnDescription.TabStop = false;
            this.BtnDescription.UseVisualStyleBackColor = false;
            this.BtnDescription.Click += new System.EventHandler(this.BtnDescription_Click);
            // 
            // BtnView
            // 
            this.BtnView.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnView.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnView.Appearance.Options.UseFont = true;
            this.BtnView.Appearance.Options.UseForeColor = true;
            this.BtnView.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnView.Location = new System.Drawing.Point(270, 4);
            this.BtnView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnView.Name = "BtnView";
            this.BtnView.Size = new System.Drawing.Size(88, 32);
            this.BtnView.TabIndex = 3;
            this.BtnView.Tag = "ViewButtonCheck";
            this.BtnView.Text = "&VIEW";
            this.BtnView.Click += new System.EventHandler(this.BtnView_Click);
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(10, 256);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(612, 2);
            this.clsSeparator2.TabIndex = 56;
            this.clsSeparator2.TabStop = false;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(6, 38);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(622, 2);
            this.clsSeparator1.TabIndex = 55;
            this.clsSeparator1.TabStop = false;
            // 
            // TxtVat
            // 
            this.TxtVat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtVat.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtVat.Location = new System.Drawing.Point(383, 203);
            this.TxtVat.Name = "TxtVat";
            this.TxtVat.Size = new System.Drawing.Size(47, 25);
            this.TxtVat.TabIndex = 15;
            this.TxtVat.Text = "13";
            this.TxtVat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtVat.TextChanged += new System.EventHandler(this.TxtVat_TextChanged);
            this.TxtVat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtVatRate_KeyPress);
            this.TxtVat.Validating += new System.ComponentModel.CancelEventHandler(this.TxtVat_Validating);
            // 
            // TxtSubGroup
            // 
            this.TxtSubGroup.BackColor = System.Drawing.Color.White;
            this.TxtSubGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSubGroup.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSubGroup.Location = new System.Drawing.Point(122, 175);
            this.TxtSubGroup.Name = "TxtSubGroup";
            this.TxtSubGroup.ReadOnly = true;
            this.TxtSubGroup.Size = new System.Drawing.Size(470, 25);
            this.TxtSubGroup.TabIndex = 13;
            this.TxtSubGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSubGroup_KeyDown);
            // 
            // TxtDescription
            // 
            this.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDescription.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDescription.Location = new System.Drawing.Point(122, 40);
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(471, 25);
            this.TxtDescription.TabIndex = 5;
            this.TxtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDescription_KeyDown);
            this.TxtDescription.Leave += new System.EventHandler(this.TxtDescription_Leave);
            this.TxtDescription.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDescription_Validating);
            // 
            // TxtSalesRate
            // 
            this.TxtSalesRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSalesRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSalesRate.Location = new System.Drawing.Point(122, 202);
            this.TxtSalesRate.Name = "TxtSalesRate";
            this.TxtSalesRate.Size = new System.Drawing.Size(148, 25);
            this.TxtSalesRate.TabIndex = 14;
            this.TxtSalesRate.Text = "0.00";
            this.TxtSalesRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtSalesRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtSalesRate_KeyPress);
            this.TxtSalesRate.Validating += new System.ComponentModel.CancelEventHandler(this.TxtSalesRate_Validating);
            // 
            // TxtShortName
            // 
            this.TxtShortName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtShortName.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtShortName.Location = new System.Drawing.Point(122, 68);
            this.TxtShortName.Name = "TxtShortName";
            this.TxtShortName.Size = new System.Drawing.Size(169, 25);
            this.TxtShortName.TabIndex = 6;
            this.TxtShortName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtShortName_KeyDown);
            this.TxtShortName.Leave += new System.EventHandler(this.TxtShortName_Leave);
            this.TxtShortName.Validating += new System.ComponentModel.CancelEventHandler(this.TxtShortName_Validating);
            // 
            // TxtRate
            // 
            this.TxtRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtRate.Location = new System.Drawing.Point(497, 203);
            this.TxtRate.Name = "TxtRate";
            this.TxtRate.Size = new System.Drawing.Size(94, 25);
            this.TxtRate.TabIndex = 16;
            this.TxtRate.Text = "0.00";
            this.TxtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtSalesRate_KeyPress);
            this.TxtRate.Validating += new System.ComponentModel.CancelEventHandler(this.TxtRate_Validating);
            // 
            // TxtConvQty
            // 
            this.TxtConvQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtConvQty.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtConvQty.Location = new System.Drawing.Point(425, 121);
            this.TxtConvQty.Name = "TxtConvQty";
            this.TxtConvQty.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TxtConvQty.Size = new System.Drawing.Size(89, 25);
            this.TxtConvQty.TabIndex = 11;
            this.TxtConvQty.Text = "0";
            this.TxtConvQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtQtyCov_KeyPress);
            this.TxtConvQty.Validating += new System.ComponentModel.CancelEventHandler(this.TxtConvQty_Validating);
            // 
            // TxtConvAltQty
            // 
            this.TxtConvAltQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtConvAltQty.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtConvAltQty.Location = new System.Drawing.Point(122, 121);
            this.TxtConvAltQty.Name = "TxtConvAltQty";
            this.TxtConvAltQty.Size = new System.Drawing.Size(93, 25);
            this.TxtConvAltQty.TabIndex = 10;
            this.TxtConvAltQty.Text = "0";
            this.TxtConvAltQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtConvAltQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtAltQtyCov_KeyPress);
            this.TxtConvAltQty.Validating += new System.ComponentModel.CancelEventHandler(this.TxtConvAltQty_Validating);
            // 
            // TxtUnit
            // 
            this.TxtUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtUnit.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUnit.Location = new System.Drawing.Point(122, 95);
            this.TxtUnit.Name = "TxtUnit";
            this.TxtUnit.Size = new System.Drawing.Size(169, 25);
            this.TxtUnit.TabIndex = 8;
            this.TxtUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtUnit_KeyDown);
            // 
            // TxtAltUnit
            // 
            this.TxtAltUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAltUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtAltUnit.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAltUnit.Location = new System.Drawing.Point(425, 95);
            this.TxtAltUnit.Name = "TxtAltUnit";
            this.TxtAltUnit.Size = new System.Drawing.Size(169, 25);
            this.TxtAltUnit.TabIndex = 9;
            this.TxtAltUnit.TextChanged += new System.EventHandler(this.TxtAltUnit_TextChanged);
            this.TxtAltUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtAltUnit_KeyDown);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.ImageOptions.Image = global::MrBLL.Properties.Resources.ReportRefresh;
            this.simpleButton1.Location = new System.Drawing.Point(281, 259);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(85, 37);
            this.simpleButton1.TabIndex = 319;
            this.simpleButton1.Text = "&SYNC";
            this.simpleButton1.Click += new System.EventHandler(this.BtnSync_Click);
            // 
            // FrmRestaurantProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(624, 298);
            this.Controls.Add(this.StorePanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "FrmRestaurantProduct";
            this.ShowIcon = false;
            this.Tag = "Restro Product / Menu Items";
            this.Text = "RESTRO MENU SETUP";
            this.Load += new System.EventHandler(this.FrmRestaurantProduct_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmRestaurantProduct_KeyPress);
            this.StorePanel.ResumeLayout(false);
            this.StorePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label qtyDesc;
        private System.Windows.Forms.Label AltUniDesc;
        private System.Windows.Forms.Button BtnSubGroup;
        private System.Windows.Forms.Label lvlProductVatPer;
        private System.Windows.Forms.Label lvlProductTaxable;
        private System.Windows.Forms.Label lvlProductSubGroup;
        private System.Windows.Forms.Label lvlProductConversion;
        private System.Windows.Forms.Label lvlProductAltUnit;
        private System.Windows.Forms.Label lvlProductUnit;
        private System.Windows.Forms.ComboBox CmbItemType;
        private System.Windows.Forms.Label lvlProductType;
        private System.Windows.Forms.Label lvlProductCode;
        private System.Windows.Forms.Label lvlProductName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lvlProductSalesRate;
        private System.Windows.Forms.CheckBox ChkActive;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private ClsSeparator clsSeparator1;
        private ClsSeparator clsSeparator2;
        private DevExpress.XtraEditors.SimpleButton BtnView;
        private System.Windows.Forms.Button BtnDescription;
        private System.Windows.Forms.Button BtnUOM;
        private System.Windows.Forms.Button BtnAltUOM;
        private System.Windows.Forms.Button BtnCategory;
        private System.Windows.Forms.Label label2;
        private MrTextBox TxtVat;
        private MrTextBox TxtSubGroup;
        private MrTextBox TxtSalesRate;
        private MrTextBox TxtConvQty;
        private MrTextBox TxtConvAltQty;
        private MrTextBox TxtAltUnit;
        private MrTextBox TxtUnit;
        private MrTextBox TxtShortName;
        private MrTextBox TxtDescription;
        private MrTextBox TxtRate;
        private MrTextBox TxtCategory;
        private System.Windows.Forms.Label label3;
        private MrTextBox TxtItemCode;
        private MrPanel StorePanel;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}