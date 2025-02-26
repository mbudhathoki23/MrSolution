using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Register_Report.Register_Report
{
    partial class FrmReceiptPaymentRegister
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
			this.RGrpDetails = new RoundPanel();
			this.roundPanel1 = new RoundPanel();
			this.CmbDateType = new System.Windows.Forms.ComboBox();
			this.MskFrom = new MrMaskedTextBox();
			this.MskToDate = new MrMaskedTextBox();
			this.roundPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// RGrpDetails
			// 
			this.RGrpDetails.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.RGrpDetails.Location = new System.Drawing.Point(3, 73);
			this.RGrpDetails.Name = "RGrpDetails";
			this.RGrpDetails.Radious = 25;
			this.RGrpDetails.Size = new System.Drawing.Size(515, 183);
			this.RGrpDetails.TabIndex = 0;
			this.RGrpDetails.TabStop = false;
			this.RGrpDetails.Text = "PAYMENT & RECEIPT REGISTER";
			this.RGrpDetails.TitleBackColor = System.Drawing.Color.DarkSlateBlue;
			this.RGrpDetails.TitleFont = new System.Drawing.Font("Bookman Old Style", 12F);
			this.RGrpDetails.TitleForeColor = System.Drawing.Color.White;
			this.RGrpDetails.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
			// 
			// roundPanel1
			// 
			this.roundPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.roundPanel1.Controls.Add(this.CmbDateType);
			this.roundPanel1.Controls.Add(this.MskFrom);
			this.roundPanel1.Controls.Add(this.MskToDate);
			this.roundPanel1.Location = new System.Drawing.Point(3, 3);
			this.roundPanel1.Name = "roundPanel1";
			this.roundPanel1.Radious = 25;
			this.roundPanel1.Size = new System.Drawing.Size(515, 64);
			this.roundPanel1.TabIndex = 1;
			this.roundPanel1.TabStop = false;
			this.roundPanel1.Text = "Date Type";
			this.roundPanel1.TitleBackColor = System.Drawing.Color.DarkSlateBlue;
			this.roundPanel1.TitleFont = new System.Drawing.Font("Bookman Old Style", 12F);
			this.roundPanel1.TitleForeColor = System.Drawing.Color.White;
			this.roundPanel1.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
			// 
			// CmbDateType
			// 
			this.CmbDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CmbDateType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.CmbDateType.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.CmbDateType.FormattingEnabled = true;
			this.CmbDateType.Location = new System.Drawing.Point(3, 30);
			this.CmbDateType.Name = "CmbDateType";
			this.CmbDateType.Size = new System.Drawing.Size(241, 27);
			this.CmbDateType.TabIndex = 0;
			// 
			// MskFrom
			// 
			this.MskFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.MskFrom.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MskFrom.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
			this.MskFrom.Location = new System.Drawing.Point(250, 30);
			this.MskFrom.Mask = "00/00/0000";
			this.MskFrom.Name = "MskFrom";
			this.MskFrom.Size = new System.Drawing.Size(120, 25);
			this.MskFrom.TabIndex = 1;
			// 
			// MskToDate
			// 
			this.MskToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.MskToDate.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MskToDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
			this.MskToDate.Location = new System.Drawing.Point(376, 30);
			this.MskToDate.Mask = "00/00/0000";
			this.MskToDate.Name = "MskToDate";
			this.MskToDate.Size = new System.Drawing.Size(120, 25);
			this.MskToDate.TabIndex = 2;
			// 
			// FrmReceiptPaymentRegister
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(562, 306);
			this.Controls.Add(this.roundPanel1);
			this.Controls.Add(this.RGrpDetails);
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "FrmReceiptPaymentRegister";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Receipt Payment Register";
			this.roundPanel1.ResumeLayout(false);
			this.roundPanel1.PerformLayout();
			this.ResumeLayout(false);

        }

		#endregion

		private RoundPanel RGrpDetails;
		private RoundPanel roundPanel1;
		private System.Windows.Forms.ComboBox CmbDateType;
		private System.Windows.Forms.MaskedTextBox MskFrom;
		private System.Windows.Forms.MaskedTextBox MskToDate;
	}
}