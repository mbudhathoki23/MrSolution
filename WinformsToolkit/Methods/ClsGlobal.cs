using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace MrSolutionTable.Methods;

public abstract class ClsGlobal
{
    public static string ServerDesc = string.Empty;
    public static string LoginInitialCatalog = string.Empty;
    public static string ServerUserId = string.Empty;
    public static string ServerUserPsw = string.Empty;

    public static string WinConnection => $"Server ={ServerDesc}; Database={LoginInitialCatalog};Integrated Security=SSPI;";
    public static string ConnectionString => $"data source={ServerDesc}; Initial Catalog={LoginInitialCatalog}; User Id={ServerUserId}; pwd={ServerUserPsw};MultipleActiveResultSets=True;Connection Timeout= 500";

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

                    ServerDesc = split[0];
                    ServerUserId = split[1];
                    ServerUserPsw = split[2];

                    var con = new SqlConnection(ConnectionString);
                    con.Open();
                    if (con.State == ConnectionState.Open)
                    {
                        return true;
                    }
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
            return true;
        }

        return true;
    }
}