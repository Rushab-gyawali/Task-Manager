USE [TaskMeroYatra]
GO

/****** Object:  Table [dbo].[TBL_SPRINTSLog]    Script Date: 2/21/2021 4:07:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TBL_SPRINTSLog](
	[LogId] [bigint] IDENTITY(1,1) NOT NULL,
	[RowId] [nvarchar](250) NULL,
	[SprintId] [nvarchar](250) NULL,
	[SprintName] [nvarchar](100) NULL,
	[SprintStatus] [nvarchar](100) NULL,
	[StartedDate] [nvarchar](100) NULL,
	[EndDate] [nvarchar](100) NULL,
	[CreatedBy] [nvarchar](200) NULL,
	[CreatedDate] [datetime] NULL
) ON [PRIMARY]
GO


