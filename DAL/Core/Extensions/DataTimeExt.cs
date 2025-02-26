using System;
using System.Windows.Forms;

namespace MrDAL.Core.Extensions;

public static class DataTimeExt
{
    // RETURN VALUE IN DATE TIME FORMAT
    public static DateTime GetDateTime(this string value)
    {
        var result = DateTime.TryParse(value, out var dateTime);
        return result ? dateTime : DateTime.Now;
    }
    public static DateTime GetDateTime(this object value)
    {
        if (value is null) return DateTime.Now;
        DateTime.TryParse(value.ToString(), out var dateTime);
        return dateTime;
    }
    public static DateTime GetDateTime(this MaskedTextBox value)
    {
        if (!value.MaskCompleted) return DateTime.Now;
        DateTime.TryParse(value.Text, out var dateTime);
        return dateTime;
    }
}