using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Test.Tools
{
	public class HttpClientTest
	{
		public static void Test01()
		{
#if false
			{
				string s = "a\n".Trim();
				DebugTools.WriteLog("[" + s + "]");
				s = "a\r\n".Trim();
				DebugTools.WriteLog("[" + s + "]");
			}
#endif

			//GetTest("http://localhost/");
			GetTest("https://www.google.co.jp/");
			//GetTest("http://localhost/Echo/Echo.cgi", Encoding.ASCII.GetBytes("Test Body"));
		}

		private static void GetTest(string url, byte[] body = null)
		{
			HttpClient hc = new HttpClient(url);
			hc.AddHeader("X-Header", "Value 123 ABC");
			hc.SetIEProxy();
			hc.Send(body);

			DebugTools.WriteLog(url);

			foreach (string name in hc.GetResHeaders().Keys)
				DebugTools.WriteLog(name + ": " + hc.GetResHeaders()[name]);

			DebugTools.WriteLog(StringTools.ENCODING_SJIS.GetString(hc.GetResBody()));
		}
	}
}
