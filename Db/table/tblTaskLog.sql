USE [TaskMeroYatra]
GO

/****** Object:  Table [dbo].[TBL_TASKSLog]    Script Date: 2/21/2021 4:10:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TBL_TASKSLog](
	[LogId] [bigint] IDENTITY(1,1) NOT NULL,
	[RowId] [nvarchar](250) NULL,
	[TaskId] [varchar](150) NULL,
	[TaskName] [varchar](150) NULL,
	[TaskStartDate] [datetime] NULL,
	[TaskEndDate] [datetime] NULL,
	[TaskDescription] [nvarchar](500) NULL,
	[IsActive] [bit] NULL,
	[Status] [varchar](80) NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[AssignTo] [nvarchar](200) NULL,
	[SprintId] [nvarchar](300) NULL
) ON [PRIMARY]
GO


