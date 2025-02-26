using DatabaseModule.CloudSync;
using MrDAL.Reports.Interface;
using MrDAL.Utility.Server;
using System.Text;

namespace MrDAL.Reports.Common;

public class ApiErrorLog : IApiErrorLog
{
    public ApiErrorLog()
    {
        ObjLog = new SyncApiErrorLog();
    }

    public int SaveErrorLog()
    {
        var cmdString = new StringBuilder();
        cmdString.Append(" INSERT INTO AMS.SyncApiErrorLog(Module, Created, ErrorMessage, ErrorType) ");
        cmdString.Append("\n VALUES \n");
        cmdString.Append($"'{ObjLog.Module}',{ObjLog.Created},'{ObjLog.ErrorMessage}','{ObjLog.ErrorType}'");

        var result = SqlExtensions.ExecuteNonTrans(cmdString);

        return result;
    }


    public SyncApiErrorLog ObjLog { get; set; }
}