using System.Windows.Forms;
using MrDAL.Utility.Server;

namespace MrDAL.Core.Extensions;

public static class ShortExt
{
    // RETURN VALUE IN INT
    public static short GetShort(this string value)
    {
        short.TryParse(value, out var result);
        return result;
    }
    public static short GetShort(this object value)
    {
        if (value == null)
        {
            return 0;
        }
        return GetShort(value.ToString());
    }
    public static short GetShort(this TextBox textBox)
    {
        if (textBox.IsBlankOrEmpty())
        {
            return 0;
        }
        return GetShort(textBox.Text);
    }
    public static short ReturnRowNo(this string value, string module, string filterValue)
    {
        var (table, column, _) = StringExt.GetTableInfo(module);
        if (table.IsBlankOrEmpty()) return 1;
        var cmdString = @$"
        SELECT MAX(CAST(ISNULL(SyncRowVersion,0) AS INT))+1 MaxId FROM {table} where {column} = '{filterValue}' ";
        var dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        var result = dt.Rows[0]["MaxId"].GetShort();
        return dt.RowsCount() > 0 && result > 0 ? result : 1.GetShort();
    }
    public static short ReturnSyncRowNo(this object value, string module, int filterValue)
    {
        value = value is null ? 1.GetShort() : ReturnRowNo(value.ToString(), module, filterValue.ToString());
        return value.GetShort();
    }
    public static short ReturnSyncRowNo(this object value, string module, string filterValue)
    {
        value = value is null ? 1.GetShort() : ReturnRowNo(value.ToString(), module, filterValue);
        return value.GetShort();
    }
}