using DevExpress.XtraEditors;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MrBLL.Domains.Hospital.Master;

public partial class FrmBedType : MrForm
{
    // OBJECT FOR THIS FORM
    private string actionTag;

    public string BedTypeDesc { get; set; }
    public int BedTypeId { get; set; }

    #region --------------- Bed Type ---------------

    public FrmBedType(bool isZoom)
    {
        InitializeComponent();
        ControlEnable();
        ControlClear();
    }

    private void FrmBedType_Load(object sender, EventArgs e)
    {
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }

    private void FrmBedType_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    #endregion --------------- Bed Type ---------------

    #region --------------- Method ---------------

    internal void ControlEnable(bool isEnable = false)
    {
    }

    internal void ControlClear()
    {
        TxtDescription.Clear();
        txtShortName.Clear();
        TxtDescription.Focus();
    }

    internal int SaveBedType()
    {
        return 0;
    }

    internal void SetGridDataToText()
    {
    }

    #endregion --------------- Method ---------------
}