using MrBLL.DataEntry.Common;
using MrBLL.Utility.Common;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.WinForm;
using MrDAL.Utility.PickList;
using MrDAL.Utility.Server;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MrBLL.Domains.Production.Entry;

public partial class FrmProductionIssue : MrForm
{
    private readonly string _actionTag = string.Empty;

    public FrmProductionIssue(bool zoom, string txtZoomVno)
    {
        InitializeComponent();
    }

    private void FrmProductionIssue_Load(object sender, EventArgs e)
    {
        BackColor = ObjGlobal.FrmBackColor();
        Tag = string.Empty;
        Clear();
        EnableDisable(false);
        ObjGlobal.DGridColorCombo(dgvPurchase);
        btnNew.Focus();
    }

    private void FrmProductionIssue_KeyPress(object sender, KeyPressEventArgs e)
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
                    var dialogResult = MessageBox.Show(@"ARE YOU SURE WANT TO CLEAR FORM..??", ObjGlobal.Caption,
                        MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Clear();
                        EnableDisable(false);
                        ButtonEnableDisable(true);
                        btnSave.Text = @"&SAVE";
                        Tag = string.Empty;
                        btnNew.Focus();
                    }
                }
                else
                {
                    var dialogResult = MessageBox.Show(@"ARE YOU SURE WANT TO CLOSE FORM.!!", ObjGlobal.Caption,
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

    private void btnRemarks_Click(object sender, EventArgs e)
    {
        var frmPickList =
            new FrmAutoPopList("MIN", "NRMASTER", _actionTag, ObjGlobal.SearchText, string.Empty, "LIST");
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
#pragma warning disable CS0414 // The field 'FrmProductionIssue.PID' is assigned but its value is never used
    private long PID = 0;
#pragma warning restore CS0414 // The field 'FrmProductionIssue.PID' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'FrmProductionIssue.GLID' is assigned but its value is never used
    private long GLID = 0;
#pragma warning restore CS0414 // The field 'FrmProductionIssue.GLID' is assigned but its value is never used
    private DataTable dtvalidate = new();
#pragma warning disable CS0414 // The field 'FrmProductionIssue.rowIndex' is assigned but its value is never used
    private int rowIndex = 0;
#pragma warning restore CS0414 // The field 'FrmProductionIssue.rowIndex' is assigned but its value is never used
    private readonly int currentColumn = 0;
    private readonly string LedgerCode = string.Empty;
    private readonly string CostCenterId = string.Empty;
    private readonly TextBox txtInvoiceNo = new();

    #endregion Global

    #region Botton

    private void btnNew_Click(object sender, EventArgs e)
    {
        try
        {
            Text = "Production Issue [NEW]";
            Tag = "NEW";
            btnSave.Text = "&Save";
            EnableDisable(true);
            ButtonEnableDisable(false);
            Clear();
            txtVno.Focus();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult == DialogResult.Yes) Close();
        }

        txtVno.Focus();
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            Text = "Production Issue [UPDATE]";
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
            Text = "Production Issue [DELETE]";
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
            Text = "Production Issue [REVERSE]";
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
        var FrmDP = new FrmDocumentPrint("Document Printing - Production Issue", "RMI", string.Empty, txtVno.Text,
            txtVno.Text, string.Empty, string.Empty, ObjGlobal.SysDefaultInvoicePrinter,
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

    private void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
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

            if (txtCustomer.Text.Trim() == string.Empty)
            {
                MessageBox.Show(@"Enter Party Ledger!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txtCustomer.Focus();
                return;
            }

            if (dgvPurchase.RowCount <= 0)
            {
                MessageBox.Show(@"Enter Production Issue Details of Product ", ObjGlobal.Caption,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txtProduct.Focus();
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

            IUDProductionIssue();
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

    private void txtVno_Validating(object sender, CancelEventArgs e)
    {
    }

    private void txtVno_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void btnVno_Click(object sender, EventArgs e)
    {
    }

    private void txtSalesOrder_Enter(object sender, EventArgs e)
    {
        txtVno.BackColor = Color.Thistle;
    }

    private void txtSalesOrder_Leave(object sender, EventArgs e)
    {
        txtVno.BackColor = Color.Wheat;
    }

    private void txtSalesOrder_Validating(object sender, CancelEventArgs e)
    {
    }

    private void txtSalesOrder_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void btnSalesOrder_Click(object sender, EventArgs e)
    {
    }

    private void mskMiti_Enter(object sender, EventArgs e)
    {
        txtVno.BackColor = Color.Thistle;
    }

    private void mskMiti_Leave(object sender, EventArgs e)
    {
        txtVno.BackColor = Color.Wheat;
    }

    private void mskMiti_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void dpDate_Enter(object sender, EventArgs e)
    {
        txtVno.BackColor = Color.Thistle;
    }

    private void dpDate_Leave(object sender, EventArgs e)
    {
        txtVno.BackColor = Color.Wheat;
    }

    private void dpDate_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void txtCustomer_Enter(object sender, EventArgs e)
    {
        txtVno.BackColor = Color.Thistle;
    }

    private void txtCustomer_Leave(object sender, EventArgs e)
    {
        txtVno.BackColor = Color.Wheat;
    }

    private void txtCustomer_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void txtCustomer_Validating(object sender, CancelEventArgs e)
    {
    }

    private void txtMaster_Enter(object sender, EventArgs e)
    {
        txtVno.BackColor = Color.Thistle;
    }

    private void txtMaster_Leave(object sender, EventArgs e)
    {
        txtVno.BackColor = Color.Wheat;
    }

    private void txtMaster_Validating(object sender, CancelEventArgs e)
    {
    }

    private void txtMaster_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void txtCutterName_Enter(object sender, EventArgs e)
    {
        txtVno.BackColor = Color.Thistle;
    }

    private void txtCutterName_Leave(object sender, EventArgs e)
    {
        txtVno.BackColor = Color.Wheat;
    }

    private void txtCutterName_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void txtCostCenter_Enter(object sender, EventArgs e)
    {
        txtVno.BackColor = Color.Thistle;
    }

    private void txtCostCenter_Leave(object sender, EventArgs e)
    {
        txtVno.BackColor = Color.Wheat;
    }

    private void txtCostCenter_Validating(object sender, CancelEventArgs e)
    {
    }

    private void txtCostCenter_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void txtProduct_Enter(object sender, EventArgs e)
    {
        txtVno.BackColor = Color.Thistle;
    }

    private void txtProduct_Leave(object sender, EventArgs e)
    {
        txtVno.BackColor = Color.Wheat;
    }

    private void txtProduct_Validating(object sender, CancelEventArgs e)
    {
    }

    private void txtProduct_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void txtQty_Enter(object sender, EventArgs e)
    {
        txtVno.BackColor = Color.Thistle;
    }

    private void txtQty_Leave(object sender, EventArgs e)
    {
        txtVno.BackColor = Color.Wheat;
    }

    private void txtQty_Validating(object sender, CancelEventArgs e)
    {
    }

    private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !(sender as TextBox).Text.Contains(".")) return;
        decimal isNumber = 0;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out isNumber);
    }

    private void txtRate_Enter(object sender, EventArgs e)
    {
        txtVno.BackColor = Color.Thistle;
    }

    private void txtRate_Leave(object sender, EventArgs e)
    {
        txtVno.BackColor = Color.Wheat;
    }

    private void txtRate_Validating(object sender, CancelEventArgs e)
    {
    }

    private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void txtRemarks_Enter(object sender, EventArgs e)
    {
        txtVno.BackColor = Color.Thistle;
    }

    private void txtRemarks_Leave(object sender, EventArgs e)
    {
        txtVno.BackColor = Color.Thistle;
    }

    private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1) btnRemarks_Click(sender, e);
    }

    #endregion Event

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
        txtCustomer.Enabled = bt;
        txtCostCenter.Enabled = bt;
        txtRate.Enabled = bt;
        btnSave.Enabled = bt;
        btnCancel.Enabled = bt;
    }

    private void Clear()
    {
        dpDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        mskMiti.Text = ObjGlobal.ReturnNepaliDate(DateTime.Now.ToString("dd/MM/yyyy"));
        ObjGlobal.DGridColorCombo(dgvPurchase);
        txtSalesOrder.Text = string.Empty;
        txtProduct.Text = string.Empty;
        txtCustomer.Text = string.Empty;
        txtCostCenter.Text = string.Empty;
        txtRate.Text = string.Empty;
        lbl_NoInWordsDetl.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        btnSave.Text = "&Save";
        if (txtVno.Enabled)
            txtVno.Focus();
        else if (mskMiti.Enabled)
            mskMiti.Focus();
    }

    private void IUDProductionIssue()
    {
        try
        {
            var sql = new StringBuilder();
            Query = string.Empty;
            sql.Append("BEGIN TRANSACTION \n");
            sql.Append("BEGIN TRY \n");
            if (Tag.ToString() == "NEW")
            {
                sql.Append(
                    " Insert into AMS.SO_Master( SO_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, Ref_Vno, Ref_Date, Ref_Miti, Customer_Id, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, Invoice_Type, Invoice_Mode,Payment_Mode, DueDays, DueDate, Agent_Id, Subledger_Id, IND_Invoice, IND_Date, QOT_Invoice, QOT_Date, \n");
                sql.Append(
                    " Cls1, Cls2, Cls3, Cls4, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Action_Type, R_Invoice, No_Print, In_Words, Remarks, Audit_Lock, Enter_By, Enter_Date, CUnit_Id, CBranch_Id)\n"); //Reconcile_By, Reconcile_Date, Auth_By, Auth_Date,
                sql.Append(" Values ( \n");
                sql.Append(" '" + txtVno.Text.Trim() + "',\n");
                sql.Append(" '" + Convert.ToDateTime(dpDate.Text).ToString("yyyy-MM-dd") + "',\n");
                sql.Append(" '" + mskMiti.Text + "',\n");
                sql.Append(" '" + DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToShortTimeString() +
                           "',\n");
            }
            else if (Tag.ToString() == "UPDATE")
            {
                sql.Append(" Update AMS.SO_Master set \n");
                sql.Append(" Invoice_Date = '" + Convert.ToDateTime(dpDate.Text).ToString("yyyy-MM-dd") + "',\n");
                sql.Append(" Invoice_Miti = '" + mskMiti.Text + "',\n");
                sql.Append(" Invoice_Time = '" + DateTime.Now.ToString("yyyy-MM-dd") + " " +
                           DateTime.Now.ToShortTimeString() + "',\n");
                sql.Append(" Action_Type = 'EDIT', \n");
                sql.Append(" In_Words = '" + lbl_NoInWordsDetl.Text.Trim() + "',\n");
                sql.Append(" Remarks = '" + txtRemarks.Text.Trim().Replace("'", "''") + "',\n");
                sql.Append(" Enter_By =  '" + ObjGlobal.LogInUser + "',\n");
                sql.Append(" Enter_Date =  '" + DateTime.Now.ToString("yyyy-MM-dd") + " " +
                           DateTime.Now.ToShortTimeString() + "' \n");
                sql.Append(" where SO_Invoice = '" + txtVno.Text.Trim() + "' \n");
            }
            else if (Tag.ToString() == "DELETE")
            {
                sql.Append(" Delete from AMS.CostCenterExpenses_Details where Costing_No = '" + txtVno.Text.Trim() +
                           "'\n");
                sql.Append(" Delete from AMS.CostCenterExpenses_Master where Costing_No = '" + txtVno.Text.Trim() +
                           "'\n");
            }

            sql.Append("  Commit Transaction\n");
            sql.Append("  END TRY \n");
            sql.Append("  BEGIN CATCH \n");
            sql.Append("  ROLLBACK TRANSACTION \n");
            sql.Append("  END CATCH \n");
            var cmd = new SqlCommand(sql.ToString(), GetConnection.GetSqlConnection());
            var Result = cmd.ExecuteNonQuery();
            if (Result > 0)
            {
                if (Tag.ToString() == "NEW")
                    MessageBox.Show(@"Data Saved Successfully!!!", ObjGlobal.Caption);
                else if (Tag.ToString() == "UPDATE")
                    MessageBox.Show(@"Data Updated Successfully!!!", ObjGlobal.Caption);
                else if (Tag.ToString() == "DELETE")
                    MessageBox.Show(@"Data Deleted Successfully!!!", ObjGlobal.Caption);

                Clear();
                btnSave.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, ex.Message, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            btnSave.Enabled = true;
            if (DialogResult == DialogResult.Yes) Close();
        }
    }

    private void FillOrderData()
    {
        try
        {
            Query = string.Empty;
            Query = string.Empty;
            dt = GetConnection.SelectDataTableQuery(Query);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error Assigning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
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
            "Select PName,PShortName,Convert(Decimal(18,2),Sum(BalanceQty) ) BalanceQty,UnitCode,Convert(Decimal(18,2),PBuyRate ) PBuyRate,Convert(Decimal(18,2),PSalesRate)PSalesRate,GrpName From (Select P.PName,P.PShortName,Case When EntryType='I' Then isnull(Sum(StockQty),0) Else  -isnull(Sum(StockQty),0) End BalanceQty,PU.UnitCode,P.PBuyRate,P.PSalesRate,PG.GrpName  from AMS.Product  as P left outer join AMS.StockDetails as SD on P.PID=SD.Product_Id ";
        Query = Query +
                " left outer join AMS.ProductUnit as PU on  P.PUnit=PU.UID left outer join AMS.ProductGroup as PG on  PG.PGrpID=p.PGrpId Group By P.PName,P.PShortName,PU.UnitCode,P.PBuyRate,P.PSalesRate,PG.GrpName,EntryType ) as aa Group by PName,PShortName,UnitCode,PBuyRate,PSalesRate,GrpName";
        using var PkLst = new FrmPickList(Query, HeaderCap, ColumnWidths, "Product List", string.Empty);
        if (PkLst.ShowDialog() != DialogResult.OK) return;
        if (ClsPickList.PlValue1 == null || ClsPickList.PlValue2 == null || ClsPickList.PlValue1 == string.Empty ||
            ClsPickList.PlValue2 == string.Empty) return;
        txtProduct.Text = ClsPickList.PlValue1;
        txtProduct.Focus();
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
                    txtCustomer.Text = ClsPickList.PlValue2;
                    txtCustomer.Focus();
                }
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
        HeaderCap.Add("ShortName");
        ColumnWidths.Add(0);
        ColumnWidths.Add(275);
        ColumnWidths.Add(120);
        Query = "SELECT CCId,CCName,CCcode From AMS.CostCenter Order By CCName";
        using var PkLst = new FrmPickList(Query, HeaderCap, ColumnWidths, "CostCenter List", string.Empty);
        if (PkLst.ShowDialog() != DialogResult.OK) return;
        if (ClsPickList.PlValue1 == null || ClsPickList.PlValue2 == null || ClsPickList.PlValue1 == string.Empty ||
            ClsPickList.PlValue2 == string.Empty) return;
        txtCostCenter.Text = ClsPickList.PlValue2;
        txtCostCenter.Focus();
    }

    private void AddDataToGridDetails(int GId)
    {
        DataRow row2;
        if (dt.Rows.Count > 0)
        {
            if (GId == 0 && txtProduct.Text != string.Empty)
            {
                var rows = dgvPurchase.Rows.Count;
                dgvPurchase.Rows.Add();
                dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[0].Value = rows + 1;
                dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[1].Value = LedgerCode;
                dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[2].Value = txtProduct.Text;
                dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[3].Value = CostCenterId;
                dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[4].Value = txtCostCenter.Text;
                dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[5].Value = txtRate.Text != string.Empty
                    ? Convert.ToDouble(txtRate.Text).ToString(ObjGlobal.SysQtyFormat)
                    : Convert.ToDouble(0).ToString(ObjGlobal.SysQtyFormat);
                dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[6].Value = txtRate.Text != string.Empty
                    ? Convert.ToDouble(txtRate.Text).ToString(ObjGlobal.SysAmountFormat)
                    : Convert.ToDouble(0).ToString(ObjGlobal.SysAmountFormat);
                dgvPurchase.CurrentCell = dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[currentColumn];
                dgvPurchase.ClearSelection();
            }
            else if (GId != 0 && txtProduct.Text != string.Empty && txtCostCenter.Text != string.Empty)
            {
                dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[1].Value = LedgerCode;
                dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[2].Value = txtProduct.Text;
                dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[3].Value = CostCenterId;
                dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[4].Value = txtCostCenter.Text;
                dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[5].Value = txtRate.Text != string.Empty
                    ? Convert.ToDouble(txtRate.Text).ToString(ObjGlobal.SysQtyFormat)
                    : Convert.ToDouble(0).ToString(ObjGlobal.SysQtyFormat);
                dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[6].Value = txtRate.Text != string.Empty
                    ? Convert.ToDouble(txtRate.Text).ToString(ObjGlobal.SysAmountFormat)
                    : Convert.ToDouble(0).ToString(ObjGlobal.SysAmountFormat);
                dgvPurchase.CurrentCell = dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[currentColumn];
                dgvPurchase.ClearSelection();
            }
        }

        ObjGlobal.DGridColorCombo(dgvPurchase);
        DetailsTotalCalc();
        Clear();
    }

    private void DetailsTotalCalc()
    {
        double MNetTotal = 0;
        MNetTotal = 0;
        var rowsCount = dgvPurchase.Rows.Count;
        if (dgvPurchase.Rows.Count > 0)
            for (var i = 0; i < rowsCount; i++)
                if (dgvPurchase.Rows[i].Cells[2].Value.ToString() != string.Empty &&
                    dgvPurchase.Rows[i].Cells[2].Value != null)
                    if (dgvPurchase.Rows[i].Cells[6].Value.ToString() != string.Empty &&
                        dgvPurchase.Rows[i].Cells[6].Value != null)
                        MNetTotal += Convert.ToDouble(dgvPurchase[6, i].Value.ToString());

        lblDetTotNetAmt.Text = MNetTotal.ToString(ObjGlobal.SysAmountFormat);
        lbl_NoInWordsDetl.Text =
            ClsMoneyConversion.MoneyConversion(lblDetTotNetAmt.GetDateString());
    }

    #endregion Method
}