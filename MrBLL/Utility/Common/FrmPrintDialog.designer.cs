using System.ComponentModel;
using System.Windows.Forms;
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Utility.Common
{
    partial class FrmPrintDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.btn_Print = new System.Windows.Forms.Button();
            this.chk_PageBreak = new System.Windows.Forms.CheckBox();
            this.gb_PaperOrientation = new System.Windows.Forms.GroupBox();
            this.rb_Landscape = new System.Windows.Forms.RadioButton();
            this.rb_Portrait = new System.Windows.Forms.RadioButton();
            this.cmb_PaperSize = new System.Windows.Forms.ComboBox();
            this.lbl_PaperSize = new System.Windows.Forms.Label();
            this.gb_Copies = new System.Windows.Forms.GroupBox();
            this.txt_NoOfCopies = new MrTextBox();
            this.lbl_NoOfCopies = new System.Windows.Forms.Label();
            this.gb_PageRange = new System.Windows.Forms.GroupBox();
            this.txt_To = new MrTextBox();
            this.txt_From = new MrTextBox();
            this.lbl_To = new System.Windows.Forms.Label();
            this.rb_Pages = new System.Windows.Forms.RadioButton();
            this.rb_All = new System.Windows.Forms.RadioButton();
            this.cmb_Printer = new System.Windows.Forms.ComboBox();
            this.lbl_PrinterName = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.gb_PaperOrientation.SuspendLayout();
            this.gb_Copies.SuspendLayout();
            this.gb_PageRange.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_Print);
            this.groupBox1.Controls.Add(this.chk_PageBreak);
            this.groupBox1.Controls.Add(this.gb_PaperOrientation);
            this.groupBox1.Controls.Add(this.cmb_PaperSize);
            this.groupBox1.Controls.Add(this.lbl_PaperSize);
            this.groupBox1.Controls.Add(this.gb_Copies);
            this.groupBox1.Controls.Add(this.gb_PageRange);
            this.groupBox1.Controls.Add(this.cmb_Printer);
            this.groupBox1.Controls.Add(this.lbl_PrinterName);
            this.groupBox1.Location = new System.Drawing.Point(6, -1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(335, 258);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // btn_Print
            // 
            this.btn_Print.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Print.Location = new System.Drawing.Point(128, 226);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(70, 26);
            this.btn_Print.TabIndex = 83;
            this.btn_Print.Text = "&Print";
            this.btn_Print.UseVisualStyleBackColor = true;
            this.btn_Print.Click += new System.EventHandler(this.btn_Print_Click);
            // 
            // chk_PageBreak
            // 
            this.chk_PageBreak.Font = new System.Drawing.Font("Arial", 9F);
            this.chk_PageBreak.Location = new System.Drawing.Point(228, 187);
            this.chk_PageBreak.Name = "chk_PageBreak";
            this.chk_PageBreak.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_PageBreak.Size = new System.Drawing.Size(101, 19);
            this.chk_PageBreak.TabIndex = 82;
            this.chk_PageBreak.Text = "Page Break";
            this.chk_PageBreak.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_PageBreak.UseVisualStyleBackColor = true;
            // 
            // gb_PaperOrientation
            // 
            this.gb_PaperOrientation.Controls.Add(this.rb_Landscape);
            this.gb_PaperOrientation.Controls.Add(this.rb_Portrait);
            this.gb_PaperOrientation.Location = new System.Drawing.Point(5, 168);
            this.gb_PaperOrientation.Name = "gb_PaperOrientation";
            this.gb_PaperOrientation.Size = new System.Drawing.Size(220, 50);
            this.gb_PaperOrientation.TabIndex = 81;
            this.gb_PaperOrientation.TabStop = false;
            this.gb_PaperOrientation.Text = "Paper Orientation";
            // 
            // rb_Landscape
            // 
            this.rb_Landscape.AutoSize = true;
            this.rb_Landscape.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_Landscape.Location = new System.Drawing.Point(123, 19);
            this.rb_Landscape.Name = "rb_Landscape";
            this.rb_Landscape.Size = new System.Drawing.Size(87, 19);
            this.rb_Landscape.TabIndex = 76;
            this.rb_Landscape.TabStop = true;
            this.rb_Landscape.Text = "Landscape";
            this.rb_Landscape.UseVisualStyleBackColor = true;
            // 
            // rb_Portrait
            // 
            this.rb_Portrait.AutoSize = true;
            this.rb_Portrait.Checked = true;
            this.rb_Portrait.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_Portrait.Location = new System.Drawing.Point(9, 22);
            this.rb_Portrait.Name = "rb_Portrait";
            this.rb_Portrait.Size = new System.Drawing.Size(64, 19);
            this.rb_Portrait.TabIndex = 75;
            this.rb_Portrait.TabStop = true;
            this.rb_Portrait.Text = "Portrait";
            this.rb_Portrait.UseVisualStyleBackColor = true;
            // 
            // cmb_PaperSize
            // 
            this.cmb_PaperSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_PaperSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_PaperSize.FormattingEnabled = true;
            this.cmb_PaperSize.Location = new System.Drawing.Point(91, 147);
            this.cmb_PaperSize.Name = "cmb_PaperSize";
            this.cmb_PaperSize.Size = new System.Drawing.Size(224, 21);
            this.cmb_PaperSize.TabIndex = 79;
            // 
            // lbl_PaperSize
            // 
            this.lbl_PaperSize.AutoSize = true;
            this.lbl_PaperSize.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_PaperSize.Location = new System.Drawing.Point(4, 149);
            this.lbl_PaperSize.Name = "lbl_PaperSize";
            this.lbl_PaperSize.Size = new System.Drawing.Size(66, 15);
            this.lbl_PaperSize.TabIndex = 80;
            this.lbl_PaperSize.Text = "Paper Size";
            // 
            // gb_Copies
            // 
            this.gb_Copies.Controls.Add(this.txt_NoOfCopies);
            this.gb_Copies.Controls.Add(this.lbl_NoOfCopies);
            this.gb_Copies.Location = new System.Drawing.Point(3, 94);
            this.gb_Copies.Name = "gb_Copies";
            this.gb_Copies.Size = new System.Drawing.Size(329, 45);
            this.gb_Copies.TabIndex = 78;
            this.gb_Copies.TabStop = false;
            this.gb_Copies.Text = "Copies";
            // 
            // txt_NoOfCopies
            // 
            this.txt_NoOfCopies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_NoOfCopies.Location = new System.Drawing.Point(122, 17);
            this.txt_NoOfCopies.Name = "txt_NoOfCopies";
            this.txt_NoOfCopies.Size = new System.Drawing.Size(37, 20);
            this.txt_NoOfCopies.TabIndex = 78;
            this.txt_NoOfCopies.Text = "1";
            this.txt_NoOfCopies.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl_NoOfCopies
            // 
            this.lbl_NoOfCopies.AutoSize = true;
            this.lbl_NoOfCopies.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_NoOfCopies.Location = new System.Drawing.Point(9, 21);
            this.lbl_NoOfCopies.Name = "lbl_NoOfCopies";
            this.lbl_NoOfCopies.Size = new System.Drawing.Size(81, 15);
            this.lbl_NoOfCopies.TabIndex = 77;
            this.lbl_NoOfCopies.Text = "No Of Copies";
            // 
            // gb_PageRange
            // 
            this.gb_PageRange.Controls.Add(this.txt_To);
            this.gb_PageRange.Controls.Add(this.txt_From);
            this.gb_PageRange.Controls.Add(this.lbl_To);
            this.gb_PageRange.Controls.Add(this.rb_Pages);
            this.gb_PageRange.Controls.Add(this.rb_All);
            this.gb_PageRange.Location = new System.Drawing.Point(3, 41);
            this.gb_PageRange.Name = "gb_PageRange";
            this.gb_PageRange.Size = new System.Drawing.Size(327, 50);
            this.gb_PageRange.TabIndex = 77;
            this.gb_PageRange.TabStop = false;
            this.gb_PageRange.Text = "Page Range";
            // 
            // txt_To
            // 
            this.txt_To.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_To.Location = new System.Drawing.Point(279, 21);
            this.txt_To.Name = "txt_To";
            this.txt_To.Size = new System.Drawing.Size(37, 20);
            this.txt_To.TabIndex = 79;
            // 
            // txt_From
            // 
            this.txt_From.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_From.Location = new System.Drawing.Point(197, 20);
            this.txt_From.Name = "txt_From";
            this.txt_From.Size = new System.Drawing.Size(37, 20);
            this.txt_From.TabIndex = 77;
            // 
            // lbl_To
            // 
            this.lbl_To.AutoSize = true;
            this.lbl_To.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_To.Location = new System.Drawing.Point(241, 21);
            this.lbl_To.Name = "lbl_To";
            this.lbl_To.Size = new System.Drawing.Size(20, 15);
            this.lbl_To.TabIndex = 78;
            this.lbl_To.Text = "To";
            // 
            // rb_Pages
            // 
            this.rb_Pages.AutoSize = true;
            this.rb_Pages.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_Pages.Location = new System.Drawing.Point(119, 19);
            this.rb_Pages.Name = "rb_Pages";
            this.rb_Pages.Size = new System.Drawing.Size(61, 19);
            this.rb_Pages.TabIndex = 76;
            this.rb_Pages.TabStop = true;
            this.rb_Pages.Text = "Pages";
            this.rb_Pages.UseVisualStyleBackColor = true;
            this.rb_Pages.CheckedChanged += new System.EventHandler(this.rb_Pages_CheckedChanged);
            // 
            // rb_All
            // 
            this.rb_All.AutoSize = true;
            this.rb_All.Checked = true;
            this.rb_All.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_All.Location = new System.Drawing.Point(9, 22);
            this.rb_All.Name = "rb_All";
            this.rb_All.Size = new System.Drawing.Size(38, 19);
            this.rb_All.TabIndex = 75;
            this.rb_All.TabStop = true;
            this.rb_All.Text = "All";
            this.rb_All.UseVisualStyleBackColor = true;
            this.rb_All.CheckedChanged += new System.EventHandler(this.rb_All_CheckedChanged);
            // 
            // cmb_Printer
            // 
            this.cmb_Printer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Printer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_Printer.FormattingEnabled = true;
            this.cmb_Printer.Items.AddRange(new object[] {
            "Local",
            "Foreign",
            "Both"});
            this.cmb_Printer.Location = new System.Drawing.Point(93, 14);
            this.cmb_Printer.Name = "cmb_Printer";
            this.cmb_Printer.Size = new System.Drawing.Size(224, 21);
            this.cmb_Printer.TabIndex = 61;
            // 
            // lbl_PrinterName
            // 
            this.lbl_PrinterName.AutoSize = true;
            this.lbl_PrinterName.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_PrinterName.Location = new System.Drawing.Point(6, 16);
            this.lbl_PrinterName.Name = "lbl_PrinterName";
            this.lbl_PrinterName.Size = new System.Drawing.Size(80, 15);
            this.lbl_PrinterName.TabIndex = 62;
            this.lbl_PrinterName.Text = "Printer Name";
            // 
            // PrintDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 260);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "PrintDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Print Dialog";
            this.Load += new System.EventHandler(this.PrintDialog_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PrintDialog_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gb_PaperOrientation.ResumeLayout(false);
            this.gb_PaperOrientation.PerformLayout();
            this.gb_Copies.ResumeLayout(false);
            this.gb_Copies.PerformLayout();
            this.gb_PageRange.ResumeLayout(false);
            this.gb_PageRange.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private Button btn_Print;
        private CheckBox chk_PageBreak;
        private GroupBox gb_PaperOrientation;
        private RadioButton rb_Landscape;
        private RadioButton rb_Portrait;
        private ComboBox cmb_PaperSize;
        private Label lbl_PaperSize;
        private GroupBox gb_Copies;
        private TextBox txt_NoOfCopies;
        private Label lbl_NoOfCopies;
        private GroupBox gb_PageRange;
        private TextBox txt_To;
        private TextBox txt_From;
        private Label lbl_To;
        private RadioButton rb_Pages;
        private RadioButton rb_All;
        private ComboBox cmb_Printer;
        private Label lbl_PrinterName;
    }
}