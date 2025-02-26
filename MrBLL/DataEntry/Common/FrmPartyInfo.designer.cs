using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.DataEntry.Common
{
    partial class FrmPartyInfo
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
            this.MskChequeDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.lbl_ChqDate = new System.Windows.Forms.Label();
            this.TxtChequeNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_ChqNo = new System.Windows.Forms.Label();
            this.BtnGeneralLedger = new System.Windows.Forms.Button();
            this.TxtContactNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_Mob = new System.Windows.Forms.Label();
            this.TxtAddress = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_Address = new System.Windows.Forms.Label();
            this.TxtContactPerson = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_ContactPerson = new System.Windows.Forms.Label();
            this.TxtTPanNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_VatPan = new System.Windows.Forms.Label();
            this.TxtDescription = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_Partyname = new System.Windows.Forms.Label();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.StorePanel = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.mrGroup1 = new MrDAL.Control.ControlsEx.Control.MrGroup();
            this.TxtEmailAddress = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.MskChequeMiti = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.StorePanel.SuspendLayout();
            this.mrGroup1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MskChequeDate
            // 
            this.MskChequeDate.BackColor = System.Drawing.SystemColors.HighlightText;
            this.MskChequeDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskChequeDate.Enabled = false;
            this.MskChequeDate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.MskChequeDate.ForeColor = System.Drawing.Color.Black;
            this.MskChequeDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskChequeDate.Location = new System.Drawing.Point(140, 81);
            this.MskChequeDate.Mask = "00/00/0000";
            this.MskChequeDate.Name = "MskChequeDate";
            this.MskChequeDate.Size = new System.Drawing.Size(122, 25);
            this.MskChequeDate.TabIndex = 2;
            // 
            // lbl_ChqDate
            // 
            this.lbl_ChqDate.AutoSize = true;
            this.lbl_ChqDate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.lbl_ChqDate.ForeColor = System.Drawing.Color.Black;
            this.lbl_ChqDate.Location = new System.Drawing.Point(11, 85);
            this.lbl_ChqDate.Name = "lbl_ChqDate";
            this.lbl_ChqDate.Size = new System.Drawing.Size(107, 19);
            this.lbl_ChqDate.TabIndex = 63;
            this.lbl_ChqDate.Text = "Cheque Date";
            // 
            // TxtChequeNo
            // 
            this.TxtChequeNo.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtChequeNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtChequeNo.Enabled = false;
            this.TxtChequeNo.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.TxtChequeNo.ForeColor = System.Drawing.Color.Black;
            this.TxtChequeNo.Location = new System.Drawing.Point(140, 54);
            this.TxtChequeNo.MaxLength = 255;
            this.TxtChequeNo.Name = "TxtChequeNo";
            this.TxtChequeNo.Size = new System.Drawing.Size(250, 25);
            this.TxtChequeNo.TabIndex = 1;
            // 
            // lbl_ChqNo
            // 
            this.lbl_ChqNo.AutoSize = true;
            this.lbl_ChqNo.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.lbl_ChqNo.ForeColor = System.Drawing.Color.Black;
            this.lbl_ChqNo.Location = new System.Drawing.Point(11, 57);
            this.lbl_ChqNo.Name = "lbl_ChqNo";
            this.lbl_ChqNo.Size = new System.Drawing.Size(90, 19);
            this.lbl_ChqNo.TabIndex = 62;
            this.lbl_ChqNo.Text = "Cheque No";
            // 
            // BtnGeneralLedger
            // 
            this.BtnGeneralLedger.CausesValidation = false;
            this.BtnGeneralLedger.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.BtnGeneralLedger.ForeColor = System.Drawing.Color.Black;
            this.BtnGeneralLedger.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnGeneralLedger.Location = new System.Drawing.Point(534, 27);
            this.BtnGeneralLedger.Name = "BtnGeneralLedger";
            this.BtnGeneralLedger.Size = new System.Drawing.Size(28, 24);
            this.BtnGeneralLedger.TabIndex = 11;
            this.BtnGeneralLedger.TabStop = false;
            this.BtnGeneralLedger.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BtnGeneralLedger.UseVisualStyleBackColor = false;
            this.BtnGeneralLedger.Click += new System.EventHandler(this.BtnGeneralLedger_Click);
            // 
            // TxtContactNo
            // 
            this.TxtContactNo.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtContactNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtContactNo.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.TxtContactNo.ForeColor = System.Drawing.Color.Black;
            this.TxtContactNo.Location = new System.Drawing.Point(140, 137);
            this.TxtContactNo.MaxLength = 50;
            this.TxtContactNo.Name = "TxtContactNo";
            this.TxtContactNo.Size = new System.Drawing.Size(250, 25);
            this.TxtContactNo.TabIndex = 5;
            // 
            // lbl_Mob
            // 
            this.lbl_Mob.AutoSize = true;
            this.lbl_Mob.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.lbl_Mob.ForeColor = System.Drawing.Color.Black;
            this.lbl_Mob.Location = new System.Drawing.Point(11, 139);
            this.lbl_Mob.Name = "lbl_Mob";
            this.lbl_Mob.Size = new System.Drawing.Size(91, 19);
            this.lbl_Mob.TabIndex = 53;
            this.lbl_Mob.Text = "Contact No";
            // 
            // TxtAddress
            // 
            this.TxtAddress.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAddress.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.TxtAddress.ForeColor = System.Drawing.Color.Black;
            this.TxtAddress.Location = new System.Drawing.Point(140, 193);
            this.TxtAddress.MaxLength = 50;
            this.TxtAddress.Name = "TxtAddress";
            this.TxtAddress.Size = new System.Drawing.Size(392, 25);
            this.TxtAddress.TabIndex = 7;
            // 
            // lbl_Address
            // 
            this.lbl_Address.AutoSize = true;
            this.lbl_Address.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.lbl_Address.ForeColor = System.Drawing.Color.Black;
            this.lbl_Address.Location = new System.Drawing.Point(11, 196);
            this.lbl_Address.Name = "lbl_Address";
            this.lbl_Address.Size = new System.Drawing.Size(69, 19);
            this.lbl_Address.TabIndex = 53;
            this.lbl_Address.Text = "Address";
            // 
            // TxtContactPerson
            // 
            this.TxtContactPerson.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtContactPerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtContactPerson.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.TxtContactPerson.ForeColor = System.Drawing.Color.Black;
            this.TxtContactPerson.Location = new System.Drawing.Point(140, 165);
            this.TxtContactPerson.MaxLength = 50;
            this.TxtContactPerson.Name = "TxtContactPerson";
            this.TxtContactPerson.Size = new System.Drawing.Size(392, 25);
            this.TxtContactPerson.TabIndex = 6;
            // 
            // lbl_ContactPerson
            // 
            this.lbl_ContactPerson.AutoSize = true;
            this.lbl_ContactPerson.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.lbl_ContactPerson.ForeColor = System.Drawing.Color.Black;
            this.lbl_ContactPerson.Location = new System.Drawing.Point(11, 165);
            this.lbl_ContactPerson.Name = "lbl_ContactPerson";
            this.lbl_ContactPerson.Size = new System.Drawing.Size(123, 19);
            this.lbl_ContactPerson.TabIndex = 53;
            this.lbl_ContactPerson.Text = "Contact Person";
            // 
            // TxtTPanNo
            // 
            this.TxtTPanNo.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtTPanNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTPanNo.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.TxtTPanNo.ForeColor = System.Drawing.Color.Black;
            this.TxtTPanNo.Location = new System.Drawing.Point(140, 109);
            this.TxtTPanNo.MaxLength = 9;
            this.TxtTPanNo.Name = "TxtTPanNo";
            this.TxtTPanNo.Size = new System.Drawing.Size(250, 25);
            this.TxtTPanNo.TabIndex = 4;
            // 
            // lbl_VatPan
            // 
            this.lbl_VatPan.AutoSize = true;
            this.lbl_VatPan.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.lbl_VatPan.ForeColor = System.Drawing.Color.Black;
            this.lbl_VatPan.Location = new System.Drawing.Point(11, 112);
            this.lbl_VatPan.Name = "lbl_VatPan";
            this.lbl_VatPan.Size = new System.Drawing.Size(96, 19);
            this.lbl_VatPan.TabIndex = 53;
            this.lbl_VatPan.Text = "Vat/Pan No";
            // 
            // TxtDescription
            // 
            this.TxtDescription.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDescription.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.TxtDescription.ForeColor = System.Drawing.Color.Black;
            this.TxtDescription.Location = new System.Drawing.Point(140, 27);
            this.TxtDescription.MaxLength = 255;
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(392, 25);
            this.TxtDescription.TabIndex = 0;
            this.TxtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDescription_KeyDown);
            // 
            // lbl_Partyname
            // 
            this.lbl_Partyname.AutoSize = true;
            this.lbl_Partyname.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.lbl_Partyname.ForeColor = System.Drawing.Color.Black;
            this.lbl_Partyname.Location = new System.Drawing.Point(11, 30);
            this.lbl_Partyname.Name = "lbl_Partyname";
            this.lbl_Partyname.Size = new System.Drawing.Size(96, 19);
            this.lbl_Partyname.TabIndex = 53;
            this.lbl_Partyname.Text = "Party Name";
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnCancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.Appearance.Options.UseForeColor = true;
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.BtnCancel.Location = new System.Drawing.Point(392, 255);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(108, 38);
            this.BtnCancel.TabIndex = 10;
            this.BtnCancel.Text = "&CANCEL";
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Appearance.Options.UseForeColor = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(282, 255);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(108, 38);
            this.BtnSave.TabIndex = 9;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.mrGroup1);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(572, 300);
            this.StorePanel.TabIndex = 0;
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrDAL.Control.ControlsEx.Control.MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.TxtDescription);
            this.mrGroup1.Controls.Add(this.TxtEmailAddress);
            this.mrGroup1.Controls.Add(this.lbl_ContactPerson);
            this.mrGroup1.Controls.Add(this.label1);
            this.mrGroup1.Controls.Add(this.BtnGeneralLedger);
            this.mrGroup1.Controls.Add(this.MskChequeMiti);
            this.mrGroup1.Controls.Add(this.lbl_ChqNo);
            this.mrGroup1.Controls.Add(this.clsSeparator1);
            this.mrGroup1.Controls.Add(this.TxtContactPerson);
            this.mrGroup1.Controls.Add(this.BtnCancel);
            this.mrGroup1.Controls.Add(this.TxtTPanNo);
            this.mrGroup1.Controls.Add(this.BtnSave);
            this.mrGroup1.Controls.Add(this.TxtContactNo);
            this.mrGroup1.Controls.Add(this.MskChequeDate);
            this.mrGroup1.Controls.Add(this.TxtChequeNo);
            this.mrGroup1.Controls.Add(this.lbl_Partyname);
            this.mrGroup1.Controls.Add(this.lbl_Address);
            this.mrGroup1.Controls.Add(this.lbl_VatPan);
            this.mrGroup1.Controls.Add(this.TxtAddress);
            this.mrGroup1.Controls.Add(this.lbl_Mob);
            this.mrGroup1.Controls.Add(this.lbl_ChqDate);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "Fill Party Information";
            this.mrGroup1.Location = new System.Drawing.Point(2, 2);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 10;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = false;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(566, 295);
            this.mrGroup1.TabIndex = 0;
            // 
            // TxtEmailAddress
            // 
            this.TxtEmailAddress.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtEmailAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtEmailAddress.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.TxtEmailAddress.ForeColor = System.Drawing.Color.Black;
            this.TxtEmailAddress.Location = new System.Drawing.Point(140, 221);
            this.TxtEmailAddress.MaxLength = 50;
            this.TxtEmailAddress.Name = "TxtEmailAddress";
            this.TxtEmailAddress.Size = new System.Drawing.Size(392, 25);
            this.TxtEmailAddress.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(11, 224);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 19);
            this.label1.TabIndex = 66;
            this.label1.Text = "Email Address";
            // 
            // MskChequeMiti
            // 
            this.MskChequeMiti.BackColor = System.Drawing.SystemColors.HighlightText;
            this.MskChequeMiti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskChequeMiti.Enabled = false;
            this.MskChequeMiti.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.MskChequeMiti.ForeColor = System.Drawing.Color.Black;
            this.MskChequeMiti.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskChequeMiti.Location = new System.Drawing.Point(268, 81);
            this.MskChequeMiti.Mask = "00/00/0000";
            this.MskChequeMiti.Name = "MskChequeMiti";
            this.MskChequeMiti.Size = new System.Drawing.Size(122, 25);
            this.MskChequeMiti.TabIndex = 3;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(5, 250);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(557, 2);
            this.clsSeparator1.TabIndex = 64;
            this.clsSeparator1.TabStop = false;
            // 
            // FrmPartyInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(572, 300);
            this.Controls.Add(this.StorePanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPartyInfo";
            this.ShowIcon = false;
            this.Text = "CASH PARTY INFORMATION";
            this.Load += new System.EventHandler(this.FrmPartyInfo_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmPartyInfo_KeyPress);
            this.StorePanel.ResumeLayout(false);
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl_ChqDate;
        private System.Windows.Forms.Label lbl_ChqNo;
        private System.Windows.Forms.Button BtnGeneralLedger;
        private System.Windows.Forms.Label lbl_Mob;
        private System.Windows.Forms.Label lbl_Address;
        private System.Windows.Forms.Label lbl_ContactPerson;
        private System.Windows.Forms.Label lbl_VatPan;
        private System.Windows.Forms.Label lbl_Partyname;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private ClsSeparator clsSeparator1;
        private System.Windows.Forms.Label label1;
        private MrGroup mrGroup1;
        private MrMaskedTextBox MskChequeDate;
        private MrTextBox TxtChequeNo;
        private MrTextBox TxtContactNo;
        private MrTextBox TxtAddress;
        private MrTextBox TxtContactPerson;
        private MrTextBox TxtTPanNo;
        private MrTextBox TxtDescription;
        private MrMaskedTextBox MskChequeMiti;
        private MrTextBox TxtEmailAddress;
        private MrPanel StorePanel;
    }
}