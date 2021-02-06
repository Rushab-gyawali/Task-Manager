
CREATE OR ALTER PROC [dbo].[proc_GetMenus]
	 @flag			VARCHAR(10) = NULL
	,@userName		VARCHAR(50) = NULL

AS

IF @flag = 'MENU'
BEGIN
	IF @username IN('admin','superAdmin')
	BEGIN
		
		select m.MenuName,linkPage = m.Url,m.MenuGroup,m.ParentGroup,m.Class
		from tblMenus m(nolock)
		where m.isActive = 1
		order by m.groupPosition,m.ParentGroup

		select FunctionId from tblApplicationFunctions(nolock)
		--select m.MenuName,m.Url,m.MenuGroup,m.ParentGroup,m.Class,f.FunctionId
		--from tblMenus m(nolock)
		--inner join tblApplicationFunctions f (nolock)on m.functionId = f.parentFunctionId
		--where m.isActive = 1
		--order by m.ParentGroup,m.groupPosition
		RETURN	
	END
	ELSE
	BEGIN
		
		select m.MenuName,linkPage = m.Url,m.MenuGroup,m.ParentGroup,m.Class
		from tblMenus m(nolock)
		inner join(
		select distinct a.ParentFunctionId from tblUserRoles r
		inner join tblApplicationRoleFunctions f on f.RoleId = r.RoleId
		inner join tblApplicationFunctions a on a.FunctionId = f.FunctionId
		where r.UserId = @userName
		)x on x.ParentFunctionId = m.functionId
		where m.isActive = 1

		select f.FunctionId
		from tblApplicationFunctions f(nolock)
		inner join tblApplicationRoleFunctions ar(nolock) on ar.FunctionId = f.FunctionId
		inner join tblUserRoles r(nolock) on r.RoleId = ar.RoleId
		where r.UserId = @userName

		
		RETURN	
	END
END

