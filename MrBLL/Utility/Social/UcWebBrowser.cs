using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Core.Utils;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Utility.Social;

public partial class UcWebBrowser : XtraUserControl
{
    private readonly BrowserScreenSize _browserScreenSize;
    private readonly string _homeUrl;

    public UcWebBrowser(string homeUrl, BrowserScreenSize browserScreenSize = BrowserScreenSize.Desktop)
    {
        InitializeComponent();
        _homeUrl = string.IsNullOrWhiteSpace(homeUrl) ? "https://." : homeUrl;
        _browserScreenSize = browserScreenSize;
        mainRibbonControl.Minimized = true;
        if (browserScreenSize == BrowserScreenSize.Desktop)
            return;
        Controls.Remove(webView21);
        webView21.Dispose();
        InitializeMobileWebView();
    }

    private void BbiHome_ItemClick(object sender, ItemClickEventArgs e)
    {
        webView21.CoreWebView2?.Navigate(_homeUrl);
    }

    private void BbiBack_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (webView21.CoreWebView2 != null && webView21.CanGoBack)
        {
            webView21.GoBack();
        }
    }

    private void BbiForward_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (webView21.CoreWebView2 != null && webView21.CanGoForward)
        {
            webView21.GoForward();
        }
    }

    private void BbiReload_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (webView21.CoreWebView2 != null)
        {
            webView21.Reload();
        }
    }

    private void BbiStop_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (webView21.CoreWebView2 != null)
        {
            webView21.Stop();
        }
    }

    private void UcWebBrowser_Load(object sender, EventArgs e)
    {
        if (_browserScreenSize != BrowserScreenSize.Desktop)
        {
            return;
        }
        if (string.IsNullOrWhiteSpace(_homeUrl))
        {
            this.NotifyError("The home url for requested page is empty.");
            return;
        }
        webView21.CoreWebView2InitializationCompleted += WebBrowserOnCoreWebView2InitializationCompleted;
        InitializeAsync();
        webView21.CoreWebView2?.Navigate(_homeUrl);
    }
    private void WebBrowserOnCoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
    {
        webView21.CoreWebView2?.Navigate(_homeUrl);
    }
    private void InitializeMobileWebView()
    {
        // Create the environment manually
        var options = new CoreWebView2EnvironmentOptions("--user-agent=\"Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.182 Mobile Safari/537.36 Edg/88.0.705.81\"");
        var envTask = CoreWebView2Environment.CreateAsync(null, null, options);
        // Do this so the task is continued on the UI Thread
        var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
        envTask.ContinueWith(_ =>
        {
            var environment = envTask.Result;

            // Create the web view 2 control and add it to the form.
            var webView2Control = new WebView2
            {
                Dock = DockStyle.Fill,
                Name = "webView2Control1",
                Anchor = AnchorStyles.None,
                Size = new Size(500, Height - mainRibbonControl.Height - 20),
                Location = new Point(500, 500),
                Top = mainRibbonControl.Bottom + 10
            };

            webView2Control.CoreWebView2InitializationCompleted += (_, args) =>
            {
                if (args.IsSuccess)
                {
                    webView2Control.CoreWebView2.Navigate(_homeUrl);
                }
                else
                {
                    var errorResult = args.InitializationException.ToNonQueryErrorResult(args);
                    this.NotifyError(errorResult.ErrorMessage);
                }
            };
            Controls.Add(webView2Control);
            webView2Control.Left = Left + (Width - Size.Width) / 2;
            // Create the CoreWebView
            var engineInitTask = webView2Control.EnsureCoreWebView2Async(environment);
            engineInitTask.ContinueWith(_ =>
            {
                //If needed do something here
            }, scheduler);
        }, scheduler);
    }

    private async void InitializeAsync()
    {
        try
        {
            await webView21.EnsureCoreWebView2Async();
        }
        catch (Exception ex)
        {
            var errMsg = ex.Message;
        }
    }
}