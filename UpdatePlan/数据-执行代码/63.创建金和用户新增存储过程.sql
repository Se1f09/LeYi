USE [C6]
GO
CREATE   proc [dbo].[__InsertUser]
	@strUserID			varchar(20),
	@strUserName   		varchar(30),
	@strPassword		varchar(100),
	@NotInUse			int,
	@strLoginCode	varchar(30),
	@strBMID	varchar(50),
	@strYHID	varchar(50),
	@strMobielTel		varchar(20),
	@strIDCard	varchar(30),
	@strOrder int,
	@strRelaDelFlag int,
	@strGroupID varchar(50)
AS
begin tran

DECLARE @strRelaID varchar(50)
DECLARE @strDeptID varchar(50)
DECLARE @strStatID int

INSERT INTO [dbo].[Users] ([UserID],[UserName],[PassWord],[EmployeeID],[PassWordType],[PassWordTerm],[PassWordLeastLengh],[Status],[SysFlag] ,[UserType],[LoginCode],[UserPy],[UnitID],[YHID])
     VALUES(@strUserID,@strUserName,@strPassword,null,'2',0,0,'A',@NotInUse,0,@strLoginCode,dbo.fn_getPy(@strUserName),@strBMID,@strYHID)

insert into usersinfo(userid,UserInitPassWord,UserMobileTelePhone,SmsNO, UserMobileIsHide,UserNo,YHID)
select @strUserID,'000000',@strMobielTel ,max(convert(int,SmsNO))+1, 0,@strIDCard,@strYhId  from usersinfo 

SELECT @strRelaID = CONVERT(varchar(50), max(convert(int,ID))+1) FROM RelationshipUsers
SELECT @strDeptID = DeptID FROM Department WHERE UnitID = @strBMID OR BMID = @strBMID
SELECT @strStatID = StaID FROM Station WHERE UnitID = @strBMID


insert into RelationshipUsers values('PDU',@strUserID,@strDeptID,'','0',@strOrder,1,@strStatID,@strRelaID,@strRelaDelFlag)


	 if @@error != 0 rollback tran

	 exec pt_UserInsertRelationShipUser 'PDU','',@strUserID
exec pt_UserInsertSmsSet @strUserID,0,'Y',0
exec pt_UserInsertGroup @strUserID,@strGroupID
commit tran


GO


