using MrBLL.Utility.Common;
using MrDAL.Control.WinControl;
using MrDAL.Utility.PickList;
using System;
using System.Drawing;
using System.Windows.Forms;
using static MrDAL.Global.Common.ObjGlobal;

namespace MrBLL.Reports.Register_Report.OutStanding_Report;

public partial class FrmOutstandingSalesChallan : MrForm
{
    #region --------------- OUTSTANDING SALES CHALLAN ---------------

    public sealed override Color BackColor
    {
        get => base.BackColor;
        set => base.BackColor = value;
    }

    public FrmOutstandingSalesChallan()
    {
        InitializeComponent();
        BackColor = FrmBackColor();
        BindDateType(CmbDateType);
        BindPeriodicDate(MskFrom, MskToDate);
    }

    private void OutstandingSalesChallan_Load(object sender, EventArgs e)
    {
        CmbDateType.SelectedIndex = 8;
        ChkSummary_CheckedChanged(sender, e);
        ChkDateWise.Text = SysDateType.Equals("M") ? "Date" : "Miti";
        CmbDateType.Focus();
    }

    private void OutstandingSalesChallan_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) SendKeys.Send("{TAB}");
        if (e.KeyChar == (char)Keys.Escape) BtnCancel.PerformClick();
    }

    #endregion --------------- OUTSTANDING SALES CHALLAN ---------------

    #region --------------- EVENT OF THIS FORM ---------------

    private void ChkSummary_CheckedChanged(object sender, EventArgs e)
    {
        ChkDateWise.Enabled = !ChkSummary.Checked;
        ChkWithAdjustment.Enabled = ChkSummary.Checked;
        ChkIncludeOrderNo.Enabled = ChkSummary.Checked;
        ChkIncludeChallanNo.Enabled = ChkSummary.Checked;
        ChkIncludeBatch.Enabled = ChkSummary.Checked;
        ChkIncludeGodown.Enabled = ChkSummary.Checked;
        ChkIncludeAltQty.Enabled = ChkSummary.Checked;
        ChkFreeQty.Enabled = ChkSummary.Checked;
        ChkIncludeNarration.Enabled = !ChkSummary.Checked;
        ChkIncludeNarration.Checked =
            (!ChkSummary.Checked || !ChkIncludeNarration.Checked) && ChkIncludeNarration.Checked;
    }

    private void CmbDateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDate("R", CmbDateType.SelectedIndex, MskFrom, MskToDate);
    }

    private void MskFrom_Validated(object sender, EventArgs e)
    {
        if (MskFrom.Text.Trim() != "/  /")
        {
            if (SysDateType == "D")
            {
                if (ValidDate(MskFrom.Text, SysDateType))
                {
                    if (ValidDateRange(Convert.ToDateTime(MskFrom.Text))) return;
                    MessageBox.Show($@"DATE MUST BE BETWEEN {CfStartAdDate} AND {CfEndAdDate} ", Caption,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MskFrom.Focus();
                }
                else
                {
                    MessageBox.Show(@"Plz. Enter Valid Date !", Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    MskFrom.Focus();
                }
            }
            else
            {
                if (ValidDate(MskFrom.Text, SysDateType))
                {
                    if (ValidDateRange(Convert.ToDateTime(ReturnEnglishDate(MskFrom.Text))) == false)
                    {
                        MessageBox.Show(
                            $@"DATE MUST BE BETWEEN {ReturnNepaliDate(CfStartAdDate.ToShortDateString())} AND {ReturnNepaliDate(CfEndAdDate.ToShortDateString())} ",
                            Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MskFrom.Focus();
                    }
                }
                else
                {
                    MessageBox.Show(@"Plz. Enter Valid Date !", Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    MskFrom.Focus();
                }
            }
        }
        else
        {
            MessageBox.Show(@"FROM DATE CANNOT BE LEFT BLANK..!!", Caption);
            MskFrom.Focus();
        }
    }

    private void MskToDate_Validated(object sender, EventArgs e)
    {
        if (MskToDate.MaskCompleted)
        {
            if (SysDateType == "D")
            {
                if (ValidDate(MskToDate.Text, SysDateType))
                {
                    if (ValidDateRange(Convert.ToDateTime(MskToDate.Text))) return;
                    MessageBox.Show(
                        $@"Date Must be Between {CfStartAdDate} and {CfEndAdDate} ",
                        Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MskToDate.Focus();
                }
                else
                {
                    MessageBox.Show(@"PLZ. ENTER VALID DATE..!!", Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    MskToDate.Focus();
                }
            }
            else
            {
                if (ValidDate(MskToDate.Text, SysDateType))
                {
                    if (ValidDateRange(Convert.ToDateTime(ReturnEnglishDate(MskToDate.Text))))
                        return;
                    MessageBox.Show(
                        $@"DATE MUST BE BETWEEN {ReturnNepaliDate(CfStartAdDate.ToShortDateString())} AND {ReturnNepaliDate(CfEndAdDate.ToShortDateString())} ",
                        Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MskToDate.Focus();
                }
                else
                {
                    MessageBox.Show(@"PLZ. ENTER VALID DATE..!!", Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    MskToDate.Focus();
                }
            }
        }
        else
        {
            MessageBox.Show(@"TO DATE CANNOT BE LEFT BLANK..!!", Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            MskToDate.Focus();
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        var dialogResult = MessageBox.Show(@"ARE YOU SURE WANT TO CLOSE FORM..??", Caption,
            MessageBoxButtons.YesNo);
        if (dialogResult == DialogResult.Yes) Close();
    }

    private void BtnShow_Click(object sender, EventArgs e)
    {
        RptType = "Normal";
        RptName = "SALES OUTSTANDING";
        FromADDate = SysDateType == "M"
            ? ReturnEnglishDate(MskFrom.Text)
            : MskFrom.Text;
        FromBSDate = SysDateType == "M" ? MskFrom.Text : ReturnNepaliDate(MskFrom.Text);
        ToADDate = SysDateType == "M" ? ReturnEnglishDate(MskToDate.Text) : MskToDate.Text;
        ToBSDate = SysDateType == "M" ? MskToDate.Text : ReturnNepaliDate(MskToDate.Text);
        RptStartDate = SysDateType == "M" ? FromBSDate : FromADDate;
        RptEndDate = SysDateType == "M" ? ToBSDate : ToADDate;

        RepotMode =
            rChkCustomer.Checked ? "Customer Wise"
            : rChkAgent.Checked ? "Agent Wise"
            : rChkArea.Checked ? "Area Wise"
            : rChkDepartment.Checked ? "Department Wise"
            : rChkDate.Checked ? "Date Wise"
            : rChkInvoice.Checked ? "Invoice Wise" : "Date Wise";

        if (ChkSelectAll.Checked)
        {
        }
        else if (rChkCustomer.Checked)
        {
            using var frm2 = new FrmTagList
            {
                ReportDesc = @"GENERAL_LEDGER",
                Category = "Customer",
                BranchId = SysBranchId.ToString(),
                CompanyUnitId = SysCompanyUnitId.ToString(),
                FiscalYearId = SysFiscalYearId.ToString()
            };
            frm2.ShowDialog();
            LedgerId = ClsTagList.PlValue1;
            if (string.IsNullOrEmpty(LedgerId))
            {
                MessageBox.Show(@"LEDGER NOT SELECTED..!!", Caption);
                return;
            }
        }
        else if (rChkAgent.Checked)
        {
            using var frm2 = new FrmTagList
            {
                ReportDesc = @"AGENT",
                BranchId = SysBranchId.ToString(),
                CompanyUnitId = SysCompanyUnitId.ToString(),
                FiscalYearId = SysFiscalYearId.ToString()
            };
            frm2.ShowDialog();
            AgentId = ClsTagList.PlValue1;
            if (string.IsNullOrEmpty(AgentId))
            {
                MessageBox.Show(@"SALES AGENT ARE NOT SELECTED..!!", Caption);
                return;
            }
        }
        else if (rChkArea.Checked)
        {
            using var frm2 = new FrmTagList
            {
                ReportDesc = @"AREA",
                BranchId = SysBranchId.ToString(),
                CompanyUnitId = SysCompanyUnitId.ToString(),
                FiscalYearId = SysFiscalYearId.ToString()
            };
            frm2.ShowDialog();

            AreaId = ClsTagList.PlValue1;
            if (string.IsNullOrEmpty(AreaId))
            {
                MessageBox.Show(@"SALES AREA ARE NOT SELECTED..!!", Caption);
                return;
            }
        }
        else if (rChkDepartment.Checked)
        {
            using var frm2 = new FrmTagList
            {
                ReportDesc = @"DEPARTMENT",
                BranchId = SysBranchId.ToString(),
                CompanyUnitId = SysCompanyUnitId.ToString(),
                FiscalYearId = SysFiscalYearId.ToString()
            };
            frm2.ShowDialog();

            DepartmentId = ClsTagList.PlValue1;
            if (string.IsNullOrEmpty(DepartmentId))
            {
                MessageBox.Show(@"SALES DEPARTMENT ARE NOT SELECTED..!!", Caption);
                return;
            }
        }

        RptDate = SysDateType == "M" && ChkDateWise.Checked ||
                  SysDateType == "D" && ChkDateWise.Checked
            ? $"FROM DATE {FromBSDate} TO {ToBSDate}"
            : $"FROM DATE {Convert.ToDateTime(FromADDate).ToShortDateString()} TO {Convert.ToDateTime(ToADDate).ToShortDateString()}";

        using var display = new DisplayRegisterReports
        {
            RptMode = RepotMode,
            RptType = RptType,
            RptName = RptName,
            RptDate = RptDate,
            FromAdDate = FromADDate,
            ToAdDate = ToADDate,
            IsSummary = ChkSummary.Checked,
            IsAdditionalTerm = ChkAdditionalTerm.Checked,
            IncludeAdjustment = ChkWithAdjustment.Checked,
            IncludeSalesOrder = ChkIncludeOrderNo.Checked,
            IncludeSalesChallan = ChkIncludeChallanNo.Checked,
            IncludeGodown = ChkIncludeGodown.Checked,
            IncludeAltQty = ChkIncludeAltQty.Checked,
            IncludeFreeQty = ChkFreeQty.Checked,
            IncludeRemarks = ChkIncludeRemarks.Checked,
            IncludeNarration = ChkIncludeNarration.Checked,
            IsDate = ChkDateWise.Checked,
            LedgerId = LedgerId,
            CounterId = CounterId,
            AgentId = AgentId,
            AreaId = AreaId,
            DepartmentId = DepartmentId,
            Module = "SC",
            EntryUser = GetEntryUser,
            InvoiceType = InvoiceCategory,
            BranchId = SysBranchId.ToString(),
            FiscalYearId = SysFiscalYearId.ToString(),
            CompanyUnitId = SysCompanyUnitId.ToString()
        };
        display.Show();
        InvoiceNo = string.Empty;
        LedgerId = string.Empty;
        DepartmentId = string.Empty;
        AgentId = string.Empty;
        SubLedgerId = string.Empty;
        InvoiceCategory = string.Empty;
        GetEntryUser = string.Empty;
        AreaId = string.Empty;
        CounterId = string.Empty;
    }

    #endregion --------------- EVENT OF THIS FORM ---------------

    #region --------------- Class ---------------

    private string RptType { get; set; }
    private string RepotMode { get; set; }
    private string RptName { get; set; }
    private string RptStartDate { get; set; }
    private string RptEndDate { get; set; }
    private string RptDate { get; set; }
    private string FromADDate { get; set; }
    private string FromBSDate { get; set; }
    private string ToADDate { get; set; }
    private string ToBSDate { get; set; }
    private string GetEntryUser { get; set; } = string.Empty;
    private string InvoiceNo { get; set; } = string.Empty;
    private string LedgerId { get; set; } = string.Empty;
    private string DepartmentId { get; set; } = string.Empty;
    private string AgentId { get; set; } = string.Empty;
    private string AreaId { get; set; } = string.Empty;
    private string CounterId { get; set; } = string.Empty;
    private string SubLedgerId { get; set; } = string.Empty;
    private string InvoiceCategory { get; set; } = string.Empty;

    #endregion --------------- Class ---------------
}