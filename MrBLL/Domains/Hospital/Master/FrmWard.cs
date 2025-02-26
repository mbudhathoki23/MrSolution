using DevExpress.XtraEditors;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MrBLL.Domains.Hospital.Master;

public partial class FrmWard : MrForm
{
    // METHOD FOR THIS FORM
    private string actionTag;

    public string WardDesc { get; set; }
    public int WardId { get; set; }

    #region --------------- Form ---------------

    public FrmWard(bool zoom)
    {
        InitializeComponent();
        EnableDisable();
        ClearControl();
    }

    private void FrmWard_Load(object sender, EventArgs e)
    {
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }

    private void FrmWard_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    #endregion --------------- Form ---------------

    #region --------------- Method ---------------

    internal void EnableDisable(bool isEnable = false)
    {
        TxtDescription.Enabled = isEnable;
        TxtShortName.Enabled = isEnable;
        TxtDepartment.Enabled = isEnable;
        TxtRate.Enabled = isEnable;
        ChkActive.Enabled = actionTag.Equals("UPDATE");
        BtnSave.Enabled = isEnable || actionTag.Equals("DELETE");
        BtnCancel.Enabled = BtnSave.Enabled;
    }

    internal void ClearControl()
    {
        TxtDescription.Text = string.Empty;
        TxtShortName.Text = string.Empty;
        TxtRate.Text = string.Empty;
        TxtDepartment.Text = string.Empty;
        TxtDescription.Focus();
    }

    internal void SetGridDataToText()
    {
    }

    internal void SaveDepartmentDetails()
    {
    }

    #endregion --------------- Method ---------------
}