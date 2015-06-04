USE C6
GO
CREATE View [dbo].[HomorySsoView] AS
SELECT UO.Id, U.Account, U6.UserID FROM [Platform_BT].[dbo].[UserOnline] UO INNER JOIN [Platform_BT].[dbo].[User] U
ON UO.UserId = U.Id INNER JOIN [C6].[dbo].[Users] U6 
ON U.Account = U6.LoginCode
Go