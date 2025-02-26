namespace MrBLL.Reports.Inventory_Report.ListOfMaster
{
    partial class FrmProductPreview
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
            this.DGrid = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblProductPic = new System.Windows.Forms.Label();
            this.PbPicbox = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_MRP = new System.Windows.Forms.Label();
            this.lbl_Purchase = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_Sales = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_Qty = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_AltQty = new System.Windows.Forms.Label();
            this.lbl_TotAltQty1 = new System.Windows.Forms.Label();
            this.GTxtSNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtProductId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtShortName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtUOM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbPicbox)).BeginInit();
            this.SuspendLayout();
            // 
            // DGrid
            // 
            this.DGrid.AllowUserToAddRows = false;
            this.DGrid.AllowUserToDeleteRows = false;
            this.DGrid.AllowUserToResizeColumns = false;
            this.DGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.DGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 11F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DGrid.ColumnHeadersHeight = 25;
            this.DGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GTxtSNo,
            this.GTxtProductId,
            this.GTxtDescription,
            this.GTxtShortName,
            this.GTxtUOM});
            this.DGrid.Location = new System.Drawing.Point(6, 11);
            this.DGrid.Margin = new System.Windows.Forms.Padding(2);
            this.DGrid.MultiSelect = false;
            this.DGrid.Name = "DGrid";
            this.DGrid.ReadOnly = true;
            this.DGrid.RowHeadersVisible = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.DGrid.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.DGrid.RowTemplate.Height = 24;
            this.DGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGrid.Size = new System.Drawing.Size(585, 426);
            this.DGrid.StandardTab = true;
            this.DGrid.TabIndex = 0;
            this.DGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellContentClick);
            this.DGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellEnter);
            this.DGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellValueChanged);
            this.DGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_RowEnter);
            this.DGrid.SelectionChanged += new System.EventHandler(this.Grid_SelectionChanged);
            this.DGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Grid_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DGrid);
            this.groupBox1.Location = new System.Drawing.Point(5, -4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(600, 442);
            this.groupBox1.TabIndex = 321;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblProductPic);
            this.groupBox2.Controls.Add(this.PbPicbox);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.lbl_MRP);
            this.groupBox2.Controls.Add(this.lbl_Purchase);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lbl_Sales);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.lbl_Qty);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.lbl_AltQty);
            this.groupBox2.Controls.Add(this.lbl_TotAltQty1);
            this.groupBox2.Location = new System.Drawing.Point(605, -4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(416, 442);
            this.groupBox2.TabIndex = 321;
            this.groupBox2.TabStop = false;
            // 
            // lblProductPic
            // 
            this.lblProductPic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblProductPic.Enabled = false;
            this.lblProductPic.Font = new System.Drawing.Font("Arial", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductPic.ForeColor = System.Drawing.SystemColors.Window;
            this.lblProductPic.Image = global::MrBLL.Properties.Resources.noimage;
            this.lblProductPic.Location = new System.Drawing.Point(30, 37);
            this.lblProductPic.Name = "lblProductPic";
            this.lblProductPic.Size = new System.Drawing.Size(360, 252);
            this.lblProductPic.TabIndex = 343;
            this.lblProductPic.Text = "Picture Preview";
            // 
            // PbPicbox
            // 
            this.PbPicbox.BackColor = System.Drawing.SystemColors.Control;
            this.PbPicbox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PbPicbox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PbPicbox.Enabled = false;
            this.PbPicbox.Location = new System.Drawing.Point(10, 19);
            this.PbPicbox.Name = "PbPicbox";
            this.PbPicbox.Size = new System.Drawing.Size(400, 290);
            this.PbPicbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PbPicbox.TabIndex = 342;
            this.PbPicbox.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label5.Location = new System.Drawing.Point(227, 325);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 20);
            this.label5.TabIndex = 341;
            this.label5.Text = "MRP";
            // 
            // lbl_MRP
            // 
            this.lbl_MRP.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lbl_MRP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_MRP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_MRP.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MRP.Location = new System.Drawing.Point(296, 321);
            this.lbl_MRP.Name = "lbl_MRP";
            this.lbl_MRP.Size = new System.Drawing.Size(116, 27);
            this.lbl_MRP.TabIndex = 339;
            this.lbl_MRP.Text = "0.00";
            this.lbl_MRP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_Purchase
            // 
            this.lbl_Purchase.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lbl_Purchase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Purchase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Purchase.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_Purchase.Location = new System.Drawing.Point(102, 411);
            this.lbl_Purchase.Name = "lbl_Purchase";
            this.lbl_Purchase.Size = new System.Drawing.Size(116, 27);
            this.lbl_Purchase.TabIndex = 338;
            this.lbl_Purchase.Text = "0.00";
            this.lbl_Purchase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label6.Location = new System.Drawing.Point(6, 412);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 20);
            this.label6.TabIndex = 337;
            this.label6.Text = "Buy Rate";
            // 
            // lbl_Sales
            // 
            this.lbl_Sales.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lbl_Sales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Sales.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Sales.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_Sales.Location = new System.Drawing.Point(102, 381);
            this.lbl_Sales.Name = "lbl_Sales";
            this.lbl_Sales.Size = new System.Drawing.Size(116, 27);
            this.lbl_Sales.TabIndex = 336;
            this.lbl_Sales.Text = "0.00";
            this.lbl_Sales.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label4.Location = new System.Drawing.Point(6, 384);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 20);
            this.label4.TabIndex = 335;
            this.label4.Text = "Sales Rate";
            // 
            // lbl_Qty
            // 
            this.lbl_Qty.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lbl_Qty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Qty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Qty.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Qty.Location = new System.Drawing.Point(102, 351);
            this.lbl_Qty.Name = "lbl_Qty";
            this.lbl_Qty.Size = new System.Drawing.Size(116, 27);
            this.lbl_Qty.TabIndex = 334;
            this.lbl_Qty.Text = "0.00";
            this.lbl_Qty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label2.Location = new System.Drawing.Point(6, 355);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 20);
            this.label2.TabIndex = 333;
            this.label2.Text = "Qty";
            // 
            // lbl_AltQty
            // 
            this.lbl_AltQty.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lbl_AltQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_AltQty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_AltQty.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AltQty.Location = new System.Drawing.Point(102, 321);
            this.lbl_AltQty.Name = "lbl_AltQty";
            this.lbl_AltQty.Size = new System.Drawing.Size(116, 27);
            this.lbl_AltQty.TabIndex = 332;
            this.lbl_AltQty.Text = "0.00";
            this.lbl_AltQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_TotAltQty1
            // 
            this.lbl_TotAltQty1.AutoSize = true;
            this.lbl_TotAltQty1.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_TotAltQty1.Location = new System.Drawing.Point(6, 327);
            this.lbl_TotAltQty1.Name = "lbl_TotAltQty1";
            this.lbl_TotAltQty1.Size = new System.Drawing.Size(64, 20);
            this.lbl_TotAltQty1.TabIndex = 331;
            this.lbl_TotAltQty1.Text = "Alt Qty";
            // 
            // GTxtSNo
            // 
            this.GTxtSNo.HeaderText = "SNo";
            this.GTxtSNo.Name = "GTxtSNo";
            this.GTxtSNo.ReadOnly = true;
            this.GTxtSNo.Width = 61;
            // 
            // GTxtProductId
            // 
            this.GTxtProductId.HeaderText = "GlId";
            this.GTxtProductId.Name = "GTxtProductId";
            this.GTxtProductId.ReadOnly = true;
            this.GTxtProductId.Visible = false;
            this.GTxtProductId.Width = 58;
            // 
            // GTxtDescription
            // 
            this.GTxtDescription.HeaderText = "Description";
            this.GTxtDescription.Name = "GTxtDescription";
            this.GTxtDescription.ReadOnly = true;
            this.GTxtDescription.Width = 325;
            // 
            // GTxtShortName
            // 
            this.GTxtShortName.HeaderText = "ShortName";
            this.GTxtShortName.Name = "GTxtShortName";
            this.GTxtShortName.ReadOnly = true;
            this.GTxtShortName.Width = 107;
            // 
            // GTxtUOM
            // 
            this.GTxtUOM.HeaderText = "UOM";
            this.GTxtUOM.Name = "GTxtUOM";
            this.GTxtUOM.ReadOnly = true;
            this.GTxtUOM.Width = 58;
            // 
            // FrmProductPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1021, 437);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmProductPreview";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Product Preview";
            this.Load += new System.EventHandler(this.FrmProductPreview_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmProductPreview_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbPicbox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGrid;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbl_Purchase;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_Sales;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_Qty;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_AltQty;
        private System.Windows.Forms.Label lbl_TotAltQty1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_MRP;
        private System.Windows.Forms.PictureBox PbPicbox;
        private System.Windows.Forms.Label lblProductPic;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtSNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtProductId;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtShortName;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtUOM;
    }
}