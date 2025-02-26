using MrBLL.Utility.Restore.Properties;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using MrDAL.Master;
using MrDAL.Setup.BackupRestore;
using MrDAL.Setup.Interface;
using MrDAL.Utility.Server;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Utility.Restore;

public partial class FrmRestore : MrForm
{
    // RESTORE FUNCTION
    #region ---------- RESTORE FUNCTION ----------
    public FrmRestore()
    {
        InitializeComponent();
        _restoreRepository = new BackupRestoreRepository();
        Main();
        _me = this;
        AppTitle = ((AssemblyTitleAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), true)[0]).Title;
    }
    private void BtnBrowseFileClick(object sender, EventArgs e)
    {
        openBakFileDialog.FileName = txtBakFile.Text;
        try
        {
            openBakFileDialog.InitialDirectory = Path.GetDirectoryName(txtBakFile.Text);
        }
        catch (Exception exception)
        {
            // ignored
        }

        if (openBakFileDialog.ShowDialog() != DialogResult.OK) return;
        txtBakFile.Text = openBakFileDialog.FileName;
        GuessDatabase();
        SaveSettings();
        cbDatabase.Text = GetConnection.LoginInitialCatalog;
    }

    private void BtnRestoreClick(object sender, EventArgs e)
    {
        try
        {
            if (_restoreThread == null)
            {
                var restoreParam = new RestoreParams
                {
                    SqlServerInfo = _sqlInfo,
                    BakFile = txtBakFile.Text.Trim(),
                    Database = cbDatabase.Text.Trim()
                };
                if (!File.Exists(restoreParam.BakFile))
                {
                    MessageBox.Show($@"THE SOURCE FILE {restoreParam.BakFile} DOES NOT EXIST..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else if (!cbDatabase.Items.Contains(restoreParam.Database) || MessageBox.Show($@"
                    THIS WILL REPLACE THE CURRENT DATA IN {restoreParam.Database} DATABASE
                    WITH THE BACKUP FROM {restoreParam.BakFile} FILE.
                    THIS CAN NOT BE UNDONE UNLESS YOU CREATE A BACKUP OF THE CURRENT DATA FROM
                    THE {restoreParam.Database} DATABASE PRIOR TO THIS RESTORE..
                    ARE YOU SURE YOU WANT TO PROCEED WITH THIS RESTORE..??", ObjGlobal.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    _restoreThread = new Thread(RestoreThreadProc);
                    _restoreThread.Start(restoreParam);
                    BtnRestore.Text = @"&CANCEL";
                    progressBar.Visible = lblRestoring.Visible = true;
                    lblStatus.Visible = gbSource.Enabled = gbTarget.Enabled = false;
                }
            }
            else
            {
                try
                {
                    _restoreThread.Abort();
                }
                catch
                {
                    // ignored
                }
                finally
                {
                    FinishRestoreProcess();
                    Close();
                }
            }
        }
        catch (Exception exception1)
        {
            var exception = exception1;
            CustomMessageBox.ErrorMessage(exception1.Message);
        }
    }

    private void CbDatabasePreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
    {
        if (e.KeyCode == Keys.Return && txtBakFile.Text.Trim().Length == 0)
        {
            txtBakFile.Focus();
        }
    }

    private void CbDatabaseSelectedIndexChanged(object sender, EventArgs e)
    {
        RefreshRestoreButton();
    }

    private void CbDatabaseTextUpdate(object sender, EventArgs e)
    {
        RefreshRestoreButton();
    }


    private void FrmRestore_Closed(object sender, FormClosedEventArgs e)
    {
        SaveSettings();
    }

    private void FrmRestore_Load(object sender, EventArgs e)
    {
        if (!Settings.Default.DontAssoc)
        {
            try
            {
                if (!FileAssociation.CheckAssociation())
                {
                    Settings.Default.DontAssoc = true;
                    Settings.Default.Save();
                }
            }
            catch (Exception exception1)
            {
                var exception = exception1;
                MessageBox.Show($@"{exception.Message}{(exception.InnerException != null ? string.Concat(" ", exception.InnerException.Message) : "")}", AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        LoadSettings();
        if (!string.IsNullOrEmpty(FileToOpen)) txtBakFile.Text = FileToOpen;
        if (!string.IsNullOrEmpty(InitDb))
        {
            cbDatabase.Text = InitDb;
            cbDatabase.Enabled = false;
        }

        if (!string.IsNullOrEmpty(InitServer))
        {
            _sqlInfo.Server = InitServer;
            _sqlInfo.IntegratedSecurity = string.IsNullOrEmpty(InitUser);
            if (!string.IsNullOrEmpty(InitUser))
            {
                _sqlInfo.UserName = InitUser;
                _sqlInfo.Password = InitPassword ?? string.Empty;
            }
        }
        InitSqlServer();
        RefreshSqlServer();
        RefreshRestoreButton();
    }

    private void Form1Shown(object sender, EventArgs e)
    {
        var title = ((AssemblyTitleAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), true)[0]).Title;
        var version = Assembly.GetExecutingAssembly().GetName().Version;
        object[] major = [title, version.Major, version.Minor, version.Build];
        Text = string.Format("{0} {1}.{2}.{3}", major);
        if (txtBakFile.Text.Trim().Length == 0)
        {
            txtBakFile.Focus();
        }
        else if (cbDatabase.Text.Trim().Length != 0)
        {
            BtnRestore.Focus();
        }
        else
        {
            cbDatabase.Focus();
        }
        GuessDatabase();
    }

    private void LinkLabel1LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        Process.Start("http://mrsolution.com.np/");
    }

    private void LnkConnectLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        if (new SqlConnectDialog(_sqlInfo).ShowDialog() == DialogResult.OK)
        {
            RefreshSqlServer();
            GuessDatabase();
            SaveSettings();
            cbDatabase.Enabled = true;
        }
    }
    private void LnkDescriptionLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        Process.Start("http://mrsolution.com.np/");
    }
    private void FrmRestoreDragDrop(object sender, DragEventArgs e)
    {
        try
        {
            txtBakFile.Text = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            GuessDatabase();
            SaveSettings();
        }
        catch
        {
            // ignored
        }
    }

    private void FrmRestoreDragEnter(object sender, DragEventArgs e)
    {
        try
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var data = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (data.Length == 1 && (Path.GetExtension(data[0]).ToLower() is ".bak" or ".zip"))
                {
                    e.Effect = DragDropEffects.Copy;
                    return;
                }
            }
        }
        catch
        {
            // ignored
        }

        e.Effect = DragDropEffects.None;
    }
    private void LinkHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        _ActionTag = "RESTORE";
        var frmPickList =
            new FrmAutoPopList("MAX", "BRLOG", _ActionTag, ObjGlobal.SearchText, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
        }
        else
        {
            MessageBox.Show(@"CANNOT FIND ANY LOG HOSTROY..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    #endregion

    // METHOD FOR THIS FORM
    #region ---------- METHOD FOR THIS FORM ----------
    private void FinishRestoreProcess()
    {
        BtnRestore.Text = @"&RESTORE";
        progressBar.Visible = false;
        lblRestoring.Visible = false;
        lblStatus.Visible = true;
        lblStatus.Text = _status;
        gbSource.Enabled = true;
        gbTarget.Enabled = true;
        _restoreThread = null;
        RefreshSqlServer();
    }
    private static void EnterConsole()
    {
        AttachConsole(1);
    }
    private void GuessDatabase()
    {
        FileInfo fileInfo;
        var text = txtBakFile.Text;
        if (string.IsNullOrEmpty(text)) return;
        try
        {
            fileInfo = new FileInfo(txtBakFile.Text);
        }
        catch (Exception)
        {
            return;
        }

        if (fileInfo.Exists)
        {
            var dbInfo = ServerInfo.Load(_sqlInfo, text);
            if (dbInfo.State != ServerInfo.InfoState.Loaded)
                try
                {
                    if (txtBakFile.Text.Trim().Length != 0)
                    {
                        var empty = string.Empty;
                        var fileName = Path.GetFileName(text.Trim());
                        var str = string.Concat(
                            "(?'db'.+?)(?'year'\\d{4})(?'month'\\d{2})(?'day'\\d{2})(?'hour'\\d{2})(?'minute'\\d{2})(?'suffix'diff|log|logcopy|fullcopy)?\\",
                            fileInfo.Extension);
                        var match = Regex.Match(fileName, str);
                        if (!match.Success)
                            str = string.Concat(
                                "(?'db'.+?)(?'year'\\d{4})(?'month'\\d{2})(?'day'\\d{2})(?'suffix'diff|log|logcopy|fullcopy)?\\",
                                fileInfo.Extension);
                        match = Regex.Match(fileName, str);
                        if (match.Success && int.Parse(match.Groups["year"].Value) > 2007 &&
                            int.Parse(match.Groups["month"].Value) < 13 &&
                            int.Parse(match.Groups["day"].Value) < 32 &&
                            (match.Groups["suffix"] == null || string.IsNullOrEmpty(match.Groups["suffix"].Value) ||
                             match.Groups["suffix"].Value == "fullcopy")) empty = match.Groups["db"].Value;
                        fileName = Path.GetFileNameWithoutExtension(fileName);
                        if (empty == string.Empty)
                        {
                            foreach (var item in cbDatabase.Items)
                            {
                                if (item.ToString().ToLower() != fileName) continue;
                                empty = item.ToString();
                                break;
                            }
                        }

                        if (empty == string.Empty)
                        {
                            foreach (var obj in cbDatabase.Items)
                            {
                                if (!obj.ToString().ToLower().StartsWith(fileName)) continue;
                                empty = obj.ToString();
                                break;
                            }
                        }

                        if (empty != string.Empty) cbDatabase.Text = empty;
                    }
                    else
                    {
                        return;
                    }
                }
                catch (Exception exception1)
                {
                    // ignored
                }
            else
                cbDatabase.Text = dbInfo.Name;
        }

        RefreshRestoreButton();
    }

    public void InitSqlServer()
    {
        if (string.IsNullOrEmpty(_sqlInfo.Server))
        {
            var empty = string.Empty;
            var sqlServerInfo = new SqlServerInfo
            {
                IntegratedSecurity = true
            };
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                sqlServerInfo.Server = ".";
                if (!SqlServer.TestConnection(sqlServerInfo, ref empty, true))
                {
                    sqlServerInfo.Server = ".\\SQLEXPRESS";
                    if (!SqlServer.TestConnection(sqlServerInfo, ref empty, true))
                    {
                        throw new ApplicationException("no sql server found");
                    }
                }
            }
            catch
            {
                sqlServerInfo.Server = string.Empty;
            }
            finally
            {
                Cursor.Current = Cursors.Arrow;
            }
            if (string.IsNullOrEmpty(sqlServerInfo.Server))
            {
                return;
            }
            _sqlInfo = sqlServerInfo;
            SaveSettings();
        }
    }

    private void LoadSettings()
    {
        Settings.Default.SqlServer = GetConnection.ServerDesc;
        Settings.Default.SqlUser = GetConnection.ServerUserId;
        Settings.Default.SqlPassword = GetConnection.ServerUserPsw;
        Settings.Default.Database = GetConnection.LoginInitialCatalog;

        _sqlInfo.Server = Settings.Default.SqlServer;
        _sqlInfo.IntegratedSecurity = Settings.Default.IntergatedSecurity;
        _sqlInfo.UserName = Settings.Default.SqlUser;
        _sqlInfo.Password = Settings.Default.SqlPassword;
        txtBakFile.Text = Settings.Default.BakFile;
        cbDatabase.Text = Settings.Default.Database;
    }

    private void RefreshRestoreButton()
    {
        BtnRestore.Enabled = txtBakFile.Text.Trim().Length > 0 && cbDatabase.Text.Trim().Length > 0 &&
                             _sqlInfo.Server.Trim().Length > 0;
        if (!string.IsNullOrWhiteSpace(cbDatabase.Text)) return;
        cbDatabase.Text = ObjGlobal.InitialCatalog;
    }

    private void RefreshSqlServer()
    {
        lblServer.Text = _sqlInfo.Server;
        lnkConnect.Text = string.IsNullOrEmpty(lblServer.Text) ? "Connect" : "Change";
        var databases = SqlServer.GetDatabases(_sqlInfo);
        cbDatabase.Items.Clear();
        foreach (var database in databases.Where(database => !SqlServer.IsSystemDb(database)))
        {
            cbDatabase.Items.Add(database);
        }
    }

    private void TxtBakFileLeave(object sender, EventArgs e)
    {
        GuessDatabase();
    }

    private void TxtBakFilePreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
    {
        if (e.KeyCode == Keys.Return && cbDatabase.Text.Trim().Length == 0) cbDatabase.Focus();
    }

    private void TxtBakFileTextChanged(object sender, EventArgs e)
    {
        RefreshRestoreButton();
    }

    public static void UpdateProgress(string message, int value)
    {
        try
        {
            if (_me != null)
            {
                var FrmRestore = _me;
                object[] objArray = [message, value];
                FrmRestore.Invoke(new UpdateProgressDelegate((text, progress) =>
                {
                    if (_me.lblRestoring != null && message != null) _me.lblRestoring.Text = text;
                    if (_me.progressBar != null)
                    {
                        _me.progressBar.Style =
                            progress > 0 ? ProgressBarStyle.Continuous : ProgressBarStyle.Marquee;
                        _me.progressBar.Value = progress;
                    }
                }), objArray);
            }
        }
        catch
        {
            //ignore
        }
    }

    [STAThread]
    public void Main()
    {
        //Licenser.LicenseKey = "ZIN42NF46T1BA7A88NA";
        var commandLineArgs = Environment.GetCommandLineArgs();
        var num = 1;
        string str = null;
        string str1 = null;
        string str2 = null;
        string str3 = null;
        var empty = string.Empty;
        var flag = false;
        var flag1 = false;
        var flag2 = false;
        var flag3 = false;
        var empty1 = string.Empty;
        var empty2 = string.Empty;
        var flag4 = false;
        while (commandLineArgs.Length > num)
        {
            var str4 = commandLineArgs[num].ToLower().Trim();
            var str5 = str4;
            if (str4 != null)
            {
                if (str5 == "-help" || str5 == "-?")
                    try
                    {
                        EnterConsole();
                        WriteHeader();
                        WriteHelp();
                        SendKeys.SendWait("{ENTER}");
                        FreeConsole();
                        Environment.Exit(0);
                    }
                    catch (Exception exception)
                    {
                    }
                else if (str5 == "-quiet") flag2 = true;
            }

            if (num != 1 || flag2)
            {
                var str6 = commandLineArgs[num].ToLower().Trim();
                var str7 = str6;
                if (str6 != null)
                    switch (str7)
                    {
                        case "-ext":
                        {
                            flag = true;
                            break;
                        }
                        case "-run":
                        {
                            flag1 = true;
                            break;
                        }
                        case "-db":
                        {
                            if (commandLineArgs.Length <= num + 1) break;
                            var str8 = commandLineArgs[num + 1];
                            char[] chrArray = ['\"', ' '];
                            str1 = str8.Trim(chrArray);
                            break;
                        }
                        case "-srv":
                        {
                            if (commandLineArgs.Length <= num + 1) break;
                            var str9 = commandLineArgs[num + 1];
                            char[] chrArray1 = ['\"', ' '];
                            str2 = str9.Trim(chrArray1);
                            break;
                        }
                        case "-usr":
                        {
                            if (commandLineArgs.Length <= num + 1) break;
                            var str10 = commandLineArgs[num + 1];
                            char[] chrArray2 = ['\"', ' '];
                            str3 = str10.Trim(chrArray2);
                            break;
                        }
                        case "-pwd":
                        {
                            if (commandLineArgs.Length <= num + 1) break;
                            var str11 = commandLineArgs[num + 1];
                            char[] chrArray3 = ['\"', ' '];
                            empty = str11.Trim(chrArray3);
                            break;
                        }
                        case "-copy":
                        {
                            flag3 = true;
                            if (commandLineArgs.Length > num + 1)
                            {
                                var str12 = commandLineArgs[num + 1];
                                char[] chrArray4 = ['\"', ' '];
                                empty1 = str12.Trim(chrArray4);
                            }

                            if (commandLineArgs.Length <= num + 2) break;
                            var str13 = commandLineArgs[num + 2];
                            char[] chrArray5 = ['\"', ' '];
                            empty2 = str13.Trim(chrArray5);
                            break;
                        }
                        case "-force":
                        {
                            flag4 = true;
                            break;
                        }
                    }
            }
            else
            {
                var str14 = commandLineArgs[num];
                char[] chrArray6 = ['\"', ' '];
                str = str14.Trim(chrArray6);
            }

            num++;
        }

        if (!flag1 && !flag2)
        {
            IsGuiMode = true;
            FileToOpen = str;
            if (str1 == null)
            {
                InitDb = GetConnection.LoginInitialCatalog;
                InitServer = GetConnection.ServerDesc;
                InitUser = GetConnection.ServerUserId;
                InitPassword = GetConnection.ServerUserPsw;
            }
            else
            {
                InitDb = str1;
                InitServer = str2;
                InitUser = str3;
                InitPassword = empty;
            }

            return;
        }

        IsGuiMode = false;
        try
        {
            EnterConsole();
        }
        catch (Exception exception1)
        {
            // ignored
        }

        WriteHeader();
        if (flag)
        {
            if (!IsAdmin)
                Console.WriteLine(
                    $@"Cannot assign {".bak"} file association. Please run the application with administrator role.");
            else
                FileAssociation.SetAssociation();
        }

        var num1 = 0;
        if (flag3)
        {
            try
            {
                File.Copy(empty1, empty2, true);
            }
            catch (Exception exception2)
            {
                Console.Error.WriteLine(exception2.Message);
                num1 = -1;
            }
        }

        if (flag2)
        {
            if (num1 == 0) return;
            SendKeys.SendWait("{ENTER}");
            FreeConsole();
            Environment.Exit(num1);

            return;
        }

        try
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new Exception("backup file (.bak or .zip) needs to be specified");
            }
            if (!string.IsNullOrEmpty(empty) && string.IsNullOrEmpty(str3))
            {
                throw new Exception("-usr <user name> option needs to be specified");
            }
            if (string.IsNullOrEmpty(str2))
            {
                throw new Exception("-srv <SQL GetServer name> option needs to be specified");
            }
            if (string.IsNullOrEmpty(str1))
            {
                throw new Exception("-db <target Database> option needs to be specified");
            }
            var restoreParam = new RestoreParams
            {
                BakFile = str,
                Database = str1
            };
            var sqlServerInfo = new SqlServerInfo
            {
                IntegratedSecurity = string.IsNullOrEmpty(str3),
                Server = str2,
                UserName = str3,
                Password = empty
            };
            restoreParam.SqlServerInfo = sqlServerInfo;
            restoreParam.DisconnectApps = flag4;
            SqlServer.Restore(restoreParam);
            Console.WriteLine("{0} Database has been successfully restored from {1}", str1, str);
            num1 = 0;
        }
        catch (Exception exception3)
        {
            Console.WriteLine(string.Concat("ERROR: ", exception3.Message));
            Console.WriteLine();
            Console.WriteLine("Run sqlrestore -? for help");
            num1 = -1;
        }
        finally
        {
            if (!IsGuiMode)
            {
                FreeConsole();
                SendKeys.SendWait("{ENTER}");
            }
        }

        Environment.Exit(num1);
    }

    private static void WriteHeader()
    {
        Console.WriteLine();
        Console.WriteLine("One-Click SQL Restore. A simple tool to restore MS SQL GetServer Database backups.");
        Console.WriteLine("Copyright (С) Pranas.NET. All rights reserved.\n");
    }

    private static void WriteHelp()
    {
        Console.WriteLine("Usage: SqlRestore <backup file ({0} or {1})> [options]\n", ".bak", ".zip");
        Console.WriteLine("Options:");
        Console.WriteLine("-run - runs the program in automatic (console) mode");
        Console.WriteLine("-db <target Database> - a target Database where the backup file will be restored to");
        Console.WriteLine("-srv <SQL GetServer name> - SQL GetServer instance");
        Console.WriteLine(
            "-usr <SQL GetServer user name> - SQL GetServer user name (if not specified, then Windows Authentication will be used)");
        Console.WriteLine(
            "-pwd <user password> > - SQL GetServer user password (if not specified, then empty password will be used)");
        Console.WriteLine("-ext - associates {0} file extension with the application", ".bak");
        Console.WriteLine("-force - disconnect all applications from the Database before restoring");
        Console.WriteLine("-help or -? shows this help");
    }
    private async Task<int> DB_Log()
    {
        _ActionTag = "SAVE";
        _restoreRepository.BackupLog.Log_ID = _ActionTag is "SAVE"
            ? ObjGlobal.ReturnInt(ClsMasterSetup.ReturnMaxIntValue("MASTER.AMS.BR_LOG", "Log_ID").ToString())
            : Log_ID;
        _restoreRepository.BackupLog.DB_NAME = ObjGlobal.InitialCatalog;
        _restoreRepository.BackupLog.COMPANY = ObjGlobal.LogInCompany?.ToUpper();
        _restoreRepository.BackupLog.LOCATION = txtBakFile.Text.Trim().Replace("'", "''");
        _restoreRepository.BackupLog.USED_BY = ObjGlobal.LogInUser.ToUpper();
        _restoreRepository.BackupLog.ACTION = "R";
        _restoreRepository.BackupLog.SyncRowVersion = (short?)(_ActionTag is "SAVE"
            ? ClsMasterSetup.ReturnMaxCountValue("DB_NAME", "MASTER.AMS.BR_LOG", ObjGlobal.InitialCatalog, _restoreRepository.BackupLog.ACTION) + 1
            : 1);
        return await _restoreRepository.SaveBackupAndRestoreDatabaseLog(_ActionTag);
    }

    private async void RestoreThreadProc(object param)
    {
        try
        {
            SqlServer.Restore((RestoreParams)param);
            _status = $"{((RestoreParams)param).Database} DATABASE HAS BEEN SUCCESSFULLY RESTORED..!!";
            await DB_Log();
        }
        catch (ThreadAbortException)
        {
            _status = $"RESTORE OF {((RestoreParams)param).Database} DATABASE HAS BEEN CANCELLED..!!";
        }
        catch (Exception exception1)
        {
            _status = $"RESTORE FAILED FOR {((RestoreParams)param).Database} DATABASE..!!";
            MessageBox.Show(exception1.Message, ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
        finally
        {
            Invoke(new InvoikeDelegate(FinishRestoreProcess));
        }
    }

    private void SaveSettings()
    {
        Settings.Default.SqlServer = _sqlInfo.Server;
        Settings.Default.IntergatedSecurity = _sqlInfo.IntegratedSecurity;
        Settings.Default.SqlUser = _sqlInfo.UserName;
        Settings.Default.SqlPassword = _sqlInfo.Password;
        Settings.Default.BakFile = txtBakFile.Text;
        Settings.Default.Database = cbDatabase.Text;
        Settings.Default.Save();
    }

    private static void SetFileAssociation()
    {
        try
        {
            var uri = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase);
            var processStartInfo = new ProcessStartInfo
            {
                UseShellExecute = true,
                WorkingDirectory = Environment.CurrentDirectory,
                FileName = uri.LocalPath,
                Arguments = "-quiet -ext"
            };
            var processStartInfo1 = processStartInfo;
            if (Environment.OSVersion.Version.Major >= 6) processStartInfo1.Verb = "runas";
            Process.Start(processStartInfo1)?.WaitForExit();
        }
        catch (Win32Exception win32Exception)
        {
            MessageBox.Show(win32Exception.Message);
        }
    }

    public static void SetStatus(string status)
    {
        if (_me != null)
        {
            var frmRestore = _me;

            static void SetStatusDelegate(string text)
            {
                if (_me.lblRestoring != null) _me.lblRestoring.Text = text;
            }

            object[] objArray = [status];
            frmRestore.Invoke((SetStatusDelegate)SetStatusDelegate, objArray);
        }
        else
        {
            Console.WriteLine(status);
        }
    }

    #endregion



    // OBJECT FOR THIS FORM
    #region ---------- OBJECT FOR THIS FORM ----------
    private delegate void InvoikeDelegate();
    private delegate void SetStatusDelegate(string text);
    private delegate void UpdateProgressDelegate(string message, int value);
    private const uint ATTACH_PARENT_PROCESS = 4294967295;
    public const string BackupFileExtension = ".bak";
    public const string ZipFileExtension = ".zip";
    public static string AppTitle;
    public static string FileToOpen;
    public static string InitDb;
    public static string InitServer;
    public static string InitUser;
    public static string InitPassword;
    private static FrmRestore _me;
    private static bool IsAdmin1 = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
    private string _ActionTag;
    private Thread _restoreThread;
    private SqlServerInfo _sqlInfo = new();
    private string _status = "";
    private Image _statusImage;
    private DataTable dt = new();
    public int Log_ID = 0;
    private IBackupRestore _restoreRepository;
    private string Query = string.Empty;
    public static bool IsGuiMode { get; private set; }
    public static bool IsAdmin => IsAdmin1;
    [DllImport("kernel32.dll", CharSet = CharSet.None, ExactSpelling = false, SetLastError = true)]
    private static extern bool FreeConsole();

    [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false, SetLastError = true)]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

    [DllImport("kernel32.dll", CharSet = CharSet.None, ExactSpelling = false, SetLastError = true)]
    private static extern bool AllocConsole();

    [DllImport("kernel32", CharSet = CharSet.None, ExactSpelling = false, SetLastError = true)]
    private static extern bool AttachConsole(uint dwProcessId);
    #endregion
}