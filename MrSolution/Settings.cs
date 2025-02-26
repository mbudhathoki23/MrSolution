namespace MrSolution;

internal sealed class Settings
{
    public class Default
    {
        public static string SqlServer { get; }
        public static string ServerUser { get; set; }
        public static string ServerPassword { get; set; }
        public static bool runMode { get; set; } = false;
    }
}