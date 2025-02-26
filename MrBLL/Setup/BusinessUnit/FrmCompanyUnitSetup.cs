using DatabaseModule.CloudSync;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using MrBLL.Setup.BranchSetup;
using MrDAL.Control.ControlsEx;
using MrDAL.Control.SplashScreen;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Models.Common;
using MrDAL.Setup.CompanySetup;
using MrDAL.Setup.Interface;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatabaseModule.Setup.CompanyMaster;

namespace MrBLL.Setup.BusinessUnit;

public partial class FrmCompanyUnitSetup : MrForm
{
    #region --------------- GLOBAL VARIABLE ---------------

    private int cmpunit_ID;
    public string SelectedUnit = string.Empty;
    private string BranchName = string.Empty;
    public int CmpUnit_Id = 0;
    private bool _isZoom;
    private string _ActionTag = string.Empty;
    private string Query = string.Empty;
    private ClsMasterForm GetForm;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
    private DbSyncRepoInjectData _injectData;
    private readonly IMasterSetup ObjUnit = new ClsMasterSetup();
    // private readonly IMasterSetup ObjMaster = new ClsMasterSetup();
    private readonly ICompanyUnitSetupRepository _companyUnitSetup = new CompanyUnitSetupRepository();

    #endregion --------------- GLOBAL VARIABLE ---------------

    #region --------------- Frm ---------------

    public FrmCompanyUnitSetup()
    {
        InitializeComponent();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
        GetForm = new ClsMasterForm(this, BtnExit);
    }

    private void FrmCompanyUnitSetup_Load(object sender, EventArgs e)
    {
        ClearControl();
        EnableControl();
        BtnNew.Focus();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedCompanyUnit);
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }

    private void FrmCompanyUnitSetup_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)27)
        {
            if (BtnNew.Enabled == false || BtnEdit.Enabled == false || BtnDelete.Enabled == false)
            {
                if (MessageBox.Show(@"ARE YOU SURE WANT TO CLEAR FORM..??", ObjGlobal.Caption,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _ActionTag = string.Empty;

                    ClearControl();
                    EnableControl();
                    BtnNew.Focus();
                }
            }
            else
            {
                BtnExit.PerformClick();
            }
        }
    }

    #endregion --------------- Frm ---------------

    #region --------------- Botton ---------------

    private void btn_New_Click(object sender, EventArgs e)
    {
        _ActionTag = "SAVE";
        btn_Save.Text = "&Save";
        ClearControl();
        EnableControl(false, true);
        TxtDescription.Focus();
    }

    private void btn_Edit_Click(object sender, EventArgs e)
    {
        _ActionTag = "UPDATE";
        btn_Save.Text = "&Update";
        ClearControl();
        EnableControl(false, true);
        TxtDescription.ReadOnly = true;
        TxtDescription.Focus();
    }

    private void btn_Delete_Click(object sender, EventArgs e)
    {
        _ActionTag = "DELETE";
        btn_Save.Text = "&Delete";
        EnableControl(false);
        TxtDescription.ReadOnly = true;
        TxtDescription.Focus();
        btn_Save.Enabled = true;
        btn_Clear.Enabled = true;
    }

    private void btn_Sync_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedCompanyUnit);
        }
        else
        {
            CustomMessageBox.Warning("PLEASE CONFIG YOUR CLOUD BACK UP SYNC..!!");
        }
    }

    private bool IsValidForm()
    {
        if (string.IsNullOrEmpty(TxtDescription.Text.Trim()))
        {
            MessageBox.Show(@"UNIT NAME IS NOT BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtDescription.Focus();
            return false;
        }

        if (string.IsNullOrEmpty(TxtShortName.Text.Trim()))
        {
            MessageBox.Show(@"UNIT CODE IS NOT BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtShortName.Focus();
            return false;
        }

        if (string.IsNullOrEmpty(txt_BranchName.Text.Trim()))
        {
            MessageBox.Show(@"COMPANY BRANCH IS NOT BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            txt_BranchName.Focus();
            return false;
        }

        return true;
    }

    private void btn_Save_Click(object sender, EventArgs e)
    {
        if (!IsValidForm()) return;

        if (SaveCompanyUnit() > 0)
        {
            if (_isZoom)
            {
                SelectedUnit = TxtShortName.Text;
                Close();
                return;
            }

            MessageBox.Show($@"{TxtDescription.Text} COMPANY UNIT  {_ActionTag} SUCCESSFULLY..!!",
                ObjGlobal.Caption);
            ClearControl();
            TxtDescription.Focus();
            return;
        }

        MessageBox.Show($@"{TxtDescription.Text} COMPANY UNIT {_ActionTag} UNSUCESSFUL!! (MIGHT BE IN USE)",
            ObjGlobal.Caption);
    }

    private void btn_Clear_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtDescription.Text.Trim()))
        {
            Close();
        }
        else
        {
            ClearControl();
            if (_ActionTag == "SAVE")
                txt_BranchName.Focus();
        }
    }

    private void btnExit_Click(object sender, EventArgs e)
    {

        var result = CustomMessageBox.ExitActiveForm();
        if (result == DialogResult.Yes)
        {
            Close();
        }
    }

    #endregion --------------- Botton ---------------

    #region --------------- dgv_BranchDetl ---------------

    private int rowIndex;
    private int currentColumn;

    private void dgv_BranchDetl_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
        currentColumn = e.ColumnIndex;
    }

    private void dgv_BranchDetl_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
        rowIndex = e.RowIndex;
    }

    #endregion --------------- dgv_BranchDetl ---------------

    #region --------------- Event ---------------

    private void TxtDescription_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnDescription_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtDescription.Text.Trim().Replace("'", "''")) &&
                TxtDescription.Enabled is true && TxtDescription.Focused is true)
            {
                MessageBox.Show(@"DESCRIPTION IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtDescription.Focus();
            }
        }
        else if (_ActionTag.ToUpper() != "SAVE" && TxtDescription.ReadOnly is true)
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtDescription, BtnDescription);
        }
    }

    private void BtnDescription_Click(object sender, EventArgs e)
    {
        var frmPickList =
            new FrmAutoPopList("MIN", "CUNIT", ObjGlobal.SearchText, _ActionTag, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtDescription.Text = frmPickList.SelectedList[0]["CmpUnit_Name"].ToString().Trim();
                cmpunit_ID = ObjGlobal.ReturnInt(frmPickList.SelectedList[0]["CmpUnit_ID"].ToString().Trim());
                if (_ActionTag != "SAVE")
                {
                    SetGridDataToText(cmpunit_ID);
                    TxtDescription.ReadOnly = false;
                }
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"COMPANY UNIT LIST NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtDescription.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtDescription.Focus();
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (ActiveControl != null && !string.IsNullOrWhiteSpace(TxtDescription.Text.Replace("'", "''")) &&
            _ActionTag == "DELETE" && !TxtDescription.Focused)
        {
            var dtBranch = _companyUnitSetup.CheckMasterValidData(_ActionTag, "AMS.CompanyUnit", "CmpUnit_Name",
                "CmpUnit_ID", TxtDescription.Text.Replace("'", "''"), cmpunit_ID.ToString());
            if (dtBranch.Rows.Count <= 0) return;
            MessageBox.Show(@"COMPANY UNIT IS ALREADY EXITS..!!", ObjGlobal.Caption);
            TxtDescription.Focus();
        }
    }

    private void TxtDescription_Leave(object sender, EventArgs e)
    {
        if (ActiveControl != null && !string.IsNullOrEmpty(TxtDescription.Text.Trim().Replace("'", "''")) &&
            !string.IsNullOrEmpty(_ActionTag))
            TxtShortName.Text = ObjGlobal.BindAutoIncrementCode("CMU", TxtDescription.Text.Replace("'", "''"));

        if (ActiveControl != null && string.IsNullOrEmpty(TxtDescription.Text.Trim().Replace("'", "''")) &&
            TxtDescription.Focused && !string.IsNullOrEmpty(_ActionTag))
        {
            MessageBox.Show(@"UNIT DESCRIPTION IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDescription.Focus();
        }
    }

    private void TxtShortName_Leave(object sender, EventArgs e)
    {
        if (ActiveControl != null && !string.IsNullOrWhiteSpace(TxtShortName.Text.Replace("'", "''")) &&
            _ActionTag != "DELETE" && TxtShortName.Focused)
        {
            var dtBranch = _companyUnitSetup.CheckMasterValidData(_ActionTag, "AMS.CompanyUnit", "CmpUnit_Name",
                "CmpUnit_ID", TxtShortName.Text.Replace("'", "''"), cmpunit_ID.ToString());
            if (dtBranch.Rows.Count <= 0) return;
            MessageBox.Show(@"COMPANY UNIT NAME IS ALREADY EXITS..!!", ObjGlobal.Caption);
            TxtShortName.Focus();
        }
    }

    private void TxtShortName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1) BtnDescription_Click(sender, e);
    }

    private void TxtShortName_Validated(object sender, EventArgs e)
    {
        if (ActiveControl != null && !string.IsNullOrWhiteSpace(TxtShortName.Text.Replace("'", "''")) &&
            _ActionTag != "DELETE" &&
            TxtShortName.Focused)
        {
            var dtBranch = _companyUnitSetup.CheckMasterValidData(_ActionTag, "AMS.CompanyUnit", "CmpUnit_Name",
                "CmpUnit_ID", TxtShortName.Text.Replace("'", "''"), cmpunit_ID.ToString());
            if (dtBranch.Rows.Count <= 0) return;
            MessageBox.Show(@"COMPANY UNIT SHORT NAME ALREADY EXITS..!!", ObjGlobal.Caption);
            TxtShortName.Focus();
        }
    }

    private void MskRegistrationDate_Leave(object sender, EventArgs e)
    {
        if (ActiveControl != null && MskRegistrationDate.Text.Length is 0 &&
            MskRegistrationDate.Focused & !string.IsNullOrWhiteSpace(_ActionTag))
        {
            MessageBox.Show(@"COMPANY UNIT NAME IS ALREADY EXITS..!!", ObjGlobal.Caption);
            TxtShortName.Focus();
        }
    }

    private void TxtPhone_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void txt_Email_Leave(object sender, EventArgs e)
    {
    }

    private void txt_ContPerPhone_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void txt_BranchName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            btn_Branch_Click(sender, e);
        }
        else if (e.Control is true && e.KeyCode is Keys.N)
        {
            var frm = new FrmBranchSetup(true);
            frm.ShowDialog();
            txt_BranchName.Text = frm.BranchName;
        }
        else if (txt_BranchName.ReadOnly is true)
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), txt_BranchName, btn_Branch);
        }
    }

    private void btn_Branch_Click(object sender, EventArgs e)
    {
        var frmPickList =
            new FrmAutoPopList("MIN", "BRANCH", ObjGlobal.SearchText, _ActionTag, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
                txt_BranchName.Text = frmPickList.SelectedList[0]["ValueName"].ToString().Trim();
            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"BRANCH LIST NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            txt_BranchName.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        txt_BranchName.Focus();
    }

    #endregion --------------- Event ---------------

    #region Method

    private int SaveCompanyUnit()
    {
        _companyUnitSetup.CompanyUnitSetup.CmpUnit_ID = _ActionTag is "SAVE"
            ? ObjGlobal.ReturnInt(ClsMasterSetup.ReturnMaxIntValue("AMS.CompanyUnit ", "CmpUnit_ID").ToString())
            : cmpunit_ID;
        _companyUnitSetup.CompanyUnitSetup.CmpUnit_Name = TxtDescription.Text.Trim();
        _companyUnitSetup.CompanyUnitSetup.CmpUnit_Code = TxtShortName.Text.Trim();
        _companyUnitSetup.CompanyUnitSetup.Reg_Date = MskRegistrationDate.Text.GetDateTime();
        _companyUnitSetup.CompanyUnitSetup.Address = TxtAddress.Text.Trim();
        _companyUnitSetup.CompanyUnitSetup.Country = TxtCountry.Text.Trim();
        _companyUnitSetup.CompanyUnitSetup.State = "";
        _companyUnitSetup.CompanyUnitSetup.City = "";
        _companyUnitSetup.CompanyUnitSetup.PhoneNo = TxtPhone.Text.Trim();
        _companyUnitSetup.CompanyUnitSetup.Fax = txt_Fax.Text.Trim();
        _companyUnitSetup.CompanyUnitSetup.Email = TxtEmail.Text.Trim();
        _companyUnitSetup.CompanyUnitSetup.ContactPerson = txt_ContactPerson.Text.Trim();
        _companyUnitSetup.CompanyUnitSetup.ContactPersonAdd = txt_ContPerAdd.Text.Trim();
        _companyUnitSetup.CompanyUnitSetup.ContactPersonPhone = txt_ContPerPhone.Text.Trim();
        _companyUnitSetup.CompanyUnitSetup.Branch_ID = ObjGlobal.SysBranchId;
        _companyUnitSetup.CompanyUnitSetup.Created_By = ObjGlobal.LogInUser;
        _companyUnitSetup.CompanyUnitSetup.Created_Date = DateTime.Now;
        _companyUnitSetup.CompanyUnitSetup.SyncRowVersion = (short)(_ActionTag is "UPDATE"
            ? ClsMasterSetup.ReturnMaxSyncBaseIdValue("AMS.CompanyUnit", "SyncRowVersion", "CmpUnit_ID",
                cmpunit_ID.ToString())
            : 1);
        return _companyUnitSetup.SaveCompanyUnit(_ActionTag);
    }

    private void ClearControl()
    {
        Text = !string.IsNullOrEmpty(_ActionTag)
            ? $@"COMPANY UNIT SETUP [{_ActionTag.Trim()}]"
            : "COMPANY UNIT SETUP";
        TxtDescription.Clear();
        TxtShortName.Clear();
        MskRegistrationDate.Text = DateTime.Now.ToString();
        TxtAddress.Clear();
        TxtCountry.Clear();
        TxtPhone.Clear();
        txt_Fax.Clear();
        TxtEmail.Clear();
        txt_ContactPerson.Clear();
        txt_ContPerAdd.Clear();
        txt_ContPerPhone.Clear();
        txt_BranchName.Clear();
    }

    private void SetGridDataToText(int cmpunit_ID)
    {
        var dt = ObjUnit.GetCompanyUnit(_ActionTag, cmpunit_ID);
        if (dt.Rows.Count <= 0) return;
        _companyUnitSetup.CompanyUnitSetup.CmpUnit_ID = cmpunit_ID;
        TxtDescription.Text = dt.Rows[0]["CmpUnit_Name"].ToString();
        TxtShortName.Text = dt.Rows[0]["CmpUnit_Code"].ToString();
        MskRegistrationDate.Text = dt.Rows[0]["Reg_Date"].ToString(); //DateTime.Now.ToString();
        TxtAddress.Text = dt.Rows[0]["Address"].ToString();
        TxtCountry.Text = dt.Rows[0]["Country"].ToString();
        TxtPhone.Text = dt.Rows[0]["PhoneNo"].ToString();
        txt_Fax.Text = dt.Rows[0]["Fax"].ToString();
        TxtEmail.Text = dt.Rows[0]["Email"].ToString();
        txt_ContactPerson.Text = dt.Rows[0]["ContactPerson"].ToString();
        txt_ContPerAdd.Text = dt.Rows[0]["ContactPersonAdd"].ToString();
        txt_ContPerPhone.Text = dt.Rows[0]["ContactPersonPhone"].ToString();
        txt_BranchName.Text = dt.Rows[0]["Branch_Name"].ToString();
        TxtDescription.Focus();
    }

    private void EnableControl(bool Btn = true, bool Txt = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = Btn;
        TxtDescription.Enabled = BtnDescription.Enabled =
            !string.IsNullOrEmpty(_ActionTag) && _ActionTag is "DELETE" || Txt;
        TxtDescription.ReadOnly = false;
        TxtShortName.Enabled = Txt;
        MskRegistrationDate.Enabled = Txt;
        TxtAddress.Enabled = TxtCountry.Enabled = TxtPhone.Enabled = Txt;
        txt_Fax.Enabled = TxtEmail.Enabled = txt_ContactPerson.Enabled = Txt;
        txt_ContPerAdd.Enabled = txt_ContPerPhone.Enabled = txt_BranchName.Enabled = btn_Branch.Enabled = Txt;
        btn_Save.Enabled = btn_Clear.Enabled = !string.IsNullOrEmpty(_ActionTag) && _ActionTag is "DELETE" || Txt;
    }

    private async void GetAndSaveUnSynchronizedCompanyUnit()
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
            GetUrl = @$"{_configParams.Model.Item2}CompanyUnit/GetCompanyUnitByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}CompanyUnit/InsertCompanyUnitList",
            UpdateUrl = @$"{_configParams.Model.Item2}CompanyUnit/UpdateCompanyUnit"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var companyUnitRepo = DataSyncProviderFactory.GetRepository<CompanyUnit>(_injectData);
        SplashScreenManager.ShowForm(typeof(PleaseWait));
        // pull all new company unit data
        var pullResponse = await _companyUnitSetup.PullCompanyUnitServerToClientByRowCounts(companyUnitRepo, 1);
        SplashScreenManager.CloseForm();

        // push all new company unit data
        var sqlcuQuery = _companyUnitSetup.GetCompanyUnitScript();
        var queryResponse = await QueryUtils.GetListAsync<CompanyUnit>(sqlcuQuery);
        var companyUnits = queryResponse.List.ToList();
        if (companyUnits.Count > 0)
        {
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            var pushResponse = await companyUnitRepo.PushNewListAsync(companyUnits);
            SplashScreenManager.CloseForm();
        }
    }
    #endregion Method



}