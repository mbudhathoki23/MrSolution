
using System.Windows.Forms;
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Utility.DataSync
{
    partial class FrmIrdSync
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
            this.ChkSelectAll = new System.Windows.Forms.CheckBox();
            this.BtnClose = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSync = new DevExpress.XtraEditors.SimpleButton();
            this.RGrid = new EntryGridViewEx();
            this.IsTrue = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.VoucherNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Customer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PanNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NetAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CmbSource = new MrComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mrPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // mrPanel1
            // 
            this.mrPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel1.Controls.Add(this.ChkSelectAll);
            this.mrPanel1.Controls.Add(this.BtnClose);
            this.mrPanel1.Controls.Add(this.BtnSync);
            this.mrPanel1.Controls.Add(this.RGrid);
            this.mrPanel1.Controls.Add(this.CmbSource);
            this.mrPanel1.Controls.Add(this.label1);
            this.mrPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrPanel1.Location = new System.Drawing.Point(0, 0);
            this.mrPanel1.Name = "mrPanel1";
            this.mrPanel1.Size = new System.Drawing.Size(1009, 528);
            this.mrPanel1.TabIndex = 0;
            // 
            // ChkSelectAll
            // 
            this.ChkSelectAll.AutoSize = true;
            this.ChkSelectAll.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSelectAll.Location = new System.Drawing.Point(6, 496);
            this.ChkSelectAll.Name = "ChkSelectAll";
            this.ChkSelectAll.Size = new System.Drawing.Size(100, 23);
            this.ChkSelectAll.TabIndex = 12;
            this.ChkSelectAll.Text = "Select All";
            this.ChkSelectAll.UseVisualStyleBackColor = true;
            this.ChkSelectAll.CheckedChanged += new System.EventHandler(this.ChkSelectAll_CheckedChanged);
            // 
            // BtnClose
            // 
            this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClose.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClose.Appearance.Options.UseFont = true;
            this.BtnClose.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnClose.Location = new System.Drawing.Point(898, 491);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(100, 32);
            this.BtnClose.TabIndex = 11;
            this.BtnClose.Text = "&CLOSE";
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // BtnSync
            // 
            this.BtnSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSync.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSync.Appearance.Options.UseFont = true;
            this.BtnSync.ImageOptions.Image = global::MrBLL.Properties.Resources.refresh;
            this.BtnSync.Location = new System.Drawing.Point(796, 491);
            this.BtnSync.Name = "BtnSync";
            this.BtnSync.Size = new System.Drawing.Size(101, 32);
            this.BtnSync.TabIndex = 10;
            this.BtnSync.Text = "&SYNC";
            this.BtnSync.Click += new System.EventHandler(this.BtnSync_Click);
            // 
            // RGrid
            // 
            this.RGrid.AllowUserToAddRows = false;
            this.RGrid.AllowUserToDeleteRows = false;
            this.RGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.RGrid.ColumnHeadersHeight = 30;
            this.RGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.RGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IsTrue,
            this.VoucherNo,
            this.Customer,
            this.PanNo,
            this.NetAmount});
            this.RGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.RGrid.Location = new System.Drawing.Point(4, 40);
            this.RGrid.MultiSelect = false;
            this.RGrid.Name = "RGrid";
            this.RGrid.ReadOnly = true;
            this.RGrid.RowHeadersWidth = 25;
            this.RGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RGrid.Size = new System.Drawing.Size(998, 445);
            this.RGrid.TabIndex = 2;
            this.RGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_CellContentClick);
            // 
            // IsTrue
            // 
            this.IsTrue.DataPropertyName = "Check";
            this.IsTrue.HeaderText = "Check";
            this.IsTrue.Name = "IsTrue";
            this.IsTrue.ReadOnly = true;
            this.IsTrue.Width = 65;
            // 
            // VoucherNo
            // 
            this.VoucherNo.DataPropertyName = "VoucherNo";
            this.VoucherNo.HeaderText = "VoucherNo";
            this.VoucherNo.Name = "VoucherNo";
            this.VoucherNo.ReadOnly = true;
            this.VoucherNo.Width = 150;
            // 
            // Customer
            // 
            this.Customer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Customer.DataPropertyName = "Customer";
            this.Customer.HeaderText = "Customer";
            this.Customer.Name = "Customer";
            this.Customer.ReadOnly = true;
            // 
            // PanNo
            // 
            this.PanNo.DataPropertyName = "PanNo";
            this.PanNo.HeaderText = "PanNo";
            this.PanNo.Name = "PanNo";
            this.PanNo.ReadOnly = true;
            this.PanNo.Width = 120;
            // 
            // NetAmount
            // 
            this.NetAmount.DataPropertyName = "NetAmount";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.NetAmount.DefaultCellStyle = dataGridViewCellStyle1;
            this.NetAmount.HeaderText = "Net Amount";
            this.NetAmount.Name = "NetAmount";
            this.NetAmount.ReadOnly = true;
            this.NetAmount.Width = 150;
            // 
            // CmbSource
            // 
            this.CmbSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbSource.FormattingEnabled = true;
            this.CmbSource.Items.AddRange(new object[] {
            "Sales Invoice",
            "Sales Return",
            "Sales Cancel",
            "Sales Return Cancel"});
            this.CmbSource.Location = new System.Drawing.Point(74, 5);
            this.CmbSource.Name = "CmbSource";
            this.CmbSource.Size = new System.Drawing.Size(281, 27);
            this.CmbSource.TabIndex = 1;
            this.CmbSource.SelectedIndexChanged += new System.EventHandler(this.CmbSource_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source";
            // 
            // FrmIrdSync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 528);
            this.Controls.Add(this.mrPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmIrdSync";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SYNC INVOICE TO IRD";
            this.Load += new System.EventHandler(this.FrmIrdSync_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmIrdSync_KeyPress);
            this.mrPanel1.ResumeLayout(false);
            this.mrPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MrPanel mrPanel1;
        private MrComboBox CmbSource;
        private System.Windows.Forms.Label label1;
        private DataGridView RGrid;
        private DevExpress.XtraEditors.SimpleButton BtnClose;
        private DevExpress.XtraEditors.SimpleButton BtnSync;
        private System.Windows.Forms.CheckBox ChkSelectAll;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsTrue;
        private System.Windows.Forms.DataGridViewTextBoxColumn VoucherNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Customer;
        private System.Windows.Forms.DataGridViewTextBoxColumn PanNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn NetAmount;
    }
}