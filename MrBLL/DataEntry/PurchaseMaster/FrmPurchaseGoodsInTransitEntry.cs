using DevExpress.XtraEditors;
using MrBLL.DataEntry.Common;
using MrBLL.Master.ProductSetup;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.GridControl;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.DataEntry.Design;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace MrBLL.DataEntry.PurchaseMaster;

public partial class FrmPurchaseGoodsInTransitEntry : MrForm
{
    #region --------------- PURCHASE INVOICE ENTRY ---------------

    public FrmPurchaseGoodsInTransitEntry(bool zoom, string txtZoomVno, bool provisionVoucher = false,
        string invoiceType = "NORMAL")
    {
        InitializeComponent();
        _dtPartyInfo = _setup.GetPartyInfo();
        _dtBillTerm = _setup.GetBillingTerm();
        _dtProductTerm = _setup.GetBillingTerm();
        _isProvision = provisionVoucher;
        _txtZoomVno = txtZoomVno;
        _isZoom = zoom;
        _invoiceType = invoiceType;
        DesignGridColumnsAsync();
        EnableControl();
        ClearControl();
    }

    private void FrmPurchaseGoodsInTransitEntry_Load(object sender, EventArgs e)
    {
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }

    private void FrmPurchaseGoodsInTransitEntry_KeyPress(object sender, KeyPressEventArgs e)
    {
        _getKeys = e;
        if (e.KeyChar is not (char)Keys.Escape)
        {
            return;
        }

        if (BtnNew.Enabled)
        {
            var result = CustomMessageBox.ExitActiveForm();
            if (result == DialogResult.Yes)
            {
                Close();
            }
        }
        else
        {
            if (TxtProduct.Enabled)
            {
                ClearProductDetails();
                EnableGridControl();
                DGrid.Focus();
                SendToBack();
            }
            else
            {
                _actionTag = string.Empty;
                EnableControl();
                ClearControl();
                BtnNew.Focus();
            }
        }
    }

    private void FrmPurchaseGoodsInTransitEntry_Shown(object sender, EventArgs e)
    {
        if (BtnNew.Enabled)
        {
            BtnNew.Focus();
        }
        else
        {
            if (TxtVno.Enabled)
            {
                TxtVno.Focus();
            }
            else
            {
                MskDate.Focus();
            }
        }
    }

    private void GlobalEnter_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    #endregion --------------- PURCHASE INVOICE ENTRY ---------------

    // METHOD FOR THIS FORM

    #region --------------- METHOD FOR THIS FORM ---------------

    internal void ReturnVoucherNo()
    {
        var dt = _setup.IsExitsCheckDocumentNumbering("PB");
        if (dt?.Rows.Count is 1)
        {
            _numberScheme = dt.Rows[0]["DocDesc"].ToString();
            TxtVno.Text = TxtVno.GetCurrentVoucherNo("PB", _numberScheme);
        }
        else if (dt != null && dt.Rows.Count > 1)
        {
            using var wnd = new FrmNumberingScheme("PB");
            if (wnd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (string.IsNullOrEmpty(wnd.VNo))
            {
                return;
            }

            _numberScheme = wnd.Description;
            TxtVno.Text = wnd.VNo;
        }
    }

    internal void ClearControl()
    {
        Text = !string.IsNullOrEmpty(_actionTag) ? $"{Text} [{_actionTag}]" : Text;
        TxtVno.ReadOnly = !string.IsNullOrEmpty(_actionTag) && _actionTag != "SAVE";
        TxtVno.Clear();
        if (!_isZoom)
        {
            if (_actionTag.Equals("SAVE"))
            {
                TxtVno.GetCurrentVoucherNo("PB", _numberScheme);
            }

            if (BtnNew.Enabled)
            {
                MskDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                MskMiti.Text = DateTime.Now.GetNepaliDate();
                MskRefDate.Text = ObjGlobal.SysDateType == "M"
                    ? MskRefDate.GetNepaliDate(MskDate.Text)
                    : MskDate.Text;
            }

            _ledgerId = _agentId = _sub_ledgerId = _departmentId = _departmentId = _currencyId = 0;
            TxtChallan.Clear();
            TxtOrder.Clear();
            TxtRefVno.Clear();
            TxtDueDays.Clear();
            TxtVendor.Clear();
            TxtDepartment.Clear();
            TxtAgent.Clear();
            TxtSubledger.Clear();
            TxtCurrencyRate.Clear();
            TxtVendor.Clear();
            CmbInvType.SelectedIndex = 2;
            CmbBillIn.SelectedIndex = 0;
            TxtCurrency.Text = ObjGlobal.SysCurrency;
            TxtCurrencyRate.Text = 1.GetDecimalString(true);
            PAttachment1.Image = null;
            PAttachment2.Image = null;
            PAttachment3.Image = null;
            PAttachment4.Image = null;
            PAttachment5.Image = null;
            _dtProductTerm.Clear();
            _dtPartyInfo.Clear();
            _dtBillTerm.Clear();
            TxtRemarks.Clear();
            LblTotalAltQty.IsClear();
            LblTotalQty.IsClear();
            LblTotalBasicAmount.IsClear();
            LblTotalNetAmount.IsClear();
            LblTotalLocalNetAmount.IsClear();
            TxtTermAmount.Clear();
            if (_dtProductTerm.RowsCount() > 0)
            {
                _dtProductTerm.Rows.Clear();
            }

            if (_dtBillTerm.RowsCount() > 0)
            {
                _dtBillTerm.Rows.Clear();
            }

            DGrid.Rows.Clear();
            ClearProductDetails();
        }

        DGrid.ClearSelection();
    }

    internal void ClearProductDetails()
    {
        if (DGrid.RowCount is 0)
        {
            DGrid.Rows.Add();
            GetSerialNo();
        }

        _isRowUpdate = false;
        _productId = _altUnitId = _godownId = _unitId = 0;
        _conQty = 0;
        TxtProduct.Clear();
        TxtShortName.Clear();
        TxtGodown.Clear();
        TxtAltQty.Enabled = false;
        TxtAltQty.Clear();
        _altUnitId = 0;
        TxtAltUnit.Clear();
        TxtQty.Clear();
        _unitId = 0;
        TxtUnit.Clear();
        TxtRate.Clear();
        TxtBasicAmount.Clear();
        TxtTermAmount.Clear();
        TxtNetAmount.Clear();
        VoucherTotalCalculation();
        AdjustControlsInDataGrid();
    }

    internal void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = BtnReverse.Enabled = !isEnable;
        TxtVno.Enabled = BtnVno.Enabled = isEnable || _tagStrings.Contains(_actionTag);
        MskDate.Enabled = MskMiti.Enabled = isEnable;
        TxtRefVno.Enabled = MskRefDate.Enabled = isEnable;
        TxtChallan.Enabled = BtnChallanNo.Enabled = isEnable && ObjGlobal.PurchaseChallanEnable;
        TxtOrder.Enabled = BtnOrderNo.Enabled = isEnable && ObjGlobal.PurchaseOrderEnable;
        CmbBillIn.Enabled = CmbInvType.Enabled = isEnable;
        TxtVendor.Enabled = BtnVendor.Enabled = isEnable;
        MskDueDays.Enabled = false;
        TxtDueDays.Enabled = isEnable;
        TxtCurrency.Enabled = BtnCurrency.Enabled = TxtCurrencyRate.Enabled = isEnable && ObjGlobal.PurchaseCurrencyEnable;
        TxtSubledger.Enabled = BtnSubledger.Enabled = isEnable && ObjGlobal.PurchaseSubLedgerEnable;
        TxtDepartment.Enabled = BtnDepartment.Enabled = isEnable && ObjGlobal.PurchaseDepartmentEnable;
        TxtAgent.Enabled = BtnAgent.Enabled = isEnable && ObjGlobal.PurchaseAgentEnable;
        TxtBillTermAmount.Enabled = BtnBillingTerm.Enabled = isEnable && _dtBillTerm.Columns.Count > 0;
        TabLedgerOpening.Enabled = isEnable;
        TxtRemarks.Enabled = isEnable && ObjGlobal.PurchaseRemarksEnable;
        BtnSave.Enabled = BtnCancel.Enabled = isEnable || _tagStrings.Contains(_actionTag);
        EnableGridControl();
    }

    internal void EnableGridControl(bool isEnable = false)
    {
        TxtSno.Enabled = false;
        TxtSno.Visible = isEnable;

        TxtShortName.Enabled = TxtShortName.Visible = isEnable && ObjGlobal.StockShortNameWise;

        TxtProduct.Enabled = TxtProduct.Visible = isEnable;

        TxtGodown.Enabled = TxtGodown.Visible = isEnable && ObjGlobal.StockGodownEnable;

        TxtAltQty.Enabled = false;
        TxtAltQty.Visible = isEnable;

        TxtAltUnit.Enabled = false;
        TxtAltUnit.Visible = isEnable;

        TxtQty.Enabled = TxtQty.Visible = isEnable;
        TxtUnit.Enabled = false;
        TxtUnit.Visible = isEnable;

        TxtRate.Enabled = TxtRate.Visible = isEnable;

        TxtBasicAmount.Enabled = isEnable && ObjGlobal.PurchaseBasicAmountEnable;
        TxtBasicAmount.Visible = isEnable;

        TxtTermAmount.Enabled = TxtTermAmount.Visible = isEnable && _dtProductTerm.Columns.Count > 0;
        TxtNetAmount.Enabled = false;
        TxtNetAmount.Visible = isEnable && _dtProductTerm.Columns.Count > 0;
        DGrid.ScrollBars = isEnable ? ScrollBars.None : ScrollBars.Both;
    }

    internal void SetLedgerInfo(long ledgerId)
    {
        var dtLedger = _setup.GetLedgerBalance(ledgerId, MskDate);
        if (dtLedger.Rows.Count <= 0)
        {
            return;
        }

        LblPanNo.Text = dtLedger.Rows[0]["PanNo"].ToString();
        LblBalance.Text = dtLedger.Rows[0]["Balance"].ToString();
        LblBalance.Text = dtLedger.Rows[0]["CrLimit"].GetDecimalString();
        LblCreditDays.Text = dtLedger.Rows[0]["CrDays"].GetDecimalString();
        TxtDueDays.Text = LblCreditDays.GetDecimalString();
        MskDueDays.Text = MskDate.GetDateTime().AddDays(TxtDueDays.GetDouble()).GetDateString();
    }

    internal void OpenProductList()
    {
        (TxtProduct.Text, _productId) = GetMasterList.GetProduct(_actionTag, MskDate.Text);
        SetProductInfo(_productId);
    }

    internal void OpenGodownList()
    {
    }

    internal bool IsValidInformation()
    {
        if (_actionTag != "SAVE")
        {
            if (CustomMessageBox.VoucherNoAction(_actionTag, TxtVno.Text) == DialogResult.No)
            {
                return false;
            }
        }

        return true;
    }

    internal int SaveCashAndBankDetails()
    {
        try
        {
            _financeEntry.CbMaster.VoucherMode = !_isProvision ? "Contra" : "PROV";
            _financeEntry.CbMaster.Voucher_No = TxtVno.Text;
            _financeEntry.CbMaster.Voucher_Date = MskDate.Text.GetDateTime();
            _financeEntry.CbMaster.Voucher_Miti = MskMiti.Text;
            _financeEntry.CbMaster.Voucher_Time = DateTime.Now;
            _financeEntry.CbMaster.Ref_VNo = TxtRefVno.Text;
            _financeEntry.CbMaster.Ref_VDate = !string.IsNullOrEmpty(TxtRefVno.Text.Trim())
                ? DateTime.Parse(MskRefDate.Text)
                : DateTime.Now;
            _financeEntry.CbMaster.VoucherType = "Contra";
            _financeEntry.CbMaster.Ledger_Id = CmbBillIn.Text.Equals("Bank")
                ? ObjGlobal.FinanceBankLedgerId.GetLong()
                : ObjGlobal.FinanceCashLedgerId.GetLong();
            _financeEntry.CbMaster.CheqNo = string.Empty;
            _financeEntry.CbMaster.CheqDate = DateTime.Now;
            _financeEntry.CbMaster.CheqMiti = string.Empty;
            _financeEntry.CbMaster.Currency_Id = _currencyId > 0 ? _currencyId : ObjGlobal.SysCurrencyId;
            _financeEntry.CbMaster.Currency_Rate =
                TxtCurrencyRate.GetDecimal() > 0 ? TxtCurrency.Text.GetDecimal() : 1;
            _financeEntry.CbMaster.Cls1 = _departmentId;
            _financeEntry.CbMaster.Cls2 = 0;
            _financeEntry.CbMaster.Cls3 = 0;
            _financeEntry.CbMaster.Cls4 = 0;
            _financeEntry.CbMaster.Remarks = TxtRemarks.Text;
            _financeEntry.CbMaster.SyncRowVersion = (short)(_actionTag is "UPDATE"
                ? ClsMasterSetup.ReturnMaxSyncBaseIdValue("AMS.CB_Master", "SyncRowVersion", "Voucher_No",
                    TxtVno.Text.Trim())
                : 1);
            _financeEntry.CbDetails.Voucher_No = TxtVno.Text;
            _financeEntry.CbDetails.Ledger_Id = _ledgerId;
            _financeEntry.CbDetails.Subledger_Id = _sub_ledgerId;
            _financeEntry.CbDetails.Agent_Id = _agentId;
            _financeEntry.CbDetails.Cls1 = _departmentId;
            _financeEntry.CbDetails.Debit = LblTotalLocalNetAmount.GetDecimal();
            _financeEntry.CbDetails.Credit = 0;
            _financeEntry.CbDetails.Narration =
                $"BEING PAYMENT MADE AGAINST PURCHASE INVOICE NO : {TxtRefVno.Text}";
            _financeEntry.CbDetails.Tbl_Amount = 0;
            _financeEntry.CbDetails.V_Amount = 0;

            _financeEntry.CbDetails.Party_No = TxtRefVno.Text;
            _financeEntry.CbDetails.Invoice_Date = MskDate.Text.GetDateTime();
            _financeEntry.CbDetails.Invoice_Miti = MskMiti.Text;
            _financeEntry.CbDetails.VatLedger_Id = 0;
            _financeEntry.CbDetails.PanNo = 0;
            _financeEntry.CbDetails.Vat_Reg = false;
            _financeEntry.CbDetails.CBLedgerId = ObjGlobal.SalesLedgerId;
            _financeEntry.CbDetails.CurrencyId = _currencyId;
            _financeEntry.CbDetails.CurrencyRate = TxtCurrencyRate.Text.GetDecimal();

            return _financeEntry.SaveCashBankVoucher(_actionTag);
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            this.NotifyError(ex.Message);
            return 0;
        }
    }

    internal int SavePurchaseInvoice()
    {
        return 0;
    }

    internal void PrintVoucher()
    {
        if (_setup.GetPrintVoucherList("GIT").Rows.Count > 0)
        {
            new FrmDocumentPrint("Crystal", "GIT", TxtVno.Text, TxtVno.Text).ShowDialog();
        }
    }

    internal void TextFromGrid()
    {
        if (DGrid.CurrentRow == null)
        {
            return;
        }

        TxtSno.Text = DGrid.Rows[_rowIndex].Cells["GTxtSno"].Value.GetString();
        TxtProduct.Text = DGrid.Rows[_rowIndex].Cells["GTxtProduct"].Value.GetString();
        _productId = DGrid.Rows[_rowIndex].Cells["GTxtProductId"].Value.GetLong();
        SetProductInfo(_productId);
        TxtGodown.Text = DGrid.Rows[_rowIndex].Cells["GTxtGodown"].Value.GetString();
        _godownId = DGrid.Rows[_rowIndex].Cells["GTxtGodownId"].Value.GetInt();
        _altUnitId = DGrid.Rows[_rowIndex].Cells["GTxtAltUOMId"].Value.GetInt();
        TxtAltQty.Text = DGrid.Rows[_rowIndex].Cells["GTxtAltQty"].Value.GetString();
        TxtAltUnit.Text = DGrid.Rows[_rowIndex].Cells["GTxtAltUOM"].Value.GetString();
        _unitId = DGrid.Rows[_rowIndex].Cells["GTxtUOMId"].Value.GetInt();
        TxtQty.Text = DGrid.Rows[_rowIndex].Cells["GTxtQty"].Value.GetString();
        TxtUnit.Text = DGrid.Rows[_rowIndex].Cells["GTxtUOM"].Value.GetString();
        TxtRate.Text = DGrid.Rows[_rowIndex].Cells["GTxtRate"].Value.GetString();
        TxtBasicAmount.Text = DGrid.Rows[_rowIndex].Cells["GTxtAmount"].Value.GetString();
        TxtTermAmount.Text = DGrid.Rows[_rowIndex].Cells["GTxtTermAmount"].Value.GetString();
        TxtNetAmount.Text = DGrid.Rows[_rowIndex].Cells["GTxtNetAmount"].Value.GetString();
        _isRowUpdate = true;
    }

    internal void SetProductInfo(long productId)
    {
        if (productId is 0)
        {
            return;
        }

        var dtLedger = _setup.GetMasterProductList(_actionTag, productId);
        if (dtLedger.Rows.Count <= 0)
        {
            return;
        }

        TxtShortName.Text = dtLedger.Rows[0]["PShortName"].ToString();
        //LblAltQty.Text = dtLedger.Rows[0]["AltStockQty"].ToString();
        //LblQty.Text = dtLedger.Rows[0]["StockQty"].ToString();
        _altUnitId = dtLedger.Rows[0]["PAltUnit"].GetInt();
        _unitId = dtLedger.Rows[0]["PUnit"].GetInt();
        _conQty = dtLedger.Rows[0]["PQtyConv"].GetDecimal();
        TxtAltUnit.Text = dtLedger.Rows[0]["AltUnitCode"].GetString();
        TxtUnit.Text = dtLedger.Rows[0]["UnitCode"].GetString();
        TxtAltQty.Enabled = _altUnitId > 0;
        TxtRate.Text = dtLedger.Rows[0]["PBuyRate"].GetDecimalString();
        //LblBuyRate.Text = dtLedger.Rows[0]["PBuyRate"].GetDecimalString();
        //LblSalesRate.Text = dtLedger.Rows[0]["PSalesRate"].GetDecimalString();
    }

    internal bool AddTextToGrid(bool isUpdate)
    {
        if (ObjGlobal.StockGodownMandatory && TxtGodown.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtGodown, "GODOWN IS MANDATORY..!!");
            return false;
        }

        if (ObjGlobal.FinanceCurrencyEnable && TxtCurrency.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtCurrency, "CURRENCY IS MANDATORY..!!");
            return false;
        }

        if (ObjGlobal.PurchaseDepartmentEnable && TxtDepartment.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtDepartment, "DEPARTMENT IS MANDATORY..!!");
            return false;
        }

        if (TxtQty.GetDecimal() <= 0)
        {
            this.NotifyValidationError(TxtQty, "PRODUCT QTY IS CANNOT BE ZERO..!!");
            return false;
        }

        var iRows = _rowIndex;
        if (!isUpdate)
        {
            DGrid.Rows.Add();
            DGrid.Rows[iRows].Cells["GTxtSNo"].Value = iRows + 1;
        }

        DGrid.Rows[iRows].Cells["GTxtProductId"].Value = _productId.ToString();
        DGrid.Rows[iRows].Cells["GTxtProduct"].Value = TxtProduct.Text;
        DGrid.Rows[iRows].Cells["GTxtGodownId"].Value = _godownId.ToString();
        DGrid.Rows[iRows].Cells["GTxtGodown"].Value = TxtGodown.Text;
        DGrid.Rows[iRows].Cells["GTxtAltQty"].Value = TxtAltQty.GetDecimalQtyString();
        DGrid.Rows[iRows].Cells["GTxtAltUOMId"].Value = _altUnitId.ToString();
        DGrid.Rows[iRows].Cells["GTxtAltUOM"].Value = TxtAltUnit.Text.Trim();
        DGrid.Rows[iRows].Cells["GTxtQty"].Value = TxtQty.GetDecimalQtyString();
        DGrid.Rows[iRows].Cells["GTxtUOMId"].Value = _unitId;
        DGrid.Rows[iRows].Cells["GTxtUOM"].Value = TxtUnit.Text;
        DGrid.Rows[iRows].Cells["GTxtRate"].Value = TxtRate.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtAmount"].Value = TxtBasicAmount.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtTermAmount"].Value = TxtTermAmount.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtNetAmount"].Value = TxtNetAmount.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtAltStockQty"].Value = TxtAltQty.GetDecimalQtyString();
        DGrid.Rows[iRows].Cells["GTxtStockQty"].Value = TxtQty.GetDecimalQtyString();
        DGrid.Rows[iRows].Cells["GTxtNarration"].Value = _description;
        DGrid.Rows[iRows].Cells["GTxtBatchNo"].Value = _batchNo.Length > 0 ? _batchNo : string.Empty;
        DGrid.Rows[iRows].Cells["GTxtMFGDate"].Value = _batchNo.Length > 0 ? _mfgDate : string.Empty;
        DGrid.Rows[iRows].Cells["GMskEXPDate"].Value = _batchNo.Length > 0 ? _expDate : string.Empty;
        DGrid.Rows[iRows].Cells["GTxtOrderNo"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtOrderSno"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtChallanNo"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtChallanSno"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtConFraction"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtFreeQty"].Value = 0;
        DGrid.Rows[iRows].Cells["GTxtStockFreeQty"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtFreeUnitId"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtExtraFreeQty"].Value = 0;
        DGrid.Rows[iRows].Cells["GTxtExtraFreeUnitId"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtTaxPriceRate"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtTaxGroupId"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtVatAmount"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtTaxableAmount"].Value = string.Empty;
        DGrid.CurrentCell = DGrid.Rows[_isRowUpdate ? iRows : DGrid.RowCount - 1].Cells[_columnIndex];
        if (_isRowUpdate)
        {
            EnableGridControl();
            ClearProductDetails();
            DGrid.Focus();
            return false;
        }

        ClearProductDetails();
        TxtProduct.AcceptsTab = false;
        GetSerialNo();
        return true;
    }

    internal void GetSerialNo()
    {
        for (var i = 0; i < DGrid.RowCount; i++)
        {
            TxtSno.Text = (i + 1).ToString();
            DGrid.Rows[i].Cells["GTxtSNo"].Value = TxtSno.Text;
        }
    }

    internal void VoucherTotalCalculation()
    {
        if (DGrid.RowCount <= 0 || !DGrid.Rows[0].Cells["GTxtProduct"].Value.IsValueExits())
        {
            return;
        }

        var enumerable = DGrid.Rows.OfType<DataGridViewRow>();
        var viewRows = enumerable as DataGridViewRow[] ?? enumerable.ToArray();

        var totalAltQty = viewRows.Sum(row => row.Cells["GTxtAltQty"].Value.GetDecimal());
        LblTotalAltQty.Text = totalAltQty.GetDecimalString();

        var totalQty = viewRows.Sum(row => row.Cells["GTxtQty"].Value.GetDecimal());
        LblTotalQty.Text = totalQty.GetDecimalString();

        var basicAmount = viewRows.Sum(row => row.Cells["GTxtNetAmount"].Value.GetDecimal());
        LblTotalBasicAmount.Text = basicAmount.GetDecimalString();

        var totalNetAmount = LblTotalBasicAmount.GetDecimal() + TxtBillTermAmount.GetDecimal();
        LblTotalNetAmount.Text = totalNetAmount.GetDecimalString();

        var localNetAmount = LblTotalNetAmount.GetDecimal() * TxtCurrencyRate.GetDecimal(true);
        LblTotalLocalNetAmount.Text = TxtCurrencyRate.GetDecimal() > 1 ? localNetAmount.GetDecimalString() : LblTotalNetAmount.Text;

        LblNumberInWords.Text = LblTotalLocalNetAmount.GetNumberInWords();
    }

    internal void AdjustControlsInDataGrid()
    {
        if (DGrid.CurrentRow == null)
        {
            return;
        }
        var currentRow = _rowIndex;

        var columnIndex = DGrid.Columns["GTxtSNo"].Index;
        TxtSno.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtSno.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtSno.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtShortName"].Index;
        TxtShortName.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtShortName.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtShortName.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtProduct"].Index;
        TxtProduct.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtProduct.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtProduct.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtGodown"].Index;
        TxtGodown.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtGodown.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtGodown.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtAltQty"].Index;
        TxtAltQty.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtAltQty.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtAltQty.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtAltUOM"].Index;
        TxtAltUnit.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtAltUnit.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtAltUnit.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtQty"].Index;
        TxtQty.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtQty.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtQty.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtUOM"].Index;
        TxtUnit.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtUnit.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtUnit.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtRate"].Index;
        TxtRate.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtRate.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtRate.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtAmount"].Index;
        TxtBasicAmount.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtBasicAmount.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtBasicAmount.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtTermAmount"].Index;
        TxtTermAmount.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtTermAmount.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtTermAmount.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtNetAmount"].Index;
        TxtNetAmount.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtNetAmount.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtNetAmount.TabIndex = columnIndex;
    }

    internal void DesignGridColumnsAsync()
    {
        _design.GetPurchaseEntryDesign(DGrid, "PB");
        DGrid.Columns["GTxtGodown"].Visible = ObjGlobal.StockGodownEnable;
        DGrid.Columns["GTxtTermAmount"].Visible = _dtProductTerm.Columns.Count > 0;
        DGrid.Columns["GTxtNetAmount"].Visible = _dtProductTerm.Columns.Count > 0;
        if (DGrid.Columns["GTxtGodown"].Visible)
        {
            DGrid.Columns["GTxtProduct"].Width -= DGrid.Columns["GTxtGodown"].Width;
        }
        DGrid.Columns["GTxtShortName"].Visible = ObjGlobal.StockShortNameWise;
        if (DGrid.Columns["GTxtShortName"].Visible)
        {
            DGrid.Columns["GTxtProduct"].Width -= DGrid.Columns["GTxtShortName"].Width;
        }
        DGrid.GotFocus += (sender, e) => { DGrid.Rows[_rowIndex].Cells[0].Selected = true; };
        DGrid.RowEnter += (_, e) => _rowIndex = e.RowIndex;
        DGrid.CellEnter += (_, e) => _columnIndex = e.ColumnIndex;
        DGrid.UserDeletedRow += (sender, e) =>
        {
            if (!_isRowDelete)
            {
                return;
            }
            if (DGrid.RowCount is 0)
            {
                DGrid.Rows.Add();
            }
            GetSerialNo();
        };
        DGrid.UserDeletingRow += (sender, e) =>
        {
            if (CustomMessageBox.DeleteRow() is DialogResult.No)
            {
                return;
            }

            _isRowDelete = true;
        };
        DGrid.KeyDown += DGrid_KeyDown;

        TxtSno = new MrGridNumericTextBox(DGrid)
        {
            ReadOnly = true,
            TextAlign = HorizontalAlignment.Center
        };
        TxtShortName = new MrGridTextBox(DGrid)
        {
            ReadOnly = !ObjGlobal.StockShortNameWise
        };
        TxtShortName.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.F1)
            {
                OpenProductList();
            }
            else if (e.KeyCode is Keys.F2)
            {
                (TxtProduct.Text, _productId) = GetMasterList.GetCounterProduct(TxtShortName.Text);
                SetProductInfo(_productId);
                TxtProduct.Focus();
            }
            else if (e.Shift && e.KeyCode is Keys.Tab)
            {
                SendToBack();
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                (TxtProduct.Text, _productId) = GetMasterList.CreateProduct(true);
                SetProductInfo(_productId);
                TxtProduct.Focus();
            }
            else
            {
                ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                    e.KeyCode.ToString(), TxtProduct, OpenProductList);
            }
        };
        TxtProduct = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtProduct.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.F1)
            {
                OpenProductList();
            }
            else if (e.KeyCode is Keys.F2)
            {
                (TxtProduct.Text, _productId) = GetMasterList.GetCounterProduct(TxtShortName.Text);
                SetProductInfo(_productId);
                TxtProduct.Focus();
            }
            else if (e.Shift && e.KeyCode is Keys.Tab)
            {
                SendToBack();
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                (TxtProduct.Text, _productId) = GetMasterList.CreateProduct(true);
                SetProductInfo(_productId);
                TxtProduct.Focus();
            }
            else
            {
                ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                    e.KeyCode.ToString(), TxtProduct, OpenProductList);
            }
        };
        TxtProduct.Validating += (_, e) =>
        {
            if (DGrid.RowCount is 0)
            {
                this.NotifyValidationError(TxtProduct, "PLEASE SELECT PRODUCT LEDGER FOR OPENING");
            }
            if (DGrid.RowCount == 1 && TxtProduct.Enabled && TxtProduct.IsBlankOrEmpty())
            {
                if (DGrid.Rows[0].Cells["GTxtProductId"].Value.GetLong() is 0)
                {
                    if (_getKeys.KeyChar is (char)Keys.Enter)
                    {
                        _getKeys.Handled = false;
                    }

                    this.NotifyValidationError(TxtProduct, "PLEASE SELECT PRODUCT LEDGER FOR OPENING");
                    return;
                }
            }
            if (DGrid.RowCount >= 1 && TxtProduct.Enabled && TxtProduct.IsBlankOrEmpty())
            {
                EnableGridControl();
                DGrid.ClearSelection();
                if (_getKeys is { KeyChar: (char)Keys.Escape })
                {
                    _getKeys = null;
                    return;
                }

                if (TxtBillTermAmount.Enabled)
                {
                    TxtBillTermAmount.Focus();
                }
                else if (TxtRemarks.Enabled)
                {
                    TxtRemarks.Focus();
                }
                else
                {
                    BtnSave.Focus();
                }
            }
        };
        TxtGodown = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtGodown.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.F1)
            {
                OpenGodownList();
            }
            else if (e.Shift && e.KeyCode is Keys.Tab)
            {
                SendToBack();
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                var frm = new FrmGodownName(true);
                frm.ShowDialog();
                TxtGodown.Text = frm.GodownMaster;
                _godownId = frm.GodownId;
                TxtGodown.Focus();
            }
            else
            {
                ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                    e.KeyCode.ToString(), TxtGodown, OpenGodownList);
            }
        };
        TxtAltQty = new MrGridNumericTextBox(DGrid);
        TxtAltQty.Validating += (sender, e) =>
        {
            if (TxtAltQty.Enabled && TxtAltQty.GetDecimal() > 0)
            {
                TxtAltQty.Text = TxtAltQty.GetDecimalQtyString();
            }
        };
        TxtAltQty.TextChanged += (sender, e) =>
        {
            if (!TxtAltQty.Focused)
            {
                return;
            }

            if (TxtAltQty.GetDecimal() > 0 && _conQty > 0)
            {
                TxtQty.Text = (TxtAltQty.GetDecimal() * _conQty).GetDecimalQtyString();
            }
            else if (TxtAltQty.GetDecimal() is 0)
            {
                TxtQty.Text = 1.GetDecimalQtyString();
            }
        };
        TxtAltUnit = new MrGridTextBox(DGrid);
        TxtQty = new MrGridNumericTextBox(DGrid);
        TxtQty.Validated += (sender, e) =>
        {
            TxtQty.Text = TxtQty.GetDecimalQtyString();
            if (!TxtQty.Enabled || !TxtProduct.Enabled)
            {
                return;
            }

            if (TxtQty.IsBlankOrEmpty())
            {
                this.NotifyValidationError(TxtQty, "PRODUCT OPENING QTY CANNOT BE ZERO");
            }
        };
        TxtQty.TextChanged += (sender, e) =>
        {
            if (!TxtQty.Focused)
            {
                return;
            }

            TxtBasicAmount.Text = TxtQty.Enabled && TxtRate.GetDecimal() > 0
                ? (TxtQty.GetDecimal() * TxtRate.GetDecimal()).GetDecimalString()
                : 0.GetDecimalString();
            TxtNetAmount.Text = (TxtBasicAmount.GetDecimal() - TxtTermAmount.GetDecimal()).GetDecimalString();
        };
        TxtUnit = new MrGridTextBox(DGrid);
        TxtRate = new MrGridNumericTextBox(DGrid);
        TxtRate.KeyDown += (sender, e) =>
        {
            if (e.Shift && e.KeyCode is Keys.Tab)
            {
                SendToBack();
            }
        };
        TxtRate.TextChanged += (sender, e) =>
        {
            if (!TxtRate.Focused)
            {
                return;
            }

            TxtBasicAmount.Text = TxtRate.GetDecimal() > 0
                ? (TxtQty.GetDecimal() * TxtRate.GetDecimal()).GetDecimalString()
                : 0.GetDecimalString();
            TxtNetAmount.Text = (TxtBasicAmount.GetDecimal() - TxtTermAmount.GetDecimal()).GetDecimalString();
        };
        TxtBasicAmount = new MrGridNumericTextBox(DGrid);
        TxtBasicAmount.KeyDown += (sender, e) =>
        {
            if (e.Shift && e.KeyCode is Keys.Tab)
            {
                SendToBack();
            }
        };
        TxtBasicAmount.Validating += (sender, e) =>
        {
            if (ActiveControl == TxtRate || TxtTermAmount.Enabled)
            {
                return;
            }

            if (TxtProduct.Enabled && TxtProduct.IsValueExits())
            {
                AddTextToGrid(_isRowUpdate);
                TxtProduct.Focus();
            }
        };
        TxtTermAmount = new MrGridNumericTextBox(DGrid);
        TxtTermAmount.Enter += (_, e) =>
        {
            if (!TxtProduct.IsValueExits() || !TxtProduct.Enabled)
            {
                return;
            }

            var existingTerm = new DataTable();
            var serialNo = 0;
            if (DGrid.CurrentRow != null)
            {
                serialNo = DGrid.CurrentRow.Cells["GTxtSno"].Value.GetInt();
                var exDetails = _dtProductTerm.AsEnumerable().Any(c =>
                    c.Field<string>("ProductSno").Equals(serialNo.ToString()) &&
                    c.Field<string>("ProductId").Equals(_productId.ToString()));
                if (exDetails)
                {
                    existingTerm = _dtProductTerm.Select($"ProductSno= '{serialNo}' and ProductId='{_productId}'")
                        .CopyToDataTable();
                }
            }

            var result = new FrmTermCalculation(_actionTag == "SAVE" && !_isRowUpdate ? "SAVE" : "UPDATE",
                TxtBasicAmount.Text, true, "PB", _productId, serialNo, existingTerm);
            result.ShowDialog();
            TxtTermAmount.Text = result.TotalTermAmount;
            AddToProductTerm(result.CalcTermTable);
            TxtNetAmount.Focus();
        };
        TxtTermAmount.TextChanged += (sender, e) =>
        {
            var basicAmount = TxtBasicAmount.GetDecimal();
            var termAmount = TxtTermAmount.GetDecimal();
            TxtNetAmount.Text = (basicAmount + termAmount).GetDecimalString();
        };
        TxtTermAmount.Validating += (sender, e) =>
        {
            if (ActiveControl == TxtBasicAmount)
            {
                return;
            }

            if (!TxtProduct.Enabled || !TxtProduct.IsValueExits())
            {
                return;
            }

            AddTextToGrid(_isRowUpdate);
            TxtProduct.Focus();
        };
        TxtNetAmount = new MrGridNumericTextBox(DGrid);
        ObjGlobal.DGridColorCombo(DGrid);
        AdjustControlsInDataGrid();
    }

    internal void AddToProductTerm(DataTable dtTerm)
    {
        if (dtTerm.RowsCount() <= 0)
        {
            return;
        }

        var serialNo = 0;
        if (DGrid.CurrentRow != null)
        {
            serialNo = DGrid.CurrentRow.Cells["GTxtSno"].Value.GetInt();
            var exDetails = _dtProductTerm.AsEnumerable().Any(c =>
                c.Field<string>("ProductSno").Equals(serialNo.ToString()) &&
                c.Field<string>("ProductId").Equals(_productId.ToString()));
            if (exDetails)
            {
                foreach (DataRow ro in dtTerm.Rows)
                foreach (DataRow row in _dtProductTerm.Rows)
                    if (row["ProductSno"] == ro["ProductSno"] && row["ProductId"] == ro["ProductId"])
                    {
                        _dtProductTerm.Rows.Remove(row);
                    }
            }
        }

        foreach (DataRow ro in dtTerm.Rows)
        {
            var dataRow = _dtProductTerm.NewRow();
            dataRow["OrderNo"] = ro["GTxtOrderNo"];
            dataRow["SNo"] = ro["GTxtSno"];
            dataRow["TermId"] = ro["GTxtTermId"];
            dataRow["TermName"] = ro["GTxtDescription"];
            dataRow["Basis"] = ro["GTxtBasic"];
            dataRow["Sign"] = ro["GTxtSign"];
            dataRow["ProductId"] = ro["GTxtProductId"];
            dataRow["TermType"] = ro["GTxtTermCondition"];
            dataRow["TermRate"] = ro["GTxtRate"];
            dataRow["TermAmt"] = ro["GTxtValueAmount"];
            dataRow["Source"] = "PB";
            dataRow["Formula"] = string.Empty;
            dataRow["ProductSno"] = ro["GTxtProductSno"];
            _dtProductTerm.Rows.InsertAt(dataRow, _dtProductTerm.RowsCount() + 1);
        }
    }

    internal void AddToBillingTerm(DataTable dtTerm)
    {
        if (dtTerm.RowsCount() <= 0)
        {
            return;
        }

        _dtBillTerm.Rows.Clear();
        foreach (DataRow ro in dtTerm.Rows)
        {
            var dataRow = _dtBillTerm.NewRow();
            dataRow["OrderNo"] = ro["GTxtOrderNo"];
            dataRow["SNo"] = ro["GTxtSno"];
            dataRow["TermId"] = ro["GTxtTermId"];
            dataRow["TermName"] = ro["GTxtDescription"];
            dataRow["Basis"] = ro["GTxtBasic"];
            dataRow["Sign"] = ro["GTxtSign"];
            dataRow["ProductId"] = ro["GTxtProductId"];
            dataRow["TermType"] = ro["GTxtTermCondition"];
            dataRow["TermRate"] = ro["GTxtRate"];
            dataRow["TermAmt"] = ro["GTxtValueAmount"];
            dataRow["Source"] = "PB";
            dataRow["Formula"] = string.Empty;
            dataRow["ProductSno"] = ro["GTxtProductSno"];
            _dtBillTerm.Rows.InsertAt(dataRow, _dtProductTerm.RowsCount() + 1);
        }
    }

    internal void CashAndBankValidation(string ledgerType)
    {
        var partyInfo = new FrmPartyInfo(ledgerType, _dtPartyInfo, "PB");
        partyInfo.ShowDialog();
        if (_dtPartyInfo.Rows.Count > 0)
        {
            _dtPartyInfo.Rows.Clear();
        }

        foreach (DataRow row in partyInfo.PartyInfo.Rows)
        {
            var dr = _dtPartyInfo.NewRow();
            dr["PartyLedgerId"] = row["PartyLedger_Id"];
            dr["PartyName"] = row["PartyName"];
            dr["ChequeNo"] = row["ChequeNo"];
            dr["ChequeDate"] = row["ChequeDate"];
            dr["VatNo"] = row["VatNo"];
            dr["ContactPerson"] = row["ContactPerson"];
            dr["Address"] = row["Address"];
            dr["City"] = row["City"];
            dr["Mob"] = row["Mob"];
            dr["Email"] = row["Email"];
            _dtPartyInfo.Rows.Add(dr);
        }
    }

    internal void FillInvoiceData(string voucherNo)
    {
        try
        {
            var dsPurchase = _entry.ReturnPurchaseInvoiceDetailsInDataSet(voucherNo);
            if (dsPurchase.Tables.Count > 0)
            {
                if (dsPurchase.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsPurchase.Tables[0].Rows)
                    {
                        if (_actionTag != "SAVE")
                        {
                            TxtVno.Text = dr["PB_Invoice"].ToString();
                        }

                        MskMiti.Text = dr["Invoice_Miti"].ToString();
                        MskDate.Text = dr["Invoice_Date"].GetDateString();
                        TxtRefVno.Text = dr["PB_VNo"].ToString();

                        if (!string.IsNullOrEmpty(dr["VNo_Date"].ToString()))
                        {
                            MskRefDate.Text = dr["VNo_Date"].GetDateString();
                        }

                        if (_actionTag != "SAVE")
                        {
                            TxtOrder.Text = dr["PO_Invoice"].ToString();
                            if (!string.IsNullOrEmpty(TxtOrder.Text.Trim()))
                            {
                                _mskOrderDate = dr["PO_Date"].GetDateString();
                            }

                            TxtChallan.Text = Convert.ToString(dr["PC_Invoice"].ToString());
                            if (!string.IsNullOrEmpty(TxtChallan.Text.Trim()))
                            {
                                _mskChallanDate = dr["PC_Date"].GetDateString();
                            }
                        }

                        TxtVendor.Text = Convert.ToString(dr["GLName"].ToString());
                        _ledgerId = ObjGlobal.ReturnLong(dr["Vendor_ID"].ToString());
                        SetLedgerInfo(_ledgerId);
                        TxtDueDays.Text = dr["DueDays"].ToString();
                        MskDueDays.Text = dr["DueDate"].GetDateString();
                        _dtPartyInfo.Rows.Clear();
                        var drp = _dtPartyInfo.NewRow();
                        drp["PartyLedgerId"] = null;
                        drp["PartyName"] = dr["Party_Name"];
                        drp["ChequeNo"] = dr["ChqNo"];
                        drp["ChequeDate"] = dr["ChqDate"].GetDateString();
                        drp["VatNo"] = dr["Vat_No"];
                        drp["ContactPerson"] = dr["Contact_Person"];
                        drp["Address"] = dr["Address"];
                        drp["Mob"] = dr["Mobile_No"];
                        drp["Email"] = string.Empty;
                        _dtPartyInfo.Rows.Add(drp);
                        TxtSubledger.Text = dr["SlName"].ToString();
                        TxtAgent.Text = dr["AgentName"].ToString();
                        TxtDepartment.Text = dr["DName"].ToString();
                        _departmentId = dr["Cls1"].GetInt();
                        if (dr["Cur_Id"].GetInt() > 0)
                        {
                            _currencyId = dr["Cur_Id"].GetInt();
                            TxtCurrency.Text = dr["Ccode"].ToString();
                        }
                        else
                        {
                            _currencyId = ObjGlobal.SysCurrencyId;
                            TxtCurrency.Text = ObjGlobal.SysCurrency;
                        }

                        TxtCurrencyRate.Text = dr["Cur_Rate"].GetDecimalString(true);
                        LblTotalBasicAmount.Text = dr["B_Amount"].GetDecimalString();
                        TxtBillTermAmount.Text = dr["T_Amount"].GetDecimalString();
                        LblTotalNetAmount.Text = dr["N_Amount"].GetDecimalString();
                        LblTotalLocalNetAmount.Text = dr["LN_Amount"].GetDecimalString();
                        TxtRemarks.Text = Convert.ToString(dr["Remarks"].ToString());
                        PAttachment1.Image = dr["PAttachment1"].GetImage();
                        PAttachment2.Image = dr["PAttachment2"].GetImage();
                        PAttachment3.Image = dr["PAttachment3"].GetImage();
                        PAttachment4.Image = dr["PAttachment4"].GetImage();
                        PAttachment5.Image = dr["PAttachment5"].GetImage();
                    }

                    if (dsPurchase.Tables[1].Rows.Count > 0)
                    {
                        DGrid.Rows.Clear();
                        var iRows = 0;
                        DGrid.Rows.Add(dsPurchase.Tables[1].Rows.Count + 1);
                        foreach (DataRow dr in dsPurchase.Tables[1].Rows)
                        {
                            DGrid.Rows[iRows].Cells["GTxtSNo"].Value = dr["Invoice_SNo"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtProductId"].Value = dr["P_Id"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtProduct"].Value = dr["PName"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtGodownId"].Value = dr["Gdn_Id"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtGodown"].Value = dr["GName"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtAltQty"].Value = dr["Alt_Qty"].GetDecimalQtyString();
                            DGrid.Rows[iRows].Cells["GTxtAltUOMId"].Value = dr["Alt_UnitId"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtAltUOM"].Value = dr["AltUnitId"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtQty"].Value = dr["Qty"].GetDecimalQtyString();
                            DGrid.Rows[iRows].Cells["GTxtUOMId"].Value = dr["Unit_Id"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtUOM"].Value = dr["UnitCode"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtRate"].Value = dr["Rate"].GetDecimalString();
                            DGrid.Rows[iRows].Cells["GTxtAmount"].Value = dr["B_Amount"].GetDecimalString();
                            DGrid.Rows[iRows].Cells["GTxtTermAmount"].Value = dr["T_Amount"].GetDecimalString();
                            DGrid.Rows[iRows].Cells["GTxtNetAmount"].Value = dr["N_Amount"].GetDecimalString();
                            DGrid.Rows[iRows].Cells["GTxtAltStockQty"].Value = dr["AltStock_Qty"].GetDecimal();
                            DGrid.Rows[iRows].Cells["GTxtStockQty"].Value = dr["Stock_Qty"].GetDecimal();
                            DGrid.Rows[iRows].Cells["GTxtNarration"].Value = dr["Narration"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtOrderNo"].Value = dr["PO_Invoice"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtOrderSno"].Value = dr["PO_Sno"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtChallanNo"].Value = dr["PC_Invoice"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtChallanSno"].Value = dr["PC_SNo"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtConFraction"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtFreeQty"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtStockFreeQty"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtFreeUnitId"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtExtraFreeQty"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtExtraFreeUnitId"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtTaxPriceRate"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtVatAmount"].Value = string.Empty;
                            DGrid.CurrentCell = DGrid.Rows[DGrid.RowCount - 1].Cells[_columnIndex];
                            iRows++;
                        }

                        DGrid.ClearSelection();
                    }

                    if (dsPurchase.Tables[2].Rows.Count > 0)
                    {
                        _dtProductTerm = dsPurchase.Tables[2];
                    }

                    if (dsPurchase.Tables[3].Rows.Count > 0)
                    {
                        _dtBillTerm = dsPurchase.Tables[3];
                    }
                }
            }

            ObjGlobal.DGridColorCombo(DGrid);
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
        }
    }

    internal void FillChallanData(string voucherNo)
    {
        try
        {
            var dsPurchase = _entry.ReturnPurchaseInvoiceDetailsInDataSet(voucherNo);
            if (dsPurchase.Tables.Count > 0)
            {
                if (dsPurchase.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsPurchase.Tables[0].Rows)
                    {
                        if (_actionTag != "SAVE")
                        {
                            TxtVno.Text = dr["PB_Invoice"].ToString();
                        }

                        MskMiti.Text = dr["Invoice_Miti"].ToString();
                        MskDate.Text = dr["Invoice_Date"].GetDateString();
                        TxtRefVno.Text = dr["PB_VNo"].ToString();

                        if (!string.IsNullOrEmpty(dr["VNo_Date"].ToString()))
                        {
                            MskRefDate.Text = dr["VNo_Date"].GetDateString();
                        }

                        if (_actionTag != "SAVE")
                        {
                            TxtOrder.Text = dr["PO_Invoice"].ToString();
                            if (!string.IsNullOrEmpty(TxtOrder.Text.Trim()))
                            {
                                _mskOrderDate = dr["PO_Date"].GetDateString();
                            }

                            TxtChallan.Text = Convert.ToString(dr["PC_Invoice"].ToString());
                            if (!string.IsNullOrEmpty(TxtChallan.Text.Trim()))
                            {
                                _mskChallanDate = dr["PC_Date"].GetDateString();
                            }
                        }

                        TxtVendor.Text = Convert.ToString(dr["GLName"].ToString());
                        _ledgerId = ObjGlobal.ReturnLong(dr["Vendor_ID"].ToString());
                        SetLedgerInfo(_ledgerId);
                        TxtDueDays.Text = dr["DueDays"].ToString();
                        MskDueDays.Text = dr["DueDate"].GetDateString();
                        _dtPartyInfo.Rows.Clear();
                        var drp = _dtPartyInfo.NewRow();
                        drp["PartyLedger_Id"] = null;
                        drp["PartyName"] = dr["Party_Name"];
                        drp["ChequeNo"] = dr["ChqNo"];
                        drp["ChequeDate"] = dr["ChqDate"].GetDateString();
                        drp["VatNo"] = dr["Vat_No"];
                        drp["ContactPerson"] = dr["Contact_Person"];
                        drp["Address"] = dr["Address"];
                        drp["Mob"] = dr["Mobile_No"];
                        _dtPartyInfo.Rows.Add(drp);
                        TxtSubledger.Text = dr["SlName"].ToString();
                        TxtAgent.Text = dr["AgentName"].ToString();
                        TxtDepartment.Text = dr["DName"].ToString();
                        _departmentId = dr["Cls1"].GetInt();
                        if (dr["Cur_Id"].GetInt() > 0)
                        {
                            _currencyId = dr["Cur_Id"].GetInt();
                            TxtCurrency.Text = dr["Ccode"].ToString();
                        }
                        else
                        {
                            _currencyId = ObjGlobal.SysCurrencyId;
                            TxtCurrency.Text = ObjGlobal.SysCurrency;
                        }

                        TxtCurrencyRate.Text = dr["Cur_Rate"].GetDecimalString(true);
                        LblTotalBasicAmount.Text = dr["B_Amount"].GetDecimalString();
                        TxtBillTermAmount.Text = dr["T_Amount"].GetDecimalString();
                        LblTotalNetAmount.Text = dr["N_Amount"].GetDecimalString();
                        LblTotalLocalNetAmount.Text = dr["LN_Amount"].GetDecimalString();
                        TxtRemarks.Text = Convert.ToString(dr["Remarks"].ToString());
                        PAttachment1.Image = dr["PAttachment1"].GetImage();
                        PAttachment2.Image = dr["PAttachment2"].GetImage();
                        PAttachment3.Image = dr["PAttachment3"].GetImage();
                        PAttachment4.Image = dr["PAttachment4"].GetImage();
                        PAttachment5.Image = dr["PAttachment5"].GetImage();
                    }

                    if (dsPurchase.Tables[1].Rows.Count > 0)
                    {
                        DGrid.Rows.Clear();
                        var iRows = 0;
                        DGrid.Rows.Add(dsPurchase.Tables[1].Rows.Count + 1);
                        foreach (DataRow dr in dsPurchase.Tables[1].Rows)
                        {
                            DGrid.Rows[iRows].Cells["GTxtSNo"].Value = dr["Invoice_SNo"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtProductId"].Value = dr["P_Id"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtProduct"].Value = dr["PName"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtGodownId"].Value = dr["Gdn_Id"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtGodown"].Value = dr["GName"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtAltQty"].Value = dr["Alt_Qty"].GetDecimalQtyString();
                            DGrid.Rows[iRows].Cells["GTxtAltUOMId"].Value = dr["Alt_UnitId"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtAltUOM"].Value = dr["AltUnitId"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtQty"].Value = dr["Qty"].GetDecimalQtyString();
                            DGrid.Rows[iRows].Cells["GTxtUOMId"].Value = dr["Unit_Id"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtUOM"].Value = dr["UnitCode"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtRate"].Value = dr["Rate"].GetDecimalString();
                            DGrid.Rows[iRows].Cells["GTxtAmount"].Value = dr["B_Amount"].GetDecimalString();
                            DGrid.Rows[iRows].Cells["GTxtTermAmount"].Value = dr["T_Amount"].GetDecimalString();
                            DGrid.Rows[iRows].Cells["GTxtNetAmount"].Value = dr["N_Amount"].GetDecimalString();
                            DGrid.Rows[iRows].Cells["GTxtAltStockQty"].Value = dr["AltStock_Qty"].GetDecimal();
                            DGrid.Rows[iRows].Cells["GTxtStockQty"].Value = dr["Stock_Qty"].GetDecimal();
                            DGrid.Rows[iRows].Cells["GTxtNarration"].Value = dr["Narration"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtOrderNo"].Value = dr["PO_Invoice"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtOrderSno"].Value = dr["PO_Sno"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtChallanNo"].Value = dr["PC_Invoice"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtChallanSno"].Value = dr["PC_SNo"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtConFraction"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtFreeQty"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtStockFreeQty"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtFreeUnitId"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtExtraFreeQty"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtExtraFreeUnitId"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtTaxPriceRate"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtVatAmount"].Value = string.Empty;
                            DGrid.CurrentCell = DGrid.Rows[DGrid.RowCount - 1].Cells[_columnIndex];
                            iRows++;
                        }

                        DGrid.ClearSelection();
                    }

                    if (dsPurchase.Tables[2].Rows.Count > 0)
                    {
                        _dtProductTerm = dsPurchase.Tables[2];
                    }

                    if (dsPurchase.Tables[3].Rows.Count > 0)
                    {
                        _dtBillTerm = dsPurchase.Tables[3];
                    }
                }
            }

            ObjGlobal.DGridColorCombo(DGrid);
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
        }
    }

    internal void FillOrderData(string voucherNo)
    {
        try
        {
            var dsPurchase = _entry.ReturnPurchaseInvoiceDetailsInDataSet(voucherNo);
            if (dsPurchase.Tables.Count > 0)
            {
                if (dsPurchase.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsPurchase.Tables[0].Rows)
                    {
                        if (_actionTag != "SAVE")
                        {
                            TxtVno.Text = dr["PB_Invoice"].ToString();
                        }

                        MskMiti.Text = dr["Invoice_Miti"].ToString();
                        MskDate.Text = dr["Invoice_Date"].GetDateString();
                        TxtRefVno.Text = dr["PB_VNo"].ToString();

                        if (!string.IsNullOrEmpty(dr["VNo_Date"].ToString()))
                        {
                            MskRefDate.Text = dr["VNo_Date"].GetDateString();
                        }

                        if (_actionTag != "SAVE")
                        {
                            TxtOrder.Text = dr["PO_Invoice"].ToString();
                            if (!string.IsNullOrEmpty(TxtOrder.Text.Trim()))
                            {
                                _mskOrderDate = dr["PO_Date"].GetDateString();
                            }

                            TxtChallan.Text = Convert.ToString(dr["PC_Invoice"].ToString());
                            if (!string.IsNullOrEmpty(TxtChallan.Text.Trim()))
                            {
                                _mskChallanDate = dr["PC_Date"].GetDateString();
                            }
                        }

                        TxtVendor.Text = Convert.ToString(dr["GLName"].ToString());
                        _ledgerId = ObjGlobal.ReturnLong(dr["Vendor_ID"].ToString());
                        SetLedgerInfo(_ledgerId);
                        TxtDueDays.Text = dr["DueDays"].ToString();
                        MskDueDays.Text = dr["DueDate"].GetDateString();
                        _dtPartyInfo.Rows.Clear();
                        var drp = _dtPartyInfo.NewRow();
                        drp["PartyLedger_Id"] = null;
                        drp["PartyName"] = dr["Party_Name"];
                        drp["ChequeNo"] = dr["ChqNo"];
                        drp["ChequeDate"] = dr["ChqDate"].GetDateString();
                        drp["VatNo"] = dr["Vat_No"];
                        drp["ContactPerson"] = dr["Contact_Person"];
                        drp["Address"] = dr["Address"];
                        drp["Mob"] = dr["Mobile_No"];
                        _dtPartyInfo.Rows.Add(drp);
                        TxtSubledger.Text = dr["SlName"].ToString();
                        TxtAgent.Text = dr["AgentName"].ToString();
                        TxtDepartment.Text = dr["DName"].ToString();
                        _departmentId = dr["Cls1"].GetInt();
                        if (dr["Cur_Id"].GetInt() > 0)
                        {
                            _currencyId = dr["Cur_Id"].GetInt();
                            TxtCurrency.Text = dr["Ccode"].ToString();
                        }
                        else
                        {
                            _currencyId = ObjGlobal.SysCurrencyId;
                            TxtCurrency.Text = ObjGlobal.SysCurrency;
                        }

                        TxtCurrencyRate.Text = dr["Cur_Rate"].GetDecimalString(true);
                        LblTotalBasicAmount.Text = dr["B_Amount"].GetDecimalString();
                        TxtBillTermAmount.Text = dr["T_Amount"].GetDecimalString();
                        LblTotalNetAmount.Text = dr["N_Amount"].GetDecimalString();
                        LblTotalLocalNetAmount.Text = dr["LN_Amount"].GetDecimalString();
                        TxtRemarks.Text = Convert.ToString(dr["Remarks"].ToString());
                        PAttachment1.Image = dr["PAttachment1"].GetImage();
                        PAttachment2.Image = dr["PAttachment2"].GetImage();
                        PAttachment3.Image = dr["PAttachment3"].GetImage();
                        PAttachment4.Image = dr["PAttachment4"].GetImage();
                        PAttachment5.Image = dr["PAttachment5"].GetImage();
                    }

                    if (dsPurchase.Tables[1].Rows.Count > 0)
                    {
                        DGrid.Rows.Clear();
                        var iRows = 0;
                        DGrid.Rows.Add(dsPurchase.Tables[1].Rows.Count + 1);
                        foreach (DataRow dr in dsPurchase.Tables[1].Rows)
                        {
                            DGrid.Rows[iRows].Cells["GTxtSNo"].Value = dr["Invoice_SNo"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtProductId"].Value = dr["P_Id"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtProduct"].Value = dr["PName"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtGodownId"].Value = dr["Gdn_Id"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtGodown"].Value = dr["GName"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtAltQty"].Value = dr["Alt_Qty"].GetDecimalQtyString();
                            DGrid.Rows[iRows].Cells["GTxtAltUOMId"].Value = dr["Alt_UnitId"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtAltUOM"].Value = dr["AltUnitId"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtQty"].Value = dr["Qty"].GetDecimalQtyString();
                            DGrid.Rows[iRows].Cells["GTxtUOMId"].Value = dr["Unit_Id"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtUOM"].Value = dr["UnitCode"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtRate"].Value = dr["Rate"].GetDecimalString();
                            DGrid.Rows[iRows].Cells["GTxtAmount"].Value = dr["B_Amount"].GetDecimalString();
                            DGrid.Rows[iRows].Cells["GTxtTermAmount"].Value = dr["T_Amount"].GetDecimalString();
                            DGrid.Rows[iRows].Cells["GTxtNetAmount"].Value = dr["N_Amount"].GetDecimalString();
                            DGrid.Rows[iRows].Cells["GTxtAltStockQty"].Value = dr["AltStock_Qty"].GetDecimal();
                            DGrid.Rows[iRows].Cells["GTxtStockQty"].Value = dr["Stock_Qty"].GetDecimal();
                            DGrid.Rows[iRows].Cells["GTxtNarration"].Value = dr["Narration"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtOrderNo"].Value = dr["PO_Invoice"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtOrderSno"].Value = dr["PO_Sno"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtChallanNo"].Value = dr["PC_Invoice"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtChallanSno"].Value = dr["PC_SNo"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtConFraction"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtFreeQty"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtStockFreeQty"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtFreeUnitId"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtExtraFreeQty"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtExtraFreeUnitId"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtTaxPriceRate"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtVatAmount"].Value = string.Empty;
                            DGrid.CurrentCell = DGrid.Rows[DGrid.RowCount - 1].Cells[_columnIndex];
                            iRows++;
                        }

                        DGrid.ClearSelection();
                    }

                    if (dsPurchase.Tables[2].Rows.Count > 0)
                    {
                        _dtProductTerm = dsPurchase.Tables[2];
                    }

                    if (dsPurchase.Tables[3].Rows.Count > 0)
                    {
                        _dtBillTerm = dsPurchase.Tables[3];
                    }
                }
            }

            ObjGlobal.DGridColorCombo(DGrid);
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
        }
    }

    internal void FillSalesInvoiceData(string voucherNo)
    {
        try
        {
            var dsPurchase = _entry.ReturnPurchaseInvoiceDetailsInDataSet(voucherNo);
            if (dsPurchase.Tables.Count > 0)
            {
                if (dsPurchase.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsPurchase.Tables[0].Rows)
                    {
                        if (_actionTag != "SAVE")
                        {
                            TxtVno.Text = dr["PB_Invoice"].ToString();
                        }

                        MskMiti.Text = dr["Invoice_Miti"].ToString();
                        MskDate.Text = dr["Invoice_Date"].GetDateString();
                        TxtRefVno.Text = dr["PB_VNo"].ToString();

                        if (!string.IsNullOrEmpty(dr["VNo_Date"].ToString()))
                        {
                            MskRefDate.Text = dr["VNo_Date"].GetDateString();
                        }

                        if (_actionTag != "SAVE")
                        {
                            TxtOrder.Text = dr["PO_Invoice"].ToString();
                            if (!string.IsNullOrEmpty(TxtOrder.Text.Trim()))
                            {
                                _mskOrderDate = dr["PO_Date"].GetDateString();
                            }

                            TxtChallan.Text = Convert.ToString(dr["PC_Invoice"].ToString());
                            if (!string.IsNullOrEmpty(TxtChallan.Text.Trim()))
                            {
                                _mskChallanDate = dr["PC_Date"].GetDateString();
                            }
                        }

                        TxtVendor.Text = Convert.ToString(dr["GLName"].ToString());
                        _ledgerId = ObjGlobal.ReturnLong(dr["Vendor_ID"].ToString());
                        SetLedgerInfo(_ledgerId);
                        TxtDueDays.Text = dr["DueDays"].ToString();
                        MskDueDays.Text = dr["DueDate"].GetDateString();
                        _dtPartyInfo.Rows.Clear();
                        var drp = _dtPartyInfo.NewRow();
                        drp["PartyLedger_Id"] = null;
                        drp["PartyName"] = dr["Party_Name"];
                        drp["ChequeNo"] = dr["ChqNo"];
                        drp["ChequeDate"] = dr["ChqDate"].GetDateString();
                        drp["VatNo"] = dr["Vat_No"];
                        drp["ContactPerson"] = dr["Contact_Person"];
                        drp["Address"] = dr["Address"];
                        drp["Mob"] = dr["Mobile_No"];
                        _dtPartyInfo.Rows.Add(drp);
                        TxtSubledger.Text = dr["SlName"].ToString();
                        TxtAgent.Text = dr["AgentName"].ToString();
                        TxtDepartment.Text = dr["DName"].ToString();
                        _departmentId = dr["Cls1"].GetInt();
                        if (dr["Cur_Id"].GetInt() > 0)
                        {
                            _currencyId = dr["Cur_Id"].GetInt();
                            TxtCurrency.Text = dr["Ccode"].ToString();
                        }
                        else
                        {
                            _currencyId = ObjGlobal.SysCurrencyId;
                            TxtCurrency.Text = ObjGlobal.SysCurrency;
                        }

                        TxtCurrencyRate.Text = dr["Cur_Rate"].GetDecimalString(true);
                        LblTotalBasicAmount.Text = dr["B_Amount"].GetDecimalString();
                        TxtBillTermAmount.Text = dr["T_Amount"].GetDecimalString();
                        LblTotalNetAmount.Text = dr["N_Amount"].GetDecimalString();
                        LblTotalLocalNetAmount.Text = dr["LN_Amount"].GetDecimalString();
                        TxtRemarks.Text = Convert.ToString(dr["Remarks"].ToString());
                        PAttachment1.Image = dr["PAttachment1"].GetImage();
                        PAttachment2.Image = dr["PAttachment2"].GetImage();
                        PAttachment3.Image = dr["PAttachment3"].GetImage();
                        PAttachment4.Image = dr["PAttachment4"].GetImage();
                        PAttachment5.Image = dr["PAttachment5"].GetImage();
                    }

                    if (dsPurchase.Tables[1].Rows.Count > 0)
                    {
                        DGrid.Rows.Clear();
                        var iRows = 0;
                        DGrid.Rows.Add(dsPurchase.Tables[1].Rows.Count + 1);
                        foreach (DataRow dr in dsPurchase.Tables[1].Rows)
                        {
                            DGrid.Rows[iRows].Cells["GTxtSNo"].Value = dr["Invoice_SNo"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtProductId"].Value = dr["P_Id"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtProduct"].Value = dr["PName"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtGodownId"].Value = dr["Gdn_Id"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtGodown"].Value = dr["GName"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtAltQty"].Value = dr["Alt_Qty"].GetDecimalQtyString();
                            DGrid.Rows[iRows].Cells["GTxtAltUOMId"].Value = dr["Alt_UnitId"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtAltUOM"].Value = dr["AltUnitId"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtQty"].Value = dr["Qty"].GetDecimalQtyString();
                            DGrid.Rows[iRows].Cells["GTxtUOMId"].Value = dr["Unit_Id"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtUOM"].Value = dr["UnitCode"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtRate"].Value = dr["Rate"].GetDecimalString();
                            DGrid.Rows[iRows].Cells["GTxtAmount"].Value = dr["B_Amount"].GetDecimalString();
                            DGrid.Rows[iRows].Cells["GTxtTermAmount"].Value = dr["T_Amount"].GetDecimalString();
                            DGrid.Rows[iRows].Cells["GTxtNetAmount"].Value = dr["N_Amount"].GetDecimalString();
                            DGrid.Rows[iRows].Cells["GTxtAltStockQty"].Value = dr["AltStock_Qty"].GetDecimal();
                            DGrid.Rows[iRows].Cells["GTxtStockQty"].Value = dr["Stock_Qty"].GetDecimal();
                            DGrid.Rows[iRows].Cells["GTxtNarration"].Value = dr["Narration"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtOrderNo"].Value = dr["PO_Invoice"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtOrderSno"].Value = dr["PO_Sno"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtChallanNo"].Value = dr["PC_Invoice"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtChallanSno"].Value = dr["PC_SNo"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtConFraction"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtFreeQty"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtStockFreeQty"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtFreeUnitId"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtExtraFreeQty"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtExtraFreeUnitId"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtTaxPriceRate"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtVatAmount"].Value = string.Empty;
                            DGrid.CurrentCell = DGrid.Rows[DGrid.RowCount - 1].Cells[_columnIndex];
                            iRows++;
                        }

                        DGrid.ClearSelection();
                    }

                    if (dsPurchase.Tables[2].Rows.Count > 0)
                    {
                        _dtProductTerm = dsPurchase.Tables[2];
                    }

                    if (dsPurchase.Tables[3].Rows.Count > 0)
                    {
                        _dtBillTerm = dsPurchase.Tables[3];
                    }
                }
            }

            ObjGlobal.DGridColorCombo(DGrid);
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
        }
    }

    #endregion --------------- METHOD FOR THIS FORM ---------------

    #region --------------- BUTTON CLICK EVENTS ---------------

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        ReturnVoucherNo();
        ClearControl();
        EnableControl(true);
        MskMiti.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _actionTag = "UPDATE";
        ClearControl();
        EnableControl(true);
        TxtVno.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        ClearControl();
        EnableControl();
        TxtVno.Focus();
    }

    private void BtnSalesInvoice_Click(object sender, EventArgs e)
    {
        BtnNew.PerformClick();
        var voucherNo = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "SB");
        FillSalesInvoiceData(voucherNo);
    }

    private void BtnPrintInvoice_Click(object sender, EventArgs e)
    {
        var frmDp = new FrmDocumentPrint("Crystal", "GIT", TxtVno.Text, TxtVno.Text, TxtVno.Text)
        {
            Owner = ActiveForm
        };
        frmDp.ShowDialog();
    }

    private void BtnReverse_Click(object sender, EventArgs e)
    {
        _actionTag = "REVERSE";
        ClearControl();
        EnableControl();
        TxtVno.Focus();
    }

    private void BtnCopy_Click(object sender, EventArgs e)
    {
        BtnNew.PerformClick();
        TxtVno.Text = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "PB");
        FillInvoiceData(TxtVno.Text);
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
    }

    private void BtnVno_Click(object sender, EventArgs e)
    {
        TxtVno.Text = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "PB");
        if (_actionTag != "SAVE")
        {
            FillInvoiceData(TxtVno.Text);
        }
    }

    private void BtnChallan_Click(object sender, EventArgs e)
    {
        TxtChallan.Text = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "PC", "OPC");
        FillChallanData(TxtChallan.Text);
    }

    private void BtnOrder_Click(object sender, EventArgs e)
    {
        TxtOrder.Text = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "PO", "OPO");
        FillOrderData(TxtOrder.Text);
    }

    private void BtnVendor_Click(object sender, EventArgs e)
    {
        (TxtVendor.Text, _ledgerId) = GetMasterList.GetGeneralLedger(_actionTag, "VENDOR");
        SetLedgerInfo(_ledgerId);
    }

    private void BtnCurrency_Click(object sender, EventArgs e)
    {
        (TxtCurrencyRate.Text, _currencyId, TxtCurrencyRate.Text) = GetMasterList.GetCurrencyList(_actionTag);
    }

    private void BtnSubledger_Click(object sender, EventArgs e)
    {
        (TxtSubledger.Text, _sub_ledgerId) = GetMasterList.GetSubLedgerList(_actionTag);
    }

    private void BtnDepartment_Click(object sender, EventArgs e)
    {
        (TxtDepartment.Text, _departmentId) = GetMasterList.GetDepartmentList(_actionTag);
    }

    private void BtnAgent_Click(object sender, EventArgs e)
    {
        (TxtAgent.Text, _agentId) = GetMasterList.GetAgentList(_actionTag);
    }

    private void BtnBillingTerm_Click(object sender, EventArgs e)
    {
        if (!TxtBillTermAmount.Enabled || DGrid.Rows[0].Cells["GTxtProductId"].Value.GetLong() is 0)
        {
            return;
        }

        var result = new FrmTermCalculation(_dtBillTerm.RowsCount() > 0 ? "UPDATE" : "SAVE",
            LblTotalBasicAmount.Text, false, "PB", 0, 0, _dtBillTerm);
        result.ShowDialog();
        TxtBillTermAmount.Text = result.TotalTermAmount;
        AddToBillingTerm(result.CalcTermTable);
        TxtNetAmount.Focus();
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsLicenseExpire && !Debugger.IsAttached)
        {
            this.NotifyWarning("SOFTWARE LICENSE HAS BEEN EXPIRED..!! CONTACT WITH VENDOR FOR FURTHER..!!");
            return;
        }
        if (IsValidInformation())
        {
            if (SavePurchaseInvoice() != 0)
            {
                PrintVoucher();
                if (CmbBillIn.Text is "Cash" or "Bank")
                {
                    SaveCashAndBankDetails();
                }

                if (_isZoom)
                {
                    Close();
                }

                this.NotifySuccess($@"{TxtVno.Text} PURCHASE INVOICE NUMBER {_actionTag} SUCCESSFULLY..!!");
                ClearControl();
                if (_actionTag != "SAVE")
                {
                    TxtVno.Enabled = true;
                    TxtVno.Focus();
                }

                if (TxtVno.IsValueExits())
                {
                    MskMiti.Focus();
                }
                else
                {
                    TxtVno.Focus();
                }
            }
        }
    }

    private void BtnClear_Click(object sender, EventArgs e)
    {
    }

    private void TxtVno_KeyDown(object sender, KeyEventArgs e)
    {

    }

    private void TxtVno_Validating(object sender, CancelEventArgs e)
    {
        var dtVoucher = _entry.CheckVoucherExitsOrNot("PB_Master", "PB_Invoice", TxtVno.Text);
        if (dtVoucher.RowsCount() > 0 && _actionTag.Equals("SAVE"))
        {
            this.NotifyValidationError(TxtVno, "INVOICE NUMBER ALREADY EXITS..!!");
        }
        else if (dtVoucher.Rows.Count <= 0 && !_actionTag.Equals("SAVE"))
        {
            this.NotifyValidationError(TxtVno, "INVOICE NUMBER NOT EXITS..!!");
        }
    }

    private void MskMiti_Validating(object sender, CancelEventArgs e)
    {
        if (!MskMiti.MaskCompleted && MskMiti.Enabled && TxtVno.IsValueExits())
        {
            this.NotifyValidationError(MskMiti, "VOUCHER MITI IS INVALID..!!");
        }
        else if (MskMiti.MaskCompleted && !MskDate.Text.IsValidDateRange("D"))
        {
            this.NotifyValidationError(MskMiti,
                $"MITI MUST BE BETWEEN [{ObjGlobal.CfStartBsDate} AND {ObjGlobal.CfEndBsDate}]");
        }

        if (MskMiti.MaskCompleted)
        {
            MskDate.GetEnglishDate(MskMiti.Text);
        }
    }

    private void MskDate_Validating(object sender, CancelEventArgs e)
    {
        if (!MskDate.MaskCompleted && MskDate.Enabled && TxtVno.IsValueExits())
        {
            this.NotifyValidationError(MskDate, "VOUCHER DATE IS INVALID..!!");
        }
        else if (MskDate.MaskCompleted && !MskDate.Text.IsValidDateRange("D"))
        {
            this.NotifyValidationError(MskDate,
                $"DATE MUST BE BETWEEN [{ObjGlobal.CfStartAdDate} AND {ObjGlobal.CfEndAdDate}]");
        }

        if (MskDate.MaskCompleted)
        {
            MskMiti.GetNepaliDate(MskDate.Text);
        }
    }

    private void TxtRefVno_KeyDown(object sender, KeyEventArgs e)
    {

    }

    private void CmbInvType_KeyPress(object sender, KeyPressEventArgs e)
    {

    }

    private void CmbBillIn_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Space)
        {
            SendKeys.Send("{F4}");
        }
        else if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void TxtVendor_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnVendor_Click(sender, e);
        }
        else if (e.KeyData is Keys.Enter)
        {
            if (TxtVendor.IsBlankOrEmpty())
            {
                BtnVendor.PerformClick();
            }
            else
            {
                SendKeys.Send("{TAB}");
            }
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            (TxtVendor.Text, _ledgerId) = GetMasterList.CreateGeneralLedger("VENDOR", true);
            SetLedgerInfo(_ledgerId);
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtVendor, BtnVendor);
        }
    }

    private void TxtDueDays_TextChanged(object sender, EventArgs e)
    {
        MskRefDate.Text = TxtDueDays.GetDouble() > 0
            ? MskDate.GetDateTime().AddDays(TxtDueDays.GetDouble()).GetDateString()
            : DateTime.Now.GetDateString();
    }

    private void TxtCurrency_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnCurrency_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            (TxtCurrency.Text, _currencyId, TxtCurrencyRate.Text) = GetMasterList.CreateCurrency(true);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtCurrency.IsBlankOrEmpty() && ObjGlobal.PurchaseCurrencyMandatory)
            {
                this.NotifyValidationError(TxtCurrency, "CURRENCY IS MANDATORY PLEASE SELECT CURRENCY");
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtCurrency, BtnCurrency);
        }
    }

    private void TxtCurrencyRate_TextChanged(object sender, EventArgs e)
    {
        VoucherTotalCalculation();
    }

    private void TxtCurrencyRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            if (TxtSubledger.Enabled)
            {
                TxtSubledger.Focus();
            }
            else if (TxtDepartment.Enabled)
            {
                TxtDepartment.Focus();
            }
            else if (TxtAgent.Enabled)
            {
                TxtAgent.Focus();
            }
            else
            {
                DGrid.Focus();
            }
        }

        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out var isNumber);
        }
    }

    private void TxtSubledger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnSubledger_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            (TxtSubledger.Text, _sub_ledgerId) = GetMasterList.CreateSubLedger(true);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtSubledger.IsBlankOrEmpty() && ObjGlobal.PurchaseSubLedgerMandatory)
            {
                this.NotifyValidationError(TxtSubledger, "SUB LEDGER IS MANDATORY PLEASE SELECT SUBLEDGER");
            }
            else
            {
                if (TxtDepartment.Enabled)
                {
                    TxtDepartment.Focus();
                }
                else if (TxtAgent.Enabled)
                {
                    TxtAgent.Focus();
                }
                else
                {
                    DGrid.Focus();
                }
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtSubledger, BtnSubledger);
        }
    }

    private void TxtDepartment_KeyDown(object sender, KeyEventArgs e)
    {

    }

    private void TxtAgent_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnAgent_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            (TxtAgent.Text, _agentId) = GetMasterList.CreateAgent(true);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtDepartment.IsBlankOrEmpty() && ObjGlobal.PurchaseAgentMandatory)
            {
                this.NotifyValidationError(TxtDepartment, "AGENT IS MANDATORY PLEASE SELECT AGENT");
            }
            else
            {
                DGrid.Focus();
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtAgent, BtnAgent);
        }
    }

    private void DGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is not Keys.Enter || TxtProduct.Enabled)
        {
            return;
        }

        e.SuppressKeyPress = true;
        EnableGridControl(true);
        AdjustControlsInDataGrid();
        if (DGrid.Rows[_rowIndex].Cells["GTxtProduct"].Value.IsValueExits())
        {
            TextFromGrid();
            TxtProduct.Focus();
            return;
        }

        GetSerialNo();
        TxtProduct.Focus();
    }

    private void TxtBillTermAmount_Enter(object sender, EventArgs e)
    {
        BtnBillingTerm.PerformClick();
    }

    private void TxtBillTermAmount_TextChanged(object sender, EventArgs e)
    {
        LblTotalNetAmount.Text =
            (LblTotalBasicAmount.GetDecimal() + TxtBillTermAmount.GetDecimal()).GetDecimalString();
        LblTotalLocalNetAmount.Text = TxtCurrencyRate.GetDecimal() > 1
            ? (LblTotalNetAmount.GetDecimal() * TxtCurrencyRate.GetDecimal()).GetDecimalString()
            : LblTotalNetAmount.Text;
        LblNumberInWords.Text = ClsMoneyConversion.MoneyConversion(LblTotalLocalNetAmount.Text);
    }

    private void TxtBillTermAmount_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (TxtRemarks.Enabled)
            {
                TxtRemarks.Focus();
            }
            else
            {
                BtnSave.Focus();
            }
        }
    }

    private void TxtDueDays_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            if (TxtCurrency.Enabled)
            {
                SendKeys.Send("{TAB}");
            }
            else if (TxtSubledger.Enabled)
            {
                TxtSubledger.Focus();
            }
            else if (TxtDepartment.Enabled)
            {
                TxtDepartment.Focus();
            }
            else if (TxtAgent.Enabled)
            {
                TxtAgent.Focus();
            }
            else
            {
                DGrid.Focus();
            }
        }

        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out var isNumber);
        }
    }

    #endregion --------------- BUTTON CLICK EVENTS ---------------

    #region -------------- OBJECT --------------

    private int _sub_ledgerId;
    private int _currencyId = ObjGlobal.SysCurrencyId;
    private int _departmentId;
    private int _agentId;
    private int _godownId;
    private int _unitId;
    private int _altUnitId;
    private int _rowIndex;
    private int _columnIndex;

    private long _ledgerId;
    private long _productId;

    private bool _isRowDelete;
    private bool _isRowUpdate;
    private readonly bool _isProvision;
    private readonly bool _isZoom;

    private string _actionTag = string.Empty;
    private string _mskOrderDate = string.Empty;
    private string _mskChallanDate = string.Empty;
    private string _numberScheme = string.Empty;
    private string _txtZoomVno = string.Empty;
    private string _invoiceType = string.Empty;
    private readonly string _description = string.Empty;
    private readonly string _batchNo = string.Empty;
    private readonly string _mfgDate = string.Empty;
    private readonly string _expDate = string.Empty;
    private readonly string[] _tagStrings = ["DELETE", "REVERSE"];
    private decimal _conQty;

    private KeyPressEventArgs _getKeys;
    private readonly DataTable _dtPartyInfo = new();
    private DataTable _dtProductTerm = new();
    private DataTable _dtBillTerm = new();
    private readonly IPurchaseEntry _entry = new ClsPurchaseEntry();
    private readonly IFinanceEntry _financeEntry = new ClsFinanceEntry();
    private readonly IMasterSetup _setup = new ClsMasterSetup();
    private readonly IPurchaseDesign _design = new PurchaseEntryDesign();
    private MrGridNumericTextBox TxtSno { get; set; }
    private MrGridTextBox TxtShortName { get; set; }
    private MrGridTextBox TxtProduct { get; set; }
    private MrGridTextBox TxtGodown { get; set; }
    private MrGridTextBox TxtAltUnit { get; set; }
    private MrGridTextBox TxtUnit { get; set; }
    private MrGridNumericTextBox TxtAltQty { get; set; }
    private MrGridNumericTextBox TxtQty { get; set; }
    private MrGridNumericTextBox TxtRate { get; set; }
    private MrGridNumericTextBox TxtBasicAmount { get; set; }
    private MrGridNumericTextBox TxtTermAmount { get; set; }
    private MrGridNumericTextBox TxtNetAmount { get; set; }

    #endregion -------------- OBJECT --------------
}