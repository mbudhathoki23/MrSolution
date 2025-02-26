using MrBLL.Utility.Restore.Properties;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using System;
using System.Windows.Forms;

namespace MrBLL.Utility.Restore;

public partial class SqlConnectDialog : MrForm
{
    private readonly SqlServerInfo Info;

    public SqlConnectDialog(SqlServerInfo info)
    {
        Info = info;
        InitializeComponent();
    }

    private void BtnOk_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtServer.Text))
        {
            MessageBox.Show(@"Please select a SQL Server!", Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            return;
        }

        if (!SqlServer.IsLocalServer(txtServer.Text))
        {
            MessageBox.Show(@"Sorry, you can not restore to a remote SQL GetServer, only local. You have to run this tool on the same server where your target SQL GetServer is running.",
                Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
        }

        if (!TestConnection(out var str))
        {
            MessageBox.Show(str, @"SQL Server Connection", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            return;
        }

        Info.IntegratedSecurity = rbWinAuth.Checked;
        Info.Server = txtServer.Text.Trim();
        Info.UserName = txtUser.Text.Trim();
        Info.Password = txtPassword.Text.Trim();
        DialogResult = DialogResult.OK;
    }

    private void BtnTest_Click(object sender, EventArgs e)
    {
        if (!SqlServer.IsLocalServer(txtServer.Text))
        {
            MessageBox.Show(@"Sorry, you can not restore to a remote SQL GetServer, only local. You have to run this tool on the same server where your target SQL GetServer is running.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
        }

        if (TestConnection(out var str))
        {
            MessageBox.Show(@"Test connection succeeded", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            return;
        }
        MessageBox.Show(str, ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }

    private void FtpConnectDialog_Load(object sender, EventArgs e)
    {
        txtServer.Text = Info.Server;
        txtUser.Text = Info.UserName;
        txtPassword.Text = Info.Password;
        rbWinAuth.Checked = Info.IntegratedSecurity;
        rbSqlAuth.Checked = !Info.IntegratedSecurity;
        foreach (var localSqlServer in SqlServer.GetLocalSqlServers())
        {
            txtServer.Items.Add(localSqlServer);
        }

        if (txtServer.Items.Count > 0)
        {
            txtServer.SelectedIndex = txtServer.FindString(Info.Server);
        }
    }

    private void rbSqlAuth_CheckedChanged(object sender, EventArgs e)
    {
        var label = label2;
        var label1 = label3;
        var textBox = txtUser;
        var textBox1 = txtPassword;
        var @checked = rbSqlAuth.Checked;
        var flag = @checked;
        textBox1.Enabled = @checked;
        var flag1 = flag;
        var flag2 = flag1;
        textBox.Enabled = flag1;
        var flag3 = flag2;
        var flag4 = flag3;
        label1.Enabled = flag3;
        label.Enabled = flag4;
    }

    private bool TestConnection(out string error)
    {
        var sqlServerInfo = new SqlServerInfo
        {
            IntegratedSecurity = rbWinAuth.Checked,
            Server = txtServer.Text,
            UserName = txtUser.Text,
            Password = txtPassword.Text
        };
        Cursor.Current = Cursors.WaitCursor;
        error = "";
        var flag = SqlServer.TestConnection(sqlServerInfo, ref error, false);
        Cursor.Current = Cursors.Arrow;
        return flag;
    }
}