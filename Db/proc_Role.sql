
CREATE OR ALTER proc [dbo].[proc_Role]
@FLAG		VARCHAR(20),
@Id			BIGINT = NULL,
@UserId		VARCHAR(30) = NULL,
@RoleName	VARCHAR(50) = NULL,
@IsActive	BIT = NULL,
@functionRole	XML = NULL,
@addId		VARCHAR(500) = NULL,
@DeleteId	VARCHAR(500) = NULL,
@User		VARCHAR(50),
@Search		VARCHAR(50) = NULL,
@PageSize	VARCHAR(5)		= 10

AS
SET NOCOUNT ON;
BEGIN TRY
IF @FLAG = 'I'
BEGIN
	INSERT INTO tblRoleSetup(RoleName,IsActive,CreatedBy,CreatedDate)
	SELECT @RoleName,@IsActive,@User,GETDATE()

	SELECT 0 CODE,'Record added successfully' msg,null id
	RETURN
END
ELSE IF @FLAG = 'U'
BEGIN
	
	UPDATE tblRoleSetup 
		SET RoleName = @RoleName
		,IsActive = @IsActive
		,CreatedBy =  @User
		,CreatedDate = GETDATE()
	WHERE roleId = @Id

	SELECT 0 CODE,'Record updated successfully' msg,null id
	RETURN
END
ELSE IF @FLAG = 'A'
BEGIN
	DECLARE @SQL VARCHAR(MAX)

	SET @SQL = '
	SELECT TOP '+@PageSize+' *
	FROM tblRoleSetup U (NOLOCK)
	WHERE u.IsActive = 1 '

	IF @Search IS NOT NULL
		SET @SQL +=' AND RoleName LIKE '''+@Search+'%'''
	
	EXEC(@SQL)
	RETURN
END
ELSE IF @FLAG = 'S'
BEGIN
	SELECT * FROM tblRoleSetup (NOLOCK) WHERE roleId = @Id
	RETURN
END
ELSE IF @FLAG ='getAssignList'
BEGIN

	--SELECT M.sno,m.ModuleName,m.parentGroup,@Id AS RoleId ,ISNULL(X.AddEdit,0) AddEdit,ISNULL(X.DeleteId,0) DeleteId,ISNULL(X.ViewId,0) ViewId
	--FROM Modules M(NOLOCK) 
	--LEFT JOIN  (SELECT * FROM ModulePermissions P (NOLOCK) WHERE P.RoleID = @Id) X ON M.sno = X.ModuleID
	--ORDER BY m.parentGroup
	select Sno = m.MenuId,m.menuGroup,m.ParentGroup,m.menuName,f.parentFunctionId,f.functionId,f.functionName ,@Id RoleId
		,hasChecked = r.RoleId,m.groupPosition
	from tblMenus m
	inner join tblapplicationFunctions f on m.functionId = f.parentFunctionId
	left join (select RoleId,functionId from tblapplicationRoleFunctions where RoleId = @Id) r on f.functionId = r.functionId
	where m.isActive= 1
	order by m.ParentGroup,m.groupPosition

END
ELSE IF @FLAG ='AssignRole'
BEGIN
	DELETE FROM tblApplicationRoleFunctions WHERE RoleId = @Id
	INSERT INTO tblApplicationRoleFunctions(RoleId,FunctionId,CreatedBy,CreatedDate)
	SELECT @Id,P.t.value('@id', 'varchar(10)') AS FunctionId,@User,GETDATE()
    FROM @functionRole.nodes('/root/row') AS P(t);

	SELECT 0 CODE,'Role Assigned successfully' msg,null id
END
ELSE IF @FLAG ='GetUserRole'
BEGIN
	SELECT @RoleName = UserName FROM tblUsers (nolock) WHERE UserId = @Id
	SELECT @UserId = roleId FROM tblUserRoles(NOLOCK) WHERE UserId = @RoleName
	SELECT username = @RoleName,roleid = @UserId
END
ELSE IF @FLAG ='AssignUserRole'
BEGIN
	set @UserId = @RoleName
	delete tblUserRoles WHERE UserId = @UserId

	INSERT INTO tblUserRoles(UserId,RoleId,CreatedBy,CreatedDate)
	SELECT @RoleName,@Id,@User,GETDATE()

	SELECT 0 CODE,'Role Assigned successfully' msg,null id
END
END TRY
BEGIN CATCH
IF @@trancount > 0
ROLLBACK TRANSACTION

SELECT 1 CODE,ERROR_MESSAGE() msg,null id 
END CATCH

