using DevExpress.XtraEditors;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MrBLL.Domains.Hospital.Master;

public partial class FrmDoctor : MrForm
{
    #region --------------- Form ---------------

    public FrmDoctor(bool isZoom)
    {
        InitializeComponent();
    }

    private void FrmDoctor_Load(object sender, EventArgs e)
    {
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }

    private void FrmDoctor_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    #endregion --------------- Form ---------------

    #region --------------- Method ---------------

    internal void ControlEnable(bool isEnable = false)
    {
    }

    internal void ControlClear()
    {
    }

    internal int SaveDoctorSetup()
    {
        return 0;
    }

    internal void SetGridDataToText()
    {
    }

    internal bool ChkFormValid()
    {
        if (string.IsNullOrEmpty(TxtDescription.Text.Trim()))
        {
            MessageBox.Show(@"DESCRIPTION IS REQUIRED..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtDescription.Focus();
            return false;
        }

        if (string.IsNullOrEmpty(TxtShortName.Text.Trim()))
        {
            MessageBox.Show(@"SHORTNAME IS REQUIRED..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtShortName.Focus();
            return false;
        }

        return true;
    }

    #endregion --------------- Method ---------------

    private void TxtShortName_TextChanged(object sender, EventArgs e)
    {

    }
}