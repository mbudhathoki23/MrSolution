using Dapper;
using MrDAL.Core.Utils;
using MrDAL.Domains.Shared.AppLogger.Models;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace MrDAL.Domains.Reports;

public class AppLogReportService
{
    public async Task<ListResult<AppLogReportModel>> GetLogReportAsync(AppLogContext? context, int? branchId, DateTime? dateFrom, DateTime? dateTo, AppLogType? logType, AppLogGroup? logGroup, AppLogActionType? actionType, string appUser = null)
    {
        var result = new ListResult<AppLogReportModel>();

        var parameters = new DynamicParameters();
        var query = new StringBuilder(
            @"SELECT
	                    lg.Id, lg.Description, lg.log_type AS LogType, lg.log_type_alias AS LogTypeAlias, lg.log_group AS LogGroup, lg.log_group_alias AS LogGroupAlias, lg.action_type AS ActionType,
	                    lg.action_type_alias AS ActionTypeAlias, lg.action_time AS ActionType, lg.branch_id AS BranchId, lg.enter_by AS EnterBy,
	                    lg.old_value AS OldValueXml, lg.new_value AS NewValueXml, lg.ref_no AS RefNo, lg.ref_id AS RefId, lg.ref_type AS RefType, lg.ref_type_alias AS RefTypeAlias, lg.is_audit AS IsAudit
                    FROM AMS.AppLog lg WHERE 1 = 1 ");

        if (dateFrom.HasValue)
        {
            query.AppendLine("AND CONVERT(DATE, lg.action_time) >= @prFrom ");
            parameters.Add(@"prFrom", dateFrom.Value.Date);
        }

        if (dateTo.HasValue)
        {
            query.AppendLine("AND CONVERT(DATE, lg.action_time) <= @prTo ");
            parameters.Add(@"prFrom", dateTo.Value.Date);
        }

        if (context.HasValue)
            switch (context.Value)
            {
                case AppLogContext.UserActivity:
                    query.AppendLine("AND lg.is_audit = 0 ");
                    break;

                case AppLogContext.Audit:
                    query.AppendLine("AND lg.is_audit = 1 ");
                    break;

                case AppLogContext.UserActivityWithAudit:
                    break;
            }

        if (branchId.HasValue)
        {
            query.AppendLine("AND lg.branch_id = @prBranchId ");
            parameters.Add("prBranchId", branchId.Value);
        }

        if (logType.HasValue)
        {
            query.AppendLine("AND lg.log_type_alias = @prLogType ");
            parameters.Add("prLogType", (short)logType.Value);
        }

        if (logGroup.HasValue)
        {
            query = query.AppendLine("AND lg.log_group_alias = @prGroup ");
            parameters.Add("prGroup", (byte)logGroup);
        }

        if (actionType.HasValue)
        {
            query.AppendLine("AND lg.action_type_alias = @prActionType ");
            parameters.Add("prActionType", (byte)actionType.Value);
        }

        if (!string.IsNullOrWhiteSpace(appUser))
        {
            query.AppendLine("AND lg.enter_by = @prUser ");
            parameters.Add("prUser", appUser);
        }

        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            result.List = (await conn.QueryAsync<AppLogReportModel>(query.ToString(), parameters)).AsList();
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToListErrorResult<AppLogReportModel>(this);
        }

        return result;
    }
}