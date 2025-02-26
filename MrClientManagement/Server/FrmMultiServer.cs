using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Utility.Server;
using System;
using System.IO;
using System.Windows.Forms;
using static System.Environment;

namespace MrClientManagement.Server
{
    public partial class FrmMultiServer : Form
    {
        public FrmMultiServer(bool zoomFrm)
        {
            InitializeComponent();
            TxtServerInfo.Enabled = true;
            _isZoom = zoomFrm;
            ShowInTaskbar = !_isZoom;
            ShowIcon = !_isZoom;
        }

        private void FrmMultiServer_Load(object sender, EventArgs e)
        {
            ServerConnectionDetails();
            TxtServerInfo.Enabled = true;
            TxtServerInfo.Focus();
        }

        private void BtnSaveServer_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtPassword.Text == string.Empty && TxtPassword.Enabled && TxtPassword.Focused)
                {
                    TxtPassword.Focus();
                    MessageBox.Show(@"ENTER SQL SERVER PASSWORD TO CONNECT..!!", ObjGlobal.Caption);
                    return;
                }
                if (!string.IsNullOrEmpty(TxtServerInfo.Text))
                {
                    GetConnection.ServerDesc = TxtServerInfo.Text.Trim();
                    GetConnection.ServerUserId = TxtUser.Text.Trim();
                    GetConnection.ServerUserPsw = TxtPassword.Text.Trim();
                    GetConnection.MultiServer = ChkMultiServer.Checked;

                    if (GetConnection.ConnectionCheck())
                    {
                        if (!_isZoom)
                        {
                            var connection = CurrentDirectory + @"\Connection.txt";
                            var file = new StreamWriter(connection);
                            TextWriter tw = file;
                            file.WriteLine($"{TxtServerInfo.Text.Trim()};{TxtUser.Text.Trim()};{TxtPassword.Text.Trim()};{ChkMultiServer.Checked}");
                            tw.Close();
                            Close();
                            return;
                        }
                        else
                        {
                            Application.Run(new MainClientInformation());
                        }
                    }
                    else
                    {
                        MessageBox.Show(@"DATA SERVER NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TxtServerInfo.Focus();
                        return;
                    }
                }
                DialogResult = DialogResult.Yes;
            }
            catch (Exception ex)
            {
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
                Application.Exit();
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
                MessageBox.Show(@"SERVER DESCRIPTION IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtSecondServerName.Focus();
            }
        }

        private void TxtServerInfo_Leave(object sender, EventArgs e)
        {
            if (ActiveControl is null || !TxtServerInfo.Focused || !string.IsNullOrWhiteSpace(TxtServerInfo.Text))
            {
                return;
            }
            MessageBox.Show(@"SERVER DESCRIPTION IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            TxtServerInfo.Focus();
        }

        private void TxtUser_Leave(object sender, EventArgs e)
        {
            if (ActiveControl is null || !TxtUser.Focused || !string.IsNullOrWhiteSpace(TxtUser.Text))
            {
                return;
            }
            MessageBox.Show(@"SERVER USER DESCRIPTION IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            TxtServerInfo.Focus();
        }

        private void TxtPassword_Leave(object sender, EventArgs e)
        {
            if (ActiveControl is null || !TxtPassword.Focused || !string.IsNullOrWhiteSpace(TxtPassword.Text))
            {
                return;
            }
            MessageBox.Show(@"SERVER USER DESCRIPTION IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            TxtServerInfo.Focus();
        }

        // METHOD
        private void ServerConnectionDetails()
        {
            TxtServerInfo.Text = GetConnection.ServerDesc;
            TxtUser.Text = GetConnection.ServerUserId;
            TxtPassword.Text = GetConnection.ServerUserPsw;
            ChkMultiServer.Checked = ChkMultiServer.Visible = GetConnection.MultiServer;
        }

        // OBJECT

        #region --------------- Global Varaible ---------------

        private readonly bool _isZoom;

        #endregion --------------- Global Varaible ---------------
    }
}