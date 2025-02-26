using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using System;
using System.Windows.Forms;

namespace MrBLL.Reports.Finance_Report.FinalReport;

public partial class FrmTrialBalance : MrForm
{
    // TRIAL BALANCE

    #region --------------- TRIAL BALANCE FORM ---------------

    public FrmTrialBalance(string isNormal)
    {
        InitializeComponent();
        _isNormal = isNormal;
        ObjGlobal.BindDateType(CmbDateType, _isNormal.Equals("TB") ? 9 : 8);
        ObjGlobal.BindPeriodicDate(MskFrom, MskToDate);
        ChkDate.Text = ObjGlobal.SysDateType is "M" ? "Date" : "Miti";
        FormMethod();
    }

    private void FrmTrialBalance_Load(object sender, EventArgs e)
    {
        Text = _isNormal switch
        {
            "OB" => "OPENING TRIAL BALANCE REPORT [NORMAL]",
            "PTB" => "TRIAL BALANCE REPORT [PERIODIC]",
            _ => "TRIAL BALANCE REPORT [NORMAL]"
        };
        ChkIncludeLedger.Enabled = ChkIncludeLedger.Enabled = !rChkLedger.Checked;
    }

    private void FrmTrialBalance_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            BtnCancel.PerformClick();
        }
    }

    private void FrmTrialBalance_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void RbNormal_CheckedChanged(object sender, EventArgs e)
    {
        if (rChkNormal.Checked)
        {
            RptType = "Normal";
            MskFrom.Text = ObjGlobal.SysDateType == "M"
                ? ObjGlobal.ReturnNepaliDate(DateTime.Now.ToShortDateString())
                : DateTime.Now.GetDateString();
        }
        else if (RbtnPeriodic.Checked)
        {
            RptType = "Periodic";
            MskFrom.Text = ObjGlobal.SysDateType == "M" ? ObjGlobal.CfStartBsDate : ObjGlobal.CfStartAdDate.GetDateString();
            MskToDate.Text = ObjGlobal.SysDateType == "M" ? ObjGlobal.CfEndBsDate : ObjGlobal.CfEndAdDate.GetDateString();
        }
    }

    private void RbPeriodic_CheckedChanged(object sender, EventArgs e)
    {
        RbNormal_CheckedChanged(sender, e);
    }

    private void MskFrom_DateEnter(object sender, EventArgs e)
    {
        if (MouseButtons == MouseButtons.None)
        {
            MskFrom.SelectAll();
        }
        ObjGlobal.MskTxtBackColor(MskFrom, 'E');
    }

    private void msk_FromDate_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskFrom, 'L');
    }

    private void MskToDate_Enter(object sender, EventArgs e)
    {
        if (MouseButtons == MouseButtons.None)
        {
            MskToDate.SelectAll();
        }
        ObjGlobal.MskTxtBackColor(MskToDate, 'E');
    }

    private void MskToDate_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskToDate, 'L');
    }

    private void BtnShow_Click(object sender, EventArgs e)
    {
        if (!MskFrom.MaskCompleted)
        {
            MessageBox.Show(@"FROM DATE CAN'T BE BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            MskFrom.Focus();
            return;
        }
        if (!rChkNormal.Checked && !MskToDate.MaskCompleted)
        {
            MessageBox.Show(@"FROM DATE CAN'T BE BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            MskToDate.Focus();
            return;
        }
        FromAdDate = ObjGlobal.SysDateType is "M" ? ObjGlobal.ReturnEnglishDate(MskFrom.Text) : MskFrom.Text;
        FromBsDate = ObjGlobal.SysDateType is "M" ? MskFrom.Text : ObjGlobal.ReturnNepaliDate(MskFrom.Text);
        ToAdDate = ObjGlobal.SysDateType is "M" ? ObjGlobal.ReturnEnglishDate(MskToDate.Text) : MskToDate.Text;
        ToBsDate = ObjGlobal.SysDateType is "M" ? MskToDate.Text : ObjGlobal.ReturnNepaliDate(MskToDate.Text);
        if (rChkLedger.Checked)
        {
            ReportOption = ChkIncludeSubledger.Checked ? "Ledger/SubLedger" : "Ledger";
        }
        else if (rChkAccountGroup.Checked)
        {
            if (ChkIncludeLedger.Checked)
            {
                _details = true;
                ReportOption = "Account Group/Ledger";
            }
            else
            {
                _details = false;
                ReportOption = "Account Group";
            }
        }
        else if (rChkAccountSubGroup.Checked)
        {
            if (ChkIncludeLedger.Checked)
            {
                _details = true;
                ReportOption = "Account Group/Sub Group/Ledger";
            }
            else
            {
                _details = false;
                ReportOption = "Account Group/Sub Group";
            }
        }

        if (rChkNormal.Checked && RbtnPeriodic.Enabled == false)
        {
            RptType = "Normal";
            switch (ObjGlobal.SysDateType)
            {
                case "M" when !ChkDate.Checked:
                case "D" when ChkDate.Checked:
                {
                    RptDate = "As On " + ToBsDate;
                    break;
                }
                case "D" when !ChkDate.Checked:
                case "M" when ChkDate.Checked:
                {
                    RptDate = "As On " + Convert.ToDateTime(ToAdDate).ToShortDateString();
                    break;
                }
            }
        }
        else if (RbtnPeriodic.Checked && rChkNormal.Enabled == false)
        {
            RptType = "Periodic";
            switch (ObjGlobal.SysDateType)
            {
                case "M" when !ChkDate.Checked:
                case "D" when ChkDate.Checked:
                {
                    RptDate = $"From Date {FromBsDate} To {ToBsDate}";
                    break;
                }
                case "D" when !ChkDate.Checked:
                case "M" when ChkDate.Checked:
                {
                    RptDate = $"From Date {FromAdDate.GetDateTime()} To {ToAdDate.GetDateTime()}";
                    break;
                }
            }
        }
        var orderBy = rChkSchedule.Checked ? "SCHEDULE" : rChkShortName.Checked ? "SHORTNAME" : "DESCRIPTION";
        var rptReport = new DisplayFinanceReports
        {
            Text = Text,
            IncludeLedger = ChkIncludeLedger.Checked,
            RptName = rChkOpening.Checked ? "OPENING BALANCE" : "TRIAL BALANCE",
            RptDate = RptDate,
            IncludePdc = ChkIncludePdc.Checked,
            IncludeShortName = ChkShortName.Checked,
            RptType = RbtnPeriodic.Checked ? "PERIODIC" : ChkTFormat.Checked ? "T FORMAT" : "NORMAL",
            IsOpeningOnly = rChkOpening.Checked,
            IsBalanceSheet = false,
            BranchId = !string.IsNullOrEmpty(_branchId) ? _branchId : ObjGlobal.SysBranchId.ToString(),
            ReportOption = ReportOption,
            FromAdDate = FromAdDate,
            FromBsDate = FromBsDate,
            ToAdDate = ToAdDate,
            ToBsDate = ToBsDate,
            IsZeroBalance = ChkZeroBalance.Checked,
            IsCombineCustomerVendor = ChkCombineCustomerVendor.Checked,
            SortBy = orderBy,
            IsTrialBalance = true
        };
        rptReport.Show();
    }

    private void cmbSysDateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CmbDateType != null)
        {
            ObjGlobal.LoadDate("R", CmbDateType.SelectedIndex, MskFrom, MskToDate);
        }
    }

    private void cmbSysDateType_Enter(object sender, EventArgs e)
    {
        ObjGlobal.ComboBoxBackColor(CmbDateType, 'E');
    }

    private void cmbSysDateType_Leave(object sender, EventArgs e)
    {
        if (CmbDateType != null)
        {
            ObjGlobal.ComboBoxBackColor(CmbDateType, 'L');
        }
    }

    private void cmbSysDateType_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Space)
        {
            SendKeys.Send("{F4}");
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (MessageBox.Show(@"DO YOU WANT TO CLOSE THE WINDOW..??", ObjGlobal.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) is DialogResult.Yes)
        {
            Close();
        }
    }

    private void ChkDetails_Click(object sender, EventArgs e)
    {
    }

    private void RbtnAccountGroup_CheckedChanged(object sender, EventArgs e)
    {
        ChkIncludeLedger.Enabled = rChkAccountGroup.Checked;
    }

    private void RbtnAccountSubGroup_CheckedChanged(object sender, EventArgs e)
    {
        ChkIncludeLedger.Enabled = rChkAccountSubGroup.Checked;
    }

    private void RbtnLedger_CheckedChanged(object sender, EventArgs e)
    {
        ChkIncludeLedger.Enabled = ChkIncludeLedger.Checked = !rChkLedger.Checked;
    }

    private void FormMethod()
    {
        switch (_isNormal)
        {
            case "TB":
            {
                CmbDateType.SelectedIndex = 9;
                CmbDateType.Enabled = RbtnPeriodic.Checked = RbtnPeriodic.Enabled = RbtnPeriodic.Visible = rChkOpening.Visible = false;
                rChkNormal.Checked = true;
                MskFrom.Enabled = MskFrom.Visible = false;
                MskToDate.Location = MskFrom.Location;
                MskFrom.Text = ObjGlobal.SysDateType is "M" ? ObjGlobal.CfStartBsDate : ObjGlobal.CfStartAdDate.ToString("dd/MM/yyyy");
                MskToDate.Text = ObjGlobal.SysDateType is "M" ? ObjGlobal.ReturnNepaliDate(DateTime.Now.ToString("dd/MM/yyyy")) : DateTime.Now.ToString("dd/MM/yyyy");
                if (DateTime.Now > ObjGlobal.CfEndAdDate)
                {
                    MskToDate.Text = ObjGlobal.SysDateType is "M" ? ObjGlobal.CfEndBsDate : ObjGlobal.CfEndAdDate.GetDateString();
                }
                MskToDate.Enabled = true;
                rChkNormal.Focus();
                break;
            }
            case "PTB":
            {
                CmbDateType.SelectedIndex = 8;
                rChkNormal.Checked = rChkNormal.Enabled = rChkNormal.Visible = rChkOpening.Visible = false;
                CmbDateType.Enabled = RbtnPeriodic.Checked = true;
                RbtnPeriodic.Location = rChkNormal.Location;
                RbtnPeriodic.Checked = true;
                RbtnPeriodic.Focus();
                break;
            }
            case "OB":
            {
                CmbDateType.SelectedIndex = 8;
                CmbDateType.Visible = RbtnPeriodic.Checked = RbtnPeriodic.Visible = false;
                rChkNormal.Checked = rChkNormal.Enabled = rChkNormal.Visible = false;
                rChkOpening.Location = rChkNormal.Location;
                rChkOpening.Checked = true;
                rChkOpening.Focus();
                break;
            }
        }
    }

    #endregion --------------- TRIAL BALANCE FORM ---------------

    //OBJECT FOR THIS FORM

    #region --------------- OBJECT ---------------

    private readonly string _isNormal = "Normal";
    private bool _details;
    public string FromAdDate = string.Empty;
    public string FromBsDate = string.Empty;
    public string ReportOption = string.Empty;
    public string RptDate = string.Empty;
    public string RptType = string.Empty;
    public string ToAdDate = string.Empty;
    public string ToBsDate = string.Empty;
    private readonly string _branchId = string.Empty;

    #endregion --------------- OBJECT ---------------
}