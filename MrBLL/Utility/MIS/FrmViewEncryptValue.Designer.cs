namespace MrBLL.Utility.MIS
{
    partial class FrmViewEncryptValue
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
            this.mrPanel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.BtnConvert = new System.Windows.Forms.Button();
            this.TxtResult = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtInputValue = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.mrPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mrPanel1
            // 
            this.mrPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel1.Controls.Add(this.button1);
            this.mrPanel1.Controls.Add(this.BtnConvert);
            this.mrPanel1.Controls.Add(this.TxtResult);
            this.mrPanel1.Controls.Add(this.TxtInputValue);
            this.mrPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrPanel1.Location = new System.Drawing.Point(0, 0);
            this.mrPanel1.Name = "mrPanel1";
            this.mrPanel1.Size = new System.Drawing.Size(609, 480);
            this.mrPanel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(469, 238);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 40);
            this.button1.TabIndex = 3;
            this.button1.Text = "Convert";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // BtnConvert
            // 
            this.BtnConvert.Location = new System.Drawing.Point(7, 237);
            this.BtnConvert.Name = "BtnConvert";
            this.BtnConvert.Size = new System.Drawing.Size(136, 40);
            this.BtnConvert.TabIndex = 2;
            this.BtnConvert.Text = "Convert";
            this.BtnConvert.UseVisualStyleBackColor = true;
            this.BtnConvert.Click += new System.EventHandler(this.BtnConvert_Click);
            // 
            // TxtResult
            // 
            this.TxtResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtResult.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TxtResult.Location = new System.Drawing.Point(0, 280);
            this.TxtResult.Multiline = true;
            this.TxtResult.Name = "TxtResult";
            this.TxtResult.Size = new System.Drawing.Size(609, 200);
            this.TxtResult.TabIndex = 1;
            // 
            // TxtInputValue
            // 
            this.TxtInputValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtInputValue.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtInputValue.Location = new System.Drawing.Point(0, 0);
            this.TxtInputValue.Multiline = true;
            this.TxtInputValue.Name = "TxtInputValue";
            this.TxtInputValue.Size = new System.Drawing.Size(609, 235);
            this.TxtInputValue.TabIndex = 0;
            // 
            // FrmViewEncryptValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 480);
            this.Controls.Add(this.mrPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmViewEncryptValue";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "View Encrypt Value";
            this.Load += new System.EventHandler(this.FrmViewEncryptValue_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmViewEncryptValue_KeyPress);
            this.mrPanel1.ResumeLayout(false);
            this.mrPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MrDAL.Control.ControlsEx.Control.MrPanel mrPanel1;
        private MrDAL.Control.ControlsEx.Control.MrTextBox TxtInputValue;
        private MrDAL.Control.ControlsEx.Control.MrTextBox TxtResult;
        private System.Windows.Forms.Button BtnConvert;
        private System.Windows.Forms.Button button1;
    }
}