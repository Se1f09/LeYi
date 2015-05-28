using Microsoft.Win32;
using System;

namespace JHSoft.web.ssoc6
{
	public class Common
	{
		private static string RegStr;

		private static string RegStr64;

		static Common()
		{
			Common.RegStr = "Software\\Jinher\\IOA";
			Common.RegStr64 = "Software\\Wow6432Node\\Jinher\\IOA";
		}

		public Common()
		{
		}

		public static string GetRTXIniPath(string StrC6Path)
		{
			string value = "";
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(Common.RegStr);
			if (registryKey == null)
			{
				registryKey = Registry.LocalMachine.OpenSubKey(Common.RegStr64);
			}
			if (registryKey != null)
			{
				value = (string)registryKey.GetValue("Install", "D:\\Jinher\\C6");
				registryKey.Close();
			}
			if (value.Length > 0)
			{
				if (value.LastIndexOf('\\') != value.Length - 1)
				{
					value = string.Concat(value, "\\");
				}
				value = string.Concat(value, "System\\RTXInfo.ini");
			}
			return value;
		}
	}
}