using DatabaseModule.CloudSync;
using DevExpress.XtraSplashScreen;
using MrDAL.Control.SplashScreen;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Models.Common;
using MrDAL.Setup.CompanySetup;
using MrDAL.Setup.Interface;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatabaseModule.Setup.CompanyMaster;

namespace MrBLL.Setup.CompanySetup;

public partial class FrmFiscalYear : MrForm
{
    // FISCAL YEAR

    #region --------------- Form ---------------

    public FrmFiscalYear()
    {
        // _master = new ClsMasterSetup();
        _fiscalYear = new FiscalYearRepository();
        InitializeComponent();
    }

    private void FrmFiscalYear_Load(object sender, EventArgs e)
    {
        BindFiscalYear();
        ObjGlobal.DGridColorCombo(RGrid);
        RGrid.Focus();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedFiscalYear);
        }
    }

    private void FrmFiscalYear_FormClosing(object sender, FormClosingEventArgs e)
    {
    }

    private void FrmFiscalYear_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape)
        {
            if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
            {
                Close();
            }
        }
    }

    private void RGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
        var currentColumn = e.ColumnIndex;
    }

    private void RGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
        _rowIndex = e.RowIndex;
    }

    private void RGrid_KeyDown(object sender, KeyEventArgs e)
    {
        try
        {
            if (e.KeyCode != Keys.Enter || RGrid.Rows.Count == 0)
            {
                return;
            }
            e.SuppressKeyPress = true;
            BtnLogin.PerformClick();
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }
    }

    private void RGrid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        BtnLogin.PerformClick();
    }

    private void RGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        BtnLogin.PerformClick();
    }

    private void RGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        BtnLogin.PerformClick();
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (RGrid.Rows.Count > 0)
        {
            LoginFiscalYearId = RGrid.SelectedRows[0].Cells[0].Value.GetInt();
            ObjGlobal.SysFiscalYearId = LoginFiscalYearId;
            Dispose(true);
        }
        else
        {
            CustomMessageBox.Warning("PLEASE SELECT THE FISCAL YEAR");
            RGrid.Focus();
            return;
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }
    private void BtnSync_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedFiscalYear);
        }
        else
        {
            CustomMessageBox.Warning("PLEASE CONFIG YOUR CLOUD BACK UP SYNC..!!");
        }
    }
    #endregion --------------- Form ---------------

    //METHOD FOR THE

    #region --------------- Method ---------------
    private void BindFiscalYear()
    {
        var iRows = 0;
        var dtFiscalYear = _fiscalYear.GetMasterFiscalYear();
        if (dtFiscalYear.RowsCount() is 0)
        {
            CustomMessageBox.Information("THERE IS NO ANY OTHER FISCAL YEAR");
            Close();
            return;
        }

        RGrid.Rows.Add(dtFiscalYear.Rows.Count);
        foreach (DataRow ro in dtFiscalYear.Rows)
        {
            RGrid.Rows[iRows].Cells["GTxtFiscalYearId"].Value = ro["FiscalYearId"].ToString();
            RGrid.Rows[iRows].Cells["GTxtFiscalYear"].Value = ObjGlobal.SysDateType == "D" ? ro["AD_FY"].ToString() : ro["BS_FY"].ToString();
            RGrid.Rows[iRows].Cells["GMskStartDate"].Value = ObjGlobal.SysDateType == "D" ? ro["Start_ADDate"].ToString() : ro["Start_BSDate"].ToString();
            RGrid.Rows[iRows].Cells["GMskEndDate"].Value = ObjGlobal.SysDateType == "D" ? ro["End_ADDate"].ToString() : ro["End_BSDate"].ToString();
            iRows++;
        }
    }
    private async void GetAndSaveUnSynchronizedFiscalYear()
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
                GetUrl = @$"{_configParams.Model.Item2}FiscalYear/GetFiscalYearByCallCount",
                InsertUrl = @$"{_configParams.Model.Item2}FiscalYear/InsertFiscalYearList",
                UpdateUrl = @$"{_configParams.Model.Item2}FiscalYear/UpdateFiscalYear"
            };

            DataSyncHelper.SetConfig(apiConfig);
            _injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(_injectData);
            var yearRepo = DataSyncProviderFactory.GetRepository<FiscalYear>(_injectData);

            SplashScreenManager.ShowForm(typeof(PleaseWait));
            //pull all new fiscal year data
            var pullResponse = await _fiscalYear.PullFiscalYearServerToClientByRowCount(yearRepo, 1);
            SplashScreenManager.CloseForm();
            // push all new fiscal year data
            var sqlCrQuery = _fiscalYear.GetFiscalYearScript();
            var queryResponse = await QueryUtils.GetListAsync<FiscalYear>(sqlCrQuery);
            var yearList = queryResponse.List.ToList();
            if (yearList.Count > 0)
            {
                SplashScreenManager.ShowForm(typeof(PleaseWait));
                var pushResponse = await yearRepo.PushNewListAsync(yearList);
                SplashScreenManager.CloseForm();
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    #endregion --------------- Method ---------------

    // GLOBAL OBJECT

    #region --------------- Global ---------------

    private int _rowIndex;
    public int LoginFiscalYearId = 0;
    private readonly IFiscalYear _fiscalYear;
    private InfoResult<ValueModel<string, string, Guid>> _configParams = new();
    private DbSyncRepoInjectData _injectData = new();
    #endregion --------------- Global ---------------
}