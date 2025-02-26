using DatabaseModule.DataEntry.PurchaseMaster.PurchaseOrder;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.DataEntry.Interface.PurchaseMaster;

public interface IPurchaseOrderRepository
{
    // INSERT UPDATE DELETE
    int SavePurchaseOrder(string actionTag);
    Task<int> SyncPurchaseOrderAsync(string actionTag);

    // OBJECT FOR THIS FORM
    PO_Master PoMaster { get; set; }
    List<PO_Details> DetailsList { get; set; }
    List<PO_Term> Terms { get; set; }
}