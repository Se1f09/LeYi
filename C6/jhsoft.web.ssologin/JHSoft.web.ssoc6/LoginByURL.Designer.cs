using JHSoft.CustomQuery;
using JHSoft.Globalization;
using JHSoft.Login;
using JHSoft.OS;
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
	public class LoginByURL : System.Web.UI.Page
	{
		protected HtmlForm Form1;

		public LoginByURL()
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
			CRegister cRegister = new CRegister();
			if (cRegister.VerifyInfo() == 1)
			{
				MessageBox.ShowErr(cRegister.MessageInformation(), this.Page);
				this.Page.RegisterStartupScript("closepage", "<script language='javascript'>window.close();</script>");
			}
			else if (!this.Page.IsPostBack)
			{
				string userID = "";
				string str = "";
				string str1 = "";
				if (base.Request.QueryString.ToString() != "")
				{
					string str2 = base.Request.Url.ToString();
					int num = str2.LastIndexOf("?");
					str2 = str2.Substring(num + 1, str2.Length - num - 1);
					string str3 = this.Decryptstr(str2, out str1, out str);
					if (!(str3 == "Yes"))
					{
						if (str3 == "TimeOut")
						{
							MessageBox.ShowErr("时间超时请重新登陆。", this.Page);
						}
						if (str3 == "Error")
						{
							MessageBox.ShowErr("输入参数有误,请重新输入。", this.Page);
						}
					}
					else
					{
						JHSoft.Login.Roles role = new JHSoft.Login.Roles();
						string str4 = str1.Trim();
						userID = role.GetUserID(str4);
						if (userID == null)
						{
							MessageBox.ShowErr("用户名和密码出错，请重新登陆。", this.Page);
							return;
						}
						this.Session["UserLoginCode"] = str4;
						Localization.SetCulture("zh-cn", this);
						if (!base.Request.Browser.Cookies)
						{
							MessageBox.ShowErr("系统需要使用cookie。请打开浏览器的cookie选项。", this.Page);
						}
						else
						{
							this.Session["UserCode"] = "";
							PassWord passWord = new PassWord();
							string str5 = this.Session.SessionID.ToString();
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
									empty1 = passWord.IsPassword(userID, str);
									if (!(empty1 == "OK"))
									{
										string str6 = empty1;
										if (str6 != null)
										{
											if (str6 == "PassNew")
											{
												MessageBox.ShowErr("密码已到期，请修改密码。", this.Page);
											}
											else
											{
                                                if (str6 != "ERROR")
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
									else if (!role.InsertLogin(userID, str5))
									{
										MessageBox.ShowErr("用户添加登陆日志错误。请重新登陆。", this.Page);
									}
									else
									{
										string roles = role.GetRoles(userID);
										DateTime now = DateTime.Now;
										DateTime dateTime = DateTime.Now;
										FormsAuthenticationTicket formsAuthenticationTicket = new FormsAuthenticationTicket(1, userID, now, dateTime.AddMinutes(600), false, roles, "/");
										string str7 = FormsAuthentication.Encrypt(formsAuthenticationTicket);
										HttpCookie httpCookie = new HttpCookie(FormsAuthentication.FormsCookieName, str7);
										this.Context.Response.Cookies.Add(httpCookie);
										HttpCookie httpCookie1 = new HttpCookie("Login")
										{
											Value = "PassWord",
											Expires = DateTime.MaxValue
										};
										base.Response.Cookies.Add(httpCookie1);
										HttpCookie httpCookie2 = new HttpCookie(string.Concat("SessionID", userID))
										{
											Value = str5,
											Expires = DateTime.MaxValue
										};
										base.Response.Cookies.Add(httpCookie2);
										this.CreateSession(userID);
										if (this.Context.Request["ReturnUrl"] == null)
										{
											Random random = new Random();
											string str8 = string.Concat("win", userID, random.Next(1000) + 1);
											MessageBox.OpenWindowInNewPage(this.Page, "../JHSoft.Web.WorkFlat/Index.aspx", str8);
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