using DevExpress.XtraEditors;

namespace MrDAL.Control.ControlsEx;

internal class CustomTextEdit : TextEdit, ICustomControl
{
    public CustomTextEdit()
    {
        Text = @"TextEdit";
        Name = "CustomTextEdit";
    }
}