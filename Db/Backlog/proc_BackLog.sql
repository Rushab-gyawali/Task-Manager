CREATE OR ALTER PROCEDURE proc_BackLog
(
@Flag NVARCHAR(100) = NULL
,@BackLogId BIGINT = NULL
,@TaskName NVARCHAR(255) = NULL
,@TaskDescription NVARCHAR(255) = NULL
,@TaskReportedDate DATETIME = NULL
,@DiscussionDate DateTime = NULL
,@Owner NVARCHAR(255) = NULL
,@ClientId BIGINT = NULL
,@StoryPoint NVARCHAR(255) = NULL
,@CreatedBy NVARCHAR(100) = NULL
,@CreatedDate DATETIME = Null
)

AS 
	BEGIN
		if(@Flag='List') 
			BEGIN
			select * from dbo.TBL_TASK_BACKLOG WITH (NOLOCK) --WHERE isActive = '1'
			END
	

	
			if(@Flag='Insert') 
				BEGIN
				INSERT INTO TBL_TASK_BACKLOG(BackLogId,TaskName,TaskDescription,TaskReportedDate,DiscussionDate,[Owner],ClientId,StoryPoint,CreatedBy,CreatedDate)
				VALUES (@BackLogId,@TaskName,@TaskDescription,@TaskReportedDate,@DiscussionDate,@Owner,@ClientId,@StoryPoint,@CreatedBy,GETDATE())
				END

		

		
		if(@Flag = 'Update')
			BEGIN
				UPDATE dbo.TBL_TASK_BACKLOG
				SET TaskName = @TaskName ,TaskDescription = @TaskDescription,TaskReportedDate = @TaskReportedDate, DiscussionDate = @DiscussionDate, [Owner] = @Owner, ClientId = @ClientId, StoryPoint = @StoryPoint, CreatedBy = @CreatedBy,CreatedDate = GETDATE()
				WHERE BackLogId = @BackLogId
				select '0' as ErrorCode,'Sucessfully Updated' as Msg
			END
			IF(@Flag = 'GetById')
			BEGIN
				SELECT * FROM dbo.TBL_TASK_BACKLOG WITH (NOLOCK)
				WHERE BackLogId = @BackLogId
				END
END