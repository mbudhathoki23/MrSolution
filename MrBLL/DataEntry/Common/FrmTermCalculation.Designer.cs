using System.Windows.Forms;
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.DataEntry.Common
{
    partial class FrmTermCalculation
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTermCalculation));
            this.panel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.LblTaxableAmount = new System.Windows.Forms.Label();
            this.LblTaxable = new System.Windows.Forms.Label();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.LblTotalNetTermAmount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.LblBasicAmount = new System.Windows.Forms.Label();
            this.lblBillAmount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DGrid = new MrDAL.Control.ControlsEx.Control.EntryGridViewEx();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.LblTaxableAmount);
            this.panel1.Controls.Add(this.LblTaxable);
            this.panel1.Controls.Add(this.clsSeparator1);
            this.panel1.Controls.Add(this.LblTotalNetTermAmount);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.BtnCancel);
            this.panel1.Controls.Add(this.BtnSave);
            this.panel1.Controls.Add(this.LblBasicAmount);
            this.panel1.Controls.Add(this.lblBillAmount);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.DGrid);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(636, 264);
            this.panel1.TabIndex = 0;
            // 
            // LblTaxableAmount
            // 
            this.LblTaxableAmount.BackColor = System.Drawing.SystemColors.HighlightText;
            this.LblTaxableAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTaxableAmount.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTaxableAmount.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.LblTaxableAmount.Location = new System.Drawing.Point(236, 199);
            this.LblTaxableAmount.Name = "LblTaxableAmount";
            this.LblTaxableAmount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LblTaxableAmount.Size = new System.Drawing.Size(136, 25);
            this.LblTaxableAmount.TabIndex = 116;
            this.LblTaxableAmount.Text = "0.00";
            this.LblTaxableAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblTaxable
            // 
            this.LblTaxable.AutoSize = true;
            this.LblTaxable.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTaxable.Location = new System.Drawing.Point(105, 202);
            this.LblTaxable.Name = "LblTaxable";
            this.LblTaxable.Size = new System.Drawing.Size(129, 19);
            this.LblTaxable.TabIndex = 115;
            this.LblTaxable.Text = "Taxable Amount";
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(9, 226);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(623, 2);
            this.clsSeparator1.TabIndex = 114;
            this.clsSeparator1.TabStop = false;
            // 
            // LblTotalNetTermAmount
            // 
            this.LblTotalNetTermAmount.BackColor = System.Drawing.SystemColors.HighlightText;
            this.LblTotalNetTermAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTotalNetTermAmount.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotalNetTermAmount.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.LblTotalNetTermAmount.Location = new System.Drawing.Point(497, 199);
            this.LblTotalNetTermAmount.Name = "LblTotalNetTermAmount";
            this.LblTotalNetTermAmount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LblTotalNetTermAmount.Size = new System.Drawing.Size(136, 25);
            this.LblTotalNetTermAmount.TabIndex = 113;
            this.LblTotalNetTermAmount.Text = "0.00";
            this.LblTotalNetTermAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(378, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 19);
            this.label3.TabIndex = 112;
            this.label3.Text = "Term Amount";
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.BtnCancel24;
            this.BtnCancel.Location = new System.Drawing.Point(525, 229);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(108, 33);
            this.BtnCancel.TabIndex = 2;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(431, 229);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(92, 33);
            this.BtnSave.TabIndex = 1;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // LblBasicAmount
            // 
            this.LblBasicAmount.BackColor = System.Drawing.SystemColors.HighlightText;
            this.LblBasicAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblBasicAmount.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblBasicAmount.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.LblBasicAmount.Location = new System.Drawing.Point(497, 5);
            this.LblBasicAmount.Name = "LblBasicAmount";
            this.LblBasicAmount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LblBasicAmount.Size = new System.Drawing.Size(136, 25);
            this.LblBasicAmount.TabIndex = 98;
            this.LblBasicAmount.Text = "0.00";
            this.LblBasicAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBillAmount
            // 
            this.lblBillAmount.AutoSize = true;
            this.lblBillAmount.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillAmount.Location = new System.Drawing.Point(381, 7);
            this.lblBillAmount.Name = "lblBillAmount";
            this.lblBillAmount.Size = new System.Drawing.Size(113, 19);
            this.lblBillAmount.TabIndex = 97;
            this.lblBillAmount.Text = "Basic Amount";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Billing Term Calculation";
            // 
            // DGrid
            // 
            this.DGrid.AllowUserToAddRows = false;
            this.DGrid.AllowUserToDeleteRows = false;
            this.DGrid.AllowUserToResizeColumns = false;
            this.DGrid.AllowUserToResizeRows = false;
            this.DGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.DGrid.BlockNavigationOnNextRowOnEnter = true;
            this.DGrid.CausesValidation = false;
            this.DGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.DGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.DGrid.ColumnHeadersHeight = 25;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGrid.DefaultCellStyle = dataGridViewCellStyle1;
            this.DGrid.DoubleBufferEnabled = true;
            this.DGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.DGrid.Location = new System.Drawing.Point(3, 32);
            this.DGrid.MultiSelect = false;
            this.DGrid.Name = "DGrid";
            this.DGrid.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Bookman Old Style", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DGrid.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            this.DGrid.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.DGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGrid.Size = new System.Drawing.Size(630, 165);
            this.DGrid.StandardTab = true;
            this.DGrid.TabIndex = 0;
            this.DGrid.TabStop = false;
            this.DGrid.EnterKeyPressed += new System.EventHandler<System.EventArgs>(this.DGrid_EnterKeyPressed);
            // 
            // FrmTermCalculation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 264);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmTermCalculation";
            this.ShowIcon = false;
            this.Text = "BILLING TERM CALCULATION";
            this.Load += new System.EventHandler(this.FrmTermCalculation_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmTermCalculation_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LblBasicAmount;
        private System.Windows.Forms.Label lblBillAmount;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private System.Windows.Forms.Label label3;
        private ClsSeparator clsSeparator1;
        private System.Windows.Forms.Label LblTotalNetTermAmount;
        private MrPanel panel1;
        private System.Windows.Forms.Label LblTaxableAmount;
        private System.Windows.Forms.Label LblTaxable;
        private EntryGridViewEx DGrid;
    }
}