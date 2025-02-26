using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Master.LedgerSetup
{
    partial class FrmCurrency
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
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.ChkActive = new System.Windows.Forms.CheckBox();
            this.TxtSalesRate = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblCurrencyRate = new System.Windows.Forms.Label();
            this.TxtShortName = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblCurrencyCode = new System.Windows.Forms.Label();
            this.TxtDescription = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblCurrencyName = new System.Windows.Forms.Label();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.StorePanel = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.BtnSync = new DevExpress.XtraEditors.SimpleButton();
            this.BtnView = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDescription = new System.Windows.Forms.Button();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.TxtBuyRate = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.StorePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(342, 163);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(104, 33);
            this.BtnCancel.TabIndex = 10;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(250, 163);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(90, 33);
            this.BtnSave.TabIndex = 9;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // ChkActive
            // 
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkActive.Location = new System.Drawing.Point(11, 166);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkActive.Size = new System.Drawing.Size(98, 24);
            this.ChkActive.TabIndex = 9;
            this.ChkActive.Text = "Active";
            this.ChkActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkActive.UseVisualStyleBackColor = true;
            // 
            // TxtSalesRate
            // 
            this.TxtSalesRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSalesRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSalesRate.Location = new System.Drawing.Point(123, 129);
            this.TxtSalesRate.MaxLength = 25;
            this.TxtSalesRate.Name = "TxtSalesRate";
            this.TxtSalesRate.Size = new System.Drawing.Size(150, 25);
            this.TxtSalesRate.TabIndex = 8;
            this.TxtSalesRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtSalesRate.TextBoxType = MrDAL.Control.ControlsEx.Control.TextBoxType.Decimal;
            this.TxtSalesRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtRate_KeyDown);
            this.TxtSalesRate.Leave += new System.EventHandler(this.TxtRate_Leave);
            // 
            // lblCurrencyRate
            // 
            this.lblCurrencyRate.AutoSize = true;
            this.lblCurrencyRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrencyRate.Location = new System.Drawing.Point(11, 132);
            this.lblCurrencyRate.Name = "lblCurrencyRate";
            this.lblCurrencyRate.Size = new System.Drawing.Size(90, 19);
            this.lblCurrencyRate.TabIndex = 0;
            this.lblCurrencyRate.Text = "Sales Rate";
            // 
            // TxtShortName
            // 
            this.TxtShortName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtShortName.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtShortName.Location = new System.Drawing.Point(123, 73);
            this.TxtShortName.MaxLength = 50;
            this.TxtShortName.Name = "TxtShortName";
            this.TxtShortName.Size = new System.Drawing.Size(150, 25);
            this.TxtShortName.TabIndex = 6;
            this.TxtShortName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtShortName_KeyDown);
            this.TxtShortName.Leave += new System.EventHandler(this.TxtShortName_Leave);
            this.TxtShortName.Validating += new System.ComponentModel.CancelEventHandler(this.TxtShortName_Validating);
            // 
            // lblCurrencyCode
            // 
            this.lblCurrencyCode.AutoSize = true;
            this.lblCurrencyCode.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrencyCode.Location = new System.Drawing.Point(11, 76);
            this.lblCurrencyCode.Name = "lblCurrencyCode";
            this.lblCurrencyCode.Size = new System.Drawing.Size(98, 19);
            this.lblCurrencyCode.TabIndex = 0;
            this.lblCurrencyCode.Text = "Short Name";
            // 
            // TxtDescription
            // 
            this.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDescription.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDescription.Location = new System.Drawing.Point(123, 47);
            this.TxtDescription.MaxLength = 100;
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(292, 25);
            this.TxtDescription.TabIndex = 5;
            this.TxtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDescription_KeyDown);
            this.TxtDescription.Leave += new System.EventHandler(this.TxtDescription_Leave);
            this.TxtDescription.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDescription_Validating);
            // 
            // lblCurrencyName
            // 
            this.lblCurrencyName.AutoSize = true;
            this.lblCurrencyName.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrencyName.Location = new System.Drawing.Point(11, 50);
            this.lblCurrencyName.Name = "lblCurrencyName";
            this.lblCurrencyName.Size = new System.Drawing.Size(95, 19);
            this.lblCurrencyName.TabIndex = 0;
            this.lblCurrencyName.Text = "Description";
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.BtnExit.Location = new System.Drawing.Point(368, 6);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(77, 31);
            this.BtnExit.TabIndex = 4;
            this.BtnExit.Text = "E&XIT";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(155, 6);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(100, 31);
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
            this.BtnEdit.Location = new System.Drawing.Point(81, 6);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(73, 31);
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
            this.BtnNew.Location = new System.Drawing.Point(6, 6);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(74, 31);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Tag = "NewButtonCheck";
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.label1);
            this.StorePanel.Controls.Add(this.TxtBuyRate);
            this.StorePanel.Controls.Add(this.BtnSync);
            this.StorePanel.Controls.Add(this.BtnView);
            this.StorePanel.Controls.Add(this.BtnDescription);
            this.StorePanel.Controls.Add(this.BtnCancel);
            this.StorePanel.Controls.Add(this.BtnSave);
            this.StorePanel.Controls.Add(this.clsSeparator1);
            this.StorePanel.Controls.Add(this.clsSeparator2);
            this.StorePanel.Controls.Add(this.ChkActive);
            this.StorePanel.Controls.Add(this.BtnExit);
            this.StorePanel.Controls.Add(this.TxtSalesRate);
            this.StorePanel.Controls.Add(this.lblCurrencyRate);
            this.StorePanel.Controls.Add(this.BtnDelete);
            this.StorePanel.Controls.Add(this.TxtShortName);
            this.StorePanel.Controls.Add(this.lblCurrencyCode);
            this.StorePanel.Controls.Add(this.BtnEdit);
            this.StorePanel.Controls.Add(this.TxtDescription);
            this.StorePanel.Controls.Add(this.lblCurrencyName);
            this.StorePanel.Controls.Add(this.BtnNew);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(451, 200);
            this.StorePanel.TabIndex = 0;
            // 
            // BtnSync
            // 
            this.BtnSync.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSync.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSync.Appearance.Options.UseFont = true;
            this.BtnSync.Appearance.Options.UseForeColor = true;
            this.BtnSync.ImageOptions.Image = global::MrBLL.Properties.Resources.ReportRefresh;
            this.BtnSync.Location = new System.Drawing.Point(147, 163);
            this.BtnSync.Margin = new System.Windows.Forms.Padding(4);
            this.BtnSync.Name = "BtnSync";
            this.BtnSync.Size = new System.Drawing.Size(100, 33);
            this.BtnSync.TabIndex = 11;
            this.BtnSync.Text = "&SYNC";
            this.BtnSync.Click += new System.EventHandler(this.BtnSync_Click);
            // 
            // BtnView
            // 
            this.BtnView.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnView.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnView.Appearance.Options.UseFont = true;
            this.BtnView.Appearance.Options.UseForeColor = true;
            this.BtnView.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnView.Location = new System.Drawing.Point(256, 6);
            this.BtnView.Margin = new System.Windows.Forms.Padding(4);
            this.BtnView.Name = "BtnView";
            this.BtnView.Size = new System.Drawing.Size(82, 31);
            this.BtnView.TabIndex = 3;
            this.BtnView.Tag = "ViewButtonCheck";
            this.BtnView.Text = "&VIEW";
            this.BtnView.Click += new System.EventHandler(this.BtnView_Click);
            // 
            // BtnDescription
            // 
            this.BtnDescription.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnDescription.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnDescription.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDescription.Location = new System.Drawing.Point(417, 47);
            this.BtnDescription.Name = "BtnDescription";
            this.BtnDescription.Size = new System.Drawing.Size(25, 25);
            this.BtnDescription.TabIndex = 310;
            this.BtnDescription.TabStop = false;
            this.BtnDescription.UseVisualStyleBackColor = false;
            this.BtnDescription.Click += new System.EventHandler(this.BtnDescription_Click);
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(12, 157);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(436, 2);
            this.clsSeparator1.TabIndex = 7;
            this.clsSeparator1.TabStop = false;
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(12, 40);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(436, 2);
            this.clsSeparator2.TabIndex = 39;
            this.clsSeparator2.TabStop = false;
            // 
            // TxtBuyRate
            // 
            this.TxtBuyRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBuyRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBuyRate.Location = new System.Drawing.Point(123, 101);
            this.TxtBuyRate.MaxLength = 25;
            this.TxtBuyRate.Name = "TxtBuyRate";
            this.TxtBuyRate.Size = new System.Drawing.Size(150, 25);
            this.TxtBuyRate.TabIndex = 7;
            this.TxtBuyRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtBuyRate.TextBoxType = MrDAL.Control.ControlsEx.Control.TextBoxType.Decimal;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 19);
            this.label1.TabIndex = 314;
            this.label1.Text = "Buy Rate";
            // 
            // FrmCurrency
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(451, 200);
            this.Controls.Add(this.StorePanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmCurrency";
            this.ShowIcon = false;
            this.Tag = "Currency";
            this.Text = "CURRENCY SETUP";
            this.Load += new System.EventHandler(this.FrmCurrency_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmCurrency_KeyPress);
            this.StorePanel.ResumeLayout(false);
            this.StorePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblCurrencyRate;
        private System.Windows.Forms.Label lblCurrencyCode;
        private System.Windows.Forms.Label lblCurrencyName;
        private System.Windows.Forms.CheckBox ChkActive;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private ClsSeparator clsSeparator2;
        private ClsSeparator clsSeparator1;
        private System.Windows.Forms.Button BtnDescription;
		private DevExpress.XtraEditors.SimpleButton BtnView;
        private MrTextBox TxtSalesRate;
        private MrTextBox TxtShortName;
        private MrTextBox TxtDescription;
        private MrPanel StorePanel;
        private DevExpress.XtraEditors.SimpleButton BtnSync;
        private MrTextBox TxtBuyRate;
        private System.Windows.Forms.Label label1;
    }
}