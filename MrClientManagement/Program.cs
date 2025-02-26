using MrClientManagement.Server;
using MrDAL.Utility.Server;
using System;
using System.Windows.Forms;

namespace MrClientManagement
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!GetConnection.ConnectionCheck())
            {
                var result = new FrmMultiServer(false);
                result.ShowDialog();
            }
            else
            {
                Application.Run(new MainClientInformation());
            }
        }
    }
}