using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.DataEntry.Common
{
    partial class FrmProductBatchList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mrPanel1 = new MrPanel();
            this.DGrid = new EntryGridViewEx();
            this.mrGroup2 = new MrGroup();
            this.LblTotalQty = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mrGroup1 = new MrGroup();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.GTxtSNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtProductId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtBatchNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtMFDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtExpDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtSalesRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtMrp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtProductSno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mrPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).BeginInit();
            this.mrGroup2.SuspendLayout();
            this.mrGroup1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mrPanel1
            // 
            this.mrPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel1.Controls.Add(this.DGrid);
            this.mrPanel1.Controls.Add(this.mrGroup2);
            this.mrPanel1.Controls.Add(this.mrGroup1);
            this.mrPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrPanel1.Location = new System.Drawing.Point(0, 0);
            this.mrPanel1.Name = "mrPanel1";
            this.mrPanel1.Size = new System.Drawing.Size(871, 272);
            this.mrPanel1.TabIndex = 0;
            // 
            // DGrid
            // 
            this.DGrid.AllowUserToAddRows = false;
            this.DGrid.AllowUserToDeleteRows = false;
            this.DGrid.AllowUserToResizeColumns = false;
            this.DGrid.AllowUserToResizeRows = false;
            this.DGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.DGrid.BlockNavigationOnNextRowOnEnter = true;
            this.DGrid.ColumnHeadersHeight = 27;
            this.DGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GTxtSNo,
            this.GTxtProductId,
            this.GTxtBatchNo,
            this.GTxtMFDate,
            this.GTxtExpDate,
            this.GTxtQty,
            this.GTxtSalesRate,
            this.GTxtMrp,
            this.GTxtProductSno});
            this.DGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.DGrid.DoubleBufferEnabled = true;
            this.DGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.DGrid.Location = new System.Drawing.Point(0, 0);
            this.DGrid.MultiSelect = false;
            this.DGrid.Name = "DGrid";
            this.DGrid.ReadOnly = true;
            this.DGrid.RowHeadersWidth = 20;
            this.DGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGrid.Size = new System.Drawing.Size(871, 192);
            this.DGrid.StandardTab = true;
            this.DGrid.TabIndex = 0;
            // 
            // mrGroup2
            // 
            this.mrGroup2.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup2.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup2.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup2.BorderColor = System.Drawing.Color.White;
            this.mrGroup2.BorderThickness = 1F;
            this.mrGroup2.Controls.Add(this.LblTotalQty);
            this.mrGroup2.Controls.Add(this.label1);
            this.mrGroup2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup2.GroupImage = null;
            this.mrGroup2.GroupTitle = "";
            this.mrGroup2.Location = new System.Drawing.Point(0, 183);
            this.mrGroup2.Name = "mrGroup2";
            this.mrGroup2.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup2.PaintGroupBox = false;
            this.mrGroup2.RoundCorners = 3;
            this.mrGroup2.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup2.ShadowControl = false;
            this.mrGroup2.ShadowThickness = 3;
            this.mrGroup2.Size = new System.Drawing.Size(868, 43);
            this.mrGroup2.TabIndex = 7;
            // 
            // LblTotalQty
            // 
            this.LblTotalQty.BackColor = System.Drawing.Color.White;
            this.LblTotalQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTotalQty.Location = new System.Drawing.Point(509, 12);
            this.LblTotalQty.Name = "LblTotalQty";
            this.LblTotalQty.Size = new System.Drawing.Size(135, 27);
            this.LblTotalQty.TabIndex = 5;
            this.LblTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(416, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "TotalQty:";
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.BtnSave);
            this.mrGroup1.Controls.Add(this.BtnCancel);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "";
            this.mrGroup1.Location = new System.Drawing.Point(2, 218);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 4;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = false;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(866, 51);
            this.mrGroup1.TabIndex = 6;
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnSave.Location = new System.Drawing.Point(648, 14);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(100, 34);
            this.BtnSave.TabIndex = 2;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(751, 14);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(109, 34);
            this.BtnCancel.TabIndex = 3;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // GTxtSNo
            // 
            this.GTxtSNo.HeaderText = "SNo";
            this.GTxtSNo.Name = "GTxtSNo";
            this.GTxtSNo.ReadOnly = true;
            this.GTxtSNo.Width = 50;
            // 
            // GTxtProductId
            // 
            this.GTxtProductId.HeaderText = "ProductId";
            this.GTxtProductId.Name = "GTxtProductId";
            this.GTxtProductId.ReadOnly = true;
            this.GTxtProductId.Visible = false;
            // 
            // GTxtBatchNo
            // 
            this.GTxtBatchNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.GTxtBatchNo.HeaderText = "Batch No";
            this.GTxtBatchNo.Name = "GTxtBatchNo";
            this.GTxtBatchNo.ReadOnly = true;
            // 
            // GTxtMFDate
            // 
            this.GTxtMFDate.HeaderText = "MF Date";
            this.GTxtMFDate.Name = "GTxtMFDate";
            this.GTxtMFDate.ReadOnly = true;
            this.GTxtMFDate.Width = 90;
            // 
            // GTxtExpDate
            // 
            this.GTxtExpDate.HeaderText = "Exp. Date";
            this.GTxtExpDate.Name = "GTxtExpDate";
            this.GTxtExpDate.ReadOnly = true;
            this.GTxtExpDate.Width = 90;
            // 
            // GTxtQty
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GTxtQty.DefaultCellStyle = dataGridViewCellStyle1;
            this.GTxtQty.HeaderText = "Qty";
            this.GTxtQty.Name = "GTxtQty";
            this.GTxtQty.ReadOnly = true;
            this.GTxtQty.Width = 90;
            // 
            // GTxtSalesRate
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GTxtSalesRate.DefaultCellStyle = dataGridViewCellStyle2;
            this.GTxtSalesRate.HeaderText = "Sales Rate";
            this.GTxtSalesRate.Name = "GTxtSalesRate";
            this.GTxtSalesRate.ReadOnly = true;
            this.GTxtSalesRate.Width = 150;
            // 
            // GTxtMrp
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GTxtMrp.DefaultCellStyle = dataGridViewCellStyle3;
            this.GTxtMrp.HeaderText = "MRP";
            this.GTxtMrp.Name = "GTxtMrp";
            this.GTxtMrp.ReadOnly = true;
            this.GTxtMrp.Width = 120;
            // 
            // GTxtProductSno
            // 
            this.GTxtProductSno.HeaderText = "ProductSno";
            this.GTxtProductSno.Name = "GTxtProductSno";
            this.GTxtProductSno.ReadOnly = true;
            this.GTxtProductSno.Visible = false;
            this.GTxtProductSno.Width = 5;
            // 
            // FrmProductBatchList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(871, 272);
            this.Controls.Add(this.mrPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmProductBatchList";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BATCH SETUP";
            this.Load += new System.EventHandler(this.FrmProductBatchList_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmProductBatchList_KeyPress);
            this.mrPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).EndInit();
            this.mrGroup2.ResumeLayout(false);
            this.mrGroup2.PerformLayout();
            this.mrGroup1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private MrPanel mrPanel1;
        private EntryGridViewEx DGrid;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private System.Windows.Forms.Label label1;
        private MrGroup mrGroup1;
        private MrGroup mrGroup2;
        public System.Windows.Forms.Label LblTotalQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtSNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtProductId;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtBatchNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtMFDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtExpDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtSalesRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtMrp;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtProductSno;
    }
}