using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Master.LedgerSetup
{
    partial class FrmSubArea
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSubArea));
            this.TxtMainArea = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ChkActive = new System.Windows.Forms.CheckBox();
            this.TxtCountry = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblAreaCountry = new System.Windows.Forms.Label();
            this.TxtShortName = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtDescription = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblAreaName = new System.Windows.Forms.Label();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.StorePanel = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.BtnView = new DevExpress.XtraEditors.SimpleButton();
            this.BtnMainArea = new System.Windows.Forms.Button();
            this.BtnDescription = new System.Windows.Forms.Button();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.lblAreaCode = new System.Windows.Forms.Label();
            this.BtnSync = new DevExpress.XtraEditors.SimpleButton();
            this.StorePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxtMainArea
            // 
            this.TxtMainArea.BackColor = System.Drawing.Color.White;
            this.TxtMainArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMainArea.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtMainArea.Location = new System.Drawing.Point(111, 99);
            this.TxtMainArea.MaxLength = 50;
            this.TxtMainArea.Name = "TxtMainArea";
            this.TxtMainArea.ReadOnly = true;
            this.TxtMainArea.Size = new System.Drawing.Size(340, 25);
            this.TxtMainArea.TabIndex = 6;
            this.TxtMainArea.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtMainArea_KeyDown);
            this.TxtMainArea.Validating += new System.ComponentModel.CancelEventHandler(this.TxtMainArea_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(10, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 19);
            this.label1.TabIndex = 28;
            this.label1.Text = "Main Area";
            // 
            // ChkActive
            // 
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkActive.Location = new System.Drawing.Point(10, 164);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkActive.Size = new System.Drawing.Size(107, 24);
            this.ChkActive.TabIndex = 10;
            this.ChkActive.Text = "Active";
            this.ChkActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkActive.UseVisualStyleBackColor = true;
            // 
            // TxtCountry
            // 
            this.TxtCountry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCountry.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCountry.Location = new System.Drawing.Point(111, 126);
            this.TxtCountry.MaxLength = 50;
            this.TxtCountry.Name = "TxtCountry";
            this.TxtCountry.Size = new System.Drawing.Size(176, 25);
            this.TxtCountry.TabIndex = 7;
            this.TxtCountry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCountry_KeyDown);
            // 
            // lblAreaCountry
            // 
            this.lblAreaCountry.AutoSize = true;
            this.lblAreaCountry.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAreaCountry.Location = new System.Drawing.Point(10, 129);
            this.lblAreaCountry.Name = "lblAreaCountry";
            this.lblAreaCountry.Size = new System.Drawing.Size(69, 19);
            this.lblAreaCountry.TabIndex = 0;
            this.lblAreaCountry.Text = "Country";
            // 
            // TxtShortName
            // 
            this.TxtShortName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtShortName.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtShortName.Location = new System.Drawing.Point(111, 72);
            this.TxtShortName.MaxLength = 50;
            this.TxtShortName.Name = "TxtShortName";
            this.TxtShortName.Size = new System.Drawing.Size(176, 25);
            this.TxtShortName.TabIndex = 5;
            this.TxtShortName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtShortName_KeyDown);
            this.TxtShortName.Validating += new System.ComponentModel.CancelEventHandler(this.TxtShortName_Validating);
            // 
            // TxtDescription
            // 
            this.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDescription.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDescription.Location = new System.Drawing.Point(111, 45);
            this.TxtDescription.MaxLength = 150;
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(340, 25);
            this.TxtDescription.TabIndex = 4;
            this.TxtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDescription_KeyDown);
            this.TxtDescription.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDescription_Validating);
            // 
            // lblAreaName
            // 
            this.lblAreaName.AutoSize = true;
            this.lblAreaName.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAreaName.Location = new System.Drawing.Point(10, 48);
            this.lblAreaName.Name = "lblAreaName";
            this.lblAreaName.Size = new System.Drawing.Size(95, 19);
            this.lblAreaName.TabIndex = 0;
            this.lblAreaName.Text = "Description";
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.Appearance.Options.UseForeColor = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.BtnCancel.Location = new System.Drawing.Point(377, 157);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(103, 37);
            this.BtnCancel.TabIndex = 9;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Appearance.Options.UseForeColor = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(279, 157);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(97, 37);
            this.BtnSave.TabIndex = 8;
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
            this.BtnNew.Location = new System.Drawing.Point(10, 6);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(75, 32);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Tag = "NewButtonCheck";
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.Appearance.Options.UseForeColor = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(166, 6);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(103, 32);
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
            this.BtnEdit.Location = new System.Drawing.Point(85, 6);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(82, 32);
            this.BtnEdit.TabIndex = 1;
            this.BtnEdit.Tag = "EditButtonCheck";
            this.BtnEdit.Text = "&EDIT";
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseForeColor = true;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.BtnExit.Location = new System.Drawing.Point(397, 6);
            this.BtnExit.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(83, 32);
            this.BtnExit.TabIndex = 3;
            this.BtnExit.Text = "E&XIT";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.BtnSync);
            this.StorePanel.Controls.Add(this.BtnView);
            this.StorePanel.Controls.Add(this.BtnMainArea);
            this.StorePanel.Controls.Add(this.BtnDescription);
            this.StorePanel.Controls.Add(this.clsSeparator2);
            this.StorePanel.Controls.Add(this.clsSeparator1);
            this.StorePanel.Controls.Add(this.BtnCancel);
            this.StorePanel.Controls.Add(this.TxtMainArea);
            this.StorePanel.Controls.Add(this.BtnSave);
            this.StorePanel.Controls.Add(this.BtnNew);
            this.StorePanel.Controls.Add(this.BtnDelete);
            this.StorePanel.Controls.Add(this.label1);
            this.StorePanel.Controls.Add(this.ChkActive);
            this.StorePanel.Controls.Add(this.BtnEdit);
            this.StorePanel.Controls.Add(this.TxtCountry);
            this.StorePanel.Controls.Add(this.lblAreaCountry);
            this.StorePanel.Controls.Add(this.BtnExit);
            this.StorePanel.Controls.Add(this.TxtShortName);
            this.StorePanel.Controls.Add(this.lblAreaName);
            this.StorePanel.Controls.Add(this.TxtDescription);
            this.StorePanel.Controls.Add(this.lblAreaCode);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(485, 199);
            this.StorePanel.TabIndex = 0;
            // 
            // BtnView
            // 
            this.BtnView.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnView.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnView.Appearance.Options.UseFont = true;
            this.BtnView.Appearance.Options.UseForeColor = true;
            this.BtnView.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnView.Location = new System.Drawing.Point(268, 6);
            this.BtnView.Margin = new System.Windows.Forms.Padding(4);
            this.BtnView.Name = "BtnView";
            this.BtnView.Size = new System.Drawing.Size(78, 33);
            this.BtnView.TabIndex = 4;
            this.BtnView.Tag = "ViewButtonCheck";
            this.BtnView.Text = "&VIEW";
            this.BtnView.Click += new System.EventHandler(this.BtnView_Click);
            // 
            // BtnMainArea
            // 
            this.BtnMainArea.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnMainArea.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnMainArea.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnMainArea.Location = new System.Drawing.Point(451, 99);
            this.BtnMainArea.Name = "BtnMainArea";
            this.BtnMainArea.Size = new System.Drawing.Size(25, 25);
            this.BtnMainArea.TabIndex = 11;
            this.BtnMainArea.TabStop = false;
            this.BtnMainArea.UseVisualStyleBackColor = false;
            this.BtnMainArea.Click += new System.EventHandler(this.BtnMainArea_Click);
            // 
            // BtnDescription
            // 
            this.BtnDescription.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnDescription.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnDescription.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDescription.Location = new System.Drawing.Point(452, 45);
            this.BtnDescription.Name = "BtnDescription";
            this.BtnDescription.Size = new System.Drawing.Size(25, 25);
            this.BtnDescription.TabIndex = 12;
            this.BtnDescription.TabStop = false;
            this.BtnDescription.UseVisualStyleBackColor = false;
            this.BtnDescription.Click += new System.EventHandler(this.BtnDescription_Click);
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(0, 152);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(485, 2);
            this.clsSeparator2.TabIndex = 33;
            this.clsSeparator2.TabStop = false;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(0, 39);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(485, 2);
            this.clsSeparator1.TabIndex = 32;
            this.clsSeparator1.TabStop = false;
            // 
            // lblAreaCode
            // 
            this.lblAreaCode.AutoSize = true;
            this.lblAreaCode.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAreaCode.Location = new System.Drawing.Point(10, 75);
            this.lblAreaCode.Name = "lblAreaCode";
            this.lblAreaCode.Size = new System.Drawing.Size(93, 19);
            this.lblAreaCode.TabIndex = 0;
            this.lblAreaCode.Text = "ShortName";
            // 
            // BtnSync
            // 
            this.BtnSync.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSync.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSync.Appearance.Options.UseFont = true;
            this.BtnSync.Appearance.Options.UseForeColor = true;
            this.BtnSync.ImageOptions.Image = global::MrBLL.Properties.Resources.ReportRefresh;
            this.BtnSync.Location = new System.Drawing.Point(178, 157);
            this.BtnSync.Margin = new System.Windows.Forms.Padding(4);
            this.BtnSync.Name = "BtnSync";
            this.BtnSync.Size = new System.Drawing.Size(100, 35);
            this.BtnSync.TabIndex = 313;
            this.BtnSync.Text = "&SYNC";
            this.BtnSync.Click += new System.EventHandler(this.BtnSync_Click);
            // 
            // FrmSubArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(485, 199);
            this.Controls.Add(this.StorePanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmSubArea";
            this.ShowIcon = false;
            this.Tag = "Area";
            this.Text = "Area Setup ";
            this.Load += new System.EventHandler(this.FrmArea_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmArea_KeyPress);
            this.StorePanel.ResumeLayout(false);
            this.StorePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblAreaName;
        private System.Windows.Forms.Label lblAreaCountry;
        private System.Windows.Forms.CheckBox ChkActive;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private ClsSeparator clsSeparator2;
        private ClsSeparator clsSeparator1;
        private System.Windows.Forms.Button BtnMainArea;
        private System.Windows.Forms.Button BtnDescription;
		private DevExpress.XtraEditors.SimpleButton BtnView;
        private System.Windows.Forms.Label lblAreaCode;
        private MrTextBox TxtShortName;
        private MrTextBox TxtDescription;
        private MrTextBox TxtCountry;
        private MrTextBox TxtMainArea;
        private MrPanel StorePanel;
        private DevExpress.XtraEditors.SimpleButton BtnSync;
    }
}