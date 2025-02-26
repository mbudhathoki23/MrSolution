using MrDAL.Global.Common;
using System;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace MrDAL.Control.ControlsEx.NotifyPanel;

public static class UiNotifier
{
    public static void NotifySuccess(this System.Windows.Forms.Control container, string message, int duration = 3)
    {
        if (container is Form or UserControl)
            ShowMessage(container, NotifyType.Success, message, duration);
        else
            throw new ArgumentException(@"The parameter must be an instance of UserControl or a Form.",
                nameof(container));
    }

    public static void NotifyError(this System.Windows.Forms.Control container, string message, int duration = 3)
    {
        if (container is not Form && container is not UserControl)
            throw new ArgumentException(@"The parameter must be an instance of UserControl or a Form.",
                nameof(container));
        ShowMessage(container, NotifyType.Error, message, duration);
    }

    public static void NotifyHint(this System.Windows.Forms.Control container, string message, int duration = 3)
    {
        if (container is not Form && container is not UserControl)
            throw new ArgumentException(@"The parameter must be an instance of UserControl or a Form.",
                nameof(container));
        ShowMessage(container, NotifyType.Hint, message, duration);
    }

    public static void NotifyWarning(this System.Windows.Forms.Control container, string message, int duration = 3)
    {
        if (container is not Form && container is not UserControl)
            throw new ArgumentException(@"The parameter must be an instance of UserControl or a Form.",
                nameof(container));
        ShowMessage(container, NotifyType.Warning, message, duration);
    }

    public static void NotifyValidationError(this System.Windows.Forms.Control container,
        System.Windows.Forms.Control control, string message, int duration = 3)
    {
        if (container is not Form && container is not UserControl)
            throw new ArgumentException(@"The parameter must be an instance of UserControl or a Form.",
                nameof(container));

        ShowMessage(container, NotifyType.Error, message, duration);
        control.Focus();
    }

    private static void ShowMessage(System.Windows.Forms.Control container, NotifyType notifyType, string message,
        int duration = 3)
    {
        try
        {
            var panel = new NotifyPanel(container)
            {
                Text = ObjGlobal.Caption
            };
            panel.Notify(notifyType, message);
            var timer = new Timer
            {
                AutoReset = true,
                Interval = duration * 1000
            };
            timer.Elapsed += (_, _) =>
            {
                if (!container.IsHandleCreated) return;
                container.Invoke(new Action(() =>
                {
                    container.Controls.Remove(panel);
                    timer.Stop();
                }));
            };
            timer.Start();
        }
        catch (Exception ex)
        {
            var msg = ex.Message;
            //ex.ToNonQueryErrorResult(ex.StackTrace);
        }
    }
}