using DatabaseModule.DataEntry.ProductionSystem.RawMaterialsIssue;
using System.Collections.Generic;
using System.Data;

namespace MrDAL.DataEntry.Interface.ProductionSystem.RawMaterialsIssue;

public interface IRawMaterialsIssueRepository
{
    // RETURN VALUE IN DATA TABLE
    DataSet GetBomVoucherDetails(string voucherNo);

    // INSERT UPDATE DELETE
    int SaveRawMaterialIssue(string actionTag);

    // OBJECT FOR THIS FORM
    string NumberSchema { get; set; }
    string Module { get; set; }
    StockIssue_Master IssueMaster { get; set; }
    List<StockIssue_Details> DetailsList { get; set; }
}