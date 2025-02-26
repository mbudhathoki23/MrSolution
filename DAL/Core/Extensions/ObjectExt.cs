using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace MrDAL.Core.Extensions;

public static class ObjectExt
{
    public static T DeepCopy<T>(this T other)
    {
        var ms = new MemoryStream();
        var formatter = new BinaryFormatter();
        formatter.Serialize(ms, other);
        ms.Position = 0;
        return (T)formatter.Deserialize(ms);
    }
    public static T CloneObject<T>(this object source, bool incRefMembers)
    {
        var newObject = Activator.CreateInstance<T>();

        var fields = incRefMembers
            ? newObject.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
            : newObject.GetType().GetProperties(BindingFlags.Public | BindingFlags.CreateInstance);

        foreach (var field in fields)
        {
            var value = field.GetValue(source, null);
            field.SetValue(newObject, value, null);
        }

        return newObject;
    }
}