using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Utility.Common
{
    partial class XFrmRecalculate
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
            this.ChkListFinance = new System.Windows.Forms.CheckedListBox();
            this.ChkInventory = new System.Windows.Forms.CheckBox();
            this.ChkFinance = new System.Windows.Forms.CheckBox();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Recalculate = new DevExpress.XtraEditors.SimpleButton();
            this.ckbUnSellectAll = new System.Windows.Forms.CheckBox();
            this.ckbSelectAll = new System.Windows.Forms.CheckBox();
            this.ChkListInventory = new System.Windows.Forms.CheckedListBox();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparator3 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparatorH1 = new MrDAL.Control.ControlsEx.Control.ClsSeparatorH();
            this.clsSeparator4 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparator5 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.PanelHeader = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.PanelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChkListFinance
            // 
            this.ChkListFinance.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ChkListFinance.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkListFinance.FormattingEnabled = true;
            this.ChkListFinance.Location = new System.Drawing.Point(7, 32);
            this.ChkListFinance.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ChkListFinance.Name = "ChkListFinance";
            this.ChkListFinance.ScrollAlwaysVisible = true;
            this.ChkListFinance.Size = new System.Drawing.Size(242, 224);
            this.ChkListFinance.TabIndex = 13;
            // 
            // ChkInventory
            // 
            this.ChkInventory.AutoSize = true;
            this.ChkInventory.ForeColor = System.Drawing.Color.Black;
            this.ChkInventory.Location = new System.Drawing.Point(278, 6);
            this.ChkInventory.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ChkInventory.Name = "ChkInventory";
            this.ChkInventory.Size = new System.Drawing.Size(166, 24);
            this.ChkInventory.TabIndex = 1;
            this.ChkInventory.Text = "Inventory Module";
            this.ChkInventory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkInventory.UseVisualStyleBackColor = true;
            this.ChkInventory.CheckedChanged += new System.EventHandler(this.CmbInventory_CheckedChanged);
            // 
            // ChkFinance
            // 
            this.ChkFinance.AutoSize = true;
            this.ChkFinance.ForeColor = System.Drawing.Color.Black;
            this.ChkFinance.Location = new System.Drawing.Point(11, 6);
            this.ChkFinance.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ChkFinance.Name = "ChkFinance";
            this.ChkFinance.Size = new System.Drawing.Size(155, 24);
            this.ChkFinance.TabIndex = 0;
            this.ChkFinance.Text = "Finance Module";
            this.ChkFinance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkFinance.UseVisualStyleBackColor = true;
            this.ChkFinance.CheckedChanged += new System.EventHandler(this.CmbFinance_CheckedChanged);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Appearance.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btn_Cancel.Appearance.Options.UseFont = true;
            this.btn_Cancel.Appearance.Options.UseForeColor = true;
            this.btn_Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_Cancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(359, 264);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(106, 38);
            this.btn_Cancel.TabIndex = 3;
            this.btn_Cancel.Text = "CANCEL";
            this.btn_Cancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btn_Recalculate
            // 
            this.btn_Recalculate.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Recalculate.Appearance.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btn_Recalculate.Appearance.Options.UseBackColor = true;
            this.btn_Recalculate.Appearance.Options.UseFont = true;
            this.btn_Recalculate.Appearance.Options.UseForeColor = true;
            this.btn_Recalculate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_Recalculate.ImageOptions.Image = global::MrBLL.Properties.Resources.Return;
            this.btn_Recalculate.Location = new System.Drawing.Point(252, 264);
            this.btn_Recalculate.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_Recalculate.Name = "btn_Recalculate";
            this.btn_Recalculate.Size = new System.Drawing.Size(107, 38);
            this.btn_Recalculate.TabIndex = 2;
            this.btn_Recalculate.Text = "REPOST";
            this.btn_Recalculate.Click += new System.EventHandler(this.BtnRecalculate_Click);
            // 
            // ckbUnSellectAll
            // 
            this.ckbUnSellectAll.AutoSize = true;
            this.ckbUnSellectAll.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbUnSellectAll.ForeColor = System.Drawing.Color.Black;
            this.ckbUnSellectAll.Location = new System.Drawing.Point(115, 271);
            this.ckbUnSellectAll.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ckbUnSellectAll.Name = "ckbUnSellectAll";
            this.ckbUnSellectAll.Size = new System.Drawing.Size(130, 24);
            this.ckbUnSellectAll.TabIndex = 1;
            this.ckbUnSellectAll.Text = "UnSellect All";
            this.ckbUnSellectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbUnSellectAll.UseVisualStyleBackColor = true;
            this.ckbUnSellectAll.Visible = false;
            this.ckbUnSellectAll.CheckedChanged += new System.EventHandler(this.CkbUnSelectAll_CheckedChanged);
            // 
            // ckbSelectAll
            // 
            this.ckbSelectAll.AutoSize = true;
            this.ckbSelectAll.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbSelectAll.ForeColor = System.Drawing.Color.Black;
            this.ckbSelectAll.Location = new System.Drawing.Point(6, 271);
            this.ckbSelectAll.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ckbSelectAll.Name = "ckbSelectAll";
            this.ckbSelectAll.Size = new System.Drawing.Size(102, 24);
            this.ckbSelectAll.TabIndex = 0;
            this.ckbSelectAll.Text = "Select All";
            this.ckbSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbSelectAll.UseVisualStyleBackColor = true;
            this.ckbSelectAll.CheckedChanged += new System.EventHandler(this.CkbSelectAll_CheckedChanged);
            // 
            // ChkListInventory
            // 
            this.ChkListInventory.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ChkListInventory.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkListInventory.FormattingEnabled = true;
            this.ChkListInventory.Location = new System.Drawing.Point(260, 32);
            this.ChkListInventory.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ChkListInventory.Name = "ChkListInventory";
            this.ChkListInventory.ScrollAlwaysVisible = true;
            this.ChkListInventory.Size = new System.Drawing.Size(242, 224);
            this.ChkListInventory.TabIndex = 14;
            this.ChkListInventory.ThreeDCheckBoxes = true;
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(299, -54);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(200, 2);
            this.clsSeparator2.TabIndex = 43;
            this.clsSeparator2.TabStop = false;
            // 
            // clsSeparator3
            // 
            this.clsSeparator3.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator3.Location = new System.Drawing.Point(12, 27);
            this.clsSeparator3.Name = "clsSeparator3";
            this.clsSeparator3.Size = new System.Drawing.Size(486, 2);
            this.clsSeparator3.TabIndex = 44;
            this.clsSeparator3.TabStop = false;
            // 
            // clsSeparatorH1
            // 
            this.clsSeparatorH1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparatorH1.Location = new System.Drawing.Point(254, 32);
            this.clsSeparatorH1.Name = "clsSeparatorH1";
            this.clsSeparatorH1.Size = new System.Drawing.Size(3, 224);
            this.clsSeparatorH1.TabIndex = 45;
            this.clsSeparatorH1.TabStop = false;
            // 
            // clsSeparator4
            // 
            this.clsSeparator4.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator4.Location = new System.Drawing.Point(8, 259);
            this.clsSeparator4.Name = "clsSeparator4";
            this.clsSeparator4.Size = new System.Drawing.Size(490, 2);
            this.clsSeparator4.TabIndex = 45;
            this.clsSeparator4.TabStop = false;
            // 
            // clsSeparator5
            // 
            this.clsSeparator5.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator5.Location = new System.Drawing.Point(6, 303);
            this.clsSeparator5.Name = "clsSeparator5";
            this.clsSeparator5.Size = new System.Drawing.Size(496, 2);
            this.clsSeparator5.TabIndex = 46;
            this.clsSeparator5.TabStop = false;
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.clsSeparator5);
            this.PanelHeader.Controls.Add(this.btn_Cancel);
            this.PanelHeader.Controls.Add(this.clsSeparator4);
            this.PanelHeader.Controls.Add(this.btn_Recalculate);
            this.PanelHeader.Controls.Add(this.clsSeparatorH1);
            this.PanelHeader.Controls.Add(this.ckbUnSellectAll);
            this.PanelHeader.Controls.Add(this.ckbSelectAll);
            this.PanelHeader.Controls.Add(this.clsSeparator3);
            this.PanelHeader.Controls.Add(this.ChkFinance);
            this.PanelHeader.Controls.Add(this.ChkListFinance);
            this.PanelHeader.Controls.Add(this.ChkInventory);
            this.PanelHeader.Controls.Add(this.ChkListInventory);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(507, 311);
            this.PanelHeader.TabIndex = 47;
            // 
            // XFrmRecalculate
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Appearance.BackColor2 = System.Drawing.SystemColors.ActiveCaption;
            this.Appearance.FontStyleDelta = System.Drawing.FontStyle.Underline;
            this.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(507, 311);
            this.Controls.Add(this.PanelHeader);
            this.Controls.Add(this.clsSeparator2);
            this.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Underline);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Glow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.IconOptions.ShowIcon = false;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XFrmRecalculate";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RECALCULATE VALUE";
            this.Load += new System.EventHandler(this.XFrmRecalculate_Load);
            this.Shown += new System.EventHandler(this.XFrmRecalculate_Shown);
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox ChkListFinance;
        private System.Windows.Forms.CheckBox ChkInventory;
        private System.Windows.Forms.CheckBox ChkFinance;
        private DevExpress.XtraEditors.SimpleButton btn_Cancel;
        private DevExpress.XtraEditors.SimpleButton btn_Recalculate;
        private System.Windows.Forms.CheckBox ckbUnSellectAll;
        private System.Windows.Forms.CheckBox ckbSelectAll;
        private System.Windows.Forms.CheckedListBox ChkListInventory;
        private ClsSeparator clsSeparator2;
        private ClsSeparator clsSeparator3;
        private ClsSeparatorH clsSeparatorH1;
        private ClsSeparator clsSeparator4;
        private ClsSeparator clsSeparator5;
        private MrPanel PanelHeader;
    }
}