using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ComboBox = System.Windows.Forms.ComboBox;

namespace MrDAL.Control.ControlsEx;

public class ClsMasterForm
{
    private readonly SimpleButton _btnExit;
    private readonly bool _upper;
    public Form FrmControl;

    public ClsMasterForm(Form ctrlFrm, SimpleButton btnExit, bool upper = false)
    {
        FrmControl = ctrlFrm;
        FrmControl.ShowIcon = false;
        FrmControl.KeyPreview = true;
        FrmControl.ShowInTaskbar = upper;
        FrmControl.KeyPress += _frmControl_KeyPress;
        ViewFormControl(null);
        FrmControl.Focus();
        FrmControl.BackColor = SystemColors.InactiveCaption;
        _btnExit = btnExit;
        _upper = upper;
    }

    private static void _frmControl_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    public void ViewFormControl(System.Windows.Forms.Control.ControlCollection ctrlDesc)
    {
        ctrlDesc ??= FrmControl.Controls;
        foreach (System.Windows.Forms.Control ctrl in ctrlDesc)
            if (ctrl != null)
                switch (ctrl)
                {
                    case Panel panel:
                        ViewAddFormControl(panel.Controls);
                        break;

                    case GroupBox box:
                        GroupControl(box.Controls);
                        break;

                    case TabControl control:
                        TabControl(control.Controls);
                        break;
                }
    }

    public void ViewAddFormControl(System.Windows.Forms.Control.ControlCollection ctrlDesc)
    {
        foreach (System.Windows.Forms.Control ctrl in ctrlDesc)
            if (ctrl != null)
                switch (ctrl)
                {
                    case GroupBox box:
                        GroupControl(box.Controls);
                        break;

                    case Panel panel:
                        AdditionalControl(panel.Controls);
                        break;

                    case TabControl control:
                        TabControl(control.Controls);
                        break;

                    case TextBox _:
                        ctrl.Enter += Ctrl_Enter;
                        ctrl.Leave += Ctrl_Leave;
                        break;

                    case MaskedTextBox _:
                        ctrl.Enter += Ctrl_Enter2;
                        ctrl.Leave += Ctrl_Leave2;
                        ctrl.Validating += Ctrl_Validating;
                        break;

                    case ComboBox _:
                        ctrl.Enter += Ctrl_Enter3;
                        ctrl.Leave += Ctrl_Leave3;
                        ctrl.KeyPress += Ctrl_KeyPress;
                        break;
                }
    }

    public void GroupControl(System.Windows.Forms.Control.ControlCollection CtrlDesc)
    {
        foreach (System.Windows.Forms.Control Ctrl in CtrlDesc)
        {
            switch (Ctrl)
            {
                case GroupBox box:
                    GroupControl(box.Controls);
                    break;

                case Panel panel:
                    AdditionalControl(panel.Controls);
                    break;

                case TabControl control:
                    TabControl(control.Controls);
                    break;

                case TextBox _:
                    Ctrl.Enter += Ctrl_Enter;
                    Ctrl.Leave += Ctrl_Leave;
                    break;

                case MaskedTextBox _:
                    Ctrl.Enter += Ctrl_Enter2;
                    Ctrl.Leave += Ctrl_Leave2;
                    Ctrl.Validating += Ctrl_Validating;
                    break;

                case ComboBox _:
                    Ctrl.Enter += Ctrl_Enter3;
                    Ctrl.Leave += Ctrl_Leave3;
                    Ctrl.KeyPress += Ctrl_KeyPress;
                    break;
            }

            switch (Ctrl)
            {
                case GroupBox box:
                    ViewAddFormControl(box.Controls);
                    break;

                case TabControl control:
                    ViewAddFormControl(control.Controls);
                    break;
            }
        }
    }

    public void TabControl(System.Windows.Forms.Control.ControlCollection CtrlDesc)
    {
        foreach (System.Windows.Forms.Control Ctrl in CtrlDesc)
            switch (Ctrl)
            {
                case TextBox _:
                    Ctrl.Enter += Ctrl_Enter;
                    Ctrl.Leave += Ctrl_Leave;
                    break;

                case Panel panel:
                    AdditionalControl(panel.Controls);
                    break;

                case MaskedTextBox _:
                    Ctrl.Enter += Ctrl_Enter2;
                    Ctrl.Leave += Ctrl_Leave2;
                    Ctrl.Validating += Ctrl_Validating;
                    break;

                case ComboBox _:
                    Ctrl.Enter += Ctrl_Enter3;
                    Ctrl.Leave += Ctrl_Leave3;
                    Ctrl.KeyPress += Ctrl_KeyPress;
                    break;

                case GroupBox box:
                    GroupControl(box.Controls);
                    break;

                case TabControl control:
                    TabControl(control.Controls);
                    break;
            }
    }

    public void AdditionalControl(System.Windows.Forms.Control.ControlCollection CtrlDesc)
    {
        foreach (System.Windows.Forms.Control Ctrl in CtrlDesc)
            if (Ctrl != null)
                switch (Ctrl)
                {
                    case TextBox _:
                        Ctrl.Enter += Ctrl_Enter;
                        Ctrl.Leave += Ctrl_Leave;
                        break;

                    case MaskedTextBox _:
                        Ctrl.Enter += Ctrl_Enter2;
                        Ctrl.Leave += Ctrl_Leave2;
                        Ctrl.Validating += Ctrl_Validating;
                        break;

                    case ComboBox _:
                        Ctrl.Enter += Ctrl_Enter3;
                        Ctrl.Leave += Ctrl_Leave3;
                        Ctrl.KeyPress += Ctrl_KeyPress;
                        break;

                    case GroupBox box:
                        GroupControl(box.Controls);
                        break;
                }
    }

    private static void Ctrl_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Space) SendKeys.Send("{F4}");
    }

    private static void Ctrl_Leave3(object sender, EventArgs e)
    {
        ((ComboBox)sender).BackColor = Color.AliceBlue;
    }

    private static void Ctrl_Enter3(object sender, EventArgs e)
    {
        ((ComboBox)sender).BackColor = Color.LightPink;
    }

    private static void Ctrl_Validating(object sender, CancelEventArgs e)
    {
    }

    private static void Ctrl_Leave2(object sender, EventArgs e)
    {
        ((MaskedTextBox)sender).BackColor = Color.AliceBlue;
    }

    private void Ctrl_Enter2(object sender, EventArgs e)
    {
        ((MaskedTextBox)sender).BackColor = Color.LightPink;
    }

    private static void Ctrl_Leave(object sender, EventArgs e)
    {
        ((TextBox)sender).BackColor = Color.AliceBlue;
    }

    private void Ctrl_Enter(object sender, EventArgs e)
    {
        ((TextBox)sender).BackColor = Color.LightPink;
        ((TextBox)sender).CharacterCasing = _upper ? CharacterCasing.Upper : CharacterCasing.Normal;
    }
}