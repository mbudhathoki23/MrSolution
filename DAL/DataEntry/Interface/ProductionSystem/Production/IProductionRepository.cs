using DatabaseModule.DataEntry.ProductionSystem.Production;
using System.Collections.Generic;
using System.Data;

namespace MrDAL.DataEntry.Interface.ProductionSystem.Production;

public interface IProductionRepository
{
    // INSERT UPDATE DELETE
    int SaveProductionSetup(string actionTag);

    // RETURN VALUE IN DATA TABLE
    DataSet GetProductionVoucherDetails(string voucherNo);
    DataSet GetBomVoucherDetails(string voucherNo);

    // OBJECT FOR THIS FORM
    Production_Master VmProductionMaster { get; set; }
    List<Production_Details> DetailsList { get; set; }
}