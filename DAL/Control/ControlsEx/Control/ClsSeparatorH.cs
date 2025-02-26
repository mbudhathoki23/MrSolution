using MrDAL.Global.Common;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MrDAL.Control.ControlsEx.Control;

public class ClsSeparatorH : GroupBox
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
        base.SetBoundsCore(x, y, 3, height, specified);
        base.BackColor = ObjGlobal.GetEnterBackColor();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var g = e.Graphics;
        var myPen = new Pen(ObjGlobal.GetEnterBackColor())
        {
            Width = 30
        };
        g.DrawLine(myPen, 30, 30, 45, 65);
        g.DrawLine(myPen, 1, 1, 45, 65);
    }
}