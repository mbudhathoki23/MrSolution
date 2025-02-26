using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.POS.Master
{
    partial class FrmStoreCustomer
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
            this.roundPanel1 = new RoundPanel();
            this.TxtBillAmount = new MrTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.TxtPhoneNo = new MrTextBox();
            this.BtnPartyName = new System.Windows.Forms.Button();
            this.btnCustomer = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.TxtBalance = new MrTextBox();
            this.ChkPrint = new System.Windows.Forms.CheckBox();
            this.TxtCustomer = new MrTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSavePrint = new DevExpress.XtraEditors.SimpleButton();
            this.TxtAdvance = new MrTextBox();
            this.TxtChangeAmount = new MrTextBox();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.clsSeparator1 = new ClsSeparator();
            this.TxtTenderAmount = new MrTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtPanNo = new MrTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtAddress = new MrTextBox();
            this.TxtPrintingDesc = new MrTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.roundPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // roundPanel1
            // 
            this.roundPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.roundPanel1.Controls.Add(this.TxtBillAmount);
            this.roundPanel1.Controls.Add(this.label10);
            this.roundPanel1.Controls.Add(this.TxtPhoneNo);
            this.roundPanel1.Controls.Add(this.BtnPartyName);
            this.roundPanel1.Controls.Add(this.btnCustomer);
            this.roundPanel1.Controls.Add(this.label9);
            this.roundPanel1.Controls.Add(this.TxtBalance);
            this.roundPanel1.Controls.Add(this.ChkPrint);
            this.roundPanel1.Controls.Add(this.TxtCustomer);
            this.roundPanel1.Controls.Add(this.label8);
            this.roundPanel1.Controls.Add(this.btnSave);
            this.roundPanel1.Controls.Add(this.BtnSavePrint);
            this.roundPanel1.Controls.Add(this.TxtAdvance);
            this.roundPanel1.Controls.Add(this.TxtChangeAmount);
            this.roundPanel1.Controls.Add(this.btnCancel);
            this.roundPanel1.Controls.Add(this.clsSeparator1);
            this.roundPanel1.Controls.Add(this.TxtTenderAmount);
            this.roundPanel1.Controls.Add(this.label1);
            this.roundPanel1.Controls.Add(this.label7);
            this.roundPanel1.Controls.Add(this.label2);
            this.roundPanel1.Controls.Add(this.label6);
            this.roundPanel1.Controls.Add(this.TxtPanNo);
            this.roundPanel1.Controls.Add(this.label3);
            this.roundPanel1.Controls.Add(this.label5);
            this.roundPanel1.Controls.Add(this.TxtAddress);
            this.roundPanel1.Controls.Add(this.TxtPrintingDesc);
            this.roundPanel1.Controls.Add(this.label4);
            this.roundPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roundPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roundPanel1.Location = new System.Drawing.Point(0, 0);
            this.roundPanel1.Name = "roundPanel1";
            this.roundPanel1.Radious = 25;
            this.roundPanel1.Size = new System.Drawing.Size(887, 524);
            this.roundPanel1.TabIndex = 0;
            this.roundPanel1.TabStop = false;
            this.roundPanel1.Text = "CUSTOMER DETAILS";
            this.roundPanel1.TitleBackColor = System.Drawing.Color.DarkSlateBlue;
            this.roundPanel1.TitleFont = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.roundPanel1.TitleForeColor = System.Drawing.Color.White;
            this.roundPanel1.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
            // 
            // TxtBillAmount
            // 
            this.TxtBillAmount.BackColor = System.Drawing.Color.White;
            this.TxtBillAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBillAmount.Enabled = false;
            this.TxtBillAmount.Font = new System.Drawing.Font("Bookman Old Style", 45F);
            this.TxtBillAmount.ForeColor = System.Drawing.Color.Navy;
            this.TxtBillAmount.Location = new System.Drawing.Point(201, 238);
            this.TxtBillAmount.Margin = new System.Windows.Forms.Padding(2);
            this.TxtBillAmount.Multiline = true;
            this.TxtBillAmount.Name = "TxtBillAmount";
            this.TxtBillAmount.ReadOnly = true;
            this.TxtBillAmount.Size = new System.Drawing.Size(634, 76);
            this.TxtBillAmount.TabIndex = 7;
            this.TxtBillAmount.Text = "0.00";
            this.TxtBillAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Bookman Old Style", 20F);
            this.label10.Location = new System.Drawing.Point(6, 159);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(141, 32);
            this.label10.TabIndex = 305;
            this.label10.Text = "Phone No";
            // 
            // TxtPhoneNo
            // 
            this.TxtPhoneNo.BackColor = System.Drawing.Color.White;
            this.TxtPhoneNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPhoneNo.Enabled = false;
            this.TxtPhoneNo.Font = new System.Drawing.Font("Bookman Old Style", 20F);
            this.TxtPhoneNo.Location = new System.Drawing.Point(201, 152);
            this.TxtPhoneNo.Margin = new System.Windows.Forms.Padding(2);
            this.TxtPhoneNo.Name = "TxtPhoneNo";
            this.TxtPhoneNo.Size = new System.Drawing.Size(251, 39);
            this.TxtPhoneNo.TabIndex = 4;
            this.TxtPhoneNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // BtnPartyName
            // 
            this.BtnPartyName.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPartyName.Image = global::MrBLL.Properties.Resources.Search;
            this.BtnPartyName.Location = new System.Drawing.Point(836, 68);
            this.BtnPartyName.Name = "BtnPartyName";
            this.BtnPartyName.Size = new System.Drawing.Size(49, 39);
            this.BtnPartyName.TabIndex = 303;
            this.BtnPartyName.TabStop = false;
            this.BtnPartyName.UseVisualStyleBackColor = true;
            this.BtnPartyName.Click += new System.EventHandler(this.BtnPartyName_Click);
            // 
            // btnCustomer
            // 
            this.btnCustomer.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustomer.Image = global::MrBLL.Properties.Resources.Search;
            this.btnCustomer.Location = new System.Drawing.Point(836, 28);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Size = new System.Drawing.Size(49, 39);
            this.btnCustomer.TabIndex = 302;
            this.btnCustomer.TabStop = false;
            this.btnCustomer.UseVisualStyleBackColor = true;
            this.btnCustomer.Click += new System.EventHandler(this.BtnCustomer_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Bookman Old Style", 20F);
            this.label9.Location = new System.Drawing.Point(456, 112);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(121, 32);
            this.label9.TabIndex = 301;
            this.label9.Text = "Balance";
            // 
            // TxtBalance
            // 
            this.TxtBalance.BackColor = System.Drawing.Color.White;
            this.TxtBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBalance.Enabled = false;
            this.TxtBalance.Font = new System.Drawing.Font("Bookman Old Style", 20F);
            this.TxtBalance.Location = new System.Drawing.Point(586, 110);
            this.TxtBalance.Margin = new System.Windows.Forms.Padding(2);
            this.TxtBalance.Name = "TxtBalance";
            this.TxtBalance.Size = new System.Drawing.Size(249, 39);
            this.TxtBalance.TabIndex = 300;
            this.TxtBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ChkPrint
            // 
            this.ChkPrint.AutoSize = true;
            this.ChkPrint.Checked = true;
            this.ChkPrint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkPrint.Font = new System.Drawing.Font("Bookman Old Style", 20F);
            this.ChkPrint.Location = new System.Drawing.Point(7, 476);
            this.ChkPrint.Name = "ChkPrint";
            this.ChkPrint.Size = new System.Drawing.Size(200, 36);
            this.ChkPrint.TabIndex = 13;
            this.ChkPrint.Text = "Print Invoice";
            this.ChkPrint.UseVisualStyleBackColor = true;
            // 
            // TxtCustomer
            // 
            this.TxtCustomer.BackColor = System.Drawing.Color.White;
            this.TxtCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCustomer.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCustomer.Location = new System.Drawing.Point(201, 28);
            this.TxtCustomer.Margin = new System.Windows.Forms.Padding(2);
            this.TxtCustomer.Name = "TxtCustomer";
            this.TxtCustomer.ReadOnly = true;
            this.TxtCustomer.Size = new System.Drawing.Size(634, 39);
            this.TxtCustomer.TabIndex = 1;
            this.TxtCustomer.Enter += new System.EventHandler(this.Global_Enter);
            this.TxtCustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCustomer_KeyDown);
            this.TxtCustomer.Leave += new System.EventHandler(this.Global_Leave);
            this.TxtCustomer.Validating += new System.ComponentModel.CancelEventHandler(this.TxtCustomer_Validating);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Bookman Old Style", 20F);
            this.label8.Location = new System.Drawing.Point(456, 154);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(126, 32);
            this.label8.TabIndex = 60;
            this.label8.Text = "Advance";
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 20F);
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnSave.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnSave.Location = new System.Drawing.Point(553, 472);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(128, 48);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "&SAVE";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // BtnSavePrint
            // 
            this.BtnSavePrint.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 20F);
            this.BtnSavePrint.Appearance.Options.UseFont = true;
            this.BtnSavePrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.BtnSavePrint.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.BtnSavePrint.Location = new System.Drawing.Point(324, 472);
            this.BtnSavePrint.Name = "BtnSavePrint";
            this.BtnSavePrint.Size = new System.Drawing.Size(228, 48);
            this.BtnSavePrint.TabIndex = 10;
            this.BtnSavePrint.Text = "&SAVE && PRINT";
            this.BtnSavePrint.Click += new System.EventHandler(this.BtnSavePrint_Click);
            // 
            // TxtAdvance
            // 
            this.TxtAdvance.BackColor = System.Drawing.Color.White;
            this.TxtAdvance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAdvance.Enabled = false;
            this.TxtAdvance.Font = new System.Drawing.Font("Bookman Old Style", 20F);
            this.TxtAdvance.Location = new System.Drawing.Point(586, 152);
            this.TxtAdvance.Margin = new System.Windows.Forms.Padding(2);
            this.TxtAdvance.Name = "TxtAdvance";
            this.TxtAdvance.Size = new System.Drawing.Size(249, 39);
            this.TxtAdvance.TabIndex = 5;
            this.TxtAdvance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtChangeAmount
            // 
            this.TxtChangeAmount.BackColor = System.Drawing.Color.White;
            this.TxtChangeAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtChangeAmount.Enabled = false;
            this.TxtChangeAmount.Font = new System.Drawing.Font("Bookman Old Style", 45F);
            this.TxtChangeAmount.ForeColor = System.Drawing.Color.Navy;
            this.TxtChangeAmount.Location = new System.Drawing.Point(201, 390);
            this.TxtChangeAmount.Margin = new System.Windows.Forms.Padding(2);
            this.TxtChangeAmount.Multiline = true;
            this.TxtChangeAmount.Name = "TxtChangeAmount";
            this.TxtChangeAmount.ReadOnly = true;
            this.TxtChangeAmount.Size = new System.Drawing.Size(634, 76);
            this.TxtChangeAmount.TabIndex = 9;
            this.TxtChangeAmount.Text = "0.00";
            this.TxtChangeAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 20F);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(682, 472);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(152, 48);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "&CANCEL";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.Lavender;
            this.clsSeparator1.Font = new System.Drawing.Font("Bookman Old Style", 20F);
            this.clsSeparator1.Location = new System.Drawing.Point(5, 467);
            this.clsSeparator1.Margin = new System.Windows.Forms.Padding(2);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Padding = new System.Windows.Forms.Padding(2);
            this.clsSeparator1.Size = new System.Drawing.Size(880, 3);
            this.clsSeparator1.TabIndex = 59;
            this.clsSeparator1.TabStop = false;
            // 
            // TxtTenderAmount
            // 
            this.TxtTenderAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTenderAmount.Font = new System.Drawing.Font("Bookman Old Style", 40F);
            this.TxtTenderAmount.Location = new System.Drawing.Point(201, 317);
            this.TxtTenderAmount.Margin = new System.Windows.Forms.Padding(2);
            this.TxtTenderAmount.Name = "TxtTenderAmount";
            this.TxtTenderAmount.Size = new System.Drawing.Size(634, 70);
            this.TxtTenderAmount.TabIndex = 8;
            this.TxtTenderAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtTenderAmount.TextChanged += new System.EventHandler(this.TxtTenderAmount_TextChanged);
            this.TxtTenderAmount.Validating += new System.ComponentModel.CancelEventHandler(this.TxtTenderAmount_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 20F);
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Customer";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Bookman Old Style", 20F);
            this.label7.Location = new System.Drawing.Point(6, 412);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 32);
            this.label7.TabIndex = 58;
            this.label7.Text = "Change";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 20F);
            this.label2.Location = new System.Drawing.Point(6, 71);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 32);
            this.label2.TabIndex = 55;
            this.label2.Text = "Printing Desc";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Bookman Old Style", 20F);
            this.label6.Location = new System.Drawing.Point(1, 336);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 32);
            this.label6.TabIndex = 57;
            this.label6.Text = "Tender ";
            // 
            // TxtPanNo
            // 
            this.TxtPanNo.BackColor = System.Drawing.Color.White;
            this.TxtPanNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPanNo.Enabled = false;
            this.TxtPanNo.Font = new System.Drawing.Font("Bookman Old Style", 20F);
            this.TxtPanNo.Location = new System.Drawing.Point(201, 110);
            this.TxtPanNo.Margin = new System.Windows.Forms.Padding(2);
            this.TxtPanNo.Name = "TxtPanNo";
            this.TxtPanNo.Size = new System.Drawing.Size(251, 39);
            this.TxtPanNo.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 20F);
            this.label3.Location = new System.Drawing.Point(11, 197);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 32);
            this.label3.TabIndex = 53;
            this.label3.Text = "Address";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Bookman Old Style", 20F);
            this.label5.Location = new System.Drawing.Point(6, 260);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(172, 32);
            this.label5.TabIndex = 56;
            this.label5.Text = "Bill Amount";
            // 
            // TxtAddress
            // 
            this.TxtAddress.BackColor = System.Drawing.Color.White;
            this.TxtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAddress.Enabled = false;
            this.TxtAddress.Font = new System.Drawing.Font("Bookman Old Style", 20F);
            this.TxtAddress.Location = new System.Drawing.Point(201, 195);
            this.TxtAddress.Margin = new System.Windows.Forms.Padding(2);
            this.TxtAddress.Name = "TxtAddress";
            this.TxtAddress.Size = new System.Drawing.Size(634, 39);
            this.TxtAddress.TabIndex = 6;
            // 
            // TxtPrintingDesc
            // 
            this.TxtPrintingDesc.BackColor = System.Drawing.Color.White;
            this.TxtPrintingDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPrintingDesc.Enabled = false;
            this.TxtPrintingDesc.Font = new System.Drawing.Font("Bookman Old Style", 20F);
            this.TxtPrintingDesc.Location = new System.Drawing.Point(201, 68);
            this.TxtPrintingDesc.Margin = new System.Windows.Forms.Padding(2);
            this.TxtPrintingDesc.Name = "TxtPrintingDesc";
            this.TxtPrintingDesc.Size = new System.Drawing.Size(634, 39);
            this.TxtPrintingDesc.TabIndex = 2;
            this.TxtPrintingDesc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPrintingDesc_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bookman Old Style", 20F);
            this.label4.Location = new System.Drawing.Point(6, 113);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 32);
            this.label4.TabIndex = 54;
            this.label4.Text = "Pan No";
            // 
            // FrmStoreCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 524);
            this.Controls.Add(this.roundPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmStoreCustomer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Details";
            this.Load += new System.EventHandler(this.FrmStoreCustomer_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmStoreCustomer_KeyDown);
            this.roundPanel1.ResumeLayout(false);
            this.roundPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label8;
        private ClsSeparator clsSeparator1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public DevExpress.XtraEditors.SimpleButton btnCancel;
        public DevExpress.XtraEditors.SimpleButton BtnSavePrint;
        public DevExpress.XtraEditors.SimpleButton btnSave;
        private MrTextBox TxtAdvance;
        private MrTextBox TxtCustomer;
        private MrTextBox TxtPrintingDesc;
        private MrTextBox TxtAddress;
        private MrTextBox TxtPanNo;
        private MrTextBox TxtTenderAmount;
        private System.Windows.Forms.CheckBox ChkPrint;
		private RoundPanel roundPanel1;
		private System.Windows.Forms.Label label9;
		private MrTextBox TxtBalance;
		private System.Windows.Forms.Button BtnPartyName;
		private System.Windows.Forms.Button btnCustomer;
		private System.Windows.Forms.Label label10;
		private MrTextBox TxtPhoneNo;
        private MrTextBox TxtBillAmount;
        public MrTextBox TxtChangeAmount;
    }
}