using System;
using System.IO;
using System.Net;
using System.Text;

namespace JHSoft.web.ssoc6
{
	public class HttpUtility
	{
		public HttpUtility()
		{
		}

		public string GetPage(string posturl, string postData)
		{
			string empty;
			string str;
			Stream requestStream = null;
			HttpWebResponse response = null;
			HttpWebRequest cookieContainer = null;
			Encoding encoding = Encoding.GetEncoding("utf-8");
			byte[] bytes = encoding.GetBytes(postData);
			try
			{
				cookieContainer = WebRequest.Create(posturl) as HttpWebRequest;
				cookieContainer.CookieContainer = new CookieContainer();
				cookieContainer.AllowAutoRedirect = true;
				cookieContainer.Method = "POST";
				cookieContainer.ContentType = "application/x-www-form-urlencoded";
				cookieContainer.ContentLength = (long)((int)bytes.Length);
				requestStream = cookieContainer.GetRequestStream();
				requestStream.Write(bytes, 0, (int)bytes.Length);
				requestStream.Close();
				response = cookieContainer.GetResponse() as HttpWebResponse;
				string end = (new StreamReader(response.GetResponseStream(), encoding)).ReadToEnd();
				empty = string.Empty;
				str = end;
			}
			catch (Exception exception)
			{
				empty = exception.Message;
				str = string.Empty;
			}
			return str;
		}
	}
}