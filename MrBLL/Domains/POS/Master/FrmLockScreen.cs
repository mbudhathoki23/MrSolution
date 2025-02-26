using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using System;
using System.Windows.Forms;

namespace MrBLL.Domains.POS.Master;

public partial class FrmLockScreen : MrForm
{
    public sealed override string Text
    {
        get => base.Text;
        set => base.Text = value;
    }

    public FrmLockScreen(bool isUpdate = false, string module = "")
    {
        InitializeComponent();
        _isUpdate = isUpdate;
        _module = module;
        Text = module switch
        {
            "APIKEY" => "COMPANY API KEY LIST PASSWORD",
            "API" => "API CONFIG PASSWORD",
            "RECOVERY" => "DATABASE RESTORE PASSWORD",
            "RESET" => "DATABASE RESET PASSWORD",
            "DECRYPT" => "DECRYPT VALUE PASSWORD",
            _ => "LOCK SCREEN PASSWORD"
        };
    }

    private void BtnLogin_Click(object sender, EventArgs e)
    {
        if (EnterPasswordTime == 4)
        {
            DialogResult = DialogResult.No;
            MessageBox.Show(@"PLEASE RE-OPEN YOUR APPLICATION ..!!", ObjGlobal.Caption);
            Application.Exit();
        }

        if (_isUpdate)
        {
            if (TxtUserPassword.Text.Equals("UPD@T3"))
            {
                DialogResult = DialogResult.Yes;
                Close();
                return;
            }
            if (TxtUserPassword.IsValueExits())
            {
                MessageBox.Show(@"ENTER PASSWORD DOESN'T MATCH TO USER..!!", ObjGlobal.Caption);
                EnterPasswordTime += 1;
            }
        }
        else
        {
            if (_module.Equals("RESTORE"))
            {
                if (TxtUserPassword.Text.Equals("RESTORE@000"))
                {
                    DialogResult = DialogResult.Yes;
                    Close();
                    return;
                }
                else if (TxtUserPassword.IsValueExits())
                {
                    MessageBox.Show(@"ENTER PASSWORD DOESN'T MATCH TO USER..!!", ObjGlobal.Caption);
                    EnterPasswordTime += 1;
                }
            }
            else if (_module.Equals("RESET"))
            {
                if (TxtUserPassword.Text.Equals("RESET@111"))
                {
                    DialogResult = DialogResult.Yes;
                    Close();
                }
                else if (TxtUserPassword.IsValueExits())
                {
                    MessageBox.Show(@"ENTER PASSWORD DOESN'T MATCH TO USER..!!", ObjGlobal.Caption);
                    EnterPasswordTime += 1;
                }
            }
            else if (_module.Equals("API"))
            {
                if (TxtUserPassword.Text.Equals("API@321"))
                {
                    DialogResult = DialogResult.Yes;
                    Close();
                }
                else if (TxtUserPassword.IsValueExits())
                {
                    MessageBox.Show(@"ENTER PASSWORD DOESN'T MATCH TO USER..!!", ObjGlobal.Caption);
                    EnterPasswordTime += 1;
                }
            }
            else if (_module.Equals("APIKEY"))
            {
                if (TxtUserPassword.Text.Equals("APIKEY@321"))
                {
                    DialogResult = DialogResult.Yes;
                    Close();
                }
                else if (TxtUserPassword.IsValueExits())
                {
                    MessageBox.Show(@"ENTER PASSWORD DOESN'T MATCH TO USER..!!", ObjGlobal.Caption);
                    EnterPasswordTime += 1;
                }
            }
            else if (_module.Equals("DECRYPT"))
            {
                if (TxtUserPassword.Text.Equals("DECRYPT@321"))
                {
                    DialogResult = DialogResult.Yes;
                    Close();
                }
                else if (TxtUserPassword.IsValueExits())
                {
                    MessageBox.Show(@"ENTER PASSWORD DOESN'T MATCH TO USER..!!", ObjGlobal.Caption);
                    EnterPasswordTime += 1;
                }
            }
            else
            {
                if (ObjGlobal.LogInUserPassword.GetString().Equals(TxtUserPassword.Text.GetUpper()))
                {
                    Close();
                }
                else if (TxtUserPassword.IsValueExits())
                {
                    MessageBox.Show(@"ENTER PASSWORD DOESN'T MATCH TO USER..!!", ObjGlobal.Caption);
                    EnterPasswordTime += 1;
                }
            }
        }
    }

    private void TxtUserPassword_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            BtnLogin.Focus();
        }

        if (_module.IsValueExits())
        {
            if (e.KeyChar is (char)Keys.Escape)
            {
                if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
                {
                    Close();
                }
            }
        }
    }

    private void FrmLockScreen_Load(object sender, EventArgs e)
    {
    }

    //OBJECT
    private int EnterPasswordTime { get; set; }

    private readonly bool _isUpdate;
    private string _module;
}