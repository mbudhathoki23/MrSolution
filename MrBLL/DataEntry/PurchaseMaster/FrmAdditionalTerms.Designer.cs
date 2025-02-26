using System.Windows.Forms;
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.DataEntry.PurchaseMaster
{
    partial class FrmAdditionalTerms
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAdditionalTerms));
            this.BillGrid = new EntryGridViewEx();
            this.GTxtTermSno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtTermId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtTerm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtLedgerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtCbLedgerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtLedger = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtSubledgerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtSubLedger = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtTermType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtSign = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtInvoiceNetAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.mrPanel1 = new MrPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LblBasicAmount = new System.Windows.Forms.Label();
            this.lblBillAmount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.BillGrid)).BeginInit();
            this.mrPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BillGrid
            // 
            this.BillGrid.AllowUserToAddRows = false;
            this.BillGrid.AllowUserToDeleteRows = false;
            this.BillGrid.AllowUserToResizeColumns = false;
            this.BillGrid.AllowUserToResizeRows = false;
            this.BillGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.BillGrid.CausesValidation = false;
            this.BillGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.BillGrid.ColumnHeadersHeight = 25;
            this.BillGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GTxtTermSno,
            this.GTxtTermId,
            this.GTxtTerm,
            this.GTxtLedgerId,
            this.GTxtCbLedgerId,
            this.GTxtLedger,
            this.GTxtSubledgerId,
            this.GTxtSubLedger,
            this.GTxtTermType,
            this.GTxtSign,
            this.GTxtRate,
            this.GTxtAmount,
            this.GTxtInvoiceNetAmount});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.BillGrid.DefaultCellStyle = dataGridViewCellStyle6;
            this.BillGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.BillGrid.Location = new System.Drawing.Point(3, 38);
            this.BillGrid.MultiSelect = false;
            this.BillGrid.Name = "BillGrid";
            this.BillGrid.ReadOnly = true;
            this.BillGrid.RowHeadersVisible = false;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            this.BillGrid.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.BillGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BillGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.BillGrid.Size = new System.Drawing.Size(869, 245);
            this.BillGrid.StandardTab = true;
            this.BillGrid.TabIndex = 0;
            this.BillGrid.TabStop = false;
            this.BillGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BillGrid_KeyDown);
            // 
            // GTxtTermSno
            // 
            this.GTxtTermSno.HeaderText = "SNO";
            this.GTxtTermSno.Name = "GTxtTermSno";
            this.GTxtTermSno.ReadOnly = true;
            this.GTxtTermSno.Width = 65;
            // 
            // GTxtTermId
            // 
            this.GTxtTermId.HeaderText = "TermId";
            this.GTxtTermId.Name = "GTxtTermId";
            this.GTxtTermId.ReadOnly = true;
            this.GTxtTermId.Visible = false;
            this.GTxtTermId.Width = 5;
            // 
            // GTxtTerm
            // 
            this.GTxtTerm.HeaderText = "TERM";
            this.GTxtTerm.Name = "GTxtTerm";
            this.GTxtTerm.ReadOnly = true;
            this.GTxtTerm.Width = 150;
            // 
            // GTxtLedgerId
            // 
            this.GTxtLedgerId.HeaderText = "LedgerId";
            this.GTxtLedgerId.Name = "GTxtLedgerId";
            this.GTxtLedgerId.ReadOnly = true;
            this.GTxtLedgerId.Visible = false;
            this.GTxtLedgerId.Width = 5;
            // 
            // GTxtCbLedgerId
            // 
            this.GTxtCbLedgerId.HeaderText = "CbLedgerId";
            this.GTxtCbLedgerId.Name = "GTxtCbLedgerId";
            this.GTxtCbLedgerId.ReadOnly = true;
            this.GTxtCbLedgerId.Visible = false;
            // 
            // GTxtLedger
            // 
            this.GTxtLedger.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.GTxtLedger.HeaderText = "LEDGER";
            this.GTxtLedger.Name = "GTxtLedger";
            this.GTxtLedger.ReadOnly = true;
            // 
            // GTxtSubledgerId
            // 
            this.GTxtSubledgerId.HeaderText = "SubledgerId";
            this.GTxtSubledgerId.Name = "GTxtSubledgerId";
            this.GTxtSubledgerId.ReadOnly = true;
            this.GTxtSubledgerId.Visible = false;
            this.GTxtSubledgerId.Width = 5;
            // 
            // GTxtSubLedger
            // 
            this.GTxtSubLedger.HeaderText = "SUB_LEDGER";
            this.GTxtSubLedger.Name = "GTxtSubLedger";
            this.GTxtSubLedger.ReadOnly = true;
            this.GTxtSubLedger.Visible = false;
            // 
            // GTxtTermType
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.GTxtTermType.DefaultCellStyle = dataGridViewCellStyle1;
            this.GTxtTermType.HeaderText = "TT";
            this.GTxtTermType.Name = "GTxtTermType";
            this.GTxtTermType.ReadOnly = true;
            this.GTxtTermType.Width = 45;
            // 
            // GTxtSign
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.GTxtSign.DefaultCellStyle = dataGridViewCellStyle2;
            this.GTxtSign.HeaderText = "SIGN";
            this.GTxtSign.Name = "GTxtSign";
            this.GTxtSign.ReadOnly = true;
            this.GTxtSign.Width = 65;
            // 
            // GTxtRate
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GTxtRate.DefaultCellStyle = dataGridViewCellStyle3;
            this.GTxtRate.HeaderText = "RATE";
            this.GTxtRate.Name = "GTxtRate";
            this.GTxtRate.ReadOnly = true;
            this.GTxtRate.Width = 120;
            // 
            // GTxtAmount
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GTxtAmount.DefaultCellStyle = dataGridViewCellStyle4;
            this.GTxtAmount.HeaderText = "AMOUNT";
            this.GTxtAmount.Name = "GTxtAmount";
            this.GTxtAmount.ReadOnly = true;
            this.GTxtAmount.Width = 120;
            // 
            // GTxtInvoiceNetAmount
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GTxtInvoiceNetAmount.DefaultCellStyle = dataGridViewCellStyle5;
            this.GTxtInvoiceNetAmount.HeaderText = "NET_AMOUNT";
            this.GTxtInvoiceNetAmount.Name = "GTxtInvoiceNetAmount";
            this.GTxtInvoiceNetAmount.ReadOnly = true;
            this.GTxtInvoiceNetAmount.Width = 150;
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(328, 286);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(92, 34);
            this.BtnSave.TabIndex = 1;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(422, 286);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(108, 34);
            this.BtnCancel.TabIndex = 2;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // mrPanel1
            // 
            this.mrPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel1.Controls.Add(this.label1);
            this.mrPanel1.Controls.Add(this.label2);
            this.mrPanel1.Controls.Add(this.LblBasicAmount);
            this.mrPanel1.Controls.Add(this.lblBillAmount);
            this.mrPanel1.Controls.Add(this.BtnCancel);
            this.mrPanel1.Controls.Add(this.BtnSave);
            this.mrPanel1.Controls.Add(this.BillGrid);
            this.mrPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrPanel1.Location = new System.Drawing.Point(0, 0);
            this.mrPanel1.Name = "mrPanel1";
            this.mrPanel1.Size = new System.Drawing.Size(875, 322);
            this.mrPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(696, 286);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(176, 30);
            this.label1.TabIndex = 102;
            this.label1.Text = "0.00";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(575, 292);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 19);
            this.label2.TabIndex = 101;
            this.label2.Text = "Basic Amount";
            // 
            // LblBasicAmount
            // 
            this.LblBasicAmount.BackColor = System.Drawing.SystemColors.HighlightText;
            this.LblBasicAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblBasicAmount.Font = new System.Drawing.Font("Bookman Old Style", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblBasicAmount.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.LblBasicAmount.Location = new System.Drawing.Point(696, 5);
            this.LblBasicAmount.Name = "LblBasicAmount";
            this.LblBasicAmount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LblBasicAmount.Size = new System.Drawing.Size(176, 30);
            this.LblBasicAmount.TabIndex = 100;
            this.LblBasicAmount.Text = "0.00";
            this.LblBasicAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBillAmount
            // 
            this.lblBillAmount.AutoSize = true;
            this.lblBillAmount.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillAmount.Location = new System.Drawing.Point(575, 11);
            this.lblBillAmount.Name = "lblBillAmount";
            this.lblBillAmount.Size = new System.Drawing.Size(113, 19);
            this.lblBillAmount.TabIndex = 99;
            this.lblBillAmount.Text = "Basic Amount";
            // 
            // FrmAdditionalTerms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 322);
            this.Controls.Add(this.mrPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmAdditionalTerms";
            this.Text = "Additional Terms";
            this.Load += new System.EventHandler(this.FrmAdditionalTerms_Load);
            this.Shown += new System.EventHandler(this.FrmAdditionalTerms_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmAdditionalTerms_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.BillGrid)).EndInit();
            this.mrPanel1.ResumeLayout(false);
            this.mrPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView BillGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtTermSno;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtTermId;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtTerm;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtLedgerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtCbLedgerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtLedger;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtSubledgerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtSubLedger;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtTermType;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtSign;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtInvoiceNetAmount;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private MrPanel mrPanel1;
        private System.Windows.Forms.Label LblBasicAmount;
        private System.Windows.Forms.Label lblBillAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}