using DatabaseModule.Setup.LogSetting;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Control;
using MrDAL.Setup.Interface;
using MrDAL.Utility.Server;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrDAL.Setup.LogSetting;

public class SyncTableRepository : ISyncTableRepository
{
    public SyncTableRepository()
    {
        SyncTable = new SyncTable();
    }

    public int SaveSyncTable(string _actionTag)
    {
        var cmdString = new StringBuilder();
        string[] strNamesArray = { "DELETE", "REVERSE" };
        if (strNamesArray.Contains(_actionTag))
        {
        }

        if (_actionTag is "SAVE")
        {
            cmdString.Append(@" 
                INSERT INTO AMS.SyncTable(SyncId, IsBranch, IsGeneralLedger, IsTableId, IsArea, IsBillingTerm, IsAgent, IsProduct, IsCostCenter, IsMember, IsCashBank, IsJournalVoucher, IsNotesRegister, IsPDCVoucher, IsLedgerOpening, IsProductOpening, IsSalesQuotation, IsSalesOrder, IsSalesChallan, IsSalesInvoice, IsSalesReturn, IsSalesAdditional, IsPurchaseIndent, IsPurchaseOrder, IsPurchaseChallan, IsPurchaseInvoice, IsPurchaseReturn, IsPurchaseAdditional, IsStockAdjustment, SyncAPI, SyncOriginId, EnterBy, EnterDate, ApiKey) ");
            cmdString.Append("\n VALUES \n");
            cmdString.Append(
                $"{SyncTable.SyncId}, {SyncTable.IsBranch}, {SyncTable.IsGeneralLedger}, {SyncTable.IsTableId}, {SyncTable.IsArea}, {SyncTable},{SyncTable.IsBillingTerm}, {SyncTable.IsAgent}, {SyncTable.IsProduct}, {SyncTable.IsCostCenter}, {SyncTable.IsMember}, {SyncTable.IsCashBank}, {SyncTable.IsJournalVoucher}, {SyncTable.IsNotesRegister}, {SyncTable.IsPDCVoucher}, {SyncTable.IsLedgerOpening}, {SyncTable.IsProductOpening}, {SyncTable.IsSalesQuotation}, {SyncTable.IsSalesOrder}, {SyncTable.IsSalesChallan}, {SyncTable.IsSalesInvoice}, {SyncTable.IsSalesReturn}, {SyncTable.IsSalesAdditional}, {SyncTable.IsPurchaseIndent}, {SyncTable.IsPurchaseOrder}, {SyncTable.IsPurchaseChallan}, {SyncTable.IsPurchaseInvoice}, {SyncTable.IsPurchaseReturn}, {SyncTable.IsPurchaseAdditional}, {SyncTable.IsStockAdjustment}, {SyncTable.SyncAPI}, {SyncTable.SyncOriginId}, {SyncTable.EnterBy}, {SyncTable.EnterDate}, {SyncTable.ApiKey}");
            cmdString.Append($"{ObjSyncTable.SyncId},");
            cmdString.Append($"{ObjSyncTable.IsBranch},");
            cmdString.Append($"cast('{ObjSyncTable.IsGeneralLedger}' as bit),");
            cmdString.Append($"cast('{ObjSyncTable.IsTableId}' as bit),");
            cmdString.Append($"cast('{ObjSyncTable.IsArea},as bit),");
            cmdString.Append($"cast('{ObjSyncTable.IsBillingTerm},as bit),");
            cmdString.Append($"cast('{ObjSyncTable.IsAgent},as bit),");
            cmdString.Append($"cast('{ObjSyncTable.IsProduct},as bit),");
            cmdString.Append($"cast('{ObjSyncTable.IsCostCenter},as bit),");
            cmdString.Append($"cast('{ObjSyncTable.IsMember},as bit),");
            cmdString.Append($"cast('{ObjSyncTable.IsCashBank},as bit),");
            cmdString.Append($"cast('{ObjSyncTable.IsJournalVoucher},as bit),");
            cmdString.Append($"cast('{ObjSyncTable.IsNotesRegister},as bit),");
            cmdString.Append($"cast('{ObjSyncTable.IsPDCVoucher},as bit),");
            cmdString.Append($"cast('{ObjSyncTable.IsNotesRegister},as bit),");
            cmdString.Append($"cast('{ObjSyncTable.IsPDCVoucher},as bit),");
            cmdString.Append($"cast('{ObjSyncTable.IsLedgerOpening},as bit),");
            cmdString.Append($"cast('{ObjSyncTable.IsProductOpening},as bit),");
            cmdString.Append($"cast('{ObjSyncTable.IsSalesQuotation},as bit),");
            cmdString.Append($"cast('{ObjSyncTable.IsSalesOrder},as bit),");
            cmdString.Append($"cast('{ObjSyncTable.IsSalesChallan},as bit),");
            cmdString.Append($"cast('{ObjSyncTable.IsSalesInvoice},as bit)");
            cmdString.Append($"cast('{ObjSyncTable.IsSalesReturn},as bit)");
            cmdString.Append(ObjSyncTable.IsSalesAdditional);
            cmdString.Append(ObjSyncTable.IsSalesAdditional);
            cmdString.Append(ObjSyncTable.IsPurchaseIndent);
            cmdString.Append(ObjSyncTable.IsPurchaseOrder);
            cmdString.Append(ObjSyncTable.IsPurchaseChallan);
            cmdString.Append(ObjSyncTable.IsPurchaseInvoice);
            cmdString.Append(ObjSyncTable.IsPurchaseReturn);
            cmdString.Append(ObjSyncTable.IsPurchaseAdditional);
            cmdString.Append(ObjSyncTable.IsStockAdjustment);
            cmdString.Append(ObjSyncTable.SyncAPI.IsValueExits()
                ? $"N'{ObjSyncTable.SyncAPI.GetTrimReplace()}'," : "NULL");
            cmdString.Append(ObjSyncTable.SyncOriginId.IsValueExits()
                ? $"N'{ObjSyncTable.SyncOriginId.GetTrimReplace()}'," : "NULL");
            cmdString.Append(ObjSyncTable.EnterBy.IsValueExits()
                ? $"N'{ObjSyncTable.EnterBy.GetTrimReplace()}'," : "NULL");
            cmdString.Append(ObjSyncTable.ApiKey.IsValueExits()
                ? $"N'{ObjSyncTable.ApiKey.GetSystemDate()}'," : "NULL");

        }
        else if (_actionTag.Equals("UPDATE"))
        {
            cmdString.Append(
                $"UPDATE AWS.SyncTable SET Invoice_Date ='{SyncTable}");

        }

        var iResult = SaveDataInDatabase(cmdString);
        if (iResult <= 0)
        {
            return iResult;
        }

        return iResult;
    }

    private int SaveDataInDatabase(StringBuilder query)
    {
        return ExecuteValueInDatabase(query.ToString());
    }

    private int ExecuteValueInDatabase(string query)
    {
        try
        {
            var iRows = SqlExtensions.ExecuteNonTrans(query);
            return iRows;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            ex.DialogResult();
            return 0;
        }
    }

    public Task<int> SyncTableTask(string actionTag)
    {
        throw new NotImplementedException();
    }

    // OBJECT FOR THIS FORM

    public SyncTable SyncTable { get; set; }
    public SyncTable ObjSyncTable { get; set; }
}