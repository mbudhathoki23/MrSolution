using Microsoft.Web.WebView2.Core;
using System;
using System.Windows.Forms;

namespace MrBLL.Utility.MIS;

public partial class FrmWebSites : Form
{
    public FrmWebSites()
    {
        InitializeComponent();
    }

    private void FrmWebSites_Load(object sender, EventArgs e)
    {
        var userDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\WebView2";
        var env = CoreWebView2Environment.CreateAsync(null, userDataFolder);
    }
}