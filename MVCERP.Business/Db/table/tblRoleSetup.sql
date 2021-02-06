

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblRoleSetup](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](50) NULL,
	[CreatedBy] [varchar](30) NULL,
	[CreatedDate] [datetime] NULL,
	[IsActive] [bit] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblRoleSetup] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO


