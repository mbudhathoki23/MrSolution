using MrDAL.Utility.Server;
using System.Data;
using System.Windows.Forms;

namespace MrDAL.Core.Extensions;

public static class IntExt
{
    public static int GetInt(this string value)
    {
        var result = int.TryParse(value, out var decimalValue);
        return result ? decimalValue : 0;
    }

    public static int GetInt(this object value)
    {
        return value == null ? 0 : GetInt(value.ToString());
    }

    public static int GetInt(this TextBox value)
    {
        return GetInt(value.Text);
    }

    public static int RowsCount(this DataTable value)
    {
        if (value?.Rows.Count != null) return (int)value?.Rows.Count;
        return 0;
    }

    private static int ReturnMaxIntId(this string value, string module, string filterValue)
    {
        var (table, column, _) = StringExt.GetTableInfo(module);
        column = filterValue.IsValueExits() ? filterValue : column;
        var cmdString = $@"
        SELECT ISNULL(MAX(CAST({column} AS INT)), 0) + 1 MaxId FROM {table};";
        var dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        var result = dt.Rows[0]["MaxId"].GetInt();
        return dt.RowsCount() > 0 && result > 0 ? result : 1.GetInt();
    }

    public static int ReturnMaxIntId(this object value, string module, string filterValue = "")
    {
        return ReturnMaxIntId(value.ToString(), module, filterValue);
    }
}