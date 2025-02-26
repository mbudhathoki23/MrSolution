using DatabaseModule.CloudSync;
using DatabaseModule.Master.InventorySetup;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using MrDAL.Control.ControlsEx;
using MrDAL.Control.SplashScreen;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Domains.Interface;
using MrDAL.Domains.Restro.Master;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Domains.Restro.Master;

public partial class FrmTable : MrForm
{
    // TABLE MASTER

    #region "--------- Form ----------"

    public FrmTable(bool isZoom = false)
    {
        InitializeComponent();
        _getForm = new ClsMasterForm(this, BtnExit);
        _objSetup = new ClsMasterSetup();
        _isZoom = isZoom;
        _tableMasterRepository = new TableMasterRepository();
    }

    private void FrmTable_Load(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        BtnView.Enabled = true;
        EnableDisable();
        ClearControl();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() =>
            {
                GetAndSaveUnsynchronizedTableMasters();
            });
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete, BtnView], this.Tag);
    }

    private void FrmTable_KeyPress(object sender, KeyPressEventArgs e)
    {
        switch (e.KeyChar)
        {
            case (char)27:
            {
                if (BtnNew.Enabled == false || BtnEdit.Enabled == false || BtnDelete.Enabled == false)
                {
                    if (MessageBox.Show(@"Are you sure want to ClearControl Form!", "ClearControl Form", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _actionTag = string.Empty;
                        EnableDisable();
                        ClearControl();
                        Text = "Table Details";
                        BtnNew.Focus();
                    }
                }
                else
                {
                    BtnExit.PerformClick();
                }

                break;
            }
        }
    }

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        EnableDisable(true);
        ClearControl();
        TxtDescription.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _actionTag = "UPDATE";
        EnableDisable(true);
        ClearControl();
        TxtDescription.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        EnableDisable();
        ClearControl();
        TxtDescription.Focus();
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
        {
            Close();
        }
    }


    private void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!IsFormValid())
            {
                return;
            }
            if (SaveTableInfo() != 0)
            {
                if (_isZoom)
                {
                    SelectedTable = TxtDescription.Text;
                    Close();
                    return;
                }
                else
                {
                    CustomMessageBox.ActionSuccess(TxtDescription.Text.GetUpper(), "TABLE", _actionTag);
                    ClearControl();
                    TxtDescription.Focus();
                    return;
                }
            }
            else
            {
                CustomMessageBox.ActionError(TxtDescription.Text.GetUpper(), "TABLE", _actionTag);
            }
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            ex.DialogResult();
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
        }
    }

    private void CmbFloor_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 32)
        {
            SendKeys.Send("{F4}");
        }
    }

    private void CmbStatus_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 32) //Action New
        {
            SendKeys.Send("{F4}");
        }
    }

    private void CmbTableType_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 32) //Action New
        {
            SendKeys.Send("{F4}");
        }
    }

    private void BtnDescription_Click(object sender, EventArgs e)
    {
        using var FrmList =
            new FrmAutoPopList("MIN", "TABLE", ObjGlobal.SearchText, _actionTag, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            FrmList.ShowDialog();
            if (FrmList.SelectedList.Count != 0)
                if (!string.IsNullOrEmpty(_actionTag) && _actionTag != "SAVE")
                {
                    TxtDescription.Text = FrmList.SelectedList[0]["TableName"].ToString().Trim();
                    TableId = ObjGlobal.ReturnInt(FrmList.SelectedList[0]["tableId"].ToString().Trim());
                    if (_actionTag != "SAVE")
                    {
                        TxtDescription.ReadOnly = _actionTag is "DELETE" ? true : false;
                        SetGridDataToText(TableId);
                    }
                }

            FrmList.Dispose();
        }
        else
        {
            MessageBox.Show(@"TABLE NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDescription.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtDescription.Focus();
    }

    private void TxtDescription_Leave(object sender, EventArgs e)
    {
        if (ActiveControl != null && string.IsNullOrEmpty(TxtDescription.Text.Trim().Replace("'", "''")) &&
            TxtDescription.Focused && !string.IsNullOrEmpty(_actionTag))
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
            if (string.IsNullOrEmpty(TxtDescription.Text.Trim().Replace("'", "''")) &&
                TxtDescription.Focused is true && TxtDescription.Enabled is true)
            {
                MessageBox.Show(@"DESCRIPTION IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtDescription.Focus();
            }
        }
        else if (_actionTag.ToUpper() != "SAVE" && TxtDescription.ReadOnly is true)
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtDescription, BtnDescription);
        }
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtDescription.Text.Trim().Replace("'", "''")) && _actionTag.ToUpper() is "SAVE")
            TxtShortName.Text = string.IsNullOrEmpty(TxtShortName.Text.Replace("'", "''"))
                ? ObjGlobal.BindAutoIncrementCode("TB", TxtDescription.Text.Trim().Replace("'", "''"))
                : TxtShortName.Text.Replace("'", "''");
        if (!string.IsNullOrEmpty(_actionTag) && _actionTag != "DELETE")
        {
            var dt = _objSetup.CheckIsValidData(_actionTag, "TableMaster", "TableName", "tableId",
                TxtDescription.Text.Trim().Replace("'", "''"), TableId.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                MessageBox.Show(@"DESCRIPTION ALREADY EXITS..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                ClearControl();
                TxtDescription.Focus();
            }
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
        if (!string.IsNullOrEmpty(_actionTag) &&
            string.IsNullOrEmpty(TxtShortName.Text.Trim().Replace("'", "''")) && TxtShortName.Focused is true)
        {
            MessageBox.Show(@"FLOOR SHORTNAME IS BLANK ..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtShortName.Focus();
        }
    }

    private void TxtShortName_Validating(object sender, CancelEventArgs e)
    {
        using var dt = _objSetup.CheckIsValidData(_actionTag, "TableMaster", "TableCode", "tableId",
            TxtShortName.Text.Trim().Replace("'", "''"), TableId.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            MessageBox.Show(@"SHORT NAME ALREADY EXITS..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtShortName.Clear();
            TxtShortName.Focus();
        }
    }

    private void BtnView_Click(object sender, EventArgs e)
    {
        BtnDescription_Click(sender, e);
        BtnView.Focus();
    }
    private void BtnSync_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnsynchronizedTableMasters);
        }
        else
        {
            CustomMessageBox.Warning("PLEASE CONFIG YOUR CLOUD BACK UP SYNC..!!");
        }
    }
    #endregion "--------- Form ----------"

    // METHOD FOR THIS FORM

    #region "---------- Method ----------"

    private void EnableDisable(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable;

        TxtDescription.Enabled = BtnDescription.Enabled = isEnable || _actionTag.Equals("DELETE");
        TxtShortName.Enabled = isEnable;
        CmbFloor.Enabled = isEnable;
        CmbStatus.Enabled = isEnable;
        CmbTableType.Enabled = isEnable;
        ChkActive.Enabled = isEnable;
        BtnSave.Enabled = BtnCancel.Enabled = isEnable || _actionTag.Equals("DELETE");

        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable;
    }

    private void ClearControl()
    {
        Text = _actionTag.IsValueExits() ? $@"TABLE SETUP DETAILS[{_actionTag}]" : @"TABLE SETUP DETAILS";
        BindFloor();
        BindStations();
        ChkActive.Checked = true;
        TxtDescription.Clear();
        TxtShortName.Clear();
        TxtDescription.ReadOnly = !_actionTag.Equals("SAVE") && TxtDescription.Enabled;
    }

    private void BindFloor()
    {
        CmbFloor.DataSource = _objSetup.GetFloorList();
        CmbFloor.DisplayMember = "Description";
        CmbFloor.ValueMember = "LedgerId";
        if (CmbFloor.Items.Count > 0) CmbFloor.SelectedIndex = 0;
    }

    private void SetGridDataToText(int tableId) //, int FloorId)
    {
        var dt = _objSetup.GetMasterTable(_actionTag, tableId);

        if (dt.Rows.Count <= 0) return;
        TxtDescription.Text = dt.Rows[0]["TableName"].ToString();
        TxtShortName.Text = dt.Rows[0]["TableCode"].ToString();
        CmbFloor.SelectedValue = dt.Rows[0]["FloorId"].GetInt();
        ChkIsPrepaid.Checked = dt.Rows[0]["IsPrePaid"].GetBool();
        ChkActive.Checked = dt.Rows[0]["TableStatus"].GetBool();
        CmbStatus.SelectedIndex = CmbStatus.FindString(dt.Rows[0]["TableStatus"].ToString());
        CmbTableType.SelectedIndex = CmbTableType.FindString(dt.Rows[0]["TableType"].ToString());
    }

    private int SaveTableInfo()
    {
        TableId = _actionTag is "SAVE" ? TableId.ReturnMaxIntId("TABLE", string.Empty) : TableId;
        var actionTag = _actionTag;
        _tableMasterRepository.Table.TableId = TableId;
        _tableMasterRepository.Table.TableName = TxtDescription.Text.Trim().Replace("'", "''");
        _tableMasterRepository.Table.TableCode = TxtShortName.Text.Trim().Replace("'", "''");
        _tableMasterRepository.Table.FloorId = CmbFloor.SelectedValue.GetInt();
        _tableMasterRepository.Table.Status = ChkActive.Checked;
        _tableMasterRepository.Table.IsPrePaid = ChkIsPrepaid.Checked;
        _tableMasterRepository.Table.TableStatus = CmbStatus.SelectedValue.ToString();
        _tableMasterRepository.Table.TableType = CmbTableType.SelectedValue.ToString();
        _tableMasterRepository.Table.Printed = 0;
        _tableMasterRepository.Table.SyncRowVersion = _tableMasterRepository.Table.SyncRowVersion.ReturnSyncRowNo("TABLE", TableId.ToString());

        return _tableMasterRepository.SaveTable(_actionTag);

    }

    private void BindStations()
    {
        var statusDataSource = new List<ValueModel<string, string>>
        {
            new("Available", "A"),
            new("Occupied", "O"),
            new("Booked", "B"),
            new("Reserve", "R"),
            new("Repair", "M")
        };
        CmbStatus.DataSource = statusDataSource;
        CmbStatus.DisplayMember = "Item1";
        CmbStatus.ValueMember = "Item2";
        CmbStatus.SelectedIndex = 0;

        var tableDataSource = new List<ValueModel<string, string>>
        {
            new("Dinning", "D"),
            new("Split", "S"),
            new("Take Away", "T"),
            new("Delivery", "R")
        };
        CmbTableType.DataSource = tableDataSource;
        CmbTableType.DisplayMember = "Item1";
        CmbTableType.ValueMember = "Item2";
        CmbTableType.SelectedIndex = 0;
    }

    private bool IsFormValid()
    {
        if (string.IsNullOrEmpty(TxtDescription.Text))
        {
            MessageBox.Show(@"TABLE DESCRIPTION IS REQUIRED..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            TxtDescription.Focus();
            return false;
        }

        if (string.IsNullOrEmpty(TxtShortName.Text))
        {
            MessageBox.Show(@"TABLE CODE IS REQUIRED..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            TxtShortName.Focus();
            return false;
        }

        return true;
    }

    private async void GetAndSaveUnsynchronizedTableMasters()
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
                GetUrl = @$"{_configParams.Model.Item2}TableMaster/GetTableMastersByCallCount",
                InsertUrl = @$"{_configParams.Model.Item2}TableMaster/InsertTableMasterList",
                UpdateUrl = @$"{_configParams.Model.Item2}TableMaster/UpdateTableMaster"
            };

            DataSyncHelper.SetConfig(apiConfig);
            _injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(_injectData);
            var currencyRepo = DataSyncProviderFactory.GetRepository<TableMaster>(_injectData);

            SplashScreenManager.ShowForm(typeof(PleaseWait));
            //pull all new table master data
            var pullResponse = await _tableMasterRepository.PullTableMasterServerToClientByRowCounts(currencyRepo, 1);
            SplashScreenManager.CloseForm();
            // push all new table master data
            var sqlCrQuery = _tableMasterRepository.GetMasterTable();
            var queryResponse = await QueryUtils.GetListAsync<TableMaster>(sqlCrQuery);
            var curList = queryResponse.List.ToList();
            if (curList.Count > 0)
            {
                SplashScreenManager.ShowForm(typeof(PleaseWait));
                var pushResponse = await currencyRepo.PushNewListAsync(curList);
                SplashScreenManager.CloseForm();
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    #endregion "---------- Method ----------"

    // OBJECT FOR THIS FORM

    #region "---------- Class ----------"

    public int TableId;
    public string SelectedTable = string.Empty;
    private string _actionTag;
    private readonly bool _isZoom;
    private readonly IMasterSetup _objSetup;
    private ClsMasterForm _getForm;
    private InfoResult<ValueModel<string, string, Guid>> _configParams = new();
    private DbSyncRepoInjectData _injectData = new();
    private readonly ITableMasterRepository _tableMasterRepository;

    #endregion "---------- Class ----------"
}