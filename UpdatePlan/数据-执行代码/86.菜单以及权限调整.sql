USE Platform_BT
 GO

INSERT INTO [Right](Name, ApplicationId) VALUES ('OtherPublish', '3047E587-8CC1-4645-8536-08D1AF49409F')
INSERT INTO [Right](Name, ApplicationId) VALUES ('StatisticsOperation', '3047E587-8CC1-4645-8536-08D1AF49409F')
INSERT INTO [Right](Name, ApplicationId) VALUES ('StatisticsLogin', '3047E587-8CC1-4645-8536-08D1AF49409F')
UPDATE [Menu] SET [RightName] = 'StatisticsOperation' WHERE Id = '65E9E587-8CF5-4584-BB49-08D1C65E0F51'
UPDATE [Menu] SET [RightName] = 'StatisticsLogin' WHERE Id = 'D1C484AC-0D13-484E-A2D6-6A6B223C34BE'