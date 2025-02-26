using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.SystemSetting
{
    partial class FrmInventorySetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInventorySetting));
            this.Btn_Save = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.ChkCostCenterMandatory = new System.Windows.Forms.CheckBox();
            this.ChkGodownMandatory = new System.Windows.Forms.CheckBox();
            this.ChkGodownWiseStock = new System.Windows.Forms.CheckBox();
            this.ChkGroupWiseFilter = new System.Windows.Forms.CheckBox();
            this.ChkAltUnitEnable = new System.Windows.Forms.CheckBox();
            this.ChkGodownEnable = new System.Windows.Forms.CheckBox();
            this.ChkGodownItemEnable = new System.Windows.Forms.CheckBox();
            this.ChkCostCenterItem = new System.Windows.Forms.CheckBox();
            this.ChkRemarksEnable = new System.Windows.Forms.CheckBox();
            this.ChkUnitEnable = new System.Windows.Forms.CheckBox();
            this.ChkCaryBatchQty = new System.Windows.Forms.CheckBox();
            this.ChkExpdate = new System.Windows.Forms.CheckBox();
            this.ChkFreeQty = new System.Windows.Forms.CheckBox();
            this.ChkCostCenter = new System.Windows.Forms.CheckBox();
            this.BtnStockInHand = new System.Windows.Forms.Button();
            this.BtnClosingStock = new System.Windows.Forms.Button();
            this.BtnOpeningStock = new System.Windows.Forms.Button();
            this.TxtOpeningStock = new MrTextBox();
            this.TxtClosingStock = new MrTextBox();
            this.TxtStockInHand = new MrTextBox();
            this.lbl_OpeningStockPl = new System.Windows.Forms.Label();
            this.lbl_ClosingStockPL = new System.Windows.Forms.Label();
            this.lbl_NegStockWarning = new System.Windows.Forms.Label();
            this.CmbNegativeStockWarning = new System.Windows.Forms.ComboBox();
            this.lbl_ClosingStock = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtBarCode = new MrTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Enable = new System.Windows.Forms.GroupBox();
            this.ChkDepartmentItem = new System.Windows.Forms.CheckBox();
            this.ChkDepartment = new System.Windows.Forms.CheckBox();
            this.ChkShortNameWise = new System.Windows.Forms.CheckBox();
            this.ChkNarrationEnable = new System.Windows.Forms.CheckBox();
            this.ChkCostCenterItemMandatory = new System.Windows.Forms.CheckBox();
            this.ChkGodownItemMandatory = new System.Windows.Forms.CheckBox();
            this.Mandatory = new System.Windows.Forms.GroupBox();
            this.ChkDepartmentItemMandatory = new System.Windows.Forms.CheckBox();
            this.ChkDepartmentMandatory = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel1 = new MrPanel();
            this.clsSeparator1 = new ClsSeparator();
            this.groupBox1.SuspendLayout();
            this.Enable.SuspendLayout();
            this.Mandatory.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_Save
            // 
            this.Btn_Save.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Save.Appearance.Options.UseFont = true;
            this.Btn_Save.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.Btn_Save.Location = new System.Drawing.Point(365, 351);
            this.Btn_Save.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new System.Drawing.Size(99, 36);
            this.Btn_Save.TabIndex = 4;
            this.Btn_Save.Text = "&SAVE";
            this.Btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(203, 351);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(161, 36);
            this.BtnSave.TabIndex = 3;
            this.BtnSave.Text = "SAVE && C&LOSE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // ChkCostCenterMandatory
            // 
            this.ChkCostCenterMandatory.Enabled = false;
            this.ChkCostCenterMandatory.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkCostCenterMandatory.Location = new System.Drawing.Point(6, 21);
            this.ChkCostCenterMandatory.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChkCostCenterMandatory.Name = "ChkCostCenterMandatory";
            this.ChkCostCenterMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkCostCenterMandatory.Size = new System.Drawing.Size(159, 22);
            this.ChkCostCenterMandatory.TabIndex = 0;
            this.ChkCostCenterMandatory.Text = "Cost Center";
            this.ChkCostCenterMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkCostCenterMandatory.UseVisualStyleBackColor = true;
            // 
            // ChkGodownMandatory
            // 
            this.ChkGodownMandatory.Enabled = false;
            this.ChkGodownMandatory.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkGodownMandatory.Location = new System.Drawing.Point(6, 65);
            this.ChkGodownMandatory.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChkGodownMandatory.Name = "ChkGodownMandatory";
            this.ChkGodownMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkGodownMandatory.Size = new System.Drawing.Size(159, 22);
            this.ChkGodownMandatory.TabIndex = 2;
            this.ChkGodownMandatory.Text = "Godown";
            this.ChkGodownMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkGodownMandatory.UseVisualStyleBackColor = true;
            // 
            // ChkGodownWiseStock
            // 
            this.ChkGodownWiseStock.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkGodownWiseStock.Location = new System.Drawing.Point(149, 65);
            this.ChkGodownWiseStock.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChkGodownWiseStock.Name = "ChkGodownWiseStock";
            this.ChkGodownWiseStock.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkGodownWiseStock.Size = new System.Drawing.Size(182, 22);
            this.ChkGodownWiseStock.TabIndex = 9;
            this.ChkGodownWiseStock.Text = "Godowon Wise Stock";
            this.ChkGodownWiseStock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkGodownWiseStock.UseVisualStyleBackColor = true;
            // 
            // ChkGroupWiseFilter
            // 
            this.ChkGroupWiseFilter.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkGroupWiseFilter.Location = new System.Drawing.Point(149, 43);
            this.ChkGroupWiseFilter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChkGroupWiseFilter.Name = "ChkGroupWiseFilter";
            this.ChkGroupWiseFilter.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkGroupWiseFilter.Size = new System.Drawing.Size(182, 22);
            this.ChkGroupWiseFilter.TabIndex = 8;
            this.ChkGroupWiseFilter.Text = "Group Wise Filter";
            this.ChkGroupWiseFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkGroupWiseFilter.UseVisualStyleBackColor = true;
            // 
            // ChkAltUnitEnable
            // 
            this.ChkAltUnitEnable.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkAltUnitEnable.Location = new System.Drawing.Point(6, 21);
            this.ChkAltUnitEnable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChkAltUnitEnable.Name = "ChkAltUnitEnable";
            this.ChkAltUnitEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkAltUnitEnable.Size = new System.Drawing.Size(135, 22);
            this.ChkAltUnitEnable.TabIndex = 0;
            this.ChkAltUnitEnable.Text = "Alt Unit";
            this.ChkAltUnitEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkAltUnitEnable.UseVisualStyleBackColor = true;
            // 
            // ChkGodownEnable
            // 
            this.ChkGodownEnable.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkGodownEnable.Location = new System.Drawing.Point(149, 131);
            this.ChkGodownEnable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChkGodownEnable.Name = "ChkGodownEnable";
            this.ChkGodownEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkGodownEnable.Size = new System.Drawing.Size(182, 22);
            this.ChkGodownEnable.TabIndex = 12;
            this.ChkGodownEnable.Text = "Godown";
            this.ChkGodownEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkGodownEnable.UseVisualStyleBackColor = true;
            this.ChkGodownEnable.CheckStateChanged += new System.EventHandler(this.ChkGodownEnable_CheckStateChanged);
            // 
            // ChkGodownItemEnable
            // 
            this.ChkGodownItemEnable.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkGodownItemEnable.Location = new System.Drawing.Point(149, 153);
            this.ChkGodownItemEnable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChkGodownItemEnable.Name = "ChkGodownItemEnable";
            this.ChkGodownItemEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkGodownItemEnable.Size = new System.Drawing.Size(182, 22);
            this.ChkGodownItemEnable.TabIndex = 13;
            this.ChkGodownItemEnable.Text = "Godown Item";
            this.ChkGodownItemEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkGodownItemEnable.UseVisualStyleBackColor = true;
            this.ChkGodownItemEnable.CheckedChanged += new System.EventHandler(this.ChkGodownItemEnable_CheckedChanged);
            // 
            // ChkCostCenterItem
            // 
            this.ChkCostCenterItem.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkCostCenterItem.Location = new System.Drawing.Point(149, 109);
            this.ChkCostCenterItem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChkCostCenterItem.Name = "ChkCostCenterItem";
            this.ChkCostCenterItem.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkCostCenterItem.Size = new System.Drawing.Size(182, 22);
            this.ChkCostCenterItem.TabIndex = 11;
            this.ChkCostCenterItem.Text = "Cost Center Item";
            this.ChkCostCenterItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkCostCenterItem.UseVisualStyleBackColor = true;
            this.ChkCostCenterItem.CheckedChanged += new System.EventHandler(this.ChkCostCenterItem_CheckedChanged);
            // 
            // ChkRemarksEnable
            // 
            this.ChkRemarksEnable.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkRemarksEnable.Location = new System.Drawing.Point(6, 65);
            this.ChkRemarksEnable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChkRemarksEnable.Name = "ChkRemarksEnable";
            this.ChkRemarksEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkRemarksEnable.Size = new System.Drawing.Size(135, 22);
            this.ChkRemarksEnable.TabIndex = 2;
            this.ChkRemarksEnable.Text = "Remarks";
            this.ChkRemarksEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkRemarksEnable.UseVisualStyleBackColor = true;
            // 
            // ChkUnitEnable
            // 
            this.ChkUnitEnable.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkUnitEnable.Location = new System.Drawing.Point(6, 43);
            this.ChkUnitEnable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChkUnitEnable.Name = "ChkUnitEnable";
            this.ChkUnitEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkUnitEnable.Size = new System.Drawing.Size(135, 22);
            this.ChkUnitEnable.TabIndex = 1;
            this.ChkUnitEnable.Text = "Change Unit";
            this.ChkUnitEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkUnitEnable.UseVisualStyleBackColor = true;
            // 
            // ChkCaryBatchQty
            // 
            this.ChkCaryBatchQty.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkCaryBatchQty.Location = new System.Drawing.Point(6, 131);
            this.ChkCaryBatchQty.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChkCaryBatchQty.Name = "ChkCaryBatchQty";
            this.ChkCaryBatchQty.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkCaryBatchQty.Size = new System.Drawing.Size(135, 22);
            this.ChkCaryBatchQty.TabIndex = 5;
            this.ChkCaryBatchQty.Text = "Batch Qty";
            this.ChkCaryBatchQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkCaryBatchQty.UseVisualStyleBackColor = true;
            // 
            // ChkExpdate
            // 
            this.ChkExpdate.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkExpdate.Location = new System.Drawing.Point(6, 153);
            this.ChkExpdate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChkExpdate.Name = "ChkExpdate";
            this.ChkExpdate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkExpdate.Size = new System.Drawing.Size(135, 22);
            this.ChkExpdate.TabIndex = 6;
            this.ChkExpdate.Text = "Exp. Date";
            this.ChkExpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkExpdate.UseVisualStyleBackColor = true;
            // 
            // ChkFreeQty
            // 
            this.ChkFreeQty.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkFreeQty.Location = new System.Drawing.Point(149, 21);
            this.ChkFreeQty.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChkFreeQty.Name = "ChkFreeQty";
            this.ChkFreeQty.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkFreeQty.Size = new System.Drawing.Size(182, 22);
            this.ChkFreeQty.TabIndex = 7;
            this.ChkFreeQty.Text = "Free Qty";
            this.ChkFreeQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkFreeQty.UseVisualStyleBackColor = true;
            // 
            // ChkCostCenter
            // 
            this.ChkCostCenter.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkCostCenter.Location = new System.Drawing.Point(149, 87);
            this.ChkCostCenter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChkCostCenter.Name = "ChkCostCenter";
            this.ChkCostCenter.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkCostCenter.Size = new System.Drawing.Size(182, 22);
            this.ChkCostCenter.TabIndex = 10;
            this.ChkCostCenter.Text = "Cost Center";
            this.ChkCostCenter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkCostCenter.UseVisualStyleBackColor = true;
            this.ChkCostCenter.CheckStateChanged += new System.EventHandler(this.ChkICostCenter_CheckStateChanged);
            // 
            // BtnStockInHand
            // 
            this.BtnStockInHand.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnStockInHand.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnStockInHand.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnStockInHand.Location = new System.Drawing.Point(481, 73);
            this.BtnStockInHand.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnStockInHand.Name = "BtnStockInHand";
            this.BtnStockInHand.Size = new System.Drawing.Size(29, 26);
            this.BtnStockInHand.TabIndex = 196;
            this.BtnStockInHand.TabStop = false;
            this.BtnStockInHand.UseVisualStyleBackColor = true;
            this.BtnStockInHand.Click += new System.EventHandler(this.BtnStockInHand_Click);
            // 
            // BtnClosingStock
            // 
            this.BtnClosingStock.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnClosingStock.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnClosingStock.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnClosingStock.Location = new System.Drawing.Point(481, 47);
            this.BtnClosingStock.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnClosingStock.Name = "BtnClosingStock";
            this.BtnClosingStock.Size = new System.Drawing.Size(29, 24);
            this.BtnClosingStock.TabIndex = 194;
            this.BtnClosingStock.TabStop = false;
            this.BtnClosingStock.UseVisualStyleBackColor = true;
            this.BtnClosingStock.Click += new System.EventHandler(this.BtnClosingStock_Click);
            // 
            // BtnOpeningStock
            // 
            this.BtnOpeningStock.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnOpeningStock.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnOpeningStock.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnOpeningStock.Location = new System.Drawing.Point(481, 21);
            this.BtnOpeningStock.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnOpeningStock.Name = "BtnOpeningStock";
            this.BtnOpeningStock.Size = new System.Drawing.Size(29, 24);
            this.BtnOpeningStock.TabIndex = 195;
            this.BtnOpeningStock.TabStop = false;
            this.BtnOpeningStock.UseVisualStyleBackColor = true;
            this.BtnOpeningStock.Click += new System.EventHandler(this.BtnOpeningStock_Click);
            // 
            // TxtOpeningStock
            // 
            this.TxtOpeningStock.BackColor = System.Drawing.Color.White;
            this.TxtOpeningStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtOpeningStock.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtOpeningStock.Location = new System.Drawing.Point(131, 21);
            this.TxtOpeningStock.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TxtOpeningStock.MaxLength = 255;
            this.TxtOpeningStock.Name = "TxtOpeningStock";
            this.TxtOpeningStock.ReadOnly = true;
            this.TxtOpeningStock.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtOpeningStock.Size = new System.Drawing.Size(348, 25);
            this.TxtOpeningStock.TabIndex = 0;
            this.TxtOpeningStock.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtOpeningStock.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtOpeningStockPl_KeyDown);
            this.TxtOpeningStock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtOpeningStock.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // TxtClosingStock
            // 
            this.TxtClosingStock.BackColor = System.Drawing.Color.White;
            this.TxtClosingStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtClosingStock.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtClosingStock.Location = new System.Drawing.Point(131, 47);
            this.TxtClosingStock.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TxtClosingStock.MaxLength = 255;
            this.TxtClosingStock.Name = "TxtClosingStock";
            this.TxtClosingStock.ReadOnly = true;
            this.TxtClosingStock.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtClosingStock.Size = new System.Drawing.Size(348, 25);
            this.TxtClosingStock.TabIndex = 1;
            this.TxtClosingStock.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtClosingStock.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtClosingStockPL_KeyDown);
            this.TxtClosingStock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtClosingStock.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // TxtStockInHand
            // 
            this.TxtStockInHand.BackColor = System.Drawing.Color.White;
            this.TxtStockInHand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtStockInHand.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtStockInHand.Location = new System.Drawing.Point(131, 74);
            this.TxtStockInHand.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TxtStockInHand.MaxLength = 255;
            this.TxtStockInHand.Name = "TxtStockInHand";
            this.TxtStockInHand.ReadOnly = true;
            this.TxtStockInHand.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtStockInHand.Size = new System.Drawing.Size(348, 25);
            this.TxtStockInHand.TabIndex = 2;
            this.TxtStockInHand.Enter += new System.EventHandler(this.TextBoxCtrl_Enter);
            this.TxtStockInHand.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtClosingStockBS_KeyDown);
            this.TxtStockInHand.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Global_EnterKey);
            this.TxtStockInHand.Leave += new System.EventHandler(this.TextBoxCtrl_Leave);
            // 
            // lbl_OpeningStockPl
            // 
            this.lbl_OpeningStockPl.AutoSize = true;
            this.lbl_OpeningStockPl.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_OpeningStockPl.Location = new System.Drawing.Point(6, 25);
            this.lbl_OpeningStockPl.Name = "lbl_OpeningStockPl";
            this.lbl_OpeningStockPl.Size = new System.Drawing.Size(117, 19);
            this.lbl_OpeningStockPl.TabIndex = 190;
            this.lbl_OpeningStockPl.Text = "Opening Stock";
            // 
            // lbl_ClosingStockPL
            // 
            this.lbl_ClosingStockPL.AutoSize = true;
            this.lbl_ClosingStockPL.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_ClosingStockPL.Location = new System.Drawing.Point(6, 48);
            this.lbl_ClosingStockPL.Name = "lbl_ClosingStockPL";
            this.lbl_ClosingStockPL.Size = new System.Drawing.Size(110, 19);
            this.lbl_ClosingStockPL.TabIndex = 191;
            this.lbl_ClosingStockPL.Text = "Closing Stock";
            // 
            // lbl_NegStockWarning
            // 
            this.lbl_NegStockWarning.AccessibleDescription = "";
            this.lbl_NegStockWarning.AutoSize = true;
            this.lbl_NegStockWarning.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_NegStockWarning.Location = new System.Drawing.Point(6, 106);
            this.lbl_NegStockWarning.Name = "lbl_NegStockWarning";
            this.lbl_NegStockWarning.Size = new System.Drawing.Size(119, 19);
            this.lbl_NegStockWarning.TabIndex = 192;
            this.lbl_NegStockWarning.Text = "Negative Stock";
            // 
            // CmbNegativeStockWarning
            // 
            this.CmbNegativeStockWarning.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbNegativeStockWarning.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbNegativeStockWarning.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.CmbNegativeStockWarning.FormattingEnabled = true;
            this.CmbNegativeStockWarning.Location = new System.Drawing.Point(131, 102);
            this.CmbNegativeStockWarning.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CmbNegativeStockWarning.Name = "CmbNegativeStockWarning";
            this.CmbNegativeStockWarning.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CmbNegativeStockWarning.Size = new System.Drawing.Size(196, 27);
            this.CmbNegativeStockWarning.TabIndex = 3;
            // 
            // lbl_ClosingStock
            // 
            this.lbl_ClosingStock.AutoSize = true;
            this.lbl_ClosingStock.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_ClosingStock.Location = new System.Drawing.Point(6, 74);
            this.lbl_ClosingStock.Name = "lbl_ClosingStock";
            this.lbl_ClosingStock.Size = new System.Drawing.Size(115, 19);
            this.lbl_ClosingStock.TabIndex = 193;
            this.lbl_ClosingStock.Text = "Stock In Hand";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Controls.Add(this.TxtBarCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.TxtOpeningStock);
            this.groupBox1.Controls.Add(this.BtnStockInHand);
            this.groupBox1.Controls.Add(this.lbl_NegStockWarning);
            this.groupBox1.Controls.Add(this.CmbNegativeStockWarning);
            this.groupBox1.Controls.Add(this.BtnClosingStock);
            this.groupBox1.Controls.Add(this.lbl_ClosingStockPL);
            this.groupBox1.Controls.Add(this.lbl_ClosingStock);
            this.groupBox1.Controls.Add(this.BtnOpeningStock);
            this.groupBox1.Controls.Add(this.lbl_OpeningStockPl);
            this.groupBox1.Controls.Add(this.TxtStockInHand);
            this.groupBox1.Controls.Add(this.TxtClosingStock);
            this.groupBox1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(6, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(515, 141);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "LEDGER TAG";
            // 
            // TxtBarCode
            // 
            this.TxtBarCode.BackColor = System.Drawing.Color.White;
            this.TxtBarCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBarCode.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.TxtBarCode.Location = new System.Drawing.Point(408, 103);
            this.TxtBarCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TxtBarCode.MaxLength = 255;
            this.TxtBarCode.Name = "TxtBarCode";
            this.TxtBarCode.ReadOnly = true;
            this.TxtBarCode.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtBarCode.Size = new System.Drawing.Size(71, 25);
            this.TxtBarCode.TabIndex = 198;
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "";
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.label1.Location = new System.Drawing.Point(334, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 19);
            this.label1.TabIndex = 197;
            this.label1.Text = "BarCode";
            // 
            // Enable
            // 
            this.Enable.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Enable.Controls.Add(this.ChkDepartmentItem);
            this.Enable.Controls.Add(this.ChkDepartment);
            this.Enable.Controls.Add(this.ChkCostCenter);
            this.Enable.Controls.Add(this.ChkCostCenterItem);
            this.Enable.Controls.Add(this.ChkShortNameWise);
            this.Enable.Controls.Add(this.ChkGodownEnable);
            this.Enable.Controls.Add(this.ChkNarrationEnable);
            this.Enable.Controls.Add(this.ChkGodownItemEnable);
            this.Enable.Controls.Add(this.ChkAltUnitEnable);
            this.Enable.Controls.Add(this.ChkGroupWiseFilter);
            this.Enable.Controls.Add(this.ChkUnitEnable);
            this.Enable.Controls.Add(this.ChkGodownWiseStock);
            this.Enable.Controls.Add(this.ChkRemarksEnable);
            this.Enable.Controls.Add(this.ChkCaryBatchQty);
            this.Enable.Controls.Add(this.ChkFreeQty);
            this.Enable.Controls.Add(this.ChkExpdate);
            this.Enable.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.Enable.Location = new System.Drawing.Point(6, 144);
            this.Enable.Name = "Enable";
            this.Enable.Size = new System.Drawing.Size(337, 200);
            this.Enable.TabIndex = 1;
            this.Enable.TabStop = false;
            this.Enable.Text = "INVENTORY TAG";
            // 
            // ChkDepartmentItem
            // 
            this.ChkDepartmentItem.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkDepartmentItem.Location = new System.Drawing.Point(149, 173);
            this.ChkDepartmentItem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChkDepartmentItem.Name = "ChkDepartmentItem";
            this.ChkDepartmentItem.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDepartmentItem.Size = new System.Drawing.Size(182, 22);
            this.ChkDepartmentItem.TabIndex = 15;
            this.ChkDepartmentItem.Text = "Department Item";
            this.ChkDepartmentItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDepartmentItem.UseVisualStyleBackColor = true;
            // 
            // ChkDepartment
            // 
            this.ChkDepartment.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkDepartment.Location = new System.Drawing.Point(6, 173);
            this.ChkDepartment.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChkDepartment.Name = "ChkDepartment";
            this.ChkDepartment.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDepartment.Size = new System.Drawing.Size(134, 22);
            this.ChkDepartment.TabIndex = 14;
            this.ChkDepartment.Text = "Department";
            this.ChkDepartment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDepartment.UseVisualStyleBackColor = true;
            this.ChkDepartment.CheckedChanged += new System.EventHandler(this.ChkDepartment_CheckedChanged);
            // 
            // ChkShortNameWise
            // 
            this.ChkShortNameWise.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkShortNameWise.Location = new System.Drawing.Point(6, 109);
            this.ChkShortNameWise.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChkShortNameWise.Name = "ChkShortNameWise";
            this.ChkShortNameWise.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkShortNameWise.Size = new System.Drawing.Size(134, 22);
            this.ChkShortNameWise.TabIndex = 4;
            this.ChkShortNameWise.Text = "Is Shortname ";
            this.ChkShortNameWise.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ChkNarrationEnable
            // 
            this.ChkNarrationEnable.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkNarrationEnable.Location = new System.Drawing.Point(6, 87);
            this.ChkNarrationEnable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChkNarrationEnable.Name = "ChkNarrationEnable";
            this.ChkNarrationEnable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkNarrationEnable.Size = new System.Drawing.Size(135, 22);
            this.ChkNarrationEnable.TabIndex = 3;
            this.ChkNarrationEnable.Text = "Narration";
            this.ChkNarrationEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkNarrationEnable.UseVisualStyleBackColor = true;
            // 
            // ChkCostCenterItemMandatory
            // 
            this.ChkCostCenterItemMandatory.Enabled = false;
            this.ChkCostCenterItemMandatory.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkCostCenterItemMandatory.Location = new System.Drawing.Point(6, 43);
            this.ChkCostCenterItemMandatory.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChkCostCenterItemMandatory.Name = "ChkCostCenterItemMandatory";
            this.ChkCostCenterItemMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkCostCenterItemMandatory.Size = new System.Drawing.Size(159, 22);
            this.ChkCostCenterItemMandatory.TabIndex = 1;
            this.ChkCostCenterItemMandatory.Text = "Cost Center Item";
            this.ChkCostCenterItemMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkCostCenterItemMandatory.UseVisualStyleBackColor = true;
            // 
            // ChkGodownItemMandatory
            // 
            this.ChkGodownItemMandatory.Enabled = false;
            this.ChkGodownItemMandatory.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkGodownItemMandatory.Location = new System.Drawing.Point(6, 87);
            this.ChkGodownItemMandatory.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChkGodownItemMandatory.Name = "ChkGodownItemMandatory";
            this.ChkGodownItemMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkGodownItemMandatory.Size = new System.Drawing.Size(159, 22);
            this.ChkGodownItemMandatory.TabIndex = 3;
            this.ChkGodownItemMandatory.Text = "Godown Item";
            this.ChkGodownItemMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkGodownItemMandatory.UseVisualStyleBackColor = true;
            // 
            // Mandatory
            // 
            this.Mandatory.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Mandatory.Controls.Add(this.ChkDepartmentItemMandatory);
            this.Mandatory.Controls.Add(this.ChkDepartmentMandatory);
            this.Mandatory.Controls.Add(this.checkBox1);
            this.Mandatory.Controls.Add(this.ChkGodownItemMandatory);
            this.Mandatory.Controls.Add(this.ChkGodownMandatory);
            this.Mandatory.Controls.Add(this.ChkCostCenterItemMandatory);
            this.Mandatory.Controls.Add(this.ChkCostCenterMandatory);
            this.Mandatory.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.Mandatory.Location = new System.Drawing.Point(345, 144);
            this.Mandatory.Name = "Mandatory";
            this.Mandatory.Size = new System.Drawing.Size(176, 200);
            this.Mandatory.TabIndex = 2;
            this.Mandatory.TabStop = false;
            this.Mandatory.Text = "MANDATORY TAG";
            // 
            // ChkDepartmentItemMandatory
            // 
            this.ChkDepartmentItemMandatory.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkDepartmentItemMandatory.Location = new System.Drawing.Point(6, 153);
            this.ChkDepartmentItemMandatory.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChkDepartmentItemMandatory.Name = "ChkDepartmentItemMandatory";
            this.ChkDepartmentItemMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDepartmentItemMandatory.Size = new System.Drawing.Size(159, 22);
            this.ChkDepartmentItemMandatory.TabIndex = 16;
            this.ChkDepartmentItemMandatory.Text = "Department Item";
            this.ChkDepartmentItemMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDepartmentItemMandatory.UseVisualStyleBackColor = true;
            // 
            // ChkDepartmentMandatory
            // 
            this.ChkDepartmentMandatory.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.ChkDepartmentMandatory.Location = new System.Drawing.Point(6, 131);
            this.ChkDepartmentMandatory.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChkDepartmentMandatory.Name = "ChkDepartmentMandatory";
            this.ChkDepartmentMandatory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkDepartmentMandatory.Size = new System.Drawing.Size(159, 22);
            this.ChkDepartmentMandatory.TabIndex = 16;
            this.ChkDepartmentMandatory.Text = "Department";
            this.ChkDepartmentMandatory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDepartmentMandatory.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.checkBox1.Location = new System.Drawing.Point(6, 109);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox1.Size = new System.Drawing.Size(159, 22);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.Text = "Remarks";
            this.checkBox1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.clsSeparator1);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.Btn_Save);
            this.panel1.Controls.Add(this.BtnSave);
            this.panel1.Controls.Add(this.Mandatory);
            this.panel1.Controls.Add(this.Enable);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(526, 392);
            this.panel1.TabIndex = 0;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator1.Location = new System.Drawing.Point(3, 346);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(514, 2);
            this.clsSeparator1.TabIndex = 200;
            this.clsSeparator1.TabStop = false;
            // 
            // FrmInventorySetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(526, 392);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmInventorySetting";
            this.ShowIcon = false;
            this.Text = "INVENTORY SETTING";
            this.Load += new System.EventHandler(this.FrmInventorySetting_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmInventorySetting_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Enable.ResumeLayout(false);
            this.Mandatory.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button BtnStockInHand;
        private System.Windows.Forms.Button BtnClosingStock;
        private System.Windows.Forms.Button BtnOpeningStock;
        private System.Windows.Forms.Label lbl_OpeningStockPl;
        private System.Windows.Forms.Label lbl_ClosingStockPL;
        private System.Windows.Forms.Label lbl_NegStockWarning;
        private System.Windows.Forms.ComboBox CmbNegativeStockWarning;
        private System.Windows.Forms.Label lbl_ClosingStock;
        private System.Windows.Forms.CheckBox ChkCaryBatchQty;
        private System.Windows.Forms.CheckBox ChkExpdate;
        private System.Windows.Forms.CheckBox ChkFreeQty;
        private System.Windows.Forms.CheckBox ChkCostCenter;
        private System.Windows.Forms.CheckBox ChkAltUnitEnable;
        private System.Windows.Forms.CheckBox ChkGodownEnable;
        private System.Windows.Forms.CheckBox ChkGodownItemEnable;
        private System.Windows.Forms.CheckBox ChkCostCenterItem;
        private System.Windows.Forms.CheckBox ChkRemarksEnable;
        private System.Windows.Forms.CheckBox ChkUnitEnable;
        private System.Windows.Forms.CheckBox ChkGodownWiseStock;
        private System.Windows.Forms.CheckBox ChkGroupWiseFilter;
        private System.Windows.Forms.CheckBox ChkCostCenterMandatory;
        private System.Windows.Forms.CheckBox ChkGodownMandatory;
        private DevExpress.XtraEditors.SimpleButton Btn_Save;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox Enable;
        private System.Windows.Forms.CheckBox ChkGodownItemMandatory;
        private System.Windows.Forms.CheckBox ChkCostCenterItemMandatory;
        private System.Windows.Forms.CheckBox ChkShortNameWise;
        private System.Windows.Forms.CheckBox ChkNarrationEnable;
        private System.Windows.Forms.GroupBox Mandatory;
        private ClsSeparator clsSeparator1;
        private System.Windows.Forms.CheckBox checkBox1;
        private MrTextBox TxtOpeningStock;
        private MrTextBox TxtClosingStock;
        private MrTextBox TxtStockInHand;
        private MrPanel panel1;
        private System.Windows.Forms.Label label1;
        private MrTextBox TxtBarCode;
        private System.Windows.Forms.CheckBox ChkDepartment;
        private System.Windows.Forms.CheckBox ChkDepartmentItem;
        private System.Windows.Forms.CheckBox ChkDepartmentItemMandatory;
        private System.Windows.Forms.CheckBox ChkDepartmentMandatory;
    }
}