USE [TaskMeroYatra]
GO
/****** Object:  StoredProcedure [dbo].[PROC_TBLTASK]    Script Date: 2/18/2021 12:03:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER    PROCEDURE [dbo].[PROC_TBLTASK](
@RowId BIGINT =NULL,
@TaskId VARCHAR(150) = NULL
,@TaskName VARCHAR(150) = NULL
,@TaskStartDate DATETIME = NULL
,@TaskEndDate DATETIME = NULL
,@TaskDescription NVARCHAR(500) = NULL
,@IsActive BIT = NULL
,@Status VARCHAR(80) = NULL
,@CreatedBy VARCHAR(50) = NULL
,@CreatedDate DATETIME = NULL
,@AssignTo VARCHAR(80) = NULL
,@Flag VARCHAR(50) = NULL
,@BacklogList NVARCHAR(max) =NULL
,@SprintName VARCHAR(500) =NULL
,@SprintId NVARCHAR(200) =NULL
,@StartedDate DATETIME =NULL
,@EndDate DATETIME =NULL
,@User NVARCHAR = NULL
)
AS
BEGIN
IF(@Flag = 'Insert')
BEGIN

IF OBJECT_ID('tempdb..#tblTaskdetail') IS NOT NULL
   DROP TABLE #tblTaskdetail
--convert json data to tablular form 
SELECT * INTO #tblTaskdetail FROM OPENJSON(@BacklogList)
WITH( Id NVARCHAR(200),TaskName NVARCHAR(500))


 DELETE FROM #tblTaskdetail WHERE  TaskName IN (SELECT TaskName FROM dbo.TBL_TASKS )

--generate the task id  start here
DECLARE @TaskNoList TABLE(TaskSequence int,Id INT IDENTITY(1,1),BackLogId VARCHAR(80),TaskId VARCHAR(80))
DECLARE @taskNo INT
SELECT @taskNo = ISNULL(TaskSequence,1) FROM tblTaskSequence(NOLOCK)
INSERT INTO @TaskNoList(TaskSequence,BackLogId)
SELECT @taskNo,Id from #tblTaskdetail

UPDATE @TaskNoList SET TaskId = 'Tsk_' +CONVERT(VARCHAR(10),TaskSequence + Id)

DECLARE @newguid NVARCHAR(MAX);
DECLARE @count INT;

SET @newguid=NEWID();
SET @count=(SELECT COUNT(*) FROM dbo.TBL_SPRINTS WHERE SprintId=@newguid)

IF (isnull((SELECT COUNT(TaskName) FROM #tblTaskdetail),0))>0
BEGIN

INSERT INTO dbo.TBL_SPRINTS(SprintId,SprintName,SprintStatus, StartedDate,EndDate)VALUES(@newguid,@SprintName,'1',@StartedDate,@EndDate)

INSERT INTO dbo.TBL_TASKS(TaskId,TaskName,TaskDescription,IsActive,[Status],CreatedBy,CreatedDate,SprintId)

SELECT ls.TaskId,blg.TaskName,blg.TaskDescription,1,'NotAssigned','Kushal',GETDATE(),@newguid 
FROM dbo.TBL_TASK_BACKLOGS  blg(NOLOCK)
INNER JOIN #tblTaskdetail t(NOLOCK) ON blg.BackLogId=t.Id
INNER JOIN @TaskNoList ls ON ls.BackLogId = t.Id

--update the seq. of the task
SELECT @taskNo = COUNT(*) FROM @TaskNoList
UPDATE dbo.tblTaskSequence SET TaskSequence = TaskSequence + @taskNo

END
SELECT '0' as ErrorCode,'Sucessfully Added' as Msg
END



IF(@Flag = 'A')
BEGIN
 SELECT * FROM dbo.TBL_TASKS
END



IF (@Flag = 'S')
BEGIN
SELECT * FROM dbo.TBL_SPRINTS
END



IF(@Flag= 'B')
BEGIN
SELECT * FROM dbo.TBL_TASK_BACKLOGS
END


IF (@Flag = 'C')
BEGIN
UPDATE TBL_TASKS SET Status=@Status WHERE TaskId=@TaskId
SELECT 1
END

IF(@Flag = 'GetbyId')
BEGIN
SELECT * FROM dbo.TBL_SPRINTS s WHERE SprintId=@SprintId
SELECT * FROM dbo.TBL_TASKS WHERE SprintId=@SprintId
END


IF(@Flag= 'Update')
BEGIN

IF OBJECT_ID('tempdb..#tblTaskdetail1') IS NOT NULL
   DROP TABLE #tblTaskdetail1

--convert json data to tablular form 
SELECT * INTO #tblTaskdetail1 FROM OPENJSON(@BacklogList)
WITH( Id NVARCHAR(2000),TaskName NVARCHAR(500))
   PRINT 1

DELETE FROM #tblTaskdetail1 WHERE  TaskName IN (SELECT TaskName FROM dbo.TBL_TASKS WHERE SprintId<>@SprintId)
--==================
SELECT * FROM #tblTaskdetail1
--================
INSERT INTO [dbo].[TBL_TASKSLog] SELECT * FROM   dbo.TBL_TASKS WHERE SprintId=@SprintId
INSERT INTO  [dbo].[TBL_SPRINTSLog]  SELECT * from dbo.TBL_SPRINTS WHERE SprintId=@SprintId

DELETE from dbo.TBL_SPRINTS WHERE SprintId=@SprintId
DELETE from dbo.TBL_TASKS WHERE SprintId=@SprintId

--generate the task id  start here
DECLARE @TaskNoList1 TABLE(TaskSequence int,Id INT IDENTITY(1,1),BackLogId VARCHAR(80),TaskId VARCHAR(80))
DECLARE @taskNo1 INT
SELECT @taskNo1 = ISNULL(TaskSequence,1) FROM tblTaskSequence(NOLOCK)
INSERT INTO @TaskNoList1(TaskSequence,BackLogId)
SELECT @taskNo1,Id from #tblTaskdetail1

UPDATE @TaskNoList1 SET TaskId = 'Tsk_' +CONVERT(VARCHAR(10),TaskSequence + Id)


IF (isnull((SELECT COUNT(TaskName) FROM #tblTaskdetail1),0))>0
BEGIN

INSERT INTO dbo.TBL_SPRINTS(SprintId,SprintName,SprintStatus, StartedDate,EndDate)VALUES(@SprintId,@SprintName,'1',@StartedDate,@EndDate)

INSERT INTO dbo.TBL_TASKS(TaskId,TaskName,TaskDescription,IsActive,[Status],CreatedBy,CreatedDate,SprintId)

SELECT ls.TaskId,blg.TaskName,blg.TaskDescription,1,'NotAssigned','Kushal',GETDATE(),@SprintId 
FROM dbo.TBL_TASK_BACKLOGS  blg(NOLOCK)
INNER JOIN #tblTaskdetail1 t(NOLOCK) ON blg.BackLogId=t.Id
INNER JOIN @TaskNoList1 ls ON ls.BackLogId = t.Id

--update the seq. of the task
SELECT @taskNo1 = COUNT(*) FROM @TaskNoList
UPDATE dbo.tblTaskSequence SET TaskSequence = TaskSequence + @taskNo1
END
END

END
