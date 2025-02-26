	ALTER PROCEDURE [AMS].[Usp_IUD_User_Role]
					(
						@Event char(2) = 'I',
						@Id Int = 0,
						@Role varchar(50)=NULL,
						@Status bit=1,
						@Msg Varchar(max) output ,
						@Return_Id int output
					)
					As
					If @Event = 'I'  --for Insert
					Begin
						Insert Into AMS.User_Role Values
						(
							@Role,
							@Status
						)
						Set @Return_Id = @@IDENTITY
						Set @Msg = 'Record Inserted Successfully'

					End
					Else
					If @Event = 'U'    --- Update
					Begin
						Update AMS.User_Role Set
							[Role] = @Role,
							[Status] = @Status
							Where [Role_Id] = @Id

						Set @Msg = 'Record Updated Successfully'
					End
					Else
					If @Event = 'D'   -- For Delete
					Begin
						Delete from AMS.User_Role Where [Role_Id] = @Id
						Set @Msg = 'Record Deleted Successfully'
					End;