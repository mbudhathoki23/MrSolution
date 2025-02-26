using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.POS.Master
{
    partial class FrmDiscount
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
            this.label19 = new System.Windows.Forms.Label();
            this.TxtDiscount = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtDiscAmount = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.SuspendLayout();
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Bookman Old Style", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(7, 27);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(98, 23);
            this.label19.TabIndex = 17;
            this.label19.Text = "Discount";
            // 
            // TxtDiscount
            // 
            this.TxtDiscount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDiscount.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDiscount.Location = new System.Drawing.Point(109, 24);
            this.TxtDiscount.MaxLength = 50;
            this.TxtDiscount.Name = "TxtDiscount";
            this.TxtDiscount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TxtDiscount.Size = new System.Drawing.Size(79, 30);
            this.TxtDiscount.TabIndex = 0;
            this.TxtDiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDiscount_KeyPress);
            this.TxtDiscount.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDiscount_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(191, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 23);
            this.label1.TabIndex = 18;
            this.label1.Text = "%";
            // 
            // TxtDiscAmount
            // 
            this.TxtDiscAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDiscAmount.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDiscAmount.Location = new System.Drawing.Point(230, 24);
            this.TxtDiscAmount.MaxLength = 255;
            this.TxtDiscAmount.Name = "TxtDiscAmount";
            this.TxtDiscAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TxtDiscAmount.Size = new System.Drawing.Size(168, 30);
            this.TxtDiscAmount.TabIndex = 1;
            this.TxtDiscAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDiscAmount_KeyDown);
            this.TxtDiscAmount.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDiscAmount_Validating);
            // 
            // FrmDiscount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 90);
            this.Controls.Add(this.TxtDiscAmount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.TxtDiscount);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDiscount";
            this.ShowIcon = false;
            this.Text = "Discount";
            this.Load += new System.EventHandler(this.FrmDiscount_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmDiscount_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmDiscount_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label1;
        private MrTextBox TxtDiscount;
        private MrTextBox TxtDiscAmount;
    }
}