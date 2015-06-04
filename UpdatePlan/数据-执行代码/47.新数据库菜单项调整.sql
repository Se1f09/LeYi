USE [Platform_BT]
GO

INSERT INTO [dbo].[Menu]
           ([Id]
           ,[ParentId]
           ,[ApplicationId]
           ,[Name]
           ,[Redirect]
           ,[Icon]
           ,[State]
           ,[Ordinal]
           ,[RightName])
     VALUES
           ('D1C484AC-0D13-484E-A2D6-6A6B223C34BE'
           ,'848FE587-8C1F-4DFE-AF4C-08D1C65D8783'
           ,'3047E587-8CC1-4645-8536-08D1AF49409F'
           ,'µÇÂ¼Í³¼Æ'
           ,'StatisticsLogin'
           ,''
           ,1
           ,7
           ,'QueryActivity')
GO
