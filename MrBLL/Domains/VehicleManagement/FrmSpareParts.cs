using MrBLL.Master.ProductSetup;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using MrDAL.Master;
using MrDAL.Master.Interface;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MrBLL.Domains.VehicleManagement;

public partial class FrmSpareParts : MrForm
{
    private string _ActionTag = string.Empty;

    private readonly bool _IsZoom = false;
    public string Desc = string.Empty;

    private readonly IMasterSetup ObjSetup = new ClsMasterSetup();

    private int PUnitId;

    public long ProductId { get; set; }

    private void GlobalKeyEvents_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void TxtDescription_Leave(object sender, EventArgs e)
    {
        if (ActiveControl != null && string.IsNullOrEmpty(TxtDescription.Text.Trim()) && TxtDescription.Focused &&
            !string.IsNullOrEmpty(_ActionTag))
        {
            MessageBox.Show(@"DESCRIPTION IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDescription.Focus();
        }
    }

    private void BtnDescription_Click(object sender, EventArgs e)
    {
        using var frmPickList = new FrmAutoPopList("MAX", "MASTERPRODUCT", _ActionTag, ObjGlobal.SearchText, "NORMAL", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                if (_ActionTag != "SAVE")
                {
                    TxtDescription.Text = frmPickList.SelectedList[0]["PName"].ToString().Trim();
                    ProductId = frmPickList.SelectedList[0]["Pid"].GetLong();
                }
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"PRODUCT ARE NOT AVAILABLE.!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            TxtDescription.Focus();
            return;
        }
        ObjGlobal.SearchText = string.Empty;
        TxtDescription.Focus();
    }

    private void TxtShortName_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtShortName.Text.Trim()) && TxtShortName.Focused && !string.IsNullOrEmpty(_ActionTag))
        {
            MessageBox.Show(@"SHORT NAME IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            TxtShortName.Focus();
            return;
        }
    }

    private void TxtShortName_Validating(object sender, CancelEventArgs e)
    {
        using var dt = ObjSetup.CheckIsValidData(_ActionTag, "Product", "PShortName", "PID", TxtShortName.Text, ProductId.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            MessageBox.Show(@"SHORT NAME ALREADY EXITS..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            TxtShortName.Focus();
        }
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        TxtShortName.Text = string.IsNullOrEmpty(TxtDescription.Text.Trim()) switch
        {
            false when _ActionTag is "SAVE" => ObjGlobal.BindAutoIncrementCode("PR", TxtDescription.Text),
            _ => TxtShortName.Text
        };
        if (!string.IsNullOrEmpty(_ActionTag) && _ActionTag != "DELETE")
        {
            using var dtDesc = ObjSetup.CheckIsValidData(_ActionTag, "Product", "PName", "PID", TxtDescription.Text.Trim(), ProductId.ToString());
            if (dtDesc != null && dtDesc.Rows.Count > 0)
            {
                MessageBox.Show(@"DESCRIPTION IS ALREADY EXITS..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
                TxtDescription.Focus();
            }
        }
    }

    private void TxtModule_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtModule.Text.Trim()) && TxtModule.Focused && !string.IsNullOrEmpty(_ActionTag))
        {
            MessageBox.Show(@"MODULE IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            TxtModule.Focus();
        }
    }

    private void TxtUnit_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control is true && e.KeyCode is Keys.N)
        {
            using var frm = new FrmProductUnit(true);
            frm.ShowDialog();
            PUnitId = frm.ProductUnitId;
            frm.Dispose();
        }
        else if (e.KeyCode is Keys.Enter)
        {
        }
    }

    private void lblProductPic_DoubleClick(object sender, EventArgs e)
    {
        PbPicbox_DoubleClick(sender, e);
    }

    private void PbPicbox_DoubleClick_1(object sender, EventArgs e)
    {
        var IsFileExists = string.Empty;
        var FileName = string.Empty;
        try
        {
            using var dlg = new OpenFileDialog
            {
                Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp"
            };
            dlg.ShowDialog();
            FileName = dlg.FileName;
            IsFileExists = dlg.FileName;
            PbPicbox.ImageLocation = FileName;
            PbPicbox.Load(FileName);
            lblProductPic.Visible = false;
            // lnk_PreviewImage.Visible = true;
            // lbl_ImageAttachment.Text = Path.GetFileName(FileName);
        }
        catch (Exception ex)
        {
            if (!string.IsNullOrEmpty(IsFileExists))
                MessageBox.Show("Picture File Format & " + ex.Message);
            else
                lblProductPic.Visible = true;
            //   lnk_PreviewImage.Visible = false;
        }
    }

    private void TxtCategory_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtCategory.Text.Trim()) && TxtCategory.Focused is true &&
            !string.IsNullOrEmpty(_ActionTag))
        {
            MessageBox.Show(@"CATEGORY IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtCategory.Focus();
        }
    }

    private void TxtRack_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode is Keys.N)
        {
            using var frm = new FrmRack(true);
            frm.ShowDialog();
            PUnitId = frm.RackId;
            frm.Dispose();
        }
        else if (e.KeyCode is Keys.Enter)
        {
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtRack, BtnRack);
        }
    }

    #region --------------- FrmSpareParts ---------------

    public FrmSpareParts()
    {
        InitializeComponent();
    }

    private void FrmSpareParts_Load(object sender, EventArgs e)
    {
        ClearControl();
        EnableControl(false, true);
        CmbVat.SelectedIndex = 0;
        BtnNew.Focus();
    }

    private void FrmSpareParts_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (BtnNew.Enabled is false || BtnEdit.Enabled is false || BtnDelete.Enabled is false)
            {
                if (MessageBox.Show(@"ARE YOU SURE WANT TO CLEAR FORM..??", ObjGlobal.Caption,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _ActionTag = string.Empty;
                    ClearControl();
                    EnableControl(false, true);
                    BtnNew.Focus();
                }
            }
            else
            {
                if (MessageBox.Show(@"ARE YOU SURE WANT TO CLEAR FORM..??", ObjGlobal.Caption,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Close();
            }
        }
    }

    #endregion --------------- FrmSpareParts ---------------

    #region -------------- METHOD --------------

    private void ClearControl()
    {
        Text = !string.IsNullOrEmpty(_ActionTag) ? $"SPARE PARTS SETUP [{_ActionTag}] " : "SPARE PARTS SETUP";
        TxtBarcode.Text = TxtBarcode.Text = TxtDescription.Text = TxtDepartment.Text = TxtRack.Text =
            TxtProductGroup.Text =
                TxtAltQty.Text = TxtCategory.Text = TxtShortName.Text = TxtBuyRate.Text = TxtMargin.Text =
                    TxtSalesRate.Text =
                        TxtMRP.Text = string.Empty;
        CmbVat.SelectedIndex = 0;
    }

    private void EnableControl(bool Txt, bool Btn)
    {
        BtnDelete.Enabled = BtnEdit.Enabled = BtnNew.Enabled = BtnExit.Enabled = Btn;
        TxtDescription.Enabled = BtnDescription.Enabled =
            !string.IsNullOrEmpty(_ActionTag) && _ActionTag != "SAVE" ? true : Txt;
    }

    private bool FormValidate()
    {
        if (string.IsNullOrEmpty(_ActionTag) || string.IsNullOrEmpty(TxtDescription.Text) ||
            string.IsNullOrEmpty(TxtShortName.Text)) return false;

        if (string.IsNullOrEmpty(TxtModule.Text))
        {
            MessageBox.Show(@"MODULE IS REQUIRED..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtModule.Focus();
            return false;
        }

        if (string.IsNullOrEmpty(TxtRack.Text))
        {
            MessageBox.Show(@"RACK IS REQUIRED..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtRack.Focus();
            return false;
        }

        if (string.IsNullOrEmpty(TxtDescription.Text))
        {
            MessageBox.Show(@"DESCRIPTION IS REQUIRED..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDescription.Focus();
            return false;
        }

        if (string.IsNullOrEmpty(TxtCategory.Text))
        {
            MessageBox.Show(@"CATEGORY IS REQUIRED..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDescription.Focus();
            return false;
        }

        return true;
    }

    private int I_U_D_SPARE_PARTS()
    {
        return 1;
    }

    private void PbPicbox_DoubleClick(object sender, EventArgs e)
    {
        var IsFileExists = string.Empty;
        var FileName = string.Empty;
        try
        {
            using var dlg = new OpenFileDialog
            {
                Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp"
            };
            dlg.ShowDialog();
            FileName = dlg.FileName;
            IsFileExists = dlg.FileName;
            PbPicbox.ImageLocation = FileName;
            PbPicbox.Load(FileName);
            lblProductPic.Visible = false;
            // lnk_PreviewImage.Visible = true;
            // lbl_ImageAttachment.Text = Path.GetFileName(FileName);
        }
        catch (Exception ex)
        {
            if (!string.IsNullOrEmpty(IsFileExists))
                MessageBox.Show("Picture File Format & " + ex.Message);
            else
                lblProductPic.Visible = true;
            // lnk_PreviewImage.Visible = false;
        }
    }

    #endregion -------------- METHOD --------------

    #region -------------- BUTTONS --------------

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _ActionTag = "SAVE";
        ClearControl();
        EnableControl(true, false);
        TxtDescription.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _ActionTag = "UPDATE";
        ClearControl();
        EnableControl(true, false);
        TxtDescription.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _ActionTag = "DELETE";
        ClearControl();
        EnableControl(false, false);
        TxtDescription.Focus();
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (FormValidate())
        {
            if (I_U_D_SPARE_PARTS() > 0)
            {
                if (_IsZoom)
                {
                    Desc = TxtDescription.Text;
                    Close();
                }

                MessageBox.Show($@"DATA {_ActionTag.ToUpper()}  SUCCESSFULLY..!!", ObjGlobal.Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControl();
                TxtDescription.Focus();
            }
        }
        else
        {
            MessageBox.Show($@"ERROR OCCUR WHILE {_ActionTag.ToUpper()} DATA..!!", ObjGlobal.Caption,
                MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            ClearControl();
            TxtDescription.Focus();
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
    }

    #endregion -------------- BUTTONS --------------
}