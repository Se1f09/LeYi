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
using System.Text;

namespace Go
{
	public partial class GoViewVideoNone : HomoryResourcePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
			}
		}

		protected override bool ShouldOnline
		{
			get { return false; }
		}
    }
}
