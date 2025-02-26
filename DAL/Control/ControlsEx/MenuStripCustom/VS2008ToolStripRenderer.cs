using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MrDAL.Control.ControlsEx.MenuStripCustom;

public class Vs2008ToolStripRenderer : ToolStripProfessionalRenderer
{
    // Render custom background gradient
    protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
    {
        base.OnRenderToolStripBackground(e);

        using var b = new LinearGradientBrush(e.AffectedBounds, ClsColor.ClrVerBgWhite, ClsColor.ClrVerBgGrayBlue,
            LinearGradientMode.Vertical);
        using var shadow = new SolidBrush(ClsColor.ClrVerBgShadow);
        var rect = new Rectangle(0, e.ToolStrip.Height - 2, e.ToolStrip.Width, 1);
        e.Graphics.FillRectangle(b, e.AffectedBounds);
        e.Graphics.FillRectangle(shadow, rect);
    }

    // Render button selected and pressed state
    protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
    {
        base.OnRenderButtonBackground(e);
        var rectBorder = new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 1);
        var rect = new Rectangle(1, 1, e.Item.Width - 2, e.Item.Height - 2);

        if (e.Item.Selected || ((ToolStripButton)e.Item).Checked)
        {
            using var b = new LinearGradientBrush(rect, ClsColor.ClrToolStripBtnGradWhite,
                ClsColor.ClrToolStripBtnGradBlue, LinearGradientMode.Vertical);
            using var b2 = new SolidBrush(ClsColor.ClrToolStripBtnBorder);
            e.Graphics.FillRectangle(b2, rectBorder);
            e.Graphics.FillRectangle(b, rect);
        }

        if (!e.Item.Pressed) return;
        {
            using var b = new LinearGradientBrush(rect, ClsColor.ClrToolStripBtnGradWhitePressed,
                ClsColor.ClrToolStripBtnGradBluePressed, LinearGradientMode.Vertical);
            using var b2 = new SolidBrush(ClsColor.ClrToolStripBtnBorder);
            e.Graphics.FillRectangle(b2, rectBorder);
            e.Graphics.FillRectangle(b, rect);
        }
    }
}