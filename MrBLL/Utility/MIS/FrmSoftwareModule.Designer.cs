using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Utility.MIS
{
    partial class FrmSoftwareModule
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
            this.PanelHeader = new MrPanel();
            this.DGrid = new System.Windows.Forms.DataGridView();
            this.roundPanel1 = new RoundPanel();
            this.PanelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).BeginInit();
            this.roundPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.roundPanel1);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(748, 400);
            this.PanelHeader.TabIndex = 40;
            // 
            // DGrid
            // 
            this.DGrid.AllowUserToAddRows = false;
            this.DGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.DGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.DGrid.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.DGrid.Location = new System.Drawing.Point(3, 22);
            this.DGrid.Margin = new System.Windows.Forms.Padding(2);
            this.DGrid.MultiSelect = false;
            this.DGrid.Name = "DGrid";
            this.DGrid.ReadOnly = true;
            this.DGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.DGrid.RowHeadersWidth = 20;
            this.DGrid.RowTemplate.Height = 24;
            this.DGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGrid.Size = new System.Drawing.Size(742, 375);
            this.DGrid.TabIndex = 22;
            this.DGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGrid_CellContentClick);
            this.DGrid.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGrid_CellContentDoubleClick);
            this.DGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGrid_CellEnter);
            this.DGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGrid_RowEnter);
            this.DGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DGrid_KeyDown);
            // 
            // roundPanel1
            // 
            this.roundPanel1.BackColor = System.Drawing.Color.Transparent;
            this.roundPanel1.Controls.Add(this.DGrid);
            this.roundPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roundPanel1.Location = new System.Drawing.Point(0, 0);
            this.roundPanel1.Name = "roundPanel1";
            this.roundPanel1.Radious = 25;
            this.roundPanel1.Size = new System.Drawing.Size(748, 400);
            this.roundPanel1.TabIndex = 23;
            this.roundPanel1.TabStop = false;
            this.roundPanel1.Text = "SOFTWARE MODULE";
            this.roundPanel1.TitleBackColor = System.Drawing.Color.DarkSlateBlue;
            this.roundPanel1.TitleFont = new System.Drawing.Font("Bookman Old Style", 10F);
            this.roundPanel1.TitleForeColor = System.Drawing.Color.White;
            this.roundPanel1.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent50;
            // 
            // FrmSoftwareModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 400);
            this.ControlBox = false;
            this.Controls.Add(this.PanelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Name = "FrmSoftwareModule";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SOFTWARE MODULE";
            this.Load += new System.EventHandler(this.FrmSoftwareModule_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmSoftwareModule_KeyPress);
            this.PanelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).EndInit();
            this.roundPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private RoundPanel roundPanel1;
        private System.Windows.Forms.DataGridView DGrid;
        private MrPanel PanelHeader;
    }
}