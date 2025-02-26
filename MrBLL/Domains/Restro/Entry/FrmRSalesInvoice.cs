using DatabaseModule.CustomEnum;
using DatabaseModule.DataEntry.SalesMaster.SalesInvoice;
using DatabaseModule.DataEntry.SalesMaster.SalesOrder;
using DatabaseModule.DataEntry.SalesMaster.SalesReturn;
using MrBLL.DataEntry.Common;
using MrBLL.Domains.POS.Entry;
using MrBLL.Domains.POS.Master;
using MrBLL.Domains.Restro.Master;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.DataEntry.Design;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.Interface.SalesMaster;
using MrDAL.DataEntry.Interface.SalesOrder;
using MrDAL.DataEntry.Interface.SalesReturn;
using MrDAL.DataEntry.SalesMaster;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Utility.dbMaster;
using MrDAL.Utility.Server;
using PrintControl.PrintMethod;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using PrintControl.PrintClass.DirectPrint;

namespace MrBLL.Domains.Restro.Entry;

public partial class FrmRSalesInvoice : MrForm
{
    #region --------------- SALES INVOICE ---------------

    public FrmRSalesInvoice(string table, int tableId, string tableType)
    {
        InitializeComponent();
        InitialiseDataTable();
        _objDesign.GetRestroInvoiceDesign(RGrid, "RESTRO");
        _actionTag = string.Empty;


        _tableId = tableId;
        _tableType = tableType;
        TxtCounter.Text = table;

        _printFunction = new ClsPrintFunction();
        _master.BindPaymentType(CmbPaymentType);
        _defaultPrinter = new PrinterSettings().PrinterName;

        _salesOrderRepository = new SalesOrderRepository();
        _salesInvoiceRepository = new SalesInvoiceRepository();
        _salesReturnRepository = new SalesReturnRepository();
    }

    private void FrmRSalesInvoice_Shown(object sender, EventArgs e)
    {
        TxtProduct.Focus();
    }

    private void FrmRSalesInvoice_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F6)
        {
            BtnMember_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.U)
        {
            MnuAddOrder.PerformClick();
            _actionTag = "UPDATE";
        }
    }

    private void FrmRSalesInvoice_Load(object sender, EventArgs e)
    {
        MnuAddOrder.PerformClick();
        if (!TxtCounter.Text.IsValueExits())
        {
            Close();
            return;
        }
        if (_tableId > 0)
        {
            var dtCheckOrder = _entry.CheckTableOrderExitsOrNot(_tableId);
            if (dtCheckOrder.Rows.Count > 0)
            {
                FillOrderInvoice(dtCheckOrder.Rows[0]["SO_Invoice"].GetString());
            }
        }
        TxtCounter.Focus();
    }

    private void FrmRSalesInvoice_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)21)
        {
            if (!ObjGlobal.IsIrdRegister)
            {
                _actionTag = "UPDATE";
                Text = @"RESTRO INVOICE DETAILS [UPDATE]";
                ClearControl();
                EnableControl(true);
                TxtVNo.Enabled = true;
                TxtVNo.Focus();
            }
        }
        else if (e.KeyChar == (char)27)
        {
            if (PDetails.Visible)
            {
                PDetails.Enabled = false;
                PDetails.Visible = false;
                TxtProduct.Focus();
            }
            else
            {
                if (_tableType.Equals("P") && RGrid.Rows.Count > 0)
                {
                    if (CustomMessageBox.Question("THIS TABLE IS PRE-PAID BOOKING.. DO YOU WANT TO CANCEL THE ORDER..??") == DialogResult.Yes)
                    {
                        Close();
                        return;
                    }
                }
                else if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
                {
                    Dispose();
                    return;
                }
            }
        }
    }

    #endregion --------------- SALES INVOICE ---------------

    // BUTTON CLICK EVENTS FOR THIS FORM

    #region --------------- BOTTON ---------------

    private void MnuAddOrder_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        ClearControl();
        EnableControl(true);
        ReturnOrderVoucherNumber();
        Text = _actionTag.IsValueExits()
            ? $"RESTAURANT POINT OF SALES [{_actionTag}]"
            : "RESTAURANT POINT OF SALES";
        RGrid.ReadOnly = true;
        MskDate.Text = DateTime.Now.GetDateString();
        MskMiti.GetNepaliDate(MskDate.Text);
        TxtProduct.Focus();
    }

    private void MnuOrderCancel_Click(object sender, EventArgs e)
    {
        _actionTag = "REVERSE";
        _invoiceType = "ORDER";
        EnableControl(false);
        PDetails.Enabled = true;
        PDetails.Visible = true;
        TxtRemarks.Enabled = true;
        TxtRemarks.Focus();
    }

    private void MnuOrderPrint_Click(object sender, EventArgs e)
    {
        var bill = new PrintSalesBill
        {
            BillNo = TxtVNo.Text,
            Printer = ObjGlobal.SysDefaultOrderPrinter,
            Printdesign = "KOT/BOT",
            PrintedBy = ObjGlobal.LogInUser,
            NoofPrint = 1,
            PDiscountId = ObjGlobal.SalesDiscountTermId.ToString(),
            BDiscountId = ObjGlobal.SalesSpecialDiscountTermId.ToString(),
            ServiceChargeId = ObjGlobal.SalesServiceChargeTermId.ToString(),
            SalesVatTermId = ObjGlobal.SalesVatTermId.ToString()
        };
        bill.PrintDocumentDesign();
    }

    private void MnuRePrintOrder_Click(object sender, EventArgs e)
    {
        var result = new FrmDocumentPrint("DLL", "SO", string.Empty, string.Empty);
        result.ShowDialog();
    }

    private void MnuReturnInvoice_Click(object sender, EventArgs e)
    {
        ClearControl();
        _actionTag = "SAVE";
        _invoiceType = "RETURN";
        Text = _actionTag.IsValueExits() ? $"RESTRO INVOICE OF SALES RETURN INVOICE [{_actionTag}]" : "RESTO SALES INVOICE";
        LblInvoiceNo.Visible = true;
        TxtRefVno.Enabled = TxtRefVno.Visible = true;
        BtnRefVno.Enabled = BtnRefVno.Visible = true;
        RGrid.ReadOnly = true;
        MskDate.Text = ObjGlobal.SysIsNightAudit ? ObjGlobal.NightAuditDate.GetDateString() : DateTime.Now.GetDateString();
        MskMiti.Text = ObjGlobal.ReturnNepaliDate(MskDate.Text);
        ReturnSrVoucherNumber();
        EnableControl(true);
        LblVoucherCaption.Text = @"Return No";
        TxtRefVno.Focus();
    }

    private void MnuReverseInvoice_Click(object sender, EventArgs e)
    {
        _actionTag = "REVERSE";
        _invoiceType = "INVOICE";
        ClearControl();
        Text = @$"RESTRO INVOICE DETAILS [{_actionTag}]";
        EnableControl(false);
        TxtVNo.Enabled = true;
        LblVoucherCaption.Text = @"Invoice No";
        TxtVNo.Focus();
    }

    private void MnuPrintInvoice_Click(object sender, EventArgs e)
    {
        var result = new FrmDocumentPrint("DLL", "SB", string.Empty, string.Empty);
        result.ShowDialog();
    }

    private void MnuDayClosing_Click(object sender, EventArgs e)
    {
        var result = new FrmCashClosing();
        result.ShowDialog();
    }

    private void MnuLockBilling_Click(object sender, EventArgs e)
    {
        new FrmLockScreen().ShowDialog();
    }

    private void MnuConfirmationPrint_Click(object sender, EventArgs e)
    {
        var result = new FrmConfirmationDiscount(TxtBasicAmount.Text);
        result.ShowDialog();
        if (result.DialogResult == DialogResult.OK)
        {
            var bill = new PrintSalesBill
            {
                BillNo = TxtVNo.Text,
                Printer = ObjGlobal.SysDefaultOrderPrinter,
                Printdesign = "CONFORMATION",
                PrintedBy = ObjGlobal.LogInUser,
                NoofPrint = 1,
                DiscountAmount = result.TxtAmount.Text,
                IsServiceApplicable = result.ChkServicesIncluded.Checked,
                PDiscountId = ObjGlobal.SalesDiscountTermId.ToString(),
                BDiscountId = ObjGlobal.SalesSpecialDiscountTermId.ToString(),
                ServiceChargeId = ObjGlobal.SalesServiceChargeTermId.ToString(),
                SalesVatTermId = ObjGlobal.SalesVatTermId.ToString()
            };
            bill.PrintDocumentDesign();
        }
    }

    private void MnuTableTransfer_Click(object sender, EventArgs e)
    {
        var result = new FrmTableTransfer(_tableId, TxtCounter.Text);
        result.ShowDialog();
        if (result.DialogResult != DialogResult.OK)
        {
            return;
        }
        _dialogResult = DialogResult.OK;
        Close();
        return;
    }

    private void MnuTableSplit_Click(object sender, EventArgs e)
    {
        var result = new FrmSplitTable(_tableId, TxtCounter.Text);
        result.ShowDialog();
        if (result.DialogResult != DialogResult.OK)
        {
            return;
        }
        _dialogResult = DialogResult.OK;
        Close();
        return;
    }

    #endregion --------------- BOTTON ---------------

    // MASTER EVENTS FOR THIS FORM

    #region --------------- MAIN EVENT ---------------

    private void TxtVno_Validating(object sender, CancelEventArgs e)
    {
        if (_actionTag.Equals("UPDATE"))
        {
            FillInvoiceForUpdate(TxtVNo.Text);
        }
        if (_actionTag.IsValueExits() && TxtVNo.IsBlankOrEmpty())
        {
            if (TxtVNo.ValidControl(ActiveControl))
            {
                TxtVNo.Enabled = true;
                TxtVNo.WarningMessage("VOUCHER NUMBER IS REQUIRED...!!");
            }
        }
    }

    private void BtnVno_Click(object sender, EventArgs e)
    {
        var listType = _invoiceType.Equals("RETURN") ? "SR" : "SB";
        var result = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", listType);
        if (result.IsValueExits())
        {
            TxtVNo.Text = result;
            if (_actionTag != "SAVE" && listType == "SB")
            {
                FillInvoiceData(result);
            }
            else if (listType == "SR" && _actionTag.Equals("SAVE"))
            {
                FillReturnInvoiceData(result);
            }
        }
        TxtVNo.Focus();
    }

    private void TxtVNo_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnVno.PerformClick();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (_actionTag.Equals("REVERSE"))
            {
                PDetails.Visible = true;
                PDetails.Enabled = true;
                TxtRemarks.Enabled = true;
                TxtRemarks.Focus();
            }
            else
            {
                SendKeys.Send("{TAB}");
            }
        }
    }

    private void TxtCounter_Leave(object sender, EventArgs e)
    {
        if (_actionTag.IsValueExits() && TxtCounter.Text.IsBlankOrEmpty())
        {
            if (TxtCounter.ValidControl(ActiveControl))
            {
                TxtCounter.WarningMessage("TERMINAL IS REQUIRED FOR BILLING..!!");
                TxtCounter.Focus();
            }
        }
    }

    private void TxtRefVno_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnRefVno_Click(sender, e);
            TxtVNo.Focus();
        }
    }

    private void BtnRefVno_Click(object sender, EventArgs e)
    {
        var result = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "SB");
        if (result.IsValueExits())
        {
            TxtRefVno.Text = result;
            if (_actionTag.Equals("SAVE"))
            {
                FillInvoiceData(result);
            }
        }
        TxtRefVno.Focus();
    }

    private void TxtRefVno_Leave(object sender, EventArgs e)
    {
        if (_tagStrings.Equals(_invoiceType))
        {
            if (TxtRefVno.Text.IsBlankOrEmpty() && TxtRefVno.ValidControl(ActiveControl))
            {
                if (CustomMessageBox.Question(@"PLEASE SELECT SALES INVOICE NUMBER FOR RETURN OR CLICK ON YES TO CONTINUE..!!") is DialogResult.No)
                {
                    TxtRefVno.Focus();
                }
            }
            if (RGrid.RowCount is 0 && _invoiceType.Equals("RETURN") && TxtVNo.Text.IsValueExits())
            {
                FillInvoiceData(TxtRefVno.Text);
            }
        }
    }

    private void TxtRefVno_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            if (_tagStrings.Equals(_invoiceType) && TxtRefVno.IsBlankOrEmpty())
            {
                TxtRefVno.WarningMessage("REFERENCE VOUCHER NUMBER IS REQUIRED..!!");
                TxtRefVno.Focus();
                return;
            }
        }
        GlobalControl_KeyPress(sender, e);
    }

    #endregion --------------- MAIN EVENT ---------------

    // DETAILS EVENTS OF THIS FORM

    #region --------------- DETAILS EVENT ---------------

    private void ListOfProduct_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            OpenProductList();
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id) = GetMasterList.CreateProduct(true);
            if (description.IsValueExits())
            {
                TxtProduct.Text = description;
                _productId = id;
            }
            SetProductInfo();
            TxtProduct.Focus();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtProduct.IsBlankOrEmpty())
            {
                if (RGrid.RowCount is 0)
                {
                    TxtProduct.WarningMessage("PLEASE SELECT THE MENU ITEMS");
                }
                else
                {
                    PDetails.Visible = PDetails.Enabled = true;
                    if (_tagStrings.Contains(_actionTag))
                    {
                        TxtRemarks.Focus();
                    }
                    else
                    {
                        if (TxtBillDiscountPercentage.Enabled)
                        {
                            TxtBillDiscountPercentage.Focus();
                        }
                        else if (TxtBillDiscountAmount.Enabled)
                        {
                            TxtBillDiscountAmount.Focus();
                        }
                        else if (TxtServiceChargeRate.Enabled)
                        {
                            TxtServiceChargeRate.Focus();
                        }
                        else if (TxtServiceCharge.Enabled)
                        {
                            TxtServiceCharge.Focus();
                        }
                        else if (TxtVatRate.Enabled)
                        {
                            TxtVatRate.Focus();
                        }
                        else if (TxtVatAmount.Enabled)
                        {
                            TxtVatAmount.Focus();
                        }
                        else if (TxtMember.Enabled)
                        {
                            TxtMember.Focus();
                        }
                        else
                        {
                            BtnSave.Focus();
                        }
                    }
                }
            }
            else
            {
                GlobalControl_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtProduct, BtnProduct);
        }
    }

    private void ListOfProduct_Enter(object sender, EventArgs e)
    {
        if (TxtProduct.Text.IsBlankOrEmpty())
        {
            RGrid.ClearSelection();
        }
        PDetails.Visible = false;
        PDetails.Enabled = false;
        TxtProduct.SelectAll();
    }

    private void BtnProduct_Click(object sender, EventArgs e)
    {
        OpenProductList();
    }

    private void TxtQty_KeyDown(object sender, KeyEventArgs e)
    {
        // if user tried to change the qty, rate or discount
        if (e.KeyCode != Keys.F2) return;
        var result = new FrmProductInfo(_productId, TxtAltQty.GetDecimal(), TxtQty.GetDecimal(), _salesRate, _pDiscountPercentage, _pDiscount);
        result.ShowDialog();
        if (result.DialogResult is not DialogResult.OK)
        {
            return;
        }
        TxtAltQty.Text = result.ChangeAltQty.GetDecimalString();
        _salesRate = result.ChangeRate;
        TxtQty.Text = result.ChangeQty.GetDecimalString();
        _pDiscountPercentage = result.DiscountPercent;
        _pDiscount = result.Discount;
    }

    private void TxtQty_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            AddDataToGridDetails(_isRowUpdate);
            TotalCalculationOfInvoice();
            RGrid.CurrentCell = RGrid.Rows[RGrid.RowCount - 1].Cells[0];
            if (!_tableType.Equals("P"))
            {
                if (_orderAction == "SAVE")
                {
                    var saveOrder = SaveSalesOrder();
                    _orderAction = saveOrder > 0 ? "UPDATE" : "SAVE";
                    if (saveOrder != 0)
                    {
                        PrintVoucher("SO");
                    }
                }
                else
                {
                    var updateOrder = UpdateSalesOrder(false);
                    if (updateOrder != 0)
                    {
                        PrintVoucher("SO");
                    }
                }
            }
            ClearDetails();
            TxtProduct.Focus();
        }
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var _);
        }
    }

    private void TxtQty_Validating(object sender, CancelEventArgs e)
    {

    }

    #endregion --------------- DETAILS EVENT ---------------

    // GRID EVENTS CONTROL

    #region --------------- DATA GRID EVENTS ---------------

    private void RGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
        var _rIndex = e.RowIndex;
    }

    private void RGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
        var index = e.ColumnIndex;
    }

    private void RGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (RGrid.CurrentRow != null)
        {
            if (e.KeyCode is Keys.Delete)
            {
                var dtRowCheck = $"SELECT * FROM AMS.SO_Details so WHERE so.SO_Invoice ='{TxtVNo.Text}'".GetQueryDataTable();
                if (dtRowCheck.Rows.Count is 1 || RGrid.RowCount is 1)
                {
                    MessageBox.Show(@"UNABLE TO DELETE THE PRODUCT..!!");
                    RGrid.ClearSelection();
                    TxtProduct.Focus();
                }
                else if (dtRowCheck.Rows.Count > 1)
                {
                    var result = dtRowCheck.Rows[RGrid.CurrentRow.Index]["PrintKOT"].GetBool();
                    if (result)
                    {
                        MessageBox.Show(@"PRINTED KOT IS UNABLE TO DELETE THE PRODUCT..!!");
                        return;
                    }
                    if (CustomMessageBox.Question(@"DO YOU WANT TO CANCEL ORDER..??") == DialogResult.Yes)
                    {
                        var reason = new FrmAddDescriptions();
                        reason.ShowDialog();
                        if (reason.Descriptions.IsValueExits())
                        {
                            var selectedProductId = RGrid.CurrentRow.Cells["GTxtProductId"].Value.GetLong();
                            var sno = RGrid.CurrentRow.Cells["GTxtSNo"].Value.GetInt();
                            var cmdString = $"DELETE AMS.SO_Details WHERE SO_Invoice='{TxtVNo.Text}' AND P_Id = {selectedProductId} AND Invoice_SNo = {sno}";
                            var updateQuery = SqlExtensions.ExecuteNonQuery(cmdString);
                            if (updateQuery != 0)
                            {
                                var record = $"Select * from AMS.OrderCancelMaster where SO_Invoice = '{TxtVNo.Text}'";
                                var dtRecord = SqlExtensions.DataSet(record).Tables[0];
                                SaveSalesOrderCancel(RGrid.CurrentRow, reason.Descriptions, dtRecord.Rows.Count > 0);
                                RGrid.Rows.RemoveAt(RGrid.CurrentRow.Index);
                                TotalCalculationOfInvoice();
                                TxtProduct.Focus();
                            }
                        }
                    }
                }
            }
        }
    }

    private void RGrid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        SetDataFromGridToTextBox();
    }

    private void RGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (RGrid.CurrentRow != null)
        {
            var index = RGrid.CurrentRow.Index;
            var narr = RGrid.Rows[index].Cells["GTxtNarration"].Value.ToString();
            var desc = new FrmAddDescriptions()
            {
                Descriptions = narr
            };
            desc.ShowDialog();
            if (desc.DialogResult == DialogResult.OK)
            {
                RGrid.Rows[index].Cells["GTxtNarration"].Value = desc.TxtDescription.Text;
            }
        }
    }

    #endregion --------------- DATA GRID EVENTS ---------------

    // EVENTS OF THIS FORM

    #region --------------- FOOTER EVENT ---------------

    private void CmbPaymentType_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (CmbPaymentType.Text.Equals("PARTIAL"))
            {
                TxtTenderAmount.Enabled = false;
                BtnSave.Focus();
                return;
            }
            TxtTenderAmount.Focus();
        }
    }

    private void CmbPaymentType_Leave(object sender, EventArgs e)
    {
        ObjGlobal.ComboBoxBackColor(CmbPaymentType, 'L');
        if (PDetails.Visible)
        {
            if (CmbPaymentType.Text is "PARTIAL")
            {
                var result = new FrmSettlement(TxtNetAmount.Text);
                result.ShowDialog();
                if (result.DialogResult == DialogResult.OK)
                {
                    TxtTenderAmount.Text = result.TotalReceived.GetDecimalString();
                    _getPaymentDetails = result.SettleView;
                    TxtTenderAmount_TextChanged(sender, EventArgs.Empty);
                }
            }
        }
    }

    private void CmbPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        var result = CmbPaymentType.Text.ToUpper();
        var userLedgerId = _master.GetUserLedgerIdFromUser(ObjGlobal.LogInUser);
        TxtCustomer.ReadOnly = result.Equals("CASH");
        _ledgerId = result switch
        {
            "CASH" => userLedgerId > 0 ? userLedgerId : ObjGlobal.FinanceCashLedgerId,
            "CARD" => ObjGlobal.FinanceCardLedgerId.GetLong(),
            "BANK" => ObjGlobal.FinanceBankLedgerId.GetLong(),
            "PHONE PAY" => ObjGlobal.FinanceFonePayLedgerId.GetLong(),
            "E-SEWA" => ObjGlobal.FinanceKhaltiLedgerId.GetLong(),
            "KHALTI" => ObjGlobal.FinanceKhaltiLedgerId.GetLong(),
            "REMIT" => ObjGlobal.FinanceRemitLedgerId.GetLong(),
            "CONNECTIPS" => ObjGlobal.FinanceConnectIpsLedgerId.GetLong(),
            "PARTIAL" => ObjGlobal.FinancePartialLedgerId.GetLong(),
            _ => 0
        };
        TxtCustomer.Text = _master.GetLedgerDescription(_ledgerId);
        TxtCustomer.Enabled = !CmbPaymentType.SelectedValue.Equals("PARTIAL");
    }

    private void TxtCustomer_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            btnCustomer.PerformClick();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var (description, id) = GetMasterList.CreateGeneralLedger("CUSTOMER", true);
            if (description.IsValueExits())
            {
                TxtCustomer.Text = description;
                _ledgerId = id;
            }
            TxtCustomer.Focus();
        }
        else if (e.KeyCode is Keys.Enter or Keys.Tab)
        {
            if (TxtCustomer.Text.Trim() == string.Empty)
            {
                BtnCustomer_Click(sender, e);
            }
            if (TxtCustomer.IsValueExits() && _ledgerId > 0)
            {
                BtnSave.Focus();
            }
            else
            {
                CustomMessageBox.Information(@"CUSTOMER CANNOT LEFT BLANK..! PLEASE SELECT CUSTOMER..!!");
            }
        }
        else if (e.KeyCode == Keys.F2)
        {
            var table = GetConnection.SelectDataTableQuery($"SELECT gl.GLType FROM AMS.GeneralLedger gl WHERE gl.GLName='{TxtCustomer.Text}'");
            if (table.Rows.Count <= 0)
            {
                return;
            }
            if (table.Rows[0]["GlType"].ToString() == "Bank" || table.Rows[0]["GlType"].ToString() == "Cash")
            {
                CashAndBankValidation(table.Rows[0]["GlType"].ToString());
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtCustomer, btnCustomer);
        }
    }

    private void BtnCustomer_Click(object sender, EventArgs e)
    {
        const string category = "Customer";
        var (description, id) = GetMasterList.GetGeneralLedger(_actionTag, category, MskDate.Text, "MASTER");
        if (id <= 0)
        {
            return;
        }
        TxtCustomer.Text = description;
        _ledgerId = id;
        LedgerCurrentBalance(_ledgerId);
        TxtCustomer.Focus();
    }

    private void TxtCustomer_TextChanged(object sender, EventArgs e)
    {
        LedgerCurrentBalance(_ledgerId);
    }

    private void TxtCustomer_Validating(object sender, CancelEventArgs e)
    {
        if (TxtCustomer.IsValueExits() && _actionTag.Equals("SAVE") && _customerResult != DialogResult.Yes)
        {
            var paymentType = _entry.GetInvoicePaymentMode(_ledgerId).GetUpper();
            if (paymentType.IsBlankOrEmpty())
            {
                return;
            }
            if (paymentType != CmbPaymentType.Text)
            {
                var msg = $"SELECTED CUSTOMER IS BILLED IN {paymentType}..!! DO YOU WANT TO CONTINUE..??";
                if (CustomMessageBox.Question(msg) is DialogResult.No)
                {
                    TxtCustomer.Focus();
                }
                else
                {
                    _customerResult = DialogResult.Yes;
                }
            }
        }
    }

    private void TxtRemarks_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (TxtRemarks.Text.IsBlankOrEmpty())
            {
                if (_tagStrings.Contains(_invoiceType) || _tagStrings.Contains(_actionTag))
                {
                    var msg = _tagStrings.Contains(_invoiceType)
                        ? "PLEASE ENTER THE REMARKS FOR RETURN"
                        : $"PLEASE ENTER THE REMARKS FOR {_actionTag}";

                    this.NotifyValidationError(TxtRemarks, msg);
                    return;
                }

                if (TxtRemarks.Enabled && ObjGlobal.SalesRemarksMandatory)
                {
                    this.NotifyValidationError(TxtRemarks, $"PLEASE ENTER THE REMARKS FOR {_actionTag}");
                    return;
                }
            }

            if (_actionTag == "REVERSE")
            {
                PDetails.Visible = PDetails.Enabled = true;
                PDetails.Focus();
                BtnSave.Focus();
            }
            else
            {
                BtnSave.Focus();
            }
        }
    }

    private void TxtRemarks_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtRemarks, 'E');
    }

    private void TxtRemarks_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtRemarks, 'L');
    }

    private void TxtRemarks_Validated(object sender, EventArgs e)
    {
        if (ObjGlobal.SalesRemarksMandatory && TxtRemarks.Text.Trim() == string.Empty)
        {
            MessageBox.Show(@"REMARKS IS MANDATORY, SO IT CAN'T BE LEFT BLANK..!!", ObjGlobal.Caption);
            TxtRemarks.Focus();
        }
    }

    private void TxtBillDiscountPercentage_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtBillDiscountPercentage, 'E');
        TxtBillDiscountPercentage.SelectAll();
    }

    private void TxtBillDiscountPercentage_KeyPress(object sender, KeyPressEventArgs e)
    {
        GlobalControl_KeyPress(sender, e);
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
        }
    }

    private void TxtBillDiscountPercentage_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtBillDiscountPercentage, 'L');
    }

    private void TxtBillDiscountPercentage_Validated(object sender, EventArgs e)
    {
        if (TxtBillDiscountPercentage.Text.GetDecimal() > 0)
        {
            if (ObjGlobal.ReturnDecimal(TxtBillDiscountPercentage.Text) > 100)
            {
                MessageBox.Show(@"Discount Percentage can't more then 100%..!!", ObjGlobal.Caption);
                TxtBillDiscountPercentage.Clear();
                TxtBillDiscountPercentage.Focus();
            }
            else
            {
                TxtBillDiscountPercentage.Text = ((object)TxtBillDiscountPercentage.Text).GetDecimalString();
            }
        }
        else
        {
            TxtBillDiscountPercentage.Text = 0.GetDecimalString();
        }
        TxtBillDiscountAmount_TextChanged(sender, EventArgs.Empty);
    }

    private void TxtBillDiscountAmount_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtBillDiscountAmount, 'E');
        TxtBillDiscountAmount.SelectAll();
    }

    private void TxtBillDiscountAmount_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            if (TxtServiceChargeRate.Enabled)
            {
                GlobalControl_KeyPress(sender, e);
            }
            else
            {
                TxtMember.Focus();
            }
        }

        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
        }
    }

    private void TxtBillDiscountAmount_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtBillDiscountAmount, 'L');
        if (TxtBillDiscountAmount.Text.GetDecimal() > 0)
        {
            TxtBillDiscountAmount.Text = TxtBillDiscountAmount.Text.GetDecimalString();
        }
    }

    private void TxtBillDiscountAmount_Validating(object sender, CancelEventArgs e)
    {
        try
        {
            var amount = TxtBasicAmount.GetDecimal() - TxtBillDiscountAmount.GetDecimal();
            if (amount < 0)
            {
                MessageBox.Show(@"DISCOUNT AMOUNT CAN'T BE GREATER THEN BASIC AMOUNT..!!", ObjGlobal.Caption);
                TxtBillDiscountAmount.Focus();
                return;
            }
            TxtBillDiscountAmount_TextChanged(sender, EventArgs.Empty);
        }
        catch
        {
            // ~~ignored~~
        }
    }

    private void TxtTenderAmt_Enter(object sender, EventArgs e)
    {
        TxtMember.SelectAll();
    }

    private void TxtTenderAmt_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter)
        {
            return;
        }
        if (CmbPaymentType.SelectedIndex == 0)
        {
            if (TxtTenderAmount.GetDecimal() is 0 && !_tagStrings.Contains(_invoiceType) && !ObjGlobal.SalesIgnoreTenderAmount)
            {
                if (TxtNetAmount.GetDecimal() == 0)
                {
                    if (ObjGlobal.SalesRemarksEnable && TxtRemarks.Enabled)
                    {
                        TxtRemarks.Focus();
                    }
                    else
                    {
                        BtnSave.Focus();
                    }
                    return;
                }
                TxtTenderAmount.WarningMessage(@"TENDER AMOUNT CAN NOT LEFT BLANK OR ZERO AMOUNT");
                return;
            }

            if (!ObjGlobal.SalesIgnoreTenderAmount)
            {
                if (TxtTenderAmount.GetDecimal() != 0 && TxtTenderAmount.GetDecimal() - TxtNetAmount.GetDecimal() < 0)
                {
                    TxtTenderAmount.WarningMessage(@"TENDER AMOUNT CAN NOT LESS THEN INVOICE INVOICE");
                    return;
                }
            }
        }

        if (ObjGlobal.SalesRemarksEnable && TxtRemarks.Enabled)
        {
            TxtRemarks.Focus();
        }
        else
        {
            BtnSave.Focus();
        }
    }

    private void TxtTenderAmt_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
        }
    }

    private void TxtTenderAmt_Leave(object sender, EventArgs e)
    {
        TxtTenderAmount.Text = TxtTenderAmount.GetDecimalString();
    }

    private void PDetails_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            PDetails.Visible = false;
            TxtMember.Focus();
        }
    }

    private void TxtTenderAmt_Validating(object sender, CancelEventArgs e)
    {
        if (TxtChangeAmount.GetDecimal() < 0 && !_tagStrings.Contains(_invoiceType) && !ObjGlobal.SalesIgnoreTenderAmount)
        {
            if (TxtNetAmount.GetDecimal() == 0)
            {
                return;
            }
            if (TxtTenderAmount.ValidControl(ActiveControl) && PDetails.Visible)
            {
                TxtTenderAmount.WarningMessage(@"TENDER AMOUNT CAN'T BE BLANK..!!");
                return;
            }
        }
        if (!ObjGlobal.SalesIgnoreTenderAmount)
        {
            if (TxtTenderAmount.GetDecimal() > 0 && TxtTenderAmount.GetDecimal() < TxtNetAmount.GetDecimal() && TxtTenderAmount.Focused && PDetails.Visible)
            {
                e.Cancel = true;
                MessageBox.Show(@"TENDER AMOUNT CAN'T BE LESS THAN BILL AMOUNT..!!", ObjGlobal.Caption);
                TxtTenderAmount.Focus();
            }
        }
        TxtTenderAmount.Text = TxtTenderAmount.GetDecimalString();
    }

    private void BtnMember_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetMemberShipList(_actionTag);
        if (description.IsValueExits())
        {
            TxtMember.Text = description;
            _memberShipId = id;
        }
        TxtMember.Focus();
    }

    private void TxtMember_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnMember_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            CmbPaymentType.Focus();
        }
    }

    private void TxtMember_Validated(object sender, EventArgs e)
    {
        if (!TxtMember.Text.IsValueExits())
        {
            return;
        }
        var dtMember = _master.CheckMemberShipValidData(TxtMember.Text);
        if (dtMember.Rows.Count > 0)
        {
            FillMemberValue(dtMember.Rows[0]["MShipId"].GetInt());
        }
        else
        {
            var frm = new FrmMemberShip(true, true, TxtMember.Text);
            frm.ShowDialog();
            FillMemberValue(frm.MemberShipId.GetInt());
        }
    }

    private void TxtMember_KeyPress(object sender, KeyPressEventArgs e)
    {
        e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
    }

    private void TxtTenderAmount_TextChanged(object sender, EventArgs e)
    {
        if (TxtNetAmount.IsValueExits())
        {
            TxtChangeAmount.Text = (TxtTenderAmount.GetDecimal() - TxtNetAmount.GetDecimal()).GetDecimalString();
        }
    }

    private void TxtBillDiscountPercentage_TextChanged(object sender, EventArgs e)
    {
        TxtBillDiscountAmount.Text = (TxtBillDiscountPercentage.GetDecimal() * TxtBasicAmount.GetDecimal() / 100).GetDecimalString();
        TxtVoucherAmount.Text = (TxtBasicAmount.GetDecimal() - TxtBillDiscountAmount.GetDecimal()).GetDecimalString();
        TxtServiceCharge.Text = (TxtServiceChargeRate.GetDecimal() * TxtVoucherAmount.GetDecimal() / 100).GetDecimalString();
        TxtTaxableAmount.Text = (TxtVoucherAmount.GetDecimal() + TxtServiceCharge.GetDecimal()).GetDecimalString();
        TxtVatAmount.Text = (TxtVatRate.GetDecimal() * TxtTaxableAmount.GetDecimal() / 100).GetDecimalString();
        TxtNetAmount.Text = (TxtTaxableAmount.GetDecimal() + TxtVatAmount.GetDecimal()).GetDecimalString();
        LblNumberInWords.Text = ClsMoneyConversion.MoneyConversion(TxtNetAmount.GetDecimalString());
    }

    private void TxtBillDiscountAmount_TextChanged(object sender, EventArgs e)
    {
        if (!TxtBillDiscountAmount.Enabled && ActiveControl.Name != null)
        {
            return;
        }

        var amount = TxtBasicAmount.GetDecimal() - TxtBillDiscountAmount.GetDecimal();
        if (amount < 0)
        {
            TxtBillDiscountAmount.WarningMessage("DISCOUNT AMOUNT CANNOT BE GREATER THAN INVOICE AMOUNT");
        }
        TxtVoucherAmount.Text = (TxtBasicAmount.GetDecimal() - TxtBillDiscountAmount.GetDecimal()).GetDecimalString();
        TxtTaxableAmount.Text = (TxtVoucherAmount.GetDecimal() + TxtServiceCharge.GetDecimal()).GetDecimalString();
        TxtVatAmount.Text = (TxtVatRate.GetDecimal() * TxtTaxableAmount.GetDecimal() / 100).GetDecimalString();
        TxtNetAmount.Text = (TxtTaxableAmount.GetDecimal() + TxtVatAmount.GetDecimal()).GetDecimalString();
        LblNumberInWords.Text = ClsMoneyConversion.MoneyConversion(TxtNetAmount.GetDecimalString());
    }

    private void TxtServiceChangeRate_TextChanged(object sender, EventArgs e)
    {
        TxtServiceCharge.Text = (TxtServiceChargeRate.GetDecimal() * TxtVoucherAmount.GetDecimal() / 100).GetDecimalString();
        TxtTaxableAmount.Text = (TxtVoucherAmount.GetDecimal() + TxtServiceCharge.GetDecimal()).ToString(ObjGlobal.SysAmountFormat);
        TxtVatAmount.Text = (TxtVatRate.GetDecimal() * TxtTaxableAmount.GetDecimal() / 100).GetDecimalString();
        TxtNetAmount.Text = (TxtTaxableAmount.GetDecimal() + TxtVatAmount.GetDecimal()).ToString(ObjGlobal.SysAmountFormat);
        LblNumberInWords.Text = ClsMoneyConversion.MoneyConversion(TxtNetAmount.GetDecimalString());
    }

    private void TxtServiceChange_TextChanged(object sender, EventArgs e)
    {
        if (!TxtServiceCharge.Enabled && ActiveControl.Name != null)
        {
            return;
        }
        TxtTaxableAmount.Text = (TxtVoucherAmount.GetDecimal() + TxtServiceCharge.GetDecimal()).ToString(ObjGlobal.SysAmountFormat);
        TxtVatAmount.Text = (TxtVatRate.GetDecimal() * TxtTaxableAmount.GetDecimal() / 100).GetDecimalString();
        TxtNetAmount.Text = (TxtTaxableAmount.GetDecimal() + TxtVatAmount.GetDecimal()).ToString(ObjGlobal.SysAmountFormat);
        LblNumberInWords.Text = ClsMoneyConversion.MoneyConversion(TxtNetAmount.GetDecimalString());
    }

    private void TxtServiceChargeRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            GlobalControl_KeyPress(sender, e);
        }
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
        }
    }

    private void TxtServiceCharge_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            GlobalControl_KeyPress(sender, e);
        }
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
        }
    }

    private void TxtVatRate_TextChanged(object sender, EventArgs e)
    {
        TxtVatAmount.Text = (TxtVatRate.GetDecimal() * TxtTaxableAmount.GetDecimal() / 100).GetDecimalString();
        TxtNetAmount.Text = (TxtTaxableAmount.GetDecimal() + TxtVatAmount.GetDecimal()).ToString(ObjGlobal.SysAmountFormat);
        LblNumberInWords.Text = ClsMoneyConversion.MoneyConversion(TxtNetAmount.GetDecimalString());
    }

    private void TxtVatRate_Validating(object sender, CancelEventArgs e)
    {
        TxtVatRate.Text = TxtVatRate.GetDecimalString();
        TxtVatAmount_TextChanged(sender, EventArgs.Empty);
    }

    private void TxtVatAmount_TextChanged(object sender, EventArgs e)
    {
        TxtNetAmount.Text = (TxtTaxableAmount.GetDecimal() + TxtVatAmount.GetDecimal()).ToString(ObjGlobal.SysAmountFormat);
        LblNumberInWords.Text = ClsMoneyConversion.MoneyConversion(TxtNetAmount.GetDecimalString());
    }

    private void TxtVatAmount_Validating(object sender, CancelEventArgs e)
    {
        TxtVatAmount.Text = TxtVatAmount.GetDecimalString();
        TxtVatAmount_TextChanged(sender, EventArgs.Empty);
    }

    private void TxtServiceChargeRate_Validating(object sender, CancelEventArgs e)
    {
        TxtServiceChargeRate.Text = TxtServiceChargeRate.GetDecimalString();
        TxtServiceChange_TextChanged(sender, EventArgs.Empty);
    }

    private void TxtServiceCharge_Validating(object sender, CancelEventArgs e)
    {
        TxtServiceCharge.Text = TxtServiceCharge.GetDecimalString();
        if (TxtVoucherAmount.GetDecimal() is 0)
        {
            return;
        }
        if (TxtVoucherAmount.GetDecimal() <= TxtServiceCharge.GetDecimal() && RGrid.RowCount > 0)
        {
            TxtServiceCharge.WarningMessage("SERVICE CHARGE AMOUNT CANNOT BE GREATER THAN INVOICE AMOUNT..!!");
            return;
        }
        TxtServiceChange_TextChanged(sender, EventArgs.Empty);
    }

    private void TxtVatRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            GlobalControl_KeyPress(sender, e);
        }

        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
        }
    }

    private void TxtVatAmount_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            TxtMember.Focus();
        }

        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
        }
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ObjGlobal.IsLicenseExpire && !Debugger.IsAttached)
            {
                this.NotifyWarning("SOFTWARE LICENSE HAS BEEN EXPIRED..!! CONTACT WITH VENDOR FOR FURTHER..!!");
                return;
            }
            BtnSave.Enabled = false;
            if (IsValidInvoice())
            {
                var result = 0;
                CreateDatabaseTable.DropTrigger();
                if (_actionTag.Equals("REVERSE"))
                {
                    result = _invoiceType.Equals("ORDER")
                        ? ReverseSelectedOrderNumber()
                        : ReverseSelectedInvoiceNumber();
                    if (result > 0)
                    {
                        MessageBox.Show($@"{TxtVNo.Text} {_invoiceType} NUMBER {_actionTag} SUCCESSFULLY..!!");
                        if (!_invoiceType.Equals("ORDER"))
                        {
                            PrintVoucher("SB");
                        }
                        CreateDatabaseTable.CreateTrigger();
                        Close();
                        return;
                    }
                }
                else
                {
                    result = _invoiceType.Equals("RETURN") ? SavePosReturnInvoice() : SavePosInvoice();
                }

                if (result > 0)
                {
                    BtnSave.Enabled = true;
                    var resultDesc = _invoiceType.Equals("RETURN") ? "SALES RETURN INVOICE" : "SALES INVOICE";
                    CustomMessageBox.ActionSuccess(TxtVNo.Text, resultDesc, _actionTag);
                    if (_actionTag == "REVERSE")
                    {
                        EnableControl(true);
                    }
                    LblDisplayReceivedAmount.Text = TxtTenderAmount.Text;
                    LblDisplayReturnAmount.Text = TxtChangeAmount.Text;
                    if (_actionTag.Equals("SAVE"))
                    {
                        _entry.UpdateDocumentNumbering(_invoiceType.Equals("RETURN") ? "SR" : "SB", _invoiceType.Equals("RETURN") ? _returnNumberSchema : _docDesc);
                        if (ChkPrint.Checked && _actionTag == "SAVE")
                        {
                            var module = _invoiceType.Equals("RETURN") ? "SR" : "SB";
                            PrintVoucher(module);
                        }
                    }
                    _invoiceType = "ORDER";
                    _actionTag = _actionTag.Equals("UPDATE")
                        ? _actionTag
                        : _actionTag.IsValueExits() ? "SAVE" : _actionTag;
                    ClearDetails();
                    PnlInvoiceDetails.Visible = true;
                    _dialogResult = DialogResult.OK;
                    Close();
                    return;
                }
                else
                {
                    BtnSave.Enabled = true;
                    CustomMessageBox.Warning($@" ERROR OCCURS WHILE {TxtVNo.Text} {_actionTag} ..!!");
                    TxtProduct.Focus();
                }
                CreateDatabaseTable.CreateTrigger();
            }
            else
            {
                BtnSave.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            BtnSave.Enabled = true;
            CreateDatabaseTable.CreateTrigger();
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }
    }

    #endregion --------------- FOOTER EVENT ---------------

    // METHOD FOR THIS FORM

    #region --------------- METHOD ---------------

    private void PrintVoucher(string module)
    {
        try
        {
            module = module is "RSB" or "POS" ? "SB" : module;
            var design = string.Empty;

            var dtDesign = _master.GetPrintVoucherList(module);
            if (dtDesign.Rows.Count > 0)
            {
                var noOfPrint = dtDesign.Rows[0]["NoOfPrint"].ToString().GetShort();
                var designType = dtDesign.Rows[0]["DesignerPaper_Name"].GetTrimReplace();
                design = dtDesign.Rows[0]["Paper_Name"].GetTrimReplace();
                var isOnline = dtDesign.Rows[0]["Is_Online"].ToString().GetBool();
                if (!isOnline)
                {
                    return;
                }
                switch (designType)
                {
                    case "DLL":
                    {
                        var invoice = module is "SO" or "SR" ? TxtVNo.Text : _tempSalesInvoice;
                        _printFunction.PrintDirectSalesInvoice(module, TxtNetAmount.GetDecimal(), invoice, _defaultPrinter, string.Empty, design, noOfPrint);
                        break;
                    }
                    case "CRYSTAL":
                    {
                        var fromDoc = module.Equals("SR") ? TxtVNo.Text : _entry.SbMaster.SB_Invoice;
                        var location = dtDesign.Rows[0]["Paths"].GetString();
                        var result = FrmDocumentPrint.CrystalDesignPrint(fromDoc, module, location);
                        break;
                    }
                }
            }
            else
            {
                design = module switch
                {
                    "SB" => "RestaurantDesignWithVAT",
                    "SR" => "RestaurantReturnDesignWithVAT",
                    _ => design
                };
                if (design.IsBlankOrEmpty())
                {
                    design = "RestaurantDesignWithVAT";
                }
                _printFunction.PrintDirectSalesInvoice(module, TxtNetAmount.GetDecimal(), module.Equals("SR") ? TxtVNo.Text : _entry.SbMaster.SB_Invoice, _defaultPrinter, string.Empty, design, 1);
            }
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }
    }
        
    private void GlobalControl_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void FillMemberValue(int memberId)
    {
        var dtMemberInfo = _master.ReturnMemberShipValue(memberId);
        if (dtMemberInfo.Rows.Count <= 0) return;
        _memberShipId = memberId;
        LblMemberName.Text = dtMemberInfo.Rows[0]["MShipDesc"].ToString();
        LblMemberShortName.Text = dtMemberInfo.Rows[0]["MShipShortName"].ToString();
        LblMemberType.Text = dtMemberInfo.Rows[0]["MemberDesc"].ToString();

        TxtBillDiscountPercentage.Text = dtMemberInfo.Rows[0]["Discount"].GetDecimalString();
        TxtBillDiscountPercentage.Enabled = dtMemberInfo.Rows[0]["Discount"].GetDecimal() <= 0;

        var discountAmount = dtMemberInfo.Rows[0]["Discount"].GetDecimal() * TxtBasicAmount.GetDecimal() / 100;
        TxtBillDiscountAmount.Text = discountAmount.GetDecimalString();
        LblMemberAmount.Text = dtMemberInfo.Rows[0]["Balance"].GetDecimalString();
        LblTag.Text = dtMemberInfo.Rows[0]["PriceTag"].ToString();
    }

    private void SetProductInfo()
    {
        if (_productId is 0) return;
        var dtProduct = _master.GetPosProductInfo(_productId.ToString());
        if (dtProduct.Rows.Count <= 0) return;

        PnlProductDetails.Visible = true;
        PnlProductDetails.Enabled = true;
        _shortName = dtProduct.Rows[0]["PShortName"].GetString();
        LblProduct.Text = dtProduct.Rows[0]["PName"].GetString();
        TxtProduct.Text = dtProduct.Rows[0]["PName"].GetString();
        _salesRate = dtProduct.Rows[0]["PSalesRate"].GetDecimal();
        _unitId = dtProduct.Rows[0]["PUnit"].GetInt();
        _altUnitId = dtProduct.Rows[0]["PAltUnit"].GetInt();
        var altQtyConv = dtProduct.Rows[0]["PAltConv"].GetDecimal();
        var qtyConv = dtProduct.Rows[0]["PQtyConv"].GetDecimal();
        TxtUnit.Text = dtProduct.Rows[0]["UOM"].GetString();
        _taxRate = dtProduct.Rows[0]["PTax"].GetDecimal();
        _isTaxable = _taxRate > 0;
        TxtQty.Text = TxtQty.GetDecimal() is 0 ? 1.00.GetDecimalQtyString() : TxtQty.Text.GetDecimalQtyString();
        LblBarcode.Text = TxtProduct.Text;
        LblProduct.Text = TxtProduct.Text;
        LblUnit.Text = TxtUnit.Text;
        LblSalesRate.Text = _salesRate.GetDecimalString();
    }

    private bool IsValidInvoice()
    {
        if (TxtVNo.IsBlankOrEmpty())
        {
            TxtVNo.WarningMessage("VOUCHER NUMBER CANNOT BE BLANK..!!");
            return false;
        }
        if (_tagStrings.Contains(_actionTag))
        {
            if (CustomMessageBox.VoucherNoAction(_actionTag, TxtVNo.Text) == DialogResult.No)
            {
                return false;
            }

            if (TxtRemarks.IsBlankOrEmpty())
            {
                TxtRemarks.WarningMessage($"REMARKS IS REQUIRED FOR {_actionTag}");
                return false;
            }
        }

        if (_invoiceType.Equals("RETURN") || _actionTag.Equals("REVERSE"))
        {
            if (TxtRemarks.IsBlankOrEmpty())
            {
                TxtRemarks.WarningMessage("INVOICE REMARKS IS REQUIRED..!!");
                return false;
            }
        }
        if (!_tagStrings.Contains(_actionTag))
        {
            TxtBillDiscountAmount_Validating(this, null);
            TxtServiceCharge_Validating(this, null);
            TxtVatAmount_Validating(this, null);
            TxtCustomer_Validating(this, new CancelEventArgs(false));
            if (TxtCounter.Text.IsBlankOrEmpty() || _tableId is 0)
            {
                TxtCounter.WarningMessage("TERMINAL IS REQUIRED FOR BILLING..!!");
                return false;
            }

            if (TxtProduct.Text.IsBlankOrEmpty() && RGrid.RowCount is 0)
            {
                TxtProduct.WarningMessage("INVOICE PRODUCT DETAILS IS MISSING CANNOT SAVE BLANK..!!");
                return false;
            }

            if (TxtCustomer.Text.IsBlankOrEmpty() || _ledgerId is 0)
            {
                TxtCustomer.WarningMessage("INVOICE CUSTOMER DETAILS IS MISSING CANNOT SAVE BLANK..!!");
                return false;
            }

            if (TxtBillDiscountPercentage.GetDecimal() >= 100)
            {
                TxtBillDiscountPercentage.Clear();
                TxtBillDiscountPercentage.WarningMessage("DISCOUNT PERCENTAGE CAN'T MORE THEN 100%..!!");
                return false;
            }

            if (TxtBillDiscountAmount.GetDecimal() >= TxtBasicAmount.GetDecimal())
            {
                if (TxtVoucherAmount.GetDecimal() is 0)
                {
                    return true;
                }
                TxtBillDiscountPercentage.Clear();
                TxtBillDiscountPercentage.WarningMessage("DISCOUNT AMOUNT CAN'T MORE THEN INVOICE AMOUNT ..!!");
                return false;
            }
            if (TxtTenderAmount.GetDecimal() is 0 && _invoiceType.Equals("NORMAL") && !ObjGlobal.SalesIgnoreTenderAmount)
            {
                TxtTenderAmount.WarningMessage("TENDER AMOUNT CAN'T BE ZERO..!!");
                return false;
            }
        }

        return true;
    }

    private void TotalCalculationOfInvoice()
    {
        var sumColType = RGrid.Rows.OfType<DataGridViewRow>();
        var gridViewRows = sumColType as DataGridViewRow[] ?? sumColType.ToArray();

        LblItemsTotalQty.Text = gridViewRows.Sum(row => row.Cells["GTxtQty"].Value.GetDecimal()).GetDecimalString();
        LblItemsTotal.Text = gridViewRows.Sum(row => row.Cells["GTxtDisplayAmount"].Value.GetDecimal()).GetDecimalString();
        LblItemsDiscountSum.Text = gridViewRows.Sum(row => row.Cells["GTxtPDiscount"].Value.GetDecimal()).GetDecimalString();
        LblItemsNetAmount.Text = gridViewRows.Sum(row => row.Cells["GTxtDisplayNetAmount"].Value.GetDecimal()).GetDecimalString();

        TxtBasicAmount.Text = LblItemsNetAmount.GetDecimalString();

        TxtBillDiscountPercentage.Text = TxtBillDiscountPercentage.GetDecimalString();
        TxtBillDiscountAmount.Text = (TxtBillDiscountPercentage.GetDecimal() * TxtBasicAmount.GetDecimal() / 100).GetDecimalString();
        TxtVoucherAmount.Text = (TxtBasicAmount.GetDecimal() - TxtBillDiscountAmount.GetDecimal()).ToString(ObjGlobal.SysAmountFormat);

        TxtServiceChargeRate.Text = TxtServiceChargeRate.GetDecimalString();
        TxtServiceCharge.Text = (TxtServiceChargeRate.GetDecimal() * TxtVoucherAmount.GetDecimal() / 100).GetDecimalString();
        TxtTaxableAmount.Text = (TxtVoucherAmount.GetDecimal() + TxtServiceCharge.GetDecimal()).ToString(ObjGlobal.SysAmountFormat);

        TxtVatRate.Text = TxtVatRate.GetDecimalString();
        TxtVatAmount.Text = (TxtVatRate.GetDecimal() * TxtTaxableAmount.GetDecimal() / 100).GetDecimalString();
        TxtNetAmount.Text = (TxtTaxableAmount.GetDecimal() + TxtVatAmount.GetDecimal()).ToString(ObjGlobal.SysAmountFormat);

        TxtChangeAmount.Text = TxtTenderAmount.GetDecimal() > 0 ? (TxtTenderAmount.GetDecimal() - TxtNetAmount.GetDecimal()).ToString(ObjGlobal.SysAmountFormat) : 0.GetDecimalString();
        LblNumberInWords.Text = ClsMoneyConversion.MoneyConversion(TxtNetAmount.Text.GetDecimalString());
    }

    private void EnableControl(bool ctrl)
    {
        MnuAddOrder.Enabled = false;
        TxtVNo.Enabled = _tagStrings.Contains(_actionTag);
        BtnVno.Enabled = _tagStrings.Contains(_actionTag) || ctrl;
        MskDate.Enabled = MskMiti.Enabled = false;
        TxtRefVno.Visible = BtnRefVno.Visible = _invoiceType.Equals("RETURN");
        TxtRefVno.Enabled = BtnRefVno.Enabled = _invoiceType.Equals("RETURN");
        TxtProduct.Enabled = ctrl;
        TxtAltQty.Enabled = false;
        TxtAltUnit.Enabled = false;
        TxtCounter.Enabled = ctrl;
        TxtBillDiscountAmount.Enabled = ctrl && ObjGlobal.SalesSpecialDiscountTermId > 0;
        TxtBillDiscountPercentage.Enabled = TxtBillDiscountAmount.Enabled;
        TxtServiceCharge.Enabled = ctrl && ObjGlobal.SalesServiceChargeTermId > 0;
        TxtServiceChargeRate.Enabled = TxtServiceCharge.Enabled;
        TxtBasicAmount.ReadOnly = true;
        TxtBasicAmount.Enabled = ctrl;
        TxtChangeAmount.ReadOnly = true;
        TxtChangeAmount.Enabled = ctrl;
        TxtMember.Enabled = ctrl;
        TxtNetAmount.ReadOnly = true;
        TxtNetAmount.Enabled = ctrl;
        CmbPaymentType.Enabled = ctrl;
        TxtCustomer.Enabled = ctrl;
        btnCustomer.Enabled = ctrl;
        TxtProduct.Enabled = ctrl;
        TxtQty.Enabled = ctrl;
        TxtUnit.Enabled = false;

        TxtVatRate.Enabled = TxtVatRate.GetDecimal() > 0;
        TxtVatAmount.Enabled = TxtVatRate.Enabled;

        TxtRemarks.Enabled = _tagStrings.Contains(_actionTag) || ctrl;
        BtnSave.Enabled = _tagStrings.Contains(_actionTag) || ctrl;
        RGrid.ReadOnly = true;
    }

    private void ClearControl()
    {
        if (_actionTag.Equals("UPDATE") && ObjGlobal.IsIrdRegister)
        {
            Text = _actionTag.IsValueExits() ? $"RESTRO SALES INVOICE [{_actionTag}]" : "RESTRO SALES INVOICE";
        }
        TxtVNo.Clear();
        TxtVNo.Text = _actionTag.Equals("SAVE") ? TxtVNo.GetCurrentVoucherNo("RSB", _docDesc) : TxtVNo.Text;
        if (RGrid.Rows.Count > 0)
        {
            RGrid.Rows.Clear();
        }
        ClearDetails();
        LblInvoiceNo.Visible = false;
        TxtRefVno.Enabled = TxtRefVno.Visible = false;
        BtnRefVno.Enabled = BtnRefVno.Visible = false;
        TxtRefVno.Clear();
        LblItemsTotal.Text = string.Empty;
        LblItemsTotalQty.Text = string.Empty;
        LblItemsDiscountSum.Text = string.Empty;
        LblItemsNetAmount.Text = string.Empty;
        lblTax.Text = string.Empty;
        lblTaxable.Text = string.Empty;
        lblNonTaxable.Text = string.Empty;
        TxtMember.Clear();
        _memberShipId = 0;
        CmbPaymentType.SelectedIndex = 0;
        _dtPartyInfo.Clear();
        TxtCustomer.Clear();
        _ledgerId = 0;
        _ledgerId = _master.GetUserLedgerIdFromUser(ObjGlobal.LogInUser);
        _ledgerId = _ledgerId is 0 ? ObjGlobal.FinanceCashLedgerId.GetLong() : _ledgerId;
        TxtCustomer.Text = _master.GetLedgerDescription(_ledgerId);
        TxtBillDiscountAmount.Clear();
        TxtBillDiscountPercentage.Clear();
        TxtVoucherAmount.Clear();
        TxtServiceCharge.Clear();
        TxtServiceChargeRate.Clear();
        TxtTaxableAmount.Clear();
        TxtVatRate.Clear();
        TxtVatAmount.Clear();
        TxtBasicAmount.Clear();
        TxtNetAmount.Clear();
        TxtMember.Clear();
        TxtChangeAmount.Clear();
        TxtRemarks.Clear();

        var cmd = $"SELECT ST_Rate FROM AMS.ST_Term WHERE ST_ID ='{ObjGlobal.SalesServiceChargeTermId}'";
        TxtServiceChargeRate.Text = _tableType.Equals("T") ? 0.GetDecimalString() : cmd.GetQueryData().GetDecimalString();

        cmd = $"SELECT ST_Rate FROM AMS.ST_Term WHERE ST_ID ='{ObjGlobal.SalesVatTermId}'";
        TxtVatRate.Text = cmd.GetQueryData().GetDecimalString();

        _customerResult = DialogResult.None;
        _dialogResult = DialogResult.None;
        LblNumberInWords.Text = string.Empty;
        PDetails.Visible = false;
        LblVoucherCaption.Text = @"Order No";
    }

    private void ClearDetails()
    {
        _isRowUpdate = false;
        _productId = 0;
        _unitId = 0;
        _pDiscount = 0;
        _pDiscountPercentage = 0;
        _salesRate = 0;
        TxtProduct.Clear();
        TxtQty.Text = 1.GetDecimalQtyString();
        TxtUnit.Clear();
        PnlProductDetails.Visible = false;
        PnlProductDetails.Enabled = PnlProductDetails.Visible;
        LblProduct.Text = string.Empty;
        LblBarcode.Text = string.Empty;
        LblUnit.Text = string.Empty;
        LblSalesRate.Text = string.Empty;
        PnlInvoiceDetails.Visible = false;
        TxtProduct.Clear();
        _shortName = string.Empty;
    }

    private void SetDataFromGridToTextBox()
    {
        if (RGrid.CurrentRow != null)
        {
            var result = new FrmAddDescriptions();
            result.ShowDialog();
            if (result.Descriptions.IsValueExits())
            {
                RGrid.Rows[RGrid.CurrentRow.Index].Cells["GTxtNarration"].Value = result.Descriptions;
            }
        }
        _isRowUpdate = true;
        TxtProduct.Focus();
    }

    private void AddDataToGridDetails(bool isUpdate)
    {
        LblDisplayReceivedAmount.Text = string.Empty;
        LblDisplayReturnAmount.Text = string.Empty;
        if (_productId is 0 || TxtProduct.Text.IsBlankOrEmpty())
        {
            TxtProduct.WarningMessage("INVALID PRODUCT OR PLEASE CHECK THE BARCODE..!!");
            return;
        }
        if (TxtQty.Text.GetDecimal() is 0)
        {
            TxtProduct.WarningMessage("QUANTITY CANNOT BE ZERO..!!");
            return;
        }
        if (_salesRate is 0)
        {
            if (CustomMessageBox.Question(@"SALES RATE IS ZERO DO YOU WANT TO CONTINUE..!!") is DialogResult.No)
            {
                TxtProduct.Focus();
                return;
            }
        }
        var serialNo = RGrid.RowCount > 0 ? RGrid.Rows[RGrid.RowCount - 1].Cells["GTxtSNo"].Value.GetInt() + 1 : 1;


        var basicAmount = (TxtQty.GetDecimal() * _salesRate);
        var altQty = TxtAltQty.GetDecimal();
        var qty = TxtQty.GetDecimal();
        var bAmount = qty * _salesRate;
        var netAmount = (bAmount - _pDiscount);

        var sGridId = 0;
        if (!isUpdate)
        {
            RGrid.Rows.Add();
            sGridId = RGrid.RowCount - 1;
        }

        RGrid.Rows[sGridId].Cells["GTxtSNo"].Value = serialNo;
        RGrid.Rows[sGridId].Cells["GTxtProductId"].Value = _productId;
        RGrid.Rows[sGridId].Cells["GTxtShortName"].Value = _shortName;
        RGrid.Rows[sGridId].Cells["GTxtOrderTime"].Value = DateTime.Now.ToLongTimeString();
        RGrid.Rows[sGridId].Cells["GTxtProduct"].Value = TxtProduct.Text;
        RGrid.Rows[sGridId].Cells["GTxtGodownId"].Value = 0;
        RGrid.Rows[sGridId].Cells["GTxtGodown"].Value = string.Empty;
        RGrid.Rows[sGridId].Cells["GTxtAltUOMId"].Value = _altUnitId;
        RGrid.Rows[sGridId].Cells["GTxtAltUOM"].Value = TxtAltUnit.Text;
        RGrid.Rows[sGridId].Cells["GTxtUOMId"].Value = _unitId;
        RGrid.Rows[sGridId].Cells["GTxtMRP"].Value = _salesRate;
        RGrid.Rows[sGridId].Cells["GTxtUOM"].Value = TxtUnit.Text;

        RGrid.Rows[sGridId].Cells["GTxtAltQty"].Value = altQty > 0 ? altQty : string.Empty;
        RGrid.Rows[sGridId].Cells["GTxtQty"].Value = qty.GetDecimalString();
        RGrid.Rows[sGridId].Cells["GTxtDisplayRate"].Value = _salesRate.GetDecimalString();
        RGrid.Rows[sGridId].Cells["GTxtDisplayAmount"].Value = bAmount.GetDecimalString();
        RGrid.Rows[sGridId].Cells["GTxtValueBDiscount"].Value = 0;
        RGrid.Rows[sGridId].Cells["GTxtDisplayNetAmount"].Value = netAmount.GetDecimalString();

        RGrid.Rows[sGridId].Cells["GTxtDiscountRate"].Value = _pDiscountPercentage.GetDecimal();
        RGrid.Rows[sGridId].Cells["GTxtPDiscount"].Value = _pDiscount.GetDecimalString();

        RGrid.Rows[sGridId].Cells["GTxtValueRate"].Value = _salesRate;
        RGrid.Rows[sGridId].Cells["GTxtValueNetAmount"].Value = netAmount.GetDecimalString();

        RGrid.Rows[sGridId].Cells["GTxtIsTaxable"].Value = _isTaxable;
        RGrid.Rows[sGridId].Cells["GTxtTaxPriceRate"].Value = _taxRate;

        RGrid.Rows[sGridId].Cells["GTxtValueVatAmount"].Value = 0;
        RGrid.Rows[sGridId].Cells["GTxtValueTaxableAmount"].Value = 0;
        RGrid.Rows[sGridId].Cells["GTxtValueExemptedAmount"].Value = 0;

        RGrid.Rows[sGridId].Cells["GTxtNarration"].Value = string.Empty;

        RGrid.Rows[sGridId].Cells["GTxtFreeQty"].Value = 0;
        RGrid.Rows[sGridId].Cells["GTxtFreeUnitId"].Value = 0;
        _dialogResult = DialogResult.OK;
    }
    private int ReverseSelectedInvoiceNumber()
    {
        if (_invoiceType.Equals("RETURN"))
        {
            _salesReturnRepository.SrMaster.SR_Invoice = TxtVNo.Text;
            _salesReturnRepository.SrMaster.R_Invoice = true;
            _salesReturnRepository.SrMaster.Cancel_By = ObjGlobal.LogInUser;
            _salesReturnRepository.SrMaster.Cancel_Date = DateTime.Now;
            _salesReturnRepository.SrMaster.Cancel_Remarks = TxtRemarks.Text;
        }
        else
        {
            _salesInvoiceRepository.SbMaster.SB_Invoice = TxtVNo.Text;
            _salesInvoiceRepository.SbMaster.R_Invoice = true;
            _salesInvoiceRepository.SbMaster.Cancel_By = ObjGlobal.LogInUser;
            _salesInvoiceRepository.SbMaster.Cancel_Date = DateTime.Now;
            _salesInvoiceRepository.SbMaster.Cancel_Remarks = TxtRemarks.Text;
        }

        var result = _invoiceType.Equals("RETURN")
            ? _salesReturnRepository.SaveSalesReturn("REVERSE")
            : _salesInvoiceRepository.SaveSalesInvoice("REVERSE");

        return result.GetHashCode();
    }

    private int ReverseSelectedOrderNumber()
    {
        _salesOrderRepository.SoMaster.SO_Invoice = TxtVNo.Text;
        _salesOrderRepository.SoMaster.TableId = _tableId;
        _salesOrderRepository.SoMaster.R_Invoice = true;
        _salesOrderRepository.SoMaster.CancelBy = ObjGlobal.LogInUser;
        _salesOrderRepository.SoMaster.CancelDate = DateTime.Now;
        _salesOrderRepository.SoMaster.CancelReason = TxtRemarks.Text;
        return _salesOrderRepository.SaveSalesOrder(_actionTag);
    }

    private int SaveSalesOrder()
    {
        _actionTag = "SAVE";
        _salesOrderRepository.SoMaster.MasterKeyId = _salesOrderRepository.SoMaster.MasterKeyId.ReturnMasterKeyId("SO");
        TxtVNo.Text = _actionTag.Equals("SAVE") ? TxtVNo.GetCurrentVoucherNo("RSO", _orderNumber) : TxtVNo.Text;
        _salesOrderRepository.SoMaster.SO_Invoice = TxtVNo.Text;
        _salesOrderRepository.SoMaster.Invoice_Date = MskDate.Text.GetDateTime();
        _salesOrderRepository.SoMaster.Invoice_Miti = MskMiti.Text;
        _salesOrderRepository.SoMaster.Invoice_Time = DateTime.Now;
        _salesOrderRepository.SoMaster.Ref_Vno = _tempSalesInvoice;
        _salesOrderRepository.SoMaster.Ref_Date = _tempSalesInvoice.IsValueExits() ? _tempInvoiceDate.GetDateTime() : DateTime.Now;
        _salesOrderRepository.SoMaster.Ref_Miti = _tempInvoiceMiti;
        _salesOrderRepository.SoMaster.Customer_Id = _ledgerId;
        _salesOrderRepository.SoMaster.Party_Name = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["PartyName"].ToString() : string.Empty;
        _salesOrderRepository.SoMaster.Vat_No = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["VatNo"].ToString() : string.Empty;
        _salesOrderRepository.SoMaster.Contact_Person = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ContactPerson"].ToString() : string.Empty;
        _salesOrderRepository.SoMaster.Mobile_No = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Mob"].ToString() : string.Empty;
        _salesOrderRepository.SoMaster.Address = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Address"].ToString() : string.Empty;
        _salesOrderRepository.SoMaster.ChqNo = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ChequeNo"].ToString() : string.Empty;
        _salesOrderRepository.SoMaster.ChqDate = _dtPartyInfo.Rows.Count > 0 && _dtPartyInfo.Rows[0]["ChequeNo"].IsValueExits() ? DateTime.Parse(_dtPartyInfo.Rows[0]["ChequeDate"].ToString()) : DateTime.Now;
        _salesOrderRepository.SoMaster.Invoice_Type = "ORDER";
        _salesOrderRepository.SoMaster.Invoice_Mode = "RSO";
        _salesOrderRepository.SoMaster.Payment_Mode = CmbPaymentType.Text;
        _salesOrderRepository.SoMaster.DueDays = 0;
        _salesOrderRepository.SoMaster.DueDate = DateTime.Now;
        _salesOrderRepository.SoMaster.Agent_Id = _agentId;
        _salesOrderRepository.SoMaster.Subledger_Id = _subLedgerId;
        _salesOrderRepository.SoMaster.Cls1 = _departmentId;
        _salesOrderRepository.SoMaster.Cls2 = 0;
        _salesOrderRepository.SoMaster.Cls3 = 0;
        _salesOrderRepository.SoMaster.Cls4 = 0;
        _salesOrderRepository.SoMaster.Cur_Id = _currencyId > 0 ? _currencyId : ObjGlobal.SysCurrencyId;
        _salesOrderRepository.SoMaster.Cur_Rate = 1;
        _salesOrderRepository.SoMaster.CounterId = 0;
        _salesOrderRepository.SoMaster.TableId = _tableId;
        _salesOrderRepository.SoMaster.B_Amount = TxtBasicAmount.GetDecimal();
        _salesOrderRepository.SoMaster.TermRate = TxtBillDiscountPercentage.GetDecimal();
        _salesOrderRepository.SoMaster.T_Amount = TxtBillDiscountAmount.GetDecimal();
        _salesOrderRepository.SoMaster.N_Amount = TxtNetAmount.GetDecimal();
        _salesOrderRepository.SoMaster.LN_Amount = TxtNetAmount.GetDecimal();
        _salesOrderRepository.SoMaster.Tender_Amount = TxtTenderAmount.GetDecimal();
        _salesOrderRepository.SoMaster.Return_Amount = TxtChangeAmount.GetDecimal();
        _salesOrderRepository.SoMaster.V_Amount = lblTax.Text.GetDecimal();
        _salesOrderRepository.SoMaster.Tbl_Amount = lblTaxable.Text.GetDecimal();
        _salesOrderRepository.SoMaster.Action_Type = _actionTag;
        _salesOrderRepository.SoMaster.R_Invoice = false;
        _salesOrderRepository.SoMaster.No_Print = 0;
        _salesOrderRepository.SoMaster.In_Words = LblNumberInWords.Text;
        _salesOrderRepository.SoMaster.Remarks = TxtRemarks.Text;
        _salesOrderRepository.SoMaster.Audit_Lock = false;
        _salesOrderRepository.SoMaster.OrderType = OrderType.RSO;

        // SALES ORDER DETAILS
        _salesOrderRepository.DetailsList.Clear();

        if (RGrid.Rows.Count > 0)
        {
            foreach (DataGridViewRow row in RGrid.Rows)
            {
                var list = new SO_Details();
                if (row.Cells["GTxtProductId"].Value.GetLong() is 0)
                {
                    continue;
                }
                list.SO_Invoice = TxtVNo.Text;
                list.Invoice_SNo = row.Cells["GTxtSno"].Value.GetInt();
                list.P_Id = row.Cells["GTxtProductId"].Value.GetLong();
                list.Gdn_Id = row.Cells["GTxtGodownId"].Value.GetInt() > 0 ? row.Cells["GTxtGodownId"].Value.GetInt() : null;
                list.Alt_Qty = row.Cells["GTxtAltQty"].Value.GetDecimal();
                list.Alt_UnitId = row.Cells["GTxtAltUOMId"].Value.GetInt() > 0 ? row.Cells["GTxtAltUOMId"].Value.GetInt() : null;
                list.Qty = row.Cells["GTxtQty"].Value.GetDecimal();
                list.Unit_Id = row.Cells["GTxtUOMId"].Value.GetInt() > 0 ? row.Cells["GTxtUOMId"].Value.GetInt() : null;

                list.Rate = row.Cells["GTxtDisplayRate"].Value.GetDecimal();
                list.B_Amount = row.Cells["GTxtDisplayAmount"].Value.GetDecimal();
                list.T_Amount = row.Cells["GTxtPDiscount"].Value.GetDecimal();
                list.N_Amount = row.Cells["GTxtValueNetAmount"].Value.GetDecimal();
                list.AltStock_Qty = row.Cells["GTxtAltQty"].Value.GetDecimal();
                list.Stock_Qty = row.Cells["GTxtQty"].Value.GetDecimal();
                list.Narration = row.Cells["GTxtNarration"].Value.GetString();
                list.IND_Invoice = string.Empty;
                list.IND_Sno = 0;
                list.Tax_Amount = list.V_Amount = list.V_Rate = 0;
                list.Free_Unit_Id = 0;
                list.Free_Qty = list.StockFree_Qty = 0;
                list.ExtraFree_Unit_Id = 0;
                list.ExtraFree_Qty = 0;
                list.ExtraStockFree_Qty = 0;
                list.T_Product = false;
                list.S_Ledger = null;
                list.SR_Ledger = null;
                list.SZ1 = list.SZ2 = list.SZ3 = list.SZ4 = list.SZ5 = list.SZ6 = list.SZ7 = list.SZ8 = list.SZ9 = list.SZ10 = string.Empty;
                list.Serial_No = string.Empty;
                list.Batch_No = string.Empty;
                list.Exp_Date = null;
                list.Manu_Date = null;
                list.PDiscountRate = 0;
                list.PDiscount = 0;
                list.BDiscountRate = 0;
                list.BDiscount = 0;
                list.ServiceChargeRate = 0;
                list.ServiceCharge = 0;
                list.CancelNotes = list.Narration = list.IND_Invoice = list.QOT_Invoice = list.Notes = string.Empty;

                list.SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
                list.SyncGlobalId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
                if (ObjGlobal.LocalOriginId != null)
                {
                    list.SyncOriginId = ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.IsValueExits()
                        ? ObjGlobal.LocalOriginId.Value
                        : Guid.Empty;
                }

                list.SyncCreatedOn = DateTime.Now;
                list.SyncLastPatchedOn = DateTime.Now;
                list.SyncRowVersion = 1;

                _salesOrderRepository.DetailsList.Add(list);
            }
        }
        var result = _salesOrderRepository.SaveSalesOrder(_actionTag);
        return result;
    }

    private int SaveSalesOrderCancel(DataGridViewRow row, string reason, bool isUpdate)
    {
        TxtVNo.Text = TxtVNo.Text;
        _salesOrderRepository.SoMaster.SO_Invoice = TxtVNo.Text;
        _salesOrderRepository.SoMaster.Invoice_Date = MskDate.Text.GetDateTime();
        _salesOrderRepository.SoMaster.Invoice_Miti = MskMiti.Text;
        _salesOrderRepository.SoMaster.Invoice_Time = DateTime.Now;
        _salesOrderRepository.SoMaster.Ref_Vno = _tempSalesInvoice;
        _salesOrderRepository.SoMaster.Ref_Date = _tempSalesInvoice.IsValueExits() ? _tempInvoiceDate.GetDateTime() : DateTime.Now;
        _salesOrderRepository.SoMaster.Ref_Miti = _tempInvoiceMiti;
        _salesOrderRepository.SoMaster.Customer_Id = _ledgerId;
        _salesOrderRepository.SoMaster.Party_Name = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["PartyName"].ToString() : string.Empty;
        _salesOrderRepository.SoMaster.Vat_No = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["VatNo"].ToString() : string.Empty;
        _salesOrderRepository.SoMaster.Contact_Person = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ContactPerson"].ToString() : string.Empty;
        _salesOrderRepository.SoMaster.Mobile_No = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Mob"].ToString() : string.Empty;
        _salesOrderRepository.SoMaster.Address = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Address"].ToString() : string.Empty;
        _salesOrderRepository.SoMaster.ChqNo = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ChequeNo"].ToString() : string.Empty;
        _salesOrderRepository.SoMaster.ChqDate = _dtPartyInfo.Rows.Count > 0 && _dtPartyInfo.Rows[0]["ChequeNo"].IsValueExits() ? DateTime.Parse(_dtPartyInfo.Rows[0]["ChequeDate"].ToString()) : DateTime.Now;
        _salesOrderRepository.SoMaster.Invoice_Type = "ORDER";
        _salesOrderRepository.SoMaster.Invoice_Mode = "RSO";
        _salesOrderRepository.SoMaster.Payment_Mode = CmbPaymentType.Text;
        _salesOrderRepository.SoMaster.DueDays = 0;
        _salesOrderRepository.SoMaster.DueDate = DateTime.Now;
        _salesOrderRepository.SoMaster.Agent_Id = _agentId;
        _salesOrderRepository.SoMaster.Subledger_Id = _subLedgerId;
        _salesOrderRepository.SoMaster.Cls1 = _departmentId;
        _salesOrderRepository.SoMaster.Cls2 = 0;
        _salesOrderRepository.SoMaster.Cls3 = 0;
        _salesOrderRepository.SoMaster.Cls4 = 0;
        _salesOrderRepository.SoMaster.Cur_Id = _currencyId > 0 ? _currencyId : ObjGlobal.SysCurrencyId;
        _salesOrderRepository.SoMaster.Cur_Rate = 1;
        _salesOrderRepository.SoMaster.CounterId = 0;
        _salesOrderRepository.SoMaster.TableId = _tableId;
        _salesOrderRepository.SoMaster.B_Amount = TxtBasicAmount.GetDecimal();
        _salesOrderRepository.SoMaster.TermRate = TxtBillDiscountPercentage.GetDecimal();
        _salesOrderRepository.SoMaster.T_Amount = TxtBillDiscountAmount.GetDecimal();
        _salesOrderRepository.SoMaster.N_Amount = TxtNetAmount.GetDecimal();
        _salesOrderRepository.SoMaster.LN_Amount = TxtNetAmount.GetDecimal();
        _salesOrderRepository.SoMaster.Tender_Amount = TxtTenderAmount.GetDecimal();
        _salesOrderRepository.SoMaster.Return_Amount = TxtChangeAmount.GetDecimal();
        _salesOrderRepository.SoMaster.V_Amount = lblTax.Text.GetDecimal();
        _salesOrderRepository.SoMaster.Tbl_Amount = lblTaxable.Text.GetDecimal();
        _salesOrderRepository.SoMaster.Action_Type = _actionTag;
        _salesOrderRepository.SoMaster.R_Invoice = false;
        _salesOrderRepository.SoMaster.No_Print = 0;
        _salesOrderRepository.SoMaster.In_Words = LblNumberInWords.Text;
        _salesOrderRepository.SoMaster.Remarks = TxtRemarks.Text;
        _salesOrderRepository.SoMaster.Audit_Lock = false;
        _salesOrderRepository.SoMaster.OrderType = OrderType.RSO;

        _salesOrderRepository.DetailsList.Clear();
        var details = new SO_Details
        {
            SO_Invoice = TxtVNo.Text,
            Invoice_SNo = row.Cells["GTxtSNo"].Value.GetInt(),
            P_Id = row.Cells["GTxtProductId"].Value.GetLong(),
            Gdn_Id = row.Cells["GTxtGodownId"].Value.GetInt(),
            Alt_Qty = row.Cells["GTxtAltQty"].Value.GetDecimal(),
            Alt_UnitId = row.Cells["GTxtAltUOMId"].Value.GetInt(),
            Qty = row.Cells["GTxtQty"].Value.GetDecimal(),
            Unit_Id = row.Cells["GTxtUOMId"].Value.GetInt(),
            Rate = row.Cells["GTxtDisplayRate"].Value.GetDecimal(),
            B_Amount = row.Cells["GTxtDisplayAmount"].Value.GetDecimal(),
            T_Amount = row.Cells["GTxtPDiscount"].Value.GetDecimal(),
            N_Amount = row.Cells["GTxtDisplayNetAmount"].Value.GetDecimal(),
            AltStock_Qty = row.Cells["GTxtAltQty"].Value.GetDecimal(),
            Stock_Qty = row.Cells["GTxtQty"].Value.GetDecimal(),
            PDiscountRate = row.Cells["GTxtDiscountRate"].Value.GetDecimal(),
            PDiscount = row.Cells["GTxtPDiscount"].Value.GetDecimal(),
            BDiscountRate = 0,
            BDiscount = 0,
            ServiceCharge = 0,
            ServiceChargeRate = 0,
            CancelNotes = reason
        };
        _salesOrderRepository.DetailsList.Add(details);
        var result = isUpdate ? _salesOrderRepository.UpdateRestaurantOrderCancel() : _salesOrderRepository.SaveRestroOrderCancel(_actionTag);
        return result;
    }

    private int UpdateSalesOrder(bool isQty)
    {
        var serialNo = RGrid.RowCount > 0 ? RGrid.Rows[RGrid.RowCount - 1].Cells["GTxtSNo"].Value.GetInt() + 1 : 1;
        var basicAmount = (TxtQty.GetDecimal() * _salesRate);
        var altQty = TxtAltQty.GetDecimal();
        var qty = TxtQty.GetDecimal();
        var bAmount = qty * _salesRate;
        var netAmount = (bAmount - _pDiscount);

        _salesOrderRepository.DetailsList.Clear();
        var details = new SO_Details
        {
            SO_Invoice = TxtVNo.Text,
            Invoice_SNo = serialNo.GetInt(),
            Qty = TxtQty.GetDecimal(),
            Rate = _salesRate,
            BDiscount = basicAmount,
            P_Id = _productId,
            T_Amount = _pDiscount,
            PDiscountRate = _pDiscountPercentage,
            PDiscount = _pDiscount,
            N_Amount = netAmount,
            Gdn_Id = 0,
            Alt_UnitId = _altUnitId,
            Unit_Id = _unitId,
            Alt_Qty = altQty,
            MasterKeyId = _masterKeyId
        };
        _salesOrderRepository.DetailsList.Add(details);
        return _salesOrderRepository.UpdateRestroOrder(isQty);
    }

    private int SavePosInvoice()
    {
        ReturnSbVoucherNumber();
        if (_tempSalesInvoice.IsBlankOrEmpty())
        {
            CustomMessageBox.ErrorMessage("Invoice Number is Missing Please check the Invoice Numbering");
            return 0;
        }

        _salesInvoiceRepository.SbMaster.SB_Invoice = _actionTag.IsValueExits() && _actionTag != "SAVE" ? TxtVNo.Text : _tempSalesInvoice;
        _salesInvoiceRepository.SbMaster.Invoice_Date = DateTime.Now;
        _salesInvoiceRepository.SbMaster.Invoice_Miti = DateTime.Now.GetNepaliDate();
        _salesInvoiceRepository.SbMaster.Invoice_Time = DateTime.Now;
        _salesInvoiceRepository.SbMaster.PB_Vno = string.Empty;
        _salesInvoiceRepository.SbMaster.Vno_Date = MskDate.Text.GetDateTime();
        _salesInvoiceRepository.SbMaster.Vno_Miti = MskMiti.Text;
        _salesInvoiceRepository.SbMaster.Customer_Id = _ledgerId;

        _salesInvoiceRepository.SbMaster.PartyLedgerId = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["PartyLedgerId"].GetLong() : 0;
        _salesInvoiceRepository.SbMaster.Party_Name = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["PartyName"].ToString() : string.Empty;
        _salesInvoiceRepository.SbMaster.Vat_No = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["VatNo"].ToString() : string.Empty;
        _salesInvoiceRepository.SbMaster.Contact_Person = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ContactPerson"].ToString() : string.Empty;
        _salesInvoiceRepository.SbMaster.Mobile_No = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Mob"].ToString() : string.Empty;
        _salesInvoiceRepository.SbMaster.Address = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Address"].ToString() : string.Empty;
        _salesInvoiceRepository.SbMaster.ChqNo = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ChequeNo"].ToString() : string.Empty;
        _salesInvoiceRepository.SbMaster.ChqDate = _dtPartyInfo.Rows.Count > 0 && _dtPartyInfo.Rows[0]["ChequeNo"].IsValueExits() ? DateTime.Parse(_dtPartyInfo.Rows[0]["ChequeDate"].ToString()) : DateTime.Now;
        _salesInvoiceRepository.SbMaster.Invoice_Type = "LOCAL";
        _salesInvoiceRepository.SbMaster.Invoice_Mode = "RSB";
        _salesInvoiceRepository.SbMaster.Payment_Mode = CmbPaymentType.Text;
        _salesInvoiceRepository.SbMaster.DueDays = 0;
        _salesInvoiceRepository.SbMaster.DueDate = DateTime.Now;
        _salesInvoiceRepository.SbMaster.Agent_Id = _agentId;
        _salesInvoiceRepository.SbMaster.Subledger_Id = _subLedgerId;
        _salesInvoiceRepository.SbMaster.SO_Invoice = TxtVNo.Text;
        _salesInvoiceRepository.SbMaster.SO_Date = DateTime.Now;
        _salesInvoiceRepository.SbMaster.SC_Invoice = string.Empty;
        _salesInvoiceRepository.SbMaster.SC_Date = DateTime.Now;
        _salesInvoiceRepository.SbMaster.Cls1 = _departmentId;
        _salesInvoiceRepository.SbMaster.Cls2 = 0;
        _salesInvoiceRepository.SbMaster.Cls3 = 0;
        _salesInvoiceRepository.SbMaster.Cls4 = 0;
        _salesInvoiceRepository.SbMaster.Cur_Id = _currencyId > 0 ? _currencyId : ObjGlobal.SysCurrencyId;
        _salesInvoiceRepository.SbMaster.Cur_Rate = 1;
        _salesInvoiceRepository.SbMaster.CounterId = 0;
        _salesInvoiceRepository.SbMaster.TableId = _tableId;
        _salesInvoiceRepository.SbMaster.B_Amount = TxtBasicAmount.GetDecimal();

        _salesInvoiceRepository.SbMaster.SpecialDiscountRate = TxtBillDiscountPercentage.GetDecimal();
        _salesInvoiceRepository.SbMaster.SpecialDiscount = TxtBillDiscountAmount.GetDecimal();

        _salesInvoiceRepository.SbMaster.ServiceChargeRate = TxtServiceChargeRate.GetDecimal();
        _salesInvoiceRepository.SbMaster.ServiceCharge = TxtServiceCharge.GetDecimal();

        _salesInvoiceRepository.SbMaster.VatRate = TxtVatRate.GetDecimal();
        _salesInvoiceRepository.SbMaster.VatAmount = TxtVatAmount.GetDecimal();

        _salesInvoiceRepository.SbMaster.T_Amount = (TxtServiceCharge.GetDecimal() - TxtBillDiscountAmount.GetDecimal() + TxtVatAmount.GetDecimal());

        _salesInvoiceRepository.SbMaster.N_Amount = TxtNetAmount.GetDecimal();
        _salesInvoiceRepository.SbMaster.LN_Amount = TxtNetAmount.GetDecimal();
        _salesInvoiceRepository.SbMaster.Tender_Amount = TxtTenderAmount.GetDecimal();
        _salesInvoiceRepository.SbMaster.Return_Amount = TxtChangeAmount.GetDecimal();
        _salesInvoiceRepository.SbMaster.V_Amount = lblTax.Text.GetDecimal();
        _salesInvoiceRepository.SbMaster.Tbl_Amount = lblTaxable.Text.GetDecimal();
        _salesInvoiceRepository.SbMaster.Action_Type = _actionTag;
        _salesInvoiceRepository.SbMaster.R_Invoice = false;
        _salesInvoiceRepository.SbMaster.No_Print = 0;
        _salesInvoiceRepository.SbMaster.In_Words = LblNumberInWords.Text;
        _salesInvoiceRepository.SbMaster.Remarks = TxtRemarks.Text;
        _salesInvoiceRepository.SbMaster.Audit_Lock = false;

        _salesInvoiceRepository.SbMaster.CBranch_Id = ObjGlobal.SysBranchId;
        _salesInvoiceRepository.SbMaster.FiscalYearId = ObjGlobal.SysFiscalYearId;

        _salesInvoiceRepository.SbMaster.SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
        _salesInvoiceRepository.SbMaster.SyncGlobalId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
        if (ObjGlobal.LocalOriginId != null)
        {
            _salesInvoiceRepository.SbMaster.SyncOriginId = ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.IsValueExits()
                ? ObjGlobal.LocalOriginId.Value
                : Guid.Empty;
        }

        _salesInvoiceRepository.SbMaster.SyncCreatedOn = DateTime.Now;
        _salesInvoiceRepository.SbMaster.SyncLastPatchedOn = DateTime.Now;

        var sync = _salesInvoiceRepository.ReturnSyncRowVersionVoucher("SB", _salesInvoiceRepository.SbMaster.SB_Invoice);
        _salesInvoiceRepository.SbMaster.SyncRowVersion = sync;

        _salesInvoiceRepository.DetailsList.Clear();
        foreach (DataGridViewRow row in RGrid.Rows)
        {
            var exitsProduct = row.Cells["GTxtProductId"].Value.GetLong();
            if (exitsProduct is 0)
            {
                continue;
            }
            var rowExits = _salesInvoiceRepository.DetailsList.FirstOrDefault(r => r.P_Id == exitsProduct);
            if (rowExits != null)
            {
                var altQty = RGrid.Rows[row.Index].Cells["GTxtAltQty"].Value.GetDecimal();
                altQty += rowExits.Alt_Qty;
                rowExits.Alt_Qty = altQty;

                var qty = RGrid.Rows[row.Index].Cells["GTxtQty"].Value.GetDecimal();
                qty += rowExits.Qty;
                rowExits.Qty = qty;

                var rate = rowExits.Rate;

                var basicAmount = rate * qty;

                rowExits.B_Amount = basicAmount.GetDecimal();

                var pDiscount = RGrid.Rows[row.Index].Cells["GTxtPDiscount"].Value.GetDecimal();
                pDiscount += rowExits.PDiscount;

                rowExits.PDiscount = pDiscount;
                rowExits.N_Amount = basicAmount - pDiscount;
            }
            else
            {
                var list = new SB_Details();
                if (row.Cells["GTxtProductId"].Value.GetLong() is 0)
                {
                    continue;
                }
                list.SB_Invoice = _salesInvoiceRepository.SbMaster.SB_Invoice;
                list.Invoice_SNo = row.Cells["GTxtSno"].Value.GetInt();
                list.P_Id = row.Cells["GTxtProductId"].Value.GetLong();
                list.Gdn_Id = row.Cells["GTxtGodownId"].Value.GetInt();
                list.Alt_Qty = row.Cells["GTxtAltQty"].Value.GetDecimal();
                list.Alt_UnitId = row.Cells["GTxtAltUOMId"].Value.GetInt();
                list.Qty = row.Cells["GTxtQty"].Value.GetDecimal();
                list.Unit_Id = row.Cells["GTxtUOMId"].Value.GetInt();
                list.Rate = row.Cells["GTxtDisplayRate"].Value.GetDecimal();

                var termAmount = row.Cells["GTxtPDiscount"].Value.GetDecimal();
                var basicAmount = row.Cells["GTxtDisplayAmount"].Value.GetDecimal();

                list.B_Amount = basicAmount;
                list.T_Amount = termAmount;

                list.N_Amount = row.Cells["GTxtDisplayNetAmount"].Value.GetDecimal();
                list.AltStock_Qty = row.Cells["GTxtAltQty"].Value.GetDecimal();
                list.Stock_Qty = row.Cells["GTxtQty"].Value.GetDecimal();

                list.Narration = row.Cells["GTxtNarration"].Value.GetString();
                list.SO_Invoice = TxtVNo.Text;
                list.SO_Sno = row.Cells["GTxtSno"].Value.GetInt();
                list.SC_Invoice = string.Empty;
                list.SC_SNo = 0;

                var taxAmount = row.Cells["GTxtValueTaxableAmount"].Value.GetDecimal();
                var vAmount = row.Cells["GTxtValueVatAmount"].Value.GetDecimal();
                var vRate = row.Cells["GTxtTaxPriceRate"].Value.GetDecimal();

                list.Tax_Amount = taxAmount;
                list.V_Amount = vAmount;
                list.V_Rate = vRate;

                list.Free_Unit_Id = 0;
                list.Free_Qty = list.StockFree_Qty = 0;
                list.ExtraFree_Unit_Id = 0;
                list.ExtraFree_Qty = 0;
                list.ExtraStockFree_Qty = 0;

                list.T_Product = false;
                list.S_Ledger = 0;
                list.SR_Ledger = 0;

                list.SZ1 = list.SZ2 = list.SZ3 = list.SZ4 = list.SZ5 = list.SZ6 = list.SZ7 = list.SZ8 = list.SZ9 = list.SZ10 = null;
                list.Serial_No = null;
                list.Batch_No = null;
                list.Exp_Date = null;
                list.Manu_Date = null;
                list.MaterialPost = null;

                var pDiscountRate = row.Cells["GTxtDiscountRate"].Value.GetDecimal();
                var pDiscount = row.Cells["GTxtPDiscount"].Value.GetDecimal();
                var bDiscount = row.Cells["GTxtValueBDiscount"].Value.GetDecimal();

                list.PDiscountRate = pDiscountRate;
                list.PDiscount = pDiscount;
                list.BDiscountRate = TxtBillDiscountPercentage.GetDecimal();
                list.BDiscount = bDiscount;
                list.ServiceChargeRate = TxtServiceCharge.GetDecimal();
                list.ServiceCharge = 0;

                list.SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
                list.SyncGlobalId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
                if (ObjGlobal.LocalOriginId != null)
                {
                    list.SyncOriginId = ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.IsValueExits()
                        ? ObjGlobal.LocalOriginId.Value
                        : Guid.Empty;
                }

                list.SyncCreatedOn = DateTime.Now;
                list.SyncLastPatchedOn = DateTime.Now;
                list.SyncRowVersion = sync;

                _salesInvoiceRepository.DetailsList.Add(list);
            }
        }

        foreach (DataGridViewRow row in _getPaymentDetails.Rows)
        {
            var payment = new SalesPaymentMode()
            {
                SB_Invoice = TxtVNo.Text,
                LedgerId = row.Cells["LedgerId"].Value.GetLong(),
                SerialNo = row.Cells["SerialNo"].Value.GetInt(),
                Payment_Mode = row.Cells["Payment_Mode"].Value.ToString(),
                Amount = row.Cells["Amount"].Value.GetDecimal()
            };
            _salesInvoiceRepository.SbMaster.SalesPaymentModes.Add(payment);
        }

        return _salesInvoiceRepository.SaveSalesInvoice("SAVE");
    }

    private int SavePosReturnInvoice()
    {
        {
            TxtVNo.Text = TxtVNo.GetCurrentVoucherNo("SR", _docDesc);
        }
        _salesReturnRepository.SrMaster.SR_Invoice = TxtVNo.Text;
        _salesReturnRepository.SrMaster.Invoice_Date = DateTime.Now;
        _salesReturnRepository.SrMaster.Invoice_Miti = DateTime.Now.GetNepaliDate();
        _salesReturnRepository.SrMaster.Invoice_Time = DateTime.Now;
        _salesReturnRepository.SrMaster.SB_Invoice = TxtRefVno.Text;
        _salesReturnRepository.SrMaster.SB_Date = MskDate.Text.GetDateTime();
        _salesReturnRepository.SrMaster.SB_Miti = MskMiti.Text;
        _salesReturnRepository.SrMaster.Customer_ID = _ledgerId;

        _salesReturnRepository.SrMaster.PartyLedgerId = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["PartyLedgerId"].GetLong() : 0;
        _salesReturnRepository.SrMaster.Party_Name = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["PartyName"].ToString() : string.Empty;
        _salesReturnRepository.SrMaster.Vat_No = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["VatNo"].ToString() : string.Empty;
        _salesReturnRepository.SrMaster.Contact_Person = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ContactPerson"].ToString() : string.Empty;
        _salesReturnRepository.SrMaster.Mobile_No = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Mob"].ToString() : string.Empty;
        _salesReturnRepository.SrMaster.Address = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Address"].ToString() : string.Empty;
        _salesReturnRepository.SrMaster.ChqNo = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ChequeNo"].ToString() : string.Empty;
        _salesReturnRepository.SrMaster.ChqDate = _dtPartyInfo.Rows.Count > 0 && _dtPartyInfo.Rows[0]["ChequeNo"].IsValueExits() ? DateTime.Parse(_dtPartyInfo.Rows[0]["ChequeDate"].ToString()) : DateTime.Now;
        _salesReturnRepository.SrMaster.Invoice_Type = "LOCAL";
        _salesReturnRepository.SrMaster.Invoice_Mode = "RSB";
        _salesReturnRepository.SrMaster.Payment_Mode = CmbPaymentType.Text;
        _salesReturnRepository.SrMaster.DueDays = 0;
        _salesReturnRepository.SrMaster.DueDate = DateTime.Now;
        _salesReturnRepository.SrMaster.Agent_Id = _agentId;
        _salesReturnRepository.SrMaster.Subledger_Id = _subLedgerId;

        _salesReturnRepository.SrMaster.Cls1 = _departmentId;
        _salesReturnRepository.SrMaster.Cls2 = 0;
        _salesReturnRepository.SrMaster.Cls3 = 0;
        _salesReturnRepository.SrMaster.Cls4 = 0;
        _salesReturnRepository.SrMaster.Cur_Id = _currencyId > 0 ? _currencyId : ObjGlobal.SysCurrencyId;
        _salesReturnRepository.SrMaster.Cur_Rate = 1;
        _salesReturnRepository.SrMaster.CounterId = 0;
        _salesReturnRepository.SrMaster.B_Amount = TxtBasicAmount.GetDecimal();

        _salesReturnRepository.SrMaster.SpecialDiscountRate = TxtBillDiscountPercentage.GetDecimal();
        _salesReturnRepository.SrMaster.SpecialDiscount = TxtBillDiscountAmount.GetDecimal();

        _salesReturnRepository.SrMaster.ServiceChargeRate = TxtServiceChargeRate.GetDecimal();
        _salesReturnRepository.SrMaster.ServiceCharge = TxtServiceCharge.GetDecimal();

        _salesReturnRepository.SrMaster.VatRate = TxtVatRate.GetDecimal();
        _salesReturnRepository.SrMaster.VatAmount = TxtVatAmount.GetDecimal();

        var termAmount = TxtServiceCharge.GetDecimal() + TxtVatAmount.GetDecimal();
        termAmount -= TxtBillDiscountAmount.GetDecimal();
        _salesReturnRepository.SrMaster.T_Amount = termAmount;

        _salesReturnRepository.SrMaster.N_Amount = TxtNetAmount.GetDecimal();
        _salesReturnRepository.SrMaster.LN_Amount = TxtNetAmount.GetDecimal();
        _salesReturnRepository.SrMaster.Tender_Amount = TxtTenderAmount.GetDecimal();
        _salesReturnRepository.SrMaster.Return_Amount = TxtChangeAmount.GetDecimal();
        _salesReturnRepository.SrMaster.V_Amount = lblTax.Text.GetDecimal();
        _salesReturnRepository.SrMaster.Tbl_Amount = lblTaxable.Text.GetDecimal();
        _salesReturnRepository.SrMaster.Action_Type = _actionTag;
        _salesReturnRepository.SrMaster.R_Invoice = false;
        _salesReturnRepository.SrMaster.No_Print = 0;
        _salesReturnRepository.SrMaster.In_Words = LblNumberInWords.Text;
        _salesReturnRepository.SrMaster.Remarks = TxtRemarks.Text;
        _salesReturnRepository.SrMaster.Audit_Lock = false;

        foreach (DataGridViewRow viewRow in RGrid.Rows)
        {
            var details = new SR_Details();
        }

        return _salesReturnRepository.SaveSalesReturn("SAVE");
    }

    private void FillOrderInvoice(string voucherNo)
    {
        var dsSales = _entry.ReturnSalesOrderDetailsInDataSet(voucherNo);
        if (dsSales.Tables.Count <= 0)
        {
            return;
        }
        var dtMaster = dsSales.Tables[0];
        var dtDetails = dsSales.Tables[1];
        var dtBTerm = dsSales.Tables[3];
        if (dtMaster.Rows.Count < 0)
        {
            return;
        }
        _orderAction = "UPDATE";
        foreach (DataRow dr in dtMaster.Rows)
        {
            TxtVNo.Text = dr["SO_Invoice"].ToString();
            MskDate.Text = DateTime.Now.GetDateString();
            MskMiti.Text = DateTime.Now.GetNepaliDate();

            _ledgerId = _master.GetUserLedgerIdFromUser(ObjGlobal.LogInUser);
            _ledgerId = _ledgerId is 0 ? ObjGlobal.FinanceCashLedgerId.GetLong() : _ledgerId;
            TxtCustomer.Text = _master.GetLedgerDescription(_ledgerId);

            _dtPartyInfo.Rows.Clear();
            var newRow = _dtPartyInfo.NewRow();
            newRow["PartyLedgerId"] = null;
            newRow["PartyName"] = dr["Party_Name"];
            newRow["ChequeNo"] = dr["ChqNo"];
            newRow["ChequeDate"] = dr["ChqDate"].GetDateString();
            newRow["VatNo"] = dr["Vat_No"];
            newRow["ContactPerson"] = dr["Contact_Person"];
            newRow["Address"] = dr["Address"];
            newRow["Mob"] = dr["Mobile_No"];
            _dtPartyInfo.Rows.Add(newRow);

            _currencyId = dr["Cur_Id"].GetInt();
            TxtRemarks.Text = Convert.ToString(dr["Remarks"].ToString());
            TxtTenderAmount.Text = dr["Tender_Amount"].GetDecimalString();
            TxtChangeAmount.Text = dr["Return_Amount"].GetDecimalString();
            TxtBillDiscountAmount.Text = dr["T_Amount"].GetDecimalString();
            var discount = dr["T_Amount"].GetDecimal() > 0 ? dr["T_Amount"].GetDecimal() / dr["B_Amount"].GetDecimal() * 100 : 0;
            TxtBillDiscountPercentage.Text = discount.GetDecimalString();
            _masterKeyId = dr["MasterKeyId"].GetLong();
        }
        if (dtDetails.RowsCount() > 0)
        {
            var iRow = 0;
            RGrid.Rows.Clear();
            RGrid.Rows.Add(dtDetails.RowsCount());
            foreach (DataRow dr in dtDetails.Rows)
            {
                var orderTime = dr["OrderTime"].GetDateTime();
                RGrid.Rows[iRow].Cells["GTxtSNo"].Value = dr["Invoice_SNo"].ToString();
                RGrid.Rows[iRow].Cells["GTxtProductId"].Value = dr["P_Id"].ToString();
                RGrid.Rows[iRow].Cells["GTxtOrderTime"].Value = Convert.ToDateTime(orderTime).ToLongTimeString();
                RGrid.Rows[iRow].Cells["GTxtShortName"].Value = dr["PShortName"].ToString();
                RGrid.Rows[iRow].Cells["GTxtProduct"].Value = dr["PName"].ToString();
                RGrid.Rows[iRow].Cells["GTxtGodownId"].Value = 0;
                RGrid.Rows[iRow].Cells["GTxtGodown"].Value = string.Empty;
                RGrid.Rows[iRow].Cells["GTxtAltQty"].Value = dr["Alt_Qty"].GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtAltUOMId"].Value = dr["Alt_UnitId"].ToString();
                RGrid.Rows[iRow].Cells["GTxtAltUOM"].Value = TxtAltUnit.Text;
                RGrid.Rows[iRow].Cells["GTxtQty"].Value = dr["AltUnitCode"].ToString();

                var qty = dr["Qty"].GetDecimal();
                RGrid.Rows[iRow].Cells["GTxtQty"].Value = qty.GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtUOMId"].Value = dr["Unit_Id"].ToString();
                RGrid.Rows[iRow].Cells["GTxtMRP"].Value = 0;
                RGrid.Rows[iRow].Cells["GTxtUOM"].Value = dr["UnitCode"].ToString();

                var salesRate = dr["Rate"].GetDecimal();
                var pDiscount = dr["PDiscount"].GetDecimal();

                var bAmount = (salesRate * qty);
                RGrid.Rows[iRow].Cells["GTxtDisplayRate"].Value = salesRate.GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtDisplayAmount"].Value = bAmount.GetDecimalString();

                RGrid.Rows[iRow].Cells["GTxtDiscountRate"].Value = dr["PDiscountRate"].GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtPDiscount"].Value = pDiscount.GetDecimalString();

                RGrid.Rows[iRow].Cells["GTxtValueBDiscount"].Value = dr["BDiscount"].GetDecimalString();

                var taxRate = dr["PTax"].GetDecimal();
                var isTaxable = taxRate > 0;

                RGrid.Rows[iRow].Cells["GTxtDisplayNetAmount"].Value = (bAmount - pDiscount).GetDecimalString();

                RGrid.Rows[iRow].Cells["GTxtValueRate"].Value = salesRate;

                RGrid.Rows[iRow].Cells["GTxtValueNetAmount"].Value = (bAmount - pDiscount).GetDecimalString();

                RGrid.Rows[iRow].Cells["GTxtIsTaxable"].Value = isTaxable;
                RGrid.Rows[iRow].Cells["GTxtTaxPriceRate"].Value = taxRate;

                RGrid.Rows[iRow].Cells["GTxtValueVatAmount"].Value = 0;
                RGrid.Rows[iRow].Cells["GTxtValueTaxableAmount"].Value = 0;
                RGrid.Rows[iRow].Cells["GTxtValueExemptedAmount"].Value = 0;

                RGrid.Rows[iRow].Cells["GTxtNarration"].Value = string.Empty;
                RGrid.Rows[iRow].Cells["GTxtFreeQty"].Value = 0;
                RGrid.Rows[iRow].Cells["GTxtFreeUnitId"].Value = 0;

                RGrid.Rows[iRow].Cells["GTxtInvoiceNo"].Value = TxtRefVno.Text;
                RGrid.Rows[iRow].Cells["GTxtInvoiceSNo"].Value = dr["Invoice_SNo"].ToString();
                iRow++;
            }

            RGrid.CurrentCell = RGrid.Rows[RGrid.RowCount - 1].Cells[0];
            ObjGlobal.DGridColorCombo(RGrid);
            TotalCalculationOfInvoice();
            RGrid.ClearSelection();
        }
    }

    private void FillInvoiceForUpdate(string voucherNo)
    {
        var dsSales = _entry.ReturnSalesInvoiceDetailsInDataSet(voucherNo);
        if (dsSales.Tables.Count <= 0)
        {
            return;
        }
        var dtMaster = dsSales.Tables[0];
        var dtDetails = dsSales.Tables[1];
        var dtPTerm = dsSales.Tables[2];
        var dtBTerm = dsSales.Tables[3];
        if (dtMaster.Rows.Count < 0)
        {
            return;
        }
        foreach (DataRow dr in dtMaster.Rows)
        {
            TxtVNo.Text = dr["SB_Invoice"].ToString();
            MskDate.Text = dr["Invoice_Date"].GetDateString();
            MskMiti.Text = dr["Invoice_Miti"].ToString();
            TxtCustomer.Text = dr["GLName"].ToString();
            _ledgerId = dr["Customer_ID"].GetLong();

            _dtPartyInfo.Rows.Clear();
            var newRow = _dtPartyInfo.NewRow();
            newRow["PartyLedgerId"] = null;
            newRow["PartyName"] = dr["Party_Name"];
            newRow["ChequeNo"] = dr["ChqNo"];
            newRow["ChequeDate"] = dr["ChqDate"].GetDateString();
            newRow["VatNo"] = dr["Vat_No"];
            newRow["ContactPerson"] = dr["Contact_Person"];
            newRow["Address"] = dr["Address"];
            newRow["Mob"] = dr["Mobile_No"];
            _dtPartyInfo.Rows.Add(newRow);

            _currencyId = dr["Cur_Id"].GetInt();
            TxtRemarks.Text = Convert.ToString(dr["Remarks"].ToString());
            TxtTenderAmount.Text = dr["Tender_Amount"].GetDecimalString();
            TxtChangeAmount.Text = dr["Return_Amount"].GetDecimalString();
            TxtBillDiscountAmount.Text = dr["T_Amount"].GetDecimalString();
            var discount = dr["T_Amount"].GetDecimal() > 0 ? dr["T_Amount"].GetDecimal() / dr["B_Amount"].GetDecimal() * 100 : 0;
            TxtBillDiscountPercentage.Text = discount.GetDecimalString();
        }

        if (dtDetails.RowsCount() > 0)
        {
            var iRow = 0;
            RGrid.Rows.Clear();
            RGrid.Rows.Add(dtDetails.RowsCount());
            foreach (DataRow dr in dtDetails.Rows)
            {
                RGrid.Rows[iRow].Cells["GTxtSNo"].Value = dr["Invoice_SNo"].ToString();
                RGrid.Rows[iRow].Cells["GTxtProductId"].Value = dr["P_Id"].ToString();
                RGrid.Rows[iRow].Cells["GTxtShortName"].Value = dr["PShortName"].ToString();
                RGrid.Rows[iRow].Cells["GTxtProduct"].Value = dr["PName"].ToString();
                RGrid.Rows[iRow].Cells["GTxtGodownId"].Value = 0;
                RGrid.Rows[iRow].Cells["GTxtGodown"].Value = string.Empty;
                RGrid.Rows[iRow].Cells["GTxtAltQty"].Value = dr["Alt_Qty"].GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtAltUOMId"].Value = dr["Alt_UnitId"].ToString();
                RGrid.Rows[iRow].Cells["GTxtAltUOM"].Value = TxtAltUnit.Text;
                RGrid.Rows[iRow].Cells["GTxtQty"].Value = dr["AltUnitCode"].ToString();
                var qty = dr["Qty"].GetDecimal();
                RGrid.Rows[iRow].Cells["GTxtQty"].Value = qty.GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtUOMId"].Value = dr["Unit_Id"].ToString();
                RGrid.Rows[iRow].Cells["GTxtMRP"].Value = 0;
                RGrid.Rows[iRow].Cells["GTxtUOM"].Value = dr["UnitCode"].ToString();
                var salesRate = dr["Rate"].GetDecimal();
                var pDiscount = dr["PDiscount"].GetDecimal();

                RGrid.Rows[iRow].Cells["GTxtDisplayRate"].Value = salesRate.GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtDisplayAmount"].Value =
                    (salesRate * qty).ToString(ObjGlobal.SysAmountFormat);
                RGrid.Rows[iRow].Cells["GTxtDiscountRate"].Value = dr["PDiscountRate"].GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtPDiscount"].Value = pDiscount.GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtValueBDiscount"].Value = dr["BDiscount"].GetDecimalString();
                var taxRate = dr["PTax"].GetDecimal();
                var isTaxable = taxRate > 0;
                RGrid.Rows[iRow].Cells["GTxtDisplayNetAmount"].Value =
                    (qty * salesRate - pDiscount).GetDecimalString();
                var taxableSalesRate = isTaxable ? salesRate / (decimal)1.13 : salesRate;
                RGrid.Rows[iRow].Cells["GTxtValueRate"].Value = taxableSalesRate;
                RGrid.Rows[iRow].Cells["GTxtValueNetAmount"].Value = taxableSalesRate * qty;
                RGrid.Rows[iRow].Cells["GTxtIsTaxable"].Value = isTaxable;
                RGrid.Rows[iRow].Cells["GTxtTaxPriceRate"].Value = taxRate;
                var taxableAmount = isTaxable ? RGrid.Rows[iRow].Cells["GTxtDisplayNetAmount"].Value.GetDecimal() / (decimal)1.13 : RGrid.Rows[iRow].Cells["GTxtValueNetAmount"].Value.GetDecimal();
                var netAmount = RGrid.Rows[iRow].Cells["GTxtDisplayNetAmount"].Value.GetDecimal();
                RGrid.Rows[iRow].Cells["GTxtValueVatAmount"].Value = isTaxable ? netAmount - taxableAmount : 0;
                RGrid.Rows[iRow].Cells["GTxtValueTaxableAmount"].Value = isTaxable ? taxableAmount : 0;
                RGrid.Rows[iRow].Cells["GTxtValueExemptedAmount"].Value = isTaxable ? 0 : taxableAmount;

                RGrid.Rows[iRow].Cells["GTxtNarration"].Value = string.Empty;
                RGrid.Rows[iRow].Cells["GTxtFreeQty"].Value = 0;
                RGrid.Rows[iRow].Cells["GTxtFreeUnitId"].Value = 0;
                RGrid.Rows[iRow].Cells["GTxtInvoiceNo"].Value = TxtRefVno.Text;
                RGrid.Rows[iRow].Cells["GTxtInvoiceSNo"].Value = dr["Invoice_SNo"].ToString();
                iRow++;
            }

            RGrid.CurrentCell = RGrid.Rows[RGrid.RowCount - 1].Cells[0];
            ObjGlobal.DGridColorCombo(RGrid);
            TotalCalculationOfInvoice();
            RGrid.ClearSelection();
        }

        if (dtBTerm.Rows.Count <= 0)
        {
            return;
        }

        var rAmount = TxtBillDiscountAmount.GetDecimal() > 0 ? TxtBillDiscountAmount.GetDecimal() / TxtBasicAmount.GetDecimal() : 0;
        for (var i = 0; i < RGrid.RowCount; i++)
        {
            var basicAmount = RGrid.Rows[i].Cells["GTxtDisplayNetAmount"].Value.GetDecimal();
            var dAmount = rAmount is > 0 ? basicAmount * rAmount : 0;
            var isTaxable = RGrid.Rows[i].Cells["GTxtIsTaxable"].Value.GetBool();
            var netAmount = basicAmount - dAmount;
            var taxableAmount = isTaxable ? netAmount / (decimal)1.13 : 0;
            RGrid.Rows[i].Cells["GTxtValueBDiscount"].Value = dAmount;
            RGrid.Rows[i].Cells["GTxtValueVatAmount"].Value = isTaxable ? netAmount - taxableAmount : 0;
            RGrid.Rows[i].Cells["GTxtValueTaxableAmount"].Value = isTaxable ? taxableAmount : 0;
            RGrid.Rows[i].Cells["GTxtValueExemptedAmount"].Value = isTaxable ? 0 : taxableAmount;
        }
    }

    private void FillInvoiceData(string voucherNo)
    {
        var dsSales = _entry.ReturnSalesInvoiceDetailsInDataSet(voucherNo);
        if (dsSales.Tables.Count <= 0 || dsSales.Tables[0].Rows.Count < 0)
        {
            return;
        }
        var dtMaster = dsSales.Tables[0];

        foreach (DataRow dr in dtMaster.Rows)
        {
            //_selectedInvoice = _invoiceType.Equals("NORMAL") ? TxtVNo.Text : TxtRefVno.Text;
            MskDate.Text = _invoiceType.Equals("RETURN") ? DateTime.Now.GetDateString() : dr["Invoice_Date"].GetDateString();
            MskMiti.Text = _invoiceType.Equals("RETURN") ? MskMiti.GetNepaliDate(MskDate.Text) : dr["Invoice_Miti"].ToString();

            if (dr["PB_Vno"].ToString() != string.Empty && _invoiceType.Equals("NORMAL"))
            {
                TxtRefVno.Text = Convert.ToString(dr["PB_Vno"].ToString());
                _refInvoiceMiti = dr["Vno_Miti"].GetDateString();
                ObjGlobal.ReturnEnglishDate(_refInvoiceMiti);
            }
            else if (_invoiceType.Equals("RETURN"))
            {
                TxtRefVno.Text = dr["SB_Invoice"].ToString();
                _refInvoiceMiti = dr["Invoice_Miti"].ToString();
                dr["Invoice_Date"].ToString();
            }

            TxtCustomer.Text = dr["GLName"].ToString();
            _ledgerId = dr["Customer_ID"].GetLong();

            _dtPartyInfo.Rows.Clear();
            var newRow = _dtPartyInfo.NewRow();
            newRow["PartyLedgerId"] = null;
            newRow["PartyName"] = dr["Party_Name"];
            newRow["ChequeNo"] = dr["ChqNo"];
            newRow["ChequeDate"] = dr["ChqDate"].GetDateString();
            newRow["VatNo"] = dr["Vat_No"];
            newRow["ContactPerson"] = dr["Contact_Person"];
            newRow["Address"] = dr["Address"];
            newRow["Mob"] = dr["Mobile_No"];
            _dtPartyInfo.Rows.Add(newRow);

            _currencyId = dr["Cur_Id"].GetInt();
            TxtRemarks.Text = Convert.ToString(dr["Remarks"].ToString());
            TxtTenderAmount.Text = dr["Tender_Amount"].GetDecimalString();
            TxtChangeAmount.Text = dr["Return_Amount"].GetDecimalString();
        }

        if (dsSales.Tables[1].Rows.Count <= 0) return;
        {
            var iRow = 0;
            RGrid.Rows.Clear();
            RGrid.Rows.Add(dsSales.Tables[1].Rows.Count);
            foreach (DataRow dr in dsSales.Tables[1].Rows)
            {
                RGrid.Rows[iRow].Cells["GTxtSNo"].Value = dr["Invoice_SNo"].ToString();
                RGrid.Rows[iRow].Cells["GTxtProductId"].Value = dr["P_Id"].ToString();
                RGrid.Rows[iRow].Cells["GTxtShortName"].Value = dr["PShortName"].ToString();
                RGrid.Rows[iRow].Cells["GTxtProduct"].Value = dr["PName"].ToString();
                RGrid.Rows[iRow].Cells["GTxtGodownId"].Value = 0;
                RGrid.Rows[iRow].Cells["GTxtGodown"].Value = string.Empty;
                RGrid.Rows[iRow].Cells["GTxtAltQty"].Value = dr["Alt_Qty"].GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtAltUOMId"].Value = dr["Alt_UnitId"].ToString();
                RGrid.Rows[iRow].Cells["GTxtAltUOM"].Value = dr["AltUnitCode"].ToString();
                RGrid.Rows[iRow].Cells["GTxtQty"].Value = dr["Qty"].GetDecimalQtyString();
                RGrid.Rows[iRow].Cells["GTxtUOMId"].Value = dr["Unit_Id"].ToString();
                RGrid.Rows[iRow].Cells["GTxtMRP"].Value = 0;
                RGrid.Rows[iRow].Cells["GTxtUOM"].Value = dr["UnitCode"].ToString();
                var salesRate = dr["Rate"].GetDecimal();
                var pDiscount = dr["PDiscount"].GetDecimal();

                RGrid.Rows[iRow].Cells["GTxtDisplayRate"].Value = dr["Rate"].GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtDisplayAmount"].Value = dr["B_Amount"].GetDecimalString();

                RGrid.Rows[iRow].Cells["GTxtDiscountRate"].Value = dr["PDiscountRate"].GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtPDiscount"].Value = dr["T_Amount"].GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtValueBDiscount"].Value = dr["BDiscount"].GetDecimalString();

                var taxRate = dr["PTax"].GetDecimal();
                var isTaxable = taxRate > 0;

                RGrid.Rows[iRow].Cells["GTxtDisplayNetAmount"].Value = dr["N_Amount"].GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtValueRate"].Value = dr["Rate"].GetDecimalString(); ;
                RGrid.Rows[iRow].Cells["GTxtValueNetAmount"].Value = dr["N_Amount"].GetDecimalString();

                RGrid.Rows[iRow].Cells["GTxtIsTaxable"].Value = isTaxable;
                RGrid.Rows[iRow].Cells["GTxtTaxPriceRate"].Value = taxRate;

                var taxableAmount = RGrid.Rows[iRow].Cells["GTxtValueNetAmount"].Value.GetDecimal();

                var netAmount = RGrid.Rows[iRow].Cells["GTxtDisplayNetAmount"].Value.GetDecimal();

                RGrid.Rows[iRow].Cells["GTxtValueVatAmount"].Value = 0;
                RGrid.Rows[iRow].Cells["GTxtValueTaxableAmount"].Value = 0;
                RGrid.Rows[iRow].Cells["GTxtValueExemptedAmount"].Value = 0;

                RGrid.Rows[iRow].Cells["GTxtNarration"].Value = string.Empty;
                RGrid.Rows[iRow].Cells["GTxtFreeQty"].Value = 0;
                RGrid.Rows[iRow].Cells["GTxtFreeUnitId"].Value = 0;
                RGrid.Rows[iRow].Cells["GTxtInvoiceNo"].Value = TxtRefVno.Text;
                RGrid.Rows[iRow].Cells["GTxtInvoiceSNo"].Value = dr["Invoice_SNo"].ToString();
                iRow++;
            }
            if (dsSales.Tables[3].Rows.Count > 0)
            {
                foreach (DataRow row in dsSales.Tables[3].Rows)
                {
                    if (row["TermId"].GetInt() == ObjGlobal.SalesSpecialDiscountTermId)
                    {
                        TxtBillDiscountPercentage.Text = row["TermRate"].GetDecimalString();
                        TxtBillDiscountAmount.Text = row["TermAmt"].GetDecimalString();
                    }

                    if (row["TermId"].GetInt() == ObjGlobal.SalesServiceChargeTermId)
                    {
                        TxtServiceChargeRate.Text = row["TermRate"].GetDecimalString();
                        TxtServiceCharge.Text = row["TermAmt"].GetDecimalString();
                    }

                    if (row["TermId"].GetInt() == ObjGlobal.SalesVatTermId)
                    {
                        TxtVatRate.Text = row["TermRate"].GetDecimalString();
                        TxtVatAmount.Text = row["TermAmt"].GetDecimalString();
                    }
                }
            }

            RGrid.CurrentCell = RGrid.Rows[RGrid.RowCount - 1].Cells[0];
            ObjGlobal.DGridColorCombo(RGrid);
            TotalCalculationOfInvoice();
            RGrid.ClearSelection();
        }
    }

    private void FillReturnInvoiceData(string voucherNo)
    {
        var dsSales = _entry.ReturnSalesReturnDetailsInDataSet(voucherNo);
        if (dsSales.Tables.Count <= 0) return;
        foreach (DataRow dr in dsSales.Tables[0].Rows)
        {
            //_selectedInvoice = TxtVNo.Text;
            MskDate.Text = DateTime.Now.GetDateString();
            MskMiti.Text = MskDate.GetNepaliDate(MskDate.Text);

            if (dr["SB_Invoice"].ToString() != string.Empty)
            {
                TxtRefVno.Text = Convert.ToString(dr["SB_Invoice"].ToString());
                _refInvoiceMiti = dr["SB_Date"].GetDateString();
                dr["SB_Miti"].ToString();
            }

            TxtCustomer.Text = dr["GLName"].ToString();
            _ledgerId = dr["Customer_ID"].GetLong();

            _dtPartyInfo.Rows.Clear();
            var newRow = _dtPartyInfo.NewRow();
            newRow["PartyLedgerId"] = null;
            newRow["PartyName"] = dr["Party_Name"];
            newRow["ChequeNo"] = dr["ChqNo"];
            newRow["ChequeDate"] = dr["ChqDate"].GetDateString();
            newRow["VatNo"] = dr["Vat_No"];
            newRow["ContactPerson"] = dr["Contact_Person"];
            newRow["Address"] = dr["Address"];
            newRow["Mob"] = dr["Mobile_No"];
            _dtPartyInfo.Rows.Add(newRow);

            _currencyId = dr["Cur_Id"].GetInt();
            TxtRemarks.Text = Convert.ToString(dr["Remarks"].ToString());
        }

        if (dsSales.Tables[1].Rows.Count <= 0) return;
        {
            var iRow = 0;
            RGrid.Rows.Clear();
            RGrid.Rows.Add(dsSales.Tables[1].Rows.Count);
            foreach (DataRow dr in dsSales.Tables[1].Rows)
            {
                RGrid.Rows[iRow].Cells["GTxtSNo"].Value = dr["Invoice_SNo"].ToString();
                RGrid.Rows[iRow].Cells["GTxtProductId"].Value = dr["P_Id"].ToString();
                RGrid.Rows[iRow].Cells["GTxtShortName"].Value = dr["PShortName"].ToString();
                RGrid.Rows[iRow].Cells["GTxtProduct"].Value = dr["PName"].ToString();
                RGrid.Rows[iRow].Cells["GTxtGodownId"].Value = 0;
                RGrid.Rows[iRow].Cells["GTxtGodown"].Value = string.Empty;
                RGrid.Rows[iRow].Cells["GTxtAltQty"].Value = dr["Alt_Qty"].GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtAltUOMId"].Value = dr["Alt_UnitId"].ToString();
                RGrid.Rows[iRow].Cells["GTxtAltUOM"].Value = TxtAltUnit.Text;
                RGrid.Rows[iRow].Cells["GTxtQty"].Value = dr["AltUnitCode"].ToString();
                var qty = dr["Qty"].GetDecimal();
                RGrid.Rows[iRow].Cells["GTxtQty"].Value = qty.GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtUOMId"].Value = dr["Unit_Id"].ToString();
                RGrid.Rows[iRow].Cells["GTxtMRP"].Value = 0;
                RGrid.Rows[iRow].Cells["GTxtUOM"].Value = dr["UnitCode"].ToString();
                var salesRate = dr["Rate"].GetDecimal();
                var pDiscount = dr["PDiscount"].GetDecimal();

                RGrid.Rows[iRow].Cells["GTxtDisplayRate"].Value = salesRate.GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtDisplayAmount"].Value =
                    (salesRate * qty).ToString(ObjGlobal.SysAmountFormat);
                RGrid.Rows[iRow].Cells["GTxtDiscountRate"].Value = dr["PDiscountRate"].GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtPDiscount"].Value = pDiscount.GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtValueBDiscount"].Value = dr["BDiscount"].GetDecimalString();
                var taxRate = dr["PTax"].GetDecimal();
                var isTaxable = taxRate > 0;
                RGrid.Rows[iRow].Cells["GTxtDisplayNetAmount"].Value =
                    (qty * salesRate - pDiscount).GetDecimalString();
                var taxableSalesRate = isTaxable
                    ? salesRate / (decimal)1.13
                    : salesRate;
                RGrid.Rows[iRow].Cells["GTxtValueRate"].Value = taxableSalesRate;
                RGrid.Rows[iRow].Cells["GTxtValueNetAmount"].Value = taxableSalesRate * qty;
                RGrid.Rows[iRow].Cells["GTxtIsTaxable"].Value = isTaxable;
                RGrid.Rows[iRow].Cells["GTxtTaxPriceRate"].Value = taxRate;
                var taxableAmount = isTaxable
                    ? RGrid.Rows[iRow].Cells["GTxtDisplayNetAmount"].Value.GetDecimal() / (decimal)1.13
                    : RGrid.Rows[iRow].Cells["GTxtValueNetAmount"].Value.GetDecimal();
                var netAmount = RGrid.Rows[iRow].Cells["GTxtDisplayNetAmount"].Value.GetDecimal();
                RGrid.Rows[iRow].Cells["GTxtValueVatAmount"].Value = isTaxable ? netAmount - taxableAmount : 0;
                RGrid.Rows[iRow].Cells["GTxtValueTaxableAmount"].Value = isTaxable ? taxableAmount : 0;
                RGrid.Rows[iRow].Cells["GTxtValueExemptedAmount"].Value = isTaxable ? 0 : taxableAmount;

                RGrid.Rows[iRow].Cells["GTxtNarration"].Value = string.Empty;
                RGrid.Rows[iRow].Cells["GTxtFreeQty"].Value = 0;
                RGrid.Rows[iRow].Cells["GTxtFreeUnitId"].Value = 0;
                RGrid.Rows[iRow].Cells["GTxtInvoiceNo"].Value = TxtRefVno.Text;
                RGrid.Rows[iRow].Cells["GTxtInvoiceSNo"].Value = dr["Invoice_SNo"].ToString();
                iRow++;
            }

            RGrid.CurrentCell = RGrid.Rows[RGrid.RowCount - 1].Cells[0];
            ObjGlobal.DGridColorCombo(RGrid);
            TotalCalculationOfInvoice();
            RGrid.ClearSelection();
        }
    }

    private void CashAndBankValidation(string ledgerType)
    {
        if (TxtCustomer.IsBlankOrEmpty()) return;
        var partyInfo = new FrmPartyInfo(ledgerType, _dtPartyInfo, "PB");
        partyInfo.ShowDialog();
        if (_dtPartyInfo.Rows.Count > 0)
        {
            _dtPartyInfo.Rows.Clear();
        }

        if (partyInfo.PartyInfo.Rows.Count > 0)
        {
            foreach (DataRow row in partyInfo.PartyInfo.Rows)
            {
                var dr = _dtPartyInfo.NewRow();
                dr["PartyLedgerId"] = row["PartyLedgerId"];
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
    }

    private void InitialiseDataTable()
    {
        _dtPTerm.Reset();
        _dtPTerm = _master.GetBillingTerm();

        _dtPartyInfo.Reset();
        _dtPartyInfo = _master.GetPartyInfo();
    }

    private void LedgerCurrentBalance(long selectLedgerId)
    {
        if (selectLedgerId is 0)
        {
            return;
        }
        var date = MskDate.MaskCompleted ? MskDate.Text.GetSystemDate() : DateTime.Now.GetSystemDate();
        var dtCustomer = ClsMasterSetup.LedgerInformation(selectLedgerId, date);
        if (dtCustomer.Rows.Count > 0)
        {
            lblPan.Text = dtCustomer.Rows[0]["PanNo"].ToString();
            lblCreditDays.Text = dtCustomer.Rows[0]["CrDays"].GetDecimalString();
            lblCrLimit.Text = dtCustomer.Rows[0]["CrLimit"].GetDecimalString();
            var result = dtCustomer.Rows[0]["Amount"].GetDouble();
            lbl_CurrentBalance.Text = result > 0
                ? $"{Math.Abs(result).GetDecimalString()} Dr" :
                result < 0 ? $"{Math.Abs(result).GetDecimalString()} Cr" : "0";
            var customerType = dtCustomer.Rows[0]["CrTYpe"].GetString();
            if (customerType.GetUpper() is "CASH" or "BANK")
            {
                if (CmbPaymentType.Text is "CREDIT" or "OTHER")
                {
                    TxtCustomer.WarningMessage("SELECTED CUSTOMER AND PAYMENT MODE IS MIS MATCH..!!");
                    return;
                }
            }
            if (_actionTag != "SAVE")
            {
                return;
            }
            _agentId = dtCustomer.Rows[0]["AgentId"].GetInt();
            _currencyId = dtCustomer.Rows[0]["CurrId"].GetInt();
        }
        else
        {
            lblPan.Text = 0.GetDecimalString();
            lblCreditDays.Text = 0.GetDecimalString();
            lblCrLimit.Text = 0.GetDecimalString();
            lbl_CurrentBalance.Text = 0.GetDecimalString();
        }
    }

    private void ReturnSrVoucherNumber()
    {
        var dt = _master.IsExitsCheckDocumentNumbering("SR");
        if (dt?.Rows.Count is 1)
        {
            _returnNumberSchema = dt.Rows[0]["DocDesc"].ToString();
            TxtVNo.Text =
                ObjGlobal.ReturnDocumentNumbering("AMS.SR_Master", "SR_Invoice", "SR", _returnNumberSchema);
        }
        else if (dt != null && dt.Rows.Count > 1)
        {
            using var wnd = new FrmNumberingScheme("SR", "AMS.SR_Master", "SR_Invoice");
            if (wnd.ShowDialog() != DialogResult.OK) return;
            if (string.IsNullOrEmpty(wnd.VNo)) return;
            _docDesc = wnd.Description;
            TxtVNo.Text = wnd.VNo;
        }
        else if (dt?.Rows.Count is 0)
        {
            MessageBox.Show(@"VOUCHER NUMBER SCHEME NOT FOUND..!!", ObjGlobal.Caption);
            TxtVNo.Focus();
        }
    }

    private void ReturnSbVoucherNumber()
    {
        var dt = _master.IsExitsCheckDocumentNumbering("RSB");
        if (dt?.Rows.Count is 1)
        {
            _docDesc = dt.Rows[0]["DocDesc"].ToString();
            _tempSalesInvoice = ObjGlobal.ReturnDocumentNumbering("AMS.SB_Master", "SB_Invoice", "RSB", _docDesc);
        }
        else if (dt?.Rows.Count > 1)
        {
            _docDesc = dt.Rows[0]["DocDesc"].ToString();
            _tempSalesInvoice = ObjGlobal.ReturnDocumentNumbering("AMS.SB_Master", "SB_Invoice", "RSB", _docDesc);
        }
        else if (dt?.Rows.Count is 0)
        {
            MessageBox.Show(@"VOUCHER NUMBER SCHEME NOT FOUND..!!", ObjGlobal.Caption);
            TxtVNo.Focus();
        }
    }

    private void ReturnOrderVoucherNumber()
    {
        _masterKeyId = _masterKeyId.ReturnMasterKeyId("SO");
        var dt = _master.IsExitsCheckDocumentNumbering("RSO");
        if (dt?.Rows.Count is 1)
        {
            _orderNumber = dt.Rows[0]["DocDesc"].ToString();
            TxtVNo.Text = TxtVNo.GetCurrentVoucherNo("RSO", _orderNumber);
        }
        else if (dt != null && dt.Rows.Count > 1)
        {
            _orderNumber = dt.Rows[0]["DocDesc"].ToString();
            TxtVNo.Text = TxtVNo.GetCurrentVoucherNo("RSO", _orderNumber);
        }
        else if (dt?.Rows.Count is 0)
        {
            MessageBox.Show(@"VOUCHER NUMBER SCHEME NOT FOUND..!!", ObjGlobal.Caption);
            TxtVNo.Focus();
        }
    }

    private void OpenProductList()
    {
        var result = GetMasterList.GetRestroProduct(_actionTag, MskDate.Text);
        if (result.id > 0)
        {
            LblProduct.Text = result.description;
            _productId = result.id;
            SetProductInfo();
        }

        TxtProduct.Focus();
    }

    #endregion --------------- METHOD ---------------

    // OBJECT FOR THIS FORM

    #region --------------- OBJECT ---------------
    private int _unitId;
    private int _altUnitId;
    private int _agentId;
    private int _tableId;
    private int _currencyId;
    private int _subLedgerId;
    private int _departmentId;
    private int _memberShipId;

    private long _productId;
    private long _ledgerId;
    private long _masterKeyId;

    private bool _isRowUpdate;
    private bool _isTaxable;

    private string _actionTag = @"SAVE";
    private string _orderAction = @"SAVE";
    private string _docDesc;
    private string _orderNumber;
    private string _returnNumberSchema;

    private string _invoiceType = "ORDER";
    private string _tempSalesInvoice;
    private string _tempInvoiceDate;
    private string _tempInvoiceMiti;
    private string _refInvoiceMiti;
    private string _shortName;

    private decimal _salesRate;
    private decimal _taxRate;
    private decimal _pDiscount;
    private decimal _pDiscountPercentage;

    private readonly string _defaultPrinter;
    private readonly string _tableType;
    private readonly string[] _tagStrings = ["DELETE", "REVERSE", "RETURN"];

    private readonly ISalesEntry _entry = new ClsSalesEntry();

    private readonly IMasterSetup _master = new ClsMasterSetup();
    private readonly ISalesDesign _objDesign = new SalesEntryDesign();

    private readonly ISalesOrderRepository _salesOrderRepository;

    private readonly ISalesInvoiceRepository _salesInvoiceRepository;

    private readonly ISalesReturn _salesReturnRepository;

    private DialogResult _customerResult = DialogResult.None;
    public DialogResult _dialogResult = DialogResult.None;

    private DataTable _dtPTerm = new("IsPTermExitsTerm");
    private DataTable _dtPartyInfo = new("PartyInfo");
    private DataGridView _getPaymentDetails = new();
    private readonly ClsPrintFunction _printFunction;


    #endregion --------------- OBJECT ---------------
}