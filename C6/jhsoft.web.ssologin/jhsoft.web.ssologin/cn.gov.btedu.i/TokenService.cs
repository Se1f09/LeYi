using jhsoft.web.ssologin.Properties;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;

namespace jhsoft.web.ssologin.cn.gov.btedu.i
{
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("System.Web.Services", "2.0.50727.4927")]
	[WebServiceBinding(Name="TokenServiceSoap", Namespace="http://www.passport.com/")]
	public class TokenService : SoapHttpClientProtocol
	{
		private SendOrPostCallback TokenGetCredenceOperationCompleted;

		private SendOrPostCallback ClearTokenOperationCompleted;

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

		public TokenService()
		{
			this.Url = Settings.Default.jhsoft_web_ssologin_cn_gov_btedu_i_TokenService;
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

		public IAsyncResult BeginClearToken(string tokenValue, AsyncCallback callback, object asyncState)
		{
			object[] objArray = new object[] { tokenValue };
			return base.BeginInvoke("ClearToken", objArray, callback, asyncState);
		}

		public IAsyncResult BeginTokenGetCredence(string tokenValue, string sys_id, AsyncCallback callback, object asyncState)
		{
			object[] objArray = new object[] { tokenValue, sys_id };
			return base.BeginInvoke("TokenGetCredence", objArray, callback, asyncState);
		}

		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		[SoapDocumentMethod("http://www.passport.com/ClearToken", RequestNamespace="http://www.passport.com/", ResponseNamespace="http://www.passport.com/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public void ClearToken(string tokenValue)
		{
			object[] objArray = new object[] { tokenValue };
			base.Invoke("ClearToken", objArray);
		}

		public void ClearTokenAsync(string tokenValue)
		{
			this.ClearTokenAsync(tokenValue, null);
		}

		public void ClearTokenAsync(string tokenValue, object userState)
		{
			if (this.ClearTokenOperationCompleted == null)
			{
				this.ClearTokenOperationCompleted = new SendOrPostCallback(this.OnClearTokenOperationCompleted);
			}
			object[] objArray = new object[] { tokenValue };
			base.InvokeAsync("ClearToken", objArray, this.ClearTokenOperationCompleted, userState);
		}

		public void EndClearToken(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		public string[] EndTokenGetCredence(IAsyncResult asyncResult)
		{
			return (string[])base.EndInvoke(asyncResult)[0];
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

		private void OnClearTokenOperationCompleted(object arg)
		{
			if (this.ClearTokenCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this.ClearTokenCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		private void OnTokenGetCredenceOperationCompleted(object arg)
		{
			if (this.TokenGetCredenceCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArg = (InvokeCompletedEventArgs)arg;
				this.TokenGetCredenceCompleted(this, new TokenGetCredenceCompletedEventArgs(invokeCompletedEventArg.Results, invokeCompletedEventArg.Error, invokeCompletedEventArg.Cancelled, invokeCompletedEventArg.UserState));
			}
		}

		[SoapDocumentMethod("http://www.passport.com/TokenGetCredence", RequestNamespace="http://www.passport.com/", ResponseNamespace="http://www.passport.com/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
		public string[] TokenGetCredence(string tokenValue, string sys_id)
		{
			object[] objArray = new object[] { tokenValue, sys_id };
			return (string[])base.Invoke("TokenGetCredence", objArray)[0];
		}

		public void TokenGetCredenceAsync(string tokenValue, string sys_id)
		{
			this.TokenGetCredenceAsync(tokenValue, sys_id, null);
		}

		public void TokenGetCredenceAsync(string tokenValue, string sys_id, object userState)
		{
			if (this.TokenGetCredenceOperationCompleted == null)
			{
				this.TokenGetCredenceOperationCompleted = new SendOrPostCallback(this.OnTokenGetCredenceOperationCompleted);
			}
			object[] objArray = new object[] { tokenValue, sys_id };
			base.InvokeAsync("TokenGetCredence", objArray, this.TokenGetCredenceOperationCompleted, userState);
		}

		public event ClearTokenCompletedEventHandler ClearTokenCompleted;

		public event TokenGetCredenceCompletedEventHandler TokenGetCredenceCompleted;
	}
}