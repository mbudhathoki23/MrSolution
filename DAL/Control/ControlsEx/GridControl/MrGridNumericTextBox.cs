using MrDAL.Global.Common;
using System.Windows.Forms;

namespace MrDAL.Control.ControlsEx.GridControl;

public class MrGridNumericTextBox : TextBox
{
    public MrGridNumericTextBox(System.Windows.Forms.Control grid)
    {
        TextAlign = HorizontalAlignment.Right;
        BorderStyle = BorderStyle.FixedSingle;
        KeyDown += MrGridNumericTextBox_KeyDown;
        GotFocus += (sender, e) =>
        {
            SelectAll();
            BackColor = ObjGlobal.GetEnterBackColor();
            ForeColor = ObjGlobal.GetEnterForeColor();
        };
        LostFocus += (sender, e) =>
        {
            BackColor = ObjGlobal.GetLeaveBackColor();
            ForeColor = ObjGlobal.GetLeaveForeColor();
        };
        KeyPress += OnKeyPressEventHandler;
        grid.Controls.Add(this);
    }

    private void OnKeyPressEventHandler(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
        }
    }

    private void MrGridNumericTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode is Keys.A) SelectAll();

        if (e.Control && e.KeyCode is Keys.C) Copy();

        if (e.Control && e.KeyCode is Keys.X) Cut();

        if (e.Control && e.KeyCode is Keys.V) Paste();

        if (e.Control && e.KeyCode is Keys.Z) Undo();

        if (e.KeyCode is Keys.Enter)
        {
        }

        if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
            e.Handled = false;
        }
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        var key = (char)keyData;
        if (keyData == Keys.Enter || char.IsLetter(key) || key == 191 || key == 187 || key == 188 || key == 189)
        {
            if (key != 96 && key != 97 && key != 98 && key != 99 && key != 100 && key != 101 && key != 102 &&
                key != 103 && key != 104 && key != 105 && key != 110 && key != 113)
            {
                if (key == 191 || key == 187 || key == 188 || key == 189)
                {
                    Focus();
                    return true;
                }

                if (char.IsLetter(key)) return true;

                if (keyData == Keys.Control) SelectAll();
                if (keyData == Keys.Enter)
                {
                    SendKeys.Send("{TAB}");
                    base.ProcessCmdKey(ref msg, keyData);
                }
            }
            else if (keyData is Keys.Up or Keys.Down)
            {
                return true;
            }
        }
        else if (keyData is Keys.Up or Keys.Down)
        {
            return true;
        }

        if (Text.Split('.').Length - 1 > 0 && key == 110) return true;
        return base.ProcessCmdKey(ref msg, keyData);
    }
}