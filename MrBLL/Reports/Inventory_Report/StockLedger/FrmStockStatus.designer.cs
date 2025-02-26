using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Inventory_Report.StockLedger
{
    partial class FrmStockStatus
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
            this.btn_Show = new System.Windows.Forms.Button();
            this.gb_TBOptions = new System.Windows.Forms.GroupBox();
            this.lbl_Branch = new System.Windows.Forms.Label();
            this.cmb_Branch = new System.Windows.Forms.ComboBox();
            this.cmb_Unit = new System.Windows.Forms.ComboBox();
            this.lbl_Unit = new System.Windows.Forms.Label();
            this.chk_Date = new System.Windows.Forms.CheckBox();
            this.chk_SelectAll = new System.Windows.Forms.CheckBox();
            this.msk_AsOnDate = new MrMaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gb_TBOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Show
            // 
            this.btn_Show.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Show.Location = new System.Drawing.Point(136, 96);
            this.btn_Show.Name = "btn_Show";
            this.btn_Show.Size = new System.Drawing.Size(71, 28);
            this.btn_Show.TabIndex = 15;
            this.btn_Show.Text = "&Show";
            this.btn_Show.UseVisualStyleBackColor = true;
            this.btn_Show.Click += new System.EventHandler(this.btn_Show_Click);
            // 
            // gb_TBOptions
            // 
            this.gb_TBOptions.Controls.Add(this.lbl_Branch);
            this.gb_TBOptions.Controls.Add(this.cmb_Branch);
            this.gb_TBOptions.Controls.Add(this.cmb_Unit);
            this.gb_TBOptions.Controls.Add(this.lbl_Unit);
            this.gb_TBOptions.Controls.Add(this.chk_Date);
            this.gb_TBOptions.Controls.Add(this.chk_SelectAll);
            this.gb_TBOptions.Controls.Add(this.msk_AsOnDate);
            this.gb_TBOptions.Controls.Add(this.label1);
            this.gb_TBOptions.Location = new System.Drawing.Point(2, -1);
            this.gb_TBOptions.Name = "gb_TBOptions";
            this.gb_TBOptions.Size = new System.Drawing.Size(330, 94);
            this.gb_TBOptions.TabIndex = 14;
            this.gb_TBOptions.TabStop = false;
            // 
            // lbl_Branch
            // 
            this.lbl_Branch.AutoSize = true;
            this.lbl_Branch.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_Branch.Location = new System.Drawing.Point(10, 70);
            this.lbl_Branch.Name = "lbl_Branch";
            this.lbl_Branch.Size = new System.Drawing.Size(49, 15);
            this.lbl_Branch.TabIndex = 79;
            this.lbl_Branch.Text = "Branch ";
            // 
            // cmb_Branch
            // 
            this.cmb_Branch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Branch.FormattingEnabled = true;
            this.cmb_Branch.Location = new System.Drawing.Point(86, 68);
            this.cmb_Branch.Name = "cmb_Branch";
            this.cmb_Branch.Size = new System.Drawing.Size(105, 21);
            this.cmb_Branch.TabIndex = 3;
            // 
            // cmb_Unit
            // 
            this.cmb_Unit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Unit.FormattingEnabled = true;
            this.cmb_Unit.Location = new System.Drawing.Point(86, 42);
            this.cmb_Unit.Name = "cmb_Unit";
            this.cmb_Unit.Size = new System.Drawing.Size(104, 21);
            this.cmb_Unit.TabIndex = 2;
            // 
            // lbl_Unit
            // 
            this.lbl_Unit.AutoSize = true;
            this.lbl_Unit.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_Unit.Location = new System.Drawing.Point(10, 42);
            this.lbl_Unit.Name = "lbl_Unit";
            this.lbl_Unit.Size = new System.Drawing.Size(29, 15);
            this.lbl_Unit.TabIndex = 75;
            this.lbl_Unit.Text = "Unit";
            // 
            // chk_Date
            // 
            this.chk_Date.Font = new System.Drawing.Font("Arial", 9F);
            this.chk_Date.Location = new System.Drawing.Point(208, 19);
            this.chk_Date.Name = "chk_Date";
            this.chk_Date.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Date.Size = new System.Drawing.Size(117, 19);
            this.chk_Date.TabIndex = 4;
            this.chk_Date.Text = "Date";
            this.chk_Date.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Date.UseVisualStyleBackColor = true;
            // 
            // chk_SelectAll
            // 
            this.chk_SelectAll.Font = new System.Drawing.Font("Arial", 9F);
            this.chk_SelectAll.Location = new System.Drawing.Point(208, 45);
            this.chk_SelectAll.Name = "chk_SelectAll";
            this.chk_SelectAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_SelectAll.Size = new System.Drawing.Size(117, 19);
            this.chk_SelectAll.TabIndex = 5;
            this.chk_SelectAll.Text = "Select All";
            this.chk_SelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_SelectAll.UseVisualStyleBackColor = true;
            // 
            // msk_AsOnDate
            // 
            this.msk_AsOnDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_AsOnDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msk_AsOnDate.Location = new System.Drawing.Point(88, 16);
            this.msk_AsOnDate.Mask = "00/00/0000";
            this.msk_AsOnDate.Name = "msk_AsOnDate";
            this.msk_AsOnDate.Size = new System.Drawing.Size(102, 20);
            this.msk_AsOnDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F);
            this.label1.Location = new System.Drawing.Point(10, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 15);
            this.label1.TabIndex = 55;
            this.label1.Text = "As On Date";
            // 
            // FrmStockStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 131);
            this.Controls.Add(this.btn_Show);
            this.Controls.Add(this.gb_TBOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Name = "FrmStockStatus";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Status";
            this.Load += new System.EventHandler(this.FrmStockStatus_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmStockStatus_KeyPress);
            this.gb_TBOptions.ResumeLayout(false);
            this.gb_TBOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Show;
        private System.Windows.Forms.GroupBox gb_TBOptions;
        private System.Windows.Forms.ComboBox cmb_Unit;
        private System.Windows.Forms.Label lbl_Unit;
        private System.Windows.Forms.CheckBox chk_Date;
        private System.Windows.Forms.CheckBox chk_SelectAll;
        private System.Windows.Forms.MaskedTextBox msk_AsOnDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_Branch;
        private System.Windows.Forms.ComboBox cmb_Branch;
    }
}