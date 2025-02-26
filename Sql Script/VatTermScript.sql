DECLARE @orderNo INT=(SELECT Order_No FROM AMS.ST_Term WHERE ST_ID='7');
SELECT t.SB_VNo, t.Product_Id, t.SNo, SUM(ISNULL(t.TermAmount, 0)) TermAmount, SUM(ISNULL(t.VatAmount, 0)) VatAmount
FROM(SELECT term.SB_VNo, term.Product_Id, term.Order_No, term.SNo, SUM(CASE WHEN term.Order_No<@orderNo THEN term.Amount ELSE 0 END) TermAmount, SUM(CASE WHEN term.Order_No=@orderNo THEN term.Amount ELSE 0 END) VatAmount
     FROM(SELECT sbt.SB_VNo, sbt.Product_Id, st.Order_No, sbt.SNo, CASE WHEN st.ST_Sign='+' THEN Amount ELSE -Amount END Amount
          FROM AMS.SB_Term sbt
               LEFT OUTER JOIN AMS.ST_Term st ON st.ST_ID=sbt.ST_Id) term
     WHERE term.SB_VNo='HSTSB/00253/79-80'
     GROUP BY term.SB_VNo, term.Product_Id, term.Order_No, term.SNo) t
GROUP BY t.SB_VNo, t.Product_Id, t.SNo;

--SELECT * FROM AMS.ST_Term;

SELECT VoucherMode, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ref_VNo, Ref_VDate, Currency_Id, Currency_Rate, Cls1, Cls2, Cls3, Cls4, N_Amount, Remarks, Action_Type, EnterBy, EnterDate, Audit_Lock, IsReverse, CancelBy, CancelDate, CancelReason, ReconcileBy, ReconcileDate, ClearingBy, ClearingDate, PrintValue, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, CancelRemarks
FROM AMS.JV_Master;


