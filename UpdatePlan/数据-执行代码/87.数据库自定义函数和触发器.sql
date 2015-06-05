create function PadLeft(@num varchar(16),@paddingChar char(1),@totalWidth int)

returns varchar(16) as

begin

declare @curStr varchar(16)

select @curStr = isnull(replicate(@paddingChar,@totalWidth - len(isnull(@num ,0))), '') + @num

return @curStr

end


UPDATE [Group] SET Icon = '~/Common/头像/随机/' + dbo.PadLeft( CAST(cast( floor(rand()*128) as int) as nvarchar(3) ),'0',3) + '.png' WHERE Icon = '~/Common/默认/群组.png'

UPDATE [User] SET Icon = '~/Common/头像/随机/' + dbo.PadLeft( CAST(cast( floor(rand()*128) as int) as nvarchar(3) ),'0',3) + '.png' WHERE Icon = '~/Common/默认/用户.png'

CREATE TRIGGER dbo.GenUserIcon
   ON  dbo.[User]
   AFTER INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Id uniqueidentifier
	DECLARE @Icon nvarchar(MAX)
	SELECT @Id = Id, @Icon = Icon FROM inserted
	IF(@Icon = '~/Common/默认/用户.png')
	BEGIN
		UPDATE [User] SET Icon = '~/Common/头像/随机/' + dbo.PadLeft( CAST(cast( floor(rand()*128) as int) as nvarchar(3) ),'0',3) + '.png' WHERE Id = @Id
	END

END
GO

CREATE TRIGGER dbo.GenStudioIcon
   ON  dbo.[Group]
   AFTER INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Id uniqueidentifier
	DECLARE @Icon nvarchar(MAX)
	SELECT @Id = Id, @Icon = Icon FROM inserted
	IF(@Icon = '~/Common/默认/群组.png')
	BEGIN
		UPDATE [Group] SET Icon = '~/Common/头像/随机/' + dbo.PadLeft( CAST(cast( floor(rand()*128) as int) as nvarchar(3) ),'0',3) + '.png' WHERE Id = @Id
	END

END
GO
