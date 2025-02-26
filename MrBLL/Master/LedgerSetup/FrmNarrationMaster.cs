using DatabaseModule.CloudSync;
using DatabaseModule.Master.FinanceSetup;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
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
using MrDAL.Master.FinanceSetup;
using MrDAL.Master.Interface;
using MrDAL.Master.Interface.FinanceSetup;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Master.LedgerSetup;

public partial class FrmNarrationMaster : MrForm
{
    private void FrmNarrationMaster_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (BtnNew.Enabled is false || BtnEdit.Enabled is false || BtnDelete.Enabled is false)
            {
                if (MessageBox.Show(@"ARE YOU SURE WANT TO CLEAR FORM..??", ObjGlobal.Caption,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _actionTag = string.Empty;
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



    #region --------------- Frm ---------------

    public FrmNarrationMaster(bool IsZoom)
    {
        InitializeComponent();
        clsFormControl = new ClsMasterForm(this, BtnExit);
        _actionTag = string.Empty;
        _Zoom = IsZoom;
    }

    private void NarrationMaster_Load(object sender, EventArgs e)
    {
        ClearControl();
        EnableControl();
        CmbType.Text = "Both";
        BtnNew.Focus();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() => { GetAndSaveUnsynchronizedNarration(); });
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }

    #endregion --------------- Frm ---------------

    #region--------------- Method ---------------

    private void EnableControl(bool Txt = false, bool Btn = true)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = Btn;

        TxtDescription.Enabled = BtnDescription.Enabled =
            !string.IsNullOrEmpty(_actionTag) && _actionTag != "SAVE" ? true : Txt;
        CmbType.Enabled = Txt;
        ChkActive.Enabled = !string.IsNullOrEmpty(_actionTag) && _actionTag == "UPDATE" ? true : false;
        BtnSave.Enabled =
            BtnCancel.Enabled = !string.IsNullOrEmpty(_actionTag) && _actionTag != "SAVE" ? true : Txt;
    }

    private void ClearControl()
    {
        Text = $"NARRATION DETAILS SETUP {_actionTag} ".Trim();
        IList list = StorePanel.Controls;
        for (var i = 0; i < list.Count; i++)
        {
            var TxtControl = (Control)list[i];
            if (TxtControl is TextBox)
            {
                TxtControl.Text = string.Empty;
                TxtControl.BackColor = SystemColors.Window;
            }
        }

        ChkActive.Checked = true;
        TxtDescription.ReadOnly =
            !string.IsNullOrEmpty(_actionTag) && _actionTag.ToUpper() != "SAVE" ? true : false;
    }

    private int SaveNarrationMaster()
    {
        short syncRow = 1;
        _nrMaster.Narration.NRID = _actionTag is "SAVE" ? NRID.ReturnMaxIntId("NRM") : NRID;
        _nrMaster.Narration.NRDESC = TxtDescription.Text.Trim();
        switch (CmbType.SelectedIndex.ToString())
        {
            case "0":
            {
                _nrMaster.Narration.NRTYPE = "RE";
                break;
            }
            case "1":
            {
                _nrMaster.Narration.NRTYPE = "NA";
                break;
            }
            case "2":
            {
                _nrMaster.Narration.NRTYPE = "BO";
                break;
            }
        }


        _nrMaster.Narration.IsActive = ChkActive.Checked;
        _nrMaster.Narration.EnterBy = ObjGlobal.LogInUser;
        _nrMaster.Narration.EnterDate = DateTime.Now;
        _nrMaster.Narration.CompanyUnitId = ObjGlobal.SysCompanyUnitId;
        _nrMaster.Narration.BranchId = ObjGlobal.SysBranchId;
        syncRow = syncRow.ReturnSyncRowNo("NRM", NRID);
        var result = _nrMaster.SaveNarration(_actionTag);
        return result;
    }

    protected void SetGridDataToText(int NRID)
    {
        var dt = _nrMaster.GetMasterNarration(_actionTag, NRID);
        if (dt.Rows.Count <= 0) return;
        _nrMaster.Narration.NRID = NRID;
        TxtDescription.Text = dt.Rows[0]["NRDESC"].ToString();
        switch (dt.Rows[0]["NRTYPE"].ToString())
        {
            case "RE":
            {
                CmbType.Text = "Remarks";
                break;
            }
            case "NA":
            {
                CmbType.Text = "Narration";
                break;
            }
            case "BO":
            {
                CmbType.Text = "Both";
                break;
            }
        }

        bool.TryParse(dt.Rows[0]["IsActive"].ToString(), out var _Check);
        ChkActive.Checked = _Check;
        if (_actionTag == "UPDATE")
            TxtDescription.Focus();
        else
            BtnSave.Focus();
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
        {
            Close();
        }
    }

    #endregion

    #region--------------- Botton ---------------

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

    private bool IsValid()
    {
        if (_actionTag.IsBlankOrEmpty())
        {
            return false;
        }

        if (TxtDescription.IsBlankOrEmpty())
        {
            TxtDescription.WarningMessage(@"NARRATION & REMARKS NAME IS REQUIRED...!");
            return false;
        }

        if (String.IsNullOrEmpty(_actionTag))
        {
            return false;
        }

        if (string.IsNullOrEmpty(CmbType.Text))
        {
            CmbType.WarningMessage(@"NARRATION & REMARKS TYPE IS REQUIRED...!");
            return false;
        }

        if (_actionTag != "SAVE") ;

        return true;
    }


    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (IsValid())
        {
            TxtDescription.Focus();

            if (SaveNarrationMaster() > 0)
            {
                if (_Zoom is false)
                {
                    NarrationMasterDetails = TxtDescription.Text;
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
            MessageBox.Show($@"DATA {_actionTag.ToUpper()}  UNSUCCESSFULLY..!!", ObjGlobal.Caption,
                MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
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
            Task.Run(GetAndSaveUnsynchronizedNarration);
        }
        else
        {
            CustomMessageBox.Warning("PLEASE CONFIG YOUR CLOUD BACK UP SYNC..!!");
        }
    }
    private async void GetAndSaveUnsynchronizedNarration()
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
                GetUrl = @$"{_configParams.Model.Item2}NrMaster/GetNrMastersByCallCount",
                InsertUrl = @$"{_configParams.Model.Item2}NrMaster/InsertNrMasterList",
                UpdateUrl = @$"{_configParams.Model.Item2}NrMaster/UpdateNrMaster"
            };

            DataSyncHelper.SetConfig(apiConfig);
            _injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(_injectData);
            var narrationRepo = DataSyncProviderFactory.GetRepository<NR_Master>(_injectData);
            SplashScreenManager.ShowForm(typeof(PleaseWait));

            // pull all new narration data
            var pullResponse = await _nrMaster.PullNarrationMasterServerToClientByRowCount(narrationRepo, 1);
            SplashScreenManager.CloseForm();

            // push all new narration data
            var sqlBrQuery = _nrMaster.GetNarrationMasterScript();
            var queryResponse = await QueryUtils.GetListAsync<NR_Master>(sqlBrQuery);
            var narrations = queryResponse.List.ToList();
            if (narrations.Count > 0)
            {
                SplashScreenManager.ShowForm(typeof(PleaseWait));
                var pushResponse = await narrationRepo.PushNewListAsync(narrations);
                SplashScreenManager.CloseForm();

            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    #endregion

    #region--------------- Event ---------------

    private void TxtDescription_Leave(object sender, EventArgs e)
    {
        if (ActiveControl is not null && string.IsNullOrEmpty(TxtDescription.Text.Trim().Replace("'", "''")) &&
            TxtDescription.Focused && !string.IsNullOrEmpty(_actionTag))
        {
            MessageBox.Show(@"DESCRIPTION IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDescription.Focus();
        }
    }

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
                TxtDescription.WarningMessage("NARRATION DESCRIPTION IS BLANK..!!");
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
        var frmPickList =
            new FrmAutoPopList("MIN", "NRMASTER", ObjGlobal.SearchText, _actionTag, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtDescription.Text = frmPickList.SelectedList[0]["NRDESC"].ToString().Trim();
                NRID = Convert.ToInt32(frmPickList.SelectedList[0]["NRID"].ToString().Trim());
                if (_actionTag != "SAVE")
                {
                    TxtDescription.ReadOnly = false;
                    SetGridDataToText(NRID);
                }
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"COULDN'T FIND ANY NARRATION OR REMARKS..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDescription.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtDescription.Focus();
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (!string.IsNullOrEmpty(_actionTag) && _actionTag != "DELETE")
        {
            var dt = _nrMaster.CheckIsValidData(_actionTag, "NR_Master", "NRDESC", "NRID",
                TxtDescription.Text.Trim().Replace("'", "''"), NRID.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                MessageBox.Show(@"DESCRIPTION ALREADY EXITS..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtDescription.Focus();
            }
        }
    }

    private void cmbType_Leave(object sender, EventArgs e)
    {
        ObjGlobal.ComboBoxBackColor(CmbType, 'L');
    }

    private void cmbType_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (CmbType.Text.Trim() != string.Empty)
            {
            }
            else
            {
                MessageBox.Show(@"Narration & Remarks  Description Cannot Left  Blank, Plz. Enter The Value !",
                    ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtDescription.Focus();
            }
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
    }

    private void Global_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    #endregion

    #region --------------- Global Class ---------------

    public string NarrationMasterDetails = string.Empty;

    private int NRID;
    private readonly bool _Zoom;

    private string _SearchKey;
    private string _actionTag = string.Empty;
    private string Query = string.Empty;
    private string Searchtext = string.Empty;

    private DataTable dt = new();
    private readonly INarrationRemarksRepository _nrMaster = new NarrationRemarksRepository();
    private IMasterSetup _setup;
    private ClsMasterForm clsFormControl;
    private object CmbStatus;
    private InfoResult<ValueModel<string, string, Guid>> _configParams = new();
    private DbSyncRepoInjectData _injectData = new();

    #endregion --------------- Global Class ---------------
}