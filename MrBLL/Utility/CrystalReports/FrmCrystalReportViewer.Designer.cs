
namespace MrBLL.Utility.CrystalReports
{
    partial class FrmCrystalReportViewer
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
            this.PrintViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this._document = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            this.SuspendLayout();
            // 
            // PrintViewer
            // 
            this.PrintViewer.ActiveViewIndex = -1;
            this.PrintViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PrintViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.PrintViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrintViewer.Location = new System.Drawing.Point(0, 0);
            this.PrintViewer.Name = "PrintViewer";
            this.PrintViewer.Size = new System.Drawing.Size(1186, 831);
            this.PrintViewer.TabIndex = 0;
            this.PrintViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // FrmCrystalReportViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1186, 831);
            this.Controls.Add(this.PrintViewer);
            this.Name = "FrmCrystalReportViewer";
            this.Text = "Crystal Report Viewer";
            this.Load += new System.EventHandler(this.FrmCrystalReportViewer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer PrintViewer;
        private CrystalDecisions.CrystalReports.Engine.ReportDocument _document;
    }
}