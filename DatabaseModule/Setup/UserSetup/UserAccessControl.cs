namespace DatabaseModule.Setup.UserSetup;

public class UserAccessControl
{
    public int Id { get; set; }
    public int UserRoleId { get; set; }
    public int FeatureAlias { get; set; }
    public int? BranchId { get; set; }
    public bool IsValid { get; set; }
    public System.DateTime CreatedOn { get; set; }
    public System.DateTime? ModifiedOn { get; set; }
    public string UpdatedBy { get; set; }
    public string ConfigXml { get; set; }
    public int? UserId { get; set; }
    public string ConfigFormsXml { get; set; }
}