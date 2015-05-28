using JHSoft.Crypt;
using JHSoft.Globalization;
using JHSoft.IDAL;
using JHSoft.Login;
using JHSoft.OS;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Xml;

namespace jhsoft.web.ssologin
{
	public class loginByiYou : System.Web.UI.Page
	{
		private string Sign = "";

		private string Url = "";

		private string LoginCode = "";

		private string PWDEn = "";

		private string UserID = "";

		private string sessionid = "";

		public loginByiYou()
		{
		}

		protected void C6Check()
		{
			JHSoft.Login.Roles role = new JHSoft.Login.Roles();
			this.UserID = this.GetUserID(this.LoginCode);
			string item = base.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
			if ((item == null ? true : item == ""))
			{
				item = base.Request.ServerVariables["REMOTE_ADDR"];
			}
			string str = item;
			this.Session["UserCode"] = "";
			this.sessionid = this.Session.SessionID.ToString();
			if (!base.Request.Browser.Cookies)
			{
				this.Returns("用户登录登陆失败。系统需要使用cookie，请打开浏览器的cookie选项。");
			}
			string login = role.GetLogin(this.UserID);
			if (login == "Empty")
			{
				this.Returns("用户登录登陆失败，此用户不存在。");
			}
			if (login == "Delete")
			{
				this.Returns("用户登录登陆失败，此用户已被删除。");
			}
			if (login == "Leaver")
			{
				this.Returns("用户登录登陆失败，用户已离职。");
			}
			if (login == "Padlock")
			{
				this.Returns("用户登录登陆失败，用户已被禁止登陆。");
			}
			if (!role.IsAllowLogin(this.UserID, base.Request.ServerVariables["REMOTE_ADDR"]))
			{
				this.Returns("用户登录登陆失败。您的IP已被系统限制。请重新登陆。");
			}
			if (!role.InsertLogin(this.UserID, this.sessionid, item, str))
			{
				this.Returns("用户添加登陆日志错误，请重新登陆。");
			}
			if (login == "OK")
			{
				string[] loginCode = new string[] { "SELECT SystemUID FROM dbo.SystemAuthorize WHERE SystemUID='", this.LoginCode, "'  and  AuthorizeCode='", this.PWDEn, "'" };
				string str1 = string.Concat(loginCode);
				if (DBOperatorFactory.GetDBOperator().ExecSQLReDataTable(str1).Rows.Count != 0)
				{
					this.CreateSession(this.UserID);
				}
				else
				{
					this.Returns("登录失败，尚未进行授权绑定。");
				}
			}
		}

		public string CheckSign(string Sign, string url)
		{
			string str = url;
			string str1 = "Verify";
			string str2 = string.Concat("{\"guid\":\"", Sign, "\"}");
			string str3 = string.Format("{0}/{1}", str, str1);
			HttpWebRequest length = (HttpWebRequest)WebRequest.Create(str3);
			byte[] bytes = Encoding.GetEncoding("UTF-8").GetBytes(str2);
			length.Method = "POST";
			length.ContentLength = (long)((int)bytes.Length);
			length.ContentType = "application/json";
			length.MaximumAutomaticRedirections = 1;
			length.AllowAutoRedirect = true;
			Stream requestStream = length.GetRequestStream();
			requestStream.Write(bytes, 0, (int)bytes.Length);
			requestStream.Close();
			HttpWebResponse response = (HttpWebResponse)length.GetResponse();
			StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
			string end = streamReader.ReadToEnd();
			streamReader.Close();
			response.Close();
			return end;
		}

		protected void CheckSSOParameter()
		{
			CRegister cRegister = new CRegister();
			string empty = string.Empty;
			if (cRegister.VerifyInfo() == 1)
			{
				this.Returns(cRegister.MessageInformation());
			}
			this.Sign = string.Format("{0}", base.Request["sign"]).Trim();
			if (this.Sign == "")
			{
				this.Returns("SSO登陆失败。没有提供SSO验证信息。");
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(base.Server.MapPath("../JHSoft.Web.SSOC6/config.xml"));
			this.Url = xmlDocument.SelectSingleNode("//root//WebServiceURL").InnerXml.ToLower();
			if (this.Url == "")
			{
				this.Returns("当前C6系统中没有设置验证iYou的验证中心Url地址,请与系统管理员联系。");
			}
			try
			{
				empty = this.CheckSign(this.Sign, this.Url).Trim();
			}
			catch (Exception exception)
			{
				this.Returns(string.Concat("用户登录登陆失败。目前无法访问身证验证服务器。", exception.Message));
			}
			if ((empty == string.Empty ? false : empty.IndexOf(":") > -1))
			{
				char[] chrArray = new char[] { ':' };
				empty = empty.Split(chrArray)[1].Replace("\"", "").Replace("}", "").Trim();
				if (empty != string.Empty)
				{
					empty = AES.C6AESDecrypt(empty).Trim();
					if (empty.IndexOf("$") > -1)
					{
						chrArray = new char[] { '$' };
						this.LoginCode = empty.Split(chrArray)[0].Trim();
						chrArray = new char[] { '$' };
						this.PWDEn = empty.Split(chrArray)[1].Trim();
					}
				}
			}
			if (empty == "")
			{
				this.Returns("验证码无效，用户登录登陆失败。");
			}
		}

		private void CreateSession(string userID)
		{
			JHSoft.Login.Roles role = new JHSoft.Login.Roles();
			DataTable dataTable = role.SearchSession(userID);
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
					this.Session["IsAipUser"] = dataRowArray[0]["Reg_AipPower"].ToString();
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
					this.Returns("手写批注初始错误。");
				}
				string roles = role.GetRoles(this.UserID);
				string str1 = this.UserID;
				DateTime dateTime = DateTime.Now;
				now = DateTime.Now;
				FormsAuthenticationTicket formsAuthenticationTicket = new FormsAuthenticationTicket(1, str1, dateTime, now.AddMinutes(600), false, roles, "/");
				string str2 = FormsAuthentication.Encrypt(formsAuthenticationTicket);
				HttpCookie httpCookie = new HttpCookie(FormsAuthentication.FormsCookieName, str2);
				this.Context.Response.Cookies.Add(httpCookie);
				HttpCookie httpCookie1 = new HttpCookie("Login")
				{
					Value = "PassWord",
					Expires = DateTime.MaxValue
				};
				base.Response.Cookies.Add(httpCookie1);
				HttpCookie httpCookie2 = new HttpCookie(string.Concat("SessionID", this.UserID))
				{
					Value = this.sessionid,
					Expires = DateTime.MaxValue
				};
				base.Response.Cookies.Add(httpCookie2);
				base.Response.Write("<script language='JavaScript'>window.open('../JHSoft.Web.WorkFlat/Index.aspx','','resizable=yes,state=no');try{window.open('','_parent',''); window.opener=null;window.close();}catch(error){window.close();}</script>");
			}
		}

		public string GetUserID(string strLoginCode)
		{
			string str = "";
			string str1 = string.Format("select top 1 UserID from Users where LoginCode='{0}' and SysFlag=0", strLoginCode);
			DataTable dataTable = new DataTable();
			dataTable = DBOperatorFactory.GetDBOperator().ExecSQLReDataTable(str1);
			if (dataTable.Rows.Count <= 0)
			{
				str = null;
			}
			else
			{
				str = dataTable.Rows[0][0].ToString();
			}
			dataTable.Dispose();
			return str;
		}

		public string IsPassword(string userID, string pwd)
		{
			string empty = string.Empty;
			string errorMessage = string.Empty;
			string str = pwd;
			DBOperator dBOperator = DBOperatorFactory.GetDBOperator();
			string[] strArrays = new string[] { "select PassWordType,PWDLastUpdateTime,PassWordTerm,PassWordLeastLengh, DATEDIFF(day, DATEADD(day, passwordTerm, PWDLastUpdateTime), getdate()) as Span, LastLonginActive from Users where SysFlag=0 and UserID='", userID, "'and PassWord='", str, "' " };
			string str1 = string.Concat(strArrays);
			DataTable dataTable = new DataTable();
			dataTable = dBOperator.ExecSQLReDataTable(str1);
			if (dBOperator.IsError)
			{
				errorMessage = dBOperator.ErrorMessage;
			}
			if (dataTable.Rows.Count != 0)
			{
				if (dataTable.Rows[0]["PassWordType"].ToString() == "1")
				{
					empty = (Convert.ToInt32(dataTable.Rows[0]["Span"].ToString()) < 0 ? "OK" : "PassNew");
				}
				if ((dataTable.Rows[0]["PassWordType"].ToString() != "3" ? false : dataTable.Rows[0]["LastLonginActive"].ToString().Trim() != string.Empty))
				{
					empty = "OK";
				}
				if (dataTable.Rows[0]["PassWordType"].ToString() == "2")
				{
					empty = "OK";
				}
				if ((dataTable.Rows[0]["PassWordType"].ToString() == "" ? true : dataTable.Rows[0]["PassWordType"].ToString() == "0"))
				{
					empty = "OK";
				}
				if (empty == "OK")
				{
					if (Convert.ToInt32(dataTable.Rows[0]["PassWordLeastLengh"].ToString()) > pwd.Length)
					{
						empty = dataTable.Rows[0]["PassWordLeastLengh"].ToString();
					}
				}
				if (dataTable.Rows[0]["LastLonginActive"].ToString().Trim() == string.Empty)
				{
					empty = "FirstLogin";
				}
			}
			else
			{
				empty = "ERROR";
			}
			return empty;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			Localization.SetCulture("zh-cn", this);
			this.CheckSSOParameter();
			this.C6Check();
		}

		protected void Returns(string msg)
		{
			base.Response.Write(string.Concat("<script>alert('", msg, "');window.close();</script>"));
			base.Response.End();
		}
	}
}