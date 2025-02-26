using System.Data;

namespace MrDAL.Master.Interface.LedgerSetup;

public interface IClassRepository
{
    DataTable CheckIsValidData(string actionTag, string tableName, string whereValue, string validId, string inputTxt, string selectedId);
}