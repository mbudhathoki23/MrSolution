using System;
using System.Runtime.InteropServices;

namespace PrintControl.RawPrintFunction;

public partial class RawPrinter
{
    private IntPtr _handlePrinter;
    public string PrinterName;

    public RawPrinter()
    {
        _handlePrinter = IntPtr.Zero;
    }

    public RawPrinter(string printerName) : this()
    {
        PrinterName = printerName;
    }

    public bool Open(string documentName)
    {
        // see if printer is already open
        if (_handlePrinter != IntPtr.Zero)
            return false;

        // opens the printer
        var risp = RawPrinter.OpenPrinter(PrinterName, out _handlePrinter, IntPtr.Zero);
        if (risp == false)
            return false;

        // starts a print job
        var printJob = new RawPrinter.DOCINFOA
        {
            pDocName = documentName,
            pOutputFile = null,
            pDataType = "RAW"
        };

        if (!RawPrinter.StartDocPrinter(_handlePrinter, 1, printJob))
            return false;

        RawPrinter.StartPagePrinter(_handlePrinter); //starts a page       
        return true;
    }

    public bool Close()
    {
        if (_handlePrinter == IntPtr.Zero)
            return false;

        if (!RawPrinter.EndPagePrinter(_handlePrinter))
            return false;

        if (!RawPrinter.EndDocPrinter(_handlePrinter))
            return false;

        if (!RawPrinter.ClosePrinter(_handlePrinter))
            return false;

        _handlePrinter = IntPtr.Zero;
        return true;
    }

    public bool SendData(string data)
    {
        if (_handlePrinter == IntPtr.Zero)
        {
            return false;
        }

        var buf = Marshal.StringToCoTaskMemAnsi(data);
        var ok = RawPrinter.WritePrinter(_handlePrinter, buf, data.Length, out var done);
        Marshal.FreeCoTaskMem(buf);

        return ok;
    }

    public bool SendData(char data) => SendData(data.ToString());
    public bool SendData(int data) => SendData(((char)data).ToString());
}