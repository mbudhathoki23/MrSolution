using DevExpress.XtraSplashScreen;
using MrBLL.Utility.SplashScreen;
using MrDAL.Core.Extensions;
using MrDAL.Global.Control;
using MrDAL.Utility.dbMaster;
using System;
using System.IO;
using System.Windows.Forms;

namespace MrBLL.Utility.Database;

public partial class FrmExternalDeviceTools : Form
{
    // EXTERNAL DEVICE MANAGEMENT

    #region --------------- EXTERNAL DEVICE MANAGEMENT ---------------

    public FrmExternalDeviceTools()
    {
        InitializeComponent();
    }

    private void BtnBrowser_Click(object sender, EventArgs e)
    {
        openBakFileDialog.FileName = TxtLocation.Text;
        try
        {
            openBakFileDialog.InitialDirectory = Path.GetDirectoryName(TxtLocation.Text);
        }
        catch (Exception)
        {
            // ignored
        }

        if (openBakFileDialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        TxtLocation.Text = openBakFileDialog.FileName;
        _location = TxtLocation.Text;
        _fileName = Path.GetFileName(_location);
    }

    private void BtnAttach_Click(object sender, EventArgs e)
    {
        if (_fileName.IsValueExits())
         {
            if (CustomMessageBox.Question("DO YOU WANT TO ATTACH DATABASE..??") is DialogResult.Yes)
            {
                SplashScreenManager.ShowForm(typeof(FrmWait));
                var result = _attach.AttachSelectedDatabase(_location, _fileName);
                SplashScreenManager.CloseForm(false);
                if (result != 0)
                {
                    CustomMessageBox.Information($"{_fileName} DATABASE ATTACH SUCCESSFULLY..!!");
                    BindCompanyInformation();
                    TxtLocation.Clear();
                    TxtLocation.Focus();
                }
            }
        }
    }

    private void BtnDeAttach_Click(object sender, EventArgs e)
    {
        if (RGrid.CurrentRow != null)
        {
            var fileName = RGrid.CurrentRow.Cells[0].Value;
            if (fileName.IsValueExits())
            {
                if (CustomMessageBox.Question("DO YOU WANT TO DE - ATTACH DATABASE..??") is DialogResult.Yes)
                {
                    SplashScreenManager.ShowForm(typeof(FrmWait));
                    var result = _attach.DeAttachSelectedDatabase(fileName.ToString());
                    SplashScreenManager.CloseForm(false);
                    if (result != 0)
                    {
                        CustomMessageBox.Information("SELECTED COMPANY IS DE-ATTACHED FROM DATABASE");
                        BindCompanyInformation();
                    }
                }
            }
        }
    }

    private void FrmExternalDeviceTools_Load(object sender, EventArgs e)
    {
        BindCompanyInformation();
    }

    private void FrmExternalDeviceTools_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (CustomMessageBox.ExitActiveForm() is DialogResult.Yes)
            {
                Close();
            }
        }
    }

    #endregion --------------- EXTERNAL DEVICE MANAGEMENT ---------------

    // METHOD FOR THIS FORM
    private void BindCompanyInformation()
    {
        var result = _attach.GetCompanyList();
        RGrid.DataSource = result;
        RGrid.ClearSelection();
    }

    // OBJECT FOR THIS FORM

    #region --------------- OBJECT ---------------

    private string _location = string.Empty;
    private string _fileName = string.Empty;
    private readonly ClsAttachDeAttach _attach = new();

    #endregion --------------- OBJECT ---------------
}