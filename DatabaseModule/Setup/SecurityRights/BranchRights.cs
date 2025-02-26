namespace DatabaseModule.Setup.SecurityRights;

public class BranchRights
{
    public int RightsId { get; set; }
    public int UserId { get; set; }
    public int BranchId { get; set; }
    public string Branch { get; set; }
}