using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using Homory.Model;
using System.Data.Entity.Migrations;

namespace Go
{
	public partial class GoGroup : HomoryResourcePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				Session["F____K"] = keyword.Value;

				course.DataSource =
					HomoryContext.Value.Catalog.Where(o => o.Type == CatalogType.课程 && o.State < State.审核)
						.OrderBy(o => o.State)
						.ThenBy(o => o.Ordinal)
						.ToList();
				course.DataBind();

				grade.DataSource =
					HomoryContext.Value.Catalog.Where(o => (o.Type == CatalogType.年级_九年制 || o.Type == CatalogType.年级_六年制 || o.Type == CatalogType.年级_幼儿园) && o.State < State.审核)
						.OrderBy(o => o.State)
						.ThenBy(o => o.Ordinal)
						.ToList();
				grade.DataBind();

				B();
			}
		}

		protected string CatalogName(object id)
		{
			if (id == null)
				return "";
			var x = Guid.Parse(id.ToString());
			return HomoryContext.Value.Catalog.First(o => o.Id == x).Name;
		}

		protected string UUU(object id)
		{
			var gid = (Guid)id;
			return
				HomoryContext.Value.GroupUser.First(o => o.GroupId == gid && o.Type == GroupUserType.创建者 && o.State < State.审核)
					.User.DisplayName;
		}

		protected string PPP(object id)
		{
			var gid = (Guid)id;
			return
				HomoryContext.Value.GroupUser.First(o => o.GroupId == gid && o.Type == GroupUserType.创建者 && o.State < State.审核)
					.User.Id.ToString();
		}

		protected override bool ShouldOnline
		{
			get { return false; }
		}

		protected void queryG_OnServerClick(object sender, EventArgs e)
		{
			Session["F____K"] = keyword.Value.Trim();
			Session.Remove("F____G");
			Session.Remove("F____C");
			B();
		}

		protected void xk_OnServerClick(object sender, EventArgs e)
		{
			Session["F____C"] = Guid.Parse(((HtmlAnchor)sender).Attributes["data-id"]);
			B();
		}

		protected void nj_OnServerClick(object sender, EventArgs e)
		{
			Session["F____G"] = Guid.Parse(((HtmlAnchor)sender).Attributes["data-id"]);
			B();
		}

		protected void B()
		{
			var kid = Session["F____K"].ToString();
			var s = HomoryContext.Value.Group.Where(o => o.Type == GroupType.教研团队 && o.State < State.审核);
			if (Session["F____C"] != null)
			{
				var cid = (Guid)Session["F____C"];
				s = s.Where(o => o.CourseId == cid);
			}
			if (Session["F____G"] != null)
			{
				var cid = (Guid)Session["F____G"];
				s = s.Where(o => o.GradeId == cid);
			}
			var result = s.ToList().Where(o => o.Serial.Contains(kid) || o.Name.Contains(kid)).ToList();
			int count = result.Count;
			if (count < 3)
			{
				if (count == 1)
				{
					col1.DataSource = result.Take(1);
				}
				else if (count == 2)
				{
					col1.DataSource = result.Take(1);
					col2.DataSource = result.Skip(1).Take(1);
				}
			}
			else
			{
				while (count % 3 != 0)
				{
					count++;
				}
				count = count / 3;
				col1.DataSource = result.Take(count);
				col2.DataSource = result.Skip(count).Take(count);
				col3.DataSource = result.Skip(count * 2).Take(result.Count - count * 2);
			}
			col1.DataBind();
			col2.DataBind();
			col3.DataBind();
		}

		protected void joinG_ServerClick(object sender, EventArgs e)
		{
			if (!IsOnline)
			{
				SignOn();
				return;
			}
			var id = Guid.Parse(((HtmlAnchor)sender).Attributes["data-id"]);
			var gu = new GroupUser();
			gu.GroupId = id;
			gu.Ordinal = 1;
			gu.State = State.启用;
			gu.Time = DateTime.Now;
			gu.Type = GroupUserType.组成员;
			gu.UserId = CurrentUser.Id;
			HomoryContext.Value.GroupUser.AddOrUpdate(gu);
			HomoryContext.Value.SaveChanges();
			B();
		}

		protected bool NoJoin(Guid uid, Guid gid)
		{
			return HomoryContext.Value.GroupUser.Count(o => o.GroupId == gid && o.UserId == uid && o.State < State.审核) > 0;
		}
	}
}
