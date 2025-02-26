using MrBLL.Utility.ServerConnection;
using MrDAL.Core.Logging;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Utility.dbMaster;
using MrDAL.Utility.Server;
using SiyZo.About;
using System;
using System.Threading;
using System.Windows.Forms;

namespace SiyZo
{
    internal static class Program
    {
        [STAThread]
        [Obsolete]
        private static void Main()
        {
            ObjGlobal.Project = "SIYZO";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LogEngineFactory.SetDefaultEngine(LogEngineE.NLog);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            ClsDateMiti.ChangeDateFormat();
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
                if (dialogResult == DialogResult.Yes)
                {
                    AlterDatabaseTable.AlterMasterTable();
                    Application.Run(new MdiSiyZo());
                }
            }
            else
            {
                Application.Exit();
            }
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
}