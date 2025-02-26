using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using MrDAL.Core.Extensions;
using System;
using System.Linq;
using System.Windows.Forms;

namespace MrBLL.Utility.Social;

public partial class FrmSocialControl : XtraForm
{
    public FrmSocialControl()
    {
        InitializeComponent();
    }

    private void AccordionControl_ElementClick(object sender, ElementClickEventArgs e)
    {
        if (e.Element?.Tag == null) return;
        var item = (WebBrowserLink)Enum.Parse(typeof(WebBrowserLink), e.Element.Tag.ToString());

        var dupTab = tabControl.TabPages.FirstOrDefault(x => x.Text.Equals(e.Element.Text));
        if (dupTab != null)
        {
            tabControl.SelectedTabPage = dupTab;
        }
        else
        {
            var (url, screenSize) = GetUrlFromLink(item);
            var control = new UcWebBrowser(url, screenSize)
            {
                Dock = DockStyle.Fill
            };
            var newTab = new XtraTabPage
            {
                Text = e.Element.Text,
                Controls = { control }
            };
            tabControl.TabPages.Add(newTab);
            tabControl.SelectedTabPage = newTab;
        }
    }

    public (string Url, BrowserScreenSize screenSize) GetUrlFromLink(WebBrowserLink item)
    {
        var url = item switch
        {
            WebBrowserLink.Facebook => "https://facebook.com",
            WebBrowserLink.Youtube => "https://youtube.com",
            WebBrowserLink.Viber => "https://viber.com",
            WebBrowserLink.Gmail => "https://mail.google.com",
            WebBrowserLink.Outlook => "https://outlook.com",
            WebBrowserLink.Yandex => "https://mail.yandex.com",
            WebBrowserLink.ZohoMail => "https://mail.zoho.com",
            WebBrowserLink.Yahoo => "https://mail.yahoo.com",
            WebBrowserLink.Google => "https://google.com",
            WebBrowserLink.Twitter => "https://twitter.com",
            WebBrowserLink.WhatsApp => "https://whatsapp.com",
            WebBrowserLink.TikTok => "https://m.tiktok.com",
            WebBrowserLink.Instagram => "https://instagram.com",
            _ => ""
        };

        return (url, item == WebBrowserLink.TikTok ? BrowserScreenSize.Mobile : BrowserScreenSize.Desktop);
    }

    private void TabControl_CloseButtonClick(object sender, EventArgs e)
    {
        var args = (ClosePageButtonEventArgs)e;
        if (e == null) return;

        tabControl.TabPages.Remove(args.Page as XtraTabPage);
    }

    private void TabControl_ControlRemoved(object sender, ControlEventArgs e)
    {
        e.Control?.DisposeChildren();
    }

    private void FrmSocialControl_Load(object sender, EventArgs e)
    {
    }
}