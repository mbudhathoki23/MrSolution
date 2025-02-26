using MrBLL.Utility.Common;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Utility.PickList;
using System;
using System.Windows.Forms;
using static MrDAL.Global.Common.ObjGlobal;

namespace MrBLL.Reports.Inventory_Report.StockLedger;

public partial class FrmStockLedger : MrForm
{
    public FrmStockLedger(bool isValue)
    {
        InitializeComponent();
        BindDateType(CmbDateType);
        BindPeriodicDate(MskFrom, MskToDate);
        _IsValue = isValue;
        CmbDateType.SelectedIndex = 8;
    }

    private void FrmStockLedger_Load(object sender, EventArgs e)
    {
        FrmControl();
        CmbDateType.Focus();
    }

    private void FrmStockLedger_KeyPress(object sender, KeyPressEventArgs e)
    {
        switch (e.KeyChar)
        {
            case (char)Keys.Enter:
            {
                SendKeys.Send("{TAB}");
                break;
            }
            case (char)Keys.Escape:
            {
                if (MessageBox.Show(@"ARE YOU SURE WANT TO CLOSE FORM..!!", Caption, MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                    Close();
                break;
            }
        }
    }

    private void BtnShow_Click(object sender, EventArgs e)
    {
        FromADDate = SysDateType.Equals("M") ? ReturnEnglishDate(MskFrom.Text) : MskFrom.Text;
        FromBSDate = SysDateType.Equals("M") ? MskFrom.Text : ReturnNepaliDate(MskFrom.Text);
        ToADDate = SysDateType.Equals("M") ? ReturnEnglishDate(MskToDate.Text) : MskToDate.Text;
        ToBSDate = SysDateType == "M" ? MskToDate.Text : ReturnNepaliDate(MskToDate.Text);
        var branchId = SysBranchId.GetIntString();
        var companyUnitId = SysCompanyUnitId.GetIntString();
        var fiscalYearId = SysFiscalYearId.GetIntString();

        ProductGroupId = string.Empty;
        ProductSubGroupId = string.Empty;
        ProductId = string.Empty;
        GodownId = string.Empty;

        if (ChkSelectAll.Checked == false)
        {
            if (rChkGroupWise.Checked || rChkSubGroupWise.Checked)
            {
                var frm = new FrmTagList
                {
                    ReportDesc = "PRODUCTGROUP",
                    BranchId = branchId,
                    CompanyUnitId = companyUnitId,
                    FiscalYearId = fiscalYearId
                };
                frm.ShowDialog();
                ProductGroupId = ClsTagList.PlValue1;
                if (string.IsNullOrEmpty(ProductGroupId))
                {
                    MessageBox.Show(@"PRODUCT GROUP NOT SELECTED ..!!", Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (rChkSubGroupWise.Checked && !string.IsNullOrEmpty(ProductGroupId))
                {
                    var frm1 = new FrmTagList
                    {
                        ReportDesc = "PRODUCTSUBGROUP",
                        BranchId = branchId,
                        CompanyUnitId = companyUnitId,
                        FiscalYearId = fiscalYearId,
                        GroupId = ProductGroupId
                    };
                    frm1.ShowDialog();

                    ProductSubGroupId = ClsTagList.PlValue1;
                    if (string.IsNullOrEmpty(ProductSubGroupId))
                    {
                        MessageBox.Show(@"PRODUCT SUBGROUP NOT SELECTED ..!!", Caption,
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                var frm2 = new FrmTagList
                {
                    ReportDesc = "PRODUCT",
                    BranchId = branchId,
                    CompanyUnitId = companyUnitId,
                    FiscalYearId = fiscalYearId,
                    GroupId = ProductGroupId,
                    SubGroupId = ProductSubGroupId
                };
                frm2.ShowDialog();
                ProductId = ClsTagList.PlValue1;
                if (string.IsNullOrEmpty(ProductId))
                {
                    MessageBox.Show(@"PRODUCT NOT SELECTED FROM THIS GROUP..!!", Caption,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (rChkProductWise.Checked)
            {
                var frm2 = new FrmTagList
                {
                    ReportDesc = "PRODUCT",
                    BranchId = branchId,
                    CompanyUnitId = companyUnitId,
                    FiscalYearId = fiscalYearId,
                    GroupId = ProductGroupId,
                    SubGroupId = ProductSubGroupId
                };
                frm2.ShowDialog();
                ProductId = ClsTagList.PlValue1;
                //ProductId = ClsTagList.PlValue3;
                if (string.IsNullOrEmpty(ProductId))
                {
                    MessageBox.Show(@"PRODUCT NOT SELECTED..!!", Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        if (SysDateType == "M" && ChkDate.Checked == false || SysDateType == "D" && ChkDate.Checked)
            RptDate = $"From Date {FromBSDate} To {ToBSDate}";
        else
            RptDate = $"From Date {FromADDate.GetDateTime()} To {ToADDate.GetDateTime()}";

        var rptMode =
            rChkSubGroupWise.Checked && !ChkOnly.Checked ? "SUBGROUP WISE"
            : rChkGroupWise.Checked && !ChkOnly.Checked ? "GROUP WISE"
            : rChkSubGroupWise.Checked && ChkOnly.Checked ? "SUBGROUP ONLY"
            : rChkGroupWise.Checked && ChkOnly.Checked ? "GROUP ONLY"
            : "PRODUCT WISE";
        var display = new DisplayInventoryReports
        {
            Text = RptName + @" REPORTS",
            RptType = "NORMAL",
            RptName = "STOCK LEDGER",
            RptDate = RptDate,
            RptMode = rptMode,
            FromBsDate = FromBSDate,
            ToBsDate = ToBSDate,
            FromAdDate = FromADDate,
            ToAdDate = ToADDate,
            ProductSubGroupId = ProductSubGroupId,
            ProductGroupId = ProductGroupId,
            ProductId = ProductId,
            BranchId = branchId,
            FiscalYearId = fiscalYearId,
            CompanyUnitId = companyUnitId,
            IsDate = ChkDate.Checked,
            IsSummary = ChkSummary.Checked,
            IncludeAltQty = ChkAltQuantity.Checked,
            IncludeValue = ChkWithValue.Checked,
            IncludeVat = ChkIncludeVat.Checked,
            RePostValue = ChkRePostValue.Checked,
            IncludeZero = ChkZeroBalance.Checked,
            NegativeStock = rChkNegativeInclude.Checked,
            ExcludeNegative = rChkNegativeExclude.Checked,
            NegativeStockOnly = rChkNegativeOnly.Checked
        };
        display.Show();
        ProductGroupId = ProductSubGroupId = ProductId = string.Empty;
    }

    private void CmbDateType_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void CmbDateType_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 32) SendKeys.Send("{F4}");
    }

    private void CmbDateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CmbDateType.Focused)
        {
            LoadDate("R", CmbDateType.SelectedIndex, MskFrom, MskToDate);
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (MessageBox.Show(@"Are you sure want to Close Form!", Caption, MessageBoxButtons.YesNo) ==
            DialogResult.Yes) Close();
    }

    private void ChkDynamicReports_CheckedChanged(object sender, EventArgs e)
    {
        if (!ChkDynamicReports.Focused) return;
        ChkSummary.Enabled = !ChkDynamicReports.Checked;
        ChkAltQuantity.Enabled = !ChkDynamicReports.Checked;
        ChkBatchWise.Enabled = !ChkDynamicReports.Checked;
        ChkZeroBalance.Enabled = !ChkDynamicReports.Checked;
        ChkWithValue.Enabled = !ChkDynamicReports.Checked;
        ChkDate.Enabled = !ChkDynamicReports.Checked;
        ChkSelectAll.Enabled = !ChkDynamicReports.Checked;
    }

    private void rChkGroupWise_CheckedChanged(object sender, EventArgs e)
    {
        ChkOnly.Enabled = !rChkProductWise.Checked;
        if (ChkOnly.Checked) ChkOnly.Checked = !rChkProductWise.Checked && ChkOnly.Checked;
    }

    private void rChkSubGroupWise_CheckedChanged(object sender, EventArgs e)
    {
        rChkGroupWise_CheckedChanged(sender, e);
    }

    private void ChkOnly_Click(object sender, EventArgs e)
    {
        if (rChkProductWise.Checked) ChkOnly.Checked = false;
    }

    private void ChkOnly_CheckedChanged(object sender, EventArgs e)
    {
        if (rChkProductWise.Checked) ChkOnly.Checked = false;
    }

    private void rChkProductWise_CheckedChanged(object sender, EventArgs e)
    {
        rChkGroupWise_CheckedChanged(sender, e);
    }

    private void ChkSummary_CheckedChanged(object sender, EventArgs e)
    {
        if (!ChkSummary.Focused) return;
        ChkDate.Enabled = !ChkSummary.Checked;
        ChkOnly.Enabled = ChkSummary.Checked;
    }

    private void FrmControl()
    {
        ChkWithValue.Enabled = ChkWithValue.Checked = _IsValue;
        ChkIncludeVat.Enabled = _IsValue;
        if (!ChkIncludeVat.Enabled) ChkIncludeVat.Checked = false;
        ChkOnly.Enabled = !rChkProductWise.Checked;
        if (!ChkOnly.Enabled) ChkOnly.Checked = false;
        ChkDate.Text = SysDateType.Equals("D") ? @"Miti" : @"Date";
        ChkDate.Enabled = !ChkSummary.Checked;
        CmbDateType.SelectedIndex = 8;
    }

    #region --------------- Global Value ---------------

    private bool _IsValue { get; }
    private string RptName { get; } = string.Empty;
    private string RptDate { get; set; } = string.Empty;
    private string FromADDate { get; set; } = string.Empty;
    private string FromBSDate { get; set; } = string.Empty;
    private string ToADDate { get; set; } = string.Empty;
    private string ToBSDate { get; set; } = string.Empty;
    private string ProductGroupId { get; set; } = string.Empty;
    private string ProductSubGroupId { get; set; } = string.Empty;
    private string ProductId { get; set; } = string.Empty;
    private string GodownId { get; set; } = string.Empty;

    #endregion --------------- Global Value ---------------
}