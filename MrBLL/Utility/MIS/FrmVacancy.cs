using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using System;

namespace MrBLL.Utility.MIS;

public sealed partial class FrmVacancy : MrForm
{
    public FrmVacancy(string type)
    {
        InitializeComponent();
        Text = type.GetUpper() switch
        {
            "TRAINING" => "FILL THE INFORMATION FOR TRAINING",
            "WEBSITES" => "MRSOLUTION WEBSITES",
            _ => "FILL THE INFORMATION FOR VACANCY"
        };
        webView21.Source = type.GetUpper() switch
        {
            "TRAINING" => new Uri("https://forms.gle/wG2Kdv1C3diEKyeB6"),
            "MR_VACANCY" => new Uri("https://forms.gle/wG2Kdv1C3diEKyeB6"),
            "WEBSITES" => new Uri("http://mrsolution.com.np/"),
            _ => new Uri("https://forms.gle/Hn1Ur6NL4WXdHRLm9")
        };
        Closed += FrmVacancy_Closed;
    }

    private void FrmVacancy_Closed(object sender, EventArgs e)
    {
        webView21.Dispose();
    }

    private void FrmVacancy_Load(object sender, EventArgs e)
    {
    }
}