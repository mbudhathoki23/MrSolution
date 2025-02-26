using MrDAL.Core.Extensions;
using System.Windows.Forms;

namespace MrDAL.Global.Common;

public static class ClsMoneyConversion
{
    public static string MoneyConversion(decimal mVal)
    {
        return MoneyConversion(mVal.GetString());
    }
    public static string GetNumberInWords(this Label mVal)
    {
        var txtMon = mVal.GetDecimalString();
        var allowed = txtMon.Length > 18 ? "Maximum 18 digits only Allowed" : NToW(txtMon.Trim());
        return allowed;
    }
    public static string GetNumberInWords(this string mVal)
    {
        var txtMon = mVal.GetDecimalString();
        var allowed = txtMon.Length > 18 ? "Maximum 18 digits only Allowed" : NToW(txtMon.Trim());
        return allowed;
    }
    public static string MoneyConversion(string mVal)
    {
        var txtMon = mVal.GetDecimalString();
        var allowed = txtMon.Trim().Length > 18 ? "Maximum 18 digits only Allowed" : NToW(txtMon.Trim());
        return allowed;
    }

    private static string One(long x)
    {
        return x switch
        {
            1 => "One",
            2 => "Two",
            3 => "Three",
            4 => "Four",
            5 => "Five",
            6 => "Six",
            7 => "Seven",
            8 => "Eight",
            9 => "Nine",
            _ => string.Empty
        };
    }

    private static string Two(long x, long y)
    {
        return x switch
        {
            1 => y switch
            {
                0 => "Ten",
                1 => "Eleven",
                2 => "Twelve",
                3 => "Thirteen",
                4 => "Fourteen",
                5 => "Fifteen",
                6 => "Sixteen",
                7 => "Seventeen",
                8 => "Eighteen",
                9 => "Nineteen",
                _ => string.Empty
            },
            2 => "Twenty ",
            3 => "Thirty ",
            4 => "Forty ",
            5 => "Fifty ",
            6 => "Sixty ",
            7 => "Seventy ",
            8 => "Eighty ",
            9 => "Ninety ",
            _ => string.Empty
        };
    }

    private static string Three(long x)
    {
        if (x == 0) return string.Empty;
        var xx = $"{One(x)} Hundred ";
        return xx;
    }

    private static string Thousand(long x, long y)
    {
        return x == 0 && y == 0
            ? string.Empty
            : x != 0
                ? x != 1 ? $"{Two(x, y)}{One(y)} Thousand " : $"{Two(x, y)} Thousand "
                : $"{One(y)} Thousand ";
    }

    private static string Lakh(long x, long y)
    {
        return x == 0 && y == 0 ? string.Empty :
            x != 0 ? x != 1 ? $"{Two(x, y)}{One(y)} Lakh " : $"{Two(x, y)} Lakh " : $"{One(y)} Lakh ";
    }

    private static string Crores(long x, long y)
    {
        if (x == 0 && y == 0)
        {
            return string.Empty;
        }
        else
        {
            return x != 0 ? x != 1 ? $"{Two(x, y)}{One(y)} Crores " : $"{Two(x, y)} Crores " : $"{One(y)} Crores ";
        }
    }

    private static string NToWConv(string aaa)
    {
        var no = new long[10];
        var a = string.Empty;
        var k = aaa.Length;
        for (var j = 0; j < 7; j++)
        {
            no[j] = k > 0 ? aaa.Substring(k - 1, 1).GetLong() : 0;
            k -= 1;
        }

        a += Lakh(no[6], no[5]);
        a += Thousand(no[4], no[3]);
        a += Three(no[2]);
        if (a.Trim().Length > 0 && (no[1] != 0 || no[0] != 0)) a = $"{a} And ";

        a += Two(no[1], no[0]);
        if (no[1] != 1) a += One(no[0]);
        return a;
    }

    public static string NToW(string paraNum)
    {
        if (paraNum.Length > 18) return "Maximum 18 digits only Allowed";
        var a1 = ObjGlobal.ReturnDecimal(paraNum);
        var a2 = (long)decimal.Floor(a1);
        var a3 = a1 - a2;
        if (a2.ToString().Length > 18) return "Maximum 18 digits only Allowed";

        long part1, part2, part3, tempV1, tempV2;
        tempV1 = tempV2 = part1 = part2 = part3 = 0;
        var fr = (long)(a3 * 100);
        tempV1 = a2;

        tempV2 = (long)decimal.Floor(tempV1 / 10_000_000);
        part1 = tempV1 - tempV2 * 10000000;
        tempV1 = tempV2;

        if (tempV1 >= 10000000)
        {
            tempV2 = (long)decimal.Floor(tempV1 / 10_000_000);
            part2 = tempV1 - tempV2 * 10_000_000;
            tempV1 = tempV2;
        }
        else
        {
            part2 = tempV1;
            tempV1 = 0;
        }

        if (tempV1 >= 10000000)
        {
            tempV2 = (long)decimal.Floor(tempV1 / 10_000_000);
            part3 = tempV1 - tempV2 * 10_000_000;
            tempV1 = tempV2;
        }
        else
        {
            part3 = tempV1;
            tempV1 = 0;
        }

        if (tempV1 >= 10000000) return "Rupees Conversion Error: Number exceeds the Length 21 digits";

        var nName = string.Empty;
        var ln = nName.Trim().Length;

        if (part3 > 0) nName = $"{nName.Trim()}{NToWConv(part3.ToString())} Crore Crore";
        if (part2 > 0)
            nName = part2 == 1
                ? $"{nName.Trim()}{string.Empty}{NToWConv(part2.ToString())} Crore"
                : $"{nName.Trim()}{string.Empty}{NToWConv(part2.ToString())} Crores";
        if (part1 > 0) nName = nName.Trim() + string.Empty + NToWConv(part1.ToString());
        if (nName.Trim().Length > 0) nName = $"Rs. {nName}";
        if (fr > 0)
            nName = nName.Trim().Length > 0
                ? $"{nName.Trim()} And {NToWConv(fr.ToString())} Paisa"
                : $"{NToWConv(fr.ToString())} Paisa";
        if (nName.Trim().Length > 0) nName = $"{nName} Only";
        return nName;
    }
}