using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Master;
using MrDAL.Master.Interface;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Reports.Register_Report.Vat_Report;

public partial class FrmMaterializeView : MrForm
{
    private string FromADDate;
    private string FromBSDate;
    private IMasterSetup GetMaster = new ClsMasterSetup();
    private string Query = string.Empty;
    private string ReportMode = string.Empty;
    private string RptDate;
    private string RptName;
    private string RptType;
    private string ToADDate;
    private string ToBSDate;
    private int VatTermId;

    public FrmMaterializeView()
    {
        InitializeComponent();
        BackColor = ObjGlobal.FrmBackColor();
    }

    public sealed override Color BackColor
    {
        get => base.BackColor;
        set => base.BackColor = value;
    }

    private void MaterializeView_Activated(object sender, EventArgs e)
    {
    }

    private void MaterializeView_Load(object sender, EventArgs e)
    {
        ObjGlobal.BindDateType(CmbDateType);
        ObjGlobal.BindPeriodicDate(MskFrom, MskToDate);
        CmbDateType.SelectedIndex = 8;
        CmbDateType.Focus();
    }

    private void MaterializeView_KeyPress(object sender, KeyPressEventArgs e)
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
                BtnCancel.PerformClick();
                break;
            }
        }
    }

    private void msk_FromDate_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskFrom, 'E');
    }

    private void msk_FromDate_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void msk_FromDate_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskFrom, 'L');
    }

    private void msk_FromDate_Validated(object sender, EventArgs e)
    {
    }

    private void msk_ToDate_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskToDate, 'E');
    }

    private void msk_ToDate_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void msk_ToDate_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskToDate, 'L');
    }

    private void msk_ToDate_Validated(object sender, EventArgs e)
    {
    }

    private void btn_Show_Click(object sender, EventArgs e)
    {
        if (!MskFrom.MaskCompleted)
        {
            MessageBox.Show(@"FROM DATE CAN'T BE BLANK..!!", ObjGlobal.Caption);
            MskFrom.Focus();
            return;
        }

        if (!MskToDate.MaskCompleted)
        {
            MessageBox.Show(@"FROM DATE CAN'T BE BLANK..!!", ObjGlobal.Caption);
            MskToDate.Focus();
            return;
        }

        FromADDate = ObjGlobal.SysDateType == "M"
            ? ObjGlobal.ReturnEnglishDate(MskFrom.Text)
            : MskFrom.Text;
        FromBSDate = ObjGlobal.SysDateType == "M" ? MskFrom.Text : ObjGlobal.ReturnNepaliDate(MskFrom.Text);
        ToADDate = ObjGlobal.SysDateType == "M" ? ObjGlobal.ReturnEnglishDate(MskToDate.Text) : MskToDate.Text;
        ToBSDate = ObjGlobal.SysDateType == "M" ? MskToDate.Text : ObjGlobal.ReturnNepaliDate(MskToDate.Text);
        RptType = "Normal";
        RptDate = ObjGlobal.SysDateType == "M"
            ? $"From Date {FromBSDate} To {ToBSDate}"
            : $"From Date {Convert.ToDateTime(FromADDate).ToShortDateString()} To {Convert.ToDateTime(ToADDate).ToShortDateString()}";
        var reports = new DisplayRegisterReports
        {
            RptName = "MATERIALIZE VIEW",
            RptDate = RptDate,
            RptType = RptType,
            AccountType = "SB",
            FromAdDate = FromADDate,
            FromBsDate = FromBSDate,
            ToAdDate = ToADDate
        };
        reports.Show();
        reports.BringToFront();
    }

    private void btn_Cancel_Click(object sender, EventArgs e)
    {
        var dialogResult = MessageBox.Show(@"Are you sure want to Close Form..??", ObjGlobal.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (dialogResult == DialogResult.Yes)
        {
            Close();
        }
    }

    private void CmbDateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjGlobal.LoadDate("R", CmbDateType.SelectedIndex, MskFrom, MskToDate);
    }

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
            SendKeys.Send("{TAB}");
    }

    private void CmbDateType_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 32) SendKeys.Send("{F4}");
    }
}