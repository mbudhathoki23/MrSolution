using DatabaseModule.CloudSync;
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
using MrDAL.Models.Common;
using MrDAL.SystemSetting;
using MrDAL.SystemSetting.Interface;
using MrDAL.Utility.Config;
using MrDAL.Utility.Interface;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.SystemSetting;

public partial class FrmInventorySetting : MrForm
{
    // INVENTORY SETTING

    #region --------------- From ---------------

    public FrmInventorySetting()
    {
        InitializeComponent();
        BindINegativeStockWarning();
        FillSystemConfiguration();
        _inventorySettingRepository = new InventorySettingRepository();
    }

    private void FrmInventorySetting_Load(object sender, EventArgs e)
    {
        ChkDepartment_CheckedChanged(sender, e);
        ChkICostCenter_CheckStateChanged(sender, e);
        ChkGodownItemEnable_CheckedChanged(sender, e);
        ChkCostCenterItem_CheckedChanged(sender, e);
        ChkGodownEnable_CheckStateChanged(sender, e);
        ChkGodownEnable_CheckStateChanged(sender, e);
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() =>
            {
                GetAndSaveUnsynchronizedSystemSetting();
            });
        }
    }

    private void FrmInventorySetting_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (CustomMessageBox.ExitActiveForm() is DialogResult.Yes)
            {
                Close();
            }
        }
        else if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void BtnOpeningStock_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetGeneralLedger("SAVE", "OTHER");
        {
            TxtOpeningStock.Text = description;
            _openingLedgerId = id;
        }
        TxtOpeningStock.Focus();
    }

    private void TxtOpeningStockPl_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnOpeningStock_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtOpeningStock.IsBlankOrEmpty())
            {
                TxtOpeningStock.WarningMessage(@"OPENING STOCK LEDGER IS BLANK..!!");
                return;
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtOpeningStock, BtnOpeningStock);
        }
    }

    private void TxtClosingStockPL_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnClosingStock_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id) = GetMasterList.CreateGeneralLedger("OTHER", true);
            if (id > 0)
            {
                TxtClosingStock.Text = description;
                _closingLedgerId = id;
            }
            TxtClosingStock.Focus();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtClosingStock.Enabled)
            {
                TxtClosingStock.WarningMessage(@"CLOSING STOCK LEDGER IS BLANK..!!");
                return;
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtClosingStock, BtnClosingStock);
        }
    }

    private void TxtClosingStockBS_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnStockInHand_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtStockInHand.Enabled)
            {
                TxtStockInHand.WarningMessage("STOCK LEDGER IS BLANK..!!");
                return;
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtStockInHand, BtnStockInHand);
        }
    }

    private void ChkICostCenter_CheckStateChanged(object sender, EventArgs e)
    {
        if (ChkCostCenter.Checked)
        {
            ChkCostCenterMandatory.Enabled = true;
        }
        else
        {
            ChkCostCenterMandatory.Enabled = false;
            if (ChkCostCenterMandatory.Checked)
            {
                ChkCostCenterMandatory.Checked = false;
            }
        }
    }

    private void ChkGodownItemEnable_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkGodownItemEnable.Checked)
        {
            ChkGodownItemMandatory.Enabled = true;
        }
        else
        {
            ChkGodownItemMandatory.Enabled = false;
            if (ChkGodownItemMandatory.Checked)
            {
                ChkGodownItemMandatory.Checked = false;
            }
        }
    }

    private void ChkCostCenterItem_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkCostCenterItem.Checked)
        {
            ChkCostCenterItemMandatory.Enabled = true;
        }
        else
        {
            ChkCostCenterItemMandatory.Enabled = false;
            if (ChkCostCenterItemMandatory.Checked)
            {
                ChkCostCenterItemMandatory.Checked = false;
            }
        }
    }

    private void ChkGodownEnable_CheckStateChanged(object sender, EventArgs e)
    {
        if (ChkGodownEnable.Checked)
        {
            ChkGodownMandatory.Enabled = true;
        }
        else
        {
            ChkGodownMandatory.Enabled = true;
            if (ChkGodownMandatory.Checked)
            {
                ChkGodownMandatory.Checked = false;
            }
        }
    }

    private void BtnClosingStock_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetGeneralLedger("SAVE");
        if (id > 0)
        {
            TxtClosingStock.Text = description;
            _closingLedgerId = id;
        }
        TxtClosingStock.Focus();
    }

    private void ChkDepartment_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkDepartment.Checked)
        {
            ChkDepartmentMandatory.Enabled = false;
            ChkDepartmentItem.Enabled = false;
            if (ChkDepartment.Checked)
            {
                ChkDepartment.Checked = false;
            }
        }
        else
        {
            ChkDepartmentItem.Enabled = true;
            if (ChkDepartmentMandatory.Checked)
            {
                ChkDepartmentMandatory.Enabled = false;
            }
        }
    }

    private void BtnStockInHand_Click(object sender, EventArgs e)
    {
        (TxtStockInHand.Text, _stockInHandLedgerId) = GetMasterList.GetGeneralLedger("SAVE");
    }

    private void TextBoxCtrl_Leave(object sender, EventArgs e)
    {
        // BackColor = Color.AliceBlue;
    }

    private void TextBoxCtrl_Enter(object sender, EventArgs e)
    {
        // BackColor = Color.Honeydew;
    }

    private void Global_EnterKey(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void Btn_Save_Click(object sender, EventArgs e)
    {
        if (!IsControlValid()) return;
        if (SaveInventorySetting() != 0) ObjGlobal.FillSystemConFiguration();
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (!IsControlValid()) return;
        if (SaveInventorySetting() != 0)
        {
            ObjGlobal.FillSystemConFiguration();
        }
        Close();
        return;
    }

    #endregion --------------- From ---------------

    // METHOD FOR THIS FORM

    #region --------------- Method ---------------

    private void FillSystemConfiguration()
    {
        var dtStock = _setup.GetInventorySetting();
        if (dtStock.Rows.Count <= 0) return;
        foreach (DataRow dr in dtStock.Rows)
        {
            _openingLedgerId = dr["OPLedgerId"].GetLong();
            TxtOpeningStock.Text = dr["OPLedger"].ToString();
            _closingLedgerId = dr["CSPLLedgerId"].GetLong();
            TxtClosingStock.Text = dr["PLLedger"].ToString();
            _stockInHandLedgerId = dr["CSBSLedgerId"].GetLong();
            TxtStockInHand.Text = dr["BSLedger"].ToString();

            var value = dr["NegativeStock"].ToString();
            CmbNegativeStockWarning.SelectedIndex = CmbNegativeStockWarning.FindString(value);

            ChkAltUnitEnable.Checked = dr["AlternetUnit"].GetBool();
            ChkCostCenter.Checked = dr["CostCenterEnable"].GetBool();
            ChkCostCenterMandatory.Checked = dr["CostCenterMandetory"].GetBool();
            ChkCostCenterItem.Checked = dr["CostCenterItemEnable"].GetBool();
            ChkCostCenterItemMandatory.Checked = dr["CostCenterItemMandetory"].GetBool();
            ChkUnitEnable.Checked = dr["ChangeUnit"].GetBool();
            ChkGodownEnable.Checked = dr["GodownEnable"].GetBool();
            ChkGodownMandatory.Checked = dr["GodownMandetory"].GetBool();
            ChkRemarksEnable.Checked = dr["RemarksEnable"].GetBool();
            ChkGodownItemEnable.Checked = dr["GodownItemEnable"].GetBool();
            ChkGodownItemMandatory.Checked = dr["GodownItemMandetory"].GetBool();
            ChkNarrationEnable.Checked = dr["NarrationEnable"].GetBool();
            ChkShortNameWise.Checked = dr["ShortNameWise"].GetBool();
            ChkCaryBatchQty.Checked = dr["BatchWiseQtyEnable"].GetBool();
            ChkExpdate.Checked = dr["ExpiryDate"].GetBool();
            ChkFreeQty.Checked = dr["FreeQty"].GetBool();
            ChkGroupWiseFilter.Checked = dr["GroupWiseFilter"].GetBool();
            ChkGodownWiseStock.Checked = dr["GodownWiseStock"].GetBool();

            ChkDepartment.Checked = dr["DepartmentMandatory"].GetBool();
            ChkDepartmentItem.Checked = dr["DepartmentItemMandatory"].GetBool();
            ChkGodownWiseStock.Checked = dr["DepartmentItemEnable"].GetBool();
            ChkGodownWiseStock.Checked = dr["DepartmentEnable"].GetBool();
        }
    }

    private bool IsControlValid()
    {
        if (_openingLedgerId is 0 || TxtOpeningStock.IsBlankOrEmpty())
        {
            TxtOpeningStock.WarningMessage("OPENING STOCK LEDGER IS BLANK..!!");
            return false;
        }
        if (_closingLedgerId is 0 || TxtClosingStock.IsBlankOrEmpty())
        {
            TxtClosingStock.WarningMessage(@"CLOSING STOCK LEDGER IS BLANK ..!!");
            return false;
        }
        if (_stockInHandLedgerId is 0 || TxtStockInHand.IsBlankOrEmpty())
        {
            TxtStockInHand.WarningMessage("STOCK LEDGER IS BLANK..!!");
            return false;
        }

        return true;
    }

    private void BindINegativeStockWarning()
    {
        var list = new List<ValueModel<string, int>>
        {
            new("Block", 1),
            new("Ignore", 2),
            new("Warning", 3)
        };
        if (list.Count <= 0) return;
        CmbNegativeStockWarning.DataSource = list;
        CmbNegativeStockWarning.DisplayMember = "Item1";
        CmbNegativeStockWarning.ValueMember = "Item2";
        CmbNegativeStockWarning.SelectedIndex = 2;
    }

    private int SaveInventorySetting()
    {
        _inventorySettingRepository.VmStock.InvId = 1;
        _inventorySettingRepository.VmStock.OPLedgerId = _openingLedgerId;
        _inventorySettingRepository.VmStock.CSPLLedgerId = _closingLedgerId;
        _inventorySettingRepository.VmStock.CSBSLedgerId = _stockInHandLedgerId;
        _inventorySettingRepository.VmStock.NegativeStock = CmbNegativeStockWarning.Text.Substring(0, 1);
        _inventorySettingRepository.VmStock.AlternetUnit = ChkAltUnitEnable.Checked;
        _inventorySettingRepository.VmStock.CostCenterEnable = ChkCostCenter.Checked;
        _inventorySettingRepository.VmStock.CostCenterMandetory = ChkCostCenterMandatory.Checked;
        _inventorySettingRepository.VmStock.CostCenterItemEnable = ChkCostCenterItem.Checked;
        _inventorySettingRepository.VmStock.CostCenterItemMandetory = ChkCostCenterItemMandatory.Checked;
        _inventorySettingRepository.VmStock.ChangeUnit = ChkUnitEnable.Checked;
        _inventorySettingRepository.VmStock.GodownEnable = ChkGodownEnable.Checked;
        _inventorySettingRepository.VmStock.GodownMandetory = ChkGodownMandatory.Checked;
        _inventorySettingRepository.VmStock.RemarksEnable = ChkRemarksEnable.Checked;
        _inventorySettingRepository.VmStock.GodownItemEnable = ChkGodownItemEnable.Checked;
        _inventorySettingRepository.VmStock.GodownItemMandetory = ChkGodownItemMandatory.Checked;
        _inventorySettingRepository.VmStock.NarrationEnable = ChkNarrationEnable.Checked;
        _inventorySettingRepository.VmStock.ShortNameWise = ChkShortNameWise.Checked;
        _inventorySettingRepository.VmStock.BatchWiseQtyEnable = ChkCaryBatchQty.Checked;
        _inventorySettingRepository.VmStock.ExpiryDate = ChkExpdate.Checked;
        _inventorySettingRepository.VmStock.FreeQty = ChkFreeQty.Checked;
        _inventorySettingRepository.VmStock.GroupWiseFilter = ChkGroupWiseFilter.Checked;
        _inventorySettingRepository.VmStock.GodownWiseStock = ChkGodownWiseStock.Checked;
        return _inventorySettingRepository.SaveInventorySetting("");
    }

    private async void GetAndSaveUnsynchronizedSystemSetting()
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
                GetUrl = @$"{_configParams.Model.Item2}InventorySetting/GetInventorySettingByCallCount",
                InsertUrl = @$"{_configParams.Model.Item2}InventorySetting/InsertInventorySettingList",
                UpdateUrl = @$"{_configParams.Model.Item2}InventorySetting/UpdateInventorySetting"
            };

            DataSyncHelper.SetConfig(apiConfig);
            _injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(_injectData);
            var settingRepo = DataSyncProviderFactory.GetRepository<DatabaseModule.Setup.SystemSetting.InventorySetting>(_injectData);

            SplashScreenManager.ShowForm(typeof(PleaseWait));
            //pull all new InventorySetting data
            var pullResponse = await _inventorySettingRepository.PullInventorySettingServerToClientByRowCounts(settingRepo, 1);
            SplashScreenManager.CloseForm();
            // push all new InventorySetting data
            var sqlCrQuery = _inventorySettingRepository.GetInventoryScript();
            var queryResponse = await QueryUtils.GetListAsync<DatabaseModule.Setup.SystemSetting.InventorySetting>(sqlCrQuery);
            var curList = queryResponse.List.ToList();
            if (curList.Count > 0)
            {
                SplashScreenManager.ShowForm(typeof(PleaseWait));
                var pushResponse = await settingRepo.PushNewListAsync(curList);
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

    private long _openingLedgerId;
    private long _closingLedgerId;
    private long _stockInHandLedgerId;
    private readonly ISetup _setup = new ClsSetup();
    private readonly IInventorySettingRepository _inventorySettingRepository;
    private DbSyncRepoInjectData _injectData = new();
    private InfoResult<ValueModel<string, string, Guid>> _configParams = new();
    #endregion --------------- Global ---------------
}