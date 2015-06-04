USE Platform_BT
GO
CREATE VIEW dbo.System_Teacher_Full AS
SELECT U1.*,
 [UserID]
      ,[UserName]
      ,U2.[PassWord] AS C6PassWord
      ,[EmployeeID]
      ,[PassWordType]
      ,[PassWordTerm]
      ,[PassWordLeastLengh]
      ,[PWDLastUpdateTime]
      ,[PhotoURL]
      ,[Status]
      ,[LonginStatus]
      ,[LastLonginActive]
      ,[SysFlag]
      ,[UserType]
      ,[LastFlushTime]
      ,[SessionID]
      ,[LoginCode]
      ,[LoginIp]
      ,[RegTime]
      ,[Reg_AipPower]
      ,[UserPy]
      ,[LastLonginIP]
      ,[WorkStatus]
      ,[InPhase]
      ,[UnitID]
      ,[YHID] FROM [Platform_BT].dbo.[User] U1 LEFT JOIN C6.dbo.Users U2 ON
U1.Account = U2.LoginCode AND U2.SysFlag = 0 AND U1.[Type] = 1