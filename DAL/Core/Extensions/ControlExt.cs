namespace MrDAL.Core.Extensions;

public static class ControlExt
{
    public static void DisposeChildren(this System.Windows.Forms.Control control)
    {
        foreach (System.Windows.Forms.Control ctrl in control.Controls) ctrl.Dispose();
        control.Controls.Clear();
    }
}