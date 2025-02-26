using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MrBLL.Utility.CrystalReports;

public partial class FrmPrintDesignAdd : MrForm
{
    // ADD CRYSTAL DESIGN FORM
    public FrmPrintDesignAdd()
    {
        InitializeComponent();
    }

    private void FrmPrintDesignAdd_Load(object sender, EventArgs e)
    {
        try
        {
            Location = new Point(240, 40);
            BackColor = ObjGlobal.FrmBackColor();
            CmbType.SelectedIndex = 0;
            BindListItem();
            CmbModuleName_SelectedIndexChanged(sender, e);
            var path = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\PrintDesign\\"; //StartupPath
            if (!Directory.Exists(path))
            {
                // Try to create the directory.
                var di = Directory.CreateDirectory(path);
            }
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
        }
    }

    private void FrmPrintDesignAdd_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape)
        {
            var dialogResult = MessageBox.Show(@"ARE YOU SURE WANT TO CLOSE FORM!", ObjGlobal.Caption,
                MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) Close();
        }
    }

    private void cb_ModuleName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void CmbModuleName_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    private void lnkBrowseFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        openFileDialog1.Filter = "RPT|*.rpt|All files|*.*";
        var rs = openFileDialog1.ShowDialog();
        if (rs == DialogResult.OK)
        {
            _fileExt = Path.GetExtension(openFileDialog1.FileName);
            LblFileName.Text = Path.GetFileName(openFileDialog1.FileName);
            LblLocation.Text = openFileDialog1.FileName;
        }
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(CmbModule.Text.Trim()))
                MessageBox.Show(@"Please Select the Module..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            if (string.IsNullOrEmpty(TxtDesignName.Text.Trim()))
                MessageBox.Show(@"Enter the Design Name..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            if (string.IsNullOrEmpty(LblLocation.Text.Trim()))
                MessageBox.Show(@"File can't be left blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            if (CmbType.Text == "Report")
            {
                _saveFilePath = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\_Reports\\{CmbModule.Text}\\";
                if (!Directory.Exists(_saveFilePath))
                {
                    // Try to create the product wise directory.
                    var di = Directory.CreateDirectory(_saveFilePath);
                }
            }
            else
            {
                _saveFilePath = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\PrintDesign\\{CmbModule.Text}\\";
                if (!Directory.Exists(_saveFilePath))
                {
                    // Try to create the product wise directory.
                    var di = Directory.CreateDirectory(_saveFilePath);
                }
            }

            _query =
                "Insert Into Master.AMS.PrintDocument_Designer(Designerpaper_Name, Station,Type, Paths, Paper_Size, Orientation, Height, Width, Margin_Left, Margin_Right, Margin_Top, Margin_Bottom) "; //
            _query = _query + "  Values('" + TxtDesignName.Text.Trim() + "', '" + CmbModule.SelectedValue +
                     "', '" + CmbType.Text + "','" + Path.Combine(_saveFilePath, LblFileName.Text) +
                     "',1,	1,	0.00,	0.00,	0.25,	0.25,	0.25,	0.25 ";
            _query = _query + " )";
            using var cmd = new SqlCommand(_query, GetConnection.GetConnectionMaster());
            var Result = cmd.ExecuteNonQuery();
            if (Result > 0)
            {
                if (!File.Exists(_saveFilePath + LblFileName.Text) && LblLocation.Text != string.Empty)
                    File.Copy(LblLocation.Text, Path.Combine(_saveFilePath, LblFileName.Text));

                MessageBox.Show(@"Record Inserted Successfully..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                BindData();
                TxtDesignName.Text = string.Empty;
                LblLocation.Text = string.Empty;
                LblFileName.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
        }
    }

    private void BtnClear_Click(object sender, EventArgs e)
    {
        CmbModule.SelectedIndex = 0;
        TxtDesignName.Text = string.Empty;
        LblLocation.Text = string.Empty;
        LblFileName.Text = string.Empty;
    }

    private void RGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
        _currentColumn = e.ColumnIndex;
    }

    private void RGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
        _rowIndex = e.RowIndex;
    }

    private void RGrid_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void RGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (e.ColumnIndex == 4)
                if (RGrid[1, e.RowIndex].Value != null)
                {
                    CmbModule.Text = RGrid.Rows[e.RowIndex].Cells["txt_dgvModule"].Value.ToString();
                    CmbType.Text = RGrid.Rows[e.RowIndex].Cells["txt_dgvType"].Value.ToString();
                    TxtDesignName.Text = RGrid.Rows[e.RowIndex].Cells["txt_dgvDesignName"].Value
                        .ToString();
                    LblLocation.Text = RGrid.Rows[e.RowIndex].Cells["txt_dgvDesignPath"].Value
                        .ToString();
                    LblFileName.Text = string.Empty;
                    CmbModule.Focus();
                }

            if (e.ColumnIndex == 5)
                if (RGrid[1, e.RowIndex].Value != null)
                {
                    var res = MessageBox.Show(@"ARE YOU SURE YOU WANT TO DELETE THIS ROW?", ObjGlobal.Caption,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        _query = "Delete from Master.AMS.PrintDocument_Designer Where DocDesigner_Id='" +
                                 RGrid[0, e.RowIndex].Value + "' ";
                        using (var cmd = new SqlCommand(_query, GetConnection.GetConnectionMaster()))
                        {
                            var Result = cmd.ExecuteNonQuery();
                            if (Result > 0)
                                //if (!System.IO.File.Exists(SavefilePath + lbl_FileName.Text) && lbl_FilePath.Text != "")
                                //    File.Copy(lbl_FilePath.Text, Path.Combine(SavefilePath, lbl_FileName.Text));

                                MessageBox.Show(@"Record Deleted Successfully..!!", ObjGlobal.Caption,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        RGrid.Rows.RemoveAt(e.RowIndex);
                        for (var i = 0; i < RGrid.Rows.Count; i++)
                            RGrid.Rows[i].Cells[0].Value = i + 1;
                    }

                    RGrid.Focus();
                }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
        }
    }

    private void RGrid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
    {
        if (!e.Row.IsNewRow)
        {
            var res = MessageBox.Show(@"Are you sure you want to delete this row?", ObjGlobal.Caption,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                _rowsDelete = true;

                //RGrid.SelectedRows[0].Cells[0].Value.ToString();
                _query = "Delete from Master.AMS.PrintDocument_Designer Where DocDesigner_Id='" +
                         RGrid.Rows[_rowIndex].Cells["txt_dgvDocDesignId"].Value + "' ";
                using (var cmd = new SqlCommand(_query, GetConnection.GetConnectionMaster()))
                {
                    var Result = cmd.ExecuteNonQuery();
                    if (Result > 0)
                        //if (!System.IO.File.Exists(SavefilePath + lbl_FileName.Text) && lbl_FilePath.Text != "")
                        //    File.Copy(lbl_FilePath.Text, Path.Combine(SavefilePath, lbl_FileName.Text));

                        MessageBox.Show(@"RECORD DELETED SUCCESSFULLY..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                }

                //this.RGrid.Rows.RemoveAt(e.Row.Index);
                //for (int i = 0; i < RGrid.Rows.Count; i++)
                //{
                //    RGrid.Rows[i].Cells[0].Value = i + 1;
                //}
            }
            else
            {
                e.Cancel = true;
            }
        }
    }

    private void RGrid_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
        try
        {
            if (_rowsDelete)
            {
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
        }
    }

    // METHOD FOR THIS FORM

    #region --------------- METHOD FOR THIS FORM

    private void BindData()
    {
        try
        {
            if (CmbModule.SelectedValue.IsBlankOrEmpty())
            {
                return;
            }
            RGrid.Rows.Clear();
            var dt = GetConnection.SelectQueryFromMaster($"Select * from Master.AMS.PrintDocument_Designer Where Station = '{CmbModule.SelectedValue}' ");
            if (dt.Rows.Count <= 0)
            {
                return;
            }
            foreach (DataRow ro in dt.Rows)
            {
                var rows = RGrid.Rows.Count;
                RGrid.Rows.Add();
                RGrid.Rows[RGrid.RowCount - 1].Cells["txt_dgvDocDesignId"].Value = ro["DocDesigner_Id"].ToString();
                RGrid.Rows[RGrid.RowCount - 1].Cells["txt_dgvModule"].Value = ro["Station"].ToString();
                RGrid.Rows[RGrid.RowCount - 1].Cells["txt_dgvType"].Value = ro["Type"].ToString();
                RGrid.Rows[RGrid.RowCount - 1].Cells["txt_dgvDesignName"].Value = ro["Designerpaper_Name"].ToString();
                RGrid.Rows[RGrid.RowCount - 1].Cells["txt_dgvDesignPath"].Value = ro["Paths"].ToString();
            }

            ObjGlobal.DGridColorCombo(RGrid);
            RGrid.ClearSelection();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
        }
    }

    private void BindListItem()
    {
        var list = new List<ValueModel<string, string>>
        {
            new("Charts of Account Opening", "COA"),
            new("Product Opening", "POP"),
            new("PDC", "PDC"),
            new("Journal Voucher", "JV"),
            new("CashBank Voucher", "CB"),
            new("Debit Note", "DN"),
            new("Credit Note", "CN"),
            new("Purchase Indent", "PI"),
            new("Purchase Quotation", "PQ"),
            new("Purchase Order", "PO"),
            new("Goods In Transit", "GIT"),
            new("Purchase Challan", "PC"),
            new("Purchase Inter Branch", "PIB"),
            new("Purchase Quality Control", "PQC"),
            new("Purchase Invoice", "PB"),
            new("Purchase Additional", "PAB"),
            new("Purchase Return", "PR"),
            new("Purchase Travel & Tour", "PBT"),
            new("Purchase Expiry/Breakage Return", "PEB"),
            new("Sales Quotation", "SQ"),
            new("Sales Order", "SO"),
            new("Restro Order", "RSO"),
            new("Sales Order Cancellation", "SOC"),
            new("Sales Dispatch Order", "SDO"),
            new("Sales Dispatch Order Cancellation", "SDOC"),
            new("Sales Challan", "SC"),
            new("Sales Inter Branch", "SIB"),
            new("Sales Invoice", "SB"),
            new("Temp Sales Invoice", "TSB"),
            new("Sales Additional Invoice", "SAB"),
            new("Point Of Sales", "POS"),
            new("Restro Invoice", "RSB"),
            new("Abbreviated Tax Invoice", "ATI"),
            new("Sales Return", "SR"),
            new("Sales Expiry/Breakage Return", "SEB"),
            new("Sales Tours & Travel", "SBT"),
            new("Godown Transfer", "GT"),
            new("Stock Adjustment", "SA"),
            new("Physical Stock", "PSA"),
            new("Transfer Expiry/Breakage", "STEB"),
            new("Refund Entry", "NN"),
            new("Assembly Master", "ASSM"),
            new("Memo", "BOM"),
            new("Inventory Requisition", "SREQ"),
            new("Inventory Issue", "MI"),
            new("Inventory Issue Return", "MIR"),
            new("Inventory Receive", "MR"),
            new("Inventory Receive Return", "MRR"),
            new("Cost Center Expenses", "PCCE"),
            new("Sample Costing", "PSC"),
            new("Production Master Memo", "MBOM"),
            new("Production Memo", "IBOM"),
            new("Production Requisition", "IREQ"),
            new("Production Issue", "RMI"),
            new("Production Issue Return", "RMIR"),
            new("Finished Good Receive", "FGR"),
            new("Finished Good Receive Return", "FGRR"),
            new("Production Order", "IPO"),
            new("Production Planning", "IPP"),
            new("Assets Log", "ASL"),
            new("Bank Reconciliation", "BRN"),
            new("Patient Registration", "PGL"),
            new("OPD Patient Registration", "OPR"),
            new("IPD Patient Registration", "IPR"),
            new("EMR Patient Registration", "EPR"),
            new("Patient Flow up", "FPR"),
            new("Hospital", "HS"),
            new("OPD Billing", "OPDB"),
            new("IPD Billing", "IPDB"),
            new("Patient Discharge", "HPD"),
            new("Patient Drug History", "PDH")
        };
        CmbModule.DataSource = list;
        CmbModule.DisplayMember = "Item1";
        CmbModule.ValueMember = "Item2";
        CmbModule.SelectedIndex = 0;
    }

    #endregion --------------- METHOD FOR THIS FORM

    // OBJECT FOR THIS FORM

    #region --------------- OBJECT FOR THIS FORM ---------------

    private int _currentColumn;
    private string _fileExt = string.Empty;
    private string _fileName = string.Empty;
    private string _filePath = string.Empty;
    private string _query = string.Empty;
    private int _rowIndex;
    private bool _rowsDelete;
    private string _saveFilePath = string.Empty;

    #endregion --------------- OBJECT FOR THIS FORM ---------------
}