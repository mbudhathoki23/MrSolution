using System;
using System.Drawing;

namespace MrDAL.Control.ControlsEx.MenuStripCustom;

public class ClsColor
{
    public static Color ClrHorBgGrayBlue = Color.FromArgb(255, 233, 236, 250);
    public static Color ClrHorBgWhite = Color.FromArgb(255, 244, 247, 252);
    public static Color ClrSubmenuBg = Color.FromArgb(255, 240, 240, 240);
    public static Color ClrImageMarginBlue = Color.FromArgb(255, 212, 216, 230);
    public static Color ClrImageMarginWhite = Color.FromArgb(255, 244, 247, 252);
    public static Color ClrImageMarginLine = Color.FromArgb(255, 160, 160, 180);
    public static Color ClrSelectedBgBlue = Color.FromArgb(255, 186, 228, 246);
    public static Color ClrSelectedBgHeaderBlue = Color.FromArgb(255, 146, 202, 230);
    public static Color ClrSelectedBgWhite = Color.FromArgb(255, 241, 248, 251);
    public static Color ClrSelectedBgBorder = Color.FromArgb(255, 150, 217, 249);
    public static Color ClrSelectedBgDropBlue = Color.FromArgb(255, 139, 195, 225);
    public static Color ClrSelectedBgDropBorder = Color.FromArgb(255, 48, 127, 177);
    public static Color ClrMenuBorder = Color.FromArgb(255, 160, 160, 160);
    public static Color ClrCheckBg = Color.FromArgb(255, 206, 237, 250);

    public static Color ClrVerBgGrayBlue = Color.FromArgb(255, 196, 203, 219);
    public static Color ClrVerBgWhite = Color.FromArgb(255, 250, 250, 253);
    public static Color ClrVerBgShadow = Color.FromArgb(255, 181, 190, 206);

    public static Color ClrToolStripBtnGradBlue = Color.FromArgb(255, 129, 192, 224);
    public static Color ClrToolStripBtnGradWhite = Color.FromArgb(255, 237, 248, 253);
    public static Color ClrToolStripBtnBorder = Color.FromArgb(255, 41, 153, 255);
    public static Color ClrToolStripBtnGradBluePressed = Color.FromArgb(255, 124, 177, 204);
    public static Color ClrToolStripBtnGradWhitePressed = Color.FromArgb(255, 228, 245, 252);

    public static void DrawRoundedRectangle(Graphics g, int x, int y,
        int width, int height, int mDiameter, Color color)
    {
        using var pen = new Pen(color);
        //Dim g As Graphics
        var baseRect = new RectangleF(x, y, width, height);
        var arcRect = new RectangleF(baseRect.Location, new SizeF(mDiameter, mDiameter));
        //top left Arc
        g.DrawArc(pen, arcRect, 180, 90);
        g.DrawLine(pen, x + Convert.ToInt32(mDiameter / 2), y, x + width - Convert.ToInt32(mDiameter / 2), y);

        // top right arc
        arcRect.X = baseRect.Right - mDiameter;
        g.DrawArc(pen, arcRect, 270, 90);
        g.DrawLine(pen, x + width, y + Convert.ToInt32(mDiameter / 2), x + width,
            y + height - Convert.ToInt32(mDiameter / 2));

        // bottom right arc
        arcRect.Y = baseRect.Bottom - mDiameter;
        g.DrawArc(pen, arcRect, 0, 90);
        g.DrawLine(pen, x + Convert.ToInt32(mDiameter / 2), y + height, x + width - Convert.ToInt32(mDiameter / 2),
            y + height);

        // bottom left arc
        arcRect.X = baseRect.Left;
        g.DrawArc(pen, arcRect, 90, 90);
        g.DrawLine(pen, x, y + Convert.ToInt32(mDiameter / 2), x, y + height - Convert.ToInt32(mDiameter / 2));
    }
}