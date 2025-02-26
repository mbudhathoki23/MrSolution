using System.ComponentModel;
using System.Windows.Forms;
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Finance_Report.Analysis
{
    partial class FrmCashFlowStatement
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
            this.btn_Show = new System.Windows.Forms.Button();
            this.gb_TBOptions = new System.Windows.Forms.GroupBox();
            this.btn_BookName = new System.Windows.Forms.Button();
            this.txt_BookName = new MrTextBox();
            this.lbl_BankName = new System.Windows.Forms.Label();
            this.lbl_Branch = new System.Windows.Forms.Label();
            this.cmb_Branch = new System.Windows.Forms.ComboBox();
            this.chk_Date = new System.Windows.Forms.CheckBox();
            this.msk_ToDate = new MrMaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.msk_FromDate = new MrMaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gb_TBOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Show
            // 
            this.btn_Show.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Show.Location = new System.Drawing.Point(195, 109);
            this.btn_Show.Name = "btn_Show";
            this.btn_Show.Size = new System.Drawing.Size(70, 26);
            this.btn_Show.TabIndex = 7;
            this.btn_Show.Text = "&Show";
            this.btn_Show.UseVisualStyleBackColor = true;
            this.btn_Show.Click += new System.EventHandler(this.btn_Show_Click);
            // 
            // gb_TBOptions
            // 
            this.gb_TBOptions.Controls.Add(this.btn_BookName);
            this.gb_TBOptions.Controls.Add(this.txt_BookName);
            this.gb_TBOptions.Controls.Add(this.lbl_BankName);
            this.gb_TBOptions.Controls.Add(this.lbl_Branch);
            this.gb_TBOptions.Controls.Add(this.cmb_Branch);
            this.gb_TBOptions.Controls.Add(this.chk_Date);
            this.gb_TBOptions.Controls.Add(this.msk_ToDate);
            this.gb_TBOptions.Controls.Add(this.label2);
            this.gb_TBOptions.Controls.Add(this.msk_FromDate);
            this.gb_TBOptions.Controls.Add(this.label1);
            this.gb_TBOptions.Location = new System.Drawing.Point(1, -1);
            this.gb_TBOptions.Name = "gb_TBOptions";
            this.gb_TBOptions.Size = new System.Drawing.Size(561, 104);
            this.gb_TBOptions.TabIndex = 29;
            this.gb_TBOptions.TabStop = false;
            // 
            // btn_BookName
            // 
            this.btn_BookName.Location = new System.Drawing.Point(529, 15);
            this.btn_BookName.Name = "btn_BookName";
            this.btn_BookName.Size = new System.Drawing.Size(25, 23);
            this.btn_BookName.TabIndex = 5;
            this.btn_BookName.TabStop = false;
            this.btn_BookName.Text = "..";
            this.btn_BookName.UseVisualStyleBackColor = true;
            this.btn_BookName.Click += new System.EventHandler(this.btn_BookName_Click);
            // 
            // txt_BookName
            // 
            this.txt_BookName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_BookName.Location = new System.Drawing.Point(307, 16);
            this.txt_BookName.MaxLength = 255;
            this.txt_BookName.Name = "txt_BookName";
            this.txt_BookName.Size = new System.Drawing.Size(222, 20);
            this.txt_BookName.TabIndex = 4;
            this.txt_BookName.Enter += new System.EventHandler(this.txt_BookName_Enter);
            this.txt_BookName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_BookName_KeyDown);
            this.txt_BookName.Leave += new System.EventHandler(this.txt_BookName_Leave);
            this.txt_BookName.Validated += new System.EventHandler(this.txt_BookName_Validated);
            // 
            // lbl_BankName
            // 
            this.lbl_BankName.AutoSize = true;
            this.lbl_BankName.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_BankName.Location = new System.Drawing.Point(211, 19);
            this.lbl_BankName.Name = "lbl_BankName";
            this.lbl_BankName.Size = new System.Drawing.Size(72, 15);
            this.lbl_BankName.TabIndex = 81;
            this.lbl_BankName.Text = "Book Name";
            // 
            // lbl_Branch
            // 
            this.lbl_Branch.AutoSize = true;
            this.lbl_Branch.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_Branch.Location = new System.Drawing.Point(10, 70);
            this.lbl_Branch.Name = "lbl_Branch";
            this.lbl_Branch.Size = new System.Drawing.Size(49, 15);
            this.lbl_Branch.TabIndex = 75;
            this.lbl_Branch.Text = "Branch ";
            // 
            // cmb_Branch
            // 
            this.cmb_Branch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Branch.FormattingEnabled = true;
            this.cmb_Branch.Location = new System.Drawing.Point(86, 68);
            this.cmb_Branch.Name = "cmb_Branch";
            this.cmb_Branch.Size = new System.Drawing.Size(110, 21);
            this.cmb_Branch.TabIndex = 3;
            // 
            // chk_Date
            // 
            this.chk_Date.Font = new System.Drawing.Font("Arial", 9F);
            this.chk_Date.Location = new System.Drawing.Point(210, 44);
            this.chk_Date.Name = "chk_Date";
            this.chk_Date.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Date.Size = new System.Drawing.Size(111, 19);
            this.chk_Date.TabIndex = 6;
            this.chk_Date.Text = "Date";
            this.chk_Date.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Date.UseVisualStyleBackColor = true;
            // 
            // msk_ToDate
            // 
            this.msk_ToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_ToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msk_ToDate.Location = new System.Drawing.Point(88, 43);
            this.msk_ToDate.Mask = "00/00/0000";
            this.msk_ToDate.Name = "msk_ToDate";
            this.msk_ToDate.Size = new System.Drawing.Size(107, 20);
            this.msk_ToDate.TabIndex = 2;
            this.msk_ToDate.Enter += new System.EventHandler(this.msk_ToDate_Enter);
            this.msk_ToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.msk_ToDate_KeyDown);
            this.msk_ToDate.Leave += new System.EventHandler(this.msk_ToDate_Leave);
            this.msk_ToDate.Validated += new System.EventHandler(this.msk_ToDate_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F);
            this.label2.Location = new System.Drawing.Point(10, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 15);
            this.label2.TabIndex = 57;
            this.label2.Text = "To Date";
            // 
            // msk_FromDate
            // 
            this.msk_FromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_FromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msk_FromDate.Location = new System.Drawing.Point(88, 18);
            this.msk_FromDate.Mask = "00/00/0000";
            this.msk_FromDate.Name = "msk_FromDate";
            this.msk_FromDate.Size = new System.Drawing.Size(107, 20);
            this.msk_FromDate.TabIndex = 1;
            this.msk_FromDate.Enter += new System.EventHandler(this.msk_FromDate_Enter);
            this.msk_FromDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.msk_FromDate_KeyDown);
            this.msk_FromDate.Leave += new System.EventHandler(this.msk_FromDate_Leave);
            this.msk_FromDate.Validated += new System.EventHandler(this.msk_FromDate_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F);
            this.label1.Location = new System.Drawing.Point(10, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 15);
            this.label1.TabIndex = 55;
            this.label1.Text = "From Date";
            // 
            // FrmCashFlowStatement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 138);
            this.Controls.Add(this.btn_Show);
            this.Controls.Add(this.gb_TBOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Name = "FrmCashFlowStatement";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cash Flow Statement";
            this.Load += new System.EventHandler(this.FrmCashFlowStatement_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCashFlowStatement_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmCashFlowStatement_KeyPress);
            this.gb_TBOptions.ResumeLayout(false);
            this.gb_TBOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Button btn_Show;
        private GroupBox gb_TBOptions;
        private CheckBox chk_Date;
        private MaskedTextBox msk_ToDate;
        private Label label2;
        private MaskedTextBox msk_FromDate;
        private Label label1;
        private Label lbl_Branch;
        private ComboBox cmb_Branch;
        private Button btn_BookName;
        private TextBox txt_BookName;
        private Label lbl_BankName;
    }
}