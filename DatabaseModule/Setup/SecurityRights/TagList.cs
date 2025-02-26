using System.Collections;

namespace DatabaseModule.Setup.SecurityRights;

public class TagList
{
    public string SelectQuery { get; set; } = string.Empty;
    public string FrmName { get; set; } = string.Empty;
    public string ConType { get; set; } = string.Empty;
    public ArrayList HeaderCap { get; set; } = new();
    public ArrayList ColumnWidths { get; set; } = new();
}