using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace MrBLL.Utility.DataSync;

public partial class FrmSyncLogs : XtraForm
{
    public FrmSyncLogs(bool allowClosing)
    {
        InitializeComponent();
        AllowClosing = allowClosing;
    }

    public bool AllowClosing { get; set; }

    public void AddLog(string message)
    {
        memo.Text = string.IsNullOrWhiteSpace(memo.Text) ? message : memo.Text + Environment.NewLine + message;

        memo.SelectionStart = memo.Text.Length;
        memo.ScrollToCaret();
    }

    public void ClearLog()
    {
        memo.Text = "";
    }

    private void FrmSyncLogs_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = !AllowClosing;
    }
}