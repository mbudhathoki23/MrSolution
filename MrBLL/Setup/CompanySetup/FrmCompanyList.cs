using DevExpress.XtraSplashScreen;
using MrDAL.Control.SplashScreen;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Utility.Config;
using MrDAL.Utility.dbMaster;
using MrDAL.Utility.Interface;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using MrDAL.Setup.CompanySetup;

namespace MrBLL.Setup.CompanySetup;

public partial class FrmCompanyList : MrForm
{
    #region --------------- Form ---------------

    public FrmCompanyList(bool zoom)
    {
        InitializeComponent();
        BindCompanyDetail();
        ObjGlobal.DGridColorCombo(RGrid);
        Size = RGrid.RowCount > 10 ? new Size(984, 583) : new Size(984, 400);
        _zoom = zoom;
    }

    private void FrmCompanyList_Load(object sender, EventArgs e)
    {
        RGrid.Focus();
    }

    private void FrmCompanyList_Shown(object sender, EventArgs e)
    {
        if (RGrid.Rows.Count == 0)
        {
            var result = new FrmCompanySetup(true);
            result.ShowDialog();
        }
        else if (RGrid.Rows.Count > 0)
        {
            btn_NewCompany.Enabled = false;
            RGrid.Focus();
        }
        else
        {
            Application.Exit();
        }
    }

    private void BtnNewCompany_Click(object sender, EventArgs e)
    {
        var result = new FrmCompanySetup(true);
        result.ShowDialog();
        if (result.DialogResult == DialogResult.OK)
        {
            BindCompanyDetail();
        }
    }

    private void FrmCompanyList_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.N)
        {
            var result = new FrmCompanySetup(true);
            result.ShowDialog();
            if (result.DialogResult == DialogResult.OK)
            {
                BindCompanyDetail();
            }
        }
    }

    private void FrmCompanyList_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)8)
        {
            if (TxtSearch.Text.Length > 0)
            {
                TxtSearch.Text = TxtSearch.Text.Substring(0, TxtSearch.Text.Length - 1);
            }
        }
        else if (e.KeyChar == (char)27)
        {
            if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
            {
                if (_zoom)
                {
                    Dispose(true);
                    return;
                }
                Environment.Exit(0);
            }
        }
        else
        {
            TxtSearch.Text += e.KeyChar.ToString();
        }
    }

    #endregion --------------- Form ---------------

    #region --------------- Details ---------------

    private void RGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
        currentColumn = e.ColumnIndex;
    }

    private void RGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
        rowIndex = e.RowIndex;
    }

    private void RGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter || RGrid.Rows.Count <= 0)
        {
            return;
        }
        e.SuppressKeyPress = true;
        LoginSelectedCompany();
    }

    private void RGrid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        LoginSelectedCompany();
    }

    private void RGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        LoginSelectedCompany();
    }

    private void RGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        LoginSelectedCompany();
    }

    private void RGrid_Sorted(object sender, EventArgs e)
    {
        RGrid.FirstDisplayedCell = RGrid.CurrentCell;
    }

    private void RGrid_EnterKeyPressed(object sender, EventArgs e)
    {
        RGrid_KeyDown(RGrid, new KeyEventArgs(Keys.Enter));
    }

    #endregion --------------- Details ---------------

    #region --------------- Method ---------------

    private void LoginSelectedCompany()
    {
        try
        {
            decimal currentVersion = 0;
            RGrid.Rows[rowIndex].Selected = true;
            if (DateTime.Now.GetDateTime() < RGrid.Rows[rowIndex].Cells["LoginDate"].Value.GetDateTime())
            {
                if (CustomMessageBox.Question(@"YOU ARE USING YOUR SOFTWARE IN BACK DATE.. DO YOU WANT TO CONTINUE..!!") is DialogResult.No)
                {
                    return;
                }
            }
            ObjGlobal.InitialCatalog = RGrid.Rows[rowIndex].Cells["Database_Name"].Value.ToString();
            GetConnection.LoginInitialCatalog = ObjGlobal.InitialCatalog;

            ObjGlobal.CompanyId = RGrid.Rows[rowIndex].Cells["GComp_Id"].Value.GetInt();
                
            var dtCompanyInfo = _master.LoginCompanyDataTable();
            if (dtCompanyInfo.Rows.Count > 0)
            {
                ObjGlobal.LogInCompany = dtCompanyInfo.Rows[0]["Company_Name"].GetTrimApostrophe();
                ObjGlobal.CompanyPrintDesc = dtCompanyInfo.Rows[0]["PrintDesc"].GetString();
                ObjGlobal.CompanyLogo = dtCompanyInfo.Rows[0]["Company_Logo"].GetByte();
                ObjGlobal.RegistrationDate = dtCompanyInfo.Rows[0]["CReg_Date"].GetDateTime();
                ObjGlobal.CompanyAddress = dtCompanyInfo.Rows[0]["Address"].GetString();
                ObjGlobal.CompanyCountry = dtCompanyInfo.Rows[0]["Country"].GetString();
                ObjGlobal.CompanyState = dtCompanyInfo.Rows[0]["State"].GetString();
                ObjGlobal.CompanyCity = dtCompanyInfo.Rows[0]["City"].GetString();
                ObjGlobal.CompanyPhoneNo = dtCompanyInfo.Rows[0]["PhoneNo"].GetString();
                ObjGlobal.CompanyFaxNo = dtCompanyInfo.Rows[0]["Fax"].GetString();
                ObjGlobal.CompanyPanNo = dtCompanyInfo.Rows[0]["Pan_No"].GetString();
                ObjGlobal.CompanyEmailAddress = dtCompanyInfo.Rows[0]["Email"].GetString();
                ObjGlobal.CompanyWebSites = dtCompanyInfo.Rows[0]["Website"].GetString();
                ObjGlobal.SoftwareModule = dtCompanyInfo.Rows[0]["SoftModule"].GetString();
                ObjGlobal.IsOnlineSync = dtCompanyInfo.Rows[0]["IsSyncOnline"].GetBool();
                ObjGlobal.IsIrdRegister = dtCompanyInfo.Rows[0]["IsTaxRegister"].GetBool();
                ObjGlobal.LocalOriginId = dtCompanyInfo.Rows[0]["ApiKey"].GetGuid();
                currentVersion = dtCompanyInfo.Rows[0]["Version_No"].GetDecimal();
            }
            else
            {
                ObjGlobal.CompanyId = RGrid.Rows[rowIndex].Cells["GComp_Id"].Value.GetInt();
                ObjGlobal.LogInCompany = RGrid.Rows[rowIndex].Cells["Description"].Value.ToString();
                ObjGlobal.CompanyPanNo = RGrid.Rows[rowIndex].Cells["PanNo"].Value.ToString();
            }

            if (currentVersion < ObjGlobal.CompanyVersion.GetDecimal())
            {
                try
                {
                    SplashScreenManager.ShowForm(typeof(PleaseWait));
                    var updateDatabase = CompanySetupRepository.UpdateDatabase();
                    var updateCurrentVersion = CompanySetupRepository.UpdateCurrentVersion(ObjGlobal.CompanyVersion.ToString());
                    SplashScreenManager.CloseForm(true);
                }
                catch
                {
                    SplashScreenManager.CloseForm(true);
                    //ignore
                }
            }
            else
            {
                var result = _master.IsExitsCheckDocumentNumbering("SB");
                if (result.Rows.Count == 0)
                {
                    AlterDatabaseTable.InsertDocumentNumbering();
                }
            }
            //var originIdValid = Guid.TryParse(RGrid.SelectedRows[0].Cells["DataSyncOriginId"].Value?.ToString(), out var originId);
            var value = RGrid.Rows[rowIndex].Cells["LoginDate"].Value;
            if (value != null)
            {
                ObjGlobal.CompanyLoginDateTime = value.GetDateTime();
            }
            CompanySetupRepository.UpdateLogInDate();
            ObjGlobal.FillSystemConFiguration();
            DialogResult = DialogResult.OK;
            Task.Run(UpdateSoftwareRegistrationFromServer);
        }
        catch (Exception ex)
        {
            ex.DialogResult();
            if (MessageBox.Show(@"ERROR OCCURS WHILE LOGIN..!!", ObjGlobal.Caption, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
            {
                AlterDatabaseTable.CreateDatabaseTableColumn();
                CompanySetupRepository.UpdateCurrentVersion(ObjGlobal.CompanyVersion.ToString());
                _recalculate.ShrinkDatabase(ObjGlobal.InitialCatalog);
                _recalculate.ShrinkDatabaseLog(GetConnection.LoginInitialCatalog);
            }
        }
    }

    private static async Task<bool> UpdateSoftwareRegistrationFromServer()
    {
        ////sync
        //try
        //{
        //    var configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
        //    if (!configParams.Success || configParams.Model.Item1 == null)
        //    {
        //        configParams.Model = new ValueModel<string, string, Guid>
        //        {
        //            Item1 = string.Empty,
        //            Item2 = ObjGlobal.CloudSyncBaseUrl,
        //            Item3 = Guid.NewGuid()
        //        };
        //    }

        //    var injectData = new DbSyncRepoInjectData
        //    {
        //        ExternalConnectionString = null,
        //        DateTime = DateTime.Now,
        //        LocalOriginId = configParams.Model.Item1,
        //        LocalCompanyUnitId = ObjGlobal.SysCompanyUnitId,
        //        Username = ObjGlobal.LogInUser,
        //        LocalConnectionString = GetConnection.ConnectionString,
        //        LocalBranchId = ObjGlobal.SysBranchId,
        //        ApiConfig = DataSyncHelper.GetConfig()
        //    };

        //    var apiConfig = new SyncApiConfig
        //    {
        //        BaseUrl = configParams.Model.Item2,
        //        Apikey = configParams.Model.Item3,
        //        Username = ObjGlobal.LogInUser,
        //        BranchId = ObjGlobal.SysBranchId,
        //        GetUrl = @$"{configParams.Model.Item2}SoftwareRegistration/GetSoftwareRegistrationByClientDescription?clientDesc={ObjGlobal.LogInCompany.GetUpper()}",
        //        InsertUrl = @$"{configParams.Model.Item2}SoftwareRegistration/InsertSoftwareRegistration",
        //        UpdateUrl = @$"{configParams.Model.Item2}SoftwareRegistration/UpdateSoftwareRegistration",
        //    };

        //    DataSyncHelper.SetConfig(apiConfig);
        //    injectData.ApiConfig = apiConfig;
        //    DataSyncManager.SetGlobalInjectData(injectData);
        //    var softwareRegistrationRepo = DataSyncProviderFactory.GetRepository<SoftwareRegistration>(DataSyncManager.GetGlobalInjectData());
        //    var result = await softwareRegistrationRepo?.GetUnSynchronizedDataAsync()!;
        //    if (result.List?[0] == null)
        //    {
        //        var query = $@"SELECT *FROM master.AMS.SoftwareRegistration";
        //        var queryResult = await ExecuteCommand.ExecuteDataSetOnMasterAsync(query);
        //        if (queryResult.Tables[0].Rows.Count <= 0)
        //        {
        //            return false;
        //        }
        //        var table = queryResult.Tables[0].Rows[0];
        //        //sync
        //        try
        //        {
        //            DataSyncHelper.SetConfig(apiConfig);
        //            injectData.ApiConfig = apiConfig;
        //            DataSyncManager.SetGlobalInjectData(injectData);
        //            var syncRepository = DataSyncProviderFactory.GetRepository<SoftwareRegistration>(DataSyncManager.GetGlobalInjectData());
        //            var sr = new SoftwareRegistration
        //            {
        //                CustomerId = table["CustomerId"].GetString(),
        //                RegistrationId = table["RegistrationId"].GetGuid(),
        //                ClientDescription = table["ClientDescription"].GetString(),
        //                ClientAddress = table["ClientAddress"].GetString(),
        //                ClientSerialNo = table["ClientSerialNo"].GetString(),
        //                Reguestby = table["Reguestby"].GetString(),
        //                RegisterBy = table["RegisterBy"].GetString(),
        //                RegistrationDate = table["RegistrationDate"].GetString(),
        //                RegistrationDays = table["RegistrationDays"].GetString(),
        //                ExpiredDate = table["ExpiredDate"].GetString(),
        //                ProductDescription = table["ProductDescription"].GetString(),
        //                NoOfNodes = table["NoOfNodes"].GetString(),
        //                Module = table["Module"].GetString(),
        //                System_Id = table["System_Id"].GetString(),
        //                ActivationCode = table["ActivationCode"].GetString(),
        //                Server_MacAdd = table["Server_MacAdd"].GetString(),
        //                Server_Desc = table["Server_Desc"].GetString(),
        //                IsOnline = table["IsOnline"].GetBool()
        //            };
        //            var result1 = await softwareRegistrationRepo?.PushNewAsync(sr)!;

        //            return result1.Value;
        //        }
        //        catch (Exception ex)
        //        {
        //            ex.ToNonQueryErrorResult(ex.StackTrace);
        //            return false;
        //        }
        //        return false;
        //    }
        //    foreach (var data in result.List)
        //    {
        //        var query = $@"SELECT *FROM Master.AMS.SoftwareRegistration WHERE RegistrationId = '{data.RegistrationId}'";
        //        var queryResult = await ExecuteCommand.ExecuteDataSetOnMasterAsync(query);
        //        var cmdTxt = string.Empty;
        //        if (queryResult.Tables.Count == 0)
        //        {
        //            cmdTxt += " INSERT INTO [master].[AMS].[SoftwareRegistration]([CustomerId],[RegistrationId],[ClientDescription],[ClientAddress],[ClientSerialNo],[Reguestby],[RegisterBy],[RegistrationDate],[RegistrationDays],[ExpiredDate],[ProductDescription],[NoOfNodes],[Module],[System_Id],[ActivationCode],[Server_MacAdd],[Server_Desc],[IsOnline] )";
        //            cmdTxt += " VALUES (";
        //            cmdTxt += $"'{data.CustomerId}','{data.RegistrationId}','{data.ClientDescription}','{data.ClientAddress}',";
        //            cmdTxt += $"'{data.ClientSerialNo}','{data.Reguestby}','{data.RegisterBy}',";
        //            cmdTxt += $"'{data.RegistrationDate}','{data.RegistrationDays}','{data.ExpiredDate}',";
        //            cmdTxt += $"'{data.ProductDescription}','{data.NoOfNodes}', ";
        //            cmdTxt += $"'{data.Module}', ";
        //            cmdTxt += $"'{data.System_Id}',";
        //            cmdTxt += $"'{data.ActivationCode}',";
        //            cmdTxt += $"'{data.Server_MacAdd}','{data.Server_Desc}',";
        //            cmdTxt += $"{data.IsOnline}";
        //            cmdTxt += " )";
        //        }
        //        else
        //        {
        //            cmdTxt = $" UPDATE Master.AMS.SoftwareRegistration SET ExpiredDate='{data.ExpiredDate}' WHERE RegistrationId = '{data.RegistrationId}'";
        //        }
        //        var cmd = ExecuteCommand.ExecuteNonQueryIgnoreExceptionMaster(cmdTxt);
        //    }
        //    return true;
        //}
        //catch (Exception ex)
        //{
        //    var errMsg = ex.Message;
        //    return false;
        //}

        return true;
    }

    private void BindCompanyDetail()
    {
        dt = _master.GetCompanyList();
        if (RGrid.Columns.Count > 0)
        {
            RGrid.Columns.Clear();
        }
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("GComp_Id", "GComp_Id", "GComp_Id", 0, 2, false);
        RGrid.AddColumn("Database_Name", "Database_Name", "Database_Name", 0, 2, false);
        RGrid.AddColumn("Description", "DESCRIPTION", "Description", 250, 200, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("PanNo", "PAN NO", "PanNo", 150, 100, true);
        RGrid.AddColumn("Address", "ADDRESS", "Address", 150, 100, false);
        RGrid.AddColumn("DataSyncOriginId", "DataSyncOriginId", "DataSyncOriginId", 150, 100, false);
        RGrid.AddColumn("DataSyncApiBaseUrl", "DataSyncApiBaseUrl", "DataSyncApiBaseUrl", 150, 100, false);
        RGrid.AddColumn("LoginDate", "LoginDate", "LoginDate", 150, 100, false);
        RGrid.DataSource = dt;
    }

    private void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        Search();
    }

    private void Search()
    {
        try
        {
            ((DataTable)RGrid.DataSource).DefaultView.RowFilter = $"Description LIKE '{TxtSearch.Text.Trim()}%'";
            if (RGrid.Rows.Count != 0 || TxtSearch.Text != string.Empty)
            {
                return;
            }
            BindCompanyDetail();
            ObjGlobal.DGridColorCombo(RGrid);
        }
        catch
        {
            // ignored
        }
    }

    private void TxtSearch_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    #endregion --------------- Method ---------------

    // OBJECT

    #region --------------- Global Class ---------------

    private int rowIndex;
    private int currentColumn;
    private bool _zoom;

    private DataTable dt = new DataTable();
    private readonly IMasterSetup _master = new ClsMasterSetup();
    private readonly IRecalculate _recalculate = new ClsRecalculate();

    #endregion --------------- Global Class ---------------
}