using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Master.LedgerSetup
{
    partial class FrmSeniorAgent
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
            this.ChkActive = new System.Windows.Forms.CheckBox();
            this.lblAgentName = new System.Windows.Forms.Label();
            this.TxtDescription = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblAgentCode = new System.Windows.Forms.Label();
            this.TxtShortName = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblAgentAddress = new System.Windows.Forms.Label();
            this.TxtAddress = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblAgentLedger = new System.Windows.Forms.Label();
            this.TxtLedger = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblAgentCommission = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtCommission = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblAgentPhone = new System.Windows.Forms.Label();
            this.TxtPhone = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.StorePanel = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.BtnSync = new DevExpress.XtraEditors.SimpleButton();
            this.BtnView = new DevExpress.XtraEditors.SimpleButton();
            this.BtnLedger = new System.Windows.Forms.Button();
            this.BtnDescription = new System.Windows.Forms.Button();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.StorePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChkActive
            // 
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkActive.Location = new System.Drawing.Point(16, 200);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkActive.Size = new System.Drawing.Size(91, 24);
            this.ChkActive.TabIndex = 25;
            this.ChkActive.Text = "Active";
            this.ChkActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkActive.UseVisualStyleBackColor = true;
            // 
            // lblAgentName
            // 
            this.lblAgentName.AutoSize = true;
            this.lblAgentName.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgentName.Location = new System.Drawing.Point(8, 52);
            this.lblAgentName.Name = "lblAgentName";
            this.lblAgentName.Size = new System.Drawing.Size(95, 19);
            this.lblAgentName.TabIndex = 0;
            this.lblAgentName.Text = "Description";
            // 
            // TxtDescription
            // 
            this.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDescription.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDescription.Location = new System.Drawing.Point(116, 49);
            this.TxtDescription.MaxLength = 200;
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(369, 25);
            this.TxtDescription.TabIndex = 4;
            this.TxtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDescription_KeyDown);
            this.TxtDescription.Leave += new System.EventHandler(this.TxtDescription_Leave);
            this.TxtDescription.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDescription_Validating);
            // 
            // lblAgentCode
            // 
            this.lblAgentCode.AutoSize = true;
            this.lblAgentCode.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgentCode.Location = new System.Drawing.Point(8, 80);
            this.lblAgentCode.Name = "lblAgentCode";
            this.lblAgentCode.Size = new System.Drawing.Size(98, 19);
            this.lblAgentCode.TabIndex = 0;
            this.lblAgentCode.Text = "Short Name";
            // 
            // TxtShortName
            // 
            this.TxtShortName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtShortName.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtShortName.Location = new System.Drawing.Point(116, 77);
            this.TxtShortName.MaxLength = 50;
            this.TxtShortName.Name = "TxtShortName";
            this.TxtShortName.Size = new System.Drawing.Size(141, 25);
            this.TxtShortName.TabIndex = 5;
            this.TxtShortName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtShortName_KeyDown);
            this.TxtShortName.Leave += new System.EventHandler(this.TxtShortName_Leave);
            this.TxtShortName.Validating += new System.ComponentModel.CancelEventHandler(this.TxtShortName_Validating);
            // 
            // lblAgentAddress
            // 
            this.lblAgentAddress.AutoSize = true;
            this.lblAgentAddress.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgentAddress.Location = new System.Drawing.Point(8, 108);
            this.lblAgentAddress.Name = "lblAgentAddress";
            this.lblAgentAddress.Size = new System.Drawing.Size(69, 19);
            this.lblAgentAddress.TabIndex = 0;
            this.lblAgentAddress.Text = "Address";
            // 
            // TxtAddress
            // 
            this.TxtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAddress.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAddress.Location = new System.Drawing.Point(116, 105);
            this.TxtAddress.MaxLength = 50;
            this.TxtAddress.Name = "TxtAddress";
            this.TxtAddress.Size = new System.Drawing.Size(396, 25);
            this.TxtAddress.TabIndex = 7;
            this.TxtAddress.Leave += new System.EventHandler(this.TxtAddress_Leave);
            // 
            // lblAgentLedger
            // 
            this.lblAgentLedger.AutoSize = true;
            this.lblAgentLedger.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgentLedger.Location = new System.Drawing.Point(8, 136);
            this.lblAgentLedger.Name = "lblAgentLedger";
            this.lblAgentLedger.Size = new System.Drawing.Size(60, 19);
            this.lblAgentLedger.TabIndex = 0;
            this.lblAgentLedger.Text = "Ledger";
            // 
            // TxtLedger
            // 
            this.TxtLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtLedger.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtLedger.Location = new System.Drawing.Point(116, 133);
            this.TxtLedger.Name = "TxtLedger";
            this.TxtLedger.Size = new System.Drawing.Size(369, 25);
            this.TxtLedger.TabIndex = 8;
            this.TxtLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtLedger_KeyDown);
            this.TxtLedger.Leave += new System.EventHandler(this.TxtLedger_Leave);
            this.TxtLedger.Validating += new System.ComponentModel.CancelEventHandler(this.TxtLedger_Validating);
            // 
            // lblAgentCommission
            // 
            this.lblAgentCommission.AutoSize = true;
            this.lblAgentCommission.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgentCommission.Location = new System.Drawing.Point(8, 164);
            this.lblAgentCommission.Name = "lblAgentCommission";
            this.lblAgentCommission.Size = new System.Drawing.Size(100, 19);
            this.lblAgentCommission.TabIndex = 0;
            this.lblAgentCommission.Text = "Commission";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(184, 167);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 19);
            this.label7.TabIndex = 0;
            this.label7.Text = "%";
            // 
            // TxtCommission
            // 
            this.TxtCommission.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCommission.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCommission.Location = new System.Drawing.Point(116, 161);
            this.TxtCommission.MaxLength = 25;
            this.TxtCommission.Name = "TxtCommission";
            this.TxtCommission.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TxtCommission.Size = new System.Drawing.Size(65, 25);
            this.TxtCommission.TabIndex = 9;
            this.TxtCommission.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtComm_KeyPress);
            // 
            // lblAgentPhone
            // 
            this.lblAgentPhone.AutoSize = true;
            this.lblAgentPhone.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgentPhone.Location = new System.Drawing.Point(266, 80);
            this.lblAgentPhone.Name = "lblAgentPhone";
            this.lblAgentPhone.Size = new System.Drawing.Size(79, 19);
            this.lblAgentPhone.TabIndex = 0;
            this.lblAgentPhone.Text = "Phone No";
            // 
            // TxtPhone
            // 
            this.TxtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPhone.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPhone.Location = new System.Drawing.Point(351, 77);
            this.TxtPhone.MaxLength = 50;
            this.TxtPhone.Name = "TxtPhone";
            this.TxtPhone.Size = new System.Drawing.Size(161, 25);
            this.TxtPhone.TabIndex = 6;
            this.TxtPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPhone_KeyPress);
            this.TxtPhone.Leave += new System.EventHandler(this.TxtPhone_Leave);
            this.TxtPhone.Validating += new System.ComponentModel.CancelEventHandler(this.TxtPhone_Validating);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.Appearance.Options.UseForeColor = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.BtnCancel.Location = new System.Drawing.Point(411, 196);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(104, 33);
            this.BtnCancel.TabIndex = 11;
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
            this.BtnSave.Location = new System.Drawing.Point(322, 196);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(89, 33);
            this.BtnSave.TabIndex = 10;
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
            this.BtnNew.Location = new System.Drawing.Point(8, 6);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(75, 33);
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
            this.BtnDelete.Location = new System.Drawing.Point(154, 6);
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
            this.BtnEdit.Location = new System.Drawing.Point(82, 6);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(73, 33);
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
            this.BtnExit.Location = new System.Drawing.Point(443, 6);
            this.BtnExit.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(77, 33);
            this.BtnExit.TabIndex = 3;
            this.BtnExit.Text = "&EXIT";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.BtnSync);
            this.StorePanel.Controls.Add(this.BtnView);
            this.StorePanel.Controls.Add(this.BtnLedger);
            this.StorePanel.Controls.Add(this.BtnDescription);
            this.StorePanel.Controls.Add(this.clsSeparator2);
            this.StorePanel.Controls.Add(this.BtnCancel);
            this.StorePanel.Controls.Add(this.clsSeparator1);
            this.StorePanel.Controls.Add(this.BtnSave);
            this.StorePanel.Controls.Add(this.BtnNew);
            this.StorePanel.Controls.Add(this.ChkActive);
            this.StorePanel.Controls.Add(this.lblAgentCommission);
            this.StorePanel.Controls.Add(this.lblAgentPhone);
            this.StorePanel.Controls.Add(this.lblAgentLedger);
            this.StorePanel.Controls.Add(this.lblAgentAddress);
            this.StorePanel.Controls.Add(this.BtnDelete);
            this.StorePanel.Controls.Add(this.lblAgentCode);
            this.StorePanel.Controls.Add(this.TxtPhone);
            this.StorePanel.Controls.Add(this.lblAgentName);
            this.StorePanel.Controls.Add(this.TxtCommission);
            this.StorePanel.Controls.Add(this.label7);
            this.StorePanel.Controls.Add(this.BtnEdit);
            this.StorePanel.Controls.Add(this.TxtLedger);
            this.StorePanel.Controls.Add(this.BtnExit);
            this.StorePanel.Controls.Add(this.TxtAddress);
            this.StorePanel.Controls.Add(this.TxtDescription);
            this.StorePanel.Controls.Add(this.TxtShortName);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(524, 235);
            this.StorePanel.TabIndex = 209;
            // 
            // BtnSync
            // 
            this.BtnSync.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSync.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSync.Appearance.Options.UseFont = true;
            this.BtnSync.Appearance.Options.UseForeColor = true;
            this.BtnSync.ImageOptions.Image = global::MrBLL.Properties.Resources.ReportRefresh;
            this.BtnSync.Location = new System.Drawing.Point(215, 196);
            this.BtnSync.Margin = new System.Windows.Forms.Padding(4);
            this.BtnSync.Name = "BtnSync";
            this.BtnSync.Size = new System.Drawing.Size(100, 33);
            this.BtnSync.TabIndex = 313;
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
            this.BtnView.Location = new System.Drawing.Point(253, 6);
            this.BtnView.Margin = new System.Windows.Forms.Padding(4);
            this.BtnView.Name = "BtnView";
            this.BtnView.Size = new System.Drawing.Size(78, 33);
            this.BtnView.TabIndex = 311;
            this.BtnView.Tag = "ViewButtonCheck";
            this.BtnView.Text = "&VIEW";
            this.BtnView.Click += new System.EventHandler(this.BtnView_Click);
            // 
            // BtnLedger
            // 
            this.BtnLedger.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnLedger.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnLedger.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnLedger.Location = new System.Drawing.Point(486, 133);
            this.BtnLedger.Name = "BtnLedger";
            this.BtnLedger.Size = new System.Drawing.Size(24, 24);
            this.BtnLedger.TabIndex = 310;
            this.BtnLedger.TabStop = false;
            this.BtnLedger.UseVisualStyleBackColor = false;
            this.BtnLedger.Click += new System.EventHandler(this.BtnLedger_Click);
            // 
            // BtnDescription
            // 
            this.BtnDescription.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnDescription.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnDescription.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDescription.Location = new System.Drawing.Point(485, 49);
            this.BtnDescription.Name = "BtnDescription";
            this.BtnDescription.Size = new System.Drawing.Size(24, 24);
            this.BtnDescription.TabIndex = 310;
            this.BtnDescription.TabStop = false;
            this.BtnDescription.UseVisualStyleBackColor = false;
            this.BtnDescription.Click += new System.EventHandler(this.BtnDescription_Click);
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(12, 190);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(507, 2);
            this.clsSeparator2.TabIndex = 40;
            this.clsSeparator2.TabStop = false;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(12, 42);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(507, 2);
            this.clsSeparator1.TabIndex = 32;
            this.clsSeparator1.TabStop = false;
            // 
            // FrmSeniorAgent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(524, 235);
            this.Controls.Add(this.StorePanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmSeniorAgent";
            this.ShowIcon = false;
            this.Tag = "Main Agent";
            this.Text = "Senior Agent Details";
            this.Load += new System.EventHandler(this.FrmSeniorAgent_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmSeniorAgent_KeyPress);
            this.StorePanel.ResumeLayout(false);
            this.StorePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox ChkActive;
        private System.Windows.Forms.Label lblAgentName;
        private System.Windows.Forms.Label lblAgentCode;
        private System.Windows.Forms.Label lblAgentAddress;
        private System.Windows.Forms.Label lblAgentLedger;
        private System.Windows.Forms.Label lblAgentCommission;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblAgentPhone;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private ClsSeparator clsSeparator1;
        private ClsSeparator clsSeparator2;
        private System.Windows.Forms.Button BtnDescription;
        private System.Windows.Forms.Button BtnLedger;
		private DevExpress.XtraEditors.SimpleButton BtnView;
        private MrTextBox TxtDescription;
        private MrTextBox TxtShortName;
        private MrTextBox TxtAddress;
        private MrTextBox TxtLedger;
        private MrTextBox TxtCommission;
        private MrTextBox TxtPhone;
        private MrPanel StorePanel;
        private DevExpress.XtraEditors.SimpleButton BtnSync;
    }
}