using JHBase;
using JHSoft.Base;
using JHSoft.CustomQuery;
using JHSoft.Globalization;
using JHSoft.IDAL;
using JHSoft.Login;
using JHSoft.WorkFlat;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace JHSoft.web.ssoc6
{
	public class LoginByRTX : JHSoft.Base.Page
	{
		protected HtmlForm Form1;

		public LoginByRTX()
		{
		}

		private void CreateSession(string userID)
		{
			DataTable dataTable = (new JHSoft.Login.Roles()).SearchSession(userID);
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

		private string Decryptstr(string urlstr, out string username, out string uerpwd)
		{
			string str;
			string empty = string.Empty;
			username = "";
			uerpwd = "";
			string[] strArrays = urlstr.Split(new char[] { '=' });
			int num = 0;
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				if (strArrays[i].ToString().Length > 0)
				{
					if (num == 0)
					{
						username = strArrays[i].ToString();
						username = string.Concat(username, "==");
					}
					if (num == 1)
					{
						uerpwd = strArrays[i].ToString();
						uerpwd = string.Concat(uerpwd, "==");
					}
					if (num == 2)
					{
						empty = strArrays[i].ToString();
						empty = string.Concat(empty, "=");
					}
					num++;
				}
			}
			try
			{
				AES aE = new AES();
				byte[] numArray = Convert.FromBase64String(username);
				byte[] numArray1 = Convert.FromBase64String(uerpwd);
				byte[] numArray2 = Convert.FromBase64String(empty);
				Encoding @default = Encoding.Default;
				string str1 = DateTime.Now.ToString("yyyyMMdd");
				DateTime now = DateTime.Now;
				byte[] bytes = @default.GetBytes(string.Concat(str1, now.ToString("yyyyMMdd")));
				Encoding encoding = Encoding.Default;
				string str2 = DateTime.Now.ToString("yyyyMMdd");
				string str3 = DateTime.Now.ToString("yyyyMMdd");
				string str4 = DateTime.Now.ToString("yyyyMMdd");
				now = DateTime.Now;
				byte[] bytes1 = encoding.GetBytes(string.Concat(str2, str3, str4, now.ToString("yyyyMMdd")));
				aE.CreateKey(bytes1, bytes);
				byte[] numArray3 = aE.Decrypt(numArray);
				byte[] numArray4 = aE.Decrypt(numArray1);
				byte[] numArray5 = aE.Decrypt(numArray2);
				username = Encoding.Default.GetString(numArray3);
				uerpwd = Encoding.Default.GetString(numArray4);
				empty = Encoding.Default.GetString(numArray5);
				now = DateTime.Now;
				TimeSpan timeSpan = new TimeSpan(now.Ticks);
				now = Convert.ToDateTime(empty);
				TimeSpan timeSpan1 = new TimeSpan(now.Ticks);
				str = (timeSpan.Subtract(timeSpan1).Minutes <= 1 ? "Yes" : "TimeOut");
			}
			catch
			{
				str = "Error";
			}
			return str;
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
			base.KeyCtrl("JHRTX");
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
				string str1 = "";
				str1 = base.Request["username"].ToString();
				if ((new INIFile(Common.GetRTXIniPath(""))).ReadString("C6Message", "usec6userid") == "0")
				{
					dataTable = dBOperator.ExecSQLReDataTable(string.Concat("select userid,rtxid from Users_RTX where rtxid='", str1, "' "));
					if ((dataTable == null ? false : dataTable.Rows.Count > 0))
					{
						str1 = dataTable.Rows[0]["userid"].ToString();
					}
				}
				if (base.Request.QueryString.ToString() != "")
				{
					string str2 = "Yes";
					if (!(str2 == "Yes"))
					{
						if (str2 == "TimeOut")
						{
							MessageBox.ShowErr("时间超时请重新登陆。", this.Page);
						}
						if (str2 == "Error")
						{
							MessageBox.ShowErr("输入参数有误,请重新输入。", this.Page);
						}
					}
					else
					{
						JHSoft.Login.Roles role = new JHSoft.Login.Roles();
						string str3 = str1.Trim();
						if (str1 == null)
						{
							MessageBox.ShowErr("用户名和密码出错，请重新登陆。", this.Page);
							return;
						}
						this.Session["UserLoginCode"] = str3;
						Localization.SetCulture("zh-cn", this);
						if (!base.Request.Browser.Cookies)
						{
							MessageBox.ShowErr("系统需要使用cookie。请打开浏览器的cookie选项。", this.Page);
						}
						else
						{
							this.Session["UserCode"] = "";
							PassWord passWord = new PassWord();
							string str4 = this.Session.SessionID.ToString();
							string empty = string.Empty;
							string empty1 = string.Empty;
							if (!passWord.IsSqlConnect())
							{
								MessageBox.ShowErr("数据库连接有问题，请检查。", this.Page);
							}
							else
							{
								empty = role.GetLogin(str1);
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
										string str5 = empty1;
										if (str5 != null)
										{
											if (str5 == "PassNew")
											{
												MessageBox.ShowErr("密码已到期，请修改密码。", this.Page);
											}
											else
											{
                                                if (str5 != "ERROR")
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
									else if (!role.IsAllowLogin(str1, base.Request.ServerVariables["REMOTE_ADDR"]))
									{
										MessageBox.ShowErr("您的IP已被系统限制。请重新登陆。", this.Page);
									}
									else if (!role.GetEpass(str1))
									{
										MessageBox.ShowErr("您是EPass用户，请选择EPass登陆方式，或者让管理员通过Epass失效操作来还原为正常登陆方式。", this.Page);
									}
									else if (!role.InsertLogin(str1, str4))
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
										HttpCookie httpCookie1 = new HttpCookie(string.Concat("SessionID", str1))
										{
											Value = str4,
											Expires = DateTime.MaxValue
										};
										base.Response.Cookies.Add(httpCookie1);
										this.CreateSession(str1);
										if (this.Context.Request["ReturnUrl"] == null)
										{
											Random random = new Random();
											string str6 = string.Concat("win", str1, random.Next(1000) + 1);
											MessageBox.OpenWindowInNewPage(this.Page, "../JHSoft.Web.WorkFlat/Index.aspx", str6);
										}
										else
										{
											base.Response.Redirect(FormsAuthentication.GetRedirectUrl(base.Request.Params["UserID"], false));
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
}