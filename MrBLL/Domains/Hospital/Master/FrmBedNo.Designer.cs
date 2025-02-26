using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.Hospital.Master
{
    partial class FrmBedNo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBedNo));
            this.label3 = new System.Windows.Forms.Label();
            this.CmbBedType = new System.Windows.Forms.ComboBox();
            this.TxtCharge = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtWardNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ChkActive = new System.Windows.Forms.CheckBox();
            this.TxtShortName = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtDescription = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PanelHeader = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.BtnWardNo = new System.Windows.Forms.Button();
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(412, 86);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 21);
            this.label3.TabIndex = 37;
            this.label3.Text = "Bed Type";
            // 
            // CmbBedType
            // 
            this.CmbBedType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbBedType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbBedType.FormattingEnabled = true;
            this.CmbBedType.Location = new System.Drawing.Point(539, 81);
            this.CmbBedType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CmbBedType.Name = "CmbBedType";
            this.CmbBedType.Size = new System.Drawing.Size(191, 28);
            this.CmbBedType.TabIndex = 8;
            // 
            // TxtCharge
            // 
            this.TxtCharge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCharge.Location = new System.Drawing.Point(141, 150);
            this.TxtCharge.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtCharge.Name = "TxtCharge";
            this.TxtCharge.Size = new System.Drawing.Size(225, 29);
            this.TxtCharge.TabIndex = 7;
            this.TxtCharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 154);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 21);
            this.label6.TabIndex = 34;
            this.label6.Text = "Charge";
            // 
            // TxtWardNo
            // 
            this.TxtWardNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtWardNo.Location = new System.Drawing.Point(141, 82);
            this.TxtWardNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtWardNo.Name = "TxtWardNo";
            this.TxtWardNo.Size = new System.Drawing.Size(225, 29);
            this.TxtWardNo.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 86);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 21);
            this.label4.TabIndex = 30;
            this.label4.Text = "Ward No";
            // 
            // ChkActive
            // 
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkActive.Location = new System.Drawing.Point(7, 193);
            this.ChkActive.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkActive.Size = new System.Drawing.Size(127, 28);
            this.ChkActive.TabIndex = 11;
            this.ChkActive.Text = "Active";
            this.ChkActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkActive.UseVisualStyleBackColor = true;
            // 
            // TxtShortName
            // 
            this.TxtShortName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtShortName.Location = new System.Drawing.Point(141, 117);
            this.TxtShortName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtShortName.Name = "TxtShortName";
            this.TxtShortName.Size = new System.Drawing.Size(225, 29);
            this.TxtShortName.TabIndex = 6;
            // 
            // TxtDescription
            // 
            this.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDescription.Location = new System.Drawing.Point(141, 49);
            this.TxtDescription.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(563, 29);
            this.TxtDescription.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 121);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "ShortName";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(8, 53);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Description";
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.BtnWardNo);
            this.PanelHeader.Controls.Add(this.BtnDescription);
            this.PanelHeader.Controls.Add(this.BtnCancel);
            this.PanelHeader.Controls.Add(this.BtnSave);
            this.PanelHeader.Controls.Add(this.clsSeparator2);
            this.PanelHeader.Controls.Add(this.clsSeparator1);
            this.PanelHeader.Controls.Add(this.BtnNew);
            this.PanelHeader.Controls.Add(this.BtnExit);
            this.PanelHeader.Controls.Add(this.BtnDelete);
            this.PanelHeader.Controls.Add(this.BtnEdit);
            this.PanelHeader.Controls.Add(this.label3);
            this.PanelHeader.Controls.Add(this.CmbBedType);
            this.PanelHeader.Controls.Add(this.TxtCharge);
            this.PanelHeader.Controls.Add(this.label6);
            this.PanelHeader.Controls.Add(this.TxtWardNo);
            this.PanelHeader.Controls.Add(this.TxtDescription);
            this.PanelHeader.Controls.Add(this.label4);
            this.PanelHeader.Controls.Add(this.label1);
            this.PanelHeader.Controls.Add(this.ChkActive);
            this.PanelHeader.Controls.Add(this.label2);
            this.PanelHeader.Controls.Add(this.TxtShortName);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(747, 238);
            this.PanelHeader.TabIndex = 0;
            // 
            // BtnWardNo
            // 
            this.BtnWardNo.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnWardNo.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnWardNo.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnWardNo.Location = new System.Drawing.Point(371, 82);
            this.BtnWardNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnWardNo.Name = "BtnWardNo";
            this.BtnWardNo.Size = new System.Drawing.Size(36, 30);
            this.BtnWardNo.TabIndex = 313;
            this.BtnWardNo.TabStop = false;
            this.BtnWardNo.UseVisualStyleBackColor = false;
            // 
            // BtnDescription
            // 
            this.BtnDescription.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnDescription.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnDescription.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDescription.Location = new System.Drawing.Point(707, 49);
            this.BtnDescription.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnDescription.Name = "BtnDescription";
            this.BtnDescription.Size = new System.Drawing.Size(36, 30);
            this.BtnDescription.TabIndex = 6;
            this.BtnDescription.TabStop = false;
            this.BtnDescription.UseVisualStyleBackColor = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnCancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.Appearance.Options.UseForeColor = true;
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(589, 190);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(135, 42);
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
            this.BtnSave.Location = new System.Drawing.Point(456, 190);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(133, 42);
            this.BtnSave.TabIndex = 9;
            this.BtnSave.Text = "&SAVE";
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(9, 182);
            this.clsSeparator2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clsSeparator2.Size = new System.Drawing.Size(728, 2);
            this.clsSeparator2.TabIndex = 44;
            this.clsSeparator2.TabStop = false;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(15, 44);
            this.clsSeparator1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clsSeparator1.Size = new System.Drawing.Size(728, 2);
            this.clsSeparator1.TabIndex = 43;
            this.clsSeparator1.TabStop = false;
            // 
            // BtnNew
            // 
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNew.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.Appearance.Options.UseForeColor = true;
            this.BtnNew.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnNew.Location = new System.Drawing.Point(12, 4);
            this.BtnNew.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(105, 41);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Tag = "NewButtonCheck";
            this.BtnNew.Text = "&NEW";
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseForeColor = true;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.BtnExit.Location = new System.Drawing.Point(641, 4);
            this.BtnExit.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.BtnExit.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(101, 41);
            this.BtnExit.TabIndex = 3;
            this.BtnExit.Text = "E&XIT";
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.Appearance.Options.UseForeColor = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(229, 4);
            this.BtnDelete.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(137, 41);
            this.BtnDelete.TabIndex = 2;
            this.BtnDelete.Tag = "DeleteButtonCheck";
            this.BtnDelete.Text = "&DELETE";
            // 
            // BtnEdit
            // 
            this.BtnEdit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEdit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnEdit.Appearance.Options.UseFont = true;
            this.BtnEdit.Appearance.Options.UseForeColor = true;
            this.BtnEdit.ImageOptions.Image = global::MrBLL.Properties.Resources.Edit;
            this.BtnEdit.Location = new System.Drawing.Point(119, 4);
            this.BtnEdit.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(109, 41);
            this.BtnEdit.TabIndex = 1;
            this.BtnEdit.Tag = "EditButtonCheck";
            this.BtnEdit.Text = "&EDIT";
            // 
            // FrmBedNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 238);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.MaximizeBox = false;
            this.Name = "FrmBedNo";
            this.ShowIcon = false;
            this.Tag = "BED Number";
            this.Text = "Bed Number";
            this.Load += new System.EventHandler(this.FrmBedNo_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmBedNo_KeyPress);
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox ChkActive;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CmbBedType;
        private ClsSeparator clsSeparator1;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private ClsSeparator clsSeparator2;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private System.Windows.Forms.Button BtnDescription;
        private System.Windows.Forms.Button BtnWardNo;
        private MrTextBox TxtCharge;
        private MrTextBox TxtWardNo;
        private MrTextBox TxtShortName;
        private MrTextBox TxtDescription;
        private MrPanel PanelHeader;
    }
}