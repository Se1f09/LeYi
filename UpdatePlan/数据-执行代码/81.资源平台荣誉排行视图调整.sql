USE [Platform_BT]
GO

/****** Object:  View [dbo].[ViewTS]    Script Date: 2015/5/6 10:41:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ViewTS] AS

SELECT U.*, ISNULL(C.Article, 0) Article , ISNULL(C.Courseware, 0) Courseware , ISNULL(C.Paper, 0) Paper , ISNULL(C.Media, 0) Media , ISNULL(C.[View], 0) [View] , ISNULL(C.Download, 0) Download , ISNULL(C.Credit, 0) Credit FROM [User] U LEFT JOIN
(
SELECT Id, SUM(Article) Article, SUM(Courseware) Courseware, SUM(Paper) Paper, SUM(Media) Media, SUM([View]) [View], SUM(Download) Download, SUM(Credit) Credit FROM ResourceLog GROUP BY Id
) C ON U.Id = C.Id



GO


