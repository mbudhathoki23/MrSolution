using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Master.LedgerSetup
{
    partial class FrmSubLedger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSubLedger));
            this.lblSl_Address = new System.Windows.Forms.Label();
            this.lblSl_Code = new System.Windows.Forms.Label();
            this.TxtDescription = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblSl_Name = new System.Windows.Forms.Label();
            this.lblSl_PhoneNumber = new System.Windows.Forms.Label();
            this.TxtShortName = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblSl_Ledger = new System.Windows.Forms.Label();
            this.TxtAddress = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtPhoneNumber = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.TxtLedger = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.ChkActive = new System.Windows.Forms.CheckBox();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.BtnDescription = new System.Windows.Forms.Button();
            this.BtnLedger = new System.Windows.Forms.Button();
            this.BtnView = new DevExpress.XtraEditors.SimpleButton();
            this.StorePanel = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.BtnSync = new DevExpress.XtraEditors.SimpleButton();
            this.StorePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSl_Address
            // 
            this.lblSl_Address.AutoSize = true;
            this.lblSl_Address.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSl_Address.Location = new System.Drawing.Point(9, 103);
            this.lblSl_Address.Name = "lblSl_Address";
            this.lblSl_Address.Size = new System.Drawing.Size(69, 19);
            this.lblSl_Address.TabIndex = 0;
            this.lblSl_Address.Text = "Address";
            // 
            // lblSl_Code
            // 
            this.lblSl_Code.AutoSize = true;
            this.lblSl_Code.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSl_Code.Location = new System.Drawing.Point(9, 76);
            this.lblSl_Code.Name = "lblSl_Code";
            this.lblSl_Code.Size = new System.Drawing.Size(93, 19);
            this.lblSl_Code.TabIndex = 0;
            this.lblSl_Code.Text = "ShortName";
            // 
            // TxtDescription
            // 
            this.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDescription.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDescription.Location = new System.Drawing.Point(109, 46);
            this.TxtDescription.MaxLength = 200;
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(342, 25);
            this.TxtDescription.TabIndex = 5;
            this.TxtDescription.TextChanged += new System.EventHandler(this.TxtDescription_TextChanged);
            this.TxtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDescription_KeyDown);
            this.TxtDescription.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDescription_Validating);
            // 
            // lblSl_Name
            // 
            this.lblSl_Name.AutoSize = true;
            this.lblSl_Name.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSl_Name.Location = new System.Drawing.Point(9, 49);
            this.lblSl_Name.Name = "lblSl_Name";
            this.lblSl_Name.Size = new System.Drawing.Size(95, 19);
            this.lblSl_Name.TabIndex = 0;
            this.lblSl_Name.Text = "Description";
            // 
            // lblSl_PhoneNumber
            // 
            this.lblSl_PhoneNumber.AutoSize = true;
            this.lblSl_PhoneNumber.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSl_PhoneNumber.Location = new System.Drawing.Point(9, 131);
            this.lblSl_PhoneNumber.Name = "lblSl_PhoneNumber";
            this.lblSl_PhoneNumber.Size = new System.Drawing.Size(84, 19);
            this.lblSl_PhoneNumber.TabIndex = 0;
            this.lblSl_PhoneNumber.Text = "Phone No.";
            // 
            // TxtShortName
            // 
            this.TxtShortName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtShortName.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtShortName.Location = new System.Drawing.Point(109, 73);
            this.TxtShortName.MaxLength = 50;
            this.TxtShortName.Name = "TxtShortName";
            this.TxtShortName.Size = new System.Drawing.Size(173, 25);
            this.TxtShortName.TabIndex = 6;
            this.TxtShortName.Validating += new System.ComponentModel.CancelEventHandler(this.TxtShortName_Validating);
            // 
            // lblSl_Ledger
            // 
            this.lblSl_Ledger.AutoSize = true;
            this.lblSl_Ledger.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSl_Ledger.Location = new System.Drawing.Point(9, 160);
            this.lblSl_Ledger.Name = "lblSl_Ledger";
            this.lblSl_Ledger.Size = new System.Drawing.Size(60, 19);
            this.lblSl_Ledger.TabIndex = 0;
            this.lblSl_Ledger.Text = "Ledger";
            // 
            // TxtAddress
            // 
            this.TxtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAddress.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAddress.Location = new System.Drawing.Point(109, 100);
            this.TxtAddress.MaxLength = 50;
            this.TxtAddress.Name = "TxtAddress";
            this.TxtAddress.Size = new System.Drawing.Size(342, 25);
            this.TxtAddress.TabIndex = 7;
            // 
            // TxtPhoneNumber
            // 
            this.TxtPhoneNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPhoneNumber.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPhoneNumber.Location = new System.Drawing.Point(109, 128);
            this.TxtPhoneNumber.Name = "TxtPhoneNumber";
            this.TxtPhoneNumber.Size = new System.Drawing.Size(342, 25);
            this.TxtPhoneNumber.TabIndex = 8;
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseForeColor = true;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.BtnExit.Location = new System.Drawing.Point(401, 6);
            this.BtnExit.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(77, 33);
            this.BtnExit.TabIndex = 4;
            this.BtnExit.Text = "&EXIT";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // TxtLedger
            // 
            this.TxtLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtLedger.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtLedger.Location = new System.Drawing.Point(109, 157);
            this.TxtLedger.Name = "TxtLedger";
            this.TxtLedger.ReadOnly = true;
            this.TxtLedger.Size = new System.Drawing.Size(342, 25);
            this.TxtLedger.TabIndex = 9;
            this.TxtLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtLedger_KeyDown);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEdit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnEdit.Appearance.Options.UseFont = true;
            this.BtnEdit.Appearance.Options.UseForeColor = true;
            this.BtnEdit.ImageOptions.Image = global::MrBLL.Properties.Resources.Edit;
            this.BtnEdit.Location = new System.Drawing.Point(84, 6);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(73, 33);
            this.BtnEdit.TabIndex = 1;
            this.BtnEdit.Tag = "EditButtonCheck";
            this.BtnEdit.Text = "&EDIT";
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // ChkActive
            // 
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkActive.Location = new System.Drawing.Point(9, 200);
            this.ChkActive.Margin = new System.Windows.Forms.Padding(2);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkActive.Size = new System.Drawing.Size(89, 20);
            this.ChkActive.TabIndex = 12;
            this.ChkActive.Text = "Active";
            this.ChkActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkActive.UseVisualStyleBackColor = true;
            // 
            // BtnNew
            // 
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNew.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.Appearance.Options.UseForeColor = true;
            this.BtnNew.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnNew.Location = new System.Drawing.Point(9, 6);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(75, 33);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Tag = "NewButtonCheck";
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Appearance.Options.UseForeColor = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(283, 193);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(89, 33);
            this.BtnSave.TabIndex = 10;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.Appearance.Options.UseForeColor = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(157, 6);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(100, 33);
            this.BtnDelete.TabIndex = 2;
            this.BtnDelete.Tag = "DeleteButtonCheck";
            this.BtnDelete.Text = "&DELETE";
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.Appearance.Options.UseForeColor = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.BtnCancel.Location = new System.Drawing.Point(372, 193);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(104, 33);
            this.BtnCancel.TabIndex = 11;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(3, 40);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(478, 2);
            this.clsSeparator1.TabIndex = 32;
            this.clsSeparator1.TabStop = false;
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(0, 186);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(484, 2);
            this.clsSeparator2.TabIndex = 33;
            this.clsSeparator2.TabStop = false;
            // 
            // BtnDescription
            // 
            this.BtnDescription.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnDescription.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnDescription.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDescription.Location = new System.Drawing.Point(452, 46);
            this.BtnDescription.Name = "BtnDescription";
            this.BtnDescription.Size = new System.Drawing.Size(27, 26);
            this.BtnDescription.TabIndex = 309;
            this.BtnDescription.TabStop = false;
            this.BtnDescription.UseVisualStyleBackColor = false;
            this.BtnDescription.Click += new System.EventHandler(this.BtnDescription_Click);
            // 
            // BtnLedger
            // 
            this.BtnLedger.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnLedger.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnLedger.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnLedger.Location = new System.Drawing.Point(452, 156);
            this.BtnLedger.Name = "BtnLedger";
            this.BtnLedger.Size = new System.Drawing.Size(27, 26);
            this.BtnLedger.TabIndex = 310;
            this.BtnLedger.TabStop = false;
            this.BtnLedger.UseVisualStyleBackColor = false;
            this.BtnLedger.Click += new System.EventHandler(this.BtnLedger_Click);
            // 
            // BtnView
            // 
            this.BtnView.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnView.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnView.Appearance.Options.UseFont = true;
            this.BtnView.Appearance.Options.UseForeColor = true;
            this.BtnView.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnView.Location = new System.Drawing.Point(257, 6);
            this.BtnView.Margin = new System.Windows.Forms.Padding(4);
            this.BtnView.Name = "BtnView";
            this.BtnView.Size = new System.Drawing.Size(78, 33);
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
            this.StorePanel.Controls.Add(this.BtnLedger);
            this.StorePanel.Controls.Add(this.BtnDescription);
            this.StorePanel.Controls.Add(this.clsSeparator2);
            this.StorePanel.Controls.Add(this.clsSeparator1);
            this.StorePanel.Controls.Add(this.BtnCancel);
            this.StorePanel.Controls.Add(this.BtnDelete);
            this.StorePanel.Controls.Add(this.BtnSave);
            this.StorePanel.Controls.Add(this.BtnNew);
            this.StorePanel.Controls.Add(this.ChkActive);
            this.StorePanel.Controls.Add(this.BtnEdit);
            this.StorePanel.Controls.Add(this.TxtLedger);
            this.StorePanel.Controls.Add(this.BtnExit);
            this.StorePanel.Controls.Add(this.TxtPhoneNumber);
            this.StorePanel.Controls.Add(this.TxtAddress);
            this.StorePanel.Controls.Add(this.lblSl_Ledger);
            this.StorePanel.Controls.Add(this.TxtShortName);
            this.StorePanel.Controls.Add(this.lblSl_PhoneNumber);
            this.StorePanel.Controls.Add(this.lblSl_Name);
            this.StorePanel.Controls.Add(this.TxtDescription);
            this.StorePanel.Controls.Add(this.lblSl_Code);
            this.StorePanel.Controls.Add(this.lblSl_Address);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(484, 232);
            this.StorePanel.TabIndex = 0;
            // 
            // BtnSync
            // 
            this.BtnSync.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSync.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSync.Appearance.Options.UseFont = true;
            this.BtnSync.Appearance.Options.UseForeColor = true;
            this.BtnSync.ImageOptions.Image = global::MrBLL.Properties.Resources.ReportRefresh;
            this.BtnSync.Location = new System.Drawing.Point(183, 192);
            this.BtnSync.Margin = new System.Windows.Forms.Padding(4);
            this.BtnSync.Name = "BtnSync";
            this.BtnSync.Size = new System.Drawing.Size(100, 35);
            this.BtnSync.TabIndex = 41;
            this.BtnSync.Text = "&SYNC";
            this.BtnSync.Click += new System.EventHandler(this.BtnSync_Click);
            // 
            // FrmSubLedger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(484, 232);
            this.Controls.Add(this.StorePanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmSubLedger";
            this.ShowIcon = false;
            this.Tag = "SubLedger";
            this.Text = "SubLedger Setup";
            this.Load += new System.EventHandler(this.FrmSubLedger_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmSubLedger_KeyPress);
            this.StorePanel.ResumeLayout(false);
            this.StorePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSl_Address;
        private System.Windows.Forms.Label lblSl_Code;
        private MrTextBox TxtDescription;
        private System.Windows.Forms.Label lblSl_Name;
        private System.Windows.Forms.Label lblSl_PhoneNumber;
        private MrTextBox TxtShortName;
        private System.Windows.Forms.Label lblSl_Ledger;
        private MrTextBox TxtAddress;
        private MrTextBox TxtPhoneNumber;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private MrTextBox TxtLedger;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private System.Windows.Forms.CheckBox ChkActive;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private ClsSeparator clsSeparator1;
        private ClsSeparator clsSeparator2;
        private System.Windows.Forms.Button BtnDescription;
        private System.Windows.Forms.Button BtnLedger;
        private DevExpress.XtraEditors.SimpleButton BtnView;
        private MrPanel StorePanel;
        private DevExpress.XtraEditors.SimpleButton BtnSync;
    }
}