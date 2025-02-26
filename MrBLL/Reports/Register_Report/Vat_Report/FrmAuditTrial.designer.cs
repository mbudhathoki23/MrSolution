using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Register_Report.Vat_Report
{
    partial class FrmAuditTrial
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
            this.BtnShow = new DevExpress.XtraEditors.SimpleButton();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSysDateType = new System.Windows.Forms.ComboBox();
            this.lbl_OpenFor = new System.Windows.Forms.Label();
            this.cmb_FilterBy = new System.Windows.Forms.ComboBox();
            this.lbl_Action = new System.Windows.Forms.Label();
            this.cmb_Action = new System.Windows.Forms.ComboBox();
            this.chk_Date = new System.Windows.Forms.CheckBox();
            this.chklstbox_DayBook = new System.Windows.Forms.CheckedListBox();
            this.cmb_Branch = new System.Windows.Forms.ComboBox();
            this.lbl_Branch = new System.Windows.Forms.Label();
            this.msk_ToDate = new MrMaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.msk_FromDate = new MrMaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.StorePanel = new MrPanel();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.clsSeparator1 = new ClsSeparator();
            this.panel13 = new MrPanel();
            this.panel15 = new MrPanel();
            this.panel16 = new MrPanel();
            this.StorePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnShow
            // 
            this.BtnShow.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnShow.Appearance.Options.UseFont = true;
            this.BtnShow.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnShow.Location = new System.Drawing.Point(9, 225);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(89, 38);
            this.BtnShow.TabIndex = 6;
            this.BtnShow.Text = "&SHOW";
            this.BtnShow.Click += new System.EventHandler(this.btn_Show_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 19);
            this.label4.TabIndex = 95;
            this.label4.Text = "Date Type";
            // 
            // cmbSysDateType
            // 
            this.cmbSysDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSysDateType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbSysDateType.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSysDateType.FormattingEnabled = true;
            this.cmbSysDateType.Items.AddRange(new object[] {
            "Custom Date",
            "Today",
            "Yesterday",
            "Current Week",
            "Last Week",
            "Current Month",
            "Last Month",
            "Upto Date",
            "Accounting Period"});
            this.cmbSysDateType.Location = new System.Drawing.Point(106, 9);
            this.cmbSysDateType.Name = "cmbSysDateType";
            this.cmbSysDateType.Size = new System.Drawing.Size(197, 27);
            this.cmbSysDateType.TabIndex = 0;
            this.cmbSysDateType.SelectedIndexChanged += new System.EventHandler(this.cmbSysDateType_SelectedIndexChanged);
            // 
            // lbl_OpenFor
            // 
            this.lbl_OpenFor.AutoSize = true;
            this.lbl_OpenFor.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_OpenFor.Location = new System.Drawing.Point(9, 174);
            this.lbl_OpenFor.Name = "lbl_OpenFor";
            this.lbl_OpenFor.Size = new System.Drawing.Size(75, 19);
            this.lbl_OpenFor.TabIndex = 75;
            this.lbl_OpenFor.Text = "Filter By";
            // 
            // cmb_FilterBy
            // 
            this.cmb_FilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_FilterBy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmb_FilterBy.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_FilterBy.FormattingEnabled = true;
            this.cmb_FilterBy.Items.AddRange(new object[] {
            "Action",
            "Bill No",
            "Bill Date",
            "User"});
            this.cmb_FilterBy.Location = new System.Drawing.Point(105, 170);
            this.cmb_FilterBy.Name = "cmb_FilterBy";
            this.cmb_FilterBy.Size = new System.Drawing.Size(198, 27);
            this.cmb_FilterBy.TabIndex = 5;
            // 
            // lbl_Action
            // 
            this.lbl_Action.AutoSize = true;
            this.lbl_Action.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Action.Location = new System.Drawing.Point(9, 141);
            this.lbl_Action.Name = "lbl_Action";
            this.lbl_Action.Size = new System.Drawing.Size(56, 19);
            this.lbl_Action.TabIndex = 73;
            this.lbl_Action.Text = "Action";
            // 
            // cmb_Action
            // 
            this.cmb_Action.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Action.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmb_Action.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_Action.FormattingEnabled = true;
            this.cmb_Action.Items.AddRange(new object[] {
            "Both",
            "Update",
            "Delete"});
            this.cmb_Action.Location = new System.Drawing.Point(105, 137);
            this.cmb_Action.Name = "cmb_Action";
            this.cmb_Action.Size = new System.Drawing.Size(198, 27);
            this.cmb_Action.TabIndex = 4;
            // 
            // chk_Date
            // 
            this.chk_Date.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_Date.Location = new System.Drawing.Point(9, 194);
            this.chk_Date.Name = "chk_Date";
            this.chk_Date.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Date.Size = new System.Drawing.Size(117, 31);
            this.chk_Date.TabIndex = 8;
            this.chk_Date.Text = "(BS&) Date";
            this.chk_Date.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Date.UseVisualStyleBackColor = true;
            // 
            // chklstbox_DayBook
            // 
            this.chklstbox_DayBook.CheckOnClick = true;
            this.chklstbox_DayBook.Dock = System.Windows.Forms.DockStyle.Right;
            this.chklstbox_DayBook.FormattingEnabled = true;
            this.chklstbox_DayBook.Location = new System.Drawing.Point(307, 0);
            this.chklstbox_DayBook.Name = "chklstbox_DayBook";
            this.chklstbox_DayBook.Size = new System.Drawing.Size(276, 266);
            this.chklstbox_DayBook.TabIndex = 7;
            // 
            // cmb_Branch
            // 
            this.cmb_Branch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Branch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmb_Branch.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_Branch.FormattingEnabled = true;
            this.cmb_Branch.Location = new System.Drawing.Point(105, 104);
            this.cmb_Branch.Name = "cmb_Branch";
            this.cmb_Branch.Size = new System.Drawing.Size(198, 27);
            this.cmb_Branch.TabIndex = 3;
            // 
            // lbl_Branch
            // 
            this.lbl_Branch.AutoSize = true;
            this.lbl_Branch.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Branch.Location = new System.Drawing.Point(9, 108);
            this.lbl_Branch.Name = "lbl_Branch";
            this.lbl_Branch.Size = new System.Drawing.Size(64, 19);
            this.lbl_Branch.TabIndex = 60;
            this.lbl_Branch.Text = "Branch";
            // 
            // msk_ToDate
            // 
            this.msk_ToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_ToDate.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msk_ToDate.Location = new System.Drawing.Point(106, 73);
            this.msk_ToDate.Mask = "00/00/0000";
            this.msk_ToDate.Name = "msk_ToDate";
            this.msk_ToDate.Size = new System.Drawing.Size(124, 25);
            this.msk_ToDate.TabIndex = 2;
            this.msk_ToDate.Enter += new System.EventHandler(this.msk_ToDate_Enter);
            this.msk_ToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.msk_ToDate_KeyDown);
            this.msk_ToDate.Leave += new System.EventHandler(this.msk_ToDate_Leave);
            this.msk_ToDate.Validated += new System.EventHandler(this.msk_ToDate_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 19);
            this.label2.TabIndex = 57;
            this.label2.Text = "To Date";
            // 
            // msk_FromDate
            // 
            this.msk_FromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_FromDate.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msk_FromDate.Location = new System.Drawing.Point(106, 42);
            this.msk_FromDate.Mask = "00/00/0000";
            this.msk_FromDate.Name = "msk_FromDate";
            this.msk_FromDate.Size = new System.Drawing.Size(124, 25);
            this.msk_FromDate.TabIndex = 1;
            this.msk_FromDate.Enter += new System.EventHandler(this.msk_FromDate_Enter);
            this.msk_FromDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.msk_FromDate_KeyDown);
            this.msk_FromDate.Leave += new System.EventHandler(this.msk_FromDate_Leave);
            this.msk_FromDate.Validated += new System.EventHandler(this.msk_FromDate_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 19);
            this.label1.TabIndex = 55;
            this.label1.Text = "From Date";
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.BtnCancel);
            this.StorePanel.Controls.Add(this.clsSeparator1);
            this.StorePanel.Controls.Add(this.BtnShow);
            this.StorePanel.Controls.Add(this.panel13);
            this.StorePanel.Controls.Add(this.label4);
            this.StorePanel.Controls.Add(this.panel15);
            this.StorePanel.Controls.Add(this.cmbSysDateType);
            this.StorePanel.Controls.Add(this.lbl_OpenFor);
            this.StorePanel.Controls.Add(this.panel16);
            this.StorePanel.Controls.Add(this.cmb_FilterBy);
            this.StorePanel.Controls.Add(this.chklstbox_DayBook);
            this.StorePanel.Controls.Add(this.lbl_Action);
            this.StorePanel.Controls.Add(this.label1);
            this.StorePanel.Controls.Add(this.cmb_Action);
            this.StorePanel.Controls.Add(this.msk_FromDate);
            this.StorePanel.Controls.Add(this.chk_Date);
            this.StorePanel.Controls.Add(this.label2);
            this.StorePanel.Controls.Add(this.cmb_Branch);
            this.StorePanel.Controls.Add(this.msk_ToDate);
            this.StorePanel.Controls.Add(this.lbl_Branch);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(583, 266);
            this.StorePanel.TabIndex = 2;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(98, 225);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(103, 38);
            this.BtnCancel.TabIndex = 97;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.Lavender;
            this.clsSeparator1.Location = new System.Drawing.Point(13, 221);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(290, 3);
            this.clsSeparator1.TabIndex = 96;
            this.clsSeparator1.TabStop = false;
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.Color.White;
            this.panel13.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel13.Location = new System.Drawing.Point(0, 3);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(3, 260);
            this.panel13.TabIndex = 4;
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.Color.White;
            this.panel15.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel15.Location = new System.Drawing.Point(0, 0);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(307, 3);
            this.panel15.TabIndex = 8;
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.White;
            this.panel16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel16.Location = new System.Drawing.Point(0, 263);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(307, 3);
            this.panel16.TabIndex = 0;
            // 
            // FrmAuditTrial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(583, 266);
            this.Controls.Add(this.StorePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmAuditTrial";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Audit Trial";
            this.Load += new System.EventHandler(this.FrmAuditTrial_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmAuditTrial_KeyPress);
            this.StorePanel.ResumeLayout(false);
            this.StorePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl_OpenFor;
        private System.Windows.Forms.ComboBox cmb_FilterBy;
        private System.Windows.Forms.Label lbl_Action;
        private System.Windows.Forms.ComboBox cmb_Action;
        private System.Windows.Forms.CheckBox chk_Date;
        private System.Windows.Forms.CheckedListBox chklstbox_DayBook;
        private System.Windows.Forms.MaskedTextBox msk_ToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox msk_FromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_Branch;
        private System.Windows.Forms.Label lbl_Branch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSysDateType;
        private DevExpress.XtraEditors.SimpleButton BtnShow;
        private System.Windows.Forms.Panel StorePanel;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Panel panel16;
        private ClsSeparator clsSeparator1;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
    }
}