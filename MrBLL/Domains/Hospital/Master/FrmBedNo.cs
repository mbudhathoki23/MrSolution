using DevExpress.XtraEditors;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MrBLL.Domains.Hospital.Master;

public partial class FrmBedNo : MrForm
{
    // OBJECT FOR THIS FORM
    private string actionTag;

    public string BedNoDesc { get; set; }
    public int BedNoId { get; set; }

    #region --------------- Frm ---------------

    public FrmBedNo(bool isZoom)
    {
        InitializeComponent();
    }

    private void FrmBedNo_Load(object sender, EventArgs e)
    {
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }

    private void FrmBedNo_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    #endregion --------------- Frm ---------------

    #region --------------- Method ---------------

    private void ControlEnable(bool isEnable = false)
    {
    }

    private void ControlClear()
    {
    }

    private void BindDepartmentDetails()
    {
    }

    private void SetGridDataToText()
    {
    }

    private int SaveDepartmentDetails()
    {
        return 0;
    }

    #endregion --------------- Method ---------------
}