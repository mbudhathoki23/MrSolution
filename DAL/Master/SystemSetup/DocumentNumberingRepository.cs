using DatabaseModule.CloudSync;
using DatabaseModule.Setup.DocumentNumberings;
using MrDAL.Core.Extensions;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Master.Interface.SystemSetup;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MrDAL.Master.SystemSetup;

public class DocumentNumberingRepository : IDocumentNumberingRepository
{
    public DocumentNumberingRepository()
    {
        ObjDocumentNumbering = new DocumentNumbering();
    }

    // INSERT UPDATE DELETE
    public int SaveDocumentNumbering(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag.ToUpper() == "SAVE")
        {
            cmdString.Append("INSERT INTO  AMS.DocumentNumbering (DocId, DocModule, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocUser, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) \n");
            cmdString.Append($" Values({ObjDocumentNumbering.DocId},");
            cmdString.Append(ObjDocumentNumbering.DocModule.IsValueExits() ? $" '{ObjDocumentNumbering.DocModule.GetTrimReplace()}'," : "Null,");
            cmdString.Append(ObjDocumentNumbering.DocDesc.IsValueExits() ? $" '{ObjDocumentNumbering.DocDesc.GetTrimReplace()}'," : " NUll,");
            //cmdString.Append(ObjDocumentNumbering.DocStartDate != null ? $" '{Convert.ToDateTime(ObjDocumentNumbering.DocStartDate):yyyy-MM-dd}'," : "NUll,");
            cmdString.Append(ObjDocumentNumbering.DocStartDate != null ? "GETDATE()," : "NULL,");
            cmdString.Append(ObjDocumentNumbering.DocStartMiti.IsValueExits() ? $" '{ObjDocumentNumbering.DocStartMiti}'," : "NUll,");
            //cmdString.Append(ObjDocumentNumbering.DocEndDate != null ? $" '{Convert.ToDateTime(ObjDocumentNumbering.DocEndDate):yyyy-MM-dd}'," : "NUll,");
            cmdString.Append(ObjDocumentNumbering.DocEndDate != null ? "GETDATE()," : "NULL,");
            cmdString.Append(ObjDocumentNumbering.DocEndMiti.IsValueExits()
                ? $" '{ObjDocumentNumbering.DocEndMiti}',"
                : "NUll,");
            cmdString.Append(ObjDocumentNumbering.DocUser.IsValueExits() ? $" '{ObjDocumentNumbering.DocUser.GetTrimReplace()}'," : "Null,");

            cmdString.Append(ObjDocumentNumbering.DocType.IsValueExits()
                ? $" '{ObjDocumentNumbering.DocType}',"
                : "'AN',");
            cmdString.Append(ObjDocumentNumbering.DocPrefix.IsValueExits()
                ? $" '{ObjDocumentNumbering.DocPrefix.GetTrimReplace()}',"
                : "NUll,");
            cmdString.Append(ObjDocumentNumbering.DocSufix.IsValueExits()
                ? $" '{ObjDocumentNumbering.DocSufix.GetTrimReplace()}',"
                : "NUll,");
            cmdString.Append(ObjDocumentNumbering.DocBodyLength > 0
                ? $" '{ObjDocumentNumbering.DocBodyLength}',"
                : "0,");
            cmdString.Append(ObjDocumentNumbering.DocTotalLength > 0
                ? $" '{ObjDocumentNumbering.DocTotalLength}',"
                : "0,");
            cmdString.Append(ObjDocumentNumbering.DocBlank is true ? "1," : "0,");
            cmdString.Append(ObjDocumentNumbering.DocBlankCh.IsValueExits()
                ? $" '{ObjDocumentNumbering.DocBlankCh}',"
                : "0,");
            cmdString.Append(ObjDocumentNumbering.DocBranch.GetInt() > 0
                ? $" '{ObjDocumentNumbering.DocBranch}',"
                : $" '{ObjGlobal.SysBranchId}',");
            cmdString.Append(ObjDocumentNumbering.DocUnit.GetInt() > 0
                ? $" '{ObjDocumentNumbering.DocUnit}',"
                : "Null,");
            cmdString.Append(ObjDocumentNumbering.DocStart > 0
                ? $"  '{double.Parse(ObjDocumentNumbering.DocStart.ToString())}',"
                : "1,");
            cmdString.Append(ObjDocumentNumbering.DocCurr > 0
                ? $"  '{double.Parse(ObjDocumentNumbering.DocCurr.ToString())}',"
                : " 1, ");
            cmdString.Append(ObjDocumentNumbering.DocEnd > 0
                ? $"  '{double.Parse(ObjDocumentNumbering.DocEnd.ToString())}',"
                : "9999,");
            cmdString.Append(ObjDocumentNumbering.DocDesign.IsValueExits()
                ? $" '{ObjDocumentNumbering.DocDesign}',"
                : "Null,");
            cmdString.Append(ObjDocumentNumbering.Status is true ? "1," : "0,");
            cmdString.Append($" '{ObjGlobal.LogInUser}', GETDATE(),{ObjGlobal.SysFiscalYearId} , ");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync
                ? ObjGlobal.LocalOriginId.HasValue ? $" '{ObjGlobal.LocalOriginId}'," : "NULL,"
                : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
            cmdString.Append($"{ObjDocumentNumbering.SyncRowVersion.GetDecimal(true)} , ");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID());" : "NULL);");
        }
        else if (actionTag.ToUpper() == "UPDATE")
        {
            cmdString.Append(" Update AMS.DocumentNumbering SET ");
            cmdString.Append($" DocUser='{ObjDocumentNumbering.DocUser}',");
            cmdString.Append($" DocDesc='{ObjDocumentNumbering.DocDesc}',");
            cmdString.Append($" DocStartMiti='{ObjDocumentNumbering.DocStartMiti}',");
            cmdString.Append(
                $" DocStartDate='{Convert.ToDateTime(ObjDocumentNumbering.DocStartDate):yyyy-MM-dd}',");
            cmdString.Append($" DocEndMiti='{ObjDocumentNumbering.DocEndMiti}',");
            cmdString.Append($" DocEndDate='{Convert.ToDateTime(ObjDocumentNumbering.DocEndDate):yyyy-MM-dd}',");
            cmdString.Append($" DocPrefix='{ObjDocumentNumbering.DocPrefix}',");
            cmdString.Append($" DocSufix='{ObjDocumentNumbering.DocSufix}',");
            cmdString.Append($" DocBodyLength='{ObjDocumentNumbering.DocBodyLength}',");
            cmdString.Append(
                $" DocTotalLength='{ObjGlobal.ReturnDouble(ObjDocumentNumbering.DocTotalLength.ToString())}',");
            cmdString.Append(ObjDocumentNumbering.DocType.IsValueExits()
                ? $" DocType = '{ObjDocumentNumbering.DocType}',"
                : " DocType = 'AN',");
            cmdString.Append(ObjDocumentNumbering.DocBranch.GetInt() > 0
                ? $"DocBranch=  '{ObjDocumentNumbering.DocBranch}',"
                : $" DocBranch= '{ObjGlobal.SysBranchId}',");
            cmdString.Append(ObjDocumentNumbering.DocUnit.GetInt() > 0
                ? $" DocUnit = '{ObjDocumentNumbering.DocUnit}',"
                : "DocUnit = Null,");
            cmdString.Append($" DocStart='{ObjDocumentNumbering.DocStart.GetDecimal(true)}',");
            cmdString.Append($" DocCurr='{ObjDocumentNumbering.DocCurr.GetDecimal(true)}',");
            cmdString.Append($" DocEnd='{ObjDocumentNumbering.DocEnd.GetDecimal()}',");
            cmdString.Append(ObjDocumentNumbering.DocDesign.IsValueExits()
                ? $" DocDesign='{ObjDocumentNumbering.DocDesign}',"
                : "DocDesign= Null, ");
            cmdString.Append(ObjDocumentNumbering.Status is true ? "Status = 1," : "Status = 0,");
            cmdString.Append(ObjGlobal.IsOnlineSync
                ? "SyncLastPatchedOn = GETDATE(),"
                : "SyncLastPatchedOn =NULL,");
            cmdString.Append($"SyncRowVersion = {ObjDocumentNumbering.SyncRowVersion.GetDecimal(true)}");
            cmdString.Append($"WHERE DocId =  {ObjDocumentNumbering.DocId} ");
        }
        else if (actionTag.ToUpper() == "DELETE")
        {
            cmdString.Append($"DELETE FROM AMS.DocumentNumbering where DocId=  {ObjDocumentNumbering.DocId} ");
        }

        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() => SyncDocumentNumberingAsync(actionTag));
        }
        return exe;
    }

    public async Task<int> SyncDocumentNumberingAsync(string actionTag)
    {
        //sync
        try
        {
            _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!_configParams.Success || _configParams.Model.Item1 == null)
            {
                return 1;
            }
            var apiConfig = new SyncApiConfig
            {
                BaseUrl = _configParams.Model.Item2,
                Apikey = _configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,
                GetUrl = @$"{_configParams.Model.Item2}SalesOrder/GetDocumentNumberingByCallCount",
                InsertUrl = @$"{_configParams.Model.Item2}SalesOrder/InsertDocumentNumberingList",
                UpdateUrl = @$"{_configParams.Model.Item2}SalesOrder/UpdateDocumentNumbering",

            };

            DataSyncHelper.SetConfig(apiConfig);
            _injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(_injectData);
            var departmentRepo = DataSyncProviderFactory.GetRepository<DocumentNumbering>(_injectData);
            var departments = new List<DocumentNumbering>
            {
                ObjDocumentNumbering
            };
            // push realtime DocumentNumbering details to server
            await departmentRepo.PushNewListAsync(departments);

            return departmentRepo.GetHashCode();

        }
        catch (Exception ex)
        {
            return 0;
        }
    }
    public async Task<bool> PullDocumentNumberingServerToClientByRowCount(IDataSyncRepository<DocumentNumbering> membershipSetupRepo, int callCount)
    {
        try
        {
            var pullResponse = await membershipSetupRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                return false;
            }

            var query = GetDocumentNumberingScript();
            var alldata = SqlExtensions.ExecuteDataSetSql(query);

            foreach (var documentNumberingData in pullResponse.List)
            {
                ObjDocumentNumbering = documentNumberingData;

                var alreadyExistData = alldata.Select("DocId= " + documentNumberingData.DocId + "");
                if (alreadyExistData.Length > 0)
                {
                    //get SyncRowVersion from client database table
                    int ClientSyncRowVersionId = 1;
                    ClientSyncRowVersionId = Convert.ToInt32(alreadyExistData[0]["SyncRowVersion"]);

                    //update only server SyncRowVersion is greater than client database while data pulling from server
                    if (documentNumberingData.SyncRowVersion > ClientSyncRowVersionId)
                    {
                        var result = SaveDocumentNumbering("UPDATE");
                    }
                }
                else
                {
                    var result = SaveDocumentNumbering("SAVE");
                }
            }


            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullDocumentNumberingServerToClientByRowCount(membershipSetupRepo, callCount);
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    public string GetDocumentNumberingScript(int docId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.DocumentNumbering";
        cmdString += docId > 0 ? $" WHERE SyncGlobalId IS NULL AND MShipId= {docId} " : "";
        return cmdString;
    }
    // RETURN VALUE IN DATA TABLE
    public DataTable GetMasterDocumentNumbering(string actionTag, string module, int selectedId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.DocumentNumbering WHERE DocId = '{selectedId}'";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    // OBJECT FOR THIS FORM
    public DocumentNumbering ObjDocumentNumbering { get; set; }
    private DbSyncRepoInjectData _injectData;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
}