using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Reports.Register_Report.Vat_Report;

public partial class FrmEntryLogRegister : MrForm
{
    private void BindChkListItem()
    {
        var list = new List<ValueModel<string, string>>
        {
            new("Journal Voucher", "JV"),
            new("Cash/Bank Voucher", "CB"),
            new("Debit Note", "DN"),
            new("Credit Note", "CN"),
            new("Purchase Invoice", "PB"),
            new("Purchase Return", "PR"),
            new("Sales Invoice", "SB"),
            new("Sales Return", "SR"),
            new("Post Dated Cheque", "PDC")
        };
        ChkEntryLog.DataSource = list;
        ChkEntryLog.DisplayMember = "Item1";
        ChkEntryLog.ValueMember = "Item2";
        for (var i = 0; i < ChkEntryLog.Items.Count; i++) ChkEntryLog.SetItemChecked(i, true);
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
        RptType = "Normal";
        RptDate = ObjGlobal.SysDateType == "M"
            ? $"From Date {FromBSDate} To {ToBSDate}"
            : $"From Date {Convert.ToDateTime(FromADDate).ToShortDateString()} To {Convert.ToDateTime(ToADDate).ToShortDateString()}";
        var dr = new DisplayRegisterReports
        {
            RptName = "ENTRY LOG REGISTER",
            RptDate = RptDate,
            RptType = RptType,
            FromAdDate = FromADDate,
            FromBsDate = FromBSDate,
            ToAdDate = ToADDate
        };
        dr.Show();
        dr.BringToFront();
        dr.Focus();
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
    }

    #region --------------- OBJECT ---------------

    private string FromADDate;
    private string FromBSDate;
    private string Query = string.Empty;
    private string RptDate;
    private string RptName;
    private string ReportMode = string.Empty;
    private string RptType;
    private string ToADDate;
    private string ToBSDate;
    private int VatTermId;
    private IMasterSetup _getMaster;

    #endregion --------------- OBJECT ---------------

    #region --------------- FrmEntryLogRegister ---------------

    public FrmEntryLogRegister()
    {
        InitializeComponent();
        BackColor = ObjGlobal.FrmBackColor();
        _getMaster = new ClsMasterSetup();
    }

    public sealed override Color BackColor
    {
        get => base.BackColor;
        set => base.BackColor = value;
    }

    private void FrmEntryLogRegister_Load(object sender, EventArgs e)
    {
        ObjGlobal.BindDateType(CmbDateType);
        ObjGlobal.BindPeriodicDate(MskFrom, MskToDate);
        BindChkListItem();
        ChkDate.Text = ObjGlobal.SysDateType == "M" ? "Date" : "Miti";
        CmbDateType.SelectedIndex = 8;
        CmbDateType.Focus();
    }

    private void FrmEntryLogRegister_KeyPress(object sender, KeyPressEventArgs e)
    {
        switch (e.KeyChar)
        {
            case (char)Keys.Escape:
            {
                BtnCancel.PerformClick();
                break;
            }
            case (char)Keys.Enter:
            {
                SendKeys.Send("{TAB}");
                break;
            }
        }
    }

    #endregion --------------- FrmEntryLogRegister ---------------
}