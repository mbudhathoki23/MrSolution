using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Interface;
using MrDAL.Utility.Server;
using System.Data;
using System.Text;

namespace MrDAL.DataEntry.TransactionClass;

public class ClsPurchaseEntry : IPurchaseEntry
{
    // PURCHASE ENTRY FUNCTION
    #region --------------- CONSTRUCTOR ---------------

    public ClsPurchaseEntry()
    {

    }

    #endregion --------------- CONSTRUCTOR ---------------

    // RETURN VALUE IN SHORT 
    #region  --------------- SHORT ---------------
    public short ReturnSyncRowVersionVoucher(string module, string voucherNo)
    {
        var cmdString = module switch
        {
            "PIN" => $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.PIN_Master pm WHERE pm.PIN_Invoice = '{voucherNo}'",
            "PO" => $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.PO_Master pm WHERE pm.PO_Invoice = '{voucherNo}'",
            "PC" => $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.PC_Master pm WHERE pm.PC_Invoice = '{voucherNo}'",
            "GIT" => $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.GIT_Master pm WHERE pm.GIT_Invoice = '{voucherNo}'",
            "PB" => $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.PB_Master pm WHERE pm.PB_Invoice = '{voucherNo}'",
            "PR" => $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.PR_Master pm WHERE pm.PR_Invoice = '{voucherNo}'",
            "PAB" => $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.VmPabMaster pm WHERE pm.PAB_Invoice = '{voucherNo}'",
            _ => string.Empty
        };
        var result = cmdString.IsBlankOrEmpty() ? (short)1 : cmdString.GetQueryData().GetShort();

        return result.GetHashCode() > 0 ? result : (short)1;
    }
    #endregion


    // RETURN VALUE IN DATA TABLE
    #region --------------- DATA TABLE ---------------
    public DataTable CheckVoucherExitsOrNot(string tableName, string tableVoucherNo, string inputVoucherNo)
    {
        var cmdString = $" SELECT * FROM {tableName} WHERE {tableVoucherNo}= '{inputVoucherNo}'";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }
    public DataTable CheckVoucherList(string module)
    {
        var dtVoucherList = new DataTable();
        if (string.IsNullOrEmpty(module)) return dtVoucherList;
        var cmdString = $"SELECT {module}_Invoice FROM AMS.{module}_Master";
        dtVoucherList = SqlExtensions.ExecuteDataSet(cmdString)
            .Tables[0];
        return dtVoucherList;
    }

    #endregion --------------- DATA TABLE ---------------


    // RETURN VALUE IN DATA SET
    #region --------------- DATASET ---------------
    public DataSet ReturnPurchaseInvoiceDetailsInDataSet(string voucherNo)
    {
        var cmdString = $@"
		DECLARE @voucherNo NVARCHAR(50) = '{voucherNo}'
		SELECT GL.GLID, GL.GLName, SL.SLCode, SL.SLName, A.AgentCode, A.AgentName, C.CName, C.Ccode, C.CId, D.DName, PIM.*
		FROM AMS.PB_Master AS PIM
			 INNER JOIN AMS.GeneralLedger AS GL ON GL.GLID=PIM.Vendor_ID
			 LEFT OUTER JOIN AMS.SubLedger AS SL ON SL.SLId=PIM.Subledger_Id
			 LEFT OUTER JOIN AMS.JuniorAgent AS A ON A.AgentId=PIM.Agent_ID
			 LEFT OUTER JOIN AMS.Currency AS C ON C.CId=PIM.Cur_Id
			 LEFT OUTER JOIN AMS.Department AS D ON D.DId=PIM.Cls1
		WHERE PIM.PB_Invoice = @voucherNo;

		SELECT PID.PB_Invoice, PID.Invoice_SNo, PID.P_Id,P.PName, P.PAlias, P.PShortName, PID.Gdn_Id,G.GName, G.GCode, PID.Alt_Qty, PID.Alt_UnitId,P.PAltUnit,ALTU.UnitCode AS AltUnitCode, PID.Qty, PID.Unit_Id,P.PUnit, U.UnitCode, PID.Rate, PID.B_Amount, PID.T_Amount, PID.N_Amount, PID.AltStock_Qty, PID.Stock_Qty, PID.Narration, PID.PO_Invoice, PID.PO_Sno, PID.PC_Invoice, PID.PC_SNo, PID.Tax_Amount, PID.V_Amount, PID.V_Rate, PID.Free_Unit_Id, PID.Free_Qty, PID.StockFree_Qty, PID.ExtraFree_Unit_Id, PID.ExtraFree_Qty, PID.ExtraStockFree_Qty, PID.T_Product, PID.P_Ledger, PID.PR_Ledger, PID.SZ1, PID.SZ2, PID.SZ3, PID.SZ4, PID.SZ5, PID.SZ6, PID.SZ7, PID.SZ8, PID.SZ9, PID.SZ10, PID.Serial_No, PID.Batch_No, PID.Exp_Date, PID.Manu_Date, PID.TaxExempted_Amount, P.PTax
		FROM AMS.PB_Details AS PID
			 INNER JOIN AMS.Product AS P ON P.PID = PID.P_Id
			 LEFT OUTER JOIN AMS.Godown AS G ON G.GID = PID.Gdn_Id
			 LEFT OUTER JOIN AMS.ProductUnit AS ALTU ON ALTU.UID = P.PAltUnit
			 LEFT OUTER JOIN AMS.ProductUnit AS U ON U.UID = P.PUnit
		WHERE PID.PB_Invoice = @voucherNo
		ORDER BY PID.Invoice_SNo
		
		SELECT PIT.SNo, PBT.Order_No OrderNo, PBT.PT_Id TermId, PBT.PT_Name TermName, CASE WHEN PBT.PT_Basis='V' THEN 'Value' ELSE 'Qty' END Basis, PBT.PT_Sign Sign, PIT.Product_Id ProductId, PIT.Term_Type TermCondition,PBT.PT_Type TermType,PIT.Rate TermRate, PIT.Amount TermAmt, 'PB' Source, '' Formula
		FROM AMS.PB_Term AS PIT
			 INNER JOIN AMS.PT_Term AS PBT ON PBT.PT_Id=PIT.PT_Id
		WHERE PIT.PB_VNo=@voucherNo AND PIT.Term_Type='P' AND PIT.Product_Id IN (SELECT P_Id FROM AMS.PB_Details WHERE PB_Invoice=@voucherNo)
		ORDER BY PIT.SNo ASC
		SELECT PIT.SNo, PBT.Order_No OrderNo, PBT.PT_Id TermId, PBT.PT_Name TermName, CASE WHEN PBT.PT_Basis='V' THEN 'Value' ELSE 'Qty' END Basis, PBT.PT_Sign Sign, PIT.Product_Id ProductId, PIT.Term_Type TermCondition,PBT.PT_Type TermType,PIT.Rate TermRate, PIT.Amount TermAmt, 'PB' Source, '' Formula
		FROM AMS.PB_Term AS PIT
			 INNER JOIN AMS.PT_Term AS PBT ON PBT.PT_Id=PIT.PT_Id
		WHERE PIT.PB_VNo = @voucherNo AND PIT.Term_Type='B'
		ORDER BY PIT.SNo ASC
        SELECT * FROM AMS.ProductAddInfo
        WHERE VoucherNo = @voucherNo AND Module ='PB'";
        return SqlExtensions.ExecuteDataSet(cmdString);
    }
    public DataSet ReturnPurchaseIndentDetailsInDataSet(string voucherNo)
    {
        var cmdString = new StringBuilder();
        cmdString.Append(
            $"Select GL.GLID,GL.GLName,SL.SLCode, SL.SLName ,A.AgentCode,A.AgentName,C.CName,C.Ccode,C.CId,D.DName, PIM.* from  AMS.PB_Master AS PIM inner JOIN AMS.GeneralLedger as GL ON GL.GLID=PIM.Vendor_ID LEFT OUTER JOIN\tAMS.SubLedger AS SL ON SL.SLId=PIM.Subledger_Id LEFT OUTER JOIN\tAMS.JuniorAgent as A ON A.AgentId=PIM.Agent_Id LEFT OUTER JOIN\t AMS.Currency as C ON C.CId=PIM.Cur_Id Left Outer Join AMS.Department as D On D.DId=PIM.Cls1 WHERE PIM.PB_Invoice ='{voucherNo}'; \n");
        cmdString.Append(
            $"Select PID.PB_Invoice, PID.Invoice_SNo,P.PName,PAlias,P.PShortName,PID.P_Id, G.GName,G.GCode,PID.Gdn_Id, P.PAltUnit,PID.Alt_UnitId, ALTU.UID as AltUnitId,PID.Alt_Qty,PID.Qty,P.PUnit ,U.UnitCode,PID.Unit_Id, PId.Rate, PID.B_Amount,PID.T_Amount,PID.N_Amount,AltStock_Qty,Stock_Qty,Free_Qty,Narration,PO_Sno,PO_Invoice,PC_SNo,PC_Invoice From AMS.PB_Details as PID INNER JOIN AMS.Product AS P ON P.PID = PID.P_Id  LEFT OUTER JOIN  AMS.Godown AS G ON G.GID = PID.Gdn_Id  LEFT OUTER JOIN AMS.ProductUnit AS ALTU ON ALTU.UID = P.PAltUnit LEFT OUTER JOIN AMS.ProductUnit AS U ON U.UID = P.PUnit  WHERE PID.PB_Invoice = '{voucherNo}' Order By Invoice_SNo \n");
        cmdString.Append(
            $"Select PIT.SNo,Order_No OrderNo, PBT.PT_Id TermId,PBT.PT_Name TermName,Case When PBT.PT_Basis='V' Then 'Value' else 'Qty' end Basis  ,PBT.PT_Sign Sign, PIT.Product_Id ProductId,PIT.Term_Type TermType, PIT.Rate TermRate, PIT.Amount TermAmt,'PB' Source, '' Formula from AMS.PB_Term AS PIT Inner Join AMS.PT_Term as PBT ON PBT.PT_Id=PIT.PT_Id WHERE PB_VNo='{voucherNo}' AND Term_Type='P' ORDER BY PIT.SNo ASC \n");
        cmdString.Append(
            $"Select PIT.SNo,Order_No OrderNo, PBT.PT_Id TermId,PBT.PT_Name TermName,Case When PBT.PT_Basis='V' Then 'Value' else 'Qty' end Basis  ,PBT.PT_Sign Sign, PIT.Product_Id ProductId,PIT.Term_Type TermType, PIT.Rate TermRate, PIT.Amount TermAmt,'PB' Source, '' Formula from AMS.PB_Term AS PIT Inner Join AMS.PT_Term as PBT ON PBT.PT_Id=PIT.PT_Id WHERE PB_VNo = '{voucherNo}' AND Term_Type='B' ORDER BY PIT.SNo ASC  \n");
        return SqlExtensions.ExecuteDataSet(cmdString.ToString());
    }
    public DataSet ReturnPurchaseOrderDetailsInDataSet(string voucherNo)
    {
        var cmdString = $@"
		DECLARE @voucherNo NVARCHAR(50) = '{voucherNo}'
		SELECT GL.GLID, GL.GLName, SL.SLCode, SL.SLName, A.AgentCode, A.AgentName, C.CName, C.Ccode, C.CId, D.DName, PIM.*
		FROM AMS.PO_Master AS PIM
				INNER JOIN AMS.GeneralLedger AS GL ON GL.GLID=PIM.Vendor_ID
				LEFT OUTER JOIN AMS.SubLedger AS SL ON SL.SLId=PIM.Subledger_Id
				LEFT OUTER JOIN AMS.JuniorAgent AS A ON A.AgentId=PIM.Agent_ID
				LEFT OUTER JOIN AMS.Currency AS C ON C.CId=PIM.Cur_Id
				LEFT OUTER JOIN AMS.Department AS D ON D.DId=PIM.Cls1
		WHERE PIM.PO_Invoice =@voucherNo;
		SELECT PID.PO_Invoice, PID.Invoice_SNo, P.PName, PAlias, P.PShortName, PID.P_Id, G.GName, G.GCode, PID.Gdn_Id, P.PAltUnit, PID.Alt_UnitId, ALTU.UID AS AltUnitId, PID.Alt_Qty, PID.Qty, P.PUnit, U.UnitCode, PID.Unit_Id, PID.Rate, PID.B_Amount, PID.T_Amount, PID.N_Amount, AltStock_Qty, Stock_Qty, Free_Qty, Narration,  PID.PIN_Invoice, PID.PIN_Sno
		FROM AMS.PO_Details AS PID
				INNER JOIN AMS.Product AS P ON P.PID = PID.P_Id
				LEFT OUTER JOIN AMS.Godown AS G ON G.GID = PID.Gdn_Id
				LEFT OUTER JOIN AMS.ProductUnit AS ALTU ON ALTU.UID = P.PAltUnit
				LEFT OUTER JOIN AMS.ProductUnit AS U ON U.UID = P.PUnit
		WHERE PID.PO_Invoice = @voucherNo
		ORDER BY Invoice_SNo
		SELECT PIT.SNo, Order_No OrderNo, PBT.PT_Id TermId, PBT.PT_Name TermName, CASE WHEN PBT.PT_Basis='V' THEN 'Value' ELSE 'Qty' END Basis, PBT.PT_Sign Sign, PIT.Product_Id ProductId, PIT.Term_Type TermCondition,PBT.PT_Type TermType,PIT.Rate TermRate, PIT.Amount TermAmt, 'PB' Source, '' Formula
		FROM AMS.PO_Term AS PIT
				INNER JOIN AMS.PT_Term AS PBT ON PBT.PT_Id=PIT.PT_Id
		WHERE PO_Vno=@voucherNo AND Term_Type='P'
		ORDER BY PIT.SNo ASC
		SELECT PIT.SNo, Order_No OrderNo, PBT.PT_Id TermId, PBT.PT_Name TermName, CASE WHEN PBT.PT_Basis='V' THEN 'Value' ELSE 'Qty' END Basis, PBT.PT_Sign Sign, PIT.Product_Id ProductId, PIT.Term_Type TermCondition,PBT.PT_Type TermType,PIT.Rate TermRate, PIT.Amount TermAmt, 'PB' Source, '' Formula
		FROM AMS.PO_Term AS PIT
				INNER JOIN AMS.PT_Term AS PBT ON PBT.PT_Id=PIT.PT_Id
		WHERE PO_Vno = @voucherNo AND Term_Type='B'
		ORDER BY PIT.SNo ASC  ";
        return SqlExtensions.ExecuteDataSet(cmdString);
    }
    public DataSet ReturnPurchaseChallanDetailsInDataSet(string voucherNo)
    {
        var cmdString = $@"
		DECLARE @voucherNo NVARCHAR(50) = '{voucherNo}'
		SELECT GL.GlID, GL.GLName, SL.SLCode, SL.SLName, A.AgentCode, A.AgentName, C.CName, C.Ccode, C.CId, D.DName, SCM.*
		FROM AMS.PC_Master AS SCM
			 INNER JOIN AMS.GeneralLedger AS GL ON GL.GLID=SCM.Vendor_ID
			 LEFT OUTER JOIN AMS.SubLedger AS SL ON SL.SLId=SCM.Subledger_Id
			 LEFT OUTER JOIN AMS.JuniorAgent AS A ON A.AgentId=SCM.Agent_Id
			 LEFT OUTER JOIN AMS.Currency AS C ON C.CId=SCM.Cur_Id
			 LEFT OUTER JOIN AMS.Department AS D ON D.DId=SCM.Cls1
		WHERE SCM.PC_Invoice = @voucherNo;
		SELECT SCD.PC_Invoice, SCD.Invoice_SNo, P.PName, P.PShortName, SCD.P_Id, G.GName, G.GCode, SCD.Gdn_Id, SCD.Alt_UnitId, ALTU.UnitCode AS AltUnitId, SCD.Alt_Qty, SCD.Qty, U.UnitCode UnitId, SCD.Unit_Id, SCD.Rate, SCD.B_Amount, SCD.T_Amount, SCD.N_Amount, AltStock_Qty, Stock_Qty, Free_Qty, Narration, QOT_Sno, QOT_Invoice, PO_Sno, PO_Invoice, SCD.*
		FROM AMS.PC_Details AS SCD
			 INNER JOIN AMS.Product AS P ON P.PID=SCD.P_Id
			 LEFT OUTER JOIN AMS.Godown AS G ON G.GID=SCD.Gdn_Id
			 LEFT OUTER JOIN AMS.ProductUnit AS ALTU ON ALTU.UID=P.PAltUnit
			 LEFT OUTER JOIN AMS.ProductUnit AS U ON U.UID=P.PUnit
		WHERE SCD.PC_Invoice=@voucherNo
		ORDER BY SCD.Serial_No;
		SELECT SIT.SNo, Order_No OrderNo, SBT.ST_Id TermId, SBT.ST_Name TermName, CASE WHEN SBT.ST_Basis='V' THEN 'Value' ELSE 'Qty' END Basis, SBT.ST_Sign Sign, SIT.Product_Id ProductId, SIT.Term_Type TermType, SIT.Rate TermRate, SIT.Amount TermAmt, 'SC' Source, '' Formula
		FROM AMS.PC_Term AS SIT
			 INNER JOIN AMS.ST_Term AS SBT ON SBT.ST_Id=SIT.PT_Id
		WHERE PC_VNo=@voucherNo AND Term_Type='P'
		ORDER BY SIT.SNo ASC
		SELECT SIT.SNo, Order_No OrderNo, SBT.ST_Id TermId, SBT.ST_Name TermName, CASE WHEN SBT.ST_Basis='V' THEN 'Value' ELSE 'Qty' END Basis, SBT.ST_Sign Sign, SIT.Product_Id ProductId, SIT.Term_Type TermType, SIT.Rate TermRate, SIT.Amount TermAmt, 'SC' Source, '' Formula
		FROM AMS.PC_Term AS SIT
			 INNER JOIN AMS.ST_Term AS SBT ON SBT.ST_Id=SIT.PT_Id
		WHERE PC_VNo = @voucherNo AND Term_Type='B'
		ORDER BY SIT.SNo ASC ";
        return SqlExtensions.ExecuteDataSet(cmdString);
    }
    public DataSet ReturnPurchaseReturnDetailsInDataSet(string voucherNo)
    {
        var cmdString = $@"
			DECLARE @voucherNo NVARCHAR(50) = '{voucherNo}'
			SELECT GL.GLID, GL.GLName, SL.SLCode, SL.SLName, A.AgentCode, A.AgentName, C.CName, C.Ccode, C.CId, D.DName, PIM.*
			FROM AMS.PR_Master AS PIM
				 INNER JOIN AMS.GeneralLedger AS GL ON GL.GLID=PIM.Vendor_ID
				 LEFT OUTER JOIN AMS.SubLedger AS SL ON SL.SLId=PIM.Subledger_Id
				 LEFT OUTER JOIN AMS.JuniorAgent AS A ON A.AgentId=PIM.Agent_ID
				 LEFT OUTER JOIN AMS.Currency AS C ON C.CId=PIM.Cur_Id
				 LEFT OUTER JOIN AMS.Department AS D ON D.DId=PIM.Cls1
			WHERE PIM.PR_Invoice =@voucherNo;
			SELECT PID.PR_Invoice, PID.Invoice_SNo, P.PName, PAlias, P.PShortName, PID.P_Id, G.GName, G.GCode, PID.Gdn_Id, P.PAltUnit, PID.Alt_UnitId, ALTU.UID AS AltUnitId, PID.Alt_Qty, PID.Qty, P.PUnit, U.UnitCode, PID.Unit_Id, PID.Rate, PID.B_Amount, PID.T_Amount, PID.N_Amount, AltStock_Qty, Stock_Qty, Free_Qty, Narration, '' PO_Sno, '' PO_Invoice, '' PC_SNo, '' PR_Invoice
			FROM AMS.PR_Details AS PID
				 INNER JOIN AMS.Product AS P ON P.PID = PID.P_Id
				 LEFT OUTER JOIN AMS.Godown AS G ON G.GID = PID.Gdn_Id
				 LEFT OUTER JOIN AMS.ProductUnit AS ALTU ON ALTU.UID = P.PAltUnit
				 LEFT OUTER JOIN AMS.ProductUnit AS U ON U.UID = P.PUnit
			WHERE PID.PR_Invoice = @voucherNo
			ORDER BY Invoice_SNo
			SELECT PIT.SNo, Order_No OrderNo, PBT.PT_Id TermId, PBT.PT_Name TermName, CASE WHEN PBT.PT_Basis='V' THEN 'Value' ELSE 'Qty' END Basis, PBT.PT_Sign Sign, PIT.Product_Id ProductId, PIT.Term_Type TermCondition,PBT.PT_Type TermType,PIT.Rate TermRate, PIT.Amount TermAmt, 'PR' Source, '' Formula
			FROM AMS.PR_Term AS PIT
				 INNER JOIN AMS.PT_Term AS PBT ON PBT.PT_Id=PIT.PT_Id
			WHERE PR_VNo=@voucherNo AND Term_Type='P' AND PIT.Product_Id IN (SELECT P_Id FROM AMS.PR_Details WHERE PR_Invoice=@voucherNo)
			ORDER BY PIT.SNo ASC
			SELECT PIT.SNo, Order_No OrderNo, PBT.PT_Id TermId, PBT.PT_Name TermName, CASE WHEN PBT.PT_Basis='V' THEN 'Value' ELSE 'Qty' END Basis, PBT.PT_Sign Sign, PIT.Product_Id ProductId, PIT.Term_Type TermCondition,PBT.PT_Type TermType,PIT.Rate TermRate, PIT.Amount TermAmt, 'PR' Source, '' Formula
			FROM AMS.PR_Term AS PIT
				 INNER JOIN AMS.PT_Term AS PBT ON PBT.PT_Id=PIT.PT_Id
			WHERE PR_VNo = @voucherNo AND Term_Type='B'
			ORDER BY PIT.SNo ASC   ";
        return SqlExtensions.ExecuteDataSet(cmdString);
    }
    public DataSet ReturnPurchaseAdditionalDetailsInDataSet(string voucherNo)
    {
        var cmdString = $@"
			Select GL.GLID,GL.GLName,SL.SLCode, SL.SLName ,A.AgentCode,A.AgentName,C.CName,C.Ccode,C.CId,D.DName, PIM.* from  AMS.PB_Master AS PIM inner JOIN AMS.GeneralLedger as GL ON GL.GLID=PIM.Vendor_ID LEFT OUTER JOIN	AMS.SubLedger AS SL ON SL.SLId=PIM.Subledger_Id LEFT OUTER JOIN	AMS.JuniorAgent as A ON A.AgentId=PIM.Agent_Id LEFT OUTER JOIN	 AMS.Currency as C ON C.CId=PIM.Cur_Id Left Outer Join AMS.Department as D On D.DId=PIM.Cls1 WHERE PIM.PB_Invoice ='{voucherNo};
			Select PID.PB_Invoice, PID.Invoice_SNo,P.PName,PAlias,P.PShortName,PID.P_Id, G.GName,G.GCode,PID.Gdn_Id, P.PAltUnit,PID.Alt_UnitId, ALTU.UID as AltUnitId,PID.Alt_Qty,PID.Qty,P.PUnit ,U.UnitCode,PID.Unit_Id, PId.Rate, PID.B_Amount,PID.T_Amount,PID.N_Amount,AltStock_Qty,Stock_Qty,Free_Qty,Narration,PO_Sno,PO_Invoice,PC_SNo,PC_Invoice From AMS.PB_Details as PID INNER JOIN AMS.Product AS P ON P.PID = PID.P_Id  LEFT OUTER JOIN  AMS.Godown AS G ON G.GID = PID.Gdn_Id  LEFT OUTER JOIN AMS.ProductUnit AS ALTU ON ALTU.UID = P.PAltUnit LEFT OUTER JOIN AMS.ProductUnit AS U ON U.UID = P.PUnit  WHERE PID.PB_Invoice = '{voucherNo}' Order By Invoice_SNo
			Select PIT.SNo,Order_No OrderNo, PBT.PT_Id TermId,PBT.PT_Name TermName,Case When PBT.PT_Basis='V' Then 'Value' else 'Qty' end Basis  ,PBT.PT_Sign Sign, PIT.Product_Id ProductId,PIT.Term_Type TermType, PIT.Rate TermRate, PIT.Amount TermAmt,'PB' Source, '' Formula from AMS.PB_Term AS PIT Inner Join AMS.PT_Term as PBT ON PBT.PT_Id=PIT.PT_Id WHERE PB_VNo='{voucherNo}' AND Term_Type='P' ORDER BY PIT.SNo ASC
			Select PIT.SNo,Order_No OrderNo, PBT.PT_Id TermId,PBT.PT_Name TermName,Case When PBT.PT_Basis='V' Then 'Value' else 'Qty' end Basis  ,PBT.PT_Sign Sign, PIT.Product_Id ProductId,PIT.Term_Type TermType, PIT.Rate TermRate, PIT.Amount TermAmt,'PB' Source, '' Formula from AMS.PB_Term AS PIT Inner Join AMS.PT_Term as PBT ON PBT.PT_Id=PIT.PT_Id WHERE PB_VNo = '{voucherNo}' AND Term_Type='B' ORDER BY PIT.SNo ASC  ";
        return SqlExtensions.ExecuteDataSet(cmdString);
    }
    public DataSet ReturnPurchaseExpiryBreakageDetailsInDataSet(string voucherNo)
    {
        var cmdString = $@"
			Select GL.GLID,GL.GLName,SL.SLCode, SL.SLName ,A.AgentCode,A.AgentName,C.CName,C.Ccode,C.CId,D.DName, PIM.* from  AMS.PB_Master AS PIM inner JOIN AMS.GeneralLedger as GL ON GL.GLID=PIM.Vendor_ID LEFT OUTER JOIN	AMS.SubLedger AS SL ON SL.SLId=PIM.Subledger_Id LEFT OUTER JOIN	AMS.JuniorAgent as A ON A.AgentId=PIM.Agent_Id LEFT OUTER JOIN	 AMS.Currency as C ON C.CId=PIM.Cur_Id Left Outer Join AMS.Department as D On D.DId=PIM.Cls1 WHERE PIM.PB_Invoice ='{voucherNo}';
			Select PID.PB_Invoice, PID.Invoice_SNo,P.PName,PAlias,P.PShortName,PID.P_Id, G.GName,G.GCode,PID.Gdn_Id, P.PAltUnit,PID.Alt_UnitId, ALTU.UID as AltUnitId,PID.Alt_Qty,PID.Qty,P.PUnit ,U.UnitCode,PID.Unit_Id, PId.Rate, PID.B_Amount,PID.T_Amount,PID.N_Amount,AltStock_Qty,Stock_Qty,Free_Qty,Narration,PO_Sno,PO_Invoice,PC_SNo,PC_Invoice From AMS.PB_Details as PID INNER JOIN AMS.Product AS P ON P.PID = PID.P_Id  LEFT OUTER JOIN  AMS.Godown AS G ON G.GID = PID.Gdn_Id  LEFT OUTER JOIN AMS.ProductUnit AS ALTU ON ALTU.UID = P.PAltUnit LEFT OUTER JOIN AMS.ProductUnit AS U ON U.UID = P.PUnit  WHERE PID.PB_Invoice = '{voucherNo}' Order By Invoice_SNo ;
			Select PIT.SNo,Order_No OrderNo, PBT.PT_Id TermId,PBT.PT_Name TermName,Case When PBT.PT_Basis='V' Then 'Value' else 'Qty' end Basis  ,PBT.PT_Sign Sign, PIT.Product_Id ProductId,PIT.Term_Type TermType, PIT.Rate TermRate, PIT.Amount TermAmt,'PB' Source, '' Formula from AMS.PB_Term AS PIT Inner Join AMS.PT_Term as PBT ON PBT.PT_Id=PIT.PT_Id WHERE PB_VNo='{voucherNo}' AND Term_Type='P' ORDER BY PIT.SNo ASC ;
			Select PIT.SNo,Order_No OrderNo, PBT.PT_Id TermId,PBT.PT_Name TermName,Case When PBT.PT_Basis='V' Then 'Value' else 'Qty' end Basis  ,PBT.PT_Sign Sign, PIT.Product_Id ProductId,PIT.Term_Type TermType, PIT.Rate TermRate, PIT.Amount TermAmt,'PB' Source, '' Formula from AMS.PB_Term AS PIT Inner Join AMS.PT_Term as PBT ON PBT.PT_Id=PIT.PT_Id WHERE PB_VNo = '{voucherNo}' AND Term_Type='B' ORDER BY PIT.SNo ASC; ";
        return SqlExtensions.ExecuteDataSet(cmdString);
    }
    #endregion --------------- DATASET ---------------
}