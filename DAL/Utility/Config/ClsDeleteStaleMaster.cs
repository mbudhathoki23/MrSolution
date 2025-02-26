using MrDAL.Utility.Server;
using System.Text;

namespace MrDAL.Utility.Config;

public class ClsDeleteStaleMaster
{
    public ClsDeleteStaleMaster(string DBConString)
    {
    }

    public string DeleteGeneralLedger(string code, string table, string colCode)
    {
        string str;
        try
        {
            var cmdString = new StringBuilder();
            cmdString.Append("BEGIN TRANSACTION \n");
            if (table == "Product")
                cmdString.Append(string.Concat("DELETE from Product_Picture Where P_Code = '", code, "' \n"));
            var strArrays = new[] { "DELETE from ", table, " Where ", colCode, " = '", code, "' \n" };
            cmdString.Append(string.Concat(strArrays));
            cmdString.Append("ROLLBACK TRANSACTION \n");
            SqlExtensions.ExecuteNonQuery(cmdString.ToString());
            str = code;
        }
        catch
        {
            str = string.Empty;
        }

        return str;
    }
}