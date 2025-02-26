using System.ComponentModel.DataAnnotations;

namespace DatabaseModule.CloudSync;

public class DataSyncPushModel
{
    [Required] public string LocalOriginId { get; set; }

    [Required] public string RepoType { get; set; }

    [Required] public object Data { get; set; }
}