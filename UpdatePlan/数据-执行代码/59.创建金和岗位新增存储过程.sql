USE [C6]
GO

CREATE  PROCEDURE [dbo].[__InsertStation]
@StatID				int,
@DeptName			varchar(100),
@DeptOrdinal				int,
@UnitID varchar(50)
AS
INSERT INTO [dbo].[Station]
           ([StaID]
           ,[StaName]
           ,[StaDesc]
           ,[StaIsdriver]
           ,[StaType]
           ,[DelFlag]
           ,[StaOrder]
           ,[UnitID])
     VALUES
           (@StatID
           ,@DeptName
           ,''
           ,0
           ,0
           ,0
           ,@DeptOrdinal
           ,@UnitID)
GO


