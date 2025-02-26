using DevExpress.XtraEditors;
using MrDAL.Control.ControlsEx;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MrBLL.Setup.UserSetup;

public partial class FrmUserRoleSetup : MrForm
{
    public FrmUserRoleSetup()
    {
        InitializeComponent();
        GetForm = new ClsMasterForm(this, BtnExit);
        BackColor = ObjGlobal.FrmBackColor();
    }

    #region --------------- Global Variable ---------------

    private int UserRoleId;
    private string Query = string.Empty;
    private string _ActionTag = string.Empty;

    private DataTable dt = new();
    private IMasterSetup ObjSetup = new ClsMasterSetup();
    private readonly ObjGlobal Gobj = new();
    private ClsMasterForm GetForm;

    #endregion --------------- Global Variable ---------------

    #region --------------- Frm ---------------

    private void FrmUserRoleSetup_Load(object sender, EventArgs e)
    {
        ClearControl();
        EnableDisable(true, false);
        //BtnNew.Focus();
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }

    private void GlobalKeyEvents_KeyPress(object sender, KeyPressEventArgs e)
    {
        //   if (e.KeyChar is (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void FrmUserRoleSetup_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (BtnNew.Enabled is false || BtnEdit.Enabled is false || BtnDelete.Enabled is false)
            {
                if (MessageBox.Show(@"ARE YOU SURE WANT TO CLEAR FORM..??", ObjGlobal.Caption,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _ActionTag = string.Empty;
                    ClearControl();
                    EnableDisable(true, false);
                    BtnNew.Focus();
                }
            }
            else
            {
                BtnExit.PerformClick();
            }
        }
    }

    #endregion --------------- Frm ---------------

    #region ------------- Botton -------------

    private void btn_New_Click(object sender, EventArgs e)
    {
        ClearControl();
        EnableDisable(false, true);
        TxtDescription.ReadOnly = false;
        _ActionTag = "NEW";
        TxtDescription.Focus();
        btn_Save.Text = "SAVE";
    }

    private void btn_Edit_Click(object sender, EventArgs e)
    {
        ClearControl();
        EnableDisable(false, true);
        _ActionTag = "UPDATE";
        TxtDescription.ReadOnly = true;
        TxtDescription.Focus();
        btn_Save.Text = "UPDATE";
    }

    private void btn_Delete_Click(object sender, EventArgs e)
    {
        EnableDisable(false, true);
        //btn_Save.Enabled = btn_Clear.Enabled = BtnDescription.Enabled = true;
        ClearControl();
        _ActionTag = "DELETE";
        TxtDescription.ReadOnly = true;
        TxtDescription.Focus();
        btn_Save.Text = "DELETE";
    }

    private void btn_Save_Click(object sender, EventArgs e)
    {
        if (TxtDescription.Text.Trim().Replace("'", "''") == string.Empty)
        {
            MessageBox.Show("ROLE IS NOT BLANK!");
            TxtDescription.Focus();
            return;
        }

        IUDUserRoleData();
    }

    private void btn_Clear_Click(object sender, EventArgs e)
    {
        ClearControl();
        if (_ActionTag == "NEW") TxtDescription.Focus();
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
    }

    #endregion ------------- Botton -------------

    #region ------------- Event -------------

    private void BtnDescription_Click(object sender, EventArgs e)
    {
        using var frmPickList = new FrmAutoPopList("MIN", "USER_ROLE", _ActionTag, ObjGlobal.SearchText,
            string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
                if (_ActionTag != "NEW")
                {
                    TxtDescription.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                    UserRoleId = Convert.ToInt32(frmPickList.SelectedList[0]["LedgerId"].ToString().Trim());
                    TxtDescription.ReadOnly = false;
                    bool.TryParse(frmPickList.SelectedList[0]["Status"].ToString(), out var _BatchNo);
                    ChkActive.Checked = _BatchNo;
                    //SetGridDataToText(UserRoleId);
                }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"USER NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtDescription.Focus();
            return;
        }

        TxtDescription.Focus();
    }

    private void txt_UnitName_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtDescription.Text.Trim()) && TxtDescription.Focused &&
            !string.IsNullOrEmpty(_ActionTag))
        {
            MessageBox.Show(@"DESCRIPTION IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDescription.Focus();
        }
    }

    private void txt_RoleName_KeyDown(object sender, KeyEventArgs e)
    {
        //if (ActionTag != "NEW" && e.KeyCode is Keys.F1) BtnDescription_Click(sender, e);
        if (e.KeyCode is Keys.F1)
        {
            BtnDescription_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtDescription.Text.Trim().Replace("'", "''")) &&
                TxtDescription.Focused is true)
                MessageBox.Show(@"DESCRIPTION IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
        }
        else if (TxtDescription.ReadOnly is true)
        {
            if (_ActionTag.ToUpper() != "SAVE" && TxtDescription.ReadOnly is true)
                ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                    e.KeyCode.ToString(), TxtDescription, BtnDescription);
        }
    }

    private void txt_RoleName_Validated(object sender, EventArgs e)
    {
        if (TxtDescription.Text != string.Empty)
        {
            //if (!string.IsNullOrEmpty(TxtDescription.Text.Trim().Replace("'", "''")) && ActionTag is "SAVE")
            //{
            //	TxtShortName.Text = ObjGlobal.BindAutoIncrementCode("UR", TxtDescription.Text.Replace("'", "''"));
            //}

            if (_ActionTag == "NEW")
            {
                Query = "SELECT Role_Id,Role,Status FROM AMS.User_Role Where Role ='" +
                        TxtDescription.Text.Replace("'", "''") + "'";
                dt.Reset();
                dt = GetConnection.SelectQueryFromMaster(Query);
                if (dt.Rows.Count > 0)
                {
                    TxtDescription.Text = string.Empty;
                    MessageBox.Show("ROLE NAME CANNOT BE DUPLICATE!");
                    TxtDescription.Focus();
                }
            }
            //else if (ActionTag.ToString() == "UPDATE")
            //{
            //    Query = "SELECT Role_Id,Role,Status FROM AMS.User_Role Where Role='" + TxtDescription.Text.Replace("'", "''") +
            //            "' and Role_Id <> " + Gobj.Id + string.Empty;
            //    ReportTable.Reset();
            //    ReportTable = GetConnection.SelectQueryFromMaster(Query);
            //    if (ReportTable.Rows.Count > 0)
            //    {
            //        TxtDescription.Text = string.Empty;
            //        MessageBox.Show("ROLE NAME CANNOT BE DUPLICATE!");
            //        TxtDescription.Focus();
            //    }
            //}
            else if (string.IsNullOrEmpty(TxtDescription.Text.Replace("'", "''")) &&
                     string.IsNullOrEmpty(_ActionTag) && TxtDescription.Focused)
            {
                MessageBox.Show("DESCRIPTION CAN'T LEFT NOT BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtDescription.Focus();
            }
        }
        else
        {
            if (TxtDescription.Enabled && TxtDescription.ReadOnly is false)
            {
                MessageBox.Show("ROLE NAME IS NOT BLANK!");
                TxtDescription.Focus();
            }
        }
    }

    private void txt_RoleCode_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtShortName, 'E');
    }

    private void txt_RoleCode_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtShortName, 'L');
    }

    private void txt_RoleCode_KeyDown(object sender, KeyEventArgs e)
    {
        //if (e.KeyCode == Keys.Enter) ChkActive.Focus();
    }

    private void txt_RoleCode_Validated(object sender, EventArgs e)
    {
        if (TxtShortName.Text.Replace("'", "''") != string.Empty)
        {
        }
        else
        {
            if (TxtShortName.Enabled)
            {
                MessageBox.Show("Role Code is not blank!");
                TxtShortName.Focus();
            }
        }
    }

    private void cb_Status_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) btn_Save.Focus();
    }

    #endregion ------------- Event -------------

    #region ------------- Method -------------

    private void IUDUserRoleData()
    {
        try
        {
            Query = string.Empty;
            Query = "BEGIN TRANSACTION  ";
            Query = Query + "BEGIN TRY ";
            if (_ActionTag == "NEW")
            {
                Query = Query + "  Insert into AMS.User_Role(Role,Status)";
                Query = Query + "  Values( ";
                Query = Query + "  '" + TxtDescription.Text.Trim().Replace("'", "''") + "',";
                //Query = Query + "  '" + txt_RoleCode.Text.Trim() + "',";
                if (ChkActive.Checked)
                    Query = Query + " 1 ";
                else
                    Query = Query + " 0 ";
                Query = Query + " )";
            }
            else if (_ActionTag == "UPDATE")
            {
                Query = Query + "  Update AMS.User_Role Set ";
                Query = Query + "  Role = '" + TxtDescription.Text.Trim().Replace("'", "''") + "',";
                //Query = Query + "  Code = '" + txt_RoleCode.Text.Trim() + "',";
                if (ChkActive.Checked)
                    Query = Query + " Status=1 ";
                else
                    Query = Query + " Status=0 ";
                Query = Query + " Where Role_Id = " + UserRoleId + " ";
            }
            else if (_ActionTag == "DELETE")
            {
                Query = Query + " Delete from AMS.User_Role Where Role_Id = " + UserRoleId + " ";
            }

            Query = Query + "  Commit Transaction";
            Query = Query + "  END TRY";
            Query = Query + "  BEGIN CATCH";
            Query = Query + "  ROLLBACK TRANSACTION";
            Query = Query + "  END CATCH ";
            var cmd = new SqlCommand(Query, GetConnection.GetConnectionMaster());
            var Result = cmd.ExecuteNonQuery();
            if (Result > 0)
            {
                if (_ActionTag == "NEW")
                    MessageBox.Show(@"Data Inserted Sucessfully", ObjGlobal.Caption);
                else if (_ActionTag == "UPDATE")
                    MessageBox.Show(@"Data Updated Sucessfully", ObjGlobal.Caption);
                else if (_ActionTag == "DELETE")
                    MessageBox.Show(@"Data Deleted Sucessfully", ObjGlobal.Caption);
                ClearControl();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }

    private void ClearControl()
    {
        TxtDescription.Clear();
        TxtShortName.Clear();
        ChkActive.Checked = true;
        btn_Save.Text = btn_Save.Text;
        TxtDescription.ReadOnly = false;
        TxtDescription.Focus();
    }

    private void SetGridDataToText(long Role_Id)
    {
        try
        {
            var dtb = new DataTable();
            Query = "SELECT * FROM Master.AMS.User_Role Where Role_Id='" + Role_Id + "'";
            dtb.Reset();
            dtb = GetConnection.SelectQueryFromMaster(Query);
            if (dtb.Rows.Count > 0)
            {
                foreach (DataRow ro in dtb.Rows)
                    //TxtShortName.Text = ro["RCode"].ToString();
                    btn_Save.Text = "Update";

                TxtDescription.Focus();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
        }
    }

    private void EnableDisable(bool bt, bool Txt)
    {
        TxtDescription.Enabled = BtnDescription.Enabled =
            TxtShortName.Enabled = btn_Save.Enabled = btn_Clear.Enabled = ChkActive.Enabled = Txt;
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = bt;
    }

    #endregion ------------- Method -------------
}