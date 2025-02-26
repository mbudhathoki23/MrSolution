using MrBLL.Utility.Common;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Utility.PickList;
using System;
using System.Windows.Forms;

namespace MrBLL.Reports.Inventory_Report.ListOfMaster;

public partial class FrmProductList : MrForm
{
    // PRODUCT LIST

    #region --------------- PRODUCT LIST ---------------
    public FrmProductList()
    {
        InitializeComponent();
    }
    public FrmProductList(string description)
    {
        InitializeComponent();
    }

    private void FrmProductList_Load(object sender, EventArgs e)
    {
        rChkProduct.Focus();
    }

    private void FrmProductList_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }

        if (e.KeyChar == (char)Keys.Escape)
        {
            if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
            {
                Close();
            }
        }
    }

    private void BtnShow_Click(object sender, EventArgs e)
    {
        var branchId = ObjGlobal.SysBranchId.ToString();
        var companyUnitId = ObjGlobal.SysCompanyUnitId.ToString();
        var fiscalYearId = ObjGlobal.SysFiscalYearId.ToString();

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
                _groupId = ClsTagList.PlValue1;

                if (rChkProductSubGroup.Checked && !string.IsNullOrEmpty(_groupId))
                {
                    var frm1 = new FrmTagList
                    {
                        ReportDesc = "PRODUCTSUBGROUP",
                        BranchId = branchId,
                        CompanyUnitId = companyUnitId,
                        FiscalYearId = fiscalYearId,
                        GroupId = _groupId
                    };
                    frm1.ShowDialog();
                }

                var frm2 = new FrmTagList
                {
                    BranchId = branchId,
                    CompanyUnitId = companyUnitId,
                    FiscalYearId = fiscalYearId,
                    GroupId = _groupId,
                    SubGroupId = _subGroupId
                };
                frm2.ShowDialog();
                _productId = ClsTagList.PlValue1;
                if (string.IsNullOrEmpty(_productId))
                {
                    MessageBox.Show(@"PRODUCT NOT SELECTED..!!", ObjGlobal.Caption);
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
                    GroupId = _groupId,
                    SubGroupId = _subGroupId
                };
                frm2.ShowDialog();
                _productId = ClsTagList.PlValue1;
                if (string.IsNullOrEmpty(_productId))
                {
                    MessageBox.Show(@"PRODUCT NOT SELECTED..!!", ObjGlobal.Caption);
                    return;
                }
            }
        }

        var rptName = rChkProductGroup.Checked
            ? "PRODUCT GROUP/PRODUCT"
            : rChkProductSubGroup.Checked
                ? "PRODUCT SUB GROUP/PRODUCT"
                : rChkProductTaxType.Checked
                    ? "TAXABLE/NON TAXABLE"
                    : rChkProductType.Checked
                        ? "PRODUCT TYPE"
                        : "PRODUCT";
        var dr = new DisplayInventoryReports
        {
            Text = rptName + @" Report",
            RptType = @"LIST_OF_MASTER",
            RptName = rptName,
            ProductSubGroupId = _subGroupId,
            ProductGroupId = _groupId,
            ProductId = _productId,
            BranchId = branchId,
            FiscalYearId = fiscalYearId,
            CompanyUnitId = companyUnitId,
            IsShortName = ChkShortName.Checked,
            IncludeLedgerInfo = ChkLedgerInfo.Checked,
            IncludeProduct = ChkIncludeProduct.Checked,
            IncludeReOrderInfo = ChkReOrderInfo.Checked,
            IncludeBonusFreeQty = ChkBonusFreeQty.Checked,
            IncludePriceInfo = ChkPriceInfo.Checked
        };
        dr.Show();
        _groupId = _subGroupId = _productId = string.Empty;
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
        {
            Close();
        }
    }

    private void ChkIncludeProduct_CheckedChanged(object sender, EventArgs e)
    {
        if (rChkProduct.Checked)
        {
            ChkIncludeProduct.Checked = false;
        }
    }

    private void ChkLedgerInfo_CheckedChanged(object sender, EventArgs e)
    {
    }

    private void rChkProduct_CheckedChanged(object sender, EventArgs e)
    {
        ChkIncludeProduct_CheckedChanged(sender, e);
    }

    #endregion --------------- PRODUCT LIST ---------------

    // OBJECT FOR THIS CLASS

    #region --------------- OBJECT OF THIS FORM ---------------

    private string _description = string.Empty;
    private string _godownId = string.Empty;
    private string _groupId = string.Empty;
    private string _productId = string.Empty;
    private string _subGroupId = string.Empty;
    private string _query = string.Empty;

    #endregion --------------- OBJECT OF THIS FORM ---------------
}