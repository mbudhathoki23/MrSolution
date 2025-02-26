using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace MrDAL.Utility.Server;
public class GetConnection
{
    // CONNECTION STRING
    public static bool MultiServer;

    public static string ServerDesc = string.Empty;
    public static string LoginInitialCatalog = string.Empty;
    public static string ServerUserId = string.Empty;
    public static string ServerUserPsw = string.Empty;

    public static string OnlineServerDesc = "mr.siyzo.com.np,2033";
    public static string OnlineInitialCatalog = "DataSyncTest";
    public static string OnlineServerUserId = "mrsolution_db_user";
    public static string OnlineServerUserPsw = "NepAL21%23#$";

    public static string SecondaryServerDesc = string.Empty;
    public static string SecondaryInitialCatalog = string.Empty;
    public static string SecondaryServerUserId = string.Empty;
    public static string SecondaryServerUserPsw = string.Empty;

    public static string CloudInitialCatalog = string.Empty;
    public static string WinConnection =>
        $"Server ={ServerDesc}; Database={LoginInitialCatalog};Integrated Security=SSPI;";

    public static string WinMasterConnection =>
        $"Server ={ServerDesc}; Database=master;Integrated Security=SSPI;Connection Timeout=15;";

    public static string CloudMasterConnectionString =>
        $"Data Source=api.mrsolution.com.np,2033;Initial Catalog=master;Encrypt=False;Integrated Security=False;User ID=sa; pwd=MrSolution@;";
    public static string CloudConnectionString =>
        $"Data Source=api.mrsolution.com.np,2033;Initial Catalog={CloudInitialCatalog};Encrypt=False;Integrated Security=False;User ID=sa; pwd=MrSolution@;";

    public static string SecondaryConnectionString =>
        $"data source={SecondaryServerDesc}; Initial Catalog={SecondaryInitialCatalog}; User Id={ServerUserId}; pwd={ServerUserPsw};";

    public static string ConnectionString =>
        $"data source={ServerDesc}; Initial Catalog={LoginInitialCatalog}; User Id={ServerUserId}; pwd={ServerUserPsw};MultipleActiveResultSets=True;Connection Timeout= 500";

    public static string ConnectionStringMaster =>
        $@"data source={ServerDesc}; initial catalog= master; User Id={ServerUserId}; password={ServerUserPsw};Connection Timeout=15;";

    public static string ConnectionStringMasterOnline =>
        $@"Data Source={ServerDesc};Network Library=DBMSSOCN;Initial Catalog=master;User ID={ServerUserId};Password={ServerUserPsw},Integrated Security=SSPI;Connection Timeout=30;";

    public static string ConnectionStringOnline =>
        $@"Data Source={ServerDesc};Network Library=DBMSSOCN;Initial Catalog={LoginInitialCatalog};User ID={ServerUserId};Password={ServerUserPsw}";

    public static string ConnectionStringMrMaster =>
        $@"data source={ServerDesc}; Initial Catalog=dtMaster; User Id={ServerUserId}; pwd={ServerUserPsw};";

    public static int GetSqlServerVersion()
    {
        const string cmdString = @"
        SELECT CAST(PARSENAME(CAST(SERVERPROPERTY('ProductVersion') AS SYSNAME), CASE WHEN SERVERPROPERTY('ProductVersion') IS NULL THEN 3 ELSE 4 END) AS INT) ProductVersion";
        var dtResult = SelectQueryFromMaster(cmdString);
        return dtResult.Rows.Count > 0 ? dtResult.Rows[0]["ProductVersion"].GetInt() : 0;
    }

    public static int IntExecuteNonQuery(string sqlStatement)
    {
        try
        {
            var cmd = new SqlCommand(sqlStatement, ReturnConnection())
            {
                CommandType = CommandType.Text
            };
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            return 1;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult("GetConnection.cs");
            return 0;
        }
    }

    public static bool ImpExecuteNonQuery(string sqlStatement)
    {
        try
        {
            var cmd = new SqlCommand(sqlStatement, ReturnConnection())
            {
                CommandType = CommandType.Text
            };
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            return true;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult("GetConnection.cs");
            return false;
        }
    }

    public static bool ExecuteNonQueryOnMaster(string sqlStatement)
    {
        try
        {
            var cmd = new SqlCommand(sqlStatement, GetConnectionMaster())
            {
                CommandType = CommandType.Text
            };
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            return true;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult("GetConnection.cs");
            return false;
        }
    }
    public static bool CreateLinkServer(string server, string user, string password)
    {
        var cmdString = @$"
        EXEC master.dbo.sp_addlinkedserver @server = N'{server}', @srvproduct=N'SQL Server'
        EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname=N'{server}',@useself=N'False',@locallogin=N'sa',@rmtuser=N'{user}',@rmtpassword='{password}'
        EXEC master.dbo.sp_serveroption @server=N'{server}', @optname=N'collation compatible', @optvalue=N'true'
        EXEC master.dbo.sp_serveroption @server=N'{server}', @optname=N'data access', @optvalue=N'true'
        EXEC master.dbo.sp_serveroption @server=N'{server}', @optname=N'dist', @optvalue=N'false'
        EXEC master.dbo.sp_serveroption @server=N'{server}', @optname=N'pub', @optvalue=N'false'
        EXEC master.dbo.sp_serveroption @server=N'{server}', @optname=N'rpc', @optvalue=N'true'
        EXEC master.dbo.sp_serveroption @server=N'{server}', @optname=N'rpc out', @optvalue=N'true'
        EXEC master.dbo.sp_serveroption @server=N'{server}', @optname=N'sub', @optvalue=N'false'
        EXEC master.dbo.sp_serveroption @server=N'{server}', @optname=N'connect timeout', @optvalue=N'0'
        EXEC master.dbo.sp_serveroption @server=N'{server}', @optname=N'collation name', @optvalue=null
        EXEC master.dbo.sp_serveroption @server=N'{server}', @optname=N'lazy schema validation', @optvalue=N'false'
        EXEC master.dbo.sp_serveroption @server=N'{server}', @optname=N'query timeout', @optvalue=N'0'
        EXEC master.dbo.sp_serveroption @server=N'{server}', @optname=N'use remote collation', @optvalue=N'true'
        EXEC master.dbo.sp_serveroption @server=N'{server}', @optname=N'remote proc transaction promotion', @optvalue=N'true' ";
        var execute = SqlExtensions.ExecuteNonQuery(cmdString);
        return execute != 0;
    }

    public static bool CheckOnlineConnection()
    {
        try
        {
            OnlineInitialCatalog = "MASTER";

            var con = new SqlConnection(CloudMasterConnectionString);
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            return con.State == ConnectionState.Open;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult("GetConnection.cs");
            return false;
        }
    }

    public static bool CheckSecondaryConnection()
    {
        try
        {
            SecondaryInitialCatalog = "MASTER";
            var con = new SqlConnection(CloudMasterConnectionString);
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            return con.State == ConnectionState.Open;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult("GetConnection.cs");
            return false;
        }
    }

    public static bool CheckSqlOpen()
    {
        var isOpen = false;
        try
        {
            if (ReturnConnection().State == ConnectionState.Open) return isOpen = true;

            ReturnConnection().Open();

            if (ReturnConnection().State == ConnectionState.Open) return isOpen = true;
            return false;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            return isOpen;
        }
    }

    public static bool ConnectionCheck()
    {
        if (string.IsNullOrEmpty(ServerDesc))
        {
            var connectionTxt = Application.StartupPath + @"\Connection.txt";
            if (!File.Exists(connectionTxt))
            {
                var sw = File.CreateText(connectionTxt);
                sw.WriteLine("Server;SA;321;True;false");
            }

            try
            {
                var fileStream = new FileStream(connectionTxt, FileMode.Open, FileAccess.Read);
                using var streamReader = new StreamReader(fileStream, Encoding.UTF8);
                var text = streamReader.ReadToEnd();
                fileStream.Close();
                if (text is { Length: > 3 })
                {
                    var split = text.Split(';');
                    var o = split.Length;

                    ObjGlobal.DataSource = split[0];
                    ObjGlobal.ServerUser = split[1];
                    ObjGlobal.ServerPassword = split[2];

                    ServerDesc = split[0];
                    ServerUserId = split[1];
                    ServerUserPsw = split[2];
                    MultiServer = split[3].GetBool();
                    ObjGlobal.MultiServerOption = split[3].GetBool();

                    if (o > 4) Settings.Default.RunMode = split[4].GetBool();

                    if (MultiServer) return false;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                Thread.Sleep(100);
                return false;
            }
        }

        using var con = GetConnectionMaster();
        try
        {
            return con.State == ConnectionState.Open;
        }
        catch
        {
            var con2 = new SqlConnection(ConnectionStringMasterOnline);
            try
            {
                if (con2.State == ConnectionState.Open) con2.Close();
                con2.Open();
                return con2.State == ConnectionState.Open;
            }
            catch
            {
                return false;
            }
        }
    }

    public static bool CheckImportConnection(string impServer, string impUser, string impUserPsw)
    {
        var connection =
            $@"Data Source={impServer};Initial Catalog=Master;Persist Security Info=True;User ID={impUser};Password={impUserPsw}";
        try
        {
            var con = new SqlConnection(connection);
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            return con.State == ConnectionState.Open;
        }
        catch
        {
            return false;
        }
    }

    public static SqlConnection GetOnlineConnection(string onlineServerDesc, string onlineLoginInitialCatalog, string onlineServerUserId, string onlineServerUserPsw)
    {
        var con = new SqlConnection();
        try
        {
            con.ConnectionString = $"data source={onlineServerDesc}; Initial Catalog={onlineLoginInitialCatalog}; User Id={onlineServerUserId}; pwd={onlineServerUserPsw};"; //Integrated Security=True; pooling=false; Connection Timeout=0;";
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult("GetConnection.cs");

            try
            {
                var exception = ex;
                con.ConnectionString = $"data source={onlineServerDesc}; Initial Catalog={onlineLoginInitialCatalog}; Integrated Security=false; pooling=false; Connection Timeout=0;";
                if (con.State != ConnectionState.Open) con.Open();
            }
            catch (Exception ex1)
            {
                ex1.ToNonQueryErrorResult("GetConnection.cs");
                throw new ArgumentException(ex1.Message);
            }
        }

        return con;
    }

    public static SqlConnection GetOnlineConnectionMaster(string onlineServerDesc, string onlineServerUserId, string onlineServerUserPsw)
    {
        var con = new SqlConnection();
        try
        {
            con.ConnectionString = $@"data source={onlineServerDesc}; Initial Catalog=master; User Id={onlineServerUserId}; pwd={onlineServerUserPsw}; ;Persist Security Info=True; Max Pool Size=100;";

            if (con.State != ConnectionState.Open) con.Open();
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult("GetConnection.cs");

            try
            {
                var exception = ex;
                con.ConnectionString = $@"data source={ServerDesc}; Initial Catalog=master; Persist Security Info=false; Integrated Security=SSPI;Max Pool Size=100;";

                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
            }
            catch (Exception ex1)
            {
                ex1.ToNonQueryErrorResult("GetConnection.cs");
                con.Close();
                MessageBox.Show(@"Server is Down or SA Password is Not Match..!!", ObjGlobal.Caption);
            }
        }

        return con;
    }

    public static SqlConnection GetSqlConnection()
    {
        using var con = new SqlConnection();
        try
        {
            con.ConnectionString = LoginInitialCatalog.IsValueExits() ? ConnectionString : ConnectionStringMaster;
            if (con.State != ConnectionState.Open) con.Open();
        }
        catch (Exception ex)
        {
            var errMsg = ex.Message;
            con.ConnectionString = LoginInitialCatalog.IsValueExits() ? WinConnection : WinMasterConnection;
            if (con.State != ConnectionState.Open) con.Open();
        }

        return con;
    }

    public static SqlConnection GetConnectionMaster()
    {
        var con = new SqlConnection();
        try
        {
            con.ConnectionString = ConnectionStringMaster;
            if (con.State != ConnectionState.Open)
            {
                con.Open();
                return con;
            }
        }
        catch (Exception ex)
        {
            try
            {
                var msg = ex.Message;
                con.ConnectionString = WinMasterConnection;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                    return con;
                }
            }
            catch (Exception ex1)
            {
                var exception = ex1;
                con.Close();
                var result = StartSqlServerServices();
                if (result)
                {
                    var connection = GetConnectionMaster();
                    if (connection.State == ConnectionState.Open) return connection;
                }

                MessageBox.Show(@"Server is Down or SA Password is Not Match..!!", ObjGlobal.Caption);
            }
        }

        return con;
    }

    private static bool StartSqlServerServices()
    {
        var result = false;
        try
        {
            var server = Environment.MachineName.Equals(ServerDesc) ? "MSSQLSERVER" : ServerDesc;
            var sc = new ServiceController(server, Environment.MachineName);
            if (sc.Status.Equals(ServiceControllerStatus.Stopped))
            {
                sc.Start();
                sc.Refresh();
                result = true;
            }
            return result;
            //var process = new System.Diagnostics.Process();
            //var server = System.Environment.MachineName.Equals(ServerDesc) ? "MSSQLSERVER" : ServerDesc;
            //process.StartInfo.FileName = $"net start \"Sql Server ({server})\"";
            //return process.Start();
        }
        catch
        {
            return result;
        }
    }

    public static SqlConnection ReturnConnection()
    {
        var con = new SqlConnection();
        try
        {
            con.ConnectionString = ConnectionString;
            if (con.State != ConnectionState.Open) con.Open();
            return con;
        }
        catch (Exception ex)
        {
            var msg = ex.Message;
            con.ConnectionString = WinConnection;
            if (con.State != ConnectionState.Open) con.Open();
            return con;
        }
    }

    public static string SaveSqlServerInfo(string serverName, string userName, string password, bool multipleDataSource)
    {
        try
        {
            const string cmdString = "Delete From MASTER.AMS.DataServerInfo";
            SqlExtensions.ExecuteNonQuery(ConnectionStringMaster, CommandType.Text, cmdString);
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult("GetConnection.cs");
        }

        var cmd = $@"
        INSERT INTO AMS.DataServerInfo (ServerName,UserName,Password,MultipleDatasource)
        VALUES ('{serverName}','{userName}','{password}',CAST('{multipleDataSource}' AS BIT) )";
        SqlExtensions.ExecuteNonQuery(ConnectionStringMaster, CommandType.Text, cmd);
        var connection = Environment.CurrentDirectory + @"\Connection.txt";
        var file = new StreamWriter(connection);
        TextWriter tw = file;
        file.WriteLine($"{serverName};{userName};{password};{multipleDataSource}");
        tw.Close();
        return "Record Inserted Successfully !";
    }

    public static string RollBackConMaster()
    {
        try
        {
            var con1 = "";
            if (ServerUserPsw != "")
            {
                con1 = $"data source={ServerDesc}; Initial Catalog=master; User Id={ServerUserId}; pwd={ServerUserPsw};";
            }
            else
            {
                con1 = $"data source={ServerDesc}; Initial Catalog=master; Integrated Security = SSPI; Connection Timeout = 15";
            }
            return con1;
        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.Message);
        }
    }

    public static string ReturnConnectionStringMaster()
    {
        try
        {
            var con1 = "";
            if (ServerUserPsw != "")
            {
                con1 = $"data source={ServerDesc}; Initial Catalog=master; User Id={ServerUserId}; pwd={ServerUserPsw};MultipleActiveResultSets=True;Connection Timeout= 500";
            }
            else
            {
                con1 = $"data source={ServerDesc}; Initial Catalog=master; Integrated Security = SSPI; Connection Timeout = 15";
            }
            return con1;
        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.Message);
        }
    }

    public static string GetConnectionString()
    {
        using var con = new SqlConnection();
        try
        {
            con.ConnectionString = LoginInitialCatalog.IsValueExits() ? ConnectionString : ConnectionStringMaster;
            if (con.State != ConnectionState.Open)
            {
                con.Open();
                return con.ConnectionString;
            }
        }
        catch (Exception ex)
        {
            var errMsg = ex.Message;
            con.ConnectionString = LoginInitialCatalog.IsValueExits() ? WinConnection : WinMasterConnection;
            if (con.State != ConnectionState.Open)
            {
                con.Open();
                return con.ConnectionString;
            }
        }

        return con.ConnectionString;
    }

    public static string GetQueryData(string query)
    {
        try
        {
            var table = SqlExtensions.ExecuteDataSet(query).Tables[0];
            return table?.Rows.Count > 0 ? table.Rows[0][0].ToString() : string.Empty;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return string.Empty;
        }
    }

    public string GetSqlMasterData(string cmdString)
    {
        try
        {
            var dt = new DataTable();
            var cmd = new SqlCommand(cmdString, GetConnectionMaster())
            {
                CommandTimeout = 500
            };
            var sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            return dt.Rows.Count > 0 ? dt.Rows[0][0].ToString() : string.Empty;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult("GetConnection.cs");
            return ex.Message;
        }
    }

    public string GetSqlData(string cmdString)
    {
        try
        {
            var dt = new DataTable();
            var cmd = new SqlCommand(cmdString, ReturnConnection())
            {
                CommandTimeout = 500
            };
            var sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            return dt.Rows.Count > 0 ? dt.Rows[0][0].ToString() : string.Empty;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult("GetConnection.cs");
            return ex.Message;
        }
    }

    // FETCH DATA FROM DATA READER AND TABLE

    #region ------------------ FETCH DATA FROM DATA READER AND TABLE ------------------

    public static DataTable SelectDataTableQuery(string query)
    {
        try
        {
            var result = SqlExtensions.ExecuteDataSet(query);
            return result.Tables.Count > 0 ? result.Tables[0] : new DataTable();
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return new DataTable("error");
        }
    }

    public static DataTable SelectDataTable(string cmdString)
    {
        var dt = new DataTable();
        try
        {
            var cmd = new SqlCommand(cmdString, ReturnConnection())
            {
                CommandTimeout = 500
            };
            var sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            return dt;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult("GetConnection.cs");
        }

        return dt;
    }

    public static DataTable SelectQueryFromMaster(string cmdString)
    {
        return SqlExtensions.ExecuteDataSetOnMaster(cmdString).Tables[0];
    }

    #endregion ------------------ FETCH DATA FROM DATA READER AND TABLE ------------------
}