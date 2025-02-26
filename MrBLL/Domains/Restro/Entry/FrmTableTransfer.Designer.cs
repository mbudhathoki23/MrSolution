using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.Restro.Entry
{
    partial class FrmTableTransfer
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
            this.mrPanel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.mrGroup2 = new MrDAL.Control.ControlsEx.Control.MrGroup();
            this.RGrid = new MrDAL.Control.ControlsEx.Control.DataGridViewEx();
            this.GTxtSno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtnOccupied = new System.Windows.Forms.Button();
            this.TxtOccupiedTable = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.mrGroup1 = new MrDAL.Control.ControlsEx.Control.MrGroup();
            this.BtnAvailable = new System.Windows.Forms.Button();
            this.TxtAvailableTable = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.mrGroup3 = new MrDAL.Control.ControlsEx.Control.MrGroup();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnTransfer = new DevExpress.XtraEditors.SimpleButton();
            this.mrPanel1.SuspendLayout();
            this.mrGroup2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).BeginInit();
            this.mrGroup1.SuspendLayout();
            this.mrGroup3.SuspendLayout();
            this.SuspendLayout();
            // 
            // mrPanel1
            // 
            this.mrPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel1.Controls.Add(this.mrGroup2);
            this.mrPanel1.Controls.Add(this.mrGroup1);
            this.mrPanel1.Controls.Add(this.mrGroup3);
            this.mrPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrPanel1.Location = new System.Drawing.Point(0, 0);
            this.mrPanel1.Name = "mrPanel1";
            this.mrPanel1.Size = new System.Drawing.Size(469, 480);
            this.mrPanel1.TabIndex = 0;
            // 
            // mrGroup2
            // 
            this.mrGroup2.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup2.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup2.BackgroundGradientMode = MrDAL.Control.ControlsEx.Control.MrGroup.GroupBoxGradientMode.None;
            this.mrGroup2.BorderColor = System.Drawing.Color.White;
            this.mrGroup2.BorderThickness = 1F;
            this.mrGroup2.Controls.Add(this.RGrid);
            this.mrGroup2.Controls.Add(this.BtnOccupied);
            this.mrGroup2.Controls.Add(this.TxtOccupiedTable);
            this.mrGroup2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup2.GroupImage = null;
            this.mrGroup2.GroupTitle = "OCCUPIED";
            this.mrGroup2.Location = new System.Drawing.Point(3, 4);
            this.mrGroup2.Name = "mrGroup2";
            this.mrGroup2.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup2.PaintGroupBox = false;
            this.mrGroup2.RoundCorners = 10;
            this.mrGroup2.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup2.ShadowControl = true;
            this.mrGroup2.ShadowThickness = 3;
            this.mrGroup2.Size = new System.Drawing.Size(463, 340);
            this.mrGroup2.TabIndex = 0;
            // 
            // RGrid
            // 
            this.RGrid.AllowUserToAddRows = false;
            this.RGrid.AllowUserToDeleteRows = false;
            this.RGrid.AllowUserToResizeRows = false;
            this.RGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.RGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GTxtSno,
            this.GTxtProduct});
            this.RGrid.DoubleBufferEnabled = true;
            this.RGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.RGrid.Location = new System.Drawing.Point(9, 64);
            this.RGrid.MultiSelect = false;
            this.RGrid.Name = "RGrid";
            this.RGrid.ReadOnly = true;
            this.RGrid.RowHeadersWidth = 25;
            this.RGrid.Size = new System.Drawing.Size(443, 269);
            this.RGrid.StandardTab = true;
            this.RGrid.TabIndex = 311;
            // 
            // GTxtSno
            // 
            this.GTxtSno.DataPropertyName = "Invoice_SNo";
            this.GTxtSno.HeaderText = "Sno";
            this.GTxtSno.Name = "GTxtSno";
            this.GTxtSno.ReadOnly = true;
            this.GTxtSno.Width = 50;
            // 
            // GTxtProduct
            // 
            this.GTxtProduct.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.GTxtProduct.DataPropertyName = "PName";
            this.GTxtProduct.HeaderText = "Product";
            this.GTxtProduct.Name = "GTxtProduct";
            this.GTxtProduct.ReadOnly = true;
            // 
            // BtnOccupied
            // 
            this.BtnOccupied.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnOccupied.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnOccupied.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnOccupied.Location = new System.Drawing.Point(422, 31);
            this.BtnOccupied.Name = "BtnOccupied";
            this.BtnOccupied.Size = new System.Drawing.Size(28, 26);
            this.BtnOccupied.TabIndex = 310;
            this.BtnOccupied.TabStop = false;
            this.BtnOccupied.UseVisualStyleBackColor = false;
            this.BtnOccupied.Click += new System.EventHandler(this.BtnOccupied_Click);
            // 
            // TxtOccupiedTable
            // 
            this.TxtOccupiedTable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtOccupiedTable.Location = new System.Drawing.Point(9, 32);
            this.TxtOccupiedTable.Name = "TxtOccupiedTable";
            this.TxtOccupiedTable.Size = new System.Drawing.Size(409, 25);
            this.TxtOccupiedTable.TabIndex = 0;
            this.TxtOccupiedTable.TextChanged += new System.EventHandler(this.TxtOccupiedTable_TextChanged);
            this.TxtOccupiedTable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtOccupiedTable_KeyDown);
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrDAL.Control.ControlsEx.Control.MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.BtnAvailable);
            this.mrGroup1.Controls.Add(this.TxtAvailableTable);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "AVAILABLE";
            this.mrGroup1.Location = new System.Drawing.Point(3, 343);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 10;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = true;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(463, 73);
            this.mrGroup1.TabIndex = 1;
            // 
            // BtnAvailable
            // 
            this.BtnAvailable.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnAvailable.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnAvailable.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnAvailable.Location = new System.Drawing.Point(422, 35);
            this.BtnAvailable.Name = "BtnAvailable";
            this.BtnAvailable.Size = new System.Drawing.Size(28, 26);
            this.BtnAvailable.TabIndex = 311;
            this.BtnAvailable.TabStop = false;
            this.BtnAvailable.UseVisualStyleBackColor = false;
            this.BtnAvailable.Click += new System.EventHandler(this.BtnAvailable_Click);
            // 
            // TxtAvailableTable
            // 
            this.TxtAvailableTable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAvailableTable.Location = new System.Drawing.Point(20, 36);
            this.TxtAvailableTable.Name = "TxtAvailableTable";
            this.TxtAvailableTable.Size = new System.Drawing.Size(396, 25);
            this.TxtAvailableTable.TabIndex = 0;
            this.TxtAvailableTable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtAvailableTable_KeyDown);
            // 
            // mrGroup3
            // 
            this.mrGroup3.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup3.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup3.BackgroundGradientMode = MrDAL.Control.ControlsEx.Control.MrGroup.GroupBoxGradientMode.None;
            this.mrGroup3.BorderColor = System.Drawing.Color.White;
            this.mrGroup3.BorderThickness = 1F;
            this.mrGroup3.Controls.Add(this.BtnCancel);
            this.mrGroup3.Controls.Add(this.BtnTransfer);
            this.mrGroup3.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup3.GroupImage = null;
            this.mrGroup3.GroupTitle = "";
            this.mrGroup3.Location = new System.Drawing.Point(3, 407);
            this.mrGroup3.Name = "mrGroup3";
            this.mrGroup3.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup3.PaintGroupBox = false;
            this.mrGroup3.RoundCorners = 10;
            this.mrGroup3.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup3.ShadowControl = true;
            this.mrGroup3.ShadowThickness = 3;
            this.mrGroup3.Size = new System.Drawing.Size(463, 63);
            this.mrGroup3.TabIndex = 2;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.BtnCancel.Location = new System.Drawing.Point(315, 18);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(104, 33);
            this.BtnCancel.TabIndex = 11;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnTransfer
            // 
            this.BtnTransfer.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnTransfer.Appearance.Options.UseFont = true;
            this.BtnTransfer.ImageOptions.Image = global::MrBLL.Properties.Resources.Reverse;
            this.BtnTransfer.Location = new System.Drawing.Point(179, 18);
            this.BtnTransfer.Name = "BtnTransfer";
            this.BtnTransfer.Size = new System.Drawing.Size(134, 33);
            this.BtnTransfer.TabIndex = 10;
            this.BtnTransfer.Text = "&TRANSFER";
            this.BtnTransfer.Click += new System.EventHandler(this.BtnTransfer_Click);
            // 
            // FrmTableTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(469, 480);
            this.Controls.Add(this.mrPanel1);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTableTransfer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TABLE TRANSFER";
            this.Load += new System.EventHandler(this.FrmTableTransfer_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmTableTransfer_KeyPress);
            this.mrPanel1.ResumeLayout(false);
            this.mrGroup2.ResumeLayout(false);
            this.mrGroup2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).EndInit();
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            this.mrGroup3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MrPanel mrPanel1;
        private MrGroup mrGroup2;
        private MrGroup mrGroup1;
        private MrTextBox TxtOccupiedTable;
        private MrTextBox TxtAvailableTable;
        private System.Windows.Forms.Button BtnOccupied;
        private System.Windows.Forms.Button BtnAvailable;
        private MrGroup mrGroup3;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnTransfer;
        private DataGridViewEx RGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtSno;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtProduct;
    }
}