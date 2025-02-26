using MrDAL.Global.Control;
using NLog;
using System;
using System.IO;

namespace MrDAL.Core.Logging;

internal class NLogWrapper : ILogEngine
{
    private static readonly Logger Logger = LogManager.GetLogger("jsonLogger");

    public NLogWrapper(bool desktop = false)
    {
        try
        {
            if (desktop)
            {
                var varPath =
                    $@"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\MrSolution";
                if (!Directory.Exists(varPath)) Directory.CreateDirectory(varPath);
                LogManager.Configuration.Variables["logDirectory"] = varPath;
            }
            else
            {
                LogManager.Configuration.Variables["logDirectory"] = Environment.CurrentDirectory;
            }
        }
        catch (Exception e)
        {
            e.DialogResult();
        }
    }

    public void LogError(Exception e, string message)
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