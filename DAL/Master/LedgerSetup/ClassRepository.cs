using MrDAL.Core.Extensions;
using MrDAL.Master.Interface.LedgerSetup;
using MrDAL.Utility.Server;
using System.Data;

namespace MrDAL.Master.LedgerSetup;

public class ClassRepository : IClassRepository
{
    public ClassRepository()
    {

    }

    public DataTable CheckIsValidData(string actionTag, string tableName, string whereValue, string validId, string inputTxt, string selectedId)
    {
        var cmdString = $@"Select * From AMS.{tableName} where {whereValue}='{inputTxt}'";
        cmdString += selectedId.GetLong() > 0 && actionTag != "SAVE" ? $" and {validId} <> {selectedId} " : "";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }
}