using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.DataEntry.Common
{
    partial class FrmDocumentPrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDocumentPrint));
            this.BtnVoucherNoTo = new System.Windows.Forms.Button();
            this.BtnVoucherNoFrom = new System.Windows.Forms.Button();
            this.TxtNoOfCopies = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtTO = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtFrom = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_NoofCopy = new System.Windows.Forms.Label();
            this.lbl_To = new System.Windows.Forms.Label();
            this.lbl_From = new System.Windows.Forms.Label();
            this.CmbReportType = new System.Windows.Forms.ComboBox();
            this.CmbDesign = new System.Windows.Forms.ComboBox();
            this.CmbPrinter = new System.Windows.Forms.ComboBox();
            this.lbl_Type = new System.Windows.Forms.Label();
            this.lbl_DesignName = new System.Windows.Forms.Label();
            this.lbl_PrinterName = new System.Windows.Forms.Label();
            this.MskToDate = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.MskFrom = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.BtnPreview = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.PanelHeader = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.mrGroup1 = new MrDAL.Control.ControlsEx.Control.MrGroup();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.PanelHeader.SuspendLayout();
            this.mrGroup1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnVoucherNoTo
            // 
            this.BtnVoucherNoTo.CausesValidation = false;
            this.BtnVoucherNoTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnVoucherNoTo.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnVoucherNoTo.Image = ((System.Drawing.Image)(resources.GetObject("BtnVoucherNoTo.Image")));
            this.BtnVoucherNoTo.Location = new System.Drawing.Point(534, 46);
            this.BtnVoucherNoTo.Name = "BtnVoucherNoTo";
            this.BtnVoucherNoTo.Size = new System.Drawing.Size(27, 25);
            this.BtnVoucherNoTo.TabIndex = 9;
            this.BtnVoucherNoTo.TabStop = false;
            this.BtnVoucherNoTo.UseVisualStyleBackColor = false;
            this.BtnVoucherNoTo.Visible = false;
            this.BtnVoucherNoTo.Click += new System.EventHandler(this.BtnTo_Click);
            // 
            // BtnVoucherNoFrom
            // 
            this.BtnVoucherNoFrom.CausesValidation = false;
            this.BtnVoucherNoFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnVoucherNoFrom.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnVoucherNoFrom.Image = ((System.Drawing.Image)(resources.GetObject("BtnVoucherNoFrom.Image")));
            this.BtnVoucherNoFrom.Location = new System.Drawing.Point(255, 46);
            this.BtnVoucherNoFrom.Name = "BtnVoucherNoFrom";
            this.BtnVoucherNoFrom.Size = new System.Drawing.Size(27, 25);
            this.BtnVoucherNoFrom.TabIndex = 8;
            this.BtnVoucherNoFrom.TabStop = false;
            this.BtnVoucherNoFrom.UseVisualStyleBackColor = false;
            this.BtnVoucherNoFrom.Visible = false;
            this.BtnVoucherNoFrom.Click += new System.EventHandler(this.BtnFrom_Click);
            // 
            // TxtNoOfCopies
            // 
            this.TxtNoOfCopies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNoOfCopies.Location = new System.Drawing.Point(438, 17);
            this.TxtNoOfCopies.MaxLength = 50;
            this.TxtNoOfCopies.Name = "TxtNoOfCopies";
            this.TxtNoOfCopies.Size = new System.Drawing.Size(96, 26);
            this.TxtNoOfCopies.TabIndex = 2;
            this.TxtNoOfCopies.Text = "1";
            this.TxtNoOfCopies.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtNoOfCopies.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNoOfCopy_KeyPress);
            // 
            // TxtTO
            // 
            this.TxtTO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTO.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTO.Location = new System.Drawing.Point(408, 46);
            this.TxtTO.Name = "TxtTO";
            this.TxtTO.Size = new System.Drawing.Size(126, 24);
            this.TxtTO.TabIndex = 4;
            this.TxtTO.Visible = false;
            this.TxtTO.Enter += new System.EventHandler(this.TxtTo_Enter);
            this.TxtTO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtTo_KeyDown);
            this.TxtTO.Leave += new System.EventHandler(this.TxtTo_Leave);
            this.TxtTO.Validated += new System.EventHandler(this.TxtTo_Validated);
            // 
            // TxtFrom
            // 
            this.TxtFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtFrom.Location = new System.Drawing.Point(127, 46);
            this.TxtFrom.Name = "TxtFrom";
            this.TxtFrom.Size = new System.Drawing.Size(126, 24);
            this.TxtFrom.TabIndex = 3;
            this.TxtFrom.Visible = false;
            this.TxtFrom.Enter += new System.EventHandler(this.TxtFrom_Enter);
            this.TxtFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtFrom_KeyDown);
            this.TxtFrom.Leave += new System.EventHandler(this.TxtFrom_Leave);
            // 
            // lbl_NoofCopy
            // 
            this.lbl_NoofCopy.AutoSize = true;
            this.lbl_NoofCopy.Location = new System.Drawing.Point(347, 20);
            this.lbl_NoofCopy.Name = "lbl_NoofCopy";
            this.lbl_NoofCopy.Size = new System.Drawing.Size(94, 20);
            this.lbl_NoofCopy.TabIndex = 8;
            this.lbl_NoofCopy.Text = "No of Copy";
            // 
            // lbl_To
            // 
            this.lbl_To.AutoSize = true;
            this.lbl_To.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_To.Location = new System.Drawing.Point(323, 49);
            this.lbl_To.Name = "lbl_To";
            this.lbl_To.Size = new System.Drawing.Size(61, 18);
            this.lbl_To.TabIndex = 7;
            this.lbl_To.Text = "To Date";
            // 
            // lbl_From
            // 
            this.lbl_From.AutoSize = true;
            this.lbl_From.Location = new System.Drawing.Point(5, 47);
            this.lbl_From.Name = "lbl_From";
            this.lbl_From.Size = new System.Drawing.Size(92, 20);
            this.lbl_From.TabIndex = 6;
            this.lbl_From.Text = "From Date";
            // 
            // CmbReportType
            // 
            this.CmbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbReportType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbReportType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbReportType.FormattingEnabled = true;
            this.CmbReportType.Location = new System.Drawing.Point(126, 17);
            this.CmbReportType.Name = "CmbReportType";
            this.CmbReportType.Size = new System.Drawing.Size(217, 26);
            this.CmbReportType.TabIndex = 1;
            this.CmbReportType.SelectedIndexChanged += new System.EventHandler(this.CmbReportType_SelectedIndexChanged);
            // 
            // CmbDesign
            // 
            this.CmbDesign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbDesign.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbDesign.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbDesign.FormattingEnabled = true;
            this.CmbDesign.Location = new System.Drawing.Point(126, 103);
            this.CmbDesign.Name = "CmbDesign";
            this.CmbDesign.Size = new System.Drawing.Size(437, 26);
            this.CmbDesign.TabIndex = 8;
            // 
            // CmbPrinter
            // 
            this.CmbPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbPrinter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbPrinter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbPrinter.FormattingEnabled = true;
            this.CmbPrinter.Location = new System.Drawing.Point(126, 74);
            this.CmbPrinter.Name = "CmbPrinter";
            this.CmbPrinter.Size = new System.Drawing.Size(437, 26);
            this.CmbPrinter.TabIndex = 7;
            // 
            // lbl_Type
            // 
            this.lbl_Type.AutoSize = true;
            this.lbl_Type.Location = new System.Drawing.Point(5, 20);
            this.lbl_Type.Name = "lbl_Type";
            this.lbl_Type.Size = new System.Drawing.Size(46, 20);
            this.lbl_Type.TabIndex = 2;
            this.lbl_Type.Text = "Type";
            // 
            // lbl_DesignName
            // 
            this.lbl_DesignName.AutoSize = true;
            this.lbl_DesignName.Location = new System.Drawing.Point(5, 104);
            this.lbl_DesignName.Name = "lbl_DesignName";
            this.lbl_DesignName.Size = new System.Drawing.Size(92, 20);
            this.lbl_DesignName.TabIndex = 1;
            this.lbl_DesignName.Text = "Paper Size";
            // 
            // lbl_PrinterName
            // 
            this.lbl_PrinterName.AutoSize = true;
            this.lbl_PrinterName.Location = new System.Drawing.Point(5, 75);
            this.lbl_PrinterName.Name = "lbl_PrinterName";
            this.lbl_PrinterName.Size = new System.Drawing.Size(64, 20);
            this.lbl_PrinterName.TabIndex = 0;
            this.lbl_PrinterName.Text = "Printer";
            // 
            // MskToDate
            // 
            this.MskToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskToDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskToDate.Location = new System.Drawing.Point(408, 46);
            this.MskToDate.Mask = "00/00/0000";
            this.MskToDate.Name = "MskToDate";
            this.MskToDate.Size = new System.Drawing.Size(126, 24);
            this.MskToDate.TabIndex = 6;
            this.MskToDate.Enter += new System.EventHandler(this.MskToDate_Enter);
            this.MskToDate.Leave += new System.EventHandler(this.MskToDate_Leave);
            // 
            // MskFrom
            // 
            this.MskFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskFrom.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskFrom.Location = new System.Drawing.Point(126, 46);
            this.MskFrom.Mask = "00/00/0000";
            this.MskFrom.Name = "MskFrom";
            this.MskFrom.Size = new System.Drawing.Size(126, 24);
            this.MskFrom.TabIndex = 5;
            this.MskFrom.Enter += new System.EventHandler(this.MskFromDate_Enter);
            this.MskFrom.Leave += new System.EventHandler(this.MskFromDate_Leave);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // BtnPrint
            // 
            this.BtnPrint.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPrint.Appearance.Options.UseFont = true;
            this.BtnPrint.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnPrint.ImageOptions.Image = global::MrBLL.Properties.Resources.Printer24;
            this.BtnPrint.Location = new System.Drawing.Point(241, 135);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(90, 37);
            this.BtnPrint.TabIndex = 0;
            this.BtnPrint.Text = "&PRINT";
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // BtnPreview
            // 
            this.BtnPreview.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPreview.Appearance.Options.UseFont = true;
            this.BtnPreview.ImageOptions.Image = global::MrBLL.Properties.Resources.Printerview;
            this.BtnPreview.Location = new System.Drawing.Point(332, 135);
            this.BtnPreview.Name = "BtnPreview";
            this.BtnPreview.Size = new System.Drawing.Size(110, 37);
            this.BtnPreview.TabIndex = 1;
            this.BtnPreview.Text = "P&REVIEW";
            this.BtnPreview.Click += new System.EventHandler(this.BtnPreview_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(443, 135);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(108, 37);
            this.BtnCancel.TabIndex = 2;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.mrGroup1);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(569, 176);
            this.PanelHeader.TabIndex = 40;
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrDAL.Control.ControlsEx.Control.MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.BtnVoucherNoTo);
            this.mrGroup1.Controls.Add(this.CmbReportType);
            this.mrGroup1.Controls.Add(this.clsSeparator1);
            this.mrGroup1.Controls.Add(this.lbl_Type);
            this.mrGroup1.Controls.Add(this.lbl_PrinterName);
            this.mrGroup1.Controls.Add(this.CmbPrinter);
            this.mrGroup1.Controls.Add(this.lbl_DesignName);
            this.mrGroup1.Controls.Add(this.BtnPrint);
            this.mrGroup1.Controls.Add(this.lbl_From);
            this.mrGroup1.Controls.Add(this.TxtNoOfCopies);
            this.mrGroup1.Controls.Add(this.MskFrom);
            this.mrGroup1.Controls.Add(this.CmbDesign);
            this.mrGroup1.Controls.Add(this.MskToDate);
            this.mrGroup1.Controls.Add(this.BtnVoucherNoFrom);
            this.mrGroup1.Controls.Add(this.BtnPreview);
            this.mrGroup1.Controls.Add(this.TxtTO);
            this.mrGroup1.Controls.Add(this.BtnCancel);
            this.mrGroup1.Controls.Add(this.lbl_NoofCopy);
            this.mrGroup1.Controls.Add(this.lbl_To);
            this.mrGroup1.Controls.Add(this.TxtFrom);
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
            this.mrGroup1.ShadowControl = false;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(569, 176);
            this.mrGroup1.TabIndex = 13;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(4, 132);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(561, 2);
            this.clsSeparator1.TabIndex = 11;
            this.clsSeparator1.TabStop = false;
            // 
            // FrmDocumentPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(569, 176);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmDocumentPrint";
            this.ShowIcon = false;
            this.Text = "Voucher Printing";
            this.Load += new System.EventHandler(this.FrmDocumentPrint_Load);
            this.Shown += new System.EventHandler(this.FrmDocumentPrint_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmDocumentPrint_KeyPress);
            this.PanelHeader.ResumeLayout(false);
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl_NoofCopy;
        private System.Windows.Forms.Label lbl_To;
        private System.Windows.Forms.Label lbl_From;
        private System.Windows.Forms.ComboBox CmbReportType;
        private System.Windows.Forms.ComboBox CmbDesign;
        private System.Windows.Forms.ComboBox CmbPrinter;
        private System.Windows.Forms.Label lbl_Type;
        private System.Windows.Forms.Label lbl_DesignName;
        private System.Windows.Forms.Label lbl_PrinterName;
        private System.Windows.Forms.Button BtnVoucherNoTo;
        private System.Windows.Forms.Button BtnVoucherNoFrom;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private DevExpress.XtraEditors.SimpleButton BtnPrint;
        private DevExpress.XtraEditors.SimpleButton BtnPreview;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private ClsSeparator clsSeparator1;
        private MrTextBox TxtNoOfCopies;
        private MrTextBox TxtTO;
        private MrTextBox TxtFrom;
        private MrMaskedTextBox MskToDate;
        private MrMaskedTextBox MskFrom;
        private MrGroup mrGroup1;
        private MrPanel PanelHeader;
    }
}