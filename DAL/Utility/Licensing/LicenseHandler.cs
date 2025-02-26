using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MrDAL.Utility.Licensing;

public class LicenseHandler
{
    public const string LixKix = "Max?.@@#%$^";
    private static LicDetail _license;
    private static IList<LicBranch> _licOutlets;

    public static LicDetail GetLicense()
    {
        return _license;
    }

    public static (bool valid, string message) ValidateLicense(Guid originId)
    {
        var license = GetLicense();
        var hwId = FingerPrint.GetMotherboardUuId();

        if (license == null) return (false, "Unable to load license. Please contact support.");
        if (!license.HwIds.Contains(hwId))
            return (false, "License invalid. Please contact support. ErrorCode #HWID");

        var licenseBranch = license.Branches.FirstOrDefault(x => x.OutletUqId == originId);
        if (licenseBranch == null)
            return (false, "License Invalid.");

        if (DateTime.Today >= licenseBranch.ExpDate)
            return (false, $@"License expired on {licenseBranch.ExpDate:D}.");

        if (license.DateGenerated.Date > DateTime.Today)
            return (false, "Invalid system date or the license is invalid.");

        _licOutlets = license.Branches;
        return (true, string.Empty);
    }

    public static void LoadLicense()
    {
        try
        {
            using var reader = new StreamReader("MrLicense.lix");
            var text = reader.ReadToEnd();

            var decText = ObjGlobal.Decrypt(text);
            _license = JsonConvert.DeserializeObject<LicDetail>(decText);
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult("LicenseHandler");
        }
    }
}