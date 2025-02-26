using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Register_Report.Analysis_Report.Purchase
{
    partial class FrmPurchaseAnalysisPerodic
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
            this.lbl_Branch = new System.Windows.Forms.Label();
            this.cmb_Branch = new System.Windows.Forms.ComboBox();
            this.chk_BillAgent = new System.Windows.Forms.CheckBox();
            this.lbl_PartyType = new System.Windows.Forms.Label();
            this.cmb_GroupBy = new System.Windows.Forms.ComboBox();
            this.lbl_GroupBy = new System.Windows.Forms.Label();
            this.cmb_Unit = new System.Windows.Forms.ComboBox();
            this.lbl_Unit = new System.Windows.Forms.Label();
            this.chk_Date = new System.Windows.Forms.CheckBox();
            this.chk_SelectAllFilterBy = new System.Windows.Forms.CheckBox();
            this.chk_SelectAllRptType = new System.Windows.Forms.CheckBox();
            this.cmb_FilteredBy = new System.Windows.Forms.ComboBox();
            this.lbl_FilteredBy = new System.Windows.Forms.Label();
            this.chk_AddTerm = new System.Windows.Forms.CheckBox();
            this.msk_ToDate = new MrMaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.msk_FromDate = new MrMaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb_Product = new System.Windows.Forms.RadioButton();
            this.rb_Party = new System.Windows.Forms.RadioButton();
            this.rb_Area = new System.Windows.Forms.RadioButton();
            this.rb_Agent = new System.Windows.Forms.RadioButton();
            this.gb_TBOptions = new System.Windows.Forms.GroupBox();
            this.btn_Show = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.gb_TBOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Branch
            // 
            this.lbl_Branch.AutoSize = true;
            this.lbl_Branch.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Branch.Location = new System.Drawing.Point(11, 177);
            this.lbl_Branch.Name = "lbl_Branch";
            this.lbl_Branch.Size = new System.Drawing.Size(72, 20);
            this.lbl_Branch.TabIndex = 75;
            this.lbl_Branch.Text = "Branch ";
            // 
            // cmb_Branch
            // 
            this.cmb_Branch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Branch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmb_Branch.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_Branch.FormattingEnabled = true;
            this.cmb_Branch.Location = new System.Drawing.Point(109, 176);
            this.cmb_Branch.Name = "cmb_Branch";
            this.cmb_Branch.Size = new System.Drawing.Size(188, 28);
            this.cmb_Branch.TabIndex = 13;
            // 
            // chk_BillAgent
            // 
            this.chk_BillAgent.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_BillAgent.Location = new System.Drawing.Point(347, 96);
            this.chk_BillAgent.Name = "chk_BillAgent";
            this.chk_BillAgent.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_BillAgent.Size = new System.Drawing.Size(175, 24);
            this.chk_BillAgent.TabIndex = 12;
            this.chk_BillAgent.Text = "Bill Agent";
            this.chk_BillAgent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_BillAgent.UseVisualStyleBackColor = true;
            // 
            // lbl_PartyType
            // 
            this.lbl_PartyType.AutoSize = true;
            this.lbl_PartyType.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_PartyType.Location = new System.Drawing.Point(10, 7);
            this.lbl_PartyType.Name = "lbl_PartyType";
            this.lbl_PartyType.Size = new System.Drawing.Size(104, 20);
            this.lbl_PartyType.TabIndex = 71;
            this.lbl_PartyType.Text = "Report Type";
            // 
            // cmb_GroupBy
            // 
            this.cmb_GroupBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_GroupBy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmb_GroupBy.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_GroupBy.FormattingEnabled = true;
            this.cmb_GroupBy.Location = new System.Drawing.Point(109, 150);
            this.cmb_GroupBy.Name = "cmb_GroupBy";
            this.cmb_GroupBy.Size = new System.Drawing.Size(188, 28);
            this.cmb_GroupBy.TabIndex = 9;
            this.cmb_GroupBy.Visible = false;
            // 
            // lbl_GroupBy
            // 
            this.lbl_GroupBy.AutoSize = true;
            this.lbl_GroupBy.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_GroupBy.Location = new System.Drawing.Point(11, 151);
            this.lbl_GroupBy.Name = "lbl_GroupBy";
            this.lbl_GroupBy.Size = new System.Drawing.Size(85, 20);
            this.lbl_GroupBy.TabIndex = 68;
            this.lbl_GroupBy.Text = "Group By";
            this.lbl_GroupBy.Visible = false;
            // 
            // cmb_Unit
            // 
            this.cmb_Unit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Unit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmb_Unit.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_Unit.FormattingEnabled = true;
            this.cmb_Unit.Location = new System.Drawing.Point(109, 203);
            this.cmb_Unit.Name = "cmb_Unit";
            this.cmb_Unit.Size = new System.Drawing.Size(188, 28);
            this.cmb_Unit.TabIndex = 10;
            // 
            // lbl_Unit
            // 
            this.lbl_Unit.AutoSize = true;
            this.lbl_Unit.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Unit.Location = new System.Drawing.Point(11, 206);
            this.lbl_Unit.Name = "lbl_Unit";
            this.lbl_Unit.Size = new System.Drawing.Size(43, 20);
            this.lbl_Unit.TabIndex = 67;
            this.lbl_Unit.Text = "Unit";
            // 
            // chk_Date
            // 
            this.chk_Date.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_Date.Location = new System.Drawing.Point(347, 171);
            this.chk_Date.Name = "chk_Date";
            this.chk_Date.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Date.Size = new System.Drawing.Size(175, 24);
            this.chk_Date.TabIndex = 16;
            this.chk_Date.Text = "Date";
            this.chk_Date.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Date.UseVisualStyleBackColor = true;
            // 
            // chk_SelectAllFilterBy
            // 
            this.chk_SelectAllFilterBy.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_SelectAllFilterBy.Location = new System.Drawing.Point(347, 146);
            this.chk_SelectAllFilterBy.Name = "chk_SelectAllFilterBy";
            this.chk_SelectAllFilterBy.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_SelectAllFilterBy.Size = new System.Drawing.Size(175, 24);
            this.chk_SelectAllFilterBy.TabIndex = 15;
            this.chk_SelectAllFilterBy.Text = "Select All";
            this.chk_SelectAllFilterBy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_SelectAllFilterBy.UseVisualStyleBackColor = true;
            // 
            // chk_SelectAllRptType
            // 
            this.chk_SelectAllRptType.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_SelectAllRptType.Location = new System.Drawing.Point(347, 121);
            this.chk_SelectAllRptType.Name = "chk_SelectAllRptType";
            this.chk_SelectAllRptType.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_SelectAllRptType.Size = new System.Drawing.Size(175, 24);
            this.chk_SelectAllRptType.TabIndex = 14;
            this.chk_SelectAllRptType.Text = "Select All";
            this.chk_SelectAllRptType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_SelectAllRptType.UseVisualStyleBackColor = true;
            // 
            // cmb_FilteredBy
            // 
            this.cmb_FilteredBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_FilteredBy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmb_FilteredBy.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_FilteredBy.FormattingEnabled = true;
            this.cmb_FilteredBy.Location = new System.Drawing.Point(109, 123);
            this.cmb_FilteredBy.Name = "cmb_FilteredBy";
            this.cmb_FilteredBy.Size = new System.Drawing.Size(188, 28);
            this.cmb_FilteredBy.TabIndex = 8;
            // 
            // lbl_FilteredBy
            // 
            this.lbl_FilteredBy.AutoSize = true;
            this.lbl_FilteredBy.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_FilteredBy.Location = new System.Drawing.Point(11, 124);
            this.lbl_FilteredBy.Name = "lbl_FilteredBy";
            this.lbl_FilteredBy.Size = new System.Drawing.Size(96, 20);
            this.lbl_FilteredBy.TabIndex = 64;
            this.lbl_FilteredBy.Text = "Filtered By";
            // 
            // chk_AddTerm
            // 
            this.chk_AddTerm.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_AddTerm.Location = new System.Drawing.Point(347, 71);
            this.chk_AddTerm.Name = "chk_AddTerm";
            this.chk_AddTerm.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_AddTerm.Size = new System.Drawing.Size(175, 24);
            this.chk_AddTerm.TabIndex = 11;
            this.chk_AddTerm.Text = "Add Term";
            this.chk_AddTerm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_AddTerm.UseVisualStyleBackColor = true;
            // 
            // msk_ToDate
            // 
            this.msk_ToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_ToDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msk_ToDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.msk_ToDate.Location = new System.Drawing.Point(109, 96);
            this.msk_ToDate.Mask = "00/00/0000";
            this.msk_ToDate.Name = "msk_ToDate";
            this.msk_ToDate.Size = new System.Drawing.Size(126, 26);
            this.msk_ToDate.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 57;
            this.label2.Text = "To Date";
            // 
            // msk_FromDate
            // 
            this.msk_FromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_FromDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msk_FromDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.msk_FromDate.Location = new System.Drawing.Point(109, 69);
            this.msk_FromDate.Mask = "00/00/0000";
            this.msk_FromDate.Name = "msk_FromDate";
            this.msk_FromDate.Size = new System.Drawing.Size(126, 26);
            this.msk_FromDate.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 20);
            this.label1.TabIndex = 55;
            this.label1.Text = "From Date";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rb_Product);
            this.groupBox1.Controls.Add(this.rb_Party);
            this.groupBox1.Controls.Add(this.rb_Area);
            this.groupBox1.Controls.Add(this.rb_Agent);
            this.groupBox1.Location = new System.Drawing.Point(3, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(543, 41);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            // 
            // rb_Product
            // 
            this.rb_Product.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_Product.Location = new System.Drawing.Point(6, 14);
            this.rb_Product.Name = "rb_Product";
            this.rb_Product.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rb_Product.Size = new System.Drawing.Size(139, 23);
            this.rb_Product.TabIndex = 2;
            this.rb_Product.Text = "Product Wise";
            this.rb_Product.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rb_Product.UseVisualStyleBackColor = true;
            // 
            // rb_Party
            // 
            this.rb_Party.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_Party.Location = new System.Drawing.Point(145, 14);
            this.rb_Party.Name = "rb_Party";
            this.rb_Party.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rb_Party.Size = new System.Drawing.Size(120, 23);
            this.rb_Party.TabIndex = 3;
            this.rb_Party.Text = "Party Wise";
            this.rb_Party.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rb_Party.UseVisualStyleBackColor = true;
            // 
            // rb_Area
            // 
            this.rb_Area.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_Area.Location = new System.Drawing.Point(385, 16);
            this.rb_Area.Name = "rb_Area";
            this.rb_Area.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rb_Area.Size = new System.Drawing.Size(107, 23);
            this.rb_Area.TabIndex = 5;
            this.rb_Area.Text = "Area Wise";
            this.rb_Area.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rb_Area.UseVisualStyleBackColor = true;
            // 
            // rb_Agent
            // 
            this.rb_Agent.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_Agent.Location = new System.Drawing.Point(265, 15);
            this.rb_Agent.Name = "rb_Agent";
            this.rb_Agent.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rb_Agent.Size = new System.Drawing.Size(120, 23);
            this.rb_Agent.TabIndex = 4;
            this.rb_Agent.Text = "Agent Wise";
            this.rb_Agent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rb_Agent.UseVisualStyleBackColor = true;
            // 
            // gb_TBOptions
            // 
            this.gb_TBOptions.Controls.Add(this.lbl_Branch);
            this.gb_TBOptions.Controls.Add(this.cmb_Branch);
            this.gb_TBOptions.Controls.Add(this.chk_BillAgent);
            this.gb_TBOptions.Controls.Add(this.lbl_PartyType);
            this.gb_TBOptions.Controls.Add(this.cmb_GroupBy);
            this.gb_TBOptions.Controls.Add(this.lbl_GroupBy);
            this.gb_TBOptions.Controls.Add(this.cmb_Unit);
            this.gb_TBOptions.Controls.Add(this.lbl_Unit);
            this.gb_TBOptions.Controls.Add(this.chk_Date);
            this.gb_TBOptions.Controls.Add(this.chk_SelectAllFilterBy);
            this.gb_TBOptions.Controls.Add(this.chk_SelectAllRptType);
            this.gb_TBOptions.Controls.Add(this.cmb_FilteredBy);
            this.gb_TBOptions.Controls.Add(this.lbl_FilteredBy);
            this.gb_TBOptions.Controls.Add(this.chk_AddTerm);
            this.gb_TBOptions.Controls.Add(this.msk_ToDate);
            this.gb_TBOptions.Controls.Add(this.label2);
            this.gb_TBOptions.Controls.Add(this.msk_FromDate);
            this.gb_TBOptions.Controls.Add(this.label1);
            this.gb_TBOptions.Controls.Add(this.groupBox1);
            this.gb_TBOptions.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_TBOptions.Location = new System.Drawing.Point(2, -7);
            this.gb_TBOptions.Name = "gb_TBOptions";
            this.gb_TBOptions.Size = new System.Drawing.Size(552, 235);
            this.gb_TBOptions.TabIndex = 21;
            this.gb_TBOptions.TabStop = false;
            // 
            // btn_Show
            // 
            this.btn_Show.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Show.Location = new System.Drawing.Point(456, 229);
            this.btn_Show.Name = "btn_Show";
            this.btn_Show.Size = new System.Drawing.Size(98, 35);
            this.btn_Show.TabIndex = 22;
            this.btn_Show.Text = "&Show";
            this.btn_Show.UseVisualStyleBackColor = true;
            // 
            // FrmPurchaseAnalysisPerodic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(557, 263);
            this.Controls.Add(this.gb_TBOptions);
            this.Controls.Add(this.btn_Show);
            this.Name = "FrmPurchaseAnalysisPerodic";
            this.Text = "PurchaseAnalysisPerodic";
            this.groupBox1.ResumeLayout(false);
            this.gb_TBOptions.ResumeLayout(false);
            this.gb_TBOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_Branch;
        private System.Windows.Forms.ComboBox cmb_Branch;
        private System.Windows.Forms.CheckBox chk_BillAgent;
        private System.Windows.Forms.Label lbl_PartyType;
        private System.Windows.Forms.ComboBox cmb_GroupBy;
        private System.Windows.Forms.Label lbl_GroupBy;
        private System.Windows.Forms.ComboBox cmb_Unit;
        private System.Windows.Forms.Label lbl_Unit;
        private System.Windows.Forms.CheckBox chk_Date;
        private System.Windows.Forms.CheckBox chk_SelectAllFilterBy;
        private System.Windows.Forms.CheckBox chk_SelectAllRptType;
        private System.Windows.Forms.ComboBox cmb_FilteredBy;
        private System.Windows.Forms.Label lbl_FilteredBy;
        private System.Windows.Forms.CheckBox chk_AddTerm;
        private System.Windows.Forms.MaskedTextBox msk_ToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox msk_FromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rb_Product;
        private System.Windows.Forms.RadioButton rb_Party;
        private System.Windows.Forms.RadioButton rb_Area;
        private System.Windows.Forms.RadioButton rb_Agent;
        private System.Windows.Forms.GroupBox gb_TBOptions;
        private System.Windows.Forms.Button btn_Show;
    }
}