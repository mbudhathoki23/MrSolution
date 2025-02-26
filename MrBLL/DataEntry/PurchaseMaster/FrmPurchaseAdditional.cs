using DatabaseModule.DataEntry.PurchaseMaster.PurchaseAdditional;
using DevExpress.XtraEditors;
using MrBLL.DataEntry.Common;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.GridControl;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.Interface.PurchaseMaster;
using MrDAL.DataEntry.PurchaseMaster;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MrBLL.DataEntry.PurchaseMaster;

public partial class FrmPurchaseAdditional : MrForm
{
    #region -------------- ADDITIONAL --------------

    public FrmPurchaseAdditional(bool zoom, string txtZoomVno, string invoiceType)
    {
        InitializeComponent();
        // _purchaseEntry = new ClsPurchaseEntry();
        _additional = new PurchaseAdditionalRepository();
        _masterSetup = new ClsMasterSetup();
        _dtProductTerm = _masterSetup.GetBillingTerm();
        _txtZoomVno = txtZoomVno;
        _isZoom = zoom;
        _invoiceType = invoiceType;
        _isPTermExits = _masterSetup.IsAdditionalBillingTermExitsOrNot("PB", "P");
        _isBTermExits = _masterSetup.IsAdditionalBillingTermExitsOrNot("PB", "B");
        DesignGridColumnsAsync();
        ClearControl();
        EnableControl();
        AdjustControlsInDataBillGrid();
        AdjustControlsInDataProductGrid();
    }

    private void FrmPurchaseAdditional_Load(object sender, EventArgs e)
    {
        if (_isZoom && _txtZoomVno.IsValueExits())
        {
            FillInvoiceData(_txtZoomVno);
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }

    private void FrmPurchaseAdditional_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (TabAdditionalTerm.SelectedTab == TabBillWise)
            {
                if (TxtBTermDesc.Enabled)
                {
                    EnableBillGridControl();
                    BillGrid.Focus();
                }
                else
                {
                    if (BillGrid.Rows.Count > 0 && BillGrid.Focused)
                    {
                    }
                    else if (!BtnNew.Enabled)
                    {
                        _actionTag = string.Empty;
                        ClearControl();
                        EnableControl();
                        BtnNew.Focus();
                    }
                    else
                    {
                        BtnExit.PerformClick();
                    }
                }
            }
            else if (TabAdditionalTerm.SelectedTab == TabProductWise)
            {
                if (TxtPTermAmount.Enabled)
                {
                    EnableProductGridControl();
                    ProductGrid.Focus();
                    return;
                }
                if (BillGrid.Rows.Count > 0 && BillGrid.Focused)
                {
                }
                else if (!BtnNew.Enabled)
                {
                    _actionTag = string.Empty;
                    EnableControl();
                    ClearBillDetails();
                    ClearProductDetails();
                }
                else
                {
                    BtnExit.PerformClick();
                }
            }
            else
            {
                BtnExit.PerformClick();
            }
        }
    }

    private void GlobalKeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        ReturnVoucherNo();
        ClearControl();
        EnableControl(true);
        if (TxtVoucherNo.IsValueExits())
        {
            MskMiti.Focus();
        }
        else
        {
            TxtVoucherNo.Focus();
        }
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _actionTag = "UPDATE";
        ReturnVoucherNo();
        ClearControl();
        EnableControl(true);
        if (TxtVoucherNo.IsValueExits())
        {
            MskMiti.Focus();
        }
        else
        {
            TxtVoucherNo.Focus();
        }
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        ReturnVoucherNo();
        ClearControl();
        EnableControl();
        if (TxtVoucherNo.IsValueExits())
        {
            MskMiti.Focus();
        }
        else
        {
            TxtVoucherNo.Focus();
        }
    }

    private void BtnPrintInvoice_Click(object sender, EventArgs e)
    {
        PrintVoucher();
    }

    private void BtnReverse_Click(object sender, EventArgs e)
    {
    }

    private void BtnCopy_Click(object sender, EventArgs e)
    {
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.Question("DO YOU WANT TO EXIT THE FORM..??") is DialogResult.Yes)
        {
            Close();
            return;
        }
    }

    private void TxtAdditionalVoucherNo_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (TxtVoucherNo.IsValueExits())
            {
                GlobalKeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
            }
        }
    }

    private void TxtAdditionalVoucherNo_Validating(object sender, CancelEventArgs e)
    {
    }

    private void BtnVno_Click(object sender, EventArgs e)
    {
        var result = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "PAB");
        if (result.IsValueExits())
        {
            TxtVoucherNo.Text = result;
            if (_actionTag.Equals("SAVE"))
            {
            }
        }
    }

    private void MskMiti_Validating(object sender, CancelEventArgs e)
    {
        if (!MskMiti.MaskCompleted && MskMiti.Enabled && TxtVoucherNo.IsValueExits())
        {
            MskDate.WarningMessage("VOUCHER MITI IS INVALID..!!");
            return;
        }
        if (MskMiti.MaskCompleted && !MskMiti.Text.IsValidDateRange("M"))
        {
            MskDate.WarningMessage($"MITI MUST BE BETWEEN [{ObjGlobal.CfStartBsDate} AND {ObjGlobal.CfEndBsDate}]");
            return;
        }
        MskDate.GetEnglishDate(MskMiti.Text);
    }

    private void MskDate_Validating(object sender, CancelEventArgs e)
    {
        if (!MskDate.MaskCompleted && MskDate.Enabled && TxtVoucherNo.IsValueExits())
        {
            MskDate.WarningMessage("VOUCHER DATE IS INVALID..!!");
            return;
        }
        if (MskDate.MaskCompleted && !MskDate.Text.IsValidDateRange("D"))
        {
            MskDate.WarningMessage($"DATE MUST BE BETWEEN [{ObjGlobal.CfStartAdDate} AND {ObjGlobal.CfEndAdDate}]");
            return;
        }
        if (MskDate.MaskCompleted)
        {
            MskMiti.GetNepaliDate(MskDate.Text);
        }
    }

    private void TxtDepartment_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyData is Keys.F1)
        {
            BtnDepartment_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id) = GetMasterList.CreateDepartment(true);
            if (!description.IsValueExits()) return;
            TxtDepartment.Text = description;
            _departmentId = id;
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtDepartment, BtnDepartment);
        }
    }

    private void BtnDepartment_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetDepartmentList(_actionTag);
        if (!description.IsValueExits()) return;
        TxtDepartment.Text = description;
        _departmentId = id;
    }

    private void TxtDepartment_Validating(object sender, CancelEventArgs e)
    {
        if (TxtDepartment.Enabled && _actionTag.IsValueExits() && ObjGlobal.PurchaseDepartmentMandatory && TxtDepartment.IsBlankOrEmpty())
        {
            TxtDepartment.WarningMessage($"DEPARTMENT IS REQUIRED FOR {_actionTag} THIS VOUCHER");
        }
    }

    private void TxtPurchaseInvoiceNo_Validating(object sender, CancelEventArgs e)
    {
        if (TxtPurchaseInvoiceNo.Enabled && _actionTag.IsValueExits() && TxtVoucherNo.IsValueExits() && TxtPurchaseInvoiceNo.IsBlankOrEmpty())
        {
            TxtPurchaseInvoiceNo.WarningMessage($"PURCHASE INVOICE IS REQUIRED FOR {_actionTag} THIS VOUCHER");
        }
    }

    private void TxtPurchaseInvoiceNo_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnPurchaseInvoice_Click(sender, e);
        }
        else if (e.KeyData is Keys.Enter)
        {
            if (TxtPurchaseInvoiceNo.IsBlankOrEmpty())
            {
                TxtPurchaseInvoiceNo.WarningMessage($"PURCHASE INVOICE IS REQUIRED FOR {_actionTag} THIS VOUCHER");
                return;
            }
            if (TabProductWise.Enabled)
            {
                TabAdditionalTerm.SelectedTab = TabProductWise;
                TabProductWise.Focus();
                ProductGrid.Focus();
                ProductGrid.Rows[0].Cells[0].Selected = true;
            }
            else
            {
                TabAdditionalTerm.SelectedTab = TabBillWise;
                TabBillWise.Focus();
                BillGrid.Focus();
                BillGrid.Rows[0].Cells[0].Selected = true;
            }
        }
        else if (e.Control && e.KeyCode is Keys.V)
        {
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtVoucherNo, BtnPurchaseInvoice);
        }
    }

    private void BtnPurchaseInvoice_Click(object sender, EventArgs e)
    {
        var result = GetTransactionList.GetTransactionVoucherNo(_actionTag, "PB", MskDate.Text);
        if (result.IsValueExits())
        {
            TxtPurchaseInvoiceNo.Text = result;
            FillInvoiceData(result);
            TxtPurchaseInvoiceNo.Focus();
        }
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
            if (SavePurchaseAdditionalTermInvoice() != 0)
            {
                CustomMessageBox.ActionError(TxtVoucherNo.Text, "PURCHASE ADDITIONAL NUMBER", _actionTag);
                ClearControl();
                if (TxtVoucherNo.Enabled && TxtVoucherNo.IsBlankOrEmpty())
                {
                    TxtVoucherNo.Focus();
                }
                else
                {
                    MskMiti.Focus();
                }
            }
            else
            {
                CustomMessageBox.Warning(@$"ERROR OCCURS WHILE ADDITIONAL VOUCHER NUMBER {TxtVoucherNo.TextBoxType} {_actionTag}");
            }
        }
        else
        {
            return;
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        ClearControl();
    }

    private void TxtPTermAmount_Validating(object sender, CancelEventArgs e)
    {
        if (TxtProduct.IsBlankOrEmpty() || _productId is 0)
        {
            CustomMessageBox.Warning("SELECTED PRODUCT IS INVALID");
            return;
        }
        AddTextToProductGrid(true);
        EnableProductGridControl();
        ProductGrid.Focus();
    }

    private void TxtPTermAmountOnEnter(object sender, EventArgs e)
    {
        if (!TxtProduct.IsValueExits() || !TxtPTermAmount.Enabled)
        {
            return;
        }
        var existingTerm = new DataTable();
        var serialNo = 0;

        if (ProductGrid.CurrentRow != null)
        {
            serialNo = ProductGrid.CurrentRow.Cells["GTxtSNo"].Value.GetInt();
            var exDetails = _dtProductTerm.AsEnumerable().Any(c => c.Field<string>("ProductId").Equals(_productId.ToString().Trim()));
            if (exDetails)
            {
                var exitAny = _dtProductTerm.AsEnumerable().Any(c => c.Field<string>("ProductId").Equals(_productId.ToString()) && c.Field<string>("ProductSno").Equals(serialNo.GetString()));
                if (exitAny)
                {
                    existingTerm = _dtProductTerm.Select($"ProductId = '{_productId}'").CopyToDataTable();
                    if (existingTerm.Rows.Count is 0)
                    {
                        existingTerm = _dtProductTerm.Select($" ProductId = '{_productId}'").CopyToDataTable();
                    }
                    else if (existingTerm.Rows.Count > 1)
                    {
                        var any = _dtProductTerm.AsEnumerable().Any(c => c.Field<string>("ProductId").Equals(_productId.ToString()) && c.Field<string>("ProductSno").Equals(serialNo.GetString()));
                        if (any)
                        {
                            existingTerm = _dtProductTerm.Select($"ProductSno = '{serialNo}' and ProductId = '{_productId}'").CopyToDataTable();
                        }
                    }
                }
            }
        }
        var tag = _actionTag == "SAVE" && !_isRowUpdate ? "SAVE" : "UPDATE";

        var result = new FrmAdditionalTerms(existingTerm, TxtPBasicAmount.GetDecimal());
        result.ShowDialog();
        var basicAmount = TxtPBasicAmount.GetDecimal();
        var termAmount = TxtPTermAmount.GetDecimal();
        TxtPNetAmount.Text = (basicAmount + termAmount).GetDecimalString();
        AddToProductTerm(result.TermCalculation);
        TxtPNetAmount.Text = (TxtPBasicAmount.GetDecimal() + TxtPTermAmount.GetDecimal()).GetDecimalString();
        TxtPTermAmount.Focus();
    }

    private void TxtPTermAmount_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void ProductGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter && !TxtPTermAmount.Enabled)
        {
            e.SuppressKeyPress = true;
            if (ProductGrid.CurrentRow != null && ProductGrid.CurrentRow.Cells["GTxtProduct"].Value.IsValueExits())
            {
                EnableProductGridControl(true);
                TextFromProductGrid();
                ProductGrid.CurrentCell = ProductGrid.CurrentRow?.Cells[0];
                AdjustControlsInDataProductGrid();
                TxtPTermAmount.Focus();
                return;
            }
            else
            {
                TabAdditionalTerm.SelectedTab = TabBillWise;
                BillGrid.Focus();
                ProductGrid.CurrentCell = ProductGrid.Rows[0].Cells[0];
            }
        }
    }

    private void ProductGrid_EnterKeyPressed(object sender, EventArgs e)
    {
        ProductGrid_KeyDown(sender, new KeyEventArgs(Keys.Enter));
    }

    private void BillGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Delete && BillGrid.RowCount > 0)
        {
            if (BillGrid.CurrentRow != null)
            {
                var sno = BillGrid.CurrentRow.Cells["GTxtTermSno"].Value.GetInt();
                var termId = BillGrid.CurrentRow.Cells["GTxtTermId"].Value.GetLong();

                if (termId is 0)
                {
                    return;
                }
                if (CustomMessageBox.DeleteRow() is DialogResult.No)
                {
                    return;
                }
                _rowDelete = true;
                DeletedRowExitsOrNot(sno, termId);
            }

            if (BillGrid.CurrentRow != null)
            {
                BillGrid.Rows.RemoveAt(BillGrid.CurrentRow.Index);
            }
            if (BillGrid.RowCount is 0)
            {
                BillGrid.Rows.Add();
            }
            BillVoucherTotalCalculation();
            GetBillSerialNo();
        }

        if (e.KeyCode is Keys.Enter && !TxtBTermDesc.Enabled)
        {
            e.SuppressKeyPress = true;
            EnableBillGridControl(true);
            BillGrid.CurrentCell = BillGrid.CurrentRow?.Cells[0];
            AdjustControlsInDataBillGrid();
            if (BillGrid.CurrentRow != null && BillGrid.Rows[BillGrid.CurrentRow.Index].Cells["GTxtTerm"].Value.IsValueExits())
            {
                TextFromBillGrid();
                TxtBTermDesc.Focus();
                return;
            }
            GetBillSerialNo();
            TxtBTermDesc.Focus();
        }
    }

    private void BillGrid_EnterKeyPressed(object sender, EventArgs e)
    {
        BillGrid_KeyDown(sender, new KeyEventArgs(Keys.Enter));
    }

    private void TxtBTermAmount_Validated(object sender, EventArgs e)
    {
        if (TxtBTermAmount.Focused)
        {
            TxtBTermAmount.Text = TxtBTermAmount.GetDecimalString();
            TxtBTermAmount_TextChanged(sender, EventArgs.Empty);
            if (TxtBTermAmount.GetDecimal() > 0)
            {
                AddTextToBillGrid(false);
                AdjustControlsInDataBillGrid();
                TxtBTermDesc.Focus();
                return;
            }
        }
    }

    private void TxtBTermAmount_TextChanged(object sender, EventArgs e)
    {
        var amount = TxtLocalInvoiceAmount.GetDecimal();
        if (BillGrid.CurrentRow != null)
        {
            for (var i = 0; i < BillGrid.CurrentRow.Index; i++)
            {
                var sign = BillGrid.Rows[i].Cells["GTxtSign"].Value.GetString();
                if (sign.Equals("-"))
                {
                    amount -= BillGrid.Rows[i].Cells["GTxtAmount"].Value.GetDecimal();
                }
                else if (sign.Equals("+"))
                {
                    amount += BillGrid.Rows[i].Cells["GTxtAmount"].Value.GetDecimal();
                }
            }
            amount = TxtBTermSign.Text switch
            {
                "-" => amount - TxtBTermAmount.GetDecimal(),
                _ => amount - TxtBTermAmount.GetDecimal()
            };
        }
        TxtBNetAmount.Text = amount.GetDecimalString();
    }

    private void TxtBTermRate_Validated(object sender, EventArgs e)
    {
        TxtBTermRate.Text = TxtBTermRate.GetDecimalString();
        TxtBTermRate_TextChanged(sender, EventArgs.Empty);
    }

    private void TxtBTermRate_TextChanged(object sender, EventArgs e)
    {
        decimal basicAmount = 0;
        if (BillGrid.CurrentRow != null)
        {
            basicAmount = BillGrid.CurrentRow.Index <= 0
                ? TxtLocalInvoiceAmount.GetDecimal()
                : BillGrid.Rows[BillGrid.CurrentRow.Index - 1].Cells["GTxtAmount"].Value.GetDecimal();
        }
        var billTermRate = TxtBTermRate.GetDecimal();
        var amount = billTermRate switch
        {
            > 0 => billTermRate * basicAmount / 100,
            _ => 0
        };
        TxtBTermAmount.Text = amount.GetDecimalString();
        TxtBTermAmount_TextChanged(sender, e);
    }

    private void TxtBSubLedger_Validated(object sender, EventArgs e)
    {
    }

    private void TxtBSubLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            OpenSubLedgerList();
        }
        else if (e.KeyCode is Keys.Enter)
        {
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtBSubLedger, OpenSubLedgerList);
        }
    }

    private void TxtBLedger_Validated(object sender, EventArgs e)
    {
    }

    private void TxtBLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            OpenGeneralLedger();
        }
        else if (e.KeyCode is Keys.Enter)
        {
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtBLedger, OpenGeneralLedger);
        }
    }

    private void TxtBTerm_Validated(object sender, EventArgs e)
    {
        if (BillGrid.CurrentRow != null && BillGrid.RowCount > 0 && BillGrid.Rows[BillGrid.CurrentRow.Index].Cells["GTxtTermId"].Value.GetInt() is 0)
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

    private void TxtBTerm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            OpenPurchaseTermList();
        }
        else if (e.KeyCode is Keys.Enter)
        {
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtBTermDesc, OpenPurchaseTermList);
        }
    }

    #endregion -------------- ADDITIONAL --------------

    // METHOD FOR THIS FORM

    #region --------------- METHOD FOR THIS FORM ---------------

    private int SavePurchaseAdditionalTermInvoice()
    {
        try
        {
            if (_actionTag is "SAVE" && TxtVoucherNo.IsDuplicate("PAB"))
            {
                TxtVoucherNo.Text = TxtVoucherNo.GetCurrentVoucherNo("PAB", _numberScheme);
            }
            _additional.PabMaster.PB_Invoice = TxtVoucherNo.Text;
            _additional.PabMaster.Invoice_Date = MskDate.Text.GetDateTime();
            _additional.PabMaster.Invoice_Miti = MskDate.Text;
            _additional.PabMaster.Invoice_Time = DateTime.Now;
            _additional.PabMaster.Cls1 = _departmentId;
            _additional.PabMaster.Cls2 = 0;
            _additional.PabMaster.Cls3 = 0;
            _additional.PabMaster.Cls4 = 0;
            _additional.PabMaster.Agent_Id = 0;
            _additional.PabMaster.PB_Invoice = TxtPurchaseInvoiceNo.Text;
            _additional.PabMaster.PB_Date = MskInvoiceDate.Text.GetDateTime();
            _additional.PabMaster.PB_Miti = MskInvoiceMiti.Text;
            _additional.PabMaster.PB_Qty = 0;
            _additional.PabMaster.PB_Amount = TxtInvoiceAmount.GetDecimal();
            _additional.PabMaster.LocalAmount = TxtLocalInvoiceAmount.GetDecimal();
            _additional.PabMaster.Cur_Id = _currencyId;
            _additional.PabMaster.Cur_Rate = TxtCurrencyRate.GetDecimal();
            _additional.PabMaster.T_Amount = LblBillTermAmount.GetDecimal();
            _additional.PabMaster.Remarks = TxtRemarks.Text;
            _additional.PabMaster.Action_Type = _actionTag;
            _additional.PabMaster.R_Invoice = false;
            _additional.PabMaster.No_Print = 0;
            _additional.PabMaster.In_Words = LblNumberInWords.Text;

            if (BillGrid.RowCount <= 0)
            {
                return _additional.SavePurchaseInvoice(_actionTag);
            }

            foreach (DataGridViewRow viewRow in BillGrid.Rows)
            {
                var list = new PAB_Details();
                var detailsProduct = viewRow.Cells["GTxtProductId"].Value.GetLong();
                if (detailsProduct is 0)
                {
                    continue;
                }
                list.PAB_Invoice = TxtVoucherNo.Text;
                list.SNo = viewRow.Cells["GTxtSNo"].Value.GetInt();
                list.PT_Id = viewRow.Cells["GTxtPTId"].Value.GetInt();
                list.Ledger_Id = viewRow.Cells["GTxtLedgerId"].Value.GetLong();
                list.CBLedger_Id = viewRow.Cells["GTxtCBLedgerId"].Value.GetLong();
                list.Subledger_Id = viewRow.Cells["GTxtSubledgerId"].Value.GetInt();
                list.Agent_Id = viewRow.Cells["GTxtAgentId"].Value.GetInt();
                list.DepartmentId = viewRow.Cells["GTxtDepartmentId"].Value.GetInt();
                list.Product_Id = viewRow.Cells["GTxtProductId"].Value.GetLong();
                list.Percentage = viewRow.Cells["GTxtPercentage"].Value.GetDecimal();
                list.Amount = viewRow.Cells["GTxtAmount"].Value.GetDecimal();
                list.N_Amount = viewRow.Cells["GTxtNAmount"].Value.GetDecimal();
                list.Term_Type = viewRow.Cells["GTxtTermType"].Value.GetString();
                list.PAB_Narration = viewRow.Cells["GTxtPABNarration"].Value.GetString();
                list.SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
                list.SyncGlobalId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
                list.SyncOriginId = ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.IsValueExits() ? ObjGlobal.LocalOriginId.Value : Guid.Empty;
                list.SyncCreatedOn = DateTime.Now;
                list.SyncLastPatchedOn = DateTime.Now;
                list.SyncRowVersion = _additional.PabMaster.SyncRowVersion;
                list.FiscalYearId = viewRow.Cells["GTxtFiscalYearId"].Value.GetInt();
                _additional.DetailsList.Add(list);
            }

            if (BillGrid.RowCount > 0 && BillGrid.Rows[BillGrid.RowCount - 1].Cells["GTxtProductId"].Value.GetLong() is 0)
            {
                BillGrid.Rows.RemoveAt(BillGrid.RowCount - 1);
            }

            return _additional.SavePurchaseInvoice(_actionTag);
        }
        catch (Exception ex)
        {
            ex.DialogResult();
            return 0;
        }
    }

    private void ReturnVoucherNo()
    {
        var dt = _masterSetup.IsExitsCheckDocumentNumbering("PAB");
        if (dt?.Rows.Count is 1)
        {
            _numberScheme = dt.Rows[0]["DocDesc"].ToString();
            TxtVoucherNo.Text = TxtVoucherNo.GetCurrentVoucherNo("PAB", _numberScheme);
        }
        else if (dt.Rows.Count > 1)
        {
            using var wnd = new FrmNumberingScheme("PAB");
            if (wnd.ShowDialog() != DialogResult.OK) return;
            if (string.IsNullOrEmpty(wnd.VNo)) return;
            _numberScheme = wnd.Description;
            TxtVoucherNo.Text = wnd.VNo;
        }
    }

    private void ClearControl()
    {
        Text = !string.IsNullOrEmpty(_actionTag) ? $"PURCHASE ADDITIONAL INVOICE DETAILS [{_actionTag}]" : "PURCHASE ADDITIONAL INVOICE DETAILS";
        TxtVoucherNo.ReadOnly = !string.IsNullOrEmpty(_actionTag) && _actionTag != "SAVE";
        TxtVoucherNo.Clear();
        if (_actionTag.Equals("SAVE"))
        {
            TxtVoucherNo.GetCurrentVoucherNo("PAB", _numberScheme);
        }
        if (BtnNew.Enabled)
        {
            MskDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            MskMiti.Text = DateTime.Now.GetNepaliDate();
            MskInvoiceMiti.Text = MskMiti.Text;
            MskInvoiceDate.Text = MskDate.Text;
        }
        _ledgerId = _departmentId = 0;
        TxtPurchaseInvoiceNo.Clear();
        TxtDepartment.Clear();
        TxtCurrencyRate.Clear();
        TabAdditionalTerm.SelectedTab = _dtProductTerm.Rows.Count > 0 ? TabProductWise : TabBillWise;

        PAttachment1.Image = null;
        PAttachment2.Image = null;
        PAttachment3.Image = null;
        PAttachment4.Image = null;
        PAttachment5.Image = null;

        _dtProductTerm.Clear();
        TxtRemarks.Clear();
        LblTotalBasicAmount.IsClear();
        LblTotalNetAmount.IsClear();
        LblTotalProductTerm.Clear();

        if (_dtProductTerm.RowsCount() > 0)
        {
            _dtProductTerm.Rows.Clear();
        }
        BillGrid.Rows.Clear();
        ProductGrid.Rows.Clear();
        ClearProductDetails();
        ClearBillDetails();
        BillGrid.ClearSelection();
        ProductGrid.ClearSelection();
    }

    private void ClearProductDetails()
    {
        _isRowUpdate = false;
        _productId = 0;
        TxtProduct.Clear();
        ProductVoucherTotalCalculation();
        AdjustControlsInDataProductGrid();
    }

    private void ClearBillDetails()
    {
        if (BillGrid.RowCount is 0)
        {
            BillGrid.Rows.Add();
            GetBillSerialNo();
        }

        _termId = 0;
        TxtBTermDesc.Clear();
        _ledgerId = 0;
        TxtBLedger.Clear();
        _subledgerId = 0;
        TxtBSubLedger.Clear();
        TxtBTermRate.Clear();
        TxtBTermAmount.Clear();
        TxtBTermType.Clear();
        TxtBTermSign.Clear();
        TxtBNetAmount.Clear();
        BillVoucherTotalCalculation();
    }

    private void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = BtnReverse.Enabled = !isEnable && !_tagStrings.Contains(_actionTag);
        TxtVoucherNo.Enabled = BtnVno.Enabled = isEnable || _tagStrings.Contains(_actionTag);
        MskDate.Enabled = MskMiti.Enabled = isEnable;
        TxtCurrency.Enabled = TxtCurrencyRate.Enabled = false;
        TxtPurchaseInvoiceNo.Enabled = BtnPurchaseInvoice.Enabled = isEnable;
        MskInvoiceDate.Enabled = MskInvoiceMiti.Enabled = false;
        TxtInvoiceAmount.Enabled = false;
        TxtLocalInvoiceAmount.Enabled = TxtCurrency.Enabled = TxtCurrencyRate.Enabled = TxtInvoiceAmount.Enabled;
        TxtDepartment.Enabled = BtnDepartment.Enabled = isEnable && ObjGlobal.PurchaseDepartmentEnable;
        TxtRemarks.Enabled = isEnable && ObjGlobal.PurchaseRemarksEnable || _tagStrings.Contains(_actionTag);
        BtnSave.Enabled = BtnCancel.Enabled = isEnable || _tagStrings.Contains(_actionTag);
        TabAdditionalTerm.Enabled = isEnable;
        EnableBillGridControl();
        EnableProductGridControl();
        AdjustControlsInDataProductGrid();
        AdjustControlsInDataBillGrid();
    }

    private void EnableBillGridControl(bool isEnable = false)
    {
        TxtBTermSno.Enabled = false;
        TxtBTermSno.Visible = isEnable;
        TxtBTermDesc.Enabled = TxtBTermDesc.Visible = isEnable;
        TxtBLedger.Enabled = TxtBLedger.Visible = isEnable;
        TxtBSubLedger.Enabled = TxtBSubLedger.Enabled = isEnable && ObjGlobal.PurchaseSubLedgerEnable;
        TxtBTermType.Enabled = false;
        TxtBTermType.Visible = isEnable;
        TxtBTermSign.Enabled = false;
        TxtBTermSign.Visible = isEnable;
        TxtBTermRate.Enabled = TxtBTermRate.Visible = isEnable;
        TxtBTermAmount.Enabled = TxtBTermAmount.Visible = isEnable;
        TxtBNetAmount.Enabled = false;
        TxtBNetAmount.Visible = isEnable;
    }

    private void EnableProductGridControl(bool isEnable = false)
    {
        TxtPTermSno.Enabled = false;
        TxtPTermSno.Visible = isEnable;
        TxtProduct.Enabled = false;
        TxtProduct.Visible = isEnable;
        TxtPBasicQty.Enabled = false;
        TxtPBasicQty.Visible = false;
        TxtPBasicAmount.Enabled = false;
        TxtPBasicAmount.Visible = isEnable;
        TxtPTermAmount.Enabled = TxtPTermAmount.Visible = isEnable;
        TxtPNetAmount.Enabled = false;
        TxtPNetAmount.Visible = isEnable;
    }

    private void PrintVoucher()
    {
        var dtDesign = _masterSetup.GetPrintVoucherList("PAB");
        if (dtDesign.Rows.Count <= 0)
        {
            return;
        }
    }

    private void TextFromProductGrid()
    {
        if (ProductGrid.CurrentRow is null)
        {
            return;
        }
        var index = ProductGrid.CurrentRow.Index;
        TxtPTermSno.Text = ProductGrid.Rows[index].Cells["GTxtSNo"].Value.GetIntString();
        _productId = ProductGrid.Rows[index].Cells["GTxtProductId"].Value.GetLong();
        TxtProduct.Text = ProductGrid.Rows[index].Cells["GTxtProduct"].Value.GetString();
        TxtPBasicQty.Text = ProductGrid.Rows[index].Cells["GTxtBasicQty"].Value.GetDecimalQtyString();
        TxtPBasicAmount.Text = ProductGrid.Rows[index].Cells["GTxtBasicAmount"].Value.GetDecimalString();
        TxtPTermAmount.Text = ProductGrid.Rows[index].Cells["GTxtTermAmount"].Value.GetDecimalString();
        TxtPNetAmount.Text = ProductGrid.Rows[index].Cells["GTxtNetAmount"].Value.GetDecimalString();
        _isRowUpdate = true;
    }

    private void TextFromBillGrid()
    {
        if (BillGrid.CurrentRow is null)
        {
            return;
        }
        var index = BillGrid.CurrentRow.Index;
        TxtBTermSno.Text = BillGrid.Rows[index].Cells["GTxtTermSno"].Value.GetIntString();
        _termId = BillGrid.Rows[index].Cells["GTxtTermId"].Value.GetInt();
        TxtBTermDesc.Text = BillGrid.Rows[index].Cells["GTxtTerm"].Value.GetString();
        TxtBTermDesc.Text = BillGrid.Rows[index].Cells["GTxtLedgerId"].Value.GetString();
        TxtBTermDesc.Text = BillGrid.Rows[index].Cells["GTxtLedger"].Value.GetString();
        TxtBTermDesc.Text = BillGrid.Rows[index].Cells["GTxtSubledgerId"].Value.GetString();
        TxtBTermDesc.Text = BillGrid.Rows[index].Cells["GTxtSubLedger"].Value.GetString();
        TxtBTermDesc.Text = BillGrid.Rows[index].Cells["GTxtTermType"].Value.GetString();
        TxtBTermSign.Text = BillGrid.Rows[index].Cells["GTxtSign"].Value.GetString();
        TxtBTermRate.Text = BillGrid.Rows[index].Cells["GTxtRate"].Value.GetDecimalString();
        TxtBTermAmount.Text = BillGrid.Rows[index].Cells["GTxtAmount"].Value.GetDecimalString();
        TxtInvoiceAmount.Text = BillGrid.Rows[index].Cells["GTxtInvoiceNetAmount"].Value.GetDecimalString();
        _isRowUpdate = true;
    }

    private void GetBillSerialNo()
    {
        if (BillGrid.RowCount <= 0)
        {
            return;
        }
        for (var i = 0; i < BillGrid.RowCount; i++)
        {
            TxtBTermSno.Text = (i + 1).ToString();
            BillGrid.Rows[i].Cells["GTxtTermSno"].Value = TxtBTermSno.Text;
        }
    }

    private void ProductVoucherTotalCalculation()
    {
        if (ProductGrid.RowCount <= 0 || !ProductGrid.Rows[0].Cells["GTxtProduct"].Value.IsValueExits())
        {
            return;
        }
        LblTotalQty.Text = ProductGrid.Rows.OfType<DataGridViewRow>().Sum(row => row.Cells["GTxtBasicQty"].Value.GetDecimal()).GetDecimalString();
        LblTotalBasicAmount.Text = ProductGrid.Rows.OfType<DataGridViewRow>().Sum(row => row.Cells["GTxtBasicAmount"].Value.GetDecimal()).GetDecimalString();
        LblTotalProductTerm.Text = ProductGrid.Rows.OfType<DataGridViewRow>().Sum(row => row.Cells["GTxtTermAmount"].Value.GetDecimal()).GetDecimalString();
        LblTotalNetAmount.Text = (LblTotalBasicAmount.GetDecimal() + LblTotalProductTerm.GetDecimal()).GetDecimalString();
    }

    private void BillVoucherTotalCalculation()
    {
        if (BillGrid.RowCount <= 0 || !BillGrid.Rows[0].Cells["GTxtTerm"].Value.IsValueExits())
        {
            return;
        }
        LblTotalBasicAmount.Text = BillGrid.Rows.OfType<DataGridViewRow>().Sum(row => row.Cells["GTxtAmount"].Value.GetDecimal()).GetDecimalString();
        foreach (DataGridViewRow row in BillGrid.Rows)
        {
            var index = BillGrid.Rows.IndexOf(row);
            var amount = row.Cells["GTxtInvoiceNetAmount"].Value.GetDecimal();
            amount += row.Cells["GTxtAmount"].Value.GetDecimal();
            row.Cells["GTxtInvoiceNetAmount"].Value = amount;
        }
        LblTotalNetAmount.Text = (LblTotalBasicAmount.GetDecimal() + LblTotalProductTerm.GetDecimal()).GetDecimalString();
    }

    private void AdjustControlsInDataBillGrid()
    {
        if (BillGrid.CurrentRow == null)
        {
            return;
        }
        var currentRow = BillGrid.CurrentRow.Index;
        var columnIndex = BillGrid.Columns["GTxtTermSno"]!.Index;
        TxtBTermSno.Size = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtBTermSno.Location = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtBTermSno.TabIndex = columnIndex;

        columnIndex = BillGrid.Columns["GTxtTerm"]!.Index;
        TxtBTermDesc.Size = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtBTermDesc.Location = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtBTermDesc.TabIndex = columnIndex;

        columnIndex = BillGrid.Columns["GTxtLedger"]!.Index;
        TxtBLedger.Size = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtBLedger.Location = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtBLedger.TabIndex = columnIndex;

        columnIndex = BillGrid.Columns["GTxtSubLedger"]!.Index;
        TxtBSubLedger.Size = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtBSubLedger.Location = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtBSubLedger.TabIndex = columnIndex;

        columnIndex = BillGrid.Columns["GTxtTermType"]!.Index;
        TxtBTermType.Size = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtBTermType.Location = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtBTermType.TabIndex = columnIndex;

        columnIndex = BillGrid.Columns["GTxtSign"]!.Index;
        TxtBTermSign.Size = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtBTermSign.Location = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtBTermSign.TabIndex = columnIndex;

        columnIndex = BillGrid.Columns["GTxtRate"]!.Index;
        TxtBTermRate.Size = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtBTermRate.Location = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtBTermRate.TabIndex = columnIndex;

        columnIndex = BillGrid.Columns["GTxtAmount"]!.Index;
        TxtBTermAmount.Size = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtBTermAmount.Location = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtBTermAmount.TabIndex = columnIndex;

        columnIndex = BillGrid.Columns["GTxtInvoiceNetAmount"]!.Index;
        TxtBNetAmount.Size = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtBNetAmount.Location = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtBNetAmount.TabIndex = columnIndex;
    }

    private void AdjustControlsInDataProductGrid()
    {
        if (ProductGrid.CurrentRow == null)
        {
            return;
        }
        var currentRow = ProductGrid.CurrentRow.Index;
        var columnIndex = ProductGrid.Columns["GTxtSNo"]!.Index;
        TxtPTermSno.Size = ProductGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtPTermSno.Location = ProductGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtPTermSno.TabIndex = columnIndex;

        columnIndex = ProductGrid.Columns["GTxtProduct"]!.Index;
        TxtProduct.Size = ProductGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtProduct.Location = ProductGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtProduct.TabIndex = columnIndex;

        columnIndex = ProductGrid.Columns["GTxtBasicQty"]!.Index;
        TxtPBasicQty.Size = ProductGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtPBasicQty.Location = ProductGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtPBasicQty.TabIndex = columnIndex;

        columnIndex = ProductGrid.Columns["GTxtBasicAmount"]!.Index;
        TxtPBasicAmount.Size = ProductGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtPBasicAmount.Location = ProductGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtPBasicAmount.TabIndex = columnIndex;

        columnIndex = ProductGrid.Columns["GTxtTermAmount"]!.Index;
        TxtPTermAmount.Size = ProductGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtPTermAmount.Location = ProductGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtPTermAmount.TabIndex = columnIndex;

        columnIndex = ProductGrid.Columns["GTxtNetAmount"]!.Index;
        TxtPNetAmount.Size = ProductGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtPNetAmount.Location = ProductGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtPNetAmount.TabIndex = columnIndex;
    }

    private void DesignGridColumnsAsync()
    {
        TxtPTermSno = new MrGridNumericTextBox(ProductGrid)
        {
            ReadOnly = true,
            TextAlign = HorizontalAlignment.Center
        };
        TxtProduct = new MrGridTextBox(ProductGrid)
        {
            ReadOnly = true
        };
        TxtPBasicQty = new MrGridNumericTextBox(ProductGrid)
        {
            ReadOnly = true
        };
        TxtPBasicAmount = new MrGridNumericTextBox(ProductGrid)
        {
            ReadOnly = true
        };
        TxtPTermAmount = new MrGridNumericTextBox(ProductGrid)
        {
            ReadOnly = false
        };
        TxtPTermAmount.Enter += TxtPTermAmountOnEnter;
        TxtPTermAmount.KeyPress += TxtPTermAmount_KeyPress;
        TxtPTermAmount.Validating += TxtPTermAmount_Validating;
        TxtPNetAmount = new MrGridNumericTextBox(ProductGrid);

        TxtBTermSno = new MrGridNumericTextBox(BillGrid)
        {
            ReadOnly = true,
            TextAlign = HorizontalAlignment.Center
        };
        TxtBTermDesc = new MrGridTextBox(BillGrid)
        {
            ReadOnly = true
        };
        TxtBTermDesc.KeyDown += TxtBTerm_KeyDown;
        TxtBTermDesc.Validated += TxtBTerm_Validated;

        TxtBLedger = new MrGridTextBox(BillGrid)
        {
            ReadOnly = true
        };
        TxtBLedger.KeyDown += TxtBLedger_KeyDown;
        TxtBLedger.Validated += TxtBLedger_Validated;

        TxtBSubLedger = new MrGridTextBox(BillGrid)
        {
            ReadOnly = true
        };
        TxtBSubLedger.KeyDown += TxtBSubLedger_KeyDown;
        TxtBSubLedger.Validated += TxtBSubLedger_Validated;

        TxtBTermType = new MrGridTextBox(BillGrid)
        {
            ReadOnly = true
        };
        TxtBTermSign = new MrGridTextBox(BillGrid)
        {
            ReadOnly = true
        };
        TxtBTermRate = new MrGridNumericTextBox(BillGrid);
        TxtBTermRate.TextChanged += TxtBTermRate_TextChanged;
        TxtBTermRate.Validated += TxtBTermRate_Validated;

        TxtBTermAmount = new MrGridNumericTextBox(BillGrid);
        TxtBTermAmount.TextChanged += TxtBTermAmount_TextChanged;
        TxtBTermAmount.Validated += TxtBTermAmount_Validated;

        TxtBNetAmount = new MrGridNumericTextBox(BillGrid)
        {
            ReadOnly = true
        };
        ObjGlobal.DGridColorCombo(BillGrid);
        AdjustControlsInDataBillGrid();
    }

    private void AddToProductTerm(DataTable dtTerm)
    {
        if (dtTerm.RowsCount() <= 0)
        {
            return;
        }

        var serialNo = 0;
        if (ProductGrid.CurrentRow == null)
        {
            return;
        }
        serialNo = ProductGrid.CurrentRow.Cells["GTxtSno"].Value.GetInt();
        var exDetails = _dtProductTerm.AsEnumerable().Any(c => c.Field<string>("ProductId").Equals(_productId.ToString()));
        if (exDetails)
        {
            var exitAny = _dtProductTerm.AsEnumerable().Any(c => c.Field<string>("ProductId").Equals(_productId.ToString()) && c.Field<string>("ProductSno").Equals(serialNo.ToString().Trim()));
            if (exitAny)
            {
                foreach (DataRow ro in dtTerm.Rows)
                {
                    foreach (DataRow row in _dtProductTerm.Rows)
                    {
                        if (row["ProductSno"] == ro["GTxtProductSno"] && row["ProductId"] == ro["GTxtProductId"])
                        {
                            var index = _dtProductTerm.Rows.IndexOf(row);
                            _dtProductTerm.Rows[index].SetField("TermRate", ro["GTxtRate"]);
                            _dtProductTerm.Rows[index].SetField("TermAmt", ro["GTxtValueAmount"]);
                            _dtProductTerm.AcceptChanges();
                        }
                    }
                }
            }
            else
            {
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
                    dataRow["TermCondition"] = ro["GTxtTermCondition"];
                    dataRow["TermType"] = ro["GTxtTermType"];
                    dataRow["TermRate"] = ro["GTxtRate"];
                    dataRow["TermAmt"] = ro["GTxtValueAmount"];
                    dataRow["Source"] = "PB";
                    dataRow["Formula"] = string.Empty;
                    dataRow["ProductSno"] = ro["GTxtProductSno"];
                    _dtProductTerm.Rows.InsertAt(dataRow, _dtProductTerm.RowsCount() + 1);
                }
            }
        }
        else
        {
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
                dataRow["TermCondition"] = ro["GTxtTermCondition"];
                dataRow["TermType"] = ro["GTxtTermType"];
                dataRow["TermRate"] = ro["GTxtRate"];
                dataRow["TermAmt"] = ro["GTxtValueAmount"];
                dataRow["Source"] = "PB";
                dataRow["Formula"] = string.Empty;
                dataRow["ProductSno"] = ro["GTxtProductSno"];
                _dtProductTerm.Rows.InsertAt(dataRow, _dtProductTerm.RowsCount() + 1);
            }
        }
    }

    private void FillInvoiceData(string voucherNo)
    {
        try
        {
            var dsPurchase = _additional.ReturnPurchaseInvoiceDetailsInDataSet(voucherNo);
            var dtMaster = dsPurchase.Tables[0];
            var dtDetails = dsPurchase.Tables[1];
            if (dtMaster.Rows.Count <= 0)
            {
                return;
            }
            foreach (DataRow dr in dtMaster.Rows)
            {
                TxtPurchaseInvoiceNo.Text = dr["PB_Invoice"].ToString();
                MskMiti.Text = dr["Invoice_Miti"].ToString();
                MskDate.Text = dr["Invoice_Date"].GetDateString();
                TxtCurrency.Text = dr["Ccode"].ToString();
                TxtCurrencyRate.Text = dr["Cur_Rate"].GetDecimalString(true);
                TxtInvoiceAmount.Text = dr["N_Amount"].GetDecimalString();
                TxtLocalInvoiceAmount.Text = dr["LN_Amount"].GetDecimalString();
                TxtRemarks.Text = Convert.ToString(dr["Remarks"].ToString());
            }
            if (dtDetails.Rows.Count > 0)
            {
                var iRows = 0;
                ProductGrid.Rows.Clear();
                ProductGrid.Rows.Add(dtDetails.Rows.Count + 1);
                foreach (DataRow dr in dtDetails.Rows)
                {
                    ProductGrid.Rows[iRows].Cells["GTxtSNo"].Value = dr["Invoice_SNo"].ToString();
                    ProductGrid.Rows[iRows].Cells["GTxtProductId"].Value = dr["P_Id"].ToString();
                    ProductGrid.Rows[iRows].Cells["GTxtProduct"].Value = dr["PName"].ToString();
                    ProductGrid.Rows[iRows].Cells["GTxtBasicQty"].Value = dr["Qty"].GetDecimalQtyString();
                    ProductGrid.Rows[iRows].Cells["GTxtBasicAmount"].Value = dr["B_Amount"].GetDecimalString();
                    ProductGrid.Rows[iRows].Cells["GTxtTermAmount"].Value = dr["T_Amount"].GetDecimalString();
                    ProductGrid.Rows[iRows].Cells["GTxtNetAmount"].Value = dr["N_Amount"].GetDecimalString();
                    iRows++;
                }
                ProductGrid.CurrentCell = ProductGrid.Rows[ProductGrid.RowCount - 1].Cells[0];
                ProductGrid.ClearSelection();
            }
            ProductVoucherTotalCalculation();
            BillGrid.ClearSelection();
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
        }
    }

    private void DeletedRowExitsOrNot(int sno, long productId)
    {
        if (_dtProductTerm.RowsCount() <= 0 || BillGrid.CurrentRow == null)
        {
            return;
        }
        var exDetails = _dtProductTerm.AsEnumerable().Any(c => c.Field<string>("ProductSno").Equals(sno.ToString()) && c.Field<string>("ProductId").Equals(productId.ToString()));
        if (!exDetails)
        {
            return;
        }
        foreach (DataRow ro in _dtProductTerm.Rows)
        {
            var index = _dtProductTerm.Rows.IndexOf(ro);
            if (sno == ro["ProductSno"].GetLong() && productId == ro["ProductId"].GetInt())
            {
                _dtProductTerm.Rows.Remove(ro);
                break;
            }
        }
        for (var i = 0; i < _dtProductTerm.RowsCount(); i++)
        {
            var exitSno = _dtProductTerm.Rows[i]["ProductSno"].GetInt();
            exitSno -= 1;
            _dtProductTerm.Rows[i].SetField("ProductSno", exitSno);
        }
    }

    private void PreviewImage(PictureBox pictureBox)
    {
        if (pictureBox.BackgroundImage == null) return;
        var fileExt = Path.GetExtension(pictureBox.ImageLocation);
        var location = pictureBox.ImageLocation;
        if (fileExt is ".JPEG" or ".jpg" or ".Bitmap" or ".png")
        {
            ObjGlobal.PreviewPicture(pictureBox, string.Empty);
        }
        else
        {
            Process.Start(location ?? string.Empty);
        }
    }

    private void AddTextToProductGrid(bool isUpdate)
    {
        if (ProductGrid.CurrentRow != null)
        {
            var iRows = ProductGrid.CurrentRow.Index;
            ProductGrid.Rows[iRows].Cells["GTxtTermAmount"].Value = TxtPTermAmount.Text;
            ProductGrid.Rows[iRows].Cells["GTxtNetAmount"].Value = TxtPNetAmount.Text;

            var currentRow = ProductGrid.RowCount - 1 > iRows ? iRows + 1 : ProductGrid.RowCount - 1;
            ProductGrid.CurrentCell = ProductGrid.Rows[currentRow].Cells[_columnIndex];
        }
        ClearProductDetails();
        TxtProduct.AcceptsTab = false;
    }

    private bool AddTextToBillGrid(bool isUpdate)
    {
        if (BillGrid.CurrentRow != null)
        {
            var iRows = BillGrid.CurrentRow.Index;
            if (!isUpdate)
            {
                BillGrid.Rows.Add();
                BillGrid.Rows[iRows].Cells["GTxtTermSno"].Value = iRows + 1;
            }
            BillGrid.Rows[iRows].Cells["GTxtTermId"].Value = _termId.ToString();
            BillGrid.Rows[iRows].Cells["GTxtTerm"].Value = TxtBTermDesc.Text;
            BillGrid.Rows[iRows].Cells["GTxtLedgerId"].Value = _ledgerId.ToString();
            BillGrid.Rows[iRows].Cells["GTxtLedger"].Value = TxtBLedger.Text;
            BillGrid.Rows[iRows].Cells["GTxtSubledgerId"].Value = _subledgerId;
            BillGrid.Rows[iRows].Cells["GTxtSubLedger"].Value = TxtBSubLedger.Text;
            BillGrid.Rows[iRows].Cells["GTxtTermType"].Value = TxtBTermType.Text;
            BillGrid.Rows[iRows].Cells["GTxtSign"].Value = TxtBTermSign.Text;
            BillGrid.Rows[iRows].Cells["GTxtRate"].Value = TxtBTermRate.Text;
            BillGrid.Rows[iRows].Cells["GTxtAmount"].Value = TxtBTermAmount.Text;
            BillGrid.Rows[iRows].Cells["GTxtInvoiceNetAmount"].Value = TxtBNetAmount.Text;
            var currentRow = BillGrid.RowCount - 1 > iRows ? iRows + 1 : BillGrid.RowCount - 1;
            BillGrid.CurrentCell = BillGrid.Rows[currentRow].Cells[_columnIndex];
        }
        if (_isRowUpdate)
        {
            EnableBillGridControl();
            ClearBillDetails();
            BillGrid.Focus();
            return false;
        }
        ClearBillDetails();
        GetBillSerialNo();
        return true;
    }

    private bool IsValidInformation()
    {
        if (_actionTag != "SAVE")
        {
            if (CustomMessageBox.VoucherNoAction(_actionTag, TxtVoucherNo.Text) == DialogResult.No)
            {
                return false;
            }
        }
        if (TxtVoucherNo.IsBlankOrEmpty())
        {
            TxtVoucherNo.Enabled = true;
            TxtVoucherNo.WarningMessage($"VOUCHER NUMBER MUST HAVE A VALUE FOR {_actionTag}..!!");
            return false;
        }
        if (_tagStrings.Contains(_actionTag))
        {
            return true;
        }
        if (MskDate.MaskCompleted)
        {
            if (MskDate.Text.IsDateExits("D"))
            {
                if (!MskDate.Text.IsValidDateRange("D"))
                {
                    MskDate.WarningMessage($"ENTER DATE MUST BE BETWEEN {ObjGlobal.CfStartAdDate} AND {ObjGlobal.CfEndAdDate}");
                    return false;
                }
            }
            else
            {
                this.NotifyValidationError(MskDate, "ENTER VOUCHER DATE IS INVALID..!!");
                return false;
            }
        }
        else if (!MskDate.MaskCompleted)
        {
            MskDate.WarningMessage("ENTER VOUCHER MITI IS INVALID..!!");
            return false;
        }
        if (MskMiti.MaskCompleted)
        {
            if (MskMiti.Text.IsDateExits("M"))
            {
                if (!MskMiti.Text.IsValidDateRange("M"))
                {
                    MskMiti.WarningMessage($"ENTER MITI MUST BE BETWEEN {ObjGlobal.CfStartBsDate} AND {ObjGlobal.CfEndBsDate}");
                    return false;
                }
            }
            else
            {
                MskMiti.WarningMessage("ENTER VOUCHER MITI IS INVALID..!!");
                return false;
            }
        }
        else if (!MskDate.MaskCompleted)
        {
            MskDate.WarningMessage("ENTER VOUCHER MITI IS INVALID..!!");
            return false;
        }
        if (TxtPurchaseInvoiceNo.IsBlankOrEmpty())
        {
            TxtPurchaseInvoiceNo.WarningMessage("PURCHASE INVOICE VOUCHER NUMBER NOT TAG IN THIS VOUCHER");
            return false;
        }
        if (BillGrid.RowCount <= 0)
        {
            CustomMessageBox.Warning("PLEASE ENTER BILLING TERM AMOUNT");
            BillGrid.Focus();
            return false;
        }
        if (TxtVoucherNo.IsBlankOrEmpty())
        {
            TxtVoucherNo.WarningMessage("ENTER VOUCHER NUMBER OR SELECTED IT..!!");
            return false;
        }
        if (TxtRemarks.IsBlankOrEmpty() && _tagStrings.Contains(_actionTag))
        {
            TxtRemarks.WarningMessage($"ENTER REMARKS OF THIS VOUCHER FOR {_actionTag}..!!");
            return false;
        }
        if ((TxtDepartment.IsBlankOrEmpty() || _departmentId is 0) && ObjGlobal.PurchaseDepartmentMandatory)
        {
            TxtDepartment.WarningMessage("ENTER DEPARTMENT OF THIS VOUCHER..!!");
            return false;
        }
        return true;
    }

    private void OpenPurchaseTermList()
    {
        var (description, id) = GetMasterList.GetPurchaseTermList(_actionTag, "A");
        if (description.IsValueExits())
        {
            TxtBTermDesc.Text = description;
            _termId = id;
            FillTermInformation();
        }
        TxtBSubLedger.Focus();
    }

    private void FillTermInformation()
    {
        var dt = _additional.GetPurchaseTermInformation(_termId);
        if (dt.Rows.Count <= 0)
        {
            return;
        }
        TxtBTermType.Text = dt.Rows[0]["PT_Basis"].ToString();
        TxtBTermSign.Text = dt.Rows[0]["PT_Sign"].ToString();
        _ledgerId = dt.Rows[0]["Ledger"].GetLong();
        TxtBLedger.Text = dt.Rows[0]["GLName"].ToString();
        TxtBTermRate.Text = dt.Rows[0]["PT_Rate"].GetDecimalString();
        TxtBTermAmount_TextChanged(this, EventArgs.Empty);
    }

    private void OpenSubLedgerList()
    {
        var (description, id) = GetMasterList.GetSubLedgerList(_actionTag);
        if (description.IsValueExits())
        {
            TxtBSubLedger.Text = description;
            _subledgerId = id;
        }
        TxtBSubLedger.Focus();
    }

    private void OpenGeneralLedger()
    {
        var (description, id) = GetMasterList.GetGeneralLedger(_actionTag, "OTHER");
        if (description.IsValueExits())
        {
            TxtBLedger.Text = description;
            _ledgerId = id;
        }
        TxtBLedger.Focus();
    }

    #endregion --------------- METHOD FOR THIS FORM ---------------

    //  OBJECT FOR THIS FORM

    #region -------------- OBJECT --------------

    private int _departmentId;
    private int _currencyId;
    private int _termId;
    private int _rowIndex;
    private int _columnIndex;
    private int _subledgerId;

    private long _ledgerId;
    private long _productId;

    private bool _rowDelete;
    private bool _isRowUpdate;
    private bool _isProvision;
    private bool _isZoom;
    private bool _isPTermExits;
    private bool _isBTermExits;
    private bool _isTaxable;

    private string _invoiceType;
    private string _actionTag = string.Empty;
    private string _numberScheme = string.Empty;
    private string _txtZoomVno = string.Empty;
    private string[] _tagStrings = ["DELETE", "REVERSE"];

    private decimal _taxRate;
    private decimal _totalTaxable;
    private decimal _totalVat;

    private KeyPressEventArgs _getKeys;
    private DataTable _dtPartyInfo;
    private DataTable _dtProductTerm;
    private DataTable _dtBillTerm;
    // private IPurchaseEntry _purchaseEntry;
    private readonly IPurchaseAdditionalRepository _additional;
    private IMasterSetup _masterSetup;
    private IPurchaseDesign _purchaseDesign;

    private MrGridNumericTextBox TxtBTermSno { get; set; }
    private MrGridTextBox TxtBTermDesc { get; set; }
    private MrGridTextBox TxtBLedger { get; set; }
    private MrGridTextBox TxtBSubLedger { get; set; }
    private MrGridTextBox TxtBTermType { get; set; }
    private MrGridTextBox TxtBTermSign { get; set; }
    private MrGridNumericTextBox TxtBTermRate { get; set; }
    private MrGridNumericTextBox TxtBTermAmount { get; set; }
    private MrGridNumericTextBox TxtBNetAmount { get; set; }
    private MrGridNumericTextBox TxtPTermSno { get; set; }
    private MrGridTextBox TxtProduct { get; set; }
    private MrGridNumericTextBox TxtPBasicQty { get; set; }
    private MrGridNumericTextBox TxtPBasicAmount { get; set; }
    private MrGridNumericTextBox TxtPTermAmount { get; set; }
    private MrGridNumericTextBox TxtPNetAmount { get; set; }

    #endregion -------------- OBJECT --------------
}