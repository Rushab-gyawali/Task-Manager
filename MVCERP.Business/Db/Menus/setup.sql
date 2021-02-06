
DECLARE 
	 @ViewFunctionId	VARCHAR(8)
	,@AddEditFunctionId VARCHAR(8)
	,@DeleteFunctionId	VARCHAR(8)
	,@ApproveFuntionId	VARCHAR(8)
	,@AssignFuntionId	VARCHAR(8)
	,@menuName			VARCHAR(50)
	,@ParentGroup		VARCHAR(50)
	,@linkPage			VARCHAR(255)
	,@menuGroup			VARCHAR(50)
	,@Class				VARCHAR(50)
	,@position			INT
	,@isActive			CHAR(1)
	,@groupPosition		INT

--[User Management]
SELECT 
	 @isActive = 1
	,@ParentGroup = 'Setup'
	,@Class = 'mdi mdi-settings'

BEGIN
	
	select @menuGroup = 'Company',@groupPosition = 18
	--1) Company
	SELECT
		 @ViewFunctionId	= '107000'
		,@AddEditFunctionId	= '107010'
		,@menuName			= 'Company'
		,@linkPage			= '/Company'
		,@position = 1
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'
		EXEC proc_AddFunction @AddEditFunctionId, @ViewFunctionId, 'Add/Edit'
	
	--1) User Setup
	SELECT
		 @ViewFunctionId	= '108300'
		,@AddEditFunctionId	= '108310'
		,@menuName			= 'User Setup'
		,@linkPage			= '/User'
		,@position = 1
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'Add'
		EXEC proc_AddFunction @AddEditFunctionId, @ViewFunctionId, 'Add/Edit'
		EXEC proc_AddFunction '108320', @ViewFunctionId, 'Role'

	--2) Permission Setup
	SELECT
		 @ViewFunctionId	= '108400'
		,@AddEditFunctionId	= '108410'
		,@menuName			= 'Permission Setup'
		,@linkPage			= '/Role'
		,@position = 2
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'Add'
		EXEC proc_AddFunction @AddEditFunctionId, @ViewFunctionId, 'Add/Edit'
		EXEC proc_AddFunction '108420', @ViewFunctionId, 'Role'

	
	--6) Static Setup
	SELECT
		 @ViewFunctionId	= '108800'
		,@AddEditFunctionId	= '108810'
		,@menuName			= 'Static Setup'
		,@linkPage			= '/StaticData'
		,@position = 6
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'Add'
		EXEC proc_AddFunction @AddEditFunctionId, @ViewFunctionId, 'Add/Edit'
		EXEC proc_AddFunction '108820', @ViewFunctionId, 'Item'

			--11) Notification Setup 
	SELECT
		 @ViewFunctionId	= '108880'
		,@menuName			= 'Notification Setup'
		,@linkPage			= '/NotificationSetup'
		,@position = 11
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'Add'

	select @menuGroup = 'Account Mapping',@groupPosition = 22
	
END


