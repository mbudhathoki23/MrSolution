using DevExpress.XtraEditors;

namespace MrDAL.Control.ControlsEx;

internal class CustomCheckEdit : CheckEdit, ICustomControl
{
    public CustomCheckEdit()
    {
        Text = "CheckEdit";
        Name = "CustomCheckEdit";
    }
}