ALTER PROCEDURE [AMS].[Usp_IUD_UserInfo]
(@Event CHAR(2) = 'I', @Id INT = 0, @Name VARCHAR(255) = NULL, @User_Name VARCHAR(50) = NULL, @Ori_Password NVARCHAR(555) = NULL, @Password NVARCHAR(555) = NULL, @Address VARCHAR(255) = NULL, @Mobile_No VARCHAR(50) = NULL, @Tel_PhoneNo NVARCHAR(50) = NULL, @Email_Id NVARCHAR(255) = NULL, @Role_Id INT = 0, @Br_Id INT = 0, @Allow_Posting BIT = 0, @Posting_StartDate DATETIME = NULL, @Posting_EndDate DATETIME = NULL, @Modify_StartDate DATETIME = NULL, @Modify_EndDate DATETIME = NULL, @Auditors_Lock BIT = 0, @Valid_Days INT = 0, @Created_By INT = 0, @Created_Date DATETIME = '01/01/1753', @DEFAULT_DATABASE VARCHAR(50) = NULL, @Status BIT = 1, @Msg VARCHAR(MAX) OUTPUT, @Return_Id INT OUTPUT)
AS
IF @Event = 'I' --for Insert
BEGIN
INSERT INTO AMS.UserInfo
VALUES (@Name, @User_Name, @Password, @Address, @Mobile_No, @Tel_PhoneNo, @Email_Id, @Role_Id, @Br_Id, @Allow_Posting, @Posting_StartDate, @Posting_EndDate, @Modify_StartDate, @Modify_EndDate, @Auditors_Lock, @Valid_Days, @Created_By, @Created_Date, @Status);
SET @Return_Id = @@IDENTITY;
SET @Msg = 'Record Inserted Successfully';
END;
ELSE IF @Event = 'U' --- Update
BEGIN
UPDATE AMS.UserInfo
 SET [Full_Name] = @Name,
--[User_Name] = @User_Name,
--[Password] = @Password,
[Address] = @Address, [Mobile_No] = @Mobile_No, [Phone_No] = @Tel_PhoneNo, [Email_Id] = @Email_Id, [Role_Id] = @Role_Id, [Branch_Id] = @Br_Id, [Allow_Posting] = @Allow_Posting, [Posting_StartDate] = @Posting_StartDate, [Posting_EndDate] = @Posting_EndDate, [Modify_StartDate] = @Modify_StartDate, [Modify_EndDate] = @Modify_EndDate, [Auditors_Lock] = @Auditors_Lock, [Valid_Days] = @Valid_Days, [Status] = @Status
 WHERE [User_Id] = @Id;
SET @Msg = 'Record Updated Successfully';
END;
ELSE IF @Event = 'D' -- For Delete
BEGIN
DELETE FROM AMS.UserInfo
 WHERE [User_Id] = @Id;
SET @Msg = 'Record Deleted Successfully';
END;
IF @Event <> 'D' BEGIN
EXEC ('CREATE LOGIN  [' + @User_Name + '] WITH PASSWORD=''' + @Ori_Password + ''' , DEFAULT_DATABASE=' + @DEFAULT_DATABASE + ', DEFAULT_LANGUAGE=[English] ');
EXEC ('ALTER SERVER ROLE [sysadmin] ADD MEMBER [' + @User_Name + ']');
END;