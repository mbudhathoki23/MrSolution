INSERT INTO AMS.AccountGroup(GrpId,NepaliDesc,GrpName,GrpCode,Schedule,PrimaryGrp,GrpType,Branch_ID,Company_Id,Status,EnterBy,EnterDate,PrimaryGroupId,IsDefault,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion)
SELECT ag.AccountGrpId GrpId,ag.AccountGrpDesc NepaliDesc,ag.AccountGrpDesc GrpName,ag.AccountGrpShortName GrpCode,Schedule,ag.GrpType,SUBSTRING(ag.PrimaryGrp,1,1) GrpType,1 Branch_ID,NULL Company_Id,Status,EnterBy,EnterDate,NULL PrimaryGroupId,0 IsDefault,NULL SyncBaseId,NULL SyncGlobalId,NULL SyncOriginId,NULL SyncCreatedOn,NULL SyncLastPatchedOn,1 SyncRowVersion FROM PRAN798001.ERP.AccountGroup ag
WHERE ag.AccountGrpId NOT IN (SELECT ag1.GrpId FROM AMS.AccountGroup ag1)

INSERT INTO AMS.AccountSubGroup(SubGrpId,NepaliDesc,SubGrpName,GrpId,SubGrpCode,Branch_ID,Company_Id,Status,EnterBy,EnterDate,PrimaryGroupId,PrimarySubGroupId,IsDefault,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion)
SELECT asg.AccountSubGrpId SubGrpId,asg.AccountSubGrpDesc NepaliDesc,asg.AccountSubGrpDesc SubGrpName,asg.AccountGrpId GrpId,asg.AccountSubGrpShortName SubGrpCode,1 Branch_ID,NULL Company_Id,Status,EnterBy,EnterDate,NULL PrimaryGroupId,NULL PrimarySubGroupId,0 IsDefault,NULL SyncBaseId,NULL SyncGlobalId,NULL SyncOriginId,NULL SyncCreatedOn,NULL SyncLastPatchedOn,1 SyncRowVersion FROM PRAN798001.ERP.AccountSubGroup asg
WHERE asg.AccountSubGrpId NOT IN (SELECT asg1.SubGrpId FROM AMS.AccountSubGroup asg1)

INSERT INTO AMS.Currency(CId,NepaliDesc,CName,CCode,CRate,Branch_ID,Company_Id,Status,IsDefault,EnterBy,EnterDate,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion)
SELECT c.CurrencyId CId,c.CurrencyDesc NepaliDesc,c.CurrencyDesc CName,c.CurrencyDesc CCode,c.CurrencyRate CRate,1 Branch_ID,NULL Company_Id,Status,0 IsDefault,EnterBy,EnterDate,NULL SyncBaseId,NULL SyncGlobalId,NULL SyncOriginId,NULL SyncCreatedOn,NULL SyncLastPatchedOn,1 SyncRowVersion FROM PRAN798001.ERP.Currency c
WHERE c.CurrencyId NOT IN (SELECT c1.CId FROM AMS.Currency c1)

INSERT INTO AMS.GeneralLedger(GLID,NepaliDesc,GLName,GLCode,ACCode,GLType,GrpId,PrimaryGroupId,SubGrpId,PrimarySubGroupId,PanNo,AreaId,AgentId,CurrId,CrDays,CrLimit,CrTYpe,IntRate,GLAddress,PhoneNo,LandLineNo,OwnerName,OwnerNumber,Scheme,Email,Branch_ID,Company_Id,EnterBy,EnterDate,Status,IsDefault,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion)
SELECT gl.LedgerId GLID,gl.GlDesc NepaliDesc,gl.GlDesc GLName,gl.GlShortName GLCode,ISNULL(gl.ACCode,gl.LedgerId) ACCode,gl.GlCategory GLType,gl.AccountGrpId GrpId,NULL PrimaryGroupId,gl.AccountSubGrpId SubGrpId,NULL PrimarySubGroupId,PanNo,AreaId,gl.SalesmanId AgentId,ISNULL(gl.CurrencyId,1) CurrId,gl.CreditDays CrDays,gl.CreditLimit CrLimit,SUBSTRING(gl.CreditDaysWarning,1,1) CrTYpe,ISNULL(gl.InterestRate,0) IntRate,gl.Address GLAddress,PhoneNo,gl.CPPhoneNo LandLineNo,NULL OwnerName,NULL OwnerNumber,NULL Scheme,Email,1 Branch_ID,NULL Company_Id,EnterBy,EnterDate,Status,0 IsDefault,NULL SyncBaseId,NULL SyncGlobalId,NULL SyncOriginId,NULL SyncCreatedOn,NULL SyncLastPatchedOn,1 SyncRowVersion FROM PRAN798001.ERP.GeneralLedger gl
WHERE gl.LedgerId NOT IN (SELECT gl1.GLID FROM AMS.GeneralLedger gl1)


INSERT INTO AMS.Subledger(SLId,NepalDesc,SLName,SLCode,SLAddress,SLPhoneNo,GLID,Branch_ID,Company_Id,Status,IsDefault,EnterBy,EnterDate,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion)
SELECT sl.SubledgerId SLId,sl.SubledgerDesc NepalDesc,sl.SubledgerDesc SLName,sl.SubledgerShortName SLCode,sl.Address SLAddress,sl.PhoneNo SLPhoneNo,sl.LedgerId GLID,1 Branch_ID,NULL Company_Id,Status,0 IsDefault,EnterBy,EnterDate,NULL SyncBaseId,NULL SyncGlobalId,NULL SyncOriginId,NULL SyncCreatedOn,NULL SyncLastPatchedOn,1 SyncRowVersion FROM PRAN798001.ERP.Subledger sl
WHERE sl.SubledgerId NOT IN (SELECT s.SLId FROM AMS.Subledger s)

INSERT INTO AMS.ProductUnit(UID,NepaliDesc,UnitName,UnitCode,Branch_ID,Company_Id,EnterBy,EnterDate,Status,IsDefault,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion)
SELECT pu.ProductUnitId UID,pu.ProductUnitDesc NepaliDesc,pu.ProductUnitDesc UnitName,pu.ProductUnitShortName UnitCode,1 Branch_ID,NULL Company_Id,EnterBy,EnterDate,Status,0 IsDefault,NULL SyncBaseId,NULL SyncGlobalId,NULL SyncOriginId,NULL SyncCreatedOn,NULL SyncLastPatchedOn,1 SyncRowVersion FROM PRAN798001.ERP.ProductUnit pu
WHERE pu.ProductUnitId NOT IN (SELECT pu1.UID FROM AMS.ProductUnit pu1)

INSERT INTO AMS.ProductGroup(PGrpId,NepaliDesc,GrpName,GrpCode,GMargin,Gprinter,PurchaseLedgerId,PurchaseReturnLedgerId,SalesLedgerId,SalesReturnLedgerId,OpeningStockLedgerId,ClosingStockLedgerId,StockInHandLedgerId,Branch_ID,Company_Id,Status,EnterBy,EnterDate,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion)
SELECT pg.ProductGrpId PGrpId,pg.ProductGrpDesc NepaliDesc,pg.ProductGrpDesc GrpName,pg.ProductGrpShortName GrpCode,pg.Margin GMargin,pg.PrinterName Gprinter,NULL PurchaseLedgerId,NULL PurchaseReturnLedgerId,NULL SalesLedgerId,NULL SalesReturnLedgerId,NULL OpeningStockLedgerId,NULL ClosingStockLedgerId,NULL StockInHandLedgerId,1 Branch_ID,NULL Company_Id,Status,EnterBy,EnterDate,NULL SyncBaseId,NULL SyncGlobalId,NULL SyncOriginId,NULL SyncCreatedOn,NULL SyncLastPatchedOn,1 SyncRowVersion FROM PRAN798001.ERP.ProductGroup pg
WHERE pg.ProductGrpId NOT IN (SELECT pg.ProductGrpId FROM AMS.Product p)

INSERT INTO AMS.ProductSubGroup(PSubGrpId,NepaliDesc,SubGrpName,ShortName,GrpId,Branch_ID,Company_Id,EnterBy,EnterDate,IsDefault,Status,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion)
SELECT psg.ProductSubGrpId PSubGrpId,psg.ProductSubGrpDesc NepaliDesc,psg.ProductSubGrpDesc SubGrpName,psg.ProductSubGrpShortName ShortName,psg.ProductGrpId GrpId,1 Branch_ID,NULL Company_Id,EnterBy,EnterDate,0 IsDefault,Status,NULL SyncBaseId,NULL SyncGlobalId,NULL SyncOriginId,NULL SyncCreatedOn,NULL SyncLastPatchedOn,1 SyncRowVersion FROM PRAN798001.ERP.ProductSubGroup psg
WHERE psg.ProductSubGrpId NOT IN (SELECT psg1.PSubGrpId FROM AMS.ProductSubGroup psg1)


INSERT INTO AMS.Product(PID,NepaliDesc,PName,PAlias,PShortName,PType,PCategory,PUnit,PAltUnit,PQtyConv,PAltConv,PValTech,PSerialno,PSizewise,PBatchwise,PVehicleWise,PublicationWise,PBuyRate,AltSalesRate,PSalesRate,PMargin1,TradeRate,PMargin2,PMRP,PGrpId,PSubGrpId,PTax,PMin,PMax,CmpId,CmpId1,CmpId2,CmpId3,Branch_Id,CmpUnit_Id,PPL,PPR,PSL,PSR,PL_Opening,PL_Closing,BS_Closing,PImage,EnterBy,EnterDate,IsDefault,Status,ChasisNo,EngineNo,VHModel,VHColor,VHNumber,BeforeBuyRate,BeforeSalesRate,Barcode,Barcode1,Barcode2,Barcode3,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion)
SELECT p.ProductId PID,p.ProductDesc NepaliDesc,p.ProductDesc PName,p.GenericName PAlias,ISNULL(p.BarCodeNo1,p.ProductShortName) PShortName,'I' PType,'FG' PCategory,ISNULL(p.ProductUnitId,1) PUnit,p.ProductAltUnitId PAltUnit,p.QtyConv PQtyConv,p.AltConv PAltConv,p.ValuationTech PValTech,0 PSerialno,0 PSizewise,0 PBatchwise,0 PVehicleWise,0 PublicationWise,p.BuyRate PBuyRate,p.SalesRate * p.QtyConv AltSalesRate,p.SalesRate PSalesRate,p.Margin1 PMargin1,TradeRate,p.Margin2 PMargin2,p.MRP PMRP,p.ProductGrpId PGrpId,p.ProductSubGrpId PSubGrpId,p.TaxRate PTax,p.MinStock PMin,p.MaxStock PMax,p.DepartmentId CmpId,p.DepartmentId1 CmpId1,p.DepartmentId2 CmpId2,p.DepartmentId3 CmpId3,1 Branch_Id,NULL CmpUnit_Id,p.PurchaseLedgerId PPL,p.PurchaseReturnLedgerId PPR,p.SalesLedgerId PSL,p.SalesReturnLedgerId PSR,p.PLOpeningLedgerId PL_Opening,p.PLClosingLedgerId PL_Closing,p.BSClosingLedgerId BS_Closing,PImage,EnterBy,EnterDate,0 IsDefault,Status,NULL ChasisNo,NULL EngineNo,NULL VHModel,NULL VHColor,NULL VHNumber,p.BuyRate BeforeBuyRate,p.SalesRate BeforeSalesRate,p.BarCodeNo2 Barcode,NULL Barcode1,NULL Barcode2,NULL Barcode3,NULL SyncBaseId,NULL SyncGlobalId,NULL SyncOriginId,NULL SyncCreatedOn,NULL SyncLastPatchedOn,1 SyncRowVersion FROM PRAN798001.ERP.Product p
WHERE p.ProductId NOT IN (SELECT p1.PID FROM AMS.Product p1)

INSERT INTO AMS.ST_Term(ST_ID,Order_No,ST_Name,Module,ST_Type,ST_Basis,ST_Sign,ST_Condition,Ledger,ST_Rate,ST_Branch,ST_CompanyUnit,ST_Profitability,ST_Supess,ST_Status,EnterBy,EnterDate,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion)
SELECT sbt.TermId ST_ID,sbt.TermPosition Order_No,sbt.TermDesc ST_Name,'SB' Module,sbt.TermType ST_Type,sbt.Basis ST_Basis,sbt.STSign ST_Sign,SUBSTRING(sbt.Category,1,1) ST_Condition,sbt.LedgerId Ledger,sbt.TermRate ST_Rate,1 ST_Branch,NULL ST_CompanyUnit,sbt.Profitability ST_Profitability,sbt.SupressZero ST_Supess,sbt.Status ST_Status,EnterBy,EnterDate,NULL SyncBaseId,NULL SyncGlobalId,NULL SyncOriginId,NULL SyncCreatedOn,NULL SyncLastPatchedOn,1 SyncRowVersion FROM PRAN798001.ERP.SalesBillingTerm sbt
WHERE sbt.TermId NOT IN (SELECT st.ST_ID FROM AMS.ST_Term st)

INSERT INTO AMS.PT_Term(PT_Id,Order_No,PT_Name,Module,PT_Type,PT_Basis,PT_Sign,PT_Condition,Ledger,PT_Rate,PT_Branch,PT_CompanyUnit,PT_Profitability,PT_Supess,PT_Status,EnterBy,EnterDate,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion)
SELECT pbt.TermId PT_Id,pbt.TermPosition Order_No,pbt.TermDesc PT_Name,'PB' Module,pbt.TermType PT_Type,pbt.Basis PT_Basis,pbt.PTSign PT_Sign,SUBSTRING(pbt.Category,1,1) PT_Condition,pbt.LedgerId Ledger,pbt.TermRate PT_Rate,1 PT_Branch,NULL PT_CompanyUnit,pbt.StockValuation PT_Profitability,pbt.SupressZero PT_Supess,pbt.Status PT_Status,EnterBy,EnterDate,NULL SyncBaseId,NULL SyncGlobalId,NULL SyncOriginId,NULL SyncCreatedOn,NULL SyncLastPatchedOn,1 SyncRowVersion FROM PRAN798001.ERP.PurchaseBillingTerm pbt
WHERE pbt.TermId NOT IN (SELECT pt.PT_Id FROM AMS.PT_Term pt)


INSERT INTO AMS.PostDateCheque(VoucherNo,VoucherDate,VoucherTime,VoucherMiti,BankLedgerId,VoucherType,Status,BankName,BranchName,ChequeNo,ChqDate,ChqMiti,DrawOn,Amount,LedgerId,SubledgerId,AgentId,Remarks,DepositedBy,DepositeDate,IsReverse,CancelReason,CancelBy,CancelDate,Cls1,Cls2,Cls3,Cls4,BranchId,CompanyUnitId,FiscalYearId,EnterBy,EnterDate,PAttachment1,PAttachment2,PAttachment3,PAttachment4,PAttachment5,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion,IsSynced)
SELECT VoucherNo,p.VDate VoucherDate,p.EnterDate VoucherTime,p.VMiti VoucherMiti,ISNULL(BankLedgerId,(SELECT TOP 1 fs.PDCBankLedgerId FROM AMS.FinanceSetting fs)),p.DepositType VoucherType,p.Status,BankName,BranchName,ChequeNo,p.ChequeDate ChqDate,p.ChequeMiti ChqMiti,gl.GLName DrawOn,Amount,LedgerId,SubledgerId,AgentId,Remarks,NULL DepositedBy,NULL DepositeDate,0 IsReverse,CancelReason,CancelBy,CancelDate,p.DepartmentId1 Cls1,p.DepartmentId2 Cls2,p.DepartmentId3 Cls3,p.DepartmentId4 Cls4,1 BranchId,CompanyUnitId,13 FiscalYearId,p.EnterBy,p.EnterDate,NULL PAttachment1,NULL PAttachment2,NULL PAttachment3,NULL PAttachment4,NULL PAttachment5,NULL SyncBaseId,NULL SyncGlobalId,NULL SyncOriginId,NULL SyncCreatedOn,NULL SyncLastPatchedOn,1 SyncRowVersion,0 IsSynced FROM PRAN798001.ERP.PDC p
	 LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID = p.LedgerId
WHERE p.VoucherNo NOT IN (SELECT pdc.VoucherNo FROM AMS.PostDateCheque pdc)


INSERT INTO AMS.CB_Master(VoucherMode,Voucher_No,Voucher_Date,Voucher_Miti,Voucher_Time,Ref_VNo,Ref_VDate,VoucherType,Ledger_Id,CheqNo,CheqDate,CheqMiti,Currency_Id,Currency_Rate,Cls1,Cls2,Cls3,Cls4,Remarks,Action_Type,EnterBy,EnterDate,ReconcileBy,ReconcileDate,Audit_Lock,ClearingBy,ClearingDate,PrintValue,CBranch_Id,CUnit_Id,FiscalYearId,PAttachment1,PAttachment2,PAttachment3,PAttachment4,PAttachment5,IsReverse,CancelBy,CancelDate,CancelReason,In_Words,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion,CancelRemarks,IsSynced)
SELECT 'CONTRA' VoucherMode,cbm.VoucherNo Voucher_No,cbm.VDate Voucher_Date,cbm.VMiti Voucher_Miti,cbm.VTime Voucher_Time,cbm.ReferenceNo Ref_VNo,cbm.ReferenceDate Ref_VDate,'ALL' VoucherType,cbm.LedgerId Ledger_Id,cbm.ChequeNo CheqNo,cbm.ChequeDate CheqDate,cbm.ChequeMiti CheqMiti,ISNULL(cbm.CurrencyId,1) Currency_Id,cbm.CurrencyRate Currency_Rate,cbm.DepartmentId1 Cls1,cbm.DepartmentId2 Cls2,cbm.DepartmentId3 Cls3,cbm.DepartmentId4 Cls4,Remarks,'NEW' Action_Type,EnterBy,EnterDate,ReconcileBy,ReconcileDate,0 Audit_Lock,NULL ClearingBy,NULL ClearingDate,0 PrintValue,1 CBranch_Id,NULL CUnit_Id,13 FiscalYearId,NULL PAttachment1,NULL PAttachment2,NULL PAttachment3,NULL PAttachment4,NULL PAttachment5,0 IsReverse,CancelBy,CancelDate,NULL CancelReason,NULL In_Words,NULL SyncBaseId,NULL SyncGlobalId,NULL SyncOriginId,NULL SyncCreatedOn,NULL SyncLastPatchedOn,1 SyncRowVersion,NULL CancelRemarks,0 IsSynced FROM PRAN798001.ERP.CashBankMaster cbm
WHERE cbm.VoucherNo NOT IN (SELECT cm.Voucher_No FROM AMS.CB_Master cm)


INSERT INTO AMS.CB_Details(Voucher_No,Sno,CBLedgerId,Ledger_Id,Subledger_Id,Agent_Id,Cls1,Cls2,Cls3,Cls4,CurrencyId,CurrencyRate,Debit,Credit,LocalDebit,LocalCredit,Tbl_Amount,V_Amount,Narration,Party_No,Invoice_Date,Invoice_Miti,VatLedger_Id,PanNo,Vat_Reg,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion)
SELECT cbd.VoucherNo Voucher_No,Sno,cm.Ledger_Id CBLedgerId,cbd.LedgerId Ledger_Id,cbd.SubledgerId Subledger_Id,cbd.SalesmanId Agent_Id,cbd.DepartmentIdDet1 Cls1,cbd.DepartmentIdDet2 Cls2,cbd.DepartmentIdDet3 Cls3,cbd.DepartmentIdDet4 Cls4,ISNULL(cm.Currency_Id,1) CurrencyId,cm.Currency_Rate CurrencyRate,cbd.PayAmt Debit,cbd.RecAmt Credit,cbd.PayLocalAmt LocalDebit,cbd.RecLocalAmt LocalCredit,0 Tbl_Amount,0 V_Amount,cbd.Naration Narration,NULL Party_No,NULL Invoice_Date,NULL Invoice_Miti,NULL VatLedger_Id,NULL PanNo,NULL Vat_Reg,NULL SyncBaseId,NULL SyncGlobalId,NULL SyncOriginId,NULL SyncCreatedOn,SyncLastPatchedOn,1 SyncRowVersion FROM PRAN798001.ERP.CashBankDetails cbd
	 LEFT OUTER JOIN AMS.CB_Master cm ON cbd.VoucherNo = cm.Voucher_No
WHERE cbd.VoucherNo NOT IN (SELECT cd.Voucher_No FROM AMS.CB_Details cd)


INSERT INTO AMS.SB_Master (SB_Invoice,Invoice_Date,Invoice_Miti,Invoice_Time,PB_Vno,Vno_Date,Vno_Miti,Customer_Id,PartyLedgerId,Party_Name,Vat_No,Contact_Person,Mobile_No,Address,ChqNo,ChqDate,ChqMiti,Invoice_Type,Invoice_Mode,Payment_Mode,DueDays,DueDate,Agent_Id,Subledger_Id,SO_Invoice,SO_Date,SC_Invoice,SC_Date,Cls1,Cls2,Cls3,Cls4,CounterId,Cur_Id,Cur_Rate,B_Amount,T_Amount,N_Amount,LN_Amount,V_Amount,Tbl_Amount,Tender_Amount,Return_Amount,Action_Type,In_Words,Remarks,R_Invoice,Cancel_By,Cancel_Date,Cancel_Remarks,Is_Printed,No_Print,Printed_By,Printed_Date,Audit_Lock,Enter_By,Enter_Date,Reconcile_By,Reconcile_Date,Auth_By,Auth_Date,Cleared_By,Cleared_Date,DoctorId,PatientId,HDepartmentId,MShipId,TableId,CBranch_Id,CUnit_Id,FiscalYearId,IsAPIPosted,IsRealtime,PAttachment1,PAttachment2,PAttachment3,PAttachment4,PAttachment5,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion,IsSynced)
SELECT * FROM PRAN798001.


SELECT * FROM PRAN798001.ERP.CashBankDetails cbd
WHERE cbd.VoucherNo = 'PV-000006'
--SELECT DISTINCT ft.Source FROM PRAN798001.ERP.FinanceTransaction ft 
--SELECT DISTINCT it.Source FROM PRAN798001.ERP.InventoryTransaction it
SELECT * FROM MR_MANDAL01.AMS.CB_Master cm



SELECT * FROM PRAN798001.ERP.InventoryExpBrkDetails iebd
