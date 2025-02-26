using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace MrDAL.Utility.GridControl;

public class StackedHeaderDecorator
{
    private readonly DataGridView objDataGrid;
    private readonly StringFormat objFormat;
    private readonly IStackedHeaderGenerator objStackedHeaderGenerator = StackedHeaderGenerator.Instance;
    private int iNoOfLevels;
    private Graphics objGraphics;
    private Header objHeaderTree;

    public StackedHeaderDecorator(DataGridView objDataGrid)
    {
        this.objDataGrid = objDataGrid;
        objFormat = new StringFormat();
        objFormat.Alignment = StringAlignment.Center;
        objFormat.LineAlignment = StringAlignment.Center;

        var dgvType = objDataGrid.GetType();
        var pi = dgvType.GetProperty("DoubleBuffered",
            BindingFlags.Instance | BindingFlags.NonPublic);
        pi.SetValue(objDataGrid, true, null);

        objDataGrid.Scroll += objDataGrid_Scroll;
        objDataGrid.Paint += objDataGrid_Paint;
        objDataGrid.ColumnRemoved += objDataGrid_ColumnRemoved;
        objDataGrid.ColumnAdded += objDataGrid_ColumnAdded;
        objDataGrid.ColumnWidthChanged += objDataGrid_ColumnWidthChanged;
        objHeaderTree = objStackedHeaderGenerator.GenerateStackedHeader(objDataGrid);
    }

    public StackedHeaderDecorator(IStackedHeaderGenerator objStackedHeaderGenerator, DataGridView objDataGrid)
        : this(objDataGrid)
    {
        this.objStackedHeaderGenerator = objStackedHeaderGenerator;
    }

    private void objDataGrid_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
    {
        Refresh();
    }

    private void objDataGrid_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
    {
        RegenerateHeaders();
        Refresh();
    }

    private void objDataGrid_ColumnRemoved(object sender, DataGridViewColumnEventArgs e)
    {
        RegenerateHeaders();
        Refresh();
    }

    private void objDataGrid_Paint(object sender, PaintEventArgs e)
    {
        iNoOfLevels = NoOfLevels(objHeaderTree);
        objGraphics = e.Graphics;
        objDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        objDataGrid.ColumnHeadersHeight = iNoOfLevels * 20;
        if (null != objHeaderTree) RenderColumnHeaders();
    }

    private void objDataGrid_Scroll(object sender, ScrollEventArgs e)
    {
        Refresh();
    }

    private void Refresh()
    {
        var rtHeader = objDataGrid.DisplayRectangle;
        objDataGrid.Invalidate(rtHeader);
    }

    private void RegenerateHeaders()
    {
        objHeaderTree = objStackedHeaderGenerator.GenerateStackedHeader(objDataGrid);
    }

    private void RenderColumnHeaders()
    {
        objGraphics.FillRectangle(new SolidBrush(objDataGrid.ColumnHeadersDefaultCellStyle.BackColor),
            new Rectangle(objDataGrid.DisplayRectangle.X, objDataGrid.DisplayRectangle.Y,
                objDataGrid.DisplayRectangle.Width, objDataGrid.ColumnHeadersHeight));

        foreach (var objChild in objHeaderTree.Children)
        {
            objChild.Measure(objDataGrid, 0, objDataGrid.ColumnHeadersHeight / iNoOfLevels);
            objChild.AcceptRenderer(this);
        }
    }

    public void Render(Header objHeader)
    {
        if (objHeader.Children.Count == 0)
        {
            var r1 = objDataGrid.GetColumnDisplayRectangle(objHeader.ColumnId, true);
            if (r1.Width == 0) return;
            r1.Y = objHeader.Y;
            r1.Width += 1;
            r1.X -= 1;
            r1.Height = objHeader.Height;
            objGraphics.SetClip(r1);

            if (r1.X + objDataGrid.Columns[objHeader.ColumnId].Width < objDataGrid.DisplayRectangle.Width)
                r1.X -= objDataGrid.Columns[objHeader.ColumnId].Width - r1.Width;
            r1.X -= 1;
            r1.Width = objDataGrid.Columns[objHeader.ColumnId].Width;
            objGraphics.DrawRectangle(Pens.Gray, r1);
            objGraphics.DrawString(objHeader.Name,
                objDataGrid.ColumnHeadersDefaultCellStyle.Font,
                new SolidBrush(objDataGrid.ColumnHeadersDefaultCellStyle.ForeColor),
                r1,
                objFormat);
            objGraphics.ResetClip();
        }
        else
        {
            var x = objDataGrid.RowHeadersWidth;
            for (var i = 0; i < objHeader.Children[0].ColumnId; ++i)
                if (objDataGrid.Columns[i].Visible)
                    x += objDataGrid.Columns[i].Width;
            if (x > objDataGrid.HorizontalScrollingOffset + objDataGrid.DisplayRectangle.Width - 5) return;

            //Rectangle r1 = objDataGrid.GetCellDisplayRectangle(objHeader.Children[0].ColumnId, -1, true);
            var r1 = objDataGrid.GetCellDisplayRectangle(objHeader.ColumnId, -1, true);
            r1.Y = objHeader.Y;
            r1.Height = objHeader.Height;
            r1.Width = objHeader.Width + 1;
            if (r1.X < objDataGrid.RowHeadersWidth) r1.X = objDataGrid.RowHeadersWidth;
            r1.X -= 1;
            objGraphics.SetClip(r1);
            r1.X = x - objDataGrid.HorizontalScrollingOffset;
            r1.Width -= 1;
            objGraphics.DrawRectangle(Pens.Gray, r1);
            r1.X -= 1;
            objGraphics.DrawString(objHeader.Name, objDataGrid.ColumnHeadersDefaultCellStyle.Font,
                new SolidBrush(objDataGrid.ColumnHeadersDefaultCellStyle.ForeColor),
                r1, objFormat);
            objGraphics.ResetClip();
        }
    }

    private int NoOfLevels(Header header)
    {
        var level = 0;
        foreach (var child in header.Children)
        {
            var temp = NoOfLevels(child);
            level = temp > level ? temp : level;
        }

        return level + 1;
    }
}