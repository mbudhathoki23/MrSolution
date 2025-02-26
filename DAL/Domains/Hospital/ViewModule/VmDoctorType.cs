using MrDAL.Global.Common;
using System;

namespace MrDAL.Domains.Hospital.ViewModule;

public class VmDoctorType
{
    public string ActionTag { get; set; }
    public int DtID { get; set; }
    public string NepaliDesc { get; set; }
    public string DrTypeDesc { get; set; }
    public string DrTypeShortName { get; set; }
    public int BranchId { get; set; } = ObjGlobal.SysBranchId;
    public int? Company_Unit { get; set; } = ObjGlobal.SysCompanyUnitId;
    public string EnterBy { get; set; } = ObjGlobal.LogInUser;
    public DateTime EnterDate { get; set; } = DateTime.Now;
    public bool Status { get; set; } = true;
    public Guid SyncBaseId { get; set; } = Guid.NewGuid();
    public Guid SyncGlobalId { get; set; } = Guid.NewGuid();
    public Guid SyncOriginId { get; set; } = Guid.NewGuid();
    public DateTime? SyncCreatedOn { get; set; } = DateTime.Now;
    public DateTime? SyncLastPatchedOn { get; set; } = DateTime.Now;
    public short SyncRowVersion { get; set; } = 1;
}