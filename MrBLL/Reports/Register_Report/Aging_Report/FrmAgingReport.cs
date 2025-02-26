using MrBLL.Utility.Common;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Utility.PickList;
using System;
using System.Windows.Forms;

namespace MrBLL.Reports.Register_Report.Aging_Report;

public partial class FrmAgingReport : MrForm
{
    // AGING REPORT

    #region ---------- AGING REPORT ----------

    public FrmAgingReport(bool isCustomer = true)
    {
        InitializeComponent();
        _isCustomer = isCustomer;
        ClearControl();
        CmbReportMode_SelectedIndexChanged(this, null);
        CmbAgingSlab_SelectedIndexChanged(this, null);
    }

    private void FrmAgingReport_Load(object sender, EventArgs e)
    {
        CmbReportMode.Focus();
    }

    private void FrmAgingReport_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
        else if (e.KeyChar is (char)Keys.Escape)
        {
            BtnCancel.PerformClick();
        }
    }

    private void CmbReportMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        MskAsOnDate.Enabled = CmbReportMode.SelectedIndex is not 2;
    }

    private void CmbAgingSlab_SelectedIndexChanged(object sender, EventArgs e)
    {
        TxtAgeingSlabDays.Enabled = CmbAgingSlab.SelectedIndex is not 0;
    }

    private void BtnShow_Click(object sender, EventArgs e)
    {
        _rptType = "NORMAL";

        if (!MskAsOnDate.MaskCompleted && MskAsOnDate.Enabled)
        {
            MskAsOnDate.WarningMessage("AGING AS ON DATE IS REQUIRED..!!");
            return;
        }

        _reportMode = CmbAgingOn.Text;
        _fromAdDate = ObjGlobal.CfStartAdDate.GetDateString();
        _fromBsDate = ObjGlobal.CfStartBsDate;

        _toAdDate = ObjGlobal.SysDateType == "M" ? ObjGlobal.ReturnEnglishDate(MskAsOnDate.Text) : MskAsOnDate.Text;
        _toBsDate = ObjGlobal.SysDateType == "M" ? MskAsOnDate.Text : ObjGlobal.ReturnNepaliDate(MskAsOnDate.Text);

        var result = !ObjGlobal.SysIsDateWise && ChkDate.Checked == false || ObjGlobal.SysIsDateWise && ChkDate.Checked;

        _rptDate = result switch
        {
            true => $"FROM DATE {_fromBsDate} TO {_toBsDate}",
            _ => $"FROM DATE {_fromAdDate.GetDateString()} TO {_toAdDate.GetDateString()}"
        };

        if (!ChkSelectAll.Checked)
        {
            using var frm2 = new FrmTagList
            {
                ReportDesc = @"GENERAL_LEDGER",
                Category = _isCustomer ? "CUSTOMER" : "VENDOR",
                Module = _isCustomer ? "SB" : "PB",
                BranchId = ObjGlobal.SysBranchId.ToString(),
                CompanyUnitId = ObjGlobal.SysCompanyUnitId.ToString(),
                FiscalYearId = ObjGlobal.SysFiscalYearId.ToString()
            };
            frm2.ShowDialog();
            _ledgerId = ClsTagList.PlValue1;
            if (string.IsNullOrEmpty(_ledgerId))
            {
                MessageBox.Show(@"LEDGER NOT SELECTED..!!", ObjGlobal.Caption);
                return;
            }
        }
        var reports = new DisplayRegisterReports
        {
            RptName = @"AGING",
            RptType = _rptType,
            RptMode = _reportMode,
            FromAdDate = _fromAdDate,
            ToAdDate = _toAdDate,
            RptDate = _rptDate,
            IsDate = ChkDate.Checked,
            IsCustomer = _isCustomer,
            LedgerId = _ledgerId,
            AgingDays = TxtAgeingSlabDays.GetInt(),
            ColumnsNo = CmbColumnNumber.Text.GetInt(),
            FilterValue = string.Empty
        };
        reports.Show();
        reports.BringToFront();
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() is DialogResult.Yes)
        {
            Close();
        }
    }

    #endregion ---------- AGING REPORT ----------

    // METHOD FOR THIS FORM

    #region ---------- METHOD ----------

    private void ClearControl()
    {
        Text = _isCustomer switch
        {
            false => "VENDOR WISE AGING REPORT",
            _ => "CUSTOMER WISE AGING REPORT"
        };
        MskAsOnDate.Text = ObjGlobal.SysIsDateWise ? DateTime.Now.GetDateString() : DateTime.Now.GetNepaliDate();
        ChkDate.Text = ObjGlobal.SysIsDateWise ? "Miti" : "Date";
        CmbReportMode.SelectedIndex = 0;
        CmbAgingOn.SelectedIndex = 1;
        CmbAgingSlab.SelectedIndex = 0;
        CmbColumnNumber.SelectedIndex = 0;
    }

    #endregion ---------- METHOD ----------

    // OBJECT

    #region ---------- OBJECT ----------

    private readonly bool _isCustomer;
    private string _fromAdDate;
    private string _fromBsDate;
    private string _reportMode = string.Empty;
    private string _rptDate;
    private string _rptType;
    private string _toAdDate;
    private string _toBsDate;
    private string _ledgerId;

    #endregion ---------- OBJECT ----------
}