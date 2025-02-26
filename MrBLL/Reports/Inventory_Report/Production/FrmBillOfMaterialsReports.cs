using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using System;
using System.Windows.Forms;

namespace MrBLL.Reports.Inventory_Report.Production;

public partial class FrmBillOfMaterialsReports : MrForm
{
    public FrmBillOfMaterialsReports()
    {
        InitializeComponent();
        ObjGlobal.BindDateType(CmbDateType);
        ObjGlobal.BindPeriodicDate(MskFrom, MskToDate);
        CmbDateType.SelectedIndex = 8;
        ChkDate.Text = ObjGlobal.SysDateType.Equals("M") ? "Date" : "Miti";
    }

    private void FrmBillOfMaterialsReports_Load(object sender, EventArgs e)
    {
    }

    private void FrmBillOfMaterialsReports_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
        else if (e.KeyChar == (char)Keys.Escape)
        {
            BtnCancel.PerformClick();
        }
    }

    private void CmbDateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjGlobal.LoadDate("R", CmbDateType.SelectedIndex, MskFrom, MskToDate);
    }

    private void BtnShow_Click(object sender, EventArgs e)
    {
        var rptName = string.Empty;
        var rptDate = string.Empty;
        var productGroupId = string.Empty;
        var productSubGroupId = string.Empty;
        var productId = string.Empty;
        string fromAdDate = string.Empty,
            fromBsDate = string.Empty,
            toAdDate = string.Empty,
            toBsDate = string.Empty;

        fromAdDate = ObjGlobal.SysDateType.Equals("M") ? fromAdDate.GetEnglishDate(MskFrom.Text) : MskFrom.Text;
        fromBsDate = ObjGlobal.SysDateType.Equals("M") ? MskFrom.Text : fromBsDate.GetNepaliDate(MskFrom.Text);
        toAdDate = ObjGlobal.SysDateType.Equals("M") ? toAdDate.GetEnglishDate(MskToDate.Text) : MskToDate.Text;
        toBsDate = ObjGlobal.SysDateType == "M" ? MskToDate.Text : toBsDate.GetNepaliDate(MskToDate.Text);
        var branchId = ObjGlobal.SysBranchId.GetIntString();
        var companyUnitId = ObjGlobal.SysCompanyUnitId.GetIntString();
        var fiscalYearId = ObjGlobal.SysFiscalYearId.GetIntString();

        if (ObjGlobal.SysDateType == "M" && ChkDate.Checked == false ||
            ObjGlobal.SysDateType == "D" && ChkDate.Checked)
            rptDate = $"FROM DATE {fromBsDate} TO {toBsDate}";
        else
            rptDate = $"FROM DATE {fromAdDate.GetDateTime()} TO {toAdDate.GetDateTime()}";

        var rptMode =
            rChkSubGroupWise.Checked ? "SUBGROUP WISE"
            : rChkGroupWise.Checked ? "GROUP WISE" : "PRODUCT WISE";
        var display = new DisplayInventoryReports
        {
            Text = rptName + @" REPORTS",
            RptType = "NORMAL",
            RptName = "BILL OF MATERIALS",
            RptDate = rptDate,
            RptMode = rptMode,
            FromBsDate = fromBsDate,
            ToBsDate = toBsDate,
            FromAdDate = fromAdDate,
            ToAdDate = toAdDate,
            ProductSubGroupId = productSubGroupId,
            ProductGroupId = productGroupId,
            ProductId = productId,
            BranchId = branchId,
            FiscalYearId = fiscalYearId,
            CompanyUnitId = companyUnitId,
            IsDate = ChkDate.Checked,
            IsSummary = ChkSummary.Checked,
            CostRatio = ChkCostRatio.Checked
        };
        display.Show();
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void ChkSummary_CheckedChanged(object sender, EventArgs e)
    {
    }

    // OBJECT FOR THIS FORM
}