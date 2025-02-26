using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Register_Report.Register_Report
{
    partial class OtherCurrency
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
            this.gb_Currency = new System.Windows.Forms.GroupBox();
            this.txt_CurrencyRate = new MrTextBox();
            this.btn_Currency = new System.Windows.Forms.Button();
            this.txt_Currency = new MrTextBox();
            this.lbl_CurrencyRate = new System.Windows.Forms.Label();
            this.lbl_Currency = new System.Windows.Forms.Label();
            this.btn_Save = new System.Windows.Forms.Button();
            this.gb_Currency.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_Currency
            // 
            this.gb_Currency.Controls.Add(this.txt_CurrencyRate);
            this.gb_Currency.Controls.Add(this.btn_Currency);
            this.gb_Currency.Controls.Add(this.txt_Currency);
            this.gb_Currency.Controls.Add(this.lbl_CurrencyRate);
            this.gb_Currency.Controls.Add(this.lbl_Currency);
            this.gb_Currency.Controls.Add(this.btn_Save);
            this.gb_Currency.Location = new System.Drawing.Point(2, 1);
            this.gb_Currency.Name = "gb_Currency";
            this.gb_Currency.Size = new System.Drawing.Size(294, 133);
            this.gb_Currency.TabIndex = 0;
            this.gb_Currency.TabStop = false;
            // 
            // txt_CurrencyRate
            // 
            this.txt_CurrencyRate.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txt_CurrencyRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_CurrencyRate.Font = new System.Drawing.Font("Arial", 9F);
            this.txt_CurrencyRate.Location = new System.Drawing.Point(85, 47);
            this.txt_CurrencyRate.MaxLength = 255;
            this.txt_CurrencyRate.Name = "txt_CurrencyRate";
            this.txt_CurrencyRate.Size = new System.Drawing.Size(96, 21);
            this.txt_CurrencyRate.TabIndex = 3;
            this.txt_CurrencyRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_CurrencyRate.Enter += new System.EventHandler(this.txt_CurrencyRate_Enter);
            this.txt_CurrencyRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_CurrencyRate_KeyDown);
            this.txt_CurrencyRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_CurrencyRate_KeyPress);
            this.txt_CurrencyRate.Leave += new System.EventHandler(this.txt_CurrencyRate_Leave);
            this.txt_CurrencyRate.Validated += new System.EventHandler(this.txt_CurrencyRate_Validated);
            // 
            // btn_Currency
            // 
            this.btn_Currency.CausesValidation = false;
            this.btn_Currency.Location = new System.Drawing.Point(236, 14);
            this.btn_Currency.Name = "btn_Currency";
            this.btn_Currency.Size = new System.Drawing.Size(27, 23);
            this.btn_Currency.TabIndex = 2;
            this.btn_Currency.TabStop = false;
            this.btn_Currency.Text = "..";
            this.btn_Currency.UseVisualStyleBackColor = true;
            this.btn_Currency.Click += new System.EventHandler(this.btn_Currency_Click);
            // 
            // txt_Currency
            // 
            this.txt_Currency.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txt_Currency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Currency.Font = new System.Drawing.Font("Arial", 9F);
            this.txt_Currency.Location = new System.Drawing.Point(85, 15);
            this.txt_Currency.MaxLength = 255;
            this.txt_Currency.Name = "txt_Currency";
            this.txt_Currency.Size = new System.Drawing.Size(150, 21);
            this.txt_Currency.TabIndex = 1;
            this.txt_Currency.Enter += new System.EventHandler(this.txt_Currency_Enter);
            this.txt_Currency.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Currency_KeyDown);
            this.txt_Currency.Leave += new System.EventHandler(this.txt_Currency_Leave);
            this.txt_Currency.Validated += new System.EventHandler(this.txt_Currency_Validated);
            // 
            // lbl_CurrencyRate
            // 
            this.lbl_CurrencyRate.AutoSize = true;
            this.lbl_CurrencyRate.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_CurrencyRate.Location = new System.Drawing.Point(10, 50);
            this.lbl_CurrencyRate.Name = "lbl_CurrencyRate";
            this.lbl_CurrencyRate.Size = new System.Drawing.Size(33, 15);
            this.lbl_CurrencyRate.TabIndex = 60;
            this.lbl_CurrencyRate.Text = "Rate";
            // 
            // lbl_Currency
            // 
            this.lbl_Currency.AutoSize = true;
            this.lbl_Currency.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_Currency.Location = new System.Drawing.Point(10, 18);
            this.lbl_Currency.Name = "lbl_Currency";
            this.lbl_Currency.Size = new System.Drawing.Size(56, 15);
            this.lbl_Currency.TabIndex = 59;
            this.lbl_Currency.Text = "Currency";
            // 
            // btn_Save
            // 
            this.btn_Save.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.Location = new System.Drawing.Point(110, 97);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(74, 28);
            this.btn_Save.TabIndex = 4;
            this.btn_Save.Text = "&Ok";
            this.btn_Save.UseVisualStyleBackColor = true;
            // 
            // OtherCurrency
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(300, 134);
            this.Controls.Add(this.gb_Currency);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OtherCurrency";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Other Currency";
            this.Load += new System.EventHandler(this.OtherCurrency_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OtherCurrency_KeyPress);
            this.gb_Currency.ResumeLayout(false);
            this.gb_Currency.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_Currency;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.TextBox txt_CurrencyRate;
        private System.Windows.Forms.Button btn_Currency;
        private System.Windows.Forms.TextBox txt_Currency;
        private System.Windows.Forms.Label lbl_CurrencyRate;
        private System.Windows.Forms.Label lbl_Currency;
    }
}