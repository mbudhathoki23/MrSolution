using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Master.DocumentNumbering
{
    partial class FrmDocumentNumbering
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDocumentNumbering));
            this.MskEndDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.MskStartDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.TxtTotalLength = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtBlank = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblDocTotalLength = new System.Windows.Forms.Label();
            this.ChkBlankNumber = new System.Windows.Forms.CheckBox();
            this.TxtBodyLength = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblDocBodylenght = new System.Windows.Forms.Label();
            this.TxtSufix = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblDocSufix = new System.Windows.Forms.Label();
            this.TxtPrefix = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblDOcPrefix = new System.Windows.Forms.Label();
            this.lblDocEnd = new System.Windows.Forms.Label();
            this.lblDocStart = new System.Windows.Forms.Label();
            this.TxtDescription = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblDocDesc = new System.Windows.Forms.Label();
            this.CmbUser = new System.Windows.Forms.ComboBox();
            this.lblDocUser = new System.Windows.Forms.Label();
            this.lblDocName = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rChkAutoNumber = new System.Windows.Forms.RadioButton();
            this.rChkAlphaNumber = new System.Windows.Forms.RadioButton();
            this.rChkNumerical = new System.Windows.Forms.RadioButton();
            this.CmbDesign = new System.Windows.Forms.ComboBox();
            this.grpDocNumberInfo = new System.Windows.Forms.GroupBox();
            this.CmbUnit = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CmbBranch = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDocEndNumber = new System.Windows.Forms.Label();
            this.TxtEnd = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblDocCurrentNumber = new System.Windows.Forms.Label();
            this.TxtCurrent = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtStart = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lblDocStartNumber = new System.Windows.Forms.Label();
            this.lblDocDefaultDesign = new System.Windows.Forms.Label();
            this.ChkActive = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.StorePanel = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.BtnView = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDocumentSchema = new System.Windows.Forms.Button();
            this.TxtDocumentDesc = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnDescription = new System.Windows.Forms.Button();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.MskEDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.MskSDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.groupBox1.SuspendLayout();
            this.grpDocNumberInfo.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.StorePanel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // MskEndDate
            // 
            this.MskEndDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskEndDate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskEndDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskEndDate.Location = new System.Drawing.Point(97, 39);
            this.MskEndDate.Mask = "00/00/0000";
            this.MskEndDate.Name = "MskEndDate";
            this.MskEndDate.Size = new System.Drawing.Size(120, 25);
            this.MskEndDate.TabIndex = 1;
            this.MskEndDate.Tag = "DD/MM/YYYY";
            this.MskEndDate.Validated += new System.EventHandler(this.MskEndDate_Validated);
            // 
            // MskStartDate
            // 
            this.MskStartDate.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.MskStartDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskStartDate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskStartDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskStartDate.Location = new System.Drawing.Point(97, 12);
            this.MskStartDate.Mask = "00/00/0000";
            this.MskStartDate.Name = "MskStartDate";
            this.MskStartDate.Size = new System.Drawing.Size(120, 25);
            this.MskStartDate.TabIndex = 0;
            this.MskStartDate.Tag = "dd/MM/yyyy";
            this.MskStartDate.Enter += new System.EventHandler(this.MskStartDate_Enter);
            this.MskStartDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MskStartDate_KeyDown);
            this.MskStartDate.Validating += new System.ComponentModel.CancelEventHandler(this.MskStartDate_Validating);
            // 
            // TxtTotalLength
            // 
            this.TxtTotalLength.BackColor = System.Drawing.Color.White;
            this.TxtTotalLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTotalLength.Enabled = false;
            this.TxtTotalLength.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTotalLength.Location = new System.Drawing.Point(148, 92);
            this.TxtTotalLength.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.TxtTotalLength.MaxLength = 18;
            this.TxtTotalLength.Name = "TxtTotalLength";
            this.TxtTotalLength.ReadOnly = true;
            this.TxtTotalLength.Size = new System.Drawing.Size(143, 25);
            this.TxtTotalLength.TabIndex = 3;
            // 
            // TxtBlank
            // 
            this.TxtBlank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBlank.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBlank.Location = new System.Drawing.Point(191, 119);
            this.TxtBlank.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.TxtBlank.MaxLength = 1;
            this.TxtBlank.Name = "TxtBlank";
            this.TxtBlank.Size = new System.Drawing.Size(100, 25);
            this.TxtBlank.TabIndex = 5;
            this.TxtBlank.Text = "0";
            this.TxtBlank.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtBlank.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBlank_KeyDown);
            this.TxtBlank.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBlank_KeyPress);
            // 
            // lblDocTotalLength
            // 
            this.lblDocTotalLength.AutoSize = true;
            this.lblDocTotalLength.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocTotalLength.Location = new System.Drawing.Point(16, 95);
            this.lblDocTotalLength.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDocTotalLength.Name = "lblDocTotalLength";
            this.lblDocTotalLength.Size = new System.Drawing.Size(103, 19);
            this.lblDocTotalLength.TabIndex = 21;
            this.lblDocTotalLength.Text = "Total Length";
            // 
            // ChkBlankNumber
            // 
            this.ChkBlankNumber.AutoSize = true;
            this.ChkBlankNumber.Checked = true;
            this.ChkBlankNumber.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkBlankNumber.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkBlankNumber.Location = new System.Drawing.Point(16, 121);
            this.ChkBlankNumber.Name = "ChkBlankNumber";
            this.ChkBlankNumber.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkBlankNumber.Size = new System.Drawing.Size(153, 23);
            this.ChkBlankNumber.TabIndex = 4;
            this.ChkBlankNumber.Text = "Blank Character";
            this.ChkBlankNumber.UseVisualStyleBackColor = true;
            this.ChkBlankNumber.UseWaitCursor = true;
            // 
            // TxtBodyLength
            // 
            this.TxtBodyLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBodyLength.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBodyLength.Location = new System.Drawing.Point(148, 65);
            this.TxtBodyLength.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.TxtBodyLength.MaxLength = 2;
            this.TxtBodyLength.Name = "TxtBodyLength";
            this.TxtBodyLength.Size = new System.Drawing.Size(143, 25);
            this.TxtBodyLength.TabIndex = 2;
            this.TxtBodyLength.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBodyLength_KeyDown);
            this.TxtBodyLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBodyLength_KeyPress);
            this.TxtBodyLength.Leave += new System.EventHandler(this.TxtBodyLength_Leave);
            this.TxtBodyLength.Validating += new System.ComponentModel.CancelEventHandler(this.TxtBodyLength_Validating);
            // 
            // lblDocBodylenght
            // 
            this.lblDocBodylenght.AutoSize = true;
            this.lblDocBodylenght.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocBodylenght.Location = new System.Drawing.Point(16, 68);
            this.lblDocBodylenght.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDocBodylenght.Name = "lblDocBodylenght";
            this.lblDocBodylenght.Size = new System.Drawing.Size(102, 19);
            this.lblDocBodylenght.TabIndex = 18;
            this.lblDocBodylenght.Text = "Body Length";
            // 
            // TxtSufix
            // 
            this.TxtSufix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSufix.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSufix.Location = new System.Drawing.Point(148, 38);
            this.TxtSufix.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.TxtSufix.MaxLength = 50;
            this.TxtSufix.Name = "TxtSufix";
            this.TxtSufix.Size = new System.Drawing.Size(143, 25);
            this.TxtSufix.TabIndex = 1;
            this.TxtSufix.Leave += new System.EventHandler(this.TxtSufix_Leave);
            // 
            // lblDocSufix
            // 
            this.lblDocSufix.AutoSize = true;
            this.lblDocSufix.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocSufix.Location = new System.Drawing.Point(16, 41);
            this.lblDocSufix.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDocSufix.Name = "lblDocSufix";
            this.lblDocSufix.Size = new System.Drawing.Size(47, 19);
            this.lblDocSufix.TabIndex = 16;
            this.lblDocSufix.Text = "Sufix";
            // 
            // TxtPrefix
            // 
            this.TxtPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPrefix.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPrefix.Location = new System.Drawing.Point(148, 12);
            this.TxtPrefix.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.TxtPrefix.MaxLength = 50;
            this.TxtPrefix.Name = "TxtPrefix";
            this.TxtPrefix.Size = new System.Drawing.Size(143, 25);
            this.TxtPrefix.TabIndex = 0;
            this.TxtPrefix.Leave += new System.EventHandler(this.TxtPrefix_Leave);
            // 
            // lblDOcPrefix
            // 
            this.lblDOcPrefix.AutoSize = true;
            this.lblDOcPrefix.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOcPrefix.Location = new System.Drawing.Point(16, 15);
            this.lblDOcPrefix.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDOcPrefix.Name = "lblDOcPrefix";
            this.lblDOcPrefix.Size = new System.Drawing.Size(52, 19);
            this.lblDOcPrefix.TabIndex = 14;
            this.lblDOcPrefix.Text = "Prefix";
            this.lblDOcPrefix.Click += new System.EventHandler(this.lblDOcPrefix_Click);
            // 
            // lblDocEnd
            // 
            this.lblDocEnd.AutoSize = true;
            this.lblDocEnd.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocEnd.Location = new System.Drawing.Point(5, 41);
            this.lblDocEnd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDocEnd.Name = "lblDocEnd";
            this.lblDocEnd.Size = new System.Drawing.Size(79, 19);
            this.lblDocEnd.TabIndex = 8;
            this.lblDocEnd.Text = "End Date";
            // 
            // lblDocStart
            // 
            this.lblDocStart.AutoSize = true;
            this.lblDocStart.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocStart.Location = new System.Drawing.Point(5, 14);
            this.lblDocStart.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDocStart.Name = "lblDocStart";
            this.lblDocStart.Size = new System.Drawing.Size(88, 19);
            this.lblDocStart.TabIndex = 6;
            this.lblDocStart.Text = "Start Date";
            // 
            // TxtDescription
            // 
            this.TxtDescription.BackColor = System.Drawing.Color.White;
            this.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDescription.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDescription.Location = new System.Drawing.Point(104, 70);
            this.TxtDescription.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.TxtDescription.MaxLength = 50;
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(399, 25);
            this.TxtDescription.TabIndex = 5;
            this.TxtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDescription_KeyDown);
            this.TxtDescription.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDescription_Validating);
            // 
            // lblDocDesc
            // 
            this.lblDocDesc.AutoSize = true;
            this.lblDocDesc.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocDesc.Location = new System.Drawing.Point(5, 73);
            this.lblDocDesc.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDocDesc.Name = "lblDocDesc";
            this.lblDocDesc.Size = new System.Drawing.Size(95, 19);
            this.lblDocDesc.TabIndex = 4;
            this.lblDocDesc.Text = "Description";
            // 
            // CmbUser
            // 
            this.CmbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbUser.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbUser.FormattingEnabled = true;
            this.CmbUser.Location = new System.Drawing.Point(97, 12);
            this.CmbUser.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.CmbUser.Name = "CmbUser";
            this.CmbUser.Size = new System.Drawing.Size(202, 27);
            this.CmbUser.TabIndex = 0;
            // 
            // lblDocUser
            // 
            this.lblDocUser.AutoSize = true;
            this.lblDocUser.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocUser.Location = new System.Drawing.Point(8, 15);
            this.lblDocUser.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDocUser.Name = "lblDocUser";
            this.lblDocUser.Size = new System.Drawing.Size(45, 19);
            this.lblDocUser.TabIndex = 2;
            this.lblDocUser.Text = "User";
            this.lblDocUser.Click += new System.EventHandler(this.lblDocUser_Click);
            // 
            // lblDocName
            // 
            this.lblDocName.AutoSize = true;
            this.lblDocName.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocName.Location = new System.Drawing.Point(8, 48);
            this.lblDocName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDocName.Name = "lblDocName";
            this.lblDocName.Size = new System.Drawing.Size(63, 19);
            this.lblDocName.TabIndex = 0;
            this.lblDocName.Text = "Module";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.MskStartDate);
            this.groupBox1.Controls.Add(this.lblDocEnd);
            this.groupBox1.Controls.Add(this.MskEndDate);
            this.groupBox1.Controls.Add(this.rChkAutoNumber);
            this.groupBox1.Controls.Add(this.rChkAlphaNumber);
            this.groupBox1.Controls.Add(this.rChkNumerical);
            this.groupBox1.Controls.Add(this.lblDocStart);
            this.groupBox1.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.groupBox1.Location = new System.Drawing.Point(8, 87);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(229, 146);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // rChkAutoNumber
            // 
            this.rChkAutoNumber.AutoSize = true;
            this.rChkAutoNumber.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rChkAutoNumber.Location = new System.Drawing.Point(9, 118);
            this.rChkAutoNumber.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.rChkAutoNumber.Name = "rChkAutoNumber";
            this.rChkAutoNumber.Size = new System.Drawing.Size(61, 23);
            this.rChkAutoNumber.TabIndex = 4;
            this.rChkAutoNumber.TabStop = true;
            this.rChkAutoNumber.Text = "Auto";
            this.rChkAutoNumber.UseVisualStyleBackColor = true;
            // 
            // rChkAlphaNumber
            // 
            this.rChkAlphaNumber.AllowDrop = true;
            this.rChkAlphaNumber.AutoSize = true;
            this.rChkAlphaNumber.Checked = true;
            this.rChkAlphaNumber.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rChkAlphaNumber.Location = new System.Drawing.Point(9, 72);
            this.rChkAlphaNumber.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.rChkAlphaNumber.Name = "rChkAlphaNumber";
            this.rChkAlphaNumber.Size = new System.Drawing.Size(152, 23);
            this.rChkAlphaNumber.TabIndex = 2;
            this.rChkAlphaNumber.TabStop = true;
            this.rChkAlphaNumber.Text = "Alpha Numerical";
            this.rChkAlphaNumber.UseVisualStyleBackColor = true;
            // 
            // rChkNumerical
            // 
            this.rChkNumerical.AutoSize = true;
            this.rChkNumerical.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rChkNumerical.Location = new System.Drawing.Point(9, 95);
            this.rChkNumerical.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.rChkNumerical.Name = "rChkNumerical";
            this.rChkNumerical.Size = new System.Drawing.Size(105, 23);
            this.rChkNumerical.TabIndex = 3;
            this.rChkNumerical.Text = "Numerical";
            this.rChkNumerical.UseVisualStyleBackColor = true;
            // 
            // CmbDesign
            // 
            this.CmbDesign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbDesign.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbDesign.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbDesign.FormattingEnabled = true;
            this.CmbDesign.Location = new System.Drawing.Point(147, 15);
            this.CmbDesign.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.CmbDesign.Name = "CmbDesign";
            this.CmbDesign.Size = new System.Drawing.Size(375, 27);
            this.CmbDesign.TabIndex = 0;
            this.CmbDesign.SelectedIndexChanged += new System.EventHandler(this.CmbDesign_SelectedIndexChanged);
            // 
            // grpDocNumberInfo
            // 
            this.grpDocNumberInfo.Controls.Add(this.CmbUnit);
            this.grpDocNumberInfo.Controls.Add(this.label2);
            this.grpDocNumberInfo.Controls.Add(this.CmbBranch);
            this.grpDocNumberInfo.Controls.Add(this.label1);
            this.grpDocNumberInfo.Controls.Add(this.lblDocEndNumber);
            this.grpDocNumberInfo.Controls.Add(this.TxtEnd);
            this.grpDocNumberInfo.Controls.Add(this.lblDocCurrentNumber);
            this.grpDocNumberInfo.Controls.Add(this.TxtCurrent);
            this.grpDocNumberInfo.Controls.Add(this.TxtStart);
            this.grpDocNumberInfo.Controls.Add(this.lblDocStartNumber);
            this.grpDocNumberInfo.Controls.Add(this.CmbUser);
            this.grpDocNumberInfo.Controls.Add(this.lblDocUser);
            this.grpDocNumberInfo.Font = new System.Drawing.Font("Arial", 10F);
            this.grpDocNumberInfo.Location = new System.Drawing.Point(8, 227);
            this.grpDocNumberInfo.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.grpDocNumberInfo.Name = "grpDocNumberInfo";
            this.grpDocNumberInfo.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.grpDocNumberInfo.Size = new System.Drawing.Size(528, 106);
            this.grpDocNumberInfo.TabIndex = 8;
            this.grpDocNumberInfo.TabStop = false;
            // 
            // CmbUnit
            // 
            this.CmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbUnit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbUnit.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbUnit.FormattingEnabled = true;
            this.CmbUnit.Location = new System.Drawing.Point(97, 72);
            this.CmbUnit.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.CmbUnit.Name = "CmbUnit";
            this.CmbUnit.Size = new System.Drawing.Size(202, 27);
            this.CmbUnit.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 76);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 19);
            this.label2.TabIndex = 34;
            this.label2.Text = "Unit";
            // 
            // CmbBranch
            // 
            this.CmbBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbBranch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbBranch.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbBranch.FormattingEnabled = true;
            this.CmbBranch.Location = new System.Drawing.Point(97, 42);
            this.CmbBranch.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.CmbBranch.Name = "CmbBranch";
            this.CmbBranch.Size = new System.Drawing.Size(203, 27);
            this.CmbBranch.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 19);
            this.label1.TabIndex = 25;
            this.label1.Text = "Branch";
            // 
            // lblDocEndNumber
            // 
            this.lblDocEndNumber.AutoSize = true;
            this.lblDocEndNumber.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocEndNumber.Location = new System.Drawing.Point(318, 76);
            this.lblDocEndNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDocEndNumber.Name = "lblDocEndNumber";
            this.lblDocEndNumber.Size = new System.Drawing.Size(102, 19);
            this.lblDocEndNumber.TabIndex = 30;
            this.lblDocEndNumber.Text = "End Number";
            // 
            // TxtEnd
            // 
            this.TxtEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtEnd.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtEnd.Location = new System.Drawing.Point(427, 73);
            this.TxtEnd.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.TxtEnd.MaxLength = 18;
            this.TxtEnd.Name = "TxtEnd";
            this.TxtEnd.Size = new System.Drawing.Size(95, 25);
            this.TxtEnd.TabIndex = 5;
            this.TxtEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtEnd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtEnd_KeyPress);
            // 
            // lblDocCurrentNumber
            // 
            this.lblDocCurrentNumber.AutoSize = true;
            this.lblDocCurrentNumber.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocCurrentNumber.Location = new System.Drawing.Point(318, 46);
            this.lblDocCurrentNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDocCurrentNumber.Name = "lblDocCurrentNumber";
            this.lblDocCurrentNumber.Size = new System.Drawing.Size(98, 19);
            this.lblDocCurrentNumber.TabIndex = 28;
            this.lblDocCurrentNumber.Text = "Current No.";
            // 
            // TxtCurrent
            // 
            this.TxtCurrent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCurrent.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCurrent.Location = new System.Drawing.Point(427, 43);
            this.TxtCurrent.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.TxtCurrent.MaxLength = 18;
            this.TxtCurrent.Name = "TxtCurrent";
            this.TxtCurrent.Size = new System.Drawing.Size(95, 25);
            this.TxtCurrent.TabIndex = 4;
            this.TxtCurrent.Text = "1";
            this.TxtCurrent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtCurrent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCurrent_KeyDown);
            this.TxtCurrent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCurrent_KeyPress);
            // 
            // TxtStart
            // 
            this.TxtStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtStart.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtStart.Location = new System.Drawing.Point(427, 12);
            this.TxtStart.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.TxtStart.MaxLength = 18;
            this.TxtStart.Name = "TxtStart";
            this.TxtStart.Size = new System.Drawing.Size(95, 25);
            this.TxtStart.TabIndex = 3;
            this.TxtStart.Text = "1";
            this.TxtStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtStart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtStart_KeyDown);
            this.TxtStart.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtStart_KeyPress);
            // 
            // lblDocStartNumber
            // 
            this.lblDocStartNumber.AutoSize = true;
            this.lblDocStartNumber.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocStartNumber.Location = new System.Drawing.Point(318, 15);
            this.lblDocStartNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDocStartNumber.Name = "lblDocStartNumber";
            this.lblDocStartNumber.Size = new System.Drawing.Size(76, 19);
            this.lblDocStartNumber.TabIndex = 25;
            this.lblDocStartNumber.Text = "Start No.";
            // 
            // lblDocDefaultDesign
            // 
            this.lblDocDefaultDesign.AutoSize = true;
            this.lblDocDefaultDesign.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocDefaultDesign.Location = new System.Drawing.Point(9, 18);
            this.lblDocDefaultDesign.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDocDefaultDesign.Name = "lblDocDefaultDesign";
            this.lblDocDefaultDesign.Size = new System.Drawing.Size(122, 19);
            this.lblDocDefaultDesign.TabIndex = 30;
            this.lblDocDefaultDesign.Text = "Default Design";
            // 
            // ChkActive
            // 
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkActive.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ChkActive.Location = new System.Drawing.Point(12, 377);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkActive.Size = new System.Drawing.Size(93, 24);
            this.ChkActive.TabIndex = 13;
            this.ChkActive.Text = "Active";
            this.ChkActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkActive.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CmbDesign);
            this.groupBox3.Controls.Add(this.lblDocDefaultDesign);
            this.groupBox3.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(8, 324);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(528, 43);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.BtnCancel.Location = new System.Drawing.Point(430, 375);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(107, 34);
            this.BtnCancel.TabIndex = 12;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(343, 375);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(86, 34);
            this.BtnSave.TabIndex = 11;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnExit.Location = new System.Drawing.Point(461, 4);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(76, 32);
            this.BtnExit.TabIndex = 3;
            this.BtnExit.Text = "E&XIT";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(166, 4);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(103, 32);
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
            this.BtnEdit.Location = new System.Drawing.Point(83, 4);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(82, 32);
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
            this.BtnNew.Location = new System.Drawing.Point(3, 4);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(79, 32);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Tag = "NewButtonCheck";
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.BtnView);
            this.StorePanel.Controls.Add(this.BtnDocumentSchema);
            this.StorePanel.Controls.Add(this.TxtDocumentDesc);
            this.StorePanel.Controls.Add(this.BtnDescription);
            this.StorePanel.Controls.Add(this.ChkActive);
            this.StorePanel.Controls.Add(this.clsSeparator1);
            this.StorePanel.Controls.Add(this.BtnCancel);
            this.StorePanel.Controls.Add(this.BtnSave);
            this.StorePanel.Controls.Add(this.clsSeparator2);
            this.StorePanel.Controls.Add(this.BtnExit);
            this.StorePanel.Controls.Add(this.BtnDelete);
            this.StorePanel.Controls.Add(this.BtnEdit);
            this.StorePanel.Controls.Add(this.BtnNew);
            this.StorePanel.Controls.Add(this.lblDocName);
            this.StorePanel.Controls.Add(this.lblDocDesc);
            this.StorePanel.Controls.Add(this.TxtDescription);
            this.StorePanel.Controls.Add(this.groupBox1);
            this.StorePanel.Controls.Add(this.groupBox2);
            this.StorePanel.Controls.Add(this.MskEDate);
            this.StorePanel.Controls.Add(this.MskSDate);
            this.StorePanel.Controls.Add(this.grpDocNumberInfo);
            this.StorePanel.Controls.Add(this.groupBox3);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(542, 412);
            this.StorePanel.TabIndex = 0;
            // 
            // BtnView
            // 
            this.BtnView.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnView.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnView.Appearance.Options.UseFont = true;
            this.BtnView.Appearance.Options.UseForeColor = true;
            this.BtnView.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnView.Location = new System.Drawing.Point(270, 4);
            this.BtnView.Margin = new System.Windows.Forms.Padding(4);
            this.BtnView.Name = "BtnView";
            this.BtnView.Size = new System.Drawing.Size(100, 32);
            this.BtnView.TabIndex = 209;
            this.BtnView.Tag = "ViewButtonCheck";
            this.BtnView.Text = "&VIEW";
            this.BtnView.Click += new System.EventHandler(this.BtnView_Click);
            // 
            // BtnDocumentSchema
            // 
            this.BtnDocumentSchema.CausesValidation = false;
            this.BtnDocumentSchema.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.BtnDocumentSchema.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnDocumentSchema.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDocumentSchema.Location = new System.Drawing.Point(504, 44);
            this.BtnDocumentSchema.Name = "BtnDocumentSchema";
            this.BtnDocumentSchema.Size = new System.Drawing.Size(25, 25);
            this.BtnDocumentSchema.TabIndex = 208;
            this.BtnDocumentSchema.TabStop = false;
            this.BtnDocumentSchema.UseVisualStyleBackColor = false;
            this.BtnDocumentSchema.Click += new System.EventHandler(this.BtnDocumentSchema_Click);
            // 
            // TxtDocumentDesc
            // 
            this.TxtDocumentDesc.BackColor = System.Drawing.Color.White;
            this.TxtDocumentDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDocumentDesc.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDocumentDesc.Location = new System.Drawing.Point(104, 44);
            this.TxtDocumentDesc.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.TxtDocumentDesc.MaxLength = 50;
            this.TxtDocumentDesc.Name = "TxtDocumentDesc";
            this.TxtDocumentDesc.ReadOnly = true;
            this.TxtDocumentDesc.Size = new System.Drawing.Size(399, 25);
            this.TxtDocumentDesc.TabIndex = 4;
            this.TxtDocumentDesc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDocumentDesc_KeyDown);
            this.TxtDocumentDesc.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDocumentDesc_Validating);
            // 
            // BtnDescription
            // 
            this.BtnDescription.CausesValidation = false;
            this.BtnDescription.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.BtnDescription.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnDescription.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDescription.Location = new System.Drawing.Point(504, 70);
            this.BtnDescription.Name = "BtnDescription";
            this.BtnDescription.Size = new System.Drawing.Size(25, 25);
            this.BtnDescription.TabIndex = 6;
            this.BtnDescription.TabStop = false;
            this.BtnDescription.UseVisualStyleBackColor = false;
            this.BtnDescription.Click += new System.EventHandler(this.BtnDescription_Click);
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(13, 370);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(527, 2);
            this.clsSeparator1.TabIndex = 20;
            this.clsSeparator1.TabStop = false;
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(8, 39);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(529, 2);
            this.clsSeparator2.TabIndex = 43;
            this.clsSeparator2.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblDOcPrefix);
            this.groupBox2.Controls.Add(this.TxtPrefix);
            this.groupBox2.Controls.Add(this.lblDocSufix);
            this.groupBox2.Controls.Add(this.TxtSufix);
            this.groupBox2.Controls.Add(this.lblDocBodylenght);
            this.groupBox2.Controls.Add(this.TxtBodyLength);
            this.groupBox2.Controls.Add(this.ChkBlankNumber);
            this.groupBox2.Controls.Add(this.lblDocTotalLength);
            this.groupBox2.Controls.Add(this.TxtBlank);
            this.groupBox2.Controls.Add(this.TxtTotalLength);
            this.groupBox2.Location = new System.Drawing.Point(239, 88);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(297, 146);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // MskEDate
            // 
            this.MskEDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskEDate.Enabled = false;
            this.MskEDate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskEDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskEDate.Location = new System.Drawing.Point(240, 129);
            this.MskEDate.Mask = "00/00/0000";
            this.MskEDate.Name = "MskEDate";
            this.MskEDate.Size = new System.Drawing.Size(47, 25);
            this.MskEDate.TabIndex = 206;
            this.MskEDate.Tag = "DD/MM/YYYY";
            this.MskEDate.Visible = false;
            // 
            // MskSDate
            // 
            this.MskSDate.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.MskSDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskSDate.Enabled = false;
            this.MskSDate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskSDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskSDate.Location = new System.Drawing.Point(240, 103);
            this.MskSDate.Mask = "00/00/0000";
            this.MskSDate.Name = "MskSDate";
            this.MskSDate.Size = new System.Drawing.Size(47, 25);
            this.MskSDate.TabIndex = 205;
            this.MskSDate.Tag = "dd/MM/yyyy";
            this.MskSDate.Visible = false;
            // 
            // FrmDocumentNumbering
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(542, 412);
            this.Controls.Add(this.StorePanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.Name = "FrmDocumentNumbering";
            this.ShowIcon = false;
            this.Tag = "Voucher Numbering";
            this.Text = "VOUCHER NUMBERING SCHEME";
            this.Load += new System.EventHandler(this.FrmDocumentNumbering_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmDocumentNumbering_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpDocNumberInfo.ResumeLayout(false);
            this.grpDocNumberInfo.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.StorePanel.ResumeLayout(false);
            this.StorePanel.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblDocEnd;
        private System.Windows.Forms.Label lblDocStart;
        private System.Windows.Forms.Label lblDocDesc;
        private System.Windows.Forms.ComboBox CmbUser;
        private System.Windows.Forms.Label lblDocUser;
        private System.Windows.Forms.ComboBox CmbDesign;
        private System.Windows.Forms.Label lblDocName;
        private System.Windows.Forms.RadioButton rChkAutoNumber;
        private System.Windows.Forms.RadioButton rChkNumerical;
        private System.Windows.Forms.Label lblDocTotalLength;
        private System.Windows.Forms.RadioButton rChkAlphaNumber;
        private System.Windows.Forms.Label lblDocBodylenght;
        private System.Windows.Forms.Label lblDocSufix;
        private System.Windows.Forms.Label lblDOcPrefix;
        private System.Windows.Forms.GroupBox grpDocNumberInfo;
        private System.Windows.Forms.CheckBox ChkBlankNumber;
        private System.Windows.Forms.Label lblDocEndNumber;
        private System.Windows.Forms.Label lblDocCurrentNumber;
        private System.Windows.Forms.Label lblDocStartNumber;
        private System.Windows.Forms.Label lblDocDefaultDesign;
        private System.Windows.Forms.ComboBox CmbUnit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CmbBranch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ChkActive;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private ClsSeparator clsSeparator2;
        private ClsSeparator clsSeparator1;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Button BtnDescription;
		public System.Windows.Forms.Button BtnDocumentSchema;
		private DevExpress.XtraEditors.SimpleButton BtnView;
        private MrTextBox TxtDescription;
        private MrTextBox TxtTotalLength;
        private MrTextBox TxtBodyLength;
        private MrTextBox TxtSufix;
        private MrTextBox TxtPrefix;
        private MrTextBox TxtBlank;
        private MrTextBox TxtEnd;
        private MrTextBox TxtCurrent;
        private MrTextBox TxtStart;
        private MrMaskedTextBox MskEndDate;
        private MrMaskedTextBox MskStartDate;
        private MrPanel StorePanel;
        private MrMaskedTextBox MskEDate;
        private MrMaskedTextBox MskSDate;
        private MrTextBox TxtDocumentDesc;
    }
}