using MrDAL.Control.ControlsEx;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Reports.Finance_Report.DayBook;

public partial class FrmDayBook : MrForm
{
    public FrmDayBook()
    {
        InitializeComponent();
        _getForm = new ClsMasterForm(this, btnCancel);
        BackColor = ObjGlobal.FrmBackColor();
        ObjGlobal.BindDateType(CmbDateType);
        ObjGlobal.BindPeriodicDate(MskFrom, MskToDate);
    }

    public sealed override Color BackColor
    {
        get => base.BackColor;
        set => base.BackColor = value;
    }

    private void FrmDayBook_Load(object sender, EventArgs e)
    {
        BindChkListItem();
        ChkIsDate.Text = ObjGlobal.SysDateType == "M" ? "Date" : "Miti";
        CmbDateType.SelectedIndex = 8;
        CmbDateType.Focus();
    }

    private void FrmDayBook_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Escape) return;
        if (MessageBox.Show(@"ARE YOU SURE WANT TO CLOSE FORM..!!", ObjGlobal.Caption, MessageBoxButtons.YesNo) ==
            DialogResult.Yes) Close();
    }

    private void FrmDayBook_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) SendKeys.Send("{TAB}");
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

        _fromAdDate = ObjGlobal.SysDateType == "M"
            ? ObjGlobal.ReturnEnglishDate(MskFrom.Text)
            : MskFrom.Text;
        _fromBsDate = ObjGlobal.SysDateType == "M" ? MskFrom.Text : ObjGlobal.ReturnNepaliDate(MskFrom.Text);
        _toAdDate = ObjGlobal.SysDateType == "M" ? ObjGlobal.ReturnEnglishDate(MskToDate.Text) : MskToDate.Text;
        _toBsDate = ObjGlobal.SysDateType == "M" ? MskToDate.Text : ObjGlobal.ReturnNepaliDate(MskToDate.Text);
        _transStation = string.Empty;

        for (var i = 0; i < ChkListDaybook.Items.Count; i++)
        {
            ChkListDaybook.SelectedIndex = i;
            if (ChkListDaybook.GetItemChecked(ChkListDaybook.SelectedIndex) != true) continue;
            _transStation = _transStation != string.Empty
                ? $"{_transStation},{ChkListDaybook.SelectedValue}"
                : ChkListDaybook.SelectedValue.ToString();
        }

        _rptType = ChkIsTFormat.Checked ? "T Format" : "Normal";
        _rptDate = ObjGlobal.SysDateType == "M" && !ChkIsDate.Checked ||
                   ObjGlobal.SysDateType == "D" && ChkIsDate.Checked
            ? $"From Date {_fromBsDate} To {_toBsDate}"
            : $"From Date {Convert.ToDateTime(_fromAdDate).ToShortDateString()} To {Convert.ToDateTime(_toAdDate).ToShortDateString()}";
        var dr = new DisplayFinanceReports
        {
            RptType = ChkIsTFormat.Checked ? "T FORMAT" : "NORMAL",
            RptName = "DAY BOOK",
            RptDate = _rptDate,
            AccountType = _transStation,
            FromAdDate = _fromAdDate,
            FromBsDate = _fromBsDate,
            ToAdDate = _toAdDate,
            IsDetails = ChkSummary.Checked,
            IsDate = ChkIsDate.Checked,
            IsTFormat = ChkIsTFormat.Checked,
            ReportOption = TxtFilterValue.Text,
            IsCombineSales = ChkCombineSales.Checked
        };
        dr.Show(this);
    }

    private void cmbSysDateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjGlobal.LoadDate("R", CmbDateType.SelectedIndex, MskFrom, MskToDate);
    }

    private void ckbSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        if (ckbSelectAll.Checked) TagFinance();
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
    }

    private void ChkListDayBook_ItemCheck(object sender, ItemCheckEventArgs e)
    {
        if (!ChkListDaybook.Focused) return;
        if (ChkListDaybook.SelectedItem is not ValueModel<string, string> model) return;
        if (model.Item2 == "SB")
        {
            if (e.NewValue is CheckState.Checked)
            {
                ChkCombineSales.Enabled = true;
            }
            else if (e.NewValue is CheckState.Unchecked)
            {
                ChkCombineSales.Checked = false;
                ChkCombineSales.Enabled = false;
            }
        }
    }

    private void ChkCombineSales_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkListDaybook.Items.Count <= 0) return;
        for (var i = 0; i < ChkListDaybook.Items.Count; i++)
        {
            var item = ChkListDaybook.Items[i];
            if (item is not ValueModel<string, string> model ||
                ChkListDaybook.GetItemCheckState(i) != CheckState.Checked) continue;
            if (model.Item2 is "SB") ChkListDaybook.SetItemChecked(i, !ChkCombineSales.Checked);
        }
    }

    private void BindChkListItem()
    {
        ChkListDaybook.ItemCheck -= ChkListDayBook_ItemCheck;
        var list = new List<ValueModel<string, string>>
        {
            new("Journal Voucher", "JV"),
            new("Cash/Bank Voucher", "CB"),
            new("Receipt Voucher", "RV"),
            new("Payment Voucher", "PV"),
            new("Contra Voucher", "CV"),
            new("Receipt/Payment Voucher", "RPV"),
            new("Debit Note", "DN"),
            new("Credit Note", "CN"),
            new("Purchase Invoice", "PB"),
            new("Ticket Purchase", "PBT"),
            new("Purchase Additional", "PAB"),
            new("Purchase Return", "PR"),
            new("Purchase Expiry/Breakage Return", "PEB"),
            new("Sales Invoice", "SB"),
            new("Ticket Sales", "SBT"),
            new("Sales Additional", "SAB"),
            new("Sales Return", "SR"),
            new("Sales Expiry/Breakage Return", "SEB"),
            new("Post Dated Cheque", "PDC")
        };
        ChkListDaybook.DataSource = list;
        ChkListDaybook.DisplayMember = "Item1";
        ChkListDaybook.ValueMember = "Item2";
        ChkListDaybook.ItemCheck += ChkListDayBook_ItemCheck;
        for (var i = 0; i < ChkListDaybook.Items.Count; i++) ChkListDaybook.SetItemChecked(i, true);
    }

    private void TagFinance()
    {
        if (ChkListDaybook.Items.Count <= 0) return;
        for (var i = 0; i < ChkListDaybook.Items.Count; i++) ChkListDaybook.SetItemChecked(i, ckbSelectAll.Checked);
    }

    // OBJECT FOR THIS FORM

    #region MyRegion

    private string _fromAdDate;
    private string _fromBsDate;
    private string _rptDate;
    private string _rptName;
    private string _rptType;
    private string _toAdDate;
    private string _toBsDate;
    private string _transStation;
    private ClsMasterForm _getForm;

    #endregion MyRegion
}