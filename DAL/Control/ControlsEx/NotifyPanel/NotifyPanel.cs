using System;
using System.Drawing;
using System.Windows.Forms;

namespace MrDAL.Control.ControlsEx.NotifyPanel;

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