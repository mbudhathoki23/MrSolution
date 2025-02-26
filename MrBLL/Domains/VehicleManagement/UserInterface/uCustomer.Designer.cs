namespace MrBLL.Domains.VehicleManagement.UserInterface
{
    partial class uCustomer
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
            this.RGrid = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // RGrid
            // 
            this.RGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RGrid.Location = new System.Drawing.Point(0, 0);
            this.RGrid.MainView = this.gridView1;
            this.RGrid.Name = "RGrid";
            this.RGrid.Size = new System.Drawing.Size(1099, 501);
            this.RGrid.TabIndex = 1;
            this.RGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.RGrid;
            this.gridView1.Name = "gridView1";
            // 
            // uCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RGrid);
            this.Name = "uCustomer";
            this.Size = new System.Drawing.Size(1099, 501);
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl RGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}
