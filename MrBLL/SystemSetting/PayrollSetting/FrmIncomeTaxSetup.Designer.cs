namespace MrBLL.SystemSetting.PayrollSetting
{
    partial class FrmIncomeTaxSetup
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
            this.mrPanel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.DGrid = new MrDAL.Control.ControlsEx.Control.DataGridViewEx();
            this.GTxtSno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtParticular = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxSingleTaxAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtMarriedTaxAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtTaxChargePercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.mrPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mrPanel1
            // 
            this.mrPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel1.Controls.Add(this.DGrid);
            this.mrPanel1.Controls.Add(this.groupBox1);
            this.mrPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrPanel1.Location = new System.Drawing.Point(0, 0);
            this.mrPanel1.Name = "mrPanel1";
            this.mrPanel1.Size = new System.Drawing.Size(891, 458);
            this.mrPanel1.TabIndex = 0;
            // 
            // DGrid
            // 
            this.DGrid.AllowUserToDeleteRows = false;
            this.DGrid.AllowUserToResizeRows = false;
            this.DGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.DGrid.BlockNavigationOnNextRowOnEnter = true;
            this.DGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GTxtSno,
            this.GTxtParticular,
            this.GTxSingleTaxAmount,
            this.GTxtMarriedTaxAmount,
            this.GTxtTaxChargePercent});
            this.DGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGrid.DoubleBufferEnabled = true;
            this.DGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.DGrid.Location = new System.Drawing.Point(0, 0);
            this.DGrid.MultiSelect = false;
            this.DGrid.Name = "DGrid";
            this.DGrid.ReadOnly = true;
            this.DGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGrid.Size = new System.Drawing.Size(891, 399);
            this.DGrid.StandardTab = true;
            this.DGrid.TabIndex = 20;
            // 
            // GTxtSno
            // 
            this.GTxtSno.HeaderText = "Sno";
            this.GTxtSno.Name = "GTxtSno";
            this.GTxtSno.ReadOnly = true;
            this.GTxtSno.Width = 65;
            // 
            // GTxtParticular
            // 
            this.GTxtParticular.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.GTxtParticular.HeaderText = "Particulars";
            this.GTxtParticular.Name = "GTxtParticular";
            this.GTxtParticular.ReadOnly = true;
            // 
            // GTxSingleTaxAmount
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GTxSingleTaxAmount.DefaultCellStyle = dataGridViewCellStyle1;
            this.GTxSingleTaxAmount.HeaderText = "Single Amount";
            this.GTxSingleTaxAmount.Name = "GTxSingleTaxAmount";
            this.GTxSingleTaxAmount.ReadOnly = true;
            this.GTxSingleTaxAmount.Width = 160;
            // 
            // GTxtMarriedTaxAmount
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GTxtMarriedTaxAmount.DefaultCellStyle = dataGridViewCellStyle2;
            this.GTxtMarriedTaxAmount.HeaderText = "Married Amount";
            this.GTxtMarriedTaxAmount.Name = "GTxtMarriedTaxAmount";
            this.GTxtMarriedTaxAmount.ReadOnly = true;
            this.GTxtMarriedTaxAmount.Width = 160;
            // 
            // GTxtTaxChargePercent
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GTxtTaxChargePercent.DefaultCellStyle = dataGridViewCellStyle3;
            this.GTxtTaxChargePercent.HeaderText = "Tax Rate";
            this.GTxtTaxChargePercent.Name = "GTxtTaxChargePercent";
            this.GTxtTaxChargePercent.ReadOnly = true;
            this.GTxtTaxChargePercent.Width = 150;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnSave);
            this.groupBox1.Controls.Add(this.BtnCancel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 399);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(891, 59);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Appearance.Options.UseForeColor = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.OK;
            this.BtnSave.Location = new System.Drawing.Point(674, 17);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(89, 35);
            this.BtnSave.TabIndex = 17;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnCancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.Appearance.Options.UseForeColor = true;
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(764, 17);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(120, 35);
            this.BtnCancel.TabIndex = 18;
            this.BtnCancel.Text = "&CANCEL";
            // 
            // FrmIncomeTaxSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 458);
            this.Controls.Add(this.mrPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmIncomeTaxSetup";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "INCOME TAX RULES";
            this.Load += new System.EventHandler(this.FrmIncomeTaxSetup_Load);
            this.mrPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MrDAL.Control.ControlsEx.Control.MrPanel mrPanel1;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private MrDAL.Control.ControlsEx.Control.DataGridViewEx DGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtSno;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtParticular;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxSingleTaxAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtMarriedTaxAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtTaxChargePercent;
    }
}