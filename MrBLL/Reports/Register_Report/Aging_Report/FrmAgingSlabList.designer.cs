using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Register_Report.Aging_Report
{
    partial class FrmAgingSlabList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAgingSlabList));
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Save = new DevExpress.XtraEditors.SimpleButton();
            this.GSlabList = new System.Windows.Forms.DataGridView();
            this.txt_GCompanyId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_GCompanyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StorePanel = new MrPanel();
            this.clsSeparator1 = new ClsSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.GSlabList)).BeginInit();
            this.StorePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Appearance.Options.UseFont = true;
            this.btn_Cancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(205, 149);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(106, 32);
            this.btn_Cancel.TabIndex = 5;
            this.btn_Cancel.Text = "&CANCEL";
            this.btn_Cancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.Appearance.Options.UseFont = true;
            this.btn_Save.ImageOptions.Image = global::MrBLL.Properties.Resources.OK;
            this.btn_Save.Location = new System.Drawing.Point(134, 149);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(70, 32);
            this.btn_Save.TabIndex = 4;
            this.btn_Save.Text = "OK";
            this.btn_Save.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // GSlabList
            // 
            this.GSlabList.AllowUserToAddRows = false;
            this.GSlabList.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.GSlabList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GSlabList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.txt_GCompanyId,
            this.txt_GCompanyName});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkSeaGreen;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GSlabList.DefaultCellStyle = dataGridViewCellStyle2;
            this.GSlabList.GridColor = System.Drawing.Color.DarkSeaGreen;
            this.GSlabList.Location = new System.Drawing.Point(8, 8);
            this.GSlabList.Margin = new System.Windows.Forms.Padding(2);
            this.GSlabList.Name = "GSlabList";
            this.GSlabList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GSlabList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.GSlabList.RowHeadersWidth = 20;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.GSlabList.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.GSlabList.RowTemplate.Height = 24;
            this.GSlabList.Size = new System.Drawing.Size(303, 133);
            this.GSlabList.TabIndex = 1;
            this.GSlabList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GSlabList_RowEnter);
            this.GSlabList.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.GSlabList_CellEnter);
            this.GSlabList.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.GSlabList_CellLeave);
            this.GSlabList.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.GSlabList_CellValidating);
            this.GSlabList.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.GSlabList_DataError);
            this.GSlabList.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.GSlabList_EditingControlShowing);
            this.GSlabList.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.GSlabList_RowEnter);
            this.GSlabList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GSlabList_KeyDown);
            this.GSlabList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GSlabList_KeyPress);
            // 
            // txt_GCompanyId
            // 
            this.txt_GCompanyId.HeaderText = "SNo.";
            this.txt_GCompanyId.Name = "txt_GCompanyId";
            this.txt_GCompanyId.ReadOnly = true;
            this.txt_GCompanyId.Width = 50;
            // 
            // txt_GCompanyName
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            this.txt_GCompanyName.DefaultCellStyle = dataGridViewCellStyle1;
            this.txt_GCompanyName.FillWeight = 130F;
            this.txt_GCompanyName.HeaderText = "Slab";
            this.txt_GCompanyName.MaxInputLength = 10;
            this.txt_GCompanyName.Name = "txt_GCompanyName";
            this.txt_GCompanyName.Width = 200;
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.clsSeparator1);
            this.StorePanel.Controls.Add(this.btn_Cancel);
            this.StorePanel.Controls.Add(this.btn_Save);
            this.StorePanel.Controls.Add(this.GSlabList);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(316, 185);
            this.StorePanel.TabIndex = 2;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator1.Location = new System.Drawing.Point(5, 145);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(306, 2);
            this.clsSeparator1.TabIndex = 10;
            this.clsSeparator1.TabStop = false;
            // 
            // FrmAgingSlabList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(316, 185);
            this.Controls.Add(this.StorePanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAgingSlabList";
            this.ShowIcon = false;
            this.Text = "Slab List";
            this.Load += new System.EventHandler(this.FrmAgingSlabList_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmAgingSlabList_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.GSlabList)).EndInit();
            this.StorePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView GSlabList;
        private DevExpress.XtraEditors.SimpleButton btn_Cancel;
        private DevExpress.XtraEditors.SimpleButton btn_Save;
        private System.Windows.Forms.DataGridViewTextBoxColumn txt_GCompanyId;
        private System.Windows.Forms.DataGridViewTextBoxColumn txt_GCompanyName;
        private ClsSeparator clsSeparator1;
        private MrPanel StorePanel;
    }
}