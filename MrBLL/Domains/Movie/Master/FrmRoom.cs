using DevExpress.XtraEditors;
using MrDAL.Control.ControlsEx;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Domains.Movie.Master;

public partial class FrmRoom : MrForm
{
    #region "---------- Class ----------"

    private ClsMasterForm clsFormControl;
    private string _Tag { get; set; }

    #endregion "---------- Class ----------"

    #region "--------- Form ---------"

    public FrmRoom()
    {
        InitializeComponent();
        Tag = string.Empty;
        clsFormControl = new ClsMasterForm(this, BtnExit);
    }

    private void FrmRoom_Load(object sender, EventArgs e)
    {
        EnableDisable();
        ClearControl();
        BtnNew.Focus();
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }

    private void FrmRoom_KeyPress(object sender, KeyPressEventArgs e)
    {
        try
        {
            switch (e.KeyChar)
            {
                case (char)39:
                    e.KeyChar = '0';
                {
                    break;
                }
                case (char)14:
                {
                    break;
                }
                case (char)21:
                {
                    break;
                }
                case (char)4:
                {
                    break;
                }
                case (char)27:
                {
                    if (BtnNew.Enabled == false || BtnEdit.Enabled == false || BtnDelete.Enabled == false)
                    {
                        var dialogResult = MessageBox.Show(@"Are you sure want to ClearControl Form!", "ClearControl Form",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            Tag = string.Empty;
                            EnableDisable();
                            ClearControl();
                            Text = "Room Setup";
                            BtnNew.Focus();
                        }
                    }
                    else
                    {
                        var dialogResult = MessageBox.Show(@"Are you sure want to Close Form!", ObjGlobal.Caption,
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes) Close();
                    }

                    break;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
        }
    }

    #endregion "--------- Form ---------"

    #region "--------- Buttons ---------"

    private void btnNew_Click(object sender, EventArgs e)
    {
        Text = "Room Setup [NEW]";
        Tag = "NEW";
        _Tag = Tag.ToString();
        TxtNameReadOnly(_Tag);
        EnableDisable(true, false);
        ClearControl();
        TxtDescription.Focus();
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        Text = "Room Setup [UPDATE]";
        Tag = "UPDATE";
        _Tag = Tag.ToString();
        TxtNameReadOnly(_Tag);
        EnableDisable(true, false);
        ClearControl();
        TxtDescription.Focus();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        Text = "Room Setup [DELETE]";
        Tag = "DELETE";
        _Tag = Tag.ToString();
        TxtNameReadOnly(_Tag);
        EnableDisable(false, false);
        ChkActive.Enabled = false;
        TxtDescription.Enabled = true;
        BtnSave.Enabled = true;
        BtnCancel.Enabled = true;
        TxtDescription.Focus();
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
        var dialogResult = MessageBox.Show("Are you sure want to Close Form!", ObjGlobal.Caption,
            MessageBoxButtons.YesNo);
        if (dialogResult == DialogResult.Yes) Close();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        if (TxtDescription.Text.Trim() == string.Empty)
            Close();
        else
            ClearControl();
    }

    #endregion "--------- Buttons ---------"

    #region "--------- Methods ---------"

    private void EnableDisable(bool bt = false, bool MT = true)
    {
        #region "Details"

        TxtDescription.Enabled = bt;
        BtnSave.Enabled = bt;
        BtnCancel.Enabled = bt;
        ChkActive.Enabled = bt;

        #endregion "Details"

        #region "Buttons"

        BtnNew.Enabled = MT;
        BtnEdit.Enabled = MT;
        BtnDelete.Enabled = MT;
        BtnExit.Enabled = MT;

        #endregion "Buttons"
    }

    private void ClearControl()
    {
        IList list = StorePanel.Controls;
        for (var i = 0; i < list.Count; i++)
        {
            var control = (Control)list[i];
            if (control is TextBox)
            {
                control.Text = string.Empty;
                control.BackColor = SystemColors.Window;
            }
        }

        ChkActive.Checked = true;
        TxtNameReadOnly(Tag.ToString());
        TxtDescription.Focus();
    }

    private void TxtNameReadOnly(string _Tag)
    {
        TxtDescription.ReadOnly = _Tag.ToUpper() != "New" ? true : false;
    }

    #endregion "--------- Methods ---------"
}