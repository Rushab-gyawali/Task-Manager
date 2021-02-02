

CREATE TABLE [dbo].[tblApplicationRoleFunctions](
	[RowId] [int] IDENTITY(1,1) NOT NULL,
	[FunctionId] [varchar](10) NULL,
	[RoleId] [int] NULL,
	[CreatedBy] [varchar](30) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](30) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [pk_idx_TblApplicationRoleFunctions_RowId] PRIMARY KEY CLUSTERED 
(
	[RowId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblApplicationRoleFunctions] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO


