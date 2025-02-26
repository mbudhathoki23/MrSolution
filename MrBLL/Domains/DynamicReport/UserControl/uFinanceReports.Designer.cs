namespace MrBLL.Domains.DynamicReport.UserControl
{
    partial class uFinanceReports
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
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.TabPivotReports = new DevExpress.XtraTab.XtraTabPage();
            this.GrpPivotReportTemplete = new DevExpress.XtraEditors.GroupControl();
            this.BtnPivodCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnPivotSave = new DevExpress.XtraEditors.SimpleButton();
            this.TxtPivotGridTemplete = new DevExpress.XtraEditors.TextEdit();
            this.PGrid = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.TabGridReport = new DevExpress.XtraTab.XtraTabPage();
            this.GrpGridReportTemplete = new DevExpress.XtraEditors.GroupControl();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnOk = new DevExpress.XtraEditors.SimpleButton();
            this.TxtGridReportTemplete = new DevExpress.XtraEditors.TextEdit();
            this.RGrid = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ReportTabControl = new DevExpress.XtraTab.XtraTabControl();
            this.TabPivotReports.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrpPivotReportTemplete)).BeginInit();
            this.GrpPivotReportTemplete.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtPivotGridTemplete.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PGrid)).BeginInit();
            this.TabGridReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrpGridReportTemplete)).BeginInit();
            this.GrpGridReportTemplete.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtGridReportTemplete.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportTabControl)).BeginInit();
            this.ReportTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabPivotReports
            // 
            this.TabPivotReports.Controls.Add(this.GrpPivotReportTemplete);
            this.TabPivotReports.Controls.Add(this.PGrid);
            this.TabPivotReports.Name = "TabPivotReports";
            this.TabPivotReports.Size = new System.Drawing.Size(1095, 454);
            this.TabPivotReports.Text = "PivotReports";
            // 
            // GrpPivotReportTemplete
            // 
            this.GrpPivotReportTemplete.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.GrpPivotReportTemplete.Controls.Add(this.BtnPivodCancel);
            this.GrpPivotReportTemplete.Controls.Add(this.BtnPivotSave);
            this.GrpPivotReportTemplete.Controls.Add(this.TxtPivotGridTemplete);
            this.GrpPivotReportTemplete.Location = new System.Drawing.Point(366, 179);
            this.GrpPivotReportTemplete.Name = "GrpPivotReportTemplete";
            this.GrpPivotReportTemplete.Size = new System.Drawing.Size(346, 96);
            this.GrpPivotReportTemplete.TabIndex = 25;
            this.GrpPivotReportTemplete.Text = "Template Name ";
            // 
            // BtnPivodCancel
            // 
            this.BtnPivodCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPivodCancel.Appearance.Options.UseFont = true;
            this.BtnPivodCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnPivodCancel.Location = new System.Drawing.Point(102, 63);
            this.BtnPivodCancel.Name = "BtnPivodCancel";
            this.BtnPivodCancel.Size = new System.Drawing.Size(102, 30);
            this.BtnPivodCancel.TabIndex = 2;
            this.BtnPivodCancel.Text = "&CANCEL";
            this.BtnPivodCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnPivotSave
            // 
            this.BtnPivotSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPivotSave.Appearance.Options.UseFont = true;
            this.BtnPivotSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnPivotSave.Location = new System.Drawing.Point(9, 63);
            this.BtnPivotSave.Name = "BtnPivotSave";
            this.BtnPivotSave.Size = new System.Drawing.Size(91, 30);
            this.BtnPivotSave.TabIndex = 1;
            this.BtnPivotSave.Text = "&SAVE";
            this.BtnPivotSave.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // TxtPivotGridTemplete
            // 
            this.TxtPivotGridTemplete.Location = new System.Drawing.Point(9, 34);
            this.TxtPivotGridTemplete.Name = "TxtPivotGridTemplete";
            this.TxtPivotGridTemplete.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPivotGridTemplete.Properties.Appearance.Options.UseFont = true;
            this.TxtPivotGridTemplete.Size = new System.Drawing.Size(330, 26);
            this.TxtPivotGridTemplete.TabIndex = 0;
            // 
            // PGrid
            // 
            this.PGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PGrid.Location = new System.Drawing.Point(0, 0);
            this.PGrid.Name = "PGrid";
            this.PGrid.OptionsPrint.UsePrintAppearance = true;
            this.PGrid.Size = new System.Drawing.Size(1095, 454);
            this.PGrid.TabIndex = 0;
            // 
            // TabGridReport
            // 
            this.TabGridReport.Controls.Add(this.GrpGridReportTemplete);
            this.TabGridReport.Controls.Add(this.RGrid);
            this.TabGridReport.Name = "TabGridReport";
            this.TabGridReport.Size = new System.Drawing.Size(1095, 454);
            this.TabGridReport.Text = "Grid Reports";
            // 
            // GrpGridReportTemplete
            // 
            this.GrpGridReportTemplete.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.GrpGridReportTemplete.Controls.Add(this.BtnCancel);
            this.GrpGridReportTemplete.Controls.Add(this.BtnOk);
            this.GrpGridReportTemplete.Controls.Add(this.TxtGridReportTemplete);
            this.GrpGridReportTemplete.Location = new System.Drawing.Point(400, 179);
            this.GrpGridReportTemplete.Name = "GrpGridReportTemplete";
            this.GrpGridReportTemplete.Size = new System.Drawing.Size(346, 96);
            this.GrpGridReportTemplete.TabIndex = 24;
            this.GrpGridReportTemplete.Text = "Template Name ";
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(102, 63);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(102, 30);
            this.BtnCancel.TabIndex = 2;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnOk
            // 
            this.BtnOk.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnOk.Appearance.Options.UseFont = true;
            this.BtnOk.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnOk.Location = new System.Drawing.Point(9, 63);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(91, 30);
            this.BtnOk.TabIndex = 1;
            this.BtnOk.Text = "&SAVE";
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // TxtGridReportTemplete
            // 
            this.TxtGridReportTemplete.Location = new System.Drawing.Point(9, 34);
            this.TxtGridReportTemplete.Name = "TxtGridReportTemplete";
            this.TxtGridReportTemplete.Properties.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtGridReportTemplete.Properties.Appearance.Options.UseFont = true;
            this.TxtGridReportTemplete.Size = new System.Drawing.Size(330, 26);
            this.TxtGridReportTemplete.TabIndex = 0;
            // 
            // RGrid
            // 
            this.RGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RGrid.Location = new System.Drawing.Point(0, 0);
            this.RGrid.MainView = this.gridView1;
            this.RGrid.Name = "RGrid";
            this.RGrid.Size = new System.Drawing.Size(1095, 454);
            this.RGrid.TabIndex = 0;
            this.RGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.RGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RGrid_KeyDown);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FocusedRow.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.FocusedRow.Options.UseFont = true;
            this.gridView1.Appearance.FooterPanel.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.gridView1.Appearance.FooterPanel.Options.UseFont = true;
            this.gridView1.Appearance.GroupFooter.Font = new System.Drawing.Font("Bookman Old Style", 9.75F);
            this.gridView1.Appearance.GroupFooter.Options.UseFont = true;
            this.gridView1.Appearance.GroupPanel.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.GroupPanel.Options.UseFont = true;
            this.gridView1.Appearance.GroupRow.Font = new System.Drawing.Font("Bookman Old Style", 9.75F);
            this.gridView1.Appearance.GroupRow.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Bookman Old Style", 9.75F);
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Appearance.TopNewRow.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Italic);
            this.gridView1.Appearance.TopNewRow.Options.UseFont = true;
            this.gridView1.GridControl = this.RGrid;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsLayout.StoreAllOptions = true;
            this.gridView1.OptionsLayout.StoreAppearance = true;
            this.gridView1.OptionsLayout.StoreFormatRules = true;
            this.gridView1.OptionsMenu.EnableGroupRowMenu = true;
            this.gridView1.OptionsMenu.ShowFooterItem = true;
            this.gridView1.OptionsMenu.ShowGroupSummaryEditorItem = true;
            this.gridView1.OptionsPrint.ExpandAllDetails = true;
            this.gridView1.OptionsPrint.PrintDetails = true;
            this.gridView1.OptionsPrint.PrintPreview = true;
            this.gridView1.OptionsView.AllowCellMerge = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowChildrenInGroupPanel = true;
            this.gridView1.OptionsView.ShowFooter = true;
            // 
            // ReportTabControl
            // 
            this.ReportTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportTabControl.Location = new System.Drawing.Point(0, 0);
            this.ReportTabControl.Name = "ReportTabControl";
            this.ReportTabControl.SelectedTabPage = this.TabGridReport;
            this.ReportTabControl.Size = new System.Drawing.Size(1097, 479);
            this.ReportTabControl.TabIndex = 1;
            this.ReportTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.TabGridReport,
            this.TabPivotReports});
            // 
            // uFinanceReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.Controls.Add(this.ReportTabControl);
            this.Name = "uFinanceReports";
            this.Size = new System.Drawing.Size(1097, 479);
            this.Load += new System.EventHandler(this.UReports_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UReports_KeyDown);
            this.TabPivotReports.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GrpPivotReportTemplete)).EndInit();
            this.GrpPivotReportTemplete.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TxtPivotGridTemplete.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PGrid)).EndInit();
            this.TabGridReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GrpGridReportTemplete)).EndInit();
            this.GrpGridReportTemplete.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TxtGridReportTemplete.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportTabControl)).EndInit();
            this.ReportTabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private DevExpress.XtraTab.XtraTabPage TabPivotReports;
        private DevExpress.XtraEditors.GroupControl GrpPivotReportTemplete;
        private DevExpress.XtraEditors.SimpleButton BtnPivodCancel;
        private DevExpress.XtraEditors.SimpleButton BtnPivotSave;
        private DevExpress.XtraEditors.TextEdit TxtPivotGridTemplete;
        private DevExpress.XtraTab.XtraTabPage TabGridReport;
        private DevExpress.XtraEditors.GroupControl GrpGridReportTemplete;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnOk;
        public DevExpress.XtraEditors.TextEdit TxtGridReportTemplete;
        public DevExpress.XtraGrid.GridControl RGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        public DevExpress.XtraTab.XtraTabControl ReportTabControl;
        public DevExpress.XtraPivotGrid.PivotGridControl PGrid;
    }
}
