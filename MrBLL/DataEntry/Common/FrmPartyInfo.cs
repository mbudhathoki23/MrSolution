using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.CommonSetup;
using MrDAL.DataEntry.Interface.Common;
using MrDAL.Global.Common;
using MrDAL.Master;
using System;
using System.Data;
using System.Windows.Forms;

namespace MrBLL.DataEntry.Common;

public partial class FrmPartyInfo : MrForm
{
    // PARTY INFORMATION

    #region --------------- Frm ---------------

    public FrmPartyInfo(string ledgerType, DataTable dtPartyInfo, string category = "")
    {
        InitializeComponent();
        ClearControl();
        _ledgerType = ledgerType;
        _category = category;
        BindDataToTable(dtPartyInfo);
        _form = new ClsMasterForm(this, BtnCancel);
    }

    private void FrmPartyInfo_Load(object sender, EventArgs e)
    {
        if (_ledgerType == "B")
        {
            TxtChequeNo.Enabled = true;
            MskChequeDate.Enabled = true;
            MskChequeDate.Text = DateTime.Now.GetDateString();
            MskChequeMiti.Text = DateTime.Now.GetNepaliDate();
            ObjGlobal.PageLoadDateType(MskChequeDate);
        }

        BindDataFromTableToTextBox();
    }

    private void FrmPartyInfo_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape) Close();
        //if (e.KeyChar is (char)Keys.Enter)
        //{
        //    SendKeys.Send("{TAB}");
        //}
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (PartyInfo.Rows.Count > 0)
        {
            PartyInfo.Rows.Clear();
        }
        var dataRow = PartyInfo.NewRow();
        dataRow["PartyLedgerId"] = _partyLedgerId;
        dataRow["PartyName"] = TxtDescription.Text;
        dataRow["ChequeNo"] = TxtChequeNo.Text;
        dataRow["ChequeDate"] = MskChequeDate.Text;
        dataRow["ChequeMiti"] = MskChequeMiti.Text;
        dataRow["VatNo"] = TxtTPanNo.Text;
        dataRow["ContactPerson"] = TxtContactNo.Text;
        dataRow["Address"] = TxtAddress.Text;
        dataRow["Mob"] = TxtContactNo.Text;
        dataRow["Email"] = TxtEmailAddress.Text;
        PartyInfo.Rows.Add(dataRow);
        Close();
    }

    private void TxtDescription_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnGeneralLedger_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (!TxtDescription.IsBlankOrEmpty()) return;
            e.SuppressKeyPress = true;
            BtnSave.Focus();
        }
    }

    private void BtnGeneralLedger_Click(object sender, EventArgs e)
    {
        (TxtDescription.Text, _partyLedgerId) = GetMasterList.GetPartyLedger("SAVE", _category);
        FillPartyInfo();
    }

    #endregion --------------- Frm ---------------

    // METHOD FOR THIS FORM

    #region --------------- Method ---------------

    private void BindDataFromTableToTextBox()
    {
        if (PartyInfo.Rows.Count <= 0) return;
        foreach (DataRow dr in PartyInfo.Rows)
        {
            _partyLedgerId = dr["PartyLedgerId"].GetLong();
            TxtDescription.Text = dr["PartyName"].ToString();
            TxtChequeNo.Text = dr["ChequeNo"].ToString();
            MskChequeDate.Text = dr["ChequeDate"].ToString();
            TxtTPanNo.Text = dr["VatNo"].ToString();
            TxtContactPerson.Text = dr["ContactPerson"].ToString();
            TxtAddress.Text = dr["Address"].ToString();
            TxtContactNo.Text = dr["Mob"].ToString();
        }
    }

    private void ClearControl()
    {
        TxtDescription.Clear();
        TxtChequeNo.Clear();
        MskChequeDate.Clear();
        TxtTPanNo.Clear();
        TxtContactPerson.Clear();
        TxtAddress.Clear();
        TxtContactNo.Clear();
    }

    private void FillPartyInfo()
    {
        var date = DateTime.Now.GetSystemDate();
        var dtCustomer = ClsMasterSetup.LedgerInformation(_partyLedgerId, date);
        if (dtCustomer.Rows.Count > 0)
        {
            TxtDescription.Text = dtCustomer.Rows[0]["GLName"].GetString();
            TxtTPanNo.Text = dtCustomer.Rows[0]["PanNo"].GetString();
            TxtContactNo.Text = dtCustomer.Rows[0]["PhoneNo"].GetString();
        }
        else if (dtCustomer.Rows.Count is 0)
        {
            dtCustomer = ClsMasterSetup.PartyInformation(TxtDescription.Text);
            if (dtCustomer.Rows.Count > 0)
            {
                TxtDescription.Text = dtCustomer.Rows[0]["PartyName"].GetString();
                TxtTPanNo.Text = dtCustomer.Rows[0]["Party_PanNo"].GetString();
            }
        }
    }

    private void BindDataToTable(DataTable dtPartyInfo)
    {
        if (dtPartyInfo.Rows.Count > 0)
        {
            PartyInfo.Reset();
            PartyInfo = dtPartyInfo.Copy();
        }
        else
        {
            PartyInfo = _partyInfoRepository.GetPartyInfo();
        }
    }

    #endregion --------------- Method ---------------

    // OBJECT FOR THIS FORM

    #region --------------- Global Variable ---------------

    private long _partyLedgerId;
    private readonly string _ledgerType;
    private readonly string _category;
    public DataTable PartyInfo = new();
    private readonly IPartyInfoRepository _partyInfoRepository = new PartyInfoRepository();
    private ClsMasterForm _form;

    #endregion --------------- Global Variable ---------------
}