using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Setup.BranchSetup
{
    partial class FrmBranchList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBranchList));
            this.DGrid = new System.Windows.Forms.DataGridView();
            this.txt_BranchId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_BranchName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CmbFiscalYear = new System.Windows.Forms.ComboBox();
            this.lbl_FiscalYear = new System.Windows.Forms.Label();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new MrPanel();
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DGrid
            // 
            this.DGrid.AllowUserToAddRows = false;
            this.DGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.DGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.txt_BranchId,
            this.txt_BranchName});
            this.DGrid.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.DGrid.Location = new System.Drawing.Point(2, 2);
            this.DGrid.Margin = new System.Windows.Forms.Padding(2);
            this.DGrid.Name = "DGrid";
            this.DGrid.ReadOnly = true;
            this.DGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.DGrid.RowHeadersVisible = false;
            this.DGrid.RowTemplate.Height = 24;
            this.DGrid.Size = new System.Drawing.Size(587, 215);
            this.DGrid.TabIndex = 21;
            this.DGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_CellContentClick);
            this.DGrid.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_CellContentDoubleClick);
            this.DGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_CellEnter);
            this.DGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_RowEnter);
            this.DGrid.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.RGrid_RowHeaderMouseDoubleClick);
            this.DGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RGrid_KeyDown);
            // 
            // txt_BranchId
            // 
            this.txt_BranchId.HeaderText = "Branch Id";
            this.txt_BranchId.Name = "txt_BranchId";
            this.txt_BranchId.ReadOnly = true;
            this.txt_BranchId.Visible = false;
            // 
            // txt_BranchName
            // 
            this.txt_BranchName.FillWeight = 130F;
            this.txt_BranchName.HeaderText = "BRANCH LIST";
            this.txt_BranchName.Name = "txt_BranchName";
            this.txt_BranchName.ReadOnly = true;
            this.txt_BranchName.Width = 550;
            // 
            // CmbFiscalYear
            // 
            this.CmbFiscalYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbFiscalYear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbFiscalYear.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbFiscalYear.FormattingEnabled = true;
            this.CmbFiscalYear.Location = new System.Drawing.Point(101, 223);
            this.CmbFiscalYear.Name = "CmbFiscalYear";
            this.CmbFiscalYear.Size = new System.Drawing.Size(275, 28);
            this.CmbFiscalYear.TabIndex = 24;
            // 
            // lbl_FiscalYear
            // 
            this.lbl_FiscalYear.BackColor = System.Drawing.Color.Transparent;
            this.lbl_FiscalYear.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_FiscalYear.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lbl_FiscalYear.Location = new System.Drawing.Point(1, 226);
            this.lbl_FiscalYear.Name = "lbl_FiscalYear";
            this.lbl_FiscalYear.Size = new System.Drawing.Size(94, 23);
            this.lbl_FiscalYear.TabIndex = 25;
            this.lbl_FiscalYear.Text = "Fiscal Year";
            this.lbl_FiscalYear.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(482, 221);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(107, 32);
            this.BtnCancel.TabIndex = 27;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // BtnLogin
            // 
            this.BtnLogin.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnLogin.Appearance.Options.UseFont = true;
            this.BtnLogin.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnLogin;
            this.BtnLogin.Location = new System.Drawing.Point(380, 221);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(99, 32);
            this.BtnLogin.TabIndex = 26;
            this.BtnLogin.Text = "&LOGIN";
            this.BtnLogin.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.DGrid);
            this.panel1.Controls.Add(this.BtnCancel);
            this.panel1.Controls.Add(this.CmbFiscalYear);
            this.panel1.Controls.Add(this.lbl_FiscalYear);
            this.panel1.Controls.Add(this.BtnLogin);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(593, 257);
            this.panel1.TabIndex = 28;
            // 
            // FrmBranchList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(593, 257);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBranchList";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Branch List";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmBranchList_FormClosing);
            this.Load += new System.EventHandler(this.FrmBranchList_Load);
            this.Shown += new System.EventHandler(this.FrmBranchList_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmBranchList_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGrid;
        private System.Windows.Forms.ComboBox CmbFiscalYear;
        private System.Windows.Forms.Label lbl_FiscalYear;
        private DevExpress.XtraEditors.SimpleButton BtnLogin;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn txt_BranchId;
        private System.Windows.Forms.DataGridViewTextBoxColumn txt_BranchName;
        private System.Windows.Forms.Panel panel1;
    }
}