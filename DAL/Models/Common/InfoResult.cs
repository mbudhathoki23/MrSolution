namespace MrDAL.Models.Common;

public struct InfoResult<T>
{
    public bool Success;
    public T Model { get; set; }
    public string ErrorMessage { get; set; }
    public ResultType ResultType { get; set; }
}