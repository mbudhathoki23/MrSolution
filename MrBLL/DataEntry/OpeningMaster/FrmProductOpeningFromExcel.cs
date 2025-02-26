using DevExpress.Utils.Extensions;
using DevExpress.XtraSplashScreen;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.SplashScreen;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.DataEntry.Interface.OpeningMaster;
using MrDAL.DataEntry.OpeningMaster;
using MrDAL.Global.Common;
using MrDAL.Global.Dialogs;
using MrDAL.ImportExport.Implementations;
using MrDAL.Models.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MrBLL.DataEntry.OpeningMaster;

public partial class FrmProductOpeningFromExcel : MrForm
{
    private readonly ExcelDataImportExport _export = new();
    private readonly IProductOpeningRepository _groupSetup;
    public FrmProductOpeningFromExcel()
    {
        InitializeComponent();
        _groupSetup = new ProductOpeningRepository();
    }

    private void ProductOpeningFromExcel_Load(object sender, EventArgs e)
    {
        mrProductDataGridView.Rows.Clear();
    }

    private void BtnDownloadFormat_Click(object sender, EventArgs e)
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
        foreach (DataGridViewColumn column in mrProductDataGridView.Columns)
        {
            table.Columns.Add(column.Name, column.CellType);
        }
        var fileName = folderDlg.SelectedPath;
        _export.Export(table, fileName, "Product Opening Sample");
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
            mrProductDataGridView.DataSource = null;
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            var table = _groupSetup.ReadExcelFile(Path.GetFullPath(fileDialog.FileName), "Product");
            if (table == null) return;
            mrProductDataGridView.AutoGenerateColumns = false;
            mrProductDataGridView.DataSource = table.ToList<ImportProductOpeningEModel>().Where(x => !string.IsNullOrWhiteSpace(x.Description)).ToList();
            SplashScreenManager.CloseForm();
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            SplashScreenManager.CloseForm(false);
        }
    }
    private async void btnSave_Click(object sender, EventArgs e)
    {
        SplashScreenManager.ShowForm(typeof(PleaseWait));
        if (mrProductDataGridView.Rows.Count == 0)
        {
            // show no items to import
            this.NotifyError("No record to process data import");
            return;
        }
        //var models = bsImport.List.OfType<ImportProductEModel>().ToList();
        List<ImportProductOpeningEModel> models = [];
        foreach (DataGridViewRow row in mrProductDataGridView.Rows)
        {
            if (row.DataBoundItem is ImportProductOpeningEModel model)
            {
                models.Add(model);
            }
        }
        const string errorMessage = "One more more rows doesn't have value for required field:";
        if (models.Any(x => string.IsNullOrWhiteSpace(x.ShortName)))
        {
            this.NotifyError($@"{errorMessage} 'ShortName'.");
            return;
        }
        if (models.Any(x => string.IsNullOrWhiteSpace(x.Quantity.ToString())))
        {
            this.NotifyError($@"{errorMessage} 'Quantity'.");
            return;
        }
        if (models.Any(x => string.IsNullOrWhiteSpace(x.Rate.ToString())))
        {
            this.NotifyError($@"{errorMessage} 'Rate'.");
            return;
        }
        if (models.Any(x => string.IsNullOrWhiteSpace(x.Amount.ToString())))
        {
            this.NotifyError($@"{errorMessage} 'Amount'.");
            return;
        }
        //if (models.Any(x => string.IsNullOrWhiteSpace(x.BranchId.ToString())))
        //{
        //    this.NotifyError($@"{errorMessage} 'BranchId'.");
        //    return;
        //}
        //if (models.Any(x => string.IsNullOrWhiteSpace(x.CompanyId.ToString())))
        //{
        //    this.NotifyError($@"{errorMessage} 'CompanyId'.");
        //    return;
        //}

        //var itemGroups = models.GroupBy(x => x.Description).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
        //if (itemGroups.Any())
        //{
        //    var builder = new StringBuilder(itemGroups[0]);
        //    for (var i = 0; i < itemGroups.Count; i++)
        //    {
        //        if (i != 0)
        //        {
        //            builder.Append($@", {itemGroups[i]}");
        //        }
        //    }

        //    this.NotifyError($@"Product(s): {builder} repeated multiple times in the list.");
        //    return;
        //}

        // verify and update the units
        var unitsResponse = await _groupSetup.CreateAndFetchUnitsAsync(ObjGlobal.SysBranchId.GetInt(), models.Select(x => x.Unit).Distinct().ToList(), ObjGlobal.LogInUser);
        if (!unitsResponse.Success)
        {
            SplashScreenManager.CloseForm();
            unitsResponse.ShowErrorDialog();
            return;
        }
        EnumerableExtensions.ForEach(unitsResponse.List, unit =>
        {
            EnumerableExtensions.ForEach(models.Where(m => m.Unit.Equals(unit.Value, StringComparison.OrdinalIgnoreCase)), fData => fData.UId = unit.Id);
        });
        //// verify and update the product name
        var productResponse = await _groupSetup.CreateAndFetchProductAsync(ObjGlobal.SysBranchId.GetInt(), models.Select(x => x.ShortName).Distinct().ToList(), ObjGlobal.LogInUser);
        if (!productResponse.Success)
        {
            SplashScreenManager.CloseForm();
            productResponse.ShowErrorDialog();
            return;
        }
        EnumerableExtensions.ForEach(productResponse.List, productName =>
        {
            EnumerableExtensions.ForEach(models.Where(m => m.ShortName.Equals(productName.LongValue, StringComparison.OrdinalIgnoreCase)), fData => fData.ProductId = Convert.ToInt32(productName.LongId));
        });
        //// verify and update the godown name
        var goDownResponse = await _groupSetup.CreateAndFetchGoDownAsync(ObjGlobal.SysBranchId.GetInt(), models.Select(x => x.ShortName).Distinct().ToList(), ObjGlobal.LogInUser);
        if (!goDownResponse.Success)
        {
            SplashScreenManager.CloseForm();
            goDownResponse.ShowErrorDialog();
            return;
        }
        EnumerableExtensions.ForEach(goDownResponse.List, GName =>
        {
            EnumerableExtensions.ForEach(models.Where(m => m.ShortName.Equals(GName.Value, StringComparison.OrdinalIgnoreCase)), fData => fData.ProductId = GName.Id);
        });

        var updateResponse = await _groupSetup.UpdateProductOpeningImportAsync(models, ObjGlobal.SysBranchId, ObjGlobal.LogInUser);
        SplashScreenManager.CloseForm();
        if (!updateResponse.Value)
        {
            // show error dialog
            updateResponse.ShowErrorDialog();
            return;
        }
        mrProductDataGridView.DataSource = null;
        // show update success message and close the dialog
        this.NotifySuccess("DATA IMPORTED SUCCESSFULLY");
    }
    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }
}