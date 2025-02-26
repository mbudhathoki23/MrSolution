using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MrDAL.Control.ControlsEx.Control;

public class ClsSeparator : GroupBox
{
    // Properties
    [DefaultValue("")]
    public override string Text
    {
        get => string.Empty;
        set { }
    }

    protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
    {
        base.SetBoundsCore(x, y, width, 2, specified);
        base.BackColor = Color.AliceBlue;
    }
}