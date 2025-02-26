using DevExpress.XtraEditors;
using MrDAL.Utility.PickList;
using System;

namespace MrBLL.Domains.VehicleManagement.UserInterface;

public partial class uProduct : XtraUserControl
{
    // OBJECT FOR THIS FORM
    private readonly ClsPickList _objPickList = new();

    public uProduct()
    {
        InitializeComponent();
    }

    private void uProduct_Load(object sender, EventArgs e)
    {
        BindData();
    }

    // METHOD FOR THIS FORM
    internal void BindData()
    {
        var dtTable = _objPickList.GetProductWithQty("ACTION", DateTime.Now.ToString("yyyy-MM-dd"));
        RGrid.DataSource = dtTable;
    }
}