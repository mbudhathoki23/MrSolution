using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.Hospital.Master
{
    partial class FrmHDepartment
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
            this.CmbLevel = new System.Windows.Forms.ComboBox();
            this.TxtShortName = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtDescription = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtCharge = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtDoctor = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.PanelHeader = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.BtnDoctorList = new System.Windows.Forms.Button();
            this.BtnDescription = new System.Windows.Forms.Button();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.PanelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChkActive
            // 
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Location = new System.Drawing.Point(12, 189);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkActive.Size = new System.Drawing.Size(113, 26);
            this.ChkActive.TabIndex = 8;
            this.ChkActive.Text = "Active";
            this.ChkActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkActive.UseVisualStyleBackColor = true;
            // 
            // CmbLevel
            // 
            this.CmbLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbLevel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbLevel.FormattingEnabled = true;
            this.CmbLevel.Items.AddRange(new object[] {
            "I",
            "II",
            "III",
            "IV"});
            this.CmbLevel.Location = new System.Drawing.Point(105, 124);
            this.CmbLevel.MaxLength = 10;
            this.CmbLevel.Name = "CmbLevel";
            this.CmbLevel.Size = new System.Drawing.Size(172, 27);
            this.CmbLevel.TabIndex = 7;
            this.CmbLevel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbLevel_KeyPress);
            // 
            // TxtShortName
            // 
            this.TxtShortName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtShortName.Location = new System.Drawing.Point(105, 96);
            this.TxtShortName.MaxLength = 50;
            this.TxtShortName.Name = "TxtShortName";
            this.TxtShortName.Size = new System.Drawing.Size(172, 25);
            this.TxtShortName.TabIndex = 6;
            this.TxtShortName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtShortName_KeyDown);
            this.TxtShortName.Validating += new System.ComponentModel.CancelEventHandler(this.TxtShortName_Validating);
            // 
            // TxtDescription
            // 
            this.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDescription.Location = new System.Drawing.Point(105, 43);
            this.TxtDescription.MaxLength = 50;
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(313, 25);
            this.TxtDescription.TabIndex = 4;
            this.TxtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDescription_KeyDown);
            this.TxtDescription.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDescription_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "Level";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "ShortName";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(7, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Description";
            // 
            // TxtCharge
            // 
            this.TxtCharge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCharge.Location = new System.Drawing.Point(105, 70);
            this.TxtCharge.MaxLength = 50;
            this.TxtCharge.Name = "TxtCharge";
            this.TxtCharge.Size = new System.Drawing.Size(172, 25);
            this.TxtCharge.TabIndex = 5;
            this.TxtCharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtCharge.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCharge_KeyPress);
            this.TxtCharge.Validating += new System.ComponentModel.CancelEventHandler(this.TxtCharge_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 19);
            this.label6.TabIndex = 34;
            this.label6.Text = "Charge";
            // 
            // TxtDoctor
            // 
            this.TxtDoctor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDoctor.Location = new System.Drawing.Point(105, 155);
            this.TxtDoctor.MaxLength = 50;
            this.TxtDoctor.Name = "TxtDoctor";
            this.TxtDoctor.Size = new System.Drawing.Size(313, 25);
            this.TxtDoctor.TabIndex = 8;
            this.TxtDoctor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDoctor_KeyDown);
            this.TxtDoctor.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDoctor_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 19);
            this.label4.TabIndex = 30;
            this.label4.Text = "Doctor";
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.BtnDoctorList);
            this.PanelHeader.Controls.Add(this.BtnDescription);
            this.PanelHeader.Controls.Add(this.BtnCancel);
            this.PanelHeader.Controls.Add(this.BtnSave);
            this.PanelHeader.Controls.Add(this.clsSeparator2);
            this.PanelHeader.Controls.Add(this.clsSeparator1);
            this.PanelHeader.Controls.Add(this.BtnNew);
            this.PanelHeader.Controls.Add(this.BtnExit);
            this.PanelHeader.Controls.Add(this.BtnDelete);
            this.PanelHeader.Controls.Add(this.BtnEdit);
            this.PanelHeader.Controls.Add(this.TxtCharge);
            this.PanelHeader.Controls.Add(this.label6);
            this.PanelHeader.Controls.Add(this.TxtDoctor);
            this.PanelHeader.Controls.Add(this.label4);
            this.PanelHeader.Controls.Add(this.ChkActive);
            this.PanelHeader.Controls.Add(this.TxtDescription);
            this.PanelHeader.Controls.Add(this.CmbLevel);
            this.PanelHeader.Controls.Add(this.label1);
            this.PanelHeader.Controls.Add(this.TxtShortName);
            this.PanelHeader.Controls.Add(this.label2);
            this.PanelHeader.Controls.Add(this.label3);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(450, 224);
            this.PanelHeader.TabIndex = 0;
            // 
            // BtnDoctorList
            // 
            this.BtnDoctorList.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnDoctorList.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnDoctorList.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDoctorList.Location = new System.Drawing.Point(420, 155);
            this.BtnDoctorList.Name = "BtnDoctorList";
            this.BtnDoctorList.Size = new System.Drawing.Size(26, 26);
            this.BtnDoctorList.TabIndex = 76;
            this.BtnDoctorList.TabStop = false;
            this.BtnDoctorList.UseVisualStyleBackColor = false;
            this.BtnDoctorList.Click += new System.EventHandler(this.BtnDoctorList_Click);
            // 
            // BtnDescription
            // 
            this.BtnDescription.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnDescription.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnDescription.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDescription.Location = new System.Drawing.Point(420, 43);
            this.BtnDescription.Name = "BtnDescription";
            this.BtnDescription.Size = new System.Drawing.Size(26, 26);
            this.BtnDescription.TabIndex = 75;
            this.BtnDescription.TabStop = false;
            this.BtnDescription.UseVisualStyleBackColor = false;
            this.BtnDescription.Click += new System.EventHandler(this.BtnDescription_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnCancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.Appearance.Options.UseForeColor = true;
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.BtnCancel.Location = new System.Drawing.Point(342, 189);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(104, 32);
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
            this.BtnSave.Location = new System.Drawing.Point(246, 189);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(96, 32);
            this.BtnSave.TabIndex = 9;
            this.BtnSave.Text = "&SAVE";
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(12, 184);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(434, 2);
            this.clsSeparator2.TabIndex = 74;
            this.clsSeparator2.TabStop = false;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(5, 38);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(441, 2);
            this.clsSeparator1.TabIndex = 73;
            this.clsSeparator1.TabStop = false;
            // 
            // BtnNew
            // 
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNew.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.Appearance.Options.UseForeColor = true;
            this.BtnNew.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnNew.Location = new System.Drawing.Point(5, 4);
            this.BtnNew.Margin = new System.Windows.Forms.Padding(4);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(79, 33);
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
            this.BtnExit.Location = new System.Drawing.Point(370, 3);
            this.BtnExit.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(76, 33);
            this.BtnExit.TabIndex = 3;
            this.BtnExit.Text = "E&XIT";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.Appearance.Options.UseForeColor = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(168, 4);
            this.BtnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(103, 33);
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
            this.BtnEdit.Location = new System.Drawing.Point(85, 4);
            this.BtnEdit.Margin = new System.Windows.Forms.Padding(4);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(82, 33);
            this.BtnEdit.TabIndex = 1;
            this.BtnEdit.Tag = "EditButtonCheck";
            this.BtnEdit.Text = "&EDIT";
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // FrmHDepartment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 224);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmHDepartment";
            this.ShowIcon = false;
            this.Tag = "Hospital Department";
            this.Text = "Hospital Department";
            this.Load += new System.EventHandler(this.FrmDepartment_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmDepartment_KeyPress);
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox ChkActive;
        private System.Windows.Forms.ComboBox CmbLevel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private ClsSeparator clsSeparator1;
        private ClsSeparator clsSeparator2;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private MrTextBox TxtShortName;
        private MrTextBox TxtDescription;
        private MrTextBox TxtDoctor;
        private MrTextBox TxtCharge;
        private System.Windows.Forms.Button BtnDescription;
        private System.Windows.Forms.Button BtnDoctorList;
        private MrPanel PanelHeader;
    }
}