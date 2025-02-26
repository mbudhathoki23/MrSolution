using MrBLL.Master.LedgerSetup;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using MrDAL.Utility.dbMaster;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MrBLL.SystemSetting;

public partial class FrmSystemConfiguration : MrForm
{
    #region -------------- Form --------------

    public FrmSystemConfiguration()
    {
        InitializeComponent();

        #region -------------- InitializeComponent --------------

        BindCurrency();
        ObjGlobal.BindFiscalYear(CmbFiscalYear);
        ObjGlobal.BindPrinter(CmbDefaultPrinter);

        BindAmountItem();
        BindRateItem();
        BindCurrencyRateItem();
        BindQtyItem();
        BindAltQtyItem();

        BindFont();
        BindFontColor();
        BindFontSize();
        BindPaperSize();
        BindReportFontStyleItem();

        CmbAmountFormat.SelectedIndex = 2;
        CmbRateFormat.SelectedIndex = 2;
        CmbCurrencyFormat.SelectedIndex = 2;
        CmbQtyFormat.SelectedIndex = 2;
        CmbAltFormat.SelectedIndex = 2;

        CmbFonts.SelectedIndex = 0;
        CmbFontSize.SelectedIndex = 2;
        CmbReportStyle.SelectedIndex = 3;
        CmbSearchValue.SelectedIndex = 0;

        BindPCreditBalanceWarning();
        BindPCreditDaysWarning();
        BindSCreditBalanceWarning();
        BindSCreditDaysWarning();
        BindINegativeStockWarning();
        FillSystemConfiguration();

        #endregion -------------- InitializeComponent --------------
    }

    private void FrmSystemConfiguration_Load(object sender, EventArgs e)
    {
        _ActionTag = "UPDATE";
        EnableDisable(false);
    }

    private void FrmSystemConfiguration_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape)
            if (MessageBox.Show(@"ARE YOU SURE WANT TO CLOSE FORM..??", ObjGlobal.Caption,
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                Close();
    }

    private void TextBoxCtrl_Leave(object sender, EventArgs e)
    {
        BackColor = Color.AliceBlue;
    }

    private void TextBoxCtrl_Enter(object sender, EventArgs e)
    {
        BackColor = Color.Honeydew;
    }

    #endregion -------------- Form --------------

    #region -------------- Method --------------

    private void IudSystemConfiguration()
    {
        try
        {
            var cmdTxt = new StringBuilder();
            var getFiscalYearId =
                GetConnection.GetQueryData($"select FY_Id from ams.fiscalyear where BS_Fy='{CmbFiscalYear.Text}'");
            _ = new SqlCommand("Delete AMS.SystemConfiguration", GetConnection.GetSqlConnection())
                .ExecuteNonQuery();
            _ = new SqlCommand("INSERT INTO AMS.SystemConfiguration(SC_Id) VALUES(1)",
                GetConnection.GetSqlConnection()).ExecuteNonQuery();
            if (_ActionTag is not "UPDATE") return;

            cmdTxt.Append(" UPDATE AMS.SystemConfiguration 	SET \n");
            cmdTxt.Append(RbtnADDate.Checked ? "Date_Type = 'D' \n" : "Date_Type = 'M' \n");
            cmdTxt.Append($",Audit_Trial =CAST('{ChkAuditTrial.Checked}' AS BIT)\n");
            cmdTxt.Append($",Udf = CAST('{ChkUDF.Checked}' AS BIT)\n");
            cmdTxt.Append($",AutoPupup = CAST('{ChkAutopopList.Checked}' AS BIT)\n");
            cmdTxt.Append($",CurrentDate = CAST('{ChkCurrentDate.Checked}' AS BIT)\n");
            cmdTxt.Append($",ConfirmSave = CAST('{ChkConfirmSave.Checked}' AS BIT)\n"); //bit
            cmdTxt.Append($",ConfirmCancel =  CAST('{ChkConfirmSave.Checked}' AS BIT)\n");
            cmdTxt.Append($",DefaultPrinter = N'{CmbDefaultPrinter.Text}'\n");
            cmdTxt.Append($",FY_Id = {getFiscalYearId} \n");
            cmdTxt.Append($",BackupSch_IntvDays = {ObjGlobal.ReturnDouble(TxtBackupIntervalDays.Text)}\n");
            cmdTxt.Append($",Backup_Path = '{TxtSaveLocation.Text}'\n");
            cmdTxt.Append(ProfitLossLedgerId > 0 ? $",PL_AC = {ProfitLossLedgerId} \n" : ",PL_AC = NUll \n");
            cmdTxt.Append(CashLedgerId > 0 ? $",Cash_AC = {CashLedgerId} \n" : ",Cash_AC = NUll \n");
            cmdTxt.Append(VatLedgerId > 0 ? $",Vat_AC = {VatLedgerId} \n" : ",Vat_AC = NUll \n");
            cmdTxt.Append(BankLedgerId > 0 ? $",PDCBank_AC = {BankLedgerId} \n" : ",PDCBank_AC = NUll \n");
            cmdTxt.Append($",Transby_Code = CAST('{ChkLedgerShortNameTransaction.Checked}' AS BIT)\n");
            cmdTxt.Append($",Negative_Tran = CAST('{ChkLedgerNegativeTransaction.Checked}' AS BIT)\n");
            cmdTxt.Append($",Amount_Format = '{CmbAmountFormat.Text}'\n");
            cmdTxt.Append($",Rate_Format = '{CmbRateFormat.Text}'\n");
            cmdTxt.Append($",Qty_Format = '{CmbQtyFormat.Text}'\n");
            cmdTxt.Append($",AltQty_Format = '{CmbAltFormat.Text}' \n");
            cmdTxt.Append($",Cur_Format = '{CmbCurrencyFormat.Text}'\n");
            cmdTxt.Append($",Font_Name = '{CmbFonts.Text}'\n");
            cmdTxt.Append($",Font_Size = {CmbFontSize.Text}\n");
            cmdTxt.Append($",Paper_Size = '{CmbPaperSize.Text}'\n");
            cmdTxt.Append($",ReportFont_Style = N'{CmbReportStyle.Text}'\n");
            cmdTxt.Append($",Printing_DateTime = CAST('{ChkPrintingDateTime.Checked}' AS BIT)\n");
            cmdTxt.Append(PurchaseLedgerId > 0
                ? $",Purchase_AC = {PurchaseLedgerId} \n"
                : ",Purchase_AC = NUll \n");
            cmdTxt.Append(PurchaseReturnLedgerId > 0
                ? $",PurchaseReturn_AC = {PurchaseReturnLedgerId} \n"
                : ",PurchaseReturn_AC = NUll \n");
            cmdTxt.Append(PurchaseVatTermId > 0
                ? $",PurchaseVat_Id = {PurchaseVatTermId} \n"
                : ",PurchaseVat_Id = NUll \n");
            cmdTxt.Append(PurchaseAddVatTermId > 0
                ? $",PurchaseAddVat_Id = {PurchaseAddVatTermId} \n"
                : ",PurchaseAddVat_Id = NUll \n");
            cmdTxt.Append(PurchaseDiscountTermId > 0
                ? $",PurchaseDiscount_ID = {PurchaseDiscountTermId} \n"
                : ",PurchaseDiscount_ID = NUll \n");
            cmdTxt.Append(PurchaseProductWiseDiscountTermId > 0
                ? $",PurchaseProDiscount_Id = {PurchaseProductWiseDiscountTermId} \n"
                : ",PurchaseProDiscount_Id = NUll \n");

            cmdTxt.Append($",PCredit_Balance_War ={CmbCreditDaysWarning.SelectedIndex}\n");
            cmdTxt.Append($",PCredit_Days_War = {CmbCreditBalanceWarning.SelectedIndex}\n");
            cmdTxt.Append($",PCarry_Rate = CAST('{ChkEnablePurchaseCarryRate.Checked}' AS BIT)\n");
            cmdTxt.Append($",PLast_Rate = CAST('{ChkEnablePurchaseLastRate.Checked}' AS BIT)\n");
            cmdTxt.Append($",PBatch_Rate = CAST('{ChkEnablePurchaseBatchRate.Checked}' AS BIT)\n");
            cmdTxt.Append($",PQuality_Control = CAST('{ChkEnablePurchaseQualityControl.Checked}' AS BIT)\n");
            cmdTxt.Append($",PPGrpWiseBilling = CAST('{ChkEnablePurchaseProductGroupWise.Checked}' AS BIT)\n");
            cmdTxt.Append($",PAdvancePayment = CAST('{ChkEnablePurchaseAdvancePayment.Checked}' AS BIT)\n");
            cmdTxt.Append(SalesLedgerId > 0 ? $",Sales_AC = {SalesLedgerId} \n" : ",Sales_AC = NUll \n");
            cmdTxt.Append(SalesReturnLedgerId > 0
                ? $",SalesReturn_AC = {SalesReturnLedgerId} \n"
                : ",SalesReturn_AC = NUll \n");
            cmdTxt.Append(SalesVatTermId > 0 ? $",SalesVat_Id = {SalesVatTermId} \n" : ",SalesVat_Id = NUll \n");
            cmdTxt.Append(SalesDiscountTermId > 0
                ? $",SalesDiscount_Id = {SalesDiscountTermId} \n"
                : ",SalesDiscount_Id = NUll \n");
            cmdTxt.Append(SalesSpecialDiscountTermId > 0
                ? $",SalesSpecialDiscount_Id = {SalesSpecialDiscountTermId} \n"
                : ",SalesSpecialDiscount_Id = NUll \n");
            cmdTxt.Append(SalesServiceChargeTermId > 0
                ? $",SalesServiceCharge_Id = {SalesServiceChargeTermId} \n"
                : ",SalesServiceCharge_Id = NUll \n");
            cmdTxt.Append($",SCreditBalance_War = {CmbCreditBalanceWarning.SelectedIndex}");
            cmdTxt.Append($",SCreditDays_War = {CmbCreditDaysWarning.SelectedIndex}");
            cmdTxt.Append($",SChange_Rate = CAST('{ChkSalesChangeRate.Checked}' AS BIT)\n");
            cmdTxt.Append($",SLast_Rate = CAST('{ChkSalesLastRate.Checked}' AS BIT)\n");
            cmdTxt.Append($",SSalesCarry_Rate = CAST('{ChkSalesCarryRate.Checked}' AS BIT)\n"); //bit
            cmdTxt.Append($",SDispatch_Order = CAST('{ChkEnableSalesDispatchOrder.Checked}' AS BIT)\n"); //bit
            cmdTxt.Append($",DefaultInvoicePrinter = N'{TxtSalesInvoicePrinter.Text}'\n"); // nvarchar
            cmdTxt.Append($",DefaultInvoiceDocNumbering = N'{TxtSalesInvoiceNumbering.Text}'\n"); // nvarchar
            cmdTxt.Append($",DefaultPreInvoiceDesign = N'{TxtSalesInvoiceDesign.Text}'\n"); // nvarchar
            cmdTxt.Append($",DefaultOrderDesign = N'{TxtSalesOrderDesign.Text}'\n"); // nvarchar
            cmdTxt.Append($",DefaultOrderPrinter = N'{TxtSalesOrderPrinter.Text}'\n"); // nvarchar
            cmdTxt.Append($",DefaultOrderDocNumbering = N'{TxtSalesOrderNumbering.Text}'\n"); // nvarchar
            cmdTxt.Append(
                $",Stock_ValueInSales_Return = CAST('{ChkEnableSalesStockValueInSalesReturn.Checked}' AS BIT)\n"); //bit
            cmdTxt.Append($",Available_Stock = CAST('{ChkEnableSalesAvailableStock.Checked}' AS BIT)\n"); //bit
            cmdTxt.Append($",Customer_Name = CAST('{ChkEnableSalesCustomerName.Checked}' AS BIT)\n"); //bit
            cmdTxt.Append(
                $",SPGrpWiseBilling = CAST('{ChkEnableSalesProductGroupwisebilling.Checked}' AS BIT)\n"); //bit
            cmdTxt.Append($",TenderAmount = CAST('{ChkEnableSalesTenderAmt.Checked}' AS BIT)\n"); //bit
            cmdTxt.Append($",AdvanceReceipt = CAST('{ChkEnableSalesAdvanceReceipt.Checked}' AS BIT)\n"); //bit
            cmdTxt.Append(OpeningStockLedgerId > 0
                ? $",OpeningStockPL_AC = {OpeningStockLedgerId} \n"
                : ",OpeningStockPL_AC = NUll \n"); //int
            cmdTxt.Append(ClosingStockPLLedgerId > 0
                ? $",ClosingStockPL_AC = {ClosingStockPLLedgerId} \n"
                : ",ClosingStockPL_AC = NUll \n"); //int
            cmdTxt.Append(ClosingStockBSLedgerId > 0
                ? $",ClosingStockBS_AC = {ClosingStockBSLedgerId} \n"
                : ",ClosingStockBS_AC = NUll \n"); //int
            cmdTxt.Append($",Negative_Stock_War = {CmbNegativeStockTypes.SelectedIndex}"); //int
            cmdTxt.Append($",Product_Code = CAST('{ChkEnableInventoryProductCodeWise.Checked}' AS BIT)\n"); //bit
            cmdTxt.Append(
                $",AltQty_Alteration = CAST('{ChkEnableInventoryAltQtyAlteration.Checked}' AS BIT)\n"); //bit
            cmdTxt.Append($",Godown_Category = CAST('{ChkEnableInventoryAltQtyAlteration.Checked}' AS BIT)\n"); ////
            cmdTxt.Append($",Alteration_Part = CAST('{ChkEnableInventoryAltQtyAlteration.Checked}' AS BIT)\n"); ////
            cmdTxt.Append($",CarryBatch_Qty = CAST('{ChkEnableInventoryCarryBatchQty.Checked}' AS BIT)\n"); //bit
            cmdTxt.Append($",Breakup_Qty = CAST('{ChkEnableInventoryBreakUpQuantity.Checked}' AS BIT)\n"); //bit
            cmdTxt.Append($",Mfg_Date = CAST('{ChkEnableInventoryMfgDate.Checked}' AS BIT)\n"); //bit
            cmdTxt.Append($",Exp_Date = CAST('{ChkEnableInventoryExpiryDate.Checked}' AS BIT)\n"); //bit
            cmdTxt.Append(
                $",Exp_Date_Validation = CAST('{ChkEnableInventoryExpDateValidation.Checked}' AS BIT)\n"); //bit
            cmdTxt.Append(
                $",Mfg_Date_Validation = CAST('{ChkEnableInventoryMfgDateValidation.Checked}' AS BIT)\n"); //bit
            cmdTxt.Append($",Free_Qty = CAST('{ChkEnableInventoryFreeQty.Checked}' AS BIT)\n"); //bit
            cmdTxt.Append(
                $",Extra_Free_Qty = CAST('{ChkEnbaleInventoryExtraFreeQuantity.Checked}' AS BIT)\n"); //bit
            cmdTxt.Append(
                $",Godown_Wise_Filter = CAST('{ChkEnableInventoryGodownWiseFilter.Checked}' AS BIT)\n"); //bit
            cmdTxt.Append($",Finished_Qty = CAST('{ChkEnableInventoryFinishedQty.Checked}' AS BIT)\n"); //bit
            cmdTxt.Append($",Equal_Qty = CAST('{ChkEnableInventoryEqualQty.Checked}' AS BIT)\n"); //bit
            cmdTxt.Append(
                $",IGodown_Wise_Filter = CAST('{ChkEnableInventoryItemWiseGodownWiseFilter.Checked}' AS BIT)\n"); //bit
            cmdTxt.Append(DebtorsGroupId > 0 ? $",Debtor_Id = {DebtorsGroupId} \n" : ",Debtor_Id = NUll \n"); //int
            cmdTxt.Append(CreditorsGroupId > 0
                ? $",Creditor_Id = {CreditorsGroupId} \n"
                : ",Creditor_Id = NUll \n"); //int
            cmdTxt.Append(SalaryLedgerId > 0 ? $",Salary_Id = {SalaryLedgerId} \n" : ",Salary_Id = NUll \n"); //int
            cmdTxt.Append(TDSLedgerId > 0 ? $",TDS_Id = {TDSLedgerId} \n" : ",TDS_Id = NUll \n"); //int
            cmdTxt.Append(PFLedgerId > 0 ? $",PF_Id = {PFLedgerId} \n" : ",PF_Id = NUll \n"); //int
            cmdTxt.Append($",Email_Id = '{TxtDefaultEmailAddress.Text}'\n");
            cmdTxt.Append($",Email_Password = '{TxtEmailPassword.Text}'\n");
            cmdTxt.Append($",BarcodePrinter = '{TxtBarcodePrinter.Text}'\n");
            cmdTxt.Append($",DefaultBarcodeDesign = '{TxtBarcodeDesign.Text}'\n");
            cmdTxt.Append($",BeforeBuyRate = CAST('{ChkEnablePurchaseBeforeVatRate.Checked}' AS BIT)\n"); //bit
            cmdTxt.Append($",BeforeSalesRate = CAST('{ChkEnableSalesBeforeVat.Checked}' AS BIT)\n"); //bit
            cmdTxt.Append("WHERE SC_Id = 1; \n");

            var isInsert = new SqlCommand(cmdTxt.ToString(), GetConnection.GetSqlConnection()).ExecuteNonQuery();
            if (isInsert <= 0) return;
            var iCurr =
                $"UPDATE AMS.SystemConfiguration SET Cur_Id =  (SELECT c.CId FROM AMS.Currency c WHERE c.Ccode = '{CmbCurrency.Text}') WHERE SC_Id = '1'";
            _ = new SqlCommand(iCurr, GetConnection.GetSqlConnection()).ExecuteNonQuery();

            cmdTxt.Clear();
            cmdTxt.Append(" Delete from AMS.SystemControlOptions; \n");
            cmdTxt.Append(" Insert into AMS.SystemControlOptions \n");
            cmdTxt.Append(" VALUES \n");
            cmdTxt.Append($@"
				('FINANCE' ,'Currency' ,'{ChkEnableCurrency.Checked}' , '{ChkMandetoryCurrency.Checked}'),
				('FINANCE' ,'Agent' ,'{ChkEnableAgent.Checked}' , '{ChkMandatoryAgent.Checked}'),
				('FINANCE' ,'Department' ,'{ChkEnableDepartment.Checked}' , '{ChkMandatoryDepartment.Checked}'),
				('FINANCE' ,'DepartmentItem' ,'{ChkEnableItemDepartment.Checked}' , 'False'),
				('FINANCE' ,'Sub Ledger' ,'{ChkEnableSubledger.Checked}' , '{ChkMandatorySubledger.Checked}'),
				('FINANCE' ,'Remarks' ,'{ChkEnableRemarks.Checked}' , '{ChkMandatoryRemarks.Checked}'),
				('FINANCE' ,'Narration' ,'{ChkEnableNarration.Checked}' , 'False'),
				('FINANCE' ,'Voucher Date' ,'{ChkEnableVoucherDate.Checked}' , 'False'),
				('PURCHASE ACCOUNT' ,'Order' ,'{ChkEnablePurchaseOrder.Checked}' , '{ChkMandatoryPurchaseOrder.Checked}' ),
				('PURCHASE ACCOUNT' ,'Challan' ,'{ChkEnablePurchaseChallan.Checked}' , '{ChkMandatoryPurchaseChallan.Checked}'),
				('PURCHASE ACCOUNT' ,'SubLedger' ,'{ChkEnablePurchaseSubledger.Checked}' , '{ChkMandatoryPurchaseSubLedger.Checked}' ),
				('PURCHASE ACCOUNT' ,'Agent' ,'{chk_PAgent.Checked}' , '{ChkMandatoryPurchaseAgent.Checked}' ),
				('PURCHASE ACCOUNT' ,'Department' ,'{ChkEnablePurchaseDepartment.Checked}' , '{ChkMandatoryPurchaseDepartment.Checked}' ),
				('PURCHASE ACCOUNT' ,'Currency' ,'{ChkEnablePurchaseCurrency.Checked}' , '{ChkMandatoryPurchaseCurrency.Checked}' ),
				('PURCHASE ACCOUNT' ,'Godown' ,'{ChkEnablePurchaseGodown.Checked}' , '{ChkMandatoryPurchaseGodown.Checked}' ),
				('PURCHASE ACCOUNT' ,'Unit' ,'{ChkEnablePurchaseUnit.Checked}' , 'False' ),
				('PURCHASE ACCOUNT' ,'Alt Unit' ,'{ChkEnablePurchaseAltUnit.Checked}' , 'False' ),
				('PURCHASE ACCOUNT' ,'Basic Amount' ,'{ChkEnablePurchaseBasicAmount.Checked}' , 'False' ),
				('PURCHASE ACCOUNT' ,'Descriptions' ,'{ChkEnablePurchaseDiscriptions.Checked}' , 'False' ),
				('PURCHASE ACCOUNT' ,'Remarks' ,'{ChkEnablePurchaseRamarks.Checked}' , '{ChkMandatoryPurchaseRemarks.Checked}' ),
				('PURCHASE ACCOUNT' ,'Indent' ,'{ChkEnablePurchaseIndent.Checked}' , 'False' ),
				('PURCHASE ACCOUNT' ,'Quotation' ,'{ChkEnablePurchaseQuotation.Checked}' , 'False' ),
				('PURCHASE ACCOUNT' ,'Invoice Date' ,'{ChkEnablePurchaseInvoiceDate.Checked}' , 'False' ),
				('PURCHASE ACCOUNT' ,'Update Rate' ,'{ChkEnablePurchaseUpdateRate.Checked}' , 'False' ),
				('SALES ACCOUNT' ,'Order' ,'{ChkEnableSalesOrder.Checked}' , '{ChkMandatorySalesOrder.Checked}'),
				('SALES ACCOUNT' ,'Challan' ,'{ChkEnableSalesChallan.Checked}' , '{ChkMandatorySalesChallan.Checked}' ),
				('SALES ACCOUNT' ,'SubLedger' ,'{ChkEnableSalesSubLedger.Checked}' , '{ChkMandatorySalesSubLedger.Checked}' ),
				('SALES ACCOUNT' ,'Agent' ,'{ChkEnableSalesAgent.Checked}' , '{ChkMandatorySalesAgent.Checked}' ),
				('SALES ACCOUNT' ,'Department' ,'{ChkEnableSalesDepartment.Checked}' , '{ChkMandatorySalesDepartment.Checked}' ),
				('SALES ACCOUNT' ,'Currency' ,'{ChkEnableSalesCurrency.Checked}' , '{ChkMandatorySalesCurrency.Checked}' ),
				('SALES ACCOUNT' ,'Godown' ,'{ChkEnableSalesGodwon.Checked}' , '{ChkMandatorySalesGodown.Checked}' ),
				('SALES ACCOUNT' ,'Unit' ,'{ChkEnableSalesEnableUnit.Checked}' , 'False' ),
				('SALES ACCOUNT' ,'Alt Unit' ,'{chk_SEnableAltUnit.Checked}' , 'False' ),
				('SALES ACCOUNT' ,'Basic Amount' ,'{ChkEnableSalesEnableBasicAmt.Checked}' , 'False' ),
				('SALES ACCOUNT' ,'Descriptions' ,'{ChkEnableSalesDescriptions.Checked}' , 'False' ),
				('SALES ACCOUNT' ,'Remarks' ,'{ChkEnableSalesRemarks.Checked}' , '{ChkMandatorySalesRemarks.Checked}' ),
				('SALES ACCOUNT' ,'Dispatch Order' ,'{ChkEnableSalesDisOrder.Checked}' , 'False' ),
				('SALES ACCOUNT' ,'Quotation' ,'{ChkEnableSalesQuotation.Checked}' , 'False' ),
				('SALES ACCOUNT' ,'Invoice Date' ,'{ChkEnableSalesInvoiceDate.Checked}' , 'False' ),
				('SALES ACCOUNT' ,'Update Rate' ,'{ChkEnableSalesUpdateRate.Checked}' , 'False' ),
				('INVENTORY','Cost Center' ,'{chk_ICostCenter.Checked}' , '{chk_MandatoryICostCenter.Checked}'),
				('INVENTORY','Cost Center Item' ,'{chk_ICostCenterItem.Checked}' , 'False'),
				('INVENTORY','Godown' ,'{chk_IEnableGodwon.Checked}' , '{ChkMandatoryInventoryGodwon.Checked}'),
				('INVENTORY','Godown Item' ,'{chk_IEnableGodwonItem.Checked}' , 'False'),
				('INVENTORY','Department' ,'{chk_IEnableDepartment.Checked}' , '{chk_MandatoryIDepartment.Checked}'),
				('INVENTORY','Unit' ,'{chk_IEnableUnit.Checked}' , 'False'),
				('INVENTORY','Alt Unit' ,'{chk_IEnableAltrUnit.Checked}' , 'False'),
				('INVENTORY','Descriptions' ,'{chk_IEnableDescriptions.Checked}' , 'False'),
				('INVENTORY','Remarks' ,'{chk_IEnableRemarks.Checked}' , '{chk_MandatoryIRerarks.Checked}');
				");
            var IOk = new SqlCommand(cmdTxt.ToString(), GetConnection.GetSqlConnection()).ExecuteNonQuery();
            if (IOk > 0)
            {
                cmdTxt.Clear();
                cmdTxt.Append("Update AMS.FiscalYear set Current_FY = 0; \n");
                cmdTxt.Append(
                    $"Update AMS.FiscalYear  set Current_FY=1 where [FY_Id] = '{CmbFiscalYear.SelectedValue}' \n");
                if (new SqlCommand(cmdTxt.ToString(), GetConnection.GetSqlConnection()).ExecuteNonQuery() > 0)
                {
                    cmdTxt.Clear();
                    cmdTxt.Append("Delete AMS.IRDAPISetting ; ");
                    cmdTxt.Append(" Insert into AMS.IRDAPISetting  ");
                    cmdTxt.Append(" VALUES (   ");
                    cmdTxt.Append(!string.IsNullOrEmpty(TxtIrdAPI.Text) ? $"'{TxtIrdAPI.Text.Trim()}'," : "Null,");
                    cmdTxt.Append(
                        !string.IsNullOrEmpty(TxtIrdUser.Text) ? $"'{TxtIrdUser.Text.Trim()}'," : " Null,");
                    cmdTxt.Append(string.IsNullOrEmpty(TxtIrdUserPassword.Text)
                        ? $"'{TxtIrdUserPassword.Text.Trim()}',"
                        : " Null,");
                    cmdTxt.Append(!string.IsNullOrEmpty(TxtIrdComPan.Text)
                        ? $"'{TxtIrdComPan.Text.Trim()}',"
                        : " Null,");
                    cmdTxt.Append($"CAST('{ChkIRDRegister.Checked}' AS BIT) );");
                    if (new SqlCommand(cmdTxt.ToString(), GetConnection.GetSqlConnection()).ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show(@"SYSTEM SETTING SET SUCCESSFULLY..!!", ObjGlobal.Caption);
                        ObjGlobal.FillSystemConFiguration();
                    }
                }
            }

            if (ChkIRDRegister.Checked) CreateDatabaseTable.CreateTrigger();
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }
    }

    private void FillSystemConfiguration()
    {
        Query = " SELECT SC_Id, Date_Type, Audit_Trial, Udf, AutoPupup, CurrentDate, ConfirmSave, ConfirmCancel, Cur_Id ,c.CCode  Currency, sc.FY_Id, fy.BS_FY FiscalYear ,DefaultPrinter, BackupSch_IntvDays, Backup_Path, PL_AC,PL.GLName ProfitLedger ,Cash_AC,cb.GLName CashLedger, Vat_AC,vt.GLName VatLedger , PDCBank_AC, PDC.GLName PDCBank, Transby_Code, Negative_Tran, Amount_Format, Rate_Format, Qty_Format, AltQty_Format, Cur_Format, Font_Name, Font_Size, Paper_Size, ReportFont_Style, Printing_DateTime, Purchase_AC,pc.GLName InVoiceAccount, PurchaseReturn_AC,ra.GLName ReturnAccount, PurchaseVat_Id, PVT.PT_Name PurchaseVat, PurchaseAddVat_Id,PVAT.PT_Name PurchaseAddVat, PurchaseProDiscount_Id, PPROD.PT_Name PurchaseProDiscount, PurchaseDiscount_ID, PDIS.PT_Name PurchaseDiscount, PCredit_Balance_War, PCredit_Days_War, PCarry_Rate, PLast_Rate, PBatch_Rate, PQuality_Control, PPGrpWiseBilling, PAdvancePayment, Sales_AC, SI.GLName SalesInvoice, SalesReturn_AC,SR.GLName SalesReturn, SalesVat_Id, SV.ST_Name SalesVat, SalesDiscount_Id, SD.ST_Name SalesDiscount, SalesSpecialDiscount_Id, SPD.ST_Name SalesSpecialDscount, SalesServiceCharge_Id, SSC.ST_Name ServiceCharge, SCreditBalance_War, SCreditDays_War, SChange_Rate, SLast_Rate, SSalesCarry_Rate, SDispatch_Order, SCancellationCustomer_Code, SCancellationProduct_Id, DefaultInvoiceDesign, DefaultABTInvoiceDesign, DefaultInvoicePrinter, DefaultInvoiceDocNumbering, DefaultPreInvoiceDesign, DefaultOrderDesign, DefaultOrderPrinter, DefaultOrderDocNumbering, Stock_ValueInSales_Return, Available_Stock, Customer_Name, SPGrpWiseBilling, TenderAmount, AdvanceReceipt, OpeningStockPL_AC, IOS.GLName OpeningStock, ClosingStockPL_AC, ICS.GLName ClosingStock, ClosingStockBS_Ac,  cls.GLName StockInHand, Negative_Stock_War, Godown_Category, Product_Code, AltQty_Alteration, Alteration_Part, CarryBatch_Qty, Breakup_Qty, Mfg_Date, Exp_Date, Mfg_Date_Validation, Exp_Date_Validation, Free_Qty, Extra_Free_Qty, Godown_Wise_Filter, Finished_Qty, Equal_Qty, IGodown_Wise_Filter, Debtor_Id,DEB.GrpName Debtors,   Creditor_Id, CEB.GrpName Credtors, Salary_Id,  sal.GLName Salary, TDS_Id, TDS.GLName TDS, PF_Id, PF.GLID PF, Email_Id, Email_Password, BeforeBuyRate, BeforeSalesRate, BarcodePrinter, DefaultBarcodeDesign   from ams.SystemConfiguration sc LEFT OUTER JOIN ams.Currency c on sc.Cur_Id = c.CId LEFT OUTER JOIN ams.FiscalYear fy on sc.FY_Id = fy.FY_Id LEFT OUTER JOIN AMS.GeneralLedger PL ON SC.PL_AC = PL.GLID LEFT OUTER JOIN AMS.GeneralLedger CB ON SC.Cash_AC = CB.GLID LEFT OUTER JOIN AMS.GeneralLedger VT ON SC.Vat_AC = VT.GLID LEFT OUTER JOIN AMS.GeneralLedger CH ON SC.PDCBank_AC = CH.GLID left OUTER JOIN ams.GeneralLedger PC on sc.Purchase_AC = pc.GLID LEFT OUTER JOIN AMS.GeneralLedger RA on sc.PurchaseReturn_AC = ra.GLID left OUTER JOIN AMS.PT_Term PVT on sc.PurchaseVat_Id  = PVT.PT_Id left OUTER JOIN ams.PT_Term PVAT on sc.PurchaseAddVat_Id = PVAT.PT_Id LEFT OUTER JOIN AMS.PT_Term PPROD ON sc.PurchaseProDiscount_Id = PPROD.PT_Id left OUTER JOIN ams.PT_Term PDIS on sc.PurchaseDiscount_ID = PDIS.PT_Id LEFT outer JOIN AMS.GeneralLedger SI on sc.Sales_AC = SI.GLID  left outer JOIN ams.GeneralLedger SR on sc.SalesReturn_AC = sr.GLID left outer join ams.ST_Term SV on sc.SalesVat_Id = sv.ST_Id left outer join ams.ST_Term SD on sc.SalesDiscount_Id = SD.ST_Id left outer JOIN ams.ST_Term SPD on sc.SalesSpecialDiscount_Id = SPD.ST_Id left outer JOIN ams.ST_Term SSC on sc.SC_Id = SSC.ST_Id left outer JOIN AMS.GeneralLedger IOS on sc.OpeningStockPL_AC = IOS.GLID left outer JOIN ams.GeneralLedger ICS on sc.ClosingStockPL_AC = ICS.GLID  left outer JOIN ams.AccountGroup DEB on sc.Debtor_Id = deb.GrpId left outer JOIN AMS.AccountGroup CEB ON SC.Creditor_Id = CEB.GrpId left outer JOIN ams.GeneralLedger SAL on sc.Salary_Id = sal.GLID left OUTER JOIN ams.GeneralLedger TDS on sc.TDS_Id = tds.GLID LEFT OUTER JOIN ams.GeneralLedger PF on sc.PF_Id = pf.GLID LEFT OUTER JOIN AMS.GeneralLedger PDC on sc.PDCBank_AC = pdc.GLID LEFT OUTER JOIN ams.GeneralLedger CLS on sc.ClosingStockBS_Ac = cls.GLID; \n";
        Query += " SELECT * FROM AMS.SystemControlOptions; \n";
        Query += " SELECT * FROM [AMS].[IRDAPISetting]; ";
        var dsSystem = SqlExtensions.ExecuteDataSet(Query);

        if (dsSystem.Tables.Count > 0 && dsSystem.Tables[0].Rows.Count > 0)
        {
            _ActionTag = "UPDATE";
            try
            {
                foreach (DataRow dr in dsSystem.Tables[0].Rows)
                {
                    var fiscalYearId = dr["FY_Id"].GetInt();
                    SettingId = dr["Sc_Id"].GetInt();
                    TxtDefaultEmailAddress.Text = dr["Email_Id"].ToString();
                    TxtEmailPassword.Text = Convert.ToString(ObjGlobal.Decrypt(dr["Email_Password"].ToString()));
                    CmbFiscalYear.SelectedIndex = CmbFiscalYear.Items.Count > 0 ? fiscalYearId - 1 : -1;
                    RbtnADDate.Checked = Convert.ToString(dr["Date_Type"]) == "D";
                    RbtnBSDate.Checked = !RbtnADDate.Checked;
                    ChkAuditTrial.Checked = dr["Audit_Trial"].GetBool();
                    ChkUDF.Checked = dr["Udf"].GetBool();
                    ChkAutopopList.Checked = dr["Autopupup"].GetBool();
                    ChkCurrentDate.Checked = dr["CurrentDate"].GetBool();
                    ChkConfirmSave.Checked = dr["ConfirmSave"].GetBool();
                    ChkConfirmCancel.Checked = dr["ConfirmCancel"].GetBool();
                    CmbCurrency.SelectedValue = dr["Cur_Id"].GetInt();
                    CmbFiscalYear.SelectedValue = dr["FY_Id"].GetInt();
                    CmbAmountFormat.SelectedItem = dr["Amount_Format"].ToString();
                    CmbRateFormat.SelectedItem = dr["Rate_Format"].ToString();
                    CmbQtyFormat.SelectedItem = dr["Qty_Format"].ToString();
                    CmbAltFormat.SelectedItem = dr["AltQty_Format"].ToString();
                    CmbCurrencyFormat.SelectedItem = dr["Cur_Format"].ToString();
                    CmbFonts.SelectedItem = dr["Font_Name"].ToString();
                    CmbFontSize.SelectedItem = dr["Font_Size"].GetInt();
                    CmbPaperSize.SelectedItem = dr["Paper_Size"].ToString();
                    ChkPrintingDateTime.Checked = dr["Printing_DateTime"].GetBool();
                    ChkPrintingDateTime.Checked = dr["Printing_DateTime"].GetBool();
                    TxtBackupIntervalDays.Text = dr["BackupSch_IntvDays"].ToString();

                    ProfitLossLedgerId = dr["PL_AC"].GetLong();
                    CashLedgerId = dr["Cash_AC"].GetLong();
                    VatLedgerId = dr["VAT_AC"].GetLong();
                    BankLedgerId = dr["PDCBANK_AC"].GetLong();

                    TxtProfitLossLedger.Text = dr["ProfitLedger"].ToString();
                    TxtVatLedger.Text = dr["VatLedger"].ToString();
                    TxtCashLedger.Text = dr["CashLedger"].ToString();
                    TxtSalesVatTerm.Text = dr["SalesVat"].ToString();
                    TxtChequeInHand.Text = dr["PDCBank"].ToString();

                    ChkLedgerShortNameTransaction.Checked = dr["Transby_Code"].GetBool();
                    ChkLedgerNegativeTransaction.Checked = dr["Negative_Tran"].GetBool();

                    PurchaseLedgerId = dr["Purchase_AC"].GetLong();
                    PurchaseReturnLedgerId = dr["PurchaseReturn_AC"].GetLong();
                    PurchaseDiscountTermId = dr["PurchaseDiscount_ID"].GetInt();
                    TxtPurchaseDiscount.Text = dr["PurchaseDiscount"].ToString();
                    PurchaseProductWiseDiscountTermId = dr["PurchaseProDiscount_Id"].GetInt();
                    TxtPurchaseProDiscount.Text = dr["PurchaseProDiscount"].ToString();
                    TxtPurchaseLedger.Text = dr["InVoiceAccount"].ToString();
                    TxtReturnAccount.Text = dr["ReturnAccount"].ToString();
                    PurchaseVatTermId = dr["PurchaseVat_Id"].GetInt();
                    TxtPurchaseVat.Text = dr["PurchaseVat"].ToString();
                    PurchaseAddVatTermId = dr["PurchaseAddVat_Id"].GetInt();
                    TxtPurchaseAddVat.Text = dr["PurchaseAddVat"].ToString();

                    CmbCreditBalance.SelectedValue = dr["PCredit_Balance_War"].ToString();
                    CmbProductCreditLimit.SelectedValue = dr["PCredit_Days_War"].ToString();
                    ChkEnablePurchaseCarryRate.Checked = dr["PCarry_Rate"].GetBool();
                    ChkEnablePurchaseLastRate.Checked = dr["PLast_Rate"].GetBool();
                    ChkEnablePurchaseBatchRate.Checked = dr["PBatch_Rate"].GetBool();
                    ChkEnablePurchaseQualityControl.Checked = dr["PQuality_Control"].GetBool();

                    SalesLedgerId = dr["Sales_AC"].GetLong();
                    TxtSalesLedger.Text = dr["SalesInvoice"].ToString();
                    SalesReturnLedgerId = dr["SalesReturn_AC"].GetLong();
                    TxtSalesReturnLedger.Text = dr["SalesReturn"].ToString();

                    SalesVatTermId = dr["SalesVat_Id"].GetInt();
                    TxtSalesVatTerm.Text = dr["SalesVat"].ToString();
                    SalesDiscountTermId = dr["SalesDiscount_Id"].GetInt();
                    TxtSalesDiscountTerm.Text = dr["SalesDiscount"].ToString();
                    SalesSpecialDiscountTermId = dr["SalesSpecialDiscount_Id"].GetInt();
                    TxtSalesBDiscountTerm.Text = dr["SalesSpecialDscount"].ToString();
                    SalesServiceChargeTermId = dr["SalesServiceCharge_Id"].GetInt();
                    TxtSalesServiceTerm.Text = dr["ServiceCharge"].ToString();

                    CmbCreditDaysWarning.SelectedValue = dr["SCreditBalance_War"].ToString();
                    CmbCreditBalanceWarning.SelectedValue = dr["SCreditDays_War"].ToString();
                    ChkSalesChangeRate.Checked = dr["SChange_Rate"].GetBool();
                    ChkSalesLastRate.Checked = dr["SLast_Rate"].GetBool();
                    ChkSalesCarryRate.Checked = dr["SSalesCarry_Rate"].GetBool();
                    ChkEnableSalesStockValueInSalesReturn.Checked = dr["Stock_ValueinSales_Return"].GetBool();
                    ChkEnableSalesAvailableStock.Checked = dr["Available_Stock"].GetBool();
                    ChkEnableSalesCustomerName.Checked = dr["Customer_Name"].GetBool();
                    ChkEnableSalesDispatchOrder.Checked = dr["SDispatch_Order"].GetBool();

                    TxtSalesInvoiceDesign.Text = dr["DefaultInvoiceDesign"].ToString();
                    TxtSalesAvtPrintDesign.Text = dr["DefaultABTInvoiceDesign"].ToString();
                    TxtBarcodeDesign.Text = dr["DefaultBarcodeDesign"].ToString();
                    TxtSalesInvoicePrinter.Text = dr["DefaultInvoicePrinter"].ToString();
                    TxtSalesInvoiceNumbering.Text = dr["DefaultInvoiceDocNumbering"].ToString();
                    TxtSalesOrderDesign.Text = dr["DefaultOrderDesign"].ToString();
                    TxtSalesOrderPrinter.Text = dr["DefaultOrderPrinter"].ToString();
                    TxtSalesOrderNumbering.Text = dr["DefaultOrderDocNumbering"].ToString();
                    TxtBarcodePrinter.Text = dr["BarcodePrinter"].ToString();

                    OpeningStockLedgerId = dr["OpeningStockPL_AC"].GetLong();
                    TxtInventoryOpeningStock.Text = dr["OpeningStock"].ToString();
                    ClosingStockPLLedgerId = dr["ClosingStockPL_AC"].GetLong();
                    TxtInventoryClosingStock.Text = dr["ClosingStock"].ToString();
                    ClosingStockBSLedgerId = dr["ClosingStockBS_Ac"].GetLong();
                    TxtStockInHand.Text = dr["StockInHand"].ToString();

                    CmbNegativeStockTypes.SelectedValue = dr["Negative_Stock_War"].ToString();
                    ChkEnableInventoryGodownWiseFilter.Checked =
                        dr["Godown_Category"].GetBool();
                    ChkEnableInventoryProductCodeWise.Checked = dr["Product_Code"].GetBool();
                    ChkEnableInventoryAltQtyAlteration.Checked =
                        dr["AltQty_Alteration"].GetBool();
                    Chk_AlterationPart.Checked = dr["Alteration_Part"].GetBool();

                    ChkEnableInventoryCarryBatchQty.Checked = dr["CarryBatch_Qty"].GetBool();
                    ChkEnableInventoryBreakUpQuantity.Checked = dr["Breakup_Qty"].GetBool();
                    ChkEnableInventoryMfgDate.Checked = dr["Mfg_Date"].GetBool();
                    ChkEnableInventoryExpiryDate.Checked = dr["Exp_Date"].GetBool();
                    ChkEnableInventoryMfgDateValidation.Checked = dr["Mfg_Date_Validation"].GetBool();
                    ChkEnableInventoryExpDateValidation.Checked = dr["Exp_Date_Validation"].GetBool();
                    ChkEnableInventoryFreeQty.Checked = dr["Free_Qty"].GetBool();
                    ChkEnbaleInventoryExtraFreeQuantity.Checked = dr["Extra_Free_Qty"].GetBool();
                    ChkEnableInventoryItemWiseGodownWiseFilter.Checked = dr["Godown_Wise_Filter"].GetBool();
                    ChkEnableInventoryFinishedQty.Checked = dr["Finished_Qty"].GetBool();
                    ChkEnableInventoryEqualQty.Checked = dr["Equal_Qty"].GetBool();
                    ChkEnableInventoryItemWiseGodownWiseFilter.Checked = dr["IGodown_Wise_Filter"].GetBool();

                    DebtorsGroupId = dr["Debtor_Id"].GetInt();
                    TxtDebtorsGroup.Text = dr["Debtors"].ToString();
                    CreditorsGroupId = dr["Creditor_Id"].GetInt();
                    TxtCreditorsGroup.Text = dr["Credtors"].ToString();
                    SalaryLedgerId = dr["Salary_Id"].GetLong();
                    TxtSalaryLedger.Text = dr["Salary"].ToString();
                    TDSLedgerId = dr["TDS_Id"].GetLong();
                    TxtTDSLedger.Text = dr["TDS"].ToString();
                    PFLedgerId = dr["PF_Id"].GetLong();
                    TxtPFLedger.Text = dr["PF"].ToString();
                }

                if (dsSystem.Tables[1].Rows.Count > 0)
                    foreach (DataRow dr in dsSystem.Tables[1].Rows)
                    {
                        if (dr["Header"].ToString() == "FINANCE")
                        {
                            if (dr["Options_Name"].ToString() == "Currency")
                            {
                                ChkEnableCurrency.Checked = dr["Enable"].GetBool();
                                ChkMandetoryCurrency.Checked = dr["Mandatory"].GetBool();
                            }

                            if (dr["Options_Name"].ToString() == "Agent")
                            {
                                ChkEnableAgent.Checked = dr["Enable"].GetBool();
                                ChkMandatoryAgent.Checked = dr["Mandatory"].GetBool();
                            }

                            if (dr["Options_Name"].ToString() == "Department")
                            {
                                ChkEnableDepartment.Checked = dr["Enable"].GetBool();
                                ChkMandatoryDepartment.Checked = dr["Mandatory"].GetBool();
                            }

                            if (dr["Options_Name"].ToString() == "DepartmentItem" &&
                                ChkEnableItemDepartment.Checked == false)
                            {
                                ChkEnableItemDepartment.Checked = dr["Enable"].GetBool();
                                ChkMandatoryDepartment.Checked = dr["Mandatory"].GetBool();
                            }

                            if (dr["Options_Name"].ToString() == "Sub Ledger")
                            {
                                ChkEnableSubledger.Checked = dr["Enable"].GetBool();
                                ChkMandatorySubledger.Checked = dr["Mandatory"].GetBool();
                            }

                            if (dr["Options_Name"].ToString() is "Remarks")
                            {
                                ChkEnableRemarks.Checked = dr["Enable"].GetBool();
                                ChkMandatoryRemarks.Checked = dr["Mandatory"].GetBool();
                            }

                            if (dr["Options_Name"].ToString() is "Voucher Date")
                                ChkEnableVoucherDate.Checked = dr["Enable"].GetBool();
                            if (dr["Options_Name"].ToString() is "Narration")
                                ChkEnableNarration.Checked = dr["Enable"].GetBool();
                        }

                        if (dr["Header"].ToString() == "PURCHASE ACCOUNT")
                        {
                            if (dr["Options_Name"].ToString() is "Order")
                            {
                                ChkEnablePurchaseOrder.Checked = dr["Enable"].GetBool();
                                ChkMandatoryPurchaseOrder.Checked =
                                    dr["Mandatory"].GetBool();
                            }

                            if (dr["Options_Name"].ToString() is "Challan")
                            {
                                ChkEnablePurchaseChallan.Checked = dr["Enable"].GetBool();
                                ChkMandatoryPurchaseChallan.Checked =
                                    dr["Mandatory"].GetBool();
                            }

                            if (dr["Options_Name"].ToString() is "SubLedger")
                            {
                                ChkEnablePurchaseSubledger.Checked = dr["Enable"].GetBool();
                                ChkMandatoryPurchaseSubLedger.Checked =
                                    dr["Mandatory"].GetBool();
                            }

                            if (dr["Options_Name"].ToString() is "Agent")
                            {
                                chk_PAgent.Checked = dr["Enable"].GetBool();
                                ChkMandatoryPurchaseAgent.Checked =
                                    dr["Mandatory"].GetBool();
                            }

                            if (dr["Options_Name"].ToString() is "Currency")
                            {
                                ChkEnablePurchaseCurrency.Checked = dr["Enable"].GetBool();
                                ChkMandatoryPurchaseCurrency.Checked =
                                    dr["Mandatory"].GetBool();
                            }

                            if (dr["Options_Name"].ToString() is "Godown")
                            {
                                ChkEnablePurchaseGodown.Checked = dr["Enable"].GetBool();
                                ChkMandatoryPurchaseGodown.Checked =
                                    dr["Mandatory"].GetBool();
                            }

                            if (dr["Options_Name"].ToString() is "Unit")
                                ChkEnablePurchaseUnit.Checked = dr["Enable"].GetBool();
                            if (dr["Options_Name"].ToString() is "Alt Unit")
                                ChkEnablePurchaseAltUnit.Checked = dr["Enable"].GetBool();
                            if (dr["Options_Name"].ToString() is "Basic Amount")
                                ChkEnablePurchaseBasicAmount.Checked =
                                    dr["Enable"].GetBool();
                            if (dr["Options_Name"].ToString() is "Descriptions")
                                ChkEnablePurchaseDiscriptions.Checked =
                                    dr["Enable"].GetBool();
                            if (dr["Options_Name"].ToString() is "Remarks")
                            {
                                ChkEnablePurchaseRamarks.Checked = dr["Enable"].GetBool();
                                ChkMandatoryPurchaseRemarks.Checked =
                                    dr["Mandatory"].GetBool();
                            }

                            if (dr["Options_Name"].ToString() is "Department")
                            {
                                ChkEnablePurchaseDepartment.Checked = dr["Enable"].GetBool();
                                ChkMandatoryPurchaseDepartment.Checked =
                                    dr["Mandatory"].GetBool();
                            }

                            if (dr["Options_Name"].ToString() is "Indent")
                                ChkEnablePurchaseIndent.Checked = dr["Enable"].GetBool();
                            if (dr["Options_Name"].ToString() is "Quotation")
                                ChkEnablePurchaseQuotation.Checked = dr["Enable"].GetBool();
                            if (dr["Options_Name"].ToString() is "Invoice date")
                                ChkEnablePurchaseInvoiceDate.Checked =
                                    dr["Enable"].GetBool();
                            if (dr["Options_Name"].ToString() is "Update rate")
                                ChkEnablePurchaseUpdateRate.Checked = dr["Enable"].GetBool();
                        }

                        if (dr["Header"].ToString() is "SALES ACCOUNT")
                        {
                            if (dr["Options_Name"].ToString() is "Order")
                            {
                                ChkEnableSalesOrder.Checked = dr["Enable"].GetBool();
                                ChkMandatorySalesOrder.Checked = dr["Mandatory"].GetBool();
                            }

                            if (dr["Options_Name"].ToString() is "Challan")
                            {
                                ChkEnableSalesChallan.Checked = dr["Enable"].GetBool();
                                ChkMandatorySalesChallan.Checked = dr["Mandatory"].GetBool();
                            }

                            if (dr["Options_Name"].ToString() is "SubLedger")
                            {
                                ChkEnableSalesSubLedger.Checked = dr["Enable"].GetBool();
                                ChkMandatorySalesSubLedger.Checked =
                                    dr["Mandatory"].GetBool();
                            }

                            if (dr["Options_Name"].ToString() is "Agent")
                            {
                                ChkEnableSalesAgent.Checked = dr["Enable"].GetBool();
                                ChkMandatorySalesAgent.Checked = dr["Mandatory"].GetBool();
                            }

                            if (dr["Options_Name"].ToString() is "Currency")
                            {
                                ChkEnableSalesCurrency.Checked = dr["Enable"].GetBool();
                                ChkMandatorySalesCurrency.Checked =
                                    dr["Mandatory"].GetBool();
                            }

                            if (dr["Options_Name"].ToString() is "Godown")
                            {
                                ChkEnableSalesGodwon.Checked = dr["Enable"].GetBool();
                                ChkMandatorySalesGodown.Checked = dr["Mandatory"].GetBool();
                            }

                            if (dr["Options_Name"].ToString() is "Unit")
                                ChkEnableSalesEnableUnit.Checked = dr["Enable"].GetBool();
                            if (dr["Options_Name"].ToString() is "Alt Unit")
                                chk_SEnableAltUnit.Checked = dr["Enable"].GetBool();
                            if (dr["Options_Name"].ToString() is "Basic Amount")
                                ChkEnableSalesEnableBasicAmt.Checked =
                                    dr["Enable"].GetBool();
                            if (dr["Options_Name"].ToString() is "Descriptions")
                                ChkEnableSalesDescriptions.Checked = dr["Enable"].GetBool();
                            if (dr["Options_Name"].ToString() is "Remarks")
                            {
                                ChkEnableSalesRemarks.Checked = dr["Enable"].GetBool();
                                ChkMandatorySalesRemarks.Checked = dr["Mandatory"].GetBool();
                            }

                            if (dr["Options_Name"].ToString() is "Department")
                            {
                                ChkEnableSalesDepartment.Checked = dr["Enable"].GetBool();
                                ChkMandatorySalesDepartment.Checked =
                                    dr["Mandatory"].GetBool();
                            }

                            if (dr["Options_Name"].ToString() is "Quotation")
                                ChkEnableSalesQuotation.Checked = dr["Enable"].GetBool();
                            if (dr["Options_Name"].ToString() is "Dispatch Order")
                                ChkEnableSalesDisOrder.Checked = dr["Enable"].GetBool();
                            if (dr["Options_Name"].ToString() is "Invoice Date")
                                ChkEnableSalesInvoiceDate.Checked = dr["Enable"].GetBool();
                            if (dr["Options_Name"].ToString() is "Update Rate")
                                ChkEnableSalesUpdateRate.Checked = dr["Enable"].GetBool();
                        }

                        if (dr["Header"].ToString() is "INVENTORY")
                        {
                            if (dr["Options_Name"].ToString() is "Cost Center")
                                chk_ICostCenter.Checked = dr["Enable"].GetBool();
                            if (dr["Options_Name"].ToString() is "Cost Center Item")
                                chk_ICostCenterItem.Checked = dr["Enable"].GetBool();
                            if (dr["Options_Name"].ToString() is "Godown")
                            {
                                chk_IEnableGodwon.Checked = dr["Enable"].GetBool();
                                ChkMandatoryInventoryGodwon.Checked =
                                    dr["Mandatory"].GetBool();
                            }

                            if (dr["Options_Name"].ToString() is "Godown Item")
                                chk_IEnableGodwonItem.Checked = dr["Enable"].GetBool();
                            if (dr["Options_Name"].ToString() is "Unit")
                                chk_IEnableUnit.Checked = dr["Enable"].GetBool();
                            if (dr["Options_Name"].ToString() is "Alt Unit")
                                chk_IEnableAltrUnit.Checked = dr["Enable"].GetBool();

                            if (dr["Options_Name"].ToString() is "Remarks")
                            {
                                chk_IEnableRemarks.Checked = dr["Enable"].GetBool();
                                chk_MandatoryIRerarks.Checked = dr["Mandatory"].GetBool();
                            }

                            if (dr["Options_Name"].ToString() == "Descriptions")
                                chk_IEnableDescriptions.Checked = dr["Enable"].GetBool();
                            if (dr["Options_Name"].ToString() == "Department")
                            {
                                chk_IEnableDepartment.Checked = dr["Enable"].GetBool();
                                chk_MandatoryIDepartment.Checked = dr["Mandatory"].GetBool();
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                ex.ToNonQueryErrorResult(ex.StackTrace);
                var exception = ex;
            }
        }

        try
        {
            if (dsSystem.Tables[2].Rows.Count > 0)
                foreach (DataRow dr in dsSystem.Tables[2].Rows)
                {
                    TxtIrdAPI.Text = dr["IRDAPI"].ToString();
                    TxtIrdUser.Text = dr["IrdUser"].ToString();
                    TxtIrdUserPassword.Text = dr["IrdUserPassword"].ToString();
                    TxtIrdComPan.Text = dr["IsIRDRegister"].ToString();
                    int.TryParse(dr["IsIRDRegister"].ToString(), out var irdRegister);
                    ChkIRDRegister.Checked = irdRegister > 0;
                }
        }
        catch
        {
            // ignored
        }
    }

    public void BindCurrency()
    {
        //var ReportTable = Gobj.BindCurrency(true);
        //if (ReportTable.Rows.Count > 0)
        //{
        //    CmbCurrency.DataSource = ReportTable;
        //    CmbCurrency.DisplayMember = "CCode";
        //    CmbCurrency.ValueMember = "CId";
        //}
    }

    private void BindAmountItem()
    {
        var items = new List<string>
        {
            "0",
            "0.0",
            "0.00",
            "0.000",
            "0.0000",
            "0.00000",
            "0.000000"
        };
        CmbAmountFormat.DataSource = items;
    }

    private void BindRateItem()
    {
        var items = new List<string>
        {
            "0",
            "0.0",
            "0.00",
            "0.000",
            "0.0000",
            "0.00000",
            "0.000000"
        };
        CmbRateFormat.DataSource = items;
    }

    private void BindQtyItem()
    {
        var items = new List<string>
        {
            "0",
            "0.0",
            "0.00",
            "0.000",
            "0.0000",
            "0.00000",
            "0.000000"
        };
        CmbQtyFormat.DataSource = items;
    }

    private void BindAltQtyItem()
    {
        var items = new List<string>
        {
            "0",
            "0.0",
            "0.00",
            "0.000",
            "0.0000",
            "0.00000",
            "0.000000"
        };
        CmbAltFormat.DataSource = items;
    }

    private void BindCurrencyRateItem()
    {
        var items = new List<string>
        {
            "0",
            "0.0",
            "0.00",
            "0.000",
            "0.0000",
            "0.00000",
            "0.000000"
        };
        CmbCurrencyFormat.DataSource = items;
    }

    private void BindFontSize()
    {
        for (var i = 6; i <= 20; i++) CmbFontSize.Items.Add(i);
    }

    private void BindPaperSize()
    {
        var items = new List<string>
        {
            "A4 Full",
            "A4 Full LandScape",
            "Letter Full"
        };

        CmbPaperSize.DataSource = items;
    }

    private void BindFont()
    {
        var autoComStr = new AutoCompleteStringCollection();
        foreach (var font in FontFamily.Families)
        {
            CmbFonts.Items.Add(font.Name);
            autoComStr.Add(font.Name);
        }
    }

    private static void BindFontColor()
    {
    }

    private void BindReportFontStyleItem()
    {
        var items = new List<string>
        {
            "Upper",
            "Lower",
            "Normal", //First Letter
            "None"
        };
        CmbReportStyle.DataSource = items;
    }

    private void BindPCreditBalanceWarning()
    {
        var list = new List<ObjGlobal>();
        if (list.Count <= 0) return;
        CmbCreditBalance.DataSource = list;
        CmbCreditBalance.DisplayMember = "DisplayMember";
        CmbCreditBalance.ValueMember = "ValueMember";
        CmbCreditBalance.SelectedIndex = 2;
    }

    private void BindPCreditDaysWarning()
    {
        var list = new List<ObjGlobal>();
        list = [];
        if (list.Count <= 0) return;
        CmbProductCreditLimit.DataSource = list;
        CmbProductCreditLimit.DisplayMember = "DisplayMember";
        CmbProductCreditLimit.ValueMember = "ValueMember";
        CmbProductCreditLimit.SelectedIndex = 2;
    }

    private void BindSCreditBalanceWarning()
    {
        var list = new List<ObjGlobal>();
        list = [];
        if (list.Count <= 0) return;
        CmbCreditDaysWarning.DataSource = list;
        CmbCreditDaysWarning.DisplayMember = "DisplayMember";
        CmbCreditDaysWarning.ValueMember = "ValueMember";
        CmbCreditDaysWarning.SelectedIndex = 2;
    }

    private void BindSCreditDaysWarning()
    {
        var list = new List<ObjGlobal>();
        list = [];
        if (list.Count <= 0) return;
        CmbCreditBalanceWarning.DataSource = list;
        CmbCreditBalanceWarning.DisplayMember = "DisplayMember";
        CmbCreditBalanceWarning.ValueMember = "ValueMember";
        CmbCreditDaysWarning.SelectedIndex = 2;
    }

    private void BindINegativeStockWarning()
    {
        var list = new List<ObjGlobal>();
        list = [];
        if (list.Count <= 0) return;
        CmbNegativeStockTypes.DataSource = list;
        CmbNegativeStockTypes.DisplayMember = "DisplayMember";
        CmbNegativeStockTypes.ValueMember = "ValueMember";
        CmbNegativeStockTypes.SelectedIndex = 2;
    }

    private void EnableDisable(bool isEnable)
    {
        TxtSBSubledger.Enabled = isEnable;
        txt_SRSubledger.Enabled = isEnable;
        TxtPBSubLedger.Enabled = isEnable;
        TxtPRSubledger.Enabled = isEnable;
        txt_OPSubledger.Enabled = isEnable;
        txt_ClSubledger.Enabled = isEnable;
        txt_InvSubledger.Enabled = isEnable;
    }

    #endregion -------------- Method --------------

    #region -------------- For Popup --------------

    private void TxtProfitLossLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnProfitLossLedger_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtProfitLossLedger.Text.Trim()))
            {
                MessageBox.Show(@"PROFIT & LOSS LEDGER IS REQUIRED FOR SYSTEM SETTING..!!", ObjGlobal.Caption, MessageBoxButtons.RetryCancel);
                TxtProfitLossLedger.Focus();
            }
            else
            {
                TxtCashLedger.Focus();
            }
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            using var frm = new FrmGeneralLedger(string.Empty, true);
            frm.ShowDialog();
            TxtProfitLossLedger.Text = frm.LedgerDesc;
            ProfitLossLedgerId = frm.LedgerId;
            TxtProfitLossLedger.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtProfitLossLedger, BtnProfitLossLedger);
        }
    }

    private void BtnProfitLossLedger_Click(object sender, EventArgs e)
    {
        using var frmPickList = new FrmAutoPopList("MAX", "GENERALLEDGER", _ActionTag, ObjGlobal.SearchText, "OTHER", "MASTER");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtProfitLossLedger.Text = frmPickList.SelectedList[0]["Particular"].ToString().Trim();
                ProfitLossLedgerId = ObjGlobal.ReturnInt(frmPickList.SelectedList[0]["LedgerId"].ToString().Trim());
            }
            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"PROFIT & LOSS LEDGER NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtProfitLossLedger.Focus();
            return;
        }
        ObjGlobal.SearchText = string.Empty;
        TxtProfitLossLedger.Focus();
    }

    private void TxtCashLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control is true && e.KeyCode is Keys.N)
        {
            using var frm = new FrmGeneralLedger("Cash", true);
            frm.ShowDialog();
            TxtCashLedger.Text = frm.LedgerDesc;
            CashLedgerId = frm.LedgerId;
            TxtCashLedger.Focus();
        }
        else if (e.KeyCode is Keys.F1)
        {
            BtnCashLedger_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtCashLedger.Text.Trim()))
            {
                MessageBox.Show(@"CASH LEDGER IS REQUIRED IN SYSTEM SETTING..!!", ObjGlobal.Caption,
                    MessageBoxButtons.RetryCancel);
                TxtCashLedger.Focus();
            }
            else
            {
                TxtVatLedger.Focus();
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtCashLedger, BtnCashLedger);
        }
    }

    private void BtnCreditorsGroup_Click(object sender, EventArgs e)
    {
        using var frmPickList = new FrmAutoPopList("MIN", "ACCOUNTGROUP", ObjGlobal.SearchText, _ActionTag, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                if (!string.IsNullOrEmpty(_ActionTag) && _ActionTag != "SAVE")
                {
                    TxtCreditorsGroup.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                    CreditorsGroupId = ObjGlobal.ReturnInt(frmPickList.SelectedList[0]["LedgerId"].ToString().Trim());
                    TxtCreditorsGroup.ReadOnly = false;
                }
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"ACCOUNT GROUP NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            TxtCreditorsGroup.Focus();
        }
    }

    private void BtnStockInHand_Click(object sender, EventArgs e)
    {
        using (var frmPickList = new FrmAutoPopList("MAX", "GENERALLEDGER", _ActionTag, ObjGlobal.SearchText,
                   "OTHER", "MASTER"))
        {
            if (FrmAutoPopList.GetListTable.Rows.Count > 0)
            {
                frmPickList.ShowDialog();
                if (frmPickList.SelectedList.Count > 0)
                {
                    TxtStockInHand.Text = frmPickList.SelectedList[0]["Particular"].ToString().Trim();
                    invStockInHand =
                        ObjGlobal.ReturnLong(frmPickList.SelectedList[0]["LedgerId"].ToString().Trim());
                }

                frmPickList.Dispose();
            }
            else
            {
                MessageBox.Show(@"CLOSING STOCK LEDGER NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                TxtStockInHand.Focus();
                return;
            }
        }

        ObjGlobal.SearchText = string.Empty;
        TxtStockInHand.Focus();
    }

    private void BtnCashLedger_Click(object sender, EventArgs e)
    {
        using var frmPickList = new FrmAutoPopList("MAX", "GENERALLEDGER", _ActionTag, ObjGlobal.SearchText, "CASH", "MASTER");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtCashLedger.Text = frmPickList.SelectedList[0]["Particular"].ToString().Trim();
                CashLedgerId = ObjGlobal.ReturnLong(frmPickList.SelectedList[0]["LedgerId"].ToString().Trim());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"CASH LEDGER NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtCashLedger.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtCashLedger.Focus();
    }

    private void TxtVatLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control is true && e.KeyCode is Keys.N)
        {
            using var frm = new FrmGeneralLedger("Other", true);
            frm.ShowDialog();
            TxtVatLedger.Text = frm.LedgerDesc;
            VatLedgerId = frm.LedgerId;
            TxtVatLedger.Focus();
        }
        else if (e.KeyCode is Keys.F1)
        {
            btn_VatAC_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtVatLedger.Text.Trim()))
            {
                MessageBox.Show(@"VALUE ADDED TAX LEDGER IS REQUIRED FOR SYSTEM SETTING..!!", ObjGlobal.Caption,
                    MessageBoxButtons.RetryCancel);
                TxtVatLedger.Focus();
                return;
            }

            TxtChequeInHand.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtVatLedger, BtnVatLedger);
        }
    }

    private void btn_VatAC_Click(object sender, EventArgs e)
    {
        using var frmPickList = new FrmAutoPopList("MAX", "GENERALLEDGER", _ActionTag, ObjGlobal.SearchText, "OTHER", "MASTER");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtVatLedger.Text = frmPickList.SelectedList[0]["Particular"].ToString().Trim();
                VatLedgerId = ObjGlobal.ReturnLong(frmPickList.SelectedList[0]["LedgerId"].ToString().Trim());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"Bank Not Found..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtVatLedger.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtVatLedger.Focus();
    }

    private void txt_PDCBankAc_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control is true && e.KeyCode is Keys.N)
        {
            using var frm = new FrmGeneralLedger("Bank Account");
            frm.ShowDialog();
            TxtChequeInHand.Text = frm.LedgerDesc;
            TxtChequeInHand.Focus();
        }
        else if (e.KeyCode is Keys.F1)
        {
            btn_PDCBankAc_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            ChkLedgerShortNameTransaction.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtChequeInHand, BtnProvisionLedger);
        }
    }

    private void btn_PDCBankAc_Click(object sender, EventArgs e)
    {
        using var frmPickList = new FrmAutoPopList("MAX", "GENERALLEDGER", ObjGlobal.SearchText, string.Empty,
            "BANK", "MASTER");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtChequeInHand.Text = frmPickList.SelectedList[0]["Particular"].ToString().Trim();
                BankLedgerId = ObjGlobal.ReturnLong(frmPickList.SelectedList[0]["LedgerId"].ToString().Trim());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"BANK NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            TxtChequeInHand.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtChequeInHand.Focus();
    }

    private void txt_PurchaseAc_KeyDown(object sender, KeyEventArgs e)

    {
        if (e.Control is true && e.KeyCode is Keys.N)
        {
            using var frm = new FrmGeneralLedger(string.Empty);
            frm.ShowDialog();
            TxtPurchaseLedger.Text = frm.LedgerDesc;
            PurchaseLedgerId = frm.LedgerId;
            TxtPurchaseLedger.Focus();
        }
        else if (e.KeyCode is Keys.F1)
        {
            btn_PurchaseAc_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtPurchaseLedger.Text.Trim()))
            {
                MessageBox.Show(@"PLEASE TAG PURCHASE ACCOUNT BECASUE IT CAN'T BE LEFT BLANK!", ObjGlobal.Caption,
                    MessageBoxButtons.RetryCancel);
                TxtPurchaseLedger.Focus();
                return;
            }

            TxtReturnAccount.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtPurchaseLedger, btnPurchaseInvoice);
        }
    }

    private void btn_PurchaseAc_Click(object sender, EventArgs e)
    {
        using var frmPickList = new FrmAutoPopList("MAX", "GENERALLEDGER", ObjGlobal.SearchText, _ActionTag,
            "OTHER", "MASTER");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtPurchaseLedger.Text = frmPickList.SelectedList[0]["Particular"].ToString().Trim();
                PurchaseLedgerId = ObjGlobal.ReturnInt(frmPickList.SelectedList[0]["LedgerId"].ToString().Trim());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"CUSTOMER NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtPurchaseLedger.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtPurchaseLedger.Focus();
    }

    private void txt_PurchaseReturnAc_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control is true && e.KeyCode is Keys.N)
        {
            using var frm = new FrmGeneralLedger(string.Empty);
            frm.ShowDialog();
            TxtReturnAccount.Text = frm.LedgerDesc;
            PurchaseReturnLedgerId = frm.LedgerId;
            TxtReturnAccount.Focus();
        }
        else if (e.KeyCode is Keys.F1)
        {
            btn_PurchaseReturnAc_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            CmbCreditBalance.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtReturnAccount, btnPurchaseReturn);
        }
    }

    private void btn_PurchaseReturnAc_Click(object sender, EventArgs e)
    {
        using var frmPickList = new FrmAutoPopList("MAX", "GENERALLEDGER", ObjGlobal.SearchText, _ActionTag,
            "OTHER", "MASTER");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtReturnAccount.Text = frmPickList.SelectedList[0]["Particular"].ToString().Trim();
                PurchaseReturnLedgerId =
                    ObjGlobal.ReturnLong(frmPickList.SelectedList[0]["LedgerId"].ToString().Trim());
            }

            frmPickList.Dispose();
        }

        ObjGlobal.SearchText = string.Empty;
        TxtReturnAccount.Focus();
    }

    private void BtnPurchaseVat_Click(object sender, EventArgs e)
    {
        using var frmPickList =
            new FrmAutoPopList("MIN", "PURCHASETERM", ObjGlobal.SearchText, _ActionTag, "B", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtPurchaseVat.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                PurchaseVatTermId = ObjGlobal.ReturnInt(frmPickList.SelectedList[0]["Id"].ToString().Trim());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"NO VAT AMOUNT FOUND ..!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        ObjGlobal.SearchText = string.Empty;
        TxtPurchaseVat.Focus();
    }

    private void txt_PurchaseVat_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
            BtnPurchaseVat_Click(sender, e);
        else if (e.KeyCode is Keys.Enter)
            TxtPurchaseProDiscount.Focus();
        else
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtPurchaseVat, btnPurchaseVAT);
    }

    private void btn_PurchaseAddVat_Click(object sender, EventArgs e)
    {
        using var frmPickList =
            new FrmAutoPopList("MIN", "PURCHASETERM", ObjGlobal.SearchText, _ActionTag, "OTHER", "A");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtPurchaseAddVat.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                PurchaseAddVatTermId = ObjGlobal.ReturnInt(frmPickList.SelectedList[0]["Id"].ToString().Trim());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"ADD VAT CANNOT BE FOUND ..!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtPurchaseAddVat.Focus();
        }

        ObjGlobal.SearchText = string.Empty;
        TxtPurchaseAddVat.Focus();
    }

    private void txt_PurchaseAddVat_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
            btn_PurchaseAddVat_Click(sender, e);
        else if (e.KeyCode is Keys.Enter)
            CmbCreditBalance.Focus();
        else
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtPurchaseAddVat, btnPurchaseAdditionalVAT);
    }

    private void txt_ProDiscount_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
            btn_ProDiscount_Click(sender, e);
        else if (e.KeyCode is Keys.Enter)
            TxtPurchaseDiscount.Focus();
        else
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtPurchaseProDiscount, BtnPurchaseProDiscount);
    }

    private void btn_ProDiscount_Click(object sender, EventArgs e)
    {
        using var frmPickList = new FrmAutoPopList("MAX", "PURCHASETERM", _ActionTag, ObjGlobal.SearchText, "P", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtPurchaseProDiscount.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                PurchaseProductWiseDiscountTermId = ObjGlobal.ReturnInt(frmPickList.SelectedList[0]["Id"].ToString().Trim());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"NO DISCOUNT TERM NOT FOUND.!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtPurchaseProDiscount.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtPurchaseProDiscount.Focus();
    }

    private void txt_Discount_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
            btn_Discount_Click(sender, e);
        else if (e.KeyCode is Keys.Enter)
            CmbCreditBalance.Focus();
        else
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtPurchaseDiscount, btnPurchaseDiscount);
    }

    private void btn_Discount_Click(object sender, EventArgs e)
    {
        using var frmPickList =
            new FrmAutoPopList("MIN", "PURCHASETERM", ObjGlobal.SearchText, _ActionTag, "B", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtPurchaseDiscount.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                PurchaseDiscountTermId = Convert.ToInt16(frmPickList.SelectedList[0]["Id"].ToString().Trim());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"NO DISCOUNT.!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            TxtPurchaseDiscount.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtPurchaseDiscount.Focus();
    }

    private void TxtSalesDiscount_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
            btn_SalesDiscount_Click(sender, e);
        else if (e.KeyCode is Keys.Enter)
            TxtSalesServiceTerm.Focus();
        else
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtSalesDiscountTerm, BtnSalesDiscountTerm);
    }

    private void btn_SalesDiscount_Click(object sender, EventArgs e)
    {
        using var frmPickList =
            new FrmAutoPopList("MIN", "SALESTERM", ObjGlobal.SearchText, _ActionTag, "P", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtSalesDiscountTerm.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                SalesDiscountTermId = Convert.ToInt16(frmPickList.SelectedList[0]["Id"].ToString().Trim());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"NO DISCOUNT.!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            TxtSalesDiscountTerm.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtSalesDiscountTerm.Focus();
    }

    private void txt_SalesAc_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control is true && e.KeyCode is Keys.N)
        {
            using var frm = new FrmGeneralLedger(string.Empty, true);
            frm.ShowDialog();
            TxtSalesLedger.Text = frm.LedgerDesc;
            SalesLedgerId = frm.LedgerId;
            TxtSalesLedger.Focus();
        }
        else if (e.KeyCode is Keys.F1)
        {
            btn_SalesAc_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            TxtSalesReturnLedger.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtSalesLedger, BtnSalesLedger);
        }
    }

    private void btn_SalesAc_Click(object sender, EventArgs e)
    {
        using var frmPickList =
            new FrmAutoPopList("MAX", "GENERALLEDGER", ObjGlobal.SearchText, "", "OTHER", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtSalesLedger.Text = frmPickList.SelectedList[0]["GLName"].ToString().Trim();
                SalesLedgerId = ObjGlobal.ReturnLong(frmPickList.SelectedList[0]["GLID"].ToString().Trim());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"SALES LEDGER NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtSalesLedger.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtSalesLedger.Focus();
    }

    private void txt_SalesReturnAc_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control is true && e.KeyCode is Keys.N)
        {
            using var frm = new FrmGeneralLedger(string.Empty);
            frm.ShowDialog();
            TxtSalesReturnLedger.Text = frm.LedgerDesc;
            SalesReturnLedgerId = frm.LedgerId;
            TxtSalesReturnLedger.Focus();
        }
        else if (e.KeyCode == Keys.F1)
        {
            BtnSalesReturnLedger_Click(sender, e);
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtSalesReturnLedger, BtnSalesReturnLedger);
        }
    }

    private void BtnSalesReturnLedger_Click(object sender, EventArgs e)
    {
        using var frmPickList =
            new FrmAutoPopList("MAX", "GENERALLEDGER", ObjGlobal.SearchText, "", "OTHER", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtSalesReturnLedger.Text = frmPickList.SelectedList[0]["GLName"].ToString().Trim();
                SalesReturnLedgerId = ObjGlobal.ReturnLong(frmPickList.SelectedList[0]["GLID"].ToString().Trim());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"SALES RETURN LEDGER NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtSalesReturnLedger.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtSalesReturnLedger.Focus();
    }

    private void txt_SalesVat_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
            BtnSalesVatTermId_Click(sender, e);
        else
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtSalesVatTerm, BtnSalesVatTerm);
    }

    private void BtnSalesVatTermId_Click(object sender, EventArgs e)
    {
        using var frmPickList = new FrmAutoPopList("MIN", @"SALESTERM", ObjGlobal.SearchText, "", "P", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtSalesVatTerm.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                SalesVatTermId = ObjGlobal.ReturnInt(frmPickList.SelectedList[0]["ID"].ToString().Trim());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"SALES VAT TERM ID NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtSalesVatTerm.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtSalesVatTerm.Focus();
    }

    private void btn_SDiscountTerm_Click(object sender, EventArgs e)
    {
        using var frmPickList = new FrmAutoPopList("MIN", "SALESTERM", ObjGlobal.SearchText, "", "", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtSalesDiscountTerm.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                SalesDiscountTermId = ObjGlobal.ReturnInt(frmPickList.SelectedList[0]["ID"].ToString());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"SALES SPECIAL DISCOUNT TERM IS NOT FOUND..!!", ObjGlobal.Caption,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            TxtSalesDiscountTerm.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtSalesDiscountTerm.Focus();
    }

    private void txt_SDiscountTerm_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void btn_SSpecialDiscountTerm_Click(object sender, EventArgs e)
    {
        using var frmPickList =
            new FrmAutoPopList("MIN", "SALESTERM", _ActionTag, ObjGlobal.SearchText, "B", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtSalesBDiscountTerm.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                SalesSpecialDiscountTermId =
                    ObjGlobal.ReturnInt(frmPickList.SelectedList[0]["ID"].ToString().Trim());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"SALES DISCOUNT TERM NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtSalesBDiscountTerm.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtSalesBDiscountTerm.Focus();
    }

    private void txt_SSpecialDiscountTerm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
            btn_SSpecialDiscountTerm_Click(sender, e);
        else
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtSalesBDiscountTerm, BtnSalesBDiscountTerm);
    }

    private void btn_SServiceCharge_Click(object sender, EventArgs e)
    {
        using var frmPickList = new FrmAutoPopList("MIN", "SALESTERM", ObjGlobal.SearchText, "", "OTHER", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtSalesServiceTerm.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                SalesServiceChargeTermId = Convert.ToInt16(frmPickList.SelectedList[0]["ID"].ToString().Trim());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"SALES SERVICE CHARGE TERM NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtSalesServiceTerm.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtSalesServiceTerm.Focus();
    }

    private void txt_SalesServiceCharge_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
            btn_SServiceCharge_Click(sender, e);
        else
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtSalesServiceTerm, BtnSalesServiceTerm);
    }

    private void txt_OpeningStockPl_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control is true && e.KeyCode is Keys.N)
        {
            using var frm = new FrmGeneralLedger(string.Empty, true);
            frm.ShowDialog();
            TxtInventoryOpeningStock.Text = frm.LedgerDesc;
            OpeningStockLedgerId = frm.LedgerId;
            TxtInventoryOpeningStock.Focus();
        }
        else if (e.KeyCode is Keys.F1)
        {
            btn_OpeningStockPL_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            TxtInventoryClosingStock.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtInventoryOpeningStock, Btn_OpeningStockPL);
        }
    }

    private void btn_OpeningStockPL_Click(object sender, EventArgs e)
    {
        using var frmPickList = new FrmAutoPopList("MAX", "GENERALLEDGER", _ActionTag, ObjGlobal.SearchText,
            "OTHER", "MASTER");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtInventoryOpeningStock.Text = frmPickList.SelectedList[0]["Particular"].ToString().Trim();
                OpeningStockLedgerId =
                    ObjGlobal.ReturnLong(frmPickList.SelectedList[0]["LedgerId"].ToString().Trim());
            }

            frmPickList.Dispose();
        }

        ObjGlobal.SearchText = string.Empty;
        TxtInventoryOpeningStock.Focus();
    }

    private void txt_ClosingStockPL_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control is true && e.KeyCode is Keys.N)
        {
            using var frm = new FrmGeneralLedger(string.Empty);
            frm.ShowDialog();
            TxtInventoryClosingStock.Text = frm.LedgerDesc;
            ClosingStockPLLedgerId = frm.LedgerId;
            TxtInventoryClosingStock.Focus();
        }
        else if (e.KeyCode is Keys.F1)
        {
            btn_ClosingStockPL_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            TxtStockInHand.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtInventoryClosingStock, Btn_ClosingStockPL);
        }
    }

    private void btn_ClosingStockPL_Click(object sender, EventArgs e)
    {
        using var frmPickList = new FrmAutoPopList("MAX", "GENERALLEDGER", _ActionTag, ObjGlobal.SearchText,
            "OTHER", "MASTER");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtInventoryClosingStock.Text = frmPickList.SelectedList[0]["Particular"].ToString().Trim();
                ClosingStockPLLedgerId =
                    ObjGlobal.ReturnLong(frmPickList.SelectedList[0]["LedgerId"].ToString().Trim());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"CLOSING STOCK LEDGER NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtInventoryClosingStock.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtInventoryClosingStock.Focus();
    }

    private void txt_ClosingStockBS_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control is true && e.KeyCode is Keys.N)
        {
            using var frm = new FrmGeneralLedger(string.Empty);
            frm.ShowDialog();
            TxtStockInHand.Text = frm.LedgerDesc;
            ClosingStockBSLedgerId = frm.LedgerId;
            TxtStockInHand.Focus();
        }
        else if (e.KeyCode is Keys.F1)
        {
            BtnStockInHand_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            CmbNegativeStockTypes.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtStockInHand, BtnStockInHand);
        }
    }

    private void btn_ClosingStockBS_Click(object sender, EventArgs e)
    {
        using var frmPickList =
            new FrmAutoPopList("MAX", "GENERALLEDGER", ObjGlobal.SearchText, "", "OTHER", "MASTER");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtStockInHand.Text = frmPickList.SelectedList[0]["Particular"].ToString().Trim();
                ClosingStockBSLedgerId =
                    ObjGlobal.ReturnLong(frmPickList.SelectedList[0]["LedgerId"].ToString().Trim());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"CLOSING STOCK LEDGER NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtInventoryClosingStock.Focus();
        }
    }

    private void txt_BackupSchIntervaldays_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !int.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private bool IsValidForm()
    {
        if (ProfitLossLedgerId is 0 || string.IsNullOrEmpty(TxtProfitLossLedger.Text.Trim()))
        {
            MessageBox.Show(@"PROFIT AND LOSS LEDGER IS BLANK ..!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtProfitLossLedger.Focus();
            return false;
        }

        if (CashLedgerId is 0 || string.IsNullOrEmpty(TxtCashLedger.Text.Trim()))
        {
            MessageBox.Show(@"CASH ACCOUNT LEDGER IS BLANK ..!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtCashLedger.Focus();
            return false;
        }

        if (VatLedgerId is 0 || string.IsNullOrEmpty(TxtVatLedger.Text.Trim()))
        {
            MessageBox.Show(@"VAT ACCOUNT LEDGER IS BLANK ..!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtVatLedger.Focus();
            return false;
        }

        if (PurchaseLedgerId is 0 || string.IsNullOrEmpty(TxtPurchaseLedger.Text.Trim()))
        {
            MessageBox.Show(@"INVOICE ACCOUNT LEDGER IS BLANK ..!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtPurchaseLedger.Focus();
            return false;
        }

        if (PurchaseReturnLedgerId is 0 || string.IsNullOrEmpty(TxtReturnAccount.Text.Trim()))
        {
            MessageBox.Show(@"RETURN ACCOUNT LEDGER IS BLANK ..!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtReturnAccount.Focus();
            return false;
        }

        if (SalesLedgerId is 0 || string.IsNullOrEmpty(TxtSalesLedger.Text.Trim()))
        {
            MessageBox.Show(@"SALES INVOICE LEDGER IS BLANK ..!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtSalaryLedger.Focus();
            return false;
        }

        if (SalesReturnLedgerId is 0 || string.IsNullOrEmpty(TxtSalesReturnLedger.Text.Trim()))
        {
            MessageBox.Show(@"SALES RETURN LEDGER IS BLANK ..!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtSalesReturnLedger.Focus();
            return false;
        }

        if (OpeningStockLedgerId is 0 || string.IsNullOrEmpty(TxtInventoryOpeningStock.Text.Trim()))
        {
            MessageBox.Show(@"OPENING STOCK LEDGER IS BLANK ..!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtInventoryOpeningStock.Focus();
            return false;
        }

        if (string.IsNullOrEmpty(TxtInventoryClosingStock.Text.Trim()))
        {
            MessageBox.Show(@"CLOSING STOCK LEDGER IS BLANK ..!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtInventoryClosingStock.Focus();
            return false;
        }

        return true;
    }

    private void btn_Save_Click(object sender, EventArgs e)
    {
        if (IsValidForm()) IudSystemConfiguration();
    }

    private void btn_SaveClosed_Click(object sender, EventArgs e)
    {
        IudSystemConfiguration();
        Close();
    }

    private void TxtDebtorsGroup_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control is true && e.KeyCode is Keys.N)
        {
            using var frm = new FrmAccountGroup(true);
            frm.ShowDialog();
            TxtDebtorsGroup.Text = frm.GroupDesc;
            DebtorsGroupId = frm.GroupId;
            TxtDebtorsGroup.Focus();
        }
        else if (e.KeyCode is Keys.F1)
        {
            BtnDebtorsGroup_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            TxtDebtorsGroup.Focus();
        }
    }

    private void txt_Creditors_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control is true && e.KeyCode is Keys.N)
        {
            var frm = new FrmAccountGroup(true);
            frm.ShowDialog();
            TxtCreditorsGroup.Text = frm.GroupDesc;
            CreditorsGroupId = frm.GroupId;
            TxtCreditorsGroup.Focus();
        }
        else if (e.KeyCode is Keys.F1)
        {
            BtnDebtorsGroup_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            btnSave.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtCreditorsGroup, BtnCreditorsGroup);
        }
    }

    #endregion -------------- For Popup --------------

    #region --------------- Enable Disable Check box Validation for Finance ---------------

    private void ChkEnableCurrency_CheckedChanged(object sender, EventArgs e)
    {
        ChkMandetoryCurrency.Enabled = ChkMandetoryCurrency.Checked = ChkEnableCurrency.Checked;
    }

    private void ChkEnableAgent_CheckedChanged(object sender, EventArgs e)
    {
        ChkMandatoryAgent.Enabled = ChkMandatoryAgent.Checked = ChkEnableAgent.Checked;
    }

    private void ChkEnableDepartment_CheckedChanged(object sender, EventArgs e)
    {
        ChkEnableItemDepartment.Checked = ChkMandatoryDepartment.Enabled = ChkEnableDepartment.Checked;
    }

    private void ChkEnableItemDepartment_CheckedChanged(object sender, EventArgs e)
    {
        ChkEnableDepartment.Checked = ChkMandatoryDepartment.Enabled = ChkEnableItemDepartment.Checked;
    }

    private void ChkEnableSubledger_CheckedChanged(object sender, EventArgs e)
    {
        ChkMandatorySubledger.Enabled = ChkMandatorySubledger.Checked = ChkEnableSubledger.Checked;
    }

    private void ChkEnableRemarks_CheckedChanged(object sender, EventArgs e)
    {
        ChkMandatoryRemarks.Enabled = ChkMandatoryRemarks.Checked = ChkEnableRemarks.Checked;
    }

    #endregion --------------- Enable Disable Check box Validation for Finance ---------------

    #region --------------- Enable Disable Check box Validation For Purchase ---------------

    private void ChkEnablePurchaseOrder_CheckedChanged(object sender, EventArgs e)
    {
        ChkMandatoryPurchaseOrder.Enabled = ChkMandatoryPurchaseOrder.Checked = ChkEnablePurchaseOrder.Checked;
    }

    private void ChkEnablePurchaseChallan_CheckedChanged(object sender, EventArgs e)
    {
        ChkMandatoryPurchaseChallan.Enabled =
            ChkMandatoryPurchaseChallan.Checked = ChkEnablePurchaseChallan.Checked;
    }

    private void chk_PSubLedger_CheckedChanged(object sender, EventArgs e)
    {
        ChkMandatoryPurchaseSubLedger.Enabled =
            ChkMandatoryPurchaseSubLedger.Checked = ChkEnablePurchaseSubledger.Checked;
    }

    private void chk_PAgent_CheckedChanged(object sender, EventArgs e)
    {
        ChkMandatoryPurchaseAgent.Enabled = ChkMandatoryPurchaseAgent.Checked = chk_PAgent.Checked;
    }

    private void chk_PCurrency_CheckedChanged(object sender, EventArgs e)
    {
        ChkMandatoryPurchaseCurrency.Enabled =
            ChkMandatoryPurchaseCurrency.Checked = ChkEnablePurchaseCurrency.Checked;
    }

    private void chk_PGodown_CheckedChanged(object sender, EventArgs e)
    {
        ChkMandatoryPurchaseGodown.Enabled = ChkMandatoryPurchaseGodown.Checked = ChkEnablePurchaseGodown.Checked;
    }

    private void chk_PRemarks_CheckedChanged(object sender, EventArgs e)
    {
        ChkMandatoryPurchaseRemarks.Enabled =
            ChkMandatoryPurchaseRemarks.Checked = ChkEnablePurchaseRamarks.Checked;
    }

    private void chk_PUDepartment_CheckedChanged(object sender, EventArgs e)
    {
        ChkMandatoryPurchaseDepartment.Enabled =
            ChkMandatoryPurchaseDepartment.Checked = ChkEnablePurchaseDepartment.Checked;
    }

    private void chk_PDiscriptions_CheckedChanged(object sender, EventArgs e)
    {
    }

    #endregion --------------- Enable Disable Check box Validation For Purchase ---------------

    #region --------------- Enable Disable Check box Validation for Sales ---------------

    private void chk_SEnableOrder_CheckedChanged(object sender, EventArgs e)
    {
        ChkMandatorySalesOrder.Enabled = ChkMandatorySalesOrder.Checked = ChkEnableSalesOrder.Checked;
    }

    private void chk_SEnableChallan_CheckedChanged(object sender, EventArgs e)
    {
        ChkMandatorySalesChallan.Enabled = ChkMandatorySalesChallan.Checked = ChkEnableSalesChallan.Checked;
    }

    private void chk_SEnableSubLedger_CheckedChanged(object sender, EventArgs e)
    {
        ChkMandatorySalesSubLedger.Enabled = ChkMandatorySalesSubLedger.Checked = ChkEnableSalesSubLedger.Checked;
    }

    private void chk_SEnableAgent_CheckedChanged(object sender, EventArgs e)
    {
        ChkMandatorySalesAgent.Enabled = ChkMandatorySalesAgent.Checked = ChkEnableSalesAgent.Checked;
    }

    private void chk_SEnableCurrency_CheckedChanged(object sender, EventArgs e)
    {
        ChkMandatorySalesCurrency.Enabled = ChkMandatorySalesCurrency.Checked = ChkEnableSalesCurrency.Checked;
    }

    private void chk_SEnableGodwon_CheckedChanged(object sender, EventArgs e)
    {
        ChkMandatorySalesGodown.Enabled = ChkMandatorySalesGodown.Checked = ChkEnableSalesGodwon.Checked;
    }

    private void chk_SEnableRemarks_CheckedChanged(object sender, EventArgs e)
    {
        ChkMandatorySalesRemarks.Enabled = ChkMandatorySalesRemarks.Checked = ChkEnableSalesRemarks.Checked;
    }

    private void chk_SEnableDepartment_CheckedChanged(object sender, EventArgs e)
    {
        ChkMandatorySalesDepartment.Enabled =
            ChkMandatorySalesDepartment.Checked = ChkEnableSalesDepartment.Checked;
    }

    private void chk_SMandatoryOrder_CheckedChanged(object sender, EventArgs e)
    {
    }

    #endregion --------------- Enable Disable Check box Validation for Sales ---------------

    #region --------------- Enable Disable Check box Validation fro Inventory ---------------

    private void chk_ICostCenter_CheckedChanged(object sender, EventArgs e)
    {
        chk_MandatoryICostCenter.Enabled = chk_MandatoryICostCenter.Checked = chk_ICostCenter.Checked;
    }

    private void chk_ICostCenterItem_CheckedChanged(object sender, EventArgs e)
    {
        chk_MandatoryICostCenter.Enabled = chk_MandatoryICostCenter.Checked = chk_ICostCenterItem.Checked;
    }

    private void chk_IEnableGodwon_CheckedChanged(object sender, EventArgs e)
    {
        ChkMandatoryInventoryGodwon.Enabled = ChkMandatoryInventoryGodwon.Checked = chk_IEnableGodwon.Checked;
    }

    private void chk_IEnableGodwonItem_CheckedChanged(object sender, EventArgs e)
    {
        ChkMandatoryInventoryGodwon.Enabled = ChkMandatoryInventoryGodwon.Checked = chk_IEnableGodwonItem.Checked;
    }

    private void chk_IEnableRemarks_CheckedChanged(object sender, EventArgs e)
    {
        chk_MandatoryIRerarks.Enabled = chk_MandatoryIRerarks.Checked = chk_IEnableRemarks.Checked;
    }

    private void chk_IEnableDepartment_CheckedChanged(object sender, EventArgs e)
    {
        chk_MandatoryIDepartment.Enabled = chk_MandatoryIDepartment.Checked = chk_IEnableDepartment.Checked;
    }

    #endregion --------------- Enable Disable Check box Validation fro Inventory ---------------

    #region --------------- Event ---------------

    private void txtClosingCompany_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void BtnDOrderPrinter_Click(object sender, EventArgs e)
    {
        var frmPickList = new FrmAutoPopList(string.Empty, "Printer", ObjGlobal.SearchText, _ActionTag,
            string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable != null && FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
                TxtSalesOrderPrinter.Text = frmPickList.SelectedList[0]["Printer"].ToString().Trim();
            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"PRINTER ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtSalesOrderPrinter.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtSalesOrderPrinter.Focus();
    }

    private void BtnOrderNumbering_Click(object sender, EventArgs e)
    {
        var frmPickList = new FrmAutoPopList(string.Empty, "DOCUMENTNUMBERING", ObjGlobal.SearchText, _ActionTag,
            "SO", "LIST");
        if (FrmAutoPopList.GetListTable != null && FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
                TxtSalesOrderNumbering.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"DOCUMENT NUMBERING ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtSalesOrderNumbering.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtSalesOrderNumbering.Focus();
    }

    private void BtnDInvoiceDesign_Click(object sender, EventArgs e)
    {
        var frmPickList = new FrmAutoPopList(string.Empty, "DESIGN", ObjGlobal.SearchText, _ActionTag, "SB",
            "LIST");
        if (FrmAutoPopList.GetListTable != null && FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
                TxtSalesInvoiceDesign.Text = frmPickList.SelectedList[0]["Design"].ToString().Trim();
            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"DESIGN ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtSalesInvoiceDesign.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtSalesInvoiceDesign.Focus();
    }

    private void BtnOrderDesign_Click(object sender, EventArgs e)
    {
        var frmPickList = new FrmAutoPopList(string.Empty, "DESIGN", ObjGlobal.SearchText, _ActionTag, "SO",
            "LIST");
        if (FrmAutoPopList.GetListTable != null && FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
                TxtSalesOrderDesign.Text = frmPickList.SelectedList[0]["Design"].ToString().Trim();
            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"DESIGN ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtSalesOrderDesign.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtSalesOrderDesign.Focus();
    }

    private void BtnDInvoicePrinter_Click(object sender, EventArgs e)
    {
        var frmPickList = new FrmAutoPopList(string.Empty, "Printer", ObjGlobal.SearchText, _ActionTag,
            string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable != null && FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
                TxtSalesInvoicePrinter.Text = frmPickList.SelectedList[0]["Printer"].ToString().Trim();
            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"PRINTER ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtSalesInvoicePrinter.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtSalesInvoicePrinter.Focus();
    }

    private void BtnInvoiceNumbering_Click(object sender, EventArgs e)
    {
        var frmPickList = new FrmAutoPopList(string.Empty, "DOCUMENTNUMBERING", ObjGlobal.SearchText, _ActionTag,
            "SB", "LIST");
        if (FrmAutoPopList.GetListTable != null && FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
                TxtSalesInvoiceNumbering.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"DOCUMENT NUMBERING ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtSalesInvoiceNumbering.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtSalesInvoiceNumbering.Focus();
    }

    private void BtnABTPrinter_Click(object sender, EventArgs e)
    {
        var frmPickList = new FrmAutoPopList("MIN", "Design", ObjGlobal.SearchText, _ActionTag, "SB", "LIST");
        if (FrmAutoPopList.GetListTable != null && FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
                TxtSalesAvtPrintDesign.Text = frmPickList.SelectedList[0]["Design"].ToString().Trim();
            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"DESIGN ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtSalesAvtPrintDesign.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtSalesAvtPrintDesign.Focus();
    }

    private void btnBarcodeDesign_Click(object sender, EventArgs e)
    {
        var frmPickList = new FrmAutoPopList(string.Empty, "Design", _ActionTag, ObjGlobal.SearchText, "BC", "LIST");
        if (FrmAutoPopList.GetListTable != null && FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
                TxtBarcodeDesign.Text = frmPickList.SelectedList[0]["Design"].ToString().Trim();
            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"DESIGN ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtBarcodeDesign.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtBarcodeDesign.Focus();
    }

    private void btnBarcodePrinter_Click(object sender, EventArgs e)
    {
        var frmPickList = new FrmAutoPopList(string.Empty, "Printer", ObjGlobal.SearchText, _ActionTag,
            string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable != null && FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
                TxtBarcodePrinter.Text = frmPickList.SelectedList[0]["Printer"].ToString().Trim();
            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"PRINTER ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtBarcodePrinter.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtBarcodePrinter.Focus();
    }

    private void BtnDebtorsGroup_Click(object sender, EventArgs e)
    {
        using var frmPickList = new FrmAutoPopList("MIN", "ACCOUNTGROUP", ObjGlobal.SearchText, _ActionTag,
            string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
                if (!string.IsNullOrEmpty(_ActionTag) && _ActionTag != "SAVE")
                {
                    TxtDebtorsGroup.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                    DebtorsGroupId = ObjGlobal.ReturnInt(frmPickList.SelectedList[0]["LedgerId"].ToString().Trim());
                    TxtDebtorsGroup.ReadOnly = false;
                }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"ACCOUNT GROUP NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDebtorsGroup.Focus();
        }
    }

    private void BtnSalaryLedger_Click(object sender, EventArgs e)
    {
    }

    private void BtnTDSLedger_Click(object sender, EventArgs e)
    {
    }

    private void BtnPFLedger_Click(object sender, EventArgs e)
    {
    }

    private void TxtSalesOrderPrinter_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
            BtnDOrderPrinter_Click(sender, e);
        else if (e.KeyCode is Keys.Enter)
            TxtSalesOrderDesign.Focus();
        else
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtSalesOrderPrinter, BtnDOrderPrinter);
    }

    private void TxtSalesOrderDesign_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
            BtnOrderDesign_Click(sender, e);
        else if (e.KeyCode is Keys.Enter)
            TxtSalesOrderNumbering.Focus();
        else
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtSalesOrderDesign, BtnOrderDesign);
    }

    private void TxtSalesOrderNumbering_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
            BtnOrderNumbering_Click(sender, e);
        else if (e.KeyCode is Keys.Enter)
            TxtSalesAvtPrintDesign.Focus();
        else
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtSalesOrderNumbering, BtnOrderNumbering);
    }

    private void TxtSalesAvtPrintDesing_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
            BtnABTPrinter_Click(sender, e);
        else if (e.KeyCode is Keys.Enter)
            TxtBarcodeDesign.Focus();
        else
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtSalesAvtPrintDesign, BtnABTPrintDesign);
    }

    private void TxtBarcodeDesign_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
            btnBarcodePrinter_Click(sender, e);
        else if (e.KeyCode is Keys.Enter)
            TxtBarcodePrinter.Focus();
        else
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtBarcodeDesign, BtnBarcodeDesign);
    }

    private void TxtBarcodePrinter_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
            btnBarcodePrinter_Click(sender, e);
        else if (e.KeyCode is Keys.Enter)
            TxtSalesInvoicePrinter.Focus();
        else
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtBarcodePrinter, BtnBarcodePrinter);
    }

    private void TxtSalesInvoicePrinter_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
            BtnDInvoicePrinter_Click(sender, e);
        else if (e.KeyCode is Keys.Enter)
            TxtSalesInvoiceDesign.Focus();
        else
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtSalesInvoicePrinter, BtnDInvoicePrinter);
    }

    private void TxtSalesInvoiceDesign_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
            BtnDInvoiceDesign_Click(sender, e);
        else if (e.KeyCode is Keys.Enter)
            TxtSalesInvoiceNumbering.Focus();
        else
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtSalesInvoiceDesign, BtnDInvoiceDesign);
    }

    private void TxtSalesInvoiceNumbering_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
            BtnInvoiceNumbering_Click(sender, e);
        else if (e.KeyCode is Keys.Enter)
            TxtIrdAPI.Focus();
        else
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtSalesInvoiceNumbering, BtnInvoiceNumbering);
    }

    #endregion --------------- Event ---------------

    #region -------------- Global --------------

    public int SubledgerId = 0;
    public int SettingId;
    public int PT_ID = 0;
    public int DebtorsGroupId;
    public int CreditorsGroupId;

    public int? SalesDiscountTermId = 0;
    public int? SalesServiceChargeTermId = 0;
    public int? SalesProductWiseDiscountTermId;
    public int? SalesSpecialDiscountTermId = 0;
    public int? SalesCancelProductId;
    public int? SalesVatTermId;
    public int? PurchaseVatTermId;
    public int? PurchaseAddVatTermId = 0;
    public int? PurchaseDiscountTermId = 0;
    public int? PurchaseProductWiseDiscountTermId = 0;

    public long LedgerId = 0;
    public long CashLedgerId;
    public long VatLedgerId;
    public long BankLedgerId;
    public long ProfitLossLedgerId;
    public long SalaryLedgerId;
    public long PFLedgerId;
    public long TDSLedgerId;

    public long PurchaseLedgerId;
    public long PurchaseReturnLedgerId;
    public long invStockInHand;

    public long SalesLedgerId;
    public long SalesReturnLedgerId;

    public long OpeningStockLedgerId;
    public long ClosingStockPLLedgerId;
    public long ClosingStockBSLedgerId;

    private string Query = string.Empty;
    public string _ActionTag;

    private readonly ObjGlobal Gobj = new();
    private DataTable dtvalidate = new();

    #endregion -------------- Global --------------
}