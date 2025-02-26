using MrDAL.Global.Common;
using System.Windows.Forms;

namespace MrDAL.Control.ControlsEx.Control;

public class MrNumericTextBox : TextBox
{
    public MrNumericTextBox()
    {
        TextAlign = HorizontalAlignment.Right;
        BorderStyle = BorderStyle.FixedSingle;
        string val;
        GotFocus += (_, e) =>
        {
            BackColor = ObjGlobal.GetEnterBackColor();
            ForeColor = ObjGlobal.GetEnterForeColor();
        };
        LostFocus += (_, e) =>
        {
            BackColor = ObjGlobal.GetLeaveBackColor();
            ForeColor = ObjGlobal.GetLeaveForeColor();
        };
        KeyPress += (_, e) =>
        {
            if (Text == @"0.00")
            {
                val = string.Empty;
            }
            else if (Text.Length == 0 && e.KeyChar == '.')
            {
                val = @"0.";
                Text = @"0.";
            }
            else
            {
                val = Text;
            }

            if (val.Split('.').Length - 1 >= 1 && e.KeyChar == 46)
            {
                e.Handled = true;
                Focus();
                return;
            }

            if (e.KeyChar == 13 || e.KeyChar == 8) return;
            if (char.IsDigit(e.KeyChar) || e.KeyChar == 46) return;
            e.Handled = true;
            Focus();
        };
    }
}