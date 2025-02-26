namespace MrBLL.DataEntry.Common
{
    partial class FrmNumberingScheme
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNumberingScheme));
            this.grp_NumberingScheme = new System.Windows.Forms.GroupBox();
            this.DGrid = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnOk = new DevExpress.XtraEditors.SimpleButton();
            this.txt_GCompanyId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_GCompanyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gv_CurrentNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grp_NumberingScheme.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grp_NumberingScheme
            // 
            this.grp_NumberingScheme.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.grp_NumberingScheme.Controls.Add(this.DGrid);
            this.grp_NumberingScheme.Controls.Add(this.groupBox1);
            this.grp_NumberingScheme.Location = new System.Drawing.Point(1, -5);
            this.grp_NumberingScheme.Name = "grp_NumberingScheme";
            this.grp_NumberingScheme.Size = new System.Drawing.Size(811, 293);
            this.grp_NumberingScheme.TabIndex = 0;
            this.grp_NumberingScheme.TabStop = false;
            // 
            // DGrid
            // 
            this.DGrid.AllowUserToAddRows = false;
            this.DGrid.AllowUserToDeleteRows = false;
            this.DGrid.AllowUserToResizeColumns = false;
            this.DGrid.AllowUserToResizeRows = false;
            this.DGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.DGrid.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.txt_GCompanyId,
            this.txt_GCompanyName,
            this.txt_Type,
            this.StartNumber,
            this.EndNumber,
            this.gv_CurrentNum});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkSeaGreen;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.DGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGrid.GridColor = System.Drawing.Color.DarkSeaGreen;
            this.DGrid.Location = new System.Drawing.Point(3, 16);
            this.DGrid.Margin = new System.Windows.Forms.Padding(2);
            this.DGrid.MultiSelect = false;
            this.DGrid.Name = "DGrid";
            this.DGrid.ReadOnly = true;
            this.DGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DGrid.RowHeadersWidth = 20;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.DGrid.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.DGrid.RowTemplate.Height = 24;
            this.DGrid.ShowCellErrors = false;
            this.DGrid.ShowCellToolTips = false;
            this.DGrid.ShowEditingIcon = false;
            this.DGrid.ShowRowErrors = false;
            this.DGrid.Size = new System.Drawing.Size(805, 226);
            this.DGrid.StandardTab = true;
            this.DGrid.TabIndex = 0;
            this.DGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGrid_CellEnter);
            this.DGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGrid_RowEnter);
            this.DGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DGrid_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Controls.Add(this.btn_Cancel);
            this.groupBox1.Controls.Add(this.BtnOk);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(3, 242);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(805, 48);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Appearance.Options.UseFont = true;
            this.btn_Cancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(494, 8);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(108, 37);
            this.btn_Cancel.TabIndex = 2;
            this.btn_Cancel.Text = "&CANCEL";
            this.btn_Cancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnOk
            // 
            this.BtnOk.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnOk.Appearance.Options.UseFont = true;
            this.BtnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnOk.ImageOptions.Image = global::MrBLL.Properties.Resources.Login24;
            this.BtnOk.Location = new System.Drawing.Point(383, 8);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(111, 37);
            this.BtnOk.TabIndex = 1;
            this.BtnOk.Text = "&SELECT";
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // txt_GCompanyId
            // 
            this.txt_GCompanyId.HeaderText = "Autogenerate Id";
            this.txt_GCompanyId.Name = "txt_GCompanyId";
            this.txt_GCompanyId.ReadOnly = true;
            this.txt_GCompanyId.Visible = false;
            // 
            // txt_GCompanyName
            // 
            this.txt_GCompanyName.FillWeight = 130F;
            this.txt_GCompanyName.HeaderText = "Description";
            this.txt_GCompanyName.Name = "txt_GCompanyName";
            this.txt_GCompanyName.ReadOnly = true;
            this.txt_GCompanyName.Width = 200;
            // 
            // txt_Type
            // 
            this.txt_Type.HeaderText = "Type";
            this.txt_Type.Name = "txt_Type";
            this.txt_Type.ReadOnly = true;
            // 
            // StartNumber
            // 
            this.StartNumber.HeaderText = "Start Number";
            this.StartNumber.Name = "StartNumber";
            this.StartNumber.ReadOnly = true;
            this.StartNumber.Width = 150;
            // 
            // EndNumber
            // 
            this.EndNumber.HeaderText = "End Number";
            this.EndNumber.Name = "EndNumber";
            this.EndNumber.ReadOnly = true;
            this.EndNumber.Width = 150;
            // 
            // gv_CurrentNum
            // 
            this.gv_CurrentNum.HeaderText = "Current Number";
            this.gv_CurrentNum.Name = "gv_CurrentNum";
            this.gv_CurrentNum.ReadOnly = true;
            this.gv_CurrentNum.Width = 165;
            // 
            // FrmNumberingScheme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(811, 289);
            this.Controls.Add(this.grp_NumberingScheme);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmNumberingScheme";
            this.ShowIcon = false;
            this.Text = "DOCUMENT NUMBERING SELECTION";
            this.Load += new System.EventHandler(this.NumberingScheme_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumberingScheme_KeyPress);
            this.grp_NumberingScheme.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grp_NumberingScheme;
        private System.Windows.Forms.DataGridView DGrid;
        private DevExpress.XtraEditors.SimpleButton BtnOk;
        private DevExpress.XtraEditors.SimpleButton btn_Cancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn txt_GCompanyId;
        private System.Windows.Forms.DataGridViewTextBoxColumn txt_GCompanyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn txt_Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn gv_CurrentNum;
    }
}