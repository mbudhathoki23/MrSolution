using System;
using System.Runtime.InteropServices;

namespace MrBLL.Utility.Restore.PInvoke;

public class Windows
{
    [DllImport("coredll.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
    public static extern bool CloseHandle(IntPtr hObject);

    [DllImport("advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
    public static extern bool CreateProcessAsUser(IntPtr hToken, string lpApplicationName, string lpCommandLine,
        IntPtr lpProcessAttributes, IntPtr lpThreadAttributes, bool bInheritHandles, uint dwCreationFlags,
        IntPtr lpEnvironment, string lpCurrentDirectory, ref STARTUPINFO lpStartupInfo,
        out PROCESS_INFORMATION lpProcessInformation);

    public struct SECURITY_ATTRIBUTES
    {
        public int nLength;

        public IntPtr lpSecurityDescriptor;

        public int bInheritHandle;
    }
}