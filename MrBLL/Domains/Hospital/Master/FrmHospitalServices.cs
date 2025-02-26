using DevExpress.XtraEditors;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MrBLL.Domains.Hospital.Master;

public partial class FrmHospitalServices : MrForm
{
    public FrmHospitalServices(bool isZoom)
    {
        InitializeComponent();
    }

    private void FrmItem_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void FrmItem_Load(object sender, EventArgs e)
    {
        ObjGlobal.GetFormAccessControl([btnNew, btnEdit, btnDelete], this.Tag);
    }

    // OBJECT
    public string ProductDesc { get; set; }

    public long ProductId { get; set; }
}