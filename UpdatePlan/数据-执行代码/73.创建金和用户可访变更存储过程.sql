USE [C6]
GO

/****** Object:  StoredProcedure [dbo].[__InsertUserParttime]    Script Date: 2015/5/4 13:22:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE   proc [dbo].[__UpdateUserVisitable]
	@strYHID	varchar(50),
	@strBMID	varchar(50)
AS
begin tran

DECLARE @strDeptID varchar(50)
DECLARE @strUserID varchar(20)

SELECT @strUserID = UserID FROM [Users] WHERE YHID = @strYHID
SELECT @strDeptID = DeptID FROM [Department] WHERE BMID = @strBMID 


DELETE [dbo].[Data_UserViewDepartment] WHERE UserID = @strUserID AND DeptID = @strDeptID


	 if @@error != 0 rollback tran
commit tran





GO


