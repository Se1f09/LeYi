using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;

namespace JHSoft.web.ssoc6.JHSSO
{
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("System.Web.Services", "2.0.50727.3053")]
	[WebServiceBinding(Name="CheckSSOSoap", Namespace="http://tempuri.org/")]
	public class CheckSSO : SoapHttpClientProtocol
	{
		private SendOrPostCallback CheckSignOperationCompleted;

		private bool useDefaultCredentialsSetExplicitly;

		public new string Url
		{
			get
			{
				return base.Url;
			}
			set
			{
				if ((!this.IsLocalFileSystemWebService(base.Url) || this.useDefaultCredentialsSetExplicitly ? false : !this.IsLocalFileSystemWebService(value)))
				{
					base.UseDefaultCredentials = false;
				}
				base.Url = value;
			}
		}

		public new bool UseDefaultCredentials
		{
			get
			{
				return base.UseDefaultCredentials;
			}
			set
			{
				base.UseDefaultCredentials = value;
				this.useDefaultCredentialsSetExplicitly = true;
			}
		}

		public CheckSSO()
		{
			this.Url = "http://localhost/c6/JHSoft.Web.SSO/CheckSSO.asmx";
			if (!this.IsLocalFileSystemWebService(this.Url))
			{
				this.useDefaultCredentialsSetExplicitly = true;
			}
			else
			{
				this.UseDefaultCredentials = true;
				this.useDefaultCredentialsSetExplicitly = false;
			}
		}

		public IAsyncResult BeginCheckSign(string sign, AsyncCallback callback, object asyncState)
		{
			object[] objArray = new object[] { sign };
			return base.BeginInvoke("CheckSign", objArray, callback, asyncState);
		}

		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		[SoapDocumentMethod("http://tempuri.org/CheckSign", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public string CheckSign(string sign)
		{
			object[] objArray = new object[] { sign };
			return (string)base.Invoke("CheckSign", objArray)[0];
		}

		public void CheckSignAsync(string sign)
		{
			this.CheckSignAsync(sign, null);
		}

		public void CheckSignAsync(string sign, object userState)
		{
			if (this.CheckSignOperationCompleted == null)
			{
				this.CheckSignOperationCompleted = new SendOrPostCallback(this.OnCheckSignOperationCompleted);
			}
			object[] objArray = new object[] { sign };
			base.InvokeAsync("CheckSign", objArray, this.CheckSignOperationCompleted, userState);
		}

		public string EndCheckSign(IAsyncResult asyncResult)
		{
			return (string)base.EndInvoke(asyncResult)[0];
		}

		private bool IsLocalFileSystemWebService(string url)
		{
			bool flag;
			if ((url == null ? false : !(url == string.Empty)))
			{
				System.Uri uri = new System.Uri(url);
				flag = ((uri.Port < 1024 ? true : string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) != 0) ? false : true);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		private void OnCheckSignOperationCompleted(object arg)
		{
			if (this.CheckSignCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this.CheckSignCompleted(this, new CheckSignCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		public event CheckSignCompletedEventHandler CheckSignCompleted;
	}
}