using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Windows.Forms;
using MrBLL.Utility.Common.Class;
using MrBLL.Utility.CrystalReports;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Global.Common.RawQuery;
using MrDAL.Global.Control;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PrintControl.PrintClass.DirectPrint;
using PrintControl.PrintDesign.Sales_Invoice;

namespace MrBLL.DataEntry.Common;

public partial class FrmDocumentPrint : MrForm
{
    // DOCUMENT PRINT

    #region --------------- FrmDocumentPrint ---------------

    public FrmDocumentPrint(bool zoom = false)
    {
        InitializeComponent();
        _zoom = zoom;
    }

    public FrmDocumentPrint(string frmName, string module, string fromDocNo, string toDocNo, bool isZoom = false) : this(frmName, module, string.Empty, fromDocNo, toDocNo, string.Empty, string.Empty, ObjGlobal.SysDefaultPrinter, string.Empty, string.Empty, isZoom)
    {
    }

    public FrmDocumentPrint(string frmName, string module, string selectQuery, string fromDocNo, string toDocNo, string fromDate = null, string toDate = null, string printerName = null, string designName = null, string invoiceType = null, bool isZoom = false)
    {
        InitializeComponent();
        _module = module;
        _printing = new ClsDocumentPrinting
        {
            FrmName = frmName,
            SelectQuery = selectQuery,
            Module = module,
            FromDocNo = fromDocNo,
            ToDocNo = toDocNo,
            FromDate = fromDate,
            ToDate = toDate,
            PrinterName = printerName,
            DocDesignName = designName
        };
        if (invoiceType != null)
        {
            _printing.InvoiceType = invoiceType;
        }
        _zoom = isZoom;
    }

    private void FrmDocumentPrint_Load(object sender, EventArgs e)
    {
        ObjGlobal.BindPrinter(CmbPrinter);
        Text = _printing.FrmName;
        var table = _printing.GetPrintVoucherName(_printing.Module);
        if (table != null && table.Rows.Count > 0)
        {
            CmbDesign.Items.Clear();
            CmbDesign.DataSource = table;
            CmbDesign.DisplayMember = "Design";
            CmbDesign.ValueMember = "DDP_Id";
            TxtNoOfCopies.Text = table.Rows[0]["NoOfPrint"].ToString();
            CmbDesign.SelectedIndex = 0;
        }
        else
        {
            //ObjGlobal.BindPaperSize(CmbDesign, _printing.Module, _printing.InvoiceType);
            CmbDesign.Items.Clear();
            CmbDesign.DataSource = null;
            CustomMessageBox.Information("PRINT DESIGN SETTING IS REQUIRED FOR PRINT DOCUMENT..!!");
            //return;
        }
        if (CmbDesign.Items.Count > 0)
        {
            if (_printing.FrmName.Equals("DLL") && _module is "SB" or "ATI" or "POS" or "RESTRO" or "SR")
            {
                CmbDesign.SelectedIndex = _module.Equals("SB") ? CmbDesign.FindString(ObjGlobal.SysDefaultInvoiceDesign) : 0;
                if (CmbDesign.SelectedIndex == -1)
                {
                    CmbDesign.SelectedIndex = 0;
                }
            }
            else if (CmbDesign.SelectedIndex != -1 && CmbDesign.Items.Count > 0)
            {
                CmbDesign.SelectedIndex = 0;
            }
        }
        var postdoc = new PrintDocument();
        var strDefaultPrinter = postdoc.PrinterSettings.PrinterName;
        CmbPrinter.Text = strDefaultPrinter;
        BindDateType();
        ObjGlobal.PageLoadDateType(MskFrom);
        ObjGlobal.PageLoadDateType(MskToDate);
        TxtFrom.Text = _printing.FromDocNo;
        TxtTO.Text = _printing.ToDocNo;
    }

    private void FrmDocumentPrint_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            if (_zoom)
            {
                BtnPrint.Focus();
            }
            else
            {
                SendKeys.Send("{TAB}");
            }
        }
        else if (e.KeyChar == (char)Keys.Escape)
        {
            if (CustomMessageBox.ExitActiveForm() is DialogResult.Yes)
            {
                Close();
            }
        }
    }

    private void FrmDocumentPrint_Shown(object sender, EventArgs e)
    {
        CmbReportType.Focus();
    }

    #endregion --------------- FrmDocumentPrint ---------------

    // EVENTS FOR THIS FORM

    #region --------------- Event ---------------

    private void CmbReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbl_From.Text = CmbReportType.SelectedValue.ToString() == "Date" ? "From Date" : "From Number";
        lbl_To.Text = CmbReportType.SelectedValue.ToString() == "Date" ? "To Date" : "To Number";
        MskFrom.Visible = CmbReportType.SelectedValue.ToString() == "Date";
        MskToDate.Visible = CmbReportType.SelectedValue.ToString() == "Date";
        TxtFrom.Visible = CmbReportType.SelectedValue.ToString() != "Date";
        TxtTO.Visible = CmbReportType.SelectedValue.ToString() != "Date";
        BtnVoucherNoFrom.Visible = CmbReportType.SelectedValue.ToString() != "Date";
        BtnVoucherNoTo.Visible = CmbReportType.SelectedValue.ToString() != "Date";
    }

    private void MskFromDate_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskFrom, 'E');
    }

    private void MskFromDate_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskFrom, 'L');
    }

    private void MskToDate_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskToDate, 'E');
    }

    private void MskToDate_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskToDate, 'L');
    }

    private void TxtFrom_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtFrom, 'E');
    }

    private void TxtFrom_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1 or Keys.Tab)
        {
            BtnFrom_Click(sender, e);
        }
    }

    private void TxtFrom_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtFrom, 'L');
        if (TxtFrom.Text.Trim() != string.Empty) TxtTO.Text = TxtFrom.Text;
    }

    private void BtnFrom_Click(object sender, EventArgs e)
    {
        var voucherNo = GetTransactionList.GetTransactionVoucherNo("SAVE", DateTime.Now.GetDateString(), "MED", _module, "Print");
        if (voucherNo.IsValueExits())
        {
            TxtFrom.Text = voucherNo;
        }
        TxtFrom.Focus();
    }

    private void TxtTo_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1 or Keys.Tab) BtnTo_Click(sender, e);
    }

    private void TxtTo_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtTO, 'E');
    }

    private void TxtTo_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtTO, 'L');
    }

    private void TxtTo_Validated(object sender, EventArgs e)
    {
        if (TxtTO.Text.Trim() == string.Empty)
        {
            return;
        }
    }

    private void BtnTo_Click(object sender, EventArgs e)
    {
        TxtTO.Text = GetTransactionList.GetTransactionVoucherNo("SAVE", DateTime.Now.GetDateString(), "MED", _module, "Print");
        TxtTO.Focus();
    }

    private void TxtNoOfCopy_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void BtnPrint_Click(object sender, EventArgs e)
    {
        PrintFunctionAsync();
    }

    private void PrintFunctionAsync()
    {
        #region --------------- Validation on Print Page ---------------

        if (CmbDesign != null)
        {
            if (CmbDesign.Text != "")
            {
                var design = CmbDesign.Text;
                var dtDesign = GetConnection.SelectQueryFromMaster(
                    $"Select * from MASTER.AMS.PrintDocument_Designer where Designerpaper_Name='{design}'");
                if (dtDesign.Rows.Count == 0)
                {
                    _printing.FrmName = "DLL";
                }
            }
            else
            {
                MessageBox.Show(@"Please select the paper size ..!!");
                CmbDesign.Focus();
                return;
            }
        }

        if (TxtFrom.Text.Trim() == string.Empty && TxtTO.Text.Trim() == string.Empty || MskFrom.Text.Trim() == string.Empty && MskToDate.Text.Trim() == string.Empty)
        {
            switch (MskFrom.Visible)
            {
                case true:
                {
                    MessageBox.Show(@"From Date and To Date Can't Left Blank..!!", ObjGlobal.Caption);
                    MskFrom.Focus();
                    return;
                }
                default:
                {
                    MessageBox.Show(@"From No and To No Can't Left Blank..!!", ObjGlobal.Caption);
                    TxtFrom.Focus();
                    return;
                }
            }
        }

        #endregion --------------- Validation on Print Page ---------------

        if (_printing.FrmName == "DLL")
        {
            try
            {
                #region --------------- Sales Bill Print  ---------------

                string[] entrySales = ["SEB", "POS", "SBF", "SB", "SR", "ATI", "SC", "SOC", "SOF", "SO", "SQ"];
                if (entrySales.Contains(_printing.Module))
                {
                    var form = new PrintSalesBill
                    {
                        BillNo = TxtTO.Text,
                        Printer = CmbPrinter.Text,
                        Printdesign = CmbDesign?.Text,
                        PrintedBy = ObjGlobal.LogInUser,
                        NoofPrint = TxtNoOfCopies.Text.GetShort(),
                        PDiscountId = ObjGlobal.SalesDiscountTermId.ToString(),
                        BDiscountId = ObjGlobal.SalesSpecialDiscountTermId.ToString(),
                        ServiceChargeId = ObjGlobal.SalesServiceChargeTermId.ToString(),
                        SalesVatTermId = ObjGlobal.SalesVatTermId.ToString()
                    };
                    form.PrintDocumentDesign();
                }

                #endregion --------------- Sales Bill Print  ---------------

                #region --------------- Print Command ---------------

                MessageBox.Show(@"DOCUMENT PRINT SUCCESSFULLY..!!", ObjGlobal.Caption);
                //Close();

                #endregion --------------- Print Command ---------------
            }
            catch (Exception ex)
            {
                ex.ToNonQueryErrorResult(ex.StackTrace);
                // ignored
            }
        }
        else
        {
            _printing.NoOfCopy = TxtNoOfCopies.GetInt();
            _printing.PrinterName = CmbPrinter.Text;
            _printing.FromDocNo = TxtFrom.Text.Trim();
            _printing.ToDocNo = TxtTO.Text.Trim();
            if (CmbDesign != null)
            {
                _printing.DesignLocation = CmbDesign.SelectedValue.ToString();
            }

            //var isPrintDocument = CrystalDesignPrint();
            var isPrintDocument = Task.Run(() =>
            {
                _ = CrystalDesignPrintAsync();
            });
            if (!_zoom)
            {
                MessageBox.Show(@"PRINT DOCUMENT SUCCESSFULLY..!!", ObjGlobal.Caption, MessageBoxButtons.OK);
            }
        }
    }

    private void BtnPreview_Click(object sender, EventArgs e)
    {
        try
        {
            //if (Obj.FrmName != "Crystal") return;
            if (TxtFrom.IsBlankOrEmpty() && TxtTO.IsBlankOrEmpty() || !MskFrom.MaskCompleted && !MskToDate.MaskCompleted)
            {
                if (MskFrom.Visible)
                {
                    MessageBox.Show(@"FROM DATE AND TO DATE CAN'T LEFT BLANK..!!", ObjGlobal.Caption);
                    MskFrom.Focus();
                    return;
                }
                MessageBox.Show(@"FROM NO AND TO NO CAN'T LEFT BLANK..!!", ObjGlobal.Caption);
                TxtFrom.Focus();
                return;
            }
            var design = CmbDesign.Text;
            var printer = CmbPrinter.Text;
            var location = CmbDesign.SelectedValue.ToString();
            var rType = _printing.FrmName;

            var voucherNo = new[] { TxtFrom.Text, TxtTO.Text };
            var result = new FrmPrintPreViewer(voucherNo, TxtNoOfCopies.GetInt(), printer, _module, location, rType);
            result.ShowDialog();
        }
        catch (Exception ex)
        {
            ex.DialogResult();
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void BindDateType()
    {
        var list = new List<ValueModel<string, string>>
        {
            new("Date", "Date"),
            new("Number", "Number")
        };

        CmbReportType.DataSource = list;
        CmbReportType.DisplayMember = "Item1";
        CmbReportType.ValueMember = "Item2";
        CmbReportType.SelectedIndex = 1;
    }

    // PRINT CRYSTAL

    #region ------------- Print Event -------------

    private async Task<bool> CrystalDesignPrintAsync()
    {
        var isPrint = false;
        var crystalReportViewer1 = new CrystalReportViewer();
        var objReportDocument = new ReportDocument();

        #region -------------- Voucher --------------

        var dataSet = _printing.GetVoucherDetailsForPrinting(_printing.FromDocNo, _printing.ToDocNo, _printing.Module);
        objReportDocument.Load(_printing.DesignLocation);
        //objReportDocument = new SalesInvoiceA4();
        objReportDocument.SetDataSource(dataSet);

        var sa = GetConnection.ServerUserId;
        var psw = GetConnection.ServerUserPsw;
        var server = GetConnection.ServerDesc;
        var database = GetConnection.LoginInitialCatalog;

        objReportDocument.SetDatabaseLogon(sa, psw, server, database);

        var val = _printing.NoOfCopy.GetInt();
        for (var val1 = 1; val1 <= val; val1++)
        {
            try
            {
                objReportDocument.SetParameterValue("copyNo", val1);
            }
            catch
            {
                // ignored
            }
            crystalReportViewer1.ReportSource = objReportDocument;
            crystalReportViewer1.Refresh();

            objReportDocument.PrintOptions.PrinterName = _printing.PrinterName;
            objReportDocument.PrintToPrinter(1, false, 0, 0);
            isPrint = true;
        }
        if (_printing.Module is "SB")
        {
            PrintSalesBill.PrintUpdate(_printing.FromDocNo);
        }
        return await Task.FromResult(isPrint);

        #endregion -------------- Voucher --------------
    }

    public bool CrystalDesignPrint()
    {
        var isPrint = false;
        var crystalReportViewer1 = new CrystalReportViewer();
        var objReportDocument = new ReportDocument();

        #region -------------- Voucher --------------

        var dataSet = _printing.GetVoucherDetailsForPrinting(_printing.FromDocNo, _printing.ToDocNo, _printing.Module);
        objReportDocument.Load(_printing.DesignLocation);
        objReportDocument.SetDataSource(dataSet);

        var sa = GetConnection.ServerUserId;
        var psw = GetConnection.ServerUserPsw;
        var server = GetConnection.ServerDesc;
        var database = GetConnection.LoginInitialCatalog;

        objReportDocument.SetDatabaseLogon(sa, psw, server, database);

        var val = _printing.NoOfCopy.GetInt();
        for (var val1 = 1; val1 <= val; val1++)
        {
            try
            {
                objReportDocument.SetParameterValue("copyNo", val1);
            }
            catch
            {
                // ignored
            }
            crystalReportViewer1.ReportSource = objReportDocument;
            crystalReportViewer1.Refresh();

            objReportDocument.PrintOptions.PrinterName = _printing.PrinterName;
            objReportDocument.PrintToPrinter(1, false, 0, 0);
            isPrint = true;
        }
        if (_printing.Module is "SB")
        {
            PrintSalesBill.PrintUpdate(_printing.FromDocNo);
        }
        return isPrint;

        #endregion -------------- Voucher --------------
    }

    public static bool CrystalDesignPrint(string fromDocNo, string module, string designLocation)
    {
        var postdoc = new PrintDocument();
        var strDefaultPrinter = postdoc.PrinterSettings.PrinterName;
        var isPrint = false;
        var crystalReportViewer1 = new CrystalReportViewer();
        var objReportDocument = new ReportDocument();

        // VOUCHER DETAILS

        #region -------------- Voucher --------------

        using (var dataSet = DirectPrinting.GetVoucherDetailsForPrinting(fromDocNo, fromDocNo, module))
        {
            objReportDocument.Load(designLocation);
            objReportDocument.SetDataSource(dataSet);
        }
        objReportDocument.SetDatabaseLogon(GetConnection.ServerUserId, GetConnection.ServerUserPsw, GetConnection.ServerDesc, GetConnection.LoginInitialCatalog);
        const int val = 1;
        for (var val1 = 1; val1 <= val; val1++)
        {
            try
            {
                objReportDocument.SetParameterValue("copyNo", val1);
            }
            catch
            {
                // ignored
            }

            crystalReportViewer1.ReportSource = objReportDocument;
            crystalReportViewer1.Refresh();
            objReportDocument.PrintOptions.PrinterName = strDefaultPrinter;
            objReportDocument.PrintToPrinter(1, false, 0, 0);
            isPrint = true;
            if (module.Equals("SB"))
            {
                PrintSalesBill.PrintUpdate(fromDocNo);
            }
        }

        return isPrint;

        #endregion -------------- Voucher --------------
    }

    #endregion ------------- Print Event -------------

    #endregion --------------- Event ---------------

    //OBJECT FOR THIS FORM

    #region --------------- OBJECT ---------------

    public int PageWidth;
    private readonly bool _zoom;
    private readonly string _module;
    private readonly ClsDocumentPrinting _printing;
    private static readonly ClsDocumentPrinting DirectPrinting = new();

    #endregion --------------- OBJECT ---------------
}