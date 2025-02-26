using DatabaseModule.DataEntry.SalesMaster.SalesOrder;
using MrBLL.Utility.Common.Class;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.DataEntry.Design;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.Interface.SalesOrder;
using MrDAL.DataEntry.SalesMaster;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MrBLL.Domains.Restro.Entry;

public partial class FrmSplitTable : Form
{
    public FrmSplitTable(int tableId = 0, string tableName = "")
    {
        InitializeComponent();
        _master = new ClsMasterSetup();
        _entry = new ClsSalesEntry();
        _objDesign = new SalesEntryDesign();
        _occupiedTableId = tableId;
        TxtOccupiedTable.Text = tableName;
        BindGridControl();
        ReturnOrderVoucherNumber();
        _detailList = [];
        _salesOrderRepository = new SalesOrderRepository();
        if (tableId > 0)
        {
            GetProductDetails(_occupiedTableId);
        }
    }

    private void FrmSplitTable_Load(object sender, EventArgs e)
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

    private void RGridOccupied_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (RGridOccupied.RowCount == 1)
        {
            CustomMessageBox.Warning("THIS IS LAST ORDER OF THIS TABLE..!!");
            return;
        }
        if (RGridOccupied.CurrentRow != null)
        {
            ProductTransfer(RGridOccupied, RGridSplit);
        }
    }

    private void RGridSplit_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (RGridSplit.CurrentRow != null)
        {
            ProductTransfer(RGridSplit, RGridOccupied);
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

    private void TxtOccupiedTable_TextChanged(object sender, EventArgs e)
    {
        if (_occupiedTableId > 0)
        {
            if (RGridOccupied.ColumnCount == 0)
            {
                return;
            }
            GetProductDetails(_occupiedTableId);
        }
        else
        {
            if (RGridOccupied.Rows.Count > 0)
            {
                RGridOccupied.Rows.Clear();
            }
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
        var (description, id) = GetMasterList.GetTableList("SAVE", "A");
        if (description.IsValueExits())
        {
            TxtAvailableTable.Text = description;
            _availableTableId = id;
            var dtCheckOrder = _entry.CheckTableOrderExitsOrNot(_availableTableId);
            _availableVoucherNo = dtCheckOrder.Rows.Count > 0 ? dtCheckOrder.Rows[0]["SO_Invoice"].GetString() : "";
        }
        TxtAvailableTable.Focus();
    }

    private void BtnSplit_Click(object sender, EventArgs e)
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
        SaveSplitTableData();
        DialogResult = DialogResult.OK;
        Close();
        return;
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
        return;
    }


    //private void SaveSplitTableData()
    //{
    //    try
    //    {
    //        if (RGridSplit.RowCount > 0)
    //        {
    //            SaveSalesOrder();
    //        }

    //        if (RGridOccupied.RowCount > 0)
    //        {
    //            var cmd = $"DELETE AMS.SO_Details WHERE SO_Invoice ='{_voucherNo}'";
    //            var result = ExecuteCommand.ExecuteNonQuery(cmd);
    //            SaveSalesOrder(true);
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        CustomMessageBox.Warning("ERROR OCCURS WHILE TABLE TRANSFER..!!");
    //        TxtOccupiedTable.Focus();
    //        e.ToNonQueryErrorResult(e.StackTrace);
    //    }
    //}

    // METHOD FOR THIS FORM
    private void SaveSplitTableData()
    {
        try
        {
            if (string.IsNullOrEmpty(_availableVoucherNo))
            {
                var result = SaveSalesOrder(false);
                if (result.GetInt() > 0)
                {
                    RemoveRestraDetailsFromOccupiedTable();

                }
            }
            else
            {
                _voucherNo = _availableVoucherNo;
                var result = SaveSalesOrder(true);
                if (result.GetInt() > 0)
                {
                    RemoveRestraDetailsFromOccupiedTable();

                }
            }

        }
        catch (Exception ex)
        {
            CustomMessageBox.Warning("ERROR OCCURS WHILE TABLE TRANSFER..!!");
            TxtOccupiedTable.Focus();
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }
    }

    private void RemoveRestraDetailsFromOccupiedTable()
    {
        var data = _salesOrderRepository.DetailsList.Select(x => x.P_Id).ToList();
        var id = string.Join(", ", data);
        var cmd = $"DELETE AMS.SO_Details WHERE SO_Invoice = '{_voucherNo}' and P_Id in ({id})";
        var result = SqlExtensions.ExecuteNonQuery(cmd);
    }
    private void ReturnOrderVoucherNumber()
    {
        var dt = _master.IsExitsCheckDocumentNumbering("RSO");
        if (dt?.Rows.Count is 1)
        {
            _orderNumber = dt.Rows[0]["DocDesc"].ToString();
        }
    }

    private int SaveSalesOrder(bool isUpdate = false)
    {
        var actionTag = isUpdate ? "UPDATE" : "SAVE";
        const int newMasterKey = 0;
        _salesOrderRepository.SoMaster.MasterKeyId = isUpdate ? _masterKeyId : newMasterKey.ReturnMasterKeyId("SO");
        _availableVoucherNo = actionTag.Equals("SAVE") ? _availableVoucherNo.GetCurrentVoucherNo("RSO", _orderNumber) : _voucherNo;

        _salesOrderRepository.SoMaster.SO_Invoice = _availableVoucherNo;
        _salesOrderRepository.SoMaster.Invoice_Date = DateTime.Now;
        _salesOrderRepository.SoMaster.Invoice_Miti = DateTime.Now.GetNepaliDate();
        _salesOrderRepository.SoMaster.Invoice_Time = DateTime.Now;
        _salesOrderRepository.SoMaster.Ref_Vno = _voucherNo;
        _salesOrderRepository.SoMaster.Ref_Date = DateTime.Now;
        _salesOrderRepository.SoMaster.Ref_Miti = string.Empty;
        _salesOrderRepository.SoMaster.Customer_Id = ObjGlobal.FinanceCashLedgerId;
        _salesOrderRepository.SoMaster.Party_Name = string.Empty;
        _salesOrderRepository.SoMaster.Vat_No = string.Empty;
        _salesOrderRepository.SoMaster.Contact_Person = string.Empty;
        _salesOrderRepository.SoMaster.Mobile_No = string.Empty;
        _salesOrderRepository.SoMaster.Address = string.Empty;
        _salesOrderRepository.SoMaster.ChqNo = string.Empty;
        _salesOrderRepository.SoMaster.ChqDate = DateTime.Now;
        _salesOrderRepository.SoMaster.Invoice_Type = "ORDER";
        _salesOrderRepository.SoMaster.Invoice_Mode = "RSO";
        _salesOrderRepository.SoMaster.Payment_Mode = "CREDIT";
        _salesOrderRepository.SoMaster.DueDays = 0;
        _salesOrderRepository.SoMaster.DueDate = DateTime.Now;
        _salesOrderRepository.SoMaster.Agent_Id = 0;
        _salesOrderRepository.SoMaster.Subledger_Id = 0;
        _salesOrderRepository.SoMaster.Cls1 = 0;
        _salesOrderRepository.SoMaster.Cls2 = 0;
        _salesOrderRepository.SoMaster.Cls3 = 0;
        _salesOrderRepository.SoMaster.Cls4 = 0;
        _salesOrderRepository.SoMaster.Cur_Id = ObjGlobal.SysCurrencyId;
        _salesOrderRepository.SoMaster.Cur_Rate = 1;
        _salesOrderRepository.SoMaster.CounterId = 0;
        _salesOrderRepository.SoMaster.TableId = isUpdate ? _occupiedTableId : _availableTableId;

        var sumColType = isUpdate
            ? RGridOccupied.Rows.OfType<DataGridViewRow>()
            : RGridSplit.Rows.OfType<DataGridViewRow>();

        var gridViewRows = sumColType as DataGridViewRow[] ?? sumColType.ToArray();

        var basicAmount = gridViewRows.Sum(row => row.Cells["GTxtDisplayAmount"].Value.GetDecimal()).GetDecimalString();
        var discount = gridViewRows.Sum(row => row.Cells["GTxtPDiscount"].Value.GetDecimal()).GetDecimalString();
        var netAmount = gridViewRows.Sum(row => row.Cells["GTxtDisplayNetAmount"].Value.GetDecimal()).GetDecimalString();

        _salesOrderRepository.SoMaster.B_Amount = basicAmount.GetDecimal();
        _salesOrderRepository.SoMaster.TermRate = 0.GetDecimal();
        _salesOrderRepository.SoMaster.T_Amount = discount.GetDecimal();
        _salesOrderRepository.SoMaster.N_Amount = basicAmount.GetDecimal();
        _salesOrderRepository.SoMaster.LN_Amount = netAmount.GetDecimal();
        _salesOrderRepository.SoMaster.Tender_Amount = 0.GetDecimal();
        _salesOrderRepository.SoMaster.Return_Amount = 0.GetDecimal();
        _salesOrderRepository.SoMaster.V_Amount = 0.GetDecimal();
        _salesOrderRepository.SoMaster.Tbl_Amount = 0.GetDecimal();

        _salesOrderRepository.SoMaster.Action_Type = actionTag;
        _salesOrderRepository.SoMaster.R_Invoice = false;
        _salesOrderRepository.SoMaster.No_Print = 0;
        _salesOrderRepository.SoMaster.In_Words = ClsMoneyConversion.MoneyConversion(netAmount.GetDecimalString()); ;
        _salesOrderRepository.SoMaster.Remarks = "";
        _salesOrderRepository.SoMaster.Audit_Lock = false;

        if (RGridSplit.Rows.Count > 0)
        {
            var index = 0;
            foreach (var row in RGridSplit.Rows)
            {
                SetSalesOrderDetails(RGridSplit, index);
                index++;
            }
            _salesOrderRepository.DetailsList = _detailList;
        }
        var result = _salesOrderRepository.SaveSalesOrder("SAVE");
        return result.GetInt();
    }

    private void ProductTransfer(DataGridView from, DataGridView toView)
    {
        if (from.CurrentCell == null)
        {
            return;
        }
        toView.Rows.Add();
        var resultRow = from.CurrentRow;
        var index = toView.RowCount - 1;
        if (resultRow != null)
        {
            toView.Rows[index].Cells["GTxtSNo"].Value = index + 1;
            toView.Rows[index].Cells["GTxtProductId"].Value = resultRow.Cells["GTxtProductId"].Value.ToString();
            toView.Rows[index].Cells["GTxtOrderTime"].Value = resultRow.Cells["GTxtOrderTime"].Value.ToString();
            toView.Rows[index].Cells["GTxtShortName"].Value = resultRow.Cells["GTxtShortName"].Value.ToString();
            toView.Rows[index].Cells["GTxtProduct"].Value = resultRow.Cells["GTxtProduct"].Value.ToString();
            toView.Rows[index].Cells["GTxtGodownId"].Value = resultRow.Cells["GTxtGodownId"].Value.ToString();
            toView.Rows[index].Cells["GTxtGodown"].Value = resultRow.Cells["GTxtGodown"].Value.ToString();
            toView.Rows[index].Cells["GTxtAltQty"].Value = resultRow.Cells["GTxtAltQty"].Value.ToString();
            toView.Rows[index].Cells["GTxtAltUOMId"].Value = resultRow.Cells["GTxtAltUOMId"].Value.ToString();
            toView.Rows[index].Cells["GTxtAltUOM"].Value = resultRow.Cells["GTxtAltUOM"].Value.ToString();
            toView.Rows[index].Cells["GTxtQty"].Value = resultRow.Cells["GTxtQty"].Value.ToString();
            toView.Rows[index].Cells["GTxtUOMId"].Value = resultRow.Cells["GTxtUOMId"].Value.ToString();
            toView.Rows[index].Cells["GTxtMRP"].Value = resultRow.Cells["GTxtMRP"].Value.ToString();
            toView.Rows[index].Cells["GTxtUOM"].Value = resultRow.Cells["GTxtUOM"].Value.ToString();
            toView.Rows[index].Cells["GTxtValueRate"].Value = resultRow.Cells["GTxtValueRate"].Value.ToString();
            toView.Rows[index].Cells["GTxtDisplayRate"].Value = resultRow.Cells["GTxtDisplayRate"].Value.ToString();
            toView.Rows[index].Cells["GTxtDisplayAmount"].Value = resultRow.Cells["GTxtDisplayAmount"].Value.ToString();
            toView.Rows[index].Cells["GTxtDiscountRate"].Value = resultRow.Cells["GTxtDiscountRate"].Value.ToString();
            toView.Rows[index].Cells["GTxtPDiscount"].Value = resultRow.Cells["GTxtPDiscount"].Value.ToString();
            toView.Rows[index].Cells["GTxtValueBDiscount"].Value = resultRow.Cells["GTxtValueBDiscount"].Value.ToString();
            toView.Rows[index].Cells["GTxtDisplayNetAmount"].Value = resultRow.Cells["GTxtDisplayNetAmount"].Value.ToString();
            toView.Rows[index].Cells["GTxtValueRate"].Value = resultRow.Cells["GTxtValueRate"].Value.ToString();
            toView.Rows[index].Cells["GTxtValueNetAmount"].Value = resultRow.Cells["GTxtValueNetAmount"].Value.ToString();
            toView.Rows[index].Cells["GTxtIsTaxable"].Value = resultRow.Cells["GTxtIsTaxable"].Value.ToString();
            toView.Rows[index].Cells["GTxtTaxPriceRate"].Value = resultRow.Cells["GTxtTaxPriceRate"].Value.ToString();
            toView.Rows[index].Cells["GTxtValueVatAmount"].Value = resultRow.Cells["GTxtValueVatAmount"].Value.ToString();
            toView.Rows[index].Cells["GTxtValueTaxableAmount"].Value = resultRow.Cells["GTxtValueTaxableAmount"].Value.ToString();
            toView.Rows[index].Cells["GTxtValueExemptedAmount"].Value = resultRow.Cells["GTxtValueExemptedAmount"].Value.ToString();
            toView.Rows[index].Cells["GTxtNarration"].Value = resultRow.Cells["GTxtNarration"].Value.ToString();
            toView.Rows[index].Cells["GTxtFreeQty"].Value = resultRow.Cells["GTxtFreeQty"].Value.ToString();
            toView.Rows[index].Cells["GTxtFreeUnitId"].Value = resultRow.Cells["GTxtFreeUnitId"].Value.ToString();
            toView.Rows[index].Cells["GTxtInvoiceNo"].Value = resultRow.Cells["GTxtInvoiceNo"].Value.ToString();
            toView.Rows[index].Cells["GTxtInvoiceSNo"].Value = resultRow.Cells["GTxtInvoiceSNo"].Value.ToString();
            from.Rows.RemoveAt(resultRow.Index);

            //SetSalesOrderDetails(toView, index);
            foreach (DataGridViewRow row in from.Rows)
            {
                row.Cells["GTxtSNo"].Value = row.Index + 1;
            }
        }
    }

    public void SetSalesOrderDetails(DataGridView row, int index)
    {

        var list = new SO_Details();

        list.SO_Invoice = _availableVoucherNo;
        list.Invoice_SNo = index + 1;
        //list.Invoice_SNo = row.Rows[index].Cells["GTxtInvoiceSNo"].Value.GetInt();
        list.P_Id = row.Rows[index].Cells["GTxtProductId"].Value.GetLong();
        list.Gdn_Id = row.Rows[index].Cells["GTxtGodownId"].Value.GetInt() > 0 ? row.Rows[index].Cells["GTxtGodownId"].Value.GetInt() : null;
        list.Alt_Qty = row.Rows[index].Cells["GTxtAltQty"].Value.GetDecimal();
        list.Alt_UnitId = row.Rows[index].Cells["GTxtAltUOMId"].Value.GetInt() > 0 ? row.Rows[index].Cells["GTxtAltUOMId"].Value.GetInt() : null;
        list.Qty = row.Rows[index].Cells["GTxtQty"].Value.GetDecimal();
        list.Unit_Id = row.Rows[index].Cells["GTxtUOMId"].Value.GetInt() > 0 ? row.Rows[index].Cells["GTxtUOMId"].Value.GetInt() : null;
        list.Rate = row.Rows[index].Cells["GTxtDisplayRate"].Value.GetDecimal();
        list.B_Amount = row.Rows[index].Cells["GTxtValueBDiscount"].Value.GetDecimal();
        list.T_Amount = row.Rows[index].Cells["GTxtValueTaxableAmount"].Value.GetDecimal();
        list.N_Amount = row.Rows[index].Cells["GTxtValueNetAmount"].Value.GetDecimal();
        //list.AltStock_Qty = row.Rows[index].Cells["GTxtAltUOM"].Value.GetDecimal();
        //list.Stock_Qty = row.Rows[index].Cells["GTxtStockQty"].Value.GetDecimal();
        list.Narration = row.Rows[index].Cells["GTxtNarration"].Value.GetString();
        //list.IND_Invoice = row.Rows[index].Cells["GTxtQuotNo"].Value.GetString();
        //list.IND_Sno = row.Rows[index].Cells["GTxtQuotSno"].Value.GetInt();
        list.Tax_Amount = row.Rows[index].Cells["GTxtValueTaxableAmount"].GetDecimal();
        list.V_Amount = row.Rows[index].Cells["GTxtValueVatAmount"].GetDecimal();
        list.V_Rate = row.Rows[index].Cells["GTxtTaxPriceRate"].GetDecimal();
        list.Free_Unit_Id = row.Rows[index].Cells["GTxtFreeUnitId"].Value.GetInt();
        list.Free_Qty = list.StockFree_Qty = row.Rows[index].Cells["GTxtFreeQty"].Value.GetDecimal();
        list.ExtraFree_Unit_Id = 0;
        list.ExtraFree_Qty = 0;
        list.ExtraStockFree_Qty = 0;
        list.T_Product = row.Rows[index].Cells["GTxtIsTaxable"].Value.GetBool();
        //list.S_Ledger = row.Rows[index].Cells["GTxtSBLedgerId"].Value.GetLong() > 0 ? row.Rows[index].Cells["GTxtSBLedgerId"].Value.GetLong() : null;
        //list.SR_Ledger = row.Rows[index].Cells["GTxtSRLedgerId"].Value.GetLong() > 0 ? row.Rows[index].Cells["GTxtSRLedgerId"].Value.GetLong() : null;
        list.SZ1 = list.SZ2 = list.SZ3 = list.SZ4 = list.SZ5 = list.SZ6 = list.SZ7 = list.SZ8 = list.SZ9 = list.SZ10 = String.Empty;
        list.Serial_No = String.Empty;
        list.Batch_No = String.Empty;
        list.Exp_Date = null;
        list.Manu_Date = null;
        list.PDiscountRate = 0;
        list.PDiscount = 0;
        list.BDiscountRate = 0;
        list.BDiscount = 0;
        list.ServiceChargeRate = 0;
        list.ServiceCharge = 0;
        list.CancelNotes = list.Narration = list.IND_Invoice = list.QOT_Invoice = list.Notes = String.Empty;

        list.SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
        list.SyncGlobalId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
        if (ObjGlobal.LocalOriginId != null)
        {
            list.SyncOriginId = ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.IsValueExits()
                ? ObjGlobal.LocalOriginId.Value
                : Guid.Empty;
        }

        list.SyncCreatedOn = DateTime.Now;
        list.SyncLastPatchedOn = DateTime.Now;
        list.SyncRowVersion = _salesOrderRepository.ReturnSyncRowVersionVoucher("RSO", _voucherNo);
        _detailList.Add(list);
    }

    private void GetProductDetails(int tableId)
    {
        var rowIndex = 0;
        var dtCheckOrder = _entry.CheckTableOrderExitsOrNot(tableId);
        _voucherNo = dtCheckOrder.Rows[0]["SO_Invoice"].GetString();
        var dsSales = _entry.ReturnSalesOrderDetailsInDataSet(_voucherNo);
        var result = dsSales.Tables[1];
        _masterKeyId = dsSales.Tables[0].Rows[0]["MasterKeyId"].GetLong();
        foreach (DataRow resultRow in result.Rows)
        {
            var qtyCount = resultRow["Qty"].GetDecimal();
            //qtyCount = qtyCount.GetInt();
            for (var i = 0; i < qtyCount; i++)
            {
                RGridOccupied.Rows.Add();
                RGridOccupied.Rows[rowIndex].Cells["GTxtSNo"].Value = resultRow["Invoice_SNo"].ToString();
                RGridOccupied.Rows[rowIndex].Cells["GTxtProductId"].Value = resultRow["P_Id"].ToString();
                RGridOccupied.Rows[rowIndex].Cells["GTxtOrderTime"].Value = resultRow["OrderTime"];
                RGridOccupied.Rows[rowIndex].Cells["GTxtShortName"].Value = resultRow["PShortName"].ToString();
                RGridOccupied.Rows[rowIndex].Cells["GTxtProduct"].Value = resultRow["PName"].ToString();
                RGridOccupied.Rows[rowIndex].Cells["GTxtGodownId"].Value = 0;
                RGridOccupied.Rows[rowIndex].Cells["GTxtGodown"].Value = string.Empty;
                RGridOccupied.Rows[rowIndex].Cells["GTxtAltQty"].Value = resultRow["Alt_Qty"].GetDecimalString();
                RGridOccupied.Rows[rowIndex].Cells["GTxtAltUOMId"].Value = resultRow["Alt_UnitId"].ToString();
                RGridOccupied.Rows[rowIndex].Cells["GTxtAltUOM"].Value = resultRow["AltUnitCode"];
                RGridOccupied.Rows[rowIndex].Cells["GTxtQty"].Value = 1.GetDecimalString();
                RGridOccupied.Rows[rowIndex].Cells["GTxtUOMId"].Value = resultRow["Unit_Id"].ToString();
                RGridOccupied.Rows[rowIndex].Cells["GTxtMRP"].Value = 0;
                RGridOccupied.Rows[rowIndex].Cells["GTxtUOM"].Value = resultRow["UnitCode"].ToString();

                var salesRate = resultRow["Rate"].GetDecimal();
                var pDiscount = resultRow["PDiscount"].GetDecimal();

                var bAmount = (salesRate * 1);
                RGridOccupied.Rows[rowIndex].Cells["GTxtDisplayRate"].Value = salesRate.GetDecimalString();
                RGridOccupied.Rows[rowIndex].Cells["GTxtDisplayAmount"].Value = bAmount.GetDecimalString();

                RGridOccupied.Rows[rowIndex].Cells["GTxtDiscountRate"].Value = resultRow["PDiscountRate"].GetDecimalString();
                RGridOccupied.Rows[rowIndex].Cells["GTxtPDiscount"].Value = pDiscount.GetDecimalString();

                RGridOccupied.Rows[rowIndex].Cells["GTxtValueBDiscount"].Value = resultRow["BDiscount"].GetDecimalString();

                var taxRate = resultRow["PTax"].GetDecimal();
                var isTaxable = taxRate > 0;

                RGridOccupied.Rows[rowIndex].Cells["GTxtDisplayNetAmount"].Value = (bAmount - pDiscount).GetDecimalString();

                RGridOccupied.Rows[rowIndex].Cells["GTxtValueRate"].Value = salesRate;

                RGridOccupied.Rows[rowIndex].Cells["GTxtValueNetAmount"].Value = (bAmount - pDiscount).GetDecimalString();

                RGridOccupied.Rows[rowIndex].Cells["GTxtIsTaxable"].Value = isTaxable;
                RGridOccupied.Rows[rowIndex].Cells["GTxtTaxPriceRate"].Value = taxRate;

                RGridOccupied.Rows[rowIndex].Cells["GTxtValueVatAmount"].Value = 0;
                RGridOccupied.Rows[rowIndex].Cells["GTxtValueTaxableAmount"].Value = 0;
                RGridOccupied.Rows[rowIndex].Cells["GTxtValueExemptedAmount"].Value = 0;

                RGridOccupied.Rows[rowIndex].Cells["GTxtNarration"].Value = string.Empty;
                RGridOccupied.Rows[rowIndex].Cells["GTxtFreeQty"].Value = 0;
                RGridOccupied.Rows[rowIndex].Cells["GTxtFreeUnitId"].Value = 0;

                RGridOccupied.Rows[rowIndex].Cells["GTxtInvoiceNo"].Value = _voucherNo;
                RGridOccupied.Rows[rowIndex].Cells["GTxtInvoiceSNo"].Value = 0;
                rowIndex++;
            }
        }
    }

    private void BindGridControl()
    {
        _objDesign.GetRestroInvoiceDesign(RGridOccupied, "RESTRO");
        if (RGridOccupied.ColumnCount > 0)
        {
            foreach (DataGridViewColumn column in RGridOccupied.Columns)
            {
                if (column.Name is "GTxtProduct" or "GTxtQty")
                {
                    column.Visible = true;
                    continue;
                }
                column.Visible = false;
            }
        }
        _objDesign.GetRestroInvoiceDesign(RGridSplit, "RESTRO");
        if (RGridSplit.ColumnCount > 0)
        {
            foreach (DataGridViewColumn column in RGridSplit.Columns)
            {
                if (column.Name is "GTxtProduct" or "GTxtQty")
                {
                    column.Visible = true;
                    continue;
                }
                column.Visible = false;
            }
        }
    }

    // OBJECT FOR THIS FORM
    private int _occupiedTableId = 0;

    private int _availableTableId = 0;
    private long _masterKeyId;
    private string _orderNumber = string.Empty;
    private string _voucherNo = string.Empty;
    private string _availableVoucherNo = string.Empty;
    private readonly IMasterSetup _master;
    private readonly ISalesEntry _entry;
    private readonly ISalesDesign _objDesign;
    private readonly List<SO_Details> _detailList;
    private readonly ISalesOrderRepository _salesOrderRepository;
}