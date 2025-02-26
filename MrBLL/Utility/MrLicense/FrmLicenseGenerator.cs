using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Utility.Interface;
using MrDAL.Utility.Licensing;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace MrBLL.Utility.MrLicense;

public partial class FrmLicenseGenerator : MrForm
{
    // ONLINE REGISTRATION
    #region --------------- ONLINE REGISTRATION ---------------

    public FrmLicenseGenerator()
    {
        InitializeComponent();
        ClearControl();
        TxtSerialNo.Text = ObjGlobal.GetSerialNo().Result;
        GetSoftwareModule();
        TxtMacAddress.Text = ObjGlobal.GetMacAddress();
        _license = new SoftwareLicenseRepository();
    }

    private void FrmOnlineRegistration_Load(object sender, EventArgs e)
    {
        _actionTag = "NEW";
        CheckRegistration();
        CheckLicenseRegistration();
        CmbSoftwareModule.SelectedIndex = 0;
        TxtCompany.Focus();
    }

    private void FrmOnlineRegistration_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape)
        {
            if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
            {
                Close();
            }
            return;
        }
        else if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }

    }

    private void TxtClientName_Leave(object sender, EventArgs e)
    {
        if (ActiveControl != null && _actionTag.IsValueExits() && TxtCompany.IsBlankOrEmpty() && TxtCompany.Focused)
        {
            MessageBox.Show(@"CLIENT DESCRIPTION CANNOT LEFT BLANK..!!", ObjGlobal.Caption);
            TxtCompany.Focus();
            return;
        }
    }

    private void TxtClientAddress_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtAddress, 'L');
        if (ActiveControl != null && _actionTag.IsValueExits() && TxtAddress.IsBlankOrEmpty() && TxtAddress.Focused)
        {
            MessageBox.Show(@"ADDRESS CANNOT BE BLANK..!!", ObjGlobal.Caption);
            TxtAddress.Focus();
            return;
        }
    }

    private void TxtClientId_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains("."))
        {
            return;
        }
        e.Handled = !int.TryParse(e.KeyChar.ToString(), out _);
    }

    private void TxtRequestBy_Leave(object sender, EventArgs e)
    {
        if (ActiveControl != null && _actionTag.IsValueExits() && TxtRequestBy.IsBlankOrEmpty() && TxtRequestBy.Focused)
        {
            MessageBox.Show(@"REQUEST FIELD IS BLANK ..!!", ObjGlobal.Caption);
            TxtRequestBy.Focus();
            return;
        }
    }

    private void TxtRegDays_Leave(object sender, EventArgs e)
    {
        try
        {
            if (TxtRegDays.Text.Length == 0)
            {
                MessageBox.Show(@"REGISTRATION DATE CANNOT BE LEFT BLANK..!!", ObjGlobal.Caption);
                TxtRegDays.Focus();
            }
            else
            {
                MskExpireDate.Text = DateTime.Now.AddDays(TxtRegDays.GetInt()).GetDateString();
            }
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }
    }

    private void TxtRegDays_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !int.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void MskRegDate_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskStartDate, 'E');
    }

    private void MskRegDate_Leave(object sender, EventArgs e)
    {
        try
        {
            ObjGlobal.MskTxtBackColor(MskStartDate, 'L');
            MskExpireDate.Text = MskStartDate.Text.GetDateTime().AddDays(TxtRegDays.GetInt()).GetDateString();
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }
    }

    private void MskRegDate_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private async void BtnRegistration_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(TxtCompany.Text))
        {
            var result = SaveSoftwareRegistration();
            if (result != 0)
            {
                //SendMailToAdmin();
                var res = SaveLicenseInfo();
                if (res != 0)
                {
                    await _license.SyncLicenseRegistration();
                    CustomMessageBox.Information("SOFTWARE LICENSE REGISTER ADDED SUCCESSFULLY..!!");
                    Dispose(true);
                }

            }
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
        {
            Close();
        }
    }

    private void TxtRegDays_TextChanged(object sender, EventArgs e)
    {
        try
        {
            MskExpireDate.Text = DateTime.Now.AddDays(TxtRegDays.GetInt()).GetDateString();
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }
    }

    private void BtnMail_Click(object sender, EventArgs e)
    {
        SendMailToAdmin();
    }

    #endregion --------------- ONLINE REGISTRATION ---------------


    // METHOD
    #region --------------- METHOD FOR THIS FORM ---------------

    private void ClearControl()
    {
        IList list = PanelHeader.Controls;
        foreach (var t in list)
        {
            var control = (Control)t;
            if (control is not TextBox) continue;
            if (control.Name != "TxtSerialNo")
            {
                control.Text = string.Empty;
                control.BackColor = SystemColors.Window;
            }
        }

        var date = new MaskedTextBox();
        var dpDate = new MaskedTextBox();
        date.Text = DateTime.Now.ToString("dd/MM/yyyy");
        dpDate.Text = date.Text;
        MskStartDate.Text = dpDate.Text;
    }

    private void GetSoftwareModule()
    {
        var table = new DataTable();
        table = table.GetSoftwareModule();
        CmbSoftwareModule.DataSource = table;
        CmbSoftwareModule.DisplayMember = "DESCRIPTION";
        CmbSoftwareModule.ValueMember = "MODULE";
        CmbSoftwareModule.SelectedIndex = 0;
    }

    private int SaveSoftwareRegistration()
    {
        var txtCustomerId = DateTime.Now.ToString("yyyyMMdd");
        var licenseId = _actionTag is "NEW" ? Guid.NewGuid() : _registrationId;
        _license.Registration.RegistrationId = licenseId;
        _license.Registration.CustomerId = txtCustomerId;
        _license.Registration.Server_MacAdd = ObjGlobal.Encrypt(TxtMacAddress.Text);
        _license.Registration.Server_Desc = ObjGlobal.Encrypt(ObjGlobal.DataSource);
        _license.Registration.ClientDescription = TxtCompany.Text;
        _license.Registration.ClientAddress = TxtAddress.Text;
        _license.Registration.ClientSerialNo = ObjGlobal.Encrypt(TxtSerialNo.Text);
        _license.Registration.RequestBy = ObjGlobal.Encrypt(TxtRequestBy.Text);
        _license.Registration.RegisterBy = ObjGlobal.Encrypt(TxtRegistrationBy.Text);
        _license.Registration.RegistrationDate = ObjGlobal.Encrypt(MskStartDate.Text);
        _license.Registration.RegistrationDays = ObjGlobal.Encrypt(TxtRegDays.Text);
        _license.Registration.ExpiredDate = ObjGlobal.Encrypt(MskExpireDate.Text);
        _license.Registration.ProductDescription = ObjGlobal.Encrypt(CmbSoftwareModule.Text);
        _license.Registration.NoOfNodes = ObjGlobal.Encrypt(TxtNoOfUsers.Text);
        _license.Registration.Module = ObjGlobal.Encrypt(CmbSoftwareModule.SelectedValue.GetString());
        _license.Registration.System_Id = ObjGlobal.Encrypt(licenseId.ToString());
        _license.Registration.ActivationCode = ObjGlobal.Encrypt(TxtSerialNo.Text);
        _license.Registration.IsOnline = ChkOnline.Checked;
        _license.Registration.IrdRegistration = ChkIRDRegister.Checked;

        return _license.SaveSoftwareLicense(_actionTag);
    }

    private int SaveLicenseInfo()
    {
        var txtCustomerId = DateTime.Now.ToString("yyyyMMdd");
        var licenseId = _actionTag is "NEW" ? Guid.NewGuid() : _licenseId;
        var expiry = _license.Registration.ExpiredDate;
        var expiryDate = ObjGlobal.Decrypt(expiry).GetDateTime();

        _license.License.LicenseId = licenseId;
        _license.License.InstalDate = _license.Registration.RegistrationDate;
        _license.License.RegistrationId = _license.Registration.RegistrationId;
        _license.License.RegistrationDate = _license.Registration.RegistrationDate;
        _license.License.SerialNo = DateTime.Now.GuidFromDateTime();
        _license.License.ActivationId = _license.Registration.CustomerId.GetLong().GuidFromLong();
        _license.License.LicenseExpireDate = _license.Registration.ExpiredDate;
        _license.License.ExpireGuid = expiryDate.GuidFromDateTime();
        _license.License.LicenseTo = _license.Registration.ClientDescription;
        _license.License.RegAddress = _license.Registration.ClientAddress;
        _license.License.ParentCompany = ObjGlobal.CompanyAddress;
        _license.License.ParentAddress = ObjGlobal.CompanyAddress;
        _license.License.CustomerId = _license.Registration.CustomerId;
        _license.License.ServerMacAddress = _license.Registration.Server_MacAdd;
        _license.License.ServerName = _license.Registration.Server_Desc;
        _license.License.RequestBy = _license.Registration.RequestBy;
        _license.License.RegisterBy = _license.Registration.RegisterBy;
        _license.License.LicenseDays = _license.Registration.RegistrationDays;
        _license.License.LicenseModule = _license.Registration.Module;
        _license.License.NodesNo = _license.Registration.NoOfNodes;
        _license.License.SystemId = _license.Registration.System_Id;

        return _license.SaveLicenseInfo(_actionTag);
    }
        
    private void SendMailToAdmin()
    {
        using var mail = new MailMessage();
        try
        {
            mail.From = new MailAddress("info.mrsolution7@gmail.com");
            mail.To.Add("info@mrsolution.com.np");
            mail.Subject = $"ONLINE REGISTRATION REQUEST FROM {ObjGlobal.LogInCompany.GetUpper()}";
            mail.Body = $"{ObjGlobal.LogInCompany} : , {ObjGlobal.CompanyAddress} : , {TxtClientId.Text} : , {ObjGlobal.CompanyPhoneNo} : , {TxtSerialNo.Text} : , {ObjGlobal.SoftwareModule} : , {ObjGlobal.TotalUser}";
            mail.IsBodyHtml = true;

            using var client = new SmtpClient();
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("info.mrsolution7@gmail.com", "9801093561");
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            client.Send(mail);
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }
    }

    private void CheckRegistration()
    {
        var dtRegistration = _license.ReturnSoftwareRegistrationHistory();
        if (dtRegistration.Rows.Count > 0)
        {
            var row = dtRegistration.NewRow();
            _actionTag = "UPDATE";
            if (dtRegistration.Rows.Count > 1)
            {
                var systemSerialNo = ObjGlobal.SystemSerialNo;

                var macAddress = ObjGlobal.GetMacAddress();
                macAddress = ObjGlobal.Encrypt(macAddress);
                var decrypt = ObjGlobal.Encrypt(systemSerialNo);

                var license = dtRegistration.Select($"ClientSerialNo = '{decrypt}' or Server_MacAdd='{macAddress}'");
                if (license.Length > 0)
                {
                    row = license[0];
                }
            }
            else
            {
                row = dtRegistration.Rows[0];
            }

            var customerId = ObjGlobal.Decrypt(row["CustomerId"].GetString());

            _registrationId = row["RegistrationId"].GetGuid();

            TxtCompany.Text = row["ClientDescription"].GetString();
            TxtAddress.Text = row["ClientAddress"].GetString();

            var serialNo = ObjGlobal.Decrypt(row["ClientSerialNo"].GetString());
            if (serialNo.IsValueExits())
            {
                TxtSerialNo.Text = serialNo;
            }

            TxtRequestBy.Text = ObjGlobal.Decrypt(row["RequestBy"].GetString());

            TxtRegistrationBy.Text = ObjGlobal.Decrypt(row["RegisterBy"].GetString());
            MskStartDate.Text = ObjGlobal.Decrypt(row["RegistrationDate"].GetString()).GetDateString();
            MskExpireDate.Text = ObjGlobal.Decrypt(row["ExpiredDate"].GetString()).GetDateString();

            TxtClientId.Text = customerId;

            var module = ObjGlobal.Decrypt(row["Module"].GetString());
            CmbSoftwareModule.SelectedIndex = CmbSoftwareModule.FindString(module);

            TxtNoOfUsers.Text = ObjGlobal.Decrypt(row["NoOfNodes"].GetString());
            MskStartDate.Enabled = false;

            var macAdd = ObjGlobal.Decrypt(row["Server_MacAdd"].GetString());
            if (macAdd.IsValueExits())
            {
                TxtMacAddress.Text = macAdd;
            }
            TxtCompany.Enabled = !TxtCompany.IsValueExits();

        }
        else
        {
            TxtCompany.Text = ObjGlobal.CompanyPrintDesc.GetUpper();
            TxtAddress.Text = ObjGlobal.CompanyAddress.GetUpper();
            TxtClientId.Text = DateTime.Now.ToString("yyyyMMdd");
        }
    }

    private void CheckLicenseRegistration()
    {
        var dtLicenseRegistration = _license.ReturnLicenseInfoHistory();
        if (dtLicenseRegistration.Rows.Count > 0)
        {
            var row = dtLicenseRegistration.NewRow();
            _actionTag = "UPDATE";
            if (dtLicenseRegistration.Rows.Count > 1)
            {
                var systemSerialNo = ObjGlobal.SystemSerialNo;

                var macAddress = ObjGlobal.GetMacAddress();
                macAddress = ObjGlobal.Encrypt(macAddress);
                var decrypt = ObjGlobal.Encrypt(systemSerialNo);

                var license = dtLicenseRegistration.Select($"SerialNo = '{decrypt}' or ServerMacAddress='{macAddress}'");
                if (license.Length > 0)
                {
                    row = license[0];
                }
            }
            else
            {
                row = dtLicenseRegistration.Rows[0];
            }

            var customerId = ObjGlobal.Decrypt(row["CustomerId"].GetString());

            _licenseId = row["LicenseId"].GetGuid();

            TxtCompany.Text = row["ParentCompany"].GetString();
            TxtAddress.Text = row["ParentAddress"].GetString();

            var serialNo = ObjGlobal.Decrypt(row["SerialNo"].GetString());
            if (serialNo.IsValueExits())
            {
                TxtSerialNo.Text = serialNo;
            }

            TxtRequestBy.Text = ObjGlobal.Decrypt(row["RequestBy"].GetString());

            TxtRegistrationBy.Text = ObjGlobal.Decrypt(row["RegisterBy"].GetString());
            MskStartDate.Text = ObjGlobal.Decrypt(row["RegistrationDate"].GetString()).GetDateString();
            MskExpireDate.Text = ObjGlobal.Decrypt(row["LicenseExpireDate"].GetString()).GetDateString();

            TxtClientId.Text = customerId;

            var module = ObjGlobal.Decrypt(row["LicenseModule"].GetString());
            CmbSoftwareModule.SelectedIndex = CmbSoftwareModule.FindString(module);

            TxtNoOfUsers.Text = ObjGlobal.Decrypt(row["NodesNo"].GetString());
            MskStartDate.Enabled = false;

            var macAdd = ObjGlobal.Decrypt(row["ServerMacAddress"].GetString());
            if (macAdd.IsValueExits())
            {
                TxtMacAddress.Text = macAdd;
            }
            TxtCompany.Enabled = !TxtCompany.IsValueExits();
        }
        else
        {
            TxtCompany.Text = ObjGlobal.CompanyPrintDesc.GetUpper();
            TxtAddress.Text = ObjGlobal.CompanyAddress.GetUpper();
            TxtClientId.Text = DateTime.Now.ToString("yyyyMMdd");
        }
    }

    #endregion --------------- METHOD FOR THIS FORM ---------------

    // OBJECT FOR THIS FORM
    private string _actionTag = string.Empty;
    private ToolTip _toolTip1 = new();
    private Guid _getCustomerId;
    private Guid _registrationId = Guid.Empty;
    private Guid _licenseId = Guid.Empty;
    private ISoftwareLicenseRepository _license;
}