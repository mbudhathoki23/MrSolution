using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Setup.CompanySetup
{
    partial class FrmFiscalYear
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFiscalYear));
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.PanelHeader = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.BtnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.RGrid = new System.Windows.Forms.DataGridView();
            this.GTxtFiscalYearId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtFiscalYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GMskStartDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GMskEndDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PanelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnCancel.ImageOptions.Image")));
            this.BtnCancel.Location = new System.Drawing.Point(502, 191);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(110, 34);
            this.BtnCancel.TabIndex = 2;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.clsSeparator1);
            this.PanelHeader.Controls.Add(this.BtnCancel);
            this.PanelHeader.Controls.Add(this.BtnLogin);
            this.PanelHeader.Controls.Add(this.RGrid);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(615, 227);
            this.PanelHeader.TabIndex = 40;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(3, 186);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(609, 2);
            this.clsSeparator1.TabIndex = 11;
            this.clsSeparator1.TabStop = false;
            // 
            // BtnLogin
            // 
            this.BtnLogin.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnLogin.Appearance.Options.UseFont = true;
            this.BtnLogin.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnLogin.ImageOptions.Image")));
            this.BtnLogin.Location = new System.Drawing.Point(393, 191);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(107, 34);
            this.BtnLogin.TabIndex = 1;
            this.BtnLogin.Text = "&LOGIN";
            this.BtnLogin.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // RGrid
            // 
            this.RGrid.AllowUserToAddRows = false;
            this.RGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.RGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GTxtFiscalYearId,
            this.GTxtFiscalYear,
            this.GMskStartDate,
            this.GMskEndDate});
            this.RGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.RGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.RGrid.Location = new System.Drawing.Point(0, 0);
            this.RGrid.Margin = new System.Windows.Forms.Padding(2);
            this.RGrid.MultiSelect = false;
            this.RGrid.Name = "RGrid";
            this.RGrid.ReadOnly = true;
            this.RGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.RGrid.RowHeadersWidth = 10;
            this.RGrid.RowTemplate.Height = 24;
            this.RGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RGrid.Size = new System.Drawing.Size(615, 184);
            this.RGrid.TabIndex = 0;
            this.RGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_CellContentClick);
            this.RGrid.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_CellContentDoubleClick);
            this.RGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_CellEnter);
            this.RGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_RowEnter);
            this.RGrid.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.RGrid_RowHeaderMouseDoubleClick);
            this.RGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RGrid_KeyDown);
            // 
            // GTxtFiscalYearId
            // 
            this.GTxtFiscalYearId.HeaderText = "FiscalYearId";
            this.GTxtFiscalYearId.Name = "GTxtFiscalYearId";
            this.GTxtFiscalYearId.ReadOnly = true;
            this.GTxtFiscalYearId.Visible = false;
            // 
            // GTxtFiscalYear
            // 
            this.GTxtFiscalYear.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.GTxtFiscalYear.FillWeight = 130F;
            this.GTxtFiscalYear.HeaderText = "Fiscal Year";
            this.GTxtFiscalYear.Name = "GTxtFiscalYear";
            this.GTxtFiscalYear.ReadOnly = true;
            // 
            // GMskStartDate
            // 
            this.GMskStartDate.HeaderText = "Start Date";
            this.GMskStartDate.Name = "GMskStartDate";
            this.GMskStartDate.ReadOnly = true;
            this.GMskStartDate.Width = 150;
            // 
            // GMskEndDate
            // 
            this.GMskEndDate.HeaderText = "End Date";
            this.GMskEndDate.Name = "GMskEndDate";
            this.GMskEndDate.ReadOnly = true;
            this.GMskEndDate.Width = 150;
            // 
            // FrmFiscalYear
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(615, 227);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFiscalYear";
            this.ShowIcon = false;
            this.Text = "Fiscal Year List";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmFiscalYear_FormClosing);
            this.Load += new System.EventHandler(this.FrmFiscalYear_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmFiscalYear_KeyPress);
            this.PanelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton BtnLogin;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private ClsSeparator clsSeparator1;
        private System.Windows.Forms.DataGridView RGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtFiscalYearId;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtFiscalYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn GMskStartDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn GMskEndDate;
        private MrPanel PanelHeader;
    }
}