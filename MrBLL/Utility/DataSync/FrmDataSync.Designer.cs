namespace MrBLL.Utility.DataSync
{
    partial class FrmDataSync
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnImport = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSync = new DevExpress.XtraEditors.SimpleButton();
            this.btnCheckUpdates = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
            this.chkMarkAll = new DevExpress.XtraEditors.CheckEdit();
            this.clbSyncItems = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.flpDataContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.progressBar = new DevExpress.XtraEditors.ProgressBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl.Panel1)).BeginInit();
            this.splitContainerControl.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl.Panel2)).BeginInit();
            this.splitContainerControl.Panel2.SuspendLayout();
            this.splitContainerControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkMarkAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clbSyncItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnImport);
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Controls.Add(this.btnSync);
            this.panelControl1.Controls.Add(this.btnCheckUpdates);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 518);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1140, 49);
            this.panelControl1.TabIndex = 0;
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.Location = new System.Drawing.Point(413, 8);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(120, 32);
            this.btnImport.TabIndex = 10;
            this.btnImport.Text = "Import";
            this.btnImport.Visible = false;
            this.btnImport.Click += new System.EventHandler(this.BtnImport_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(1009, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 32);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnSync
            // 
            this.btnSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSync.Location = new System.Drawing.Point(882, 8);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(120, 32);
            this.btnSync.TabIndex = 8;
            this.btnSync.Text = "Sync";
            this.btnSync.Click += new System.EventHandler(this.BtnSync_Click);
            // 
            // btnCheckUpdates
            // 
            this.btnCheckUpdates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheckUpdates.Location = new System.Drawing.Point(756, 8);
            this.btnCheckUpdates.Name = "btnCheckUpdates";
            this.btnCheckUpdates.Size = new System.Drawing.Size(120, 32);
            this.btnCheckUpdates.TabIndex = 5;
            this.btnCheckUpdates.Text = "Check Updates";
            this.btnCheckUpdates.Visible = false;
            this.btnCheckUpdates.Click += new System.EventHandler(this.BtnCheckUpdates_Click);
            // 
            // splitContainerControl
            // 
            this.splitContainerControl.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel1;
            this.splitContainerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl.Location = new System.Drawing.Point(0, 45);
            this.splitContainerControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainerControl.Name = "splitContainerControl";
            // 
            // splitContainerControl.Panel1
            // 
            this.splitContainerControl.Panel1.Controls.Add(this.chkMarkAll);
            this.splitContainerControl.Panel1.Controls.Add(this.clbSyncItems);
            this.splitContainerControl.Panel1.Controls.Add(this.labelControl1);
            this.splitContainerControl.Panel1.Text = "Panel1";
            // 
            // splitContainerControl.Panel2
            // 
            this.splitContainerControl.Panel2.Controls.Add(this.flpDataContainer);
            this.splitContainerControl.Panel2.Text = "Panel2";
            this.splitContainerControl.Size = new System.Drawing.Size(1140, 455);
            this.splitContainerControl.SplitterPosition = 236;
            this.splitContainerControl.TabIndex = 1;
            // 
            // chkMarkAll
            // 
            this.chkMarkAll.Location = new System.Drawing.Point(3, 1);
            this.chkMarkAll.Name = "chkMarkAll";
            this.chkMarkAll.Properties.Caption = "";
            this.chkMarkAll.Size = new System.Drawing.Size(23, 20);
            this.chkMarkAll.TabIndex = 2;
            this.chkMarkAll.CheckedChanged += new System.EventHandler(this.ChkMarkAll_CheckedChanged);
            // 
            // clbSyncItems
            // 
            this.clbSyncItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbSyncItems.Location = new System.Drawing.Point(0, 21);
            this.clbSyncItems.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.clbSyncItems.Name = "clbSyncItems";
            this.clbSyncItems.Size = new System.Drawing.Size(236, 434);
            this.clbSyncItems.TabIndex = 0;
            this.clbSyncItems.ItemChecking += new DevExpress.XtraEditors.Controls.ItemCheckingEventHandler(this.ClbSyncItems_ItemChecking);
            this.clbSyncItems.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.ClbSyncItems_ItemCheck);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelControl1.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.labelControl1.Appearance.Options.UseBackColor = true;
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelControl1.Location = new System.Drawing.Point(0, 0);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(236, 21);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Mark items to sync";
            this.labelControl1.Click += new System.EventHandler(this.labelControl1_Click);
            // 
            // flpDataContainer
            // 
            this.flpDataContainer.AutoScroll = true;
            this.flpDataContainer.BackColor = System.Drawing.Color.Transparent;
            this.flpDataContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpDataContainer.Location = new System.Drawing.Point(0, 0);
            this.flpDataContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flpDataContainer.Name = "flpDataContainer";
            this.flpDataContainer.Size = new System.Drawing.Size(894, 455);
            this.flpDataContainer.TabIndex = 0;
            // 
            // panelControl2
            // 
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1140, 45);
            this.panelControl2.TabIndex = 2;
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(0, 500);
            this.progressBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1140, 18);
            this.progressBar.TabIndex = 9;
            // 
            // FrmDataSync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1140, 567);
            this.Controls.Add(this.splitContainerControl);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmDataSync";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data Sync Wizard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmDataSync_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmDataSync_FormClosed);
            this.Load += new System.EventHandler(this.FrmDataSync_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl.Panel1)).EndInit();
            this.splitContainerControl.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl.Panel2)).EndInit();
            this.splitContainerControl.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
            this.splitContainerControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkMarkAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clbSyncItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.CheckedListBoxControl clbSyncItems;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnSync;
        private DevExpress.XtraEditors.SimpleButton btnCheckUpdates;
        private DevExpress.XtraEditors.ProgressBarControl progressBar;
        private System.Windows.Forms.FlowLayoutPanel flpDataContainer;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnImport;
        private DevExpress.XtraEditors.CheckEdit chkMarkAll;
    }
}