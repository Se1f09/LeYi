USE [C6]
GO

/****** Object:  StoredProcedure [dbo].[__UpdateCampus]    Script Date: 2015/5/3 7:12:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROCEDURE [dbo].[__UpdateDepartment]
@DeptName			varchar(100),
@DeptOrdinal				int,
@PID varchar(50),
@MeID varchar(50),
@DeptState   int

AS

IF(@PID IS NULL OR @PID = '')
BEGIN
UPDATE [dbo].[Department]
   SET [DeptName] = @DeptName
      ,[DeptSort] = @DeptOrdinal
      ,[DeptDesc] = @DeptName
      ,[DeptPy] = dbo.fn_getPy(@DeptName)
	  ,[DeptDelFlag] = @DeptState
 WHERE [BMID] = @MeID
 END
 ELSE
 BEGIN
 DECLARE @ParentId varchar(30)
 SELECT @ParentId = DeptID FROM [Department] WHERE UnitID = @PID OR BMID = @PID
 UPDATE [dbo].[Department]
   SET [DeptName] = @DeptName
      ,[DeptParentID] = @ParentId
      ,[DeptSort] = @DeptOrdinal
      ,[DeptDesc] = @DeptName
      ,[DeptPy] = dbo.fn_getPy(@DeptName)
	  ,[DeptDelFlag] = @DeptState
 WHERE [BMID] = @MeID
 END

 SELECT DeptID FROM [dbo].[Department] WHERE UnitID = @MeID OR BMID = @MeID

GO


