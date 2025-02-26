using MrDAL.Global.Common;
using System.Windows.Forms;

namespace MrDAL.Control.ControlsEx.Control;

public class MrMaskedTextBox : MaskedTextBox
{
    public MrMaskedTextBox()
    {
        Mask = @"99/99/9999";
        BorderStyle = BorderStyle.FixedSingle;
        InsertKeyMode = InsertKeyMode.Overwrite;
        GotFocus += (sender, e) =>
        {
            SelectionStart = 0;
            BackColor = ObjGlobal.GetEnterBackColor();
            ForeColor = ObjGlobal.GetEnterForeColor();
        };
        LostFocus += (sender, e) =>
        {
            BackColor = ObjGlobal.GetLeaveBackColor();
            ForeColor = ObjGlobal.GetLeaveForeColor();
        };
    }
}