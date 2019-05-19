WITH   ItemEndTotal AS(
SELECT  Product_Id,PName,SUM(CASE WHEN EntryType ='O' THEN-Qty ELSE Qty END) AS FinalCount FROM  AMS.StockDetails,AMS.Product WHERE PID=Product_Id
GROUP BY Product_Id,PName
),
ReverseRunningTotal	 AS (
SELECT PName,Product_Id ,EntryType ,Voucher_Date ,Qty ,Rate ,SUM(Qty) OVER (PARTITION BY Product_Id ORDER BY Voucher_Date ROWS BETWEEN CURRENT ROW AND UNBOUNDED FOLLOWING ) ReverseTotal
FROM    AMS.StockDetails,AMS.Product WHERE PID=Product_Id
AND   EntryType IN ( 'I','RET' )
),
FindDate AS (
SELECT DISTINCT T.Product_Id,FinalCount ,LAST_VALUE(Voucher_Date) OVER (PARTITION BY P.Product_Id ORDER BY Voucher_Date ROWS BETWEEN CURRENT ROW AND UNBOUNDED FOLLOWING )AS TheDate
FROM    ItemEndTotal AS T
JOIN ReverseRunningTotal AS P
ON T.Product_Id= P.Product_Id
AND P.ReverseTotal>= T.FinalCount
)

SELECT RRT.PName,RRT.Product_Id,FinalCount,CASE WHEN FINALCOUNT=0 THEN 0 ELSE SUM(CASE WHEN TheDate = Voucher_Date THEN FinalCount- ( ReverseTotal - Qty ) ELSE Qty END * Rate)/FinalCount END AS RATE,SUM(CASE WHEN TheDate = Voucher_Date THEN FinalCount- ( ReverseTotal - Qty ) ELSE Qty END * Rate) AS Value
FROM   ReverseRunningTotal RRT
JOIN FindDate ON RRT.Product_Id= FindDate.Product_Id 
CROSS APPLY (SELECT TOP( 1 ) Rate AS PurchaseRate FROM     ReverseRunningTotal AS R
WHERE    RRT.Product_Id= R.Product_Id
AND EntryType= 'I'
AND R.Voucher_Date<= RRT.Voucher_Date
ORDER BY Voucher_Date DESC
) AS P
WHERE  RRT.Voucher_Date>= TheDate
GROUP BY RRT.Product_Id ,RRT.PName,
FinalCount
ORDER BY RRT.PName,RRT.Product_Id;