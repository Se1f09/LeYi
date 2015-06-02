using System;
using System.Linq;
using Homory.Model;
using System.IO;

namespace Go
{
	public partial class GoBoard : HomoryPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				if (!GetOnlineUser(out _user))
				{
					Response.Redirect(Application["Sso"] + "Go/SignOn", false);
					return;
				}
				else
				{
					LoadInit();
				}
			}
		}

        protected string P(object icon)
        {
            var url = icon.ToString();
            if (!url.Equals("~/Common/默认/用户.png") && !url.Equals("~/Common/默认/群组.png") && File.Exists(Server.MapPath(url)))
            {
                return url;
            }
            else
            {
                var files = new DirectoryInfo(Server.MapPath("~/Common/头像/随机")).GetFiles();
                var r = new Random(Guid.NewGuid().GetHashCode());
                return "~/Common/头像/随机/" + files[r.Next(0, files.Length)].Name;
            }
        }

        private User _user;

		protected User InitUser
		{
			get
			{
				if (_user == null)
				{
					GetOnlineUser(out _user);
				}
				return _user;
			}
		}

		private void LoadInit()
		{
			LoadUser();
		}

		private void LoadUser()
		{
			icon.Visible = !string.IsNullOrWhiteSpace(InitUser.Icon);
            icon.ImageUrl = P(InitUser.Icon);
			headInfo.InnerText = string.Format("{0} ——乐翼教育云平台控制面板", InitUser.DisplayName);
			LoadSite(InitUser.Type);
		}

		public bool GetOnlineUser(out User user)
		{
			try
			{
				var oid = string.Empty;
				if (Session[HomoryConstant.SessionOnlineId] != null)
				{
					oid = Session[HomoryConstant.SessionOnlineId].ToString();
				}
				else if (Request.Cookies.AllKeys.Contains(HomoryConstant.CookieOnlineId))
				{
					var httpCookie = Request.Cookies[HomoryConstant.CookieOnlineId];
					if (httpCookie != null)
					{
						var value = httpCookie.Value;
						Session[HomoryConstant.SessionOnlineId] = Guid.Parse(value);
						oid = value;
					}
				}
				if (string.IsNullOrEmpty(oid))
				{
					user = null;
					return false;
				}
				var onlineGuid = Guid.Parse(oid);
				var online = HomoryContext.Value.UserOnline.SingleOrDefault(o => o.Id == onlineGuid);
				if (online == null)
				{
					user = null;
					return false;
				}
				user = HomoryContext.Value.User.SingleOrDefault(o => o.Id == online.UserId && o.State < State.审核);
				return user != null;
			}
			catch
			{
				user = null;
				return false;
			}
		}

		private void LoadSite(UserType userType)
		{
			if (userType == UserType.内置)
			{
				var sitesX =
				 HomoryContext.Value.Application.Where(o => o.Type == ApplicationType.平台 && o.State < State.审核);
				items.DataSource = sitesX.OrderBy(o => o.Ordinal).ToList();
				items.DataBind();
				return;
			}
			var sites =
	HomoryContext.Value.ApplicationRole.Where(o => o.UserType == userType)
	 .ToList()
	 .Select(o => o.Application)
	 .ToList()
	 .Where(o => o.Type == ApplicationType.平台 && o.State < State.审核);
			items.DataSource = sites.OrderBy(o => o.Ordinal).ToList();
			items.DataBind();
		}
	}
}
