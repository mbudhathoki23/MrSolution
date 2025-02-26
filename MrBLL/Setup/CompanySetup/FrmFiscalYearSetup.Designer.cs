namespace MrBLL.Setup.CompanySetup;

partial class FrmFiscalYearSetup
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
            this.mrPanel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.StorePanel = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.MskAdYear = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.MskBsYear = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.MskEndMiti = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.MskStartMiti = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.MskEndDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.BtnSync = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDescription = new System.Windows.Forms.Button();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.MskStartDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_Regdate = new System.Windows.Forms.Label();
            this.lable1 = new System.Windows.Forms.Label();
            this.mrPanel1.SuspendLayout();
            this.StorePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mrPanel1
            // 
            this.mrPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel1.Controls.Add(this.StorePanel);
            this.mrPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrPanel1.Location = new System.Drawing.Point(0, 0);
            this.mrPanel1.Name = "mrPanel1";
            this.mrPanel1.Size = new System.Drawing.Size(543, 167);
            this.mrPanel1.TabIndex = 0;
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.MskAdYear);
            this.StorePanel.Controls.Add(this.MskBsYear);
            this.StorePanel.Controls.Add(this.label4);
            this.StorePanel.Controls.Add(this.label2);
            this.StorePanel.Controls.Add(this.MskEndMiti);
            this.StorePanel.Controls.Add(this.MskStartMiti);
            this.StorePanel.Controls.Add(this.label3);
            this.StorePanel.Controls.Add(this.label1);
            this.StorePanel.Controls.Add(this.MskEndDate);
            this.StorePanel.Controls.Add(this.BtnSync);
            this.StorePanel.Controls.Add(this.BtnDescription);
            this.StorePanel.Controls.Add(this.clsSeparator2);
            this.StorePanel.Controls.Add(this.clsSeparator1);
            this.StorePanel.Controls.Add(this.BtnCancel);
            this.StorePanel.Controls.Add(this.BtnSave);
            this.StorePanel.Controls.Add(this.BtnExit);
            this.StorePanel.Controls.Add(this.MskStartDate);
            this.StorePanel.Controls.Add(this.BtnEdit);
            this.StorePanel.Controls.Add(this.BtnNew);
            this.StorePanel.Controls.Add(this.lbl_Regdate);
            this.StorePanel.Controls.Add(this.lable1);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(543, 167);
            this.StorePanel.TabIndex = 0;
            // 
            // MskAdYear
            // 
            this.MskAdYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskAdYear.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskAdYear.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskAdYear.Location = new System.Drawing.Point(366, 44);
            this.MskAdYear.Margin = new System.Windows.Forms.Padding(4);
            this.MskAdYear.Mask = "00/00";
            this.MskAdYear.Name = "MskAdYear";
            this.MskAdYear.Size = new System.Drawing.Size(138, 25);
            this.MskAdYear.TabIndex = 4;
            this.MskAdYear.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MskAdYear_KeyDown);
            this.MskAdYear.Validating += new System.ComponentModel.CancelEventHandler(this.MskAdYear_Validating);
            // 
            // MskBsYear
            // 
            this.MskBsYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskBsYear.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskBsYear.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskBsYear.Location = new System.Drawing.Point(134, 44);
            this.MskBsYear.Margin = new System.Windows.Forms.Padding(4);
            this.MskBsYear.Mask = "00/00";
            this.MskBsYear.Name = "MskBsYear";
            this.MskBsYear.Size = new System.Drawing.Size(138, 25);
            this.MskBsYear.TabIndex = 3;
            this.MskBsYear.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MskBsYear_KeyDown);
            this.MskBsYear.Validating += new System.ComponentModel.CancelEventHandler(this.MskBsYear_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(277, 47);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 19);
            this.label4.TabIndex = 47;
            this.label4.Text = "AD Year";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(277, 99);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 19);
            this.label2.TabIndex = 46;
            this.label2.Text = "End Miti";
            // 
            // MskEndMiti
            // 
            this.MskEndMiti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskEndMiti.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskEndMiti.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskEndMiti.Location = new System.Drawing.Point(366, 96);
            this.MskEndMiti.Margin = new System.Windows.Forms.Padding(4);
            this.MskEndMiti.Mask = "00/00/0000";
            this.MskEndMiti.Name = "MskEndMiti";
            this.MskEndMiti.Size = new System.Drawing.Size(138, 25);
            this.MskEndMiti.TabIndex = 8;
            this.MskEndMiti.ValidatingType = typeof(System.DateTime);
            this.MskEndMiti.Validating += new System.ComponentModel.CancelEventHandler(this.MskEndMiti_Validating);
            // 
            // MskStartMiti
            // 
            this.MskStartMiti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskStartMiti.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskStartMiti.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskStartMiti.Location = new System.Drawing.Point(134, 96);
            this.MskStartMiti.Margin = new System.Windows.Forms.Padding(4);
            this.MskStartMiti.Mask = "00/00/0000";
            this.MskStartMiti.Name = "MskStartMiti";
            this.MskStartMiti.Size = new System.Drawing.Size(138, 25);
            this.MskStartMiti.TabIndex = 7;
            this.MskStartMiti.ValidatingType = typeof(System.DateTime);
            this.MskStartMiti.Validating += new System.ComponentModel.CancelEventHandler(this.MskStartMiti_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 99);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 19);
            this.label3.TabIndex = 44;
            this.label3.Text = "Start Miti";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(277, 73);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 19);
            this.label1.TabIndex = 42;
            this.label1.Text = "End Date";
            // 
            // MskEndDate
            // 
            this.MskEndDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskEndDate.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskEndDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskEndDate.Location = new System.Drawing.Point(366, 70);
            this.MskEndDate.Margin = new System.Windows.Forms.Padding(4);
            this.MskEndDate.Mask = "00/00/0000";
            this.MskEndDate.Name = "MskEndDate";
            this.MskEndDate.Size = new System.Drawing.Size(138, 25);
            this.MskEndDate.TabIndex = 6;
            this.MskEndDate.ValidatingType = typeof(System.DateTime);
            this.MskEndDate.Validating += new System.ComponentModel.CancelEventHandler(this.MskEndDate_Validating);
            // 
            // BtnSync
            // 
            this.BtnSync.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSync.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSync.Appearance.Options.UseFont = true;
            this.BtnSync.Appearance.Options.UseForeColor = true;
            this.BtnSync.ImageOptions.Image = global::MrBLL.Properties.Resources.ReportRefresh;
            this.BtnSync.Location = new System.Drawing.Point(248, 128);
            this.BtnSync.Margin = new System.Windows.Forms.Padding(4);
            this.BtnSync.Name = "BtnSync";
            this.BtnSync.Size = new System.Drawing.Size(100, 35);
            this.BtnSync.TabIndex = 40;
            this.BtnSync.Text = "&SYNC";
            this.BtnSync.Click += new System.EventHandler(this.BtnSync_Click);
            // 
            // BtnDescription
            // 
            this.BtnDescription.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnDescription.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnDescription.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDescription.Location = new System.Drawing.Point(504, 43);
            this.BtnDescription.Name = "BtnDescription";
            this.BtnDescription.Size = new System.Drawing.Size(29, 28);
            this.BtnDescription.TabIndex = 39;
            this.BtnDescription.TabStop = false;
            this.BtnDescription.UseVisualStyleBackColor = false;
            this.BtnDescription.Click += new System.EventHandler(this.BtnDescription_Click);
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(10, 125);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(530, 2);
            this.clsSeparator2.TabIndex = 36;
            this.clsSeparator2.TabStop = false;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(3, 39);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(537, 2);
            this.clsSeparator1.TabIndex = 35;
            this.clsSeparator1.TabStop = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(435, 128);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(106, 35);
            this.BtnCancel.TabIndex = 10;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(349, 128);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(85, 35);
            this.BtnSave.TabIndex = 9;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.BtnExit.Location = new System.Drawing.Point(464, 4);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(76, 33);
            this.BtnExit.TabIndex = 2;
            this.BtnExit.Text = "E&XIT";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // MskStartDate
            // 
            this.MskStartDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskStartDate.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskStartDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskStartDate.Location = new System.Drawing.Point(134, 70);
            this.MskStartDate.Margin = new System.Windows.Forms.Padding(4);
            this.MskStartDate.Mask = "00/00/0000";
            this.MskStartDate.Name = "MskStartDate";
            this.MskStartDate.Size = new System.Drawing.Size(138, 25);
            this.MskStartDate.TabIndex = 5;
            this.MskStartDate.ValidatingType = typeof(System.DateTime);
            this.MskStartDate.Validating += new System.ComponentModel.CancelEventHandler(this.MskStartDate_Validating);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEdit.Appearance.Options.UseFont = true;
            this.BtnEdit.ImageOptions.Image = global::MrBLL.Properties.Resources.Edit;
            this.BtnEdit.Location = new System.Drawing.Point(83, 4);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(76, 33);
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
            this.BtnNew.Location = new System.Drawing.Point(5, 4);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(77, 33);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Tag = "NewButtonCheck";
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // lbl_Regdate
            // 
            this.lbl_Regdate.AutoSize = true;
            this.lbl_Regdate.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Regdate.Location = new System.Drawing.Point(6, 73);
            this.lbl_Regdate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Regdate.Name = "lbl_Regdate";
            this.lbl_Regdate.Size = new System.Drawing.Size(88, 19);
            this.lbl_Regdate.TabIndex = 17;
            this.lbl_Regdate.Text = "Start Date";
            // 
            // lable1
            // 
            this.lable1.AutoSize = true;
            this.lable1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lable1.Location = new System.Drawing.Point(6, 47);
            this.lable1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lable1.Name = "lable1";
            this.lable1.Size = new System.Drawing.Size(128, 19);
            this.lable1.TabIndex = 2;
            this.lable1.Text = "Bs Year (01/02)";
            // 
            // FrmFiscalYearSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 167);
            this.Controls.Add(this.mrPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmFiscalYearSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FISCAL YEAR SETUP";
            this.Load += new System.EventHandler(this.FrmFiscalYearSetup_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmFiscalYearSetup_KeyPress);
            this.mrPanel1.ResumeLayout(false);
            this.StorePanel.ResumeLayout(false);
            this.StorePanel.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private MrDAL.Control.ControlsEx.Control.MrPanel mrPanel1;
    private MrDAL.Control.ControlsEx.Control.MrPanel StorePanel;
    private System.Windows.Forms.Label label1;
    private MrDAL.Control.ControlsEx.Control.MrMaskedTextBox MskEndDate;
    private DevExpress.XtraEditors.SimpleButton BtnSync;
    private System.Windows.Forms.Button BtnDescription;
    private MrDAL.Control.ControlsEx.Control.ClsSeparator clsSeparator2;
    private MrDAL.Control.ControlsEx.Control.ClsSeparator clsSeparator1;
    private DevExpress.XtraEditors.SimpleButton BtnCancel;
    private DevExpress.XtraEditors.SimpleButton BtnSave;
    private DevExpress.XtraEditors.SimpleButton BtnExit;
    private MrDAL.Control.ControlsEx.Control.MrMaskedTextBox MskStartDate;
    private DevExpress.XtraEditors.SimpleButton BtnEdit;
    private DevExpress.XtraEditors.SimpleButton BtnNew;
    private System.Windows.Forms.Label lbl_Regdate;
    private System.Windows.Forms.Label lable1;
    private System.Windows.Forms.Label label2;
    private MrDAL.Control.ControlsEx.Control.MrMaskedTextBox MskEndMiti;
    private MrDAL.Control.ControlsEx.Control.MrMaskedTextBox MskStartMiti;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private MrDAL.Control.ControlsEx.Control.MrMaskedTextBox MskAdYear;
    private MrDAL.Control.ControlsEx.Control.MrMaskedTextBox MskBsYear;
}