namespace MrDAL;

public sealed class Settings
{
    public class Default
    {
        public static string SqlServer { get; }
        public static string ServerUser { get; set; }
        public static string ServerPassword { get; set; }
        public static bool RunMode { get; set; } = false;
    }
}