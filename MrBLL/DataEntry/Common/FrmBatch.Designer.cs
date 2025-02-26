using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.DataEntry.Common
{
    partial class FrmBatch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBatch));
            this.PanelHeader = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.MskEXPMiti = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.MskEXPDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.MskMFGMiti = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.MskMFGDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.TxtBatchNo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.PanelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.btnClear);
            this.PanelHeader.Controls.Add(this.btnSave);
            this.PanelHeader.Controls.Add(this.clsSeparator1);
            this.PanelHeader.Controls.Add(this.label3);
            this.PanelHeader.Controls.Add(this.label2);
            this.PanelHeader.Controls.Add(this.label1);
            this.PanelHeader.Controls.Add(this.MskEXPMiti);
            this.PanelHeader.Controls.Add(this.MskEXPDate);
            this.PanelHeader.Controls.Add(this.MskMFGMiti);
            this.PanelHeader.Controls.Add(this.MskMFGDate);
            this.PanelHeader.Controls.Add(this.TxtBatchNo);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(310, 134);
            this.PanelHeader.TabIndex = 0;
            // 
            // btnClear
            // 
            this.btnClear.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.btnClear.Location = new System.Drawing.Point(198, 96);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(102, 35);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "&CANCEL";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.btnSave.Location = new System.Drawing.Point(131, 96);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(67, 35);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "&OK";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(5, 92);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(300, 2);
            this.clsSeparator1.TabIndex = 19;
            this.clsSeparator1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "EXP Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 17;
            this.label2.Text = "MFG Date";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 20);
            this.label1.TabIndex = 16;
            this.label1.Text = "Batch No";
            // 
            // MskEXPMiti
            // 
            this.MskEXPMiti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskEXPMiti.Enabled = false;
            this.MskEXPMiti.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.MskEXPMiti.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskEXPMiti.Location = new System.Drawing.Point(206, 64);
            this.MskEXPMiti.Mask = "00/00/0000";
            this.MskEXPMiti.Name = "MskEXPMiti";
            this.MskEXPMiti.Size = new System.Drawing.Size(100, 24);
            this.MskEXPMiti.TabIndex = 4;
            this.MskEXPMiti.Enter += new System.EventHandler(this.MskEXPMiti_Enter);
            this.MskEXPMiti.Leave += new System.EventHandler(this.MskEXPMiti_Leave);
            this.MskEXPMiti.Validating += new System.ComponentModel.CancelEventHandler(this.MskEXPMiti_Validating);
            // 
            // MskEXPDate
            // 
            this.MskEXPDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskEXPDate.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskEXPDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskEXPDate.Location = new System.Drawing.Point(104, 64);
            this.MskEXPDate.Mask = "00/00/0000";
            this.MskEXPDate.Name = "MskEXPDate";
            this.MskEXPDate.Size = new System.Drawing.Size(100, 25);
            this.MskEXPDate.TabIndex = 3;
            this.MskEXPDate.Enter += new System.EventHandler(this.MskEXPDate_Enter);
            this.MskEXPDate.Leave += new System.EventHandler(this.MskEXPDate_Leave);
            this.MskEXPDate.Validating += new System.ComponentModel.CancelEventHandler(this.MskEXPDate_Validating);
            // 
            // MskMFGMiti
            // 
            this.MskMFGMiti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskMFGMiti.Enabled = false;
            this.MskMFGMiti.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.MskMFGMiti.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskMFGMiti.Location = new System.Drawing.Point(206, 36);
            this.MskMFGMiti.Mask = "00/00/0000";
            this.MskMFGMiti.Name = "MskMFGMiti";
            this.MskMFGMiti.Size = new System.Drawing.Size(100, 24);
            this.MskMFGMiti.TabIndex = 2;
            this.MskMFGMiti.Enter += new System.EventHandler(this.MskMFGMiti_Enter);
            this.MskMFGMiti.Leave += new System.EventHandler(this.MskMFGMiti_Leave);
            this.MskMFGMiti.Validating += new System.ComponentModel.CancelEventHandler(this.MskMFGMiti_Validating);
            // 
            // MskMFGDate
            // 
            this.MskMFGDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskMFGDate.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskMFGDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskMFGDate.Location = new System.Drawing.Point(104, 36);
            this.MskMFGDate.Mask = "00/00/0000";
            this.MskMFGDate.Name = "MskMFGDate";
            this.MskMFGDate.Size = new System.Drawing.Size(100, 25);
            this.MskMFGDate.TabIndex = 1;
            this.MskMFGDate.Enter += new System.EventHandler(this.MskMFGDate_Enter);
            this.MskMFGDate.Leave += new System.EventHandler(this.MskMFGDate_Leave);
            this.MskMFGDate.Validating += new System.ComponentModel.CancelEventHandler(this.MskMFGDate_Validating);
            // 
            // TxtBatchNo
            // 
            this.TxtBatchNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBatchNo.Location = new System.Drawing.Point(104, 7);
            this.TxtBatchNo.MaxLength = 50;
            this.TxtBatchNo.Name = "TxtBatchNo";
            this.TxtBatchNo.Size = new System.Drawing.Size(203, 26);
            this.TxtBatchNo.TabIndex = 0;
            this.TxtBatchNo.Enter += new System.EventHandler(this.TxtBatchNo_Enter);
            this.TxtBatchNo.Leave += new System.EventHandler(this.TxtBatchNo_Leave);
            this.TxtBatchNo.Validating += new System.ComponentModel.CancelEventHandler(this.TxtBatchNo_Validating);
            // 
            // FrmBatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 134);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBatch";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Product Details";
            this.Load += new System.EventHandler(this.FrmBatch_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmBatch_KeyPress);
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ClsSeparator clsSeparator1;
        public DevExpress.XtraEditors.SimpleButton btnClear;
        public DevExpress.XtraEditors.SimpleButton btnSave;
        private MrPanel PanelHeader;
        private MrTextBox TxtBatchNo;
        public MrMaskedTextBox MskEXPMiti;
        private MrMaskedTextBox MskEXPDate;
        public MrMaskedTextBox MskMFGMiti;
        private MrMaskedTextBox MskMFGDate;
    }
}