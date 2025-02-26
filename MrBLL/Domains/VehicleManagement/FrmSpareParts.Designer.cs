using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.VehicleManagement
{
    partial class FrmSpareParts
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
            this.PanelHeader = new MrPanel();
            this.ChkActive = new System.Windows.Forms.CheckBox();
            this.clsSeparator2 = new ClsSeparator();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbProductInformation = new System.Windows.Forms.TabPage();
            this.BtnUOM = new System.Windows.Forms.Button();
            this.BtnRack = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.BtnDepartment = new System.Windows.Forms.Button();
            this.TxtMaxReOrderLevel = new MrTextBox();
            this.TxtRack = new MrTextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.BtnModule = new System.Windows.Forms.Button();
            this.BtnProductGroup = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.TxtReorderStock = new MrTextBox();
            this.TxtAltQty = new MrTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.TxtDepartment = new MrTextBox();
            this.TxtProductGroup = new MrTextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.TxtReorderLevel = new MrTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.TxtBarcode = new MrTextBox();
            this.TxtMRP = new MrTextBox();
            this.TxtSalesRate = new MrTextBox();
            this.TxtModule = new MrTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnDescription = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.BtnBarcode = new System.Windows.Forms.Button();
            this.BtnBarcode1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.BtnCategory = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtBarcode1 = new MrTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.CmbVat = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.TxtMargin = new MrTextBox();
            this.TxtBuyRate = new MrTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.TxtShortName = new MrTextBox();
            this.TxtDescription = new MrTextBox();
            this.TxtCategory = new MrTextBox();
            this.tbProductImage = new System.Windows.Forms.TabPage();
            this.lblProductPic = new System.Windows.Forms.Label();
            this.PbPicbox = new System.Windows.Forms.PictureBox();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.PanelHeader.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tbProductInformation.SuspendLayout();
            this.tbProductImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbPicbox)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.ChkActive);
            this.PanelHeader.Controls.Add(this.clsSeparator2);
            this.PanelHeader.Controls.Add(this.BtnNew);
            this.PanelHeader.Controls.Add(this.BtnEdit);
            this.PanelHeader.Controls.Add(this.BtnDelete);
            this.PanelHeader.Controls.Add(this.BtnExit);
            this.PanelHeader.Controls.Add(this.tabControl1);
            this.PanelHeader.Controls.Add(this.BtnSave);
            this.PanelHeader.Controls.Add(this.BtnCancel);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(713, 411);
            this.PanelHeader.TabIndex = 42;
            // 
            // ChkActive
            // 
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ChkActive.Location = new System.Drawing.Point(6, 377);
            this.ChkActive.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkActive.Size = new System.Drawing.Size(117, 22);
            this.ChkActive.TabIndex = 3;
            this.ChkActive.Text = "Active";
            this.ChkActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkActive.UseVisualStyleBackColor = true;
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator2.Location = new System.Drawing.Point(5, 40);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(705, 2);
            this.clsSeparator2.TabIndex = 50;
            this.clsSeparator2.TabStop = false;
            // 
            // BtnNew
            // 
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnNew.Location = new System.Drawing.Point(2, 5);
            this.BtnNew.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(75, 33);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            this.BtnNew.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyEvents_KeyPress);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEdit.Appearance.Options.UseFont = true;
            this.BtnEdit.ImageOptions.Image = global::MrBLL.Properties.Resources.Edit;
            this.BtnEdit.Location = new System.Drawing.Point(77, 4);
            this.BtnEdit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(82, 34);
            this.BtnEdit.TabIndex = 1;
            this.BtnEdit.Text = "&EDIT";
            this.BtnEdit.ToolTipTitle = "3";
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            this.BtnEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyEvents_KeyPress);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(159, 5);
            this.BtnDelete.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(103, 33);
            this.BtnDelete.TabIndex = 2;
            this.BtnDelete.Text = "&DELETE";
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            this.BtnDelete.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyEvents_KeyPress);
            // 
            // BtnExit
            // 
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.BtnExit.Location = new System.Drawing.Point(634, 5);
            this.BtnExit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(76, 33);
            this.BtnExit.TabIndex = 3;
            this.BtnExit.Text = "E&XIT";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            this.BtnExit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyEvents_KeyPress);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbProductInformation);
            this.tabControl1.Controls.Add(this.tbProductImage);
            this.tabControl1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.tabControl1.Location = new System.Drawing.Point(3, 42);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(707, 324);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyEvents_KeyPress);
            // 
            // tbProductInformation
            // 
            this.tbProductInformation.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tbProductInformation.Controls.Add(this.BtnUOM);
            this.tbProductInformation.Controls.Add(this.BtnRack);
            this.tbProductInformation.Controls.Add(this.label14);
            this.tbProductInformation.Controls.Add(this.label16);
            this.tbProductInformation.Controls.Add(this.BtnDepartment);
            this.tbProductInformation.Controls.Add(this.TxtMaxReOrderLevel);
            this.tbProductInformation.Controls.Add(this.TxtRack);
            this.tbProductInformation.Controls.Add(this.label17);
            this.tbProductInformation.Controls.Add(this.BtnModule);
            this.tbProductInformation.Controls.Add(this.BtnProductGroup);
            this.tbProductInformation.Controls.Add(this.label18);
            this.tbProductInformation.Controls.Add(this.TxtReorderStock);
            this.tbProductInformation.Controls.Add(this.TxtAltQty);
            this.tbProductInformation.Controls.Add(this.label12);
            this.tbProductInformation.Controls.Add(this.TxtDepartment);
            this.tbProductInformation.Controls.Add(this.TxtProductGroup);
            this.tbProductInformation.Controls.Add(this.label20);
            this.tbProductInformation.Controls.Add(this.label15);
            this.tbProductInformation.Controls.Add(this.label13);
            this.tbProductInformation.Controls.Add(this.TxtReorderLevel);
            this.tbProductInformation.Controls.Add(this.label11);
            this.tbProductInformation.Controls.Add(this.TxtBarcode);
            this.tbProductInformation.Controls.Add(this.TxtMRP);
            this.tbProductInformation.Controls.Add(this.TxtSalesRate);
            this.tbProductInformation.Controls.Add(this.TxtModule);
            this.tbProductInformation.Controls.Add(this.label1);
            this.tbProductInformation.Controls.Add(this.label2);
            this.tbProductInformation.Controls.Add(this.label3);
            this.tbProductInformation.Controls.Add(this.BtnDescription);
            this.tbProductInformation.Controls.Add(this.label4);
            this.tbProductInformation.Controls.Add(this.BtnBarcode);
            this.tbProductInformation.Controls.Add(this.BtnBarcode1);
            this.tbProductInformation.Controls.Add(this.label6);
            this.tbProductInformation.Controls.Add(this.BtnCategory);
            this.tbProductInformation.Controls.Add(this.label7);
            this.tbProductInformation.Controls.Add(this.TxtBarcode1);
            this.tbProductInformation.Controls.Add(this.label8);
            this.tbProductInformation.Controls.Add(this.CmbVat);
            this.tbProductInformation.Controls.Add(this.label9);
            this.tbProductInformation.Controls.Add(this.TxtMargin);
            this.tbProductInformation.Controls.Add(this.TxtBuyRate);
            this.tbProductInformation.Controls.Add(this.label10);
            this.tbProductInformation.Controls.Add(this.TxtShortName);
            this.tbProductInformation.Controls.Add(this.TxtDescription);
            this.tbProductInformation.Controls.Add(this.TxtCategory);
            this.tbProductInformation.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbProductInformation.Location = new System.Drawing.Point(4, 28);
            this.tbProductInformation.Name = "tbProductInformation";
            this.tbProductInformation.Padding = new System.Windows.Forms.Padding(3);
            this.tbProductInformation.Size = new System.Drawing.Size(699, 292);
            this.tbProductInformation.TabIndex = 0;
            this.tbProductInformation.Text = "INFORMATION";
            // 
            // BtnUOM
            // 
            this.BtnUOM.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnUOM.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnUOM.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnUOM.Location = new System.Drawing.Point(670, 85);
            this.BtnUOM.Margin = new System.Windows.Forms.Padding(4);
            this.BtnUOM.Name = "BtnUOM";
            this.BtnUOM.Size = new System.Drawing.Size(25, 25);
            this.BtnUOM.TabIndex = 530;
            this.BtnUOM.TabStop = false;
            this.BtnUOM.UseVisualStyleBackColor = false;
            // 
            // BtnRack
            // 
            this.BtnRack.CausesValidation = false;
            this.BtnRack.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRack.ForeColor = System.Drawing.Color.Transparent;
            this.BtnRack.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnRack.Location = new System.Drawing.Point(314, 144);
            this.BtnRack.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnRack.Name = "BtnRack";
            this.BtnRack.Size = new System.Drawing.Size(29, 26);
            this.BtnRack.TabIndex = 512;
            this.BtnRack.TabStop = false;
            this.BtnRack.UseVisualStyleBackColor = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(346, 148);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 19);
            this.label14.TabIndex = 528;
            this.label14.Text = "Model";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(346, 232);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(147, 19);
            this.label16.TabIndex = 521;
            this.label16.Text = "MaxReOrder Level";
            // 
            // BtnDepartment
            // 
            this.BtnDepartment.CausesValidation = false;
            this.BtnDepartment.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDepartment.ForeColor = System.Drawing.Color.Transparent;
            this.BtnDepartment.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDepartment.Location = new System.Drawing.Point(670, 117);
            this.BtnDepartment.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnDepartment.Name = "BtnDepartment";
            this.BtnDepartment.Size = new System.Drawing.Size(29, 26);
            this.BtnDepartment.TabIndex = 513;
            this.BtnDepartment.TabStop = false;
            this.BtnDepartment.UseVisualStyleBackColor = false;
            // 
            // TxtMaxReOrderLevel
            // 
            this.TxtMaxReOrderLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMaxReOrderLevel.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtMaxReOrderLevel.Location = new System.Drawing.Point(493, 228);
            this.TxtMaxReOrderLevel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtMaxReOrderLevel.Name = "TxtMaxReOrderLevel";
            this.TxtMaxReOrderLevel.Size = new System.Drawing.Size(176, 25);
            this.TxtMaxReOrderLevel.TabIndex = 520;
            this.TxtMaxReOrderLevel.Text = "0.00";
            this.TxtMaxReOrderLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtRack
            // 
            this.TxtRack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtRack.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtRack.Location = new System.Drawing.Point(128, 144);
            this.TxtRack.Name = "TxtRack";
            this.TxtRack.Size = new System.Drawing.Size(185, 25);
            this.TxtRack.TabIndex = 5;
            this.TxtRack.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtRack_KeyDown);
            this.TxtRack.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyEvents_KeyPress);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(4, 147);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(46, 19);
            this.label17.TabIndex = 61;
            this.label17.Text = "Rack";
            // 
            // BtnModule
            // 
            this.BtnModule.CausesValidation = false;
            this.BtnModule.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnModule.ForeColor = System.Drawing.Color.Transparent;
            this.BtnModule.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnModule.Location = new System.Drawing.Point(670, 144);
            this.BtnModule.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnModule.Name = "BtnModule";
            this.BtnModule.Size = new System.Drawing.Size(28, 26);
            this.BtnModule.TabIndex = 529;
            this.BtnModule.TabStop = false;
            this.BtnModule.UseVisualStyleBackColor = false;
            // 
            // BtnProductGroup
            // 
            this.BtnProductGroup.CausesValidation = false;
            this.BtnProductGroup.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnProductGroup.ForeColor = System.Drawing.Color.Transparent;
            this.BtnProductGroup.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnProductGroup.Location = new System.Drawing.Point(670, 57);
            this.BtnProductGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnProductGroup.Name = "BtnProductGroup";
            this.BtnProductGroup.Size = new System.Drawing.Size(29, 26);
            this.BtnProductGroup.TabIndex = 519;
            this.BtnProductGroup.TabStop = false;
            this.BtnProductGroup.UseVisualStyleBackColor = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(346, 91);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(46, 19);
            this.label18.TabIndex = 63;
            this.label18.Text = "UOM";
            // 
            // TxtReorderStock
            // 
            this.TxtReorderStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtReorderStock.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtReorderStock.Location = new System.Drawing.Point(493, 257);
            this.TxtReorderStock.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtReorderStock.Name = "TxtReorderStock";
            this.TxtReorderStock.Size = new System.Drawing.Size(176, 25);
            this.TxtReorderStock.TabIndex = 19;
            this.TxtReorderStock.Text = "0.00";
            this.TxtReorderStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtReorderStock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyEvents_KeyPress);
            // 
            // TxtAltQty
            // 
            this.TxtAltQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAltQty.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAltQty.Location = new System.Drawing.Point(471, 88);
            this.TxtAltQty.Name = "TxtAltQty";
            this.TxtAltQty.Size = new System.Drawing.Size(196, 25);
            this.TxtAltQty.TabIndex = 7;
            this.TxtAltQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyEvents_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(346, 261);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(119, 19);
            this.label12.TabIndex = 53;
            this.label12.Text = "ReOrder Stock";
            // 
            // TxtDepartment
            // 
            this.TxtDepartment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDepartment.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDepartment.Location = new System.Drawing.Point(471, 118);
            this.TxtDepartment.Name = "TxtDepartment";
            this.TxtDepartment.Size = new System.Drawing.Size(196, 25);
            this.TxtDepartment.TabIndex = 3;
            this.TxtDepartment.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyEvents_KeyPress);
            // 
            // TxtProductGroup
            // 
            this.TxtProductGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtProductGroup.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtProductGroup.Location = new System.Drawing.Point(128, 61);
            this.TxtProductGroup.Name = "TxtProductGroup";
            this.TxtProductGroup.Size = new System.Drawing.Size(539, 25);
            this.TxtProductGroup.TabIndex = 6;
            this.TxtProductGroup.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyEvents_KeyPress);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(4, 64);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(75, 19);
            this.label20.TabIndex = 518;
            this.label20.Text = "Category";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(346, 121);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 19);
            this.label15.TabIndex = 57;
            this.label15.Text = "Company";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(6, 267);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(117, 19);
            this.label13.TabIndex = 52;
            this.label13.Text = "ReOrder Level";
            // 
            // TxtReorderLevel
            // 
            this.TxtReorderLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtReorderLevel.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtReorderLevel.Location = new System.Drawing.Point(128, 260);
            this.TxtReorderLevel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtReorderLevel.Name = "TxtReorderLevel";
            this.TxtReorderLevel.Size = new System.Drawing.Size(185, 25);
            this.TxtReorderLevel.TabIndex = 18;
            this.TxtReorderLevel.Text = "0.00";
            this.TxtReorderLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtReorderLevel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyEvents_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(6, 238);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 19);
            this.label11.TabIndex = 49;
            this.label11.Text = "MRP Rate";
            // 
            // TxtBarcode
            // 
            this.TxtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBarcode.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBarcode.Location = new System.Drawing.Point(128, 4);
            this.TxtBarcode.Name = "TxtBarcode";
            this.TxtBarcode.Size = new System.Drawing.Size(185, 25);
            this.TxtBarcode.TabIndex = 0;
            this.TxtBarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyEvents_KeyPress);
            // 
            // TxtMRP
            // 
            this.TxtMRP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMRP.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtMRP.Location = new System.Drawing.Point(128, 231);
            this.TxtMRP.Name = "TxtMRP";
            this.TxtMRP.Size = new System.Drawing.Size(185, 25);
            this.TxtMRP.TabIndex = 16;
            this.TxtMRP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtMRP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyEvents_KeyPress);
            // 
            // TxtSalesRate
            // 
            this.TxtSalesRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSalesRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSalesRate.Location = new System.Drawing.Point(493, 201);
            this.TxtSalesRate.Name = "TxtSalesRate";
            this.TxtSalesRate.Size = new System.Drawing.Size(176, 25);
            this.TxtSalesRate.TabIndex = 15;
            this.TxtSalesRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtSalesRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyEvents_KeyPress);
            // 
            // TxtModule
            // 
            this.TxtModule.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtModule.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtModule.Location = new System.Drawing.Point(471, 145);
            this.TxtModule.Name = "TxtModule";
            this.TxtModule.Size = new System.Drawing.Size(196, 25);
            this.TxtModule.TabIndex = 527;
            this.TxtModule.Leave += new System.EventHandler(this.TxtModule_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 19);
            this.label1.TabIndex = 10;
            this.label1.Text = "BarCode";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 19);
            this.label2.TabIndex = 11;
            this.label2.Text = "Description";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 19);
            this.label3.TabIndex = 12;
            this.label3.Text = "Category";
            // 
            // BtnDescription
            // 
            this.BtnDescription.CausesValidation = false;
            this.BtnDescription.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDescription.ForeColor = System.Drawing.Color.Transparent;
            this.BtnDescription.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDescription.Location = new System.Drawing.Point(670, 32);
            this.BtnDescription.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnDescription.Name = "BtnDescription";
            this.BtnDescription.Size = new System.Drawing.Size(29, 26);
            this.BtnDescription.TabIndex = 44;
            this.BtnDescription.TabStop = false;
            this.BtnDescription.UseVisualStyleBackColor = false;
            this.BtnDescription.Click += new System.EventHandler(this.BtnDescription_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 19);
            this.label4.TabIndex = 13;
            this.label4.Text = "ShortName";
            // 
            // BtnBarcode
            // 
            this.BtnBarcode.CausesValidation = false;
            this.BtnBarcode.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBarcode.ForeColor = System.Drawing.Color.Transparent;
            this.BtnBarcode.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnBarcode.Location = new System.Drawing.Point(314, 4);
            this.BtnBarcode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnBarcode.Name = "BtnBarcode";
            this.BtnBarcode.Size = new System.Drawing.Size(29, 26);
            this.BtnBarcode.TabIndex = 43;
            this.BtnBarcode.TabStop = false;
            this.BtnBarcode.UseVisualStyleBackColor = false;
            // 
            // BtnBarcode1
            // 
            this.BtnBarcode1.CausesValidation = false;
            this.BtnBarcode1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBarcode1.ForeColor = System.Drawing.Color.Transparent;
            this.BtnBarcode1.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnBarcode1.Location = new System.Drawing.Point(670, 4);
            this.BtnBarcode1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnBarcode1.Name = "BtnBarcode1";
            this.BtnBarcode1.Size = new System.Drawing.Size(29, 26);
            this.BtnBarcode1.TabIndex = 42;
            this.BtnBarcode1.TabStop = false;
            this.BtnBarcode1.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 180);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 19);
            this.label6.TabIndex = 15;
            this.label6.Text = "VAT";
            // 
            // BtnCategory
            // 
            this.BtnCategory.CausesValidation = false;
            this.BtnCategory.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCategory.ForeColor = System.Drawing.Color.Transparent;
            this.BtnCategory.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnCategory.Location = new System.Drawing.Point(314, 116);
            this.BtnCategory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnCategory.Name = "BtnCategory";
            this.BtnCategory.Size = new System.Drawing.Size(29, 26);
            this.BtnCategory.TabIndex = 41;
            this.BtnCategory.TabStop = false;
            this.BtnCategory.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(346, 174);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 19);
            this.label7.TabIndex = 16;
            this.label7.Text = "Buy Rate";
            // 
            // TxtBarcode1
            // 
            this.TxtBarcode1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBarcode1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBarcode1.Location = new System.Drawing.Point(491, 4);
            this.TxtBarcode1.Name = "TxtBarcode1";
            this.TxtBarcode1.Size = new System.Drawing.Size(176, 25);
            this.TxtBarcode1.TabIndex = 1;
            this.TxtBarcode1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyEvents_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 209);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 19);
            this.label8.TabIndex = 17;
            this.label8.Text = "Margin";
            // 
            // CmbVat
            // 
            this.CmbVat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbVat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbVat.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbVat.FormattingEnabled = true;
            this.CmbVat.Items.AddRange(new object[] {
            "YES",
            "NO"});
            this.CmbVat.Location = new System.Drawing.Point(128, 172);
            this.CmbVat.Name = "CmbVat";
            this.CmbVat.Size = new System.Drawing.Size(185, 26);
            this.CmbVat.TabIndex = 12;
            this.CmbVat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyEvents_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(346, 201);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 19);
            this.label9.TabIndex = 18;
            this.label9.Text = "Sales Rate";
            // 
            // TxtMargin
            // 
            this.TxtMargin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMargin.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtMargin.Location = new System.Drawing.Point(128, 202);
            this.TxtMargin.Name = "TxtMargin";
            this.TxtMargin.Size = new System.Drawing.Size(185, 25);
            this.TxtMargin.TabIndex = 14;
            this.TxtMargin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtMargin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyEvents_KeyPress);
            // 
            // TxtBuyRate
            // 
            this.TxtBuyRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBuyRate.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBuyRate.Location = new System.Drawing.Point(493, 174);
            this.TxtBuyRate.Name = "TxtBuyRate";
            this.TxtBuyRate.Size = new System.Drawing.Size(176, 25);
            this.TxtBuyRate.TabIndex = 13;
            this.TxtBuyRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtBuyRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyEvents_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(401, 8);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 19);
            this.label10.TabIndex = 22;
            this.label10.Text = "BarCode1";
            // 
            // TxtShortName
            // 
            this.TxtShortName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtShortName.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtShortName.Location = new System.Drawing.Point(128, 89);
            this.TxtShortName.Name = "TxtShortName";
            this.TxtShortName.Size = new System.Drawing.Size(185, 25);
            this.TxtShortName.TabIndex = 10;
            this.TxtShortName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyEvents_KeyPress);
            this.TxtShortName.Leave += new System.EventHandler(this.TxtShortName_Leave);
            this.TxtShortName.Validating += new System.ComponentModel.CancelEventHandler(this.TxtShortName_Validating);
            // 
            // TxtDescription
            // 
            this.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDescription.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDescription.Location = new System.Drawing.Point(128, 33);
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(539, 25);
            this.TxtDescription.TabIndex = 2;
            this.TxtDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyEvents_KeyPress);
            this.TxtDescription.Leave += new System.EventHandler(this.TxtDescription_Leave);
            this.TxtDescription.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDescription_Validating);
            // 
            // TxtCategory
            // 
            this.TxtCategory.BackColor = System.Drawing.SystemColors.Window;
            this.TxtCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCategory.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCategory.Location = new System.Drawing.Point(128, 117);
            this.TxtCategory.Name = "TxtCategory";
            this.TxtCategory.ReadOnly = true;
            this.TxtCategory.Size = new System.Drawing.Size(185, 25);
            this.TxtCategory.TabIndex = 9;
            this.TxtCategory.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyEvents_KeyPress);
            this.TxtCategory.Leave += new System.EventHandler(this.TxtCategory_Leave);
            // 
            // tbProductImage
            // 
            this.tbProductImage.Controls.Add(this.lblProductPic);
            this.tbProductImage.Controls.Add(this.PbPicbox);
            this.tbProductImage.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.tbProductImage.Location = new System.Drawing.Point(4, 28);
            this.tbProductImage.Name = "tbProductImage";
            this.tbProductImage.Padding = new System.Windows.Forms.Padding(3);
            this.tbProductImage.Size = new System.Drawing.Size(699, 292);
            this.tbProductImage.TabIndex = 1;
            this.tbProductImage.Text = "IMAGE";
            this.tbProductImage.UseVisualStyleBackColor = true;
            // 
            // lblProductPic
            // 
            this.lblProductPic.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblProductPic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblProductPic.Enabled = false;
            this.lblProductPic.Font = new System.Drawing.Font("Arial", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductPic.Location = new System.Drawing.Point(186, 68);
            this.lblProductPic.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProductPic.Name = "lblProductPic";
            this.lblProductPic.Size = new System.Drawing.Size(367, 189);
            this.lblProductPic.TabIndex = 38;
            this.lblProductPic.Text = "Double Click For Picture Upload";
            this.lblProductPic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblProductPic.DoubleClick += new System.EventHandler(this.lblProductPic_DoubleClick);
            // 
            // PbPicbox
            // 
            this.PbPicbox.BackColor = System.Drawing.SystemColors.Control;
            this.PbPicbox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PbPicbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PbPicbox.Enabled = false;
            this.PbPicbox.Image = global::MrBLL.Properties.Resources.noimage;
            this.PbPicbox.Location = new System.Drawing.Point(3, 3);
            this.PbPicbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PbPicbox.Name = "PbPicbox";
            this.PbPicbox.Size = new System.Drawing.Size(693, 286);
            this.PbPicbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PbPicbox.TabIndex = 37;
            this.PbPicbox.TabStop = false;
            this.PbPicbox.DoubleClick += new System.EventHandler(this.PbPicbox_DoubleClick_1);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(500, 370);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(103, 37);
            this.BtnSave.TabIndex = 5;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            this.BtnSave.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyEvents_KeyPress);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnCancel.Location = new System.Drawing.Point(609, 370);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(103, 37);
            this.BtnCancel.TabIndex = 6;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            this.BtnCancel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyEvents_KeyPress);
            // 
            // FrmSpareParts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 411);
            this.Controls.Add(this.PanelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmSpareParts";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "SPARE PARTS SETUP";
            this.Load += new System.EventHandler(this.FrmSpareParts_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmSpareParts_KeyPress);
            this.PanelHeader.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tbProductInformation.ResumeLayout(false);
            this.tbProductInformation.PerformLayout();
            this.tbProductImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PbPicbox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbProductImage;
        private System.Windows.Forms.Label lblProductPic;
        private System.Windows.Forms.PictureBox PbPicbox;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnExit;
        private ClsSeparator clsSeparator2;
        private System.Windows.Forms.TabPage tbProductInformation;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button BtnProductGroup;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button BtnDepartment;
        private System.Windows.Forms.Button BtnRack;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnDescription;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BtnBarcode;
        private System.Windows.Forms.Button BtnBarcode1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button BtnCategory;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox CmbVat;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox ChkActive;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button BtnModule;
        private System.Windows.Forms.Button BtnUOM;
        private MrPanel PanelHeader;
        private MrTextBox TxtMaxReOrderLevel;
        private MrTextBox TxtProductGroup;
        private MrTextBox TxtAltQty;
        private MrTextBox TxtRack;
        private MrTextBox TxtDepartment;
        private MrTextBox TxtReorderStock;
        private MrTextBox TxtReorderLevel;
        private MrTextBox TxtBarcode;
        private MrTextBox TxtMRP;
        private MrTextBox TxtSalesRate;
        private MrTextBox TxtBarcode1;
        private MrTextBox TxtMargin;
        private MrTextBox TxtBuyRate;
        private MrTextBox TxtShortName;
        private MrTextBox TxtDescription;
        private MrTextBox TxtCategory;
        private MrTextBox TxtModule;
    }
}