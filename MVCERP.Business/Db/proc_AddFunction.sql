CREATE or ALTER PROC [dbo].[proc_AddFunction] (
	 @FunctionId VARCHAR(8)
	,@ParentFunctionId VARCHAR(8)
	,@FunctionText VARCHAR(200)
)
AS
IF NOT EXISTS (SELECT 'X' FROM tblapplicationFunctions WHERE functionId = @FunctionId)
BEGIN
	INSERT INTO tblapplicationFunctions (functionId, parentFunctionId, functionName)
	SELECT @FunctionId, @ParentFunctionId, @FunctionText
	PRINT 'Added function ' + @FunctionId
END	



