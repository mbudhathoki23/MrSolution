ALTER PROCEDURE [AMS].[Usp_IUD_Menu_Rights]
(@EVENT CHAR(2), @Id INT OUTPUT, @Role_Id INT, @XMLDETAILS VARCHAR(MAX), @MSG VARCHAR(200) = 'Error' OUTPUT, @Return_Id INT OUTPUT)
AS BEGIN TRY
DECLARE @xml XML;
SET @xml = CONVERT ( XML, @XMLDETAILS );
SET XACT_ABORT ON;
BEGIN TRANSACTION;
CREATE TABLE #TEMPMENU
(
  Menu_Id INT
);
INSERT INTO #TEMPMENU
 SELECT ParamValues.ID.query ( 'Menu_Id' ).value ( '.', 'INT' ) AS SNo
  FROM @xml.nodes('/DocumentElement/Temp') AS ParamValues(ID);
IF @EVENT = 'I' BEGIN

-------------Insert--------------------------------
DELETE FROM AMS.Menu_Rights
 WHERE Role_Id = @Role_Id AND Menu_Id IN (
                                           SELECT Menu_Id
                                            FROM #TEMPMENU
                                         );
INSERT INTO AMS.Menu_Rights
 SELECT Role_Id = @Role_Id, ParamValues.ID.query ( 'Menu_Id' ).value ( '.', 'INT' ) AS Menu_Id, ParamValues.ID.query ( 'SubModule_Id' ).value ( '.', 'INT' ) AS SubModule_Id, ParamValues.ID.query ( 'New' ).value ( '.', 'bit' ) AS IsNew, ParamValues.ID.query ( 'Save' ).value ( '.', 'bit' ) AS IsSave, ParamValues.ID.query ( 'Update' ).value ( '.', 'bit' ) AS IsUpdate, ParamValues.ID.query ( 'Delete' ).value ( '.', 'bit' ) AS IsDelete, ParamValues.ID.query ( 'Copy' ).value ( '.', 'bit' ) AS Copy, ParamValues.ID.query ( 'Search' ).value ( '.', 'bit' ) AS IsSearch, ParamValues.ID.query ( 'Print' ).value ( '.', 'bit' ) AS IsPrint, ParamValues.ID.query ( 'Approved' ).value ( '.', 'bit' ) AS IsApproved, ParamValues.ID.query ( 'Reverse' ).value ( '.', 'bit' ) AS IsReverse, ParamValues.ID.query ( 'Isparent' ).value ( '.', 'bit' ) AS Isparent, ParamValues.ID.query ( 'Created_By' ).value ( '.', 'INT' ) AS Created_By, ParamValues.ID.query ( 'Created_Date' ).value ( '.', 'DATETIME' ) AS Created_Date, ParamValues.ID.query ( 'Branch_Id' ).value ( '.', 'INT' ) AS Branch_Id, ParamValues.ID.query ( 'FiscalYear_Id' ).value ( '.', 'INT' ) AS FiscalYear_Id
  FROM @xml.nodes('/DocumentElement/Temp') AS ParamValues(ID);
SET @MSG = 'Record Inserted Successfully !';
SET @Return_Id = @@IDENTITY;
END;
---------------End Insert---------------------------
---------------Update-------------------------------
ELSE IF @EVENT = 'U' BEGIN
DELETE FROM AMS.Menu_Rights
 WHERE Role_Id = @Role_Id AND Menu_Id IN (
                                           SELECT Menu_Id
                                            FROM #TEMPMENU
                                         );
INSERT INTO AMS.Menu_Rights
 SELECT Role_Id = @Role_Id, ParamValues.ID.query ( 'Menu_Id' ).value ( '.', 'INT' ) AS Menu_Id, ParamValues.ID.query ( 'SubModule_Id' ).value ( '.', 'INT' ) AS SubModule_Id, ParamValues.ID.query ( 'New' ).value ( '.', 'bit' ) AS IsNew, ParamValues.ID.query ( 'Save' ).value ( '.', 'bit' ) AS IsSave, ParamValues.ID.query ( 'Update' ).value ( '.', 'bit' ) AS IsUpdate, ParamValues.ID.query ( 'Delete' ).value ( '.', 'bit' ) AS IsDelete, ParamValues.ID.query ( 'Copy' ).value ( '.', 'bit' ) AS Copy, ParamValues.ID.query ( 'Search' ).value ( '.', 'bit' ) AS IsSearch, ParamValues.ID.query ( 'Print' ).value ( '.', 'bit' ) AS IsPrint, ParamValues.ID.query ( 'Approved' ).value ( '.', 'bit' ) AS IsApproved, ParamValues.ID.query ( 'Reverse' ).value ( '.', 'bit' ) AS IsReverse, ParamValues.ID.query ( 'Isparent' ).value ( '.', 'bit' ) AS Isparent, ParamValues.ID.query ( 'Created_By' ).value ( '.', 'INT' ) AS Created_By, ParamValues.ID.query ( 'Created_Date' ).value ( '.', 'DATETIME' ) AS Created_Date, ParamValues.ID.query ( 'Branch_Id' ).value ( '.', 'INT' ) AS Branch_Id, ParamValues.ID.query ( 'FiscalYear_Id' ).value ( '.', 'INT' ) AS FiscalYear_Id
  FROM @xml.nodes('/DocumentElement/Temp') AS ParamValues(ID);
SET @MSG = 'Record Updated Successfully !';
SET @Return_Id = @@IDENTITY;
END;
-----------------End Update-----------------------------
-----------------Delete---------------------------------
ELSE IF @EVENT = 'D' BEGIN
DELETE FROM AMS.Menu_Rights
 WHERE Role_Id = @Role_Id AND Menu_Id IN (
                                           SELECT Menu_Id
                                            FROM #TEMPMENU
                                         );
SET @MSG = 'Record Deleted Successfully !';
END;
-----------------End Delete-----------------------------
DELETE FROM AMS.Menu_Rights
 WHERE menu_id NOT IN (
                        SELECT SubModule_Id
                         FROM AMS.Menu_Rights
                         WHERE Role_Id = @Role_Id
                      ) AND isparent = 1 AND Role_Id = @Role_Id;
SET @Return_Id = 1;
DROP TABLE #TEMPMENU;
COMMIT TRANSACTION;
END TRY
BEGIN CATCH
ROLLBACK;
END CATCH;