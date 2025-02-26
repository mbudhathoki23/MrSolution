using Dapper.Contrib.Extensions;

namespace MrSolutionTable.Methods;

[Table("lkup.Device"), Serializable]
public class Device
{
    [ExplicitKey]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string DeviceType { get; set; }
    public string Brand { get; set; }
    public string Description { get; set; }
    public string Feature { get; set; }
    public string ImageUrl { get; set; }
    public byte[] photo_bin { get; set; }
}