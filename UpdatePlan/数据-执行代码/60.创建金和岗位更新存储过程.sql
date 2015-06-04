USE [C6]
GO

/****** Object:  StoredProcedure [dbo].[__UpdateCampus]    Script Date: 2015/5/3 0:09:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROCEDURE [dbo].[__UpdateStation]
@UnitID varchar(50),
@DeptName			varchar(100),
@DeptOrdinal				int,
@DeptState   int

AS

UPDATE [dbo].[Station]
   SET StaName = @DeptName
      ,StaOrder = @DeptOrdinal
	  ,DelFlag = @DeptState
 WHERE [UnitID] = @UnitID

GO


