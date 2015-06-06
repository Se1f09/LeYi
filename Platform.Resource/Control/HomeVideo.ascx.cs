using System;
using System.Linq;
using Homory.Model;
using System.Collections.Generic;

namespace Control
{
	public partial class ControlHomeGroupVideo : HomoryResourceControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				BindVideo();
			}
		}

		public void BindVideo()
		{
			video_latest.DataSource =
				HomoryContext.Value.Resource.Where(o => o.State < State.审核 && o.Type == ResourceType.视频)
					.OrderByDescending(o => o.Time)
					.Take(4)
					.ToList();
			video_latest.DataBind();
			video_popular.DataSource =
				HomoryContext.Value.Resource.Where(o => o.State < State.审核 && o.Type == ResourceType.视频)
					.OrderByDescending(o => o.View)
					.Take(4)
					.ToList();
			video_popular.DataBind();


            var list = new List<Resource>();
            if (IsOnline)
            {
                var __courses = CurrentUser.Taught.Where(o => o.State < State.审核).Select(o => o.CourseId).ToList();
                foreach (var __c in __courses)
                {
                    list.AddRange(HomoryContext.Value.Resource.Where(o => o.State == State.启用 && o.UserId == CurrentUser.Id && o.ResourceCatalog.Count(p => p.CatalogId == __c && p.State < State.启用) > 0).Take(12).ToList());
                    if (list.Count >= 12)
                        break;
                }
            }

            list = list.OrderByDescending(o=>o.Time).ToList();

            if (list.Count < 12)
            {
                list.AddRange(HomoryContext.Value.Resource.Where(o => o.State < State.审核 && o.Type == ResourceType.视频)
                .OrderByDescending(o => o.Credit)
                .Take(12 - list.Count)
                .ToList());
            }

            video_random.DataSource = list.Take(4);
			video_random.DataBind();
            video_randomX.DataSource = list.Count > 4 ? list.Skip(4).Take(list.Count - 4) : list.Take(4);
			video_randomX.DataBind();

            video_cut.DataSource = HomoryContext.Value.ResourceComment.Where(o => o.State < State.审核 && o.Timed == true).OrderByDescending(o => o.Time).Select(o => o.Resource).Distinct().Take(4).ToList();
            video_cut.DataBind();
		}

        protected string FormatPeriod(Resource res)
        {
            var comment = res.ResourceComment.Where(o => o.Timed == true).OrderByDescending(o => o.Time).First();
            var s = comment.Start.Value.ToString().Split(new char[] { '.' })[0] + "秒";
            var e = " - " + (comment.End.HasValue ? comment.End.Value.ToString().Split(new char[] { '.' })[0] + "秒" : string.Empty);
            return string.Format("<a onclick=\"popup('{1}');\">{0}</a>", s + e, comment.Id);
        }

        protected string GetCatalogName(Resource resource)
        {
            if (resource.ResourceCatalog.Count(o => (o.Catalog.Type == CatalogType.年级_幼儿园 || o.Catalog.Type == CatalogType.年级_六年制 || o.Catalog.Type == CatalogType.年级_九年制 || o.Catalog.Type == CatalogType.年级_高中) && o.State < State.审核) > 0 && resource.ResourceCatalog.Count(o => o.Catalog.Type == CatalogType.课程 && o.State < State.审核) > 0)
            {
                return string.Format("{0} {1}",
                    resource.ResourceCatalog.First(o => (o.Catalog.Type == CatalogType.年级_幼儿园 || o.Catalog.Type == CatalogType.年级_六年制 || o.Catalog.Type == CatalogType.年级_九年制 || o.Catalog.Type == CatalogType.年级_高中) && o.State < State.审核).Catalog.Name,
                    resource.ResourceCatalog.First(o => o.Catalog.Type == CatalogType.课程 && o.State < State.审核).Catalog.Name);
            }
            return resource.ResourceCatalog.First(o => o.Catalog.Type == CatalogType.视频 && o.State < State.审核).Catalog.Name;
        }

		protected override bool ShouldOnline
		{
			get { return false; }
		}
	}
}
