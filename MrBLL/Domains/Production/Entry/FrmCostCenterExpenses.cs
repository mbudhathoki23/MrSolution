using DevExpress.XtraEditors;
using MrBLL.DataEntry.Common;
using MrBLL.Master.LedgerSetup;
using MrBLL.Master.ProductSetup;
using MrBLL.Utility.Common;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Global.WinForm;
using MrDAL.Utility.Config;
using MrDAL.Utility.PickList;
using MrDAL.Utility.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Domains.Production.Entry;

public partial class FrmCostCenterExpenses : MrForm
{
    private readonly string _ActionTag = string.Empty;
    private ClsEntryForm GetFrom = null;

    public FrmCostCenterExpenses(bool Zoom, string TxtZoomVno)
    {
        InitializeComponent();
    }

    private void btnRemarks_Click(object sender, EventArgs e)
    {
        var frmPickList =
            new FrmAutoPopList("MIN", "NRMASTER", _ActionTag, ObjGlobal.SearchText, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
                txtRemarks.Text = frmPickList.SelectedList[0]["NRDESC"].ToString().Trim();
            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"CODULDN'T FIND ANY NARRATION OR REMARKS..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            txtRemarks.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        txtRemarks.Focus();
    }

    #region Global

    private DataTable dt = new();
    private string DocDesc = string.Empty;
    private readonly ArrayList HeaderCap = new();
    private readonly ArrayList ColumnWidths = new();
    private string Query = string.Empty;
    private long PID;
    private long GLID;
    private DataTable dtvalidate = new();
    private readonly int rowIndex = 0;
    private readonly int currentColumn = 0;
    private readonly string LedgerCode = string.Empty;
    private string CostCenterId = string.Empty;
    private readonly TextBox txtInvoiceNo = new();
    private long LedgerId;

    #endregion Global

    #region Frm

    private void FrmCostCenterExpenses_Load(object sender, EventArgs e)
    {
        BackColor = ObjGlobal.FrmBackColor();
        Tag = string.Empty;
        Clear();
        EnableDisable(false);
        ObjGlobal.DGridColorCombo(dgvPurchase);
        btnNew.Focus();
        ObjGlobal.GetFormAccessControl([btnNew, btnEdit, btnDelete], this.Tag);
    }

    private void FrmCostCenterExpenses_KeyPress(object sender, KeyPressEventArgs e)
    {
        try
        {
            if (e.KeyChar == 39) e.KeyChar = '0';
            if (e.KeyChar == 14) //Action New
            {
            }

            if (e.KeyChar == 21) //Action Update
            {
            }

            if (e.KeyChar == 4) //Action Delete
            {
            }

            if (e.KeyChar == 27) //Action unload page
            {
                if (btnNew.Enabled == false && btnEdit.Enabled == false && btnDelete.Enabled == false)
                {
                    var dialogResult = MessageBox.Show("Are you sure want to ClearControl Form..??", "ClearControl Form",
                        MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Clear();
                        EnableDisable(false);
                        ButtonEnableDisable(true);
                        btnSave.Text = "&Save";
                        Tag = string.Empty;
                        btnNew.Focus();
                    }
                }
                else
                {
                    var dialogResult = MessageBox.Show("Are you sure want to Close Form!", ObjGlobal.Caption,
                        MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes) Close();
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
        }
    }

    #endregion Frm

    #region Botton

    private void btnNew_Click(object sender, EventArgs e)
    {
        try
        {
            Text = "Cost Center Expenses [NEW]";
            Tag = "NEW";
            btnSave.Text = "&Save";
            EnableDisable(true);
            ButtonEnableDisable(false);
            Clear();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult == DialogResult.Yes) Close();
        }
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            Text = "Cost Center Expenses [UPDATE]";
            Tag = "UPDATE";
            btnSave.Text = "&Update";
            EnableDisable(true);
            ButtonEnableDisable(false);
            txtVno.Focus();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            Text = "Cost Center Expenses [DELETE]";
            Tag = "DELETE";
            btnSave.Text = "&Delete";
            EnableDisable(false);
            ButtonEnableDisable(false);
            txtVno.Enabled = true;
            btnVno.Enabled = true;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            txtVno.Focus();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }

    private void btnReverse_Click(object sender, EventArgs e)
    {
        try
        {
            Text = "Cost Center Expenses [REVERSE]";
            Tag = "REVERSE";
            btnSave.Text = "&Reverse";
            EnableDisable(false);
            ButtonEnableDisable(false);
            txtVno.Enabled = true;
            btnVno.Enabled = true;
            txtRemarks.Enabled = true;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            txtVno.Focus();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }

    private void btnPrint_Click(object sender, EventArgs e)
    {
        var FrmDP = new FrmDocumentPrint("Document Printing - Cost Center Expenses", "PCCE", string.Empty,
            txtVno.Text, txtVno.Text, string.Empty, string.Empty, ObjGlobal.SysDefaultInvoicePrinter,
            ObjGlobal.SysDefaultInvoiceDesign, string.Empty);
        FrmDP.Owner = this;
        FrmDP.Show();
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
        var dialogResult = MessageBox.Show("Are you sure want to Close Form..??", ObjGlobal.Caption,
            MessageBoxButtons.YesNo);
        if (DialogResult == DialogResult.Yes) Close();
    }

    #endregion Botton

    #region Event

    private void txtVno_Enter(object sender, EventArgs e)
    {
        txtVno.BackColor = Color.Thistle;
        txtVno.SelectAll();
    }

    private void txtVno_Leave(object sender, EventArgs e)
    {
        txtVno.BackColor = Color.Wheat;
    }

    private void txtVno_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (txtVno.Text.Trim() != string.Empty)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                MessageBox.Show("Costing Number Cannot Left  Blank..!!");
                txtVno.Focus();
            }
        }

        if (e.KeyCode == Keys.F1) btnVno_Click(sender, e);
    }

    private void txtVno_Validating(object sender, CancelEventArgs e)
    {
        if (txtVno.Text.Trim() != string.Empty)
        {
            if (Tag.ToString() == "NEW")
            {
                Query = "SELECT Costing_No FROM AMS.CostCenterExpenses_Master Where Costing_No='" + txtVno.Text +
                        "'";
                dt.Reset();
                dt = GetConnection.SelectDataTableQuery(Query);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show(
                        "Invoice Cannot Save Dublicate, Please Check The Invoice Number <This Invoice ALready Enter Plz Enter New One>!");
                    txtVno.Text = string.Empty;
                    txtVno.Focus();
                }
            }
            else if (Tag.ToString() == "UPDATE" || Tag.ToString() == "DELETE" || Tag.ToString() == "REVERSE")
            {
                Query = "SELECT Costing_No FROM AMS.CostCenterExpenses_Master Where Costing_No='" + txtVno.Text +
                        "'";
                dt.Reset();
                dt = GetConnection.SelectDataTableQuery(Query);
                if (dt.Rows.Count > 0)
                {
                    Clear();
                    FillInvoiceData(string.Empty, dt.Rows[0]["Costing_No"].ToString());
                    ObjGlobal.DGridColorCombo(dgvPurchase);
                    lbl_NoInWordsDetl.Text =
                        ClsMoneyConversion.MoneyConversion(Convert
                            .ToDecimal(ObjGlobal.ReturnDecimal(lblDetTotNetAmt.Text)).ToString());
                    txtVno.Enabled = false;
                    if (dpDate.Enabled)
                        dpDate.Focus();
                }
                else
                {
                    MessageBox.Show("Invoice Number Cannot be found, Please Check The Invoice Number!");
                    txtVno.Text = string.Empty;
                    txtVno.Focus();
                }
            }
        }
    }

    private void btnVno_Click(object sender, EventArgs e)
    {
        try
        {
            ClsPickList.PlValue1 = string.Empty;
            ClsPickList.PlValue2 = string.Empty;
            ClsPickList.PlValue3 = string.Empty;
            HeaderCap.Clear();
            ColumnWidths.Clear();
            HeaderCap.Add("Costing No");
            HeaderCap.Add("Invoice Date");
            HeaderCap.Add("Invoice Miti");
            HeaderCap.Add("Finished Good");
            HeaderCap.Add("Ref No");
            HeaderCap.Add("Ref Date");
            HeaderCap.Add("Net Amount");
            ColumnWidths.Add(130);
            ColumnWidths.Add(130);
            ColumnWidths.Add(120);
            ColumnWidths.Add(220);
            ColumnWidths.Add(100);
            ColumnWidths.Add(110);
            ColumnWidths.Add(120);

            Query = string.Empty;
            Query =
                "Select CCM.Costing_No,Invoice_Date,Invoice_Miti,P.PName,CCM.Order_No,Order_Date,Sum(CCD.Amount) Amount from AMS.CostCenterExpenses_Master as CCM Inner Join AMS.CostCenterExpenses_Details as CCD On CCD.Costing_No = CCM.Costing_No Inner Join AMS.Product as P On P.PID = CCM.FGProduct_Id ";
            Query = Query +
                    " Group By CCM.Costing_No,Invoice_Date,Invoice_Miti,CCM.Order_No,Order_Date,P.PName Order By Invoice_Date ";
            var PkLst = new FrmPickList(Query, HeaderCap, ColumnWidths, "Cost Center Expenses List", string.Empty);
            if (PkLst.ShowDialog() == DialogResult.OK)
                if (ClsPickList.PlValue1 != null && ClsPickList.PlValue3 != null &&
                    ClsPickList.PlValue1 != string.Empty && ClsPickList.PlValue3 != string.Empty)
                {
                    txtVno.Text = ClsPickList.PlValue1;
                    txtVno.Focus();
                }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
        }
    }

    private void txtSalesOrder_Enter(object sender, EventArgs e)
    {
        txtSalesOrder.BackColor = Color.Thistle;
        txtSalesOrder.SelectAll();
    }

    private void txtSalesOrder_Leave(object sender, EventArgs e)
    {
        txtSalesOrder.BackColor = Color.Wheat;
        FillOrderData();
    }

    private void txtSalesOrder_Validating(object sender, CancelEventArgs e)
    {
    }

    private void txtSalesOrder_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (txtSalesOrder.Text.Trim() != string.Empty)
                SendKeys.Send("{TAB}");
            else
                MessageBox.Show(@"Party Order Cannot Left Blank..!!", ObjGlobal.Caption);
        }

        if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.Tab) btnSalesOrder_Click(sender, e);
    }

    private void btnSalesOrder_Click(object sender, EventArgs e)
    {
        try
        {
            ClsPickList.PlValue1 = string.Empty;
            ClsPickList.PlValue2 = string.Empty;
            ClsPickList.PlValue3 = string.Empty;
            HeaderCap.Clear();
            ColumnWidths.Clear();
            HeaderCap.Add("Order No");
            HeaderCap.Add("Order Date");
            HeaderCap.Add("Customer Name");
            HeaderCap.Add("Net Amount");
            ColumnWidths.Add(120);
            ColumnWidths.Add(130);
            ColumnWidths.Add(250);
            ColumnWidths.Add(120);

            if (ObjGlobal.SysDateType == "D")
                Query = " Select SOM.SO_Invoice,SOM.Invoice_Date,";
            else
                Query = " Select SOM.SO_Invoice,SOM.Invoice_Miti,";
            Query = Query +
                    " GLName,Convert(Decimal(18,2),SOM.N_Amount) Amount From AMS.SO_Master SOM Inner Join AMS.SO_Details SOD On SOM.SO_Invoice=SOD.SO_Invoice Inner Join AMS.GeneralLedger as Gl On Gl.GLID=SOM.Customer_ID";
            Query = Query + " Where SOM.Invoice_Date<='" + Convert.ToDateTime(dpDate.Text).ToString("yyyy-MM-dd") +
                    "' and SOM.CBranch_Id=" + ObjGlobal.SysBranchId + " ";
            Query = Query + "and SOM.Action_Type Not In('Cancel')";
            //and SOM.SO_Invoice Not In (Select Distinct SO_Invoice From AMS.SOC_Master)";
            if (ObjGlobal.SysDateType == "D")
                Query = Query + " Group By SOM.SO_Invoice,SOM.Invoice_Date, GLName,SOM.N_Amount";
            else
                Query = Query + " Group By SOM.SO_Invoice,SOM.Invoice_Miti, GLName,SOM.N_Amount";
            //Query = Query + " Having SUM(Qty-Isnull(Issue_Qty,0))>0 ";
            Query = Query + " Order By SOM.SO_Invoice";
            var PkLst = new FrmPickList(Query, HeaderCap, ColumnWidths, "Sales Order List", string.Empty);
            if (PkLst.ShowDialog() == DialogResult.OK)
                if (ClsPickList.PlValue1 != null && ClsPickList.PlValue2 != null &&
                    ClsPickList.PlValue1 != string.Empty && ClsPickList.PlValue2 != string.Empty)
                {
                    txtSalesOrder.Text = ClsPickList.PlValue1;
                    txtSalesOrder.Focus();
                }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
        }
    }

    private void mskMiti_Enter(object sender, EventArgs e)
    {
        mskMiti.BackColor = Color.Thistle;
    }

    private void mskMiti_Leave(object sender, EventArgs e)
    {
        mskMiti.BackColor = Color.Wheat;
        if (mskMiti.Text != "  /  /")
            dpDate.Text =
                GetConnection.GetQueryData("Select AD_Date from AMS.DateMiti where BS_DateDMY='" + mskMiti.Text +
                                           "'");
    }

    private void mskMiti_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (mskMiti.Text.Trim() != string.Empty)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                MessageBox.Show("Voucher Miti Cannot Left  Blank..!!");
                mskMiti.Focus();
            }
        }
    }

    private void dpDate_Enter(object sender, EventArgs e)
    {
        dpDate.BackColor = Color.Thistle;
    }

    private void dpDate_Leave(object sender, EventArgs e)
    {
        dpDate.BackColor = Color.Wheat;
        if (dpDate.Text != "  /  /")
            mskMiti.Text = GetConnection.GetQueryData("Select BS_DateDMY from AMS.DateMiti where AD_Date='" +
                                                      Convert.ToDateTime(dpDate.Text).ToString("yyyy-MM-dd") + "'");
    }

    private void dpDate_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (dpDate.Text.Trim() != string.Empty)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                MessageBox.Show("Voucher Date Cannot Left  Blank..!!");
                dpDate.Focus();
            }
        }
    }

    private void txtProduct_Enter(object sender, EventArgs e)
    {
        txtProduct.BackColor = Color.Thistle;
        txtProduct.SelectAll();
    }

    private void txtProduct_Leave(object sender, EventArgs e)
    {
        txtProduct.BackColor = Color.Wheat;
    }

    private void txtProduct_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.F6 && e.Control != true && e.KeyCode != Keys.N && e.KeyCode != Keys.ShiftKey &&
            e.KeyCode != Keys.Escape && e.KeyCode != Keys.Back && e.KeyCode != Keys.Enter &&
            e.KeyCode != Keys.Alt && e.KeyCode != Keys.Space && e.KeyCode != Keys.Menu && e.KeyCode != Keys.LMenu &&
            e.KeyCode != Keys.RMenu && e.KeyCode != Keys.LWin &&
            e.KeyCode != Keys.RWin) btnProductList_Click(sender, e);
        if (e.Control && e.KeyCode == Keys.N)
            using (var frm = new FrmProduct(true))
            {
                frm.ShowDialog();
                txtProduct.Text = frm.ProductName;
                txtProduct.Focus();
            }

        if (e.KeyCode == Keys.Enter)
        {
            if (txtProduct.Text.Trim() == string.Empty)
                btnProductList_Click(sender, e);
            else
                txtLedger.Focus();
        }
    }

    private void txtProduct_Validating(object sender, CancelEventArgs e)
    {
        PID = 0;
        if (txtProduct.Text.Trim() == string.Empty && txtProduct.Enabled)
        {
            MessageBox.Show(@"Product cannot be left Blank..!!", ObjGlobal.Caption);
            txtProduct.Focus();
            return;
        }

        if (txtProduct.Text.Trim() != string.Empty)
        {
            Query = "Select PID,PName,PShortName From AMS.Product where PName='" + txtProduct.Text + "'";
            dtvalidate.Reset();
            dtvalidate = GetConnection.SelectDataTableQuery(Query);
            if (dtvalidate.Rows.Count > 0)
            {
                PID = Convert.ToInt64(dtvalidate.Rows[0]["PID"].ToString());
            }
            else
            {
                MessageBox.Show(@"Does Not Exits Product.!!", ObjGlobal.Caption);
                txtProduct.Focus();
            }
        }
        else
        {
            PID = 0;
        }
    }

    private void btnProductList_Click(object sender, EventArgs e)
    {
        ClsPickList.PlValue1 = string.Empty;
        ClsPickList.PlValue2 = string.Empty;
        ClsPickList.PlValue3 = string.Empty;
        HeaderCap.Clear();
        ColumnWidths.Clear();
        HeaderCap.Add("Name");
        HeaderCap.Add("Code");
        HeaderCap.Add("Stock Qty");
        HeaderCap.Add("Unit");
        HeaderCap.Add("Buy Rate");
        HeaderCap.Add("Sales Rate");
        HeaderCap.Add("Group");

        ColumnWidths.Add(200);
        ColumnWidths.Add(120);
        ColumnWidths.Add(120);
        ColumnWidths.Add(80);
        ColumnWidths.Add(110);
        ColumnWidths.Add(110);
        ColumnWidths.Add(120);
        Query =
            @"Select PName,PShortName,Convert(Decimal(18,2),Sum(BalanceQty) ) BalanceQty,UnitCode,Convert(Decimal(18,2),PBuyRate ) PBuyRate, Convert(Decimal(18,2),PSalesRate)PSalesRate,GrpName From (Select P.PName,P.PShortName,Case When EntryType='I' Then isnull(Sum(StockQty),0) Else  -isnull(Sum(StockQty),0) End BalanceQty,PU.UnitCode,P.PBuyRate,P.PSalesRate,PG.GrpName  from AMS.Product  as P
                        left outer join AMS.StockDetails as SD on P.PID=SD.Product_Id  left outer join AMS.ProductUnit as PU on  P.PUnit=PU.UID left outer join AMS.ProductGroup as PG on  PG.PGrpID=p.PGrpId where PCategory='Finished Goods'  Group By P.PName,P.PShortName,PU.UnitCode,P.PBuyRate,P.PSalesRate,PG.GrpName,EntryType ) as aa Group by PName,PShortName,UnitCode,PBuyRate,PSalesRate,GrpName
                        ";
        var PkLst = new FrmPickList(Query, HeaderCap, ColumnWidths, "Product List", string.Empty);
        if (PkLst.ShowDialog() == DialogResult.OK)
            if (ClsPickList.PlValue1 != null && ClsPickList.PlValue2 != null &&
                ClsPickList.PlValue1 != string.Empty && ClsPickList.PlValue2 != string.Empty)
            {
                txtProduct.Text = ClsPickList.PlValue1;
                txtProduct.Focus();
            }
    }

    private void txtLedger_Enter(object sender, EventArgs e)
    {
        txtLedger.BackColor = Color.Thistle;
        txtLedger.SelectAll();
    }

    private void txtLedger_Leave(object sender, EventArgs e)
    {
        txtLedger.BackColor = Color.Wheat;
    }

    private void txtLedger_Validating(object sender, CancelEventArgs e)
    {
        try
        {
            GLID = 0;
            if (txtLedger.Text.Trim() != string.Empty)
            {
                Query = "Select GLID,GLName,GLCode from AMS.GeneralLedger Where GLName='" + txtLedger.Text + "'";
                dtvalidate.Reset();
                dtvalidate = GetConnection.SelectDataTableQuery(Query);
                if (dtvalidate.Rows.Count > 0)
                {
                    GLID = Convert.ToInt64(dtvalidate.Rows[0]["GLID"].ToString());
                }
                else
                {
                    MessageBox.Show(@"Does Not Exits Ledger.!!", ObjGlobal.Caption);
                    txtLedger.Focus();
                }
            }
            else
            {
                GLID = 0;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
        }
    }

    private void txtLedger_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void btnLedgerList_Click(object sender, EventArgs e)
    {
        ClsPickList.PlValue1 = string.Empty;
        ClsPickList.PlValue2 = string.Empty;
        ClsPickList.PlValue3 = string.Empty;
        HeaderCap.Clear();
        ColumnWidths.Clear();
        HeaderCap.Add("Id");
        HeaderCap.Add("Description");
        HeaderCap.Add("ShortName");
        HeaderCap.Add("category");
        HeaderCap.Add("Group");
        ColumnWidths.Add(0);
        ColumnWidths.Add(275);
        ColumnWidths.Add(120);
        ColumnWidths.Add(120);
        ColumnWidths.Add(250);
        Query =
            "Select GLID, GLName, GLCode, GLType, GrpName from Ams.GeneralLedger as Gl Inner Join AMS.AccountGroup as AG On Gl.GrpId = Ag.GrpId Where GLType='Other' and (Gl.Status = 1 or Gl.Status is Null) and GrpType ='Expenses' order By GLName";
        using (var PkLst = new FrmPickList(Query, HeaderCap, ColumnWidths, "Ledger List", string.Empty))
        {
            if (PkLst.ShowDialog() == DialogResult.OK)
                if (ClsPickList.PlValue1 != null && ClsPickList.PlValue2 != null &&
                    ClsPickList.PlValue1 != string.Empty && ClsPickList.PlValue2 != string.Empty)
                {
                    txtLedger.Text = ClsPickList.PlValue2;
                    txtLedger.Focus();
                }
        }
    }

    private void txtCostCenter_Enter(object sender, EventArgs e)
    {
        txtCostCenter.BackColor = Color.Thistle;
    }

    private void txtCostCenter_Leave(object sender, EventArgs e)
    {
        txtCostCenter.BackColor = Color.Wheat;
    }

    private void txtCostCenter_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void txtCostCenter_Validating(object sender, CancelEventArgs e)
    {
        try
        {
            CostCenterId = string.Empty;
            if (txtCostCenter.Text.Trim() != string.Empty)
            {
                Query = "Select CCID,CCName,CCCode from AMS.CostCenter Where CCName='" + txtCostCenter.Text + "'";
                dtvalidate.Reset();
                dtvalidate = GetConnection.SelectDataTableQuery(Query);
                if (dtvalidate.Rows.Count > 0)
                {
                    CostCenterId = dtvalidate.Rows[0]["CCID"].ToString();
                }
                else
                {
                    MessageBox.Show(@"Does Not Exits Costcenter.!!", ObjGlobal.Caption);
                    txtCostCenter.Focus();
                }
            }
            else
            {
                CostCenterId = string.Empty;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
        }
    }

    private void btnCostCenterList_Click(object sender, EventArgs e)
    {
        ClsPickList.PlValue1 = string.Empty;
        ClsPickList.PlValue2 = string.Empty;
        ClsPickList.PlValue3 = string.Empty;
        HeaderCap.Clear();
        ColumnWidths.Clear();
        HeaderCap.Add("Id");
        HeaderCap.Add("Description");
        HeaderCap.Add("Short Name");
        ColumnWidths.Add(0);
        ColumnWidths.Add(275);
        ColumnWidths.Add(120);
        Query = "SELECT CCId,CCName,CCcode From AMS.CostCenter Order By CCName";
        using var PkLst = new FrmPickList(Query, HeaderCap, ColumnWidths, "CostCenter List", string.Empty);
        if (PkLst.ShowDialog() == DialogResult.OK)
            if (ClsPickList.PlValue1 != null && ClsPickList.PlValue2 != null &&
                ClsPickList.PlValue1 != string.Empty && ClsPickList.PlValue2 != string.Empty)
            {
                txtCostCenter.Text = ClsPickList.PlValue2;
                txtCostCenter.Focus();
            }
    }

    private void txtRate_Enter(object sender, EventArgs e)
    {
        txtRate.BackColor = Color.Thistle;
    }

    private void txtRate_Leave(object sender, EventArgs e)
    {
        txtRate.BackColor = Color.Wheat;
    }

    private void txtRate_Validating(object sender, CancelEventArgs e)
    {
        txtRate.Text = Convert.ToDouble(ObjGlobal.ReturnDecimal(txtRate.Text)).ToString(ObjGlobal.SysAmountFormat);
    }

    private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        try
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Tab)
                if (txtLedger.Text.Trim() != string.Empty && txtRate.Text.Trim() != string.Empty)
                {
                    if (dgvPurchase.Rows.Count >= 1)
                    {
                        if (dgvPurchase.Rows[rowIndex].Selected)
                            AddDataToGridDetails(Convert.ToInt16(dgvPurchase.SelectedRows[0].Cells[0].Value));
                        else
                            AddDataToGridDetails(0);
                    }
                    else
                    {
                        AddDataToGridDetails(0);
                    }

                    txtLedger.Focus();
                }
        }
        catch (InvalidOperationException ex)
        {
            MessageBox.Show(ex.Message, "Error Assigning", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !(sender as TextBox).Text.Contains(".")) return;
        decimal isNumber = 0;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out isNumber);
    }

    private void txtRemarks_Enter(object sender, EventArgs e)
    {
        txtRemarks.BackColor = Color.Thistle;
    }

    private void txtRemarks_Leave(object sender, EventArgs e)
    {
        txtRemarks.BackColor = Color.Thistle;
    }

    private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            btnRemarks_Click(sender, e);
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var frm = new FrmNarrationMaster(true);
            frm.ShowDialog();
            txtRemarks.Text = frm.NarrationMasterDetails;
            txtRemarks.Focus();
        }

        if (e.KeyCode == Keys.Enter) btnSave.Focus();
    }

    #endregion Event

    #region Botton

    private void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            var dt = new DataTable();
            var dt1 = new DataTable();
            var Sales_Miti = string.Empty;
            var Start_Date = DateTime.Now;
            btnSave.Enabled = false;
            if (Tag.ToString() == "NEW")
            {
                if (MessageBox.Show(@"Are You Sure want To Save !", ObjGlobal.Caption, MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information) == DialogResult.No)
                    return;
            }
            else if (Tag.ToString() == "UPDATE")
            {
                if (MessageBox.Show(@"Are You Sure want To Update !", ObjGlobal.Caption, MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information) == DialogResult.No)
                    return;
            }
            else if (Tag.ToString() == "DELETE")
            {
                if (MessageBox.Show(@"Are You Sure want To Delete !", ObjGlobal.Caption, MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information) == DialogResult.No)
                    return;
            }

            if (txtVno.Text.Trim() == string.Empty)
            {
                MessageBox.Show(@"Enter Invoice No.", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txtVno.Focus();
                return;
            }

            if (ObjGlobal.ValidDate(dpDate.Text, "D"))
            {
                if (ObjGlobal.ValidDateRange(Convert.ToDateTime(dpDate.Text)))
                {
                    Start_Date = Convert.ToDateTime(dpDate.Text);
                    Sales_Miti = ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(dpDate.Text).ToShortDateString());
                }
                else
                {
                    MessageBox.Show(
                        "Date Must be Between " + ObjGlobal.CfStartAdDate + " and " + ObjGlobal.CfEndAdDate +
                        " ", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dpDate.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show(@"Plz. Enter Valid Date !", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                dpDate.Focus();
                return;
            }

            if (txtProduct.Text.Trim() == string.Empty)
            {
                MessageBox.Show(@"Plz. Select Finished Goods..!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txtProduct.Focus();
                return;
            }

            if (dgvPurchase.RowCount <= 0)
            {
                MessageBox.Show(@"Plz. Select Expenses Ledger ", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txtLedger.Focus();
                return;
            }

            if (Convert.ToDateTime(Start_Date) < ObjGlobal.CfStartAdDate ||
                Convert.ToDateTime(Start_Date) > ObjGlobal.CfEndAdDate)
            {
                if (ObjGlobal.SysDateType == "D")
                {
                    MessageBox.Show(
                        "Date Must be Between " + ObjGlobal.CfStartAdDate + " and " + ObjGlobal.CfEndAdDate +
                        " ", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dpDate.Focus();
                    return;
                }

                MessageBox.Show(
                    "Date Must be Between " +
                    ObjGlobal.ReturnNepaliDate(ObjGlobal.CfStartAdDate.ToShortDateString()) + " and " +
                    ObjGlobal.ReturnNepaliDate(ObjGlobal.CfEndAdDate.ToShortDateString()) + " ", ObjGlobal.Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                dpDate.Focus();
                return;
            }

            IUDCostCenterExpenses();
        }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
        catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
        {
            btnSave.Enabled = true;
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        var dialogResult = MessageBox.Show("Are you sure want to Close Form..??", ObjGlobal.Caption,
            MessageBoxButtons.YesNo);
        if (DialogResult == DialogResult.Yes) Close();
    }

    #endregion Botton

    #region Method

    private void ButtonEnableDisable(bool bt)
    {
        btnNew.Enabled = bt;
        btnEdit.Enabled = bt;
        btnDelete.Enabled = bt;
        btnReverse.Enabled = bt;
        btnPrint.Enabled = bt;
        btnExit.Enabled = bt;
    }

    private void EnableDisable(bool bt)
    {
        dgvPurchase.ReadOnly = true;
        txtVno.Enabled = bt;
        btnVno.Enabled = bt;
        dpDate.Enabled = bt;
        mskMiti.Enabled = bt;
        txtSalesOrder.Enabled = bt;
        btnSalesOrder.Enabled = bt;
        txtProduct.Enabled = bt;
        txtLedger.Enabled = bt;
        txtCostCenter.Enabled = bt;
        txtRate.Enabled = bt;
        btnSave.Enabled = bt;
        btnCancel.Enabled = bt;
    }

    private void AddDataToGridDetails(int GId)
    {
        Query = "Select * from AMS.GeneralLedger where GlName = '" + txtLedger.Text + "'";
        dt.Reset();
        dt = GetConnection.SelectDataTableQuery(Query);
        if (dt.Rows.Count < 1)
            return;
        if (dt.Rows.Count > 0)
        {
            if (GId == 0 && txtLedger.Text.Trim() != string.Empty)
            {
                var rows = dgvPurchase.Rows.Count;
                dgvPurchase.Rows.Add();
                dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[0].Value = rows + 1;
                dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[1].Value = GLID.ToString();
                dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[2].Value = txtLedger.Text;
                dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[3].Value = CostCenterId;
                dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[4].Value = txtCostCenter.Text;
                if (txtRate.Text.Trim() != string.Empty)
                    dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[5].Value = Convert
                        .ToDouble(ObjGlobal.ReturnDecimal(txtRate.Text)).ToString(ObjGlobal.SysAmountFormat);
                else
                    dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[5].Value =
                        Convert.ToDouble(0).ToString(ObjGlobal.SysAmountFormat);
                if (txtRate.Text.Trim() != string.Empty)
                    dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[6].Value =
                        Convert.ToDouble(txtRate.Text).ToString(ObjGlobal.SysAmountFormat);
                else
                    dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[6].Value =
                        Convert.ToDouble(0).ToString(ObjGlobal.SysAmountFormat);
                dgvPurchase.CurrentCell = dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[currentColumn];
                dgvPurchase.ClearSelection();
            }
            else if (GId != 0 && txtLedger.Text != string.Empty && txtCostCenter.Text != string.Empty)
            {
                dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[1].Value = LedgerCode;
                dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[2].Value = txtLedger.Text;
                dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[3].Value = CostCenterId;
                dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[4].Value = txtCostCenter.Text;
                if (txtRate.Text != string.Empty)
                    dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[5].Value =
                        Convert.ToDouble(txtRate.Text).ToString(ObjGlobal.SysAmountFormat);
                else
                    dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[5].Value =
                        Convert.ToDouble(0).ToString(ObjGlobal.SysAmountFormat);
                if (txtRate.Text != string.Empty)
                    dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[6].Value =
                        Convert.ToDouble(txtRate.Text).ToString(ObjGlobal.SysAmountFormat);
                else
                    dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[6].Value =
                        Convert.ToDouble(0).ToString(ObjGlobal.SysAmountFormat);
                dgvPurchase.CurrentCell = dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[currentColumn];
                dgvPurchase.ClearSelection();
            }
        }

        ObjGlobal.DGridColorCombo(dgvPurchase);
        DetailsTotalCalc();
        ClearDetails();
    }

    private void DetailsTotalCalc()
    {
        double MNetTotal = 0;
        MNetTotal = 0;
        var rcount = dgvPurchase.Rows.Count;
        if (dgvPurchase.Rows.Count > 0)
            for (var i = 0; i < rcount; i++)
                if (dgvPurchase.Rows[i].Cells[2].Value.ToString() != string.Empty &&
                    dgvPurchase.Rows[i].Cells[2].Value != null)
                    if (dgvPurchase.Rows[i].Cells[6].Value.ToString() != string.Empty &&
                        dgvPurchase.Rows[i].Cells[6].Value != null)
                        MNetTotal = MNetTotal + Convert.ToDouble(dgvPurchase[6, i].Value.ToString());

        lblDetTotNetAmt.Text = MNetTotal.ToString(ObjGlobal.SysAmountFormat);
        lbl_NoInWordsDetl.Text =
            ClsMoneyConversion.MoneyConversion(Convert.ToDecimal(ObjGlobal.ReturnDecimal(lblDetTotNetAmt.Text))
                .ToString());
    }

    private void Clear()
    {
        txtVno.Text = string.Empty;
        txtInvoiceNo.Text = string.Empty;
        dpDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        mskMiti.Text = ObjGlobal.ReturnNepaliDate(DateTime.Now.ToString("dd/MM/yyyy"));

        ClearDetails();
        dgvPurchase.Rows.Clear();
        //GridDesign();
        ObjGlobal.DGridColorCombo(dgvPurchase);

        txtSalesOrder.Text = string.Empty;
        txtProduct.Text = string.Empty;
        lblDetTotNetAmt.Text = string.Empty;
        lbl_NoInWordsDetl.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        btnSave.Text = "&Save";
        if (txtVno.Enabled)
            txtVno.Focus();
        else if (mskMiti.Enabled)
            mskMiti.Focus();
    }

    private void ClearDetails()
    {
        GLID = 0;
        CostCenterId = string.Empty;
        txtLedger.Text = string.Empty;
        txtCostCenter.Text = string.Empty;
        txtRate.Text = string.Empty;
    }

    private void IUDCostCenterExpenses()
    {
    }

    private void FillInvoiceData(string Action, string Voucher_No)
    {
        try
        {
            decimal NetAmt = 0;
            Query = string.Empty;
            Query =
                "Select CCM.*,P.PName from  AMS.CostCenterExpenses_Master as CCM Inner Join AMS.CostCenterExpenses_Details as CCD On CCD.Costing_No=CCM.Costing_No Inner Join AMS.Product as P On P.PID=CCM.FGProduct_Id ";
            Query = Query + " WHERE CCM.Costing_No ='" + Voucher_No + "' ";
            dt = GetConnection.SelectDataTableQuery(Query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (Tag.ToString() != "NEW")
                        txtVno.Text = dr["Costing_No"].ToString();
                    mskMiti.Text = dr["Invoice_Miti"].ToString();
                    dpDate.Text = Convert.ToDateTime(dr["Invoice_Date"].ToString()).ToString("dd/MM/yyyy");
                    txtSalesOrder.Text = Convert.ToString(dr["Order_No"].ToString());
                    txtProduct.Text = Convert.ToString(dr["PName"].ToString());
                    txtRemarks.Text = Convert.ToString(dr["Remarks"].ToString());
                }

                dt.Reset();
                Query = string.Empty;
                Query = " Select CCD.*,Gl.GlName,CC.CCName ";
                Query = Query +
                        " From AMS.CostCenterExpenses_Details as CCD INNER JOIN AMS.GeneralLedger AS Gl ON Gl.GlID=CCD.Ledger_Id  LEFT OUTER JOIN	AMS.CostCenter AS CC ON CC.CCID=CCD.CC_Id ";
                Query = Query + " WHERE CCD.Costing_No='" + Voucher_No + "' ";
                dt = GetConnection.SelectDataTableQuery(Query);
                if (dt.Rows.Count > 0)
                {
                    dgvPurchase.Rows.Clear();
                    foreach (DataRow dr in dt.Rows)
                    {
                        var rows = dgvPurchase.Rows.Count;
                        dgvPurchase.Rows.Add();
                        dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[0].Value = dr["SNo"].ToString();
                        dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[1].Value = dr["Ledger_Id"].ToString();
                        dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[2].Value = dr["GlName"].ToString();
                        dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[3].Value = dr["CC_Id"].ToString();
                        dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[4].Value = dr["CCName"].ToString();
                        dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[5].Value = Convert
                            .ToDouble(ObjGlobal.ReturnDecimal(dr["Rate"].ToString()))
                            .ToString(ObjGlobal.SysAmountFormat);
                        dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[6].Value = Convert
                            .ToDouble(ObjGlobal.ReturnDecimal(dr["Amount"].ToString()))
                            .ToString(ObjGlobal.SysAmountFormat);
                        NetAmt = NetAmt + Convert.ToDecimal(ObjGlobal.ReturnDecimal(dr["Amount"].ToString()));
                        dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[7].Value = dr["Narration"].ToString();
                        if (Tag.ToString() != "NEW")
                        {
                            dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[8].Value = dr["Order_No"].ToString();
                            dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[9].Value = dr["Order_Sno"].ToString();
                        }

                        dgvPurchase.CurrentCell = dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[currentColumn];
                    }
                }

                dgvPurchase.ClearSelection();
                lblDetTotNetAmt.Text = Convert.ToDouble(NetAmt).ToString(ObjGlobal.SysAmountFormat);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error Assigning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }

    private void FillOrderData()
    {
        try
        {
            //Query = "";
            //Query = "";
            //ReportTable = GetConnection.SelectDataTableQuery(Query);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error Assigning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
        }
    }

    #endregion Method
}