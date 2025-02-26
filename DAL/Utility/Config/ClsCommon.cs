using MrDAL.Utility.Server;
using System.Text;

namespace MrDAL.Utility.Config;

public class ClsCommon
{
    public string GenerateShortName(string val, string description, string shortName, string tableName)
    {
        var ShortName = string.Empty;
        if (string.IsNullOrEmpty(val)) return ShortName;
        var cmdString = new StringBuilder();
        cmdString.Append("declare @c int \n");
        cmdString.Append(
            $"set @c=(SELECT TOP 1 (Right({shortName},5)+1) AS D  FROM AMS.{tableName} where left({shortName},2) =  Left('{val}', 2)  and len({shortName}) = 7  AND ISNUMERIC(Right({shortName},5))=1 ORDER BY D DESC ) \n");
        cmdString.Append(
            $"Select top(1) Left({description},2)+CASE WHEN ISNULL(LEN(@c),1)=1 THEN '0000' WHEN LEN(@c)=2 THEN '000' WHEN LEN(@c)=3 THEN '00' WHEN LEN(@c)=4 THEN '0' WHEN LEN(@c)=5 THEN '' END+ convert(varchar(5),(Right({shortName},5))+1) as ShortName   \n");
        cmdString.Append(
            $"from AMS.{tableName} where left({shortName},2) =  Left('{val}', 2)  and len({shortName}) = 7   AND ISNUMERIC(Right({shortName},5))=1 \n");
        cmdString.Append("order by " + shortName + " Desc");
        var dt = SqlExtensions
            .ExecuteDataSet(cmdString.ToString()).Tables[0];
        if (dt.Rows.Count > 0) ShortName = dt.Rows[0]["ShortName"].ToString();
        if (ShortName != string.Empty) return ShortName;
        return val.Length == 1 ? val.Substring(0, 1) + "00001" : val.Substring(0, 2) + "00001";
    }

    public int CheckDescriptionDuplicateRecord(string desc, string tableName, string retColumnName,
        string idColumnName, int id = 0)
    {
        var dt = SqlExtensions.ExecuteDataSet(
                $"select top 1 {retColumnName} from AMS.{tableName} where {retColumnName} = '{desc.Trim()}' AND {idColumnName} <> {id}")
            .Tables[0];
        return dt.Rows.Count > 0 ? 1 : 0;
    }

    public int CheckShortNameDuplicateRecord(string shortName, string tableName, string retColumnName,
        string idColumnName, int id = 0)
    {
        var dt = SqlExtensions.ExecuteDataSet(
                $"select top 1 {retColumnName} from AMS.{tableName} where {retColumnName} = '{shortName.Trim()}' AND {idColumnName} <> {id}")
            .Tables[0];
        return dt.Rows.Count > 0 ? 1 : 0;
    }
}