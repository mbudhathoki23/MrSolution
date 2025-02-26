using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.POS.Master
{
    partial class FrmCounterTagList
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
            this.lbl_SearchValue = new System.Windows.Forms.Label();
            this.lbl_Search = new System.Windows.Forms.Label();
            this.SGrid = new System.Windows.Forms.DataGridView();
            this.PanelHeader = new MrPanel();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.clsSeparator2 = new ClsSeparator();
            this.GTxtLedgerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtShortName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtPrinter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.SGrid)).BeginInit();
            this.PanelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_SearchValue
            // 
            this.lbl_SearchValue.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lbl_SearchValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_SearchValue.Location = new System.Drawing.Point(73, 186);
            this.lbl_SearchValue.Name = "lbl_SearchValue";
            this.lbl_SearchValue.Size = new System.Drawing.Size(456, 23);
            this.lbl_SearchValue.TabIndex = 2;
            this.lbl_SearchValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_SearchValue.TextChanged += new System.EventHandler(this.lbl_SearchValue_TextChanged);
            // 
            // lbl_Search
            // 
            this.lbl_Search.AutoSize = true;
            this.lbl_Search.Location = new System.Drawing.Point(3, 186);
            this.lbl_Search.Name = "lbl_Search";
            this.lbl_Search.Size = new System.Drawing.Size(64, 20);
            this.lbl_Search.TabIndex = 21;
            this.lbl_Search.Text = "Search";
            // 
            // SGrid
            // 
            this.SGrid.AllowUserToAddRows = false;
            this.SGrid.AllowUserToDeleteRows = false;
            this.SGrid.AllowUserToResizeColumns = false;
            this.SGrid.AllowUserToResizeRows = false;
            this.SGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.SGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.SGrid.ColumnHeadersHeight = 30;
            this.SGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GTxtLedgerId,
            this.GTxtDescription,
            this.GTxtShortName,
            this.GTxtPrinter});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.SGrid.DefaultCellStyle = dataGridViewCellStyle1;
            this.SGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.SGrid.Location = new System.Drawing.Point(0, 0);
            this.SGrid.Margin = new System.Windows.Forms.Padding(2);
            this.SGrid.MultiSelect = false;
            this.SGrid.Name = "SGrid";
            this.SGrid.ReadOnly = true;
            this.SGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.SGrid.RowHeadersWidth = 30;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.SGrid.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.SGrid.RowTemplate.Height = 24;
            this.SGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.SGrid.Size = new System.Drawing.Size(532, 184);
            this.SGrid.TabIndex = 1;
            this.SGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SGrid_CellClick);
            this.SGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.SGrid_CellEnter);
            this.SGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.SGrid_RowEnter);
            this.SGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SGrid_KeyDown);
            this.SGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SGrid_KeyPress);
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.simpleButton2);
            this.PanelHeader.Controls.Add(this.simpleButton1);
            this.PanelHeader.Controls.Add(this.clsSeparator2);
            this.PanelHeader.Controls.Add(this.lbl_SearchValue);
            this.PanelHeader.Controls.Add(this.lbl_Search);
            this.PanelHeader.Controls.Add(this.SGrid);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(532, 252);
            this.PanelHeader.TabIndex = 40;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.ImageOptions.Image = global::MrBLL.Properties.Resources.logout;
            this.simpleButton2.Location = new System.Drawing.Point(267, 215);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(94, 33);
            this.simpleButton2.TabIndex = 23;
            this.simpleButton2.Text = "&LOGIN";
            this.simpleButton2.Click += new System.EventHandler(this.CmdOk_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.simpleButton1.Location = new System.Drawing.Point(362, 215);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(106, 33);
            this.simpleButton1.TabIndex = 22;
            this.simpleButton1.Text = "&CANCEL";
            this.simpleButton1.Click += new System.EventHandler(this.CmdCancel_Click);
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.Lavender;
            this.clsSeparator2.Location = new System.Drawing.Point(1, 211);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(524, 3);
            this.clsSeparator2.TabIndex = 12;
            this.clsSeparator2.TabStop = false;
            // 
            // GTxtLedgerId
            // 
            this.GTxtLedgerId.DataPropertyName = "LedgerId";
            this.GTxtLedgerId.HeaderText = "LedgerId";
            this.GTxtLedgerId.Name = "GTxtLedgerId";
            this.GTxtLedgerId.ReadOnly = true;
            this.GTxtLedgerId.Visible = false;
            // 
            // GTxtDescription
            // 
            this.GTxtDescription.DataPropertyName = "Description";
            this.GTxtDescription.HeaderText = "DESCRIPTION";
            this.GTxtDescription.Name = "GTxtDescription";
            this.GTxtDescription.ReadOnly = true;
            // 
            // GTxtShortName
            // 
            this.GTxtShortName.DataPropertyName = "ShortName";
            this.GTxtShortName.HeaderText = "SHORTNAME";
            this.GTxtShortName.Name = "GTxtShortName";
            this.GTxtShortName.ReadOnly = true;
            // 
            // GTxtPrinter
            // 
            this.GTxtPrinter.DataPropertyName = "Printer";
            this.GTxtPrinter.HeaderText = "PRINTER";
            this.GTxtPrinter.Name = "GTxtPrinter";
            this.GTxtPrinter.ReadOnly = true;
            // 
            // FrmCounterTagList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(532, 252);
            this.Controls.Add(this.PanelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmCounterTagList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Counter Login";
            this.Load += new System.EventHandler(this.FrmCounterTagList_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmCounterTagList_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.SGrid)).EndInit();
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl_SearchValue;
        private System.Windows.Forms.Label lbl_Search;
        private System.Windows.Forms.DataGridView SGrid;
        private System.Windows.Forms.Panel PanelHeader;
        private ClsSeparator clsSeparator2;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtLedgerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtShortName;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtPrinter;
    }
}