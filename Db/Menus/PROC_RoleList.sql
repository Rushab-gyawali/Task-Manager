CREATE OR ALTER  PROC PROC_RoleList(
@Flag NVARCHAR(100) = NULL
,@RoleId NVARCHAR(100) = NULL
,@RolesName NVARCHAR(100) = NULL
)

AS 
	BEGIN
	IF(@Flag = 'ListRoles')
		BEGIN
			SELECT RoleId,RoleName FROM dbo.tblRoleSetup WITH(NOLOCK);
		END
END



