using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Master.ProductSetup
{
    partial class FrmColastar
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
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.ChkActive = new System.Windows.Forms.CheckBox();
            this.TxtDescription = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.CmbSection = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.PanelHeader = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.BtnView = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDescription = new System.Windows.Forms.Button();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.panel5 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.panel4 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.PnlBorderHeaderTop = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.PnlBorderHeaderBottom = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.PanelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.BtnExit.Location = new System.Drawing.Point(549, 7);
            this.BtnExit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(101, 39);
            this.BtnExit.TabIndex = 9;
            this.BtnExit.Text = "E&XIT";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(219, 7);
            this.BtnDelete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(137, 39);
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
            this.BtnEdit.Location = new System.Drawing.Point(109, 7);
            this.BtnEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(109, 39);
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
            this.BtnNew.Location = new System.Drawing.Point(9, 7);
            this.BtnNew.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(100, 39);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Tag = "NewButtonCheck";
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // ChkActive
            // 
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Location = new System.Drawing.Point(16, 151);
            this.ChkActive.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkActive.Size = new System.Drawing.Size(127, 30);
            this.ChkActive.TabIndex = 6;
            this.ChkActive.Text = "Active";
            this.ChkActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkActive.UseVisualStyleBackColor = true;
            // 
            // TxtDescription
            // 
            this.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDescription.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDescription.Location = new System.Drawing.Point(151, 54);
            this.TxtDescription.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtDescription.MaxLength = 50;
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(459, 29);
            this.TxtDescription.TabIndex = 3;
            this.TxtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDescription_KeyDown);
            this.TxtDescription.Leave += new System.EventHandler(this.TxtDescription_Leave);
            this.TxtDescription.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDescription_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 58);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Description";
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnCancel.Location = new System.Drawing.Point(503, 139);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(145, 46);
            this.BtnCancel.TabIndex = 8;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(357, 139);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(144, 46);
            this.BtnSave.TabIndex = 7;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // CmbSection
            // 
            this.CmbSection.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.CmbSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbSection.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbSection.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbSection.FormattingEnabled = true;
            this.CmbSection.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E"});
            this.CmbSection.Location = new System.Drawing.Point(151, 92);
            this.CmbSection.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CmbSection.Name = "CmbSection";
            this.CmbSection.Size = new System.Drawing.Size(237, 28);
            this.CmbSection.TabIndex = 5;
            this.CmbSection.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbSection_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 96);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 21);
            this.label3.TabIndex = 28;
            this.label3.Text = "Section";
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.BtnView);
            this.PanelHeader.Controls.Add(this.BtnDescription);
            this.PanelHeader.Controls.Add(this.clsSeparator2);
            this.PanelHeader.Controls.Add(this.clsSeparator1);
            this.PanelHeader.Controls.Add(this.ChkActive);
            this.PanelHeader.Controls.Add(this.BtnCancel);
            this.PanelHeader.Controls.Add(this.BtnExit);
            this.PanelHeader.Controls.Add(this.BtnSave);
            this.PanelHeader.Controls.Add(this.panel5);
            this.PanelHeader.Controls.Add(this.CmbSection);
            this.PanelHeader.Controls.Add(this.BtnDelete);
            this.PanelHeader.Controls.Add(this.label3);
            this.PanelHeader.Controls.Add(this.BtnEdit);
            this.PanelHeader.Controls.Add(this.panel4);
            this.PanelHeader.Controls.Add(this.TxtDescription);
            this.PanelHeader.Controls.Add(this.label1);
            this.PanelHeader.Controls.Add(this.BtnNew);
            this.PanelHeader.Controls.Add(this.PnlBorderHeaderTop);
            this.PanelHeader.Controls.Add(this.PnlBorderHeaderBottom);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(659, 191);
            this.PanelHeader.TabIndex = 210;
            // 
            // BtnView
            // 
            this.BtnView.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnView.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnView.Appearance.Options.UseFont = true;
            this.BtnView.Appearance.Options.UseForeColor = true;
            this.BtnView.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnView.Location = new System.Drawing.Point(359, 7);
            this.BtnView.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.BtnView.Name = "BtnView";
            this.BtnView.Size = new System.Drawing.Size(133, 39);
            this.BtnView.TabIndex = 31;
            this.BtnView.Tag = "ViewButtonCheck";
            this.BtnView.Text = "&VIEW";
            this.BtnView.Click += new System.EventHandler(this.BtnView_Click);
            // 
            // BtnDescription
            // 
            this.BtnDescription.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnDescription.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnDescription.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDescription.Location = new System.Drawing.Point(612, 54);
            this.BtnDescription.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnDescription.Name = "BtnDescription";
            this.BtnDescription.Size = new System.Drawing.Size(36, 30);
            this.BtnDescription.TabIndex = 4;
            this.BtnDescription.TabStop = false;
            this.BtnDescription.UseVisualStyleBackColor = false;
            this.BtnDescription.Click += new System.EventHandler(this.BtnDescription_Click);
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(16, 130);
            this.clsSeparator2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clsSeparator2.Size = new System.Drawing.Size(633, 2);
            this.clsSeparator2.TabIndex = 1;
            this.clsSeparator2.TabStop = false;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(16, 50);
            this.clsSeparator1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clsSeparator1.Size = new System.Drawing.Size(633, 2);
            this.clsSeparator1.TabIndex = 30;
            this.clsSeparator1.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(0, 4);
            this.panel5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(4, 183);
            this.panel5.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(655, 4);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(4, 183);
            this.panel4.TabIndex = 9;
            // 
            // PnlBorderHeaderTop
            // 
            this.PnlBorderHeaderTop.BackColor = System.Drawing.Color.White;
            this.PnlBorderHeaderTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlBorderHeaderTop.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlBorderHeaderTop.Location = new System.Drawing.Point(0, 0);
            this.PnlBorderHeaderTop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PnlBorderHeaderTop.Name = "PnlBorderHeaderTop";
            this.PnlBorderHeaderTop.Size = new System.Drawing.Size(659, 4);
            this.PnlBorderHeaderTop.TabIndex = 8;
            // 
            // PnlBorderHeaderBottom
            // 
            this.PnlBorderHeaderBottom.BackColor = System.Drawing.Color.White;
            this.PnlBorderHeaderBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PnlBorderHeaderBottom.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlBorderHeaderBottom.Location = new System.Drawing.Point(0, 187);
            this.PnlBorderHeaderBottom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PnlBorderHeaderBottom.Name = "PnlBorderHeaderBottom";
            this.PnlBorderHeaderBottom.Size = new System.Drawing.Size(659, 4);
            this.PnlBorderHeaderBottom.TabIndex = 0;
            // 
            // FrmColastar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(659, 191);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmColastar";
            this.Tag = "Colastar";
            this.Text = "Colastar Setup";
            this.Load += new System.EventHandler(this.FrmColastar_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmColastar_KeyPress);
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private System.Windows.Forms.CheckBox ChkActive;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private System.Windows.Forms.ComboBox CmbSection;
        private System.Windows.Forms.Label label3;
        private ClsSeparator clsSeparator1;
        private ClsSeparator clsSeparator2;
        private System.Windows.Forms.Button BtnDescription;
        private DevExpress.XtraEditors.SimpleButton BtnView;
        private MrTextBox TxtDescription;
        private MrPanel PanelHeader;
        private MrPanel panel5;
        private MrPanel panel4;
        private MrPanel PnlBorderHeaderTop;
        private MrPanel PnlBorderHeaderBottom;
    }
}