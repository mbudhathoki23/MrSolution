using ClosedXML.Excel;
using DevExpress.XtraSplashScreen;
using Microsoft.Office.Interop.Excel;
using MoreLinq.Extensions;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.SplashScreen;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.Dialogs;
using MrDAL.Master.Interface;
using MrDAL.Master.Interface.SystemSetup;
using MrDAL.Master.SystemSetup;
using MrDAL.Models.Custom;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;

namespace MrBLL.Master.Import;

public partial class FrmProductUpdate : MrForm
{
    private readonly IProductUpdateRepository _groupSetup;
    private IMasterSetup _setup;

    public FrmProductUpdate()
    {
        _groupSetup = new ProductUpdateRepository();
        InitializeComponent();
    }

    private void FrmProductUpdate_Load(object sender, EventArgs e)
    {
        bsImport.Clear();
        DGrid.Rows.Clear();
    }

    private void BtnFileBrowser_Click(object sender, EventArgs e)
    {
        using var fileDialog = new OpenFileDialog
        {
            Filter = @"Excel Worksheets|*.xlsx"
        };
        if (fileDialog.ShowDialog() != DialogResult.OK) return;
        try
        {
            bsImport.DataSource = null;

            SplashScreenManager.ShowForm(typeof(PleaseWait));
            var table = _groupSetup.ReadExcelFile(Path.GetFullPath(fileDialog.FileName), "Product");
            if (table == null) return;
            bsImport.DataSource = table.ToList<ImportProductEModel>().Where(x => !string.IsNullOrWhiteSpace(x.Description)).ToList();
            SplashScreenManager.CloseForm();
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            SplashScreenManager.CloseForm(false);
        }
    }

    private void BtnDownloadFormat_Click(object sender, EventArgs e)
    {
        var format = _groupSetup.DownloadFormat();
        if (format == null)
        {
            return;
        }
        DataTableToExcel(format, "Product");
    }

    private void DataTableToExcel(DataTable dataTable, string sheetName)
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
        using var wb = new XLWorkbook();
        wb.Worksheets.Add(dataTable, sheetName);
        wb.Author = ObjGlobal.Caption;
        wb.SaveAs($"{folderDlg.SelectedPath}\\Product List.xlsx");

        //_groupSetup.DataGridToExcel(dataTable, folderDlg.SelectedPath, "Product List")
    }

    public void FormattingExcelCells(Range range, string htmLColorCode, Color fontColor, bool isFontbool)
    {
        range.Interior.Color = ColorTranslator.FromHtml(htmLColorCode);
        range.Font.Color = ColorTranslator.ToOle(fontColor);
        if (isFontbool)
        {
            range.Font.Bold = true;
        }
    }

    private void BtnDownload_Click(object sender, EventArgs e)
    {
        using var folderDlg = new FolderBrowserDialog
        {
            ShowNewFolderButton = true
        };
        if (folderDlg.ShowDialog() is not DialogResult.OK) return;
        SplashScreenManager.ShowForm(typeof(PleaseWait));
        using var table = _groupSetup.GetProductListForImportFormat();
        _groupSetup.DataGridToExcel(table, folderDlg.SelectedPath, "Product List");
        SplashScreenManager.CloseForm();
        CustomMessageBox.Information(@"PRODUCT LIST DOWNLOADED SUCCESSFULLY.");
    }

    public void GridToExcel1(DataTable dt, string folderPath)
    {
        using var workbook = new XLWorkbook();
        workbook.Worksheets.Add(dt, "Product");
        workbook.SaveAs(folderPath + "\\Product.xlsx");
    }

    private void BtnUpdate_Click(object sender, EventArgs e)
    {
        SaveProductUnit("SAVE", string.Empty);
    }

    private async void btnSave_Click(object sender, EventArgs e)
    {
        //SaveProductUnit("SAVE", string.Empty);
        if (DGrid.Rows.Count == 0 || bsImport.Count == 0)
        {
            // show no items to import
            this.NotifyError("No record to process data import");
            return;
        }
        var models = bsImport.List.OfType<ImportProductEModel>().ToList();
        const string errorMessage = "One more more rows doesn't have value for required field:";
        if (models.Any(x => string.IsNullOrWhiteSpace(x.Description)))
        {
            this.NotifyError($@"{errorMessage} 'Description'.");
            return;
        }

        if (models.Any(x => string.IsNullOrWhiteSpace(x.UOM)))
        {
            this.NotifyError($@"{errorMessage} 'UOM'.");
            return;
        }

        if (models.Any(x => string.IsNullOrWhiteSpace(x.Group)))
        {
            this.NotifyError($@"{errorMessage} 'Group'.");
            return;
        }

        if (models.Any(x => string.IsNullOrWhiteSpace(x.SubGroup)))
        {
            this.NotifyError($@"{errorMessage} 'UOM'.");
            return;
        }

        var itemGroups = models.GroupBy(x => x.Description).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
        if (itemGroups.Any())
        {
            var builder = new StringBuilder(itemGroups[0]);
            for (var i = 0; i < itemGroups.Count; i++)
            {
                if (i != 0)
                {
                    builder.Append($@", {itemGroups[i]}");
                }
            }
            this.NotifyError($@"Product(s): {builder} repeated multiple times in the list.");
            return;
        }

        SplashScreenManager.ShowForm(typeof(PleaseWait));

        // verify and update the units
        var unitsResponse = await _groupSetup.CreateAndFetchUnitsAsync(ObjGlobal.SysBranchId.GetInt(), models.Select(x => x.UOM).Distinct().ToList(), ObjGlobal.LogInUser);
        if (!unitsResponse.Success)
        {
            SplashScreenManager.CloseForm();
            unitsResponse.ShowErrorDialog();
            return;
        }

        unitsResponse.List.ForEach(unit =>
        {
            models.Where(m => m.UOM.Equals(unit.Value, StringComparison.OrdinalIgnoreCase)).ForEach(fData => fData.UOMId = unit.Id);
        });

        // verify and update the product-groups
        var groupResponse = await _groupSetup.CreateAndFetchProductGroupsAsync(ObjGlobal.SysBranchId.GetInt(), models.Select(x => x.Group).Distinct().ToList(), ObjGlobal.LogInUser);
        if (!groupResponse.Success)
        {
            SplashScreenManager.CloseForm();
            groupResponse.ShowErrorDialog();
            return;
        }

        groupResponse.List.ForEach(group =>
        {
            models.Where(m => m.Group.Equals(group.Value, StringComparison.OrdinalIgnoreCase))
                .ForEach(fData => fData.GroupId = group.Id);
        });

        // verify and update the sub-groups
        var sGroupResponse = await _groupSetup.CreateAndFetchProductSubgroupsAsync(ObjGlobal.SysBranchId.GetInt(), models.DistinctBy(x => x.SubGroup)
                .Select(s => new ProductSubGroupEModel
                {
                    Id = s.SubGroupId,
                    Name = s.SubGroup,
                    GroupId = s.GroupId
                }).ToList(),
            ObjGlobal.LogInUser);

        if (!sGroupResponse.Success)
        {
            SplashScreenManager.CloseForm();
            sGroupResponse.ShowErrorDialog();
            return;
        }

        sGroupResponse.List.ForEach(sGroup =>
        {
            models.Where(m => m.SubGroup.Equals(sGroup.Name, StringComparison.OrdinalIgnoreCase)).ForEach(fData => fData.SubGroupId = sGroup.Id);
        });

        bsImport.ResetBindings(false);
        DGrid.Refresh();

        var updateResponse = await _groupSetup.UpdateProductImportAsync(models, ObjGlobal.SysBranchId, ObjGlobal.LogInUser);
        SplashScreenManager.CloseForm();

        if (!updateResponse.Value)
        {
            // show error dialog
            updateResponse.ShowErrorDialog();
            return;
        }

        // show update success message and close the dialog
        this.NotifySuccess("Data imported successfully");
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private int SaveProductUnit(string tag, string description)
    {
        // if (string.IsNullOrEmpty(description)) return 0;
        // _groupSetup.ObjMaster._ActionTag = tag;
        // _groupSetup.ObjMaster.TxtDescription = description;
        // _groupSetup.ObjMaster.TxtShortName = description;
        // _groupSetup.ObjMaster.ChkActive = true;
        // _groupSetup.ObjMaster.TxtEnterBy = ObjGlobal.LogInUser;
        // _groupSetup.ObjMaster.TxtBranchId = Convert.ToInt32(ObjGlobal.SysBranchId);
        // int.TryParse(ObjGlobal.SysCompanyUnitId.ToString(), out var companyUnitId);
        // _groupSetup.ObjMaster.TxtCompanyUnitId = companyUnitId;
        // return _groupSetup.SaveProductUnit();
        return 0;
    }

    private int SaleProductSubGroup(string tag, string description)
    {
        // if (string.IsNullOrEmpty(description)) return 0;
        // _groupSetup.ObjMaster._ActionTag = tag;
        // _groupSetup.ObjMaster.TxtDescription = description;
        // _groupSetup.ObjMaster.TxtShortName = description;
        // _groupSetup.ObjMaster.ChkActive = true;
        // _groupSetup.ObjMaster.TxtEnterBy = ObjGlobal.LogInUser;
        // _groupSetup.ObjMaster.TxtBranchId = Convert.ToInt32(ObjGlobal.SysBranchId);
        // int.TryParse(ObjGlobal.SysCompanyUnitId.ToString(), out var companyUnitId);
        // _groupSetup.ObjMaster.TxtCompanyUnitId = companyUnitId;
        // return _groupSetup.SaveProductSubGroup();
        return 0;
    }

    private int SaveProductGroup(string tag, string description)
    {
        // if (string.IsNullOrEmpty(description)) return 0;
        // _groupSetup.ObjMaster._ActionTag = tag;
        // _groupSetup.ObjMaster.TxtDescription = description;
        // _groupSetup.ObjMaster.TxtShortName = description;
        // _groupSetup.ObjMaster.ChkActive = true;
        // _groupSetup.ObjMaster.TxtEnterBy = ObjGlobal.LogInUser;
        // _groupSetup.ObjMaster.TxtBranchId = Convert.ToInt32(ObjGlobal.SysBranchId);
        // int.TryParse(ObjGlobal.SysCompanyUnitId.ToString(), out var companyUnitId);
        // _groupSetup.ObjMaster.TxtCompanyUnitId = companyUnitId;
        // return _groupSetup.SaveProductGroup();
        return 0;
    }

    private int SaveProduct(string tag, string descrition)
    {
        // if (string.IsNullOrEmpty(descrition)) return 0;
        // _groupSetup.ObjMaster._ActionTag = tag;
        // _groupSetup.ObjMaster.TxtDescription = descrition;
        // _groupSetup.ObjMaster.TxtShortName = descrition;
        // _groupSetup.ObjMaster.ChkActive = true;
        // _groupSetup.ObjMaster.TxtEnterBy = ObjGlobal.LogInUser;
        // _groupSetup.ObjMaster.TxtBranchId = Convert.ToInt32(ObjGlobal.SysBranchId);
        // int.TryParse(ObjGlobal.SysCompanyUnitId.ToString(), out var companyUnitId);
        // _groupSetup.ObjMaster.TxtCompanyUnitId = companyUnitId;
        // return _groupSetup.SaveProductInfo();
        return 0;
    }

    private void DGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
}