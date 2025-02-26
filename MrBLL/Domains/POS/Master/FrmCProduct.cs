using MrBLL.Utility.Common;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Utility.PickList;
using MrDAL.Utility.Server;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MrBLL.Domains.POS.Master;

public partial class FrmCProduct : MrForm
{
    private void txt_ShortName_TextChanged(object sender, EventArgs e)
    {
    }

    #region --------------- MainEvent ---------------

    private readonly AutoCompleteStringCollection NCProductGroup = new();
    private readonly AutoCompleteStringCollection NCUnit = new();
    public string PCode;
    private string Query = string.Empty;
    private readonly string searchKey = string.Empty;
    private string Sql = string.Empty;
#pragma warning disable CS0414 // The field 'FrmCProduct.UID' is assigned but its value is never used
    private long UID = 0;
#pragma warning restore CS0414 // The field 'FrmCProduct.UID' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'FrmCProduct.GrpId' is assigned but its value is never used
    private long GrpId = 0;
#pragma warning restore CS0414 // The field 'FrmCProduct.GrpId' is assigned but its value is never used
    private bool RowsDelete;
#pragma warning disable CS0414 // The field 'FrmCProduct.GridRowUpdateMode' is assigned but its value is never used
    private bool GridRowUpdateMode;
#pragma warning restore CS0414 // The field 'FrmCProduct.GridRowUpdateMode' is assigned but its value is never used
    private DataTable dtTemp = new();
    private readonly ArrayList HeaderCap = new();
    private readonly ArrayList ColumnWidths = new();
    private StringWriter sw = new();

    public string ProductDetails = string.Empty;
    private readonly Button BtnProductGroup = new();
    private readonly Button BtnProductUnit = new();

    #endregion --------------- MainEvent ---------------

    #region --------------- Frm ---------------

    public FrmCProduct()
    {
        InitializeComponent();
        BtnProductGroup.Click += BtnProductGroup_Click;
        BtnProductUnit.Click += BtnProductUnit_Click;
    }

    private void FrmCounterProduct_Load(object sender, EventArgs e)
    {
        BackColor = ObjGlobal.FrmBackColor();
        ObjGlobal.DGridColorCombo(Grid);
        Clear();
        txt_ProductCode.Enabled = true;
        SearchGroup(string.Empty);
        SearchUnit(string.Empty);
        PnOpeningDetails.Visible = false;
        txt_ShortName.Focus();
    }

    #region AutoComplete

    private void SearchGroup(string Search)
    {
        var connection = new SqlConnection(GetConnection.ConnectionString);
        connection.Open();
        Query = "Select * From AMS.ProductGroup Order By GrpName Asc";
        var cmd = new SqlCommand
        {
            Connection = connection,
            CommandType = CommandType.Text,
            CommandText = Query
        };
        var rea = cmd.ExecuteReader();
        if (rea.HasRows)
        {
            while (rea.Read())
            {
                NCProductGroup.Add(rea["GrpName"].ToString());
            }
        }

        rea.Close();
        txt_ProductGroup.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        txt_ProductGroup.AutoCompleteSource = AutoCompleteSource.CustomSource;
        txt_ProductGroup.AutoCompleteCustomSource = NCProductGroup;
    }

    private void SearchUnit(string Search)
    {
        Query = "Select UnitCode from AMS.ProductUnit";
        var cmd = new SqlCommand
        {
            Connection = GetConnection.GetSqlConnection(),
            CommandType = CommandType.Text,
            CommandText = Query
        };
        var rea = cmd.ExecuteReader();
        GetConnection.GetSqlConnection().Close();
        if (rea.HasRows)
            while (rea.Read())
                NCUnit.Add(rea["UnitCode"].ToString());
        rea.Close();
        txt_Unit.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        txt_Unit.AutoCompleteSource = AutoCompleteSource.CustomSource;
        txt_Unit.AutoCompleteCustomSource = NCUnit;
    }

    #endregion AutoComplete

    private void FrmCounterProduct_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            Clear();
            Close();
        }
    }

    private void FrmCounterProduct_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void FrmCounterProduct_Resize(object sender, EventArgs e)
    {
        Grid.Size = new Size(Size.Width - 24, Size.Height - 158);
        groupBox1.Size = new Size(Size.Width - 20, Size.Height - 155);
    }

    #endregion --------------- Frm ---------------

    #region --------------- Master ---------------

    private void txt_ShortName_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_ShortName, 'E');
    }

    private void txt_ShortName_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_ShortName, 'L');
    }

    private void txt_ShortName_Validating(object sender, CancelEventArgs e)
    {
        if (string.IsNullOrEmpty(txt_ShortName.Text.Replace("'", "''")))
        {
        }
        else
        {
            BindSelectedProduct();
        }

        if (Tag.ToString() == "NEW")
            if (!string.IsNullOrEmpty(GetConnection.GetQueryData(
                    $"Select PShortName from AMS.Product where PShortName = '{txt_ShortName.Text.Trim().Replace("'", "''")}'")))
            {
                MessageBox.Show(@"Duplicate Bar Code can't Accepted.!!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txt_ShortName.Focus();
                return;
            }

        if (txt_ShortName.Text.Trim().Replace("'", "''") != string.Empty && Tag.ToString() == "EDIT")
            if (GetConnection.GetQueryData(
                    $"Select PShortName from AMS.Product Where PShortName='{txt_ShortName.Text.Trim().Replace("'", "''")}' and PID <> '{txt_ProductCode.Text.Trim().Replace("'", "''")}' ") !=
                string.Empty)
            {
                MessageBox.Show(@"Bar Code can not be Duplicate!!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txt_ShortName.Focus();
                e.Cancel = true;
            }
    }

    private void txt_ShortName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.Tab)
        {
            ClsPickList.PlValue1 = string.Empty;
            ClsPickList.PlValue2 = string.Empty;
            ClsPickList.PlValue3 = string.Empty;
            HeaderCap.Clear();
            ColumnWidths.Clear();
            if (ObjGlobal.StockShortNameWise)
            {
                HeaderCap.Add("Id");
                HeaderCap.Add("Code");
                HeaderCap.Add("Name");
                HeaderCap.Add("Type");
                HeaderCap.Add("Unit");
                HeaderCap.Add("Buy Rate");
                HeaderCap.Add("Sales Rate");
                HeaderCap.Add("Group");
                ColumnWidths.Add(0);
                ColumnWidths.Add(100);
                ColumnWidths.Add(200);
                ColumnWidths.Add(80);
                ColumnWidths.Add(70);
                ColumnWidths.Add(100);
                ColumnWidths.Add(100);
                ColumnWidths.Add(100);
                Query =
                    "Select PId, PShortName, PName, PType, UnitCode, PBuyRate, PSalesRate, GrpName, Convert(Decimal(18, 2), isnull(Sum(StockQty), 0)) StockQty From(SELECT P.PId, PShortName, PName, P.PType, PU.UnitCode, Convert(Decimal(18,2), PBuyRate) PBuyRate,Convert(Decimal(18, 2), PSalesRate) PSalesRate,PG.GrpName, ";
                Query =
                    $"{Query}Case When EntryType = 'I' Then isnull(Sum(StockQty),0) Else - isnull(Sum(StockQty), 0) End StockQty FROM AMS.Product as P Left Outer Join AMS.ProductUnit as PU On PU.UID = P.PUnit Left Outer Join AMS.ProductGroup as PG On PG.PGrpID = P.PGrpId left outer join AMS.StockDetails as SD on P.PID = SD.Product_Id Where(P.Status <> 0 or P.Status is Null) ";
                Query =
                    $"{Query}Group By P.PId,P.PName,P.PShortName,P.PType,PU.UnitCode,P.PBuyRate,P.PSalesRate,PG.GrpName,EntryType ) as aa Group By PId,PName,PShortName,PType,UnitCode,PBuyRate,PSalesRate,GrpName Order By PShortName ";
            }
            else
            {
                HeaderCap.Add("Id");
                HeaderCap.Add("Name");
                HeaderCap.Add("ShortName");
                HeaderCap.Add("Type");
                HeaderCap.Add("Unit");
                HeaderCap.Add("Buy Rate");
                HeaderCap.Add("Sales Rate");
                HeaderCap.Add("Group");
                ColumnWidths.Add(0);
                ColumnWidths.Add(200);
                ColumnWidths.Add(100);
                ColumnWidths.Add(80);
                ColumnWidths.Add(70);
                ColumnWidths.Add(100);
                ColumnWidths.Add(100);
                ColumnWidths.Add(100);

                Query =
                    "Select PId,  PName,PShortName, PType, UnitCode, PBuyRate, PSalesRate, GrpName, Convert(Decimal(18, 2), isnull(Sum(StockQty), 0)) StockQty From( SELECT P.PId, PShortName,Palias PName, P.PType, PU.UnitCode, Convert(Decimal(18,2), PBuyRate) PBuyRate,Convert(Decimal(18, 2), PSalesRate) PSalesRate,PG.GrpName, ";
                Query +=
                    "Case When EntryType = 'I' Then isnull(Sum(StockQty),0) Else - isnull(Sum(StockQty), 0) End StockQty FROM AMS.Product as P Left Outer Join AMS.ProductUnit as PU On PU.UID = P.PUnit Left Outer Join AMS.ProductGroup as PG On PG.PGrpID = P.PGrpId left outer join AMS.StockDetails as SD on P.PID = SD.Product_Id Where(P.Status <> 0 or P.Status is Null) ";
                Query +=
                    "Group By P.PId,P.Palias,P.PShortName,P.PType,PU.UnitCode,P.PBuyRate,P.PSalesRate,PG.GrpName,EntryType ) as aa Group By PId,PName,PShortName,PType,UnitCode,PBuyRate,PSalesRate,GrpName Order By PShortName ";
            }

            using var PkLst = new FrmPickList(Query, HeaderCap, ColumnWidths, "Product List", string.Empty);
            if (PkLst.ShowDialog() == DialogResult.OK)
                if (ClsPickList.PlValue1 != null && ClsPickList.PlValue2 != null &&
                    ClsPickList.PlValue1 != string.Empty && ClsPickList.PlValue2 != string.Empty)
                {
                    PCode = ClsPickList.PlValue1;
                    if (ObjGlobal.StockShortNameWise)
                    {
                        txt_ShortName.Text = ClsPickList.PlValue2;
                        txt_Product.Text = ClsPickList.PlValue3;
                    }
                    else
                    {
                        txt_Product.Text = ClsPickList.PlValue2;
                        txt_ShortName.Text = ClsPickList.PlValue3;
                    }

                    txt_ProductCode.Text = ClsPickList.PlValue1;
                    txt_ProductGroup.Text = ClsPickList.PlValue8;
                    txt_Unit.Text = ClsPickList.PlValue5;
                    Tag = "EDIT";
                    Text = "Counter Product [EDIT]";
                    txt_ShortName.Focus();
                }
        }
        else if (e.KeyCode is Keys.F2 or Keys.Tab)
        {
            txt_ShortName.Focus();
            var frm = new FrmSearchCProduct("CounterProduct", "Product List", searchKey,
                "('Inventory','Service','Counter')");
            frm.ShowDialog();
            if (frm.SelectList.Count > 0)
            {
                txt_ShortName.Text = frm.SelectList[0]["PShortName"].ToString().Trim();
                txt_Product.Text = frm.SelectList[0]["PName"].ToString().Trim();
                txt_ProductCode.Text = frm.SelectList[0]["PId"].ToString().Trim();
                PCode = frm.SelectList[0]["PId"].ToString().Trim();
                txt_ProductGroup.Text = frm.SelectList[0]["GrpName"].ToString().Trim();
                txt_Unit.Text = frm.SelectList[0]["UnitCode"].ToString().Trim();
                Tag = "EDIT";
                Text = @"COUNTER PRODUCT [EDIT]";
                txt_ShortName.Focus();
            }

            frm.Dispose();
        }

        if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void txt_Product_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_Product, 'E');
    }

    private void txt_Product_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_Product, 'L');
    }

    private void txt_Product_Validating(object sender, CancelEventArgs e)
    {
        if (Tag.ToString() == string.Empty || ActiveControl == txt_Product) return;

        if (string.IsNullOrEmpty(txt_Product.Text.Trim().Replace("'", "''")))
        {
            MessageBox.Show(@"Empty Field can't Accepted.", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            txt_Product.Focus();
            return;
        }

        if (Tag.ToString() == "NEW")
            if (!string.IsNullOrEmpty(GetConnection.GetQueryData(
                    $"Select PName from AMS.Product where PName = '{txt_Product.Text.Trim().Replace("'", "''")}'")))
            {
                MessageBox.Show(@"Duplicate Product can't Accepted.", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txt_Product.Focus();
                return;
            }

        if (txt_Product.Text.Trim().Replace("'", "''") != string.Empty && Tag.ToString() == "EDIT")
            if (GetConnection.GetQueryData(
                    $"Select PName from AMS.Product Where PName='{txt_Product.Text.Trim().Replace("'", "''")}' and PId <> '{PCode}' ") !=
                string.Empty)
            {
                MessageBox.Show(@"Name can not be Duplicate", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txt_Product.Focus();
                e.Cancel = true;
            }
    }

    private void txt_Product_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void txt_ProductGroup_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_ProductGroup, 'E');
    }

    private void txt_ProductGroup_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_ProductGroup, 'L');
        if (txt_ProductGroup.Text.Trim().Replace("'", "''") == string.Empty || Tag.ToString() != "NEW") return;
        try
        {
            var cc = string.Empty;
            var Pc = string.Empty;
            double CC1 = 0;
            double cc2 = 0;
            double i = 0;
            var AA = string.Empty;
            var bb = string.Empty;
            var dtPG = new DataTable();
            dtPG = GetConnection.SelectDataTableQuery(
                $"Select PGrpId from AMS.ProductGroup where GrpName='{txt_ProductGroup.Text.Trim().Replace("'", "''")}'");
            if (dtPG.Rows.Count <= 0) return;
            bb = dtPG.Rows[0]["PGrpId"].ToString();
            var dtProd = new DataTable();
            dtProd = GetConnection.SelectDataTableQuery(
                $"Select top(1) PID from AMS.Product as P Left Outer join AMS.ProductGroup as PG on PG.PGrpID=P.PGrpId where PG.GrpName='{txt_ProductGroup.Text.Trim().Replace("'", "''")}' and ISNUMERIC(right(PID,Len(PID)-Len(P.PGrpId)))=1 Order by Convert(numeric,right(PID,Len(PID)-Len(P.PGrpId))) Desc");
            if (dtProd.Rows.Count > 0)
            {
                cc = Convert.ToInt32(dtProd.Rows[0]["PID"]).ToString();
                cc = dtProd.Rows[0]["PID"].ToString().Substring(bb.Length, cc.Length - bb.Length);
                cc = (Convert.ToInt32(cc) + 1).ToString();
                AA = cc;
                CC1 = AA.Length;
                if (5 - CC1 > 0) cc2 = 5 - CC1;
                for (i = 1; i <= cc2; i++) Pc += "0";
                Pc += cc;
            }
            else
            {
                Pc = "00001";
            }

            txt_ProductCode.Text = dtPG.Rows[0]["PGrpId"] + Pc;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.AbortRetryIgnore,
                MessageBoxIcon.Error);
        }
    }

    private void txt_ProductGroup_Validating(object sender, CancelEventArgs e)
    {
        if (Tag.ToString() == string.Empty || ActiveControl == txt_ProductGroup) return;

        if (string.IsNullOrEmpty(txt_ProductGroup.Text.Replace("'", "''")))
        {
            MessageBox.Show(@"Product Group is required.", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            txt_ProductGroup.Focus();
            return;
        }

        if (!string.IsNullOrEmpty(txt_ProductGroup.Text.Replace("'", "''")))
            if (string.IsNullOrEmpty(GetConnection.GetQueryData(
                    $"Select GrpName from AMS.ProductGroup where GrpName='{txt_ProductGroup.Text.Replace("'", "''")}'")))
            {
                MessageBox.Show(@"Product Group not found.", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                txt_ProductGroup.Focus();
            }
    }

    private void BtnProductUnit_Click(object sender, EventArgs e)
    {
        ClsPickList.PlValue1 = string.Empty;
        ClsPickList.PlValue2 = string.Empty;
        ClsPickList.PlValue3 = string.Empty;

        HeaderCap.Clear();
        ColumnWidths.Clear();

        HeaderCap.Add("Id");
        HeaderCap.Add("Description");
        HeaderCap.Add("ShortName");

        ColumnWidths.Add(0);
        ColumnWidths.Add(120);
        ColumnWidths.Add(120);

        Query = "Select UID,UnitName,UnitCode From AMS.ProductUnit where (Status=1 or Status is null)";

        var frmPickList =
            new FrmPickList(Query, HeaderCap, ColumnWidths, "Product Unit List", ObjGlobal.SearchText);
        if (frmPickList.ShowDialog() != DialogResult.OK) return;
        if (ClsPickList.PlValue1 != null && ClsPickList.PlValue3 != null &&
            ClsPickList.PlValue1 != string.Empty && ClsPickList.PlValue3 != string.Empty)
            txt_Unit.Text = ClsPickList.PlValue3;
    }

    private void BtnProductGroup_Click(object sender, EventArgs e)
    {
        ClsPickList.PlValue1 = string.Empty;
        ClsPickList.PlValue2 = string.Empty;
        ClsPickList.PlValue3 = string.Empty;

        HeaderCap.Clear();
        ColumnWidths.Clear();

        HeaderCap.Add("Id");
        HeaderCap.Add("Description");
        HeaderCap.Add("ShortName");
        HeaderCap.Add("Margin");

        ColumnWidths.Add(0);
        ColumnWidths.Add(250);
        ColumnWidths.Add(100);
        ColumnWidths.Add(100);
        ColumnWidths.Add(0);

        Query = "Select PGrpID,GrpName,GrpCode,Convert(Decimal(18,2),GMargin) Margin From AMS.ProductGroup";

        var frmPickList =
            new FrmPickList(Query, HeaderCap, ColumnWidths, "Product Group List", ObjGlobal.SearchText);
        if (frmPickList.ShowDialog() != DialogResult.OK) return;
        if (ClsPickList.PlValue1 == null || ClsPickList.PlValue3 == null || ClsPickList.PlValue1 == string.Empty ||
            ClsPickList.PlValue3 == string.Empty) return;
        txt_ProductGroup.Text = ClsPickList.PlValue2;
        txt_Margin.Text = ClsPickList.PlValue4;
        txt_ProductGroup.Focus();
    }

    private void txt_ProductGroup_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (string.IsNullOrEmpty(txt_ProductGroup.Text))
                BtnProductGroup.PerformClick();
            else
                SendKeys.Send("{TAB}");
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), txt_ProductGroup, BtnProductGroup);
        }
    }

    private void txt_ProductCode_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_ProductCode, 'E');
    }

    private void txt_ProductCode_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void txt_ProductCode_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !int.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void txt_ProductCode_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_ProductCode, 'L');
        if (txt_ShortName.Text == string.Empty) txt_ShortName.Text = txt_ProductCode.Text;
    }

    private void txt_ProductCode_Validating(object sender, CancelEventArgs e)
    {
        if (Tag.ToString() == string.Empty || ActiveControl == txt_ProductCode) return;

        if (string.IsNullOrEmpty(txt_ProductCode.Text.Trim().Replace("'", "''")))
        {
            MessageBox.Show(@"Empty Field can't Accepted.", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            txt_ProductCode.Focus();
            return;
        }

        if (Tag.ToString() == "NEW")
            if (!string.IsNullOrEmpty(GetConnection.GetQueryData(
                    $"Select PId from AMS.Product where PId = '{txt_ProductCode.Text.Trim().Replace("'", "''")}'")))
            {
                MessageBox.Show(@"Duplicate Product Code can't Accepted.!!!!", ObjGlobal.Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_ProductCode.Focus();
                return;
            }

        if (txt_ProductCode.Text.Trim().Replace("'", "''") == string.Empty || Tag.ToString() != "EDIT") return;
        if (GetConnection.GetQueryData(
                $"Select PId from AMS.Product Where PId='{txt_ProductCode.Text.Trim().Replace("'", "''")}' and PId <> '{PCode}' ") !=
            string.Empty)
        {
            MessageBox.Show(@"Product Code Can not be Duplicate..!!", ObjGlobal.Caption);
            txt_ProductCode.Focus();
            e.Cancel = true;
        }
    }

    private void txt_Unit_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_Unit, 'E');
    }

    private void txt_Unit_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_Unit, 'L');
    }

    private void txt_Unit_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
            SendKeys.Send("{TAB}");
        else
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), txt_Unit, BtnProductUnit);
    }

    private void txt_Unit_Validating(object sender, CancelEventArgs e)
    {
        if (string.IsNullOrEmpty(txt_Unit.Text.Replace("'", "''"))) return;
        if (!string.IsNullOrEmpty(GetConnection.GetQueryData(
                $"Select UnitCode from AMS.ProductUnit where UnitCode ='{txt_Unit.Text.Replace("'", "''")}'"))) return;
        MessageBox.Show(@"Product Unit not found..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
            MessageBoxIcon.Information);
        txt_Unit.Focus();
    }

    private void Txt_Vat_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_Vat, 'E');
    }

    private void Txt_Vat_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_Vat, 'L');
        decimal rateAmt = 0;
        rateAmt = Math.Round(
            txt_PurchaseRate.GetDecimal() + txt_PurchaseRate.GetDecimal() * txt_Margin.GetDecimal() / 100, 0,
            MidpointRounding.AwayFromZero);
        txt_SalesRate.Text =
            (rateAmt + Convert.ToDecimal(rateAmt) * Convert.ToDecimal(ObjGlobal.ReturnDecimal(txt_Vat.Text)) / 100)
            .ToString("0.00");
    }

    private void txt_Vat_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void txt_Vat_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void txt_PurchaseRate_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_PurchaseRate, 'E');
    }

    private void txt_PurchaseRate_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_PurchaseRate, 'L');
    }

    private void TxtPurchaseRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
        if (e.KeyChar == (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void Txt_PurchaseRate_Validating(object sender, CancelEventArgs e)
    {
        decimal result = 0;
        decimal.TryParse(txt_PurchaseRate.Text, out var buyRate);
        result = buyRate;
        if (result == 0)
        {
            MessageBox.Show(@"PURCHASE RATE CANNOT LEFT BLANK OR ZERO..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            e.Cancel = true;
            txt_PurchaseRate.Clear();
        }
        else
        {
            decimal rateAmt = 0;
            rateAmt = Math.Round(
                (txt_PurchaseRate.GetDecimal() + txt_PurchaseRate.GetDecimal()) * txt_Margin.GetDecimal() / 100, 0,
                MidpointRounding.AwayFromZero);
            txt_SalesRate.Text =
                (rateAmt + rateAmt * txt_Vat.GetDecimal() / 100).ToString(ObjGlobal.SysAmountFormat);
        }
    }

    private void TxtMargin_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_Margin, 'E');
    }

    private void txt_Margin_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_Margin, 'L');
    }

    private void TxtMargin_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
        if (e.KeyChar == (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void TxtMargin_Validating(object sender, CancelEventArgs e)
    {
        decimal.TryParse(txt_Margin.Text, out var margin);
        if (margin == 0)
        {
            MessageBox.Show(@"MARGIN RATE CANNOT LEFT BLANK OR ZERO", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            e.Cancel = true;
            txt_Margin.Clear();
        }
        else
        {
            decimal rateAmt = 0;
            rateAmt = Math.Round(
                (txt_PurchaseRate.GetDecimal() + txt_PurchaseRate.GetDecimal()) * txt_Margin.GetDecimal() / 100, 0,
                MidpointRounding.AwayFromZero);
            txt_SalesRate.Text = Math.Round(rateAmt + rateAmt * txt_Vat.GetDecimal() / 100)
                .ToString(ObjGlobal.SysAmountFormat);
        }
    }

    private void txt_SalesRate_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_SalesRate, 'E');
    }

    private void txt_SalesRate_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_SalesRate, 'E');
    }

    private void txt_SalesRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
        if (e.KeyChar == (char)Keys.Enter) btnSave.Focus();

        //SendKeys.Send("{TAB}");
        if (e.KeyChar == (char)Keys.F2)
        {
            PnOpeningDetails.Visible = true;
            txtOpeningStock.Focus();
        }
    }

    private void txt_SalesRate_Validating(object sender, CancelEventArgs e)
    {
        decimal Margin = 0;
        decimal Sales = 0;
        decimal Vat = 0;
        decimal Taxable = 0;

        Sales = Convert.ToDecimal(ObjGlobal.ReturnDecimal(txt_SalesRate.Text));
        Vat = Convert.ToDecimal(ObjGlobal.ReturnDecimal(txt_Vat.Text)) + 100;
        if (Sales == 0)
        {
            MessageBox.Show(@"Sales Rate Cannot Be Left Blank or Zero", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            e.Cancel = true;
            txt_SalesRate.Clear();
        }
        else
        {
            Taxable = Sales / Vat * 100;
            Margin = Math.Round(Sales - Taxable, 0, MidpointRounding.AwayFromZero);
            txt_Margin.Text =
                (Convert.ToDecimal(Margin) * 100 / Convert.ToDecimal(ObjGlobal.ReturnDecimal(txt_SalesRate.Text)))
                .ToString("0.00");
        }
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (txt_Product.Text.Trim() == string.Empty)
        {
            MessageBox.Show("Product Name Can Not Left Blank");
            txt_Product.Focus();
            return;
        }

        if (txt_ShortName.Text.Trim() == string.Empty)
        {
            MessageBox.Show("Product Code Can Not Left Blank");
            txt_ShortName.Focus();
        }

        if (Tag.ToString() == "NEW")
        {
            if (!string.IsNullOrEmpty(GetConnection.GetQueryData(
                    $"Select PShortName from AMS.Product  where PShortName = '{txt_ShortName.Text.Trim()}'")))
            {
                MessageBox.Show(@"Duplicate Bar Code can't Accepted.", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txt_ShortName.Focus();
                return;
            }

            if (!string.IsNullOrEmpty(GetConnection.GetQueryData(
                    $"Select PName from AMS.Product Where PName = '{txt_Product.Text.Trim()}'")))
            {
                MessageBox.Show(@"Duplicate Product Name can't Accepted.", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txt_Product.Focus();
                return;
            }

            if (!string.IsNullOrEmpty(GetConnection.GetQueryData(
                    $"Select PID from AMS.Product Where PID = '{txt_ProductCode.Text.Trim()}'")))
            {
                MessageBox.Show(@"Duplicate Product Code can't Accepted.", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txt_ProductCode.Focus();
                return;
            }
        }

        if (Tag.ToString() == "EDIT")
        {
            if (GetConnection.GetQueryData(
                    $"Select PShortName from AMS.Product Where PShortName='{txt_ShortName.Text.Trim()}' and PID <> '{txt_ProductCode.Text.Trim()}' ") !=
                string.Empty)
            {
                MessageBox.Show(@"Bar Code can not be Duplicate", ObjGlobal.Caption);
                txt_ShortName.Focus();
                return;
            }

            if (GetConnection.GetQueryData(
                    $"Select PName from AMS.Product Where PName='{txt_Product.Text.Trim()}' and PID <> '{txt_ProductCode.Text.Trim()}' ") !=
                string.Empty)
            {
                MessageBox.Show(@"Product Name can not be Duplicate", ObjGlobal.Caption);
                txt_Product.Focus();
                return;
            }

            if (GetConnection.GetQueryData(
                    $"Select PID from AMS.Product Where PID='{txt_ProductCode.Text.Trim()}' and PID <> '{txt_ProductCode.Text.Trim()}' ") !=
                string.Empty)
            {
                MessageBox.Show(@"Product Code can not be Duplicate", ObjGlobal.Caption);
                txt_ProductCode.Focus();
                return;
            }
        }

        IUDProduct();
        PnOpeningDetails.Visible = false;
    }

    #endregion --------------- Master ---------------

    #region --------------- Grid ---------------

    private int currentColumn;
    private int rowIndex;

    private void Grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.ColumnIndex == 13) //For Edit
            if (Grid[1, e.RowIndex].Value != null)
            {
                txt_ShortName.Text = Grid.Rows[e.RowIndex].Cells["dgv_BarCode"].Value.ToString();
                txt_Product.Text = Grid.Rows[e.RowIndex].Cells["dgv_Description"].Value.ToString();
                txt_ProductGroup.Text = Grid.Rows[e.RowIndex].Cells["dgv_Group"].Value.ToString();
                txt_ProductCode.Text = Grid.Rows[e.RowIndex].Cells["dgv_ShortName"].Value.ToString();
                PCode = Grid.Rows[e.RowIndex].Cells["dgv_ShortName"].Value.ToString();

                txt_Unit.Text = Grid.Rows[e.RowIndex].Cells["dgv_Unit"].Value.ToString();
                txt_Vat.Text = Grid.Rows[e.RowIndex].Cells["dgv_Vat"].Value.ToString();
                txt_PurchaseRate.Text = Grid.Rows[e.RowIndex].Cells["dgv_BuyRate"].Value.ToString();
                txt_Margin.Text = Grid.Rows[e.RowIndex].Cells["dgv_Margin"].Value.ToString();
                txt_SalesRate.Text = Grid.Rows[e.RowIndex].Cells["dgv_SaleRate"].Value.ToString();
                Grid.Rows.RemoveAt(e.RowIndex);

                Grid.CurrentCell = Grid[2, e.RowIndex];
                Tag = "UPDATE";
                Text = "Counter Product [UPDATE]";
                txt_Unit.Focus();
                GridRowUpdateMode = true;
                EnableDisable(false);
            }

        if (e.ColumnIndex == 14) //Delete
            if (Grid[1, e.RowIndex].Value != null)
                if (MessageBox.Show(@"Are you sure you want to delete this row?", "Delete confirmation",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Grid.Rows.RemoveAt(e.RowIndex);
                    for (var i = 0; i < Grid.Rows.Count; i++) Grid.Rows[i].Cells[0].Value = i + 1;
                }
    }

    private void Grid_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
        currentColumn = e.ColumnIndex;
    }

    private void Grid_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
        rowIndex = e.RowIndex;
    }

    private void Grid_KeyDown(object sender, KeyEventArgs e)
    {
        try
        {
            if (e.KeyCode == Keys.Enter)
            {
                Grid.Rows[rowIndex].Selected = true;

                e.SuppressKeyPress = true;
                if (Grid.Rows[rowIndex].Selected)
                {
                    txt_ShortName.Text = Grid.Rows[rowIndex].Cells["dgv_BarCode"].Value.ToString();
                    txt_Product.Text = Grid.Rows[rowIndex].Cells["dgv_Description"].Value.ToString();
                    txt_ProductGroup.Text = Grid.Rows[rowIndex].Cells["dgv_Group"].Value.ToString();
                    txt_ProductCode.Text = Grid.Rows[rowIndex].Cells["dgv_ShortName"].Value.ToString();
                    PCode = Grid.Rows[rowIndex].Cells["dgv_ShortName"].Value.ToString();

                    txt_Unit.Text = Grid.Rows[rowIndex].Cells["dgv_Unit"].Value.ToString();
                    txt_Vat.Text = Grid.Rows[rowIndex].Cells["dgv_Vat"].Value.ToString();
                    txt_PurchaseRate.Text = Grid.Rows[rowIndex].Cells["dgv_BuyRate"].Value.ToString();
                    txt_Margin.Text = Grid.Rows[rowIndex].Cells["dgv_Margin"].Value.ToString();
                    txt_SalesRate.Text = Grid.Rows[rowIndex].Cells["dgv_SaleRate"].Value.ToString();
                    Grid.Rows.RemoveAt(rowIndex);
                    if (Grid.Rows.Count > 0) Grid.CurrentCell = Grid[2, rowIndex];
                    Tag = "UPDATE";
                    Text = "Counter Product [UPDATE]";
                    txt_Unit.Focus();
                    GridRowUpdateMode = true;
                    EnableDisable(false);
                }
            }

            if (e.KeyCode == Keys.Delete)
            {
                if (Grid.CurrentCell.RowIndex == 0)
                {
                    //Do Nothing
                }
                else
                {
                    foreach (DataGridViewCell oneCell in Grid.SelectedCells)
                        if (oneCell.Selected)
                            if (MessageBox.Show(@"Are you sure you want to delete this row?", "Delete confirmation",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                Grid.Rows.RemoveAt(oneCell.RowIndex);
                                for (var i = 0; i < Grid.Rows.Count; i++) Grid.Rows[i].Cells[0].Value = i + 1;
                            }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.AbortRetryIgnore,
                MessageBoxIcon.Error);
        }
    }

    private void Grid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        try
        {
            txt_ShortName.Text = Grid.Rows[e.RowIndex].Cells["dgv_BarCode"].Value.ToString();
            txt_Product.Text = Grid.Rows[e.RowIndex].Cells["dgv_Description"].Value.ToString();
            txt_ProductGroup.Text = Grid.Rows[e.RowIndex].Cells["dgv_Group"].Value.ToString();
            txt_ProductCode.Text = Grid.Rows[e.RowIndex].Cells["dgv_ShortName"].Value.ToString();
            PCode = Grid.Rows[e.RowIndex].Cells["dgv_ShortName"].Value.ToString();

            txt_Unit.Text = Grid.Rows[e.RowIndex].Cells["dgv_Unit"].Value.ToString();
            txt_Vat.Text = Grid.Rows[e.RowIndex].Cells["dgv_Vat"].Value.ToString();
            txt_PurchaseRate.Text = Grid.Rows[e.RowIndex].Cells["dgv_BuyRate"].Value.ToString();
            txt_Margin.Text = Grid.Rows[e.RowIndex].Cells["dgv_Margin"].Value.ToString();
            txt_SalesRate.Text = Grid.Rows[e.RowIndex].Cells["dgv_SaleRate"].Value.ToString();
            Grid.Rows.RemoveAt(e.RowIndex);
            if (Grid.Rows.Count > 0) Grid.CurrentCell = Grid[2, e.RowIndex];

            Tag = "UPDATE";
            Text = "Counter Product [UPDATE]";
            txt_Unit.Focus();
            GridRowUpdateMode = true;
            EnableDisable(false);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.AbortRetryIgnore,
                MessageBoxIcon.Error);
        }
    }

    private void Grid_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
        if (RowsDelete)
            for (var i = 0; i < Grid.Rows.Count; i++)
                Grid.Rows[i].Cells[0].Value = i + 1;
    }

    private void Grid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
    {
        if (!e.Row.IsNewRow)
        {
            if (MessageBox.Show(@"Are you sure you want to delete this row?", "Delete confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                e.Cancel = true;
            else
                RowsDelete = true;
        }
    }

    #endregion --------------- Grid ---------------

    #region --------------- Method ---------------

    private void IUDProduct()
    {
        try
        {
            Query = string.Empty;
            if (Tag.ToString() == "NEW")
            {
                Query =
                    " Insert Into AMS.Product (PId, PName,PALias,PShortName,PType,PCategory,PUnit,PAltUnit,PQtyConv,PAltConv,PValTech,PSerialno,PSizewise,PBatchwise,PBuYRate,PSalesRate, ";
                Query =
                    $"{Query} PMargin1,TradeRate,PMargin2,PMRP,PGrpID,PSubGrpID,PTax,PMin,PMax,CmpId,Branch_Id,CmpUnit_Id,PPL,PPR,PSL,PSR,EnterBy,EnterDate,Status)";
                Query = $"{Query}  Select  ";
                Query = $"{Query}  '{txt_ProductCode.Text.Trim()}',";
                Query = ObjGlobal.StockShortNameWise
                    ? $"{Query}  '{txt_ProductCode.Text.Trim()}',"
                    : $"{Query}  '{txt_Product.Text.Trim()}',";
                Query = $"{Query}  '{txt_Product.Text.Trim()}',";
                Query = $"{Query}  '{txt_ShortName.Text.Trim()}',";
                Query = $"{Query}  'Inventory',";
                Query = $"{Query}  'Finished Goods',";
                Query = txt_Unit.Text.Trim() != string.Empty
                    ? $"{Query} (Select UId from AMS.ProductUnit where UnitCode= '{txt_Unit.Text.Trim()}'),"
                    : $"{Query} Null,";
                Query = $"{Query} Null,";
                Query = $"{Query} Null,";
                Query = $"{Query}  Null,";
                Query = $"{Query}  'FIFO',";
                Query = $"{Query} 0,";
                Query = $"{Query} 0,";
                Query = $"{Query} 0,";
                Query = $"{Query} '{Convert.ToDecimal(ObjGlobal.ReturnDecimal(txt_PurchaseRate.Text))}',";
                Query = $"{Query} '{Convert.ToDecimal(ObjGlobal.ReturnDecimal(txt_SalesRate.Text))}',";
                Query = $"{Query} '{Convert.ToDecimal(ObjGlobal.ReturnDecimal(txt_Margin.Text))}',";
                Query = $"{Query} '0',";
                Query = $"{Query} '0',";
                Query = $"{Query} '0',";
                Query = txt_ProductGroup.Text.Trim() != string.Empty
                    ? $"{Query} (Select PGrpId from AMS.ProductGroup where GrpName= '{txt_ProductGroup.Text.Trim()}'),"
                    : $"{Query} Null,";
                Query = $"{Query} Null,";
                Query = $"{Query}  {Convert.ToDecimal(ObjGlobal.ReturnDecimal(txt_Vat.Text))},";
                Query = $"{Query}  0 ,";
                Query = $"{Query}  0 ,";
                Query = $"{Query} Null,";
                Query = $"{Query}  {ObjGlobal.SysBranchId}, ";
                Query = ObjGlobal.SysCompanyUnitId != null
                    ? $"{Query}  {ObjGlobal.SysCompanyUnitId},"
                    : $"{Query} null,";
                Query = $"{Query} Null,";
                Query = $"{Query} Null,";
                Query = $"{Query} Null,";
                Query = $"{Query} Null,";
                Query = $"{Query} '{ObjGlobal.LogInUser}',";
                Query = $"{Query} '{DateTime.Now:yyyy-MM-d} {DateTime.Now.ToShortTimeString()}',";
                Query = $"{Query} 1";
            }
            else if (Tag.ToString() == "UPDATE")
            {
                Query = "  Update AMS.Product set ";
                Query = $"{Query}  PName = '{txt_Product.Text.Trim()}',";
                Query = $"{Query}  PAlias ='{txt_Product.Text.Trim()}',";
                Query = $"{Query}  PShortName ='{txt_ShortName.Text.Trim()}',";
                Query = txt_Unit.Text.Trim() != string.Empty
                    ? $"{Query} PUnit = (Select UId from AMS.ProductUnit where UnitCode= '{txt_Unit.Text.Trim()}'),"
                    : $"{Query} PUnit = Null,";
                Query =
                    $"{Query} PBuyRate = '{Convert.ToDecimal(ObjGlobal.ReturnDecimal(txt_PurchaseRate.Text))}',";
                Query = $"{Query} PSalesRate = '{Convert.ToDecimal(ObjGlobal.ReturnDecimal(txt_SalesRate.Text))}',";
                Query = $"{Query} PMargin1 = '{Convert.ToDecimal(ObjGlobal.ReturnDecimal(txt_Margin.Text))}',";
                Query = txt_ProductGroup.Text.Trim() != string.Empty
                    ? $"{Query} PGrpId= (Select PGrpId from AMS.ProductGroup where GrpName= '{txt_ProductGroup.Text}'),"
                    : $"{Query} PGrpId=Null,";
                Query = $"{Query} PTax = '{Convert.ToDecimal(ObjGlobal.ReturnDecimal(txt_Vat.Text))}',";
                Query = $"{Query} EnterBy = '{ObjGlobal.LogInUser}',";
                Query = $"{Query} EnterDate = '{DateTime.Now:yyyy-MM-d} {DateTime.Now.ToShortTimeString()}'";
                Query = $"{Query} where PID = '{txt_ProductCode.Text.Trim()}' ";
            }
            else if (Tag.ToString() == "DELETE")
            {
                Query = $"{Query} Delete from AMS.Product where PID = '{txt_ProductCode.Text.Trim()}'";
            }

            var cmd = new SqlCommand(Query, GetConnection.GetSqlConnection());
            if (cmd.ExecuteNonQuery() > 0)
            {
                if (Tag.ToString() == "NEW")
                {
                    MessageBox.Show(@"Data Saved Sucessfully", ObjGlobal.Caption);
                    ProductDetails = txt_Product.Text.Trim();
                    if (Convert.ToDecimal(ObjGlobal.ReturnDecimal(txtOpeningStock.Text)) > 0) IUDOpeningDetails();
                }
                else if (Tag.ToString() == "UPDATE")
                {
                    MessageBox.Show(@"Data Updated Sucessfully", ObjGlobal.Caption);
                }
                else if (Tag.ToString() == "DELETE")
                {
                    MessageBox.Show(@"Data Deleted Sucessfully", ObjGlobal.Caption);
                }

                BindSelectedProduct();
                Clear();
                txt_ShortName.Focus();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
        }
    }

    private void IUDOpeningDetails()
    {
    }

    private void Clear()
    {
        txt_ShortName.Text = string.Empty;
        txt_Product.Text = string.Empty;
        txt_ProductCode.Text = string.Empty;
        txt_ProductGroup.Text = string.Empty;
        txt_Unit.Text = string.Empty;
        txt_Vat.Text = "13.0";
        txt_PurchaseRate.Text = string.Empty;
        txt_SalesRate.Text = string.Empty;
        txt_Margin.Text = string.Empty;
        Tag = "NEW";
        Text = "Counter Product [NEW]";
        EnableDisable(true);
        ObjGlobal.DGridColorCombo(Grid);
    }

    private void EnableDisable(bool bt)
    {
        txt_ShortName.Enabled = bt;
        txt_Product.Enabled = bt;
        txt_ProductGroup.Enabled = bt;
        txt_ProductCode.Enabled = bt;
    }

    private void BindSelectedProduct()
    {
        try
        {
            Query = string.Empty;
            Query =
                "Select ROW_NUMBER() Over(Order by PName) SNo, PShortName,PName,PID,PG.PGrpID, PG.GrpCode,PG.GrpName,PUnit,UnitCode,Convert(Decimal(18,2),PTax) Vat,Convert(Decimal(18,2),P.PBuyRate) BuyRate,Convert(Decimal(18,2),P.PMargin1) Margin,Convert(Decimal(18,2),PSalesRate) as SalesRate From AMS.Product as P Left Outer Join AMS.ProductGroup as PG On PG.PGrpID=P.PGrpId left outer join AMS.ProductUnit as PU on  P.PUnit=PU.UID ";
            Query = $"{Query} Where PShortName ='{txt_ShortName.Text}' ";
            dtTemp.Reset();
            dtTemp = GetConnection.SelectDataTableQuery(Query);
            if (dtTemp.Rows.Count > 0)
            {
                txt_Product.Text = dtTemp.Rows[0]["PName"].ToString();
                txt_ProductCode.Text = dtTemp.Rows[0]["PID"].ToString();
                PCode = dtTemp.Rows[0]["PID"].ToString();
                txt_ProductGroup.Text = dtTemp.Rows[0]["GrpName"].ToString();
                txt_Unit.Text = dtTemp.Rows[0]["UnitCode"].ToString();
                txt_Vat.Text = Convert.ToDecimal(ObjGlobal.ReturnDecimal(dtTemp.Rows[0]["Vat"].ToString()))
                    .ToString(ObjGlobal.SysAmountFormat);
                txt_PurchaseRate.Text = Convert
                    .ToDecimal(ObjGlobal.ReturnDecimal(dtTemp.Rows[0]["BuyRate"].ToString()))
                    .ToString(ObjGlobal.SysAmountFormat);
                txt_Margin.Text =
                    Math.Round(Convert.ToDecimal(ObjGlobal.ReturnDecimal(dtTemp.Rows[0]["Margin"].ToString())))
                        .ToString(ObjGlobal.SysAmountFormat);
                txt_SalesRate.Text =
                    Math.Round(Convert.ToDecimal(ObjGlobal.ReturnDecimal(dtTemp.Rows[0]["SalesRate"].ToString())))
                        .ToString(ObjGlobal.SysAmountFormat);
                Tag = "UPDATE";
                Text = "Counter Product [UPDATE]";
                for (var i = 0; i < Grid.Rows.Count; i++)
                    if (Grid[6, i].Value != null)
                        if (dtTemp.Rows[0]["PID"].ToString() == Grid[6, i].Value.ToString())
                            Grid.Rows.RemoveAt(i);
                foreach (DataGridViewRow gro in Grid.Rows) gro.Cells["dgv_SNo"].Value = Grid.Rows.IndexOf(gro) + 1;
                for (var i = 0; i < dtTemp.Rows.Count; i++)
                {
                    Grid.Rows.Add();
                    if (Grid.Rows.Count == 1)
                        Grid[0, Grid.Rows.Count - 1].Value = Grid.Rows.Count.ToString();
                    else
                        Grid[0, Grid.Rows.Count - 1].Value = (Grid.Rows.Count - 1).ToString();
                    Grid[1, Grid.Rows.Count - 1].Value = dtTemp.Rows[i]["PId"].ToString();
                    Grid[2, Grid.Rows.Count - 1].Value = dtTemp.Rows[i]["PShortName"].ToString();
                    Grid[3, Grid.Rows.Count - 1].Value = dtTemp.Rows[i]["PName"].ToString();
                    Grid[4, Grid.Rows.Count - 1].Value = dtTemp.Rows[i]["PGrpID"].ToString();
                    Grid[5, Grid.Rows.Count - 1].Value = dtTemp.Rows[i]["GrpName"].ToString();
                    Grid[6, Grid.Rows.Count - 1].Value = dtTemp.Rows[i]["PId"].ToString();
                    Grid[7, Grid.Rows.Count - 1].Value = dtTemp.Rows[i]["PUnit"].ToString();
                    Grid[8, Grid.Rows.Count - 1].Value = dtTemp.Rows[i]["UnitCode"].ToString();
                    Grid[9, Grid.Rows.Count - 1].Value = Convert
                        .ToDecimal(ObjGlobal.ReturnDecimal(dtTemp.Rows[i]["Vat"].ToString()))
                        .ToString(ObjGlobal.SysAmountFormat);
                    Grid[10, Grid.Rows.Count - 1].Value = Convert
                        .ToDecimal(ObjGlobal.ReturnDecimal(dtTemp.Rows[i]["BuyRate"].ToString()))
                        .ToString(ObjGlobal.SysAmountFormat);
                    Grid[11, Grid.Rows.Count - 1].Value = Math
                        .Round(Convert.ToDecimal(ObjGlobal.ReturnDecimal(dtTemp.Rows[i]["Margin"].ToString())))
                        .ToString(ObjGlobal.SysAmountFormat);
                    Grid[12, Grid.Rows.Count - 1].Value = Math
                        .Round(Convert.ToDecimal(ObjGlobal.ReturnDecimal(dtTemp.Rows[i]["SalesRate"].ToString())))
                        .ToString(ObjGlobal.SysAmountFormat);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
        }
    }

    private void BindProduct()
    {
        dtTemp.Reset();
        dtTemp = GetConnection.SelectDataTableQuery(
            "Select ROW_NUMBER() Over(Order by PName) SNo, PShortName,PName,PAlias, PID,PG.PGrpID,PG.GrpName,PUnit,UnitCode,Convert(Decimal(18,2),PTax) Vat,Convert(Decimal(18,2),P.PBuyRate) BuyRate,Convert(Decimal(18,2),P.PMargin1) Margin,Convert(Decimal(18,2),PSalesRate) as SalesRate From AMS.Product as P Left Outer Join AMS.ProductGroup as PG On PG.PGrpID=P.PGrpId left outer join AMS.ProductUnit as PU on  P.PUnit=PU.UID ");
        if (dtTemp.Rows.Count > 0)
        {
            Grid.Rows.Clear();
            for (var i = 0; i < dtTemp.Rows.Count; i++)
            {
                Grid.Rows.Add();
                Grid[0, i].Value = dtTemp.Rows[i]["SNo"].ToString();
                Grid[1, i].Value = dtTemp.Rows[i]["PID"].ToString();
                Grid[2, i].Value = dtTemp.Rows[i]["PShortName"].ToString();
                Grid[3, i].Value = dtTemp.Rows[i]["PName"].ToString();
                Grid[4, i].Value = dtTemp.Rows[i]["PGrpID"].ToString();
                Grid[5, i].Value = dtTemp.Rows[i]["GrpName"].ToString();
                Grid[6, i].Value = dtTemp.Rows[i]["PID"].ToString();
                Grid[7, i].Value = dtTemp.Rows[i]["PUnit"].ToString();
                Grid[8, i].Value = dtTemp.Rows[i]["UnitCode"].ToString();
                Grid[9, i].Value =
                    Math.Round(Convert.ToDecimal(ObjGlobal.ReturnDecimal(dtTemp.Rows[i]["Vat"].ToString())))
                        .ToString(ObjGlobal.SysAmountFormat);
                Grid[10, i].Value =
                    Math.Round(Convert.ToDecimal(ObjGlobal.ReturnDecimal(dtTemp.Rows[i]["BuyRate"].ToString())))
                        .ToString(ObjGlobal.SysAmountFormat);
                Grid[11, i].Value =
                    Math.Round(Convert.ToDecimal(ObjGlobal.ReturnDecimal(dtTemp.Rows[i]["Margin"].ToString())))
                        .ToString(ObjGlobal.SysAmountFormat);
                Grid[12, i].Value =
                    Math.Round(Convert.ToDecimal(ObjGlobal.ReturnDecimal(dtTemp.Rows[i]["SalesRate"].ToString())))
                        .ToString(ObjGlobal.SysAmountFormat);
            }
        }
    }

    #endregion --------------- Method ---------------
}