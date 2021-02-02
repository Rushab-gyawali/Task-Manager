

/****** Object:  Table [dbo].[tblUsers]    Script Date: 5/22/2017 11:29:11 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tblUsers](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[Email] [nvarchar](max) NULL,
	[PhoneNo] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[FullName] [varchar](100) NULL,
	[BranchId] [int] NULL,
	[IsSuperUser] [bit] NULL,
	[IsAdminUser] [bit] NULL,
	[IsOnline] [bit] NULL,
	[LastOnlineDate] [datetime] NULL,
	[CanApproveTransaction] [bit] NULL,
	[DepartmentId] [int] NULL,
	[DesignationId] [int] NULL,
	[LogId] [bigint] NULL,
	[UserPwd] [varchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


