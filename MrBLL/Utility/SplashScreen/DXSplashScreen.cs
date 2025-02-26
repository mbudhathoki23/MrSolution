using System;

namespace MrBLL.Utility.SplashScreen;

public partial class DXSplashScreen : DevExpress.XtraSplashScreen.SplashScreen
{
    public enum SplashScreenCommand
    {
    }

    public DXSplashScreen()
    {
        InitializeComponent();
        labelCopyright.Text = "Copyright © 1998-" + DateTime.Now.Year;
    }

    #region Overrides

    public override void ProcessCommand(Enum cmd, object arg)
    {
        base.ProcessCommand(cmd, arg);
    }

    #endregion Overrides
}