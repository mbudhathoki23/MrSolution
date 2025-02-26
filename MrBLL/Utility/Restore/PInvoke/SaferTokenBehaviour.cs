namespace MrBLL.Utility.Restore.PInvoke;

public enum SaferTokenBehaviour : uint
{
    Default = 0,
    NullIfEqual = 1,
    CompareOnly = 2,
    MakeInert = 4,
    WantFlags = 8
}