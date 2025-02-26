using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using System;
using System.Windows.Forms;

namespace MrBLL.Reports.Finance_Report.FinalReport;

public partial class FrmBalanceSheet : MrForm
{
    // METHOD FOR THIS FORM

    #region --------------- METHOD FOR THIS FORM ---------------

    internal void MethodForControl()
    {
        switch (_isNormal)
        {
            case "BS":
            {
                rChkNormal.Checked = true;
                rChkPeriodic.Enabled = rChkPeriodic.Visible = rChkPeriodic.Checked = false;
                rChkOpening.Enabled = rChkOpening.Visible = rChkOpening.Checked = false;
                rChkNormal.Focus();
                break;
            }
            case "PBS":
            {
                rChkPeriodic.Location = rChkNormal.Location;
                rChkPeriodic.Enabled = rChkPeriodic.Checked = true;
                rChkNormal.Enabled = rChkNormal.Visible = rChkNormal.Checked = false;
                rChkOpening.Enabled = rChkOpening.Visible = rChkOpening.Checked = false;
                rChkPeriodic.Focus();
                break;
            }
            case "OB":
            {
                rChkOpening.Location = rChkNormal.Location;
                rChkOpening.Checked = true;
                rChkNormal.Enabled = rChkNormal.Visible = rChkNormal.Checked = false;
                rChkPeriodic.Enabled = rChkPeriodic.Visible = rChkPeriodic.Checked = false;
                rChkOpening.Focus();
                break;
            }
        }
    }

    #endregion --------------- METHOD FOR THIS FORM ---------------

    #region --------------- BALANCE SHEET ---------------

    public FrmBalanceSheet(string isNormal)
    {
        InitializeComponent();
        _isNormal = isNormal;
        ObjGlobal.BindDateType(CmbDateType, _isNormal.Equals("BS") ? 9 : 8);
        ObjGlobal.BindPeriodicDate(MskFrom, MskToDate);
        CmbDateType.SelectedIndex = 8;
        ChkDate.Text = ObjGlobal.SysDateType == "M" ? "Date" : "Miti";
    }

    private void FrmBalanceSheet_Load(object sender, EventArgs e)
    {
        Text = _isNormal switch
        {
            "OB" => "BALANCE SHEET REPORT [OPENING]",
            "PBS" => "BALANCE SHEET REPORT [PERIODIC]",
            _ => "BALANCE SHEET REPORT [NORMAL]"
        };
        MethodForControl();
        rChkNormal_CheckedChanged(sender, e);
        rChkLedger_CheckedChanged(sender, e);
        ChkIncludeClosingStock_CheckedChanged(sender, e);
        CmbDateType.Focus();
    }

    private void FrmBalanceSheet_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape) BtnCancel.PerformClick();
    }

    private void FrmBalanceSheet_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    #endregion --------------- BALANCE SHEET ---------------

    //EVENTS OF CONTROL

    #region --------------- EVENTS ---------------

    private void rChkNormal_CheckedChanged(object sender, EventArgs e)
    {
        if (rChkNormal.Checked)
        {
            var txtDateType = new TextBox();
            mrGroup1.Controls.Add(txtDateType);
            rChkPeriodic.Checked = !rChkNormal.Checked;
            txtDateType.Text = @"AS ON DATE";
            txtDateType.Enabled = false;
            MskFrom.Enabled = MskFrom.Visible = false;
            MskToDate.Visible = MskToDate.Enabled = true;
            MskToDate.Location = MskFrom.Location;
            CmbDateType.Enabled = CmbDateType.Visible = false;
            txtDateType.Location = CmbDateType.Location;
            txtDateType.Size = CmbDateType.Size;
            MskFrom.Text = ObjGlobal.SysDateType == "M"
                ? ObjGlobal.CfStartBsDate
                : ObjGlobal.CfStartAdDate.ToString("dd/MM/yyyy");
            MskToDate.Text = ObjGlobal.SysDateType is "M"
                ? ObjGlobal.ReturnNepaliDate(DateTime.Now.ToString("dd/MM/yyyy"))
                : DateTime.Now.ToString("dd/MM/yyyy");
        }
        else if (rChkPeriodic.Checked)
        {
            rChkPeriodic.Checked = !rChkNormal.Checked;
            MskFrom.Enabled = MskFrom.Visible = true;
            CmbDateType.Enabled = true;
            if (ObjGlobal.SysDateType == "M")
            {
                MskFrom.Text = ObjGlobal.CfStartBsDate;
                MskToDate.Text = ObjGlobal.CfEndBsDate;
            }
            else
            {
                MskFrom.Text = ObjGlobal.CfStartAdDate.ToString("dd/MM/yyyy");
                MskToDate.Text = ObjGlobal.CfEndAdDate.ToString("dd/MM/yyyy");
            }

            CmbDateType.SelectedIndex = 8;
        }
        else if (rChkOpening.Checked)
        {
            CmbDateType.Enabled = MskToDate.Enabled = MskToDate.Enabled = false;
            CmbDateType.Visible = MskToDate.Visible = MskToDate.Visible = false;
        }
    }

    private void rChkPeriodic_CheckedChanged(object sender, EventArgs e)
    {
        rChkNormal_CheckedChanged(sender, e);
    }

    private void MskFrom_Enter(object sender, EventArgs e)
    {
        if (MouseButtons == MouseButtons.None) MskFrom.SelectAll();
        ObjGlobal.MskTxtBackColor(MskFrom, 'E');
    }

    private void MskFrom_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskFrom, 'L');
    }

    private void MskToDate_Enter(object sender, EventArgs e)
    {
        if (MouseButtons == MouseButtons.None) MskToDate.SelectAll();
        ObjGlobal.MskTxtBackColor(MskToDate, 'E');
    }

    private void MskToDate_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskToDate, 'L');
    }

    private void rChkLedger_CheckedChanged(object sender, EventArgs e)
    {
        if (rChkLedger.Checked)
            ChkIncludeLedger.Enabled = ChkIncludeLedger.Checked = false;
        else
            ChkIncludeLedger.Enabled = ChkIncludeLedger.Checked = true;
    }

    private void CmbDateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjGlobal.LoadDate("R", CmbDateType.SelectedIndex, MskFrom, MskToDate);
    }

    private void ChkIncludeClosingStock_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkIncludeClosingStock.Checked)
        {
            ChkRePost.Enabled = true;
        }
        else
        {
            ChkRePost.Enabled = false;
            ChkRePost.Checked = false;
        }
    }

    private void BtnShow_Click(object sender, EventArgs e)
    {
        if (!MskFrom.MaskCompleted && rChkNormal.Checked)
        {
            MessageBox.Show(@"FROM DATE CAN'T BE BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            MskFrom.Focus();
            return;
        }

        if (!rChkNormal.Checked && !MskToDate.MaskCompleted)
        {
            MessageBox.Show(@"FROM DATE CAN'T BE BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            MskToDate.Focus();
            return;
        }

        if (rChkLedger.Checked)
        {
            ReportOption = ChkIncludeSubledger.Checked ? "Ledger/SubLedger" : "Ledger";
        }
        else if (rChkAccountGroup.Checked)
        {
            _details = ChkIncludeLedger.Checked;
            ReportOption = ChkIncludeLedger.Checked ? "Account Group/Ledger" : "Account Group";
        }
        else if (rChkAccountSubGroup.Checked)
        {
            _details = ChkIncludeLedger.Checked;
            ReportOption = ChkIncludeLedger.Checked ? "Account Group/Sub Group/Ledger" : "Account Group/Sub Group";
        }

        _fromAdDate = ObjGlobal.SysDateType is "M" ? ObjGlobal.ReturnEnglishDate(MskFrom.Text) : MskFrom.Text;
        _fromBsDate = ObjGlobal.SysDateType is "M" ? MskFrom.Text : ObjGlobal.ReturnNepaliDate(MskFrom.Text);
        _toAdDate = ObjGlobal.SysDateType is "M" ? ObjGlobal.ReturnEnglishDate(MskToDate.Text) : MskToDate.Text;
        _toBsDate = ObjGlobal.SysDateType is "M" ? MskToDate.Text : ObjGlobal.ReturnNepaliDate(MskToDate.Text);

        if (rChkNormal.Checked && !rChkPeriodic.Checked)
        {
            switch (ObjGlobal.SysDateType)
            {
                case "M" when !ChkDate.Checked:
                case "D" when ChkDate.Checked:
                {
                    RptDate = $"AS ON {_fromBsDate}";
                    break;
                }
                case "D" when !ChkDate.Checked:
                case "M" when ChkDate.Checked:
                {
                    RptDate = $"AS ON {_fromAdDate.GetDateTime()}";
                    break;
                }
            }
        }
        else if (rChkPeriodic.Checked && !rChkNormal.Checked)
        {
            RptDate = ObjGlobal.SysDateType switch
            {
                "M" when !ChkDate.Checked => $"From Date {_fromBsDate} To {_toBsDate}",
                "D" when ChkDate.Checked => $"From Date {_fromBsDate} To {_toBsDate}",
                "D" when !ChkDate.Checked => $"From Date {_fromAdDate.GetDateTime()} To {_toAdDate.GetDateTime()}",
                "M" when ChkDate.Checked => $"From Date {_fromAdDate.GetDateTime()} To {_toAdDate.GetDateTime()}",
                _ => RptDate
            };
        }
        var orderBy = rChkSchedule.Checked ? "SCHEDULE" : rChkShortName.Checked ? "SHORTNAME" : "DESCRIPTION";
        var reports = new DisplayFinanceReports
        {
            Text = Text,
            RptName = rChkOpening.Checked ? "OPENING BALANCE" : "BALANCE SHEET",
            RptType = rChkPeriodic.Checked ? "PERIODIC" : ChkTFormat.Checked ? "T FORMAT" : "NORMAL",
            RptDate = RptDate,
            IncludeLedger = ChkIncludeLedger.Checked,
            IsOpeningOnly = rChkOpening.Checked,
            IncludePdc = ChkIncluePDC.Checked,
            IsClosingStock = ChkIncludeClosingStock.Checked,
            IsRePostValue = ChkRePost.Checked,
            IsCombineCustomerVendor = ChkCombineCustomerVendor.Checked,
            IsSubLedger = ChkIncludeSubledger.Checked,
            IncludeShortName = ChkShortName.Checked,
            BranchId = _branchId.IsValueExits() ? _branchId : ObjGlobal.SysBranchId.ToString(),
            CompanyUnitId = ObjGlobal.SysCompanyUnitId.ToString(),
            FiscalYearId = ObjGlobal.SysFiscalYearId.ToString(),
            ReportOption = ReportOption,
            SortBy = orderBy,
            FromAdDate = _fromAdDate,
            FromBsDate = _fromBsDate,
            ToAdDate = _toAdDate,
            ToBsDate = _toBsDate
        };
        reports.Show();
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (MessageBox.Show(@"ARE YOU SURE WANT TO CLOSE FORM..??", ObjGlobal.Caption, MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes) Close();
    }

    #endregion --------------- EVENTS ---------------

    //OBJECT OF CONTROL

    #region --------------- OBJECT ---------------

    private readonly string _isNormal = string.Empty;
    private readonly string _branchId = ObjGlobal.SysBranchId.ToString();
    private bool _details;
    private string _fiscalYearId = ObjGlobal.SysFiscalYearId.ToString();
    private string _fromAdDate = string.Empty;
    private string _fromBsDate = string.Empty;
    private string ReportOption = string.Empty;
    private string RptDate = string.Empty;
    private string RptType = string.Empty;
    private string _toAdDate = string.Empty;
    private string _toBsDate = string.Empty;

    #endregion --------------- OBJECT ---------------
}