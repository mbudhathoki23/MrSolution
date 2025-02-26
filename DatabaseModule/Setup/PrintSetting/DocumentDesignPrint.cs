using System;
using System.Windows.Forms;

namespace DatabaseModule.Setup.PrintSetting;

public class DocumentDesignPrint
{
    public string ActionTag { get; set; }
    public DataGridView SGridView { get; set; }
    public int DDP_Id { get; set; }
    public string Module { get; set; }
    public string Paper_Name { get; set; }
    public bool Is_Online { get; set; }
    public int NoOfPrint { get; set; }
    public string Notes { get; set; }
    public string DesignerPaper_Name { get; set; }
    public string Created_By { get; set; }
    public DateTime? Created_Date { get; set; }
    public bool Status { get; set; }
    public int Branch_Id { get; set; }
    public int? CmpUnit_Id { get; set; }
}