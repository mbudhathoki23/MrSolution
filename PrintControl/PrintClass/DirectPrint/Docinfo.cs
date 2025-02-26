using System.Runtime.InteropServices;

namespace PrintControl.PrintClass.DirectPrint;

[StructLayout(LayoutKind.Sequential)]
public struct Docinfo
{
    [MarshalAs(UnmanagedType.LPWStr)] public string pDocName;

    [MarshalAs(UnmanagedType.LPWStr)] public string pOutputFile;

    [MarshalAs(UnmanagedType.LPWStr)] public string pDataType;
}