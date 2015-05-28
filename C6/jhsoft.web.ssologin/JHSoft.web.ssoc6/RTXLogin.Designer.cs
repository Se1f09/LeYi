using JHBase;
using JHSoft.Base;
using JHSoft.IDAL;
using System;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;

namespace JHSoft.web.ssoc6
{
	public class RTXLogin : JHSoft.Base.Page
	{
		public RTXLogin()
		{
		}

		private void InitializeComponent()
		{
		}

		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			base.KeyCtrl("JHRTX");
			if (this.Session["UserCode"] != null)
			{
				if ((new INIFile(Common.GetRTXIniPath(""))).ReadString("C6Message", "usec6userid") == "0")
				{
					DBOperator dBOperator = DBOperatorFactory.GetDBOperator();
					DataTable dataTable = dBOperator.ExecSQLReDataTable(string.Concat("select userid,rtxid from Users_RTX where userid='", this.Session["UserCode"].ToString(), "' "));
					if ((dataTable == null ? false : dataTable.Rows.Count > 0))
					{
						this.Session["UserCode"] = dataTable.Rows[0]["rtxid"].ToString();
					}
				}
				string absoluteUri = base.Request.Url.AbsoluteUri;
				absoluteUri = string.Concat(absoluteUri.Substring(0, absoluteUri.LastIndexOf("/") + 1), "SessionKeySvr.asp");
				string str = string.Concat("user=", this.Session["UserCode"].ToString());
				string page = (new JHSoft.web.ssoc6.HttpUtility()).GetPage(absoluteUri, str);
				base.Response.Write(page);
				base.Response.End();
			}
		}
	}
}