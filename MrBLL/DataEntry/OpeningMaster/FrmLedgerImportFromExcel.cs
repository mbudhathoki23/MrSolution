using DevExpress.XtraSplashScreen;
using MrBLL.DataEntry.Common;
using MrDAL.Control.SplashScreen;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.DataEntry.Design;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.Interface.OpeningMaster;
using MrDAL.DataEntry.OpeningMaster;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Master.Interface.SystemSetup;
using MrDAL.Master.SystemSetup;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace MrBLL.DataEntry.OpeningMaster;

public partial class FrmLedgerImportFromExcel : Form
{
    // LEDGER OPENING IMPORT FORM
    public FrmLedgerImportFromExcel()
    {
        InitializeComponent();
        _setup = new ClsMasterSetup();
        //_entry = new ClsFinanceEntry();
        _voucher = new FinanceDesign();
        RGrid.AutoGenerateColumns = false;
        _voucher.GetJournalVoucherDesign(RGrid, "S");
        _productUpdateRepository = new ProductUpdateRepository();
        _ledgerOpening = new LedgerOpeningRepository();
    }

    private void FrmLedgerImportFromExcel_Load(object sender, EventArgs e)
    {
    }

    private void BtnDownloadSample_Click(object sender, EventArgs e)
    {
        var dt = _setup.GetOpeningLedgerList();
        if (dt.Rows.Count > 0)
        {
            DataTableToExcel(dt, "OpeningLedger");
        }
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (RGrid.Rows.Count == 0)
        {
            CustomMessageBox.Warning("THERE IS NO DATA TO IMPORT..!!");
            return;
        }
        ReturnVoucherNo();
        var result = SaveLedgerOpening();
        if (result != 0)
        {
            CustomMessageBox.Information("DATA IMPORT SUCCESSFULLY..!!");
            RGrid.Rows.Clear();
        }
        else
        {
            CustomMessageBox.Warning("ERROR OCCURS WHILE DATA IMPORT");
        }
    }

    private int SaveLedgerOpening()
    {
        var openingId = _ledgerOpening.GetOpening.Opening_Id.ReturnMaxIntId("COA", "Opening_Id");
        _ledgerOpening.GetOpening.Opening_Id = openingId;
        _ledgerOpening.GetOpening.Voucher_No = _voucherNo.Text;
        _ledgerOpening.GetOpening.Module = "OB";
        var startDate = ObjGlobal.CfStartAdDate.GetDateTime().AddDays(-1);
        _ledgerOpening.GetOpening.OP_Date = startDate;
        _ledgerOpening.GetOpening.OP_Miti = startDate.GetNepaliDate();
        _ledgerOpening.GetOpening.Remarks = "OPENING IMPORT FROM EXCEL..!!";
        _ledgerOpening.GetOpening.GetView = RGrid;
        const int sync = 0;
        _ledgerOpening.GetOpening.SyncRowVersion = sync.ReturnSyncRowNo("COA", openingId.ToString());
        return _ledgerOpening.SaveLedgerOpening("SAVE");
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
    }

    // METHOD FOR THIS FORM
    private void DataTableToExcel(DataTable dataTable, string sheetName)
    {
        //using var folderDlg = new FolderBrowserDialog
        //{
        //    ShowNewFolderButton = true
        //};
        //var result = folderDlg.ShowDialog();
        //if (result is not DialogResult.OK)
        //{
        //    return;
        //}
        //using var wb = new XLWorkbook();
        //wb.Worksheets.Add(dataTable, sheetName);
        //wb.Author = ObjGlobal.Caption;
        //wb.SaveAs($"{folderDlg.SelectedPath}\\{sheetName}.xlsx");
    }

    private void BtnUpload_Click(object sender, EventArgs e)
    {
        using var fileDialog = new OpenFileDialog
        {
            Filter = @"Excel Worksheets|*.xlsx"
        };
        if (fileDialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }
        try
        {
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            var table = _productUpdateRepository.ReadExcelFile(Path.GetFullPath(fileDialog.FileName), "OpeningLedger");
            if (table == null)
            {
                return;
            }

            var result = table.Copy();
            foreach (DataRow row in table.Rows)
            {
                var debit = row["Debit"].GetDecimal();
                debit += row["Credit"].GetDecimal();
                if (debit == 0)
                {
                    result.Rows.Remove(row);
                }
            }
            RGrid.DataSource = result;
            SplashScreenManager.CloseForm();
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            SplashScreenManager.CloseForm(false);
        }
    }

    private void ReturnVoucherNo()
    {
        var dt = _setup.IsExitsCheckDocumentNumbering("COA");
        if (dt?.Rows.Count is 1)
        {
            _docDesc = dt.Rows[0]["DocDesc"].ToString();
            _voucherNo.Text = _voucherNo.GetCurrentVoucherNo("COA", _docDesc);
        }
        else if (dt?.Rows.Count > 1)
        {
            using var wnd = new FrmNumberingScheme("COA");
            if (wnd.ShowDialog() != DialogResult.OK) return;
            if (string.IsNullOrEmpty(wnd.VNo)) return;
            _docDesc = wnd.Description;
            _voucherNo.Text = wnd.VNo;
        }
    }

    // OBJECT FOR THIS FORM
    private IMasterSetup _setup;

    //private IFinanceEntry _entry;
    private readonly IFinanceDesign _voucher;
    private string _docDesc;
    private TextBox _voucherNo = new();
    private readonly IProductUpdateRepository _productUpdateRepository;
    private readonly ILedgerOpeningRepository _ledgerOpening;
}