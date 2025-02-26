using DatabaseModule.CloudSync;
using DatabaseModule.Master.FinanceSetup;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx;
using MrDAL.Control.SplashScreen;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.FinanceSetup;
using MrDAL.Master.Interface;
using MrDAL.Master.Interface.FinanceSetup;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Master.LedgerSetup;

public partial class FrmDepartmentSetup : MrForm
{
    private void CmbLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void BtnView_Click(object sender, EventArgs e)
    {
        BtnDescription_Click(sender, e);
        BtnView.Focus();
    }



    #region --------------- Frm ---------------

    public FrmDepartmentSetup(bool _ZoomFrm = false, string txt = "DEPARTMENT SETUP")
    {
        InitializeComponent();
        _txt = txt;
        _IsZoom = _ZoomFrm;
        _actionTag = string.Empty;
        clsFormControl = new ClsMasterForm(this, BtnExit);
        _departmentRepository = new DepartmentRepository();
        _injectData = new DbSyncRepoInjectData();
    }

    private void DepartmentName_Load(object sender, EventArgs e)
    {
        ClearControl();
        EnableControl();
        CmbLevel.SelectedIndex = 0;
        BtnNew.Focus();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedDepartments);
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete, BtnView], this.Tag);
    }

    private void FrmDepartmentName_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is not (char)Keys.Escape) return;
        if (!BtnNew.Enabled)
        {
            if (MessageBox.Show(@"ARE YOU SURE WANT TO CLEAR FORM..??", ObjGlobal.Caption,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            _actionTag = string.Empty;
            ClearControl();
            EnableControl();
            BtnNew.Focus();
        }
        else
        {
            BtnExit.PerformClick();
        }
    }

    private void Global_KeyPress(object sender, KeyPressEventArgs e)
    {

    }

    #endregion --------------- Frm ---------------

    #region --------------- Botton ---------------

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        ClearControl();
        EnableControl(true, false);
        TxtDescription.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _actionTag = "UPDATE";
        ClearControl();
        EnableControl(true, false);
        TxtDescription.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        ClearControl();
        EnableControl(false, false);
        TxtDescription.Focus();
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
        {
            Close();
        }
    }

    private bool IsValidForm()
    {
        if (_actionTag.IsBlankOrEmpty())
        {
            return false;
        }

        if (TxtDescription.IsBlankOrEmpty())
        {
            TxtDescription.WarningMessage(@"DEPARTMENT NAME IS REQUIRED..!");
            return false;
        }

        if (TxtShortName.IsBlankOrEmpty())
        {
            TxtShortName.WarningMessage(@"DEPARTMENT SHORTNAME IS REQUIRED..!!");
            return false;
        }

        return true;
    }


    private void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValidForm())
            {
                if (SaveDepartmentDetails() > 0)
                {
                    if (_IsZoom is true)
                    {
                        DepartmentDesc = TxtDescription.Text.Trim();
                        Close();
                        return;
                    }

                    MessageBox.Show($@"DATA {_actionTag.ToUpper()}  SUCCESSFULLY..!!", ObjGlobal.Caption,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearControl();
                    TxtDescription.Focus();
                }
            }
            else
            {
                MessageBox.Show($@"ERROR OCCUR WHILE {_actionTag.ToUpper()} DATA..!!", ObjGlobal.Caption,
                    MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                TxtDescription.Focus();
            }
        }
        catch
        {
            MessageBox.Show(
                "'" + TxtDescription.Text + "' mignt be in use. \n So unable to '" + _actionTag + "'...!!",
                MessageBoxIcon.Error.ToString(), MessageBoxButtons.OK);
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtDescription.Text.Trim())) BtnExit.PerformClick();
        else ClearControl();
    }

    private void BtnSync_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedDepartments);
        }
        else
        {
            CustomMessageBox.Warning("PLEASE CONFIG YOUR CLOUD BACK UP SYNC..!!");
        }
    }

    #endregion --------------- Botton ---------------

    #region --------------- Event ---------------

    private void TxtDescription_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnDescription_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.U)
        {
            TxtDescription.Text = TxtDescription.GetUpper();
        }
        else if (e.Control && e.KeyCode is Keys.U)
        {
            TxtDescription.Text = TxtDescription.GetProperCase();
        }
        else if (e.KeyCode == Keys.Enter)
        {
            if (TxtDescription.IsBlankOrEmpty())
            {
                TxtDescription.WarningMessage("DEPARTMENT DESCRIPTION IS BLANK..!!");
                return;
            }
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
        else
        {
            if (TxtDescription.ReadOnly)
            {
                ClsKeyPreview.KeyEvent(e, "DELETE", TxtDescription, BtnDescription);
            }
        }
    }

    private void BtnDescription_Click(object sender, EventArgs e)
    {
        (TxtDescription.Text, DepartmentId) = GetMasterList.GetDepartmentList(_actionTag);
        if (!_actionTag.Equals("SAVE"))
        {
            SetGridDataToText(DepartmentId);
            TxtDescription.ReadOnly = !_actionTag.Equals("UPDATE");
        }
        TxtDescription.Focus();
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtDescription.Text.Trim().Replace("'", "''")) && _actionTag.ToUpper() is "SAVE")
            TxtShortName.Text = string.IsNullOrEmpty(TxtShortName.Text.Replace("'", "''"))
                ? ObjGlobal.BindAutoIncrementCode("DEP", TxtDescription.Text.Trim().Replace("'", "''"))
                : TxtShortName.Text.Replace("'", "''");
        if (!string.IsNullOrEmpty(_actionTag) && _actionTag is not "DELETE")
        {
            using var dt = _departmentRepository.CheckIsValidData(_actionTag, "Department", "DName", "DId",
                TxtDescription.Text.Replace("'", "''"), DepartmentId.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                MessageBox.Show(@"DESCRIPTION ALREADY EXITS..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                ClearControl();
                TxtDescription.Focus();
            }
        }
    }

    private void TxtDescription_Leave(object sender, EventArgs e)
    {
        if (ActiveControl != null && !string.IsNullOrEmpty(_actionTag) && TxtDescription.Focused &&
            string.IsNullOrEmpty(TxtDescription.Text.Trim().Replace("'", "''")))
        {
            MessageBox.Show(@"DESCRIPTION IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDescription.Focus();
        }
    }

    private void TxtShortName_Validating(object sender, CancelEventArgs e)
    {
        var dt = _departmentRepository.CheckIsValidData(_actionTag, "Department", "DCode", "DId",
            TxtShortName.Text.Replace("'", "''"), DepartmentId.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            MessageBox.Show(@"SHORTNAME ALREADY EXITS..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDescription.Focus();
        }
    }

    private void TxtShortName_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtShortName.Text.Trim().Replace("'", "''")) && TxtShortName.Focused is true &&
            !string.IsNullOrEmpty(_actionTag))
        {
            MessageBox.Show(@"GROUP SHORTNAME IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtShortName.Focus();
        }
        Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
    }

    private void cbLevel_Validating(object sender, CancelEventArgs e)
    {
    }

    #endregion --------------- Event ---------------

    #region --------------- Method ---------------

    private void EnableControl(bool Txt = false, bool Btn = true)
    {
        TxtDescription.Enabled = BtnCancel.Enabled =
            BtnSave.Enabled = !string.IsNullOrEmpty(_actionTag) && _actionTag == "DELETE" || Txt;
        TxtShortName.Enabled = Txt;
        CmbLevel.Enabled = Txt;
        ChkActive.Enabled = !string.IsNullOrEmpty(_actionTag) && _actionTag is "UPDATE" ? true : false;
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = Btn;
    }

    private void ClearControl()
    {
        Text = _actionTag.IsValueExits() ? $"DEPARTMENT SETUP [{_actionTag}]" : "DEPARTMENT SETUP";
        DepartmentId = 0;
        TxtDescription.Clear();
        TxtShortName.Clear();
        ChkActive.Checked = true;
        TxtDescription.ReadOnly = _actionTag.ToUpper() != "SAVE";
    }

    private void SetGridDataToText(int DepartmentId)
    {
        var dt = _departmentRepository.GetMasterDepartment(_actionTag, DepartmentId);
        if (dt != null && dt.Rows.Count > 0)
        {
            int.TryParse(dt.Rows[0]["DId"].ToString(), out var _DepId);
            DepartmentId = _DepId;
            TxtDescription.Text = dt.Rows[0]["DName"].ToString();
            TxtShortName.Text = dt.Rows[0]["Dcode"].ToString();
            switch (dt.Rows[0]["Dlevel"].ToString())
            {
                case "1":
                {
                    CmbLevel.Text = "I";
                    break;
                }

                case "2":
                {
                    CmbLevel.Text = "II";
                    break;
                }

                case "3":
                {
                    CmbLevel.Text = "III";
                    break;
                }

                case "4":
                {
                    CmbLevel.Text = "IV";
                    break;
                }
            }

            bool.TryParse(dt.Rows[0]["Status"].ToString(), out var _Check);
            ChkActive.Checked = _Check;
            if (_actionTag == "UPDATE")
                TxtDescription.Focus();
            else
                BtnSave.Focus();
        }
    }

    private int SaveDepartmentDetails()
    {
        DepartmentId = _actionTag is "SAVE"
            ? ObjGlobal.ReturnInt(ClsMasterSetup.ReturnMaxIntValue("AMS.Department", "DId").ToString())
            : DepartmentId;
        _departmentRepository.ObjDepartment.DId = DepartmentId;
        _departmentRepository.ObjDepartment.DName = TxtDescription.Text.Trim().Replace("'", "''");
        _departmentRepository.ObjDepartment.DCode = TxtShortName.Text.Trim().Replace("'", "''");
        _departmentRepository.ObjDepartment.Dlevel = (CmbLevel.SelectedIndex + 1).ToString();
        _departmentRepository.ObjDepartment.Branch_ID = ObjGlobal.SysBranchId;
        _departmentRepository.ObjDepartment.Status = ChkActive.Checked;
        _departmentRepository.ObjDepartment.EnterDate = DateTime.Now;
        _departmentRepository.ObjDepartment.EnterBy = ObjGlobal.LogInUser;
        _departmentRepository.ObjDepartment.SyncRowVersion = (short)(_actionTag is "UPDATE"
            ? ClsMasterSetup.ReturnMaxSyncBaseIdValue("AMS.Department", "SyncRowVersion", "DId",
                DepartmentId.ToString())
            : 1);

        return _departmentRepository.SaveDepartment(_actionTag);
    }

    private async void GetAndSaveUnSynchronizedDepartments()
    {
        _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
        if (!_configParams.Success || _configParams.Model.Item2 == null)
        {
            return;
        }
        var apiConfig = new SyncApiConfig
        {
            BaseUrl = _configParams.Model.Item2,
            Apikey = _configParams.Model.Item3,
            Username = ObjGlobal.LogInUser,
            BranchId = ObjGlobal.SysBranchId,
            GetUrl = @$"{_configParams.Model.Item2}Department/GetDepartmentsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}Department/InsertDepartmentList",
            UpdateUrl = @$"{_configParams.Model.Item2}Department/UpdateDepartment"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var departmentRepo = DataSyncProviderFactory.GetRepository<Department>(_injectData);

        SplashScreenManager.ShowForm(typeof(PleaseWait));
        // pull all new department data
        var pullResponse = await _departmentRepository.PullDepartmentsServerToClientByRowCounts(departmentRepo, 1);
        SplashScreenManager.CloseForm();

        // push all new department data
        var sqlDpQuery = _departmentRepository.GetDepartmentScript();
        var queryResponse = await QueryUtils.GetListAsync<Department>(sqlDpQuery);
        var dpList = queryResponse.List.ToList();
        if (dpList.Count > 0)
        {
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            var pushResponse = await departmentRepo.PushNewListAsync(dpList);
            SplashScreenManager.CloseForm();

        }
    }

    #endregion --------------- Method ---------------


    //OBJECT FOR THIS FORM
    private readonly IDepartmentRepository _departmentRepository;
    public int DepartmentId;
    public string DepartmentDesc = string.Empty;
    public string DepartmentName = string.Empty;
    private string Query = string.Empty;
    private string _SearchKey = string.Empty;
    private string _actionTag = string.Empty;
    private string _txt = "DEPARTMENT SETUP";
    private readonly bool _IsZoom;

    private IMasterSetup _setup;
    private ClsMasterForm clsFormControl;

    private InfoResult<ValueModel<string, string, Guid>> _configParams;
    private DbSyncRepoInjectData _injectData;
}