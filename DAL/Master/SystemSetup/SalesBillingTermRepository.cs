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

public class SalesBillingTermRepository : ISalesBillingTermRepository
{
    public SalesBillingTermRepository()
    {
        StTerm = new ST_Term();
    }

    // UPDATE INSERT DELETE
    public int SaveSalesTerm(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag.ToUpper() == "SAVE")
        {
            cmdString.Append(
                " INSERT INTO AMS.ST_Term(ST_ID, Order_No, ST_Name, Module, ST_Type, ST_Basis, ST_Sign, ST_Condition, Ledger, ST_Rate, ST_Branch, ST_CompanyUnit, ST_Profitability, ST_Supess, ST_Status, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
            cmdString.Append($"\n VALUES \n");
            cmdString.Append($" ({StTerm.ST_ID},");
            cmdString.Append($" '{StTerm.Order_No}',");
            cmdString.Append($" '{StTerm.ST_Name}',");
            cmdString.Append($" '{StTerm.Module}',");
            cmdString.Append($" '{StTerm.ST_Type}',");
            cmdString.Append($" '{StTerm.ST_Basis}',");
            cmdString.Append($" '{StTerm.ST_Sign}',");
            cmdString.Append($" '{StTerm.ST_Condition}',");
            cmdString.Append(StTerm.Ledger > 0 ? $"' {StTerm.Ledger}'," : $"{ObjGlobal.SalesLedgerId},");
            cmdString.Append($" '{StTerm.ST_Rate}',");
            cmdString.Append($" '{StTerm.ST_Branch}',");
            cmdString.Append(StTerm.ST_CompanyUnit > 0 ? $" '{StTerm.ST_CompanyUnit}'," : "NULL,");
            cmdString.Append(StTerm.ST_Profitability ? " 1," : "0,");
            cmdString.Append(StTerm.ST_Supess ? " 1," : "0,");
            cmdString.Append(StTerm.ST_Status ? " 1," : "0,");
            cmdString.Append(
                $"'{StTerm.EnterBy}',GETDATE(),NULL,NULL,NULL,NULL,NULL,{StTerm.SyncRowVersion.GetDecimal(true)} );");
        }
        else if (actionTag.ToUpper() == "UPDATE")
        {
            cmdString.Append(" UPDATE AMS.ST_Term SET \n");
            cmdString.Append($"ST_Name =  '{StTerm.ST_Name}',");
            cmdString.Append($"Module =  '{StTerm.Module}',");
            cmdString.Append($"ST_Type = '{StTerm.ST_Type}',");
            cmdString.Append($"ST_Basis =  '{StTerm.ST_Basis}',");
            cmdString.Append($"ST_Sign = '{StTerm.ST_Sign}',");
            cmdString.Append($"ST_Condition = '{StTerm.ST_Condition}',");
            cmdString.Append(StTerm.Ledger > 0
                ? $"Ledger = '{StTerm.Ledger}',"
                : $"Ledger = {ObjGlobal.PurchaseLedgerId},");
            cmdString.Append($" ST_Rate = '{StTerm.ST_Rate}',");
            cmdString.Append(StTerm.ST_Profitability ? "ST_Profitability = 1," : "ST_Profitability = 0,");
            cmdString.Append(StTerm.ST_Supess ? " ST_Supess = 1," : "ST_Supess = 0,");
            cmdString.Append(StTerm.ST_Status ? "ST_Status = 1," : " ST_Status = 0,");
            cmdString.Append($"SyncRowVersion = {StTerm.SyncRowVersion.GetDecimal(true)} ");
            cmdString.Append($" WHERE ST_ID = {StTerm.ST_ID} ;");
        }
        else if (actionTag.ToUpper() == "DELETE")
        {
            cmdString.Append($"Delete from AMS.ST_Term where ST_ID = {StTerm.ST_ID} ");
        }

        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        return exe;
    }
    public string GetSalesTermScript(int Id = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.ST_Term s ";
        cmdString += Id > 0 ? $" WHERE s.SyncGlobalId IS NULL AND s.ST_ID= {Id} " : "";
        return cmdString;
    }
    // RETURN DATA IN DATA TABLE
    public DataTable GetMasterSalesTermList(string actionTag, string category, int selectedId = 0)
    {
        var cmdString = @"
			SELECT ST_ID, Order_No, ST.Module, CASE WHEN ST.Module = 'SB' THEN 'Sales Invoice' WHEN ST.Module = 'SR' THEN 'Sales Return' END AS ModuleDesc, ST_Name,ST.ST_Type, CASE WHEN ST.ST_Type = 'G' THEN 'General' WHEN ST.ST_Type = 'A' THEN 'Additional' WHEN ST.ST_Type = 'R' THEN 'RoundOff' END AS ST_TypeDesc, Gl.GLID, Gl.GLName,ST.ST_Basis, CASE WHEN ST.ST_Basis = 'V' THEN 'Value' WHEN ST.ST_Basis = 'Q' THEN 'Quantity' END AS ST_BasisDesc, ST.ST_Sign, st.ST_Condition,CASE WHEN ST.ST_Condition = 'B' THEN 'Bill Wise' WHEN ST.ST_Condition = 'P' THEN 'Product Wise' END AS ST_ConditionDesc,ST.ST_Rate, ST.ST_Profitability, ST.ST_Supess, ST.ST_Status
			FROM AMS.ST_Term ST
				 LEFT OUTER JOIN AMS.GeneralLedger AS Gl ON ST.Ledger = Gl.GLID ";
        cmdString += $" WHERE ST_ID = '{selectedId}' ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }
    public DataTable CheckIsValidData(string actionTag, string tableName, string whereValue, string validId, string inputTxt, string selectedId)
    {
        var cmdString = $@"Select * From AMS.{tableName} where {whereValue}='{inputTxt}'";
        cmdString += selectedId.GetLong() > 0 && actionTag != "SAVE" ? $" and {validId} <> {selectedId} " : "";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
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
    public async Task<bool> PullSalesBillingTermServerToClientByRowCounts(IDataSyncRepository<ST_Term> salesTermRepo, int callCount)
    {
        try
        {
            var pullResponse = await salesTermRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                return false;
            }

            var query = GetSalesTermScript();
            var alldata = SqlExtensions.ExecuteDataSetSql(query);

            foreach (var salesTermData in pullResponse.List)
            {
                StTerm = salesTermData;

                var alreadyExistData = alldata.Select("ST_ID=" + salesTermData.ST_ID + "");
                if (alreadyExistData.Length > 0)
                {
                    //get SyncRowVersion from client database table
                    int ClientSyncRowVersionId = 1;
                    ClientSyncRowVersionId = Convert.ToInt32(alreadyExistData[0]["SyncRowVersion"]);

                    //update only server SyncRowVersion is greater than client database while data pulling from server
                    if (salesTermData.SyncRowVersion > ClientSyncRowVersionId)
                    {
                        var result = SaveSalesTerm("UPDATE");
                    }
                }
                else
                {
                    var result = SaveSalesTerm("SAVE");
                }
            }


            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullSalesBillingTermServerToClientByRowCounts(salesTermRepo, callCount);
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    // OBJECT FOR THIS FORM
    public ST_Term StTerm { get; set; }
}