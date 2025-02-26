using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.PartyConfirmation
{
    partial class FrmPartyConfirmation
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
            this.PanelHeader = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.mrGroup4 = new MrDAL.Control.ControlsEx.Control.MrGroup();
            this.ChkPrintHeader = new System.Windows.Forms.CheckBox();
            this.ChkPartyConfirmationEnglish = new System.Windows.Forms.CheckBox();
            this.ChkIncludeVatNetAmount = new System.Windows.Forms.CheckBox();
            this.ChkIncludeReturn = new System.Windows.Forms.CheckBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.TxtTransactionAbove = new System.Windows.Forms.TextBox();
            this.ChkSelectAll = new System.Windows.Forms.CheckBox();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.CmbFiscalYear = new System.Windows.Forms.ComboBox();
            this.ChkVendorWise = new System.Windows.Forms.RadioButton();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.ChkCustomerWise = new System.Windows.Forms.RadioButton();
            this.BtnShow = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.PanelHeader.SuspendLayout();
            this.mrGroup4.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.mrGroup4);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(495, 240);
            this.PanelHeader.TabIndex = 1;
            // 
            // mrGroup4
            // 
            this.mrGroup4.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup4.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup4.BackgroundGradientMode = MrDAL.Control.ControlsEx.Control.MrGroup.GroupBoxGradientMode.None;
            this.mrGroup4.BorderColor = System.Drawing.Color.White;
            this.mrGroup4.BorderThickness = 1F;
            this.mrGroup4.Controls.Add(this.ChkPrintHeader);
            this.mrGroup4.Controls.Add(this.ChkPartyConfirmationEnglish);
            this.mrGroup4.Controls.Add(this.ChkIncludeVatNetAmount);
            this.mrGroup4.Controls.Add(this.ChkIncludeReturn);
            this.mrGroup4.Controls.Add(this.labelControl2);
            this.mrGroup4.Controls.Add(this.TxtTransactionAbove);
            this.mrGroup4.Controls.Add(this.ChkSelectAll);
            this.mrGroup4.Controls.Add(this.clsSeparator2);
            this.mrGroup4.Controls.Add(this.labelControl1);
            this.mrGroup4.Controls.Add(this.CmbFiscalYear);
            this.mrGroup4.Controls.Add(this.ChkVendorWise);
            this.mrGroup4.Controls.Add(this.clsSeparator1);
            this.mrGroup4.Controls.Add(this.ChkCustomerWise);
            this.mrGroup4.Controls.Add(this.BtnShow);
            this.mrGroup4.Controls.Add(this.BtnCancel);
            this.mrGroup4.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrGroup4.GroupImage = null;
            this.mrGroup4.GroupTitle = "";
            this.mrGroup4.Location = new System.Drawing.Point(0, 0);
            this.mrGroup4.Name = "mrGroup4";
            this.mrGroup4.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup4.PaintGroupBox = false;
            this.mrGroup4.RoundCorners = 10;
            this.mrGroup4.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup4.ShadowControl = true;
            this.mrGroup4.ShadowThickness = 3;
            this.mrGroup4.Size = new System.Drawing.Size(495, 240);
            this.mrGroup4.TabIndex = 0;
            // 
            // ChkPrintHeader
            // 
            this.ChkPrintHeader.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkPrintHeader.Location = new System.Drawing.Point(177, 151);
            this.ChkPrintHeader.Name = "ChkPrintHeader";
            this.ChkPrintHeader.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkPrintHeader.Size = new System.Drawing.Size(155, 24);
            this.ChkPrintHeader.TabIndex = 106;
            this.ChkPrintHeader.Text = "Print Header";
            this.ChkPrintHeader.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkPrintHeader.UseVisualStyleBackColor = true;
            // 
            // ChkPartyConfirmationEnglish
            // 
            this.ChkPartyConfirmationEnglish.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkPartyConfirmationEnglish.Location = new System.Drawing.Point(177, 119);
            this.ChkPartyConfirmationEnglish.Name = "ChkPartyConfirmationEnglish";
            this.ChkPartyConfirmationEnglish.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkPartyConfirmationEnglish.Size = new System.Drawing.Size(155, 24);
            this.ChkPartyConfirmationEnglish.TabIndex = 105;
            this.ChkPartyConfirmationEnglish.Text = "English Letter";
            this.ChkPartyConfirmationEnglish.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkPartyConfirmationEnglish.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeVatNetAmount
            // 
            this.ChkIncludeVatNetAmount.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeVatNetAmount.Location = new System.Drawing.Point(10, 151);
            this.ChkIncludeVatNetAmount.Name = "ChkIncludeVatNetAmount";
            this.ChkIncludeVatNetAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeVatNetAmount.Size = new System.Drawing.Size(155, 24);
            this.ChkIncludeVatNetAmount.TabIndex = 104;
            this.ChkIncludeVatNetAmount.Text = "Vat Net Amount";
            this.ChkIncludeVatNetAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeVatNetAmount.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeReturn
            // 
            this.ChkIncludeReturn.Checked = true;
            this.ChkIncludeReturn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkIncludeReturn.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeReturn.Location = new System.Drawing.Point(10, 119);
            this.ChkIncludeReturn.Name = "ChkIncludeReturn";
            this.ChkIncludeReturn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeReturn.Size = new System.Drawing.Size(155, 24);
            this.ChkIncludeReturn.TabIndex = 103;
            this.ChkIncludeReturn.Text = "Include Return";
            this.ChkIncludeReturn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeReturn.UseVisualStyleBackColor = true;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(177, 91);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(143, 20);
            this.labelControl2.TabIndex = 102;
            this.labelControl2.Text = "Transaction Above";
            // 
            // TxtTransactionAbove
            // 
            this.TxtTransactionAbove.Location = new System.Drawing.Point(329, 91);
            this.TxtTransactionAbove.Name = "TxtTransactionAbove";
            this.TxtTransactionAbove.Size = new System.Drawing.Size(158, 26);
            this.TxtTransactionAbove.TabIndex = 5;
            this.TxtTransactionAbove.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtTransactionAbove.Validating += new System.ComponentModel.CancelEventHandler(this.TxtTransactionAbove_Validating);
            // 
            // ChkSelectAll
            // 
            this.ChkSelectAll.Checked = true;
            this.ChkSelectAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkSelectAll.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSelectAll.Location = new System.Drawing.Point(10, 89);
            this.ChkSelectAll.Name = "ChkSelectAll";
            this.ChkSelectAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSelectAll.Size = new System.Drawing.Size(155, 24);
            this.ChkSelectAll.TabIndex = 4;
            this.ChkSelectAll.Text = "Select All";
            this.ChkSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSelectAll.UseVisualStyleBackColor = true;
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clsSeparator2.Location = new System.Drawing.Point(4, 51);
            this.clsSeparator2.Margin = new System.Windows.Forms.Padding(2);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Padding = new System.Windows.Forms.Padding(2);
            this.clsSeparator2.Size = new System.Drawing.Size(478, 2);
            this.clsSeparator2.TabIndex = 98;
            this.clsSeparator2.TabStop = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(10, 62);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(85, 20);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Fiscal Year";
            // 
            // CmbFiscalYear
            // 
            this.CmbFiscalYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbFiscalYear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbFiscalYear.FormattingEnabled = true;
            this.CmbFiscalYear.Location = new System.Drawing.Point(105, 58);
            this.CmbFiscalYear.Name = "CmbFiscalYear";
            this.CmbFiscalYear.Size = new System.Drawing.Size(382, 28);
            this.CmbFiscalYear.TabIndex = 3;
            // 
            // ChkVendorWise
            // 
            this.ChkVendorWise.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkVendorWise.Location = new System.Drawing.Point(206, 19);
            this.ChkVendorWise.Name = "ChkVendorWise";
            this.ChkVendorWise.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkVendorWise.Size = new System.Drawing.Size(185, 24);
            this.ChkVendorWise.TabIndex = 1;
            this.ChkVendorWise.Text = "Vendor Wise";
            this.ChkVendorWise.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkVendorWise.UseVisualStyleBackColor = true;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clsSeparator1.Location = new System.Drawing.Point(6, 185);
            this.clsSeparator1.Margin = new System.Windows.Forms.Padding(2);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Padding = new System.Windows.Forms.Padding(2);
            this.clsSeparator1.Size = new System.Drawing.Size(478, 2);
            this.clsSeparator1.TabIndex = 97;
            this.clsSeparator1.TabStop = false;
            // 
            // ChkCustomerWise
            // 
            this.ChkCustomerWise.Checked = true;
            this.ChkCustomerWise.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkCustomerWise.Location = new System.Drawing.Point(14, 19);
            this.ChkCustomerWise.Name = "ChkCustomerWise";
            this.ChkCustomerWise.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkCustomerWise.Size = new System.Drawing.Size(186, 24);
            this.ChkCustomerWise.TabIndex = 0;
            this.ChkCustomerWise.TabStop = true;
            this.ChkCustomerWise.Text = "Customer Wise";
            this.ChkCustomerWise.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkCustomerWise.UseVisualStyleBackColor = true;
            // 
            // BtnShow
            // 
            this.BtnShow.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnShow.Appearance.Options.UseFont = true;
            this.BtnShow.ImageOptions.Image = global::MrBLL.Properties.Resources.ShowReport;
            this.BtnShow.Location = new System.Drawing.Point(257, 192);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(111, 38);
            this.BtnShow.TabIndex = 6;
            this.BtnShow.Text = "&SHOW";
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(369, 192);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(111, 38);
            this.BtnCancel.TabIndex = 7;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // FrmPartyConfirmation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 240);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmPartyConfirmation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PARTY CONFIRMATION";
            this.Load += new System.EventHandler(this.FrmPartyConfirmation_Load);
            this.PanelHeader.ResumeLayout(false);
            this.mrGroup4.ResumeLayout(false);
            this.mrGroup4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RadioButton ChkVendorWise;
        private System.Windows.Forms.RadioButton ChkCustomerWise;
        private DevExpress.XtraEditors.SimpleButton BtnShow;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private MrPanel PanelHeader;
        private MrGroup mrGroup4;
        private ClsSeparator clsSeparator1;
        private ClsSeparator clsSeparator2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.ComboBox CmbFiscalYear;
        private System.Windows.Forms.CheckBox ChkSelectAll;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.TextBox TxtTransactionAbove;
        private System.Windows.Forms.CheckBox ChkIncludeReturn;
        private System.Windows.Forms.CheckBox ChkIncludeVatNetAmount;
        private System.Windows.Forms.CheckBox ChkPartyConfirmationEnglish;
        private System.Windows.Forms.CheckBox ChkPrintHeader;
    }
}