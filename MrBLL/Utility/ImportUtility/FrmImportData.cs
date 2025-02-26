using MrBLL.Setup.UserSetup;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using MrDAL.Utility.Config;
using MrDAL.Utility.Interface;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MrBLL.Utility.ImportUtility;

public partial class FrmImportData : MrForm
{
    // IMPORT FROM LOCAL
    #region --------------- IMPORT DATA ---------------

    public FrmImportData(string modelName)
    {
        InitializeComponent();
        _ModelName = modelName;
    }

    private void FrmImportData_Load(object sender, EventArgs e)
    {
        ClearControl();
        BindTransactionList();
        BindMasterList();
        ControlEnable();
        UploadServerInfo();
    }

    private void BtnConnect_Click(object sender, EventArgs e)
    {
        if (_ModelName.ToUpper() is "CLOUD")
        {
            var frm = new FrmOnlineUser();
            GetConnection.OnlineServerDesc = TxtServerInfo.Text;
            GetConnection.OnlineServerUserId = TxtServerUser.Text;
            GetConnection.OnlineServerUserPsw = TxtPassword.Text;
            if (GetConnection.CheckOnlineConnection())
            {
                _isConnect = true;
                MessageBox.Show(@"CONNECTION SUCCESSFULLY..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                _onlineUser = frm.OnlineLoginUser;
                _onlineUserId = frm.OnlineLoginUserId;
            }
            else
            {
                _objUtility.ImportLog.IsSuccess = true;
                _objUtility.SaveServerInfo();
                MessageBox.Show(@"ERROR OCCURS WHILE CONNECTION TO THE SERVER..!!", ObjGlobal.Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        else if (_ModelName.ToUpper() is "LOCAL")
        {
            if (CheckConnection())
            {
                _isConnect = true;
                MessageBox.Show(@"CONNECTION SUCCESSFULLY", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                TxtDatabase.Enabled = true;
                TxtDatabase.Focus();
            }
            else
            {
                MessageBox.Show(@"SERVER NOT FOUND", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                btnConnect.Focus();
            }
        }
        else if (_ModelName.ToUpper() is "SECONDARY")
        {
            var data = new ClsImport();
            var frm = new FrmOnlineUser();
            _isConnect = true;
            if (!frm.IsVerify) return;
            try
            {
                var result = SqlExtensions.ExecuteNonQuery(
                    "EXEC master.dbo.sp_addlinkedserver @server = N'link_MrSolution', @srvproduct=N'SQL GetServer''");
            }
            catch
            {
                // ignored
            }

            try
            {
                var result = SqlExtensions.ExecuteNonQuery(
                    $"EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname=N'{Server}',@useself=N'False',@locallogin=NULL,@rmtuser=N'mrsolution_db_user',@rmtpassword='{Password}''");
            }
            catch
            {
                // ignored
            }

            try
            {
                var result = SqlExtensions.ExecuteNonQuery(
                    $"EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname=N'{Server}',@useself=N'False',@locallogin=N'sa',@rmtuser=N'mrsolution_db_user',@rmtpassword='{Password}''");
            }
            catch
            {
                // ignored
            }

            MessageBox.Show(@"DATA IMPORT  SUCCESSFULLY..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }

    private void ChkCopyMaster_CheckedChanged(object sender, EventArgs e)
    {
        ChkListMaster.Enabled = ChkCopyMaster.Checked;
        TagMaster();
    }

    private void ChkTransaction_CheckedChanged(object sender, EventArgs e)
    {
        ChkListEntry.Enabled = ChkTransaction.Checked;
        TagTransaction();
    }

    private void BtnCompany_Click(object sender, EventArgs e)
    {
        if (_ModelName.Equals("LOCAL"))
        {
            var cmdTxt = $"SELECT gc.Database_Name LedgerId, gc.Company_Name Description FROM MASTER.AMS.GlobalCompany gc WHERE gc.Database_Name <> '{ObjGlobal.InitialCatalog}'";
            var fmComList = new FrmAutoPopList(cmdTxt);
            if (FrmAutoPopList.GetListTable.Rows.Count <= 0) return;
            fmComList.ShowDialog();
            if (fmComList.SelectedList.Count <= 0) return;
            TxtCompany.Text = fmComList.SelectedList[0]["Description"].ToString().Trim();
            TxtDatabase.Text = fmComList.SelectedList[0]["LedgerId"].ToString().Trim();
        }
        else if (_ModelName.Equals("SECONDARY"))
        {
            var cmdTxt =
                $"SELECT gc.Database_Name LedgerId, gc.Company_Name Description FROM [{TxtServerInfo.Text}].MASTER.AMS.GlobalCompany gc";
            var fmComList = new FrmAutoPopList(cmdTxt);
            if (FrmAutoPopList.GetListTable.Rows.Count <= 0) return;
            fmComList.ShowDialog();
            if (fmComList.SelectedList.Count <= 0) return;
            TxtCompany.Text = fmComList.SelectedList[0]["Description"].ToString().Trim();
            TxtDatabase.Text = fmComList.SelectedList[0]["LedgerId"].ToString().Trim();
        }
    }

    private void BtnUpdate_Click(object sender, EventArgs e)
    {
        if (CheckConnection())
        {
            UpdateData();
        }
    }

    private void BtnImport_Click(object sender, EventArgs e)
    {
        if (_isConnect)
        {
            _objUtility.ImportLog.ImportType = ObjGlobal.Encrypt(_ModelName);
            _objUtility.ImportLog.ImportDate = DateTime.Now;
            _objUtility.ImportLog.ServerDesc = ObjGlobal.Encrypt(TxtServerInfo.Text);
            _objUtility.ImportLog.ServerUser = ObjGlobal.Encrypt(TxtServerUser.Text);
            _objUtility.ImportLog.ServerPassword = ObjGlobal.Encrypt(TxtPassword.Text);
            _objUtility.ImportLog.dbInitial = ObjGlobal.Encrypt(TxtDatabase.Text);
            _objUtility.ImportLog.dbCompanyInfo = ObjGlobal.Encrypt(TxtCompany.Text);
            _objUtility.ImportLog.IsSuccess = true;
            var result = _objUtility.SaveServerInfo();
            BtnUpdate.Enabled = BtnUpdate.Visible = true;
            if (ImportData())
            {
                CustomMessageBox.Information("IMPORT DATA SUCCESSFULLY..!!");
            }
            else
            {
                CustomMessageBox.ErrorMessage("ERROR OCCURED WHILE DATA IMPORT. DO YOU WANT TO UPDATE..??");
            }
        }
        else
        {
            MessageBox.Show(@"PLEASE CLICK ON CONNECT OR CHECK YOUR CONNECTION..!!", ObjGlobal.Caption,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            _objUtility.ImportLog.IsSuccess = false;
            _objUtility.SaveServerInfo();
        }
    }

    private void TxtDatabase_KeyDown(object sender, KeyEventArgs e)
    {
        ClsKeyPreview.KeyEvent(e, "DELETE", TxtDatabase, BtnCompany);
    }

    private void TxtCompany_KeyDown(object sender, KeyEventArgs e)
    {
        ClsKeyPreview.KeyEvent(e, "DELETE", TxtDatabase, BtnCompany);
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
    }

    private void CkbSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        TagMaster();
        TagTransaction();
    }

    #endregion --------------- IMPORT DATA ---------------


    // METHOD FOR THIS FORM
    #region ---------------  METHOD ---------------

    private void UploadServerInfo()
    {
        if (_ModelName.ToUpper() is "CLOUD")
        {
            TxtPassword.PasswordChar = TxtServerUser.PasswordChar = TxtServerInfo.PasswordChar = 'X';
            //TxtServerInfo.Text = ObjGlobal.OnlineServerInfo;
            //TxtServerUser.Text = ObjGlobal.OnlineServerUser;
            TxtPassword.Text = ObjGlobal.ServerPassword;
        }
        else
        {
            var dtServer = _objUtility.GetServerInfo(_ModelName);
            if (dtServer.Rows.Count <= 0)
            {
                TxtServerInfo.Text = GetConnection.ServerDesc;
                TxtServerUser.Text = GetConnection.ServerUserId;
                TxtPassword.Text = GetConnection.ServerUserPsw;
                return;
            }

            TxtServerInfo.Text = dtServer.Rows[0]["ServerDesc"].ToString();
            TxtServerUser.Text = dtServer.Rows[0]["ServerUser"].ToString();
            TxtPassword.Text = dtServer.Rows[0]["ServerPassword"].ToString();
            TxtDatabase.Text = dtServer.Rows[0]["dbInitial"].ToString();
            TxtCompany.Text = dtServer.Rows[0]["dbCompanyInfo"].ToString();

            TxtServerInfo.Text = ObjGlobal.Decrypt(TxtServerInfo.Text);
            TxtServerUser.Text = ObjGlobal.Decrypt(TxtServerUser.Text);
            TxtPassword.Text = ObjGlobal.Decrypt(TxtPassword.Text);
            TxtDatabase.Text = ObjGlobal.Decrypt(TxtDatabase.Text);
            TxtCompany.Text = ObjGlobal.Decrypt(TxtCompany.Text);
        }
    }

    private void ClearControl()
    {
        BtnUpdate.Enabled = BtnUpdate.Visible = false;
        ChkListMaster.Enabled = ChkCopyMaster.Checked;
        ChkListEntry.Enabled = ChkTransaction.Checked;
    }

    private bool ImportData()
    {
        try
        {
            var data = new ClsImport();
            if (ChkCopyMaster.Checked && ChkListMaster.Items.Count > 0)
            {
                for (var i = 0; i < ChkListMaster.Items.Count; i++)
                {
                    ChkListMaster.SelectedIndex = i;
                    if (ChkListMaster.GetItemChecked(ChkListMaster.SelectedIndex))
                    {
                        var value = ChkListMaster.SelectedValue.ToString();
                        var server = TxtServerInfo.Text;
                        var database = TxtDatabase.Text;
                        var result = data.ImportMasterFromLocal(value, server, database);
                    }
                }
            }

            if (!ChkTransaction.Checked || ChkListEntry.Items.Count <= 0)
            {
                return true;
            }
            for (var i = 0; i < ChkListEntry.Items.Count; i++)
            {
                ChkListEntry.SelectedIndex = i;
                if (ChkListEntry.GetItemChecked(ChkListEntry.SelectedIndex))
                {
                    var value = ChkListEntry.SelectedValue.ToString();
                    var server = TxtServerInfo.Text;
                    var database = TxtDatabase.Text;
                    var result = data.ImportTransactionFromLocal(value, server, database);
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return false;
        }
    }

    private bool UpdateData()
    {
        try
        {
            var data = new ClsImport();
            if (ChkCopyMaster.Checked && ChkListMaster.Items.Count > 0)
            {
                for (var i = 0; i < ChkListMaster.Items.Count; i++)
                {
                    ChkListMaster.SelectedIndex = i;
                    if (ChkListMaster.GetItemChecked(ChkListMaster.SelectedIndex))
                    {
                        var result = data.UpdateMaster(ChkListMaster.SelectedValue.ToString(), Server, TxtDatabase.Text);
                    }
                }
            }

            if (ChkTransaction.Checked && ChkListEntry.Items.Count > 0)
            {
            }
            return true;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return false;
        }
    }

    private void BindTransactionList()
    {
        var dtTransaction = new DataTable();
        dtTransaction = dtTransaction.DataEntryImportModule();
        ChkListEntry.DataSource = dtTransaction;
        ChkListEntry.DisplayMember = "DESCRIPTION";
        ChkListEntry.ValueMember = "MODULE";
    }

    private void BindMasterList()
    {
        var table = new DataTable();
        table = table.MasterImportModule();
        ChkListMaster.DataSource = table;
        ChkListMaster.DisplayMember = "DESCRIPTION";
        ChkListMaster.ValueMember = "MODULE";
    }

    private void TagMaster()
    {
        if (ChkListMaster.Items.Count <= 0) return;
        for (var i = 0; i < ChkListMaster.Items.Count; i++)
            ChkListMaster.SetItemChecked(i, ChkCopyMaster.Checked && ckbSelectAll.Checked);
    }

    private void TagTransaction()
    {
        if (ChkListEntry.Items.Count <= 0) return;
        for (var i = 0; i < ChkListEntry.Items.Count; i++)
            ChkListEntry.SetItemChecked(i, ChkTransaction.Checked && ckbSelectAll.Checked);
    }

    private void ControlEnable()
    {
        ChkListMaster.Enabled = ChkCopyMaster.Checked;
        ChkListEntry.Enabled = ChkTransaction.Checked;
    }

    public bool CheckConnection()
    {
        try
        {
            var con = new SqlConnection(GetConnection.ConnectionStringMaster);
            if (con.State is ConnectionState.Open) con.Close();
            con.Open();
            return con.State == ConnectionState.Open;
        }
        catch
        {
            return false;
        }
    }

    #endregion ---------------  METHOD ---------------


    // OBJECT FOR THIS FORM

    #region --------------- Class ---------------
    public class ChkList
    {
        public ChkList(int s, string n, string v)
        {
            SNo = s;
            Name = n;
            Value = v;
        }

        public int SNo { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
    private readonly string _ModelName = string.Empty;
    private string _onlineUser = string.Empty;
    private int _onlineUserId;
    private int CompanyId;
    private bool _isConnect;
    private string Server, User, Password, initial = string.Empty;

    private void TxtServerInfo_TextChanged(object sender, EventArgs e)
    {

    }

    private void ChkListMaster_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private readonly IOnlineSync _objUtility = new ClsImport();

    #endregion -------------- OBJECT --------------

}