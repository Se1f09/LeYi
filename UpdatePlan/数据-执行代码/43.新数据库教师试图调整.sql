USE [Platform_BT]
GO

/****** Object:  View [dbo].[ViewTeacher]    Script Date: 2015/4/14 23:28:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[ViewTeacher] AS
SELECT 
T.Id,
U.Account,
U.RealName,
U.DisplayName,
T.Phone,
T.Email,
T.Gender,
T.Birthday,
T.Birthplace,
T.Address,
T.Nationality,
T.IDCard,
T.PerStaff,
T.Sync,
T.AutoId,
U.Ordinal PriorOrdinal,
DU.DepartmentId,
DU.TopDepartmentId,
DU.State,
D.Name DepartmentName,
D.DisplayName DeaprtmentDisplayName,
D.Level,
DU.Ordinal MinorOrdinal,
DU.Type
FROM [Teacher] T INNER JOIN [User] U
ON T.Id = U.Id AND U.State < 2 LEFT JOIN [DepartmentUser] DU
ON T.Id = DU.UserId AND DU.State < 5 LEFT JOIN [Department] D
ON DU.DepartmentId = D.Id AND D.State < 2



GO


