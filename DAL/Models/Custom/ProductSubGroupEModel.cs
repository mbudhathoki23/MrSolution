namespace MrDAL.Models.Custom;

public class ProductSubGroupEModel
{
    public ProductSubGroupEModel(int id, int groupId, string name)
    {
        Id = id;
        GroupId = groupId;
        Name = name;
    }

    public ProductSubGroupEModel()
    {
    }

    public int Id { get; set; }
    public int GroupId { get; set; }
    public string Name { get; set; }
}