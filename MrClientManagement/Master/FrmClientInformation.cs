using DevExpress.XtraEditors;
using MrDAL.Core.Extensions;
using MrDAL.Domains.CRM.Common;
using MrDAL.Domains.CRM.Master;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using System;
using System.Windows.Forms;

namespace MrClientManagement.Master
{
    public partial class FrmClientInformation : XtraForm
    {
        // CLIENT INFORMATION

        #region --------------- CLIENT INFORMATION ---------------

        public FrmClientInformation()
        {
            InitializeComponent();
            _crmMaster = new CrmMaster();
            _master = new ClsMasterSetup();
            EnableControl();
            ClearControl();
        }

        private void FrmClientInformation_Load(object sender, EventArgs e)
        {
        }

        private void FrmClientInformation_Shown(object sender, EventArgs e)
        {
            BtnNew.Focus();
        }

        private void FrmClientInformation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar is (char)Keys.Escape)
            {
                if (!BtnNew.Enabled || TxtDescription.IsValueExits())
                {
                    if (CustomMessageBox.ClearVoucherDetails("CLIENT INFORMATION") is DialogResult.Yes)
                    {
                        _actionTag = string.Empty;
                        ClearControl();
                        EnableControl();
                        BtnNew.Focus();
                    }
                    else
                    {
                        Close();
                    }
                }
                else if (CustomMessageBox.ExitActiveForm() is DialogResult.Yes)
                {
                    Close();
                }
            }
            if (e.KeyChar is (char)Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtDescription_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (TxtDescription.IsValueExits() && _actionTag.IsValueExits())
            {
                var result = TxtDescription.AlreadyExits("ClientCollection", "ClientDescription", _actionTag, ClientId.ToString());
                if (result)
                {
                    if (_actionTag.Equals("SAVE"))
                    {
                        TxtDescription.WarningMessage("CLIENT DESCRIPTION IS ALREADY EXITS..!!");
                        return;
                    }
                }
            }

            if (TxtDescription.IsBlankOrEmpty() && _actionTag.IsValueExits() && TxtDescription.ValidControl(ActiveControl))
            {
                TxtDescription.WarningMessage($"CLIENT DESCRIPTION IS REQUIRED FOR [{_actionTag}]..!!");
                return;
            }
        }

        private void TxtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode is Keys.F1)
            {
                BtnDescription_Click(sender, e);
            }
            else if (TxtDescription.ReadOnly)
            {
                ClsKeyPreview.KeyEvent(e, "DELETE", TxtDescription, BtnDescription);
            }
        }

        private void BtnDescription_Click(object sender, EventArgs e)
        {
            var (description, id) = GetCrmMasterList.GetClientCollection("", false);
            if (description.IsValueExits())
            {
                if (_actionTag != "SAVE")
                {
                    TxtDescription.Text = description;
                    ClientId = id;
                    FillClientInformation();
                    TxtDescription.ReadOnly = false;
                }
            }
            TxtDescription.Focus();
        }

        private void TxtSource_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode is Keys.F1)
            {
                BtnSource_Click(sender, e);
            }
            else if (e.Control && e.KeyCode is Keys.N)
            {
            }
            else
            {
                ClsKeyPreview.KeyEvent(e, "DELETE", TxtSource, BtnSource);
            }
        }

        private void TxtSource_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (TxtSource.IsBlankOrEmpty() && _actionTag.IsValueExits() && TxtSource.ValidControl(ActiveControl))
            {
                TxtSource.WarningMessage($"CLIENT SOURCE IS REQUIRED FOR [{_actionTag}]..!!");
                return;
            }
        }

        private void BtnSource_Click(object sender, EventArgs e)
        {
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            _actionTag = "SAVE";
            ClearControl();
            EnableControl(true);
            TxtDescription.Focus();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            _actionTag = "UPDATE";
            ClearControl();
            EnableControl(true);
            TxtDescription.Focus();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            _actionTag = "DELETE";
            ClearControl();
            EnableControl();
            TxtDescription.Focus();
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            GetCrmMasterList.GetClientCollection("", false);
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            if (BtnNew.Enabled && TxtDescription.IsValueExits())
            {
                if (CustomMessageBox.ClearVoucherDetails("CLIENT INFORMATION") is DialogResult.Yes)
                {
                    ClearControl();
                    TxtDescription.Focus();
                }
                else
                {
                    Close();
                }
            }
            else if (CustomMessageBox.ExitActiveForm() is DialogResult.Yes)
            {
                Close();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (IsFormValid())
            {
                var result = SaveClientInformation();
                if (result != 0)
                {
                    CustomMessageBox.Information($"CLIENT INFORMATION {_actionTag} SUCCESSFULLY..!!");
                    ClearControl();
                    TxtDescription.Focus();
                    return;
                }
                TxtDescription.WarningMessage("INVALID INFORMATION..!! CONTACT TO VENDOR");
                return;
            }
            else
            {
                return;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            BtnExit.PerformClick();
        }

        #endregion --------------- CLIENT INFORMATION ---------------

        //METHOD

        #region --------------- METHOD ---------------

        private bool IsFormValid()
        {
            if (TxtDescription.IsBlankOrEmpty())
            {
                TxtDescription.WarningMessage($"DESCRIPTION IS REQUIRED FOR {_actionTag}");
                return false;
            }
            if (TxtSource.IsBlankOrEmpty() && TxtSource.Enabled)
            {
                TxtSource.WarningMessage($"CLIENT SOURCE IS REQUIRED FOR {_actionTag}");
                return false;
            }
            if (_actionTag != "SAVE" && ClientId is 0)
            {
                TxtDescription.WarningMessage($"SELECTED CLIENT IS INVALID..!!");
                return false;
            }
            return true;
        }

        private int SaveClientInformation()
        {
            _crmMaster.ClientInfo.ActionTag = _actionTag;
            _crmMaster.ClientInfo.ClientId = _actionTag switch
            {
                "SAVE" => _crmMaster.ReturnMaxIdFromTable("ClientCollection"),
                _ => ClientId
            };
            _crmMaster.ClientInfo.ClientDescription = TxtDescription.GetString();
            _crmMaster.ClientInfo.PanNo = TxtPanNo.GetDecimal();
            _crmMaster.ClientInfo.ClientAddress = TxtAddress.GetString();
            _crmMaster.ClientInfo.EmailAddress = TxtEmailAddress.GetString();
            _crmMaster.ClientInfo.ContactNo = TxtContactNo.GetString();
            _crmMaster.ClientInfo.PhoneNo = TxtPhoneNo.GetString();
            _crmMaster.ClientInfo.CollectionSource = TxtSource.GetString();
            _crmMaster.ClientInfo.Status = ChkActive.Checked;
            _crmMaster.ClientInfo.EnterBy = ObjGlobal.LogInUser;
            _crmMaster.ClientInfo.EnterDate = DateTime.Now;
            return _crmMaster.SaveClientCollection();
        }

        private void ClearControl()
        {
            Text = _actionTag.IsValueExits()
                ? $@"CLIENT INFORMATION SETUP [{_actionTag}]"
                : $@"CLIENT INFORMATION SETUP";
            ClientId = 0;
            TxtDescription.Clear();
            if (_actionTag.IsValueExits() && _actionTag != "SAVE")
            {
                TxtDescription.ReadOnly = true;
            }
            TxtPanNo.Clear();
            TxtAddress.Clear();
            TxtEmailAddress.Clear();
            TxtContactNo.Clear();
            TxtPhoneNo.Clear();
            TxtSource.Clear();
            ChkActive.Checked = true;
        }

        private void EnableControl(bool isEnable = false)
        {
            BtnNew.Enabled = !isEnable;
            BtnEdit.Enabled = BtnDelete.Enabled = !isEnable;

            TxtDescription.Enabled = isEnable || (_actionTag != "SAVE" && _actionTag.IsValueExits());
            BtnDescription.Enabled = TxtDescription.Enabled;

            TxtPanNo.Enabled = isEnable;
            TxtAddress.Enabled = isEnable;
            TxtEmailAddress.Enabled = isEnable;
            TxtContactNo.Enabled = isEnable;
            TxtPhoneNo.Enabled = isEnable;
            TxtSource.Enabled = isEnable;

            BtnSave.Enabled = isEnable || (_actionTag != "SAVE" && _actionTag.IsValueExits());
            BtnCancel.Enabled = BtnSave.Enabled;
        }

        private void FillClientInformation()
        {
            var dt = _crmMaster.GetClientCollectionInformation(ClientId);
            if (dt.RowsCount() is 0)
            {
                CustomMessageBox.Warning("SELECTED CLIENT IS INVALID..!!");
                return;
            }
            TxtDescription.Text = dt.Rows[0]["ClientDescription"].GetString();
            TxtPanNo.Text = dt.Rows[0]["PanNo"].GetString();
            TxtAddress.Text = dt.Rows[0]["ClientAddress"].GetString();
            TxtEmailAddress.Text = dt.Rows[0]["EmailAddress"].GetString();
            TxtContactNo.Text = dt.Rows[0]["ContactNo"].GetString();
            TxtPhoneNo.Text = dt.Rows[0]["PhoneNo"].GetString();
            TxtSource.Text = dt.Rows[0]["CollectionSource"].GetString();
            ChkActive.Checked = dt.Rows[0]["Status"].GetBool();
        }

        #endregion --------------- METHOD ---------------

        //OBJECT

        #region --------------- OBJECT ---------------

        public long ClientId;
        private string _actionTag;
        private readonly IMasterSetup _master;
        private ICrmMaster _crmMaster;

        #endregion --------------- OBJECT ---------------
    }
}