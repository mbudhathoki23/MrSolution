using System;

namespace MrDAL.Core.Utils;

internal readonly struct ErrorDefinition(string message, string code, Type type, string dump, ErrorType errorType, Type classType)
{
    public string Message { get; } = message;
    public string Code { get; } = code;
    public Type ExceptionType { get; } = type;
    public string Dump { get; } = dump;
    public ErrorType ErrorType { get; } = errorType;
    public Type ClassType { get; } = classType;
}