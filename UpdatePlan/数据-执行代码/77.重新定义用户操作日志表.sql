USE [Platform_BT]
GO

/****** Object:  Table [dbo].[OperationLog]    Script Date: 2015/5/5 23:40:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[OperationStatistics]
GO


CREATE TABLE [dbo].[OperationLog](
	[Id] [uniqueidentifier] NOT NULL,
	[Time] [datetime2](7) NOT NULL,
	[Type] [int] NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[CampusId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_OperationLog_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[OperationLog] ADD  CONSTRAINT [DF_OperationLog_UserId]  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [UserId]
GO

ALTER TABLE [dbo].[OperationLog] ADD  CONSTRAINT [DF_OperationLog_CampusId]  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [CampusId]
GO


