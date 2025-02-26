using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.DynamicReport
{
    partial class FrmFilter
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnOk = new System.Windows.Forms.Button();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.CmbDateOption = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.MskToDate = new MrMaskedTextBox();
            this.LblFromDate = new System.Windows.Forms.Label();
            this.MskFrom = new MrMaskedTextBox();
            this.LblToDate = new System.Windows.Forms.Label();
            this.Grid = new System.Windows.Forms.DataGridView();
            this.SNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TemplateId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TemplateName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.BtnCancel);
            this.panelControl1.Controls.Add(this.BtnOk);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 291);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(560, 44);
            this.panelControl1.TabIndex = 2;
            // 
            // BtnCancel
            // 
            this.BtnCancel.CausesValidation = false;
            this.BtnCancel.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.BtnCancel.Image = global::MrBLL.Properties.Resources.Cancel24;
            this.BtnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnCancel.Location = new System.Drawing.Point(454, 5);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(102, 35);
            this.BtnCancel.TabIndex = 2;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnOk
            // 
            this.BtnOk.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.BtnOk.Image = global::MrBLL.Properties.Resources.ShowReport;
            this.BtnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnOk.Location = new System.Drawing.Point(365, 5);
            this.BtnOk.Margin = new System.Windows.Forms.Padding(4);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(88, 35);
            this.BtnOk.TabIndex = 1;
            this.BtnOk.Text = "&SHOW";
            this.BtnOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.CmbDateOption);
            this.panelControl2.Controls.Add(this.label7);
            this.panelControl2.Controls.Add(this.MskToDate);
            this.panelControl2.Controls.Add(this.LblFromDate);
            this.panelControl2.Controls.Add(this.MskFrom);
            this.panelControl2.Controls.Add(this.LblToDate);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(4);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(560, 71);
            this.panelControl2.TabIndex = 0;
            // 
            // CmbDateOption
            // 
            this.CmbDateOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbDateOption.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbDateOption.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.CmbDateOption.FormattingEnabled = true;
            this.CmbDateOption.Location = new System.Drawing.Point(100, 6);
            this.CmbDateOption.Margin = new System.Windows.Forms.Padding(4);
            this.CmbDateOption.Name = "CmbDateOption";
            this.CmbDateOption.Size = new System.Drawing.Size(454, 27);
            this.CmbDateOption.TabIndex = 1;
            this.CmbDateOption.SelectionChangeCommitted += new System.EventHandler(this.CmbDateOption_SelectionChangeCommitted);
            this.CmbDateOption.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbDateOption_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.label7.Location = new System.Drawing.Point(10, 10);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 19);
            this.label7.TabIndex = 0;
            this.label7.Text = "Date Type";
            // 
            // MskToDate
            // 
            this.MskToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskToDate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.MskToDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskToDate.Location = new System.Drawing.Point(296, 39);
            this.MskToDate.Margin = new System.Windows.Forms.Padding(4);
            this.MskToDate.Mask = "99/99/9999";
            this.MskToDate.Name = "MskToDate";
            this.MskToDate.Size = new System.Drawing.Size(118, 25);
            this.MskToDate.TabIndex = 5;
            this.MskToDate.Validating += new System.ComponentModel.CancelEventHandler(this.TxtToDate_Validating);
            // 
            // LblFromDate
            // 
            this.LblFromDate.AutoSize = true;
            this.LblFromDate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.LblFromDate.Location = new System.Drawing.Point(229, 42);
            this.LblFromDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblFromDate.Name = "LblFromDate";
            this.LblFromDate.Size = new System.Drawing.Size(67, 19);
            this.LblFromDate.TabIndex = 4;
            this.LblFromDate.Text = "To Date";
            // 
            // MskFrom
            // 
            this.MskFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskFrom.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.MskFrom.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskFrom.Location = new System.Drawing.Point(100, 39);
            this.MskFrom.Margin = new System.Windows.Forms.Padding(4);
            this.MskFrom.Mask = "99/99/9999";
            this.MskFrom.Name = "MskFrom";
            this.MskFrom.Size = new System.Drawing.Size(118, 25);
            this.MskFrom.TabIndex = 3;
            this.MskFrom.Validating += new System.ComponentModel.CancelEventHandler(this.TxtFromDate_Validating);
            // 
            // LblToDate
            // 
            this.LblToDate.AutoSize = true;
            this.LblToDate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.LblToDate.Location = new System.Drawing.Point(11, 42);
            this.LblToDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblToDate.Name = "LblToDate";
            this.LblToDate.Size = new System.Drawing.Size(89, 19);
            this.LblToDate.TabIndex = 2;
            this.LblToDate.Text = "Date From";
            // 
            // Grid
            // 
            this.Grid.AllowUserToAddRows = false;
            this.Grid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Grid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.Grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SNo,
            this.TemplateId,
            this.TemplateName});
            this.Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.Grid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.Grid.Location = new System.Drawing.Point(0, 71);
            this.Grid.Margin = new System.Windows.Forms.Padding(4);
            this.Grid.MultiSelect = false;
            this.Grid.Name = "Grid";
            this.Grid.RowHeadersVisible = false;
            this.Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Grid.Size = new System.Drawing.Size(560, 220);
            this.Grid.StandardTab = true;
            this.Grid.TabIndex = 25;
            this.Grid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Grid_KeyDown);
            // 
            // SNo
            // 
            this.SNo.FillWeight = 228.4264F;
            this.SNo.Frozen = true;
            this.SNo.HeaderText = "SNo";
            this.SNo.Name = "SNo";
            this.SNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.SNo.ToolTipText = "Delete Row";
            this.SNo.Width = 50;
            // 
            // TemplateId
            // 
            this.TemplateId.HeaderText = "TemplateId";
            this.TemplateId.Name = "TemplateId";
            this.TemplateId.Visible = false;
            // 
            // TemplateName
            // 
            this.TemplateName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.TemplateName.DefaultCellStyle = dataGridViewCellStyle2;
            this.TemplateName.FillWeight = 83.94669F;
            this.TemplateName.HeaderText = "Template";
            this.TemplateName.Name = "TemplateName";
            this.TemplateName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FrmFilter
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 335);
            this.Controls.Add(this.Grid);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IconOptions.ShowIcon = false;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFilter";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "REPORT FILTER";
            this.Load += new System.EventHandler(this.FrmFilter_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmFilter_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        protected internal System.Windows.Forms.ComboBox CmbDateOption;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label LblFromDate;
        private System.Windows.Forms.Label LblToDate;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.DataGridView Grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn SNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn TemplateId;
        private System.Windows.Forms.DataGridViewTextBoxColumn TemplateName;
        public MrMaskedTextBox MskFrom;
        public MrMaskedTextBox MskToDate;
    }
}