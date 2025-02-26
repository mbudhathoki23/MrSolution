using System;

namespace MrDAL.Core.Utils;

public interface IAppLogger
{
    void LogError(object sender, Exception e, string message);

    void LogError(Exception e, string message);

    void LogError(string message, string dump);

    void LogInfo(string message);

    void LogWarning(string message, string dump);

    void LogWarning(string message);
}