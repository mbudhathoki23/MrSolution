
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Register_Report.Analysis_Report
{
    partial class FrmTopNAnalysis
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RbtnCash = new System.Windows.Forms.RadioButton();
            this.RbtnAll = new System.Windows.Forms.RadioButton();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.rChkQuantity = new System.Windows.Forms.RadioButton();
            this.rChkBalance = new System.Windows.Forms.RadioButton();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.BtnShow = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.CmbDateType = new System.Windows.Forms.ComboBox();
            this.MskFrom = new MrMaskedTextBox();
            this.MskToDate = new MrMaskedTextBox();
            this.PanelHeader = new MrPanel();
            this.roundPanel5 = new RoundPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TxtTopNumber = new MrNumericTextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.PanelHeader.SuspendLayout();
            this.roundPanel5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RbtnCash);
            this.groupBox1.Controls.Add(this.RbtnAll);
            this.groupBox1.Location = new System.Drawing.Point(293, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(204, 88);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Report Type";
            // 
            // RbtnCash
            // 
            this.RbtnCash.AutoSize = true;
            this.RbtnCash.Location = new System.Drawing.Point(9, 50);
            this.RbtnCash.Name = "RbtnCash";
            this.RbtnCash.Size = new System.Drawing.Size(99, 24);
            this.RbtnCash.TabIndex = 1;
            this.RbtnCash.Text = "Button N";
            this.RbtnCash.UseVisualStyleBackColor = true;
            // 
            // RbtnAll
            // 
            this.RbtnAll.AutoSize = true;
            this.RbtnAll.Checked = true;
            this.RbtnAll.Location = new System.Drawing.Point(8, 26);
            this.RbtnAll.Name = "RbtnAll";
            this.RbtnAll.Size = new System.Drawing.Size(72, 24);
            this.RbtnAll.TabIndex = 0;
            this.RbtnAll.TabStop = true;
            this.RbtnAll.Text = "Top N";
            this.RbtnAll.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.rChkQuantity);
            this.groupBox7.Controls.Add(this.rChkBalance);
            this.groupBox7.Location = new System.Drawing.Point(3, 95);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(284, 47);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Sort On";
            // 
            // rChkQuantity
            // 
            this.rChkQuantity.AutoCheck = false;
            this.rChkQuantity.AutoSize = true;
            this.rChkQuantity.Location = new System.Drawing.Point(120, 17);
            this.rChkQuantity.Name = "rChkQuantity";
            this.rChkQuantity.Size = new System.Drawing.Size(97, 24);
            this.rChkQuantity.TabIndex = 0;
            this.rChkQuantity.Text = "Quantity";
            this.rChkQuantity.UseVisualStyleBackColor = true;
            this.rChkQuantity.Visible = false;
            // 
            // rChkBalance
            // 
            this.rChkBalance.AutoSize = true;
            this.rChkBalance.Checked = true;
            this.rChkBalance.Location = new System.Drawing.Point(12, 18);
            this.rChkBalance.Name = "rChkBalance";
            this.rChkBalance.Size = new System.Drawing.Size(90, 24);
            this.rChkBalance.TabIndex = 1;
            this.rChkBalance.TabStop = true;
            this.rChkBalance.Text = "Balance";
            this.rChkBalance.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.BtnShow);
            this.groupBox8.Controls.Add(this.BtnCancel);
            this.groupBox8.Location = new System.Drawing.Point(9, 134);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(488, 53);
            this.groupBox8.TabIndex = 4;
            this.groupBox8.TabStop = false;
            // 
            // BtnShow
            // 
            this.BtnShow.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnShow.Appearance.Options.UseFont = true;
            this.BtnShow.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnShow.Location = new System.Drawing.Point(255, 12);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(109, 36);
            this.BtnShow.TabIndex = 0;
            this.BtnShow.Text = "&SHOW";
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(366, 12);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(109, 36);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // CmbDateType
            // 
            this.CmbDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbDateType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbDateType.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.CmbDateType.Location = new System.Drawing.Point(6, 24);
            this.CmbDateType.Name = "CmbDateType";
            this.CmbDateType.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CmbDateType.Size = new System.Drawing.Size(278, 28);
            this.CmbDateType.TabIndex = 0;
            this.CmbDateType.SelectedIndexChanged += new System.EventHandler(this.CmbDateType_SelectedIndexChanged);
            // 
            // MskFrom
            // 
            this.MskFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskFrom.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskFrom.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskFrom.Location = new System.Drawing.Point(6, 56);
            this.MskFrom.Mask = "00/00/0000";
            this.MskFrom.Name = "MskFrom";
            this.MskFrom.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.MskFrom.Size = new System.Drawing.Size(139, 26);
            this.MskFrom.TabIndex = 1;
            this.MskFrom.Validating += new System.ComponentModel.CancelEventHandler(this.MskFrom_Validating);
            // 
            // MskToDate
            // 
            this.MskToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskToDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskToDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskToDate.Location = new System.Drawing.Point(145, 56);
            this.MskToDate.Mask = "00/00/0000";
            this.MskToDate.Name = "MskToDate";
            this.MskToDate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.MskToDate.Size = new System.Drawing.Size(139, 26);
            this.MskToDate.TabIndex = 2;
            this.MskToDate.Validating += new System.ComponentModel.CancelEventHandler(this.MskToDate_Validating);
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.roundPanel5);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(500, 192);
            this.PanelHeader.TabIndex = 1;
            // 
            // roundPanel5
            // 
            this.roundPanel5.BackColor = System.Drawing.Color.Transparent;
            this.roundPanel5.Controls.Add(this.groupBox2);
            this.roundPanel5.Controls.Add(this.groupBox6);
            this.roundPanel5.Controls.Add(this.groupBox1);
            this.roundPanel5.Controls.Add(this.groupBox7);
            this.roundPanel5.Controls.Add(this.groupBox8);
            this.roundPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roundPanel5.Location = new System.Drawing.Point(0, 0);
            this.roundPanel5.Name = "roundPanel5";
            this.roundPanel5.Radious = 25;
            this.roundPanel5.Size = new System.Drawing.Size(500, 192);
            this.roundPanel5.TabIndex = 0;
            this.roundPanel5.TabStop = false;
            this.roundPanel5.TitleBackColor = System.Drawing.Color.DarkSlateBlue;
            this.roundPanel5.TitleFont = new System.Drawing.Font("Bookman Old Style", 0.01F);
            this.roundPanel5.TitleForeColor = System.Drawing.Color.White;
            this.roundPanel5.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TxtTopNumber);
            this.groupBox2.Location = new System.Drawing.Point(293, 95);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(204, 47);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Top Number";
            // 
            // TxtTopNumber
            // 
            this.TxtTopNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTopNumber.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTopNumber.Location = new System.Drawing.Point(3, 18);
            this.TxtTopNumber.Name = "TxtTopNumber";
            this.TxtTopNumber.Size = new System.Drawing.Size(188, 25);
            this.TxtTopNumber.TabIndex = 0;
            this.TxtTopNumber.Text = "10.00";
            this.TxtTopNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.CmbDateType);
            this.groupBox6.Controls.Add(this.MskFrom);
            this.groupBox6.Controls.Add(this.MskToDate);
            this.groupBox6.Location = new System.Drawing.Point(3, 8);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(287, 88);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Date Type";
            // 
            // FrmTopNAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(500, 192);
            this.Controls.Add(this.PanelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Name = "FrmTopNAnalysis";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Top N Analysis";
            this.Load += new System.EventHandler(this.FrmTopNAnalysis_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmTopNAnalysis_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.PanelHeader.ResumeLayout(false);
            this.roundPanel5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RbtnCash;
        private System.Windows.Forms.RadioButton RbtnAll;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RadioButton rChkQuantity;
        private System.Windows.Forms.RadioButton rChkBalance;
        private System.Windows.Forms.GroupBox groupBox8;
        private DevExpress.XtraEditors.SimpleButton BtnShow;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private System.Windows.Forms.ComboBox CmbDateType;
        private System.Windows.Forms.MaskedTextBox MskFrom;
        private System.Windows.Forms.MaskedTextBox MskToDate;
        private System.Windows.Forms.Panel PanelHeader;
        private RoundPanel roundPanel5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox2;
        private MrNumericTextBox TxtTopNumber;
    }
}