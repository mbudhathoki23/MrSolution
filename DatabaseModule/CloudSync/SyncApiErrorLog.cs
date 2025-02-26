using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseModule.CloudSync;

[Table("SyncApiErrorLog", Schema = "AMS")]
public class SyncApiErrorLog
{
    [Key]
    public int Id { get; set; }
    public string? Module { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime? Created { get; set; }
    public string? ErrorType { get; set; }
}