using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Aspose.Words.Lists;
using Homory.Model;
using Telerik.Web.UI;
using Resource = Homory.Model.Resource;
using ResourceType = Homory.Model.ResourceType;
using System.Dynamic;

namespace Go
{
    public partial class GoCatalog : HomoryResourcePage
    {
        public class S
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int Month { get; set; }
            public int Article { get; set; }
            public int Video { get; set; }
            public int CoursewarePaper { get; set; }
            public int Activity { get; set; }
        }

        private List<ViewQueryResource> _query;

        protected List<ViewQueryResource> query
        {
            get
            {
                if (_query == null)
                {
                    int year = DateTime.Today.Year;
                    _query = HomoryContext.Value.ViewQueryResource.Where(o => o.年份 == year).ToList();

                }
                return _query;
            }
        }

        private List<Department> _campus;

        protected List<Department> campus
        {
            get
            {
                if (_campus == null)
                {
                    _campus = HomoryContext.Value.Department.Where(o => o.State < State.审核 && o.Id == o.TopId).ToList();

                }
                return _campus;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var g = query.GroupBy(o => o.CampusId).Select(o => new { Id = o.Key, Count = o.Sum(p => p.发布文章 + p.发布视频 + p.发布试卷 + p.发布课件 + p.评定资源 + p.评论资源 + p.回复评论) });
                var s = g.OrderByDescending(x => x.Count).Select(o => o.Id).ToList().Join(campus, y => y, z => z.Id, (y, z) => z).ToList();
                repeater.DataSource = s;
                repeater.DataBind();
            }
        }

        protected override bool ShouldOnline
        {
            get { return false; }
        }

        protected void repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var c = e.Item.FindControl("RadarAreaChart") as RadHtmlChart;
            Department d = e.Item.DataItem as Department;
            int year = DateTime.Today.Year;
            Guid campusId = d.Id;
            List<S> list = new List<S>();
            c.ChartTitle.Text = d.Name;
            c.PlotArea.Series.Clear();
            for (var i = 1; i < 13; i++)
            {
                S s = new S { Id = campusId, Name = d.Name, Month = i };
                s.Article = query.Count(o => o.CampusId == campusId && o.年份 == year && o.月份 == i) > 0 ? query.Where(o => o.CampusId == campusId && o.年份 == year && o.月份 == i).Sum(o => o.发布文章) : 0;
                s.Video = query.Count(o => o.CampusId == campusId && o.年份 == year && o.月份 == i) > 0 ? query.Where(o => o.CampusId == campusId && o.年份 == year && o.月份 == i).Sum(o => o.发布视频) : 0;
                s.CoursewarePaper = query.Count(o => o.CampusId == campusId && o.年份 == year && o.月份 == i) > 0 ? query.Where(o => o.CampusId == campusId && o.年份 == year && o.月份 == i).Sum(o => o.发布课件 + o.发布试卷) : 0;
                s.Activity = query.Count(o => o.CampusId == campusId && o.年份 == year && o.月份 == i) > 0 ? query.Where(o => o.CampusId == campusId && o.年份 == year && o.月份 == i).Sum(o => o.评论资源 + o.评定资源 + o.回复评论) : 0;
                list.Add(s);
            }
            BuildChart(c, "发布视频", 0xf4, 0x52, 0x62, list.Select(o => o.Video).ToArray());
            BuildChart(c, "发布文章", 0xf2, 0xe1, 0x23, list.Select(o => o.Article).ToArray());
            BuildChart(c, "发布课件", 0x2d, 0xc1, 0xec, list.Select(o => o.CoursewarePaper).ToArray());
            BuildChart(c, "资源互动", 0x8b, 0xe2, 0x64, list.Select(o => o.Activity).ToArray());
        }

        protected void BuildChart(RadHtmlChart chart, string name, int r, int g, int b, int[] values)
        {
            RadarColumnSeries item = new RadarColumnSeries();
            item.Name = name;
            item.Stacked = true;
            item.Appearance.FillStyle.BackgroundColor = System.Drawing.Color.FromArgb(r, g, b);
            item.LabelsAppearance.Visible = false;
            foreach (var value in values)
                item.SeriesItems.Add(new CategorySeriesItem(value));
            chart.PlotArea.Series.Add(item);
        }
    }
}
