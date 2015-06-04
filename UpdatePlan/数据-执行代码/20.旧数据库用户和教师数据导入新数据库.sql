INSERT INTO [Platform_BT].[dbo].[User]
SELECT * FROM [DataCenter].[dbo].[UserToMigrate_User]

INSERT INTO [Platform_BT].[dbo].[Teacher]
SELECT * FROM [DataCenter].[dbo].[UserToMigrate_Teacher]
