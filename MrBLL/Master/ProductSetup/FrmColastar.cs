using DevExpress.XtraEditors;
using MrDAL.Control.ControlsEx;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using MrDAL.Master.Interface;
using MrDAL.Master.Interface.ProductSetup;
using MrDAL.Master.ProductSetup;
using MrDAL.Utility.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Master.ProductSetup;

public partial class FrmColastar : MrForm
{
    #region---------------Global--------------

    private int ColasterId;

    private string ColasterDesc = string.Empty;
    private string _Query = string.Empty;
    private string _ActionTag = string.Empty;
    private string _SearchKey = string.Empty;

    private ObjGlobal GObj = new();
    private readonly DataTable dt = new();
    private ArrayList HeaderCap = new();
    private ArrayList ColumnWidths = new();

#pragma warning disable CS0414 // The field 'FrmColastar.ClsFormMaster' is assigned but its value is never used
    private ClsMasterForm ClsFormMaster = null;
#pragma warning restore CS0414 // The field 'FrmColastar.ClsFormMaster' is assigned but its value is never used
    private IColastarRepository _colastarRepository;

    private IMasterSetup _setup;

    #endregion

    #region---------------Frm-------------

    private ClsMasterForm clsFormControl;

    public FrmColastar()
    {
        InitializeComponent();
        Tag = string.Empty;
        clsFormControl = new ClsMasterForm(this, BtnExit);
        StartPosition = FormStartPosition.CenterParent;
        _colastarRepository = new ColastarRepository();
    }

    private void FrmColastar_Load(object sender, EventArgs e)
    {
        EnableControl();
        ClearControl();
        BtnNew.Focus();
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete, BtnView], this.Tag);
    }

    private void FrmColastar_KeyPress(object sender, KeyPressEventArgs e)
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
                        if (MessageBox.Show(@"DO YOU WANT TO CLEAR THE FORM..??", "ClearControl Form",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                        }
                    }
                    else
                    {
                        if (MessageBox.Show(@"DO YOU WANT TO CLOSE THE FORM..??", ObjGlobal.Caption,
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) Close();
                    }

                    break;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    #endregion

    #region---------------Method-------------

    private void EnableControl(bool Txt = false, bool Btn = true)
    {
        #region"Details"

        TxtDescription.Enabled = BtnSave.Enabled =
            BtnCancel.Enabled = !string.IsNullOrEmpty(_ActionTag) && _ActionTag != "SAVE" ? true : Txt;
        CmbSection.Enabled = Txt;
        ChkActive.Enabled = !string.IsNullOrEmpty(_ActionTag) && _ActionTag != "SAVE" ? true : false;

        #endregion "Details"

        #region "Button"

        BtnNew.Enabled = Btn;
        BtnEdit.Enabled = Btn;
        BtnDelete.Enabled = Btn;

        #endregion "Button"
    }

    private void ClearControl()
    {
        IList list = PanelHeader.Controls;
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
        TxtDescription.ReadOnly =
            !string.IsNullOrEmpty(_ActionTag) && _ActionTag.ToUpper() != "SAVE" ? true : false;
    }

    #endregion

    #region-------------Event-------------

    private void Global_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void TxtDescription_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyData == Keys.F1) BtnDescription_Click(sender, e);
        if (e.KeyCode == Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtDescription.Text.Trim()) && TxtDescription.Enabled is true &&
                TxtDescription.Focused is true)
            {
                MessageBox.Show(@"DESCRIPTION IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtDescription.Focus();
            }
        }
        else if (e.KeyCode is Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)(Keys.Enter)));
        }
        else
        {
            if (_ActionTag.ToUpper() != "SAVE" && TxtDescription.ReadOnly is true)
                ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                    e.KeyCode.ToString(), TxtDescription, BtnDescription);
        }
    }

    private void TxtDescription_Leave(object sender, EventArgs e)
    {
        if (ActiveControl != null && string.IsNullOrEmpty(TxtDescription.Text.Trim()) && TxtDescription.Focused &&
            !string.IsNullOrEmpty(_ActionTag))

        {
            MessageBox.Show(@"DESCRIPTION IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDescription.Focus();
        }
    }

    private void CmbSection_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape) SendKeys.Send("{f4}");
    }

    private void BtnDescription_Click(object sender, EventArgs e)
    {
        using var frmPickList = new FrmAutoPopList("MIN", "DEPARTMENT", ObjGlobal.SearchText, _ActionTag,
            string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
                if (!string.IsNullOrEmpty(_ActionTag) && _ActionTag != "SAVE")
                {
                    TxtDescription.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                    ColasterId = Convert.ToInt32(frmPickList.SelectedList[0]["Id"].ToString().Trim());
                    TxtDescription.ReadOnly = false;
                    SetGridDataToText(ColasterId);
                }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"COLASTAR NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtDescription.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        if (_ActionTag is "DELETE")
            BtnSave.Focus();
        else
            TxtDescription.Focus();
    }

    private void SetGridDataToText(int CID)
    {
        if (GetConnection.SelectDataTableQuery("Select *  from AMS.ColastarSetup  where CID=' " + CID + "' ").Rows
                .Count > 0)
        {
            TxtDescription.Text = dt.Rows[0]["CName"].ToString();
            CmbSection.Text = dt.Rows[0]["CSection"].ToString();
            if (_ActionTag == "UPDATE")
                TxtDescription.Focus();
            else
                BtnSave.Focus();
        }
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (_ActionTag == "SAVE")
        {
            if (GetConnection
                    .SelectDataTableQuery(
                        "Select * from AMS.Colastar where CName= '" + TxtDescription.Text.Trim() + "'").Rows.Count > 0)
            {
                MessageBox.Show(@"DESCRIPTION ALREADY EXIST..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtDescription.Focus();
                TxtDescription.SelectAll();
            }
        }
        else if (_ActionTag == "UPDATE")
        {
            if (GetConnection
                    .SelectDataTableQuery("Select * from AMS.Colastar where CName='" + TxtDescription.Text.Trim() + "'")
                    .Rows.Count > 0)
            {
                MessageBox.Show(@"DESCRIPTION ALREADY EXIST..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtDescription.Focus();
                TxtDescription.SelectAll();
            }
        }
    }

    #endregion

    #region-------------Button-------------

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _ActionTag = "SAVE";
        Text = "Colastar Setup [NEW]";
        EnableControl(true, false);
        ClearControl();
        TxtDescription.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _ActionTag = "UPDATE";
        Text = "Colastar Setup [UPDATE]";
        EnableControl(true, false);
        ClearControl();
        TxtDescription.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _ActionTag = "DELETE";
        Text = "Colastar Setup [DELETE]";
        EnableControl(false, false);
        ClearControl();
        TxtDescription.Focus();
    }

    private void BtnView_Click(object sender, EventArgs e)
    {
        BtnDescription_Click(sender, e);
        BtnView.Focus();
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        if (MessageBox.Show(@"DO YOU WANT TO CLOSE THE FORM..??", ObjGlobal.Caption, MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes) Close();
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (MessageBox.Show(@"DO YOU WANT TO CLOSE THE FORM..??", ObjGlobal.Caption, MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            Close();
        else
            ClearControl();
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (ISValidFormn() is true)
        {
            if (IUDColastarDetails() > 0)
            {
                MessageBox.Show($@"DATA {_ActionTag} SUCCESSFULLY..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                ClearControl();
                TxtDescription.Focus();
            }
            else
            {
                MessageBox.Show($@"DATA {_ActionTag} UNSUCCESSFULLY..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                TxtDescription.Focus();
            }
        }
    }

    private int IUDColastarDetails()
    {
        throw new NotImplementedException();
    }

    private bool ISValidFormn()
    {
        throw new NotImplementedException();
    }

    #endregion
}