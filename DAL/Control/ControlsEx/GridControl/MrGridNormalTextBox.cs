using MrDAL.Global.Common;
using System.Windows.Forms;

namespace MrDAL.Control.ControlsEx.GridControl;

public class MrGridNormalTextBox : TextBox
{
    public MrGridNormalTextBox(System.Windows.Forms.Control grid)
    {
        TextAlign = HorizontalAlignment.Left;
        BorderStyle = BorderStyle.FixedSingle;
        GotFocus += (sender, e) =>
        {
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
        if (keyData != Keys.Enter) return base.ProcessCmdKey(ref msg, keyData);
        SendKeys.Send("{TAB}");
        return true;
    }
}