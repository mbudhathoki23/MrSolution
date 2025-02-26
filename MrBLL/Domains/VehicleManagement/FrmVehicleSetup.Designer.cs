using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.VehicleManagement
{
    partial class FrmVehicleSetup
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
            this.TxtDescription = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lvlProductName = new System.Windows.Forms.Label();
            this.lvlProductCode = new System.Windows.Forms.Label();
            this.BtnSubGroup = new System.Windows.Forms.Button();
            this.TxtEngineNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TPProductInfo = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.CmbVehicleType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.TxtVechileNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnVehicleNo = new System.Windows.Forms.Button();
            this.BtnColor = new System.Windows.Forms.Button();
            this.BtnModel = new System.Windows.Forms.Button();
            this.TxtColor = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtModel = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnUOM = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtChasisNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtShortName = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.BtnDescription = new System.Windows.Forms.Button();
            this.BtnCompany = new System.Windows.Forms.Button();
            this.CmbCategory = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lb = new System.Windows.Forms.Label();
            this.TxtTradeRate = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lvlProductUnit = new System.Windows.Forms.Label();
            this.TxtUOM = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnGroup = new System.Windows.Forms.Button();
            this.TxtCompany = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtBuyRate = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtSalesRate = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtMergeDesc = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtMRP = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lvlProductPurchaseRate = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lvlProductSalesRate = new System.Windows.Forms.Label();
            this.TxtMargin1 = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lvlProductMRPRate = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lvlProductGroup = new System.Windows.Forms.Label();
            this.TxtMargin = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtGroup = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lvlProductSubGroup = new System.Windows.Forms.Label();
            this.TxtSubGroup = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TabProduct = new System.Windows.Forms.TabControl();
            this.TPPictureInfo = new System.Windows.Forms.TabPage();
            this.lbl_ImageAttachment = new System.Windows.Forms.Label();
            this.lnk_PreviewImage = new System.Windows.Forms.LinkLabel();
            this.lblProductPic = new System.Windows.Forms.Label();
            this.PbPicbox = new System.Windows.Forms.PictureBox();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.ChkActive = new System.Windows.Forms.CheckBox();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.panel3 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.panel6 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.panel7 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.panel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.clsSeparator3 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.TPProductInfo.SuspendLayout();
            this.TabProduct.SuspendLayout();
            this.TPPictureInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbPicbox)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxtDescription
            // 
            this.TxtDescription.AcceptsReturn = true;
            this.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDescription.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDescription.Location = new System.Drawing.Point(99, 5);
            this.TxtDescription.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(500, 25);
            this.TxtDescription.TabIndex = 0;
            this.TxtDescription.TextChanged += new System.EventHandler(this.TxtDescription_TextChanged);
            this.TxtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDescription_KeyDown);
            this.TxtDescription.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDescription_Validating);
            // 
            // lvlProductName
            // 
            this.lvlProductName.AutoSize = true;
            this.lvlProductName.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductName.Location = new System.Drawing.Point(4, 8);
            this.lvlProductName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lvlProductName.Name = "lvlProductName";
            this.lvlProductName.Size = new System.Drawing.Size(95, 19);
            this.lvlProductName.TabIndex = 0;
            this.lvlProductName.Text = "Description";
            // 
            // lvlProductCode
            // 
            this.lvlProductCode.AutoSize = true;
            this.lvlProductCode.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductCode.Location = new System.Drawing.Point(319, 121);
            this.lvlProductCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lvlProductCode.Name = "lvlProductCode";
            this.lvlProductCode.Size = new System.Drawing.Size(85, 19);
            this.lvlProductCode.TabIndex = 2;
            this.lvlProductCode.Text = "Engine No";
            // 
            // BtnSubGroup
            // 
            this.BtnSubGroup.CausesValidation = false;
            this.BtnSubGroup.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSubGroup.ForeColor = System.Drawing.Color.Transparent;
            this.BtnSubGroup.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnSubGroup.Location = new System.Drawing.Point(599, 201);
            this.BtnSubGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnSubGroup.Name = "BtnSubGroup";
            this.BtnSubGroup.Size = new System.Drawing.Size(28, 25);
            this.BtnSubGroup.TabIndex = 60;
            this.BtnSubGroup.TabStop = false;
            this.BtnSubGroup.UseVisualStyleBackColor = false;
            this.BtnSubGroup.Click += new System.EventHandler(this.BtnSubGroup_Click);
            // 
            // TxtEngineNo
            // 
            this.TxtEngineNo.AcceptsReturn = true;
            this.TxtEngineNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtEngineNo.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtEngineNo.Location = new System.Drawing.Point(415, 118);
            this.TxtEngineNo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtEngineNo.Name = "TxtEngineNo";
            this.TxtEngineNo.Size = new System.Drawing.Size(211, 25);
            this.TxtEngineNo.TabIndex = 8;
            this.TxtEngineNo.TextChanged += new System.EventHandler(this.TxtEngineNo_TextChanged);
            this.TxtEngineNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtEngineNo_KeyDown);
            this.TxtEngineNo.Validating += new System.ComponentModel.CancelEventHandler(this.TxtEngineNo_Validating);
            // 
            // TPProductInfo
            // 
            this.TPProductInfo.AutoScroll = true;
            this.TPProductInfo.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.TPProductInfo.CausesValidation = false;
            this.TPProductInfo.Controls.Add(this.label11);
            this.TPProductInfo.Controls.Add(this.CmbVehicleType);
            this.TPProductInfo.Controls.Add(this.label10);
            this.TPProductInfo.Controls.Add(this.TxtVechileNo);
            this.TPProductInfo.Controls.Add(this.BtnVehicleNo);
            this.TPProductInfo.Controls.Add(this.BtnColor);
            this.TPProductInfo.Controls.Add(this.BtnModel);
            this.TPProductInfo.Controls.Add(this.TxtColor);
            this.TPProductInfo.Controls.Add(this.TxtModel);
            this.TPProductInfo.Controls.Add(this.BtnUOM);
            this.TPProductInfo.Controls.Add(this.label9);
            this.TPProductInfo.Controls.Add(this.label8);
            this.TPProductInfo.Controls.Add(this.label6);
            this.TPProductInfo.Controls.Add(this.TxtChasisNo);
            this.TPProductInfo.Controls.Add(this.TxtShortName);
            this.TPProductInfo.Controls.Add(this.label5);
            this.TPProductInfo.Controls.Add(this.BtnDescription);
            this.TPProductInfo.Controls.Add(this.BtnCompany);
            this.TPProductInfo.Controls.Add(this.BtnSubGroup);
            this.TPProductInfo.Controls.Add(this.TxtDescription);
            this.TPProductInfo.Controls.Add(this.lvlProductName);
            this.TPProductInfo.Controls.Add(this.lvlProductCode);
            this.TPProductInfo.Controls.Add(this.CmbCategory);
            this.TPProductInfo.Controls.Add(this.TxtEngineNo);
            this.TPProductInfo.Controls.Add(this.label7);
            this.TPProductInfo.Controls.Add(this.lb);
            this.TPProductInfo.Controls.Add(this.TxtTradeRate);
            this.TPProductInfo.Controls.Add(this.lvlProductUnit);
            this.TPProductInfo.Controls.Add(this.TxtUOM);
            this.TPProductInfo.Controls.Add(this.BtnGroup);
            this.TPProductInfo.Controls.Add(this.TxtCompany);
            this.TPProductInfo.Controls.Add(this.label4);
            this.TPProductInfo.Controls.Add(this.TxtBuyRate);
            this.TPProductInfo.Controls.Add(this.TxtSalesRate);
            this.TPProductInfo.Controls.Add(this.TxtMergeDesc);
            this.TPProductInfo.Controls.Add(this.TxtMRP);
            this.TPProductInfo.Controls.Add(this.label3);
            this.TPProductInfo.Controls.Add(this.lvlProductPurchaseRate);
            this.TPProductInfo.Controls.Add(this.label2);
            this.TPProductInfo.Controls.Add(this.lvlProductSalesRate);
            this.TPProductInfo.Controls.Add(this.TxtMargin1);
            this.TPProductInfo.Controls.Add(this.lvlProductMRPRate);
            this.TPProductInfo.Controls.Add(this.label1);
            this.TPProductInfo.Controls.Add(this.lvlProductGroup);
            this.TPProductInfo.Controls.Add(this.TxtMargin);
            this.TPProductInfo.Controls.Add(this.TxtGroup);
            this.TPProductInfo.Controls.Add(this.lvlProductSubGroup);
            this.TPProductInfo.Controls.Add(this.TxtSubGroup);
            this.TPProductInfo.Location = new System.Drawing.Point(27, 4);
            this.TPProductInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TPProductInfo.Name = "TPProductInfo";
            this.TPProductInfo.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TPProductInfo.Size = new System.Drawing.Size(636, 310);
            this.TPProductInfo.TabIndex = 0;
            this.TPProductInfo.Text = "DETAILS";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(4, 35);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 19);
            this.label11.TabIndex = 98;
            this.label11.Text = "ShortName";
            // 
            // CmbVehicleType
            // 
            this.CmbVehicleType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.CmbVehicleType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CmbVehicleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbVehicleType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbVehicleType.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbVehicleType.FormattingEnabled = true;
            this.CmbVehicleType.Items.AddRange(new object[] {
            "Branded",
            "Used"});
            this.CmbVehicleType.Location = new System.Drawing.Point(415, 60);
            this.CmbVehicleType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CmbVehicleType.Name = "CmbVehicleType";
            this.CmbVehicleType.Size = new System.Drawing.Size(212, 27);
            this.CmbVehicleType.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(316, 64);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 19);
            this.label10.TabIndex = 97;
            this.label10.Text = "Type";
            // 
            // TxtVechileNo
            // 
            this.TxtVechileNo.AcceptsReturn = true;
            this.TxtVechileNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtVechileNo.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtVechileNo.Location = new System.Drawing.Point(415, 32);
            this.TxtVechileNo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtVechileNo.Name = "TxtVechileNo";
            this.TxtVechileNo.Size = new System.Drawing.Size(184, 25);
            this.TxtVechileNo.TabIndex = 2;
            this.TxtVechileNo.TextChanged += new System.EventHandler(this.TxtVechileNo_TextChanged);
            this.TxtVechileNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtVechileNo_KeyDown);
            this.TxtVechileNo.Leave += new System.EventHandler(this.TxtVechileNo_Leave);
            // 
            // BtnVehicleNo
            // 
            this.BtnVehicleNo.CausesValidation = false;
            this.BtnVehicleNo.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnVehicleNo.ForeColor = System.Drawing.Color.Transparent;
            this.BtnVehicleNo.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnVehicleNo.Location = new System.Drawing.Point(600, 32);
            this.BtnVehicleNo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnVehicleNo.Name = "BtnVehicleNo";
            this.BtnVehicleNo.Size = new System.Drawing.Size(28, 25);
            this.BtnVehicleNo.TabIndex = 95;
            this.BtnVehicleNo.TabStop = false;
            this.BtnVehicleNo.UseVisualStyleBackColor = false;
            this.BtnVehicleNo.Click += new System.EventHandler(this.BtnVehicleNo_Click);
            // 
            // BtnColor
            // 
            this.BtnColor.CausesValidation = false;
            this.BtnColor.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnColor.ForeColor = System.Drawing.Color.Transparent;
            this.BtnColor.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnColor.Location = new System.Drawing.Point(279, 117);
            this.BtnColor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnColor.Name = "BtnColor";
            this.BtnColor.Size = new System.Drawing.Size(25, 24);
            this.BtnColor.TabIndex = 94;
            this.BtnColor.TabStop = false;
            this.BtnColor.UseVisualStyleBackColor = false;
            this.BtnColor.Click += new System.EventHandler(this.BtnColor_Click);
            // 
            // BtnModel
            // 
            this.BtnModel.CausesValidation = false;
            this.BtnModel.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnModel.ForeColor = System.Drawing.Color.Transparent;
            this.BtnModel.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnModel.Location = new System.Drawing.Point(279, 90);
            this.BtnModel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnModel.Name = "BtnModel";
            this.BtnModel.Size = new System.Drawing.Size(25, 24);
            this.BtnModel.TabIndex = 93;
            this.BtnModel.TabStop = false;
            this.BtnModel.UseVisualStyleBackColor = false;
            this.BtnModel.Click += new System.EventHandler(this.BtnModel_Click);
            // 
            // TxtColor
            // 
            this.TxtColor.AcceptsReturn = true;
            this.TxtColor.BackColor = System.Drawing.Color.White;
            this.TxtColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtColor.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtColor.Location = new System.Drawing.Point(99, 117);
            this.TxtColor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtColor.Name = "TxtColor";
            this.TxtColor.ReadOnly = true;
            this.TxtColor.Size = new System.Drawing.Size(180, 25);
            this.TxtColor.TabIndex = 7;
            this.TxtColor.TextChanged += new System.EventHandler(this.TxtColor_TextChanged);
            this.TxtColor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtColor_KeyDown);
            this.TxtColor.Leave += new System.EventHandler(this.TxtColor_Leave);
            // 
            // TxtModel
            // 
            this.TxtModel.AcceptsReturn = true;
            this.TxtModel.BackColor = System.Drawing.Color.White;
            this.TxtModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtModel.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtModel.Location = new System.Drawing.Point(99, 90);
            this.TxtModel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtModel.Name = "TxtModel";
            this.TxtModel.ReadOnly = true;
            this.TxtModel.Size = new System.Drawing.Size(180, 25);
            this.TxtModel.TabIndex = 5;
            this.TxtModel.TextChanged += new System.EventHandler(this.TxtModel_TextChanged);
            this.TxtModel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtModel_KeyDown);
            this.TxtModel.Leave += new System.EventHandler(this.TxtModel_Leave);
            // 
            // BtnUOM
            // 
            this.BtnUOM.CausesValidation = false;
            this.BtnUOM.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnUOM.ForeColor = System.Drawing.Color.Transparent;
            this.BtnUOM.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnUOM.Location = new System.Drawing.Point(279, 146);
            this.BtnUOM.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnUOM.Name = "BtnUOM";
            this.BtnUOM.Size = new System.Drawing.Size(26, 24);
            this.BtnUOM.TabIndex = 90;
            this.BtnUOM.TabStop = false;
            this.BtnUOM.UseVisualStyleBackColor = false;
            this.BtnUOM.Click += new System.EventHandler(this.BtnUOM_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(4, 120);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 19);
            this.label9.TabIndex = 89;
            this.label9.Text = "Color";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(319, 93);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 19);
            this.label8.TabIndex = 87;
            this.label8.Text = "Chasis No";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(320, 35);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 19);
            this.label6.TabIndex = 86;
            this.label6.Text = "Vechile No";
            // 
            // TxtChasisNo
            // 
            this.TxtChasisNo.AcceptsReturn = true;
            this.TxtChasisNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtChasisNo.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtChasisNo.Location = new System.Drawing.Point(415, 90);
            this.TxtChasisNo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtChasisNo.Name = "TxtChasisNo";
            this.TxtChasisNo.Size = new System.Drawing.Size(211, 25);
            this.TxtChasisNo.TabIndex = 6;
            this.TxtChasisNo.TextChanged += new System.EventHandler(this.TxtChasisNo_TextChanged);
            this.TxtChasisNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtChasisNo_KeyDown);
            this.TxtChasisNo.Validating += new System.ComponentModel.CancelEventHandler(this.TxtChasisNo_Validating);
            // 
            // TxtShortName
            // 
            this.TxtShortName.AcceptsReturn = true;
            this.TxtShortName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtShortName.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtShortName.Location = new System.Drawing.Point(99, 32);
            this.TxtShortName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtShortName.Name = "TxtShortName";
            this.TxtShortName.Size = new System.Drawing.Size(180, 25);
            this.TxtShortName.TabIndex = 1;
            this.TxtShortName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtShortName_KeyDown);
            this.TxtShortName.Validating += new System.ComponentModel.CancelEventHandler(this.TxtShortName_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 93);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 19);
            this.label5.TabIndex = 84;
            this.label5.Text = "Model";
            // 
            // BtnDescription
            // 
            this.BtnDescription.CausesValidation = false;
            this.BtnDescription.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDescription.ForeColor = System.Drawing.Color.Transparent;
            this.BtnDescription.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDescription.Location = new System.Drawing.Point(600, 5);
            this.BtnDescription.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnDescription.Name = "BtnDescription";
            this.BtnDescription.Size = new System.Drawing.Size(28, 25);
            this.BtnDescription.TabIndex = 82;
            this.BtnDescription.TabStop = false;
            this.BtnDescription.UseVisualStyleBackColor = false;
            this.BtnDescription.Click += new System.EventHandler(this.BtnDescription_Click);
            // 
            // BtnCompany
            // 
            this.BtnCompany.CausesValidation = false;
            this.BtnCompany.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCompany.ForeColor = System.Drawing.Color.Transparent;
            this.BtnCompany.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnCompany.Location = new System.Drawing.Point(599, 145);
            this.BtnCompany.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnCompany.Name = "BtnCompany";
            this.BtnCompany.Size = new System.Drawing.Size(29, 24);
            this.BtnCompany.TabIndex = 81;
            this.BtnCompany.TabStop = false;
            this.BtnCompany.UseVisualStyleBackColor = false;
            this.BtnCompany.Click += new System.EventHandler(this.BtnCompany_Click);
            // 
            // CmbCategory
            // 
            this.CmbCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.CmbCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbCategory.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbCategory.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbCategory.FormattingEnabled = true;
            this.CmbCategory.Items.AddRange(new object[] {
            "2 Wheeler",
            "4 Wheeler"});
            this.CmbCategory.Location = new System.Drawing.Point(99, 60);
            this.CmbCategory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CmbCategory.Name = "CmbCategory";
            this.CmbCategory.Size = new System.Drawing.Size(180, 27);
            this.CmbCategory.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(4, 64);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 19);
            this.label7.TabIndex = 53;
            this.label7.Text = "Category";
            // 
            // lb
            // 
            this.lb.AutoSize = true;
            this.lb.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb.Location = new System.Drawing.Point(207, 256);
            this.lb.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb.Name = "lb";
            this.lb.Size = new System.Drawing.Size(92, 19);
            this.lb.TabIndex = 48;
            this.lb.Text = "Trade Rate";
            // 
            // TxtTradeRate
            // 
            this.TxtTradeRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTradeRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTradeRate.Location = new System.Drawing.Point(303, 254);
            this.TxtTradeRate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtTradeRate.Name = "TxtTradeRate";
            this.TxtTradeRate.Size = new System.Drawing.Size(104, 25);
            this.TxtTradeRate.TabIndex = 17;
            this.TxtTradeRate.Text = "0.00";
            this.TxtTradeRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtTradeRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtTradeRate_KeyPress);
            // 
            // lvlProductUnit
            // 
            this.lvlProductUnit.AutoSize = true;
            this.lvlProductUnit.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductUnit.Location = new System.Drawing.Point(4, 148);
            this.lvlProductUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lvlProductUnit.Name = "lvlProductUnit";
            this.lvlProductUnit.Size = new System.Drawing.Size(46, 19);
            this.lvlProductUnit.TabIndex = 5;
            this.lvlProductUnit.Text = "UOM";
            // 
            // TxtUOM
            // 
            this.TxtUOM.AcceptsReturn = true;
            this.TxtUOM.BackColor = System.Drawing.Color.White;
            this.TxtUOM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtUOM.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUOM.Location = new System.Drawing.Point(99, 145);
            this.TxtUOM.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtUOM.Name = "TxtUOM";
            this.TxtUOM.ReadOnly = true;
            this.TxtUOM.Size = new System.Drawing.Size(180, 25);
            this.TxtUOM.TabIndex = 9;
            this.TxtUOM.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtUOM_KeyDown);
            this.TxtUOM.Leave += new System.EventHandler(this.TxtUOM_Leave);
            // 
            // BtnGroup
            // 
            this.BtnGroup.CausesValidation = false;
            this.BtnGroup.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGroup.ForeColor = System.Drawing.Color.Transparent;
            this.BtnGroup.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnGroup.Location = new System.Drawing.Point(599, 174);
            this.BtnGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnGroup.Name = "BtnGroup";
            this.BtnGroup.Size = new System.Drawing.Size(29, 24);
            this.BtnGroup.TabIndex = 40;
            this.BtnGroup.TabStop = false;
            this.BtnGroup.UseVisualStyleBackColor = false;
            this.BtnGroup.Click += new System.EventHandler(this.BtnGroup_Click);
            // 
            // TxtCompany
            // 
            this.TxtCompany.AcceptsReturn = true;
            this.TxtCompany.BackColor = System.Drawing.Color.White;
            this.TxtCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCompany.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCompany.Location = new System.Drawing.Point(415, 146);
            this.TxtCompany.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtCompany.Name = "TxtCompany";
            this.TxtCompany.ReadOnly = true;
            this.TxtCompany.Size = new System.Drawing.Size(184, 25);
            this.TxtCompany.TabIndex = 10;
            this.TxtCompany.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCompany_KeyDown);
            this.TxtCompany.Leave += new System.EventHandler(this.TxtCompany_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(319, 147);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 19);
            this.label4.TabIndex = 36;
            this.label4.Text = "Company ";
            // 
            // TxtBuyRate
            // 
            this.TxtBuyRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBuyRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBuyRate.Location = new System.Drawing.Point(99, 226);
            this.TxtBuyRate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtBuyRate.Name = "TxtBuyRate";
            this.TxtBuyRate.Size = new System.Drawing.Size(119, 25);
            this.TxtBuyRate.TabIndex = 13;
            this.TxtBuyRate.Text = "0.00";
            this.TxtBuyRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtBuyRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBuyRate_KeyPress);
            // 
            // TxtSalesRate
            // 
            this.TxtSalesRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSalesRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSalesRate.Location = new System.Drawing.Point(510, 253);
            this.TxtSalesRate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtSalesRate.Name = "TxtSalesRate";
            this.TxtSalesRate.Size = new System.Drawing.Size(118, 25);
            this.TxtSalesRate.TabIndex = 18;
            this.TxtSalesRate.Text = "0.00";
            this.TxtSalesRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtSalesRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtSalesRate_KeyPress);
            // 
            // TxtMergeDesc
            // 
            this.TxtMergeDesc.BackColor = System.Drawing.Color.White;
            this.TxtMergeDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMergeDesc.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtMergeDesc.Location = new System.Drawing.Point(99, 280);
            this.TxtMergeDesc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtMergeDesc.Name = "TxtMergeDesc";
            this.TxtMergeDesc.ReadOnly = true;
            this.TxtMergeDesc.Size = new System.Drawing.Size(528, 25);
            this.TxtMergeDesc.TabIndex = 19;
            // 
            // TxtMRP
            // 
            this.TxtMRP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMRP.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtMRP.Location = new System.Drawing.Point(510, 227);
            this.TxtMRP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtMRP.Name = "TxtMRP";
            this.TxtMRP.Size = new System.Drawing.Size(118, 25);
            this.TxtMRP.TabIndex = 15;
            this.TxtMRP.Text = "0.00";
            this.TxtMRP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtMRP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtMRP_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 283);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 19);
            this.label3.TabIndex = 31;
            this.label3.Text = "Print Desc";
            // 
            // lvlProductPurchaseRate
            // 
            this.lvlProductPurchaseRate.AutoSize = true;
            this.lvlProductPurchaseRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductPurchaseRate.Location = new System.Drawing.Point(4, 229);
            this.lvlProductPurchaseRate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lvlProductPurchaseRate.Name = "lvlProductPurchaseRate";
            this.lvlProductPurchaseRate.Size = new System.Drawing.Size(78, 19);
            this.lvlProductPurchaseRate.TabIndex = 14;
            this.lvlProductPurchaseRate.Text = "Buy Rate";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 256);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 19);
            this.label2.TabIndex = 30;
            this.label2.Text = "Margin";
            // 
            // lvlProductSalesRate
            // 
            this.lvlProductSalesRate.AutoSize = true;
            this.lvlProductSalesRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductSalesRate.Location = new System.Drawing.Point(411, 256);
            this.lvlProductSalesRate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lvlProductSalesRate.Name = "lvlProductSalesRate";
            this.lvlProductSalesRate.Size = new System.Drawing.Size(90, 19);
            this.lvlProductSalesRate.TabIndex = 15;
            this.lvlProductSalesRate.Text = "Sales Rate";
            // 
            // TxtMargin1
            // 
            this.TxtMargin1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMargin1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtMargin1.Location = new System.Drawing.Point(99, 253);
            this.TxtMargin1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtMargin1.Name = "TxtMargin1";
            this.TxtMargin1.Size = new System.Drawing.Size(107, 25);
            this.TxtMargin1.TabIndex = 16;
            this.TxtMargin1.Text = "0.00";
            this.TxtMargin1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtMargin1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtMargin1_KeyPress);
            // 
            // lvlProductMRPRate
            // 
            this.lvlProductMRPRate.AutoSize = true;
            this.lvlProductMRPRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductMRPRate.Location = new System.Drawing.Point(411, 230);
            this.lvlProductMRPRate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lvlProductMRPRate.Name = "lvlProductMRPRate";
            this.lvlProductMRPRate.Size = new System.Drawing.Size(82, 19);
            this.lvlProductMRPRate.TabIndex = 15;
            this.lvlProductMRPRate.Text = "MRP Rate";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(218, 229);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 19);
            this.label1.TabIndex = 28;
            this.label1.Text = "Margin";
            // 
            // lvlProductGroup
            // 
            this.lvlProductGroup.AutoSize = true;
            this.lvlProductGroup.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductGroup.Location = new System.Drawing.Point(4, 175);
            this.lvlProductGroup.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lvlProductGroup.Name = "lvlProductGroup";
            this.lvlProductGroup.Size = new System.Drawing.Size(59, 19);
            this.lvlProductGroup.TabIndex = 16;
            this.lvlProductGroup.Text = "Group ";
            // 
            // TxtMargin
            // 
            this.TxtMargin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMargin.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtMargin.Location = new System.Drawing.Point(303, 227);
            this.TxtMargin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtMargin.Name = "TxtMargin";
            this.TxtMargin.Size = new System.Drawing.Size(104, 25);
            this.TxtMargin.TabIndex = 14;
            this.TxtMargin.Text = "0.00";
            this.TxtMargin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtMargin.TextChanged += new System.EventHandler(this.TxtMargin_TextChanged);
            this.TxtMargin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtMargin_KeyPress);
            // 
            // TxtGroup
            // 
            this.TxtGroup.AcceptsReturn = true;
            this.TxtGroup.BackColor = System.Drawing.Color.White;
            this.TxtGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtGroup.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtGroup.Location = new System.Drawing.Point(99, 173);
            this.TxtGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtGroup.Name = "TxtGroup";
            this.TxtGroup.ReadOnly = true;
            this.TxtGroup.Size = new System.Drawing.Size(500, 25);
            this.TxtGroup.TabIndex = 11;
            this.TxtGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtGroup_KeyDown);
            this.TxtGroup.Leave += new System.EventHandler(this.TxtGroup_Leave);
            // 
            // lvlProductSubGroup
            // 
            this.lvlProductSubGroup.AutoSize = true;
            this.lvlProductSubGroup.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductSubGroup.Location = new System.Drawing.Point(4, 203);
            this.lvlProductSubGroup.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lvlProductSubGroup.Name = "lvlProductSubGroup";
            this.lvlProductSubGroup.Size = new System.Drawing.Size(82, 19);
            this.lvlProductSubGroup.TabIndex = 18;
            this.lvlProductSubGroup.Text = "SubGroup";
            // 
            // TxtSubGroup
            // 
            this.TxtSubGroup.AcceptsReturn = true;
            this.TxtSubGroup.BackColor = System.Drawing.Color.White;
            this.TxtSubGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSubGroup.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSubGroup.Location = new System.Drawing.Point(99, 200);
            this.TxtSubGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtSubGroup.Name = "TxtSubGroup";
            this.TxtSubGroup.ReadOnly = true;
            this.TxtSubGroup.Size = new System.Drawing.Size(500, 25);
            this.TxtSubGroup.TabIndex = 12;
            this.TxtSubGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSubGroup_KeyDown);
            // 
            // TabProduct
            // 
            this.TabProduct.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.TabProduct.CausesValidation = false;
            this.TabProduct.Controls.Add(this.TPProductInfo);
            this.TabProduct.Controls.Add(this.TPPictureInfo);
            this.TabProduct.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabProduct.HotTrack = true;
            this.TabProduct.Location = new System.Drawing.Point(7, 40);
            this.TabProduct.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TabProduct.Multiline = true;
            this.TabProduct.Name = "TabProduct";
            this.TabProduct.SelectedIndex = 0;
            this.TabProduct.ShowToolTips = true;
            this.TabProduct.Size = new System.Drawing.Size(667, 318);
            this.TabProduct.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabProduct.TabIndex = 4;
            // 
            // TPPictureInfo
            // 
            this.TPPictureInfo.AutoScroll = true;
            this.TPPictureInfo.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.TPPictureInfo.CausesValidation = false;
            this.TPPictureInfo.Controls.Add(this.lbl_ImageAttachment);
            this.TPPictureInfo.Controls.Add(this.lnk_PreviewImage);
            this.TPPictureInfo.Controls.Add(this.lblProductPic);
            this.TPPictureInfo.Controls.Add(this.PbPicbox);
            this.TPPictureInfo.Location = new System.Drawing.Point(27, 4);
            this.TPPictureInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TPPictureInfo.Name = "TPPictureInfo";
            this.TPPictureInfo.Size = new System.Drawing.Size(636, 310);
            this.TPPictureInfo.TabIndex = 2;
            this.TPPictureInfo.Text = "PICTURE";
            // 
            // lbl_ImageAttachment
            // 
            this.lbl_ImageAttachment.AutoSize = true;
            this.lbl_ImageAttachment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_ImageAttachment.Enabled = false;
            this.lbl_ImageAttachment.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ImageAttachment.ForeColor = System.Drawing.SystemColors.Window;
            this.lbl_ImageAttachment.Location = new System.Drawing.Point(440, 172);
            this.lbl_ImageAttachment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_ImageAttachment.Name = "lbl_ImageAttachment";
            this.lbl_ImageAttachment.Size = new System.Drawing.Size(23, 16);
            this.lbl_ImageAttachment.TabIndex = 38;
            this.lbl_ImageAttachment.Text = "....";
            this.lbl_ImageAttachment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lnk_PreviewImage
            // 
            this.lnk_PreviewImage.AutoSize = true;
            this.lnk_PreviewImage.Location = new System.Drawing.Point(306, 171);
            this.lnk_PreviewImage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnk_PreviewImage.Name = "lnk_PreviewImage";
            this.lnk_PreviewImage.Size = new System.Drawing.Size(126, 19);
            this.lnk_PreviewImage.TabIndex = 37;
            this.lnk_PreviewImage.TabStop = true;
            this.lnk_PreviewImage.Text = "Picture Preview";
            this.lnk_PreviewImage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnk_PreviewImage_LinkClicked);
            // 
            // lblProductPic
            // 
            this.lblProductPic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblProductPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductPic.Enabled = false;
            this.lblProductPic.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductPic.ForeColor = System.Drawing.SystemColors.Window;
            this.lblProductPic.Location = new System.Drawing.Point(0, 0);
            this.lblProductPic.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProductPic.Name = "lblProductPic";
            this.lblProductPic.Size = new System.Drawing.Size(636, 310);
            this.lblProductPic.TabIndex = 36;
            this.lblProductPic.Text = "Double Click For Picture Upload";
            this.lblProductPic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblProductPic.DoubleClick += new System.EventHandler(this.lblProductPic_DoubleClick);
            // 
            // PbPicbox
            // 
            this.PbPicbox.BackColor = System.Drawing.SystemColors.Control;
            this.PbPicbox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PbPicbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PbPicbox.Enabled = false;
            this.PbPicbox.Location = new System.Drawing.Point(0, 0);
            this.PbPicbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PbPicbox.Name = "PbPicbox";
            this.PbPicbox.Size = new System.Drawing.Size(636, 310);
            this.PbPicbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PbPicbox.TabIndex = 35;
            this.PbPicbox.TabStop = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(554, 366);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(103, 34);
            this.BtnCancel.TabIndex = 6;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.BtnExit.Location = new System.Drawing.Point(590, 3);
            this.BtnExit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(76, 34);
            this.BtnExit.TabIndex = 3;
            this.BtnExit.Text = "E&XIT";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(472, 367);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(81, 34);
            this.BtnSave.TabIndex = 5;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // ChkActive
            // 
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.ChkActive.Location = new System.Drawing.Point(18, 374);
            this.ChkActive.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkActive.Size = new System.Drawing.Size(114, 22);
            this.ChkActive.TabIndex = 7;
            this.ChkActive.Text = "Active";
            this.ChkActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkActive.UseVisualStyleBackColor = true;
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(167, 5);
            this.BtnDelete.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(103, 34);
            this.BtnDelete.TabIndex = 2;
            this.BtnDelete.Text = "&DELETE";
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEdit.Appearance.Options.UseFont = true;
            this.BtnEdit.ImageOptions.Image = global::MrBLL.Properties.Resources.Edit;
            this.BtnEdit.Location = new System.Drawing.Point(85, 5);
            this.BtnEdit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(82, 34);
            this.BtnEdit.TabIndex = 1;
            this.BtnEdit.Text = "&EDIT";
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnNew
            // 
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnNew.Location = new System.Drawing.Point(10, 5);
            this.BtnNew.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(75, 34);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(0, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(3, 401);
            this.panel2.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(678, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(3, 401);
            this.panel3.TabIndex = 3;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.White;
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(681, 3);
            this.panel6.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel7.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel7.Location = new System.Drawing.Point(0, 404);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(681, 3);
            this.panel7.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.TabProduct);
            this.panel1.Controls.Add(this.BtnCancel);
            this.panel1.Controls.Add(this.BtnExit);
            this.panel1.Controls.Add(this.BtnSave);
            this.panel1.Controls.Add(this.ChkActive);
            this.panel1.Controls.Add(this.BtnDelete);
            this.panel1.Controls.Add(this.clsSeparator3);
            this.panel1.Controls.Add(this.BtnEdit);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.BtnNew);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(681, 407);
            this.panel1.TabIndex = 0;
            // 
            // clsSeparator3
            // 
            this.clsSeparator3.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator3.Location = new System.Drawing.Point(7, 361);
            this.clsSeparator3.Name = "clsSeparator3";
            this.clsSeparator3.Size = new System.Drawing.Size(667, 2);
            this.clsSeparator3.TabIndex = 1;
            this.clsSeparator3.TabStop = false;
            // 
            // FrmVehicleSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 407);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmVehicleSetup";
            this.ShowIcon = false;
            this.Text = "Vehicle Setup";
            this.Load += new System.EventHandler(this.FrmVehicleSetup_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmVehicleSetup_KeyPress);
            this.TPProductInfo.ResumeLayout(false);
            this.TPProductInfo.PerformLayout();
            this.TabProduct.ResumeLayout(false);
            this.TPPictureInfo.ResumeLayout(false);
            this.TPPictureInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbPicbox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lvlProductName;
        private System.Windows.Forms.Label lvlProductCode;
        private System.Windows.Forms.Button BtnSubGroup;
        private System.Windows.Forms.TabPage TPProductInfo;
        private System.Windows.Forms.ComboBox CmbCategory;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lb;
        private System.Windows.Forms.Label lvlProductUnit;
        private System.Windows.Forms.Button BtnGroup;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lvlProductPurchaseRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lvlProductSalesRate;
        private System.Windows.Forms.Label lvlProductMRPRate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lvlProductGroup;
        private System.Windows.Forms.Label lvlProductSubGroup;
        private System.Windows.Forms.TabPage TPPictureInfo;
        private System.Windows.Forms.Label lbl_ImageAttachment;
        private System.Windows.Forms.LinkLabel lnk_PreviewImage;
        private System.Windows.Forms.Label lblProductPic;
        private System.Windows.Forms.PictureBox PbPicbox;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private System.Windows.Forms.CheckBox ChkActive;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private ClsSeparator clsSeparator3;
        private System.Windows.Forms.Button BtnCompany;
        public System.Windows.Forms.TabControl TabProduct;
        private System.Windows.Forms.Button BtnDescription;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button BtnUOM;
        private System.Windows.Forms.Button BtnColor;
        private System.Windows.Forms.Button BtnModel;
        private System.Windows.Forms.Button BtnVehicleNo;
        private System.Windows.Forms.ComboBox CmbVehicleType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private MrTextBox TxtDescription;
        private MrTextBox TxtEngineNo;
        private MrTextBox TxtTradeRate;
        private MrTextBox TxtUOM;
        private MrTextBox TxtCompany;
        private MrTextBox TxtBuyRate;
        private MrTextBox TxtSalesRate;
        private MrTextBox TxtMergeDesc;
        private MrTextBox TxtMRP;
        private MrTextBox TxtMargin1;
        private MrTextBox TxtMargin;
        private MrTextBox TxtGroup;
        private MrTextBox TxtSubGroup;
        private MrPanel panel2;
        private MrPanel panel3;
        private MrPanel panel6;
        private MrPanel panel7;
        private MrPanel panel1;
        private MrTextBox TxtShortName;
        private MrTextBox TxtChasisNo;
        private MrTextBox TxtColor;
        private MrTextBox TxtModel;
        private MrTextBox TxtVechileNo;
    }
}