using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.Hospital.Master
{
    partial class FrmDoctor
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
            this.TxtAddress = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ChkActive = new System.Windows.Forms.CheckBox();
            this.lblGrpPrimary = new System.Windows.Forms.Label();
            this.TxtContactNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblGrpSchedule = new System.Windows.Forms.Label();
            this.TxtShortName = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblGrpCode = new System.Windows.Forms.Label();
            this.TxtDescription = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblGrpName = new System.Windows.Forms.Label();
            this.PanelHeader = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.mrTextBox1 = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnDescription = new System.Windows.Forms.Button();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.PanelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxtAddress
            // 
            this.TxtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAddress.Location = new System.Drawing.Point(112, 128);
            this.TxtAddress.MaxLength = 50;
            this.TxtAddress.Name = "TxtAddress";
            this.TxtAddress.Size = new System.Drawing.Size(367, 25);
            this.TxtAddress.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 19);
            this.label1.TabIndex = 27;
            this.label1.Text = "Address";
            // 
            // ChkActive
            // 
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Location = new System.Drawing.Point(8, 162);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkActive.Size = new System.Drawing.Size(123, 24);
            this.ChkActive.TabIndex = 11;
            this.ChkActive.Text = "Active";
            this.ChkActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkActive.UseVisualStyleBackColor = true;
            // 
            // lblGrpPrimary
            // 
            this.lblGrpPrimary.AutoSize = true;
            this.lblGrpPrimary.Location = new System.Drawing.Point(8, 104);
            this.lblGrpPrimary.Name = "lblGrpPrimary";
            this.lblGrpPrimary.Size = new System.Drawing.Size(75, 19);
            this.lblGrpPrimary.TabIndex = 6;
            this.lblGrpPrimary.Text = "Category";
            // 
            // TxtContactNo
            // 
            this.TxtContactNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtContactNo.Location = new System.Drawing.Point(352, 72);
            this.TxtContactNo.MaxLength = 15;
            this.TxtContactNo.Name = "TxtContactNo";
            this.TxtContactNo.Size = new System.Drawing.Size(127, 25);
            this.TxtContactNo.TabIndex = 6;
            // 
            // lblGrpSchedule
            // 
            this.lblGrpSchedule.AutoSize = true;
            this.lblGrpSchedule.Location = new System.Drawing.Point(257, 75);
            this.lblGrpSchedule.Name = "lblGrpSchedule";
            this.lblGrpSchedule.Size = new System.Drawing.Size(91, 19);
            this.lblGrpSchedule.TabIndex = 4;
            this.lblGrpSchedule.Text = "Contact No";
            // 
            // TxtShortName
            // 
            this.TxtShortName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtShortName.Location = new System.Drawing.Point(112, 73);
            this.TxtShortName.MaxLength = 50;
            this.TxtShortName.Name = "TxtShortName";
            this.TxtShortName.Size = new System.Drawing.Size(124, 25);
            this.TxtShortName.TabIndex = 5;
            this.TxtShortName.TextChanged += new System.EventHandler(this.TxtShortName_TextChanged);
            // 
            // lblGrpCode
            // 
            this.lblGrpCode.AutoSize = true;
            this.lblGrpCode.Location = new System.Drawing.Point(8, 76);
            this.lblGrpCode.Name = "lblGrpCode";
            this.lblGrpCode.Size = new System.Drawing.Size(93, 19);
            this.lblGrpCode.TabIndex = 2;
            this.lblGrpCode.Text = "ShortName";
            // 
            // TxtDescription
            // 
            this.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDescription.Location = new System.Drawing.Point(112, 45);
            this.TxtDescription.MaxLength = 150;
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(367, 25);
            this.TxtDescription.TabIndex = 4;
            // 
            // lblGrpName
            // 
            this.lblGrpName.AutoSize = true;
            this.lblGrpName.Location = new System.Drawing.Point(8, 48);
            this.lblGrpName.Name = "lblGrpName";
            this.lblGrpName.Size = new System.Drawing.Size(95, 19);
            this.lblGrpName.TabIndex = 0;
            this.lblGrpName.Text = "Description";
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.button1);
            this.PanelHeader.Controls.Add(this.mrTextBox1);
            this.PanelHeader.Controls.Add(this.BtnDescription);
            this.PanelHeader.Controls.Add(this.BtnSave);
            this.PanelHeader.Controls.Add(this.BtnCancel);
            this.PanelHeader.Controls.Add(this.clsSeparator2);
            this.PanelHeader.Controls.Add(this.BtnExit);
            this.PanelHeader.Controls.Add(this.BtnEdit);
            this.PanelHeader.Controls.Add(this.BtnNew);
            this.PanelHeader.Controls.Add(this.BtnDelete);
            this.PanelHeader.Controls.Add(this.clsSeparator1);
            this.PanelHeader.Controls.Add(this.TxtAddress);
            this.PanelHeader.Controls.Add(this.label1);
            this.PanelHeader.Controls.Add(this.ChkActive);
            this.PanelHeader.Controls.Add(this.TxtDescription);
            this.PanelHeader.Controls.Add(this.lblGrpPrimary);
            this.PanelHeader.Controls.Add(this.TxtContactNo);
            this.PanelHeader.Controls.Add(this.lblGrpSchedule);
            this.PanelHeader.Controls.Add(this.TxtShortName);
            this.PanelHeader.Controls.Add(this.lblGrpName);
            this.PanelHeader.Controls.Add(this.lblGrpCode);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(510, 196);
            this.PanelHeader.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.button1.ForeColor = System.Drawing.SystemColors.Window;
            this.button1.Image = global::MrBLL.Properties.Resources.search16;
            this.button1.Location = new System.Drawing.Point(481, 103);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(27, 25);
            this.button1.TabIndex = 317;
            this.button1.TabStop = false;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // mrTextBox1
            // 
            this.mrTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mrTextBox1.Location = new System.Drawing.Point(112, 101);
            this.mrTextBox1.MaxLength = 150;
            this.mrTextBox1.Name = "mrTextBox1";
            this.mrTextBox1.Size = new System.Drawing.Size(367, 25);
            this.mrTextBox1.TabIndex = 316;
            // 
            // BtnDescription
            // 
            this.BtnDescription.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.BtnDescription.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnDescription.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDescription.Location = new System.Drawing.Point(481, 45);
            this.BtnDescription.Name = "BtnDescription";
            this.BtnDescription.Size = new System.Drawing.Size(27, 25);
            this.BtnDescription.TabIndex = 315;
            this.BtnDescription.TabStop = false;
            this.BtnDescription.UseVisualStyleBackColor = false;
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(310, 160);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(93, 34);
            this.BtnSave.TabIndex = 9;
            this.BtnSave.Text = "&SAVE";
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.BtnCancel.Location = new System.Drawing.Point(404, 160);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(104, 34);
            this.BtnCancel.TabIndex = 10;
            this.BtnCancel.Text = "&CANCEL";
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(3, 39);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(503, 2);
            this.clsSeparator2.TabIndex = 218;
            this.clsSeparator2.TabStop = false;
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.BtnExit.Location = new System.Drawing.Point(418, 4);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(87, 33);
            this.BtnExit.TabIndex = 3;
            this.BtnExit.Text = "E&XIT";
            // 
            // BtnEdit
            // 
            this.BtnEdit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEdit.Appearance.Options.UseFont = true;
            this.BtnEdit.ImageOptions.Image = global::MrBLL.Properties.Resources.Edit;
            this.BtnEdit.Location = new System.Drawing.Point(80, 4);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(75, 33);
            this.BtnEdit.TabIndex = 1;
            this.BtnEdit.Tag = "EditButtonCheck";
            this.BtnEdit.Text = "&EDIT";
            // 
            // BtnNew
            // 
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnNew.Location = new System.Drawing.Point(4, 4);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(75, 33);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Tag = "NewButtonCheck";
            this.BtnNew.Text = "&NEW";
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(156, 4);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(101, 33);
            this.BtnDelete.TabIndex = 2;
            this.BtnDelete.Tag = "DeleteButtonCheck";
            this.BtnDelete.Text = "&DELETE";
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(3, 156);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(503, 2);
            this.clsSeparator1.TabIndex = 217;
            this.clsSeparator1.TabStop = false;
            // 
            // FrmDoctor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 196);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "FrmDoctor";
            this.ShowIcon = false;
            this.Tag = "Doctor Master";
            this.Text = "Doctor Setup";
            this.Load += new System.EventHandler(this.FrmDoctor_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmDoctor_KeyPress);
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox ChkActive;
        private System.Windows.Forms.Label lblGrpSchedule;
        private System.Windows.Forms.Label lblGrpCode;
        private System.Windows.Forms.Label lblGrpName;
        private System.Windows.Forms.Label lblGrpPrimary;
        private System.Windows.Forms.Label label1;
        private ClsSeparator clsSeparator1;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private ClsSeparator clsSeparator2;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        public System.Windows.Forms.Button BtnDescription;
        private MrTextBox TxtContactNo;
        private MrTextBox TxtShortName;
        private MrTextBox TxtDescription;
        private MrTextBox TxtAddress;
        public System.Windows.Forms.Button button1;
        private MrTextBox mrTextBox1;
        private MrPanel PanelHeader;
    }
}