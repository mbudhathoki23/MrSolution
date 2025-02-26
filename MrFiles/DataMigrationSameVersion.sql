--
INSERT INTO AMS.Currency(CId, NepaliDesc, CName, CCode, CRate, Branch_ID, Company_Id, Status, IsDefault, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, BuyRate)
SELECT CId, NepaliDesc, CName, CCode, CRate, Branch_ID, Company_Id, Status, IsDefault, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, BuyRate
FROM MR_BHABSA01.AMS.Currency
WHERE CId NOT IN(SELECT CId FROM AMS.Currency);

-- 
INSERT INTO AMS.AccountGroup(GrpId, NepaliDesc, GrpName, GrpCode, Schedule, PrimaryGrp, GrpType, Branch_ID, Company_Id, Status, EnterBy, EnterDate, PrimaryGroupId, IsDefault, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
SELECT GrpId, NepaliDesc, GrpName, GrpCode, Schedule, PrimaryGrp, GrpType, Branch_ID, Company_Id, Status, EnterBy, EnterDate, PrimaryGroupId, IsDefault, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
FROM MR_BHABSA01.AMS.AccountGroup
WHERE GrpId NOT IN(SELECT GrpId FROM AMS.AccountGroup);

--
INSERT INTO AMS.AccountSubGroup(SubGrpId, NepaliDesc, SubGrpName, GrpId, SubGrpCode, Branch_ID, Company_Id, Status, EnterBy, EnterDate, PrimaryGroupId, PrimarySubGroupId, IsDefault, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
SELECT SubGrpId, NepaliDesc, SubGrpName, GrpId, SubGrpCode, Branch_ID, Company_Id, Status, EnterBy, EnterDate, PrimaryGroupId, PrimarySubGroupId, IsDefault, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
FROM MR_BHABSA01.AMS.AccountSubGroup
WHERE SubGrpId NOT IN(SELECT SubGrpId FROM AMS.AccountSubGroup);

--
INSERT INTO AMS.GeneralLedger(GLID, NepaliDesc, GLName, GLCode, ACCode, GLType, GrpId, PrimaryGroupId, SubGrpId, PrimarySubGroupId, PanNo, AreaId, AgentId, CurrId, CrDays, CrLimit, CrTYpe, IntRate, GLAddress, PhoneNo, LandLineNo, OwnerName, OwnerNumber, Scheme, Email, Branch_ID, Company_Id, EnterBy, EnterDate, Status, IsDefault, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
SELECT GLID, NepaliDesc, GLName, GLCode, ACCode, GLType, GrpId, PrimaryGroupId, SubGrpId, PrimarySubGroupId, PanNo, AreaId, AgentId, CurrId, CrDays, CrLimit, CrTYpe, IntRate, GLAddress, PhoneNo, LandLineNo, OwnerName, OwnerNumber, Scheme, Email, Branch_ID, Company_Id, EnterBy, EnterDate, Status, IsDefault, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
FROM MR_BHABSA01.AMS.GeneralLedger
WHERE GLID NOT IN(SELECT GLID FROM AMS.GeneralLedger);

--
INSERT INTO AMS.Subledger(SLId, NepalDesc, SLName, SLCode, SLAddress, SLPhoneNo, GLID, Branch_ID, Company_Id, Status, IsDefault, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
SELECT SLId, NepalDesc, SLName, SLCode, SLAddress, SLPhoneNo, GLID, Branch_ID, Company_Id, Status, IsDefault, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
FROM MR_BHABSA01.AMS.Subledger
WHERE SLId NOT IN(SELECT SLId FROM AMS.Subledger);

--
INSERT INTO AMS.JuniorAgent(AgentId, NepaliDesc, AgentName, AgentCode, Address, PhoneNo, GLCode, Commission, CrLimit, CrDays, CrTYpe, TargetLimit, SAgent, Email, Branch_ID, Company_Id, EnterBy, EnterDate, Status, IsDefault, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
SELECT AgentId, NepaliDesc, AgentName, AgentCode, Address, PhoneNo, GLCode, Commission, CrLimit, CrDays, CrTYpe, TargetLimit, SAgent, Email, Branch_ID, Company_Id, EnterBy, EnterDate, Status, IsDefault, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
FROM MR_BHABSA01.AMS.JuniorAgent
WHERE AgentId NOT IN(SELECT AgentId FROM AMS.JuniorAgent);

--
INSERT INTO AMS.Area(AreaId, NepaliDesc, AreaName, AreaCode, Country, Branch_ID, Company_Id, Main_Area, Status, IsDefault, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
SELECT AreaId, NepaliDesc, AreaName, AreaCode, Country, Branch_ID, Company_Id, Main_Area, Status, IsDefault, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
FROM MR_BHABSA01.AMS.Area
WHERE AreaId NOT IN(SELECT AreaId FROM AMS.Area);

--
INSERT INTO AMS.ProductUnit(UID, NepaliDesc, UnitName, UnitCode, Branch_ID, Company_Id, EnterBy, EnterDate, Status, IsDefault, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
SELECT UID, NepaliDesc, UnitName, UnitCode, Branch_ID, Company_Id, EnterBy, EnterDate, Status, IsDefault, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
FROM MR_BHABSA01.AMS.ProductUnit
WHERE UID NOT IN(SELECT UID FROM AMS.ProductUnit);

--
INSERT INTO AMS.ProductGroup(PGrpId, NepaliDesc, GrpName, GrpCode, GMargin, Gprinter, PurchaseLedgerId, PurchaseReturnLedgerId, SalesLedgerId, SalesReturnLedgerId, OpeningStockLedgerId, ClosingStockLedgerId, StockInHandLedgerId, Branch_ID, Company_Id, Status, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
SELECT PGrpId, NepaliDesc, GrpName, GrpCode, GMargin, Gprinter, PurchaseLedgerId, PurchaseReturnLedgerId, SalesLedgerId, SalesReturnLedgerId, OpeningStockLedgerId, ClosingStockLedgerId, StockInHandLedgerId, Branch_ID, Company_Id, Status, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
FROM MR_BHABSA01.AMS.ProductGroup
WHERE PGrpId NOT IN(SELECT PGrpId FROM AMS.ProductGroup);

--
INSERT INTO AMS.ProductSubGroup(PSubGrpId, NepaliDesc, SubGrpName, ShortName, GrpId, Branch_ID, Company_Id, EnterBy, EnterDate, IsDefault, Status, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
SELECT PSubGrpId, NepaliDesc, SubGrpName, ShortName, GrpId, Branch_ID, Company_Id, EnterBy, EnterDate, IsDefault, Status, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
FROM MR_BHABSA01.AMS.ProductSubGroup
WHERE PSubGrpId NOT IN(SELECT PSubGrpId FROM AMS.ProductSubGroup);

--
INSERT INTO AMS.Product(PID, NepaliDesc, PName, PAlias, PShortName, PType, PCategory, PUnit, PAltUnit, PQtyConv, PAltConv, PValTech, PSerialno, PSizewise, PBatchwise, PVehicleWise, PublicationWise, PBuyRate, AltSalesRate, PSalesRate, PMargin1, TradeRate, PMargin2, PMRP, PGrpId, PSubGrpId, PTax, PMin, PMax, CmpId, CmpId1, CmpId2, CmpId3, Branch_Id, CmpUnit_Id, PPL, PPR, PSL, PSR, PL_Opening, PL_Closing, BS_Closing, PImage, EnterBy, EnterDate, IsDefault, Status, ChasisNo, EngineNo, VHModel, VHColor, VHNumber, BeforeBuyRate, BeforeSalesRate, Barcode, Barcode1, Barcode2, Barcode3, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, Image, HsCode)
SELECT PID, NepaliDesc, PName, PAlias, PShortName, PType, PCategory, PUnit, PAltUnit, PQtyConv, PAltConv, PValTech, PSerialno, PSizewise, PBatchwise, ISNULL(PVehicleWise, 0), ISNULL(PublicationWise, 0), PBuyRate, ISNULL(AltSalesRate, 0), PSalesRate, PMargin1, TradeRate, PMargin2, PMRP, PGrpId, PSubGrpId, PTax, PMin, PMax, CmpId, CmpId1, CmpId2, CmpId3, Branch_Id, CmpUnit_Id, PPL, PPR, PSL, PSR, PL_Opening, PL_Closing, BS_Closing, PImage, EnterBy, EnterDate, IsDefault, Status, ChasisNo, EngineNo, VHModel, VHColor, VHNumber, BeforeBuyRate, BeforeSalesRate, Barcode, Barcode1, Barcode2, Barcode3, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, Image, HsCode
FROM MR_BHABSA01.AMS.Product
WHERE PID NOT IN(SELECT PID FROM AMS.Product);

--
INSERT INTO AMS.BarcodeList(ProductId, Barcode, SalesRate, MRP, Trade, Wholesale, Retail, Dealer, Resellar, UnitId, AltUnitId, DailyRateChange)
SELECT ProductId, Barcode, SalesRate, MRP, Trade, Wholesale, Retail, Dealer, Resellar, UnitId, AltUnitId, DailyRateChange
FROM MR_BHABSA01.AMS.BarcodeList
WHERE Barcode NOT IN(SELECT Barcode COLLATE Latin1_General_CI_AS FROM AMS.BarcodeList);

--
INSERT INTO AMS.PT_Term(PT_Id, Order_No, PT_Name, Module, PT_Type, PT_Basis, PT_Sign, PT_Condition, Ledger, PT_Rate, PT_Branch, PT_CompanyUnit, PT_Profitability, PT_Supess, PT_Status, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
SELECT PT_Id, Order_No, PT_Name, Module, PT_Type, PT_Basis, PT_Sign, PT_Condition, Ledger, PT_Rate, PT_Branch, PT_CompanyUnit, PT_Profitability, PT_Supess, PT_Status, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
FROM MR_BHABSA01.AMS.PT_Term
WHERE PT_Id NOT IN(SELECT PT_Id FROM AMS.PT_Term);

--
INSERT INTO AMS.ST_Term(ST_ID, Order_No, ST_Name, Module, ST_Type, ST_Basis, ST_Sign, ST_Condition, Ledger, ST_Rate, ST_Branch, ST_CompanyUnit, ST_Profitability, ST_Supess, ST_Status, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
SELECT ST_ID, Order_No, ST_Name, Module, ST_Type, ST_Basis, ST_Sign, ST_Condition, Ledger, ST_Rate, ST_Branch, ST_CompanyUnit, ST_Profitability, ST_Supess, ST_Status, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
FROM MR_BHABSA01.AMS.ST_Term
WHERE ST_ID NOT IN(SELECT ST_ID FROM AMS.ST_Term);

--
INSERT INTO AMS.MemberType(MemberTypeId, NepaliDesc, MemberDesc, MemberShortName, Discount, BranchId, CompanyUnitId, EnterBy, EnterDate, ActiveStatus, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
SELECT MemberTypeId, NepaliDesc, MemberDesc, MemberShortName, Discount, BranchId, CompanyUnitId, EnterBy, EnterDate, ActiveStatus, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
FROM MR_BHABSA01.AMS.MemberType
WHERE MemberTypeId NOT IN(SELECT MemberTypeId FROM AMS.MemberType);

--
INSERT INTO AMS.MemberShipSetup(MShipId, MemberId, NepaliDesc, MShipDesc, MShipShortName, PhoneNo, PriceTag, LedgerId, EmailAdd, MemberTypeId, BranchId, CompanyUnitId, MValidDate, MExpireDate, EnterBy, EnterDate, ActiveStatus, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
SELECT MShipId, MemberId, NepaliDesc, MShipDesc, MShipShortName, PhoneNo, PriceTag, LedgerId, EmailAdd, MemberTypeId, BranchId, CompanyUnitId, MValidDate, MExpireDate, EnterBy, EnterDate, ActiveStatus, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
FROM MR_BHABSA01.AMS.MemberShipSetup
WHERE MShipId NOT IN(SELECT MShipId FROM AMS.MemberShipSetup);

--
INSERT INTO AMS.Counter(CId, CName, CCode, Branch_ID, Company_Id, Status, EnterBy, EnterDate, Printer, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
SELECT CId, CName, CCode, Branch_ID, Company_Id, Status, EnterBy, EnterDate, Printer, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
FROM MR_BHABSA01.AMS.Counter
WHERE CId NOT IN(SELECT CId FROM AMS.Counter);

--
INSERT INTO AMS.Floor(FloorId, Description, ShortName, Type, EnterBy, EnterDate, Branch_ID, Company_Id, Status, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
SELECT FloorId, Description, ShortName, Type, EnterBy, EnterDate, Branch_ID, Company_Id, Status, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
FROM MR_BHABSA01.AMS.Floor
WHERE FloorId NOT IN(SELECT FloorId FROM AMS.Floor);

--
INSERT INTO AMS.TableMaster(TableId, TableName, TableCode, FloorId, Branch_ID, Company_Id, TableStatus, TableType, IsPrePaid, Status, EnterBy, EnterDate, Printed, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
SELECT TableId, TableName, TableCode, FloorId, Branch_ID, Company_Id, TableStatus, TableType, IsPrePaid, Status, EnterBy, EnterDate, Printed, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
FROM MR_BHABSA01.AMS.TableMaster
WHERE TableId NOT IN(SELECT TableId FROM AMS.TableMaster);

--
INSERT INTO AMS.Godown(GID, NepaliDesc, GName, GCode, GLocation, Status, EnterBy, EnterDate, BranchUnit, CompUnit, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
SELECT GID, NepaliDesc, GName, GCode, GLocation, Status, EnterBy, EnterDate, BranchUnit, CompUnit, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
FROM MR_BHABSA01.AMS.Godown
WHERE GID NOT IN(SELECT GID FROM AMS.Godown);

--
INSERT INTO AMS.LedgerOpening(Opening_Id, Module, Serial_No, Voucher_No, OP_Date, OP_Miti, Ledger_Id, Subledger_Id, Agent_Id, Cls1, Cls2, Cls3, Cls4, Currency_Id, Currency_Rate, Debit, LocalDebit, Credit, LocalCredit, Narration, Remarks, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Branch_Id, Company_Id, FiscalYearId, IsReverse, CancelRemarks, CancelBy, CancelDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
SELECT Opening_Id, Module, Serial_No, Voucher_No, OP_Date, OP_Miti, Ledger_Id, Subledger_Id, Agent_Id, Cls1, Cls2, Cls3, Cls4, Currency_Id, Currency_Rate, Debit, LocalDebit, Credit, LocalCredit, Narration, Remarks, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Branch_Id, Company_Id, FiscalYearId, IsReverse, CancelRemarks, CancelBy, CancelDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
FROM MR_BHABSA01.AMS.LedgerOpening
WHERE Voucher_No NOT IN(SELECT Voucher_No COLLATE DATABASE_DEFAULT FROM AMS.LedgerOpening);

--
INSERT INTO AMS.ProductOpening(OpeningId, Voucher_No, Serial_No, OP_Date, OP_Miti, Product_Id, Godown_Id, Cls1, Cls2, Cls3, Cls4, Currency_Id, Currency_Rate, AltQty, AltUnit, Qty, QtyUnit, Rate, LocalRate, Amount, LocalAmount, IsReverse, CancelRemarks, CancelBy, CancelDate, Remarks, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, CBranch_Id, CUnit_Id, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
SELECT OpeningId, Voucher_No, Serial_No, OP_Date, OP_Miti, Product_Id, Godown_Id, Cls1, Cls2, Cls3, Cls4, Currency_Id, Currency_Rate, AltQty, AltUnit, Qty, QtyUnit, Rate, LocalRate, Amount, LocalAmount, IsReverse, CancelRemarks, CancelBy, CancelDate, Remarks, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, CBranch_Id, CUnit_Id, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
FROM MR_BHABSA01.AMS.ProductOpening
WHERE Voucher_No NOT IN(SELECT Voucher_No COLLATE DATABASE_DEFAULT FROM AMS.ProductOpening);

--
INSERT INTO AMS.CB_Master(VoucherMode, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ref_VNo, Ref_VDate, VoucherType, Ledger_Id, CheqNo, CheqDate, CheqMiti, Currency_Id, Currency_Rate, Cls1, Cls2, Cls3, Cls4, Remarks, Action_Type, EnterBy, EnterDate, ReconcileBy, ReconcileDate, Audit_Lock, ClearingBy, ClearingDate, PrintValue, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, IsReverse, CancelBy, CancelDate, CancelReason, In_Words, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, CancelRemarks, IsSynced)
SELECT VoucherMode, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ref_VNo, Ref_VDate, VoucherType, Ledger_Id, CheqNo, CheqDate, CheqMiti, Currency_Id, Currency_Rate, Cls1, Cls2, Cls3, Cls4, Remarks, Action_Type, EnterBy, EnterDate, ReconcileBy, ReconcileDate, Audit_Lock, ClearingBy, ClearingDate, PrintValue, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, IsReverse, CancelBy, CancelDate, CancelReason, In_Words, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, CancelRemarks, ISNULL(IsSynced,0)
FROM MR_BHABSA01.AMS.CB_Master
WHERE Voucher_No NOT IN(SELECT Voucher_No COLLATE DATABASE_DEFAULT FROM AMS.CB_Master);

INSERT INTO AMS.CB_Details(Voucher_No, SNo, CBLedgerId, Ledger_Id, Subledger_Id, Agent_Id, Cls1, Cls2, Cls3, Cls4, CurrencyId, CurrencyRate, Debit, Credit, LocalDebit, LocalCredit, Tbl_Amount, V_Amount, Narration, Party_No, Invoice_Date, Invoice_Miti, VatLedger_Id, PanNo, Vat_Reg, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
SELECT Voucher_No, SNo, CBLedgerId, Ledger_Id, Subledger_Id, Agent_Id, Cls1, Cls2, Cls3, Cls4, CurrencyId, CurrencyRate, Debit, Credit, LocalDebit, LocalCredit, Tbl_Amount, V_Amount, Narration, Party_No, Invoice_Date, Invoice_Miti, VatLedger_Id, PanNo, Vat_Reg, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
FROM MR_BHABSA01.AMS.CB_Details WHERE Voucher_No NOT IN (SELECT Voucher_No COLLATE DATABASE_DEFAULT FROM AMS.CB_Details)
