using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Base;

namespace MrDAL.Control.ControlsEx.GridControl;

public class CustomGridControl : DevExpress.XtraGrid.GridControl
{
    protected override void RegisterAvailableViewsCore(InfoCollection collection)
    {
        base.RegisterAvailableViewsCore(collection);
        collection.Add(new GridInfoRegistrator());
    }

    protected override BaseView CreateDefaultView()
    {
        return CreateView("CustomGridView");
    }
}