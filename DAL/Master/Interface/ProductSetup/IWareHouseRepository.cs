using DatabaseModule.Master.ProductSetup;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.Master.Interface.ProductSetup;

public interface IWareHouseRepository
{
    // INSERT UPDATE DELETE
    int SaveGodown(string actionTag);
    Task<int> SyncGodownAsync(string actionTag);

    // RETURN VALUE IN DATA TABLE
    DataTable GetMasterGoDown(string actionTag, int status = 0, int selectedId = 0);

    // OBJECT FOR THIS FORM
    Godown ObjGodown { get; set; }
}
