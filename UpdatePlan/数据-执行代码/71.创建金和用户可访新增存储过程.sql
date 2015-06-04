USE [C6]
GO

/****** Object:  StoredProcedure [dbo].[__InsertUserParttime]    Script Date: 2015/5/4 13:22:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE   proc [dbo].[__InsertUserVisitable]
	@strYHID	varchar(50),
	@strUnitID    varchar(50),
	@strBMID	varchar(50)
AS
begin tran

DECLARE @strRelaID varchar(50)
DECLARE @strDeptID varchar(50)
DECLARE @strUserID varchar(20)

SELECT @strUserID = UserID FROM [Users] WHERE YHID = @strYHID
SELECT @strUnitID = DeptID FROM [Department] WHERE UnitID = @strUnitID
SELECT @strDeptID = DeptID FROM [Department] WHERE BMID = @strBMID 
SELECT @strRelaID = CONVERT(varchar(50), max(convert(int,UserViewDeptID))+1) FROM Data_UserViewDepartment


INSERT INTO [dbo].[Data_UserViewDepartment]
           ([UserViewDeptID]
           ,[UserID]
           ,[UnitID]
           ,[DeptID]
           ,[DeleteFlag])
     VALUES
           (@strRelaID
           ,@strUserID
           ,@strUnitID
           ,@strDeptID
           ,0)


	 if @@error != 0 rollback tran
commit tran





GO


