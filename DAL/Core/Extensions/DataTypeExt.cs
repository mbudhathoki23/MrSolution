using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Label = System.Windows.Forms.Label;
using TextBox = System.Windows.Forms.TextBox;

namespace MrDAL.Core.Extensions;

public static class DataTypeExt
{
  
    // RETURN VALUE IN DOUBLE
    public static float GetFloat(this string value)
    {
        var result = float.TryParse(value, out var decimalValue);
        return result ? decimalValue : 0;
    }
    public static double GetDouble(this object value)
    {
        if (value == null) return 0;
        return GetDouble(value.ToString());
    }
    public static double GetDouble(this Label value)
    {
        return GetDouble(value.Text);
    }
    public static double GetDouble(this TextBox value)
    {
        return GetDouble(value.Text);
    }
    public static double GetDouble(this string value)
    {
        var result = double.TryParse(value, out var decimalValue);
        return result ? decimalValue : 0;
    }
    
    // DEV LABEL ALIGNMENT
    public static void SetDevExpressTextAlignment(this XRLabel label, string alignment)
    {
        label.TextAlignment = alignment switch
        {
            "Left" => TextAlignment.MiddleLeft,
            "Center" => TextAlignment.MiddleCenter,
            "Right" => TextAlignment.MiddleRight,
            _ => TextAlignment.MiddleLeft
        };
    }
}