using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using MrBLL.DataEntry.Common;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MrBLL.Domains.Restro.Entry;

public partial class FrmTablesList : MrForm
{
    public FrmTablesList()
    {
        InitializeComponent();
        splitContainer1.Panel2.Visible = false;
        splitContainer1.Panel2Collapsed = true;
        GenerateDynamicFloor();
        GenerateDynamicTable("All", 0);
        ChkTableReport_CheckedChanged(this, EventArgs.Empty);
    }

    private void FrmTablesList_Load(object sender, EventArgs e)
    {
        BtnAll.Focus();
    }

    private void FrmTablesList_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
            {
                Close();
            }
        }
        else if (e.KeyCode == Keys.F5)
        {
            BtnAll.PerformClick();
        }
    }

    private void BtnAll_Click(object sender, EventArgs e)
    {
        GenerateDynamicTable("All", 0);
    }

    private void ChkTableReport_CheckedChanged(object sender, EventArgs e)
    {
        splitContainer1.Panel2Collapsed = !ChkTableReport.Checked;
    }

    private void BtnAvailable_Click(object sender, EventArgs e)
    {
        GenerateDynamicTable("A", 0);
    }

    private void BtnOccupied_Click(object sender, EventArgs e)
    {
        GenerateDynamicTable("O", 0);
    }

    private void BtnBooked_Click(object sender, EventArgs e)
    {
        GenerateDynamicTable("B", 0);
    }

    private void BtnCombine_Click(object sender, EventArgs e)
    {
        var floorId = string.Empty;
        GenerateDynamicTable("C", 0);
    }

    private void BtnDisable_Click(object sender, EventArgs e)
    {
        var floorId = string.Empty;
        GenerateDynamicTable("N", 0);
    }

    private void BtnPrint_Click(object sender, EventArgs e)
    {
        var dtDesign = _setup.GetPrintVoucherList("SB");
        if (dtDesign.Rows.Count <= 0)
        {
            CustomMessageBox.Warning("PRINT SETTING IS REQUIRED TO PRINT INVOICE..!!");
            return;
        }
        var type = dtDesign.Rows[0]["DesignerPaper_Name"].ToString();
        var frmDp = new FrmDocumentPrint(type, "SB", string.Empty, string.Empty, true)
        {
            Owner = ActiveForm
        };
        frmDp.ShowDialog();
    }

    private void BtnTableTransfer_Click(object sender, EventArgs e)
    {
        var result = new FrmTableTransfer();
        result.ShowDialog();
        if (result.DialogResult is DialogResult.OK)
        {
            BtnAll.PerformClick();
        }
    }

    private void BtnTableSplit_Click(object sender, EventArgs e)
    {
        var result = new FrmSplitTable();
        result.ShowDialog();
        if (result.DialogResult is DialogResult.OK)
        {
            BtnAll.PerformClick();
        }
    }

    private void ButtonTable_Click(object sender, EventArgs e)
    {
        Table = ((SimpleButton)sender).Text;
        TableId = ((SimpleButton)sender).Name.GetInt();
        var tableType = ((SimpleButton)sender).Tag.GetString();
        if (TableId <= 0)
        {
            return;
        }
        var result = new FrmRSalesInvoice(Table, TableId, tableType);
        result.ShowDialog();
        if (result._dialogResult == DialogResult.OK)
        {
            BtnAll.PerformClick();
        }
    }

    private void ButtonFloor_Click(object sender, EventArgs e)
    {
        _floor = ((SimpleButton)sender).Text;
        _floorId = ((SimpleButton)sender).Name.GetInt();
        var timer = new Timer
        {
            Interval = (1 * 1000)
        };
        timer.Tick += Timer_Tick;
        timer.Start();
        GenerateDynamicTable("All", _floorId);
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        var queryData = "Select CHECKSUM_AGG(BINARY_CHECKSUM(TableStatus)) from AMS.TableMaster".GetQueryData();
        if (_tableStatus == string.Empty) return;
        if (queryData != _tableStatus)
        {
            GenerateDynamicTable("A", 0);
        }
    }

    private void ButtonTable_Leave(object sender, EventArgs e)
    {
        if (RGrid.RowCount > 0)
        {
        }
    }

    private void ButtonTable_Enter(object sender, EventArgs e)
    {
        if (ChkTableReport.Checked)
        {
            if (sender is SimpleButton button)
            {
                if (button.Name.GetInt() > 0)
                {
                    var result = _entry.GetTableOrderDetails(button.Name.GetInt());
                    RGrid.DataSource = result;
                    TotalCalculationOfInvoice();
                }
            }
        }
    }

    //private void toolTip1_Popup(object sender, PopupEventArgs e)
    //{
    //    foreach (Control ctrl in this.Controls)
    //    {
    //        if (ctrl is SimpleButton && ctrl.Tag is string)
    //        {
    //            ctrl.MouseHover += new EventHandler(delegate (Object o, EventArgs a)
    //            {
    //                var btn = (Control)o;
    //                toolTip1.SetToolTip(btn, btn.Tag.ToString());
    //            });
    //        }
    //    }
    //}

    // METHOD FOR THIS FORM
    public void DynamicFloorButton()
    {
        try
        {
            var dtFloor = ClsMasterSetup.GenerateRestaurantFloor();
            int x = 10, y = 20;
            var button = new SimpleButton();
            PanelFloorList.Controls.Remove(button);
            for (var i = 0; i < dtFloor.Rows.Count; i++)
            {
                var ro = dtFloor.Rows[i];
                var buttonTable = new SimpleButton
                {
                    Text = ro["Description"].ToString(),
                    Name = ro["FloorId"].ToString(),
                    Tag = ro["ShortName"].ToString(),
                    CausesValidation = false,
                    Size = new Size(120, 45)
                };
                buttonTable.Click += ButtonFloor_Click;
                buttonTable.LookAndFeel.UseDefaultLookAndFeel = false;
                buttonTable.Appearance.Options.UseBackColor = true;
                buttonTable.ForeColor = SystemColors.InactiveCaptionText;
                buttonTable.BackColor = SystemColors.Info;
                buttonTable.Font = new Font("Bookman Old Style", 12, FontStyle.Bold);
                buttonTable.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;

                var w = (i * 130) + 40;
                if (PanelFloorList.Width >= w)
                {
                    x = (i * 130) + 10;
                    y = 1 + 10;
                    buttonTable.Location = new Point(x, y);

                    if (w + 120 >= PanelFloorList.Width)
                    {
                        x = 0;
                    }
                }
                else if (PanelFloorList.Width <= w && x != 0)
                {
                    y += 0;
                    if (x + 120 >= panelTablelist.Width)
                    {
                        x = 120;
                        buttonTable.Location = new Point(x, y);
                    }
                    else
                    {
                        buttonTable.Location = new Point(x, y);
                        x += 120;
                    }
                }
                else if (PanelFloorList.Width <= w && x == 0)
                {
                    y += 120;
                    buttonTable.Location = new Point(x, y);
                    x += 120;
                }

                PanelFloorList.Controls.Add(buttonTable);
            }
        }
        catch (Exception ex)
        {
            ex.DialogResult();
        }
    }

    private void GenerateDynamicFloor()
    {
        DynamicFloorButton();
    }

    public void DynamicTableButton(string tableStatus, int floorId)
    {
        try
        {
            var table = ClsMasterSetup.GenerateRestaurantTable(tableStatus, floorId);
            panelTablelist.Controls.Clear();
            for (var i = 0; i < table.Rows.Count; i++)
            {
                var ro = table.Rows[i];
                var buttonTable = new SimpleButton
                {
                    Size = new Size(110, 70),
                    TabIndex = 0,
                    TabStop = false,
                    CausesValidation = false,
                    Font = new Font("Bookman Old Style", 8),
                    ForeColor = Color.White,
                    Text = ro["TableName"].ToString(),
                    Name = ro["TableId"].ToString(),
                    Tag = ro["TableType"].ToString(),
                    ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat
                };
                buttonTable.LookAndFeel.Style = LookAndFeelStyle.Office2003;
                buttonTable.LookAndFeel.UseDefaultLookAndFeel = false;
                buttonTable.Appearance.TextOptions.WordWrap = WordWrap.Wrap;
                buttonTable.Size = new Size(110, 90);
                buttonTable.Padding = new Padding(30, 3, 20, 3);
                buttonTable.Click += ButtonTable_Click;
                buttonTable.MouseEnter += ButtonTable_Enter;
                buttonTable.MouseLeave += ButtonTable_Leave;
                tableStatus = ro["TableStatus"].ToString();
                var tableType = ro["TableType"].ToString();
                buttonTable.Font = tableStatus switch
                {
                    "A" => new Font("Bookman Old Style", 10, FontStyle.Regular),
                    _ => new Font("Bookman Old Style", 10, FontStyle.Regular)
                };

                buttonTable.Appearance.BackColor = tableStatus switch
                {
                    "A" when tableType.Equals("D") => Color.White,
                    "O" when tableType.Equals("D") => Color.DarkOrchid,
                    "B" when tableType.Equals("D") => Color.Tomato,
                    "C" when tableType.Equals("D") => Color.LightSlateGray,
                    "A" when tableType.Equals("N") => Color.Brown,
                    "A" when tableType.Equals("S") => Color.AliceBlue,
                    "A" when tableType.Equals("T") => Color.Orange,
                    "O" when tableType.Equals("T") => Color.DarkOrchid,
                    "O" when tableType.Equals("S") => Color.DarkOrchid,
                    "A" when tableType.Equals("T") => Color.Orange,
                    _ => buttonTable.Appearance.BackColor
                };
                buttonTable.BackColor = tableStatus switch
                {
                    "A" when tableType.Equals("D") => Color.White,
                    "O" when tableType.Equals("D") => Color.DarkOrchid,
                    "B" when tableType.Equals("D") => Color.Tomato,
                    "C" when tableType.Equals("D") => Color.LightSlateGray,
                    "A" when tableType.Equals("N") => Color.Brown,
                    "A" when tableType.Equals("S") => Color.AliceBlue,
                    "A" when tableType.Equals("T") => Color.Orange,
                    "O" when tableType.Equals("T") => Color.DarkOrchid,
                    "O" when tableType.Equals("S") => Color.DarkOrchid,
                    _ => buttonTable.Appearance.BackColor
                };
                buttonTable.ForeColor = tableStatus switch
                {
                    "A" when tableType.Equals("D") => Color.Black,
                    "O" when tableType.Equals("D") => Color.White,
                    "B" when tableType.Equals("D") => Color.White,
                    "C" when tableType.Equals("D") => Color.White,
                    "A" when tableType.Equals("N") => Color.White,
                    "A" when tableType.Equals("S") => Color.Black,
                    "O" when tableType.Equals("T") => Color.White,
                    "O" when tableType.Equals("S") => Color.White,
                    "A" when tableType.Equals("T") => Color.White,
                    _ => buttonTable.Appearance.BackColor
                };
                panelTablelist.Controls.Add(buttonTable);
            }
        }
        catch (Exception ex)
        {
            ex.DialogResult();
        }
    }

    private void GenerateDynamicTable(string tableStatus, int floorId)
    {
        DynamicTableButton(tableStatus, floorId);
    }

    private void TotalCalculationOfInvoice()
    {
        var sumColType = RGrid.Rows.OfType<DataGridViewRow>();
        var gridViewRows = sumColType as DataGridViewRow[] ?? sumColType.ToArray();

        LblItemsTotalQty.Text = gridViewRows.Sum(row => row.Cells["QTY"].Value.GetDecimal()).GetDecimalString();
        LblItemsTotal.Text = gridViewRows.Sum(row => row.Cells["AMOUNT"].Value.GetDecimal()).GetDecimalString();
        LblItemsDiscountSum.Text = gridViewRows.Sum(row => row.Cells["DISCOUNT"].Value.GetDecimal()).GetDecimalString();
        LblItemsNetAmount.Text = gridViewRows.Sum(row => row.Cells["NETAMOUNT"].Value.GetDecimal()).GetDecimalString();
    }


    // OBJECT FOR THIS FORM
    private string _tableStatus = string.Empty;
    private string _floor = string.Empty;
    private int _floorId = 0;
    private string _comTables = string.Empty;
    private string _comTableCode = string.Empty;
    public string Table = string.Empty;
    public int TableId = 0;
    private string _query = string.Empty;
    private DataTable dt = new DataTable();
    private readonly IMasterSetup _setup = new ClsMasterSetup();
    private ISalesEntry _entry = new ClsSalesEntry();


}