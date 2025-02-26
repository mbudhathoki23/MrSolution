using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Windows.Forms;

namespace MrBLL.Utility.MIS;

public partial class FrmSoftwareInformation : MrForm
{
    private DataTable dt = new();
    private string Query = string.Empty;

    public FrmSoftwareInformation()
    {
        InitializeComponent();
    }

    private void FrmSoftwareInformation_Load(object sender, EventArgs e)
    {
        BindingMemberInfo();
    }

    public void BindingMemberInfo()
    {
        Query =
            "Select CustomerId, RegistrationId, ClientDescription, ClientAddress, ClientSerialNo, RequestBy, RegisterBy, RegistrationDate, RegistrationDays, ExpiredDate, ProductDescription, NoOfNodes, Module, System_Id, ActivationCode, Server_MacAdd, Server_Desc from master.AMS.SoftwareRegistration";
        dt.Reset();
        dt = GetConnection.SelectDataTableQuery(Query);
        if (dt.Rows.Count > 0)
        {
            var ExpiryDate = string.Empty;
            var RegDate = new TextBox();
            txtClientName.Text = dt.Rows[0]["ClientDescription"].ToString();
            txtClientAddress.Text = dt.Rows[0]["ClientAddress"].ToString();
            txtClientId.Text = ObjGlobal.Decrypt(dt.Rows[0]["CustomerId"].ToString());
            txtRegDays.Text = ObjGlobal.Decrypt(dt.Rows[0]["RegistrationDays"].ToString());
            mskRegDate.Text = ObjGlobal.Decrypt(dt.Rows[0]["RegistrationDate"].ToString());

            ExpiryDate = dt.Rows[0]["RegistrationDate"].ToString();
            RegDate.Text = ObjGlobal.Decrypt(ExpiryDate);
            mskExperiy.Text = ObjGlobal.Decrypt(dt.Rows[0]["ExpiredDate"].ToString());
            txtSoftwareModule.Text = ObjGlobal.Decrypt(dt.Rows[0]["ProductDescription"].ToString());
            txtNoOfUsers.Text = ObjGlobal.Decrypt(dt.Rows[0]["NoOfNodes"].ToString());
            txtIrdRegistration.Text = ObjGlobal.Decrypt(dt.Rows[0]["Module"].ToString());
        }
    }

    private void FrmSoftwareInformation_KeyPress(object sender, KeyPressEventArgs e)
    {
        try
        {
            switch (e.KeyChar)
            {
                case (char)39:
                    e.KeyChar = '0';
                {
                    break;
                }
                case (char)14:
                {
                    break;
                }
                case (char)21:
                {
                    break;
                }
                case (char)4:
                {
                    break;
                }
                case (char)27:
                {
                    var dialogResult = MessageBox.Show(@"Are you sure want to Close Form..??", ObjGlobal.Caption,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes) Close();
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
        }
    }
}