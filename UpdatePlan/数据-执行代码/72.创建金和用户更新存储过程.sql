USE [C6]
GO

/****** Object:  StoredProcedure [dbo].[__InsertUser]    Script Date: 2015/5/4 8:48:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   proc [dbo].[__UpdateUser]
	@strUserName   		varchar(30),
	@NotInUse			int,
	@strLoginCode	varchar(30),
	@strYHID	varchar(50),
	@strMobielTel		varchar(20),
	@strIDCard	varchar(30),
	@strOrder int
AS
begin tran
DECLARE @strUserID varchar(20)

SELECT @strUserID = UserID FROM [Users] WHERE YHID = @strYHID

UPDATE [dbo].[Users] SET UserName = @strUserName, [SysFlag] = @NotInUse, [LoginCode] = @strLoginCode, [UserPy]=dbo.fn_getPy(@strUserName) WHERE UserID = @strUserID
UPDATE [dbo].[UsersInfo] SET UserMobileTelePhone = @strMobielTel,UserNo = @strIDCard, DeleteFlag = @NotInUse  WHERE UserID = @strUserID
UPDATE [dbo].[RelationshipUsers] SET @strOrder = @strOrder WHERE UserID = @strUserID AND RelaPrimary = 1 AND DelFlag = 0


	 if @@error != 0 rollback tran

	
commit tran



GO


