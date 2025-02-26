using System;
using System.ComponentModel;
namespace DatabaseModule.Master.ProductSetup;

[Description("ProductSetup"), Category("Master/ImportProduct")]
public class ImportProduct
{
    public long PID { get; set; }
    public string NepaliDesc { get; set; }
    public string PName { get; set; }
    public string PAlias { get; set; }
    public string PShortName { get; set; }
    public string PType { get; set; }
    public string PCategory { get; set; }
    public int PUnit { get; set; }
    public int? PAltUnit { get; set; }
    public decimal PQtyConv { get; set; }
    public decimal PAltConv { get; set; }
    public string PValTech { get; set; }

    public bool PSerialno { get; set; } = false;
    //public bool isBook { get; set; } = false;
    public bool PSizewise { get; set; } = false;
    public bool PBatchwise { get; set; } = false;
    public bool PVehicleWise { get; set; } = false;
    public bool PublicationWise { get; set; } = false;

    public decimal PBuyRate { get; set; } = 0;
    public decimal AltSalesRate { get; set; } = 0;
    public decimal PSalesRate { get; set; } = 0;
    public decimal PMargin1 { get; set; } = 0;
    public decimal TradeRate { get; set; } = 0;
    public decimal PMargin2 { get; set; } = 0;
    public decimal PMRP { get; set; } = 0;

    public int? PGrpId { get; set; }
    public int? PSubGrpId { get; set; }

    public decimal PTax { get; set; } = 0;
    public decimal PMin { get; set; } = 0;
    public decimal PMax { get; set; } = 0;

    public int? CmpId { get; set; }
    public int? CmpId1 { get; set; }
    public int? CmpId2 { get; set; }
    public int? CmpId3 { get; set; }
    public int Branch_Id { get; set; }
    public int? CmpUnit_Id { get; set; }
    public long? PPL { get; set; }
    public long? PPR { get; set; }
    public long? PSL { get; set; }
    public long? PSR { get; set; }
    public long? PL_Opening { get; set; }
    public long? PL_Closing { get; set; }
    public long? BS_Closing { get; set; }
    public byte[] PImage { get; set; }
    public string EnterBy { get; set; } = "ADMIN";
    public DateTime EnterDate { get; set; } = DateTime.Now;
    public short IsDefault { get; set; } = 0;
    public bool Status { get; set; } = true;
    public string ChasisNo { get; set; }
    public string EngineNo { get; set; }
    public string VHModel { get; set; }
    public string VHColor { get; set; }
    public string VHNumber { get; set; }
    public decimal BeforeBuyRate { get; set; } = 0;
    public decimal BeforeSalesRate { get; set; } = 0;
    public string Barcode { get; set; }
    public string Barcode1 { get; set; }
    public string Barcode2 { get; set; }
    public string Barcode3 { get; set; }
    public Guid? SyncBaseId { get; set; }
    public Guid? SyncGlobalId { get; set; }
    public Guid? SyncOriginId { get; set; }
    public DateTime? SyncCreatedOn { get; set; }
    public DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; } = 1;
}