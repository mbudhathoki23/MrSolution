using DevExpress.XtraEditors;
using MrBLL.DataEntry.Common;
using MrBLL.Master.LedgerSetup;
using MrBLL.Master.ProductSetup;
using MrBLL.Utility.Common;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Interface.ProductionSystem.FinishedGoodReceived;
using MrDAL.DataEntry.ProductionSystem.FinishedGoodReceived;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Global.Common;
using MrDAL.Global.WinForm;
using MrDAL.Utility.PickList;
using MrDAL.Utility.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.DataEntry.ProductionMaster;

public partial class FrmFinishedGoodsReceived : MrForm
{
    #region --------------- EVENT ---------------

    private readonly string _actionTag = string.Empty;

    public FrmFinishedGoodsReceived(bool zoom, string txtZoomVno)
    {
        _finished = new FinishedGoodReceivedRepository();
        InitializeComponent();
    }

    private void FrmFinishedGoodsReceived_Load(object sender, EventArgs e)
    {
        BackColor = ObjGlobal.FrmBackColor();
        Clear();
        Tag = string.Empty;
        EnableDisable(false);
        ObjGlobal.DGridColorCombo(dgvPurchase);
        ObjGlobal.GetFormAccessControl([btnNew, btnEdit, btnDelete], this.Tag);
    }

    private void FrmFinishedGoodsReceived_KeyPress(object sender, KeyPressEventArgs e)
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

    #endregion --------------- EVENT ---------------

    #region --------------- EVENT ---------------

    private void btnNew_Click(object sender, EventArgs e)
    {
        try
        {
            Text = "Finish Goods Receive [NEW]";
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
            Text = "Finish Goods Receive [UPDATE]";
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
            Text = "Finish Goods Receive [DELETE]";
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
            Text = "Finish Goods Receive [REVERSE]";
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
        var FrmDP = new FrmDocumentPrint("Document Printing - Finish Goods Receive", "PCCE", string.Empty,
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
                Query = "SELECT FGR_No FROM AMS.FGR_Master Where FGR_No='" + txtVno.Text + "'";
                _dt.Reset();
                _dt = GetConnection.SelectDataTableQuery(Query);
                if (_dt.Rows.Count > 0)
                {
                    MessageBox.Show(
                        "Invoice Cannot Save Dublicate, Please Check The Invoice Number <This Invoice ALready Enter Plz Enter New One>!");
                    txtVno.Text = string.Empty;
                    txtVno.Focus();
                }
            }
            else if (Tag.ToString() == "UPDATE" || Tag.ToString() == "DELETE" || Tag.ToString() == "REVERSE")
            {
                Query = "SELECT FGR_No FROM AMS.FGR_Master Where FGR_No='" + txtVno.Text + "'";
                _dt.Reset();
                _dt = GetConnection.SelectDataTableQuery(Query);
                if (_dt.Rows.Count > 0)
                {
                    Clear();
                    //rowadj = false;
                    FillIssueNo(string.Empty, _dt.Rows[0]["FGR_No"].ToString());
                    ObjGlobal.DGridColorCombo(dgvPurchase);
                    // lbl_NoInWordsDetl.Text = ClsMoneyConversion.MoneyConversion(Convert.ToDecimal(ObjGlobal.DecimalValue(lblDetTotNetAmt.Text)).ToString());
                    txtVno.Enabled = false;
                    if (dpDate.Enabled)
                    {
                        dpDate.Focus();
                    }
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
                MessageBox.Show("FGR Number Cannot Left  Blank..!!");
                txtVno.Focus();
            }
        }

        if (e.KeyCode == Keys.F1) btnVno_Click(sender, e);
    }

    private void btnVno_Click(object sender, EventArgs e)
    {
    }

    private void txtGdn_Enter(object sender, EventArgs e)
    {
    }

    private void txtGdn_Leave(object sender, EventArgs e)
    {
    }

    private void txtGdn_KeyDown(object sender, KeyEventArgs e)
    {
        var Searchtext = e.KeyCode.ToString();
        if (e.KeyCode == Keys.F1)
        {
            BtnGodown.PerformClick();
        }
        if (e.KeyCode == Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
        else
            Searchtext = e.KeyData.ToString();
        //ObjGlobal.KeyEventDevexpress((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE", Searchtext, TxtGodown, BtnGodown);
    }

    private void txtGdn_Validating(object sender, CancelEventArgs e)
    {
        _godownId = 0;
        if (TxtGodown.Text.Trim() != string.Empty)
        {
        }
    }

    private void mskMiti_Enter(object sender, EventArgs e)
    {
        mskMiti.BackColor = Color.Thistle;
    }

    private void mskMiti_Leave(object sender, EventArgs e)
    {
        mskMiti.BackColor = Color.Wheat;
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
    }

    private void dpDate_KeyDown(object sender, KeyEventArgs e)
    {
        if (dpDate.Text != "  /  /")
            mskMiti.Text = GetConnection.GetQueryData("Select BS_DateDMY from AMS.DateMiti where AD_Date='" +
                                                      Convert.ToDateTime(dpDate.Text).ToString("yyyy-MM-dd") + "'");
        if (dpDate.Text.Trim() != string.Empty)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void txtRawMaterial_Enter(object sender, EventArgs e)
    {
        txtRawMaterial.BackColor = Color.Thistle;
        txtRawMaterial.SelectAll();
    }

    private void txtRawMaterial_Leave(object sender, EventArgs e)
    {
        txtRawMaterial.BackColor = Color.Wheat;
    }

    private void txtRawMaterial_Validating(object sender, CancelEventArgs e)
    {
        try
        {
            if (Tag != null && (Tag.GetString() == string.Empty || ActiveControl == txtRawMaterial))
            {
                return;
            }

            if (txtRawMaterial.Text.Trim() == string.Empty && txtRawMaterial.Enabled && dgvPurchase.Rows.Count == 0)
            {
                MessageBox.Show(@"Product cannot be left Blank..!!", ObjGlobal.Caption);
                txtRawMaterial.Focus();
                return;
            }

            if (txtRawMaterial.Text.Trim() != string.Empty)
            {
                Query = "Select PID,PName,PShortName From AMS.Product where PName='" + txtRawMaterial.Text + "'";
                _dtvalidate.Reset();
                _dtvalidate = GetConnection.SelectDataTableQuery(Query);
                if (_dtvalidate.Rows.Count > 0)
                {
                    Convert.ToInt64(_dtvalidate.Rows[0]["PID"].ToString());
                }
                else
                {
                    MessageBox.Show(@"Does Not Exits Product.!!", ObjGlobal.Caption);
                    txtRawMaterial.Focus();
                }
            }
            else
            {
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
        }
    }

    private void btnProductList_Click(object sender, EventArgs e, string fieldName = "")
    {
        ClsPickList.PlValue1 = string.Empty;
        ClsPickList.PlValue2 = string.Empty;
        ClsPickList.PlValue3 = string.Empty;
        ClsPickList.PlValue4 = string.Empty;
        _headerCap.Clear();
        _columnWidths.Clear();
        _headerCap.Add("PID");
        _headerCap.Add("Name");
        _headerCap.Add("Code");
        _headerCap.Add("Stock Qty");
        _headerCap.Add("Unit");
        _headerCap.Add("Buy Rate");
        _headerCap.Add("Sales Rate");
        _headerCap.Add("Group");

        _columnWidths.Add(0);
        _columnWidths.Add(200);
        _columnWidths.Add(120);
        _columnWidths.Add(120);
        _columnWidths.Add(80);
        _columnWidths.Add(110);
        _columnWidths.Add(110);
        _columnWidths.Add(120);
        Query =
            "Select PID, PName,PShortName,Convert(Decimal(18,2),Sum(BalanceQty) ) BalanceQty,UnitCode,Convert(Decimal(18,2),PBuyRate ) PBuyRate,Convert(Decimal(18,2),PSalesRate)PSalesRate,GrpName,PUnit From (Select P.PID,P.PName,P.PShortName,Case When EntryType='I' Then isnull(Sum(StockQty),0) Else  -isnull(Sum(StockQty),0) End BalanceQty,PU.UnitCode,p.PUnit,P.PBuyRate,P.PSalesRate,PG.GrpName  from AMS.Product  as P left outer join AMS.StockDetails as SD on P.PID=SD.Product_Id ";
        Query = Query +
                " left outer join AMS.ProductUnit as PU on  P.PUnit=PU.UID left outer join AMS.ProductGroup as PG on  PG.PGrpID=p.PGrpId Group By P.PID, P.PName,P.PShortName,PU.UnitCode,P.PBuyRate,P.PSalesRate,PG.GrpName,EntryType,p.PUnit ) as aa Group by PID,PName,PShortName,UnitCode,PBuyRate,PSalesRate,GrpName,PUnit";
        using (var PkLst = new FrmPickList(Query, _headerCap, _columnWidths, "Product List", string.Empty))
        {
            if (PkLst.ShowDialog() == DialogResult.OK)
                if (ClsPickList.PlValue1 != null && ClsPickList.PlValue2 != null &&
                    ClsPickList.PlValue1 != string.Empty && ClsPickList.PlValue2 != string.Empty)
                    if (fieldName == txtRawMaterial.Name)
                    {
                        txtRawMaterial.Text = ClsPickList.PlValue2;
                        txtRawMaterial.Tag = ClsPickList.PlValue1;
                        // lblRawQtyUnit.Tag = ClsPickList._plValue5;
                        txtRawMaterial.Focus();
                    }
        }
    }

    private void txtRawMaterial_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.F6 && e.Control != true && e.KeyCode != Keys.N && e.KeyCode != Keys.ShiftKey &&
            e.KeyCode != Keys.Escape && e.KeyCode != Keys.Back && e.KeyCode != Keys.Enter &&
            e.KeyCode != Keys.Alt && e.KeyCode != Keys.Space && e.KeyCode != Keys.Menu && e.KeyCode != Keys.LMenu &&
            e.KeyCode != Keys.RMenu && e.KeyCode != Keys.LWin &&
            e.KeyCode != Keys.RWin) btnProductList_Click(sender, e, txtRawMaterial.Name);
        if (e.Control && e.KeyCode == Keys.N)
            using (var frm = new FrmProduct(true))
            {
                frm.ShowDialog();
                txtRawMaterial.Text = frm.ProductName;
                txtRawMaterial.Focus();
            }

        if (e.KeyCode == Keys.Enter)
            if (txtRawMaterial.Text.Trim() == string.Empty)
                btnProductList_Click(sender, e, txtRawMaterial.Name);
        //else
        //    txtRawMaterial.Focus();
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

    private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void txtQty_Validating(object sender, CancelEventArgs e)
    {
        if (ActiveControl == txtQty) return;
        if (string.IsNullOrEmpty(txtQty.Text.Trim()))
        {
            MessageBox.Show(@"Quantiy Cannot Left Blank..!!", ObjGlobal.Caption);
            txtQty.Focus();
        }
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
        txtRate.Text = Convert.ToDouble(ObjGlobal.ReturnDecimal(txtRate.Text)).ToString(ObjGlobal.SysAmountFormat);
    }

    private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        try
        {
            if (e.KeyChar == (char)Keys.Back ||
                e.KeyChar == '.' && !(sender as TextBox).Text.Contains(".")) return;
            decimal isNumber = 0;
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out isNumber);

            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Tab)
                if (txtRawMaterial.Text.Trim() != string.Empty && txtRate.Text.Trim() != string.Empty)
                {
                    if (dgvPurchase.Rows.Count >= 1)
                    {
                        if (dgvPurchase.Rows[RowIndex].Selected)
                            AddDataToGridDetails(Convert.ToInt16(dgvPurchase.SelectedRows[0].Cells[0].Value));
                        else
                            AddDataToGridDetails(0);
                    }
                    else
                    {
                        AddDataToGridDetails(0);
                    }

                    txtRawMaterial.Focus();
                }
        }
        catch (InvalidOperationException ex)
        {
            MessageBox.Show(ex.Message, "Error Assigning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
        }
    }

    private void txtRemarks_Enter(object sender, EventArgs e)
    {
        txtRemarks.BackColor = Color.Thistle;
        txtRemarks.SelectAll();
    }

    private void txtRemarks_Leave(object sender, EventArgs e)
    {
        txtRemarks.BackColor = Color.Wheat;
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
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        //btnSave.Enabled = false;
        //_objProduction.BomMaster._Tag = Tag.ToString();
        //_objProduction.BomMaster.FGR_No = txtVno.Text;
        //_objProduction.BomMaster.Invoice_Date = DateTime.Now;
        //_objProduction.BomMaster.Invoice_Miti = mskMiti.Text;
        //// _objProduction.BomMaster.Memo_Desc = txtProduct.Text;
        //_objProduction.BomMaster.Order_No = string.Empty;
        //_objProduction.BomMaster.Order_Date = null;
        //// _objProduction.BomMaster.FGProduct_Id = Convert.ToInt32(txtProduct.Tag);
        ////  _objProduction.BomMaster.Alt_Qty = 0;
        ////  _objProduction.BomMaster.AltUnit_Id = 0;
        //// _objProduction.BomMaster.Qty = Convert.ToDecimal(txtFGQty.Text);
        //_objProduction.BomMaster.Unit_Id = 0;
        //_objProduction.BomMaster.Factor = 0;
        //_objProduction.BomMaster.FFactor = 0;
        //_objProduction.BomMaster.Cost_Rate = 0;
        //_objProduction.BomMaster.Gdn_Id = 0;
        //_objProduction.BomMaster.CC_Id = 0;
        //_objProduction.BomMaster.Cls1 = 0;
        //_objProduction.BomMaster.Cls2 = 0;
        //_objProduction.BomMaster.Cls3 = 0;
        //_objProduction.BomMaster.Cls4 = 0;
        //_objProduction.BomMaster.Total_Qty = 0;
        //_objProduction.BomMaster.N_Amount = 0;
        //_objProduction.BomMaster.Remarks = string.Empty;
        //_objProduction.BomMaster.Module = string.Empty;
        //_objProduction.BomMaster.Action_Type = string.Empty;
        //_objProduction.BomMaster.Enter_By = string.Empty;
        //_objProduction.BomMaster.Enter_Date = DateTime.Now;
        //_objProduction.BomMaster.Reconcile_By = string.Empty;
        //_objProduction.BomMaster.Reconcile_Date = null;
        //_objProduction.BomMaster.Auth_By = string.Empty;
        //_objProduction.BomMaster.Auth_Date = null;
        //_objProduction.BomMaster.CUnit_Id = Convert.ToInt32(ObjGlobal.SysCompanyUnitId);
        //_objProduction.BomMaster.CBranch_Id = Convert.ToInt32(ObjGlobal.SysBranchId);
        //_objProduction.BomMaster.FiscalYear = Convert.ToInt32(ObjGlobal.SysFiscalYearId);

        //VmBillofMaterialsDetails bomDetails = null;

        //foreach (DataGridViewRow item in dgvPurchase.Rows)
        //{
        //    bomDetails = new VmBillofMaterialsDetails()
        //    {
        //        Memo_No = _objProduction.BomMaster.Memo_No,
        //        SNo = item.Cells["dgv_Sno"].Value.GetInt(),
        //        Product_Id = item.Cells["dgv_ProductId"].Value.GetLong(),
        //        Gdn_Id = item.Cells["gvn_GdnId"].Value.GetInt(),
        //        CC_Id = item.Cells["gvn_CCId"].Value.GetInt(),
        //        Alt_Qty = 0,
        //        AltUnit_Id = 0,
        //        Qty = Convert.ToDecimal(item.Cells["dgv_Qty"].Value.ToString()),
        //        Unit_Id = item.Cells["dgv_UnitId"].Value.GetString() != string.Empty ? Convert.ToInt32(item.Cells["dgv_UnitId"].Value.ToString()) : 0,
        //        Rate = Convert.ToDecimal(item.Cells["dgv_Rate"].Value.ToString()),
        //        Amount = Convert.ToDecimal(item.Cells["dgv_Amount"].Value.ToString()),
        //        Narration = string.Empty,
        //        Order_No = string.Empty,
        //        Order_SNo = 0
        //    };
        //    _objProduction.BomDetails.Add(bomDetails);
        //}

        //var isSave = _objProduction.SaveFinishedGoods();
        //if (isSave <= 0)
        //{
        //    return;
        //}
        //MessageBox.Show(@"DATA SAVED SUCCESSFULLY", ObjGlobal.Caption);
        //Clear();
        //btnSave.Enabled = true;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        if (mskMiti.Enabled)
        {
            mskMiti.Focus();
        }
    }

    private void AddDataToGridDetails(int gId)
    {
        //  Query = "Select * from AMS.GeneralLedger where GlName = '" + txtLedger.Text + "'";
        _dt.Reset();
        _dt = GetConnection.SelectDataTableQuery(Query);
        if (_dt.Rows.Count < 1)
        {
            return;
        }
        if (_dt.Rows.Count > 0)
        {
            var rows = dgvPurchase.Rows.Count;
            dgvPurchase.Rows.Add();
            dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells["dgv_Sno"].Value = rows + 1;
            dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells["dgv_ProductId"].Value = txtRawMaterial.Tag.ToString();
            dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells["dgv_Description"].Value = txtRawMaterial.Text;
            dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells["gvn_GdnId"].Value = TxtGodown.Tag != null ? TxtGodown.Tag.ToString() : string.Empty;
            dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells["gvn_CCId"].Value = TxtCostcenter.Tag != null ? TxtCostcenter.Tag.ToString() : string.Empty;
            dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells["Godown"].Value = TxtGodown.Text;
            dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells["CostCenter"].Value = TxtCostcenter.Text;
            dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells["dgv_UnitId"].Value = string.Empty; //lblRawQtyUnit.Tag.ToString();
            dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells["dgv_Unit"].Value = string.Empty; //lblRawQtyUnit.Text.ToString();
            dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells["dgv_Rate"].Value = txtRate.Text.Trim() != string.Empty
                ? Convert.ToDouble(ObjGlobal.ReturnDecimal(txtRate.Text)).ToString(ObjGlobal.SysAmountFormat)
                : Convert.ToDouble(0).ToString(ObjGlobal.SysAmountFormat);

            dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells["dgv_Qty"].Value = txtQty.Text.Trim() != string.Empty
                ? Convert.ToDouble(txtQty.Text).ToString(ObjGlobal.SysAmountFormat)
                : Convert.ToDouble(0).ToString(ObjGlobal.SysAmountFormat);

            decimal.TryParse(dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells["dgv_Qty"].Value.ToString(), out var qty);
            decimal.TryParse(dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells["dgv_Rate"].Value.ToString(), out var rate);

            dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells["dgv_Amount"].Value = qty * rate;

            dgvPurchase.CurrentCell = dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[CurrentColumn];
            dgvPurchase.ClearSelection();

            dgvPurchase.CurrentCell = dgvPurchase.Rows[dgvPurchase.RowCount - 1].Cells[CurrentColumn];
            dgvPurchase.ClearSelection();
        }
        ObjGlobal.DGridColorCombo(dgvPurchase);
        // VoucherTotal();
    }

    private void BtnGodown_Click(object sender, EventArgs e)
    {
        var frmPickList = new FrmAutoPopList("MAX", "GODOWN", _actionTag, ObjGlobal.SearchText, "SAVE", "ALL", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtGodown.Text = frmPickList.SelectedList[0]["PName"].ToString().Trim();
                _godownId = Convert.ToInt32(frmPickList.SelectedList[0]["Pid"].ToString().Trim());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"Godown are not Available..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtGodown.Focus();
            return;
        }

        _searchKey = string.Empty;
        TxtGodown.Focus();
    }

    private void BtnCostCenter_Click(object sender, EventArgs e)
    {
        var frmPickList = new FrmAutoPopList("MAX", "CostCenter", Tag.ToString(), _searchKey, "ALL", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtGodown.Text = frmPickList.SelectedList[0]["CCName"].ToString().Trim();
                _godownId = Convert.ToInt32(frmPickList.SelectedList[0]["CCid"].ToString().Trim());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"CostCenter are not Available..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtGodown.Focus();
            return;
        }

        _searchKey = string.Empty;
        TxtGodown.Focus();
    }

    private void TxtCostCenter_Validating(object sender, CancelEventArgs e)
    {
    }

    private void TxtCostCenter_Enter(object sender, EventArgs e)
    {
    }

    private void TxtCostCenter_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnCostCenter.PerformClick();
        }
        if (e.KeyCode == Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void TxtCostCenter_Leave(object sender, EventArgs e)
    {
    }

    private void txtQty_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void TxtMasterGodown_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void TxtMasterCostCenter_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void btnRemarks_Click(object sender, EventArgs e)
    {
        var frmPickList = new FrmAutoPopList("MIN", "NRMASTER", _actionTag, ObjGlobal.SearchText, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            txtRemarks.Text = frmPickList.SelectedList.Count switch
            {
                > 0 => frmPickList.SelectedList[0]["NRDESC"].ToString().Trim(),
                _ => txtRemarks.Text
            };
            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"COULD N'T FIND ANY NARRATION OR REMARKS..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtRemarks.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        txtRemarks.Focus();
    }

    #endregion --------------- EVENT ---------------

    #region --------------- METHOD ---------------

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
        txtRawMaterial.Enabled = bt;
        // txtIssueNo.Enabled = bt;
        txtRate.Enabled = bt;
        btnSave.Enabled = bt;
        btnCancel.Enabled = bt;
    }

    private void Clear()
    {
        txtVno.Text = string.Empty;
        dpDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        mskMiti.Text = ObjGlobal.ReturnNepaliDate(DateTime.Now.ToString("dd/MM/yyyy"));
        ObjGlobal.DGridColorCombo(dgvPurchase);
        //lbl_NoInWordsDetl.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        txtRawMaterial.Text = string.Empty;
        txtQty.Text = string.Empty;
        txtRate.Text = string.Empty;
        dgvPurchase.Rows.Clear();
        btnSave.Text = "&Save";
        if (txtVno.Enabled)
        {
            txtVno.Focus();
        }
        else if (mskMiti.Enabled)
        {
            mskMiti.Focus();
        }
    }

    private void FillIssueNo(string Action, string Voucher_No)
    {
        try
        {
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
        _headerCap.Clear();
        _columnWidths.Clear();
        _headerCap.Add("Name");
        _headerCap.Add("Code");
        _headerCap.Add("Stock Qty");
        _headerCap.Add("Unit");
        _headerCap.Add("Buy Rate");
        _headerCap.Add("Sales Rate");
        _headerCap.Add("Group");

        _columnWidths.Add(200);
        _columnWidths.Add(120);
        _columnWidths.Add(120);
        _columnWidths.Add(80);
        _columnWidths.Add(110);
        _columnWidths.Add(110);
        _columnWidths.Add(120);
        Query =
            "Select PName,PShortName,Convert(Decimal(18,2),Sum(BalanceQty) ) BalanceQty,UnitCode,Convert(Decimal(18,2),PBuyRate ) PBuyRate,Convert(Decimal(18,2),PSalesRate)PSalesRate,GrpName From (Select P.PName,P.PShortName,Case When EntryType='I' Then isnull(Sum(StockQty),0) Else  -isnull(Sum(StockQty),0) End BalanceQty,PU.UnitCode,P.PBuyRate,P.PSalesRate,PG.GrpName  from AMS.Product  as P left outer join AMS.StockDetails as SD on P.PID=SD.Product_Id ";
        Query = Query +
                " left outer join AMS.ProductUnit as PU on  P.PUnit=PU.UID left outer join AMS.ProductGroup as PG on  PG.PGrpID=p.PGrpId Group By P.PName,P.PShortName,PU.UnitCode,P.PBuyRate,P.PSalesRate,PG.GrpName,EntryType ) as aa Group by PName,PShortName,UnitCode,PBuyRate,PSalesRate,GrpName";
        using (var PkLst = new FrmPickList(Query, _headerCap, _columnWidths, "Product List", string.Empty))
        {
            if (PkLst.ShowDialog() == DialogResult.OK)
                if (ClsPickList.PlValue1 != null && ClsPickList.PlValue2 != null &&
                    ClsPickList.PlValue1 != string.Empty && ClsPickList.PlValue2 != string.Empty)
                {
                    txtRawMaterial.Text = ClsPickList.PlValue1;
                    txtRawMaterial.Focus();
                }
        }
    }

    #endregion --------------- METHOD ---------------

    #region --------------- GLOBAL ---------------

    private DataTable _dt = new();
    private readonly ArrayList _headerCap = new();
    private readonly ArrayList _columnWidths = new();
    public string Query = string.Empty;
    private long LedgerId = 0;
    private DataTable _dtvalidate = new();
    private const int RowIndex = 0;
    private const int CurrentColumn = 0;
    private string _searchKey = string.Empty;
    private readonly ClsProduction _objProduction = new();
    private readonly IFinishedGoodReceivedRepository _finished;
    private int _godownId;

    #endregion --------------- GLOBAL ---------------
}