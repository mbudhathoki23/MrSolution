using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Master.LedgerSetup
{
    partial class FrmAccountGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAccountGroup));
            this.lblGrpName = new System.Windows.Forms.Label();
            this.lblGrpCode = new System.Windows.Forms.Label();
            this.lblGrpSchedule = new System.Windows.Forms.Label();
            this.lblGrpPrimary = new System.Windows.Forms.Label();
            this.lblGrpType = new System.Windows.Forms.Label();
            this.CmbPrimaryGroup = new System.Windows.Forms.ComboBox();
            this.CmbGroupType = new System.Windows.Forms.ComboBox();
            this.StorePanel = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.BtnSync = new DevExpress.XtraEditors.SimpleButton();
            this.BtnView = new DevExpress.XtraEditors.SimpleButton();
            this.ChkActive = new System.Windows.Forms.CheckBox();
            this.TxtNepaliName = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblNepali = new System.Windows.Forms.Label();
            this.ChkSecondary = new System.Windows.Forms.CheckBox();
            this.BtnSecondary = new System.Windows.Forms.Button();
            this.TxtSecondary = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnDescription = new System.Windows.Forms.Button();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.TxtDescription = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.TxtShortName = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtSchedule = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.StorePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblGrpName
            // 
            this.lblGrpName.AutoSize = true;
            this.lblGrpName.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lblGrpName.Location = new System.Drawing.Point(8, 46);
            this.lblGrpName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGrpName.Name = "lblGrpName";
            this.lblGrpName.Size = new System.Drawing.Size(95, 19);
            this.lblGrpName.TabIndex = 0;
            this.lblGrpName.Text = "Description";
            // 
            // lblGrpCode
            // 
            this.lblGrpCode.AutoSize = true;
            this.lblGrpCode.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lblGrpCode.Location = new System.Drawing.Point(8, 73);
            this.lblGrpCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGrpCode.Name = "lblGrpCode";
            this.lblGrpCode.Size = new System.Drawing.Size(93, 19);
            this.lblGrpCode.TabIndex = 2;
            this.lblGrpCode.Text = "ShortName";
            // 
            // lblGrpSchedule
            // 
            this.lblGrpSchedule.AutoSize = true;
            this.lblGrpSchedule.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lblGrpSchedule.Location = new System.Drawing.Point(291, 73);
            this.lblGrpSchedule.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGrpSchedule.Name = "lblGrpSchedule";
            this.lblGrpSchedule.Size = new System.Drawing.Size(79, 19);
            this.lblGrpSchedule.TabIndex = 4;
            this.lblGrpSchedule.Text = "Schedule";
            // 
            // lblGrpPrimary
            // 
            this.lblGrpPrimary.AutoSize = true;
            this.lblGrpPrimary.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lblGrpPrimary.Location = new System.Drawing.Point(8, 101);
            this.lblGrpPrimary.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGrpPrimary.Name = "lblGrpPrimary";
            this.lblGrpPrimary.Size = new System.Drawing.Size(118, 19);
            this.lblGrpPrimary.TabIndex = 6;
            this.lblGrpPrimary.Text = "Primary Group";
            // 
            // lblGrpType
            // 
            this.lblGrpType.AutoSize = true;
            this.lblGrpType.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lblGrpType.Location = new System.Drawing.Point(8, 134);
            this.lblGrpType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGrpType.Name = "lblGrpType";
            this.lblGrpType.Size = new System.Drawing.Size(93, 19);
            this.lblGrpType.TabIndex = 8;
            this.lblGrpType.Text = "Group Type";
            // 
            // CmbPrimaryGroup
            // 
            this.CmbPrimaryGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.CmbPrimaryGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CmbPrimaryGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbPrimaryGroup.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbPrimaryGroup.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.CmbPrimaryGroup.Location = new System.Drawing.Point(129, 98);
            this.CmbPrimaryGroup.MaxLength = 50;
            this.CmbPrimaryGroup.Name = "CmbPrimaryGroup";
            this.CmbPrimaryGroup.Size = new System.Drawing.Size(267, 27);
            this.CmbPrimaryGroup.Sorted = true;
            this.CmbPrimaryGroup.TabIndex = 8;
            this.CmbPrimaryGroup.SelectedIndexChanged += new System.EventHandler(this.CmbPrimaryGroup_SelectedIndexChanged);
            this.CmbPrimaryGroup.SelectedValueChanged += new System.EventHandler(this.CmbPrimaryGroup_SelectedIndexChanged);
            this.CmbPrimaryGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbCategory_KeyDown);
            // 
            // CmbGroupType
            // 
            this.CmbGroupType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.CmbGroupType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CmbGroupType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbGroupType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbGroupType.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.CmbGroupType.FormattingEnabled = true;
            this.CmbGroupType.Location = new System.Drawing.Point(129, 130);
            this.CmbGroupType.MaxLength = 50;
            this.CmbGroupType.Name = "CmbGroupType";
            this.CmbGroupType.Size = new System.Drawing.Size(267, 27);
            this.CmbGroupType.TabIndex = 9;
            this.CmbGroupType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbCategory_KeyDown);
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.BtnSync);
            this.StorePanel.Controls.Add(this.BtnView);
            this.StorePanel.Controls.Add(this.ChkActive);
            this.StorePanel.Controls.Add(this.TxtNepaliName);
            this.StorePanel.Controls.Add(this.lblNepali);
            this.StorePanel.Controls.Add(this.ChkSecondary);
            this.StorePanel.Controls.Add(this.BtnSecondary);
            this.StorePanel.Controls.Add(this.TxtSecondary);
            this.StorePanel.Controls.Add(this.BtnDescription);
            this.StorePanel.Controls.Add(this.clsSeparator1);
            this.StorePanel.Controls.Add(this.clsSeparator2);
            this.StorePanel.Controls.Add(this.BtnCancel);
            this.StorePanel.Controls.Add(this.BtnSave);
            this.StorePanel.Controls.Add(this.BtnNew);
            this.StorePanel.Controls.Add(this.BtnExit);
            this.StorePanel.Controls.Add(this.TxtDescription);
            this.StorePanel.Controls.Add(this.BtnDelete);
            this.StorePanel.Controls.Add(this.CmbGroupType);
            this.StorePanel.Controls.Add(this.BtnEdit);
            this.StorePanel.Controls.Add(this.lblGrpName);
            this.StorePanel.Controls.Add(this.CmbPrimaryGroup);
            this.StorePanel.Controls.Add(this.lblGrpCode);
            this.StorePanel.Controls.Add(this.lblGrpType);
            this.StorePanel.Controls.Add(this.TxtShortName);
            this.StorePanel.Controls.Add(this.lblGrpPrimary);
            this.StorePanel.Controls.Add(this.TxtSchedule);
            this.StorePanel.Controls.Add(this.lblGrpSchedule);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(593, 255);
            this.StorePanel.TabIndex = 0;
            // 
            // BtnSync
            // 
            this.BtnSync.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSync.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSync.Appearance.Options.UseFont = true;
            this.BtnSync.Appearance.Options.UseForeColor = true;
            this.BtnSync.ImageOptions.Image = global::MrBLL.Properties.Resources.ReportRefresh;
            this.BtnSync.Location = new System.Drawing.Point(268, 215);
            this.BtnSync.Margin = new System.Windows.Forms.Padding(4);
            this.BtnSync.Name = "BtnSync";
            this.BtnSync.Size = new System.Drawing.Size(100, 36);
            this.BtnSync.TabIndex = 314;
            this.BtnSync.Text = "&SYNC";
            this.BtnSync.Click += new System.EventHandler(this.BtnSync_Click);
            // 
            // BtnView
            // 
            this.BtnView.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnView.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnView.Appearance.Options.UseFont = true;
            this.BtnView.Appearance.Options.UseForeColor = true;
            this.BtnView.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnView.Location = new System.Drawing.Point(259, 3);
            this.BtnView.Margin = new System.Windows.Forms.Padding(4);
            this.BtnView.Name = "BtnView";
            this.BtnView.Size = new System.Drawing.Size(78, 33);
            this.BtnView.TabIndex = 3;
            this.BtnView.Tag = "ViewButtonCheck";
            this.BtnView.Text = "&VIEW";
            this.BtnView.Click += new System.EventHandler(this.BtnView_Click);
            // 
            // ChkActive
            // 
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkActive.Location = new System.Drawing.Point(8, 221);
            this.ChkActive.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkActive.Size = new System.Drawing.Size(108, 24);
            this.ChkActive.TabIndex = 13;
            this.ChkActive.Text = "Active";
            this.ChkActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkActive.UseVisualStyleBackColor = true;
            this.ChkActive.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_KeyPress);
            // 
            // TxtNepaliName
            // 
            this.TxtNepaliName.BackColor = System.Drawing.SystemColors.Window;
            this.TxtNepaliName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNepaliName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtNepaliName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtNepaliName.Location = new System.Drawing.Point(129, 186);
            this.TxtNepaliName.MaxLength = 200;
            this.TxtNepaliName.Name = "TxtNepaliName";
            this.TxtNepaliName.Size = new System.Drawing.Size(454, 24);
            this.TxtNepaliName.TabIndex = 12;
            this.TxtNepaliName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_KeyPress);
            // 
            // lblNepali
            // 
            this.lblNepali.AutoSize = true;
            this.lblNepali.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNepali.Location = new System.Drawing.Point(8, 188);
            this.lblNepali.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNepali.Name = "lblNepali";
            this.lblNepali.Size = new System.Drawing.Size(63, 20);
            this.lblNepali.TabIndex = 313;
            this.lblNepali.Text = "नाम लेख्नुस";
            // 
            // ChkSecondary
            // 
            this.ChkSecondary.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkSecondary.Location = new System.Drawing.Point(8, 161);
            this.ChkSecondary.Margin = new System.Windows.Forms.Padding(4);
            this.ChkSecondary.Name = "ChkSecondary";
            this.ChkSecondary.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSecondary.Size = new System.Drawing.Size(109, 23);
            this.ChkSecondary.TabIndex = 10;
            this.ChkSecondary.Text = "Secondary";
            this.ChkSecondary.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSecondary.UseVisualStyleBackColor = true;
            this.ChkSecondary.CheckStateChanged += new System.EventHandler(this.ChkSecondary_CheckStateChanged);
            this.ChkSecondary.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_KeyPress);
            // 
            // BtnSecondary
            // 
            this.BtnSecondary.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnSecondary.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnSecondary.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnSecondary.Location = new System.Drawing.Point(559, 159);
            this.BtnSecondary.Name = "BtnSecondary";
            this.BtnSecondary.Size = new System.Drawing.Size(24, 24);
            this.BtnSecondary.TabIndex = 11;
            this.BtnSecondary.TabStop = false;
            this.BtnSecondary.UseVisualStyleBackColor = false;
            this.BtnSecondary.Click += new System.EventHandler(this.BtnSecondary_Click);
            // 
            // TxtSecondary
            // 
            this.TxtSecondary.BackColor = System.Drawing.SystemColors.Window;
            this.TxtSecondary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSecondary.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtSecondary.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtSecondary.Location = new System.Drawing.Point(129, 160);
            this.TxtSecondary.MaxLength = 200;
            this.TxtSecondary.Name = "TxtSecondary";
            this.TxtSecondary.Size = new System.Drawing.Size(426, 25);
            this.TxtSecondary.TabIndex = 11;
            this.TxtSecondary.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSecondary_KeyDown);
            this.TxtSecondary.Leave += new System.EventHandler(this.TxtSecondary_Leave);
            this.TxtSecondary.Validated += new System.EventHandler(this.TxtSecondary_Validated);
            // 
            // BtnDescription
            // 
            this.BtnDescription.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnDescription.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnDescription.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDescription.Location = new System.Drawing.Point(559, 43);
            this.BtnDescription.Name = "BtnDescription";
            this.BtnDescription.Size = new System.Drawing.Size(24, 24);
            this.BtnDescription.TabIndex = 4;
            this.BtnDescription.TabStop = false;
            this.BtnDescription.UseVisualStyleBackColor = false;
            this.BtnDescription.Click += new System.EventHandler(this.BtnDescription_Click);
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(11, 38);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(579, 2);
            this.clsSeparator1.TabIndex = 38;
            this.clsSeparator1.TabStop = false;
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(4, 213);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(586, 2);
            this.clsSeparator2.TabIndex = 13;
            this.clsSeparator2.TabStop = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnCancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.Appearance.Options.UseForeColor = true;
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(459, 216);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(120, 35);
            this.BtnCancel.TabIndex = 16;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Appearance.Options.UseForeColor = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.OK;
            this.BtnSave.Location = new System.Drawing.Point(369, 216);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(89, 35);
            this.BtnSave.TabIndex = 15;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnNew
            // 
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNew.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.Appearance.Options.UseForeColor = true;
            this.BtnNew.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnNew.Location = new System.Drawing.Point(8, 3);
            this.BtnNew.Margin = new System.Windows.Forms.Padding(4);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(75, 33);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Tag = "NewButtonCheck";
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseForeColor = true;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.BtnExit.Location = new System.Drawing.Point(513, 3);
            this.BtnExit.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(77, 33);
            this.BtnExit.TabIndex = 4;
            this.BtnExit.Text = "E&XIT";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // TxtDescription
            // 
            this.TxtDescription.BackColor = System.Drawing.SystemColors.Window;
            this.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDescription.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtDescription.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtDescription.Location = new System.Drawing.Point(129, 43);
            this.TxtDescription.MaxLength = 200;
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(426, 25);
            this.TxtDescription.TabIndex = 5;
            this.TxtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDescription_KeyDown);
            this.TxtDescription.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDescription_Validating);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.Appearance.Options.UseForeColor = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(157, 3);
            this.BtnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(100, 33);
            this.BtnDelete.TabIndex = 2;
            this.BtnDelete.Tag = "DeleteButtonCheck";
            this.BtnDelete.Text = "&DELETE";
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEdit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnEdit.Appearance.Options.UseFont = true;
            this.BtnEdit.Appearance.Options.UseForeColor = true;
            this.BtnEdit.ImageOptions.Image = global::MrBLL.Properties.Resources.Edit;
            this.BtnEdit.Location = new System.Drawing.Point(83, 3);
            this.BtnEdit.Margin = new System.Windows.Forms.Padding(4);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(73, 33);
            this.BtnEdit.TabIndex = 1;
            this.BtnEdit.Tag = "EditButtonCheck";
            this.BtnEdit.Text = "&EDIT";
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // TxtShortName
            // 
            this.TxtShortName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtShortName.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtShortName.Location = new System.Drawing.Point(129, 70);
            this.TxtShortName.MaxLength = 50;
            this.TxtShortName.Name = "TxtShortName";
            this.TxtShortName.Size = new System.Drawing.Size(157, 25);
            this.TxtShortName.TabIndex = 6;
            this.TxtShortName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtShortName_KeyDown);
            this.TxtShortName.Validating += new System.ComponentModel.CancelEventHandler(this.TxtShortName_Validating);
            // 
            // TxtSchedule
            // 
            this.TxtSchedule.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSchedule.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtSchedule.Location = new System.Drawing.Point(391, 70);
            this.TxtSchedule.MaxLength = 4;
            this.TxtSchedule.Name = "TxtSchedule";
            this.TxtSchedule.Size = new System.Drawing.Size(164, 25);
            this.TxtSchedule.TabIndex = 7;
            this.TxtSchedule.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtSchedule.TextBoxType = MrDAL.Control.ControlsEx.Control.TextBoxType.Numeric;
            this.TxtSchedule.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtSchedule_KeyPress);
            // 
            // FrmAccountGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(593, 255);
            this.Controls.Add(this.StorePanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmAccountGroup";
            this.ShowIcon = false;
            this.Tag = "Account Group";
            this.Text = "ACCOUNT GROUP SETUP";
            this.TransparencyKey = System.Drawing.SystemColors.ActiveCaption;
            this.Load += new System.EventHandler(this.FrmAccountGroup_Load);
            this.Shown += new System.EventHandler(this.FrmAccountGroup_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmAccountGroup_KeyPress);
            this.StorePanel.ResumeLayout(false);
            this.StorePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblGrpName;
        private System.Windows.Forms.Label lblGrpCode;
        private System.Windows.Forms.Label lblGrpSchedule;
        private System.Windows.Forms.Label lblGrpPrimary;
        private System.Windows.Forms.Label lblGrpType;
        private System.Windows.Forms.ComboBox CmbGroupType;
        private System.Windows.Forms.ComboBox CmbPrimaryGroup;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private ClsSeparator clsSeparator2;
        private ClsSeparator clsSeparator1;
        private System.Windows.Forms.Button BtnDescription;
        private System.Windows.Forms.Button BtnSecondary;
        private System.Windows.Forms.CheckBox ChkSecondary;
        private System.Windows.Forms.Label lblNepali;
        private System.Windows.Forms.CheckBox ChkActive;
        private DevExpress.XtraEditors.SimpleButton BtnView;
        private MrTextBox TxtShortName;
        private MrTextBox TxtSchedule;
        public MrTextBox TxtDescription;

        public MrTextBox TxtSecondary;

        public MrTextBox TxtNepaliName;
        private MrPanel StorePanel;
        private DevExpress.XtraEditors.SimpleButton BtnSync;
    }
}