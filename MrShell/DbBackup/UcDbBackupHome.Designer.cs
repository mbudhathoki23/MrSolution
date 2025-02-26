namespace MrShell.DbBackup
{
    partial class UcDbBackupHome
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnToggleBackupSync = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pbUpload = new System.Windows.Forms.ProgressBar();
            this.llConfigure = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // btnToggleBackupSync
            // 
            this.btnToggleBackupSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToggleBackupSync.Location = new System.Drawing.Point(620, 14);
            this.btnToggleBackupSync.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnToggleBackupSync.Name = "btnToggleBackupSync";
            this.btnToggleBackupSync.Size = new System.Drawing.Size(88, 25);
            this.btnToggleBackupSync.TabIndex = 0;
            this.btnToggleBackupSync.Text = "Stop";
            this.btnToggleBackupSync.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // pbUpload
            // 
            this.pbUpload.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbUpload.Location = new System.Drawing.Point(0, 327);
            this.pbUpload.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pbUpload.Name = "pbUpload";
            this.pbUpload.Size = new System.Drawing.Size(724, 25);
            this.pbUpload.TabIndex = 2;
            // 
            // llConfigure
            // 
            this.llConfigure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llConfigure.AutoSize = true;
            this.llConfigure.Location = new System.Drawing.Point(553, 25);
            this.llConfigure.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.llConfigure.Name = "llConfigure";
            this.llConfigure.Size = new System.Drawing.Size(59, 14);
            this.llConfigure.TabIndex = 3;
            this.llConfigure.TabStop = true;
            this.llConfigure.Text = "Configure";
            this.llConfigure.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llConfigure_LinkClicked);
            // 
            // UcDbBackupHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.llConfigure);
            this.Controls.Add(this.pbUpload);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnToggleBackupSync);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "UcDbBackupHome";
            this.Size = new System.Drawing.Size(724, 352);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnToggleBackupSync;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar pbUpload;
        private System.Windows.Forms.LinkLabel llConfigure;
    }
}
