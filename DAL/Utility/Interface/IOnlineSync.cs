using DatabaseModule.Setup.LogSetting;
using System.Data;

namespace MrDAL.Utility.Interface;

public interface IOnlineSync
{
    // OBJECT FOR THIS FORM
    #region -------------- OBJECT FOR THIS FORM --------------
    ImportLog ImportLog { get; set; }
    #endregion



    // RETURN VALUE IN INT
    #region --------------- RETURN VALUE IN INT ---------------

    int SaveServerInfo();

    int ResetTransaction(string value = "", string dbSelect = "", bool auditStart = true);

    int ResetMaster(string value = "", string dbSelect = "", bool auditStart = true);
    int ImportMasterFromLocal(string value, string server = "", string dbSelect = "");
    int UpdateMaster(string value, string server = "", string dbSelect = "");
    int ImportTransactionFromLocal(string value, string server = "", string dbSelect = "");
    int UpdateTransaction(string value, string server = "", string dbSelect = "");
    #endregion --------------- RETURN VALUE IN INT ---------------



    // RETURN VALUE IN DATA TABLE
    #region --------------- RETURN VALIE IN DATATABLE ---------------

    DataTable GetCompanyList(string source, string userId, string userName);

    DataTable GetServerInfo(string source);

    #endregion --------------- RETURN VALUE IN DATATABLE ---------------
}