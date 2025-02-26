using MrBLL.Utility.Common;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Utility.PickList;
using System;
using System.Windows.Forms;

namespace MrBLL.Reports.Inventory_Report.Analysis;

public partial class FrmProfitability : MrForm
{
    public FrmProfitability()
    {
        InitializeComponent();
        ObjGlobal.BindDateType(CmbDateType);
        ObjGlobal.BindPeriodicDate(MskFrom, MskToDate);
        CmbDateType.SelectedIndex = 8;
    }

    private void FrmProfitability_Load(object sender, EventArgs e)
    {
        rChkProduct_CheckedChanged(sender, EventArgs.Empty);
        CmbDateType.Focus();
    }

    private void FrmProfitability_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void MskFromDate_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskFrom, 'E');
    }

    private void MskFromDate_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void MskFromDate_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskFrom, 'L');
    }

    private void MskFromDate_Validated(object sender, EventArgs e)
    {
    }

    private void MskToDate_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskToDate, 'E');
    }

    private void MskToDate_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void MskToDate_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskToDate, 'L');
    }

    private void MskToDate_Validated(object sender, EventArgs e)
    {
    }

    private void BtnShow_Click(object sender, EventArgs e)
    {
        FromADDate = ObjGlobal.SysDateType.Equals("M") ? ObjGlobal.ReturnEnglishDate(MskFrom.Text) : MskFrom.Text;
        FromBSDate = ObjGlobal.SysDateType.Equals("M") ? MskFrom.Text : ObjGlobal.ReturnNepaliDate(MskFrom.Text);
        ToADDate = ObjGlobal.SysDateType.Equals("M") ? ObjGlobal.ReturnEnglishDate(MskToDate.Text) : MskToDate.Text;
        ToBSDate = ObjGlobal.SysDateType == "M" ? MskToDate.Text : ObjGlobal.ReturnNepaliDate(MskToDate.Text);

        var branchId = ObjGlobal.SysBranchId.GetIntString();
        var companyUnitId = ObjGlobal.SysCompanyUnitId.GetIntString();
        var fiscalYearId = ObjGlobal.SysFiscalYearId.GetIntString();

        ProductGroupId = string.Empty;
        ProductSubGroupId = string.Empty;
        ProductId = string.Empty;
        GodownId = string.Empty;

        if (ChkSelectAll.Checked == false)
        {
            if (rChkProductGroup.Checked || rChkProductSubGroup.Checked)
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
                    CustomMessageBox.Warning(@"PRODUCT GROUP NOT SELECTED ..!!");
                    return;
                }

                if (rChkProductSubGroup.Checked && !string.IsNullOrEmpty(ProductGroupId))
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
                        CustomMessageBox.Warning(@"PRODUCT SUBGROUP NOT SELECTED ..!!");
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
                    CustomMessageBox.Warning(@"PRODUCT NOT SELECTED FROM THIS GROUP..!!");
                    return;
                }
            }

            if (rChkProduct.Checked)
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
                if (string.IsNullOrEmpty(ProductId))
                {
                    CustomMessageBox.Warning(@"PRODUCT IS NOT SELECTED ..!!");
                    return;
                }
            }
        }

        RptDate = ObjGlobal.SysDateType == "M" ? $"From Date {FromBSDate} To {ToBSDate}" : $"From Date {FromADDate.GetDateTime()} To {ToADDate.GetDateTime()}";
        var rptMode = rChkProductGroup.Checked switch
        {
            true => "GROUP WISE",
            false when rChkCustomer.Checked => "CUSTOMER WISE",
            false when rChkProductSubGroup.Checked => "SUB GROUP WISE",
            false when rChkBill.Checked => "BILL WISE",
            _ => "PRODUCT WISE"
        };
        var sortOn = RbtnDescrption.Checked switch
        {
            false when RbtnBarcode.Checked => "SHORTNAME",
            false when RbtnQuantity.Checked => "QTY",
            false when RbtnMargin.Checked => "MARGIN",
            _ => "DESCRIPTION"
        };

        var display = new DisplayInventoryReports
        {
            Text = RptName + @" REPORTS",
            RptType = "NORMAL",
            RptName = "PROFITABILITY",
            RptDate = RptDate,
            RptMode = rptMode,
            IsSummary = !ChkIncludeProduct.Checked,
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
            SortOn = sortOn,
            RePostValue = ChkRepostValue.Checked
        };
        display.Show();
        ProductGroupId = ProductSubGroupId = ProductId = string.Empty;
    }

    private void rChkProduct_CheckedChanged(object sender, EventArgs e)
    {
        ChkIncludeProduct.Enabled = !rChkProduct.Checked;
    }

    private void CmbSysDateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjGlobal.LoadDate("R", CmbDateType.SelectedIndex, MskFrom, MskToDate);
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    // OBJECT FOR THIS FORM

    #region --------------- OBJECT ---------------

    private string ProductId = string.Empty;
    private string GodownId = string.Empty;
    private string ProductGroupId = string.Empty;
    private string ProductSubGroupId = string.Empty;
    private string FromADDate;
    private string FromBSDate;
    private string InvoiceNo = string.Empty;
    private string Query = string.Empty;
    private string RptDate;
    private string RptName;
    private string RptType;
    private string ToADDate;
    private string ToBSDate;

    #endregion --------------- OBJECT ---------------
}