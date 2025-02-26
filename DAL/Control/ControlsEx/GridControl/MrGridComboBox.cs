using MrDAL.Global.Common;
using System.Windows.Forms;

namespace MrDAL.Control.ControlsEx.GridControl;

public class MrGridComboBox : ComboBox
{
    public delegate void EnterKeyPressHandler();

    public MrGridComboBox(System.Windows.Forms.Control grid)
    {
        DropDownStyle = ComboBoxStyle.DropDownList;
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

    public event EnterKeyPressHandler EnterKeyPress;

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData is Keys.Space) SendKeys.Send("{F4}");
        if (keyData != Keys.Enter) return base.ProcessCmdKey(ref msg, keyData);
        if (EnterKeyPress != null)
        {
            EnterKeyPress();
            return true;
        }

        SendKeys.Send("{Tab}");
        return true;
    }
}