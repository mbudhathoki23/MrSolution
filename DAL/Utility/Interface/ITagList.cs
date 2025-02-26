using DatabaseModule.Setup.SecurityRights;
using System.Data;

namespace MrDAL.Utility.Interface;

public interface ITagList
{
    TagList TagList { get; set; }

    DataTable GetAccountGroupList();

    DataTable GetAccountSubGroupList(string groupId);

    DataTable GetAllGeneralLedgerList(string category, string groupId, string subGroupId, string module);

    DataTable GetSubLedgerList(string ledgerId, string module);

    DataTable GetEntryBranchList();

    DataTable GetEntryCompanyUnitList();

    DataTable GetEntryFiscalYearList();

    DataTable GetProductGroupList();

    DataTable GetProductSubGroupList(string groupId);

    DataTable GetProductList(string groupId, string subGroupId, string module);

    DataTable GetEntryUserInfo(string module);
}