using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.GridControl;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Setup.CompanySetup;
using MrDAL.Setup.Interface;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DatabaseModule.Setup.CompanyMaster;

namespace MrBLL.Setup.CompanySetup;

public partial class FrmCompanyRights : MrForm
{
    public FrmCompanyRights()
    {
        InitializeComponent();
        _companySetup = new CompanySetupRepository();

        BindCompany();
        GetGridColumns();
        AdjustControlsInDataGrid();
        EnableControl(false);
        ClearCompanyDetails();
        ObjGlobal.DGridColorCombo(SGrid);
    }
    private void CompanyRights_Load(object sender, EventArgs e)
    {
        TxtUserInfo.Focus();
    }
    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (!IsFormValid())
        {
            return;
        }

        var result = SaveCompanyUserRights("SAVE");
        if (result != 0)
        {
            CustomMessageBox.Information("COMPANY RIGHTS INFORMATION IS SAVE SUCCESSFULLY");
            ClearCompanyDetails();
            TxtUserInfo.Focus();
            return;
        }
        else
        {
            CustomMessageBox.Warning("ERROR OCCURS WHILE COMPANY RIGHTS SAVE..");
            SGrid.Focus();
            return;
        }
    }
    private void BtnUser_Click(object sender, EventArgs e)
    {
        OpenLoginUserList();
    }
    private void TxtUserInfo_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnUser_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            SGrid.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtCompanyName, OpenCompanyList);
        }
    }
    private void FrmCompanyRights_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (TxtCompanyName.Enabled)
            {
                ClearCompanyDetails();
                EnableControl(false);
                SGrid.Focus();
            }
            else
            {
                var result = CustomMessageBox.ExitActiveForm();
                if (result == DialogResult.Yes)
                {
                    Close();
                }
            }
        }
    }

    //DATA GRID CONTROL
    private void DGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Delete && SGrid.RowCount > 0)
        {
            if (CustomMessageBox.DeleteRow() is DialogResult.No)
            {
                return;
            }
            _isRowDelete = true;
            if (SGrid.CurrentRow is { Index: >= 0 } && SGrid.Rows.Count > SGrid.CurrentRow.Index)
            {
                SGrid.Rows.RemoveAt(SGrid.CurrentRow.Index);
            }
            if (SGrid.RowCount is 0)
            {
                SGrid.Rows.Add();
            }
            GetSerialNo();
        }

        if (e.KeyCode is Keys.Enter && !TxtCompanyName.Enabled)
        {
            e.SuppressKeyPress = true;
            AdjustControlsInDataGrid();
            if (SGrid.Rows[_rowIndex].Cells["GTxtCompanyName"].Value.IsValueExits())
            {
                TextFromGrid();
                TxtCompanyName.Focus();
                return;
            }
            EnableControl(true);
            GetSerialNo();
            TxtCompanyName.Focus();
        }
    }
    private void DGrid_EnterKeyPressed(object sender, EventArgs e)
    {
        DGrid_KeyDown(sender, new KeyEventArgs(Keys.Enter));
    }
    private void DGrid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
    {
        if (CustomMessageBox.DeleteRow() is DialogResult.No)
        {
            return;
        }
        _isRowDelete = true;
    }
    private void DGrid_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
        if (!_isRowDelete)
        {
            return;
        }
        if (SGrid.RowCount is 0)
        {
            SGrid.Rows.Add();
        }
        GetSerialNo();
    }
    private void OnDGridOnCellEnter(object _, DataGridViewCellEventArgs e)
    {
        _columnIndex = e.ColumnIndex;
    }
    private void OnDGridOnGotFocus(object sender, EventArgs e)
    {
        SGrid.Rows[_rowIndex].Cells[0].Selected = true;
    }
    private void OnDGridOnRowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
    {
        if (SGrid.CurrentCell != null)
        {
            _rowIndex = SGrid.CurrentCell.RowIndex.GetInt();
        }
    }
    private void OnDGridOnRowEnter(object _, DataGridViewCellEventArgs e)
    {
        if (TxtCompanyName.Enabled)
        {
            return;
        }
        _rowIndex = e.RowIndex;
        AdjustControlsInDataGrid();
    }



    // METHOD FOR THIS FORM
    private void EnableControl(bool isEnable)
    {
        TxtSno.Enabled = false;
        TxtSno.Visible = isEnable;

        TxtFileName.Enabled = isEnable;
        TxtFileName.Visible = isEnable;

        TxtCompanyName.Enabled = isEnable;
        TxtCompanyName.Visible = isEnable;
    }
    public void BindCompany()
    {
        try
        {
            var dt = _companySetup.GetCompanyRightList(_userId);
            if (dt.Rows.Count <= 0)
            {
                SGrid.Rows.Add();
                return;
            }

            SGrid.Rows.Clear();
            SGrid.Rows.Add(dt.Rows.Count + 1);
            foreach (DataRow row in dt.Rows)
            {
                SGrid.Rows[dt.Rows.IndexOf(row)].Cells[0].Value = row["CompanyId"];
                SGrid.Rows[dt.Rows.IndexOf(row)].Cells[1].Value = dt.Rows.IndexOf(row) + 1;
                SGrid.Rows[dt.Rows.IndexOf(row)].Cells[2].Value = row["FileName"];
                SGrid.Rows[dt.Rows.IndexOf(row)].Cells[2].Value = row["CompanyName"];
            }
        }
        catch (Exception ex)
        {
            var exception = ex;
            //MessageBox.Show(ex.Message, ObjGlobal.Caption);
        }
    }
    private void AdjustControlsInDataGrid()
    {
        if (SGrid.CurrentRow == null)
        {
            return;
        }
        var currentRow = _rowIndex;
        var columnIndex = SGrid.Columns["GTxtSNo"].Index;
        TxtSno.Size = SGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtSno.Location = SGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtSno.TabIndex = columnIndex;

        columnIndex = SGrid.Columns["GTxtFileName"].Index;
        TxtFileName.Size = SGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtFileName.Location = SGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtFileName.TabIndex = columnIndex;

        columnIndex = SGrid.Columns["GTxtCompanyName"].Index;
        TxtCompanyName.Size = SGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtCompanyName.Location = SGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtCompanyName.TabIndex = columnIndex;

    }
    private void GetGridColumns()
    {

        SGrid.RowEnter += OnDGridOnRowEnter;
        SGrid.RowsAdded += OnDGridOnRowsAdded;
        SGrid.GotFocus += OnDGridOnGotFocus;
        SGrid.CellEnter += OnDGridOnCellEnter;
        SGrid.KeyDown += DGrid_KeyDown;
        SGrid.UserDeletingRow += DGrid_UserDeletingRow;
        SGrid.EnterKeyPressed += DGrid_EnterKeyPressed;
        SGrid.UserDeletedRow += DGrid_UserDeletedRow;
        TxtSno = new MrGridNumericTextBox(SGrid)
        {
            ReadOnly = true
        };
        TxtFileName = new MrGridTextBox(SGrid)
        {
            ReadOnly = true,
            TextAlign = HorizontalAlignment.Center
        };
        TxtCompanyName = new MrGridTextBox(SGrid)
        {
            ReadOnly = true
        };
        TxtCompanyName.KeyDown += (sender, e) =>
        {
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
                TxtFileName.Focus();
                SendToBack();
                return;
            }
            else if (e.KeyCode == Keys.F1)
            {
                OpenCompanyList();
            }
            else if (e.Shift && e.KeyCode is Keys.Tab)
            {
                SendToBack();
            }
            else
            {
                ClsKeyPreview.KeyEvent(e, "DELETE", TxtCompanyName, OpenCompanyList);
            }
        };
        TxtCompanyName.Validating += (sender, e) =>
        {
            if (!TxtCompanyName.Enabled || ActiveControl == TxtFileName)
            {
                return;
            }


            if (!AddTextToGrid(_isRowUpdate))
            {
                return;
            }
            TxtCompanyName.Focus();
        };

    }
    private void OpenCompanyList()
    {
        var (description, id) = GetMasterList.GetCompanyList("NEW");
        if (id > 0)
        {
            var dt = _companySetup.GetCompanyInfo(id);
            CompanyId = id;
            TxtCompanyName.Text = description;
            TxtFileName.Text = dt.Rows[0]["FileName"].GetString();
        }

        TxtCompanyName.Focus();
    }
    private void OpenLoginUserList()
    {
        var (description, id) = GetMasterList.GetUserInfoList("NEW");
        if (id > 0)
        {
            _userId = id;
            TxtUserInfo.Text = description;
        }

        TxtUserInfo.Focus();
    }
    private void GetSerialNo()
    {
        for (var i = 0; i < SGrid.RowCount; i++)
        {
            var sno = SGrid.Rows[i].Cells["GTxtSNo"].Value.GetInt();
            TxtSno.Text = (sno + 1).GetIntString();
        }
    }
    private void TextFromGrid()
    {
        if (SGrid.CurrentRow == null)
        {
            return;
        }

        TxtSno.Text = SGrid.Rows[_rowIndex].Cells["GTxtSNo"].Value.GetIntString();
        TxtFileName.Text = SGrid.Rows[_rowIndex].Cells["GTxtFileName"].Value.GetString();
        TxtCompanyName.Text = SGrid.Rows[_rowIndex].Cells["GTxtCompanyName"].Value.GetString();
        _isRowUpdate = true;
    }
    private bool AddTextToGrid(bool isUpdate)
    {
        if (TxtCompanyName.Text.IsBlankOrEmpty())
        {
            TxtCompanyName.WarningMessage("COMPANY NAME IS BLANK....");
            return false;
        }


        var item = SGrid.Rows.Cast<DataGridViewRow>()
            .FirstOrDefault(r => r.Cells["GTxtCompanyName"].Value.GetString() == TxtCompanyName.Text);
        var index = item?.Index ?? 0;

        if (index == 0)
        {
            var iRows = _rowIndex;
            if (!isUpdate)
            {
                SGrid.Rows.Add();
                SGrid.Rows[iRows].Cells["GTxtSNo"].Value = TxtSno.Text;
            }

            SGrid.Rows[iRows].Cells["GTxtCompanyId"].Value = CompanyId;
            SGrid.Rows[iRows].Cells["GTxtFileName"].Value = TxtFileName.Text;
            SGrid.Rows[iRows].Cells["GTxtCompanyName"].Value = TxtCompanyName.Text;

            _rowIndex = SGrid.RowCount - 1 > iRows ? iRows + 1 : SGrid.RowCount - 1;
            SGrid.CurrentCell = SGrid.Rows[_rowIndex].Cells[_columnIndex];
            if (_isRowUpdate)
            {
                EnableControl(false);
                ClearCompanyDetails();
                SGrid.Focus();
                return false;
            }

            AdjustControlsInDataGrid();
            ClearCompanyDetails();
            TxtCompanyName.AcceptsTab = false;
            GetSerialNo();
        }
        else
        {
            TxtCompanyName.WarningMessage("ALREADY SELECTED THIS COMPANY PLEASE CHOOSE NEXT ONE...");
            return false;
        }

        return true;
    }

    private void ClearCompanyDetails()
    {
        CompanyId = 0;
        TxtCompanyName.Clear();
        TxtFileName.Clear();
    }

    private bool IsFormValid()
    {
        if (_userId == 0 || TxtUserInfo.IsBlankOrEmpty())
        {
            TxtUserInfo.WarningMessage("PLEASE SELECT THE USER FOR RIGHTS");
            return false;
        }

        if (SGrid.RowCount == 0 || SGrid.Rows[0].Cells[0].Value.GetInt() == 0)
        {
            CustomMessageBox.Warning("PLEASE SELECT COMPANY FOR THE USER");
            SGrid.Focus();
            return false;
        }
        return true;
    }

    private int SaveCompanyUserRights(string action)
    {

        foreach (DataGridViewRow gridRow in SGrid.Rows)
        {
            var details = new CompanyRights
            {
                CompanyRights_Id = _companySetup.MaxCompanyRightsId(),
                User_Id = _userId,
                Company_Id = gridRow.Cells[""].Value.GetInt(),
                Company_Name = gridRow.Cells[""].Value.GetString(),
                Start_AdDate = gridRow.Cells[""].Value.GetDateTime(),
                End_AdDate = gridRow.Cells[""].Value.GetDateTime(),
                Modify_Start_AdDate = null,
                Modify_End_AdDate = null,
                Back_Days_Restriction = false
            };
            _companySetup.RightsList.Add(details);
        }
        return _companySetup.SaveCompanyRights(action);
    }

    // OBJECT FOR THIS FORM
    private int _rowIndex;
    private int _columnIndex;

    private int CompanyId;
    private int _userId;

    private bool _isRowDelete;
    private bool _isRowUpdate;
    private string _query = string.Empty;

    private readonly ICompanySetup _companySetup;
    private MrGridNumericTextBox TxtSno { get; set; }
    private MrGridTextBox TxtFileName { get; set; }
    private MrGridTextBox TxtCompanyName { get; set; }


}