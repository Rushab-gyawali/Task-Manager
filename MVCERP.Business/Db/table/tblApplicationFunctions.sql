

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblApplicationFunctions](
	[FunctionId] [varchar](10) NOT NULL,
	[ParentFunctionId] [varchar](10) NULL,
	[FunctionName] [varchar](50) NULL,
	[CreatedBy] [varchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[RowId] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [pk_idx_appnFunctions_RowId] PRIMARY KEY CLUSTERED 
(
	[RowId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblApplicationFunctions] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO


