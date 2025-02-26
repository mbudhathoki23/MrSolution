using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using System;

namespace MrBLL.DataEntry.ProductionMaster;

public partial class FrmFinishedGoodsReturn : MrForm
{
    public FrmFinishedGoodsReturn(bool Zoom, string TxtZoomVno)
    {
        InitializeComponent();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
    }

    private void FrmFinishedGoodsReturn_Load(object sender, EventArgs e)
    {
        ObjGlobal.GetFormAccessControl([btnNew, btnEdit, btnDelete], this.Tag);
    }
}