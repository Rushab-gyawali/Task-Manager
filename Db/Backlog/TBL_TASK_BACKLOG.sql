CREATE TABLE TBL_TASK_BACKLOG(
RowId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
BackLogId VARCHAR(500) NOT NULL,

TaskName NVARCHAR(255) NOT NULL,
TaskDescription NVARCHAR(255)NOT NULL,
TaskReportedDate DATETIME NOT NULL,
DiscussionDate DATETIME NOT NULL,
[Owner] NVARCHAR(100) NOT NULL,
ClientId BIGINT NOT NULL,
StoryPoint NVARCHAR(255) NOT NULL,
CreatedBy NVARCHAR(100) NOT NULL,
CreatedDate DATETIME NOT NULL
);


ALTER TABLE dbo.TBL_TASK_BACKLOG
ADD IsActive BIT NOT NULL;

ALTER TABLE dbo.TBL_TASK_BACKLOG
ADD IsApproved BIT NOT NULL;


SELECT * FROM dbo.TBL_TASK_BACKLOG




