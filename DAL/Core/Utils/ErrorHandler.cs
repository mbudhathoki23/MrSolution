using MrDAL.Core.Logging;
using MrDAL.Models.Common;
using System;
using System.Data.SqlClient;
using MrDAL.Core.Extensions;

namespace MrDAL.Core.Utils;

public static class ErrorHandler
{
    public static InfoResult<T> InternalError<T>(string message = null)
    {
        var result = new InfoResult<T>
        {
            ErrorMessage = string.IsNullOrWhiteSpace(message) ? "Internal error occurred. " : message,
            ResultType = ResultType.InternalError
        };

        return result;
    }
    public static InfoResult<T> ValidationError<T>(string message)
    {
        return new InfoResult<T>
        {
            ErrorMessage = message,
            ResultType = ResultType.ValidationError
        };
    }
    public static InfoResult<T> ToInfoErrorResult<T>(this Exception exception, object sender, bool makeLog = true, string userMessage = null)
    {
        var result = new InfoResult<T>();
        var definition = GetErrorDefinition(sender, exception);

        switch (definition.ErrorType)
        {
            case ErrorType.ForeignKeyConflict:
                {
                    makeLog = true;
                    result.ResultType = ResultType.ReferenceConflicted;
                    break;
                }
            case ErrorType.OtherSqlException:
                {
                    makeLog = true;
                    result.ResultType = ResultType.DataAccessError;
                    break;
                }
            case ErrorType.OtherException:
                {
                    makeLog = true;
                    result.ResultType = ResultType.InternalError;
                    break;
                }
            case ErrorType.UniqueKeyViolation:
                {
                    makeLog = false;
                    result.ResultType = ResultType.UniqueValueViolation;
                    break;
                }
            default:
                {
                    makeLog = true;
                    result.ResultType = ResultType.InternalError;
                    break;
                }
        }

        var message = string.IsNullOrWhiteSpace(userMessage)
            ? definition.Message
            : definition.Message + Environment.NewLine + userMessage;

        if (makeLog) LogEngineFactory.GetDefaultEngine().LogError(exception, message);

        result.ErrorMessage = definition.Message;
        return result;
    }
    public static ListResult<T> ToListErrorResult<T>(this Exception exception, object sender, bool makeLog = true, string userMessage = null)
    {
        var result = new ListResult<T>();
        var definition = GetErrorDefinition(sender, exception);

        switch (definition.ErrorType)
        {
            case ErrorType.ForeignKeyConflict:
                {
                    makeLog = true;
                    result.ResultType = ResultType.ReferenceConflicted;
                    break;
                }
            case ErrorType.OtherSqlException:
                {
                    makeLog = true;
                    result.ResultType = ResultType.DataAccessError;
                    break;
                }
            case ErrorType.OtherException:
                {
                    makeLog = true;
                    result.ResultType = ResultType.InternalError;
                    break;
                }
            case ErrorType.UniqueKeyViolation:
                makeLog = false;
                result.ResultType = ResultType.UniqueValueViolation;
                break;

            default:
                makeLog = true;
                result.ResultType = ResultType.InternalError;
                break;
        }

        var message = string.IsNullOrWhiteSpace(userMessage)
            ? definition.Message
            : definition.Message + Environment.NewLine + userMessage;

        if (makeLog) LogEngineFactory.GetDefaultEngine().LogError(exception, message);

        result.ErrorMessage = definition.Message;
        return result;
    }

    public static NonQueryResult ToNonQueryErrorResult(this Exception exception, object sender, bool makeLog = true, string userMessage = null)
    {
        var result = new NonQueryResult();
        var definition = GetErrorDefinition(sender, exception);
        switch (definition)
        {
            case { ErrorType: ErrorType.ForeignKeyConflict }:
                {
                    makeLog = true;
                    result.ResultType = ResultType.ReferenceConflicted;
                    break;
                }
            case { ErrorType: ErrorType.OtherSqlException }:
                {
                    makeLog = true;
                    result.ResultType = ResultType.DataAccessError;
                    break;
                }
            case { ErrorType: ErrorType.OtherException }:
                {
                    makeLog = true;
                    result.ResultType = ResultType.InternalError;
                    break;
                }
            case { ErrorType: ErrorType.UniqueKeyViolation }:
                {
                    makeLog = true;
                    result.ResultType = ResultType.UniqueValueViolation;
                    break;
                }
            default:
                {
                    makeLog = true;
                    result.ResultType = ResultType.InternalError;
                    break;
                }
        }

        var message = userMessage.IsBlankOrEmpty()
            ? definition.Message
            : definition.Message + Environment.NewLine + userMessage;

        LogEngineFactory.GetDefaultEngine().LogError(exception, message);
        result.ErrorMessage = definition.Message;
        return result;
    }
    public static NonQueryResult NonQueryInternalError(string message = null)
    {
        var result = new NonQueryResult
        {
            ErrorMessage = message ?? "Internal error occurred.",
            ResultType = ResultType.InternalError
        };

        return result;
    }
    public static NonQueryResult ValidationError(string message)
    {
        return new NonQueryResult(false, false, message, ResultType.ValidationError);
    }
    public static NonQueryResult EntityNotExists(string message)
    {
        return new NonQueryResult(false, false, message, ResultType.EntityNotExists);
    }
    
    private static ErrorDefinition GetErrorDefinition(object sender, Exception exception)
    {
        while (exception.InnerException != null)
            exception = exception.InnerException;

        string code = string.Empty, message;
        var errorType = ErrorType.OtherException;

        var exType = exception.GetType();
        if (exType == typeof(SqlException))
        {
            var sqlException = (SqlException)exception;
            code = sqlException.Number.ToString();
            message = sqlException.Message;

            errorType = sqlException.Number switch
            {
                2601 or 2627 => ErrorType.UniqueKeyViolation,
                547 => ErrorType.ForeignKeyConflict,
                _ => ErrorType.OtherSqlException
            };
        }
        else
        {
            message = exception.Message;
        }

        return new ErrorDefinition(message, code, exception.GetType(), exception.ToString(), errorType, sender.GetType());
    }
}