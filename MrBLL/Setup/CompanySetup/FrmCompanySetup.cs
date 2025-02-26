using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using MrDAL.Control.ControlsEx;
using MrDAL.Control.SplashScreen;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Setup.CompanySetup;
using MrDAL.Setup.Interface;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DatabaseModule.Setup.CompanyMaster;

namespace MrBLL.Setup.CompanySetup;

public partial class FrmCompanySetup : MrForm
{
    #region --------------- Form  ---------------

    public FrmCompanySetup(bool zoom)
    {
        InitializeComponent();
        _isZoom = zoom;
        _objCon = new GetConnection();
        // _objMaster = new CreateDatabase();
        _companySetup = new CompanySetupRepository();
        _getForm = new ClsMasterForm(this, BtnExit);
    }

    private void FrmCompanySetup_Load(object sender, EventArgs e)
    {
        var drives = DriveInfo.GetDrives();
        for (var i = 0; i <= drives.Length - 1; i++)
        {
            cb_DataBasePath.Items.Add(drives[i].Name);
        }
        cb_DataBasePath.SelectedIndex = 0;
        ClearControl();
        EnableControl();
        CompanyregToolTip.SetToolTip(MskRegDate, "DD/MM/YYYY");
        BindFiscalYear();
        BtnNew.Focus();
        //var accessControl = ObjGlobal.GetFormAccessControl("Company");
        //BtnNew.Visible = accessControl != null ? accessControl.NewButtonCheck : true;
        //BtnEdit.Visible = accessControl != null ? accessControl.EditButtonCheck : true;
        //BtnDelete.Visible = accessControl != null ? accessControl.DeleteButtonCheck : true;
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }

    private void FrmCompanySetup_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape)
        {
            if (!BtnNew.Enabled)
            {
                if (CustomMessageBox.ClearVoucherDetails("COMPANY SETUP") == DialogResult.Yes)
                {
                    if (TxtPrintingName.Enabled || TxtPrintingName.Text == string.Empty || _actionTag != "SAVE")
                    {
                        _actionTag = string.Empty;
                        ClearControl();
                        EnableControl();
                        BtnNew.Focus();
                    }
                }
            }
            else
            {
                if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
                {
                    BtnExit.PerformClick();
                }
            }
        }
    }

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        ClearControl();
        EnableControl(true);
        MskRegDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        MskRegDate.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _actionTag = "UPDATE";
        ClearControl();
        EnableControl(true);
        BtnSave.Text = @"&" + _actionTag;
        SetGridDataToText();
        MskRegDate.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        BtnSave.Text = @"&" + _actionTag;
        SetGridDataToText();
        EnableControl();
        BtnSave.Focus();
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        var result = CustomMessageBox.ExitActiveForm();
        if (result == DialogResult.Yes)
        {
            Close();
        }
    }

    [Obsolete]
    private void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            BtnProgressBar_Click(sender, e);
            PanelHeader.Visible = false;
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            if (string.IsNullOrEmpty(_actionTag))
            {
                return;
            }
            if (TxtPrintingName.Text.IsBlankOrEmpty() || TxtCompanyDesc.Text.IsBlankOrEmpty())
            {
                MessageBox.Show(@"COMPANY DESCRIPTION IS BLANK..!!", ObjGlobal.Caption);
                TxtPrintingName.Focus();
                return;
            }
            if (_actionTag is "SAVE")
            {
                CheckPath();
                GenerateInitial();
            }
            DrawMovingText();
        }
        catch (Exception ex)
        {
            SplashScreenManager.CloseForm(false);
            var message = ex.Message;
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }
    }

    private void BtnClear_Click(object sender, EventArgs e)
    {
        ClearControl();
        Close();
    }

    private void CmbFiscalYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        var cmdString = $"Select * from Master.AMS.FiscalYear where BSFiscalYear='{CmbFiscalYear.Text}'";
        var table = SqlExtensions.ExecuteDataSetOnMaster(cmdString).Tables[0];
        if (table.Rows.Count <= 0)
        {
            return;
        }
        foreach (DataRow item in table.Rows)
        {
            MskStartDate.Text = Convert.ToDateTime(item["StartAdDate"].ToString()).ToString("dd/MM/yyyy");
            MskEndDate.Text = Convert.ToDateTime(item["EndAdDate"].ToString()).ToString("dd/MM/yyyy");
        }
    }

    private async void BtnProgressBar_Click(object sender, EventArgs e)
    {
        //var progress = new Progress<int>(v =>
        //{
        //});
        //await Task.Run(() => DoWork(progress));
    }

    private void TxtPrintingName_TextChanged(object sender, EventArgs e)
    {
        TxtCompanyDesc.Text = $@"{TxtPrintingName.Text.Trim()} ( FISCAL YEAR : {CmbFiscalYear.Text.Trim()}) ";
    }

    private void MskRegister_DateValidated(object sender, EventArgs e)
    {
        if (MskRegDate.MaskCompleted)
        {
            var day = Convert.ToInt32(Convert.ToString(MskRegDate.Text).Substring(0, 2));
            var month = Convert.ToInt32(Convert.ToString(MskRegDate.Text).Substring(3, 2));

            if (day is > 32 or < 1)
            {
                MessageBox.Show(@"PLZ. ENTER VALID DAY..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                MskRegDate.Focus();
            }
            else if (month is > 12 or < 1)
            {
                MessageBox.Show(@"PLZ. ENTER VALID MONTH..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                MskRegDate.Focus();
            }
        }
        else
        {
            MessageBox.Show(@"COMPANY REGISTER DATE CANNOT BE LEFT BLANK..!!", ObjGlobal.Caption);
            MskRegDate.Focus();
        }
    }

    private void TxtCompanyName_Validated(object sender, EventArgs e)
    {
        Debug.Assert(CmbFiscalYear != null, nameof(CmbFiscalYear) + " != null");
        if (string.IsNullOrEmpty(TxtPrintingName.Text.Replace("'", "''"))) return;
        TxtCompanyDesc.Text = $@"{TxtPrintingName.Text} ( FISCAL YEAR : {CmbFiscalYear.Text})";
        if (string.IsNullOrEmpty(TxtCompanyDesc.Text.Replace("'", "''")) ||
            string.IsNullOrEmpty(_actionTag)) return;
        var query =
            $"SELECT * FROM MASTER.AMS.GlobalCompany gc WHERE gc.Company_Name='{TxtCompanyDesc.Text.Trim().Replace("'", "''")}'";
        if (_actionTag is "UPDATE") query += $" and Database_Name <> '{ObjGlobal.InitialCatalog}'";
        using var tbCompany = SqlExtensions.ExecuteDataSetOnMaster(query).Tables[0];
        if (tbCompany.Rows.Count <= 0) return;
        MessageBox.Show(@"COMPANY IS ALREADY EXITS PLEASE CHOOSE NEXT NAME..!!", ObjGlobal.Caption);
        TxtPrintingName.Focus();
    }

    private void TxtPrintingName_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtCompanyDesc.Text.Trim().Replace("'", "''")) && TxtPrintingName.Focused &&
            !string.IsNullOrEmpty(_actionTag))
        {
            MessageBox.Show(@"COMPANY DESCRIPTION CAN'T LEFT BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtPrintingName.Focus();
        }
    }

    private void TxtPhone_KeyPress(object sender, KeyPressEventArgs e)
    {
        //ObjGlobal.ValidatePhoneNo(e);
    }

    private void TxtFax_KeyPress(object sender, KeyPressEventArgs e)
    {
        //ObjGlobal.ValidatePhoneNo(e);
    }

    private void TxtEmailId_Leave(object sender, EventArgs e)
    {
        //if (ObjGlobal.ValidateEmailId(txt_EmailId.Text.Trim().Replace("'", "''")))
        //{
        //    //MessageBox.Show("valid");
        //}
        //else
        //{
        //    if (txt_EmailId.Text == string.Empty) return;
        //    MessageBox.Show(@"ENTER VALID EMAIL ID !", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    txt_EmailId.Text = string.Empty;
        //    txt_EmailId.Focus();
        //}
    }

    private void TxtWebSite_Leave(object sender, EventArgs e)
    {
        //if (ObjGlobal.ValidateUrl(txt_WebSite.Text))
        //{
        //    //MessageBox.Show("valid");
        //}
        //else
        //{
        //    if (string.IsNullOrEmpty(txt_WebSite.Text.Trim())) return;
        //    MessageBox.Show(@"THE URL ADDRESS YOU HAVE ENTERED IS INCORRECT!", ObjGlobal.Caption,
        //        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    txt_WebSite.Text = string.Empty;
        //    txt_WebSite.Focus();
        //}
    }

    private void LblCompanyLogo_DoubleClick(object sender, EventArgs e)
    {
        PbCompanyLogo_DoubleClick(sender, e);
    }

    private void PbCompanyLogo_DoubleClick(object sender, EventArgs e)
    {
        var isFileExists = string.Empty;
        try
        {
            using var dlg = new OpenFileDialog();
            dlg.Filter = @"Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            dlg.ShowDialog();
            var fileName = dlg.FileName;
            isFileExists = dlg.FileName;
            pb_CompanyLogo.Load(fileName);
            lbl_CompanyLogo.Visible = false;
        }
        catch (Exception ex)
        {
            if (isFileExists != string.Empty)
                MessageBox.Show(@"Picture File Format & " + ex.Message);
            else
                lbl_CompanyLogo.Visible = true;
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }
    }

    #endregion --------------- Form  ---------------

    // METHOD FOR THIS FORM

    #region --------------- Method ---------------

    private void DeclareValue()
    {
        _companySetup.Info.Company_Id = ObjGlobal.CompanyId.GetInt();
        _companySetup.Info.CReg_Date = MskRegDate.Text.GetDateTime();
        _companySetup.Info.PrintDesc = TxtPrintingName.Text.GetTrimReplace();
        _companySetup.Info.Company_Name = TxtCompanyDesc.Text.GetTrimReplace();
        _companySetup.Info.Company_Logo = ObjGlobal.ReadFile(pb_CompanyLogo.ImageLocation);
        _companySetup.Info.Address = TxtAddress.Text.GetTrimReplace();
        _companySetup.Info.Country = TxtCountry.Text.GetTrimReplace();
        _companySetup.Info.State = string.Empty;
        _companySetup.Info.City = string.Empty;
        _companySetup.Info.PhoneNo = TxtPhoneNo.Text;
        _companySetup.Info.Fax = TxtFaxNo.Text;
        _companySetup.Info.Email = TxtEmailAddress.Text.GetTrimReplace();
        _companySetup.Info.Website = TxtWebSites.Text;
        _companySetup.Info.Version_No = ObjGlobal.CompanyVersion.ToString();
        _companySetup.Info.Pan_No = TxtTPanNo.Text;

        //_companySetup.Info.EnterBy = ObjGlobal.LogInUser;
        //_companySetup.Info.FiscalYear = CmbFiscalYear.Text;
        //_companySetup.Info.StartAdDate = MskStartDate.Text;
        //_companySetup.Info.EndAdDate = MskEndDate.Text;

        _companySetup.Info.Status = true;
        _companySetup.Info.IsSyncOnline = ObjGlobal.IsOnlineSync;
        _companySetup.Info.ApiKey = _apiKey ?? Guid.NewGuid();
        _companySetup.Info.Database_Name = _actionTag.Equals("SAVE") ? _companySetup.Info.Database_Name : ObjGlobal.InitialCatalog;
        ObjGlobal.SysFiscalYearId = CmbFiscalYear.SelectedValue.GetInt();

        _companySetup.Setup.Database_Name = _companySetup.Info.Database_Name;
        _companySetup.Setup.Company_Name = _companySetup.Info.Company_Name;
        _companySetup.Setup.PrintingDesc = _companySetup.Info.PrintDesc;
        _companySetup.Setup.Database_Path = _companySetup.Info.Database_Path;
        _companySetup.Setup.Status = true;
        _companySetup.Setup.PanNo = _companySetup.Info.Pan_No;
        _companySetup.Setup.Address = _companySetup.Info.Address;
        _companySetup.Setup.CurrentFiscalYear = CmbFiscalYear.Text;
        _companySetup.Setup.LoginDate = DateTime.Now;
        _companySetup.Setup.DataSyncOriginId = string.Empty;
        _companySetup.Setup.DataSyncApiBaseUrl = string.Empty;
        _companySetup.Setup.ApiKey = Guid.NewGuid();


        var list = new CompanyRights
        {
            CompanyRights_Id = _companySetup.MaxCompanyRightsId(),
            User_Id = ObjGlobal.LogInUserId,
            Company_Id = _companySetup.Info.Company_Id,
            Company_Name = _companySetup.Info.Company_Name,
            Start_AdDate = MskStartDate.GetDateTime(),
            End_AdDate = MskEndDate.GetDateTime(),
            Modify_Start_AdDate = null,
            Modify_End_AdDate = null,
            Back_Days_Restriction = false
        };
        _companySetup.RightsList.Add(list);
    }

    private void GenerateInitial()
    {
        if (!string.IsNullOrEmpty(TxtIntial.Text.Trim()))
        {

            TxtIntial.Text = Regex.Replace(TxtIntial.Text, @"\s", "");

            var cmd = $"SELECT COUNT(*) + 1  FROM sys.databases d WHERE d.name like '%{TxtIntial.Text.Trim()}%'; ";
            var companyReturnInt = _objCon.GetSqlMasterData(cmd).GetInt();

            _companySetup.Info.Database_Name = companyReturnInt.ToString().Length > 2
                ? TxtIntial.Text.Trim() + companyReturnInt
                : TxtIntial.Text.Trim() + "0" + companyReturnInt;
        }
        else
        {
            const string yourString = "Hello@Hello&Hello(Hello)";
            Regex.Replace(yourString, @"[^0-9a-zA-Z]+", ",");

            _companySetup.Info.Database_Name = "MR_" + Convert.ToString(Regex.Replace(TxtPrintingName.Text.Trim(), @"[^0-9a-zA-Z]+", string.Empty).Length >= 6
                ? Regex.Replace(TxtPrintingName.Text.Trim(), @"[^0-9a-zA-Z]+", string.Empty).Substring(0, 6).ToUpper()
                : Regex.Replace(TxtPrintingName.Text.Trim().ToUpper(), @"[^0-9a-zA-Z]+", string.Empty).PadLeft(6, '0'));

            _companySetup.Info.Database_Name = _companySetup.Info.Database_Name.Trim().PadRight(8, '0') + "01";
            _query = $@" SELECT name FROM master.dbo.sysdatabases WHERE Left(name,8)='{_companySetup.Info.Database_Name.Substring(0, 8)}' ";
            _table.Reset();
            _table = GetConnection.SelectQueryFromMaster(_query);
            if (_table.Rows.Count <= 0)
            {
                return;
            }

            _companySetup.Info.Database_Name = _companySetup.Info.Database_Name?.Substring(0, 8) + (_table.Rows.Count + 1).ToString().PadLeft(2, '0');
        }
    }

    private void CheckPath()
    {
        var path = cb_DataBasePath.SelectedItem + "MR_DB_FILES\\";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        _companySetup.Info.Database_Path = path;
    }

    private void ClearControl()
    {
        Text = !string.IsNullOrEmpty(_actionTag) ? $"COMPANY SETUP DETAILS [{_actionTag}]" : "COMPANY SETUP DETAILS";
        TxtPrintingName.Clear();
        MskRegDate.Text = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy");
        TxtAddress.Clear();
        TxtCountry.Clear();
        TxtPhoneNo.Clear();
        TxtFaxNo.Clear();
        TxtEmailAddress.Clear();
        TxtTPanNo.Clear();
        TxtWebSites.Clear();
        pb_CompanyLogo.ImageLocation = string.Empty;
        pb_CompanyLogo.Image = null;
        lbl_CompanyLogo.Visible = true;
        BtnSave.Text = @"&CREATE";
    }

    private void SetGridDataToText()
    {
        _query = $"SELECT TOP 1 * FROM AMS.CompanyInfo where  Status=1";
        _table.Reset();
        _table = GetConnection.SelectDataTableQuery(_query);
        if (_table.Rows.Count <= 0)
        {
            return;
        }
        TxtPrintingName.Text = _table.Rows[0]["PrintDesc"].IsValueExits()
            ? _table.Rows[0]["PrintDesc"].GetTrimApostrophe()
            : _table.Rows[0]["Company_Name"].GetTrimApostrophe();

        TxtAddress.Text = _table.Rows[0]["Address"].GetTrimApostrophe();
        TxtCountry.Text = _table.Rows[0]["Country"].GetTrimApostrophe();

        TxtPhoneNo.Text = _table.Rows[0]["PhoneNO"].ToString();

        TxtFaxNo.Text = _table.Rows[0]["Fax"].ToString();
        TxtEmailAddress.Text = _table.Rows[0]["Email"].ToString();

        if (Convert.ToDateTime(_table.Rows[0]["CReg_Date"].ToString()) != Convert.ToDateTime("1753/01/01"))
        {
            MskRegDate.Text = ObjGlobal.FillDateType(_table.Rows[0]["CReg_Date"].ToString());
        }

        TxtTPanNo.Text = _table.Rows[0]["Pan_No"].ToString();
        TxtWebSites.Text = _table.Rows[0]["Website"].ToString();

        _apiKey = _table.Rows[0]["ApiKey"].GetGuid();
        _isOnlineSync = _table.Rows[0]["IsSyncOnline"].GetBool();

        var imageData = _table.Rows[0]["Company_Logo"].GetImage();
        if (imageData != null)
        {
            pb_CompanyLogo.Image = imageData;
        }
        if (_actionTag == "UPDATE")
        {
            TxtPrintingName.Focus();
        }
        else
        {
            BtnSave.Focus();
        }
    }

    private void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable;
        BtnEdit.Enabled = BtnDelete.Enabled = !_isZoom && !isEnable;
        BtnEdit.Visible = BtnDelete.Visible = !_isZoom && !isEnable;

        TxtIntial.Enabled = isEnable;
        CmbFiscalYear.Enabled = isEnable;

        MskStartDate.Enabled = _actionTag is "UPDATE";
        MskEndDate.Enabled = _actionTag is "UPDATE";
        MskRegDate.Enabled = _actionTag is "SAVE";

        TxtPrintingName.Enabled = isEnable;
        TxtAddress.Enabled = isEnable;

        TxtCompanyDesc.Enabled = isEnable;
        TxtCountry.Enabled = isEnable;
        TxtPhoneNo.Enabled = isEnable;
        TxtTPanNo.Enabled = isEnable;
        TxtFaxNo.Enabled = isEnable;
        TxtEmailAddress.Enabled = isEnable;
        TxtWebSites.Enabled = isEnable;

        cb_DataBasePath.Enabled = !string.IsNullOrEmpty(_actionTag) && _actionTag == "SAVE";

        BtnSave.Enabled = !string.IsNullOrEmpty(_actionTag) || isEnable;
        BtnCancel.Enabled = !string.IsNullOrEmpty(_actionTag) || isEnable;

        pb_CompanyLogo.Enabled = isEnable;
        lbl_CompanyLogo.Enabled = isEnable;
    }

    private void BindFiscalYear()
    {
        ObjGlobal.BindMasterFiscalYear(CmbFiscalYear);

        const string cmd = @" SELECT FiscalYearId From MASTER.AMS.FiscalYear where GETDATE() Between StartAdDate and EndAdDate";
        var fiscalYearId = _objCon.GetSqlMasterData(cmd);
        CmbFiscalYear.SelectedValue = ObjGlobal.SysFiscalYearId > 0 ? ObjGlobal.SysFiscalYearId : fiscalYearId;
    }

    private static void Calculate(int i)
    {
        var pow = Math.Pow(i, i);
    }

    private void CheckDatabaseExits()
    {
        var exists = true;
        while (exists)
        {
            exists = File.Exists(_companySetup.Info.Database_Path + _companySetup.Info.Database_Name + ".mdf");
            if (exists)
            {
                CustomMessageBox.Warning($"{_companySetup.Info.Database_Name} ALREADY EXITS");

                var lastCharacter = _companySetup.Info.Database_Name[_companySetup.Info.Database_Name.Length - 1];
                var dbInt = lastCharacter.GetInt() + 1;
                var founderMinus = _companySetup.Info.Database_Name.Remove(_companySetup.Info.Database_Name.Length - 1, 1);
                _companySetup.Info.Database_Name = founderMinus + dbInt;
            }

            // This path is a file
        }
    }

    [Obsolete]
    private void DrawMovingText()
    {
        DeclareValue();
        if (_actionTag.Equals("SAVE"))
        {
            CheckDatabaseExits();
        }
        if (_companySetup.NewCreateDatabase(_companySetup.Info.Database_Path, _companySetup.Info.Database_Name, _actionTag))
        {
            SplashScreenManager.CloseForm(true);
            if (CustomMessageBox.Information($@"COMPANY {_actionTag} SUCCESSFULLY..!!") != DialogResult.OK)
            {
                return;
            }
            IsSetupSuccess = true;
            DialogResult = DialogResult.OK;
        }
        else
        {
            if (CustomMessageBox.ErrorMessage($@"ERROR OCCURS WHILE COMPANY INFORMATION {_actionTag} ..!!") !=
                DialogResult.OK)
            {
                return;
            }

            TxtIntial.Focus();
        }
    }

    #endregion --------------- Method ---------------

    // OBJECT FOR THIS FORM

    #region --------------- Global ---------------

    public bool IsSetupSuccess;
    private bool _isZoom;

    private string _query = string.Empty;
    private string _actionTag = string.Empty;

    private DataTable _table = new();

    private GetConnection _objCon;
    private ClsMasterForm _getForm;
    private ICompanySetup _companySetup;


    private bool _isOnlineSync = false;

    private Guid? _apiKey = ObjGlobal.SyncBaseSync;

    #endregion --------------- Global ---------------
}