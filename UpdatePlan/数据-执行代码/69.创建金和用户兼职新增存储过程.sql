USE [C6]
GO

/****** Object:  StoredProcedure [dbo].[__ResetUserFulltime]    Script Date: 2015/5/4 12:37:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE   proc [dbo].[__InsertUserParttime]
	@strYHID	varchar(50),
	@strBMID	varchar(50),
	@strOrder int
AS
begin tran

DECLARE @strRelaID varchar(50)
DECLARE @strDeptID varchar(50)
DECLARE @strUserID varchar(20)

SELECT @strUserID = UserID FROM [Users] WHERE YHID = @strYHID
SELECT @strDeptID = DeptID FROM [Department] WHERE BMID = @strBMID OR UnitID = @strBMID
SELECT @strRelaID = CONVERT(varchar(50), max(convert(int,ID))+1) FROM RelationshipUsers

INSERT INTO RelationshipUsers VALUES('PDU',@strUserID,@strDeptID,'','0',@strOrder,0,NULL,@strRelaID,0)


	 if @@error != 0 rollback tran
commit tran




GO


