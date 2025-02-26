using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Windows.Forms;
namespace MrBLL.Setup.BranchSetup;

public partial class FrmBranchRights : MrForm
{
    // BRANCH RIGHTS
    public FrmBranchRights()
    {
        InitializeComponent();
    }

    private void FrmBranchRights_Load(object sender, EventArgs e)
    {
        ObjGlobal.DgvBackColor(SGrid);
        BindUsers();
        var userId = cmb_User.SelectedIndex != -1 ? cmb_User.SelectedValue.GetInt() : 0;
        BindCompany(userId);
    }

    private void ChkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkSelectAll.Checked)
        {
            foreach (DataGridViewRow viewRow in SGrid.Rows)
            {
                viewRow.Cells[0].Value = true;
            }
        }
        else
        {
            foreach (DataGridViewRow viewRow in SGrid.Rows)
            {
                viewRow.Cells[0].Value = false;
            }
        }
    }

    private void CmbUser_SelectedValueChanged(object sender, EventArgs e)
    {
        if (ActiveControl != null)
        {
            BindCompany(cmb_User.SelectedValue.GetInt());
        }
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        //var returnInt = cmb_User.SelectedValue.GetInt();
        //_master.GetUser.User_Id = returnInt;
        //for (var i = 0; i < SGrid.Rows.Count; i++)
        //{
        //    var check = SGrid.Rows[i].Cells["IsCheck"].Value.GetBool();
        //    if (!check)
        //    {
        //        continue;
        //    }
        //    var getBranch = new BranchRights
        //    {
        //        UserId = (int)cmb_User.SelectedValue,
        //        BranchId = SGrid.Rows[i].Cells["GTxtBranchId"].Value.GetInt(),
        //        Branch = SGrid.Rows[i].Cells["GTxtBranch"].Value.ToString()
        //    };
        //    _master.GetRights.Add(getBranch);
        //}

        //if (_master.GetRights.Count == 0)
        //{
        //    MessageBox.Show(@"PLEASE SELECT THE BRANCH..!!", ObjGlobal.Caption);
        //    SGrid.Focus();
        //    return;

        //}
        //if (_master.SaveBranchRight("SAVE") <= 0)
        //{
        //    return;
        //}
        //MessageBox.Show(@"BRANCH RIGHTS SAVE SUCCESSFULLY..!!", ObjGlobal.Caption);
        //Close();
    }

    // METHOD FOR THIS FORM
    public void BindUsers()
    {
        var dt1 = _master.GetMasterUser();
        if (dt1.Rows.Count <= 0)
        {
            return;
        }
        cmb_User.DataSource = dt1;
        cmb_User.DisplayMember = "User_Name";
        cmb_User.ValueMember = "User_Id";
        cmb_User.SelectedIndex = 0;
    }

    public void BindCompany(int loginUserId)
    {
        SGrid.DataSource = null;
        string query = @$"
            SELECT CASE WHEN ISNULL(br.BranchId,0) = 0 THEN 0 ELSE 1 END CmpChk,b.Branch_ID Branch_Id,b.Branch_Name Branch_Name FROM AMS.Branch b
	            LEFT OUTER JOIN 
				(
					SELECT DISTINCT br.BranchId FROM AMS.BranchRights br WHERE br.UserId = {loginUserId}
				) AS br ON b.Branch_ID = br.BranchId; ";
        var dtCompanyBind = GetConnection.SelectDataTableQuery(query);
        SGrid.Rows.Clear();
        if (dtCompanyBind.Rows.Count > 0)
        {
            var iRows = 0;
            SGrid.Rows.Clear();
            SGrid.Rows.Add(dtCompanyBind.RowsCount());
            foreach (DataRow ro in dtCompanyBind.Rows)
            {
                SGrid.Rows[iRows].Cells["IsCheck"].Value = ro["CmpChk"].GetInt() == 1;
                SGrid.Rows[iRows].Cells["GTxtBranchId"].Value = ro["Branch_Id"].ToString();
                SGrid.Rows[iRows].Cells["GTxtBranch"].Value = ro["Branch_Name"].ToString();
                iRows++;
            }
            SGrid.ClearSelection();
        }
        ObjGlobal.DgvBackColor(SGrid);
    }

    // OBJECT
    private IMasterSetup _master = new ClsMasterSetup();

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        this.Close();
    }
}