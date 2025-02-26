using MrDAL.Core.Utils;
using MrDAL.Properties;
using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrDAL.Core.Logging;

internal class SqliteWrapper : ILogEngine
{
    private bool _initialized;

    public void LogError(Exception e, string message)
    {
        var sc = Environment.UserName.StartsWith("MrDev", StringComparison.OrdinalIgnoreCase) ? null : GetScreenShot();
        if (sc == null)
        {
            LogErrorOnly(e, message);
            return;
        }

        const string errSql =
            @" INSERT INTO Log (Id, message, log_time, image_id, dump, other_data, log_type_alias, log_type, machine, machine_user)
                        VALUES (@Id, @msg, @log_time, @imgId, @dump, @otData, @typeAlias, 'ERROR', @machine, @machineUsr ) ";

        const string imgSql =
            @" INSERT INTO ImgContent (Id, image, datetime, machine, machine_user)
                        VALUES (@Id, @img, @dt, @machine, @machineUsr) ";

        if (!_initialized) Initialize();

        Task.Run(() =>
        {
            using var conn = new SQLiteConnection(Common.SqliteConnString);
            using var cmd = new SQLiteCommand(imgSql, conn);
            cmd.CommandType = CommandType.Text;
            var newGuid = Guid.NewGuid();
            cmd.Parameters.Add("Id", DbType.Guid).Value = newGuid;
            cmd.Parameters.Add("dt", DbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("machine", DbType.String).Value = Environment.MachineName;
            cmd.Parameters.Add("machineUsr", DbType.String).Value = Environment.UserName;

            try
            {
                using var ms = new MemoryStream();
                sc.Save(ms, ImageFormat.Png);
                cmd.Parameters.Add("img", DbType.Binary).Value = ms.ToArray();

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                var cmd2 = new SQLiteCommand(errSql, conn) { CommandType = CommandType.Text };

                cmd2.Parameters.Add("Id", DbType.Guid).Value = Guid.NewGuid();
                cmd2.Parameters.Add("msg", DbType.String).Value = message;
                cmd2.Parameters.Add("log_time", DbType.DateTime).Value = DateTime.Now;
                cmd2.Parameters.Add("dump", DbType.String).Value = e.ToString();
                cmd2.Parameters.Add("typeAlias", DbType.Int32).Value = (int)SqliteLogTypeE.Error;
                cmd2.Parameters.Add("imgId", DbType.Guid).Value = newGuid;
                cmd2.Parameters.Add("machine", DbType.String).Value = Environment.MachineName;
                cmd2.Parameters.Add("machineUsr", DbType.String).Value = Environment.UserName;
                cmd2.Parameters.Add("otData", DbType.AnsiString).Value = string.Empty;

                cmd2.Connection.Open();
                cmd2.ExecuteNonQuery();
                cmd2.Dispose();
            }
            catch (Exception ex)
            {
                new NLogWrapper().LogError(ex, $"Error occurred while logging error to Sqlite db.{Environment.NewLine}{message}{Environment.NewLine}{ex.Message}");
            }
        });
    }

    public void LogError(string message, string dump)
    {
        var sc = GetScreenShot();
        const string errSql = @"
            INSERT INTO Log (Id, message, log_time, image_id, dump, other_data, log_type_alias, log_type, machine, machine_user)
            VALUES (@Id, @msg, @log_time, @imgId, @dump, @otData, @typeAlias, 'ERROR', @machine, @machineUsr ) ";

        const string imgSql = @"
            INSERT INTO ImgContent (Id, image, datetime, machine, machine_user)
            VALUES (@Id, @img, @dt, @machine, @machineUsr) ";

        if (!_initialized) Initialize();

        Task.Factory.StartNew(() =>
        {
            using var conn = new SQLiteConnection(Common.SqliteConnString);
            using var cmd = new SQLiteCommand(imgSql, conn) { CommandType = CommandType.Text };
            var newGuid = Guid.NewGuid();
            cmd.Parameters.Add("Id", DbType.Guid).Value = newGuid;
            cmd.Parameters.Add("dt", DbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("machine", DbType.String).Value = Environment.MachineName;
            cmd.Parameters.Add("machineUsr", DbType.String).Value = Environment.UserName;

            try
            {
                var ms = new MemoryStream();
                sc.Save(ms, ImageFormat.Png);
                cmd.Parameters.Add("img", DbType.Binary).Value = ms.ToArray();

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                var cmd2 = new SQLiteCommand(errSql, conn) { CommandType = CommandType.Text };

                cmd2.Parameters.Add("Id", DbType.Guid).Value = Guid.NewGuid();
                cmd2.Parameters.Add("msg", DbType.String).Value = message;
                cmd2.Parameters.Add("log_time", DbType.DateTime).Value = DateTime.Now;
                cmd2.Parameters.Add("dump", DbType.String).Value = dump ?? string.Empty;
                cmd2.Parameters.Add("typeAlias", DbType.Int32).Value = (int)SqliteLogTypeE.Error;
                cmd2.Parameters.Add("imgId", DbType.Guid).Value = newGuid;
                cmd2.Parameters.Add("machine", DbType.String).Value = Environment.MachineName;
                cmd2.Parameters.Add("machineUsr", DbType.String).Value = Environment.UserName;
                cmd2.Parameters.Add("otData", DbType.String).Value = string.Empty;

                cmd2.Connection.Open();
                cmd2.ExecuteNonQuery();
                cmd2.Dispose();
            }
            catch (Exception ex)
            {
                new NLogWrapper().LogError(ex,
                    $"Error occurred while logging error to Sqlite db.{Environment.NewLine}{message}{Environment.NewLine}{ex.Message}");
            }
        });
    }

    public void LogInfo(string message)
    {
        try
        {
            const string sql =
                @"INSERT INTO Log (Id, message, log_time, image_id, dump, log_type_alias, log_type, machine, machine_user )
                     VALUES (@Id, @msg, @log_time, NULL, @dump, @typeAlias, 'INFO', @machine, @machineUsr) ";

            if (!_initialized)
            {
                //Initialize();
            }

            Task.Run(() =>
            {
                try
                {
                    using var conn = new SQLiteConnection(Common.SqliteConnString);
                    using var cmd = new SQLiteCommand(sql, conn);
                    cmd.Parameters.Add("Id", DbType.Guid).Value = Guid.NewGuid();
                    cmd.Parameters.Add("msg", DbType.String).Value = message;
                    cmd.Parameters.Add("log_time", DbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("dump", DbType.String).Value = DBNull.Value;
                    cmd.Parameters.Add("typeAlias", DbType.Int32).Value = (int)SqliteLogTypeE.Info;
                    cmd.Parameters.Add("machine", DbType.String).Value = Environment.MachineName;
                    cmd.Parameters.Add("machineUsr", DbType.String).Value = Environment.UserName;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    new NLogWrapper().LogError(ex,
                        $"Error occurred while logging info to Sqlite db.{Environment.NewLine}{message}{Environment.NewLine}{ex.Message}");
                }
            });
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
        }
    }

    public void LogWarning(string message, string dump)
    {
        const string sql =
            @"INSERT INTO Log (Id, message, log_time, image_id, dump, log_type_alias, log_type, machine, machine_user )
                VALUES (@Id, @msg, @log_time, NULL, @dump, @typeAlias, 'WARNING', @machine, @machineUsr) ";

        if (!_initialized) Initialize();
        Task.Run(() =>
        {
            using var conn = new SQLiteConnection(Common.SqliteConnString);
            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.Add("Id", DbType.Guid).Value = Guid.NewGuid();
            cmd.Parameters.Add("msg", DbType.String).Value = message;
            cmd.Parameters.Add("log_time", DbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("dump", DbType.String).Value = string.IsNullOrWhiteSpace(dump) ? string.Empty : dump;
            cmd.Parameters.Add("typeAlias", DbType.Int32).Value = (int)SqliteLogTypeE.Warn;
            cmd.Parameters.Add("machine", DbType.String).Value = Environment.MachineName;
            cmd.Parameters.Add("machineUsr", DbType.String).Value = Environment.UserName;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                new NLogWrapper().LogError(ex,
                    $@"Error occurred while logging info to Sqlite db.{Environment.NewLine}{message}{Environment.NewLine}{ex.Message}");
            }
        });
    }

    public void LogWarning(string message)
    {
        const string sql =
            @"INSERT INTO Log (Id, message, log_time, dump, log_type_alias, log_type, machine, machine_user )
                VALUES (@Id, @msg, @log_time, @dump, @typeAlias, 'WARNING', @machine, @machineUsr ) ";

        if (!_initialized) Initialize();
        Task.Factory.StartNew(() =>
        {
            using var conn = new SQLiteConnection(Common.SqliteConnString);
            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.Add("Id", DbType.Guid).Value = Guid.NewGuid();
            cmd.Parameters.Add("msg", DbType.String).Value = message;
            cmd.Parameters.Add("log_time", DbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("dump", DbType.String).Value = DBNull.Value;
            cmd.Parameters.Add("typeAlias", DbType.Int32).Value = (int)SqliteLogTypeE.Warn;
            cmd.Parameters.Add("machine", DbType.String).Value = Environment.MachineName;
            cmd.Parameters.Add("machineUsr", DbType.String).Value = Environment.UserName;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                new NLogWrapper().LogError(ex,
                    $"Error occurred while logging warn to Sqlite db.{Environment.NewLine}{message}{Environment.NewLine}{ex.Message}");
            }
        });
    }

    private void Initialize()
    {
        try
        {
            var sql = Resources.LauncherSqlite;
            using var conn = new SQLiteConnection(Common.SqliteConnString);
            using var cmd = new SQLiteCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            var fileInfo =
                new FileInfo(
                    $@"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\MrSolution\Logs.db");

            if (fileInfo.Directory is not { Exists: true } && fileInfo.Directory != null)
            {
                Directory.CreateDirectory(fileInfo.Directory.FullName);
            }
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            new NLogWrapper().LogError(ex,
                $"Error occurred while initializing the sqlite schema to Sqlite db.{Environment.NewLine}{ex.Message}");
        }

        _initialized = true;
    }

    private void LogErrorOnly(Exception e, string message)
    {
        const string errSql =
            @"INSERT INTO Log (Id, message, log_time, image_id, dump, other_data, log_type_alias, log_type, machine, machine_user)
                VALUES (@Id, @msg, @log_time, @imgId, @dump, @otData, @typeAlias, 'ERROR', @machine, @machineUsr ) ";
        if (!_initialized) Initialize();

        Task.Run(() =>
        {
            try
            {
                using var conn = new SQLiteConnection(Common.SqliteConnString);
                var cmd = new SQLiteCommand(errSql, conn) { CommandType = CommandType.Text };

                cmd.Parameters.Add("Id", DbType.Guid).Value = Guid.NewGuid();
                cmd.Parameters.Add("msg", DbType.String).Value = message;
                cmd.Parameters.Add("log_time", DbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("dump", DbType.String).Value = e.ToString();
                cmd.Parameters.Add("typeAlias", DbType.Int32).Value = (int)SqliteLogTypeE.Error;
                cmd.Parameters.Add("imgId", DbType.Guid).Value = DBNull.Value;
                cmd.Parameters.Add("machine", DbType.String).Value = Environment.MachineName;
                cmd.Parameters.Add("machineUsr", DbType.String).Value = Environment.UserName;
                cmd.Parameters.Add("otData", DbType.AnsiString).Value = string.Empty;

                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                new NLogWrapper().LogError(ex,
                    $"Error occurred while logging error to Sqlite db.{Environment.NewLine}{message}{Environment.NewLine}{ex.Message}");
            }
        });
    }

    public static Bitmap GetScreenShot()
    {
        try
        {
            var bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            using var g = Graphics.FromImage(bmp);
            g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
            return bmp;
        }
        catch
        {
            //
        }

        return null;
    }
}