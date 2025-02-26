using DatabaseModule.DataEntry.ProductionSystem.BillOfMaterials;
using System.Collections.Generic;
using System.Data;

namespace MrDAL.DataEntry.Interface.ProductionSystem.BillOfMaterials;

public interface IBillOfMaterialsRepository
{
    // RETURN VALUE IN DATA TABLE
    DataTable CheckVoucherNo(string tableName, string tableVoucherNo, string inputVoucherNo);
    DataSet GetBomVoucherDetails(string voucherNo);

    // INSERT UPDATE DELETE
    int SaveBillOfMaterialsSetup(string actionTag);

    // OBJECT FOR THIS FORM
    BOM_Master VmBomMaster { get; set; }
    string NumberSchema { get; set; }
    List<BOM_Details> DetailsList { get; set; }
    string Module { get; set; }

}