using DatabaseModule.CloudSync;
using DatabaseModule.Master.ProductSetup;
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
using MrDAL.Master.Interface.ProductSetup;
using MrDAL.Master.ProductSetup;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Master.ProductSetup;

public partial class FrmProductUnit : MrForm
{
    // PRODUCT UNIT SETUP
    #region -------------- PRODUCT UNIT --------------
    public FrmProductUnit(bool zoomFrm = false)
    {
        InitializeComponent();
        _actionTag = string.Empty;
        _isZoom = zoomFrm;
        ClearControl();
        EnableControl();

        _injectData = new DbSyncRepoInjectData();
        _unitRepository = new ProductUnitRepository();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }

    private void FrmProductUnit_Load(object sender, EventArgs e)
    {
        BtnNew.Focus();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedProductUnits);
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete, BtnView], this.Tag);
    }

    private void FrmProductUnit_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (!BtnNew.Enabled)
            {
                if (CustomMessageBox.ClearVoucherDetails("PRODUCT UNIT") == DialogResult.Yes)
                {
                    _actionTag = string.Empty;
                    ClearControl();
                    EnableControl();
                    BtnNew.Focus();
                }
            }
            else
            {
                if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
                {
                    Close();
                    return;
                }
            }
        }
    }

    private void BtnSync_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedProductUnits);
        }
        else
        {
            CustomMessageBox.Warning("PLEASE CONFIG YOUR CLOUD BACK UP SYNC..!!");
        }
    }

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        ClearControl();
        EnableControl(true);
        TxtDescription.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _actionTag = "UPDATE";
        ClearControl();
        EnableControl(true);
        TxtDescription.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        ClearControl();
        EnableControl();
        TxtDescription.Focus();
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (IsValidForm())
        {
            if (SaveProductUnit() > 0)
            {
                if (_isZoom)
                {
                    ProductUnitName = TxtDescription.Text.Trim().Replace("'", "''");
                    Close();
                    return;
                }
                CustomMessageBox.ActionSuccess(TxtDescription.Text.GetUpper(), "PRODUCT UNIT", _actionTag);
                ClearControl();
                TxtDescription.Focus();
            }
        }
        else
        {
            TxtDescription.ErrorMessage($"ERROR OCCURS WHILE [{TxtDescription}] [{_actionTag}]");
            TxtDescription.Focus();
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtDescription.Text.Trim()))
        {
            Close();
        }
        else ClearControl();
    }

    private void TxtDescription_Leave(object sender, EventArgs e)
    {
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
                TxtDescription.WarningMessage("PRODUCT UNIT DESCRIPTION IS BLANK..!!");
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
        var (description, id) = GetMasterList.GetProductUnit(_actionTag);
        if (description.IsValueExits())
        {
            TxtDescription.Text = description;
            ProductUnitId = id;
            if (_actionTag != "SAVE")
            {
                TxtDescription.ReadOnly = false;
                SetGridDataToText();
            }
        }
        TxtDescription.Focus();
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (TxtDescription.IsValueExits() && _actionTag.IsValueExits())
        {
            TxtShortName.Text = TxtDescription.GenerateShortName("ProductUnit", "UnitCode");
            var result = TxtDescription.IsDuplicate("UnitName", ProductUnitId, _actionTag, "PU");
            if (result)
            {
                TxtDescription.WarningMessage("PRODUCT UNIT DESCRIPTION IS ALREADY EXITS..!!");
                return;
            }
        }
        else if (TxtDescription.IsBlankOrEmpty() && TxtDescription.Enabled)
        {
            if (ActiveControl == BtnDescription)
            {
                return;
            }
            if (TxtDescription.ValidControl(ActiveControl) && _actionTag.IsValueExits())
            {
                TxtDescription.WarningMessage("PRODUCT UNIT DESCRIPTION IS REQUIRED..!!");
            }
        }
    }

    private void TxtShortName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (TxtShortName.IsBlankOrEmpty())
            {
                TxtShortName.WarningMessage("PRODUCT UNIT SHORT NAME IS REQUIRED ");
                return;
            }
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
    }

    private void TxtShortName_Leave(object sender, EventArgs e)
    {
    }

    private void TxtShortName_Validating(object sender, EventArgs e)
    {
        if (TxtShortName.IsValueExits())
        {
            if (_actionTag.IsValueExits())
            {
                var result = TxtShortName.CheckValueExits(_actionTag, "PRODUCTUNIT", "UnitCode", ProductUnitId);
                if (result.Rows.Count > 0)
                {
                    TxtShortName.WarningMessage("PRODUCT UNIT SHORTNAME IS ALREADY EXITS");
                    return;
                }
            }
        }
        if (TxtShortName.IsBlankOrEmpty() && _actionTag.IsValueExits())
        {
            TxtShortName.WarningMessage($"PRODUCT UNIT SHORTNAME IS REQUIRED FOR [{_actionTag}]");
        }
    }

    private void BtnView_Click(object sender, EventArgs e)
    {
        GetMasterList.GetProductUnit("VIEW");
    }

    private void Global_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
        {
            Close();
        }
    }

    #endregion -------------- EVENT --------------

    // METHOD FOR THIS FORM
    #region -------------- Method --------------
    private void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable;

        TxtDescription.Enabled = _actionTag.IsValueExits() && _actionTag != "SAVE" || isEnable;
        BtnDescription.Enabled = TxtDescription.Enabled;
        TxtShortName.Enabled = isEnable;
        ChkActive.Enabled = _actionTag.IsValueExits() && _actionTag != "SAVE";
        BtnSave.Enabled = _actionTag.IsValueExits() && _actionTag != "SAVE" || isEnable;
        BtnCancel.Enabled = BtnSave.Enabled;
    }

    private void ClearControl()
    {
        Text = _actionTag.IsBlankOrEmpty() ? "PRODUCT UNIT SETUP" : $"PRODUCT UNIT SETUP [{_actionTag}]";
        TxtDescription.Clear();
        TxtShortName.Clear();
        TxtDescription.ReadOnly = _actionTag.ToUpper() != "SAVE";
        ChkActive.Checked = true;
    }

    private void SetGridDataToText()
    {
        using var dt = _unitRepository.GetMasterProductUnit(_actionTag, string.Empty, 1, ProductUnitId);
        if (dt == null || dt.Rows.Count <= 0) return;
        ProductUnitId = dt.Rows[0]["UID"].GetInt();
        TxtDescription.Text = dt.Rows[0]["UnitName"].ToString();
        TxtShortName.Text = dt.Rows[0]["UnitCode"].ToString();
        ChkActive.Checked = dt.Rows[0]["Status"].GetBool();
    }

    private int SaveProductUnit()
    {
        ProductUnitId = _actionTag is "SAVE" ? ProductUnitId.ReturnMaxIntId("PU") : ProductUnitId;
        _unitRepository.ObjProductUnit.UID = ProductUnitId;
        _unitRepository.ObjProductUnit.UnitName = TxtDescription.Text.Trim().Replace("'", "''");
        _unitRepository.ObjProductUnit.UnitCode = TxtShortName.Text.Trim().Replace("'", "''");
        _unitRepository.ObjProductUnit.Branch_ID = ObjGlobal.SysBranchId;
        _unitRepository.ObjProductUnit.EnterBy = ObjGlobal.LogInUser;
        _unitRepository.ObjProductUnit.EnterDate = DateTime.Now;
        _unitRepository.ObjProductUnit.Status = ChkActive.Checked;
        _unitRepository.ObjProductUnit.SyncRowVersion = _unitRepository.ObjProductUnit.SyncRowVersion.ReturnSyncRowNo("PU", ProductUnitId.ToString());
        return _unitRepository.SaveProductUnit(_actionTag);
    }

    private async void GetAndSaveUnSynchronizedProductUnits()
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
            GetUrl = @$"{_configParams.Model.Item2}ProductUnit/GetProductUnitsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}ProductUnit/InsertProductUnitList",
            UpdateUrl = @$"{_configParams.Model.Item2}ProductUnit/UpdateProductUnit"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var productUnitRepo = DataSyncProviderFactory.GetRepository<ProductUnit>(_injectData);
        SplashScreenManager.ShowForm(typeof(PleaseWait));
        // pull all new account data
        var pullResponse = await _unitRepository.PullProductUnitsServerToClientByRowCount(productUnitRepo, 1);
        SplashScreenManager.CloseForm();

        // push all new account data
        var sqlQuery = _unitRepository.GetProductUnitScript();
        var queryResponse = await QueryUtils.GetListAsync<ProductUnit>(sqlQuery);
        var uList = queryResponse.List.ToList();
        if (uList.Count > 0)
        {
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            var pushResponse = await productUnitRepo.PushNewListAsync(uList);
            SplashScreenManager.CloseForm();

        }
    }

    private bool IsValidForm()
    {
        if (string.IsNullOrEmpty(_actionTag))
        {
            return false;
        }
        if (TxtDescription.IsBlankOrEmpty())
        {
            TxtDescription.WarningMessage("DESCRIPTION IS REQUIRED..!!");
            return false;
        }
        if (TxtShortName.IsBlankOrEmpty())
        {
            TxtShortName.WarningMessage("SHORT NAME IS REQUIRED..!!");
            return false;
        }
        return true;
    }
    #endregion -------------- Method --------------

    // OBJECT FOR THIS GLOBAL
    #region -------------- GLOBAL VARIABLE--------------
    public string ProductUnitName = string.Empty;
    public int ProductUnitId;
    private string _actionTag;
    private readonly bool _isZoom;
    private readonly IProductUnitRepository _unitRepository;
    private readonly DbSyncRepoInjectData _injectData;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
    #endregion -------------- GLOBAL VARIABLE--------------
}