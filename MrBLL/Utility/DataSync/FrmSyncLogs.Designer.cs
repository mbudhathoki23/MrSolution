namespace MrBLL.Utility.DataSync
{
    partial class FrmSyncLogs
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
            this.memo = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.memo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // memo
            // 
            this.memo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memo.Location = new System.Drawing.Point(0, 0);
            this.memo.Name = "memo";
            this.memo.Properties.ReadOnly = true;
            this.memo.Size = new System.Drawing.Size(815, 571);
            this.memo.TabIndex = 0;
            // 
            // FrmSyncLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 571);
            this.ControlBox = false;
            this.Controls.Add(this.memo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSyncLogs";
            this.ShowIcon = false;
            this.Text = "Data sync logs";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSyncLogs_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.memo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit memo;
    }
}