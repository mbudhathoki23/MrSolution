using MrBLL.Utility.Common;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Utility.PickList;
using System;
using System.Windows.Forms;

namespace MrBLL.Reports.Inventory_Report.StockLedger;

public partial class FrmProductOpeningReport : MrForm
{
    // Product Opening
    public FrmProductOpeningReport()
    {
        InitializeComponent();
    }

    private void FrmProductOpening_Load(object sender, EventArgs e)
    {
        rChkNormal.Focus();
    }

    private void FrmProductOpening_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) SendKeys.Send("{TAB}");
        if (e.KeyChar == (char)Keys.Escape)
            if (MessageBox.Show(@"Are you sure want to Close Form..??", ObjGlobal.Caption, MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                Close();
        if (e.KeyChar == (char)Keys.F7) SelectBranchList();
        if (e.KeyChar == (char)Keys.F8) SelectCompanyUnitList();
        if (e.KeyChar == (char)Keys.F9) SelectFiscalYearList();
    }

    private void RbtnNormal_CheckedChanged(object sender, EventArgs e)
    {
        rChkProductDetails.Enabled = !rChkNormal.Checked;
    }

    private void BtnShow_Click(object sender, EventArgs e)
    {
        var filterValue = rChkVoucherDetails.Checked switch
        {
            true => "VOUCHER WISE",
            _ => rChkOpeningOnly.Checked ? "OPENING ONLY" : "NORMAL"
        };

        if (!ChkSelectAll.Checked)
        {
            if (rChkGroupWise.Checked || rChkSubGroupWise.Checked)
            {
                var frm = new FrmTagList
                {
                    ReportDesc = "PRODUCTGROUP",
                    BranchId = _branchId,
                    CompanyUnitId = _companyUnitId,
                    FiscalYearId = _fiscalYearId
                };
                frm.ShowDialog();
                _productGroupId = ClsTagList.PlValue1;
                if (rChkSubGroupWise.Checked && !string.IsNullOrEmpty(_productGroupId))
                {
                    var frm1 = new FrmTagList
                    {
                        ReportDesc = "PRODUCTSUBGROUP",
                        BranchId = _branchId,
                        CompanyUnitId = _companyUnitId,
                        FiscalYearId = _fiscalYearId,
                        GroupId = _productGroupId
                    };
                    frm1.ShowDialog();
                }

                using var _frm2 = new FrmTagList
                {
                    ReportDesc = "PRODUCT",
                    BranchId = _branchId,
                    CompanyUnitId = _companyUnitId,
                    FiscalYearId = _fiscalYearId,
                    GroupId = _productGroupId,
                    SubGroupId = _productSubGroupId
                };
                _frm2.ShowDialog();
                _productId = ClsTagList.PlValue1;
                if (string.IsNullOrEmpty(_productId))
                {
                    MessageBox.Show(@"PRODUCT LIST NOT AVAILABLE IN THIS GROUP..!!", ObjGlobal.Caption,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        var rptMode = string.Empty;
        if (rChkGroupWise.Checked)
        {
            rptMode = "GROUP WISE";
        }
        else if (rChkSubGroupWise.Checked)
        {
            rptMode = "SUB GROUP WISE";
        }
        else if (rChkGodownWise.Checked)
        {
            rptMode = "GODOWN WISE";
        }
        else
        {
            rptMode = "PRODUCT WISE";
        }
        var objDisplay = new DisplayInventoryReports
        {
            Text = @"PRODUCT OPENING REPORT",
            RptType = "NORMAL",
            RptName = "PRODUCT OPENING",
            RptMode = rptMode,
            FilterFor = filterValue,
            ToAdDate = ObjGlobal.CfStartAdDate.GetSystemDate(),
            ProductSubGroupId = _productSubGroupId,
            ProductGroupId = _productGroupId,
            ProductId = _productId,
            BranchId = _branchId,
            FiscalYearId = _fiscalYearId,
            CompanyUnitId = _companyUnitId
        };
        objDisplay.Show(this);
        _productGroupId = _productSubGroupId = _productId = string.Empty;
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    // OBJECT FOR THIS FORM
    internal void SelectBranchList()
    {
    }

    internal void SelectCompanyUnitList()
    {
    }

    internal void SelectFiscalYearList()
    {
    }

    #region --------------- Global Value ---------------

    private string _productGroupId = string.Empty;
    private string _productSubGroupId = string.Empty;
    private string _productId = string.Empty;
    private string _branchId = ObjGlobal.SysBranchId.ToString();
    private string _companyUnitId = ObjGlobal.SysCompanyUnitId.ToString();
    private string _fiscalYearId = ObjGlobal.SysFiscalYearId.ToString();

    #endregion --------------- Global Value ---------------
}