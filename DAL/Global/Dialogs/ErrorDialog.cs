using MrDAL.Core.Extensions;
using MrDAL.Models.Common;
using System;
using System.Windows.Forms;

namespace MrDAL.Global.Dialogs;

public static class ErrorDialog
{
    public static void ShowErrorDialog(this NonQueryResult result)
    {
        MessageBox.Show(result.ErrorMessage, result.ResultType.SplitCamelCase(),
            MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public static void ShowErrorDialog<T>(this InfoResult<T> result, string userMessage = null)
    {
        MessageBox.Show(
            string.IsNullOrWhiteSpace(userMessage)
                ? result.ErrorMessage
                : userMessage + Environment.NewLine + result.ErrorMessage,
            result.ResultType.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public static void ShowErrorDialog<T>(this ListResult<T> result, string userMessage = null)
    {
        MessageBox.Show(
            string.IsNullOrWhiteSpace(userMessage)
                ? result.ErrorMessage
                : userMessage + Environment.NewLine + result.ErrorMessage,
            result.ResultType.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}