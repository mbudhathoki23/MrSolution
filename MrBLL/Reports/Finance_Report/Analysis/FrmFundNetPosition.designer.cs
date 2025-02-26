using System.ComponentModel;
using System.Windows.Forms;
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Finance_Report.Analysis
{
    partial class FrmFundNetPosition
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.btn_Show = new System.Windows.Forms.Button();
            this.gb_TBOptions = new System.Windows.Forms.GroupBox();
            this.chk_SelectAll = new System.Windows.Forms.CheckBox();
            this.chk_Date = new System.Windows.Forms.CheckBox();
            this.lbl_Branch = new System.Windows.Forms.Label();
            this.cmb_Branch = new System.Windows.Forms.ComboBox();
            this.msk_AsOnDate = new MrMaskedTextBox();
            this.lbl_AsOnDate = new System.Windows.Forms.Label();
            this.gb_TBOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Show
            // 
            this.btn_Show.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Show.Location = new System.Drawing.Point(165, 76);
            this.btn_Show.Name = "btn_Show";
            this.btn_Show.Size = new System.Drawing.Size(70, 26);
            this.btn_Show.TabIndex = 12;
            this.btn_Show.Text = "&Show";
            this.btn_Show.UseVisualStyleBackColor = true;
            this.btn_Show.Click += new System.EventHandler(this.btn_Show_Click);
            // 
            // gb_TBOptions
            // 
            this.gb_TBOptions.Controls.Add(this.chk_SelectAll);
            this.gb_TBOptions.Controls.Add(this.chk_Date);
            this.gb_TBOptions.Controls.Add(this.lbl_Branch);
            this.gb_TBOptions.Controls.Add(this.cmb_Branch);
            this.gb_TBOptions.Controls.Add(this.msk_AsOnDate);
            this.gb_TBOptions.Controls.Add(this.lbl_AsOnDate);
            this.gb_TBOptions.Location = new System.Drawing.Point(1, -1);
            this.gb_TBOptions.Name = "gb_TBOptions";
            this.gb_TBOptions.Size = new System.Drawing.Size(347, 71);
            this.gb_TBOptions.TabIndex = 11;
            this.gb_TBOptions.TabStop = false;
            // 
            // chk_SelectAll
            // 
            this.chk_SelectAll.Font = new System.Drawing.Font("Arial", 9F);
            this.chk_SelectAll.Location = new System.Drawing.Point(214, 41);
            this.chk_SelectAll.Name = "chk_SelectAll";
            this.chk_SelectAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_SelectAll.Size = new System.Drawing.Size(118, 19);
            this.chk_SelectAll.TabIndex = 76;
            this.chk_SelectAll.Text = "Select All";
            this.chk_SelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_SelectAll.UseVisualStyleBackColor = true;
            // 
            // chk_Date
            // 
            this.chk_Date.Font = new System.Drawing.Font("Arial", 9F);
            this.chk_Date.Location = new System.Drawing.Point(214, 16);
            this.chk_Date.Name = "chk_Date";
            this.chk_Date.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_Date.Size = new System.Drawing.Size(120, 19);
            this.chk_Date.TabIndex = 9;
            this.chk_Date.Text = "Date";
            this.chk_Date.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Date.UseVisualStyleBackColor = true;
            // 
            // lbl_Branch
            // 
            this.lbl_Branch.AutoSize = true;
            this.lbl_Branch.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_Branch.Location = new System.Drawing.Point(10, 45);
            this.lbl_Branch.Name = "lbl_Branch";
            this.lbl_Branch.Size = new System.Drawing.Size(49, 15);
            this.lbl_Branch.TabIndex = 75;
            this.lbl_Branch.Text = "Branch ";
            // 
            // cmb_Branch
            // 
            this.cmb_Branch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Branch.FormattingEnabled = true;
            this.cmb_Branch.Location = new System.Drawing.Point(88, 43);
            this.cmb_Branch.Name = "cmb_Branch";
            this.cmb_Branch.Size = new System.Drawing.Size(109, 21);
            this.cmb_Branch.TabIndex = 5;
            // 
            // msk_AsOnDate
            // 
            this.msk_AsOnDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_AsOnDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msk_AsOnDate.Location = new System.Drawing.Point(88, 14);
            this.msk_AsOnDate.Mask = "00/00/0000";
            this.msk_AsOnDate.Name = "msk_AsOnDate";
            this.msk_AsOnDate.Size = new System.Drawing.Size(108, 20);
            this.msk_AsOnDate.TabIndex = 3;
            this.msk_AsOnDate.Enter += new System.EventHandler(this.msk_AsOnDate_Enter);
            this.msk_AsOnDate.Leave += new System.EventHandler(this.msk_AsOnDate_Leave);
            this.msk_AsOnDate.Validated += new System.EventHandler(this.msk_AsOnDate_Validated);
            // 
            // lbl_AsOnDate
            // 
            this.lbl_AsOnDate.AutoSize = true;
            this.lbl_AsOnDate.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_AsOnDate.Location = new System.Drawing.Point(10, 15);
            this.lbl_AsOnDate.Name = "lbl_AsOnDate";
            this.lbl_AsOnDate.Size = new System.Drawing.Size(69, 15);
            this.lbl_AsOnDate.TabIndex = 55;
            this.lbl_AsOnDate.Text = "As On Date";
            // 
            // FrmFundNetPosition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 105);
            this.Controls.Add(this.btn_Show);
            this.Controls.Add(this.gb_TBOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmFundNetPosition";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fund Net Positions";
            this.Load += new System.EventHandler(this.FrmFundNetPosition_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmFundNetPosition_KeyPress);
            this.gb_TBOptions.ResumeLayout(false);
            this.gb_TBOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Button btn_Show;
        private GroupBox gb_TBOptions;
        private CheckBox chk_Date;
        private Label lbl_Branch;
        private ComboBox cmb_Branch;
        private MaskedTextBox msk_AsOnDate;
        private Label lbl_AsOnDate;
        private CheckBox chk_SelectAll;
    }
}