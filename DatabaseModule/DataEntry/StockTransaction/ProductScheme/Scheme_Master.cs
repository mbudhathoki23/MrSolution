using DatabaseModule.CloudSync;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DatabaseModule.DataEntry.StockTransaction.ProductScheme;

public class Scheme_Master : BaseSyncData
{
    public int SchemeId { get; set; }
    public System.DateTime SchemeDate { get; set; }
    public string SchemeMiti { get; set; }
    public System.DateTime SchemeTime { get; set; }
    public string SchemeDesc { get; set; }
    public System.DateTime? ValidFrom { get; set; }
    public string ValidFromMiti { get; set; }
    public System.DateTime? ValidTo { get; set; }
    public string ValidToMiti { get; set; }
    public string EnterBy { get; set; }
    public System.DateTime? EnterDate { get; set; }
    public bool IsActive { get; set; }
    public string Remarks { get; set; }
    public int BranchId { get; set; }
    public int? CompanyUnitId { get; set; }
    public int FiscalYearId { get; set; }
    public List<Scheme_Details> SchemeDetails { get; set; }
    public DataGridView GetView { get; set; }
}