
CREATE OR ALTER PROC [dbo].[proc_addMenu]
	@functionId			VARCHAR(8)	
	,@menuName			VARCHAR(50)
	,@linkPage			VARCHAR(255)
	,@menuGroup			VARCHAR(50)
	,@position			INT
	,@isActive			CHAR(1)
	,@groupPosition		INT
	,@ParentGroup		VARCHAR(50) = NULL
	,@Class				VARCHAR(50) = NULL
	
AS


IF NOT EXISTS (SELECT 'X' FROM tblMenus WHERE functionId = @functionId)
BEGIN	
	INSERT INTO tblMenus (
		 functionId, menuName, Url, menuGroup, position, isActive, groupPosition,ParentGroup,Class
	)
	SELECT @functionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition, @ParentGroup,@Class
	PRINT @menuName  + ' menu added.'		
END




