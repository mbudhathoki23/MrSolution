using System.Management;

namespace MrDAL.Core.Utils;

public class FingerPrint
{
    private static string _cachedUuId;

    public static string GetMotherboardUuId()
    {
        if (!string.IsNullOrWhiteSpace(_cachedUuId)) return _cachedUuId;
        var uuid = string.Empty;
        var mClass = new ManagementClass("Win32_ComputerSystemProduct");
        var mCollection = mClass.GetInstances();

        foreach (var mObject in mCollection)
        {
            var mo = (ManagementObject)mObject;
            uuid = mo.Properties["UUID"].Value.ToString();
            break;
        }

        if (!string.IsNullOrWhiteSpace(uuid)) _cachedUuId = uuid;
        return uuid;
    }
}