using DatabaseModule.Setup.SoftwareRegistration;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.Utility.Interface;

public interface ISoftwareLicenseRepository
{
    int SaveSoftwareLicense(string tag);
    int SaveLicenseInfo(string tag);
    Task<bool> SyncLicenseRegistration();

    // RETURN VALUE DATA TABLE
    DataTable ReturnSoftwareRegistrationHistory();
    DataTable ReturnLicenseInfoHistory();

    SoftwareRegistration Registration { get; set; }
    LicenseInfo License { get; set; }
}