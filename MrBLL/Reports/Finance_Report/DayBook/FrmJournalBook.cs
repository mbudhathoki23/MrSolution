using MrDAL.Control.ControlsEx;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Reports.Finance_Report.DayBook;

public partial class FrmJournalBook : MrForm
{
    private string FromADDate;
    private string FromBSDate;
    private ClsMasterForm getForm;
    private string RptDate;
    private string RptName;
    private string RptType;
    private string ToADDate;
    private string ToBSDate;
    private string TransStation;

    public FrmJournalBook()
    {
        InitializeComponent();
        BackColor = ObjGlobal.FrmBackColor();
        getForm = new ClsMasterForm(this, BtnCancel);
        ObjGlobal.BindDateType(CmbDateType);
        ObjGlobal.BindPeriodicDate(MskFrom, MskToDate);
    }

    public sealed override Color BackColor
    {
        get => base.BackColor;
        set => base.BackColor = value;
    }

    private void FrmJournalBook_Load(object sender, EventArgs e)
    {
        CmbDateType.SelectedIndex = 8;
        CmbDateType.Focus();
    }

    private void FrmJournalBook_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape)
        {
            var dialogResult = MessageBox.Show(@"ARE YOU SURE WANT TO CLOSE FORM..!!", ObjGlobal.Caption,
                MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) Close();
        }
        else if (e.KeyChar == (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void CmbDateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjGlobal.LoadDate("R", CmbDateType.SelectedIndex, MskFrom, MskToDate);
    }

    private void BtnShow_Click(object sender, EventArgs e)
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
        TransStation = string.Empty;

        RptType = ChkIsTFormat.Checked ? "T Format" : "Normal";
        RptDate = ObjGlobal.SysDateType == "M" && !ChkIsDate.Checked ||
                  ObjGlobal.SysDateType == "D" && ChkIsDate.Checked
            ? $"From Date {FromBSDate} To {ToBSDate}"
            : $"From Date {Convert.ToDateTime(FromADDate).ToShortDateString()} To {Convert.ToDateTime(ToADDate).ToShortDateString()}";
        var dr = new DisplayFinanceReports
        {
            RptName = "JOURNAL BOOK",
            RptDate = RptDate,
            RptType = RptType,
            AccountType = TransStation,
            FromAdDate = FromADDate,
            FromBsDate = FromBSDate,
            ToAdDate = ToADDate,
            IsTFormat = ChkIsTFormat.Checked,
            ReportOption = TxtFilterValue.Text
        };
        dr.Show();
    }
}