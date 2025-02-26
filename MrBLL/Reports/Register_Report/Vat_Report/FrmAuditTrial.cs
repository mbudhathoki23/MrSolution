using MrBLL.Utility.Common;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Utility.PickList;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Reports.Register_Report.Vat_Report;

public partial class FrmAuditTrial : MrForm
{
    private readonly ArrayList ColumnWidths = new();
    private readonly ArrayList HeaderCap = new();
    private string FromADDate;
    private string FromBSDate;
    private string RptDate;
    private string RptName;
    private string RptType;
    private string ToADDate;
    private string ToBSDate;
    private string TransStation;

    public FrmAuditTrial()
    {
        InitializeComponent();
    }

    private void FrmAuditTrial_Load(object sender, EventArgs e)
    {
        try
        {
            Location = new Point(240, 40);
            BackColor = ObjGlobal.FrmBackColor();
            ObjGlobal.BindPeriodicDate(msk_FromDate, msk_ToDate);
            ObjGlobal.BindBranch(cmb_Branch);
            cmb_Branch.SelectedIndex = 0;
            cmb_Action.SelectedIndex = 0;
            cmb_FilterBy.SelectedIndex = 0;
            BindChkListItem();
            for (var i = 0; i < chklstbox_DayBook.Items.Count; i++)
            {
                chklstbox_DayBook.SelectedIndex = i;
                if (chklstbox_DayBook.SelectedValue.ToString() == "SB")
                    chklstbox_DayBook.SetItemChecked(i, true);
                else
                    chklstbox_DayBook.SetItemChecked(i, false);
            }

            if (ObjGlobal.SysDateType == "M")
                chk_Date.Text = "Date";
            else
                chk_Date.Text = "Miti";
            cmbSysDateType.SelectedIndex = 8;
            cmbSysDateType.Focus();
        }
        catch (Exception ex)
        {
            var exception = ex;
        }
    }

    private void FrmAuditTrial_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape)
        {
            var dialogResult = MessageBox.Show("Are you sure want to Close Form!", ObjGlobal.Caption,
                MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) Close();
        }

        if (e.KeyChar == (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void BindChkListItem()
    {
        //var Id = 1;
        //var list = new List<ChkList>();
        //list.Add(new ChkList(Id, "Journal Voucher", "JV"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Cash/bank Voucher", "CB"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Receipt Voucher", "RV"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Payment Voucher", "PV"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Contra Voucher", "CV"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Receipt/Payment Voucher", "RPV"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Debit Note", "DN"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Credit Note", "CN"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Purchase Indent", "PI"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Purchase Quotation", "PQ"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Purchase Order", "PO"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Purchase Order Cancellation", "POC"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Purchase Challan", "PC"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Purchase Inter Branch", "PIB"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Purchase Quality Control", "PQC"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Purchase Invoice", "PB"));
        //Id = Id + 1;
        //list.Add(new ChkList(15, "Purchase Additional Invoice", "PAB"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Purchase Return", "PR"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Purchase Expiry/Breakage Return", "PEB"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Sales Quotation", "SQ"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Sales Order", "SO"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Sales Order Cancellation", "SOC"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Sales Dispatch Order", "SDO"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Sales Dispatch Order Cancellation", "SDOC"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Sales Challan", "SC"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Sales Inter Branch", "SIB"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Sales Invoice", "SB"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Sales Additional Invoice", "SAB"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Point Of Sales", "POS"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Abbreviated Tax Invoice", "ATI"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Sales Return", "SR"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Sales Expiry/Breakage Return", "SEB"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Stock Transfer", "ST"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Stock Adjustment", "SA"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Transfer Expiry/Breakage", "STEB"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Assembly Master", "ASSM"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Memo", "BOM"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Inventory Requisition", "SREQ"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Inventory Issue", "MI"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Inventory Issue Return", "MIR"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Inventory Receive", "MR"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Inventory Receive Return", "MRR"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Production Master Memo", "MBOM"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Production Memo", "IBOM"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Production Requisition", "IREQ"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Production Issue", "RMI"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Production Issue Return", "RMIR"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Finished Good Receive", "FGR"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Finished Good Receive Return", "FGRR"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Production Order", "IPO"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Production Planning", "IPP"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Assets Log", "ASL"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Bank Reconcillation", "BRN"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Chart of Product", "PM"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Product Group", "PG"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Product Sub Group", "PSG"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Chart of Account", "LM"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Sub Ledger Master", "SL"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Ledger Opening", "LO"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Agent Master", "AM"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Area Master", "ARM"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Class Master", "CM"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Currency Master", "CCM"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Godown Master", "GM"));
        //Id = Id + 1;
        //list.Add(new ChkList(Id, "Billing Term Master", "BT"));
        //Id = Id + 1;
        //if (ObjGlobal._ProjectCode == "08" || ObjGlobal._ProjectCode == "99") //'08 - Hotel'--'99 - Developer '
        //{
        //    list.Add(new ChkList(Id, "Hotel Booking NO", "HBN"));
        //    Id = Id + 1;
        //    list.Add(new ChkList(Id, "Hotel Booking Cancel No", "HBCN"));
        //    Id = Id + 1;
        //    list.Add(new ChkList(Id, "Hotel Check In", "HCI"));
        //    Id = Id + 1;
        //    list.Add(new ChkList(Id, "Hotel Check Out", "HCO"));
        //    Id = Id + 1;
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
        if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void msk_FromDate_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(msk_FromDate, 'L');
    }

    private void msk_FromDate_Validated(object sender, EventArgs e)
    {
        if (msk_FromDate.Text.Trim() == "/  /")
        {
            MessageBox.Show("From Date can't be blank!!");
            msk_FromDate.Focus();
            return;
        }

        if (ObjGlobal.SysDateType == "M")
        {
            FromADDate = ObjGlobal.ReturnEnglishDate(msk_FromDate.Text);
            FromBSDate = msk_FromDate.Text;
            ToADDate = ObjGlobal.ReturnEnglishDate(msk_ToDate.Text);
            ToBSDate = msk_ToDate.Text;
        }
        else
        {
            FromADDate = msk_FromDate.Text;
            FromBSDate = ObjGlobal.ReturnNepaliDate(msk_FromDate.Text);
            ToADDate = msk_ToDate.Text;
            ToBSDate = ObjGlobal.ReturnNepaliDate(msk_ToDate.Text);
        }
    }

    private void msk_ToDate_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(msk_ToDate, 'E');
    }

    private void msk_ToDate_KeyDown(object sender, KeyEventArgs e)
    {
        //if(e.KeyCode==Keys.Enter)
        //{
        //    SendKeys.Send("{TAB}");
        //}
    }

    private void msk_ToDate_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(msk_ToDate, 'L');
    }

    private void msk_ToDate_Validated(object sender, EventArgs e)
    {
        if (msk_ToDate.Text.Trim() == "/  /")
        {
            MessageBox.Show("From Date can't be blank!!");
            msk_ToDate.Focus();
            return;
        }

        if (ObjGlobal.SysDateType == "M")
        {
            FromADDate = ObjGlobal.ReturnEnglishDate(msk_FromDate.Text);
            FromBSDate = msk_FromDate.Text;
            ToADDate = ObjGlobal.ReturnEnglishDate(msk_ToDate.Text);
            ToBSDate = msk_ToDate.Text;
        }
        else
        {
            FromADDate = msk_FromDate.Text;
            FromBSDate = ObjGlobal.ReturnNepaliDate(msk_FromDate.Text);
            ToADDate = msk_ToDate.Text;
            ToBSDate = ObjGlobal.ReturnNepaliDate(msk_ToDate.Text);
        }
    }

    private void btn_Show_Click(object sender, EventArgs e)
    {
        var Query = string.Empty;
        string[] Code;
        var UserId = string.Empty;
        var UserIdList = string.Empty;
        FromADDate = ObjGlobal.SysDateType == "M"
            ? ObjGlobal.ReturnEnglishDate(msk_FromDate.Text)
            : msk_FromDate.Text;
        FromBSDate = ObjGlobal.SysDateType == "M"
            ? msk_FromDate.Text
            : ObjGlobal.ReturnNepaliDate(msk_FromDate.Text);
        ToADDate = ObjGlobal.SysDateType == "M" ? ObjGlobal.ReturnEnglishDate(msk_ToDate.Text) : msk_ToDate.Text;
        ToBSDate = ObjGlobal.SysDateType == "M" ? msk_ToDate.Text : ObjGlobal.ReturnNepaliDate(msk_ToDate.Text);
        if (msk_FromDate.Text.Trim() == "/  /")
        {
            MessageBox.Show("From Date can't be blank!!");
            msk_FromDate.Focus();
            return;
        }

        if (msk_ToDate.Text.Trim() == "/  /")
        {
            MessageBox.Show("From Date can't be blank!!");
            msk_ToDate.Focus();
            return;
        }

        ClsTagList.PlValue1 = string.Empty;
        ClsTagList.PlValue2 = string.Empty;
        ClsTagList.PlValue3 = string.Empty;
        HeaderCap.Clear();
        ColumnWidths.Clear();
        HeaderCap.Add("Check");
        HeaderCap.Add("Description");
        HeaderCap.Add("ShortName");
        HeaderCap.Add("Id");
        HeaderCap.Add("Id");
        HeaderCap.Add("Id");

        ColumnWidths.Add(50);
        ColumnWidths.Add(250);
        ColumnWidths.Add(100);
        ColumnWidths.Add(0);
        ColumnWidths.Add(0);
        ColumnWidths.Add(0);
        Query = "Select CONVERT(bit, 0) Chk, Full_Name,User_Name,User_Id,'','' From Master.AMS.UserInfo";
        var PkLst = FrmTagList.CreateInstance(Query, HeaderCap, ColumnWidths, "Users List");
        if (PkLst.ShowDialog() == DialogResult.OK)
        {
            if (!string.IsNullOrEmpty(ClsTagList.PlValue3))
            {
                UserId = ClsTagList.PlValue3;
            }
            else
            {
                MessageBox.Show("Users Not Selected!");
                return;
            }
        }
        else
        {
            MessageBox.Show("Users Not Selected!");
            return;
        }

        TransStation = string.Empty;
        for (var i = 0; i < chklstbox_DayBook.Items.Count; i++)
        {
            chklstbox_DayBook.SelectedIndex = i;
            if (chklstbox_DayBook.GetItemChecked(chklstbox_DayBook.SelectedIndex))
            {
                if (TransStation != string.Empty)
                    TransStation = TransStation + "," + chklstbox_DayBook.SelectedValue;
                else TransStation = chklstbox_DayBook.SelectedValue.ToString();
            }
        }

        TransStation = TransStation.Trim(',');

        RptType = "Normal";
        if (cmb_Branch.SelectedIndex == 0) RptName = "Audit Trial Register";
        else RptName = "Audit Trial Register " + "(For " + cmb_Branch.Text + " Branch )";
        if (ObjGlobal.SysDateType == "M" && chk_Date.Checked == false ||
            ObjGlobal.SysDateType == "D" && chk_Date.Checked)
            RptDate = "From Date " + FromBSDate + " To " + ToBSDate;
        else
            RptDate = "From Date " + Convert.ToDateTime(FromADDate).ToShortDateString() + " To " +
                      Convert.ToDateTime(ToADDate).ToShortDateString();
        var DR = new FrmAuditorsLockUnlockReport(RptType, RptName, RptDate, FromADDate, ToADDate, false, false,
            UserId, false, false, false, chk_Date.Checked, string.Empty, TransStation,
            cmb_Branch.SelectedValue.ToString(), cmb_Action.Text, cmb_FilterBy.Text);
        DR.Text = RptName + " Report";
        DR.Show();
    }

    private void cmbSysDateType_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
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