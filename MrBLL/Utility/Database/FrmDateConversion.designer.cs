using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Utility.Database
{
    partial class FrmDateConversion
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.lbl_To = new System.Windows.Forms.Label();
            this.msk_ToDate = new MrMaskedTextBox();
            this.lbl_ToDate = new System.Windows.Forms.Label();
            this.msk_FromDate = new MrMaskedTextBox();
            this.lbl_FromDate = new System.Windows.Forms.Label();
            this.lbl_MemoNo = new System.Windows.Forms.Label();
            this.btn_Save = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btn_Cancel);
            this.groupBox1.Controls.Add(this.lbl_To);
            this.groupBox1.Controls.Add(this.msk_ToDate);
            this.groupBox1.Controls.Add(this.lbl_ToDate);
            this.groupBox1.Controls.Add(this.msk_FromDate);
            this.groupBox1.Controls.Add(this.lbl_FromDate);
            this.groupBox1.Controls.Add(this.lbl_MemoNo);
            this.groupBox1.Controls.Add(this.btn_Save);
            this.groupBox1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(391, 143);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Date Conveter";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Location = new System.Drawing.Point(246, 107);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(74, 30);
            this.btn_Cancel.TabIndex = 6;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // lbl_To
            // 
            this.lbl_To.AutoSize = true;
            this.lbl_To.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_To.Location = new System.Drawing.Point(161, 64);
            this.lbl_To.Name = "lbl_To";
            this.lbl_To.Size = new System.Drawing.Size(65, 15);
            this.lbl_To.TabIndex = 183;
            this.lbl_To.Text = "Convert To";
            // 
            // msk_ToDate
            // 
            this.msk_ToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_ToDate.Font = new System.Drawing.Font("Arial", 9F);
            this.msk_ToDate.Location = new System.Drawing.Point(289, 61);
            this.msk_ToDate.Mask = "00/00/0000";
            this.msk_ToDate.Name = "msk_ToDate";
            this.msk_ToDate.Size = new System.Drawing.Size(95, 21);
            this.msk_ToDate.TabIndex = 2;
            this.msk_ToDate.TextChanged += new System.EventHandler(this.msk_ToDate_TextChanged);
            this.msk_ToDate.Enter += new System.EventHandler(this.msk_ToDate_Enter);
            this.msk_ToDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.msk_ToDate_KeyPress);
            this.msk_ToDate.Leave += new System.EventHandler(this.msk_ToDate_Leave);
            this.msk_ToDate.Validated += new System.EventHandler(this.msk_ToDate_Validated);
            // 
            // lbl_ToDate
            // 
            this.lbl_ToDate.AutoSize = true;
            this.lbl_ToDate.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_ToDate.Location = new System.Drawing.Point(243, 64);
            this.lbl_ToDate.Name = "lbl_ToDate";
            this.lbl_ToDate.Size = new System.Drawing.Size(33, 15);
            this.lbl_ToDate.TabIndex = 182;
            this.lbl_ToDate.Text = "Date";
            // 
            // msk_FromDate
            // 
            this.msk_FromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_FromDate.Font = new System.Drawing.Font("Arial", 9F);
            this.msk_FromDate.Location = new System.Drawing.Point(57, 60);
            this.msk_FromDate.Mask = "00/00/0000";
            this.msk_FromDate.Name = "msk_FromDate";
            this.msk_FromDate.Size = new System.Drawing.Size(95, 21);
            this.msk_FromDate.TabIndex = 1;
            this.msk_FromDate.TextChanged += new System.EventHandler(this.msk_FromDate_TextChanged);
            this.msk_FromDate.Enter += new System.EventHandler(this.msk_FromDate_Enter);
            this.msk_FromDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.msk_FromDate_KeyPress);
            this.msk_FromDate.Leave += new System.EventHandler(this.msk_FromDate_Leave);
            this.msk_FromDate.Validated += new System.EventHandler(this.msk_FromDate_Validated);
            // 
            // lbl_FromDate
            // 
            this.lbl_FromDate.AutoSize = true;
            this.lbl_FromDate.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_FromDate.Location = new System.Drawing.Point(9, 63);
            this.lbl_FromDate.Name = "lbl_FromDate";
            this.lbl_FromDate.Size = new System.Drawing.Size(33, 15);
            this.lbl_FromDate.TabIndex = 180;
            this.lbl_FromDate.Text = "Date";
            // 
            // lbl_MemoNo
            // 
            this.lbl_MemoNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MemoNo.Location = new System.Drawing.Point(93, 18);
            this.lbl_MemoNo.Name = "lbl_MemoNo";
            this.lbl_MemoNo.Size = new System.Drawing.Size(234, 22);
            this.lbl_MemoNo.TabIndex = 131;
            this.lbl_MemoNo.Text = "Convert Date AD To BS Or BS To AD";
            // 
            // btn_Save
            // 
            this.btn_Save.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.Location = new System.Drawing.Point(166, 107);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(74, 30);
            this.btn_Save.TabIndex = 5;
            this.btn_Save.Text = "Ok";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // FrmDateConversion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 156);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDateConversion";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Date Conversion";
            this.Load += new System.EventHandler(this.FrmDateConversion_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmDateConversion_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Label lbl_MemoNo;
        private System.Windows.Forms.Label lbl_To;
        private System.Windows.Forms.MaskedTextBox msk_ToDate;
        private System.Windows.Forms.Label lbl_ToDate;
        private System.Windows.Forms.MaskedTextBox msk_FromDate;
        private System.Windows.Forms.Label lbl_FromDate;
        private System.Windows.Forms.Button btn_Cancel;
    }
}