namespace MrSolutionTable.Methods;

public class RemoteCommandMessageModel
{
    public string Payload { get; }
    public string? ErrorMessage { get; }
    public RemoteCommandType CommandType { get; }
    public string? ErrorDump { get; }
    public bool Success { get; set; }

    public RemoteCommandMessageModel(bool success, string message, string? errorMessage,
        RemoteCommandType commandType, string? errorDump)
    {
        Success = success;
        Payload = message;
        ErrorMessage = errorMessage;
        CommandType = commandType;
        ErrorDump = errorDump;
    }
}