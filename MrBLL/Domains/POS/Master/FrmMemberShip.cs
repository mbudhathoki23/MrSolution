using DatabaseModule.CloudSync;
using DatabaseModule.Master.FinanceSetup;
using DevExpress.XtraSplashScreen;
using MrBLL.Master.LedgerSetup;
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
using MrDAL.Global.WinForm;
using MrDAL.Master;
using MrDAL.Master.FinanceSetup;
using MrDAL.Master.Interface;
using MrDAL.Master.Interface.FinanceSetup;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Domains.POS.Master;

public partial class FrmMemberShip : MrForm
{
    #region --------------- Frm ---------------

    public FrmMemberShip(bool IsZoom = false, bool isNew = false, string MbNumber = "")
    {
        InitializeComponent();
        _setup = new MembershipSetupRepository();
        ObjSetup = new ClsMasterSetup();
        _IsZoom = IsZoom;
        _isNew = isNew;
        _isMbNumber = MbNumber;
        _ActionTag = string.Empty;
        clsFormControl = new ClsMasterForm(this, BtnExit);
        ClearControl();
        EnableControl();
        Shown += (_, _) =>
        {
            if (_isNew)
            {
                BtnNew.PerformClick();
                TxtPhoneNo.Text = _isMbNumber;
                TxtPhoneNo.Focus();
            }
            else
            {
                BtnNew.Focus();
            }
        };
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }

    private void FrmMemberShip_Load(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() =>
            {
                GetAndSaveUnSynchronizedMemberShips();
            });
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete, BtnView], this.Tag);
    }

    private void FrmMemberShip_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
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
                if (MessageBox.Show(@"ARE YOU SURE WANT TO EXIT THE FORM..??", ObjGlobal.Caption,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Close();
            }
        }
    }

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _ActionTag = "SAVE";
        ClearControl();
        EnableControl(false, true);
        TxtPhoneNo.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _ActionTag = "UPDATE";
        ClearControl();
        EnableControl(false, true);
        TxtMemberId.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _ActionTag = "DELETE";
        ClearControl();
        EnableControl(false);
        TxtDescription.Focus();
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (!FrmValid())
        {
            return;
        }

        if (SaveMemberShipDetails() > 0)
        {
            if (_IsZoom)
            {
                MemberTypeDesc = TxtDescription.Text.Trim();
                Close();
                return;
            }

            CustomMessageBox.ActionSuccess(TxtDescription.Text.GetUpper(), "MEMBERSHIP", _ActionTag);
            ClearControl();
            TxtPhoneNo.Focus();
        }
        else
        {
            TxtPhoneNo.ErrorMessage($"ERROR OCCURE WHILE {TxtDescription.Text} {_ActionTag}");
            TxtPhoneNo.Focus();
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtDescription.Text.Trim()))
        {
            BtnExit.PerformClick();
        }
        else
        {
            ClearControl();
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

    private void TxtDescription_Leave(object sender, EventArgs e)
    {
        if (ActiveControl != null && string.IsNullOrEmpty(TxtDescription.Text.Trim().Replace("'", "''")) &&
            TxtDescription.Focused && !string.IsNullOrEmpty(_ActionTag))
        {
            MessageBox.Show(@"DESCRIPTION IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDescription.Focus();
        }
    }

    private void TxtDescription_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnDescription_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtDescription.Text.Trim().Replace("'", "''")) && TxtDescription.Enabled &&
                TxtDescription.Focused)
            {
                MessageBox.Show(@"DESCRIPTION IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtDescription.Focus();
            }
        }
        else if (_ActionTag.ToUpper() != "SAVE" && TxtDescription.ReadOnly)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE",
                TxtDescription, BtnDescription);
        }
    }

    private void BtnDescription_Click(object sender, EventArgs e)
    {
        var frmPickList = new FrmAutoPopList("MIN", "MEMBERSHIP", _SearchKey, _ActionTag, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
                if (_ActionTag != "SAVE")
                {
                    TxtDescription.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                    MemberShipId = Convert.ToInt32(frmPickList.SelectedList[0]["LedgerId"].ToString().Trim());
                    SetGridDataToText(MemberShipId);
                    TxtDescription.ReadOnly = false;
                    TxtDescription.Focus();
                }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"MEMBERSHIP NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            TxtDescription.Focus();
            return;
        }

        _SearchKey = string.Empty;
        TxtDescription.Focus();
    }

    private void TxtPhoneNo_Validating(object sender, CancelEventArgs e)
    {
        if (!string.IsNullOrEmpty(_ActionTag) && TxtPhoneNo.Focused && TxtPhoneNo.Text.Length <= 7 &&
            TxtPhoneNo.Text.Length <= 10)
        {
            MessageBox.Show(@"PHONE NUMBER IS INVALID ..!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtPhoneNo.Focus();
        }
        else
        {
            var CheckValid =
                GetConnection.SelectDataTableQuery("SELECT * FROM AMS.MemberShipSetup mss WHERE mss.PhoneNo='" +
                                                   TxtPhoneNo.Text + "'");
            if (CheckValid.Rows.Count > 0)
            {
                _ActionTag = "UPDATE";
                ClearControl();
                SetGridDataToText(ObjGlobal.ReturnInt(CheckValid.Rows[0][0].ToString()));
                TxtDescription.Focus();
            }
            else
            {
                _ActionTag = "SAVE";
            }
        }
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        var Query =
            $"SELECT * FROM AMS.MemberShipSetup mss WHERE mss.MShipDesc='{TxtDescription.Text.Replace("'", "''")}' AND mss.MemberId = '{TxtMemberId.Text}' AND mss.PhoneNo='{TxtPhoneNo.Text}' ";

        if (!string.IsNullOrEmpty(TxtDescription.Text.Trim().Replace("'", "''")) && _ActionTag == "SAVE")
            TxtShortName.Text = string.IsNullOrEmpty(TxtShortName.Text.Trim().Replace("'", "''"))
                ? ObjGlobal.BindAutoIncrementCode("MS", TxtDescription.Text.Trim().Replace("'", "''"))
                : TxtShortName.Text.Trim().Replace("'", "''");

        if (!string.IsNullOrEmpty(_ActionTag) && _ActionTag != "DELETE")
        {
            var dt = ObjSetup.CheckIsValidData(_ActionTag, "MemberType", "MemberDesc", "MemberTypeId",
                TxtDescription.Text.Replace("'", "''"), MemberId.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                MessageBox.Show(@"DESCRIPTION ALREADY EXITS..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                ClearControl();
                TxtDescription.Focus();
                return;
            }
        }

        if (_ActionTag is "UPDATE")
        {
            Query += $" AND mss.MShipId <> '{MemberShipId}' ";

            using var dt = GetConnection.SelectDataTableQuery(Query);
            if (dt != null && dt.Rows.Count > 0)
            {
                MessageBox.Show($@"{TxtDescription.Text}  DESCRIPTION ALREADY EXITS..!!", ObjGlobal.Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtDescription.Focus();
            }
        }
    }

    private void TxtShortName_Validating(object sender, CancelEventArgs e)
    {
        if (ActiveControl is null && string.IsNullOrEmpty(_ActionTag) &&
            !string.IsNullOrEmpty(TxtShortName.Text.Replace("'", "''")) && !TxtShortName.Focused) return;
        using var dt = ObjSetup.CheckIsValidData(_ActionTag, "MemberShipSetup", "MShipShortName", "MShipId",
            TxtShortName.Text.Replace("'", "''"), MemberShipId.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            MessageBox.Show(@"SHORTNAME ALREADY EXITS..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtShortName.Focus();
        }
    }

    private void TxtShortName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
            if (string.IsNullOrEmpty(TxtShortName.Text.Trim().Replace("'", "''")) && TxtShortName.Focused is true &&
                TxtShortName.Enabled is true)
            {
                MessageBox.Show(@"SHORTNAME IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtShortName.Focus();
            }
    }

    private void TxtShortName_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtShortName.Text.Trim().Replace("'", "''")) && TxtShortName.Focused is true &&
            TxtShortName.Enabled is true)
        {
            MessageBox.Show(@"SHORTNAME IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtShortName.Focus();
        }
    }

    private void TxtLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode is Keys.N && _IsZoom is false)
        {
            using var Frm = new FrmGeneralLedger("", true);
            Frm.ShowDialog();
            TxtLedger.Text = Frm.LedgerDesc.Trim();
            Frm.Dispose();
            LedgerId = Frm.LedgerId;
        }
        else if (e.KeyCode is Keys.F1)
        {
            BtnLedger_Click(sender, e);
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtLedger, BtnLedger);
        }
    }

    private void BtnLedger_Click(object sender, EventArgs e)
    {
        (TxtLedger.Text, LedgerId) = GetMasterList.GetGeneralLedger(_ActionTag, "CUSTOMER");
        TxtLedger.Focus();
    }

    private void TxtLedger_Validating(object sender, CancelEventArgs e)
    {
        if (LedgerId is 0 && !string.IsNullOrEmpty(TxtLedger.Text.Trim().Replace("'", "''")))
            LedgerId = ObjSetup.ReturnIntValueFromTable("AMS.GeneralLedger", "GLID", "GLName",
                TxtLedger.Text.Trim().Replace("'", "''"));
    }

    private void TxtLedger_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtLedger.Text.Trim().Replace("'", "''")) && TxtLedger.Focused is true &&
            !string.IsNullOrEmpty(_ActionTag))
            if (MessageBox.Show(@"MEMBER GENERAL LEDGER IS BLANK, DO YOU WANT TO CONTINUE..!!", ObjGlobal.Caption,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) is DialogResult.No)
                TxtLedger.Focus();
    }

    private void TxtMemberType_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode is Keys.N)
        {
            using var Frm = new FrmMemberType(true);
            Frm.ShowDialog();
            TxtMemberType.Text = Frm.MemberDesc;
            MemberTypeId = Frm.MemberId;
            Frm.Dispose();
        }
        else if (e.KeyCode is Keys.F1)
        {
            BtnMemberType_Click(sender, e);
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtMemberType, BtnMemberType);
        }
    }

    private void BtnMemberType_Click(object sender, EventArgs e)
    {
        using var frmPickList = new FrmAutoPopList("MIN", "MEMBERTYPE", ObjGlobal.SearchText, _ActionTag,
            string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtMemberType.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                MemberTypeId = Convert.ToInt32(frmPickList.SelectedList[0]["LedgerId"].ToString().Trim());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"MEMBERTYPE NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtMemberType.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtMemberType.Focus();
    }

    private void TxtMemberType_Validating(object sender, CancelEventArgs e)
    {
        if (MemberTypeId is 0 && !string.IsNullOrEmpty(TxtMemberType.Text.Trim().Replace("'", "''")))
            MemberTypeId = ObjSetup.ReturnIntValueFromTable("AMS.MemberType", "MemberTypeId", "MemberDesc",
                TxtMemberType.Text.Trim().Replace("'", "''"));
    }

    private void TxtMemberType_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtMemberType.Text.Trim().Replace("'", "''")) && TxtMemberType.Focused is true &&
            !string.IsNullOrEmpty(_ActionTag))
        {
            MessageBox.Show(@"MEMBERTYPE IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtMemberType.Focus();
        }
    }

    private void TxtPhoneNo_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1) BtnDescription_Click(sender, e);
    }

    private void TxtPhoneNo_KeyPress(object sender, KeyPressEventArgs e)
    {
        e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
    }

    private void BtnView_Click(object sender, EventArgs e)
    {
        BtnDescription_Click(sender, e);
        BtnView.Focus();
    }

    private void StorePanel_Paint(object sender, PaintEventArgs e)
    {
    }
    private void BtnSync_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedMemberShips);
        }
        else
        {
            CustomMessageBox.Warning("PLEASE CONFIG YOUR CLOUD BACK UP SYNC..!!");
        }
    }
    #endregion --------------- Frm ---------------

    // METHOD FOR THIS FORM

    #region --------------- Method ---------------

    private void SetGridDataToText(int selectedId)
    {
        var dt = ObjSetup.GetMasterMemberShip(_ActionTag, selectedId);
        if (dt == null || dt.Rows.Count <= 0) return;
        MemberShipId = dt.Rows[0]["MShipId"].GetInt();
        TxtDescription.Text = dt.Rows[0]["MShipDesc"].ToString();
        TxtMemberId.Text = dt.Rows[0]["MemberId"].ToString();
        TxtShortName.Text = dt.Rows[0]["MShipShortName"].ToString();
        TxtPhoneNo.Text = dt.Rows[0]["PhoneNo"].ToString();
        TxtEmail.Text = dt.Rows[0]["EmailAdd"].ToString();
        LedgerId = dt.Rows[0]["LedgerId"].GetLong();
        TxtLedger.Text = dt.Rows[0]["GLName"].ToString();
        MemberTypeId = dt.Rows[0]["MemberTypeId"].GetInt();
        MemberTypeId = dt.Rows[0]["MembertypeId"].GetInt();
        TxtMemberType.Text = dt.Rows[0]["MemberDesc"].ToString();
        MskStartDate.Text = dt.Rows[0]["MValidDate"].ToString();
        MskExpireDate.Text = dt.Rows[0]["MExpireDate"].ToString();
        ChkActive.Checked = dt.Rows[0]["ActiveStatus"].GetBool();
    }

    private void EnableControl(bool Btn = true, bool Txt = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = Btn;
        TxtDescription.Enabled = BtnDescription.Enabled = !string.IsNullOrEmpty(_ActionTag) && _ActionTag != "SAVE" || Txt;
        TxtMemberId.Enabled = !string.IsNullOrEmpty(_ActionTag) && _ActionTag is not "SAVE";
        TxtPhoneNo.Enabled = Txt;
        CmbPriceTag.Enabled = Txt;
        TxtLedger.Enabled = BtnLedger.Enabled = Txt;
        TxtEmail.Enabled = Txt;
        TxtMemberType.Enabled = Txt;
        TxtMemberType.Enabled = BtnMemberType.Enabled = Txt;
        MskStartDate.Enabled = MskExpireDate.Enabled = Txt;
        ChkActive.Enabled = !string.IsNullOrEmpty(_ActionTag) && _ActionTag == "UPDATE";
        BtnSave.Enabled = !string.IsNullOrEmpty(_ActionTag) && _ActionTag != "SAVE" || Txt;
        BtnCancel.Enabled = !string.IsNullOrEmpty(_ActionTag) && _ActionTag != "SAVE" || Txt;
    }

    private void ClearControl()
    {
        Text = !string.IsNullOrEmpty(_ActionTag) ? @$"MEMBERSHIP DETAILS SETUP [{_ActionTag}]" : "MEMBERSHIP DETAILS SETUP";
        TxtMemberId.Text = ObjSetup.ReturnMaxMemberId().ToString();
        TxtPhoneNo.Clear();
        TxtDescription.Clear();
        TxtShortName.Clear();
        if (_isNew)
        {
            TxtPhoneNo.Text = _isMbNumber;
        }
        CmbPriceTag.SelectedIndex = 0;
        ChkActive.Checked = true;
        TxtLedger.ReadOnly = true;
        LedgerId = 0;
        TxtLedger.Clear();
        TxtEmail.Clear();
        MemberTypeId = 0;
        TxtMemberType.Clear();
        TxtMemberType.ReadOnly = true;
        MskStartDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        MskExpireDate.Text = DateTime.Today.AddYears(1).ToString("dd/MM/yyyy");
        TxtDescription.ReadOnly = _ActionTag.ToUpper() != "SAVE";
    }

    private int SaveMemberShipDetails()
    {
        try
        {
            const short syncId = 0;
            MemberShipId = _ActionTag is "SAVE" ? MemberShipId.ReturnMaxIntId("MEMBERSHIPSETUP", "") : MemberShipId;
            _setup.MemberShipSetup.MShipId = MemberShipId;
            _setup.MemberShipSetup.MemberId = ObjSetup.ReturnMaxMemberId().GetInt();
            _setup.MemberShipSetup.MShipDesc = TxtDescription.GetString();
            _setup.MemberShipSetup.MShipShortName = TxtDescription.GetString();
            _setup.MemberShipSetup.PrimaryGrp = TxtDescription.GetString();
            _setup.MemberShipSetup.PriceTag = TxtDescription.GetString();
            _setup.MemberShipSetup.PhoneNo = TxtPhoneNo.Text;
            _setup.MemberShipSetup.LedgerId = LedgerId > 0 ? LedgerId : ObjGlobal.FinanceCashLedgerId;
            _setup.MemberShipSetup.EmailAdd = TxtEmail.GetString();
            _setup.MemberShipSetup.MemberTypeId = MemberTypeId;
            _setup.MemberShipSetup.BranchId = ObjGlobal.SysBranchId;
            _setup.MemberShipSetup.CompanyUnitId = ObjGlobal.SysCompanyUnitId;
            _setup.MemberShipSetup.MValidDate = MskStartDate.GetDateTime();
            _setup.MemberShipSetup.MExpireDate = MskExpireDate.GetDateTime();
            _setup.MemberShipSetup.EnterBy = ObjGlobal.LogInUser;
            _setup.MemberShipSetup.EnterDate = DateTime.Now;
            _setup.MemberShipSetup.ActiveStatus = ChkActive.Checked;
            _setup.MemberShipSetup.SyncBaseId = ObjGlobal.SyncBaseSync ?? Guid.Empty;
            _setup.MemberShipSetup.SyncOriginId = ObjGlobal.SyncBaseSync ?? Guid.Empty;
            _setup.MemberShipSetup.SyncCreatedOn = DateTime.Today;
            _setup.MemberShipSetup.SyncLastPatchedOn = DateTime.Now;
            _setup.MemberShipSetup.SyncRowVersion = (short)(_ActionTag is "UPDATE" ? syncId.ReturnSyncRowNo("GD", MemberShipId) : 1);

            var result = _setup.SaveMembershipSetup(_ActionTag);
            return result;
        }
        catch (Exception ex)
        {
            ex.DialogResult();
            return 0;
        }

    }

    private bool FrmValid()
    {
        if (string.IsNullOrEmpty(_ActionTag))
        {
            return false;
        }
        if (string.IsNullOrEmpty(TxtDescription.Text))
        {
            MessageBox.Show(@"Description is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            TxtDescription.Focus();
            return false;
        }

        if (string.IsNullOrEmpty(TxtShortName.Text))
        {
            MessageBox.Show(@"ShortName is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            TxtShortName.Focus();
            return false;
        }

        if (string.IsNullOrEmpty(TxtMemberType.Text) || TxtMemberType.Text.Trim().Length is 0)
        {
            MessageBox.Show(@"Member Type is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            TxtMemberType.Focus();
            return false;
        }

        return true;
    }

    private async void GetAndSaveUnSynchronizedMemberShips()
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
                GetUrl = @$"{_configParams.Model.Item2}MemberShipSetup/GetMemberShipSetupsByCallCount",
                InsertUrl = @$"{_configParams.Model.Item2}MemberShipSetup/InsertMemberShipSetupList",
                UpdateUrl = @$"{_configParams.Model.Item2}MemberShipSetup/UpdateMemberShipSetup",
            };

            DataSyncHelper.SetConfig(apiConfig);
            _injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(_injectData);
            var membershipSetupRepo = DataSyncProviderFactory.GetRepository<MemberShipSetup>(_injectData);

            SplashScreenManager.ShowForm(typeof(PleaseWait));
            // pull all new membership setup data
            var pullResponse = await _setup.PullMembershipSetupServerToClientByRowCount(membershipSetupRepo, 1);
            SplashScreenManager.CloseForm();

            // push all new membership setup data
            var sqlSaQuery = _setup.GetMembershipSetupScript();
            var queryResponse = await QueryUtils.GetListAsync<MemberShipSetup>(sqlSaQuery);
            var maList = queryResponse.List.ToList();
            if (maList.Count > 0)
            {
                SplashScreenManager.ShowForm(typeof(PleaseWait));
                var pushResponse = await membershipSetupRepo.PushNewListAsync(maList);
                SplashScreenManager.CloseForm();
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    #endregion --------------- Method ---------------

    // OBJECT FOR THIS FORM

    #region --------------- Global ---------------

    public int MemberShipId;
    private int MemberTypeId;
    private long LedgerId;
    public long ProductId = 0;
    public int MemberId;
    private readonly bool _IsZoom;
    private readonly bool _isNew;
    public string MemberTypeDesc = string.Empty;
    private readonly string _isMbNumber;
    private string _ActionTag = string.Empty;
    private string Query = string.Empty;
    private string Searchtext = string.Empty;
    private string _SearchKey = string.Empty;
    private readonly IMembershipSetupRepository _setup;
    private readonly IMasterSetup ObjSetup;
    private ClsMasterForm clsFormControl;
    private DbSyncRepoInjectData _injectData;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
    #endregion --------------- Global ---------------
}