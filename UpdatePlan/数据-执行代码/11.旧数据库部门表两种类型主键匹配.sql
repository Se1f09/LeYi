DECLARE @Min int
DECLARE @Max int
DECLARE @Count int
DECLARE @Id uniqueidentifier
SELECT @Min = MIN(ID) FROM [DataCenter].[dbo].[Department] WHERE [Status] = 'N'
SELECT @Max = MAX(ID) FROM [DataCenter].[dbo].[Department] WHERE [Status] = 'N'
DECLARE @Loop int
SET @Loop = @Min
WHILE(@Loop<=@Max)
BEGIN
SELECT @Count=COUNT(*) FROM [DataCenter].[dbo].[Department] WHERE [Status] = 'N' AND [ID]=@Loop
IF(@Count>0)
BEGIN
	SET @Id = NEWID()
	UPDATE [DataCenter].[dbo].[Department] SET [G_ID] = @Id WHERE [ID] = @Loop AND [Status] = 'N'
	UPDATE [DataCenter].[dbo].[Department] SET [G_ParentID] = @Id WHERE [ParentID] = @Loop AND [Status] = 'N'
	UPDATE [DataCenter].[dbo].[Department] SET [G_TopID] = @Id WHERE [TopDepartmentID] = @Loop AND [Status] = 'N'
END
SET @Loop=@Loop+1
END
GO