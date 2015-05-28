using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Activities;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Homory.Model;
using Telerik.Web.UI;
using Resource = Homory.Model.Resource;
using ResourceType = Homory.Model.ResourceType;

namespace Go
{
	public partial class GoViewVideo :  System.Web.UI.Page
	{
        protected Lazy<Entities> HomoryContext = new Lazy<Entities>(() => new Entities());
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				player.Video = CurrentResource.Preview;
				catalog.Visible = CurrentResource.Type == ResourceType.视频;
				HomoryContext.Value.ST_Resource(CurrentResource.Id, ResourceOperationType.View, 1);
				CurrentResource.View += 1;
				HomoryContext.Value.SaveChanges();
			}
		}

		private Resource _resource;

		protected static string QueryType(CatalogType type)
		{
			switch (type)
			{
				case CatalogType.年级_幼儿园:
                case CatalogType.年级_六年制:
                case CatalogType.年级_九年制:
					{
						return "Grade";
					}
				case CatalogType.课程:
					{
						return "Course";
					}
				default:
					{
						return "Catalog";
					}
			}
		}

		protected Func<string, ResourceCatalog, string> Combine = (a, o) => string.Format("{0}<a target='_blank' href='../Go/Search?{2}={3}'>{1}</a>、", a, o.Catalog.Name, QueryType(o.Catalog.Type), o.CatalogId);

		protected Resource CurrentResource
		{
			get
			{
				if (_resource == null)
				{
					var id = Guid.Parse(Request.QueryString["Id"]);
					_resource = HomoryContext.Value.Resource.Single(o => o.Id == id);
				}
				return _resource;
			}
		}

		private User _user;

		protected User TargetUser
		{
			get
			{
				if (_user == null)
				{
					_user = CurrentResource.User;
				}
				return _user;
			}
		}

	}
}
