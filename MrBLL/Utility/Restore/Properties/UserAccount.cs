using MrBLL.Utility.Restore.PInvoke;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MrBLL.Utility.Restore.Properties;

public class UserAccount
{
    private const int UAC_SHIELD_IMAGE_WIDTH = 16;

    private const int UAC_SHIELD_IMAGE_HEIGHT = 16;

    private const uint BCM_SETSHIELD = 5644;

    private static Image _uacShieldImage;

    protected static Image UacShieldImage
    {
        get
        {
            if (_uacShieldImage != null) return _uacShieldImage;
            using var image = Image.FromStream(Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("SqlBakRestore.global::MrBLL.Properties.Resources.UAC_shield.png"));
            _uacShieldImage = ResizeImage(image, new Size(16, 16));

            return _uacShieldImage;
        }
    }

    public static void CreateSaferProcess(string fileName, string arguments, SaferLevel saferLevel)
    {
        var zero = IntPtr.Zero;
        if (!WinSafer.SaferCreateLevel(SaferLevelScope.User, saferLevel, SaferOpen.Open, out zero, IntPtr.Zero))
            throw new Win32Exception(Marshal.GetLastWin32Error());
        try
        {
            var intPtr = IntPtr.Zero;
            if (!WinSafer.SaferComputeTokenFromLevel(zero, IntPtr.Zero, out intPtr, SaferTokenBehaviour.Default,
                    IntPtr.Zero)) throw new Win32Exception(Marshal.GetLastWin32Error());
            try
            {
                var sTartuffe = new STARTUPINFO
                {
                    //cb = Marshal.SizeOf((object?)sTARTUPINFO),
                    lpDesktop = string.Empty
                };
                if (!Windows.CreateProcessAsUser(intPtr, fileName, arguments, IntPtr.Zero, IntPtr.Zero, false, 0,
                        IntPtr.Zero, null, ref sTartuffe, out var pROCESSINFORMATION))
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                if (pROCESSINFORMATION.hProcess != IntPtr.Zero) Windows.CloseHandle(pROCESSINFORMATION.hProcess);
                if (pROCESSINFORMATION.hThread != IntPtr.Zero) Windows.CloseHandle(pROCESSINFORMATION.hThread);
            }
            finally
            {
                if (intPtr != IntPtr.Zero) Windows.CloseHandle(intPtr);
            }
        }
        finally
        {
            WinSafer.SaferCloseLevel(zero);
        }
    }

    [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false, SetLastError = true)]
    internal static extern IntPtr FindWindow(string lpszClass, string lpszWindow);

    private static Image ResizeImage(Image image, Size size)
    {
        var bitmap = new Bitmap(size.Width, size.Height);
        using var graphic = Graphics.FromImage(bitmap);
        graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphic.DrawImage(image, 0, 0, size.Width, size.Height);
        return bitmap;
    }

    [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
    internal static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    internal static void SetUacShield(Button button)
    {
        if (FrmRestore.IsAdmin) return;
        if (Environment.OSVersion.Version <= new Version(6, 0))
        {
            button.Image = UacShieldImage;
            button.ImageAlign = ContentAlignment.MiddleLeft;
            return;
        }

        button.FlatStyle = FlatStyle.System;
        var intPtr = new IntPtr(0);
        var intPtr1 = new IntPtr(1);
        SendMessage(button.Handle, 5644, intPtr, intPtr1);
    }
}