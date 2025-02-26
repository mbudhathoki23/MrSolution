using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.SystemSetting.PrintSetting
{
    partial class FrmPrintDesignSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrintDesignSetting));
            this.CmbEntryModule = new System.Windows.Forms.ComboBox();
            this.lbl_UserDefineFields = new System.Windows.Forms.Label();
            this.ChkIsOnline = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtNotes = new MrTextBox();
            this.lbl_Notes = new System.Windows.Forms.Label();
            this.gb_Module = new System.Windows.Forms.GroupBox();
            this.TxtNoOfPrint = new MrTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_Branch = new System.Windows.Forms.Label();
            this.CmbBranch = new System.Windows.Forms.ComboBox();
            this.PanelHeader = new MrPanel();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.SGrid = new System.Windows.Forms.DataGridView();
            this.GTxtSelectedDesign = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGrid = new System.Windows.Forms.DataGridView();
            this.GTxtDesign = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clsSeparatorH2 = new ClsSeparatorH();
            this.clsSeparatorH1 = new ClsSeparatorH();
            this.clsSeparator2 = new ClsSeparator();
            this.clsSeparator1 = new ClsSeparator();
            this.btn_RemoveAll = new System.Windows.Forms.Button();
            this.btn_Remove = new System.Windows.Forms.Button();
            this.btn_MoveAll = new System.Windows.Forms.Button();
            this.btn_Move = new System.Windows.Forms.Button();
            this.gb_Module.SuspendLayout();
            this.PanelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // CmbEntryModule
            // 
            this.CmbEntryModule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbEntryModule.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbEntryModule.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbEntryModule.FormattingEnabled = true;
            this.CmbEntryModule.Location = new System.Drawing.Point(63, 9);
            this.CmbEntryModule.Name = "CmbEntryModule";
            this.CmbEntryModule.Size = new System.Drawing.Size(292, 26);
            this.CmbEntryModule.TabIndex = 1;
            this.CmbEntryModule.SelectedIndexChanged += new System.EventHandler(this.CmbEntryModule_SelectedIndexChanged);
            this.CmbEntryModule.Enter += new System.EventHandler(this.Cb_ModuleName_Enter);
            this.CmbEntryModule.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbEntryModule_KeyDown);
            this.CmbEntryModule.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbEntryModule_KeyPress);
            this.CmbEntryModule.Leave += new System.EventHandler(this.Cb_ModuleName_Leave);
            // 
            // lbl_UserDefineFields
            // 
            this.lbl_UserDefineFields.AutoSize = true;
            this.lbl_UserDefineFields.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_UserDefineFields.Location = new System.Drawing.Point(4, 13);
            this.lbl_UserDefineFields.Name = "lbl_UserDefineFields";
            this.lbl_UserDefineFields.Size = new System.Drawing.Size(55, 18);
            this.lbl_UserDefineFields.TabIndex = 206;
            this.lbl_UserDefineFields.Text = "Module";
            // 
            // ChkIsOnline
            // 
            this.ChkIsOnline.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIsOnline.Location = new System.Drawing.Point(581, 10);
            this.ChkIsOnline.Name = "ChkIsOnline";
            this.ChkIsOnline.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIsOnline.Size = new System.Drawing.Size(135, 24);
            this.ChkIsOnline.TabIndex = 3;
            this.ChkIsOnline.Text = "Online Printing";
            this.ChkIsOnline.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIsOnline.UseVisualStyleBackColor = true;
            this.ChkIsOnline.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Chk_OnlinePrinting_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(461, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 16);
            this.label2.TabIndex = 210;
            this.label2.Text = "Selected Designs";
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 16);
            this.label1.TabIndex = 209;
            this.label1.Text = "Avaliable Designs";
            this.label1.Visible = false;
            // 
            // TxtNotes
            // 
            this.TxtNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNotes.Location = new System.Drawing.Point(59, 381);
            this.TxtNotes.Multiline = true;
            this.TxtNotes.Name = "TxtNotes";
            this.TxtNotes.Size = new System.Drawing.Size(577, 29);
            this.TxtNotes.TabIndex = 10;
            this.TxtNotes.Enter += new System.EventHandler(this.TxtNotes_Enter);
            this.TxtNotes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNotes_KeyPress);
            this.TxtNotes.Leave += new System.EventHandler(this.TxtNotes_Leave);
            // 
            // lbl_Notes
            // 
            this.lbl_Notes.AutoSize = true;
            this.lbl_Notes.Font = new System.Drawing.Font("Arial", 11F);
            this.lbl_Notes.Location = new System.Drawing.Point(9, 387);
            this.lbl_Notes.Name = "lbl_Notes";
            this.lbl_Notes.Size = new System.Drawing.Size(46, 17);
            this.lbl_Notes.TabIndex = 207;
            this.lbl_Notes.Text = "Notes";
            // 
            // gb_Module
            // 
            this.gb_Module.Controls.Add(this.TxtNoOfPrint);
            this.gb_Module.Controls.Add(this.label3);
            this.gb_Module.Controls.Add(this.lbl_Branch);
            this.gb_Module.Controls.Add(this.CmbBranch);
            this.gb_Module.Controls.Add(this.ChkIsOnline);
            this.gb_Module.Controls.Add(this.lbl_UserDefineFields);
            this.gb_Module.Controls.Add(this.CmbEntryModule);
            this.gb_Module.Dock = System.Windows.Forms.DockStyle.Top;
            this.gb_Module.Location = new System.Drawing.Point(0, 0);
            this.gb_Module.Name = "gb_Module";
            this.gb_Module.Size = new System.Drawing.Size(845, 40);
            this.gb_Module.TabIndex = 22;
            this.gb_Module.TabStop = false;
            // 
            // TxtNoOfPrint
            // 
            this.TxtNoOfPrint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNoOfPrint.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtNoOfPrint.Location = new System.Drawing.Point(804, 10);
            this.TxtNoOfPrint.Name = "TxtNoOfPrint";
            this.TxtNoOfPrint.Size = new System.Drawing.Size(38, 23);
            this.TxtNoOfPrint.TabIndex = 211;
            this.TxtNoOfPrint.Text = "1";
            this.TxtNoOfPrint.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtNoOfPrint.Enter += new System.EventHandler(this.TxtNoOfPrint_Enter);
            this.TxtNoOfPrint.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNoOfPrint_KeyPress);
            this.TxtNoOfPrint.Leave += new System.EventHandler(this.TxtNoOfPrint_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(716, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 18);
            this.label3.TabIndex = 210;
            this.label3.Text = "No. Of Print";
            // 
            // lbl_Branch
            // 
            this.lbl_Branch.AutoSize = true;
            this.lbl_Branch.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Branch.Location = new System.Drawing.Point(355, 13);
            this.lbl_Branch.Name = "lbl_Branch";
            this.lbl_Branch.Size = new System.Drawing.Size(60, 18);
            this.lbl_Branch.TabIndex = 209;
            this.lbl_Branch.Text = "Branch ";
            // 
            // CmbBranch
            // 
            this.CmbBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbBranch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbBranch.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbBranch.FormattingEnabled = true;
            this.CmbBranch.Location = new System.Drawing.Point(416, 9);
            this.CmbBranch.Name = "CmbBranch";
            this.CmbBranch.Size = new System.Drawing.Size(163, 26);
            this.CmbBranch.TabIndex = 2;
            this.CmbBranch.Enter += new System.EventHandler(this.Cmb_Branch_Enter);
            this.CmbBranch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Cmb_Branch_KeyPress);
            this.CmbBranch.Leave += new System.EventHandler(this.Cmb_Branch_Leave);
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.BtnCancel);
            this.PanelHeader.Controls.Add(this.BtnSave);
            this.PanelHeader.Controls.Add(this.SGrid);
            this.PanelHeader.Controls.Add(this.DGrid);
            this.PanelHeader.Controls.Add(this.clsSeparatorH2);
            this.PanelHeader.Controls.Add(this.clsSeparatorH1);
            this.PanelHeader.Controls.Add(this.clsSeparator2);
            this.PanelHeader.Controls.Add(this.clsSeparator1);
            this.PanelHeader.Controls.Add(this.TxtNotes);
            this.PanelHeader.Controls.Add(this.lbl_Notes);
            this.PanelHeader.Controls.Add(this.label1);
            this.PanelHeader.Controls.Add(this.label2);
            this.PanelHeader.Controls.Add(this.btn_RemoveAll);
            this.PanelHeader.Controls.Add(this.btn_Remove);
            this.PanelHeader.Controls.Add(this.btn_MoveAll);
            this.PanelHeader.Controls.Add(this.btn_Move);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 40);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(845, 414);
            this.PanelHeader.TabIndex = 40;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnCancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.Appearance.Options.UseForeColor = true;
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(733, 378);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(104, 32);
            this.BtnCancel.TabIndex = 218;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Appearance.Options.UseForeColor = true;
            this.BtnSave.Location = new System.Drawing.Point(642, 378);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(89, 32);
            this.BtnSave.TabIndex = 217;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // SGrid
            // 
            this.SGrid.AllowUserToAddRows = false;
            this.SGrid.AllowUserToDeleteRows = false;
            this.SGrid.AllowUserToOrderColumns = true;
            this.SGrid.AllowUserToResizeColumns = false;
            this.SGrid.AllowUserToResizeRows = false;
            this.SGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.SGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GTxtSelectedDesign});
            this.SGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.SGrid.Location = new System.Drawing.Point(452, 28);
            this.SGrid.MultiSelect = false;
            this.SGrid.Name = "SGrid";
            this.SGrid.ReadOnly = true;
            this.SGrid.RowHeadersWidth = 25;
            this.SGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.SGrid.Size = new System.Drawing.Size(390, 341);
            this.SGrid.TabIndex = 216;
            this.SGrid.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.SGrid_CellMouseDoubleClick);
            this.SGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SGrid_KeyDown);
            // 
            // GTxtSelectedDesign
            // 
            this.GTxtSelectedDesign.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.GTxtSelectedDesign.HeaderText = "DESIGN";
            this.GTxtSelectedDesign.Name = "GTxtSelectedDesign";
            this.GTxtSelectedDesign.ReadOnly = true;
            // 
            // DGrid
            // 
            this.DGrid.AllowUserToAddRows = false;
            this.DGrid.AllowUserToDeleteRows = false;
            this.DGrid.AllowUserToOrderColumns = true;
            this.DGrid.AllowUserToResizeColumns = false;
            this.DGrid.AllowUserToResizeRows = false;
            this.DGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.DGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.DGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GTxtDesign});
            this.DGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.DGrid.Location = new System.Drawing.Point(9, 28);
            this.DGrid.MultiSelect = false;
            this.DGrid.Name = "DGrid";
            this.DGrid.ReadOnly = true;
            this.DGrid.RowHeadersWidth = 25;
            this.DGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGrid.Size = new System.Drawing.Size(390, 341);
            this.DGrid.TabIndex = 215;
            this.DGrid.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DGrid_CellMouseDoubleClick);
            this.DGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DGrid_KeyDown);
            // 
            // GTxtDesign
            // 
            this.GTxtDesign.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.GTxtDesign.DataPropertyName = "Design";
            this.GTxtDesign.HeaderText = "DESIGN";
            this.GTxtDesign.Name = "GTxtDesign";
            this.GTxtDesign.ReadOnly = true;
            // 
            // clsSeparatorH2
            // 
            this.clsSeparatorH2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparatorH2.Location = new System.Drawing.Point(444, 28);
            this.clsSeparatorH2.Name = "clsSeparatorH2";
            this.clsSeparatorH2.Size = new System.Drawing.Size(3, 348);
            this.clsSeparatorH2.TabIndex = 214;
            this.clsSeparatorH2.TabStop = false;
            // 
            // clsSeparatorH1
            // 
            this.clsSeparatorH1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparatorH1.Location = new System.Drawing.Point(405, 28);
            this.clsSeparatorH1.Name = "clsSeparatorH1";
            this.clsSeparatorH1.Size = new System.Drawing.Size(3, 348);
            this.clsSeparatorH1.TabIndex = 213;
            this.clsSeparatorH1.TabStop = false;
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator2.Location = new System.Drawing.Point(9, 21);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(833, 2);
            this.clsSeparator2.TabIndex = 212;
            this.clsSeparator2.TabStop = false;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator1.Location = new System.Drawing.Point(4, 375);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(833, 2);
            this.clsSeparator1.TabIndex = 211;
            this.clsSeparator1.TabStop = false;
            // 
            // btn_RemoveAll
            // 
            this.btn_RemoveAll.Image = global::MrBLL.Properties.Resources.Previous;
            this.btn_RemoveAll.Location = new System.Drawing.Point(410, 232);
            this.btn_RemoveAll.Name = "btn_RemoveAll";
            this.btn_RemoveAll.Size = new System.Drawing.Size(31, 29);
            this.btn_RemoveAll.TabIndex = 8;
            this.btn_RemoveAll.UseVisualStyleBackColor = false;
            this.btn_RemoveAll.Click += new System.EventHandler(this.BtnRemoveAll_Click);
            // 
            // btn_Remove
            // 
            this.btn_Remove.Font = new System.Drawing.Font("Arial", 14F);
            this.btn_Remove.Image = global::MrBLL.Properties.Resources.SinglePrevious;
            this.btn_Remove.Location = new System.Drawing.Point(410, 191);
            this.btn_Remove.Name = "btn_Remove";
            this.btn_Remove.Size = new System.Drawing.Size(31, 29);
            this.btn_Remove.TabIndex = 7;
            this.btn_Remove.UseVisualStyleBackColor = false;
            this.btn_Remove.Click += new System.EventHandler(this.BtnRemove_Click);
            // 
            // btn_MoveAll
            // 
            this.btn_MoveAll.Font = new System.Drawing.Font("Arial", 14F);
            this.btn_MoveAll.Image = global::MrBLL.Properties.Resources.Next;
            this.btn_MoveAll.Location = new System.Drawing.Point(410, 150);
            this.btn_MoveAll.Name = "btn_MoveAll";
            this.btn_MoveAll.Size = new System.Drawing.Size(31, 29);
            this.btn_MoveAll.TabIndex = 6;
            this.btn_MoveAll.UseVisualStyleBackColor = false;
            this.btn_MoveAll.Click += new System.EventHandler(this.BtnMoveAll_Click);
            // 
            // btn_Move
            // 
            this.btn_Move.Font = new System.Drawing.Font("Arial", 14F);
            this.btn_Move.Image = global::MrBLL.Properties.Resources.SingleNext;
            this.btn_Move.Location = new System.Drawing.Point(410, 109);
            this.btn_Move.Name = "btn_Move";
            this.btn_Move.Size = new System.Drawing.Size(31, 29);
            this.btn_Move.TabIndex = 5;
            this.btn_Move.UseVisualStyleBackColor = false;
            this.btn_Move.Click += new System.EventHandler(this.BtnMove_Click);
            // 
            // FrmPrintDesignSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(845, 454);
            this.Controls.Add(this.PanelHeader);
            this.Controls.Add(this.gb_Module);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmPrintDesignSetting";
            this.ShowIcon = false;
            this.Text = "Print Design Setting";
            this.Load += new System.EventHandler(this.FrmPrintDesignSetting_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmPrintDesignSetting_KeyPress);
            this.gb_Module.ResumeLayout(false);
            this.gb_Module.PerformLayout();
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox CmbEntryModule;
        private System.Windows.Forms.Label lbl_UserDefineFields;
        private System.Windows.Forms.CheckBox ChkIsOnline;
        private System.Windows.Forms.GroupBox gb_Module;
        private System.Windows.Forms.Button btn_RemoveAll;
        private System.Windows.Forms.Button btn_Remove;
        private System.Windows.Forms.Button btn_MoveAll;
        private System.Windows.Forms.Button btn_Move;
        private System.Windows.Forms.Label lbl_Notes;
        private System.Windows.Forms.Label lbl_Branch;
        private System.Windows.Forms.ComboBox CmbBranch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private ClsSeparator clsSeparator1;
        private ClsSeparator clsSeparator2;
        private ClsSeparatorH clsSeparatorH1;
        private ClsSeparatorH clsSeparatorH2;
        private System.Windows.Forms.DataGridView SGrid;
        private System.Windows.Forms.DataGridView DGrid;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtDesign;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtSelectedDesign;
        private MrTextBox TxtNotes;
        private MrTextBox TxtNoOfPrint;
        private MrPanel PanelHeader;
    }
}