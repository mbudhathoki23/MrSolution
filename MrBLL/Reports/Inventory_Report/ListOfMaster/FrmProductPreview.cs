using MrDAL.Control.WinControl;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MrBLL.Reports.Inventory_Report.ListOfMaster;

public partial class FrmProductPreview : MrForm
{
    private int currentColumn;
    private DataTable dt = new();
    private DataTable dtProduct = new();
    private int ProductId, BranchId;
    private string Query = string.Empty;
    private int rowIndex;
    private TextBox textBox = new();

    public FrmProductPreview()
    {
        InitializeComponent();
    }

    private void FrmProductPreview_Load(object sender, EventArgs e)
    {
        BackColor = ObjGlobal.FrmBackColor();
        ObjGlobal.DGridColorCombo(DGrid);
        ObjGlobal.FetchPic(PbPicbox, string.Empty);
        GridDesign();
        LoadData();
        DGrid.Focus();
    }

    private void FrmProductPreview_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void GridDesign()
    {
        //try
        //{
        //    Grid.ReadOnly = true;
        //    Grid.Columns[0].Width = 40;     //SNo
        //    Grid.Columns[2].Width = 250;    //Description
        //    Grid.Columns[3].Width = 100;    //Amount

        //    Grid.Columns[0].HeaderText = "SNo";
        //    Grid.Columns[2].HeaderText = "Description";
        //    Grid.Columns[3].HeaderText = "Amount";

        //    Grid.Columns[0].Name = "txt_OPSno";
        //    Grid.Columns[2].Name = "txt_OPledger";
        //    Grid.Columns[3].Name = "txt_OPAmount";

        //    Grid.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
        //    Grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    this.Grid.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        //    this.Grid.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        //    this.Grid.Columns[3].DefaultCellStyle.Format = "0.00";
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.RetryCancel);
        //}
    }

    private void LoadData()
    {
        try
        {
            Query =
                "Select ROW_NUMBER() OVER (ORDER BY PName) AS SNO,PID,PName,PShortName,UnitName from AMS.Product as P left Outer Join AMS.ProductUnit as PU on P.PUnit=Pu.UID Order By PName,PShortName";
            dt.Reset();
            DGrid.Rows.Clear();
            dt = GetConnection.SelectDataTableQuery(Query);
            if (dt.Rows.Count > 0)
                foreach (DataRow ro in dt.Rows)
                {
                    var rows = DGrid.Rows.Count;
                    DGrid.Rows.Add();
                    DGrid.Rows[DGrid.RowCount - 1].Cells["txt_Sno"].Value = ro["SNO"].ToString();
                    DGrid.Rows[DGrid.RowCount - 1].Cells["txt_ProId"].Value = ro["PID"].ToString();
                    DGrid.Rows[DGrid.RowCount - 1].Cells["txt_Product"].Value = ro["PName"].ToString();
                    DGrid.Rows[DGrid.RowCount - 1].Cells["txt_ShortName"].Value = ro["PShortName"].ToString();
                    DGrid.Rows[DGrid.RowCount - 1].Cells["txt_Unit"].Value = ro["UnitName"].ToString();
                }
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

    private void Grid_SelectionChanged(object sender, EventArgs e)
    {
    }

    private void Grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        ProductId = 0;
        LoadDataDetails();
    }

    private void Grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
    }

    private void Grid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) //|| e.KeyCode == Keys.Tab
        {
            DGrid.Rows[rowIndex].Selected = true;
            e.SuppressKeyPress = true;
            ProductId = Convert.ToInt16(DGrid.Rows[rowIndex].Cells[1].Value);
            LoadDataDetails();
        }
    }

    private void LoadDataDetails()
    {
        try
        {
            if (DGrid.Rows[rowIndex].Cells[1].Value != null)
            {
                BranchId = Convert.ToInt16(ObjGlobal.SysBranchId);
                var FromDate = new MaskedTextBox();
                var ToDate = new MaskedTextBox();
                FromDate.Text = ObjGlobal.CfStartAdDate.ToString();
                ToDate.Text = ObjGlobal.CfEndAdDate.ToString();
                ProductId = Convert.ToInt16(DGrid.Rows[rowIndex].Cells[1].Value);
                dtProduct.Reset();
                Query =
                    "Select PID,PName,PShortName,Convert(Decimal(18,2),Sum(BalanceAltQty) ) BalanceAltQty,Convert(Decimal(18,2),Sum(BalanceQty) ) BalanceQty,UnitCode,Convert(Decimal(18,2),PSalesRate)PMRP,Convert(Decimal(18,2),PBuyRate ) PBuyRate,Convert(Decimal(18,2),PSalesRate)PSalesRate,GrpName From";
                Query = Query +
                        " (Select P.PID PID,P.PName,P.PShortName,Case When EntryType='I' Then isnull(Sum(AltStockQty),0) Else  -isnull(Sum(AltStockQty),0) End BalanceAltQty,Case When EntryType='I' Then isnull(Sum(StockQty),0) Else  -isnull(Sum(StockQty),0) End BalanceQty,PU.UnitCode,P.PMRP,P.PBuyRate,P.PSalesRate,PG.GrpName  from AMS.Product  as P ";
                Query = Query +
                        " left outer join AMS.StockDetails as SD on P.PID=SD.Product_Id left outer join AMS.ProductUnit as PU on  P.PUnit=PU.UID left outer join AMS.ProductGroup as PG on  PG.PGrpID=p.PGrpId ";
                Query = Query +
                        " Group By PID,P.PName,P.PShortName,PU.UnitCode,P.PBuyRate,P.PSalesRate,PG.GrpName,EntryType,PMRP ) as aa  where PId = " +
                        ProductId + " ";
                Query = Query + " Group by PID,PName,PShortName,UnitCode,PBuyRate,PSalesRate,GrpName,PMRP";
                dtProduct = GetConnection.SelectDataTableQuery(Query);
                if (dtProduct.Rows.Count > 0)
                {
                    lbl_AltQty.Text = Convert.ToDecimal(dtProduct.Rows[0]["BalanceAltQty"].ToString())
                        .ToString("0.00");
                    lbl_Qty.Text = Convert.ToDecimal(dtProduct.Rows[0]["BalanceQty"].ToString()).ToString("0.00");
                    lbl_Sales.Text = Convert.ToDecimal(dtProduct.Rows[0]["PSalesRate"].ToString()).ToString("0.00");
                    lbl_Purchase.Text =
                        Convert.ToDecimal(dtProduct.Rows[0]["PBuyRate"].ToString()).ToString("0.00");
                    lbl_MRP.Text = Convert.ToDecimal(dtProduct.Rows[0]["PMRP"].ToString()).ToString("0.00");
                    //ObjGlobal.FetchPic(picDocumentPreview, "");
                    dt.Clear();
                    dt.Reset();
                    Query = @"Select PImage from AMS.Product where PID=" + ProductId + "  ";
                    dt = GetConnection.SelectDataTableQuery(Query);
                    var imageData = dt.Rows[0]["PImage"] as byte[] ?? null;
                    if (imageData != null)
                    {
                        Image newImage;
                        using (var ms = new MemoryStream(imageData, 0, imageData.Length))
                        {
                            ms.Write(imageData, 0, imageData.Length);
                            newImage = Image.FromStream(ms, true);
                        }

                        PbPicbox.Image = newImage;
                        lblProductPic.Visible = false;
                    }
                    else
                    {
                        PbPicbox.Image = null;
                        lblProductPic.Visible = true;
                    }
                }
            }
            else
            {
                lbl_AltQty.Text = Convert.ToDecimal(0).ToString("0.00");
                lbl_Qty.Text = Convert.ToDecimal(0).ToString("0.00");
                lbl_Sales.Text = Convert.ToDecimal(0).ToString("0.00");
                lbl_Purchase.Text = Convert.ToDecimal(0).ToString("0.00");
                lbl_MRP.Text = Convert.ToDecimal(0).ToString("0.00");
                PbPicbox.Image = null;
                lblProductPic.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}