using System;
using System.Drawing;
using System.Windows.Forms;

namespace PrintControl.PrintClass.CustomClass;

/// <summary>
///     Class for the ownerdraw event. Provide the caller with the cell data, the current
///     graphics context and the location in which to draw the cell.
/// </summary>
public class DGVCellDrawingEventArgs : EventArgs
{
    public DataGridViewCellStyle CellStyle;
    public int column;
    public RectangleF DrawingBounds;
    public Graphics g;
    public bool Handled;
    public int row;

    public DGVCellDrawingEventArgs(Graphics g, RectangleF bounds, DataGridViewCellStyle style,
        int row, int column)
    {
        this.g = g;
        DrawingBounds = bounds;
        CellStyle = style;
        this.row = row;
        this.column = column;
        Handled = false;
    }
}