namespace MrDAL.Models.Common;

public struct NonQueryResult
{
    public bool Completed { get; set; }
    public bool Value { get; set; }
    public string ErrorMessage { get; set; }
    public ResultType ResultType { get; set; }

    public NonQueryResult(bool completed, bool value, string errorMessage, ResultType resultType)
    {
        Completed = completed;
        Value = value;
        ErrorMessage = errorMessage;
        ResultType = resultType;
    }
}