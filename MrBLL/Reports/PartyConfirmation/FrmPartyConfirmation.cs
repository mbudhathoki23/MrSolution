using MrBLL.Utility.Common;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Utility.PickList;
using System;
using System.Windows.Forms;

namespace MrBLL.Reports.PartyConfirmation;

public partial class FrmPartyConfirmation : Form
{
    // PARTY CONFIRMATION

    #region --------------- PARTY CONFIRMATION ---------------

    public FrmPartyConfirmation()
    {
        InitializeComponent();
        _setup = new ClsMasterSetup();
        _setup.BindFiscalYear(CmbFiscalYear);
        CmbFiscalYear.SelectedValue = CmbFiscalYear.Items.Count > 2 ? ObjGlobal.SysFiscalYearId - 1 : ObjGlobal.SysFiscalYearId;
    }

    private void FrmPartyConfirmation_Load(object sender, EventArgs e)
    {
        CmbFiscalYear.Focus();
    }

    private void TxtTransactionAbove_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
        TxtTransactionAbove.Text = TxtTransactionAbove.GetDecimalString();
    }

    private void BtnShow_Click(object sender, EventArgs e)
    {
        if (!ChkSelectAll.Checked)
        {
            GetPartyLedger();
            if (_ledgerId.IsBlankOrEmpty())
            {
                CustomMessageBox.Warning("PLEASE SELECT THE PARTY LEDGER..!!");
                return;
            }
        }

        var result = new DisplayPartyConfirmation
        {
            LedgerId = _ledgerId,
            LedgerType = ChkCustomerWise.Checked ? "CUSTOMER" : "VENDOR",
            FiscalYearId = CmbFiscalYear.SelectedValue.GetInt(),
            IncludeReturn = ChkIncludeReturn.Checked,
            IncludeVat = ChkIncludeVatNetAmount.Checked,
            EnglishLetter = ChkPartyConfirmationEnglish.Checked,
            PrintHeader = ChkPrintHeader.Checked,
            AboveAmount = TxtTransactionAbove.GetDecimal()
        };
        result.ShowDialog();
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
        {
            Close();
        }
    }

    #endregion --------------- PARTY CONFIRMATION ---------------

    // METHOD FOR THIS FORM

    #region --------------- METHOD FOR THIS FORM ---------------

    private string GetPartyLedger()
    {
        var frm2 = new FrmTagList
        {
            ReportDesc = @"GENERALLEDGER",
            Category = ChkCustomerWise.Checked ? "Customer" :
                ChkVendorWise.Checked ? "Vendor" : "",
            FiscalYearId = CmbFiscalYear.SelectedValue.GetIntString()
        };
        frm2.ShowDialog();

        _ledgerId = ClsTagList.PlValue1;
        return _ledgerId;
    }

    #endregion --------------- METHOD FOR THIS FORM ---------------

    // OBJECT FOR THIS FORM

    #region --------------- OBJECT FOR THIS FORM ---------------

    private IMasterSetup _setup;
    private string _ledgerId;

    #endregion --------------- OBJECT FOR THIS FORM ---------------
}