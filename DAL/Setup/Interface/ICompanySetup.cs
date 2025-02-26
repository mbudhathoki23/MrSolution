using System.Collections.Generic;
using MrDAL.Utility.dbMaster;
using System.Data;
using DatabaseModule.Setup.CompanyMaster;
using CompanyRights = DatabaseModule.Setup.CompanyMaster.CompanyRights;

namespace MrDAL.Setup.Interface;

public interface ICompanySetup
{
    // METHOD FOR THIS FORM
    bool NewCreateDatabase(string databasePath, string initialCatalog, string actionTag);
        
    int SaveCompanyInfo(string actionTag);
    int SaveGlobalCompany(string actionTag);
    int SaveCompanyRights(string actionTag);
    void KillAllConnection(string dbName);

    int RunDateMiti();
    int MaxCompanyRightsId();
    void RunDefaultValue();


    // RETURN VALUE IN DATA TABLE
    DataTable GetCompanyInfo(int companyId);
    DataTable GetCompanyRightList(int userId);


    // OBJECT FOR THIS FORM
    CompanyInfo Info { get; set; }
    GlobalCompany Setup { get; set; }
    List<CompanyRights> RightsList { get; set; }
}