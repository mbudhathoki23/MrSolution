DELETE AMS.AccountDetails
DELETE AMS.StockBatchDetails
DELETE AMS.StockDetails

DELETE AMS.SystemSetting
DELETE AMS.SalesSetting
DELETE AMS.PurchaseSetting
DELETE AMS.FinanceSetting
DELETE AMS.InventorySetting
DELETE AMS.PaymentSetting

DELETE AMS.PT_Term
DELETE AMS.ST_Term

DELETE AMS.ProductSubGroup
UPDATE AMS.ProductGroup SET PurchaseLedgerId = NULL,PurchaseReturnLedgerId =NULL,SalesLedgerId =NULL,SalesReturnLedgerId =NULL,OpeningStockLedgerId = NULL,ClosingStockLedgerId =NULL,StockInHandLedgerId= NULL;
DELETE AMS.ProductGroup;

UPDATE AMS.Product SET PPL = NULL,PPR =NULL,PSL =NULL,PSR =NULL,PL_Closing = NULL,PL_Opening =NULL,BS_Closing = NULL;
DELETE AMS.Product;
DELETE AMS.ProductUnit

UPDATE AMS.GeneralLedger SET AgentId = NULL;
UPDATE AMS.JuniorAgent SET GLCode = NULL;
DELETE AMS.JuniorAgent 

UPDATE AMS.SeniorAgent SET GLID = NULL;
DELETE AMS.SeniorAgent

DELETE AMS.Area
DELETE AMS.MainArea

DELETE AMS.MemberShipSetup
DELETE AMS.MemberType

DELETE AMS.Counter
DELETE AMS.Floor WHERE FloorId NOT IN (SELECT tm.FloorId FROM AMS.TableMaster tm)
DELETE AMS.TableMaster WHERE TableId NOT IN (SELECT sm.TableId FROM AMS.SB_Master sm)

DELETE AMS.CostCenter WHERE CCId NOT IN (SELECT sd.CostCenter_Id FROM AMS.StockDetails sd)
DELETE AMS.Godown WHERE GID NOT IN (SELECT sd.Godown_Id FROM AMS.StockDetails sd)
DELETE AMS.RACK WHERE RID NOT IN (SELECT * FROM AMS.R

DELETE AMS.Subledger
DELETE AMS.GeneralLedger
DELETE AMS.AccountSubGroup
DELETE AMS.AccountGroup

DELETE AMS.LedgerOpening
DELETE AMS.ProductOpening

DELETE FROM AMS.SR_Term
DELETE FROM AMS.SR_Details
DELETE FROM AMS.SR_Master_OtherDetails
DELETE FROM AMS.SR_Master

DELETE FROM AMS.SB_Term
DELETE FROM AMS.SB_Details
DELETE FROM AMS.SB_ExchangeDetails
DELETE FROM AMS.SB_Master_OtherDetails
DELETE FROM AMS.SB_Master

DELETE FROM AMS.SC_Term
DELETE FROM AMS.SC_Details
DELETE FROM AMS.SC_Master_OtherDetails
DELETE FROM AMS.SC_Master

DELETE FROM AMS.SO_Term
DELETE FROM AMS.SO_Details
DELETE FROM AMS.SO_Master_OtherDetails
DELETE FROM AMS.SO_Master

DELETE FROM AMS.SQ_Term
DELETE FROM AMS.SQ_Details
DELETE FROM AMS.SQ_Master

DELETE FROM AMS.PAB_Details
DELETE FROM AMS.PAB_Master

DELETE FROM AMS.PR_Term
DELETE FROM AMS.PR_Details
DELETE FROM AMS.PR_Master

DELETE FROM AMS.PB_Term
DELETE FROM AMS.PB_Details
DELETE FROM AMS.PB_OtherMaster
DELETE FROM AMS.PB_Master

DELETE FROM AMS.PCR_Term
DELETE FROM AMS.PCR_Details
DELETE FROM AMS.PCR_Master

DELETE FROM AMS.PC_Term
DELETE FROM AMS.PC_Details
DELETE FROM AMS.PC_Master

DELETE FROM AMS.PO_Term
DELETE FROM AMS.PO_Details
DELETE FROM AMS.PO_Master

DELETE FROM AMS.PIN_Details
DELETE FROM AMS.PIN_Master

DELETE FROM AMS.PostDateCheque

DELETE FROM AMS.CB_Details
DELETE FROM AMS.CB_Master

DELETE FROM AMS.JV_Details
DELETE FROM AMS.JV_Master

DELETE AMS.Notes_Details WHERE Voucher_No IN (SELECT nm.Voucher_No FROM AMS.Notes_Master nm WHERE nm.VoucherMode='CN')
DELETE AMS.Notes_Master WHERE Voucher_No IN (SELECT nm.Voucher_No FROM AMS.Notes_Master nm WHERE nm.VoucherMode='CN')

DELETE AMS.Notes_Details WHERE Voucher_No IN (SELECT nm.Voucher_No FROM AMS.Notes_Master nm WHERE nm.VoucherMode='DN')
DELETE AMS.Notes_Master WHERE Voucher_No IN (SELECT nm.Voucher_No FROM AMS.Notes_Master nm WHERE nm.VoucherMode='DN')


