using MrSolutionTable.Methods;

namespace MrSolutionTable;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        ClsGlobal.ConnectionCheck();
        Application.Run(new FrmPocoGenerator());
    }
}