using System.Windows.Forms;
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.Restro.Entry
{
    partial class FrmTablesList
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTablesList));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelTablelist = new System.Windows.Forms.FlowLayoutPanel();
            this.RGrid = new MrDAL.Control.ControlsEx.Control.MrDataGridView();
            this.SNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DESCRIPTION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AMOUNT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DISCOUNT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NETAMOUNT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mrGroup1 = new MrDAL.Control.ControlsEx.Control.MrGroup();
            this.label21 = new System.Windows.Forms.Label();
            this.LblItemsDiscountSum = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.LblItemsTotal = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.LblItemsTotalQty = new System.Windows.Forms.Label();
            this.LblItemsNetAmount = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.PanelFloorList = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.BtnAvailable = new System.Windows.Forms.Button();
            this.BtnAll = new System.Windows.Forms.Button();
            this.BtnOccupied = new System.Windows.Forms.Button();
            this.BtnBooked = new System.Windows.Forms.Button();
            this.BtnCombine = new System.Windows.Forms.Button();
            this.BtnDisiable = new System.Windows.Forms.Button();
            this.BtnPrint = new System.Windows.Forms.Button();
            this.panel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnTableSplit = new System.Windows.Forms.Button();
            this.BtnTableTransfer = new System.Windows.Forms.Button();
            this.ChkTableReport = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).BeginInit();
            this.mrGroup1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panelTablelist);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.RGrid);
            this.splitContainer1.Panel2.Controls.Add(this.mrGroup1);
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            // 
            // panelTablelist
            // 
            resources.ApplyResources(this.panelTablelist, "panelTablelist");
            this.panelTablelist.BackColor = System.Drawing.Color.SlateGray;
            this.panelTablelist.Name = "panelTablelist";
            // 
            // RGrid
            // 
            this.RGrid.AllowUserToAddRows = false;
            this.RGrid.AllowUserToDeleteRows = false;
            this.RGrid.AllowUserToResizeColumns = false;
            this.RGrid.AllowUserToResizeRows = false;
            this.RGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.RGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SNO,
            this.DESCRIPTION,
            this.QTY,
            this.RATE,
            this.AMOUNT,
            this.DISCOUNT,
            this.NETAMOUNT});
            resources.ApplyResources(this.RGrid, "RGrid");
            this.RGrid.DoubleBufferEnabled = true;
            this.RGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.RGrid.MultiSelect = false;
            this.RGrid.Name = "RGrid";
            this.RGrid.ReadOnly = true;
            this.RGrid.StandardTab = true;
            // 
            // SNO
            // 
            this.SNO.DataPropertyName = "Invoice_SNo";
            resources.ApplyResources(this.SNO, "SNO");
            this.SNO.Name = "SNO";
            this.SNO.ReadOnly = true;
            // 
            // DESCRIPTION
            // 
            this.DESCRIPTION.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DESCRIPTION.DataPropertyName = "PName";
            resources.ApplyResources(this.DESCRIPTION, "DESCRIPTION");
            this.DESCRIPTION.Name = "DESCRIPTION";
            this.DESCRIPTION.ReadOnly = true;
            // 
            // QTY
            // 
            this.QTY.DataPropertyName = "Qty";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.QTY.DefaultCellStyle = dataGridViewCellStyle17;
            resources.ApplyResources(this.QTY, "QTY");
            this.QTY.Name = "QTY";
            this.QTY.ReadOnly = true;
            // 
            // RATE
            // 
            this.RATE.DataPropertyName = "Rate";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.RATE.DefaultCellStyle = dataGridViewCellStyle18;
            resources.ApplyResources(this.RATE, "RATE");
            this.RATE.Name = "RATE";
            this.RATE.ReadOnly = true;
            // 
            // AMOUNT
            // 
            this.AMOUNT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.AMOUNT.DataPropertyName = "B_Amount";
            resources.ApplyResources(this.AMOUNT, "AMOUNT");
            this.AMOUNT.Name = "AMOUNT";
            this.AMOUNT.ReadOnly = true;
            // 
            // DISCOUNT
            // 
            this.DISCOUNT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.DISCOUNT.DataPropertyName = "T_Amount";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DISCOUNT.DefaultCellStyle = dataGridViewCellStyle19;
            resources.ApplyResources(this.DISCOUNT, "DISCOUNT");
            this.DISCOUNT.Name = "DISCOUNT";
            this.DISCOUNT.ReadOnly = true;
            // 
            // NETAMOUNT
            // 
            this.NETAMOUNT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.NETAMOUNT.DataPropertyName = "N_Amount";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.NETAMOUNT.DefaultCellStyle = dataGridViewCellStyle20;
            resources.ApplyResources(this.NETAMOUNT, "NETAMOUNT");
            this.NETAMOUNT.Name = "NETAMOUNT";
            this.NETAMOUNT.ReadOnly = true;
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrDAL.Control.ControlsEx.Control.MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.label21);
            this.mrGroup1.Controls.Add(this.LblItemsDiscountSum);
            this.mrGroup1.Controls.Add(this.label32);
            this.mrGroup1.Controls.Add(this.LblItemsTotal);
            this.mrGroup1.Controls.Add(this.label31);
            this.mrGroup1.Controls.Add(this.LblItemsTotalQty);
            this.mrGroup1.Controls.Add(this.LblItemsNetAmount);
            this.mrGroup1.Controls.Add(this.label29);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            resources.ApplyResources(this.mrGroup1, "mrGroup1");
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "";
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 5;
            this.mrGroup1.ShadowColor = System.Drawing.Color.White;
            this.mrGroup1.ShadowControl = true;
            this.mrGroup1.ShadowThickness = 3;
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.Name = "label21";
            // 
            // LblItemsDiscountSum
            // 
            resources.ApplyResources(this.LblItemsDiscountSum, "LblItemsDiscountSum");
            this.LblItemsDiscountSum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblItemsDiscountSum.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblItemsDiscountSum.Name = "LblItemsDiscountSum";
            // 
            // label32
            // 
            resources.ApplyResources(this.label32, "label32");
            this.label32.Name = "label32";
            // 
            // LblItemsTotal
            // 
            resources.ApplyResources(this.LblItemsTotal, "LblItemsTotal");
            this.LblItemsTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblItemsTotal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblItemsTotal.Name = "LblItemsTotal";
            // 
            // label31
            // 
            resources.ApplyResources(this.label31, "label31");
            this.label31.Name = "label31";
            // 
            // LblItemsTotalQty
            // 
            resources.ApplyResources(this.LblItemsTotalQty, "LblItemsTotalQty");
            this.LblItemsTotalQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblItemsTotalQty.Name = "LblItemsTotalQty";
            // 
            // LblItemsNetAmount
            // 
            resources.ApplyResources(this.LblItemsNetAmount, "LblItemsNetAmount");
            this.LblItemsNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblItemsNetAmount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblItemsNetAmount.Name = "LblItemsNetAmount";
            // 
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.Name = "label29";
            // 
            // PanelFloorList
            // 
            this.PanelFloorList.BackColor = System.Drawing.SystemColors.InactiveCaption;
            resources.ApplyResources(this.PanelFloorList, "PanelFloorList");
            this.PanelFloorList.Name = "PanelFloorList";
            // 
            // BtnAvailable
            // 
            this.BtnAvailable.BackColor = System.Drawing.SystemColors.ControlLight;
            resources.ApplyResources(this.BtnAvailable, "BtnAvailable");
            this.BtnAvailable.Name = "BtnAvailable";
            this.BtnAvailable.Tag = "AVAILABLE";
            this.toolTip1.SetToolTip(this.BtnAvailable, resources.GetString("BtnAvailable.ToolTip"));
            this.BtnAvailable.UseVisualStyleBackColor = false;
            this.BtnAvailable.Click += new System.EventHandler(this.BtnAvailable_Click);
            // 
            // BtnAll
            // 
            this.BtnAll.BackColor = System.Drawing.Color.SteelBlue;
            resources.ApplyResources(this.BtnAll, "BtnAll");
            this.BtnAll.Name = "BtnAll";
            this.BtnAll.Tag = "REFRESH";
            this.toolTip1.SetToolTip(this.BtnAll, resources.GetString("BtnAll.ToolTip"));
            this.BtnAll.UseVisualStyleBackColor = false;
            this.BtnAll.Click += new System.EventHandler(this.BtnAll_Click);
            // 
            // BtnOccupied
            // 
            this.BtnOccupied.BackColor = System.Drawing.Color.DarkOrchid;
            resources.ApplyResources(this.BtnOccupied, "BtnOccupied");
            this.BtnOccupied.Name = "BtnOccupied";
            this.BtnOccupied.Tag = "OCCUPIED";
            this.toolTip1.SetToolTip(this.BtnOccupied, resources.GetString("BtnOccupied.ToolTip"));
            this.BtnOccupied.UseVisualStyleBackColor = false;
            this.BtnOccupied.Click += new System.EventHandler(this.BtnOccupied_Click);
            // 
            // BtnBooked
            // 
            this.BtnBooked.BackColor = System.Drawing.Color.Tomato;
            resources.ApplyResources(this.BtnBooked, "BtnBooked");
            this.BtnBooked.Name = "BtnBooked";
            this.BtnBooked.Tag = "BOOKED";
            this.toolTip1.SetToolTip(this.BtnBooked, resources.GetString("BtnBooked.ToolTip"));
            this.BtnBooked.UseVisualStyleBackColor = false;
            this.BtnBooked.Click += new System.EventHandler(this.BtnBooked_Click);
            // 
            // BtnCombine
            // 
            resources.ApplyResources(this.BtnCombine, "BtnCombine");
            this.BtnCombine.Name = "BtnCombine";
            this.BtnCombine.Tag = "COMBINE";
            this.toolTip1.SetToolTip(this.BtnCombine, resources.GetString("BtnCombine.ToolTip"));
            this.BtnCombine.UseVisualStyleBackColor = true;
            this.BtnCombine.Click += new System.EventHandler(this.BtnCombine_Click);
            // 
            // BtnDisiable
            // 
            this.BtnDisiable.BackColor = System.Drawing.SystemColors.Info;
            resources.ApplyResources(this.BtnDisiable, "BtnDisiable");
            this.BtnDisiable.Name = "BtnDisiable";
            this.BtnDisiable.Tag = "DISABLE";
            this.toolTip1.SetToolTip(this.BtnDisiable, resources.GetString("BtnDisiable.ToolTip"));
            this.BtnDisiable.UseVisualStyleBackColor = false;
            this.BtnDisiable.Click += new System.EventHandler(this.BtnDisable_Click);
            // 
            // BtnPrint
            // 
            resources.ApplyResources(this.BtnPrint, "BtnPrint");
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Tag = "PRINT";
            this.toolTip1.SetToolTip(this.BtnPrint, resources.GetString("BtnPrint.ToolTip"));
            this.BtnPrint.UseVisualStyleBackColor = true;
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSlateGray;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.ChkTableReport);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnAll);
            this.groupBox1.Controls.Add(this.BtnAvailable);
            this.groupBox1.Controls.Add(this.BtnTableSplit);
            this.groupBox1.Controls.Add(this.BtnOccupied);
            this.groupBox1.Controls.Add(this.BtnTableTransfer);
            this.groupBox1.Controls.Add(this.BtnBooked);
            this.groupBox1.Controls.Add(this.BtnPrint);
            this.groupBox1.Controls.Add(this.BtnDisiable);
            this.groupBox1.Controls.Add(this.BtnCombine);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // BtnTableSplit
            // 
            resources.ApplyResources(this.BtnTableSplit, "BtnTableSplit");
            this.BtnTableSplit.Name = "BtnTableSplit";
            this.BtnTableSplit.Tag = "SPLIT";
            this.toolTip1.SetToolTip(this.BtnTableSplit, resources.GetString("BtnTableSplit.ToolTip"));
            this.BtnTableSplit.UseVisualStyleBackColor = true;
            this.BtnTableSplit.Click += new System.EventHandler(this.BtnTableSplit_Click);
            // 
            // BtnTableTransfer
            // 
            resources.ApplyResources(this.BtnTableTransfer, "BtnTableTransfer");
            this.BtnTableTransfer.Name = "BtnTableTransfer";
            this.BtnTableTransfer.Tag = "TRANSFER";
            this.toolTip1.SetToolTip(this.BtnTableTransfer, resources.GetString("BtnTableTransfer.ToolTip"));
            this.BtnTableTransfer.UseVisualStyleBackColor = true;
            this.BtnTableTransfer.Click += new System.EventHandler(this.BtnTableTransfer_Click);
            // 
            // ChkTableReport
            // 
            resources.ApplyResources(this.ChkTableReport, "ChkTableReport");
            this.ChkTableReport.Name = "ChkTableReport";
            this.ChkTableReport.UseVisualStyleBackColor = true;
            this.ChkTableReport.CheckedChanged += new System.EventHandler(this.ChkTableReport_CheckedChanged);
            // 
            // FrmTablesList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.PanelFloorList);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "FrmTablesList";
            this.ShowIcon = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmTablesList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmTablesList_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).EndInit();
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button BtnAvailable;
        private System.Windows.Forms.Button BtnAll;
        private System.Windows.Forms.Button BtnOccupied;
        private System.Windows.Forms.Button BtnBooked;
        private System.Windows.Forms.Button BtnCombine;
        private System.Windows.Forms.Button BtnDisiable;
        private System.Windows.Forms.Button BtnPrint;
        private FlowLayoutPanel panelTablelist;
        private MrPanel PanelFloorList;
        private MrPanel panel1;
        private System.Windows.Forms.Button BtnTableTransfer;
        private System.Windows.Forms.Button BtnTableSplit;
        private MrDataGridView RGrid;
        private SplitContainer splitContainer1;
        private CheckBox ChkTableReport;
        private MrGroup mrGroup1;
        private Label label21;
        private Label LblItemsDiscountSum;
        private Label label32;
        private Label LblItemsTotal;
        private Label label31;
        private Label LblItemsTotalQty;
        private Label LblItemsNetAmount;
        private Label label29;
        private DataGridViewTextBoxColumn SNO;
        private DataGridViewTextBoxColumn DESCRIPTION;
        private DataGridViewTextBoxColumn QTY;
        private DataGridViewTextBoxColumn RATE;
        private DataGridViewTextBoxColumn AMOUNT;
        private DataGridViewTextBoxColumn DISCOUNT;
        private DataGridViewTextBoxColumn NETAMOUNT;
        private GroupBox groupBox1;
        private ToolTip toolTip1;
    }
}