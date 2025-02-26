using DevExpress.XtraEditors;
using MrBLL.DataEntry.Common;
using MrBLL.Master.LedgerSetup;
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

public partial class FrmCostCenterIssue : MrForm
{
    private readonly string _ActionTag = string.Empty;
    private ClsEntryForm GetFrom = null;

    public FrmCostCenterIssue(bool Zoom, string TxtZoomVno)
    {
        InitializeComponent();
    }

    private void FrmCostCenterIssue_Load(object sender, EventArgs e)
    {
        BackColor = ObjGlobal.FrmBackColor();
        Tag = string.Empty;
        Clear();
        EnableDisable(false);
        ObjGlobal.DGridColorCombo(dgvPurchase);
        ObjGlobal.GetFormAccessControl([btnNew, btnEdit, btnDelete], this.Tag);
    }

    private void FrmCostCenterIssue_KeyPress(object sender, KeyPressEventArgs e)
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
#pragma warning disable CS0414 // The field 'FrmCostCenterIssue.PID' is assigned but its value is never used
    private long PID = 0;
#pragma warning restore CS0414 // The field 'FrmCostCenterIssue.PID' is assigned but its value is never used
    private long GLID;
    private DataTable dtvalidate = new();
#pragma warning disable CS0414 // The field 'FrmCostCenterIssue.rowIndex' is assigned but its value is never used
    private int rowIndex = 0;
#pragma warning restore CS0414 // The field 'FrmCostCenterIssue.rowIndex' is assigned but its value is never used
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

            IUDCostCenterIssue();
        }
        catch (Exception ex)
        {
            var errMsg = ex.Message;
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
                    //rowadj = false;
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

    private void btnVno_Click(object sender, EventArgs e)
    {
    }

    private void txtReceivedNo_Enter(object sender, EventArgs e)
    {
        txtReceivedNo.BackColor = Color.Thistle;
        txtReceivedNo.SelectAll();
    }

    private void txtReceivedNo_Leave(object sender, EventArgs e)
    {
        txtReceivedNo.BackColor = Color.Wheat;
    }

    private void txtReceivedNo_Validating(object sender, CancelEventArgs e)
    {
    }

    private void txtReceivedNo_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void btnReceivedNo_Click(object sender, EventArgs e)
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
            if (ObjGlobal.SysDateType == "D")
                Query = Query + " Group By SOM.SO_Invoice,SOM.Invoice_Date, GLName,SOM.N_Amount";
            else
                Query = Query + " Group By SOM.SO_Invoice,SOM.Invoice_Miti, GLName,SOM.N_Amount";
            Query = Query + " Order By SOM.SO_Invoice";
            var PkLst = new FrmPickList(Query, HeaderCap, ColumnWidths, "Sales Order List", string.Empty);
            if (PkLst.ShowDialog() == DialogResult.OK)
                if (ClsPickList.PlValue1 != null && ClsPickList.PlValue2 != null &&
                    ClsPickList.PlValue1 != string.Empty && ClsPickList.PlValue2 != string.Empty)
                {
                    txtReceivedNo.Text = ClsPickList.PlValue1;
                    txtReceivedNo.Focus();
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
        mskMiti.BackColor = Color.Thistle;
    }

    private void mskMiti_Validating(object sender, CancelEventArgs e)
    {
    }

    private void mskMiti_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void dpDate_Enter(object sender, EventArgs e)
    {
        dpDate.BackColor = Color.Thistle;
    }

    private void dpDate_Leave(object sender, EventArgs e)
    {
        dpDate.BackColor = Color.Wheat;
    }

    private void dpDate_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void txtCustomer_Enter(object sender, EventArgs e)
    {
        txtCustomer.BackColor = Color.Thistle;
        txtCustomer.SelectAll();
    }

    private void txtCustomer_Leave(object sender, EventArgs e)
    {
        txtCustomer.BackColor = Color.Wheat;
    }

    private void txtCustomer_Validating(object sender, CancelEventArgs e)
    {
    }

    private void txtCustomer_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void txtMaster_Enter(object sender, EventArgs e)
    {
        txtMaster.BackColor = Color.Thistle;
    }

    private void txtMaster_Leave(object sender, EventArgs e)
    {
        txtMaster.BackColor = Color.Wheat;
    }

    private void txtMaster_Validating(object sender, CancelEventArgs e)
    {
    }

    private void txtMaster_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void txtCutterName_Enter(object sender, EventArgs e)
    {
        txtCutterName.BackColor = Color.Thistle;
        txtCutterName.SelectAll();
    }

    private void txtCutterName_Leave(object sender, EventArgs e)
    {
        txtCutterName.BackColor = Color.Wheat;
    }

    private void txtCutterName_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void txtFrmCostCenter_Enter(object sender, EventArgs e)
    {
        txtFrmCostCenter.BackColor = Color.Thistle;
        txtFrmCostCenter.SelectAll();
    }

    private void txtFrmCostCenter_Leave(object sender, EventArgs e)
    {
        txtFrmCostCenter.BackColor = Color.Wheat;
    }

    private void txtFrmCostCenter_Validating(object sender, CancelEventArgs e)
    {
    }

    private void txtFrmCostCenter_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void txtToCostCenter_Enter(object sender, EventArgs e)
    {
        txtToCostCenter.BackColor = Color.Thistle;
        txtToCostCenter.SelectAll();
    }

    private void txtToCostCenter_Leave(object sender, EventArgs e)
    {
        txtToCostCenter.BackColor = Color.Thistle;
    }

    private void txtToCostCenter_Validating(object sender, CancelEventArgs e)
    {
    }

    private void txtToCostCenter_KeyDown(object sender, KeyEventArgs e)
    {
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

    private void txtProduct_Validating(object sender, CancelEventArgs e)
    {
    }

    private void txtProduct_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void txtQty_Enter(object sender, EventArgs e)
    {
        txtQty.BackColor = Color.Thistle;
        txtQty.SelectAll();
    }

    private void txtQty_Leave(object sender, EventArgs e)
    {
        txtQty.BackColor = Color.Wheat;
    }

    private void txtQty_Validating(object sender, CancelEventArgs e)
    {
    }

    private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void txtRate_Enter(object sender, EventArgs e)
    {
        txtRate.BackColor = Color.Thistle;
        txtRate.SelectAll();
    }

    private void txtRate_Leave(object sender, EventArgs e)
    {
        txtRate.BackColor = Color.Wheat;
    }

    private void txtRate_Validating(object sender, CancelEventArgs e)
    {
    }

    private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void txtRemarks_Enter(object sender, EventArgs e)
    {
        txtRemarks.BackColor = Color.Thistle;
        txtRemarks.Select();
    }

    private void txtRemarks_Leave(object sender, EventArgs e)
    {
        txtRemarks.BackColor = Color.Wheat;
    }

    private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) btnSave.Focus();
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
        txtReceivedNo.Enabled = bt;
        btnReceivedNo.Enabled = bt;
        txtProduct.Enabled = bt;
        txtRate.Enabled = bt;
        btnSave.Enabled = bt;
        btnCancel.Enabled = bt;
    }

    private void AddDataToGridDetails(int GId)
    {
        Query = "Select * from AMS.Product where PName = '" + txtProduct.Text + "'";
        dt.Reset();
        dt = GetConnection.SelectDataTableQuery(Query);
        if (dt.Rows.Count < 1)
            return;
        if (dt.Rows.Count > 0)
        {
            if (GId == 0 && txtProduct.Text.Trim() != string.Empty)
            {
                var rows = dgvPurchase.Rows.Count;
                dgvPurchase.Rows.Add();
                dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[0].Value = rows + 1;
                dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[1].Value = GLID.ToString();
                dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[2].Value = txtProduct.Text;
                dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[3].Value = CostCenterId;
                // dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[4].Value = txtCostCenter.Text.ToString();
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
            else if (GId != 0 && txtProduct.Text != string.Empty /* && ((txtCustomer.Text) != "")*/)
            {
                dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[1].Value = LedgerCode;
                dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[2].Value = txtProduct.Text;
                dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[3].Value = CostCenterId;
                //dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[4].Value = txtCustomer.Text.ToString();
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

        txtReceivedNo.Text = string.Empty;
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
        txtRate.Text = string.Empty;
    }

    private void IUDCostCenterIssue()
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
                    txtReceivedNo.Text = Convert.ToString(dr["Order_No"].ToString());
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

    #endregion Method
}