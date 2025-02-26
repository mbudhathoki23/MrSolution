using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace MrDAL.Control.ControlsEx.GridControl;

public class CustomGridPainter : GridPainter
{
    public CustomGridPainter(GridView view) : base(view)
    {
    }

    public new virtual CustomGridView View => (CustomGridView)base.View;

    protected override void DrawRowCell(GridViewDrawArgs e, GridCellInfo cell)
    {
        cell.ViewInfo.MatchedStringUseContains = true;
        cell.ViewInfo.MatchedString = View.GetExtraFilterText;
        cell.State = GridRowCellState.Dirty;
        e.ViewInfo.UpdateCellAppearance(cell);
        base.DrawRowCell(e, cell);
    }
}