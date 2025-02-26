SELECT SB_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, SO_Invoice, SO_Sno, SC_Invoice, SC_SNo, Tax_Amount, V_Amount, V_Rate, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, MaterialPost, PDiscountRate, PDiscount, BDiscountRate, BDiscount, ServiceChargeRate, ServiceCharge, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId
	FROM AMS.SB_Details
WHERE SB_Invoice='SB-000025-77\78';

SELECT SB_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, PB_Vno, Vno_Date, Vno_Miti, Customer_Id, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_Id, Subledger_Id, SO_Invoice, SO_Date, SC_Invoice, SC_Date, Cls1, Cls2, Cls3, Cls4, CounterId, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Tender_Amount, Return_Amount, Action_Type, In_Words, Remarks, R_Invoice, Is_Printed, No_Print, Printed_By, Printed_Date, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, Cancel_By, Cancel_Date, Cancel_Remarks, CUnit_Id, CBranch_Id, IsAPIPosted, IsRealtime, FiscalYearId, MShipId, TableId, DoctorId, PatientId, HDepartmentId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId, ChqMiti, PartyLedgerId
	FROM AMS.SB_Master
WHERE SB_Invoice='SB-000025-77\78';

SELECT SB_VNo, ST_Id, SNo, Term_Type, Rate, Amount, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId
	FROM AMS.SB_Term
WHERE SB_VNo='SB-000025-77\78';


INSERT INTO AMS.SB_Term(SB_VNo, ST_Id, SNo, Term_Type, Rate, Amount, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
SELECT SB_Invoice,6,1,'B',0,T_Amount,NULL,'N',NULL,NULL,NULL,NULL,1,NULL
FROM AMS.SB_Master
WHERE T_Amount>0 AND SB_Invoice NOT IN(SELECT SB_VNo
										FROM AMS.SB_Term
										WHERE Term_Type='B' AND ST_Id=6);



					SELECT * FROM AMS.SB_Details WHERE SB_Invoice='SB-003793-77\78'


								DELETE AMS.SB_Term WHERE Term_Type='BT' 
            INSERT INTO AMS.SB_Term(SB_VNo, ST_Id, SNo, Term_Type, Product_Id, Rate, Amount, Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT sbt.SB_VNo SB_VNo, sbt.ST_Id ST_Id, sd.Invoice_SNo AS SNo, 'BT' Term_Type, sd.P_Id Product_Id,CASE WHEN sm.Invoice_Type = 'P VAT' AND ISNULL(p.PTax,0) = 0 AND sbt.ST_Id = 4  THEN 0 ELSE sbt.Rate END Rate,
            CASE WHEN sm.Invoice_Type = 'P VAT' AND ISNULL(p.PTax,0) = 0 AND sbt.ST_Id = 4  THEN 0 ELSE (sbt.Amount / sm.N_Amount)* sd.N_Amount END  Amount, CASE WHEN sd.T_Product=1 THEN 'Y' ELSE 'N' END Taxable, sbt.SyncBaseId, sbt.SyncGlobalId, sbt.SyncOriginId, sbt.SyncCreatedOn, sbt.SyncLastPatchedOn, ISNULL(sbt.SyncRowVersion, 1) SyncRowVersion
            FROM AMS.SB_Details sd
	            LEFT OUTER JOIN AMS.SB_Master sm ON sm.SB_Invoice=sd.SB_Invoice
	            LEFT OUTER JOIN AMS.SB_Term sbt ON sd.SB_Invoice=sbt.SB_VNo
	            LEFT OUTER JOIN AMS.Product P ON P.PID = sd.P_Id
            WHERE sbt.Term_Type='B' AND Product_Id IS NULL  AND sbt.SB_VNo = 'SB-003793-77\78'
			ORDER BY ST_Id,sd.Invoice_SNo


			SELECT * FROM AMS.SB_Master WHERE SB_Invoice = 'SB-003793-77\78'

			SELECT * FROM AMS.SB_Term WHERE SB_VNo = 'SB-003793-77\78'


			--UPDATE AMS.SB_Term SET Term_Type = 'P' WHERE ST_Id = 4 AND Term_Type = 'B'