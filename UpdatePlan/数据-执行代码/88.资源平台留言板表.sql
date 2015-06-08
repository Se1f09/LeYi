USE [Platform_BT]
GO

/****** Object:  Table [dbo].[ResourceCommentTemp]    Script Date: 2015-06-06 10:34:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GroupBoard](
	[Id] [uniqueidentifier] NOT NULL,
	[GroupId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[Time] [datetime2](7) NOT NULL,
	[State] [int] NOT NULL,
 CONSTRAINT [PK_GroupBoard] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[GroupBoard]  WITH CHECK ADD  CONSTRAINT [FK_GroupBoard_Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([Id])
GO

ALTER TABLE [dbo].[GroupBoard] CHECK CONSTRAINT [FK_GroupBoard_Group]
GO

ALTER TABLE [dbo].[GroupBoard]  WITH CHECK ADD  CONSTRAINT [FK_GroupBoard_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[GroupBoard] CHECK CONSTRAINT [FK_GroupBoard_User]
GO


