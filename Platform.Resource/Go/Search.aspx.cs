using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using Aspose.Words.Lists;
using Homory.Model;
using Telerik.Web.UI;
using System.Web.UI.WebControls;

namespace Go
{
    public partial class GoSearch : HomoryResourcePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                search_content.Value = Request.QueryString["Content"];
                search_go_OnServerClick(null, null);
            }
        }

        protected List<Homory.Model.Resource> LoadDataSource()
        {
            var content = search_content.Value.Trim();
            var source = HomoryContext.Value.Resource.Where(o => o.State < State.审核).ToList();
            var final = source.Where(o => o.Title.Contains(content) || o.ResourceTag.Count(ox => ox.Tag == content) > 0).ToList();
            return final;
        }

        protected void search_go_OnServerClick(object sender, EventArgs e)
        {
            var source = LoadDataSource();
            result.DataSource = source;
            result.DataBind();
            total.InnerText = source.Count.ToString();
            var list = new List<ResourceCatalog>();
            var filter = source.Aggregate(list, (catalogs, resource) =>
            {
                catalogs.AddRange(resource.ResourceCatalog);
                return catalogs;
            }).Where(o => o.State == State.启用).Select(o => o.CatalogId).Distinct().ToList().Join(HomoryContext.Value.Catalog, o => o, o => o.Id, (o1, o2) => o2).Where(o => o.State < State.审核).OrderBy(o => o.State).ThenBy(o => o.Ordinal).ToList();
            course.DataSource = filter.Where(o => o.Type == CatalogType.课程).ToList();
            course.DataBind();
            grade.DataSource = filter.Where(o => o.Type == CatalogType.年级_幼儿园 || o.Type == CatalogType.年级_六年制 || o.Type == CatalogType.年级_九年制).ToList();
            grade.DataBind();
            catalog.DataSource = filter.Where(o => o.Type == CatalogType.文章 || o.Type == CatalogType.视频).ToList();
            catalog.DataBind();
            t1.Checked = true;
            t2.Checked = true;
            t3.Checked = true;
            t4.Checked = true;
        }

        protected override bool ShouldOnline
        {
            get { return false; }
        }

        protected void item1_OnClick(object sender, EventArgs e)
        {
            var button = ((RadButton)sender);
            var list = course.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single());
            foreach (var btn in list)
            {
                btn.Checked = btn.Value == button.Value;
            }
            B();
        }

        protected void item2_OnClick(object sender, EventArgs e)
        {
            var button = ((RadButton)sender);
            var list = grade.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single());
            foreach (var btn in list)
            {
                btn.Checked = btn.Value == button.Value;
            }
            B();
        }

        protected void item3_OnClick(object sender, EventArgs e)
        {
            var button = ((RadButton)sender);
            var list = catalog.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single());
            foreach (var btn in list)
            {
                btn.Checked = btn.Value == button.Value;
            }
            B();
        }

        protected void item_OnClick(object sender, EventArgs e)
        {
            var button = ((RadButton)sender);
            var list = new RadButton[] { t1, t2, t3, t4 };
            foreach (var btn in list)
            {
                btn.Checked = btn.Value == button.Value;
            }
            B();
        }

        protected void itemX_OnClick(object sender, EventArgs e)
        {
            var button = ((RadButton)sender);
            var list = new RadButton[] { s1, s2, s3 };
            foreach (var btn in list)
            {
                btn.Checked = btn.Value == button.Value;
            }
            B();
        }

        private void B()
        {
            var source = LoadDataSource();
            Guid id;
            if (course.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single()).Count(o => o.Checked) == 1)
            {
                id = Guid.Parse(course.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single()).Single(o => o.Checked).Value);
                source = source.Where(o => o.ResourceCatalog.Count(p => p.CatalogId == id) > 0).ToList();
            }
            if (grade.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single()).Count(o => o.Checked) == 1)
            {
                id = Guid.Parse(grade.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single()).Single(o => o.Checked).Value);
                source = source.Where(o => o.ResourceCatalog.Count(p => p.CatalogId == id) > 0).ToList();
            }
            if (catalog.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single()).Count(o => o.Checked) == 1)
            {
                id = Guid.Parse(catalog.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single()).Single(o => o.Checked).Value);
                source = source.Where(o => o.ResourceCatalog.Count(p => p.CatalogId == id) > 0).ToList();
            }
            if (new RadButton[] { t1, t2, t3, t4 }.Count(o => o.Checked) == 1)
            {
                Homory.Model.ResourceType tt = (Homory.Model.ResourceType)int.Parse(new RadButton[] { t1, t2, t3, t4 }.Single(o => o.Checked).Value);
                source = source.Where(o => o.Type == tt).ToList();
            }
            if (s1.Checked)
                result.DataSource = source.OrderByDescending(o => o.Time);
            if (s2.Checked)
                result.DataSource = source.OrderByDescending(o => o.View);
            if (s3.Checked)
                result.DataSource = source.OrderByDescending(o => o.Grade);
            total.InnerText = source.Count.ToString();
        }

        protected void result_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            B();
        }
    }
}
