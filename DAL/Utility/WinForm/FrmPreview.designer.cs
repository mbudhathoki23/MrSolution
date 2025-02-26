using System.Windows.Forms;

namespace MrDAL.Utility.WinForm
{
	partial class FrmPreview
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
            this.picDocumentPreview = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnPrint = new System.Windows.Forms.Button();
            this.RdoZoom = new System.Windows.Forms.RadioButton();
            this.btnClose = new System.Windows.Forms.Button();
            this.RdoCenterImage = new System.Windows.Forms.RadioButton();
            this.RdoAutoSize = new System.Windows.Forms.RadioButton();
            this.RdoStretchImage = new System.Windows.Forms.RadioButton();
            this.RdoNormal = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.picDocumentPreview)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picDocumentPreview
            // 
            this.picDocumentPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picDocumentPreview.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.picDocumentPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picDocumentPreview.Location = new System.Drawing.Point(5, 5);
            this.picDocumentPreview.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picDocumentPreview.Name = "picDocumentPreview";
            this.picDocumentPreview.Size = new System.Drawing.Size(1201, 542);
            this.picDocumentPreview.TabIndex = 38;
            this.picDocumentPreview.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnPrint);
            this.groupBox1.Controls.Add(this.RdoZoom);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.RdoCenterImage);
            this.groupBox1.Controls.Add(this.RdoAutoSize);
            this.groupBox1.Controls.Add(this.RdoStretchImage);
            this.groupBox1.Controls.Add(this.RdoNormal);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 556);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox1.Size = new System.Drawing.Size(1212, 69);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pictue Box Size Mode";
            // 
            // BtnPrint
            // 
            this.BtnPrint.Location = new System.Drawing.Point(159, 16);
            this.BtnPrint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(133, 49);
            this.BtnPrint.TabIndex = 43;
            this.BtnPrint.Text = "&PRINT";
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // RdoZoom
            // 
            this.RdoZoom.AutoSize = true;
            this.RdoZoom.Location = new System.Drawing.Point(1049, 30);
            this.RdoZoom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RdoZoom.Name = "RdoZoom";
            this.RdoZoom.Size = new System.Drawing.Size(84, 27);
            this.RdoZoom.TabIndex = 4;
            this.RdoZoom.TabStop = true;
            this.RdoZoom.Text = "Zoom";
            this.RdoZoom.UseVisualStyleBackColor = true;
            this.RdoZoom.CheckedChanged += new System.EventHandler(this.rdZoom_CheckedChanged);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(8, 16);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(148, 49);
            this.btnClose.TabIndex = 42;
            this.btnClose.Text = "&CLOSE";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // RdoCenterImage
            // 
            this.RdoCenterImage.AutoSize = true;
            this.RdoCenterImage.Location = new System.Drawing.Point(847, 30);
            this.RdoCenterImage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RdoCenterImage.Name = "RdoCenterImage";
            this.RdoCenterImage.Size = new System.Drawing.Size(158, 27);
            this.RdoCenterImage.TabIndex = 3;
            this.RdoCenterImage.TabStop = true;
            this.RdoCenterImage.Text = "CenterImage";
            this.RdoCenterImage.UseVisualStyleBackColor = true;
            this.RdoCenterImage.CheckedChanged += new System.EventHandler(this.rdCenterImage_CheckedChanged);
            // 
            // RdoAutoSize
            // 
            this.RdoAutoSize.AutoSize = true;
            this.RdoAutoSize.Location = new System.Drawing.Point(685, 30);
            this.RdoAutoSize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RdoAutoSize.Name = "RdoAutoSize";
            this.RdoAutoSize.Size = new System.Drawing.Size(118, 27);
            this.RdoAutoSize.TabIndex = 2;
            this.RdoAutoSize.TabStop = true;
            this.RdoAutoSize.Text = "AutoSize";
            this.RdoAutoSize.UseVisualStyleBackColor = true;
            this.RdoAutoSize.CheckedChanged += new System.EventHandler(this.rdAutoSize_CheckedChanged);
            // 
            // RdoStretchImage
            // 
            this.RdoStretchImage.AutoSize = true;
            this.RdoStretchImage.Location = new System.Drawing.Point(477, 30);
            this.RdoStretchImage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RdoStretchImage.Name = "RdoStretchImage";
            this.RdoStretchImage.Size = new System.Drawing.Size(163, 27);
            this.RdoStretchImage.TabIndex = 1;
            this.RdoStretchImage.TabStop = true;
            this.RdoStretchImage.Text = "StretchImage";
            this.RdoStretchImage.UseVisualStyleBackColor = true;
            this.RdoStretchImage.CheckedChanged += new System.EventHandler(this.rdStretchImage_CheckedChanged);
            // 
            // RdoNormal
            // 
            this.RdoNormal.AutoSize = true;
            this.RdoNormal.Location = new System.Drawing.Point(333, 30);
            this.RdoNormal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RdoNormal.Name = "RdoNormal";
            this.RdoNormal.Size = new System.Drawing.Size(102, 27);
            this.RdoNormal.TabIndex = 0;
            this.RdoNormal.TabStop = true;
            this.RdoNormal.Text = "Normal";
            this.RdoNormal.UseVisualStyleBackColor = true;
            this.RdoNormal.CheckedChanged += new System.EventHandler(this.rdNormal_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.picDocumentPreview);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1212, 556);
            this.panel1.TabIndex = 41;
            // 
            // FrmPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1212, 625);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPreview";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preview Picture";
            this.Load += new System.EventHandler(this.FrmPreview_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPreview_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.picDocumentPreview)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox picDocumentPreview;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton RdoZoom;
		private System.Windows.Forms.RadioButton RdoCenterImage;
		private System.Windows.Forms.RadioButton RdoAutoSize;
		private System.Windows.Forms.RadioButton RdoStretchImage;
		private System.Windows.Forms.RadioButton RdoNormal;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button BtnPrint;
    }
}