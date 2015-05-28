using JHSoft.Globalization;
using JHSoft.IDAL;
using JHSoft.Login;
using jhsoft.web.ssologin.cn.gov.btedu.i;
using JHSoft.WorkFlat;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace jhsoft.web.ssologin
{
    public class SSOLoginToC6 : System.Web.UI.Page
    {
        protected HtmlForm form1;

        protected HtmlGenericControl frmContent;

        public SSOLoginToC6()
        {
        }

        private void CreateSession(string userID)
        {
            DataTable dataTable = (new JHSoft.Login.Roles()).SearchWuXiSession(userID);
            if ((dataTable == null ? false : dataTable.Rows.Count > 0))
            {
                DataRow[] dataRowArray = dataTable.Select("RelaPrimary=1");
                if ((int)dataRowArray.Length >= 1)
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

        protected string SsoSignOn
        {
            get
            {
                return WebConfigurationManager.AppSettings["SsoSignOn"];
            }
        }

        protected string SsoSignTo
        {
            get
            {
                return WebConfigurationManager.AppSettings["SsoSignTo"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["OnlineId"]))
            {
                Response.Redirect(string.Format("{0}?SsoRedirect={1}", SsoSignOn, Server.UrlEncode(SsoSignTo)));
                return;
            }
            else
            {
                var onlineId = Request.QueryString["OnlineId"];
                if (!base.IsPostBack)
                {
                    if (onlineId != "")
                    {
                        DBOperator dBOperator = DBOperatorFactory.GetDBOperator();
                        var tttt = dBOperator.ExecSQLReDataTable(string.Concat("select UserID, Account from [HomorySsoView] where Id='", onlineId, "'"));
                        if (tttt.Rows.Count == 0)
                        {
                            Response.Redirect(string.Format("{0}", SsoSignOn));
                            return;
                        }
                        string str2 = (string)tttt.Rows[0][0];
                        string strX = (string)tttt.Rows[0][1];
                        PassWord passWord = new PassWord();
                        JHSoft.Login.Roles role = new JHSoft.Login.Roles();
                        this.Session["UserLoginCode"] = strX;
                        Localization.SetCulture("zh-cn", this);
                        if (!base.Request.Browser.Cookies)
                        {
                            base.Response.Write(string.Concat("<SCRIPT language=\"javascript\">alert(\"系统需要使用cookie。请打开浏览器的cookie选项。\");window.location.href='", this.SsoSignOn, "';</SCRIPT>"));
                        }
                        else
                        {
                            this.Session["UserCode"] = "";
                            string str3 = this.Session.SessionID.ToString();
                            string empty = string.Empty;
                            string empty1 = string.Empty;
                            if (!passWord.IsSqlConnect())
                            {
                                base.Response.Write(string.Concat("<SCRIPT language=\"javascript\">alert(\"数据库连接有问题，请检查。\");window.location.href='", this.SsoSignOn, "';</SCRIPT>"));
                            }
                            else
                            {
                                empty = role.GetLogin(str2);
                                if (!(empty == "OK"))
                                {
                                    if (empty == "Empty")
                                    {
                                        base.Response.Write(string.Concat("<SCRIPT language=\"javascript\">alert(\"用户不存在，请重新登录。\");window.location.href='", this.SsoSignOn, "';</SCRIPT>"));
                                    }
                                    if (empty == "Delete")
                                    {
                                        base.Response.Write(string.Concat("<SCRIPT language=\"javascript\">alert(\"用户已删除，请重新登录。\");window.location.href='", this.SsoSignOn, "';</SCRIPT>"));
                                    }
                                    if (empty == "Leaver")
                                    {
                                        base.Response.Write(string.Concat("<SCRIPT language=\"javascript\">alert(\"用户离职，请重新登录。\");window.location.href='", this.SsoSignOn, "';</SCRIPT>"));
                                    }
                                    if (empty == "Padlock")
                                    {
                                        base.Response.Write(string.Concat("<SCRIPT language=\"javascript\">alert(\"用户已锁定，请重新登录。\");window.location.href='", this.SsoSignOn, "';</SCRIPT>"));
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
                                                base.Response.Write(string.Concat("<SCRIPT language=\"javascript\">alert(\"密码已到期，请修改密码。\");window.location.href='", this.SsoSignOn, "';</SCRIPT>"));
                                            }
                                            else
                                            {
                                                if (str4 != "ERROR")
                                                {
                                                    HttpResponse response = base.Response;
                                                    string[] strArrays1 = new string[] { "<SCRIPT language=\"javascript\">alert(\"密码最小长度为", empty1.ToString(), "位，请修改密码。\");window.location.href='", this.SsoSignOn, "';</SCRIPT>" };
                                                    response.Write(string.Concat(strArrays1));
                                                }
                                                else
                                                {
                                                    base.Response.Write(string.Concat("<SCRIPT language=\"javascript\">alert(\"用户名和密码出错，请重新登陆。\");window.location.href='", this.SsoSignOn, "';</SCRIPT>"));
                                                }
                                            }
                                        }
                                    }
                                    else if (!role.IsAllowLogin(str2, base.Request.ServerVariables["REMOTE_ADDR"]))
                                    {
                                        base.Response.Write(string.Concat("<SCRIPT language=\"javascript\">alert(\"您的IP已被系统限制。请重新登陆\");window.location.href='", this.SsoSignOn, "';</SCRIPT>"));
                                    }
                                    else if (!role.GetEpass(str2))
                                    {
                                        base.Response.Write(string.Concat("<SCRIPT language=\"javascript\">alert(\"您是EPass用户，请选择EPass登陆方式，或者让管理员通过Epass失效操作来还原为正常登陆方式。\");window.location.href='", this.SsoSignOn, "';</SCRIPT>"));
                                    }
                                    else if (!role.InsertLogin(str2, str3))
                                    {
                                        base.Response.Write(string.Concat("<SCRIPT language=\"javascript\">alert(\"用户添加登陆日志错误。请重新登陆。\");window.location.href='", this.SsoSignOn, "';</SCRIPT>"));
                                    }
                                    else
                                    {
                                        string roles = role.GetRoles(str2);
                                        DateTime now = DateTime.Now;
                                        DateTime dateTime = DateTime.Now;
                                        FormsAuthenticationTicket formsAuthenticationTicket = new FormsAuthenticationTicket(1, str2, now, dateTime.AddMinutes(600), false, roles, "/");
                                        string str5 = FormsAuthentication.Encrypt(formsAuthenticationTicket);
                                        HttpCookie httpCookie = new HttpCookie(FormsAuthentication.FormsCookieName, str5);
                                        this.Context.Response.Cookies.Add(httpCookie);
                                        HttpCookie httpCookie1 = new HttpCookie("Login")
                                        {
                                            Value = "PassWord",
                                            Expires = DateTime.MaxValue
                                        };
                                        base.Response.Cookies.Add(httpCookie1);
                                        HttpCookie httpCookie2 = new HttpCookie(string.Concat("SessionID", str2))
                                        {
                                            Value = str3,
                                            Expires = DateTime.MaxValue
                                        };
                                        base.Response.Cookies.Add(httpCookie2);
                                        this.CreateSession(str2);
                                        base.Response.Redirect("../JHSoft.Web.WorkFlat/Index.aspx?OnlineId="+Request.QueryString["OnlineId"]);
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