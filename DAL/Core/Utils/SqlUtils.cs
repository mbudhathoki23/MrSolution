using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace MrDAL.Core.Utils;

public static class SqlUtils
{
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

    public static T? GetValueOrNull<T>(DbDataReader reader, string columnName) where T : struct
    {
        var columnValue = reader[columnName];

        if (!(columnValue is DBNull))
            return (T)columnValue;

        return null;
    }

    public static T GetValueOrNull2<T>(DbDataReader reader, string columnName) where T : class
    {
        var columnValue = reader[columnName];

        if (!(columnValue is DBNull))
            return (T)columnValue;

        return null;
    }
}