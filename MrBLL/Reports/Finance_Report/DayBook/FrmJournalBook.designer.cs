using System.ComponentModel;
using System.Windows.Forms;
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Reports.Finance_Report.DayBook
{
    partial class FrmJournalBook
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
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnShow = new DevExpress.XtraEditors.SimpleButton();
            this.RbtnCurrencyBoth = new System.Windows.Forms.RadioButton();
            this.RbtnForeign = new System.Windows.Forms.RadioButton();
            this.RbtnLocal = new System.Windows.Forms.RadioButton();
            this.TxtFilterValue = new MrTextBox();
            this.lbl_Find = new System.Windows.Forms.Label();
            this.ChkNarration = new System.Windows.Forms.CheckBox();
            this.ChkIsTFormat = new System.Windows.Forms.CheckBox();
            this.ChkIsDate = new System.Windows.Forms.CheckBox();
            this.ChkIncludeSubledger = new System.Windows.Forms.CheckBox();
            this.ChkIncludeRemarks = new System.Windows.Forms.CheckBox();
            this.CmbDateType = new System.Windows.Forms.ComboBox();
            this.MskFrom = new MrMaskedTextBox();
            this.MskToDate = new MrMaskedTextBox();
            this.mrGroup1 = new MrGroup();
            this.mrPanel1 = new MrPanel();
            this.mrGroup2 = new MrGroup();
            this.mrGroup3 = new MrGroup();
            this.mrGroup1.SuspendLayout();
            this.mrPanel1.SuspendLayout();
            this.mrGroup2.SuspendLayout();
            this.mrGroup3.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(383, 140);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(109, 37);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "&CANCEL";
            // 
            // BtnShow
            // 
            this.BtnShow.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnShow.Appearance.Options.UseFont = true;
            this.BtnShow.ImageOptions.Image = global::MrBLL.Properties.Resources.show;
            this.BtnShow.Location = new System.Drawing.Point(272, 140);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(109, 37);
            this.BtnShow.TabIndex = 0;
            this.BtnShow.Text = "&SHOW";
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // RbtnCurrencyBoth
            // 
            this.RbtnCurrencyBoth.AutoSize = true;
            this.RbtnCurrencyBoth.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.RbtnCurrencyBoth.Location = new System.Drawing.Point(92, 16);
            this.RbtnCurrencyBoth.Name = "RbtnCurrencyBoth";
            this.RbtnCurrencyBoth.Size = new System.Drawing.Size(65, 24);
            this.RbtnCurrencyBoth.TabIndex = 1;
            this.RbtnCurrencyBoth.Text = "Both";
            this.RbtnCurrencyBoth.UseVisualStyleBackColor = true;
            // 
            // RbtnForeign
            // 
            this.RbtnForeign.AutoSize = true;
            this.RbtnForeign.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.RbtnForeign.Location = new System.Drawing.Point(165, 16);
            this.RbtnForeign.Name = "RbtnForeign";
            this.RbtnForeign.Size = new System.Drawing.Size(87, 24);
            this.RbtnForeign.TabIndex = 2;
            this.RbtnForeign.Text = "Foreign";
            this.RbtnForeign.UseVisualStyleBackColor = true;
            // 
            // RbtnLocal
            // 
            this.RbtnLocal.AutoSize = true;
            this.RbtnLocal.Checked = true;
            this.RbtnLocal.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.RbtnLocal.Location = new System.Drawing.Point(16, 16);
            this.RbtnLocal.Name = "RbtnLocal";
            this.RbtnLocal.Size = new System.Drawing.Size(68, 24);
            this.RbtnLocal.TabIndex = 0;
            this.RbtnLocal.TabStop = true;
            this.RbtnLocal.Text = "Local";
            this.RbtnLocal.UseVisualStyleBackColor = true;
            // 
            // TxtFilterValue
            // 
            this.TxtFilterValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtFilterValue.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtFilterValue.Location = new System.Drawing.Point(222, 74);
            this.TxtFilterValue.Name = "TxtFilterValue";
            this.TxtFilterValue.Size = new System.Drawing.Size(204, 26);
            this.TxtFilterValue.TabIndex = 3;
            // 
            // lbl_Find
            // 
            this.lbl_Find.AutoSize = true;
            this.lbl_Find.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Find.Location = new System.Drawing.Point(168, 76);
            this.lbl_Find.Name = "lbl_Find";
            this.lbl_Find.Size = new System.Drawing.Size(45, 20);
            this.lbl_Find.TabIndex = 71;
            this.lbl_Find.Text = "Find";
            // 
            // ChkNarration
            // 
            this.ChkNarration.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkNarration.Location = new System.Drawing.Point(168, 52);
            this.ChkNarration.Name = "ChkNarration";
            this.ChkNarration.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkNarration.Size = new System.Drawing.Size(151, 23);
            this.ChkNarration.TabIndex = 4;
            this.ChkNarration.Text = "Narration";
            this.ChkNarration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkNarration.UseVisualStyleBackColor = true;
            // 
            // ChkIsTFormat
            // 
            this.ChkIsTFormat.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIsTFormat.Location = new System.Drawing.Point(10, 28);
            this.ChkIsTFormat.Name = "ChkIsTFormat";
            this.ChkIsTFormat.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIsTFormat.Size = new System.Drawing.Size(151, 23);
            this.ChkIsTFormat.TabIndex = 0;
            this.ChkIsTFormat.Text = "T - Format";
            this.ChkIsTFormat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIsTFormat.UseVisualStyleBackColor = true;
            // 
            // ChkIsDate
            // 
            this.ChkIsDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIsDate.Location = new System.Drawing.Point(10, 52);
            this.ChkIsDate.Name = "ChkIsDate";
            this.ChkIsDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIsDate.Size = new System.Drawing.Size(151, 23);
            this.ChkIsDate.TabIndex = 1;
            this.ChkIsDate.Text = "Miti";
            this.ChkIsDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIsDate.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeSubledger
            // 
            this.ChkIncludeSubledger.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeSubledger.Location = new System.Drawing.Point(10, 78);
            this.ChkIncludeSubledger.Name = "ChkIncludeSubledger";
            this.ChkIncludeSubledger.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeSubledger.Size = new System.Drawing.Size(151, 23);
            this.ChkIncludeSubledger.TabIndex = 2;
            this.ChkIncludeSubledger.Text = "Sub Ledger";
            this.ChkIncludeSubledger.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeSubledger.UseVisualStyleBackColor = true;
            // 
            // ChkIncludeRemarks
            // 
            this.ChkIncludeRemarks.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeRemarks.Location = new System.Drawing.Point(168, 28);
            this.ChkIncludeRemarks.Name = "ChkIncludeRemarks";
            this.ChkIncludeRemarks.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ChkIncludeRemarks.Size = new System.Drawing.Size(151, 23);
            this.ChkIncludeRemarks.TabIndex = 3;
            this.ChkIncludeRemarks.Text = "Remarks";
            this.ChkIncludeRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIncludeRemarks.UseVisualStyleBackColor = true;
            // 
            // CmbDateType
            // 
            this.CmbDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbDateType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbDateType.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbDateType.FormattingEnabled = true;
            this.CmbDateType.Location = new System.Drawing.Point(5, 14);
            this.CmbDateType.Name = "CmbDateType";
            this.CmbDateType.Size = new System.Drawing.Size(241, 28);
            this.CmbDateType.TabIndex = 0;
            this.CmbDateType.SelectedIndexChanged += new System.EventHandler(this.CmbDateType_SelectedIndexChanged);
            // 
            // MskFrom
            // 
            this.MskFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskFrom.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskFrom.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskFrom.Location = new System.Drawing.Point(252, 15);
            this.MskFrom.Mask = "00/00/0000";
            this.MskFrom.Name = "MskFrom";
            this.MskFrom.Size = new System.Drawing.Size(129, 26);
            this.MskFrom.TabIndex = 1;
            // 
            // MskToDate
            // 
            this.MskToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskToDate.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskToDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskToDate.Location = new System.Drawing.Point(385, 15);
            this.MskToDate.Mask = "00/00/0000";
            this.MskToDate.Name = "MskToDate";
            this.MskToDate.Size = new System.Drawing.Size(122, 26);
            this.MskToDate.TabIndex = 2;
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.CmbDateType);
            this.mrGroup1.Controls.Add(this.MskFrom);
            this.mrGroup1.Controls.Add(this.MskToDate);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "";
            this.mrGroup1.Location = new System.Drawing.Point(2, -4);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 10;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = true;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(514, 50);
            this.mrGroup1.TabIndex = 1;
            // 
            // mrPanel1
            // 
            this.mrPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel1.Controls.Add(this.mrGroup2);
            this.mrPanel1.Controls.Add(this.mrGroup1);
            this.mrPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrPanel1.Location = new System.Drawing.Point(0, 0);
            this.mrPanel1.Name = "mrPanel1";
            this.mrPanel1.Size = new System.Drawing.Size(516, 231);
            this.mrPanel1.TabIndex = 5;
            // 
            // mrGroup2
            // 
            this.mrGroup2.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup2.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup2.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup2.BorderColor = System.Drawing.Color.White;
            this.mrGroup2.BorderThickness = 1F;
            this.mrGroup2.Controls.Add(this.TxtFilterValue);
            this.mrGroup2.Controls.Add(this.lbl_Find);
            this.mrGroup2.Controls.Add(this.BtnShow);
            this.mrGroup2.Controls.Add(this.BtnCancel);
            this.mrGroup2.Controls.Add(this.ChkNarration);
            this.mrGroup2.Controls.Add(this.ChkIsTFormat);
            this.mrGroup2.Controls.Add(this.ChkIncludeRemarks);
            this.mrGroup2.Controls.Add(this.ChkIsDate);
            this.mrGroup2.Controls.Add(this.ChkIncludeSubledger);
            this.mrGroup2.Controls.Add(this.mrGroup3);
            this.mrGroup2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup2.GroupImage = null;
            this.mrGroup2.GroupTitle = "Filter Value";
            this.mrGroup2.Location = new System.Drawing.Point(2, 47);
            this.mrGroup2.Name = "mrGroup2";
            this.mrGroup2.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup2.PaintGroupBox = false;
            this.mrGroup2.RoundCorners = 10;
            this.mrGroup2.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup2.ShadowControl = false;
            this.mrGroup2.ShadowThickness = 3;
            this.mrGroup2.Size = new System.Drawing.Size(510, 181);
            this.mrGroup2.TabIndex = 0;
            // 
            // mrGroup3
            // 
            this.mrGroup3.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup3.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup3.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup3.BorderColor = System.Drawing.Color.White;
            this.mrGroup3.BorderThickness = 1F;
            this.mrGroup3.Controls.Add(this.RbtnLocal);
            this.mrGroup3.Controls.Add(this.RbtnForeign);
            this.mrGroup3.Controls.Add(this.RbtnCurrencyBoth);
            this.mrGroup3.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup3.GroupImage = null;
            this.mrGroup3.GroupTitle = "";
            this.mrGroup3.Location = new System.Drawing.Point(7, 92);
            this.mrGroup3.Name = "mrGroup3";
            this.mrGroup3.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup3.PaintGroupBox = false;
            this.mrGroup3.RoundCorners = 10;
            this.mrGroup3.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup3.ShadowControl = true;
            this.mrGroup3.ShadowThickness = 3;
            this.mrGroup3.Size = new System.Drawing.Size(495, 48);
            this.mrGroup3.TabIndex = 5;
            // 
            // FrmJournalBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(516, 231);
            this.Controls.Add(this.mrPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmJournalBook";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "JOURNAL BOOK";
            this.Load += new System.EventHandler(this.FrmJournalBook_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmJournalBook_KeyPress);
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            this.mrPanel1.ResumeLayout(false);
            this.mrGroup2.ResumeLayout(false);
            this.mrGroup2.PerformLayout();
            this.mrGroup3.ResumeLayout(false);
            this.mrGroup3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnShow;
        private RadioButton RbtnCurrencyBoth;
        private RadioButton RbtnForeign;
        private RadioButton RbtnLocal;
        private Label lbl_Find;
        private CheckBox ChkIsTFormat;
        private CheckBox ChkIsDate;
        private CheckBox ChkIncludeSubledger;
        private CheckBox ChkIncludeRemarks;
        private ComboBox CmbDateType;
        private CheckBox ChkNarration;
        private MrTextBox TxtFilterValue;
        private MrMaskedTextBox MskFrom;
        private MrMaskedTextBox MskToDate;
        private MrGroup mrGroup1;
        private MrPanel mrPanel1;
        private MrGroup mrGroup2;
        private MrGroup mrGroup3;
    }
}