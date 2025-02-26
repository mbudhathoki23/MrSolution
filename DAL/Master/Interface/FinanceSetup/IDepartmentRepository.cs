using DatabaseModule.Master.FinanceSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.Master.Interface.FinanceSetup;

public interface IDepartmentRepository
{
    // INSERT UPDATE DELETE
    int SaveDepartment(string actionTag);
    Task<int> SyncDepartmentAsync(string actionTag);
    Task<bool> SyncDepartmentDetailsAsync();
    Task<int> SyncUpdateDepartment(int dId);


    //PULL DEPARTMENT
    Task<bool> PullDepartmentsServerToClientByRowCounts(IDataSyncRepository<Department> departmentRepo, int callCount);


    // RETURN VALUE IN DATA TABLE
    DataTable CheckIsValidData(string actionTag, string tableName, string whereValue, string validId, string inputTxt, string selectedId);

    DataTable GetMasterDepartment(string actionTag, int selectedId = 0);
    public string GetDepartmentScript(int DId = 0);




    // OBJECT FOR THIS FORM
    Department ObjDepartment { get; set; }
}



