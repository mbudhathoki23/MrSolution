using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Master.ProductSetup
{
    partial class FrmProductScheme
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.PanelHeader = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.BtnDescription = new System.Windows.Forms.Button();
            this.MskFrom = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.MskTo = new MrDAL.Control.ControlsEx.Control.MrMaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtDescription = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.GrpProductType = new System.Windows.Forms.GroupBox();
            this.rChkCustomer = new System.Windows.Forms.RadioButton();
            this.rChkProduct = new System.Windows.Forms.RadioButton();
            this.SGrid = new MrDAL.Control.ControlsEx.Control.EntryGridViewEx();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.PanelHeader.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.GrpProductType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SGrid)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.ImageOptions.Image = global::MrBLL.Properties.Resources.Exit;
            this.btnExit.Location = new System.Drawing.Point(1117, 15);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(101, 39);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "E&XIT";
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Appearance.Options.UseFont = true;
            this.BtnDelete.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnDelete.Location = new System.Drawing.Point(212, 15);
            this.BtnDelete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(137, 39);
            this.BtnDelete.TabIndex = 2;
            this.BtnDelete.Tag = "DeleteButtonCheck";
            this.BtnDelete.Text = "&DELETE";
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEdit.Appearance.Options.UseFont = true;
            this.BtnEdit.ImageOptions.Image = global::MrBLL.Properties.Resources.Edit;
            this.BtnEdit.Location = new System.Drawing.Point(103, 15);
            this.BtnEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(109, 39);
            this.BtnEdit.TabIndex = 1;
            this.BtnEdit.Tag = "EditButtonCheck";
            this.BtnEdit.Text = "&EDIT";
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnNew
            // 
            this.BtnNew.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNew.Appearance.Options.UseFont = true;
            this.BtnNew.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnNew.Location = new System.Drawing.Point(3, 15);
            this.BtnNew.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(100, 39);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Tag = "NewButtonCheck";
            this.BtnNew.Text = "&NEW";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.groupBox1);
            this.PanelHeader.Controls.Add(this.SGrid);
            this.PanelHeader.Controls.Add(this.groupBox2);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(1219, 630);
            this.PanelHeader.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.clsSeparator2);
            this.groupBox1.Controls.Add(this.BtnDescription);
            this.groupBox1.Controls.Add(this.MskFrom);
            this.groupBox1.Controls.Add(this.MskTo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TxtDescription);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.clsSeparator1);
            this.groupBox1.Controls.Add(this.BtnDelete);
            this.groupBox1.Controls.Add(this.btnExit);
            this.groupBox1.Controls.Add(this.BtnNew);
            this.groupBox1.Controls.Add(this.BtnEdit);
            this.groupBox1.Controls.Add(this.GrpProductType);
            this.groupBox1.Location = new System.Drawing.Point(0, -11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(1219, 144);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(15, 98);
            this.clsSeparator2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clsSeparator2.Size = new System.Drawing.Size(1203, 2);
            this.clsSeparator2.TabIndex = 5;
            this.clsSeparator2.TabStop = false;
            // 
            // BtnDescription
            // 
            this.BtnDescription.Font = new System.Drawing.Font("Bookman Old Style", 10.75F);
            this.BtnDescription.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnDescription.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnDescription.Location = new System.Drawing.Point(765, 65);
            this.BtnDescription.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnDescription.Name = "BtnDescription";
            this.BtnDescription.Size = new System.Drawing.Size(37, 28);
            this.BtnDescription.TabIndex = 315;
            this.BtnDescription.TabStop = false;
            this.BtnDescription.UseVisualStyleBackColor = false;
            this.BtnDescription.Click += new System.EventHandler(this.BtnDescription_Click);
            // 
            // MskFrom
            // 
            this.MskFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskFrom.Enabled = false;
            this.MskFrom.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskFrom.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskFrom.Location = new System.Drawing.Point(871, 63);
            this.MskFrom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MskFrom.Mask = "00/00/0000";
            this.MskFrom.Name = "MskFrom";
            this.MskFrom.Size = new System.Drawing.Size(150, 29);
            this.MskFrom.TabIndex = 5;
            this.MskFrom.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.MskFrom_MaskInputRejected);
            this.MskFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyPress);
            this.MskFrom.Validating += new System.ComponentModel.CancelEventHandler(this.MskFrom_Validating);
            // 
            // MskTo
            // 
            this.MskTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MskTo.Font = new System.Drawing.Font("Bookman Old Style", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MskTo.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MskTo.Location = new System.Drawing.Point(1064, 63);
            this.MskTo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MskTo.Mask = "00/00/0000";
            this.MskTo.Name = "MskTo";
            this.MskTo.Size = new System.Drawing.Size(153, 29);
            this.MskTo.TabIndex = 6;
            this.MskTo.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.MskTo_MaskInputRejected);
            this.MskTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyPress);
            this.MskTo.Validating += new System.ComponentModel.CancelEventHandler(this.MskTo_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1020, 65);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 23);
            this.label3.TabIndex = 8;
            this.label3.Text = "To";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(799, 65);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 23);
            this.label2.TabIndex = 7;
            this.label2.Text = "From";
            // 
            // TxtDescription
            // 
            this.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDescription.Location = new System.Drawing.Point(145, 64);
            this.TxtDescription.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(617, 31);
            this.TxtDescription.TabIndex = 4;
            this.TxtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDescription_KeyDown);
            this.TxtDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GlobalKeyPress);
            this.TxtDescription.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDescription_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 68);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 23);
            this.label1.TabIndex = 5;
            this.label1.Text = "Descrption";
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(11, 58);
            this.clsSeparator1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clsSeparator1.Size = new System.Drawing.Size(1208, 2);
            this.clsSeparator1.TabIndex = 4;
            this.clsSeparator1.TabStop = false;
            // 
            // GrpProductType
            // 
            this.GrpProductType.Controls.Add(this.rChkCustomer);
            this.GrpProductType.Controls.Add(this.rChkProduct);
            this.GrpProductType.Location = new System.Drawing.Point(11, 91);
            this.GrpProductType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GrpProductType.Name = "GrpProductType";
            this.GrpProductType.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GrpProductType.Size = new System.Drawing.Size(1203, 49);
            this.GrpProductType.TabIndex = 7;
            this.GrpProductType.TabStop = false;
            // 
            // rChkCustomer
            // 
            this.rChkCustomer.AutoSize = true;
            this.rChkCustomer.Location = new System.Drawing.Point(201, 17);
            this.rChkCustomer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rChkCustomer.Name = "rChkCustomer";
            this.rChkCustomer.Size = new System.Drawing.Size(127, 27);
            this.rChkCustomer.TabIndex = 5;
            this.rChkCustomer.Text = "Customer";
            this.rChkCustomer.UseVisualStyleBackColor = true;
            // 
            // rChkProduct
            // 
            this.rChkProduct.AutoSize = true;
            this.rChkProduct.Checked = true;
            this.rChkProduct.Location = new System.Drawing.Point(8, 17);
            this.rChkProduct.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rChkProduct.Name = "rChkProduct";
            this.rChkProduct.Size = new System.Drawing.Size(159, 27);
            this.rChkProduct.TabIndex = 1;
            this.rChkProduct.TabStop = true;
            this.rChkProduct.Text = "Product Wise";
            this.rChkProduct.UseVisualStyleBackColor = true;
            this.rChkProduct.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rChkProduct_KeyPress);
            // 
            // SGrid
            // 
            this.SGrid.AllowUserToAddRows = false;
            this.SGrid.AllowUserToDeleteRows = false;
            this.SGrid.AllowUserToOrderColumns = true;
            this.SGrid.AllowUserToResizeColumns = false;
            this.SGrid.AllowUserToResizeRows = false;
            this.SGrid.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.SGrid.BlockNavigationOnNextRowOnEnter = true;
            this.SGrid.CausesValidation = false;
            this.SGrid.ColumnHeadersHeight = 29;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Bookman Old Style", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.SGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.SGrid.DoubleBufferEnabled = true;
            this.SGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.SGrid.Location = new System.Drawing.Point(0, 135);
            this.SGrid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SGrid.MultiSelect = false;
            this.SGrid.Name = "SGrid";
            this.SGrid.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Bookman Old Style", 9F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.SGrid.RowHeadersWidth = 25;
            this.SGrid.RowTemplate.ReadOnly = true;
            this.SGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.SGrid.Size = new System.Drawing.Size(1219, 443);
            this.SGrid.StandardTab = true;
            this.SGrid.TabIndex = 1;
            this.SGrid.TabStop = false;
            this.SGrid.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SGrid_CellContentDoubleClick);
            this.SGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.SGrid_RowEnter);
            this.SGrid.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.SGrid_RowHeaderMouseDoubleClick);
            this.SGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SGrid_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.BtnCancel);
            this.groupBox2.Controls.Add(this.BtnSave);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 571);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(1219, 59);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnCancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.Appearance.Options.UseForeColor = true;
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(917, 15);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(139, 42);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Appearance.Options.UseForeColor = true;
            this.BtnSave.Location = new System.Drawing.Point(797, 15);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(119, 42);
            this.BtnSave.TabIndex = 0;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // FrmProductScheme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1219, 630);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.MaximizeBox = false;
            this.Name = "FrmProductScheme";
            this.ShowIcon = false;
            this.Tag = "Product Scheme";
            this.Text = "Product Scheme";
            this.Load += new System.EventHandler(this.FrmProductScheme_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmProductScheme_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmProductScheme_KeyPress);
            this.PanelHeader.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.GrpProductType.ResumeLayout(false);
            this.GrpProductType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SGrid)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnEdit;
        private DevExpress.XtraEditors.SimpleButton BtnNew;
        private System.Windows.Forms.GroupBox groupBox1;
        private ClsSeparator clsSeparator1;
        private MrTextBox TxtDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Button BtnDescription;
        private ClsSeparator clsSeparator2;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private System.Windows.Forms.GroupBox GrpProductType;
        private System.Windows.Forms.RadioButton rChkProduct;
        private System.Windows.Forms.RadioButton rChkCustomer;
        private MrPanel PanelHeader;
        public MrMaskedTextBox MskFrom;
        private MrMaskedTextBox MskTo;
        private EntryGridViewEx SGrid;
    }
}