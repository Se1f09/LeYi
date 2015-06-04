USE [Platform_BT]
GO

/****** Object:  View [dbo].[ViewQuerySign]    Script Date: 2015/5/5 23:43:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


  CREATE VIEW [dbo].[ViewQueryOperation] AS
  SELECT L.*, D.Name, U.RealName AS ÐÕÃû FROM [OperationLog] L LEFT JOIN [User] U ON U.[State] < 2 AND U.Id = L.UserId LEFT JOIN Department D ON L.CampusId = D.Id AND D.State < 2

GO


