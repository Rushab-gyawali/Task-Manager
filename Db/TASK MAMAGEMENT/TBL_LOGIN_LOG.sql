CREATE TABLE [dbo].[TBL_LOGIN_LOG](
	ID int IDENTITY(1,1)NOT NULL,
	UserName nvarchar(255) NULL,
	[Ip] nvarchar(255) NULL,
	BrowserInfo nvarchar(255) NULL,
	LoginTime NVARCHAR(255) NULL,
	[Status] NVARCHAR(255) NULL
) ON [PRIMARY]
GO

--DROP TABLE dbo.TBL_LOGIN_LOG