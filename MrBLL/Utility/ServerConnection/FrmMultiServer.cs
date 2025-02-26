using MrDAL.Control.WinControl;
using MrDAL.Core.Utils;
using MrDAL.Global.Control;
using System;
using System.IO;
using System.Windows.Forms;
using static MrDAL.Global.Common.ObjGlobal;
using static MrDAL.Utility.dbMaster.AlterDatabaseTable;
using static MrDAL.Utility.dbMaster.CreateDatabaseTable;
using static MrDAL.Utility.Server.GetConnection;
using static System.Environment;

namespace MrBLL.Utility.ServerConnection;

public partial class FrmMultiServer : MrForm
{
    public FrmMultiServer(bool zoomFrm)
    {
        InitializeComponent();
        _isZoom = zoomFrm;
        //Size = _isZoom ? new Size(359, 399) : new Size(710, 399);
        ShowInTaskbar = !_isZoom;
        ShowIcon = !_isZoom;
    }

    private void FrmMultiServer_Load(object sender, EventArgs e)
    {
        ServerConnectionDetails();
        TxtServerInfo.Enabled = true;
    }

    private void FrmMultiServer_Shown(object sender, EventArgs e)
    {
        TxtServerInfo.Focus();
    }

    private void FrmMultiServer_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void BtnSaveServer_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtPassword.Text == string.Empty && TxtPassword.Enabled && TxtPassword.Focused)
            {
                TxtPassword.Focus();
                MessageBox.Show(@"ENTER SQL SERVER PASSWORD TO CONNECT..!!", Caption);
                return;
            }
            if (!string.IsNullOrEmpty(TxtServerInfo.Text))
            {
                DataSource = TxtServerInfo.Text.Trim();
                LogInUser = TxtUser.Text.Trim();
                ServerPassword = TxtPassword.Text.Trim();
                MultiServerOption = ChkMultiServer.Checked;

                InitialCatalog = string.Empty;
                ServerDesc = DataSource;
                ServerUserId = LogInUser;
                ServerUserPsw = ServerPassword;
                MultiServer = MultiServerOption;

                if (ConnectionCheck())
                {
                    if (_isZoom == false)
                    {
                        CreateMasterTable();
                        AlterMasterTable();
                        DefaultValueOnMasterDatabase();
                        var str = SaveSqlServerInfo(TxtServerInfo.Text.Trim(), TxtUser.Text.Trim(), TxtPassword.Text.Trim(), ChkMultiServer.Checked);
                    }
                    else
                    {
                        var connection = CurrentDirectory + @"\Connection.txt";
                        var file = new StreamWriter(connection);
                        TextWriter tw = file;
                        file.WriteLine($"{TxtServerInfo.Text.Trim()};{TxtUser.Text.Trim()};{TxtPassword.Text.Trim()};{ChkMultiServer.Checked}");
                        tw.Close();
                        Close();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(@"DATA SERVER NOT FOUND..!!", Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtServerInfo.Focus();
                    return;
                }
            }
            DialogResult = DialogResult.Yes;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            ex.DialogResult();
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (_isZoom)
        {
            Close();
        }
        else
        {
            if (MultiServerEdit == false)
            {
                Exit(0);
            }
            else Application.Exit();
        }
    }

    private void PictureBox1_Click(object sender, EventArgs e)
    {
    }

    private void BtnOnlineServer_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtSecondServerName.Text))
        {
        }
        else
        {
            MessageBox.Show(@"SERVER DESCRIPTION IS BLANK..!!", Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtSecondServerName.Focus();
        }
    }

    private void TxtServerInfo_Leave(object sender, EventArgs e)
    {
        if (ActiveControl is null || !TxtServerInfo.Focused ||
            !string.IsNullOrWhiteSpace(TxtServerInfo.Text)) return;
        MessageBox.Show(@"SERVER DESCRIPTION IS BLANK..!!", Caption, MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
        TxtServerInfo.Focus();
    }

    private void TxtUser_Leave(object sender, EventArgs e)
    {
        if (ActiveControl is null || !TxtUser.Focused || !string.IsNullOrWhiteSpace(TxtUser.Text)) return;
        MessageBox.Show(@"SERVER USER DESCRIPTION IS BLANK..!!", Caption, MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
        TxtServerInfo.Focus();
    }

    private void TxtPassword_Leave(object sender, EventArgs e)
    {
        if (ActiveControl is null || !TxtPassword.Focused || !string.IsNullOrWhiteSpace(TxtPassword.Text)) return;
        MessageBox.Show(@"SERVER USER DESCRIPTION IS BLANK..!!", Caption, MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
        TxtServerInfo.Focus();
    }

    // METHOD
    private void ServerConnectionDetails()
    {
        TxtServerInfo.Text = ServerDesc;
        TxtUser.Text = ServerUserId;
        TxtPassword.Text = ServerUserPsw;
        ChkMultiServer.Checked = ChkMultiServer.Visible = MultiServer;
    }

    // OBJECT

    #region --------------- Global Varaible ---------------

    private readonly bool _isZoom;

    #endregion --------------- Global Varaible ---------------
}