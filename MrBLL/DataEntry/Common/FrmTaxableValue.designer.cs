using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.DataEntry.Common
{
    partial class FrmTaxableValue
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
            this.lbl_TaxableValue = new System.Windows.Forms.Label();
            this.txt_TaxableValue = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_TaxableValue
            // 
            this.lbl_TaxableValue.AutoSize = true;
            this.lbl_TaxableValue.Font = new System.Drawing.Font("Arial", 11F);
            this.lbl_TaxableValue.Location = new System.Drawing.Point(8, 23);
            this.lbl_TaxableValue.Name = "lbl_TaxableValue";
            this.lbl_TaxableValue.Size = new System.Drawing.Size(96, 17);
            this.lbl_TaxableValue.TabIndex = 55;
            this.lbl_TaxableValue.Text = "Taxable Value";
            // 
            // txt_TaxableValue
            // 
            this.txt_TaxableValue.AcceptsTab = true;
            this.txt_TaxableValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_TaxableValue.Font = new System.Drawing.Font("Arial", 11F);
            this.txt_TaxableValue.Location = new System.Drawing.Point(116, 20);
            this.txt_TaxableValue.MaxLength = 25;
            this.txt_TaxableValue.Name = "txt_TaxableValue";
            this.txt_TaxableValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_TaxableValue.Size = new System.Drawing.Size(150, 24);
            this.txt_TaxableValue.TabIndex = 1;
            this.txt_TaxableValue.Enter += new System.EventHandler(this.txt_TaxableValue_Enter);
            this.txt_TaxableValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_TaxableValue_KeyDown);
            this.txt_TaxableValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_TaxableValue_KeyPress);
            this.txt_TaxableValue.Leave += new System.EventHandler(this.txt_TaxableValue_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_TaxableValue);
            this.groupBox1.Controls.Add(this.txt_TaxableValue);
            this.groupBox1.Location = new System.Drawing.Point(5, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 55);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btn_Save
            // 
            this.btn_Save.BackColor = System.Drawing.Color.SteelBlue;
            this.btn_Save.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Save.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.ForeColor = System.Drawing.SystemColors.Window;
            this.btn_Save.Location = new System.Drawing.Point(204, 55);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(74, 26);
            this.btn_Save.TabIndex = 2;
            this.btn_Save.Text = "&Ok";
            this.btn_Save.UseVisualStyleBackColor = false;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // FrmTaxableValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 82);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTaxableValue";
            this.ShowIcon = false;
            this.Text = "Taxable Value";
            this.Load += new System.EventHandler(this.FrmTaxableValue_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TaxableValue_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_TaxableValue;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Save;
        private MrTextBox txt_TaxableValue;
    }
}