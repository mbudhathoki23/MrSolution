using System;

namespace MrDAL.Reports.ViewModule;

public class VmDynamicReportTemplate
{
    public int ID { get; set; }
    public string ActionTag { get; set; }
    public string Report_Name { get; set; }
    public string Reports_Type { get; set; }
    public byte[] FileName { get; set; }
    public string FullPath { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string ReportSource { get; set; }
    public string ReportCategory { get; set; }
}