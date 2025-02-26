using MrBLL.Master.LedgerSetup;
using MrBLL.Master.ProductSetup;
using MrDAL.Control.ControlsEx;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using MrDAL.Master;
using MrDAL.Utility.Server;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace MrBLL.Domains.VehicleManagement;

public partial class FrmVehicleSetup : MrForm
{
    #region --------------- Global Value ---------------

    private long ProductId;
    private int UOMId;
    private int ModelId;
    private int ColorId;
    private int VehicleNoId;
    private int GroupId;
    private int SubGroupId;
    private readonly int PCompanyId;

    private readonly bool _FrmZoom;

    public string _ProductDesc = string.Empty;
    private string _Query = string.Empty;
    private string _SearchKey = string.Empty;
    private string _ActionTag = string.Empty;
    private string FileExt = string.Empty;
    private string SavefilePath = string.Empty;
    private ClsMasterForm MasterForm;
    private readonly ClsMasterSetup ObjMaster = new();
    private readonly ObjGlobal Gobj = new();

    #endregion --------------- Global Value ---------------

    #region --------------- Form Event ---------------

    public FrmVehicleSetup(bool Zoom)
    {
        InitializeComponent();
        _FrmZoom = Zoom;
        MasterForm = new ClsMasterForm(this, BtnExit);
    }

    private void FrmVehicleSetup_Load(object sender, EventArgs e)
    {
        ControlClear();
        ControlEnable(true, false);
        BtnNew.Focus();
    }

    private void FrmVehicleSetup_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    #endregion --------------- Form Event ---------------

    #region --------------- Button Event ---------------

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _ActionTag = "SAVE";
        Text = $"VEHICLE SETUP {_ActionTag.ToUpper()}";
        ControlClear();
        ControlEnable(false, true);
        TxtDescription.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _ActionTag = "UPDATE";
        Text = $"VEHICLE SETUP {_ActionTag.ToUpper()}";
        ControlClear();
        ControlEnable(false, true);
        TxtDescription.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _ActionTag = "UPDATE";
        Text = $"VEHICLE SETUP {_ActionTag.ToUpper()}";
        ControlClear();
        ControlEnable(false, true);
        TxtDescription.Focus();
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
    }

    private bool IsValidForm(bool IsOk = true)
    {
        if (string.IsNullOrEmpty(TxtDescription.Text))
        {
            MessageBox.Show(@"Description is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDescription.Focus();
            return false;
        }

        if (string.IsNullOrEmpty(TxtShortName.Text))
        {
            MessageBox.Show(@"ShortName is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtShortName.Focus();
            return false;
        }

        if (string.IsNullOrEmpty(TxtColor.Text))
        {
            MessageBox.Show(@"Vehicle Color is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtColor.Focus();
            return false;
        }

        if (string.IsNullOrEmpty(TxtChasisNo.Text))
        {
            MessageBox.Show(@"Vehicle ChasisNo is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtChasisNo.Focus();
            return false;
        }

        if (string.IsNullOrEmpty(TxtEngineNo.Text))
        {
            MessageBox.Show(@"Vehicle EngineNo is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtEngineNo.Focus();
            return false;
        }

        if (string.IsNullOrEmpty(TxtUOM.Text))
        {
            MessageBox.Show(@"Unit is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            TxtUOM.Focus();
            return false;
        }

        if (string.IsNullOrEmpty(TxtGroup.Text))
        {
            MessageBox.Show(@"Group is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            TxtGroup.Focus();
            return false;
        }

        double.TryParse(TxtSalesRate.Text, out var _SalesRate);
        if (_SalesRate == 0)
        {
            MessageBox.Show(@"Sales Rate is Zero..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtSalesRate.Focus();
            return false;
        }

        return IsOk;
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        var Query = string.Empty;
        if (IsValidForm())
        {
            var iCount = SaveVehicleDetails();
            if (iCount > 0)
            {
                if (_ActionTag != "DELETE")
                {
                    Query = " Update AMS.Product set PImage=@PImage Where PName= '" + TxtMergeDesc.Text + "' ";
                    using (var cmd = new SqlCommand(Query, GetConnection.GetSqlConnection()))
                    {
                        if (_ActionTag != "DELETE")
                        {
                            if (PbPicbox.Image != null)
                                cmd.Parameters.Add("@PImage", SqlDbType.VarBinary).Value =
                                    Gobj.GetPic(PbPicbox.Image, PbPicbox);
                            else cmd.Parameters.Add("@PImage", SqlDbType.VarBinary).Value = DBNull.Value;
                        }

                        if (cmd.ExecuteNonQuery() > 0)
                        {
                        }
                    }
                }

                MessageBox.Show($@"DATA {_ActionTag.ToUpper()} SUCESSFULLY..!!", ObjGlobal.Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (_FrmZoom) Close();
                ControlClear();
            }
            else if (MessageBox.Show(@"ERROR ..!! PLEASE CHECK THE VALUE..!!", ObjGlobal.Caption,
                         MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
            {
                SaveVehicleDetails();
            }
        }
        else
        {
            if (MessageBox.Show(@"ERROR ..!! PLEASE CHECK THE VALUE..!!", ObjGlobal.Caption,
                    MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry) SaveVehicleDetails();
        }
    }

    private void BtnClear_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtDescription.Text))
            BtnExit.PerformClick();
        else ControlClear();
    }

    #endregion --------------- Button Event ---------------

    #region --------------- Event ---------------

    private void TxtDescription_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtDescription.Text.Trim()) && TxtDescription.Text.Trim().Length == 0 &&
                TxtDescription.Focused && !string.IsNullOrEmpty(_ActionTag))
            {
                MessageBox.Show(@"DESCRIPTION IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtDescription.Focus();
            }
        }
        else if (e.KeyCode == Keys.F1)
        {
            BtnDescription_Click(sender, e);
        }
        else if (TxtDescription.ReadOnly)
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtDescription, BtnDescription);
        }
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (string.IsNullOrEmpty(TxtDescription.Text.Trim()) && TxtDescription.Text.Trim().Length == 0 &&
            TxtDescription.Focused && !string.IsNullOrEmpty(_ActionTag))
        {
            MessageBox.Show(@"DESCRIPTION IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDescription.Focus();
            return;
        }

        if (_ActionTag == "SAVE")
            TxtShortName.Text = ObjGlobal.BindAutoIncrementCode("PR", TxtDescription.Text.Trim());
    }

    private void TxtDescription_TextChanged(object sender, EventArgs e)
    {
        TxtMergeDesc.Text = TxtDescription.Text.Trim();
        if (!string.IsNullOrEmpty(TxtModel.Text.Trim()))
            TxtMergeDesc.Text = TxtDescription.Text.Trim() + "-" + TxtModel.Text.Trim();
        if (!string.IsNullOrEmpty(TxtChasisNo.Text.Trim()))
            TxtMergeDesc.Text = TxtDescription.Text.Trim() + "-" + TxtModel.Text.Trim() + "-" +
                                TxtChasisNo.Text.Trim();
        if (!string.IsNullOrEmpty(TxtEngineNo.Text.Trim()))
            TxtMergeDesc.Text = TxtDescription.Text.Trim() + "-" + TxtModel.Text.Trim() + "-" +
                                TxtChasisNo.Text.Trim() + "-" + TxtEngineNo.Text.Trim();
        if (!string.IsNullOrEmpty(TxtColor.Text.Trim()))
            TxtMergeDesc.Text = TxtDescription.Text.Trim() + "-" + TxtModel.Text.Trim() + "-" +
                                TxtChasisNo.Text.Trim() + "-" + TxtEngineNo.Text.Trim() + "-" +
                                TxtColor.Text.Trim();
        if (!string.IsNullOrEmpty(TxtVechileNo.Text.Trim()))
            TxtMergeDesc.Text = TxtDescription.Text.Trim() + "-" + TxtModel.Text.Trim() + "-" +
                                TxtChasisNo.Text.Trim() + "-" + TxtEngineNo.Text.Trim() + "-" +
                                TxtColor.Text.Trim() + "-" + TxtVechileNo.Text.Trim();
    }

    private void BtnDescription_Click(object sender, EventArgs e)
    {
        var frmPickList = new FrmAutoPopList("MAX", "VEHICLE", _ActionTag, ObjGlobal.SearchKey, "NORMAL", "MASTER");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
                if (_ActionTag != "NEW")
                {
                    TxtDescription.Text = frmPickList.SelectedList[0]["PAlias"].ToString().Trim();
                    ProductId = Convert.ToInt64(frmPickList.SelectedList[0]["Pid"].ToString().Trim());
                    FillDataFromTable(ProductId);
                }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"Product are not Aviable..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtDescription.Focus();
            return;
        }

        _SearchKey = string.Empty;
        TxtDescription.Focus();
    }

    private void TxtShortName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
            if (string.IsNullOrEmpty(TxtShortName.Text.Trim()) && TxtShortName.Text.Trim().Length == 0 &&
                TxtShortName.Focused && !string.IsNullOrEmpty(_ActionTag))
            {
                MessageBox.Show(@"ShortName is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtShortName.Focus();
            }
    }

    private void TxtShortName_Validating(object sender, CancelEventArgs e)
    {
        if (string.IsNullOrEmpty(TxtDescription.Text.Trim()) && TxtDescription.Text.Trim().Length == 0 &&
            TxtDescription.Focused && !string.IsNullOrEmpty(_ActionTag))
        {
            MessageBox.Show(@"Description is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDescription.Focus();
            return;
        }

        if (!string.IsNullOrEmpty(TxtDescription.Text.Trim()) && !string.IsNullOrEmpty(_ActionTag))
        {
            var ChkData = new DataTable();
            if (_ActionTag == "SAVE")
            {
                ChkData = GetConnection.SelectDataTableQuery("SELECT * FROM AMS.Product p WHERE p.PShortName='" +
                                                             TxtShortName.Text.Trim() + "'");
            }
            else if (_ActionTag == "UPDATE")
            {
            }
        }
    }

    private void TxtModel_Leave(object sender, EventArgs e)
    {
    }

    private void TxtModel_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtModel.Text.Trim()) && TxtModel.Text.Trim().Length == 0 &&
                TxtModel.Focused && !string.IsNullOrEmpty(_ActionTag))
            {
                MessageBox.Show(@"Model is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtModel.Focus();
            }
        }
        else if (e.KeyCode == Keys.F1)
        {
            BtnModel_Click(sender, e);
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtModel, BtnModel);
        }
    }

    private void BtnModel_Click(object sender, EventArgs e)
    {
        var frmPickList = new FrmAutoPopList("MIN", "VEHICLEMODEL", "LIST", _SearchKey, _ActionTag, "NORMAL",
            string.Empty);
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtModel.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                ModelId = Convert.ToInt32(frmPickList.SelectedList[0]["LedgerId"].ToString().Trim());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"Product are not Aviable..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtModel.Focus();
            return;
        }

        _SearchKey = string.Empty;
        TxtModel.Focus();
    }

    private void TxtVechileNo_Leave(object sender, EventArgs e)
    {
    }

    private void TxtVechileNo_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            //if (string.IsNullOrEmpty(TxtModel.Text.Trim()) && TxtModel.Text.Trim().Length == 0 && TxtModel.Focused == true && !string.IsNullOrEmpty(ActionTag))
            //{
            //    MessageBox.Show("Model is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    TxtModel.Focus();
            //    return;
            //}
        }
        else if (e.KeyCode == Keys.F1)
        {
            BtnVehicleNo_Click(sender, e);
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtVechileNo, BtnVehicleNo);
        }
    }

    private void BtnVehicleNo_Click(object sender, EventArgs e)
    {
        using (var frmPickList =
               new FrmAutoPopList("MIN", "VEHICLENUMBER", _SearchKey, _ActionTag, string.Empty, "LIST"))
        {
            if (FrmAutoPopList.GetListTable == null)
            {
                MessageBox.Show(@"Vehicle No Not Found..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                TxtVechileNo.Focus();
                return;
            }

            if (FrmAutoPopList.GetListTable.Rows.Count > 0)
            {
                frmPickList.ShowDialog();
                if (frmPickList.SelectedList.Count > 0)
                {
                    TxtVechileNo.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                    VehicleNoId = Convert.ToInt32(frmPickList.SelectedList[0]["LedgerId"].ToString().Trim());
                }

                frmPickList.Dispose();
            }
            else
            {
                MessageBox.Show(@"Vehicle No Not Found..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                TxtVechileNo.Focus();
                return;
            }
        }

        _SearchKey = string.Empty;
        TxtVechileNo.Focus();
    }

    private void TxtColor_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtColor.Text.Trim()) && TxtColor.Text.Trim().Length == 0 && TxtColor.Focused &&
            !string.IsNullOrEmpty(_ActionTag))
        {
            MessageBox.Show(@"Model is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            TxtModel.Focus();
        }
    }

    private void TxtColor_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtColor.Text.Trim()) && TxtColor.Text.Trim().Length == 0 &&
                TxtColor.Focused && !string.IsNullOrEmpty(_ActionTag))
            {
                MessageBox.Show(@"Color is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtModel.Focus();
            }
        }
        else if (e.KeyCode == Keys.F1)
        {
            BtnColor_Click(sender, e);
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtColor, BtnColor);
        }
    }

    private void BtnColor_Click(object sender, EventArgs e)
    {
        using (var frmPickList =
               new FrmAutoPopList("MIN", "VEHICLECOLOR", _SearchKey, _ActionTag, string.Empty, "LIST"))
        {
            if (FrmAutoPopList.GetListTable.Rows.Count > 0)
            {
                frmPickList.ShowDialog();
                if (frmPickList.SelectedList.Count > 0)
                {
                    TxtColor.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                    ColorId = Convert.ToInt32(frmPickList.SelectedList[0]["LedgerId"].ToString().Trim());
                }

                frmPickList.Dispose();
            }
            else
            {
                MessageBox.Show(@"Vehicle No Not Found..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                TxtColor.Focus();
                return;
            }
        }

        _SearchKey = string.Empty;
        TxtColor.Focus();
    }

    private void TxtChasisNo_Validating(object sender, CancelEventArgs e)
    {
        if (string.IsNullOrEmpty(TxtChasisNo.Text.Trim()) && TxtChasisNo.Text.Trim().Length == 0 &&
            TxtChasisNo.Focused && !string.IsNullOrEmpty(_ActionTag))
        {
            MessageBox.Show(@"Chasis No is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtChasisNo.Focus();
            return;
        }

        if (!string.IsNullOrEmpty(TxtChasisNo.Text.Trim()) && !string.IsNullOrEmpty(_ActionTag))
        {
            var ChkData = new DataTable();
            if (_ActionTag == "SAVE")
            {
                ChkData = GetConnection.SelectDataTableQuery("SELECT * FROM AMS.Product p WHERE p.ChasisNo='" +
                                                             TxtChasisNo.Text.Trim() + "'");
                if (ChkData.Rows.Count > 0)
                {
                    MessageBox.Show(@"Chasis No is Already Exits ..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    TxtChasisNo.SelectAll();
                    TxtChasisNo.Focus();
                }
            }
            else if (_ActionTag == "UPDATE")
            {
                ChkData = GetConnection.SelectDataTableQuery("SELECT * FROM AMS.Product p WHERE p.ChasisNo='" +
                                                             TxtChasisNo.Text.Trim() + "' and PID <> '" +
                                                             ProductId + "'");
                if (ChkData.Rows.Count > 0)
                {
                    MessageBox.Show(@"Chasis No is Already Exits ..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    TxtChasisNo.SelectAll();
                    TxtChasisNo.Focus();
                }
            }
        }
    }

    private void TxtChasisNo_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
            if (string.IsNullOrEmpty(TxtChasisNo.Text.Trim()) && TxtChasisNo.Text.Trim().Length == 0 &&
                TxtChasisNo.Focused && !string.IsNullOrEmpty(_ActionTag))
            {
                MessageBox.Show(@"ChasisNo is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtModel.Focus();
            }
    }

    private void TxtEngineNo_Validating(object sender, CancelEventArgs e)
    {
        if (string.IsNullOrEmpty(TxtEngineNo.Text.Trim()) && TxtEngineNo.Text.Trim().Length == 0 &&
            TxtEngineNo.Focused && !string.IsNullOrEmpty(_ActionTag))
        {
            MessageBox.Show(@"Engine No is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtEngineNo.Focus();
            return;
        }

        if (!string.IsNullOrEmpty(TxtEngineNo.Text.Trim()) && !string.IsNullOrEmpty(_ActionTag))
        {
            var ChkData = new DataTable();
            if (_ActionTag == "SAVE")
            {
                ChkData = GetConnection.SelectDataTableQuery("SELECT * FROM AMS.Product p WHERE p.EngineNo='" +
                                                             TxtEngineNo.Text.Trim() + "'");
                if (ChkData.Rows.Count > 0)
                {
                    MessageBox.Show(@"Engine No is Already Exits ..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    TxtEngineNo.SelectAll();
                    TxtEngineNo.Focus();
                }
            }
            else if (_ActionTag == "UPDATE")
            {
                ChkData = GetConnection.SelectDataTableQuery("SELECT * FROM AMS.Product p WHERE p.EngineNo='" +
                                                             TxtEngineNo.Text.Trim() + "'  and PID<> '" +
                                                             ProductId + "' ");
                if (ChkData.Rows.Count > 0)
                {
                    MessageBox.Show(@"Engine No is Already Exits ..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    TxtEngineNo.SelectAll();
                    TxtEngineNo.Focus();
                }
            }
        }
    }

    private void TxtEngineNo_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
            if (string.IsNullOrEmpty(TxtUOM.Text.Trim()) && TxtUOM.Text.Trim().Length == 0 && TxtUOM.Focused &&
                !string.IsNullOrEmpty(_ActionTag))
            {
                MessageBox.Show(@"Unit is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtUOM.Focus();
            }
    }

    private void TxtUOM_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtUOM.Text.Trim()) && TxtUOM.Text.Trim().Length == 0 && TxtUOM.Focused &&
            !string.IsNullOrEmpty(_ActionTag))
        {
            MessageBox.Show(@"Unit is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            TxtUOM.Focus();
        }
    }

    private void TxtUOM_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnUOM_Click(sender, e);
        }
        else if (e.KeyCode == Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtUOM.Text.Trim()) && TxtUOM.Text.Trim().Length == 0 && TxtUOM.Focused &&
                !string.IsNullOrEmpty(_ActionTag))
            {
                MessageBox.Show(@"Unit is Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtUOM.Focus();
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtUOM, BtnUOM);
        }
    }

    private void BtnUOM_Click(object sender, EventArgs e)
    {
        var frmPickList = new FrmAutoPopList("MIN", "PRODUCTUNIT", "LIST", _SearchKey, _ActionTag, "NORMAL",
            string.Empty);
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtUOM.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                UOMId = Convert.ToInt32(frmPickList.SelectedList[0]["Id"].ToString().Trim());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"UOM are not Aviable..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtUOM.Focus();
            return;
        }

        _SearchKey = string.Empty;
        TxtUOM.Focus();
    }

    private void TxtCompany_Leave(object sender, EventArgs e)
    {
    }

    private void TxtCompany_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnCompany_Click(sender, e);
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var frm = new FrmDepartmentSetup(true, "COMPANY SETUP");
            frm.ShowDialog();
            TxtCompany.Text = frm.DepartmentDesc;
            TxtCompany.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtCompany, BtnCompany);
        }
    }

    private void BtnCompany_Click(object sender, EventArgs e)
    {
    }

    private void TxtGroup_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtGroup.Text.Trim()) && TxtGroup.Text.Trim().Length == 0 && TxtGroup.Focused &&
            !string.IsNullOrEmpty(_ActionTag))
        {
            MessageBox.Show(@"PRODUCT GROUP IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtModel.Focus();
        }
    }

    private void TxtGroup_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnGroup_Click(sender, e);
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var frm = new FrmProductGroup();
            frm.ShowDialog();
            TxtGroup.Text = frm.ProductGroupDesc;
            TxtGroup.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtGroup, BtnGroup);
        }
    }

    private void BtnGroup_Click(object sender, EventArgs e)
    {
        var frmPickList = new FrmAutoPopList("MED", "PRODUCTGROUP", "LIST", _SearchKey, _ActionTag, "NORMAL",
            string.Empty);
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtGroup.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                GroupId = Convert.ToInt32(frmPickList.SelectedList[0]["Id"].ToString().Trim());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"Group are not Aviable..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtGroup.Focus();
            return;
        }

        _SearchKey = string.Empty;
        TxtGroup.Focus();
    }

    private void TxtSubGroup_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnSubGroup_Click(sender, e);
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var frm = new FrmProductSubGroup();
            frm.ShowDialog();
            TxtSubGroup.Text = frm.ProductSubGroupName;
            TxtSubGroup.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtSubGroup, BtnSubGroup);
        }
    }

    private void BtnSubGroup_Click(object sender, EventArgs e)
    {
        var frmPickList = new FrmAutoPopList("MIN", "PRODUCTSUBGROUP", "LIST", _SearchKey, _ActionTag,
            GroupId.ToString(), string.Empty);
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtSubGroup.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                SubGroupId = Convert.ToInt32(frmPickList.SelectedList[0]["Id"].ToString().Trim());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"SubGroup are not Aviable..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtSubGroup.Focus();
            return;
        }

        _SearchKey = string.Empty;
        TxtSubGroup.Focus();
    }

    private void TxtBuyRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !(sender as TextBox).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void TxtMargin_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !(sender as TextBox).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void TxtMRP_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !(sender as TextBox).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void TxtMargin1_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !(sender as TextBox).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void TxtTradeRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !(sender as TextBox).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void TxtSalesRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !(sender as TextBox).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void lblProductPic_DoubleClick(object sender, EventArgs e)
    {
        PbPicbox_DoubleClick(sender, e);
    }

    private void PbPicbox_DoubleClick(object sender, EventArgs e)
    {
        var IsFileExists = string.Empty;
        try
        {
            var FileName = string.Empty;
            var dlg = new OpenFileDialog
            {
                Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp"
            };
            dlg.ShowDialog();
            FileName = dlg.FileName;
            IsFileExists = dlg.FileName;
            PbPicbox.ImageLocation = FileName;
            PbPicbox.Load(FileName);
            lblProductPic.Visible = false;
            lnk_PreviewImage.Visible = true;
            lbl_ImageAttachment.Text = Path.GetFileName(FileName);
        }
        catch (Exception ex)
        {
            if (IsFileExists != string.Empty)
            {
                MessageBox.Show("Picture File Format & " + ex.Message);
            }
            else
            {
                lblProductPic.Visible = true;
                lnk_PreviewImage.Visible = false;
            }
        }
    }

    private void lnk_PreviewImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        if (PbPicbox.Image != null)
        {
            FileExt = Path.GetExtension(PbPicbox.ImageLocation);
            if (FileExt == ".JPEG" || FileExt == ".jpg" || FileExt == ".Bitmap" ||
                FileExt == ".png") //&& this.Tag == "NEW")
            {
                ObjGlobal.FetchPic(PbPicbox, string.Empty);
            }
            else
            {
                SavefilePath = PbPicbox.ImageLocation;
                Process.Start(SavefilePath);
            }
        }
    }

    private void TxtVechileNo_TextChanged(object sender, EventArgs e)
    {
        TxtDescription_TextChanged(sender, e);
    }

    private void TxtModel_TextChanged(object sender, EventArgs e)
    {
        TxtDescription_TextChanged(sender, e);
    }

    private void TxtColor_TextChanged(object sender, EventArgs e)
    {
        TxtDescription_TextChanged(sender, e);
    }

    private void TxtChasisNo_TextChanged(object sender, EventArgs e)
    {
        TxtDescription_TextChanged(sender, e);
    }

    private void TxtEngineNo_TextChanged(object sender, EventArgs e)
    {
        TxtDescription_TextChanged(sender, e);
    }

    #endregion --------------- Event ---------------

    #region --------------- Method ---------------

    private void ControlClear()
    {
        ModelId = 0;
        TxtDescription.Clear();
        TxtDescription.ReadOnly = string.IsNullOrEmpty(_ActionTag) || _ActionTag != "SAVE";
        TxtShortName.Clear();
        TxtVechileNo.Clear();
        TxtChasisNo.Clear();
        TxtEngineNo.Clear();
        TxtGroup.Clear();
        TxtSubGroup.Clear();
        TxtColor.Clear();
        TxtCompany.Clear();
        TxtTradeRate.Clear();
        TxtSalesRate.Clear();
        TxtShortName.Clear();
        TxtMargin.Clear();
        TxtMargin1.Clear();
        TxtModel.Clear();
        TxtMRP.Clear();
        CmbVehicleType.SelectedIndex = 0;
        CmbCategory.SelectedIndex = 0;
    }

    private void ControlEnable(bool Btn, bool Txt)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = Btn;
        TxtDescription.Enabled = BtnDescription.Enabled = !string.IsNullOrEmpty(_ActionTag) && _ActionTag != "SAVE" || Txt;
        TxtShortName.Enabled = Txt;
        TxtModel.Enabled = Txt;
        TxtColor.Enabled = Txt;
        TxtVechileNo.Enabled = Txt;
        TxtChasisNo.Enabled = Txt;
        TxtEngineNo.Enabled = Txt;
        CmbCategory.Enabled = Txt;
        TxtUOM.Enabled = BtnUOM.Enabled = Txt;
        TxtGroup.Enabled = BtnGroup.Enabled = Txt;
        TxtSubGroup.Enabled = BtnSubGroup.Enabled = Txt;
        TxtBuyRate.Enabled = TxtMargin.Enabled =
            TxtMargin1.Enabled = TxtMRP.Enabled = TxtTradeRate.Enabled = TxtSalesRate.Enabled = Txt;
        TxtMergeDesc.Enabled = false;
        ChkActive.Enabled = !string.IsNullOrEmpty(_ActionTag) && _ActionTag == "UPDATE" ? true : false;
        BtnSave.Enabled =
            BtnCancel.Enabled = !string.IsNullOrEmpty(_ActionTag) && _ActionTag != "SAVE" || Txt;
    }

    private int SaveVehicleDetails()
    {
        //ObjMaster.ObjMaster._ActionTag = _ActionTag;
        //ObjMaster.ObjMaster.MasterId = ProductId;

        //ObjMaster.ObjMaster.TxtAliasDescription = TxtMergeDesc.Text.Trim();
        //ObjMaster.ObjMaster.TxtDescription = TxtDescription.Text.Trim();
        //ObjMaster.ObjMaster.TxtShortName = TxtShortName.Text.Trim();
        //ObjMaster.ObjMaster.TxtType = CmbVehicleType.Text.Trim();
        //ObjMaster.ObjMaster.TxtCategory = CmbCategory.Text.Trim();

        //int.TryParse(UOMId.ToString(), out var _UOMId);
        //ObjMaster.ObjMaster.TxtUnitId = _UOMId;
        //ObjMaster.ObjMaster.TxtValMethod = "FIFO";
        //double.TryParse(TxtBuyRate.Text, out var _BuyRate);
        //ObjMaster.ObjMaster.TxtBuyRate = _BuyRate;
        //ObjMaster.ObjMaster.TxtBeforeVatPurchase = _BuyRate;
        //double.TryParse(TxtSalesRate.Text, out var _SalesRate);
        //ObjMaster.ObjMaster.TxtSalesRate = _SalesRate;
        //ObjMaster.ObjMaster.TxtBeforeVatSales = _SalesRate;
        //double.TryParse(TxtMargin.Text, out var _Margin);
        //ObjMaster.ObjMaster.TxtMargin = _Margin;
        //double.TryParse(TxtTradeRate.Text, out var _TradeRate);
        //ObjMaster.ObjMaster.TxtTradeRate = _TradeRate;
        //double.TryParse(TxtMargin1.Text, out var _Margin1);
        //ObjMaster.ObjMaster.TxtMargin1 = _Margin1;
        //double.TryParse(TxtMRP.Text, out var _MRP);
        //ObjMaster.ObjMaster.TxtMRP = _MRP;
        //int.TryParse(GroupId.ToString(), out var _GroupId);
        //ObjMaster.ObjMaster.TxtGroupId = _GroupId;
        //int.TryParse(SubGroupId.ToString(), out var _SubGroupId);
        //ObjMaster.ObjMaster.TxtSubGroupId = _SubGroupId;
        //ObjMaster.ObjMaster.TxtTaxRate = 13;

        //int.TryParse(PCompanyId.ToString(), out var _PCompanyId);
        //ObjMaster.ObjMaster.TxtCompanyId = _PCompanyId;

        //ObjMaster.ObjMaster.TxtBranchId = Convert.ToInt32(ObjGlobal.SysBranchId);

        //int.TryParse(ModelId.ToString(), out var _ModelId);
        //ObjMaster.ObjMaster.TxtModelId = _ModelId;

        //int.TryParse(VehicleNoId.ToString(), out var _VehicleId);
        //ObjMaster.ObjMaster.TxtVehicleNoId = _VehicleId;
        //int.TryParse(ColorId.ToString(), out var _ColorId);
        //ObjMaster.ObjMaster.TxtColorId = _ColorId;
        //ObjMaster.ObjMaster.TxtChassisNo = TxtChasisNo.Text.Trim();
        //ObjMaster.ObjMaster.TxtEngineNo = TxtEngineNo.Text.Trim();
        //ObjMaster.ObjMaster.ChkActive = ChkActive.Checked;
        //ObjMaster.ObjMaster.TxtEnterBy = ObjGlobal.LogInUser;
        return 0;
    }

    private void FillDataFromTable(long selectedId)
    {
        var dt = new DataTable();
        dt = ObjMaster.GetMasterVehicle(_ActionTag, selectedId);
        if (dt.Rows.Count > 0)
        {
            //PID, PName, PAlias, PShortName, PType, PCategory, PUnit, PAltUnit, PQtyConv, PAltConv, PValTech, PSerialno, PSizewise, PBatchwise,
            //PBuyRate, PSalesRate, PMargin1, TradeRate, PMargin2, PMRP, PGrpId, PSubGrpId, PTax, PMin, PMax, CmpId, CmpId1, CmpId2, CmpId3, Branch_Id, CmpUnit_Id, PPL, PPR, PSL, PSR, PL_Opening, PL_Closing, BS_Closing, PImage, EnterBy, EnterDate, Status, BeforeBuyRate, BeforeSalesRate, Barcode, ChasisNo, EngineNo, VHColor, VHModel, VHNumber
            TxtDescription.Text = dt.Rows[0]["PAlias"].ToString();
            TxtMergeDesc.Text = dt.Rows[0]["PName"].ToString();
            TxtShortName.Text = dt.Rows[0]["PShortName"].ToString();
            TxtModel.Text = dt.Rows[0]["VHModelDesc"].ToString();
            TxtVechileNo.Text = dt.Rows[0]["VNDesc"].ToString();
            TxtColor.Text = dt.Rows[0]["VHColorsDesc"].ToString();
            TxtChasisNo.Text = dt.Rows[0]["ChasisNo"].ToString();
            TxtEngineNo.Text = dt.Rows[0]["EngineNo"].ToString();
            CmbVehicleType.Text = dt.Rows[0]["PType"].ToString();
            CmbCategory.Text = dt.Rows[0]["PCategory"].ToString();
            TxtUOM.Text = dt.Rows[0]["UnitCode"].ToString();
            TxtCompany.Text = dt.Rows[0]["GrpName"].ToString();
            TxtGroup.Text = dt.Rows[0]["GrpName"].ToString();
            TxtSubGroup.Text = dt.Rows[0]["SubGrpName"].ToString();
            double.TryParse(dt.Rows[0]["PBuyRate"].ToString(), out var _BuyRate);
            TxtBuyRate.Text = Convert.ToDecimal(_BuyRate).ToString(ObjGlobal.SysAmountFormat);
            double.TryParse(dt.Rows[0]["PMargin1"].ToString(), out var _Margin);
            TxtMargin.Text = Convert.ToDecimal(_Margin).ToString(ObjGlobal.SysAmountFormat);
            double.TryParse(dt.Rows[0]["PMRP"].ToString(), out var _MRP);
            TxtMRP.Text = Convert.ToDecimal(_MRP).ToString(ObjGlobal.SysAmountFormat);
            double.TryParse(dt.Rows[0]["PMargin2"].ToString(), out var _Margin1);
            TxtMargin1.Text = Convert.ToDecimal(_Margin1).ToString(ObjGlobal.SysAmountFormat);
            double.TryParse(dt.Rows[0]["TradeRate"].ToString(), out var _TradeRate);
            TxtTradeRate.Text = Convert.ToDecimal(_TradeRate).ToString(ObjGlobal.SysAmountFormat);
            double.TryParse(dt.Rows[0]["PSalesRate"].ToString(), out var _SalesRate);
            TxtSalesRate.Text = Convert.ToDecimal(_SalesRate).ToString(ObjGlobal.SysAmountFormat);
            bool.TryParse(dt.Rows[0]["Status"].ToString(), out var _ChkActive);
            ChkActive.Checked = _ChkActive;
        }
    }

    #endregion --------------- Method ---------------

    private void TxtMargin_TextChanged(object sender, EventArgs e)
    {

    }
}