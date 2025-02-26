namespace MrBLL.Utility.DataSync
{
    partial class FrmApiKeyList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mrPanel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.RGrid = new MrDAL.Control.ControlsEx.Control.EntryGridViewEx();
            this.GTxtCompanyId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtCompany = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtDatabase = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtPanNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtApiKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TxtSearch = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.mrPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // mrPanel1
            // 
            this.mrPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel1.Controls.Add(this.RGrid);
            this.mrPanel1.Controls.Add(this.TxtSearch);
            this.mrPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrPanel1.Location = new System.Drawing.Point(0, 0);
            this.mrPanel1.Name = "mrPanel1";
            this.mrPanel1.Size = new System.Drawing.Size(1484, 658);
            this.mrPanel1.TabIndex = 3;
            // 
            // RGrid
            // 
            this.RGrid.AllowUserToAddRows = false;
            this.RGrid.AllowUserToDeleteRows = false;
            this.RGrid.AllowUserToResizeColumns = false;
            this.RGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle21;
            this.RGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.RGrid.BlockNavigationOnNextRowOnEnter = true;
            this.RGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.RGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.RGrid.ColumnHeadersHeight = 30;
            this.RGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GTxtCompanyId,
            this.GTxtCompany,
            this.GTxtDatabase,
            this.GTxtPanNo,
            this.GTxtAddress,
            this.GTxtApiKey});
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Bookman Old Style", 9F);
            dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.RGrid.DefaultCellStyle = dataGridViewCellStyle23;
            this.RGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RGrid.DoubleBufferEnabled = true;
            this.RGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.RGrid.Location = new System.Drawing.Point(0, 0);
            this.RGrid.MultiSelect = false;
            this.RGrid.Name = "RGrid";
            this.RGrid.ReadOnly = true;
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Bookman Old Style", 9F);
            dataGridViewCellStyle24.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle24.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle24;
            this.RGrid.RowHeadersWidth = 25;
            dataGridViewCellStyle25.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle25.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            this.RGrid.RowsDefaultCellStyle = dataGridViewCellStyle25;
            this.RGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RGrid.RowTemplate.Height = 24;
            this.RGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RGrid.Size = new System.Drawing.Size(1484, 632);
            this.RGrid.StandardTab = true;
            this.RGrid.TabIndex = 1;
            this.RGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RGrid_KeyDown);
            // 
            // GTxtCompanyId
            // 
            this.GTxtCompanyId.HeaderText = "Company Id";
            this.GTxtCompanyId.Name = "GTxtCompanyId";
            this.GTxtCompanyId.ReadOnly = true;
            this.GTxtCompanyId.Visible = false;
            // 
            // GTxtCompany
            // 
            this.GTxtCompany.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.GTxtCompany.FillWeight = 130F;
            this.GTxtCompany.HeaderText = "Description";
            this.GTxtCompany.MinimumWidth = 10;
            this.GTxtCompany.Name = "GTxtCompany";
            this.GTxtCompany.ReadOnly = true;
            // 
            // GTxtDatabase
            // 
            this.GTxtDatabase.HeaderText = "DataBase";
            this.GTxtDatabase.Name = "GTxtDatabase";
            this.GTxtDatabase.ReadOnly = true;
            this.GTxtDatabase.Visible = false;
            // 
            // GTxtPanNo
            // 
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GTxtPanNo.DefaultCellStyle = dataGridViewCellStyle22;
            this.GTxtPanNo.HeaderText = "Pan No";
            this.GTxtPanNo.Name = "GTxtPanNo";
            this.GTxtPanNo.ReadOnly = true;
            this.GTxtPanNo.Width = 150;
            // 
            // GTxtAddress
            // 
            this.GTxtAddress.HeaderText = "Address";
            this.GTxtAddress.Name = "GTxtAddress";
            this.GTxtAddress.ReadOnly = true;
            this.GTxtAddress.Width = 220;
            // 
            // GTxtApiKey
            // 
            this.GTxtApiKey.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.GTxtApiKey.HeaderText = "API Key";
            this.GTxtApiKey.Name = "GTxtApiKey";
            this.GTxtApiKey.ReadOnly = true;
            // 
            // TxtSearch
            // 
            this.TxtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSearch.CausesValidation = false;
            this.TxtSearch.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TxtSearch.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSearch.Location = new System.Drawing.Point(0, 632);
            this.TxtSearch.MaxLength = 200;
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(1484, 26);
            this.TxtSearch.TabIndex = 2;
            this.TxtSearch.Visible = false;
            this.TxtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            // 
            // FrmApiKeyList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1484, 658);
            this.Controls.Add(this.mrPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmApiKeyList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "API KEY LIST";
            this.Load += new System.EventHandler(this.FrmApiKeyList_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmApiKeyList_KeyPress);
            this.mrPanel1.ResumeLayout(false);
            this.mrPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MrDAL.Control.ControlsEx.Control.EntryGridViewEx RGrid;
        private MrDAL.Control.ControlsEx.Control.MrTextBox TxtSearch;
        private MrDAL.Control.ControlsEx.Control.MrPanel mrPanel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtCompanyId;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtCompany;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtDatabase;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtPanNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtApiKey;
    }
}