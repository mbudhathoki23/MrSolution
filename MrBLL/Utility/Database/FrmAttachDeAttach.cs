using Microsoft.SqlServer.Management.Smo;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Utility.Server;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace MrBLL.Utility.Database;

public partial class FrmAttachDeAttach : MrForm
{
    private string[] _dbName;

    [Obsolete]
    public FrmAttachDeAttach()
    {
        InitializeComponent();
    }

    private void FrmAttachDeAttach_Load(object sender, EventArgs e)
    {
        BindCompany();
    }

    private void FrmAttachDeAttach_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (CustomMessageBox.ExitActiveForm() is DialogResult.Yes)
            {
                Close();
            }
        }
    }

    [Obsolete]
    private void DriveFolder_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            dListDrive.Path = DriveFolder.Drive;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    [Obsolete]
    private void dListDrive_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (FbFolderLocation == null) return;
            Debug.Assert(dListDrive != null, nameof(dListDrive) + " != null");
            FbFolderLocation.Path = dListDrive.Path;
        }
        catch (Exception ex)
        {
            ex.DialogResult();
        }
    }

    private void FbFolderLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    #region ------------- Method -------------

    private void BindCompany()
    {
        var Query = ObjGlobal.LogInUser.ToUpper() is "ADMIN"
            ? $"SELECT * FROM AMS.GlobalCompany gc WHERE gc.Company_Name <> '{ObjGlobal.LogInCompany}' Order By Company_Name"
            : $"SELECT cr.Company_Name,gc.Database_Name FROM AMS.CompanyRights cr LEFT OUTER JOIN AMS.GlobalCompany gc ON cr.Company_Name = gc.Company_Name WHERE cr.User_Id='{ObjGlobal.LogInUserId}' AND cr.Company_Name <> '{ObjGlobal.LogInCompany}' Order By Company_Name";
        var dt = GetConnection.SelectQueryFromMaster(Query);
        _dbName = new string[dt.Rows.Count];
        var i = 0;
        LstViewCompany.Clear();
        for (var c = 0; c < dt.Rows.Count; c++)
        {
            LstViewCompany.Items.Add(dt.Rows[c]["Company_Name"].ToString());
            _dbName[c] = dt.Rows[c]["Database_Name"].ToString();
            i += 1;
        }
    }

    #endregion ------------- Method -------------

    [Obsolete]
    private void BtnSave_Click(object sender, EventArgs e)
    {
        var fileName = string.Empty;
        //FileSystemObject fso = new FileSystemObject();
        var type = Type.GetTypeFromProgID("Scripting.FileSystemObject");
        dynamic instance = Activator.CreateInstance(type);

        if (RBtnAttach.Checked)
        {
            var iResult = 0;
            var path = dListDrive.Path + "\\";
            var fileNameLength = FbFolderLocation.FileName.Length;
            fileNameLength -= 4;
            var query = $"EXEC sp_attach_db @dbname = N'{FbFolderLocation.FileName.Substring(0, fileNameLength)}', @filename1 = N'{path}{FbFolderLocation.FileName.Substring(0, fileNameLength)}.mdf', @filename2 = N'{path}{FbFolderLocation.FileName.Substring(0, fileNameLength)}Log.ldf'";
            try
            {
                iResult = SqlExtensions.ExecuteNonQuery(query);
                //ClsServerConnection.ExecuteNonQueryOnMaster(Query);
            }
            catch
            {
                // ignored
            }

            if (iResult is 0)
            {
                try
                {
                    var iEmpty = $@"
						CREATE DATABASE [{FbFolderLocation.FileName.Substring(0, fileNameLength)}]
						ON PRIMARY (FILENAME = N'{path}{FbFolderLocation.FileName.Substring(0, fileNameLength)}.mdf')
						LOG ON (FILENAME =N'{path}{FbFolderLocation.FileName.Substring(0, fileNameLength)}.ldf')
						FOR ATTACH";
                    var ok = SqlExtensions.ExecuteNonQuery(iEmpty);
                }
                catch (Exception ex)
                {
                    try
                    {
                        var getCon = GetConnection.GetConnectionMaster();
                        var conserve2 = new Microsoft.SqlServer.Management.Common.ServerConnection
                        {
                            ConnectionString = getCon.ConnectionString
                        };
                        var s1 = new Server(conserve2);
                        s1.AttachDatabase($"{FbFolderLocation.FileName.Substring(0, fileNameLength)}", new StringCollection
                        {
                            $@"{path}{FbFolderLocation.FileName.Substring(0, fileNameLength)}.mdf",
                            $@"{path}{FbFolderLocation.FileName.Substring(0, fileNameLength)}_log.ldf"
                        }, AttachOptions.None);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                        MessageBox.Show(ex.Message, ObjGlobal.Caption);
                        return;
                    }
                }
            }

            var dtCmpInfo = GetConnection.SelectQueryFromMaster($"Select * from {FbFolderLocation.FileName.Substring(0, fileNameLength)}.AMS.CompanyInfo");
            if (dtCmpInfo.Rows.Count <= 0) return;
            var dtTest = GetConnection.SelectQueryFromMaster($"Select * from AMS.GlobalCompany Where DataBase_Name = '{dtCmpInfo.Rows[0]["DataBase_Name"]}'");
            if (dtTest.Rows.Count >= 1) return;

            query = "INSERT INTO AMS.GlobalCompany (Company_Name, Database_Path, Database_Name, Status, PanNo, Address, CurrentFiscalYear, LoginDate, DataSyncOriginId, DataSyncApiBaseUrl) ";
            query += $" VALUES ( '{dtCmpInfo.Rows[0]["Company_Name"]}',";
            query += $"'{dtCmpInfo.Rows[0]["DataBase_Path"]}',";
            query +=
                $"'{FbFolderLocation.FileName.Substring(0, fileNameLength).Trim()}', 1,null,Null,null,GetDate(),Null, NUll ); ";
            var iMaster = GetConnection.ExecuteNonQueryOnMaster(query);

            query = " INSERT INTO AMS.CompanyRights (User_Id, Company_Id, Company_Name) ";
            query += $" Values ({ObjGlobal.LogInUserId},{dtCmpInfo.Rows[0]["Company_Id"]}, '{dtCmpInfo.Rows[0]["Company_Name"]}'); ";
            var onMaster = GetConnection.ExecuteNonQueryOnMaster(query);
        }
        else if (RBtnDeAttach.Checked)
        {
            var dtPath = new DataTable();
            var dtTemp1 = new DataTable();

            var dtMCmpInfo = GetConnection.SelectQueryFromMaster($" Select * from AMS.GlobalCompany where DataBase_Name ='{_dbName[LstViewCompany.SelectedIndices[0]]}'");
            if (dtMCmpInfo.Rows.Count <= 0) return;
            dtPath.Reset();
            dtPath = GetConnection.SelectQueryFromMaster("Select * from sysdatabases where Name='" + dtMCmpInfo.Rows[0]["Database_Name"] + "'");
            if (dtMCmpInfo.Rows.Count > 0)
            {
                fileName = instance.GetParentFolderName(dtPath.Rows[0]["Filename"]) + "\\" + dtMCmpInfo.Rows[0]["Database_Name"] + ".ini";
                var wrt = new StreamWriter(fileName);
                fileName = dtMCmpInfo.Rows[0]["Database_Name"].ToString();
                wrt.Write($"{dtMCmpInfo.Rows[0]["Database_Name"]}|{dtMCmpInfo.Rows[0]["Company_Name"]}|{dtMCmpInfo.Rows[0]["Database_Path"]}|{dtMCmpInfo.Rows[0]["DataBase_Name"]}");
            }

            var dtTemp = GetConnection.SelectQueryFromMaster($@"Select Spid from Sysprocesses inner join sysdatabases on Sysprocesses.dbid = sysdatabases.dbid  where sysdatabases.Name = '{fileName}'");
            var sqlConnection = GetConnection.GetConnectionMaster();
            if (dtTemp.Rows.Count > 0)
            {
                var cmd = new SqlCommand("Kill " + dtTemp.Rows[0]["Spid"], sqlConnection) { CommandType = CommandType.Text };
                for (var ro = 0; ro < dtTemp.Rows.Count; ro++)
                {
                    cmd.CommandText = "Kill " + dtTemp.Rows[ro]["Spid"];
                    cmd.ExecuteNonQuery();
                }
            }

            dtTemp1.Reset();
            dtTemp1 = GetConnection.SelectQueryFromMaster($"EXEC sp_detach_db '{_dbName[LstViewCompany.SelectedIndices[0]]}', 'true'");
            dtTemp1.Reset();
            dtTemp1 = GetConnection.SelectQueryFromMaster($"Delete from AMS.GlobalCompany Where DataBase_Name='{fileName}'");
            dtTemp1.Reset();
            dtTemp1 = GetConnection.SelectQueryFromMaster($"Delete from AMS.CompanyRights Where Company_Id='{dtMCmpInfo.Rows[0]["GComp_Id"]}'");
        }

        var databaseSuccessfully = RBtnDeAttach.Checked ? "COMPANY DE-ATTACH SUCCESSFULLY..!!" : "ATTACH DATABASE SUCCESSFULLY..!!";
        MessageBox.Show(databaseSuccessfully, ObjGlobal.Caption);
        if (RBtnDeAttach.Checked) Close();
        BindCompany();
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.Question(@"DO YOU WANT TO CLOSE THE WINDOWS..??") is DialogResult.Yes)
        {
            Close();
        }
    }
}