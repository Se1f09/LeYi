USE [Platform_BT]
GO

/****** Object:  View [dbo].[ViewQuerySign]    Script Date: 2015/5/5 22:57:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  ALTER VIEW [dbo].[ViewQuerySign] AS
  SELECT L.*, D.Name, U.RealName AS 姓名 FROM [SignLog] L LEFT JOIN [User] U ON U.[State] < 2 AND U.Id = L.UserId LEFT JOIN Department D ON L.CampusId = D.Id AND D.State < 2
GO


