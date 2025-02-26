using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Reports.Register_Report.Vat_Report;

public partial class FrmAuditorsLockUnlock : MrForm
{
    private ArrayList ColumnWidths = new();
    private string FromADDate;
    private string FromBSDate;
    private ArrayList HeaderCap = new();
    private string RptDate;
    private string RptName;
    private string RptType;
    private string ToADDate;
    private string ToBSDate;
    private string TransStation;

    public FrmAuditorsLockUnlock()
    {
        InitializeComponent();
    }

    private void FrmAuditorsLockUnlock_Load(object sender, EventArgs e)
    {
        try
        {
            Location = new Point(240, 40);
            BackColor = ObjGlobal.FrmBackColor();
            ObjGlobal.BindPeriodicDate(msk_FromDate, msk_ToDate);
            cmb_Currency.SelectedIndex = 0;
            ObjGlobal.BindBranch(cmb_Branch);
            cmb_Branch.SelectedIndex = 1;
            BindChkListItem();
            for (var i = 0; i < chklstbox_DayBook.Items.Count; i++) chklstbox_DayBook.SetItemChecked(i, true);
            chk_Date.Text = ObjGlobal.SysDateType == "M" ? "Date" : "Miti";
            msk_FromDate.Focus();
            cmb_OpenFor.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            var exception = ex;
        }
    }

    private void FrmAuditorsLockUnlock_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape)
        {
            if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
            {
                Close();
                return;
            }
        }

        if (e.KeyChar == (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void BindChkListItem()
    {
        //var i = 1;
        //var list = new List<ChkList>();
        //list.Add(new ChkList(i, "Journal Voucher", "JV"));
        //i = i + 1;
        //list.Add(new ChkList(i, "Receipt Voucher", "RV"));
        //i = i + 1;
        //list.Add(new ChkList(i, "Payment Voucher", "PV"));
        //i = i + 1;
        //list.Add(new ChkList(i, "Contra Voucher", "CV"));
        //i = i + 1;
        //list.Add(new ChkList(i, "Receipt/Payment Voucher", "RPV"));
        //i = i + 1;
        //list.Add(new ChkList(i, "Debit Note", "DN"));
        //i = i + 1;
        //list.Add(new ChkList(i, "Credit Note", "CN"));
        //i = i + 1;
        //list.Add(new ChkList(i, "Purchase Invoice", "PB"));
        //i = i + 1;
        //list.Add(new ChkList(i, "Purchase Additional", "PA"));
        //i = i + 1;
        //list.Add(new ChkList(i, "Purchase Return", "PR"));
        //i = i + 1;
        //list.Add(new ChkList(i, "Purchase Expiry/Breakage Return", "PEB"));
        //i = i + 1;
        //list.Add(new ChkList(i, "Sales Invoice", "SB"));
        //i = i + 1;
        //list.Add(new ChkList(i, "Sales Additional", "SA"));
        //i = i + 1;
        //list.Add(new ChkList(i, "Sales Return", "SR"));
        //i = i + 1;
        //list.Add(new ChkList(i, "Sales Expiry/Breakage Return", "SEB"));
        //i = i + 1;
        //if (ObjGlobal._ProjectCode == "08" || ObjGlobal._ProjectCode == "99") //'08 - Hotel'--'99 - Developer '
        //{
        //    list.Add(new ChkList(i, "Hotel Booking", "HBN"));
        //    i = i + 1;
        //    list.Add(new ChkList(i, "Hotel Booking Cancel", "HBCN"));
        //    i = i + 1;
        //    list.Add(new ChkList(i, "Hotel Check In", "HCI"));
        //    i = i + 1;
        //    list.Add(new ChkList(i, "Hotel Check Out", "HCO"));
        //    i = i + 1;
        //}

        //chklstbox_DayBook.DataSource = list;
        //chklstbox_DayBook.DisplayMember = "Name";
        //chklstbox_DayBook.ValueMember = "Value";
    }

    private void msk_FromDate_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(msk_FromDate, 'E');
    }

    private void msk_FromDate_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void msk_FromDate_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(msk_FromDate, 'L');
    }

    private void msk_FromDate_Validated(object sender, EventArgs e)
    {
    }

    private void msk_ToDate_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(msk_ToDate, 'E');
    }

    private void msk_ToDate_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void msk_ToDate_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(msk_ToDate, 'L');
    }

    private void msk_ToDate_Validated(object sender, EventArgs e)
    {
    }

    private void txt_Find_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_Find, 'E');
    }

    private void txt_Find_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_Find, 'L');
    }

    private void btn_Show_Click(object sender, EventArgs e)
    {
    }

    private class ChkList
    {
        public ChkList(int s, string n, string v)
        {
            SNo = s;
            Name = n;
            Value = v;
        }

        public int SNo { get; }
        public string Name { get; }
        public string Value { get; }
    }
}