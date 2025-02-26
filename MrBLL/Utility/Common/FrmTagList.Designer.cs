using System.Windows.Forms;
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Utility.Common
{
    sealed partial class FrmTagList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTagList));
            this.SGrid = new EntryGridViewEx();
            this.CmdCancel = new DevExpress.XtraEditors.SimpleButton();
            this.CmdOk = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Find = new DevExpress.XtraEditors.SimpleButton();
            this.rb_UnTagAll = new System.Windows.Forms.RadioButton();
            this.rb_TagAll = new System.Windows.Forms.RadioButton();
            this.TxtFindValue = new MrTextBox();
            this.lbl_Find = new System.Windows.Forms.Label();
            this.TxtSearch = new MrTextBox();
            this.lbl_Search = new System.Windows.Forms.Label();
            this.clsSeparator3 = new ClsSeparator();
            this.clsSeparator2 = new ClsSeparator();
            this.clsSeparator1 = new ClsSeparator();
            this.PanelHeader = new MrPanel();
            ((System.ComponentModel.ISupportInitialize)(this.SGrid)).BeginInit();
            this.PanelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // SGrid
            // 
            this.SGrid.AllowUserToAddRows = false;
            this.SGrid.AllowUserToDeleteRows = false;
            this.SGrid.AllowUserToResizeColumns = false;
            this.SGrid.AllowUserToResizeRows = false;
            this.SGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.SGrid.BlockNavigationOnNextRowOnEnter = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.InfoText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.SGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.SGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SGrid.DoubleBufferEnabled = true;
            this.SGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.SGrid.Location = new System.Drawing.Point(0, 0);
            this.SGrid.Margin = new System.Windows.Forms.Padding(2);
            this.SGrid.Name = "SGrid";
            this.SGrid.ReadOnly = true;
            this.SGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.SGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.SGrid.RowHeadersVisible = false;
            this.SGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SGrid.RowTemplate.Height = 24;
            this.SGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.SGrid.Size = new System.Drawing.Size(1003, 478);
            this.SGrid.StandardTab = true;
            this.SGrid.TabIndex = 0;
            this.SGrid.EnterKeyPressed += new System.EventHandler<System.EventArgs>(this.SGrid_EnterKeyPressed);
            this.SGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SGrid_CellClick);
            this.SGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.SGrid_CellEnter);
            this.SGrid.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.SGrid_CellMouseDoubleClick);
            this.SGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.SGrid_RowEnter);
            this.SGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SGrid_KeyDown);
            // 
            // CmdCancel
            // 
            this.CmdCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmdCancel.Appearance.Options.UseFont = true;
            this.CmdCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.CmdCancel.Location = new System.Drawing.Point(514, 79);
            this.CmdCancel.Name = "CmdCancel";
            this.CmdCancel.Size = new System.Drawing.Size(104, 34);
            this.CmdCancel.TabIndex = 5;
            this.CmdCancel.Text = "&CANCEL";
            this.CmdCancel.Click += new System.EventHandler(this.CmdCancel_Click);
            // 
            // CmdOk
            // 
            this.CmdOk.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmdOk.Appearance.Options.UseFont = true;
            this.CmdOk.ImageOptions.Image = global::MrBLL.Properties.Resources.OK;
            this.CmdOk.Location = new System.Drawing.Point(399, 79);
            this.CmdOk.Name = "CmdOk";
            this.CmdOk.Size = new System.Drawing.Size(114, 34);
            this.CmdOk.TabIndex = 4;
            this.CmdOk.Text = "&OK";
            this.CmdOk.Click += new System.EventHandler(this.CmdOk_Click);
            // 
            // btn_Find
            // 
            this.btn_Find.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Find.Appearance.Options.UseFont = true;
            this.btn_Find.ImageOptions.Image = global::MrBLL.Properties.Resources.search16;
            this.btn_Find.Location = new System.Drawing.Point(554, 41);
            this.btn_Find.Name = "btn_Find";
            this.btn_Find.Size = new System.Drawing.Size(72, 25);
            this.btn_Find.TabIndex = 3;
            this.btn_Find.Text = "&Find";
            this.btn_Find.Click += new System.EventHandler(this.BtnFind_Click);
            // 
            // rb_UnTagAll
            // 
            this.rb_UnTagAll.AutoSize = true;
            this.rb_UnTagAll.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_UnTagAll.Location = new System.Drawing.Point(126, 86);
            this.rb_UnTagAll.Name = "rb_UnTagAll";
            this.rb_UnTagAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rb_UnTagAll.Size = new System.Drawing.Size(121, 23);
            this.rb_UnTagAll.TabIndex = 7;
            this.rb_UnTagAll.TabStop = true;
            this.rb_UnTagAll.Text = "&UnSelect All";
            this.rb_UnTagAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rb_UnTagAll.UseVisualStyleBackColor = true;
            this.rb_UnTagAll.CheckedChanged += new System.EventHandler(this.rChkUnTagAll_CheckedChanged);
            // 
            // rb_TagAll
            // 
            this.rb_TagAll.AutoSize = true;
            this.rb_TagAll.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_TagAll.Location = new System.Drawing.Point(10, 86);
            this.rb_TagAll.Name = "rb_TagAll";
            this.rb_TagAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rb_TagAll.Size = new System.Drawing.Size(99, 23);
            this.rb_TagAll.TabIndex = 6;
            this.rb_TagAll.TabStop = true;
            this.rb_TagAll.Text = "Select &All";
            this.rb_TagAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rb_TagAll.UseVisualStyleBackColor = true;
            this.rb_TagAll.CheckedChanged += new System.EventHandler(this.rChkTagAll_CheckedChanged);
            // 
            // TxtFindValue
            // 
            this.TxtFindValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtFindValue.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtFindValue.Location = new System.Drawing.Point(79, 41);
            this.TxtFindValue.MaxLength = 255;
            this.TxtFindValue.Name = "TxtFindValue";
            this.TxtFindValue.Size = new System.Drawing.Size(473, 25);
            this.TxtFindValue.TabIndex = 2;
            this.TxtFindValue.Enter += new System.EventHandler(this.TxtFindValue_Enter);
            this.TxtFindValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtFindValue_KeyDown);
            this.TxtFindValue.Leave += new System.EventHandler(this.TxtFindValue_Leave);
            // 
            // lbl_Find
            // 
            this.lbl_Find.AutoSize = true;
            this.lbl_Find.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Find.Location = new System.Drawing.Point(11, 44);
            this.lbl_Find.Name = "lbl_Find";
            this.lbl_Find.Size = new System.Drawing.Size(43, 19);
            this.lbl_Find.TabIndex = 33;
            this.lbl_Find.Text = "Find";
            // 
            // TxtSearch
            // 
            this.TxtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtSearch.BackColor = System.Drawing.SystemColors.Info;
            this.TxtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSearch.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSearch.ForeColor = System.Drawing.Color.Red;
            this.TxtSearch.Location = new System.Drawing.Point(79, 10);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(915, 25);
            this.TxtSearch.TabIndex = 1;
            this.TxtSearch.TextChanged += new System.EventHandler(this.LblSearchValue_TextChanged);
            this.TxtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtSearch_KeyPress);
            // 
            // lbl_Search
            // 
            this.lbl_Search.AutoSize = true;
            this.lbl_Search.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Search.Location = new System.Drawing.Point(11, 15);
            this.lbl_Search.Name = "lbl_Search";
            this.lbl_Search.Size = new System.Drawing.Size(62, 19);
            this.lbl_Search.TabIndex = 30;
            this.lbl_Search.Text = "Search";
            // 
            // clsSeparator3
            // 
            this.clsSeparator3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clsSeparator3.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator3.Location = new System.Drawing.Point(10, 115);
            this.clsSeparator3.Name = "clsSeparator3";
            this.clsSeparator3.Size = new System.Drawing.Size(988, 2);
            this.clsSeparator3.TabIndex = 41;
            this.clsSeparator3.TabStop = false;
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clsSeparator2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator2.Location = new System.Drawing.Point(8, 73);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(986, 2);
            this.clsSeparator2.TabIndex = 40;
            this.clsSeparator2.TabStop = false;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clsSeparator1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator1.Location = new System.Drawing.Point(8, 4);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(986, 2);
            this.clsSeparator1.TabIndex = 39;
            this.clsSeparator1.TabStop = false;
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.clsSeparator3);
            this.PanelHeader.Controls.Add(this.clsSeparator2);
            this.PanelHeader.Controls.Add(this.clsSeparator1);
            this.PanelHeader.Controls.Add(this.TxtSearch);
            this.PanelHeader.Controls.Add(this.CmdCancel);
            this.PanelHeader.Controls.Add(this.lbl_Search);
            this.PanelHeader.Controls.Add(this.CmdOk);
            this.PanelHeader.Controls.Add(this.lbl_Find);
            this.PanelHeader.Controls.Add(this.btn_Find);
            this.PanelHeader.Controls.Add(this.TxtFindValue);
            this.PanelHeader.Controls.Add(this.rb_UnTagAll);
            this.PanelHeader.Controls.Add(this.rb_TagAll);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 478);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(1003, 122);
            this.PanelHeader.TabIndex = 42;
            // 
            // FrmTagList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1003, 600);
            this.Controls.Add(this.SGrid);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTagList";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "TAG LIST";
            this.Load += new System.EventHandler(this.FrmTagList_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmTagList_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.SGrid)).EndInit();
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton CmdCancel;
        private DevExpress.XtraEditors.SimpleButton CmdOk;
        private DevExpress.XtraEditors.SimpleButton btn_Find;
        private System.Windows.Forms.RadioButton rb_UnTagAll;
        private System.Windows.Forms.RadioButton rb_TagAll;
        private System.Windows.Forms.Label lbl_Find;
        private System.Windows.Forms.Label lbl_Search;
        private ClsSeparator clsSeparator1;
        private ClsSeparator clsSeparator2;
        private ClsSeparator clsSeparator3;
        private MrTextBox TxtFindValue;
        private MrTextBox TxtSearch;
        private MrPanel PanelHeader;
        private EntryGridViewEx SGrid;
    }
}