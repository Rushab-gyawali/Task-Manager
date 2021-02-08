USE [LOGIN_USER]
GO

/****** Object:  Table [dbo].[TBL_LOGIN_LOG]    Script Date: 2/2/2021 4:30:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TBL_LOGIN_LOG](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](255) NOT NULL,
	[Ip] [nvarchar](255) NOT NULL,
	[LastLoginTime] [datetime] NOT NULL,
	[LoginTime] [datetime] NOT NULL,
	[BrowserInfo] [nvarchar](255) NOT NULL,
	[Status] [bit] NULL
) ON [PRIMARY]
GO


