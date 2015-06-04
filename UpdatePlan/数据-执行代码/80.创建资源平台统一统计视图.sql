USE Platform_BT
GO
CREATE VIEW ViewQueryResource AS
SELECT L.Id, L.CampusId, L.[Year] AS 年份,
L.Month AS 月份, L.Article AS 发布文章,
L.Courseware AS 发布课件,
L.Paper AS 发布试卷,
L.Media AS 发布视频,
L.[View] AS 浏览资源,
L.Favourite AS 收藏资源,
L.Download AS 下载资源,
L.Comment AS 评论资源,
L.Reply AS 回复评论,
L.Rate AS 评定资源,
L.Credit AS 获得积分,
D.Name AS 学校,
U.RealName AS 教师 FROM ResourceLog L LEFT JOIN [User] U ON U.[State] < 2 AND U.Id = L.Id LEFT JOIN Department D ON L.CampusId = D.Id AND D.State < 2
