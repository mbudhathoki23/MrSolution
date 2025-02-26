namespace MrDAL.Models.Custom;

public class UserAccessFormControl
{
    public string FormId { get; set; }
    public string FormName { get; set; }
    public bool NewButtonCheck { get; set; }
    public bool SaveButtonCheck { get; set; }
    public bool EditButtonCheck { get; set; }
    public bool UpdateButtonCheck { get; set; }
    public bool DeleteButtonCheck { get; set; }
    public bool ViewButtonCheck { get; set; }
    public bool SearchButtonCheck { get; set; }
    public bool PrintButtonCheck { get; set; }
    public bool ExportButtonCheck { get; set; }
}
