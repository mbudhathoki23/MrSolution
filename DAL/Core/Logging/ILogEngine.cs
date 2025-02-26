using System;

namespace MrDAL.Core.Logging;

public interface ILogEngine
{
    void LogError(Exception e, string message);

    void LogError(string message, string dump);

    void LogInfo(string message);

    void LogWarning(string message, string dump);

    void LogWarning(string message);
}