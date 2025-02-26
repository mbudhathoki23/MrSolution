using MrBLL.Utility.Common;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Utility.PickList;
using System;
using System.Windows.Forms;

namespace MrBLL.Reports.Inventory_Report.StockLedger;

public partial class FrmStockValuation : MrForm
{
    //STOCK VALUE FORM

    #region --------------- STOCK VALUE ---------------

    public FrmStockValuation()
    {
        InitializeComponent();
        ObjGlobal.BindDateType(CmbDateType, 9);
        ObjGlobal.BindPeriodicDate(MskFrom, MskToDate);
        CmbDateType.SelectedIndex = 9;
        CmbDateType.Enabled = false;
    }

    private void FrmStockValuation_Load(object sender, EventArgs e)
    {
        ChkDate.Text = ObjGlobal.SysDateType == "M" ? "Date" : "Miti";
        rChkProduct_CheckedChanged(sender, e);
        MskToDate.Focus();
    }

    private void FrmStockValuation_KeyPress(object sender, KeyPressEventArgs e)
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

    #endregion --------------- STOCK VALUE ---------------

    // EVENTS OF THIS FORM

    #region --------------- EVENT CLICK OF THE FORM ---------------

    private void CmbDateType_Enter(object sender, EventArgs e)
    {
        ObjGlobal.ComboBoxBackColor(CmbDateType, 'E');
    }

    private void CmbDateType_Leave(object sender, EventArgs e)
    {
        ObjGlobal.ComboBoxBackColor(CmbDateType, 'L');
    }

    private void CmbDateType_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void CmbDateType_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 32)
        {
            SendKeys.Send("{F4}");
        }
    }

    private void CmbDateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjGlobal.LoadDate("R", CmbDateType.SelectedIndex, MskFrom, MskToDate);
    }

    private void MskFrom_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskFrom, 'E');
    }

    private void MskFrom_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void MskFrom_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskFrom, 'L');
    }

    private void MskFrom_Validated(object sender, EventArgs e)
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
        FromADDate = ObjGlobal.SysDateType is "M" ? MskFrom.Text.GetEnglishDate() : MskFrom.Text;
        FromBSDate = ObjGlobal.SysDateType is "M" ? MskFrom.Text : MskFrom.Text.GetNepaliDate();

        ToADDate = ObjGlobal.SysDateType is "M" ? MskToDate.Text.GetEnglishDate() : MskToDate.Text; ;
        ToBSDate = ObjGlobal.SysDateType is "M" ? MskToDate.Text : MskToDate.Text.GetNepaliDate(); ;

        var branchId = ObjGlobal.SysBranchId.GetIntString();
        var companyUnitId = ObjGlobal.SysCompanyUnitId.GetIntString();
        var fiscalYearId = ObjGlobal.SysFiscalYearId.GetIntString();

        ProductGroupId = string.Empty;
        ProductSubGroupId = string.Empty;
        ProductId = string.Empty;
        GodownId = string.Empty;

        if (ChkSelectAll.Checked == false)
        {
            if (rChkGroup.Checked || rChkSubGroup.Checked)
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
                    MessageBox.Show(@"PRODUCT GROUP NOT SELECTED ..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (rChkSubGroup.Checked && !string.IsNullOrEmpty(ProductGroupId))
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
                        MessageBox.Show(@"PRODUCT SUBGROUP NOT SELECTED ..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show(@"PRODUCT NOT SELECTED FROM THIS GROUP..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show(@"PRODUCT NOT SELECTED..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        var result = ObjGlobal.SysDateType == "M" && !ChkDate.Checked || ObjGlobal.SysDateType == "D" && ChkDate.Checked;
        RptDate = result switch
        {
            true => $"FROM DATE {FromBSDate} TO {ToBSDate}",
            _ => $"FROM DATE {FromADDate.GetDateTime()} TO {ToADDate.GetDateTime()}"
        };
        var rptMode = rChkGroup.Checked switch
        {
            true => "GROUP WISE",
            false when rChkGodown.Checked => "GODOWN WISE",
            false when rChkSubGroup.Checked => "SUB GROUP WISE",
            _ => "PRODUCT WISE"
        };

        var sortOn = rChkDescription.Checked switch
        {
            false when rChkShortName.Checked => "SHORTNAME",
            false when rChkQuantity.Checked => "QTY",
            false when rChkAmount.Checked => "AMOUNT",
            _ => "DESCRIPTION"
        };

        var display = new DisplayInventoryReports
        {
            Text = RptName + @" REPORTS",
            RptType = "NORMAL",
            RptName = "STOCK VALUATION",
            RptDate = RptDate,
            RptMode = rptMode,
            IsSummary = true,
            FromBsDate = FromBSDate,
            ToBsDate = ToBSDate,
            FromAdDate = FromADDate,
            ToAdDate = ToADDate,
            ProductSubGroupId = ProductSubGroupId,
            ProductGroupId = ProductGroupId,
            ProductId = ProductId,
            IsShortName = ChkShortName.Checked,
            SortOn = sortOn,
            BranchId = branchId,
            FiscalYearId = fiscalYearId,
            CompanyUnitId = companyUnitId,
            IsDate = ChkDate.Checked,
            IncludeVat = ChkIncludeVat.Checked,
            NegativeStock = rChkInclude.Checked,
            ExcludeNegative = rChkExclude.Checked,
            NegativeStockOnly = rChkOnly.Checked,
            IncludeAltQty = ChkIncludeAltQty.Checked,
            RePostValue = ChkRepostValue.Checked
        };
        display.Show();
        display.BringToFront();
        ProductGroupId = ProductSubGroupId = ProductId = string.Empty;
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void rChkProduct_CheckedChanged(object sender, EventArgs e)
    {
        if (rChkProduct.Checked)
        {
            ChkIncludeProduct.Enabled = false;
            ChkIncludeProduct.Checked = false;
        }
        else
        {
            ChkIncludeProduct.Enabled = true;
            ChkIncludeProduct.Checked = true;
        }
    }

    #endregion --------------- EVENT CLICK OF THE FORM ---------------

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