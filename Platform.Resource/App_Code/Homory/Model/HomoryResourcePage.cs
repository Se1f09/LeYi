﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace Homory.Model
{
	public abstract class HomoryResourcePage : HomoryPage
	{
		protected bool IsOnline
		{
			get { return Session[HomoryResourceConstant.SessionUserId] != null; }
		}

        protected void LogOp(ResourceLogType type, int? value = null)
        {
            HomoryContext.Value.LogOp(CurrentUser.Id, CurrentCampus.Id, type, value);
        }

        protected Resource R(object id)
        {
            var gid = Guid.Parse(id.ToString());
            return HomoryContext.Value.Resource.Single(o => o.Id == gid);
        }

        protected User U(object id)
        {
            var gid = Guid.Parse(id.ToString());
            return HomoryContext.Value.User.Single(o => o.Id == gid);
        }

        protected string UC(object id)
        {
            var gid = Guid.Parse(id.ToString());
            var user = HomoryContext.Value.User.Single(o => o.Id == gid);
            return user.DepartmentUser.Count(o => o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师) > 0 ? "[" + user.DepartmentUser.First(o => o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师).TopDepartment.Name + "]" : "";
        }

        protected User CurrentUser
		{
			get
			{
				var id = (Guid) Session[HomoryResourceConstant.SessionUserId];
				return HomoryContext.Value.User.Single(o => o.Id == id);
			}
		}

		private Department _campus;

        protected Department CurrentCampus
        {
            get
            {
                if (_campus != null) return _campus;
                var department =
                    CurrentUser.DepartmentUser.FirstOrDefault(o => (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师) && o.State == State.启用);
                if (department == null) return null;
                _campus = department.TopDepartment;
                return _campus;
            }
        }

        private bool? _isMaster;

		protected bool IsMaster
		{
			get
			{
				if (!_isMaster.HasValue)
				{
					_isMaster = CurrentUser != null &&
					            CurrentUser.DepartmentUser.Count(o => o.Type == DepartmentUserType.班级班主任 && o.State == State.启用) > 0;
				}
				return _isMaster.Value;
			}
		}

		protected void SignOn()
		{
			var path = Request.Url.AbsoluteUri;
			if (path.IndexOf('?') > 0)
				path = path.Substring(0, path.IndexOf('?'));
			var query = Request.QueryString.ToString();
			var url = string.Format("{0}?SsoRedirect={1}{2}{3}", "SsoOn".FromHomoryConfig(), Server.UrlEncode(path),
				string.IsNullOrWhiteSpace(query) ? string.Empty : "&", query);
			Response.Redirect(url, false);
		}

		protected void SignOff()
		{
			var path = Request.Url.AbsoluteUri;
			if (path.IndexOf('?') > 0)
				path = path.Substring(0, path.IndexOf('?'));
			var query = Request.QueryString.ToString();
			var url = string.Format("{0}?SsoRedirect={1}", "SsoOff".FromHomoryConfig(), Server.UrlEncode("ResourceHome".FromHomoryConfig()));
			Response.Redirect(url, false);
		}

		protected void TrySignOn()
		{
			if (Session["RESOURCE"] == null && !Request.QueryString.AllKeys.Contains("OnlineId"))
			{
				Session["RESOURCE"] = "RESOURCE";
				var path = Request.Url.AbsoluteUri;
				if (path.IndexOf('?') > 0)
					path = path.Substring(0, path.IndexOf('?'));
				var query = Request.QueryString.ToString();
				var url = string.Format("{0}?SsoRedirect={1}{2}{3}", "SsoApi".FromHomoryConfig(), Server.UrlEncode(path),
					string.IsNullOrWhiteSpace(query) ? string.Empty : "&", query);
				Response.Redirect(url, false);
			}
		}

		protected abstract bool ShouldOnline { get; }

		protected override void OnLoad(EventArgs e)
		{
            var doc = XDocument.Load(Server.MapPath("../Common/配置/Title.xml"));
            this.Title = doc.Root.Element("Resource").Value;

            if (IsOnline)
			{
				if (Session["RESOURCE"] == null)
					Session["RESOURCE"] = "RESOURCE";
				base.OnLoad(e);
				return;
			}

            if (Session["RESOURCE"] == null && !Request.QueryString.AllKeys.Contains("OnlineId"))
			{
				TrySignOn();
				return;
			}

			if (Request.QueryString.AllKeys.Contains("OnlineId"))
			{
				if (!string.IsNullOrWhiteSpace(Request.QueryString["OnlineId"]))
				{
					var id = Guid.Parse(Request.QueryString["OnlineId"]);
					if (HomoryContext.Value.UserOnline.Count(o => o.Id == id) == 0)
					{
						SignOff();
						return;
					}
					Session[HomoryResourceConstant.SessionUserId] = HomoryContext.Value.UserOnline.Single(o => o.Id == id).UserId;
                    Session["RESOURCE"] = "RESOURCE";
                    base.OnLoad(e);
					return;
				}
			}

			if (!IsOnline && ShouldOnline)
			{
				SignOn();
				return;
			}

			base.OnLoad(e);
		}
	}
}
