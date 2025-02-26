using DevExpress.XtraEditors;
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.SchoolTime.Stationary
{
    partial class FrmBook
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
            this.BtnAuthor = new System.Windows.Forms.Button();
            this.TxtAuthor = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnUnit = new System.Windows.Forms.Button();
            this.BtnPublication = new System.Windows.Forms.Button();
            this.TxtSubGroup = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lvlProductSubGroup = new System.Windows.Forms.Label();
            this.TxtGroup = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnSubGroup = new System.Windows.Forms.Button();
            this.lvlProductGroup = new System.Windows.Forms.Label();
            this.TxtUnit = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnGroup = new System.Windows.Forms.Button();
            this.lvlProductUnit = new System.Windows.Forms.Label();
            this.BtnISBN = new System.Windows.Forms.Button();
            this.BtnDescription = new System.Windows.Forms.Button();
            this.TxtAlias = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lvlProductName = new System.Windows.Forms.Label();
            this.lvlProductCode = new System.Windows.Forms.Label();
            this.TxtISBN = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtPublication = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.xtbProduct = new DevExpress.XtraTab.XtraTabControl();
            this.tbInformation = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lb = new System.Windows.Forms.Label();
            this.TxtTradeRate = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtPurRate = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtSalesRate = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtMrp = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lvlProductPurchaseRate = new System.Windows.Forms.Label();
            this.lvlProductSalesRate = new System.Windows.Forms.Label();
            this.lvlProductMRPRate = new System.Windows.Forms.Label();
            this.tbImage = new DevExpress.XtraTab.XtraTabPage();
            this.LinkAttachment1 = new System.Windows.Forms.LinkLabel();
            this.ChkIsTaxable = new System.Windows.Forms.CheckBox();
            this.ChkActive = new System.Windows.Forms.CheckBox();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvlProductMinStock = new System.Windows.Forms.Label();
            this.txtMinQty = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtReorderLevel = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtReorderStock = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMaxQy = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lvlProductMaxStock = new System.Windows.Forms.Label();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.xtbProduct)).BeginInit();
            this.xtbProduct.SuspendLayout();
            this.tbInformation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.tbImage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnAuthor
            // 
            this.BtnAuthor.CausesValidation = false;
            this.BtnAuthor.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAuthor.ForeColor = System.Drawing.Color.Transparent;
            this.BtnAuthor.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnAuthor.Location = new System.Drawing.Point(470, 130);
            this.BtnAuthor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnAuthor.Name = "BtnAuthor";
            this.BtnAuthor.Size = new System.Drawing.Size(26, 23);
            this.BtnAuthor.TabIndex = 52;
            this.BtnAuthor.TabStop = false;
            this.BtnAuthor.UseVisualStyleBackColor = false;
            this.BtnAuthor.Click += new System.EventHandler(this.BtnAuthor_Click);
            // 
            // TxtAuthor
            // 
            this.TxtAuthor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAuthor.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAuthor.Location = new System.Drawing.Point(104, 130);
            this.TxtAuthor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtAuthor.MaxLength = 100;
            this.TxtAuthor.Name = "TxtAuthor";
            this.TxtAuthor.Size = new System.Drawing.Size(364, 23);
            this.TxtAuthor.TabIndex = 6;
            this.TxtAuthor.Enter += new System.EventHandler(this.Ctrl_Enter);
            this.TxtAuthor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtWriter_KeyDown);
            this.TxtAuthor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_KeyPress);
            this.TxtAuthor.Leave += new System.EventHandler(this.Ctrl_Leave);
            this.TxtAuthor.Validating += new System.ComponentModel.CancelEventHandler(this.TxtWriter_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 132);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 19);
            this.label1.TabIndex = 50;
            this.label1.Text = "Author";
            // 
            // BtnUnit
            // 
            this.BtnUnit.CausesValidation = false;
            this.BtnUnit.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnUnit.Location = new System.Drawing.Point(470, 6);
            this.BtnUnit.Name = "BtnUnit";
            this.BtnUnit.Size = new System.Drawing.Size(26, 23);
            this.BtnUnit.TabIndex = 49;
            this.BtnUnit.TabStop = false;
            this.BtnUnit.UseVisualStyleBackColor = false;
            this.BtnUnit.Click += new System.EventHandler(this.BtnUnit_Click);
            // 
            // BtnPublication
            // 
            this.BtnPublication.CausesValidation = false;
            this.BtnPublication.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPublication.ForeColor = System.Drawing.Color.Transparent;
            this.BtnPublication.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnPublication.Location = new System.Drawing.Point(470, 105);
            this.BtnPublication.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnPublication.Name = "BtnPublication";
            this.BtnPublication.Size = new System.Drawing.Size(26, 23);
            this.BtnPublication.TabIndex = 46;
            this.BtnPublication.TabStop = false;
            this.BtnPublication.UseVisualStyleBackColor = false;
            this.BtnPublication.Click += new System.EventHandler(this.BtnPublication_Click);
            // 
            // TxtSubGroup
            // 
            this.TxtSubGroup.BackColor = System.Drawing.Color.White;
            this.TxtSubGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSubGroup.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSubGroup.Location = new System.Drawing.Point(104, 80);
            this.TxtSubGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtSubGroup.MaxLength = 200;
            this.TxtSubGroup.Name = "TxtSubGroup";
            this.TxtSubGroup.ReadOnly = true;
            this.TxtSubGroup.Size = new System.Drawing.Size(364, 23);
            this.TxtSubGroup.TabIndex = 4;
            this.TxtSubGroup.Enter += new System.EventHandler(this.Ctrl_Enter);
            this.TxtSubGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSubGroup_KeyDown);
            this.TxtSubGroup.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_KeyPress);
            this.TxtSubGroup.Leave += new System.EventHandler(this.Ctrl_Leave);
            this.TxtSubGroup.Validating += new System.ComponentModel.CancelEventHandler(this.TxtSubGroup_Validating);
            // 
            // lvlProductSubGroup
            // 
            this.lvlProductSubGroup.AutoSize = true;
            this.lvlProductSubGroup.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductSubGroup.Location = new System.Drawing.Point(7, 80);
            this.lvlProductSubGroup.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lvlProductSubGroup.Name = "lvlProductSubGroup";
            this.lvlProductSubGroup.Size = new System.Drawing.Size(79, 19);
            this.lvlProductSubGroup.TabIndex = 41;
            this.lvlProductSubGroup.Text = "SubGroup";
            // 
            // TxtGroup
            // 
            this.TxtGroup.BackColor = System.Drawing.Color.White;
            this.TxtGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtGroup.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtGroup.Location = new System.Drawing.Point(104, 55);
            this.TxtGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtGroup.MaxLength = 200;
            this.TxtGroup.Name = "TxtGroup";
            this.TxtGroup.ReadOnly = true;
            this.TxtGroup.Size = new System.Drawing.Size(364, 23);
            this.TxtGroup.TabIndex = 3;
            this.TxtGroup.Enter += new System.EventHandler(this.Ctrl_Enter);
            this.TxtGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtGroup_KeyDown);
            this.TxtGroup.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_KeyPress);
            this.TxtGroup.Leave += new System.EventHandler(this.Ctrl_Leave);
            this.TxtGroup.Validating += new System.ComponentModel.CancelEventHandler(this.TxtGroup_Validating);
            // 
            // BtnSubGroup
            // 
            this.BtnSubGroup.CausesValidation = false;
            this.BtnSubGroup.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSubGroup.ForeColor = System.Drawing.Color.Transparent;
            this.BtnSubGroup.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnSubGroup.Location = new System.Drawing.Point(470, 80);
            this.BtnSubGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnSubGroup.Name = "BtnSubGroup";
            this.BtnSubGroup.Size = new System.Drawing.Size(26, 23);
            this.BtnSubGroup.TabIndex = 43;
            this.BtnSubGroup.TabStop = false;
            this.BtnSubGroup.UseVisualStyleBackColor = false;
            this.BtnSubGroup.Click += new System.EventHandler(this.BtnSubGroup_Click);
            // 
            // lvlProductGroup
            // 
            this.lvlProductGroup.AutoSize = true;
            this.lvlProductGroup.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductGroup.Location = new System.Drawing.Point(7, 56);
            this.lvlProductGroup.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lvlProductGroup.Name = "lvlProductGroup";
            this.lvlProductGroup.Size = new System.Drawing.Size(56, 19);
            this.lvlProductGroup.TabIndex = 38;
            this.lvlProductGroup.Text = "Group ";
            // 
            // TxtUnit
            // 
            this.TxtUnit.BackColor = System.Drawing.Color.White;
            this.TxtUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtUnit.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUnit.Location = new System.Drawing.Point(371, 6);
            this.TxtUnit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtUnit.Name = "TxtUnit";
            this.TxtUnit.ReadOnly = true;
            this.TxtUnit.Size = new System.Drawing.Size(97, 23);
            this.TxtUnit.TabIndex = 1;
            this.TxtUnit.Enter += new System.EventHandler(this.Ctrl_Enter);
            this.TxtUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtUnit_KeyDown);
            this.TxtUnit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_KeyPress);
            this.TxtUnit.Leave += new System.EventHandler(this.TxtUnit_Leave);
            this.TxtUnit.Validating += new System.ComponentModel.CancelEventHandler(this.TxtUnit_Validating);
            // 
            // BtnGroup
            // 
            this.BtnGroup.CausesValidation = false;
            this.BtnGroup.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGroup.ForeColor = System.Drawing.Color.Transparent;
            this.BtnGroup.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnGroup.Location = new System.Drawing.Point(470, 55);
            this.BtnGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnGroup.Name = "BtnGroup";
            this.BtnGroup.Size = new System.Drawing.Size(26, 23);
            this.BtnGroup.TabIndex = 40;
            this.BtnGroup.TabStop = false;
            this.BtnGroup.UseVisualStyleBackColor = false;
            this.BtnGroup.Click += new System.EventHandler(this.BtnGroup_Click);
            // 
            // lvlProductUnit
            // 
            this.lvlProductUnit.AutoSize = true;
            this.lvlProductUnit.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductUnit.Location = new System.Drawing.Point(331, 9);
            this.lvlProductUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lvlProductUnit.Name = "lvlProductUnit";
            this.lvlProductUnit.Size = new System.Drawing.Size(39, 19);
            this.lvlProductUnit.TabIndex = 47;
            this.lvlProductUnit.Text = "Unit";
            // 
            // BtnISBN
            // 
            this.BtnISBN.CausesValidation = false;
            this.BtnISBN.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnISBN.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnISBN.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnISBN.Location = new System.Drawing.Point(295, 5);
            this.BtnISBN.Name = "BtnISBN";
            this.BtnISBN.Size = new System.Drawing.Size(28, 23);
            this.BtnISBN.TabIndex = 32;
            this.BtnISBN.TabStop = false;
            this.BtnISBN.UseVisualStyleBackColor = true;
            this.BtnISBN.Click += new System.EventHandler(this.BtnISBN_Click);
            // 
            // BtnDescription
            // 
            this.BtnDescription.CausesValidation = false;
            this.BtnDescription.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnDescription.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnDescription.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDescription.Location = new System.Drawing.Point(470, 31);
            this.BtnDescription.Name = "BtnDescription";
            this.BtnDescription.Size = new System.Drawing.Size(26, 23);
            this.BtnDescription.TabIndex = 35;
            this.BtnDescription.TabStop = false;
            this.BtnDescription.UseVisualStyleBackColor = false;
            this.BtnDescription.Click += new System.EventHandler(this.BtnDescription_Click);
            // 
            // TxtAlias
            // 
            this.TxtAlias.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAlias.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAlias.Location = new System.Drawing.Point(104, 31);
            this.TxtAlias.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtAlias.MaxLength = 200;
            this.TxtAlias.Name = "TxtAlias";
            this.TxtAlias.Size = new System.Drawing.Size(364, 23);
            this.TxtAlias.TabIndex = 2;
            this.TxtAlias.Enter += new System.EventHandler(this.Ctrl_Enter);
            this.TxtAlias.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtAlias_KeyDown);
            this.TxtAlias.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_KeyPress);
            this.TxtAlias.Leave += new System.EventHandler(this.TxtAlias_Leave);
            this.TxtAlias.Validating += new System.ComponentModel.CancelEventHandler(this.TxtAlias_Validating);
            // 
            // lvlProductName
            // 
            this.lvlProductName.AutoSize = true;
            this.lvlProductName.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductName.Location = new System.Drawing.Point(7, 33);
            this.lvlProductName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lvlProductName.Name = "lvlProductName";
            this.lvlProductName.Size = new System.Drawing.Size(41, 19);
            this.lvlProductName.TabIndex = 33;
            this.lvlProductName.Text = "Title";
            // 
            // lvlProductCode
            // 
            this.lvlProductCode.AutoSize = true;
            this.lvlProductCode.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductCode.Location = new System.Drawing.Point(7, 9);
            this.lvlProductCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lvlProductCode.Name = "lvlProductCode";
            this.lvlProductCode.Size = new System.Drawing.Size(43, 19);
            this.lvlProductCode.TabIndex = 30;
            this.lvlProductCode.Text = "ISBN";
            // 
            // TxtISBN
            // 
            this.TxtISBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtISBN.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtISBN.Location = new System.Drawing.Point(104, 5);
            this.TxtISBN.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtISBN.MaxLength = 200;
            this.TxtISBN.Name = "TxtISBN";
            this.TxtISBN.Size = new System.Drawing.Size(190, 23);
            this.TxtISBN.TabIndex = 0;
            this.TxtISBN.Enter += new System.EventHandler(this.Ctrl_Enter);
            this.TxtISBN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtISBN_KeyDown);
            this.TxtISBN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_KeyPress);
            this.TxtISBN.Leave += new System.EventHandler(this.TxtISBN_Leave);
            this.TxtISBN.Validating += new System.ComponentModel.CancelEventHandler(this.TxtISBN_Validating);
            // 
            // TxtPublication
            // 
            this.TxtPublication.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPublication.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPublication.Location = new System.Drawing.Point(104, 105);
            this.TxtPublication.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtPublication.MaxLength = 100;
            this.TxtPublication.Name = "TxtPublication";
            this.TxtPublication.Size = new System.Drawing.Size(364, 23);
            this.TxtPublication.TabIndex = 5;
            this.TxtPublication.Enter += new System.EventHandler(this.Ctrl_Enter);
            this.TxtPublication.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPublication_KeyDown);
            this.TxtPublication.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_KeyPress);
            this.TxtPublication.Leave += new System.EventHandler(this.Ctrl_Leave);
            this.TxtPublication.Validating += new System.ComponentModel.CancelEventHandler(this.TxtPublication_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 107);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 19);
            this.label4.TabIndex = 44;
            this.label4.Text = "Publication";
            // 
            // xtbProduct
            // 
            this.xtbProduct.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.xtbProduct.Appearance.Options.UseFont = true;
            this.xtbProduct.AppearancePage.Header.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.xtbProduct.AppearancePage.Header.Options.UseFont = true;
            this.xtbProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtbProduct.Location = new System.Drawing.Point(0, 0);
            this.xtbProduct.Name = "xtbProduct";
            this.xtbProduct.SelectedTabPage = this.tbInformation;
            this.xtbProduct.Size = new System.Drawing.Size(502, 252);
            this.xtbProduct.TabIndex = 0;
            this.xtbProduct.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tbInformation,
            this.tbImage});
            // 
            // tbInformation
            // 
            this.tbInformation.Appearance.Header.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tbInformation.Appearance.Header.Options.UseBackColor = true;
            this.tbInformation.Appearance.PageClient.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tbInformation.Appearance.PageClient.Options.UseBackColor = true;
            this.tbInformation.Controls.Add(this.panelControl1);
            this.tbInformation.Controls.Add(this.lb);
            this.tbInformation.Controls.Add(this.TxtTradeRate);
            this.tbInformation.Controls.Add(this.TxtPurRate);
            this.tbInformation.Controls.Add(this.TxtSalesRate);
            this.tbInformation.Controls.Add(this.TxtMrp);
            this.tbInformation.Controls.Add(this.lvlProductPurchaseRate);
            this.tbInformation.Controls.Add(this.lvlProductSalesRate);
            this.tbInformation.Controls.Add(this.lvlProductMRPRate);
            this.tbInformation.Controls.Add(this.TxtISBN);
            this.tbInformation.Controls.Add(this.BtnAuthor);
            this.tbInformation.Controls.Add(this.BtnGroup);
            this.tbInformation.Controls.Add(this.TxtUnit);
            this.tbInformation.Controls.Add(this.TxtAuthor);
            this.tbInformation.Controls.Add(this.lvlProductUnit);
            this.tbInformation.Controls.Add(this.lvlProductGroup);
            this.tbInformation.Controls.Add(this.label1);
            this.tbInformation.Controls.Add(this.BtnISBN);
            this.tbInformation.Controls.Add(this.label4);
            this.tbInformation.Controls.Add(this.BtnSubGroup);
            this.tbInformation.Controls.Add(this.BtnUnit);
            this.tbInformation.Controls.Add(this.BtnDescription);
            this.tbInformation.Controls.Add(this.TxtPublication);
            this.tbInformation.Controls.Add(this.TxtGroup);
            this.tbInformation.Controls.Add(this.BtnPublication);
            this.tbInformation.Controls.Add(this.TxtAlias);
            this.tbInformation.Controls.Add(this.lvlProductCode);
            this.tbInformation.Controls.Add(this.lvlProductSubGroup);
            this.tbInformation.Controls.Add(this.TxtSubGroup);
            this.tbInformation.Controls.Add(this.lvlProductName);
            this.tbInformation.Name = "tbInformation";
            this.tbInformation.Size = new System.Drawing.Size(500, 220);
            this.tbInformation.Text = "Information";
            // 
            // panelControl1
            // 
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 210);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(500, 10);
            this.panelControl1.TabIndex = 61;
            // 
            // lb
            // 
            this.lb.AutoSize = true;
            this.lb.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb.Location = new System.Drawing.Point(255, 186);
            this.lb.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb.Name = "lb";
            this.lb.Size = new System.Drawing.Size(92, 19);
            this.lb.TabIndex = 60;
            this.lb.Text = "Trade Rate";
            // 
            // TxtTradeRate
            // 
            this.TxtTradeRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTradeRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTradeRate.Location = new System.Drawing.Point(352, 183);
            this.TxtTradeRate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtTradeRate.MaxLength = 50;
            this.TxtTradeRate.Name = "TxtTradeRate";
            this.TxtTradeRate.Size = new System.Drawing.Size(116, 25);
            this.TxtTradeRate.TabIndex = 10;
            this.TxtTradeRate.Text = "0.00";
            this.TxtTradeRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtTradeRate.Enter += new System.EventHandler(this.Ctrl_Enter);
            this.TxtTradeRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtTradeRate_KeyDown);
            this.TxtTradeRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtTradeRate_KeyPress);
            this.TxtTradeRate.Leave += new System.EventHandler(this.Ctrl_Leave);
            this.TxtTradeRate.Validating += new System.ComponentModel.CancelEventHandler(this.TxtTradeRate_Validating);
            // 
            // TxtPurRate
            // 
            this.TxtPurRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPurRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPurRate.Location = new System.Drawing.Point(353, 155);
            this.TxtPurRate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtPurRate.MaxLength = 50;
            this.TxtPurRate.Name = "TxtPurRate";
            this.TxtPurRate.Size = new System.Drawing.Size(115, 25);
            this.TxtPurRate.TabIndex = 8;
            this.TxtPurRate.Text = "0.00";
            this.TxtPurRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtPurRate.Enter += new System.EventHandler(this.Ctrl_Enter);
            this.TxtPurRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPurRate_KeyPress);
            this.TxtPurRate.Leave += new System.EventHandler(this.Ctrl_Leave);
            this.TxtPurRate.Validating += new System.ComponentModel.CancelEventHandler(this.TxtPurRate_Validating);
            // 
            // TxtSalesRate
            // 
            this.TxtSalesRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSalesRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSalesRate.Location = new System.Drawing.Point(104, 183);
            this.TxtSalesRate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtSalesRate.MaxLength = 50;
            this.TxtSalesRate.Name = "TxtSalesRate";
            this.TxtSalesRate.Size = new System.Drawing.Size(120, 25);
            this.TxtSalesRate.TabIndex = 9;
            this.TxtSalesRate.Text = "0.00";
            this.TxtSalesRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtSalesRate.Enter += new System.EventHandler(this.Ctrl_Enter);
            this.TxtSalesRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtSalesRate_KeyPress);
            this.TxtSalesRate.Leave += new System.EventHandler(this.Ctrl_Leave);
            this.TxtSalesRate.Validating += new System.ComponentModel.CancelEventHandler(this.TxtSalesRate_Validating);
            // 
            // TxtMrp
            // 
            this.TxtMrp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMrp.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtMrp.Location = new System.Drawing.Point(104, 156);
            this.TxtMrp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtMrp.MaxLength = 50;
            this.TxtMrp.Name = "TxtMrp";
            this.TxtMrp.Size = new System.Drawing.Size(120, 25);
            this.TxtMrp.TabIndex = 7;
            this.TxtMrp.Text = "0.00";
            this.TxtMrp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtMrp.Enter += new System.EventHandler(this.Ctrl_Enter);
            this.TxtMrp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtMrp_KeyPress);
            this.TxtMrp.Leave += new System.EventHandler(this.Ctrl_Leave);
            this.TxtMrp.Validating += new System.ComponentModel.CancelEventHandler(this.TxtMrp_Validating);
            // 
            // lvlProductPurchaseRate
            // 
            this.lvlProductPurchaseRate.AutoSize = true;
            this.lvlProductPurchaseRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductPurchaseRate.Location = new System.Drawing.Point(275, 158);
            this.lvlProductPurchaseRate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lvlProductPurchaseRate.Name = "lvlProductPurchaseRate";
            this.lvlProductPurchaseRate.Size = new System.Drawing.Size(78, 19);
            this.lvlProductPurchaseRate.TabIndex = 55;
            this.lvlProductPurchaseRate.Text = "Buy Rate";
            // 
            // lvlProductSalesRate
            // 
            this.lvlProductSalesRate.AutoSize = true;
            this.lvlProductSalesRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductSalesRate.Location = new System.Drawing.Point(7, 186);
            this.lvlProductSalesRate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lvlProductSalesRate.Name = "lvlProductSalesRate";
            this.lvlProductSalesRate.Size = new System.Drawing.Size(90, 19);
            this.lvlProductSalesRate.TabIndex = 56;
            this.lvlProductSalesRate.Text = "Sales Rate";
            // 
            // lvlProductMRPRate
            // 
            this.lvlProductMRPRate.AutoSize = true;
            this.lvlProductMRPRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductMRPRate.Location = new System.Drawing.Point(7, 159);
            this.lvlProductMRPRate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lvlProductMRPRate.Name = "lvlProductMRPRate";
            this.lvlProductMRPRate.Size = new System.Drawing.Size(82, 19);
            this.lvlProductMRPRate.TabIndex = 57;
            this.lvlProductMRPRate.Text = "MRP Rate";
            // 
            // tbImage
            // 
            this.tbImage.Controls.Add(this.LinkAttachment1);
            this.tbImage.Controls.Add(this.ChkIsTaxable);
            this.tbImage.Controls.Add(this.ChkActive);
            this.tbImage.Controls.Add(this.BtnSave);
            this.tbImage.Controls.Add(this.BtnCancel);
            this.tbImage.Controls.Add(this.groupBox1);
            this.tbImage.Controls.Add(this.pictureEdit1);
            this.tbImage.Name = "tbImage";
            this.tbImage.Size = new System.Drawing.Size(500, 220);
            this.tbImage.Text = "Picture";
            // 
            // LinkAttachment1
            // 
            this.LinkAttachment1.AutoSize = true;
            this.LinkAttachment1.Location = new System.Drawing.Point(3, 164);
            this.LinkAttachment1.Name = "LinkAttachment1";
            this.LinkAttachment1.Size = new System.Drawing.Size(45, 13);
            this.LinkAttachment1.TabIndex = 361;
            this.LinkAttachment1.TabStop = true;
            this.LinkAttachment1.Text = "Preview";
            this.LinkAttachment1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkAttachment1_LinkClicked);
            // 
            // ChkIsTaxable
            // 
            this.ChkIsTaxable.Checked = true;
            this.ChkIsTaxable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkIsTaxable.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ChkIsTaxable.Location = new System.Drawing.Point(140, 186);
            this.ChkIsTaxable.Name = "ChkIsTaxable";
            this.ChkIsTaxable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIsTaxable.Size = new System.Drawing.Size(105, 24);
            this.ChkIsTaxable.TabIndex = 16;
            this.ChkIsTaxable.Text = "Taxable";
            this.ChkIsTaxable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIsTaxable.ThreeState = true;
            this.ChkIsTaxable.UseVisualStyleBackColor = true;
            this.ChkIsTaxable.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_KeyPress);
            // 
            // ChkActive
            // 
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ChkActive.Location = new System.Drawing.Point(14, 186);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkActive.Size = new System.Drawing.Size(105, 24);
            this.ChkActive.TabIndex = 15;
            this.ChkActive.Text = "Active";
            this.ChkActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkActive.ThreeState = true;
            this.ChkActive.UseVisualStyleBackColor = true;
            this.ChkActive.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_KeyPress);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Appearance.Options.UseForeColor = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(283, 182);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(105, 33);
            this.BtnSave.TabIndex = 17;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.Appearance.Options.UseForeColor = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.BtnCancel.Location = new System.Drawing.Point(388, 182);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(101, 33);
            this.BtnCancel.TabIndex = 18;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvlProductMinStock);
            this.groupBox1.Controls.Add(this.txtMinQty);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.TxtReorderLevel);
            this.groupBox1.Controls.Add(this.TxtReorderStock);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtMaxQy);
            this.groupBox1.Controls.Add(this.lvlProductMaxStock);
            this.groupBox1.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.groupBox1.Location = new System.Drawing.Point(227, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(269, 173);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ADDITIONAL INFORMATION";
            // 
            // lvlProductMinStock
            // 
            this.lvlProductMinStock.AutoSize = true;
            this.lvlProductMinStock.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductMinStock.Location = new System.Drawing.Point(7, 79);
            this.lvlProductMinStock.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lvlProductMinStock.Name = "lvlProductMinStock";
            this.lvlProductMinStock.Size = new System.Drawing.Size(114, 19);
            this.lvlProductMinStock.TabIndex = 324;
            this.lvlProductMinStock.Text = "Min Stock Qty";
            // 
            // txtMinQty
            // 
            this.txtMinQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMinQty.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinQty.Location = new System.Drawing.Point(131, 79);
            this.txtMinQty.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMinQty.Name = "txtMinQty";
            this.txtMinQty.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtMinQty.Size = new System.Drawing.Size(128, 25);
            this.txtMinQty.TabIndex = 13;
            this.txtMinQty.Text = "0.00";
            this.txtMinQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(7, 24);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 19);
            this.label6.TabIndex = 322;
            this.label6.Text = "ReOrder Level";
            // 
            // TxtReorderLevel
            // 
            this.TxtReorderLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtReorderLevel.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtReorderLevel.Location = new System.Drawing.Point(131, 24);
            this.TxtReorderLevel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtReorderLevel.Name = "TxtReorderLevel";
            this.TxtReorderLevel.Size = new System.Drawing.Size(128, 25);
            this.TxtReorderLevel.TabIndex = 11;
            this.TxtReorderLevel.Text = "0.00";
            this.TxtReorderLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtReorderLevel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_KeyPress);
            // 
            // TxtReorderStock
            // 
            this.TxtReorderStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtReorderStock.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtReorderStock.Location = new System.Drawing.Point(131, 51);
            this.TxtReorderStock.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtReorderStock.Name = "TxtReorderStock";
            this.TxtReorderStock.Size = new System.Drawing.Size(128, 25);
            this.TxtReorderStock.TabIndex = 12;
            this.TxtReorderStock.Text = "0.00";
            this.TxtReorderStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtReorderStock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 51);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 19);
            this.label5.TabIndex = 323;
            this.label5.Text = "ReOrder Stock";
            // 
            // txtMaxQy
            // 
            this.txtMaxQy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaxQy.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxQy.Location = new System.Drawing.Point(131, 107);
            this.txtMaxQy.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMaxQy.Name = "txtMaxQy";
            this.txtMaxQy.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtMaxQy.Size = new System.Drawing.Size(128, 25);
            this.txtMaxQy.TabIndex = 14;
            this.txtMaxQy.Text = "0.00";
            this.txtMaxQy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_KeyPress);
            // 
            // lvlProductMaxStock
            // 
            this.lvlProductMaxStock.AutoSize = true;
            this.lvlProductMaxStock.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductMaxStock.Location = new System.Drawing.Point(7, 107);
            this.lvlProductMaxStock.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lvlProductMaxStock.Name = "lvlProductMaxStock";
            this.lvlProductMaxStock.Size = new System.Drawing.Size(116, 19);
            this.lvlProductMaxStock.TabIndex = 325;
            this.lvlProductMaxStock.Text = "Max Stock Qty";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Location = new System.Drawing.Point(3, 3);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.pictureEdit1.Properties.Appearance.Options.UseFont = true;
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit1.Size = new System.Drawing.Size(218, 174);
            this.pictureEdit1.TabIndex = 0;
            this.pictureEdit1.DoubleClick += new System.EventHandler(this.PictureEdit1_DoubleClick);
            this.pictureEdit1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PictureEdit1_KeyDown);
            // 
            // FrmBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 252);
            this.Controls.Add(this.xtbProduct);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmBook";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BOOK INFORMATION SETUP";
            this.Load += new System.EventHandler(this.Book_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Book_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.xtbProduct)).EndInit();
            this.xtbProduct.ResumeLayout(false);
            this.tbInformation.ResumeLayout(false);
            this.tbInformation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.tbImage.ResumeLayout(false);
            this.tbImage.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnAuthor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnUnit;
        private System.Windows.Forms.Button BtnPublication;
        private System.Windows.Forms.Label lvlProductSubGroup;
        private System.Windows.Forms.Button BtnSubGroup;
        private System.Windows.Forms.Label lvlProductGroup;
        private System.Windows.Forms.Button BtnGroup;
        private System.Windows.Forms.Label lvlProductUnit;
        private System.Windows.Forms.Button BtnISBN;
        private System.Windows.Forms.Button BtnDescription;
        private System.Windows.Forms.Label lvlProductName;
        private System.Windows.Forms.Label lvlProductCode;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraTab.XtraTabControl xtbProduct;
        private DevExpress.XtraTab.XtraTabPage tbInformation;
        private DevExpress.XtraTab.XtraTabPage tbImage;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private System.Windows.Forms.Label lb;
        private System.Windows.Forms.Label lvlProductPurchaseRate;
        private System.Windows.Forms.Label lvlProductSalesRate;
        private System.Windows.Forms.Label lvlProductMRPRate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lvlProductMinStock;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lvlProductMaxStock;
        private PanelControl panelControl1;
        private System.Windows.Forms.CheckBox ChkIsTaxable;
        private System.Windows.Forms.CheckBox ChkActive;
        private SimpleButton BtnSave;
        private SimpleButton BtnCancel;
        private System.Windows.Forms.LinkLabel LinkAttachment1;
        private MrTextBox TxtAuthor;
        private MrTextBox TxtSubGroup;
        private MrTextBox TxtGroup;
        private MrTextBox TxtUnit;
        private MrTextBox TxtAlias;
        private MrTextBox TxtISBN;
        private MrTextBox TxtPublication;
        private MrTextBox TxtTradeRate;
        private MrTextBox TxtPurRate;
        private MrTextBox TxtSalesRate;
        private MrTextBox TxtMrp;
        private MrTextBox txtMinQty;
        private MrTextBox TxtReorderLevel;
        private MrTextBox TxtReorderStock;
        private MrTextBox txtMaxQy;
    }
}