USE [Platform_BT]
GO

/****** Object:  View [dbo].[ViewRole]    Script Date: 2015/5/5 16:17:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ViewRole] AS
SELECT UR.RoleId, UR.UserId, V_T.RealName AS UserName, V_T.DepartmentId, R.Name AS RoleName, UR.State FROM UserRole UR INNER JOIN ViewTeacher V_T ON
V_T.Id = UR.UserId AND UR.State < 2 AND V_T.Type = -1 INNER JOIN [Role] R ON UR.RoleId = R.Id AND R.State < 2



GO
