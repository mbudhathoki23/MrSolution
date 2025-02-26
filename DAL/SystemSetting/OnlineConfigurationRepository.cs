using DatabaseModule.Setup.LogSetting;
using MrDAL.Global.Common;
using MrDAL.SystemSetting.SystemInterface;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Text;

namespace MrDAL.SystemSetting;

public class OnlineConfigurationRepository : IOnlineConfigurationRepository
{
    public OnlineConfigurationRepository()
    {
        ObjSync = new SyncTable();
    }
    public int SaveOnlineSyncConfig(string actionTag)
    {
        var cmdString = new StringBuilder();
        cmdString.Append(" DELETE FROM AMS.SyncTable; \n");
        cmdString.Append(" INSERT INTO AMS.SyncTable (SyncId,IsBranch,IsGeneralLedger,IsTableId,IsArea,IsBillingTerm,IsAgent,IsProduct,IsCostCenter,IsMember,IsCashBank,IsJournalVoucher,IsNotesRegister,IsPDCVoucher,IsLedgerOpening,IsProductOpening,IsSalesQuotation,IsSalesOrder,IsSalesChallan,IsSalesInvoice,IsSalesReturn,IsSalesAdditional,IsPurchaseIndent,IsPurchaseOrder,IsPurchaseChallan,IsPurchaseInvoice,IsPurchaseReturn,IsPurchaseAdditional,IsStockAdjustment,SyncAPI,SyncOriginId,EnterBy,EnterDate,ApiKey) ");
        cmdString.Append("\n VALUES \n");
        cmdString.Append($" ({ObjSync.SyncId},CAST('{ObjSync.IsBranch}' AS BIT),CAST('{ObjSync.IsGeneralLedger}' AS BIT),");
        cmdString.Append($"CAST('{ObjSync.IsTableId}' AS BIT), CAST('{ObjSync.IsArea}' AS BIT), CAST('{ObjSync.IsBillingTerm}' AS BIT), CAST('{ObjSync.IsAgent}' AS BIT),");
        cmdString.Append($"CAST('{ObjSync.IsProduct}' AS BIT), CAST('{ObjSync.IsCostCenter}' AS BIT), CAST('{ObjSync.IsMember}' AS BIT), CAST('{ObjSync.IsCashBank}' AS BIT),");
        cmdString.Append($"CAST('{ObjSync.IsJournalVoucher}' AS BIT), CAST('{ObjSync.IsNotesRegister}'AS BIT), CAST('{ObjSync.IsPDCVoucher}' AS BIT),");
        cmdString.Append($" CAST('{ObjSync.IsLedgerOpening}' AS BIT), CAST('{ObjSync.IsProductOpening}' AS BIT), CAST('{ObjSync.IsSalesQuotation}' AS BIT),");
        cmdString.Append($"CAST('{ObjSync.IsSalesOrder}' AS BIT), CAST('{ObjSync.IsSalesChallan}' AS BIT), CAST('{ObjSync.IsSalesInvoice}' AS BIT),");
        cmdString.Append($"CAST('{ObjSync.IsSalesReturn}' AS BIT), CAST('{ObjSync.IsSalesAdditional}' AS BIT), CAST('{ObjSync.IsPurchaseIndent}' AS BIT),");
        cmdString.Append($"CAST('{ObjSync.IsPurchaseOrder}' AS BIT), CAST('{ObjSync.IsPurchaseChallan}' AS BIT), CAST('{ObjSync.IsPurchaseInvoice}' AS BIT),");
        cmdString.Append($"CAST('{ObjSync.IsPurchaseReturn}' AS BIT), CAST('{ObjSync.IsPurchaseAdditional}' AS BIT), CAST('{ObjSync.IsStockAdjustment}' AS BIT),");
        cmdString.Append($"N'{ObjSync.SyncAPI}', N'{ObjSync.SyncOriginId}', '{ObjGlobal.LogInUser}', GETDATE(), ");
        cmdString.Append(ObjSync.ApiKey != null ? $"'{ObjSync.ApiKey}'); " : "NULL );");
        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (exe == 0)
        {
            return exe;
        }

        try
        {
            var query = $@"
			    UPDATE MASTER.AMS.GlobalCompany SET DataSyncApiBaseUrl = '{ObjSync.SyncAPI}',ApiKey = '{ObjSync.ApiKey}' 
                WHERE GComp_Id='{ObjGlobal.CompanyId}'; ";

            var result = SqlExtensions.ExecuteNonQuery(query);

            if (string.IsNullOrEmpty(ObjSync.SyncAPI) && ObjSync.ApiKey == null)
            {
                query = $" UPDATE AMS.CompanyInfo SET IsSyncOnline=0";
            }
            else
            {
                query = $" UPDATE AMS.CompanyInfo SET IsSyncOnline=1,ApiKey='{ObjSync.ApiKey}'";
            }
            result = SqlExtensions.ExecuteNonQuery(GetConnection.ConnectionStringMaster, CommandType.Text, query);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

        return exe;
    }

    // OBJECT
    public SyncTable ObjSync { get; set; }
}