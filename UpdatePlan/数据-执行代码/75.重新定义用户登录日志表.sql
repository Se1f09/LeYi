USE [Platform_BT]
GO

/****** Object:  Table [dbo].[SignLog]    Script Date: 2015/5/5 21:54:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[LoginLog]
GO

CREATE TABLE [dbo].[SignLog](
	[Id] [uniqueidentifier] NOT NULL,
	[Time] [datetime2](7) NOT NULL,
	[TriedAccount] [nvarchar](max) NOT NULL,
	[Browser] [nvarchar](max) NOT NULL,
	[IP] [nvarchar](max) NOT NULL,
	[Login] [bit] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[CampusId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_SignLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[SignLog] ADD  CONSTRAINT [DF_SignLog_UserId]  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [UserId]
GO

ALTER TABLE [dbo].[SignLog] ADD  CONSTRAINT [DF_SignLog_CampusId]  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [CampusId]
GO


