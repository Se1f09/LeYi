using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace jhsoft.web.ssologin.Properties
{
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings defaultInstance;

		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("http://i.btedu.gov.cn/ssodemo/tokenservice.asmx")]
		[SpecialSetting(SpecialSetting.WebServiceUrl)]
		public string jhsoft_web_ssologin_cn_gov_btedu_i_TokenService
		{
			get
			{
				return (string)this["jhsoft_web_ssologin_cn_gov_btedu_i_TokenService"];
			}
		}

		static Settings()
		{
			Settings.defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
		}

		public Settings()
		{
		}
	}
}