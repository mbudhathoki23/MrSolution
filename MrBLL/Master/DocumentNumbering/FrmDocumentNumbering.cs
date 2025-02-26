using DatabaseModule.CloudSync;
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
using MrDAL.Master.Interface.SystemSetup;
using MrDAL.Master.SystemSetup;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Master.DocumentNumbering;

public partial class FrmDocumentNumbering : MrForm
{
    #region --------------- Frm ---------------

    public FrmDocumentNumbering()
    {
        InitializeComponent();
        _groupSetup = new DocumentNumberingRepository();
        var form = new ClsMasterForm(this, BtnExit);
    }

    private void FrmDocumentNumbering_Load(object sender, EventArgs e)
    {
        ClearControl();
        EnableControl(true, false);
        BtnNew.Focus();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() =>
            {
                GetAndSaveUnSynchronizedDocumentNumbering();
            });
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete, BtnView], this.Tag);
    }

    private void FrmDocumentNumbering_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)27)
        {
            if (BtnNew.Enabled is false || BtnEdit.Enabled is false || BtnDelete.Enabled is false)
            {
                if (CustomMessageBox.ClearVoucherDetails("DOCUMENT NUMBERING") == DialogResult.Yes)
                {
                    _actionTag = string.Empty;
                    ClearControl();
                    EnableControl(true, false);
                    BtnNew.Focus();
                }
            }
            else
            {
                BtnExit.PerformClick();
            }
        }
    }

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        ClearControl();
        EnableControl(false, true);
        TxtDocumentDesc.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _actionTag = "UPDATE";
        EnableControl(false, true);
        ClearControl();
        TxtDocumentDesc.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        ClearControl();
        EnableControl(false, false);
        TxtDocumentDesc.Focus();
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {

        var result = CustomMessageBox.ExitActiveForm();
        if (result == DialogResult.Yes)
        {
            Close();
        }
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (!IsValidForm) return;
        if (SaveDocumentNumberingDetails() > 0)
        {
            CustomMessageBox.ActionSuccess(TxtDescription.Text.GetUpper(), "DOCUMENT NUMBERING", _actionTag);
            ClearControl();
            TxtDocumentDesc.Focus();
        }
        else
        {
            TxtDocumentDesc.ErrorMessage($"ERROR OCCURS WHILE DOCUMENT NUMBERING {_actionTag}");
            TxtDocumentDesc.Focus();
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtDescription.Text.Trim())) Close();
        else ClearControl();
    }

    private void TxtDocumentDesc_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is not Keys.F1)
        {
            if (e.KeyCode is Keys.Enter)
            {
                if (!TxtDocumentDesc.IsBlankOrEmpty())
                {
                    return;
                }
                TxtDocumentDesc.WarningMessage("DOCUMENT NUMBERING MODULE CAN NOT LEFT BLANK..!!");
            }
            else
            {
                ClsKeyPreview.KeyEvent(e, "DELETE", TxtDocumentDesc, BtnDocumentSchema);
            }
        }
        else
        {
            BtnDocumentSchema_Click(sender, e);
        }
    }

    private void BtnDocumentSchema_Click(object sender, EventArgs e)
    {
        var (description, module) = GetMasterList.GetDocumentSchemaList(_actionTag);
        if (description.IsValueExits())
        {
            _module = module;
            TxtDocumentDesc.Text = description;
        }
        TxtDocumentDesc.Focus();
    }

    private void TxtDocumentDesc_Validating(object sender, CancelEventArgs e)
    {
        if ((!TxtDocumentDesc.IsBlankOrEmpty() && !_module.IsBlankOrEmpty()) || !_actionTag.IsValueExits())
        {
            return;
        }
        if (ActiveControl == TxtDocumentDesc || ActiveControl == BtnExit)
        {
            return;
        }
        TxtDocumentDesc.WarningMessage(@"DOCUMENT NUMBERING MODULE CAN NOT LEFT BLANK..!!");
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
                TxtDescription.WarningMessage(@"DOCUMENT NUMBERING DESCRIPTION CAN NOT LEFT BLANK..!!");
                return;
            }
            MskStartDate.SelectAll();
        }
        else if (_actionTag != "SAVE" && TxtDescription.ReadOnly)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtDescription, BtnDescription);
        }
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (TxtDescription.IsValueExits())
        {
            var exits = TxtDescription.IsDocumentNumberingExits(_module, _actionTag, _documentId);
            if (exits)
            {
                TxtDescription.WarningMessage("DOCUMENT NUMBER DESCRIPTION IS ALREADY EXITS..!!");
            }
        }

        if (!TxtDescription.IsBlankOrEmpty() || !_actionTag.IsValueExits())
        {
            return;
        }
        if (ActiveControl.Name == "TxtDescription")
        {
            return;
        }
        TxtDescription.WarningMessage("DOCUMENT NUMBER DESCRIPTION IS BLANK..!!");
    }

    private void BtnDescription_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetDocumentNumberingList(_actionTag, _module);
        if (description.IsValueExits())
        {
            if (_actionTag.ToUpper() != "SAVE")
            {
                TxtDescription.Text = description;
                _documentId = id;
                TxtDescription.ReadOnly = false;
                SetGridDataToText(_documentId);
            }
        }
        TxtDescription.Focus();
    }

    private void MskStartDate_Enter(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(MskStartDate.Text))
        {
            MskStartDate.SelectionStart = 0;
            MskStartDate.SelectionLength = MskStartDate.Text.Length;
        }
        MskStartDate.SelectAll();
    }

    private void MskStartDate_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            MskEndDate.SelectAll();
        }
    }

    private void MskStartDate_Validating(object sender, CancelEventArgs e)
    {
        if (MskStartDate.MaskCompleted)
        {
            if (ActiveControl != MskStartDate)
            {
                var result = MskStartDate.IsDateExits(ObjGlobal.SysDateType);
                if (result)
                {
                    result = MskStartDate.IsValidDateRange(ObjGlobal.SysDateType);
                    if (!result)
                    {
                        MskStartDate.WarningMessage("ENTER DATE IS INVALID.!!");
                    }
                }
                else
                {
                    MskStartDate.WarningMessage("ENTER DATE IS INVALID.!!");
                }
            }
        }
    }

    private void MskEndDate_Validated(object sender, EventArgs e)
    {
        if (MskStartDate.MaskCompleted)
        {
            if (ActiveControl != MskStartDate)
            {
                var result = MskStartDate.IsDateExits(ObjGlobal.SysDateType);
                if (result)
                {
                    result = MskStartDate.IsValidDateRange(ObjGlobal.SysDateType);
                    if (!result)
                    {
                        MskStartDate.WarningMessage("ENTER DATE IS INVALID.!!");
                    }
                }
                else
                {
                    MskStartDate.WarningMessage("ENTER DATE IS INVALID.!!");
                }
            }
        }
    }

    private void TxtPrefix_Leave(object sender, EventArgs e)
    {
        TxtTotalLength.Text = (TxtSufix.Text.Length + TxtPrefix.Text.Length + TxtBodyLength.GetInt()).ToString();
    }

    private void TxtSufix_Leave(object sender, EventArgs e)
    {
        TxtTotalLength.Text = (TxtSufix.Text.Length + TxtPrefix.Text.Length + TxtBodyLength.GetInt()).ToString();
    }

    private void TxtBodyLength_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter && TxtBodyLength.GetInt() is 0)
        {
            TxtBodyLength.WarningMessage("BODY LENGTH IS ZERO PLEASE ENTER THE VALUE");
        }
    }

    private void TxtBodyLength_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is not (char)Keys.Enter)
        {
        }
        else
        {
            if (TxtBodyLength.GetInt() is 0)
            {
                TxtBodyLength.WarningMessage("BODY LENGTH CAN NOT BE ZERO PLEASE ENTER THE VALUE");
                return;
            }
        }

        if (e.KeyChar is (char)Keys.Back && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !int.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void TxtBodyLength_Leave(object sender, EventArgs e)
    {
        TxtTotalLength.Text = (TxtSufix.Text.Length + TxtPrefix.Text.Length + TxtBodyLength.GetInt()).ToString();
    }

    private void TxtStart_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter && TxtStart.GetInt() is 0)
        {
            TxtStart.WarningMessage("START NUMBER IS ZERO PLEASE ENTER THE NUMBER");
        }
    }

    private void TxtStart_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !int.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void TxtCurrent_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter && TxtCurrent.GetInt() <= 0)
        {
            TxtCurrent.WarningMessage("CURRENT NUMBER IS ZERO PLEASE ENTER THE NUMBER");
        }
    }

    private void TxtCurrent_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !int.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void TxtEnd_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !int.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void TxtBlank_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter)
        {
            return;
        }
        if (!TxtBlank.IsBlankOrEmpty() || rChkAutoNumber.Checked)
        {
            return;
        }
        TxtBlank.WarningMessage("PLEASE ENTER THE BLANK CHARACTER VALUE..!!");
    }

    private void TxtBlank_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !int.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void TxtBodyLength_Validating(object sender, CancelEventArgs e)
    {
        if (TxtBodyLength.Text.Trim().Length > 0 && !string.IsNullOrEmpty(_actionTag))
        {
            TxtEnd.Text = string.Empty;
            if (TxtBodyLength.IsValueExits())
            {
                for (var i = 0; i < TxtBodyLength.GetInt(); i++)
                {
                    if (TxtEnd.Text == string.Empty)
                    {
                        TxtEnd.Text = @"9";
                    }
                    else
                    {
                        TxtEnd.Text += @"9";
                    }
                }

                ChkBlankNumber.Focus();
            }
        }
    }

    private void BtnView_Click(object sender, EventArgs e)
    {
        _ = GetMasterList.GetDocumentNumberingList("VIEW", "");
    }

    #endregion --------------- Frm ---------------

    #region --------------- Method ---------------

    private void EnableControl(bool Btn, bool Txt)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = Btn;

        TxtDocumentDesc.Enabled = BtnDocumentSchema.Enabled =
            !string.IsNullOrWhiteSpace(_actionTag) && _actionTag is "DELETE" || Txt;
        CmbUser.Enabled = !string.IsNullOrEmpty(_actionTag) && _actionTag.ToUpper() is "UPDATE" || Txt;
        BtnDescription.Enabled = TxtDescription.Enabled =
            !string.IsNullOrEmpty(_actionTag) && _actionTag.ToUpper() != "SAVE" || Txt;
        MskStartDate.Enabled = Txt;
        MskEndDate.Enabled = Txt;
        TxtPrefix.Enabled = Txt;
        TxtSufix.Enabled = Txt;
        TxtBodyLength.Enabled = Txt;
        rChkAlphaNumber.Enabled = Txt;
        rChkNumerical.Enabled = Txt;
        rChkAutoNumber.Enabled = Txt;
        ChkBlankNumber.Enabled = Txt;
        TxtBlank.Enabled = Txt;
        CmbBranch.Enabled = Txt;
        CmbUnit.Enabled = Txt;
        TxtStart.Enabled = Txt;
        TxtCurrent.Enabled = Txt;
        TxtEnd.Enabled = false;
        CmbDesign.Enabled = Txt;
        ChkActive.Enabled = BtnSave.Enabled =
            BtnCancel.Enabled = !string.IsNullOrEmpty(_actionTag) && _actionTag != "SAVE" || Txt;
    }

    private void BindUser()
    {
        _query =
            "Select 'ALL' User_Name Union All select User_Name From master.AMS.UserInfo where User_Name not in ('Admin','AMSAdmin','MrDemo')";
        CmbUser.DataSource = GetConnection.SelectDataTableQuery(_query);
        CmbUser.DisplayMember = "User_Name";
        CmbUser.ValueMember = "User_Name";
        CmbUser.SelectedIndex = 0;
    }

    private void BindBranch()
    {
        _query = "Select Branch_Id,Branch_Name from AMS.Branch";
        CmbBranch.DataSource = GetConnection.SelectDataTableQuery(_query);
        CmbBranch.DisplayMember = "Branch_Name";
        CmbBranch.ValueMember = "Branch_Id";
        CmbBranch.SelectedIndex = ObjGlobal.SysBranchId - 1;
    }

    private void BindCompanyUnit()
    {
        _query = "Select CmpUnit_Id, CmpUnit_Code from AMS.CompanyUnit";
        CmbUnit.DataSource = GetConnection.SelectDataTableQuery(_query);
        CmbUnit.DisplayMember = "CmpUnit_Code";
        CmbUnit.ValueMember = "CmpUnit_Id";

    }

    private void ClearControl()
    {
        TxtDescription.Clear();
        TxtDocumentDesc.Clear();
        TxtDescription.ReadOnly = _actionTag.ToUpper() != "SAVE";
        Text = !string.IsNullOrEmpty(_actionTag)
            ? $"DOCUMENT NUMBERING SETUP [{_actionTag.ToUpper()}]".Trim()
            : "DOCUMENT NUMBERING SETUP";
        BindUser();
        BindBranch();
        BindCompanyUnit();
        MskStartDate.Text = ObjGlobal.CfStartBsDate;
        MskEndDate.Text = ObjGlobal.CfEndBsDate;
        TxtDescription.Clear();
        TxtPrefix.Clear();
        TxtSufix.Clear();
        TxtBodyLength.Clear();
        TxtTotalLength.Clear();
        CmbDesign.Text = string.Empty;
    }

    private void SetGridDataToText(int docId)
    {
        using var dt = _groupSetup.GetMasterDocumentNumbering(_actionTag, "", docId);
        if (dt == null || dt.Rows.Count <= 0)
        {
            return;
        }

        foreach (DataRow dtRow in dt.Rows)
        {
            CmbUser.Text = dtRow["DocUser"].ToString();
            MskStartDate.Text = dtRow["DocStartMiti"].GetDateString();
            MskEndDate.Text = dtRow["DocEndMiti"].GetDateString();
            if (MskStartDate.Text == MskEndDate.Text)
            {
                MskEndDate.Text = ObjGlobal.CfEndBsDate;
            }
            switch (dtRow["DocType"].ToString())
            {
                case "Alpha":
                case "AN":
                {
                    rChkNumerical.Checked = true;
                    break;
                }
                case "Numeric":
                case "N":
                {
                    rChkAlphaNumber.Checked = true;
                    break;
                }
                case "Auto":
                case "A":
                {
                    rChkAutoNumber.Checked = true;
                    break;
                }
            }

            TxtPrefix.Text = dtRow["DocPrefix"].GetString();
            TxtSufix.Text = dtRow["DocSufix"].GetString();
            TxtBodyLength.Text = dtRow["DocBodyLength"].GetString();
            TxtTotalLength.Text = dtRow["DocTotalLength"].GetString();
            TxtBlank.Text = dtRow["DocBlankCh"].GetString();
            if (dt.Rows[0]["DocBlank"].IsValueExits())
            {
                ChkBlankNumber.Checked = dtRow["DocBlank"].GetBool();
            }

            TxtStart.Text = dtRow["DocStart"].ToString();
            TxtCurrent.Text = dtRow["DocCurr"].ToString();
            TxtEnd.Text = dtRow["DocEnd"].ToString();
            CmbBranch.SelectedValue = dtRow["DocBranch"].GetInt();
            CmbUnit.SelectedValue = dtRow["DocUnit"].GetInt();
            CmbDesign.Text = dtRow["DocDesign"].ToString();
        }
    }

    private int SaveDocumentNumberingDetails()
    {
        _documentId = _actionTag is "SAVE"
            ? _documentId.ReturnMaxIntId("DOCUMENTNUMBERING", string.Empty)
            : _documentId;
        _groupSetup.ObjDocumentNumbering.DocId = _documentId;
        _groupSetup.ObjDocumentNumbering.DocModule = _module;
        _groupSetup.ObjDocumentNumbering.DocUser = CmbUser.Text;
        _groupSetup.ObjDocumentNumbering.DocDesc = TxtDescription.Text.Trim().Replace("'", "''");
        _groupSetup.ObjDocumentNumbering.DocStartDate = MskStartDate.GetEnglishDate(MskStartDate.Text).GetDateTime();
        _groupSetup.ObjDocumentNumbering.DocStartMiti = MskStartDate.Text;
        _groupSetup.ObjDocumentNumbering.DocEndDate = MskEndDate.GetEnglishDate(MskEndDate.Text).GetDateTime();
        _groupSetup.ObjDocumentNumbering.DocEndMiti = MskEndDate.Text;
        _groupSetup.ObjDocumentNumbering.DocPrefix = TxtPrefix.Text;
        _groupSetup.ObjDocumentNumbering.DocSufix = TxtSufix.Text;
        _groupSetup.ObjDocumentNumbering.DocBodyLength = ObjGlobal.ReturnInt(TxtBodyLength.Text);
        _groupSetup.ObjDocumentNumbering.DocTotalLength = ObjGlobal.ReturnDecimal(TxtTotalLength.Text);
        _groupSetup.ObjDocumentNumbering.DocType = rChkNumerical.Checked ? "N" : "A";
        _groupSetup.ObjDocumentNumbering.DocBlankCh = TxtBlank.Text;
        _groupSetup.ObjDocumentNumbering.DocBlank = ChkBlankNumber.Checked;
        _groupSetup.ObjDocumentNumbering.DocBranch = CmbBranch.SelectedValue.GetInt();
        _groupSetup.ObjDocumentNumbering.DocUnit = CmbUnit.SelectedIndex;
        _groupSetup.ObjDocumentNumbering.DocStart = TxtStart.Text.GetDecimal(true);
        _groupSetup.ObjDocumentNumbering.DocCurr = TxtCurrent.Text.GetDecimal(true);
        _groupSetup.ObjDocumentNumbering.DocEnd = TxtEnd.Text.GetDecimal();
        _groupSetup.ObjDocumentNumbering.DocDesign = CmbDesign.Text;
        _groupSetup.ObjDocumentNumbering.Status = ChkActive.Checked;
        _groupSetup.ObjDocumentNumbering.SyncRowVersion =
            _groupSetup.ObjDocumentNumbering.SyncRowVersion.ReturnSyncRowNo("DOCUMENTNUMBERING",
                _documentId.ToString());
        return _groupSetup.SaveDocumentNumbering(_actionTag);
    }
    private async void GetAndSaveUnSynchronizedDocumentNumbering()
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
                GetUrl = @$"{_configParams.Model.Item2}DocumentNumbering/GetDocumentNumberingByCallCount",
                InsertUrl = @$"{_configParams.Model.Item2}DocumentNumbering/InsertDocumentNumberingList",
                UpdateUrl = @$"{_configParams.Model.Item2}DocumentNumbering/UpdateDocumentNumbering",
            };

            DataSyncHelper.SetConfig(apiConfig);
            _injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(_injectData);
            var documentNumberingRepo = DataSyncProviderFactory.GetRepository<DatabaseModule.Setup.DocumentNumberings.DocumentNumbering>(_injectData);

            SplashScreenManager.ShowForm(typeof(PleaseWait));
            // pull all new account data
            var pullResponse = await _groupSetup.PullDocumentNumberingServerToClientByRowCount(documentNumberingRepo, 1);
            SplashScreenManager.CloseForm();

            // push all new account data
            var sqlSaQuery = _groupSetup.GetDocumentNumberingScript();
            var queryResponse = await QueryUtils.GetListAsync<DatabaseModule.Setup.DocumentNumberings.DocumentNumbering>(sqlSaQuery);
            var numberingList = queryResponse.List.ToList();
            if (numberingList.Count > 0)
            {
                SplashScreenManager.ShowForm(typeof(PleaseWait));
                var pushResponse = await documentNumberingRepo.PushNewListAsync(numberingList);
                SplashScreenManager.CloseForm();
            }
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            SplashScreenManager.CloseForm();
        }

    }
    private bool IsValidForm
    {
        get
        {
            if (string.IsNullOrEmpty(TxtDocumentDesc.Text))
            {
                MessageBox.Show(@"DOCUMENT NUMBERING SCHEMA IS REQUIRED...!!", ObjGlobal.Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtDocumentDesc.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(_module))
            {
                MessageBox.Show(@"DOCUMENT NUMBERING SCHEMA IS REQUIRED..!!", ObjGlobal.Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtDocumentDesc.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(TxtDescription.Text))
            {
                MessageBox.Show(@"DOCUMENT NUMBERING NAME IS REQUIRED.", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtDescription.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(TxtBodyLength.Text))
            {
                MessageBox.Show(@"DOCUMENT NUMBERING BODY IS REQUIRED.", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtBodyLength.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(MskStartDate.Text))
            {
                MessageBox.Show(@"DOCUMENT NUMBERING DATE IS REQUIRED.", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                MskStartDate.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(MskEndDate.Text))
            {
                MessageBox.Show(@"DOCUMENT NUMBERING DATE IS REQUIRED.", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                MskEndDate.Focus();
                return false;
            }

            return true;
        }
    }

    private void CmbDesign_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void lblDocUser_Click(object sender, EventArgs e)
    {

    }

    private void lblDOcPrefix_Click(object sender, EventArgs e)
    {

    }

    #endregion --------------- Method ---------------

    //OBJECT FOR THIS FORM

    #region --------------- Class ---------------

    private int _documentId;
    private string _module = string.Empty;
    private string _query = string.Empty;
    private string _actionTag = string.Empty;
    private readonly IDocumentNumberingRepository _groupSetup;
    private DbSyncRepoInjectData _injectData;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
    #endregion --------------- Class ---------------
}