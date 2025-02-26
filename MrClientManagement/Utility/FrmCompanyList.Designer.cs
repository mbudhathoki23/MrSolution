using MrDAL.Control.ControlsEx.Control;

namespace MrClientManagement.Utility
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.RGrid = new System.Windows.Forms.DataGridView();
            this.TxtSearch = new MrTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.RGrid);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(831, 455);
            this.panelControl1.TabIndex = 0;
            // 
            // RGrid
            // 
            this.RGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.RGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.RGrid.DefaultCellStyle = dataGridViewCellStyle1;
            this.RGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RGrid.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.RGrid.Location = new System.Drawing.Point(2, 2);
            this.RGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RGrid.Name = "RGrid";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.RGrid.RowHeadersWidth = 20;
            this.RGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.RGrid.RowTemplate.Height = 24;
            this.RGrid.Size = new System.Drawing.Size(827, 451);
            this.RGrid.TabIndex = 0;
            this.RGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_CellContentClick);
            this.RGrid.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_CellContentDoubleClick);
            this.RGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_CellEnter);
            this.RGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_RowEnter);
            this.RGrid.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.RGrid_RowHeaderMouseDoubleClick);
            this.RGrid.Sorted += new System.EventHandler(this.RGrid_Sorted);
            this.RGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RGrid_KeyDown);
            // 
            // TxtSearch
            // 
            this.TxtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSearch.CausesValidation = false;
            this.TxtSearch.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TxtSearch.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSearch.Location = new System.Drawing.Point(0, 429);
            this.TxtSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TxtSearch.MaxLength = 200;
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(831, 26);
            this.TxtSearch.TabIndex = 1;
            this.TxtSearch.Visible = false;
            this.TxtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            this.TxtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtSearch_KeyPress);
            // 
            // FrmCompanyList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 455);
            this.Controls.Add(this.TxtSearch);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCompanyList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "COMPANY LIST";
            this.Load += new System.EventHandler(this.FrmCompanyList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.DataGridView RGrid;
        private MrTextBox TxtSearch;
    }
}