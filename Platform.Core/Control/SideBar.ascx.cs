using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Homory.Model;

namespace Control
{
	public partial class ControlSideBar : HomoryCoreControl
	{
		private List<Menu> _menus;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadInit();
			}
		}

		protected void LoadInit()
		{
			u.InnerText = string.Format("你好，{0}", CurrentUser.DisplayName);
			_menus =
				HomoryContext.Value.Menu.Where(o => o.ApplicationId == HomoryCoreConstant.ApplicationKey && o.State == State.启用)
					.ToList();
			var menuList = _menus.Where(o => o.ParentId == null)
				.OrderBy(o => o.Ordinal)
				.ToList();
			repeater.DataSource = menuList;
			repeater.DataBind();
		}

		protected string SubMenu(Menu menu)
		{
			var query = _menus.Where(o => o.ParentId == menu.Id).OrderBy(o => o.Ordinal).ToList();
			var sb = new StringBuilder();
			foreach (var item in query)
			{
				if (CurrentRights.Contains(item.RightName))
					sb.Append(string.Format("<a class=\"item\" href=\"{0}\">{1}<i class=\"icon\"></i></a>", item.Redirect.StartsWith("+") ? item.Redirect.Substring(1).FromHomoryConfig() : item.Redirect, item.Name));
            }
			return sb.ToString();
		}

        protected void SignOff()
        {
            var path = Request.Url.AbsoluteUri;
            if (path.IndexOf('?') > 0)
                path = path.Substring(0, path.IndexOf('?'));
            var query = Request.QueryString.ToString();
            var url = string.Format("{0}?SsoRedirect={1}", "SsoOff".FromHomoryConfig(), Server.UrlEncode("CoreHome".FromHomoryConfig()));
            Response.Redirect(url, false);
        }

        protected void qb_Click(object sender, EventArgs e)
        {
            Session.Clear();
            SignOff();
        }
    }
}
