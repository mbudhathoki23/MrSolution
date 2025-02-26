using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Utility.Config;
using MrDAL.Utility.Interface;
using System;
using System.Data;
using System.Windows.Forms;

namespace MrBLL.SystemSetting;

public partial class FrmIrdApiConfig : MrForm
{
    public FrmIrdApiConfig()
    {
        InitializeComponent();
    }

    private void FrmIrdApiConfig_Load(object sender, EventArgs e)
    {
        FillIrdConfigure();
        TxtApiAddress.Focus();
    }

    private void FrmIrdApiConfig_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            BtnCancel.PerformClick();
        }
        else if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{Tab}");
        }
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (TxtApiAddress.IsBlankOrEmpty())
        {
            CustomMessageBox.Warning("API ADDRESS REQUIRED FOR IRD SYNC");
            TxtApiAddress.Focus();
            return;
        }

        if (TxtUserName.IsBlankOrEmpty())
        {
            CustomMessageBox.Warning("API USER NAME REQUIRED FOR IRD SYNC");
            TxtUserName.Focus();
            return;
        }

        if (TxtPassword.IsBlankOrEmpty())
        {
            CustomMessageBox.Warning("API USER PASSWORD REQUIRED FOR IRD SYNC");
            TxtPassword.Focus();
            return;
        }

        if (TxtPanNo.IsBlankOrEmpty())
        {
            CustomMessageBox.Warning("CUSTOMER PAN NO REQUIRED FOR IRD SYNC");
            TxtPanNo.Focus();
            return;
        }
        _setup.VmIrd.IRDAPI = TxtApiAddress.Text;
        _setup.VmIrd.IrdUser = TxtUserName.Text;
        _setup.VmIrd.IrdUserPassword = TxtPassword.Text;
        _setup.VmIrd.IrdCompanyPan = TxtPanNo.Text;
        _setup.VmIrd.IsIRDRegister = ChkOnlineSync.Checked ? 1 : 0;
        var result = _setup.SaveIrdApiSetting();
        if (result == 0)
        {
            return;
        }
        if (TxtLocation.IsValueExits())
        {
            _setup.GenerateSqlAuditLog(TxtLocation.Text);
        }
        CustomMessageBox.ActionSuccess("API", "CONFIGURATION", "SAVE");
        Close();
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
        {
            Close();
        }
    }

    // METHOD FOR THIS FORM
    internal void FillIrdConfigure()
    {
        var dtIrdConfig = "SELECT * FROM AMS.IRDAPISetting i".GetQueryDataTable();
        if (dtIrdConfig is { Rows.Count: > 0 })
        {
            foreach (DataRow dr in dtIrdConfig.Rows)
            {
                var msg = "API SAVE IN DATABASE & SOFTWARE IS DIFFRENCE.. DO YOU WANT TO UPDATE..??";
                var question = CustomMessageBox.Question(msg);
                if (question == DialogResult.Yes)
                {
                    TxtApiAddress.Text = ObjGlobal.IrdApiAddress;
                    TxtUserName.Text = ObjGlobal.IrdUser;
                    TxtPassword.Text = ObjGlobal.IrdUserPassword;
                    TxtPanNo.Text = ObjGlobal.IrdCompanyPan;
                    ChkOnlineSync.Checked = true;
                    break;
                }
                TxtApiAddress.Text = dr["IRDAPI"].GetString();
                TxtUserName.Text = dr["IrdUser"].GetString();
                TxtPassword.Text = dr["IrdUserPassword"].GetString();
                TxtPanNo.Text = dr["IrdCompanyPan"].GetString();
                ChkOnlineSync.Checked = dr["IsIRDRegister"].GetInt() > 0;
            }
        }
        else
        {
            TxtApiAddress.Text = ObjGlobal.IrdApiAddress;
            TxtUserName.Text = ObjGlobal.IrdUser;
            TxtPassword.Text = ObjGlobal.IrdUserPassword;
            TxtPanNo.Text = ObjGlobal.IrdCompanyPan;
            ChkOnlineSync.Checked = true;
        }
    }

    // OBJECT FOR THIS FORM
    private readonly ISetup _setup = new ClsSetup();

    private void BtnLocation_Click(object sender, EventArgs e)
    {
        if (SaveLocation.ShowDialog() == DialogResult.OK)
        {
            TxtLocation.Text = SaveLocation.SelectedPath;
        }
    }
}