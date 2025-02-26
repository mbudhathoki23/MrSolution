using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace MrDAL.Control.ControlsEx.GridControl;

public class GridInfoRegistrator : DevExpress.XtraGrid.Registrator.GridInfoRegistrator
{
    public override string ViewName => "CustomGridView";

    public override BaseViewPainter CreatePainter(BaseView view)
    {
        return new CustomGridPainter(view as GridView);
    }

    public override BaseView CreateView(DevExpress.XtraGrid.GridControl grid)
    {
        var view = new CustomGridView();
        view.SetGridControlAccessMethod(grid);
        return view;
    }
}