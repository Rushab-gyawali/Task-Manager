CREATE TABLE TBL_TASK_BACKLOG(
RowId  BIGINT IDENTITY(1,1),
BackLogId VARCHAR(150) PRIMARY KEY,
TaskName NVARCHAR NOT NULL,
TaskDescription NVARCHAR(255)NOT NULL,
TaskReportedDate DATETIME NOT NULL,
DiscussionDate DATETIME NOT NULL,
[Owner] NVARCHAR(100) NOT NULL,
ClientId BIGINT NOT NULL,
StoryPoint NVARCHAR(255) NOT NULL,
CreatedBy NVARCHAR(100) NOT NULL,
CreatedDate DATETIME NOT NULL
);