using Microsoft.Win32;
using MrDAL.Global.Common;
using MrDAL.Utility.dbMaster;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MrBLL.Utility.Restore.Properties;

public class SqlServer
{
    public const string DefaultInstanceName = "MSSQLSERVER";

    private static volatile List<string> _systemDatabases;

    private static readonly object SyncRoot;

    static SqlServer()
    {
        SyncRoot = new object();
    }

    protected static List<string> SystemDatabases
    {
        get
        {
            if (_systemDatabases != null)
            {
                return _systemDatabases;
            }
            lock (SyncRoot)
            {
                _systemDatabases = ["master", "model", "msdb", "tempdb"];
            }

            return _systemDatabases;
        }
    }

    private static void DisconnectApplications(SqlServerInfo sqlInfo, string databaseName)
    {
        var sqlCommand = GetSqlCommand(sqlInfo,
            $"DECLARE @DatabaseName varchar(30), @ServerProcessID varchar(10), @StartTime datetime SELECT @StartTime = current_timestamp, @DatabaseName = '{databaseName}'while(exists(Select * FROM sysprocesses WHERE dbid = db_id(@DatabaseName)) AND datediff(mi, @StartTime, current_timestamp) < 3) begin DECLARE spids CURSOR FOR  SELECT convert(varchar, spid) FROM sysprocesses  WHERE dbid = db_id(@DatabaseName) OPEN spids while(1=1) BEGIN  FETCH spids INTO @ServerProcessID IF @@fetch_status < 0 BREAK exec('kill ' + @ServerProcessID) END DEALLOCATE spids end IF NOT exists(Select * FROM sysprocesses WHERE dbid = db_id(@DatabaseName))  EXEC sp_dboption @DatabaseName, offline, true else begin PRINT 'The following server processes are still using '+ @DatabaseName +':' SELECT spid, cmd, status, last_batch, open_tran, program_name, hostname FROM sysprocesses WHERE dbid = db_id(@DatabaseName) end",
            10000);
        sqlCommand.Connection.Open();
        sqlCommand.Connection.ChangeDatabase("master");
        try
        {
            sqlCommand?.ExecuteNonQuery();
        }
        catch (Exception)
        {
            // ignored
        }
        finally
        {
            sqlCommand.Connection.Close();
        }
    }

    private static List<DbFileInfo> GetBakFiles(SqlServerInfo sqlInfo, string bakFile)
    {
        var dbFileInfos = new List<DbFileInfo>();
        SqlCommand sqlCommand = null;
        try
        {
            var str = $"RESTORE FILELISTONLY FROM DISK='{bakFile.Replace("'", "''")}'";
            sqlCommand = GetSqlCommand(sqlInfo, str, 10000);
            sqlCommand.Connection.Open();
            var sqlDataReader = sqlCommand.ExecuteReader();
            try
            {
                while (sqlDataReader.Read())
                {
                    var dbFileInfo = new DbFileInfo
                    {
                        Name = sqlDataReader.GetString(sqlDataReader.GetOrdinal("LogicalName")),
                        Path = sqlDataReader.GetString(sqlDataReader.GetOrdinal("PhysicalName"))
                    };
                    dbFileInfos.Add(dbFileInfo);
                }
            }
            finally
            {
                sqlDataReader.Close();
            }
        }
        finally
        {
            if (sqlCommand is
                {
                    Connection: { }
                }) sqlCommand.Connection.Close();
        }

        return dbFileInfos;
    }

    private static string GetConnectionString(SqlServerInfo server, string database, int? timeout)
    {
        string str;
        var str1 = string.IsNullOrEmpty(database) ? "" : $"Initial Catalog={database};";
        if (server.IntegratedSecurity)
        {
            str = $"Data Source={server.Server};{str1}Integrated Security=True{(timeout.HasValue ? string.Concat(";Connection Timeout=", timeout.Value) : "")}";
        }
        else
        {
            object[] objArray = [server.Server, server.UserName, server.Password, null, null];
            objArray[3] = timeout.HasValue ? string.Concat(";Connection Timeout=", timeout.Value) : "";
            objArray[4] = str1;
            str = string.Format("Data Source={0};{4}User ID={1};Password={2}{3}", objArray);
        }

        return str;
    }

    public static List<string> GetDatabases(SqlServerInfo server)
    {
        var getDatabases = new List<string>();
        if (server == null) throw new ArgumentException(@"DATA SERVER IS NULL..!!", ObjGlobal.Caption);
        SqlCommand sqlCommand = null;
        try
        {
            sqlCommand = GetSqlCommand(server, "SELECT name FROM sys.databases", 5);
            sqlCommand.Connection.Open();
            SqlDataReader sqlDataReader;
            try
            {
                sqlDataReader = sqlCommand.ExecuteReader();
            }
            catch
            {
                sqlCommand.CommandText = "sp_databases";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlDataReader = sqlCommand.ExecuteReader();
            }

            try
            {
                while (sqlDataReader.Read()) getDatabases.Add(sqlDataReader.GetString(0));
            }
            finally
            {
                sqlDataReader.Close();
            }
        }
        catch (Exception)
        {
            // ignored
        }
        finally
        {
            sqlCommand?.Connection.Close();
        }

        return getDatabases;
    }

    private static List<DbFileInfo> GetDbFiles(SqlServerInfo sqlInfo, string dbName)
    {
        var dbFileInfos = new List<DbFileInfo>();
        SqlCommand sqlCommand = null;
        try
        {
            var str = $"\r\nselect fl.name, fl.filename, fl.fileid\r\nfrom sys.sysdatabases db \r\ninner join sys.sysaltfiles fl\r\non db.dbid = fl.dbid where db.name='{dbName.Replace("'", "''")}'";
            sqlCommand = GetSqlCommand(sqlInfo, str, 10000);
            sqlCommand.Connection.Open();
            var sqlDataReader = sqlCommand.ExecuteReader();
            try
            {
                while (sqlDataReader.Read())
                {
                    var dbFileInfo = new DbFileInfo
                    {
                        Name = sqlDataReader.GetString(0),
                        Path = sqlDataReader.GetString(1)
                    };
                    dbFileInfos.Add(dbFileInfo);
                }
            }
            finally
            {
                sqlDataReader.Close();
            }
        }
        catch (Exception)
        {
            // ignored
        }
        finally
        {
            sqlCommand?.Connection?.Close();
        }

        return dbFileInfos;
    }

    public static List<string> GetLocalSqlServers()
    {
        List<string> getLocalSqlServers;
        try
        {
            var registryKey =
                Registry.LocalMachine.OpenSubKey(
                    "\\SOFTWARE\\Microsoft\\Microsoft SQL GetServer\\Instance Names\\SQLL");
            if (registryKey == null)
            {
                var registryView = Environment.Is64BitOperatingSystem
                    ? RegistryView.Registry64
                    : RegistryView.Registry32;
                using var openBaseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView);
                var instanceKey = openBaseKey.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL GetServer\Instance Names\SQL", false);
                if (instanceKey != null)
                {
                    var valueNames = instanceKey.GetValueNames();
                    var localSqlServers = new List<string>(valueNames.Length);
                    localSqlServers.AddRange(valueNames.Select(name =>
                    {
                        var computerName = Environment.MachineName;
                        return name.Equals("MSSQLSERVER") ? $"{computerName}" : string.Concat($"{computerName}\\", name);
                    }));
                    getLocalSqlServers = localSqlServers;
                }
                else
                {
                    var valueNames = new[]
                    {
                        "MSSQLSERVER"
                    };
                    var localSqlServers = new List<string>(valueNames.Length);
                    localSqlServers.AddRange(valueNames.Select(name =>
                    {
                        var computerName = Environment.MachineName;
                        return name.Equals("MSSQLSERVER") ? $"{computerName}" : string.Concat($"{computerName}\\", name);
                    }));
                    getLocalSqlServers = localSqlServers;
                }
            }
            else
            {
                var valueNames = registryKey.GetValueNames();
                var localSqlServers = new List<string>(valueNames.Length);
                localSqlServers.AddRange(valueNames.Select(name => name.Equals("MSSQLSERVER") ? "." : string.Concat(".\\", name)));
                getLocalSqlServers = localSqlServers;
            }
        }
        catch
        {
            getLocalSqlServers = [];
        }

        return getLocalSqlServers;
    }

    private static string GetServiceName(SqlServerInfo sqlInfo)
    {
        string str;
        SqlCommand sqlCommand = null;
        try
        {
            sqlCommand = GetSqlCommand(sqlInfo, "select @@ServiceName", 10000);
            sqlCommand.Connection.Open();
            str = sqlCommand.ExecuteScalar().ToString();
        }
        finally
        {
            if (sqlCommand is { Connection: { } }) sqlCommand.Connection.Close();
        }

        return str;
    }

    internal static SqlCommand GetSqlCommand(SqlServerInfo server, string cmdText, int? timeout)
    {
        if (server == null) throw new ArgumentException(@"Server is null", ObjGlobal.Caption);
        var connectionString = GetConnectionString(server, null, timeout);
        var sqlCommand = new SqlCommand(cmdText, new SqlConnection(connectionString))
        {
            CommandTimeout = 10000
        };
        return sqlCommand;
    }

    public static bool IsLocalServer(string machine)
    {
        bool hostName;
        try
        {
            var num = machine.LastIndexOf('\\');
            if (num > 0) machine = machine.Substring(0, num);
            if (machine.Trim() != ".")
            {
                var hostEntry = Dns.GetHostEntry(machine);
                var pHostEntry = Dns.GetHostEntry(Environment.MachineName);
                hostName = hostEntry.HostName == pHostEntry.HostName;
            }
            else
            {
                hostName = true;
            }
        }
        catch (Exception)
        {
            hostName = true;
        }

        return hostName;
    }

    public static bool IsSystemDb(string database)
    {
        var str = database.ToLower().Trim();
        return SystemDatabases.Contains(str);
    }

    private static void RestartServer(SqlServerInfo sqlInfo)
    {
        var serviceName = GetServiceName(sqlInfo);
        if (serviceName == null) throw new Exception("SQL GetServer service not found");
        FrmRestore.SetStatus($"Stopping {serviceName} server...");
        var processStartInfo = new ProcessStartInfo
        {
            FileName = "net",
            Arguments = string.Concat("stop mssql$", serviceName),
            ErrorDialog = true,
            CreateNoWindow = true,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            StandardOutputEncoding = Encoding.Default
        };
        Process.Start(processStartInfo)?.WaitForExit();
        FrmRestore.SetStatus($"Starting {serviceName} server...");
        processStartInfo.Arguments = string.Concat("start mssql$", serviceName);
        Process.Start(processStartInfo)?.WaitForExit();
    }

    internal static void Restore(RestoreParams param)
    {
        List<DbFileInfo> bakFiles = null;
        string str = null;
        var flag = false;
        var str1 = param.BakFile;
        if (Path.GetExtension(str1) == ".zip")
        {
            try
            {
                FrmRestore.SetStatus($"Unzipping {Path.GetFileName(str1)}...");
                str1 = Unzip(str1);
                FrmRestore.SetStatus($"Analyzing {Path.GetFileName(str1)}...");
                if (string.IsNullOrEmpty(str1))
                    throw new Exception("NO FILE WITH .BAK EXTENSION WAS FOUND IN THE ARCHIVE");
                flag = true;
            }
            catch (Exception exception1)
            {
                throw new Exception(string.Concat("CAN'T UNZIP ", Path.GetFileName(param.BakFile), ": ", exception1.Message));
            }
        }

        try
        {
            var stringBuilder = new StringBuilder();
            var sqlParameters = new List<SqlParameter>();
            try
            {
                try
                {
                    bakFiles = GetBakFiles(param.SqlServerInfo, str1);
                    stringBuilder.AppendFormat("RESTORE DATABASE [{0}] FROM DISK = {1} WITH REPLACE", param.Database, "@Path");
                }
                catch (SqlException sqlException)
                {
                    if (sqlException.Number != 3201) throw;
                    var registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Microsoft SQL GetServer");
                    if (registryKey == null) throw;
                    var subKeyNames = registryKey.GetSubKeyNames();
                    foreach (var str2 in subKeyNames)
                    {
                        if (!str2.StartsWith("MSSQL")) continue;
                        var registryKey1 = registryKey.OpenSubKey(string.Concat(str2, "\\MSSQLServer"));
                        if (registryKey1 == null)
                        {
                            continue;
                        }

                        var str3 = registryKey1.GetValue("BackupDirectory").ToString();
                        var fileName = Path.GetFileName(Path.GetTempFileName());
                        str = Path.Combine(str3, fileName);
                        FrmRestore.SetStatus("Copying to backup folder...");
                        try
                        {
                            File.Copy(str1, str, true);
                        }
                        catch (UnauthorizedAccessException)
                        {
                            var uri = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase);
                            var processStartInfo = new ProcessStartInfo
                            {
                                UseShellExecute = true,
                                WorkingDirectory = Environment.CurrentDirectory,
                                FileName = uri.LocalPath
                            };
                            string[] strArrays = ["-quiet -copy \"", str1, "\" \"", str, "\""];
                            processStartInfo.Arguments = string.Concat(strArrays);
                            if (Environment.OSVersion.Version.Major >= 6) processStartInfo.Verb = "runas";
                            var process = new Process
                            {
                                StartInfo = processStartInfo
                            };
                            process.Start();
                            process.WaitForExit();
                            if (process.ExitCode != 0)
                            {
                                throw new InvalidOperationException($"Cannot copy file \"{str1}\" to \"{str}\"");
                            }
                        }

                        FrmRestore.SetStatus("Getting backup information...");
                        try
                        {
                            bakFiles = GetBakFiles(param.SqlServerInfo, str);
                            stringBuilder.AppendFormat("RESTORE DATABASE [{0}] FROM DISK = {1} WITH REPLACE", param.Database, "@Path");
                            break;
                        }
                        catch
                        {
                            try
                            {
                                File.Delete(str);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                            str = null;
                        }
                    }

                    if (bakFiles == null || str == null) throw;
                }

                switch (bakFiles.Count)
                {
                    case 0: throw new Exception("No Database files found in the source backup file");
                    case <= 2:
                    {
                        var num = 0;
                        var dbFiles = GetDbFiles(param.SqlServerInfo, param.Database);
                        switch (dbFiles.Count)
                        {
                            case > 0 and <= 2:
                            {
                                foreach (var bakFile in bakFiles)
                                foreach (var dbFile in dbFiles)
                                {
                                    if (Path.GetExtension(dbFile.Path) != Path.GetExtension(bakFile.Path)) continue;
                                    var str4 = $"@FileMove0_{num}";
                                    var str5 = $"@FileMove1_{num}";
                                    stringBuilder.AppendFormat(", MOVE {0} TO {1}", str4, str5);
                                    var sqlParameter = new SqlParameter(str4, SqlDbType.NVarChar)
                                    {
                                        Value = bakFile.Name
                                    };
                                    var sqlParameter1 = sqlParameter;
                                    var sqlParameter2 = new SqlParameter(str5, SqlDbType.NVarChar)
                                    {
                                        Value = dbFile.Path
                                    };
                                    var sqlParameter3 = sqlParameter2;
                                    sqlParameters.Add(sqlParameter1);
                                    sqlParameters.Add(sqlParameter3);
                                    num++;
                                    break;
                                }

                                break;
                            }
                            case 0:
                            {
                                var dbFileInfos = GetDbFiles(param.SqlServerInfo, "master");
                                if (dbFileInfos.Count > 0)
                                {
                                    var directoryName = Path.GetDirectoryName(dbFileInfos[0].Path) ??
                                                        Directory.GetCurrentDirectory();
                                    foreach (var dbFileInfo in bakFiles)
                                    {
                                        var str6 = $"@FileMove0_{num}";
                                        var str7 = $"@FileMove1_{num}";
                                        stringBuilder.AppendFormat(", MOVE {0} TO {1}", str6, str7);
                                        var sqlParameter4 = new SqlParameter(str6, SqlDbType.NVarChar)
                                        {
                                            Value = dbFileInfo.Name
                                        };
                                        var sqlParameter5 = sqlParameter4;
                                        var sqlParameter6 = new SqlParameter(str7, SqlDbType.NVarChar)
                                        {
                                            Value =
                                                $"{Path.Combine(directoryName, param.Database)}{(Path.GetExtension(dbFileInfo.Path) == ".ldf" ? "_log" : string.Empty)}{Path.GetExtension(dbFileInfo.Path)}"
                                        };
                                        sqlParameters.Add(sqlParameter5);
                                        sqlParameters.Add(sqlParameter6);
                                        num++;
                                    }
                                }

                                break;
                            }
                        }

                        break;
                    }
                }
            }
            catch (Exception exception3)
            {
                throw new Exception(string.Concat("Can't match logical files: ", exception3.Message));
            }

            var flag1 = false;
            while (true)
            {
                var backgroundWorker1 = new BackgroundWorker
                {
                    WorkerSupportsCancellation = true,
                    WorkerReportsProgress = true
                };
                try
                {
                    if (param.DisconnectApps)
                    {
                        FrmRestore.SetStatus("DIS - CONNECTING APPLICATION...");
                        DisconnectApplications(param.SqlServerInfo, param.Database);
                    }

                    FrmRestore.SetStatus($"RESTORING {param.Database} DATABASE...");
                    using var sqlCommand = GetSqlCommand(param.SqlServerInfo, stringBuilder.ToString(), 10000);
                    sqlCommand.Connection.Open();
                    try
                    {
                        var sqlCommand1 = sqlCommand.Connection.CreateCommand();
                        sqlCommand1.CommandText = "select @@SPID";
                        short? nullable2 = null;
                        using (var sqlDataReader = sqlCommand1.ExecuteReader())
                        {
                            if (sqlDataReader.Read()) nullable2 = (short)sqlDataReader.GetValue(0);
                        }

                        backgroundWorker1.DoWork += (s1, e1) =>
                        {
                            if (s1 is not BackgroundWorker backgroundWorker)
                            {
                                return;
                            }

                            var argument = e1.Argument as SqlConnection;
                            var nullable = nullable2;
                            if (!(nullable.HasValue ? new int?(nullable.GetValueOrDefault()) : null).HasValue || argument == null) return;
                            argument.Open();
                            if (nullable2 != null)
                            {
                                var dbExecStatus = new DbExecStatus(argument, nullable2.Value);
                                do
                                {
                                    dbExecStatus.UpdateStatus();
                                    backgroundWorker.ReportProgress((int)dbExecStatus.PercentComplete);
                                    Thread.Sleep(500);
                                } while (!backgroundWorker.CancellationPending);
                            }

                            argument.Close();
                        };
                        backgroundWorker1.ProgressChanged += (s1, e1) =>
                            FrmRestore.UpdateProgress(null, e1.ProgressPercentage);
                        backgroundWorker1.RunWorkerCompleted +=
                            (s1, e1) => FrmRestore.UpdateProgress(null, 0);
                        var nullable3 = nullable2;
                        int? nullable1;
                        if (nullable3.HasValue)
                            nullable1 = nullable3.GetValueOrDefault();
                        else
                            nullable1 = null;
                        if (nullable1.HasValue)
                        {
                            int? nullable4 = null;
                            backgroundWorker1.RunWorkerAsync(
                                GetSqlCommand(param.SqlServerInfo, string.Empty, nullable4).Connection);
                        }

                        sqlCommand.Parameters.Clear();
                        var array = (
                            from ICloneable x in sqlParameters
                            select x.Clone() as SqlParameter
                            into x
                            where x != null
                            select x).ToArray();
                        sqlCommand.Parameters.AddRange(array);
                        sqlCommand.Parameters.Add("@Path", SqlDbType.VarChar, 4096).Value = str1;
                        sqlCommand.ExecuteNonQuery();
                    }
                    finally
                    {
                        sqlCommand.Connection.Close();
                    }

                    break;
                }
                catch (SqlException sqlException2)
                {
                    if (!FrmRestore.IsGuiMode || sqlException2.Number != 3101 || flag1) throw;

                    if (MessageBox.Show(@"THIS DATABASE IS IN USE. DO YOU WANT TO DIS-CONNECT DATABASE...??",
                            ObjGlobal.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button1) == DialogResult.No) throw;
                    try
                    {
                        DisconnectApplications(param.SqlServerInfo, param.Database);
                        flag1 = true;
                    }
                    catch
                    {
                        throw sqlException2;
                    }
                }
                finally
                {
                    if (backgroundWorker1.IsBusy) backgroundWorker1.CancelAsync();
                }
            }
        }
        finally
        {
            if (str != null)
            {
                try
                {
                    File.Delete(str);
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            if (flag && !string.IsNullOrEmpty(str1))
            {
                try
                {
                    File.Delete(str1);
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            FrmRestore.SetStatus(string.Empty);
            AlterDatabaseTable.AlterRestoreDatabaseInitial();
        }
    }

    public static bool TestConnection(SqlServerInfo server, ref string error, bool quick)
    {
        bool flag;
        error = "OK";
        var sqlCommand = GetSqlCommand(server, string.Empty, quick ? 1 : 15);
        try
        {
            sqlCommand.Connection.Open();
            sqlCommand.Connection.Close();
            flag = true;
        }
        catch (Exception exception)
        {
            error = exception.Message;
            flag = false;
        }

        return flag;
    }

    private static string Unzip(string zipFileName)
    {
        //var zipArchive = new System.IO.Compression.ZipArchive(new DiskFile(zipFileName));
        //var files = zipArchive.GetFiles(false);
        //foreach (var abstractFile in files)
        //{
        //    if (Path.GetExtension(abstractFile.Name) != ".bak") continue;
        //    var directoryName = Path.GetDirectoryName(zipFileName) ?? Path.GetTempPath();
        //    if (!Directory.Exists(directoryName)) Directory.CreateDirectory(directoryName);
        //    return abstractFile.CopyTo(new DiskFolder(directoryName), true).FullName;
        //}
        return null;
    }
}