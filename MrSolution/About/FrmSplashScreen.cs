using MrSolution.Properties;
using System;
using System.Windows.Forms;

namespace MrSolution.About;

public sealed partial class FrmSplashScreen : Form
{
    private Timer _tmr;
    public bool IsClose;

    public FrmSplashScreen()
    {
        InitializeComponent();
        BackgroundImage = Resources.splash1;
        BackgroundImageLayout = ImageLayout.Stretch;
    }

    private void SplashScreen_Shown(object sender, EventArgs e)
    {
        _tmr = new Timer { Interval = 3000 };
        _tmr.Start();
        _tmr.Tick += Tmr_Tick;
    }

    private void Tmr_Tick(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Yes;
        _tmr.Stop();
        Close();
    }

    private void SplashScreen_FormClosed(object sender, FormClosedEventArgs e)
    {
        IsClose = true;
    }

    private void FrmSplashScreen_Click(object sender, EventArgs e)
    {
        Tmr_Tick(sender, e);
    }

    private void FrmSplashScreen_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            Tmr_Tick(sender, e);
        }
    }

    private void FrmSplashScreen_Load(object sender, EventArgs e)
    {
        //ReadLicenseAsync();
    }
}