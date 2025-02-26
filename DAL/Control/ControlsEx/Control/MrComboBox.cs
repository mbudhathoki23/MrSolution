using MrDAL.Global.Common;
using System.Windows.Forms;

namespace MrDAL.Control.ControlsEx.Control;

public class MrComboBox : ComboBox
{
    public MrComboBox()
    {
        FlatStyle = FlatStyle.Flat;
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
        KeyPress += (sender, e) =>
        {
            if (e.KeyChar is (char)Keys.Space) SendKeys.Send("{TAB}");
        };
    }
}