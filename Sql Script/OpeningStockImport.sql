

INSERT INTO AMS.GeneralLedger(GLID, NepaliDesc, GLName, GLCode, ACCode, GLType, GrpId, PrimaryGroupId, SubGrpId, PrimarySubGroupId, PanNo, AreaId, AgentId, CurrId, CrDays, CrLimit, CrTYpe, IntRate, GLAddress, PhoneNo, LandLineNo, OwnerName, OwnerNumber, Scheme, Email, Branch_ID, Company_Id, EnterBy, EnterDate, Status, IsDefault, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
SELECT GLID, NepaliDesc, GLName, GLCode, ACCode, GLType, GrpId, PrimaryGroupId, SubGrpId, PrimarySubGroupId, PanNo, AreaId, AgentId, CurrId, CrDays, CrLimit, CrTYpe, IntRate, GLAddress, PhoneNo, LandLineNo, OwnerName, OwnerNumber, Scheme, Email, Branch_ID, Company_Id, EnterBy, EnterDate, Status, IsDefault, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
FROM MR_ACCOU02.AMS.GeneralLedger
WHERE GLID NOT IN(SELECT gl.GLID FROM AMS.GeneralLedger gl);


INSERT INTO AMS.Product(PID, NepaliDesc, PName, PAlias, PShortName, PType, PCategory, PUnit, PAltUnit, PQtyConv, PAltConv, PValTech, PSerialno, PSizewise, PBatchwise, PBuyRate, PSalesRate, PMargin1, TradeRate, PMargin2, PMRP, PGrpId, PSubGrpId, PTax, PMin, PMax, CmpId, CmpId1, CmpId2, CmpId3, Branch_Id, CmpUnit_Id, PPL, PPR, PSL, PSR, PL_Opening, PL_Closing, BS_Closing, PImage, EnterBy, EnterDate, IsDefault, Status, ChasisNo, EngineNo, VHModel, VHColor, VHNumber, BeforeBuyRate, BeforeSalesRate, Barcode, Barcode1, Barcode2, Barcode3, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, AltSalesRate)
SELECT PID, NepaliDesc, PName, PAlias, PShortName, PType, PCategory, PUnit, PAltUnit, PQtyConv, PAltConv, PValTech, PSerialno, PSizewise, PBatchwise, PBuyRate, PSalesRate, PMargin1, TradeRate, PMargin2, PMRP, PGrpId, PSubGrpId, PTax, PMin, PMax, CmpId, CmpId1, CmpId2, CmpId3, Branch_Id, CmpUnit_Id, PPL, PPR, PSL, PSR, PL_Opening, PL_Closing, BS_Closing, PImage, EnterBy, EnterDate, IsDefault, Status, ChasisNo, EngineNo, VHModel, VHColor, VHNumber, BeforeBuyRate, BeforeSalesRate, Barcode, Barcode1, Barcode2, Barcode3, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, ISNULL(AltSalesRate, 0)
FROM MR_ACCOU02.AMS.Product
WHERE PID NOT IN(SELECT PID FROM AMS.Product);


INSERT INTO AMS.PostDateCheque(VoucherNo, VoucherDate, VoucherTime, VoucherMiti, BankLedgerId, VoucherType, Status, BankName, BranchName, ChequeNo, ChqDate, ChqMiti, DrawOn, Amount, LedgerId, SubLedgerId, AgentId, Remarks, DepositedBy, DepositeDate, IsReverse, CancelReason, CancelBy, CancelDate, Cls1, Cls2, Cls3, Cls4, BranchId, CompanyUnitId, FiscalYearId, EnterBy, EnterDate, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
SELECT VoucherNo, VoucherDate, VoucherTime, VoucherMiti, BankLedgerId, VoucherType, Status, BankName, BranchName, ChequeNo, ChqDate, ChqMiti, DrawOn, Amount, LedgerId, SubLedgerId, AgentId, Remarks, DepositedBy, DepositeDate, IsReverse, CancelReason, CancelBy, CancelDate, Cls1, Cls2, Cls3, Cls4, BranchId, CompanyUnitId, FiscalYearId, EnterBy, EnterDate, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
FROM MR_ACCOU02.AMS.PostDateCheque
WHERE [Status]='DUE' AND VoucherNo NOT IN(SELECT pdc.VoucherNo FROM AMS.PostDateCheque pdc);


INSERT INTO AMS.LedgerOpening(Opening_Id, Module, Serial_No, Voucher_No, OP_Date, OP_Miti, Ledger_Id, Subledger_Id, Agent_Id, Cls1, Cls2, Cls3, Cls4, Currency_Id, Currency_Rate, Debit, LocalDebit, Credit, LocalCredit, Narration, Remarks, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Branch_Id, Company_Id, FiscalYearId, IsReverse, CancelRemarks, CancelBy, CancelDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
SELECT 1 Opening_Id, 'LOB' Module, ROW_NUMBER() OVER (ORDER BY Ledger_ID) Serial_No, 'LOB/0001/79-80' Voucher_No, '2022-07-15' OP_Date, '31/03/2079' OP_Miti, Ledger_ID, NULL Subledger_Id, NULL Agent_Id, NULL Cls1, NULL Cls2, NULL Cls3, NULL Cls4, 1 Currency_Id, 1 Currency_Rate, CASE WHEN SUM(LocalDebit_Amt-LocalCredit_Amt)>0 THEN SUM(LocalDebit_Amt-LocalCredit_Amt)ELSE 0 END Debit, CASE WHEN SUM(LocalDebit_Amt-LocalCredit_Amt)>0 THEN SUM(LocalDebit_Amt-LocalCredit_Amt)ELSE 0 END LocalDebit, CASE WHEN SUM(LocalCredit_Amt-LocalDebit_Amt)>0 THEN SUM(LocalCredit_Amt-LocalDebit_Amt)ELSE 0 END Credit, CASE WHEN SUM(LocalCredit_Amt-LocalDebit_Amt)>0 THEN SUM(LocalCredit_Amt-LocalDebit_Amt)ELSE 0 END LocalCredit, NULL Narration, NULL Remarks, 'MrSolution' Enter_By, GETDATE() Enter_Date, NULL Reconcile_By, NULL Reconcile_Date, od.Branch_ID, NULL Company_Id, 13 FiscalYearId, 0 IsReverse, NULL CancelRemarks, NULL CancelBy, NULL CancelDate, NULL SyncBaseId, NULL SyncGlobalId, NULL SyncOriginId, NULL SyncCreatedOn, NULL SyncLastPatchedOn, 1 SyncRowVersion
FROM MR_FARMWE01.AMS.AccountDetails od
left outer join AMS.GeneralLedger gl on gl.GLID = od.Ledger_ID
left outer join AMS.AccountGroup ag on ag.GrpId = gl.GrpId
where PrimaryGrp in( 'BS','Balance Sheet') and GLType <> 'Customer'
GROUP BY Ledger_ID, od.Branch_ID
HAVING SUM(LocalDebit_Amt-LocalCredit_Amt)<>0;


INSERT INTO AMS.ProductOpening(OpeningId, Voucher_No, Serial_No, OP_Date, OP_Miti, Product_Id, Godown_Id, Cls1, Cls2, Cls3, Cls4, Currency_Id, Currency_Rate, AltQty, AltUnit, Qty, QtyUnit, Rate, LocalRate, Amount, LocalAmount, IsReverse, CancelRemarks, CancelBy, CancelDate, Remarks, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, CBranch_Id, CUnit_Id, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
SELECT 1 OpeningId, 'OB/00001/79-80' Voucher_No, ROW_NUMBER() OVER (ORDER BY sd.Product_Id) Serial_No, '2022-07-15' OP_Date, '31/03/2079' OP_Miti, Product_Id, NULL Godown_Id, NULL Cls1, NULL Cls2, NULL Cls3, NULL Cls4, 1 Currency_Id, 1 Currency_Rate, SUM(CASE WHEN sd.EntryType='I' THEN sd.AltStockQty ELSE -sd.AltStockQty END) AltQty, sd.AltUnit_Id AltUnit, SUM(CASE WHEN sd.EntryType='I' THEN sd.StockQty ELSE -sd.StockQty END) Qty, sd.Unit_Id QtyUnit, 0 Rate, 0 LocalRate, SUM(CASE WHEN sd.EntryType='I' THEN sd.StockVal ELSE -sd.StockVal END) Amount, SUM(CASE WHEN sd.EntryType='I' THEN sd.StockVal ELSE -sd.StockVal END) LocalAmount, 0 IsReverse, NULL CancelRemarks, NULL CancelBy, NULL CancelDate, NULL Remarks, 'MrSolution' Enter_By, GETDATE() Enter_Date, NULL Reconcile_By, NULL Reconcile_Date, sd.Branch_Id CBranch_Id, NULL CUnit_Id, 13 FiscalYearId, NULL SyncBaseId, NULL SyncGlobalId, NULL SyncOriginId, NULL SyncCreatedOn, NULL SyncLastPatchedOn, 1 SyncRowVersion
FROM MR_ACCOUN02.AMS.StockDetails sd
GROUP BY sd.AltUnit_Id, sd.Unit_Id, sd.Branch_Id, sd.Product_Id
HAVING SUM(CASE WHEN sd.EntryType='I' THEN sd.StockQty ELSE -sd.StockQty END)>0;
