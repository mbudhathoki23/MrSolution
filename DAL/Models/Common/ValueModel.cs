namespace MrDAL.Models.Common;

public class ValueModel<T>
{
    public T Id { get; set; }
    public string Value { get; set; }

    public ValueModel(T id, string value)
    {
        Id = id;
        Value = value;
    }

    public ValueModel()
    {
    }

    //public T Id { get; set; }
    //public string Value { get; set; }
}

public class ValueModel<T1, T2>
{
    public ValueModel(T1 item1, T2 item2)
    {
        Item1 = item1;
        Item2 = item2;
    }

    public ValueModel()
    {
    }

    public T1 Item1 { get; set; }
    public T2 Item2 { get; set; }
}

public class ValueModel<T1, T2, T3>
{
    public ValueModel(T1 item1, T2 item2, T3 item3)
    {
        Item1 = item1;
        Item2 = item2;
        Item3 = item3;
    }

    public ValueModel()
    {
    }

    public T1 Item1 { get; set; }
    public T2 Item2 { get; set; }
    public T3 Item3 { get; set; }
}