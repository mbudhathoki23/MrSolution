using DatabaseModule.CustomEnum;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Models.Common;
using MrDAL.Utility.Config;
using MrDAL.Utility.Interface;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MrBLL.SystemSetting.PrintSetting;

public partial class FrmPrintDesignSetting : MrForm
{
    #region --------------- PRINT DESIGN SETTING ---------------

    public FrmPrintDesignSetting()
    {
        InitializeComponent();
        ObjGlobal.BindBranch(CmbBranch);
    }

    private void FrmPrintDesignSetting_Load(object sender, EventArgs e)
    {
        BindListItem();
        CmbEntryModule_SelectedIndexChanged(sender, e);
        CmbEntryModule.Focus();
    }

    private void FrmPrintDesignSetting_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar != (char)Keys.Escape) return;
        if (MessageBox.Show(@"Are you sure want to Close Form!", ObjGlobal.Caption, MessageBoxButtons.YesNo) ==
            DialogResult.Yes)
            Close();
    }

    #endregion --------------- PRINT DESIGN SETTING ---------------

    //EVENTS OF THIS FORM

    #region --------------- EVENTS OF THIS FORM ---------------

    private void CmbEntryModule_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void CmbEntryModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        AvailableDesigns();
    }

    private void BtnMove_Click(object sender, EventArgs e)
    {
        AddRowFromDataGridToSelectedGrid(DGrid, SGrid);
    }

    private void BtnMoveAll_Click(object sender, EventArgs e)
    {
        AddRowFromDataGridToSelectedGrid(DGrid, SGrid, true);
    }

    private void BtnRemove_Click(object sender, EventArgs e)
    {
        AddRowFromDataGridToSelectedGrid(SGrid, DGrid);
    }

    private void BtnRemoveAll_Click(object sender, EventArgs e)
    {
        AddRowFromDataGridToSelectedGrid(SGrid, DGrid, true);
    }

    private void CmbEntryModule_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Space) SendKeys.Send("{F4}");
    }

    private void Cmb_Branch_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Space) SendKeys.Send("{F4}");
    }

    private void TxtNoOfPrint_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtNoOfPrint, 'E');
    }

    private void TxtNoOfPrint_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtNoOfPrint, 'L');
    }

    private void TxtNoOfPrint_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !int.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void Cb_ModuleName_Enter(object sender, EventArgs e)
    {
        ObjGlobal.ComboBoxBackColor(CmbEntryModule, 'E');
    }

    private void Cb_ModuleName_Leave(object sender, EventArgs e)
    {
        ObjGlobal.ComboBoxBackColor(CmbEntryModule, 'L');
    }

    private void Cmb_Branch_Enter(object sender, EventArgs e)
    {
        ObjGlobal.ComboBoxBackColor(CmbBranch, 'E');
    }

    private void Cmb_Branch_Leave(object sender, EventArgs e)
    {
        ObjGlobal.ComboBoxBackColor(CmbBranch, 'L');
    }

    private void Chk_OnlinePrinting_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void TxtNotes_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtNotes, 'E');
    }

    private void TxtNotes_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtNotes, 'L');
    }

    private void TxtNotes_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) BtnSave.Focus();
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (SGrid.Rows.Count is 0 || !SGrid.Rows[0].Cells[0].Value.IsValueExits())
                this.NotifyValidationError(DGrid, "PLEASE SELECT THE DESIGN FIRST TO SAVE");

            var result = SaveDesign();
            if (result != 0)
            {
                MessageBox.Show(@"DESIGN ADD SUCCESSFULLY..!!", ObjGlobal.Caption);
                Close();
            }
            else
            {
                this.NotifyError("ERROR OCCURS WHILE DESIGN ADD..!!");
            }
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    #endregion --------------- EVENTS OF THIS FORM ---------------

    //GRID EVENTS CLICK

    #region --------------- GRID CONTROL EVENTS ---------------

    private void DGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            e.SuppressKeyPress = true;
            AddRowFromDataGridToSelectedGrid(DGrid, SGrid);
        }
    }

    private void SGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            e.SuppressKeyPress = true;
            AddRowFromDataGridToSelectedGrid(SGrid, DGrid);
        }
    }

    private void DGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (DGrid.CurrentRow != null)
        {
            AddRowFromDataGridToSelectedGrid(DGrid, SGrid);
        }
    }

    private void DGrid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        DGrid_CellContentDoubleClick(sender, null);
    }

    #endregion --------------- GRID CONTROL EVENTS ---------------

    // METHOD FOR THIS FORM

    #region --------------- OBJECT FOR THIS FORM ---------------

    private int SaveDesign()
    {
        _setup.VmDocument.ActionTag = "SAVE";
        _setup.VmDocument.DDP_Id = _setup.VmDocument.DDP_Id.ReturnMaxIntId("DOCUMENTDESIGNPRINT", string.Empty);
        _setup.VmDocument.Module = CmbEntryModule.SelectedValue.ToString();
        _setup.VmDocument.Is_Online = ChkIsOnline.Checked;
        _setup.VmDocument.NoOfPrint = TxtNoOfPrint.GetInt();
        _setup.VmDocument.Notes = TxtNotes.Text;
        _setup.VmDocument.Branch_Id = CmbBranch.SelectedValue.GetInt();
        _setup.VmDocument.SGridView = SGrid;
        return _setup.SaveDocumentDesignPrint();
    }

    private void AddRowFromDataGridToSelectedGrid(DataGridView data, DataGridView selected, bool isMultiple = false)
    {
        try
        {
            if (!isMultiple)
            {
                if (data.CurrentRow == null)
                {
                    return;
                }
                selected.Rows.Add();
                var value = data.Rows[data.CurrentRow.Index].Cells[0].Value;
                var value1 = data.Rows[data.CurrentRow.Index].Cells[1].Value;
                selected.Rows[selected.RowCount - 1].Cells[0].Value = value;
                selected.Rows[selected.RowCount - 1].Cells[1].Value = value1;
                data.Rows.RemoveAt(data.CurrentRow.Index);
            }
            else
            {
                for (var i = 0; i < data.RowCount; i++)
                {
                    selected.Rows.Add();
                    var value = data.Rows[i].Cells[0].Value;
                    var value1 = data.Rows[i].Cells[1].Value;
                    selected.Rows[selected.RowCount - 1].Cells[0].Value = value;
                    selected.Rows[selected.RowCount - 1].Cells[1].Value = value1;
                }

                data.Rows.Clear();
            }
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }
    }

    private void AvailableDesigns()
    {
        SGrid.Rows.Clear();
        DGrid.Rows.Clear();

        DGrid.Columns.Clear();
        SGrid.Columns.Clear();

        SGrid.AutoGenerateColumns = false;
        DGrid.AutoGenerateColumns = false;

        DGrid.AddColumn("GTxtDesign", "DESIGN", "Design", 200, 200, true, DataGridViewAutoSizeColumnMode.Fill);
        DGrid.AddColumn("GTxtDesignType", "DESIGN_TYPE", "DesignType", 0, 2, false);
        SGrid.AddColumn("GTxtDesign", "DESIGN", "Design", 200, 200, true, DataGridViewAutoSizeColumnMode.Fill);
        SGrid.AddColumn("GTxtDesignType", "DESIGN_TYPE", "DesignType", 0, 2, false);

        var dtDesign = new DataTable();
        dtDesign.AddColumn("Design", typeof(string));
        dtDesign.AddColumn("DesignType", typeof(string));

        dtDesign.Rows.Add("A4 Full", "DLL");
        dtDesign.Rows.Add("A4 Half", "DLL");
        dtDesign.Rows.Add("A5 Full", "DLL");

        var module = CmbEntryModule.SelectedValue;
        if (module is EntryModule.SB or EntryModule.POS or EntryModule.ATI or EntryModule.RSB)
        {
            dtDesign.Rows.Add("DefaultInvoice", "DLL");
            dtDesign.Rows.Add("Proforma Invoice", "DLL");
            dtDesign.Rows.Add("DefaultInvoiceWithNotes", "DLL");

            dtDesign.Rows.Add("MorselDesignWithVAT", "DLL");
            dtDesign.Rows.Add("DefaultInvoiceWithVAT", "DLL");
            dtDesign.Rows.Add("DefaultInvoiceWithPAN", "DLL");
            dtDesign.Rows.Add("DefaultPerformaInvoice", "DLL");

            dtDesign.Rows.Add("BanepaMiniMart", "DLL");
            dtDesign.Rows.Add("BhabSagarSalesInvoice", "DLL");

            dtDesign.Rows.Add("RestaurantDesignWithPAN", "DLL");
            dtDesign.Rows.Add("RestaurantDesignWithVAT", "DLL");

            dtDesign.Rows.Add("ThermalRestaurantDesignWithPAN", "DLL");
            dtDesign.Rows.Add("ThermalRestaurantDesignWithVAT", "DLL");

            dtDesign.Rows.Add("RestaurantDesignWithVAT5Inch", "DLL");
            dtDesign.Rows.Add("RestaurantDesignWithPAN5Inch", "DLL");

            dtDesign.Rows.Add("DefaultInvoiceWithVATWithOutNotes", "DLL");
            dtDesign.Rows.Add("AbbreviatedTaxInvoice3inch", "DLL");
            dtDesign.Rows.Add("AbbreviatedTaxInvoice3inchWithPreview", "DLL");
        }
        else if (module.Equals(EntryModule.SR))
        {
            dtDesign.Rows.Add("DefaultReturnInvoice", "DLL");
            dtDesign.Rows.Add("PrintDefaultReturnInvoice", "DLL");
            dtDesign.Rows.Add("DefaultReturnInvoiceWithVAT", "DLL");
            dtDesign.Rows.Add("DefaultReturnPerformaInvoice", "DLL");

            dtDesign.Rows.Add("RestaurantReturnDesignWithPan", "DLL");
            dtDesign.Rows.Add("RestaurantReturnDesignWithVAT", "DLL");

            dtDesign.Rows.Add("ThermalRestaurantReturnDesignWithVAT", "DLL");
            dtDesign.Rows.Add("ThermalRestaurantReturnDesignWithPan", "DLL");

            dtDesign.Rows.Add("AbbreviatedTaxReturnInvoice3inch", "DLL");
        }
        else if (module.Equals(EntryModule.SO))
        {
            dtDesign.Rows.Add("DefaultOrder", "DLL");
            dtDesign.Rows.Add("KOT/BOT", "DLL");
            dtDesign.Rows.Add("KOT/BOT RePrint", "DLL");
        }
        else if (module.Equals(EntryModule.DC))
        {
            dtDesign.Rows.Add("DayClosing", "DLL");
        }
        else if (module.Equals(EntryModule.SBC))
        {
            dtDesign.Rows.Add("CancelInvoice", "DLL");
            dtDesign.Rows.Add("DirectCancelInvoice3Inch", "DLL");
        }

        var source = CmbEntryModule.SelectedValue;
        var cmdString = $@"
            SELECT Designerpaper_Name Design FROM MASTER.AMS.PrintDocument_Designer WHERE Station='{source}'
            AND Designerpaper_Name NOT IN (SELECT Paper_Name FROM AMS.DocumentDesignPrint WHERE Module='{source}'
            AND ISNULL(Paper_Name,'') <> '' ) ";
        try
        {
            var dtCheckList = cmdString.Length > 0
                ? SqlExtensions.ExecuteDataSet(cmdString).Tables[0]
                : new DataTable();
            if (dtCheckList.Rows.Count > 0)
            {
                foreach (DataRow ro in dtCheckList.Rows)
                {
                    dtDesign.Rows.Add(ro["Design"], "CRYSTAL");
                }
            }
        }
        catch (Exception ex)
        {
            cmdString = @$"
                SELECT Designerpaper_Name Design FROM MASTER.AMS.PrintDocument_Designer WHERE Station='{source}'
                AND Designerpaper_Name NOT IN (SELECT Paper_Name collate Latin1_General_CI_AI FROM AMS.DocumentDesignPrint WHERE Module='{source}'
                AND ISNULL(Paper_Name,'') <> '' )  ";
            var dtCheckList = cmdString.Length > 0
                ? SqlExtensions.ExecuteDataSet(cmdString).Tables[0]
                : new DataTable();
            if (dtCheckList.Rows.Count > 0)
            {
                foreach (DataRow ro in dtCheckList.Rows)
                {
                    dtDesign.Rows.Add(ro["Design"], "CRYSTAL");
                }
            }
            var errMsg = ex.Message;
        }

        if (dtDesign.Rows.Count > 0)
        {
            var iRows = 0;
            DGrid.Rows.Add(dtDesign.Rows.Count);
            foreach (DataRow ro in dtDesign.Rows)
            {
                DGrid.Rows[iRows].Cells[0].Value = ro["Design"];
                DGrid.Rows[iRows].Cells[1].Value = ro["DesignType"];
                iRows++;
            }
        }

        cmdString = $" SELECT Paper_Name Design,DesignerPaper_Name DesignType FROM AMS.DocumentDesignPrint WHERE Module='{CmbEntryModule.SelectedValue}' ";
        var dtSelectedList = cmdString.Length > 0
            ? SqlExtensions.ExecuteDataSet(cmdString).Tables[0]
            : new DataTable();
        if (dtSelectedList.Rows.Count <= 0) return;
        {
            var iRows = 0;
            SGrid.Rows.Add(dtSelectedList.Rows.Count);
            foreach (DataRow ro in dtSelectedList.Rows)
            {
                SGrid.Rows[iRows].Cells[0].Value = ro["Design"];
                SGrid.Rows[iRows].Cells[1].Value = ro["DesignType"];
                iRows++;
            }
        }
    }

    private void BindListItem()
    {
        var designList = Enum.GetValues(typeof(EntryModule)).OfType<EntryModule>();
        CmbEntryModule.DataSource = designList.Select(x => new ValueModel<EntryModule, string>(x, x.GetDescription())).ToList();
        CmbEntryModule.ValueMember = "Item1";
        CmbEntryModule.DisplayMember = "Item2";
    }

    private void SGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (SGrid.CurrentRow != null)
        {
            AddRowFromDataGridToSelectedGrid(SGrid, DGrid);
        }
    }

    private void SGrid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        SGrid_CellContentDoubleClick(sender, null);
    }

    #endregion --------------- OBJECT FOR THIS FORM ---------------

    //OBJECT FOR THIS FORM

    #region --------------- OBJECT ---------------

    private readonly ISetup _setup = new ClsSetup();
    private bool isBindSuccess = false;

    #endregion --------------- OBJECT ---------------
}