using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.SalesMaster.SalesInvoice;
using DatabaseModule.DataEntry.SalesMaster.SalesReturn;
using DatabaseModule.Domains.BarcodePrint;
using MrBLL.DataEntry.Common;
using MrBLL.Domains.POS.Master;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.DataEntry.Design;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.Interface.SalesMaster;
using MrDAL.DataEntry.Interface.SalesReturn;
using MrDAL.DataEntry.SalesMaster;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Models.Common;
using MrDAL.Models.Custom;
using MrDAL.Utility.dbMaster;
using MrDAL.Utility.Server;
using PrintControl.PrintMethod;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Color = System.Drawing.Color;
namespace MrBLL.Domains.POS.Entry;

public partial class FrmPSalesInvoice : MrForm
{
    // POINT OF SALES
    #region --------------- SALES INVOICE ---------------
    public FrmPSalesInvoice()
    {
        InitializeComponent();

        _master = new ClsMasterSetup();
        _salesInvoice = new SalesInvoiceRepository();
        _design = new SalesEntryDesign();
        _returnInvoice = new SalesReturnRepository();
        _entry = new ClsSalesEntry();
        _printFunction = new ClsPrintFunction();

        InitialiseDataTable();

        isTaxBilling = ObjGlobal.SalesVatTermId > 0;
        _design.GetPointOfSalesDesign(RGrid, "POS");
        _master.BindPaymentType(CmbPaymentType);

        if (isTaxBilling)
        {
            var cmd = $"SELECT ST_Rate FROM AMS.ST_Term WHERE ST_ID ='{ObjGlobal.SalesVatTermId}'";
            var termRate = cmd.GetQueryData().GetDecimalString();

            isTaxBilling = termRate.GetDecimal() > 0;
        }
        var (invoice, avtInvoice) = _salesInvoice.GetPointOfSalesDesign();
        if (invoice.IsValueExits() || avtInvoice.IsValueExits())
        {
            ObjGlobal.SysDefaultInvoiceDesign = ObjGlobal.SysDefaultInvoiceDesign.IsBlankOrEmpty() ? invoice : ObjGlobal.SysDefaultInvoiceDesign;
            ObjGlobal.SysDefaultAbtInvoiceDesign = ObjGlobal.SysDefaultAbtInvoiceDesign.IsBlankOrEmpty() ? avtInvoice : ObjGlobal.SysDefaultAbtInvoiceDesign;
        }
        _productModels = new List<SalesInvoiceProductModel>();
        TxtBarcode.ReadOnly = ObjGlobal.SysIsBarcodeSearch;
        ObjGlobal.DGridColorCombo(RGrid);
    }
    private void FrmPSalesInvoice_Shown(object sender, EventArgs e)
    {
        var frmCounter = new FrmCounterTagList();
        frmCounter.ShowDialog();
        TxtCounter.Text = frmCounter.SelectedCounter;
        CounterId = frmCounter.SelectedCounterId;
        if (!TxtCounter.Text.IsValueExits())
        {
            Close();
            return;
        }
        TxtBarcode.Focus();
    }

    private void FrmPSalesInvoice_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F6)
        {
            BtnMember_Click(sender, e);
        }
        else if (e.KeyCode is Keys.F9)
        {
            BtnHold_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.U)
        {
            if (ObjGlobal.IsIrdRegister)
            {
                return;
            }
            BtnNew.PerformClick();
            _actionTag = "UPDATE";
        }
    }
    private void FrmPSalesInvoice_Load(object sender, EventArgs e)
    {
        Resize += FrmPSalesInvoice_Resize;
        FrmPSalesInvoice_Resize(this, EventArgs.Empty);
        _actionTag = string.Empty;
        if (isTaxBilling)
        {
            isTaxBilling = _master.GetTaxRate();
        }
        BtnNew_Click(sender, e);
        //GetAndSyncStockDetails(); // TO SYNC STOCK DETAIL API
    }
    private void FrmPSalesInvoice_Resize(object sender, EventArgs e)
    {
        var smallScreen = Width <= 1040;
        if (smallScreen)
        {
            RGrid.Font = new Font("Bookman Old Style", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            foreach (DataGridViewColumn column in RGrid.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            }
            RGrid.Columns["GTxtSNo"]!.Width = 40;
            RGrid.Columns["GTxtSNo"].HeaderText = @"#";
            RGrid.Columns["GTxtHsCode"]!.Width = 70;
            RGrid.Columns["GTxtAltQty"]!.Width = 70;
            RGrid.Columns["GTxtPDiscount"]!.HeaderText = @"DIS";
            RGrid.Columns["GTxtShortName"]!.Visible = false;
            RGrid.Columns["GTxtDisplayNetAmount"]!.HeaderText = @"NET";
            RGrid.Columns["GTxtAltQty"].HeaderText = @"A_QTY";
            splitContainer.SplitterDistance = 600;
        }
        else
        {
            RGrid.Columns["GTxtSNo"]!.Width = 65;
            RGrid.Columns["GTxtSNo"].HeaderText = @"SNO";
            RGrid.Columns["GTxtAltQty"]!.Width = 70;
            RGrid.Columns["GTxtPDiscount"]!.HeaderText = @"DISCOUNT";
            RGrid.Columns["GTxtShortName"]!.Visible = true;
            RGrid.Columns["GTxtDisplayNetAmount"]!.HeaderText = @"NET AMOUNT";
            RGrid.Columns["GTxtAltQty"].HeaderText = @"ALT QTY";
            splitContainer.SplitterDistance = 600;
        }
    }
    private void FrmPSalesInvoice_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)21)
        {
            if (ObjGlobal.IsIrdRegister)
            {
                return;
            }

            _actionTag = "UPDATE";
            Text = @"POS INVOICE DETAILS [UPDATE]";
            ClearControl();
            EnableControl(true);
            TxtVno.Enabled = true;
            TxtVno.Focus();
        }
        else if (e.KeyChar == (char)27)
        {
            if (!BtnNew.Enabled)
            {
                if (PDetails.Visible)
                {
                    PDetails.Visible = false;
                    PDetails.Enabled = false;
                    TxtBarcode.Focus();
                }
                else
                {
                    _actionTag = "";
                    _invoiceType = "NORMAL";
                    EnableControl(false);
                    ClearControl();
                    BtnNew.Focus();
                }
            }
            else
            {
                MnuExit.PerformClick();
            }
        }
    }
    private void BtnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        EnableControl(true);
        ClearControl();
        ReturnSbVoucherNumber();
        Text = _actionTag.IsValueExits() ? $"POINT OF SALES [{_actionTag}]" : "POINT OF SALES";
        RGrid.ReadOnly = true;
        MskDate.Text = DateTime.Now.GetDateString();
        MskMiti.GetNepaliDate(MskDate.Text);
        if (MskMiti.Enabled)
        {
            MskMiti.Focus();
            return;
        }
        TxtBarcode.Focus();
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        if (RGrid.RowCount is 0)
        {
            if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
            {
                Close();
                return;
            }
        }
        else
        {
            ClearControl();
        }
    }
    private void BtnReverse_Click(object sender, EventArgs e)
    {
        _actionTag = "REVERSE";
        ClearControl();
        Text = @$"POS INVOICE DETAILS [{_actionTag}]";
        EnableControl(false);
        BtnVno.PerformClick();
        TxtVno.Focus();
    }

    private void BtnPrint_Click(object sender, EventArgs e)
    {
        //var module = _invoiceType == "RETURN" ? "SR" : "SB";
        var module = _invoiceType == "RETURN" ? "SB" : "POS";
        var frmName = string.Empty;
        var dtDesign = _master.GetPrintVoucherList("SB");
        if (dtDesign.Rows.Count > 0)
        {
            frmName = dtDesign.Rows[0]["DesignerPaper_Name"].ToString();
        }
        var frmDp = new FrmDocumentPrint(frmName.IsValueExits() ? frmName : "Crystal", module, TxtVno.Text, TxtVno.Text, true)
        {
            Owner = ActiveForm
        };
        frmDp.ShowDialog();
    }

    private void BtnReturn_Click(object sender, EventArgs e)
    {
        ClearControl();
        _actionTag = "SAVE";
        _invoiceType = "RETURN";
        Text = _actionTag.IsValueExits() ? $"POINT OF SALES RETURN INVOICE [{_actionTag}]" : "POINT OF SALES";
        LblInvoiceNo.Visible = true;
        TxtRefVno.Enabled = TxtRefVno.Visible = true;
        BtnRefVno.Enabled = BtnRefVno.Visible = true;
        RGrid.ReadOnly = true;
        MskDate.Text = DateTime.Now.GetDateString();
        MskMiti.Text = ObjGlobal.ReturnNepaliDate(MskDate.Text);
        ReturnSrVoucherNumber();
        EnableControl(true);
        TxtRefVno.Focus();
    }

    private void BtnLock_Click(object sender, EventArgs e)
    {
        new FrmLockScreen().ShowDialog();
    }

    private void MnuHoldInvoiceList_Click(object sender, EventArgs e)
    {
        var (accepted, model) = FrmTempInvoices.SelectTempInvoice();
        if (!accepted)
        {
            return;
        }
        FillTempInvoiceData(model);
        TxtBarcode.Focus();
    }

    private void TxtVno_Enter(object sender, EventArgs e)
    {
        GlobalControl_Enter(sender, e);
    }

    private void TxtVno_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor((TextBox)sender, 'L');
    }

    private void TxtVno_Validating(object sender, CancelEventArgs e)
    {
        if (_actionTag.IsValueExits() && TxtVno.IsBlankOrEmpty())
        {
            TxtVno.WarningMessage("VOUCHER NUMBER IS BLANK..!!");
            return;
        }
        if (_actionTag.Equals("UPDATE") && TxtVno.IsValueExits())
        {
            FillInvoiceForUpdate(TxtVno.Text);
        }
    }

    private void BtnVno_Click(object sender, EventArgs e)
    {
        var listType = _invoiceType.Equals("RETURN") && _actionTag.Equals("REVERSE") ? "SR" : "SB";
        var frmPickList = new FrmAutoPopList("MAX", listType, _actionTag, ObjGlobal.SearchText, "PRINT", "TRANSACTION");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                if (_invoiceType.Equals("RETURN") || _actionTag.Equals("REVERSE"))
                {
                    if (_invoiceType.Equals("RETURN") && _actionTag.Equals("SAVE"))
                    {
                        TxtRefVno.Text = frmPickList.SelectedList[0]["VoucherNo"].ToString().Trim();
                    }
                    else
                    {
                        TxtVno.Text = frmPickList.SelectedList[0]["VoucherNo"].ToString().Trim();
                    }

                    if (_invoiceType.Equals("RETURN") && _actionTag.Equals("REVERSE"))
                    {
                        FillReturnInvoiceData(TxtVno.Text);
                    }
                    else
                    {
                        FillInvoiceData(_invoiceType.Equals("RETURN") ? TxtRefVno.Text : TxtVno.Text);
                    }
                }
                else if (_actionTag == "UPDATE")
                {
                    TxtVno.Text = frmPickList.SelectedList[0]["VoucherNo"].ToString().Trim();
                    FillInvoiceData(TxtVno.Text);
                }
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"InvoiceNo Not Found..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtVno.Focus();
            return;
        }

        ObjGlobal.Caption = string.Empty;
        TxtVno.Focus();
    }
    private async Task GetAndSyncStockDetails()
    {
        try
        {
            _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!_configParams.Success || _configParams.Model.Item2 == null)
            {
                return;
            }
            var apiConfig = new SyncApiConfig
            {
                BaseUrl = _configParams.Model.Item2,
                Apikey = _configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,

                InsertUrl = @$"{_configParams.Model.Item2}Product/InsertStockDetailList",

            };

            DataSyncHelper.SetConfig(apiConfig);
            _injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(_injectData);
            var repo = DataSyncProviderFactory.GetRepository<StockDetail>(_injectData);

            var sqlCrQuery = "SELECT * FROM AMS.StockDetails";

            var queryResponse = await QueryUtils.GetListAsync<StockDetail>(sqlCrQuery);
            var curList = queryResponse.List.ToList();

            await repo.PushNewListAsync(curList);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
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
    private void MskMiti_Validating(object sender, CancelEventArgs e)
    {
        if (MskMiti.MaskCompleted && !MskMiti.IsDateExits("M") || !MskMiti.MaskCompleted && MskMiti.Enabled && TxtVno.IsValueExits())
        {
            MskMiti.WarningMessage($"ENTER MITI IS INVALID..!!");
            return;
        }
        if (MskMiti.MaskCompleted && !MskMiti.Text.IsValidDateRange("M"))
        {
            MskMiti.WarningMessage($"MITI MUST BE BETWEEN [{ObjGlobal.CfStartBsDate} AND {ObjGlobal.CfEndBsDate}]");
            return;
        }
        if (MskMiti.MaskCompleted)
        {
            MskDate.GetEnglishDate(MskMiti.Text);
        }
    }

    private void MskDate_Validating(object sender, CancelEventArgs e)
    {
        if (MskDate.MaskCompleted && !MskDate.IsDateExits("D") || !MskDate.MaskCompleted && MskDate.Enabled && TxtVno.IsValueExits())
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

    private void TxtCounter_Leave(object sender, EventArgs e)
    {
        GlobalControl_Leave(sender, e);
        if (ActiveControl != null && _actionTag.IsValueExits() && TxtCounter.Focused && TxtCounter.Text.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtCounter, "TERMINAL IS REQUIRED FOR BILLING..!!");
            TxtCounter.Focus();
        }
    }

    private void BtnCounter_Click(object sender, EventArgs e)
    {
        var frmCounter = new FrmCounterTagList();
        frmCounter.ShowDialog();
        TxtCounter.Text = frmCounter.SelectedCounter;
        CounterId = frmCounter.SelectedCounterId;
        _defaultPrinter = frmCounter.CounterPrinter;
    }

    private void TxtRefVno_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1) BtnVno_Click(sender, e);
    }

    private void TxtRefVno_Leave(object sender, EventArgs e)
    {
        if (_tagStrings.Equals(_invoiceType))
        {
            if (TxtRefVno.Text.IsBlankOrEmpty() && TxtRefVno.Focused && ActiveControl != null && _actionTag.IsValueExits())
            {
                if (CustomMessageBox.Question(@"PLEASE SELECT SALES INVOICE NUMBER FOR RETURN OR CLICK ON YES TO CONTINUE..!!") is DialogResult.No)
                {
                    TxtRefVno.Focus();
                }
            }
            else if (RGrid.RowCount is 0 && _invoiceType.Equals("RETURN") && TxtVno.Text.IsValueExits())
            {
                FillInvoiceData(TxtRefVno.Text);
            }
        }
    }

    private void TxtRefVno_KeyPress(object sender, KeyPressEventArgs e)
    {
        GlobalControl_KeyPress(sender, e);
    }

    private void TxtBarcode_Enter(object sender, EventArgs e)
    {
        GlobalControl_Enter(sender, e);
        if (TxtBarcode.Text.IsBlankOrEmpty())
        {
            RGrid.ClearSelection();
        }
        PDetails.Visible = false;
        PDetails.Enabled = false;
        TxtBarcode.SelectAll();
    }

    private void TxtBarcode_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Back && TxtBarcode.Text.IsValueExits() && ProductId > 0)
        {
            ClearDetails();
        }
        else if (e.KeyChar is (char)Keys.Escape && TxtBarcode.Text.IsValueExits() && ProductId > 0)
        {
            if (MessageBox.Show(@"DO YOU WANT TO CLEAR THE DETAILS..??", ObjGlobal.Caption, MessageBoxButtons.YesNo) is DialogResult.Yes)
            {
                ClearDetails();
                TxtBarcode.Focus();
            }
        }
    }

    private void TxtBarcode_Validating(object sender, CancelEventArgs e)
    {
        if (TxtBarcode.IsBlankOrEmpty() && RGrid.RowCount > 0 || ActiveControl.Name == "TxtRefVno" && TxtRefVno.Enabled)
        {
            return;
        }

        var dtProduct = _master.GetProductInfoWithBarcode(TxtBarcode.Text);
        dtProduct = dtProduct.Rows.Count is 0 ? _master.GetProductWithBarcode(TxtBarcode.Text) : dtProduct;
        // var dtProduct = _master.GetProductWithBarcode(TxtBarcode.Text);
        if (dtProduct.Rows.Count == 1)
        {
            ProductId = dtProduct.Rows[0]["SelectedId"].GetLong();
            SetProductInfo();
        }
        else if (dtProduct.Rows.Count > 1)
        {
            var frmPickList = new FrmAutoPopList(dtProduct);
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                SalesRate = frmPickList.SelectedList[0]["PSalesRate"].GetDecimal();
                ProductId = frmPickList.SelectedList[0]["SelectedId"].GetLong();
            }

            SetProductInfo();
        }
        else if (dtProduct.Rows.Count == 0 && (_actionTag.IsValueExits() && TxtBarcode.Enabled))
        {
            MessageBox.Show(@"BARCODE NOT EXITS..!!", ObjGlobal.Caption);
            TxtBarcode.Focus();
            return;
        }

        if (TxtBarcode.Text.IsValueExits() && ProductId is 0 && _actionTag.IsValueExits() && RGrid.RowCount is 0)
        {
            this.NotifyValidationError(TxtBarcode, "PLEASE ENTER THE BARCODE OF THE ITEMS..!!");
            return;
        }
    }

    private void TxtBarcode_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.N)
        {
            (ListOfProduct.Text, ProductId) = GetMasterList.CreateProduct(true);
            SetProductInfo();
        }
        else if (e.KeyCode == Keys.F1)
        {
            BtnProduct_Click(sender, e);
        }
        else if (e.KeyCode == Keys.F2)
        {
            var (description, id) = GetMasterList.GetCounterProduct(TxtBarcode.Text);
            if (description.IsValueExits())
            {
                ListOfProduct.Text = description;
                ProductId = id;
            }
            SetProductInfo();
        }
        else if (e.KeyCode == Keys.Enter)
        {
            if (TxtBarcode.Text.Trim() == string.Empty && TxtBarcode.Enabled)
            {
                if (RGrid.Rows.Count == 0)
                {
                    MessageBox.Show(@"BARCODE CAN'T BE LEFT BLANK..!!", ObjGlobal.Caption);
                    TxtBarcode.Focus();
                }
                else
                {
                    TxtNetAmount.Text = TxtNetAmount.GetDecimal() is 0 ? TxtBasicAmount.Text : TxtNetAmount.Text;
                    PDetails.Visible = PDetails.Enabled = true;
                    PDetails.Focus();
                    if (_tagStrings.Contains(_invoiceType))
                    {
                        TxtRemarks.Enabled = true;
                        TxtRemarks.Focus();
                    }
                    else
                    {
                        if (TxtMember.Enabled)
                        {
                            TxtMember.Focus();
                        }
                        else if (TxtBillDiscountPercentage.Enabled)
                        {
                            TxtBillDiscountPercentage.Focus();
                        }
                        else if (TxtBillDiscountAmount.Enabled)
                        {
                            TxtBillDiscountAmount.Focus();
                        }
                        else if (TxtTenderAmount.Enabled)
                        {
                            TxtTenderAmount.Focus();
                        }
                        else
                        {
                            BtnSave.Focus();
                        }
                    }
                }
            }
            else if (TxtBarcode.Text.Trim() != string.Empty)
            {
                TxtQty.Enabled = true;
                TxtQty.Focus();
            }
        }
        else if (TxtBarcode.ReadOnly)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtBarcode, OpenProductList);
        }
    }

    private void BtnProduct_Click(object sender, EventArgs e)
    {
        OpenProductList();
    }

    private void TxtQty_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtQty, 'E');
        TxtQty.SelectAll();
    }

    private void TxtQty_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtQty, 'L');
    }

    private void TxtQty_TextChanged(object sender, EventArgs e)
    {
    }

    private void TxtQty_KeyDown(object sender, KeyEventArgs e)
    {
        // if user tried to change the qty, rate or discount
        if (e.KeyCode != Keys.F2)
        {
            return;
        }
        var confirm = new FrmProductInfo(ProductId, GetAltQty, TxtQty.GetDecimal(), GetSalesRate, PDiscountPercentage, PDiscount);
        confirm.ShowDialog();
        if (confirm.DialogResult != DialogResult.OK)
        {
            return;
        }
        GetAltQty = confirm.ChangeAltQty.GetDecimal();
        TxtQty.Text = confirm.ChangeQty.GetDecimalQtyString();
        PDiscount = confirm.Discount.GetDecimal();
        PDiscountPercentage = confirm.DiscountPercent.GetDecimal();
        GetSalesRate = confirm.ChangeRate.GetDecimal();
    }

    private void TxtQty_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            AddDataToGridDetails(IsRowUpdate);
        }
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var _);
        }
    }

    private void TxtQty_Validating(object sender, CancelEventArgs e)
    {
    }

    private void RGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
        RIndex = e.RowIndex;
        SGridId = RIndex;
    }

    private void RGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
        CIndex = e.ColumnIndex;
    }

    private void RGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is not Keys.Enter || RGrid.CurrentRow == null)
        {
            return;
        }
        e.SuppressKeyPress = true;
        SGridId = RIndex;
        SetDataFromGridToTextBox();
        TxtBarcode.Focus();
    }

    private void RGrid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        SetDataFromGridToTextBox();
    }

    private void RGrid_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
        TotalCalculationOfInvoice();
        for (var i = 0; i < RGrid.RowCount; i++)
        {
            RGrid.Rows[i].Cells["GTxtSNo"].Value = i + 1;
        }
    }

    private void CmbPaymentType_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (TxtCustomer.Enabled)
            {
                TxtCustomer.Focus();
            }
            else
            {
                BtnSave.Focus();
            }
        }
    }

    private void CmbPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        var result = CmbPaymentType.Text.ToUpper();
        var userLedgerId = _master.GetUserLedgerIdFromUser(ObjGlobal.LogInUser);
        TxtCustomer.ReadOnly = result.Equals("CASH");
        LedgerId = result switch
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
            "GIFT VOUCHER" => ObjGlobal.FinanaceGiftVoucherLedgerId.GetLong(),
            _ => 0
        };
        TxtCustomer.Text = _master.GetLedgerDescription(LedgerId);
        TxtCustomer.Enabled = CmbPaymentType.SelectedValue is not ("PARTIAL" or "GIFT");
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
                LedgerId = id;
                LedgerCurrentBalance(LedgerId);
            }
            TxtCustomer.Focus();
        }
        else if (e.KeyCode is Keys.Enter or Keys.Tab)
        {
            if (TxtCustomer.Text.Trim() == string.Empty)
            {
                BtnCustomer_Click(sender, e);
            }
            else if (TxtCustomer.Text.Trim() != string.Empty)
            {
                BtnSave.Focus();
            }
            else
            {
                TxtCustomer.WarningMessage(@"CUSTOMER CANNOT LEFT BLANK..! PLEASE SELECT CUSTOMER..!!");
                TxtCustomer.Focus();
            }
        }
        else if (e.KeyCode == Keys.F2)
        {
            var cmdString = $"SELECT gl.GLType FROM AMS.GeneralLedger gl WHERE gl.GLName='{TxtCustomer.Text}'";
            var table = cmdString.GetQueryDataTable();
            if (table.Rows.Count <= 0) return;
            var ledgerType = table.Rows[0]["GlType"].GetUpper();
            if (ledgerType is "BANK" or "CASH")
            {
                CashAndBankValidation(table.Rows[0]["GlType"].ToString());
            }
        }
        else if (TxtCustomer.ReadOnly)
        {
            var searchKeys = e.KeyCode.ToString();
            ClsKeyPreview.KeyEvent(e, searchKeys, TxtCustomer, btnCustomer);
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
        LedgerId = id;
        LedgerCurrentBalance(LedgerId);
        TxtCustomer.Focus();
    }

    private void TxtCustomer_TextChanged(object sender, EventArgs e)
    {
        LedgerCurrentBalance(LedgerId);
    }

    private void TxtCustomer_Validating(object sender, CancelEventArgs e)
    {
        if (TxtCustomer.IsValueExits() && _actionTag.Equals("SAVE") && _customerResult != DialogResult.Yes)
        {
            var paymentType = _salesInvoice.GetInvoicePaymentMode(LedgerId).GetUpper();
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
            if (TxtBillDiscountPercentage.GetDecimal() > 100)
            {
                MessageBox.Show(@"DISCOUNT PERCENTAGE CAN'T MORE THEN 100%..!!", ObjGlobal.Caption);
                TxtBillDiscountPercentage.Clear();
                TxtBillDiscountPercentage.Focus();
            }
            else
            {
                TxtBillDiscountPercentage.Text = TxtBillDiscountPercentage.GetDecimalString();
            }
        }
        else
        {
            TxtBillDiscountPercentage.Text = ObjGlobal.ReturnDouble("0").ToString(ObjGlobal.SysAmountFormat);
        }
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
            TxtTenderAmount.Focus();
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
            if (TxtBillDiscountAmount.GetDecimal() > TxtBasicAmount.GetDecimal())
            {
                MessageBox.Show(@"DISCOUNT AMOUNT CAN'T BE GREATER THEN BASIC AMOUNT..!!", ObjGlobal.Caption);
                TxtBillDiscountAmount.Focus();
            }
            else
            {
                TxtBillDiscountAmount_TextChanged(sender, EventArgs.Empty);
                TxtNetAmount.Text = (TxtBasicAmount.GetDecimal() - TxtBillDiscountAmount.GetDecimal()).GetDecimalString();
            }
        }
        catch
        {
            // ~~ignored~~
        }
    }

    private void TxtTenderAmt_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtTenderAmount, 'E');
        TxtTenderAmount.SelectAll();
    }

    private void TxtTenderAmt_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (CmbPaymentType.SelectedIndex == 0)
            {
                if (TxtTenderAmount.GetDecimal() is 0 && !_tagStrings.Contains(_invoiceType))
                {
                    TxtTenderAmount.WarningMessage(@"TENDER AMOUNT CAN NOT LEFT BLANK OR ZERO AMOUNT");
                    TxtTenderAmount.SelectAll();
                    return;
                }
                if (TxtTenderAmount.GetDecimal() != 0 && TxtTenderAmount.GetDecimal() - TxtNetAmount.GetDecimal() < 0)
                {
                    TxtTenderAmount.WarningMessage(@"TENDER AMOUNT CAN NOT LEFT BLANK OR ZERO AMOUNT");
                    TxtTenderAmount.SelectAll();
                    return;
                }
            }
            BtnSave.Focus();
        }
    }

    private void TxtTenderAmt_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void TxtTenderAmt_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtTenderAmount, 'L');
    }

    private void PDetails_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            PDetails.Visible = false;
            TxtBarcode.Focus();
        }
    }

    private void TxtTenderAmt_Validating(object sender, CancelEventArgs e)
    {
        if (ActiveControl.Name == "TxtChangeAmount" && TxtChangeAmount.GetDecimal() is 0 && !_tagStrings.Contains(_invoiceType))
        {
            e.Cancel = true;
            MessageBox.Show(@"TENDER AMOUNT CAN'T BE BLANK..!!", ObjGlobal.Caption);
            TxtTenderAmount.Focus();
        }

        if (TxtTenderAmount.GetDecimal() > 0 && TxtTenderAmount.Text.GetDouble() < TxtNetAmount.Text.GetDouble() &&
            TxtTenderAmount.Focused && PDetails.Visible)
        {
            e.Cancel = true;
            MessageBox.Show(@"TENDER AMOUNT CAN'T BE LESS THAN BILL AMOUNT..!!", ObjGlobal.Caption);
            TxtTenderAmount.Focus();
        }

        TxtTenderAmount.Text = ObjGlobal.ReturnDouble(TxtTenderAmount.Text).ToString(ObjGlobal.SysAmountFormat);
    }

    private void BtnDayClosing_Click(object sender, EventArgs e)
    {
        new FrmCashClosing().ShowDialog();
    }

    private void BtnTodaySales_Click(object sender, EventArgs e)
    {
        _salesInvoice.SbMaster.Invoice_Date = DateTime.Now;
        var frmPickList = new FrmAutoPopList("MAX", "TODAYSALES", ObjGlobal.SearchText, _actionTag, "ALL", "TRANSACTION");
        if (FrmAutoPopList.GetListTable == null || FrmAutoPopList.GetListTable.Rows.Count <= 0) return;
        frmPickList.ShowDialog();
        if (frmPickList.SelectedList.Count > 0)
        {
        }

        frmPickList.Dispose();
    }

    private void BtnMember_Click(object sender, EventArgs e)
    {
        var frmPickList = new FrmAutoPopList("MAX", "MEMBERSHIP", _actionTag, ObjGlobal.SearchText, "ALL", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtMember.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                MemberShipId = ObjGlobal.ReturnInt(frmPickList.SelectedList[0]["LedgerId"].ToString().Trim());
                FillMemberValue(MemberShipId);
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"MEMBER NO NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtBarcode.Focus();
        }
    }

    private void TxtMember_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnMember_Click(sender, e);
        }
        else if (e.KeyData is Keys.Back or Keys.Delete)
        {
            MemberShipId = 0;
            TxtMember.Clear();
            LblMemberAmount.IsClear();
            LblMemberName.IsClear();
            LblMemberType.IsClear();
            LblMemberShortName.IsClear();
            LblTag.IsClear();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtBillDiscountPercentage.Enabled)
            {
                TxtBillDiscountPercentage.Focus();
            }
            else if (TxtBillDiscountAmount.Enabled)
            {
                TxtBillDiscountAmount.Focus();
            }
            else if (TxtTenderAmount.Enabled)
            {
                TxtTenderAmount.Focus();
            }
            else
            {
                BtnSave.Focus();
            }
        }
    }

    private void TxtMember_Validated(object sender, EventArgs e)
    {
        if (!TxtMember.Text.IsValueExits()) return;
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
        if (TxtTenderAmount.Focused)
        {
            TxtChangeAmount.Text = (TxtTenderAmount.GetDecimal() - TxtNetAmount.GetDecimal()).GetDecimalString();
        }
    }

    private void TxtBillDiscountPercentage_TextChanged(object sender, EventArgs e)
    {
        if (!TxtBillDiscountPercentage.Focused)
        {
            return;
        }
        TxtBillDiscountAmount.Text = (TxtBillDiscountPercentage.Text.GetDecimal() * TxtBasicAmount.Text.GetDecimal() / 100).GetDecimalString();
        TxtNetAmount.Text = (TxtBasicAmount.GetDecimal() - TxtBillDiscountAmount.GetDecimal()).GetDecimalString();
        LblNumberInWords.Text = LblNumberInWords.GetNumberInWords(TxtNetAmount.Text);
    }

    private void TxtBillDiscountAmount_TextChanged(object sender, EventArgs e)
    {
        if (RGrid.RowCount is 0)
        {
            return;
        }
        if (TxtBasicAmount.GetDecimal() <= TxtBillDiscountAmount.GetDecimal())
        {
            TxtBillDiscountAmount.WarningMessage("DISCOUNT AMOUNT CAN NOT BE GREATER THAN INVOICE AMOUNT");
            TxtBillDiscountAmount.Focus();
        }
        TxtNetAmount.Text = (TxtBasicAmount.GetDecimal() - TxtBillDiscountAmount.GetDecimal()).GetDecimalString();
        TxtChangeAmount.Text = TxtTenderAmount.GetDecimal() > 0 ? (TxtTenderAmount.GetDecimal() - TxtNetAmount.GetDecimal()).GetDecimalString() : 0.ToString(ObjGlobal.SysAmountFormat);
        var rAmount = TxtBillDiscountAmount.GetDecimal() > 0 ? TxtBillDiscountAmount.GetDecimal() / TxtBasicAmount.GetDecimal() : 0;
        for (var i = 0; i < RGrid.RowCount; i++)
        {
            var basicAmount = RGrid.Rows[i].Cells["GTxtDisplayNetAmount"].Value.GetDecimal();
            var dAmount = rAmount > 0 ? basicAmount * rAmount : 0;
            var isTaxable = RGrid.Rows[i].Cells["GTxtIsTaxable"].Value.GetBool();
            var netAmount = basicAmount - dAmount;
            var taxableAmount = isTaxable ? netAmount / (decimal)1.13 : 0;
            RGrid.Rows[i].Cells["GTxtValueBDiscount"].Value = dAmount;
            RGrid.Rows[i].Cells["GTxtValueVatAmount"].Value = isTaxable ? netAmount - taxableAmount : 0;
            RGrid.Rows[i].Cells["GTxtValueTaxableAmount"].Value = isTaxable ? taxableAmount : 0;
            RGrid.Rows[i].Cells["GTxtValueExemptedAmount"].Value = isTaxable ? 0 : taxableAmount;
        }
        TotalCalculationOfInvoice();
    }

    private void PDetails_Paint(object sender, PaintEventArgs e)
    {
        ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.Gray, ButtonBorderStyle.Outset);
    }

    private void CmbPaymentType_Validating(object sender, CancelEventArgs e)
    {
        if (PDetails.Visible)
        {
            if (CmbPaymentType.Text is "PARTIAL")
            {
                var result = new FrmSettlement(TxtBasicAmount.Text);
                result.ShowDialog();
                if (result.DialogResult == DialogResult.OK)
                {
                }
            }
        }
    }

    private void TxtAltQty_TextChanged(object sender, EventArgs e)
    {
    }

    private void GlobalControl_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void GlobalControl_Leave(object sender, EventArgs e)
    {
        if (sender is TextBox box)
        {
            ObjGlobal.TxtBackColor(box, 'L');
        }
    }

    private void GlobalControl_Enter(object sender, EventArgs e)
    {
        if (sender is TextBox box)
        {
            ObjGlobal.TxtBackColor(box, 'E');
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
                result = _actionTag switch
                {
                    "REVERSE" => ReverseSelectedInvoiceNumber(),
                    _ => _invoiceType.Equals("RETURN") ? SavePosReturnInvoice() : SavePosInvoice()
                };
                if (result > 0)
                {
                    BtnSave.Enabled = true;
                    if (_actionTag.Equals("SAVE"))
                    {
                        _entry.UpdateDocumentNumbering(_invoiceType.Equals("RETURN") ? "SR" : "SB", _invoiceType.Equals("RETURN") ? _returnNumberSchema : _docDesc);
                        if (ChkPrint.Checked && _actionTag == "SAVE")
                        {
                            var design = string.Empty;
                            var module = _invoiceType.Equals("RETURN") ? "SR" : "SB";
                            if (module is "SB")
                            {
                                module = TxtNetAmount.GetDecimal() < 10000 ? "ATI" : "SB";
                            }
                            var dtCheckPrint = _master.GetPrintVoucherList(module);
                            if (dtCheckPrint.Rows.Count > 0)
                            {
                                var frmDp = new FrmDocumentPrint("Crystal", module, TxtVno.Text, TxtVno.Text, true)
                                {
                                    Owner = ActiveForm
                                };
                                frmDp.ShowDialog();
                            }
                            else
                            {
                                design = module switch
                                {
                                    "SB" or "ATI" or "POS" => ChkTaxInvoice.Checked switch
                                    {
                                        true when _invoiceType.Equals("RETURN") => "DefaultReturnInvoiceWithVAT",
                                        true when !_invoiceType.Equals("RETURN") => "DefaultInvoiceWithVAT",
                                        _ => TxtNetAmount.GetDecimal() < 10000 ? ObjGlobal.SysDefaultAbtInvoiceDesign : ObjGlobal.SysDefaultInvoiceDesign
                                    },
                                    "SR" => ChkTaxInvoice.Checked switch
                                    {
                                        true => "DefaultReturnInvoiceWithVAT",
                                        _ => TxtNetAmount.GetDecimal() > 10000 ? "DefaultReturnInvoiceWithVAT" : "DefaultReturnInvoice"
                                    },
                                    _ => design
                                };

                                if (design.IsBlankOrEmpty())
                                {
                                    design = module.Equals("SR") ? "DefaultReturnInvoiceWithVAT" : "DefaultInvoiceWithVAT";
                                }
                                _printFunction.PrintDirectSalesInvoice(module, TxtNetAmount.GetDecimal(), TxtVno.Text, _defaultPrinter, TxtCounter.Text, design);
                            }
                        }
                    }

                    var resultDesc = _invoiceType.Equals("RETURN") ? "SALES RETURN INVOICE NUMBER" : "SALES INVOICE NUMBER";
                    CustomMessageBox.ActionSuccess(TxtVno.Text, resultDesc, _actionTag);
                    if (_actionTag == "REVERSE")
                    {
                        EnableControl(true);
                    }
                    _invoiceType = "NORMAL";
                    _actionTag = _actionTag.Equals("UPDATE") ? _actionTag : _actionTag.IsValueExits() ? "SAVE" : _actionTag;
                    LblDisplayReceivedAmount.Text = TxtTenderAmount.Text;
                    LblDisplayReturnAmount.Text = TxtChangeAmount.Text;
                    ClearControl();
                    PnlInvoiceDetails.Visible = true;
                    if (_actionTag.Equals("UPDATE"))
                    {
                        TxtVno.Focus();
                    }
                    else
                    {
                        if (MskMiti.Enabled)
                        {
                            MskMiti.Focus();
                        }
                        else
                        {
                            TxtBarcode.Focus();
                        }
                    }
                }
                else
                {
                    BtnSave.Enabled = true;
                    CustomMessageBox.Warning($@" ERROR OCCURS WHILE {TxtVno.Text} {_actionTag} ..!!");
                    TxtBarcode.Focus();
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
            ex.DialogResult();
            BtnSave.Enabled = true;
            CreateDatabaseTable.CreateTrigger();
        }
    }

    private void BtnHold_Click(object sender, EventArgs e)
    {
        try
        {
            BtnHold.Enabled = false;
            _invoiceType = "HOLD";
            if (IsValidInvoice())
            {
                if (SavePosInvoiceHold() != 0)
                {
                    BtnHold.Enabled = true;
                    this.NotifySuccess($"{TxtVno.Text} NUMBER HOLD SUCCESSFULLY..!!");
                    ClearControl();
                    TxtBarcode.Focus();
                }
                else
                {
                    BtnHold.Enabled = true;
                    MessageBox.Show(@"ERROR OCCURS WHILE SAVE DATA", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(@"ERROR OCCURS WHILE SAVE DATA", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                BtnHold.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            BtnHold.Enabled = true;
            ex.ToNonQueryErrorResult(ex.StackTrace);
            this.NotifyError(ex.Message);
        }
    }

    #endregion --------------- SALES INVOICE ---------------

    // METHOD FOR THIS FORM

    #region --------------- METHOD ---------------

    private void FillMemberValue(int memberId)
    {
        var dtMemberInfo = _master.ReturnMemberShipValue(memberId);
        if (dtMemberInfo.Rows.Count <= 0) return;
        MemberShipId = memberId;
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
        if (ProductId is 0) return;
        var dtProduct = _master.GetPosProductInfo(ProductId.ToString());
        if (dtProduct.Rows.Count <= 0)
        {
            return;
        }
        if (dtProduct.Rows.Count > 1)
        {
            var dtBarcode = _master.GetProductListBarcode(ProductId);
            if (dtBarcode.Rows.Count > 0)
            {
                var frmPickList = new FrmAutoPopList(dtBarcode);
                frmPickList.ShowDialog();
                SalesRate = frmPickList.SelectedList.Count switch
                {
                    > 0 => frmPickList.SelectedList[0]["PSalesRate"].GetDecimal(),
                    _ => SalesRate
                };
            }
        }
        else
        {
            SalesRate = 0;
        }
        StockQty = dtProduct.Rows[0]["StockQty"].GetDecimal();
        if (StockQty <= 0 && ObjGlobal.StockNegativeStockBlock)
        {
            TxtBarcode.WarningMessage("SELECTED PRODUCT IS NOT AVAILABLE DUE TO NEGATIVE STOCK QTY ..!!");
            return;
        }
        PnlProductDetails.Visible = true;
        PnlProductDetails.Enabled = true;

        PnlInvoiceDetails.Enabled = PnlInvoiceDetails.Visible = false;
        var response = Task.Run(() => QueryUtils.GetFirstOrDefaultAsync<string>(@"SELECT barcode_print_config FROM ams.InventorySetting"));
        if (response.Result.Success || !string.IsNullOrWhiteSpace(response.Result.Model))
        {
            var model = XmlUtils.XmlDeserialize<BarCodePrintConfigModel>(response.Result.Model);
            if (model != null)
            {
                if (model.PrintedBarCode.ToLower() == "barcode")
                {
                    TxtBarcode.Text = dtProduct.Rows[0]["Barcode"].GetString();
                }
                else if (model.PrintedBarCode.ToLower() == "barcode1")
                {
                    TxtBarcode.Text = dtProduct.Rows[0]["Barcode1"].GetString();
                }
                else if (model.PrintedBarCode.ToLower() == "barcode2")
                {
                    TxtBarcode.Text = dtProduct.Rows[0]["Barcode2"].GetString();
                }
                else if (model.PrintedBarCode.ToLower() == "barcode3")
                {
                    TxtBarcode.Text = dtProduct.Rows[0]["Barcode3"].GetString();
                }
                else
                {
                    TxtBarcode.Text = dtProduct.Rows[0]["PShortName"].GetString();
                }
            }
            else
            {
                TxtBarcode.Text = dtProduct.Rows[0]["PShortName"].GetString();
            }
        }
        else
        {
            TxtBarcode.Text = dtProduct.Rows[0]["PShortName"].GetString();
        }

        _productHsCode = dtProduct.Rows[0]["HsCode"].GetString();
        LblProduct.Text = dtProduct.Rows[0]["PName"].GetString();
        ListOfProduct.Text = dtProduct.Rows[0]["PName"].GetString();

        SalesRate = SalesRate is 0 ? dtProduct.Rows[0]["PSalesRate"].GetDecimal() : SalesRate;
        UnitId = dtProduct.Rows[0]["PUnit"].GetInt();
        AltUnitId = dtProduct.Rows[0]["PAltUnit"].GetInt();
        AltQtyConv = dtProduct.Rows[0]["PAltConv"].GetDecimal();
        QtyConv = dtProduct.Rows[0]["PQtyConv"].GetDecimal();
        TxtUnit.Text = dtProduct.Rows[0]["UOM"].GetString();
        GetAltUnit = dtProduct.Rows[0]["AltUOM"].GetString();

        TaxRate = dtProduct.Rows[0]["PTax"].GetDecimal();
        IsTaxable = TaxRate > 0;
        GetMrp = dtProduct.Rows[0]["PMRP"].GetDecimal();
        TxtQty.Text = TxtQty.GetDecimal() is 0 ? 1.00.GetDecimalQtyString() : TxtQty.Text.GetDecimalQtyString();
        LblStockQty.Text = StockQty.GetDecimalString();
        LblBarcode.Text = TxtBarcode.Text;
        LblProduct.Text = ListOfProduct.Text;
        LblUnit.Text = TxtUnit.Text;

        TxtAltQty.Enabled = AltUnitId > 0;

        LblSalesRate.Text = SalesRate.GetDecimalString();
    }

    private bool IsValidInvoice()
    {
        if (_actionTag.Equals("REVERSE") || _actionTag.Equals("RETURN"))
        {
            if (CustomMessageBox.VoucherNoAction(_actionTag, TxtVno.Text) == DialogResult.No)
            {
                TxtBarcode.Focus();
                return false;
            }

            if (TxtRemarks.IsBlankOrEmpty())
            {
                TxtRemarks.WarningMessage($"REMARKS IS REQUIRED FOR {_actionTag}..!!");
                return false;
            }
        }

        if (!_actionTag.Equals("REVERSE"))
        {
            if (TxtCounter.Text.IsBlankOrEmpty() || CounterId is 0)
            {
                TxtCounter.WarningMessage("TERMINAL IS REQUIRED FOR BILLING..!!");
                return false;
            }

            if (TxtBarcode.Text.IsBlankOrEmpty() && RGrid.RowCount is 0)
            {
                TxtBarcode.WarningMessage("INVOICE PRODUCT DETAILS IS MISSING CANNOT SAVE BLANK..!!");
                return false;
            }
            if (TxtBillDiscountPercentage.GetDecimal() >= 100)
            {
                TxtBillDiscountPercentage.Clear();
                this.NotifyValidationError(TxtBillDiscountPercentage, "DISCOUNT PERCENTAGE CAN'T MORE THEN 100%..!!");
                return false;
            }
            if (TxtBillDiscountPercentage.GetDecimal() >= 100)
            {
                TxtBillDiscountPercentage.WarningMessage("DISCOUNT PERCENT CAN NOT BE EQUAL OR GREATER THAN 100");
                return false;
            }
            if (TxtCustomer.IsBlankOrEmpty() || LedgerId is 0)
            {
                TxtCustomer.WarningMessage("INVOICE CUSTOMER DETAILS IS MISSING CANNOT SAVE BLANK..!!");
                return false;
            }
            if (TxtVno.IsBlankOrEmpty())
            {
                TxtVno.WarningMessage("VOUCHER NUMBER CAN NOT BE BLANK..!!");
                return false;
            }
            MskMiti_Validating(this, null);
            MskDate_Validating(this, null);
            TxtCustomer_Validating(this, new CancelEventArgs(false));
            if (TxtBillDiscountAmount.GetDecimal() > 0)
            {
                TxtBillDiscountAmount_TextChanged(this, EventArgs.Empty);
                if (TxtBillDiscountAmount.GetDecimal() >= TxtBasicAmount.GetDecimal())
                {
                    TxtBillDiscountAmount.WarningMessage("DISCOUNT AMOUNT CAN NOT BE EQUAL OR GREATER THAN INVOICE AMOUNT");
                    return false;
                }
            }

            if (TxtTenderAmount.GetDecimal() is 0 && _invoiceType.Equals("NORMAL"))
            {
                TxtTenderAmount.WarningMessage("TENDER AMOUNT CAN'T BE ZERO..!!");
                return false;
            }

            if (TxtTenderAmount.GetDecimal() < TxtNetAmount.GetDecimal() && _invoiceType.Equals("NORMAL"))
            {
                TxtTenderAmount.WarningMessage("TENDER AMOUNT CAN'T BE LESS THEN INVOICE AMOUNT..!!");
                return false;
            }

            if (TxtMember.IsValueExits() && MemberShipId is 0)
            {
                TxtMember.WarningMessage("SELECTED MEMBER IS INVALID..!!");
                return false;
            }
        }

        return true;
    }

    private void TotalCalculationOfInvoice()
    {
        var viewRows = RGrid.Rows.OfType<DataGridViewRow>();
        var rows = viewRows as DataGridViewRow[] ?? viewRows.ToArray();
        LblItemsTotalQty.Text = rows.Sum(row => row.Cells["GTxtQty"].Value.GetDecimal()).GetDecimalString();
        LblItemsTotal.Text = rows.Sum(row => row.Cells["GTxtDisplayAmount"].Value.GetDecimal()).GetDecimalString();
        LblItemsDiscountSum.Text = rows.Sum(row => row.Cells["GTxtPDiscount"].Value.GetDecimal()).GetDecimalString();
        LblItemsNetAmount.Text = rows.Sum(row => row.Cells["GTxtDisplayNetAmount"].Value.GetDecimal()).GetDecimalString();
        lblTaxable.Text = rows.Sum(row => row.Cells["GTxtValueTaxableAmount"].Value.GetDecimal()).GetDecimalString();
        lblNonTaxable.Text = rows.Sum(row => row.Cells["GTxtValueExemptedAmount"].Value.GetDecimal()).GetDecimalString();
        lblTax.Text = rows.Sum(row => row.Cells["GTxtValueVatAmount"].Value.GetDecimal()).GetDecimalString();
        TxtBasicAmount.Text = LblItemsNetAmount.Text;
        TxtNetAmount.Text = (TxtBasicAmount.GetDecimal() - TxtBillDiscountAmount.GetDecimal()).GetDecimalString();
        TxtChangeAmount.Text = TxtTenderAmount.GetDecimal() > 0 ? (TxtTenderAmount.GetDecimal() - TxtNetAmount.GetDecimal()).GetDecimalString() : 0.GetDecimalString();
        LblNumberInWords.Text = ClsMoneyConversion.MoneyConversion(TxtNetAmount.Text.GetDecimalString());
    }

    private void EnableControl(bool ctrl)
    {
        BtnNew.Enabled = !ctrl;
        TxtVno.Enabled = _tagStrings.Contains(_actionTag);
        BtnVno.Enabled = _tagStrings.Contains(_actionTag) || ctrl;
        MskDate.Enabled = MskMiti.Enabled = false;
        TxtRefVno.Visible = BtnRefVno.Visible = _invoiceType.Equals("RETURN");
        TxtRefVno.Enabled = BtnRefVno.Enabled = _invoiceType.Equals("RETURN");
        TxtBarcode.Enabled = ctrl;
        TxtCounter.Enabled = ctrl;
        BtnCounter.Enabled = ctrl;
        TxtTenderAmount.Enabled = ctrl;

        TxtBasicAmount.Enabled = false;
        TxtNetAmount.Enabled = false;
        TxtChangeAmount.Enabled = false;

        CmbPaymentType.Enabled = ctrl;
        TxtCustomer.Enabled = ctrl;
        btnCustomer.Enabled = ctrl;
        ListOfProduct.Enabled = ctrl;
        TxtQty.Enabled = ctrl;
        TxtUnit.Enabled = false;
        TxtAltQty.Enabled = false;
        TxtAltUnit.Enabled = false;
        TxtBillDiscountAmount.Enabled = ctrl;
        TxtBillDiscountPercentage.Enabled = ctrl;
        RGrid.Enabled = ctrl && !_tagStrings.Contains(_actionTag);
        TxtRemarks.Enabled = _tagStrings.Contains(_actionTag) || ctrl;
        BtnSave.Enabled = _tagStrings.Contains(_actionTag) || ctrl;
        BtnHold.Enabled = ctrl && !_tagStrings.Contains(_actionTag);
        BtnHold.Visible = BtnHold.Enabled;
        RGrid.ReadOnly = true;
    }

    private void ClearControl()
    {
        if (_actionTag.Equals("UPDATE") && ObjGlobal.IsIrdRegister)
        {
            Text = _actionTag.IsValueExits() ? $"POINT OF SALES [{_actionTag}]" : "POINT OF SALES";
        }
        TxtVno.Clear();
        TxtVno.Text = _actionTag.Equals("SAVE") ? TxtVno.GetCurrentVoucherNo("POS", _docDesc) : TxtVno.Text;
        if (RGrid.Rows.Count > 0)
        {
            RGrid.Rows.Clear();
        }

        MskDate.Enabled = !ObjGlobal.IsIrdRegister;
        MskMiti.Enabled = MskDate.Enabled;
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
        MemberShipId = 0;
        CmbPaymentType.SelectedIndex = 0;
        ChkTaxInvoice.Checked = false;
        TxtCustomer.Clear();
        LedgerId = 0;
        LedgerId = _master.GetUserLedgerIdFromUser(ObjGlobal.LogInUser);
        LedgerId = LedgerId is 0 ? ObjGlobal.FinanceCashLedgerId.GetLong() : LedgerId;
        TxtCustomer.Text = _master.GetLedgerDescription(LedgerId);
        TxtBillDiscountAmount.Clear();
        TxtBillDiscountPercentage.Clear();
        TxtBasicAmount.Clear();
        TxtNetAmount.Clear();
        TxtTenderAmount.Clear();
        TxtChangeAmount.Clear();
        TxtRemarks.Clear();
        _dtPartyInfo.Clear();
        MemberShipId = 0;
        LblNumberInWords.Text = string.Empty;
        LblMemberAmount.IsClear();
        LblMemberName.IsClear();
        LblMemberType.IsClear();
        LblMemberShortName.IsClear();
        LblTag.IsClear();
        PDetails.Visible = false;
        BtnHold.Visible = !_tagStrings.Contains(_actionTag);
    }

    private void ClearDetails()
    {
        SGridId = -1;
        IsRowUpdate = false;
        ProductId = 0;
        UnitId = 0;
        GetUnitId = 0;
        GetAltUnit = string.Empty;
        GetAltQty = 0;
        GetSalesRate = 0;
        GetMrp = 0;
        GetQty = 0;
        PDiscount = 0;
        PDiscountPercentage = 0;
        SalesRate = 0;
        TxtBarcode.Clear();
        TxtQty.Text = 1.GetDecimalQtyString();
        TxtUnit.Clear();
        StockQty = 0;
        PnlProductDetails.Visible = false;
        PnlProductDetails.Enabled = PnlProductDetails.Visible;
        LblProduct.Text = string.Empty;
        LblBarcode.Text = string.Empty;
        LblStockQty.Text = string.Empty;
        LblUnit.Text = string.Empty;
        LblSalesRate.Text = string.Empty;
        PnlInvoiceDetails.Visible = false;
        ListOfProduct.Text = string.Empty;
        _productHsCode = string.Empty;
    }

    private void SetDataFromGridToTextBox()
    {
        if (RGrid.CurrentRow != null)
        {
            IsRowUpdate = true;
            ProductId = RGrid.Rows[SGridId].Cells["GTxtProductId"].Value.GetLong();
            TxtBarcode.Text = RGrid.Rows[SGridId].Cells["GTxtShortName"].Value.GetString();
            SetProductInfo();
            GetAltQty = RGrid.Rows[SGridId].Cells["GTxtAltQty"].Value.GetDecimal();
            TxtQty.Text = RGrid.Rows[SGridId].Cells["GTxtQty"].Value.GetDecimalQtyString();
            GetSalesRate = RGrid.Rows[SGridId].Cells["GTxtDisplayRate"].Value.GetDecimal();
            PDiscountPercentage = RGrid.Rows[SGridId].Cells["GTxtDiscountRate"].Value.GetDecimal();
            PDiscount = RGrid.Rows[SGridId].Cells["GTxtPDiscount"].Value.GetDecimal();
        }

        IsRowUpdate = true;
        TxtBarcode.Focus();
    }

    private void AddDataToGridDetails(bool isUpdate)
    {
        LblDisplayReceivedAmount.Text = string.Empty;
        LblDisplayReturnAmount.Text = string.Empty;
        if (ProductId is 0 || TxtBarcode.Text.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtBarcode, "INVALID PRODUCT OR PLEASE CHECK THE BARCODE..!!");
            return;
        }

        if (TxtQty.Text.GetDecimal() is 0)
        {
            this.NotifyValidationError(TxtBarcode, "QUANTITY CANNOT BE ZERO..!!");
            return;
        }

        if ((StockQty <= 0 || StockQty < TxtQty.GetDecimal()) && ObjGlobal.StockNegativeStockBlock)
        {
            TxtBarcode.WarningMessage("SELECT PRODUCT IS INVALID..!!");
            return;
        }
        else if ((StockQty <= 0 || StockQty < TxtQty.GetDecimal()) && ObjGlobal.StockNegativeStockWarning)
        {
            var result = CustomMessageBox.Question("STOCK IS GOING NEGATIVE. DO YOU WANT TO CONTINUE..!!");
            if (result is DialogResult.Yes)
            {
            }
            else return;
        }

        if (SalesRate is 0)
        {
            if (MessageBox.Show(@"SALES RATE IS ZERO. DO YOU WANT TO CONTINUE..!!", ObjGlobal.Caption, MessageBoxButtons.YesNo) is DialogResult.No)
            {
                TxtBarcode.Focus();
                return;
            }
        }
        var newRowQty = false;
        if (!isUpdate)
        {
            var row = RGrid.Rows.Cast<DataGridViewRow>().FirstOrDefault(r => string.Equals(r.Cells["GTxtProductId"].Value.GetString(), $"{ProductId}"));
            if (row != null)
            {
                if (CustomMessageBox.Question($@"{ListOfProduct.Text} PRODUCT IS ALREADY ADD ON THIS INVOICE DO YOU WANT TO UPDATE..??") is DialogResult.Yes)
                {
                    newRowQty = true;
                    SGridId = row.Index;
                }
            }
        }

        if (newRowQty)
        {
            isUpdate = true;
        }
        else if (!isUpdate)
        {
            RGrid.Rows.Add();
            SGridId = RGrid.RowCount - 1;
        }

        RGrid.Rows[SGridId].Cells["GTxtSNo"].Value = !isUpdate ? RGrid.RowCount : RGrid.Rows[SGridId].Cells["GTxtSNo"].Value;
        RGrid.Rows[SGridId].Cells["GTxtProductId"].Value = ProductId;
        RGrid.Rows[SGridId].Cells["GTxtShortName"].Value = TxtBarcode.Text;
        RGrid.Rows[SGridId].Cells["GTxtHsCode"].Value = _productHsCode;
        RGrid.Rows[SGridId].Cells["GTxtProduct"].Value = ListOfProduct.Text;
        RGrid.Rows[SGridId].Cells["GTxtGodownId"].Value = 0;
        RGrid.Rows[SGridId].Cells["GTxtGodown"].Value = string.Empty;
        RGrid.Rows[SGridId].Cells["GTxtAltQty"].Value = GetAltQty > 0 ? GetAltQty : string.Empty;
        RGrid.Rows[SGridId].Cells["GTxtAltUOMId"].Value = AltUnitId;
        RGrid.Rows[SGridId].Cells["GTxtAltUOM"].Value = GetAltUnit;

        var qty = TxtQty.Text.GetDecimal();
        qty = newRowQty ? RGrid.Rows[SGridId].Cells["GTxtQty"].Value.GetDecimal() + qty : qty;

        RGrid.Rows[SGridId].Cells["GTxtQty"].Value = qty.GetDecimalString();
        RGrid.Rows[SGridId].Cells["GTxtUOMId"].Value = UnitId;
        RGrid.Rows[SGridId].Cells["GTxtMRP"].Value = GetMrp;
        RGrid.Rows[SGridId].Cells["GTxtUOM"].Value = TxtUnit.Text;

        SalesRate = GetSalesRate > 0 ? GetSalesRate : SalesRate;
        RGrid.Rows[SGridId].Cells["GTxtDisplayRate"].Value = SalesRate.GetDecimalString();
        RGrid.Rows[SGridId].Cells["GTxtDisplayAmount"].Value = (qty * SalesRate).GetDecimalString();
        RGrid.Rows[SGridId].Cells["GTxtDiscountRate"].Value = PDiscountPercentage.GetDecimal();
        RGrid.Rows[SGridId].Cells["GTxtPDiscount"].Value = PDiscount.GetDecimalString();

        RGrid.Rows[SGridId].Cells["GTxtValueBDiscount"].Value = RGrid.Rows[SGridId].Cells["GTxtValueBDiscount"].Value.GetDecimal();
        RGrid.Rows[SGridId].Cells["GTxtDisplayNetAmount"].Value = (qty * SalesRate - PDiscount).GetDecimalString();

        var taxRate = ("1." + $"{TaxRate.GetDouble()}");
        var taxableSalesRate = IsTaxable && isTaxBilling ? SalesRate / taxRate.GetDecimal() : SalesRate;

        RGrid.Rows[SGridId].Cells["GTxtValueRate"].Value = taxableSalesRate;
        RGrid.Rows[SGridId].Cells["GTxtValueNetAmount"].Value = taxableSalesRate * qty;

        RGrid.Rows[SGridId].Cells["GTxtIsTaxable"].Value = IsTaxable;
        RGrid.Rows[SGridId].Cells["GTxtTaxPriceRate"].Value = TaxRate;

        var taxableAmount = IsTaxable && isTaxBilling
            ? RGrid.Rows[SGridId].Cells["GTxtDisplayNetAmount"].Value.GetDecimal() / taxRate.GetDecimal()
            : RGrid.Rows[SGridId].Cells["GTxtValueNetAmount"].Value.GetDecimal();

        var vatAmount = IsTaxable && isTaxBilling
            ? RGrid.Rows[SGridId].Cells["GTxtDisplayNetAmount"].Value.GetDecimal() - taxableAmount : 0;

        RGrid.Rows[SGridId].Cells["GTxtValueVatAmount"].Value = vatAmount;
        RGrid.Rows[SGridId].Cells["GTxtValueTaxableAmount"].Value = IsTaxable && isTaxBilling ? taxableAmount : 0;
        RGrid.Rows[SGridId].Cells["GTxtValueExemptedAmount"].Value = IsTaxable && isTaxBilling ? 0 : taxableAmount;

        RGrid.Rows[SGridId].Cells["GTxtNarration"].Value = string.Empty;
        RGrid.Rows[SGridId].Cells["GTxtFreeQty"].Value = 0;
        RGrid.Rows[SGridId].Cells["GTxtFreeUnitId"].Value = 0;
        RGrid.CurrentCell = RGrid.Rows[RGrid.RowCount - 1].Cells[0];
        TotalCalculationOfInvoice();
        ClearDetails();

        TxtBarcode.Focus();
    }

    private int ReverseSelectedInvoiceNumber()
    {
        if (_invoiceType.Equals("RETURN"))
        {
            _returnInvoice.SrMaster.SR_Invoice = TxtVno.Text;
        }
        else
        {
            _salesInvoice.SbMaster.SB_Invoice = TxtVno.Text;
        }

        return _invoiceType.Equals("RETURN") ? _returnInvoice.SaveSalesReturn(_actionTag) : _salesInvoice.SaveSalesInvoice(_actionTag);
    }

    private int SavePosInvoice()
    {
        TxtVno.Text = _actionTag.Equals("SAVE") ? TxtVno.GetCurrentVoucherNo("POS", _docDesc) : TxtVno.Text;
        _salesInvoice.SbMaster.SB_Invoice = TxtVno.Text;
        _salesInvoice.SbMaster.Invoice_Date = MskDate.Text.GetDateTime();
        _salesInvoice.SbMaster.Invoice_Miti = MskMiti.Text;
        _salesInvoice.SbMaster.Invoice_Time = DateTime.Now;
        _salesInvoice.SbMaster.PB_Vno = _tempSalesInvoice;
        _salesInvoice.SbMaster.Vno_Date = _tempSalesInvoice.IsValueExits() ? _tempInvoiceDate.GetDateTime() : DateTime.Now;
        _salesInvoice.SbMaster.Vno_Miti = _tempInvoiceMiti;
        _salesInvoice.SbMaster.Customer_Id = LedgerId;

        _salesInvoice.SbMaster.Party_Name = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["PartyName"].ToString() : string.Empty;
        _salesInvoice.SbMaster.Vat_No = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["VatNo"].ToString() : string.Empty;
        _salesInvoice.SbMaster.Contact_Person = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ContactPerson"].ToString() : string.Empty;
        _salesInvoice.SbMaster.Mobile_No = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Mob"].ToString() : string.Empty;
        _salesInvoice.SbMaster.Address = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Address"].ToString() : string.Empty;
        _salesInvoice.SbMaster.ChqNo = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ChequeNo"].ToString() : string.Empty;
        _salesInvoice.SbMaster.ChqDate = _dtPartyInfo.Rows.Count > 0 && !string.IsNullOrEmpty(_dtPartyInfo.Rows[0]["ChequeNo"].ToString()) ? DateTime.Parse(_dtPartyInfo.Rows[0]["ChequeDate"].ToString()) : DateTime.Now;
        _salesInvoice.SbMaster.Invoice_Type = "LOCAL";
        _salesInvoice.SbMaster.Invoice_Mode = "POS";
        _salesInvoice.SbMaster.Payment_Mode = CmbPaymentType.Text;
        _salesInvoice.SbMaster.DueDays = 0;
        _salesInvoice.SbMaster.DueDate = DateTime.Now;
        _salesInvoice.SbMaster.Agent_Id = AgentId;
        _salesInvoice.SbMaster.Subledger_Id = SubLedgerId;
        _salesInvoice.SbMaster.SO_Invoice = string.Empty;
        _salesInvoice.SbMaster.SO_Date = DateTime.Now;
        _salesInvoice.SbMaster.SC_Invoice = string.Empty;
        _salesInvoice.SbMaster.SC_Date = DateTime.Now;
        _salesInvoice.SbMaster.Cls1 = DepartmentId;
        _salesInvoice.SbMaster.Cls2 = 0;
        _salesInvoice.SbMaster.Cls3 = 0;
        _salesInvoice.SbMaster.Cls4 = 0;
        _salesInvoice.SbMaster.Cur_Id = _currencyId > 0 ? _currencyId : ObjGlobal.SysCurrencyId;
        _salesInvoice.SbMaster.Cur_Rate = 1;
        _salesInvoice.SbMaster.CounterId = CounterId;
        _salesInvoice.SbMaster.MShipId = MemberShipId;
        _salesInvoice.SbMaster.B_Amount = TxtBasicAmount.GetDecimal();
        _salesInvoice.SbMaster.TermRate = TxtBillDiscountPercentage.GetDecimal();
        _salesInvoice.SbMaster.T_Amount = TxtBillDiscountAmount.GetDecimal();
        _salesInvoice.SbMaster.N_Amount = TxtNetAmount.GetDecimal();
        _salesInvoice.SbMaster.LN_Amount = TxtNetAmount.GetDecimal();
        _salesInvoice.SbMaster.Tender_Amount = TxtTenderAmount.GetDecimal();
        _salesInvoice.SbMaster.Return_Amount = TxtChangeAmount.GetDecimal();
        _salesInvoice.SbMaster.V_Amount = lblTax.Text.GetDecimal();
        _salesInvoice.SbMaster.Tbl_Amount = lblTaxable.Text.GetDecimal();
        _salesInvoice.SbMaster.Action_Type = _actionTag;
        _salesInvoice.SbMaster.R_Invoice = false;
        _salesInvoice.SbMaster.No_Print = 0;
        _salesInvoice.SbMaster.In_Words = LblNumberInWords.Text;
        _salesInvoice.SbMaster.Remarks = TxtRemarks.Text;
        _salesInvoice.SbMaster.Audit_Lock = false;

        _salesInvoice.SbMaster.CBranch_Id = ObjGlobal.SysBranchId;
        _salesInvoice.SbMaster.FiscalYearId = ObjGlobal.SysFiscalYearId;

        _salesInvoice.SbMaster.SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
        _salesInvoice.SbMaster.SyncGlobalId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
        if (ObjGlobal.LocalOriginId != null)
        {
            _salesInvoice.SbMaster.SyncOriginId = ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.IsValueExits()
                ? ObjGlobal.LocalOriginId.Value
                : Guid.Empty;
        }

        _salesInvoice.SbMaster.SyncCreatedOn = DateTime.Now;
        _salesInvoice.SbMaster.SyncLastPatchedOn = DateTime.Now;

        var sync = _salesInvoice.ReturnSyncRowVersionVoucher("SB", TxtVno.Text);
        _salesInvoice.SbMaster.SyncRowVersion = sync;

        int index = 0;
        // SALES INVOICE DETAILS
        _salesInvoice.DetailsList.Clear();
        _salesInvoice.Terms.Clear();

        if (RGrid.Rows.Count > 0)
        {
            foreach (DataGridViewRow row in RGrid.Rows)
            {
                var list = new SB_Details();
                if (row.Cells["GTxtProductId"].Value.GetLong() is 0)
                {
                    continue;
                }

                list.SB_Invoice = TxtVno.Text;
                list.Invoice_SNo = row.Cells["GTxtSno"].Value.GetInt();

                list.P_Id = row.Cells["GTxtProductId"].Value.GetLong();
                list.Gdn_Id = row.Cells["GTxtGodownId"].Value.GetInt();
                list.Alt_Qty = row.Cells["GTxtAltQty"].Value.GetDecimal();
                list.Alt_UnitId = row.Cells["GTxtAltUOMId"].Value.GetInt();

                list.Qty = row.Cells["GTxtQty"].Value.GetDecimal();
                list.Unit_Id = row.Cells["GTxtUOMId"].Value.GetInt();
                list.Rate = row.Cells["GTxtDisplayRate"].Value.GetDecimal();

                var netAmount = row.Cells["GTxtDisplayNetAmount"].Value.GetDecimal();
                var discountAmount = row.Cells["GTxtPDiscount"].Value.GetDecimal();
                var vatAmount = row.Cells["GTxtValueVatAmount"].Value.GetDecimal();
                var basicAmount = netAmount + discountAmount - vatAmount;

                list.B_Amount = basicAmount;
                list.T_Amount = vatAmount - discountAmount;


                list.N_Amount = row.Cells["GTxtDisplayNetAmount"].Value.GetDecimal();

                list.AltStock_Qty = row.Cells["GTxtAltQty"].Value.GetDecimal();
                list.Stock_Qty = row.Cells["GTxtQty"].Value.GetDecimal();

                list.Narration = row.Cells["GTxtNarration"].Value.GetString();

                list.SO_Invoice = string.Empty;
                list.SO_Sno = 0;

                list.SC_Invoice = string.Empty;
                list.SC_SNo = 0;

                var pVatRate = row.Cells["GTxtTaxPriceRate"].Value.GetDecimal();
                list.V_Rate = row.Cells["GTxtTaxPriceRate"].Value.GetDecimal();
                list.V_Amount = vatAmount;
                list.Tax_Amount = vatAmount > 0 ? vatAmount / (pVatRate / 100) : 0;

                list.Free_Unit_Id = 0;
                list.Free_Qty = 0;

                list.StockFree_Qty = 0;
                list.ExtraFree_Unit_Id = 0;
                list.ExtraFree_Qty = 0;
                list.ExtraStockFree_Qty = 0;

                list.T_Product = row.Cells["GTxtIsTaxable"].Value.GetBool();

                list.S_Ledger = 0;
                list.SR_Ledger = 0;

                list.SZ1 = list.SZ2 = list.SZ3 = list.SZ4 = list.SZ5 = list.SZ6 = list.SZ7 = list.SZ8 = list.SZ9 = list.SZ10 = null;
                list.Serial_No = null;
                list.Batch_No = null;
                list.Exp_Date = null;
                list.Manu_Date = null;
                list.MaterialPost = null;

                var discount = row.Cells["GTxtPDiscount"].Value.GetDecimal();
                if (discount > 0)
                {
                    var term = new SB_Term()
                    {
                        SB_VNo = TxtVno.Text,
                        ST_Id = ObjGlobal.SalesDiscountTermId,
                        SNo = row.Cells["GTxtSno"].Value.GetInt(),
                        Term_Type = "P",
                        Product_Id = row.Cells["GTxtProductId"].Value.GetLong(),
                        Rate = row.Cells["GTxtDiscountRate"].Value.GetDecimal(),
                        Amount = row.Cells["GTxtPDiscount"].Value.GetDecimal(),
                        Taxable = "N",
                        SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty,
                        SyncGlobalId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty,
                        SyncOriginId = ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.IsValueExits() ? ObjGlobal.LocalOriginId.GetValueOrDefault() : Guid.Empty,
                        SyncCreatedOn = DateTime.Now,
                        SyncLastPatchedOn = DateTime.Now,
                        SyncRowVersion = sync
                    };
                    _salesInvoice.Terms.Add(term);
                }

                var vatRate = row.Cells["GTxtValueVatAmount"].Value.GetDecimal();
                if (vatRate > 0)
                {
                    var term = new SB_Term()
                    {
                        SB_VNo = TxtVno.Text,
                        ST_Id = ObjGlobal.SalesVatTermId,
                        SNo = row.Cells["GTxtSno"].Value.GetInt(),
                        Term_Type = "P",
                        Product_Id = row.Cells["GTxtProductId"].Value.GetLong(),
                        Rate = row.Cells["GTxtTaxPriceRate"].Value.GetDecimal(),
                        Amount = row.Cells["GTxtValueVatAmount"].Value.GetDecimal(),
                        Taxable = "Y",
                        SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty,
                        SyncGlobalId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty,
                        SyncOriginId = ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.IsValueExits() ? ObjGlobal.LocalOriginId.GetValueOrDefault() : Guid.Empty,
                        SyncCreatedOn = DateTime.Now,
                        SyncLastPatchedOn = DateTime.Now,
                        SyncRowVersion = sync
                    };
                    _salesInvoice.Terms.Add(term);
                }
                list.PDiscountRate = row.Cells["GTxtDiscountRate"].Value.GetDecimal();
                list.PDiscount = row.Cells["GTxtPDiscount"].Value.GetDecimal();

                list.BDiscountRate = row.Cells["GTxtValueBRate"].Value.GetDecimal();
                list.BDiscount = row.Cells["GTxtValueBDiscount"].Value.GetDecimal();

                list.ServiceChargeRate = 0;
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

                _salesInvoice.DetailsList.Add(list);
            }
        }


        //SALES TERM
        var discountTerm = new SB_Term()
        {
            SB_VNo = TxtVno.Text,
            ST_Id = ObjGlobal.SalesSpecialDiscountTermId,
            SNo = 1,
            Term_Type = "B",
            Product_Id = null,
            Rate = TxtBillDiscountPercentage.GetDecimal(),
            Amount = TxtBillDiscountAmount.GetDecimal(),
            Taxable = "N",
            SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty,
            SyncGlobalId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty,
            SyncOriginId = ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.IsValueExits() ? ObjGlobal.LocalOriginId.GetValueOrDefault() : Guid.Empty,
            SyncCreatedOn = DateTime.Now,
            SyncLastPatchedOn = DateTime.Now,
            SyncRowVersion = sync
        };
        _salesInvoice.Terms.Add(discountTerm);

        return _salesInvoice.SaveSalesInvoice(_actionTag);
    }

    private int SavePosReturnInvoice()
    {
        TxtVno.Text = _actionTag.Equals("SAVE") ? TxtVno.GetCurrentVoucherNo("SR", _returnNumberSchema) : TxtVno.Text;
        _returnInvoice.SrMaster.SR_Invoice = TxtVno.Text;
        _returnInvoice.SrMaster.Invoice_Date = MskDate.Text.GetDateTime();
        _returnInvoice.SrMaster.Invoice_Miti = MskMiti.Text;
        _returnInvoice.SrMaster.Invoice_Time = DateTime.Now;
        _returnInvoice.SrMaster.SB_Invoice = TxtRefVno.Text;
        _returnInvoice.SrMaster.SB_Date = TxtRefVno.Text.IsValueExits() ? _refInvoiceDate.GetDateTime() : DateTime.Now;
        _returnInvoice.SrMaster.SB_Miti = _refInvoiceMiti;
        _returnInvoice.SrMaster.Customer_ID = LedgerId;

        _returnInvoice.SrMaster.Party_Name = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["PartyName"].ToString() : string.Empty;
        _returnInvoice.SrMaster.Vat_No = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["VatNo"].ToString() : string.Empty;
        _returnInvoice.SrMaster.Contact_Person = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ContactPerson"].ToString() : string.Empty;
        _returnInvoice.SrMaster.Mobile_No = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Mob"].ToString() : string.Empty;
        _returnInvoice.SrMaster.Address = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Address"].ToString() : string.Empty;
        _returnInvoice.SrMaster.ChqNo = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ChequeNo"].ToString() : string.Empty;
        _returnInvoice.SrMaster.ChqDate = _dtPartyInfo.Rows.Count > 0 && _dtPartyInfo.Rows[0]["ChequeNo"].IsValueExits() ? DateTime.Parse(_dtPartyInfo.Rows[0]["ChequeDate"].ToString()) : DateTime.Now;

        _returnInvoice.SrMaster.Invoice_Type = "LOCAL";
        _returnInvoice.SrMaster.Invoice_Mode = "POS";
        _returnInvoice.SrMaster.Payment_Mode = CmbPaymentType.Text;
        _returnInvoice.SrMaster.DueDays = 0;
        _returnInvoice.SrMaster.DueDate = DateTime.Now;
        _returnInvoice.SrMaster.Agent_Id = AgentId;
        _returnInvoice.SrMaster.Subledger_Id = SubLedgerId;
        _returnInvoice.SrMaster.Cls1 = DepartmentId;
        _returnInvoice.SrMaster.Cls2 = 0;
        _returnInvoice.SrMaster.Cls3 = 0;
        _returnInvoice.SrMaster.Cls4 = 0;
        _returnInvoice.SrMaster.Cur_Id = _currencyId > 0 ? _currencyId : ObjGlobal.SysCurrencyId;
        _returnInvoice.SrMaster.Cur_Rate = 1;
        _returnInvoice.SrMaster.CounterId = CounterId;
        _returnInvoice.SrMaster.B_Amount = TxtBasicAmount.GetDecimal();
        _returnInvoice.SrMaster.TermRate = TxtBillDiscountPercentage.GetDecimal();
        _returnInvoice.SrMaster.T_Amount = TxtBillDiscountAmount.GetDecimal();

        _returnInvoice.SrMaster.N_Amount = TxtNetAmount.GetDecimal();
        _returnInvoice.SrMaster.LN_Amount = TxtNetAmount.GetDecimal();
        _returnInvoice.SrMaster.Tender_Amount = TxtTenderAmount.GetDecimal();
        _returnInvoice.SrMaster.Return_Amount = TxtChangeAmount.GetDecimal();
        _returnInvoice.SrMaster.V_Amount = lblTax.Text.GetDecimal();
        _returnInvoice.SrMaster.Tbl_Amount = lblTaxable.Text.GetDecimal();
        _returnInvoice.SrMaster.Action_Type = _actionTag;
        _returnInvoice.SrMaster.R_Invoice = false;
        _returnInvoice.SrMaster.No_Print = 0;
        _returnInvoice.SrMaster.In_Words = LblNumberInWords.Text;
        _returnInvoice.SrMaster.Remarks = TxtRemarks.Text;
        _returnInvoice.SrMaster.Audit_Lock = false;

        _returnInvoice.SrMaster.CBranch_Id = ObjGlobal.SysBranchId;
        _returnInvoice.SrMaster.FiscalYearId = ObjGlobal.SysFiscalYearId;
        _returnInvoice.SrMaster.SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
        _returnInvoice.SrMaster.SyncGlobalId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
        if (ObjGlobal.LocalOriginId != null)
        {
            _returnInvoice.SrMaster.SyncOriginId = ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.IsValueExits()
                ? ObjGlobal.LocalOriginId.Value
                : Guid.Empty;
        }

        _returnInvoice.SrMaster.SyncCreatedOn = DateTime.Now;
        _returnInvoice.SrMaster.SyncLastPatchedOn = DateTime.Now;

        var sync = _returnInvoice.ReturnSyncRowVersionVoucher("SB", TxtVno.Text);
        _returnInvoice.SrMaster.SyncRowVersion = sync;

        // SALES INVOICE DETAILS
        _returnInvoice.DetailsList.Clear();
        _returnInvoice.Terms.Clear();

        if (RGrid.Rows.Count > 0)
        {
            foreach (DataGridViewRow row in RGrid.Rows)
            {
                var list = new SR_Details();
                if (row.Cells["GTxtProductId"].Value.GetLong() is 0)
                {
                    continue;
                }

                list.SR_Invoice = TxtVno.Text;

                list.Invoice_SNo = row.Cells["GTxtSno"].Value.GetInt();
                list.P_Id = row.Cells["GTxtProductId"].Value.GetLong();
                list.Gdn_Id = row.Cells["GTxtGodownId"].Value.GetInt();
                list.Alt_Qty = row.Cells["GTxtAltQty"].Value.GetDecimal();
                list.Alt_UnitId = row.Cells["GTxtAltUOMId"].Value.GetInt();

                list.Qty = row.Cells["GTxtQty"].Value.GetDecimal();
                list.Unit_Id = row.Cells["GTxtUOMId"].Value.GetInt();
                list.Rate = row.Cells["GTxtDisplayRate"].Value.GetDecimal();

                var netAmount = row.Cells["GTxtDisplayNetAmount"].Value.GetDecimal();
                var discountAmount = row.Cells["GTxtPDiscount"].Value.GetDecimal();
                var vatAmount = row.Cells["GTxtValueVatAmount"].Value.GetDecimal();
                var basicAmount = netAmount + discountAmount - vatAmount;

                list.B_Amount = basicAmount;
                list.T_Amount = vatAmount - discountAmount;

                list.SB_Invoice = row.Cells["GTxtInvoiceNo"].Value.GetString();
                list.SB_Sno = row.Cells["GTxtInvoiceSNo"].Value.GetInt();


                list.V_Rate = 0;
                list.V_Amount = 0;
                list.Tax_Amount = 0;

                list.Free_Unit_Id = 0;
                list.Free_Qty = 0;

                list.StockFree_Qty = 0;
                list.ExtraFree_Unit_Id = 0;
                list.ExtraFree_Qty = 0;
                list.ExtraStockFree_Qty = 0;

                list.T_Product = row.Cells["GTxtIsTaxable"].Value.GetBool();
                list.S_Ledger = 0;
                list.SR_Ledger = 0;

                list.SZ1 = list.SZ2 = list.SZ3 = list.SZ4 = list.SZ5 = list.SZ6 = list.SZ7 = list.SZ8 = list.SZ9 = list.SZ10 = null;
                list.Serial_No = null;
                list.Batch_No = null;
                list.Exp_Date = null;
                list.Manu_Date = null;

                var discount = row.Cells["GTxtPDiscount"].Value.GetDecimal();
                if (discount > 0)
                {
                    var term = new SR_Term()
                    {
                        SR_VNo = TxtVno.Text,
                        ST_Id = ObjGlobal.SalesDiscountTermId,
                        SNo = row.Cells["GTxtSno"].Value.GetInt(),
                        Term_Type = "P",
                        Product_Id = row.Cells["GTxtProductId"].Value.GetLong(),
                        Rate = row.Cells["GTxtDiscountRate"].Value.GetDecimal(),
                        Amount = row.Cells["GTxtPDiscount"].Value.GetDecimal(),
                        Taxable = "N",
                        SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty,
                        SyncGlobalId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty,
                        SyncOriginId = ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.IsValueExits() ? ObjGlobal.LocalOriginId.GetValueOrDefault() : Guid.Empty,
                        SyncCreatedOn = DateTime.Now,
                        SyncLastPatchedOn = DateTime.Now,
                        SyncRowVersion = sync
                    };
                    _returnInvoice.Terms.Add(term);
                }

                var vatRate = row.Cells["GTxtValueVatAmount"].Value.GetDecimal();
                if (vatRate > 0)
                {
                    var term = new SR_Term()
                    {
                        SR_VNo = TxtVno.Text,
                        ST_Id = ObjGlobal.SalesVatTermId,
                        SNo = row.Cells["GTxtSno"].Value.GetInt(),
                        Term_Type = "P",
                        Product_Id = row.Cells["GTxtProductId"].Value.GetLong(),
                        Rate = row.Cells["GTxtTaxPriceRate"].Value.GetDecimal(),
                        Amount = row.Cells["GTxtValueVatAmount"].Value.GetDecimal(),
                        Taxable = "Y",
                        SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty,
                        SyncGlobalId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty,
                        SyncOriginId = ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.IsValueExits() ? ObjGlobal.LocalOriginId.GetValueOrDefault() : Guid.Empty,
                        SyncCreatedOn = DateTime.Now,
                        SyncLastPatchedOn = DateTime.Now,
                        SyncRowVersion = sync
                    };
                    _returnInvoice.Terms.Add(term);
                }
                list.PDiscountRate = row.Cells["GTxtDiscountRate"].Value.GetDecimal();
                list.PDiscount = row.Cells["GTxtPDiscount"].Value.GetDecimal();

                list.BDiscountRate = row.Cells["GTxtValueBRate"].Value.GetDecimal();
                list.BDiscount = row.Cells["GTxtValueBDiscount"].Value.GetDecimal();

                list.ServiceChargeRate = 0;
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

                _returnInvoice.DetailsList.Add(list);
            }
        }


        //SALES TERM
        var discountTerm = new SR_Term()
        {
            SR_VNo = TxtVno.Text,
            ST_Id = ObjGlobal.SalesSpecialDiscountTermId,
            SNo = 1,
            Term_Type = "B",
            Product_Id = null,
            Rate = TxtBillDiscountPercentage.GetDecimal(),
            Amount = TxtBillDiscountAmount.GetDecimal(),
            Taxable = "N",
            SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty,
            SyncGlobalId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty,
            SyncOriginId = ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.IsValueExits() ? ObjGlobal.LocalOriginId.GetValueOrDefault() : Guid.Empty,
            SyncCreatedOn = DateTime.Now,
            SyncLastPatchedOn = DateTime.Now,
            SyncRowVersion = sync
        };
        _returnInvoice.Terms.Add(discountTerm);

        return _returnInvoice.SaveSalesReturn(_actionTag);
    }

    private int SavePosInvoiceHold()
    {
        ReturnTsbVoucherNumber();
        _entry.TsbMaster.SB_Invoice = TxtVno.Text;
        _entry.TsbMaster.Invoice_Date = MskDate.Text.GetDateTime();
        _entry.TsbMaster.Invoice_Miti = MskMiti.Text;
        _entry.TsbMaster.Invoice_Time = DateTime.Now;
        _entry.TsbMaster.PB_Vno = string.Empty;
        _entry.TsbMaster.Vno_Date = DateTime.Now;
        _entry.TsbMaster.Vno_Miti = string.Empty;
        _entry.TsbMaster.Customer_Id = LedgerId;


        _entry.TsbMaster.Party_Name = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["PartyName"].ToString() : string.Empty;
        _entry.TsbMaster.Vat_No = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["VatNo"].ToString() : string.Empty;
        _entry.TsbMaster.Contact_Person = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ContactPerson"].ToString() : string.Empty;
        _entry.TsbMaster.Mobile_No = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Mob"].ToString() : string.Empty;
        _entry.TsbMaster.Address = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Address"].ToString() : string.Empty;
        _entry.TsbMaster.ChqNo = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ChequeNo"].ToString() : string.Empty;
        _entry.TsbMaster.ChqDate = _dtPartyInfo.Rows.Count > 0 && !string.IsNullOrEmpty(_dtPartyInfo.Rows[0]["ChequeNo"].ToString()) ? DateTime.Parse(_dtPartyInfo.Rows[0]["ChequeDate"].ToString()) : DateTime.Now;

        _entry.TsbMaster.Invoice_Type = "LOCAL";
        _entry.TsbMaster.Invoice_Mode = "NORMAL";
        _entry.TsbMaster.Payment_Mode = CmbPaymentType.Text;
        _entry.TsbMaster.DueDays = 0;
        _entry.TsbMaster.DueDate = DateTime.Now;
        _entry.TsbMaster.Agent_Id = AgentId;
        _entry.TsbMaster.Subledger_Id = SubLedgerId;
        _entry.TsbMaster.SO_Invoice = string.Empty;
        _entry.TsbMaster.SO_Date = DateTime.Now;
        _entry.TsbMaster.SC_Invoice = string.Empty;
        _entry.TsbMaster.SC_Date = DateTime.Now;
        _entry.TsbMaster.Cls1 = DepartmentId;
        _entry.TsbMaster.Cls2 = 0;
        _entry.TsbMaster.Cls3 = 0;
        _entry.TsbMaster.Cls4 = 0;
        _entry.TsbMaster.Cur_Id = _currencyId > 0 ? _currencyId : ObjGlobal.SysCurrencyId;
        _entry.TsbMaster.Cur_Rate = 1;
        _entry.TsbMaster.CounterId = CounterId;
        _entry.TsbMaster.B_Amount = TxtBasicAmount.GetDecimal();
        _entry.TsbMaster.TermRate = TxtBillDiscountPercentage.GetDecimal();
        _entry.TsbMaster.T_Amount = TxtBillDiscountAmount.GetDecimal();
        _entry.TsbMaster.N_Amount = TxtNetAmount.GetDecimal();
        _entry.TsbMaster.LN_Amount = TxtNetAmount.GetDecimal();
        _entry.TsbMaster.Tender_Amount = TxtTenderAmount.GetDecimal();
        _entry.TsbMaster.Return_Amount = TxtChangeAmount.GetDecimal();
        _entry.TsbMaster.V_Amount = ((object)lblTax.Text).GetDecimal();
        _entry.TsbMaster.Tbl_Amount = ((object)lblTaxable.Text).GetDecimal();
        _entry.TsbMaster.Action_Type = _actionTag;
        _entry.TsbMaster.R_Invoice = false;
        _entry.TsbMaster.No_Print = 0;
        _entry.TsbMaster.In_Words = LblNumberInWords.Text;
        _entry.TsbMaster.Remarks = TxtRemarks.Text;
        _entry.TsbMaster.Audit_Lock = false;
        _entry.TsbMaster.GetView = RGrid;

        return _entry.SaveTempSalesInvoice(_actionTag);
    }

    private void FillInvoiceForUpdate(string voucherNo)
    {
        var dsSales = _entry.ReturnSalesInvoiceDetailsInDataSet(voucherNo);
        if (dsSales.Tables.Count <= 0) return;
        var dtMaster = dsSales.Tables[0];
        var dtDetails = dsSales.Tables[1];
        var dtPTerm = dsSales.Tables[2];
        var dtBTerm = dsSales.Tables[3];
        if (dtMaster.Rows.Count < 0) return;
        foreach (DataRow dr in dtMaster.Rows)
        {
            TxtVno.Text = dr["SB_Invoice"].ToString();
            MskDate.Text = dr["Invoice_Date"].GetDateString();
            MskMiti.Text = dr["Invoice_Miti"].ToString();
            TxtCustomer.Text = dr["GLName"].ToString();
            LedgerId = dr["Customer_ID"].GetLong();

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
            var discount = dr["T_Amount"].GetDecimal() > 0
                ? dr["T_Amount"].GetDecimal() / dr["B_Amount"].GetDecimal() * 100
                : 0;
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
                RGrid.Rows[iRow].Cells["GTxtAltUOM"].Value = GetAltUnit;
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
                RGrid.Rows[iRow].Cells["GTxtDisplayNetAmount"].Value = (qty * salesRate - pDiscount).GetDecimalString();
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

            RGrid.CurrentCell = RGrid.Rows[RGrid.RowCount - 1].Cells[CIndex];
            ObjGlobal.DGridColorCombo(RGrid);
            TotalCalculationOfInvoice();
            RGrid.ClearSelection();
        }

        if (dtBTerm.Rows.Count > 0)
        {
            var rAmount = TxtBillDiscountAmount.GetDecimal() > 0
                ? TxtBillDiscountAmount.GetDecimal() / TxtBasicAmount.GetDecimal()
                : 0;
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
    }

    private void FillInvoiceData(string voucherNo)
    {
        var dsSales = _entry.ReturnSalesInvoiceDetailsInDataSet(voucherNo);
        if (dsSales.Tables.Count <= 0) return;
        var dtMaster = dsSales.Tables[0];
        if (dtMaster.Rows.Count < 0) return;
        foreach (DataRow dr in dtMaster.Rows)
        {
            _selectedInvoice = _invoiceType.Equals("NORMAL") ? TxtVno.Text : TxtRefVno.Text;
            MskDate.Text = _invoiceType.Equals("RETURN") ? DateTime.Now.GetDateString() : dr["Invoice_Date"].GetDateString();
            MskMiti.Text = _invoiceType.Equals("RETURN") ? MskMiti.GetNepaliDate(MskDate.Text) : dr["Invoice_Miti"].ToString();

            if (dr["PB_Vno"].ToString() != string.Empty && _invoiceType.Equals("NORMAL"))
            {
                TxtRefVno.Text = Convert.ToString(dr["PB_Vno"].ToString());
                _refInvoiceMiti = dr["Vno_Miti"].GetDateString();
                _refInvoiceDate = ObjGlobal.ReturnEnglishDate(_refInvoiceMiti);
            }
            else if (_invoiceType.Equals("RETURN"))
            {
                TxtRefVno.Text = dr["SB_Invoice"].ToString();
                _refInvoiceMiti = dr["Invoice_Miti"].ToString();
                _refInvoiceDate = dr["Invoice_Date"].ToString();
            }

            LedgerId = _master.GetUserLedgerIdFromUser(ObjGlobal.LogInUser);
            LedgerId = LedgerId is 0 ? dr["Customer_ID"].GetLong() : LedgerId;
            TxtCustomer.Text = _master.GetLedgerDescription(LedgerId);

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
            if (dsSales.Tables[1].Rows.Count <= 0) return;
            {
                var iRow = 0;
                RGrid.Rows.Clear();
                RGrid.Rows.Add(dsSales.Tables[1].Rows.Count);
                foreach (DataRow row in dsSales.Tables[1].Rows)
                {
                    RGrid.Rows[iRow].Cells["GTxtSNo"].Value = row["Invoice_SNo"].ToString();
                    RGrid.Rows[iRow].Cells["GTxtProductId"].Value = row["P_Id"].ToString();
                    RGrid.Rows[iRow].Cells["GTxtShortName"].Value = row["PShortName"].ToString();
                    RGrid.Rows[iRow].Cells["GTxtProduct"].Value = row["PName"].ToString();
                    RGrid.Rows[iRow].Cells["GTxtGodownId"].Value = 0;
                    RGrid.Rows[iRow].Cells["GTxtGodown"].Value = string.Empty;
                    RGrid.Rows[iRow].Cells["GTxtAltQty"].Value = row["Alt_Qty"].GetDecimalString();
                    RGrid.Rows[iRow].Cells["GTxtAltUOMId"].Value = row["Alt_UnitId"].ToString();
                    RGrid.Rows[iRow].Cells["GTxtAltUOM"].Value = GetAltUnit;
                    RGrid.Rows[iRow].Cells["GTxtQty"].Value = row["AltUnitCode"].ToString();
                    var qty = row["Qty"].GetDecimal();
                    RGrid.Rows[iRow].Cells["GTxtQty"].Value = qty.GetDecimalString();
                    RGrid.Rows[iRow].Cells["GTxtUOMId"].Value = row["Unit_Id"].ToString();
                    RGrid.Rows[iRow].Cells["GTxtMRP"].Value = 0;
                    RGrid.Rows[iRow].Cells["GTxtUOM"].Value = row["UnitCode"].ToString();
                    var salesRate = row["Rate"].GetDecimal();
                    var pDiscount = row["PDiscount"].GetDecimal();

                    RGrid.Rows[iRow].Cells["GTxtDisplayRate"].Value = salesRate.GetDecimalString();
                    RGrid.Rows[iRow].Cells["GTxtDisplayAmount"].Value =
                        (salesRate * qty).ToString(ObjGlobal.SysAmountFormat);
                    RGrid.Rows[iRow].Cells["GTxtDiscountRate"].Value = row["PDiscountRate"].GetDecimalString();
                    RGrid.Rows[iRow].Cells["GTxtPDiscount"].Value = pDiscount.GetDecimalString();
                    RGrid.Rows[iRow].Cells["GTxtValueBDiscount"].Value = row["BDiscount"].GetDecimalString();
                    var taxRate = row["PTax"].GetDecimal();
                    var isTaxable = taxRate > 0;
                    RGrid.Rows[iRow].Cells["GTxtDisplayNetAmount"].Value =
                        (qty * salesRate - pDiscount).GetDecimalString();
                    var taxableSalesRate = isTaxable ? salesRate / (decimal)1.13 : salesRate;
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
                    RGrid.Rows[iRow].Cells["GTxtInvoiceSNo"].Value = row["Invoice_SNo"].ToString();
                    iRow++;
                }

                RGrid.CurrentCell = RGrid.Rows[RGrid.RowCount - 1].Cells[CIndex];
                ObjGlobal.DGridColorCombo(RGrid);
                TotalCalculationOfInvoice();
                TxtRemarks.Text = Convert.ToString(dr["Remarks"].ToString());
                TxtTenderAmount.Text = dr["Tender_Amount"].GetDecimalString();
                TxtChangeAmount.Text = dr["Return_Amount"].GetDecimalString();
                TxtBillDiscountAmount.Text = dr["T_Amount"].GetDecimalString();
                var discount = dr["T_Amount"].GetDecimal() > 0 ? dr["T_Amount"].GetDecimal() / dr["B_Amount"].GetDecimal() * 100 : 0;
                TxtBillDiscountPercentage.Text = discount.GetDecimalString();
                RGrid.ClearSelection();
            }
        }
    }

    private void FillReturnInvoiceData(string voucherNo)
    {
        var dsSales = _entry.ReturnSalesReturnDetailsInDataSet(voucherNo);
        if (dsSales.Tables.Count <= 0) return;
        foreach (DataRow dr in dsSales.Tables[0].Rows)
        {
            _selectedInvoice = TxtVno.Text;
            MskDate.Text = DateTime.Now.GetDateString();
            MskMiti.Text = MskDate.GetNepaliDate(MskDate.Text);

            if (dr["SB_Invoice"].ToString() != string.Empty)
            {
                TxtRefVno.Text = Convert.ToString(dr["SB_Invoice"].ToString());
                _refInvoiceMiti = dr["SB_Date"].GetDateString();
                _refInvoiceDate = dr["SB_Miti"].ToString();
            }

            TxtCustomer.Text = dr["GLName"].ToString();
            LedgerId = dr["Customer_ID"].GetLong();

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
                RGrid.Rows[iRow].Cells["GTxtAltUOM"].Value = GetAltUnit;
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

            RGrid.CurrentCell = RGrid.Rows[RGrid.RowCount - 1].Cells[CIndex];
            ObjGlobal.DGridColorCombo(RGrid);
            TotalCalculationOfInvoice();
            RGrid.ClearSelection();
        }
    }

    private void FillTempInvoiceData(string voucherNo)
    {
        var dsTempSales = _salesInvoice.ReturnTempSalesInvoiceDetailsInDataSet(voucherNo);
        if (dsTempSales.Tables.Count <= 0) return;
        foreach (DataRow dr in dsTempSales.Tables[0].Rows)
        {
            _tempSalesInvoice = dr["SB_Invoice"].ToString();
            _tempInvoiceMiti = dr["Invoice_Miti"].ToString();
            _tempInvoiceDate = dr["Invoice_Date"].GetDateString();
            if (dr["PB_Vno"].ToString() != string.Empty)
            {
                TxtRefVno.Text = _tempSalesInvoice;
                _refInvoiceMiti = _tempInvoiceMiti;
                _refInvoiceDate = _tempInvoiceDate;
            }

            TxtCustomer.Text = Convert.ToString(dr["GLName"].ToString());
            LedgerId = dr["Customer_ID"].GetLong();

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

        if (dsTempSales.Tables[1].Rows.Count <= 0) return;
        {
            var iRow = 0;
            RGrid.Rows.Clear();
            RGrid.Rows.Add(dsTempSales.Tables[1].Rows.Count);
            foreach (DataRow dr in dsTempSales.Tables[1].Rows)
            {
                RGrid.Rows[iRow].Cells["GTxtSNo"].Value = dr["Invoice_SNo"].ToString();
                RGrid.Rows[iRow].Cells["GTxtProductId"].Value = dr["P_Id"].ToString();
                RGrid.Rows[iRow].Cells["GTxtShortName"].Value = dr["PShortName"].ToString();
                RGrid.Rows[iRow].Cells["GTxtProduct"].Value = dr["PName"].ToString();
                RGrid.Rows[iRow].Cells["GTxtGodownId"].Value = 0;
                RGrid.Rows[iRow].Cells["GTxtGodown"].Value = string.Empty;
                RGrid.Rows[iRow].Cells["GTxtAltQty"].Value = dr["Alt_Qty"].GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtAltUOMId"].Value = dr["Alt_UnitId"].ToString();
                RGrid.Rows[iRow].Cells["GTxtAltUOM"].Value = GetAltUnit;
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

            RGrid.CurrentCell = RGrid.Rows[RGrid.RowCount - 1].Cells[CIndex];
            ObjGlobal.DGridColorCombo(RGrid);
            TotalCalculationOfInvoice();
            RGrid.ClearSelection();
        }
    }

    private void CashAndBankValidation(string ledgerType)
    {
        var partyInfo = new FrmPartyInfo(ledgerType, _dtPartyInfo, "PB");
        partyInfo.ShowDialog();
        if (_dtPartyInfo.Rows.Count > 0) _dtPartyInfo.Rows.Clear();
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

    private void InitialiseDataTable()
    {
        _dtPTerm.Reset();
        _dtPTerm = _master.GetBillingTerm();

        _dtPartyInfo.Reset();
        _dtPartyInfo = _master.GetPartyInfo();
    }

    private void LedgerCurrentBalance(long selectLedgerId)
    {
        if (selectLedgerId is 0) return;
        var date = MskDate.MaskCompleted ? MskDate.Text.GetSystemDate() : DateTime.Now.GetSystemDate();
        var dtCustomer = ClsMasterSetup.LedgerInformation(selectLedgerId, date);
        if (dtCustomer is { Rows: { Count: > 0 } })
        {
            lblPan.Text = dtCustomer.Rows[0]["PanNo"].ToString();
            lblCreditDays.Text = dtCustomer.Rows[0]["CrDays"].GetDecimalString();
            lblCrLimit.Text = dtCustomer.Rows[0]["CrLimit"].GetDecimalString();
            double.TryParse(dtCustomer.Rows[0]["Amount"].ToString(), out var result);
            lbl_CurrentBalance.Text = result > 0 ? $"{Math.Abs(result).GetDecimalString()} Dr" :
                result < 0 ? $"{Math.Abs(result).GetDecimalString()} Cr" : "0";
            if (_actionTag is not "SAVE")
            {
                return;
            }
            AgentId = dtCustomer.Rows[0]["AgentId"].GetInt();
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
            TxtVno.GetCurrentVoucherNo("SR", _returnNumberSchema);
        }
        else if (dt != null && dt.Rows.Count > 1)
        {
            var wnd = new FrmNumberingScheme("SR", "AMS.SR_Master", "SR_Invoice");
            if (wnd.ShowDialog() != DialogResult.OK) return;
            if (string.IsNullOrEmpty(wnd.VNo)) return;
            _docDesc = wnd.Description;
            TxtVno.Text = wnd.VNo;
        }
        else if (dt?.Rows.Count is 0)
        {
            MessageBox.Show(@"VOUCHER NUMBER SCHEME NOT FOUND..!!", ObjGlobal.Caption);
            TxtVno.Focus();
        }
    }

    private void ReturnSbVoucherNumber()
    {
        var dt = _master.IsExitsCheckDocumentNumbering("POS");
        if (dt?.Rows.Count is 1)
        {
            _docDesc = dt.Rows[0]["DocDesc"].ToString();
            TxtVno.Text = TxtVno.GetCurrentVoucherNo("POS", _docDesc);
        }
        else if (dt != null && dt.Rows.Count > 1)
        {
            var wnd = new FrmNumberingScheme("POS", "AMS.SB_Master", "SB_Invoice");
            if (wnd.ShowDialog() != DialogResult.OK) return;
            if (string.IsNullOrEmpty(wnd.VNo)) return;
            _docDesc = wnd.Description;
            TxtVno.Text = wnd.VNo;
        }
        else if (dt?.Rows.Count is 0)
        {
            MessageBox.Show(@"VOUCHER NUMBER SCHEME NOT FOUND..!!", ObjGlobal.Caption);
            TxtVno.Focus();
        }
    }

    private void ReturnTsbVoucherNumber()
    {
        var dt = _master.IsExitsCheckDocumentNumbering("TSB");
        if (dt?.Rows.Count is 1)
        {
            _holdSalesSchemaNumber = dt.Rows[0]["DocDesc"].ToString();
            TxtVno.Text = ObjGlobal.ReturnDocumentNumbering("AMS.temp_SB_Master", "SB_Invoice", "TSB",
                _holdSalesSchemaNumber);
        }
        else if (dt != null && dt.Rows.Count > 1)
        {
            var wnd = new FrmNumberingScheme("TSB", "AMS.temp_SB_Master", "SB_Invoice");
            if (wnd.ShowDialog() != DialogResult.OK) return;
            if (string.IsNullOrEmpty(wnd.VNo)) return;
            _docDesc = wnd.Description;
            TxtVno.Text = wnd.VNo;
        }
        else if (dt?.Rows.Count is 0)
        {
            MessageBox.Show(@"VOUCHER NUMBER SCHEME NOT FOUND..!!", ObjGlobal.Caption);
            TxtVno.Focus();
        }
    }

    private void OpenProductList()
    {
        var (description, id) = GetMasterList.GetProduct(_actionTag, MskDate.Text);
        if (description.IsValueExits())
        {
            LblProduct.Text = description;
            ProductId = id;
            SetProductInfo();
        }
        TxtBarcode.Focus();
    }

    #endregion --------------- METHOD ---------------

    // OBJECT FOR THIS FORM

    #region --------------- OBJECT ---------------

    private int RIndex { get; set; }
    private int CIndex { get; set; }
    private int MemberShipId { get; set; }
    private int DepartmentId { get; set; }
    private int SubLedgerId { get; set; }
    private int AgentId { get; set; }
    private int UnitId { get; set; }
    private int AltUnitId { get; set; }
    private int CounterId { get; set; }
    private int SGridId { get; set; }

    private int _currencyId;
    private int GetUnitId { get; set; }
    private int GetAltUnitId { get; set; }

    private bool IsRowUpdate { get; set; }
    private bool IsTaxable { get; set; }
    private bool isTaxBilling { get; set; }

    private long ProductId { get; set; }
    private long LedgerId { get; set; }
    private decimal SalesRate { get; set; }
    private decimal AltSalesRate { get; set; }
    private decimal AltQtyConv { get; set; }
    private decimal QtyConv { get; set; }
    private decimal StockQty { get; set; }

    private string GetAltUnit { get; set; }
    private decimal GetAltQty { get; set; }
    private decimal TaxRate { get; set; }
    private decimal GetMrp { get; set; }
    private decimal GetQty { get; set; }
    private decimal GetSalesRate { get; set; }
    private decimal PDiscountPercentage { get; set; }
    private decimal PDiscount { get; set; }
    private decimal PNetAmount { get; set; }

    private string _actionTag = "SAVE";
    private string _docDesc;
    private string _holdSalesSchemaNumber;
    private string _returnNumberSchema;
    private string _defaultPrinter = string.Empty;
    private string _invoiceType = "NORMAL";
    private string _selectedInvoice = "";
    private string _tempSalesInvoice;
    private string _tempInvoiceDate;
    private string _tempInvoiceMiti;
    private string _refInvoiceDate;
    private string _refInvoiceMiti;
    private string _productHsCode = string.Empty;

    private readonly string[] _tagStrings = ["DELETE", "REVERSE", "RETURN"];

    private readonly IMasterSetup _master;
    private ISalesInvoiceRepository _salesInvoice;
    private ISalesReturn _returnInvoice;
    private ISalesEntry _entry;
    private readonly ISalesDesign _design;

    private readonly IList<SalesInvoiceProductModel> _productModels;

    private DataTable _dtPTerm = new("IsPTermExitsTerm");
    private DataTable _dtPartyInfo = new("PartyInfo");
    private DialogResult _customerResult = DialogResult.None;
    private ClsPrintFunction _printFunction;
    private DbSyncRepoInjectData _injectData = new();
    private InfoResult<ValueModel<string, string, Guid>> _configParams = new();
    #endregion --------------- OBJECT ---------------
}