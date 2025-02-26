using System;
using System.Windows.Forms;
using MrShell.Common;

namespace MrShell;

public partial class FrmLogin : Form
{
    public FrmLogin()
    {
        InitializeComponent();
    }

    private void FrmLogin_Load(object sender, EventArgs e)
    {

    }

    private bool InputFieldsValid()
    {
        if (string.IsNullOrWhiteSpace(txtUsername.Text))
        {
            this.NotifyError("Enter username.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtPassword.Text))
        {
            this.NotifyError("Enter password.");
            return false;
        }

        return true;
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        if (!InputFieldsValid()) return;

        var mainForm = new FrmMain();
        mainForm.FormClosed += (_, _) =>
        {
            if (!mainForm.IsLoggedOut)
            {
                Application.Exit();
                return;
            }

            this.Show();
            this.BringToFront();
        };

        mainForm.Show();
        mainForm.BringToFront();
    }
}