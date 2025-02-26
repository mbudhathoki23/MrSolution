using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Inventory_Report.ListOfMaster
{
    partial class FrmProductList
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
            this.StorePanel = new MrPanel();
            this.grouper1 = new MrGroup();
            this.rChkProductSubGroup = new System.Windows.Forms.RadioButton();
            this.rChkProduct = new System.Windows.Forms.RadioButton();
            this.rChkProductGroup = new System.Windows.Forms.RadioButton();
            this.grouper3 = new MrGroup();
            this.clsSeparator1 = new ClsSeparator();
            this.ChkIncludeProduct = new System.Windows.Forms.CheckBox();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.ChkLedgerInfo = new System.Windows.Forms.CheckBox();
            this.BtnShow = new DevExpress.XtraEditors.SimpleButton();
            this.ChkSelectAll = new System.Windows.Forms.CheckBox();
            this.ChkReOrderInfo = new System.Windows.Forms.CheckBox();
            this.ChkBonusFreeQty = new System.Windows.Forms.CheckBox();
            this.ChkPriceInfo = new System.Windows.Forms.CheckBox();
            this.ChkShortName = new System.Windows.Forms.CheckBox();
            this.rChkProductType = new System.Windows.Forms.RadioButton();
            this.rChkProductTaxType = new System.Windows.Forms.RadioButton();
            this.StorePanel.SuspendLayout();
            this.grouper1.SuspendLayout();
            this.grouper3.SuspendLayout();
            this.SuspendLayout();
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.grouper1);
            this.StorePanel.Controls.Add(this.grouper3);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(487, 191);
            this.StorePanel.TabIndex = 0;
            // 
            // grouper1
            // 
            this.grouper1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouper1.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.grouper1.BorderColor = System.Drawing.Color.White;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.rChkProductTaxType);
            this.grouper1.Controls.Add(this.rChkProductType);
            this.grouper1.Controls.Add(this.rChkProductSubGroup);
            this.grouper1.Controls.Add(this.rChkProduct);
            this.grouper1.Controls.Add(this.rChkProductGroup);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "Report Type";
            this.grouper1.Location = new System.Drawing.Point(3, 3);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 10;
            this.grouper1.ShadowColor = System.Drawing.Color.White;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 3;
            this.grouper1.Size = new System.Drawing.Size(188, 185);
            this.grouper1.TabIndex = 0;
            // 
            // rChkProductSubGroup
            // 
            this.rChkProductSubGroup.AutoSize = true;
            this.rChkProductSubGroup.Location = new System.Drawing.Point(6, 74);
            this.rChkProductSubGroup.Name = "rChkProductSubGroup";
            this.rChkProductSubGroup.Size = new System.Drawing.Size(162, 23);
            this.rChkProductSubGroup.TabIndex = 2;
            this.rChkProductSubGroup.TabStop = true;
            this.rChkProductSubGroup.Text = "Product SubGroup";
            this.rChkProductSubGroup.UseVisualStyleBackColor = true;
            // 
            // rChkProduct
            // 
            this.rChkProduct.AutoSize = true;
            this.rChkProduct.Checked = true;
            this.rChkProduct.Location = new System.Drawing.Point(6, 28);
            this.rChkProduct.Name = "rChkProduct";
            this.rChkProduct.Size = new System.Drawing.Size(84, 23);
            this.rChkProduct.TabIndex = 0;
            this.rChkProduct.TabStop = true;
            this.rChkProduct.Text = "Product";
            this.rChkProduct.UseVisualStyleBackColor = true;
            this.rChkProduct.CheckedChanged += new System.EventHandler(this.rChkProduct_CheckedChanged);
            // 
            // rChkProductGroup
            // 
            this.rChkProductGroup.AutoSize = true;
            this.rChkProductGroup.Location = new System.Drawing.Point(6, 51);
            this.rChkProductGroup.Name = "rChkProductGroup";
            this.rChkProductGroup.Size = new System.Drawing.Size(134, 23);
            this.rChkProductGroup.TabIndex = 1;
            this.rChkProductGroup.TabStop = true;
            this.rChkProductGroup.Text = "Product Group";
            this.rChkProductGroup.UseVisualStyleBackColor = true;
            // 
            // grouper3
            // 
            this.grouper3.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.grouper3.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouper3.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.grouper3.BorderColor = System.Drawing.Color.White;
            this.grouper3.BorderThickness = 1F;
            this.grouper3.Controls.Add(this.ChkShortName);
            this.grouper3.Controls.Add(this.clsSeparator1);
            this.grouper3.Controls.Add(this.ChkIncludeProduct);
            this.grouper3.Controls.Add(this.BtnCancel);
            this.grouper3.Controls.Add(this.ChkLedgerInfo);
            this.grouper3.Controls.Add(this.BtnShow);
            this.grouper3.Controls.Add(this.ChkSelectAll);
            this.grouper3.Controls.Add(this.ChkReOrderInfo);
            this.grouper3.Controls.Add(this.ChkBonusFreeQty);
            this.grouper3.Controls.Add(this.ChkPriceInfo);
            this.grouper3.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper3.GroupImage = null;
            this.grouper3.GroupTitle = "Filter For";
            this.grouper3.Location = new System.Drawing.Point(196, 3);
            this.grouper3.Name = "grouper3";
            this.grouper3.Padding = new System.Windows.Forms.Padding(20);
            this.grouper3.PaintGroupBox = false;
            this.grouper3.RoundCorners = 10;
            this.grouper3.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper3.ShadowControl = false;
            this.grouper3.ShadowThickness = 3;
            this.grouper3.Size = new System.Drawing.Size(285, 185);
            this.grouper3.TabIndex = 1;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator1.Location = new System.Drawing.Point(8, 139);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(270, 2);
            this.clsSeparator1.TabIndex = 8;
            this.clsSeparator1.TabStop = false;
            // 
            // ChkIncludeProduct
            // 
            this.ChkIncludeProduct.BackColor = System.Drawing.Color.Transparent;
            this.ChkIncludeProduct.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkIncludeProduct.Location = new System.Drawing.Point(125, 50);
            this.ChkIncludeProduct.Name = "ChkIncludeProduct";
            this.ChkIncludeProduct.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeProduct.Size = new System.Drawing.Size(153, 23);
            this.ChkIncludeProduct.TabIndex = 4;
            this.ChkIncludeProduct.Text = "Include Product";
            this.ChkIncludeProduct.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeProduct.UseVisualStyleBackColor = false;
            this.ChkIncludeProduct.CheckedChanged += new System.EventHandler(this.ChkIncludeProduct_CheckedChanged);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(161, 142);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(114, 37);
            this.BtnCancel.TabIndex = 7;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // ChkLedgerInfo
            // 
            this.ChkLedgerInfo.BackColor = System.Drawing.Color.Transparent;
            this.ChkLedgerInfo.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkLedgerInfo.Location = new System.Drawing.Point(4, 27);
            this.ChkLedgerInfo.Name = "ChkLedgerInfo";
            this.ChkLedgerInfo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkLedgerInfo.Size = new System.Drawing.Size(115, 23);
            this.ChkLedgerInfo.TabIndex = 0;
            this.ChkLedgerInfo.Text = "Ledger Info";
            this.ChkLedgerInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkLedgerInfo.UseVisualStyleBackColor = false;
            this.ChkLedgerInfo.CheckedChanged += new System.EventHandler(this.ChkLedgerInfo_CheckedChanged);
            // 
            // BtnShow
            // 
            this.BtnShow.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnShow.Appearance.Options.UseFont = true;
            this.BtnShow.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnShow.Location = new System.Drawing.Point(47, 142);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(114, 37);
            this.BtnShow.TabIndex = 6;
            this.BtnShow.Text = "&SHOW";
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // ChkSelectAll
            // 
            this.ChkSelectAll.BackColor = System.Drawing.Color.Transparent;
            this.ChkSelectAll.Checked = true;
            this.ChkSelectAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkSelectAll.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkSelectAll.Location = new System.Drawing.Point(4, 96);
            this.ChkSelectAll.Name = "ChkSelectAll";
            this.ChkSelectAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSelectAll.Size = new System.Drawing.Size(115, 23);
            this.ChkSelectAll.TabIndex = 2;
            this.ChkSelectAll.Text = "Select All";
            this.ChkSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSelectAll.UseVisualStyleBackColor = false;
            // 
            // ChkReOrderInfo
            // 
            this.ChkReOrderInfo.BackColor = System.Drawing.Color.Transparent;
            this.ChkReOrderInfo.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkReOrderInfo.Location = new System.Drawing.Point(125, 25);
            this.ChkReOrderInfo.Name = "ChkReOrderInfo";
            this.ChkReOrderInfo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkReOrderInfo.Size = new System.Drawing.Size(151, 26);
            this.ChkReOrderInfo.TabIndex = 3;
            this.ChkReOrderInfo.Text = "Reorder Info";
            this.ChkReOrderInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkReOrderInfo.UseVisualStyleBackColor = false;
            // 
            // ChkBonusFreeQty
            // 
            this.ChkBonusFreeQty.BackColor = System.Drawing.Color.Transparent;
            this.ChkBonusFreeQty.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkBonusFreeQty.Location = new System.Drawing.Point(125, 72);
            this.ChkBonusFreeQty.Name = "ChkBonusFreeQty";
            this.ChkBonusFreeQty.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkBonusFreeQty.Size = new System.Drawing.Size(153, 25);
            this.ChkBonusFreeQty.TabIndex = 5;
            this.ChkBonusFreeQty.Text = "Bonus/Free Info";
            this.ChkBonusFreeQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkBonusFreeQty.UseVisualStyleBackColor = false;
            // 
            // ChkPriceInfo
            // 
            this.ChkPriceInfo.BackColor = System.Drawing.Color.Transparent;
            this.ChkPriceInfo.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkPriceInfo.Location = new System.Drawing.Point(4, 73);
            this.ChkPriceInfo.Name = "ChkPriceInfo";
            this.ChkPriceInfo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkPriceInfo.Size = new System.Drawing.Size(115, 23);
            this.ChkPriceInfo.TabIndex = 1;
            this.ChkPriceInfo.Text = "Price Info";
            this.ChkPriceInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkPriceInfo.UseVisualStyleBackColor = false;
            // 
            // ChkShortName
            // 
            this.ChkShortName.BackColor = System.Drawing.Color.Transparent;
            this.ChkShortName.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkShortName.Location = new System.Drawing.Point(4, 50);
            this.ChkShortName.Name = "ChkShortName";
            this.ChkShortName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkShortName.Size = new System.Drawing.Size(115, 23);
            this.ChkShortName.TabIndex = 9;
            this.ChkShortName.Text = "ShortName";
            this.ChkShortName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkShortName.UseVisualStyleBackColor = false;
            // 
            // rChkProductType
            // 
            this.rChkProductType.AutoSize = true;
            this.rChkProductType.Location = new System.Drawing.Point(6, 95);
            this.rChkProductType.Name = "rChkProductType";
            this.rChkProductType.Size = new System.Drawing.Size(123, 23);
            this.rChkProductType.TabIndex = 3;
            this.rChkProductType.TabStop = true;
            this.rChkProductType.Text = "Product Type";
            this.rChkProductType.UseVisualStyleBackColor = true;
            // 
            // rChkProductTaxType
            // 
            this.rChkProductTaxType.AutoSize = true;
            this.rChkProductTaxType.Location = new System.Drawing.Point(6, 116);
            this.rChkProductTaxType.Name = "rChkProductTaxType";
            this.rChkProductTaxType.Size = new System.Drawing.Size(175, 23);
            this.rChkProductTaxType.TabIndex = 4;
            this.rChkProductTaxType.TabStop = true;
            this.rChkProductTaxType.Text = "Taxble/Non Taxable";
            this.rChkProductTaxType.UseVisualStyleBackColor = true;
            // 
            // FrmProductList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(487, 191);
            this.Controls.Add(this.StorePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmProductList";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Product List";
            this.Load += new System.EventHandler(this.FrmProductList_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmProductList_KeyPress);
            this.StorePanel.ResumeLayout(false);
            this.grouper1.ResumeLayout(false);
            this.grouper1.PerformLayout();
            this.grouper3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private System.Windows.Forms.RadioButton rChkProductSubGroup;
        private System.Windows.Forms.RadioButton rChkProduct;
        private System.Windows.Forms.RadioButton rChkProductGroup;
        private DevExpress.XtraEditors.SimpleButton BtnShow;
        private System.Windows.Forms.CheckBox ChkSelectAll;
        private System.Windows.Forms.CheckBox ChkReOrderInfo;
        private System.Windows.Forms.CheckBox ChkBonusFreeQty;
        private System.Windows.Forms.CheckBox ChkPriceInfo;
        private System.Windows.Forms.CheckBox ChkLedgerInfo;
        private System.Windows.Forms.Panel StorePanel;
        private System.Windows.Forms.CheckBox ChkIncludeProduct;
        private ClsSeparator clsSeparator1;
        private MrGroup grouper1;
        private MrGroup grouper3;
        private System.Windows.Forms.CheckBox ChkShortName;
        private System.Windows.Forms.RadioButton rChkProductType;
        private System.Windows.Forms.RadioButton rChkProductTaxType;
    }
}