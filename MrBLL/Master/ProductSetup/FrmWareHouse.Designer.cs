using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Master.ProductSetup
{
    partial class FrmWareHouse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWareHouse));
            this.BtnView = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDescription = new System.Windows.Forms.Button();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.ChkActive = new System.Windows.Forms.CheckBox();
            this.StorePanel = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtLocation = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtShortName = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtDescription = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.StorePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnView
            // 
            this.BtnView.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnView.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnView.Appearance.Options.UseFont = true;
            this.BtnView.Appearance.Options.UseForeColor = true;
            this.BtnView.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnView.Location = new System.Drawing.Point(256, 6);
            this.BtnView.Margin = new System.Windows.Forms.Padding(4);
            this.BtnView.Name = "BtnView";
            this.BtnView.Size = new System.Drawing.Size(100, 36);
            this.BtnView.TabIndex = 312;
            this.BtnView.Tag = "ViewButtonCheck";
            this.BtnView.Text = "&VIEW";
            this.BtnView.Click += new System.EventHandler(this.BtnView_Click);
            // 
            // BtnDescription
            // 
            this.BtnDescription.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnDescription.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnDescription.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDescription.Location = new System.Drawing.Point(428, 48);
            this.BtnDescription.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.BtnDescription.Name = "BtnDescription";
            this.BtnDescription.Size = new System.Drawing.Size(26, 27);
            this.BtnDescription.TabIndex = 311;
            this.BtnDescription.TabStop = false;
            this.BtnDescription.UseVisualStyleBackColor = false;
            this.BtnDescription.Click += new System.EventHandler(this.BtnDescription_Click);
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(0, 132);
            this.clsSeparator2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.clsSeparator2.Size = new System.Drawing.Size(460, 2);
            this.clsSeparator2.TabIndex = 32;
            this.clsSeparator2.TabStop = false;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(0, 45);
            this.clsSeparator1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.clsSeparator1.Size = new System.Drawing.Size(460, 2);
            this.clsSeparator1.TabIndex = 32;
            this.clsSeparator1.TabStop = false;
            // 
            // ChkActive
            // 
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkActive.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ChkActive.Location = new System.Drawing.Point(10, 144);
            this.ChkActive.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkActive.Size = new System.Drawing.Size(118, 26);
            this.ChkActive.TabIndex = 6;
            this.ChkActive.Text = "Active";
            this.ChkActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkActive.UseVisualStyleBackColor = true;
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.BtnView);
            this.StorePanel.Controls.Add(this.BtnDescription);
            this.StorePanel.Controls.Add(this.clsSeparator2);
            this.StorePanel.Controls.Add(this.clsSeparator1);
            this.StorePanel.Controls.Add(this.BtnExit);
            this.StorePanel.Controls.Add(this.ChkActive);
            this.StorePanel.Controls.Add(this.BtnDelete);
            this.StorePanel.Controls.Add(this.BtnCancel);
            this.StorePanel.Controls.Add(this.BtnNew);
            this.StorePanel.Controls.Add(this.BtnEdit);
            this.StorePanel.Controls.Add(this.label1);
            this.StorePanel.Controls.Add(this.label2);
            this.StorePanel.Controls.Add(this.TxtLocation);
            this.StorePanel.Controls.Add(this.label3);
            this.StorePanel.Controls.Add(this.TxtShortName);
            this.StorePanel.Controls.Add(this.TxtDescription);
            this.StorePanel.Controls.Add(this.BtnSave);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(464, 180);
            this.StorePanel.TabIndex = 41;
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.BtnExit.Location = new System.Drawing.Point(380, 6);
            this.BtnExit.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(76, 36);
            this.BtnExit.TabIndex = 9;
            this.BtnExit.Text = "E&XIT";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(154, 6);
            this.BtnDelete.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(100, 36);
            this.BtnDelete.TabIndex = 2;
            this.BtnDelete.Tag = "DeleteButtonCheck";
            this.BtnDelete.Text = "&DELETE";
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.BtnCancel.Location = new System.Drawing.Point(352, 140);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(104, 36);
            this.BtnCancel.TabIndex = 8;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnNew
            // 
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnNew.Location = new System.Drawing.Point(8, 6);
            this.BtnNew.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(74, 36);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Tag = "NewButtonCheck";
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEdit.Appearance.Options.UseFont = true;
            this.BtnEdit.ImageOptions.Image = global::MrBLL.Properties.Resources.Edit;
            this.BtnEdit.Location = new System.Drawing.Point(80, 6);
            this.BtnEdit.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(74, 36);
            this.BtnEdit.TabIndex = 1;
            this.BtnEdit.Tag = "EditButtonCheck";
            this.BtnEdit.Text = "&EDIT";
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 52);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Description";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 80);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Short Name";
            // 
            // TxtLocation
            // 
            this.TxtLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtLocation.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtLocation.Location = new System.Drawing.Point(116, 104);
            this.TxtLocation.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.TxtLocation.MaxLength = 50;
            this.TxtLocation.Name = "TxtLocation";
            this.TxtLocation.Size = new System.Drawing.Size(334, 27);
            this.TxtLocation.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 108);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 21);
            this.label3.TabIndex = 1;
            this.label3.Text = "Location";
            // 
            // TxtShortName
            // 
            this.TxtShortName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtShortName.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtShortName.Location = new System.Drawing.Point(116, 76);
            this.TxtShortName.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.TxtShortName.MaxLength = 50;
            this.TxtShortName.Name = "TxtShortName";
            this.TxtShortName.Size = new System.Drawing.Size(162, 27);
            this.TxtShortName.TabIndex = 4;
            // 
            // TxtDescription
            // 
            this.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDescription.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDescription.Location = new System.Drawing.Point(116, 50);
            this.TxtDescription.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.TxtDescription.MaxLength = 500;
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(308, 27);
            this.TxtDescription.TabIndex = 3;
            this.TxtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDescription_KeyDown);
            this.TxtDescription.Leave += new System.EventHandler(this.TxtDescription_Leave);
            this.TxtDescription.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDescription_Validating);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(262, 140);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(88, 36);
            this.BtnSave.TabIndex = 7;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // FrmWareHouse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(464, 180);
            this.Controls.Add(this.StorePanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.Name = "FrmWareHouse";
            this.ShowIcon = false;
            this.Tag = "WareHouse";
            this.Text = "WARE HOUSE MANAGEMENT";
            this.Load += new System.EventHandler(this.FrmWareHouse_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmWareHouse_KeyPress);
            this.StorePanel.ResumeLayout(false);
            this.StorePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton BtnView;
        private System.Windows.Forms.Button BtnDescription;
        private ClsSeparator clsSeparator2;
        private ClsSeparator clsSeparator1;
        private System.Windows.Forms.CheckBox ChkActive;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private MrPanel StorePanel;
        private MrTextBox TxtLocation;
        private MrTextBox TxtShortName;
        private MrTextBox TxtDescription;
    }
}