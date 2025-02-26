using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.DataEntry.Common
{
    partial class FrmVehicleMaster
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
            this.mrPanel1 = new MrPanel();
            this.DGrid = new MrDataGridView();
            this.mrGroup2 = new MrGroup();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mrGroup1 = new MrGroup();
            this.BtnShow = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.GTxtSNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtProductId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtChasisNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtEngineNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.mrPanel1.Size = new System.Drawing.Size(871, 273);
            this.mrPanel1.TabIndex = 0;
            // 
            // DGrid
            // 
            this.DGrid.AllowUserToDeleteRows = false;
            this.DGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.DGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GTxtSNo,
            this.GTxtProductId,
            this.GTxtChasisNo,
            this.GTxtEngineNo,
            this.GTxtModel,
            this.GTxtColor,
            this.GTxtNumber,
            this.GTxtQty});
            this.DGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.DGrid.DoubleBufferEnabled = true;
            this.DGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.DGrid.Location = new System.Drawing.Point(0, 0);
            this.DGrid.Name = "DGrid";
            this.DGrid.ReadOnly = true;
            this.DGrid.RowHeadersWidth = 25;
            this.DGrid.Size = new System.Drawing.Size(871, 192);
            this.DGrid.TabIndex = 0;
            // 
            // mrGroup2
            // 
            this.mrGroup2.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup2.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup2.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup2.BorderColor = System.Drawing.Color.White;
            this.mrGroup2.BorderThickness = 1F;
            this.mrGroup2.Controls.Add(this.label2);
            this.mrGroup2.Controls.Add(this.label1);
            this.mrGroup2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup2.GroupImage = null;
            this.mrGroup2.GroupTitle = "";
            this.mrGroup2.Location = new System.Drawing.Point(1, 183);
            this.mrGroup2.Name = "mrGroup2";
            this.mrGroup2.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup2.PaintGroupBox = false;
            this.mrGroup2.RoundCorners = 3;
            this.mrGroup2.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup2.ShadowControl = false;
            this.mrGroup2.ShadowThickness = 3;
            this.mrGroup2.Size = new System.Drawing.Size(867, 43);
            this.mrGroup2.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(706, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 27);
            this.label2.TabIndex = 5;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(623, 16);
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
            this.mrGroup1.Controls.Add(this.BtnShow);
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
            // BtnShow
            // 
            this.BtnShow.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnShow.Appearance.Options.UseFont = true;
            this.BtnShow.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnShow.Location = new System.Drawing.Point(645, 14);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(100, 34);
            this.BtnShow.TabIndex = 2;
            this.BtnShow.Text = "&SAVE";
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(748, 14);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(109, 34);
            this.BtnCancel.TabIndex = 3;
            this.BtnCancel.Text = "&CANCEL";
            // 
            // GTxtSNo
            // 
            this.GTxtSNo.HeaderText = "SNo";
            this.GTxtSNo.Name = "GTxtSNo";
            this.GTxtSNo.ReadOnly = true;
            this.GTxtSNo.Width = 65;
            // 
            // GTxtProductId
            // 
            this.GTxtProductId.HeaderText = "ProductId";
            this.GTxtProductId.Name = "GTxtProductId";
            this.GTxtProductId.ReadOnly = true;
            this.GTxtProductId.Visible = false;
            // 
            // GTxtChasisNo
            // 
            this.GTxtChasisNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.GTxtChasisNo.HeaderText = "Chasis No";
            this.GTxtChasisNo.Name = "GTxtChasisNo";
            this.GTxtChasisNo.ReadOnly = true;
            // 
            // GTxtEngineNo
            // 
            this.GTxtEngineNo.HeaderText = "Engine No";
            this.GTxtEngineNo.Name = "GTxtEngineNo";
            this.GTxtEngineNo.ReadOnly = true;
            this.GTxtEngineNo.Width = 120;
            // 
            // GTxtModel
            // 
            this.GTxtModel.HeaderText = "Model";
            this.GTxtModel.Name = "GTxtModel";
            this.GTxtModel.ReadOnly = true;
            // 
            // GTxtColor
            // 
            this.GTxtColor.HeaderText = "Color";
            this.GTxtColor.Name = "GTxtColor";
            this.GTxtColor.ReadOnly = true;
            // 
            // GTxtNumber
            // 
            this.GTxtNumber.HeaderText = "Number";
            this.GTxtNumber.Name = "GTxtNumber";
            this.GTxtNumber.ReadOnly = true;
            // 
            // GTxtQty
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GTxtQty.DefaultCellStyle = dataGridViewCellStyle1;
            this.GTxtQty.HeaderText = "Qty";
            this.GTxtQty.Name = "GTxtQty";
            this.GTxtQty.ReadOnly = true;
            this.GTxtQty.Width = 120;
            // 
            // FrmVehicleMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 273);
            this.Controls.Add(this.mrPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmVehicleMaster";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Product Serial No";
            this.Load += new System.EventHandler(this.FrmVehicleMaster_Load);
            this.mrPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).EndInit();
            this.mrGroup2.ResumeLayout(false);
            this.mrGroup2.PerformLayout();
            this.mrGroup1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MrPanel mrPanel1;
        private MrDataGridView DGrid;
        private DevExpress.XtraEditors.SimpleButton BtnShow;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private MrGroup mrGroup1;
        private MrGroup mrGroup2;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtSNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtProductId;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtChasisNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtEngineNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtQty;
    }
}