using MrDAL.Control.WinControl;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.DataEntry.Common;

public partial class FrmBillingTerm : MrForm
{
    #region --------------- Billing_Term ---------------

    public FrmBillingTerm(Point location)
    {
        InitializeComponent();
        InitializeTermDataTable();
        ObjGlobal.DGridColorCombo(PDGrid);
        ObjGlobal.DGridColorCombo(BDGrid);
        TxtTermDescription.BackColor = Color.White;
        TxtBasicAmount.BackColor = Color.White;
        TxtTermSign.BackColor = Color.White;
    }

    private void InitializeTermDataTable()
    {
        dtpbt.Reset();
        dtpbt.Columns.Add("OrderNo");
        dtpbt.Columns.Add("SNo");
        dtpbt.Columns.Add("TermId");
        dtpbt.Columns.Add("TermName");
        dtpbt.Columns.Add("Basis");
        dtpbt.Columns.Add("Sign");
        dtpbt.Columns.Add("ProductId");
        dtpbt.Columns.Add("TermType");
        dtpbt.Columns.Add("TermRate");
        dtpbt.Columns.Add("TermAmt");
        dtpbt.Columns.Add("Source");
        dtpbt.Columns.Add("Formula");
    }

    private void Billing_Term_Shown(object sender, EventArgs e)
    {
        dtTermMaster.Reset();
        dtTermMaster.Clear();
        lbl_Total.Text = TotalTermAmt.ToString("0.00");
        lbl_BasicTotProAmt.Text = ProBasicamt.ToString("0.00");
        lbl_TotProTermAmt.Text = "0";
        Text = Title;
        MyMethod();
        LoadData();
        dtTermMaster = string.IsNullOrEmpty(query) switch
        {
            false => GetConnection.SelectDataTableQuery(query),
            _ => dtTermMaster
        };
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void Billing_Term_Load(object sender, EventArgs e)
    {
        PnlBillingTerm2.Top = 25;
        PnlBillingTerm2.Left = 0;
        PnlBillingTerm1.Visible = false;
        lbl_Procap.Visible = false;
        Height = 330;
        GridDesign();
        DataIsChanged = true;
        DataIsChanged = false;
        FocusModeof_Grd = true;
        Cleartxt();
    }

    private void Billing_Term_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyValue == 27) BtnSave_Click(sender, e);
    }

    private void TxtPRate_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void MyMethod()
    {
        switch (InvoiceType?.ToUpper())
        {
            //Purchase
            case "PRO_PQ":
            case "PRO_PO":
            case "PRO_POC":
            case "PRO_PC":
            case "PRO_PB":
            case "PRO_PR":
            case "PRO_PEB":
            case "PRO_IBP":
            {
                query =
                    "Select Order_No OrderNo, PT_Id as TermId, PT_Name as TermName,'' as Formula,PT_Condition TermType from AMS.PT_Term where PT_Condition = 'P' and PT_Basis<>'Q'  ORDER BY Order_No asc";
                chk_ProTerm.Visible = false;
                break;
            }
            case "PQ":
            case "PO":
            case "POC":
            case "PC":
            case "PB":
            case "PR":
            case "PEB":
            {
                query =
                    "Select Order_No OrderNo,PT_Id as TermId, PT_Name as TermName,'' as Formula,PT_Condition TermType from AMS.PT_Term where PT_Condition = 'B' and PT_Basis<>'Q'  ORDER BY Order_No asc";
                lbl_TotProTermAmt.Visible = false;
                lblTotalAmount.Visible = false;
                lbl_TotProTermAmt.Text = ProBasicamt.ToString("0.00");
                break;
            }
            //Sales
            case "PRO_SQ":
            case "PRO_SO":
            case "PRO_SOC":
            case "PRO_SC":
            case "PRO_SB":
            case "PRO_SR":
            case "PRO_SEB":
            case "PRO_SI":
            case "PRO_SDO":
            case "PRO_SDOC":
            {
                query =
                    "Select Order_No OrderNo,ST_Id as TermId, ST_Name as TermName,'' as Formula,ST_Condition TermType from AMS.ST_Term where ST_Condition = 'P' and ST_Basis<>'Q'  ORDER BY Order_No asc";
                chk_ProTerm.Visible = false;
                dt = GetConnection.SelectDataTableQuery(
                    "Select IsNull(SalesVat_Id,0) SalesVat_Id From AMS.SystemConfiguration");
                if (dt.Rows.Count > 0)
                {
                    systemVatId = Convert.ToInt32(dt.Rows[0]["SalesVat_Id"]);
                    dtAbb.Reset();
                    dtAbb = GetConnection.SelectDataTableQuery(
                        "Select * from AMS.ST_Term where ST_Condition = 'P' and ST_Basis <> 'Q'  ORDER BY Order_No asc");
                }

                break;
            }
            case "SQ":
            case "SO":
            case "SOC":
            case "SC":
            case "SB":
            case "IBS":
            case "ATI":
            case "SR":
            case "SI":
            case "SEB":
            case "SDO":
            case "SDOC":
            case "HCO":
            {
                query =
                    "Select Order_No OrderNo,ST_Id as TermId, ST_Name as TermName,'' as Formula,ST_Condition TermType from AMS.ST_Term where ST_Condition = 'B' and ST_Basis <>'Q'  ORDER BY Order_No asc";
                lbl_TotProTermAmt.Visible = false;
                lblTotalAmount.Visible = false;
                lbl_TotProTermAmt.Text = ProBasicamt.ToString("0.00");
                dt = GetConnection.SelectDataTableQuery(
                    "Select IsNull(SalesVat_Id,0) SalesVat_Id From AMS.SystemConfiguration");
                if (dt.Rows.Count > 0)
                {
                    systemVatId = Convert.ToInt32(dt.Rows[0]["SalesVat_Id"]);
                    dtAbb.Reset();
                    dtAbb = GetConnection.SelectDataTableQuery(
                        "Select * from AMS.ST_Term where ST_Condition = 'B' and ST_Basis <>'Q'  ORDER BY Order_No asc");
                }

                break;
            }
        }
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        PTermId = string.Empty;
        BasicAmt = ObjGlobal.ReturnDouble(lbl_Total.Text);
        var rows = BDGrid.Rows.Count;
        for (var i = 0; i < rows; i++)
        {
            if (BDGrid.Rows[i].Cells[1].Value == null) continue;
            if (dtpbt.Rows.Count > 0)
            {
            }

            var row = dtpbt.NewRow();
            row["OrderNo"] = BDGrid.Rows[i].Cells[10].Value.ToString();
            row["SNo"] = PSNo;
            row["TermId"] = BDGrid.Rows[i].Cells[1].Value.ToString();
            PTermId = PTermId == string.Empty
                ? BDGrid.Rows[i].Cells[1].Value.ToString()
                : PTermId + "," + BDGrid.Rows[i].Cells[1].Value;
            row["TermName"] = BDGrid.Rows[i].Cells[2].Value.ToString();
            row["Basis"] = BDGrid.Rows[i].Cells[3].Value.ToString();
            row["Sign"] = BDGrid.Rows[i].Cells[4].Value.ToString();
            row["ProductId"] = ProductId;
            row["TermType"] = TermType;
            row["TermRate"] = BDGrid.Rows[i].Cells[5].Value.ToString();
            row["TermAmt"] = BDGrid.Rows[i].Cells[6].Value.ToString();
            row["Source"] = Source;
            row["Formula"] = BDGrid.Rows[i].Cells[7].Value.ToString();
            dtpbt.Rows.Add(row);
        }

        DialogResult = DialogResult.OK;
        Close();
    }

    #endregion --------------- Billing_Term ---------------

    #region --------------- event ---------------

    private void txt_Rate_Enter(object sender, EventArgs e)
    {
    }

    private void txt_Rate_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            if (TxtTermAmount.Enabled)
                SendKeys.Send("{TAB}");
            else
                txt_Amount_KeyPress(sender, e);
        }

        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void txt_Rate_TextChanged(object sender, EventArgs e)
    {
        if (ObjGlobal.ReturnDecimal(TxtTermRate.Text) != 0) TermCalculations();
    }

    private void txt_Rate_Validated(object sender, EventArgs e)
    {
        TxtTermRate.Text = ObjGlobal.ReturnDecimal(TxtTermRate.Text).ToString(ObjGlobal.SysAmountFormat);
    }

    private void txt_Amount_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            if (TxtTermDescription.Text.Trim() != string.Empty)
            {
                TxtTermAmount.Text = ObjGlobal.ReturnDouble(TxtTermAmount.Text).ToString(ObjGlobal.SysAmountFormat);
                TermCalculations(Convert.ToInt16(BDGrid.Rows[rowIndex].Cells[0].Value));
                AddTextDataToGrid(Convert.ToInt16(BDGrid.Rows[rowIndex].Cells[0].Value));
                Cleartxt();
                DataIsChanged = false;
                if (BDGrid.Rows.Count - 1 == rowIndex)
                {
                    btn_Ok.Focus();
                }
                else
                {
                    BDGrid.ClearSelection();
                    if (BDGrid.Rows.Count > rowIndex)
                    {
                        BDGrid.Rows[rowIndex + 1].Selected = true;
                        BDGrid.CurrentCell = BDGrid.Rows[rowIndex + 1].Cells[2];
                    }
                    else
                    {
                        BDGrid.Rows[rowIndex].Selected = true;
                        BDGrid.CurrentCell = BDGrid.Rows[rowIndex].Cells[2];
                    }

                    BDGrid.Focus();
                }
            }
        }
        else
        {
            DataIsChanged = true;
        }

        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void txt_Amount_Validated(object sender, EventArgs e)
    {
        TxtTermAmount.Text = ObjGlobal.ReturnDecimal(TxtTermAmount.Text).ToString(ObjGlobal.SysAmountFormat);
    }

    private void chk_ProTerm_Click(object sender, EventArgs e)
    {
        if (chk_ProTerm.Visible)
        {
            if (chk_ProTerm.Checked)
            {
                dtProTermDtl.Reset();
                if (DtPwiseDetl.Rows.Count > 0)
                {
                    var i = 0;

                    foreach (DataRow rowpbt in DtPwiseDetl.Rows)
                    {
                        decimal termAmount = 0;
                        var isExists = false;
                        if (i == 0)
                        {
                            dtProTermDtl.Reset();
                            dtProTermDtl.Columns.Add("SNo");
                            dtProTermDtl.Columns.Add("OrderNo");
                            dtProTermDtl.Columns.Add("TermId");
                            dtProTermDtl.Columns.Add("TermName");
                            dtProTermDtl.Columns.Add("Basis");
                            dtProTermDtl.Columns.Add("Sign");
                            dtProTermDtl.Columns.Add("ProductId");
                            dtProTermDtl.Columns.Add("TermType");
                            dtProTermDtl.Columns.Add("TermRate");
                            dtProTermDtl.Columns.Add("TermAmt");
                            dtProTermDtl.Columns.Add("Source");
                            dtProTermDtl.Columns.Add("Formula");
                        }

                        var result = DtPwiseDetl.Select("TermId='" + rowpbt["TermId"] + "'");
                        foreach (var rowAmt in result)
                        {
                            if (rowAmt["TermAmt"].ToString() == string.Empty) rowAmt["TermAmt"] = "0";

                            termAmount += ObjGlobal.ReturnDecimal(rowAmt["Sign"] + rowAmt["TermAmt"].ToString());
                        }

                        foreach (DataRow dtWiseDetails in dtProTermDtl.Rows)
                            if (dtWiseDetails["TermId"].ToString() == rowpbt["TermId"].ToString())
                                isExists = true;
                        if (isExists == false)
                        {
                            var row = dtProTermDtl.NewRow();
                            row["OrderNo"] = rowpbt["OrderNo"];
                            row["SNo"] = rowpbt["SNo"];
                            row["TermId"] = rowpbt["TermId"];
                            row["TermName"] = rowpbt["TermName"];
                            row["Basis"] = rowpbt["Basis"];
                            row["Sign"] = rowpbt["Sign"];
                            row["ProductId"] = rowpbt["ProductId"];
                            row["TermType"] = rowpbt["TermType"];
                            row["TermRate"] = rowpbt["TermRate"];
                            row["TermAmt"] = Math.Abs(termAmount).ToString("0.00");
                            row["Source"] = rowpbt["Source"];
                            row["Formula"] = rowpbt["Formula"];
                            dtProTermDtl.Rows.Add(row);
                        }

                        i += 1;
                    }
                }

                decimal totalTermAmt = 0;
                if (dtProTermDtl.Rows.Count >= 1)
                {
                    var i = 0;
                    if (PDGrid.Rows.Count > 0) PDGrid.Rows.Clear();

                    foreach (DataRow ro in dtProTermDtl.Rows)
                    {
                        var rows = PDGrid.Rows.Count;
                        PDGrid.Rows.Add();
                        PDGrid.Rows[PDGrid.RowCount - 1].Cells["dgv_SNo"].Value = ro["SNo"].ToString();
                        PDGrid.Rows[PDGrid.RowCount - 1].Cells["dgv_TermId"].Value = ro["TermId"].ToString();
                        PDGrid.Rows[PDGrid.RowCount - 1].Cells["dgv_Desc"].Value = ro["TermName"].ToString();
                        PDGrid.Rows[PDGrid.RowCount - 1].Cells["dgv_Basis"].Value = ro["Basis"].ToString();
                        PDGrid.Rows[PDGrid.RowCount - 1].Cells["dgv_Sign"].Value = ro["Sign"].ToString();
                        PDGrid.Rows[PDGrid.RowCount - 1].Cells["dgv_Amount"].Value = ro["TermAmt"].ToString();
                        PDGrid.Rows[PDGrid.RowCount - 1].Cells["dgv_Formula"].Value = ro["Formula"].ToString();
                        PDGrid.Rows[PDGrid.RowCount - 1].Cells["dgv_TaxationType"].Value = string.Empty;
                        if (ro["TermAmt"].ToString() == string.Empty) ro["TermAmt"] = "0";

                        double Rate = 0;
                        Rate = i == 0
                            ? Math.Round(
                                ObjGlobal.ReturnDouble(ro["TermAmt"].ToString()) /
                                ObjGlobal.ReturnDouble(lbl_BasicTotProAmt.Text) * 100, 0)
                            : Math.Round(
                                ObjGlobal.ReturnDouble(ro["TermAmt"].ToString()) /
                                (ObjGlobal.ReturnDouble(lbl_BasicTotProAmt.Text) +
                                 ObjGlobal.ReturnDouble(totalTermAmt.ToString())) * 100, 0);
                        PDGrid.Rows[PDGrid.RowCount - 1].Cells["dgv_Rate"].Value = Rate.ToString("0.00");

                        if (ro["Sign"].ToString() == "-")
                            totalTermAmt -= Math.Abs(ObjGlobal.ReturnDecimal(ro["TermAmt"].ToString()));
                        else
                            totalTermAmt += Math.Abs(ObjGlobal.ReturnDecimal(ro["TermAmt"].ToString()));

                        i += 1;
                    }

                    lbl_BasicTotProAmt.Visible = true;
                    lbl_TotProTermAmt.Text = totalTermAmt.ToString(ObjGlobal.SysAmountFormat);
                }

                ObjGlobal.DgvBackColor(PDGrid);
                PnlBillingTerm1.Visible = true;
                lbl_Procap.Visible = true;

                PnlBillingTerm1.Top = 10;
                PnlBillingTerm2.Top = 220;
                Height = 608;
            }
            else
            {
                PnlBillingTerm1.Visible = false;
                lbl_Procap.Visible = true;
                PnlBillingTerm2.Top = 0;
                PnlBillingTerm2.Left = 0;
                Height = 370;
            }
        }
    }

    private void Grid_Enter(object sender, EventArgs e)
    {
    }

    private void Grid_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
        rowIndex = e.RowIndex;
    }

    private void Grid_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
        currentColumn = e.ColumnIndex;
    }

    private void Grid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter) return;
        BDGrid.Rows[rowIndex].Selected = true;
        e.SuppressKeyPress = true;
        GridControlMode(true);
        SetGridDataToText();
        TxtTermRate.Focus();
    }

    #endregion --------------- event ---------------

    #region --------------- Method ---------------

    private void GridDesign()
    {
        BDGrid.ReadOnly = true;
        BDGrid.Columns[0].Width = 35;
        BDGrid.Columns[1].Width = 0; //Term Id Hide
        BDGrid.Columns[2].Width = 200; //Term Name
        BDGrid.Columns[3].Width = 55; //Basis
        BDGrid.Columns[4].Width = 45; //Sign
        BDGrid.Columns[5].Width = 70; //Rate
        BDGrid.Columns[6].Width = 100; //Amount
        BDGrid.Columns[7].Width = 0; //Formula
        BDGrid.Columns[8].Width = 0; //TaxationType
        BDGrid.Columns[9].Width = 0; //ProGroup

        BDGrid.Columns[1].Visible = false;
        BDGrid.Columns[7].Visible = false;
        BDGrid.Columns[8].Visible = false;
        BDGrid.Columns[9].Visible = false;

        BDGrid.Columns[0].HeaderText = "S.No";
        BDGrid.Columns[2].HeaderText = "Description";
        BDGrid.Columns[3].HeaderText = "Basis";
        BDGrid.Columns[4].HeaderText = "Sign";
        BDGrid.Columns[5].HeaderText = "Rate";
        BDGrid.Columns[6].HeaderText = "Amount";
        BDGrid.Columns[7].HeaderText = "Formula";
        BDGrid.Columns[8].HeaderText = "TaxationType";
        BDGrid.Columns[9].HeaderText = "PGroup";

        BDGrid.Columns[0].Name = "g_Sno";
        BDGrid.Columns[2].Name = "g_Desc";
        BDGrid.Columns[3].Name = "g_Basis";
        BDGrid.Columns[4].Name = "g_Sign";
        BDGrid.Columns[5].Name = "g_Rate";
        BDGrid.Columns[6].Name = "g_Amount";
        BDGrid.Columns[7].Name = "g_Formula";
        BDGrid.Columns[8].Name = "g_TaxationType";
        BDGrid.Columns[9].Name = "g_PGroup";

        BDGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
        BDGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        BDGrid.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        BDGrid.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        BDGrid.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        BDGrid.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        BDGrid.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        BDGrid.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        BDGrid.Columns[5].DefaultCellStyle.Format = "0.00";
        BDGrid.Columns[6].DefaultCellStyle.Format = "0.00";
        GridControlMode(false);
    }

    private void GridControlMode(bool mode)
    {
    }

    private void Cleartxt()
    {
        TxtTermDescription.Clear();
        TxtBasicAmount.Clear();
        TxtTermSign.Clear();
        TxtTermRate.Clear();
        TxtTermAmount.Clear();

        TxtTermDescription.Enabled = false;
        TxtBasicAmount.Enabled = false;
        TxtTermSign.Enabled = false;
    }

    private void SetGridDataToText()
    {
        if (BDGrid.Rows.Count <= 0) return;
        if (BDGrid.Rows[rowIndex].Selected != true) return;
        Term_Id = Convert.ToInt16(BDGrid.Rows[rowIndex].Cells[1].Value.ToString());
        TxtTermDescription.Text = BDGrid.Rows[rowIndex].Cells[2].Value.ToString();
        TxtBasicAmount.Text = BDGrid.Rows[rowIndex].Cells[3].Value.ToString();
        TxtTermSign.Text = BDGrid.Rows[rowIndex].Cells[4].Value.ToString();
        TxtTermRate.Text = BDGrid.Rows[rowIndex].Cells[5].Value.ToString();
        TxtTermAmount.Text = BDGrid.Rows[rowIndex].Cells[6].Value.ToString();
    }

    private void AddTextDataToGrid(int GId)
    {
        query = Module == "Purchase"
            ? $"Select * from AMS.PT_Term where PT_Name = '{TxtTermDescription.Text}' "
            : $"Select * from AMS.ST_Term where ST_Name = '{TxtTermDescription.Text}' ";

        dt.Reset();
        dt = GetConnection.SelectDataTableQuery(query);
        if (dt.Rows.Count >= 1)
            if (GId != 0 && TxtTermDescription.Text.Trim() != string.Empty)
            {
                BDGrid.Rows[GId - 1].Cells[1].Value = Term_Id.ToString();
                BDGrid.Rows[GId - 1].Cells[2].Value = TxtTermDescription.Text;
                BDGrid.Rows[GId - 1].Cells[3].Value = TxtBasicAmount.Text;
                BDGrid.Rows[GId - 1].Cells[4].Value = TxtTermSign.Text;
                BDGrid.Rows[GId - 1].Cells[5].Value = TxtTermRate.Text != string.Empty
                    ? ObjGlobal.ReturnDecimal(TxtTermRate.Text).ToString("0.00")
                    : ObjGlobal.ReturnDecimal(0.ToString()).ToString("0.00");
                BDGrid.Rows[GId - 1].Cells[6].Value = TxtTermAmount.Text != string.Empty
                    ? ObjGlobal.ReturnDecimal(TxtTermAmount.Text).ToString("0.00")
                    : ObjGlobal.ReturnDecimal(0.ToString()).ToString("0.00");
                BDGrid.ClearSelection();
            }

        ObjGlobal.DGridColorCombo(BDGrid);
        TermTotalCalc();
        Cleartxt();
    }

    private void LoadData()
    {
        try
        {
            double tmpTermAmt = 0;
            double termCalAmt = 0;
            lbl_BasicAmt.Text = BasicAmt.ToString("0.00");
            termCalAmt = BasicAmt;
            Row_Vat = -1;
            Free_Vat = -1;
            if (SelectQuery == null || string.IsNullOrEmpty(SelectQuery)) return;
            dt.Reset();
            dt = GetConnection.SelectDataTableQuery(SelectQuery);
            if (dt.Rows.Count <= 0) return;
            BDGrid.Rows.Clear();
            foreach (DataRow ro in dt.Rows)
                if (InvoiceType == "SI" && systemVatId == Convert.ToInt32(ro["TermId"]))
                {
                }
                else
                {
                    var rows = BDGrid.Rows.Count;
                    BDGrid.Rows.Add();
                    BDGrid.Rows[BDGrid.RowCount - 1].Cells[0].Value = rows + 1;
                    BDGrid.Rows[BDGrid.RowCount - 1].Cells[1].Value = ro["TermId"].ToString();
                    BDGrid.Rows[BDGrid.RowCount - 1].Cells[2].Value = ro["TermName"].ToString();
                    BDGrid.Rows[BDGrid.RowCount - 1].Cells[3].Value = ro["Basis"].ToString();
                    BDGrid.Rows[BDGrid.RowCount - 1].Cells[4].Value = ro["Sign"].ToString();
                    if (ObjGlobal.ReturnDecimal(ro["TermRate"].ToString()) != 0)
                    {
                        var termPercentageAmt =
                            termCalAmt * ObjGlobal.ReturnDouble(ro["TermRate"].ToString()) / 100;
                        if (ro["Sign"].ToString() == "+")
                        {
                            termCalAmt += termPercentageAmt;
                            tmpTermAmt += termPercentageAmt;
                        }
                        else
                        {
                            termCalAmt -= termPercentageAmt;
                            tmpTermAmt -= termPercentageAmt;
                        }

                        BDGrid.Rows[BDGrid.RowCount - 1].Cells[5].Value = ObjGlobal
                            .ReturnDecimal(ro["TermRate"].ToString()).ToString(ObjGlobal.SysAmountFormat);
                        BDGrid.Rows[BDGrid.RowCount - 1].Cells[6].Value = ObjGlobal
                            .ReturnDecimal(termPercentageAmt.ToString()).ToString(ObjGlobal.SysAmountFormat);
                    }
                    else
                    {
                        BDGrid.Rows[BDGrid.RowCount - 1].Cells[5].Value = ObjGlobal
                            .ReturnDecimal(ro["TermRate"].ToString()).ToString(ObjGlobal.SysAmountFormat);
                        BDGrid.Rows[BDGrid.RowCount - 1].Cells[6].Value = ObjGlobal
                            .ReturnDecimal(ro["TermAmt"].ToString()).ToString(ObjGlobal.SysAmountFormat);
                        if (ObjGlobal.ReturnDecimal(ro["TermAmt"].ToString()) != 0)
                        {
                            if (ro["Sign"].ToString() == "+")
                            {
                                termCalAmt += ObjGlobal.ReturnDouble(ro["TermAmt"].ToString());
                                tmpTermAmt += ObjGlobal.ReturnDouble(ro["TermAmt"].ToString());
                            }
                            else
                            {
                                termCalAmt -= ObjGlobal.ReturnDouble(ro["TermAmt"].ToString());
                                tmpTermAmt -= ObjGlobal.ReturnDouble(ro["TermAmt"].ToString());
                            }
                        }
                    }

                    BDGrid.Rows[BDGrid.RowCount - 1].Cells[7].Value = ro["Formula"].ToString();
                    BDGrid.Rows[BDGrid.RowCount - 1].Cells[8].Value = string.Empty;
                    BDGrid.Rows[BDGrid.RowCount - 1].Cells[9].Value = string.Empty;
                    BDGrid.Rows[BDGrid.RowCount - 1].Cells[10].Value = ro["OrderNo"].ToString();
                }

            lbl_Total.Text = tmpTermAmt.ToString("0.00");
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }
    }

    private void TermTotalCalc()
    {
        Total_Term = 0;
        var count = BDGrid.Rows.Count;
        if (BDGrid.Rows.Count > 0)
            for (var i = 0; i < count; i++)
            {
                if (Row_Vat == i || Free_Vat == i) continue;
                if (BDGrid.Rows[i].Cells[2].Value.ToString() == string.Empty ||
                    BDGrid.Rows[i].Cells[2].Value == null ||
                    BDGrid.Rows[i].Cells[6].Value.ToString() == string.Empty) continue;
                if (BDGrid.Rows[i].Cells[4].Value.ToString() == "+")
                    Total_Term += ObjGlobal.ReturnDecimal(BDGrid[6, i].Value.ToString());
                else
                    Total_Term -= ObjGlobal.ReturnDecimal(BDGrid[6, i].Value.ToString());
            }

        lbl_Total.Text = Total_Term.ToString("0.00");
    }

    private void TermCalculations(int strow = 0)
    {
        try
        {
            var i1 = 0;
            var ii = 0;
            var iii = 0;
            var Cro = 0;
            double TempAmt = 0;
            double CCAmt;
            double NetAmt = 0;
            double NetTot = 0;
            var dtCCTerm = new DataTable();
            var dtss = new DataTable();
            Cro = strow == 0 ? rowIndex : strow;
            for (i1 = Cro; i1 <= BDGrid.Rows.Count - 1; i1++)
            {
                if (Row_Vat == i1 || Free_Vat == i1) continue;
                dtTermMaster.Rows[0]["TermName"] = i1 == rowIndex
                    ? TxtTermDescription.Text
                    : BDGrid.Rows[rowIndex].Cells[2].Value.ToString();
                if (dtTermMaster.Rows.Count > 0) Term_Category = dtTermMaster.Rows[0]["TermType"].ToString();
                NetAmt = 0;
                if (BDGrid.Rows[i1].Cells[1].Value.ToString() != string.Empty &&
                    BDGrid.Rows[i1].Cells[3].Value.ToString() != "Quantity" &&
                    BDGrid.Rows[i1].Cells[3].Value.ToString() != "Round Off")
                {
                    if (BDGrid.Rows[i1].Cells[7].Value.ToString() != string.Empty) //Formula wise calc
                    {
                        NetAmt += BasicAmt;
                        for (iii = 0; iii <= i1 - 1; iii++)
                        {
                            if (dtTermMaster.Rows[0]["TermName"].ToString() !=
                                BDGrid.Rows[i1].Cells[2].Value.ToString()) continue;
                            if (rowIndex == iii && TxtTermRate.Visible)
                            {
                                if (BDGrid.Rows[iii].Cells[4].Value.ToString() == "+")
                                    NetAmt += ObjGlobal.ReturnDouble(TxtTermAmount.Text);
                                else
                                    NetAmt -= ObjGlobal.ReturnDouble(TxtTermAmount.Text);
                            }
                            else
                            {
                                if (BDGrid.Rows[iii].Cells[4].Value.ToString() == "+")
                                    NetAmt += ObjGlobal.ReturnDouble(BDGrid.Rows[iii].Cells[6].Value.ToString());
                                else
                                    NetAmt -= ObjGlobal.ReturnDouble(BDGrid.Rows[iii].Cells[6].Value.ToString());
                            }

                            break;
                        }
                    }
                    else
                    {
                        CCAmt = 0;
                        NetAmt = 0;
                        NetAmt = BasicAmt - CCAmt;
                        if (BDGrid.Rows[i1].Cells[9].Value.ToString() == string.Empty)
                            for (ii = 0; ii <= i1 - 1; ii++)
                            {
                                if (BDGrid.Rows[ii].Cells[9].Value.ToString() != string.Empty) continue;
                                if (rowIndex == ii && TxtTermRate.Visible)
                                {
                                    if (BDGrid.Rows[ii].Cells[4].Value.ToString() == "+")
                                        NetAmt += ObjGlobal.ReturnDouble(TxtTermAmount.Text);
                                    else
                                        NetAmt -= ObjGlobal.ReturnDouble(TxtTermAmount.Text);
                                }
                                else
                                {
                                    if (BDGrid.Rows[ii].Cells[4].Value.ToString() == "+")
                                        NetAmt += ObjGlobal.ReturnDouble(BDGrid.Rows[ii].Cells[6].Value.ToString());
                                    else
                                        NetAmt -= ObjGlobal.ReturnDouble(BDGrid.Rows[ii].Cells[6].Value.ToString());
                                }
                            }
                    }

                    if (i1 == rowIndex && TxtTermRate.Visible) //Convert.ToInt16(Grid.CurrentRow)
                    {
                        TxtTermAmount.Text = Term_Category == "F"
                            ? (TotFree * ProRate * ObjGlobal.ReturnDouble(TxtTermRate.Text) / 100).ToString(
                                ObjGlobal.SysAmountFormat)
                            : (Math.Abs(NetAmt) * ObjGlobal.ReturnDouble(TxtTermRate.Text) / 100).ToString(
                                ObjGlobal.SysAmountFormat);
                    }
                    else
                    {
                        if (Term_Category == "F") // For Free
                        {
                            BDGrid.Rows[i1].Cells[6].Value =
                                (TotFree * ProRate *
                                    ObjGlobal.ReturnDouble(BDGrid.Rows[i1].Cells[5].Value.ToString()) / 100)
                                .ToString("0.00");
                        }
                        else
                        {
                            if (ObjGlobal.ReturnDecimal(BDGrid.Rows[i1].Cells[5].Value.ToString()) != 0)
                                BDGrid.Rows[i1].Cells[6].Value =
                                    (Math.Abs(NetAmt) *
                                        ObjGlobal.ReturnDouble(BDGrid.Rows[i1].Cells[5].Value.ToString()) / 100)
                                    .ToString("0.00");
                        }
                    }
                }
                else if (BDGrid.Rows[i1].Cells[2].Value.ToString() != string.Empty &&
                         BDGrid.Rows[i1].Cells[3].Value.ToString() == "Round Off")
                {
                    for (ii = 0; ii <= i1 - 1; ii++)
                        if (BDGrid.Rows[ii].Cells[4].Value.ToString() == "+")
                        {
                            if (rowIndex == ii && TxtTermAmount.Visible)
                                NetAmt += ObjGlobal.ReturnDouble(TxtTermAmount.Text);
                            else
                                NetAmt += ObjGlobal.ReturnDouble(BDGrid.Rows[ii].Cells[6].Value.ToString());
                        }
                        else
                        {
                            if (rowIndex == ii && TxtTermAmount.Visible)
                                NetAmt -= ObjGlobal.ReturnDouble(TxtTermAmount.Text);
                            else
                                NetAmt -= ObjGlobal.ReturnDouble(BDGrid.Rows[ii].Cells[6].Value.ToString());
                        }

                    NetTot = NetAmt + ObjGlobal.ReturnDouble(lbl_BasicAmt.Text);
                    query = Module == "Purchase"
                        ? "Select PT_Id TermId, PT_Name TermName,'Value' Basis,PT_Sign Sign,PT_Rate Rate,0 Amount,'' Formula from AMS.PT_Term Where PT_Type = 'G' and PT_Basis = 'V' "
                        : "Select ST_Id TermId, ST_Name TermName,'Value' Basis,ST_Sign Sign,ST_Rate Rate,0 Amount,'' Formula from AMS.ST_Term Where ST_Type = 'G' and ST_Basis = 'V' ";
                    dtss.Reset();
                    dtss = GetConnection.SelectDataTableQuery(query);
                    if (dtss.Rows.Count == 1)
                    {
                        if (Convert.ToInt32(NetTot) - NetTot != 0)
                        {
                            if (BDGrid.Rows[i1].Cells[4].Value.ToString() == "+")
                            {
                                if (NetAmt != 0) TempAmt = Math.Abs(NetTot - (Convert.ToInt32(NetTot) + 1));
                            }
                            else
                            {
                                if (NetTot > 0) TempAmt = Math.Abs(Convert.ToInt32(NetTot) - NetTot);
                            }
                        }
                    }
                    else if (dtss.Rows.Count > 0)
                    {
                        if (Math.Abs(Convert.ToInt32(Math.Abs(NetTot)) - Math.Abs(NetTot)) > 0.5)
                        {
                            if (BDGrid.Rows[i1].Cells[4].Value.ToString() == "+")
                            {
                                if (NetTot > 0)
                                    TempAmt = Math.Abs(Math.Abs(NetTot) - Convert.ToInt32(Math.Abs(NetTot)) - 1);
                            }
                            else
                            {
                                if (NetTot < 0)
                                    TempAmt = Math.Abs(Math.Abs(NetTot) - Convert.ToInt32(Math.Abs(NetTot)) - 1);
                            }
                        }
                        else
                        {
                            if (BDGrid.Rows[i1].Cells[4].Value.ToString() == "+")
                            {
                                if (NetTot < 0)
                                    TempAmt = Math.Abs(Math.Abs(NetTot) - Convert.ToInt32(Math.Abs(NetTot)));
                            }
                            else
                            {
                                if (NetTot > 0)
                                    TempAmt = Math.Abs(Math.Abs(NetTot) - Convert.ToInt32(Math.Abs(NetTot)));
                            }
                        }
                    }

                    BDGrid.Rows[i1].Cells[6].Value = TempAmt.ToString("0.00");
                    TempAmt = 0;
                }
                else
                {
                    if (BDGrid.Rows[rowIndex].Cells[3].Value.ToString() != "Quantity") continue;
                    if (rowIndex != i1) continue;
                    if (TxtTermRate.Visible != true) continue;
                    TxtTermAmount.Text = Term_Category == "F"
                        ? (TotFree * ObjGlobal.ReturnDouble(TxtTermRate.Text)).ToString()
                        : (TotQty * ObjGlobal.ReturnDouble(TxtTermRate.Text)).ToString();
                }
            }

            Term_Category = string.Empty;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            MessageBox.Show(e.Message, ObjGlobal.Caption);
        }
    }

    #endregion --------------- Method ---------------

    #region --------------- Global  ---------------

    public int PSNo;
    private int Term_Id;
    private int systemVatId;
    private int Row_Vat;
    private int Free_Vat;
    private int rowIndex;
    private int currentColumn;

    public long ProSNo;

    private bool FocusModeof_Grd;
    private bool DataIsChanged;

    public string SelectQuery;
    public string Module;
    public string InvoiceType;
    public string Source;
    public string ProductId;
    public string TermType;
    public string Title;
    public string PTermId;
    public string Term_Category = string.Empty;
    private string query = string.Empty;
    public string BillType = "N";

    public double _NetAmount;
    public double BasicAmt;
    public double TotalTermAmt;
    public double TotQty;
    public double TotFree;
    public double ProRate;
    public double ProBasicamt;
    private decimal Total_Term;

    public DataTable dtpbt = new("Temp");
    private DataTable dt = new();
    private DataTable dtTermMaster = new();
    private readonly DataTable dtProTermDtl = new();
    private DataTable dtAbb = new();
    private DataTable _dtPwiseDetl;
    public DataTable DtPwiseDetl;
    public DataTable dtTerm;
    public Point _Location;

    #endregion --------------- Global  ---------------
}