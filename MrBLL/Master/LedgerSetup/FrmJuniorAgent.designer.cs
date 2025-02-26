using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Master.LedgerSetup
{
    partial class FrmJuniorAgent
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
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.TxtSeniorAgent = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ChkActive = new System.Windows.Forms.CheckBox();
            this.TxtPhone = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblAgentPhone = new System.Windows.Forms.Label();
            this.lblAgentCommission = new System.Windows.Forms.Label();
            this.TxtLedger = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblAgentLedger = new System.Windows.Forms.Label();
            this.TxtAddress = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblAgentAddress = new System.Windows.Forms.Label();
            this.TxtShortName = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblAgentCode = new System.Windows.Forms.Label();
            this.TxtDescription = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblAgentName = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.StorePanel = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.BtnSync = new DevExpress.XtraEditors.SimpleButton();
            this.BtnView = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSeniorAgent = new System.Windows.Forms.Button();
            this.BtnLedger = new System.Windows.Forms.Button();
            this.BtnDescription = new System.Windows.Forms.Button();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.TxtCommission = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.StorePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.Appearance.Options.UseForeColor = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.BtnCancel.Location = new System.Drawing.Point(432, 191);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(104, 33);
            this.BtnCancel.TabIndex = 13;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Appearance.Options.UseForeColor = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(343, 191);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(89, 33);
            this.BtnSave.TabIndex = 12;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // TxtSeniorAgent
            // 
            this.TxtSeniorAgent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSeniorAgent.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSeniorAgent.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TxtSeniorAgent.Location = new System.Drawing.Point(330, 155);
            this.TxtSeniorAgent.Name = "TxtSeniorAgent";
            this.TxtSeniorAgent.Size = new System.Drawing.Size(181, 25);
            this.TxtSeniorAgent.TabIndex = 11;
            this.TxtSeniorAgent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSeniorAgent_KeyDown);
            this.TxtSeniorAgent.Validating += new System.ComponentModel.CancelEventHandler(this.TxtSeniorAgent_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(216, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 19);
            this.label1.TabIndex = 40;
            this.label1.Text = "Senior Agent";
            // 
            // ChkActive
            // 
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkActive.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ChkActive.Location = new System.Drawing.Point(14, 196);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkActive.Size = new System.Drawing.Size(94, 24);
            this.ChkActive.TabIndex = 10;
            this.ChkActive.Text = "Active";
            this.ChkActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkActive.UseVisualStyleBackColor = true;
            // 
            // TxtPhone
            // 
            this.TxtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPhone.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPhone.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TxtPhone.Location = new System.Drawing.Point(365, 74);
            this.TxtPhone.MaxLength = 50;
            this.TxtPhone.Name = "TxtPhone";
            this.TxtPhone.Size = new System.Drawing.Size(168, 25);
            this.TxtPhone.TabIndex = 7;
            this.TxtPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPhone_KeyPress);
            // 
            // lblAgentPhone
            // 
            this.lblAgentPhone.AutoSize = true;
            this.lblAgentPhone.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgentPhone.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAgentPhone.Location = new System.Drawing.Point(240, 77);
            this.lblAgentPhone.Name = "lblAgentPhone";
            this.lblAgentPhone.Size = new System.Drawing.Size(119, 19);
            this.lblAgentPhone.TabIndex = 0;
            this.lblAgentPhone.Text = "Phone Number";
            // 
            // lblAgentCommission
            // 
            this.lblAgentCommission.AutoSize = true;
            this.lblAgentCommission.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgentCommission.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAgentCommission.Location = new System.Drawing.Point(10, 158);
            this.lblAgentCommission.Name = "lblAgentCommission";
            this.lblAgentCommission.Size = new System.Drawing.Size(100, 19);
            this.lblAgentCommission.TabIndex = 0;
            this.lblAgentCommission.Text = "Commission";
            // 
            // TxtLedger
            // 
            this.TxtLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtLedger.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtLedger.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TxtLedger.Location = new System.Drawing.Point(116, 128);
            this.TxtLedger.Name = "TxtLedger";
            this.TxtLedger.Size = new System.Drawing.Size(395, 25);
            this.TxtLedger.TabIndex = 9;
            this.TxtLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtLedger_KeyDown);
            this.TxtLedger.Validating += new System.ComponentModel.CancelEventHandler(this.TxtLedger_Validating);
            // 
            // lblAgentLedger
            // 
            this.lblAgentLedger.AutoSize = true;
            this.lblAgentLedger.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgentLedger.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAgentLedger.Location = new System.Drawing.Point(10, 131);
            this.lblAgentLedger.Name = "lblAgentLedger";
            this.lblAgentLedger.Size = new System.Drawing.Size(60, 19);
            this.lblAgentLedger.TabIndex = 0;
            this.lblAgentLedger.Text = "Ledger";
            // 
            // TxtAddress
            // 
            this.TxtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAddress.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAddress.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TxtAddress.Location = new System.Drawing.Point(116, 101);
            this.TxtAddress.MaxLength = 50;
            this.TxtAddress.Name = "TxtAddress";
            this.TxtAddress.Size = new System.Drawing.Size(417, 25);
            this.TxtAddress.TabIndex = 8;
            // 
            // lblAgentAddress
            // 
            this.lblAgentAddress.AutoSize = true;
            this.lblAgentAddress.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgentAddress.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAgentAddress.Location = new System.Drawing.Point(10, 104);
            this.lblAgentAddress.Name = "lblAgentAddress";
            this.lblAgentAddress.Size = new System.Drawing.Size(69, 19);
            this.lblAgentAddress.TabIndex = 0;
            this.lblAgentAddress.Text = "Address";
            // 
            // TxtShortName
            // 
            this.TxtShortName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtShortName.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtShortName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TxtShortName.Location = new System.Drawing.Point(116, 74);
            this.TxtShortName.MaxLength = 50;
            this.TxtShortName.Name = "TxtShortName";
            this.TxtShortName.Size = new System.Drawing.Size(112, 25);
            this.TxtShortName.TabIndex = 6;
            this.TxtShortName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtShortName_KeyDown);
            this.TxtShortName.Validating += new System.ComponentModel.CancelEventHandler(this.TxtShortName_Validating);
            // 
            // lblAgentCode
            // 
            this.lblAgentCode.AutoSize = true;
            this.lblAgentCode.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgentCode.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAgentCode.Location = new System.Drawing.Point(10, 77);
            this.lblAgentCode.Name = "lblAgentCode";
            this.lblAgentCode.Size = new System.Drawing.Size(98, 19);
            this.lblAgentCode.TabIndex = 0;
            this.lblAgentCode.Text = "Short Name";
            // 
            // TxtDescription
            // 
            this.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDescription.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDescription.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TxtDescription.Location = new System.Drawing.Point(116, 47);
            this.TxtDescription.MaxLength = 200;
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(395, 25);
            this.TxtDescription.TabIndex = 5;
            this.TxtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDescription_KeyDown);
            this.TxtDescription.Leave += new System.EventHandler(this.TxtDescription_Leave);
            this.TxtDescription.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDescription_Validating);
            // 
            // lblAgentName
            // 
            this.lblAgentName.AutoSize = true;
            this.lblAgentName.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgentName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAgentName.Location = new System.Drawing.Point(10, 50);
            this.lblAgentName.Name = "lblAgentName";
            this.lblAgentName.Size = new System.Drawing.Size(95, 19);
            this.lblAgentName.TabIndex = 0;
            this.lblAgentName.Text = "Description";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(187, 158);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 19);
            this.label7.TabIndex = 0;
            this.label7.Text = "%";
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.BtnExit.Location = new System.Drawing.Point(465, 6);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(77, 33);
            this.BtnExit.TabIndex = 4;
            this.BtnExit.Text = "E&XIT";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(156, 6);
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
            this.BtnEdit.Appearance.Options.UseFont = true;
            this.BtnEdit.ImageOptions.Image = global::MrBLL.Properties.Resources.Edit;
            this.BtnEdit.Location = new System.Drawing.Point(84, 6);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(73, 33);
            this.BtnEdit.TabIndex = 1;
            this.BtnEdit.Tag = "EditButtonCheck";
            this.BtnEdit.Text = "&EDIT";
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnNew
            // 
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnNew.Location = new System.Drawing.Point(10, 6);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(75, 33);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Tag = "NewButtonCheck";
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.BtnSync);
            this.StorePanel.Controls.Add(this.BtnView);
            this.StorePanel.Controls.Add(this.BtnSeniorAgent);
            this.StorePanel.Controls.Add(this.BtnLedger);
            this.StorePanel.Controls.Add(this.BtnDescription);
            this.StorePanel.Controls.Add(this.clsSeparator2);
            this.StorePanel.Controls.Add(this.clsSeparator1);
            this.StorePanel.Controls.Add(this.BtnCancel);
            this.StorePanel.Controls.Add(this.BtnExit);
            this.StorePanel.Controls.Add(this.BtnSave);
            this.StorePanel.Controls.Add(this.TxtSeniorAgent);
            this.StorePanel.Controls.Add(this.label1);
            this.StorePanel.Controls.Add(this.BtnDelete);
            this.StorePanel.Controls.Add(this.lblAgentName);
            this.StorePanel.Controls.Add(this.label7);
            this.StorePanel.Controls.Add(this.BtnEdit);
            this.StorePanel.Controls.Add(this.ChkActive);
            this.StorePanel.Controls.Add(this.BtnNew);
            this.StorePanel.Controls.Add(this.TxtDescription);
            this.StorePanel.Controls.Add(this.lblAgentLedger);
            this.StorePanel.Controls.Add(this.TxtPhone);
            this.StorePanel.Controls.Add(this.TxtLedger);
            this.StorePanel.Controls.Add(this.lblAgentCode);
            this.StorePanel.Controls.Add(this.TxtAddress);
            this.StorePanel.Controls.Add(this.lblAgentPhone);
            this.StorePanel.Controls.Add(this.lblAgentCommission);
            this.StorePanel.Controls.Add(this.TxtShortName);
            this.StorePanel.Controls.Add(this.lblAgentAddress);
            this.StorePanel.Controls.Add(this.TxtCommission);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(545, 230);
            this.StorePanel.TabIndex = 41;
            // 
            // BtnSync
            // 
            this.BtnSync.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSync.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSync.Appearance.Options.UseFont = true;
            this.BtnSync.Appearance.Options.UseForeColor = true;
            this.BtnSync.ImageOptions.Image = global::MrBLL.Properties.Resources.ReportRefresh;
            this.BtnSync.Location = new System.Drawing.Point(243, 190);
            this.BtnSync.Margin = new System.Windows.Forms.Padding(4);
            this.BtnSync.Name = "BtnSync";
            this.BtnSync.Size = new System.Drawing.Size(100, 35);
            this.BtnSync.TabIndex = 42;
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
            this.BtnView.Location = new System.Drawing.Point(255, 6);
            this.BtnView.Margin = new System.Windows.Forms.Padding(4);
            this.BtnView.Name = "BtnView";
            this.BtnView.Size = new System.Drawing.Size(81, 33);
            this.BtnView.TabIndex = 3;
            this.BtnView.Tag = "ViewButtonCheck";
            this.BtnView.Text = "&VIEW";
            this.BtnView.Click += new System.EventHandler(this.BtnView_Click);
            // 
            // BtnSeniorAgent
            // 
            this.BtnSeniorAgent.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnSeniorAgent.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnSeniorAgent.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnSeniorAgent.Location = new System.Drawing.Point(512, 155);
            this.BtnSeniorAgent.Name = "BtnSeniorAgent";
            this.BtnSeniorAgent.Size = new System.Drawing.Size(24, 24);
            this.BtnSeniorAgent.TabIndex = 47;
            this.BtnSeniorAgent.TabStop = false;
            this.BtnSeniorAgent.UseVisualStyleBackColor = false;
            this.BtnSeniorAgent.Click += new System.EventHandler(this.BtnSeniorAgent_Click);
            // 
            // BtnLedger
            // 
            this.BtnLedger.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnLedger.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnLedger.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnLedger.Location = new System.Drawing.Point(512, 128);
            this.BtnLedger.Name = "BtnLedger";
            this.BtnLedger.Size = new System.Drawing.Size(24, 24);
            this.BtnLedger.TabIndex = 46;
            this.BtnLedger.TabStop = false;
            this.BtnLedger.UseVisualStyleBackColor = false;
            this.BtnLedger.Click += new System.EventHandler(this.BtnLedger_Click);
            // 
            // BtnDescription
            // 
            this.BtnDescription.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnDescription.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnDescription.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDescription.Location = new System.Drawing.Point(512, 47);
            this.BtnDescription.Name = "BtnDescription";
            this.BtnDescription.Size = new System.Drawing.Size(24, 24);
            this.BtnDescription.TabIndex = 45;
            this.BtnDescription.TabStop = false;
            this.BtnDescription.UseVisualStyleBackColor = false;
            this.BtnDescription.Click += new System.EventHandler(this.BtnDescription_Click);
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(0, 183);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(541, 2);
            this.clsSeparator2.TabIndex = 42;
            this.clsSeparator2.TabStop = false;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(3, 42);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(538, 2);
            this.clsSeparator1.TabIndex = 41;
            this.clsSeparator1.TabStop = false;
            // 
            // TxtCommission
            // 
            this.TxtCommission.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCommission.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCommission.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TxtCommission.Location = new System.Drawing.Point(116, 155);
            this.TxtCommission.MaxLength = 25;
            this.TxtCommission.Name = "TxtCommission";
            this.TxtCommission.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TxtCommission.Size = new System.Drawing.Size(65, 25);
            this.TxtCommission.TabIndex = 10;
            this.TxtCommission.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_KeyPress);
            // 
            // FrmJuniorAgent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(545, 230);
            this.Controls.Add(this.StorePanel);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmJuniorAgent";
            this.ShowIcon = false;
            this.Tag = "Agent";
            this.Text = "Agent Description";
            this.Load += new System.EventHandler(this.FrmAgent_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmAgent_KeyPress);
            this.StorePanel.ResumeLayout(false);
            this.StorePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblAgentPhone;
        private System.Windows.Forms.Label lblAgentLedger;
        private System.Windows.Forms.Label lblAgentAddress;
        private System.Windows.Forms.Label lblAgentCode;
        private System.Windows.Forms.Label lblAgentName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblAgentCommission;
        private System.Windows.Forms.CheckBox ChkActive;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private ClsSeparator clsSeparator1;
        private ClsSeparator clsSeparator2;
        private System.Windows.Forms.Button BtnSeniorAgent;
        private System.Windows.Forms.Button BtnLedger;
        private System.Windows.Forms.Button BtnDescription;
        private DevExpress.XtraEditors.SimpleButton BtnView;
        private MrTextBox TxtPhone;
        private MrTextBox TxtLedger;
        private MrTextBox TxtAddress;
        private MrTextBox TxtShortName;
        private MrTextBox TxtDescription;
        private MrTextBox TxtSeniorAgent;
        private MrPanel StorePanel;
        private MrTextBox TxtCommission;
        private DevExpress.XtraEditors.SimpleButton BtnSync;
    }
}