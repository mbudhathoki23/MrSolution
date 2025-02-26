using System;

namespace MrDAL.Utility.Analytics.Models;

public class ClientImgPushModel
{
    public string Content { get; set; }
    public Guid ClientImageId { get; set; }
    public DateTime ClientDateTime { get; set; }
    public Guid? ClientId { get; set; }
    public string Machine { get; set; }
    public string MachineUser { get; set; }
}