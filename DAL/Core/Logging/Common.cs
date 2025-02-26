using System;

namespace MrDAL.Core.Logging;

public static class Common
{
    public static string SqliteConnString =>
        $@"Data Source={Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\MrSolution\Logs.db;Version=3;";
}