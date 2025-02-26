using System.ComponentModel;
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Reconcile.BankReconcile
{
    partial class FrmBankReconciliationStatement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBankReconciliationStatement));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TxtCashLedger = new MrTextBox();
            this.lbl_BankName = new System.Windows.Forms.Label();
            this.BtnLedger = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_Show = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.CmbDateType = new System.Windows.Forms.ComboBox();
            this.MskFrom = new MrMaskedTextBox();
            this.MskToDate = new MrMaskedTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mrPanel1 = new MrPanel();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.mrPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TxtCashLedger);
            this.groupBox2.Controls.Add(this.lbl_BankName);
            this.groupBox2.Controls.Add(this.BtnLedger);
            this.groupBox2.Location = new System.Drawing.Point(3, 37);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(499, 40);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // TxtCashLedger
            // 
            this.TxtCashLedger.BackColor = System.Drawing.Color.White;
            this.TxtCashLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCashLedger.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCashLedger.Location = new System.Drawing.Point(118, 9);
            this.TxtCashLedger.MaxLength = 255;
            this.TxtCashLedger.Name = "TxtCashLedger";
            this.TxtCashLedger.ReadOnly = true;
            this.TxtCashLedger.Size = new System.Drawing.Size(347, 25);
            this.TxtCashLedger.TabIndex = 1;
            // 
            // lbl_BankName
            // 
            this.lbl_BankName.AutoSize = true;
            this.lbl_BankName.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_BankName.Location = new System.Drawing.Point(4, 12);
            this.lbl_BankName.Name = "lbl_BankName";
            this.lbl_BankName.Size = new System.Drawing.Size(108, 19);
            this.lbl_BankName.TabIndex = 0;
            this.lbl_BankName.Text = "Cash && Bank";
            // 
            // BtnLedger
            // 
            this.BtnLedger.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnLedger.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnLedger.Location = new System.Drawing.Point(468, 8);
            this.BtnLedger.Name = "BtnLedger";
            this.BtnLedger.Size = new System.Drawing.Size(27, 27);
            this.BtnLedger.TabIndex = 14;
            this.BtnLedger.TabStop = false;
            this.BtnLedger.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(4, 69);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(498, 181);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_Show);
            this.groupBox3.Controls.Add(this.btnCancel);
            this.groupBox3.Location = new System.Drawing.Point(3, 242);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(499, 53);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            // 
            // btn_Show
            // 
            this.btn_Show.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Show.Appearance.Options.UseFont = true;
            this.btn_Show.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.btn_Show.Location = new System.Drawing.Point(241, 12);
            this.btn_Show.Name = "btn_Show";
            this.btn_Show.Size = new System.Drawing.Size(97, 35);
            this.btn_Show.TabIndex = 0;
            this.btn_Show.Text = "&SHOW";
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(340, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(125, 35);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&CANCEL";
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
            this.CmbDateType.Location = new System.Drawing.Point(6, 13);
            this.CmbDateType.Name = "CmbDateType";
            this.CmbDateType.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CmbDateType.Size = new System.Drawing.Size(251, 28);
            this.CmbDateType.TabIndex = 0;
            // 
            // MskFrom
            // 
            this.MskFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskFrom.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskFrom.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskFrom.Location = new System.Drawing.Point(263, 14);
            this.MskFrom.Mask = "00/00/0000";
            this.MskFrom.Name = "MskFrom";
            this.MskFrom.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.MskFrom.Size = new System.Drawing.Size(115, 26);
            this.MskFrom.TabIndex = 1;
            // 
            // MskToDate
            // 
            this.MskToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskToDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskToDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskToDate.Location = new System.Drawing.Point(380, 14);
            this.MskToDate.Mask = "00/00/0000";
            this.MskToDate.Name = "MskToDate";
            this.MskToDate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.MskToDate.Size = new System.Drawing.Size(115, 26);
            this.MskToDate.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CmbDateType);
            this.groupBox1.Controls.Add(this.MskFrom);
            this.groupBox1.Controls.Add(this.MskToDate);
            this.groupBox1.Location = new System.Drawing.Point(3, -3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(499, 46);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // mrPanel1
            // 
            this.mrPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel1.Controls.Add(this.groupBox1);
            this.mrPanel1.Controls.Add(this.groupBox2);
            this.mrPanel1.Controls.Add(this.groupBox4);
            this.mrPanel1.Controls.Add(this.groupBox3);
            this.mrPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrPanel1.Location = new System.Drawing.Point(0, 0);
            this.mrPanel1.Name = "mrPanel1";
            this.mrPanel1.Size = new System.Drawing.Size(508, 298);
            this.mrPanel1.TabIndex = 2;
            // 
            // FrmBankReconciliationStatement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 298);
            this.Controls.Add(this.mrPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmBankReconciliationStatement";
            this.ShowIcon = false;
            this.Text = "Bank Reconciliation Statement";
            this.Load += new System.EventHandler(this.FrmBankReconciliationStatement_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmBankReconciliationStatement_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmBankReconciliationStatement_KeyPress);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.mrPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private MrTextBox TxtCashLedger;
        private System.Windows.Forms.Label lbl_BankName;
        private System.Windows.Forms.Button BtnLedger;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraEditors.SimpleButton btn_Show;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.ComboBox CmbDateType;
        private MrMaskedTextBox MskFrom;
        private MrMaskedTextBox MskToDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private MrPanel mrPanel1;
    }
}