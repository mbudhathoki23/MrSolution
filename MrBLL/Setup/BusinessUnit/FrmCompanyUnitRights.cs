using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace MrBLL.Setup.BusinessUnit;

public partial class FrmCompanyUnitRights : MrForm
{
    public FrmCompanyUnitRights()
    {
        InitializeComponent();
    }

    private void FrmCompanyUnitRights_Load(object sender, EventArgs e)
    {
        ObjGlobal.DgvBackColor(dgv_CompanyUnitRights);
        BindUsers();
        var userId = cmb_User.SelectedIndex != -1 ? cmb_User.SelectedValue.GetInt() : 0;
        BindCompany(userId);
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
        dgv_CompanyUnitRights.DataSource = null;
        string query = @$"
            SELECT CASE WHEN ISNULL(br.BranchId,0) = 0 THEN 0 ELSE 1 END CmpChk,b.Branch_ID Branch_Id,b.CmpUnit_Name CmpUnit_Name FROM AMS.CompanyUnit b
	            LEFT OUTER JOIN 
				(
					SELECT DISTINCT br.BranchId FROM AMS.BranchRights br WHERE br.UserId = {loginUserId}
				) AS br ON b.Branch_ID = br.BranchId; ";
        var dtCompanyBind = GetConnection.SelectDataTableQuery(query);
        dgv_CompanyUnitRights.Rows.Clear();
        if (dtCompanyBind.Rows.Count > 0)
        {
            var iRows = 0;
            dgv_CompanyUnitRights.Rows.Clear();
            dgv_CompanyUnitRights.Rows.Add(dtCompanyBind.RowsCount());
            foreach (DataRow ro in dtCompanyBind.Rows)
            {
                dgv_CompanyUnitRights.Rows[iRows].Cells["IsCheck"].Value = ro["CmpChk"].GetInt() == 1;
                dgv_CompanyUnitRights.Rows[iRows].Cells["GtxtCompanyUnitId"].Value = ro["Branch_Id"].ToString();
                dgv_CompanyUnitRights.Rows[iRows].Cells["GtxtCompanyUnit"].Value = ro["CmpUnit_Name"].ToString();
                iRows++;
            }
            dgv_CompanyUnitRights.ClearSelection();
        }
        ObjGlobal.DgvBackColor(dgv_CompanyUnitRights);
    }
    private void cmb_User_SelectedValueChanged(object sender, EventArgs e)
    {
        if (ActiveControl != null)
        {
            BindCompany(cmb_User.SelectedValue.GetInt());
        }
    }
    private void cb_SelectAll_CheckedChanged(object sender, EventArgs e)
    {
        if (cb_SelectAll.Checked)
        {
            foreach (DataGridViewRow viewRow in dgv_CompanyUnitRights.Rows)
            {
                viewRow.Cells[0].Value = true;
            }
        }
        else
        {
            foreach (DataGridViewRow viewRow in dgv_CompanyUnitRights.Rows)
            {
                viewRow.Cells[0].Value = false;
            }
        }
    }
    private void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            var con = new SqlConnection(Convert.ToString(GetConnection.RollBackConMaster()));
            var msg = string.Empty;
            var returnId = string.Empty;
            con.Open();
            var cmd = con.CreateCommand();
            var transaction = con.BeginTransaction("SampleTransaction");
            cmd.Connection = con;
            cmd.Transaction = transaction;
            var timeout = TimeSpan.Zero;
            try
            {
                if (dgv_CompanyUnitRights.Rows.Count < 1) return;
                cmd.CommandText = "Delete From AMS.CompanyRights Where User_Id=@User_Id";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@User_Id", cmb_User.SelectedValue);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                _query = @" Insert Into ";
                _query += @" AMS.CompanyRights(User_Id,Company_Id,Company_Name,Start_AdDate,End_AdDate,Modify_Start_AdDate,Modify_End_AdDate,Back_Days_Restriction) ";
                _query += @" Values(@User_Id,@Company_Id,@Company_Name,@Start_AdDate,@End_AdDate,@Modify_Start_AdDate,@Modify_End_AdDate,@Back_Days_Restriction) ";
                var result = 0;
                var rows = dgv_CompanyUnitRights.Rows.Count;
                for (var i = 0; i < rows; i++)
                {
                    var isTrue = ObjGlobal.ReturnInt(dgv_CompanyUnitRights.Rows[i].Cells["gv_CmpName"].Value.ToString());
                    if (isTrue <= 0)
                    {
                        continue;
                    }
                    cmd.CommandText = _query;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@User_Id", cmb_User.SelectedValue);
                    cmd.Parameters.AddWithValue("@Company_Id", dgv_CompanyUnitRights.Rows[i].Cells[1].Value.ToString());
                    cmd.Parameters.AddWithValue("@Company_Name", dgv_CompanyUnitRights.Rows[i].Cells[2].Value.ToString());
                    cmd.Parameters.AddWithValue("@Start_AdDate", dgv_CompanyUnitRights.Rows[i].Cells[3].Value.GetSystemDate());
                    cmd.Parameters.AddWithValue("@End_AdDate", dgv_CompanyUnitRights.Rows[i].Cells[4].Value.GetSystemDate());
                    cmd.Parameters.AddWithValue("@Modify_Start_AdDate", dgv_CompanyUnitRights.Rows[i].Cells[5].Value.GetSystemDate());
                    cmd.Parameters.AddWithValue("@Modify_End_AdDate", dgv_CompanyUnitRights.Rows[i].Cells[6].Value.GetSystemDate());
                    cmd.Parameters.AddWithValue("@Back_Days_Restriction", dgv_CompanyUnitRights.Rows[i].Cells[7].Value.ToString());
                    result = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }

                if (result <= 0)
                {
                    return;
                }
                MessageBox.Show(@"RECORD INSERTED SUCCESSFULLY..!!", ObjGlobal.Caption);
                Close();
                return;
            }
            catch (Exception ex)
            {
                ex.ToNonQueryErrorResult(ex.StackTrace);
                transaction.Rollback();
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                transaction.Commit();
                con.Close();
            }
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            // ignored
        }
    }
    private void BtnCancel_Click(object sender, EventArgs e)
    {
        this.Close();
    }
    // OBJECT
    private IMasterSetup _master = new ClsMasterSetup();
    private string _query = string.Empty;

}