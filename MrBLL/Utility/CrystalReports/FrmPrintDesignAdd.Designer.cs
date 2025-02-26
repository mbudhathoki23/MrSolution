using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Utility.CrystalReports
{
    partial class FrmPrintDesignAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrintDesignAdd));
            this.lbl_UserDefineFields = new System.Windows.Forms.Label();
            this.CmbModule = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CmbType = new System.Windows.Forms.ComboBox();
            this.LblFileName = new System.Windows.Forms.Label();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.LblLocation = new System.Windows.Forms.Label();
            this.lnkBrowseFile = new System.Windows.Forms.LinkLabel();
            this.TxtDesignName = new MrTextBox();
            this.lbl_DesignName = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.RGrid = new System.Windows.Forms.DataGridView();
            this.txt_dgvDocDesignId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_dgvModule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_dgvType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_dgvDesignName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_dgvDesignPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_BtnEdit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_BtnDelete = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_UserDefineFields
            // 
            this.lbl_UserDefineFields.AutoSize = true;
            this.lbl_UserDefineFields.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbl_UserDefineFields.Location = new System.Drawing.Point(9, 20);
            this.lbl_UserDefineFields.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_UserDefineFields.Name = "lbl_UserDefineFields";
            this.lbl_UserDefineFields.Size = new System.Drawing.Size(77, 25);
            this.lbl_UserDefineFields.TabIndex = 208;
            this.lbl_UserDefineFields.Text = "Module";
            // 
            // CmbModule
            // 
            this.CmbModule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbModule.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbModule.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.CmbModule.FormattingEnabled = true;
            this.CmbModule.Location = new System.Drawing.Point(175, 15);
            this.CmbModule.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CmbModule.Name = "CmbModule";
            this.CmbModule.Size = new System.Drawing.Size(416, 33);
            this.CmbModule.TabIndex = 0;
            this.CmbModule.SelectedIndexChanged += new System.EventHandler(this.CmbModuleName_SelectedIndexChanged);
            this.CmbModule.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cb_ModuleName_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.CmbType);
            this.groupBox1.Controls.Add(this.LblFileName);
            this.groupBox1.Controls.Add(this.btn_Clear);
            this.groupBox1.Controls.Add(this.btn_Save);
            this.groupBox1.Controls.Add(this.LblLocation);
            this.groupBox1.Controls.Add(this.lnkBrowseFile);
            this.groupBox1.Controls.Add(this.TxtDesignName);
            this.groupBox1.Controls.Add(this.lbl_DesignName);
            this.groupBox1.Controls.Add(this.lbl_UserDefineFields);
            this.groupBox1.Controls.Add(this.CmbModule);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.groupBox1.Location = new System.Drawing.Point(7, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(1261, 123);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(592, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 25);
            this.label1.TabIndex = 231;
            this.label1.Text = "Type";
            // 
            // CmbType
            // 
            this.CmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.CmbType.FormattingEnabled = true;
            this.CmbType.Items.AddRange(new object[] {
            "Voucher",
            "Report"});
            this.CmbType.Location = new System.Drawing.Point(649, 15);
            this.CmbType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CmbType.Name = "CmbType";
            this.CmbType.Size = new System.Drawing.Size(224, 33);
            this.CmbType.TabIndex = 1;
            // 
            // LblFileName
            // 
            this.LblFileName.BackColor = System.Drawing.SystemColors.ControlLight;
            this.LblFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.LblFileName.Location = new System.Drawing.Point(176, 84);
            this.LblFileName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblFileName.Name = "LblFileName";
            this.LblFileName.Size = new System.Drawing.Size(309, 26);
            this.LblFileName.TabIndex = 4;
            this.LblFileName.Visible = false;
            // 
            // btn_Clear
            // 
            this.btn_Clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btn_Clear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Clear.Location = new System.Drawing.Point(833, 82);
            this.btn_Clear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(147, 39);
            this.btn_Clear.TabIndex = 6;
            this.btn_Clear.Text = "&ClearControl";
            this.btn_Clear.UseVisualStyleBackColor = false;
            this.btn_Clear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btn_Save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Save.Location = new System.Drawing.Point(665, 82);
            this.btn_Save.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(159, 39);
            this.btn_Save.TabIndex = 5;
            this.btn_Save.Text = "&Save";
            this.btn_Save.UseVisualStyleBackColor = false;
            this.btn_Save.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // LblLocation
            // 
            this.LblLocation.BackColor = System.Drawing.SystemColors.ControlLight;
            this.LblLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.LblLocation.Location = new System.Drawing.Point(175, 57);
            this.LblLocation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblLocation.Name = "LblLocation";
            this.LblLocation.Size = new System.Drawing.Size(1073, 23);
            this.LblLocation.TabIndex = 3;
            // 
            // lnkBrowseFile
            // 
            this.lnkBrowseFile.AutoSize = true;
            this.lnkBrowseFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lnkBrowseFile.LinkColor = System.Drawing.Color.Black;
            this.lnkBrowseFile.Location = new System.Drawing.Point(9, 58);
            this.lnkBrowseFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkBrowseFile.Name = "lnkBrowseFile";
            this.lnkBrowseFile.Size = new System.Drawing.Size(128, 25);
            this.lnkBrowseFile.TabIndex = 211;
            this.lnkBrowseFile.TabStop = true;
            this.lnkBrowseFile.Text = "Browse File...";
            this.lnkBrowseFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkBrowseFile_LinkClicked);
            // 
            // TxtDesignName
            // 
            this.TxtDesignName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDesignName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.TxtDesignName.Location = new System.Drawing.Point(1015, 16);
            this.TxtDesignName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtDesignName.Name = "TxtDesignName";
            this.TxtDesignName.Size = new System.Drawing.Size(238, 30);
            this.TxtDesignName.TabIndex = 2;
            // 
            // lbl_DesignName
            // 
            this.lbl_DesignName.AutoSize = true;
            this.lbl_DesignName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbl_DesignName.Location = new System.Drawing.Point(875, 20);
            this.lbl_DesignName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_DesignName.Name = "lbl_DesignName";
            this.lbl_DesignName.Size = new System.Drawing.Size(130, 25);
            this.lbl_DesignName.TabIndex = 210;
            this.lbl_DesignName.Text = "Design Name";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // RGrid
            // 
            this.RGrid.AllowUserToAddRows = false;
            this.RGrid.AllowUserToResizeColumns = false;
            this.RGrid.AllowUserToResizeRows = false;
            this.RGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.RGrid.ColumnHeadersHeight = 30;
            this.RGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.txt_dgvDocDesignId,
            this.txt_dgvModule,
            this.txt_dgvType,
            this.txt_dgvDesignName,
            this.txt_dgvDesignPath,
            this.dgv_BtnEdit,
            this.dgv_BtnDelete});
            this.RGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.RGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.RGrid.Location = new System.Drawing.Point(7, 126);
            this.RGrid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RGrid.Name = "RGrid";
            this.RGrid.ReadOnly = true;
            this.RGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.RGrid.RowHeadersWidth = 51;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.RGrid.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.RGrid.RowTemplate.Height = 24;
            this.RGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RGrid.Size = new System.Drawing.Size(1261, 398);
            this.RGrid.TabIndex = 0;
            this.RGrid.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_CellContentDoubleClick);
            this.RGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_CellEnter);
            this.RGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.RGrid_RowEnter);
            this.RGrid.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.RGrid_UserDeletedRow);
            this.RGrid.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.RGrid_UserDeletingRow);
            this.RGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RGrid_KeyDown);
            // 
            // txt_dgvDocDesignId
            // 
            this.txt_dgvDocDesignId.HeaderText = "DocDesign Id";
            this.txt_dgvDocDesignId.MinimumWidth = 6;
            this.txt_dgvDocDesignId.Name = "txt_dgvDocDesignId";
            this.txt_dgvDocDesignId.ReadOnly = true;
            this.txt_dgvDocDesignId.Visible = false;
            this.txt_dgvDocDesignId.Width = 125;
            // 
            // txt_dgvModule
            // 
            this.txt_dgvModule.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.txt_dgvModule.HeaderText = "Module";
            this.txt_dgvModule.MinimumWidth = 6;
            this.txt_dgvModule.Name = "txt_dgvModule";
            this.txt_dgvModule.ReadOnly = true;
            // 
            // txt_dgvType
            // 
            this.txt_dgvType.HeaderText = "Type";
            this.txt_dgvType.MinimumWidth = 6;
            this.txt_dgvType.Name = "txt_dgvType";
            this.txt_dgvType.ReadOnly = true;
            this.txt_dgvType.Width = 80;
            // 
            // txt_dgvDesignName
            // 
            this.txt_dgvDesignName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.txt_dgvDesignName.FillWeight = 130F;
            this.txt_dgvDesignName.HeaderText = "Design Name";
            this.txt_dgvDesignName.MinimumWidth = 10;
            this.txt_dgvDesignName.Name = "txt_dgvDesignName";
            this.txt_dgvDesignName.ReadOnly = true;
            // 
            // txt_dgvDesignPath
            // 
            this.txt_dgvDesignPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.txt_dgvDesignPath.HeaderText = "Design Path";
            this.txt_dgvDesignPath.MinimumWidth = 6;
            this.txt_dgvDesignPath.Name = "txt_dgvDesignPath";
            this.txt_dgvDesignPath.ReadOnly = true;
            // 
            // dgv_BtnEdit
            // 
            this.dgv_BtnEdit.HeaderText = "Edit";
            this.dgv_BtnEdit.MinimumWidth = 6;
            this.dgv_BtnEdit.Name = "dgv_BtnEdit";
            this.dgv_BtnEdit.ReadOnly = true;
            this.dgv_BtnEdit.Visible = false;
            this.dgv_BtnEdit.Width = 70;
            // 
            // dgv_BtnDelete
            // 
            this.dgv_BtnDelete.HeaderText = "Delete";
            this.dgv_BtnDelete.MinimumWidth = 6;
            this.dgv_BtnDelete.Name = "dgv_BtnDelete";
            this.dgv_BtnDelete.ReadOnly = true;
            this.dgv_BtnDelete.Width = 70;
            // 
            // FrmPrintDesignAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1271, 538);
            this.Controls.Add(this.RGrid);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPrintDesignAdd";
            this.ShowIcon = false;
            this.Text = "Add Print Design";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmPrintDesignAdd_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmPrintDesignAdd_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_UserDefineFields;
        private System.Windows.Forms.ComboBox CmbModule;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_DesignName;
        private System.Windows.Forms.Label LblLocation;
        private System.Windows.Forms.LinkLabel lnkBrowseFile;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label LblFileName;
        private System.Windows.Forms.DataGridView RGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CmbType;
        private System.Windows.Forms.DataGridViewTextBoxColumn txt_dgvDocDesignId;
        private System.Windows.Forms.DataGridViewTextBoxColumn txt_dgvModule;
        private System.Windows.Forms.DataGridViewTextBoxColumn txt_dgvType;
        private System.Windows.Forms.DataGridViewTextBoxColumn txt_dgvDesignName;
        private System.Windows.Forms.DataGridViewTextBoxColumn txt_dgvDesignPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_BtnEdit;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_BtnDelete;
        private MrTextBox TxtDesignName;
    }
}