
USE [DataCenter]
GO
CREATE VIEW [dbo].[UserToMigrate] AS
SELECT U.G_ID AS Id, CardNo AS Account, T.Name AS RealName, T.Name AS DisplayName, '~/Common/Ä¬ÈÏ/ÓÃ»§.png' AS Icon, NEWID() AS Stamp, 'xVpYuF094TdY41x8EhjE0w==' AS [Password], NULL AS PasswordEx, '7U7x0keu+d5EbThVQZzgFlfdVelKNmqml2RRmSi3Y/4=' AS CryptoKey, 'l46OWQ3WRn4RBpAQpUZhDg==' AS CryptoSalt, 1 AS [Type], CASE U.[Status] WHEN 'N' THEN 1 ELSE 4 END AS [State], 50 AS Ordinal, NULL AS [Description] 
,T.Phone, T.IDCard, T.PerStaff, T.ID AS AutoId FROM [DataCenter].[dbo].[Teacher] T INNER JOIN [DataCenter].[dbo].[User] U
ON T.[Status] = 'N' AND (U.[Status] = 'N' OR U.[Status] = 'T') AND T.ID=U.ID
GO
USE [DataCenter]
GO
CREATE VIEW [dbo].[UserToMigrate_User] AS
SELECT [Id]
      ,[Account]
      ,[RealName]
      ,[DisplayName]
      ,[Icon]
      ,[Stamp]
      ,[Password]
      ,[PasswordEx]
      ,[CryptoKey]
      ,[CryptoSalt]
      ,[Type]
      ,[State]
      ,[Ordinal]
      ,[Description]
FROM [DataCenter].[dbo].[UserToMigrate]

USE [DataCenter]
GO
CREATE VIEW [dbo].[UserToMigrate_Teacher] AS
SELECT [Id]
      ,[Phone]
      ,CONVERT(nvarchar(100),NULL) AS [Email]
      ,CONVERT(bit,NULL) AS [Gender]
      ,CONVERT(datetime2(7),NULL) AS [Birthday]
      ,CONVERT(nvarchar(100),NULL) AS [Birthplace]
      ,CONVERT(nvarchar(100),NULL) AS [Address]
      ,CONVERT(nvarchar(100),NULL) AS [Nationality]
      ,[IDCard]
	  ,[AutoId]
	  ,PerStaff
	  ,'True' AS Sync
FROM [DataCenter].[dbo].[UserToMigrate]

