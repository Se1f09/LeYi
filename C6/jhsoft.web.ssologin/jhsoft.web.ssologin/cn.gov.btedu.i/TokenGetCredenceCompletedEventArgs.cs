using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace jhsoft.web.ssologin.cn.gov.btedu.i
{
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("System.Web.Services", "2.0.50727.4927")]
	public class TokenGetCredenceCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public string[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[0];
			}
		}

		internal TokenGetCredenceCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
}