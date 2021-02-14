USE [TaskMeroYatra]
GO
/****** Object:  StoredProcedure [dbo].[proc_BackLog]    Script Date: 2/12/2021 3:02:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   PROCEDURE [dbo].[proc_BackLog]
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
,@CreatedDate DATETIME = NULL
,@IsActive BIT = NULL
,@IsApproved BIT = Null
)

AS 
	BEGIN
		if(@Flag='List') 
			BEGIN
			select * from dbo.TBL_TASK_BACKLOG WITH (NOLOCK) WHERE IsActive = '1'
			END
	

	
			if(@Flag='Insert') 
				BEGIN
				INSERT INTO TBL_TASK_BACKLOG(TaskName,TaskDescription,TaskReportedDate,DiscussionDate,[Owner],ClientId,StoryPoint,CreatedBy,CreatedDate,IsActive, IsApproved)
				VALUES (@TaskName,@TaskDescription,@TaskReportedDate,@DiscussionDate,@Owner,@ClientId,@StoryPoint,@CreatedBy,GETDATE(),1,0)
				select '0' as ErrorCode,'Sucessfully Added' as Msg
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



			if(@Flag='Delete')	
			BEGIN
			DECLARE @IsApprove bit
			SELECT @IsApprove = IsApproved FROM dbo.TBL_TASK_BACKLOG WHERE BackLogId = @BackLogId
			IF(@IsApprove = '1')
			BEGIN
			PRINT 'Not To be Deleted'
			END
			IF(@IsApprove = '0')
			BEGIN
			UPDATE dbo.TBL_TASK_BACKLOG
			SET IsActive = '0'
			WHERE BackLogId = @BackLogId
			select '0' as ErrorCode,'Sucessfully Deleted' as Msg
			END
			
			END


END