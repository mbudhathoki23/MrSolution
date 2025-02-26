namespace MrBLL.Domains.Restro.Entry
{
    partial class FrmSplitTable
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
            this.mrPanel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.mrGroup2 = new MrDAL.Control.ControlsEx.Control.MrGroup();
            this.BtnAvailable = new System.Windows.Forms.Button();
            this.TxtAvailableTable = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.RGridSplit = new MrDAL.Control.ControlsEx.Control.MrDataGridView();
            this.mrGroup1 = new MrDAL.Control.ControlsEx.Control.MrGroup();
            this.BtnOccupied = new System.Windows.Forms.Button();
            this.TxtOccupiedTable = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.RGridOccupied = new MrDAL.Control.ControlsEx.Control.MrDataGridView();
            this.mrGroup3 = new MrDAL.Control.ControlsEx.Control.MrGroup();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSplit = new DevExpress.XtraEditors.SimpleButton();
            this.mrPanel1.SuspendLayout();
            this.mrGroup2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGridSplit)).BeginInit();
            this.mrGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGridOccupied)).BeginInit();
            this.mrGroup3.SuspendLayout();
            this.SuspendLayout();
            // 
            // mrPanel1
            // 
            this.mrPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel1.Controls.Add(this.mrGroup2);
            this.mrPanel1.Controls.Add(this.mrGroup1);
            this.mrPanel1.Controls.Add(this.mrGroup3);
            this.mrPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrPanel1.Location = new System.Drawing.Point(0, 0);
            this.mrPanel1.Name = "mrPanel1";
            this.mrPanel1.Size = new System.Drawing.Size(1054, 632);
            this.mrPanel1.TabIndex = 0;
            // 
            // mrGroup2
            // 
            this.mrGroup2.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup2.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup2.BackgroundGradientMode = MrDAL.Control.ControlsEx.Control.MrGroup.GroupBoxGradientMode.None;
            this.mrGroup2.BorderColor = System.Drawing.Color.White;
            this.mrGroup2.BorderThickness = 1F;
            this.mrGroup2.Controls.Add(this.BtnAvailable);
            this.mrGroup2.Controls.Add(this.TxtAvailableTable);
            this.mrGroup2.Controls.Add(this.RGridSplit);
            this.mrGroup2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup2.Dock = System.Windows.Forms.DockStyle.Right;
            this.mrGroup2.GroupImage = null;
            this.mrGroup2.GroupTitle = "SPLIT TABLE";
            this.mrGroup2.Location = new System.Drawing.Point(528, 0);
            this.mrGroup2.Name = "mrGroup2";
            this.mrGroup2.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup2.PaintGroupBox = false;
            this.mrGroup2.RoundCorners = 10;
            this.mrGroup2.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup2.ShadowControl = false;
            this.mrGroup2.ShadowThickness = 3;
            this.mrGroup2.Size = new System.Drawing.Size(526, 571);
            this.mrGroup2.TabIndex = 1;
            // 
            // BtnAvailable
            // 
            this.BtnAvailable.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnAvailable.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnAvailable.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnAvailable.Location = new System.Drawing.Point(486, 30);
            this.BtnAvailable.Name = "BtnAvailable";
            this.BtnAvailable.Size = new System.Drawing.Size(28, 26);
            this.BtnAvailable.TabIndex = 311;
            this.BtnAvailable.TabStop = false;
            this.BtnAvailable.UseVisualStyleBackColor = false;
            this.BtnAvailable.Click += new System.EventHandler(this.BtnAvailable_Click);
            // 
            // TxtAvailableTable
            // 
            this.TxtAvailableTable.BackColor = System.Drawing.Color.White;
            this.TxtAvailableTable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAvailableTable.Location = new System.Drawing.Point(20, 31);
            this.TxtAvailableTable.Name = "TxtAvailableTable";
            this.TxtAvailableTable.ReadOnly = true;
            this.TxtAvailableTable.Size = new System.Drawing.Size(464, 25);
            this.TxtAvailableTable.TabIndex = 1;
            this.TxtAvailableTable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtAvailableTable_KeyDown);
            // 
            // RGridSplit
            // 
            this.RGridSplit.AllowUserToAddRows = false;
            this.RGridSplit.AllowUserToDeleteRows = false;
            this.RGridSplit.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.RGridSplit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RGridSplit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.RGridSplit.DoubleBufferEnabled = true;
            this.RGridSplit.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.RGridSplit.Location = new System.Drawing.Point(20, 62);
            this.RGridSplit.MultiSelect = false;
            this.RGridSplit.Name = "RGridSplit";
            this.RGridSplit.ReadOnly = true;
            this.RGridSplit.RowHeadersWidth = 25;
            this.RGridSplit.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RGridSplit.Size = new System.Drawing.Size(486, 489);
            this.RGridSplit.TabIndex = 2;
            this.RGridSplit.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGridSplit_CellContentDoubleClick);
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrDAL.Control.ControlsEx.Control.MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.BtnOccupied);
            this.mrGroup1.Controls.Add(this.TxtOccupiedTable);
            this.mrGroup1.Controls.Add(this.RGridOccupied);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.Dock = System.Windows.Forms.DockStyle.Left;
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "OCCUPIED TABLE";
            this.mrGroup1.Location = new System.Drawing.Point(0, 0);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 10;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = false;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(522, 571);
            this.mrGroup1.TabIndex = 0;
            // 
            // BtnOccupied
            // 
            this.BtnOccupied.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnOccupied.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnOccupied.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnOccupied.Location = new System.Drawing.Point(474, 30);
            this.BtnOccupied.Name = "BtnOccupied";
            this.BtnOccupied.Size = new System.Drawing.Size(28, 26);
            this.BtnOccupied.TabIndex = 310;
            this.BtnOccupied.TabStop = false;
            this.BtnOccupied.UseVisualStyleBackColor = false;
            this.BtnOccupied.Click += new System.EventHandler(this.BtnOccupied_Click);
            // 
            // TxtOccupiedTable
            // 
            this.TxtOccupiedTable.BackColor = System.Drawing.Color.White;
            this.TxtOccupiedTable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtOccupiedTable.Location = new System.Drawing.Point(20, 31);
            this.TxtOccupiedTable.Name = "TxtOccupiedTable";
            this.TxtOccupiedTable.ReadOnly = true;
            this.TxtOccupiedTable.Size = new System.Drawing.Size(451, 25);
            this.TxtOccupiedTable.TabIndex = 0;
            this.TxtOccupiedTable.TextChanged += new System.EventHandler(this.TxtOccupiedTable_TextChanged);
            this.TxtOccupiedTable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtOccupiedTable_KeyDown);
            // 
            // RGridOccupied
            // 
            this.RGridOccupied.AllowUserToAddRows = false;
            this.RGridOccupied.AllowUserToDeleteRows = false;
            this.RGridOccupied.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.RGridOccupied.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RGridOccupied.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.RGridOccupied.DoubleBufferEnabled = true;
            this.RGridOccupied.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.RGridOccupied.Location = new System.Drawing.Point(20, 62);
            this.RGridOccupied.MultiSelect = false;
            this.RGridOccupied.Name = "RGridOccupied";
            this.RGridOccupied.ReadOnly = true;
            this.RGridOccupied.RowHeadersWidth = 25;
            this.RGridOccupied.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RGridOccupied.Size = new System.Drawing.Size(482, 489);
            this.RGridOccupied.TabIndex = 0;
            this.RGridOccupied.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGridOccupied_CellContentDoubleClick);
            this.RGridOccupied.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGridOccupied_CellContentDoubleClick);
            // 
            // mrGroup3
            // 
            this.mrGroup3.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup3.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup3.BackgroundGradientMode = MrDAL.Control.ControlsEx.Control.MrGroup.GroupBoxGradientMode.None;
            this.mrGroup3.BorderColor = System.Drawing.Color.White;
            this.mrGroup3.BorderThickness = 1F;
            this.mrGroup3.Controls.Add(this.BtnCancel);
            this.mrGroup3.Controls.Add(this.BtnSplit);
            this.mrGroup3.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mrGroup3.GroupImage = null;
            this.mrGroup3.GroupTitle = "";
            this.mrGroup3.Location = new System.Drawing.Point(0, 571);
            this.mrGroup3.Name = "mrGroup3";
            this.mrGroup3.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup3.PaintGroupBox = false;
            this.mrGroup3.RoundCorners = 10;
            this.mrGroup3.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup3.ShadowControl = false;
            this.mrGroup3.ShadowThickness = 3;
            this.mrGroup3.Size = new System.Drawing.Size(1054, 61);
            this.mrGroup3.TabIndex = 311;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.BtnCancel.Location = new System.Drawing.Point(879, 16);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(104, 39);
            this.BtnCancel.TabIndex = 13;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSplit
            // 
            this.BtnSplit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSplit.Appearance.Options.UseFont = true;
            this.BtnSplit.ImageOptions.Image = global::MrBLL.Properties.Resources.Reverse;
            this.BtnSplit.Location = new System.Drawing.Point(774, 16);
            this.BtnSplit.Name = "BtnSplit";
            this.BtnSplit.Size = new System.Drawing.Size(103, 39);
            this.BtnSplit.TabIndex = 12;
            this.BtnSplit.Text = "&SPLIT";
            this.BtnSplit.Click += new System.EventHandler(this.BtnSplit_Click);
            // 
            // FrmSplitTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 632);
            this.Controls.Add(this.mrPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSplitTable";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SPLIT TABLE";
            this.Load += new System.EventHandler(this.FrmSplitTable_Load);
            this.mrPanel1.ResumeLayout(false);
            this.mrGroup2.ResumeLayout(false);
            this.mrGroup2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGridSplit)).EndInit();
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGridOccupied)).EndInit();
            this.mrGroup3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MrDAL.Control.ControlsEx.Control.MrPanel mrPanel1;
        private MrDAL.Control.ControlsEx.Control.MrGroup mrGroup2;
        private MrDAL.Control.ControlsEx.Control.MrGroup mrGroup1;
        private MrDAL.Control.ControlsEx.Control.MrDataGridView RGridOccupied;
        private System.Windows.Forms.Button BtnOccupied;
        private MrDAL.Control.ControlsEx.Control.MrTextBox TxtOccupiedTable;
        private System.Windows.Forms.Button BtnAvailable;
        private MrDAL.Control.ControlsEx.Control.MrTextBox TxtAvailableTable;
        private MrDAL.Control.ControlsEx.Control.MrDataGridView RGridSplit;
        private MrDAL.Control.ControlsEx.Control.MrGroup mrGroup3;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSplit;
    }
}