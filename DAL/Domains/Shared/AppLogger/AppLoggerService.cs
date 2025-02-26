using DatabaseModule.CloudSync;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Domains.Shared.AppLogger.Models;
using MrDAL.Global.Common;
using MrDAL.Lib.Dapper.Contrib;
using MrDAL.Utility.Server;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.AppLogger;

public static class AppLoggerService
{
    public static void LogAsync(AppLogContext context, string description, AppLogType logType,
        AppLogActionType actionType, AppLogRefType refType, string refId, string refCode, object oldValue,
        object newValue, int? branchId)
    {
        Task.Run(() =>
        {
            try
            {
                var oldValueXml = oldValue == null ? null :
                    oldValue is string old ? old : XmlUtils.SerializeToXml(oldValue);
                var newValueXml = newValue == null ? null :
                    newValue is string @new ? @new : XmlUtils.SerializeToXml(newValue);
                var logTypeInfo = AppLogTypeCollection.GetAppLogType(logType);

                var model = new AppLog
                {
                    ActionTime = DateTime.Now,
                    BranchId = branchId,
                    LogDescription = description,
                    EnterBy = ObjGlobal.LogInUser,
                    RefId = refId,
                    RefVno = refCode,
                    OldValue = oldValueXml,
                    NewValue = newValueXml,
                    LogGroup = logTypeInfo.LogGroup.GetDescription(),
                    LogGroupAlias = (byte)logTypeInfo.LogGroup,
                    LogType = logType.GetDescription(),
                    LogTypeAlias = (byte)logType,
                    RefType = refType.GetDescription(),
                    RefTypeAlias = (byte)refType,
                    IsAudit = context is AppLogContext.Audit or AppLogContext.UserActivityWithAudit,
                    ActionTypeAlias = (byte)actionType,
                    ActionType = actionType.GetDescription()
                };

                using var conn = new SqlConnection(GetConnection.ConnectionString);
                conn.Insert(model);
            }
            catch (Exception e)
            {
                e.ToNonQueryErrorResult("AppLoggerService");
            }
        });
    }
}