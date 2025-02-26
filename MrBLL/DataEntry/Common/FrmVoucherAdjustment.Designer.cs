using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.DataEntry.Common
{
    partial class FrmVoucherAdjustment
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
            this.DGrid = new EntryGridViewEx();
            this.GTxtSNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtAdjustType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.GTxtDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtAdjustment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // DGrid
            // 
            this.DGrid.AllowUserToAddRows = false;
            this.DGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GTxtSNo,
            this.GTxtAdjustType,
            this.GTxtDescription,
            this.GTxtAmount,
            this.GTxtAdjustment});
            this.DGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGrid.Location = new System.Drawing.Point(0, 0);
            this.DGrid.MultiSelect = false;
            this.DGrid.Name = "DGrid";
            this.DGrid.ReadOnly = true;
            this.DGrid.RowHeadersWidth = 25;
            this.DGrid.Size = new System.Drawing.Size(667, 195);
            this.DGrid.TabIndex = 0;
            // 
            // GTxtSNo
            // 
            this.GTxtSNo.HeaderText = "SNO";
            this.GTxtSNo.Name = "GTxtSNo";
            this.GTxtSNo.ReadOnly = true;
            this.GTxtSNo.Width = 65;
            // 
            // GTxtAdjustType
            // 
            this.GTxtAdjustType.HeaderText = "ADJUST_TYPE";
            this.GTxtAdjustType.Items.AddRange(new object[] {
            "Normal",
            "Settle"});
            this.GTxtAdjustType.Name = "GTxtAdjustType";
            this.GTxtAdjustType.ReadOnly = true;
            this.GTxtAdjustType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.GTxtAdjustType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.GTxtAdjustType.Width = 150;
            // 
            // GTxtDescription
            // 
            this.GTxtDescription.HeaderText = "VOUCHER_NO";
            this.GTxtDescription.Name = "GTxtDescription";
            this.GTxtDescription.ReadOnly = true;
            this.GTxtDescription.Width = 150;
            // 
            // GTxtAmount
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GTxtAmount.DefaultCellStyle = dataGridViewCellStyle2;
            this.GTxtAmount.HeaderText = "AMOUNT";
            this.GTxtAmount.Name = "GTxtAmount";
            this.GTxtAmount.ReadOnly = true;
            // 
            // GTxtAdjustment
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GTxtAdjustment.DefaultCellStyle = dataGridViewCellStyle3;
            this.GTxtAdjustment.HeaderText = "ADJ_AMOUNT";
            this.GTxtAdjustment.Name = "GTxtAdjustment";
            this.GTxtAdjustment.ReadOnly = true;
            this.GTxtAdjustment.Width = 150;
            // 
            // FrmVoucherAdjustment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 195);
            this.Controls.Add(this.DGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmVoucherAdjustment";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VOUCHER AMOUNT ADJUSTMENT";
            this.Load += new System.EventHandler(this.FrmVoucherAdjustment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private EntryGridViewEx DGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtSNo;
        private System.Windows.Forms.DataGridViewComboBoxColumn GTxtAdjustType;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtAdjustment;
    }
}