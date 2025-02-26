using System;

namespace MrDAL.Core.Logging;

public abstract class LogEngineFactory
{
    private static LogEngineE DefaultEngine { get; set; }

    public static ILogEngine GetLogEngine(LogEngineE engine)
    {
        try
        {
            return engine switch
            {
                LogEngineE.Sqlite => new SqliteWrapper(),
                LogEngineE.NLog => new NLogWrapper(),
                _ => throw new ArgumentOutOfRangeException(nameof(engine), engine, null)
            };
        }
        catch (Exception e)
        {
            throw new ArgumentOutOfRangeException(nameof(engine), engine, e.Message);
        }
    }

    public static void SetDefaultEngine(LogEngineE engine)
    {
        DefaultEngine = engine;
    }

    public static ILogEngine GetDefaultEngine()
    {
        return GetLogEngine(DefaultEngine);
    }
}