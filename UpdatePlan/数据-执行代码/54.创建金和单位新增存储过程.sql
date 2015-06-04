USE [C6]
GO

/****** Object:  StoredProcedure [dbo].[__InsertCampus]    Script Date: 2015/5/2 23:04:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROCEDURE [dbo].[__InsertCampus]
@DeptID				varchar(30),
@DeptName			varchar(100),
@DeptOrdinal				int,
@UnitID varchar(50)


AS

INSERT INTO [dbo].[Department]
           ([DeptID]
           ,[DeptName]
           ,[DeptParentID]
           ,[DeptSort]
           ,[DeptDesc]
           ,[DeptDelFlag]
           ,[DeptGuid]
           ,[DeptPy]
           ,[DeptPrincipal]
           ,[UpperLeader]
           ,[IsCompany]
           ,[UnitID]
           ,[BMID])
     VALUES
           (@DeptID
           ,@DeptName
           ,'1'
           ,@DeptOrdinal
           ,@DeptName
           ,0
           ,NULL
           ,dbo.fn_getPy(@DeptName)
           ,''
           ,''
           ,0
           ,@UnitID
           ,NULL)

GO


