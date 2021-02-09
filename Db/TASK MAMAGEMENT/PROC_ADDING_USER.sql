CREATE OR ALTER PROCEDURE proc_tblUsers 
(
@Flag NVARCHAR(20) = NULL
,@ID INT = NULL
,@FullName NVARCHAR(150) = NULL
,@UserName NVARCHAR(100) = NULL
,@Email NVARCHAR(100) = NULL
,@PhoneNo NVARCHAR(10) = NULL
,@Password NVARCHAR(100) = NULL
,@CreatedDate DATETIME = NULL
,@ModifiedDate DATETIME = NULL
,@AdminRight NVARCHAR(20) = Null
)

AS 
	BEGIN
		if(@Flag='List') 
			BEGIN
			select * from dbo.TBL_USERS
			END
	

	
			if(@Flag='Insert') 
				BEGIN
				INSERT INTO TBL_USERS(FullName,UserName,Email,PhoneNo,[Password],CreatedDate,AdminRight)
				VALUES (@FullName,@UserName,@Email,@PhoneNo, @Password,GETDATE(),@AdminRight)
				END

		if(@Flag='Delete')	
 
			BEGIN
			delete from TBL_USERS where ID=@ID
			select '0' as ErrorCode,'Sucessfully Deleted' as Msg
			END

		
		if(@Flag = 'Update')
			BEGIN
				UPDATE dbo.TBL_USERS
				SET FullName = @FullName,UserName = @UserName, Email=@Email,PhoneNo = @PhoneNo, [Password]=@Password,ModifiedDate = GETDATE(),AdminRight=@AdminRight
				WHERE ID = @ID
				select '0' as ErrorCode,'Sucessfully Updated' as Msg
			END
			IF(@Flag = 'GetById')
			BEGIN
				SELECT * FROM TBL_USERS WITH (NOLOCK)
				WHERE ID = @ID
				END
END