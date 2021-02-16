USE [TaskMeroYatra]
GO

/****** Object:  Table [dbo].[tblRoleSetup]    Script Date: 2/16/2021 10:56:18 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblRoleSetup](
	[RoleId] [INT] IDENTITY(1,1) NOT NULL,
	[RoleName] [VARCHAR](50) NULL,
	[CreatedBy] [VARCHAR](30) NULL,
	[CreatedDate] [DATETIME] NULL,
	[IsActive] [BIT] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblRoleSetup] ADD  DEFAULT (GETDATE()) FOR [CreatedDate]
GO

ALTER TABLE [tblRoleSetup]
ADD CONSTRAINT PK_RoleId PRIMARY KEY(RoleId)


