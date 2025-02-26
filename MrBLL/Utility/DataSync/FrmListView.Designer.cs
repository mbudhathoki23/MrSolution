
namespace MrBLL.Utility.DataSync
{
    partial class FrmListView
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
            this.ReportView = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // ReportView
            // 
            this.ReportView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReportView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportView.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReportView.FullRowSelect = true;
            this.ReportView.GridLines = true;
            this.ReportView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.ReportView.HideSelection = false;
            this.ReportView.Location = new System.Drawing.Point(0, 0);
            this.ReportView.Name = "ReportView";
            this.ReportView.Size = new System.Drawing.Size(653, 509);
            this.ReportView.TabIndex = 0;
            this.ReportView.UseCompatibleStateImageBehavior = false;
            this.ReportView.View = System.Windows.Forms.View.Tile;
            // 
            // FrmListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 509);
            this.Controls.Add(this.ReportView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmListView";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ReportView;
    }
}