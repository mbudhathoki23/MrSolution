
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.SystemSetting
{
    partial class FrmPaymentSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPaymentSetting));
            this.panel1 = new MrPanel();
            this.mrGroup1 = new MrGroup();
            this.BtnPartialCustomer = new System.Windows.Forms.Button();
            this.TxtPartialCustomer = new MrTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.BtnConnectIpsLedger = new System.Windows.Forms.Button();
            this.TxtConnectIPSLedger = new MrTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnRemitLedger = new System.Windows.Forms.Button();
            this.TxtRemitLedger = new MrTextBox();
            this.BtnKhaltiLedger = new System.Windows.Forms.Button();
            this.TxtKhaltiLedger = new MrTextBox();
            this.BtnEsewaLedger = new System.Windows.Forms.Button();
            this.TxtEsewaLedger = new MrTextBox();
            this.BtnPhonePayLedger = new System.Windows.Forms.Button();
            this.TxtPhonePayLedger = new MrTextBox();
            this.BtnBankLedger = new System.Windows.Forms.Button();
            this.TxtBankLedger = new MrTextBox();
            this.BtnCardLedger = new System.Windows.Forms.Button();
            this.TxtCardLedger = new MrTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnCashLedger = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSaveClosed = new DevExpress.XtraEditors.SimpleButton();
            this.TxtCashLedger = new MrTextBox();
            this.BtnGiftVoucher = new System.Windows.Forms.Button();
            this.TxtGiftVoucher = new MrTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.clsSeparator1 = new ClsSeparator();
            this.panel1.SuspendLayout();
            this.mrGroup1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.mrGroup1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(513, 335);
            this.panel1.TabIndex = 0;
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.clsSeparator1);
            this.mrGroup1.Controls.Add(this.BtnGiftVoucher);
            this.mrGroup1.Controls.Add(this.TxtGiftVoucher);
            this.mrGroup1.Controls.Add(this.label10);
            this.mrGroup1.Controls.Add(this.BtnPartialCustomer);
            this.mrGroup1.Controls.Add(this.TxtPartialCustomer);
            this.mrGroup1.Controls.Add(this.label9);
            this.mrGroup1.Controls.Add(this.BtnConnectIpsLedger);
            this.mrGroup1.Controls.Add(this.TxtConnectIPSLedger);
            this.mrGroup1.Controls.Add(this.label3);
            this.mrGroup1.Controls.Add(this.BtnRemitLedger);
            this.mrGroup1.Controls.Add(this.TxtRemitLedger);
            this.mrGroup1.Controls.Add(this.BtnKhaltiLedger);
            this.mrGroup1.Controls.Add(this.TxtKhaltiLedger);
            this.mrGroup1.Controls.Add(this.BtnEsewaLedger);
            this.mrGroup1.Controls.Add(this.TxtEsewaLedger);
            this.mrGroup1.Controls.Add(this.BtnPhonePayLedger);
            this.mrGroup1.Controls.Add(this.TxtPhonePayLedger);
            this.mrGroup1.Controls.Add(this.BtnBankLedger);
            this.mrGroup1.Controls.Add(this.TxtBankLedger);
            this.mrGroup1.Controls.Add(this.BtnCardLedger);
            this.mrGroup1.Controls.Add(this.TxtCardLedger);
            this.mrGroup1.Controls.Add(this.label8);
            this.mrGroup1.Controls.Add(this.label7);
            this.mrGroup1.Controls.Add(this.label6);
            this.mrGroup1.Controls.Add(this.label5);
            this.mrGroup1.Controls.Add(this.label4);
            this.mrGroup1.Controls.Add(this.label2);
            this.mrGroup1.Controls.Add(this.BtnCashLedger);
            this.mrGroup1.Controls.Add(this.label1);
            this.mrGroup1.Controls.Add(this.BtnSave);
            this.mrGroup1.Controls.Add(this.BtnSaveClosed);
            this.mrGroup1.Controls.Add(this.TxtCashLedger);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "";
            this.mrGroup1.Location = new System.Drawing.Point(0, 0);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 10;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = true;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(513, 335);
            this.mrGroup1.TabIndex = 0;
            // 
            // BtnPartialCustomer
            // 
            this.BtnPartialCustomer.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPartialCustomer.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnPartialCustomer.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnPartialCustomer.Location = new System.Drawing.Point(480, 232);
            this.BtnPartialCustomer.Name = "BtnPartialCustomer";
            this.BtnPartialCustomer.Size = new System.Drawing.Size(28, 25);
            this.BtnPartialCustomer.TabIndex = 202;
            this.BtnPartialCustomer.TabStop = false;
            this.BtnPartialCustomer.UseVisualStyleBackColor = true;
            this.BtnPartialCustomer.Click += new System.EventHandler(this.BtnPartialCustomer_Click);
            // 
            // TxtPartialCustomer
            // 
            this.TxtPartialCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPartialCustomer.Location = new System.Drawing.Point(142, 232);
            this.TxtPartialCustomer.Name = "TxtPartialCustomer";
            this.TxtPartialCustomer.Size = new System.Drawing.Size(332, 25);
            this.TxtPartialCustomer.TabIndex = 8;
            this.TxtPartialCustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPartialCustomer_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 235);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 19);
            this.label9.TabIndex = 201;
            this.label9.Text = "PARTIAL";
            // 
            // BtnConnectIpsLedger
            // 
            this.BtnConnectIpsLedger.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnConnectIpsLedger.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnConnectIpsLedger.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnConnectIpsLedger.Location = new System.Drawing.Point(480, 204);
            this.BtnConnectIpsLedger.Name = "BtnConnectIpsLedger";
            this.BtnConnectIpsLedger.Size = new System.Drawing.Size(28, 25);
            this.BtnConnectIpsLedger.TabIndex = 199;
            this.BtnConnectIpsLedger.TabStop = false;
            this.BtnConnectIpsLedger.UseVisualStyleBackColor = true;
            this.BtnConnectIpsLedger.Click += new System.EventHandler(this.BtnConnectIpsLedger_Click);
            // 
            // TxtConnectIPSLedger
            // 
            this.TxtConnectIPSLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtConnectIPSLedger.Location = new System.Drawing.Point(142, 204);
            this.TxtConnectIPSLedger.Name = "TxtConnectIPSLedger";
            this.TxtConnectIPSLedger.Size = new System.Drawing.Size(332, 25);
            this.TxtConnectIPSLedger.TabIndex = 7;
            this.TxtConnectIPSLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtConnectIPSLedger_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 207);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 19);
            this.label3.TabIndex = 198;
            this.label3.Text = "CONNECT IPS";
            // 
            // BtnRemitLedger
            // 
            this.BtnRemitLedger.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRemitLedger.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnRemitLedger.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnRemitLedger.Location = new System.Drawing.Point(480, 177);
            this.BtnRemitLedger.Name = "BtnRemitLedger";
            this.BtnRemitLedger.Size = new System.Drawing.Size(28, 25);
            this.BtnRemitLedger.TabIndex = 196;
            this.BtnRemitLedger.TabStop = false;
            this.BtnRemitLedger.UseVisualStyleBackColor = true;
            this.BtnRemitLedger.Click += new System.EventHandler(this.BtnRemitLedger_Click);
            // 
            // TxtRemitLedger
            // 
            this.TxtRemitLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtRemitLedger.Location = new System.Drawing.Point(142, 177);
            this.TxtRemitLedger.Name = "TxtRemitLedger";
            this.TxtRemitLedger.Size = new System.Drawing.Size(332, 25);
            this.TxtRemitLedger.TabIndex = 6;
            this.TxtRemitLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtRemitLedger_KeyDown);
            // 
            // BtnKhaltiLedger
            // 
            this.BtnKhaltiLedger.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnKhaltiLedger.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnKhaltiLedger.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnKhaltiLedger.Location = new System.Drawing.Point(480, 150);
            this.BtnKhaltiLedger.Name = "BtnKhaltiLedger";
            this.BtnKhaltiLedger.Size = new System.Drawing.Size(28, 25);
            this.BtnKhaltiLedger.TabIndex = 194;
            this.BtnKhaltiLedger.TabStop = false;
            this.BtnKhaltiLedger.UseVisualStyleBackColor = true;
            this.BtnKhaltiLedger.Click += new System.EventHandler(this.BtnKhaltiLedger_Click);
            // 
            // TxtKhaltiLedger
            // 
            this.TxtKhaltiLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtKhaltiLedger.Location = new System.Drawing.Point(142, 150);
            this.TxtKhaltiLedger.Name = "TxtKhaltiLedger";
            this.TxtKhaltiLedger.Size = new System.Drawing.Size(332, 25);
            this.TxtKhaltiLedger.TabIndex = 5;
            this.TxtKhaltiLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtKhaltiLedger_KeyDown);
            // 
            // BtnEsewaLedger
            // 
            this.BtnEsewaLedger.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEsewaLedger.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnEsewaLedger.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnEsewaLedger.Location = new System.Drawing.Point(480, 123);
            this.BtnEsewaLedger.Name = "BtnEsewaLedger";
            this.BtnEsewaLedger.Size = new System.Drawing.Size(28, 25);
            this.BtnEsewaLedger.TabIndex = 192;
            this.BtnEsewaLedger.TabStop = false;
            this.BtnEsewaLedger.UseVisualStyleBackColor = true;
            this.BtnEsewaLedger.Click += new System.EventHandler(this.BtnEsewaLedger_Click);
            // 
            // TxtEsewaLedger
            // 
            this.TxtEsewaLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtEsewaLedger.Location = new System.Drawing.Point(142, 123);
            this.TxtEsewaLedger.Name = "TxtEsewaLedger";
            this.TxtEsewaLedger.Size = new System.Drawing.Size(332, 25);
            this.TxtEsewaLedger.TabIndex = 4;
            this.TxtEsewaLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtEsewaLedger_KeyDown);
            // 
            // BtnPhonePayLedger
            // 
            this.BtnPhonePayLedger.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPhonePayLedger.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnPhonePayLedger.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnPhonePayLedger.Location = new System.Drawing.Point(480, 96);
            this.BtnPhonePayLedger.Name = "BtnPhonePayLedger";
            this.BtnPhonePayLedger.Size = new System.Drawing.Size(28, 25);
            this.BtnPhonePayLedger.TabIndex = 190;
            this.BtnPhonePayLedger.TabStop = false;
            this.BtnPhonePayLedger.UseVisualStyleBackColor = true;
            this.BtnPhonePayLedger.Click += new System.EventHandler(this.BtnPhonePayLedger_Click);
            // 
            // TxtPhonePayLedger
            // 
            this.TxtPhonePayLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPhonePayLedger.Location = new System.Drawing.Point(142, 96);
            this.TxtPhonePayLedger.Name = "TxtPhonePayLedger";
            this.TxtPhonePayLedger.Size = new System.Drawing.Size(332, 25);
            this.TxtPhonePayLedger.TabIndex = 3;
            this.TxtPhonePayLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPhonePayLedger_KeyDown);
            // 
            // BtnBankLedger
            // 
            this.BtnBankLedger.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBankLedger.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnBankLedger.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnBankLedger.Location = new System.Drawing.Point(480, 69);
            this.BtnBankLedger.Name = "BtnBankLedger";
            this.BtnBankLedger.Size = new System.Drawing.Size(28, 25);
            this.BtnBankLedger.TabIndex = 188;
            this.BtnBankLedger.TabStop = false;
            this.BtnBankLedger.UseVisualStyleBackColor = true;
            this.BtnBankLedger.Click += new System.EventHandler(this.BtnBankLedger_Click);
            // 
            // TxtBankLedger
            // 
            this.TxtBankLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBankLedger.Location = new System.Drawing.Point(142, 69);
            this.TxtBankLedger.Name = "TxtBankLedger";
            this.TxtBankLedger.Size = new System.Drawing.Size(332, 25);
            this.TxtBankLedger.TabIndex = 2;
            this.TxtBankLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBankLedger_KeyDown);
            // 
            // BtnCardLedger
            // 
            this.BtnCardLedger.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCardLedger.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnCardLedger.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnCardLedger.Location = new System.Drawing.Point(480, 42);
            this.BtnCardLedger.Name = "BtnCardLedger";
            this.BtnCardLedger.Size = new System.Drawing.Size(28, 25);
            this.BtnCardLedger.TabIndex = 184;
            this.BtnCardLedger.TabStop = false;
            this.BtnCardLedger.UseVisualStyleBackColor = true;
            this.BtnCardLedger.Click += new System.EventHandler(this.BtnCardLedger_Click);
            // 
            // TxtCardLedger
            // 
            this.TxtCardLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCardLedger.Location = new System.Drawing.Point(142, 42);
            this.TxtCardLedger.Name = "TxtCardLedger";
            this.TxtCardLedger.Size = new System.Drawing.Size(332, 25);
            this.TxtCardLedger.TabIndex = 1;
            this.TxtCardLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCardLedger_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 180);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 19);
            this.label8.TabIndex = 181;
            this.label8.Text = "REMIT";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 153);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 19);
            this.label7.TabIndex = 180;
            this.label7.Text = "KHALTI";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 19);
            this.label6.TabIndex = 179;
            this.label6.Text = "E-SEWA";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 19);
            this.label5.TabIndex = 178;
            this.label5.Text = "PHONE PAY";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 19);
            this.label4.TabIndex = 177;
            this.label4.Text = "BANK";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 19);
            this.label2.TabIndex = 175;
            this.label2.Text = "CARD";
            // 
            // BtnCashLedger
            // 
            this.BtnCashLedger.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCashLedger.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnCashLedger.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnCashLedger.Location = new System.Drawing.Point(480, 15);
            this.BtnCashLedger.Name = "BtnCashLedger";
            this.BtnCashLedger.Size = new System.Drawing.Size(28, 25);
            this.BtnCashLedger.TabIndex = 174;
            this.BtnCashLedger.TabStop = false;
            this.BtnCashLedger.UseVisualStyleBackColor = true;
            this.BtnCashLedger.Click += new System.EventHandler(this.BtnCashLedger_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 19);
            this.label1.TabIndex = 9;
            this.label1.Text = "CASH";
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(402, 292);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(95, 36);
            this.BtnSave.TabIndex = 11;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnSaveClosed
            // 
            this.BtnSaveClosed.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSaveClosed.Appearance.Options.UseFont = true;
            this.BtnSaveClosed.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSaveClosed.Location = new System.Drawing.Point(229, 292);
            this.BtnSaveClosed.Name = "BtnSaveClosed";
            this.BtnSaveClosed.Size = new System.Drawing.Size(171, 36);
            this.BtnSaveClosed.TabIndex = 10;
            this.BtnSaveClosed.Text = "SAVE && C&LOSED";
            this.BtnSaveClosed.Click += new System.EventHandler(this.BtnSaveClosed_Click);
            // 
            // TxtCashLedger
            // 
            this.TxtCashLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCashLedger.Location = new System.Drawing.Point(142, 15);
            this.TxtCashLedger.Name = "TxtCashLedger";
            this.TxtCashLedger.Size = new System.Drawing.Size(332, 25);
            this.TxtCashLedger.TabIndex = 0;
            this.TxtCashLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCashLedger_KeyDown);
            // 
            // BtnGiftVoucher
            // 
            this.BtnGiftVoucher.Font = new System.Drawing.Font("Bookman Old Style", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGiftVoucher.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnGiftVoucher.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnGiftVoucher.Location = new System.Drawing.Point(480, 259);
            this.BtnGiftVoucher.Name = "BtnGiftVoucher";
            this.BtnGiftVoucher.Size = new System.Drawing.Size(28, 25);
            this.BtnGiftVoucher.TabIndex = 205;
            this.BtnGiftVoucher.TabStop = false;
            this.BtnGiftVoucher.UseVisualStyleBackColor = true;
            this.BtnGiftVoucher.Click += new System.EventHandler(this.BtnGiftVoucher_Click);
            // 
            // TxtGiftVoucher
            // 
            this.TxtGiftVoucher.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtGiftVoucher.Location = new System.Drawing.Point(142, 259);
            this.TxtGiftVoucher.Name = "TxtGiftVoucher";
            this.TxtGiftVoucher.Size = new System.Drawing.Size(332, 25);
            this.TxtGiftVoucher.TabIndex = 9;
            this.TxtGiftVoucher.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtGiftVoucher_KeyDown);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 262);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(129, 19);
            this.label10.TabIndex = 204;
            this.label10.Text = "GIFT VOUCHER";
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator1.Location = new System.Drawing.Point(11, 289);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(490, 2);
            this.clsSeparator1.TabIndex = 206;
            this.clsSeparator1.TabStop = false;
            // 
            // FrmPaymentSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 335);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmPaymentSetting";
            this.ShowIcon = false;
            this.Text = "PAYMENT LEDGER TAG SETTING";
            this.Load += new System.EventHandler(this.FrmPaymentSetting_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmPaymentSetting_KeyPress);
            this.panel1.ResumeLayout(false);
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private MrTextBox TxtCashLedger;
        private MrGroup mrGroup1;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SimpleButton BtnSaveClosed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnCashLedger;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button BtnCardLedger;
        private MrTextBox TxtCardLedger;
        private System.Windows.Forms.Button BtnEsewaLedger;
        private MrTextBox TxtEsewaLedger;
        private System.Windows.Forms.Button BtnPhonePayLedger;
        private MrTextBox TxtPhonePayLedger;
        private System.Windows.Forms.Button BtnBankLedger;
        private MrTextBox TxtBankLedger;
        private System.Windows.Forms.Button BtnKhaltiLedger;
        private MrTextBox TxtKhaltiLedger;
        private System.Windows.Forms.Button BtnRemitLedger;
        private MrTextBox TxtRemitLedger;
        private System.Windows.Forms.Button BtnConnectIpsLedger;
        private MrTextBox TxtConnectIPSLedger;
        private System.Windows.Forms.Label label3;
        private MrPanel panel1;
        private System.Windows.Forms.Button BtnPartialCustomer;
        private MrTextBox TxtPartialCustomer;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button BtnGiftVoucher;
        private MrTextBox TxtGiftVoucher;
        private System.Windows.Forms.Label label10;
        private ClsSeparator clsSeparator1;
    }
}