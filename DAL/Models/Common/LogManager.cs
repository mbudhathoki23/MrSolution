using System;
using System.Diagnostics;

namespace MrDAL.Models.Common;

/// <summary>
///     Setup and implements logs for internal logging
/// </summary>
internal class LogManager
{
    /// <summary>
    ///     Define logging message categories
    /// </summary>
    public enum Categories
    {
        Info = 1,
        Warning,
        Error,
        Exception
    }

    private int useFrame = 1;

    /// <summary>
    ///     Constructor, allow user to override path and name of logging file
    /// </summary>
    /// <param name="userbasepath"></param>
    /// <param name="userlogname"></param>
    public LogManager(string userbasepath, string userlogname)
    {
        BasePath = string.IsNullOrEmpty(userbasepath) ? "." : userbasepath;
        LogNameHeader = string.IsNullOrEmpty(userlogname) ? "MsgLog" : userlogname;

        Log(Categories.Info, "********************* New Trace *********************");
    }

    /// <summary>
    ///     Path to log file
    /// </summary>
    public string BasePath { get; set; }

    /// <summary>
    ///     Header for log file name
    /// </summary>
    public string LogNameHeader { get; set; }

    /// <summary>
    ///     Log a message, using the provided category
    /// </summary>
    /// <param name="category"></param>
    /// <param name="msg"></param>
    public void Log(Categories category, string msg)
    {
        // get call stack
        var stackTrace = new StackTrace();

        // get calling method name
        var caller = stackTrace.GetFrame(useFrame).GetMethod().Name;

        // log it
        LogWriter.Write(caller, category, msg, BasePath, LogNameHeader);

        // reset frame pointer
        useFrame = 1;
    }

    /// <summary>
    ///     Log an informational message
    /// </summary>
    /// <param name="msg"></param>
    public void LogInfoMsg(string msg)
    {
        useFrame++; // bump up the stack frame pointer to skip this entry
        Log(Categories.Info, msg);
    }

    /// <summary>
    ///     Log an error message
    /// </summary>
    /// <param name="msg"></param>
    public void LogErrorMsg(string msg)
    {
        useFrame++; // bump up the stack frame pointer to skip this entry
        Log(Categories.Error, msg);
    }

    /// <summary>
    ///     Log an exception
    /// </summary>
    /// <param name="ex"></param>
    public void Log(Exception ex)
    {
        useFrame++; // bump up the stack frame pointer to skip this entry
        Log(Categories.Exception, $"{ex.Message} from {ex.Source}");
    }
}