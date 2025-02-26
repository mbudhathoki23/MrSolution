using DevExpress.Utils.Extensions;
using DevExpress.XtraSplashScreen;
using MoreLinq;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.SplashScreen;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Global.Dialogs;
using MrDAL.ImportExport.Implementations;
using MrDAL.Master.Interface.SystemSetup;
using MrDAL.Master.SystemSetup;
using MrDAL.Models.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
namespace MrBLL.Master.Import;

public partial class FrmSparePartsImport : MrForm
{
    private readonly ExcelDataImportExport _export = new();
    private readonly ISparePartsImportRepository _groupSetup;

    public FrmSparePartsImport()
    {
        InitializeComponent();
        _groupSetup = new SparePartsImportRepository();
    }

    private void FrmProductImport_Load(object sender, EventArgs e)
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
        _export.Export(table, fileName, "Spare Parts Sample");
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
            mrProductDataGridView.DataSource = table.ToList<ImportSparePartsEModel>().Where(x => !string.IsNullOrWhiteSpace(x.Description)).ToList();
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
        if (mrProductDataGridView.Rows.Count == 0)
        {
            // show no items to import
            this.NotifyError("No record to process data import");
            return;
        }
        //var models = bsImport.List.OfType<ImportProductEModel>().ToList();
        List<ImportSparePartsEModel> models = [];

        foreach (DataGridViewRow row in mrProductDataGridView.Rows)
        {
            if (row.DataBoundItem is ImportSparePartsEModel model)
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

        if (models.Any(x => string.IsNullOrWhiteSpace(x.Description)))
        {
            this.NotifyError($@"{errorMessage} 'Description'.");
            return;
        }
        //if (models.Any(x => string.IsNullOrWhiteSpace(x.Group)))
        //{
        //    this.NotifyError($@"{errorMessage} 'Group'.");
        //    return;
        //}
        //if (models.Any(x => string.IsNullOrWhiteSpace(x.SubGroup)))
        //{
        //    this.NotifyError($@"{errorMessage} 'SubGroup'.");
        //    return;
        //}
        //if (models.Any(x => string.IsNullOrWhiteSpace(x.Unit.ToString())))
        //{
        //    this.NotifyError($@"{errorMessage} 'Unit'.");
        //    return;
        //}
        //if (models.Any(x => string.IsNullOrWhiteSpace(x.AltUnit.ToString())))
        //{
        //    this.NotifyError($@"{errorMessage} 'AltUnit'.");
        //    return;
        //}
        //if (models.Any(x => string.IsNullOrWhiteSpace(x.QtyConv.ToString())))
        //{
        //    this.NotifyError($@"{errorMessage} 'QtyConv'.");
        //    return;
        //}
        //if (models.Any(x => string.IsNullOrWhiteSpace(x.AltConv.ToString())))
        //{
        //    this.NotifyError($@"{errorMessage} 'AltConv'.");
        //    return;
        //}
        //if (models.Any(x => string.IsNullOrWhiteSpace(x.BuyRate.ToString())))
        //{
        //    this.NotifyError($@"{errorMessage} 'BuyRate'.");
        //    return;
        //}

        //if (models.Any(x => string.IsNullOrWhiteSpace(x.SalesRate.ToString())))
        //{
        //    this.NotifyError($@"{errorMessage} 'SalesRate'.");
        //    return;
        //}

        //if (models.Any(x => string.IsNullOrWhiteSpace(x.TaxRate.ToString())))
        //{
        //    this.NotifyError($@"{errorMessage} 'TaxRate'.");
        //    return;
        //}
        //if (models.Any(x => string.IsNullOrWhiteSpace(x.MinStock.ToString())))
        //{
        //    this.NotifyError($@"{errorMessage} 'MinStock'.");
        //    return;
        //}
        //if (models.Any(x => string.IsNullOrWhiteSpace(x.MaxStock.ToString())))
        //{
        //    this.NotifyError($@"{errorMessage} 'MaxStock'.");
        //    return;
        //}
        //if (models.Any(x => string.IsNullOrWhiteSpace(x.MRPRate.ToString())))
        //{
        //    this.NotifyError($@"{errorMessage} 'MRPRate'.");
        //    return;
        //}
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


        //// verify and update the product-groups
        var groupResponse = await _groupSetup.CreateAndFetchProductGroupsAsync(ObjGlobal.SysBranchId.GetInt(), models.Select(x => x.Group).Distinct().ToList(), ObjGlobal.LogInUser);
        if (!groupResponse.Success)
        {
            SplashScreenManager.CloseForm();
            groupResponse.ShowErrorDialog();
            return;
        }

        EnumerableExtensions.ForEach(groupResponse.List, group =>
        {
            EnumerableExtensions.ForEach(models.Where(m => m.Group.Equals(group.Value, StringComparison.OrdinalIgnoreCase)), fData => fData.GroupId = group.Id);
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

        EnumerableExtensions.ForEach(sGroupResponse.List, sGroup =>
        {
            EnumerableExtensions.ForEach(models.Where(m => m.SubGroup.Equals(sGroup.Name, StringComparison.OrdinalIgnoreCase)), fData => fData.SubGroupId = sGroup.Id);
        });


        SplashScreenManager.ShowForm(typeof(PleaseWait));
        var updateResponse = await _groupSetup.UpdateProductImportAsync(models, ObjGlobal.SysBranchId, ObjGlobal.LogInUser);
        SplashScreenManager.CloseForm();
        if (!updateResponse.Value)
        {
            // show error dialog
            updateResponse.ShowErrorDialog();
            return;
        }
        mrProductDataGridView.DataSource = null;
        // show update success message and close the dialog
        this.NotifySuccess("Data imported successfully");
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }
}