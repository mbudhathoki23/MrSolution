using Microsoft.Win32;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MrDAL.Global.Common;

public class ClsDateMiti
{
    public DateTime MDate { get; set; }
    public string MMiti { get; set; }
    public string MonthName { get; set; }
    public string Days { get; set; }

    public DataTable Get(DateTime? date = null, string miti = "")
    {
        var cmdString = new StringBuilder();
        cmdString.Append("Select * from AMS.DateMiti Where 1= 1 \n");

        if (date != null) cmdString.Append($"and AD_Date = '{Convert.ToDateTime(date):MM/dd/yyyy}'");
        if (!string.IsNullOrEmpty(miti)) cmdString.Append($"and BS_DateDMY = '{miti}'");
        return SqlExtensions.ExecuteDataSet(cmdString.ToString())
            .Tables[0];
    }

    public DateTime? GetDate(string miti)
    {
        var dt = Get(null, miti);
        return dt.Rows.Count > 0 ? Convert.ToDateTime(dt.Rows[0]["AD_Date"]) : null;
    }

    public DateTime GetDate1(string miti)
    {
        var dttime = GetDate(miti);
        return dttime != null ? Convert.ToDateTime(dttime) : new DateTime();
    }

    public string GetMiti(DateTime date)
    {
        var dt = Get(date);
        return dt.Rows.Count > 0 ? dt.Rows[0]["BS_DateDMY"].ToString() : string.Empty;
    }

    public string GetServerDate()
    {
        var cmdString = new StringBuilder();
        cmdString.Append("Select convert(varchar(10),GETDATE(),103) as ServerDate");
        var dt = SqlExtensions.ExecuteDataSet(cmdString.ToString()).Tables[0];
        return dt.Rows[0]["ServerDate"].ToString();
    }

    public DateTime GetServerDateTime()
    {
        var cmdString = new StringBuilder();
        cmdString.Append("Select getDate() As ServerDateTime");
        var dt = SqlExtensions.ExecuteDataSet(cmdString.ToString()).Tables[0];
        return Convert.ToDateTime(dt.Rows[0]["ServerDateTime"].ToString());
    }

    public string DateFormatYmd(string date)
    {
        var s = date.Split('/');
        return s[2] + '-' + s[1] + '-' + s[0];
    }

    public DateTime _GetDate(string miti)
    {
        var date = GetDate(miti);
        return date != null ? Convert.ToDateTime(date) : new DateTime();
    }

    private void SetDate(string dateInYourSystemFormat)
    {
        var proc = new ProcessStartInfo
        {
            UseShellExecute = true,
            WorkingDirectory = @"C:\Windows\System32",
            CreateNoWindow = true,
            FileName = @"C:\Windows\System32\cmd.exe",
            Verb = "runas",
            Arguments = "/C date " + dateInYourSystemFormat
        };
        try
        {
            Process.Start(proc);
        }
        catch
        {
            MessageBox.Show("Error to change time of your system");
            Application.ExitThread();
        }
    }

    internal void SetTime(string timeInYourSystemFormat)
    {
        var proc = new ProcessStartInfo
        {
            UseShellExecute = true,
            WorkingDirectory = @"C:\Windows\System32",
            CreateNoWindow = true,
            FileName = @"C:\Windows\System32\cmd.exe",
            Verb = "runas",
            Arguments = "/C time " + timeInYourSystemFormat
        };
        try
        {
            Process.Start(proc);
        }
        catch
        {
            MessageBox.Show("Error to change time of your system");
            Application.ExitThread();
        }
    }

    // Return Type: BOOL
    [DllImport("kernel32.dll", EntryPoint = "SetSystemTime")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetSystemTime([In] ref SystemTime lpSystemTime);

    public static bool SetSystemDateTime(DateTime newDateTime)
    {
        var result = false;

        try
        {
            newDateTime = newDateTime.ToUniversalTime();
            var sysTime = new SystemTime
            {
                wYear = (short)newDateTime.Year /* must be short */,
                wMonth = (short)newDateTime.Month,
                wDayOfWeek = (short)newDateTime.DayOfWeek,
                wDay = (short)newDateTime.Day,
                wHour = (short)newDateTime.Hour,
                wMinute = (short)newDateTime.Minute,
                wSecond = (short)newDateTime.Second,
                wMilliseconds = (short)newDateTime.Millisecond
            };

            result = SetSystemTime(ref sysTime);
        }
        catch (Exception)
        {
            result = false;
        }

        return result;
    }

    public static bool ChangeDateFormat()
    {
        try
        {
            var rkey = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
            rkey.SetValue("sShortDate", "dd/MM/yyyy");
            rkey.SetValue("sLongDate", "dd/MM/yyyy");

            var ci = new CultureInfo(CultureInfo.CurrentCulture.Name)
            {
                DateTimeFormat = { ShortDatePattern = "dd'/'MM'/'yyyy", LongTimePattern = "hh':'mm tt" }
            };
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            return true;
        }
        catch (Exception)

        {
            return false;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct SystemTime
    {
        public short wYear;
        public short wMonth;
        public short wDayOfWeek;
        public short wDay;
        public short wHour;
        public short wMinute;
        public short wSecond;
        public short wMilliseconds;
    }
}