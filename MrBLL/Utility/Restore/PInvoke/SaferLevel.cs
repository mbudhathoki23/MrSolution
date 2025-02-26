namespace MrBLL.Utility.Restore.PInvoke;

public enum SaferLevel : uint
{
    Disallowed = 0,
    Untrusted = 4096,
    Constrained = 65536,
    NormalUser = 131072,
    FullyTrusted = 262144
}