using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Inventory_Report.Analysis
{
    partial class FrmStockAgeing
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
            this.lbl_Branch = new System.Windows.Forms.Label();
            this.cmb_Branch = new System.Windows.Forms.ComboBox();
            this.txt_Days = new MrTextBox();
            this.lbl_Days = new System.Windows.Forms.Label();
            this.txt_Interval = new MrTextBox();
            this.lbl_Interval = new System.Windows.Forms.Label();
            this.chk_IncludeProduct = new System.Windows.Forms.CheckBox();
            this.msk_AsOnDate = new MrMaskedTextBox();
            this.lbl_AsOnDate = new System.Windows.Forms.Label();
            this.chk_Details = new System.Windows.Forms.CheckBox();
            this.chk_Date = new System.Windows.Forms.CheckBox();
            this.cmb_FreeGoods = new System.Windows.Forms.ComboBox();
            this.lbl_FreeGoods = new System.Windows.Forms.Label();
            this.chk_SelectAll = new System.Windows.Forms.CheckBox();
            this.cmb_GroupBy = new System.Windows.Forms.ComboBox();
            this.lbl_GroupBy = new System.Windows.Forms.Label();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Show = new DevExpress.XtraEditors.SimpleButton();
            this.StorePanel = new MrPanel();
            this.clsSeparatorH1 = new ClsSeparatorH();
            this.clsSeparator1 = new ClsSeparator();
            this.StorePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Branch
            // 
            this.lbl_Branch.AutoSize = true;
            this.lbl_Branch.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Branch.Location = new System.Drawing.Point(15, 107);
            this.lbl_Branch.Name = "lbl_Branch";
            this.lbl_Branch.Size = new System.Drawing.Size(72, 20);
            this.lbl_Branch.TabIndex = 2;
            this.lbl_Branch.Text = "Branch ";
            // 
            // cmb_Branch
            // 
            this.cmb_Branch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Branch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmb_Branch.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_Branch.FormattingEnabled = true;
            this.cmb_Branch.Location = new System.Drawing.Point(115, 103);
            this.cmb_Branch.Name = "cmb_Branch";
            this.cmb_Branch.Size = new System.Drawing.Size(195, 28);
            this.cmb_Branch.TabIndex = 3;
            // 
            // txt_Days
            // 
            this.txt_Days.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Days.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Days.Location = new System.Drawing.Point(511, 12);
            this.txt_Days.Name = "txt_Days";
            this.txt_Days.Size = new System.Drawing.Size(52, 26);
            this.txt_Days.TabIndex = 5;
            this.txt_Days.Text = "90";
            this.txt_Days.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Days.Enter += new System.EventHandler(this.txt_Days_Enter);
            this.txt_Days.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Days_KeyPress);
            this.txt_Days.Leave += new System.EventHandler(this.txt_Days_Leave);
            this.txt_Days.Validating += new System.ComponentModel.CancelEventHandler(this.txt_Days_Validating);
            // 
            // lbl_Days
            // 
            this.lbl_Days.AutoSize = true;
            this.lbl_Days.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Days.Location = new System.Drawing.Point(460, 15);
            this.lbl_Days.Name = "lbl_Days";
            this.lbl_Days.Size = new System.Drawing.Size(48, 20);
            this.lbl_Days.TabIndex = 2;
            this.lbl_Days.Text = "Days";
            // 
            // txt_Interval
            // 
            this.txt_Interval.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Interval.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Interval.Location = new System.Drawing.Point(399, 12);
            this.txt_Interval.Name = "txt_Interval";
            this.txt_Interval.Size = new System.Drawing.Size(59, 26);
            this.txt_Interval.TabIndex = 4;
            this.txt_Interval.Text = "30";
            this.txt_Interval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Interval.Enter += new System.EventHandler(this.txt_Interval_Enter);
            this.txt_Interval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Interval_KeyPress);
            this.txt_Interval.Leave += new System.EventHandler(this.txt_Interval_Leave);
            this.txt_Interval.Validating += new System.ComponentModel.CancelEventHandler(this.txt_Interval_Validating);
            // 
            // lbl_Interval
            // 
            this.lbl_Interval.AutoSize = true;
            this.lbl_Interval.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Interval.Location = new System.Drawing.Point(331, 15);
            this.lbl_Interval.Name = "lbl_Interval";
            this.lbl_Interval.Size = new System.Drawing.Size(68, 20);
            this.lbl_Interval.TabIndex = 3;
            this.lbl_Interval.Text = "Interval";
            // 
            // chk_IncludeProduct
            // 
            this.chk_IncludeProduct.Enabled = false;
            this.chk_IncludeProduct.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_IncludeProduct.Location = new System.Drawing.Point(331, 62);
            this.chk_IncludeProduct.Name = "chk_IncludeProduct";
            this.chk_IncludeProduct.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_IncludeProduct.Size = new System.Drawing.Size(232, 22);
            this.chk_IncludeProduct.TabIndex = 7;
            this.chk_IncludeProduct.Text = "Include Product";
            this.chk_IncludeProduct.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_IncludeProduct.UseVisualStyleBackColor = true;
            this.chk_IncludeProduct.CheckedChanged += new System.EventHandler(this.chk_IncludeProduct_CheckedChanged);
            // 
            // msk_AsOnDate
            // 
            this.msk_AsOnDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_AsOnDate.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.msk_AsOnDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msk_AsOnDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.msk_AsOnDate.Location = new System.Drawing.Point(115, 7);
            this.msk_AsOnDate.Mask = "00/00/0000";
            this.msk_AsOnDate.Name = "msk_AsOnDate";
            this.msk_AsOnDate.Size = new System.Drawing.Size(138, 26);
            this.msk_AsOnDate.TabIndex = 0;
            this.msk_AsOnDate.Enter += new System.EventHandler(this.msk_AsOnDate_Enter);
            this.msk_AsOnDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.msk_AsOnDate_KeyDown);
            this.msk_AsOnDate.Leave += new System.EventHandler(this.msk_AsOnDate_Leave);
            this.msk_AsOnDate.Validated += new System.EventHandler(this.msk_AsOnDate_Validated);
            // 
            // lbl_AsOnDate
            // 
            this.lbl_AsOnDate.AutoSize = true;
            this.lbl_AsOnDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AsOnDate.Location = new System.Drawing.Point(15, 10);
            this.lbl_AsOnDate.Name = "lbl_AsOnDate";
            this.lbl_AsOnDate.Size = new System.Drawing.Size(99, 20);
            this.lbl_AsOnDate.TabIndex = 5;
            this.lbl_AsOnDate.Text = "As On Date";
            // 
            // chk_Details
            // 
            this.chk_Details.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_Details.Location = new System.Drawing.Point(331, 40);
            this.chk_Details.Name = "chk_Details";
            this.chk_Details.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Details.Size = new System.Drawing.Size(232, 22);
            this.chk_Details.TabIndex = 6;
            this.chk_Details.Text = "Details";
            this.chk_Details.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Details.UseVisualStyleBackColor = true;
            this.chk_Details.CheckedChanged += new System.EventHandler(this.chk_Details_CheckedChanged);
            // 
            // chk_Date
            // 
            this.chk_Date.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_Date.Location = new System.Drawing.Point(331, 84);
            this.chk_Date.Name = "chk_Date";
            this.chk_Date.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Date.Size = new System.Drawing.Size(232, 22);
            this.chk_Date.TabIndex = 8;
            this.chk_Date.Text = "Date";
            this.chk_Date.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Date.UseVisualStyleBackColor = true;
            // 
            // cmb_FreeGoods
            // 
            this.cmb_FreeGoods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_FreeGoods.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmb_FreeGoods.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_FreeGoods.FormattingEnabled = true;
            this.cmb_FreeGoods.Items.AddRange(new object[] {
            "Include Free",
            "Exclude Free",
            "Only Free"});
            this.cmb_FreeGoods.Location = new System.Drawing.Point(115, 70);
            this.cmb_FreeGoods.Name = "cmb_FreeGoods";
            this.cmb_FreeGoods.Size = new System.Drawing.Size(195, 28);
            this.cmb_FreeGoods.TabIndex = 2;
            // 
            // lbl_FreeGoods
            // 
            this.lbl_FreeGoods.AutoSize = true;
            this.lbl_FreeGoods.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_FreeGoods.Location = new System.Drawing.Point(15, 74);
            this.lbl_FreeGoods.Name = "lbl_FreeGoods";
            this.lbl_FreeGoods.Size = new System.Drawing.Size(98, 20);
            this.lbl_FreeGoods.TabIndex = 1;
            this.lbl_FreeGoods.Text = "Free Goods";
            // 
            // chk_SelectAll
            // 
            this.chk_SelectAll.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_SelectAll.Location = new System.Drawing.Point(331, 106);
            this.chk_SelectAll.Name = "chk_SelectAll";
            this.chk_SelectAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_SelectAll.Size = new System.Drawing.Size(232, 22);
            this.chk_SelectAll.TabIndex = 9;
            this.chk_SelectAll.Text = "Select All";
            this.chk_SelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_SelectAll.UseVisualStyleBackColor = true;
            // 
            // cmb_GroupBy
            // 
            this.cmb_GroupBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_GroupBy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmb_GroupBy.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_GroupBy.FormattingEnabled = true;
            this.cmb_GroupBy.Items.AddRange(new object[] {
            "Product",
            "Product Group",
            "Product SubGroup",
            "Company"});
            this.cmb_GroupBy.Location = new System.Drawing.Point(115, 37);
            this.cmb_GroupBy.Name = "cmb_GroupBy";
            this.cmb_GroupBy.Size = new System.Drawing.Size(195, 28);
            this.cmb_GroupBy.TabIndex = 1;
            this.cmb_GroupBy.SelectedIndexChanged += new System.EventHandler(this.cmb_GroupBy_SelectedIndexChanged);
            // 
            // lbl_GroupBy
            // 
            this.lbl_GroupBy.AutoSize = true;
            this.lbl_GroupBy.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_GroupBy.Location = new System.Drawing.Point(15, 41);
            this.lbl_GroupBy.Name = "lbl_GroupBy";
            this.lbl_GroupBy.Size = new System.Drawing.Size(85, 20);
            this.lbl_GroupBy.TabIndex = 0;
            this.lbl_GroupBy.Text = "Group By";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(302, 140);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(112, 39);
            this.simpleButton2.TabIndex = 11;
            this.simpleButton2.Text = "&CANCEL";
            // 
            // btn_Show
            // 
            this.btn_Show.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Show.Appearance.Options.UseFont = true;
            this.btn_Show.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.btn_Show.Location = new System.Drawing.Point(195, 140);
            this.btn_Show.Name = "btn_Show";
            this.btn_Show.Size = new System.Drawing.Size(107, 39);
            this.btn_Show.TabIndex = 10;
            this.btn_Show.Text = "&SHOW";
            this.btn_Show.Click += new System.EventHandler(this.btn_Show_Click);
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.clsSeparatorH1);
            this.StorePanel.Controls.Add(this.clsSeparator1);
            this.StorePanel.Controls.Add(this.lbl_Branch);
            this.StorePanel.Controls.Add(this.simpleButton2);
            this.StorePanel.Controls.Add(this.btn_Show);
            this.StorePanel.Controls.Add(this.cmb_Branch);
            this.StorePanel.Controls.Add(this.txt_Days);
            this.StorePanel.Controls.Add(this.lbl_Days);
            this.StorePanel.Controls.Add(this.txt_Interval);
            this.StorePanel.Controls.Add(this.cmb_GroupBy);
            this.StorePanel.Controls.Add(this.lbl_Interval);
            this.StorePanel.Controls.Add(this.lbl_GroupBy);
            this.StorePanel.Controls.Add(this.chk_IncludeProduct);
            this.StorePanel.Controls.Add(this.chk_SelectAll);
            this.StorePanel.Controls.Add(this.msk_AsOnDate);
            this.StorePanel.Controls.Add(this.lbl_FreeGoods);
            this.StorePanel.Controls.Add(this.lbl_AsOnDate);
            this.StorePanel.Controls.Add(this.cmb_FreeGoods);
            this.StorePanel.Controls.Add(this.chk_Details);
            this.StorePanel.Controls.Add(this.chk_Date);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(572, 183);
            this.StorePanel.TabIndex = 0;
            // 
            // clsSeparatorH1
            // 
            this.clsSeparatorH1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparatorH1.Location = new System.Drawing.Point(323, 7);
            this.clsSeparatorH1.Name = "clsSeparatorH1";
            this.clsSeparatorH1.Size = new System.Drawing.Size(3, 127);
            this.clsSeparatorH1.TabIndex = 14;
            this.clsSeparatorH1.TabStop = false;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator1.Location = new System.Drawing.Point(13, 136);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(547, 2);
            this.clsSeparator1.TabIndex = 13;
            this.clsSeparator1.TabStop = false;
            // 
            // FrmStockAgeing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(572, 183);
            this.Controls.Add(this.StorePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmStockAgeing";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ageing Stock Status";
            this.Load += new System.EventHandler(this.FrmStockAgeing_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmStockAgeing_KeyPress);
            this.StorePanel.ResumeLayout(false);
            this.StorePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox chk_Date;
        private System.Windows.Forms.ComboBox cmb_FreeGoods;
        private System.Windows.Forms.Label lbl_FreeGoods;
        private System.Windows.Forms.CheckBox chk_SelectAll;
        private System.Windows.Forms.ComboBox cmb_GroupBy;
        private System.Windows.Forms.Label lbl_GroupBy;
        private System.Windows.Forms.CheckBox chk_Details;
        private System.Windows.Forms.CheckBox chk_IncludeProduct;
        private System.Windows.Forms.Label lbl_AsOnDate;
        private System.Windows.Forms.Label lbl_Interval;
        private System.Windows.Forms.Label lbl_Days;
        private System.Windows.Forms.Label lbl_Branch;
        private System.Windows.Forms.ComboBox cmb_Branch;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton btn_Show;
        private ClsSeparator clsSeparator1;
        private ClsSeparatorH clsSeparatorH1;
        private MrMaskedTextBox msk_AsOnDate;
        private MrTextBox txt_Interval;
        private MrTextBox txt_Days;
        private MrPanel StorePanel;
    }
}