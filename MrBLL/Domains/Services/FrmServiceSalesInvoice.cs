using DevExpress.XtraEditors;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Utility.Interface;
using MrDAL.Utility.PickList;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MrBLL.Domains.Services;

public partial class FrmServiceSalesInvoice : DevExpress.XtraEditors.XtraForm
{
    public FrmServiceSalesInvoice()
    {
        InitializeComponent();
        _list = new ClsPickList();
        GridColumnSetup();
        BindCustomerList();
        EnableControl();
    }

    private void FrmServiceSalesInvoice_Load(object sender, EventArgs e)
    {
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }

    private void GridColumnSetup()
    {
        foreach (DataGridViewColumn column in RGrid.Columns)
        {
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }
    }

    private void BindCustomerList()
    {
        RGrid.AutoGenerateColumns = false;
        var dt = _list.GetGeneralLedgerList("SAVE", "CUSTOMER", DateTime.Now.GetDateString(), true);
        if (dt.Rows.Count > 0)
        {
            RGrid.DataSource = dt;
        }
    }

    private void EnableControl(bool isEnable = false)
    {
        SalesInvoice.Enabled = isEnable;
        if (SalesInvoice.Enabled)
        {
        }
    }

    private IPickList _list;
}