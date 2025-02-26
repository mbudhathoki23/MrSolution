using System.Drawing;

namespace MrDAL.Core.Extensions;

public static class ByteExt
{
    // RETURN IMAGE
    #region --------------- RETURN VALUE IN IMANGE ---------------
    public static Image GetImage(this object value)
    {
        if (value is not byte[] image || value.GetByte() is 0) return null;
        Image newImage = (Bitmap)new ImageConverter().ConvertFrom(image);
        return newImage;
    }
    public static byte GetByte(this object value)
    {
        byte.TryParse(value.ToString(), out var decimalValue);
        return decimalValue;
    }
    public static byte[] ConvertImage(this Image value)
    {
        var converter = new ImageConverter();
        var result = (byte[])converter.ConvertTo(value, typeof(byte[]));
        return result is { Length: > 0 } ? result : null;
    }
    public static byte GetBase64String(this string value)
    {
        var result = byte.TryParse(value, out var valueResult);
        return result ? valueResult : new byte();
    }
    #endregion
}