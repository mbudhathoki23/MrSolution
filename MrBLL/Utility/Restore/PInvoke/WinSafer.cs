using System;
using System.Runtime.InteropServices;

namespace MrBLL.Utility.Restore.PInvoke;

public class WinSafer
{
    [DllImport("advapi32", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.None,
        ExactSpelling = false, SetLastError = true)]
    public static extern bool SaferCloseLevel(IntPtr levelHandle);

    [DllImport("advapi32", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.None,
        ExactSpelling = false, SetLastError = true)]
    public static extern bool SaferComputeTokenFromLevel(IntPtr levelHandle, IntPtr inAccessToken,
        out IntPtr outAccessToken, SaferTokenBehaviour flags, IntPtr lpReserved);

    [DllImport("advapi32", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.None,
        ExactSpelling = false, SetLastError = true)]
    public static extern bool SaferCreateLevel(SaferLevelScope scopeId, SaferLevel levelId, SaferOpen openFlags,
        out IntPtr levelHandle, IntPtr reserved);
}