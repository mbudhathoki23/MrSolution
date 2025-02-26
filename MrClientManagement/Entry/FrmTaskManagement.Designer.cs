using MrDAL.Control.ControlsEx.Control;

namespace MrClientManagement.Entry
{
    partial class FrmTaskManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTaskManagement));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.clsSeparator2 = new ClsSeparator();
            this.ChkActive = new System.Windows.Forms.CheckBox();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.clsSeparator1 = new ClsSeparator();
            this.TxtRoleUser = new MrTextBox();
            this.BtnView = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDescription = new System.Windows.Forms.Button();
            this.lblGrpName = new System.Windows.Forms.Label();
            this.TxtCompany = new MrTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtTaskType = new MrTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.TxtTaskStatus = new MrTextBox();
            this.mrTextBox4 = new MrTextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.panelControl1.Appearance.Options.UseFont = true;
            this.panelControl1.Appearance.Options.UseForeColor = true;
            this.panelControl1.Appearance.Options.UseTextOptions = true;
            this.panelControl1.Controls.Add(this.mrTextBox4);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.TxtTaskStatus);
            this.panelControl1.Controls.Add(this.button3);
            this.panelControl1.Controls.Add(this.TxtTaskType);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.button2);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.TxtCompany);
            this.panelControl1.Controls.Add(this.button1);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.clsSeparator2);
            this.panelControl1.Controls.Add(this.ChkActive);
            this.panelControl1.Controls.Add(this.BtnCancel);
            this.panelControl1.Controls.Add(this.BtnSave);
            this.panelControl1.Controls.Add(this.clsSeparator1);
            this.panelControl1.Controls.Add(this.TxtRoleUser);
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
            this.panelControl1.Size = new System.Drawing.Size(613, 361);
            this.panelControl1.TabIndex = 0;
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator2.Location = new System.Drawing.Point(4, 313);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(604, 2);
            this.clsSeparator2.TabIndex = 244;
            this.clsSeparator2.TabStop = false;
            // 
            // ChkActive
            // 
            this.ChkActive.AutoSize = true;
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Location = new System.Drawing.Point(18, 324);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkActive.Size = new System.Drawing.Size(73, 23);
            this.ChkActive.TabIndex = 13;
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
            this.BtnCancel.Location = new System.Drawing.Point(490, 318);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(116, 35);
            this.BtnCancel.TabIndex = 12;
            this.BtnCancel.Text = "&CANCEL";
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Appearance.Options.UseForeColor = true;
            this.BtnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnSave.ImageOptions.Image")));
            this.BtnSave.Location = new System.Drawing.Point(393, 318);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(94, 35);
            this.BtnSave.TabIndex = 10;
            this.BtnSave.Text = "&SAVE";
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator1.Location = new System.Drawing.Point(8, 40);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(600, 2);
            this.clsSeparator1.TabIndex = 243;
            this.clsSeparator1.TabStop = false;
            // 
            // TxtRoleUser
            // 
            this.TxtRoleUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtRoleUser.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.TxtRoleUser.Location = new System.Drawing.Point(114, 46);
            this.TxtRoleUser.Name = "TxtRoleUser";
            this.TxtRoleUser.Size = new System.Drawing.Size(465, 25);
            this.TxtRoleUser.TabIndex = 5;
            // 
            // BtnView
            // 
            this.BtnView.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnView.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnView.Appearance.Options.UseFont = true;
            this.BtnView.Appearance.Options.UseForeColor = true;
            this.BtnView.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnView.ImageOptions.SvgImage")));
            this.BtnView.Location = new System.Drawing.Point(281, 3);
            this.BtnView.Margin = new System.Windows.Forms.Padding(4);
            this.BtnView.Name = "BtnView";
            this.BtnView.Size = new System.Drawing.Size(94, 33);
            this.BtnView.TabIndex = 3;
            this.BtnView.Text = "&VIEW";
            // 
            // BtnEdit
            // 
            this.BtnEdit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEdit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnEdit.Appearance.Options.UseFont = true;
            this.BtnEdit.Appearance.Options.UseForeColor = true;
            this.BtnEdit.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnEdit.ImageOptions.SvgImage")));
            this.BtnEdit.Location = new System.Drawing.Point(89, 3);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(82, 33);
            this.BtnEdit.TabIndex = 1;
            this.BtnEdit.Text = "&EDIT";
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.Appearance.Options.UseForeColor = true;
            this.BtnDelete.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnDelete.ImageOptions.SvgImage")));
            this.BtnDelete.Location = new System.Drawing.Point(172, 3);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(108, 33);
            this.BtnDelete.TabIndex = 2;
            this.BtnDelete.Text = "&DELETE";
            // 
            // BtnNew
            // 
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNew.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.Appearance.Options.UseForeColor = true;
            this.BtnNew.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnNew.ImageOptions.SvgImage")));
            this.BtnNew.Location = new System.Drawing.Point(7, 3);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(81, 33);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Text = "&NEW";
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseForeColor = true;
            this.BtnExit.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnExit.ImageOptions.SvgImage")));
            this.BtnExit.Location = new System.Drawing.Point(522, 3);
            this.BtnExit.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(87, 33);
            this.BtnExit.TabIndex = 4;
            this.BtnExit.Text = "E&XIT";
            // 
            // BtnDescription
            // 
            this.BtnDescription.CausesValidation = false;
            this.BtnDescription.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnDescription.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnDescription.Image = global::MrClientManagement.Properties.Resources.search16;
            this.BtnDescription.Location = new System.Drawing.Point(580, 45);
            this.BtnDescription.Name = "BtnDescription";
            this.BtnDescription.Size = new System.Drawing.Size(29, 26);
            this.BtnDescription.TabIndex = 197;
            this.BtnDescription.TabStop = false;
            this.BtnDescription.UseVisualStyleBackColor = false;
            // 
            // lblGrpName
            // 
            this.lblGrpName.AutoSize = true;
            this.lblGrpName.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lblGrpName.Location = new System.Drawing.Point(12, 49);
            this.lblGrpName.Name = "lblGrpName";
            this.lblGrpName.Size = new System.Drawing.Size(83, 19);
            this.lblGrpName.TabIndex = 196;
            this.lblGrpName.Text = "Role User";
            // 
            // TxtCompany
            // 
            this.TxtCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCompany.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.TxtCompany.Location = new System.Drawing.Point(114, 74);
            this.TxtCompany.Name = "TxtCompany";
            this.TxtCompany.Size = new System.Drawing.Size(465, 25);
            this.TxtCompany.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.CausesValidation = false;
            this.button1.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.button1.ForeColor = System.Drawing.SystemColors.Window;
            this.button1.Image = global::MrClientManagement.Properties.Resources.search16;
            this.button1.Location = new System.Drawing.Point(580, 73);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(29, 26);
            this.button1.TabIndex = 247;
            this.button1.TabStop = false;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label1.Location = new System.Drawing.Point(12, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 19);
            this.label1.TabIndex = 246;
            this.label1.Text = "Company";
            // 
            // TxtTaskType
            // 
            this.TxtTaskType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTaskType.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.TxtTaskType.Location = new System.Drawing.Point(114, 103);
            this.TxtTaskType.Name = "TxtTaskType";
            this.TxtTaskType.Size = new System.Drawing.Size(465, 25);
            this.TxtTaskType.TabIndex = 7;
            // 
            // button2
            // 
            this.button2.CausesValidation = false;
            this.button2.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.button2.ForeColor = System.Drawing.SystemColors.Window;
            this.button2.Image = global::MrClientManagement.Properties.Resources.search16;
            this.button2.Location = new System.Drawing.Point(580, 102);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(29, 26);
            this.button2.TabIndex = 250;
            this.button2.TabStop = false;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label2.Location = new System.Drawing.Point(12, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 19);
            this.label2.TabIndex = 249;
            this.label2.Text = "Task Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label3.Location = new System.Drawing.Point(12, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 19);
            this.label3.TabIndex = 249;
            this.label3.Text = "Task Status";
            // 
            // button3
            // 
            this.button3.CausesValidation = false;
            this.button3.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.button3.ForeColor = System.Drawing.SystemColors.Window;
            this.button3.Image = global::MrClientManagement.Properties.Resources.search16;
            this.button3.Location = new System.Drawing.Point(580, 133);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(29, 26);
            this.button3.TabIndex = 250;
            this.button3.TabStop = false;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // TxtTaskStatus
            // 
            this.TxtTaskStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTaskStatus.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.TxtTaskStatus.Location = new System.Drawing.Point(114, 134);
            this.TxtTaskStatus.Name = "TxtTaskStatus";
            this.TxtTaskStatus.Size = new System.Drawing.Size(465, 25);
            this.TxtTaskStatus.TabIndex = 8;
            // 
            // mrTextBox4
            // 
            this.mrTextBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mrTextBox4.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.mrTextBox4.Location = new System.Drawing.Point(114, 163);
            this.mrTextBox4.Multiline = true;
            this.mrTextBox4.Name = "mrTextBox4";
            this.mrTextBox4.Size = new System.Drawing.Size(466, 146);
            this.mrTextBox4.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label4.Location = new System.Drawing.Point(12, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 19);
            this.label4.TabIndex = 252;
            this.label4.Text = "Remarks";
            // 
            // FrmTaskManagement
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 361);
            this.Controls.Add(this.panelControl1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "FrmTaskManagement";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TASK MANAGEMENT";
            this.Load += new System.EventHandler(this.FrmTaskManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private ClsSeparator clsSeparator2;
        private System.Windows.Forms.CheckBox ChkActive;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private ClsSeparator clsSeparator1;
        private MrTextBox TxtRoleUser;
        private DevExpress.XtraEditors.SimpleButton BtnView;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private System.Windows.Forms.Button BtnDescription;
        private System.Windows.Forms.Label lblGrpName;
        private MrTextBox TxtCompany;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private MrTextBox TxtTaskType;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private MrTextBox TxtTaskStatus;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label3;
        private MrTextBox mrTextBox4;
        private System.Windows.Forms.Label label4;
    }
}