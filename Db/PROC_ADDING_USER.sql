CREATE PROCEDURE PROC_ADDING_USER
(
@Flag NVARCHAR(20) = NULL
,@ID INT = NULL
,@UserName NVARCHAR(100) = NULL
,@Email NVARCHAR(100) = NULL
,@Password NVARCHAR(100) = NULL
)

AS 
	BEGIN
		if(@Flag='List') 
			BEGIN
			select * from TBL_USERS
			END
	

	
			if(@Flag='Insert') 
				BEGIN
				INSERT INTO TBL_USERS(UserName,Email,[Password])
				VALUES (@UserName,@Email, @Password)
				END

		if(@Flag='Delete')	
 
			BEGIN
			delete from TBL_USERS where ID=@ID
			select '0' as ErrorCode,'Sucessfully Deleted' as Msg
			END

		
		if(@Flag = 'Update')
			BEGIN
				UPDATE TBL_USERS
				set UserName = @UserName, Email=@Email,[Password]=@Password
				where ID = @ID
				select '0' as ErrorCode,'Sucessfully Updated' as Msg
			END
END