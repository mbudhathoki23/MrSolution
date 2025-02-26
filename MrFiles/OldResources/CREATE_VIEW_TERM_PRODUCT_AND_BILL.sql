

---------------SB_TERM(PRODUCT AND BILL)--------------

DECLARE @terms_pro AS NVARCHAR(MAX), @query_pro AS NVARCHAR(MAX);
SET @terms_pro =
(
  SELECT SUBSTRING (
         (
           SELECT ', [' + ST_Name + ']'
            FROM AMS.ST_Term
            WHERE ST_Condition IN ('P') AND ST_Type = 'G'
            ORDER BY CONVERT ( INT, Order_No )
           FOR XML PATH ( '' )
         ),        3, 200000
                   ) AS Terms
);
SELECT @terms_pro;
DECLARE @terms_bill AS NVARCHAR(MAX), @query_bill AS NVARCHAR(MAX);
SET @terms_bill =
(
  SELECT SUBSTRING (
         (
           SELECT ', [' + ST_Name + ']'
            FROM AMS.ST_Term
            WHERE ST_Condition IN ('B') AND ST_Type = 'G'
            ORDER BY CONVERT ( INT, Order_No )
           FOR XML PATH ( '' )
         ),        3, 200000
                   ) AS Terms
);
SELECT @terms_bill;
SELECT @query_bill = 'Alter View VM.VIEW_SB_TERM_BILL as Select * from ( 
						SELECT STD.SB_VNo VOUCHER_NO, STD.SNo VOUCHER_SNO,CASE WHEN ST_Sign = ''+''  then Amount else -Amount end as Amount,SBT.ST_Name
						FROM AMS.SB_Term as STD inner join AMS.ST_Term as SBT on STD.ST_Id = SBT.ST_Id 
						Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice=STD.SB_VNo WHERE Term_Type in(''B'') ) as d 
						Pivot(max(Amount) for ST_Name in (' + @terms_bill + ') ) as pid';
SELECT @query_bill;
EXEC (@query_bill);
SELECT @query_pro = 'Alter View VM.VIEW_SB_TERM_PRODUCT as 
						Select * from ( 
						SELECT STD.SB_VNo VOUCHER_NO, STD.SNo VOUCHER_SNO, STD.Product_Id PRODUCT_ID,CASE WHEN ST_Sign = ''+'' then Amount else -Amount end as Amount,SBT.ST_Name 
						FROM AMS.SB_Term as STD 
						inner join AMS.ST_Term as SBT on STD.ST_Id = SBT.ST_Id Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice=STD.SB_VNo WHERE Term_Type in(''P'') 
						) as d Pivot(max(Amount) FOR ST_Name in (' + @terms_pro + ') ) as pid ';
SELECT @query_pro;
EXEC (@query_pro);
SELECT @query_bill = 'Alter View VM.VIEW_SR_TERM_BILL as Select * from (
                        SELECT STD.SR_VNo VOUCHER_NO, STD.SNo VOUCHER_SNO,CASE WHEN ST_Sign = ''+'' then Amount 
                        else -Amount end as Amount,SBT.ST_Name 
                        FROM AMS.SR_Term as STD inner join AMS.ST_Term as SBT on STD.ST_Id = SBT.ST_Id Inner Join AMS.SR_Master as SIM On SIM.SR_Invoice=STD.SR_VNo
                        WHERE Term_Type in(''B'') ) as d Pivot(max(Amount) for ST_Name in (' + @terms_bill + ') ) as pid';
SELECT @query_bill;
EXEC (@query_bill);
SELECT @query_pro = ' Alter View VM.VIEW_SR_TERM_PRODUCT as Select * from ( 
                        SELECT STD.SR_VNo VOUCHER_NO, STD.SNo VOUCHER_SNO, STD.Product_Id PRODUCT_ID,CASE WHEN ST_Sign = ''+'' then Amount 
						else -Amount end as Amount,SBT.ST_Name 
                        FROM AMS.SR_Term as STD inner join AMS.ST_Term as SBT on STD.ST_Id = SBT.ST_Id Inner Join AMS.SR_Master as SIM On SIM.SR_Invoice=STD.SR_VNo 
						WHERE Term_Type in(''P'')) as d Pivot(max(Amount) for ST_Name in (' + @terms_pro + ')  ) as pid; ';
SELECT @query_pro;
EXEC (@query_pro);
SELECT @query_bill = 'Alter View VM.VIEW_SO_TERM_BILL as Select * from ( 
                        SELECT STD.SO_Vno VOUVHER_NO, STD.SNo VOUCHER_SNO,CASE WHEN ST_Sign = ''+'' then Amount 
                        else -Amount end as Amount,SBT.ST_Name 
                        FROM AMS.SO_Term as STD inner join AMS.ST_Term as SBT on STD.ST_Id = SBT.ST_Id Inner Join AMS.SO_Master as SIM On SIM.SO_Invoice=STD.SO_Vno
                        WHERE Term_Type in(''B'') ) as d Pivot(max(Amount) for ST_Name in (' + @terms_bill + ') ) as pid ';
SELECT @query_bill;
EXEC (@query_bill);
SELECT @query_pro = 'Alter View VM.VIEW_SO_TERM_PRODUCT as Select * from 
						(SELECT STD.SO_Vno VOUCHER_NO, STD.SNo VOUCHER_SNO, STD.Product_Id PRODUCT_ID, CASE WHEN ST_Sign = ''+'' then Amount 
                        else -Amount end as Amount,SBT.ST_Name 
                        FROM AMS.SO_Term as STD inner join AMS.ST_Term as SBT on STD.ST_Id = SBT.ST_Id Inner Join AMS.SO_Master as SIM On SIM.SO_Invoice=STD.SO_Vno
                        WHERE Term_Type in(''P'') ) as d Pivot(max(Amount) for ST_Name in (' + @terms_pro + ') ) as pid';
SELECT @query_pro;
EXEC (@query_pro);
SELECT @query_bill = 'Alter View VM.VIEW_SC_TERM_BILL as 
                            Select * from 
                            ( 
                            SELECT STD.SC_Vno VOUCHER_NO, STD.SNo VOUCHER_SNO,CASE WHEN ST_Sign = ''+'' then Amount else -Amount end as Amount,SBT.ST_Name 
                            FROM AMS.SC_Term as STD 
                            inner join AMS.ST_Term as SBT on STD.ST_Id = SBT.ST_Id Inner Join AMS.SC_Master as SIM On SIM.SC_Invoice = STD.SC_Vno WHERE Term_Type in(''B'') 
                            ) as d Pivot(max(Amount) FOR ST_Name in (' + @terms_bill + ') ) as pid';
SELECT @query_bill;
EXEC (@query_bill);
SELECT @query_pro = ' Alter View VM.VIEW_SC_TERM_PRODUCT as 
                        Select * from 
                        ( 
                          SELECT STD.SC_Vno VOUCHER_NO, STD.SNo VOUCHER_SNO, STD.Product_Id PRODUCT_ID,CASE WHEN ST_Sign = ''+'' then Amount else -Amount end as Amount,SBT.ST_Name 
                          FROM AMS.SC_Term as STD 
                          inner join AMS.ST_Term as SBT on STD.ST_Id = SBT.ST_Id Inner Join AMS.SC_Master as SIM On SIM.SC_Invoice = STD.SC_Vno WHERE Term_Type in(''P'') 
                        ) as d Pivot(max(Amount) FOR ST_Name in (' + @terms_pro + ') ) as pid ';
SELECT @query_pro;
EXEC (@query_pro);
SELECT @query_bill = 'Alter View VM.VIEW_SQ_TERM_BILL as 
                            Select * from 
                            ( 
                            SELECT STD.SQ_Vno VOUCHER_NO, STD.SNo VOUCHER_SNO,CASE WHEN ST_Sign = ''+'' then Amount else -Amount end as Amount,SBT.ST_Name 
                            FROM AMS.SQ_Term as STD inner join AMS.ST_Term as SBT on STD.ST_Id = SBT.ST_Id Inner Join AMS.SQ_Master as SIM On SIM.SQ_Invoice=STD.SQ_Vno WHERE Term_Type in(''B'') 
                            ) as d Pivot(max(Amount) for ST_Name in (' + @terms_bill + ') ) as pid';
SELECT @query_bill;
EXEC (@query_bill);
SELECT @query_pro = 'Alter View VM.VIEW_SQ_TERM_PRODUCT as 
                        Select * from 
                        ( 
                        SELECT STD.SQ_Vno VOUCHER_NO, STD.SNo VOUCHER_SNO, STD.Product_Id PRODUCT_ID,CASE WHEN ST_Sign = ''+'' then Amount else -Amount end as Amount,SBT.ST_Name 
                        FROM AMS.SQ_Term as STD inner join AMS.ST_Term as SBT on STD.ST_Id = SBT.ST_Id Inner Join AMS.SQ_Master as SIM On SIM.SQ_Invoice=STD.SQ_Vno WHERE Term_Type in(''p'') 
                        ) as d Pivot(max(Amount) for ST_Name in (' + @terms_pro + ') ) as pid';
SELECT @query_pro;
EXEC (@query_pro);

---------------PB_TERM(PRODUCT AND BILL)--------------
DECLARE @P_terms_pro AS NVARCHAR(MAX), @P_query_pro AS NVARCHAR(MAX);
SET @P_terms_pro =
(
  SELECT SUBSTRING (
         (
           SELECT ', [' + PT_Name + ']'
            FROM AMS.PT_Term
            WHERE PT_Condition IN ('P') AND PT_Type = 'G'
            ORDER BY CONVERT ( INT, Order_No )
           FOR XML PATH ( '' )
         ),        3, 200000
                   ) AS Terms
);
SELECT @P_terms_pro;
DECLARE @P_terms_bill AS NVARCHAR(MAX), @P_query_bill AS NVARCHAR(MAX);
SET @P_terms_bill =
(
  SELECT SUBSTRING (
         (
           SELECT ', [' + PT_Name + ']'
            FROM AMS.PT_Term
            WHERE PT_Condition IN ('B') AND PT_Type = 'G'
            ORDER BY CONVERT ( INT, Order_No )
           FOR XML PATH ( '' )
         ),        3, 200000
                   ) AS Terms
);
SELECT @P_terms_bill;
SELECT @P_query_bill = 'Alter View VM.VIEW_PB_TERM_BILL as 
                            Select * from 
                            ( 
                                SELECT PTD.PB_Vno VOUCHER_NO, PTD.SNo VOUCHER_SNO,CASE WHEN ST_Sign = ''+'' then Amount else -Amount end as Amount,SBT.ST_Name 
                                FROM AMS.PB_Term as PTD 
                                inner join AMS.ST_Term as SBT on PTD.PT_Id = SBT.ST_Id Inner Join AMS.PB_Master as SIM On SIM.PB_Invoice=PTD.PB_Vno WHERE Term_Type in(''B'') 
                            ) as d Pivot(max(Amount) FOR ST_Name in (' + @P_terms_bill + ') ) as pid';
SELECT @P_query_bill;
EXEC (@P_query_bill);
SELECT @query_pro = 'Alter View VM.VIEW_PB_TERM_PRODUCT as 
					    Select * from 
					    ( 
					    SELECT PTD.PB_Vno VOUCHER_NO, PTD.SNo VOUCHER_SNO, PTD.Product_Id PRODUCT_ID,CASE WHEN ST_Sign = ''+'' then Amount else -Amount end as Amount,SBT.ST_Name 
					    FROM AMS.PB_Term as PTD 
					    inner join AMS.ST_Term as SBT on PTD.PT_Id = SBT.ST_Id Inner Join AMS.PB_Master as SIM On SIM.PB_Invoice=PTD.PB_Vno WHERE Term_Type in(''P'') 
					    ) as d Pivot(max(Amount) FOR ST_Name in (' + @P_terms_pro + ') ) as pid';
SELECT @query_pro;
EXEC (@query_pro);
SELECT @query_bill = 'Alter View VM.VIEW_PR_TERM_BILL as 
                         Select * from 
                         ( 
                             SELECT PTD.PR_Vno VOUCHER_NO, PTD.SNo VOUCHER_SNO,CASE WHEN ST_Sign = ''+'' then Amount else -Amount end as Amount,SBT.ST_Name 
                             FROM AMS.PR_Term as PTD 
                             inner join AMS.ST_Term as SBT on PTD.PT_Id = SBT.ST_Id Inner Join AMS.PR_Master as SIM On SIM.PR_Invoice=PTD.PR_Vno WHERE Term_Type in(''B'') 
                         ) as d Pivot(max(Amount) FOR ST_Name in (' + @P_terms_bill + ') ) as pid';
SELECT @query_bill;
EXEC (@query_bill);
SELECT @P_query_pro = 'Alter View VM.VIEW_PR_TERM_PRODUCT as 
                            Select * from 
                            ( 
                            SELECT PTD.PR_Vno VOUCHER_NO, PTD.SNo VOUCHER_SNO, PTD.Product_Id PRODUCT_ID,CASE WHEN ST_Sign = ''+'' then Amount else -Amount end as Amount,SBT.ST_Name 
                            FROM AMS.PR_Term as PTD 
                            inner join AMS.ST_Term as SBT on PTD.PT_Id = SBT.ST_Id Inner Join AMS.PR_Master as SIM On SIM.PR_Invoice=PTD.PR_Vno WHERE Term_Type in(''P'') 
                            ) as d Pivot(max(Amount) FOR ST_Name in (' + @P_terms_pro + ') ) as pid ';
SELECT @P_query_pro;
EXEC (@P_query_pro);
SELECT @P_query_bill = 'Alter View VM.VIEW_PO_TERM_BILL as 
                            Select * from 
                            ( 
                                SELECT PTD.PR_Vno VOUCHER_NO, PTD.SNo VOUCHER_SNO,CASE WHEN ST_Sign = ''+'' then Amount else -Amount end as Amount,SBT.ST_Name 
                                FROM AMS.PR_Term as PTD 
                                inner join AMS.ST_Term as SBT on PTD.PT_Id = SBT.ST_Id Inner Join AMS.PR_Master as SIM On SIM.PR_Invoice=PTD.PR_Vno WHERE Term_Type in(''B'') 
                            ) as d Pivot(max(Amount) FOR ST_Name in (' + @P_terms_bill + ') ) as pid';
SELECT @P_query_bill;
EXEC (@P_query_bill);
SELECT @P_query_pro = 'Alter View VM.VIEW_PO_TERM_PRODUCT as 
                            Select * from 
	                        (
	                        SELECT PTD.PO_VNo VOUCHER_NO, PTD.SNo VOUCHER_SNO, PTD.Product_Id PRODUCT_ID,CASE WHEN ST_Sign = ''+'' then Amount else -Amount end as Amount,SBT.ST_Name 
                            FROM AMS.PO_Term as PTD 
                            inner join AMS.ST_Term as SBT on PTD.PT_Id = SBT.ST_Id Inner Join AMS.PO_Master as SIM On SIM.PO_Invoice=PTD.PO_VNo WHERE Term_Type in(''P'') 
                            ) as d Pivot(max(Amount) FOR ST_Name in (' + @P_terms_pro + ' ) ) as pid';
SELECT @P_query_pro;
EXEC (@P_query_pro);
SELECT @P_query_bill = 'Alter View VM.VIEW_PC_TERM_BILL as 
                            Select * from 
	                        (
	                        SELECT PTD.PC_VNo VOUCHER_NO, PTD.SNo VOUCHER_SNO, CASE WHEN ST_Sign = ''+'' then Amount else -Amount end as Amount,SBT.ST_Name 
                            FROM AMS.PC_Term as PTD 
                            inner join AMS.ST_Term as SBT on PTD.PT_Id = SBT.ST_Id Inner Join AMS.PC_Master as SIM On SIM.PC_Invoice=PTD.PC_VNo WHERE Term_Type in(''B'') 
                            ) as d Pivot(max(Amount) FOR ST_Name in (' + @P_terms_bill + ') ) as pid ';
SELECT @P_query_bill;
EXEC (@P_query_bill);
SELECT @P_query_pro = 'Alter View VM.VIEW_PC_TERM_PRODUCT as 
                            Select * from 
	                        (
	                        SELECT PTD.PC_VNo VOUCHER_NO, PTD.SNo VOUCHER_SNO, PTD.Product_Id PRODUCT_ID,CASE WHEN ST_Sign = ''+'' then Amount else -Amount end as Amount,SBT.ST_Name 
                            FROM AMS.PC_Term as PTD 
                            inner join AMS.ST_Term as SBT on PTD.PT_Id = SBT.ST_Id Inner Join AMS.PC_Master as SIM On SIM.PC_Invoice=PTD.PC_VNo WHERE Term_Type in(''P'') 
                            ) as d Pivot(max(Amount) FOR ST_Name in (' + @P_terms_pro + ') ) as pid ';
SELECT @P_query_pro;
EXEC (@P_query_pro);