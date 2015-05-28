using System;
using System.Dynamic;
using System.Linq;
using System.Web;
using EntityFramework.Extensions;
using Homory.Model;
using System.Net;

namespace Go
{
	public partial class GoSignOff : HomoryPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				UserSignOff(HomoryContext.Value);
				//foreach (var app in HomoryContext.Value.Application.Where(o => o.Type == ApplicationType.平台 && o.State == State.启用).ToList())
				//{
				//	if (!string.IsNullOrWhiteSpace(app.Quit))
				//	{
				//		try
				//		{
				//			WebRequest request = WebRequest.Create(app.Quit);
				//			var resp = request.GetResponse();
				//		}
				//		catch
				//		{
				//		}
				//	}
				//}
				var query = Request.QueryString["SsoRedirect"];
				if (string.IsNullOrWhiteSpace(query))
				{
					Response.Redirect("SsoOn".FromHomoryConfig(), false);
				}
				else
				{
					string url;
					if (query.IndexOf('&') <= 0)
						url = Server.UrlDecode(query);
					else
					{
						var index = query.IndexOf('&');
						var path = Server.UrlDecode(Request.QueryString["SsoRedirect"]);
                        url = string.Format("{0}?{1}", path, query.Substring(index + 1));
                    }
					Response.Redirect(string.IsNullOrWhiteSpace(url) ? "SsoOn".FromHomoryConfig() : url, false);
				}
			}
		}

		public bool UserSignOff(Entities db)
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
					if (httpCookie != null) oid = httpCookie.Value;
				}
				if (string.IsNullOrEmpty(oid))
				{
					return true;
				}
				var onlineGuid = Guid.Parse(oid);
				db.UserOnline.Where(o => o.Id == onlineGuid).Delete();
				db.SaveChanges();
				Session.Remove(HomoryConstant.SessionOnlineId);
				if (Request.Cookies.AllKeys.Contains(HomoryConstant.CookieOnlineId))
				{
					var cookie = Request.Cookies[HomoryConstant.CookieOnlineId];
					if (cookie != null)
					{
						cookie.Expires = DateTime.Now.AddSeconds(-1);
						HttpContext.Current.Response.SetCookie(cookie);
					}
				}
                return true;
			}
            catch
            {
                return false;
            }
		}
	}
}
