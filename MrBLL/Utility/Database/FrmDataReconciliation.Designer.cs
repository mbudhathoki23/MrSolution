using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Utility.Database
{
    partial class FrmDataReconciliation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDataReconciliation));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rDetails = new System.Windows.Forms.RadioButton();
            this.rSummary = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnShow = new DevExpress.XtraEditors.SimpleButton();
            this.DGrid = new EntryGridViewEx();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rDetails);
            this.groupBox1.Controls.Add(this.rSummary);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(974, 47);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Report Type";
            // 
            // rDetails
            // 
            this.rDetails.AutoSize = true;
            this.rDetails.Location = new System.Drawing.Point(870, 18);
            this.rDetails.Name = "rDetails";
            this.rDetails.Size = new System.Drawing.Size(92, 23);
            this.rDetails.TabIndex = 1;
            this.rDetails.Text = "DETAILS";
            this.rDetails.UseVisualStyleBackColor = true;
            // 
            // rSummary
            // 
            this.rSummary.AutoSize = true;
            this.rSummary.Checked = true;
            this.rSummary.Location = new System.Drawing.Point(739, 18);
            this.rSummary.Name = "rSummary";
            this.rSummary.Size = new System.Drawing.Size(106, 23);
            this.rSummary.TabIndex = 0;
            this.rSummary.TabStop = true;
            this.rSummary.Text = "SUMMARY";
            this.rSummary.UseVisualStyleBackColor = true;
            this.rSummary.CheckedChanged += new System.EventHandler(this.RSummary_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.BtnCancel);
            this.groupBox2.Controls.Add(this.BtnShow);
            this.groupBox2.Controls.Add(this.DGrid);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.groupBox2.Location = new System.Drawing.Point(0, 47);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(974, 434);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(630, 392);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(99, 39);
            this.BtnCancel.TabIndex = 2;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnShow
            // 
            this.BtnShow.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.BtnShow.Appearance.Options.UseFont = true;
            this.BtnShow.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnShow.Location = new System.Drawing.Point(535, 392);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(93, 39);
            this.BtnShow.TabIndex = 1;
            this.BtnShow.Text = "&SHOW";
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // DGrid
            // 
            this.DGrid.AllowUserToAddRows = false;
            this.DGrid.AllowUserToDeleteRows = false;
            this.DGrid.AllowUserToResizeColumns = false;
            this.DGrid.AllowUserToResizeRows = false;
            this.DGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.DGrid.BlockNavigationOnNextRowOnEnter = true;
            this.DGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.DGrid.DoubleBufferEnabled = true;
            this.DGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.DGrid.Location = new System.Drawing.Point(3, 21);
            this.DGrid.MultiSelect = false;
            this.DGrid.Name = "DGrid";
            this.DGrid.ReadOnly = true;
            this.DGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGrid.Size = new System.Drawing.Size(968, 368);
            this.DGrid.StandardTab = true;
            this.DGrid.TabIndex = 0;
            this.DGrid.EnterKeyPressed += new System.EventHandler<System.EventArgs>(this.DGrid_EnterKeyPressed);
            this.DGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGrid_CellEnter);
            this.DGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGrid_RowEnter);
            this.DGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DGrid_KeyDown);
            // 
            // FrmDataReconciliation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(974, 481);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmDataReconciliation";
            this.ShowIcon = false;
            this.Text = "DATA RECONCILIATION";
            this.Load += new System.EventHandler(this.FrmDataReconciliation_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmDataReconciliation_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rDetails;
        private System.Windows.Forms.RadioButton rSummary;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnShow;
        private EntryGridViewEx DGrid;
    }
}