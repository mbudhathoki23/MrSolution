using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Windows.Forms;

namespace MrClientManagement.Utility
{
    public partial class FrmLogin : DevExpress.XtraEditors.XtraForm
    {
        [Obsolete("Obsolete")]
        public FrmLogin()
        {
            InitializeComponent();
            _objMaster = new ClsMasterSetup();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            TxtUserInfo.Focus();
        }

        private void FrmLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar is (char)Keys.Escape)
            {
                if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
                {
                    Close();
                }
            }
            else if (e.KeyChar is (char)Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        [Obsolete("Obsolete")]
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (TxtUserInfo.IsBlankOrEmpty())
            {
                TxtUserInfo.WarningMessage("PLEASE ENTER USER INFO..!!");
                TxtUserInfo.Focus();
                return;
            }
            if (TxtPassword.IsBlankOrEmpty())
            {
                TxtPassword.WarningMessage("PLEASE ENTER USER PASSWORD..!!");
                TxtPassword.Focus();
                return;
            }

            var result = UserLoginIsValid();
            if (result)
            {
                if (TxtUserInfo.Text is "AMSADMIN" or "SIYZO" or "RUDRALEKHA")
                {
                    ObjGlobal.LogInUserId = 1;
                    ObjGlobal.LogInUser = TxtUserInfo.Text.ToUpper();
                    ObjGlobal.LogInUserPassword = TxtPassword.Text.ToUpper();
                }
                else
                {
                    var dtUser = _objMaster.GetMasterUserInfo(TxtUserInfo.Text);
                    foreach (DataRow dr in dtUser.Rows)
                    {
                        ObjGlobal.LogInUserId = ObjGlobal.ReturnInt(dr["User_Id"].ToString());
                        ObjGlobal.LogInUser = Convert.ToString(dr["User_Name"].ToString());
                        ObjGlobal.LogInUserPassword = TxtPassword.Text.ToUpper();
                        ObjGlobal.LogInUserPostingEndDate = dr["Posting_StartDate"].IsValueExits() ? dr["Posting_StartDate"].GetDateTime() : ObjGlobal.CfStartAdDate;
                        ObjGlobal.LogInUserPostingEndDate = dr["Posting_EndDate"].IsValueExits() ? dr["Posting_EndDate"].GetDateTime() : ObjGlobal.CfEndAdDate;
                        ObjGlobal.LogInUserModifyStartDate = dr["Modify_StartDate"].IsValueExits() ? dr["Modify_StartDate"].GetDateTime() : ObjGlobal.CfStartAdDate;
                        ObjGlobal.LogInUserModifyEndDate = dr["Modify_EndDate"].IsValueExits() ? dr["Modify_EndDate"].GetDateTime() : ObjGlobal.CfEndAdDate;
                        ObjGlobal.LogInUserValidDays = dr["Valid_Days"].GetInt();
                        ObjGlobal.LogInUserCategory = dr["Category"].GetString();
                        ObjGlobal.UserAuthorized = dr["Authorized"].GetBool();
                        ObjGlobal.UserAllowPosting = dr["Allow_Posting"].GetBool();
                        ObjGlobal.UserModify = dr["IsModify"].GetBool();
                        ObjGlobal.UserDelete = dr["IsDeleted"].GetBool();
                        ObjGlobal.UserPdcDashBoard = dr["IsPDCDashBoard"].GetBool();
                        ObjGlobal.UserChangeRate = dr["IsRateChange"].GetBool();
                        ObjGlobal.UserAuditLog = dr["Auditors_Lock"].GetBool();
                        ObjGlobal.ChangeQty = dr["IsQtyChange"].GetBool();
                        ObjGlobal.UserLedgerId = dr["Ledger_Id"].GetLong();
                    }
                }

                DialogResult = DialogResult.OK;
            }
            else
            {
                return;
            }
        }

        [Obsolete("Obsolete")]
        private bool UserLoginIsValid()
        {
            var cmdString = $"SELECT * FROM AMS.UserInfo Where User_Name='{TxtUserInfo.Text.GetString()}'";
            var dtUser = SqlExtentions.ExecuteDataSetOnMaster(cmdString).Tables[0];
            if (dtUser.Rows.Count == 0)
            {
                TxtUserInfo.WarningMessage("INVALID USER INFO..!!");
                TxtUserInfo.Focus();
                return false;
            }
            if (TxtPassword.IsValueExits())
            {
                cmdString = $"SELECT * FROM Master.AMS.UserInfo Where User_Name='{TxtUserInfo.Text}'  and Password='{ObjGlobal.Encrypt(TxtPassword.Text)}'";
                var dtCheckPassword = SqlExtentions.ExecuteDataSetOnMaster(cmdString).Tables[0];
                if (dtCheckPassword.Rows.Count == 0)
                {
                    TxtPassword.WarningMessage("INVALID USER PASSWORD & USER NAME..!!");
                    TxtPassword.Focus();
                    return false;
                }
            }
            return true;
        }

        // OBJECT FOR THIS FORM
        private readonly IMasterSetup _objMaster;
    }
}