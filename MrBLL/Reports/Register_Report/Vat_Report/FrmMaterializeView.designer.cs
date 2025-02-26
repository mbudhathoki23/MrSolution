using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Register_Report.Vat_Report
{
    partial class FrmMaterializeView
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
            this.gb_TBOptions = new System.Windows.Forms.GroupBox();
            this.ChkDefault = new System.Windows.Forms.CheckBox();
            this.CmbDateType = new System.Windows.Forms.ComboBox();
            this.MskToDate = new MrMaskedTextBox();
            this.MskFrom = new MrMaskedTextBox();
            this.BtnShow = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.roundPanel1 = new RoundPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gb_TBOptions.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.roundPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_TBOptions
            // 
            this.gb_TBOptions.Controls.Add(this.ChkDefault);
            this.gb_TBOptions.Location = new System.Drawing.Point(4, 40);
            this.gb_TBOptions.Name = "gb_TBOptions";
            this.gb_TBOptions.Size = new System.Drawing.Size(448, 43);
            this.gb_TBOptions.TabIndex = 1;
            this.gb_TBOptions.TabStop = false;
            // 
            // ChkDefault
            // 
            this.ChkDefault.Checked = true;
            this.ChkDefault.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkDefault.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkDefault.Location = new System.Drawing.Point(8, 11);
            this.ChkDefault.Name = "ChkDefault";
            this.ChkDefault.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDefault.Size = new System.Drawing.Size(136, 23);
            this.ChkDefault.TabIndex = 3;
            this.ChkDefault.Text = "Default";
            this.ChkDefault.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDefault.UseVisualStyleBackColor = true;
            // 
            // CmbDateType
            // 
            this.CmbDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbDateType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbDateType.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbDateType.FormattingEnabled = true;
            this.CmbDateType.Items.AddRange(new object[] {
            "Custom Date",
            "Today",
            "Yesterday",
            "Current Week",
            "Last Week",
            "Current Month",
            "Last Month",
            "Upto Date",
            "Accounting Period"});
            this.CmbDateType.Location = new System.Drawing.Point(6, 8);
            this.CmbDateType.Name = "CmbDateType";
            this.CmbDateType.Size = new System.Drawing.Size(191, 27);
            this.CmbDateType.TabIndex = 0;
            this.CmbDateType.SelectedIndexChanged += new System.EventHandler(this.CmbDateType_SelectedIndexChanged);
            this.CmbDateType.Enter += new System.EventHandler(this.CmbDateType_Enter);
            this.CmbDateType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbDateType_KeyDown);
            this.CmbDateType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbDateType_KeyPress);
            this.CmbDateType.Leave += new System.EventHandler(this.CmbDateType_Leave);
            // 
            // MskToDate
            // 
            this.MskToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskToDate.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskToDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskToDate.Location = new System.Drawing.Point(323, 9);
            this.MskToDate.Mask = "00/00/0000";
            this.MskToDate.Name = "MskToDate";
            this.MskToDate.Size = new System.Drawing.Size(119, 25);
            this.MskToDate.TabIndex = 2;
            this.MskToDate.Enter += new System.EventHandler(this.msk_ToDate_Enter);
            this.MskToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.msk_ToDate_KeyDown);
            this.MskToDate.Leave += new System.EventHandler(this.msk_ToDate_Leave);
            this.MskToDate.Validated += new System.EventHandler(this.msk_ToDate_Validated);
            // 
            // MskFrom
            // 
            this.MskFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskFrom.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskFrom.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskFrom.Location = new System.Drawing.Point(203, 9);
            this.MskFrom.Mask = "00/00/0000";
            this.MskFrom.Name = "MskFrom";
            this.MskFrom.Size = new System.Drawing.Size(119, 25);
            this.MskFrom.TabIndex = 1;
            this.MskFrom.Enter += new System.EventHandler(this.msk_FromDate_Enter);
            this.MskFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.msk_FromDate_KeyDown);
            this.MskFrom.Leave += new System.EventHandler(this.msk_FromDate_Leave);
            this.MskFrom.Validated += new System.EventHandler(this.msk_FromDate_Validated);
            // 
            // BtnShow
            // 
            this.BtnShow.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnShow.Appearance.Options.UseFont = true;
            this.BtnShow.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnShow.Location = new System.Drawing.Point(154, 9);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(112, 35);
            this.BtnShow.TabIndex = 0;
            this.BtnShow.Text = "&SHOW";
            this.BtnShow.Click += new System.EventHandler(this.btn_Show_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.Location = new System.Drawing.Point(267, 9);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(124, 35);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnCancel);
            this.groupBox1.Controls.Add(this.BtnShow);
            this.groupBox1.Location = new System.Drawing.Point(4, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(448, 47);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // roundPanel1
            // 
            this.roundPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.roundPanel1.Controls.Add(this.groupBox2);
            this.roundPanel1.Controls.Add(this.gb_TBOptions);
            this.roundPanel1.Controls.Add(this.groupBox1);
            this.roundPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roundPanel1.Location = new System.Drawing.Point(0, 0);
            this.roundPanel1.Name = "roundPanel1";
            this.roundPanel1.Radious = 25;
            this.roundPanel1.Size = new System.Drawing.Size(455, 132);
            this.roundPanel1.TabIndex = 0;
            this.roundPanel1.TabStop = false;
            this.roundPanel1.TitleBackColor = System.Drawing.Color.DarkSlateBlue;
            this.roundPanel1.TitleFont = new System.Drawing.Font("Bookman Old Style", 1F);
            this.roundPanel1.TitleForeColor = System.Drawing.Color.White;
            this.roundPanel1.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CmbDateType);
            this.groupBox2.Controls.Add(this.MskFrom);
            this.groupBox2.Controls.Add(this.MskToDate);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox2.Location = new System.Drawing.Point(4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(448, 41);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // FrmMaterializeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(455, 132);
            this.Controls.Add(this.roundPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMaterializeView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Materialize View";
            this.Activated += new System.EventHandler(this.MaterializeView_Activated);
            this.Load += new System.EventHandler(this.MaterializeView_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MaterializeView_KeyPress);
            this.gb_TBOptions.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.roundPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gb_TBOptions;
        private System.Windows.Forms.MaskedTextBox MskToDate;
        private System.Windows.Forms.MaskedTextBox MskFrom;
        private System.Windows.Forms.ComboBox CmbDateType;
        private System.Windows.Forms.CheckBox ChkDefault;
        private DevExpress.XtraEditors.SimpleButton BtnShow;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private RoundPanel roundPanel1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}