using System;
using System.Dynamic;
using System.Linq;
using System.Web;
using Homory.Model;

namespace Go
{
	public partial class GoVerifying : HomoryPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString.Count == 0)
			{
				Response.Redirect("SsoOn".FromHomoryConfig(), false);
				return;
			}
			try
			{
				var stamp = Guid.Parse(Request.QueryString[0]);
				var output = UserVerifying(HomoryContext.Value, stamp);
				if (output.Ok)
				{
					Response.Redirect("SsoBoard".FromHomoryConfig(), false);
					return;
				}
				Response.Redirect("SsoOn".FromHomoryConfig(), false);
			}
			catch
			{
				Response.Redirect("SsoOn".FromHomoryConfig(), false);
			}
		}

		public dynamic UserVerifying(Entities db, Guid stamp)
		{
			dynamic output = new ExpandoObject();
			output.Ok = false;
			output.Data = new ExpandoObject();
			try
			{
				var user = db.User.SingleOrDefault(o => o.Stamp == stamp && o.State < State.删除);
				if (user == null)
				{
					return output;
				}
				user.Stamp = Guid.NewGuid();
				user.State = State.启用;
				db.SaveChanges();
				var online = db.UserOnline.SingleOrDefault(o => o.UserId == user.Id);
				if (online == null)
				{
					online = new UserOnline
					{
						Id = db.GetId(),
						UserId = user.Id,
						TimeStamp = DateTime.Now
					};
					db.UserOnline.Add(online);
				}
				else
				{
					online.TimeStamp = DateTime.Now;
				}
				db.SaveChanges();
				var cookie = new HttpCookie(HomoryConstant.CookieOnlineId, online.Id.ToString().ToUpper());
				var expire = int.Parse(db.ApplicationPolicy.Single(o => o.Name == "UserCookieExpire" && o.ApplicationId == Guid.Empty).Value);
				cookie.Expires = DateTime.Now.AddMinutes(expire);
				Response.SetCookie(cookie);
				Session[HomoryConstant.SessionOnlineId] = online.Id;
				output.Ok = true;
				return output;
			}
			catch
			{
				return output;
			}
		}
	}
}
