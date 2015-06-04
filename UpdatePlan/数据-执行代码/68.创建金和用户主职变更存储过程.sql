USE [C6]
GO

/****** Object:  StoredProcedure [dbo].[__InsertUser]    Script Date: 2015/5/4 11:07:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   proc [dbo].[__ResetUserFulltime]
	@strYHID	varchar(50),
	@strBMID	varchar(50),
	@strOrder int,
	@delParttime int,
	@strGroupID varchar(50)
AS
begin tran

DECLARE @strRelaID varchar(50)
DECLARE @strDeptID varchar(50)
DECLARE @strStatID int
DECLARE @strUserID varchar(20)

SELECT @strUserID = UserID FROM [Users] WHERE YHID = @strYHID
SELECT @strDeptID = DeptID FROM [Department] WHERE BMID = @strBMID OR UnitID = @strBMID
SELECT @strRelaID = CONVERT(varchar(50), max(convert(int,ID))+1) FROM RelationshipUsers
SELECT @strStatID = StaID FROM Station WHERE UnitID = @strBMID

IF(@delParttime=1)
BEGIN
DELETE RelationshipUsers WHERE UserID = @strUserID
DELETE Data_UserViewDepartment WHERE UserID = @strUserID
END
ELSE
BEGIN
DELETE RelationshipUsers WHERE UserID = @strUserID AND RelaPrimary = 1
END
INSERT INTO RelationshipUsers VALUES('PDU',@strUserID,@strDeptID,'','0',@strOrder,1,@strStatID,@strRelaID,0)


	 if @@error != 0 rollback tran
	 DELETE UserGroupDetail WHERE  UserID = @strUserID
	 exec pt_UserInsertRelationShipUser 'PDU','',@strUserID
	 exec pt_UserInsertGroup @strUserID,@strGroupID
commit tran



GO


