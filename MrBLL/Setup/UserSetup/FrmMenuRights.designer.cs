using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Setup.UserSetup
{
    partial class FrmMenuRights
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.GMenuRights = new System.Windows.Forms.DataGridView();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.txt_GId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_GMenu_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_GMenu_Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_GMenu_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_GForm_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_GSubModule_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_GNew = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.txt_GEdit = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.txt_GDelete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.txt_GSave = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.txt_GCopy = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.txt_GSearch = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.txt_GPrint = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.txt_GApproved = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.txt_GReverse = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.txt_GIsParent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_UserType = new System.Windows.Forms.Label();
            this.cb_UserType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_Module = new System.Windows.Forms.ComboBox();
            this.lbl_MenuModule = new System.Windows.Forms.Label();
            this.cb_MenuModule = new System.Windows.Forms.ComboBox();
            this.cb_User = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_SelectAllNew = new System.Windows.Forms.CheckBox();
            this.cb_SelectAllEdit = new System.Windows.Forms.CheckBox();
            this.cb_SelectAllDelete = new System.Windows.Forms.CheckBox();
            this.cb_SelectAllSave = new System.Windows.Forms.CheckBox();
            this.cb_SelectAllCopy = new System.Windows.Forms.CheckBox();
            this.cb_SelectAllMenu = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.GMenuRights)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Delete
            // 
            this.btn_Delete.Enabled = false;
            this.btn_Delete.Font = new System.Drawing.Font("Cambria", 11F);
            this.btn_Delete.Location = new System.Drawing.Point(1104, 10);
            this.btn_Delete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(100, 41);
            this.btn_Delete.TabIndex = 47;
            this.btn_Delete.Text = "&Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Font = new System.Drawing.Font("Cambria", 11F);
            this.btn_Save.Location = new System.Drawing.Point(1005, 10);
            this.btn_Save.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(100, 41);
            this.btn_Save.TabIndex = 46;
            this.btn_Save.Text = "&Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // GMenuRights
            // 
            this.GMenuRights.AllowUserToAddRows = false;
            this.GMenuRights.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.GMenuRights.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GMenuRights.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check,
            this.txt_GId,
            this.txt_GMenu_Id,
            this.txt_GMenu_Code,
            this.txt_GMenu_Name,
            this.txt_GForm_Name,
            this.txt_GSubModule_Id,
            this.txt_GNew,
            this.txt_GEdit,
            this.txt_GDelete,
            this.txt_GSave,
            this.txt_GCopy,
            this.txt_GSearch,
            this.txt_GPrint,
            this.txt_GApproved,
            this.txt_GReverse,
            this.txt_GIsParent});
            this.GMenuRights.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GMenuRights.Location = new System.Drawing.Point(0, 0);
            this.GMenuRights.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GMenuRights.MultiSelect = false;
            this.GMenuRights.Name = "GMenuRights";
            this.GMenuRights.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.GMenuRights.RowHeadersVisible = false;
            this.GMenuRights.RowHeadersWidth = 51;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.GMenuRights.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.GMenuRights.RowTemplate.Height = 24;
            this.GMenuRights.Size = new System.Drawing.Size(1404, 454);
            this.GMenuRights.StandardTab = true;
            this.GMenuRights.TabIndex = 2;
            this.GMenuRights.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_MenuRights_CellContentClick);
            this.GMenuRights.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_MenuRights_CellContentDoubleClick);
            // 
            // Check
            // 
            this.Check.FalseValue = "0";
            this.Check.HeaderText = "Check";
            this.Check.MinimumWidth = 6;
            this.Check.Name = "Check";
            this.Check.TrueValue = "1";
            this.Check.Width = 65;
            // 
            // txt_GId
            // 
            this.txt_GId.HeaderText = "Id";
            this.txt_GId.MinimumWidth = 6;
            this.txt_GId.Name = "txt_GId";
            this.txt_GId.ReadOnly = true;
            this.txt_GId.Visible = false;
            this.txt_GId.Width = 125;
            // 
            // txt_GMenu_Id
            // 
            this.txt_GMenu_Id.FillWeight = 130F;
            this.txt_GMenu_Id.HeaderText = "Menu Id";
            this.txt_GMenu_Id.MaxInputLength = 256;
            this.txt_GMenu_Id.MinimumWidth = 6;
            this.txt_GMenu_Id.Name = "txt_GMenu_Id";
            this.txt_GMenu_Id.ReadOnly = true;
            this.txt_GMenu_Id.Visible = false;
            this.txt_GMenu_Id.Width = 320;
            // 
            // txt_GMenu_Code
            // 
            this.txt_GMenu_Code.HeaderText = "Menu_Code";
            this.txt_GMenu_Code.MinimumWidth = 6;
            this.txt_GMenu_Code.Name = "txt_GMenu_Code";
            this.txt_GMenu_Code.Visible = false;
            this.txt_GMenu_Code.Width = 125;
            // 
            // txt_GMenu_Name
            // 
            this.txt_GMenu_Name.HeaderText = "Menu Name";
            this.txt_GMenu_Name.MinimumWidth = 6;
            this.txt_GMenu_Name.Name = "txt_GMenu_Name";
            this.txt_GMenu_Name.ReadOnly = true;
            this.txt_GMenu_Name.Width = 200;
            // 
            // txt_GForm_Name
            // 
            this.txt_GForm_Name.HeaderText = "Form_Name";
            this.txt_GForm_Name.MinimumWidth = 6;
            this.txt_GForm_Name.Name = "txt_GForm_Name";
            this.txt_GForm_Name.Visible = false;
            this.txt_GForm_Name.Width = 125;
            // 
            // txt_GSubModule_Id
            // 
            this.txt_GSubModule_Id.HeaderText = "SubModule_Id";
            this.txt_GSubModule_Id.MinimumWidth = 6;
            this.txt_GSubModule_Id.Name = "txt_GSubModule_Id";
            this.txt_GSubModule_Id.Visible = false;
            this.txt_GSubModule_Id.Width = 51;
            // 
            // txt_GNew
            // 
            this.txt_GNew.FalseValue = "0";
            this.txt_GNew.HeaderText = "New";
            this.txt_GNew.MinimumWidth = 6;
            this.txt_GNew.Name = "txt_GNew";
            this.txt_GNew.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.txt_GNew.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.txt_GNew.TrueValue = "1";
            this.txt_GNew.Width = 85;
            // 
            // txt_GEdit
            // 
            this.txt_GEdit.FalseValue = "0";
            this.txt_GEdit.HeaderText = "Edit";
            this.txt_GEdit.MinimumWidth = 6;
            this.txt_GEdit.Name = "txt_GEdit";
            this.txt_GEdit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.txt_GEdit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.txt_GEdit.TrueValue = "1";
            this.txt_GEdit.Width = 85;
            // 
            // txt_GDelete
            // 
            this.txt_GDelete.FalseValue = "0";
            this.txt_GDelete.HeaderText = "Delete";
            this.txt_GDelete.MinimumWidth = 6;
            this.txt_GDelete.Name = "txt_GDelete";
            this.txt_GDelete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.txt_GDelete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.txt_GDelete.TrueValue = "1";
            this.txt_GDelete.Width = 85;
            // 
            // txt_GSave
            // 
            this.txt_GSave.FalseValue = "0";
            this.txt_GSave.HeaderText = "Save";
            this.txt_GSave.MinimumWidth = 6;
            this.txt_GSave.Name = "txt_GSave";
            this.txt_GSave.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.txt_GSave.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.txt_GSave.TrueValue = "1";
            this.txt_GSave.Width = 85;
            // 
            // txt_GCopy
            // 
            this.txt_GCopy.HeaderText = "Copy";
            this.txt_GCopy.MinimumWidth = 6;
            this.txt_GCopy.Name = "txt_GCopy";
            this.txt_GCopy.Width = 85;
            // 
            // txt_GSearch
            // 
            this.txt_GSearch.FalseValue = "0";
            this.txt_GSearch.HeaderText = "View";
            this.txt_GSearch.MinimumWidth = 6;
            this.txt_GSearch.Name = "txt_GSearch";
            this.txt_GSearch.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.txt_GSearch.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.txt_GSearch.TrueValue = "1";
            this.txt_GSearch.Width = 85;
            // 
            // txt_GPrint
            // 
            this.txt_GPrint.FalseValue = "0";
            this.txt_GPrint.HeaderText = "Print";
            this.txt_GPrint.MinimumWidth = 6;
            this.txt_GPrint.Name = "txt_GPrint";
            this.txt_GPrint.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.txt_GPrint.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.txt_GPrint.TrueValue = "1";
            this.txt_GPrint.Width = 85;
            // 
            // txt_GApproved
            // 
            this.txt_GApproved.HeaderText = "Approved";
            this.txt_GApproved.MinimumWidth = 6;
            this.txt_GApproved.Name = "txt_GApproved";
            this.txt_GApproved.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.txt_GApproved.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.txt_GApproved.Visible = false;
            this.txt_GApproved.Width = 65;
            // 
            // txt_GReverse
            // 
            this.txt_GReverse.FalseValue = "0";
            this.txt_GReverse.HeaderText = "Reverse";
            this.txt_GReverse.MinimumWidth = 6;
            this.txt_GReverse.Name = "txt_GReverse";
            this.txt_GReverse.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.txt_GReverse.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.txt_GReverse.TrueValue = "1";
            this.txt_GReverse.Visible = false;
            this.txt_GReverse.Width = 60;
            // 
            // txt_GIsParent
            // 
            this.txt_GIsParent.HeaderText = "Is Parent";
            this.txt_GIsParent.MinimumWidth = 6;
            this.txt_GIsParent.Name = "txt_GIsParent";
            this.txt_GIsParent.Visible = false;
            this.txt_GIsParent.Width = 125;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.GMenuRights);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1404, 454);
            this.panel1.TabIndex = 50;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_UserType);
            this.groupBox1.Controls.Add(this.cb_UserType);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cb_Module);
            this.groupBox1.Controls.Add(this.lbl_MenuModule);
            this.groupBox1.Controls.Add(this.cb_MenuModule);
            this.groupBox1.Controls.Add(this.cb_User);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(1404, 54);
            this.groupBox1.TabIndex = 56;
            this.groupBox1.TabStop = false;
            // 
            // lbl_UserType
            // 
            this.lbl_UserType.AutoSize = true;
            this.lbl_UserType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_UserType.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_UserType.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_UserType.Location = new System.Drawing.Point(161, 17);
            this.lbl_UserType.Name = "lbl_UserType";
            this.lbl_UserType.Size = new System.Drawing.Size(87, 22);
            this.lbl_UserType.TabIndex = 49;
            this.lbl_UserType.Text = "User Role";
            // 
            // cb_UserType
            // 
            this.cb_UserType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_UserType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_UserType.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_UserType.FormattingEnabled = true;
            this.cb_UserType.Location = new System.Drawing.Point(252, 11);
            this.cb_UserType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_UserType.Name = "cb_UserType";
            this.cb_UserType.Size = new System.Drawing.Size(260, 29);
            this.cb_UserType.TabIndex = 45;
            this.cb_UserType.SelectedIndexChanged += new System.EventHandler(this.cb_UserType_SelectedIndexChanged);
            this.cb_UserType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cb_UserType_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(513, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 22);
            this.label1.TabIndex = 53;
            this.label1.Text = "Module";
            // 
            // cb_Module
            // 
            this.cb_Module.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Module.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_Module.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Module.FormattingEnabled = true;
            this.cb_Module.Location = new System.Drawing.Point(587, 12);
            this.cb_Module.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_Module.Name = "cb_Module";
            this.cb_Module.Size = new System.Drawing.Size(221, 29);
            this.cb_Module.TabIndex = 52;
            this.cb_Module.SelectionChangeCommitted += new System.EventHandler(this.cb_Module_SelectionChangeCommitted);
            // 
            // lbl_MenuModule
            // 
            this.lbl_MenuModule.AutoSize = true;
            this.lbl_MenuModule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_MenuModule.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MenuModule.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_MenuModule.Location = new System.Drawing.Point(809, 17);
            this.lbl_MenuModule.Name = "lbl_MenuModule";
            this.lbl_MenuModule.Size = new System.Drawing.Size(123, 22);
            this.lbl_MenuModule.TabIndex = 51;
            this.lbl_MenuModule.Text = "Menu Module";
            // 
            // cb_MenuModule
            // 
            this.cb_MenuModule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_MenuModule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_MenuModule.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_MenuModule.FormattingEnabled = true;
            this.cb_MenuModule.Location = new System.Drawing.Point(933, 12);
            this.cb_MenuModule.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_MenuModule.Name = "cb_MenuModule";
            this.cb_MenuModule.Size = new System.Drawing.Size(308, 29);
            this.cb_MenuModule.TabIndex = 48;
            this.cb_MenuModule.SelectedIndexChanged += new System.EventHandler(this.cb_MenuModule_SelectedIndexChanged);
            this.cb_MenuModule.SelectionChangeCommitted += new System.EventHandler(this.cb_MenuModule_SelectionChangeCommitted);
            this.cb_MenuModule.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cb_MenuModule_KeyDown);
            // 
            // cb_User
            // 
            this.cb_User.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_User.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_User.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_User.FormattingEnabled = true;
            this.cb_User.Location = new System.Drawing.Point(64, 12);
            this.cb_User.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_User.Name = "cb_User";
            this.cb_User.Size = new System.Drawing.Size(96, 29);
            this.cb_User.TabIndex = 54;
            this.cb_User.Visible = false;
            this.cb_User.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cb_User_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(15, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 22);
            this.label2.TabIndex = 55;
            this.label2.Text = "User";
            this.label2.Visible = false;
            // 
            // cb_SelectAllNew
            // 
            this.cb_SelectAllNew.Enabled = false;
            this.cb_SelectAllNew.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cb_SelectAllNew.Font = new System.Drawing.Font("Cambria", 11F);
            this.cb_SelectAllNew.Location = new System.Drawing.Point(171, 14);
            this.cb_SelectAllNew.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_SelectAllNew.Name = "cb_SelectAllNew";
            this.cb_SelectAllNew.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cb_SelectAllNew.Size = new System.Drawing.Size(153, 27);
            this.cb_SelectAllNew.TabIndex = 57;
            this.cb_SelectAllNew.Text = "Select All New";
            this.cb_SelectAllNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cb_SelectAllNew.UseVisualStyleBackColor = true;
            this.cb_SelectAllNew.CheckedChanged += new System.EventHandler(this.cb_SelectAllNew_CheckedChanged);
            // 
            // cb_SelectAllEdit
            // 
            this.cb_SelectAllEdit.Enabled = false;
            this.cb_SelectAllEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cb_SelectAllEdit.Font = new System.Drawing.Font("Cambria", 11F);
            this.cb_SelectAllEdit.Location = new System.Drawing.Point(333, 14);
            this.cb_SelectAllEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_SelectAllEdit.Name = "cb_SelectAllEdit";
            this.cb_SelectAllEdit.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cb_SelectAllEdit.Size = new System.Drawing.Size(143, 27);
            this.cb_SelectAllEdit.TabIndex = 58;
            this.cb_SelectAllEdit.Text = "Select All Edit";
            this.cb_SelectAllEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cb_SelectAllEdit.UseVisualStyleBackColor = true;
            this.cb_SelectAllEdit.CheckedChanged += new System.EventHandler(this.cb_SelectAllEdit_CheckedChanged);
            // 
            // cb_SelectAllDelete
            // 
            this.cb_SelectAllDelete.Enabled = false;
            this.cb_SelectAllDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cb_SelectAllDelete.Font = new System.Drawing.Font("Cambria", 11F);
            this.cb_SelectAllDelete.Location = new System.Drawing.Point(485, 14);
            this.cb_SelectAllDelete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_SelectAllDelete.Name = "cb_SelectAllDelete";
            this.cb_SelectAllDelete.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cb_SelectAllDelete.Size = new System.Drawing.Size(163, 27);
            this.cb_SelectAllDelete.TabIndex = 59;
            this.cb_SelectAllDelete.Text = "Select All Delete";
            this.cb_SelectAllDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cb_SelectAllDelete.UseVisualStyleBackColor = true;
            this.cb_SelectAllDelete.CheckedChanged += new System.EventHandler(this.cb_SelectAllDelete_CheckedChanged);
            // 
            // cb_SelectAllSave
            // 
            this.cb_SelectAllSave.Enabled = false;
            this.cb_SelectAllSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cb_SelectAllSave.Font = new System.Drawing.Font("Cambria", 11F);
            this.cb_SelectAllSave.Location = new System.Drawing.Point(657, 14);
            this.cb_SelectAllSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_SelectAllSave.Name = "cb_SelectAllSave";
            this.cb_SelectAllSave.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cb_SelectAllSave.Size = new System.Drawing.Size(161, 27);
            this.cb_SelectAllSave.TabIndex = 60;
            this.cb_SelectAllSave.Text = "Select All Save";
            this.cb_SelectAllSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cb_SelectAllSave.UseVisualStyleBackColor = true;
            this.cb_SelectAllSave.CheckedChanged += new System.EventHandler(this.cb_SelectAllSave_CheckedChanged);
            // 
            // cb_SelectAllCopy
            // 
            this.cb_SelectAllCopy.Enabled = false;
            this.cb_SelectAllCopy.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cb_SelectAllCopy.Font = new System.Drawing.Font("Cambria", 11F);
            this.cb_SelectAllCopy.Location = new System.Drawing.Point(828, 14);
            this.cb_SelectAllCopy.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_SelectAllCopy.Name = "cb_SelectAllCopy";
            this.cb_SelectAllCopy.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cb_SelectAllCopy.Size = new System.Drawing.Size(163, 27);
            this.cb_SelectAllCopy.TabIndex = 61;
            this.cb_SelectAllCopy.Text = "Select All Copy";
            this.cb_SelectAllCopy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cb_SelectAllCopy.UseVisualStyleBackColor = true;
            this.cb_SelectAllCopy.CheckedChanged += new System.EventHandler(this.cb_SelectAllCopy_CheckedChanged);
            // 
            // cb_SelectAllMenu
            // 
            this.cb_SelectAllMenu.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cb_SelectAllMenu.Font = new System.Drawing.Font("Cambria", 11F);
            this.cb_SelectAllMenu.Location = new System.Drawing.Point(3, 14);
            this.cb_SelectAllMenu.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_SelectAllMenu.Name = "cb_SelectAllMenu";
            this.cb_SelectAllMenu.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cb_SelectAllMenu.Size = new System.Drawing.Size(159, 27);
            this.cb_SelectAllMenu.TabIndex = 62;
            this.cb_SelectAllMenu.Text = "Select All Menu";
            this.cb_SelectAllMenu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cb_SelectAllMenu.UseVisualStyleBackColor = true;
            this.cb_SelectAllMenu.CheckedChanged += new System.EventHandler(this.cb_SelectAllMenu_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cb_SelectAllNew);
            this.groupBox2.Controls.Add(this.cb_SelectAllMenu);
            this.groupBox2.Controls.Add(this.btn_Save);
            this.groupBox2.Controls.Add(this.cb_SelectAllCopy);
            this.groupBox2.Controls.Add(this.btn_Delete);
            this.groupBox2.Controls.Add(this.cb_SelectAllSave);
            this.groupBox2.Controls.Add(this.cb_SelectAllEdit);
            this.groupBox2.Controls.Add(this.cb_SelectAllDelete);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 508);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(1404, 57);
            this.groupBox2.TabIndex = 63;
            this.groupBox2.TabStop = false;
            // 
            // FrmMenuRights
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1404, 565);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.MaximizeBox = false;
            this.Name = "FrmMenuRights";
            this.ShowIcon = false;
            this.Text = "Menu Rights";
            this.Load += new System.EventHandler(this.FrmMenuRights_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmMenuRights_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.GMenuRights)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.DataGridView GMenuRights;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_UserType;
        private System.Windows.Forms.ComboBox cb_UserType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_Module;
        private System.Windows.Forms.Label lbl_MenuModule;
        private System.Windows.Forms.ComboBox cb_MenuModule;
        private System.Windows.Forms.ComboBox cb_User;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cb_SelectAllNew;
        private System.Windows.Forms.CheckBox cb_SelectAllEdit;
        private System.Windows.Forms.CheckBox cb_SelectAllDelete;
        private System.Windows.Forms.CheckBox cb_SelectAllSave;
        private System.Windows.Forms.CheckBox cb_SelectAllCopy;
        private System.Windows.Forms.CheckBox cb_SelectAllMenu;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
        private System.Windows.Forms.DataGridViewTextBoxColumn txt_GId;
        private System.Windows.Forms.DataGridViewTextBoxColumn txt_GMenu_Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn txt_GMenu_Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn txt_GMenu_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn txt_GForm_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn txt_GSubModule_Id;
        private System.Windows.Forms.DataGridViewCheckBoxColumn txt_GNew;
        private System.Windows.Forms.DataGridViewCheckBoxColumn txt_GEdit;
        private System.Windows.Forms.DataGridViewCheckBoxColumn txt_GDelete;
        private System.Windows.Forms.DataGridViewCheckBoxColumn txt_GSave;
        private System.Windows.Forms.DataGridViewCheckBoxColumn txt_GCopy;
        private System.Windows.Forms.DataGridViewCheckBoxColumn txt_GSearch;
        private System.Windows.Forms.DataGridViewCheckBoxColumn txt_GPrint;
        private System.Windows.Forms.DataGridViewCheckBoxColumn txt_GApproved;
        private System.Windows.Forms.DataGridViewCheckBoxColumn txt_GReverse;
        private System.Windows.Forms.DataGridViewTextBoxColumn txt_GIsParent;
        private MrPanel panel1;
    }
}