using System;
using System.IO;
using System.Linq;
using System.ServiceModel.Activities;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI.HtmlControls;
using Homory.Model;

namespace Go
{
    public partial class GoViewPlain : System.Web.UI.Page
    {


        protected Lazy<Entities> HomoryContext = new Lazy<Entities>(() => new Entities());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var url = string.Format("../Document/web/PdfViewer.aspx?Id={0}&Random={1}", Request.QueryString["Id"],
                    Guid.NewGuid());
                publish_preview_pdf.Attributes["src"] = url;
                catalog.Visible = CurrentResource.Type == ResourceType.文章;
                var p =
                    TargetUser.Resource.Where(
                        o => o.State == State.启用 && o.Type == CurrentResource.Type && o.Time > CurrentResource.Time)
                        .OrderByDescending(o => o.Time).FirstOrDefault();
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
                case CatalogType.年级_高中:
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

        protected void publish_attachment_list_OnNeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
        {
            var resource = CurrentResource;
            var files = HomoryContext.Value.Resource.Single(o => o.Id == resource.Id).ResourceAttachment.OrderBy(o => o.Id).ToList();
            publish_attachment_list.DataSource = files;
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
