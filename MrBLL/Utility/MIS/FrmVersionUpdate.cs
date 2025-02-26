using MrDAL.Control.WinControl;
using System;
using System.Threading.Tasks;

namespace MrBLL.Utility.MIS;

public partial class FrmVersionUpdate : MrForm
{
    public FrmVersionUpdate()
    {
        InitializeComponent();
        Shown += FrmVersionUpdate_Shown;
    }

    private void FrmVersionUpdate_Shown(object sender, EventArgs e)
    {
        InitBrowser();
    }

    private async Task InitializeWebView2()
    {
        await webView21.EnsureCoreWebView2Async(null);
    }
    public async void InitBrowser()
    {
        await InitializeWebView2();
        webView21.CoreWebView2.Navigate("https://mega.nz/folder/BrRWQJxZ#VUYUeqE2eAAUjkTI4jK4lw");
    }

    private void FrmVersionUpdate_Load(object sender, EventArgs e)
    {

    }
}