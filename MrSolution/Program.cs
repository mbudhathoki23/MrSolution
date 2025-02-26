using MrBLL.Utility.ServerConnection;
using MrDAL.Core.Logging;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Setup.BackupRestore;
using MrDAL.Utility.dbMaster;
using MrDAL.Utility.Server;
using MrSolution.About;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.Application;

namespace MrSolution;

internal static class Program
{
    [STAThread]
    [Obsolete]
    private static void Main()
    {
        EnableVisualStyles();
        SetCompatibleTextRenderingDefault(false);
        AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        LogEngineFactory.SetDefaultEngine(LogEngineE.NLog);
        ThreadException += Application_ThreadException;
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        ObjGlobal.Caption = @"MRSOLUTION";
        if (!Debugger.IsAttached)
        {
            AppUpdater.CheckUpdates(false);
        }
        ClsDateMiti.ChangeDateFormat();
        ObjGlobal.GetSerialNo();
        ObjGlobal.GetServerName();
        ObjGlobal.GetLocalMacAddress();
        ObjGlobal.GetIpAddress();
        var frm = new FrmSplashScreen();
        if (frm.ShowDialog() == DialogResult.Yes)
        {
            DialogResult dialogResult;
            if (!GetConnection.ConnectionCheck())
            {
                var result = new FrmMultiServer(false);
                result.ShowDialog();
                dialogResult = result.DialogResult;
            }
            else
            {
                ObjGlobal.GetMacAddress();
                ObjGlobal.ServerVersion = GetConnection.GetSqlServerVersion();
                if (ObjGlobal.MultiServerOption)
                {
                    var result = new FrmMultiServer(false);
                    result.ShowDialog();
                    dialogResult = result.DialogResult;
                }
                else
                {
                    dialogResult = DialogResult.Yes;
                }
            }
            if (dialogResult != DialogResult.Yes)
            {
                Exit();
            }
            AlterDatabaseTable.AlterMasterTable();
            Run(new MdiMrSolution());
        }
        else
        {
            Exit();
        }
    }

    private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
    {
        var currentAssembly = Assembly.GetExecutingAssembly();
        var requiredDllName = $@"{new AssemblyName(args.Name).Name}.dll";
        var resource = currentAssembly.GetManifestResourceNames().FirstOrDefault(x => x.EndsWith(requiredDllName));
        if (resource == null)
        {
            return null;
        }
        using var stream = currentAssembly.GetManifestResourceStream(resource);
        if (stream == null) return null;

        var block = new byte[stream.Length];
        var read = stream.Read(block, 0, block.Length);
        return Assembly.Load(block);
    }

    private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        LogAsync(e.ExceptionObject as Exception);
    }

    private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
    {
        e.Exception.DialogResult();
        LogAsync(e.Exception);
    }

    private static void LogAsync(Exception e)
    {
        LogEngineFactory.GetLogEngine(LogEngineE.NLog).LogError(e, e.Message);
    }
}