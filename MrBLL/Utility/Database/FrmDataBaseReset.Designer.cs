using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Utility.Database
{
    partial class FrmDataBaseReset
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
            this.StorePanel = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.chkAudit = new System.Windows.Forms.CheckBox();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.ckbSelectAll = new System.Windows.Forms.CheckBox();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnResetDatabase = new DevExpress.XtraEditors.SimpleButton();
            this.BtnReset = new DevExpress.XtraEditors.SimpleButton();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.ChkListEntry = new System.Windows.Forms.CheckedListBox();
            this.ChkListMaster = new System.Windows.Forms.CheckedListBox();
            this.StorePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.chkAudit);
            this.StorePanel.Controls.Add(this.clsSeparator2);
            this.StorePanel.Controls.Add(this.checkBox1);
            this.StorePanel.Controls.Add(this.ckbSelectAll);
            this.StorePanel.Controls.Add(this.BtnCancel);
            this.StorePanel.Controls.Add(this.BtnResetDatabase);
            this.StorePanel.Controls.Add(this.BtnReset);
            this.StorePanel.Controls.Add(this.clsSeparator1);
            this.StorePanel.Controls.Add(this.ChkListEntry);
            this.StorePanel.Controls.Add(this.ChkListMaster);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(563, 369);
            this.StorePanel.TabIndex = 2;
            // 
            // chkAudit
            // 
            this.chkAudit.AutoSize = true;
            this.chkAudit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAudit.ForeColor = System.Drawing.Color.Black;
            this.chkAudit.Location = new System.Drawing.Point(228, 303);
            this.chkAudit.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.chkAudit.Name = "chkAudit";
            this.chkAudit.Size = new System.Drawing.Size(120, 23);
            this.chkAudit.TabIndex = 18;
            this.chkAudit.Text = "Select Audit";
            this.chkAudit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAudit.UseVisualStyleBackColor = true;
            this.chkAudit.Visible = false;
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(5, 327);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(704, 2);
            this.clsSeparator2.TabIndex = 13;
            this.clsSeparator2.TabStop = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox1.ForeColor = System.Drawing.Color.Black;
            this.checkBox1.Location = new System.Drawing.Point(8, 303);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(194, 23);
            this.checkBox1.TabIndex = 17;
            this.checkBox1.Text = "Select All Transaction";
            this.checkBox1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.CkbSelectAll_CheckedChanged);
            // 
            // ckbSelectAll
            // 
            this.ckbSelectAll.AutoSize = true;
            this.ckbSelectAll.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbSelectAll.ForeColor = System.Drawing.Color.Black;
            this.ckbSelectAll.Location = new System.Drawing.Point(394, 302);
            this.ckbSelectAll.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ckbSelectAll.Name = "ckbSelectAll";
            this.ckbSelectAll.Size = new System.Drawing.Size(157, 23);
            this.ckbSelectAll.TabIndex = 16;
            this.ckbSelectAll.Text = "Select All Master";
            this.ckbSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbSelectAll.UseVisualStyleBackColor = true;
            this.ckbSelectAll.CheckedChanged += new System.EventHandler(this.CkbSelectAll_Master_CheckedChanged);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(101, 331);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(100, 33);
            this.BtnCancel.TabIndex = 15;
            this.BtnCancel.Text = "CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnResetDatabase
            // 
            this.BtnResetDatabase.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnResetDatabase.Appearance.Options.UseFont = true;
            this.BtnResetDatabase.ImageOptions.Image = global::MrBLL.Properties.Resources.Return;
            this.BtnResetDatabase.Location = new System.Drawing.Point(380, 331);
            this.BtnResetDatabase.Name = "BtnResetDatabase";
            this.BtnResetDatabase.Size = new System.Drawing.Size(175, 33);
            this.BtnResetDatabase.TabIndex = 14;
            this.BtnResetDatabase.Text = "RESET DATABASE";
            this.BtnResetDatabase.Click += new System.EventHandler(this.BtnResetDatabase_Click);
            // 
            // BtnReset
            // 
            this.BtnReset.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnReset.Appearance.Options.UseFont = true;
            this.BtnReset.ImageOptions.Image = global::MrBLL.Properties.Resources.Return;
            this.BtnReset.Location = new System.Drawing.Point(9, 331);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(92, 33);
            this.BtnReset.TabIndex = 13;
            this.BtnReset.Text = "RESET";
            this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(5, 298);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(703, 2);
            this.clsSeparator1.TabIndex = 12;
            this.clsSeparator1.TabStop = false;
            // 
            // ChkListEntry
            // 
            this.ChkListEntry.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkListEntry.FormattingEnabled = true;
            this.ChkListEntry.Items.AddRange(new object[] {
            "Opening"});
            this.ChkListEntry.Location = new System.Drawing.Point(5, 6);
            this.ChkListEntry.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ChkListEntry.Name = "ChkListEntry";
            this.ChkListEntry.Size = new System.Drawing.Size(283, 290);
            this.ChkListEntry.TabIndex = 11;
            // 
            // ChkListMaster
            // 
            this.ChkListMaster.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkListMaster.FormattingEnabled = true;
            this.ChkListMaster.Items.AddRange(new object[] {
            "Account Group"});
            this.ChkListMaster.Location = new System.Drawing.Point(298, 6);
            this.ChkListMaster.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ChkListMaster.Name = "ChkListMaster";
            this.ChkListMaster.Size = new System.Drawing.Size(257, 290);
            this.ChkListMaster.TabIndex = 10;
            // 
            // FrmDataBaseReset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 369);
            this.Controls.Add(this.StorePanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmDataBaseReset";
            this.ShowIcon = false;
            this.Text = "Reset DataBase";
            this.Load += new System.EventHandler(this.FrmDataBaseReset_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmDataBaseReset_KeyPress);
            this.StorePanel.ResumeLayout(false);
            this.StorePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckedListBox ChkListMaster;
        private System.Windows.Forms.CheckedListBox ChkListEntry;
        private ClsSeparator clsSeparator1;
        private DevExpress.XtraEditors.SimpleButton BtnReset;
        private DevExpress.XtraEditors.SimpleButton BtnResetDatabase;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private System.Windows.Forms.CheckBox ckbSelectAll;
        private System.Windows.Forms.CheckBox checkBox1;
        private ClsSeparator clsSeparator2;
        private System.Windows.Forms.CheckBox chkAudit;
        private MrPanel StorePanel;
    }
}