using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using MrDAL.Control.SplashScreen;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Domains.CRM.Master;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.ImportExport.Implementations;
using MrDAL.Master;
using MrDAL.Master.Interface;
using System;
using System.Data;
using System.Windows.Forms;

namespace MrClientManagement.Master.ExcelImport
{
    public partial class FrmClientExcelImport : XtraForm
    {
        public FrmClientExcelImport()
        {
            InitializeComponent();
            _master = new ClsMasterSetup();
            _export = new ExcelDataImportExport();
            _crmMaster = new CrmMaster();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            if (RGrid.RowCount <= 0)
            {
                CustomMessageBox.Warning("PLEASE LOAD DATA FROM EXCEL TO IMPORT");
                return;
            }
            try
            {
                SplashScreenManager.ShowForm(typeof(PleaseWait));
                SaveClientInformation();
                SplashScreenManager.CloseForm();
                CustomMessageBox.Information("DATA IMPORT SUCCESSFULLY..!!");
            }
            catch (Exception ex)
            {
                ex.DialogResult();
                SplashScreenManager.CloseForm();
            }
        }

        private void BtnSample_Click(object sender, EventArgs e)
        {
            try
            {
                using var folderDlg = new FolderBrowserDialog
                {
                    ShowNewFolderButton = true
                };
                var result = folderDlg.ShowDialog();
                if (result is not DialogResult.OK)
                {
                    return;
                }
                var table = new DataTable();
                foreach (DataGridViewColumn column in RGrid.Columns)
                {
                    table.Columns.Add(column.HeaderText, column.CellType);
                }

                var fileName = folderDlg.SelectedPath;
                _export.Export(table, fileName, "Client Sample");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnUpload_Click(object sender, EventArgs e)
        {
            using var fileDialog = new OpenFileDialog
            {
                Filter = @"Excel Worksheets|*.xlsx"
            };
            if (fileDialog.ShowDialog() != DialogResult.OK) return;
            try
            {
                SplashScreenManager.ShowForm(typeof(PleaseWait));
                //var table = _master.ReadExcelFile(Path.GetFullPath(fileDialog.FileName), "Product");
                //if (table == null)
                //{
                //    return;
                //}
                //RGrid.DataSource = table;
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                ex.ToNonQueryErrorResult(ex);
                SplashScreenManager.CloseForm(false);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
        }

        // METHOD
        private void SaveClientInformation()
        {
            foreach (DataGridViewRow rGridRow in RGrid.Rows)
            {
                try
                {
                    var description = rGridRow.Cells["GTxtCompany"].Value.GetString();
                    if (description.IsBlankOrEmpty())
                    {
                        continue;
                    }

                    var result = _crmMaster.AlreadyExits(description, "ClientCollection", "ClientDescription");
                    if (result)
                    {
                        _crmMaster.ClientInfo.ActionTag = "UPDATE";
                        _crmMaster.ClientInfo.ClientId = _crmMaster.GetIdFromValue(description, "ClientCollection");
                    }
                    else
                    {
                        _crmMaster.ClientInfo.ActionTag = "SAVE";
                        _crmMaster.ClientInfo.ClientId = _crmMaster.ReturnMaxIdFromTable("ClientCollection");
                    }
                    _crmMaster.ClientInfo.ClientDescription = description;
                    _crmMaster.ClientInfo.PanNo = rGridRow.Cells["GTxtPanNo"].Value.GetDecimal();
                    _crmMaster.ClientInfo.ClientAddress = rGridRow.Cells["GTxtAddress"].Value.GetString();
                    _crmMaster.ClientInfo.EmailAddress = rGridRow.Cells["GTxtEmailAddress"].Value.GetString();
                    _crmMaster.ClientInfo.ContactNo = rGridRow.Cells["GTxtContactNo"].Value.GetString();
                    _crmMaster.ClientInfo.PhoneNo = rGridRow.Cells["GTxtPhoneNo"].Value.GetString();
                    _crmMaster.ClientInfo.CollectionSource = rGridRow.Cells["GTxtSource"].Value.GetString();
                    _crmMaster.ClientInfo.Status = true;
                    _crmMaster.ClientInfo.EnterBy = ObjGlobal.LogInUser;
                    _crmMaster.ClientInfo.EnterDate = DateTime.Now;
                    _crmMaster.SaveClientCollection();
                }
                catch (Exception e)
                {
                    e.ToNonQueryErrorResult(e);
                }
            }
        }

        // OBJECT FOR THIS FORM
        private readonly ExcelDataImportExport _export;

        private readonly IMasterSetup _master;
        private ICrmMaster _crmMaster;
    }
}