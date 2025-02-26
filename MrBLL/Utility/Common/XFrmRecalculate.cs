using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using MrDAL.Control.SplashScreen;
using MrDAL.Core.Utils;
using MrDAL.Global.Control;
using MrDAL.Models.Common;
using MrDAL.Utility.Config;
using MrDAL.Utility.Interface;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MrBLL.Utility.Common;

public partial class XFrmRecalculate : XtraForm
{
    // RECALCULATE

    #region -------------------- Form --------------------

    public XFrmRecalculate()
    {
        InitializeComponent();
    }
    private void XFrmRecalculate_Load(object sender, EventArgs e)
    {

        ChkListInventory.Enabled = false;
        ChkListFinance.Enabled = false;
    }
    private void XFrmRecalculate_Shown(object sender, EventArgs e)
    {
        BindChkListItem();
        BindChkInvListItem();
    }
    #endregion -------------------- Form --------------------

    // METHOD FOR THIS CLASS

    #region -------------------- Method --------------------

    private void BindChkListItem()
    {
        var list = new List<ValueModel<string, string>>
        {
            new ("Opening", "OB"),
            new ("Purchase Invoice", "PB"),
            new ( "Purchase Return", "PR"),
            new ("Purchase Exp/Brk", "PEB"),
            new ("Sales Invoice", "SB"),
            new ("Sales Return", "SR"),
            new("Sales Exp/Brk", "SEB"),
            new("Cash & Bank", "CB"),
            new("Journal Voucher", "JV"),
            new("Debit Notes", "DN"),
            new("Credit Notes", "CN"),
            new("Post Dated Cheque", "PDC"),
            new("Sales Tour & Travel", "STTB"),
            new("Purchase Tour & Travel", "PTTB")
        };
        ChkListFinance.DataSource = list;
        ChkListFinance.DisplayMember = "Item1";
        ChkListFinance.ValueMember = "Item2";
    }

    private void BindChkInvListItem()
    {
        var list = new List<ValueModel<string, string>>
        {
            new("Opening", "OB"),
            new("Purchase Challan", "PC"),
            new("Purchase Invoice", "PB"),
            new("Purchase Return", "PR"),
            new("Purchase Exp/Brk", "PEB"),
            new("Sales Challan", "SC"),
            new("Sales Invoice", "SB"),
            new("Sales Return", "SR"),
            new("Sales Exp/Brk", "SEB"),
            new("Stock Adjustment", "SA"),
            new("Production", "IBOM")
        };
        ChkListInventory.DataSource = list;
        ChkListInventory.DisplayMember = "Item1";
        ChkListInventory.ValueMember = "Item2";
    }

    private void TagFinance()
    {
        if (ChkListFinance.Items.Count <= 0) return;
        for (var i = 0; i < ChkListFinance.Items.Count; i++)
            ChkListFinance.SetItemChecked(i, ChkFinance.Checked && ckbSelectAll.Checked);
    }

    private void TagInventory()
    {
        if (ChkListInventory.Items.Count <= 0) return;

        for (var i = 0; i < ChkListInventory.Items.Count; i++)
            ChkListInventory.SetItemChecked(i, ChkInventory.Checked && ckbSelectAll.Checked);
    }

    #endregion -------------------- Method --------------------

    // EVENT FOR THIS FORM

    #region -------------------- Event --------------------

    private void CkbSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        TagFinance();
        TagInventory();
        ckbUnSellectAll.Enabled = ChkFinance.Checked || ChkFinance.Checked;
    }

    private void CkbUnSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CkbSelectAll_CheckedChanged(sender, e);
    }

    private async void BtnRecalculate_Click(object sender, EventArgs e)
    {
        try
        {
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            if (ChkFinance.Checked && ChkListFinance.Items.Count > 0)
            {
                for (var i = 0; i < ChkListFinance.Items.Count; i++)
                {
                    ChkListFinance.SelectedIndex = i;
                    if (ChkListFinance.GetItemChecked(ChkListFinance.SelectedIndex))
                    {
                        _recalculate.UpdateFinanceTransaction(ChkListFinance.SelectedValue.ToString());
                    }
                }
            }

            if (ChkInventory.Checked && ChkListInventory.Items.Count > 0)
            {
                for (var i = 0; i < ChkListInventory.Items.Count; i++)
                {
                    ChkListInventory.SelectedIndex = i;
                    if (ChkListInventory.GetItemChecked(ChkListInventory.SelectedIndex))
                    {
                        _recalculate.UpdateStockTransaction(ChkListInventory.SelectedValue.ToString());

                    }
                }
            }
            var recalculateMsg =
                ChkFinance.Checked && ChkInventory.Checked ? "FINANCE & STOCK" :
                ChkFinance.Checked && !ChkInventory.Checked ? "FINANCE" :
                !ChkFinance.Checked && ChkInventory.Checked ? "STOCK" : "";
            SplashScreenManager.CloseForm(false);
            var msg = CustomMessageBox.Information($"{recalculateMsg} TRANSACTION RECALCULATED SUCCESSFULLY..!!");
            if (ChkInventory.Checked)
            {
                var question = CustomMessageBox.Question("DO YOU WANT TO RESPOST STOCK VALUE...??");
                if (question == DialogResult.Yes)
                {
                    try
                    {
                        SplashScreenManager.ShowForm(typeof(PleaseWait));
                        const string cmdRePost = "AMS.USP_PostStockValue";
                        var result = SqlExtensions.ExecuteNonQuery(cmdRePost, new SqlParameter("@PCode", null));
                        if (result == 0)
                        {
                            
                            return;
                        }
                        _recalculate.UpdateStockTransaction("REPOST");
                        SplashScreenManager.CloseForm(false);
                        CustomMessageBox.Information("RE - POST VALUE SUCCESSFULLY..!!");
                        return;
                    }
                    catch (Exception ex)
                    {
                        ex.ToNonQueryErrorResult(ex.StackTrace);
                        SplashScreenManager.CloseForm(false);
                    }

                }
                _recalculate.UpdateStockTransaction("REPOST");
            }
            if (ChkFinance.Checked == ChkInventory.Checked)
            {
                Close();
            }
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            SplashScreenManager.CloseForm(false);
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
        return;
    }

    private void CmbFinance_CheckedChanged(object sender, EventArgs e)
    {
        ChkListFinance.Enabled = ChkFinance.Checked;
        if (ChkListFinance.Items.Count <= 0) return;
        for (var i = 0; i < ChkListFinance.Items.Count; i++)
        {
            ChkListFinance.SetItemChecked(i, ChkFinance.Checked && ckbSelectAll.Checked);
        }
    }

    private void CmbInventory_CheckedChanged(object sender, EventArgs e)
    {
        ChkListInventory.Enabled = ChkInventory.Checked;
        if (ChkListInventory.Items.Count <= 0) return;
        for (var i = 0; i < ChkListInventory.Items.Count; i++)
        {
            ChkListInventory.SetItemChecked(i, ChkInventory.Checked && ckbSelectAll.Checked);
        }
    }

    #endregion -------------------- Event --------------------

    // OBJECT FOR THIS GLOBAL

    #region -------------------- GLobal --------------------

    private DataTable _dataTable = new();
    private DataTable _table = new();
    private string _query = string.Empty;
    private readonly IRecalculate _recalculate = new ClsRecalculate();

    #endregion -------------------- GLobal --------------------


}