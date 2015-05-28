using System;
using System.Linq;
using Homory.Model;

namespace Control
{
	public partial class ControlHomeGroup : HomoryResourceControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				BindStudio();
			}
		}

		public void BindStudio()
		{
			group.DataSource = HomoryContext.Value.Group.Where(o => o.State < State.审核 && o.Type == GroupType.教研团队).Take(5).ToList();
			group.DataBind();
		}

		protected override bool ShouldOnline
		{
			get { return false; }
		}
	}
}
