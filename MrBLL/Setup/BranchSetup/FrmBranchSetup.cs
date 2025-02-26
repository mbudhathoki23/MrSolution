using DatabaseModule.CloudSync;
using DatabaseModule.Setup.CompanyMaster;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.SplashScreen;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Domains.Shared.DataSync.Handlers;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.Dialogs;
using MrDAL.Master;
using MrDAL.Models.Common;
using MrDAL.Setup.CompanySetup;
using MrDAL.Setup.Interface;
using MrDAL.Utility.Server;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Setup.BranchSetup;

public partial class FrmBranchSetup : MrForm
{
    // BRANCH SETUP

    #region --------------- Form ---------------
    public FrmBranchSetup(bool zoom)
    {
        InitializeComponent();
        _isZoom = zoom;
        _branchSetup = new BranchSetupRepository();
        _injectData = new DbSyncRepoInjectData();
    }
    private void FrmBranchSetup_Load(object sender, EventArgs e)
    {
        ClearControl();
        ControlEnable();
        BtnNew.Focus();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedBranch);
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }
    private void FrmBranchSetup_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar != (char)27)
        {
            if (e.KeyChar is (char)Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
            return;
        }
        if (BtnNew.Enabled == false || BtnEdit.Enabled == false || BtnDelete.Enabled == false)
        {
            if (MessageBox.Show(@"ARE YOU SURE WANT TO CLEAR FORM..!!", ObjGlobal.Caption, MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes) return;
            _actionTag = string.Empty;
            ControlEnable();
            ClearControl();
            BtnNew.Focus();
        }
        else
        {
            BtnExit.PerformClick();
        }
    }
    private void BtnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        ControlEnable(true);
        ClearControl();
        TxtDescription.Focus();
    }
    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _actionTag = "UPDATE";
        ControlEnable(true);
        TxtDescription.ReadOnly = true;
        ClearControl();
        TxtDescription.Focus();
    }
    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        ControlEnable();
        TxtDescription.ReadOnly = true;
        ClearControl();
        TxtDescription.Focus();
    }
    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (!IsValidForm()) return;

        if (SaveBranch() > 0)
        {
            if (_isZoom)
            {
                BranchName = TxtDescription.Text;
                _branchId = ObjGlobal.ReturnInt(GetConnection.GetQueryData(
                    "SELECT Branch_ID FROM AMS.Branch WHERE Branch_Name = '" +
                    TxtDescription.Text.Replace("'", "''") + "' "));
                Close();
                return;
            }

            MessageBox.Show($@"{TxtDescription.Text.ToUpper()} BRANCH {_actionTag} SUCCESSFULLY..!!",
                ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearControl();
            TxtDescription.Focus();
        }
        else
        {
            MessageBox.Show($@"ERROR OCCURS WHILE BRANCH {_actionTag}..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDescription.Focus();
        }
    }
    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (TxtDescription.Text.Trim() == string.Empty)
        {
            BtnExit.PerformClick();
        }
        else
        {
            ClearControl();
            TxtDescription.Focus();
        }
    }
    private void BtnExit_Click(object sender, EventArgs e)
    {
        var result = CustomMessageBox.ExitActiveForm();
        if (result == DialogResult.Yes)
        {
            Close();
        }
    }
    private void BtnSync_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedBranch);
        }
        else
        {
            CustomMessageBox.Warning("PLEASE CONFIG YOUR CLOUD BACK UP SYNC..!!");
        }
    }
    private void TxtDescription_Leave(object sender, EventArgs e)
    {
    }
    private void TxtDescription_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnDescription_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtDescription.IsBlankOrEmpty())
            {
                TxtDescription.WarningMessage($"DESCRIPTION IS REQUIRED FOR {_actionTag}");
                return;
            }
        }
        else if (_actionTag != "SAVE" && TxtDescription.ReadOnly)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtDescription, BtnDescription);
        }
    }
    private void TxtDescription_Validated(object sender, EventArgs e)
    {
        if (_actionTag.IsValueExits() && TxtDescription.Enabled && TxtDescription.IsValueExits())
        {
            if (TxtDescription.IsValueExits() && _actionTag == "DELETE")
            {
                return;
            }

            var dtCheck = TxtDescription.IsDuplicate("Branch_Name", _branchId, _actionTag, "BRANCH");
            if (dtCheck)
            {
                TxtDescription.WarningMessage("DESCRIPTION IS ALREADY EXITS..!!");
                return;
            }
            else
            {
                TxtShortName.Text = TxtDescription.GenerateShortName("BRANCH", "Branch_Code");
            }
        }

        if (_actionTag.IsValueExits() && TxtDescription.IsBlankOrEmpty())
        {
            if (TxtDescription.ValidControl(ActiveControl))
            {
                TxtDescription.WarningMessage($"DESCRIPTION IS REQUIRED TO {_actionTag}");
                return;
            }
        }
    }
    private void BtnDescription_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetBranchList(_actionTag);
        if (id > 0)
        {
            TxtDescription.Text = description;
            _branchId = id;
            if (!_actionTag.Equals("SAVE"))
            {
                SetGridDataToText(_branchId);
                TxtDescription.ReadOnly = false;
            }
        }
        TxtDescription.Focus();
    }
    private void TxtShortName_Leave(object sender, EventArgs e)
    {
    }
    private void TxtShortName_Validated(object sender, EventArgs e)
    {
        if (_actionTag.IsValueExits() && TxtShortName.Enabled && TxtShortName.IsValueExits())
        {
            if (TxtShortName.IsValueExits() && _actionTag == "DELETE")
            {
                return;
            }

            var dtCheck = TxtShortName.IsDuplicate("Branch_Code", _branchId, _actionTag, "BRANCH");
            if (dtCheck)
            {
                TxtShortName.WarningMessage("DESCRIPTION SHORT NAME IS ALREADY EXITS..!!");
                return;
            }
        }

        if (_actionTag.IsValueExits() && TxtDescription.IsBlankOrEmpty())
        {
            if (TxtShortName.ValidControl(ActiveControl))
            {
                TxtDescription.WarningMessage($"DESCRIPTION SHORT NAME IS REQUIRED TO {_actionTag}");
                return;
            }
        }
    }
    private void MskRegDate_Leave(object sender, EventArgs e)
    {
        if (ActiveControl != null && MskRegDate.Text.Length is 0 &&
            MskRegDate.Focused & !string.IsNullOrWhiteSpace(_actionTag))
        {
            MessageBox.Show(@"BRANCH SHORT NAME IS ALREADY EXITS..!!", ObjGlobal.Caption);
            TxtShortName.Focus();
        }
    }

    #endregion --------------- Form ---------------

    // METHOD FOR THIS FORM

    #region --------------- Method ---------------
    private async Task<SyncApiConfig> FetchUrlParamsAsync(string baseUrl)
    {
        var client = new HttpClient(new CompressHandler(), true);
        try
        {
            client.BaseAddress = new Uri(baseUrl);
            var json = await client.GetStringAsync("datasync/config");

            var jObject = JObject.Parse(json);
            var success = (bool)jObject["Success"];

            if (success)
            {
                var token = jObject.SelectToken("Model");
                if (token != null)
                {
                    var model = new SyncApiConfig
                    {
                        BaseUrl = baseUrl,
                        GetUrl = token.Value<string>("FetchRoute"),
                        InsertUrl = token.Value<string>("PushRoute"),
                        UpdateUrl = token.Value<string>("PatchRoute")
                    };

                    return model;
                }
            }

            MessageBox.Show(@"Unable to fetch the url configs.", @"Error");
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace).ShowErrorDialog();
        }

        return null;
    }
    private bool IsValidForm()
    {
        if (_actionTag == string.Empty)
        {
            return false;
        }
        if (TxtDescription.IsBlankOrEmpty())
        {
            TxtDescription.WarningMessage($"DESCRIPTION IS REQUIRED FOR {_actionTag}");
            return false;
        }

        if (_actionTag != "SAVE")
        {
            if (_branchId is 0)
            {
                TxtDescription.WarningMessage($"DESCRIPTION IS REQUIRED FOR {_actionTag}");
                return false;
            }
        }

        if (_actionTag != "DELETE")
        {
            if (TxtShortName.IsBlankOrEmpty())
            {
                TxtShortName.WarningMessage("BRANCH SHORT NAME IS REQUIRED..!!");
                return false;
            }
        }

        return true;
    }

    private int SaveBranch()
    {
        const int syncRow = 1;
        _branchSetup.BranchSetup.Branch_ID = _actionTag is "SAVE"
            ? _branchId.ReturnMaxIntId("BRANCH")
            : _branchId;
        _branchSetup.BranchSetup.Branch_Name = TxtDescription.Text.GetString();
        _branchSetup.BranchSetup.Branch_Code = TxtShortName.Text.GetString();
        _branchSetup.BranchSetup.Reg_Date = MskRegDate.Text.GetDateTime();
        _branchSetup.BranchSetup.Address = TxtAddress.Text.GetString();
        _branchSetup.BranchSetup.Country = TxtCountry.Text.GetString();
        _branchSetup.BranchSetup.State = TxtState.Text.GetString();
        _branchSetup.BranchSetup.City = TxtCity.Text.GetString();
        _branchSetup.BranchSetup.PhoneNo = TxtPhoneNo.Text.Trim();
        _branchSetup.BranchSetup.Fax = TxtFax.Text.Trim();
        _branchSetup.BranchSetup.Email = TxtEmail.Text.Trim();
        _branchSetup.BranchSetup.ContactPerson = TxtContactPerson.Text.GetString();
        _branchSetup.BranchSetup.ContactPersonAdd = TxtContactPersonAddress.Text.GetString();
        _branchSetup.BranchSetup.ContactPersonPhone = TxtContactPersonPhoneNo.Text.Trim();
        _branchSetup.BranchSetup.SyncRowVersion = syncRow.ReturnSyncRowNo("BRANCH", _branchId);
        return _branchSetup.SaveBranch(_actionTag);
    }

    private void ClearControl()
    {
        Text = !string.IsNullOrEmpty(_actionTag) ? $"BRANCH SETUP DETAILS [{_actionTag}]" : "BRANCH SETUP DETAILS";
        IList list = StorePanel.Controls;
        foreach (var t in list)
        {
            var txt = (Control)t;
            if (txt is not System.Windows.Forms.TextBox) continue;
            txt.Text = string.Empty;
            txt.BackColor = SystemColors.Window;
        }

        ObjGlobal.PageLoadDateType(MskRegDate);
        TxtDescription.Focus();
    }

    private void SetGridDataToText(int branchId)
    {
        var dtb = _branchSetup.GetMasterBranch(branchId);
        if (dtb.Rows.Count <= 0) return;
        foreach (DataRow ro in dtb.Rows)
        {
            TxtDescription.Text = ro["Branch_Name"].GetTrimApostrophe();
            TxtShortName.Text = ro["Branch_Code"].ToString();
            MskRegDate.Text = ro["Reg_Date"].ToString(); //ObjGlobal.FillDateType(ro["Reg_Date"].ToString());
            TxtAddress.Text = ro["Address"].ToString();
            TxtCountry.Text = ro["Country"].ToString();
            TxtState.Text = ro["State"].ToString();
            TxtCity.Text = ro["City"].ToString();
            TxtPhoneNo.Text = ro["PhoneNo"].ToString();
            TxtFax.Text = ro["Fax"].ToString();
            TxtEmail.Text = ro["Email"].ToString();
            TxtContactPerson.Text = ro["ContactPerson"].ToString();
            TxtContactPersonAddress.Text = ro["ContactPersonAdd"].ToString();
            TxtContactPersonPhoneNo.Text = ro["ContactPersonPhone"].ToString();
        }
    }

    private void ControlEnable(bool isEnable = false)
    {
        BtnNew.Enabled = BtnDelete.Enabled = BtnEdit.Enabled = !isEnable;
        TxtDescription.Enabled = BtnDescription.Enabled = _actionTag.IsValueExits() && _actionTag != "SAVE" || isEnable;

        TxtShortName.Enabled = isEnable;
        MskRegDate.Enabled = isEnable;
        TxtState.Enabled = isEnable;
        TxtCity.Enabled = isEnable;
        TxtAddress.Enabled = TxtCountry.Enabled = isEnable;
        TxtPhoneNo.Enabled = TxtFax.Enabled = TxtState.Enabled = TxtCity.Enabled = isEnable;
        TxtEmail.Enabled = isEnable;
        TxtContactPerson.Enabled = TxtContactPersonAddress.Enabled = TxtContactPersonPhoneNo.Enabled = isEnable;
        BtnSave.Enabled = BtnCancel.Enabled = !string.IsNullOrEmpty(_actionTag) && _actionTag != "SAVE" || isEnable;
    }

    private async void GetAndSaveUnSynchronizedBranch()
    {
        try
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
                GetUrl = @$"{_configParams.Model.Item2}Branch/GetBranchByCallCount",
                InsertUrl = @$"{_configParams.Model.Item2}Branch/InsertBranchList",
                UpdateUrl = @$"{_configParams.Model.Item2}Branch/UpdateBranch"
            };
            DataSyncHelper.SetConfig(apiConfig);
            _injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(_injectData);
            var branchRepo = DataSyncProviderFactory.GetRepository<Branch>(_injectData);
            SplashScreenManager.ShowForm(typeof(PleaseWait));

            // pull all new branch data - From Server Data pull and push
            var pullResponse = await _branchSetup.PullBranchServerToClientByRowCounts(branchRepo, 1);
            SplashScreenManager.CloseForm();

            // push all new branch data - From Local data pull and push
            var sqlBrQuery = _branchSetup.GetBranchScript();
            var queryResponse = await QueryUtils.GetListAsync<Branch>(sqlBrQuery);
            var branches = queryResponse.List.ToList();
            if (branches.Count > 0)
            {
                SplashScreenManager.ShowForm(typeof(PleaseWait));
                var pushResponse = await branchRepo.PushNewListAsync(branches);
                SplashScreenManager.CloseForm();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion --------------- Method ---------------

    // OBJECT GLOBAL

    #region --------------- Global Value ---------------

    private int _branchId;
    public string BranchName = string.Empty;
    private string _actionTag = string.Empty;
    private readonly bool _isZoom;
    private readonly IBranchSetupRepository _branchSetup;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
    private readonly DbSyncRepoInjectData _injectData;

    #endregion --------------- Global Value ---------------


}