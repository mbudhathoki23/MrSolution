using MrDAL.Master.Interface.ProductSetup;

namespace MrDAL.Master.ProductSetup;

public class ColastarRepository : IColastarRepository
{
    public ColastarRepository() { }

    // INSERT UPDATE DELETE
    public int SaveColastarAuditLog(string actionTag)
    {
        var cmdString = $@"";
        return 0;
    }

    // RETURN DATA IN DATA TABLE

    // OBJECT FOR THIS FORM
}