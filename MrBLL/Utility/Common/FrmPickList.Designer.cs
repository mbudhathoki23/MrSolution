using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Utility.Common
{
    partial class FrmPickList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPickList));
            this.lbl_SearchValue = new System.Windows.Forms.Label();
            this.TxtFindValue = new MrTextBox();
            this.lbl_Find = new System.Windows.Forms.Label();
            this.lbl_Search = new System.Windows.Forms.Label();
            this.RGrid = new System.Windows.Forms.DataGridView();
            this.btn_Find = new DevExpress.XtraEditors.SimpleButton();
            this.CmdOk = new DevExpress.XtraEditors.SimpleButton();
            this.CmdCancel = new DevExpress.XtraEditors.SimpleButton();
            this.PanelHeader = new MrPanel();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).BeginInit();
            this.PanelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_SearchValue
            // 
            this.lbl_SearchValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_SearchValue.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lbl_SearchValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_SearchValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SearchValue.Location = new System.Drawing.Point(73, 462);
            this.lbl_SearchValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_SearchValue.Name = "lbl_SearchValue";
            this.lbl_SearchValue.Size = new System.Drawing.Size(818, 22);
            this.lbl_SearchValue.TabIndex = 22;
            this.lbl_SearchValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_SearchValue.TextChanged += new System.EventHandler(this.lbl_SearchValue_TextChanged);
            // 
            // TxtFindValue
            // 
            this.TxtFindValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtFindValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtFindValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtFindValue.Location = new System.Drawing.Point(73, 487);
            this.TxtFindValue.Margin = new System.Windows.Forms.Padding(4);
            this.TxtFindValue.MaxLength = 255;
            this.TxtFindValue.Name = "TxtFindValue";
            this.TxtFindValue.Size = new System.Drawing.Size(818, 22);
            this.TxtFindValue.TabIndex = 23;
            this.TxtFindValue.Enter += new System.EventHandler(this.TxtFindValue_Enter);
            this.TxtFindValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtFindValue_KeyDown);
            this.TxtFindValue.Leave += new System.EventHandler(this.TxtFindValue_Leave);
            // 
            // lbl_Find
            // 
            this.lbl_Find.AutoSize = true;
            this.lbl_Find.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Find.Location = new System.Drawing.Point(7, 489);
            this.lbl_Find.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Find.Name = "lbl_Find";
            this.lbl_Find.Size = new System.Drawing.Size(43, 19);
            this.lbl_Find.TabIndex = 28;
            this.lbl_Find.Text = "&Find";
            // 
            // lbl_Search
            // 
            this.lbl_Search.AutoSize = true;
            this.lbl_Search.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Search.Location = new System.Drawing.Point(7, 463);
            this.lbl_Search.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Search.Name = "lbl_Search";
            this.lbl_Search.Size = new System.Drawing.Size(62, 19);
            this.lbl_Search.TabIndex = 27;
            this.lbl_Search.Text = "Search";
            // 
            // RGrid
            // 
            this.RGrid.AllowUserToAddRows = false;
            this.RGrid.AllowUserToDeleteRows = false;
            this.RGrid.AllowUserToResizeRows = false;
            this.RGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.RGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.RGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.RGrid.Location = new System.Drawing.Point(0, 0);
            this.RGrid.MultiSelect = false;
            this.RGrid.Name = "RGrid";
            this.RGrid.ReadOnly = true;
            this.RGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.RGrid.RowHeadersVisible = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            this.RGrid.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.RGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RGrid.RowTemplate.Height = 24;
            this.RGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RGrid.Size = new System.Drawing.Size(895, 459);
            this.RGrid.TabIndex = 0;
            this.RGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_PickList_CellEnter);
            this.RGrid.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_PickList_CellMouseDoubleClick);
            this.RGrid.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgv_PickList_DataBindingComplete);
            this.RGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_PickList_RowEnter);
            this.RGrid.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_PickList_CellMouseDoubleClick);
            this.RGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_PickList_KeyDown);
            // 
            // btn_Find
            // 
            this.btn_Find.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Find.Appearance.Options.UseFont = true;
            this.btn_Find.Location = new System.Drawing.Point(230, 513);
            this.btn_Find.Name = "btn_Find";
            this.btn_Find.Size = new System.Drawing.Size(107, 33);
            this.btn_Find.TabIndex = 29;
            this.btn_Find.Text = "&FIND";
            this.btn_Find.Click += new System.EventHandler(this.btn_Find_Click);
            // 
            // CmdOk
            // 
            this.CmdOk.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmdOk.Appearance.Options.UseFont = true;
            this.CmdOk.Location = new System.Drawing.Point(343, 513);
            this.CmdOk.Name = "CmdOk";
            this.CmdOk.Size = new System.Drawing.Size(83, 33);
            this.CmdOk.TabIndex = 30;
            this.CmdOk.Text = "&OK";
            this.CmdOk.Click += new System.EventHandler(this.CmdOk_Click);
            // 
            // CmdCancel
            // 
            this.CmdCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmdCancel.Appearance.Options.UseFont = true;
            this.CmdCancel.Location = new System.Drawing.Point(428, 513);
            this.CmdCancel.Name = "CmdCancel";
            this.CmdCancel.Size = new System.Drawing.Size(105, 33);
            this.CmdCancel.TabIndex = 31;
            this.CmdCancel.Text = "&CANCEL";
            this.CmdCancel.Click += new System.EventHandler(this.CmdCancel_Click);
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.CmdCancel);
            this.PanelHeader.Controls.Add(this.CmdOk);
            this.PanelHeader.Controls.Add(this.btn_Find);
            this.PanelHeader.Controls.Add(this.TxtFindValue);
            this.PanelHeader.Controls.Add(this.lbl_Find);
            this.PanelHeader.Controls.Add(this.lbl_SearchValue);
            this.PanelHeader.Controls.Add(this.RGrid);
            this.PanelHeader.Controls.Add(this.lbl_Search);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(895, 553);
            this.PanelHeader.TabIndex = 40;
            // 
            // FrmPickList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(895, 553);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPickList";
            this.ShowIcon = false;
            this.Text = "Pick List";
            this.Load += new System.EventHandler(this.FrmPickList_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmPickList_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).EndInit();
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl_Find;
        private System.Windows.Forms.Label lbl_Search;
        private System.Windows.Forms.DataGridView RGrid;
        private DevExpress.XtraEditors.SimpleButton btn_Find;
        private DevExpress.XtraEditors.SimpleButton CmdOk;
        private DevExpress.XtraEditors.SimpleButton CmdCancel;
        public System.Windows.Forms.Label lbl_SearchValue;
        private MrTextBox TxtFindValue;
        private MrPanel PanelHeader;
    }
}