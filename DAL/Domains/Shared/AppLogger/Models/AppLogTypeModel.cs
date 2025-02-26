using MrDAL.Core.Extensions;

namespace MrDAL.Domains.Shared.AppLogger.Models;

public class AppLogTypeModel(AppLogType logTypeE, AppLogGroup logGroupE)
{
    public AppLogType LogTypeE { get; set; } = logTypeE;
    public string LogType { get; set; } = logTypeE.GetDescription();
    public AppLogGroup LogGroup { get; set; } = logGroupE;
}