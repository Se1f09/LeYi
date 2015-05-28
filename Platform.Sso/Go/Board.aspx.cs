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
					Response.Redirect("SsoOn".FromHomoryConfig(), false);
					return;
				}
				else
				{
					LoadInit();
				}
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
            if (File.Exists(Server.MapPath(InitUser.Icon)))
                icon.ImageUrl = InitUser.Icon;
            else
                icon.ImageUrl = "~/Common/默认/用户.png";
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
