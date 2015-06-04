ALTER TABLE Platform_BT.dbo.DepartmentUser DROP CONSTRAINT PK_DepartmentUser
GO
ALTER TABLE Platform_BT.dbo.DepartmentUser ADD CONSTRAINT PK_DepartmentUser primary key ( DepartmentId,UserId,[Type])
GO