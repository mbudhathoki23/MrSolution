using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Setup.UserSetup;

public partial class FrmMenuRights : MrForm
{
    private DataTable dt = new();
#pragma warning disable CS0414 // The field 'FrmMenuRights.parentNode' is assigned but its value is never used
    private TreeNode parentNode = null;
#pragma warning restore CS0414 // The field 'FrmMenuRights.parentNode' is assigned but its value is never used
    private string Query = string.Empty;

    public FrmMenuRights()
    {
        InitializeComponent();
        ObjGlobal.BindUserRoleAsync(cb_UserType);
        ObjGlobal.BindUser(cb_User);
        cb_MenuModule.SelectedIndex = -1;
        cb_User.Focus();
    }

    private void FrmMenuRights_Load(object sender, EventArgs e)
    {
        Top = 170;
        Left = 5;
        //ObjGlobal.ButtonRights(btn_Save, null, null, btn_Delete, null, null, null, null, null);
        BindProjectModule();
        cb_Module_SelectionChangeCommitted(sender, e);
        BindMenuRightsNew();
        GMenuRights.Columns[7].ReadOnly = true;
        GMenuRights.Columns[8].ReadOnly = true;
        GMenuRights.Columns[9].ReadOnly = true;
        GMenuRights.Columns[10].ReadOnly = true;
        GMenuRights.Columns[11].ReadOnly = true;
        GMenuRights.Columns[12].ReadOnly = true;
        GMenuRights.Columns[13].ReadOnly = true;
        GMenuRights.Columns[14].ReadOnly = true;
        GMenuRights.Columns[15].ReadOnly = true;
    }

    private void FrmMenuRights_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape)
            if (MessageBox.Show("Are you sure want to Close Form!", ObjGlobal.Caption, MessageBoxButtons.YesNo) ==
                DialogResult.Yes)
                Close();
    }

    private void cb_UserType_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (cb_UserType.Text != string.Empty)
            {
                cb_MenuModule.Focus();
            }
            else
            {
                MessageBox.Show("Select User Role !");
                cb_UserType.Focus();
            }
        }
    }

    private void cb_UserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMenuRightsNew();
    }

    private void cb_User_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (cb_User.Text != string.Empty)
            {
                cb_MenuModule.Focus();
            }
            else
            {
                MessageBox.Show("Select User !");
                cb_UserType.Focus();
            }
        }
    }

    private void cb_Module_SelectionChangeCommitted(object sender, EventArgs e)
    {
        if (cb_Module.SelectedValue.ToString() == "1")
            BindMenuModule("1");
        else if (cb_Module.SelectedValue.ToString() == "2")
            BindMenuModule("2");
        else if (cb_Module.SelectedValue.ToString() == "3") BindMenuModule("3");
    }

    private void cb_MenuModule_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (cb_MenuModule.Text != string.Empty)
            {
                GMenuRights.Focus();
            }
            else
            {
                MessageBox.Show("Select Menu Module !");
                cb_MenuModule.Focus();
            }
        }
    }

    private void cb_MenuModule_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void cb_MenuModule_SelectionChangeCommitted(object sender, EventArgs e)
    {
        //BindMenuRights(Convert.ToInt32(cb_MenuModule.SelectedValue));
        BindMenuRightsNew();
        GMenuRights.Focus();
    }

    private void dgv_MenuRights_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.ColumnIndex == 0) ActionbuttonReadOnly(e.RowIndex);
    }

    private void dgv_MenuRights_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.ColumnIndex == 0) ActionbuttonReadOnly(e.RowIndex);
    }

    private void btn_Save_Click(object sender, EventArgs e)
    {
        btn_Delete.Text = "&Delete1";
        InsertUserMenuRights();
        btn_Delete.Text = "&Delete";
    }

    private void cb_SelectAllMenu_CheckedChanged(object sender, EventArgs e)
    {
        if (cb_SelectAllMenu.Checked)
        {
            cb_SelectAllNew.Enabled = true;
            cb_SelectAllEdit.Enabled = true;
            cb_SelectAllDelete.Enabled = true;
            cb_SelectAllSave.Enabled = true;
            cb_SelectAllCopy.Enabled = true;
            for (var i = 0; i < GMenuRights.Rows.Count; i++) GMenuRights.Rows[i].Cells[0].Value = "1";
        }
        else
        {
            cb_SelectAllNew.Enabled = false;
            cb_SelectAllEdit.Enabled = false;
            cb_SelectAllDelete.Enabled = false;
            cb_SelectAllSave.Enabled = false;
            cb_SelectAllCopy.Enabled = false;
            for (var i = 0; i < GMenuRights.Rows.Count; i++) GMenuRights.Rows[i].Cells[0].Value = "0";
        }
    }

    private void cb_SelectAllNew_CheckedChanged(object sender, EventArgs e)
    {
        if (cb_SelectAllNew.Checked)
            for (var i = 0; i < GMenuRights.Rows.Count; i++)
                GMenuRights.Rows[i].Cells["txt_GNew"].Value = "1";
        else
            for (var i = 0; i < GMenuRights.Rows.Count; i++)
                GMenuRights.Rows[i].Cells["txt_GNew"].Value = "0";
    }

    private void cb_SelectAllEdit_CheckedChanged(object sender, EventArgs e)
    {
        if (cb_SelectAllEdit.Checked)
            for (var i = 0; i < GMenuRights.Rows.Count; i++)
                GMenuRights.Rows[i].Cells["txt_GEdit"].Value = "1";
        else
            for (var i = 0; i < GMenuRights.Rows.Count; i++)
                GMenuRights.Rows[i].Cells["txt_GEdit"].Value = "0";
    }

    private void cb_SelectAllDelete_CheckedChanged(object sender, EventArgs e)
    {
        if (cb_SelectAllDelete.Checked)
            for (var i = 0; i < GMenuRights.Rows.Count; i++)
                GMenuRights.Rows[i].Cells["txt_GDelete"].Value = "1";
        else
            for (var i = 0; i < GMenuRights.Rows.Count; i++)
                GMenuRights.Rows[i].Cells["txt_GDelete"].Value = "0";
    }

    private void cb_SelectAllSave_CheckedChanged(object sender, EventArgs e)
    {
        if (cb_SelectAllSave.Checked)
            for (var i = 0; i < GMenuRights.Rows.Count; i++)
                GMenuRights.Rows[i].Cells["txt_GSave"].Value = "1";
        else
            for (var i = 0; i < GMenuRights.Rows.Count; i++)
                GMenuRights.Rows[i].Cells["txt_GSave"].Value = "0";
    }

    private void cb_SelectAllCopy_CheckedChanged(object sender, EventArgs e)
    {
        if (cb_SelectAllCopy.Checked)
            for (var i = 0; i < GMenuRights.Rows.Count; i++)
                GMenuRights.Rows[i].Cells["txt_GCopy"].Value = "1";
        else
            for (var i = 0; i < GMenuRights.Rows.Count; i++)
                GMenuRights.Rows[i].Cells["txt_GCopy"].Value = "0";
    }

    #region Method

    private void InsertUserMenuRights()
    {
        try
        {
            var Gobj = new ObjGlobal();
            var sdate = string.Empty;
            Query = string.Empty;
            var Query1 = string.Empty;
            var Query2 = string.Empty;
            var row = 0;
            using (var cn = new SqlConnection(GetConnection.ConnectionStringMaster))
            {
                cn.Open();
                Query2 = string.Empty;
                Query = "BEGIN TRANSACTION  ";
                Query = Query + "BEGIN TRY ";
                if (GMenuRights.Rows.Count > 0)
                {
                    Query = Query + " Delete from AMS.Menu_Rights Where Role_Id=" + cb_UserType.SelectedValue +
                            " and Module_Id=" + cb_Module.SelectedValue + " and SubModule_Id=" +
                            cb_MenuModule.SelectedValue + string.Empty;
                    Query1 =
                        " Insert Into AMS.Menu_Rights(Role_Id,UserId,Menu_Id,Menu_Code,Menu_Name,Form_Name,Module_Id,SubModule_Id,[New],[Save],[Update],[Delete],Copy,Search,[Print],Approved,Reverse,Isparent,Created_By,Created_Date,Branch_Id,FiscalYear_Id) ";
                    for (var i = 0; i < GMenuRights.Rows.Count; i++)
                        if (GMenuRights.Rows[i].Cells["txt_GForm_Name"].Value != null)
                            if (GMenuRights.Rows[i].Cells[0].Value != null)
                                if (GMenuRights.Rows[i].Cells[0].Value.ToString() == "1")
                                {
                                    if (row > 0)
                                        Query2 = Query2 + " Union All ";
                                    Query2 = Query2 + "  Select " + cb_UserType.SelectedValue + " ,Null,";
                                    Query2 = Query2 + " '" + GMenuRights.Rows[i].Cells["txt_GMenu_Id"].Value + "',";
                                    Query2 = Query2 + " '" + GMenuRights.Rows[i].Cells["txt_GMenu_Code"].Value +
                                             "',";
                                    Query2 = Query2 + " '" + GMenuRights.Rows[i].Cells["txt_GMenu_Name"].Value +
                                             "',";
                                    Query2 = Query2 + " '" + GMenuRights.Rows[i].Cells["txt_GForm_Name"].Value +
                                             "', ";
                                    Query2 = Query2 + " " + cb_Module.SelectedValue + ",";
                                    Query2 = Query2 + " " + cb_MenuModule.SelectedValue + ",";
                                    Query2 = Query2 + " '" + GMenuRights.Rows[i].Cells["txt_GNew"].Value + "', ";
                                    Query2 = Query2 + " '" + GMenuRights.Rows[i].Cells["txt_GSave"].Value + "', ";
                                    Query2 = Query2 + " '" + GMenuRights.Rows[i].Cells["txt_GEdit"].Value + "', ";
                                    Query2 = Query2 + " '" + GMenuRights.Rows[i].Cells["txt_GDelete"].Value + "', ";
                                    Query2 = Query2 + " '" + GMenuRights.Rows[i].Cells["txt_GCopy"].Value + "', ";
                                    Query2 = Query2 + " '" + GMenuRights.Rows[i].Cells["txt_GSearch"].Value + "', ";
                                    Query2 = Query2 + " '" + GMenuRights.Rows[i].Cells["txt_GPrint"].Value + "', ";
                                    Query2 = Query2 + " '" + GMenuRights.Rows[i].Cells["txt_GApproved"].Value +
                                             "', ";
                                    Query2 = Query2 + " '" + GMenuRights.Rows[i].Cells["txt_GReverse"].Value +
                                             "', ";
                                    Query2 = Query2 + " '" + GMenuRights.Rows[i].Cells["txt_GIsparent"].Value +
                                             "', ";
                                    Query2 = Query2 + " " + ObjGlobal.LogInUserId + ", ";
                                    Query2 = Query2 + " '" +
                                             ObjGlobal.CurrentDate(DateTime.Now.ToShortDateString()) + " " +
                                             DateTime.Now.ToShortTimeString() + "' ,";
                                    Query2 = Query2 + " " + ObjGlobal.SysBranchId + ", ";
                                    Query2 = Query2 + " Null ";

                                    row = row + 1;
                                }
                }

                if (Query2 != string.Empty)
                    Query = Query + Query1 + Query2;
                Query = Query + "  Commit Transaction";
                Query = Query + "  END TRY";
                Query = Query + "  BEGIN CATCH";
                Query = Query + "  ROLLBACK TRANSACTION";
                Query = Query + "  END CATCH ";
                var cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                cmd.CommandText = Query;

                var Result = cmd.ExecuteNonQuery();
                if (Result > 0)
                {
                    MessageBox.Show(@"Data Inserted Sucessfully", "MR Solution");
                    cmd.Dispose();
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "MR Solution");
        }
    }

    private void BindMenuRightsNew()
    {
        try
        {
            GMenuRights.Rows.Clear();
            if (cb_Module.Text == "AMS Accounting" && cb_MenuModule.Text == "Company Manage")
            {
                //Company manage
                GMenuRights.Rows.Add();
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "MnuCompanyCreate";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Company";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "FrmCompanySetup";
                GMenuRights.Rows.Add();
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "MnuBranchCreate";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Branch";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "FrmBranchSetup";
                GMenuRights.Rows.Add();
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "MnuCompanyUnitCreate";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Company Unit";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "FrmCompanyUnitSetup";
                GMenuRights.Rows.Add();
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "MnuCompanyList";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Company List";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "FrmCompanyList";
                GMenuRights.Rows.Add();
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "MnuBranchList";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Branch List";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "FrmBranchList";
                GMenuRights.Rows.Add();
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "MnuCompanyUnitList";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Company Unit List";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "FrmCompanyUnitList";

                //User Master
                GMenuRights.Rows.Add();
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "MnuUserGroup";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "User Group";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "FrmUserRoleSetup";
                GMenuRights.Rows.Add();
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "MnuUserCreate";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "User Create";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "FrmUserSetup";
                GMenuRights.Rows.Add();
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "MnuSecurityRights";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Security Rights";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "FrmMenuRights";
                GMenuRights.Rows.Add();
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "MnuCompanyRights";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Company Rights";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "FrmCompanyRights";
                GMenuRights.Rows.Add();
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "MnuBranchRights";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Branch Rights";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "FrmBranchRights";
                GMenuRights.Rows.Add();
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "MnuCompanyUnitRights";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Unit Rights";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "FrmCompanyUnitRights";
                GMenuRights.Rows.Add();
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "MnuChangePassword";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Change Password";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "FrmChangePassword";

                // Document Numbering
                GMenuRights.Rows.Add();
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "MnuVoucherNumbering";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Document Numbering";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "FrmDocumentNumbering";
                GMenuRights.Rows.Add();
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "MnuVoucherReNumbering";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Document Re-Numbering";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "FrmDocumentReNumbering";
            }
            else if (cb_Module.Text == "AMS Accounting" && cb_MenuModule.Text == "Master")
            {
                GMenuRights.Rows.Add();
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "xxx";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "yyy";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "zzz";
            }
            else if (cb_Module.Text == "AMS Accounting" && cb_MenuModule.Text == "Entry")
            {
                GMenuRights.Rows.Add();
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "xxx";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "yyy";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "zzz";
            }
            else if (cb_Module.Text == "AMS Accounting" && cb_MenuModule.Text == "Finance")
            {
                GMenuRights.Rows.Add();
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "xxx";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "yyy";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "zzz";
            }
            else if (cb_Module.Text == "AMS Accounting" && cb_MenuModule.Text == "ARAP")
            {
                GMenuRights.Rows.Add();
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "xxx";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "yyy";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "zzz";
            }
            else if (cb_Module.Text == "AMS Accounting" && cb_MenuModule.Text == "Stock (Inventory)")
            {
                GMenuRights.Rows.Add();
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "xxx";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "yyy";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "zzz";
            }
            else if (cb_Module.Text == "AMS Accounting" && cb_MenuModule.Text == "Data Manage")
            {
                GMenuRights.Rows.Add();
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "xxx";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "yyy";
                GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "zzz";
            }
            else
            {
                GMenuRights.Rows.Clear();
            }

            if (GMenuRights.Rows.Count > 0)
            {
                var rows = GMenuRights.Rows.Count;
                for (var i = 0; i < rows; i++)
                {
                    GMenuRights.Rows[i].Cells["txt_GId"].Value = 0;
                    GMenuRights.Rows[i].Cells["txt_GMenu_Id"].Value = 0;
                    GMenuRights.Rows[i].Cells["txt_GSubModule_Id"].Value = cb_MenuModule.SelectedValue;
                    dt = GetConnection.SelectQueryFromMaster("Select * from AMS.Menu_Rights Where Role_Id=" +
                                                             Convert.ToString(cb_UserType.SelectedValue) +
                                                             " and Module_Id=" + cb_Module.SelectedValue +
                                                             " and SubModule_Id=" + cb_MenuModule.SelectedValue +
                                                             " and Menu_Name='" +
                                                             GMenuRights.Rows[i].Cells["txt_GMenu_Name"].Value +
                                                             " ' ");
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dt.Rows[0]["New"].ToString())) //---4
                            GMenuRights.Rows[i].Cells["txt_GNew"].Value = true;
                        else
                            GMenuRights.Rows[i].Cells["txt_GNew"].Value = false;
                        if (Convert.ToBoolean(dt.Rows[0]["Save"].ToString()))
                            GMenuRights.Rows[i].Cells["txt_GSave"].Value = true;
                        else
                            GMenuRights.Rows[i].Cells["txt_GSave"].Value = false;
                        if (Convert.ToBoolean(dt.Rows[0]["Update"].ToString()))
                            GMenuRights.Rows[i].Cells["txt_GEdit"].Value = true;
                        else
                            GMenuRights.Rows[i].Cells["txt_GEdit"].Value = false;
                        if (Convert.ToBoolean(dt.Rows[0]["Delete"].ToString()))
                            GMenuRights.Rows[i].Cells["txt_GDelete"].Value = true;
                        else
                            GMenuRights.Rows[i].Cells["txt_GDelete"].Value = false;
                        if (Convert.ToBoolean(dt.Rows[0]["Copy"].ToString()))
                            GMenuRights.Rows[i].Cells["txt_GCopy"].Value = true;
                        else
                            GMenuRights.Rows[i].Cells["txt_GCopy"].Value = false;
                        if (Convert.ToBoolean(dt.Rows[0]["Search"].ToString()))
                            GMenuRights.Rows[i].Cells["txt_GSearch"].Value = true;
                        else
                            GMenuRights.Rows[i].Cells["txt_GSearch"].Value = false;
                        if (Convert.ToBoolean(dt.Rows[0]["Print"].ToString()))
                            GMenuRights.Rows[i].Cells["txt_GPrint"].Value = true;
                        else
                            GMenuRights.Rows[i].Cells["txt_GPrint"].Value = false;
                        if (Convert.ToBoolean(dt.Rows[0]["Approved"].ToString()))
                            GMenuRights.Rows[i].Cells["txt_GApproved"].Value = true;
                        else
                            GMenuRights.Rows[i].Cells["txt_GApproved"].Value = false;
                        if (Convert.ToBoolean(dt.Rows[0]["Reverse"].ToString()))
                            GMenuRights.Rows[i].Cells["txt_GReverse"].Value = true;
                        else
                            GMenuRights.Rows[i].Cells["txt_GReverse"].Value = false;
                        GMenuRights.Rows[i].Cells["txt_GIsParent"].Value =
                            Convert.ToBoolean(dt.Rows[0]["IsParent"].ToString());
                    }
                    else
                    {
                        GMenuRights.Rows[i].Cells["txt_GNew"].Value = false;
                        GMenuRights.Rows[i].Cells["txt_GSave"].Value = false;
                        GMenuRights.Rows[i].Cells["txt_GEdit"].Value = false;
                        GMenuRights.Rows[i].Cells["txt_GDelete"].Value = false;
                        GMenuRights.Rows[i].Cells["txt_GCopy"].Value = false;
                        GMenuRights.Rows[i].Cells["txt_GSearch"].Value = false;
                        GMenuRights.Rows[i].Cells["txt_GPrint"].Value = false;
                        GMenuRights.Rows[i].Cells["txt_GApproved"].Value = false;
                        GMenuRights.Rows[i].Cells["txt_GReverse"].Value = false;
                    }
                }

                if (cb_MenuModule.Text == "Company Manage" || cb_MenuModule.Text == "Master")
                {
                    GMenuRights.Columns[7].Visible = true;
                    GMenuRights.Columns[8].Visible = true;
                    GMenuRights.Columns[9].Visible = true;
                    GMenuRights.Columns[10].Visible = true;
                    GMenuRights.Columns[11].Visible = false;
                    GMenuRights.Columns[12].Visible = false;
                    GMenuRights.Columns[13].Visible = false;
                }
                else if (cb_MenuModule.Text == "Entry")
                {
                    GMenuRights.Columns[7].Visible = true;
                    GMenuRights.Columns[8].Visible = true;
                    GMenuRights.Columns[9].Visible = true;
                    GMenuRights.Columns[10].Visible = true;
                    GMenuRights.Columns[11].Visible = true;
                    GMenuRights.Columns[12].Visible = true;
                    GMenuRights.Columns[13].Visible = true;
                }
                else if (cb_MenuModule.Text == "Finance" || cb_MenuModule.Text == "ARAP" ||
                         cb_MenuModule.Text == "Stock (Inventory)")
                {
                    GMenuRights.Columns[7].Visible = false;
                    GMenuRights.Columns[8].Visible = false;
                    GMenuRights.Columns[9].Visible = false;
                    GMenuRights.Columns[10].Visible = false;
                    GMenuRights.Columns[11].Visible = true;
                    GMenuRights.Columns[12].Visible = true;
                    GMenuRights.Columns[13].Visible = true;
                }
            }

            GMenuRights.ClearSelection();
            ObjGlobal.DgvBackColor(GMenuRights);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "MR Solution");
        }
    }

    private void BindProjectModule()
    {
        var list = new List<ValueModel<string, int>>
        {
            new("AMS ACCOUNTING", 1)
        };
        cb_Module.DataSource = list;
        cb_Module.DisplayMember = "DisplayMember";
        cb_Module.ValueMember = "ValueMember";
        cb_Module.SelectedIndex = 0;
    }

    public void BindMenuModule(string Module)
    {
        if (Module == "1")
        {
            var list = new List<ValueModel<string, int>>
            {
                new("Company Manage", 1),
                new("Master", 2),
                new("Entry", 3),
                new("Finance", 4),
                new("Register", 5),
                new("Stock (Inventory)", 6),
                new("Data Manage", 7)
            };
            cb_MenuModule.DataSource = list;
            cb_MenuModule.DisplayMember = "DisplayMember";
            cb_MenuModule.ValueMember = "ValueMember";
            cb_MenuModule.SelectedIndex = 0;
        }
        else if (Module == "2")
        {
            var list = new List<ObjGlobal>();
            cb_MenuModule.DataSource = list;
            cb_MenuModule.DisplayMember = "DisplayMember";
            cb_MenuModule.ValueMember = "ValueMember";
            cb_MenuModule.SelectedIndex = 0;
        }

        if (Module == "3")
        {
            var list = new List<ObjGlobal>();
            cb_MenuModule.DataSource = list;
            cb_MenuModule.DisplayMember = "DisplayMember";
            cb_MenuModule.ValueMember = "ValueMember";
            cb_MenuModule.SelectedIndex = 0;
        }
    }

    private void BindMenuRights(int Mast_Menu_Id)
    {
        try
        {
            dt.Reset();
            GMenuRights.Rows.Clear();
            dt = GetConnection.SelectQueryFromMaster("Select * from AMS.Menu_Rights Where Role_Id=" +
                                                     Convert.ToString(cb_UserType.SelectedValue) +
                                                     " and Module_Id=" +
                                                     cb_Module.SelectedValue + " and SubModule_Id=" +
                                                     cb_MenuModule.SelectedValue + " ");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow ro in dt.Rows)
                {
                    var rows = GMenuRights.Rows.Count;
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GId"].Value = ro["Id"].ToString();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Id"].Value =
                        ro["Menu_Id"].ToString();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value =
                        ro["Menu_Code"].ToString();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value =
                        ro["Menu_Name"].ToString();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value =
                        ro["Form_Name"].ToString();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GSubModule_Id"].Value =
                        ro["SubModule_Id"].ToString();
                    if (Convert.ToBoolean(ro["New"].ToString())) //---4
                        GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GNew"].Value = true;
                    else
                        GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GNew"].Value = false;
                    if (Convert.ToBoolean(ro["Save"].ToString()))
                        GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GSave"].Value = true;
                    else
                        GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GSave"].Value = false;
                    if (Convert.ToBoolean(ro["Update"].ToString()))
                        GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GEdit"].Value = true;
                    else
                        GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GEdit"].Value = false;
                    if (Convert.ToBoolean(ro["Delete"].ToString()))
                        GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GDelete"].Value = true;
                    else
                        GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GDelete"].Value = false;
                    if (Convert.ToBoolean(ro["Copy"].ToString()))
                        GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GCopy"].Value = true;
                    else
                        GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GCopy"].Value = false;
                    if (Convert.ToBoolean(ro["Search"].ToString()))
                        GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GSearch"].Value = true;
                    else
                        GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GSearch"].Value = false;
                    if (Convert.ToBoolean(ro["Print"].ToString()))
                        GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GPrint"].Value = true;
                    else
                        GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GPrint"].Value = false;

                    if (Convert.ToBoolean(ro["Approved"].ToString()))
                        GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GApproved"].Value = true;
                    else
                        GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GApproved"].Value = false;

                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GReverse"].Value =
                        Convert.ToBoolean(ro["Reverse"].ToString());

                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GIsParent"].Value =
                        Convert.ToBoolean(ro["IsParent"].ToString());
                    //if (Convert.ToString(ro["Parent"]) == "P")
                    //{
                    GMenuRights.Rows[rows].ReadOnly = true;
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Style.BackColor = Color.Gray;
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GNew"].Style.BackColor = Color.Gray;
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GSave"].Style.BackColor = Color.Gray;
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GEdit"].Style.BackColor = Color.Gray;
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GDelete"].Style.BackColor = Color.Gray;
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GCopy"].Style.BackColor = Color.Gray;
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GSearch"].Style.BackColor = Color.Gray;
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GSearch"].Style.BackColor = Color.Gray;
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GPrint"].Style.BackColor = Color.Gray;
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GApproved"].Style.BackColor = Color.Gray;
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GReverse"].Style.BackColor = Color.Gray;
                    //dgv_MenuRights.Rows[dgv_MenuRights.RowCount - 1].Cells[4].Value = Visible = false;
                    //}
                    GMenuRights.ClearSelection();
                    btn_Delete.Enabled = true;
                }

                ObjGlobal.DgvBackColor(GMenuRights);
            }
            else
            {
                if (cb_Module.Text == "AMS Accounting" && cb_MenuModule.Text == "Main")
                {
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem1";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Home";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = string.Empty;
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "btn_MnuUserRole";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "User Role";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "UserGroup";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem2";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "User Creation";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "UserMaster";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem3";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value =
                        "Company Information";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "LabInfo";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem4";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Log Off";
                    //dgv_MenuRights.Rows[dgv_MenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "LogOff";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem5";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Change Password";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "ChangePassword";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem6";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Close";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "Close";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem51";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "System Setting";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "SystemSetting";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "btn_mnuBackup";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Backup";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "Backup";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "btn_mnuRestore";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Restore";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "Restore";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "btn_MnuMenuRights";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Menu Rights";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "MenuRights";
                }
                else if (cb_Module.Text == "AMS Accounting" && cb_MenuModule.Text == "Master")
                {
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem7";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Branch";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "Branch";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem9";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Grade";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "LevelMaster";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem8";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Post";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "PostMaster";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem10";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Department";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "DepartmentMaster";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem11";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Shift";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "ShiftMaster";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem38";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Monthly Setup";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "MonthDateSetup";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem12";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Leave";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "LeaveMaster";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem13";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Head of Pay";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "HeadofPay";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem14";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Salary Structure";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "SalaryStructure";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem15";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Over Time";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "OTRateSetup";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem16";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Tax";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "Tax";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem17";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Employee";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "Employee";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem40";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Time Setting";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "TimeSetting";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem56";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Calender Setup";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "HolidaySetup";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem59";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Event";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "Events";
                }
                else if (cb_Module.Text == "AMS Accounting" && cb_MenuModule.Text == "Entry")
                {
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem52";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Leave Entry";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "LeaveEntry";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem19";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Manual Attendance";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "MonthlyAttendance";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem37";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Daily Attendance";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "DailyAttendance";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem53";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Import Attendance";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "AttendanceImport";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value =
                        "btn_MnuMachineEntry";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Machine Entry";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "MachineEntry";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value =
                        "btn_MnuManageDeviceEmp";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value =
                        "Manage Device Employee";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value =
                        "ManageDeviceEmployee";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value =
                        "btn_mnuDownloadAttendance";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value =
                        "Download Machine Attendance";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value =
                        "DownloadMachineAttendance";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem41";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Modify Attendance";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "ModifyAttendance";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem57";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Generate Salary";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "GenerateSalary";
                    //dgv_MenuRights.Rows.Add();
                    //dgv_MenuRights.Rows[dgv_MenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "btn_MnuClearMchAttenLog";
                    //dgv_MenuRights.Rows[dgv_MenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "ClearControl Machine Attendance Log";
                    //dgv_MenuRights.Rows[dgv_MenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "ClearMachineAttendance";
                }
                else if (cb_Module.Text == "AMS Accounting" && cb_MenuModule.Text == "Report")
                {
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem28";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "View";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "SpecialReport";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem35";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Report";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value =
                        "PayrollMonthlyReports";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem54";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Attendance Report";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "AttendanceReport";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "LeaveReport";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Leave Report";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "LeaveReport";
                    GMenuRights.Rows.Add();
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Code"].Value = "barButtonItem55";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GMenu_Name"].Value = "Salary Slip Repor";
                    GMenuRights.Rows[GMenuRights.RowCount - 1].Cells["txt_GForm_Name"].Value = "SalaryslipReport";
                }
                else
                {
                    GMenuRights.Rows.Clear();
                }

                if (GMenuRights.Rows.Count > 0)
                {
                    var rows = GMenuRights.Rows.Count;
                    for (var i = 0; i < rows; i++)
                    {
                        GMenuRights.Rows[i].Cells["txt_GId"].Value = 0;
                        GMenuRights.Rows[i].Cells["txt_GMenu_Id"].Value = 0;
                        GMenuRights.Rows[i].Cells["txt_GSubModule_Id"].Value = cb_MenuModule.SelectedValue;
                        GMenuRights.Rows[i].Cells["txt_GNew"].Value = false;
                        GMenuRights.Rows[i].Cells["txt_GSave"].Value = false;
                        GMenuRights.Rows[i].Cells["txt_GEdit"].Value = false;
                        GMenuRights.Rows[i].Cells["txt_GDelete"].Value = false;
                        GMenuRights.Rows[i].Cells["txt_GCopy"].Value = false;
                        GMenuRights.Rows[i].Cells["txt_GSearch"].Value = false;
                        GMenuRights.Rows[i].Cells["txt_GPrint"].Value = false;
                        GMenuRights.Rows[i].Cells["txt_GApproved"].Value = false;
                        GMenuRights.Rows[i].Cells["txt_GReverse"].Value = false;
                    }
                }

                GMenuRights.ClearSelection();
                ObjGlobal.DgvBackColor(GMenuRights);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "MR Solution");
        }
    }

    private void ActionbuttonReadOnly(int rowindex)
    {
        try
        {
            if (GMenuRights.Rows[rowindex].Cells[0].Value == null)
            {
                GMenuRights.Rows[rowindex].Cells[7].ReadOnly = false;
                GMenuRights.Rows[rowindex].Cells[8].ReadOnly = false;
                GMenuRights.Rows[rowindex].Cells[9].ReadOnly = false;
                GMenuRights.Rows[rowindex].Cells[10].ReadOnly = false;
                GMenuRights.Rows[rowindex].Cells[11].ReadOnly = false;
                GMenuRights.Rows[rowindex].Cells[12].ReadOnly = false;
                GMenuRights.Rows[rowindex].Cells[13].ReadOnly = false;
                GMenuRights.Rows[rowindex].Cells[14].ReadOnly = false;
                GMenuRights.Rows[rowindex].Cells[15].ReadOnly = false;
            }
            else if (GMenuRights.Rows[rowindex].Cells[0].Value.ToString() == "0")
            {
                GMenuRights.Rows[rowindex].Cells[7].ReadOnly = false;
                GMenuRights.Rows[rowindex].Cells[8].ReadOnly = false;
                GMenuRights.Rows[rowindex].Cells[9].ReadOnly = false;
                GMenuRights.Rows[rowindex].Cells[10].ReadOnly = false;
                GMenuRights.Rows[rowindex].Cells[11].ReadOnly = false;
                GMenuRights.Rows[rowindex].Cells[12].ReadOnly = false;
                GMenuRights.Rows[rowindex].Cells[13].ReadOnly = false;
                GMenuRights.Rows[rowindex].Cells[14].ReadOnly = false;
                GMenuRights.Rows[rowindex].Cells[15].ReadOnly = false;
            }
            else
            {
                GMenuRights.Rows[rowindex].Cells[7].ReadOnly = true;
                GMenuRights.Rows[rowindex].Cells[8].ReadOnly = true;
                GMenuRights.Rows[rowindex].Cells[9].ReadOnly = true;
                GMenuRights.Rows[rowindex].Cells[10].ReadOnly = true;
                GMenuRights.Rows[rowindex].Cells[11].ReadOnly = true;
                GMenuRights.Rows[rowindex].Cells[12].ReadOnly = true;
                GMenuRights.Rows[rowindex].Cells[13].ReadOnly = true;
                GMenuRights.Rows[rowindex].Cells[14].ReadOnly = true;
                GMenuRights.Rows[rowindex].Cells[15].ReadOnly = true;

                GMenuRights.Rows[rowindex].Cells[7].Value = "0";
                GMenuRights.Rows[rowindex].Cells[8].Value = "0";
                GMenuRights.Rows[rowindex].Cells[9].Value = "0";
                GMenuRights.Rows[rowindex].Cells[10].Value = "0";
                GMenuRights.Rows[rowindex].Cells[11].Value = "0";
                GMenuRights.Rows[rowindex].Cells[12].Value = "0";
                GMenuRights.Rows[rowindex].Cells[13].Value = "0";
                GMenuRights.Rows[rowindex].Cells[14].Value = "0";
                GMenuRights.Rows[rowindex].Cells[15].Value = "0";
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "MR Solution");
        }
    }

    #endregion Method

    private void btn_Delete_Click(object sender, EventArgs e)
    {

    }
}