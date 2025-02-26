using System;

namespace MrDAL.Utility.Licensing;

public class LicBranch
{
    public LicBranch(Guid outletUqId, OutletNature nature, uint maxUsers, uint maxPc, string serverName,
        DateTime expDate)
    {
        OutletUqId = outletUqId;
        Nature = nature;
        MaxUsers = maxUsers;
        ExpDate = expDate;
        MaxPc = maxPc;
        ServerName = serverName;
    }

    public Guid OutletUqId { get; set; }
    public OutletNature Nature { get; set; }
    public DateTime ExpDate { get; set; }
    public uint MaxPc { get; set; }
    public uint MaxUsers { get; set; }
    public string ServerName { get; set; }
}