using System;

namespace MrDAL.Models.Custom;

public class SalesInvoiceItemModel
{
    public int SNo { get; set; }
    public string ProductShortName { get; set; }
    public string ProductName { get; set; }
    public string UnitName { get; set; }
    public string AltUnitName { get; set; }
    public int? UnitId { get; set; }
    public decimal Rate { get; set; }
    public long ProductId { get; set; }
    public decimal? AltQty { get; set; }
    public int? AltUnitId { get; set; }
    public bool HasAltUnit => AltUnitId.GetValueOrDefault() > 0;
    public bool AltUnitEntered { get; set; }
    public decimal Qty { get; set; }
    public decimal FreeQty { get; set; }
    public decimal StockQty => Qty + FreeQty;

    public decimal NAmount => Rate * Qty;
    public decimal ItemDis { get; set; }
    public decimal ItemDisP => ItemDis <= 0 ? 0 : ItemDis / NAmount * 100;
    public decimal ActualAmount => Rate * Qty;
    public decimal ActualAmountAfterDiscount => ActualAmount - ItemDis;

    public decimal AfterItemDis => NAmount - ItemDis;
    public decimal BillDis { get; set; }
    public decimal BillDisP => BillDis <= 0 ? 0 : BillDis / AfterItemDis * 100;

    public decimal ActualAmountAfterBillDiscount => ActualAmountAfterDiscount - BillDis;
    public decimal ProductTermAmount => TaxAmount - ItemDis;

    public decimal TotalDis => ItemDis + BillDis;
    public decimal AfterDis => NAmount - TotalDis;

    public decimal TaxPercent { get; set; }

    //public decimal TaxAmount => TaxPercent <= 0 ? 0 : AfterDis * (TaxPercent / 100);
    //public decimal TaxableAmount => TaxPercent <= 0 ? 0 : AfterDis;

    public decimal TaxableAmount =>
        TaxPercent > 0 ? Math.Round(ActualAmountAfterBillDiscount / (decimal)1.13, 6) : 0;

    public decimal TaxAmount => TaxableAmount > 0 ? ActualAmountAfterBillDiscount - TaxableAmount : 0;
    public decimal TaxExempted => TaxableAmount is 0 ? TaxableAmount : 0;

    public decimal ActualBasicAmount => ActualAmountAfterDiscount - TaxAmount + ItemDis;

    //public decimal TermAmount => TotalDis - TaxAmount;
    //public decimal BAmount => NAmount - TermAmount;
}