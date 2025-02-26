using System;

namespace MrDAL.Models.Custom;

public class BatchNumberModel
{
    public string BatchNo { get; set; }
    public DateTime MfgDateTime { get; set; }
    public DateTime ExpDateTime { get; set; }
    public decimal QtyDecimal { get; set; }
    public decimal MrpDecimal { get; set; }
    public int UnitId { get; set; }
    public int SNo { get; set; }
    public string Unit { get; set; }
}