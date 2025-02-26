using MrDAL.Global.Common;
using System.Windows.Forms;

namespace MrDAL.Control.ControlsEx.GridControl;

public class MrGridMaskedTextBox : MaskedTextBox
{
    public MrGridMaskedTextBox(System.Windows.Forms.Control grid)
    {
        Mask = @"99/99/9999";
        InsertKeyMode = InsertKeyMode.Overwrite;
        TextAlign = HorizontalAlignment.Left;
        BorderStyle = BorderStyle.FixedSingle;
        GotFocus += (sender, e) =>
        {
            SendKeys.SendWait("{HOME}");
            SelectionStart = 0;
            BackColor = ObjGlobal.GetEnterBackColor();
            ForeColor = ObjGlobal.GetEnterForeColor();
        };
        LostFocus += (sender, e) =>
        {
            BackColor = ObjGlobal.GetLeaveBackColor();
            ForeColor = ObjGlobal.GetLeaveForeColor();
        };
        grid.Controls.Add(this);
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData == Keys.Enter)
        {
            SendKeys.Send("{TAB}");
            return true;
        }

        if (keyData == Keys.Escape)
            return base.ProcessCmdKey(ref msg, keyData);
        if (keyData == Keys.Up || keyData == Keys.Down)
            return true;
        return base.ProcessCmdKey(ref msg, keyData);
    }
}