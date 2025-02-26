using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MrDAL.Control.ControlsEx.MenuStripCustom;

public class Vs2008ToolStripContainer : ToolStripContainer
{
    public Vs2008ToolStripContainer()
    {
        TopToolStripPanel.Paint += TopToolStripPanel_Paint;
        TopToolStripPanel.SizeChanged += TopToolStripPanel_SizeChanged;
    }

    private void TopToolStripPanel_SizeChanged(object sender, EventArgs e)
    {
        Invalidate();
    }

    private void TopToolStripPanel_Paint(object sender, PaintEventArgs e)
    {
        var g = e.Graphics;
        var rect = new Rectangle(0, 0, Width, FindForm()!.Height);
        using var b = new LinearGradientBrush(rect, ClsColor.ClrHorBgGrayBlue, ClsColor.ClrHorBgWhite,
            LinearGradientMode.Horizontal);
        g.FillRectangle(b, rect);
    }
}