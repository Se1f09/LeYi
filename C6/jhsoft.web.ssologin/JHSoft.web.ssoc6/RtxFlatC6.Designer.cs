using JHBase;
using JHSoft.Globalization;
using JHSoft.IDAL;
using JHSoft.Login;
using JHSoft.WorkFlat;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.Configuration;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace JHSoft.web.ssoc6
{
	public class RtxFlatC6 : System.Web.UI.Page
	{
		protected HtmlForm Form1;

		protected HtmlGenericControl frmContent;

		public RtxFlatC6()
		{
		}

		private void CreateSession(string userID)
		{
			DataTable dataTable = (new Roles()).SearchSession(userID);
			if ((dataTable == null ? false : dataTable.Rows.Count > 0))
			{
				DataRow[] dataRowArray = dataTable.Select("RelaPrimary=1");
				if ((int)dataRowArray.Length == 1)
				{
					this.Session["UserCode"] = dataRowArray[0]["UserID"].ToString();
					this.Session["UserName"] = dataRowArray[0]["UserName"].ToString();
					this.Session["DeptID"] = dataRowArray[0]["DeptID"].ToString();
					this.Session["GroupID"] = dataRowArray[0]["GroupID"].ToString();
					this.Session["strQuarters"] = dataRowArray[0]["StatID"].ToString();
				}
				string empty = string.Empty;
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					empty = string.Concat(empty, dataTable.Rows[i]["DeptID"].ToString(), ",");
				}
				if (empty.Length > 0)
				{
					empty = empty.Substring(0, empty.Length - 1);
				}
				this.Session["strDept"] = empty;
				PassWord passWord = new PassWord();
				HttpSessionState session = this.Session;
				string str = this.Session["UserCode"].ToString().Trim();
				DateTime now = DateTime.Now;
				session["SESSION_AIP"] = string.Concat(str, "_", now.ToString("yyyyMMddHHmmssff"));
				passWord.Aip_Insert(this.Session["UserCode"].ToString(), this.Session["SESSION_AIP"].ToString());
				if (!passWord.Aip_Insert(this.Session["UserCode"].ToString(), this.Session["SESSION_AIP"].ToString()))
				{
					this.Page.RegisterStartupScript("", "<script language='javascript'>alert('初始化手写批注没有成功，请您重新登录。');</script>");
				}
			}
			string pageStyleCss = (new WorkFlatDataClass()).GetPageStyleCss(userID);
			if (!(pageStyleCss != ""))
			{
				this.Session["CssPath"] = "";
			}
			else
			{
				this.Session["CssPath"] = pageStyleCss;
			}
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
			DataTable dataTable;
			string item = "";
			string str = base.Request.QueryString["auto"];
			int num = 0;
			DBOperator dBOperator = DBOperatorFactory.GetDBOperator();
			if (str == "e5wappec")
			{
				item = base.Request.QueryString["username"];
				string[] strArrays = new string[] { "Exec CheckUser '", item, "','", item, "ERYW'" };
				dataTable = dBOperator.ExecSQLReDataTable(string.Concat(strArrays));
				if (num != 1)
				{
					num = 0;
				}
				if ((dataTable.Rows.Count == 0 ? false : num != 1))
				{
					if ((dataTable.Rows[0][0].ToString() != "1" ? false : num != 1))
					{
						num = 1;
					}
				}
				if (num != 1)
				{
					base.Response.Redirect(string.Concat("../JHSoft.Web.Login/PassWord.aspx?tt=a", dataTable.Rows[0][0].ToString(), "a"), true);
				}
			}
			if (!this.Page.IsPostBack)
			{
				string userID = "";
				string str1 = base.Request["url"].ToString();
				userID = base.Request["username"].ToString();
				if ((new INIFile(Common.GetRTXIniPath(""))).ReadString("C6Message", "usec6userid") == "0")
				{
					dataTable = dBOperator.ExecSQLReDataTable(string.Concat("select userid,rtxid from Users_RTX where rtxid='", userID, "' "));
					if ((dataTable == null ? false : dataTable.Rows.Count > 0))
					{
						userID = dataTable.Rows[0]["userid"].ToString();
					}
				}
				if (base.Request.QueryString.ToString() != "")
				{
					if (this.Session["UserCode"] != null)
					{
						this.frmContent.Attributes["src"] = str1;
					}
					else
					{
						Roles role = new Roles();
						string str2 = userID.Trim();
						userID = role.GetUserID(str2);
						if (userID == null)
						{
							MessageBox.ShowErr("用户名和密码出错，请重新登陆。", this.Page);
							return;
						}
						this.Session["UserLoginCode"] = str2;
						Localization.SetCulture("zh-cn", this);
						if (!base.Request.Browser.Cookies)
						{
							MessageBox.ShowErr("系统需要使用cookie。请打开浏览器的cookie选项。", this.Page);
						}
						else
						{
							this.Session["UserCode"] = "";
							PassWord passWord = new PassWord();
							string str3 = this.Session.SessionID.ToString();
							string empty = string.Empty;
							string empty1 = string.Empty;
							if (!passWord.IsSqlConnect())
							{
								MessageBox.ShowErr("数据库连接有问题，请检查。", this.Page);
							}
							else
							{
								empty = role.GetLogin(userID);
								if (!(empty == "OK"))
								{
									if (empty == "Empty")
									{
										MessageBox.ShowErr("此用户不存在，请更换用户重新登陆。", this.Page);
									}
									if (empty == "Delete")
									{
										MessageBox.ShowErr("此用户已被删除，请更换用户重新登陆。", this.Page);
									}
									if (empty == "Leaver")
									{
										MessageBox.ShowErr("用户已离职。请更换用户重新登陆。", this.Page);
									}
									if (empty == "Padlock")
									{
										MessageBox.ShowErr("用户已被禁止登陆。请更换用户重新登陆。", this.Page);
									}
								}
								else
								{
									empty1 = empty;
									if (!(empty1 == "OK"))
									{
										string str4 = empty1;
										if (str4 != null)
										{
											if (str4 == "PassNew")
											{
												MessageBox.ShowErr("密码已到期，请修改密码。", this.Page);
											}
											else
											{
                                                if (str4 != "ERROR")
                                                {
                                                    MessageBox.ShowErr(string.Concat("密码最小长度为", empty1.ToString(), "位，请修改密码。"), this.Page);
                                                }
                                                else
                                                {
                                                    MessageBox.ShowErr("用户名和密码出错，请重新登陆。", this.Page);
                                                }
											}
										}
									}
									else if (!role.IsAllowLogin(userID, base.Request.ServerVariables["REMOTE_ADDR"]))
									{
										MessageBox.ShowErr("您的IP已被系统限制。请重新登陆。", this.Page);
									}
									else if (!role.GetEpass(userID))
									{
										MessageBox.ShowErr("您是EPass用户，请选择EPass登陆方式，或者让管理员通过Epass失效操作来还原为正常登陆方式。", this.Page);
									}
									else if (!role.InsertLogin(userID, str3))
									{
										MessageBox.ShowErr("用户添加登陆日志错误。请重新登陆。", this.Page);
									}
									else
									{
										HttpCookie httpCookie = new HttpCookie("Login")
										{
											Value = "PassWord",
											Expires = DateTime.MaxValue
										};
										base.Response.Cookies.Add(httpCookie);
										HttpCookie httpCookie1 = new HttpCookie(string.Concat("SessionID", userID))
										{
											Value = str3,
											Expires = DateTime.MaxValue
										};
										base.Response.Cookies.Add(httpCookie1);
										this.CreateSession(userID);
										this.frmContent.Attributes["src"] = str1;
									}
								}
							}
						}
					}
				}
			}
		}
	}
}