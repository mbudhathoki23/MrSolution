using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.SchoolTime.UserControl
{
    partial class uStudentReports
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
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.GrpFilterOption = new DevExpress.XtraEditors.GroupControl();
            this.clsSeparator1 = new ClsSeparator();
            this.btnFilterCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnFilterOk = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtToDate = new MrMaskedTextBox();
            this.txtFromDate = new MrMaskedTextBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.cmbTemplateList = new System.Windows.Forms.ComboBox();
            this.cmbDateType = new System.Windows.Forms.ComboBox();
            this.RGrid = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.pivotGridControl1 = new DevExpress.XtraPivotGrid.PivotGridControl();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrpFilterOption)).BeginInit();
            this.GrpFilterOption.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1097, 479);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.GrpFilterOption);
            this.xtraTabPage1.Controls.Add(this.RGrid);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1091, 451);
            this.xtraTabPage1.Text = "GridReports";
            // 
            // GrpFilterOption
            // 
            this.GrpFilterOption.AppearanceCaption.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrpFilterOption.AppearanceCaption.Options.UseFont = true;
            this.GrpFilterOption.Controls.Add(this.clsSeparator1);
            this.GrpFilterOption.Controls.Add(this.btnFilterCancel);
            this.GrpFilterOption.Controls.Add(this.btnFilterOk);
            this.GrpFilterOption.Controls.Add(this.labelControl2);
            this.GrpFilterOption.Controls.Add(this.labelControl3);
            this.GrpFilterOption.Controls.Add(this.txtToDate);
            this.GrpFilterOption.Controls.Add(this.txtFromDate);
            this.GrpFilterOption.Controls.Add(this.labelControl1);
            this.GrpFilterOption.Controls.Add(this.labelControl5);
            this.GrpFilterOption.Controls.Add(this.cmbTemplateList);
            this.GrpFilterOption.Controls.Add(this.cmbDateType);
            this.GrpFilterOption.Location = new System.Drawing.Point(308, 145);
            this.GrpFilterOption.Name = "GrpFilterOption";
            this.GrpFilterOption.Size = new System.Drawing.Size(384, 168);
            this.GrpFilterOption.TabIndex = 26;
            this.GrpFilterOption.Text = "Filter Option";
            this.GrpFilterOption.Visible = false;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.Lavender;
            this.clsSeparator1.Location = new System.Drawing.Point(0, 122);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(376, 3);
            this.clsSeparator1.TabIndex = 91;
            this.clsSeparator1.TabStop = false;
            // 
            // btnFilterCancel
            // 
            this.btnFilterCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.btnFilterCancel.Appearance.Options.UseFont = true;
            this.btnFilterCancel.Location = new System.Drawing.Point(215, 131);
            this.btnFilterCancel.Name = "btnFilterCancel";
            this.btnFilterCancel.Size = new System.Drawing.Size(103, 32);
            this.btnFilterCancel.TabIndex = 90;
            this.btnFilterCancel.Text = "&CANCEL";
            // 
            // btnFilterOk
            // 
            this.btnFilterOk.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilterOk.Appearance.Options.UseFont = true;
            this.btnFilterOk.Location = new System.Drawing.Point(130, 131);
            this.btnFilterOk.Name = "btnFilterOk";
            this.btnFilterOk.Size = new System.Drawing.Size(81, 32);
            this.btnFilterOk.TabIndex = 89;
            this.btnFilterOk.Text = "&SHOW";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(15, 92);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(85, 19);
            this.labelControl2.TabIndex = 88;
            this.labelControl2.Text = "From Date ";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(213, 94);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(58, 19);
            this.labelControl3.TabIndex = 87;
            this.labelControl3.Text = "To Date";
            // 
            // txtToDate
            // 
            this.txtToDate.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToDate.Location = new System.Drawing.Point(277, 90);
            this.txtToDate.Mask = "00/00/0000";
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.Size = new System.Drawing.Size(101, 25);
            this.txtToDate.TabIndex = 86;
            // 
            // txtFromDate
            // 
            this.txtFromDate.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromDate.Location = new System.Drawing.Point(101, 90);
            this.txtFromDate.Mask = "00/00/0000";
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.Size = new System.Drawing.Size(101, 25);
            this.txtFromDate.TabIndex = 85;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(15, 57);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(75, 19);
            this.labelControl1.TabIndex = 84;
            this.labelControl1.Text = "Date Type";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(15, 32);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(69, 19);
            this.labelControl5.TabIndex = 83;
            this.labelControl5.Text = "Template";
            // 
            // cmbTemplateList
            // 
            this.cmbTemplateList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTemplateList.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbTemplateList.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTemplateList.FormattingEnabled = true;
            this.cmbTemplateList.Location = new System.Drawing.Point(101, 28);
            this.cmbTemplateList.Name = "cmbTemplateList";
            this.cmbTemplateList.Size = new System.Drawing.Size(274, 27);
            this.cmbTemplateList.TabIndex = 82;
            // 
            // cmbDateType
            // 
            this.cmbDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDateType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbDateType.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDateType.FormattingEnabled = true;
            this.cmbDateType.Items.AddRange(new object[] {
            "Custom Date",
            "Today",
            "Yesterday",
            "Current Week",
            "Last Week",
            "Current Month",
            "Last Month",
            "Accounting Period"});
            this.cmbDateType.Location = new System.Drawing.Point(101, 59);
            this.cmbDateType.Name = "cmbDateType";
            this.cmbDateType.Size = new System.Drawing.Size(274, 27);
            this.cmbDateType.TabIndex = 81;
            // 
            // RGrid
            // 
            this.RGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RGrid.Location = new System.Drawing.Point(0, 0);
            this.RGrid.MainView = this.gridView1;
            this.RGrid.Name = "RGrid";
            this.RGrid.Size = new System.Drawing.Size(1091, 451);
            this.RGrid.TabIndex = 0;
            this.RGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.RGrid;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsLayout.StoreAllOptions = true;
            this.gridView1.OptionsPrint.ExpandAllDetails = true;
            this.gridView1.OptionsPrint.PrintDetails = true;
            this.gridView1.OptionsPrint.PrintPreview = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowFooter = true;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.pivotGridControl1);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1091, 410);
            this.xtraTabPage2.Text = "PivotReports";
            // 
            // pivotGridControl1
            // 
            this.pivotGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pivotGridControl1.Location = new System.Drawing.Point(0, 0);
            this.pivotGridControl1.Name = "pivotGridControl1";
            this.pivotGridControl1.Size = new System.Drawing.Size(1091, 410);
            this.pivotGridControl1.TabIndex = 0;
            // 
            // uReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "uReports";
            this.Size = new System.Drawing.Size(1097, 479);
            this.Load += new System.EventHandler(this.uReports_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.uReports_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GrpFilterOption)).EndInit();
            this.GrpFilterOption.ResumeLayout(false);
            this.GrpFilterOption.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraEditors.GroupControl GrpFilterOption;
        private ClsSeparator clsSeparator1;
        private DevExpress.XtraEditors.SimpleButton btnFilterCancel;
        private DevExpress.XtraEditors.SimpleButton btnFilterOk;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.MaskedTextBox txtToDate;
        private System.Windows.Forms.MaskedTextBox txtFromDate;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private System.Windows.Forms.ComboBox cmbTemplateList;
        private System.Windows.Forms.ComboBox cmbDateType;
        private DevExpress.XtraGrid.GridControl RGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraPivotGrid.PivotGridControl pivotGridControl1;
    }
}
