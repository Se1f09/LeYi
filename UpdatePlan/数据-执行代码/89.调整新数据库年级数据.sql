
  USE [Platform_BT]
GO

	 INSERT INTO [dbo].[Catalog]([Id],[ParentId],[TopId],[Type],[Name],[State],[Ordinal])
     VALUES ('85AD57A8-B503-4B83-99B4-9040DADC1B22',NULL,NULL,-4,'高一',1,1)
	 INSERT INTO [dbo].[Catalog]([Id],[ParentId],[TopId],[Type],[Name],[State],[Ordinal])
     VALUES ('AC02A035-24F2-41F5-BA6B-178ED4348009',NULL,NULL,-4,'高二',1,2)
	 INSERT INTO [dbo].[Catalog]([Id],[ParentId],[TopId],[Type],[Name],[State],[Ordinal])
     VALUES ('0E667D0D-0BF5-4D18-9B4D-9096EDBEF969',NULL,NULL,-4,'高三',1,3)


	  UPDATE [dbo].[Catalog] SET Ordinal = 1 WHERE Id = 'c0ce6ba0-dd24-4316-a6c2-b998353cabad'
	  UPDATE [dbo].[Catalog] SET Ordinal = 2 WHERE Id = 'e1dd20c7-e15a-4f6e-bc18-24801292fe29'
	  UPDATE [dbo].[Catalog] SET Ordinal = 3 WHERE Id = 'a00d96d1-6a53-427f-b253-d92ae9e86802'
	  UPDATE [dbo].[Catalog] SET Ordinal = 4 WHERE Id = '077ee587-8cda-44a9-9ea0-08d1c78e61d5'
	  UPDATE [dbo].[Catalog] SET Ordinal = 5 WHERE Id = '8b26e587-8ca9-4d15-b328-08d1c78e7d23'
	  UPDATE [dbo].[Catalog] SET Ordinal = 6 WHERE Id = '58abe587-8c48-443e-ae1a-08d1c78e9641'
	  UPDATE [dbo].[Catalog] SET Ordinal = 7 WHERE Id = '9785e587-8c4d-49ad-b774-08d1c78eb002'
	  UPDATE [dbo].[Catalog] SET Ordinal = 8 WHERE Id = '18c4e587-8c47-4fec-9fea-08d1c78ec896'
	  UPDATE [dbo].[Catalog] SET Ordinal = 9 WHERE Id = 'be0ae587-8cc5-40d7-b3ae-08d1c78ee12f'

	  UPDATE [dbo].[Catalog] SET Ordinal = 10 WHERE Id = 'b25de587-8c11-4b58-a5fe-08d1c78efa2e'
	  UPDATE [dbo].[Catalog] SET Ordinal = 11 WHERE Id = '08dde587-8cfa-4c95-b63b-08d1c78f124d'
	  UPDATE [dbo].[Catalog] SET Ordinal = 12 WHERE Id = '8444e587-8cb6-4d3e-afba-08d1c78f2f5b'


	  UPDATE [dbo].[Catalog] SET Ordinal = 13 WHERE Id = '85ad57a8-b503-4b83-99b4-9040dadc1b22'
	  UPDATE [dbo].[Catalog] SET Ordinal = 14 WHERE Id = 'ac02a035-24f2-41f5-ba6b-178ed4348009'
	  UPDATE [dbo].[Catalog] SET Ordinal = 15 WHERE Id = '0e667d0d-0bf5-4d18-9b4d-9096edbef969'
GO
