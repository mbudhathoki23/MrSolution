namespace DatabaseModule.DataEntry.PurchaseMaster.PurchaseChallan;

public class PC_Term
{
    public string PC_VNo { get; set; }
    public int PT_Id { get; set; }
    public int SNo { get; set; }
    public string Term_Type { get; set; }
    public long? Product_Id { get; set; }
    public decimal Rate { get; set; }
    public decimal Amount { get; set; }
    public string Taxable { get; set; }
    public System.Guid SyncBaseId { get; set; }
    public System.Guid SyncGlobalId { get; set; }
    public System.Guid SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}