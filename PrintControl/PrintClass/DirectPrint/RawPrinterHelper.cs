using System;
using System.IO;
using System.Runtime.InteropServices;

namespace PrintControl.PrintClass.DirectPrint;

public class RawPrinterHelper
{
    // Structure and API :
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class DotMatrixPrintFunction
    {
        [MarshalAs(UnmanagedType.LPStr)]
        public string pDocName;
        [MarshalAs(UnmanagedType.LPStr)]
        public string pOutputFile;
        [MarshalAs(UnmanagedType.LPStr)]
        public string pDataType;
    }
    [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

    [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    public static extern bool ClosePrinter(IntPtr hPrinter);

    [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    public static extern bool StartDocPrinter(IntPtr hPrinter, int level, [In, MarshalAs(UnmanagedType.LPStruct)] DotMatrixPrintFunction di);

    [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    public static extern bool EndDocPrinter(IntPtr hPrinter);

    [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    public static extern bool StartPagePrinter(IntPtr hPrinter);

    [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    public static extern bool EndPagePrinter(IntPtr hPrinter);

    [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, int dwCount, out int dwWritten);

    // SendBytesToPrinter()
    // When the function is given a printer name and an unmanaged array
    // of bytes, the function sends those bytes to the print queue.
    // Returns true on success, false on failure.
    public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, int dwCount)
    {
        var di = new DotMatrixPrintFunction();
        var bSuccess = false; // Assume failure unless you specifically succeed.

        di.pDocName = "RAW Document";
        // Win7
        di.pDataType = "RAW";

        // Win8+
        // di.pDataType = "XPS_PASS";

        // Open the printer.
        if (OpenPrinter(szPrinterName.Normalize(), out var hPrinter, IntPtr.Zero))
        {
            // Start a document.
            if (StartDocPrinter(hPrinter, 1, di))
            {
                // Start a page.
                if (StartPagePrinter(hPrinter))
                {
                    // Write your bytes.
                    bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out _);
                    EndPagePrinter(hPrinter);
                }
                EndDocPrinter(hPrinter);
            }
            ClosePrinter(hPrinter);
        }
        // If you did not succeed, GetLastError may give more information
        // about why not.
        if (bSuccess == false)
        {
            Marshal.GetLastWin32Error();
        }
        return bSuccess;
    }

    public static bool SendFileToPrinter(string szPrinterName, string szFileName)
    {
        // Open the file.
        var fs = new FileStream(szFileName, FileMode.Open);
        // Create a BinaryReader on the file.
        var br = new BinaryReader(fs);
        // Dim an array of bytes big enough to hold the file's contents.
        var bSuccess = false;
        // Your unmanaged pointer.

        var nLength = Convert.ToInt32(fs.Length);
        // Read the contents of the file into the array.
        var bytes = br.ReadBytes(nLength);
        // Allocate some unmanaged memory for those bytes.
        var pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
        // Copy the managed byte array into the unmanaged array.
        Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
        // Send the unmanaged bytes to the printer.
        bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, nLength);
        // Free the unmanaged memory that you allocated earlier.
        Marshal.FreeCoTaskMem(pUnmanagedBytes);
        fs.Close();
        fs.Dispose();
        return bSuccess;
    }
    public static bool SendStringToPrinter(string szPrinterName, string szString)
    {
        // How many characters are in the string?
        var dwCount = szString.Length;
        // Assume that the printer is expecting ANSI text, and then convert
        // the string to ANSI text.
        var pBytes = Marshal.StringToCoTaskMemAnsi(szString);
        // Send the converted ANSI string to the printer.
        SendBytesToPrinter(szPrinterName, pBytes, dwCount);
        Marshal.FreeCoTaskMem(pBytes);
        return true;
    }
    public static string GetPrintString(string str, PrintFontType fontType = PrintFontType.Normal, bool addLineFeed = true)
    {
        var rawStr = fontType switch
        {
            PrintFontType.Expand => $"{(char)18}{(char)14}{str}",
            PrintFontType.Contract => $"{(char)18}{(char)15}{str}",
            PrintFontType.Bold => $"{(char)27}{(char)69}{str}",
            PrintFontType.Normal => $"{(char)18}{(char)27}{(char)70}{str}",
            _ => $"{(char)18}{str}"
        };

        if (addLineFeed)
        {
            rawStr += " \n";
        }

        return rawStr;
    }
    public enum PrintFontType
    {
        Expand = 40,
        Contract = 138,
        Bold = 139,
        Normal = 79,
        Default = 0
    }
}