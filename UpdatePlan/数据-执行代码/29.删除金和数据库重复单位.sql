
DELETE FROM C6.dbo.Department where DeptID in (select min(DeptID) from C6.dbo.Department WHERE  DeptDelFlag = 0 AND UnitID IS NOT NULL AND UnitID <> ''
GROUP BY UnitID HAVING COUNT(*)>1)
GO

DELETE FROM C6.dbo.Department where DeptID in (select min(DeptID) from C6.dbo.Department WHERE  DeptDelFlag = 0 AND UnitID IS NOT NULL AND UnitID <> ''
GROUP BY UnitID HAVING COUNT(*)>1)
GO

DELETE FROM C6.dbo.Department where DeptID in (select min(DeptID) from C6.dbo.Department WHERE  DeptDelFlag = 0 AND UnitID IS NOT NULL AND UnitID <> ''
GROUP BY UnitID HAVING COUNT(*)>1)
GO

DELETE FROM C6.dbo.Department where DeptID in (select min(DeptID) from C6.dbo.Department WHERE  DeptDelFlag = 0 AND UnitID IS NOT NULL AND UnitID <> ''
GROUP BY UnitID HAVING COUNT(*)>1)
GO
