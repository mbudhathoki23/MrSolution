--SELECT * FROM Sheet1$ s


INSERT INTO AMS.ProductGroup (PGrpId,NepaliDesc,GrpName,GrpCode,GMargin,Gprinter,PurchaseLedgerId,PurchaseReturnLedgerId,SalesLedgerId,SalesReturnLedgerId,OpeningStockLedgerId,ClosingStockLedgerId,StockInHandLedgerId,Branch_ID,Company_Id,Status,EnterBy,EnterDate,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion)
SELECT ROW_NUMBER() OVER (ORDER BY s.[Group]) GroupId ,s.[Group],s.[Group],ROW_NUMBER() OVER (ORDER BY s.[Group]) GrpCode,0 GMargin,NULL Gprinter,NULL PurchaseLedgerId,NULL PurchaseReturnLedgerId,NULL SalesLedgerId,NULL SalesReturnLedgerId,NULL OpeningStockLedgerId,NULL ClosingStockLedgerId,NULL StockInHandLedgerId,1 Branch_ID,NULL Company_Id,1 Status,'MrSolution' EnterBy,GETDATE() EnterDate,NULL SyncBaseId,NULL SyncGlobalId,NULL SyncOriginId,NULL SyncCreatedOn,NULL SyncLastPatchedOn,1 SyncRowVersion FROM Sheet1$ s
WHERE s.[Group] IS NOT NULL AND s.[Group] NOT IN (SELECT pg.GrpName FROM AMS.ProductGroup pg)
GROUP BY s.[Group]


INSERT INTO AMS.ProductUnit (UID,NepaliDesc,UnitName,UnitCode,Branch_ID,Company_Id,EnterBy,EnterDate,Status,IsDefault,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion)
SELECT ROW_NUMBER() OVER (ORDER BY s.Unit) UID,UPPER(s.Unit) NepaliDesc,UPPER(s.Unit) UnitName,UPPER(s.Unit) UnitCode,1 Branch_ID,NULL Company_Id,'MrSolution' EnterBy,GETDATE() EnterDate,1 Status,0 IsDefault,NULL SyncBaseId,NULL SyncGlobalId,NULL SyncOriginId,NULL SyncCreatedOn,NULL SyncLastPatchedOn,1 SyncRowVersion FROM Sheet1$ s
WHERE s.Unit IS NOT NULL AND s.Unit NOT IN (SELECT pg.UnitCode FROM AMS.ProductUnit pg)
GROUP BY s.Unit

INSERT INTO AMS.Product (PID,NepaliDesc,PName,PAlias,PShortName,PType,PCategory,PUnit,PAltUnit,PQtyConv,PAltConv,PValTech,PSerialno,PSizewise,PBatchwise,PVehicleWise,PublicationWise,PBuyRate,AltSalesRate,PSalesRate,PMargin1,TradeRate,PMargin2,PMRP,PGrpId,PSubGrpId,PTax,PMin,PMax,CmpId,CmpId1,CmpId2,CmpId3,Branch_Id,CmpUnit_Id,PPL,PPR,PSL,PSR,PL_Opening,PL_Closing,BS_Closing,PImage,EnterBy,EnterDate,IsDefault,Status,ChasisNo,EngineNo,VHModel,VHColor,VHNumber,BeforeBuyRate,BeforeSalesRate,Barcode,Barcode1,Barcode2,Barcode3,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion)
SELECT ROW_NUMBER() OVER (ORDER BY s.Unit)  PID,AMS.ProperCase(s.[Product Description]) NepaliDesc,AMS.ProperCase(s.[Product Description]) PName,AMS.ProperCase(s.[Product Description]) PAlias,s.[Product ShortName] PShortName,'I' PType,'FG' PCategory,ISNULL(pu.UID,6) PUnit,NULL PAltUnit,0 PQtyConv,0 PAltConv,'FIFO' PValTech,0 PSerialno,0 PSizewise,0 PBatchwise,0 PVehicleWise,0 PublicationWise,s.[Buy Rate] PBuyRate,0 AltSalesRate,s.[Sales Rate] PSalesRate,0 PMargin1,s.[Sales Rate] TradeRate,0 PMargin2,s.[Sales Rate] PMRP,PGrpId,NULL PSubGrpId,CASE WHEN s.IsTaxable =1 THEN 13 ELSE 0 END  PTax,0 PMin,0 PMax,NULL CmpId,NULL CmpId1,NULL CmpId2,NULL CmpId3,1 Branch_Id,NULL CmpUnit_Id,NULL PPL,NULL PPR,NULL PSL,NULL PSR,NULL PL_Opening,NULL PL_Closing,NULL BS_Closing,NULL PImage,'MrSolution' EnterBy,GETDATE() EnterDate,0 IsDefault,1 Status,NULL ChasisNo,NULL EngineNo,NULL VHModel,NULL VHColor,NULL VHNumber,0 BeforeBuyRate,0 BeforeSalesRate,s.Barcode Barcode,NULL Barcode1,NULL Barcode2,NULL Barcode3,NULL SyncBaseId,NULL SyncGlobalId,NULL SyncOriginId,NULL SyncCreatedOn,NULL SyncLastPatchedOn,1 SyncRowVersion FROM Sheet1$ s
LEFT OUTER JOIN AMS.ProductGroup pg ON S.[Group] = PG.GrpName
LEFT OUTER JOIN AMS.ProductUnit pu ON s.Unit = pu.UnitCode

