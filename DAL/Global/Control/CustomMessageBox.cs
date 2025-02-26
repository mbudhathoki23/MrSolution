using DevExpress.XtraEditors;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using System;
using System.Windows.Forms;
using ComboBox = System.Windows.Forms.ComboBox;

namespace MrDAL.Global.Control;

public static class CustomMessageBox
{
    public static DialogResult ExitActiveForm(string customMessage = "")
    {
        var result = MessageBox.Show(customMessage.IsValueExits()
                ? customMessage
                : @"DO YOU WANT TO EXIT THE FORM..!!", ObjGlobal.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        return result;
    }

    public static DialogResult DeleteRow()
    {
        var result = MessageBox.Show(@"DO YOU WANT TO DELETE THE SELECTED ROW..!!", ObjGlobal.Caption,
            MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        return result;
    }

    public static DialogResult UpdateRow()
    {
        var result = MessageBox.Show(@"DO YOU WANT TO UPDATE THE SELECTED ROW..!!", ObjGlobal.Caption,
            MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        return result;
    }

    public static DialogResult VoucherNotExits()
    {
        var result = MessageBox.Show(@"VOUCHER NUMBER LIST NOT EXISTS..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
            MessageBoxIcon.Information);
        return result;
    }

    public static DialogResult BlankVoucherNo()
    {
        var result = MessageBox.Show(@"VOUCHER NUMBER IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
        return result;
    }

    public static DialogResult ActionSuccess(string msg, string type, string action)
    {
        var result = MessageBox.Show($@"{msg} {type} {action} SUCCESSFULLY..!!", ObjGlobal.Caption,
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        return result;
    }

    public static DialogResult ActionError(string msg, string type, string action)
    {
        var result = MessageBox.Show($@"ERROR OCCURS WHILE {msg} {type} {action} ..!!", ObjGlobal.Caption,
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        return result;
    }

    public static DialogResult BlankVoucherDetails(string details)
    {
        var result = MessageBox.Show($@"{details} IS BLANK IN THIS VOUCHER..!!", ObjGlobal.Caption,
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return result;
    }

    public static DialogResult ClearVoucherDetails(string details)
    {
        var result = MessageBox.Show($@"DO YOU WANT TO CLEAR {details} DETAILS ..!!", ObjGlobal.Caption,
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        return result;
    }

    public static DialogResult VoucherNoAction(string actionTag, string voucherNo)
    {
        var result = MessageBox.Show($@"ARE YOU SURE YOU WANT TO [{actionTag}] [{voucherNo}] VOUCHER!", ObjGlobal.Caption,
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        return result;
    }

    public static DialogResult FormAction(string actionTag, string type)
    {
        var result = MessageBox.Show($@"ARE YOU SURE YOU WANT TO [{actionTag}] [{type}]!", ObjGlobal.Caption,
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        return result;
    }

    public static DialogResult DialogResult(this Exception ex, bool isError = true)
    {
        if (isError) ex.ToNonQueryErrorResult(ex.ToString());
        var result = MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.OK,
            isError ? MessageBoxIcon.Error : MessageBoxIcon.Warning);
        return result;
    }

    public static DialogResult Information(string msg)
    {
        var result = MessageBox.Show(msg, ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        return result;
    }

    public static DialogResult Warning(string msg)
    {
        var result = MessageBox.Show(msg, ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return result;
    }

    public static DialogResult Question(string msg)
    {
        var result = MessageBox.Show(msg, ObjGlobal.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        return result;
    }

    public static DialogResult ErrorMessage(string msg)
    {
        var result = MessageBox.Show(msg, ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        return result;
    }

    public static DialogResult ErrorMessage(this TextBox box, string msg)
    {
        var result = ErrorMessage(msg);
        box.Focus();
        return result;
    }
    public static DialogResult WarningMessage(this ComboBox controlBox, string msg)
    {
        var result = MessageBox.Show(msg, ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        controlBox.Focus();
        return result;
    }
    public static DialogResult WarningMessage(this TextBox controlBox, string msg)
    {
        var result = MessageBox.Show(msg, ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        controlBox.Focus();
        return result;
    }

    public static DialogResult WarningMessage(this MaskedTextBox controlBox, string msg)
    {
        var result = MessageBox.Show(msg, ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        controlBox.Focus();
        return result;
    }

    public static DialogResult WarningMessage(this TextEdit controlBox, string msg)
    {
        var result = MessageBox.Show(msg, ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        controlBox.Focus();
        return result;
    }

    public static DialogResult WarningMessage(this Label controlBox, string msg)
    {
        var result = MessageBox.Show(msg, ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        controlBox.Focus();
        return result;
    }
}