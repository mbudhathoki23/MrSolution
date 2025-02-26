using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using System;
using System.Windows.Forms;

namespace MrBLL.Reports.Finance_Report.FinalReport;

public partial class FrmProfitNLoss : MrForm
{
    // PROFIT & LOSS ACCOUNT

    #region --------------- Profit & Loss ---------------

    public FrmProfitNLoss(string isNormal)
    {
        InitializeComponent();
        _isNormal = isNormal;
        _branchId = ObjGlobal.SysBranchId.ToString();
        ObjGlobal.BindDateType(CmbDateType, _isNormal.Equals("PL") ? 9 : 8);
    }

    private void FrmProfitNLoss_Load(object sender, EventArgs e)
    {
        Text = _isNormal switch
        {
            "OB" => "PROFIT & LOSS REPORT [OPENING]",
            "PPL" => "PROFIT & LOSS REPORT [PERIODIC]",
            _ => "PROFIT & LOSS REPORT [NORMAL]"
        };
        MethodForControl();
        rChkNormal_CheckedChanged(sender, e);
        rChkLedger_CheckedChanged(sender, e);
        ChkClosingStock_CheckedChanged(sender, e);
    }

    private void FrmProfitNLoss_KeyPress(object sender, KeyPressEventArgs e)
    {
        switch (e.KeyChar)
        {
            case (char)Keys.Escape:
            {
                BtnCancel.PerformClick();
                break;
            }
            case (char)Keys.Enter:
            {
                SendKeys.Send("{TAB}");
                break;
            }
        }
    }

    private void FrmProfitNLoss_Shown(object sender, EventArgs e)
    {
        CmbDateType.SelectedIndex = _isNormal.Equals("PL") ? 9 : 8;
        ChkDate.Text = ObjGlobal.SysDateType == "M" ? "Date" : "Miti";
        ChkIncludeLedger.Checked = ChkIncludeLedger.Enabled = false;
        if (CmbDateType.Enabled)
        {
            CmbDateType.Focus();
        }
        else
        {
            MskToDate.Focus();
        }
    }

    private void rChkNormal_CheckedChanged(object sender, EventArgs e)
    {
        if (rChkNormal.Checked)
        {
            rChkPeriodic.Enabled = rChkPeriodic.Visible = !rChkNormal.Checked;
            rChkPeriodic.Checked = !rChkNormal.Checked;
            CmbDateType.Enabled = false;
        }
        else if (rChkPeriodic.Checked)
        {
            rChkPeriodic.Enabled = rChkPeriodic.Visible = !rChkNormal.Checked;
            rChkPeriodic.Checked = !rChkNormal.Checked;
            MskFrom.Enabled = MskFrom.Visible = true;
            CmbDateType.Enabled = true;
        }
    }

    private void rChkPeriodic_CheckedChanged(object sender, EventArgs e)
    {
        rChkNormal_CheckedChanged(sender, e);
    }

    private void BtnShow_Click(object sender, EventArgs e)
    {
        var reportOption = "Ledger";
        if (!MskFrom.MaskCompleted)
        {
            MskFrom.WarningMessage(@"FROM DATE CAN'T BE BLANK..!!");
            return;
        }
        if (!MskToDate.MaskCompleted && rChkPeriodic.Checked)
        {
            MskToDate.WarningMessage(@"FROM DATE CAN'T BE BLANK..!!");
            return;
        }
        _fromAdDate = ObjGlobal.SysDateType == "M" ? _fromAdDate.GetEnglishDate(MskFrom.Text) : MskFrom.Text;
        _fromBsDate = ObjGlobal.SysDateType == "M" ? MskFrom.Text : _fromBsDate.GetNepaliDate(MskFrom.Text);
        _toAdDate = ObjGlobal.SysDateType == "M" ? _toAdDate.GetEnglishDate(MskToDate.Text) : MskToDate.Text;
        _toBsDate = ObjGlobal.SysDateType == "M" ? MskToDate.Text : _toAdDate.GetNepaliDate(MskToDate.Text);

        if (rChkLedger.Checked)
        {
            reportOption = "Ledger";
            if (ChkIncludeSubledger.Checked) reportOption = "Ledger/SubLedger";
        }
        else if (rChkAccountGroup.Checked)
        {
            _details = ChkIncludeLedger.Checked;
            reportOption = ChkIncludeLedger.Checked ? "Account Group/Ledger" : "Account Group";
        }
        else if (rChkAccountSubGroup.Checked)
        {
            _details = ChkIncludeLedger.Checked;
            reportOption = ChkIncludeLedger.Checked ? "Account Group/Sub Group/Ledger" : "Account Group/Sub Group";
        }

        switch (ObjGlobal.SysDateType)
        {
            case "M" when !ChkDate.Checked:
            case "D" when ChkDate.Checked:
            {
                RptDate = $"From Date {_fromBsDate} To {_toBsDate}";
                break;
            }
            case "D" when !ChkDate.Checked:
            case "M" when ChkDate.Checked:
            {
                RptDate =
                    $"FROM DATE {_fromAdDate.GetDateString()} TO {_toAdDate.GetDateString()}";
                break;
            }
        }

        var orderBy =
            rChkSchedule.Checked ? "SCHEDULE" :
            rChkShortName.Checked ? "SHORTNAME" : "DESCRIPTION";
        var report = new DisplayFinanceReports
        {
            Text = Text,
            RptName = "PROFIT & LOSS",
            RptType = rChkPeriodic.Checked ? "PERIODIC" : ChkTFormat.Checked ? "T FORMAT" : "NORMAL",
            ReportOption = reportOption,
            SortBy = orderBy,
            RptDate = RptDate,
            IsOpeningOnly = false,
            IsProfitLoss = true,
            IsClosingStock = ChkClosingStock.Checked,
            IsRePostValue = ChkRePost.Checked,
            BranchId = _branchId.IsValueExits() ? _branchId : ObjGlobal.SysBranchId.ToString(),
            CompanyUnitId = ObjGlobal.SysCompanyUnitId.ToString(),
            FiscalYearId = ObjGlobal.SysFiscalYearId.ToString(),
            FromAdDate = _fromAdDate,
            FromBsDate = _fromBsDate,
            ToAdDate = _toAdDate,
            ToBsDate = _toBsDate
        };
        report.Show(this);
    }

    private void CmbSysDateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjGlobal.LoadDate("R", CmbDateType.SelectedIndex, MskFrom, MskToDate);
    }

    private void CmbSysDateType_KeyPress(object sender, KeyPressEventArgs e)
    {
        switch (e.KeyChar)
        {
            case (char)Keys.Enter:
            {
                SendKeys.Send("{TAB}");
                break;
            }
            case (char)Keys.Space:
            {
                SendKeys.Send("{F4}");
                break;
            }
        }
    }

    private void MskToDate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (MskToDate.MaskCompleted)
        {
            if (!MskToDate.IsDateExits(ObjGlobal.SysDateType))
            {
                MskToDate.WarningMessage("ENTER DATE IS INVALID..!!");
                return;
            }
            if (!MskToDate.IsValidDateRange(ObjGlobal.SysDateType))
            {
                MskToDate.WarningMessage("ENTER DATE IS OUT OF RANGE..!!");
                return;
            }
        }
        else
        {
            if (MskToDate.ValidControl(ActiveControl))
            {
                MskToDate.WarningMessage("ENTER DATE IS INVALID..!!");
                return;
            }
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
        {
            Close();
        }
    }

    private void ChkClosingStock_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkClosingStock.Checked)
        {
            ChkRePost.Enabled = true;
        }
        else
        {
            ChkRePost.Enabled = false;
            ChkRePost.Checked = false;
        }
    }

    private void rChkAccountGroup_CheckedChanged(object sender, EventArgs e)
    {
        ChkIncludeLedger.Enabled = ChkIncludeLedger.Checked = rChkAccountGroup.Checked;
    }

    private void rChkAccountSubGroup_CheckedChanged(object sender, EventArgs e)
    {
        ChkIncludeLedger.Enabled = ChkIncludeLedger.Checked = rChkAccountSubGroup.Checked;
    }

    private void rChkLedger_CheckedChanged(object sender, EventArgs e)
    {
        ChkIncludeLedger.Enabled = ChkIncludeLedger.Checked = !rChkLedger.Checked;
    }

    #endregion --------------- Profit & Loss ---------------

    // METHOD FOR THIS FORM

    #region --------------- METHOD FOR THIS FORM ---------------

    private void MethodForControl()
    {
        switch (_isNormal)
        {
            case "PL":
            {
                rChkNormal.Checked = true;
                rChkPeriodic.Enabled = rChkPeriodic.Visible = rChkPeriodic.Checked = false;
                rChkNormal.Focus();
                break;
            }
            case "PPL":
            {
                rChkPeriodic.Location = rChkNormal.Location;
                rChkPeriodic.Enabled = rChkPeriodic.Checked = true;
                rChkNormal.Enabled = rChkNormal.Visible = rChkNormal.Checked = false;
                rChkPeriodic.Focus();
                break;
            }
        }
    }

    #endregion --------------- METHOD FOR THIS FORM ---------------

    // OBJECT FOR THIS FORM

    #region --------------- OBJECT  ---------------

    private string _isNormal { get; }
    private string _branchId { get; }
    private string _fromAdDate { get; set; }
    private string _fromBsDate { get; set; }
    private string _toAdDate { get; set; }
    private string _toBsDate { get; set; }
    private string _rptType { get; set; }
    private bool _details { get; set; }
    private string RptDate { get; set; }

    #endregion --------------- OBJECT  ---------------
}