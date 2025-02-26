using DatabaseModule.DataEntry.StockTransaction.ProductScheme;
using DevExpress.XtraEditors;
using MrBLL.Domains.POS.Master;
using MrBLL.Utility.Common;
using MrDAL.Control.ControlsEx.GridControl;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using MrDAL.Master.Interface;
using MrDAL.Master.Interface.ProductSetup;
using MrDAL.Master.ProductSetup;
using MrDAL.Utility.PickList;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace MrBLL.Master.ProductSetup;

public partial class FrmProductScheme : MrForm
{
    // PRODUCT SCHEME

    #region --------------- Product Scheme ---------------

    public FrmProductScheme()
    {
        InitializeComponent();
        IniInitializeGrid();
        AdjustControlsInDataGrid();
    }

    private void FrmProductScheme_Load(object sender, EventArgs e)
    {
        ClearControl();
        EnableControl();
        BtnNew.Focus();
        if (ObjGlobal.IsOnlineSync)
        {
            //Task.Run(GetAndSaveUnSynchronizedProductSchemes);
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }

    private async void GetAndSaveUnSynchronizedProductSchemes()
    {
        try
        {
            var productSchemeList = await _setup.GetUnSynchronizedData("AMS.ProductScheme");
            if (productSchemeList.List != null)
            {
                foreach (var data in productSchemeList.List)
                {
                    var productSchemeData = JsonConvert.DeserializeObject<Scheme_Master>(data.JsonData);
                    _schemeRepository.SchemeMaster.SchemeId = productSchemeData.SchemeId;
                    _schemeRepository.SchemeMaster.SchemeDate = productSchemeData.SchemeDate;
                    _schemeRepository.SchemeMaster.SchemeMiti = productSchemeData.SchemeMiti;
                    _schemeRepository.SchemeMaster.SchemeTime = productSchemeData.SchemeTime;
                    _schemeRepository.SchemeMaster.SchemeDesc = productSchemeData.SchemeDesc;
                    _schemeRepository.SchemeMaster.ValidFrom = productSchemeData.ValidFrom;
                    _schemeRepository.SchemeMaster.ValidFromMiti = productSchemeData.ValidFromMiti;
                    _schemeRepository.SchemeMaster.ValidTo = productSchemeData.ValidTo;
                    _schemeRepository.SchemeMaster.ValidToMiti = productSchemeData.ValidToMiti;
                    _schemeRepository.SchemeMaster.EnterBy = productSchemeData.EnterBy;
                    _schemeRepository.SchemeMaster.EnterDate = productSchemeData.EnterDate;
                    _schemeRepository.SchemeMaster.IsActive = productSchemeData.IsActive;
                    _schemeRepository.SchemeMaster.Remarks = productSchemeData.Remarks;
                    _schemeRepository.SchemeMaster.BranchId = productSchemeData.BranchId;
                    _schemeRepository.SchemeMaster.CompanyUnitId = productSchemeData.CompanyUnitId;
                    _schemeRepository.SchemeMaster.FiscalYearId = productSchemeData.FiscalYearId;
                    var result = _schemeRepository.SaveProductScheme("SAVE");
                    if (result > 0)
                    {
                        //_setup.ObjSyncLogDetail.BranchId = ObjGlobal.SysBranchId;
                        //_setup.ObjSyncLogDetail.SyncLogId = data.Id;
                        //var response = await _setup.SaveSyncLogDetails("SAVE");
                    }
                }
            }
        }
        catch (Exception e)
        {
        }
    }

    private void FrmProductScheme_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (!BtnNew.Enabled)
            {
                if (TxtProduct.Enabled)
                {
                    EnableGridControl();
                    ClearProductDetails();
                    SGrid.Focus();
                }
                else
                {
                    ActionTag = string.Empty;
                    ClearControl();
                    EnableControl();
                    BtnNew.Focus();
                }
            }
            else
            {
                btnExit.PerformClick();
            }
        }
    }

    private void FrmProductScheme_KeyDown(object sender, KeyEventArgs e)
    {
    }

    #endregion --------------- Product Scheme ---------------

    // BUTTON CLICK EVENTS

    #region --------------- BUTTON CLICK EVENTS ---------------

    private void BtnNew_Click(object sender, EventArgs e)
    {
        ActionTag = "SAVE";
        ClearControl();
        EnableControl(true);
        TxtDescription.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        ActionTag = "UPDATE";
        ClearControl();
        EnableControl(true);
        TxtDescription.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        ActionTag = "DELETE";
        ClearControl();
        EnableControl();
        TxtDescription.Focus();
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        if (MessageBox.Show(@"DO YOU WANT TO EXIT THIS FORM..??", ObjGlobal.Caption, MessageBoxButtons.YesNo) is
            DialogResult.Yes) Close();
    }

    #endregion --------------- BUTTON CLICK EVENTS ---------------

    // CONTROL CLICK EVENTS

    #region --------------- CONTROL CLICK EVENTS ---------------

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (ActiveControl.Name == "TxtDescription" || !TxtDescription.Enabled || ActionTag.IsBlankOrEmpty()) return;
        if (TxtDescription.Text.IsBlankOrEmpty())
            this.NotifyValidationError(TxtDescription, "SCHEME DESCRIPTION IS BLANK..!!");
    }

    private void Global_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void TxtDescription_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnDescription_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)(Keys.Enter)));
        }
        else if (TxtDescription.ReadOnly)
        {
            var searchKeys = e.KeyCode.ToString();
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE", searchKeys,
                TxtDescription, BtnDescription);
        }
    }

    private void BtnDescription_Click(object sender, EventArgs e)
    {
        var frmPickList = new FrmAutoPopList("MED", "SCHEME", ObjGlobal.SearchText, ActionTag, "ALL", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
                if (ActionTag != "SAVE")
                {
                    TxtDescription.Text = frmPickList.SelectedList[0]["SchemeDesc"].ToString().Trim();
                    SchemaId = frmPickList.SelectedList[0]["SchemeId"].GetInt();
                    FillProductScheme();
                    TxtDescription.ReadOnly = false;
                }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"PRODUCT SCHEME NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtProduct.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtProduct.Focus();
    }

    private void MskFrom_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
    {
    }

    private void MskFrom_Validating(object sender, CancelEventArgs e)
    {
    }

    private void MskTo_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
    {
    }

    private void MskTo_Validating(object sender, CancelEventArgs e)
    {
    }

    private void TxtProduct_Validated(object sender, EventArgs e)
    {
        if (ProductId is 0 && SGrid.RowCount > 0)
        {
            EnableGridControl();
            BtnSave.Focus();
        }

        if (ProductId is 0 && SGrid.RowCount is 0)
            this.NotifyValidationError(TxtProduct, "PRODUCT IS NOT SELECTED..!!");
    }

    private void OnTxtProductOnKeyDown(object _, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            if (rChkProduct.Checked)
                OpenProductLedger();
            else if (rChkCustomer.Checked) OpenGeneralLedger();
        }
        else if (e.KeyCode == Keys.F2)
        {
            var frm = new FrmSearchCProduct("CounterProduct", "Product List", ObjGlobal.SearchKey,
                "('Inventory','Service','Counter')", true);
            frm.ShowDialog();
            if (frm.SelectList.Count > 0)
            {
                TxtProduct.Text = frm.SelectList[0]["PName"].GetString();
                ProductId = frm.SelectList[0]["PID"].GetLong();
                SetProductInfo();
                TxtProduct.Focus();
            }

            frm.Dispose();
        }
        else if (e.KeyCode is Keys.F3)
        {
            OpenProductLedgerWithCheckBox();
        }
        else
        {
            var searchKeys = e.KeyCode.ToString();
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE", searchKeys,
                TxtProduct, OpenProductLedger);
        }
    }

    private void rChkProduct_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter) SGrid.Focus();
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValidForm())
            {
                if (IudProductScheme() != 0)
                {
                    this.NotifySuccess($"{TxtDescription.Text} [{ActionTag}] SUCCESSFULLY..!!");
                    ClearControl();
                    TxtDescription.Focus();
                }
                else
                {
                    this.NotifyError($" ERROR OCCURS WHILE {TxtDescription.Text} {ActionTag}..!!");
                    TxtDescription.Focus();
                }
            }
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            this.NotifyError($" ERROR OCCURS [{ex.Message}]..!!");
            TxtDescription.Focus();
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        btnExit.PerformClick();
    }

    #endregion --------------- CONTROL CLICK EVENTS ---------------

    //GRID CONTROL EVENTS

    #region --------------- GRID CONTROL ---------------

    private void SGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
        RowIndex = e.RowIndex;
    }

    private void SGrid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        if (SGrid.CurrentRow != null)
            SGrid.CurrentRow.Cells["GTxtCheck"].Value =
                SGrid.CurrentRow.Cells["GTxtCheck"].Value.GetBool() is not true;
    }

    private void SGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter && !TxtProduct.Enabled)
        {
            e.SuppressKeyPress = true;
            EnableGridControl(true);
            AdjustControlsInDataGrid();
            if (SGrid.Rows[RowIndex].Cells["GTxtProduct"].Value.IsValueExits())
                TextFromGrid();
            else
                TxtSno.Text = SGrid.RowCount.ToString();
            TxtProduct.Focus();
        }
    }

    private void SGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (SGrid.CurrentRow != null)
            SGrid.CurrentRow.Cells["GTxtCheck"].Value =
                SGrid.CurrentRow.Cells["GTxtCheck"].Value.GetBool() is not true;
    }

    private void SGrid_EnterKeyPressed(object sender, EventArgs e)
    {
        SGrid_KeyDown(sender, new KeyEventArgs(Keys.Enter));
    }

    #endregion --------------- GRID CONTROL ---------------

    // METHOD OF THIS FORM

    #region --------------- METHOD ---------------

    internal void FillProductScheme()
    {
        var dsScheme = _setup.GetProductScheme(SchemaId);
        if (dsScheme.Tables.Count > 0)
        {
            var master = dsScheme.Tables[0];
            var details = dsScheme.Tables[1];
            foreach (DataRow ro in master.Rows)
            {
                TxtDescription.Text = ro["SchemeDesc"].ToString();
                MskFrom.Text = ro["ValidFromMiti"].ToString();
                MskTo.Text = ro["ValidToMiti"].ToString();
            }

            var iRows = 0;
            SGrid.Rows.Clear();
            SGrid.Rows.Add(details.Rows.Count + 1);
            foreach (DataRow ro in details.Rows)
            {
                SGrid.Rows[iRows].Cells["GTxtSno"].Value = iRows + 1;
                SGrid.Rows[iRows].Cells["GTxtProductId"].Value = ro["ProductId"];
                SGrid.Rows[iRows].Cells["GTxtProduct"].Value = ro["PName"];
                SGrid.Rows[iRows].Cells["GTxtBarcode"].Value = ro["PShortName"];
                SGrid.Rows[iRows].Cells["GTxtUnitId"].Value = ro["PUnit"];
                SGrid.Rows[iRows].Cells["GTxtUnitCode"].Value = ro["UnitCode"];
                SGrid.Rows[iRows].Cells["GTxtGroupId"].Value = ro["GroupId"];
                SGrid.Rows[iRows].Cells["GTxtSubGroupId"].Value = ro["SubGroupId"];
                SGrid.Rows[iRows].Cells["GTxtSalesRate"].Value = ro["SalesRate"].GetDecimalString();
                SGrid.Rows[iRows].Cells["GTxtMrp"].Value = ro["MrpRate"].GetDecimalString();
                iRows++;
            }

            SGrid.CurrentCell = SGrid.Rows[iRows].Cells[0];
        }
    }

    internal void TextFromGrid()
    {
        if (SGrid.CurrentRow == null) return;
        ProductId = SGrid.CurrentRow.Cells["GTxtProductId"].Value.GetLong();
        SetProductInfo();
        TxtSno.Text = SGrid.CurrentRow.Cells["GTxtSno"].Value.ToString();
        TxtProduct.Text = SGrid.CurrentRow.Cells["GTxtProduct"].Value.ToString();
        TxtBarcode.Text = SGrid.CurrentRow.Cells["GTxtBarcode"].Value.ToString();
        PGroupId = SGrid.CurrentRow.Cells["GTxtGroupId"].Value.GetInt();
        PSubGroupId = SGrid.CurrentRow.Cells["GTxtSubGroupId"].Value.GetInt();
        UnitId = SGrid.CurrentRow.Cells["GTxtUnitId"].Value.GetInt();
        TxtUnit.Text = SGrid.CurrentRow.Cells["GTxtUnitCode"].Value.ToString();
        TxtRate.Text = SGrid.CurrentRow.Cells["GTxtSalesRate"].Value.GetDecimalString();
        TxtMrpRate.Text = SGrid.CurrentRow.Cells["GTxtMrp"].Value.GetDecimalString();
        IsGridUpdate = true;
    }

    internal void AddTextToGrid()
    {
        SGrid.Rows[RowIndex].Cells["GTxtSno"].Value = TxtSno.Text;
        SGrid.Rows[RowIndex].Cells["GTxtProductId"].Value = ProductId;
        SGrid.Rows[RowIndex].Cells["GTxtProduct"].Value = TxtProduct.Text;
        SGrid.Rows[RowIndex].Cells["GTxtBarcode"].Value = TxtBarcode.Text;
        SGrid.Rows[RowIndex].Cells["GTxtGroupId"].Value = PGroupId;
        SGrid.Rows[RowIndex].Cells["GTxtSubGroupId"].Value = PSubGroupId;
        SGrid.Rows[RowIndex].Cells["GTxtUnitId"].Value = UnitId;
        SGrid.Rows[RowIndex].Cells["GTxtUnitCode"].Value = TxtUnit.Text;
        SGrid.Rows[RowIndex].Cells["GTxtSalesRate"].Value = TxtRate.Text.GetDecimal();
        SGrid.Rows[RowIndex].Cells["GTxtMrp"].Value = TxtMrpRate.Text.GetDecimal();
        if (!IsGridUpdate)
        {
            SGrid.Rows.Add();
            SGrid.CurrentCell = SGrid.Rows[RowIndex + 1].Cells[0];
        }
        else
        {
            EnableGridControl();
            SGrid.CurrentCell = SGrid.Rows[RowIndex].Cells[0];
            SGrid.Focus();
        }

        ClearProductDetails();
        AdjustControlsInDataGrid();
        TxtProduct.Focus();
    }

    internal bool IsValidForm()
    {
        if (TxtDescription.Text.IsBlankOrEmpty())
        {
            MessageBox.Show(@"DESCRIPTION CANNOT BE BLANK..!!", ObjGlobal.Caption);
            return false;
        }

        if (!MskFrom.MaskCompleted)
        {
            MessageBox.Show(@"DESCRIPTION CANNOT BE BLANK..!!", ObjGlobal.Caption);
            return false;
        }

        if (!MskTo.MaskCompleted)
        {
            MessageBox.Show(@"DESCRIPTION CANNOT BE BLANK..!!", ObjGlobal.Caption);
            return false;
        }

        return true;
    }

    internal int IudProductScheme()
    {
        _schemeRepository.SchemeMaster.SchemeId = SchemaId is 0 ? _setup.ReturnMaxIdValue("SCHEME") : SchemaId;
        _schemeRepository.SchemeMaster.SchemeDesc = TxtDescription.Text;
        _schemeRepository.SchemeMaster.ValidFrom = ObjGlobal.ReturnEnglishDate(MskFrom.Text).GetDateTime();
        _schemeRepository.SchemeMaster.SchemeDate = DateTime.Now;
        _schemeRepository.SchemeMaster.SchemeMiti = ObjGlobal.ReturnNepaliDate(DateTime.Now.ToString());
        _schemeRepository.SchemeMaster.ValidFromMiti = MskFrom.Text;
        _schemeRepository.SchemeMaster.ValidTo = ObjGlobal.ReturnEnglishDate(MskTo.Text).GetDateTime();
        _schemeRepository.SchemeMaster.ValidToMiti = MskTo.Text;
        _schemeRepository.SchemeMaster.BranchId = ObjGlobal.SysBranchId.GetInt();
        _schemeRepository.SchemeMaster.CompanyUnitId = ObjGlobal.SysCompanyUnitId;
        _schemeRepository.SchemeMaster.FiscalYearId = ObjGlobal.SysFiscalYearId.GetInt();
        _schemeRepository.SchemeMaster.GetView = SGrid;
        return _schemeRepository.SaveProductScheme(ActionTag);
    }

    internal void ClearControl()
    {
        Text = ActionTag.IsBlankOrEmpty() ? " PRODUCT RATE SCHEME " : $"PRODUCT RATE SCHEME [{ActionTag}]";
        SGrid.Rows.Clear();
        if (SGrid.RowCount is 0) SGrid.Rows.Add();
        SGrid.ClearSelection();
        SchemaId = 0;
        TxtDescription.Clear();
        TxtDescription.ReadOnly = !ActionTag.Equals("SAVE");
        MskFrom.Text = DateTime.Now.ToString().GetNepaliDate();
        MskTo.Text = MskFrom.Text;
        ClearProductDetails();
    }

    internal void ClearProductDetails()
    {
        IsGridUpdate = false;
        ProductId = 0;
        PGroupId = 0;
        PSubGroupId = 0;
        TxtSno.Text = SGrid.RowCount.GetString();
        TxtProduct.Clear();
        TxtBarcode.Clear();
        UnitId = 0;
        TxtUnit.Clear();
        TxtMrpRate.Clear();
        TxtRate.Clear();
    }

    internal void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable;
        TxtDescription.Enabled = ActionTag.Equals("DELETE") || isEnable;
        ;
        BtnDescription.Enabled = TxtDescription.Enabled;
        MskFrom.Enabled = isEnable;
        MskTo.Enabled = isEnable;
        SGrid.Enabled = isEnable;
        GrpProductType.Enabled = isEnable;
        BtnSave.Enabled = ActionTag.Equals("DELETE") || isEnable;
        BtnCancel.Enabled = BtnSave.Enabled;
        EnableGridControl();
    }

    internal void EnableGridControl(bool isEnable = false)
    {
        TxtSno.Enabled = TxtSno.Visible = isEnable;
        TxtProduct.Enabled = TxtProduct.Visible = isEnable;
        TxtBarcode.Enabled = false;
        TxtBarcode.Visible = isEnable;
        TxtUnit.Enabled = false;
        TxtUnit.Visible = isEnable;
        TxtRate.Enabled = TxtRate.Visible = isEnable;
        TxtMrpRate.Enabled = TxtMrpRate.Visible = isEnable;
    }

    internal void IniInitializeGrid()
    {
        SGrid.AutoGenerateColumns = false;
        SGrid.AddColumn("GTxtSno", "SNO.", "GTxtSno", 65, 65, true, DataGridViewContentAlignment.MiddleCenter);
        SGrid.AddColumn("GTxtProductId", "ProductId", "GTxtProductId", 0, 2, false);
        SGrid.AddColumn("GTxtProduct", "PRODUCT", "GTxtProduct", 375, 200, true,
            DataGridViewAutoSizeColumnMode.Fill);
        SGrid.AddColumn("GTxtBarcode", "BARCODE", "GTxtBarcode", 100, 90, true,
            DataGridViewContentAlignment.MiddleCenter);
        SGrid.AddColumn("GTxtGroupId", "GroupId", "GTxtGroupId", 0, 2, false);
        SGrid.AddColumn("GTxtSubGroupId", "BARCODE", "GTxtSubGroupId", 0, 2, false);
        SGrid.AddColumn("GTxtUnitId", "UnitId", "GTxtUnitId", 0, 2, false);
        SGrid.AddColumn("GTxtUnitCode", "UOM", "GTxtUnitCode", 90, 80, true,
            DataGridViewContentAlignment.MiddleCenter);
        SGrid.AddColumn("GTxtMrp", "MRP", "GTxtMrp", 100, 90, true, DataGridViewContentAlignment.MiddleRight);
        SGrid.AddColumn("GTxtSalesRate", "SALES_RATE", "SalesRate", 120, 90, true,
            DataGridViewContentAlignment.MiddleRight);
        TxtSno = new MrGridNumericTextBox(SGrid)
        {
            ReadOnly = true,
            TextAlign = HorizontalAlignment.Center
        };
        TxtProduct = new MrGridTextBox(SGrid)
        {
            ReadOnly = true
        };
        TxtProduct.KeyDown += OnTxtProductOnKeyDown;
        TxtProduct.Validated += TxtProduct_Validated;
        TxtBarcode = new MrGridTextBox(SGrid)
        {
            ReadOnly = false
        };
        TxtUnit = new MrGridTextBox(SGrid)
        {
            ReadOnly = false
        };
        TxtRate = new MrGridNumericTextBox(SGrid)
        {
            ReadOnly = false
        };
        TxtRate.Validating += (sender, e) =>
        {
            if (TxtRate.GetDecimal() is 0 && TxtRate.Focused && TxtRate.Enabled)
            {
                this.NotifyValidationError(TxtRate, "SALES RATE CAN'T BE ZERO");
            }
            else
            {
                TxtRate.Text = TxtRate.GetDecimalString();
                AddTextToGrid();
            }
        };
        TxtMrpRate = new MrGridNumericTextBox(SGrid);
        TxtMrpRate.Validating += (sender, e) =>
        {
            if (TxtMrpRate.GetDecimal() is 0 && TxtMrpRate.Focused && TxtMrpRate.Enabled)
                this.NotifyValidationError(TxtMrpRate, "MRP RATE CAN'T BE ZERO");
            TxtMrpRate.Text = TxtMrpRate.GetDecimalString();
        };
        ObjGlobal.DGridColorCombo(SGrid);
    }

    internal void SetProductInfo()
    {
        if (ProductId is 0) return;
        var dtProduct = _setup.GetPosProductInfo(ProductId.ToString());
        if (dtProduct.Rows.Count <= 0) return;
        TxtProduct.Text = dtProduct.Rows[0]["PName"].GetString();
        TxtBarcode.Text = dtProduct.Rows[0]["PShortName"].GetString();
        TxtRate.Text = dtProduct.Rows[0]["PSalesRate"].GetDecimalString();
        PGroupId = dtProduct.Rows[0]["PGrpId"].GetInt();
        PSubGroupId = dtProduct.Rows[0]["PSubGrpId"].GetInt();
        UnitId = dtProduct.Rows[0]["PUnit"].GetInt();
        TxtUnit.Text = dtProduct.Rows[0]["UOM"].GetString();
        TxtMrpRate.Text = dtProduct.Rows[0]["PMRP"].GetDecimalString();
    }

    internal void AdjustControlsInDataGrid()
    {
        if (SGrid.CurrentRow == null) return;
        var currentRow = RowIndex;
        var columnIndex = SGrid.Columns["GTxtSno"].Index;
        TxtSno.Size = SGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtSno.Location = SGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtSno.TabIndex = columnIndex;

        columnIndex = SGrid.Columns["GTxtProduct"].Index;
        TxtProduct.Size = SGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtProduct.Location = SGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtProduct.TabIndex = columnIndex;

        columnIndex = SGrid.Columns["GTxtBarcode"].Index;
        TxtBarcode.Size = SGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtBarcode.Location = SGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtBarcode.TabIndex = columnIndex;

        columnIndex = SGrid.Columns["GTxtUnitCode"].Index;
        TxtUnit.Size = SGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtUnit.Location = SGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtUnit.TabIndex = columnIndex;

        columnIndex = SGrid.Columns["GTxtMrp"].Index;
        TxtMrpRate.Size = SGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtMrpRate.Location = SGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtMrpRate.TabIndex = columnIndex;

        columnIndex = SGrid.Columns["GTxtSalesRate"].Index;
        TxtRate.Size = SGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtRate.Location = SGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtRate.TabIndex = columnIndex;
    }

    internal void GlobalKeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    internal void OpenProductLedger()
    {
        var frmPickList = new FrmAutoPopList("MAX", "PRODUCT", ObjGlobal.SearchText, ActionTag, "ALL", "MASTER");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtProduct.Text = frmPickList.SelectedList[0]["PName"].ToString().Trim();
                ProductId = frmPickList.SelectedList[0]["PID"].GetLong();
                SetProductInfo();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"PRODUCT NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtProduct.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtProduct.Focus();
    }

    internal void OpenGeneralLedger()
    {
        var frmPickList = new FrmAutoPopList("MAX", "GENERALLEDGER", ObjGlobal.SearchText, ActionTag, "CUSTOMER",
            "MASTER");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                var LedgerId = frmPickList.SelectedList[0]["LedgerId"].GetLong();
                var dtMultiple = LedgerId > 0 ? _setup.GetProductListFromLedger(LedgerId) : new DataTable();
                if (dtMultiple.Rows.Count > 0)
                {
                    EnableGridControl();
                    var iRows = 0;
                    SGrid.Rows.Clear();
                    SGrid.Rows.Add(dtMultiple.Rows.Count + 1);
                    foreach (DataRow dr in dtMultiple.Rows)
                    {
                        SGrid.Rows[iRows].Cells["GTxtSno"].Value = iRows + 1;
                        SGrid.Rows[iRows].Cells["GTxtProductId"].Value = dr["ProductId"].ToString();
                        SGrid.Rows[iRows].Cells["GTxtProduct"].Value = dr["Product"].ToString();
                        SGrid.Rows[iRows].Cells["GTxtBarcode"].Value = dr["Barcode"].ToString();
                        SGrid.Rows[iRows].Cells["GTxtGroupId"].Value = dr["PGrpId"].ToString();
                        SGrid.Rows[iRows].Cells["GTxtSubGroupId"].Value = dr["PSubGrpId"].ToString();
                        SGrid.Rows[iRows].Cells["GTxtUnitId"].Value = dr["PUnit"].ToString();
                        SGrid.Rows[iRows].Cells["GTxtUnitCode"].Value = dr["UOM"].ToString();
                        SGrid.Rows[iRows].Cells["GTxtSalesRate"].Value = dr["SalesRate"].GetDecimalString();
                        SGrid.Rows[iRows].Cells["GTxtMrp"].Value = dr["PMRP"].GetDecimalString();
                        iRows++;
                    }

                    SGrid.CurrentCell = SGrid.Rows[iRows].Cells[0];
                    EnableGridControl();
                    AdjustControlsInDataGrid();
                    TxtProduct.Focus();
                }
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"PRODUCT NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtProduct.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtProduct.Focus();
    }

    internal void OpenProductLedgerWithCheckBox()
    {
        var frm2 = new FrmTagList
        {
            ReportDesc = "PRODUCT",
            BranchId = ObjGlobal.SysBranchId.ToString(),
            CompanyUnitId = ObjGlobal.SysCompanyUnitId.ToString(),
            FiscalYearId = ObjGlobal.SysFiscalYearId.ToString(),
            GroupId = PGroupIdString,
            SubGroupId = PSubGroupIdString
        };
        frm2.ShowDialog();
        ProductIdString = ClsTagList.PlValue1;
        if (string.IsNullOrEmpty(ProductIdString))
        {
            MessageBox.Show(@"PRODUCT NOT SELECTED FROM THIS GROUP..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
        else
        {
            var dtMultiple = _schemeRepository.GetProductListWithCheckbox(ProductIdString);
            if (dtMultiple.Rows.Count > 0)
                foreach (DataRow dr in dtMultiple.Rows)
                {
                    ProductId = dr["ProductId"].GetLong();
                    SetProductInfo();
                    AddTextToGrid();
                    ClearProductDetails();
                }
        }
    }

    #endregion --------------- METHOD ---------------

    // OBJECT OF THIS FORM

    #region --------------- OBJECT FOR THIS FORM ---------------

    private int RowIndex { get; set; }
    private int SchemaId { get; set; }
    private int UnitId { get; set; }
    private int PGroupId { get; set; }
    private string PGroupIdString { get; set; }
    private int PSubGroupId { get; set; }
    private string PSubGroupIdString { get; set; }
    private long ProductId { get; set; }
    private string ProductIdString { get; set; }
    private string ActionTag { get; set; } = string.Empty;
    private bool IsGridUpdate;
    private MrGridNumericTextBox TxtSno { get; set; }
    private MrGridTextBox TxtProduct { get; set; }
    private MrGridTextBox TxtBarcode { get; set; }
    private MrGridTextBox TxtUnit { get; set; }
    private MrGridNumericTextBox TxtRate { get; set; }
    private MrGridNumericTextBox TxtMrpRate { get; set; }
    private readonly IProductSchemeRepository _schemeRepository = new ProductSchemeRepository();
    private IMasterSetup _setup;

    #endregion --------------- OBJECT FOR THIS FORM ---------------
}