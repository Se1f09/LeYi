using System;
using System.Linq;
using Homory.Model;

namespace Control
{
    public partial class ControlPersonalActionCenter : HomoryResourceControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                actions.DataSource = HomoryContext.Value.Action.Where(o => (o.Type == ActionType.用户评分资源 || o.Type == ActionType.用户评论资源) && o.State == State.启用).OrderByDescending(o => o.Time).ToList();
                actions.DataBind();
            }
        }

        protected Func<string, ResourceCatalog, string> Combine = (a, o) => string.Format("{0}{1}、", a, o.Catalog.Name);

        protected override bool ShouldOnline
        {
            get { return true; }
        }
    }
}
