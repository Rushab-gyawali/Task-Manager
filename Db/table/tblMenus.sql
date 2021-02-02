
/****** Object:  Table [dbo].[tblMenus]    Script Date: 6/15/2019 1:50:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblMenus](
	[MenuId] [int] IDENTITY(1,1) NOT NULL,
	[MenuName] [varchar](100) NOT NULL,
	[Url] [varchar](100) NOT NULL,
	[MenuGroup] [varchar](40) NOT NULL,
	[Position] [int] NOT NULL,
	[GroupPosition] [int] NULL,
	[ParentGroup] [varchar](30) NULL,
	[Class] [varchar](30) NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [varchar](30) NULL,
	[CreatedDate] [datetime] NULL,
	[functionId] [varchar](20) NULL
) ON [PRIMARY]
GO


