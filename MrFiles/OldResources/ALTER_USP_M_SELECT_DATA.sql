ALTER PROCEDURE [AMS].[Usp_MSelect_Data]
				(
				@Event int,
				@Code1 varchar(255),
				@Code2 varchar(255)='',
				@Date datetime
				)
				as
				IF @Event=1
				BEGIN
				CREATE TABLE #TEMP1(
				[Id] [int]  NULL,
				[Menu_Id] [int] NULL,
				Menu_Name VARCHAR(255),
				[SubModule_Id] [int] NULL,
				[New] [bit] NULL,
				[Save] [bit] NULL,
				[Update] [bit] NULL,
				[Copy] [bit] NULL,
				[Delete] [bit] NULL,
				[Search] [bit] NULL,
				[Print] [bit] NULL,
				[Approved] [bit] NULL,
				[Reverse] [bit] NULL,
				[Parent] char(1) NULL
				)
				INSERT INTO #TEMP1
				SELECT isnull(MR.MR_Id,0),M.Menu_Id,M.Menu_Name,@Code1 SubModule_Id,isnull(MR.New,0),isnull(MR.[Save],0),isnull(MR.[Update],0),isnull(MR.Copy,0),isnull(MR.[Delete],0),isnull(MR.[Search],0),isnull(MR.[Print],0),isnull(MR.[Approved],0),isnull(MR.[Reverse],0),'C' from AMS.Menu as M LEFT OUTER JOIN
				AMS.Menu_Rights AS MR ON MR.Menu_Id=M.Menu_Id and MR.Role_Id=@Code2 where M.Mast_Menu_Id=@Code1  and M.Menu_Id NOT in (SELECT Mast_Menu_Id FROM AMS.Menu)
				declare @Menu_Id int
				declare @Menu_Name varchar(255)
				DECLARE cM2 INSENSITIVE CURSOR FOR
				select Menu_Id,Menu_Name from AMS.Menu where Mast_Menu_Id=@Code1 and Menu_Id  in (SELECT Mast_Menu_Id FROM AMS.Menu)
				OPEN cM2
				FETCH NEXT FROM cM2
				INTO @Menu_Id,@Menu_Name
				WHILE @@FETCH_STATUS = 0
				BEGIN
				INSERT INTO #TEMP1
				SELECT   0 Id,@Menu_Id Menu_Id,@Menu_Name Menu_Name,@Code1 SubModule_Id,convert(bit, 0)  New,convert(bit, 0)   [Save],convert(bit, 0)   [Update],convert(bit, 0)   Copy,convert(bit, 0)   [Delete],convert(bit, 0)   Search,convert(bit, 0)   [Print],convert(bit, 0)   Approved,convert(bit, 0)   Reverse ,'P'
				Union all
				SELECT isnull(MR.MR_Id,0),M.Menu_Id,M.Menu_Name,@Menu_Id SubModule_Id,isnull(MR.New,0),isnull(MR.[Save],0),isnull(MR.[Update],0),isnull(MR.Copy,0),isnull(MR.[Delete],0),isnull(MR.[Search],0),isnull(MR.[Print],0),isnull(MR.[Approved],0),isnull(MR.[Reverse],0),'C' from AMS.Menu as M LEFT OUTER JOIN
				AMS.Menu_Rights AS MR ON MR.Menu_Id=M.Menu_Id  and MR.Role_Id=@Code2  WHERE  M.Mast_Menu_Id=@Menu_Id
				FETCH NEXT FROM cM2
				INTO  @Menu_Id,@Menu_Name
				END
				DEALLOCATE cM2
				SELECT *FROM #TEMP1
				DROP TABLE #TEMP1
				END;

				