using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MrBLL.Reports.Register_Report.Analysis_Report;

public partial class FrmTopNAnalysis : MrForm
{
    //OBJECT

    #region --------------- OBJECT FOR THIS FORM ---------------

    private readonly string _formType;

    #endregion --------------- OBJECT FOR THIS FORM ---------------

    public FrmTopNAnalysis(string type)
    {
        InitializeComponent();
        _formType = type;
        ObjGlobal.BindDateType(CmbDateType);
        ObjGlobal.BindPeriodicDate(MskFrom, MskToDate);
    }

    private void FrmTopNAnalysis_Load(object sender, EventArgs e)
    {
        Text = _formType switch
        {
            "V" => $"TOP {TxtTopNumber.Text} VENDOR LIST",
            "P" => $"TOP {TxtTopNumber.Text} PRODUCT LIST",
            _ => $"TOP {TxtTopNumber.Text} CUSTOMER LIST"
        };
        rChkQuantity.Enabled = rChkQuantity.Visible = _formType.Equals("P");
        CmbDateType.Focus();
    }

    private void FrmTopNAnalysis_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
            SendKeys.Send("{TAB}");
        else if (e.KeyChar is (char)Keys.Escape)
            if (MessageBox.Show(@"ARE YOU SURE WANT TO CLOSE FORM..!!", ObjGlobal.Caption, MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                Close();
    }

    private void CmbDateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjGlobal.LoadDate("R", CmbDateType.SelectedIndex, MskFrom, MskToDate);
    }

    private void MskFrom_Validating(object sender, CancelEventArgs e)
    {
    }

    private void MskToDate_Validating(object sender, CancelEventArgs e)
    {
    }

    private void BtnShow_Click(object sender, EventArgs e)
    {
        var _rptType = "NORMAL";
        var _rptName = "TOP N ANALYSIS";
        var _fromAdDate = ObjGlobal.SysDateType == "M"
            ? ObjGlobal.ReturnEnglishDate(MskFrom.Text)
            : MskFrom.Text;
        var _fromBsDate = ObjGlobal.SysDateType == "M" ? MskFrom.Text : ObjGlobal.ReturnNepaliDate(MskFrom.Text);
        var _toAdDate = ObjGlobal.SysDateType == "M" ? ObjGlobal.ReturnEnglishDate(MskToDate.Text) : MskToDate.Text;
        var _toBsDate = ObjGlobal.SysDateType == "M" ? MskToDate.Text : ObjGlobal.ReturnNepaliDate(MskToDate.Text);
        var _rptStartDate = ObjGlobal.SysDateType == "M" ? _fromBsDate : _fromAdDate;
        var _rptEndDate = ObjGlobal.SysDateType == "M" ? _toBsDate : _toAdDate;
        var _rptDate = ObjGlobal.SysDateType == "M"
            ? $"FROM DATE {_fromBsDate} TO {_toBsDate}"
            : $"FROM DATE {_fromAdDate.GetDateTime()} TO {_toAdDate.GetDateTime()}";
        var display = new DisplayRegisterReports
        {
            RptType = _rptType.ToUpper(),
            RptName = _rptName.ToUpper(),
            RptDate = _rptDate,
            FromAdDate = _fromAdDate,
            ToAdDate = _toAdDate,
            Module = _formType,
            RptMode = TxtTopNumber.Text,
            BranchId = ObjGlobal.SysBranchId.ToString(),
            FiscalYearId = ObjGlobal.SysFiscalYearId.ToString(),
            CompanyUnitId = ObjGlobal.SysCompanyUnitId.ToString()
        };
        display.Show();
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
    }
}