using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.ServiceModel.Activities;
using System.Web;
using System.Web.DynamicData;
using System.Web.Services.Description;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Aspose.Words.Lists;
using Homory.Model;

namespace Go
{
	public partial class GoViewStudio : HomoryResourcePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				leader.DataSource = CurrentGroup.GroupUser.Where(o => o.Type == GroupUserType.创建者).Select(o => o.User).ToList();
                leader.DataBind();

                members.DataSource = CurrentGroup.GroupUser.Where(o => o.Type != GroupUserType.创建者).Select(o => o.User).ToList();
                members.DataBind();

				var list = new List<Catalog>();
				list.AddRange(HomoryContext.Value.Catalog.Where(o => o.Type == CatalogType.团队_名师 && o.ParentId == CurrentGroup.Id).ToList());
                foreach (var catalog in HomoryContext.Value.Catalog.Where(o => o.Type == CatalogType.团队_名师 && o.ParentId == CurrentGroup.Id).ToList())
				{
                    list.AddRange(HomoryContext.Value.Catalog.Where(o => o.Type == CatalogType.团队_名师 && o.ParentId == catalog.Id).ToList());
				}
				catalogs.DataSource = list.Where(o => o.ResourceCatalog.Count(p => p.State == State.启用) > 0).ToList();
				catalogs.DataBind();

                introduction.InnerText = CurrentGroup.Introduction;
			}
		}

		private Group _group;

		protected Group CurrentGroup
		{
			get
			{
				if (_group == null)
				{
					var id = Guid.Parse(Request.QueryString["Id"]);
					_group = HomoryContext.Value.Group.Single(o => o.Id == id);
				}
				return _group;
			}
		}

		protected override bool ShouldOnline
		{
			get { return false; }
		}

        protected void catalogs_OnItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			var catalog = e.Item.DataItem as Catalog;
			var item = e.Item.DataItem;
			dynamic row = e.Item.DataItem;
            var id = row.Id;
			var control = e.Item.FindControl("resources") as Repeater;
			control.DataSource =
				HomoryContext.Value.Resource.ToList().Where(
					o => o.ResourceCatalog.Count(p => p.CatalogId == id) > 0 && o.State == State.启用)
					.ToList();
			control.DataBind();
		}
	}
}
