using DatabaseModule.CloudSync;
using DatabaseModule.Master.FinanceSetup;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using MrBLL.Utility.Common.Class;
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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Domains.POS.Master;

public partial class FrmMemberType : MrForm
{


    #region --------------- Frm ---------------

    public FrmMemberType(bool isZoom)
    {
        InitializeComponent();
        _IsZoom = isZoom;
        _memberTypeRepository = new MemberTypeRepository();
        ObjSetup = new ClsMasterSetup();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }

    private void FrmMemberType_Load(object sender, EventArgs e)
    {
        ClearControl();
        EnableControl();
        BtnNew.Focus();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() =>
            {
                GetAndSaveUnsynchronizedMemberTypes();
            });
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete, BtnView], this.Tag);
    }

    private void FrmMemberType_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (!BtnNew.Enabled)
            {
                if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
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

    #region --------------- Button ---------------

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _ActionTag = "SAVE";
        ClearControl();
        EnableControl(false, true);
        TxtDescription.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _ActionTag = "UPDATE";
        ClearControl();
        EnableControl(false, true);
        TxtDescription.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _ActionTag = "DELETE";
        ClearControl();
        EnableControl(false);
        TxtDescription.Focus();
    }

    private bool FrmValid()
    {
        if (string.IsNullOrEmpty(_ActionTag)) return false;
        if (string.IsNullOrEmpty(TxtDescription.Text))
        {
            MessageBox.Show(@"Description is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDescription.Focus();
            return false;
        }

        if (string.IsNullOrEmpty(TxtShortName.Text))
        {
            MessageBox.Show(@"ShortName is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtShortName.Focus();
            return false;
        }

        return true;
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (FrmValid())
        {
            if (SaveMemberType() > 0)
            {
                if (_IsZoom)
                {
                    MemberDesc = TxtDescription.Text;
                    Close();
                    return;
                }

                MessageBox.Show($@"DATA {_ActionTag.ToUpper()}  SUCCESSFULLY..!!", ObjGlobal.Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControl();
                TxtDescription.Focus();
            }
        }
        else
        {
            MessageBox.Show($@"ERROR OCCUR WHILE {_ActionTag.ToUpper()} DATA..!!", ObjGlobal.Caption,
                MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            ClearControl();
            TxtDescription.Focus();
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtDescription.Text)) BtnExit.PerformClick();
        else ClearControl();
    }
    private void BtnSync_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnsynchronizedMemberTypes);
        }
        else
        {
            CustomMessageBox.Warning("PLEASE CONFIG YOUR CLOUD BACK UP SYNC..!!");
        }
    }
    #endregion --------------- Button ---------------

    #region --------------- Method ---------------

    protected void SetGridDataToText(int MemberId)
    {
        var dt = ObjSetup.GetMasterMemberType(_ActionTag, MemberId);
        if (dt.Rows.Count > 0)
        {
            int.TryParse(dt.Rows[0]["MemberTypeId"].ToString(), out var _MemberId);
            MemberId = _MemberId;
            TxtDescription.Text = dt.Rows[0]["MemberDesc"].ToString();
            TxtShortName.Text = dt.Rows[0]["MemberShortName"].ToString();
            TxtDiscount.Text = dt.Rows[0]["Discount"].ToString();
            bool.TryParse(dt.Rows[0]["ActiveStatus"].ToString(), out var _Check);
            ChkActive.Checked = _Check;
            if (_ActionTag == "UPDATE")
                TxtDescription.Focus();
            else
                BtnSave.Focus();
        }
    }

    private void EnableControl(bool Btn = true, bool Txt = false)
    {
        BtnDelete.Enabled = BtnEdit.Enabled = BtnNew.Enabled = Btn;

        BtnDescription.Enabled = TxtDescription.Enabled = BtnSave.Enabled = _ActionTag != "DELETE" ? Txt : true;
        TxtShortName.Enabled = TxtDiscount.Enabled = Txt;
        ChkActive.Enabled = !string.IsNullOrEmpty(_ActionTag) && _ActionTag == "UPDATE" ? true : false;
        BtnCancel.Enabled = BtnSave.Enabled = _ActionTag != "DELETE" ? Txt : true;
    }

    private void ClearControl()
    {
        Text = $"MEMBERTYPE DETAILS SETUP {_ActionTag} ".Trim();
        IList list = StorePanel.Controls;
        for (var i = 0; i < list.Count; i++)
        {
            var control = (Control)list[i];
            if (control is TextBox)
            {
                control.Text = string.Empty;
                control.BackColor = SystemColors.Window;
            }
        }

        ChkActive.Checked = true;
        TxtDescription.ReadOnly = _ActionTag.ToUpper() != "SAVE" ? true : false;
        TxtDescription.Focus();
    }

    private int SaveMemberType()
    {
        try
        {
            const short syncId = 0;
            MemberId = _ActionTag is "SAVE" ? MemberId.ReturnMaxIntId("MEMBERTYPE", "") : MemberId;
            _memberTypeRepository.ObjMemberType.MemberTypeId = MemberId;
            _memberTypeRepository.ObjMemberType.MemberDesc = TxtDescription.Text;
            _memberTypeRepository.ObjMemberType.MemberShortName = TxtShortName.Text;
            _memberTypeRepository.ObjMemberType.Discount = TxtDiscount.Text.GetDecimal();
            _memberTypeRepository.ObjMemberType.BranchId = ObjGlobal.SysBranchId;
            _memberTypeRepository.ObjMemberType.CompanyUnitId = ObjGlobal.SysCompanyUnitId > 0 ? ObjGlobal.SysCompanyUnitId : null;
            _memberTypeRepository.ObjMemberType.EnterBy = ObjGlobal.LogInUser;
            _memberTypeRepository.ObjMemberType.EnterDate = DateTime.Now;
            _memberTypeRepository.ObjMemberType.ActiveStatus = ChkActive.Checked;
            _memberTypeRepository.ObjMemberType.SyncCreatedOn = DateTime.Today;
            _memberTypeRepository.ObjMemberType.SyncLastPatchedOn = DateTime.Now;
            _memberTypeRepository.ObjMemberType.SyncRowVersion =
                (short)(_ActionTag is "UPDATE" ? syncId.ReturnSyncRowNo("MEMBERTYPE", MemberId) : 1);

            return _memberTypeRepository.SaveMemberType(_ActionTag);

        }
        catch (Exception ex)
        {
            ex.DialogResult();
            return 0;
        }
    }

    private async void GetAndSaveUnsynchronizedMemberTypes()
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
                GetUrl = @$"{_configParams.Model.Item2}MemberType/GetMemberTypesByCallCount",
                InsertUrl = @$"{_configParams.Model.Item2}MemberType/InsertMemberTypeList",
                UpdateUrl = @$"{_configParams.Model.Item2}MemberType/UpdateMemberType",
            };

            DataSyncHelper.SetConfig(apiConfig);
            _injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(_injectData);
            var memberTypeRepo = DataSyncProviderFactory.GetRepository<MemberType>(_injectData);
            SplashScreenManager.ShowForm(typeof(PleaseWait));

            // pull all new member type data
            var pullResponse = await _memberTypeRepository.PullMainAreasServerToClientByRowCount(memberTypeRepo, 1);
            SplashScreenManager.CloseForm();

            // push all new member type data
            var sqlSaQuery = _memberTypeRepository.GetMemberTypeScript();
            var queryResponse = await QueryUtils.GetListAsync<MemberType>(sqlSaQuery);
            var maList = queryResponse.List.ToList();
            if (maList.Count > 0)
            {
                SplashScreenManager.ShowForm(typeof(PleaseWait));
                var pushResponse = await memberTypeRepo.PushNewListAsync(maList);
                SplashScreenManager.CloseForm();
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    private void TxtDiscount_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            BtnSave.Focus();
        }
    }
    #endregion --------------- Method ---------------

    #region --------------- Events --------------

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
                TxtDescription.WarningMessage("MEMBER TYPE DESCRIPTION IS BLANK....!");
                TxtDescription.Focus();
            }
            else
            {
                TxtShortName.Focus();
            }
        }
        else
        {
            if (TxtDescription.ReadOnly)
            {
                ClsKeyPreview.KeyEvent(e, "DELETE", TxtDescription, BtnDescription);
            }
        }
    }

    private void TxtDescription_Leave(object sender, EventArgs e)
    {
        if (ActiveControl != null && !string.IsNullOrEmpty(_ActionTag) && TxtDescription.Focused &&
            string.IsNullOrEmpty(TxtDescription.Text.Trim().Replace("'", "''")))
        {
            MessageBox.Show(@"DESCRIPTION IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDescription.Focus();
        }
    }

    private void BtnDescription_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetMemberShipTypeList(_ActionTag);
        if (description.IsValueExits())
        {
            if (_ActionTag != "SAVE")
            {
                TxtDescription.Text = description;
                MemberId = id;
                SetGridDataToText(MemberId);
                TxtDescription.ReadOnly = false;
            }
        }

    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtDescription.Text.Trim().Replace("'", "''")) && _ActionTag.ToUpper() == "SAVE")
            TxtShortName.Text = string.IsNullOrEmpty(TxtShortName.Text.Replace("'", "''"))
                ? ObjGlobal.BindAutoIncrementCode("MT", TxtDescription.Text.Trim().Replace("'", "''"))
                : TxtShortName.Text.Replace("'", "''");
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
            }
        }
    }

    private void TxtShortName_Validating(object sender, CancelEventArgs e)
    {
        var dt = ObjSetup.CheckIsValidData(_ActionTag, "MemberType", "MemberShortName", "MemberTypeId",
            TxtShortName.Text.Replace("'", "''"), MemberId.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            MessageBox.Show(@"SHORTNAME ALREADY EXITS..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            ClearControl();
            TxtShortName.Focus();
        }
    }

    private void TxtShortName_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtShortName.Text.Trim().Replace("'", "''")) && TxtShortName.Focused is true &&
            !string.IsNullOrEmpty(_ActionTag))
        {
            MessageBox.Show(@"GROUP SHORTNAME IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtShortName.Focus();
        }
    }

    private void TxtShortName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1) BtnDescription_Click(sender, e);
        if (e.KeyCode is Keys.Enter)
        {
            TxtDiscount.Focus();
        }
    }

    private void TxtDiscount_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
        if (e.KeyChar == (char)Keys.Enter) TxtDiscount_Leave(sender, e);
    }

    private void TxtDiscount_Leave(object sender, EventArgs e)
    {
        if (ObjGlobal.ReturnDouble(TxtDiscount.Text) is 0 && TxtDiscount.Focused)
        {
            if (MessageBox.Show(@"DISCOUNT RATE IS BLANK OR ZERO DO YOU WANT TO CONTINUE..!!", ObjGlobal.Caption,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) is DialogResult.No)
                TxtDiscount.Focus();
            else
                BtnSave.Focus();
        }
        else
        {
            BtnSave.Focus();
        }
    }

    private void BtnView_Click(object sender, EventArgs e)
    {
        BtnDescription_Click(sender, e);
        BtnView.Focus();
    }

    #endregion --------------- Events --------------

    //OBJECT FOR THIS FORM 

    #region --------------- Global ---------------

    public int MemberId;
    public bool _IsZoom;
    public string MemberDesc;
    private string _Query = string.Empty;
    private string _ActionTag = string.Empty;
    private string _SearchKey = string.Empty;

    private readonly IMasterSetup ObjSetup;

    //private ClsMasterForm clsFormControl;
    private readonly IMemberTypeRepository _memberTypeRepository;
    private DbSyncRepoInjectData _injectData;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
    #endregion --------------- Global ---------------


}