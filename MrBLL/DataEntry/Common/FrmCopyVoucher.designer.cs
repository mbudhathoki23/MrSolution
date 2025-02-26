using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.DataEntry.Common
{
    partial class FrmCopyVoucher
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
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_VoucherNo = new System.Windows.Forms.Button();
            this.txt_VoucherNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_VoucherNo = new System.Windows.Forms.Label();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.PanelHeader = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.panel5 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.panel4 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.PnlBorderHeaderTop = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.PnlBorderHeaderBottom = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.PanelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Ok
            // 
            this.btn_Ok.Location = new System.Drawing.Point(93, 40);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(76, 31);
            this.btn_Ok.TabIndex = 1;
            this.btn_Ok.Text = "&Save";
            this.btn_Ok.UseVisualStyleBackColor = false;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_VoucherNo
            // 
            this.btn_VoucherNo.Image = global::MrBLL.Properties.Resources.search16;
            this.btn_VoucherNo.Location = new System.Drawing.Point(224, 9);
            this.btn_VoucherNo.Name = "btn_VoucherNo";
            this.btn_VoucherNo.Size = new System.Drawing.Size(30, 23);
            this.btn_VoucherNo.TabIndex = 3;
            this.btn_VoucherNo.TabStop = false;
            this.btn_VoucherNo.UseVisualStyleBackColor = false;
            this.btn_VoucherNo.Click += new System.EventHandler(this.btn_VoucherNo_Click);
            // 
            // txt_VoucherNo
            // 
            this.txt_VoucherNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_VoucherNo.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_VoucherNo.Location = new System.Drawing.Point(90, 9);
            this.txt_VoucherNo.MaxLength = 50;
            this.txt_VoucherNo.Name = "txt_VoucherNo";
            this.txt_VoucherNo.Size = new System.Drawing.Size(132, 25);
            this.txt_VoucherNo.TabIndex = 0;
            this.txt_VoucherNo.Enter += new System.EventHandler(this.txt_VoucherNo_Enter);
            this.txt_VoucherNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_VoucherNo_KeyDown);
            this.txt_VoucherNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_VoucherNo_KeyPress);
            this.txt_VoucherNo.Leave += new System.EventHandler(this.txt_VoucherNo_Leave);
            this.txt_VoucherNo.Validated += new System.EventHandler(this.txt_VoucherNo_Validated);
            // 
            // lbl_VoucherNo
            // 
            this.lbl_VoucherNo.AutoSize = true;
            this.lbl_VoucherNo.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_VoucherNo.Location = new System.Drawing.Point(5, 11);
            this.lbl_VoucherNo.Name = "lbl_VoucherNo";
            this.lbl_VoucherNo.Size = new System.Drawing.Size(83, 17);
            this.lbl_VoucherNo.TabIndex = 6;
            this.lbl_VoucherNo.Text = "Voucher No";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(169, 40);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(76, 31);
            this.btn_Cancel.TabIndex = 2;
            this.btn_Cancel.Text = "&Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.clsSeparator1);
            this.PanelHeader.Controls.Add(this.btn_VoucherNo);
            this.PanelHeader.Controls.Add(this.btn_Cancel);
            this.PanelHeader.Controls.Add(this.btn_Ok);
            this.PanelHeader.Controls.Add(this.panel5);
            this.PanelHeader.Controls.Add(this.txt_VoucherNo);
            this.PanelHeader.Controls.Add(this.lbl_VoucherNo);
            this.PanelHeader.Controls.Add(this.panel4);
            this.PanelHeader.Controls.Add(this.PnlBorderHeaderTop);
            this.PanelHeader.Controls.Add(this.PnlBorderHeaderBottom);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(259, 79);
            this.PanelHeader.TabIndex = 0;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(8, 37);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(239, 2);
            this.clsSeparator1.TabIndex = 15;
            this.clsSeparator1.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(0, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(3, 73);
            this.panel5.TabIndex = 10;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(256, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(3, 73);
            this.panel4.TabIndex = 9;
            // 
            // PnlBorderHeaderTop
            // 
            this.PnlBorderHeaderTop.BackColor = System.Drawing.Color.White;
            this.PnlBorderHeaderTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlBorderHeaderTop.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlBorderHeaderTop.Location = new System.Drawing.Point(0, 0);
            this.PnlBorderHeaderTop.Name = "PnlBorderHeaderTop";
            this.PnlBorderHeaderTop.Size = new System.Drawing.Size(259, 3);
            this.PnlBorderHeaderTop.TabIndex = 8;
            // 
            // PnlBorderHeaderBottom
            // 
            this.PnlBorderHeaderBottom.BackColor = System.Drawing.Color.White;
            this.PnlBorderHeaderBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PnlBorderHeaderBottom.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlBorderHeaderBottom.Location = new System.Drawing.Point(0, 76);
            this.PnlBorderHeaderBottom.Name = "PnlBorderHeaderBottom";
            this.PnlBorderHeaderBottom.Size = new System.Drawing.Size(259, 3);
            this.PnlBorderHeaderBottom.TabIndex = 0;
            // 
            // FrmCopyVoucher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 79);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCopyVoucher";
            this.ShowIcon = false;
            this.Text = "Copy Voucher";
            this.Load += new System.EventHandler(this.FrmCopyVoucher_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmCopyVoucher_KeyPress);
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_VoucherNo;
        private System.Windows.Forms.Label lbl_VoucherNo;
        private System.Windows.Forms.Button btn_Cancel;
        private ClsSeparator clsSeparator1;
        private MrTextBox txt_VoucherNo;
        private MrPanel PanelHeader;
        private MrPanel panel5;
        private MrPanel panel4;
        private MrPanel PnlBorderHeaderTop;
        private MrPanel PnlBorderHeaderBottom;
    }
}