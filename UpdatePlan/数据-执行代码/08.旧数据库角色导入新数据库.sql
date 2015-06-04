DECLARE @Name nvarchar(32)
DECLARE @Range nvarchar(32)
DECLARE @Ordinal int
DECLARE @Id uniqueidentifier
DECLARE TEMP CURSOR FOR SELECT [Name], [Range], [Ordinal] FROM [DataCenter].[dbo].[Role] WHERE [Status] = 'N'
OPEN TEMP
FETCH NEXT FROM TEMP INTO @Name,@Range,@Ordinal
WHILE(@@FETCH_STATUS=0)
BEGIN
SET @Id = NEWID()
INSERT INTO [Platform_BT].[dbo].[Role]([Id],[Name],[State],[Ordinal]) VALUES(@Id,@Name,1,@Ordinal)
IF(@Range = 'Çø¼¶')
BEGIN
INSERT INTO [Platform_BT].[dbo].[RoleRight]([RoleId],[RightName],[State]) VALUES(@Id,'Global',1)
END
FETCH NEXT FROM TEMP INTO @Name,@Range,@Ordinal
END
CLOSE TEMP
DEALLOCATE TEMP
GO

