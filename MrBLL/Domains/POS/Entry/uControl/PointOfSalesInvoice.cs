using MrBLL.DataEntry.Common;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Design;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Global.Common;
using MrDAL.Global.WinForm;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Models.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace MrBLL.Domains.POS.Entry.uControl;

public partial class PointOfSalesInvoice : DevExpress.XtraEditors.XtraUserControl
{
    public PointOfSalesInvoice()
    {
        InitializeComponent();
        Load += PointOfSalesInvoice_Load;
        dataLayoutControl1.AllowCustomization = false;
    }

    private void PointOfSalesInvoice_Load(object sender, System.EventArgs e)
    {
    }

    // METHOD FOR THIS FORM

    #region --------------- METHOD ---------------

    private void FillMemberValue(int memberId)
    {
        var dtMemberInfo = _master.ReturnMemberShipValue(memberId);
        if (dtMemberInfo.Rows.Count <= 0) return;
        MemberShipId = memberId;
        //LblMemberName.Text = dtMemberInfo.Rows[0]["MShipDesc"].ToString();
        //LblMemberShortName.Text = dtMemberInfo.Rows[0]["MShipShortName"].ToString();
        //LblMemberType.Text = dtMemberInfo.Rows[0]["MemberDesc"].ToString();

        //TxtBillDiscountPercentage.Text = dtMemberInfo.Rows[0]["Discount"].GetDecimalString();
        //TxtBillDiscountPercentage.Enabled = dtMemberInfo.Rows[0]["Discount"].GetDecimal() <= 0;

        //var discountAmount = dtMemberInfo.Rows[0]["Discount"].GetDecimal() * TxtBasicAmount.GetDecimal() / 100;
        //TxtBillDiscountAmount.Text = discountAmount.GetDecimalString();
        //LblMemberAmount.Text = dtMemberInfo.Rows[0]["Balance"].GetDecimalString();
        //LblTag.Text = dtMemberInfo.Rows[0]["PriceTag"].ToString();
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
        //if (StockQty <= 0 && ObjGlobal.StockNegativeStockBlock)
        //{
        //    TxtBarcode.WarningMessage("SELECTED PRODUCT IS NOT AVAILABLE ..!!");
        //    return;
        //}
        //PnlProductDetails.Visible = true;
        //PnlProductDetails.Enabled = true;

        //PnlInvoiceDetails.Enabled = PnlInvoiceDetails.Visible = false;

        //TxtBarcode.Text = dtProduct.Rows[0]["PShortName"].GetString();
        //LblProduct.Text = dtProduct.Rows[0]["PName"].GetString();
        //ListOfProduct.Text = dtProduct.Rows[0]["PName"].GetString();

        //SalesRate = SalesRate is 0 ? dtProduct.Rows[0]["PSalesRate"].GetDecimal() : SalesRate;
        //UnitId = dtProduct.Rows[0]["PUnit"].GetInt();
        //AltUnitId = dtProduct.Rows[0]["PAltUnit"].GetInt();
        //AltQtyConv = dtProduct.Rows[0]["PAltConv"].GetDecimal();
        //QtyConv = dtProduct.Rows[0]["PQtyConv"].GetDecimal();
        //TxtUnit.Text = dtProduct.Rows[0]["UOM"].GetString();
        //GetAltUnit = dtProduct.Rows[0]["AltUOM"].GetString();

        //TaxRate = dtProduct.Rows[0]["PTax"].GetDecimal();
        //IsTaxable = TaxRate > 0;
        //GetMrp = dtProduct.Rows[0]["PMRP"].GetDecimal();
        //TxtQty.Text = TxtQty.GetDecimal() is 0 ? 1.00.GetDecimalQtyString() : TxtQty.Text.GetDecimalQtyString();
        //LblStockQty.Text = StockQty.GetDecimalString();
        //LblBarcode.Text = TxtBarcode.Text;
        //LblProduct.Text = ListOfProduct.Text;
        //LblUnit.Text = TxtUnit.Text;

        //TxtAltQty.Enabled = AltUnitId > 0;

        //LblSalesRate.Text = SalesRate.GetDecimalString();
    }

    private bool IsValidInvoice()
    {
        if (_actionTag.Equals("REVERSE") || _actionTag.Equals("RETURN"))
        {
            //if (CustomMessageBox.VoucherNoAction(_actionTag, TxtVno.Text) == DialogResult.No)
            //{
            //    TxtBarcode.Focus();
            //    return false;
            //}

            //if (TxtRemarks.IsBlankOrEmpty())
            //{
            //    TxtRemarks.WarningMessage($"REMARKS IS REQUIRED FOR {_actionTag}..!!");
            //    return false;
            //}
        }

        if (!_actionTag.Equals("REVERSE"))
        {
            //if (TxtCounter.Text.IsBlankOrEmpty() || CounterId is 0)
            //{
            //    TxtCounter.WarningMessage("TERMINAL IS REQUIRED FOR BILLING..!!");
            //    return false;
            //}

            //if (TxtBarcode.Text.IsBlankOrEmpty() && RGrid.RowCount is 0)
            //{
            //    TxtBarcode.WarningMessage("INVOICE PRODUCT DETAILS IS MISSING CANNOT SAVE BLANK..!!");
            //    return false;
            //}
            //if (TxtBillDiscountPercentage.GetDecimal() >= 100)
            //{
            //    TxtBillDiscountPercentage.Clear();
            //    this.NotifyValidationError(TxtBillDiscountPercentage,
            //        "DISCOUNT PERCENTAGE CAN'T MORE THEN 100%..!!");
            //    return false;
            //}
            //if (TxtBillDiscountPercentage.GetDecimal() >= 100)
            //{
            //    TxtBillDiscountPercentage.WarningMessage("DISCOUNT PERCENT CAN NOT BE EQUAL OR GREATER THAN 100");
            //    return false;
            //}
            //if (TxtCustomer.Text.IsBlankOrEmpty() || LedgerId is 0)
            //{
            //    TxtCustomer.WarningMessage("INVOICE CUSTOMER DETAILS IS MISSING CANNOT SAVE BLANK..!!");
            //    return false;
            //}
            //TxtVno_Validating(this, null);
            //MskMiti_Validating(this, null);
            //MskDate_Validating(this, null);
            //TxtCustomer_Validating(this, new CancelEventArgs(false));
            //if (TxtBillDiscountAmount.GetDecimal() > 0)
            //{
            //    TxtBillDiscountAmount_TextChanged(this, EventArgs.Empty);
            //    if (TxtBillDiscountAmount.GetDecimal() >= TxtNetAmount.GetDecimal())
            //    {
            //        TxtBillDiscountAmount.WarningMessage("DISCOUNT AMOUNT CAN NOT BE EQUAL OR GREATER THAN INVOICE AMOUNT");
            //        return false;
            //    }
            //}

            //if (TxtTenderAmount.GetDecimal() is 0 && _invoiceType.Equals("NORMAL"))
            //{
            //    TxtTenderAmount.WarningMessage("TENDER AMOUNT CAN'T BE ZERO..!!");
            //    return false;
            //}

            //if (TxtTenderAmount.GetDecimal() < TxtNetAmount.GetDecimal() && _invoiceType.Equals("NORMAL"))
            //{
            //    TxtTenderAmount.WarningMessage("TENDER AMOUNT CAN'T BE LESS THEN INVOICE AMOUNT..!!");
            //    return false;
            //}

            //if (TxtMember.IsValueExits() && MemberShipId is 0)
            //{
            //    TxtMember.WarningMessage("SELECTED MEMBER IS INVALID..!!");
            //    return false;
            //}
        }

        return true;
    }

    private void TotalCalculationOfInvoice()
    {
        //LblItemsTotalQty.Text = RGrid.Rows.OfType<DataGridViewRow>().Sum(row => row.Cells["GTxtQty"].Value.GetDecimal()).GetDecimalString();
        //LblItemsTotal.Text = RGrid.Rows.OfType<DataGridViewRow>().Sum(row => row.Cells["GTxtDisplayAmount"].Value.GetDecimal()).GetDecimalString();
        //LblItemsDiscountSum.Text = RGrid.Rows.OfType<DataGridViewRow>().Sum(row => row.Cells["GTxtPDiscount"].Value.GetDecimal()).GetDecimalString();
        //LblItemsNetAmount.Text = RGrid.Rows.OfType<DataGridViewRow>().Sum(row => row.Cells["GTxtDisplayNetAmount"].Value.GetDecimal()).GetDecimalString();
        //lblTaxable.Text = RGrid.Rows.OfType<DataGridViewRow>().Sum(row => row.Cells["GTxtValueTaxableAmount"].Value.GetDecimal()).GetDecimalString();
        //lblNonTaxable.Text = RGrid.Rows.OfType<DataGridViewRow>().Sum(row => row.Cells["GTxtValueExemptedAmount"].Value.GetDecimal()).GetDecimalString();
        //lblTax.Text = RGrid.Rows.OfType<DataGridViewRow>().Sum(row => row.Cells["GTxtValueVatAmount"].Value.GetDecimal()).GetDecimalString();
        //TxtBasicAmount.Text = LblItemsNetAmount.Text;
        //TxtNetAmount.Text = (TxtBasicAmount.GetDecimal() - TxtBillDiscountAmount.GetDecimal()).GetDecimalString();
        //TxtChangeAmount.Text = TxtTenderAmount.GetDecimal() > 0 ? (TxtTenderAmount.GetDecimal() - TxtNetAmount.GetDecimal()).GetDecimalString() : 0.GetDecimalString();
        //LblNumberInWords.Text = ClsMoneyConversion.MoneyConversion(TxtNetAmount.Text.GetDecimalString());
    }

    private void EnableControl(bool ctrl)
    {
        //BtnNew.Enabled = !ctrl;
        //TxtVno.Enabled = _tagStrings.Contains(_actionTag);
        //BtnVno.Enabled = _tagStrings.Contains(_actionTag) || ctrl;
        //MskDate.Enabled = MskMiti.Enabled = false;
        //TxtRefVno.Visible = BtnRefVno.Visible = _invoiceType.Equals("RETURN");
        //TxtRefVno.Enabled = BtnRefVno.Enabled = _invoiceType.Equals("RETURN");
        //TxtBarcode.Enabled = ctrl;
        //TxtCounter.Enabled = ctrl;
        //BtnCounter.Enabled = ctrl;
        //TxtTenderAmount.Enabled = ctrl;

        //TxtBasicAmount.Enabled = false;
        //TxtNetAmount.Enabled = false;
        //TxtChangeAmount.Enabled = false;

        //CmbPaymentType.Enabled = ctrl;
        //TxtCustomer.Enabled = ctrl;
        //btnCustomer.Enabled = ctrl;
        //ListOfProduct.Enabled = ctrl;
        //TxtQty.Enabled = ctrl;
        //TxtUnit.Enabled = false;
        //TxtAltQty.Enabled = false;
        //TxtAltUnit.Enabled = false;
        //TxtBillDiscountAmount.Enabled = ctrl;
        //TxtBillDiscountPercentage.Enabled = ctrl;
        //RGrid.Enabled = ctrl && !_tagStrings.Contains(_actionTag);
        //TxtRemarks.Enabled = _tagStrings.Contains(_actionTag) || ctrl;
        //BtnSave.Enabled = _tagStrings.Contains(_actionTag) || ctrl;
        //BtnHold.Enabled = ctrl && !_tagStrings.Contains(_actionTag);
        //BtnHold.Visible = BtnHold.Enabled;
        //RGrid.ReadOnly = true;
    }

    private void ClearControl()
    {
        //if (_actionTag.Equals("UPDATE") && ObjGlobal.IsIrdRegister)
        //{
        //    Text = _actionTag.IsValueExits() ? $"POINT OF SALES [{_actionTag}]" : "POINT OF SALES";
        //}
        //TxtVno.Clear();
        //TxtVno.Text = _actionTag.Equals("SAVE") ? TxtVno.GetCurrentVoucherNo("POS", _docDesc) : TxtVno.Text;
        //if (RGrid.Rows.Count > 0)
        //{
        //    RGrid.Rows.Clear();
        //}

        //MskDate.Enabled = !ObjGlobal.IsIrdRegister;
        //MskMiti.Enabled = MskDate.Enabled;
        //ClearDetails();
        //LblInvoiceNo.Visible = false;
        //TxtRefVno.Enabled = TxtRefVno.Visible = false;
        //BtnRefVno.Enabled = BtnRefVno.Visible = false;
        //TxtRefVno.Clear();
        //LblItemsTotal.Text = string.Empty;
        //LblItemsTotalQty.Text = string.Empty;
        //LblItemsDiscountSum.Text = string.Empty;
        //LblItemsNetAmount.Text = string.Empty;
        //lblTax.Text = string.Empty;
        //lblTaxable.Text = string.Empty;
        //lblNonTaxable.Text = string.Empty;
        //TxtMember.Clear();
        //MemberShipId = 0;
        //CmbPaymentType.SelectedIndex = 0;
        //ChkTaxInvoice.Checked = false;
        //TxtCustomer.Clear();
        //LedgerId = 0;
        //LedgerId = _master.GetUserLedgerIdFromUser(ObjGlobal.LogInUser);
        //LedgerId = LedgerId is 0 ? ObjGlobal.FinanceCashLedgerId.GetLong() : LedgerId;
        //TxtCustomer.Text = _master.GetLedgerDescription(LedgerId);
        //TxtBillDiscountAmount.Clear();
        //TxtBillDiscountPercentage.Clear();
        //TxtBasicAmount.Clear();
        //TxtNetAmount.Clear();
        //TxtTenderAmount.Clear();
        //TxtChangeAmount.Clear();
        //TxtRemarks.Clear();
        //_dtPartyInfo.Clear();
        //MemberShipId = 0;
        //LblNumberInWords.Text = string.Empty;
        //LblMemberAmount.IsClear();
        //LblMemberName.IsClear();
        //LblMemberType.IsClear();
        //LblMemberShortName.IsClear();
        //LblTag.IsClear();
        //PDetails.Visible = false;
        //BtnHold.Visible = !_tagStrings.Contains(_actionTag);
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
        //TxtBarcode.Clear();
        //TxtQty.Text = 1.GetDecimalQtyString();
        //TxtUnit.Clear();
        //StockQty = 0;
        //PnlProductDetails.Visible = false;
        //PnlProductDetails.Enabled = PnlProductDetails.Visible;
        //LblProduct.Text = string.Empty;
        //LblBarcode.Text = string.Empty;
        //LblStockQty.Text = string.Empty;
        //LblUnit.Text = string.Empty;
        //LblSalesRate.Text = string.Empty;
        //PnlInvoiceDetails.Visible = false;
        //ListOfProduct.IsClear();
    }

    private void SetDataFromGridToTextBox()
    {
        //if (RGrid.CurrentRow != null)
        //{
        //    IsRowUpdate = true;
        //    ProductId = RGrid.Rows[SGridId].Cells["GTxtProductId"].Value.GetLong();
        //    TxtBarcode.Text = RGrid.Rows[SGridId].Cells["GTxtShortName"].Value.GetString();
        //    SetProductInfo();
        //    GetAltQty = RGrid.Rows[SGridId].Cells["GTxtAltQty"].Value.GetDecimal();
        //    TxtQty.Text = RGrid.Rows[SGridId].Cells["GTxtQty"].Value.GetDecimalQtyString();
        //    GetSalesRate = RGrid.Rows[SGridId].Cells["GTxtDisplayRate"].Value.GetDecimal();
        //    PDiscountPercentage = RGrid.Rows[SGridId].Cells["GTxtDiscountRate"].Value.GetDecimal();
        //    PDiscount = RGrid.Rows[SGridId].Cells["GTxtPDiscount"].Value.GetDecimal();
        //}

        //IsRowUpdate = true;
        //TxtBarcode.Focus();
    }

    private void AddDataToGridDetails(bool isUpdate)
    {
        //LblDisplayReceivedAmount.Text = string.Empty;
        //LblDisplayReturnAmount.Text = string.Empty;
        //if (ProductId is 0 || TxtBarcode.Text.IsBlankOrEmpty())
        //{
        //    this.NotifyValidationError(TxtBarcode, "INVALID PRODUCT OR PLEASE CHECK THE BARCODE..!!");
        //    return;
        //}

        //if (TxtQty.Text.GetDecimal() is 0)
        //{
        //    this.NotifyValidationError(TxtBarcode, "QUANTITY CANNOT BE ZERO..!!");
        //    return;
        //}

        //if ((StockQty <= 0 || StockQty < TxtQty.GetDecimal()) && ObjGlobal.StockNegativeStockBlock)
        //{
        //    TxtBarcode.WarningMessage("SELECT PRODUCT IS INVALID..!!");
        //    return;
        //}
        //else if ((StockQty <= 0 || StockQty < TxtQty.GetDecimal()) && ObjGlobal.StockNegativeStockWarning)
        //{
        //    var result = CustomMessageBox.Question("STOCK IS GOING NEGATIVE, DO YOU WANT TO CONTINUE..!!");
        //    if (result is DialogResult.Yes)
        //    {
        //    }
        //    else return;
        //}

        //if (SalesRate is 0)
        //{
        //    if (MessageBox.Show(@"SALES RATE IS ZERO DO YOU WANT TO CONTINUE..!!", ObjGlobal.Caption, MessageBoxButtons.YesNo) is DialogResult.No)
        //    {
        //        TxtBarcode.Focus();
        //        return;
        //    }
        //}
        //var newRowQty = false;
        //if (!isUpdate)
        //{
        //    var row = RGrid.Rows.Cast<DataGridViewRow>().FirstOrDefault(r => string.Equals(r.Cells["GTxtProductId"].Value.GetString(), $"{ProductId}"));
        //    if (row != null)
        //    {
        //        if (CustomMessageBox.Question($@"{ListOfProduct.Text} PRODUCT IS ALREADY ADD ON THIS INVOICE DO YOU WANT TO UPDATE..??") is DialogResult.Yes)
        //        {
        //            newRowQty = true;
        //            SGridId = row.Index;
        //        }
        //    }
        //}

        //if (newRowQty)
        //{
        //    isUpdate = true;
        //}
        //else if (!isUpdate)
        //{
        //    RGrid.Rows.Add();
        //    SGridId = RGrid.RowCount - 1;
        //}

        //RGrid.Rows[SGridId].Cells["GTxtSNo"].Value = !isUpdate ? RGrid.RowCount : RGrid.Rows[SGridId].Cells["GTxtSNo"].Value;
        //RGrid.Rows[SGridId].Cells["GTxtProductId"].Value = ProductId;
        //RGrid.Rows[SGridId].Cells["GTxtShortName"].Value = TxtBarcode.Text;
        //RGrid.Rows[SGridId].Cells["GTxtProduct"].Value = ListOfProduct.Text;
        //RGrid.Rows[SGridId].Cells["GTxtGodownId"].Value = 0;
        //RGrid.Rows[SGridId].Cells["GTxtGodown"].Value = string.Empty;
        //RGrid.Rows[SGridId].Cells["GTxtAltQty"].Value = GetAltQty > 0 ? GetAltQty : string.Empty;
        //RGrid.Rows[SGridId].Cells["GTxtAltUOMId"].Value = AltUnitId;
        //RGrid.Rows[SGridId].Cells["GTxtAltUOM"].Value = GetAltUnit;

        //var qty = TxtQty.Text.GetDecimal();
        //qty = newRowQty ? RGrid.Rows[SGridId].Cells["GTxtQty"].Value.GetDecimal() + qty : qty;

        //RGrid.Rows[SGridId].Cells["GTxtQty"].Value = qty.GetDecimalString();
        //RGrid.Rows[SGridId].Cells["GTxtUOMId"].Value = UnitId;
        //RGrid.Rows[SGridId].Cells["GTxtMRP"].Value = GetMrp;
        //RGrid.Rows[SGridId].Cells["GTxtUOM"].Value = TxtUnit.Text;
        //SalesRate = GetSalesRate > 0 ? GetSalesRate : SalesRate;
        //RGrid.Rows[SGridId].Cells["GTxtDisplayRate"].Value = SalesRate.GetDecimalString();
        //RGrid.Rows[SGridId].Cells["GTxtDisplayAmount"].Value = (qty * SalesRate).GetDecimalString();
        //RGrid.Rows[SGridId].Cells["GTxtDiscountRate"].Value = PDiscountPercentage.GetDecimal();
        //RGrid.Rows[SGridId].Cells["GTxtPDiscount"].Value = PDiscount.GetDecimalString();
        //RGrid.Rows[SGridId].Cells["GTxtValueBDiscount"].Value = RGrid.Rows[SGridId].Cells["GTxtValueBDiscount"].Value.GetDecimal();
        //RGrid.Rows[SGridId].Cells["GTxtDisplayNetAmount"].Value = (qty * SalesRate - PDiscount).GetDecimalString();

        //var taxRate = ("1." + $"{TaxRate.GetDouble()}");
        //var taxableSalesRate = IsTaxable && isTaxBilling ? SalesRate / taxRate.GetDecimal() : SalesRate;
        //RGrid.Rows[SGridId].Cells["GTxtValueRate"].Value = taxableSalesRate;
        //RGrid.Rows[SGridId].Cells["GTxtValueNetAmount"].Value = taxableSalesRate * qty;

        //RGrid.Rows[SGridId].Cells["GTxtIsTaxable"].Value = IsTaxable;
        //RGrid.Rows[SGridId].Cells["GTxtTaxPriceRate"].Value = TaxRate;
        //var taxableAmount = IsTaxable && isTaxBilling
        //    ? RGrid.Rows[SGridId].Cells["GTxtDisplayNetAmount"].Value.GetDecimal() / taxRate.GetDecimal()
        //    : RGrid.Rows[SGridId].Cells["GTxtValueNetAmount"].Value.GetDecimal();

        //var vatAmount = IsTaxable && isTaxBilling
        //    ? RGrid.Rows[SGridId].Cells["GTxtDisplayNetAmount"].Value.GetDecimal() - taxableAmount : 0;

        //RGrid.Rows[SGridId].Cells["GTxtValueVatAmount"].Value = vatAmount;
        //RGrid.Rows[SGridId].Cells["GTxtValueTaxableAmount"].Value = IsTaxable && isTaxBilling ? taxableAmount : 0;
        //RGrid.Rows[SGridId].Cells["GTxtValueExemptedAmount"].Value = IsTaxable && isTaxBilling ? 0 : taxableAmount;

        //RGrid.Rows[SGridId].Cells["GTxtNarration"].Value = string.Empty;
        //RGrid.Rows[SGridId].Cells["GTxtFreeQty"].Value = 0;
        //RGrid.Rows[SGridId].Cells["GTxtFreeUnitId"].Value = 0;
        //RGrid.CurrentCell = RGrid.Rows[RGrid.RowCount - 1].Cells[0];
        //TotalCalculationOfInvoice();
        //ClearDetails();

        //TxtBarcode.Focus();
    }

    private int ReverseSelectedInvoiceNumber()
    {
        //if (_invoiceType.Equals("RETURN"))
        //{
        //    _entry.SrMaster.ActionTag = _actionTag;
        //    _entry.SrMaster.SR_Invoice = TxtVno.Text;
        //}
        //else
        //{
        //    _entry.SbMaster.ActionTag = _actionTag;
        //    _entry.SbMaster.SB_Invoice = TxtVno.Text;
        //}

        return _invoiceType.Equals("RETURN") ? _entry.SavePosReturnInvoice(_actionTag) : _entry.SavePosInvoice(_actionTag);
    }

    private int SavePosInvoice()
    {
        //_entry.SbMaster.ActionTag = _actionTag;
        //TxtVno.Text = _actionTag.Equals("SAVE") ? TxtVno.GetCurrentVoucherNo("POS", _docDesc) : TxtVno.Text;
        //_entry.SbMaster.SB_Invoice = TxtVno.Text;
        //_entry.SbMaster.Invoice_Date = MskDate.Text.GetDateTime();
        //_entry.SbMaster.Invoice_Miti = MskMiti.Text;
        //_entry.SbMaster.Invoice_Time = DateTime.Now;
        //_entry.SbMaster.PB_Vno = _tempSalesInvoice;
        //_entry.SbMaster.Vno_Date = _tempSalesInvoice.IsValueExits() ? _tempInvoiceDate.GetDateTime() : DateTime.Now;
        //_entry.SbMaster.Vno_Miti = _tempInvoiceMiti;
        //_entry.SbMaster.Customer_Id = LedgerId;

        //_entry.SbMaster.Party_Name = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["PartyName"].ToString() : string.Empty;
        //_entry.SbMaster.Vat_No = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["VatNo"].ToString() : string.Empty;
        //_entry.SbMaster.Contact_Person = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ContactPerson"].ToString() : string.Empty;
        //_entry.SbMaster.Mobile_No = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Mob"].ToString() : string.Empty;
        //_entry.SbMaster.Address = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Address"].ToString() : string.Empty;
        //_entry.SbMaster.ChqNo = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ChequeNo"].ToString() : string.Empty;
        //_entry.SbMaster.ChqDate = _dtPartyInfo.Rows.Count > 0 && !string.IsNullOrEmpty(_dtPartyInfo.Rows[0]["ChequeNo"].ToString()) ? DateTime.Parse(_dtPartyInfo.Rows[0]["ChequeDate"].ToString()) : DateTime.Now;
        //_entry.SbMaster.Invoice_Type = "LOCAL";
        //_entry.SbMaster.Invoice_Mode = "POS";
        //_entry.SbMaster.Payment_Mode = CmbPaymentType.Text;
        //_entry.SbMaster.DueDays = 0;
        //_entry.SbMaster.DueDate = DateTime.Now;
        //_entry.SbMaster.Agent_Id = AgentId;
        //_entry.SbMaster.Subledger_Id = SubLedgerId;
        //_entry.SbMaster.SO_Invoice = string.Empty;
        //_entry.SbMaster.SO_Date = DateTime.Now;
        //_entry.SbMaster.SC_Invoice = string.Empty;
        //_entry.SbMaster.SC_Date = DateTime.Now;
        //_entry.SbMaster.Cls1 = DepartmentId;
        //_entry.SbMaster.Cls2 = 0;
        //_entry.SbMaster.Cls3 = 0;
        //_entry.SbMaster.Cls4 = 0;
        //_entry.SbMaster.Cur_Id = _currencyId > 0 ? _currencyId : ObjGlobal.SysCurrencyId;
        //_entry.SbMaster.Cur_Rate = 1;
        //_entry.SbMaster.CounterId = CounterId;
        //_entry.SbMaster.MShipId = MemberShipId;
        //_entry.SbMaster.B_Amount = TxtBasicAmount.GetDecimal();
        //_entry.SbMaster.TermRate = TxtBillDiscountPercentage.GetDecimal();
        //_entry.SbMaster.T_Amount = TxtBillDiscountAmount.GetDecimal();
        //_entry.SbMaster.N_Amount = TxtNetAmount.GetDecimal();
        //_entry.SbMaster.LN_Amount = TxtNetAmount.GetDecimal();
        //_entry.SbMaster.Tender_Amount = TxtTenderAmount.GetDecimal();
        //_entry.SbMaster.Return_Amount = TxtChangeAmount.GetDecimal();
        //_entry.SbMaster.V_Amount = lblTax.Text.GetDecimal();
        //_entry.SbMaster.Tbl_Amount = lblTaxable.Text.GetDecimal();
        //_entry.SbMaster.Action_Type = _actionTag;
        //_entry.SbMaster.R_Invoice = false;
        //_entry.SbMaster.No_Print = 0;
        //_entry.SbMaster.In_Words = LblNumberInWords.Text;
        //_entry.SbMaster.Remarks = TxtRemarks.Text;
        //_entry.SbMaster.Audit_Lock = false;
        //_entry.SbMaster.GetView = RGrid;
        return _entry.SavePosInvoice(_actionTag);
    }

    private int SavePosReturnInvoice()
    {
        //_entry.SrMaster.ActionTag = _actionTag;
        //TxtVno.Text = _actionTag.Equals("SAVE") ? TxtVno.GetCurrentVoucherNo("SR", _returnNumberSchema) : TxtVno.Text;
        //_entry.SrMaster.SR_Invoice = TxtVno.Text;
        //_entry.SrMaster.Invoice_Date = MskDate.Text.GetDateTime();
        //_entry.SrMaster.Invoice_Miti = MskMiti.Text;
        //_entry.SrMaster.Invoice_Time = DateTime.Now;
        //_entry.SrMaster.SB_Invoice = TxtRefVno.Text;
        //_entry.SrMaster.SB_Date = TxtRefVno.Text.IsValueExits() ? _refInvoiceDate.GetDateTime() : DateTime.Now;
        //_entry.SrMaster.SB_Miti = _refInvoiceMiti;
        //_entry.SrMaster.Customer_ID = LedgerId;

        //_entry.SrMaster.Party_Name = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["PartyName"].ToString() : string.Empty;
        //_entry.SrMaster.Vat_No = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["VatNo"].ToString() : string.Empty;
        //_entry.SrMaster.Contact_Person = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ContactPerson"].ToString() : string.Empty;
        //_entry.SrMaster.Mobile_No = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Mob"].ToString() : string.Empty;
        //_entry.SrMaster.Address = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Address"].ToString() : string.Empty;
        //_entry.SrMaster.ChqNo = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ChequeNo"].ToString() : string.Empty;
        //_entry.SrMaster.ChqDate = _dtPartyInfo.Rows.Count > 0 && _dtPartyInfo.Rows[0]["ChequeNo"].IsValueExits() ? DateTime.Parse(_dtPartyInfo.Rows[0]["ChequeDate"].ToString()) : DateTime.Now;

        //_entry.SrMaster.Invoice_Type = "LOCAL";
        //_entry.SrMaster.Invoice_Mode = "POS";
        //_entry.SrMaster.Payment_Mode = CmbPaymentType.Text;
        //_entry.SrMaster.DueDays = 0;
        //_entry.SrMaster.DueDate = DateTime.Now;
        //_entry.SrMaster.Agent_Id = AgentId;
        //_entry.SrMaster.Subledger_Id = SubLedgerId;
        //_entry.SrMaster.Cls1 = DepartmentId;
        //_entry.SrMaster.Cls2 = 0;
        //_entry.SrMaster.Cls3 = 0;
        //_entry.SrMaster.Cls4 = 0;
        //_entry.SrMaster.Cur_Id = _currencyId > 0 ? _currencyId : ObjGlobal.SysCurrencyId;
        //_entry.SrMaster.Cur_Rate = 1;
        //_entry.SrMaster.CounterId = CounterId;
        //_entry.SrMaster.B_Amount = TxtBasicAmount.GetDecimal();
        //_entry.SrMaster.TermRate = TxtBillDiscountPercentage.GetDecimal();
        //_entry.SrMaster.T_Amount = TxtBillDiscountAmount.GetDecimal();

        //_entry.SrMaster.N_Amount = TxtNetAmount.GetDecimal();
        //_entry.SrMaster.LN_Amount = TxtNetAmount.GetDecimal();
        //_entry.SrMaster.Tender_Amount = TxtTenderAmount.GetDecimal();
        //_entry.SrMaster.Return_Amount = TxtChangeAmount.GetDecimal();
        //_entry.SrMaster.V_Amount = lblTax.Text.GetDecimal();
        //_entry.SrMaster.Tbl_Amount = lblTaxable.Text.GetDecimal();
        //_entry.SrMaster.Action_Type = _actionTag;
        //_entry.SrMaster.R_Invoice = false;
        //_entry.SrMaster.No_Print = 0;
        //_entry.SrMaster.In_Words = LblNumberInWords.Text;
        //_entry.SrMaster.Remarks = TxtRemarks.Text;
        //_entry.SrMaster.Audit_Lock = false;
        //_entry.SrMaster.GetView = RGrid;
        return _entry.SavePosReturnInvoice(_actionTag);
    }

    private int SavePosInvoiceHold()
    {
        ReturnTsbVoucherNumber();
        //_entry.TsbMaster.SB_Invoice = TxtVno.Text;
        //_entry.TsbMaster.Invoice_Date = MskDate.Text.GetDateTime();
        //_entry.TsbMaster.Invoice_Miti = MskMiti.Text;
        //_entry.TsbMaster.Invoice_Time = DateTime.Now;
        //_entry.TsbMaster.PB_Vno = string.Empty;
        //_entry.TsbMaster.Vno_Date = DateTime.Now;
        //_entry.TsbMaster.Vno_Miti = string.Empty;
        //_entry.TsbMaster.Customer_Id = LedgerId;

        //_entry.TsbMaster.Party_Name = _dtPartyInfo.Rows.Count > 0
        //    ? _dtPartyInfo.Rows[0]["PartyName"].ToString()
        //    : string.Empty;
        //_entry.TsbMaster.Vat_No =
        //    _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["VatNo"].ToString() : string.Empty;
        //_entry.TsbMaster.Contact_Person = _dtPartyInfo.Rows.Count > 0
        //    ? _dtPartyInfo.Rows[0]["ContactPerson"].ToString()
        //    : string.Empty;
        //_entry.TsbMaster.Mobile_No =
        //    _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Mob"].ToString() : string.Empty;
        //_entry.TsbMaster.Address =
        //    _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Address"].ToString() : string.Empty;
        //_entry.TsbMaster.ChqNo =
        //    _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ChequeNo"].ToString() : string.Empty;
        //_entry.TsbMaster.ChqDate =
        //    _dtPartyInfo.Rows.Count > 0 && !string.IsNullOrEmpty(_dtPartyInfo.Rows[0]["ChequeNo"].ToString())
        //        ? DateTime.Parse(_dtPartyInfo.Rows[0]["ChequeDate"].ToString())
        //        : DateTime.Now;
        //_entry.TsbMaster.Invoice_Type = "LOCAL";
        //_entry.TsbMaster.Invoice_Mode = "NORMAL";
        //_entry.TsbMaster.Payment_Mode = CmbPaymentType.Text;
        //_entry.TsbMaster.DueDays = 0;
        //_entry.TsbMaster.DueDate = DateTime.Now;
        //_entry.TsbMaster.Agent_Id = AgentId;
        //_entry.TsbMaster.Subledger_Id = SubLedgerId;
        //_entry.TsbMaster.SO_Invoice = string.Empty;
        //_entry.TsbMaster.SO_Date = DateTime.Now;
        //_entry.TsbMaster.SC_Invoice = string.Empty;
        //_entry.TsbMaster.SC_Date = DateTime.Now;
        //_entry.TsbMaster.Cls1 = DepartmentId;
        //_entry.TsbMaster.Cls2 = 0;
        //_entry.TsbMaster.Cls3 = 0;
        //_entry.TsbMaster.Cls4 = 0;
        //_entry.TsbMaster.Cur_Id = _currencyId > 0 ? _currencyId : ObjGlobal.SysCurrencyId;
        //_entry.TsbMaster.Cur_Rate = 1;
        //_entry.TsbMaster.CounterId = CounterId;
        //_entry.TsbMaster.B_Amount = TxtBasicAmount.GetDecimal();
        //_entry.TsbMaster.TermRate = TxtBillDiscountPercentage.GetDecimal();
        //_entry.TsbMaster.T_Amount = TxtBillDiscountAmount.GetDecimal();
        //_entry.TsbMaster.N_Amount = TxtNetAmount.GetDecimal();
        //_entry.TsbMaster.LN_Amount = TxtNetAmount.GetDecimal();
        //_entry.TsbMaster.Tender_Amount = TxtTenderAmount.GetDecimal();
        //_entry.TsbMaster.Return_Amount = TxtChangeAmount.GetDecimal();
        //_entry.TsbMaster.V_Amount = ((object)lblTax.Text).GetDecimal();
        //_entry.TsbMaster.Tbl_Amount = ((object)lblTaxable.Text).GetDecimal();
        //_entry.TsbMaster.Action_Type = _actionTag;
        //_entry.TsbMaster.R_Invoice = false;
        //_entry.TsbMaster.No_Print = 0;
        //_entry.TsbMaster.In_Words = LblNumberInWords.Text;
        //_entry.TsbMaster.Remarks = TxtRemarks.Text;
        //_entry.TsbMaster.Audit_Lock = false;
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
            //TxtVno.Text = dr["SB_Invoice"].ToString();
            //MskDate.Text = dr["Invoice_Date"].GetDateString();
            //MskMiti.Text = dr["Invoice_Miti"].ToString();
            //TxtCustomer.Text = dr["GLName"].ToString();
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
            //TxtRemarks.Text = Convert.ToString(dr["Remarks"].ToString());
            //TxtTenderAmount.Text = dr["Tender_Amount"].GetDecimalString();
            //TxtChangeAmount.Text = dr["Return_Amount"].GetDecimalString();
            //TxtBillDiscountAmount.Text = dr["T_Amount"].GetDecimalString();
            //var discount = dr["T_Amount"].GetDecimal() > 0
            //    ? dr["T_Amount"].GetDecimal() / dr["B_Amount"].GetDecimal() * 100
            //    : 0;
            //TxtBillDiscountPercentage.Text = discount.GetDecimalString();
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
                //RGrid.Rows[iRow].Cells["GTxtInvoiceNo"].Value = TxtRefVno.Text;
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
            //var rAmount = TxtBillDiscountAmount.GetDecimal() > 0
            //    ? TxtBillDiscountAmount.GetDecimal() / TxtBasicAmount.GetDecimal()
            //    : 0;
            //for (var i = 0; i < RGrid.RowCount; i++)
            //{
            //    var basicAmount = RGrid.Rows[i].Cells["GTxtDisplayNetAmount"].Value.GetDecimal();
            //    var dAmount = rAmount is > 0 ? basicAmount * rAmount : 0;
            //    var isTaxable = RGrid.Rows[i].Cells["GTxtIsTaxable"].Value.GetBool();
            //    var netAmount = basicAmount - dAmount;
            //    var taxableAmount = isTaxable ? netAmount / (decimal)1.13 : 0;
            //    RGrid.Rows[i].Cells["GTxtValueBDiscount"].Value = dAmount;
            //    RGrid.Rows[i].Cells["GTxtValueVatAmount"].Value = isTaxable ? netAmount - taxableAmount : 0;
            //    RGrid.Rows[i].Cells["GTxtValueTaxableAmount"].Value = isTaxable ? taxableAmount : 0;
            //    RGrid.Rows[i].Cells["GTxtValueExemptedAmount"].Value = isTaxable ? 0 : taxableAmount;
            //}
        }
    }

    private void FillInvoiceData(string voucherNo)
    {
        var dsSales = _entry.ReturnSalesInvoiceDetailsInDataSet(voucherNo);
        if (dsSales.Tables.Count <= 0) return;
        var dtMaster = dsSales.Tables[0];
        if (dtMaster.Rows.Count < 0) return;
        //foreach (DataRow dr in dtMaster.Rows)
        //{
        //    _selectedInvoice = _invoiceType.Equals("NORMAL") ? TxtVno.Text : TxtRefVno.Text;
        //    MskDate.Text = _invoiceType.Equals("RETURN") ? DateTime.Now.GetDateString() : dr["Invoice_Date"].GetDateString();
        //    MskMiti.Text = _invoiceType.Equals("RETURN") ? MskMiti.GetNepaliDate(MskDate.Text) : dr["Invoice_Miti"].ToString();

        //    if (dr["PB_Vno"].ToString() != string.Empty && _invoiceType.Equals("NORMAL"))
        //    {
        //        TxtRefVno.Text = Convert.ToString(dr["PB_Vno"].ToString());
        //        _refInvoiceMiti = dr["Vno_Miti"].GetDateString();
        //        _refInvoiceDate = ObjGlobal.ReturnEnglishDate(_refInvoiceMiti);
        //    }
        //    else if (_invoiceType.Equals("RETURN"))
        //    {
        //        TxtRefVno.Text = dr["SB_Invoice"].ToString();
        //        _refInvoiceMiti = dr["Invoice_Miti"].ToString();
        //        _refInvoiceDate = dr["Invoice_Date"].ToString();
        //    }

        //    LedgerId = _master.GetUserLedgerIdFromUser(ObjGlobal.LogInUser);
        //    LedgerId = LedgerId is 0 ? dr["Customer_ID"].GetLong() : LedgerId;
        //    TxtCustomer.Text = _master.GetLedgerDescription(LedgerId);

        //    _dtPartyInfo.Rows.Clear();
        //    var newRow = _dtPartyInfo.NewRow();
        //    newRow["PartyLedgerId"] = null;
        //    newRow["PartyName"] = dr["Party_Name"];
        //    newRow["ChequeNo"] = dr["ChqNo"];
        //    newRow["ChequeDate"] = dr["ChqDate"].GetDateString();
        //    newRow["VatNo"] = dr["Vat_No"];
        //    newRow["ContactPerson"] = dr["Contact_Person"];
        //    newRow["Address"] = dr["Address"];
        //    newRow["Mob"] = dr["Mobile_No"];
        //    _dtPartyInfo.Rows.Add(newRow);

        //    _currencyId = dr["Cur_Id"].GetInt();
        //    if (dsSales.Tables[1].Rows.Count <= 0) return;
        //    {
        //        var iRow = 0;
        //        RGrid.Rows.Clear();
        //        RGrid.Rows.Add(dsSales.Tables[1].Rows.Count);
        //        foreach (DataRow row in dsSales.Tables[1].Rows)
        //        {
        //            RGrid.Rows[iRow].Cells["GTxtSNo"].Value = row["Invoice_SNo"].ToString();
        //            RGrid.Rows[iRow].Cells["GTxtProductId"].Value = row["P_Id"].ToString();
        //            RGrid.Rows[iRow].Cells["GTxtShortName"].Value = row["PShortName"].ToString();
        //            RGrid.Rows[iRow].Cells["GTxtProduct"].Value = row["PName"].ToString();
        //            RGrid.Rows[iRow].Cells["GTxtGodownId"].Value = 0;
        //            RGrid.Rows[iRow].Cells["GTxtGodown"].Value = string.Empty;
        //            RGrid.Rows[iRow].Cells["GTxtAltQty"].Value = row["Alt_Qty"].GetDecimalString();
        //            RGrid.Rows[iRow].Cells["GTxtAltUOMId"].Value = row["Alt_UnitId"].ToString();
        //            RGrid.Rows[iRow].Cells["GTxtAltUOM"].Value = GetAltUnit;
        //            RGrid.Rows[iRow].Cells["GTxtQty"].Value = row["AltUnitCode"].ToString();
        //            var qty = row["Qty"].GetDecimal();
        //            RGrid.Rows[iRow].Cells["GTxtQty"].Value = qty.GetDecimalString();
        //            RGrid.Rows[iRow].Cells["GTxtUOMId"].Value = row["Unit_Id"].ToString();
        //            RGrid.Rows[iRow].Cells["GTxtMRP"].Value = 0;
        //            RGrid.Rows[iRow].Cells["GTxtUOM"].Value = row["UnitCode"].ToString();
        //            var salesRate = row["Rate"].GetDecimal();
        //            var pDiscount = row["PDiscount"].GetDecimal();

        //            RGrid.Rows[iRow].Cells["GTxtDisplayRate"].Value = salesRate.GetDecimalString();
        //            RGrid.Rows[iRow].Cells["GTxtDisplayAmount"].Value =
        //                (salesRate * qty).ToString(ObjGlobal.SysAmountFormat);
        //            RGrid.Rows[iRow].Cells["GTxtDiscountRate"].Value = row["PDiscountRate"].GetDecimalString();
        //            RGrid.Rows[iRow].Cells["GTxtPDiscount"].Value = pDiscount.GetDecimalString();
        //            RGrid.Rows[iRow].Cells["GTxtValueBDiscount"].Value = row["BDiscount"].GetDecimalString();
        //            var taxRate = row["PTax"].GetDecimal();
        //            var isTaxable = taxRate > 0;
        //            RGrid.Rows[iRow].Cells["GTxtDisplayNetAmount"].Value =
        //                (qty * salesRate - pDiscount).GetDecimalString();
        //            var taxableSalesRate = isTaxable ? salesRate / (decimal)1.13 : salesRate;
        //            RGrid.Rows[iRow].Cells["GTxtValueRate"].Value = taxableSalesRate;
        //            RGrid.Rows[iRow].Cells["GTxtValueNetAmount"].Value = taxableSalesRate * qty;
        //            RGrid.Rows[iRow].Cells["GTxtIsTaxable"].Value = isTaxable;
        //            RGrid.Rows[iRow].Cells["GTxtTaxPriceRate"].Value = taxRate;
        //            var taxableAmount = isTaxable
        //                ? RGrid.Rows[iRow].Cells["GTxtDisplayNetAmount"].Value.GetDecimal() / (decimal)1.13
        //                : RGrid.Rows[iRow].Cells["GTxtValueNetAmount"].Value.GetDecimal();
        //            var netAmount = RGrid.Rows[iRow].Cells["GTxtDisplayNetAmount"].Value.GetDecimal();
        //            RGrid.Rows[iRow].Cells["GTxtValueVatAmount"].Value = isTaxable ? netAmount - taxableAmount : 0;
        //            RGrid.Rows[iRow].Cells["GTxtValueTaxableAmount"].Value = isTaxable ? taxableAmount : 0;
        //            RGrid.Rows[iRow].Cells["GTxtValueExemptedAmount"].Value = isTaxable ? 0 : taxableAmount;

        //            RGrid.Rows[iRow].Cells["GTxtNarration"].Value = string.Empty;
        //            RGrid.Rows[iRow].Cells["GTxtFreeQty"].Value = 0;
        //            RGrid.Rows[iRow].Cells["GTxtFreeUnitId"].Value = 0;
        //            RGrid.Rows[iRow].Cells["GTxtInvoiceNo"].Value = TxtRefVno.Text;
        //            RGrid.Rows[iRow].Cells["GTxtInvoiceSNo"].Value = row["Invoice_SNo"].ToString();
        //            iRow++;
        //        }

        //        RGrid.CurrentCell = RGrid.Rows[RGrid.RowCount - 1].Cells[CIndex];
        //        ObjGlobal.DGridColorCombo(RGrid);
        //        TotalCalculationOfInvoice();
        //        TxtRemarks.Text = Convert.ToString(dr["Remarks"].ToString());
        //        TxtTenderAmount.Text = dr["Tender_Amount"].GetDecimalString();
        //        TxtChangeAmount.Text = dr["Return_Amount"].GetDecimalString();
        //        TxtBillDiscountAmount.Text = dr["T_Amount"].GetDecimalString();
        //        var discount = dr["T_Amount"].GetDecimal() > 0 ? dr["T_Amount"].GetDecimal() / dr["B_Amount"].GetDecimal() * 100 : 0;
        //        TxtBillDiscountPercentage.Text = discount.GetDecimalString();
        //        RGrid.ClearSelection();
        //    }

        //}
    }

    private void FillReturnInvoiceData(string voucherNo)
    {
        var dsSales = _entry.ReturnSalesReturnDetailsInDataSet(voucherNo);
        if (dsSales.Tables.Count <= 0) return;
        foreach (DataRow dr in dsSales.Tables[0].Rows)
        {
            //_selectedInvoice = TxtVno.Text;
            //MskDate.Text = DateTime.Now.GetDateString();
            //MskMiti.Text = MskDate.GetNepaliDate(MskDate.Text);

            //if (dr["SB_Invoice"].ToString() != string.Empty)
            //{
            //    TxtRefVno.Text = Convert.ToString(dr["SB_Invoice"].ToString());
            //    _refInvoiceMiti = dr["SB_Date"].GetDateString();
            //    _refInvoiceDate = dr["SB_Miti"].ToString();
            //}

            //TxtCustomer.Text = dr["GLName"].ToString();
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
            //TxtRemarks.Text = Convert.ToString(dr["Remarks"].ToString());
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
                //RGrid.Rows[iRow].Cells["GTxtInvoiceNo"].Value = TxtRefVno.Text;
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
        var dsTempSales = _entry.ReturnTempSalesInvoiceDetailsInDataSet(voucherNo);
        if (dsTempSales.Tables.Count <= 0) return;
        foreach (DataRow dr in dsTempSales.Tables[0].Rows)
        {
            _tempSalesInvoice = dr["SB_Invoice"].ToString();
            _tempInvoiceMiti = dr["Invoice_Miti"].ToString();
            _tempInvoiceDate = dr["Invoice_Date"].GetDateString();
            //if (dr["PB_Vno"].ToString() != string.Empty)
            //{
            //    TxtRefVno.Text = _tempSalesInvoice;
            //    _refInvoiceMiti = _tempInvoiceMiti;
            //    _refInvoiceDate = _tempInvoiceDate;
            //}

            //TxtCustomer.Text = Convert.ToString(dr["GLName"].ToString());
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
            //TxtRemarks.Text = Convert.ToString(dr["Remarks"].ToString());
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
                //RGrid.Rows[iRow].Cells["GTxtInvoiceNo"].Value = TxtRefVno.Text;
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
        //if (dtCustomer is { Rows: { Count: > 0 } })
        //{
        //    lblPan.Text = dtCustomer.Rows[0]["PanNo"].ToString();
        //    lblCreditDays.Text = dtCustomer.Rows[0]["CrDays"].GetDecimalString();
        //    lblCrLimit.Text = dtCustomer.Rows[0]["CrLimit"].GetDecimalString();
        //    double.TryParse(dtCustomer.Rows[0]["Amount"].ToString(), out var result);
        //    lbl_CurrentBalance.Text = result > 0 ? $"{Math.Abs(result).GetDecimalString()} Dr" :
        //        result < 0 ? $"{Math.Abs(result).GetDecimalString()} Cr" : "0";
        //    if (_actionTag is not "SAVE")
        //    {
        //        return;
        //    }
        //    AgentId = dtCustomer.Rows[0]["AgentId"].GetInt();
        //    _currencyId = dtCustomer.Rows[0]["CurrId"].GetInt();
        //}
        //else
        //{
        //    lblPan.Text = 0.GetDecimalString();
        //    lblCreditDays.Text = 0.GetDecimalString();
        //    lblCrLimit.Text = 0.GetDecimalString();
        //    lbl_CurrentBalance.Text = 0.GetDecimalString();
        //}
    }

    private void ReturnSrVoucherNumber()
    {
        var dt = _master.IsExitsCheckDocumentNumbering("SR");
        //if (dt?.Rows.Count is 1)
        //{
        //    _returnNumberSchema = dt.Rows[0]["DocDesc"].ToString();
        //    TxtVno.GetCurrentVoucherNo("SR", _returnNumberSchema);
        //}
        //else if (dt != null && dt.Rows.Count > 1)
        //{
        //    var wnd = new FrmNumberingScheme("SR", "AMS.SR_Master", "SR_Invoice");
        //    if (wnd.ShowDialog() != DialogResult.OK) return;
        //    if (string.IsNullOrEmpty(wnd.VNo)) return;
        //    _docDesc = wnd.Description;
        //    TxtVno.Text = wnd.VNo;
        //}
        //else if (dt?.Rows.Count is 0)
        //{
        //    MessageBox.Show(@"VOUCHER NUMBER SCHEME NOT FOUND..!!", ObjGlobal.Caption);
        //    TxtVno.Focus();
        //}
    }

    private void ReturnSbVoucherNumber()
    {
        var dt = _master.IsExitsCheckDocumentNumbering("POS");
        //if (dt?.Rows.Count is 1)
        //{
        //    _docDesc = dt.Rows[0]["DocDesc"].ToString();
        //    TxtVno.Text = TxtVno.GetCurrentVoucherNo("POS", _docDesc);
        //}
        //else if (dt != null && dt.Rows.Count > 1)
        //{
        //    var wnd = new FrmNumberingScheme("POS", "AMS.SB_Master", "SB_Invoice");
        //    if (wnd.ShowDialog() != DialogResult.OK) return;
        //    if (string.IsNullOrEmpty(wnd.VNo)) return;
        //    _docDesc = wnd.Description;
        //    TxtVno.Text = wnd.VNo;
        //}
        //else if (dt?.Rows.Count is 0)
        //{
        //    MessageBox.Show(@"VOUCHER NUMBER SCHEME NOT FOUND..!!", ObjGlobal.Caption);
        //    TxtVno.Focus();
        //}
    }

    private void ReturnTsbVoucherNumber()
    {
        var dt = _master.IsExitsCheckDocumentNumbering("TSB");
        //if (dt?.Rows.Count is 1)
        //{
        //    _holdSalesSchemaNumber = dt.Rows[0]["DocDesc"].ToString();
        //    TxtVno.Text = ObjGlobal.ReturnDocumentNumbering("AMS.temp_SB_Master", "SB_Invoice", "TSB",
        //        _holdSalesSchemaNumber);
        //}
        //else if (dt != null && dt.Rows.Count > 1)
        //{
        //    var wnd = new FrmNumberingScheme("TSB", "AMS.temp_SB_Master", "SB_Invoice");
        //    if (wnd.ShowDialog() != DialogResult.OK) return;
        //    if (string.IsNullOrEmpty(wnd.VNo)) return;
        //    _docDesc = wnd.Description;
        //    TxtVno.Text = wnd.VNo;
        //}
        //else if (dt?.Rows.Count is 0)
        //{
        //    MessageBox.Show(@"VOUCHER NUMBER SCHEME NOT FOUND..!!", ObjGlobal.Caption);
        //    TxtVno.Focus();
        //}
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

    private readonly string[] _tagStrings = ["DELETE", "REVERSE", "RETURN"];

    private readonly IMasterSetup _master = new ClsMasterSetup();
    private readonly ISalesEntry _entry = new ClsSalesEntry();
    private readonly ISalesDesign _design = new SalesEntryDesign();

    private readonly IList<SalesInvoiceProductModel> _productModels;

    private DataTable _dtPTerm = new("IsPTermExitsTerm");
    private DataTable _dtPartyInfo = new("PartyInfo");
    private DialogResult _customerResult = DialogResult.None;

    #endregion --------------- OBJECT ---------------

    private void BtnReturn_Click(object sender, EventArgs e)
    {
    }

    private void BtnReverse_Click(object sender, EventArgs e)
    {
    }

    private void BtnPrint_Click(object sender, EventArgs e)
    {
    }

    private void BtnLock_Click(object sender, EventArgs e)
    {
    }

    private void BtnDayClosing_Click(object sender, EventArgs e)
    {
    }

    private void BtnTodaySales_Click(object sender, EventArgs e)
    {
    }

    private void MnuHoldInvoiceList_Click(object sender, EventArgs e)
    {
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
    }
}