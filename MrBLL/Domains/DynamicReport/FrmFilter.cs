using DevExpress.XtraEditors;
using DevExpress.XtraReports.Localization;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Reports.Interface;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MrBLL.Domains.DynamicReport;

public partial class FrmFilter : XtraForm
{
    // FILTER FORM CONTROL

    #region --------------- FILTER FORM ---------------

    public FrmFilter(string reportType, string gridType, string templateName = "")
    {
        InitializeComponent();
        _reportType = reportType;
        _gridType = gridType;
        _templateName = templateName;
        _getReport = new MrDAL.Reports.DyanamicReport.DynamicReport();
        var index = 8;
        if (reportStrings.Contains(_reportType))
        {
            index = 9;
        }
        ObjGlobal.BindDateType(CmbDateOption, index);
        ObjGlobal.BindPeriodicDate(MskFrom, MskToDate);
        CmbDateOption.SelectedIndex = reportStrings.Contains(_reportType) ? 9 : 8;
        CmbDateOption.Enabled = CmbDateOption.SelectedIndex is not 9;
        CmbDateOption_SelectionChangeCommitted(null, EventArgs.Empty);
    }

    private void CmbDateOption_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Space)
        {
            SendKeys.Send("{F4}");
        }
    }

    private void FrmFilter_Load(object sender, EventArgs e)
    {
        var dt = _getReport.ListTemplateType(_templateName, GetTemplateId, _gridType);
        if (dt.Rows.Count is 0)
        {
            return;
        }
        var index = 0;
        Grid.Rows.Add(dt.Rows.Count);
        foreach (DataRow drDetails in dt.Rows)
        {
            Grid.Rows[index].Cells["SNo"].Value = index + 1;
            Grid.Rows[index].Cells["TemplateId"].Value = drDetails["ID"].ToString();
            Grid.Rows[index].Cells["TemplateName"].Value = drDetails["Report_Name"].ToString();
            index++;
        }
    }

    private void CmbDateOption_SelectionChangeCommitted(object sender, EventArgs e)
    {
        ObjGlobal.LoadDate("R", CmbDateOption.SelectedIndex, MskFrom, MskToDate);
        if (CmbDateOption.SelectedIndex is 9)
        {
            MskFrom.Visible = !MskToDate.Visible;
            MskToDate.Location = MskFrom.Location;
            LblFromDate.Visible = MskFrom.Visible;
            LblToDate.Text = @"As On";
        }
        else
        {
            LblToDate.Visible = MskToDate.Visible = true;
            LblToDate.Text = @"Date From";
            LblFromDate.Text = @"To Date";
        }
    }

    private void TxtFromDate_Validating(object sender, CancelEventArgs e)
    {
        if (ActiveControl != null && MskFrom.MaskFull)
        {
            if (ObjGlobal.SysDateType.Equals("D"))
            {
                var result = MskFrom.Text.IsValidDateRange("D");
                var exits = MskFrom.Text.IsDateExits("D");
                if (exits)
                {
                    if (!result)
                    {
                        CustomMessageBox.Warning($"DATE MUST BE BETWEEN [{ObjGlobal.CfStartBsDate} AND {ObjGlobal.CfEndBsDate}]");
                        MskFrom.Focus();
                        return;
                    }
                    return;
                }
                CustomMessageBox.Warning($"ENTER DATE IS NOT A VALID DATE");
                MskFrom.Focus();
                return;
            }
            if (ObjGlobal.SysDateType.Equals("M"))
            {
                var result = MskFrom.Text.IsValidDateRange("M");
                var exits = MskFrom.Text.IsDateExits("M");
                if (exits)
                {
                    if (!result)
                    {
                        CustomMessageBox.Warning($"MITI MUST BE BETWEEN [{ObjGlobal.CfStartBsDate} AND {ObjGlobal.CfEndBsDate}]");
                        MskFrom.Focus();
                        return;
                    }
                    return;
                }
                CustomMessageBox.Warning($"ENTER MITI IS NOT A VALID MITI");
                MskFrom.Focus();
                return;
            }
        }
        else
        {
            CustomMessageBox.Warning($"DATE IS REQUIRED FOR REPORT GENERATE..!!");
            MskFrom.Focus();
            return;
        }
    }

    private void TxtToDate_Validating(object sender, CancelEventArgs e)
    {
        if (MskToDate.MaskFull)
        {
            if (ObjGlobal.SysDateType.Equals("D"))
            {
                var result = MskToDate.Text.IsValidDateRange("D");
                var exits = MskToDate.Text.IsDateExits("D");
                if (exits)
                {
                    if (!result)
                    {
                        CustomMessageBox.Warning($"DATE MUST BE BETWEEN [{ObjGlobal.CfStartBsDate} AND {ObjGlobal.CfEndBsDate}]");
                        MskFrom.Focus();
                        return;
                    }
                    return;
                }
                CustomMessageBox.Warning($"ENTER DATE IS NOT A VALID DATE");
                MskToDate.Focus();
                return;
            }
            if (ObjGlobal.SysDateType.Equals("M"))
            {
                var result = MskToDate.Text.IsValidDateRange("M");
                var exits = MskToDate.Text.IsDateExits("M");
                if (exits)
                {
                    if (!result)
                    {
                        CustomMessageBox.Warning($"MITI MUST BE BETWEEN [{ObjGlobal.CfStartBsDate} AND {ObjGlobal.CfEndBsDate}]");
                        MskToDate.Focus();
                        return;
                    }
                    return;
                }
                CustomMessageBox.Warning($"ENTER MITI IS NOT A VALID MITI");
                MskToDate.Focus();
                return;
            }
        }
        else
        {
            CustomMessageBox.Warning($"DATE IS REQUIRED FOR REPORT GENERATE..!!");
            MskToDate.Focus();
            return;
        }
    }

    private void FrmFilter_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            SendKeys.Send("{Tab}");
        }
        else if (e.KeyCode == Keys.Escape)
        {
            BtnCancel.PerformClick();
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void BtnOk_Click(object sender, EventArgs e)
    {
        if (MskFrom.Text == @"  /  /")
        {
            MskFrom.WarningMessage(@"FROM DATE CANNOT LEFT BLANK...!!");
            return;
        }

        if (MskToDate.Text == @"  /  /")
        {
            MskToDate.WarningMessage(@"TO DATE CANNOT LEFT BLANK...!!");
            return;
        }
        if (Grid.Rows.Count > 0 && Grid.CurrentRow != null)
        {
            GetTemplateId = Grid.Rows[Grid.CurrentRow.Index].Cells["TemplateId"].Value.GetInt();
            _templateName = Grid.Rows[Grid.CurrentRow.Index].Cells["TemplateName"].Value.GetString();
        }
        DialogResult = DialogResult.Yes;
        Close();
        return;
    }

    private void Grid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyData == Keys.Enter)
        {
        }
    }

    #endregion --------------- FILTER FORM ---------------

    // OBJECT FOR THIS FORM

    #region --------------- OBJECT FOR THIS FORM ---------------

    private string _reportType;

    private string[] reportStrings =
    [
        "POST DATE CHEQUE",
        "POST DATE CHEQUE - ALL"
    ];

    private string _gridType;
    public string _templateName;
    public int GetTemplateId;
    private IDynamicReport _getReport;

    #endregion --------------- OBJECT FOR THIS FORM ---------------
}