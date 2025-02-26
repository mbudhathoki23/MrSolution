namespace MrBLL.DataEntry.Common
{
    partial class FrmCalculator
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
            this.mrPanel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.TxtEqual = new DevExpress.XtraEditors.SimpleButton();
            this.TxtMinus = new DevExpress.XtraEditors.SimpleButton();
            this.TxtPlus = new DevExpress.XtraEditors.SimpleButton();
            this.TxtMultiply = new DevExpress.XtraEditors.SimpleButton();
            this.TxtDivide = new DevExpress.XtraEditors.SimpleButton();
            this.TxtDot = new DevExpress.XtraEditors.SimpleButton();
            this.TxtZero = new DevExpress.XtraEditors.SimpleButton();
            this.TxtNine = new DevExpress.XtraEditors.SimpleButton();
            this.TxtEight = new DevExpress.XtraEditors.SimpleButton();
            this.TxtSeven = new DevExpress.XtraEditors.SimpleButton();
            this.TxtSix = new DevExpress.XtraEditors.SimpleButton();
            this.TxtFive = new DevExpress.XtraEditors.SimpleButton();
            this.TxtFour = new DevExpress.XtraEditors.SimpleButton();
            this.TxtThree = new DevExpress.XtraEditors.SimpleButton();
            this.TxtTwo = new DevExpress.XtraEditors.SimpleButton();
            this.TxtOne = new DevExpress.XtraEditors.SimpleButton();
            this.LblResult = new System.Windows.Forms.Label();
            this.BtnDeleteLastValue = new DevExpress.XtraEditors.SimpleButton();
            this.BtnClear = new DevExpress.XtraEditors.SimpleButton();
            this.BtnClearEvent = new DevExpress.XtraEditors.SimpleButton();
            this.BtnPercentage = new DevExpress.XtraEditors.SimpleButton();
            this.mrPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mrPanel1
            // 
            this.mrPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel1.Controls.Add(this.BtnDeleteLastValue);
            this.mrPanel1.Controls.Add(this.BtnClear);
            this.mrPanel1.Controls.Add(this.BtnClearEvent);
            this.mrPanel1.Controls.Add(this.BtnPercentage);
            this.mrPanel1.Controls.Add(this.TxtEqual);
            this.mrPanel1.Controls.Add(this.TxtMinus);
            this.mrPanel1.Controls.Add(this.TxtPlus);
            this.mrPanel1.Controls.Add(this.TxtMultiply);
            this.mrPanel1.Controls.Add(this.TxtDivide);
            this.mrPanel1.Controls.Add(this.TxtDot);
            this.mrPanel1.Controls.Add(this.TxtZero);
            this.mrPanel1.Controls.Add(this.TxtNine);
            this.mrPanel1.Controls.Add(this.TxtEight);
            this.mrPanel1.Controls.Add(this.TxtSeven);
            this.mrPanel1.Controls.Add(this.TxtSix);
            this.mrPanel1.Controls.Add(this.TxtFive);
            this.mrPanel1.Controls.Add(this.TxtFour);
            this.mrPanel1.Controls.Add(this.TxtThree);
            this.mrPanel1.Controls.Add(this.TxtTwo);
            this.mrPanel1.Controls.Add(this.TxtOne);
            this.mrPanel1.Controls.Add(this.LblResult);
            this.mrPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrPanel1.Location = new System.Drawing.Point(0, 0);
            this.mrPanel1.Name = "mrPanel1";
            this.mrPanel1.Size = new System.Drawing.Size(344, 281);
            this.mrPanel1.TabIndex = 0;
            // 
            // TxtEqual
            // 
            this.TxtEqual.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtEqual.Appearance.Options.UseFont = true;
            this.TxtEqual.Location = new System.Drawing.Point(4, 234);
            this.TxtEqual.Name = "TxtEqual";
            this.TxtEqual.Size = new System.Drawing.Size(78, 41);
            this.TxtEqual.TabIndex = 16;
            this.TxtEqual.Text = "=";
            this.TxtEqual.Click += new System.EventHandler(this.BEq_Click);
            // 
            // TxtMinus
            // 
            this.TxtMinus.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtMinus.Appearance.Options.UseFont = true;
            this.TxtMinus.Location = new System.Drawing.Point(256, 234);
            this.TxtMinus.Name = "TxtMinus";
            this.TxtMinus.Size = new System.Drawing.Size(78, 41);
            this.TxtMinus.TabIndex = 15;
            this.TxtMinus.Text = "-";
            this.TxtMinus.Click += new System.EventHandler(this.OperandEvent);
            // 
            // TxtPlus
            // 
            this.TxtPlus.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPlus.Appearance.Options.UseFont = true;
            this.TxtPlus.Location = new System.Drawing.Point(256, 187);
            this.TxtPlus.Name = "TxtPlus";
            this.TxtPlus.Size = new System.Drawing.Size(78, 41);
            this.TxtPlus.TabIndex = 14;
            this.TxtPlus.Text = "+";
            this.TxtPlus.Click += new System.EventHandler(this.OperandEvent);
            // 
            // TxtMultiply
            // 
            this.TxtMultiply.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtMultiply.Appearance.Options.UseFont = true;
            this.TxtMultiply.Location = new System.Drawing.Point(256, 140);
            this.TxtMultiply.Name = "TxtMultiply";
            this.TxtMultiply.Size = new System.Drawing.Size(78, 41);
            this.TxtMultiply.TabIndex = 13;
            this.TxtMultiply.Text = "X";
            this.TxtMultiply.Click += new System.EventHandler(this.OperandEvent);
            // 
            // TxtDivide
            // 
            this.TxtDivide.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDivide.Appearance.Options.UseFont = true;
            this.TxtDivide.Location = new System.Drawing.Point(256, 93);
            this.TxtDivide.Name = "TxtDivide";
            this.TxtDivide.Size = new System.Drawing.Size(78, 41);
            this.TxtDivide.TabIndex = 12;
            this.TxtDivide.Text = "/";
            this.TxtDivide.Click += new System.EventHandler(this.OperandEvent);
            // 
            // TxtDot
            // 
            this.TxtDot.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDot.Appearance.Options.UseFont = true;
            this.TxtDot.Location = new System.Drawing.Point(172, 234);
            this.TxtDot.Name = "TxtDot";
            this.TxtDot.Size = new System.Drawing.Size(78, 41);
            this.TxtDot.TabIndex = 11;
            this.TxtDot.Text = ".";
            this.TxtDot.Click += new System.EventHandler(this.NumEvent);
            // 
            // TxtZero
            // 
            this.TxtZero.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtZero.Appearance.Options.UseFont = true;
            this.TxtZero.Location = new System.Drawing.Point(88, 234);
            this.TxtZero.Name = "TxtZero";
            this.TxtZero.Size = new System.Drawing.Size(78, 41);
            this.TxtZero.TabIndex = 10;
            this.TxtZero.Text = "0";
            this.TxtZero.Click += new System.EventHandler(this.NumEvent);
            // 
            // TxtNine
            // 
            this.TxtNine.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtNine.Appearance.Options.UseFont = true;
            this.TxtNine.Location = new System.Drawing.Point(172, 187);
            this.TxtNine.Name = "TxtNine";
            this.TxtNine.Size = new System.Drawing.Size(78, 41);
            this.TxtNine.TabIndex = 9;
            this.TxtNine.Text = "9";
            this.TxtNine.Click += new System.EventHandler(this.NumEvent);
            // 
            // TxtEight
            // 
            this.TxtEight.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtEight.Appearance.Options.UseFont = true;
            this.TxtEight.Location = new System.Drawing.Point(87, 187);
            this.TxtEight.Name = "TxtEight";
            this.TxtEight.Size = new System.Drawing.Size(78, 41);
            this.TxtEight.TabIndex = 8;
            this.TxtEight.Text = "8";
            this.TxtEight.Click += new System.EventHandler(this.NumEvent);
            // 
            // TxtSeven
            // 
            this.TxtSeven.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSeven.Appearance.Options.UseFont = true;
            this.TxtSeven.Location = new System.Drawing.Point(3, 187);
            this.TxtSeven.Name = "TxtSeven";
            this.TxtSeven.Size = new System.Drawing.Size(78, 41);
            this.TxtSeven.TabIndex = 7;
            this.TxtSeven.Text = "7";
            this.TxtSeven.Click += new System.EventHandler(this.NumEvent);
            // 
            // TxtSix
            // 
            this.TxtSix.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSix.Appearance.Options.UseFont = true;
            this.TxtSix.Location = new System.Drawing.Point(172, 140);
            this.TxtSix.Name = "TxtSix";
            this.TxtSix.Size = new System.Drawing.Size(78, 41);
            this.TxtSix.TabIndex = 6;
            this.TxtSix.Text = "6";
            this.TxtSix.Click += new System.EventHandler(this.NumEvent);
            // 
            // TxtFive
            // 
            this.TxtFive.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtFive.Appearance.Options.UseFont = true;
            this.TxtFive.Location = new System.Drawing.Point(88, 140);
            this.TxtFive.Name = "TxtFive";
            this.TxtFive.Size = new System.Drawing.Size(78, 41);
            this.TxtFive.TabIndex = 5;
            this.TxtFive.Text = "5";
            this.TxtFive.Click += new System.EventHandler(this.NumEvent);
            // 
            // TxtFour
            // 
            this.TxtFour.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtFour.Appearance.Options.UseFont = true;
            this.TxtFour.Location = new System.Drawing.Point(4, 140);
            this.TxtFour.Name = "TxtFour";
            this.TxtFour.Size = new System.Drawing.Size(78, 41);
            this.TxtFour.TabIndex = 4;
            this.TxtFour.Text = "4";
            this.TxtFour.Click += new System.EventHandler(this.NumEvent);
            // 
            // TxtThree
            // 
            this.TxtThree.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtThree.Appearance.Options.UseFont = true;
            this.TxtThree.Location = new System.Drawing.Point(172, 93);
            this.TxtThree.Name = "TxtThree";
            this.TxtThree.Size = new System.Drawing.Size(78, 41);
            this.TxtThree.TabIndex = 3;
            this.TxtThree.Text = "3";
            this.TxtThree.Click += new System.EventHandler(this.NumEvent);
            // 
            // TxtTwo
            // 
            this.TxtTwo.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTwo.Appearance.Options.UseFont = true;
            this.TxtTwo.Location = new System.Drawing.Point(88, 93);
            this.TxtTwo.Name = "TxtTwo";
            this.TxtTwo.Size = new System.Drawing.Size(78, 41);
            this.TxtTwo.TabIndex = 2;
            this.TxtTwo.Text = "2";
            this.TxtTwo.Click += new System.EventHandler(this.NumEvent);
            // 
            // TxtOne
            // 
            this.TxtOne.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtOne.Appearance.Options.UseFont = true;
            this.TxtOne.Location = new System.Drawing.Point(4, 93);
            this.TxtOne.Name = "TxtOne";
            this.TxtOne.Size = new System.Drawing.Size(78, 41);
            this.TxtOne.TabIndex = 1;
            this.TxtOne.Text = "1";
            this.TxtOne.Click += new System.EventHandler(this.NumEvent);
            // 
            // LblResult
            // 
            this.LblResult.BackColor = System.Drawing.Color.White;
            this.LblResult.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblResult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblResult.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblResult.Location = new System.Drawing.Point(0, 0);
            this.LblResult.Name = "LblResult";
            this.LblResult.Size = new System.Drawing.Size(344, 41);
            this.LblResult.TabIndex = 0;
            this.LblResult.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BtnDeleteLastValue
            // 
            this.BtnDeleteLastValue.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDeleteLastValue.Appearance.Options.UseFont = true;
            this.BtnDeleteLastValue.Location = new System.Drawing.Point(255, 46);
            this.BtnDeleteLastValue.Name = "BtnDeleteLastValue";
            this.BtnDeleteLastValue.Size = new System.Drawing.Size(78, 41);
            this.BtnDeleteLastValue.TabIndex = 20;
            this.BtnDeleteLastValue.Text = "[X]";
            // 
            // BtnClear
            // 
            this.BtnClear.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClear.Appearance.Options.UseFont = true;
            this.BtnClear.Location = new System.Drawing.Point(171, 46);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(78, 41);
            this.BtnClear.TabIndex = 19;
            this.BtnClear.Text = "C";
            this.BtnClear.Click += new System.EventHandler(this.BC_Click);
            // 
            // BtnClearEvent
            // 
            this.BtnClearEvent.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClearEvent.Appearance.Options.UseFont = true;
            this.BtnClearEvent.Location = new System.Drawing.Point(87, 46);
            this.BtnClearEvent.Name = "BtnClearEvent";
            this.BtnClearEvent.Size = new System.Drawing.Size(78, 41);
            this.BtnClearEvent.TabIndex = 18;
            this.BtnClearEvent.Text = "CE";
            this.BtnClearEvent.Click += new System.EventHandler(this.BCE_Click);
            // 
            // BtnPercentage
            // 
            this.BtnPercentage.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPercentage.Appearance.Options.UseFont = true;
            this.BtnPercentage.Location = new System.Drawing.Point(3, 46);
            this.BtnPercentage.Name = "BtnPercentage";
            this.BtnPercentage.Size = new System.Drawing.Size(78, 41);
            this.BtnPercentage.TabIndex = 17;
            this.BtnPercentage.Text = "%";
            // 
            // FrmCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 281);
            this.Controls.Add(this.mrPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCalculator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calculator";
            this.Load += new System.EventHandler(this.FrmCalculator_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCalculator_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmCalculator_KeyPress);
            this.mrPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MrDAL.Control.ControlsEx.Control.MrPanel mrPanel1;
        private System.Windows.Forms.Label LblResult;
        private DevExpress.XtraEditors.SimpleButton TxtThree;
        private DevExpress.XtraEditors.SimpleButton TxtTwo;
        private DevExpress.XtraEditors.SimpleButton TxtOne;
        private DevExpress.XtraEditors.SimpleButton TxtMinus;
        private DevExpress.XtraEditors.SimpleButton TxtPlus;
        private DevExpress.XtraEditors.SimpleButton TxtMultiply;
        private DevExpress.XtraEditors.SimpleButton TxtDivide;
        private DevExpress.XtraEditors.SimpleButton TxtDot;
        private DevExpress.XtraEditors.SimpleButton TxtZero;
        private DevExpress.XtraEditors.SimpleButton TxtNine;
        private DevExpress.XtraEditors.SimpleButton TxtEight;
        private DevExpress.XtraEditors.SimpleButton TxtSeven;
        private DevExpress.XtraEditors.SimpleButton TxtSix;
        private DevExpress.XtraEditors.SimpleButton TxtFive;
        private DevExpress.XtraEditors.SimpleButton TxtFour;
        private DevExpress.XtraEditors.SimpleButton TxtEqual;
        private DevExpress.XtraEditors.SimpleButton BtnDeleteLastValue;
        private DevExpress.XtraEditors.SimpleButton BtnClear;
        private DevExpress.XtraEditors.SimpleButton BtnClearEvent;
        private DevExpress.XtraEditors.SimpleButton BtnPercentage;
    }
}