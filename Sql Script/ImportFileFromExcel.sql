
INSERT INTO AMS.GeneralLedger(GLID, NepaliDesc, GLName, GLCode, ACCode, GLType, GrpId, PrimaryGroupId, SubGrpId, PrimarySubGroupId, PanNo, AreaId, AgentId, CurrId, CrDays, CrLimit, CrTYpe, IntRate, GLAddress, PhoneNo, LandLineNo, OwnerName, OwnerNumber, Scheme, Email, Branch_ID, Company_Id, EnterBy, EnterDate, Status, IsDefault, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
SELECT 80+ ROW_NUMBER() OVER (ORDER BY Description) GLID,Description NepaliDesc,Description GLName,SUBSTRING(Description,1,3) + CAST(ROW_NUMBER() OVER (PARTITION BY Description ORDER BY Description) AS NVARCHAR(3))  GLCode, ROW_NUMBER() OVER (ORDER BY Description) ACCode,
CASE WHEN ag.GrpId = 13 THEN 'Vendor' ELSE 'Customer' end GLType,ag.GrpId GrpId,NULL PrimaryGroupId,NULL SubGrpId, NULL PrimarySubGroupId,NULL PanNo,NULL AreaId,NULL AgentId,1 CurrId,0 CrDays,0 CrLimit,'I' CrTYpe,0 IntRate,NULL GLAddress,NULL PhoneNo,NULL LandLineNo,NULL OwnerName,NULL OwnerNumber,NULL Scheme,NULL Email,1 Branch_ID,NULL Company_Id,'MrSolution' EnterBy,GETDATE() EnterDate, 1 Status,0 IsDefault,NULL SyncBaseId,NULL SyncGlobalId,NULL SyncOriginId,GETDATE() SyncCreatedOn,GETDATE() SyncLastPatchedOn,1 SyncRowVersion
FROM dbo.['LedgerInformation $'] li
LEFT OUTER JOIN AMS.AccountGroup ag ON ag.GrpName = li.[Group]



INSERT INTO AMS.Product(PID, NepaliDesc, PName, PAlias, PShortName, PType, PCategory, PUnit, PAltUnit, PQtyConv, PAltConv, PValTech, PSerialno, PSizewise, PBatchwise, PBuyRate, AltSalesRate, PSalesRate, PMargin1, TradeRate, PMargin2, PMRP, PGrpId, PSubGrpId, PTax, PMin, PMax, CmpId, CmpId1, CmpId2, CmpId3, Branch_Id, CmpUnit_Id, PPL, PPR, PSL, PSR, PL_Opening, PL_Closing, BS_Closing, PImage, EnterBy, EnterDate, IsDefault, Status, ChasisNo, EngineNo, VHModel, VHColor, VHNumber, BeforeBuyRate, BeforeSalesRate, Barcode, Barcode1, Barcode2, Barcode3, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
SELECT ROW_NUMBER() OVER (ORDER BY Description) PID,Description NepaliDesc, Description PName,Description PAlias,SUBSTRING(Description,1,3) + CAST(ROW_NUMBER() OVER (PARTITION BY Description ORDER BY Description) AS NVARCHAR(3)) PShortName,'I' PType,'FG' PCategory,1 PUnit,NULL PAltUnit,
0 PQtyConv,0 PAltConv,'FIFO' PValTech,0 PSerialno,0 PSizewise,0 PBatchwise,CAST(ISNULL(BuyRate,0) AS DECIMAL) PBuyRate,0 AltSalesRate,CAST(ISNULL(SalesRate,0) AS DECIMAL ) PSalesRate,0 PMargin1,0 TradeRate,0 PMargin2,0 PMRP,NULL PGrpId,NULL PSubGrpId,13 PTax,0 PMin,0 PMax,NULL CmpId,NULL CmpId1,NULL CmpId2,NULL CmpId3,1 Branch_Id,NULL CmpUnit_Id,
NULL PPL,NULL PPR,NULL PSL,NULL PSR,NULL PL_Opening,NULL PL_Closing,NULL BS_Closing,NULL PImage,'MrSolution' EnterBy,GETDATE() EnterDate,0 IsDefault,1 Status,NULL ChasisNo,NULL EngineNo,NULL VHModel,NULL VHColor,NULL VHNumber,0 BeforeBuyRate, 0 BeforeSalesRate,NULL Barcode,NULL Barcode1,NULL Barcode2,NULL Barcode3,NULL SyncBaseId,NULL SyncGlobalId,NULL SyncOriginId,GETDATE() SyncCreatedOn,GETDATE() SyncLastPatchedOn,1 SyncRowVersion 
FROM dbo.[StockSummary$]


INSERT INTO AMS.ProductOpening(OpeningId, Voucher_No, Serial_No, OP_Date, OP_Miti, Product_Id, Godown_Id, Cls1, Cls2, Cls3, Cls4, Currency_Id, Currency_Rate, AltQty, AltUnit, Qty, QtyUnit, Rate, LocalRate, Amount, LocalAmount, IsReverse, CancelRemarks, CancelBy, CancelDate, Remarks, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, CBranch_Id, CUnit_Id, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
SELECT 1 OpeningId,'OB/0001/78-79' Voucher_No,ROW_NUMBER() OVER (ORDER BY Description) Serial_No,'2022-07-15' OP_Date,'31/03/2078' OP_Miti,p.PID Product_Id,NULL Godown_Id,NULL Cls1,NULL Cls2,NULL Cls3,NULL Cls4,1 Currency_Id,1 Currency_Rate,0 AltQty,NULL AltUnit,ISNULL(ss.Quantity,0) Qty,1 QtyUnit,ISNULL(ss.BuyRate,0) Rate,ISNULL(ss.BuyRate,0) LocalRate,ISNULL(ss.Balance,0) Amount,ISNULL(ss.Balance,0) LocalAmount,
0 IsReverse,NULL CancelRemarks,NULL CancelBy,NULL CancelDate,NULL Remarks,'MrSolution' Enter_By,GETDATE() Enter_Date,NULL Reconcile_By,NULL Reconcile_Date,1 CBranch_Id,NULL CUnit_Id,12 FiscalYearId,NULL SyncBaseId,NULL SyncGlobalId,NULL SyncOriginId,GETDATE() SyncCreatedOn,GETDATE() SyncLastPatchedOn,1 SyncRowVersion 
FROM dbo.[StockSummary$] ss
LEFT OUTER JOIN AMS.Product p ON p.PName = ss.Description
WHERE ISNULL(ss.Quantity,0) > 0
