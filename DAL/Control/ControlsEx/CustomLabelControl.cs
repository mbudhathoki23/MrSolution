using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace MrDAL.Control.ControlsEx;

internal class CustomLabelControl : LabelControl, ICustomControl
{
    public CustomLabelControl()
    {
        SetStyle(ControlStyles.Selectable, true);
        Text = @"LabelControl";
        Name = "CustomLabelControl";
    }

    public sealed override string Text
    {
        get => base.Text;
        set => base.Text = value;
    }
}