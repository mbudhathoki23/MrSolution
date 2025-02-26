using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraSplashScreen;
using MrClientManagement.Entry;
using MrClientManagement.Master;
using MrClientManagement.Master.ExcelImport;
using MrClientManagement.Utility;
using MrDAL.Control.SplashScreen;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Properties;
using MrDAL.Utility.dbMaster;
using MrDAL.Utility.Server;
using System;
using System.Windows.Forms;
using MrDAL.Setup.CompanySetup;

namespace MrClientManagement
{
    public partial class MainClientInformation : RibbonForm
    {
        [Obsolete]
        public MainClientInformation()
        {
            InitializeComponent();
            _master = new ClsMasterSetup();
        }

        [Obsolete("Obsolete")]
        private void MainClientInformation_Load(object sender, EventArgs e)
        {
            var login = new FrmLogin();
            login.ShowDialog();
            if (login.DialogResult == DialogResult.OK)
            {
                var result = new FrmCompanyList();
                result.ShowDialog();
                if (result.DialogResult != DialogResult.OK)
                {
                    Application.Exit();
                }
                else
                {
                    BindCompanyInformation();
                    BindClientInformation();
                }
            }
            else
            {
                Application.Exit();
            }
        }

        private void MainClientInformation_Shown(object sender, EventArgs e)
        {
            BindClientInformation();
        }

        private void MnuOpenCompany_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var result = new FrmCompanyList();
            result.ShowDialog();
            if (result.DialogResult == DialogResult.OK)
            {
                BindCompanyInformation();
            }
        }

        private void BtnUpdateCompany_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void MnuClientSource_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var result = new FrmClientSource();
            result.ShowDialog();
        }

        private void BtnCreateClient_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var result = new FrmClientInformation();
            result.ShowDialog();
        }

        private void MnuImportClient_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var result = new FrmClientExcelImport();
            result.ShowDialog();
        }

        private void MnuExportClient_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExportToExcel();
        }

        private void MnuRoleUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var result = new FrmRoleUser();
            result.ShowDialog();
        }

        private void MnuRoleAssign_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var result = new FrmRoleAssign();
            result.ShowDialog();
        }

        private void MnuTaskStatus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var result = new FrmTaskStatus();
            result.ShowDialog();
        }

        private void MnuTaskType_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var result = new FrmTaskType();
            result.ShowDialog();
        }

        private void MnuTaskManagement_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var result = new FrmTaskManagement();
            result.ShowDialog();
        }

        // METHOD FOR THIS FORM
        private void BindClientInformation()
        {
            //var dt = ObjGlobal.LogInUserCategory switch
            //{
            //    _ => _master.GetGeneralLedger("SAVE","");
            //};

            //DGridControl.DataSource = dt;
        }

        private void BindCompanyInformation()
        {
            ObjGlobal.GetFiscalYearDetails();
            LblInitial.Caption = ObjGlobal.InitialCatalog.IsValueExits() ? @"[INITIAL]: " + ObjGlobal.InitialCatalog : @"INITIAL";
            LblCompanyInfo.Caption = ObjGlobal.LogInCompany.IsValueExits() ? @"[COMPANY]: " + ObjGlobal.LogInCompany : @"COMPANY";
            LblStartDate.Caption = ObjGlobal.CfStartBsDate.IsValueExits() ? @"[START DATE]: " + ObjGlobal.CfStartBsDate : @"START DATE";
            LblEndDate.Caption = ObjGlobal.CfEndBsDate.IsValueExits() ? @"[END DATE]: " + ObjGlobal.CfEndBsDate : @"END DATE";
            LblUserInfo.Caption = ObjGlobal.LogInUser.IsValueExits() ? @"[LOGIN USER]: " + ObjGlobal.LogInUser : @"LOGIN USER";
        }

        private void ExportToExcel()
        {
            if (RGrid.RowCount > 0)
            {
                RGrid.ExportToXlsx(Application.ExecutablePath);
                CustomMessageBox.Information("EXPORT SUCCESSFULLY..!!");
            }
        }

        // OBJECT FOR THIS FROM
        private IMasterSetup _master;
    }
}