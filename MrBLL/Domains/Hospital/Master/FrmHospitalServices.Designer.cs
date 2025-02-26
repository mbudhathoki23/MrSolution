using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.Hospital.Master
{
    partial class FrmHospitalServices
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHospitalServices));
            this.lvlProductTaxable = new System.Windows.Forms.Label();
            this.TxtVat = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lvlProductSubGroup = new System.Windows.Forms.Label();
            this.lvlProductGroup = new System.Windows.Forms.Label();
            this.lvlProductSalesRate = new System.Windows.Forms.Label();
            this.TxtSales = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.ChkActive = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtShortName = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtDescription = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lvlProductName = new System.Windows.Forms.Label();
            this.CmbType = new System.Windows.Forms.ComboBox();
            this.PanelHeader = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.button2 = new System.Windows.Forms.Button();
            this.mrTextBox2 = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.mrTextBox1 = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.BtnDescription = new System.Windows.Forms.Button();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.btnNew = new DevExpress.XtraEditors.SimpleButton();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.PanelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvlProductTaxable
            // 
            this.lvlProductTaxable.AutoSize = true;
            this.lvlProductTaxable.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductTaxable.Location = new System.Drawing.Point(365, 95);
            this.lvlProductTaxable.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lvlProductTaxable.Name = "lvlProductTaxable";
            this.lvlProductTaxable.Size = new System.Drawing.Size(172, 21);
            this.lvlProductTaxable.TabIndex = 21;
            this.lvlProductTaxable.Text = "VAT/HST Rate %";
            // 
            // TxtVat
            // 
            this.TxtVat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtVat.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtVat.Location = new System.Drawing.Point(573, 91);
            this.TxtVat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtVat.Name = "TxtVat";
            this.TxtVat.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TxtVat.Size = new System.Drawing.Size(91, 29);
            this.TxtVat.TabIndex = 8;
            this.TxtVat.Text = "5";
            // 
            // lvlProductSubGroup
            // 
            this.lvlProductSubGroup.AutoSize = true;
            this.lvlProductSubGroup.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductSubGroup.Location = new System.Drawing.Point(12, 202);
            this.lvlProductSubGroup.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lvlProductSubGroup.Name = "lvlProductSubGroup";
            this.lvlProductSubGroup.Size = new System.Drawing.Size(107, 21);
            this.lvlProductSubGroup.TabIndex = 18;
            this.lvlProductSubGroup.Text = "SubGroup";
            // 
            // lvlProductGroup
            // 
            this.lvlProductGroup.AutoSize = true;
            this.lvlProductGroup.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductGroup.Location = new System.Drawing.Point(12, 169);
            this.lvlProductGroup.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lvlProductGroup.Name = "lvlProductGroup";
            this.lvlProductGroup.Size = new System.Drawing.Size(69, 21);
            this.lvlProductGroup.TabIndex = 16;
            this.lvlProductGroup.Text = "Group";
            // 
            // lvlProductSalesRate
            // 
            this.lvlProductSalesRate.AutoSize = true;
            this.lvlProductSalesRate.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductSalesRate.Location = new System.Drawing.Point(373, 130);
            this.lvlProductSalesRate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lvlProductSalesRate.Name = "lvlProductSalesRate";
            this.lvlProductSalesRate.Size = new System.Drawing.Size(108, 21);
            this.lvlProductSalesRate.TabIndex = 15;
            this.lvlProductSalesRate.Text = "Sales Rate";
            // 
            // TxtSales
            // 
            this.TxtSales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSales.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSales.Location = new System.Drawing.Point(523, 127);
            this.TxtSales.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtSales.Name = "TxtSales";
            this.TxtSales.Size = new System.Drawing.Size(142, 29);
            this.TxtSales.TabIndex = 12;
            this.TxtSales.Text = "0.00";
            this.TxtSales.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ChkActive
            // 
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkActive.Location = new System.Drawing.Point(11, 245);
            this.ChkActive.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkActive.Size = new System.Drawing.Size(148, 32);
            this.ChkActive.TabIndex = 13;
            this.ChkActive.Text = "Active";
            this.ChkActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkActive.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 132);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 21);
            this.label2.TabIndex = 55;
            this.label2.Text = "Type";
            // 
            // TxtShortName
            // 
            this.TxtShortName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtShortName.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtShortName.Location = new System.Drawing.Point(147, 92);
            this.TxtShortName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtShortName.Name = "TxtShortName";
            this.TxtShortName.Size = new System.Drawing.Size(213, 29);
            this.TxtShortName.TabIndex = 5;
            // 
            // TxtDescription
            // 
            this.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDescription.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDescription.Location = new System.Drawing.Point(147, 57);
            this.TxtDescription.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(518, 29);
            this.TxtDescription.TabIndex = 3;
            // 
            // lvlProductName
            // 
            this.lvlProductName.AutoSize = true;
            this.lvlProductName.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlProductName.Location = new System.Drawing.Point(12, 62);
            this.lvlProductName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lvlProductName.Name = "lvlProductName";
            this.lvlProductName.Size = new System.Drawing.Size(118, 21);
            this.lvlProductName.TabIndex = 0;
            this.lvlProductName.Text = "Description";
            // 
            // CmbType
            // 
            this.CmbType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.CmbType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbType.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbType.FormattingEnabled = true;
            this.CmbType.Items.AddRange(new object[] {
            "OPD",
            "IPD",
            "Pharma",
            "EMG",
            "LAB",
            "Other"});
            this.CmbType.Location = new System.Drawing.Point(147, 127);
            this.CmbType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CmbType.Name = "CmbType";
            this.CmbType.Size = new System.Drawing.Size(212, 28);
            this.CmbType.TabIndex = 6;
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.button2);
            this.PanelHeader.Controls.Add(this.mrTextBox2);
            this.PanelHeader.Controls.Add(this.button1);
            this.PanelHeader.Controls.Add(this.mrTextBox1);
            this.PanelHeader.Controls.Add(this.BtnDescription);
            this.PanelHeader.Controls.Add(this.btnCancel);
            this.PanelHeader.Controls.Add(this.btnSave);
            this.PanelHeader.Controls.Add(this.clsSeparator2);
            this.PanelHeader.Controls.Add(this.clsSeparator1);
            this.PanelHeader.Controls.Add(this.btnNew);
            this.PanelHeader.Controls.Add(this.btnExit);
            this.PanelHeader.Controls.Add(this.btnDelete);
            this.PanelHeader.Controls.Add(this.btnEdit);
            this.PanelHeader.Controls.Add(this.label1);
            this.PanelHeader.Controls.Add(this.ChkActive);
            this.PanelHeader.Controls.Add(this.CmbType);
            this.PanelHeader.Controls.Add(this.label2);
            this.PanelHeader.Controls.Add(this.TxtDescription);
            this.PanelHeader.Controls.Add(this.lvlProductName);
            this.PanelHeader.Controls.Add(this.lvlProductTaxable);
            this.PanelHeader.Controls.Add(this.TxtVat);
            this.PanelHeader.Controls.Add(this.TxtShortName);
            this.PanelHeader.Controls.Add(this.lvlProductSubGroup);
            this.PanelHeader.Controls.Add(this.lvlProductGroup);
            this.PanelHeader.Controls.Add(this.TxtSales);
            this.PanelHeader.Controls.Add(this.lvlProductSalesRate);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(703, 293);
            this.PanelHeader.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.button2.ForeColor = System.Drawing.SystemColors.Window;
            this.button2.Image = global::MrBLL.Properties.Resources.search16;
            this.button2.Location = new System.Drawing.Point(667, 198);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(36, 30);
            this.button2.TabIndex = 69;
            this.button2.TabStop = false;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // mrTextBox2
            // 
            this.mrTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mrTextBox2.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrTextBox2.Location = new System.Drawing.Point(147, 198);
            this.mrTextBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mrTextBox2.Name = "mrTextBox2";
            this.mrTextBox2.Size = new System.Drawing.Size(518, 29);
            this.mrTextBox2.TabIndex = 68;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.button1.ForeColor = System.Drawing.SystemColors.Window;
            this.button1.Image = global::MrBLL.Properties.Resources.search16;
            this.button1.Location = new System.Drawing.Point(667, 165);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(36, 30);
            this.button1.TabIndex = 67;
            this.button1.TabStop = false;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // mrTextBox1
            // 
            this.mrTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mrTextBox1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrTextBox1.Location = new System.Drawing.Point(147, 165);
            this.mrTextBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mrTextBox1.Name = "mrTextBox1";
            this.mrTextBox1.Size = new System.Drawing.Size(518, 29);
            this.mrTextBox1.TabIndex = 66;
            // 
            // BtnDescription
            // 
            this.BtnDescription.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnDescription.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnDescription.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDescription.Location = new System.Drawing.Point(667, 57);
            this.BtnDescription.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnDescription.Name = "BtnDescription";
            this.BtnDescription.Size = new System.Drawing.Size(36, 30);
            this.BtnDescription.TabIndex = 4;
            this.BtnDescription.TabStop = false;
            this.BtnDescription.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.btnCancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Appearance.Options.UseForeColor = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.btnCancel.Location = new System.Drawing.Point(553, 242);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(145, 46);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "&CANCEL";
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.btnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Appearance.Options.UseForeColor = true;
            this.btnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.btnSave.Location = new System.Drawing.Point(405, 242);
            this.btnSave.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(144, 46);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "&SAVE";
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(19, 234);
            this.clsSeparator2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clsSeparator2.Size = new System.Drawing.Size(680, 2);
            this.clsSeparator2.TabIndex = 65;
            this.clsSeparator2.TabStop = false;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(13, 49);
            this.clsSeparator1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clsSeparator1.Size = new System.Drawing.Size(685, 2);
            this.clsSeparator1.TabIndex = 64;
            this.clsSeparator1.TabStop = false;
            // 
            // btnNew
            // 
            this.btnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnNew.Appearance.Options.UseFont = true;
            this.btnNew.Appearance.Options.UseForeColor = true;
            this.btnNew.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.btnNew.Location = new System.Drawing.Point(11, 5);
            this.btnNew.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(105, 41);
            this.btnNew.TabIndex = 0;
            this.btnNew.Tag = "NewButtonCheck";
            this.btnNew.Text = "&NEW";
            // 
            // btnExit
            // 
            this.btnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.Appearance.Options.UseForeColor = true;
            this.btnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.btnExit.Location = new System.Drawing.Point(597, 5);
            this.btnExit.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.btnExit.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(101, 41);
            this.btnExit.TabIndex = 16;
            this.btnExit.Text = "E&XIT";
            // 
            // btnDelete
            // 
            this.btnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnDelete.Appearance.Options.UseFont = true;
            this.btnDelete.Appearance.Options.UseForeColor = true;
            this.btnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.btnDelete.Location = new System.Drawing.Point(228, 5);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(137, 41);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Tag = "DeleteButtonCheck";
            this.btnDelete.Text = "&DELETE";
            // 
            // btnEdit
            // 
            this.btnEdit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnEdit.Appearance.Options.UseFont = true;
            this.btnEdit.Appearance.Options.UseForeColor = true;
            this.btnEdit.ImageOptions.Image = global::MrBLL.Properties.Resources.Edit;
            this.btnEdit.Location = new System.Drawing.Point(117, 5);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(109, 41);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Tag = "EditButtonCheck";
            this.btnEdit.Text = "&EDIT";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 95);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 21);
            this.label1.TabIndex = 59;
            this.label1.Text = "ShortName";
            // 
            // FrmHospitalServices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(703, 293);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.MaximizeBox = false;
            this.Name = "FrmHospitalServices";
            this.ShowIcon = false;
            this.Tag = "HOSPITAL SERVICE ITEM";
            this.Text = "HOSPITAL SERVICE ITEM";
            this.Load += new System.EventHandler(this.FrmItem_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmItem_KeyPress);
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lvlProductTaxable;
        private System.Windows.Forms.Label lvlProductSubGroup;
        private System.Windows.Forms.Label lvlProductGroup;
        private System.Windows.Forms.Label lvlProductSalesRate;
        private System.Windows.Forms.CheckBox ChkActive;
        private System.Windows.Forms.Label lvlProductName;
        private System.Windows.Forms.ComboBox CmbType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ClsSeparator clsSeparator1;
        private DevExpress.XtraEditors.SimpleButton btnNew;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private ClsSeparator clsSeparator2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.Button BtnDescription;
        private MrTextBox TxtVat;
        private MrTextBox TxtSales;
        private MrTextBox TxtShortName;
        private MrTextBox TxtDescription;
        private System.Windows.Forms.Button button1;
        private MrTextBox mrTextBox1;
        private System.Windows.Forms.Button button2;
        private MrTextBox mrTextBox2;
        private MrPanel PanelHeader;
    }
}