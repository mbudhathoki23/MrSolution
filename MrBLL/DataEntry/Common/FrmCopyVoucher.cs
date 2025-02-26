using MrBLL.Utility.Common;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Utility.PickList;
using MrDAL.Utility.Server;
using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace MrBLL.DataEntry.Common;

public partial class FrmCopyVoucher : MrForm
{
    private readonly ArrayList ColumnWidths = new();
    private readonly ArrayList HeaderCap = new();
    private readonly ClsCopyVoucher Obj = new();
    private DataTable dt = new();
#pragma warning disable CS0414 // The field 'FrmCopyVoucher.ListCaption' is assigned but its value is never used
    private string ListCaption;
#pragma warning restore CS0414 // The field 'FrmCopyVoucher.ListCaption' is assigned but its value is never used
    private string Query;

    public FrmCopyVoucher(string FrmName, string Module, string DocNo)
    {
        InitializeComponent();
        Obj.FrmName = FrmName;
        Obj.Module = Module;
        Obj.DocNo = DocNo;
    }

    public string VoucherNo { get; private set; }

    private void FrmCopyVoucher_Load(object sender, EventArgs e)
    {
        Top = 50;
        Left = 240;
        BackColor = ObjGlobal.FrmBackColor();
        Text = Obj.FrmName;
    }

    private void FrmCopyVoucher_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) SendKeys.Send("{TAB}");
        if (e.KeyChar == (char)Keys.Escape)
            if (MessageBox.Show("Are you sure want to Close Form!", ObjGlobal.Caption, MessageBoxButtons.YesNo) ==
                DialogResult.Yes)
                Close();
    }

    private void txt_VoucherNo_Enter(object sender, EventArgs e)
    {
    }

    private void txt_VoucherNo_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.Tab) btn_VoucherNo_Click(sender, e);
    }

    private void txt_VoucherNo_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void txt_VoucherNo_Leave(object sender, EventArgs e)
    {
    }

    private void txt_VoucherNo_Validated(object sender, EventArgs e)
    {
        if (txt_VoucherNo.Text.Trim() != string.Empty)
        {
            if (Obj.Module == "JV" || Obj.Module == "PV" || Obj.Module == "RV" || Obj.Module == "CV")
            {
                Query = "SELECT Voucher_No  FROM AMS.Voucher_Master Where Station='" + Obj.Module + "' ";
                dt.Reset();
                dt = GetConnection.SelectDataTableQuery(Query);
                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("Voucher No doesn't exit!");
                    txt_VoucherNo.Focus();
                }
            }
            else if (Obj.Module == "PO")
            {
                Query = "SELECT Order_No FROM AMS.PurchaseOrder_Master ";
                dt.Reset();
                dt = GetConnection.SelectDataTableQuery(Query);
                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("Order No doesn't exit!");
                    txt_VoucherNo.Focus();
                }
            }
            else if (Obj.Module == "PC")
            {
                Query = "SELECT Challan_No FROM AMS.PurchaseChallan_Master ";
                dt.Reset();
                dt = GetConnection.SelectDataTableQuery(Query);
                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("Challan No doesn't exit!");
                    txt_VoucherNo.Focus();
                }
            }
            else if (Obj.Module == "PB")
            {
                Query = "SELECT Invoice_No FROM AMS.PurchaseInvoice_Master ";
                dt.Reset();
                dt = GetConnection.SelectDataTableQuery(Query);
                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("Invoice No doesn't exit!");
                    txt_VoucherNo.Focus();
                }
            }
            else if (Obj.Module == "PR")
            {
                Query = "SELECT ReturnInvoice_No FROM AMS.PurchaseReturn_Master ";
                dt.Reset();
                dt = GetConnection.SelectDataTableQuery(Query);
                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("Return Invoice No doesn't exit!");
                    txt_VoucherNo.Focus();
                }
            }
        }
        else
        {
            MessageBox.Show("Voucher No is not blank!");
            txt_VoucherNo.Focus();
        }
    }

    private void btn_VoucherNo_Click(object sender, EventArgs e)
    {
        ClsPickList.PlValue1 = string.Empty;
        HeaderCap.Clear();
        ColumnWidths.Clear();

        if (Obj.Module == "JV" || Obj.Module == "PV" || Obj.Module == "RV" || Obj.Module == "CV")
        {
            ListCaption = "Voucher List";
            HeaderCap.Add("To Number");
            ColumnWidths.Add(150);
            HeaderCap.Add("Voucher Date");
            ColumnWidths.Add(150);
            if (ObjGlobal.SysDateType == "D")
                Query =
                    "SELECT Voucher_No as [To Number],Voucher_Date [Voucher Date] FROM AMS.Voucher_Master Where Module='" +
                    Obj.Module + "' ";
            else
                Query =
                    "SELECT Voucher_No as [To Number],Voucher_BsDate [Voucher Date] FROM AMS.Voucher_Master Where Module='" +
                    Obj.Module + "' ";
        }
        else if (Obj.Module == "PO")
        {
            ListCaption = "Purchase Order List";
            HeaderCap.Add("To Number");
            ColumnWidths.Add(150);
            HeaderCap.Add("Order Date");
            ColumnWidths.Add(150);
            Query = ObjGlobal.SysDateType == "D"
                ? "SELECT Order_No [From Number],Order_Date [Order Date],L.Ledger_Name [Party Name],Net_Amount [Net Amount] FROM AMS.PurchaseOrder_Main as POM Inner Join AMS.Ledger as L on L.Ledger_Code=POM.Ledger_Code"
                : "SELECT Order_No [From Number],Order_BsDate [Order Date],L.Ledger_Name [Party Name],Net_Amount [Net Amount] FROM AMS.PurchaseOrder_Main as POM Inner Join AMS.Ledger as L on L.Ledger_Code=POM.Ledger_Code";
        }
        else if (Obj.Module == "PC")
        {
            ListCaption = "Purchase Challan List";
            HeaderCap.Add("To Number");
            ColumnWidths.Add(150);
            HeaderCap.Add("Challan Date");
            ColumnWidths.Add(150);
            Query = ObjGlobal.SysDateType == "D"
                ? "SELECT Challan_No [From Number],Challan_Date [Challan Date],L.Ledger_Name [Party Name],Net_Amount [Net Amount] FROM AMS.PurchaseChallan_Main as PCM Inner Join AMS.Ledger as L on L.Ledger_Code=PCM.Ledger_Code"
                : "SELECT Challan_No [From Number],Challan_BsDate [Challan Date],L.Ledger_Name [Party Name],Net_Amount [Net Amount] FROM AMS.PurchaseChallan_Main as PCM Inner Join AMS.Ledger as L on L.Ledger_Code=PCM.Ledger_Code";
        }
        else if (Obj.Module == "PB")
        {
            ListCaption = "Purchase Invoice List";
            HeaderCap.Add("To Number");
            ColumnWidths.Add(150);
            HeaderCap.Add("Invoice Date");
            ColumnWidths.Add(150);
            Query = ObjGlobal.SysDateType == "D"
                ? "SELECT Invoice_No [From Number],Invoice_Date [Invoice Date],L.Ledger_Name [Party Name],Net_Amount [Net Amount] FROM AMS.PurchaseInvoice_Main as PIM Inner Join AMS.Ledger as L on L.Ledger_Code=PIM.Ledger_Code"
                : "SELECT Invoice_No [From Number],Invoice_BsDate [Invoice Date],L.Ledger_Name [Party Name],Net_Amount [Net Amount] FROM AMS.PurchaseInvoice_Main as PIM Inner Join AMS.Ledger as L on L.Ledger_Code=PIM.Ledger_Code";
        }
        else if (Obj.Module == "PR")
        {
            ListCaption = "Purchase Return List";
            HeaderCap.Add("To Number");
            ColumnWidths.Add(150);
            HeaderCap.Add("Return Date");
            ColumnWidths.Add(150);
            Query = ObjGlobal.SysDateType == "D"
                ? "SELECT ReturnInvoice_No [From Number],ReturnInvoice_Date [Return Date],L.Ledger_Name [Party Name],Net_Amount [Net Amount] FROM AMS.PurchaseReturn_Main as PRM Inner Join AMS.Ledger as L on L.Ledger_Code=PRM.Ledger_Code"
                : "SELECT ReturnInvoice_No [From Number],ReturnInvoice_BsDate [Return Date],L.Ledger_Name [Party Name],Net_Amount [Net Amount] FROM AMS.PurchaseReturn_Main as PRM Inner Join AMS.Ledger as L on L.Ledger_Code=PRM.Ledger_Code";
        }
        else if (Obj.Module == "SO")
        {
            ListCaption = "Sales Order List";
            HeaderCap.Add("To Number");
            ColumnWidths.Add(150);
            HeaderCap.Add("Order Date");
            ColumnWidths.Add(150);
            Query = ObjGlobal.SysDateType == "D"
                ? "SELECT Order_No [From Number],Order_Date [Order Date],L.Ledger_Name [Party Name],Net_Amount [Net Amount] FROM AMS.SalesOrder_Main as SOM Inner Join AMS.Ledger as L on L.Ledger_Code=SOM.Ledger_Code"
                : "SELECT Order_No [From Number],Order_BsDate [Order Date],L.Ledger_Name [Party Name],Net_Amount [Net Amount] FROM AMS.SalesOrder_Main as SOM Inner Join AMS.Ledger as L on L.Ledger_Code=SOM.Ledger_Code";
        }
        else if (Obj.Module == "SC")
        {
            ListCaption = "Sales Challan List";
            HeaderCap.Add("To Number");
            ColumnWidths.Add(150);
            HeaderCap.Add("Challan Date");
            ColumnWidths.Add(150);
            Query = ObjGlobal.SysDateType == "D"
                ? "SELECT Challan_No [From Number],Challan_Date [Challan Date],L.Ledger_Name [Party Name],Net_Amount [Net Amount] FROM AMS.SalesChallan_Main as SCM Inner Join AMS.Ledger as L on L.Ledger_Code=SCM.Ledger_Code"
                : "SELECT Challan_No [From Number],Challan_BsDate [Challan Date],L.Ledger_Name [Party Name],Net_Amount [Net Amount] FROM AMS.SalesChallan_Main as SCM Inner Join AMS.Ledger as L on L.Ledger_Code=SCM.Ledger_Code";
        }
        else if (Obj.Module == "SB")
        {
            ListCaption = "Sales Invoice List";
            HeaderCap.Add("To Number");
            ColumnWidths.Add(150);
            HeaderCap.Add("Invoice Date");
            ColumnWidths.Add(150);
            Query = ObjGlobal.SysDateType == "D"
                ? "SELECT Invoice_No [From Number],Invoice_Date [Invoice Date],L.Ledger_Name [Party Name],Net_Amount [Net Amount] FROM AMS.SalesInvoice_Main as SIM Inner Join AMS.Ledger as L on L.Ledger_Code=SIM.Ledger_Code"
                : "SELECT Invoice_No [From Number],Invoice_BsDate [Invoice Date],L.Ledger_Name [Party Name],Net_Amount [Net Amount] FROM AMS.SalesInvoice_Main as SIM Inner Join AMS.Ledger as L on L.Ledger_Code=SIM.Ledger_Code";
        }
        else if (Obj.Module == "SR")
        {
            ListCaption = "Sales Return List";
            HeaderCap.Add("To Number");
            ColumnWidths.Add(150);
            HeaderCap.Add("Return Date");
            ColumnWidths.Add(150);
            Query = ObjGlobal.SysDateType == "D"
                ? "SELECT SalesReturn_Invoice_No [From Number],ReturnInvoice_Date [Invoice Date],L.Ledger_Name [Party Name],Net_Amount [Net Amount] FROM AMS.SalesReturn_Main as SRM Inner Join AMS.Ledger as L on L.Ledger_Code=SRM.Ledger_Code"
                : "SELECT SalesReturn_Invoice_No [From Number],ReturnInvoice_BsDate [Invoice Date],L.Ledger_Name [Party Name],Net_Amount [Net Amount] FROM AMS.SalesReturn_Main as SRM Inner Join AMS.Ledger as L on L.Ledger_Code=SRM.Ledger_Code";
        }

        var PkLst = new FrmPickList(Query, HeaderCap, ColumnWidths, string.Empty, string.Empty);
        if (PkLst.ShowDialog() != DialogResult.OK) return;
        if (!string.IsNullOrEmpty(ClsPickList.PlValue1))
        {
            txt_VoucherNo.Text = ClsPickList.PlValue1;
            VoucherNo = ClsPickList.PlValue1;
            txt_VoucherNo.Focus();
        }
    }

    private void btn_Ok_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btn_Cancel_Click(object sender, EventArgs e)
    {
        Close();
    }
}