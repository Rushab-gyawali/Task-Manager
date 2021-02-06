
alter proc proc_UserLogin
@Flag		VARCHAR(10)
,@UserName  VARCHAR(30) 
,@Password	VARCHAR(100)
,@Ip		VARCHAR(30) 
,@BrowserInfo VARCHAR(100) 

AS 
SET NOCOUNT ON;

DECLARE @Branch INT

IF @Flag ='Login'
BEGIN
	IF EXISTS(SELECT 'A' FROM tblUsers (NOLOCK) 
		WHERE CONVERT(VARBINARY(255),userName) = CONVERT(VARBINARY(255),@UserName)
		AND CONVERT(VARBINARY(255),UserPwd) = CONVERT(VARBINARY(255),@Password)
		AND IsActive = 1
		)
	BEGIN
		DECLARE @errorTable table(code int,msg varchar(100),id varchar(20))
		declare @sysDate date, @FPIDate date
		
		--select @sysDate = CurrentDate,@FPIDate=CASE WHEN IsFPIBackDated='false' THEN CurrentDate ELSE FPIBackDate END  from tblCommon(nolock)
		
		--SELECT @Branch = Branch FROM tblUsers(NOLOCK) WHERE UserID = @UserName

		--INSERT INTO @errorTable
		--EXEC Proc_tblFraudLogs @FLAG = 'I',@user =@UserName,@LogType='Success',@IpAddress = @Ip
		--		,@Browser = @BrowserInfo,@FunctionId='Login'
		
		declare @CNameNep varchar(100),@CName varchar(100),@addressNep varchar(200),@address varchar(200)
		--select @CNameNep = CompanyNepName,@CName=CompanyName 
		--	,@address = AddressInEng+isnull('<br>Ph. '+PhoneNo1,'')+isnull(' ,'+PhoneNo2,'')+isnull(', F. '+FaxNo,'')
		--	,@addressNep = AddressInNep+isnull('<br>Ph. '+PhoneNo1,'')+isnull(' ,'+PhoneNo2,'')+isnull(', F. '+FaxNo,'')
		--from tblCompany(nolock)

		SELECT 1 code ,msg='Success',UserID,fullName,Email,IsAdminUser,0 Branch ,CAST(0 AS BIT) CanApproveTransaction,CAST(0 AS BIT) AllowBackDate
			,CNameNep=@CNameNep,CName = @CName,addressNep =@addressNep,address = @address,sysDate = @sysDate,'' ForcePwdChange,FPIDate=@FPIDate
		FROM tblUsers(NOLOCK) WHERE userName = @UserName
		RETURN
	END
	ELSE
	BEGIN
		--INSERT INTO @errorTable
		--EXEC Proc_tblFraudLogs @FLAG = 'I',@user =@UserName,@LogType='Success',@IpAddress = @Ip
		--		,@Browser = @BrowserInfo,@FunctionId='Fail'
		
		SELECT 0 CODE,'Login Failed' msg,@UserName id

	END
END