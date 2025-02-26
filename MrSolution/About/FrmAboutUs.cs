using MrDAL.Control.WinControl;
using System;
using System.IO;
using System.Reflection;

namespace MrSolution.About;

public partial class FrmAboutUs : MrForm
{
    public FrmAboutUs()
    {
        InitializeComponent();
        Text = $@"About MrSolution";
        labelProductName.Text = @"MrSolution | Account & Inventory Management";
        LblSoftwareVersion.Text = $@"Version {AssemblyVersion}";
        labelCopyright.Text = $@"{AssemblyCopyright}";
        labelCompanyName.Text = AssemblyCompany;
        TxtProductDesc.Text = AssemblyDescription;
        //FormClosing += (sender, args) =>
        //{
        //    webView21.ContextMenu ;
        //};
    }

    public sealed override string Text
    {
        get => base.Text;
        set => base.Text = value;
    }

    private void AboutBox1_Load(object sender, EventArgs e)
    {
    }

    #region Assembly Attribute Accessors

    public string AssemblyTitle
    {
        get
        {
            var attributes = Assembly.GetExecutingAssembly()
                .GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (attributes.Length <= 0)
                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            var titleAttribute = (AssemblyTitleAttribute)attributes[0];
            return titleAttribute.Title != string.Empty
                ? titleAttribute.Title
                : Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
        }
    }

    private string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

    public string AssemblyDescription
    {
        get
        {
            var attributes = Assembly.GetExecutingAssembly()
                .GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            return attributes.Length == 0
                ? string.Empty
                : ((AssemblyDescriptionAttribute)attributes[0]).Description;
        }
    }

    public string AssemblyProduct
    {
        get
        {
            var attributes = Assembly.GetExecutingAssembly()
                .GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            return attributes.Length == 0 ? string.Empty : ((AssemblyProductAttribute)attributes[0]).Product;
        }
    }

    private string AssemblyCopyright
    {
        get
        {
            var attributes = Assembly.GetExecutingAssembly()
                .GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            return attributes.Length == 0 ? string.Empty : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
        }
    }

    private string AssemblyCompany
    {
        get
        {
            var attributes = Assembly.GetExecutingAssembly()
                .GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            return attributes.Length == 0 ? string.Empty : ((AssemblyCompanyAttribute)attributes[0]).Company;
        }
    }

    #endregion Assembly Attribute Accessors
}