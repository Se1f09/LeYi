/****** Script for SelectTopNRows command from SSMS  ******/
INSERT INTO [Platform_BT].[dbo].[Catalog]
SELECT TOP 1000 [Id]
      ,[ParentId]
      ,[TopId]
      ,[Type]
      ,[Name]
      ,[State]
      ,[Ordinal]
  FROM [Platform].[dbo].[Catalog] WHERE [Type] = 2