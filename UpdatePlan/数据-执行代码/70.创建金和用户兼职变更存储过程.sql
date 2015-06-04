USE [C6]
GO

/****** Object:  StoredProcedure [dbo].[__ResetUserFulltime]    Script Date: 2015/5/4 12:37:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE   proc [dbo].[__UpdateUserParttime]
	@strYHID	varchar(50),
	@strBMID	varchar(50),
	@strOrder int,
	@DelFlag int
AS
begin tran

DECLARE @strDeptID varchar(50)
DECLARE @strUserID varchar(20)

SELECT @strUserID = UserID FROM [Users] WHERE YHID = @strYHID
SELECT @strDeptID = DeptID FROM [Department] WHERE BMID = @strBMID OR UnitID = @strBMID

IF(@DelFlag=0)
BEGIN
UPDATE RelationshipUsers SET  UserOrder = @strOrder WHERE  UserID = @strUserID AND DeptID = @strDeptID
END
ELSE
BEGIN
DELETE RelationshipUsers WHERE UserID = @strUserID AND DeptID = @strDeptID AND RelaPrimary = 0
END

	 if @@error != 0 rollback tran
commit tran




GO


