using DatabaseModule.CloudSync;

namespace DatabaseModule.Setup.UserSetup;

public class User_Role : BaseSyncData
{
    public int Role_Id { get; set; }
    public string Role { get; set; }
    public bool? Status { get; set; }
}