
DECLARE @Level int
DECLARE @Id uniqueidentifier
DECLARE @PId uniqueidentifier
DECLARE TEMP CURSOR FOR SELECT Id, ParentId FROM [Platform_BT].[dbo].[Department]
OPEN TEMP
FETCH NEXT FROM TEMP INTO @Id, @PId
WHILE(@@FETCH_STATUS=0)
BEGIN
	SET @Level = 0
	WHILE(@PId IS NOT NULL)
	BEGIN
		SET @Level = @Level + 1
		SELECT @PId = ParentId FROM [Platform_BT].[dbo].[Department] WHERE Id = @PId
	END
	UPDATE [Platform_BT].[dbo].[Department] SET [Level] = @Level WHERE Id = @Id
	FETCH NEXT FROM TEMP INTO @Id, @PId
END
CLOSE TEMP
DEALLOCATE TEMP
GO
