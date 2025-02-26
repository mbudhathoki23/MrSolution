using DevExpress.XtraEditors;
using MrBLL.DataEntry.Common;
using MrBLL.Master.LedgerSetup;
using MrBLL.Master.ProductSetup;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Utility.Config;
using MrDAL.Utility.Server;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using static System.Int32;

namespace MrBLL.Domains.VehicleManagement.Servicing;

public partial class xUCService : XtraUserControl
{
    private string _ActionTag;
    private string _LedgerType;
    private int AltUnitId;

    private bool btnv = false;
    private string Descriptions = string.Empty;
    private DataTable dtBTerm = new("Temp");
    private DataTable dtMUdf = new("Temp");
    private DataTable dtProduct = new();
    private DataTable dtPTerm = new("Temp");
    private DataTable dtPUdf = new("Temp");

    private readonly DataTable dtvalidate = new();

    private ClsEntryForm GetForm;

    private ObjGlobal Gobj = new();
    private long GodownId;
    private readonly int GridId = 0;
    private bool IsLoad;
    private readonly bool IsSales_ProdTerm = false;

    private long ledgerId;

    private readonly ISalesEntry objEntry = new ClsSalesEntry();
    private readonly IMasterSetup ObjMaster = new ClsMasterSetup();
    private string PCategory = string.Empty;
    private long ProductId;
    private string Query = string.Empty;
    private readonly int rowIndex = 0;

    private int UnitId;
    private long VehicleId;

    public xUCService()
    {
        InitializeComponent();

        Load += XUCService_Load;
        KeyPress += XUCService_KeyPress;
        GetForm = new ClsEntryForm(this);
    }

    private void XUCService_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)27)
        {
            if (BtnNew.Enabled is false || BtnEdit.Enabled is false || BtnDelete.Enabled is false)
            {
                if (MessageBox.Show(@"ARE YOU SURE WANT TO CLEAR FORM..??", ObjGlobal.Caption,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) is not DialogResult.Yes) return;
                _ActionTag = string.Empty;
                ControlEnable(true, false);
                ControlClear();
                BtnNew.Focus();
            }
            else
            {
                BtnExit.PerformClick();
            }
        }
        else if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void XUCService_Load(object sender, EventArgs e)
    {
        _ActionTag = string.Empty;
        BackColor = ObjGlobal.FrmBackColor();

        BtnNew.Focus();
    }

    #region ---------- BUTTON ----------

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _ActionTag = "SAVE";
        ControlEnable(false, true);
        ControlClear();
        if (string.IsNullOrEmpty(TxtVno.Text))
            TxtVno.Focus();
        else if (MskMiti.Enabled)
            MskMiti.Focus();
        else
            TxtRefVno.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _ActionTag = "UPDATE";
        ControlEnable(false, true);
        ControlClear();
        TxtVno.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _ActionTag = "DELETE";
        ControlEnable(false, true);
        ControlClear();
        BtnSave.Focus();
    }

    private void BtnReverse_Click(object sender, EventArgs e)
    {
        _ActionTag = "REVERSE";
        ControlClear();
        ControlEnable(false, false);
        TxtVno.Focus();
    }

    private void BtnPrint_Click(object sender, EventArgs e)
    {
        _ActionTag = "PRINT";
        ControlClear();
    }

    private void BtnCopy_Click(object sender, EventArgs e)
    {
        _ActionTag = "COPY";
        ControlEnable(false, true);
        ControlClear();
        TxtVno.Focus();
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        _ActionTag = "SAVE";
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtVno.Text))
            ClearDetails();
        else
            BtnExit.PerformClick();
    }

    #endregion ---------- BUTTON ----------

    #region ---------- METHOD ----------

    private void ControlEnable(bool btn, bool txt)
    {
        BtnNew.Enabled = btn;
        DGrid.ReadOnly = true;
        TxtVno.Enabled = _ActionTag is "DELETE" || _ActionTag is "REVERSE" || txt;
        BtnVno.Enabled = _ActionTag is "DELETE" || _ActionTag is "REVERSE" || btn;
        TxtBTermAmt.Enabled = txt;
        TxtShortName.Enabled = false;
        MskMiti.Enabled = !string.IsNullOrEmpty(_ActionTag);
        TxtBasicAmount.Enabled =
            (!string.IsNullOrEmpty(_ActionTag) && _ActionTag is "SAVE" || _ActionTag is "UPDATE") &&
            ObjGlobal.SalesBasicAmountEnable;
        TxtDueDays.Enabled = txt;
        TxtCustomer.Enabled = BtnCustomer.Enabled = txt;
        TxtProduct.Enabled = BtnProduct.Enabled = txt;
        TxtRemarks.Enabled = _ActionTag is "DELETE" || _ActionTag is "REVERSE" || txt;
        CmbWorkType.Enabled = ActiveControl is not null && !string.IsNullOrEmpty(_ActionTag) ? true : false;
        BtnEdit.Enabled = BtnDelete.Enabled =
            _ActionTag is not "SAVE" && _ActionTag is not "UPDATE" && _ActionTag is not "DELETE";
        BtnSave.Enabled = BtnCancel.Enabled =
            ActiveControl is not null && !string.IsNullOrEmpty(_ActionTag) ? true : false;
    }

    private void LedgerCurrentBalance(long SelectLedgerId)
    {
        //  throw new NotImplementedException();
    }

    private void FillDate()
    {
        MskDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        MskMiti.Text = ObjGlobal.ReturnNepaliDate(DateTime.Now.ToString("dd/MM/yyyy"));
        MskRefDate.Text = ObjGlobal.SysDateType is "M"
            ? ObjGlobal.ReturnNepaliDate(DateTime.Now.ToString("dd/MM/yyyy"))
            : DateTime.Now.ToString("dd/MM/yyyy");
        MskDate.Enabled = false;
        MskMiti.Enabled = false;
    }

    private void ControlClear()
    {
        Text = !string.IsNullOrEmpty(_ActionTag) ? $"JOBCARD [{_ActionTag}]" : "JOBCARD";
        TxtVno.Text = TxtRefVno.Text = TxtVehicle.Text = TxtCustomer.Text = TxtDueDays.Text =
            TxtProduct.Text = TxtShortName.Text = TxtGodown.Text = TxtQty.Text =
                TxtQtyUOM.Text = TxtRate.Text = TxtBasicAmount.Text = TxtRemarks.Text = string.Empty;
        CmbWorkType.SelectedIndex = 0;
        LblTotalQty.Text = ObjGlobal.ReturnDouble("0").ToString(ObjGlobal.SysAmountFormat);
        LblTotalBasicAmt.Text = ObjGlobal.ReturnDouble("0").ToString(ObjGlobal.SysAmountFormat);
        LblTotalAmt.Text = ObjGlobal.ReturnDouble("0").ToString(ObjGlobal.SysAmountFormat);
        LblTotalLocalNetAmt.Text = ObjGlobal.ReturnDouble("0").ToString(ObjGlobal.SysAmountFormat);
        if (string.IsNullOrEmpty(_ActionTag))
            TxtVno.Enabled = MskMiti.Enabled = MskDate.Enabled = TxtRefVno.Enabled = MskRefDate.Enabled =
                TxtVehicle.Enabled = TxtCustomer.Enabled = TxtDueDays.Enabled = TxtProduct.Enabled =
                    TxtShortName.Enabled = TxtGodown.Enabled = TxtQty.Enabled = TxtRate.Enabled =
                        TxtBasicAmount.Enabled = false;

        DGrid.Rows.Clear();

        ClearDetails();
    }

    private void Initialisedatatable()
    {
    }

    private static void ClearDetails()
    {
    }

    private void FillInVoiceData(string Action, string Voucher_No)
    {
        Query = string.Empty;
    }

    private void CashandBankValidation()
    {
    }

    private void AddDataToGridDetails(bool IsUpdate)
    {
        var iRows = 0;
        if (IsUpdate)
        {
            iRows = GridId;
        }
        else
        {
            DGrid.Rows.Add();
            if (DGrid.Rows.Count != 0) iRows = DGrid.Rows.Count - 1;
            DGrid.Rows[iRows].Cells["GTxtSNo"].Value = iRows + 1;
        }

        DGrid.Rows[iRows].Cells["GTxtProductId"].Value = ProductId.ToString();
        DGrid.Rows[iRows].Cells["GTxtProduct"].Value = TxtProduct.Text;
        DGrid.Rows[iRows].Cells["GTxtGodownId"].Value = GodownId.ToString();
        DGrid.Rows[iRows].Cells["GTxtGodown"].Value = TxtGodown.Text;
        DGrid.Rows[iRows].Cells["GTxtAltQty"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtAltUOMId"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtAltUOM"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtQty"].Value = TxtQty.Text != string.Empty
            ? Convert.ToDouble(TxtQty.Text).ToString(ObjGlobal.SysQtyFormat)
            : Convert.ToDouble(0).ToString(ObjGlobal.SysQtyFormat);
        DGrid.Rows[iRows].Cells["GTxtUOMId"].Value = UnitId.ToString();
        DGrid.Rows[iRows].Cells["GTxtUOM"].Value = TxtQtyUOM.Text;
        DGrid.Rows[iRows].Cells["GTxtRate"].Value =
            ObjGlobal.ReturnDouble(TxtRate.Text).ToString(ObjGlobal.SysAmountFormat);
        DGrid.Rows[iRows].Cells["GTxtAmount"].Value =
            ObjGlobal.ReturnDouble(TxtBasicAmount.Text).ToString(ObjGlobal.SysAmountFormat);
        DGrid.Rows[iRows].Cells["GTxtTermAmount"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtNetAmount"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtAltStockQty"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtStockQty"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtNarration"].Value = Descriptions;
        DGrid.Rows[iRows].Cells["GTxtOrderNo"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtOrderSno"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtChallanNo"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtChallanSno"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtConFraction"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtFreeQty"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtStockFreeQty"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtFreeUnitId"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtExtraFreeQty"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtExtraStockQty"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtExraFreeUnitId"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtTaxPriceRate"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtTaxGroupId"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtVatAmount"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtTaxableAmount"].Value = string.Empty;
    }

    private void SetProductInfo(long SelectedProductId)
    {
        string Stdate;

        if (SelectedProductId == 0) return;
        UnitId = AltUnitId = 0;
        dtProduct = ObjMaster.GetMasterProductList(_ActionTag, SelectedProductId);
        if (dtProduct.Rows.Count <= 0) return;
        long.TryParse(dtProduct.Rows[0]["PId"].ToString(), out var _SelectedId);
        SelectedProductId = _SelectedId;
        PCategory = dtProduct.Rows[0]["PType"].ToString();

        TxtProduct.Text = dtProduct.Rows[0]["PName"].ToString();
        TxtShortName.Text = dtProduct.Rows[0]["PShortName"].ToString();

        TryParse(dtProduct.Rows[0]["PUnit"].ToString(), out var _UnitId);
        UnitId = _UnitId > 0 ? Convert.ToInt32(dtProduct.Rows[0]["PUnit"].ToString()) : _UnitId;

        TryParse(dtProduct.Rows[0]["PAltUnit"].ToString(), out var _AltUnitId);
        AltUnitId = _AltUnitId;
        TxtRate.Text =
            dtProduct.Rows[0]["PSalesRate"] != null && dtProduct.Rows[0]["PSalesRate"].ToString() != string.Empty
                ? Convert.ToDouble(ObjGlobal.ReturnDecimal(dtProduct.Rows[0]["PSalesRate"].ToString()))
                    .ToString(ObjGlobal.SysQtyFormat)
                : 0.ToString();
        Stdate = MskDate.Text;
    }

    #endregion ---------- METHOD ----------

    #region ---------- EVENTS ----------

    private void TxtVno_Leave(object sender, EventArgs e)
    {
        if (ActiveControl != null && !string.IsNullOrEmpty(_ActionTag) &&
            string.IsNullOrEmpty(TxtVno.Text.Trim()) && TxtVno.Focused)
        {
            MessageBox.Show(@"INVOICE NUMBER IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtVno.Focus();
            return;
        }

        FillDate();
    }

    private void TxtVno_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnVno_Click(sender, e);
        }
        else if (!string.IsNullOrEmpty(_ActionTag) && TxtVno.ReadOnly)
        {
            ObjGlobal.SearchText = e.KeyCode.ToString();
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                ObjGlobal.SearchText, TxtVno, BtnVno);
        }
    }

    private void BtnVno_Click(object sender, EventArgs e)
    {
        using var frmPickList =
            new FrmAutoPopList("MAX", @"SO", ObjGlobal.SearchText, _ActionTag, "NORMAL", "TRANSACTION");
        if (FrmAutoPopList.GetListTable.Rows.Count <= 0) return;
        frmPickList.ShowDialog();
        if (frmPickList.SelectedList.Count <= 0) return;
        if (string.IsNullOrEmpty(_ActionTag) || _ActionTag.ToUpper() == "SAVE") return;
        TxtVno.Text = frmPickList.SelectedList[0]["SO_Invoice"].ToString().Trim();
        FillInVoiceData(_ActionTag, TxtVno.Text);
        IsLoad = true;
    }

    private void TxtVno_Validating(object sender, CancelEventArgs e)
    {
        if (ActiveControl != null && !string.IsNullOrWhiteSpace(TxtVno.Text.Trim()) && TxtVno.Focused &&
            _ActionTag is "SAVE")
        {
            var dtVno = objEntry.CheckVoucherNoExitsOrNot("AMS.SB_Master", "SB_Invoice", TxtVno.Text.Trim());
            if (dtVno != null && dtVno.Rows.Count > 0)
            {
                MessageBox.Show(@"INVOICE NUMBER ALREADY EXITS..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtVno.Focus();
            }
        }
    }

    private void MskMiti_Enter(object sender, EventArgs e)
    {
        BeginInvoke((MethodInvoker)delegate { MskMiti.Select(0, 0); });
        SendKeys.SendWait("{HOME}");
    }

    private void MskMiti_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskMiti, 'L');
        if (MskMiti.Text != "  /  /")
            MskDate.Text =
                GetConnection.GetQueryData("Select AD_Date from AMS.DateMiti where BS_DateDMY='" + MskMiti.Text +
                                           "'");
    }

    private void MskMiti_Validating(object sender, CancelEventArgs e)
    {
        if (MskMiti.Text.Trim() != "/  /" && MskMiti.Enabled)
        {
            if (ObjGlobal.ValidDate(MskMiti.Text, "M") && MskMiti.Enabled)
            {
                if (ObjGlobal.ValidDateRange(Convert.ToDateTime(ObjGlobal.ReturnEnglishDate(MskMiti.Text)))) return;
                e.Cancel = true;
                MessageBox.Show(
                    @"DATE MUST BE BETWEEN " +
                    ObjGlobal.ReturnNepaliDate(ObjGlobal.CfStartAdDate.ToShortDateString()) + " AND " +
                    ObjGlobal.ReturnNepaliDate(ObjGlobal.CfEndAdDate.ToShortDateString()) + " ", ObjGlobal.Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MskMiti.Focus();
            }
            else
            {
                e.Cancel = true;
                MessageBox.Show(@"PLZ. ENTER VALID DATE ..!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                MskMiti.Focus();
            }
        }
        else if (MskMiti.Text.Trim() is "/  /" && MskMiti.Enabled)
        {
            e.Cancel = true;
            MessageBox.Show(@"INVOICE MITI CANNOT BE LEFT BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            MskMiti.Focus();
        }
    }

    private void MskDate_Enter(object sender, EventArgs e)
    {
        MskDate.SelectAll();
        BeginInvoke((MethodInvoker)delegate { MskDate.Select(0, 0); });
        SendKeys.SendWait("{HOME}");
    }

    private void MskDate_Leave(object sender, EventArgs e)
    {
        if (MskDate.Text != "  /  /" && MskDate.Enabled)
            MskMiti.Text = GetConnection.GetQueryData("Select BS_DateDMY from AMS.DateMiti where AD_Date='" +
                                                      Convert.ToDateTime(MskDate.Text).ToString("yyyy-MM-dd") +
                                                      "'");
    }

    private void MskDate_Validating(object sender, CancelEventArgs e)
    {
        if (MskDate.Text.Trim() != "/  /" && MskDate.Enabled)
        {
            if (ObjGlobal.ValidDate(MskDate.Text, "D") && MskDate.Enabled)
            {
                if (ObjGlobal.ValidDateRange(Convert.ToDateTime(MskDate.Text)) || !MskDate.Enabled) return;
                e.Cancel = true;
                MessageBox.Show(
                    @"DATE MUST BE BETWEEN " + ObjGlobal.CfStartAdDate + " AND " + ObjGlobal.CfEndAdDate + " ",
                    ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MskDate.Focus();
            }
            else
            {
                e.Cancel = true;
                MessageBox.Show(@"PLZ. ENTER VALID DATE..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                MskDate.Focus();
            }
        }
        else if (MskDate.Text == "/  /" && MskDate.Enabled)
        {
            e.Cancel = true;
            MessageBox.Show(@"INVOICE DATE CANNOT BE LEFT BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            MskDate.Focus();
        }
    }

    private void TxtRefVno_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtRefVno.Text)) return;
        if (MskDate.Text != "  /  /")
            MskRefDate.Text = GetConnection.GetQueryData("Select BS_DateDMY from AMS.DateMiti where AD_Date='" +
                                                         Convert.ToDateTime(MskDate.Text).ToString("yyyy-MM-dd") +
                                                         "'");
    }

    private void TxtVehicle_Leave(object sender, EventArgs e)
    {
        if (ActiveControl != null && !string.IsNullOrEmpty(_ActionTag) &&
            string.IsNullOrEmpty(TxtVehicle.Text.Trim()) && TxtVehicle.Focused)
        {
            MessageBox.Show(@"VEHICLE  IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtVehicle.Focus();
        }
    }

    private void TxtCustomer_Leave(object sender, EventArgs e)
    {
        if (ActiveControl != null && string.IsNullOrEmpty(_ActionTag) &&
            !string.IsNullOrEmpty(TxtCustomer.Text.Trim()) && TxtCustomer.Focused)
        {
            MessageBox.Show(@"CUSTOMER IS BLANK ..!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtCustomer.Focus();
        }
    }

    private void TxtCustomer_Validating(object sender, CancelEventArgs e)
    {
        if (ActiveControl is not null && string.IsNullOrEmpty(TxtCustomer.Text.Trim()) && TxtCustomer.Focused &&
            _ActionTag is "SAVE")
        {
            using var dtVno =
                objEntry.CheckVoucherNoExitsOrNot("AMS.GeneralLedger", "GLName", TxtCustomer.Text.Trim());
            if (dtVno != null && dtVno.Rows.Count > 0)
            {
                MessageBox.Show(@"CUSTOMER ALREADY EXITS..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtVno.Focus();
            }
        }
    }

    private void BtnCustomer_Click(object sender, EventArgs e)
    {
        using var frmPickList = new FrmAutoPopList("MAX", "GENERALLEDGER", ObjGlobal.SearchText, _ActionTag,
            "CUSTOMER", "MASTER");
        if (FrmAutoPopList.GetListTable != null && FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtCustomer.Text = frmPickList.SelectedList[0]["Particular"].ToString().Trim();
                ledgerId = ObjGlobal.ReturnLong(frmPickList.SelectedList[0]["LedgerId"].ToString().Trim());
                LedgerCurrentBalance(ledgerId);
                _LedgerType = frmPickList.SelectedList[0]["GLType"].ToString().Trim();
                if (_LedgerType is "Cash" || _LedgerType is "Bank") CashandBankValidation();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"CUSTOMER LEDGER NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtCustomer.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtCustomer.Focus();
    }

    private void TxtVno_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void TxtRefVno_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void MskRefDate_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void TxtVehicle_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void TxtCustomer_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void CmbWorkType_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void TxtProduct_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void TxtShortName_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void TxtGodown_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void TxtMRPRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void TxtSalesRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void TxtCustomer_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnCustomer_Click(sender, e);
        }
        else if (e.Control is true && e.KeyCode is Keys.N)
        {
            using var frm = new FrmGeneralLedger("Customer", true);
            frm.ShowDialog();
            TxtCustomer.Text = frm.LedgerDesc;
            ledgerId = frm.LedgerId;
            frm.Dispose();
            TxtCustomer.Focus();
        }
        else if (e.KeyCode is Keys.Enter || e.KeyCode is Keys.Tab)
        {
            if (string.IsNullOrEmpty(TxtCustomer.Text.Trim())) BtnCustomer_Click(sender, e);
        }
        else if (e.KeyCode is Keys.F2 || e.KeyCode is Keys.Tab)
        {
            TxtCustomer.Focus();
            using var frm = new FrmCustomerList("CustomerList", "Customer List", TxtCustomer.Text,
                "('Both','Customer')");
            frm.ShowDialog();
            if (frm.SelectList.Count > 0)
            {
                TxtCustomer.Text = frm.SelectList[0]["GLName"].ToString().Trim();
                TxtCustomer.Focus();
            }

            frm.Dispose();
        }
        else if (e.KeyCode is Keys.F6)
        {
            if (_LedgerType is "Cash" || _LedgerType is "Bank") CashandBankValidation();
        }
        else
        {
            ObjGlobal.SearchText = e.KeyCode.ToString();
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                ObjGlobal.SearchText, TxtCustomer, BtnCustomer);
        }
    }

    private void TxtCustomer_Validated(object sender, EventArgs e)
    {
        if (ledgerId is 0 && !string.IsNullOrEmpty(TxtCustomer.Text.Trim()) && !string.IsNullOrEmpty(_ActionTag))
        {
        }

        if (string.IsNullOrEmpty(TxtCustomer.Text.Trim()) || string.IsNullOrEmpty(_ActionTag)) return;
        if (dtvalidate.Rows.Count <= 0) return;
        ledgerId = Convert.ToInt64(dtvalidate.Rows[0]["GlId"].ToString());
        TxtCustomer.Text = ObjGlobal.StockShortNameWise
            ? dtvalidate.Rows[0][nameof(ledgerId)].ToString()
            : dtvalidate.Rows[0]["GlName"].ToString();

        if (Convert.ToInt32(ObjGlobal.ReturnDecimal(TxtDueDays.Text)) <= 0)
        {
            if (dtvalidate.Rows[0]["CrDays"].ToString() != string.Empty)
                TxtDueDays.Text = dtvalidate.Rows[0]["CrDays"].ToString();
            TxtDueDays_Validated(sender, e);
        }

        if (Convert.ToBoolean(dtvalidate.Rows[0]["Status"].ToString()) is false)
        {
            MessageBox.Show(@"THIS PARTY HAS BEEN LOCKED, SORRY YOU CANNOT BE PROCEED THE TRANSACTION.",
                ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            TxtCustomer.Focus();
            return;
        }

        if (dtvalidate.Rows[0]["GlType"].ToString() is "Bank" || dtvalidate.Rows[0]["GlType"].ToString() is "Cash")
            CashandBankValidation();
    }

    private void TxtDueDays_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Back || e.KeyChar is '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !TryParse(e.KeyChar.ToString(), out var isNumber);

        if (e.KeyChar is (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void TxtDueDays_Validated(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ObjGlobal.ReturnDecimal(TxtDueDays.Text)) > 0)
            MskDueDays.Text = ObjGlobal.SysDateType is "D"
                ? Convert.ToDateTime(MskDate.Text)
                    .AddDays(Convert.ToInt32(ObjGlobal.ReturnDecimal(TxtDueDays.Text))).ToShortDateString()
                : ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(MskDate.Text)
                    .AddDays(Convert.ToInt32(ObjGlobal.ReturnDecimal(TxtDueDays.Text))).ToShortDateString());
    }

    private void TxtProduct_Leave(object sender, EventArgs e)
    {
        if (ActiveControl == null || !string.IsNullOrEmpty(TxtProduct.Text.Trim()) || TxtProduct.Focused != true ||
            string.IsNullOrEmpty(_ActionTag)) return;
        MessageBox.Show(@"PRODUCT DESCRIPTION CAN'T BE LEFT BLANK!!", ObjGlobal.Caption, MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
        TxtProduct.Focus();
    }

    private void TxtProduct_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnProduct_Click(sender, e);
        }
        else if (e.Control is true && e.KeyCode is Keys.N)
        {
            using var frm = new FrmProduct(true);
            frm.ShowDialog();
            TxtProduct.Text = frm.ProductDesc;
            ProductId = frm.ProductId;
            frm.Dispose();
            TxtProduct.Focus();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (DGrid.Rows.Count is 0)
                if (string.IsNullOrEmpty(TxtProduct.Text.Trim()))
                    BtnProduct_Click(sender, e);

            if (DGrid.Rows.Count > 0 && string.IsNullOrEmpty(TxtProduct.Text.Trim()))
                for (var i = 0; i < DGrid.Rows.Count; i++)
                {
                    if (DGrid.Rows[i].Cells["GTxtProductId"].Value == null) continue;
                    if (string.IsNullOrEmpty(DGrid.Rows[i].Cells["GTxtProductId"].Value.ToString())) continue;
                    BtnTerm_Click(sender, e);
                    break;
                }
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtProduct, BtnProduct);
        }
    }

    private void BtnProduct_Click(object sender, EventArgs e)
    {
        using var frmPickList = new FrmAutoPopList("MAX", "PRODUCT", _ActionTag, ObjGlobal.SearchText, "ALL", "MASTER", MskDate.Text.Trim());
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtProduct.Text = frmPickList.SelectedList[0]["PName"].ToString().Trim();
                long.TryParse(frmPickList.SelectedList[0]["PID"].ToString().Trim(), out var _ProductId);
                ProductId = _ProductId;
                SetProductInfo(ProductId);
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"PRODUCT NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtProduct.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtProduct.Focus();
    }

    private void BtnTerm_Click(object sender, EventArgs e)
    {
    }

    private void TxtGodown_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnGodown_.PerformClick();
        }
        else
        {
            if (e.KeyCode is not Keys.Enter)
            {
                ObjGlobal.SearchText = e.KeyCode.ToString();
                ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                    ObjGlobal.SearchText, TxtGodown, BtnGodown_);
            }
            else
            {
                if (ObjGlobal.SalesGodownMandatory is true && string.IsNullOrEmpty(TxtGodown.Text))
                {
                    MessageBox.Show(@"GODOWN DOES NOT EXITS ..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    TxtGodown.Focus();
                }
            }
        }
    }

    private void TxtGodown_Validating(object sender, CancelEventArgs e)
    {
        if (TxtGodown.Text.Trim() != string.Empty)
        {
            dtvalidate.Reset();
            if (dtvalidate.Rows.Count > 0)
            {
                GodownId = Convert.ToInt32(dtvalidate.Rows[0]["GId"].ToString());
            }
            else
            {
                MessageBox.Show(@"GODOWN DOES NOT EXITS ..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtGodown.Focus();
            }
        }
        else if (string.IsNullOrEmpty(TxtGodown.Text))
        {
            if (ObjGlobal.SalesGodownMandatory is not true || TxtGodown.Focused is not true) return;
            MessageBox.Show(@"GODOWN IS BLANK ..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtGodown.Focus();
        }
    }

    private void TxtQty_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);

        if (e.KeyChar is (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void TxtQty_Validating(object sender, CancelEventArgs e)
    {
        double TermCalAmt = 0;
        if (!(Convert.ToDouble(ObjGlobal.ReturnDecimal(TxtQty.Text)) > 0)) return;
        Query =
            " SELECT PShortName,PName,PAltConv,PQtyConv,AU.UnitCode AUnit,Qu.UnitCode QUnit FROM AMS.Product P ";
        Query +=
            " Left Outer Join AMS.ProductUnit AU On P.PAltUnit=AU.UID Left Outer Join AMS.ProductUnit QU On P.PUnit=QU.UID Where (P.Status<>0 or P.Status is Null) ";
        if (ObjGlobal.StockShortNameWise)
            Query = Query + " and PAlias = '" + TxtProduct.Text + "'";
        else
            Query = Query + " and PName = '" + TxtProduct.Text + "'";

        dtProduct.Reset();
        dtProduct = GetConnection.SelectDataTableQuery(Query);
        if (dtProduct.Rows.Count <= 0) return;
        TxtBasicAmount.Text =
            (Convert.ToDouble(ObjGlobal.ReturnDecimal(TxtRate.Text)) *
             Convert.ToDouble(ObjGlobal.ReturnDecimal(TxtQty.Text))).ToString(ObjGlobal.SysAmountFormat);
        TermCalAmt = Convert.ToDouble(ObjGlobal.ReturnDecimal(TxtBasicAmount.Text));
        if (IsSales_ProdTerm)
        {
        }

        TxtQty.Text = Convert.ToDouble(TxtQty.Text).ToString(ObjGlobal.SysQtyFormat);
        TxtBasicAmount.Text =
            (Convert.ToDouble(ObjGlobal.ReturnDecimal(TxtQty.Text)) *
             Convert.ToDouble(ObjGlobal.ReturnDecimal(TxtRate.Text))).ToString(ObjGlobal.SysAmountFormat);
    }

    private void TxtRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is not (char)Keys.Enter)
        {
        }
        else
        {
            TxtRate.Text = Convert.ToDouble(ObjGlobal.ReturnDecimal(TxtRate.Text))
                .ToString(ObjGlobal.SysAmountFormat);
        }

        if (e.KeyChar is (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);

        if (e.KeyChar is (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void TxtRate_Leave(object sender, EventArgs e)
    {
        TxtRate.Text = Convert.ToDouble(ObjGlobal.ReturnDecimal(TxtRate.Text)).ToString(ObjGlobal.SysAmountFormat);
    }

    private void TxtRate_TextChanged(object sender, EventArgs e)
    {
    }

    private void TxtBasicAmount_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Tab)
        {
            TxtBasicAmount.Text = Convert.ToDouble(ObjGlobal.ReturnDecimal(TxtBasicAmount.Text))
                .ToString(ObjGlobal.SysAmountFormat);
            if (ObjGlobal.SalesChangeRate is true)
                if (Convert.ToDouble(ObjGlobal.ReturnDecimal(TxtBasicAmount.Text)) > 0 &&
                    Convert.ToDouble(ObjGlobal.ReturnDecimal(TxtQty.Text)) > 0)
                    TxtRate.Text = Convert
                        .ToDouble(Convert.ToDouble(ObjGlobal.ReturnDecimal(TxtBasicAmount.Text)) /
                                  Convert.ToDouble(ObjGlobal.ReturnDecimal(TxtQty.Text)))
                        .ToString(ObjGlobal.SysAmountFormat);

            TxtNetAmount_KeyPress(sender, e);
        }
        else if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains("."))
        {
            return;
        }

        if (e.KeyChar is (char)Keys.Enter) SendKeys.Send("{TAB}");

        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void TxtNetAmount_KeyPress(object sender, KeyPressEventArgs e)
    {
        switch (e.KeyChar)
        {
            case (char)Keys.Enter:
            case (char)Keys.Tab:
            {
                if (TxtProduct.Text.Trim() != string.Empty &&
                    Convert.ToDecimal(ObjGlobal.ReturnDecimal(TxtQty.Text)) != 0)
                {
                    if (ObjGlobal.SalesDescriptionsEnable)
                    {
                        using var AD = new FrmAddDescriptions();
                        if (Descriptions != string.Empty)
                        {
                            AD.Descriptions = Descriptions;
                        }
                        else
                        {
                            if (DGrid.Rows.Count >= 1)
                            {
                                if (DGrid.Rows[GridId].Selected is true)
                                {
                                    if (DGrid.SelectedRows[GridId].Cells["GTxtNarration"].Value != null &&
                                        DGrid.SelectedRows[GridId].Cells["GTxtNarration"].Value.ToString() !=
                                        string.Empty) //Cells[17]
                                        AD.Descriptions = DGrid.SelectedRows[GridId].Cells["GTxtNarration"].Value
                                            .ToString();
                                }
                                else
                                {
                                    AD.Descriptions = ObjGlobal.StockShortNameWise is true
                                        ? TxtShortName.Text
                                        : TxtProduct.Text;
                                }
                            }
                            else
                            {
                                AD.Descriptions = ObjGlobal.StockShortNameWise is true
                                    ? TxtShortName.Text
                                    : TxtProduct.Text;
                            }
                        }

                        if (AD.ShowDialog() == DialogResult.OK) Descriptions = AD.Descriptions;
                    }

                    if (DGrid.Rows.Count >= 1)
                        AddDataToGridDetails(DGrid.Rows[rowIndex].Selected);
                    else
                        AddDataToGridDetails(false);
                    TxtProduct.Focus();
                }

                break;
            }
            case (char)Keys.Back:
            case '.' when !((TextBox)sender).Text.Contains("."):
                return;
        }

        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void TxtRemarks_Leave(object sender, EventArgs e)
    {
        if (ActiveControl == null || !string.IsNullOrEmpty(_ActionTag) ||
            string.IsNullOrEmpty(TxtRemarks.Text.Trim()) || !TxtRemarks.Focused) return;
        MessageBox.Show(@"REMARKS IS BLANK ..!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        TxtRemarks.Focus();
    }

    private void BtnGodown__Click(object sender, EventArgs e)
    {
        using var frmPickList =
            new FrmAutoPopList("MAX", "GODOWN", ObjGlobal.SearchText, _ActionTag, "ALL", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtGodown.Text = frmPickList.SelectedList[0]["PName"].ToString().Trim();
                GodownId = ObjGlobal.ReturnInt(frmPickList.SelectedList[0]["Pid"].ToString().Trim());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"GODOWN NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtProduct.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtGodown.Focus();
    }

    private void TxtStockQty_TextChanged(object sender, EventArgs e)
    {
    }

    private void BtnVehicle_Click(object sender, EventArgs e)
    {
        using var frmPickList = new FrmAutoPopList("MAX", "PRODUCT", "MASTER", ObjGlobal.SearchText, _ActionTag,
            "ALL", MskDate.Text.Trim());
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtVehicle.Text = frmPickList.SelectedList[0]["PName"].ToString().Trim();
                VehicleId = ObjGlobal.ReturnLong(frmPickList.SelectedList[0]["PID"].ToString().Trim());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"PRODUCT NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtVehicle.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtVehicle.Focus();
    }

    private void TxtVehicle_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnVehicle_Click(sender, e);
        }
        else if (e.Control is true && e.KeyCode is Keys.N)
        {
            using var frm = new FrmProduct(true);
            frm.ShowDialog();
            TxtProduct.Text = frm.ProductDesc;
            ProductId = frm.ProductId;
            frm.Dispose();
            TxtProduct.Focus();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtVehicle.Text))
            {
                MessageBox.Show(@"VEHICLE DOES NOT EXITS ..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtVehicle.Focus();
            }
        }
        else
        {
            ObjGlobal.SearchText = e.KeyCode.ToString();
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                ObjGlobal.SearchText, TxtVehicle, BtnVehicle);
        }
    }

    #endregion ---------- EVENTS ----------
}