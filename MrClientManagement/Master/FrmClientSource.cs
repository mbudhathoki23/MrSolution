using MrDAL.Core.Extensions;
using MrDAL.Domains.CRM.Common;
using MrDAL.Domains.CRM.Master;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MrClientManagement.Master
{
    public partial class FrmClientSource : DevExpress.XtraEditors.XtraForm
    {
        // CLIENT SOURCE SETUP

        #region --------------- CLIENT SOURCE SETUP ---------------

        public FrmClientSource()
        {
            _crmMaster = new CrmMaster();
            InitializeComponent();
            ClearControl();
            EnableControl();
        }

        private void FrmClientSource_Load(object sender, EventArgs e)
        {
            BtnNew.Focus();
        }

        private void FrmClientSource_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar is (char)Keys.Escape)
            {
                if (!BtnNew.Enabled || TxtDescription.IsValueExits())
                {
                    if (CustomMessageBox.ClearVoucherDetails("CLIENT SOURCE") is DialogResult.Yes)
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

        private void FrmClientSource_Shown(object sender, EventArgs e)
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
            GetCrmMasterList.GetClientSource("", false);
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            if (BtnNew.Enabled && TxtDescription.IsValueExits())
            {
                if (CustomMessageBox.ClearVoucherDetails("CLIENT SOURCE") is DialogResult.Yes)
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

        private void TxtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (TxtDescription.IsValueExits() && _actionTag.IsValueExits())
            {
                var result = TxtDescription.AlreadyExits("ClientSource", "SDescription", _actionTag, "");
                if (result)
                {
                    if (_actionTag.Equals("SAVE"))
                    {
                        TxtDescription.WarningMessage("CLIENT SOURCE DESCRIPTION IS ALREADY EXITS..!!");
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
            var description = GetCrmMasterList.GetClientSource("", false);
            if (description.IsValueExits())
            {
                if (_actionTag != "SAVE")
                {
                    TxtDescription.Text = description;
                    FillClientSource();
                    TxtDescription.ReadOnly = false;
                }
            }
            TxtDescription.Focus();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (IsFormValid())
            {
                if (SaveClientSource() != 0)
                {
                    CustomMessageBox.Information($"CLIENT SOURCE IS {_actionTag} SUCCESSFULLY..!!");
                    ClearControl();
                    TxtDescription.Focus();
                    return;
                }
                TxtDescription.WarningMessage("INVALID INFORMATION..!! CONTACT TO VENDOR");
                return;
            }
            else return;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            BtnExit.PerformClick();
        }

        #endregion --------------- CLIENT SOURCE SETUP ---------------

        // METHOD FOR THIS FORM

        #region --------------- METHOD FOR THIS FORM

        private bool IsFormValid()
        {
            if (TxtDescription.IsBlankOrEmpty())
            {
                TxtDescription.WarningMessage("SOURCE DESCRIPTION IS REQUIRED..!!");
                TxtDescription.Focus();
                return false;
            }
            return true;
        }

        private int SaveClientSource()
        {
            _crmMaster.ClientSource.ActionTag = _actionTag;
            _crmMaster.ClientSource.SourceId = _actionTag is "SAVE" ? _crmMaster.ReturnMaxIdFromTable("ClientSource").GetInt() : _sourceId;
            _crmMaster.ClientSource.SDescription = TxtDescription.Text;
            _crmMaster.ClientSource.IsActive = ChkActive.Checked;
            _crmMaster.ClientSource.EnterBy = ObjGlobal.LogInUser;
            _crmMaster.ClientSource.EnterDate = DateTime.Now;
            return _crmMaster.SaveClientSource();
        }

        private void ClearControl()
        {
            Text = _actionTag.IsValueExits()
                ? $@"CLIENT INFORMATION SETUP [{_actionTag}]"
                : $@"CLIENT INFORMATION SETUP";
            TxtDescription.Clear();
            ChkActive.Checked = true;
        }

        private void EnableControl(bool isEnable = false)
        {
            BtnNew.Enabled = !isEnable;
            BtnEdit.Enabled = BtnDelete.Enabled = !isEnable;

            TxtDescription.Enabled = isEnable || (_actionTag != "SAVE" && _actionTag.IsValueExits());
            BtnDescription.Enabled = TxtDescription.Enabled;

            BtnSave.Enabled = isEnable || (_actionTag != "SAVE" && _actionTag.IsValueExits());
            BtnCancel.Enabled = BtnSave.Enabled;
        }

        private void FillClientSource()
        {
            var dt = _crmMaster.GetClientSource(TxtDescription.Text);
            if (dt.RowsCount() is 0)
            {
                CustomMessageBox.Warning("SELECTED CLIENT SOURCE IS INVALID..!!");
                return;
            }
            TxtDescription.Text = dt.Rows[0]["SDescription"].GetString();
        }

        #endregion --------------- METHOD FOR THIS FORM

        // OBJECT FOR THIS FORM

        #region --------------- OBJECT FOR THIS FORM ---------------

        public int _sourceId;
        private string _actionTag;
        private ICrmMaster _crmMaster;

        #endregion --------------- OBJECT FOR THIS FORM ---------------
    }
}