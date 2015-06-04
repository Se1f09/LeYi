USE [C6]
GO

/****** Object:  StoredProcedure [dbo].[__UpdateUser]    Script Date: 2015/5/4 9:58:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE   proc [dbo].[__UpdateUserPassword]
	@strYHID	varchar(50),
	@strPassword varchar(100)
AS


UPDATE [dbo].[Users] SET [PassWord] = @strPassword  WHERE YHID = @strYHID






GO


