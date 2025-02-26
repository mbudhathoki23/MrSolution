using System.Windows.Forms;

namespace MrBLL.Utility.MrLicense;

public partial class FrmLicenseExpiredMessage : Form
{
    public FrmLicenseExpiredMessage()
    {
        InitializeComponent();
        Load += FrmLicenseExpiredMessage_Load;
        KeyPress += FrmLicenseExpiredMessage_KeyPress;
    }

    private void FrmLicenseExpiredMessage_Load(object sender, System.EventArgs e)
    {
        LicenseClosed.Enabled = true;
    }

    private void FrmLicenseExpiredMessage_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape or (char)Keys.Enter)
        {
            Close();
        }
    }

    private void LicenseClosed_Tick(object sender, System.EventArgs e)
    {
        this.Close();
        LicenseClosed.Stop();
    }
}