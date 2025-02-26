using DatabaseModule.DataEntry.FinanceTransaction.DayClosing;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Interface.FinanceTransaction.DayClosing;
using MrDAL.Global.Common;
using MrDAL.Utility.Server;
using System.Data;

namespace MrDAL.DataEntry.FinanceTransaction.DayClosing;

public class CashClosingRepository : ICashClosingRepository
{
    public CashClosingRepository()
    {
        ObjCashClosing = new CashClosing();
    }

    // INSERT UPDATE DELETE

    private int SaveCashClosingAuditLog(string actionTag)
    {
        var cmdString = $@"
            INSERT INTO AUD.AUDIT_CASH_CLOSING(EnterMiti, EnterTime, CB_Balance, Cash_Sales, Cash_Purchase, TotalCash, ThauQty, ThouVal, FivHunQty, FivHunVal, HunQty, HunVal, FiFtyQty, FiftyVal, TwenteyFiveQty, TwentyFiveVal, TwentyQty, TwentyVal, TenQty, TenVal, FiveQty, FiveVal, TwoQty, TwoVal, OneQty, OneVal, Cash_Diff, Module, HandOverUser, Remarks, EnterBy, EnterDate, ModifyAction, ModifyBy, ModifyDate)
            SELECT EnterMiti, EnterTime, CB_Balance, Cash_Sales, Cash_Purchase, TotalCash, ThauQty, ThouVal, FivHunQty, FivHunVal, HunQty, HunVal, FiFtyQty, FiftyVal, TwenteyFiveQty, TwentyFiveVal, TwentyQty, TwentyVal, TenQty, TenVal, FiveQty, FiveVal, TwoQty, TwoVal, OneQty, OneVal, Cash_Diff, Module, HandOverUser, Remarks, EnterBy, EnterDate,'{actionTag}' ModifyAction,'{ObjGlobal.LogInUser}' ModifyBy,GETDATE() ModifyDate
            FROM AMS.CashClosing
            WHERE CC_Id='{ObjCashClosing.CC_Id}';";
        var result = SqlExtensions.ExecuteNonQuery(cmdString);
        return result;
    }


    //RETURN VALUE OF PRODUCT IN DATA TABLE

    public DataTable IsExitsCheckDocumentNumbering(string module)
    {
        var cmdString = $@"
        Select DocDesc from AMS.DocumentNumbering where DocModule = '{module}' and (FiscalYearId = '{ObjGlobal.SysFiscalYearId}' or FiscalYearId is NUll ) and (DocBranch = '{ObjGlobal.SysBranchId}' OR DocBranch IS NULL) and (DocUnit ='{ObjGlobal.SysCompanyUnitId}' or DocUnit is NUll);";

        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }


    // RETURN VALUE OF MASTER LIST IN DATA TABLE

    public long GetUserLedgerIdFromUser(string userInfo)
    {
        var cmdString =
            $"SELECT GLID FROM AMS.GeneralLedger WHERE GLID IN (SELECT Ledger_Id FROM master.AMS.UserInfo WHERE User_Name='{userInfo}')";
        var dtLedger = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return dtLedger.Rows.Count <= 0 ? 0 : dtLedger.Rows[0]["GLID"].GetLong();
    }


    // OBJECT FOR THIS FORM
    public CashClosing ObjCashClosing { get; set; }
}