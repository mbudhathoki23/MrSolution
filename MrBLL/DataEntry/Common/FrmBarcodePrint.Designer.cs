using MrBLL.Properties;
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.DataEntry.Common
{
    partial class FrmBarcodePrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBarcodePrint));
            this.panel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.panel3 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.DGrid = new System.Windows.Forms.DataGridView();
            this.dgv_Sno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_ProductId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SalesRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.TxtProduct = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.TxtSalesRate = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnProduct = new System.Windows.Forms.Button();
            this.txtQty = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(780, 487);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel3.Controls.Add(this.DGrid);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(780, 448);
            this.panel3.TabIndex = 2;
            // 
            // DGrid
            // 
            this.DGrid.AllowUserToAddRows = false;
            this.DGrid.AllowUserToResizeColumns = false;
            this.DGrid.AllowUserToResizeRows = false;
            this.DGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.DGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_Sno,
            this.dgv_ProductId,
            this.dgv_Code,
            this.dgv_Description,
            this.dgv_Qty,
            this.SalesRate});
            this.DGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.DGrid.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.DGrid.Location = new System.Drawing.Point(0, 41);
            this.DGrid.Name = "DGrid";
            this.DGrid.ReadOnly = true;
            this.DGrid.RowHeadersVisible = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DGrid.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGrid.Size = new System.Drawing.Size(780, 407);
            this.DGrid.TabIndex = 0;
            // 
            // dgv_Sno
            // 
            this.dgv_Sno.HeaderText = "Sno";
            this.dgv_Sno.Name = "dgv_Sno";
            this.dgv_Sno.ReadOnly = true;
            this.dgv_Sno.Width = 45;
            // 
            // dgv_ProductId
            // 
            this.dgv_ProductId.HeaderText = "ProductId";
            this.dgv_ProductId.Name = "dgv_ProductId";
            this.dgv_ProductId.ReadOnly = true;
            this.dgv_ProductId.Visible = false;
            this.dgv_ProductId.Width = 150;
            // 
            // dgv_Code
            // 
            this.dgv_Code.HeaderText = "Code";
            this.dgv_Code.Name = "dgv_Code";
            this.dgv_Code.ReadOnly = true;
            this.dgv_Code.Visible = false;
            this.dgv_Code.Width = 120;
            // 
            // dgv_Description
            // 
            this.dgv_Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgv_Description.HeaderText = "Description";
            this.dgv_Description.Name = "dgv_Description";
            this.dgv_Description.ReadOnly = true;
            // 
            // dgv_Qty
            // 
            this.dgv_Qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv_Qty.HeaderText = "Qty";
            this.dgv_Qty.Name = "dgv_Qty";
            this.dgv_Qty.ReadOnly = true;
            this.dgv_Qty.Width = 60;
            // 
            // SalesRate
            // 
            this.SalesRate.HeaderText = "SalesRate";
            this.SalesRate.Name = "SalesRate";
            this.SalesRate.ReadOnly = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.TxtProduct);
            this.panel4.Controls.Add(this.label19);
            this.panel4.Controls.Add(this.TxtSalesRate);
            this.panel4.Controls.Add(this.BtnProduct);
            this.panel4.Controls.Add(this.txtQty);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(780, 41);
            this.panel4.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label8.Location = new System.Drawing.Point(7, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 19);
            this.label8.TabIndex = 3;
            this.label8.Text = "Product";
            // 
            // TxtProduct
            // 
            this.TxtProduct.BackColor = System.Drawing.Color.White;
            this.TxtProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtProduct.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtProduct.Location = new System.Drawing.Point(79, 9);
            this.TxtProduct.MaxLength = 255;
            this.TxtProduct.Name = "TxtProduct";
            this.TxtProduct.ReadOnly = true;
            this.TxtProduct.Size = new System.Drawing.Size(331, 25);
            this.TxtProduct.TabIndex = 0;
            this.TxtProduct.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtProduct_KeyDown);
            this.TxtProduct.Validating += new System.ComponentModel.CancelEventHandler(this.TxtProduct_Validating);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label19.Location = new System.Drawing.Point(630, 12);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(35, 19);
            this.label19.TabIndex = 514;
            this.label19.Text = "Qty";
            // 
            // TxtSalesRate
            // 
            this.TxtSalesRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSalesRate.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtSalesRate.Location = new System.Drawing.Point(542, 9);
            this.TxtSalesRate.MaxLength = 255;
            this.TxtSalesRate.Name = "TxtSalesRate";
            this.TxtSalesRate.Size = new System.Drawing.Size(86, 25);
            this.TxtSalesRate.TabIndex = 1;
            this.TxtSalesRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtSalesRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSalesRate_KeyDown);
            // 
            // BtnProduct
            // 
            this.BtnProduct.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnProduct.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnProduct.Location = new System.Drawing.Point(412, 7);
            this.BtnProduct.Name = "BtnProduct";
            this.BtnProduct.Size = new System.Drawing.Size(31, 27);
            this.BtnProduct.TabIndex = 515;
            this.BtnProduct.TabStop = false;
            this.BtnProduct.UseVisualStyleBackColor = false;
            this.BtnProduct.Click += new System.EventHandler(this.BtnProduct_Click);
            // 
            // txtQty
            // 
            this.txtQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQty.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.txtQty.Location = new System.Drawing.Point(671, 9);
            this.txtQty.MaxLength = 255;
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(104, 25);
            this.txtQty.TabIndex = 2;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtQty_KeyDown);
            this.txtQty.Validating += new System.ComponentModel.CancelEventHandler(this.TxtQty_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label1.Location = new System.Drawing.Point(450, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 19);
            this.label1.TabIndex = 517;
            this.label1.Text = "Sales Rate";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel2.Controls.Add(this.btnClear);
            this.panel2.Controls.Add(this.btnPrint);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(0, 448);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(780, 39);
            this.panel2.TabIndex = 2;
            // 
            // btnClear
            // 
            this.btnClear.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.Location = new System.Drawing.Point(668, 5);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(107, 32);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "&CANCEL";
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Appearance.Options.UseFont = true;
            this.btnPrint.ImageOptions.Image = global::MrBLL.Properties.Resources.Printer24;
            this.btnPrint.Location = new System.Drawing.Point(582, 4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(84, 32);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "&PRINT";
            this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // FrmBarcodePrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 487);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBarcodePrint";
            this.ShowIcon = false;
            this.Text = "Barcode Print";
            this.Load += new System.EventHandler(this.FrmBarcodePrint_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button BtnProduct;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label8;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private System.Windows.Forms.DataGridView DGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Sno;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_ProductId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalesRate;
        private System.Windows.Forms.Label label1;
        private MrPanel panel1;
        private MrPanel panel3;
        private MrPanel panel2;
        private MrTextBox txtQty;
        private MrTextBox TxtProduct;
        private MrTextBox TxtSalesRate;
        private System.Windows.Forms.Panel panel4;
    }
}