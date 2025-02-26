using System.Collections.Generic;

namespace MrDAL.Models.Common;

public struct ListResult<T>
{
    public bool Success { get; set; }
    public IReadOnlyList<T> List { get; set; }
    public string ErrorMessage { get; set; }
    public ResultType ResultType { get; set; }
    public bool IsReCall { get; set; }
}