USE Platform_BT
GO
CREATE VIEW dbo.System_Department_Full AS
SELECT * FROM Platform_BT.dbo.Department D1 INNER JOIN C6.dbo.Department D2 ON
(D1.Id = D2.UnitID AND D1.[State] < 2 AND (D1.[Type] = 1 OR D1.[Type] = 0) AND D2.DeptDelFlag = 0 AND D2.UnitID IS NOT NULL AND D2.UnitID <> '')
OR
(D1.Id = D2.BMID AND D1.[State] < 2 AND (D1.[Type] = 1 OR D1.[Type] = 0) AND D2.DeptDelFlag = 0 AND D2.BMID IS NOT NULL AND D2.BMID <> '')