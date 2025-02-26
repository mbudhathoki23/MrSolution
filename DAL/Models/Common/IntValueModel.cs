namespace MrDAL.Models.Common;

public class IntValueModel
{
    public IntValueModel()
    {
    }

    public IntValueModel(int id, string value)
    {
        Id = id;
        Value = value;
    }

    public int Id { get; set; }
    public string Value { get; set; }
}