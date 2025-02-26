using MrDAL.Control.ControlsEx.Control;

namespace MrClientManagement.Master
{
    partial class FrmClientInformation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmClientInformation));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.TxtSource = new MrTextBox();
            this.TxtPhoneNo = new MrTextBox();
            this.TxtContactNo = new MrTextBox();
            this.TxtEmailAddress = new MrTextBox();
            this.TxtAddress = new MrTextBox();
            this.TxtPanNo = new MrTextBox();
            this.TxtDescription = new MrTextBox();
            this.ChkActive = new System.Windows.Forms.CheckBox();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.clsSeparator2 = new ClsSeparator();
            this.BtnSource = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.clsSeparator1 = new ClsSeparator();
            this.BtnView = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDescription = new System.Windows.Forms.Button();
            this.lblGrpName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelControl1.Appearance.Options.UseFont = true;
            this.panelControl1.Controls.Add(this.TxtSource);
            this.panelControl1.Controls.Add(this.TxtPhoneNo);
            this.panelControl1.Controls.Add(this.TxtContactNo);
            this.panelControl1.Controls.Add(this.TxtEmailAddress);
            this.panelControl1.Controls.Add(this.TxtAddress);
            this.panelControl1.Controls.Add(this.TxtPanNo);
            this.panelControl1.Controls.Add(this.TxtDescription);
            this.panelControl1.Controls.Add(this.ChkActive);
            this.panelControl1.Controls.Add(this.BtnCancel);
            this.panelControl1.Controls.Add(this.BtnSave);
            this.panelControl1.Controls.Add(this.clsSeparator2);
            this.panelControl1.Controls.Add(this.BtnSource);
            this.panelControl1.Controls.Add(this.label6);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.label5);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.clsSeparator1);
            this.panelControl1.Controls.Add(this.BtnView);
            this.panelControl1.Controls.Add(this.BtnEdit);
            this.panelControl1.Controls.Add(this.BtnDelete);
            this.panelControl1.Controls.Add(this.BtnNew);
            this.panelControl1.Controls.Add(this.BtnExit);
            this.panelControl1.Controls.Add(this.BtnDescription);
            this.panelControl1.Controls.Add(this.lblGrpName);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(593, 294);
            this.panelControl1.TabIndex = 0;
            // 
            // TxtSource
            // 
            this.TxtSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSource.Location = new System.Drawing.Point(108, 218);
            this.TxtSource.Name = "TxtSource";
            this.TxtSource.Size = new System.Drawing.Size(450, 25);
            this.TxtSource.TabIndex = 11;
            this.TxtSource.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSource_KeyDown);
            this.TxtSource.Validating += new System.ComponentModel.CancelEventHandler(this.TxtSource_Validating);
            // 
            // TxtPhoneNo
            // 
            this.TxtPhoneNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPhoneNo.Location = new System.Drawing.Point(108, 189);
            this.TxtPhoneNo.Name = "TxtPhoneNo";
            this.TxtPhoneNo.Size = new System.Drawing.Size(261, 25);
            this.TxtPhoneNo.TabIndex = 10;
            // 
            // TxtContactNo
            // 
            this.TxtContactNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtContactNo.Location = new System.Drawing.Point(108, 160);
            this.TxtContactNo.Name = "TxtContactNo";
            this.TxtContactNo.Size = new System.Drawing.Size(261, 25);
            this.TxtContactNo.TabIndex = 9;
            // 
            // TxtEmailAddress
            // 
            this.TxtEmailAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtEmailAddress.Location = new System.Drawing.Point(108, 131);
            this.TxtEmailAddress.Name = "TxtEmailAddress";
            this.TxtEmailAddress.Size = new System.Drawing.Size(449, 25);
            this.TxtEmailAddress.TabIndex = 8;
            // 
            // TxtAddress
            // 
            this.TxtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAddress.Location = new System.Drawing.Point(108, 103);
            this.TxtAddress.Name = "TxtAddress";
            this.TxtAddress.Size = new System.Drawing.Size(449, 25);
            this.TxtAddress.TabIndex = 7;
            // 
            // TxtPanNo
            // 
            this.TxtPanNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPanNo.Location = new System.Drawing.Point(108, 74);
            this.TxtPanNo.Name = "TxtPanNo";
            this.TxtPanNo.Size = new System.Drawing.Size(261, 25);
            this.TxtPanNo.TabIndex = 6;
            this.TxtPanNo.TextBoxType = TextBoxType.Numeric;
            // 
            // TxtDescription
            // 
            this.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDescription.Location = new System.Drawing.Point(108, 45);
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(450, 25);
            this.TxtDescription.TabIndex = 5;
            this.TxtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDescription_KeyDown);
            this.TxtDescription.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDescription_Validating);
            // 
            // ChkActive
            // 
            this.ChkActive.AutoSize = true;
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Location = new System.Drawing.Point(11, 258);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkActive.Size = new System.Drawing.Size(73, 23);
            this.ChkActive.TabIndex = 14;
            this.ChkActive.Text = "Active";
            this.ChkActive.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ChkActive.UseVisualStyleBackColor = true;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.Appearance.Options.UseForeColor = true;
            this.BtnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnCancel.ImageOptions.Image")));
            this.BtnCancel.Location = new System.Drawing.Point(472, 256);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(116, 33);
            this.BtnCancel.TabIndex = 13;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Appearance.Options.UseForeColor = true;
            this.BtnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnSave.ImageOptions.Image")));
            this.BtnSave.Location = new System.Drawing.Point(375, 256);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(94, 33);
            this.BtnSave.TabIndex = 12;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator2.Location = new System.Drawing.Point(0, 248);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(591, 2);
            this.clsSeparator2.TabIndex = 243;
            this.clsSeparator2.TabStop = false;
            // 
            // BtnSource
            // 
            this.BtnSource.CausesValidation = false;
            this.BtnSource.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnSource.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnSource.Image = global::MrClientManagement.Properties.Resources.search16;
            this.BtnSource.Location = new System.Drawing.Point(563, 216);
            this.BtnSource.Name = "BtnSource";
            this.BtnSource.Size = new System.Drawing.Size(29, 26);
            this.BtnSource.TabIndex = 254;
            this.BtnSource.TabStop = false;
            this.BtnSource.UseVisualStyleBackColor = false;
            this.BtnSource.Click += new System.EventHandler(this.BtnSource_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label6.Location = new System.Drawing.Point(6, 221);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 19);
            this.label6.TabIndex = 253;
            this.label6.Text = "Source";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label4.Location = new System.Drawing.Point(6, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 19);
            this.label4.TabIndex = 251;
            this.label4.Text = "Phone No";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label5.Location = new System.Drawing.Point(6, 163);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 19);
            this.label5.TabIndex = 249;
            this.label5.Text = "Contact No";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label3.Location = new System.Drawing.Point(6, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 19);
            this.label3.TabIndex = 247;
            this.label3.Text = "Email";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label2.Location = new System.Drawing.Point(6, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 19);
            this.label2.TabIndex = 245;
            this.label2.Text = "Address";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label1.Location = new System.Drawing.Point(6, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 19);
            this.label1.TabIndex = 243;
            this.label1.Text = "Pan No";
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator1.Location = new System.Drawing.Point(1, 41);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(590, 2);
            this.clsSeparator1.TabIndex = 242;
            this.clsSeparator1.TabStop = false;
            // 
            // BtnView
            // 
            this.BtnView.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnView.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnView.Appearance.Options.UseFont = true;
            this.BtnView.Appearance.Options.UseForeColor = true;
            this.BtnView.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnView.ImageOptions.SvgImage")));
            this.BtnView.Location = new System.Drawing.Point(275, 5);
            this.BtnView.Margin = new System.Windows.Forms.Padding(4);
            this.BtnView.Name = "BtnView";
            this.BtnView.Size = new System.Drawing.Size(94, 33);
            this.BtnView.TabIndex = 3;
            this.BtnView.Text = "&VIEW";
            this.BtnView.Click += new System.EventHandler(this.BtnView_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEdit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnEdit.Appearance.Options.UseFont = true;
            this.BtnEdit.Appearance.Options.UseForeColor = true;
            this.BtnEdit.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnEdit.ImageOptions.SvgImage")));
            this.BtnEdit.Location = new System.Drawing.Point(83, 5);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(82, 33);
            this.BtnEdit.TabIndex = 1;
            this.BtnEdit.Text = "&EDIT";
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.Appearance.Options.UseForeColor = true;
            this.BtnDelete.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnDelete.ImageOptions.SvgImage")));
            this.BtnDelete.Location = new System.Drawing.Point(166, 5);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(108, 33);
            this.BtnDelete.TabIndex = 2;
            this.BtnDelete.Text = "&DELETE";
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnNew
            // 
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNew.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.Appearance.Options.UseForeColor = true;
            this.BtnNew.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnNew.ImageOptions.SvgImage")));
            this.BtnNew.Location = new System.Drawing.Point(1, 5);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(81, 33);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseForeColor = true;
            this.BtnExit.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnExit.ImageOptions.SvgImage")));
            this.BtnExit.Location = new System.Drawing.Point(504, 2);
            this.BtnExit.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(87, 33);
            this.BtnExit.TabIndex = 4;
            this.BtnExit.Text = "E&XIT";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnDescription
            // 
            this.BtnDescription.CausesValidation = false;
            this.BtnDescription.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnDescription.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnDescription.Image = global::MrClientManagement.Properties.Resources.search16;
            this.BtnDescription.Location = new System.Drawing.Point(563, 44);
            this.BtnDescription.Name = "BtnDescription";
            this.BtnDescription.Size = new System.Drawing.Size(29, 26);
            this.BtnDescription.TabIndex = 189;
            this.BtnDescription.TabStop = false;
            this.BtnDescription.UseVisualStyleBackColor = false;
            this.BtnDescription.Click += new System.EventHandler(this.BtnDescription_Click);
            // 
            // lblGrpName
            // 
            this.lblGrpName.AutoSize = true;
            this.lblGrpName.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lblGrpName.Location = new System.Drawing.Point(6, 48);
            this.lblGrpName.Name = "lblGrpName";
            this.lblGrpName.Size = new System.Drawing.Size(95, 19);
            this.lblGrpName.TabIndex = 186;
            this.lblGrpName.Text = "Description";
            // 
            // FrmClientInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 294);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmClientInformation";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CLIENT INFORMATION";
            this.Load += new System.EventHandler(this.FrmClientInformation_Load);
            this.Shown += new System.EventHandler(this.FrmClientInformation_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmClientInformation_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Button BtnDescription;
        private System.Windows.Forms.Label lblGrpName;
        private DevExpress.XtraEditors.SimpleButton BtnView;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private ClsSeparator clsSeparator1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button BtnSource;
        private ClsSeparator clsSeparator2;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private System.Windows.Forms.CheckBox ChkActive;
        private MrTextBox TxtDescription;
        private MrTextBox TxtSource;
        private MrTextBox TxtPhoneNo;
        private MrTextBox TxtContactNo;
        private MrTextBox TxtEmailAddress;
        private MrTextBox TxtAddress;
        private MrTextBox TxtPanNo;
    }
}