using System;

namespace MrBLL.Utility.Restore.PInvoke;

public struct STARTUPINFO
{
    public int cb;

    public string lpReserved;

    public string lpDesktop;

    public string lpTitle;

    public int dwX;

    public int dwY;

    public int dwXSize;

    public int dwYSize;

    public int dwXCountChars;

    public int dwYCountChars;

    public int dwFillAttribute;

    public int dwFlags;

    public short wShowWindow;

    public short cbReserved2;

    public IntPtr lpReserved2;

    public IntPtr hStdInput;

    public IntPtr hStdOutput;

    public IntPtr hStdError;
}