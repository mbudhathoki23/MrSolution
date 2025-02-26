using Microsoft.Win32;
using System.Reflection;

namespace MrBLL.Utility.Restore.Properties;

public static class FileAssociation
{
    private const string AssociationKey = "SQLRestore";

    public static bool CheckAssociation()
    {
        var registryKey = Registry.ClassesRoot.OpenSubKey(".bak");
        if (registryKey == null) return false;
        if (registryKey.GetValue(string.Empty, string.Empty).ToString() != "SQLRestore")
        {
            registryKey.Close();
            return false;
        }

        registryKey.Close();
        registryKey = Registry.ClassesRoot.OpenSubKey("SQLRestore\\shell\\open\\command");
        if (registryKey == null) return false;
        var lower = string.Format("{0} \"%1\"", Assembly.GetExecutingAssembly().Location).ToLower();
        if (registryKey.GetValue("", "").ToString().ToLower() != lower)
        {
            registryKey.Close();
            return false;
        }

        registryKey.Close();
        return true;
    }

    public static void SetAssociation()
    {
        var registryKey = Registry.ClassesRoot.CreateSubKey(".bak");
        if (registryKey == null) return;
        registryKey.SetValue(string.Empty, "SQLRestore", RegistryValueKind.String);
        registryKey.Close();
        var registryKey1 = Registry.ClassesRoot.CreateSubKey("SQLRestore");
        if (registryKey1 == null) return;
        registryKey1.SetValue(string.Empty, "SQL GetServer Database Backup", RegistryValueKind.String);
        registryKey = registryKey1.CreateSubKey("DefaultIcon");
        if (registryKey == null) return;
        registryKey.SetValue(string.Empty, string.Format("{0}", Assembly.GetExecutingAssembly().Location),
            RegistryValueKind.String);
        registryKey.Close();
        registryKey = registryKey1.CreateSubKey("shell\\open\\command");
        if (registryKey == null) return;
        registryKey.SetValue(string.Empty, string.Format("{0} \"%1\"", Assembly.GetExecutingAssembly().Location),
            RegistryValueKind.String);
        registryKey.Close();
        registryKey1.Close();
    }
}