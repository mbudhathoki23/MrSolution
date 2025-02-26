using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Register_Report.Purchase_Register
{
    partial class FrmPurchaseProductTopNwise
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
            this.txt_NoOfItems = new MrTextBox();
            this.lbl_NoOfItems = new System.Windows.Forms.Label();
            this.cmb_OrderOn = new System.Windows.Forms.ComboBox();
            this.lbl_OrderOn = new System.Windows.Forms.Label();
            this.cmb_OrderBy = new System.Windows.Forms.ComboBox();
            this.lbl_OrderBy = new System.Windows.Forms.Label();
            this.msk_ToDate = new MrMaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.msk_FromDate = new MrMaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_Branch = new System.Windows.Forms.Label();
            this.cmb_Branch = new System.Windows.Forms.ComboBox();
            this.gb_TBOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Show
            // 
            this.btn_Show.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Show.Location = new System.Drawing.Point(206, 105);
            this.btn_Show.Name = "btn_Show";
            this.btn_Show.Size = new System.Drawing.Size(71, 27);
            this.btn_Show.TabIndex = 7;
            this.btn_Show.Text = "&Show";
            this.btn_Show.UseVisualStyleBackColor = true;
            this.btn_Show.Click += new System.EventHandler(this.btn_Show_Click);
            // 
            // gb_TBOptions
            // 
            this.gb_TBOptions.Controls.Add(this.lbl_Branch);
            this.gb_TBOptions.Controls.Add(this.cmb_Branch);
            this.gb_TBOptions.Controls.Add(this.txt_NoOfItems);
            this.gb_TBOptions.Controls.Add(this.lbl_NoOfItems);
            this.gb_TBOptions.Controls.Add(this.cmb_OrderOn);
            this.gb_TBOptions.Controls.Add(this.lbl_OrderOn);
            this.gb_TBOptions.Controls.Add(this.cmb_OrderBy);
            this.gb_TBOptions.Controls.Add(this.lbl_OrderBy);
            this.gb_TBOptions.Controls.Add(this.msk_ToDate);
            this.gb_TBOptions.Controls.Add(this.label2);
            this.gb_TBOptions.Controls.Add(this.msk_FromDate);
            this.gb_TBOptions.Controls.Add(this.label1);
            this.gb_TBOptions.Location = new System.Drawing.Point(4, -2);
            this.gb_TBOptions.Name = "gb_TBOptions";
            this.gb_TBOptions.Size = new System.Drawing.Size(453, 101);
            this.gb_TBOptions.TabIndex = 0;
            this.gb_TBOptions.TabStop = false;
            // 
            // txt_NoOfItems
            // 
            this.txt_NoOfItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_NoOfItems.Location = new System.Drawing.Point(333, 13);
            this.txt_NoOfItems.Name = "txt_NoOfItems";
            this.txt_NoOfItems.Size = new System.Drawing.Size(105, 20);
            this.txt_NoOfItems.TabIndex = 4;
            this.txt_NoOfItems.Text = "20";
            this.txt_NoOfItems.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_NoOfItems.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_NoOfItems_KeyPress);
            // 
            // lbl_NoOfItems
            // 
            this.lbl_NoOfItems.AutoSize = true;
            this.lbl_NoOfItems.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_NoOfItems.Location = new System.Drawing.Point(233, 16);
            this.lbl_NoOfItems.Name = "lbl_NoOfItems";
            this.lbl_NoOfItems.Size = new System.Drawing.Size(72, 15);
            this.lbl_NoOfItems.TabIndex = 73;
            this.lbl_NoOfItems.Text = "No Of Items";
            // 
            // cmb_OrderOn
            // 
            this.cmb_OrderOn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_OrderOn.FormattingEnabled = true;
            this.cmb_OrderOn.Items.AddRange(new object[] {
            "VALUE",
            "QUANTITY"});
            this.cmb_OrderOn.Location = new System.Drawing.Point(332, 39);
            this.cmb_OrderOn.Name = "cmb_OrderOn";
            this.cmb_OrderOn.Size = new System.Drawing.Size(105, 21);
            this.cmb_OrderOn.TabIndex = 5;
            // 
            // lbl_OrderOn
            // 
            this.lbl_OrderOn.AutoSize = true;
            this.lbl_OrderOn.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_OrderOn.Location = new System.Drawing.Point(233, 41);
            this.lbl_OrderOn.Name = "lbl_OrderOn";
            this.lbl_OrderOn.Size = new System.Drawing.Size(57, 15);
            this.lbl_OrderOn.TabIndex = 64;
            this.lbl_OrderOn.Text = "Order On";
            // 
            // cmb_OrderBy
            // 
            this.cmb_OrderBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_OrderBy.FormattingEnabled = true;
            this.cmb_OrderBy.Items.AddRange(new object[] {
            "TOP \'NO\'",
            "BUTTOM \'NO\'"});
            this.cmb_OrderBy.Location = new System.Drawing.Point(101, 67);
            this.cmb_OrderBy.Name = "cmb_OrderBy";
            this.cmb_OrderBy.Size = new System.Drawing.Size(105, 21);
            this.cmb_OrderBy.TabIndex = 3;
            // 
            // lbl_OrderBy
            // 
            this.lbl_OrderBy.AutoSize = true;
            this.lbl_OrderBy.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_OrderBy.Location = new System.Drawing.Point(10, 69);
            this.lbl_OrderBy.Name = "lbl_OrderBy";
            this.lbl_OrderBy.Size = new System.Drawing.Size(54, 15);
            this.lbl_OrderBy.TabIndex = 62;
            this.lbl_OrderBy.Text = "Order By";
            // 
            // msk_ToDate
            // 
            this.msk_ToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_ToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msk_ToDate.Location = new System.Drawing.Point(101, 42);
            this.msk_ToDate.Mask = "00/00/0000";
            this.msk_ToDate.Name = "msk_ToDate";
            this.msk_ToDate.Size = new System.Drawing.Size(105, 20);
            this.msk_ToDate.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F);
            this.label2.Location = new System.Drawing.Point(10, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 15);
            this.label2.TabIndex = 57;
            this.label2.Text = "To Date";
            // 
            // msk_FromDate
            // 
            this.msk_FromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_FromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msk_FromDate.Location = new System.Drawing.Point(101, 17);
            this.msk_FromDate.Mask = "00/00/0000";
            this.msk_FromDate.Name = "msk_FromDate";
            this.msk_FromDate.Size = new System.Drawing.Size(105, 20);
            this.msk_FromDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F);
            this.label1.Location = new System.Drawing.Point(10, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 15);
            this.label1.TabIndex = 55;
            this.label1.Text = "From Date";
            // 
            // lbl_Branch
            // 
            this.lbl_Branch.AutoSize = true;
            this.lbl_Branch.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_Branch.Location = new System.Drawing.Point(233, 67);
            this.lbl_Branch.Name = "lbl_Branch";
            this.lbl_Branch.Size = new System.Drawing.Size(49, 15);
            this.lbl_Branch.TabIndex = 75;
            this.lbl_Branch.Text = "Branch ";
            // 
            // cmb_Branch
            // 
            this.cmb_Branch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Branch.FormattingEnabled = true;
            this.cmb_Branch.Location = new System.Drawing.Point(332, 65);
            this.cmb_Branch.Name = "cmb_Branch";
            this.cmb_Branch.Size = new System.Drawing.Size(105, 21);
            this.cmb_Branch.TabIndex = 6;
            // 
            // PurchaseProductTopNwise
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 138);
            this.Controls.Add(this.btn_Show);
            this.Controls.Add(this.gb_TBOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmPurchaseProductTopNwise";
            this.Text = "Purchase Product Top No Wise";
            this.Load += new System.EventHandler(this.FrmPurchaseProductTopNwise_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmPurchaseProductTopNwise_KeyPress);
            this.gb_TBOptions.ResumeLayout(false);
            this.gb_TBOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Show;
        private System.Windows.Forms.GroupBox gb_TBOptions;
        private System.Windows.Forms.TextBox txt_NoOfItems;
        private System.Windows.Forms.Label lbl_NoOfItems;
        private System.Windows.Forms.ComboBox cmb_OrderOn;
        private System.Windows.Forms.Label lbl_OrderOn;
        private System.Windows.Forms.ComboBox cmb_OrderBy;
        private System.Windows.Forms.Label lbl_OrderBy;
        private System.Windows.Forms.MaskedTextBox msk_ToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox msk_FromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_Branch;
        private System.Windows.Forms.ComboBox cmb_Branch;
    }
}