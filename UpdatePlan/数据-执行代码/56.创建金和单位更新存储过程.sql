USE [C6]
GO

CREATE  PROCEDURE [dbo].[__UpdateCampus]
@UnitID varchar(50),
@DeptName			varchar(100),
@DeptOrdinal				int,
@DeptState   int

AS

UPDATE [dbo].[Department]
   SET [DeptName] = @DeptName
      ,[DeptSort] = @DeptOrdinal
      ,[DeptDesc] = @DeptName
      ,[DeptPy] = dbo.fn_getPy(@DeptName)
	  ,[DeptDelFlag] = @DeptState
 WHERE [UnitID] = @UnitID
GO


