USE [C6]
GO

/****** Object:  StoredProcedure [dbo].[__InsertCampus]    Script Date: 2015/5/3 6:54:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROCEDURE [dbo].[__InsertDepartment]
@DeptID				varchar(30),
@DeptName			varchar(100),
@DeptOrdinal				int,
@PID varchar(50),
@MeID varchar(50)


AS

DECLARE @ParentId  varchar(30)
SELECT @ParentId = DeptID FROM Department WHERE UnitID = @PID OR BMID = @PID

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
           ,@ParentId
           ,@DeptOrdinal
           ,@DeptName
           ,0
           ,NULL
           ,dbo.fn_getPy(@DeptName)
           ,''
           ,''
           ,0
           ,NULL
           ,@MeID)

GO


