INSERT INTO [DataCenter].[dbo].[Relation]([Time],[Status],[Operator],[TeacherID],[DepartmentID],[TopDepartmentID],[Ordinal])
SELECT [Time],'V' AS [Status],[Operator],[UserID],[DepartmentID],[TopDepartmentID],0 AS Ordinal FROM [DataCenter].[dbo].[Relation_VU] WHERE [Status] <> 'O'
