using MrDAL.Control.WinControl;
using System.Windows.Forms;

namespace MrBLL.Utility.MrLicense;

public partial class FrmRegistrationMail : MrForm
{
    public FrmRegistrationMail()
    {
        InitializeComponent();
    }

    private void FrmRegistrationMail_FormClosed(object sender, FormClosedEventArgs e)
    {
        webView21.Dispose();
    }

    private void FrmRegistrationMail_Load(object sender, System.EventArgs e)
    {
    }
}