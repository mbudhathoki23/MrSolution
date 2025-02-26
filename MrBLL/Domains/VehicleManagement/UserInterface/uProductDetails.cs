using DevExpress.XtraEditors;
using MrDAL.Domains.VehicleManagement;
using MrDAL.Global.Common;
using MrDAL.Global.WinForm;
using MrDAL.Utility.Server;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace MrBLL.Domains.VehicleManagement.UserInterface;

public partial class uProductDetails : XtraUserControl
{
    #region --------------- Global ---------------

    private string _ActionTagVehicleNo = string.Empty;
    private string _ActionTagVehicleColor = string.Empty;
    private string _ActionTagVehicleModel = string.Empty;
    private string _SearchKey = string.Empty;
    private int VehicleNoId;
    private int VehicleColorId;
    private int VehicleModelId;
    private readonly ClsVMaster ObjMaster = new();

    #endregion --------------- Global ---------------

    #region --------------- Form Model ---------------

    public uProductDetails()
    {
        InitializeComponent();
    }

    private void uProductDetails_Load(object sender, EventArgs e)
    {
        _ActionTagVehicleModel = _ActionTagVehicleColor = _ActionTagVehicleNo = "SAVE";
        CmbState.SelectedIndex = 0;
        BindDataToGrid();
    }

    #endregion --------------- Form Model ---------------

    #region --------------- Vehicle Number ---------------

    private void TxtVechileNoDesc_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtVechileNoDesc.Text) && TxtVechileNoDesc.Text.Trim().Length == 0 &&
                TxtVechileNoDesc.Focused && !string.IsNullOrEmpty(_ActionTagVehicleNo))
            {
                MessageBox.Show(@"Vechile No is Blank..!!", ObjGlobal.Caption);
                TxtVechileNoDesc.Focus();
                return;
            }

            SendKeys.Send("{TAB}");
        }

        if (e.KeyCode == Keys.F1) BtnVechileNo_Click(sender, e);
    }

    private void TxtVechileNoDesc_Validating(object sender, CancelEventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtVechileNoDesc.Text.Trim()))
        {
            var ChkTable = new DataTable();
            if (_ActionTagVehicleNo == "SAVE")
            {
                ChkTable.Reset();
                ChkTable = GetConnection.SelectDataTableQuery(
                    "SELECT * FROM AMS.VehicleNumber vn WHERE vn.VNDesc='" + TxtVechileNoDesc.Text.Trim() + "'");
                if (ChkTable.Rows.Count > 0)
                {
                    MessageBox.Show(@"Vechile No Already Exits..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    TxtVechileNoDesc.Focus();
                }
            }
            else if (_ActionTagVehicleNo == "UPDATE")
            {
                ChkTable.Reset();
                ChkTable = GetConnection.SelectDataTableQuery(
                    "SELECT * FROM AMS.VehicleNumber vn WHERE vn.VNDesc='" + TxtVechileNoDesc.Text.Trim() +
                    "' and vn.VHNoId <> '" + VehicleNoId + "'");
                if (ChkTable.Rows.Count > 0)
                {
                    MessageBox.Show(@"Vechile No Already Exits..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    TxtVechileNoDesc.Focus();
                }
            }
        }
    }

    private void TxtVechileNoDesc_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtVechileNoDesc, 'E');
    }

    private void TxtVechileNoDesc_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtVechileNoDesc, 'L');
        if (string.IsNullOrEmpty(TxtVechileNoDesc.Text) && TxtVechileNoDesc.Text.Trim().Length == 0 &&
            TxtVechileNoDesc.Focused && !string.IsNullOrEmpty(_ActionTagVehicleNo))
        {
            MessageBox.Show(@"Vechile No is Blank..!!", ObjGlobal.Caption);
            TxtVechileNoDesc.Focus();
        }
    }

    private void BtnVechileNo_Click(object sender, EventArgs e)
    {
        using (var frmPickList = new FrmAutoPopList("MIN", "VEHICLENUMBER", _SearchKey, _ActionTagVehicleNo,
                   string.Empty, "LIST"))
        {
            if (FrmAutoPopList.GetListTable == null)
            {
                MessageBox.Show(@"Vehicle No Not Found..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                TxtVechileNoDesc.Focus();
                return;
            }

            if (FrmAutoPopList.GetListTable.Rows.Count > 0)
            {
                frmPickList.ShowDialog();
                if (frmPickList.SelectedList.Count > 0)
                {
                    TxtVechileNoDesc.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                    VehicleNoId = Convert.ToInt32(frmPickList.SelectedList[0]["LedgerId"].ToString().Trim());
                    _ActionTagVehicleNo = "UPDATE";
                    SetDataToVechileNoText(VehicleNoId);
                }

                frmPickList.Dispose();
            }
            else
            {
                MessageBox.Show(@"Vehicle No Not Found..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                TxtVechileNoDesc.Focus();
                return;
            }
        }

        _SearchKey = string.Empty;
        TxtVechileNoDesc.Focus();
    }

    private void TxtVechileNoShortName_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtVechileNoShortName, 'E');
    }

    private void TxtVechileNoShortName_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtVechileNoShortName, 'L');
        if (string.IsNullOrEmpty(TxtVechileNoShortName.Text) && TxtVechileNoShortName.Text.Trim().Length == 0 &&
            TxtVechileNoShortName.Focused && !string.IsNullOrEmpty(_ActionTagVehicleNo))
        {
            MessageBox.Show(@"ShortName is Blank..!!", ObjGlobal.Caption);
            TxtVechileNoShortName.Focus();
        }
    }

    private void TxtNumber_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtNumber, 'E');
    }

    private void TxtNumber_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtNumber, 'L');
    }

    private void TxtNumber_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Back) return;
        e.Handled = !int.TryParse(e.KeyChar.ToString(), out var isNumber);
        if (e.KeyChar == (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void TxtNumber_TextChanged(object sender, EventArgs e)
    {
        TxtVechileNoDesc.Text = CmbState.Text.Trim() + " " + TxtVechileNoShortName.Text.Trim() + " " +
                                TxtNumber.Text.Trim();
    }

    private void TxtNumber_Validating(object sender, CancelEventArgs e)
    {
        TxtVechileNoDesc_Validating(sender, e);
    }

    private void TxtVechileNoShortName_Validating(object sender, CancelEventArgs e)
    {
    }

    private void TxtVechileNoShortName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtVechileNoDesc.Text) && TxtVechileNoDesc.Text.Trim().Length == 0 &&
                TxtVechileNoDesc.Focused && !string.IsNullOrEmpty(_ActionTagVehicleNo))
            {
                MessageBox.Show(@"ShortName is Blank..!!", ObjGlobal.Caption);
                TxtVechileNoDesc.Focus();
            }
            else
            {
                SendKeys.Send("{TAB}");
            }
        }
    }

    private void CmbState_Enter(object sender, EventArgs e)
    {
        ObjGlobal.ComboBoxBackColor(CmbState, 'E');
    }

    private void CmbState_Leave(object sender, EventArgs e)
    {
        ObjGlobal.ComboBoxBackColor(CmbState, 'L');
    }

    private void CmbState_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Space)
            SendKeys.Send("{F4}");
        else if (e.KeyChar == (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    #endregion --------------- Vehicle Number ---------------

    #region --------------- Vehicle Colors ---------------

    private void TxtVechileColorsDesc_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtVechileColorsDesc, 'E');
    }

    private void TxtVechileColorsDesc_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtVechileColorsDesc, 'L');
        if (string.IsNullOrEmpty(TxtVechileColorsDesc.Text) && TxtVechileColorsDesc.Text.Trim().Length == 0 &&
            TxtVechileColorsDesc.Focused && !string.IsNullOrEmpty(_ActionTagVehicleColor))
        {
            MessageBox.Show(@"Vechile No is Blank..!!", ObjGlobal.Caption);
            TxtVechileColorsDesc.Focus();
        }
    }

    private void TxtVechileColorsDesc_Validating(object sender, CancelEventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtVechileColorsDesc.Text.Trim()))
        {
            var ChkTable = new DataTable();
            if (_ActionTagVehicleColor == "SAVE")
            {
                ChkTable.Reset();
                ChkTable = GetConnection.SelectDataTableQuery(
                    "SELECT * FROM AMS.VehicleColors vc WHERE vc.VHColorsDesc ='" +
                    TxtVechileColorsDesc.Text.Trim() + "'");
                if (ChkTable.Rows.Count > 0)
                {
                    MessageBox.Show(@"ShortName Already Exits..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    TxtVechileColorsDesc.Focus();
                }
            }
            else if (_ActionTagVehicleColor == "UPDATE")
            {
                ChkTable.Reset();
                ChkTable = GetConnection.SelectDataTableQuery(
                    "SELECT * FROM AMS.VehicleColors vc WHERE vc.VHColorsDesc='" +
                    TxtVechileColorsDesc.Text.Trim() + "' and vc.VHColorsId <>'" + VehicleColorId + "'");
                if (ChkTable.Rows.Count > 0)
                {
                    MessageBox.Show(@"Vechile Color Already Exits..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    TxtVechileColorsDesc.Focus();
                }
            }
        }
    }

    private void TxtVechileColorsDesc_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtVechileColorsDesc.Text) && TxtVechileColorsDesc.Text.Trim().Length == 0 &&
                TxtVechileColorsDesc.Focused && !string.IsNullOrEmpty(_ActionTagVehicleColor))
            {
                MessageBox.Show(@"Vechile Color is Blank..!!", ObjGlobal.Caption);
                TxtVechileColorsDesc.Focus();
                return;
            }

            SendKeys.Send("{TAB}");
        }

        if (e.KeyCode == Keys.F1) BtnVechileColors_Click(sender, e);
    }

    private void BtnVechileColors_Click(object sender, EventArgs e)
    {
        using (var frmPickList = new FrmAutoPopList("MIN", "VEHICLECOLOR", _SearchKey, _ActionTagVehicleNo,
                   string.Empty, "LIST"))
        {
            if (FrmAutoPopList.GetListTable.Rows.Count > 0)
            {
                frmPickList.ShowDialog();
                if (frmPickList.SelectedList.Count > 0)
                {
                    TxtVechileColorsDesc.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                    VehicleColorId = Convert.ToInt32(frmPickList.SelectedList[0]["LedgerId"].ToString().Trim());
                    _ActionTagVehicleColor = "UPDATE";
                    SetDataToVechileColorText(VehicleColorId);
                }

                frmPickList.Dispose();
            }
            else
            {
                MessageBox.Show(@"Vehicle No Not Found..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                TxtVechileColorsDesc.Focus();
                return;
            }
        }

        _SearchKey = string.Empty;
        TxtVechileColorsDesc.Focus();
    }

    private void TxtVechileColorsShortName_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtVechileColorsShortName, 'E');
    }

    private void TxtVechileColorsShortName_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtVechileColorsShortName, 'L');
        if (string.IsNullOrEmpty(TxtVechileColorsShortName.Text) &&
            TxtVechileColorsShortName.Text.Trim().Length == 0 && TxtVechileColorsShortName.Focused &&
            !string.IsNullOrEmpty(_ActionTagVehicleColor))
        {
            MessageBox.Show(@"ShortName is Blank..!!", ObjGlobal.Caption);
            TxtVechileColorsShortName.Focus();
        }
    }

    private void TxtVechileColorsShortName_Validating(object sender, CancelEventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtVechileColorsShortName.Text.Trim()))
        {
            var ChkTable = new DataTable();
            if (_ActionTagVehicleColor == "SAVE")
            {
                ChkTable.Reset();
                ChkTable = GetConnection.SelectDataTableQuery(
                    "SELECT * FROM AMS.VehicleColors vc WHERE vc.VHColorsShortName ='" +
                    TxtVechileColorsShortName.Text.Trim() + "'");
                if (ChkTable.Rows.Count > 0)
                {
                    MessageBox.Show(@"ShortName Already Exits..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    TxtVechileColorsDesc.Focus();
                }
            }
            else if (_ActionTagVehicleColor == "UPDATE")
            {
                ChkTable.Reset();
                ChkTable = GetConnection.SelectDataTableQuery(
                    "SELECT * FROM AMS.VehicleColors vc WHERE vc.VHColorsShortName='" +
                    TxtVechileColorsShortName.Text.Trim() + "' and vc.VHColorsId <>'" + VehicleColorId + "'");
                if (ChkTable.Rows.Count > 0)
                {
                    MessageBox.Show(@"ShortName Already Exits..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    TxtVechileColorsDesc.Focus();
                }
            }
        }
    }

    private void TxtVechileColorsShortName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtVechileColorsShortName.Text) &&
                TxtVechileColorsShortName.Text.Trim().Length == 0 && TxtVechileColorsShortName.Focused &&
                !string.IsNullOrEmpty(_ActionTagVehicleColor))
            {
                MessageBox.Show(@"ShortName is Blank..!!", ObjGlobal.Caption);
                TxtVechileColorsShortName.Focus();
            }
            else
            {
                SendKeys.Send("{TAB}");
            }
        }
    }

    #endregion --------------- Vehicle Colors ---------------

    #region --------------- Vehicle Model ---------------

    private void TxtVechileModelDesc_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtVechileModelDesc, 'E');
    }

    private void TxtVechileModelDesc_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtVechileModelDesc, 'L');
        if (string.IsNullOrEmpty(TxtVechileModelDesc.Text) && TxtVechileModelDesc.Text.Trim().Length == 0 &&
            TxtVechileModelDesc.Focused && !string.IsNullOrEmpty(_ActionTagVehicleModel))
        {
            MessageBox.Show(@"Vechile Model is Blank..!!", ObjGlobal.Caption);
            TxtVechileModelDesc.Focus();
        }
    }

    private void TxtVechileModelDesc_Validating(object sender, CancelEventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtVechileModelDesc.Text.Trim()))
        {
            var ChkTable = new DataTable();
            if (_ActionTagVehicleModel == "SAVE")
            {
                ChkTable.Reset();
                ChkTable = GetConnection.SelectDataTableQuery(
                    "SELECT * FROM AMS.VehileModel vc WHERE vc.VHModelDesc ='" + TxtVechileModelDesc.Text.Trim() +
                    "'");
                if (ChkTable.Rows.Count > 0)
                {
                    MessageBox.Show(@"Description Already Exits..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    TxtVechileModelDesc.Focus();
                }
            }
            else if (_ActionTagVehicleModel == "UPDATE")
            {
                ChkTable.Reset();
                ChkTable = GetConnection.SelectDataTableQuery(
                    "SELECT * FROM AMS.VehileModel vc WHERE vc.VHModelDesc='" + TxtVechileModelDesc.Text.Trim() +
                    "' and vc.VHModelId <>'" + VehicleColorId + "'");
                if (ChkTable.Rows.Count > 0)
                {
                    MessageBox.Show(@"Vechile Model Already Exits..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    TxtVechileModelDesc.Focus();
                }
            }
        }
    }

    private void TxtVechileModelDesc_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtVechileModelDesc.Text) && TxtVechileModelDesc.Text.Trim().Length == 0 &&
                TxtVechileModelDesc.Focused && !string.IsNullOrEmpty(_ActionTagVehicleModel))
            {
                MessageBox.Show(@"Vechile Model is Blank..!!", ObjGlobal.Caption);
                TxtVechileModelDesc.Focus();
                return;
            }

            SendKeys.Send("{TAB}");
        }

        if (e.KeyCode == Keys.F1) BtnVechileModel_Click(sender, e);
    }

    private void BtnVechileModel_Click(object sender, EventArgs e)
    {
        using (var frmPickList = new FrmAutoPopList("MIN", "VEHICLEMODEL", _SearchKey, _ActionTagVehicleNo,
                   string.Empty, "LIST"))
        {
            if (FrmAutoPopList.GetListTable.Rows.Count > 0)
            {
                frmPickList.ShowDialog();
                if (frmPickList.SelectedList.Count > 0)
                {
                    TxtVechileModelDesc.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                    VehicleModelId = Convert.ToInt32(frmPickList.SelectedList[0]["LedgerId"].ToString().Trim());
                    _ActionTagVehicleModel = "UPDATE";
                    SetDataToVechileModelText(VehicleModelId);
                }

                frmPickList.Dispose();
            }
            else
            {
                MessageBox.Show(@"Vehicle Model Not Found..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                TxtVechileModelDesc.Focus();
                return;
            }
        }

        _SearchKey = string.Empty;
        TxtVechileModelDesc.Focus();
    }

    private void TxtVechileModelShortName_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtVechileModelShortName, 'E');
    }

    private void TxtVechileModelShortName_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtVechileModelShortName, 'L');
        if (string.IsNullOrEmpty(TxtVechileModelShortName.Text) &&
            TxtVechileModelShortName.Text.Trim().Length == 0 && TxtVechileModelShortName.Focused &&
            !string.IsNullOrEmpty(_ActionTagVehicleModel))
        {
            MessageBox.Show(@"ShortName is Blank..!!", ObjGlobal.Caption);
            TxtVechileModelShortName.Focus();
        }
    }

    private void TxtVechileModelShortName_Validating(object sender, CancelEventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtVechileModelDesc.Text.Trim()))
        {
            var ChkTable = new DataTable();
            if (_ActionTagVehicleModel == "SAVE")
            {
                ChkTable.Reset();
                ChkTable = GetConnection.SelectDataTableQuery(
                    "SELECT * FROM AMS.VehileModel vc WHERE vc.VHModelShortName ='" +
                    TxtVechileModelShortName.Text.Trim() + "'");
                if (ChkTable.Rows.Count > 0)
                {
                    MessageBox.Show(@"Description Already Exits..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    TxtVechileModelShortName.Focus();
                }
            }
            else if (_ActionTagVehicleModel == "UPDATE")
            {
                ChkTable.Reset();
                ChkTable = GetConnection.SelectDataTableQuery(
                    "SELECT * FROM AMS.VehileModel vc WHERE vc.VHModelShortName='" +
                    TxtVechileModelShortName.Text.Trim() + "' and vc.VHModelId <>'" + VehicleColorId + "'");
                if (ChkTable.Rows.Count > 0)
                {
                    MessageBox.Show(@"Vechile Model Already Exits..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    TxtVechileModelShortName.Focus();
                }
            }
        }
    }

    private void TxtVechileModelShortName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtVechileModelShortName.Text) &&
                TxtVechileModelShortName.Text.Trim().Length == 0 && TxtVechileModelShortName.Focused &&
                !string.IsNullOrEmpty(_ActionTagVehicleModel))
            {
                MessageBox.Show(@"ShortName is Blank..!!", ObjGlobal.Caption);
                TxtVechileModelShortName.Focus();
            }
            else
            {
                SendKeys.Send("{TAB}");
            }
        }
    }

    #endregion --------------- Vehicle Model ---------------

    #region --------------- Button Event ---------------

    private void BtnVechileNoSave_Click(object sender, EventArgs e)
    {
        if (ValidVechileNo())
        {
            ObjMaster.ObjVMaster.MasterId = VehicleNoId;
            ObjMaster.ObjVMaster._ActionTag = _ActionTagVehicleNo;
            ObjMaster.ObjVMaster.TxtDescription = TxtVechileNoDesc.Text.Trim();
            ObjMaster.ObjVMaster.TxtShortName = TxtVechileNoShortName.Text.Trim();
            ObjMaster.ObjVMaster.TxtState = CmbState.Text;
            ObjMaster.ObjVMaster.TxtStatus = ChkVechileNoActive.Checked;
            ObjMaster.ObjVMaster.TxtEnterBy = ObjGlobal.LogInUser;
            ObjMaster.ObjVMaster.TxtBranchId = Convert.ToInt32(ObjGlobal.SysBranchId);
            ObjMaster.ObjVMaster.TxtCompanyUnitId = Convert.ToInt32(ObjGlobal.SysCompanyUnitId);
            var IsOk = ObjMaster.IUDVechileNumber();
            if (IsOk > 0)
            {
                MessageBox.Show($@"DATA {_ActionTagVehicleNo.ToUpper()} SUCESSFULLY..!!", ObjGlobal.Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                VehicleNoClearControl();
                CmbState.Focus();
            }
            else
            {
                MessageBox.Show($@"DATA {_ActionTagVehicleNo.ToUpper()} UNSUCESSFULLY..!!", ObjGlobal.Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                CmbState.Focus();
            }
        }
    }

    private void BtnVechileNoClear_Click(object sender, EventArgs e)
    {
        VehicleNoClearControl();
        TxtVechileNoDesc.Focus();
    }

    private void BtnVechileColorsSave_Click(object sender, EventArgs e)
    {
        if (ValidVechileColor())
        {
            ObjMaster.ObjVMaster.MasterId = VehicleColorId;
            ObjMaster.ObjVMaster._ActionTag = _ActionTagVehicleColor;
            ObjMaster.ObjVMaster.TxtDescription = TxtVechileColorsDesc.Text.Trim();
            ObjMaster.ObjVMaster.TxtShortName = TxtVechileColorsShortName.Text.Trim();
            ObjMaster.ObjVMaster.TxtStatus = ChkVechileColorsActive.Checked;
            ObjMaster.ObjVMaster.TxtEnterBy = ObjGlobal.LogInUser;
            ObjMaster.ObjVMaster.TxtBranchId = Convert.ToInt32(ObjGlobal.SysBranchId);
            ObjMaster.ObjVMaster.TxtCompanyUnitId = Convert.ToInt32(ObjGlobal.SysCompanyUnitId);
            var IsOk = ObjMaster.IUDVechileColor();
            if (IsOk > 0)
            {
                MessageBox.Show($@"DATA {_ActionTagVehicleNo.ToUpper()} SUCESSFULLY..!!", ObjGlobal.Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                VehicleColorClearControl();
                TxtVechileColorsDesc.Focus();
            }
        }
    }

    private void BtnVechileColorsClear_Click(object sender, EventArgs e)
    {
        VehicleColorClearControl();
    }

    private void BtnVechileModelSave_Click(object sender, EventArgs e)
    {
        if (ValidVechileModel())
        {
            ObjMaster.ObjVMaster.MasterId = VehicleModelId;
            ObjMaster.ObjVMaster._ActionTag = _ActionTagVehicleModel;
            ObjMaster.ObjVMaster.TxtDescription = TxtVechileModelDesc.Text.Trim();
            ObjMaster.ObjVMaster.TxtShortName = TxtVechileModelShortName.Text.Trim();
            ObjMaster.ObjVMaster.TxtStatus = ChkVechileModelActive.Checked;
            ObjMaster.ObjVMaster.TxtEnterBy = ObjGlobal.LogInUser;
            ObjMaster.ObjVMaster.TxtBranchId = Convert.ToInt32(ObjGlobal.SysBranchId);
            ObjMaster.ObjVMaster.TxtCompanyUnitId = Convert.ToInt32(ObjGlobal.SysCompanyUnitId);
            var IsOk = ObjMaster.IUDVechileModel();
            if (IsOk > 0)
            {
                MessageBox.Show($@"DATA {_ActionTagVehicleNo.ToUpper()} SUCESSFULLY..!!", ObjGlobal.Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                VehicleModelClearControl();
                TxtVechileModelDesc.Focus();
            }
        }
    }

    private void BtnVechileModelClear_Click(object sender, EventArgs e)
    {
        VehicleModelClearControl();
        TxtVechileModelDesc.Focus();
    }

    #endregion --------------- Button Event ---------------

    #region ---------------  Method ---------------

    private bool ValidVechileNo()
    {
        var IsOkVechileNo = true;
        if (string.IsNullOrEmpty(TxtVechileNoDesc.Text.Trim()))
        {
            MessageBox.Show(@"Vechile No is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtVechileNoDesc.Focus();
            IsOkVechileNo = false;
        }

        if (string.IsNullOrEmpty(TxtVechileNoShortName.Text.Trim()))
        {
            MessageBox.Show(@"ShortName is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtVechileNoDesc.Focus();
            IsOkVechileNo = false;
        }

        return IsOkVechileNo;
    }

    private void VehicleNoClearControl()
    {
        TxtVechileNoDesc.Clear();
        TxtNumber.Clear();
        ChkVechileNoActive.Checked = true;
    }

    private bool ValidVechileColor()
    {
        var IsOkVechileNo = true;
        if (string.IsNullOrEmpty(TxtVechileColorsDesc.Text.Trim()))
        {
            MessageBox.Show(@"Vechile Color is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtVechileColorsDesc.Focus();
            IsOkVechileNo = false;
        }

        if (string.IsNullOrEmpty(TxtVechileColorsShortName.Text.Trim()))
        {
            MessageBox.Show(@"ShortName is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtVechileColorsShortName.Focus();
            IsOkVechileNo = false;
        }

        return IsOkVechileNo;
    }

    private void VehicleColorClearControl()
    {
        TxtVechileColorsDesc.Clear();
        TxtVechileColorsShortName.Clear();
        ChkVechileColorsActive.Checked = true;
    }

    private bool ValidVechileModel()
    {
        var IsOkVechileNo = true;
        if (string.IsNullOrEmpty(TxtVechileModelDesc.Text.Trim()))
        {
            MessageBox.Show(@"Vechile Model is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtVechileModelDesc.Focus();
            IsOkVechileNo = false;
        }

        if (string.IsNullOrEmpty(TxtVechileModelShortName.Text.Trim()))
        {
            MessageBox.Show(@"ShortName is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtVechileModelShortName.Focus();
            IsOkVechileNo = false;
        }

        return IsOkVechileNo;
    }

    private void VehicleModelClearControl()
    {
        TxtVechileModelDesc.Clear();
        TxtVechileModelShortName.Clear();
        ChkVechileModelActive.Checked = true;
    }

    protected void SetDataToVechileModelText(int SelectedId)
    {
        var dt = ObjMaster.Get_DataVehicleModel(_ActionTagVehicleNo, SelectedId);
        if (dt.Rows.Count > 0)
        {
            TxtVechileModelDesc.Text = dt.Rows[0]["VHModelDesc"].ToString();
            TxtVechileModelShortName.Text = dt.Rows[0]["VHModelShortName"].ToString();
            ChkVechileNoActive.Checked = Convert.ToBoolean(dt.Rows[0]["VHModelActive"].ToString());
        }
    }

    protected void SetDataToVechileColorText(int SelectedId)
    {
        var dt = ObjMaster.Get_DataVehicleColor(_ActionTagVehicleNo, SelectedId);
        if (dt.Rows.Count > 0)
        {
            TxtVechileColorsDesc.Text = dt.Rows[0]["VHColorsDesc"].ToString();
            TxtVechileColorsShortName.Text = dt.Rows[0]["VHColorsShortName"].ToString();
            ChkVechileNoActive.Checked = Convert.ToBoolean(dt.Rows[0]["VHColorsActive"].ToString());
        }
    }

    protected void SetDataToVechileNoText(int SelectedId)
    {
        var dt = ObjMaster.Get_DataVehicleNumber(_ActionTagVehicleNo, SelectedId);
        if (dt.Rows.Count > 0)
        {
            TxtVechileNoDesc.Text = dt.Rows[0]["VNDesc"].ToString();
            TxtVechileNoShortName.Text = dt.Rows[0]["VNShortName"].ToString();
            CmbState.Text = dt.Rows[0]["VNState"].ToString();
            ChkVechileNoActive.Checked = Convert.ToBoolean(dt.Rows[0]["VNStatus"].ToString());
        }
    }

    private void BindDataToGrid()
    {
        var BindData = new DataTable();
        BindData = ObjMaster.GetCombineDataValue();
        RGrid.DataSource = BindData;
    }

    #endregion ---------------  Method ---------------
}