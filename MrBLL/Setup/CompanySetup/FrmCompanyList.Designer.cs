using System.Windows.Forms;
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Setup.CompanySetup
{
    partial class FrmCompanyList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.RGrid = new MrDAL.Control.ControlsEx.Control.EntryGridViewEx();
            this.txt_GCompanyId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_GCompanyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_DatabaseName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_PanNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_NewCompany = new DevExpress.XtraEditors.SimpleButton();
            this.TxtSearch = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // RGrid
            // 
            this.RGrid.AllowUserToAddRows = false;
            this.RGrid.AllowUserToDeleteRows = false;
            this.RGrid.AllowUserToResizeColumns = false;
            this.RGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.RGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.RGrid.BlockNavigationOnNextRowOnEnter = true;
            this.RGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.RGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.RGrid.ColumnHeadersHeight = 30;
            this.RGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.txt_GCompanyId,
            this.txt_GCompanyName,
            this.txt_DatabaseName,
            this.txt_PanNo,
            this.txtAddress});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Bookman Old Style", 9F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.RGrid.DefaultCellStyle = dataGridViewCellStyle4;
            this.RGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RGrid.DoubleBufferEnabled = true;
            this.RGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.RGrid.Location = new System.Drawing.Point(0, 0);
            this.RGrid.MultiSelect = false;
            this.RGrid.Name = "RGrid";
            this.RGrid.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Bookman Old Style", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.RGrid.RowHeadersWidth = 25;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            this.RGrid.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.RGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RGrid.RowTemplate.Height = 24;
            this.RGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RGrid.Size = new System.Drawing.Size(968, 544);
            this.RGrid.TabIndex = 0;
            this.RGrid.EnterKeyPressed += new System.EventHandler<System.EventArgs>(this.RGrid_EnterKeyPressed);
            this.RGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_CellContentClick);
            this.RGrid.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_CellContentDoubleClick);
            this.RGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_CellEnter);
            this.RGrid.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.RGrid_RowHeaderMouseDoubleClick);
            this.RGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_RowEnter);
            this.RGrid.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.RGrid_RowHeaderMouseDoubleClick);
            this.RGrid.Sorted += new System.EventHandler(this.RGrid_Sorted);
            this.RGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RGrid_KeyDown);
            // 
            // txt_GCompanyId
            // 
            this.txt_GCompanyId.HeaderText = "Company Id";
            this.txt_GCompanyId.Name = "txt_GCompanyId";
            this.txt_GCompanyId.ReadOnly = true;
            this.txt_GCompanyId.Visible = false;
            // 
            // txt_GCompanyName
            // 
            this.txt_GCompanyName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.txt_GCompanyName.FillWeight = 130F;
            this.txt_GCompanyName.HeaderText = "Description";
            this.txt_GCompanyName.MinimumWidth = 10;
            this.txt_GCompanyName.Name = "txt_GCompanyName";
            this.txt_GCompanyName.ReadOnly = true;
            // 
            // txt_DatabaseName
            // 
            this.txt_DatabaseName.HeaderText = "DataBase";
            this.txt_DatabaseName.Name = "txt_DatabaseName";
            this.txt_DatabaseName.ReadOnly = true;
            this.txt_DatabaseName.Visible = false;
            // 
            // txt_PanNo
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.txt_PanNo.DefaultCellStyle = dataGridViewCellStyle3;
            this.txt_PanNo.HeaderText = "Pan No";
            this.txt_PanNo.Name = "txt_PanNo";
            this.txt_PanNo.ReadOnly = true;
            this.txt_PanNo.Width = 150;
            // 
            // txtAddress
            // 
            this.txtAddress.HeaderText = "Address";
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.ReadOnly = true;
            this.txtAddress.Visible = false;
            this.txtAddress.Width = 220;
            // 
            // btn_NewCompany
            // 
            this.btn_NewCompany.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_NewCompany.Appearance.Options.UseFont = true;
            this.btn_NewCompany.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.btn_NewCompany.Location = new System.Drawing.Point(3, 479);
            this.btn_NewCompany.Name = "btn_NewCompany";
            this.btn_NewCompany.Size = new System.Drawing.Size(129, 38);
            this.btn_NewCompany.TabIndex = 0;
            this.btn_NewCompany.Text = "&ADD NEW";
            this.btn_NewCompany.ToolTip = "Add New Company";
            this.btn_NewCompany.Visible = false;
            this.btn_NewCompany.Click += new System.EventHandler(this.BtnNewCompany_Click);
            // 
            // TxtSearch
            // 
            this.TxtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSearch.CausesValidation = false;
            this.TxtSearch.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TxtSearch.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSearch.Location = new System.Drawing.Point(0, 518);
            this.TxtSearch.MaxLength = 200;
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(968, 26);
            this.TxtSearch.TabIndex = 0;
            this.TxtSearch.Visible = false;
            this.TxtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            this.TxtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtSearch_KeyPress);
            // 
            // FrmCompanyList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(968, 544);
            this.ControlBox = false;
            this.Controls.Add(this.TxtSearch);
            this.Controls.Add(this.btn_NewCompany);
            this.Controls.Add(this.RGrid);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = global::MrBLL.Properties.Resources.MrLogo;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCompanyList";
            this.ShowIcon = false;
            this.Text = "Company List";
            this.Load += new System.EventHandler(this.FrmCompanyList_Load);
            this.Shown += new System.EventHandler(this.FrmCompanyList_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCompanyList_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmCompanyList_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btn_NewCompany;
        private System.Windows.Forms.DataGridViewTextBoxColumn txt_GCompanyId;
        private System.Windows.Forms.DataGridViewTextBoxColumn txt_GCompanyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn txt_DatabaseName;
        private System.Windows.Forms.DataGridViewTextBoxColumn txt_PanNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtAddress;
        private MrTextBox TxtSearch;
        private EntryGridViewEx RGrid;
    }
}