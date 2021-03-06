USE [TaskMeroYatra]
GO
/***** Object: StoredProcedure [dbo].[proc_tblUsers] Script Date: 2/14/2021 5:34:48 PM *****/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[proc_tblUsers]
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
,@AdminRight NVARCHAR(20) = NULL
,@isActive BIT = NULL
,@CreatedBy NVARCHAR(100) = NULL
,@OldPwd NVARCHAR(100) = Null
)

AS
BEGIN
if(@Flag='List')
BEGIN
select * from dbo.TBL_USERS WITH (NOLOCK) WHERE isActive = '1'
END



if(@Flag='Insert')
BEGIN
INSERT INTO TBL_USERS(FullName,UserName,Email,PhoneNo,[Password],CreatedDate,AdminRight,CreatedBy)
VALUES (@FullName,@UserName,@Email,@PhoneNo, @Password,GETDATE(),@AdminRight,@CreatedBy)
END

if(@Flag='Delete')

BEGIN
UPDATE dbo.TBL_USERS
SET isActive = '0'
WHERE Id = @ID

select '0' as ErrorCode,'Sucessfully Deleted' as Msg
END


if(@Flag = 'Update')
BEGIN
UPDATE dbo.TBL_USERS
SET FullName = @FullName,UserName = @UserName, Email=@Email,PhoneNo = @PhoneNo, [Password]=@Password,ModifiedDate = GETDATE(),AdminRight=@AdminRight,CreatedBy = @CreatedBy
WHERE ID = @ID
select '0' as ErrorCode,'Sucessfully Updated' as Msg
END
IF(@Flag = 'GetById')
BEGIN
SELECT * FROM TBL_USERS WITH (NOLOCK)
WHERE ID = @ID
END



BEGIN
IF(@FLAG = 'ListProfile')
BEGIN
SELECT * FROM dbo.TBL_USERS WHERE UserName = @UserName
END
END

IF(@FLAG = 'ChangePwd')
BEGIN
UPDATE dbo.TBL_USERS
SET [Password] = @Password
WHERE ID = @ID
select '0' as ErrorCode,'Sucessfully Updated' as Msg
END
IF(@Flag = 'UserDropDown')
SELECT UserName AS UserName FROM dbo.TBL_USERS
END