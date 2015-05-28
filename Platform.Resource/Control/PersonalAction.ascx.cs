using System;
using System.Linq;
using Homory.Model;

namespace Control
{
    public partial class ControlPersonalAction : HomoryResourceControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                actions.DataSource = HomoryContext.Value.Action.Where(o => (o.Type == ActionType.用户评分资源 || o.Type == ActionType.用户评论资源 || o.Type == ActionType.用户回复评论) && o.State == State.启用).OrderByDescending(o => o.Time).Take(8).ToList();
                actions.DataBind();
            }
        }

        protected override bool ShouldOnline
        {
            get { return false; }
        }
    }
}
