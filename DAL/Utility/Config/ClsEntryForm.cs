using MrDAL.Global.Common;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MrDAL.Utility.Config;

public class ClsEntryForm
{
    private readonly Form _form;
    private readonly UserControl _userControl;

    public ClsEntryForm(Form activeForm, System.Windows.Forms.Control btnExit)
    {
        _form = activeForm;
        _form.Load += FormLoad;
        _form.KeyPress += FormKeyPress;
        btnExit.Click += BtnExit_Click;
        _form.Activate();
        ViewFormControl(null);
        _form.BringToFront();
    }

    public ClsEntryForm(UserControl userControl)
    {
        _userControl = userControl;
        _userControl.Load += FormLoad;
        _userControl.KeyPress += FormKeyPress;
        ViewFormControl(null);
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        if (MessageBox.Show(@"DO YOU WANT TO CLOSE THE FORM ..??", ObjGlobal.Caption, MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) is DialogResult.Yes) _form.Close();
    }

    private void FormKeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void FormLoad(object sender, EventArgs e)
    {
        if (_form != null)
            _form.StartPosition = FormStartPosition.CenterScreen;
    }

    public void ViewFormControl(System.Windows.Forms.Control.ControlCollection CtrlDesc)
    {
        if (_form != null)
            CtrlDesc ??= _form.Controls;
        else
            CtrlDesc ??= _userControl.Controls;

        foreach (System.Windows.Forms.Control ctrl in CtrlDesc)
            switch (ctrl)
            {
                case Panel panel:
                    {
                        ViewFormControl(panel.Controls);
                        break;
                    }
                case GroupBox groupBox:
                    {
                        ViewFormControl(groupBox.Controls);
                        break;
                    }
                case TextBox _:
                    {
                        ctrl.Enter += Ctrl_Enter;
                        ctrl.Leave += Ctrl_Leave;
                        break;
                    }
                case MaskedTextBox _:
                    {
                        ctrl.Enter += Ctrl_Enter2;
                        ctrl.Leave += Ctrl_Leave2;
                        ctrl.Validating += Ctrl_Validating;
                        break;
                    }
                case ComboBox _:
                    {
                        ctrl.Enter += Ctrl_Enter3;
                        ctrl.Leave += Ctrl_Leave3;
                        ctrl.KeyPress += Ctrl_KeyPress;
                        break;
                    }
                case TabControl control:
                    {
                        ViewFormControl(control.Controls);
                        break;
                    }
                case DataGridView _:
                    {
                        break;
                    }
            }
    }

    private void Ctrl_KeyPress1(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Space) SendKeys.Send("{F4}");
    }

    private void Ctrl_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Space) SendKeys.Send("{F4}");
    }

    private void Ctrl_Leave3(object sender, EventArgs e)
    {
        ((ComboBox)sender).BackColor = Color.AliceBlue;
    }

    private void Ctrl_Enter3(object sender, EventArgs e)
    {
        ((ComboBox)sender).BackColor = Color.LightPink;
    }

    private void Ctrl_Validating(object sender, CancelEventArgs e)
    {
        if (((MaskedTextBox)sender).Text == "  /  /    " || ((MaskedTextBox)sender).Text.Trim().Length == 0)
            MessageBox.Show(@"Field cann't Be Empty", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
    }

    private void Ctrl_Leave2(object sender, EventArgs e)
    {
        ((MaskedTextBox)sender).BackColor = Color.AliceBlue;
    }

    private void Ctrl_Leave1(object sender, EventArgs e)
    {
        ((ComboBox)sender).BackColor = Color.AliceBlue;
    }

    private void Ctrl_Enter2(object sender, EventArgs e)
    {
        ((MaskedTextBox)sender).BackColor = Color.LightPink;
    }

    private void Ctrl_Enter1(object sender, EventArgs e)
    {
        ((ComboBox)sender).BackColor = Color.LightPink;
    }

    private void Ctrl_Leave(object sender, EventArgs e)
    {
        ((TextBox)sender).BackColor = Color.AliceBlue;
    }

    private void Ctrl_Enter(object sender, EventArgs e)
    {
        ((TextBox)sender).BackColor = Color.LightPink;
    }
}