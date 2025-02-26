namespace MrBLL.Domains.Restro.Master
{
    partial class FrmConfirmationDiscount
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
            this.ChkServicesIncluded = new System.Windows.Forms.CheckBox();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.TxtAmount = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtPercent = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.LblBasicAmount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mrPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mrPanel1
            // 
            this.mrPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel1.Controls.Add(this.ChkServicesIncluded);
            this.mrPanel1.Controls.Add(this.BtnCancel);
            this.mrPanel1.Controls.Add(this.BtnSave);
            this.mrPanel1.Controls.Add(this.clsSeparator1);
            this.mrPanel1.Controls.Add(this.TxtAmount);
            this.mrPanel1.Controls.Add(this.TxtPercent);
            this.mrPanel1.Controls.Add(this.LblBasicAmount);
            this.mrPanel1.Controls.Add(this.label4);
            this.mrPanel1.Controls.Add(this.label3);
            this.mrPanel1.Controls.Add(this.label1);
            this.mrPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrPanel1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrPanel1.Location = new System.Drawing.Point(0, 0);
            this.mrPanel1.Name = "mrPanel1";
            this.mrPanel1.Size = new System.Drawing.Size(355, 181);
            this.mrPanel1.TabIndex = 0;
            // 
            // ChkServicesIncluded
            // 
            this.ChkServicesIncluded.AutoSize = true;
            this.ChkServicesIncluded.Location = new System.Drawing.Point(9, 101);
            this.ChkServicesIncluded.Name = "ChkServicesIncluded";
            this.ChkServicesIncluded.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkServicesIncluded.Size = new System.Drawing.Size(171, 24);
            this.ChkServicesIncluded.TabIndex = 12;
            this.ChkServicesIncluded.Text = "Service Applicable";
            this.ChkServicesIncluded.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkServicesIncluded.UseVisualStyleBackColor = true;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.BtnCancel.Location = new System.Drawing.Point(229, 140);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(104, 36);
            this.BtnCancel.TabIndex = 11;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(139, 140);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(89, 36);
            this.BtnSave.TabIndex = 10;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(4, 133);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(334, 2);
            this.clsSeparator1.TabIndex = 3;
            this.clsSeparator1.TabStop = false;
            // 
            // TxtAmount
            // 
            this.TxtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAmount.Location = new System.Drawing.Point(139, 70);
            this.TxtAmount.Name = "TxtAmount";
            this.TxtAmount.Size = new System.Drawing.Size(200, 26);
            this.TxtAmount.TabIndex = 2;
            this.TxtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtAmount.TextBoxType = MrDAL.Control.ControlsEx.Control.TextBoxType.Decimal;
            this.TxtAmount.Validating += new System.ComponentModel.CancelEventHandler(this.TxtAmount_Validating);
            // 
            // TxtPercent
            // 
            this.TxtPercent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPercent.Location = new System.Drawing.Point(139, 41);
            this.TxtPercent.Name = "TxtPercent";
            this.TxtPercent.Size = new System.Drawing.Size(200, 26);
            this.TxtPercent.TabIndex = 1;
            this.TxtPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtPercent.TextBoxType = MrDAL.Control.ControlsEx.Control.TextBoxType.Decimal;
            this.TxtPercent.TextChanged += new System.EventHandler(this.TxtPercent_TextChanged);
            // 
            // LblBasicAmount
            // 
            this.LblBasicAmount.BackColor = System.Drawing.Color.White;
            this.LblBasicAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblBasicAmount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblBasicAmount.Location = new System.Drawing.Point(139, 12);
            this.LblBasicAmount.Name = "LblBasicAmount";
            this.LblBasicAmount.Size = new System.Drawing.Size(200, 26);
            this.LblBasicAmount.TabIndex = 0;
            this.LblBasicAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Amount";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Discount";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Basic Amount";
            // 
            // FrmConfirmationDiscount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 181);
            this.Controls.Add(this.mrPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConfirmationDiscount";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CONFIRMATION DISCOUNT";
            this.Load += new System.EventHandler(this.FrmConfirmationDiscount_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmConfirmationDiscount_KeyPress);
            this.mrPanel1.ResumeLayout(false);
            this.mrPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MrDAL.Control.ControlsEx.Control.MrPanel mrPanel1;
        private System.Windows.Forms.Label LblBasicAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private MrDAL.Control.ControlsEx.Control.MrTextBox TxtPercent;
        private MrDAL.Control.ControlsEx.Control.ClsSeparator clsSeparator1;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        public MrDAL.Control.ControlsEx.Control.MrTextBox TxtAmount;
        public System.Windows.Forms.CheckBox ChkServicesIncluded;
    }
}