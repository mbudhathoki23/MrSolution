using CrystalDecisions.CrystalReports.Engine;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Common.RawQuery;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using PrintControl.PrintDesign.Sales_Invoice;

namespace MrBLL.Utility.CrystalReports;

public partial class FrmPrintPreViewer : Form
{
    // PRINT PREVIEW FUNCTION

    #region --------------- PRINT PREVIEW FUNCTION ---------------

    public FrmPrintPreViewer(IReadOnlyList<string> voucherNo, int noOfPrintCopy, string printerName, string module, string path, string rptType)
    {
        InitializeComponent();
        PrinterName = printerName;
        VoucherNo = voucherNo[0];
        FromDocNo = voucherNo[0];
        ToDocNo = voucherNo[1];
        Module = module;
        Filepath = path;
        RptType = rptType;
        PrintNo = noOfPrintCopy.GetInt() is 0 ? 1 : noOfPrintCopy;
        crystalReportViewer1.ShowPrintButton = !ObjGlobal.IsIrdRegister || !_tagModule.Contains(module);
        crystalReportViewer1.ShowExportButton = !ObjGlobal.IsIrdRegister || !_tagModule.Contains(module);
    }

    public FrmPrintPreViewer(string query = null, string fromDocNo = null, string toDocNo = null, int noOfPrintCopy = 1, string designName = null, string printerName = null, string module = null, string rType = null, bool print = true, string path = null)
    {
        InitializeComponent();
        Filepath = path;
        IsPrint = print;
        RptType = rType;
        Module = module;
        PrinterName = printerName;
        VoucherNo = fromDocNo;
        FromDocNo = fromDocNo;
        ToDocNo = toDocNo;
        PrintNo = noOfPrintCopy;
        crystalReportViewer1.ShowPrintButton = !ObjGlobal.IsIrdRegister || !_tagModule.Contains(module);
        crystalReportViewer1.ShowExportButton = !ObjGlobal.IsIrdRegister || !_tagModule.Contains(module);
    }

    private void FrmCrystalReportViewer_Load(object sender, EventArgs e)
    {
        if (RptType.IsValueExits())
        {
            _dataSet = _printing.GetVoucherDetailsForPrinting(FromDocNo, ToDocNo, Module);
            if (Filepath == null || Filepath.IsBlankOrEmpty())
            {
                return;
            }
            _document.Load(Filepath);
            //_document = new SalesInvoiceA4();
            _document.SetDatabaseLogon(ObjGlobal.ServerUser, ObjGlobal.ServerPassword, ObjGlobal.DataSource, ObjGlobal.InitialCatalog);
            _document.SetDataSource(_dataSet);
            for (var i = 1; i <= PrintNo; i++)
            {
                if (IsPrint == false)
                {
                    crystalReportViewer1.ReportSource = _document;
                    try
                    {
                        _document.SetParameterValue("copyNo", PrintNo);
                    }
                    catch (Exception ex)
                    {
                        var errMsg = ex.Message;
                    }
                    crystalReportViewer1.Refresh();
                }
                else
                {
                    _document.PrintOptions.PrinterName = PrinterName;
                    _document.PrintToPrinter(PrintNo, false, 0, 0);
                    Close();
                }
            }
        }
        else
        {
            MessageBox.Show(@"Design Not Found", ObjGlobal.Caption);
        }
    }

    private void FrmCrystalReportViewer_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
        if (e.KeyCode == Keys.Escape)
        {
            var dialogResult = MessageBox.Show(@"ARE YOU SURE WANT TO CLOSE FORM..!!", ObjGlobal.Caption, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Close();
            }
        }
    }

    #endregion --------------- PRINT PREVIEW FUNCTION ---------------

    // OBJECT FOR THIS FORM

    #region --------------- OBJECT FOR THIS FORM ---------------

    private int PrintNo { get; }
    private string FromDocNo { get; }
    private string ToDocNo { get; }
    private string Module { get; }
    private string Filepath { get; }
    private bool IsPrint { get; }
    private string PrinterName { get; }
    private string RptType { get; }
    private string VoucherNo { get; }
    private readonly string[] _tagModule = ["SB", "SBF", "POS", "ATI"];

    private DataSet _dataSet = new();
    private ReportDocument _document = new();
    private readonly ClsDocumentPrinting _printing = new();

    #endregion --------------- OBJECT FOR THIS FORM ---------------
}