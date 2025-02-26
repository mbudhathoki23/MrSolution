using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MrDAL.Data;

public class SqlUtil
{
    private static readonly Dictionary<Type, SqlDbType> typeMap = new();

    public SqlUtil()
    {
        typeMap[typeof(string)] = SqlDbType.NVarChar;
        typeMap[typeof(char[])] = SqlDbType.NVarChar;
        typeMap[typeof(byte)] = SqlDbType.TinyInt;
        typeMap[typeof(short)] = SqlDbType.SmallInt;
        typeMap[typeof(int)] = SqlDbType.Int;
        typeMap[typeof(long)] = SqlDbType.BigInt;
        typeMap[typeof(byte[])] = SqlDbType.Image;
        typeMap[typeof(bool)] = SqlDbType.Bit;
        typeMap[typeof(DateTime)] = SqlDbType.DateTime2;
        typeMap[typeof(DateTimeOffset)] = SqlDbType.DateTimeOffset;
        typeMap[typeof(decimal)] = SqlDbType.Money;
        typeMap[typeof(float)] = SqlDbType.Real;
        typeMap[typeof(double)] = SqlDbType.Float;
        typeMap[typeof(TimeSpan)] = SqlDbType.Time;
    }

    // Non-generic argument-based method
    public static SqlDbType GetDbType(Type giveType)
    {
        // Allow nullable types to be handled
        giveType = Nullable.GetUnderlyingType(giveType) ?? giveType;

        if (typeMap.ContainsKey(giveType)) return typeMap[giveType];

        throw new ArgumentException($"{giveType.FullName} is not a supported .NET class");
    }

    // Generic version
    public static SqlDbType GetDbType<T>()
    {
        return GetDbType(typeof(T));
    }

    public static string GetParamStrForSqlIn(int[] values)
    {
        if (!values.Any()) return null;

        var result = new StringBuilder(string.Empty);

        for (var i = 0; i < values.Length; i++)
            if (i == 0) result.Append(values[0]);
            else result.Append(", " + values[i]);

        return result.ToString().Trim();
    }

    public static string GetParamStrForSqlIn<T>(IList<T> values)
    {
        if (!values.Any()) return null;

        var result = new StringBuilder(string.Empty);

        for (var i = 0; i < values.Count; i++) result.Append(i == 0 ? $@"'{values[0]}'" : $@", '{values[i]}'");

        return result.ToString().Trim();
    }

    /*
     And this is how you would use it:

var sqlDbType = SqlHelper.GetDbType<string>();
// or:
var sqlDbType = SqlHelper.GetDbType(typeof(DateTime?));
// or:
var sqlDbType = SqlHelper.GetDbType(property.PropertyType);
     */
}