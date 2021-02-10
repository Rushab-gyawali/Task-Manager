CREATE OR ALTER PROCEDURE proc_UserLogin
(
@FLAG NVARCHAR(20) =   NULL
,@UserName NVARCHAR(100) = NULL
,@Password NVARCHAR(100) = NULL
,@Ip NVARCHAR(255) = NULL
,@LastLoginTime DATETIME = NULL
,@LoginTime DATETIME = NULL
,@BrowserInfo NVARCHAR(255) = NULL
,@Status BIT = NULL
)


AS
BEGIN

	if(@FLAG='login') 
				BEGIN
				if not exists (select 'X' from [dbo].TBL_USERS where UserName=@UserName and [Password]=@Password )
					begin
					INSERT INTO TBL_LOGIN_LOG(UserName,[Ip],LoginTime,BrowserInfo,[Status])
				     VALUES (@UserName,@Ip,getdate(),@BrowserInfo,'Failed')
					select '1' as CODE,'User not register ' as Msg
					return;
					end
ELSE

begin
	INSERT INTO TBL_LOGIN_LOG(UserName,[Ip],LoginTime,BrowserInfo,[Status])
				VALUES (@UserName,@Ip,getdate(),@BrowserInfo,'Success')
				select '0' as CODE,'Login successful. ' as Msg
				return;
end

			
				return;
				END
END
