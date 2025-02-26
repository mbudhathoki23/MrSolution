INSERT INTO MR_RISHAV01.AMS.AccountGroup(GrpId, PrimaryGroupId, GrpName, GrpCode, Schedule, PrimaryGrp, GrpType, IsDefault, NepaliDesc, Branch_ID, Company_Id, Status, EnterBy, EnterDate, SyncBaseId)
SELECT RowNum=ROW_NUMBER() OVER (ORDER BY ag.GrpDesc), NULL, ag.GrpDesc, ag.GrpCode, ag.GrpSchedule, CASE WHEN ag.PrimaryGrp IN ('Expenditure', 'Income') THEN 'Profit & Loss' ELSE 'Balance Sheet' END, ag.PrimaryGrp, 0, ag.GrpDesc, 1, NULL, 1, 'MrSolution', GETDATE(), NEWID()
FROM AccountGroup ag
WHERE ag.GrpCode NOT IN(SELECT ag1.GrpCode FROM MR_RISHAV01.AMS.AccountGroup ag1);
INSERT INTO MR_RISHAV01.AMS.AccountSubGroup(SubGrpId, SubGrpName, Company_Id, PrimaryGroupId, PrimarySubGroupId, IsDefault, NepaliDesc, GrpId, SubGrpCode, Branch_ID, Status, EnterBy, EnterDate, SyncBaseId, SyncRowVersion)
SELECT RowNum=ROW_NUMBER() OVER (ORDER BY ag.sGrpDesc), ag.sGrpDesc, ag.GrpCode, NULL, NULL, 0, ag.sGrpDesc, ag1.GrpId, ag.sGrpCode, 1, 1, 'MrSolution', GETDATE(), NEWID(), 1
FROM AccountSubGroup ag
     LEFT OUTER JOIN MR_RISHAV01.AMS.AccountGroup ag1 ON ag.GrpCode=ag1.GrpCode
WHERE ag.GrpCode NOT IN(SELECT ag1.GrpCode FROM MR_RISHAV01.AMS.AccountGroup ag1);
INSERT INTO MR_RISHAV01.AMS.AccountSubGroup(SubGrpId, SubGrpName, Company_Id, PrimaryGroupId, PrimarySubGroupId, IsDefault, NepaliDesc, GrpId, SubGrpCode, Branch_ID, Status, EnterBy, EnterDate, SyncBaseId, SyncRowVersion)
SELECT RowNum=ROW_NUMBER() OVER (ORDER BY ag.sGrpDesc), ag.sGrpDesc, NULL, NULL, NULL, 0, ag.sGrpDesc, ag1.GrpId, ag.sGrpCode, 1, 1, 'MrSolution', GETDATE(), NEWID(), 1
FROM AccountSubGroup ag
     LEFT OUTER JOIN MR_RISHAV01.AMS.AccountGroup ag1 ON ag.GrpCode=ag1.GrpCode
WHERE ag.sGrpCode NOT IN(SELECT ag1.SubGrpCode FROM MR_RISHAV01.AMS.AccountSubGroup ag1);
INSERT INTO MR_RISHAV01.AMS.GeneralLedger(GLID, GLName, GLCode, ACCode, GLType, GrpId, SubGrpId, PanNo, AreaId, AgentId, CurrId, CrDays, CrLimit, CrTYpe, IntRate, GLAddress, PhoneNo, LandLineNo, OwnerName, OwnerNumber, Scheme, Email, Branch_ID, Company_Id, EnterBy, EnterDate, PrimaryGroupId, PrimarySubGroupId, IsDefault, NepaliDesc, Status, SyncBaseId)
SELECT RowNum=ROW_NUMBER() OVER (ORDER BY gl.GlDesc), gl.GlDesc, gl.GlCode, gl.GlCode, CASE WHEN gl.Catagory='Customer/Vendor' THEN 'Both'
                                                                                       WHEN gl.Catagory='Cash Book' THEN 'Cash'
                                                                                       WHEN gl.Catagory='Bank Book' THEN 'Bank' ELSE gl.Catagory END Catagory, ag.GrpId, asg.SubGrpId, gl.PanNo, NULL, NULL, 1, gl.CreditDay, gl.CreditLimite, gl.CreditType, gl.InterestRate, gl.AddressI, gl.TelNoI, gl.TelNoII, NULL, NULL, NULL, gl.Email, 1, NULL, 'MrSolution', GETDATE(), NULL, NULL, 0, gl.GlDesc, 1, NEWID()
FROM GeneralLedger gl
     LEFT OUTER JOIN MR_RISHAV01.AMS.AccountGroup ag ON gl.GrpCode=ag.GrpCode
     LEFT OUTER JOIN MR_RISHAV01.AMS.AccountSubGroup asg ON gl.SGrpCode=asg.SubGrpCode
WHERE gl.GlCode NOT IN(SELECT gl1.GLCode FROM MR_RISHAV01.AMS.GeneralLedger gl1);
INSERT INTO MR_RISHAV01.AMS.PT_Term(PT_Id, Order_No, Module, PT_Name, PT_Type, Ledger, PT_Basis, PT_Sign, PT_Condition, PT_Rate, PT_Branch, PT_CompanyUnit, PT_Profitability, PT_Supess, PT_Status, EnterBy, EnterDate)
SELECT pbt.PTCode, pbt.PTCode, 'PB', pbt.PTDesc, 1, gl.GLID, pbt.Basis, pbt.Sign, pbt.PTType, pbt.PTRate, 1, NULL, 0, 0, 1, 'MrSolution', GETDATE()
FROM PurchaseBillingTerm pbt
     LEFT OUTER JOIN MR_RISHAV01.AMS.GeneralLedger gl ON pbt.GLCode=gl.GLCode
WHERE pbt.PTCode NOT IN(SELECT ag1.PT_Id FROM MR_RISHAV01.AMS.PT_Term ag1);
INSERT INTO MR_RISHAV01.AMS.ST_Term(ST_Id, Order_No, Module, ST_Name, ST_Type, Ledger, ST_Basis, ST_Sign, ST_Condition, ST_Rate, ST_Branch, ST_CompanyUnit, ST_Profitability, ST_Supess, ST_Status, EnterBy, EnterDate)
SELECT pbt.PTCode, pbt.PTCode, 'PB', pbt.PTDesc, 1, gl.GLID, pbt.Basis, pbt.Sign, pbt.PTType, pbt.PTRate, 1, NULL, 0, 0, 1, 'MrSolution', GETDATE()
FROM SalesBillingTerm pbt
     LEFT OUTER JOIN MR_RISHAV01.AMS.GeneralLedger gl ON pbt.GLCode=gl.GLCode
WHERE pbt.PTCode NOT IN(SELECT ag1.ST_Id FROM MR_RISHAV01.AMS.ST_Term ag1);
INSERT INTO MR_RISHAV01.AMS.ProductUnit(UID, UnitName, UnitCode, Branch_ID, Company_Id, EnterBy, EnterDate, Status, SyncBaseId)
SELECT RowNum=ROW_NUMBER() OVER (ORDER BY u.UnitDesc), u.UnitDesc, u.UnitCode, 1, NULL, 'MrSolution', GETDATE(), 1, NEWID()
FROM Unit u
WHERE UnitCode NOT IN(SELECT ag1.UnitCode FROM MR_RISHAV01.AMS.ProductUnit ag1);
INSERT INTO MR_RISHAV01.AMS.Product(PID, PName, PAlias, PShortName, PType, PCategory, PUnit, PAltUnit, PQtyConv, PAltConv, PValTech, PSerialno, PSizewise, PBatchwise, PBuyRate, PSalesRate, PMargin1, TradeRate, PMargin2, PMRP, PGrpId, PSubGrpId, PTax, PMin, PMax, CmpId, CmpId1, CmpId2, CmpId3, Branch_ID, CmpUnit_ID, PPL, PPR, PSL, PSR, PL_Opening, PL_Closing, BS_Closing, PImage, EnterBy, EnterDate, Status, BeforeBuyRate, BeforeSalesRate, Barcode, ChasisNo, EngineNo, VHColor, VHModel, VHNumber, Barcode1, Barcode2, Barcode3, SyncBaseId)
SELECT RowNum=ROW_NUMBER() OVER (ORDER BY p.PDesc), p.PDesc, p.PDesc, p.PCode, 'Inventory', 'Finished Goods', ISNULL(pu.UID, 3), NULL, 0, 0, 'FIFO', 0, 0, 0, p.BuyRate, p.SalesRate, p.Margin, p.TradeRate, p.Margin, p.MRP, p.GrpCode, p.sGrpCode, p.Vat, p.MinBonusQty, p.MaxStock, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'MrSolution', GETDATE(), 1, 0, 0, p.PShortName, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NEWID()
FROM Product p
     LEFT OUTER JOIN MR_RISHAV01.AMS.ProductUnit pu ON p.UnitCode=pu.UnitCode
WHERE p.PCode NOT IN(SELECT ag1.PShortName FROM MR_RISHAV01.AMS.Product ag1);
INSERT INTO MR_RISHAV01.AMS.PB_Master(PB_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, PB_Vno, Vno_Date, Vno_Miti, Vendor_ID, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, Invoice_Type, Invoice_In, DueDays, DueDate, Agent_ID, Subledger_Id, PO_Invoice, PO_Date, PC_Invoice, PC_Date, Cls1, Cls2, Cls3, Cls4, Cur_Id, Cur_Rate, Counter_ID, B_Amount, T_Amount, N_Amount, LN_Amount, Tender_Amount, Change_Amount, V_Amount, Tbl_Amount, Action_Type, R_Invoice, No_Print, In_Words, Remarks, Audit_Lock, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, CancelBy, CancelDate, CancelRemarks, CUnit_Id, CBranch_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, Enter_By, Enter_Date, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
SELECT pim.VNo, pim.VDate, pim.VMiti, pim.VTime, pim.RefNo, pim.RefDate, pim.RefMiti, gl.GLID, pim.PartyName, pim.VatNo, NULL, NULL, NULL, pim.ChqNo, pim.ChqDate, pim.Invtype, 'Credit', pim.DueDay, pim.DueDate, gl.AgentId, pim.SLCode, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, ISNULL(pim.CurrCode, 1), pim.CurrRate, NULL, pim.BasicAmt, pim.TermAmt, pim.NetAmt, pim.NetAmt, 0, 0, 0, 0, 'NEW', 0, 0, NULL, pim.Remarks, 0, pim.Reconcile, pim.ReconcileDate, pim.Authorize, pim.AuthDate, NULL, NULL, NULL, NULL, NULL, NULL, 1, 10, NULL, NULL, NULL, NULL, NULL, 'MrSolution', GETDATE(), NULL, NULL, NULL, NULL, 1, NEWID()
FROM PurchaseInvoiceMaster pim
     LEFT OUTER JOIN MR_RISHAV01.AMS.GeneralLedger gl ON pim.GLCode=gl.GLCode
WHERE pim.VNo NOT IN(SELECT pm.PB_Invoice FROM MR_RISHAV01.AMS.PB_Master pm);
INSERT INTO MR_RISHAV01.AMS.PB_Details(PB_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, PO_Invoice, PO_Sno, PC_Invoice, PC_SNo, Tax_Amount, V_Amount, V_Rate, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, P_Ledger, PR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, TaxExempted_Amount, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
SELECT PID.VNo, PID.Sno, p.PID, PID.GodownCode, PID.AltQty, p.PAltUnit, PID.Qty, p.PUnit, PID.CurrRate, PID.BasicAmt, PID.TermAmt, PID.NetAmt, PID.AltQty, PID.Qty, PID.Narration, NULL, NULL, NULL, NULL, 0, 0, p.PTax, 0, 0, 0, 0, NULL, 0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, 1, NEWID()
FROM PurchaseInvoiceDetails PID
     LEFT OUTER JOIN MR_RISHAV01.AMS.Product p ON PID.Pcode=p.PShortName
WHERE PID.VNo NOT IN(SELECT pm.PB_Invoice FROM MR_RISHAV01.AMS.PB_Details pm);
INSERT INTO MR_RISHAV01.AMS.PB_Term(PB_Vno, PT_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
SELECT pit.VNo, pit.STCode, pit.SNo, pit.CurrRate, pit.CurrAmt, pit.TrmType, p.PID, CASE WHEN p.PTax IS NULL OR CAST(p.PTax AS DECIMAL)=0 AND pit.PCode IS NOT NULL THEN 'N'
                                                                                    WHEN p.PTax IS NULL OR CAST(p.PTax AS DECIMAL)>0 AND pit.PCode IS NOT NULL THEN 'Y' ELSE NULL END, NULL, NULL, NULL, NULL, 1, NEWID()
FROM PurchaseInvoiceTerm pit
     LEFT OUTER JOIN MR_RISHAV01.AMS.Product p ON pit.Pcode=p.PShortName
WHERE pit.VNo NOT IN(SELECT pt.PB_Vno FROM MR_RISHAV01.AMS.PB_Term pt);
INSERT INTO MR_RISHAV01.AMS.SB_Master(SB_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, PB_Vno, Vno_Date, Vno_Miti, Customer_ID, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_ID, Subledger_Id, SO_Invoice, SO_Date, SC_Invoice, SC_Date, Cls1, Cls2, Cls3, Cls4, CounterId, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Tender_Amount, Return_Amount, Action_Type, In_Words, Remarks, R_Invoice, Is_Printed, No_Print, Printed_By, Printed_Date, Audit_Lock, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, Cancel_By, Cancel_Date, Cancel_Remarks, CUnit_Id, CBranch_Id, IsAPIPosted, IsRealtime, FiscalYearId, DoctorId, PatientId, HDepartmentId, TableId, MShipId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, Enter_By, Enter_Date, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
SELECT sim.VNo, sim.VDate, sim.VMiti, sim.VTime, sim.RefNo, sim.RefDate, sim.RefMiti, gl.GLID, sim.PartyName, sim.VatNo, NULL, NULL, NULL, sim.ChqNo, sim.ChqDate, 'Local', 'Normal', 'Credit', sim.DueDay, sim.DueDate, NULL, NULL, sim.MOrderNo, sim.MOrderDate, sim.MChallanNo, sim.MChallanDate, NULL, NULL, NULL, NULL, NULL, 1, 1, sim.BasicAmt, sim.TermAmt, sim.NetAmt, sim.NetAmt, 0, 0, 0, 0, 'NEW', NULL, sim.Remarks, 0, 0, 0, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 0, 0, 10, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'MrSolution', GETDATE(), NULL, NULL, NULL, NULL, 1, NEWID()
FROM SalesInvoiceMaster sim
     LEFT OUTER JOIN MR_RISHAV01.AMS.GeneralLedger gl ON sim.GLCode=gl.GLCode
WHERE sim.VNo NOT IN(SELECT pm.SB_Invoice FROM MR_RISHAV01.AMS.SB_Master pm);
INSERT INTO MR_RISHAV01.AMS.SB_Details(SB_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, SO_Invoice, SO_Sno, SC_Invoice, SC_SNo, Tax_Amount, V_Amount, V_Rate, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, PDiscountRate, PDiscount, BDiscountRate, BDiscount, ServiceChargeRate, ServiceCharge, MaterialPost, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
SELECT PID.VNo, PID.Sno, p.PID, PID.GodownCode, PID.AltQty, p.PAltUnit, PID.Qty, p.PUnit, PID.CurrRate, PID.BasicAmt, PID.TermAmt, PID.LocalAmt, PID.AltQty, PID.Qty, PID.Narration, NULL, NULL, NULL, NULL, 0, 0, p.PTax, 0, 0, 0, 0, NULL, 0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, NULL, NULL, NULL, NULL, 1, NEWID()
FROM SalesInvoiceDetails PID
     LEFT OUTER JOIN MR_RISHAV01.AMS.Product p ON PID.Pcode=p.PShortName
WHERE PID.VNo NOT IN(SELECT pm.SB_Invoice FROM MR_RISHAV01.AMS.SB_Details pm);
INSERT INTO MR_RISHAV01.AMS.SB_Term(SB_VNo, ST_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
SELECT pit.VNo, pit.STCode, pit.SNo, pit.CurrRate, pit.CurrAmt, pit.TrmType, p.PID, CASE WHEN p.PTax IS NULL OR CAST(p.PTax AS DECIMAL)=0 AND pit.PCode IS NOT NULL THEN 'N'
                                                                                    WHEN p.PTax IS NULL OR CAST(p.PTax AS DECIMAL)>0 AND pit.PCode IS NOT NULL THEN 'Y' ELSE NULL END, NULL, NULL, NULL, NULL, 1, NEWID()
FROM SalesInvoiceTerm pit
     LEFT OUTER JOIN MR_RISHAV01.AMS.Product p ON pit.Pcode=p.PShortName
WHERE pit.VNo NOT IN(SELECT pt.SB_Vno FROM MR_RISHAV01.AMS.SB_Term pt);
INSERT INTO MR_RISHAV01.AMS.LedgerOpening(Opening_Id, Module, Voucher_No, OP_Date, OP_Miti, Serial_No, Ledger_ID, Subledger_Id, Agent_ID, Cls1, Cls2, Cls3, Cls4, Currency_ID, Currency_Rate, Debit, LocalDebit, Credit, LocalCredit, Narration, Remarks, Reconcile_By, Reconcile_Date, Branch_ID, Company_Id, FiscalYearId, Enter_By, Enter_Date)
SELECT 1, 'LOB', od.VNo, om.VDate, om.VMiti, 1, gl.GLID, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, od.DrCurrAmt, od.DrLocalAmt, od.CrCurrAmt, od.CrLocalAmt, od.Narration, om.Remarks, NULL, NULL, 1, NULL, 10, 'MrSolution', GETDATE()
FROM OpeningDetails od
     LEFT OUTER JOIN OpeningMaster om ON od.VNo=om.VNo
     LEFT OUTER JOIN MR_RISHAV01.AMS.GeneralLedger gl ON od.GLCode=gl.GlCode
WHERE od.Vno NOT IN(SELECT Voucher_No FROM MR_RISHAV01.AMS.LedgerOpening);
INSERT INTO MR_RISHAV01.AMS.Notes_Master(VoucherMode, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ref_VNo, Ref_VDate, VoucherType, Ledger_ID, CheqNo, CheqDate, CheqMiti, Subledger_Id, Agent_ID, Currency_ID, Currency_Rate, Cls1, Cls2, Cls3, Cls4, Remarks, Action_Type, ReconcileBy, ReconcileDate, Audit_Lock, ClearingBy, ClearingDate, PrintValue, CBranch_Id, CUnit_Id, FiscalYearId, EnterBy, EnterDate)
SELECT 'CN', cnm.VNo, cnm.VDate, cnm.VMiti, cnm.VTime, cnm.RefNo, cnm.RefDate, 'CN', gl.GLID, NULL, NULL, NULL, cnm.SLCode, cnm.AgentCode, 1, 1, NULL, NULL, NULL, NULL, cnm.Remarks, 'NEW', NULL, NULL, 0, NULL, NULL, 0, 1, NULL, 10, 'MrSolution', GETDATE()
FROM CreditNoteMaster cnm
     LEFT OUTER JOIN MR_RISHAV01.AMS.GeneralLedger gl ON cnm.GLCode=gl.GlCode
WHERE cnm.Vno NOT IN(SELECT Voucher_No FROM MR_RISHAV01.AMS.Notes_Master);
INSERT INTO MR_RISHAV01.AMS.Notes_Details(VoucherMode, Voucher_No, SNo, Ledger_ID, Subledger_Id, Agent_ID, Cls1, Cls2, Cls3, Cls4, Debit, Credit, LocalDebit, LocalCredit, Narration, T_Amount, V_Amount, Party_No, Invoice_Date, Invoice_Miti, VatLedger_Id, PanNo, Vat_Reg)
SELECT 'CN', cnm.VNo, cnm.SNo, gl.GLID, cnm.SLCode, NULL, NULL, NULL, NULL, NULL, 0, cnm.CurrAmt, 0, cnm.CurrAmt, cnm.Narration, 0, 0, NULL, NULL, NULL, NULL, NULL, NULL
FROM CreditNoteDetails cnm
     LEFT OUTER JOIN MR_RISHAV01.AMS.GeneralLedger gl ON cnm.GLCode=gl.GlCode
WHERE cnm.Vno NOT IN(SELECT Voucher_No FROM MR_RISHAV01.AMS.Notes_Details);
INSERT INTO MR_RISHAV01.AMS.CB_Master(VoucherMode, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ref_VNo, Ref_VDate, VoucherType, Ledger_ID, CheqNo, CheqDate, CheqMiti, Currency_ID, Currency_Rate, Cls1, Cls2, Cls3, Cls4, Remarks, Action_Type, ReconcileBy, ReconcileDate, Audit_Lock, ClearingBy, ClearingDate, PrintValue, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, EnterBy, EnterDate)
SELECT 'ALL', cnm.VNo, cnm.VDate, cnm.VMiti, cnm.VTime, cnm.RefNo, cnm.RefDate, 'CN', gl.GLID, cnm.ChqNo, cnm.ChqDate, cnm.ChqMiti, 1, 1, NULL, NULL, NULL, NULL, cnm.Remarks, 'NEW', NULL, NULL, 0, NULL, NULL, 0, 1, NULL, 10, NULL, NULL, NULL, NULL, NULL, 'MrSolution', GETDATE()
FROM CashBankMaster cnm
     LEFT OUTER JOIN MR_RISHAV01.AMS.GeneralLedger gl ON cnm.GLCode=gl.GlCode
WHERE cnm.Vno NOT IN(SELECT Voucher_No FROM MR_RISHAV01.AMS.CB_Master);
INSERT INTO MR_RISHAV01.AMS.CB_Details(Voucher_No, SNo, Ledger_ID, Subledger_Id, Agent_ID, Cls1, Cls2, Cls3, Cls4, Debit, Credit, LocalDebit, LocalCredit, Narration, Tbl_Amount, V_Amount, Party_No, Invoice_Date, Invoice_Miti, VatLedger_Id, PanNo, Vat_Reg, CBLedgerId, CurrencyId, CurrencyRate)
SELECT cnm.VNo, cnm.SNo, gl.GLID, cnm.SLCode, cnm.AgentCode, NULL, NULL, NULL, NULL, cnm.RecCurrAmt, cnm.PayCurrAmt, cnm.RecLocalAmt, cnm.PayLocalAmt, cnm.Narration, 0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
FROM CashBankDetails cnm
     LEFT OUTER JOIN MR_RISHAV01.AMS.GeneralLedger gl ON cnm.GLCode=gl.GlCode
WHERE cnm.Vno NOT IN(SELECT Voucher_No FROM MR_RISHAV01.AMS.CB_Details);
