using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Master.LedgerSetup
{
    partial class FrmAccountSubGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAccountSubGroup));
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.TxtGroup = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.TxtShortName = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtDescription = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.ChkActive = new System.Windows.Forms.CheckBox();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.BtnGroup = new System.Windows.Forms.Button();
            this.TxtSecondary = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnSecondary = new System.Windows.Forms.Button();
            this.ChkSecondary = new System.Windows.Forms.CheckBox();
            this.TxtNepaliName = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnDescription = new System.Windows.Forms.Button();
            this.BtnView = new DevExpress.XtraEditors.SimpleButton();
            this.StorePanel = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.BtnSync = new DevExpress.XtraEditors.SimpleButton();
            this.label4 = new System.Windows.Forms.Label();
            this.StorePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseForeColor = true;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.BtnExit.Location = new System.Drawing.Point(418, 4);
            this.BtnExit.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.BtnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(78, 34);
            this.BtnExit.TabIndex = 4;
            this.BtnExit.Text = "&EXIT";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEdit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnEdit.Appearance.Options.UseFont = true;
            this.BtnEdit.Appearance.Options.UseForeColor = true;
            this.BtnEdit.ImageOptions.Image = global::MrBLL.Properties.Resources.Edit;
            this.BtnEdit.Location = new System.Drawing.Point(83, 4);
            this.BtnEdit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(77, 34);
            this.BtnEdit.TabIndex = 1;
            this.BtnEdit.Tag = "EditButtonCheck";
            this.BtnEdit.Text = "&EDIT";
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // TxtGroup
            // 
            this.TxtGroup.BackColor = System.Drawing.Color.White;
            this.TxtGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtGroup.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtGroup.Location = new System.Drawing.Point(121, 99);
            this.TxtGroup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TxtGroup.MaxLength = 200;
            this.TxtGroup.Name = "TxtGroup";
            this.TxtGroup.ReadOnly = true;
            this.TxtGroup.Size = new System.Drawing.Size(352, 25);
            this.TxtGroup.TabIndex = 7;
            this.TxtGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtGroup_KeyDown);
            this.TxtGroup.Validating += new System.ComponentModel.CancelEventHandler(this.TxtGroup_Validating);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Appearance.Options.UseForeColor = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(296, 188);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(89, 36);
            this.BtnSave.TabIndex = 11;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label2.Location = new System.Drawing.Point(9, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "ShortName";
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.Appearance.Options.UseForeColor = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(161, 4);
            this.BtnDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(101, 33);
            this.BtnDelete.TabIndex = 2;
            this.BtnDelete.Tag = "DeleteButtonCheck";
            this.BtnDelete.Text = "&DELETE";
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label3.Location = new System.Drawing.Point(9, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "Group";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label1.Location = new System.Drawing.Point(9, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Description";
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.Appearance.Options.UseForeColor = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(387, 188);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(104, 36);
            this.BtnCancel.TabIndex = 12;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // TxtShortName
            // 
            this.TxtShortName.BackColor = System.Drawing.Color.White;
            this.TxtShortName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtShortName.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtShortName.Location = new System.Drawing.Point(121, 71);
            this.TxtShortName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TxtShortName.MaxLength = 50;
            this.TxtShortName.Name = "TxtShortName";
            this.TxtShortName.Size = new System.Drawing.Size(165, 25);
            this.TxtShortName.TabIndex = 6;
            this.TxtShortName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtShortName_KeyDown);
            this.TxtShortName.Validating += new System.ComponentModel.CancelEventHandler(this.TxtShortName_Validating);
            // 
            // TxtDescription
            // 
            this.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDescription.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtDescription.Location = new System.Drawing.Point(121, 44);
            this.TxtDescription.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TxtDescription.MaxLength = 200;
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(352, 25);
            this.TxtDescription.TabIndex = 5;
            this.TxtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDescription_KeyDown);
            this.TxtDescription.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDescription_Validating);
            // 
            // BtnNew
            // 
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNew.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.Appearance.Options.UseForeColor = true;
            this.BtnNew.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnNew.Location = new System.Drawing.Point(6, 4);
            this.BtnNew.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(76, 34);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Tag = "NewButtonCheck";
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // ChkActive
            // 
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkActive.Location = new System.Drawing.Point(9, 195);
            this.ChkActive.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkActive.Size = new System.Drawing.Size(108, 24);
            this.ChkActive.TabIndex = 11;
            this.ChkActive.Text = "Active";
            this.ChkActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkActive.UseVisualStyleBackColor = true;
            this.ChkActive.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ChkActive_KeyPress);
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(4, 40);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(490, 2);
            this.clsSeparator1.TabIndex = 264;
            this.clsSeparator1.TabStop = false;
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(4, 184);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(490, 2);
            this.clsSeparator2.TabIndex = 265;
            this.clsSeparator2.TabStop = false;
            // 
            // BtnGroup
            // 
            this.BtnGroup.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.BtnGroup.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnGroup.ForeColor = System.Drawing.Color.Transparent;
            this.BtnGroup.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnGroup.Location = new System.Drawing.Point(474, 99);
            this.BtnGroup.Name = "BtnGroup";
            this.BtnGroup.Size = new System.Drawing.Size(24, 24);
            this.BtnGroup.TabIndex = 16;
            this.BtnGroup.UseVisualStyleBackColor = false;
            this.BtnGroup.Click += new System.EventHandler(this.BtnAccountGroup_Click);
            // 
            // TxtSecondary
            // 
            this.TxtSecondary.BackColor = System.Drawing.SystemColors.Window;
            this.TxtSecondary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSecondary.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtSecondary.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtSecondary.Location = new System.Drawing.Point(121, 127);
            this.TxtSecondary.MaxLength = 200;
            this.TxtSecondary.Name = "TxtSecondary";
            this.TxtSecondary.Size = new System.Drawing.Size(352, 25);
            this.TxtSecondary.TabIndex = 9;
            this.TxtSecondary.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSecondary_KeyDown);
            // 
            // BtnSecondary
            // 
            this.BtnSecondary.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnSecondary.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnSecondary.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnSecondary.Location = new System.Drawing.Point(474, 127);
            this.BtnSecondary.Name = "BtnSecondary";
            this.BtnSecondary.Size = new System.Drawing.Size(24, 24);
            this.BtnSecondary.TabIndex = 9;
            this.BtnSecondary.TabStop = false;
            this.BtnSecondary.UseVisualStyleBackColor = false;
            this.BtnSecondary.Click += new System.EventHandler(this.BtnSecondary_Click);
            // 
            // ChkSecondary
            // 
            this.ChkSecondary.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkSecondary.Location = new System.Drawing.Point(9, 128);
            this.ChkSecondary.Margin = new System.Windows.Forms.Padding(4);
            this.ChkSecondary.Name = "ChkSecondary";
            this.ChkSecondary.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkSecondary.Size = new System.Drawing.Size(108, 23);
            this.ChkSecondary.TabIndex = 8;
            this.ChkSecondary.Text = "Secondary";
            this.ChkSecondary.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSecondary.UseVisualStyleBackColor = true;
            this.ChkSecondary.CheckedChanged += new System.EventHandler(this.ChkSecondary_CheckedChanged);
            this.ChkSecondary.CheckStateChanged += new System.EventHandler(this.ChkSecondary_CheckStateChanged);
            // 
            // TxtNepaliName
            // 
            this.TxtNepaliName.BackColor = System.Drawing.SystemColors.Window;
            this.TxtNepaliName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNepaliName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtNepaliName.Location = new System.Drawing.Point(121, 156);
            this.TxtNepaliName.MaxLength = 200;
            this.TxtNepaliName.Name = "TxtNepaliName";
            this.TxtNepaliName.Size = new System.Drawing.Size(347, 25);
            this.TxtNepaliName.TabIndex = 10;
            // 
            // BtnDescription
            // 
            this.BtnDescription.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnDescription.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnDescription.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDescription.Location = new System.Drawing.Point(474, 44);
            this.BtnDescription.Name = "BtnDescription";
            this.BtnDescription.Size = new System.Drawing.Size(24, 24);
            this.BtnDescription.TabIndex = 14;
            this.BtnDescription.TabStop = false;
            this.BtnDescription.UseVisualStyleBackColor = false;
            this.BtnDescription.Click += new System.EventHandler(this.BtnDescription_Click);
            // 
            // BtnView
            // 
            this.BtnView.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnView.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnView.Appearance.Options.UseFont = true;
            this.BtnView.Appearance.Options.UseForeColor = true;
            this.BtnView.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnView.Location = new System.Drawing.Point(263, 4);
            this.BtnView.Margin = new System.Windows.Forms.Padding(4);
            this.BtnView.Name = "BtnView";
            this.BtnView.Size = new System.Drawing.Size(101, 34);
            this.BtnView.TabIndex = 3;
            this.BtnView.Tag = "ViewButtonCheck";
            this.BtnView.Text = "&VIEW";
            this.BtnView.Click += new System.EventHandler(this.BtnView_Click);
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.BtnSync);
            this.StorePanel.Controls.Add(this.BtnView);
            this.StorePanel.Controls.Add(this.BtnDescription);
            this.StorePanel.Controls.Add(this.TxtNepaliName);
            this.StorePanel.Controls.Add(this.label4);
            this.StorePanel.Controls.Add(this.ChkSecondary);
            this.StorePanel.Controls.Add(this.BtnSecondary);
            this.StorePanel.Controls.Add(this.TxtSecondary);
            this.StorePanel.Controls.Add(this.BtnGroup);
            this.StorePanel.Controls.Add(this.clsSeparator2);
            this.StorePanel.Controls.Add(this.clsSeparator1);
            this.StorePanel.Controls.Add(this.ChkActive);
            this.StorePanel.Controls.Add(this.BtnNew);
            this.StorePanel.Controls.Add(this.TxtDescription);
            this.StorePanel.Controls.Add(this.TxtShortName);
            this.StorePanel.Controls.Add(this.BtnCancel);
            this.StorePanel.Controls.Add(this.label1);
            this.StorePanel.Controls.Add(this.label3);
            this.StorePanel.Controls.Add(this.BtnDelete);
            this.StorePanel.Controls.Add(this.label2);
            this.StorePanel.Controls.Add(this.BtnSave);
            this.StorePanel.Controls.Add(this.TxtGroup);
            this.StorePanel.Controls.Add(this.BtnEdit);
            this.StorePanel.Controls.Add(this.BtnExit);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(502, 229);
            this.StorePanel.TabIndex = 0;
            // 
            // BtnSync
            // 
            this.BtnSync.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSync.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSync.Appearance.Options.UseFont = true;
            this.BtnSync.Appearance.Options.UseForeColor = true;
            this.BtnSync.ImageOptions.Image = global::MrBLL.Properties.Resources.ReportRefresh;
            this.BtnSync.Location = new System.Drawing.Point(195, 188);
            this.BtnSync.Margin = new System.Windows.Forms.Padding(4);
            this.BtnSync.Name = "BtnSync";
            this.BtnSync.Size = new System.Drawing.Size(100, 36);
            this.BtnSync.TabIndex = 13;
            this.BtnSync.Text = "&SYNC";
            this.BtnSync.Click += new System.EventHandler(this.BtnSync_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label4.Location = new System.Drawing.Point(9, 157);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 20);
            this.label4.TabIndex = 318;
            this.label4.Text = "नाम लेख्नुस";
            // 
            // FrmAccountSubGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(502, 229);
            this.Controls.Add(this.StorePanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmAccountSubGroup";
            this.ShowIcon = false;
            this.Tag = "Account Sub Group";
            this.Text = "Account SubGroup Setup";
            this.Load += new System.EventHandler(this.FrmAccountSubGroup_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmAccountSubGroup_KeyPress);
            this.StorePanel.ResumeLayout(false);
            this.StorePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private MrTextBox TxtGroup;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private MrTextBox TxtShortName;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private System.Windows.Forms.CheckBox ChkActive;
        private ClsSeparator clsSeparator1;
        private ClsSeparator clsSeparator2;
        private System.Windows.Forms.Button BtnGroup;
        public MrTextBox TxtSecondary;
        private System.Windows.Forms.Button BtnSecondary;
        private System.Windows.Forms.CheckBox ChkSecondary;
        public MrTextBox TxtNepaliName;
        private System.Windows.Forms.Button BtnDescription;
        private DevExpress.XtraEditors.SimpleButton BtnView;
        private System.Windows.Forms.Label label4;
        private MrPanel StorePanel;
        private DevExpress.XtraEditors.SimpleButton BtnSync;
        private MrTextBox TxtDescription;
    }
}