USE [Platform_BT]
GO

/****** Object:  View [dbo].[ViewQueryStudent]    Script Date: 2015/4/26 22:51:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ViewQueryStudent] AS
SELECT VS.RealName AS 姓名, 
CASE VS.State WHEN 0 THEN '内置' WHEN 1 THEN '启用' WHEN 2 THEN '审核' WHEN 3 THEN '默认' WHEN 4 THEN '停用' ELSE '删除' END 状态,
VS.Account AS 账号,
D.Name AS 学校,
SUBSTRING (VS.Account, 3,4) 届,
VS.DepartmentName 班级,
VS.Ordinal 学号,
VS.UniqueId 学籍号,
VS.IDCard 身份证号,
CASE VS.Gender WHEN 'True' THEN '男' WHEN 'False' THEN '女' ELSE NULL END 性别,
VS.Birthplace 籍贯,
VS.Birthday 出生日期,
VS.Address 现居住地,
VS.Nationality 民族,
VS.Charger 联系人,
VS.ChargerContact 联系号码
FROM ViewStudent VS INNER JOIN Department D ON
VS.TopDepartmentId = D.Id AND D.State < 2


GO


