using Homory.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Popup_GroupManage : HomoryResourcePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			InitializeHomoryPage();
		}
	}

	protected void InitializeHomoryPage()
	{
		publish_course.DataSource =
			HomoryContext.Value.Catalog.Where(o => o.State < State.审核 && o.Type == CatalogType.课程 && o.Name != "综合")
				.OrderBy(o => o.State)
				.ThenBy(o => o.Ordinal)
				.ToList();
		publish_course.DataBind();
		publish_grade.DataSource =
			HomoryContext.Value.Catalog.Where(o => o.State < State.审核 && (o.Type == CatalogType.年级_幼儿园 || o.Type == CatalogType.年级_六年制 || o.Type == CatalogType.年级_九年制))
				.OrderBy(o => o.State)
				.ThenBy(o => o.Ordinal)
				.ToList();
		publish_grade.DataBind();
	}

	protected void icons_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
	{
		var dir = new DirectoryInfo(Server.MapPath("~/GroupIcon"));
		var files = dir.GetFiles("*.png").Select(o => "~/GroupIcon/" + o.Name).OrderBy(o => o).ToList();
		icons.DataSource = files;
	}

	protected override bool ShouldOnline
	{
		get { return true; }
	}

	protected void btnCreate_ServerClick(object sender, EventArgs e)
	{
		var dir = new DirectoryInfo(Server.MapPath("~/GroupIcon"));
		var files = dir.GetFiles("*.png").Select(o => "~/GroupIcon/" + o.Name).OrderBy(o => o).ToList();
		var g = new Group();
		g.Icon = icons.SelectedItems.Count == 0 ? "~/Common/默认/群组.png" : files[icons.SelectedIndexes[0]];
		g.Id = HomoryContext.Value.GetId();
		g.Introduction = intro.Text;
		g.Name = name.Text;
		g.OpenType = OpenType.互联网;
		g.Ordinal = 1;
		g.State = State.启用;
		g.Type = GroupType.教研团队;
		int s = new Random().Next(10000000, 99999999);
		string sx=s.ToString();
		while(HomoryContext.Value.Group.Count(o=>o.Serial==sx)>0)
		{
			s = new Random().Next(10000000, 99999999);
			sx = s.ToString();
		}
		if (publish_course.SelectedIndex > 0)
			g.CourseId = Guid.Parse(publish_course.SelectedItem.Value);
		if (publish_grade.SelectedIndex > 0)
			g.GradeId = Guid.Parse(publish_grade.SelectedItem.Value);
		g.Serial = sx;
		var gu = new GroupUser();
		gu.GroupId = g.Id;
		gu.Ordinal = 0;
		gu.State = State.启用;
		gu.Time = DateTime.Now;
		gu.Type = GroupUserType.创建者;
		gu.UserId = CurrentUser.Id;
		HomoryContext.Value.Group.Add(g);
		HomoryContext.Value.GroupUser.Add(gu);
		HomoryContext.Value.SaveChanges();
		panel.ResponseScripts.Add("RadCloseRebind();");
	}
}
