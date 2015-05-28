using JHBase;
using JHSoft.IDAL;
using RTXSAPILib;
using System;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml;

namespace JHSoft.web.ssoc6
{
	public class RTXSkip : System.Web.UI.Page
	{
		private DBOperator ObjDAL = DBOperatorFactory.GetDBOperator();

		protected HtmlForm Form1;

		public RTXSkip()
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
			string innerText = "";
			string str = "";
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				try
				{
					xmlDocument.Load(base.Request.InputStream);
				}
				catch (COMException cOMException)
				{
				}
				XmlNode xmlNodes = xmlDocument.SelectSingleNode("//Root//a1");
				XmlNode xmlNodes1 = xmlDocument.SelectSingleNode("//Root//a2");
				innerText = xmlNodes.InnerText;
				str = xmlNodes1.InnerText;
				string empty = string.Empty;
				INIFile nIFile = new INIFile(Common.GetRTXIniPath(""));
				string str1 = nIFile.ReadString("RTXMessage", "RTXip");
				string str2 = nIFile.ReadString("RTXMessage", "RTXport");
				RTXSAPIRootObj2 rTXSAPIRootObjClass = (RTXSAPIRootObj2)(new RTXSAPIRootObjClass());
				rTXSAPIRootObjClass.ServerIP = str1;
				rTXSAPIRootObjClass.ServerPort = Convert.ToInt16(str2);
				if (!((RTXSAPIUserAuthObj2)rTXSAPIRootObjClass.UserAuthObj).SignatureAuth(innerText, str))
				{
					base.Response.Write("签名验证失败，请登陆RTX客户端后重试。");
				}
				else
				{
					DBOperator objDAL = this.ObjDAL;
					string[] strArrays = new string[] { "Exec createSign '", innerText, "','", innerText, "ERYW'" };
					objDAL.ExecSQLReInt(string.Concat(strArrays));
					base.Response.Write("0");
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				base.Response.Write(string.Concat(exception.Message, "请确认是否成功登陆RTX客户端"));
			}
		}
	}
}