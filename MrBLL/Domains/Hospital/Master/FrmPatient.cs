using DevExpress.XtraEditors;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Domains.Hospital.Master;

public partial class FrmPatient : MrForm
{
    // PATIENT FORM & EVENTS

    #region -------------- Frm --------------

    public FrmPatient(bool zoom)
    {
        InitializeComponent();
        _master = new ClsHMaster();
        _isZoom = zoom;
        _actionTag = string.Empty;

        _master.ReturnTitleOfName(CmbTitle);
        _master.ReturnAgeType(CmbAgeType);
        _master.ReturnGender(CmbGender);
        _master.ReturnMartial(CmbMartial);
        _master.ReturnRegistrationType(CmbRegType);
        _master.ReturnBloodGroup(CmbBloodGroup);
        _master.ReturnReligion(CmbReligion);
        _master.ReturnNationality(CmbNationality);
        ControlClear();
        ControlEnable();

        CmbTitle.SelectedIndex = 0;
        CmbAgeType.SelectedIndex = 0;
        CmbGender.SelectedIndex = 0;
        CmbMartial.SelectedIndex = 0;
        CmbRegType.SelectedIndex = 0;
        CmbBloodGroup.SelectedIndex = 0;
        CmbNationality.SelectedIndex = 0;
        CmbReligion.SelectedIndex = 0;
        Shown += (sender, e) =>
        {
            if (_isZoom)
            {
                BtnNew.PerformClick();
                MskRegistrationDate.Focus();
            }
            else
            {
                BtnNew.Focus();
            }
        };
    }

    private void FrmPatient_Load(object sender, EventArgs e)
    {
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }

    private void FrmPatient_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)27)
        {
            if (BtnNew.Enabled == false)
            {
                if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
                {
                    _actionTag = string.Empty;
                    ControlEnable();
                    ControlClear();
                    BtnNew.Focus();
                }
            }
            else
            {
                BtnExit.PerformClick();
            }
        }
    }

    private void GlobalKeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) SendKeys.Send("{Tab}");
    }

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        ControlClear();
        ControlEnable(true);
        MskRegistrationDate.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _actionTag = "UPDATE";
        ControlClear();
        ControlEnable(true);
        TxtRegistrationId.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        ControlClear();
        ControlEnable();
        TxtRegistrationId.Focus();
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        if (MessageBox.Show("ARE YOU SURE WANT TO CLOSE FORM..!!", ObjGlobal.Caption, MessageBoxButtons.YesNo) ==
            DialogResult.Yes) Close();
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtRegistrationId.Text))
        {
            MessageBox.Show(@" PATIENT ID  IS REQUIRED..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtRegistrationId.Focus();
            return;
        }

        if (string.IsNullOrEmpty(TxtDescription.Text))
        {
            MessageBox.Show(@"PATIENT DESCRIPTION IS REQUIRED..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtDescription.Focus();
            return;
        }

        if (string.IsNullOrEmpty(TxtLedger.Text))
        {
            MessageBox.Show(@"BILLING DESCRIPTION IS REQUIRED..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtLedger.Focus();
            return;
        }

        if (SavePatientRegistration() != 0)
        {
            if (_isZoom)
            {
                PatientDesc = TxtRegistrationId.Text;
                Close();
                return;
            }

            CustomMessageBox.ActionSuccess(string.Empty, "PATIENT", _actionTag);
            ControlClear();
            MskRegistrationDate.Focus();
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (TxtDescription.Text.Trim() == string.Empty)
            BtnExit.PerformClick();
        else
            ControlClear();
    }

    private void BtnBrowser_Click(object sender, EventArgs e)
    {
        var fileDialog = new OpenFileDialog
        {
            Filter = @"Image Files (*.jpg;*.jpeg;.*.gif;)|*.jpg;*.jpeg;.*.gif"
        };
        if (fileDialog.ShowDialog() == DialogResult.OK)
        {
            PicPatients.Image = new Bitmap(fileDialog.FileName);
        }
    }

    private void PicPatients_DoubleClick(object sender, EventArgs e)
    {
        BtnBrowser_Click(sender, e);
    }

    private void BtnAttachment1_Click(object sender, EventArgs e)
    {
        var fileDialog = new OpenFileDialog
        {
            Filter = @"Image Files (*.jpg;*.jpeg;.*.gif;)|*.jpg;*.jpeg;.*.gif"
        };
        if (fileDialog.ShowDialog() == DialogResult.OK) PicAttachment1.Image = new Bitmap(fileDialog.FileName);
    }

    private void BtnAttachment2_Click(object sender, EventArgs e)
    {
        var fileDialog = new OpenFileDialog
        {
            Filter = @"Image Files (*.jpg;*.jpeg;.*.gif;)|*.jpg;*.jpeg;.*.gif"
        };
        if (fileDialog.ShowDialog() == DialogResult.OK) PicAttachment2.Image = new Bitmap(fileDialog.FileName);
    }

    private void BtnAttachment3_Click(object sender, EventArgs e)
    {
        var fileDialog = new OpenFileDialog
        {
            Filter = @"Image Files (*.jpg;*.jpeg;.*.gif;)|*.jpg;*.jpeg;.*.gif"
        };
        if (fileDialog.ShowDialog() == DialogResult.OK) PicAttachment3.Image = new Bitmap(fileDialog.FileName);
    }

    private void TxtRegistrationId_KeyPress(object sender, KeyPressEventArgs e)
    {
        GlobalKeyPress(sender, e);
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !int.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void MskRegistrationDate_Leave(object sender, EventArgs e)
    {
        //PatientId = TxtRegistrationId.Text.ReturnMaxLongId("PATIENT", "");
        //var year = DateTime.Now.Year.ToString();
        //TxtRegistrationId.Text = $@"{year} + {PatientId.ToString()}";
    }

    private void TxtName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (TxtDescription.IsValueExits())
            {
                GlobalKeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
            }
            else
            {
                CustomMessageBox.Warning("PATIENT DESCRIPTION IS BLANK..!!");
                TxtDescription.Focus();
            }
        }
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (string.IsNullOrEmpty(_actionTag)) return;
        MskRegistrationDate_Leave(sender, e);
        TxtDescription_TextChanged(sender, e);
    }

    private void TxtDescription_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtDescription.Text) && TxtDescription.Enabled)
        {
            CustomMessageBox.Warning(@"PATIENT NAME IS BLANK..!!");
            TxtDescription.Focus();
        }
    }

    private void TxtDescription_TextChanged(object sender, EventArgs e)
    {
        TxtLedger.Text = $@"{CmbTitle.Text} {TxtDescription.Text} ({TxtRegistrationId.Text})";
    }

    private void TxtAge_Validating(object sender, CancelEventArgs e)
    {
        if (TxtAge.GetInt() > 0) MskDOB.Text = DateTime.Now.AddYears(-TxtAge.GetInt()).GetDateString();
    }

    private void TxtAge_KeyPress(object sender, KeyPressEventArgs e)
    {
        GlobalKeyPress(sender, e);
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void TxtDepartment_Validating(object sender, CancelEventArgs e)
    {
    }

    private void TxtDepartment_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnDepartment_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            (TxtDepartment.Text, _departmentId) = GetMasterList.CreateHospitalDepartment(true);
            TxtDepartment.Focus();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtDepartment.IsValueExits())
            {
                GlobalKeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
            }
            else
            {
                CustomMessageBox.Warning("PATIENT DEPARTMENT IS BLANK..!!");
                TxtDepartment.Focus();
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtDepartment, BtnDepartment);
        }
    }

    private void TxtDoctor_Validating(object sender, CancelEventArgs e)
    {
    }

    private void TxtDoctor_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnDoctor_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            (TxtDoctor.Text, _doctorId) = GetMasterList.CreateDoctor(true);
            TxtDoctor.Focus();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtDoctor.IsValueExits())
            {
                GlobalKeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
            }
            else
            {
                CustomMessageBox.Warning("PATIENT DOCTOR IS BLANK..!!");
                TxtDoctor.Focus();
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtDoctor, BtnDoctor);
        }
    }

    private void BtnDepartment_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetHospitalDepartmentList(_actionTag);
        if (description.IsValueExits())
        {
            TxtDepartment.Text = description;
            _departmentId = id;
        }
        TxtDepartment.Focus();
    }

    private void BtnDoctor_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetDoctorList(_actionTag);
        if (description.IsValueExits())
        {
            TxtDoctor.Text = description;
            _doctorId = id;
        }
        TxtDoctor.Focus();
    }

    private void TxtRegistrationId_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1 && TxtRegistrationId.ReadOnly == false)
        {
            BtnPatientId.PerformClick();
        }
        else if (e.KeyCode == Keys.Enter)
        {
            if (TxtRegistrationId.Text.Trim() != string.Empty) SendKeys.Send("{TAB}");
        }
        else if (TxtRegistrationId.ReadOnly)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtRegistrationId, BtnPatientId);
        }
    }

    private void BtnPatientId_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetPatient(_actionTag);
        if (id > 0)
        {
            PatientId = id;
            if (!_actionTag.Equals("SAVE"))
            {
                SetGridDataToText();
            }
        }
        TxtRegistrationId.Focus();
    }

    private void TxtTState_TextChanged(object sender, EventArgs e)
    {
        TxtAddress.Text = string.IsNullOrEmpty(TxtTStreet.Text.Trim()) switch
        {
            false => TxtAddress.Text + @"," + TxtTStreet.Text.Trim(),
            _ => TxtTState.Text.Trim()
        };

        TxtAddress.Text = string.IsNullOrEmpty(TxtTCity.Text.Trim()) switch
        {
            false => TxtAddress.Text + "," + TxtTCity.Text.Trim(),
            _ => TxtAddress.Text
        };
    }

    private void TxtTStreet_TextChanged(object sender, EventArgs e)
    {
        TxtTState_TextChanged(sender, e);
    }

    private void TxtTCity_TextChanged(object sender, EventArgs e)
    {
        TxtTState_TextChanged(sender, e);
    }

    private void TxtPState_TextChanged(object sender, EventArgs e)
    {
        TxtPAddress.Text = TxtPState.Text.Trim();
        if (!string.IsNullOrEmpty(TxtPStreet.Text.Trim()))
            TxtPAddress.Text = TxtPAddress.Text + @"," + TxtPStreet.Text.Trim();

        if (!string.IsNullOrEmpty(TxtPCity.Text.Trim()))
            TxtPAddress.Text = TxtPAddress.Text + @"," + TxtPCity.Text.Trim();
    }

    private void TxtPCity_TextChanged(object sender, EventArgs e)
    {
        TxtPState_TextChanged(sender, e);
    }

    private void CmbAgeType_KeyPress(object sender, KeyPressEventArgs e)
    {
        GlobalKeyPress(sender, e);
        if (e.KeyChar == (char)Keys.Space) SendKeys.Send("{F4}");
    }

    private void CmbGender_KeyPress(object sender, KeyPressEventArgs e)
    {
        GlobalKeyPress(sender, e);
        if (e.KeyChar == (char)Keys.Space) SendKeys.Send("{F4}");
    }

    private void CmbMartial_KeyPress(object sender, KeyPressEventArgs e)
    {
        GlobalKeyPress(sender, e);
        if (e.KeyChar == (char)Keys.Space) SendKeys.Send("{F4}");
    }

    private void CmbRegType_KeyPress(object sender, KeyPressEventArgs e)
    {
        GlobalKeyPress(sender, e);
        if (e.KeyChar == (char)Keys.Space) SendKeys.Send("{F4}");
    }

    private void CmbReligion_KeyPress(object sender, KeyPressEventArgs e)
    {
        GlobalKeyPress(sender, e);
        if (e.KeyChar == (char)Keys.Space) SendKeys.Send("{F4}");
    }

    private void CmbNationality_KeyPress(object sender, KeyPressEventArgs e)
    {
        GlobalKeyPress(sender, e);
        if (e.KeyChar == (char)Keys.Space) SendKeys.Send("{F4}");
    }

    private void CmbBloodGroup_KeyPress(object sender, KeyPressEventArgs e)
    {
        GlobalKeyPress(sender, e);
        if (e.KeyChar == (char)Keys.Space) SendKeys.Send("{F4}");
    }

    private void TxtAge_Leave(object sender, EventArgs e)
    {
        double.TryParse(TxtAge.Text, out var result);
        if (result != 0 || string.IsNullOrEmpty(_actionTag)) return;
        MessageBox.Show(@"Age Can't be Zero..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        TxtAge.Focus();
    }

    private void TxtPastHistory_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (TxtPastHistory.IsBlankOrEmpty())
            {
                TxtPastHistory_Validating(this, null);
            }
        }
    }

    private void TxtPastHistory_Validating(object sender, CancelEventArgs e)
    {
        TabPatient.SelectedTab = TabInformation;
        TxtPState.Focus();
    }

    private void TxtPhoneNumber_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            BtnSave.Focus();
        }
    }

    #endregion -------------- Frm --------------

    // METHOD FOR THIS FORM

    #region --------------- Method ---------------

    private int SavePatientRegistration()
    {
        _master.VmPatient.ActionTag = _actionTag;
        PatientId = _actionTag.Equals("SAVE") ? PatientId.ReturnMaxLongId("PATIENT", "") : PatientId;
        _master.VmPatient.PaitentId = PatientId;
        _master.VmPatient.RefDate = MskRegistrationDate.Text.GetDateTime();
        _master.VmPatient.IpdId = 0;
        _master.VmPatient.Title = CmbTitle.Text.Replace("'", "''");
        _master.VmPatient.NepaliDesc = TxtDescription.Text.Replace("'", "''");
        _master.VmPatient.PaitentDesc = TxtDescription.Text.Replace("'", "''");
        _master.VmPatient.ShortName = TxtRegistrationId.Text.Replace("'", "''");
        _master.VmPatient.TAddress = TxtAddress.Text.IsValueExits() ? TxtAddress.Text : "NEPAL";
        _master.VmPatient.PAddress = TxtPAddress.Text.Replace("'", "''");
        _master.VmPatient.AccountLedger = TxtLedger.Text.Replace("'", "''");
        _master.VmPatient.ContactNo = TxtContactNo.Text.Replace("'", "''");
        _master.VmPatient.Age = TxtAge.GetInt();
        _master.VmPatient.AgeType = CmbAgeType.SelectedValue.ToString();
        _master.VmPatient.DateOfBirth = MskDOB.Text.GetDateTime();
        _master.VmPatient.Gender = CmbMartial.SelectedValue.ToString();
        _master.VmPatient.MaritalStatus = CmbMartial.SelectedValue.ToString();
        _master.VmPatient.RegType = CmbRegType.SelectedValue.ToString();
        _master.VmPatient.Nationality = CmbNationality.SelectedValue.ToString();
        _master.VmPatient.Religion = CmbReligion.SelectedValue.ToString();
        _master.VmPatient.BloodGrp = CmbBloodGroup.SelectedValue.ToString();
        _master.VmPatient.DepartmentId = _departmentId;
        _master.VmPatient.DrId = _doctorId;
        _master.VmPatient.RefDrDesc = TxtReferDoctor.Text.Replace("'", "''");
        _master.VmPatient.EmailAdd = TxtEmailAddress.Text.Replace("'", "''");
        _master.VmPatient.PastHistory = TxtPastHistory.Text.Replace("'", "''");
        _master.VmPatient.ContactPer = TxtContactPerson.Text.Replace("'", "''");
        _master.VmPatient.PhoneNo = TxtPhoneNumber.Text.Replace("'", "''");
        _master.VmPatient.Status = ChkActive.Checked;
        _master.VmPatient.EnterBy = ObjGlobal.LogInUser;
        _master.VmPatient.EnterDate = DateTime.Now;
        _master.VmPatient.BranchId = ObjGlobal.SysBranchId;
        _master.VmPatient.CompanyUnit = ObjGlobal.SysCompanyUnitId;
        _master.VmPatient.SyncBaseId = Guid.NewGuid();
        _master.VmPatient.SyncGlobalId = Guid.NewGuid();
        _master.VmPatient.SyncOriginId = Guid.NewGuid();
        _master.VmPatient.SyncCreatedOn = DateTime.Now;
        _master.VmPatient.SyncLastPatchedOn = DateTime.Now;
        var syncRow = _master.VmPatient.SyncRowVersion.ReturnSyncRowNo("PATIENT", PatientId.ToString());
        _master.VmPatient.SyncRowVersion = syncRow;
        return _master.SavePatient();
    }

    private void ControlEnable(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable && !_actionTag.Equals("DELETE");
        TxtRegistrationId.Enabled = !_actionTag.Equals("SAVE") && _actionTag.IsValueExits();
        BtnPatientId.Enabled = isEnable;
        MskRegistrationDate.Enabled = isEnable;
        TxtLedger.Enabled = false;
        CmbTitle.Enabled = isEnable;
        TxtDescription.Enabled = isEnable;
        TxtAddress.Enabled = TxtTState.Enabled = TxtTStreet.Enabled = TxtTCity.Enabled = isEnable;
        TxtPAddress.Enabled = TxtPState.Enabled = TxtPStreet.Enabled = TxtPCity.Enabled = isEnable;
        PicPatients.Enabled = isEnable;
        TxtAge.Enabled = TxtContactNo.Enabled = isEnable;
        CmbMartial.Enabled = CmbGender.Enabled = MskDOB.Enabled = CmbAgeType.Enabled = isEnable;
        CmbBloodGroup.Enabled = CmbReligion.Enabled = CmbNationality.Enabled = CmbRegType.Enabled = isEnable;
        TxtPhoneNumber.Enabled = TxtContactPerson.Enabled = TxtPastHistory.Enabled = isEnable;
        TxtEmailAddress.Enabled = TxtReferDoctor.Enabled = TxtDoctor.Enabled = TxtDepartment.Enabled = isEnable;
        BtnSave.Enabled = _actionTag == "DELETE" || isEnable;
        BtnCancel.Enabled = _actionTag == "DELETE" || isEnable;
        ChkActive.Enabled = _actionTag == "UPDATE";
    }

    private void ControlClear()
    {
        MskDOB.Text = MskRegistrationDate.Text = _actionTag == string.Empty
            ? DateTime.Now.ToString("dd/MM/yyyy")
            : MskRegistrationDate.Text;
        TxtRegistrationId.Text = TxtRegistrationId.ReturnMaxPatientId();
        TxtDescription.Clear();
        TxtAddress.Clear();
        TxtPAddress.Clear();
        TxtLedger.Clear();
        TxtContactNo.Clear();
        TxtAge.Clear();
        CmbAgeType.Text = string.Empty;
        TxtDepartment.Clear();
        TxtDoctor.Clear();
        TxtReferDoctor.Clear();
        TxtEmailAddress.Clear();
        TxtPastHistory.Clear();
        TxtContactPerson.Clear();
        TxtPhoneNumber.Clear();
        TabPatient.SelectedTab = TabPatientRegistration;
        MskDOB.Text = DateTime.Now.GetDateString();
    }

    private void SetGridDataToText()
    {
        var dtRecords = _master.ReturnPatientInformation(PatientId);
        if (dtRecords.Rows.Count <= 0 && !_actionTag.Equals("SAVE"))
        {
            TxtRegistrationId.WarningMessage("RECORDS NOT FOUND IN DATA..!!");
            return;
        }
        foreach (DataRow row in dtRecords.Rows)
        {
            MskRegistrationDate.Text = row["RefDate"].GetDateString();
            CmbTitle.SelectedIndex = CmbTitle.FindString(row["Title"].ToString());
            TxtDescription.Text = row["PaitentDesc"].GetString();
            TxtDepartment.Text = row["DName"].GetString();
            TxtDoctor.Text = row["DrName"].GetString();
            TxtReferDoctor.Text = row["RefDrDesc"].GetString();
            TxtEmailAddress.Text = row["EmailAdd"].GetString();
            TxtContactPerson.Text = row["ContactPer"].GetString();
            TxtContactNo.Text = row["ContactNo"].GetString();
            TxtPhoneNumber.Text = row["PhoneNo"].GetString();
            TxtPastHistory.Text = row["PastHistory"].GetString();

            TxtPAddress.Text = row["PAddress"].GetString();
            TxtAddress.Text = row["TAddress"].GetString();
        }
    }

    #endregion --------------- Method ---------------

    //OBJECT FOR THIS FORM

    #region -------------- Class --------------

    private int _departmentId;
    private int _doctorId;
    public long LedgerId = 0;
    public long PatientId;
    private string _actionTag;
    public string PatientDesc = string.Empty;
    private readonly bool _isZoom;
    private readonly IHMaster _master;

    #endregion -------------- Class --------------
}