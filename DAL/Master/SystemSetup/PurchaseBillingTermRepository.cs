using DatabaseModule.Setup.TermSetup;
using MrDAL.Core.Extensions;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Global.Common;
using MrDAL.Master.Interface.SystemSetup;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MrDAL.Master.SystemSetup;

public class PurchaseBillingTermRepository : IPurchaseBillingTermRepository
{
    public PurchaseBillingTermRepository()
    {
        PtTerm = new PT_Term();
    }

    // INSERT UPDATE DELETE
    public int SavePurchaseTerm(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag.ToUpper() == "SAVE")
        {
            cmdString.Append(@" 
                INSERT INTO AMS.PT_Term(PT_Id, Order_No, PT_Name, Module, PT_Type, PT_Basis, PT_Sign, PT_Condition, Ledger, PT_Rate, PT_Branch, PT_CompanyUnit, PT_Profitability, PT_Supess, PT_Status, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)");
            cmdString.Append($"\n Values({PtTerm.PT_Id}, \n");
            cmdString.Append($" '{PtTerm.Order_No}',");
            cmdString.Append($" '{PtTerm.PT_Name}',");
            cmdString.Append($" '{PtTerm.Module}',");
            cmdString.Append($" '{PtTerm.PT_Type}',");
            cmdString.Append($" '{PtTerm.PT_Basis}',");
            cmdString.Append($" '{PtTerm.PT_Sign}',");
            cmdString.Append($" '{PtTerm.PT_Condition}',");
            cmdString.Append(PtTerm.Ledger > 0 ? $"{PtTerm.Ledger}," : $"{ObjGlobal.PurchaseLedgerId},");
            cmdString.Append($" '{PtTerm.PT_Rate}',");
            cmdString.Append(PtTerm.PT_Branch > 0 ? $" '{PtTerm.PT_Branch}'," : $"{ObjGlobal.SysBranchId},");
            cmdString.Append(PtTerm.PT_CompanyUnit > 0 ? $" '{PtTerm.PT_CompanyUnit}'," : "NULL,");
            cmdString.Append(PtTerm.PT_Profitability ? " 1," : "0,");
            cmdString.Append(PtTerm.PT_Supess ? " 1," : "0,");
            cmdString.Append(PtTerm.PT_Status ? " 1," : "0,");
            cmdString.Append($"'{PtTerm.EnterBy}',GETDATE(),");
            cmdString.Append($" NULL,NULL,NULL,NULL,NULL,{PtTerm.SyncRowVersion.GetDecimal(true)} );");
        }
        else if (actionTag.ToUpper() == "UPDATE")
        {
            cmdString.Append(" UPDATE AMS.PT_Term SET \n");
            cmdString.Append($"PT_Name =  '{PtTerm.PT_Name}',");
            cmdString.Append($"Module =  '{PtTerm.Module}',");
            cmdString.Append($"PT_Type = '{PtTerm.PT_Type}',");
            cmdString.Append($"PT_Basis =  '{PtTerm.PT_Basis}',");
            cmdString.Append($" PT_Sign = '{PtTerm.PT_Sign}',");
            cmdString.Append($" PT_Condition = '{PtTerm.PT_Condition}',");
            cmdString.Append(PtTerm.Ledger > 0
                ? $"Ledger = {PtTerm.Ledger},"
                : $"Ledger = {ObjGlobal.PurchaseLedgerId},");
            cmdString.Append($" PT_Rate = '{PtTerm.PT_Rate}',");
            cmdString.Append(PtTerm.PT_Profitability ? "PT_Profitability = 1," : "PT_Profitability = 0,");
            cmdString.Append(PtTerm.PT_Supess ? " PT_Supess = 1," : "PT_Supess = 0,");
            cmdString.Append(PtTerm.PT_Status ? "PT_Status = 1," : " PT_Status = 0,");
            cmdString.Append($"SyncRowVersion = {PtTerm.SyncRowVersion.GetDecimal(true)} ");
            cmdString.Append($" WHERE PT_ID = {PtTerm.PT_Id}");
        }
        else if (actionTag.ToUpper() == "DELETE")
        {
            cmdString.Append($"Delete from AMS.PT_Term where PT_ID = {PtTerm.PT_Id} ");
        }

        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        return exe;
    }

    // RETURN VALUE IN INT
    public bool IsBillingTermUsedOrNot(string module, int termId)
    {
        var cmdString = module switch
        {
            "SB" => @$"
						SELECT * FROM
						(
							SELECT DISTINCT st.ST_Id FROM AMS.SQ_Term st WHERE st.ST_Id={termId}
							UNION ALL
							SELECT  DISTINCT st.ST_Id FROM AMS.SO_Term st WHERE st.ST_Id ={termId}
							UNION ALL
							SELECT DISTINCT st.ST_Id FROM AMS.SC_Term st WHERE st.ST_Id ={termId}
							UNION ALL
							SELECT DISTINCT st.ST_Id FROM AMS.SB_Term st WHERE st.ST_Id ={termId}
							UNION ALL
							SELECT DISTINCT st.ST_Id FROM AMS.SR_Term st WHERE st.ST_Id ={termId}
						) SalesTerm GROUP BY SalesTerm.ST_Id ",
            "PB" => @$"
						SELECT * FROM (
						SELECT  DISTINCT st.PT_Id FROM AMS.PO_Term st WHERE st.PT_Id ={termId}
						UNION ALL
						SELECT DISTINCT st.PT_Id FROM AMS.PC_Term st WHERE st.PT_Id ={termId}
						UNION ALL
						SELECT DISTINCT st.PT_Id FROM AMS.PB_Term st WHERE st.PT_Id ={termId}
						UNION ALL
						SELECT DISTINCT st.PT_Id FROM AMS.PR_Term st WHERE st.PT_Id ={termId}
						UNION ALL
						SELECT DISTINCT st.PT_Id FROM AMS.PAB_Details  st WHERE st.PT_Id ={termId}
						) PurchaseTerm GROUP BY PurchaseTerm.PT_Id",
            _ => string.Empty
        };
        var dtCheck = cmdString.IsValueExits() ? SqlExtensions.ExecuteDataSet(cmdString).Tables[0] : new DataTable();
        return dtCheck.Rows.Count > 0;
    }
    public string GetLedgerDescription(long ledgerId)
    {
        var cmdString = $"Select GLName from AMS.GeneralLedger where GLid = '{ledgerId}'";
        var dtLedger = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return dtLedger.Rows.Count <= 0 ? string.Empty : dtLedger.Rows[0]["GLName"].ToString();
    }
    public int ReturnIntValueFromTable(string tableName, string tableId, string tableColumn, string filterTxt)
    {
        var cmdString = $"SELECT  {tableId} SelectedId From {tableName} where {tableColumn}='{filterTxt}'";
        var dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return dt.Rows.Count > 0 ? dt.Rows[0]["SelectedId"].GetInt() : 0;
    }
    public string GetPurchaseTermScript(int Id = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.PT_Term p ";
        cmdString += Id > 0 ? $" WHERE p.SyncGlobalId IS NULL AND p.PT_Id= {Id} " : "";
        return cmdString;
    }
    // RETURN DATA IN DATA TABLE
    public DataTable GetMasterPurchaseTermList(string actionTag, string category, bool status = false, int selectedId = 0)
    {
        var cmdString = $@"
			SELECT PT_ID, PT_Name, Order_No,PT.Module , CASE WHEN PT.Module = 'PB' THEN 'Purchase Invoice' WHEN PT.Module = 'PR' THEN 'Purchase Return' END AS ModuleDesc, GLID, GLName,PT_Type, CASE WHEN PT_Type = 'G' THEN 'General' WHEN PT_Type ='A' THEN 'Additional' WHEN PT_Type ='R' THEN 'RoundOff' END AS PT_TypeDesc,PT_Condition, CASE WHEN PT_Condition = 'B' THEN 'Bill Wise' WHEN PT_Condition = 'P' THEN 'Product Wise' END AS PT_ConditionDesc, PT_Sign,PT_Basis, CASE WHEN PT_Basis = 'V' THEN 'Value' WHEN PT_Basis = 'Q' THEN 'Quantity' END AS PT_BasisValue, PT_Rate, PT_Profitability, PT_Supess, PT_Status
			FROM AMS.PT_Term PT
				 LEFT OUTER JOIN AMS.GeneralLedger GL ON PT.Ledger = GL.GLID
			WHERE PT_ID = '{selectedId}'";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }
    public DataTable CheckIsValidData(string actionTag, string tableName, string whereValue, string validId, string inputTxt, string selectedId)
    {
        var cmdString = $@"Select * From AMS.{tableName} where {whereValue}='{inputTxt}'";
        cmdString += selectedId.GetLong() > 0 && actionTag != "SAVE" ? $" and {validId} <> {selectedId} " : "";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }
    public async Task<bool> PullSalesBillingTermServerToClientByRowCounts(IDataSyncRepository<PT_Term> purchaseTermRepo, int callCount)
    {
        try
        {
            var pullResponse = await purchaseTermRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                return false;
            }

            var query = GetPurchaseTermScript();
            var alldata = SqlExtensions.ExecuteDataSetSql(query);

            foreach (var ptTerm in pullResponse.List)
            {
                PtTerm = ptTerm;

                var alreadyExistData = alldata.Select("PT_Id=" + ptTerm.PT_Id + "");
                if (alreadyExistData.Length > 0)
                {
                    //get SyncRowVersion from client database table
                    int ClientSyncRowVersionId = 1;
                    ClientSyncRowVersionId = Convert.ToInt32(alreadyExistData[0]["SyncRowVersion"]);

                    //update only server SyncRowVersion is greater than client database while data pulling from server
                    if (ptTerm.SyncRowVersion > ClientSyncRowVersionId)
                    {
                        var result = SavePurchaseTerm("UPDATE");
                    }
                }
                else
                {
                    var result = SavePurchaseTerm("SAVE");
                }
            }


            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullSalesBillingTermServerToClientByRowCounts(purchaseTermRepo, callCount);
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    // OBJECT FOR THIS FORM
    public PT_Term PtTerm { get; set; }
}