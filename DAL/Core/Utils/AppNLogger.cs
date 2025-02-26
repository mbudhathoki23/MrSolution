using NLog;
using System;

namespace MrDAL.Core.Utils;

public class AppNLogger : IAppLogger
{
    private static readonly Logger Logger = LogManager.GetLogger("jsonLogger");

    public void LogError(Exception e, string message)
    {
        var eventInfo = new LogEventInfo(LogLevel.Error, Logger.Name, message)
        {
            Exception = e
        };
        Logger.Log(eventInfo);
    }

    public void LogError(object sender, Exception e, string message)
    {
        var eventInfo = new LogEventInfo(LogLevel.Error, Logger.Name, message)
        {
            Exception = e
        };
        Logger.Log(eventInfo);
    }

    public void LogError(string message, string dump)
    {
        var eventInfo = new LogEventInfo(LogLevel.Error, Logger.Name, message)
        {
            Parameters = new object[] { dump }
        };
        Logger.Log(eventInfo);
    }

    public void LogInfo(string message)
    {
        Logger.Info(message);
    }

    public void LogWarning(string message, string dump)
    {
        var eventInfo = new LogEventInfo(LogLevel.Warn, Logger.Name, message)
        {
            Parameters = new object[] { dump }
        };
        Logger.Log(eventInfo);
    }

    public void LogWarning(string message)
    {
        Logger.Warn(message);
    }
}