using System;
using System.Windows.Forms;

namespace MrDAL.Core.Extensions;

public static class DecimalExt
{
    // RETURN VALUE IN DECIMAL FORMAT
    public static decimal GetAbs(this decimal value)
    {
        return Math.Abs(value);
    }

    public static decimal GetDecimal(this object value, bool isCurrency = false)
    {
        if (value == null) return isCurrency ? 1 : 0;
        decimal.TryParse(value.ToString(), out var decimalValue);
        return Math.Abs(decimalValue) > 0 ? decimalValue : decimalValue is 0 && isCurrency ? 1 : 0;
    }

    public static decimal GetDecimal(this string value, bool isCurrency = false)
    {
        decimal.TryParse(value, out var decimalValue);
        return Math.Abs(decimalValue) > 0 ? decimalValue : decimalValue is 0 && isCurrency ? 1 : 0;
    }

    public static decimal GetDecimal(this Label value)
    {
        if (value.Text == null) return 0;
        decimal.TryParse(value.Text, out var result);
        return Math.Abs(result) > 0 ? result : 0;
    }

    public static decimal GetDecimal(this TextBox value)
    {
        return GetDecimal(value.Text);
    }

    public static decimal GetPositiveDecimalOrZero(this string value)
    {
        var result = decimal.TryParse(value, out var decimalValue);
        return result && Math.Abs(decimalValue) > 0 ? decimalValue : 0;
    }
}