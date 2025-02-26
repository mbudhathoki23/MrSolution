using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using System.ComponentModel;
using System.Windows.Forms;

namespace MrDAL.Control.ControlsEx.Control;

public class MrTextBox : TextBox
{
    public MrTextBox()
    {
        BorderStyle = BorderStyle.FixedSingle;
        GotFocus += (_, _) =>
        {
            BackColor = ObjGlobal.GetEnterBackColor();
            ForeColor = ObjGlobal.GetEnterForeColor();
        };
        LostFocus += (_, _) =>
        {
            BackColor = ObjGlobal.GetLeaveBackColor();
            ForeColor = ObjGlobal.GetLeaveForeColor();
        };
        KeyPress += OnKeyPress;
        KeyDown += OnKeyDown;
    }

    [Browsable(true)]
    [DefaultValue(TextBoxType.Normal)]
    public TextBoxType TextBoxType { get; set; }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (TextBoxType is TextBoxType.Numeric or TextBoxType.Decimal)
            if (e.Control && e.KeyCode is Keys.V)
                Text = Text.GetDecimalString();
    }

    private void OnKeyPress(object sender, KeyPressEventArgs e)
    {
        if (TextBoxType == TextBoxType.Numeric) e.Handled = e.IsDecimal(sender);
    }
}