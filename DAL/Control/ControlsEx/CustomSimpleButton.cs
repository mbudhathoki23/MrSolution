using DevExpress.XtraEditors;

namespace MrDAL.Control.ControlsEx;

internal class CustomSimpleButton : SimpleButton, ICustomControl
{
    public CustomSimpleButton()
    {
        Text = @"SimpleButton";
        Name = "CustomSimpleButton";
    }

    public sealed override string Text
    {
        get => base.Text;
        set => base.Text = value;
    }
}