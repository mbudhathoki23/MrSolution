
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Utility.MrLicense
{
    partial class LicenseGenerator
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new MrPanel();
            this.llReadLicense = new System.Windows.Forms.LinkLabel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnGenerateLicense = new System.Windows.Forms.Button();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnGenClientId = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.rdoMultiBranch = new System.Windows.Forms.RadioButton();
            this.rdoSingleBranch = new System.Windows.Forms.RadioButton();
            this.cbxEditions = new System.Windows.Forms.ComboBox();
            this.nudVersion = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLicenseTo = new MrTextBox();
            this.bsOutlets = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.txtOriginGroupId = new MrTextBox();
            this.bsHardwareIds = new System.Windows.Forms.BindingSource(this.components);
            this.gridHardwareIds = new System.Windows.Forms.DataGridView();
            this.gridOutlets = new System.Windows.Forms.DataGridView();
            this.bsOutletTypes = new System.Windows.Forms.BindingSource(this.components);
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRemoveHwId = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colOutletType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colOriginId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaxUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaxPc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExpiry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRemoveOutlet = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVersion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsOutlets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsHardwareIds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHardwareIds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridOutlets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsOutletTypes)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.llReadLicense);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnGenerateLicense);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 696);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1097, 58);
            this.panel1.TabIndex = 112;
            // 
            // llReadLicense
            // 
            this.llReadLicense.AutoSize = true;
            this.llReadLicense.Font = new System.Drawing.Font("Tahoma", 9F);
            this.llReadLicense.Location = new System.Drawing.Point(13, 22);
            this.llReadLicense.Name = "llReadLicense";
            this.llReadLicense.Size = new System.Drawing.Size(78, 14);
            this.llReadLicense.TabIndex = 112;
            this.llReadLicense.TabStop = true;
            this.llReadLicense.Text = "Read License";
            this.llReadLicense.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llReadLicense_LinkClicked);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnClose.Location = new System.Drawing.Point(964, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(127, 41);
            this.btnClose.TabIndex = 111;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnGenerateLicense
            // 
            this.btnGenerateLicense.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnGenerateLicense.Location = new System.Drawing.Point(831, 9);
            this.btnGenerateLicense.Name = "btnGenerateLicense";
            this.btnGenerateLicense.Size = new System.Drawing.Size(127, 41);
            this.btnGenerateLicense.TabIndex = 110;
            this.btnGenerateLicense.Text = "Generate";
            this.btnGenerateLicense.UseVisualStyleBackColor = true;
            this.btnGenerateLicense.Click += new System.EventHandler(this.btnGenerateLicense_Click);
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.DataPropertyName = "OutletUqId";
            this.Column4.HeaderText = "Outlet Uniqe Id";
            this.Column4.Name = "Column4";
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Hardware Ids";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = " ";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.Width = 30;
            // 
            // btnGenClientId
            // 
            this.btnGenClientId.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGenClientId.Location = new System.Drawing.Point(563, 132);
            this.btnGenClientId.Name = "btnGenClientId";
            this.btnGenClientId.Size = new System.Drawing.Size(72, 23);
            this.btnGenClientId.TabIndex = 178;
            this.btnGenClientId.Text = "Generate";
            this.btnGenClientId.UseVisualStyleBackColor = true;
            this.btnGenClientId.Click += new System.EventHandler(this.btnGenClientId_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(22, 135);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 16);
            this.label9.TabIndex = 177;
            this.label9.Text = "Origin Group";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(22, 99);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 16);
            this.label8.TabIndex = 176;
            this.label8.Text = "Branch Type";
            // 
            // rdoMultiBranch
            // 
            this.rdoMultiBranch.AutoSize = true;
            this.rdoMultiBranch.BackColor = System.Drawing.Color.Transparent;
            this.rdoMultiBranch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoMultiBranch.Location = new System.Drawing.Point(214, 97);
            this.rdoMultiBranch.Name = "rdoMultiBranch";
            this.rdoMultiBranch.Size = new System.Drawing.Size(97, 20);
            this.rdoMultiBranch.TabIndex = 175;
            this.rdoMultiBranch.Text = "Multi-Branch";
            this.rdoMultiBranch.UseVisualStyleBackColor = false;
            // 
            // rdoSingleBranch
            // 
            this.rdoSingleBranch.AutoSize = true;
            this.rdoSingleBranch.BackColor = System.Drawing.Color.Transparent;
            this.rdoSingleBranch.Checked = true;
            this.rdoSingleBranch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoSingleBranch.Location = new System.Drawing.Point(104, 97);
            this.rdoSingleBranch.Name = "rdoSingleBranch";
            this.rdoSingleBranch.Size = new System.Drawing.Size(104, 20);
            this.rdoSingleBranch.TabIndex = 174;
            this.rdoSingleBranch.TabStop = true;
            this.rdoSingleBranch.Text = "Single Branch";
            this.rdoSingleBranch.UseVisualStyleBackColor = false;
            // 
            // cbxEditions
            // 
            this.cbxEditions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEditions.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxEditions.FormattingEnabled = true;
            this.cbxEditions.Location = new System.Drawing.Point(103, 58);
            this.cbxEditions.Name = "cbxEditions";
            this.cbxEditions.Size = new System.Drawing.Size(532, 24);
            this.cbxEditions.TabIndex = 172;
            // 
            // nudVersion
            // 
            this.nudVersion.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudVersion.Location = new System.Drawing.Point(103, 170);
            this.nudVersion.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudVersion.Name = "nudVersion";
            this.nudVersion.Size = new System.Drawing.Size(49, 23);
            this.nudVersion.TabIndex = 171;
            this.nudVersion.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(22, 172);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 16);
            this.label6.TabIndex = 170;
            this.label6.Text = "Version";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(22, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 16);
            this.label3.TabIndex = 165;
            this.label3.Text = "Edition";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 16);
            this.label2.TabIndex = 164;
            this.label2.Text = "License To";
            // 
            // txtLicenseTo
            // 
            this.txtLicenseTo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicenseTo.Location = new System.Drawing.Point(103, 20);
            this.txtLicenseTo.Name = "txtLicenseTo";
            this.txtLicenseTo.Size = new System.Drawing.Size(532, 23);
            this.txtLicenseTo.TabIndex = 182;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Nature";
            this.dataGridViewTextBoxColumn2.HeaderText = "Outlet Type";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 130;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "OutletUqId";
            this.dataGridViewTextBoxColumn3.HeaderText = "Outlet Uniqe Id";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = " ";
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Width = 30;
            // 
            // txtOriginGroupId
            // 
            this.txtOriginGroupId.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOriginGroupId.Location = new System.Drawing.Point(103, 132);
            this.txtOriginGroupId.Name = "txtOriginGroupId";
            this.txtOriginGroupId.Size = new System.Drawing.Size(454, 23);
            this.txtOriginGroupId.TabIndex = 184;
            // 
            // gridHardwareIds
            // 
            this.gridHardwareIds.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridHardwareIds.AutoGenerateColumns = false;
            this.gridHardwareIds.BackgroundColor = System.Drawing.SystemColors.Control;
            this.gridHardwareIds.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridHardwareIds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridHardwareIds.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.colRemoveHwId});
            this.gridHardwareIds.DataSource = this.bsHardwareIds;
            this.gridHardwareIds.GridColor = System.Drawing.SystemColors.Control;
            this.gridHardwareIds.Location = new System.Drawing.Point(12, 199);
            this.gridHardwareIds.Name = "gridHardwareIds";
            this.gridHardwareIds.Size = new System.Drawing.Size(1076, 312);
            this.gridHardwareIds.TabIndex = 186;
            this.gridHardwareIds.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridHardwareIds_CellContentClick);
            // 
            // gridOutlets
            // 
            this.gridOutlets.AllowUserToOrderColumns = true;
            this.gridOutlets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridOutlets.AutoGenerateColumns = false;
            this.gridOutlets.BackgroundColor = System.Drawing.SystemColors.Control;
            this.gridOutlets.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridOutlets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridOutlets.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colOutletType,
            this.colOriginId,
            this.colMaxUser,
            this.colMaxPc,
            this.colServer,
            this.colExpiry,
            this.colRemoveOutlet});
            this.gridOutlets.DataSource = this.bsOutlets;
            this.gridOutlets.GridColor = System.Drawing.SystemColors.Control;
            this.gridOutlets.Location = new System.Drawing.Point(12, 517);
            this.gridOutlets.Name = "gridOutlets";
            this.gridOutlets.Size = new System.Drawing.Size(1076, 167);
            this.gridOutlets.TabIndex = 187;
            this.gridOutlets.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridOutlets_CellContentClick);
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column5.HeaderText = "Unique Id";
            this.Column5.Name = "Column5";
            // 
            // colRemoveHwId
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Format = "Remove";
            dataGridViewCellStyle1.NullValue = "Remove";
            this.colRemoveHwId.DefaultCellStyle = dataGridViewCellStyle1;
            this.colRemoveHwId.HeaderText = "";
            this.colRemoveHwId.Name = "colRemoveHwId";
            this.colRemoveHwId.Text = "Remove";
            this.colRemoveHwId.Width = 80;
            // 
            // colOutletType
            // 
            this.colOutletType.DataSource = this.bsOutletTypes;
            this.colOutletType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.colOutletType.HeaderText = "Outlet Type";
            this.colOutletType.Name = "colOutletType";
            this.colOutletType.Width = 250;
            // 
            // colOriginId
            // 
            this.colOriginId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colOriginId.DataPropertyName = "OriginId";
            this.colOriginId.HeaderText = "Origin Id";
            this.colOriginId.Name = "colOriginId";
            // 
            // colMaxUser
            // 
            this.colMaxUser.DataPropertyName = "MaxUsers";
            this.colMaxUser.HeaderText = "Max User";
            this.colMaxUser.Name = "colMaxUser";
            // 
            // colMaxPc
            // 
            this.colMaxPc.DataPropertyName = "MaxPc";
            this.colMaxPc.HeaderText = "Max PC";
            this.colMaxPc.Name = "colMaxPc";
            // 
            // colServer
            // 
            this.colServer.DataPropertyName = "ServerName";
            this.colServer.HeaderText = "GetServer";
            this.colServer.Name = "colServer";
            // 
            // colExpiry
            // 
            this.colExpiry.DataPropertyName = "ExpDate";
            this.colExpiry.HeaderText = "Expiry";
            this.colExpiry.Name = "colExpiry";
            // 
            // colRemoveOutlet
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "Remove";
            dataGridViewCellStyle2.NullValue = "Remove";
            this.colRemoveOutlet.DefaultCellStyle = dataGridViewCellStyle2;
            this.colRemoveOutlet.HeaderText = "Remove";
            this.colRemoveOutlet.Name = "colRemoveOutlet";
            this.colRemoveOutlet.Text = "Remove";
            this.colRemoveOutlet.Width = 70;
            // 
            // LicenseGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1097, 754);
            this.Controls.Add(this.gridOutlets);
            this.Controls.Add(this.gridHardwareIds);
            this.Controls.Add(this.txtOriginGroupId);
            this.Controls.Add(this.txtLicenseTo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.nudVersion);
            this.Controls.Add(this.btnGenClientId);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.rdoMultiBranch);
            this.Controls.Add(this.rdoSingleBranch);
            this.Controls.Add(this.cbxEditions);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "LicenseGenerator";
            this.Text = "License Generator";
            this.Load += new System.EventHandler(this.LicenseGenerator_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVersion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsOutlets)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsHardwareIds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHardwareIds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridOutlets)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsOutletTypes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel llReadLicense;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnGenerateLicense;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewImageColumn Column2;
        private System.Windows.Forms.Button btnGenClientId;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton rdoMultiBranch;
        private System.Windows.Forms.RadioButton rdoSingleBranch;
        private System.Windows.Forms.ComboBox cbxEditions;
        private System.Windows.Forms.NumericUpDown nudVersion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLicenseTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.TextBox txtOriginGroupId;
        private System.Windows.Forms.BindingSource bsOutlets;
        private System.Windows.Forms.BindingSource bsHardwareIds;
        private System.Windows.Forms.DataGridView gridHardwareIds;
        private System.Windows.Forms.DataGridView gridOutlets;
        private System.Windows.Forms.BindingSource bsOutletTypes;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewButtonColumn colRemoveHwId;
        private System.Windows.Forms.DataGridViewComboBoxColumn colOutletType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOriginId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaxUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaxPc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExpiry;
        private System.Windows.Forms.DataGridViewButtonColumn colRemoveOutlet;
    }
}