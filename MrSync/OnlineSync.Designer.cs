namespace MrSync
{
    partial class OnlineSync
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OnlineSync));
			this.panel1 = new System.Windows.Forms.Panel();
			this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			this.BtnPush = new DevExpress.XtraEditors.SimpleButton();
			this.BtnPull = new DevExpress.XtraEditors.SimpleButton();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.simpleButton3);
			this.panel1.Controls.Add(this.simpleButton2);
			this.panel1.Controls.Add(this.simpleButton1);
			this.panel1.Controls.Add(this.BtnPush);
			this.panel1.Controls.Add(this.BtnPull);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 228);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(641, 45);
			this.panel1.TabIndex = 0;
			// 
			// simpleButton3
			// 
			this.simpleButton3.Location = new System.Drawing.Point(216, 4);
			this.simpleButton3.Name = "simpleButton3";
			this.simpleButton3.Size = new System.Drawing.Size(64, 32);
			this.simpleButton3.TabIndex = 4;
			this.simpleButton3.Text = "Sync";
			// 
			// simpleButton2
			// 
			this.simpleButton2.Location = new System.Drawing.Point(148, 4);
			this.simpleButton2.Name = "simpleButton2";
			this.simpleButton2.Size = new System.Drawing.Size(64, 32);
			this.simpleButton2.TabIndex = 3;
			this.simpleButton2.Text = "Merge";
			// 
			// simpleButton1
			// 
			this.simpleButton1.Location = new System.Drawing.Point(565, 4);
			this.simpleButton1.Name = "simpleButton1";
			this.simpleButton1.Size = new System.Drawing.Size(64, 32);
			this.simpleButton1.TabIndex = 2;
			this.simpleButton1.Text = "Closed";
			// 
			// BtnPush
			// 
			this.BtnPush.Location = new System.Drawing.Point(80, 4);
			this.BtnPush.Name = "BtnPush";
			this.BtnPush.Size = new System.Drawing.Size(64, 32);
			this.BtnPush.TabIndex = 1;
			this.BtnPush.Text = "Push";
			// 
			// BtnPull
			// 
			this.BtnPull.Location = new System.Drawing.Point(12, 4);
			this.BtnPull.Name = "BtnPull";
			this.BtnPull.Size = new System.Drawing.Size(64, 32);
			this.BtnPull.TabIndex = 0;
			this.BtnPull.Text = "Pull";
			this.BtnPull.Click += new System.EventHandler(this.BtnPull_Click);
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "Info";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 205);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(30, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Time";
			// 
			// maskedTextBox1
			// 
			this.maskedTextBox1.Location = new System.Drawing.Point(44, 202);
			this.maskedTextBox1.Mask = "90:00";
			this.maskedTextBox1.Name = "maskedTextBox1";
			this.maskedTextBox1.Size = new System.Drawing.Size(100, 20);
			this.maskedTextBox1.TabIndex = 2;
			this.maskedTextBox1.ValidatingType = typeof(System.DateTime);
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(0, 169);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(641, 29);
			this.progressBar1.TabIndex = 3;
			// 
			// OnlineSync
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(641, 273);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.maskedTextBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "OnlineSync";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Online Sync";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton BtnPush;
        private DevExpress.XtraEditors.SimpleButton BtnPull;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

