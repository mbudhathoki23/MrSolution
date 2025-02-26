using System;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace MrShell.Common;

public enum NotifyType
{
    Success,
    Error,
    Hint,
    Warning,
    Info
}

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
                Text = message
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
        catch (Exception)
        {
            //var msg = ex.Message;
            //ex.ToNonQueryErrorResult(ex);
        }
    }
}


internal class NotifyPanel : Panel
{
    private readonly Label _label;
    private readonly System.Windows.Forms.Control _window;

    public NotifyPanel(System.Windows.Forms.Control window)
    {
        _window = window;
        BorderStyle = BorderStyle.FixedSingle;

        _label = new Label
        {
            Dock = DockStyle.Fill,
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font(new FontFamily(@"Bookman Old Style"), 12.0f),
            BackColor = Color.Transparent
        };
        base.DoubleBuffered = true;
        Visible = false;
        Controls.Add(_label);
        window.Controls.Add(this);
        BringToFront();
        _label.Click += (_, _) => Controls.Remove(_label);
    }

    public void Notify(NotifyType notifyType, string message)
    {
        switch (notifyType)
        {
            case NotifyType.Success:
                BackColor = Color.DarkGreen;
                _label.ForeColor = Color.White;
                break;

            case NotifyType.Error:
                BackColor = Color.Maroon;
                _label.ForeColor = Color.White;
                break;

            case NotifyType.Hint:
                BackColor = Color.LightBlue;
                _label.ForeColor = Color.Black;
                break;

            case NotifyType.Warning:
                BackColor = Color.Yellow;
                _label.ForeColor = Color.Black;
                break;

            case NotifyType.Info:
                BackColor = Color.LightSkyBlue;
                _label.ForeColor = Color.Black;
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(notifyType), notifyType, null);
        }

        _label.Text = message;

        var size = TextRenderer.MeasureText(message, _label.Font);
        Size = new Size((int)(size.Width * 1.25), size.Height * 3);

        Left = (_window.ClientSize.Width - Size.Width) / 2;
        Top = (_window.ClientSize.Height - Size.Height) / 2;
        //Location = PointToClient(new Point(_window.Bounds.X + _window.Bounds.Width/2 - Size.Width, _window.Bounds.Bottom / 2 - Size.Height / 2));
        Show();
        BringToFront();
    }
}