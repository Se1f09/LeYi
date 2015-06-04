USE [Platform_BT]
GO

/****** Object:  Table [dbo].[TeacherStatisticsMonthly]    Script Date: 2015/5/6 9:15:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ResourceLog](
	[Id] [uniqueidentifier] NOT NULL,
	[Year] [int] NOT NULL,
	[Month] [int] NOT NULL,
	[Article] [int] NOT NULL,
	[Courseware] [int] NOT NULL,
	[Paper] [int] NOT NULL,
	[Media] [int] NOT NULL,
	[View] [int] NOT NULL,
	[Favourite] [int] NOT NULL,
	[Download] [int] NOT NULL,
	[Comment] [int] NOT NULL,
	[Reply] [int] NOT NULL,
	[Rate] [int] NOT NULL,
	[Credit] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
	[CampusId] uniqueidentifier NOT NULL
 CONSTRAINT [PK_ResourceLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[Year] ASC,
	[Month] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ResourceLog]  WITH CHECK ADD  CONSTRAINT [FK_ResourceLog_User] FOREIGN KEY([Id])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[ResourceLog] CHECK CONSTRAINT [FK_ResourceLog_User]
GO


DROP TABLE  [dbo].[TeacherStatisticsMonthly]
GO