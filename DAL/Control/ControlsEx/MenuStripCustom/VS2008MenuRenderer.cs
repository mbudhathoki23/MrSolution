using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MrDAL.Control.ControlsEx.MenuStripCustom;

public class Vs2008MenuRenderer : ToolStripRenderer
{
    // Make sure the Text color is black
    protected override void InitializeItem(ToolStripItem item)
    {
        base.InitializeItem(item);
        item.ForeColor = Color.Black;
    }

    protected override void Initialize(ToolStrip toolStrip)
    {
        base.Initialize(toolStrip);
        toolStrip.ForeColor = Color.Black;
    }

    // Render horizontal background gradient
    protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
    {
        base.OnRenderToolStripBackground(e);
        var b = new LinearGradientBrush(e.AffectedBounds, ClsColor.ClrHorBgGrayBlue, ClsColor.ClrHorBgWhite,
            LinearGradientMode.Horizontal);
        e.Graphics.FillRectangle(b, e.AffectedBounds);
    }

    // Render image margin and gray ItemBackground
    protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
    {
        base.OnRenderImageMargin(e);

        // Draw ImageMargin background gradient
        var b = new LinearGradientBrush(e.AffectedBounds, ClsColor.ClrImageMarginWhite,
            ClsColor.ClrImageMarginBlue, LinearGradientMode.Horizontal);

        // Shadow at the right of image margin
        var darkLine = new SolidBrush(ClsColor.ClrImageMarginLine);
        var whiteLine = new SolidBrush(Color.White);
        var rect = new Rectangle(e.AffectedBounds.Width, 2, 1, e.AffectedBounds.Height);
        var rect2 = new Rectangle(e.AffectedBounds.Width + 1, 2, 1, e.AffectedBounds.Height);

        // Gray background
        var solidBrush = new SolidBrush(ClsColor.ClrSubmenuBg);
        var rect3 = new Rectangle(0, 0, e.ToolStrip.Width, e.ToolStrip.Height);

        // Border
        var borderPen = new Pen(ClsColor.ClrMenuBorder);
        var rect4 = new Rectangle(0, 1, e.ToolStrip.Width - 1, e.ToolStrip.Height - 2);

        e.Graphics.FillRectangle(solidBrush, rect3);
        e.Graphics.FillRectangle(b, e.AffectedBounds);
        e.Graphics.FillRectangle(darkLine, rect);
        e.Graphics.FillRectangle(whiteLine, rect2);
        e.Graphics.DrawRectangle(borderPen, rect4);
    }

    // Render Check mark
    protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
    {
        base.OnRenderItemCheck(e);

        if (e.Item.Selected)
        {
            var rect = new Rectangle(3, 1, 20, 20);
            var rect2 = new Rectangle(4, 2, 18, 18);
            var b = new SolidBrush(ClsColor.ClrToolStripBtnBorder);
            var b2 = new SolidBrush(ClsColor.ClrCheckBg);

            e.Graphics.FillRectangle(b, rect);
            e.Graphics.FillRectangle(b2, rect2);
            e.Graphics.DrawImage(e.Image, new Point(5, 3));
        }
        else
        {
            var rect = new Rectangle(3, 1, 20, 20);
            var rect2 = new Rectangle(4, 2, 18, 18);
            var b = new SolidBrush(ClsColor.ClrSelectedBgDropBorder);
            var b2 = new SolidBrush(ClsColor.ClrCheckBg);

            e.Graphics.FillRectangle(b, rect);
            e.Graphics.FillRectangle(b2, rect2);
            e.Graphics.DrawImage(e.Image, new Point(5, 3));
        }
    }

    // Render separator
    protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
    {
        base.OnRenderSeparator(e);

        var darkLine = new SolidBrush(ClsColor.ClrImageMarginLine);
        var whiteLine = new SolidBrush(Color.White);
        var rect = new Rectangle(32, 3, e.Item.Width - 32, 1);
        e.Graphics.FillRectangle(darkLine, rect);
        e.Graphics.FillRectangle(whiteLine, rect);
    }

    // Render arrow
    protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
    {
        e.ArrowColor = Color.Black;
        base.OnRenderArrow(e);
    }

    // Render  MenuItem background: light blue if selected, dark blue if dropped down
    protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
    {
        base.OnRenderMenuItemBackground(e);

        if (!e.Item.Enabled) return;
        switch (e.Item.IsOnDropDown)
        {
            case false when e.Item.Selected:
                {
                    // If item is MenuHeader and selected: draw dark blue color
                    var rect = new Rectangle(3, 2, e.Item.Width - 6, e.Item.Height - 4);
                    using var b = new LinearGradientBrush(rect, ClsColor.ClrSelectedBgWhite,
                        ClsColor.ClrSelectedBgHeaderBlue, LinearGradientMode.Vertical);
                    using var b2 = new SolidBrush(ClsColor.ClrCheckBg);
                    e.Graphics.FillRectangle(b, rect);
                    ClsColor.DrawRoundedRectangle(e.Graphics, rect.Left - 1, rect.Top - 1, rect.Width, rect.Height + 1, 4,
                        ClsColor.ClrToolStripBtnBorder);
                    ClsColor.DrawRoundedRectangle(e.Graphics, rect.Left - 2, rect.Top - 2, rect.Width + 2, rect.Height + 3,
                        4, Color.White);
                    e.Item.ForeColor = Color.Black;
                    break;
                }
            case true when e.Item.Selected:
                {
                    // If item is NOT menu header (but sub item); and selected: draw light blue border

                    var rect = new Rectangle(4, 2, e.Item.Width - 6, e.Item.Height - 4);
                    using var b = new LinearGradientBrush(rect, ClsColor.ClrSelectedBgWhite, ClsColor.ClrSelectedBgBlue,
                        LinearGradientMode.Vertical);
                    using var b2 = new SolidBrush(ClsColor.ClrSelectedBgBorder);
                    e.Graphics.FillRectangle(b, rect);
                    ClsColor.DrawRoundedRectangle(e.Graphics, rect.Left - 1, rect.Top - 1, rect.Width, rect.Height + 1, 6,
                        ClsColor.ClrSelectedBgBorder);
                    e.Item.ForeColor = Color.Black;
                    break;
                }
        }

        // If item is MenuHeader and menu is dropped down; selection rectangle is now
        // darker
        if (!((ToolStripMenuItem)e.Item).DropDown.Visible || e.Item.IsOnDropDown) return;
        {
            // (e.Item as ToolStripMenuItem).OwnerItem == null
            var rect = new Rectangle(3, 2, e.Item.Width - 6, e.Item.Height - 4);
            using var b = new LinearGradientBrush(rect, Color.White, ClsColor.ClrSelectedBgDropBlue,
                LinearGradientMode.Vertical);
            using var b2 = new SolidBrush(ClsColor.ClrSelectedBgDropBorder);
            e.Graphics.FillRectangle(b, rect);
            ClsColor.DrawRoundedRectangle(e.Graphics, rect.Left - 1, rect.Top - 1, rect.Width, rect.Height + 1, 4,
                ClsColor.ClrSelectedBgDropBorder);
            ClsColor.DrawRoundedRectangle(e.Graphics, rect.Left - 2, rect.Top - 2, rect.Width + 2, rect.Height + 3, 4,
                Color.White);
            e.Item.ForeColor = Color.Black;
        }

    }
}
