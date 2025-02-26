using DevExpress.XtraSplashScreen;
using MrBLL.Utility.Common.Class;
using MrBLL.Utility.SplashScreen;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Design;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MrBLL.DataEntry.ProductionMaster;

public partial class FrmAutoConsumption : MrForm
{
    #region --------------- AutoConsumption ---------------

    public FrmAutoConsumption()
    {
        InitializeComponent();
        _entry = new ClsStockEntry();
        _design = new GetStockDesign();
        _design.GetProductionEntryDesign(_gridView);
        DGrid.AutoGenerateColumns = false;
        _gridView.AllowUserToAddRows = false;
    }

    private void FrmAutoConsumption_Load(object sender, EventArgs e)
    {
        ReturnVoucherNo();
        BindSalesData();
        DGrid.Focus();
    }

    private void DGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (DGrid.CurrentRow != null)
        {
            var result = DGrid.CurrentRow.Cells[0].Value.GetBool();
            DGrid.CurrentRow.Cells[0].Value = !result;
        }
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            SplashScreenManager.ShowForm(typeof(FrmWait));
            foreach (DataGridViewRow viewRow in DGrid.Rows)
            {
                var result = viewRow.Cells[0].Value.GetBool();

                if (!result)
                {
                    continue;
                }
                sbInvoice = viewRow.Cells["VoucherNo"].Value.ToString();
                voucherMiti = viewRow.Cells["VoucherDate"].Value.ToString();
                voucherDate = voucherDate.GetEnglishDate(voucherMiti);
                _finishedQty = viewRow.Cells["Qty"].Value.GetDecimal();
                productId = viewRow.Cells["ProductId"].Value.GetLong();
                var dt = _entry.GetProductBomDetails(productId);
                var voucherNo = dt.Rows.Count == 1
                    ? dt.Rows[0]["VoucherNo"].ToString()
                    : dt.Rows.Count > 1 ? GetTransactionList.GetTransactionVoucherNo("SAVE", DateTime.Now.GetDateString(), "MED", "BOM", productId.ToString())
                        : "";
                SaveProductionVoucher(voucherNo);
                DGrid.CurrentCell = DGrid.Rows[viewRow.Index].Cells[0];
                DGrid.Rows[viewRow.Index].Selected = true;
            }
            var dialog = CustomMessageBox.Information($"PRODUCTION IS SAVE SUCCESSFULLY AGAINST SALES..!!");
            BtnRefresh.PerformClick();
            SplashScreenManager.CloseForm(false);
        }
        catch (Exception ex)
        {
            SplashScreenManager.CloseForm(false);
            ex.DialogResult();
        }
    }

    private void BtnRefresh_Click(object sender, EventArgs e)
    {
        BindSalesData();
        CkbSelectAll_CheckedChanged(sender, e);
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
    }

    private void CkbSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (DataGridViewRow viewRow in DGrid.Rows)
        {
            viewRow.Cells[0].Value = ChkSelectAll.Checked;
        }
    }

    #endregion --------------- AutoConsumption ---------------

    #region --------------- METHOD ---------------

    private string ReturnVoucherNo()
    {
        var dtCheck = ClsMasterSetup.GetDocumentNumberingSchema("IBOM");
        if (dtCheck.Rows.Count > 0)
        {
            _numberSchema = dtCheck.Rows[0]["DocDesc"].ToString();
        }
        return _numberSchema;
    }

    public void BindSalesData()
    {
        var result = _entry.GetAutoConsumptionProductInvoiceDetails();
        DGrid.DataSource = result;
    }

    private int SaveProductionVoucher()
    {
        var voucherNo = string.Empty;
        voucherNo = voucherNo.GetCurrentVoucherNo("IBOM", _numberSchema);

        _entry.VmProductionMaster.VoucherNo = voucherNo;

        _entry.VmProductionMaster.BOMVNo = bomVoucherNo;
        _entry.VmProductionMaster.BOMDate = bomVoucherDate.GetDateTime();
        _entry.VmProductionMaster.VDate = voucherDate.GetDateTime();
        _entry.VmProductionMaster.VMiti = voucherMiti;
        _entry.VmProductionMaster.FinishedGoodsId = productId;
        _entry.VmProductionMaster.FinishedGoodsQty = _finishedQty;
        _entry.VmProductionMaster.DepartmentId = _departmentId;
        _entry.VmProductionMaster.CostCenterId = _masterCostCenterId;
        _entry.VmProductionMaster.Machine = "AUTO CONSUMPTION";
        _entry.VmProductionMaster.Amount = _netAmount;
        _entry.VmProductionMaster.InWords = numberInWords;
        _entry.VmProductionMaster.Remarks = "AUTO CONSUMPTION OF RAW MATERIALS AGAINST SALES ";
        _entry.VmProductionMaster.GetView = _gridView;
        return _entry.SaveProductionSetup(_actionTag);
    }

    private void VoucherTotalCalculation()
    {
        decimal altQty = 0;
        decimal qty = 0;
        decimal amount = 0;

        var execute = _gridView.Rows.OfType<DataGridViewRow>();
        var rows = execute as DataGridViewRow[] ?? execute.ToArray();

        altQty = rows.Sum(row => row.Cells["GTxtAltQty"].Value.GetDecimal());
        qty = rows.Sum(row => row.Cells["GTxtQty"].Value.GetDecimal());
        amount = rows.Sum(row => row.Cells["GTxtAmount"].Value.GetDecimal());
        _netAmount = amount;
        numberInWords = ClsMoneyConversion.MoneyConversion(amount);
    }

    private int SaveProductionVoucher(string bom)
    {
        var bomVoucherDetails = _entry.GetBomVoucherDetails(bom);
        var bomMaster = bomVoucherDetails.Tables[0];
        var bomDetails = bomVoucherDetails.Tables[1];

        _masterCostCenterId = bomMaster.Rows[0]["CCId"].GetInt();
        _departmentId = bomMaster.Rows[0]["DepartmentId"].GetInt();
        bomVoucherNo = bomMaster.Rows[0]["VoucherNo"].ToString();
        bomVoucherDate = bomMaster.Rows[0]["VDate"].GetDateString();
        bomVoucherMiti = bomMaster.Rows[0]["VMiti"].ToString();
        var bomFinishedQty = bomMaster.Rows[0]["FinishedGoodsQty"].GetDecimal();
        if (bomDetails.Rows.Count > 0)
        {
            if (_gridView.RowCount > 0)
            {
                _gridView.Rows.Clear();
            }
            var iRow = 0;
            _gridView.Rows.Add(bomDetails.RowsCount());
            foreach (DataRow dr in bomDetails.Rows)
            {
                var rows = _gridView.Rows.Count;
                _gridView.Rows[iRow].Cells["GTxtSNo"].Value = dr["SerialNo"].ToString();
                _gridView.Rows[iRow].Cells["GTxtProductId"].Value = dr["ProductId"].ToString();
                _gridView.Rows[iRow].Cells["GTxtShortName"].Value = dr["PShortName"].ToString();
                _gridView.Rows[iRow].Cells["GTxtProduct"].Value = dr["PName"].ToString();
                _gridView.Rows[iRow].Cells["GTxtGodownId"].Value = dr["GodownId"].ToString();
                _gridView.Rows[iRow].Cells["GTxtGodown"].Value = dr["GName"].ToString();
                _gridView.Rows[iRow].Cells["GTxtCostCenterId"].Value = dr["CCId"].ToString();
                _gridView.Rows[iRow].Cells["GTxtCostCenter"].Value = dr["CCName"].ToString();
                _gridView.Rows[iRow].Cells["GTxtBOMVno"].Value = dr["VoucherNo"].ToString();
                _gridView.Rows[iRow].Cells["GTxtBOMSNo"].Value = dr["SerialNo"].ToString();
                _gridView.Rows[iRow].Cells["GTxtIssueVno"].Value = string.Empty;
                _gridView.Rows[iRow].Cells["GTxtIssueSNo"].Value = 0;
                _gridView.Rows[iRow].Cells["GTxtOrderNo"].Value = dr["OrderNo"].ToString();
                _gridView.Rows[iRow].Cells["GTxtOrderSNo"].Value = dr["OrderSNo"].ToString();
                _gridView.Rows[iRow].Cells["GTxtAltQty"].Value = dr["AltQty"].GetDecimalQtyString();
                _gridView.Rows[iRow].Cells["GTxtAltUOMId"].Value = dr["AltUnitId"].ToString();
                _gridView.Rows[iRow].Cells["GTxtAltUOM"].Value = dr["AltUnitCode"].ToString();
                if (bomFinishedQty > 1)
                {
                    var issueQty = dr["Qty"].GetDecimal();
                    var singleQty = issueQty / bomFinishedQty;

                    _gridView.Rows[iRow].Cells["GTxtActualQty"].Value = singleQty.GetDecimalQtyString();
                }
                else
                {
                    _gridView.Rows[iRow].Cells["GTxtActualQty"].Value = dr["Qty"].GetDecimalQtyString();
                }

                _gridView.Rows[iRow].Cells["GTxtQty"].Value = dr["Qty"].GetDecimalQtyString();
                _gridView.Rows[iRow].Cells["GTxtUOMId"].Value = dr["UnitId"].ToString();
                _gridView.Rows[iRow].Cells["GTxtUOM"].Value = dr["UnitCode"].ToString();
                _gridView.Rows[iRow].Cells["GTxtRate"].Value = dr["Rate"].GetDecimalString();
                _gridView.Rows[iRow].Cells["GTxtAmount"].Value = dr["Amount"].GetDecimalString();
                _gridView.Rows[iRow].Cells["GTxtNarration"].Value = dr["Narration"].ToString();

                iRow++;
            }
        }

        if (_gridView.Rows.Count > 0 && _finishedQty > 0)
        {
            for (var i = 0; i < _gridView.Rows.Count; i++)
            {
                var actQty = _gridView.Rows[i].Cells["GTxtActualQty"].Value.GetDecimal();
                var fQty = _finishedQty.GetDecimal();
                var qty = fQty * actQty;
                _gridView.Rows[i].Cells["GTxtQty"].Value = qty.GetDecimalQtyString();

                var rate = _gridView.Rows[i].Cells["GTxtRate"].Value.GetDecimal();
                _gridView.Rows[i].Cells["GTxtAmount"].Value = (qty * rate).GetDecimalString();
            }
        }
        VoucherTotalCalculation();

        var save = SaveProductionVoucher();
        if (save != 0)
        {
            var cmd = $"UPDATE AMS.SB_Details SET MaterialPost = 'Y' WHERE P_Id ='{productId}'  AND SB_Invoice = '{sbInvoice}'";
            var update = SqlExtensions.ExecuteNonQuery(cmd);
        }

        return save;
    }

    #endregion --------------- METHOD ---------------

    // OBJECT FOR THIS FORM

    #region --------------- OBJECT ---------------

    private IStockEntry _entry;
    private IStockEntryDesign _design;
    private DataGridView _gridView = new();

    private int _departmentId;
    private int _masterCostCenterId;
    private long productId = 0;

    private decimal _finishedQty;
    private decimal _netAmount;

    private string _numberSchema;
    private string voucherDate = string.Empty;
    private string voucherMiti = string.Empty;
    private string bomVoucherNo = string.Empty;
    private string sbInvoice = string.Empty;
    private string _actionTag = "SAVE";
    private string bomVoucherDate = string.Empty;
    private string bomVoucherMiti = string.Empty;
    private string numberInWords = string.Empty;

    #endregion --------------- OBJECT ---------------
}