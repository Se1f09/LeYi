USE [Platform_BT]
GO

/****** Object:  View [dbo].[ViewQueryTeacher]    Script Date: 2015/4/14 23:29:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[ViewQueryTeacher] AS
SELECT VT.RealName AS 姓名, 
CASE VT.State WHEN 0 THEN '内置' WHEN 1 THEN '启用' WHEN 2 THEN '审核' WHEN 3 THEN '默认' WHEN 4 THEN '停用' ELSE '删除' END 状态,
CASE VT.PerStaff WHEN 'True' THEN '是' ELSE '否' END 在编,
CASE VT.Sync WHEN 'True' THEN '是' ELSE '否' END 同步,
D.Name 校区,
VT.DepartmentName 部门,
CASE VT.Type WHEN -1 THEN '主职' ELSE '兼职' END 主兼职,
VT.Phone 手机号码,
VT.Email 电子邮件,
VT.IDCard 身份证号,
CASE VT.Gender WHEN 'True' THEN '男' WHEN 'False' THEN '女' ELSE NULL END 性别,
VT.Birthplace 籍贯,
VT.Birthday 出生日期,
VT.Address 现居住地,
VT.Nationality 民族
FROM ViewTeacher VT INNER JOIN Department D ON
VT.TopDepartmentId = D.Id AND D.State < 2 AND VT.Type < 0



GO


