using DatabaseModule.CloudSync;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using MrDAL.Control.SplashScreen;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Domains.Shared.DataSync.Handlers;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.Dialogs;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Properties;
using MrDAL.Setup.CompanySetup;
using MrDAL.Utility.Config;
using MrDAL.Utility.dbMaster;
using MrDAL.Utility.Interface;
using MrDAL.Utility.Licensing;
using MrDAL.Utility.Server;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrClientManagement.Utility
{
    public partial class FrmCompanyList : XtraForm
    {
        public FrmCompanyList()
        {
            InitializeComponent();
            _master = new ClsMasterSetup();
            _recalculate = new ClsRecalculate();
        }

        private void FrmCompanyList_Load(object sender, System.EventArgs e)
        {
            BindCompanyDetail();
            RGrid.Focus();
        }

        #region --------------- Details ---------------

        private void RGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void RGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            _rowIndex = e.RowIndex;
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
                double currentVersion = 0;
                RGrid.Rows[_rowIndex].Selected = true;
                if (DateTime.Now.GetDateTime() < RGrid.Rows[_rowIndex].Cells["LoginDate"].Value.GetDateTime())
                {
                    if (CustomMessageBox.Question(@"YOU ARE USING YOUR SOFTWARE IN BACK DATE.. DO YOU WANT TO CONTINUE..!!") is DialogResult.No)
                    {
                        return;
                    }
                }
                ObjGlobal.InitialCatalog = RGrid.Rows[_rowIndex].Cells["Database_Name"].Value.ToString();
                GetConnection.LoginInitialCatalog = ObjGlobal.InitialCatalog;

                ObjGlobal.CompanyId = RGrid.Rows[_rowIndex].Cells["GComp_Id"].Value.GetInt();
                var dtCompanyInfo = _master.LoginCompanyDataTable();
                if (dtCompanyInfo.Rows.Count > 0)
                {
                    ObjGlobal.LogInCompany = dtCompanyInfo.Rows[0]["Company_Name"].GetString();
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
                    currentVersion = dtCompanyInfo.Rows[0]["Version_No"].GetDouble();
                }
                else
                {
                    ObjGlobal.CompanyId = RGrid.Rows[_rowIndex].Cells["GComp_Id"].Value.GetInt();
                    ObjGlobal.LogInCompany = RGrid.Rows[_rowIndex].Cells["Description"].Value.ToString();
                    ObjGlobal.CompanyPanNo = RGrid.Rows[_rowIndex].Cells["PanNo"].Value.ToString();
                }

                if (currentVersion != ObjGlobal.CompanyVersion.GetDouble())
                {
                    try
                    {
                        SplashScreenManager.ShowForm(typeof(PleaseWait));
                        var exception = SqlExtentions.ExecuteNonQueryIgnoreException(Resources.CrmCreateTable);
                        SplashScreenManager.CloseForm(true);
                        CompanySetupRepository.UpdateCurrentVersion(ObjGlobal.CompanyVersion.ToString());
                    }
                    catch
                    {
                        SplashScreenManager.CloseForm(true);
                        //ignore
                    }
                }
                //var originIdValid = Guid.TryParse(RGrid.SelectedRows[0].Cells["DataSyncOriginId"].Value?.ToString(), out var originId);
                var value = RGrid.Rows[_rowIndex].Cells["LoginDate"].Value;
                if (value != null)
                {
                    ObjGlobal.CompanyLoginDateTime = value.GetDateTime();
                }
                CompanySetupRepository.UpdateLogInDate();
                ObjGlobal.FillSystemConFiguration();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                ex.DialogResult();
                if (CustomMessageBox.ErrorMessage(@"ERROR OCCURS WHILE LOGIN..!!") == DialogResult.Retry)
                {
                    AlterDatabaseTable.CreateDatabaseTableColumn();
                    _recalculate.ShrinkDatabase(ObjGlobal.InitialCatalog);
                    _recalculate.ShrinkDatabaseLog(GetConnection.LoginInitialCatalog);
                }
            }
        }

        private static (bool valid, string Msg) ValidateLicense(Guid? originId)
        {
            LicenseHandler.LoadLicense();
            originId = Guid.Parse(FingerPrint.GetMotherboardUuId());
            var license = LicenseHandler.GetLicense();
            if (license == null || !originId.HasValue)
            {
                return (false, $@"Unable to load license..!!");
            }

            var licenseBranch = license.Branches.FirstOrDefault(x => x.OutletUqId == originId);
            if (licenseBranch == null)
            {
                return (false, $@"License expired on {licenseBranch.ExpDate:D}.");
            }

            return DateTime.Today >= licenseBranch.ExpDate
                ? (false, $@"License expired on {licenseBranch.ExpDate:D}.")
                : (false, string.Empty);
        }

        private void BindCompanyDetail()
        {
            _dt = _master.GetCompanyList();
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
            RGrid.DataSource = _dt;
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

        private async Task<SyncApiConfig> FetchUrlParamsAsync(string baseUrl)
        {
            var client = new HttpClient(new CompressHandler(), true);
            try
            {
                client.BaseAddress = new Uri(baseUrl);
                var json = await client.GetStringAsync("datasync/config");

                var jObject = JObject.Parse(json);
                var success = (bool)jObject["Success"];

                if (success)
                {
                    var token = jObject.SelectToken("Model");
                    if (token != null)
                    {
                        var model = new SyncApiConfig
                        {
                            BaseUrl = baseUrl,
                        };

                        return model;
                    }
                }

                MessageBox.Show(@"Unable to fetch the url configs.", @"Error");
            }
            catch (Exception e)
            {
                e.ToNonQueryErrorResult(this).ShowErrorDialog();
            }

            return null;
        }

        #endregion --------------- Method ---------------

        // OBJECT

        #region --------------- Global Class ---------------

        private int _rowIndex;
        private DataTable _dt = new();
        private DataSet _ds = new();
        private readonly IMasterSetup _master;
        private readonly IRecalculate _recalculate;
        private Guid _clientId;

        #endregion --------------- Global Class ---------------
    }
}