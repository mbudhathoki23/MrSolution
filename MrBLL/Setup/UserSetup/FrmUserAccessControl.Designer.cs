using System.Windows.Forms;
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Setup.UserSetup
{
    partial class FrmUserAccessControl
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvFeatures = new TreeViewEx();
            this.lbRoles = new System.Windows.Forms.ListBox();
            this.clbFeatureActions = new System.Windows.Forms.CheckedListBox();
            this.mrPanel1 = new MrPanel();
            this.btnApplyAction = new DevExpress.XtraEditors.SimpleButton();
            this.btnResetActions = new DevExpress.XtraEditors.SimpleButton();
            this.panel3 = new MrPanel();
            this.rdoSelectedActions = new System.Windows.Forms.RadioButton();
            this.rdoAllActions = new System.Windows.Forms.RadioButton();
            this.panel2 = new MrPanel();
            this.CmbBranch = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new MrPanel();
            this.btnExpandCollapseTree = new DevExpress.XtraEditors.SimpleButton();
            this.BtnTreeCheckNone = new DevExpress.XtraEditors.SimpleButton();
            this.BtnTreeCheckAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tvExistingActions = new TreeViewEx();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.mrPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 44);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvFeatures);
            this.splitContainer1.Panel1.Controls.Add(this.lbRoles);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl);
            this.splitContainer1.Size = new System.Drawing.Size(914, 390);
            this.splitContainer1.SplitterDistance = 522;
            this.splitContainer1.TabIndex = 4;
            // 
            // tvFeatures
            // 
            this.tvFeatures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvFeatures.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.tvFeatures.HideSelection = false;
            this.tvFeatures.Location = new System.Drawing.Point(181, 0);
            this.tvFeatures.Name = "tvFeatures";
            this.tvFeatures.Size = new System.Drawing.Size(341, 390);
            this.tvFeatures.TabIndex = 0;
            this.tvFeatures.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TvFeatures_AfterSelect);
            // 
            // lbRoles
            // 
            this.lbRoles.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbRoles.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRoles.FormattingEnabled = true;
            this.lbRoles.IntegralHeight = false;
            this.lbRoles.ItemHeight = 19;
            this.lbRoles.Location = new System.Drawing.Point(0, 0);
            this.lbRoles.Name = "lbRoles";
            this.lbRoles.Size = new System.Drawing.Size(181, 390);
            this.lbRoles.TabIndex = 1;
            this.lbRoles.SelectedIndexChanged += new System.EventHandler(this.lbRoles_SelectedIndexChanged);
            // 
            // clbFeatureActions
            // 
            this.clbFeatureActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbFeatureActions.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.clbFeatureActions.FormattingEnabled = true;
            this.clbFeatureActions.IntegralHeight = false;
            this.clbFeatureActions.Location = new System.Drawing.Point(3, 35);
            this.clbFeatureActions.Name = "clbFeatureActions";
            this.clbFeatureActions.Size = new System.Drawing.Size(374, 290);
            this.clbFeatureActions.TabIndex = 0;
            // 
            // mrPanel1
            // 
            this.mrPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel1.Controls.Add(this.btnApplyAction);
            this.mrPanel1.Controls.Add(this.btnResetActions);
            this.mrPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mrPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.mrPanel1.Location = new System.Drawing.Point(3, 325);
            this.mrPanel1.Name = "mrPanel1";
            this.mrPanel1.Size = new System.Drawing.Size(374, 36);
            this.mrPanel1.TabIndex = 5;
            // 
            // btnApplyAction
            // 
            this.btnApplyAction.Location = new System.Drawing.Point(5, 7);
            this.btnApplyAction.Name = "btnApplyAction";
            this.btnApplyAction.Size = new System.Drawing.Size(70, 23);
            this.btnApplyAction.TabIndex = 0;
            this.btnApplyAction.Text = "&Apply Action";
            this.btnApplyAction.Click += new System.EventHandler(this.btnApplyAction_Click);
            // 
            // btnResetActions
            // 
            this.btnResetActions.Location = new System.Drawing.Point(81, 7);
            this.btnResetActions.Name = "btnResetActions";
            this.btnResetActions.Size = new System.Drawing.Size(87, 23);
            this.btnResetActions.TabIndex = 0;
            this.btnResetActions.Text = "&Reset Actions";
            this.btnResetActions.Click += new System.EventHandler(this.btnResetActions_Click_1);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel3.Controls.Add(this.rdoSelectedActions);
            this.panel3.Controls.Add(this.rdoAllActions);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(374, 32);
            this.panel3.TabIndex = 4;
            // 
            // rdoSelectedActions
            // 
            this.rdoSelectedActions.AutoSize = true;
            this.rdoSelectedActions.Checked = true;
            this.rdoSelectedActions.Location = new System.Drawing.Point(118, 4);
            this.rdoSelectedActions.Name = "rdoSelectedActions";
            this.rdoSelectedActions.Size = new System.Drawing.Size(152, 23);
            this.rdoSelectedActions.TabIndex = 1;
            this.rdoSelectedActions.TabStop = true;
            this.rdoSelectedActions.Text = "Selected Actions";
            this.rdoSelectedActions.UseVisualStyleBackColor = true;
            this.rdoSelectedActions.CheckedChanged += new System.EventHandler(this.RdoActions_CheckedChanged);
            // 
            // rdoAllActions
            // 
            this.rdoAllActions.AutoSize = true;
            this.rdoAllActions.Location = new System.Drawing.Point(5, 4);
            this.rdoAllActions.Name = "rdoAllActions";
            this.rdoAllActions.Size = new System.Drawing.Size(107, 23);
            this.rdoAllActions.TabIndex = 1;
            this.rdoAllActions.Text = "All Actions";
            this.rdoAllActions.UseVisualStyleBackColor = true;
            this.rdoAllActions.CheckedChanged += new System.EventHandler(this.RdoActions_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel2.Controls.Add(this.CmbBranch);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(914, 44);
            this.panel2.TabIndex = 3;
            // 
            // CmbBranch
            // 
            this.CmbBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbBranch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbBranch.FormattingEnabled = true;
            this.CmbBranch.Location = new System.Drawing.Point(87, 12);
            this.CmbBranch.Name = "CmbBranch";
            this.CmbBranch.Size = new System.Drawing.Size(356, 27);
            this.CmbBranch.TabIndex = 1;
            this.CmbBranch.SelectedIndexChanged += new System.EventHandler(this.cbxBranches_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "BRANCH";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.btnExpandCollapseTree);
            this.panel1.Controls.Add(this.BtnTreeCheckNone);
            this.panel1.Controls.Add(this.BtnTreeCheckAll);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 434);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(914, 48);
            this.panel1.TabIndex = 1;
            // 
            // btnExpandCollapseTree
            // 
            this.btnExpandCollapseTree.Location = new System.Drawing.Point(335, 13);
            this.btnExpandCollapseTree.Name = "btnExpandCollapseTree";
            this.btnExpandCollapseTree.Size = new System.Drawing.Size(133, 23);
            this.btnExpandCollapseTree.TabIndex = 0;
            this.btnExpandCollapseTree.Text = "Expand-Collapse";
            this.btnExpandCollapseTree.Click += new System.EventHandler(this.BtnExpandCollapseTree_Click);
            // 
            // BtnTreeCheckNone
            // 
            this.BtnTreeCheckNone.Location = new System.Drawing.Point(259, 13);
            this.BtnTreeCheckNone.Name = "BtnTreeCheckNone";
            this.BtnTreeCheckNone.Size = new System.Drawing.Size(70, 23);
            this.BtnTreeCheckNone.TabIndex = 0;
            this.BtnTreeCheckNone.Text = "Check &None";
            this.BtnTreeCheckNone.Click += new System.EventHandler(this.BtnTreeCheckNone_Click);
            // 
            // BtnTreeCheckAll
            // 
            this.BtnTreeCheckAll.Location = new System.Drawing.Point(183, 13);
            this.BtnTreeCheckAll.Name = "BtnTreeCheckAll";
            this.BtnTreeCheckAll.Size = new System.Drawing.Size(70, 23);
            this.BtnTreeCheckAll.TabIndex = 0;
            this.BtnTreeCheckAll.Text = "&Check All";
            this.BtnTreeCheckAll.Click += new System.EventHandler(this.BtnTreeCheckAll_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(662, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(117, 32);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(785, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(117, 32);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(388, 390);
            this.tabControl.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.clbFeatureActions);
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.mrPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(380, 364);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Actions Setup";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tvExistingActions);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(380, 364);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Existing Action Permissions";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tvExistingActions
            // 
            this.tvExistingActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvExistingActions.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.tvExistingActions.HideSelection = false;
            this.tvExistingActions.Location = new System.Drawing.Point(3, 3);
            this.tvExistingActions.Name = "tvExistingActions";
            this.tvExistingActions.Size = new System.Drawing.Size(374, 358);
            this.tvExistingActions.TabIndex = 1;
            // 
            // FrmUserAccessControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 482);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "FrmUserAccessControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Access Control";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmEnableMenu_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.mrPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private System.Windows.Forms.CheckedListBox clbFeatureActions;
        private System.Windows.Forms.RadioButton rdoSelectedActions;
        private System.Windows.Forms.RadioButton rdoAllActions;
        private TreeViewEx tvFeatures;
        private System.Windows.Forms.ListBox lbRoles;
        private System.Windows.Forms.ComboBox CmbBranch;
        private System.Windows.Forms.Label label1;
        private MrPanel panel1;
        private MrPanel panel2;
        private MrPanel panel3;
        private DevExpress.XtraEditors.SimpleButton btnResetActions;
        private DevExpress.XtraEditors.SimpleButton btnApplyAction;
        private DevExpress.XtraEditors.SimpleButton BtnTreeCheckNone;
        private DevExpress.XtraEditors.SimpleButton BtnTreeCheckAll;
        private DevExpress.XtraEditors.SimpleButton btnExpandCollapseTree;
        private MrPanel mrPanel1;
        private TabControl tabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TreeViewEx tvExistingActions;
    }
}