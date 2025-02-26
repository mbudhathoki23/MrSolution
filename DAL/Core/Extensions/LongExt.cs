using System.Windows.Forms;
using MrDAL.Utility.Server;

namespace MrDAL.Core.Extensions;

public static class LongExt
{
    // RETURN VALUE IN LONG
    public static long GetLong(this string value)
    {
        var result = long.TryParse(value, out var decimalValue);
        return result ? decimalValue : 0;
    }

    public static long GetLong(this object value)
    {
        if (value == null) return 0;
        var result = long.TryParse(value.ToString(), out var decimalValue);
        return result ? decimalValue : 0;
    }

    public static long GetLong(this TextBox value)
    {
        long.TryParse(value.Text, out var result);
        return result > 0 ? result : 0;
    }

    private static long ReturnMaxLongId(this string value, string module, string filterValue)
    {
        var (table, column, _) = StringExt.GetTableInfo(module);
        if (table.IsBlankOrEmpty()) return 1;
        var cmdString = $@"
        SELECT ISNULL(MAX(CAST({column} AS BIGINT)), 0)+1 MaxId FROM {table};";
        var dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        var result = dt.Rows[0]["MaxId"].GetLong();
        return dt.RowsCount() > 0 && result > 0 ? result : 1.GetLong();
    }

    public static long ReturnMaxLongId(this object value, string module, string filterValue)
    {
        return ReturnMaxLongId(value.ToString(), module, filterValue);
    }

    public static long ReturnMasterKeyId(this object value, string module)
    {
        var (table, column, _) = StringExt.GetTableInfo(module);
        if (table.IsBlankOrEmpty()) return 1;
        var cmdString = $"SELECT ISNULL(MAX(CAST(MasterKeyId AS BIGINT)), 0)+1 MaxId FROM {table};";
        var dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        var result = dt.Rows[0]["MaxId"].GetLong();
        return dt.RowsCount() > 0 && result > 0 ? result : 1.GetLong();
    }
}