using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using System;
using System.Windows.Forms;

namespace MrBLL.Utility.Common.Class;

public record GetTransactionList
{
    public static string GetTransactionVoucherNo(string actionTag, string voucherModule, string mskDate)
    {
        return GetTransactionVoucherNo(actionTag, mskDate, "MED", voucherModule, "N", "");
    }

    //_actionTag, MskDate.Text, "MED", "PB"
    public static string GetTransactionVoucherNo(string actionTag, string mskDate, string size, string voucherModule, string category)
    {
        return GetTransactionVoucherNo(actionTag, mskDate, size, voucherModule, category, "");
    }

    public static string GetTransactionVoucherNo(string actionTag, string mskDate, string size, string voucherModule)
    {
        return GetTransactionVoucherNo(actionTag, mskDate, size, voucherModule, "N", "");
    }

    public static string GetTransactionVoucherNo(string actionTag, string mskDate, string size, string voucherModule, string category, string reportMode)
    {
        var txtVno = new TextBox();
        var getDate = new MaskedTextBox
        {
            Text = mskDate.Trim()
        };
        if (!getDate.MaskCompleted || getDate.Text.Equals("/  /"))
        {
            getDate.Text = DateTime.Now.GetDateString();
        }

        var frmPickList = new FrmAutoPopList(size, voucherModule, actionTag, ObjGlobal.SearchText, category, "TRANSACTION", getDate.Text.GetSystemDate(), reportMode);
        if (FrmAutoPopList.GetListTable is { Rows: { Count: > 0 } })
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                txtVno.Text = frmPickList.SelectedList[0]["VoucherNo"].GetString();
                txtVno.Focus();
            }

            frmPickList.Dispose();
        }
        else
        {
            CustomMessageBox.VoucherNotExits();
        }

        ObjGlobal.SearchText = string.Empty;
        return txtVno.Text;
    }
}