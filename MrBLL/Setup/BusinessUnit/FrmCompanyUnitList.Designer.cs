using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Setup.BusinessUnit
{
    partial class FrmCompanyUnitList
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
            this.RGrid = new System.Windows.Forms.DataGridView();
            this.btn_Clear = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Save = new DevExpress.XtraEditors.SimpleButton();
            this.StorePanel = new MrPanel();
            this.clsSeparator1 = new ClsSeparator();
            this.txt_CmpUnitId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_CmpUnitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).BeginInit();
            this.StorePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // RGrid
            // 
            this.RGrid.AllowUserToAddRows = false;
            this.RGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.RGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.txt_CmpUnitId,
            this.txt_CmpUnitName});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DarkSeaGreen;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.RGrid.DefaultCellStyle = dataGridViewCellStyle1;
            this.RGrid.Location = new System.Drawing.Point(3, 3);
            this.RGrid.Name = "RGrid";
            this.RGrid.ReadOnly = true;
            this.RGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.RGrid.RowHeadersVisible = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.RGrid.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.RGrid.RowTemplate.Height = 24;
            this.RGrid.Size = new System.Drawing.Size(529, 196);
            this.RGrid.TabIndex = 24;
            this.RGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_CellContentClick);
            this.RGrid.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_CellContentDoubleClick);
            this.RGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_CellEnter);
            this.RGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_RowEnter);
            this.RGrid.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.RGrid_RowHeaderMouseDoubleClick);
            this.RGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RGrid_KeyDown);
            // 
            // btn_Clear
            // 
            this.btn_Clear.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Clear.Appearance.Options.UseFont = true;
            this.btn_Clear.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.btn_Clear.Location = new System.Drawing.Point(398, 204);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(117, 38);
            this.btn_Clear.TabIndex = 29;
            this.btn_Clear.Text = "&CANCEL";
            this.btn_Clear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.Appearance.Options.UseFont = true;
            this.btn_Save.ImageOptions.Image = global::MrBLL.Properties.Resources.logout;
            this.btn_Save.Location = new System.Drawing.Point(293, 204);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(105, 38);
            this.btn_Save.TabIndex = 28;
            this.btn_Save.Text = "&LOGIN";
            this.btn_Save.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.clsSeparator1);
            this.StorePanel.Controls.Add(this.btn_Clear);
            this.StorePanel.Controls.Add(this.btn_Save);
            this.StorePanel.Controls.Add(this.RGrid);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(537, 244);
            this.StorePanel.TabIndex = 30;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator1.Location = new System.Drawing.Point(3, 200);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(525, 2);
            this.clsSeparator1.TabIndex = 30;
            this.clsSeparator1.TabStop = false;
            // 
            // txt_CmpUnitId
            // 
            this.txt_CmpUnitId.HeaderText = "COMPANY UNIT";
            this.txt_CmpUnitId.Name = "txt_CmpUnitId";
            this.txt_CmpUnitId.ReadOnly = true;
            this.txt_CmpUnitId.Visible = false;
            // 
            // txt_CmpUnitName
            // 
            this.txt_CmpUnitName.FillWeight = 130F;
            this.txt_CmpUnitName.HeaderText = "COMPANY UNIT LIST";
            this.txt_CmpUnitName.Name = "txt_CmpUnitName";
            this.txt_CmpUnitName.ReadOnly = true;
            this.txt_CmpUnitName.Width = 501;
            // 
            // FrmCompanyUnitList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(537, 244);
            this.Controls.Add(this.StorePanel);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = global::MrBLL.Properties.Resources.MrLogo;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCompanyUnitList";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Company Unit List";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCompanyUnitList_FormClosing);
            this.Load += new System.EventHandler(this.FrmCompanyUnitList_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmCompanyUnitList_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).EndInit();
            this.StorePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView RGrid;
        private DevExpress.XtraEditors.SimpleButton btn_Clear;
        private DevExpress.XtraEditors.SimpleButton btn_Save;
        private ClsSeparator clsSeparator1;
        private MrPanel StorePanel;
        private System.Windows.Forms.DataGridViewTextBoxColumn txt_CmpUnitId;
        private System.Windows.Forms.DataGridViewTextBoxColumn txt_CmpUnitName;
    }
}