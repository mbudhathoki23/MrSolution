namespace MrDAL.Core.Extensions;

public static class ReflectionExt
{
    public static object GetPropertyValue(this object obj, string name)
    {
        foreach (var part in name.Split('.'))
        {
            if (obj == null) return null;

            var type = obj.GetType();
            var info = type.GetProperty(part);
            if (info == null) return null;

            obj = info.GetValue(obj, null);
        }

        return obj;
    }
    public static T GetPropertyValue<T>(this object obj, string name)
    {
        var retrieval = GetPropertyValue(obj, name);
        if (retrieval == null) return default;

        // throws InvalidCastException if types are incompatible
        return (T)retrieval;
    }
}