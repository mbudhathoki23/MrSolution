using DatabaseModule.DataEntry.SalesMaster.SalesOrder;
using MrBLL.Utility.Common.Class;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.Interface.SalesOrder;
using MrDAL.DataEntry.SalesMaster;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Global.Control;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Windows.Forms;

namespace MrBLL.Domains.Restro.Entry;

public partial class FrmTableTransfer : Form
{
    // TABLE TRANSFER

    #region --------------- TABLE TRANSFER ---------------

    public FrmTableTransfer(int tableId = 0, string tableName = "")
    {
        InitializeComponent();
        _entry = new ClsSalesEntry();
        _salesOrderRepository = new SalesOrderRepository();
        RGrid.AutoGenerateColumns = false;
        _occupiedTableId = tableId;
        TxtOccupiedTable.Text = tableName;
        if (tableId > 0)
        {
            GetProductDetails(_occupiedTableId);
        }
    }

    private void FrmTableTransfer_Load(object sender, EventArgs e)
    {
        TxtOccupiedTable.Enabled = BtnOccupied.Enabled = _occupiedTableId <= 0;
        if (TxtOccupiedTable.Enabled)
        {
            TxtOccupiedTable.Focus();
        }
        else
        {
            TxtAvailableTable.Focus();
        }
    }

    private void FrmTableTransfer_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (CustomMessageBox.ExitActiveForm() is DialogResult.Yes)
            {
                Close();
                return;
            }
        }
    }

    private void TxtOccupiedTable_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnOccupied_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtOccupiedTable.IsBlankOrEmpty())
            {
                TxtOccupiedTable.WarningMessage("PLEASE SELECT THE TABLE YOU WANT TO TRANSFER.!!");
                return;
            }
            SendKeys.Send("{TAB}");
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtOccupiedTable, BtnOccupied);
        }
    }

    private void BtnOccupied_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetTableList("SAVE", "O");
        if (description.IsValueExits())
        {
            TxtOccupiedTable.Text = description;
            _occupiedTableId = id;
            GetProductDetails(_occupiedTableId);
        }
        TxtOccupiedTable.Focus();
    }

    private void TxtOccupiedTable_TextChanged(object sender, EventArgs e)
    {
        if (_occupiedTableId > 0)
        {
            GetProductDetails(_occupiedTableId);
        }
        else
        {
            if (RGrid.Rows.Count > 0)
            {
                RGrid.Rows.Clear();
            }
        }
    }

    private void TxtAvailableTable_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnAvailable_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtAvailableTable.IsBlankOrEmpty())
            {
                TxtAvailableTable.WarningMessage("PLEASE SELECT THE TABLE YOU WANT TO TRANSFER.!!");
                return;
            }
            SendKeys.Send("{TAB}");
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtAvailableTable, BtnAvailable);
        }
    }

    private void BtnAvailable_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetTableList("SAVE", _occupiedTableId.ToString());
        if (description.IsValueExits())
        {
            TxtAvailableTable.Text = description;
            _availableTableId = id;
        }
        TxtAvailableTable.Focus();
    }

    private void BtnTransfer_Click(object sender, EventArgs e)
    {
        if (TxtOccupiedTable.IsBlankOrEmpty() || _occupiedTableId is 0)
        {
            TxtOccupiedTable.WarningMessage("OCCUPIED TABLE IS REQUIRED FOR TRANSFER");
            return;
        }
        if (TxtAvailableTable.IsBlankOrEmpty() || _availableTableId is 0)
        {
            TxtAvailableTable.WarningMessage("AVAILABLE TABLE IS REQUIRED FOR TRANSFER");
            return;
        }
        TransferTable();
        DialogResult = DialogResult.OK;
        Close();
        return;
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() is DialogResult.Yes)
        {
            Close();
            return;
        }
    }

    #endregion --------------- TABLE TRANSFER ---------------

    //METHOD FOR THIS FORM
    private void TransferTable()
    {
        try
        {
            var cmdString = $@"
                SELECT * FROM AMS.SO_Master WHERE TableId = {_occupiedTableId} AND  Invoice_Type ='ORDER' AND ISNULL(R_Invoice,0) = 0;
                SELECT * FROM AMS.SO_Master WHERE TableId = {_availableTableId} AND  Invoice_Type ='ORDER' AND ISNULL(R_Invoice,0) = 0; ";
            var dsMaster = SqlExtensions.ExecuteDataSet(cmdString);

            var exitTable = dsMaster.Tables[0];
            var transferTable = dsMaster.Tables[1];

            if (exitTable.Rows.Count > 0)
            {
                var dataVoucher = exitTable.Rows[0]["SO_Invoice"].ToString();
                var exitingVoucher = transferTable.Rows.Count > 0 ? transferTable.Rows[0]["SO_Invoice"].ToString() : "";

                cmdString = $@"
                    SELECT * FROM AMS.SO_Details  WHERE SO_Invoice = '{dataVoucher}' ORDER BY Invoice_SNo;
                    SELECT * FROM AMS.SO_Details  WHERE SO_Invoice = '{exitingVoucher}' ORDER BY Invoice_SNo";

                var dsDetails = SqlExtensions.ExecuteDataSet(cmdString);

                var transferDetails = dsDetails.Tables[0];
                var exitsData = dsDetails.Tables[1];

                if (exitsData.Rows.Count > 0)
                {
                    var serialNo = 0;
                    foreach (DataRow row in exitsData.Rows)
                    {
                        serialNo = row["Invoice_SNo"].GetInt();
                    }
                    foreach (DataRow row in transferDetails.Rows)
                    {

                        serialNo++;
                        _salesOrderRepository.SoMaster.MasterKeyId = transferTable.Rows[0]["MasterKeyId"].GetLong();
                        var details = new SO_Details
                        {
                            SO_Invoice = exitingVoucher,
                            Invoice_SNo = serialNo.GetInt(),
                            P_Id = row["P_Id"].GetLong(),
                            Gdn_Id = row["Gdn_Id"].GetInt(),
                            Alt_UnitId = row["Alt_UnitId"].GetInt(),
                            Unit_Id = row["Unit_Id"].GetInt(),
                            Alt_Qty = row["Alt_Qty"].GetDecimal(),
                            Qty = row["Qty"].GetDecimal(),
                            Rate = row["Rate"].GetDecimal(),
                            B_Amount = row["B_Amount"].GetDecimal(),
                            T_Amount = row["T_Amount"].GetDecimal(),
                            PDiscountRate = row["PDiscountRate"].GetDecimal(),
                            PDiscount = row["PDiscount"].GetDecimal(),
                            BDiscount = row["BDiscount"].GetDecimal(),
                            BDiscountRate = row["BDiscountRate"].GetDecimal(),
                            ServiceChargeRate = row["ServiceChargeRate"].GetDecimal(),
                            ServiceCharge = row["ServiceCharge"].GetDecimal(),
                            N_Amount = row["N_Amount"].GetDecimal(),
                            Narration = row["Narration"].ToString()
                        };
                        _salesOrderRepository.DetailsList.Add(details);
                        _salesOrderRepository.UpdateRestroOrder();
                    }

                    cmdString = $" UPDATE AMS.SO_Master SET R_Invoice = 1,CancelReason='TABLE DATA MIGRATION' WHERE SO_Invoice = '{dataVoucher}'";
                    var cancel = SqlExtensions.ExecuteNonQuery(cmdString);
                    if (cancel != 0)
                    {
                        cmdString = $@"
                            UPDATE AMS.TableMaster SET TableStatus = 'A' WHERE TableId ={_occupiedTableId} ;
                            UPDATE AMS.TableMaster SET  TableStatus =  CASE WHEN TableStatus ='O' THEN 'C' ELSE 'O' END WHERE TableId = {_availableTableId};";
                        var executeNonQuery = SqlExtensions.ExecuteNonQuery(cmdString);
                        CustomMessageBox.Information($"[{TxtOccupiedTable.Text}] TABLE INFORMATION MERGE TO [{TxtAvailableTable.Text}]");
                        TxtAvailableTable.Clear();
                        TxtOccupiedTable.Clear();
                        _occupiedTableId = _availableTableId = 0;
                        TxtOccupiedTable.Focus();
                    }
                    else
                    {
                        CustomMessageBox.Warning("ERROR OCCURS WHILE TABLE TRANSFER..!!");
                        TxtOccupiedTable.Focus();
                        return;
                    }
                }
                else
                {
                    cmdString = $@"
                        UPDATE AMS.SO_Master SET TableId = {_availableTableId}
                        WHERE TableId = {_occupiedTableId} AND SO_Invoice  ='{dataVoucher}'; ";
                    var result = SqlExtensions.ExecuteNonQuery(cmdString);

                    if (result != 0)
                    {
                        cmdString = $@"
                            UPDATE AMS.TableMaster SET TableStatus = 'A' WHERE TableId ={_occupiedTableId} ;
                            UPDATE AMS.TableMaster SET  TableStatus = 'O' WHERE TableId = {_availableTableId};";
                        var executeNonQuery = SqlExtensions.ExecuteNonQuery(cmdString);
                        CustomMessageBox.Information($"[{TxtOccupiedTable.Text}] TABLE INFORMATION TRANSFER TO [{TxtAvailableTable.Text}]");
                        TxtAvailableTable.Clear();
                        TxtOccupiedTable.Clear();
                        _occupiedTableId = _availableTableId = 0;
                        TxtOccupiedTable.Focus();
                    }
                    else
                    {
                        CustomMessageBox.Warning("ERROR OCCURS WHILE TABLE TRANSFER..!!");
                        TxtOccupiedTable.Focus();
                        return;
                    }
                }
            }
        }
        catch (Exception e)
        {
            CustomMessageBox.Warning("ERROR OCCURS WHILE TABLE TRANSFER..!!");
            TxtOccupiedTable.Focus();
            e.ToNonQueryErrorResult(e.StackTrace);
        }
    }

    private void GetProductDetails(int tableId)
    {
        var result = _entry.GetTableOrderDetails(tableId);
        RGrid.DataSource = result;
    }

    // OBJECT FOR THIS FORM
    private int _occupiedTableId = 0;

    private int _availableTableId = 0;
    private ISalesEntry _entry;
    private ISalesOrderRepository _salesOrderRepository;
}