  SELECT * FROM [DataCenter].[dbo].[RelationToMigrate]
  USE DataCenter
  GO
  CREATE TABLE [dbo].[DepartmentUser](
	[DepartmentId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[TopDepartmentId] [uniqueidentifier] NOT NULL,
	[Type] [int] NOT NULL,
	[State] [int] NOT NULL,
	[Ordinal] [int] NOT NULL
) ON [PRIMARY]
GO

INSERT INTO DataCenter.[dbo].[DepartmentUser]
SELECT [DepartmentId]
      ,[UserId]
      ,[TopDepartmentId]
      ,CASE [Type] WHEN 'N' THEN -1 WHEN 'S' THEN -3 WHEN 'B' THEN -4 WHEN 'P' THEN -2 WHEN 'V' THEN -5 END AS [Type]
      ,[State]
      ,[Ordinal]
  FROM [DataCenter].[dbo].[RelationToMigrate] WHERE DepartmentId IS NOT NULL
  AND TopDepartmentId IS NOT NULL AND UserId IS NOT NULL
  GO