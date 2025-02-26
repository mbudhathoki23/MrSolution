namespace MrDAL.Models.Common;

public class IntLongValueModel
{
    public IntLongValueModel(long Id, string Value)
    {
        LongId = Id;
        LongValue = Value;
    }
    public long LongId { get; set; }
    public string LongValue { get; set; }
}